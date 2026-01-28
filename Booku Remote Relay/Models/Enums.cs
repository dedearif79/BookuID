namespace BookuRemoteRelay.Models;

/// <summary>
/// Tipe paket untuk komunikasi relay.
/// Harus sinkron dengan TipePaket di Booku Remote WPF.
/// </summary>
public enum TipePaket
{
    // Discovery (1-9) - LAN only, tidak digunakan di relay
    BROADCAST_DISCOVERY = 1,
    RESPON_DISCOVERY = 2,

    // Koneksi (10-19)
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

    // Transfer Berkas (30-39)
    PERMINTAAN_BERKAS = 30,
    DATA_BERKAS = 31,
    KONFIRMASI_BERKAS = 32,
    DAFTAR_FOLDER = 33,
    RESPON_TRANSFER = 34,
    KONFIRMASI_CHUNK = 35,
    BATAL_TRANSFER = 36,
    RESPON_DAFTAR_FOLDER = 37,

    // === RELAY / INTERNET (40-59) ===

    // Registrasi Host (40-44)
    RELAY_REGISTER_HOST = 40,
    RELAY_REGISTER_HOST_OK = 41,
    RELAY_UNREGISTER_HOST = 42,
    RELAY_HOST_HEARTBEAT = 43,

    // Tamu ke Relay (45-49)
    RELAY_QUERY_HOST = 45,
    RELAY_QUERY_HOST_RESULT = 46,
    RELAY_CONNECT_REQUEST = 47,

    // Session (50-54)
    RELAY_FORWARD_TO_HOST = 50,
    RELAY_FORWARD_TO_TAMU = 51,
    RELAY_SESSION_STARTED = 52,
    RELAY_SESSION_ENDED = 53,

    // Error (55-59)
    RELAY_ERROR = 55,
    RELAY_HOST_OFFLINE = 56,
    RELAY_INVALID_CODE = 57
}

/// <summary>
/// Status koneksi client ke relay.
/// </summary>
public enum StatusKoneksi
{
    TIDAK_TERHUBUNG = 0,
    TERHUBUNG = 1,
    MENUNGGU = 2,
    TERPUTUS = 3
}

/// <summary>
/// Tipe client yang terhubung ke relay.
/// </summary>
public enum TipeClient
{
    UNKNOWN = 0,
    HOST = 1,
    TAMU = 2
}

/// <summary>
/// Hasil persetujuan koneksi.
/// </summary>
public enum HasilPersetujuan
{
    MENUNGGU = 0,
    DITERIMA = 1,
    DITOLAK = 2
}
