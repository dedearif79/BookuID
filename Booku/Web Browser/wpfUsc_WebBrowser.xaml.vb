Imports System.Windows

Public Class wpfUsc_WebBrowser

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Public AlamatURL As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True
        web_Browser.Source = New Uri(AlamatURL)
        SudahDimuat = True
    End Sub

    Sub New()
        InitializeComponent()
    End Sub


    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub


End Class
