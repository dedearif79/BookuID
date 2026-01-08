Imports System.Windows
Imports System.Windows.Media.Animation

Public Class wpfWin_Loading

    Public StatusAktif As Boolean

    Sub New()
        InitializeComponent()
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        StatusAktif = True
        Dim sb = TryCast(Resources("SpinStoryboard"), Storyboard)
        If sb IsNot Nothing Then sb.Begin()
    End Sub

End Class
