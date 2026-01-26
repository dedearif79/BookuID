using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk menerima video frame via UDP streaming.
/// Menangani penerimaan paket UDP, reassembly frame dari chunks, dan timeout handling.
/// </summary>
public class UdpStreamingService : IDisposable
{
    #region Constants

    /// <summary>Ukuran header UDP packet (bytes)</summary>
    private const int UDP_HEADER_SIZE = 16;

    /// <summary>Ukuran codec type byte (bagian dari payload)</summary>
    private const int CODEC_TYPE_SIZE = 1;

    /// <summary>Codec type: JPEG (default)</summary>
    public const byte CODEC_TYPE_JPEG = 0;

    /// <summary>Codec type: H.264</summary>
    public const byte CODEC_TYPE_H264 = 1;

    /// <summary>Timeout untuk frame assembly (ms)</summary>
    private const int FRAME_TIMEOUT_MS = 100;

    /// <summary>Interval untuk cleanup frame timeout (ms)</summary>
    private const int CLEANUP_INTERVAL_MS = 50;

    /// <summary>Maksimum concurrent frames yang di-track</summary>
    private const int MAX_PENDING_FRAMES = 10;

    #endregion

    #region Fields

    private UdpClient? _udpClient;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _receiveTask;
    private Task? _cleanupTask;

    private int _sessionId;
    private bool _isRunning;
    private readonly object _lock = new();

    // Frame assembly
    private readonly ConcurrentDictionary<int, FrameAssemblyInfo> _pendingFrames = new();

    // Statistics
    private int _framesReceived;
    private int _framesDropped;
    private int _packetsReceived;
    private DateTime _lastStatTime = DateTime.UtcNow;

    #endregion

    #region Events

    /// <summary>
    /// Event ketika frame lengkap diterima
    /// </summary>
    public event EventHandler<UdpFrameEventArgs>? FrameReceived;

    /// <summary>
    /// Event untuk statistik UDP
    /// </summary>
    public event EventHandler<UdpStatisticsEventArgs>? StatisticsUpdated;

    #endregion

    #region Properties

    /// <summary>
    /// Port UDP video aktif (dari SettingsService)
    /// </summary>
    public int PortUdpVideo => SettingsService.Current.PortUdpVideo;

    /// <summary>
    /// IP relay server aktif (dari SettingsService)
    /// </summary>
    public string RelayServerIP => SettingsService.Current.RelayServerIP;

    /// <summary>
    /// Status apakah receiver sedang berjalan
    /// </summary>
    public bool IsRunning => _isRunning;

    #endregion

    #region Public Methods

    /// <summary>
    /// Memulai UDP receiver
    /// </summary>
    /// <param name="sessionId">Session ID untuk filtering paket</param>
    public void StartReceiver(int sessionId)
    {
        lock (_lock)
        {
            if (_isRunning) return;

            _sessionId = sessionId;
            _cancellationTokenSource = new CancellationTokenSource();
            _pendingFrames.Clear();

            // Reset statistics
            _framesReceived = 0;
            _framesDropped = 0;
            _packetsReceived = 0;
            _lastStatTime = DateTime.UtcNow;

            try
            {
                // Buat UDP client untuk receive
                // Mode LAN: bind ke port PortUdpVideo (45681) agar cocok dengan yang Host kirim
                // Mode Relay: juga pakai port ini untuk konsistensi
                var listenPort = SettingsService.Current.PortUdpVideo;
                _udpClient = new UdpClient(listenPort);
                _udpClient.Client.ReceiveTimeout = 5000;

                System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] Receiver started on port {listenPort}, SessionId={sessionId}");

                _isRunning = true;

                // Start receive loop
                _receiveTask = Task.Run(() => ReceiveLoopAsync(_cancellationTokenSource.Token));

                // Start cleanup loop untuk timeout handling
                _cleanupTask = Task.Run(() => CleanupLoopAsync(_cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] StartReceiver error: {ex.Message}");
                _isRunning = false;
            }
        }
    }

    /// <summary>
    /// Menghentikan UDP receiver
    /// </summary>
    public void StopReceiver()
    {
        lock (_lock)
        {
            if (!_isRunning) return;

            _isRunning = false;
            _cancellationTokenSource?.Cancel();

            try
            {
                _udpClient?.Close();
                _udpClient?.Dispose();
            }
            catch { }

            _udpClient = null;
            _pendingFrames.Clear();

            System.Diagnostics.Debug.WriteLine("[UDP-ANDROID] Receiver stopped");
        }
    }

    /// <summary>
    /// Mendapatkan local endpoint untuk dikirim ke relay/host
    /// </summary>
    public IPEndPoint? GetLocalEndPoint()
    {
        return _udpClient?.Client.LocalEndPoint as IPEndPoint;
    }

    /// <summary>
    /// Mengirim paket "register" ke relay agar relay tahu endpoint kita
    /// </summary>
    public async Task SendRegistrationAsync(string relayServerIP, int relayUdpPort)
    {
        if (_udpClient == null || _sessionId == 0) return;

        try
        {
            var endpoint = new IPEndPoint(IPAddress.Parse(relayServerIP), relayUdpPort);

            // Buat header-only packet untuk registrasi
            // Header: SessionId(4) + FrameId(4) + ChunkIndex(2) + ChunkCount(2) + TimestampMs(4) = 16 bytes
            var header = new byte[UDP_HEADER_SIZE];
            BitConverter.GetBytes(_sessionId).CopyTo(header, 0);      // SessionId
            BitConverter.GetBytes(0).CopyTo(header, 4);               // FrameId = 0 (registration)
            BitConverter.GetBytes((ushort)0).CopyTo(header, 8);       // ChunkIndex
            BitConverter.GetBytes((ushort)0).CopyTo(header, 10);      // ChunkCount
            BitConverter.GetBytes((uint)0).CopyTo(header, 12);        // TimestampMs

            await _udpClient.SendAsync(header, header.Length, endpoint);

            System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] Registration sent to {relayServerIP}:{relayUdpPort}, SessionId={_sessionId}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] SendRegistration error: {ex.Message}");
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Loop penerimaan UDP packets
    /// </summary>
    private async Task ReceiveLoopAsync(CancellationToken token)
    {
        System.Diagnostics.Debug.WriteLine("[UDP-ANDROID] Receive loop started");

        while (!token.IsCancellationRequested && _udpClient != null)
        {
            try
            {
                var result = await _udpClient.ReceiveAsync(token);
                var data = result.Buffer;

                if (data.Length < UDP_HEADER_SIZE)
                {
                    continue; // Invalid packet
                }

                // Parse header
                int sessionId = BitConverter.ToInt32(data, 0);
                int frameId = BitConverter.ToInt32(data, 4);
                ushort chunkIndex = BitConverter.ToUInt16(data, 8);
                ushort chunkCount = BitConverter.ToUInt16(data, 10);
                uint timestampMs = BitConverter.ToUInt32(data, 12);

                // Filter by session ID
                if (sessionId != _sessionId)
                {
                    continue;
                }

                // Skip registration packets (frameId = 0)
                if (frameId == 0)
                {
                    continue;
                }

                _packetsReceived++;

                // Log lebih detail untuk debugging frame size
                var videoDataLen = data.Length - UDP_HEADER_SIZE - CODEC_TYPE_SIZE;
                if (_packetsReceived <= 20 || _packetsReceived % 20 == 1)
                {
                    System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] Packet #{_packetsReceived}: frameId={frameId}, chunk={chunkIndex}/{chunkCount}, rawSize={data.Length}, videoDataLen={videoDataLen}, codec={data[UDP_HEADER_SIZE]}");
                }

                // Extract payload (termasuk codec type byte)
                var payloadLength = data.Length - UDP_HEADER_SIZE;
                if (payloadLength < CODEC_TYPE_SIZE)
                {
                    continue; // No codec type byte
                }

                // Extract codec type (byte pertama payload)
                byte codecType = data[UDP_HEADER_SIZE];

                // Extract video data (setelah codec type)
                var videoDataLength = payloadLength - CODEC_TYPE_SIZE;
                var videoData = new byte[videoDataLength];
                Buffer.BlockCopy(data, UDP_HEADER_SIZE + CODEC_TYPE_SIZE, videoData, 0, videoDataLength);

                // Process chunk
                ProcessChunk(frameId, chunkIndex, chunkCount, timestampMs, codecType, videoData);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
            {
                // Timeout, continue
                continue;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] Receive error: {ex.Message}");
                await Task.Delay(10, token);
            }
        }

        System.Diagnostics.Debug.WriteLine("[UDP-ANDROID] Receive loop ended");
    }

    /// <summary>
    /// Memproses chunk yang diterima
    /// </summary>
    private void ProcessChunk(int frameId, ushort chunkIndex, ushort chunkCount, uint timestampMs, byte codecType, byte[] videoData)
    {
        // Get or create frame assembly info
        var frameInfo = _pendingFrames.GetOrAdd(frameId, _ => new FrameAssemblyInfo
        {
            FrameId = frameId,
            ChunkCount = chunkCount,
            TimestampMs = timestampMs,
            CodecType = codecType,
            ReceivedTime = DateTime.UtcNow,
            Chunks = new byte[chunkCount][]
        });

        // Store chunk
        if (chunkIndex < frameInfo.ChunkCount && frameInfo.Chunks != null)
        {
            frameInfo.Chunks[chunkIndex] = videoData;
            frameInfo.ReceivedChunks++;

            // Check if frame is complete
            if (frameInfo.ReceivedChunks == frameInfo.ChunkCount)
            {
                // Assemble frame
                AssembleAndDeliverFrame(frameInfo);

                // Remove from pending
                _pendingFrames.TryRemove(frameId, out _);
            }
        }

        // Cleanup old frames if too many pending
        if (_pendingFrames.Count > MAX_PENDING_FRAMES)
        {
            CleanupOldFrames();
        }
    }

    /// <summary>
    /// Menyusun dan mengirim frame yang lengkap
    /// </summary>
    private void AssembleAndDeliverFrame(FrameAssemblyInfo frameInfo)
    {
        if (frameInfo.Chunks == null) return;

        try
        {
            // Calculate total size
            int totalSize = 0;
            foreach (var chunk in frameInfo.Chunks)
            {
                if (chunk != null)
                    totalSize += chunk.Length;
            }

            // Assemble
            var frameData = new byte[totalSize];
            int offset = 0;
            foreach (var chunk in frameInfo.Chunks)
            {
                if (chunk != null)
                {
                    Buffer.BlockCopy(chunk, 0, frameData, offset, chunk.Length);
                    offset += chunk.Length;
                }
            }

            _framesReceived++;

            // Log frame assembly lebih detail untuk debugging
            if (_framesReceived <= 20 || _framesReceived % 10 == 1)
            {
                System.Diagnostics.Debug.WriteLine($"[UDP-FRAME] #{_framesReceived} (id={frameInfo.FrameId}): {totalSize} bytes, chunks={frameInfo.ChunkCount}, codec={frameInfo.CodecType}");

                // Log warning jika frame terlalu kecil untuk H.264
                if (frameInfo.CodecType == CODEC_TYPE_H264 && totalSize < 50)
                {
                    System.Diagnostics.Debug.WriteLine($"[UDP-FRAME] WARNING: H.264 frame too small! Size={totalSize} bytes, expected >50 bytes for valid NAL");
                }
            }

            // Raise event
            FrameReceived?.Invoke(this, new UdpFrameEventArgs
            {
                FrameId = frameInfo.FrameId,
                FrameData = frameData,
                TimestampMs = frameInfo.TimestampMs,
                CodecType = frameInfo.CodecType
            });

            // Update statistics
            UpdateStatistics();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[UDP-ANDROID] AssembleFrame error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loop untuk cleanup frame yang timeout
    /// </summary>
    private async Task CleanupLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(CLEANUP_INTERVAL_MS, token);
                CleanupOldFrames();
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Membersihkan frame yang sudah timeout
    /// </summary>
    private void CleanupOldFrames()
    {
        var now = DateTime.UtcNow;
        var keysToRemove = new List<int>();

        foreach (var kvp in _pendingFrames)
        {
            if ((now - kvp.Value.ReceivedTime).TotalMilliseconds > FRAME_TIMEOUT_MS)
            {
                keysToRemove.Add(kvp.Key);
                _framesDropped++;
            }
        }

        foreach (var key in keysToRemove)
        {
            _pendingFrames.TryRemove(key, out _);
        }
    }

    /// <summary>
    /// Update dan raise statistics event
    /// </summary>
    private void UpdateStatistics()
    {
        var now = DateTime.UtcNow;
        var elapsed = (now - _lastStatTime).TotalSeconds;

        if (elapsed >= 1.0)
        {
            var fps = _framesReceived / elapsed;

            StatisticsUpdated?.Invoke(this, new UdpStatisticsEventArgs
            {
                FPS = (int)fps,
                FramesReceived = _framesReceived,
                FramesDropped = _framesDropped,
                PacketsReceived = _packetsReceived
            });

            // Reset counters
            _framesReceived = 0;
            _framesDropped = 0;
            _packetsReceived = 0;
            _lastStatTime = now;
        }
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
        StopReceiver();
        _cancellationTokenSource?.Dispose();
    }

    #endregion

    #region Static Methods

    /// <summary>
    /// Generate SessionId dari string menggunakan djb2 hash (deterministic, cross-platform).
    /// PENTING: GetHashCode() tidak konsisten antar platform (.NET Windows vs Android).
    /// djb2 menghasilkan hash yang sama di semua platform.
    /// </summary>
    public static int GenerateSessionId(string? sessionKey)
    {
        if (string.IsNullOrEmpty(sessionKey)) return 0;

        // djb2 hash algorithm - deterministic dan cross-platform
        uint hash = 5381;
        foreach (char c in sessionKey)
        {
            hash = ((hash << 5) + hash) ^ c;
        }

        // Convert to positive integer
        return (int)(hash & 0x7FFFFFFF);
    }

    #endregion

    #region Nested Classes

    /// <summary>
    /// Info untuk assembly frame dari chunks
    /// </summary>
    private class FrameAssemblyInfo
    {
        public int FrameId { get; set; }
        public ushort ChunkCount { get; set; }
        public uint TimestampMs { get; set; }
        public byte CodecType { get; set; } = CODEC_TYPE_JPEG;
        public DateTime ReceivedTime { get; set; }
        public byte[][]? Chunks { get; set; }
        public int ReceivedChunks { get; set; }
    }

    #endregion
}

#region Event Args

/// <summary>
/// Event args untuk frame UDP yang diterima
/// </summary>
public class UdpFrameEventArgs : EventArgs
{
    public int FrameId { get; set; }
    public byte[] FrameData { get; set; } = Array.Empty<byte>();
    public uint TimestampMs { get; set; }

    /// <summary>
    /// Codec type: 0=JPEG, 1=H.264
    /// </summary>
    public byte CodecType { get; set; } = UdpStreamingService.CODEC_TYPE_JPEG;

    /// <summary>
    /// Helper: apakah frame ini adalah JPEG
    /// </summary>
    public bool IsJPEG => CodecType == UdpStreamingService.CODEC_TYPE_JPEG;

    /// <summary>
    /// Helper: apakah frame ini adalah H.264
    /// </summary>
    public bool IsH264 => CodecType == UdpStreamingService.CODEC_TYPE_H264;
}

/// <summary>
/// Event args untuk statistik UDP
/// </summary>
public class UdpStatisticsEventArgs : EventArgs
{
    public int FPS { get; set; }
    public int FramesReceived { get; set; }
    public int FramesDropped { get; set; }
    public int PacketsReceived { get; set; }
}

#endregion
