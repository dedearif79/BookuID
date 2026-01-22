using System.Text.Json.Serialization;

namespace BookuRemoteRelay.Models;

/// <summary>
/// Payload untuk RELAY_REGISTER_HOST (Host -> Relay)
/// </summary>
public class PayloadRegisterHost
{
    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = "";

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
}

/// <summary>
/// Payload untuk RELAY_REGISTER_HOST_OK (Relay -> Host)
/// </summary>
public class PayloadRegisterHostOK
{
    [JsonPropertyName("hostCode")]
    public string HostCode { get; set; } = "";

    [JsonPropertyName("expiryMinutes")]
    public int ExpiryMinutes { get; set; } = 60;

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = "";
}

/// <summary>
/// Payload untuk RELAY_QUERY_HOST (Tamu -> Relay)
/// </summary>
public class PayloadQueryHost
{
    [JsonPropertyName("hostCode")]
    public string HostCode { get; set; } = "";
}

/// <summary>
/// Payload untuk RELAY_QUERY_HOST_RESULT (Relay -> Tamu)
/// </summary>
public class PayloadQueryHostResult
{
    [JsonPropertyName("found")]
    public bool Found { get; set; }

    [JsonPropertyName("namaHost")]
    public string NamaHost { get; set; } = "";

    [JsonPropertyName("requiresPassword")]
    public bool RequiresPassword { get; set; }

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = "";
}

/// <summary>
/// Payload untuk RELAY_CONNECT_REQUEST (Tamu -> Relay)
/// </summary>
public class PayloadRelayConnectRequest
{
    [JsonPropertyName("hostCode")]
    public string HostCode { get; set; } = "";

    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = "";

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = "";

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
}

/// <summary>
/// Payload untuk RELAY_ERROR (Relay -> Client)
/// </summary>
public class PayloadRelayError
{
    [JsonPropertyName("kodeError")]
    public int KodeError { get; set; }

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = "";
}

/// <summary>
/// Payload untuk PERMINTAAN_KONEKSI (forwarded by relay)
/// </summary>
public class PayloadPermintaanKoneksi
{
    [JsonPropertyName("namaPerangkat")]
    public string NamaPerangkat { get; set; } = "";

    [JsonPropertyName("alamatIP")]
    public string AlamatIP { get; set; } = "";

    [JsonPropertyName("versiProtokol")]
    public string VersiProtokol { get; set; } = "1.0";
}

/// <summary>
/// Payload untuk RESPON_KONEKSI (forwarded by relay)
/// </summary>
public class PayloadResponKoneksi
{
    /// <summary>
    /// Hasil persetujuan (enum as int):
    /// 0 = MENUNGGU, 1 = DITERIMA, 2 = DITOLAK, 3 = TIMEOUT
    /// </summary>
    [JsonPropertyName("hasil")]
    public int Hasil { get; set; } = 0;

    [JsonPropertyName("kunciSesi")]
    public string KunciSesi { get; set; } = "";

    [JsonPropertyName("pesan")]
    public string Pesan { get; set; } = "";

    [JsonPropertyName("izinKontrol")]
    public bool IzinKontrol { get; set; }

    [JsonPropertyName("izinTransferBerkas")]
    public bool IzinTransferBerkas { get; set; }

    [JsonPropertyName("izinClipboard")]
    public bool IzinClipboard { get; set; }

    /// <summary>
    /// Helper property: True jika Hasil == 1 (DITERIMA)
    /// </summary>
    [JsonIgnore]
    public bool Diterima => Hasil == 1;
}
