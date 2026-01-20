Imports System.IO
Imports System.Windows
Imports bcomm

Public Class wpfWin_PenerbitUpdater

    Public VersiApp
    Public ApdetApp
    Public urlPaketBooku
    Public urlPaketUpdater
    Public NamaFolderTempPaketBooku
    Public NamaFolderTempPaketUpdater
    Public NamaFileZipPaketBooku
    Public NamaFileZipPaketUpdater
    Public NamaFileExeUpdater

    Dim ProsesPenerbitanUpdater As Boolean

    Dim folderPathProjectBooku As String
    Dim folderPathBookuFinal As String
    Dim folderPathTempBooku As String
    Dim zipFilePathBooku As String
    Dim folderPathUpdater As String
    Dim zipFilePathUpdater As String

    Dim folderPathProjectBookuUpdater As String

    Private Async Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        chk_KompressPaketBoku.IsChecked = True
        chk_UploadPaketBoku.IsChecked = True
        chk_KompressPaketUpdater.IsChecked = False
        chk_UploadPaketUpdater.IsChecked = False
        btn_Batal.Visibility = Visibility.Collapsed

        folderPathProjectBooku = Path.Combine("D:\VB .Net Project\BookuID\Booku\")
        folderPathProjectBookuUpdater = Path.Combine("D:\VB .Net Project\BookuID\Booku Updater\")
        folderPathBookuFinal = Path.Combine("D:\VB .Net Project\BookuID\Booku\bin\Release\Final\")
        folderPathTempBooku = Path.Combine(FolderRootBookuID, "TempBookuZip")
        zipFilePathBooku = Path.Combine(FolderRootBookuID, NamaFileZipPaketBooku)
        folderPathUpdater = Path.Combine("D:\VB .Net Project\BookuID\Booku Updater\bin\Release\Final\")
        zipFilePathUpdater = Path.Combine(FolderRootBookuID, NamaFileZipPaketUpdater)

        btn_Terbitkan.IsEnabled = False

        Await JalankanPUBLISHER_Updater()  ' Tunggu sampai selesai

        Await JalankanPUBLISHER_Booku()    ' Baru jalankan setelah Updater selesai

        btn_Terbitkan.IsEnabled = True

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()
        btn_Terbitkan.IsEnabled = True
        pgb_KompressPaketBooku.Value = 0
        pgb_UploadPaketBooku.Value = 0
        pgb_KompressPaketUpdater.Value = 0
        pgb_UploadPaketUpdater.Value = 0
        lbl_KompressPaketBooku.Text = Kosongan
        lbl_UploadPaketBooku.Text = Kosongan
        lbl_KompressPaketUpdater.Text = Kosongan
        lbl_UploadPaketUpdater.Text = Kosongan
    End Sub


    Async Function JalankanPUBLISHER_Updater() As Task
        Dim po As New Process
        po.StartInfo.FileName = Path.Combine(folderPathProjectBookuUpdater, "PUBLISH-RELEASE.bat")
        po.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Try
            po.Start()
            Await Task.Run(Sub() po.WaitForExit())  ' Tunggu sampai proses selesai
        Catch ex As Exception
        End Try
    End Function

    Async Function JalankanPUBLISHER_Booku() As Task
        Dim po As New Process
        po.StartInfo.FileName = Path.Combine(folderPathProjectBooku, "PUBLISH-RELEASE.bat")
        po.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Try
            po.Start()
            Await Task.Run(Sub() po.WaitForExit())  ' Tunggu sampai proses selesai
        Catch ex As Exception
        End Try
    End Function

    Sub LogikaProsesPenerbitanUpdater(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesPenerbitanUpdater = True
        Else
            ProsesPenerbitanUpdater = False
        End If
    End Sub

    Private Async Sub TahapanProsesPenerbitan()

        ProsesPenerbitanUpdater = True

        HapusFolderCache()

        Await KompressPaketBooku()

        Await UploadPaketBooku()

        Await KompressPaketUpdater()

        Await UploadPaketUpdater()

    End Sub

    Sub HapusFolderCache()
        HapusFolder(Path.Combine(FolderRootApp, "Booku.exe.WebView2"))
    End Sub

    Async Function KompressPaketBooku() As Task

        If Not chk_KompressPaketBoku.IsChecked Then Return

        If ProsesPenerbitanUpdater Then
            SalinFolder(folderPathBookuFinal, folderPathTempBooku)
            LogikaProsesPenerbitanUpdater(SalinFolderBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            Await KompressFile(folderPathTempBooku, zipFilePathBooku, pgb_KompressPaketBooku, lbl_KompressPaketBooku, 1, 100)
            LogikaProsesPenerbitanUpdater(KompressBerhasil)
            Jeda(999)
        End If

        If ProsesPenerbitanUpdater Then
            HapusFolder(folderPathTempBooku)
            LogikaProsesPenerbitanUpdater(HapusFolderBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            lbl_KompressPaketBooku.Text = "Sukses"
        Else
            lbl_KompressPaketBooku.Text = "Gagal"
            lbl_KompressPaketBooku.Foreground = clrWhite
            pgb_KompressPaketBooku.Background = clrError
        End If

        Jeda(999)

    End Function

    Async Function UploadPaketBooku() As Task

        If Not chk_UploadPaketBoku.IsChecked Then Return

        If ProsesPenerbitanUpdater Then
            UploadBerhasil = Await UploadFileAsync_MetodeChunked(
                zipFilePathBooku,
                urlFolderServerBookuID_Support & NamaFileZipPaketBooku,
                urlFileUplaodChunk_PHP,
                urlFileMergeChunks_PHP,
                pgb_UploadPaketBooku,
                lbl_UploadPaketBooku)
            LogikaProsesPenerbitanUpdater(UploadBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            lbl_UploadPaketBooku.Text = "Sukses"
        Else
            lbl_UploadPaketBooku.Text = "Gagal"
            lbl_UploadPaketBooku.Foreground = clrWhite
            pgb_UploadPaketBooku.Background = clrError
        End If

        Jeda(999)

    End Function

    Async Function KompressPaketUpdater() As Task

        If Not chk_KompressPaketUpdater.IsChecked Then Return

        If ProsesPenerbitanUpdater Then
            Await KompressFile(folderPathUpdater, zipFilePathUpdater, pgb_KompressPaketUpdater, lbl_KompressPaketUpdater, 1, 100)
            LogikaProsesPenerbitanUpdater(KompressBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            lbl_KompressPaketUpdater.Text = "Sukses"
        Else
            lbl_KompressPaketUpdater.Text = "Gagal"
            lbl_KompressPaketUpdater.Foreground = clrWhite
            pgb_KompressPaketUpdater.Background = clrError
        End If

        Jeda(999)

    End Function

    Async Function UploadPaketUpdater() As Task

        If Not chk_UploadPaketUpdater.IsChecked Then Return

        If ProsesPenerbitanUpdater Then
            UploadBerhasil = Await UploadFileAsync_MetodeChunked(
                zipFilePathUpdater,
                urlFolderServerBookuID_Support & NamaFileZipPaketUpdater,
                urlFileUplaodChunk_PHP,
                urlFileMergeChunks_PHP,
                pgb_UploadPaketUpdater,
                lbl_UploadPaketUpdater)
            LogikaProsesPenerbitanUpdater(UploadBerhasil)
        End If

        Jeda(999)

        If ProsesPenerbitanUpdater Then
            lbl_UploadPaketUpdater.Text = "Sukses"
            HapusPaket()
        Else
            lbl_UploadPaketUpdater.Text = "Gagal"
            lbl_UploadPaketUpdater.Foreground = clrWhite
            pgb_UploadPaketUpdater.Background = clrError
        End If

    End Function

    Sub HapusPaket()

        If ProsesPenerbitanUpdater Then
            If chk_UploadPaketBoku.IsChecked Then HapusFile(zipFilePathBooku)
            If chk_UploadPaketUpdater.IsChecked Then HapusFile(zipFilePathUpdater)
        End If

    End Sub

    Private Sub btn_Terbitkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terbitkan.Click
        ResetForm()
        btn_Terbitkan.IsEnabled = False
        Terabas()
        TahapanProsesPenerbitan()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
