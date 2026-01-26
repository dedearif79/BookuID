Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports bcomm

Public Class wpfWin_PenerbitUpdater

    Public VersiApp
    Public ApdetApp
    Public urlPaketBooku
    Public urlPaketUpdater
    Public NamaFolderTempPaketBooku
    Public NamaFolderTempPaketUpdater
    Public NamaFileExeUpdater

    Dim ProsesPenerbitanUpdater As Boolean

    'Paket Booku:
    Public NamaFileZipPaketBooku
    Dim folderPathProjectBooku As String
    Dim folderPathBookuFinal As String
    Dim folderPathTempBooku As String
    Dim zipFilePathBooku As String

    'Paket Booku Assistant:
    Public NamaFileZipPaketBookuAssistant
    Dim folderPathProjectBookuAssistant As String
    Dim folderPathBookuAssistantFinal As String
    'Dim folderPathTempBookuAssistant As String
    Dim zipFilePathBookuAssistant As String

    'Paket Booku Remote:
    Public NamaFileZipPaketBookuRemote
    Dim folderPathProjectBookuRemote As String
    Dim folderPathBookuRemoteFinal As String
    'Dim folderPathTempBookuRemote As String
    Dim zipFilePathBookuRemote As String

    'Updater:
    Public NamaFileZipPaketUpdater
    Dim zipFilePathUpdater As String
    Dim folderPathUpdaterFinal As String
    Dim folderPathProjectBookuUpdater As String


    Private Async Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        chk_BuildBooku.IsChecked = True
        chk_BuildBookuUpdater.IsChecked = False
        chk_BuildBookuRemote.IsChecked = False

        chk_KompressPaketBooku.IsChecked = True
        chk_UploadPaketBooku.IsChecked = True
        chk_KompressPaketUpdater.IsChecked = False
        chk_UploadPaketUpdater.IsChecked = False
        btn_Batal.Visibility = Visibility.Collapsed

        'Paket Booku:
        folderPathProjectBooku = Path.Combine("D:\VB .Net Project\BookuID\Booku\")
        folderPathBookuFinal = Path.Combine("D:\VB .Net Project\BookuID\Booku\bin\Release\Final\")
        folderPathTempBooku = Path.Combine(FolderRootBookuID, "TempBookuZip")
        zipFilePathBooku = Path.Combine(FolderRootBookuID, NamaFileZipPaketBooku)

        'Paket Booku Assistant:
        folderPathProjectBookuAssistant = Path.Combine("D:\VB .Net Project\BookuID\Booku Assistant\")
        folderPathBookuAssistantFinal = Path.Combine("D:\VB .Net Project\BookuID\Booku Assistant\bin\Release\Final\")
        'folderPathTempBookuAssistant = Path.Combine(FolderRootBookuID, "TempBookuAssistantZip")
        zipFilePathBookuAssistant = Path.Combine(FolderRootBookuID, NamaFileZipPaketBookuAssistant)

        'Paket Booku Remote:
        folderPathProjectBookuRemote = Path.Combine("D:\VB .Net Project\BookuID\Booku Remote\")
        folderPathBookuRemoteFinal = Path.Combine("D:\VB .Net Project\BookuID\Booku Remote\bin\Release\Final\")
        'folderPathTempBookuRemote = Path.Combine(FolderRootBookuID, "TempBookuRemoteZip")
        zipFilePathBookuRemote = Path.Combine(FolderRootBookuID, NamaFileZipPaketBookuRemote)

        'Updater
        folderPathProjectBookuUpdater = Path.Combine("D:\VB .Net Project\BookuID\Booku Updater\")
        zipFilePathUpdater = Path.Combine(FolderRootBookuID, NamaFileZipPaketUpdater)
        folderPathUpdaterFinal = Path.Combine("D:\VB .Net Project\BookuID\Booku Updater\bin\Release\Final\")

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
        lbl_KompressPaketBookuAssistant.Text = Kosongan
        lbl_UploadPaketBookuAssistant.Text = Kosongan
        lbl_KompressPaketBookuRemote.Text = Kosongan
        lbl_UploadPaketBookuRemote.Text = Kosongan
    End Sub


    Async Function BuildProject(folderPathProject) As Task
        Dim po As New Process
        po.StartInfo.FileName = Path.Combine(folderPathProject, "PUBLISH-RELEASE.bat")
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

        'Paket Booku:
        If chk_KompressPaketBooku.IsChecked Then Await KompressPaketBooku() ' Ini agak laen memang...!!!!
        If chk_UploadPaketBooku.IsChecked Then Await UploadPaketAplikasi(zipFilePathBooku, NamaFileZipPaketBooku, pgb_UploadPaketBooku, lbl_UploadPaketBooku)

        'Paket Updater:
        If chk_KompressPaketUpdater.IsChecked Then Await KompressPaketAplikasi(folderPathUpdaterFinal, zipFilePathUpdater, pgb_KompressPaketUpdater, lbl_KompressPaketUpdater)
        If chk_UploadPaketUpdater.IsChecked Then Await UploadPaketAplikasi(zipFilePathUpdater, NamaFileZipPaketUpdater, pgb_UploadPaketUpdater, lbl_UploadPaketUpdater)

        'Paket Booku Assistant:
        If chk_KompressPaketBookuAssistant.IsChecked Then Await KompressPaketAplikasi(folderPathBookuAssistantFinal, zipFilePathBookuAssistant, pgb_KompressPaketBookuAssistant, lbl_KompressPaketBookuAssistant)
        If chk_UploadPaketBookuAssistant.IsChecked Then Await UploadPaketAplikasi(zipFilePathBookuAssistant, NamaFileZipPaketBookuAssistant, pgb_UploadPaketBookuAssistant, lbl_UploadPaketBookuAssistant)

        'Paket Booku Remote:
        If chk_KompressPaketBookuRemote.IsChecked Then Await KompressPaketAplikasi(folderPathBookuRemoteFinal, zipFilePathBookuRemote, pgb_KompressPaketBookuRemote, lbl_KompressPaketBookuRemote)
        If chk_UploadPaketBookuRemote.IsChecked Then Await UploadPaketAplikasi(zipFilePathBookuRemote, NamaFileZipPaketBookuRemote, pgb_UploadPaketBookuRemote, lbl_UploadPaketBookuRemote)

    End Sub

    Sub HapusFolderCache()
        HapusFolder(Path.Combine(FolderRootApp, "Booku.exe.WebView2"))
    End Sub

    Async Function KompressPaketBooku() As Task

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

    Async Function UploadPaketAplikasi(zipFilePathAplikasi As String, NamaFileZipPaketAplikasi As String, pgb_ProgressBar As ProgressBar, lbl_TextBlock As TextBlock) As Task

        If ProsesPenerbitanUpdater Then
            UploadBerhasil = Await UploadFileAsync_MetodeChunked(
                zipFilePathAplikasi,
                urlFolderServerBookuID_Support & NamaFileZipPaketAplikasi,
                urlFileUplaodChunk_PHP,
                urlFileMergeChunks_PHP,
                pgb_ProgressBar,
                lbl_TextBlock)
            LogikaProsesPenerbitanUpdater(UploadBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            lbl_TextBlock.Text = "Sukses"
        Else
            lbl_TextBlock.Text = "Gagal"
            lbl_TextBlock.Foreground = clrWhite
            pgb_ProgressBar.Background = clrError
        End If

        Jeda(999)

    End Function

    Async Function KompressPaketAplikasi(folderPathAplikasiFinal As String, zipFilePathAplikasi As String, pgb_ProgressBar As ProgressBar, lbl_TextBlock As TextBlock) As Task

        If ProsesPenerbitanUpdater Then
            Await KompressFile(folderPathAplikasiFinal, zipFilePathAplikasi, pgb_ProgressBar, lbl_TextBlock, 1, 100)
            LogikaProsesPenerbitanUpdater(KompressBerhasil)
        End If

        If ProsesPenerbitanUpdater Then
            lbl_TextBlock.Text = "Sukses"
        Else
            lbl_TextBlock.Text = "Gagal"
            lbl_TextBlock.Foreground = clrWhite
            pgb_ProgressBar.Background = clrError
        End If

        Jeda(999)

    End Function

    Sub HapusPaket()

        If ProsesPenerbitanUpdater Then
            If chk_UploadPaketBooku.IsChecked Then HapusFile(zipFilePathBooku)
            If chk_UploadPaketUpdater.IsChecked Then HapusFile(zipFilePathUpdater)
            If chk_UploadPaketBookuAssistant.IsChecked Then HapusFile(zipFilePathBookuAssistant)
            If chk_UploadPaketBookuRemote.IsChecked Then HapusFile(zipFilePathBookuRemote)
        End If

    End Sub

    Private Async Sub btn_Build_Click(sender As Object, e As RoutedEventArgs) Handles btn_Build.Click

        btn_Build.IsEnabled = False
        If chk_BuildBooku.IsChecked Then Await BuildProject(folderPathProjectBooku)
        If chk_BuildBookuUpdater.IsChecked Then Await BuildProject(folderPathProjectBookuUpdater)
        If chk_BuildBookuAssistant.IsChecked Then Await BuildProject(folderPathProjectBookuAssistant)
        If chk_BuildBookuRemote.IsChecked Then Await BuildProject(folderPathProjectBookuRemote)
        btn_Build.IsEnabled = True

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
