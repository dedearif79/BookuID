using System.Text.Json.Serialization;

namespace BookuRemoteRelay.Models;

/// <summary>
/// JSON Source Generator Context untuk kompatibilitas dengan IL Trimming.
/// Semua model yang di-serialize/deserialize harus didaftarkan di sini.
/// </summary>
[JsonSerializable(typeof(PaketData))]
[JsonSerializable(typeof(PayloadRegisterHost))]
[JsonSerializable(typeof(PayloadRegisterHostOK))]
[JsonSerializable(typeof(PayloadQueryHost))]
[JsonSerializable(typeof(PayloadQueryHostResult))]
[JsonSerializable(typeof(PayloadRelayConnectRequest))]
[JsonSerializable(typeof(PayloadRelayError))]
[JsonSerializable(typeof(PayloadPermintaanKoneksi))]
[JsonSerializable(typeof(PayloadResponKoneksi))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = false)]
public partial class RelayJsonContext : JsonSerializerContext
{
}
