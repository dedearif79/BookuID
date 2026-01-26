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

    ''' <summary>
    ''' Daftar codec video yang didukung oleh client.
    ''' Format: ["JPEG", "H264"] atau kosong untuk JPEG saja (backward compatible).
    ''' </summary>
    Public Property SupportedCodecs As String() = {"JPEG", "H264"}

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

    ''' <summary>
    ''' Codec video yang dipilih oleh Host.
    ''' "JPEG" (default) atau "H264".
    ''' </summary>
    Public Property SelectedCodec As String = "JPEG"

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

#Region "Relay Payload Classes - Fase 4"

''' <summary>
''' Payload untuk RELAY_REGISTER_HOST (Host → Relay).
''' Host mendaftarkan diri ke relay server.
''' </summary>
Public Class cls_PayloadRegisterHost

    ''' <summary>Nama perangkat Host</summary>
    Public Property NamaPerangkat As String = ""

    ''' <summary>Versi protokol</summary>
    Public Property VersiProtokol As String = VERSI_PROTOKOL

    ''' <summary>Password opsional untuk koneksi ke Host ini</summary>
    Public Property Password As String = ""

End Class

''' <summary>
''' Payload untuk RELAY_REGISTER_HOST_OK (Relay → Host).
''' Konfirmasi registrasi berhasil dengan HostCode.
''' </summary>
Public Class cls_PayloadRegisterHostOK

    ''' <summary>HostCode 6 karakter untuk identifikasi</summary>
    Public Property HostCode As String = ""

    ''' <summary>Waktu expired dalam menit</summary>
    Public Property ExpiryMinutes As Integer = 60

    ''' <summary>Pesan dari relay server</summary>
    Public Property Pesan As String = ""

End Class

''' <summary>
''' Payload untuk RELAY_QUERY_HOST (Tamu → Relay).
''' Tamu mencari Host berdasarkan HostCode.
''' </summary>
Public Class cls_PayloadQueryHost

    ''' <summary>HostCode yang dicari</summary>
    Public Property HostCode As String = ""

End Class

''' <summary>
''' Payload untuk RELAY_QUERY_HOST_RESULT (Relay → Tamu).
''' Hasil pencarian Host.
''' </summary>
Public Class cls_PayloadQueryHostResult

    ''' <summary>True jika Host ditemukan</summary>
    Public Property Found As Boolean = False

    ''' <summary>Nama Host (jika ditemukan)</summary>
    Public Property NamaHost As String = ""

    ''' <summary>True jika Host memerlukan password</summary>
    Public Property RequiresPassword As Boolean = False

    ''' <summary>Pesan dari relay server</summary>
    Public Property Pesan As String = ""

End Class

''' <summary>
''' Payload untuk RELAY_CONNECT_REQUEST (Tamu → Relay).
''' Tamu meminta koneksi ke Host via relay.
''' </summary>
Public Class cls_PayloadRelayConnectRequest

    ''' <summary>HostCode tujuan</summary>
    Public Property HostCode As String = ""

    ''' <summary>Nama perangkat Tamu</summary>
    Public Property NamaPerangkat As String = ""

    ''' <summary>Alamat IP Tamu</summary>
    Public Property AlamatIP As String = ""

    ''' <summary>Versi protokol</summary>
    Public Property VersiProtokol As String = VERSI_PROTOKOL

    ''' <summary>Password (jika Host memerlukan)</summary>
    Public Property Password As String = ""

    ''' <summary>
    ''' Daftar codec video yang didukung oleh client.
    ''' Format: ["JPEG", "H264"]
    ''' </summary>
    Public Property SupportedCodecs As String() = {"JPEG", "H264"}

End Class

''' <summary>
''' Payload untuk RELAY_ERROR (Relay → Client).
''' Pesan error dari relay server.
''' </summary>
Public Class cls_PayloadRelayError

    ''' <summary>Kode error (55=generic, 56=offline, 57=invalid code)</summary>
    Public Property KodeError As Integer = 0

    ''' <summary>Pesan error</summary>
    Public Property Pesan As String = ""

End Class

#End Region
