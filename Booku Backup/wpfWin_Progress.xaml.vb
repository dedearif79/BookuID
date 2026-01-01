Imports System.IO
Imports bcomm


Public Class wpfWin_Progress

    Dim PenambahanProgress As String
    Dim ProsentaseProgress As Integer

    Dim HariIni As Date = Today
    Dim NamaProject = "BookuID"
    Dim NamaFileZipProject = NamaProject & ".zip"
    Dim NamaFileZipProject_PlusTanggal = NamaProject & " - " & TanggalFormatMySQL(Today) & ".zip"
    Dim NamaFileZipProject_2 = NamaProject & " (2).zip"
    Dim FolderProject_UMUM = "D:\VB .Net Project\"
    Dim FolderProjectBookuID = Path.Combine(FolderProject_UMUM, NamaProject)
    Dim FolderBACKUP_UMUM = Path.Combine(FolderProject_UMUM, "BACKUP")
    Dim GoogleDrive = "G:\"
    Dim FolderSAT_GoogleDrive = "My Drive\Sistem Akuntansi Terpadu\"
    Dim NamaFileZipProject_GoogleDrive = "Sistem Akuntansi Terpadu - " & Format(HariIni, "yyyy") & "-" & Format(HariIni, "MM") & "-" & Format(HariIni, "dd") & ".zip"
    Dim FolderDowload = "C:\Users\dedea\Downloads\"
    Dim ProsesBackup As Boolean
    Dim Pilihan

    Dim filePath_ZipProjectBackup = Path.Combine(FolderBACKUP_UMUM, NamaFileZipProject)
    Dim filePath_ZipProjectBackup2 = Path.Combine(FolderBACKUP_UMUM, NamaFileZipProject_2)


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        TahapanBackup()

    End Sub


    Private Async Sub TahapanBackup()

        ProsesBackup = True

        'Hapus Folder .vs:
        Try
            Directory.Delete(FolderProjectBookuID & ".vs")
        Catch ex As Exception
        End Try

        Try
            File.Delete(filePath_ZipProjectBackup2)
        Catch ex As Exception
        End Try
        If ProsesBackup Then
        End If

        'Rename File Zip menjadi Zip 2 :
        Try
            File.Move(filePath_ZipProjectBackup, filePath_ZipProjectBackup2)
        Catch ex As Exception
        End Try

        'Proses Kompress Folder Project dan Mengirimnya ke Folder Backup:
        If ProsesBackup Then
            lbl_Report_1.Text = "Backup project sedang dikompres..."
            lbl_Report_1.Visibility = Visibility.Visible
            Await KompressFile(FolderProjectBookuID, filePath_ZipProjectBackup, pgb_Progress, lbl_Progress, 1, 100)
            If KompressBerhasil Then
                ProsesBackup = True
                lbl_Report_1.Text = "Backup project berhasil dikompres."
            Else
                lbl_Report_1.Foreground = WarnaMerahSolid_WPF
                lbl_Report_1.Text = "Backup project gagal dikompres."
                ProsesBackup = False
            End If
        End If


        If ProsesBackup Then
            Try
                File.Delete(Path.Combine(FolderDowload, NamaFileZipProject))
            Catch ex As Exception
            End Try
            Try
                File.Copy(filePath_ZipProjectBackup, Path.Combine(FolderDowload, NamaFileZipProject), True)
                lbl_Report_2.Text = "Backup project berhasil disalin ke Folder Download."
            Catch ex As Exception
                lbl_Report_2.Foreground = WarnaMerahSolid_WPF
                lbl_Report_2.Text = "Backup project gagal disalin ke Folder Download...!!!"
            End Try
            lbl_Report_2.Visibility = Visibility.Visible
        End If

        If ProsesBackup Then
            Dim Pesan As String = "Lanjut ke proses upload..?"
            Dim Pilihan As MessageBoxResult = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButton.YesNo, MessageBoxImage.Question)
            If Pilihan = MessageBoxResult.No Then Close()
        End If

        If ProsesBackup Then
            lbl_Report_3.Text = "Backup project sedang diupload ke server..."
            lbl_Report_3.Visibility = Visibility.Visible
            PindahkanFileAntarFolderDiServer_MetodeHTTP(
                urlFolderServerBookuID_BackUpProject & NamaFileZipProject_PlusTanggal,
                urlFolderServerBookuID_BackUpProject & "bak/" &
                NamaFileZipProject_PlusTanggal,
                urlFileRenamer_PHP)
            UploadBerhasil =
                Await UploadFileAsync_MetodeChunked(
                Path.Combine(FolderDowload, NamaFileZipProject),
                urlFolderServerBookuID_BackUpProject & NamaFileZipProject_PlusTanggal,
                urlFileUplaodChunk_PHP,
                urlFileMergeChunks_PHP,
                pgb_Progress, lbl_Progress)
            If UploadBerhasil Then
                lbl_Report_3.Text = "Backup project berhasil diupload ke server."
            Else
                lbl_Report_3.Foreground = WarnaMerahSolid_WPF
                lbl_Report_3.Text = "Backup project gagal diupload ke server...!!!"
            End If
        End If

        If Not UploadBerhasil Then
            pgb_Progress.Foreground = WarnaPeringatan_WPF
        End If

    End Sub

    Sub ResetForm()

        pgb_Progress.Value = 0
        ProsentaseProgress = 0
        pgb_Progress.Value = 0
        pgb_Progress.Foreground = WarnaHijauProgressBar_WPF
        lbl_Progress.Text = String.Empty
        lbl_Report_1.Text = String.Empty
        lbl_Report_2.Text = String.Empty
        lbl_Report_3.Text = String.Empty
        lbl_Report_4.Text = String.Empty
        lbl_Report_1.Visibility = Visibility.Collapsed
        lbl_Report_2.Visibility = Visibility.Collapsed
        lbl_Report_3.Visibility = Visibility.Collapsed
        lbl_Report_4.Visibility = Visibility.Collapsed
        lbl_Report_1.Foreground = WarnaTeksStandar_WPF
        lbl_Report_2.Foreground = WarnaTeksStandar_WPF
        lbl_Report_3.Foreground = WarnaTeksStandar_WPF
        lbl_Report_4.Foreground = WarnaTeksStandar_WPF

    End Sub


    Public Function TanggalFormatMySQL(TanggalKiriman As String) As String
        If TanggalKiriman = Kosongan Then TanggalKiriman = "1900-01-01"
        Dim TanggalMentah As Date = TanggalKiriman
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Tahun & "-" & Bulan & "-" & Hari
        Return TanggalHasil_KirimBalik
    End Function



    Sub New()

        InitializeComponent()

    End Sub

End Class
