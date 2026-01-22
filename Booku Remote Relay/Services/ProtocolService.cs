using System.Text;
using System.Text.Json;
using BookuRemoteRelay.Models;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Service untuk serialisasi/deserialisasi paket.
/// Menggunakan Source Generator untuk kompatibilitas dengan IL Trimming.
/// </summary>
public class ProtocolService
{
    /// <summary>
    /// Serialize paket ke bytes untuk dikirim.
    /// </summary>
    public byte[] Serialize(PaketData paket)
    {
        var json = JsonSerializer.Serialize(paket, RelayJsonContext.Default.PaketData);
        return Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Deserialize JSON string ke PaketData.
    /// </summary>
    public PaketData? Deserialize(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, RelayJsonContext.Default.PaketData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PROTOCOL] Deserialize error: {ex.Message}");
            Console.WriteLine($"[PROTOCOL] JSON (first 200 chars): {json.Substring(0, Math.Min(200, json.Length))}");
            return null;
        }
    }

    /// <summary>
    /// Deserialize payload RELAY_REGISTER_HOST.
    /// </summary>
    public PayloadRegisterHost? DeserializeRegisterHost(string payload)
    {
        try
        {
            return JsonSerializer.Deserialize(payload, RelayJsonContext.Default.PayloadRegisterHost);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserialize payload RELAY_QUERY_HOST.
    /// </summary>
    public PayloadQueryHost? DeserializeQueryHost(string payload)
    {
        try
        {
            return JsonSerializer.Deserialize(payload, RelayJsonContext.Default.PayloadQueryHost);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserialize payload RELAY_CONNECT_REQUEST.
    /// </summary>
    public PayloadRelayConnectRequest? DeserializeConnectRequest(string payload)
    {
        try
        {
            return JsonSerializer.Deserialize(payload, RelayJsonContext.Default.PayloadRelayConnectRequest);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserialize payload RESPON_KONEKSI.
    /// </summary>
    public PayloadResponKoneksi? DeserializeResponKoneksi(string payload)
    {
        try
        {
            return JsonSerializer.Deserialize(payload, RelayJsonContext.Default.PayloadResponKoneksi);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Serialize PayloadRegisterHostOK.
    /// </summary>
    public string SerializeRegisterHostOK(PayloadRegisterHostOK payload)
    {
        return JsonSerializer.Serialize(payload, RelayJsonContext.Default.PayloadRegisterHostOK);
    }

    /// <summary>
    /// Serialize PayloadQueryHostResult.
    /// </summary>
    public string SerializeQueryHostResult(PayloadQueryHostResult payload)
    {
        return JsonSerializer.Serialize(payload, RelayJsonContext.Default.PayloadQueryHostResult);
    }

    /// <summary>
    /// Serialize PayloadRelayError.
    /// </summary>
    public string SerializeRelayError(PayloadRelayError payload)
    {
        return JsonSerializer.Serialize(payload, RelayJsonContext.Default.PayloadRelayError);
    }

    /// <summary>
    /// Serialize PayloadPermintaanKoneksi.
    /// </summary>
    public string SerializePermintaanKoneksi(PayloadPermintaanKoneksi payload)
    {
        return JsonSerializer.Serialize(payload, RelayJsonContext.Default.PayloadPermintaanKoneksi);
    }

    /// <summary>
    /// Buat paket relay error.
    /// </summary>
    public PaketData CreateErrorPacket(string idSesi, int kodeError, string pesan)
    {
        var payload = new PayloadRelayError
        {
            KodeError = kodeError,
            Pesan = pesan
        };
        return PaketData.Create(TipePaket.RELAY_ERROR, idSesi, SerializeRelayError(payload));
    }

    /// <summary>
    /// Buat paket RELAY_REGISTER_HOST_OK.
    /// </summary>
    public PaketData CreateRegisterHostOKPacket(string hostCode, int expiryMinutes)
    {
        var payload = new PayloadRegisterHostOK
        {
            HostCode = hostCode,
            ExpiryMinutes = expiryMinutes,
            Pesan = "Registrasi berhasil"
        };
        return PaketData.Create(TipePaket.RELAY_REGISTER_HOST_OK, "", SerializeRegisterHostOK(payload));
    }

    /// <summary>
    /// Buat paket RELAY_QUERY_HOST_RESULT.
    /// </summary>
    public PaketData CreateQueryHostResultPacket(bool found, string namaHost, bool requiresPassword, string pesan = "")
    {
        var payload = new PayloadQueryHostResult
        {
            Found = found,
            NamaHost = namaHost,
            RequiresPassword = requiresPassword,
            Pesan = pesan
        };
        return PaketData.Create(TipePaket.RELAY_QUERY_HOST_RESULT, "", SerializeQueryHostResult(payload));
    }

    /// <summary>
    /// Buat paket RELAY_HOST_OFFLINE.
    /// </summary>
    public PaketData CreateHostOfflinePacket(string pesan = "Host tidak tersedia")
    {
        var payload = new PayloadRelayError
        {
            KodeError = 56,
            Pesan = pesan
        };
        return PaketData.Create(TipePaket.RELAY_HOST_OFFLINE, "", SerializeRelayError(payload));
    }

    /// <summary>
    /// Buat paket RELAY_INVALID_CODE.
    /// </summary>
    public PaketData CreateInvalidCodePacket(string pesan = "HostCode tidak valid")
    {
        var payload = new PayloadRelayError
        {
            KodeError = 57,
            Pesan = pesan
        };
        return PaketData.Create(TipePaket.RELAY_INVALID_CODE, "", SerializeRelayError(payload));
    }
}
