using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BookuRemoteRelay.Models;

/// <summary>
/// Struktur paket data untuk komunikasi.
/// Harus sinkron dengan cls_PaketData di Booku Remote WPF.
/// </summary>
public class PaketData
{
    [JsonPropertyName("tipePaket")]
    public int TipePaket { get; set; }

    [JsonPropertyName("idSesi")]
    public string IdSesi { get; set; } = "";

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("payload")]
    public string Payload { get; set; } = "";

    [JsonPropertyName("checksum")]
    public string Checksum { get; set; } = "";

    /// <summary>
    /// Mendapatkan TipePaket sebagai enum.
    /// </summary>
    [JsonIgnore]
    public TipePaket TipePaketEnum => (TipePaket)TipePaket;

    /// <summary>
    /// Buat paket baru dengan timestamp dan checksum otomatis.
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
    /// Menghitung checksum MD5.
    /// </summary>
    public string HitungChecksum()
    {
        var data = $"{TipePaket}|{IdSesi}|{Timestamp}|{Payload}";
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Validasi checksum.
    /// </summary>
    public bool ValidasiChecksum()
    {
        return Checksum == HitungChecksum();
    }
}
