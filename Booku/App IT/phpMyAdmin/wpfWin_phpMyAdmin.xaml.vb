Imports System.Windows

Public Class wpfWin_phpMyAdmin

    Public Property JudulForm As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        JudulForm = "phpMyAdmin"
        Title = JudulForm

        ' Buat instance baru wpfUsc_WebBrowser
        usc_phpMyAdmin = New wpfUsc_WebBrowser
        usc_phpMyAdmin.AlamatURL = urlPhpMyAdmin

        ' Tambahkan ke konten window
        pnl_Konten.Children.Add(usc_phpMyAdmin)

    End Sub

End Class
