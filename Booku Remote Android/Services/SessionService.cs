using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk mengelola state sesi remote
/// </summary>
public class SessionService
{
    /// <summary>
    /// Status koneksi saat ini
    /// </summary>
    public StatusKoneksi Status { get; private set; } = StatusKoneksi.TIDAK_TERHUBUNG;

    /// <summary>
    /// Kunci sesi aktif
    /// </summary>
    public string? KunciSesi { get; private set; }

    /// <summary>
    /// Perangkat Host yang terhubung
    /// </summary>
    public PerangkatLAN? HostTerhubung { get; private set; }

    /// <summary>
    /// Izin kontrol keyboard/mouse
    /// </summary>
    public bool IzinKontrol { get; private set; }

    /// <summary>
    /// Izin transfer berkas
    /// </summary>
    public bool IzinTransferBerkas { get; private set; }

    /// <summary>
    /// Izin clipboard sync
    /// </summary>
    public bool IzinClipboard { get; private set; }

    /// <summary>
    /// Waktu koneksi dimulai
    /// </summary>
    public DateTime? WaktuKoneksi { get; private set; }

    /// <summary>
    /// Event ketika status berubah
    /// </summary>
    public event EventHandler<StatusKoneksi>? StatusChanged;

    /// <summary>
    /// Memulai sesi baru
    /// </summary>
    public void MulaiSesi(PerangkatLAN host, ResponKoneksiData respon)
    {
        HostTerhubung = host;
        KunciSesi = respon.KunciSesi;
        IzinKontrol = respon.IzinKontrol;
        IzinTransferBerkas = respon.IzinTransferBerkas;
        IzinClipboard = respon.IzinClipboard;
        WaktuKoneksi = DateTime.UtcNow;

        SetStatus(StatusKoneksi.TERHUBUNG);
    }

    /// <summary>
    /// Mengakhiri sesi
    /// </summary>
    public void AkhiriSesi()
    {
        KunciSesi = null;
        HostTerhubung = null;
        IzinKontrol = false;
        IzinTransferBerkas = false;
        IzinClipboard = false;
        WaktuKoneksi = null;

        SetStatus(StatusKoneksi.TIDAK_TERHUBUNG);
    }

    /// <summary>
    /// Set status koneksi
    /// </summary>
    public void SetStatus(StatusKoneksi status)
    {
        if (Status != status)
        {
            Status = status;
            StatusChanged?.Invoke(this, status);
        }
    }

    /// <summary>
    /// Cek apakah sedang terhubung
    /// </summary>
    public bool IsTerhubung => Status == StatusKoneksi.TERHUBUNG && !string.IsNullOrEmpty(KunciSesi);

    /// <summary>
    /// Durasi koneksi
    /// </summary>
    public TimeSpan? DurasiKoneksi => WaktuKoneksi.HasValue
        ? DateTime.UtcNow - WaktuKoneksi.Value
        : null;
}
