Imports System.Windows

Public Class wpfUsc_WebBrowser

    Public StatusAktif As Boolean
    Public AlamatURL As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        StatusAktif = True
        web_Browser.Source = New Uri(AlamatURL)
    End Sub

    Sub New()
        InitializeComponent()
    End Sub


    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub


End Class
