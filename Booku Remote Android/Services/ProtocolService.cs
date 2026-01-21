using System.Text;
using System.Text.Json;
using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk serialisasi dan deserialisasi paket protokol.
/// Menggunakan Source Generated JSON context untuk kompatibilitas dengan IL Trimming.
/// </summary>
public class ProtocolService
{
    public const string MAGIC_STRING = "BOOKU_REMOTE_DISCOVERY";
    public const string PROTOCOL_VERSION = "1.0";

    public ProtocolService()
    {
    }

    #region Serialization

    /// <summary>
    /// Serialize paket ke JSON string
    /// </summary>
    public string SerializePaket(PaketData paket)
    {
        try
        {
            return JsonSerializer.Serialize(paket, BookuJsonContext.Default.PaketData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] SerializePaket ERROR: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Serialize paket ke bytes untuk pengiriman
    /// </summary>
    public byte[] SerializePaketToBytes(PaketData paket)
    {
        var json = SerializePaket(paket);
        return Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// Serialize PermintaanKoneksiData ke JSON
    /// </summary>
    public string SerializePermintaanKoneksi(PermintaanKoneksiData data)
    {
        try
        {
            return JsonSerializer.Serialize(data, BookuJsonContext.Default.PermintaanKoneksiData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] SerializePermintaanKoneksi ERROR: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Serialize InputKeyboard ke JSON
    /// </summary>
    public string SerializeInputKeyboard(InputKeyboard data)
    {
        try
        {
            return JsonSerializer.Serialize(data, BookuJsonContext.Default.InputKeyboard);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] SerializeInputKeyboard ERROR: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Serialize InputMouse ke JSON
    /// </summary>
    public string SerializeInputMouse(InputMouse data)
    {
        try
        {
            return JsonSerializer.Serialize(data, BookuJsonContext.Default.InputMouse);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] SerializeInputMouse ERROR: {ex.Message}");
            return string.Empty;
        }
    }

    #endregion

    #region Deserialization

    /// <summary>
    /// Deserialize JSON string ke PaketData
    /// </summary>
    public PaketData? DeserializePaket(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, BookuJsonContext.Default.PaketData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] DeserializePaket ERROR: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Deserialize bytes ke PaketData
    /// </summary>
    public PaketData? DeserializePaket(byte[] data)
    {
        try
        {
            var json = Encoding.UTF8.GetString(data);
            return DeserializePaket(json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] DeserializePaket(bytes) ERROR: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Deserialize PerangkatLAN dari JSON
    /// </summary>
    public PerangkatLAN? DeserializePerangkatLAN(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, BookuJsonContext.Default.PerangkatLAN);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] DeserializePerangkatLAN ERROR: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Deserialize ResponKoneksiData dari JSON
    /// </summary>
    public ResponKoneksiData? DeserializeResponKoneksi(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, BookuJsonContext.Default.ResponKoneksiData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] DeserializeResponKoneksi ERROR: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Deserialize FrameLayar dari JSON
    /// </summary>
    public FrameLayar? DeserializeFrameLayar(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, BookuJsonContext.Default.FrameLayar);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PROTOCOL] DeserializeFrameLayar ERROR: {ex.Message}");
            return null;
        }
    }

    #endregion

    #region Packet Creation

    /// <summary>
    /// Membuat paket discovery broadcast
    /// </summary>
    public PaketData CreateDiscoveryBroadcast()
    {
        return PaketData.Create(TipePaket.BROADCAST_DISCOVERY, "", MAGIC_STRING);
    }

    /// <summary>
    /// Membuat paket permintaan koneksi
    /// </summary>
    public PaketData CreatePermintaanKoneksi(string namaPerangkat, string alamatIP)
    {
        var data = new PermintaanKoneksiData
        {
            NamaPerangkat = namaPerangkat,
            AlamatIP = alamatIP,
            VersiProtokol = PROTOCOL_VERSION
        };

        var payload = SerializePermintaanKoneksi(data);
        return PaketData.Create(TipePaket.PERMINTAAN_KONEKSI, "", payload);
    }

    /// <summary>
    /// Membuat paket permintaan streaming
    /// </summary>
    public PaketData CreatePermintaanStreaming(string kunciSesi)
    {
        return PaketData.Create(TipePaket.PERMINTAAN_STREAMING, kunciSesi, "");
    }

    /// <summary>
    /// Membuat paket hentikan streaming
    /// </summary>
    public PaketData CreateHentikanStreaming(string kunciSesi)
    {
        return PaketData.Create(TipePaket.HENTIKAN_STREAMING, kunciSesi, "");
    }

    /// <summary>
    /// Membuat paket heartbeat
    /// </summary>
    public PaketData CreateHeartbeat(string kunciSesi)
    {
        return PaketData.Create(TipePaket.HEARTBEAT, kunciSesi, "");
    }

    /// <summary>
    /// Membuat paket tutup koneksi
    /// </summary>
    public PaketData CreateTutupKoneksi(string kunciSesi)
    {
        return PaketData.Create(TipePaket.TUTUP_KONEKSI, kunciSesi, "");
    }

    /// <summary>
    /// Membuat paket input keyboard
    /// </summary>
    public PaketData CreateInputKeyboard(string kunciSesi, InputKeyboard input)
    {
        var payload = SerializeInputKeyboard(input);
        return PaketData.Create(TipePaket.INPUT_KEYBOARD, kunciSesi, payload);
    }

    /// <summary>
    /// Membuat paket input mouse
    /// </summary>
    public PaketData CreateInputMouse(string kunciSesi, InputMouse input)
    {
        var payload = SerializeInputMouse(input);
        return PaketData.Create(TipePaket.INPUT_MOUSE, kunciSesi, payload);
    }

    #endregion

    #region Packet Parsing

    /// <summary>
    /// Parse respon discovery ke PerangkatLAN
    /// </summary>
    public PerangkatLAN? ParseResponDiscovery(PaketData paket)
    {
        if (paket.TipePaket != (int)TipePaket.RESPON_DISCOVERY)
            return null;

        return DeserializePerangkatLAN(paket.Payload);
    }

    /// <summary>
    /// Parse respon koneksi
    /// </summary>
    public ResponKoneksiData? ParseResponKoneksi(PaketData paket)
    {
        if (paket.TipePaket != (int)TipePaket.RESPON_KONEKSI)
            return null;

        return DeserializeResponKoneksi(paket.Payload);
    }

    /// <summary>
    /// Parse frame layar
    /// </summary>
    public FrameLayar? ParseFrameLayar(PaketData paket)
    {
        if (paket.TipePaket != (int)TipePaket.FRAME_LAYAR)
            return null;

        return DeserializeFrameLayar(paket.Payload);
    }

    #endregion
}
