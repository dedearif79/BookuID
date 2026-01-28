Imports System.Data.Odbc
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient
Imports bcomm

Public Class wpfWin_TryApp

    Dim cobainteger As Int64
    Dim cobastring

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Label4.Text = "Fnc : " & ClusterFinance
        Label5.Text = "Acc : " & ClusterAccounting

    End Sub


    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click

        Dim QueryUpdate As String = Kosongan

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice WHERE Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorInvoice As String
        Dim PPhTerutang As Int64
        Do While dr.Read
            NomorInvoice = dr.Item("Nomor_Invoice")
            PPhTerutang = dr.Item("PPh_Terutang")
            QueryUpdate &=
                " UPDATE tbl_Pembelian_Invoice SET PPh_Ditanggung = '" & PPhTerutang & "' " &
                " WHERE Nomor_Invoice = '" & NomorInvoice & "' ; " & vbCrLf
        Loop
        AksesDatabase_Transaksi(Tutup)

        PesanUntukProgrammer(QueryUpdate)

        BukaDatabaseTransaksi_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_110 : Transaksi")
        End Try
        TutupDatabaseTransaksi_MySQL()

    End Sub




    Private Sub Button2_Click(sender As Object, e As RoutedEventArgs) Handles Button2.Click
        Dim QueryPembuatanTabel As String
        Dim QueryAlterTable As String
        'Pembuatan Tabel : tbl_Toko
        QueryPembuatanTabel = " CREATE TABLE `tbl_Toko` (" &
                " `Kode_Toko` varchar(12) NOT NULL, " &
                " `Nama_Toko` varchar(255) NOT NULL, " &
                " `Alamat` longtext NOT NULL, " &
                " `Deskripsi` longtext NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        QueryAlterTable = " ALTER TABLE `tbl_Toko` ADD PRIMARY KEY (`Kode_Toko`), ADD UNIQUE KEY (`Kode_Toko`); "
        cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub Button3_Click(sender As Object, e As RoutedEventArgs) Handles Button3.Click
        'Simpan Perubahan Data Expire, ke File dbsatli.TXT :
        DataKoneksi = HeaderConfig &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            EnkripsiTeks("TERDAFTAR") & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            EnkripsiTeks(ID_CPU) &
            FooterConfig
        Try
            IO.File.WriteAllText(FilePathRegistrasiPerangkat, DataKoneksi)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_SimpanDataExpire_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanDataExpire.Click

        If dtp_AppExpire.SelectedDate Is Nothing Then
            Pesan_Peringatan("Pilih tanggal expire terlebih dahulu.")
            Return
        End If

        Dim TanggalExpire = dtp_AppExpire.SelectedDate.Value.ToString("dd/MM/yyyy")
        Dim TanggalExpire_Enk = EnkripsiTanggal(TanggalExpire)
        AppExpire = TanggalExpire

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_SC SET SC_99 = '" & TanggalExpire_Enk & "' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)

    End Sub

    Private Sub btn_IsiKolomNamaAkun_Click(sender As Object, e As RoutedEventArgs) Handles btn_IsiKolomNamaAkun.Click

        Dim ID
        Dim COA
        Dim NamaAkunTransaksi
        Dim NamaAkunCOA = Nothing
        Dim cmdAKUN As OdbcCommand
        Dim drAKUN As OdbcDataReader
        Dim cmdSIMPAN As OdbcCommand

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            ID = dr.Item("Nomor_ID")
            COA = dr.Item("COA")
            NamaAkunTransaksi = dr.Item("Nama_Akun")
            If NamaAkunTransaksi = Nothing Then
                AksesDatabase_General(Buka)
                cmdAKUN = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
                drAKUN = cmdAKUN.ExecuteReader
                drAKUN.Read()
                If drAKUN.HasRows Then
                    NamaAkunCOA = drAKUN.Item("Nama_Akun")
                End If
                AksesDatabase_General(Tutup)
                cmdSIMPAN = New OdbcCommand(" UPDATE tbl_Transaksi SET Nama_Akun = '" & NamaAkunCOA & "' WHERE Nomor_ID = '" & ID & "' ", KoneksiDatabaseTransaksi)
                cmdSIMPAN.ExecuteNonQuery()
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)

        Pesan_Informasi("Selesai.")

    End Sub


    Private Sub Button4_Click(sender As Object, e As RoutedEventArgs) Handles Button4.Click
        win_Loading = New wpfWin_Loading
        win_Loading.Show()
        Jeda(3333)
        win_Loading.Close()
    End Sub


    Private Sub Button5_Click(sender As Object, e As RoutedEventArgs) Handles Button5.Click

    End Sub

    Private Sub btn_EnkripsiAngka_Click(sender As Object, e As RoutedEventArgs) Handles btn_EnkripsiAngka.Click
        txt_AngkaTerEnkripsi.Text = EnkripsiAngka(txt_Angka.Text)
    End Sub

    Private Sub btn_DekripsiAngka_Click(sender As Object, e As RoutedEventArgs) Handles btn_DekripsiAngka.Click
        txt_AngkaTerDekripsi.Text = DekripsiAngka(txt_EnkripsiAngka.Text)
    End Sub

    Private Sub btn_EnkripsiTeks_Click(sender As Object, e As RoutedEventArgs) Handles btn_EnkripsiTeks.Click
        txt_TeksTerenkripsi.Text = EnkripsiTeks(txt_Teks.Text)
    End Sub

    Private Sub btn_DekripsiTeks_Click(sender As Object, e As RoutedEventArgs) Handles btn_DekripsiTeks.Click
        txt_TeksTerdekripsi.Text = DekripsiTeks(txt_EnkripsiTeks.Text)
    End Sub

    Private Sub btn_BakcupDatabase_Click(sender As Object, e As RoutedEventArgs) Handles btn_BakcupDatabase.Click
        'frm_BackupDatabase.ShowDialog()
    End Sub

    Private Sub txt_Nomor_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Nomor.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Nomor)
    End Sub

    Private Sub btn_Konversi_Click(sender As Object, e As RoutedEventArgs) Handles btn_Konversi.Click
        txt_Terbilang.Text = AngkaTerbilangRupiahDenganKurung(AmbilAngka(txt_Nomor.Text))
    End Sub

    Private Sub Button10_Click(sender As Object, e As RoutedEventArgs) Handles Button10.Click

        Dim QueryUpdate = " UPDATE tbl_coa SET Nama_Akun = 'Uang Muka Pembelian - Impor' WHERE tbl_coa.COA = '11702'; "
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryUpdate, KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper(ex.Message)
        End Try
        AksesDatabase_General(Tutup)

        PesanUntukProgrammer("Status Sunting Database : " & StatusSuntingDatabase)

    End Sub

    Private Sub Button11_Click(sender As Object, e As RoutedEventArgs) Handles Button11.Click
        CuciDebetKreditCOA()
        'Dim Tanggal As Date
        'Tanggal = New Date(2025, 1, 20)
        'JurnalAdjusment_Forex(KodeTautanCOA_HutangUsaha_USD, Tanggal)
    End Sub

    Private Sub Button12_Click(sender As Object, e As RoutedEventArgs) Handles Button12.Click

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_InfoAplikasi SET Versi_App = '" & EnkripsiTeksAES1("1") & "', Apdet_App = '" & EnkripsiTeksAES1("1") & "' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)

    End Sub

    Sub Coba(ByRef VarCoba As Integer)
        VarCoba = 20
    End Sub

    Private Sub btn_IsiTabelTautanCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_IsiTabelTautanCOA.Click
        PerbaruiTabelTautanCOA(SistemCOA_StandarAplikasi)
    End Sub



    Private Sub btn_ResetCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_ResetCOA.Click
        Dim COA = Kosongan
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            TahunBuku_Alternatif = dr.Item("Tahun_Buku")
            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            cmdTELUSUR = New OdbcCommand(" SELECT COA FROM tbl_COA ", KoneksiDatabaseGeneral)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                COA = drTELUSUR.Item("COA")
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_SaldoAwalCOA VALUES ( '" & COA & "', 0 ) ", KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
            Loop
            TutupDatabaseTransaksi_Alternatif()
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub BikinTabelBaru_Click(sender As Object, e As RoutedEventArgs) Handles btn_BikinTabelBaru.Click

        Dim QueryPembuatanTabel
        Dim QueryAlterTable
        Dim cmdPengisianTabel As OdbcCommand
        Dim NomorUrut

        'Pembuatan Tabel : tbl_PengawasanHutangBpjsKesehatan
        QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangBpjsKesehatan` ( " &
                " `Nomor_ID`            bigint(27)      NOT NULL, " &
                " `Nomor_Urut`          bigint(27)      NOT NULL, " &
                " `Bulan`               varchar(33)     NOT NULL, " &
                " `Tahun`               int(7)          NOT NULL, " &
                " `Jumlah_Tagihan`      bigint(27)      NOT NULL, " &
                " `Koreksi_Selisih`     bigint(27)      NOT NULL, " &
                " `Keterangan`          longtext        NOT NULL  " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangBpjsKesehatan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
        cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        NomorUrut = 0
        Dim Bulan = Nothing
        Do While NomorUrut < 12
            NomorUrut += 1
            Bulan = BulanTerbilang(NomorUrut)
            cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBpjsKesehatan VALUES ( " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & Bulan & "', " &
                                                    " '" & TahunBukuBaru & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & Nothing & "' " &
                                                    " )",
                                                    KoneksiDatabaseTransaksi)
            cmdPengisianTabel.ExecuteNonQuery()
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Pembuatan Tabel : tbl_PengawasanHutangBpjsKetenagakerjaan
        QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangBpjsKetenagakerjaan` ( " &
                " `Nomor_ID`            bigint(27)      NOT NULL, " &
                " `Nomor_Urut`          bigint(27)      NOT NULL, " &
                " `Bulan`               varchar(33)     NOT NULL, " &
                " `Tahun`               int(7)          NOT NULL, " &
                " `Jumlah_Tagihan`      bigint(27)      NOT NULL, " &
                " `Koreksi_Selisih`     bigint(27)      NOT NULL, " &
                " `Keterangan`          longtext        NOT NULL  " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangBpjsKetenagakerjaan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
        cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        NomorUrut = 0
        Do While NomorUrut < 12
            NomorUrut += 1
            Bulan = BulanTerbilang(NomorUrut)
            cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBpjsKetenagakerjaan VALUES ( " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & Bulan & "', " &
                                                    " '" & TahunBukuBaru & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & Nothing & "' " &
                                                    " )",
                                                    KoneksiDatabaseTransaksi)
            cmdPengisianTabel.ExecuteNonQuery()
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Pembuatan Tabel : tbl_PengawasanHutangKoperasiKaryawan
        QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangKoperasiKaryawan` ( " &
                " `Nomor_ID`            bigint(27)      NOT NULL, " &
                " `Nomor_Urut`          bigint(27)      NOT NULL, " &
                " `Bulan`               varchar(33)     NOT NULL, " &
                " `Tahun`               int(7)          NOT NULL, " &
                " `Jumlah_Tagihan`      bigint(27)      NOT NULL, " &
                " `Koreksi_Selisih`     bigint(27)      NOT NULL, " &
                " `Keterangan`          longtext        NOT NULL  " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangKoperasiKaryawan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
        cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        NomorUrut = 0
        Do While NomorUrut < 12
            NomorUrut += 1
            Bulan = BulanTerbilang(NomorUrut)
            cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangKoperasiKaryawan VALUES ( " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & Bulan & "', " &
                                                    " '" & TahunBukuBaru & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & Nothing & "' " &
                                                    " )",
                                                    KoneksiDatabaseTransaksi)
            cmdPengisianTabel.ExecuteNonQuery()
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Pembuatan Tabel : tbl_PengawasanHutangSerikat
        QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangSerikat` ( " &
                " `Nomor_ID`            bigint(27)      NOT NULL, " &
                " `Nomor_Urut`          bigint(27)      NOT NULL, " &
                " `Bulan`               varchar(33)     NOT NULL, " &
                " `Tahun`               int(7)          NOT NULL, " &
                " `Jumlah_Tagihan`      bigint(27)      NOT NULL, " &
                " `Koreksi_Selisih`     bigint(27)      NOT NULL, " &
                " `Keterangan`          longtext        NOT NULL  " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangSerikat` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
        cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        NomorUrut = 0
        Do While NomorUrut < 12
            NomorUrut += 1
            Bulan = BulanTerbilang(NomorUrut)
            cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangSerikat VALUES ( " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & NomorUrut & "', " &
                                                    " '" & Bulan & "', " &
                                                    " '" & TahunBukuBaru & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & 0 & "', " &
                                                    " '" & Nothing & "' " &
                                                    " )",
                                                    KoneksiDatabaseTransaksi)
            cmdPengisianTabel.ExecuteNonQuery()
        Loop
        AksesDatabase_Transaksi(Tutup)

    End Sub

    Private Sub btn_PulihkanBuku_Click(sender As Object, e As RoutedEventArgs) Handles btn_PulihkanBuku.Click

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return

        Dim KoneksiHapusDb As MySqlConnection
        Dim cmdHapusDb As MySqlCommand
        Dim strHapusDb As String
        KoneksiHapusDb = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";username=" & UserDatabase & ";password=" & PasswordDatabase & ";SSL Mode=None")
        cmdHapusDb = KoneksiHapusDb.CreateCommand
        strHapusDb = " DROP DATABASE " & NamaDatabaseTransaksi
        cmdHapusDb.CommandText = strHapusDb
        Try
            KoneksiHapusDb.Open()
            cmdHapusDb.ExecuteNonQuery()
            KoneksiHapusDb.Close()
        Catch ex As Exception
        End Try

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_InfoData " &
                              " WHERE Tahun_Buku = '" & TahunBukuAktif & "' ",
                              KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        cmd = New OdbcCommand(" UPDATE tbl_InfoData SET Status_Buku = '" & StatusBuku_OPEN & "' " &
                              " WHERE Tahun_Buku = '" & TahunBukuKemarin & "' ",
                              KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()


        cmd = New OdbcCommand(" UPDATE tbl_SC SET Tahun_Buku_Terakhir_Dibuka = '" & TahunBukuAktif - 1 & "' ",
                              KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        ' Trigger keluar aplikasi melalui WPF Window
        win_BOOKU.Close()

    End Sub




    Private Sub btnDownload_Click(sender As Object, e As RoutedEventArgs) Handles btnDownload.Click

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click

    End Sub

    Private Sub btn_UpdateDatabase_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateDatabase.Click
        ApdetBooku_SisiAplikasi = 188
        PesanUntukProgrammer("Tentukan Dulu Veri Updatenya...!!!" & Enter2Baris &
                             "Versi sekarang : " & ApdetBooku_SisiAplikasi)
        UpdateDatabase()
    End Sub

    Private Sub btn_TapilkanTeks_Click(sender As Object, e As RoutedEventArgs) Handles btn_TapilkanTeks.Click

        Dim path As String = "D:\VB .Net Project\BookuID\Booku\bin\Debug\net8.0-windows\Client\simulcur\Backup\MySQL\bookuid_booku_simulcur_genx.sql"
        Dim IsiTeks As String = Kosongan

        ' Pastikan file ada
        If File.Exists(path) Then
            IsiTeks = File.ReadAllText(path)
            txt_PenampilTeks.Text = IsiTeks
        Else
            Pesan_Peringatan("File tidak ditemukan: " & path)
        End If

        Dim KoneksiBackUp As MySqlConnection
        Dim cmdBackUp As MySqlCommand
        Dim strBackUp As String = IsiTeks
        KoneksiBackUp = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";" &
                                            "port=" & PortDatabase & ";" &
                                            "username=" & UserDatabase & ";" &
                                            "password=" & PasswordDatabase & ";" &
                                            "SSL Mode=None")
        cmdBackUp = KoneksiBackUp.CreateCommand
        cmdBackUp.CommandText = strBackUp

        Try
            KoneksiBackUp.Open()
            cmdBackUp.ExecuteNonQuery()
            KoneksiBackUp.Close()
            PesanPemberitahuan("Backup Berhasil")
        Catch ex As Exception
            PesanPeringatan("Bakcup Gagal...!")
        End Try

    End Sub

End Class
