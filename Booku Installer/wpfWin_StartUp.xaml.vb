Imports System.IO
Imports bcomm
Imports MySql.Data.MySqlClient
Imports System.IO.Compression
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Class MainWindow

    Dim VersiTerbaru As String
    Dim UpdateTerbaru As String
    Dim ProsesInstalasi As Boolean
    Dim InstallBerhasil As Boolean
    Dim urlPaketBooku As String
    Dim FolderTempPaketBooku As String
    Dim FilePathPaketBooku As String

    Dim FolderRootBooku As String
    Dim FolderBookuAttach As String
    Dim FolderBookuAttachNotes As String
    Dim FolderBookuClient As String
    Dim FolderBookuRuntimes As String
    Dim FolderTempAttach As String
    Dim FolderTempClient As String

    Dim FolderBookuBAK As String

    Dim NamaAplikasi As String = "Booku"
    Dim NamaFolder_Attach As String = "Attach"
    Dim NamaFolder_Client As String = "Client"
    Dim NamaFolder_Notes As String = "Notes"
    Dim NamaFolder_Runtimes As String = "runtimes"
    Dim FilePathAplikasiBooku As String
    Dim NamaFileAplikasi As String = "Booku.exe"

    Dim FilePathPathVersiDanApdetAplikasi

    Dim TahapanInstall

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        InstallBerhasil = False 'Dua variabel ini jangan dipindahkan
        ProsesInstalasi = True  'Dua variabel ini jangan dipindahkan
        StyleWindowDialogWPF_TanpaTombolX(Me)
        Terabas()
        lbl_01.Text = "Sebaiknya tutup semua aplikasi yang aktif sebelum melanjutkan proses Install..!"
        lbl_02.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Collapsed
        lbl_Progress.Text = Kosongan
        pnl_TombolForm.Visibility = Visibility.Visible
        btn_Lanjutkan.IsEnabled = True

    End Sub


    Sub LogikaProsesInstalasi(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesInstalasi = True
        Else
            ProsesInstalasi = False
        End If
    End Sub


    Async Sub TahapanProsesInstalasi()

        ProsesInstalasi = True

        'Await AmbilValueDataPublic()

        'Await DownloadPaketBooku()

        'Await EkstrakPaketBooku()

        Await InstallXAMPP(pgb_Progress, lbl_Progress)


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
            urlPaketBooku = drPublic.Item("URL_Paket_Booku")
            FolderTempPaketBooku = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Booku"))
            FilePathPaketBooku = Path.Combine(FolderTempPaketBooku, drPublic.Item("File_Paket_Booku"))
            StatusKoneksiDatabasePublic = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
        End Try
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            ProsesInstalasi = True
            FolderRootBooku = Path.Combine(FolderRootBookuID, NamaAplikasi)
            FolderBookuAttach = Path.Combine(FolderRootBooku, NamaFolder_Attach)
            FolderBookuAttachNotes = Path.Combine(FolderBookuAttach, NamaFolder_Notes)
            FolderBookuClient = Path.Combine(FolderRootBooku, NamaFolder_Client)
            FolderBookuRuntimes = Path.Combine(FolderRootBooku, NamaFolder_Runtimes)
            FolderTempAttach = Path.Combine(FolderTempPaketBooku, NamaFolder_Attach)
            FolderTempClient = Path.Combine(FolderTempPaketBooku, NamaFolder_Client)
            FilePathAplikasiBooku = Path.Combine(FolderRootBooku, NamaFileAplikasi)
            FolderBookuBAK = FolderRootBooku & "_BAK"
            FilePathPathVersiDanApdetAplikasi = Path.Combine(FolderBookuAttachNotes, NamaFileVersiDanApdetAplikasi)
        Else
            ProsesInstalasi = False
            MsgBox("Aplikasi tidak terkoneksi dengan server Booku." & Enter2Baris & "Silakan periksa koneksi internet Anda.")
        End If

    End Sub


    Async Function DownloadPaketBooku() As Task

        If ProsesInstalasi = True Then
            BuatFolder(FolderTempPaketBooku)
            Await DownloadFile_MetodeFTP(urlPaketBooku, FilePathPaketBooku, pgb_Progress, lbl_Progress)
        End If

        If DownloadBerhasil Then
            ProsesInstalasi = True
        Else
            ProsesInstalasi = False
        End If

    End Function


    Async Function EkstrakPaketBooku() As Task

        HapusHanyaFileDalamFolder(FolderRootBooku)
        HapusFolder(FolderBookuRuntimes)
        Jeda(999)

        lbl_01.Text = "Proses Install sedang berjalan."
        lbl_Progress.Text = Kosongan
        Terabas()

        If ProsesInstalasi Then
            Await EkstrakFile(FilePathPaketBooku, FolderRootBooku, pgb_Progress, lbl_Progress)
            If EkstrakBerhasil Then
                ProsesInstalasi = True
            Else
                ProsesInstalasi = False
            End If
        End If

    End Function



    Public Sub BeriKeteranganVersiDanApdetPerangkat()
        If ProsesInstalasi Then
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
                ProsesInstalasi = True
            Else
                ProsesInstalasi = False
            End If
        End If

    End Sub


    Sub Finish()

        pgb_Progress.Visibility = Visibility.Collapsed
        lbl_Progress.Visibility = Visibility.Collapsed
        pnl_TombolForm.Visibility = Visibility.Visible
        btn_Lanjutkan.IsEnabled = True

        If ProsesInstalasi = True Then
            InstallBerhasil = True
            lbl_01.Text = "Install berhasil..!"
            btn_Lanjutkan.Content = "Buka Aplikasi " & NamaAplikasi & " >>"
        Else
            lbl_01.Text = "Install gagal..!"
            btn_Lanjutkan.Content = "Tutup"
            HapusFolder(FolderTempPaketBooku)
        End If

    End Sub


    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        pnl_TombolForm.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Visible
        lbl_Progress.Visibility = Visibility.Visible
        lbl_01.Text = "Mohon tunggu! Sistem sedang mendownload file-file Install."
        btn_Lanjutkan.IsEnabled = False
        Terabas()

        If ProsesInstalasi Then
            If Not InstallBerhasil Then
                TahapanProsesInstalasi()
            End If
        Else
            End
        End If

        If InstallBerhasil Then
            JalankanAplikasi(FilePathAplikasiBooku)
            End
        End If

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
