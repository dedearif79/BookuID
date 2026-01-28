Option Explicit On
Option Strict On

Imports System.Text
Imports System.Text.Json

''' <summary>
''' Modul untuk serialization dan deserialization paket data.
''' Menggunakan System.Text.Json (bawaan .NET).
''' </summary>
Public Module mdl_Protokol

#Region "JSON Options"

    Private ReadOnly JsonOptions As New JsonSerializerOptions With {
        .PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        .PropertyNameCaseInsensitive = True,
        .WriteIndented = False
    }

#End Region

#Region "Serialization - Paket"

    ''' <summary>
    ''' Serialisasi paket data ke string JSON.
    ''' </summary>
    Public Function SerializePaket(paket As cls_PaketData) As String
        Try
            Return JsonSerializer.Serialize(paket, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi string JSON ke paket data.
    ''' </summary>
    Public Function DeserializePaket(json As String) As cls_PaketData
        Try
            Return JsonSerializer.Deserialize(Of cls_PaketData)(json, JsonOptions)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[DEBUG] DeserializePaket GAGAL: {ex.Message}")
            System.Diagnostics.Debug.WriteLine($"[DEBUG] JSON input: {json.Substring(0, Math.Min(300, json.Length))}")
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Payload Permintaan Koneksi"

    ''' <summary>
    ''' Serialisasi payload permintaan koneksi.
    ''' </summary>
    Public Function SerializePermintaanKoneksi(payload As cls_PayloadPermintaanKoneksi) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload permintaan koneksi.
    ''' </summary>
    Public Function DeserializePermintaanKoneksi(json As String) As cls_PayloadPermintaanKoneksi
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadPermintaanKoneksi)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Payload Respon Koneksi"

    ''' <summary>
    ''' Serialisasi payload respon koneksi.
    ''' </summary>
    Public Function SerializeResponKoneksi(payload As cls_PayloadResponKoneksi) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload respon koneksi.
    ''' </summary>
    Public Function DeserializeResponKoneksi(json As String) As cls_PayloadResponKoneksi
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadResponKoneksi)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Perangkat LAN"

    ''' <summary>
    ''' Serialisasi info perangkat LAN.
    ''' </summary>
    Public Function SerializePerangkatLAN(perangkat As cls_PerangkatLAN) As String
        Try
            Return JsonSerializer.Serialize(perangkat, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi info perangkat LAN.
    ''' </summary>
    Public Function DeserializePerangkatLAN(json As String) As cls_PerangkatLAN
        Try
            Return JsonSerializer.Deserialize(Of cls_PerangkatLAN)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Frame Layar"

    ''' <summary>
    ''' Serialisasi frame layar.
    ''' </summary>
    Public Function SerializeFrameLayar(frame As cls_FrameLayar) As String
        Try
            Return JsonSerializer.Serialize(frame, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi frame layar.
    ''' </summary>
    Public Function DeserializeFrameLayar(json As String) As cls_FrameLayar
        Try
            Return JsonSerializer.Deserialize(Of cls_FrameLayar)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Input Keyboard (Fase 2b)"

    ''' <summary>
    ''' Serialisasi payload input keyboard.
    ''' </summary>
    Public Function SerializeInputKeyboard(payload As cls_PayloadInputKeyboard) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload input keyboard.
    ''' </summary>
    Public Function DeserializeInputKeyboard(json As String) As cls_PayloadInputKeyboard
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadInputKeyboard)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Input Mouse (Fase 2b)"

    ''' <summary>
    ''' Serialisasi payload input mouse.
    ''' </summary>
    Public Function SerializeInputMouse(payload As cls_PayloadInputMouse) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload input mouse.
    ''' </summary>
    Public Function DeserializeInputMouse(json As String) As cls_PayloadInputMouse
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadInputMouse)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Encoding Helper"

    ''' <summary>
    ''' Konversi string ke byte array (UTF-8).
    ''' </summary>
    Public Function StringKeBytes(text As String) As Byte()
        Return Encoding.UTF8.GetBytes(text)
    End Function

    ''' <summary>
    ''' Konversi byte array ke string (UTF-8).
    ''' </summary>
    Public Function BytesKeString(data As Byte()) As String
        Return Encoding.UTF8.GetString(data)
    End Function

    ''' <summary>
    ''' Konversi byte array ke string dengan panjang tertentu.
    ''' </summary>
    Public Function BytesKeString(data As Byte(), offset As Integer, count As Integer) As String
        Return Encoding.UTF8.GetString(data, offset, count)
    End Function

#End Region

#Region "Serialization - Relay Payloads (Fase 4)"

    ''' <summary>
    ''' Serialisasi payload register host.
    ''' </summary>
    Public Function SerializeRegisterHost(payload As cls_PayloadRegisterHost) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload register host OK.
    ''' </summary>
    Public Function DeserializeRegisterHostOK(json As String) As cls_PayloadRegisterHostOK
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadRegisterHostOK)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload query host.
    ''' </summary>
    Public Function SerializeQueryHost(payload As cls_PayloadQueryHost) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload query host result.
    ''' </summary>
    Public Function DeserializeQueryHostResult(json As String) As cls_PayloadQueryHostResult
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadQueryHostResult)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload relay connect request.
    ''' </summary>
    Public Function SerializeRelayConnectRequest(payload As cls_PayloadRelayConnectRequest) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload relay error.
    ''' </summary>
    Public Function DeserializeRelayError(json As String) As cls_PayloadRelayError
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadRelayError)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Clipboard (Fase 3)"

    ''' <summary>
    ''' Serialisasi payload clipboard.
    ''' </summary>
    Public Function SerializeClipboard(payload As cls_PayloadClipboard) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload clipboard.
    ''' </summary>
    Public Function DeserializeClipboard(json As String) As cls_PayloadClipboard
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadClipboard)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Serialization - Transfer Berkas (Fase 3b)"

    ''' <summary>
    ''' Serialisasi payload permintaan transfer.
    ''' </summary>
    Public Function SerializePermintaanTransfer(payload As cls_PayloadPermintaanTransfer) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload permintaan transfer.
    ''' </summary>
    Public Function DeserializePermintaanTransfer(json As String) As cls_PayloadPermintaanTransfer
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadPermintaanTransfer)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload respon transfer.
    ''' </summary>
    Public Function SerializeResponTransfer(payload As cls_PayloadResponTransfer) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload respon transfer.
    ''' </summary>
    Public Function DeserializeResponTransfer(json As String) As cls_PayloadResponTransfer
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadResponTransfer)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload data berkas (chunk).
    ''' </summary>
    Public Function SerializeDataBerkas(payload As cls_PayloadDataBerkas) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload data berkas (chunk).
    ''' </summary>
    Public Function DeserializeDataBerkas(json As String) As cls_PayloadDataBerkas
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadDataBerkas)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload konfirmasi chunk.
    ''' </summary>
    Public Function SerializeKonfirmasiChunk(payload As cls_PayloadKonfirmasiChunk) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload konfirmasi chunk.
    ''' </summary>
    Public Function DeserializeKonfirmasiChunk(json As String) As cls_PayloadKonfirmasiChunk
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadKonfirmasiChunk)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload konfirmasi berkas (transfer selesai).
    ''' </summary>
    Public Function SerializeKonfirmasiBerkas(payload As cls_PayloadKonfirmasiBerkas) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload konfirmasi berkas.
    ''' </summary>
    Public Function DeserializeKonfirmasiBerkas(json As String) As cls_PayloadKonfirmasiBerkas
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadKonfirmasiBerkas)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload batal transfer.
    ''' </summary>
    Public Function SerializeBatalTransfer(payload As cls_PayloadBatalTransfer) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload batal transfer.
    ''' </summary>
    Public Function DeserializeBatalTransfer(json As String) As cls_PayloadBatalTransfer
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadBatalTransfer)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload daftar folder.
    ''' </summary>
    Public Function SerializeDaftarFolder(payload As cls_PayloadDaftarFolder) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload daftar folder.
    ''' </summary>
    Public Function DeserializeDaftarFolder(json As String) As cls_PayloadDaftarFolder
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadDaftarFolder)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Serialisasi payload respon daftar folder.
    ''' </summary>
    Public Function SerializeResponDaftarFolder(payload As cls_PayloadResponDaftarFolder) As String
        Try
            Return JsonSerializer.Serialize(payload, JsonOptions)
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Deserialisasi payload respon daftar folder.
    ''' </summary>
    Public Function DeserializeResponDaftarFolder(json As String) As cls_PayloadResponDaftarFolder
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadResponDaftarFolder)(json, JsonOptions)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Paket Builder Helper"

    ''' <summary>
    ''' Membuat paket discovery broadcast.
    ''' </summary>
    Public Function BuatPaketDiscoveryBroadcast() As cls_PaketData
        Return New cls_PaketData(TipePaket.BROADCAST_DISCOVERY, MAGIC_DISCOVERY)
    End Function

    ''' <summary>
    ''' Membuat paket respon discovery.
    ''' </summary>
    Public Function BuatPaketResponDiscovery(perangkat As cls_PerangkatLAN) As cls_PaketData
        Return New cls_PaketData(TipePaket.RESPON_DISCOVERY, SerializePerangkatLAN(perangkat))
    End Function

    ''' <summary>
    ''' Membuat paket permintaan koneksi.
    ''' </summary>
    ''' <param name="namaPerangkat">Nama perangkat Tamu</param>
    ''' <param name="alamatIP">Alamat IP Tamu</param>
    ''' <param name="supportedCodecs">Daftar codec yang didukung client (default: JPEG)</param>
    Public Function BuatPaketPermintaanKoneksi(namaPerangkat As String, alamatIP As String,
                                                Optional supportedCodecs As String() = Nothing) As cls_PaketData
        Dim payload As New cls_PayloadPermintaanKoneksi With {
            .NamaPerangkat = namaPerangkat,
            .AlamatIP = alamatIP,
            .VersiProtokol = VERSI_PROTOKOL,
            .SupportedCodecs = If(supportedCodecs, {"JPEG"})
        }
        Return New cls_PaketData(TipePaket.PERMINTAAN_KONEKSI, SerializePermintaanKoneksi(payload))
    End Function

    ''' <summary>
    ''' Membuat paket respon koneksi.
    ''' </summary>
    Public Function BuatPaketResponKoneksi(hasil As HasilPersetujuan, kunciSesi As String,
                                           Optional pesan As String = "",
                                           Optional izinKontrol As Boolean = True,
                                           Optional izinTransfer As Boolean = False,
                                           Optional izinClipboard As Boolean = False,
                                           Optional selectedCodec As String = "JPEG") As cls_PaketData
        Dim payload As New cls_PayloadResponKoneksi With {
            .Hasil = hasil,
            .KunciSesi = kunciSesi,
            .Pesan = pesan,
            .IzinKontrol = izinKontrol,
            .IzinTransferBerkas = izinTransfer,
            .IzinClipboard = izinClipboard,
            .SelectedCodec = selectedCodec
        }
        Return New cls_PaketData(TipePaket.RESPON_KONEKSI, SerializeResponKoneksi(payload))
    End Function

    ''' <summary>
    ''' Membuat paket heartbeat.
    ''' </summary>
    Public Function BuatPaketHeartbeat(idSesi As String) As cls_PaketData
        Dim paket As New cls_PaketData(TipePaket.HEARTBEAT)
        paket.IdSesi = idSesi
        Return paket
    End Function

    ''' <summary>
    ''' Membuat paket tutup koneksi.
    ''' </summary>
    Public Function BuatPaketTutupKoneksi(idSesi As String) As cls_PaketData
        Dim paket As New cls_PaketData(TipePaket.TUTUP_KONEKSI)
        paket.IdSesi = idSesi
        Return paket
    End Function

    ''' <summary>
    ''' Membuat paket frame layar.
    ''' </summary>
    Public Function BuatPaketFrameLayar(frame As cls_FrameLayar) As cls_PaketData
        Return New cls_PaketData(TipePaket.FRAME_LAYAR, SerializeFrameLayar(frame))
    End Function

    ''' <summary>
    ''' Membuat paket permintaan streaming (Tamu minta mulai streaming).
    ''' </summary>
    Public Function BuatPaketPermintaanStreaming(idSesi As String) As cls_PaketData
        Dim paket As New cls_PaketData(TipePaket.PERMINTAAN_STREAMING)
        paket.IdSesi = idSesi
        Return paket
    End Function

    ''' <summary>
    ''' Membuat paket hentikan streaming (Tamu minta stop streaming).
    ''' </summary>
    Public Function BuatPaketHentikanStreaming(idSesi As String) As cls_PaketData
        Dim paket As New cls_PaketData(TipePaket.HENTIKAN_STREAMING)
        paket.IdSesi = idSesi
        Return paket
    End Function

    ''' <summary>
    ''' Membuat paket input keyboard (Fase 2b - Tamu kirim keyboard ke Host).
    ''' </summary>
    Public Function BuatPaketInputKeyboard(keyCode As Integer, isKeyDown As Boolean,
                                            Optional isExtended As Boolean = False,
                                            Optional modifiers As Integer = 0) As cls_PaketData
        Dim payload As New cls_PayloadInputKeyboard With {
            .KeyCode = keyCode,
            .IsKeyDown = isKeyDown,
            .IsExtended = isExtended,
            .Modifiers = modifiers
        }
        Return New cls_PaketData(TipePaket.INPUT_KEYBOARD, SerializeInputKeyboard(payload))
    End Function

    ''' <summary>
    ''' Membuat paket input mouse move (Fase 2b).
    ''' </summary>
    Public Function BuatPaketInputMouseMove(normalizedX As Double, normalizedY As Double) As cls_PaketData
        Dim payload As New cls_PayloadInputMouse With {
            .TipeAksi = TipeAksiMouse.PINDAH,
            .X = normalizedX,
            .Y = normalizedY
        }
        Return New cls_PaketData(TipePaket.INPUT_MOUSE, SerializeInputMouse(payload))
    End Function

    ''' <summary>
    ''' Membuat paket input mouse click (Fase 2b).
    ''' </summary>
    Public Function BuatPaketInputMouseClick(button As Integer, isDown As Boolean,
                                              normalizedX As Double, normalizedY As Double) As cls_PaketData
        Dim payload As New cls_PayloadInputMouse With {
            .TipeAksi = TipeAksiMouse.KLIK,
            .X = normalizedX,
            .Y = normalizedY,
            .Button = button,
            .IsButtonDown = isDown
        }
        Return New cls_PaketData(TipePaket.INPUT_MOUSE, SerializeInputMouse(payload))
    End Function

    ''' <summary>
    ''' Membuat paket input mouse wheel (Fase 2b).
    ''' </summary>
    Public Function BuatPaketInputMouseWheel(wheelDelta As Integer,
                                              normalizedX As Double, normalizedY As Double) As cls_PaketData
        Dim payload As New cls_PayloadInputMouse With {
            .TipeAksi = TipeAksiMouse.RODA,
            .X = normalizedX,
            .Y = normalizedY,
            .WheelDelta = wheelDelta
        }
        Return New cls_PaketData(TipePaket.INPUT_MOUSE, SerializeInputMouse(payload))
    End Function

    ''' <summary>
    ''' Membuat paket input mouse dengan parameter lengkap (untuk relay mode).
    ''' </summary>
    Public Function BuatPaketInputMouse(tipeAksi As TipeAksiMouse, x As Double, y As Double,
                                         button As Integer, isButtonDown As Boolean,
                                         wheelDelta As Integer) As cls_PaketData
        Dim payload As New cls_PayloadInputMouse With {
            .TipeAksi = tipeAksi,
            .X = x,
            .Y = y,
            .Button = button,
            .IsButtonDown = isButtonDown,
            .WheelDelta = wheelDelta
        }
        Return New cls_PaketData(TipePaket.INPUT_MOUSE, SerializeInputMouse(payload))
    End Function

#End Region

#Region "Paket Builder Helper - Relay (Fase 4)"

    ''' <summary>
    ''' Membuat paket RELAY_REGISTER_HOST (Host mendaftar ke relay).
    ''' </summary>
    Public Function BuatPaketRelayRegisterHost(namaPerangkat As String, Optional password As String = "") As cls_PaketData
        Dim payload As New cls_PayloadRegisterHost With {
            .NamaPerangkat = namaPerangkat,
            .VersiProtokol = VERSI_PROTOKOL,
            .Password = password
        }
        Return New cls_PaketData(TipePaket.RELAY_REGISTER_HOST, SerializeRegisterHost(payload))
    End Function

    ''' <summary>
    ''' Membuat paket RELAY_UNREGISTER_HOST (Host logout dari relay).
    ''' </summary>
    Public Function BuatPaketRelayUnregisterHost() As cls_PaketData
        Return New cls_PaketData(TipePaket.RELAY_UNREGISTER_HOST, "")
    End Function

    ''' <summary>
    ''' Membuat paket RELAY_HOST_HEARTBEAT (Host keep-alive ke relay).
    ''' </summary>
    Public Function BuatPaketRelayHeartbeat() As cls_PaketData
        Return New cls_PaketData(TipePaket.RELAY_HOST_HEARTBEAT, "")
    End Function

    ''' <summary>
    ''' Membuat paket RELAY_QUERY_HOST (Tamu mencari Host by HostCode).
    ''' </summary>
    Public Function BuatPaketRelayQueryHost(hostCode As String) As cls_PaketData
        Dim payload As New cls_PayloadQueryHost With {
            .HostCode = hostCode
        }
        Return New cls_PaketData(TipePaket.RELAY_QUERY_HOST, SerializeQueryHost(payload))
    End Function

    ''' <summary>
    ''' Membuat paket RELAY_CONNECT_REQUEST (Tamu minta koneksi via relay).
    ''' </summary>
    ''' <param name="hostCode">HostCode 6 karakter</param>
    ''' <param name="namaTamu">Nama perangkat Tamu</param>
    ''' <param name="password">Password opsional</param>
    ''' <param name="supportedCodecs">Daftar codec yang didukung client</param>
    Public Function BuatPaketRelayConnectRequest(hostCode As String, namaTamu As String,
                                                   Optional password As String = "",
                                                   Optional supportedCodecs As String() = Nothing) As cls_PaketData
        Dim payload As New cls_PayloadRelayConnectRequest With {
            .HostCode = hostCode,
            .NamaPerangkat = namaTamu,
            .AlamatIP = AlamatIPLokal,
            .VersiProtokol = VERSI_PROTOKOL,
            .Password = password,
            .SupportedCodecs = If(supportedCodecs, {"JPEG", "H264"})
        }
        Return New cls_PaketData(TipePaket.RELAY_CONNECT_REQUEST, SerializeRelayConnectRequest(payload))
    End Function

#End Region

#Region "Paket Builder Helper - Clipboard (Fase 3)"

    ''' <summary>
    ''' Membuat paket CLIPBOARD_DATA untuk sinkronisasi clipboard.
    ''' </summary>
    ''' <param name="tipeData">"TEXT" atau "IMAGE"</param>
    ''' <param name="data">Teks biasa atau Base64 PNG untuk gambar</param>
    ''' <param name="source">"HOST" atau "TAMU"</param>
    ''' <param name="hashData">MD5 hash dari data untuk deduplikasi</param>
    Public Function BuatPaketClipboard(tipeData As String, data As String,
                                        source As String, hashData As String) As cls_PaketData
        Dim payload As New cls_PayloadClipboard With {
            .TipeData = tipeData,
            .Data = data,
            .Timestamp = DateTime.UtcNow.Ticks,
            .Source = source,
            .HashData = hashData
        }
        Return New cls_PaketData(TipePaket.CLIPBOARD_DATA, SerializeClipboard(payload))
    End Function

#End Region

#Region "Paket Builder Helper - Transfer Berkas (Fase 3b)"

    ''' <summary>
    ''' Membuat paket PERMINTAAN_BERKAS untuk meminta izin transfer file.
    ''' </summary>
    ''' <param name="transfer">State transfer yang berisi info file</param>
    Public Function BuatPaketPermintaanBerkas(transfer As cls_TransferBerkas) As cls_PaketData
        Dim payload As New cls_PayloadPermintaanTransfer With {
            .TransferId = transfer.TransferId,
            .Arah = If(transfer.Arah = ArahTransfer.UPLOAD, "UPLOAD", "DOWNLOAD"),
            .NamaFile = transfer.NamaFile,
            .UkuranFile = transfer.UkuranFile,
            .HashFile = transfer.HashFile,
            .TotalChunk = transfer.TotalChunk,
            .UkuranChunk = transfer.UkuranChunk,
            .PathSumber = transfer.PathSumber
        }
        Return New cls_PaketData(TipePaket.PERMINTAAN_BERKAS, SerializePermintaanTransfer(payload))
    End Function

    ''' <summary>
    ''' Membuat paket RESPON_TRANSFER untuk menerima/menolak request transfer.
    ''' </summary>
    ''' <param name="transferId">ID transfer yang direspon</param>
    ''' <param name="diterima">True jika request diterima</param>
    ''' <param name="pesan">Pesan atau alasan penolakan</param>
    ''' <param name="mulaiDariChunk">Index chunk untuk resume (default 0)</param>
    Public Function BuatPaketResponTransfer(transferId As String, diterima As Boolean,
                                             Optional pesan As String = "",
                                             Optional mulaiDariChunk As Integer = 0) As cls_PaketData
        Dim payload As New cls_PayloadResponTransfer With {
            .TransferId = transferId,
            .Diterima = diterima,
            .Pesan = pesan,
            .MulaiDariChunk = mulaiDariChunk
        }
        Return New cls_PaketData(TipePaket.RESPON_TRANSFER, SerializeResponTransfer(payload))
    End Function

    ''' <summary>
    ''' Membuat paket DATA_BERKAS untuk mengirim satu chunk file.
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="chunkIndex">Index chunk (0-based)</param>
    ''' <param name="data">Data chunk dalam byte array</param>
    Public Function BuatPaketDataBerkas(transferId As String, chunkIndex As Integer, data As Byte()) As cls_PaketData
        Dim payload As New cls_PayloadDataBerkas With {
            .TransferId = transferId,
            .ChunkIndex = chunkIndex,
            .Data = Convert.ToBase64String(data),
            .Checksum = cls_TransferBerkas.HitungHashData(data)
        }
        Return New cls_PaketData(TipePaket.DATA_BERKAS, SerializeDataBerkas(payload))
    End Function

    ''' <summary>
    ''' Membuat paket KONFIRMASI_CHUNK untuk ACK per chunk.
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="chunkIndex">Index chunk yang dikonfirmasi</param>
    ''' <param name="sukses">True jika chunk diterima dengan benar</param>
    ''' <param name="kirimUlang">True jika perlu kirim ulang</param>
    Public Function BuatPaketKonfirmasiChunk(transferId As String, chunkIndex As Integer,
                                              sukses As Boolean,
                                              Optional kirimUlang As Boolean = False) As cls_PaketData
        Dim payload As New cls_PayloadKonfirmasiChunk With {
            .TransferId = transferId,
            .ChunkIndex = chunkIndex,
            .Sukses = sukses,
            .KirimUlang = kirimUlang
        }
        Return New cls_PaketData(TipePaket.KONFIRMASI_CHUNK, SerializeKonfirmasiChunk(payload))
    End Function

    ''' <summary>
    ''' Membuat paket KONFIRMASI_BERKAS untuk konfirmasi transfer selesai.
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="sukses">True jika transfer sukses</param>
    ''' <param name="hashHasil">Hash file hasil untuk verifikasi</param>
    ''' <param name="pesan">Pesan error jika gagal</param>
    Public Function BuatPaketKonfirmasiBerkas(transferId As String, sukses As Boolean,
                                               Optional hashHasil As String = "",
                                               Optional pesan As String = "") As cls_PaketData
        Dim payload As New cls_PayloadKonfirmasiBerkas With {
            .TransferId = transferId,
            .Sukses = sukses,
            .HashHasil = hashHasil,
            .Pesan = pesan
        }
        Return New cls_PaketData(TipePaket.KONFIRMASI_BERKAS, SerializeKonfirmasiBerkas(payload))
    End Function

    ''' <summary>
    ''' Membuat paket BATAL_TRANSFER untuk membatalkan transfer.
    ''' </summary>
    ''' <param name="transferId">ID transfer yang dibatalkan</param>
    ''' <param name="alasan">Alasan pembatalan</param>
    Public Function BuatPaketBatalTransfer(transferId As String, alasan As String) As cls_PaketData
        Dim payload As New cls_PayloadBatalTransfer With {
            .TransferId = transferId,
            .Alasan = alasan
        }
        Return New cls_PaketData(TipePaket.BATAL_TRANSFER, SerializeBatalTransfer(payload))
    End Function

    ''' <summary>
    ''' Membuat paket DAFTAR_FOLDER untuk request daftar folder.
    ''' </summary>
    ''' <param name="path">Path folder yang diminta (kosong = home folder)</param>
    Public Function BuatPaketDaftarFolder(Optional path As String = "") As cls_PaketData
        Dim payload As New cls_PayloadDaftarFolder With {
            .Path = path
        }
        Return New cls_PaketData(TipePaket.DAFTAR_FOLDER, SerializeDaftarFolder(payload))
    End Function

    ''' <summary>
    ''' Membuat paket RESPON_DAFTAR_FOLDER untuk response daftar folder.
    ''' </summary>
    ''' <param name="path">Path folder yang di-list</param>
    ''' <param name="items">Daftar item file dan folder</param>
    ''' <param name="sukses">True jika berhasil</param>
    ''' <param name="parentPath">Path parent folder</param>
    ''' <param name="pesan">Pesan error jika gagal</param>
    Public Function BuatPaketResponDaftarFolder(path As String, items As List(Of cls_ItemFolder),
                                                 sukses As Boolean,
                                                 Optional parentPath As String = "",
                                                 Optional pesan As String = "") As cls_PaketData
        Dim payload As New cls_PayloadResponDaftarFolder With {
            .Path = path,
            .Items = items,
            .Sukses = sukses,
            .ParentPath = parentPath,
            .Pesan = pesan
        }
        Return New cls_PaketData(TipePaket.RESPON_DAFTAR_FOLDER, SerializeResponDaftarFolder(payload))
    End Function

#End Region

End Module
