Imports System.Data.Odbc
Imports bcomm
Imports MySql.Data.MySqlClient

Module mdl_UpdateDatabase

    Dim ProsesUpdateTabel

    Public Sub UpdateDatabase()

        PesanPemberitahuan("Sistem akan memperharui struktur database untuk kesesuaian dengan update terbaru aplikasi." & Enter2Baris &
                           "Silakan klik 'OK', dan mohon bersabar untuk menunggu prosesnya.")

        ProsesUpdateTabel = True

        Update_12()

        Update_27()

        Update_30()

        Update_31()

        Update_32()

        Update_36()

        Update_37()

        Update_38()

        Update_40()

        Update_41()

        Update_42()

        Update_44()

        Update_45()

        Update_46()

        Update_47()

        Update_51()

        Update_68()

        Update_75()

        Update_78()

        Update_82()

        Update_88()

        Update_90()

        Update_110()

        Update_129()

        Update_151()

        Update_152()

        Update_160()

        Update_164()

        Update_166()

        'Reset Debet/Kredit COA
        CuciDebetKreditCOA()

        'Reset Tautan COA :
        PerbaruiTabelTautanCOA(SistemCOA_StandarAplikasi)

        'Fisih :
        LaporanHasilUpdate()

    End Sub


    Sub AlterTable(Query)

        AksesDatabase_General(Buka)
        Try
            cmd = New OdbcCommand(Query, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)

    End Sub


    Sub IsiTabel(Query)
        AksesDatabase_General(Buka)
        Try
            cmd = New OdbcCommand(Query, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)
    End Sub


    Sub UpdateTable(Query)
        AksesDatabase_General(Buka)
        Try
            cmd = New OdbcCommand(Query, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)
    End Sub



    Sub CekStatusUpdate(NomorUpdate As Integer)
        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_" & NomorUpdate & " varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update Ke-" & NomorUpdate & " : General")
        End Try
        TutupDatabaseGeneral_MySQL()
    End Sub

    Sub UpdateDatabaseGeneral(QueryUpdate As String, NomorUpdate As Integer)
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_" & NomorUpdate & " : General")
        End Try
        TutupDatabaseGeneral_MySQL()
    End Sub

    Sub UpdateDatabaseTransaksi(QueryUpdate As String, NomorUpdate As Integer)
        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update Ke-" & NomorUpdate & " : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)
    End Sub



    Sub LaporanHasilUpdate()
        If ProsesUpdateTabel Then
            ApdetBooku_SisiDatabase = ApdetBooku_SisiAplikasi
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_InfoAplikasi SET Apdet_App = '" & EnkripsiAngkaAES1(ApdetBooku_SisiDatabase) & "' ", KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)
            BukaDatabasePublic()
            If StatusKoneksiDatabasePublic = True Then
                cmdPublic = New MySqlCommand(" UPDATE tbl_customer SET Apdet_App = '" & ApdetBooku_SisiDatabase & "' " &
                                             " WHERE ID_Customer = '" & ID_Customer & "' ", KoneksiDatabasePublic)
                cmdPublic_ExecuteNonQuery()
                TutupDatabasePublic()
            End If
            PesanSukses("Update Database berhasil.")
        Else
            PesanPeringatan("Update Database Gagal...!")
            End
        End If
    End Sub


    Sub CuciDebetKreditCOA()
        Dim QueryUpdate As String = Kosongan
        'COA Debet :
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'DEBET'   WHERE COA   LIKE  '1%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'DEBET'   WHERE COA   LIKE  '5%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'DEBET'   WHERE COA   LIKE  '6%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'DEBET'   WHERE COA   LIKE  '8%'; " & vbCrLf
        'COA Kredit :
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'KREDIT'   WHERE COA   LIKE  '2%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'KREDIT'   WHERE COA   LIKE  '3%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'KREDIT'   WHERE COA   LIKE  '4%'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     D_K = 'KREDIT'   WHERE COA   LIKE  '7%'; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Cuci Debet/Kredit COA: General")
        End Try
        TutupDatabaseGeneral_MySQL()
    End Sub


    Sub Update_12()
        AlterTable(" ALTER TABLE `tbl_company` CHANGE `Jenis` `Jenis_Usaha` VARCHAR(33) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL ")
        If Not StatusSuntingDatabase Then Return
        AksesDatabase_General(Buka)
        Try
            cmd = New OdbcCommand(" SELECT * FROM tbl_Company ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            JenisUsahaPerusahaan = dr.Item("Jenis_Usaha")
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)
        If StatusSuntingDatabase = True Then
            AlterTable(" ALTER TABLE `tbl_company` ADD `Jenis_WP` VARCHAR(33) NOT NULL AFTER `Jenis_Usaha` ")
        End If
        If StatusSuntingDatabase = True Then
            UpdateTable(" UPDATE `tbl_company` SET `Jenis_WP` = '" & JenisWP_BadanHukum & "' ")
            JenisWPPerusahaan = JenisWP_BadanHukum
        End If
    End Sub


    Sub Update_27()

        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!
        Dim QueryUpdate As String = Kosongan
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '11500', 'Piutang Usaha - Ekspor',       'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11610', 'Deposit Operasional Ekspor',   'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11704', 'Biaya Dibayar Dimuka - MUA',   'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '21100', 'Hutang Usaha - Impor',         'KREDIT', 'Tidak' ), "
        QueryUpdate &= " ( '62404', 'Biaya Asuransi Penjualan',     'DEBET',  'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!
        QueryUpdate &= " UPDATE tbl_coa SET Nama_Akun = 'Uang Muka Pembelian - Impor' WHERE tbl_coa.COA = '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa SET Nama_Akun = 'Biaya Dibayar Dimuka' WHERE tbl_coa.COA = '11703'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa SET Nama_Akun = 'Uang Muka Penjualan - Ekspor' WHERE tbl_coa.COA = '21501'; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_27 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        QueryUpdate = Kosongan
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa ( COA, Saldo_Awal ) VALUES "
        QueryUpdate &= " ( '11500', '0' ), "
        QueryUpdate &= " ( '11610', '0' ), "
        QueryUpdate &= " ( '11704', '0' ), "
        QueryUpdate &= " ( '21100', '0' ), "
        QueryUpdate &= " ( '62404', '0' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma..!
        QueryUpdate &= " ALTER TABLE tbl_penjualan_po ADD Kode_Mata_Uang VARCHAR(9) NOT NULL AFTER Nama_Customer; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_penjualan_po ADD Harga_Satuan_Asing DECIMAL(21,2) NOT NULL AFTER Harga_Satuan; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_penjualan_po SET Kode_Mata_Uang = 'IDR'; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_penjualan_invoice ADD Kode_Mata_Uang VARCHAR(9) NOT NULL AFTER Nama_Customer; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_penjualan_invoice ADD Kurs DECIMAL(21,2) NOT NULL AFTER Kode_Mata_Uang; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_penjualan_invoice ADD Biaya_Asuransi_Penjualan_Asing DECIMAL(21,2) NOT NULL AFTER Biaya_Transportasi; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_penjualan_invoice SET Kode_Mata_Uang = 'IDR'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_penjualan_invoice SET Kurs = 1; " & vbCrLf
        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_27 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_30()

        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = Kosongan
        QueryUpdate &= " ALTER TABLE tbl_lawantransaksi ADD UMKM INT(3) NOT NULL AFTER Nama_Mitra; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_company ADD Nomor_Suket_UMKM VARCHAR(63) NOT NULL AFTER Password_DJPO; " & vbCrLf
        QueryUpdate &= " ALTER TABLE tbl_company ADD Tanggal_Suket_UMKM DATETIME NOT NULL DEFAULT '1900-01-01' AFTER Nomor_Suket_UMKM; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_30 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return

        QueryUpdate = Kosongan

        'Pembelian - PO :
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD Kode_Mata_Uang                  VARCHAR(9)      NOT NULL    AFTER   Nama_Supplier; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD Harga_Satuan_Asing              DECIMAL(21,2)   NOT NULL    AFTER   Harga_Satuan; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD DPP_Jasa_Asing                  DECIMAL(21,2)   NOT NULL    AFTER   DPP_Jasa; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD Dasar_Pengenaan_Pajak_Asing     DECIMAL(21,2)   NOT NULL    AFTER   Dasar_Pengenaan_Pajak; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD PPN_Asing                       DECIMAL(21,2)   NOT NULL    AFTER   PPN; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        ADD PPh_Terutang_Asing              DECIMAL(21,2)   NOT NULL    AFTER   PPh_Terutang; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_pembelian_po        SET Kode_Mata_Uang                  = 'IDR'; " & vbCrLf

        'Pembelian - Invoice :
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Kode_Mata_Uang              VARCHAR(9)      NOT NULL    AFTER   Nama_Supplier; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Kurs                        DECIMAL(21,2)   NOT NULL    AFTER   Kode_Mata_Uang; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Nomor_SKB                   VARCHAR(99)     NOT NULL    AFTER   Pilihan_PPN; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Tanggal_SKB                 DATETIME        NOT NULL    DEFAULT '1900-01-01' AFTER Nomor_SKB; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Biaya_Asuransi_Pembelian    DECIMAL(21,2)   NOT NULL    AFTER   Biaya_Transportasi; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Insurance                   DECIMAL(21,2)   NOT NULL    AFTER   Termin_Persen; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Freight                     DECIMAL(21,2)   NOT NULL    AFTER   Insurance; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Bea_Masuk                   DECIMAL(21,2)   NOT NULL    AFTER   Freight; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Harga_Satuan                Harga_Satuan                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Total_Harga_Per_Item        Total_Harga_Per_Item        Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Jumlah_Harga_Keseluruhan    Jumlah_Harga_Keseluruhan    Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Diskon                      Diskon                      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  DPP_Barang                  DPP_Barang                  Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  DPP_Jasa                    DPP_Jasa                    Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Dasar_Pengenaan_Pajak       Dasar_Pengenaan_Pajak       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPN                         PPN                         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Total_Tagihan_Kotor         Total_Tagihan_Kotor         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Terutang                PPh_Terutang                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Ditanggung              PPh_Ditanggung              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Dipotong                PPh_Dipotong                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Total_Tagihan               Total_Tagihan               Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Jumlah_Hutang_Usaha         Jumlah_Hutang_Usaha         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Biaya_Administrasi_Bank     Biaya_Administrasi_Bank     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Biaya_Transportasi          Biaya_Transportasi          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Biaya_Materai               Biaya_Materai               Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Retur_DPP                   Retur_DPP                   Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Retur_PPN                   Retur_PPN                   Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_pembelian_invoice   SET     Kode_Mata_Uang              = 'IDR'; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_pembelian_invoice   SET     Kurs                        = 1; " & vbCrLf

        'Penjualan - Invoice :
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Biaya_Asuransi_Penjualan_Asing Biaya_Asuransi_Penjualan Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Harga_Satuan                Harga_Satuan                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Total_Harga_Per_Item        Total_Harga_Per_Item        Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Jumlah_Harga_Keseluruhan    Jumlah_Harga_Keseluruhan    Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Diskon                      Diskon                      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  DPP_Barang                  DPP_Barang                  Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  DPP_Jasa                    DPP_Jasa                    Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Dasar_Pengenaan_Pajak       Dasar_Pengenaan_Pajak       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPN                         PPN                         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Total_Tagihan_Kotor         Total_Tagihan_Kotor         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Terutang                PPh_Terutang                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Ditanggung              PPh_Ditanggung              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Dipotong                PPh_Dipotong                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Total_Tagihan               Total_Tagihan               Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Jumlah_Piutang_Usaha        Jumlah_Piutang_Usaha        Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Biaya_Transportasi          Biaya_Transportasi          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Retur_DPP                   Retur_DPP                   Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Retur_PPN                   Retur_PPN                   Decimal(21,2)   Not NULL; " & vbCrLf

        'Jurnal :
        QueryUpdate &= " ALTER TABLE    tbl_transaksi           ADD     Kurs                        DECIMAL(21,2)   NOT NULL    AFTER   Nama_Lawan_Transaksi; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_transaksi           CHANGE  Jumlah_Debet                Jumlah_Debet                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_transaksi           CHANGE  Jumlah_Kredit               Jumlah_Kredit               Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_transaksi           SET     Kurs                        = 1; " & vbCrLf

        'Bukti Pengeluaran :
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    ADD     Kurs                        DECIMAL(21,2)   NOT NULL    AFTER   Uraian_Invoice; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Jumlah_Tagihan              Jumlah_Tagihan              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Pokok                       Pokok                       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Bagi_Hasil                  Bagi_Hasil                  Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Jumlah_Pengajuan            Jumlah_Pengajuan            Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Jumlah_Bayar                Jumlah_Bayar                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Denda                       Denda                       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  Biaya_Administrasi_Bank     Biaya_Administrasi_Bank     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  PPh_Terutang                PPh_Terutang                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  PPh_Ditanggung              PPh_Ditanggung              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipengeluaran    CHANGE  PPh_Dipotong                PPh_Dipotong                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_buktipengeluaran    SET     Kurs                        = 1; " & vbCrLf

        'Bukti Penerimaan :
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     ADD     Kurs                        DECIMAL(21,2)   NOT NULL    AFTER   Uraian_Invoice; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Jumlah_Tagihan              Jumlah_Tagihan              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Pokok                       Pokok                       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Bagi_Hasil                  Bagi_Hasil                  Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Jumlah_Bayar                Jumlah_Bayar                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Denda                       Denda                       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  Biaya_Administrasi_Bank     Biaya_Administrasi_Bank     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  PPh_Terutang                PPh_Terutang                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  PPh_Ditanggung              PPh_Ditanggung              Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_buktipenerimaan     CHANGE  PPh_Dipotong                PPh_Dipotong                Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_buktipenerimaan     SET     Kurs                        = 1; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update 30 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_31()

        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = Kosongan
        QueryUpdate &= " CREATE TABLE  `tbl_Dummy` ("
        QueryUpdate &= " `Upd_99999`    varchar(9)     NOT NULL, "
        QueryUpdate &= " `Upd_31`       varchar(9)     NOT NULL, "
        QueryUpdate &= " `Upd_0`        varchar(9)     NOT NULL  "
        QueryUpdate &= ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_31 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return

        QueryUpdate = Kosongan

        'Pembelian - PO :
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        DROP    DPP_Jasa_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        DROP    Dasar_Pengenaan_Pajak_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        DROP    PPN_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_po        DROP    PPh_Terutang_Asing; " & vbCrLf

        'Pembelian - Invoice :
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   DROP    Biaya_Asuransi_Pembelian; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Bea_Masuk                   Bea_Masuk                   Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Dasar_Pengenaan_Pajak       Dasar_Pengenaan_Pajak       Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  DPP_Barang                  DPP_Barang                  Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  DPP_Jasa                    DPP_Jasa                    Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPN                         PPN                         Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Terutang                PPh_Terutang                Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Ditanggung              PPh_Ditanggung              Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  PPh_Dipotong                PPh_Dipotong                Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Biaya_Transportasi          Biaya_Transportasi          Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Biaya_Materai               Biaya_Materai               Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Retur_DPP                   Retur_DPP                   Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   CHANGE  Retur_PPN                   Retur_PPN                   Bigint(27)      Not NULL; " & vbCrLf

        'Penjualan - Invoice :
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Biaya_Asuransi_Penjualan    Insurance                   Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Biaya_Transportasi          Biaya_Transportasi          Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   ADD     Freight                     DECIMAL(21,2)   NOT NULL    AFTER           Insurance; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  DPP_Barang                  DPP_Barang                  Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  DPP_Jasa                    DPP_Jasa                    Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Dasar_Pengenaan_Pajak       Dasar_Pengenaan_Pajak       Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPN                         PPN                         Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Terutang                PPh_Terutang                Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Ditanggung              PPh_Ditanggung              Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  PPh_Dipotong                PPh_Dipotong                Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Biaya_Transportasi          Biaya_Transportasi          Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Retur_DPP                   Retur_DPP                   Bigint(27)      Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice   CHANGE  Retur_PPN                   Retur_PPN                   Bigint(27)      Not NULL; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_31 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_32()

        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_32 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_32 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return

        QueryUpdate = Kosongan

        'Penjualan - Invoice :
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice DROP    Harga_Satuan_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice DROP    Jumlah_Harga_Keseluruhan_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice DROP    Total_Tagihan_Asing; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_invoice DROP    Biaya_Transportasi_Asing; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_32 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_36()

        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_36 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_36 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return

        QueryUpdate = Kosongan

        QueryUpdate &= " ALTER TABLE    tbl_COA ADD     Kode_Mata_Uang      VARCHAR(9)          NOT NULL        AFTER   Nama_Akun; " & vbCrLf

        'Tabel COA :
        QueryUpdate &= " UPDATE     tbl_COA         SET     Nama_Akun =  'Hutang Usaha - Impor (USD)'   WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_COA         SET     Nama_Akun =  'Piutang Usaha - Ekspor (USD)' WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '21111'       WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '11511'       WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '11600'       WHERE COA =  '11510'; " & vbCrLf 'Gaji Dibayar Dimuka (Lama)
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Tautan COA :
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '21111'       WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '11511'       WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Update Kode Mata Uang :
        QueryUpdate &= " UPDATE     tbl_COA         SET     Kode_Mata_Uang  = 'IDR'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA         SET     Kode_Mata_Uang  = 'USD' WHERE COA =  '21111'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA         SET     Kode_Mata_Uang  = 'USD' WHERE COA =  '11511'; " & vbCrLf

        'COA Baru :
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '21112', 'Hutang Usaha - Impor - AUD',       'AUD',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '21113', 'Hutang Usaha - Impor - JPY',       'JPY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '21114', 'Hutang Usaha - Impor - CNY',       'CNY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '21115', 'Hutang Usaha - Impor - EUR',       'EUR',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11512', 'Piutang Usaha - Ekspor - AUD',     'AUD',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11513', 'Piutang Usaha - Ekspor - JPY',     'JPY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11514', 'Piutang Usaha - Ekspor - CNY',     'CNY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11515', 'Piutang Usaha - Ekspor - EUR',     'EUR',  'KREDIT',   'Tidak' ); " '<-- Ujung, jangan pakai kome (,)

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_36 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        QueryUpdate = Kosongan

        'Tabel Transaksi :
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     Nama_Akun =  'Hutang Usaha - Impor (USD)'   WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     Nama_Akun =  'Piutang Usaha - Ekspor (USD)' WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21111'       WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11511'       WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11600'       WHERE COA =  '11510'; " & vbCrLf 'Gaji Dibayar Dimuka (Lama)
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'PiUtang Usaha - Afiliasi

        'Tabel Saldo Awal COA :
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '21111'       WHERE COA =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '11511'       WHERE COA =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Bukti Pengeluaran :
        QueryUpdate &= " UPDATE tbl_BuktiPengeluaran    SET     COA_Debet  = '21111'       WHERE COA_Debet  =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE tbl_BuktiPengeluaran    SET     COA_Debet  = '21120'       WHERE COA_Debet  =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi

        'Tabel Bukti Penerimaan:
        QueryUpdate &= " UPDATE tbl_BuktiPenerimaan     SET     COA_Kredit = '11511'       WHERE COA_Kredit =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE tbl_BuktiPenerimaan     SET     COA_Kredit = '11520'       WHERE COA_Kredit =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Invoice Pembelian :
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice   SET     COA_Kredit = '21111'       WHERE COA_Kredit =  '21100'; " & vbCrLf 'Hutang Usaha - Impor (Lama)
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice   SET     COA_Kredit = '21120'       WHERE COA_Kredit =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi

        'Tabel Invoice Penjualan :
        QueryUpdate &= " UPDATE tbl_Penjualan_Invoice   SET     COA_Debet  = '11511'       WHERE COA_Debet  =  '11500'; " & vbCrLf 'Piutang Usaha - Ekspor (Lama)
        QueryUpdate &= " UPDATE tbl_Penjualan_Invoice   SET     COA_Debet  = '11520'       WHERE COA_Debet  =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi


        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_36 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_37()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_37 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return

        QueryUpdate = Kosongan


        '---------------- PENGULANGAN - UNTUK JAGA-JAGA -----------------------------------------------------------------------------------------------------
        'Tabel COA :
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_COA         SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Tautan COA :
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_TautanCOA   SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'COA Baru :
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '21116', 'Hutang Usaha - Impor - EUR',       'SGD',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11516', 'Piutang Usaha - Ekspor - EUR',     'SGD',  'DEBET',    'Tidak' ); " '<-- Ujung, jangan pakai kome (,)
        '---------------- PENGULANGAN - UNTUK JAGA-JAGA -----------------------------------------------------------------------------------------------------


        'Perubahan Property Kolom :
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Awal          Saldo_Awal          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Januari       Debet_Januari       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Januari      Kredit_Januari      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Januari       Saldo_Januari       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Februari      Debet_Februari      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Februari     Kredit_Februari     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Februari      Saldo_Februari      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Maret         Debet_Maret         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Maret        Kredit_Maret        Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Maret         Saldo_Maret         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_April         Debet_April         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_April        Kredit_April        Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_April         Saldo_April         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Mei           Debet_Mei           Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Mei          Kredit_Mei          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Mei           Saldo_Mei           Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Juni          Debet_Juni          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Juni         Kredit_Juni         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Juni          Saldo_Juni          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Juli          Debet_Juli          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Juli         Kredit_Juli         Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Juli          Saldo_Juli          Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Agustus       Debet_Agustus       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Agustus      Kredit_Agustus      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Agustus       Saldo_Agustus       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_September     Debet_September     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_September    Kredit_September    Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_September     Saldo_September     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Oktober       Debet_Oktober       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Oktober      Kredit_Oktober      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Oktober       Saldo_Oktober       Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Nopember      Debet_Nopember      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Nopember     Kredit_Nopember     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Nopember      Saldo_Nopember      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Debet_Desember      Debet_Desember      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Kredit_Desember     Kredit_Desember     Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_COA CHANGE  Saldo_Desember      Saldo_Desember      Decimal(21,2)   Not NULL; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : General")
        End Try
        TutupDatabaseGeneral_MySQL()


        QueryUpdate = Kosongan


        '---------------- PENGULANGAN - UNTUK JAGA-JAGA -----------------------------------------------------------------------------------------------------
        'Tabel Transaksi :
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'PiUtang Usaha - Afiliasi

        'Tabel Saldo Awal COA :
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '21120'       WHERE COA =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi
        QueryUpdate &= " UPDATE     tbl_SaldoAwalCOA    SET     COA = '11520'       WHERE COA =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Bukti Pengeluaran :
        QueryUpdate &= " UPDATE tbl_BuktiPengeluaran    SET     COA_Debet  = '21120'       WHERE COA_Debet  =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi

        'Tabel Bukti Penerimaan:
        QueryUpdate &= " UPDATE tbl_BuktiPenerimaan     SET     COA_Kredit = '11520'       WHERE COA_Kredit =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi

        'Tabel Invoice Pembelian :
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice   SET     COA_Kredit = '21120'       WHERE COA_Kredit =  '21102'; " & vbCrLf 'Hutang Usaha - Afiliasi

        'Tabel Invoice Penjualan :
        QueryUpdate &= " UPDATE tbl_Penjualan_Invoice   SET     COA_Debet  = '11520'       WHERE COA_Debet  =  '11502'; " & vbCrLf 'Piutang Usaha - Afiliasi
        '---------------- PENGULANGAN - UNTUK JAGA-JAGA -----------------------------------------------------------------------------------------------------


        'Update Tabel : tbl_Transaksi :
        QueryUpdate &= " ALTER TABLE    tbl_Transaksi       ADD Kode_Mata_Uang VARCHAR(9) NOT NULL AFTER Nama_Lawan_Transaksi; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Transaksi       SET Kode_Mata_Uang = 'IDR' WHERE Kurs =  1; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Transaksi       SET Kode_Mata_Uang = 'USD' WHERE Kurs >  1; " & vbCrLf

        'Update Tabel : tbl_Penerimaan :
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPenerimaan     ADD Kode_Mata_Uang VARCHAR(9) NOT NULL AFTER Uraian_Invoice; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_BuktiPenerimaan     SET Kode_Mata_Uang = 'IDR' WHERE Kurs =  1; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_BuktiPenerimaan     SET Kode_Mata_Uang = 'USD' WHERE Kurs >  1; " & vbCrLf

        'Update Tabel : tbl_Pengeluaran :
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPengeluaran    ADD Kode_Mata_Uang VARCHAR(9) NOT NULL AFTER Uraian_Invoice; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_BuktiPengeluaran    SET Kode_Mata_Uang = 'IDR' WHERE Kurs =  1; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_BuktiPengeluaran    SET Kode_Mata_Uang = 'USD' WHERE Kurs >  1; " & vbCrLf

        'Pembuatan Tabel : tbl_kursakhirbulan
        QueryUpdate &= " ALTER TABLE    tbl_SaldoAwalCOA    CHANGE  Saldo_Awal     Saldo_Awal      Decimal(21,2)   Not NULL; " & vbCrLf
        QueryUpdate &=
            "  CREATE TABLE `tbl_kursakhirbulan` (" &
            " `Kode_Mata_Uang`      varchar(9) NOT NULL, " &
            " `Akhir_Tahun_Lalu`    decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Januari`             decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Februari`            decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Maret`               decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `April`               decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Mei`                 decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Juni`                decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Juli`                decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Agustus`             decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `September`           decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Oktober`             decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Nopember`            decimal(21,2) NOT NULL DEFAULT '0.00', " &
            " `Desember`            decimal(21,2) NOT NULL DEFAULT '0.00'  " &
            " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        QueryUpdate &= " ALTER TABLE `tbl_kursakhirbulan` ADD PRIMARY KEY (`Kode_Mata_Uang`), ADD UNIQUE KEY (`Kode_Mata_Uang`); "
        QueryUpdate &= " INSERT INTO tbl_kursakhirbulan ( Kode_Mata_Uang ) VALUES  " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_USD & "' ), " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_AUD & "' ), " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_JPY & "' ), " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_CNY & "' ), " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_EUR & "' ), " & vbCrLf
        QueryUpdate &= " ( '" & KodeMataUang_SGD & "' ); " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_38()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_38 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_38 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        If Not StatusSuntingDatabase Then Return


        QueryUpdate = Kosongan

        'Tabel Saham :
        QueryUpdate &= " CREATE TABLE `tbl_DaftarSaham` ( "
        QueryUpdate &= " `Saham_Ke` int(3) NOT NULL, "
        QueryUpdate &= " `Harga` bigint(27) NOT NULL  "
        QueryUpdate &= " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
        QueryUpdate &= " ALTER TABLE `tbl_DaftarSaham` ADD UNIQUE(`Harga`); "
        QueryUpdate &= " ALTER TABLE `tbl_DaftarSaham` ADD UNIQUE(`Saham_Ke`); "
        QueryUpdate &= " ALTER TABLE `tbl_DaftarSaham` ADD PRIMARY KEY(`Saham_Ke`); "
        QueryUpdate &= " INSERT INTO tbl_DaftarSaham VALUES ( " & 1 & ", " & 10000000 & " ); "

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        QueryUpdate = Kosongan

        'Update Tabel : tbl_Pemindahbukuan :
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  MODIFY  Tanggal_Transaksi                   DATETIME        NOT NULL AFTER Tanggal_BPPB; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  CHANGE  Jumlah_Transaksi    Jumlah_Kredit   DECIMAL(21,2)   NOT NULL AFTER COA_Kredit; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kurs_Bank_Kredit                    DECIMAL(21,2)   NOT NULL AFTER COA_Kredit; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kurs_BI_Kredit                      DECIMAL(21,2)   NOT NULL AFTER COA_Kredit; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kode_Mata_Uang_Kredit               VARCHAR(9)      NOT NULL AFTER COA_Kredit; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Jumlah_Debet                        DECIMAL(21,2)   NOT NULL AFTER COA_Debet ; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kurs_Bank_Debet                     DECIMAL(21,2)   NOT NULL AFTER COA_Debet ; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kurs_BI_Debet                       DECIMAL(21,2)   NOT NULL AFTER COA_Debet ; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  ADD     Kode_Mata_Uang_Debet                VARCHAR(9)      NOT NULL AFTER COA_Debet ; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kurs_BI_Kredit      = 1; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kurs_Bank_Kredit    = 1; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kurs_BI_Debet       = 1; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kurs_Bank_Debet     = 1; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kode_Mata_Uang_Kredit = 'IDR'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pemindahbukuan  SET Kode_Mata_Uang_Debet  = 'IDR'; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_38 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_40()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_40 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_40 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        QueryUpdate = Kosongan

        'COA Baru :
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '11211', 'Kas (USD)',    'USD',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11212', 'Kas (AUD)',    'AUD',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11213', 'Kas (JPY)',    'JPY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11214', 'Kas (CNY)',    'CNY',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11215', 'Kas (EUR)',    'EUR',  'KREDIT',   'Tidak' ), "
        QueryUpdate &= " ( '11216', 'Kas (SGD)',    'SGD',  'KREDIT',   'Tidak' ); " '<-- Ujung, jangan pakai kome (,)

        'Update Nama Akun :
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (USD)'   WHERE COA =  '21111'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (AUD)'   WHERE COA =  '21112'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (JPY)'   WHERE COA =  '21113'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (CNY)'   WHERE COA =  '21114'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (EUR)'   WHERE COA =  '21115'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Hutang Usaha - Impor (SGD)'   WHERE COA =  '21116'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (USD)' WHERE COA =  '11511'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (AUD)' WHERE COA =  '11512'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (JPY)' WHERE COA =  '11513'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (CNY)' WHERE COA =  '11514'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (EUR)' WHERE COA =  '11515'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Piutang Usaha - Ekspor (SGD)' WHERE COA =  '11516'; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        QueryUpdate = Kosongan

        'Update Invoice Pembelian :
        QueryUpdate &= " ALTER TABLE `tbl_pembelian_invoice` ADD `Kurs_KMK` DECIMAL(21,2) NOT NULL AFTER `Bea_Masuk`; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_40 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_41()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_41 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_41 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        QueryUpdate = Kosongan

        'Update Tabel Pemindahbukuan :
        QueryUpdate &= " ALTER TABLE    tbl_Pemindahbukuan  MODIFY  User    VARCHAR(63)     NOT NULL    AFTER   Nomor_JV; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Pemindahbukuan  SET     User    = '" & UserAktif & "' ; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_41 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_42()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_42 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_42 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        QueryUpdate = Kosongan

        'Update Nama Akun :
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Persediaan Bahan Baku - Impor'                WHERE COA =  '11805'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Pajak Dibayar Dimuka - PPh Pasal 22 - Impor'  WHERE COA =  '11903'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'PPN Masukan - Impor'                          WHERE COA =  '11908'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Retur Pembelian Bahan Baku - Impor'           WHERE COA =  '51201'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Asuransi Impor'                         WHERE COA =  '51202'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Bea Masuk Impor'                              WHERE COA =  '51204'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Transportasi Impor'                     WHERE COA =  '51205'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Jasa Pergudangan Impor'                 WHERE COA =  '51206'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Pengurusan Dokumen Impor'               WHERE COA =  '51207'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya BB Impor-8'                             WHERE COA =  '51208'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Pembelian Bahan Baku Lainnya - Impor'   WHERE COA =  '51209'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Biaya Pemakaian Bahan Baku - Impor'           WHERE COA =  '51299'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Pembelian Bahan Baku - Impor'                 WHERE COA =  '58812'; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_37 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        QueryUpdate = Kosongan

        'Update Tabel Pemindahbukuan :
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   MODIFY  Bea_Masuk           BIGINT(27)     NOT NULL    AFTER   Freight; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   MODIFY  Biaya_Transportasi  BIGINT(27)     NOT NULL    AFTER   Ditanggung_Oleh; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_42 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_44()
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_43 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf 'Ini Kesalahan. Harusnya 44 malah 43 Tapi jangan dirubah....!!!! Sudah kelewat...!!! Kalau dirubah, bahaya...!!!
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_44 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        QueryUpdate = Kosongan

        'Update Tabel Pemindahbukuan :
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   MODIFY  Retur_PPN           BIGINT(27)     NOT NULL    AFTER   Biaya_Materai; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   MODIFY  Retur_DPP           BIGINT(27)     NOT NULL    AFTER   Biaya_Materai; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_44 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
        'SUB INI JANGAN DIJADIKAN TEMPLATE....!!! ADA KESALAHAN FATAL...!!!!
    End Sub



    Sub Update_45()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_45 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_45 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan

        'Update Tabel Dummy (Karena kesalahan pada update 44) :
        QueryUpdate &= " ALTER TABLE tbl_dummy ADD Upd_44 varchar(9) NOT NULL AFTER Upd_45; " & vbCrLf

        'Update Nama Akun :
        QueryUpdate &= " UPDATE     tbl_COA     SET     Nama_Akun =  'Pembelian Bahan Baku - Impor'                 WHERE COA =  '58812'; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_45 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        'Database Transaksi :
        QueryUpdate = Kosongan

        'Tabel Transaksi
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     Nama_Akun =  'Pembelian Bahan Baku - Impor'     WHERE COA =  '58812'; " & vbCrLf

        'Update Tabel Invoice Pembelian :
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Loko                        VARCHAR(27)     NOT NULL                            AFTER   Tanggal_Diterima_SJ_BAST_Produk; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Tanggal_Serah_Terima        DATETIME        NOT NULL    DEFAULT '1900-01-01'    AFTER   Tanggal_Diterima_SJ_BAST_Produk; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Nomor_JV_Bayar_Pajak_Impor  BIGINT(27)      NOT NULL                            AFTER   Nomor_JV_Bukti_Potong; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_pembelian_invoice   ADD     Tanggal_Bayar_Pajak_Impor   DATETIME        NOT NULL    DEFAULT '1900-01-01'    AFTER   Nomor_JV_Bukti_Potong; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   MODIFY  Biaya_Transportasi          BIGINT(27)      NOT NULL                            AFTER   Ditanggung_Oleh; " & vbCrLf

        'Update kolom-kolom Jenis Pajak/PPh :
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPenerimaan             CHANGE  Jenis_Pajak     Jenis_Pajak     VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPengeluaran            CHANGE  Jenis_Pajak     Jenis_Pajak     VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_PO                CHANGE  Jenis_PPN       Jenis_PPN       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_PO                CHANGE  Jenis_PPh       Jenis_PPh       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Retur             CHANGE  Jenis_PPN       Jenis_PPN       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_PO                CHANGE  Jenis_PPN       Jenis_PPN       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_PO                CHANGE  Jenis_PPh       Jenis_PPh       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Retur             CHANGE  Jenis_PPN       Jenis_PPN       VARCHAR(27) Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_PengawasanPelaporanPajak    CHANGE  Jenis_Pajak     Jenis_Pajak     VARCHAR(27) Not NULL; " & vbCrLf


        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_45 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_46()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_46 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_46 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan
        QueryUpdate &= " UPDATE tbl_coa     SET     Nama_Akun       = 'Biaya Dibayar Dimuka (USD)'  WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     Kode_Mata_Uang  = 'USD'                         WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     COA             = '11711'                       WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '11712', 'Biaya Dibayar Dimuka (AUD)',   'AUD',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11713', 'Biaya Dibayar Dimuka (JPY)',   'JPY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11714', 'Biaya Dibayar Dimuka (CNY)',   'CNY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11715', 'Biaya Dibayar Dimuka (EUR)',   'EUR',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11716', 'Biaya Dibayar Dimuka (SGD)',   'SGD',  'DEBET',  'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_46 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        'Database Transaksi :
        QueryUpdate = Kosongan

        'tbl_Transaksi :
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     Nama_Akun       = 'Biaya Dibayar Dimuka (USD)'  WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     Kode_Mata_Uang  = 'USD'                         WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA             = '11711'                       WHERE COA =  '11704'; " & vbCrLf

        'Tabel Bukti Pengeluaran :
        QueryUpdate &= " UPDATE tbl_BuktiPengeluaran    SET     COA_Debet  = '11711'    WHERE COA_Debet  =  '11704'; " & vbCrLf

        'Tabel Bukti Penerimaan:
        QueryUpdate &= " UPDATE tbl_BuktiPenerimaan     SET     COA_Kredit = '11711'    WHERE COA_Kredit =  '11704'; " & vbCrLf

        'tbl_SaldoAwalCOA :
        QueryUpdate &= " UPDATE tbl_saldoawalcoa        SET     COA = '11711'           WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11712', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11713', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11714', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11715', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11716', '0' ); "
        'Nalangin Ketertinggalan pada Upadate 40 :
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11211', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11212', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11213', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11214', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11215', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11216', '0' ); "
        'Nalangin Ketertinggalan pada Upadate 36 :
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21112', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21113', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21114', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21115', '0' ); "
        'Nalangin Ketertinggalan pada Upadate 37 :
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11512', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11513', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11514', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11515', '0' ); "

        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11516', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21116', '0' ); "

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_46 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_47()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_47 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_47 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan

        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'SGD'                     WHERE COA =  '11516'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'SGD'                     WHERE COA =  '21116'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Uang Muka Pembelian - Impor (USD)'   WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'USD'                                 WHERE COA =  '11702'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Uang Muka Penjualan - Ekspor (USD)'  WHERE COA =  '21502'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'USD'                                 WHERE COA =  '21502'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '11700'                   WHERE COA =  '11701'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '11701'                   WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '11710'                   WHERE COA =  '11703'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '21500'                   WHERE COA =  '21501'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '21501'                   WHERE COA =  '21502'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     COA             = '21510'                   WHERE COA =  '21503'; " & vbCrLf

        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '11702', 'Uang Muka Pembelian - Impor (AUD)',   'AUD',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11703', 'Uang Muka Pembelian - Impor (JPY)',   'JPY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11704', 'Uang Muka Pembelian - Impor (CNY)',   'CNY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11705', 'Uang Muka Pembelian - Impor (EUR)',   'EUR',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '11706', 'Uang Muka Pembelian - Impor (SGD)',   'SGD',  'DEBET',  'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!

        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '21502', 'Uang Muka Penjualan - Ekspor (AUD)',  'AUD',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '21503', 'Uang Muka Penjualan - Ekspor (JPY)',  'JPY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '21504', 'Uang Muka Penjualan - Ekspor (CNY)',  'CNY',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '21505', 'Uang Muka Penjualan - Ekspor (EUR)',  'EUR',  'DEBET',  'Tidak' ), "
        QueryUpdate &= " ( '21506', 'Uang Muka Penjualan - Ekspor (SGD)',  'SGD',  'DEBET',  'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_47 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        'Database Transaksi :
        QueryUpdate = Kosongan

        'tbl_Transaksi :
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11700'   WHERE COA =  '11701'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11701'   WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '11710'   WHERE COA =  '11703'; " & vbCrLf

        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21500'   WHERE COA =  '21501'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21501'   WHERE COA =  '21502'; " & vbCrLf
        QueryUpdate &= " UPDATE     tbl_Transaksi       SET     COA = '21510'   WHERE COA =  '21503'; " & vbCrLf

        'tbl_SaldoAwalCOA :
        QueryUpdate &= " UPDATE tbl_saldoawalcoa        SET     COA = '11700'           WHERE COA =  '11701'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_SaldoAwalCOA        SET     COA = '11701'           WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_saldoawalcoa        SET     COA = '11710'           WHERE COA =  '11703'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_saldoawalcoa        SET     COA = '21500'           WHERE COA =  '21501'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_SaldoAwalCOA        SET     COA = '21501'           WHERE COA =  '21502'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_saldoawalcoa        SET     COA = '21510'           WHERE COA =  '21503'; " & vbCrLf

        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11702', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11703', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11704', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11705', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '11706', '0' ); "

        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21502', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21503', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21504', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21505', '0' ); "
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21506', '0' ); "

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_47 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_51()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_51 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_51 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan

        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (USD)'                  WHERE COA =  '21111'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (AUD)'                  WHERE COA =  '21112'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (JPY)'                  WHERE COA =  '21113'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (CNY)'                  WHERE COA =  '21114'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (EUR)'                  WHERE COA =  '21115'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Hutang Usaha (SGD)'                  WHERE COA =  '21116'; " & vbCrLf

        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (USD)'                 WHERE COA =  '11511'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (AUD)'                 WHERE COA =  '11512'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (JPY)'                 WHERE COA =  '11513'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (CNY)'                 WHERE COA =  '11514'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (EUR)'                 WHERE COA =  '11515'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Piutang Usaha (SGD)'                 WHERE COA =  '11516'; " & vbCrLf

        'Koreksi Kesalahan Update 46 :
        QueryUpdate &= " UPDATE tbl_coa     SET     Nama_Akun       = 'Biaya Dibayar Dimuka (CNY)'          WHERE COA =  '11704'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_coa     SET     Kode_Mata_Uang  = 'CNY'                                 WHERE COA =  '11704'; " & vbCrLf

        'Koreksi Kesalahan Update 47 :
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Uang Muka Pembelian - Impor (AUD)'   WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'AUD'                                 WHERE COA =  '11702'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Nama_Akun       = 'Uang Muka Penjualan - Ekspor (AUD)'  WHERE COA =  '21502'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_COA     SET     Kode_Mata_Uang  = 'AUD'                                 WHERE COA =  '21502'; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_51 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

    End Sub


    Sub Update_68()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_68 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_68 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database Transaksi :
        QueryUpdate = Kosongan

        QueryUpdate &= " ALTER TABLE    tbl_PengawasanGaji  ADD BPJS_Kesehatan_Produksi_Dibayar_Karyawan        BIGINT(27)  NOT NULL    AFTER   BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_PengawasanGaji  ADD BPJS_Kesehatan_Administrasi_Dibayar_Karyawan    BIGINT(27)  NOT NULL    AFTER   BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan; " & vbCrLf


        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_68 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub




    Sub Update_75()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_75 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_75 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan

        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '21130', 'Hutang Deposit',   'IDR',  'KREDIT',  'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_75 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

        'Database Transaksi :
        QueryUpdate = Kosongan

        'tbl_SaldoAwalCOA :
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa VALUES ( '21130', '0' ); "

        QueryUpdate &= " ALTER TABLE    tbl_DepositOperasional  ADD Nomor_JV    BIGINT(27)  NOT NULL    AFTER   Keterangan; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_75 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_78()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_78 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_78 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan
        QueryUpdate &= " ALTER TABLE    tbl_AmortisasiBiaya     ADD     Tanggal_Mulai       datetime NOT NULL DEFAULT '1900-01-01 00:00:00'  AFTER   Tanggal_Transaksi; " & vbCrLf

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_AmortisasiBiaya ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Dim NomorID As Int64
        Dim TanggalTransaksi As Date
        Dim TanggalMulai
        Do While dr.Read
            NomorID = dr.Item("Nomor_ID")
            TanggalTransaksi = dr.Item("Tanggal_Transaksi")
            TanggalMulai = AmbilTanggalAkhirBulan_BerdasarkanTanggalLengkap(TanggalTransaksi)
            QueryUpdate &=
                " UPDATE tbl_AmortisasiBiaya SET Tanggal_Mulai = '" & TanggalFormatSimpan(TanggalMulai) & "' " &
                " WHERE Nomor_ID = '" & NomorID & "' ; " & vbCrLf
        Loop
        AksesDatabase_General(Tutup)

        'PesanKhususPCDeveloper(QueryUpdate)

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_78 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

    End Sub



    Sub Update_82()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_82 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_82 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan
        QueryUpdate &= " ALTER TABLE    tbl_AmortisasiBiaya     ADD     Nama_Produk VARCHAR(99) NOT NULL    AFTER   Nama_Akun_Amortisasi; " & vbCrLf

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_82 : General")
        End Try
        TutupDatabaseGeneral_MySQL()

    End Sub




    Sub Update_88()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_88 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_88 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database Transaksi :
        QueryUpdate = Kosongan


        QueryUpdate &= " ALTER TABLE    tbl_Transaksi   ADD Valid   VARCHAR(9)  NOT NULL    AFTER   Status_Approve; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_88 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_90()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_90 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_90 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database Transaksi :
        QueryUpdate = Kosongan

        'Tabel PO Penjualan :
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_PO        ADD Basis_Perhitungan_Termin    VARCHAR(33) NOT NULL        AFTER  Metode_Pembayaran; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_po        CHANGE  Uang_Muka_Persen        Uang_Muka   DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_po        CHANGE  Termin_1_Persen         Termin_1    DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_po        CHANGE  Termin_2_Persen         Termin_2    DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_po        CHANGE  Pelunasan_Persen        Pelunasan   DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Penjualan_PO        SET Basis_Perhitungan_Termin    = '" & BasisPerhitunganTermin_Prosentase & "' "
        QueryUpdate &= " WHERE          Metode_Pembayaran       = '" & MetodePembayaran_Termin & "' ; " & vbCrLf

        'Tabel Invoice Penjualan :
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice   ADD Basis_Perhitungan_Termin    VARCHAR(33)  NOT NULL    AFTER  Metode_Pembayaran; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_penjualan_Invoice   CHANGE  Termin_Persen       Termin      DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Penjualan_Invoice   SET Basis_Perhitungan_Termin    = '" & BasisPerhitunganTermin_Prosentase & "' "
        QueryUpdate &= " WHERE          Metode_Pembayaran       = '" & MetodePembayaran_Termin & "' ; " & vbCrLf

        'Tabel PO Pembelian :
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_PO        ADD Basis_Perhitungan_Termin    VARCHAR(33) NOT NULL        AFTER  Metode_Pembayaran; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_po        CHANGE  Uang_Muka_Persen        Uang_Muka   DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_po        CHANGE  Termin_1_Persen         Termin_1    DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_po        CHANGE  Termin_2_Persen         Termin_2    DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_po        CHANGE  Pelunasan_Persen        Pelunasan   DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Pembelian_PO        SET Basis_Perhitungan_Termin    = '" & BasisPerhitunganTermin_Prosentase & "' "
        QueryUpdate &= " WHERE          Metode_Pembayaran       = '" & MetodePembayaran_Termin & "' ; " & vbCrLf

        'Tabel Invoice Pembelian :
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   ADD Basis_Perhitungan_Termin    VARCHAR(33)  NOT NULL    AFTER  Metode_Pembayaran; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   CHANGE  Termin_Persen       Termin      DECIMAL(27,2)   NOT NULL; " & vbCrLf
        QueryUpdate &= " UPDATE         tbl_Pembelian_Invoice   SET Basis_Perhitungan_Termin    = '" & BasisPerhitunganTermin_Prosentase & "' "
        QueryUpdate &= " WHERE          Metode_Pembayaran       = '" & MetodePembayaran_Termin & "' ; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_90 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_110()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_110 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_110 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database Transaksi :
        QueryUpdate = Kosongan

        'Tabel Bukti Pengeluaran :
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPengeluaran    CHANGE  PPh_Terutang    PPh_Terutang    BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPengeluaran    CHANGE  PPh_Ditanggung  PPh_Ditanggung  BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_BuktiPengeluaran    CHANGE  PPh_Dipotong    PPh_Dipotong    BIGINT(27)  Not NULL; " & vbCrLf

        'Tabel Invoice Pembelian :
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   CHANGE  PPh_Terutang    PPh_Terutang    BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   CHANGE  PPh_Ditanggung  PPh_Ditanggung  BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Pembelian_Invoice   CHANGE  PPh_Dipotong    PPh_Dipotong    BIGINT(27)  Not NULL; " & vbCrLf

        'Tabel Invoice Penjualan :
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice   CHANGE  PPh_Terutang    PPh_Terutang    BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice   CHANGE  PPh_Ditanggung  PPh_Ditanggung  BIGINT(27)  Not NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice   CHANGE  PPh_Dipotong    PPh_Dipotong    BIGINT(27)  Not NULL; " & vbCrLf

        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_110 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub Update_129()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        '-------------------------------------------------------------------------------------------------------
        Dim QueryUpdate As String = " ALTER TABLE tbl_dummy ADD Upd_129 varchar(9) NOT NULL AFTER Upd_99999; " & vbCrLf
        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_129 : General")
        End Try
        TutupDatabaseGeneral_MySQL()
        If Not StatusSuntingDatabase Then Return
        '-------------------------------------------------------------------------------------------------------

        'Database General :
        QueryUpdate = Kosongan
        QueryUpdate &= " UPDATE tbl_COA SET COA = '51100' WHERE COA = '58811'; " & vbCrLf   '(Pembelian Bahan Baku - Lokal)
        QueryUpdate &= " UPDATE tbl_COA SET COA = '51200' WHERE COA = '58812'; " & vbCrLf   '(Pembelian Bahan Baku - Impor)
        QueryUpdate &= " UPDATE tbl_COA SET COA = '53100' WHERE COA = '58821'; " & vbCrLf   '(Pembelian Bahan Penolong)

        BukaDatabaseGeneral_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseGeneral_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
            PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_129 : General")
        End Try
        TutupDatabaseGeneral_MySQL()


        'Database Transaksi :
        QueryUpdate = Kosongan

        'Tabel Transaksi :
        QueryUpdate &= " UPDATE tbl_Transaksi SET COA = '51100' WHERE COA = '58811'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Transaksi SET COA = '51200' WHERE COA = '58812'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Transaksi SET COA = '53100' WHERE COA = '58821'; " & vbCrLf


        'Tabel Invoice Pembelian :
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice SET COA_Produk =  '51100' WHERE COA_Produk =  '58811'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice SET COA_Produk =  '51200' WHERE COA_Produk =  '58812'; " & vbCrLf
        QueryUpdate &= " UPDATE tbl_Pembelian_Invoice SET COA_Produk =  '53100' WHERE COA_Produk =  '58821'; " & vbCrLf


        'Susur Database Transaksi :
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT Tahun_Buku FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            BukaDatabaseTransaksi_Alternatif_MySQL(drTELUSUR.Item("Tahun_Buku"))
            cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_Alternatif_MySQL)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
                PesanKhususPCDeveloper("ERROR : " & Enter2Baris & ex.Message & Enter2Baris & "Update_129 : Transaksi - " & drTELUSUR.Item("Tahun_Buku"))
            End Try
            TutupDatabaseTransaksi_Alternatif_MySQL()
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub Update_151()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim NomorUpdate As Integer = 151
        Dim QueryUpdate As String = Kosongan

        CekStatusUpdate(NomorUpdate)
        If Not StatusSuntingDatabase Then Return


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database General :
        QueryUpdate = Kosongan
        QueryUpdate &= " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "
        QueryUpdate &= " ( '11217', 'Kas (GBP)',                            'GBP',  'DEBET',    'Tidak' ), "
        QueryUpdate &= " ( '11517', 'Piutang Usaha (GBP)',                  'GBP',  'DEBET',    'Tidak' ), "
        QueryUpdate &= " ( '11707', 'Uang Muka Pembelian - Impor (GBP)',    'GBP',  'DEBET',    'Tidak' ), "
        QueryUpdate &= " ( '11717', 'Biaya Dibayar Dimuka (GBP)',           'GBP',  'DEBET',    'Tidak' ), "
        QueryUpdate &= " ( '21117', 'Hutang Usaha (GBP)',                   'GBP',  'DEBET',    'Tidak' ), "
        QueryUpdate &= " ( '21507', 'Uang Muka Penjualan - Ekspor (GBP)',   'GBP',  'DEBET',    'Tidak' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma, tapi pakai titik koma..!

        UpdateDatabaseGeneral(QueryUpdate, NomorUpdate)


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database Transaksi :
        QueryUpdate = Kosongan
        QueryUpdate &= " INSERT INTO tbl_saldoawalcoa ( COA, Saldo_Awal ) VALUES "
        QueryUpdate &= " ( '11217', '0' ), "
        QueryUpdate &= " ( '11517', '0' ), "
        QueryUpdate &= " ( '11707', '0' ), "
        QueryUpdate &= " ( '11717', '0' ), "
        QueryUpdate &= " ( '21117', '0' ), "
        QueryUpdate &= " ( '21507', '0' ); " & vbCrLf  '<-- Ujurng Query, tidak pakai koma..!

        UpdateDatabaseTransaksi(QueryUpdate, NomorUpdate)

    End Sub


    Sub Update_152()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim NomorUpdate As Integer = 152
        Dim QueryUpdate As String = Kosongan

        CekStatusUpdate(NomorUpdate)
        If Not StatusSuntingDatabase Then Return


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database Transaksi :
        QueryUpdate = Kosongan
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice MODIFY    DPP_Barang              bigint(27) NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice MODIFY    DPP_Jasa                bigint(27) NOT NULL; " & vbCrLf
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice MODIFY    Dasar_Pengenaan_Pajak   bigint(27) NOT NULL; " & vbCrLf

        UpdateDatabaseTransaksi(QueryUpdate, NomorUpdate)

    End Sub




    Sub Update_160()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim NomorUpdate = 160
        Dim QueryUpdate As String = Kosongan

        CekStatusUpdate(NomorUpdate)
        If Not StatusSuntingDatabase Then Return


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database Transaksi :
        QueryUpdate = Kosongan
        QueryUpdate &= " ALTER TABLE    tbl_Penjualan_Invoice MODIFY    PPN         bigint(27) NOT NULL; " & vbCrLf

        UpdateDatabaseTransaksi(QueryUpdate, NomorUpdate)

    End Sub




    Sub Update_164()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim NomorUpdate = 164
        Dim QueryUpdate As String = Kosongan

        CekStatusUpdate(NomorUpdate)
        If Not StatusSuntingDatabase Then Return


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database Transaksi :
        QueryUpdate = Kosongan
        QueryUpdate &= " INSERT INTO tbl_kursakhirbulan (Kode_Mata_Uang) VALUES ('GBP'); " & vbCrLf

        UpdateDatabaseTransaksi(QueryUpdate, NomorUpdate)

    End Sub


    Sub Update_166()
        'Jika sudah dipublish, maka jangan ada lagi yang dirubah...!!!
        'Jika ada kekeliruan, maka direvisi pada update-an berikutnya...!!!

        Dim NomorUpdate = 166
        Dim QueryUpdate As String = Kosongan

        CekStatusUpdate(NomorUpdate)
        If Not StatusSuntingDatabase Then Return


        '------------------------------------------------------------------------------------------------------------------------------------------------
        'Database Transaksi :
        QueryUpdate = Kosongan
        QueryUpdate &= " Update tbl_Pembelian_Invoice SET Termin = 100 WHERE Metode_Pembayaran = 'Normal'; " & vbCrLf
        QueryUpdate &= " Update tbl_Penjualan_Invoice SET Termin = 100 WHERE Metode_Pembayaran = 'Normal'; " & vbCrLf

        UpdateDatabaseTransaksi(QueryUpdate, NomorUpdate)

    End Sub


End Module
