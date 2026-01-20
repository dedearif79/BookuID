Imports MySql.Data.MySqlClient

Public Module mdlPub_KoneksiDatabase

    '------------------ Koneksi Database Public : --------------------------------------------------------------------------------
    Public LokasiServerDatabasePublic As String = "69.30.235.140" '(https://www.sqlclusters.com)
    Public PortDatabasePublic As String = "10206"
    Public UserDatabasePublic As String = "admin"
    Public PasswordDatabasePublic_Enk As String = "Rl1oJ2TP7<YZvv.nsA!}~x4j&dmXPV3!*{6a_,zd@jyn6I]Or!O7kR5qg{(%(R10?t,A/=FLfy=[a)aLpTEosB[J_SBBDNIQj0$Jdld0ODQ[)?ZU.T/KZGGN6^oc05i*7Z?I5hcE;ZdX7T32d[Jy68Aw:8}H4_d7VaKb2$t3@5=fHhJNUrspJGIvy+?}!]2JPH54;JswR/#"
    Public PasswordDatabasePublic As String = DekripsiTeks(PasswordDatabasePublic_Enk)
    '-----------------------------------------------------------------------------------------------------------------------------

    Public AwalanDatabase_BookuID_Booku As String = "bookuid_booku_"
    Public NamaDatabase_BookuID_Booku_Public As String = AwalanDatabase_BookuID_Booku & "public"
    Public KoneksiDatabasePublic As New MySqlConnection
    Public StatusKoneksiDatabasePublic As Boolean
    Public StatusKoneksiDatabase As Boolean
    Public StatusSuntingDatabase As Boolean
    Public cmdPublic As MySqlCommand
    Public drPublic As MySqlDataReader


    Public ftpServerBookuID_Support As String = "ftp://booku.id/booku/support/"
    Public ftpServerBookuID_BackUpDataClient As String = "ftp://booku.id/booku/backup/client/"
    Public ftpUsernameBooku As String = "u438659811.booku.id"
    Public ftpPasswordBooku As String = "RahasiaDedearif79"

    'Alamat Folder :
    Public urlFolderServerBookuID_Support As String = "https://booku.id/booku/support/"
    Public urlFolderServerBookuID_BackUpDataClient As String = "https://booku.id/booku/backup/client/"
    Public urlFolderServerBookuID_BackUpProject As String = "https://booku.id/booku/backup/project/"
    Public urlFolderPHPCode As String = "https://booku.id/booku/phpcode/"

    'Url PHP Code :
    Public urlFileUploader_PHP As String = urlFolderPHPCode & "uploader.php"
    Public urlFileDownloader_PHP As String = urlFolderPHPCode & "downloader.php"
    Public urlFileDownloaderViaToken_PHP As String = urlFolderPHPCode & "get_token.php"
    Public urlFileUplaodChunk_PHP As String = urlFolderPHPCode & "upload_chunk.php"
    Public urlFileMergeChunks_PHP As String = urlFolderPHPCode & "merge_chunks.php"
    Public urlFileRenamer_PHP As String = urlFolderPHPCode & "renamer.php"


    Public Sub BukaDatabasePublic()
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabasePublic &
            ";DATABASE =" & NamaDatabase_BookuID_Booku_Public &
            ";USERNAME=" & UserDatabasePublic &
            ";PASSWORD=" & PasswordDatabasePublic &
            ";PORT=" & PortDatabasePublic &
            ";Connection Timeout=10" & ";"
        Try
            KoneksiDatabasePublic = New MySqlConnection(strKoneksi)
            KoneksiDatabasePublic.Open()
            StatusKoneksiDatabasePublic = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            'If StatusLogin = True Then
            '    pesan_AdaMasalahDenganKoneksiInternet()
            'End If
        End Try
    End Sub


    Public Sub TutupDatabasePublic()
        KoneksiDatabasePublic.Close()
    End Sub

    Public Sub cmdPublic_ExecuteNonQuery()
        Try
            cmdPublic.ExecuteNonQuery()
            StatusKoneksiDatabasePublic = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub drPublic_ExecuteReader()
        Try
            drPublic = cmdPublic.ExecuteReader
            StatusKoneksiDatabasePublic = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Transaction_Public As MySqlTransaction
    Sub TransactionBegin_Public()
        Transaction_Public = KoneksiDatabasePublic.BeginTransaction
    End Sub
    Sub TransactionCommit_Public()
        Try
            Transaction_Public.Commit()
            StatusSuntingDatabase = True
        Catch ex As Exception
            TransactionRollback_Public()
            StatusSuntingDatabase = False
        End Try
    End Sub
    Sub TransactionRollback_Public()
        Transaction_Public.Rollback()
    End Sub

    Public Sub cmdPublic_ExecuteNonQuery_Transaction()
        Try
            cmdPublic.Transaction = Transaction_Public
            cmdPublic.ExecuteNonQuery()
            StatusKoneksiDatabasePublic = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabasePublic = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            MsgBox(ex.Message)
        End Try
    End Sub




End Module
