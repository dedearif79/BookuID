Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.IO
Imports MySql.Data.MySqlClient
Imports bcomm

Module mdl_KoneksiDatabase

    Public DataKoneksi
    Public DataRegistrasiPerangkat
    Public DataVersiDanApdetAplikasi
    Public FilePathDataKoneksi As String
    Public FilePathRegistrasiPerangkat As String
    Public FilePathRegistrasiPerangkat_Backup As String
    Public FilePathVersiDanApdetAplikasi As String
    Public FileEksis As Boolean
    Public StatusKoneksiTesKoneksiMySQL As Boolean
    Public StatusKoneksiTesKoneksiDbSAT As Boolean
    Public StatusKoneksiDatabaseDasar As Boolean
    Public StatusKoneksiDatabaseGeneral As Boolean
    Public StatusKoneksiDatabaseGeneral_MySQL As Boolean
    Public StatusKoneksiDatabaseTransaksi As Boolean
    Public StatusKoneksiDatabaseTransaksi_MySQL As Boolean
    Public StatusKoneksiDatabaseTransaksi_Alternatif As Boolean
    Public KonfigurasiKoneksiDatabaseSudahTersimpan As Boolean
    Public LokasiServerDatabase As String
    Public PortApache As String = "8787"
    Public urlLocalhost As String
    Public urlPhpMyAdmin As String
    Public urlTelegram As String = "https://web.telegram.org/a/"
    Public urlWhatsApp As String = "https://web.whatsapp.com/"
    Public urlChatGPT As String = "https://chatgpt.com/"
    Public urlGmail As String = "https://mail.google.com/mail/u/0/#inbox"
    Public urlYouTube As String = "https://www.youtube.com/"
    Public urlFacebook As String = "https://www.facebook.com/"
    Public PortDatabase As String
    Public UserDatabase As String
    Public PasswordDatabase As String
    Public LokasiServerDatabaseTesKoneksiDbSAT As String
    Public PortServerTesKoneksiDbSAT As String
    Public UserDatabaseTesKoneksiDbSAT As String
    Public PasswordDatabaseTesKoneksiDbSAT As String
    Public UserDatabaseTesKoneksiMySQL As String = "UserApaAjaBoleh..!!!Hehe" 'Ini koneksi bebas akses. Lintas user, bahkan tanpa harus ada user, bahkan bisa ketik USER SEMBARANG.
    Public PasswordDatabaseTesKoneksiMySQL As String = Nothing 'Ini malah harus kosong.!!! Tak boleh ada Password.
    Public Odbc52Driver = "MySQL ODBC 5.2 Driver"
    Public Odbc80Driver = "MySQL ODBC 8.0 ANSI Driver"
    Public OdbcDriver = Odbc80Driver
    Public dsn_Public = "dsn_SAT_Public"
    Public dsn_TesKoneksiMySQL = "dsn_TesKoneksiMySQL"
    Public dsn_DataGeneral
    Public dsn_DataTransaksi
    Public dsn_DataTransaksi_TahunLain
    Public Awalan_dsn = "dsn_SAT_"
    Public NamaDatabaseDasar As String = AwalanDatabase_BookuID_Booku & "dasar"
    Public NamaDatabaseGeneral As String 'Jangan diisi apa-apa...!!!
    Public NamaDatabaseTransaksi As String
    Public NamaDatabaseTransaksi_TahunLain As String
    Public NamaDatabaseTesKoneksiMySQL As String = "test"
    Public NamaDatabaseTesKoneksiDbSAT As String = AwalanDatabase_BookuID_Booku & "teskoneksi"
    Public TahunBuku_Alternatif
    Public TahunBukuBaru
    Public TahunBukuAktif
    Public TahunBukuAktifAsli
    Public AwalTahunBukuAktif As Date
    Public TahunBukuKemarin
    Public TahunBukuTerakhirDibuka
    Public AkhirTahunBukuKemarin As Date
    Public BulanBukuAktif As Integer
    Public BulanTerakhirDitutup As Integer
    Public HariIniTahunBukuKemarin As Date
    Public HasilPembuatanDatabaseDasar As Boolean
    Public HasilPembuatanDatabaseGeneral As Boolean
    Public HasilPembuatanDatabaseTransaksi As Boolean
    Public LokasiFolderXAMPP
    Public KomputerSebagaiServerXAMPP As Boolean
    Public LokasiFolderLaragon = "C:\laragon\bin\mysql\mysql-8.0.30-winx64\bin\"
    Public KoneksiDatabaseDasar As New MySqlConnection
    Public KoneksiTesKoneksiMySQL As New MySqlConnection
    Public KoneksiTesKoneksiDbSAT As New MySqlConnection
    Public KoneksiDatabaseGeneral As New OdbcConnection
    Public KoneksiDatabaseGeneral_MySQL As New MySqlConnection
    Public KoneksiDatabaseTransaksi As New OdbcConnection
    Public KoneksiDatabaseTransaksi_MySQL As New MySqlConnection
    Public KoneksiDatabaseTransaksi_Alternatif As New OdbcConnection
    Public KoneksiDatabaseKhusus As New OdbcConnection

    Public ProsesSimpanData As Boolean

    Public Start_dbEngineBerhasil As Boolean
    Sub Start_dbENGINE()
        Dim po As New Process
        po.StartInfo.FileName = LokasiFolderXAMPP & "xampp_start.exe"
        'po.StartInfo.FileName = LokasiFolderLaragon & "mysql.exe"
        po.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Try
            po.Start()
            Threading.Thread.Sleep(333)
            KomputerSebagaiServerXAMPP = True
            Start_dbEngineBerhasil = True
        Catch ex As Exception
            KomputerSebagaiServerXAMPP = False
            Start_dbEngineBerhasil = False
        End Try
    End Sub

    Public Stop_dbEngineBerhasil As Boolean
    Sub Stop_dbENGINE()
        Dim po As New Process
        po.StartInfo.FileName = LokasiFolderXAMPP & "xampp_stop.exe"
        po.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Try
            po.Start()
            Threading.Thread.Sleep(333)
            KomputerSebagaiServerXAMPP = True
            Stop_dbEngineBerhasil = True
        Catch ex As Exception
            KomputerSebagaiServerXAMPP = False
            Stop_dbEngineBerhasil = False
        End Try
    End Sub

    Sub PengaturanKoneksi()
        win_Pengaturan = New wpfWin_Pengaturan
        win_Pengaturan.FungsiForm = "TES KONEKSI"
        win_Pengaturan.ShowDialog()
    End Sub

    Public Sub BukaDatabaseDasar()
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabase &
            ";DATABASE =" & NamaDatabaseDasar &
            ";USERNAME=" & UserDatabase &
            ";PASSWORD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Try
            KoneksiDatabaseDasar = New MySqlConnection(strKoneksi)
            KoneksiDatabaseDasar.Open()
            StatusKoneksiDatabaseDasar = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabaseDasar = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
        End Try
    End Sub

    Public Sub TutupDatabaseDasar()
        KoneksiDatabaseDasar.Close()
    End Sub

    Public Sub BukaTesKoneksiMySQL()
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabase &
            ";DATABASE =" & NamaDatabaseTesKoneksiMySQL &
            ";USERNAME=" & UserDatabaseTesKoneksiMySQL &
            ";PASSWORD=" & PasswordDatabaseTesKoneksiMySQL &
            ";PORT=" & PortDatabase & ";"
        Try
            KoneksiTesKoneksiMySQL = New MySqlConnection(strKoneksi)
            KoneksiTesKoneksiMySQL.Open()
            StatusKoneksiTesKoneksiMySQL = True
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiTesKoneksiMySQL = False
            StatusKoneksiDatabase = False
        End Try
    End Sub

    Public Sub TutupTesKoneksiMySQL()
        KoneksiTesKoneksiMySQL.Close()
    End Sub

    'Membuat/Mengedit Dsn Database General :
    Sub BuatDsnGeneral()
        dsn_DataGeneral = Awalan_dsn & ID_Customer & "_Gen"
        NamaDatabaseGeneral = AwalanDatabase_BookuID_Booku & ID_Customer & "_gen"
        Dim po As New Process
        Dim CreatePo As String
        CreatePo = "-s -a -c1 -n """ & dsn_DataGeneral &
            """ -t ""DRIVER=" & OdbcDriver & ";SERVER=" & LokasiServerDatabase &
            ";DATABASE=" & NamaDatabaseGeneral &
            ";UID=" & UserDatabase &
            ";PWD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Select Case OdbcDriver
            Case Odbc52Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 5.2\myodbc-installer.exe"
            Case Odbc80Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 8.0\myodbc-installer.exe"
        End Select
        po.StartInfo.Arguments = CreatePo
        po.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        po.Start()
        po.WaitForExit()
        po.Close()
    End Sub

    'Membuat/Mengedit Dsn Database Transaksi :
    Sub BuatDsnTransaksi(TahunBuku)
        dsn_DataTransaksi = Awalan_dsn & ID_Customer & "_" & TahunBuku
        NamaDatabaseTransaksi = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunBuku
        Dim po As New Process
        Dim CreatePo As String
        CreatePo = "-s -a -c1 -n """ & dsn_DataTransaksi &
            """ -t ""DRIVER=" & OdbcDriver & ";SERVER=" & LokasiServerDatabase &
            ";DATABASE=" & NamaDatabaseTransaksi &
            ";UID=" & UserDatabase &
            ";PWD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Select Case OdbcDriver
            Case Odbc52Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 5.2\myodbc-installer.exe"
            Case Odbc80Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 8.0\myodbc-installer.exe"
        End Select
        po.StartInfo.Arguments = CreatePo
        po.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        po.Start()
        po.WaitForExit()
        po.Close()
    End Sub

    'Membuat/Mengedit Dsn Database Transaksi Tahun Lain:
    Sub BuatDsnTransaksi_TahunLain(TahunDsn)
        dsn_DataTransaksi_TahunLain = Awalan_dsn & ID_Customer & "_" & TahunDsn
        NamaDatabaseTransaksi_TahunLain = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunDsn
        Dim po As New Process
        Dim CreatePo As String
        CreatePo = "-s -a -c1 -n """ & dsn_DataTransaksi_TahunLain &
            """ -t ""DRIVER=" & OdbcDriver & ";SERVER=" & LokasiServerDatabase &
            ";DATABASE=" & NamaDatabaseTransaksi_TahunLain &
            ";UID=" & UserDatabase &
            ";PWD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Select Case OdbcDriver
            Case Odbc52Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 5.2\myodbc-installer.exe"
            Case Odbc80Driver
                po.StartInfo.FileName = "C:\Program Files (x86)\MySQL\Connector ODBC 8.0\myodbc-installer.exe"
        End Select
        po.StartInfo.Arguments = CreatePo
        po.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        po.Start()
        po.WaitForExit()
        po.Close()
    End Sub

    Public TahunPajak
    Public TahunPajakSamaDenganTahunBukuAktif As Boolean = True
    Public Sub BuatDsnTransaksiSementaraSesuaiTahunPajak()
        TahunBukuAktifAsli = TahunBukuAktif
        TahunBukuAktif = TahunPajak
        BuatDsnTransaksi(TahunPajak)
    End Sub

    Public Sub PulihkanDsnTransaksiAsli()
        TahunBukuAktif = TahunBukuAktifAsli
        BuatDsnTransaksi(TahunBukuAktifAsli)
    End Sub


    'KONEKSI DATABASE MYSQL ODBC
    Public cmd As OdbcCommand
    Public dr As OdbcDataReader
    Public da As OdbcDataAdapter
    Public dt As DataTable
    Public ds As DataSet
    Public Kueri As String

    Public cmdDasar As MySqlCommand
    Public drDasar As MySqlDataReader

    Public Sub dr_ExecuteReader()
        Try
            dr = cmd.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public Sub dr_Read()
        dr_ExecuteReader()
        Try
            dr.Read()
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public Sub cmd_ExecuteNonQuery()
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public Sub dbBACA(Kueri As String, KoneksiDb As OdbcConnection)
        cmd = New OdbcCommand(Kueri, KoneksiDb)
        Try
            dr = cmd.ExecuteReader
            dr.Read()
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            MsgBox("Ups..! Ada kesalahan teknis database..!" & Enter2Baris &
                   "Kueri : " & Kueri)
        End Try
    End Sub
    Public Sub dbBACA_Loop(Kueri As String, KoneksiDb As OdbcConnection)
        cmd = New OdbcCommand(Kueri, KoneksiDb)
        Try
            dr = cmd.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            MsgBox("Ups..! Ada kesalahan teknis database..!" & Enter2Baris &
                   "Kueri :" & Enter1Baris & Kueri)
        End Try
    End Sub

    Public QueryTelusur
    Public cmdTELUSUR As OdbcCommand
    Public drTELUSUR As OdbcDataReader
    Public Sub drTELUSUR_ExecuteReader()
        Try
            drTELUSUR = cmdTELUSUR.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public QueryTelusur2
    Public cmdTELUSUR2 As OdbcCommand
    Public drTELUSUR2 As OdbcDataReader
    Public Sub drTELUSUR2_ExecuteReader()
        Try
            drTELUSUR2 = cmdTELUSUR2.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public QueryTelusur3
    Public cmdTELUSUR3 As OdbcCommand
    Public drTELUSUR3 As OdbcDataReader
    Public Sub drTELUSUR3_ExecuteReader()
        Try
            drTELUSUR3 = cmdTELUSUR3.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub


    Public cmdSIMPAN As OdbcCommand
    Public Sub cmdSIMPAN_ExecuteNonQuery()
        Try
            cmdSIMPAN.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub
    Public Sub dbSIMPAN(Kueri As String, KoneksiDb As OdbcConnection)
        cmdSIMPAN = New OdbcCommand(Kueri, KoneksiDb)
        Try
            cmdSIMPAN.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            MsgBox("Ups..! Ada kesalahan teknis database..!" & Enter2Baris &
                   "Kueri :" & Enter1Baris & Kueri)
        End Try
    End Sub


    Public cmdEDIT As OdbcCommand
    Public Sub cmdEDIT_ExecuteNonQuery()
        Try
            cmdEDIT.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub
    Public Sub dbEDIT(Kueri As String, KoneksiDb As OdbcConnection)
        cmdEDIT = New OdbcCommand(Kueri, KoneksiDb)
        Try
            cmdEDIT.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            MsgBox("Ups..! Ada kesalahan teknis database..!" & Enter2Baris &
                   "Kueri :" & Enter1Baris & Kueri)
        End Try
    End Sub


    Public cmdHAPUS As OdbcCommand
    Public Sub cmdHAPUS_ExecuteNonQuery()
        Try
            cmdHAPUS.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub
    Public Sub cmdHAPUS_ExecuteNonQuery_Transaction_Transaksi()
        Try
            cmdHAPUS.Transaction = Transaction_Transaksi
            cmdHAPUS.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub
    Public Sub cmdHAPUS_ExecuteNonQuery_Transaction_General()
        Try
            cmdHAPUS.Transaction = Transaction_General
            cmdHAPUS.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public Sub dbHAPUS(Kueri As String, KoneksiDb As OdbcConnection)
        cmdSIMPAN = New OdbcCommand(Kueri, KoneksiDb)
        Try
            cmdSIMPAN.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            MsgBox("Ups..! Ada kesalahan teknis database..!" & Enter2Baris &
                   "Kueri :" & Enter1Baris & Kueri)
        End Try
    End Sub


    Public cmdTAGIHAN As OdbcCommand
    Public drTAGIHAN As OdbcDataReader
    Public Sub drTAGIHAN_ExecuteReader()
        Try
            drTAGIHAN = cmdTAGIHAN.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub

    Public cmdBAYAR As OdbcCommand
    Public drBAYAR As OdbcDataReader
    Public Sub drBAYAR_ExecuteReader()
        Try
            drBAYAR = cmdBAYAR.ExecuteReader
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabase = False
            pesan_AdaKesalahanTeknis_Database(ex.Message)
        End Try
    End Sub


    'Akses Database General : --------------------------------------------------------------------------------------------
    Sub AksesDatabase_General(Kondisi As String)
        Select Case Kondisi
            Case Buka
                Dim dsn
                dsn = "dsn=" & dsn_DataGeneral
                KoneksiDatabaseGeneral = New OdbcConnection(dsn)
                StatusKoneksiDatabaseGeneral = False 'Defaultnya false dulu, sehingga ketika gagal koneksi, kembali ke value default.
                Try
                    KoneksiDatabaseGeneral.Open()
                    StatusKoneksiDatabaseGeneral = True
                    StatusKoneksiDatabase = True
                    StatusSuntingDatabase = True
                Catch ex As Exception
                    StatusKoneksiDatabaseGeneral = False
                    StatusKoneksiDatabase = False
                    StatusSuntingDatabase = False
                    If StatusLogin = True Then
                        pesan_AdaMasalahDenganKoneksiDatabase()
                    End If
                End Try
            Case Tutup
                KoneksiDatabaseGeneral.Close()
                StatusKoneksiDatabase = False
            Case Else
                PesanUntukProgrammer(
                    "Kesalahan Parameter!" & Enter2Baris &
                    "Parameter hanya mengenal string 'Buka' dan 'Tutup'.")
        End Select
    End Sub
    '---------------------------------------------------------------------------------------------------------------------


    Sub BukaDatabaseGeneral_MySQL()
        NamaDatabaseGeneral = AwalanDatabase_BookuID_Booku & ID_Customer & "_gen"
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabase &
            ";DATABASE =" & NamaDatabaseGeneral &
            ";USERNAME=" & UserDatabase &
            ";PASSWORD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Try
            KoneksiDatabaseGeneral_MySQL = New MySqlConnection(strKoneksi)
            KoneksiDatabaseGeneral_MySQL.Open()
            StatusKoneksiDatabaseGeneral_MySQL = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabaseGeneral_MySQL = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            If StatusLogin = True Then
                pesan_AdaMasalahDenganKoneksiDatabase()
            End If
        End Try
    End Sub
    Sub TutupDatabaseGeneral_MySQL()
        KoneksiDatabaseGeneral_MySQL.Close()
    End Sub

    Sub RefreshInfo_StatusKoneksiDatabaseGeneral()
        If KoneksiDatabaseGeneral.State = ConnectionState.Open Then
            StatusKoneksiDatabaseGeneral = True
        Else
            StatusKoneksiDatabaseGeneral = False
        End If
    End Sub


    'Akses Database Transaksi --------------------------------------------------------------------------------------------
    Sub AksesDatabase_Transaksi(Kondisi As String)
        Select Case Kondisi
            Case Buka
                Dim dsn
                dsn = "dsn=" & dsn_DataTransaksi
                KoneksiDatabaseTransaksi = New OdbcConnection(dsn)
                KoneksiDatabaseTransaksi.Close() 'Kode ini untuk antisipasi kemungkinan database belum tertutup.
                Try
                    KoneksiDatabaseTransaksi.Open()
                    StatusKoneksiDatabaseTransaksi = True
                    StatusKoneksiDatabase = True
                    StatusSuntingDatabase = True
                Catch ex As Exception
                    StatusKoneksiDatabaseTransaksi = False
                    StatusKoneksiDatabase = False
                    StatusSuntingDatabase = False
                    If StatusLogin = True Then
                        pesan_AdaMasalahDenganKoneksiDatabase()
                    End If
                End Try
            Case Tutup
                KoneksiDatabaseTransaksi.Close()
                StatusKoneksiDatabaseTransaksi = False
            Case Else
                PesanUntukProgrammer(
                    "Kesalahan Parameter!" & Enter2Baris &
                    "Parameter hanya mengenal string 'Buka' dan 'Tutup'.")
        End Select
    End Sub
    '---------------------------------------------------------------------------------------------------------------------


    Public cmdMySQL As MySqlCommand
    Public drMySQL As MySqlDataReader
    Sub BukaDatabaseTransaksi_MySQL()
        NamaDatabaseTransaksi = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunBukuAktif
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabase &
            ";DATABASE =" & NamaDatabaseTransaksi &
            ";USERNAME=" & UserDatabase &
            ";PASSWORD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Try
            KoneksiDatabaseTransaksi_MySQL = New MySqlConnection(strKoneksi)
            KoneksiDatabaseTransaksi_MySQL.Open()
            StatusKoneksiDatabaseTransaksi_MySQL = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabaseTransaksi_MySQL = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            If StatusLogin = True Then
                pesan_AdaMasalahDenganKoneksiDatabase()
            End If
        End Try
    End Sub

    Sub TutupDatabaseTransaksi_MySQL()
        KoneksiDatabaseTransaksi_MySQL.Close()
    End Sub


    Public KoneksiDatabaseTransaksi_Alternatif_MySQL As New MySqlConnection
    Public StatusKoneksiDatabaseTransaksi_Alternatif_MySQL As Boolean
    Sub BukaDatabaseTransaksi_Alternatif_MySQL(TahunBuku As Integer)
        NamaDatabaseTransaksi = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunBuku
        Dim strKoneksi =
            "DATA SOURCE =" & LokasiServerDatabase &
            ";DATABASE =" & NamaDatabaseTransaksi &
            ";USERNAME=" & UserDatabase &
            ";PASSWORD=" & PasswordDatabase &
            ";PORT=" & PortDatabase & ";"
        Try
            KoneksiDatabaseTransaksi_Alternatif_MySQL = New MySqlConnection(strKoneksi)
            KoneksiDatabaseTransaksi_Alternatif_MySQL.Open()
            StatusKoneksiDatabaseTransaksi_Alternatif_MySQL = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabaseTransaksi_Alternatif_MySQL = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            If StatusLogin = True Then
                pesan_AdaMasalahDenganKoneksiDatabase()
            End If
        End Try
    End Sub
    Sub TutupDatabaseTransaksi_Alternatif_MySQL()
        KoneksiDatabaseTransaksi_Alternatif_MySQL.Close()
    End Sub



    Sub RefreshInfo_StatusKoneksiDatabaseTransaksi()
        If KoneksiDatabaseTransaksi.State = ConnectionState.Open Then
            StatusKoneksiDatabaseTransaksi = True
        Else
            StatusKoneksiDatabaseTransaksi = False
        End If
    End Sub

    Sub BukaDatabaseTransaksi_Alternatif(TahunAlternatif)
        If TahunAlternatif < TahunCutOff Then TahunAlternatif = TahunCutOff
        BuatDsnTransaksi_TahunLain(TahunAlternatif)
        Dim dsn
        dsn = "dsn=" & dsn_DataTransaksi_TahunLain
        KoneksiDatabaseTransaksi_Alternatif = New OdbcConnection(dsn)
        KoneksiDatabaseTransaksi_Alternatif.Close() 'Kode ini untuk antisipasi kemungkinan database belum tertutup.
        Try
            KoneksiDatabaseTransaksi_Alternatif.Open()
            StatusKoneksiDatabaseTransaksi_Alternatif = True
            StatusKoneksiDatabase = True
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusKoneksiDatabaseTransaksi_Alternatif = False
            StatusKoneksiDatabase = False
            StatusSuntingDatabase = False
            If StatusLogin = True Then
                pesan_AdaMasalahDenganKoneksiDatabase()
            End If
        End Try
    End Sub

    Sub TutupDatabaseTransaksi_Alternatif()
        KoneksiDatabaseTransaksi_Alternatif.Close()
        StatusKoneksiDatabaseTransaksi_Alternatif = False
    End Sub

    Sub RefreshInfo_StatusKoneksiDatabaseTransaksi_Alternatif()
        If KoneksiDatabaseTransaksi_Alternatif.State = ConnectionState.Open Then
            StatusKoneksiDatabaseTransaksi_Alternatif = True
        Else
            StatusKoneksiDatabaseTransaksi_Alternatif = False
        End If
    End Sub

    Sub BukaDatabaseTransaksiGeneral()
        AksesDatabase_Transaksi(Buka)
        AksesDatabase_General(Buka)
    End Sub
    Sub TutupDatabaseTransaksiGeneral()
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)
    End Sub


    'BUKA/TUTUP DATABASE KONDISIONAL :
    'Buka/Tutup Database Kondisional meliputi :
    '- Database General
    '- Database Transaksi
    '- Database Transaksi Alternatif
    'Kenapa mesti ada Buka/Tutup Database Kondisional..?
    'Karena database ini dibuka dan/atau ditutup berdasarkan kondisi sebelumnya.
    'Jika status suatu koneksi database dalam kondisi sudah terbuka, maka dia tidak perlu dibuka lagi.
    'Kemudian setelah selesai kebutuhan pengaksesan database dia harus dibiarkan dalam kondisi tetap terbuka dan tidak boleh ditutup, karena masih ada kebutuhan pengaksesan database dari sub lainnya.
    'Jika status suatu koneksi database dalam kondisi tertutup, maka dia harus membukanya.
    'Kemudian setelah selesai kebutuhan pengaksesan database dia harus ditutup lagi, dan jangan dibiarkan terbuka.
    Public StatusKoneksiDatabaseTransaksi_Kondisional
    Sub BukaDatabaseTransaksi_Kondisional()
        RefreshInfo_StatusKoneksiDatabaseTransaksi()
        If StatusKoneksiDatabaseTransaksi = True Then
            StatusKoneksiDatabaseTransaksi_Kondisional = True
            StatusSuntingDatabase = True
        Else
            StatusKoneksiDatabaseTransaksi_Kondisional = False
            StatusSuntingDatabase = False
        End If
        If StatusKoneksiDatabaseTransaksi_Kondisional = False Then AksesDatabase_Transaksi(Buka)
    End Sub
    Sub TutupDatabaseTransaksi_Kondisional()
        If StatusKoneksiDatabaseTransaksi_Kondisional = False Then AksesDatabase_Transaksi(Tutup)
    End Sub
    Public StatusKoneksiDatabaseGeneral_Kondisional
    Sub BukaDatabaseGeneral_Kondisional()
        RefreshInfo_StatusKoneksiDatabaseGeneral()
        If StatusKoneksiDatabaseGeneral = True Then
            StatusKoneksiDatabaseGeneral_Kondisional = True
            StatusSuntingDatabase = True
        Else
            StatusKoneksiDatabaseGeneral_Kondisional = False
            StatusSuntingDatabase = False
        End If
        If StatusKoneksiDatabaseGeneral_Kondisional = False Then AksesDatabase_General(Buka)
    End Sub
    Sub TutupDatabaseGeneral_Kondisional()
        If StatusKoneksiDatabaseGeneral_Kondisional = False Then AksesDatabase_General(Tutup)
    End Sub
    Public StatusKoneksiDatabaseAlternatif_Kondisional
    Sub BukaDatabaseTransaksi_Alternatif_Kondisional(TahunAlternatif)
        RefreshInfo_StatusKoneksiDatabaseTransaksi_Alternatif()
        If StatusKoneksiDatabaseTransaksi_Alternatif = True Then
            StatusKoneksiDatabaseAlternatif_Kondisional = True
            StatusSuntingDatabase = True
        Else
            StatusKoneksiDatabaseAlternatif_Kondisional = False
            StatusSuntingDatabase = False
        End If
        If StatusKoneksiDatabaseAlternatif_Kondisional = False Then BukaDatabaseTransaksi_Alternatif(TahunAlternatif)
    End Sub
    Sub TutupDatabaseTransaksi_Alternatif_Kondisional()
        If StatusKoneksiDatabaseAlternatif_Kondisional = False Then TutupDatabaseTransaksi_Alternatif()
    End Sub


    Sub BuatDatabaseDasar()

        Dim KoneksiBuatDb As MySqlConnection
        Dim cmdBuatDb As MySqlCommand
        Dim strBuatDb As String

        KoneksiBuatDb = New MySqlConnection("Data Source =" & LokasiServerDatabase &
                                            ";port=" & PortDatabase &
                                            ";username=" & UserDatabase &
                                            ";password=" & PasswordDatabase &
                                            ";SSL Mode=None")
        cmdBuatDb = KoneksiBuatDb.CreateCommand
        strBuatDb = " CREATE DATABASE " & NamaDatabaseDasar
        cmdBuatDb.CommandText = strBuatDb
        Try
            KoneksiBuatDb.Open()
            cmdBuatDb.ExecuteNonQuery()
            KoneksiBuatDb.Close()
            HasilPembuatanDatabaseDasar = True
        Catch ex As Exception
            HasilPembuatanDatabaseDasar = False
        End Try

        If HasilPembuatanDatabaseDasar = True Then
            Dim QueryPembuatanTabel
            Dim QueryAlterTable
            QueryPembuatanTabel = " CREATE TABLE `tbl_ListCompany` (" &
                " `ID_Customer` varchar(12) NOT NULL, " &
                " `Nama_Perusahaan` varchar(33) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            BukaDatabaseDasar()
            cmdDasar = New MySqlCommand(QueryPembuatanTabel, KoneksiDatabaseDasar)
            Try
                cmdDasar.ExecuteNonQuery()
                HasilPembuatanDatabaseDasar = True
            Catch ex As Exception
                HasilPembuatanDatabaseDasar = False
            End Try
            QueryAlterTable = " ALTER TABLE `tbl_ListCompany` ADD PRIMARY KEY(`ID_Customer`); "
            cmdDasar = New MySqlCommand(QueryAlterTable, KoneksiDatabaseDasar)
            Try
                cmdDasar.ExecuteNonQuery()
                HasilPembuatanDatabaseDasar = True
            Catch ex As Exception
                HasilPembuatanDatabaseDasar = False
            End Try
            TutupDatabaseDasar()
        End If

    End Sub

    Sub TesKoneksiDbSAT()

        Dim KoneksiTesKoneksidbSAT As MySqlConnection

        'Buat Database TesKoneksiDbSAT dulu :
        KoneksiTesKoneksidbSAT = New MySqlConnection("Data Source =" & LokasiServerDatabaseTesKoneksiDbSAT &
                                                     ";username=" & UserDatabaseTesKoneksiDbSAT &
                                                     ";password=" & PasswordDatabaseTesKoneksiDbSAT &
                                                     ";port=" & PortServerTesKoneksiDbSAT &
                                                     ";SSL Mode=None")
        Try
            KoneksiTesKoneksidbSAT.Open()
            KoneksiTesKoneksidbSAT.Close()
            StatusKoneksiTesKoneksiDbSAT = True
            StatusKoneksiTesKoneksiMySQL = True
            StatusKoneksiDatabase = True
        Catch ex As Exception
            StatusKoneksiTesKoneksiDbSAT = False
            StatusKoneksiTesKoneksiMySQL = False
            StatusKoneksiDatabase = False
        End Try

    End Sub

    Sub BuatDatabaseGeneral()

        Dim KoneksiBuatDb As MySqlConnection
        Dim cmdBuatDb As MySqlCommand
        Dim strBuatDb As String

        'Create New Database : db_bookuid_booku_gen
        KoneksiBuatDb = New MySqlConnection("Data Source =" & LokasiServerDatabase &
                                            ";port=" & PortDatabase &
                                            ";username=" & UserDatabase &
                                            ";password=" & PasswordDatabase &
                                            ";SSL Mode=None")
        cmdBuatDb = KoneksiBuatDb.CreateCommand
        strBuatDb = " CREATE DATABASE " & AwalanDatabase_BookuID_Booku & ID_Customer & "_gen"
        cmdBuatDb.CommandText = strBuatDb
        Try
            KoneksiBuatDb.Open()
            cmdBuatDb.ExecuteNonQuery()
            KoneksiBuatDb.Close()
            HasilPembuatanDatabaseGeneral = True
        Catch ex As Exception
            HasilPembuatanDatabaseGeneral = False
        End Try

        'Buat Kerangka Tabel Database General :
        If HasilPembuatanDatabaseGeneral = True Then
            BuatDsnGeneral()
            PembuatanKerangkaTabelDatabaseGeneral()
        End If

        If HasilPembuatanDatabaseGeneral = False Then
            'Hapus Database jika konfigurasi gagal.
            Dim KoneksiHapusDb As MySqlConnection
            Dim cmdHapusDb As MySqlCommand
            Dim strHapusDb As String
            KoneksiHapusDb = KoneksiBuatDb
            cmdHapusDb = KoneksiHapusDb.CreateCommand
            strHapusDb = " DROP DATABASE " & NamaDatabaseGeneral
            cmdHapusDb.CommandText = strHapusDb
            Try
                KoneksiHapusDb.Open()
                cmdHapusDb.ExecuteNonQuery()
                KoneksiHapusDb.Close()
            Catch ex As Exception
            End Try
        End If

    End Sub

    Sub BuatDatabaseBaruTransaksi(TahunBukuBaru)

        HasilPembuatanDatabaseTransaksi = False 'Defaulnya harus false dulu. Ketika ada masalah di coding, maka kembali ke default (false/gagal).

        'Create New Database : db_bookuid_booku_**** (Database untuk Transaksi/Tahun Buku) :
        Dim KoneksiBuatDb As MySqlConnection
        Dim cmdBuatDb As MySqlCommand
        Dim strBuatDb As String
        Dim NamaDatabaseTransaksiBaru = AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunBukuBaru
        KoneksiBuatDb = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";" &
                                            "port=" & PortDatabase & ";" &
                                            "username=" & UserDatabase & ";" &
                                            "password=" & PasswordDatabase & ";" &
                                            "SSL Mode=None")
        cmdBuatDb = KoneksiBuatDb.CreateCommand
        strBuatDb = " CREATE DATABASE " & NamaDatabaseTransaksiBaru   '(Contoh penerapan : db_bookuid_booku_kjh87j69k_2022)
        cmdBuatDb.CommandText = strBuatDb
        Try
            KoneksiBuatDb.Open()
            cmdBuatDb.ExecuteNonQuery()
            KoneksiBuatDb.Close()
            HasilPembuatanDatabaseTransaksi = True
        Catch ex As Exception
            HasilPembuatanDatabaseTransaksi = False
            KoneksiBuatDb.Close()
        End Try

        'Buat dsn Baru :
        If HasilPembuatanDatabaseTransaksi = True Then
            BuatDsnTransaksi(TahunBukuBaru)
        End If

        'Buat Kerangka tabel untuk Database Tahun Buku Baru :
        If HasilPembuatanDatabaseTransaksi = True Then
            PembuatanKerangkaTabelDatabaseTransaksi()
        End If

        'Pengisian data COA pada Tabel tbl_SaldoAwalCOA dengan value Saldo_Awal 0 (nol) semua
        If HasilPembuatanDatabaseTransaksi = True Then

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_COA ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Dim cmdSimpanCOA As New OdbcCommand
            Dim COA = Nothing
            Do While dr.Read
                COA = dr.Item("COA")
                AksesDatabase_Transaksi(Buka)
                cmdSimpanCOA = New OdbcCommand(" INSERT INTO tbl_SaldoAwalCOA (COA, Saldo_Awal) VALUES ('" & COA & "', 0) ", KoneksiDatabaseTransaksi)
                Try
                    cmdSimpanCOA.ExecuteNonQuery()
                    HasilPembuatanDatabaseTransaksi = True
                Catch ex As Exception
                    HasilPembuatanDatabaseTransaksi = False
                    Exit Do
                End Try
                AksesDatabase_Transaksi(Tutup)
            Loop
            AksesDatabase_General(Tutup)
        End If

        If HasilPembuatanDatabaseTransaksi = True Then
            'Daftarkan Tahun Buku Baru ke Tabel tbl_InfoData :
            Dim ValueTrialBalance
            If JenisTahunBuku_Baru = JenisTahunBuku_LAMPAU Then
                ValueTrialBalance = 1
            Else
                ValueTrialBalance = 0
            End If
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_InfoData VALUES ( " &
                                  " '" & TahunBukuBaru & "', " &
                                  " '" & JenisTahunBuku_Baru & "', " &
                                  " '" & ValueTrialBalance & "', " &
                                  " '" & StatusBuku_OPEN & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)
        Else
            'Hapus Database jika konfigurasi gagal.
            Dim KoneksiHapusDb As MySqlConnection
            Dim cmdHapusDb As MySqlCommand
            Dim strHapusDb As String
            KoneksiHapusDb = KoneksiBuatDb
            cmdHapusDb = KoneksiHapusDb.CreateCommand
            strHapusDb = " DROP DATABASE " & NamaDatabaseTransaksiBaru
            cmdHapusDb.CommandText = strHapusDb
            Try
                KoneksiHapusDb.Open()
                cmdHapusDb.ExecuteNonQuery()
                KoneksiHapusDb.Close()
            Catch ex As Exception
            End Try
        End If

    End Sub


    Public Sub PerintahSqlUtama_Transaksi(ByVal Kueri As String)
        cmd = New OdbcCommand(Kueri, KoneksiDatabaseTransaksi)
    End Sub


    Public daCOA As OdbcDataAdapter
    Public dsCOA As DataSet
    Public Sub IsiDsCOA()
        AksesDatabase_General(Buka)
        daCOA = New OdbcDataAdapter(" SELECT * FROM tbl_COA", KoneksiDatabaseGeneral)
        dsCOA = New DataSet
        daCOA.Fill(dsCOA, "tbl_COA")
        AksesDatabase_General(Tutup)
    End Sub



    Public Transaction_Transaksi As OdbcTransaction
    Sub TransactionBegin_Transaksi()
        Transaction_Transaksi = KoneksiDatabaseTransaksi.BeginTransaction
    End Sub
    Sub TransactionCommit_Transaksi()
        If StatusSuntingDatabase = False Then
            TransactionRollback_Transaksi()
            Return
        End If
        TrialBalance_Mentahkan()
        Try
            Transaction_Transaksi.Commit()
            StatusSuntingDatabase = True
        Catch ex As Exception
            TransactionRollback_Transaksi()
            StatusSuntingDatabase = False
        End Try
    End Sub
    Sub TransactionRollback_Transaksi()
        Transaction_Transaksi.Rollback()
    End Sub


    Public Transaction_General As OdbcTransaction
    Sub TransactionBegin_General()
        Transaction_General = KoneksiDatabaseGeneral.BeginTransaction
    End Sub
    Sub TransactionCommit_General()
        Try
            Transaction_General.Commit()
            StatusSuntingDatabase = True
        Catch ex As Exception
            TransactionRollback_General()
            StatusSuntingDatabase = False
        End Try
    End Sub
    Sub TransactionRollback_General()
        Transaction_General.Rollback()
    End Sub


End Module
