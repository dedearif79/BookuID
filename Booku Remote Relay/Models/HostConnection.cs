using System.Net;
using System.Net.Sockets;

namespace BookuRemoteRelay.Models;

/// <summary>
/// Menyimpan state koneksi Host yang terdaftar di relay.
/// </summary>
public class HostConnection
{
    /// <summary>
    /// Kode unik 6 karakter untuk identifikasi Host.
    /// </summary>
    public string HostCode { get; set; } = "";

    /// <summary>
    /// Nama perangkat Host.
    /// </summary>
    public string NamaPerangkat { get; set; } = "";

    /// <summary>
    /// Password untuk koneksi (opsional).
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    /// Apakah Host memerlukan password.
    /// </summary>
    public bool RequiresPassword => !string.IsNullOrEmpty(Password);

    /// <summary>
    /// TCP client untuk koneksi ke Host.
    /// </summary>
    public TcpClient? TcpClient { get; set; }

    /// <summary>
    /// Network stream untuk komunikasi.
    /// </summary>
    public NetworkStream? Stream { get; set; }

    /// <summary>
    /// Buffer untuk menerima data (untuk bracket tracking).
    /// </summary>
    public List<byte> ReceiveBuffer { get; set; } = new();

    /// <summary>
    /// Waktu registrasi.
    /// </summary>
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Waktu heartbeat terakhir.
    /// </summary>
    public DateTime LastHeartbeat { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Waktu kadaluarsa (60 menit sejak registrasi).
    /// </summary>
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(60);

    /// <summary>
    /// Status koneksi.
    /// </summary>
    public StatusKoneksi Status { get; set; } = StatusKoneksi.TERHUBUNG;

    /// <summary>
    /// ID sesi aktif (jika sedang dalam sesi dengan Tamu).
    /// </summary>
    public string? ActiveSessionId { get; set; }

    /// <summary>
    /// UDP endpoint untuk video streaming (IP:Port dari paket UDP pertama).
    /// </summary>
    public IPEndPoint? UdpEndPoint { get; set; }

    /// <summary>
    /// UDP Session ID (integer hash dari SessionId untuk routing UDP).
    /// </summary>
    public int UdpSessionId { get; set; }

    /// <summary>
    /// Cek apakah koneksi masih valid (belum expired dan masih terhubung).
    /// </summary>
    public bool IsValid => Status == StatusKoneksi.TERHUBUNG
                           && DateTime.UtcNow < ExpiresAt
                           && TcpClient?.Connected == true;

    /// <summary>
    /// Cek apakah tersedia untuk koneksi baru (tidak sedang dalam sesi).
    /// </summary>
    public bool IsAvailable => IsValid && string.IsNullOrEmpty(ActiveSessionId);

    /// <summary>
    /// Update waktu heartbeat.
    /// </summary>
    public void UpdateHeartbeat()
    {
        LastHeartbeat = DateTime.UtcNow;
    }

    /// <summary>
    /// Cleanup resources.
    /// </summary>
    public void Dispose()
    {
        Status = StatusKoneksi.TERPUTUS;
        Stream?.Dispose();
        TcpClient?.Dispose();
        ReceiveBuffer.Clear();
    }
}

/// <summary>
/// Menyimpan state koneksi Tamu yang terhubung ke relay.
/// </summary>
public class TamuConnection
{
    /// <summary>
    /// ID unik untuk koneksi Tamu.
    /// </summary>
    public string ConnectionId { get; set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Nama perangkat Tamu.
    /// </summary>
    public string NamaPerangkat { get; set; } = "";

    /// <summary>
    /// Alamat IP Tamu.
    /// </summary>
    public string AlamatIP { get; set; } = "";

    /// <summary>
    /// TCP client untuk koneksi ke Tamu.
    /// </summary>
    public TcpClient? TcpClient { get; set; }

    /// <summary>
    /// Network stream untuk komunikasi.
    /// </summary>
    public NetworkStream? Stream { get; set; }

    /// <summary>
    /// Buffer untuk menerima data (untuk bracket tracking).
    /// </summary>
    public List<byte> ReceiveBuffer { get; set; } = new();

    /// <summary>
    /// Waktu koneksi.
    /// </summary>
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Status koneksi.
    /// </summary>
    public StatusKoneksi Status { get; set; } = StatusKoneksi.TERHUBUNG;

    /// <summary>
    /// HostCode yang sedang dituju (jika sudah pairing).
    /// </summary>
    public string? TargetHostCode { get; set; }

    /// <summary>
    /// ID sesi aktif (jika sedang dalam sesi dengan Host).
    /// </summary>
    public string? ActiveSessionId { get; set; }

    /// <summary>
    /// UDP endpoint untuk video streaming (IP:Port dari paket UDP pertama).
    /// </summary>
    public IPEndPoint? UdpEndPoint { get; set; }

    /// <summary>
    /// UDP Session ID (integer hash dari SessionId untuk routing UDP).
    /// </summary>
    public int UdpSessionId { get; set; }

    /// <summary>
    /// Cek apakah koneksi masih valid.
    /// </summary>
    public bool IsValid => Status == StatusKoneksi.TERHUBUNG && TcpClient?.Connected == true;

    /// <summary>
    /// Cleanup resources.
    /// </summary>
    public void Dispose()
    {
        Status = StatusKoneksi.TERPUTUS;
        Stream?.Dispose();
        TcpClient?.Dispose();
        ReceiveBuffer.Clear();
    }
}
