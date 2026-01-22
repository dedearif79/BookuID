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

        try
        {
            stream = client.GetStream();

            while (!ct.IsCancellationRequested && client.Connected)
            {
                // Baca data
                int bytesRead;
                try
                {
                    bytesRead = await stream.ReadAsync(readBuffer, 0, readBuffer.Length, ct);
                }
                catch (IOException)
                {
                    break; // Connection closed
                }

                if (bytesRead == 0) break; // Connection closed

                // Tambah ke buffer
                buffer.AddRange(readBuffer.Take(bytesRead));

                // Extract dan proses semua JSON lengkap
                var jsonMessages = JsonBracketParser.ExtractAllJson(buffer);

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
