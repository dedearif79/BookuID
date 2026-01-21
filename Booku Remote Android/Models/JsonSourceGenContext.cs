using System.Text.Json.Serialization;

namespace BookuRemoteAndroid.Models;

/// <summary>
/// Source Generated JSON serialization context untuk Booku Remote Android.
/// Diperlukan untuk IL Trimming (Release build) karena reflection-based serialization tidak tersedia.
/// </summary>
[JsonSerializable(typeof(PaketData))]
[JsonSerializable(typeof(PerangkatLAN))]
[JsonSerializable(typeof(FrameLayar))]
[JsonSerializable(typeof(InputKeyboard))]
[JsonSerializable(typeof(InputMouse))]
[JsonSerializable(typeof(PermintaanKoneksiData))]
[JsonSerializable(typeof(ResponKoneksiData))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
public partial class BookuJsonContext : JsonSerializerContext
{
}
