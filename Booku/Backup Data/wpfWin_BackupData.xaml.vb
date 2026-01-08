Imports System.IO
Imports System.Windows
Imports bcomm

Public Class wpfWin_BackupData

    Dim PenambahanProgress As String
    Dim ProsentaseProgress As Integer

    Dim NamaFileZipBackUp As String
    Dim zipFilePathBackUp As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        btn_Backup.Visibility = Visibility.Collapsed
        btn_Tutup.Visibility = Visibility.Collapsed
        Terabas()

        NamaFileZipBackUp = ID_Customer & ".zip"
        zipFilePathBackUp = Path.Combine(FolderListClient, NamaFileZipBackUp)

        TahapanBackup()

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()
        lbl_Progress.Foreground = WarnaTeksStandar_WPF
        pgb_Progress.Foreground = WarnaHijauProgressBar_WPF
        btn_Upload.Visibility = Visibility.Collapsed
        btn_Backup.IsEnabled = True
        pgb_Progress.Value = 0
        lbl_Progress.Text = Kosongan
    End Sub


    Dim ProsesBackupData As Boolean
    Sub LogikaProsesBackupData(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesBackupData = True
            pgb_Progress.Value += PenambahanProgress
            ProsentaseProgress += PenambahanProgress
            lbl_Progress.Text = "Proses backup... " & ProsentaseProgress & " %"
        Else
            ProsesBackupData = False
        End If
    End Sub


    Dim ProsesUploadPaketBackUp As Boolean
    Sub LogikaProsesUpload(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesUploadPaketBackUp = True
        Else
            ProsesUploadPaketBackUp = False
        End If
    End Sub

    Private Async Sub TahapanBackup()

        ProsesBackupData = True
        pgb_Progress.Value = 0
        ProsentaseProgress = 0

        lbl_Progress.Text = "Proses backup... 0 %"
        Jeda(333)

        Dim JumlahDatabase = 0
        Dim DatabaseTahunBackup As String

        AksesDatabase_General(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            JumlahDatabase += 1 'Database Transaksi
        Loop
        AksesDatabase_General(Tutup)
        JumlahDatabase += 2 'Database Dasar dan General

        PenambahanProgress = 100 / JumlahDatabase

        If ProsesBackupData Then Await BackUpMySql_TanpaProgress(NamaDatabaseDasar, UserDatabase, PasswordDatabase, LokasiServerDatabase, PortDatabase, FolderCompany_Backup_MySQL)
        LogikaProsesBackupData(BackUpDatabaseBerhasil)
        Jeda(333)

        If ProsesBackupData Then Await BackUpMySql_TanpaProgress(NamaDatabaseGeneral, UserDatabase, PasswordDatabase, LokasiServerDatabase, PortDatabase, FolderCompany_Backup_MySQL)
        LogikaProsesBackupData(BackUpDatabaseBerhasil)
        Jeda(333)

        AksesDatabase_General(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            DatabaseTahunBackup = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & dr.Item("Tahun_Buku")
            If ProsesBackupData Then Await BackUpMySql_TanpaProgress(DatabaseTahunBackup, UserDatabase, PasswordDatabase, LokasiServerDatabase, PortDatabase, FolderCompany_Backup_MySQL)
            LogikaProsesBackupData(BackUpDatabaseBerhasil)
            Jeda(333)
        Loop
        AksesDatabase_General(Tutup)

        If ProsesBackupData = True Then
            pgb_Progress.Value = 100
            lbl_Progress.Text = "Backup database berhasil."
            btn_Backup.Visibility = Visibility.Collapsed
            btn_Tutup.Visibility = Visibility.Visible
            btn_Upload.Content = "Upload"
            btn_Upload.Visibility = Visibility.Visible
            Terabas()
            Jeda(999)
        Else
            pgb_Progress.Value = 0
            lbl_Progress.Text = "Backup database gagal."
            pgb_Progress.Foreground = WarnaPeringatan_WPF
            Terabas()
            Jeda(999)
            btn_Backup.Content = "Ulangi"
            btn_Backup.Visibility = Visibility.Visible
        End If

    End Sub


    Dim JumlahTahapanUpload As Integer
    Dim AngkaFullPerTahapan As Decimal
    Async Sub TahapanProsesUploadPaketBackUp()

        Dim zipFilePathBackUp_Public As String = urlFolderServerBookuID_BackUpDataClient & NamaFileZipBackUp
        Dim zipFilePathBackUp_Public_BAK As String = urlFolderServerBookuID_BackUpDataClient & "bak/" & NamaFileZipBackUp
        Dim zipFilePathBackUp_Public_Trash As String = urlFolderServerBookuID_BackUpDataClient & "trash/" & NamaFileZipBackUp

        lbl_Progress.Foreground = WarnaTeksStandar_WPF
        pgb_Progress.Foreground = WarnaHijauProgressBar_WPF

        pgb_Progress.Value = 0
        ProsentaseProgress = 0
        JumlahTahapanUpload = 2
        AngkaFullPerTahapan = 100 / JumlahTahapanUpload
        ProsesUploadPaketBackUp = True
        Await KompressPaketBackUp()
        PindahkanFileAntarFolderDiServer_MetodeHTTP(zipFilePathBackUp_Public, zipFilePathBackUp_Public_BAK, urlFileRenamer_PHP)
        Await UploadPaketBackup()

        If ProsesUploadPaketBackUp Then
            btn_Upload.Visibility = Visibility.Collapsed
            lbl_Progress.Text = "Upload paket backup sukses"
        Else
            btn_Upload.IsEnabled = True
            btn_Upload.Content = "Coba upload lagi"
            lbl_Progress.Text = "Upload paket backup gagal"
            pgb_Progress.Foreground = WarnaMerahSolid_WPF
        End If
        Terabas()
        Jeda(999)

    End Sub

    Async Function KompressPaketBackUp() As Task

        If ProsesUploadPaketBackUp Then
            Await KompressFile(FolderCompany, zipFilePathBackUp, pgb_Progress, lbl_Progress, 1, AngkaFullPerTahapan)
            LogikaProsesUpload(KompressBerhasil)
        End If

        If ProsesUploadPaketBackUp Then
        Else
            btn_Upload.Content = "Coba upload lagi"
            btn_Upload.IsEnabled = True
            lbl_Progress.Text = "Kompresi file backup gagal"
            pgb_Progress.Foreground = WarnaMerahSolid_WPF
        End If

        Terabas()
        Jeda(111)

    End Function

    Async Function UploadPaketBackup() As Task
        If ProsesUploadPaketBackUp Then
            Await UploadFileFTPAsync_MetodeHTTP(zipFilePathBackUp, urlFolderServerBookuID_BackUpDataClient, urlFileUploader_PHP, pgb_Progress, lbl_Progress, 2, AngkaFullPerTahapan)
            If Not UploadBerhasil Then PesanUntukProgrammer("Upload gagal...!!!")
            LogikaProsesUpload(UploadBerhasil)
        End If
        Jeda(111)
    End Function




    Private Sub btn_Upload_Click(sender As Object, e As RoutedEventArgs) Handles btn_Upload.Click
        btn_Upload.IsEnabled = False
        Terabas()
        TahapanProsesUploadPaketBackUp()
    End Sub


    Private Sub btn_Backup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Backup.Click
        ResetForm()
        btn_Backup.IsEnabled = False
        Terabas()
        TahapanBackup()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class
