Option Explicit On
Option Strict On

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
        ' Tampilkan info perangkat
        lbl_NamaPerangkat.Text = NamaPerangkatIni
        lbl_AlamatIP.Text = AlamatIPLokal
    End Sub

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
