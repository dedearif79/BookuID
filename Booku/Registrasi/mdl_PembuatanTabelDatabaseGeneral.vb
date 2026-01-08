Imports bcomm
Imports System.Data.Odbc

Module mdl_PembuatanTabelDatabaseGeneral

    Dim QueryPembuatanTabel
    Dim QueryAlterTable
    Dim QueryIsiTabel


    Public Sub PembuatanKerangkaTabelDatabaseGeneral()

        Try

            'Pembuatan Tabel : tbl_AmortisasiBiaya
            QueryPembuatanTabel = " CREATE TABLE `tbl_AmortisasiBiaya` ( " &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_Pembelian` varchar(33) NOT NULL, " &
                " `Kode_Asset` varchar(63) NOT NULL, " &
                " `COA_Amortisasi` varchar(15) NOT NULL, " &
                " `Nama_Akun_Amortisasi` varchar(63) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `COA_Biaya` varchar(15) NOT NULL, " &
                " `Nama_Akun_Biaya` varchar(63) NOT NULL, " &
                " `Masa_Amortisasi` int(7) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Mulai` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Transaksi` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_AmortisasiBiaya` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_COA
            QueryPembuatanTabel = " CREATE TABLE `tbl_COA` (" &
                " `COA`                 varchar(25)     NOT NULL, " &
                " `Nama_Akun`           varchar(99)     NOT NULL, " &
                " `Kode_Mata_Uang`      varchar(9)      NOT NULL, " &
                " `D_K`                 varchar(7)      NOT NULL, " &
                " `Saldo_Awal`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Januari`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Januari`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Januari`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Februari`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Februari`     decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Februari`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Maret`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Maret`        decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Maret`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_April`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_April`        decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_April`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Mei`           decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Mei`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Mei`           decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Juni`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Juni`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Juni`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Juli`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Juli`         decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Juli`          decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Agustus`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Agustus`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Agustus`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_September`     decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_September`    decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_September`     decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Oktober`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Oktober`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Oktober`       decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Nopember`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Nopember`     decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Nopember`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Debet_Desember`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Kredit_Desember`     decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Saldo_Desember`      decimal(21,2)   NOT NULL DEFAULT '0.00', " &
                " `Uraian`              varchar(255)    NOT NULL, " &
                " `Visibilitas`         varchar(12)     NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_COA` ADD PRIMARY KEY (`COA`), ADD UNIQUE KEY (`COA`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_Company
            QueryPembuatanTabel = " CREATE TABLE `tbl_Company` (" &
                " `Nomor_Seri_Produk` varchar(33) NOT NULL, " &
                " `ID_Customer` varchar(12) NOT NULL, " &
                " `Nama_Perusahaan` varchar(72) NOT NULL, " &
                " `Tagline` varchar(99) NOT NULL, " &
                " `Jenis_Usaha` varchar(33) NOT NULL, " &
                " `Jenis_WP` varchar(33) NOT NULL, " &
                " `NPWP` varchar(33) NOT NULL, " &
                " `Nama_Direktur` varchar(63) NOT NULL, " &
                " `Alamat` longtext NOT NULL, " &
                " `Email` varchar(99) NOT NULL, " &
                " `PIC` varchar(33) NOT NULL, " &
                " `Nomor_SKT` varchar(69) NOT NULL, " &
                " `Tanggal_SKT` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_KPP` varchar(9) NOT NULL, " &
                " `Email_DJPO` varchar(99) NOT NULL, " &
                " `Password_DJPO` longtext NOT NULL, " &
                " `Nomor_Suket_UMKM` varchar(63) NOT NULL, " &
                " `Tanggal_Suket_UMKM` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_PKP` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pemotong_PPh` int(3) NOT NULL, " &
                " `Tanggal_Expire_SE` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Password_E_Faktur` longtext NOT NULL, " &
                " `Kode_Aktivasi` longtext NOT NULL, " &
                " `Passphrase` longtext NOT NULL, " &
                " `Level_PJK` int(3) NOT NULL, " &
                " `Tanggal_Expire_SBU` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Company` ADD PRIMARY KEY(`NPWP`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_DaftarSaham
            QueryPembuatanTabel = " CREATE TABLE `tbl_DaftarSaham` (" &
                " `Saham_Ke` int(3) NOT NULL, " &
                " `Harga` bigint(27) NOT NULL  " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DaftarSaham` ADD PRIMARY KEY (`Saham_Ke`), ADD UNIQUE KEY (`Saham_Ke`), ADD UNIQUE KEY (`Harga`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)


            'Pembuatan Tabel : tbl_SC
            QueryPembuatanTabel = " CREATE TABLE `tbl_SC` (" &
                " `ID` int(3) NOT NULL, " &
                " `Tahun_Buku_Terakhir_Dibuka` int(9) NOT NULL, " &
                " `SC_01` longtext NOT NULL, " &
                " `SC_02` longtext NOT NULL, " &
                " `SC_03` longtext NOT NULL, " &
                " `SC_04` longtext NOT NULL, " &
                " `SC_90` longtext NOT NULL, " &
                " `SC_99` longtext NOT NULL  " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_SC` ADD PRIMARY KEY(`ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_DataAsset
            QueryPembuatanTabel = " CREATE TABLE `tbl_DataAsset` ( " &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_Pembelian` varchar(33) NOT NULL, " &
                " `Kode_Asset` varchar(33) NOT NULL, " &
                " `Nama_Aktiva` varchar(63) NOT NULL, " &
                " `COA_Asset` varchar(17) NOT NULL, " &
                " `COA_Biaya_Penyusutan` varchar(17) NOT NULL, " &
                " `COA_Akumulasi_Penyusutan` varchar(17) NOT NULL, " &
                " `Kelompok_Harta` int(7) NOT NULL, " &
                " `Masa_Manfaat` int(7) NOT NULL, " &
                " `Kode_Divisi` varchar(9) NOT NULL, " &
                " `Divisi` varchar(63) NOT NULL, " &
                " `Tanggal_Perolehan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Harga_Perolehan` bigint(27) NOT NULL, " &
                " `Harga_Jual` bigint(27) NOT NULL, " &
                " `Akumulasi_Penyusutan` bigint(27) NOT NULL, " &
                " `Tanggal_Closing` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Closing` varchar(33) NOT NULL, " &
                " `Nomor_JV_Closing` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DataAsset` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_DataKaryawan
            QueryPembuatanTabel = " CREATE TABLE `tbl_DataKaryawan` ( " &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Tanggal_Registrasi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_ID_Karyawan` varchar(33) NOT NULL, " &
                " `NIK` varchar(33) NOT NULL, " &
                " `Nama_Karyawan` varchar(99) NOT NULL, " &
                " `Jabatan` varchar(69) NOT NULL, " &
                " `Rekening_Bank` varchar(63) NOT NULL, " &
                " `Atas_Nama` varchar(63) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Status_Aktif` int(3) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DataKaryawan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_DivisiAsset
            QueryPembuatanTabel = " CREATE TABLE `tbl_DivisiAsset` (" &
                " `Kode_Divisi` varchar(9) NOT NULL, " &
                " `Divisi` varchar(45) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DivisiAsset` ADD PRIMARY KEY(`Kode_Divisi`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_Dummy
            QueryPembuatanTabel = " CREATE TABLE `tbl_Dummy` (" &
                " `Upd_99999`    varchar(9)     NOT NULL, " &
                " `Upd_166`      varchar(9)     NOT NULL, " &
                " `Upd_164`      varchar(9)     NOT NULL, " &
                " `Upd_160`      varchar(9)     NOT NULL, " &
                " `Upd_152`      varchar(9)     NOT NULL, " &
                " `Upd_151`      varchar(9)     NOT NULL, " &
                " `Upd_129`      varchar(9)     NOT NULL, " &
                " `Upd_110`      varchar(9)     NOT NULL, " &
                " `Upd_90`       varchar(9)     NOT NULL, " &
                " `Upd_88`       varchar(9)     NOT NULL, " &
                " `Upd_82`       varchar(9)     NOT NULL, " &
                " `Upd_78`       varchar(9)     NOT NULL, " &
                " `Upd_75`       varchar(9)     NOT NULL, " &
                " `Upd_68`       varchar(9)     NOT NULL, " &
                " `Upd_51`       varchar(9)     NOT NULL, " &
                " `Upd_47`       varchar(9)     NOT NULL, " &
                " `Upd_46`       varchar(9)     NOT NULL, " &
                " `Upd_45`       varchar(9)     NOT NULL, " &
                " `Upd_44`       varchar(9)     NOT NULL, " &
                " `Upd_43`       varchar(9)     NOT NULL, " &
                " `Upd_42`       varchar(9)     NOT NULL, " &
                " `Upd_41`       varchar(9)     NOT NULL, " &
                " `Upd_40`       varchar(9)     NOT NULL, " &
                " `Upd_38`       varchar(9)     NOT NULL, " &
                " `Upd_37`       varchar(9)     NOT NULL, " &
                " `Upd_36`       varchar(9)     NOT NULL, " &
                " `Upd_32`       varchar(9)     NOT NULL, " &
                " `Upd_31`       varchar(9)     NOT NULL, " &
                " `Upd_0`        varchar(9)     NOT NULL  " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_infoaplikasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_infoaplikasi` (" &
                " `Nama_Aplikasi` varchar(45) NOT NULL, " &
                " `Versi_App` varchar(63) NOT NULL, " &
                " `Apdet_App` varchar(63) NOT NULL, " &
                " `Nomor_Hotline` varchar(27) NOT NULL, " &
                " `Website` varchar(45) NOT NULL, " &
                " `Email` varchar(45) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_infoaplikasi` ADD PRIMARY KEY (`Versi_App`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_InfoData
            QueryPembuatanTabel = " CREATE TABLE `tbl_InfoData` (" &
                " `Tahun_Buku` int(9) NOT NULL, " &
                " `Jenis_Tahun_Buku` varchar(27) NOT NULL, " &
                " `Trial_Balance` int(3) NOT NULL, " &
                " `Status_Buku` varchar(27) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_InfoData` ADD PRIMARY KEY (`Tahun_Buku`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_JenisPajak
            QueryPembuatanTabel = " CREATE TABLE `tbl_JenisPajak` (" &
                " `Kode_Jenis_Pajak` int(9) NOT NULL, " &
                " `Jenis_Pajak` varchar(33) NOT NULL, " &
                " `Jenis_Surat_Ketetapan` varchar(17) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JenisPajak` ADD PRIMARY KEY (`Kode_Jenis_Pajak`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryIsiTabel = Kosongan
            QueryIsiTabel &= " INSERT INTO `tbl_jenispajak` (`Kode_Jenis_Pajak`, `Jenis_Pajak`, `Jenis_Surat_Ketetapan`) VALUES "
            QueryIsiTabel &= " (101, 'PPh Pasal 21', 'STP'), "
            QueryIsiTabel &= " (201, 'PPh Pasal 21', 'SKPKB'), "
            QueryIsiTabel &= " (301, 'PPh Pasal 21', 'SKPKBT'), "
            QueryIsiTabel &= " (401, 'PPh Pasal 21', 'SKPLB'), "
            QueryIsiTabel &= " (501, 'PPh Pasal 21', 'SKPN'), "
            QueryIsiTabel &= " (102, 'PPh Pasal 22', 'STP'), "
            QueryIsiTabel &= " (202, 'PPh Pasal 22', 'SKPKB'), "
            QueryIsiTabel &= " (302, 'PPh Pasal 22', 'SKPKBT'), "
            QueryIsiTabel &= " (402, 'PPh Pasal 22', 'SKPLB'), "
            QueryIsiTabel &= " (502, 'PPh Pasal 22', 'SKPN'), "
            QueryIsiTabel &= "(122, 'PPh Pasal 22 - Impor', 'STP'), "
            QueryIsiTabel &= "(222, 'PPh Pasal 22 - Impor', 'SKPKB'), "
            QueryIsiTabel &= "(322, 'PPh Pasal 22 - Impor', 'SKPKBT'), "
            QueryIsiTabel &= "(422, 'PPh Pasal 22 - Impor', 'SKPLB'), "
            QueryIsiTabel &= "(522, 'PPh Pasal 22 - Impor', 'SKPN'), "
            QueryIsiTabel &= "(103, 'PPh Pasal 23', 'STP'), "
            QueryIsiTabel &= "(203, 'PPh Pasal 23', 'SKPKB'), "
            QueryIsiTabel &= "(303, 'PPh Pasal 23', 'SKPKBT'), "
            QueryIsiTabel &= "(403, 'PPh Pasal 23', 'SKPLB'), "
            QueryIsiTabel &= "(503, 'PPh Pasal 23', 'SKPN'), "
            QueryIsiTabel &= "(104, 'PPh Pasal 26', 'STP'), "
            QueryIsiTabel &= "(204, 'PPh Pasal 26', 'SKPKB'), "
            QueryIsiTabel &= "(304, 'PPh Pasal 26', 'SKPKBT'), "
            QueryIsiTabel &= "(404, 'PPh Pasal 26', 'SKPLB'), "
            QueryIsiTabel &= "(504, 'PPh Pasal 26', 'SKPN'), "
            QueryIsiTabel &= "(106, 'PPh Pasal 25', 'STP'), "
            QueryIsiTabel &= "(206, 'PPh Pasal 25', 'SKPKB'), "
            QueryIsiTabel &= "(306, 'PPh Pasal 25', 'SKPKBT'), "
            QueryIsiTabel &= "(406, 'PPh Pasal 25', 'SKPLB'), "
            QueryIsiTabel &= "(506, 'PPh Pasal 25', 'SKPN'), "
            QueryIsiTabel &= "(140, 'PPh Pasal 4 (2) ', 'STP'), "
            QueryIsiTabel &= "(240, 'PPh Pasal 4 (2) ', 'SKPKB'), "
            QueryIsiTabel &= "(340, 'PPh Pasal 4 (2) ', 'SKPKBT'), "
            QueryIsiTabel &= "(440, 'PPh Pasal 4 (2) ', 'SKPLB'), "
            QueryIsiTabel &= "(540, 'PPh Pasal 4 (2) ', 'SKPN'), "
            QueryIsiTabel &= "(107, 'PPN', 'STP'), "
            QueryIsiTabel &= "(207, 'PPN', 'SKPKB'), "
            QueryIsiTabel &= "(307, 'PPN', 'SKPKBT'), "
            QueryIsiTabel &= "(407, 'PPN', 'SKPLB'), "
            QueryIsiTabel &= "(507, 'PPN', 'SKPN'), "
            QueryIsiTabel &= "(127, 'PPN - Impor', 'STP'), "
            QueryIsiTabel &= "(227, 'PPN - Impor', 'SKPKB'), "
            QueryIsiTabel &= "(327, 'PPN - Impor', 'SKPKBT'), "
            QueryIsiTabel &= "(427, 'PPN - Impor', 'SKPLB'), "
            QueryIsiTabel &= "(527, 'PPN - Impor', 'SKPN'); "
            cmd = New OdbcCommand(QueryIsiTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_DataProject
            QueryPembuatanTabel = " CREATE TABLE `tbl_DataProject` ( " &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Kode_Project` varchar(99) NOT NULL, " &
                " `Nama_Project` varchar(99) NOT NULL, " &
                " `Nomor_PO` varchar(99) NOT NULL, " &
                " `Kode_Customer` varchar(99) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Nilai_Project` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Status` varchar(9) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DataProject` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_LawanTransaksi
            QueryPembuatanTabel = " CREATE TABLE `tbl_LawanTransaksi` (" &
                " `Kode_Mitra` varchar(12) NOT NULL, " &
                " `Nama_Mitra` varchar(255) NOT NULL, " &
                " `UMKM` int(3) NOT NULL, " &
                " `Pemegang_Saham` int(3) NOT NULL, " &
                " `Afiliasi` int(3) NOT NULL, " &
                " `Supplier` int(3) NOT NULL, " &
                " `Customer` int(3) NOT NULL, " &
                " `Keuangan` int(3) NOT NULL, " &
                " `PKP` int(3) NOT NULL, " &
                " `Pemotong_PPh` int(3) NOT NULL, " &
                " `PJK` int(3) NOT NULL, " &
                " `NPWP` varchar(27) NOT NULL, " &
                " `Jenis_WP` varchar(27) NOT NULL, " &
                " `Lokasi_WP` varchar(27) NOT NULL, " &
                " `Alamat` longtext NOT NULL, " &
                " `Email` varchar(99) NOT NULL, " &
                " `PIC` varchar(99) NOT NULL, " &
                " `Rekening_Bank` varchar(63) NOT NULL, " &
                " `Atas_Nama` varchar(63) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_LawanTransaksi` ADD PRIMARY KEY (`Kode_Mitra`), ADD UNIQUE KEY (`Kode_Mitra`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_Modal
            QueryPembuatanTabel = " CREATE TABLE `tbl_Modal` ( " &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPM` varchar(33) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Pemegang_Saham` varchar(17) NOT NULL, " &
                " `Nama_Pemegang_Saham` varchar(63) NOT NULL, " &
                " `Jumlah_Lembar` bigint(27) NOT NULL, " &
                " `Harga_Per_Lembar` bigint(27) NOT NULL, " &
                " `COA` varchar(17) NOT NULL, " &
                " `Jumlah_Debet` bigint(27) NOT NULL, " &
                " `Jumlah_Kredit` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Modal` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_Jaringan
            QueryPembuatanTabel = " CREATE TABLE `tbl_perangkat` (" &
                " `ID_Komputer` varchar(33) NOT NULL, " &
                " `Kode_Khusus` longtext NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_perangkat` ADD PRIMARY KEY (`ID_Komputer`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_TautanCOA
            QueryPembuatanTabel = " CREATE TABLE `tbl_TautanCOA` (" &
                " `Tautan_COA` varchar(99) NOT NULL, " &
                " `COA` varchar(9) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_TautanCOA` ADD PRIMARY KEY (`Tautan_COA`), ADD UNIQUE KEY (`Tautan_COA`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Pembuatan Tabel : tbl_User
            QueryPembuatanTabel = " CREATE TABLE `tbl_User` (" &
                " `Username` varchar(63) NOT NULL, " &
                " `Password` longtext NOT NULL, " &
                " `Level` int(3) NOT NULL, " &
                " `Nama` varchar(63) NOT NULL, " &
                " `Jabatan` varchar(33) NOT NULL, " &
                " `Cluster_Finance` int(3) NOT NULL, " &
                " `Cluster_Accounting` int(3) NOT NULL, " &
                " `Status_Aktif` int(3) NOT NULL " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_User` ADD PRIMARY KEY (`Username`), ADD UNIQUE KEY (`Username`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            HasilPembuatanDatabaseGeneral = True

        Catch ex As Exception

            HasilPembuatanDatabaseGeneral = False

        End Try

    End Sub

End Module
