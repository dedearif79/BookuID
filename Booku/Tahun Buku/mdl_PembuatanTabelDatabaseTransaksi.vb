Imports System.Data.Odbc

Module mdl_PembuatanTabelDatabaseTransaksi

    Dim QueryPembuatanTabel
    Dim QueryAlterTable
    Dim NomorUrut

    Public Sub PembuatanKerangkaTabelDatabaseTransaksi()

        Dim cmdPengisianTabel As OdbcCommand

        Try

            'Pembuatan Tabel : tbl_AktivaLainnya
            QueryPembuatanTabel = " CREATE TABLE `tbl_AktivaLainnya` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPAL` varchar(33) NOT NULL, " &
                " `Nomor_Bukti` varchar(63) NOT NULL, " &
                " `Tanggal_Bukti` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Uraian_Transaksi` longtext NOT NULL, " &
                " `COA_Debet` varchar(9) NOT NULL, " &
                " `COA_Kredit` varchar(9) NOT NULL, " &
                " `Jumlah_Transaksi` bigint(27) NOT NULL, " &
                " `Tanggal_Pencairan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_AktivaLainnya` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_BankGaransi
            QueryPembuatanTabel = " CREATE TABLE `tbl_BankGaransi` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPBG` varchar(33) NOT NULL, " &
                " `Nomor_Kontrak` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nama_Bank` varchar(63) NOT NULL, " &
                " `Keperluan` varchar(63) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Jumlah_Transaksi` bigint(27) NOT NULL, " &
                " `Biaya_Provisi` bigint(27) NOT NULL, " &
                " `COA_Kredit` varchar(9) NOT NULL, " &
                " `Tanggal_Pencairan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV_Transaksi` bigint(27) NOT NULL, " &
                " `Nomor_JV_Pencairan` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_BankGaransi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_BuktiPenerimaan
            QueryPembuatanTabel = " CREATE TABLE `tbl_BuktiPenerimaan` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_KM` bigint(27) NOT NULL, " &
                " `Nomor_KM` varchar(33) NOT NULL, " &
                " `Tanggal_KM` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kategori` varchar(33) NOT NULL, " &
                " `Peruntukan` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Nomor_BP` varchar(33) NOT NULL, " &
                " `Nomor_Faktur_Pajak` varchar(33) NOT NULL, " &
                " `Nomor_Invoice` varchar(63) NOT NULL, " &
                " `Tanggal_Invoice` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Uraian_Invoice` longtext NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Kurs` decimal(21,2) NOT NULL, " &
                " `Jumlah_Tagihan` decimal(21,2) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Pokok` decimal(21,2) NOT NULL, " &
                " `Bagi_Hasil` decimal(21,2) NOT NULL, " &
                " `Jumlah_Bayar` decimal(21,2) NOT NULL, " &
                " `Denda` decimal(21,2) NOT NULL, " &
                " `COA_Debet` varchar(9) NOT NULL, " &
                " `COA_Kredit` varchar(9) NOT NULL, " &
                " `Biaya_Administrasi_Bank` decimal(21,2) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Jenis_Pajak` varchar(27) NOT NULL, " &   'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(17) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_BuktiPenerimaan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_BuktiPengeluaran
            QueryPembuatanTabel = " CREATE TABLE `tbl_BuktiPengeluaran` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_KK` bigint(27) NOT NULL, " &
                " `Nomor_KK` varchar(33) NOT NULL, " &
                " `Tanggal_KK` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Bundel` varchar(33) NOT NULL, " &
                " `Kategori` varchar(33) NOT NULL, " &
                " `Peruntukan` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Nomor_BP` varchar(33) NOT NULL, " &
                " `Nomor_Invoice` varchar(63) NOT NULL, " &
                " `Tanggal_Invoice` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Uraian_Invoice` longtext NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Kurs` decimal(21,2) NOT NULL, " &
                " `Jumlah_Tagihan` decimal(21,2) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Pokok` decimal(21,2) NOT NULL, " &
                " `Bagi_Hasil` decimal(21,2) NOT NULL, " &
                " `Jumlah_Pengajuan` decimal(21,2) NOT NULL, " &
                " `Jumlah_Bayar` decimal(21,2) NOT NULL, " &
                " `Denda` decimal(21,2) NOT NULL, " &
                " `COA_Debet` varchar(9) NOT NULL, " &
                " `COA_Kredit` varchar(9) NOT NULL, " &
                " `Biaya_Administrasi_Bank` decimal(21,2) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Rekening_Penerima` varchar(63) NOT NULL, " &
                " `Atas_Nama_Penerima` varchar(63) NOT NULL, " &
                " `Status_Invoice` varchar(17) NOT NULL, " &
                " `Status_Pengajuan` varchar(17) NOT NULL, " &
                " `Jenis_Pajak` varchar(27) NOT NULL, " &   'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(17) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_BuktiPengeluaran` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_BundelPengajuanPengeluaran
            QueryPembuatanTabel = " CREATE TABLE `tbl_BundelPengajuanPengeluaran` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_Bundel` bigint(27) NOT NULL, " &
                " `Nomor_Bundel` varchar(63) NOT NULL, " &
                " `Tanggal_Bundel` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_KK_Per_Baris` varchar(99) NOT NULL, " &
                " `Kode_Lawan_Transaksi_Per_Baris` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi_Per_Baris` varchar(63) NOT NULL, " &
                " `Jumlah_Invoice_Per_Baris` bigint(27) NOT NULL, " &
                " `Jumlah_Pengajuan_Per_Baris` bigint(27) NOT NULL, " &
                " `Jumlah_Disetujui_Per_Baris` bigint(27) NOT NULL, " &
                " `COA_Kredit_Per_Baris` varchar(9) NOT NULL, " &
                " `Status` varchar(17) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_BundelPengajuanPengeluaran` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_DepositOperasional
            QueryPembuatanTabel = " CREATE TABLE `tbl_DepositOperasional` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_BPDO` bigint(27) NOT NULL, " &
                " `Nomor_BPDO` varchar(33) NOT NULL, " &
                " `Nomor_Bukti` varchar(63) NOT NULL, " &
                " `Tanggal_Bukti` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Faktur_Pajak` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(63) NOT NULL, " &
                " `Nomor_Urut_Produk` bigint(27) NOT NULL, " &
                " `COA_Produk` varchar(9) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Nomor_Referensi_Produk` varchar(63) NOT NULL, " &
                " `Tanggal_Referensi_Produk` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Harga_Produk` bigint(27) NOT NULL, " &
                " `Jumlah_Transaksi` bigint(27) NOT NULL, " &
                " `Jumlah_Reimburse` bigint(27) NOT NULL, " &
                " `Potongan_Reimburse` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_DepositOperasional` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranHutangBank
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranHutangBank` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHB` varchar(33) NOT NULL, " &
                " `Kode_Kreditur` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &     'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranHutangBank` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranHutangLeasing
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranHutangLeasing` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHL` varchar(33) NOT NULL, " &
                " `Kode_Kreditur` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " & 'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranHutangLeasing` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranHutangAfiliasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranHutangAfiliasi` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHA` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " & 'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranHutangAfiliasi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranHutangPihakKetiga
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranHutangPihakKetiga` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHPK` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " & 'Jangan dikurangi...!!!
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranHutangPihakKetiga` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PenjualanEceran
            QueryPembuatanTabel = " CREATE TABLE `tbl_PenjualanEceran` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Kas` bigint(27) NOT NULL, " &
                " `Jumlah_Bank` bigint(27) NOT NULL, " &
                " `Jumlah_Transaksi` bigint(27) NOT NULL, " &
                " `DPP` bigint(27) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PenjualanEceran` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_KetetapanPajak
            QueryPembuatanTabel = " CREATE TABLE `tbl_KetetapanPajak` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor` varchar(17) NOT NULL, " &
                " `Kode_Jenis_Pajak` varchar(9) NOT NULL, " &
                " `Jenis_Pajak` varchar(27) NOT NULL, " &   'Jangan dikurangi...!!!
                " `Masa_Pajak_Awal` varchar(17) NOT NULL, " &
                " `Masa_Pajak_Akhir` varchar(17) NOT NULL, " &
                " `Tahun_Pajak` int(9) NOT NULL, " &
                " `Tanggal_Ketetapan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Ketetapan` varchar(99) NOT NULL, " &
                " `Nomor_BPHP` varchar(33) NOT NULL, " &
                " `Kode_Akun_Pokok_Pajak` varchar(17) NOT NULL, " &
                " `Jumlah_Ketetapan` bigint(27) NOT NULL, " &
                " `Pokok_Pajak` bigint(27) NOT NULL, " &
                " `Sanksi` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_KetetapanPajak` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Notifikasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_Notifikasi` (" &
                " `Nomor_ID` bigint(33) NOT NULL, " &
                " `Jenis_Notifikasi` varchar(21) NOT NULL, " &
                " `Waktu` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Notifikasi` longtext NOT NULL, " &
                " `Halaman_Target` varchar(99) NOT NULL, " &
                " `Pesan` longtext NOT NULL, " &
                " `Status_Dibaca` int(3) NOT NULL, " &
                " `Status_Dieksekusi` int(3) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Notifikasi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pembelian_BAST
            QueryPembuatanTabel = " CREATE TABLE `tbl_Pembelian_BAST` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_BAST` bigint(27) NOT NULL, " &
                " `Nomor_BAST` varchar(99) NOT NULL, " &
                " `Tanggal_BAST` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jenis_Produk_Induk` varchar(27) NOT NULL, " &
                " `Yang_Menyerahkan` varchar(63) NOT NULL, " &
                " `Yang_Menerima` varchar(63) NOT NULL, " &
                " `Tanggal_Diterima` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_PO_Produk` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Keterangan_Produk` varchar(99) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pembelian_BAST` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pembelian_Invoice
            QueryPembuatanTabel = " CREATE TABLE `tbl_Pembelian_Invoice` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_Invoice` bigint(27) NOT NULL, " &
                " `Nomor_Invoice` longtext NOT NULL, " &
                " `Jenis_Invoice` varchar(27) NOT NULL, " &
                " `Metode_Pembayaran` varchar(33) NOT NULL, " &
                " `Basis_Perhitungan_Termin` varchar(33) NOT NULL, " &
                " `Nomor_Pembelian` varchar(33) NOT NULL, " &
                " `Referensi` varchar(99) NOT NULL, " &
                " `N_P` varchar(9) NOT NULL, " &
                " `Tanggal_Invoice` datetime NOT NULL, " &
                " `Tanggal_Diterima_Invoice` datetime NOT NULL, " &
                " `Tanggal_Pembetulan` datetime NOT NULL, " &
                " `Tanggal_Lapor` datetime NOT NULL, " &
                " `Jumlah_Hari_Jatuh_Tempo` int(9) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(27) NOT NULL, " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(99) NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Kurs` decimal(21,2) NOT NULL, " &
                " `Jenis_Jasa` varchar(63) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(27) NOT NULL, " &
                " `Nomor_SJ_BAST_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_SJ_BAST_Produk` datetime NOT NULL, " &
                " `Tanggal_Diterima_SJ_BAST_Produk` datetime NOT NULL, " &
                " `Tanggal_Serah_Terima` datetime NOT NULL, " &
                " `Loko` varchar(27) NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `COA_Produk` varchar(9) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` decimal(21,2) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` decimal(21,2) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` decimal(21,2) NOT NULL, " &
                " `Diskon` decimal(21,2) NOT NULL, " &
                " `Tahap_Termin` varchar(33) NOT NULL, " &
                " `Termin` decimal(27,2) NOT NULL, " &
                " `Insurance` decimal(21,2) NOT NULL, " &
                " `Freight` decimal(21,2) NOT NULL, " &
                " `Bea_Masuk` decimal(21,2) NOT NULL, " &
                " `Kurs_KMK` decimal(21,2) NOT NULL, " &
                " `DPP_Barang` bigint(27) NOT NULL, " &
                " `DPP_Jasa` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Nomor_Faktur_Pajak` varchar(99) NOT NULL, " &
                " `Tanggal_Faktur_Pajak` datetime NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `PPN_Dikreditkan` varchar(9) NOT NULL, " &
                " `Pilihan_PPN` varchar(27) NOT NULL, " &
                " `Nomor_SKB` varchar(99) NOT NULL, " &
                " `Tanggal_SKB` datetime NOT NULL, " &
                " `Tarif_PPN` decimal(5,2) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Total_Tagihan_Kotor` decimal(21,2) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Total_Tagihan` decimal(21,2) NOT NULL, " &
                " `Jumlah_Hutang_Usaha` decimal(21,2) NOT NULL, " &
                " `Jenis_Pembelian` varchar(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Sarana_Pembayaran` varchar(27) NOT NULL, " &
                " `Biaya_Administrasi_Bank` decimal(21,2) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Biaya_Transportasi` bigint(27) NOT NULL, " &
                " `Biaya_Materai` bigint(27) NOT NULL, " &
                " `Retur_DPP` decimal(21,2) NOT NULL, " &
                " `Retur_PPN` decimal(21,2) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Kode_Divisi_Asset` varchar(9) NOT NULL, " &
                " `Kelompok_Asset` int(3) NOT NULL, " &
                " `COA_Biaya` varchar(17) NOT NULL, " &
                " `Masa_Amortisasi` int(9) NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `Tanggal_Bayar_Pajak_Impor` datetime NOT NULL, " &
                " `Nomor_JV_Bayar_Pajak_Impor` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pembelian_Invoice` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pembelian_PO
            QueryPembuatanTabel =
                " CREATE TABLE `tbl_Pembelian_PO` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_PO` bigint(27) NOT NULL, " &
                " `Nomor_PO` varchar(99) NOT NULL, " &
                " `Tanggal_PO` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(17) NOT NULL, " &
                " `Term_Of_Payment` int(9) NOT NULL, " &
                " `Keterangan_ToP` varchar(99) NOT NULL, " &
                " `Jumlah_Hari_Jangka_Waktu_Penyelesaian` int(9) NOT NULL, " &
                " `Tanggal_Jangka_Waktu_Penyelesaian` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Pembuat_PO` varchar(99) NOT NULL, " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(99) NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Jenis_Jasa` varchar(63) NOT NULL, " &
                " `Attention` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(17) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` bigint(27) NOT NULL, " &
                " `Harga_Satuan_Asing` decimal(21,2) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` bigint(27) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` bigint(27) NOT NULL, " &
                " `Diskon` bigint(27) NOT NULL, " &
                " `DPP_Barang` bigint(27) NOT NULL, " &
                " `DPP_Jasa` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " & 'Jangan dikurangi...!!!
                " `Perlakuan_PPN` varchar(17) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " & 'Jangan dikurangi...!
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Biaya_Transportasi` bigint(27) NOT NULL, " &
                " `Total_Tagihan` bigint(27) NOT NULL, " &
                " `Metode_Pembayaran` varchar(33) NOT NULL, " &
                " `Basis_Perhitungan_Termin` varchar(33) NOT NULL, " &
                " `Uang_Muka` decimal(27,2) NOT NULL, " &
                " `Termin_1` decimal(27,2) NOT NULL, " &
                " `Termin_2` decimal(27,2) NOT NULL, " &
                " `Pelunasan` decimal(27,2) NOT NULL, " &
                " `Jumlah_Termin` int(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL," &
                " `Kontrol` varchar(21) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pembelian_PO` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pembelian_Retur
            QueryPembuatanTabel =
                " CREATE TABLE `tbl_Pembelian_Retur` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_Retur` bigint(27) NOT NULL, " &
                " `Nomor_Retur` varchar(99) NOT NULL, " &
                " `Tanggal_Retur` datetime NOT NULL, " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(33) NOT NULL, " &
                " `Nomor_Invoice_Produk` varchar(45) NOT NULL, " &
                " `Tanggal_Invoice_Produk` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `COA_Produk` varchar(17) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` bigint(27) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` bigint(27) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` bigint(27) NOT NULL, " &
                " `Diskon` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(17) NOT NULL, " &
                " `Tarif_PPN` decimal(5,2) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Total_Retur` bigint(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pembelian_Retur` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pembelian_SJ
            QueryPembuatanTabel = " CREATE TABLE `tbl_Pembelian_SJ` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_SJ` bigint(27) NOT NULL, " &
                " `Nomor_SJ` varchar(99) NOT NULL, " &
                " `Tanggal_SJ` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(27) NOT NULL, " &
                " `Nama_Penerima` varchar(63) NOT NULL, " &
                " `Tanggal_Diterima` datetime NOT NULL, " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_PO_Produk` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL," &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Keterangan_Produk` varchar(99) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pembelian_SJ` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Pemindahbukuan
            QueryPembuatanTabel = " CREATE TABLE `tbl_Pemindahbukuan` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPB` varchar(33) NOT NULL, " &
                " `Tanggal_BPPB` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `COA_Kredit` varchar(9) NOT NULL, " &
                " `Kode_Mata_Uang_Kredit` varchar(9) NOT NULL, " &
                " `Kurs_BI_Kredit` decimal(21,2) NOT NULL, " &
                " `Kurs_Bank_Kredit` decimal(21,2) NOT NULL, " &
                " `Jumlah_Kredit` decimal(21,2) NOT NULL, " &
                " `COA_Debet` varchar(9) NOT NULL, " &
                " `Kode_Mata_Uang_Debet` varchar(9) NOT NULL, " &
                " `Kurs_BI_Debet` decimal(21,2) NOT NULL, " &
                " `Kurs_Bank_Debet` decimal(21,2) NOT NULL, " &
                " `Jumlah_Debet` decimal(21,2) NOT NULL, " &
                " `Penanggungjawab` varchar(99) NOT NULL, " &
                " `Uraian_Transaksi` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Pemindahbukuan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranPiutangAfiliasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranPiutangAfiliasi` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPA` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranPiutangAfiliasi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_JadwalAngsuranPiutangPihakKetiga
            QueryPembuatanTabel = " CREATE TABLE `tbl_JadwalAngsuranPiutangPihakKetiga` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPPK` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Angsuran_Ke` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Bayar` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Pokok` bigint(27) NOT NULL, " &
                " `Bagi_Hasil` bigint(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `Jumlah_PPh` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Jumlah_Dibayarkan` bigint(27) NOT NULL, " &
                " `Denda` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_JadwalAngsuranPiutangPihakKetiga` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

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
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangBpjsKesehatan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            NomorUrut = 0
            Dim Bulan = Nothing
            Do While NomorUrut < 12
                NomorUrut += 1
                Bulan = BulanTerbilang(NomorUrut)
                cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBpjsKesehatan VALUES ( " &
                                                    " '" & NomorUrut & "', " & 'Ini karena Nomor ID = Nomor Urut, maka mengambil value Nomor Urut saja. Sama saja.
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
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangBpjsKetenagakerjaan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            NomorUrut = 0
            Do While NomorUrut < 12
                NomorUrut += 1
                Bulan = BulanTerbilang(NomorUrut)
                cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBpjsKetenagakerjaan VALUES ( " &
                                                    " '" & NomorUrut & "', " & 'Ini karena Nomor ID = Nomor Urut, maka mengambil value Nomor Urut saja. Sama saja.
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
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangKoperasiKaryawan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            NomorUrut = 0
            Do While NomorUrut < 12
                NomorUrut += 1
                Bulan = BulanTerbilang(NomorUrut)
                cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangKoperasiKaryawan VALUES ( " &
                                                    " '" & NomorUrut & "', " & 'Ini karena Nomor ID = Nomor Urut, maka mengambil value Nomor Urut saja. Sama saja.
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
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangSerikat` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            NomorUrut = 0
            Do While NomorUrut < 12
                NomorUrut += 1
                Bulan = BulanTerbilang(NomorUrut)
                cmdPengisianTabel = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangSerikat VALUES ( " &
                                                    " '" & NomorUrut & "', " & 'Ini karena Nomor ID = Nomor Urut, maka mengambil value Nomor Urut saja. Sama saja.
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

            'Pembuatan Tabel : tbl_PengawasanGaji
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanGaji` ( " &
                " `Nomor_ID`                                        bigint(27)  NOT NULL, " &
                " `Bulan`                                           varchar(27) NOT NULL, " &
                " `Tanggal_Transaksi`                               datetime    NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Gaji_Bagian_Produksi`                            bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Produksi_2`                          bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Produksi_3`                          bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Produksi_4`                          bigint(27)  NOT NULL, " &
                " `THR_Bonus_Produksi`                              bigint(27)  NOT NULL, " &
                " `Tunjangan_PPh_21_Produksi`                       bigint(27)  NOT NULL, " &
                " `BPJS_TK_JKK_JKM_Produksi`                        bigint(27)  NOT NULL, " &
                " `BPJS_TK_JHT_IP_Produksi`                         bigint(27)  NOT NULL, " &
                " `BPJS_Kesehatan_Produksi`                         bigint(27)  NOT NULL, " &
                " `Asuransi_Karyawan_Produksi`                      bigint(27)  NOT NULL, " &
                " `Pesangon_Karyawan_Produksi`                      bigint(27)  NOT NULL, " &
                " `Jumlah_Gaji_Bagian_Produksi`                     bigint(27)  NOT NULL, " &
                " `BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan`        bigint(27)  NOT NULL, " &
                " `BPJS_Kesehatan_Produksi_Dibayar_Karyawan`        bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Administrasi`                        bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Administrasi_2`                      bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Administrasi_3`                      bigint(27)  NOT NULL, " &
                " `Gaji_Bagian_Administrasi_4`                      bigint(27)  NOT NULL, " &
                " `THR_Bonus_Administrasi`                          bigint(27)  NOT NULL, " &
                " `Tunjangan_PPh_21_Administrasi`                   bigint(27)  NOT NULL, " &
                " `BPJS_TK_JKK_JKM_Administrasi`                    bigint(27)  NOT NULL, " &
                " `BPJS_TK_JHT_IP_Administrasi`                     bigint(27)  NOT NULL, " &
                " `BPJS_Kesehatan_Administrasi`                     bigint(27)  NOT NULL, " &
                " `Asuransi_Karyawan_Administrasi`                  bigint(27)  NOT NULL, " &
                " `Pesangon_Karyawan_Administrasi`                  bigint(27)  NOT NULL, " &
                " `Jumlah_Gaji_Bagian_Administrasi`                 bigint(27)  NOT NULL, " &
                " `BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan`    bigint(27)  NOT NULL, " &
                " `BPJS_Kesehatan_Administrasi_Dibayar_Karyawan`    bigint(27)  NOT NULL, " &
                " `Jumlah_Gaji_Kotor`                               bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_BPJS_Kesehatan`                  bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_BPJS_Ketenagakerjaan`            bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_Koperasi`                        bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_PPh_Pasal_21_Rutin`              bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_PPh_Pasal_21_Pesangon`           bigint(27)  NOT NULL, " &
                " `Potongan_Hutang_Serikat`                         bigint(27)  NOT NULL, " &
                " `Potongan_Kasbon_Karyawan`                        bigint(27)  NOT NULL, " &
                " `Potongan_Lainnya`                                bigint(27)  NOT NULL, " &
                " `Jumlah_Potongan`                                 bigint(27)  NOT NULL, " &
                " `Jumlah_Gaji_Dibayarkan`                          bigint(27)  NOT NULL, " &
                " `PPh_Ditanggung_Rutin`                            bigint(27)  NOT NULL, " &
                " `PPh_Ditanggung_Pesangon`                         bigint(27)  NOT NULL, " &
                " `Nomor_JV`                                        bigint(27)  NOT NULL, " &
                " `Keterangan`                                      longtext    NOT NULL  " &
                " ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanGaji` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangBank
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangBank` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHB` varchar(33) NOT NULL, " &
                " `Kode_Kreditur` varchar(21) NOT NULL, " &
                " `Nama_Kreditur` varchar(21) NOT NULL, " &
                " `Tanggal_Persetujuan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Pencairan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `Biaya_Administrasi_Kontrak` bigint(27) NOT NULL, " &
                " `Biaya_Notaris` bigint(27) NOT NULL, " &
                " `Biaya_PPh` bigint(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV_Persetujuan` bigint(27) NOT NULL, " &
                " `Nomor_JV_Pencairan` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangBank` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangLeasing
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangLeasing` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHL` varchar(33) NOT NULL, " &
                " `Kode_Kreditur` varchar(21) NOT NULL, " &
                " `Nama_Kreditur` varchar(21) NOT NULL, " &
                " `Tanggal_Persetujuan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Pencairan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `Biaya_Administrasi_Kontrak` bigint(27) NOT NULL, " &
                " `Biaya_Notaris` bigint(27) NOT NULL, " &
                " `Biaya_PPh` bigint(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV_Persetujuan` bigint(27) NOT NULL, " &
                " `Nomor_JV_Pencairan` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangLeasing` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangAfiliasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangAfiliasi` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHA` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangAfiliasi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangDividen
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangDividen` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHD` varchar(33) NOT NULL, " &
                " `Tanggal_Akta_Notaris` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Akta_Notaris` varchar(99) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Jumlah_Dividen` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangDividen` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangPihakKetiga
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangPihakKetiga` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHPK` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangPihakKetiga` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangKaryawan
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangKaryawan` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHK` varchar(33) NOT NULL, " &
                " `Nomor_Kontrak` varchar(63) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangKaryawan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanHutangPemegangSaham
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanHutangPemegangSaham` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPHPS` varchar(33) NOT NULL, " &
                " `Nomor_Kontrak` varchar(63) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanHutangPemegangSaham` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPelaporanPajak
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPelaporanPajak` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Bulan` int(9) NOT NULL, " &
                " `Jenis_Pajak` varchar(27) NOT NULL, " &
                " `Tanggal_Lapor` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `N_P` varchar(9) NOT NULL, " &
                " `Jumlah_Lebih_Bayar` bigint(27) NOT NULL, " &
                " `Kompensasi_Ke_Bulan` varchar(33) NOT NULL, " &
                " `Kompensasi_Ke_Tahun` int(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPelaporanPajak` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPiutangAfiliasi
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPiutangAfiliasi` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPA` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPiutangAfiliasi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPiutangDividen
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPiutangDividen` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPD` varchar(33) NOT NULL, " &
                " `Tanggal_Akta_Notaris` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Akta_Notaris` varchar(99) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Jumlah_Dividen` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPiutangDividen` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPiutangPihakKetiga
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPiutangPihakKetiga` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPPK` varchar(33) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Kontrak` varchar(99) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL,  " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPiutangPihakKetiga` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPiutangKaryawan
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPiutangKaryawan` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPK` varchar(33) NOT NULL, " &
                " `Nomor_Kontrak` varchar(63) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPiutangKaryawan` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PengawasanPiutangPemegangSaham
            QueryPembuatanTabel = " CREATE TABLE `tbl_PengawasanPiutangPemegangSaham` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Nomor_BPPPS` varchar(33) NOT NULL, " &
                " `Nomor_Kontrak` varchar(63) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jumlah_Pinjaman` bigint(27) NOT NULL, " &
                " `Saldo_Awal` bigint(27) NOT NULL, " &
                " `COA_Kredit` varchar(17) NOT NULL, " &
                " `Biaya_Administrasi_Bank` bigint(27) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Pembebanan` varchar(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_PengawasanPiutangPemegangSaham` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Penjualan_BAST
            QueryPembuatanTabel = " CREATE TABLE `tbl_Penjualan_BAST` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_BAST` bigint(27) NOT NULL, " &
                " `Nomor_BAST` varchar(99) NOT NULL, " &
                " `Tanggal_BAST` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jenis_Produk_Induk` varchar(27) NOT NULL, " &
                " `Yang_Menyerahkan` varchar(63) NOT NULL, " &
                " `Yang_Menerima` varchar(63) NOT NULL, " &
                " `Tanggal_Diterima` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_PO_Produk` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Keterangan_Produk` varchar(99) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Penjualan_BAST` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Penjualan_Invoice
            QueryPembuatanTabel = " CREATE TABLE `tbl_Penjualan_Invoice` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_Invoice` bigint(27) NOT NULL, " &
                " `Nomor_Invoice` longtext NOT NULL, " &
                " `Jenis_Invoice` varchar(27) NOT NULL, " &
                " `Metode_Pembayaran` varchar(33) NOT NULL, " &
                " `Basis_Perhitungan_Termin` varchar(33) NOT NULL, " &
                " `Nomor_Penjualan` varchar(33) NOT NULL, " &
                " `Referensi` varchar(99) NOT NULL, " &
                " `N_P` varchar(9) NOT NULL, " &
                " `Tanggal_Invoice` datetime NOT NULL, " &
                " `Tanggal_Pembetulan` datetime NOT NULL, " &
                " `Tanggal_Lapor` datetime NOT NULL, " &
                " `Jumlah_Hari_Jatuh_Tempo` int(9) NOT NULL, " &
                " `Tanggal_Jatuh_Tempo` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(17) NOT NULL, " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Kurs` decimal(21,2) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(27) NOT NULL, " &
                " `Nomor_SJ_BAST_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_SJ_BAST_Produk` datetime NOT NULL, " &
                " `Tanggal_Diterima_SJ_BAST_Produk` datetime NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` decimal(21,2) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` decimal(21,2) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` decimal(21,2) NOT NULL, " &
                " `Diskon` decimal(21,2) NOT NULL, " &
                " `Tahap_Termin` varchar(33) NOT NULL, " &
                " `Termin` decimal(27,2) NOT NULL, " &
                " `DPP_Barang` bigint(27) NOT NULL, " &
                " `DPP_Jasa` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Nomor_Faktur_Pajak` varchar(99) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `Tarif_PPN` decimal(5,2) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Total_Tagihan_Kotor` decimal(21,2) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Total_Tagihan` decimal(21,2) NOT NULL, " &
                " `Jumlah_Piutang_Usaha` decimal(21,2) NOT NULL, " &
                " `Jenis_Penjualan` varchar(27) NOT NULL, " &
                " `COA_Debet` varchar(17) NOT NULL, " &
                " `Sarana_Pembayaran` varchar(27) NOT NULL, " &
                " `Biaya_Administrasi_Bank` decimal(21,2) NOT NULL, " &
                " `Ditanggung_Oleh` varchar(27) NOT NULL, " &
                " `Biaya_Transportasi` decimal(21,2) NOT NULL, " &
                " `Insurance` decimal(21,2) NOT NULL, " &
                " `Freight` decimal(21,2) NOT NULL, " &
                " `Retur_DPP` decimal(21,2) NOT NULL, " &
                " `Retur_PPN` decimal(21,2) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Asset` int(3) NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `Nomor_Bukti_Potong` varchar(99) NOT NULL, " &
                " `Tanggal_Bukti_Potong` datetime NOT NULL, " &
                " `Keterangan_Bukti_Potong` longtext NOT NULL, " &
                " `Nomor_JV_Bukti_Potong` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Penjualan_Invoice` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Penjualan_PO
            QueryPembuatanTabel = " CREATE TABLE `tbl_Penjualan_PO` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_PO` bigint(27) NOT NULL, " &
                " `Nomor_PO` varchar(99) NOT NULL, " &
                " `Tanggal_PO` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(17) NOT NULL, " &
                " `Term_Of_Payment` int(9) NOT NULL, " &
                " `Keterangan_ToP` varchar(99) NOT NULL, " &
                " `Jumlah_Hari_Jangka_Waktu_Penyelesaian` int(9) NOT NULL, " &
                " `Tanggal_Jangka_Waktu_Penyelesaian` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Kode_Mata_Uang` varchar(9) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(17) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` bigint(27) NOT NULL, " &
                " `Harga_Satuan_Asing` decimal(21,2) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` bigint(27) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` bigint(27) NOT NULL, " &
                " `Diskon` bigint(27) NOT NULL, " &
                " `DPP_Barang` bigint(27) NOT NULL, " &
                " `DPP_Jasa` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(17) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Jenis_PPh` varchar(27) NOT NULL, " &
                " `Tarif_PPh` decimal(5,2) NOT NULL, " &
                " `PPh_Terutang` bigint(27) NOT NULL, " &
                " `PPh_Ditanggung` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Biaya_Transportasi` bigint(27) NOT NULL, " &
                " `Total_Tagihan` bigint(27) NOT NULL, " &
                " `Metode_Pembayaran` varchar(33) NOT NULL, " &
                " `Basis_Perhitungan_Termin` varchar(33) NOT NULL, " &
                " `Uang_Muka` decimal(27,2) NOT NULL, " &
                " `Termin_1` decimal(27,2) NOT NULL, " &
                " `Termin_2` decimal(27,2) NOT NULL, " &
                " `Pelunasan` decimal(27,2) NOT NULL, " &
                " `Jumlah_Termin` int(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL," &
                " `Kontrol` varchar(21) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Penjualan_PO` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Penjualan_Retur
            QueryPembuatanTabel = " CREATE TABLE `tbl_Penjualan_Retur` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_Retur` bigint(27) NOT NULL, " &
                " `Nomor_Retur` varchar(99) NOT NULL, " &
                " `Tanggal_Retur` datetime NOT NULL, " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Jenis_Produk_Per_Item` varchar(33) NOT NULL, " &
                " `Nomor_Invoice_Produk` varchar(45) NOT NULL, " &
                " `Tanggal_Invoice_Produk` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL, " &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Harga_Satuan` bigint(27) NOT NULL, " &
                " `Diskon_Per_Item` decimal(5,2) NOT NULL, " &
                " `Total_Harga_Per_Item` bigint(27) NOT NULL, " &
                " `Jumlah_Harga_Keseluruhan` bigint(27) NOT NULL, " &
                " `Diskon` bigint(27) NOT NULL, " &
                " `Dasar_Pengenaan_Pajak` bigint(27) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` bigint(27) NOT NULL, " &
                " `Tarif_PPN` decimal(5,2) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `Total_Retur` bigint(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Penjualan_Retur` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Penjualan_SJ
            QueryPembuatanTabel = " CREATE TABLE `tbl_Penjualan_SJ` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Angka_SJ` bigint(27) NOT NULL, " &
                " `Nomor_SJ` varchar(99) NOT NULL, " &
                " `Tanggal_SJ` datetime NOT NULL, " &
                " `Jenis_Produk_Induk` varchar(27) NOT NULL, " &
                " `Plat_Nomor` varchar(63) NOT NULL, " &
                " `Nama_Supir` varchar(63) NOT NULL, " &
                " `Nama_Pengirim` varchar(63) NOT NULL, " &
                " `Nama_Penerima` varchar(63) NOT NULL, " &
                " `Tanggal_Diterima` datetime NOT NULL, " &
                " `Kode_Customer` varchar(21) NOT NULL, " &
                " `Nama_Customer` varchar(99) NOT NULL, " &
                " `Nomor_Urut_Produk` int(9) NOT NULL, " &
                " `Nomor_PO_Produk` varchar(99) NOT NULL, " &
                " `Tanggal_PO_Produk` datetime NOT NULL, " &
                " `Kode_Project_Produk` varchar(99) NOT NULL, " &
                " `Nama_Produk` varchar(99) NOT NULL, " &
                " `Deskripsi_Produk` varchar(99) NOT NULL, " &
                " `Jumlah_Produk` int(9) NOT NULL," &
                " `Satuan_Produk` varchar(33) NOT NULL, " &
                " `Keterangan_Produk` varchar(99) NOT NULL, " &
                " `Jenis_PPN` varchar(27) NOT NULL, " &
                " `Perlakuan_PPN` varchar(27) NOT NULL, " &
                " `Catatan` longtext NOT NULL, " &
                " `User` varchar(63) NOT NULL, " &
                " `Koreksi` varchar(9) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Penjualan_SJ` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_PP_Temporary
            QueryPembuatanTabel = " CREATE TABLE `tbl_PP_Temporary` (" &
                " `ID` varchar(33) NOT NULL, " &
                " `Nomor_BPHU` varchar(33) NOT NULL, " &
                " `Nomor_Pembelian` varchar(33) NOT NULL, " &
                " `Kode_Supplier` varchar(21) NOT NULL, " &
                " `Nama_Supplier` varchar(63) NOT NULL, " &
                " `Sisa_Hutang` bigint(27) NOT NULL, " &
                " `Tanggal_Invoice` varchar(25) NOT NULL, " &
                " `Nomor_Invoice` varchar(25) NOT NULL, " &
                " `Harga` bigint(27) NOT NULL, " &
                " `PPN` bigint(27) NOT NULL, " &
                " `PPh_Dipotong` bigint(27) NOT NULL, " &
                " `Due_Date` varchar(25) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Pembuatan Tabel : tbl_SaldoAwalCOA
            QueryPembuatanTabel = " CREATE TABLE `tbl_SaldoAwalCOA` (" &
                " `COA` varchar(15) NOT NULL, " &
                " `Saldo_Awal` decimal(21,2) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_SaldoAwalCOA` ADD PRIMARY KEY (`COA`), ADD UNIQUE KEY (`COA`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_HutangPajak
            QueryPembuatanTabel = " CREATE TABLE `tbl_HutangPajak` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Tanggal_Transaksi` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Tanggal_Invoice` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nomor_Invoice` varchar(99) NOT NULL, " &
                " `Nomor_Faktur_Pajak` varchar(99) NOT NULL, " &
                " `Nama_Jasa` varchar(99) NOT NULL, " &
                " `Kode_Lawan_Transaksi` varchar(21) NOT NULL, " &
                " `Nama_Lawan_Transaksi` varchar(63) NOT NULL, " &
                " `NPWP` varchar(33) NOT NULL, " &
                " `DPP` bigint(27) NOT NULL, " &
                " `Jenis_Pajak` varchar(27) NOT NULL, " &
                " `Kode_Setoran` varchar(27) NOT NULL, " &
                " `Jumlah_Hutang` bigint(27) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Nomor_JV` bigint(27) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_HutangPajak` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_kursakhirbulan
            QueryPembuatanTabel = " CREATE TABLE `tbl_kursakhirbulan` (" &
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
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = "  ALTER TABLE `tbl_kursakhirbulan` ADD PRIMARY KEY (`Kode_Mata_Uang`), ADD UNIQUE KEY (`Kode_Mata_Uang`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            QueryAlterTable = "  INSERT INTO tbl_kursakhirbulan ( Kode_Mata_Uang ) VALUES " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_USD & "' ), " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_AUD & "' ), " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_JPY & "' ), " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_CNY & "' ), " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_EUR & "' ), " & vbCrLf
            QueryAlterTable &= " ( '" & KodeMataUang_SGD & "' ); " & vbCrLf
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_StockOpname
            QueryPembuatanTabel = " CREATE TABLE `tbl_StockOpname` (" &
                " `Nomor_ID` bigint(27) NOT NULL, " &
                " `Jenis_Stok` varchar(27) NOT NULL, " &
                " `Tanggal_Pengecekan` datetime NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Nama_Barang` varchar(99) NOT NULL, " &
                " `Jumlah_Barang` bigint(27) NOT NULL, " &
                " `Satuan` varchar(33) NOT NULL, " &
                " `Harga_Satuan` bigint(27) NOT NULL, " &
                " `Jumlah_Harga` bigint(27) NOT NULL, " &
                " `Asal` varchar(17) NOT NULL, " &
                " `Lokasi` varchar(99) NOT NULL, " &
                " `Keterangan` longtext NOT NULL, " &
                " `Jenis_Data` varchar(17) NOT NULL, " &
                " `User` varchar(63) NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_StockOpname` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            'Pembuatan Tabel : tbl_Transaksi
            QueryPembuatanTabel = " CREATE TABLE `tbl_Transaksi` (" &
                " `Nomor_ID`                    bigint(27)      NOT NULL, " &
                " `Nomor_JV`                    bigint(27)      NOT NULL, " &
                " `COA`                         varchar(17)     NOT NULL, " &
                " `Nama_Akun`                   varchar(99)     NOT NULL, " &
                " `Tanggal_Transaksi`           datetime        NOT NULL DEFAULT '1900-01-01 00:00:00', " &
                " `Jenis_Jurnal`                varchar(63)     NOT NULL, " &
                " `Kode_Dokumen`                varchar(63)     NOT NULL, " &
                " `Nomor_PO`                    varchar(63)     NOT NULL, " &
                " `Kode_Project`                varchar(63)     NOT NULL, " &
                " `Nama_Produk`                 varchar(99)     NOT NULL, " &
                " `Referensi`                   longtext        NOT NULL, " &
                " `Bundelan`                    longtext        NOT NULL, " &
                " `Tanggal_Invoice`             longtext        NOT NULL, " &
                " `Nomor_Invoice`               longtext        NOT NULL, " &
                " `Nomor_Faktur_Pajak`          longtext        NOT NULL, " &
                " `Kode_Lawan_Transaksi`        varchar(21)     NOT NULL, " &
                " `Nama_Lawan_Transaksi`        varchar(63)     NOT NULL, " &
                " `Kode_Mata_Uang`              varchar(9)      NOT NULL, " &
                " `Kurs`                        decimal(21,2)   NOT NULL, " &
                " `D_K`                         varchar(9)      NOT NULL, " &
                " `Jumlah_Debet`                decimal(21,2)   NOT NULL, " &
                " `Jumlah_Kredit`               decimal(21,2)   NOT NULL, " &
                " `Uraian_Transaksi`            varchar(255)    NOT NULL, " &
                " `Direct`                      int(3)          NOT NULL, " &
                " `Status_Approve`              int(3)          NOT NULL, " &
                " `Valid`                       varchar(9)      NOT NULL, " &
                " `Username_Entry`              varchar(99)     NOT NULL, " &
                " `Nama_User_Entry`             varchar(99)     NOT NULL, " &
                " `Username_Approve`            varchar(99)     NOT NULL, " &
                " `Nama_User_Approve`           varchar(99)     NOT NULL " &
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4; "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPembuatanTabel, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            QueryAlterTable = " ALTER TABLE `tbl_Transaksi` ADD PRIMARY KEY (`Nomor_ID`), ADD UNIQUE KEY (`Nomor_ID`); "
            cmd = New OdbcCommand(QueryAlterTable, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            HasilPembuatanDatabaseTransaksi = True

        Catch ex As Exception

            HasilPembuatanDatabaseTransaksi = False
            PesanUntukProgrammer("Ada kesalahan pada pembuatan kerangka Database Transaksi")

        End Try

    End Sub

End Module
