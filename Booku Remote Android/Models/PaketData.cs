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
