using System.Text;
using System.Text.Json;
using BookuRemoteRelay.Models;

namespace BookuRemoteRelay.Services;

/// <summary>
/// Service untuk serialisasi/deserialisasi paket.
/// </summary>
public class ProtocolService
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    /// <summary>
    /// Serialize paket ke bytes untuk dikirim.
    /// </summary>
    public byte[] Serialize(PaketData paket)
    {
        var json = JsonSerializer.Serialize(paket, _jsonOptions);
        return Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Deserialize JSON string ke PaketData.
    /// </summary>
    public PaketData? Deserialize(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<PaketData>(json, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserialize payload ke tipe spesifik.
    /// </summary>
    public T? DeserializePayload<T>(string payload) where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(payload, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Serialize payload ke JSON string.
    /// </summary>
    public string SerializePayload<T>(T payload)
    {
        return JsonSerializer.Serialize(payload, _jsonOptions);
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
        return PaketData.Create(TipePaket.RELAY_ERROR, idSesi, SerializePayload(payload));
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
        return PaketData.Create(TipePaket.RELAY_REGISTER_HOST_OK, "", SerializePayload(payload));
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
        return PaketData.Create(TipePaket.RELAY_QUERY_HOST_RESULT, "", SerializePayload(payload));
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
        return PaketData.Create(TipePaket.RELAY_HOST_OFFLINE, "", SerializePayload(payload));
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
        return PaketData.Create(TipePaket.RELAY_INVALID_CODE, "", SerializePayload(payload));
    }
}
