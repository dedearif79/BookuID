Imports System.IO
Imports bcomm

Public Class wpfUsc_Aplikasi

    ' Path ke folder aplikasi
    Private ReadOnly PathBooku As String = Path.Combine(FolderRootBookuID, "Booku", "Booku.exe")
    Private ReadOnly PathBookuRemote As String = Path.Combine(FolderRootBookuID, "Booku Remote", "Booku Remote.exe")

    Sub New()
        InitializeComponent()
    End Sub

    Private Sub wpfUsc_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Cek ketersediaan aplikasi saat UserControl dimuat
        CekKetersediaanAplikasi()
    End Sub

    Private Sub CekKetersediaanAplikasi()
        ' Cek apakah Booku.exe ada
        btn_JalankanBooku.IsEnabled = File.Exists(PathBooku)

        ' Cek apakah Booku Remote.exe ada
        btn_JalankanBookuRemote.IsEnabled = File.Exists(PathBookuRemote)
    End Sub

#Region "Tombol Jalankan Aplikasi"

    Private Sub btn_JalankanBooku_Click(sender As Object, e As RoutedEventArgs) Handles btn_JalankanBooku.Click
        If File.Exists(PathBooku) Then
            JalankanAplikasi(PathBooku)
        Else
            MessageBox.Show("Aplikasi Booku tidak ditemukan di:" & Environment.NewLine & PathBooku,
                           "Aplikasi Tidak Ditemukan",
                           MessageBoxButton.OK,
                           MessageBoxImage.Warning)
        End If
    End Sub

    Private Sub btn_JalankanBookuRemote_Click(sender As Object, e As RoutedEventArgs) Handles btn_JalankanBookuRemote.Click
        If File.Exists(PathBookuRemote) Then
            JalankanAplikasi(PathBookuRemote)
        Else
            MessageBox.Show("Aplikasi Booku Remote tidak ditemukan di:" & Environment.NewLine & PathBookuRemote,
                           "Aplikasi Tidak Ditemukan",
                           MessageBoxButton.OK,
                           MessageBoxImage.Warning)
        End If
    End Sub

#End Region

End Class
