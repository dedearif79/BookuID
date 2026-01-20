Option Explicit On
Option Strict On

Imports System.Windows
Imports System.Windows.Threading
Imports BookuID.Styles

''' <summary>
''' Dialog persetujuan koneksi - ditampilkan ke Host saat Tamu mencoba menyambung.
''' </summary>
Class wpfWin_PersetujuanKoneksi

#Region "Properties"

    ''' <summary>Nama perangkat pengirim</summary>
    Public Property NamaPengirim As String = ""

    ''' <summary>Alamat IP pengirim</summary>
    Public Property AlamatIPPengirim As String = ""

    ''' <summary>Hasil: True jika diterima, False jika ditolak</summary>
    Public Property Diterima As Boolean = False

    ''' <summary>Izin kontrol layar dan input</summary>
    Public Property IzinKontrol As Boolean = True

    ''' <summary>Izin transfer berkas</summary>
    Public Property IzinTransferBerkas As Boolean = False

    ''' <summary>Izin akses clipboard</summary>
    Public Property IzinClipboard As Boolean = False

#End Region

#Region "Private Variables"

    Private _timer As DispatcherTimer
    Private _waktuTersisa As Integer = TIMEOUT_PERSETUJUAN

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Styling window
        StyleWindowDialogWPF_Dasar(Me)

        ' Tampilkan info pengirim
        run_NamaPengirim.Text = NamaPengirim
        run_AlamatIP.Text = AlamatIPPengirim

        ' Set checkbox sesuai parameter
        chk_IzinTransferBerkas.IsChecked = IzinTransferBerkas
        chk_IzinClipboard.IsChecked = IzinClipboard

        ' Mulai countdown timer
        MulaiCountdown()

        ' Play sound alert
        Try
            System.Media.SystemSounds.Exclamation.Play()
        Catch
        End Try
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Hentikan timer
        If _timer IsNot Nothing Then
            _timer.Stop()
            _timer = Nothing
        End If
    End Sub

#End Region

#Region "Countdown Timer"

    Private Sub MulaiCountdown()
        _waktuTersisa = TIMEOUT_PERSETUJUAN
        UpdateCountdownUI()

        _timer = New DispatcherTimer()
        _timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler _timer.Tick, AddressOf Timer_Tick
        _timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        _waktuTersisa -= 1
        UpdateCountdownUI()

        If _waktuTersisa <= 0 Then
            ' Timeout - auto tolak
            _timer.Stop()
            Diterima = False
            Me.Close()
        End If
    End Sub

    Private Sub UpdateCountdownUI()
        lbl_Countdown.Text = $"Waktu tersisa: {_waktuTersisa} detik"
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_Terima_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terima.Click
        _timer?.Stop()

        ' Ambil nilai izin
        IzinKontrol = chk_IzinKontrol.IsChecked.GetValueOrDefault()
        IzinTransferBerkas = chk_IzinTransferBerkas.IsChecked.GetValueOrDefault()
        IzinClipboard = chk_IzinClipboard.IsChecked.GetValueOrDefault()

        Diterima = True
        Me.Close()
    End Sub

    Private Sub btn_Tolak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tolak.Click
        _timer?.Stop()
        Diterima = False
        Me.Close()
    End Sub

#End Region

End Class
