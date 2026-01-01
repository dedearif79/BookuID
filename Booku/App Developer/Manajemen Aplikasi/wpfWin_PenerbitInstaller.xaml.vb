Imports System.Windows
Imports bcomm

Public Class wpfWin_PenerbitInstaller

    Public VersiApp
    Public ApdetApp
    Public urlPaketBooku
    Public urlPaketInstaller
    Public NamaFolderTempPaketBooku
    Public NamaFolderTempPaketInstaller
    Public NamaFileZipPaketBooku
    Public NamaFileZipPaketInstaller
    Public NamaFileExeInstaller


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True


        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()
        VersiApp = 0
        ApdetApp = 0
        urlPaketBooku = Kosongan
        urlPaketInstaller = Kosongan
        NamaFolderTempPaketBooku = Kosongan
        NamaFolderTempPaketInstaller = Kosongan
        NamaFileZipPaketBooku = Kosongan
        NamaFileZipPaketInstaller = Kosongan
        NamaFileExeInstaller = Kosongan
    End Sub


End Class
