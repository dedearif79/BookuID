Option Explicit On
Option Strict On

Imports System.Net

''' <summary>
''' Modul berisi variabel dan konstanta umum untuk Booku Remote.
''' </summary>
Public Module mdl_VariabelUmum

#Region "Konstanta Port Default (Backward Compatibility)"

    ''' <summary>Port UDP default untuk discovery perangkat di LAN</summary>
    Public Const PORT_DISCOVERY As Integer = cls_SetelPort.DEFAULT_PORT_DISCOVERY

    ''' <summary>Port TCP default untuk koneksi remote LAN</summary>
    Public Const PORT_KONEKSI As Integer = cls_SetelPort.DEFAULT_PORT_KONEKSI

    ''' <summary>Port TCP default untuk relay server (internet)</summary>
    Public Const PORT_RELAY As Integer = cls_SetelPort.DEFAULT_PORT_RELAY

    ''' <summary>Magic string untuk identifikasi broadcast discovery</summary>
    Public Const MAGIC_DISCOVERY As String = "BOOKU_REMOTE_DISCOVERY"

    ''' <summary>Versi protokol untuk kompatibilitas</summary>
    Public Const VERSI_PROTOKOL As String = "1.0"

#End Region

#Region "Konstanta Relay Server Default (Backward Compatibility)"

    ''' <summary>Alamat IP default relay server</summary>
    Public Const RELAY_SERVER_IP As String = cls_SetelPort.DEFAULT_RELAY_SERVER_IP

    ''' <summary>Alamat lengkap default relay server</summary>
    Public ReadOnly RELAY_SERVER_ADDRESS As String = $"{RELAY_SERVER_IP}:{PORT_RELAY}"

    ''' <summary>Interval heartbeat ke relay server (milidetik)</summary>
    Public Const INTERVAL_HEARTBEAT_RELAY As Integer = 30000

#End Region

#Region "Pengaturan Port Aktif (Configurable)"

    ''' <summary>
    ''' Instance pengaturan port yang sedang aktif.
    ''' Dimuat dari file saat aplikasi dimulai, bisa diubah via UI.
    ''' Gunakan ini untuk semua operasi jaringan (bukan konstanta).
    ''' </summary>
    Public SetelPortAktif As cls_SetelPort = Nothing

    ''' <summary>
    ''' Memuat pengaturan port dari file. Panggil saat aplikasi dimulai.
    ''' </summary>
    Public Sub MuatSetelPort()
        SetelPortAktif = cls_SetelPort.MuatDariFile()
    End Sub

    ''' <summary>
    ''' Menyimpan pengaturan port ke file.
    ''' </summary>
    ''' <returns>True jika berhasil</returns>
    Public Function SimpanSetelPort() As Boolean
        If SetelPortAktif Is Nothing Then
            SetelPortAktif = New cls_SetelPort()
        End If
        Return SetelPortAktif.SimpanKeFile()
    End Function

    ''' <summary>
    ''' Mendapatkan port discovery aktif. Fallback ke default jika SetelPortAktif belum dimuat.
    ''' </summary>
    Public ReadOnly Property PortDiscoveryAktif As Integer
        Get
            If SetelPortAktif Is Nothing Then Return PORT_DISCOVERY
            Return SetelPortAktif.PortDiscovery
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan port koneksi TCP aktif. Fallback ke default jika SetelPortAktif belum dimuat.
    ''' </summary>
    Public ReadOnly Property PortKoneksiAktif As Integer
        Get
            If SetelPortAktif Is Nothing Then Return PORT_KONEKSI
            Return SetelPortAktif.PortKoneksi
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan port relay aktif. Fallback ke default jika SetelPortAktif belum dimuat.
    ''' </summary>
    Public ReadOnly Property PortRelayAktif As Integer
        Get
            If SetelPortAktif Is Nothing Then Return PORT_RELAY
            Return SetelPortAktif.PortRelay
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan IP relay server aktif. Fallback ke default jika SetelPortAktif belum dimuat.
    ''' </summary>
    Public ReadOnly Property RelayServerIPAktif As String
        Get
            If SetelPortAktif Is Nothing Then Return RELAY_SERVER_IP
            Return SetelPortAktif.RelayServerIP
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan alamat lengkap relay server aktif (IP:Port).
    ''' </summary>
    Public ReadOnly Property RelayServerAddressAktif As String
        Get
            Return $"{RelayServerIPAktif}:{PortRelayAktif}"
        End Get
    End Property

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

        ' Relay / Internet (40-59) - Fase 4
        ''' <summary>Host mendaftar ke relay server</summary>
        RELAY_REGISTER_HOST = 40
        ''' <summary>Relay response dengan HostCode</summary>
        RELAY_REGISTER_HOST_OK = 41
        ''' <summary>Host logout dari relay</summary>
        RELAY_UNREGISTER_HOST = 42
        ''' <summary>Host heartbeat ke relay</summary>
        RELAY_HOST_HEARTBEAT = 43
        ''' <summary>Tamu query host by HostCode</summary>
        RELAY_QUERY_HOST = 45
        ''' <summary>Relay response info host</summary>
        RELAY_QUERY_HOST_RESULT = 46
        ''' <summary>Tamu minta koneksi via relay</summary>
        RELAY_CONNECT_REQUEST = 47
        ''' <summary>Notify session dimulai</summary>
        RELAY_SESSION_STARTED = 52
        ''' <summary>Notify session berakhir</summary>
        RELAY_SESSION_ENDED = 53
        ''' <summary>Generic relay error</summary>
        RELAY_ERROR = 55
        ''' <summary>Host tidak online</summary>
        RELAY_HOST_OFFLINE = 56
        ''' <summary>HostCode tidak valid</summary>
        RELAY_INVALID_CODE = 57
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

    ''' <summary>
    ''' Mode koneksi: LAN atau Internet (via Relay)
    ''' </summary>
    Public Enum ModeKoneksi
        ''' <summary>Koneksi langsung dalam jaringan LAN</summary>
        LAN = 1
        ''' <summary>Koneksi via relay server (internet)</summary>
        INTERNET = 2
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

    ''' <summary>Mode koneksi saat ini (LAN atau Internet)</summary>
    Public ModeKoneksiSaatIni As ModeKoneksi = ModeKoneksi.LAN

    ''' <summary>HostCode untuk mode Internet (6 karakter)</summary>
    Public HostCodeAktif As String = ""

    ''' <summary>Flag apakah terhubung ke relay server</summary>
    Public TerhubungKeRelay As Boolean = False

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

        ' Muat pengaturan port dari file
        MuatSetelPort()
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
