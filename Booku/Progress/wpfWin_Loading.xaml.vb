Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Media.Animation
Imports bcomm

Public Class wpfWin_Loading

    Public StatusAktif As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ShowLoading("Sedang Proses")
    End Sub


    Private Sub ShowLoading(message As String)
        StatusAktif = True
        txtLoading.Text = message
        LoadingOverlay.Visibility = Visibility.Visible

        ' Pastikan overlay bisa menangkap input
        LoadingOverlay.Focus()

        Dim sb = CType(LoadingOverlay.Resources("SpinStoryboard"), Storyboard)
        sb.Begin()
    End Sub

    Sub HideLoading()
        Dim sb = CType(LoadingOverlay.Resources("SpinStoryboard"), Storyboard)
        sb.Stop()

        LoadingOverlay.Visibility = Visibility.Collapsed
        StatusAktif = False
        Close()
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        WindowStyle = WindowStyle.None
        WindowStartupLocation = WindowStartupLocation.CenterOwner
    End Sub

End Class
