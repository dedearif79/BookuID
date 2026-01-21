namespace BookuRemoteAndroid.Models;

/// <summary>
/// Tipe paket untuk protokol Booku Remote
/// </summary>
public enum TipePaket
{
    // Discovery (1-9)
    BROADCAST_DISCOVERY = 1,
    RESPON_DISCOVERY = 2,

    // Connection (10-19)
    PERMINTAAN_KONEKSI = 10,
    RESPON_KONEKSI = 11,
    TUTUP_KONEKSI = 12,
    HEARTBEAT = 13,

    // Remote Desktop (20-29)
    FRAME_LAYAR = 20,
    INPUT_KEYBOARD = 21,
    INPUT_MOUSE = 22,
    CLIPBOARD_DATA = 23,
    PERMINTAAN_STREAMING = 24,
    HENTIKAN_STREAMING = 25,

    // File Transfer (30-39) - Future
    PERMINTAAN_BERKAS = 30,
    DATA_BERKAS = 31,
    KONFIRMASI_BERKAS = 32,
    DAFTAR_FOLDER = 33
}

/// <summary>
/// Status perangkat Host
/// </summary>
public enum StatusPerangkat
{
    TERSEDIA = 1,
    SIBUK = 2,
    TIDAK_TERSEDIA = 3
}

/// <summary>
/// Status koneksi
/// </summary>
public enum StatusKoneksi
{
    TIDAK_TERHUBUNG = 0,
    MENUNGGU_PERSETUJUAN = 1,
    TERHUBUNG = 2,
    DITOLAK = 3,
    TERPUTUS = 4
}

/// <summary>
/// Hasil persetujuan koneksi
/// </summary>
public enum HasilPersetujuan
{
    MENUNGGU = 0,
    DITERIMA = 1,
    DITOLAK = 2,
    TIMEOUT = 3
}

/// <summary>
/// Tipe aksi mouse
/// </summary>
public enum TipeAksiMouse
{
    PINDAH = 1,     // Mouse move
    KLIK = 2,       // Mouse click
    RODA = 3        // Mouse wheel
}

/// <summary>
/// Tombol mouse
/// </summary>
public enum TombolMouse
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    MIDDLE = 3,
    XBUTTON1 = 4,
    XBUTTON2 = 5
}
