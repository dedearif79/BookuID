using System.Net.Sockets;
using System.Text;
using BookuRemoteRelay.Models;
using BookuRemoteRelay.Utils;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Hasil routing paket.
/// </summary>
public class RouteResult
{
    public TipeClient ClientType { get; set; } = TipeClient.UNKNOWN;
    public string? HostCode { get; set; }
    public string? TamuConnectionId { get; set; }
}

/// <summary>
/// Router paket - mengarahkan paket ke handler yang tepat.
/// </summary>
public class PacketRouter
{
    private readonly ConnectionManager _connectionManager;
    private readonly ProtocolService _protocolService;

    public PacketRouter(ConnectionManager connectionManager, ProtocolService protocolService)
    {
        _connectionManager = connectionManager;
        _protocolService = protocolService;
    }

    /// <summary>
    /// Route paket ke handler yang sesuai.
    /// </summary>
    public async Task<RouteResult> RoutePacketAsync(
        PaketData paket,
        TcpClient client,
        string clientIP,
        TipeClient currentType,
        string? currentHostCode,
        string? currentTamuId)
    {
        var result = new RouteResult
        {
            ClientType = currentType,
            HostCode = currentHostCode,
            TamuConnectionId = currentTamuId
        };

        try
        {
            switch (paket.TipePaketEnum)
            {
                // ========== HOST PACKETS ==========

                case TipePaket.RELAY_REGISTER_HOST:
                    await HandleRegisterHostAsync(paket, client, clientIP, result);
                    break;

                case TipePaket.RELAY_UNREGISTER_HOST:
                    HandleUnregisterHost(result);
                    break;

                case TipePaket.RELAY_HOST_HEARTBEAT:
                    HandleHostHeartbeat(result);
                    break;

                case TipePaket.RESPON_KONEKSI:
                    await HandleHostResponseAsync(paket, result);
                    break;

                // ========== TAMU PACKETS ==========

                case TipePaket.RELAY_QUERY_HOST:
                    await HandleQueryHostAsync(paket, client, clientIP, result);
                    break;

                case TipePaket.RELAY_CONNECT_REQUEST:
                    await HandleConnectRequestAsync(paket, client, clientIP, result);
                    break;

                case TipePaket.TUTUP_KONEKSI:
                    HandleCloseConnection(paket, result);
                    break;

                // ========== DATA RELAY (Transparent) ==========

                case TipePaket.FRAME_LAYAR:
                case TipePaket.INPUT_KEYBOARD:
                case TipePaket.INPUT_MOUSE:
                case TipePaket.CLIPBOARD_DATA:
                case TipePaket.PERMINTAAN_STREAMING:
                case TipePaket.HENTIKAN_STREAMING:
                case TipePaket.HEARTBEAT:
                    await RelayPacketAsync(paket, result);
                    break;

                default:
                    Console.WriteLine($"[ROUTER] Unknown packet type: {paket.TipePaket}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ROUTER] Error handling {paket.TipePaketEnum}: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    /// Handle RELAY_REGISTER_HOST - Host mendaftar ke relay.
    /// </summary>
    private async Task HandleRegisterHostAsync(PaketData paket, TcpClient client, string clientIP, RouteResult result)
    {
        var payload = _protocolService.DeserializePayload<PayloadRegisterHost>(paket.Payload);
        if (payload == null) return;

        // Register host
        var host = _connectionManager.RegisterHost(client, payload.NamaPerangkat, payload.Password ?? "");

        // Update result
        result.ClientType = TipeClient.HOST;
        result.HostCode = host.HostCode;

        // Kirim response dengan HostCode
        var responsePacket = _protocolService.CreateRegisterHostOKPacket(host.HostCode, 60);
        await SendPacketAsync(client, responsePacket);

        Console.WriteLine($"[ROUTER] Host registered: {payload.NamaPerangkat} => {host.HostCode}");
    }

    /// <summary>
    /// Handle RELAY_UNREGISTER_HOST - Host logout dari relay.
    /// </summary>
    private void HandleUnregisterHost(RouteResult result)
    {
        if (!string.IsNullOrEmpty(result.HostCode))
        {
            _connectionManager.UnregisterHost(result.HostCode);
            result.HostCode = null;
            Console.WriteLine($"[ROUTER] Host unregistered");
        }
    }

    /// <summary>
    /// Handle RELAY_HOST_HEARTBEAT - Keep-alive dari Host.
    /// </summary>
    private void HandleHostHeartbeat(RouteResult result)
    {
        if (!string.IsNullOrEmpty(result.HostCode))
        {
            _connectionManager.UpdateHostHeartbeat(result.HostCode);
        }
    }

    /// <summary>
    /// Handle RESPON_KONEKSI dari Host - Host menerima/menolak koneksi Tamu.
    /// </summary>
    private async Task HandleHostResponseAsync(PaketData paket, RouteResult result)
    {
        var payload = _protocolService.DeserializePayload<PayloadResponKoneksi>(paket.Payload);
        if (payload == null) return;

        // Cari session
        var session = _connectionManager.GetSession(paket.IdSesi);
        if (session == null)
        {
            Console.WriteLine($"[ROUTER] Session not found: {paket.IdSesi}");
            return;
        }

        if (payload.Diterima)
        {
            // Aktifkan session
            _connectionManager.ActivateSession(paket.IdSesi, payload.IzinKontrol);
            Console.WriteLine($"[ROUTER] Session activated: {paket.IdSesi}");
        }
        else
        {
            // End session
            _connectionManager.EndSession(paket.IdSesi);
            Console.WriteLine($"[ROUTER] Session rejected: {paket.IdSesi}");
        }

        // Forward response ke Tamu
        if (session.Tamu?.TcpClient != null && session.Tamu.IsValid)
        {
            await SendPacketAsync(session.Tamu.TcpClient, paket);
        }
    }

    /// <summary>
    /// Handle RELAY_QUERY_HOST - Tamu mencari Host berdasarkan HostCode.
    /// </summary>
    private async Task HandleQueryHostAsync(PaketData paket, TcpClient client, string clientIP, RouteResult result)
    {
        var payload = _protocolService.DeserializePayload<PayloadQueryHost>(paket.Payload);
        if (payload == null) return;

        // Register tamu jika belum
        if (result.ClientType != TipeClient.TAMU)
        {
            var tamu = _connectionManager.RegisterTamu(client, clientIP);
            result.ClientType = TipeClient.TAMU;
            result.TamuConnectionId = tamu.ConnectionId;
        }

        // Cari host
        var host = _connectionManager.GetHost(payload.HostCode);

        PaketData responsePacket;
        if (host == null)
        {
            // Host tidak ditemukan
            responsePacket = _protocolService.CreateInvalidCodePacket("HostCode tidak ditemukan");
            Console.WriteLine($"[ROUTER] Query host failed: {payload.HostCode} not found");
        }
        else if (!host.IsAvailable)
        {
            // Host tidak tersedia (sedang busy atau offline)
            responsePacket = _protocolService.CreateHostOfflinePacket("Host sedang tidak tersedia");
            Console.WriteLine($"[ROUTER] Query host failed: {payload.HostCode} not available");
        }
        else
        {
            // Host ditemukan
            responsePacket = _protocolService.CreateQueryHostResultPacket(
                true,
                host.NamaPerangkat,
                !string.IsNullOrEmpty(host.Password),
                "Host tersedia"
            );
            Console.WriteLine($"[ROUTER] Query host success: {payload.HostCode} => {host.NamaPerangkat}");
        }

        await SendPacketAsync(client, responsePacket);
    }

    /// <summary>
    /// Handle RELAY_CONNECT_REQUEST - Tamu minta koneksi ke Host.
    /// </summary>
    private async Task HandleConnectRequestAsync(PaketData paket, TcpClient client, string clientIP, RouteResult result)
    {
        var payload = _protocolService.DeserializePayload<PayloadRelayConnectRequest>(paket.Payload);
        if (payload == null) return;

        // Pastikan tamu sudah terdaftar
        TamuConnection? tamu = null;
        if (!string.IsNullOrEmpty(result.TamuConnectionId))
        {
            // Sudah ada - ini seharusnya tidak terjadi, tapi handle anyway
            // Tamu sebelumnya harus unregister dulu jika mau connect lagi
        }

        // Register tamu baru jika belum
        if (result.ClientType != TipeClient.TAMU)
        {
            tamu = _connectionManager.RegisterTamu(client, clientIP);
            tamu.NamaPerangkat = payload.NamaPerangkat;
            result.ClientType = TipeClient.TAMU;
            result.TamuConnectionId = tamu.ConnectionId;
        }
        else
        {
            // Update nama tamu dari payload
            // Need to get tamu from connection manager
        }

        // Cari host
        var host = _connectionManager.GetHost(payload.HostCode);
        if (host == null || !host.IsAvailable)
        {
            var errorPacket = _protocolService.CreateHostOfflinePacket("Host tidak tersedia");
            await SendPacketAsync(client, errorPacket);
            return;
        }

        // Validasi password jika ada
        if (!string.IsNullOrEmpty(host.Password) && host.Password != payload.Password)
        {
            var errorPacket = _protocolService.CreateErrorPacket("", 58, "Password salah");
            await SendPacketAsync(client, errorPacket);
            Console.WriteLine($"[ROUTER] Connect request failed: wrong password");
            return;
        }

        // Buat session
        var sessionId = Guid.NewGuid().ToString("N")[..16];

        // Dapatkan referensi tamu
        if (tamu == null && !string.IsNullOrEmpty(result.TamuConnectionId))
        {
            // Ini workaround - idealnya kita track tamu di ConnectionManager
            tamu = new TamuConnection
            {
                TcpClient = client,
                Stream = client.GetStream(),
                NamaPerangkat = payload.NamaPerangkat,
                AlamatIP = clientIP
            };
        }

        if (tamu == null)
        {
            Console.WriteLine($"[ROUTER] Connect request failed: tamu not registered");
            return;
        }

        var session = _connectionManager.CreateSession(host, tamu, sessionId);

        // Forward ke Host sebagai PERMINTAAN_KONEKSI
        var permintaanPayload = new PayloadPermintaanKoneksi
        {
            NamaPerangkat = payload.NamaPerangkat,
            AlamatIP = clientIP
        };
        var permintaanPacket = PaketData.Create(
            TipePaket.PERMINTAAN_KONEKSI,
            sessionId,
            _protocolService.SerializePayload(permintaanPayload)
        );

        if (host.TcpClient == null)
        {
            Console.WriteLine($"[ROUTER] Connect request failed: host has no connection");
            return;
        }

        await SendPacketAsync(host.TcpClient, permintaanPacket);

        Console.WriteLine($"[ROUTER] Connect request forwarded to host: {payload.HostCode}, session: {sessionId}");
    }

    /// <summary>
    /// Handle TUTUP_KONEKSI - Tutup session.
    /// </summary>
    private void HandleCloseConnection(PaketData paket, RouteResult result)
    {
        if (!string.IsNullOrEmpty(paket.IdSesi))
        {
            _connectionManager.EndSession(paket.IdSesi);
            Console.WriteLine($"[ROUTER] Session closed: {paket.IdSesi}");
        }
    }

    /// <summary>
    /// Relay paket ke peer yang terhubung dalam session.
    /// </summary>
    private async Task RelayPacketAsync(PaketData paket, RouteResult result)
    {
        if (string.IsNullOrEmpty(paket.IdSesi)) return;

        var session = _connectionManager.GetSession(paket.IdSesi);
        if (session == null || !session.IsActive) return;

        // Tentukan target berdasarkan pengirim
        TcpClient? targetClient = null;
        bool isFromHost = false;

        if (result.ClientType == TipeClient.HOST)
        {
            // Dari Host, relay ke Tamu
            targetClient = session.Tamu?.TcpClient;
            isFromHost = true;
            session.BytesRelayedToTamu += paket.Payload?.Length ?? 0;
        }
        else if (result.ClientType == TipeClient.TAMU)
        {
            // Dari Tamu, relay ke Host
            targetClient = session.Host?.TcpClient;
            session.BytesRelayedToHost += paket.Payload?.Length ?? 0;
        }

        if (targetClient == null || !targetClient.Connected) return;

        await SendPacketAsync(targetClient, paket);
        session.UpdateActivity();

        // Log untuk frame (skip karena terlalu banyak)
        if (paket.TipePaketEnum != TipePaket.FRAME_LAYAR && paket.TipePaketEnum != TipePaket.HEARTBEAT)
        {
            Console.WriteLine($"[RELAY] {paket.TipePaketEnum} relayed ({(isFromHost ? "Host->Tamu" : "Tamu->Host")})");
        }
    }

    /// <summary>
    /// Kirim paket ke client.
    /// </summary>
    private async Task SendPacketAsync(TcpClient client, PaketData paket)
    {
        try
        {
            if (!client.Connected) return;

            var bytes = _protocolService.Serialize(paket);
            var stream = client.GetStream();
            await stream.WriteAsync(bytes, 0, bytes.Length);
            await stream.FlushAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ROUTER] Send error: {ex.Message}");
        }
    }
}
