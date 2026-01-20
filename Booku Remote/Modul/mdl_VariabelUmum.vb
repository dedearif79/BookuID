Option Explicit On
Option Strict On

Imports System.Net

''' <summary>
''' Modul berisi variabel dan konstanta umum untuk Booku Remote.
''' </summary>
Public Module mdl_VariabelUmum

#Region "Konstanta Port Jaringan"

    ''' <summary>Port UDP untuk discovery perangkat di LAN</summary>
    Public Const PORT_DISCOVERY As Integer = 45678

    ''' <summary>Port TCP untuk koneksi remote</summary>
    Public Const PORT_KONEKSI As Integer = 45679

    ''' <summary>Magic string untuk identifikasi broadcast discovery</summary>
    Public Const MAGIC_DISCOVERY As String = "BOOKU_REMOTE_DISCOVERY"

    ''' <summary>Versi protokol untuk kompatibilitas</summary>
    Public Const VERSI_PROTOKOL As String = "1.0"

#End Region

#Region "Konstanta Timeout"

    ''' <summary>Timeout untuk discovery response (milidetik)</summary>
    Public Const TIMEOUT_DISCOVERY As Integer = 3000

    ''' <summary>Timeout untuk koneksi TCP (milidetik)</summary>
    Public Const TIMEOUT_KONEKSI As Integer = 10000

    ''' <summary>Interval heartbeat (milidetik)</summary>
    Public Const INTERVAL_HEARTBEAT As Integer = 5000

    ''' <summary>Timeout dialog persetujuan (detik)</summary>
    Public Const TIMEOUT_PERSETUJUAN As Integer = 30

#End Region

#Region "Enum Tipe Paket"

    ''' <summary>
    ''' Tipe-tipe paket yang digunakan dalam protokol komunikasi.
    ''' </summary>
    Public Enum TipePaket
        ' Discovery (1-9)
        BROADCAST_DISCOVERY = 1
        RESPON_DISCOVERY = 2

        ' Koneksi (10-19)
        PERMINTAAN_KONEKSI = 10
        RESPON_KONEKSI = 11
        TUTUP_KONEKSI = 12
        HEARTBEAT = 13

        ' Remote Desktop (20-29) - Fase 2
        FRAME_LAYAR = 20
        INPUT_KEYBOARD = 21
        INPUT_MOUSE = 22
        CLIPBOARD_DATA = 23
        PERMINTAAN_STREAMING = 24
        HENTIKAN_STREAMING = 25

        ' Transfer Berkas (30-39) - Fase 3
        PERMINTAAN_BERKAS = 30
        DATA_BERKAS = 31
        KONFIRMASI_BERKAS = 32
        DAFTAR_FOLDER = 33
    End Enum

#End Region

#Region "Enum Status"

    ''' <summary>
    ''' Status perangkat Host.
    ''' </summary>
    Public Enum StatusPerangkat
        TERSEDIA = 1
        SIBUK = 2
        TIDAK_TERSEDIA = 3
    End Enum

    ''' <summary>
    ''' Status koneksi.
    ''' </summary>
    Public Enum StatusKoneksi
        TIDAK_TERHUBUNG = 0
        MENUNGGU_PERSETUJUAN = 1
        TERHUBUNG = 2
        TERPUTUS = 3
    End Enum

    ''' <summary>
    ''' Hasil persetujuan koneksi.
    ''' </summary>
    Public Enum HasilPersetujuan
        MENUNGGU = 0
        DITERIMA = 1
        DITOLAK = 2
        TIMEOUT = 3
    End Enum

    ''' <summary>
    ''' Mode aplikasi.
    ''' </summary>
    Public Enum ModeAplikasi
        TIDAK_ADA = 0
        HOST = 1
        TAMU = 2
    End Enum

    ''' <summary>
    ''' Tipe aksi mouse untuk input remote. Fase 2b.
    ''' </summary>
    Public Enum TipeAksiMouse
        ''' <summary>Gerakan mouse (move)</summary>
        PINDAH = 1
        ''' <summary>Klik mouse (down/up)</summary>
        KLIK = 2
        ''' <summary>Scroll wheel</summary>
        RODA = 3
    End Enum

#End Region

#Region "Variabel Global"

    ''' <summary>Mode aplikasi saat ini</summary>
    Public ModeAplikasiSaatIni As ModeAplikasi = ModeAplikasi.TIDAK_ADA

    ''' <summary>Nama perangkat ini (diambil dari Environment.MachineName)</summary>
    Public NamaPerangkatIni As String = Environment.MachineName

    ''' <summary>Alamat IP lokal perangkat ini</summary>
    Public AlamatIPLokal As String = ""

    ''' <summary>Status koneksi saat ini</summary>
    Public StatusKoneksiSaatIni As StatusKoneksi = StatusKoneksi.TIDAK_TERHUBUNG

    ''' <summary>Kunci sesi untuk enkripsi (digenerate saat koneksi dibuat)</summary>
    Public KunciSesiAktif As String = ""

    ''' <summary>Sesi remote aktif (state management streaming)</summary>
    Public SesiRemoteAktif As cls_SesiRemote = Nothing

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Mendapatkan alamat IP lokal perangkat ini.
    ''' </summary>
    Public Function DapatkanAlamatIPLokal() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    ' Ambil IP yang bukan loopback
                    If Not ip.ToString().StartsWith("127.") Then
                        Return ip.ToString()
                    End If
                End If
            Next
        Catch
        End Try
        Return "127.0.0.1"
    End Function

    ''' <summary>
    ''' Inisialisasi variabel umum saat aplikasi dimulai.
    ''' </summary>
    Public Sub InisialisasiVariabelUmum()
        AlamatIPLokal = DapatkanAlamatIPLokal()
        NamaPerangkatIni = Environment.MachineName
    End Sub

    ''' <summary>
    ''' Generate string acak untuk kunci sesi.
    ''' </summary>
    Public Function AcakKarakter(panjang As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim random As New Random()
        Dim result As New System.Text.StringBuilder(panjang)
        For i As Integer = 1 To panjang
            result.Append(chars(random.Next(chars.Length)))
        Next
        Return result.ToString()
    End Function

#End Region

End Module
