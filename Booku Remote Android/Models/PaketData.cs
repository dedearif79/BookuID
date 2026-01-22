using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BookuRemoteAndroid.Models;

/// <summary>
/// Struktur paket data untuk protokol Booku Remote
/// </summary>
public class PaketData
{
    [JsonPropertyName("tipePaket")]
    public int TipePaket { get; set; }

    [JsonPropertyName("idSesi")]
    public string IdSesi { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("payload")]
    public string Payload { get; set; } = string.Empty;

    [JsonPropertyName("checksum")]
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Membuat paket baru dengan checksum otomatis
    /// </summary>
    public static PaketData Create(TipePaket tipe, string idSesi, string payload)
    {
        var paket = new PaketData
        {
            TipePaket = (int)tipe,
            IdSesi = idSesi,
            Timestamp = DateTime.UtcNow.Ticks,
            Payload = payload
        };

        paket.Checksum = paket.HitungChecksum();
        return paket;
    }

    /// <summary>
    /// Menghitung MD5 checksum dari paket
    /// Formula: MD5("{tipePaket}|{idSesi}|{timestamp}|{payload}")
    /// </summary>
    public string HitungChecksum()
    {
        var data = $"{TipePaket}|{IdSesi}|{Timestamp}|{Payload}";
        var bytes = Encoding.UTF8.GetBytes(data);
        var hash = MD5.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// Memvalidasi checksum paket
    /// </summary>
    public bool ValidasiChecksum()
    {
        return Checksum == HitungChecksum();
    }
}

/// <summary>
/// Result wrapper untuk operasi koneksi
/// </summary>
public class KoneksiResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? KunciSesi { get; set; }
    public bool IzinKontrol { get; set; }

    public static KoneksiResult Sukses(string kunciSesi, bool izinKontrol) => new()
    {
        Success = true,
        KunciSesi = kunciSesi,
        IzinKontrol = izinKontrol
    };

    public static KoneksiResult Gagal(string message) => new()
    {
        Success = false,
        Message = message
    };
}

#region Relay Payload Classes

/// <summary>
/// Payload untuk RELAY_QUERY_HOST (Tamu → Relay)
/// </summary>
public class PayloadQueryHost
{
    [JsonPropertyName("hostCode")]
    public string HostCode { get; set; } = string.Empty;
}

/// <summary>
/// Payload untuk RELAY_QUERY_HOST_RESULT (Relay → Tamu)
/// </summary>
public class PayloadQueryHostResult
{
    [JsonPropertyName("found")]
    public bool Found { get; set; }

    [JsonPropertyName("namaHost")]
    public string NamaHost { get; set; } = string.Empty;

    [JsonPropertyName("requiresPassword")]
    public bool RequiresPassword { get; set; }

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = string.Empty;
}

/// <summary>
/// Payload untuk RELAY_CONNECT_REQUEST (Tamu → Relay)
/// </summary>
public class PayloadRelayConnectRequest
{
    [JsonPropertyName("hostCode")]
    public string HostCode { get; set; } = string.Empty;

    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = string.Empty;

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = string.Empty;

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Payload untuk RELAY_ERROR (Relay → Client)
/// </summary>
public class PayloadRelayError
{
    [JsonPropertyName("kodeError")]
    public int KodeError { get; set; }

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = string.Empty;
}

/// <summary>
/// Payload untuk RESPON_KONEKSI
/// </summary>
public class PayloadResponKoneksi
{
    [JsonPropertyName("hasil")]
    public int Hasil { get; set; }

    [JsonPropertyName("kunciSesi")]
    public string KunciSesi { get; set; } = string.Empty;

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = string.Empty;

    [JsonPropertyName("izinKontrol")]
    public bool IzinKontrol { get; set; }

    [JsonPropertyName("izinTransferBerkas")]
    public bool IzinTransferBerkas { get; set; }

    [JsonPropertyName("izinClipboard")]
    public bool IzinClipboard { get; set; }
}

/// <summary>
/// Payload untuk PERMINTAAN_KONEKSI
/// </summary>
public class PayloadPermintaanKoneksi
{
    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = string.Empty;

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = string.Empty;

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";
}

#endregion
