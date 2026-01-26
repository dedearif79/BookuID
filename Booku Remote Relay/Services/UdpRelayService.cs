using System.Net;
using System.Net.Sockets;
using BookuRemoteRelay.Models;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Service untuk relay UDP video streaming antara Host dan Tamu.
/// UDP digunakan untuk data plane (video frames) karena tidak memerlukan reliability.
/// Paket yang lost/dropped tidak di-retransmit - frame berikutnya akan datang.
/// </summary>
public class UdpRelayService
{
    /// <summary>
    /// Ukuran header UDP packet (sama dengan VB.NET UdpConstants.UDP_HEADER_SIZE).
    /// SessionId(4) + FrameId(4) + ChunkIndex(2) + ChunkCount(2) + TimestampMs(4) = 16 bytes
    /// </summary>
    private const int UDP_HEADER_SIZE = 16;

    private readonly int _port;
    private readonly ConnectionManager _connectionManager;

    private UdpClient? _udpClient;
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    // Statistik
    private long _packetsReceived;
    private long _packetsForwarded;
    private long _packetsDropped;

    public UdpRelayService(int port, ConnectionManager connectionManager)
    {
        _port = port;
        _connectionManager = connectionManager;
    }

    /// <summary>
    /// Mulai UDP relay service.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (_isRunning) return;

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        try
        {
            _udpClient = new UdpClient(_port);
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _isRunning = true;

            Console.WriteLine($"[UDP-RELAY] Listening on port {_port}...");

            // Start receive loop
            _ = ReceiveLoopAsync(_cts.Token);

            // Start stats loop
            _ = StatsLoopAsync(_cts.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UDP-RELAY] Failed to start: {ex.Message}");
            _isRunning = false;
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Hentikan UDP relay service.
    /// </summary>
    public void Stop()
    {
        if (!_isRunning) return;

        _cts?.Cancel();
        _udpClient?.Close();
        _udpClient?.Dispose();
        _udpClient = null;
        _isRunning = false;

        Console.WriteLine("[UDP-RELAY] Stopped.");
    }

    /// <summary>
    /// Loop menerima dan meneruskan paket UDP.
    /// </summary>
    private async Task ReceiveLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested && _udpClient != null)
        {
            try
            {
                // Terima paket UDP
                var result = await _udpClient.ReceiveAsync(ct);
                var data = result.Buffer;
                var remoteEndPoint = result.RemoteEndPoint;

                _packetsReceived++;

                // Validasi ukuran minimum (header saja)
                if (data.Length < UDP_HEADER_SIZE)
                {
                    _packetsDropped++;
                    continue;
                }

                // Parse SessionId dari header (4 byte pertama, little-endian)
                int udpSessionId = BitConverter.ToInt32(data, 0);

                // Lookup session
                var session = _connectionManager.GetSessionByUdpSessionId(udpSessionId);
                if (session == null || !session.IsActive)
                {
                    _packetsDropped++;
                    continue;
                }

                // Tentukan apakah paket dari Host atau Tamu berdasarkan endpoint
                // PENTING: Bandingkan FULL endpoint (IP + Port), bukan hanya IP!
                // Ini penting ketika Host dan Tamu di belakang NAT yang sama (IP publik sama)
                IPEndPoint? targetEndPoint = null;
                bool isFromHost = false;

                // Cek apakah dari Host (bandingkan full endpoint)
                if (session.Host?.UdpEndPoint != null &&
                    session.Host.UdpEndPoint.Equals(remoteEndPoint))
                {
                    // Paket dari Host, kirim ke Tamu
                    isFromHost = true;
                    targetEndPoint = session.Tamu?.UdpEndPoint;
                }
                // Cek apakah dari Tamu (bandingkan full endpoint)
                else if (session.Tamu?.UdpEndPoint != null &&
                         session.Tamu.UdpEndPoint.Equals(remoteEndPoint))
                {
                    // Paket dari Tamu, kirim ke Host
                    isFromHost = false;
                    targetEndPoint = session.Host?.UdpEndPoint;
                }
                else
                {
                    // Endpoint belum terdaftar - register berdasarkan role
                    // Heuristik: Paket video biasanya dari Host ke Tamu
                    // Cek apakah Host sudah punya endpoint
                    if (session.Host?.UdpEndPoint == null)
                    {
                        // Register sebagai Host endpoint
                        _connectionManager.UpdateHostUdpEndpoint(udpSessionId, remoteEndPoint);
                        targetEndPoint = session.Tamu?.UdpEndPoint;
                        isFromHost = true;
                    }
                    else if (session.Tamu?.UdpEndPoint == null)
                    {
                        // Register sebagai Tamu endpoint
                        _connectionManager.UpdateTamuUdpEndpoint(udpSessionId, remoteEndPoint);
                        targetEndPoint = session.Host?.UdpEndPoint;
                        isFromHost = false;
                    }
                    else
                    {
                        // Both endpoints sudah ada tapi endpoint tidak match - drop
                        _packetsDropped++;
                        continue;
                    }
                }

                // Forward paket ke target
                if (targetEndPoint != null)
                {
                    await _udpClient.SendAsync(data, data.Length, targetEndPoint);
                    _packetsForwarded++;

                    // Update activity
                    session.UpdateActivity();

                    // Update stats di session
                    if (isFromHost)
                    {
                        session.BytesRelayedToTamu += data.Length;
                    }
                    else
                    {
                        session.BytesRelayedToHost += data.Length;
                    }
                }
                else
                {
                    _packetsDropped++;
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (SocketException)
            {
                // Socket closed, normal during shutdown
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UDP-RELAY] Error: {ex.Message}");
                _packetsDropped++;
            }
        }

        Console.WriteLine("[UDP-RELAY] Receive loop ended.");
    }

    /// <summary>
    /// Loop untuk print statistik.
    /// </summary>
    private async Task StatsLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(60), ct);

                if (_packetsReceived > 0)
                {
                    Console.WriteLine($"[UDP-RELAY] Stats: Received={_packetsReceived}, " +
                                    $"Forwarded={_packetsForwarded}, Dropped={_packetsDropped}");
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Apakah service sedang berjalan.
    /// </summary>
    public bool IsRunning => _isRunning;

    /// <summary>
    /// Statistik service.
    /// </summary>
    public (long received, long forwarded, long dropped) GetStats()
    {
        return (_packetsReceived, _packetsForwarded, _packetsDropped);
    }
}
