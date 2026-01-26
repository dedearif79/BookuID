namespace BookuRemoteRelay.Models;

/// <summary>
/// Menyimpan state sesi relay antara Host dan Tamu.
/// </summary>
public class RelaySession
{
    /// <summary>
    /// ID unik sesi (sama dengan KunciSesi dari RESPON_KONEKSI).
    /// </summary>
    public string SessionId { get; set; } = "";

    /// <summary>
    /// UDP Session ID (integer hash dari SessionId untuk routing UDP packets).
    /// Dihitung dari SessionId.GetHashCode() & int.MaxValue.
    /// </summary>
    public int UdpSessionId { get; set; }

    /// <summary>
    /// HostCode dari Host dalam sesi ini.
    /// </summary>
    public string HostCode { get; set; } = "";

    /// <summary>
    /// ConnectionId dari Tamu dalam sesi ini.
    /// </summary>
    public string TamuConnectionId { get; set; } = "";

    /// <summary>
    /// Referensi ke HostConnection.
    /// </summary>
    public HostConnection? Host { get; set; }

    /// <summary>
    /// Referensi ke TamuConnection.
    /// </summary>
    public TamuConnection? Tamu { get; set; }

    /// <summary>
    /// Waktu sesi dimulai.
    /// </summary>
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Waktu aktivitas terakhir.
    /// </summary>
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Status sesi.
    /// </summary>
    public SessionStatus Status { get; set; } = SessionStatus.PENDING;

    /// <summary>
    /// Izin kontrol keyboard/mouse.
    /// </summary>
    public bool IzinKontrol { get; set; }

    /// <summary>
    /// Total bytes yang di-relay dari Host ke Tamu.
    /// </summary>
    public long BytesRelayedToTamu { get; set; }

    /// <summary>
    /// Total bytes yang di-relay dari Tamu ke Host.
    /// </summary>
    public long BytesRelayedToHost { get; set; }

    /// <summary>
    /// Cek apakah sesi masih aktif (status ACTIVE dan koneksi valid).
    /// </summary>
    public bool IsActive => Status == SessionStatus.ACTIVE
                            && Host?.IsValid == true
                            && Tamu?.IsValid == true;

    /// <summary>
    /// Cek apakah sesi masih valid untuk cleanup (PENDING atau ACTIVE dengan koneksi valid).
    /// Session PENDING diberi waktu 60 detik untuk menunggu response dari Host.
    /// </summary>
    public bool IsValidForCleanup
    {
        get
        {
            // Session ENDED atau DISCONNECTED harus dihapus
            if (Status == SessionStatus.ENDED || Status == SessionStatus.DISCONNECTED)
                return false;

            // Koneksi harus valid
            if (Host?.IsValid != true || Tamu?.IsValid != true)
                return false;

            // Session PENDING punya timeout 60 detik
            if (Status == SessionStatus.PENDING)
            {
                var pendingDuration = DateTime.UtcNow - StartedAt;
                if (pendingDuration.TotalSeconds > 60)
                    return false; // Timeout, boleh dihapus
            }

            return true;
        }
    }

    /// <summary>
    /// Update waktu aktivitas terakhir.
    /// </summary>
    public void UpdateActivity()
    {
        LastActivity = DateTime.UtcNow;
    }

    /// <summary>
    /// Durasi sesi.
    /// </summary>
    public TimeSpan Duration => DateTime.UtcNow - StartedAt;
}

/// <summary>
/// Status sesi relay.
/// </summary>
public enum SessionStatus
{
    /// <summary>
    /// Menunggu persetujuan dari Host.
    /// </summary>
    PENDING = 0,

    /// <summary>
    /// Sesi aktif, streaming berlangsung.
    /// </summary>
    ACTIVE = 1,

    /// <summary>
    /// Sesi berakhir secara normal.
    /// </summary>
    ENDED = 2,

    /// <summary>
    /// Sesi terputus karena error.
    /// </summary>
    DISCONNECTED = 3
}
