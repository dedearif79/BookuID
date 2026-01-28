Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows
Imports BookuID.Styles

''' <summary>
''' Window utama Booku Remote - Menu pilih mode Host atau Tamu.
''' </summary>
Class wpfWin_StartUp

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        InisialisasiVariabelUmum()
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Cek ketersediaan FFmpeg terlebih dahulu
        If Not CekDanDownloadFFmpeg() Then
            ' User menolak download atau download gagal, keluar dari aplikasi
            Application.Current.Shutdown()
            Return
        End If

        ' Tampilkan info perangkat
        lbl_NamaPerangkat.Text = NamaPerangkatIni
        lbl_AlamatIP.Text = AlamatIPLokal
    End Sub

    ''' <summary>
    ''' Mengecek ketersediaan FFmpeg dan menawarkan download jika tidak ada.
    ''' </summary>
    ''' <returns>True jika FFmpeg tersedia atau berhasil didownload, False jika tidak</returns>
    Private Function CekDanDownloadFFmpeg() As Boolean
        ' Cek apakah ffmpeg.exe ada di folder aplikasi
        Dim folderAplikasi = AppDomain.CurrentDomain.BaseDirectory
        Dim pathFFmpeg = Path.Combine(folderAplikasi, "ffmpeg.exe")

        If File.Exists(pathFFmpeg) Then
            ' FFmpeg sudah ada
            Return True
        End If

        ' FFmpeg tidak ada, tampilkan konfirmasi
        Dim hasil = MessageBox.Show(
            "Untuk menjalankan aplikasi Booku Remote, diperlukan file pendukung (FFmpeg) yang harus diunduh dari server." & Environment.NewLine & Environment.NewLine &
            "Ukuran file: sekitar 140 MB" & Environment.NewLine &
            "File ini diperlukan untuk fitur streaming video." & Environment.NewLine & Environment.NewLine &
            "Apakah Anda ingin mengunduh file tersebut sekarang?",
            "Download File Pendukung",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question)

        If hasil = MessageBoxResult.No Then
            ' User menolak download
            MessageBox.Show(
                "Anda tidak dapat menggunakan aplikasi Booku Remote untuk saat ini karena file pendukung belum tersedia." & Environment.NewLine & Environment.NewLine &
                "Silakan jalankan aplikasi kembali jika ingin mengunduh file tersebut.",
                "Booku Remote",
                MessageBoxButton.OK,
                MessageBoxImage.Information)
            Return False
        End If

        ' User setuju download, tampilkan window download
        Dim winDownload As New wpfWin_DownloadFFmpeg()
        winDownload.ShowDialog()

        ' Cek hasil download
        If winDownload.DownloadBerhasil Then
            ' Verifikasi ulang file ada
            If File.Exists(pathFFmpeg) Then
                Return True
            End If
        End If

        ' Download gagal
        MessageBox.Show(
            "Download file pendukung gagal. Silakan coba lagi nanti." & Environment.NewLine & Environment.NewLine &
            "Pastikan koneksi internet Anda stabil.",
            "Download Gagal",
            MessageBoxButton.OK,
            MessageBoxImage.Warning)
        Return False
    End Function

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Cleanup saat window ditutup
        Try
            mdl_PenemuanPerangkat.HentikanDiscovery()
            mdl_KoneksiJaringan.HentikanServer()
        Catch
        End Try
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_AktifkanHost_Click(sender As Object, e As RoutedEventArgs) Handles btn_AktifkanHost.Click
        ' Set mode aplikasi
        ModeAplikasiSaatIni = ModeAplikasi.HOST

        ' Inisialisasi file log untuk Host
        InitLogFile("Host")

        ' Buka window Mode Host
        Dim winHost As New wpfWin_ModeHost()
        winHost.Owner = Me
        Me.Hide()
        winHost.ShowDialog()
        Me.Show()

        ' Reset mode setelah kembali
        ModeAplikasiSaatIni = ModeAplikasi.TIDAK_ADA
    End Sub

    Private Sub btn_CariPerangkat_Click(sender As Object, e As RoutedEventArgs) Handles btn_CariPerangkat.Click
        ' Set mode aplikasi
        ModeAplikasiSaatIni = ModeAplikasi.TAMU

        ' Inisialisasi file log untuk Tamu
        InitLogFile("Tamu")

        ' Buka window Mode Tamu
        Dim winTamu As New wpfWin_ModeTamu()
        winTamu.Owner = Me
        Me.Hide()
        winTamu.ShowDialog()
        Me.Show()

        ' Reset mode setelah kembali
        ModeAplikasiSaatIni = ModeAplikasi.TIDAK_ADA
    End Sub

#End Region

End Class
