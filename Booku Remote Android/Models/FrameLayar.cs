using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BookuRemoteAndroid.Models;

/// <summary>
/// Model untuk frame layar yang diterima dari Host
/// </summary>
public class FrameLayar
{
    [JsonPropertyName("nomorFrame")]
    public long NomorFrame { get; set; }

    [JsonPropertyName("lebar")]
    public int Lebar { get; set; }

    [JsonPropertyName("tinggi")]
    public int Tinggi { get; set; }

    [JsonPropertyName("dataGambarBase64")]
    public string DataGambarBase64 { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("checksum")]
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Mendecode Base64 menjadi byte array gambar
    /// </summary>
    public byte[]? GetImageBytes()
    {
        try
        {
            if (string.IsNullOrEmpty(DataGambarBase64))
                return null;

            return Convert.FromBase64String(DataGambarBase64);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Memvalidasi checksum frame
    /// Formula: MD5("{nomorFrame}|{lebar}|{tinggi}|{timestamp}|{dataGambarBase64}")
    /// </summary>
    public bool ValidasiChecksum()
    {
        var data = $"{NomorFrame}|{Lebar}|{Tinggi}|{Timestamp}|{DataGambarBase64}";
        var bytes = Encoding.UTF8.GetBytes(data);
        var hash = MD5.HashData(bytes);
        var calculatedChecksum = Convert.ToHexString(hash).ToLowerInvariant();
        return Checksum == calculatedChecksum;
    }

    /// <summary>
    /// Konversi ke ImageSource untuk MAUI
    /// </summary>
    public ImageSource? ToImageSource()
    {
        var imageBytes = GetImageBytes();
        if (imageBytes == null) return null;

        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }
}
