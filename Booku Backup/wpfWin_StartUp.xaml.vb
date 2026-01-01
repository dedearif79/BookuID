Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports MySql.Data.MySqlClient
Imports bcomm

Class MainWindow

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

    End Sub


    Sub ResetForm()
        btn_BackupFileProject.IsEnabled = True
        btn_BackupDatabase.IsEnabled = True
    End Sub

    Private Sub btn_BackupFileProject_Click(sender As Object, e As RoutedEventArgs) Handles btn_BackupFileProject.Click


        btn_BackupFileProject.IsEnabled = False
        Terabas()

        Dim win_Progress As New wpfWin_Progress
        win_Progress.ShowDialog()


        btn_BackupFileProject.IsEnabled = True


    End Sub

    Dim NamaDatabaseDasar As String = "dbsat_dasar"
    Private Sub btn_BackupDatabase_Click(sender As Object, e As RoutedEventArgs) Handles btn_BackupDatabase.Click

        Dim NamaDatabaseDasar As String = "dbsat_dasar"
        Dim LokasiServerDatabase = "localhost"
        Dim UserDatabase = "root"
        Dim PasswordDatabase = "root"

        Dim NamaFileBackup_SQL As String = "C:\backup.sql"

        Dim KoneksiBackupDb As MySqlConnection
        Dim cmdBackupDb As MySqlCommand

        KoneksiBackupDb = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";username=" & UserDatabase & ";password=" & PasswordDatabase & ";SSL Mode=None")
        cmdBackupDb = KoneksiBackupDb.CreateCommand
        cmdBackupDb.CommandText = "BACKUP DATABASE " & NamaDatabaseDasar & " TO DISK='" & NamaFileBackup_SQL & "';"

        Try
            KoneksiBackupDb.Open()
            MsgBox("Koneksi BERHASIL")
        Catch ex As Exception
            MsgBox("Koneksi GAGAL")
        End Try


        Try
            cmdBackupDb = New MySqlCommand("BACKUP DATABASE " & NamaDatabaseDasar & " TO DISK='" & NamaFileBackup_SQL & "';", KoneksiBackupDb)
            cmdBackupDb.ExecuteNonQuery()
            MsgBox("Backup BERHASIL.")
        Catch ex As Exception
            MsgBox("Backup GAGAL.")
        End Try

        KoneksiBackupDb.Close()

    End Sub

    Sub New()
        InitializeComponent()
        Me.SizeToContent = SizeToContent.WidthAndHeight
        Me.WindowStartupLocation = WindowStartupLocation.CenterScreen
        Me.WindowStyle = WindowStyle.SingleBorderWindow
        Me.ResizeMode = ResizeMode.NoResize
    End Sub

End Class
