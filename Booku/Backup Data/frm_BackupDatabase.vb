Imports System
Imports System.IO
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Diagnostics
Imports MySql.Data.MySqlClient

Public Class frm_BackupDatabase

    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean

    Private Sub frm_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub


    Private Sub btn_Backup_Click(sender As Object, e As EventArgs) Handles btn_Backup.Click

        ProsesBackup_3()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click

    End Sub

    Sub ProsesBackup_4()

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * INTO OUTFILE 'cadangan.sql' FROM tbl_COA ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Berhasil.")
        Catch ex As Exception
            MsgBox("Gagal.")
        End Try
        TutupDatabaseDasar()

    End Sub

    Sub ProsesBackup()

        'sfd_Simpan.FileName = Nothing
        'sfd_Simpan.Filter = "SQL Files (*.sql)|*.sql"
        'If sfd_Simpan.ShowDialog = System.Windows.Forms.DialogResult.Cancel Then Return

        Dim NamaDatabaseYangAkanDiBackup = "db_bookuid_booku_rinduord_gen"
        'Dim NamaFileBackup_SQL As String = sfd_Simpan.FileName
        Dim NamaFileBackup_SQL = "cadangan.sql"

        MsgBox(LokasiServerDatabase)

        Dim KoneksiBackupDb As MySqlConnection
        Dim cmdBackupDb As MySqlCommand
        Dim strBackupDb As String

        KoneksiBackupDb = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";username=" & UserDatabase & ";password=" & PasswordDatabase & ";SSL Mode=None")
        cmdBackupDb = KoneksiBackupDb.CreateCommand
        strBackupDb = "BACKUP DATABASE " & NamaDatabaseYangAkanDiBackup & " TO DISK '" & NamaFileBackup_SQL & "' "
        cmdBackupDb.CommandText = strBackupDb

        Try
            KoneksiBackupDb.Open()
            MsgBox("Koneksi BERHASIL.")
        Catch ex As Exception
            MsgBox("Koneksi GAGAL.")
        End Try

        Try
            cmdBackupDb.ExecuteNonQuery()
            KoneksiBackupDb.Close()
            MsgBox("Backup BERHASIL.")
        Catch ex As Exception
            MsgBox("Backup GAGAL.")
        End Try

    End Sub

    Sub ProsesBackup_2()

        Dim fileName As String = "C:\xampp\mysql\bin\mysqldump.exe" ' Ganti dengan path file mysqldump.exe
        Dim server As String = LokasiServerDatabase  ' Ganti dengan nama atau alamat IP server MySQL
        Dim username As String = "root" ' Ganti dengan username MySQL
        Dim password As String = "root" ' Ganti dengan password MySQL
        Dim database As String = "db_bookuid_booku_rinduord_gen" ' Ganti dengan nama database yang ingin di-backup
        Dim outputFileName As String = "cadangan.sql" ' Ganti dengan path dan nama file output backup

        Dim arguments As String = String.Format("-u{0} -p{1} -h{2} {3} > {4}", username, password, server, database, outputFileName)

        Dim processInfo As New ProcessStartInfo(fileName, arguments)
        processInfo.CreateNoWindow = True
        processInfo.UseShellExecute = False

        Dim process As Process = process.Start(processInfo)
        process.WaitForExit()

        MessageBox.Show("Backup selesai!")

    End Sub

    Sub ProsesBackup_3()

        ' Mengatur koneksi ODBC
        Dim connectionString As String = "DRIVER=" & Odbc52Driver & ";SERVER=127.0.0.1;DATABASE=db_bookuid_booku_dasar;USER=root;PASSWORD=root;"
        Dim connection As New OdbcConnection(connectionString)

        ' Membuka koneksi
        Try
            connection.Open()
            MsgBox("Koneksi BERHASIL")
        Catch ex As Exception
            MsgBox("Koneksi GAGAL")
        End Try

        ' Menjalankan perintah backup
        Dim command As New OdbcCommand("BACKUP DATABASE db_bookuid_booku_dasar TO DISK 'cadangan.sql' ", connection)
        Try
            command.ExecuteNonQuery()
            MsgBox("Backup BERHASIL.")
        Catch ex As Exception
            MsgBox("Backup GAGAL.")
        End Try

        ' Menutup koneksi
        connection.Close()

    End Sub

End Class