using System.Net;
using System.Net.Sockets;
using System.Text;
using BookuRemoteRelay.Models;
using BookuRemoteRelay.Utils;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Service untuk menerima koneksi TCP dari Host dan Tamu.
/// </summary>
public class TcpListenerService
{
    private readonly int _port;
    private readonly ConnectionManager _connectionManager;
    private readonly PacketRouter _packetRouter;
    private readonly ProtocolService _protocolService;

    private TcpListener? _listener;
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    public TcpListenerService(
        int port,
        ConnectionManager connectionManager,
        PacketRouter packetRouter,
        ProtocolService protocolService)
    {
        _port = port;
        _connectionManager = connectionManager;
        _packetRouter = packetRouter;
        _protocolService = protocolService;
    }

    /// <summary>
    /// Mulai mendengarkan koneksi.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (_isRunning) return;

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        _isRunning = true;

        Console.WriteLine($"[RELAY] Listening on port {_port}...");

        // Loop menerima koneksi
        _ = AcceptConnectionsAsync(_cts.Token);

        // Loop cleanup
        _ = CleanupLoopAsync(_cts.Token);

        await Task.CompletedTask;
    }

    /// <summary>
    /// Hentikan listener.
    /// </summary>
    public void Stop()
    {
        if (!_isRunning) return;

        _cts?.Cancel();
        _listener?.Stop();
        _isRunning = false;

        Console.WriteLine("[RELAY] Stopped.");
    }

    /// <summary>
    /// Loop menerima koneksi baru.
    /// </summary>
    private async Task AcceptConnectionsAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested && _listener != null)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(ct);

                // Disable Nagle's algorithm untuk mengirim data segera
                client.NoDelay = true;

                var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                var clientIP = remoteEndPoint?.Address.ToString() ?? "unknown";

                Console.WriteLine($"[RELAY] New connection from {clientIP}");

                // Handle connection di thread terpisah
                _ = HandleConnectionAsync(client, clientIP, ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RELAY] Accept error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Handle satu koneksi client (bisa Host atau Tamu).
    /// </summary>
    private async Task HandleConnectionAsync(TcpClient client, string clientIP, CancellationToken ct)
    {
        var buffer = new List<byte>();
        var readBuffer = new byte[65536]; // 64KB read buffer
        NetworkStream? stream = null;

        // Belum tahu ini Host atau Tamu - tentukan dari paket pertama
        string? hostCode = null;
        string? tamuConnectionId = null;
        TipeClient clientType = TipeClient.UNKNOWN;

        int readLoopCount = 0;

        try
        {
            stream = client.GetStream();

            while (!ct.IsCancellationRequested && client.Connected)
            {
                readLoopCount++;

                // Baca data
                int bytesRead;
                try
                {
                    // Log setiap 10 read atau jika buffer besar
                    if (readLoopCount % 10 == 1 || buffer.Count > 5000)
                    {
                        Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} ({clientType}) - Waiting for data... Buffer={buffer.Count} bytes");
                    }

                    bytesRead = await stream.ReadAsync(readBuffer, 0, readBuffer.Length, ct);
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} - IOException: {ioEx.Message}");
                    break; // Connection closed
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} - ReadAsync Exception: {ex.GetType().Name}: {ex.Message}");
                    break;
                }

                if (bytesRead == 0)
                {
                    Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} - Connection closed (bytesRead=0)");
                    break; // Connection closed
                }

                // Log data diterima
                Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} ({clientType}) - Received {bytesRead} bytes, Total buffer: {buffer.Count + bytesRead} bytes");

                // Tambah ke buffer
                buffer.AddRange(readBuffer.Take(bytesRead));

                // Extract dan proses semua JSON lengkap
                var jsonMessages = JsonBracketParser.ExtractAllJson(buffer);

                // Log hasil extraction
                if (jsonMessages.Count > 0)
                {
                    Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} - Extracted {jsonMessages.Count} JSON message(s), Remaining buffer: {buffer.Count} bytes");
                }
                else if (buffer.Count > 5000)
                {
                    // Debug: Log hanya untuk buffer besar (skip logging kecil untuk mengurangi spam)
                    var firstChar = buffer.Count > 0 ? (char)buffer[0] : ' ';
                    var last100 = buffer.Count > 100
                        ? Encoding.UTF8.GetString(buffer.ToArray(), buffer.Count - 100, 100)
                        : "";
                    Console.WriteLine($"[READ LOOP #{readLoopCount}] {clientIP} - Waiting for complete JSON. Buffer={buffer.Count} bytes, firstChar='{firstChar}'");
                }

                foreach (var json in jsonMessages)
                {
                    var paket = _protocolService.Deserialize(json);
                    if (paket == null) continue;

                    // Proses paket berdasarkan tipe
                    var result = await _packetRouter.RoutePacketAsync(
                        paket, client, clientIP, clientType, hostCode, tamuConnectionId);

                    // Update client type dan identifier dari hasil routing
                    if (result.ClientType != TipeClient.UNKNOWN)
                    {
                        clientType = result.ClientType;
                    }
                    if (!string.IsNullOrEmpty(result.HostCode))
                    {
                        hostCode = result.HostCode;
                    }
                    if (!string.IsNullOrEmpty(result.TamuConnectionId))
                    {
                        tamuConnectionId = result.TamuConnectionId;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RELAY] Connection error ({clientIP}): {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"[RELAY] Connection ending ({clientIP}, {clientType}) - Total read loops: {readLoopCount}, Final buffer: {buffer.Count} bytes");

            // Cleanup berdasarkan tipe client
            if (clientType == TipeClient.HOST && !string.IsNullOrEmpty(hostCode))
            {
                _connectionManager.UnregisterHost(hostCode);
            }
            else if (clientType == TipeClient.TAMU && !string.IsNullOrEmpty(tamuConnectionId))
            {
                _connectionManager.UnregisterTamu(tamuConnectionId);
            }

            try { client.Close(); } catch { }
            Console.WriteLine($"[RELAY] Connection closed ({clientIP})");
        }
    }

    /// <summary>
    /// Loop untuk cleanup koneksi mati/expired.
    /// </summary>
    private async Task CleanupLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(30), ct);
                _connectionManager.Cleanup();

                var stats = _connectionManager.GetStats();
                Console.WriteLine($"[RELAY] Stats: {stats.hosts} hosts, {stats.tamus} tamus, {stats.sessions} sessions");
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RELAY] Cleanup error: {ex.Message}");
            }
        }
    }
}
