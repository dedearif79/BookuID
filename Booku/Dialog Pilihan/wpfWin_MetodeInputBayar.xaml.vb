Imports System.Windows
Imports bcomm

Public Class wpfWin_MetodeInputBayar

    Public DenganPengajuan As Boolean
    Public LanjutkanProses As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LanjutkanProses = False
        rdb_DenganPengajuan.IsEnabled = True
        rdb_TanpaPengajuan.IsEnabled = True
        rdb_TanpaPengajuan.IsChecked = True
        If LevelUserAktif < LevelUser_99_AppDeveloper Then rdb_DenganPengajuan.IsEnabled = False
    End Sub

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_Lanjutkan.Click
        If rdb_DenganPengajuan.IsChecked = True Then DenganPengajuan = True
        If rdb_TanpaPengajuan.IsChecked = True Then DenganPengajuan = False
        LanjutkanProses = True
        Me.Close()
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        LanjutkanProses = False
        Me.Close()
    End Sub

End Class
