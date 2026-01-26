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
    /// Codec video yang dipilih oleh Host ("JPEG" atau "H264")
    /// </summary>
    public string SelectedCodec { get; private set; } = "JPEG";

    /// <summary>
    /// Waktu koneksi dimulai
    /// </summary>
    public DateTime? WaktuKoneksi { get; private set; }

    /// <summary>
    /// Event ketika status berubah
    /// </summary>
    public event EventHandler<StatusKoneksi>? StatusChanged;

    /// <summary>
    /// Memulai sesi baru (mode LAN)
    /// </summary>
    public void MulaiSesi(PerangkatLAN host, ResponKoneksiData respon)
    {
        HostTerhubung = host;
        KunciSesi = respon.KunciSesi;
        IzinKontrol = respon.IzinKontrol;
        IzinTransferBerkas = respon.IzinTransferBerkas;
        IzinClipboard = respon.IzinClipboard;
        SelectedCodec = respon.SelectedCodec ?? "JPEG";
        WaktuKoneksi = DateTime.UtcNow;

        System.Diagnostics.Debug.WriteLine($"[SESSION] MulaiSesi: SelectedCodec={SelectedCodec}");
        SetStatus(StatusKoneksi.TERHUBUNG);
    }

    /// <summary>
    /// Memulai sesi baru (mode Relay/Internet)
    /// </summary>
    /// <param name="hostCode">HostCode untuk identifikasi</param>
    /// <param name="namaHost">Nama Host</param>
    /// <param name="respon">Response data dari Host</param>
    /// <param name="relaySessionId">Relay Session ID (IdSesi dari paket) - digunakan untuk routing di Relay</param>
    public void MulaiSesiRelay(string hostCode, string namaHost, ResponKoneksiData respon, string? relaySessionId = null)
    {
        // Buat PerangkatLAN virtual untuk mode relay
        HostTerhubung = new PerangkatLAN
        {
            NamaPerangkat = namaHost,
            AlamatIP = "Relay:" + hostCode,
            Status = StatusPerangkat.TERSEDIA
        };

        // PENTING: Untuk mode Relay, gunakan relaySessionId (dari paket.IdSesi) sebagai KunciSesi
        // karena Relay Server routing paket berdasarkan IdSesi, bukan KunciSesi di payload
        KunciSesi = relaySessionId ?? respon.KunciSesi;

        IzinKontrol = respon.IzinKontrol;
        IzinTransferBerkas = respon.IzinTransferBerkas;
        IzinClipboard = respon.IzinClipboard;
        SelectedCodec = respon.SelectedCodec ?? "JPEG";
        WaktuKoneksi = DateTime.UtcNow;

        System.Diagnostics.Debug.WriteLine($"[SESSION] MulaiSesiRelay: SelectedCodec={SelectedCodec}");
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
        SelectedCodec = "JPEG";
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
