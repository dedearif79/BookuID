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
        Catch
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
    Public Function BuatPaketPermintaanKoneksi(namaPerangkat As String, alamatIP As String) As cls_PaketData
        Dim payload As New cls_PayloadPermintaanKoneksi With {
            .NamaPerangkat = namaPerangkat,
            .AlamatIP = alamatIP,
            .VersiProtokol = VERSI_PROTOKOL
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
                                           Optional izinClipboard As Boolean = False) As cls_PaketData
        Dim payload As New cls_PayloadResponKoneksi With {
            .Hasil = hasil,
            .KunciSesi = kunciSesi,
            .Pesan = pesan,
            .IzinKontrol = izinKontrol,
            .IzinTransferBerkas = izinTransfer,
            .IzinClipboard = izinClipboard
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

#End Region

End Module
