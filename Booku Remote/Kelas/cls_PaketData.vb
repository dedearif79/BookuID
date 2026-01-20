Option Explicit On
Option Strict On

Imports System.Text

''' <summary>
''' Kelas untuk struktur paket data yang dikirim melalui jaringan.
''' </summary>
Public Class cls_PaketData

#Region "Properties"

    ''' <summary>Tipe paket</summary>
    Public Property TipePaket As TipePaket

    ''' <summary>ID sesi (untuk koneksi yang sudah established)</summary>
    Public Property IdSesi As String = ""

    ''' <summary>Timestamp pengiriman (ticks)</summary>
    Public Property Timestamp As Long = DateTime.UtcNow.Ticks

    ''' <summary>Data payload (dalam bentuk string JSON atau Base64)</summary>
    Public Property Payload As String = ""

    ''' <summary>Checksum untuk validasi integritas</summary>
    Public Property Checksum As String = ""

#End Region

#Region "Constructor"

    Public Sub New()
    End Sub

    Public Sub New(tipePaket As TipePaket, Optional payload As String = "")
        Me.TipePaket = tipePaket
        Me.Payload = payload
        Me.Timestamp = DateTime.UtcNow.Ticks
        Me.Checksum = HitungChecksum()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Menghitung checksum dari paket.
    ''' </summary>
    Public Function HitungChecksum() As String
        Dim data = $"{CInt(TipePaket)}|{IdSesi}|{Timestamp}|{Payload}"
        Using md5 = System.Security.Cryptography.MD5.Create()
            Dim hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data))
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    ''' <summary>
    ''' Validasi checksum paket.
    ''' </summary>
    Public Function ValidasiChecksum() As Boolean
        Return Checksum = HitungChecksum()
    End Function

#End Region

End Class

''' <summary>
''' Kelas untuk payload permintaan koneksi.
''' </summary>
Public Class cls_PayloadPermintaanKoneksi

    ''' <summary>Nama perangkat yang meminta koneksi</summary>
    Public Property NamaPerangkat As String = ""

    ''' <summary>Alamat IP perangkat</summary>
    Public Property AlamatIP As String = ""

    ''' <summary>Versi protokol</summary>
    Public Property VersiProtokol As String = VERSI_PROTOKOL

End Class

''' <summary>
''' Kelas untuk payload respon koneksi.
''' </summary>
Public Class cls_PayloadResponKoneksi

    ''' <summary>Hasil persetujuan</summary>
    Public Property Hasil As HasilPersetujuan = HasilPersetujuan.MENUNGGU

    ''' <summary>Kunci sesi untuk enkripsi (jika diterima)</summary>
    Public Property KunciSesi As String = ""

    ''' <summary>Pesan (alasan penolakan jika ditolak)</summary>
    Public Property Pesan As String = ""

    ''' <summary>Izin yang diberikan</summary>
    Public Property IzinKontrol As Boolean = True
    Public Property IzinTransferBerkas As Boolean = False
    Public Property IzinClipboard As Boolean = False

End Class

''' <summary>
''' Kelas untuk payload input keyboard dari Tamu ke Host. Fase 2b.
''' </summary>
Public Class cls_PayloadInputKeyboard

    ''' <summary>Virtual key code (VK_*)</summary>
    Public Property KeyCode As Integer = 0

    ''' <summary>True untuk key down, False untuk key up</summary>
    Public Property IsKeyDown As Boolean = True

    ''' <summary>True jika extended key (arrows, numpad, Insert, Delete, dll)</summary>
    Public Property IsExtended As Boolean = False

    ''' <summary>Modifier flags (untuk Ctrl, Alt, Shift)</summary>
    Public Property Modifiers As Integer = 0

End Class

''' <summary>
''' Kelas untuk payload input mouse dari Tamu ke Host. Fase 2b.
''' </summary>
Public Class cls_PayloadInputMouse

    ''' <summary>Tipe aksi mouse (PINDAH, KLIK, RODA)</summary>
    Public Property TipeAksi As TipeAksiMouse = TipeAksiMouse.PINDAH

    ''' <summary>Posisi X (normalized 0.0 - 1.0)</summary>
    Public Property X As Double = 0.0

    ''' <summary>Posisi Y (normalized 0.0 - 1.0)</summary>
    Public Property Y As Double = 0.0

    ''' <summary>Tombol mouse: 0=none, 1=left, 2=right, 3=middle, 4=XButton1, 5=XButton2</summary>
    Public Property Button As Integer = 0

    ''' <summary>True untuk button down, False untuk button up</summary>
    Public Property IsButtonDown As Boolean = False

    ''' <summary>Delta scroll wheel (positive=up, negative=down). Standard: 120 per notch.</summary>
    Public Property WheelDelta As Integer = 0

End Class
