using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using BookuRemoteRelay.Models;
using BookuRemoteRelay.Utils;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Mengelola semua koneksi Host dan Tamu.
/// </summary>
public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, HostConnection> _hosts = new();
    private readonly ConcurrentDictionary<string, TamuConnection> _tamus = new();
    private readonly ConcurrentDictionary<string, RelaySession> _sessions = new();

    private readonly object _lockHosts = new();
    private readonly object _lockTamus = new();
    private readonly object _lockSessions = new();

    /// <summary>
    /// Registrasi Host baru dan generate HostCode.
    /// </summary>
    public HostConnection RegisterHost(TcpClient client, string namaPerangkat, string password)
    {
        string hostCode;

        // Generate unique HostCode
        lock (_lockHosts)
        {
            do
            {
                hostCode = HostCodeGenerator.Generate();
            } while (_hosts.ContainsKey(hostCode));
        }

        var host = new HostConnection
        {
            HostCode = hostCode,
            NamaPerangkat = namaPerangkat,
            Password = password,
            TcpClient = client,
            Stream = client.GetStream(),
            RegisteredAt = DateTime.UtcNow,
            LastHeartbeat = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            Status = StatusKoneksi.TERHUBUNG
        };

        _hosts[hostCode] = host;

        Console.WriteLine($"[HOST] Registered: {namaPerangkat} => {hostCode}");
        return host;
    }

    /// <summary>
    /// Unregister Host.
    /// </summary>
    public void UnregisterHost(string hostCode)
    {
        if (_hosts.TryRemove(hostCode, out var host))
        {
            // End any active session
            if (!string.IsNullOrEmpty(host.ActiveSessionId))
            {
                EndSession(host.ActiveSessionId);
            }

            host.Dispose();
            Console.WriteLine($"[HOST] Unregistered: {hostCode}");
        }
    }

    /// <summary>
    /// Cari Host berdasarkan HostCode.
    /// </summary>
    public HostConnection? GetHost(string hostCode)
    {
        var normalized = HostCodeGenerator.Normalize(hostCode);
        _hosts.TryGetValue(normalized, out var host);
        return host;
    }

    /// <summary>
    /// Cek apakah Host tersedia untuk koneksi.
    /// </summary>
    public bool IsHostAvailable(string hostCode)
    {
        var host = GetHost(hostCode);
        return host?.IsAvailable == true;
    }

    /// <summary>
    /// Registrasi Tamu baru.
    /// </summary>
    public TamuConnection RegisterTamu(TcpClient client, string alamatIP)
    {
        var tamu = new TamuConnection
        {
            TcpClient = client,
            Stream = client.GetStream(),
            AlamatIP = alamatIP,
            ConnectedAt = DateTime.UtcNow,
            Status = StatusKoneksi.TERHUBUNG
        };

        _tamus[tamu.ConnectionId] = tamu;

        Console.WriteLine($"[TAMU] Connected: {tamu.ConnectionId} from {alamatIP}");
        return tamu;
    }

    /// <summary>
    /// Unregister Tamu.
    /// </summary>
    public void UnregisterTamu(string connectionId)
    {
        if (_tamus.TryRemove(connectionId, out var tamu))
        {
            // End any active session
            if (!string.IsNullOrEmpty(tamu.ActiveSessionId))
            {
                EndSession(tamu.ActiveSessionId);
            }

            tamu.Dispose();
            Console.WriteLine($"[TAMU] Disconnected: {connectionId}");
        }
    }

    /// <summary>
    /// Buat sesi relay antara Host dan Tamu.
    /// </summary>
    public RelaySession CreateSession(HostConnection host, TamuConnection tamu, string sessionId)
    {
        // Generate UdpSessionId menggunakan djb2 hash (konsisten dengan Host VB.NET dan Android)
        // PENTING: GetHashCode() tidak konsisten antar platform, jadi gunakan djb2
        int udpSessionId = GenerateUdpSessionId(sessionId);

        var session = new RelaySession
        {
            SessionId = sessionId,
            UdpSessionId = udpSessionId,
            HostCode = host.HostCode,
            TamuConnectionId = tamu.ConnectionId,
            Host = host,
            Tamu = tamu,
            StartedAt = DateTime.UtcNow,
            Status = SessionStatus.PENDING
        };

        _sessions[sessionId] = session;

        // Update references including UDP session ID
        host.ActiveSessionId = sessionId;
        host.UdpSessionId = udpSessionId;
        tamu.ActiveSessionId = sessionId;
        tamu.UdpSessionId = udpSessionId;

        Console.WriteLine($"[SESSION] Created: {sessionId} (UdpSessionId={udpSessionId}) ({host.NamaPerangkat} <-> {tamu.NamaPerangkat})");
        return session;
    }

    /// <summary>
    /// Aktifkan sesi (setelah Host menerima).
    /// </summary>
    public void ActivateSession(string sessionId, bool izinKontrol)
    {
        if (_sessions.TryGetValue(sessionId, out var session))
        {
            session.Status = SessionStatus.ACTIVE;
            session.IzinKontrol = izinKontrol;
            Console.WriteLine($"[SESSION] Activated: {sessionId}");
        }
    }

    /// <summary>
    /// Akhiri sesi.
    /// </summary>
    public void EndSession(string sessionId)
    {
        if (_sessions.TryRemove(sessionId, out var session))
        {
            session.Status = SessionStatus.ENDED;

            // Clear session references
            if (session.Host != null)
            {
                session.Host.ActiveSessionId = null;
            }
            if (session.Tamu != null)
            {
                session.Tamu.ActiveSessionId = null;
            }

            Console.WriteLine($"[SESSION] Ended: {sessionId} (Duration: {session.Duration.TotalSeconds:F1}s, " +
                            $"Relayed: {(session.BytesRelayedToTamu + session.BytesRelayedToHost) / 1024}KB)");
        }
    }

    /// <summary>
    /// Dapatkan sesi berdasarkan ID.
    /// </summary>
    public RelaySession? GetSession(string sessionId)
    {
        _sessions.TryGetValue(sessionId, out var session);
        return session;
    }

    /// <summary>
    /// Dapatkan sesi berdasarkan HostCode.
    /// </summary>
    public RelaySession? GetSessionByHostCode(string hostCode)
    {
        return _sessions.Values.FirstOrDefault(s => s.HostCode == hostCode && s.IsActive);
    }

    /// <summary>
    /// Dapatkan sesi berdasarkan UdpSessionId (untuk routing UDP packets).
    /// </summary>
    public RelaySession? GetSessionByUdpSessionId(int udpSessionId)
    {
        return _sessions.Values.FirstOrDefault(s => s.UdpSessionId == udpSessionId && s.IsActive);
    }

    /// <summary>
    /// Update UDP endpoint untuk Host (dipanggil saat menerima paket UDP pertama dari Host).
    /// </summary>
    public void UpdateHostUdpEndpoint(int udpSessionId, IPEndPoint endpoint)
    {
        var session = GetSessionByUdpSessionId(udpSessionId);
        if (session?.Host != null)
        {
            session.Host.UdpEndPoint = endpoint;
            Console.WriteLine($"[UDP] Host endpoint updated: {endpoint} for UdpSessionId={udpSessionId}");
        }
    }

    /// <summary>
    /// Update UDP endpoint untuk Tamu (dipanggil saat menerima paket UDP pertama dari Tamu).
    /// </summary>
    public void UpdateTamuUdpEndpoint(int udpSessionId, IPEndPoint endpoint)
    {
        var session = GetSessionByUdpSessionId(udpSessionId);
        if (session?.Tamu != null)
        {
            session.Tamu.UdpEndPoint = endpoint;
            Console.WriteLine($"[UDP] Tamu endpoint updated: {endpoint} for UdpSessionId={udpSessionId}");
        }
    }

    /// <summary>
    /// Update heartbeat Host.
    /// </summary>
    public void UpdateHostHeartbeat(string hostCode)
    {
        var host = GetHost(hostCode);
        host?.UpdateHeartbeat();
    }

    /// <summary>
    /// Cleanup koneksi dan sesi yang expired/mati.
    /// </summary>
    public void Cleanup()
    {
        var now = DateTime.UtcNow;

        // Cleanup expired hosts
        var expiredHosts = _hosts.Values
            .Where(h => !h.IsValid || now > h.ExpiresAt)
            .Select(h => h.HostCode)
            .ToList();

        foreach (var hostCode in expiredHosts)
        {
            UnregisterHost(hostCode);
        }

        // Cleanup disconnected tamus
        var disconnectedTamus = _tamus.Values
            .Where(t => !t.IsValid)
            .Select(t => t.ConnectionId)
            .ToList();

        foreach (var tamuId in disconnectedTamus)
        {
            UnregisterTamu(tamuId);
        }

        // Cleanup dead sessions (gunakan IsValidForCleanup untuk menghormati PENDING timeout)
        var deadSessions = _sessions.Values
            .Where(s => !s.IsValidForCleanup)
            .Select(s => s.SessionId)
            .ToList();

        foreach (var sessionId in deadSessions)
        {
            EndSession(sessionId);
        }
    }

    /// <summary>
    /// Statistik koneksi.
    /// </summary>
    public (int hosts, int tamus, int sessions) GetStats()
    {
        return (_hosts.Count, _tamus.Count, _sessions.Count);
    }

    /// <summary>
    /// Generate UDP Session ID menggunakan djb2 hash (deterministic, cross-platform).
    /// PENTING: GetHashCode() tidak konsisten antar platform (.NET Windows vs Android).
    /// djb2 menghasilkan hash yang sama di semua platform.
    /// </summary>
    private static int GenerateUdpSessionId(string? sessionKey)
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
}
