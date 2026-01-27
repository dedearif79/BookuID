Option Explicit On
Option Strict On

Imports System.Threading

''' <summary>
''' Kelas untuk manajemen state sesi remote streaming.
''' </summary>
Public Class cls_SesiRemote

#Region "Enum Status Streaming"

    ''' <summary>
    ''' Status streaming layar.
    ''' </summary>
    Public Enum StatusStreaming
        TIDAK_AKTIF = 0
        MENUNGGU = 1
        AKTIF = 2
        DIJEDA = 3
        DIHENTIKAN = 4
    End Enum

#End Region

#Region "Properties - Sesi"

    ''' <summary>ID sesi (kunci sesi)</summary>
    Public Property IdSesi As String = ""

    ''' <summary>Mode aplikasi (Host atau Tamu)</summary>
    Public Property Mode As ModeAplikasi = ModeAplikasi.TIDAK_ADA

    ''' <summary>Waktu mulai sesi (UTC)</summary>
    Public Property WaktuMulai As DateTime = DateTime.MinValue

    ''' <summary>Nama perangkat peer (Host atau Tamu)</summary>
    Public Property NamaPeer As String = ""

    ''' <summary>Alamat IP peer</summary>
    Public Property AlamatIPPeer As String = ""

#End Region

#Region "Properties - Streaming"

    ''' <summary>Status streaming saat ini</summary>
    Public Property StatusStreamingSaatIni As StatusStreaming = StatusStreaming.TIDAK_AKTIF

    ''' <summary>Frame rate target (FPS)</summary>
    Public Property TargetFPS As Integer = 15

    ''' <summary>Skala gambar (0.0 - 1.0)</summary>
    Public Property SkalaGambar As Double = 0.6

    ''' <summary>Nomor frame terakhir yang dikirim/diterima</summary>
    Public Property NomorFrameTerakhir As Long = 0

    ''' <summary>Jumlah total frame yang dikirim/diterima</summary>
    Public Property TotalFrame As Long = 0

#End Region

#Region "Properties - Statistik"

    ''' <summary>FPS aktual</summary>
    Public Property FPSAktual As Double = 0

    ''' <summary>Latency terakhir (ms)</summary>
    Public Property LatencyMs As Double = 0

    ''' <summary>Ukuran frame terakhir (KB)</summary>
    Public Property UkuranFrameKB As Double = 0

    ''' <summary>Total data yang ditransfer (KB)</summary>
    Public Property TotalDataKB As Double = 0

    ''' <summary>Timestamp frame terakhir</summary>
    Public Property TimestampFrameTerakhir As DateTime = DateTime.MinValue

#End Region

#Region "Properties - Izin (dari respon koneksi)"

    ''' <summary>Izin kontrol mouse/keyboard</summary>
    Public Property IzinKontrol As Boolean = True

    ''' <summary>Izin transfer berkas</summary>
    Public Property IzinTransferBerkas As Boolean = False

    ''' <summary>Izin clipboard</summary>
    Public Property IzinClipboard As Boolean = False

#End Region

#Region "Private Variables"

    Private _cancellationTokenSource As CancellationTokenSource
    Private _fpsCounter As Integer = 0
    Private _lastFPSCalculation As DateTime = DateTime.Now

#End Region

#Region "Constructor"

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Konstruktor dengan parameter sesi.
    ''' </summary>
    Public Sub New(idSesi As String, mode As ModeAplikasi, namaPeer As String, alamatIPPeer As String)
        Me.IdSesi = idSesi
        Me.Mode = mode
        Me.NamaPeer = namaPeer
        Me.AlamatIPPeer = alamatIPPeer
        Me.WaktuMulai = DateTime.UtcNow
    End Sub

#End Region

#Region "Methods - Streaming Control"

    ''' <summary>
    ''' Mulai streaming (dipanggil oleh Host saat menerima permintaan streaming).
    ''' </summary>
    Public Sub MulaiStreaming()
        StatusStreamingSaatIni = StatusStreaming.AKTIF
        _cancellationTokenSource = New CancellationTokenSource()
        TotalFrame = 0
        NomorFrameTerakhir = 0
        TotalDataKB = 0
        _lastFPSCalculation = DateTime.Now
        _fpsCounter = 0
    End Sub

    ''' <summary>
    ''' Jeda streaming.
    ''' </summary>
    Public Sub JedaStreaming()
        StatusStreamingSaatIni = StatusStreaming.DIJEDA
    End Sub

    ''' <summary>
    ''' Lanjutkan streaming setelah jeda.
    ''' </summary>
    Public Sub LanjutkanStreaming()
        If StatusStreamingSaatIni = StatusStreaming.DIJEDA Then
            StatusStreamingSaatIni = StatusStreaming.AKTIF
        End If
    End Sub

    ''' <summary>
    ''' Hentikan streaming.
    ''' </summary>
    Public Sub HentikanStreaming()
        StatusStreamingSaatIni = StatusStreaming.DIHENTIKAN
        Try
            _cancellationTokenSource?.Cancel()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Mendapatkan CancellationToken untuk streaming loop.
    ''' </summary>
    Public Function DapatkanCancellationToken() As CancellationToken
        If _cancellationTokenSource Is Nothing Then
            _cancellationTokenSource = New CancellationTokenSource()
        End If
        Return _cancellationTokenSource.Token
    End Function

#End Region

#Region "Methods - Statistik"

    ''' <summary>
    ''' Catat frame yang dikirim/diterima.
    ''' </summary>
    Public Sub CatatFrame(nomorFrame As Long, ukuranKB As Double)
        NomorFrameTerakhir = nomorFrame
        TotalFrame += 1
        UkuranFrameKB = ukuranKB
        TotalDataKB += ukuranKB
        TimestampFrameTerakhir = DateTime.Now

        ' Hitung FPS aktual
        _fpsCounter += 1
        Dim elapsed = (DateTime.Now - _lastFPSCalculation).TotalSeconds
        If elapsed >= 1.0 Then
            FPSAktual = Math.Round(_fpsCounter / elapsed, 1)
            _fpsCounter = 0
            _lastFPSCalculation = DateTime.Now
        End If
    End Sub

    ''' <summary>
    ''' Catat latency dari timestamp frame.
    ''' </summary>
    Public Sub CatatLatency(timestampFrame As Long)
        Dim waktuKirim = New DateTime(timestampFrame, DateTimeKind.Utc)
        LatencyMs = Math.Round((DateTime.UtcNow - waktuKirim).TotalMilliseconds, 0)
    End Sub

    ''' <summary>
    ''' Mendapatkan durasi sesi.
    ''' </summary>
    Public Function DurasiSesi() As TimeSpan
        If WaktuMulai = DateTime.MinValue Then Return TimeSpan.Zero
        Return DateTime.UtcNow - WaktuMulai
    End Function

    ''' <summary>
    ''' Mendapatkan string durasi sesi yang mudah dibaca.
    ''' </summary>
    Public Function DurasiSesiString() As String
        Dim durasi = DurasiSesi()
        Return $"{CInt(durasi.TotalHours):00}:{durasi.Minutes:00}:{durasi.Seconds:00}"
    End Function

#End Region

#Region "Methods - Status Check"

    ''' <summary>
    ''' Apakah streaming sedang aktif?
    ''' </summary>
    Public Function IsStreamingAktif() As Boolean
        Return StatusStreamingSaatIni = StatusStreaming.AKTIF
    End Function

    ''' <summary>
    ''' Apakah sesi valid (terhubung)?
    ''' </summary>
    Public Function IsSesiValid() As Boolean
        Return Not String.IsNullOrEmpty(IdSesi)
    End Function

    ''' <summary>
    ''' Mendapatkan interval delay untuk target FPS (dalam milidetik).
    ''' </summary>
    Public Function IntervalDelayMs() As Integer
        If TargetFPS <= 0 Then Return 67 ' Default ~15 FPS
        Return CInt(Math.Round(1000.0 / TargetFPS))
    End Function

#End Region

#Region "Methods - Reset"

    ''' <summary>
    ''' Reset sesi ke kondisi awal.
    ''' </summary>
    Public Sub Reset()
        IdSesi = ""
        Mode = ModeAplikasi.TIDAK_ADA
        WaktuMulai = DateTime.MinValue
        NamaPeer = ""
        AlamatIPPeer = ""
        StatusStreamingSaatIni = StatusStreaming.TIDAK_AKTIF
        NomorFrameTerakhir = 0
        TotalFrame = 0
        FPSAktual = 0
        LatencyMs = 0
        UkuranFrameKB = 0
        TotalDataKB = 0

        Try
            _cancellationTokenSource?.Cancel()
            _cancellationTokenSource?.Dispose()
        Catch
        End Try
        _cancellationTokenSource = Nothing
    End Sub

#End Region

End Class
