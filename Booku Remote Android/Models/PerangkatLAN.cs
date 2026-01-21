using System.Text.Json.Serialization;

namespace BookuRemoteAndroid.Models;

/// <summary>
/// Model untuk perangkat Host yang ditemukan di LAN
/// </summary>
public class PerangkatLAN
{
    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = string.Empty;

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = string.Empty;

    [JsonPropertyName("portTCP")]
    public int PortTCP { get; set; } = 45679;

    [JsonPropertyName("status")]
    public StatusPerangkat Status { get; set; } = StatusPerangkat.TERSEDIA;

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";

    [JsonPropertyName("waktuTerdeteksi")]
    public DateTime WaktuTerdeteksi { get; set; } = DateTime.UtcNow;

    // Properties untuk UI binding
    [JsonIgnore]
    public string StatusText => Status switch
    {
        StatusPerangkat.TERSEDIA => "Tersedia",
        StatusPerangkat.SIBUK => "Sibuk",
        StatusPerangkat.TIDAK_TERSEDIA => "Offline",
        _ => "Unknown"
    };

    [JsonIgnore]
    public Color StatusColor => Status switch
    {
        StatusPerangkat.TERSEDIA => Color.FromArgb("#4CAF50"),    // Green
        StatusPerangkat.SIBUK => Color.FromArgb("#FF9800"),       // Orange
        StatusPerangkat.TIDAK_TERSEDIA => Color.FromArgb("#9E9E9E"), // Gray
        _ => Color.FromArgb("#9E9E9E")
    };
}

/// <summary>
/// Data untuk request koneksi dari Tamu ke Host
/// </summary>
public class PermintaanKoneksiData
{
    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = string.Empty;

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = string.Empty;

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";
}

/// <summary>
/// Data respon koneksi dari Host ke Tamu
/// </summary>
public class ResponKoneksiData
{
    [JsonPropertyName("hasil")]
    public HasilPersetujuan Hasil { get; set; } = HasilPersetujuan.MENUNGGU;

    [JsonPropertyName("kunciSesi")]
    public string KunciSesi { get; set; } = string.Empty;

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = string.Empty;

    [JsonPropertyName("izinKontrol")]
    public bool IzinKontrol { get; set; } = false;

    [JsonPropertyName("izinTransferBerkas")]
    public bool IzinTransferBerkas { get; set; } = false;

    [JsonPropertyName("izinClipboard")]
    public bool IzinClipboard { get; set; } = false;
}
