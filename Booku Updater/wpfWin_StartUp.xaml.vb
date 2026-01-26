Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Net.Http
Imports System.Runtime.InteropServices
Imports bcomm
Imports BookuID.Styles
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json.Linq

Class wpfWin_StartUp

    Dim VersiTerbaru As String
    Dim UpdateTerbaru As String
    Dim ProsesUpdate_Aplikasi As Boolean
    Dim UpdateBerhasil As Boolean

    'Paket Booku
    Dim FolderRootBooku As String
    Dim urlPaketBooku As String
    Dim FolderTempPaketBooku As String
    Dim FilePathPaketBooku As String

    'Paket BookuAssistant
    Dim FolderRootBookuAssistant As String
    Dim urlPaketBookuAssistant As String
    Dim FolderTempPaketBookuAssistant As String
    Dim FilePathPaketBookuAssistant As String

    'Paket BookuRemote
    Dim FolderRootBookuRemote As String
    Dim urlPaketBookuRemote As String
    Dim FolderTempPaketBookuRemote As String
    Dim FilePathPaketBookuRemote As String

    Dim FolderBookuAttach As String
    Dim FolderBookuAttachNotes As String
    Dim FolderBookuClient As String
    Dim FolderTempAttach As String
    Dim FolderTempClient As String

    Dim FolderBookuBAK As String

    Dim NamaAplikasi As String = "Booku"
    Dim NamaFolder_Attach As String = "Attach"
    Dim NamaFolder_Client As String = "Client"
    Dim NamaFolder_Notes As String = "Notes"
    Dim FilePathAplikasiBooku As String
    Dim NamaFileAplikasi As String = "Booku.exe"

    Dim FilePathPathVersiDanApdetAplikasi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        UpdateBerhasil = False          'Dua variabel ini jangan dipindahkan
        ProsesUpdate_Aplikasi = True    'Dua variabel ini jangan dipindahkan
        StyleWindowDialogWPF_TanpaTombolX(Me)
        Terabas()
        lbl_01.Text = "Sebaiknya tutup semua aplikasi yang aktif sebelum melanjutkan proses update..!"
        lbl_02.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Collapsed
        lbl_Progress.Text = Kosongan
        pnl_TombolForm.Visibility = Visibility.Visible
        btn_Lanjutkan.IsEnabled = True

    End Sub


    Async Sub TahapanUpdating()

        AmbilValueDataPublic()

        'Aplikasi Booku:
        Await DownloadPaketAplikasi(urlPaketBooku, FilePathPaketBooku, FolderTempPaketBooku)
        Await EkstrakPaketAplikasi(FilePathPaketBooku, FolderRootBooku)

        'Aplikasi Booku Assistant:
        Await DownloadPaketAplikasi(urlPaketBookuAssistant, FilePathPaketBookuAssistant, FolderTempPaketBookuAssistant)
        Await EkstrakPaketAplikasi(FilePathPaketBookuAssistant, FolderRootBookuAssistant)

        'Aplikasi Booku Remote:
        Await DownloadPaketAplikasi(urlPaketBookuRemote, FilePathPaketBookuRemote, FolderTempPaketBookuRemote)
        Await EkstrakPaketAplikasi(FilePathPaketBookuRemote, FolderRootBookuRemote)

        HapusFolder(FolderTempPaketBooku)
        HapusFolder(FolderTempPaketBookuAssistant)
        HapusFolder(FolderTempPaketBookuRemote)

        BeriKeteranganVersiDanApdetPerangkat()

        Finish()

    End Sub


    Sub AmbilValueDataPublic()

        BukaDatabasePublic()
        Try
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
            drPublic = cmdPublic.ExecuteReader
            drPublic.Read()
            VersiTerbaru = drPublic.Item("Versi_App")
            UpdateTerbaru = drPublic.Item("Apdet_App")
            'Paket Booku:
            urlPaketBooku = drPublic.Item("URL_Paket_Booku")
            FolderTempPaketBooku = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Booku"))
            FilePathPaketBooku = Path.Combine(FolderTempPaketBooku, drPublic.Item("File_Paket_Booku"))
            'Paket BookuAssistant:
            urlPaketBookuAssistant = drPublic.Item("URL_Paket_Booku_Assistant")
            FolderTempPaketBookuAssistant = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Booku_Assistant"))
            FilePathPaketBookuAssistant = Path.Combine(FolderTempPaketBookuAssistant, drPublic.Item("File_Paket_Booku_Assistant"))
            'Paket BookuRemote:
            urlPaketBookuRemote = drPublic.Item("URL_Paket_Booku_Remote")
            FolderTempPaketBookuRemote = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Booku_Remote"))
            FilePathPaketBookuRemote = Path.Combine(FolderTempPaketBookuRemote, drPublic.Item("File_Paket_Booku_Remote"))
            StatusKoneksiDatabasePublic = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
        End Try
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            ProsesUpdate_Aplikasi = True
            FolderRootBooku = Path.Combine(FolderRootBookuID, NamaAplikasi)
            FolderRootBookuAssistant = Path.Combine(FolderRootBookuID, NamaAplikasi & " Assistant")
            FolderRootBookuRemote = Path.Combine(FolderRootBookuID, NamaAplikasi & " Remote")
            FolderBookuAttach = Path.Combine(FolderRootBooku, NamaFolder_Attach)
            FolderBookuAttachNotes = Path.Combine(FolderBookuAttach, NamaFolder_Notes)
            FolderBookuClient = Path.Combine(FolderRootBooku, NamaFolder_Client)
            FolderTempAttach = Path.Combine(FolderTempPaketBooku, NamaFolder_Attach)
            FolderTempClient = Path.Combine(FolderTempPaketBooku, NamaFolder_Client)
            FilePathAplikasiBooku = Path.Combine(FolderRootBooku, NamaFileAplikasi)
            FolderBookuBAK = FolderRootBooku & "_BAK"
            FilePathPathVersiDanApdetAplikasi = Path.Combine(FolderBookuAttachNotes, NamaFileVersiDanApdetAplikasi)
        Else
            ProsesUpdate_Aplikasi = False
            MsgBox("Aplikasi tidak terkoneksi dengan server Booku." & Enter2Baris & "Silakan periksa koneksi internet Anda.")
        End If

    End Sub


    Async Function DownloadPaketAplikasi(urlPaketAplikasi As String, FilePathPaketAplikasi As String, FolderTempPaketAplikasi As String) As Task

        If ProsesUpdate_Aplikasi = True Then
            BuatFolder(FolderTempPaketAplikasi)
            DownloadBerhasil = Await DownloadFileDariServerAsync_MetodeHTTP(urlPaketAplikasi, FilePathPaketAplikasi, urlFileDownloader_PHP, pgb_Progress, lbl_Progress)
        End If

        If DownloadBerhasil Then
            ProsesUpdate_Aplikasi = True
        Else
            ProsesUpdate_Aplikasi = False
        End If

        Jeda(333)

    End Function


    Async Function EkstrakPaketAplikasi(FilePathPaketBooku As String, FolderRootBooku As String) As Task

        HapusHanyaFileDalamFolder(FolderRootBooku)
        Jeda(999)

        lbl_01.Text = "Proses update sedang berjalan."
        lbl_Progress.Text = Kosongan
        Terabas()

        If ProsesUpdate_Aplikasi Then
            Await EkstrakFile(FilePathPaketBooku, FolderRootBooku, pgb_Progress, lbl_Progress)
            If EkstrakBerhasil Then
                ProsesUpdate_Aplikasi = True
            Else
                ProsesUpdate_Aplikasi = False
            End If
        End If

    End Function



    Public Sub BeriKeteranganVersiDanApdetPerangkat()
        If ProsesUpdate_Aplikasi Then
            Dim DataKeteranganVersiDanApdet
            DataKeteranganVersiDanApdet = HeaderConfig &
            "DJdkf798iudfhkjfhdfk^=DGjT" & Enter1Baris &
            EnkripsiAngkaAES1(VersiTerbaru) & Enter1Baris &
            Enter1Baris &
            "kf25542438dfjdsfjkh&sdrf%L" & Enter1Baris &
            EnkripsiAngkaAES1(UpdateTerbaru) & Enter1Baris &
            Enter1Baris &
            FooterConfig
            SimpanDokumen(FilePathPathVersiDanApdetAplikasi, DataKeteranganVersiDanApdet)
            If SimpanDokumenBerhasil Then
                ProsesUpdate_Aplikasi = True
            Else
                ProsesUpdate_Aplikasi = False
            End If
        End If

    End Sub


    Sub Finish()

        pgb_Progress.Visibility = Visibility.Collapsed
        lbl_Progress.Visibility = Visibility.Collapsed
        pnl_TombolForm.Visibility = Visibility.Visible
        btn_Lanjutkan.IsEnabled = True

        If ProsesUpdate_Aplikasi = True Then
            UpdateBerhasil = True
            lbl_01.Text = "Update berhasil..!"
            btn_Lanjutkan.Content = "Buka Aplikasi " & NamaAplikasi & " >>"
        Else
            lbl_01.Text = "Update gagal..!"
            btn_Lanjutkan.Content = "Tutup"
            HapusFolder(FolderTempPaketBooku)
        End If

    End Sub


    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        pnl_TombolForm.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Visible
        lbl_Progress.Visibility = Visibility.Visible
        lbl_01.Text = "Mohon tunggu! Sistem sedang mendownload file-file update."
        btn_Lanjutkan.IsEnabled = False
        Terabas()

        If ProsesUpdate_Aplikasi Then
            If Not UpdateBerhasil Then
                TahapanUpdating()
            End If
        Else
            End
        End If

        If UpdateBerhasil Then
            JalankanAplikasi(FilePathAplikasiBooku)
            End
        End If

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
