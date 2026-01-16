Imports System.Windows
Imports bcomm

Public Class wpfWin_ProgressLoadingData

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        pgb_Progress.Maximum = ProgressMaximum

    End Sub

End Class
