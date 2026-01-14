Imports bcomm
Imports System.Data.Odbc
Imports System.Windows.Threading


Module wpfMdl_TutupBuku

    Sub TransferData_SisaHutangUsaha_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim AngkaInvoice = 0
        Dim NomorPembelian
        Dim NomorPembelian_Sebelumnya = Kosongan
        Dim KodeSupplier = Kosongan
        Dim NamaSupplier = Kosongan
        Dim NomorBPHU
        Dim KodeSetoran = Kosongan
        Dim NPPHU = Kosongan
        Dim TanggalBayar_HutangUsaha = TanggalBukuDitutup
        Dim JumlahTagihan
        Dim Jumlah_HutangUsaha
        Dim JumlahBayar_HutangUsaha
        Dim JumlahPPhTerutang_HutangUsaha As Int64
        Dim JumlahPPhDitanggung_HutangUsaha As Int64
        Dim JumlahPPhDipotong_HutangUsaha As Int64
        Dim JumlahBayar_Tagihan As Int64
        Dim SisaHutang_HutangUsaha As Int64
        Dim COADebet = Kosongan
        Dim COAKredit = Kosongan
        Dim NomorJV = 0

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Usaha Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorPembelian = dr.Item("Nomor_Pembelian")
            NomorBPHU = KonversiNomorPembelianKeNomorBPHU(NomorPembelian)
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            JumlahTagihan = dr.Item("Total_Tagihan")
            Jumlah_HutangUsaha = dr.Item("Jumlah_Hutang_Usaha")

            JumlahBayar_HutangUsaha = 0
            JumlahPPhTerutang_HutangUsaha = 0
            JumlahPPhDitanggung_HutangUsaha = 0
            JumlahPPhDipotong_HutangUsaha = 0
            JumlahBayar_Tagihan = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHU & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_Tagihan += drBAYAR.Item("Jumlah_Bayar")
                JumlahPPhTerutang_HutangUsaha += drBAYAR.Item("PPh_Terutang")
                JumlahPPhDitanggung_HutangUsaha += drBAYAR.Item("PPh_Ditanggung")
                JumlahPPhDipotong_HutangUsaha += drBAYAR.Item("PPh_Dipotong")
                JumlahBayar_HutangUsaha += (JumlahBayar_Tagihan + JumlahPPhDipotong_HutangUsaha)
            Loop
            SisaHutang_HutangUsaha = Jumlah_HutangUsaha - JumlahBayar_HutangUsaha

            If SisaHutang_HutangUsaha > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Invoice Pembelian
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_Pembelian_Invoice VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                dr.Item("Angka_Invoice") & "', '" &
                                dr.Item("Nomor_Invoice") & "', '" &
                                dr.Item("Metode_Pembayaran") & "', '" &
                                dr.Item("Jenis_Invoice") & "', '" &
                                dr.Item("Nomor_Pembelian") & "', '" &
                                dr.Item("Referensi") & "', '" &
                                dr.Item("N_P") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Invoice")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Diterima_Invoice")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Pembetulan")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Lapor")) & "', '" &
                                dr.Item("Jumlah_Hari_Jatuh_Tempo") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Jenis_Produk_Induk") & "', '" &
                                dr.Item("Kode_Supplier") & "', '" &
                                dr.Item("Nama_Supplier") & "', '" &
                                dr.Item("Jenis_Jasa") & "', '" &
                                dr.Item("Nomor_Urut_Produk") & "', '" &
                                dr.Item("Jenis_Produk_Per_Item") & "', '" &
                                dr.Item("Nomor_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_Diterima_SJ_BAST_Produk") & "', '" &
                                dr.Item("Nomor_PO_Produk") & "', '" &
                                dr.Item("Kode_Project_Produk") & "', '" &
                                dr.Item("COA_Produk") & "', '" &
                                dr.Item("Nama_Produk") & "', '" &
                                dr.Item("Deskripsi_Produk") & "', '" &
                                dr.Item("Jumlah_Produk") & "', '" &
                                dr.Item("Satuan_Produk") & "', '" &
                                dr.Item("Harga_Satuan") & "', '" &
                                dr.Item("Diskon_Per_Item") & "', '" &
                                dr.Item("Total_Harga_Per_Item") & "', '" &
                                dr.Item("Jumlah_Harga_Keseluruhan") & "', '" &
                                dr.Item("Diskon") & "', '" &
                                dr.Item("Tahap_Termin") & "', '" &
                                dr.Item("Termin") & "', '" &
                                dr.Item("DPP_Barang") & "', '" &
                                dr.Item("DPP_Jasa") & "', '" &
                                dr.Item("Dasar_Pengenaan_Pajak") & "', '" &
                                dr.Item("Nomor_Faktur_Pajak") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Faktur_Pajak")) & "', '" &
                                dr.Item("Jenis_PPN") & "', '" &
                                dr.Item("Perlakuan_PPN") & "', '" &
                                dr.Item("PPN_Dikreditkan") & "', '" &
                                dr.Item("Pilihan_PPN") & "', '" &
                                dr.Item("Tarif_PPN") & "', '" &
                                dr.Item("PPN") & "', '" &
                                dr.Item("Total_Tagihan_Kotor") & "', '" &
                                dr.Item("Jenis_PPh") & "', '" &
                                dr.Item("Kode_Setoran") & "', '" &
                                dr.Item("Tarif_PPh") & "', '" &
                                dr.Item("PPh_Terutang") & "', '" &
                                dr.Item("PPh_Ditanggung") & "', '" &
                                dr.Item("PPh_Dipotong") & "', '" &
                                dr.Item("Total_Tagihan") & "', '" &
                                dr.Item("Jumlah_Hutang_Usaha") & "', '" &
                                dr.Item("Jenis_Pembelian") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Sarana_Pembayaran") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Biaya_Transportasi") & "', '" &
                                dr.Item("Biaya_Materai") & "', '" &
                                dr.Item("Retur_DPP") & "', '" &
                                dr.Item("Retur_PPN") & "', '" &
                                dr.Item("Catatan") & "', '" &
                                dr.Item("Kode_Divisi_Asset") & "', '" &
                                dr.Item("Kelompok_Asset") & "', '" &
                                dr.Item("COA_Biaya") & "', '" &
                                dr.Item("Masa_Amortisasi") & "', '" &
                                0 & "', '" &
                                dr.Item("Nomor_Bukti_Potong") & "', '" &
                                dr.Item("Tanggal_Bukti_Potong") & "', '" &
                                dr.Item("Keterangan_Bukti_Potong") & "', '" &
                                0 & "', '" &
                                dr.Item("User") & "', '" &
                                dr.Item("Koreksi") & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                Dim Peruntukan = Kosongan
                COADebet = dr.Item("COA_Kredit")
                If COADebet = KodeTautanCOA_HutangUsaha_Afiliasi Then Peruntukan = Peruntukan_PembayaranHutangUsaha_Afiliasi
                If COADebet = KodeTautanCOA_HutangUsaha_NonAfiliasi Then Peruntukan = Peruntukan_PembayaranHutangUsaha_NonAfiliasi

                'Simpan value Jumlah_Bayar ke Data Bank-Cash Out
                If StatusSuntingDatabase = True And NomorPembelian <> NomorPembelian_Sebelumnya And JumlahBayar_HutangUsaha > 0 Then
                    NomorIdKK += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & "KK-" & TahunBukuDitutup & "-" & NomorIdKK & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kategori_PembayaranHutang & "', " &
                                          " '" & Peruntukan & "', " &
                                          " '" & KodeSupplier & "', " &
                                          " '" & NamaSupplier & "', " &
                                          " '" & NomorBPHU & "', " &
                                          " '" & dr.Item("Nomor_Invoice") & "', " &
                                          " '" & TanggalFormatSimpan(dr.Item("Tanggal_Invoice")) & "', " &
                                          " '" & dr.Item("Catatan") & "', " &
                                          " '" & JumlahTagihan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & JumlahBayar_Tagihan & "', " &
                                          " '" & JumlahBayar_Tagihan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & COADebet & "', " &
                                          " '" & COAKredit & "', " &
                                          " '" & BiayaAdministrasiBank & "', " &
                                          " '" & DitanggungOleh & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh")) & "', " &
                                          " '" & dr.Item("Kode_Setoran") & "', " &
                                          " '" & JumlahPPhTerutang_HutangUsaha & "', " &
                                          " '" & JumlahPPhDitanggung_HutangUsaha & "', " &
                                          " '" & JumlahPPhDipotong_HutangUsaha & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & NomorJV & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & TanggalKosongSimpan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If
            End If
            NomorPembelian_Sebelumnya = NomorPembelian
        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Usaha Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Usaha Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaPiutangUsaha_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim AngkaInvoice = 0
        Dim NomorPenjualan
        Dim NomorPenjualan_Sebelumnya = Kosongan
        Dim KodeCustomer = Kosongan
        Dim NamaCustomer = Kosongan
        Dim NomorBPPU
        Dim KodeSetoran = Kosongan
        Dim NPPHU = Kosongan
        Dim TanggalBayar_PiutangUsaha = TanggalBukuDitutup
        Dim JumlahTagihan
        Dim Jumlah_PiutangUsaha
        Dim JumlahBayar_PiutangUsaha
        Dim JumlahPPhTerutang_PiutangUsaha As Int64
        Dim JumlahPPhDitanggung_PiutangUsaha As Int64
        Dim JumlahPPhDipotong_PiutangUsaha As Int64
        Dim JumlahBayar_Tagihan As Int64
        Dim SisaPiutang_PiutangUsaha As Int64
        Dim COADebet = Kosongan
        Dim COAKredit = Kosongan
        Dim NomorJV = 0

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Piutang Usaha Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            NomorBPPU = KonversiNomorPenjualanKeNomorBPPU(NomorPenjualan)
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            JumlahTagihan = dr.Item("Total_Tagihan")
            Jumlah_PiutangUsaha = dr.Item("Jumlah_Piutang_Usaha")

            JumlahBayar_PiutangUsaha = 0
            JumlahPPhTerutang_PiutangUsaha = 0
            JumlahPPhDitanggung_PiutangUsaha = 0
            JumlahPPhDipotong_PiutangUsaha = 0
            JumlahBayar_Tagihan = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPPU & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_Tagihan += drBAYAR.Item("Jumlah_Bayar")
                JumlahPPhTerutang_PiutangUsaha += drBAYAR.Item("PPh_Terutang")
                JumlahPPhDitanggung_PiutangUsaha += drBAYAR.Item("PPh_Ditanggung")
                JumlahPPhDipotong_PiutangUsaha += drBAYAR.Item("PPh_Dipotong")
                JumlahBayar_PiutangUsaha += (JumlahBayar_Tagihan + JumlahPPhDipotong_PiutangUsaha)
            Loop
            SisaPiutang_PiutangUsaha = Jumlah_PiutangUsaha - JumlahBayar_PiutangUsaha

            If SisaPiutang_PiutangUsaha > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Invoice Penjualan
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_Penjualan_Invoice VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                dr.Item("Angka_Invoice") & "', '" &
                                dr.Item("Nomor_Invoice") & "', '" &
                                dr.Item("Jenis_Invoice") & "', '" &
                                dr.Item("Metode_Pembayaran") & "', '" &
                                dr.Item("Nomor_Penjualan") & "', '" &
                                dr.Item("Referensi") & "', '" &
                                dr.Item("N_P") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Invoice")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Pembetulan")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Lapor")) & "', '" &
                                dr.Item("Jumlah_Hari_Jatuh_Tempo") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Jenis_Produk_Induk") & "', '" &
                                dr.Item("Kode_Customer") & "', '" &
                                dr.Item("Nama_Customer") & "', '" &
                                dr.Item("Kode_Mata_Uang") & "', '" &
                                dr.Item("Kurs") & "', '" &
                                dr.Item("Nomor_Urut_Produk") & "', '" &
                                dr.Item("Jenis_Produk_Per_Item") & "', '" &
                                dr.Item("Nomor_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_Diterima_SJ_BAST_Produk") & "', '" &
                                dr.Item("Nomor_PO_Produk") & "', '" &
                                dr.Item("Kode_Project_Produk") & "', '" &
                                dr.Item("Nama_Produk") & "', '" &
                                dr.Item("Deskripsi_Produk") & "', '" &
                                dr.Item("Jumlah_Produk") & "', '" &
                                dr.Item("Satuan_Produk") & "', '" &
                                dr.Item("Harga_Satuan") & "', '" &
                                dr.Item("Harga_Satuan_Asing") & "', '" &
                                dr.Item("Diskon_Per_Item") & "', '" &
                                dr.Item("Total_Harga_Per_Item") & "', '" &
                                dr.Item("Jumlah_Harga_Keseluruhan") & "', '" &
                                dr.Item("Jumlah_Harga_Keseluruhan_Asing") & "', '" &
                                dr.Item("Diskon") & "', '" &
                                dr.Item("Tahap_Termin") & "', '" &
                                dr.Item("Termin") & "', '" &
                                dr.Item("DPP_Barang") & "', '" &
                                dr.Item("DPP_Jasa") & "', '" &
                                dr.Item("Dasar_Pengenaan_Pajak") & "', '" &
                                dr.Item("Nomor_Faktur_Pajak") & "', '" &
                                dr.Item("Jenis_PPN") & "', '" &
                                dr.Item("Perlakuan_PPN") & "', '" &
                                dr.Item("Tarif_PPN") & "', '" &
                                dr.Item("PPN") & "', '" &
                                dr.Item("Total_Tagihan_Kotor") & "', '" &
                                dr.Item("Jenis_PPh") & "', '" &
                                dr.Item("Kode_Setoran") & "', '" &
                                dr.Item("Tarif_PPh") & "', '" &
                                dr.Item("PPh_Terutang") & "', '" &
                                dr.Item("PPh_Ditanggung") & "', '" &
                                dr.Item("PPh_Dipotong") & "', '" &
                                dr.Item("Total_Tagihan") & "', '" &
                                dr.Item("Total_Tagihan_Asing") & "', '" &
                                dr.Item("Jumlah_Piutang_Usaha") & "', '" &
                                dr.Item("Jenis_Penjualan") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Sarana_Pembayaran") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Biaya_Transportasi") & "', '" &
                                dr.Item("Biaya_Transportasi_Asing") & "', '" &
                                dr.Item("Biaya_Asuransi_Penjualan_Asing") & "', '" &
                                dr.Item("Retur_DPP") & "', '" &
                                dr.Item("Retur_PPN") & "', '" &
                                dr.Item("Catatan") & "', '" &
                                dr.Item("Asset") & "', '" &
                                NomorJV & "', '" &
                                Kosongan & "', '" &
                                TanggalKosongSimpan & "', '" &
                                Kosongan & "', '" &
                                0 & "', '" &
                                dr.Item("User") & "', '" &
                                dr.Item("Koreksi") & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                Dim Peruntukan = Kosongan
                COAKredit = dr.Item("COA_Debet")
                If COAKredit = KodeTautanCOA_PiutangUsaha_Afiliasi Then Peruntukan = Peruntukan_PencairanPiutangUsaha_Afiliasi
                If COAKredit = KodeTautanCOA_PiutangUsaha_NonAfiliasi Then Peruntukan = Peruntukan_PencairanPiutangUsaha_NonAfiliasi

                'Simpan value Jumlah_Bayar ke Data Bank-Cash In
                If StatusSuntingDatabase = True And NomorPenjualan <> NomorPenjualan_Sebelumnya And JumlahBayar_PiutangUsaha > 0 Then
                    NomorIdKM += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPenerimaan VALUES ( " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & "KM-" & TahunBukuDitutup & "-" & NomorIdKM & "', " &
                                              " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                              " '" & Kategori_PencairanPiutang & "', " &
                                              " '" & Peruntukan & "', " &
                                              " '" & KodeCustomer & "', " &
                                              " '" & NamaCustomer & "', " &
                                              " '" & NomorBPPU & "', " &
                                              " '" & dr.Item("Nomor_Faktur_Pajak") & "', " &
                                              " '" & dr.Item("Nomor_Invoice") & "', " &
                                              " '" & TanggalFormatSimpan(dr.Item("Tanggal_Invoice")) & "', " &
                                              " '" & dr.Item("Catatan") & "', " &
                                              " '" & JumlahTagihan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & JumlahBayar_Tagihan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & COADebet & "', " &
                                              " '" & COAKredit & "', " &
                                              " '" & BiayaAdministrasiBank & "', " &
                                              " '" & DitanggungOleh & "', " &
                                              " '" & KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh")) & "', " &
                                              " '" & dr.Item("Kode_Setoran") & "', " &
                                              " '" & JumlahPPhTerutang_PiutangUsaha & "', " &
                                              " '" & JumlahPPhDitanggung_PiutangUsaha & "', " &
                                              " '" & JumlahPPhDipotong_PiutangUsaha & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & NomorJV & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & TanggalKosongSimpan & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & UserAktif & "' " &
                                              " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If
            End If
            NomorPenjualan_Sebelumnya = NomorPenjualan
        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Usaha Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Usaha Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaHutangKaryawan_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHK
        Dim KodeLawanTransaksi
        Dim NamaLawanTransaksi
        Dim JumlahTagihan
        Dim JumlahBayar
        Dim SisaHutang
        Dim TanggalBayar = TanggalKosong

        Dim COADebet = KodeTautanCOA_HutangKaryawan
        Dim COAKredit = Kosongan

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangKaryawan ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS_ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" Select * FROM tbl_PengawasanHutangKaryawan ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Karywan Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHK = dr.Item("Nomor_BPHK")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            JumlahTagihan = dr.Item("Saldo_Awal")
            JumlahBayar = 0
            cmdBAYAR = New OdbcCommand(" Select * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHK & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaHutang = JumlahTagihan - JumlahBayar

            If SisaHutang > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Pengawasan Hutang Karyawan 
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangKaryawan VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHK & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan value Jumlah_Bayar ke Data Bank-Cash Out
                If StatusSuntingDatabase = True And JumlahBayar > 0 Then
                    NomorIdKK += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & "KK-" & TahunBukuDitutup & "-" & NomorIdKK & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kategori_PembayaranHutang & "', " &
                                          " '" & Peruntukan_PembayaranHutangKaryawan & "', " &
                                          " '" & KodeLawanTransaksi & "', " &
                                          " '" & NamaLawanTransaksi & "', " &
                                          " '" & NomorBPHK & "', " &
                                          " '" & dr.Item("Nomor_Kontrak") & "', " &
                                          " '" & TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', " &
                                          " '" & dr.Item("Keterangan") & "', " &
                                          " '" & JumlahTagihan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & JumlahBayar & "', " &
                                          " '" & JumlahBayar & "', " &
                                          " '" & 0 & "', " &
                                          " '" & COADebet & "', " &
                                          " '" & COAKredit & "', " &
                                          " '" & BiayaAdministrasiBank & "', " &
                                          " '" & DitanggungOleh & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & JenisPajak_Non & "', " &
                                          " '" & KodeSetoran_Non & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & TanggalKosongSimpan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Karyawan Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Karyawan Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaPiutangKaryawan_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPPK
        Dim KodeLawanTransaksi
        Dim NamaLawanTransaksi
        Dim JumlahTagihan
        Dim JumlahBayar
        Dim SisaPiutang
        Dim TanggalBayar = TanggalKosong

        Dim COADebet = Kosongan
        Dim COAKredit = KodeTautanCOA_PiutangKaryawan

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangKaryawan ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS_ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" Select * FROM tbl_PengawasanPiutangKaryawan ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Piutang Karywan Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPPK = dr.Item("Nomor_BPPK")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            JumlahTagihan = dr.Item("Saldo_Awal")
            JumlahBayar = 0
            cmdBAYAR = New OdbcCommand(" Select * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP     = '" & NomorBPPK & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaPiutang = JumlahTagihan - JumlahBayar

            If SisaPiutang > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Pengawasan Piutang Karyawan 
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanPiutangKaryawan VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPPK & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan value Jumlah_Bayar ke Data Bank-Cash Out
                If StatusSuntingDatabase = True And JumlahBayar > 0 Then
                    NomorIdKM += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPenerimaan VALUES ( " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & "KM-" & TahunBukuDitutup & "-" & NomorIdKM & "', " &
                                              " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                              " '" & Kategori_PencairanPiutang & "', " &
                                              " '" & Peruntukan_PencairanPiutangKaryawan & "', " &
                                              " '" & KodeLawanTransaksi & "', " &
                                              " '" & NamaLawanTransaksi & "', " &
                                              " '" & NomorBPPK & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & dr.Item("Nomor_Kontrak") & "', " &
                                              " '" & TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', " &
                                              " '" & dr.Item("Keterangan") & "', " &
                                              " '" & JumlahTagihan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & JumlahBayar & "', " &
                                              " '" & 0 & "', " &
                                              " '" & COADebet & "', " &
                                              " '" & COAKredit & "', " &
                                              " '" & BiayaAdministrasiBank & "', " &
                                              " '" & DitanggungOleh & "', " &
                                              " '" & JenisPajak_Non & "', " &
                                              " '" & KodeSetoran_Non & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & TanggalKosongSimpan & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & UserAktif & "' " &
                                              " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Karyawan Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Karyawan Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub



    Sub TransferData_SisaHutangPemegangSaham_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHPS
        Dim KodeLawanTransaksi
        Dim NamaLawanTransaksi
        Dim JumlahTagihan
        Dim JumlahBayar
        Dim SisaHutang
        Dim TanggalBayar = TanggalKosong

        Dim COADebet = KodeTautanCOA_HutangPemegangSaham
        Dim COAKredit = Kosongan

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangPemegangSaham ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS_ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" Select * FROM tbl_PengawasanHutangPemegangSaham ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Karywan Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHPS = dr.Item("Nomor_BPHPS")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            JumlahTagihan = dr.Item("Saldo_Awal")
            JumlahBayar = 0
            cmdBAYAR = New OdbcCommand(" Select * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHPS & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaHutang = JumlahTagihan - JumlahBayar

            If SisaHutang > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Pengawasan Hutang Pemegang Saham 
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangPemegangSaham VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHPS & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan value Jumlah_Bayar ke Data Bank-Cash Out
                If StatusSuntingDatabase = True And JumlahBayar > 0 Then
                    NomorIdKK += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & NomorIdKK & "', " &
                                          " '" & "KK-" & TahunBukuDitutup & "-" & NomorIdKK & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kategori_PembayaranHutang & "', " &
                                          " '" & Peruntukan_PembayaranHutangPemegangSaham & "', " &
                                          " '" & KodeLawanTransaksi & "', " &
                                          " '" & NamaLawanTransaksi & "', " &
                                          " '" & NomorBPHPS & "', " &
                                          " '" & dr.Item("Nomor_Kontrak") & "', " &
                                          " '" & TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', " &
                                          " '" & dr.Item("Keterangan") & "', " &
                                          " '" & JumlahTagihan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & JumlahBayar & "', " &
                                          " '" & JumlahBayar & "', " &
                                          " '" & 0 & "', " &
                                          " '" & COADebet & "', " &
                                          " '" & COAKredit & "', " &
                                          " '" & BiayaAdministrasiBank & "', " &
                                          " '" & DitanggungOleh & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & Status_Dibayar & "', " &
                                          " '" & JenisPajak_Non & "', " &
                                          " '" & KodeSetoran_Non & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & 0 & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & TanggalKosongSimpan & "', " &
                                          " '" & Kosongan & "', " &
                                          " '" & 0 & "', " &
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Pemegang Saham Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Pemegang Saham Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaPiutangPemegangSaham_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPPPS
        Dim KodeLawanTransaksi
        Dim NamaLawanTransaksi
        Dim JumlahTagihan
        Dim JumlahBayar
        Dim SisaPiutang
        Dim TanggalBayar = TanggalKosong

        Dim COADebet = Kosongan
        Dim COAKredit = KodeTautanCOA_PiutangPemegangSaham

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangPemegangSaham ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS_ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" Select * FROM tbl_PengawasanPiutangPemegangSaham ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Piutang Karywan Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPPPS = dr.Item("Nomor_BPPPS")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            JumlahTagihan = dr.Item("Saldo_Awal")
            JumlahBayar = 0
            cmdBAYAR = New OdbcCommand(" Select * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP     = '" & NomorBPPPS & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaPiutang = JumlahTagihan - JumlahBayar

            If SisaPiutang > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Pengawasan Piutang Pemegang Saham 
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanPiutangPemegangSaham VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPPPS & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan value Jumlah_Cair ke Data Bank-Cash In
                If StatusSuntingDatabase = True And JumlahBayar > 0 Then
                    NomorIdKM += 1
                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPenerimaan VALUES ( " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & NomorIdKM & "', " &
                                              " '" & "KM-" & TahunBukuDitutup & "-" & NomorIdKM & "', " &
                                              " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                              " '" & Kategori_PencairanPiutang & "', " &
                                              " '" & Peruntukan_PencairanPiutangPemegangSaham & "', " &
                                              " '" & KodeLawanTransaksi & "', " &
                                              " '" & NamaLawanTransaksi & "', " &
                                              " '" & NomorBPPPS & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & dr.Item("Nomor_Kontrak") & "', " &
                                              " '" & TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', " &
                                              " '" & dr.Item("Keterangan") & "', " &
                                              " '" & JumlahTagihan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & JumlahBayar & "', " &
                                              " '" & 0 & "', " &
                                              " '" & COADebet & "', " &
                                              " '" & COAKredit & "', " &
                                              " '" & BiayaAdministrasiBank & "', " &
                                              " '" & DitanggungOleh & "', " &
                                              " '" & JenisPajak_Non & "', " &
                                              " '" & KodeSetoran_Non & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & 0 & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & TanggalKosongSimpan & "', " &
                                              " '" & Kosongan & "', " &
                                              " '" & 0 & "', " &
                                              " '" & UserAktif & "' " &
                                              " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Pemegang Saham Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Pemegang Saham Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaHutangBank_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHB
        Dim JumlahTagihan_HutangBank
        Dim JumlahBayar_HutangBank
        Dim SisaHutang_HutangBank

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangBank ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangBank ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Bank Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHB = dr.Item("Nomor_BPHB")
            JumlahTagihan_HutangBank = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_HutangBank = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                       " WHERE Nomor_BPHB   = '" & NomorBPHB & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangBank += drBAYAR.Item("Pokok")
            Loop
            SisaHutang_HutangBank = JumlahTagihan_HutangBank - JumlahBayar_HutangBank

            If SisaHutang_HutangBank > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBank VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHB & "', '" &
                                dr.Item("Kode_Kreditur") & "', '" &
                                dr.Item("Nama_Kreditur") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Persetujuan")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Pencairan")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Biaya_Administrasi_Kontrak") & "', '" &
                                dr.Item("Biaya_Notaris") & "', '" &
                                dr.Item("Biaya_PPh") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                                 " WHERE Nomor_BPHB = '" & NomorBPHB & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranHutangBank VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPHB & "', " &
                                          " '" & drTELUSUR.Item("Kode_Kreditur") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Kredit") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Bank Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Bank Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub



    Sub TransferData_SisaHutangLeasing_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHL
        Dim JumlahTagihan_HutangLeasing
        Dim JumlahBayar_HutangLeasing
        Dim SisaHutang_HutangLeasing

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangLeasing ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangLeasing ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Leasing Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHL = dr.Item("Nomor_BPHL")
            JumlahTagihan_HutangLeasing = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_HutangLeasing = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangLeasing " &
                                       " WHERE Nomor_BPHL   = '" & NomorBPHL & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangLeasing += drBAYAR.Item("Pokok")
            Loop
            SisaHutang_HutangLeasing = JumlahTagihan_HutangLeasing - JumlahBayar_HutangLeasing

            If SisaHutang_HutangLeasing > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangLeasing VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHL & "', '" &
                                dr.Item("Kode_Kreditur") & "', '" &
                                dr.Item("Nama_Kreditur") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Persetujuan")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Pencairan")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("Biaya_Administrasi_Kontrak") & "', '" &
                                dr.Item("Biaya_Notaris") & "', '" &
                                dr.Item("Biaya_PPh") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangLeasing " &
                                                 " WHERE Nomor_BPHL = '" & NomorBPHL & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranHutangLeasing VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPHL & "', " &
                                          " '" & drTELUSUR.Item("Kode_Kreditur") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Kredit") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Leasing Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Leasing Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub



    Sub TransferData_SisaHutangPihakKetiga_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHPK
        Dim JumlahTagihan_HutangPihakKetiga
        Dim JumlahBayar_HutangPihakKetiga
        Dim SisaHutang_HutangPihakKetiga

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangPihakKetiga ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangPihakKetiga ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang PihakKetiga Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHPK = dr.Item("Nomor_BPHPK")
            JumlahTagihan_HutangPihakKetiga = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_HutangPihakKetiga = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangPihakKetiga " &
                                       " WHERE Nomor_BPHPK =  '" & NomorBPHPK & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangPihakKetiga += drBAYAR.Item("Pokok")
            Loop
            SisaHutang_HutangPihakKetiga = JumlahTagihan_HutangPihakKetiga - JumlahBayar_HutangPihakKetiga

            If SisaHutang_HutangPihakKetiga > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangPihakKetiga VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHPK & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangPihakKetiga " &
                                                 " WHERE Nomor_BPHPK = '" & NomorBPHPK & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranHutangPihakKetiga VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPHPK & "', " &
                                          " '" & drTELUSUR.Item("Kode_Lawan_Transaksi") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Kredit") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Pihak Ketiga Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Pihak Ketiga Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaPiutangPihakKetiga_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPPPK
        Dim JumlahTagihan_PiutangPihakKetiga
        Dim JumlahBayar_PiutangPihakKetiga
        Dim SisaPiutang_PiutangPihakKetiga

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangPihakKetiga ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangPihakKetiga ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Piutang PihakKetiga Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPPPK = dr.Item("Nomor_BPPPK")
            JumlahTagihan_PiutangPihakKetiga = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_PiutangPihakKetiga = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangPihakKetiga " &
                                       " WHERE Nomor_BPPPK =  '" & NomorBPPPK & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_PiutangPihakKetiga += drBAYAR.Item("Pokok")
            Loop
            SisaPiutang_PiutangPihakKetiga = JumlahTagihan_PiutangPihakKetiga - JumlahBayar_PiutangPihakKetiga

            If SisaPiutang_PiutangPihakKetiga > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Piutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanPiutangPihakKetiga VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPPPK & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangPihakKetiga " &
                                                 " WHERE Nomor_BPPPK = '" & NomorBPPPK & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranPiutangPihakKetiga VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPPPK & "', " &
                                          " '" & drTELUSUR.Item("Kode_Lawan_Transaksi") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Debet") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Pihak Ketiga Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Pihak Ketiga Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub



    Sub TransferData_SisaHutangAfiliasi_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHA
        Dim JumlahTagihan_HutangAfiliasi
        Dim JumlahBayar_HutangAfiliasi
        Dim SisaHutang_HutangAfiliasi

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangAfiliasi ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangAfiliasi ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Afiliasi Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHA = dr.Item("Nomor_BPHA")
            JumlahTagihan_HutangAfiliasi = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_HutangAfiliasi = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                       " WHERE Nomor_BPHA =  '" & NomorBPHA & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangAfiliasi += drBAYAR.Item("Pokok")
            Loop
            SisaHutang_HutangAfiliasi = JumlahTagihan_HutangAfiliasi - JumlahBayar_HutangAfiliasi

            If SisaHutang_HutangAfiliasi > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangAfiliasi VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHA & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("COA_Debet") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                                 " WHERE Nomor_BPHA = '" & NomorBPHA & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranHutangAfiliasi VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPHA & "', " &
                                          " '" & drTELUSUR.Item("Kode_Lawan_Transaksi") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Kredit") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Hutang Afiliasi Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Hutang Afiliasi Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaPiutangAfiliasi_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPPA
        Dim JumlahTagihan_PiutangAfiliasi
        Dim JumlahBayar_PiutangAfiliasi
        Dim SisaPiutang_PiutangAfiliasi

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangAfiliasi ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangAfiliasi ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Piutang Afiliasi Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPPA = dr.Item("Nomor_BPPA")
            JumlahTagihan_PiutangAfiliasi = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_PiutangAfiliasi = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangAfiliasi " &
                                       " WHERE Nomor_BPPA =  '" & NomorBPPA & "' " &
                                       " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_PiutangAfiliasi += drBAYAR.Item("Pokok")
            Loop
            SisaPiutang_PiutangAfiliasi = JumlahTagihan_PiutangAfiliasi - JumlahBayar_PiutangAfiliasi

            If SisaPiutang_PiutangAfiliasi > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Piutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanPiutangAfiliasi VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPPA & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Transaksi")) & "', '" &
                                dr.Item("Jumlah_Pinjaman") & "', '" &
                                dr.Item("Saldo_Awal") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Pembebanan") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &  'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangAfiliasi " &
                                                 " WHERE Nomor_BPPA = '" & NomorBPPA & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranPiutangAfiliasi VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPPA & "', " &
                                          " '" & drTELUSUR.Item("Kode_Lawan_Transaksi") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Debet") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & drTELUSUR.Item("Nomor_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Tanggal_Bukti_Potong") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan_Bukti_Potong") & "', " &
                                          " '" & 0 & "', " & 'Ini harus nol..! Jangan diganti value NomorJV..! Ada maksudnya.
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Afiliasi Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Afiliasi Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaDepositOperasional_WPF(ByRef TahunBukuDitutup, ByRef TanggalBukuDitutup, ByRef NomorIdKK, ByRef NomorIdKM, ByRef pesan_TutupBukuGagal)

        Dim NomorID_Tabel = 0
        Dim AngkaBPDO
        Dim NomorBPDO
        Dim NomorBPDO_Sebelumnya = Kosongan
        Dim JumlahDepositOperasional As Int64
        Dim JumlahTalangan As Int64
        Dim SisaTalangan As Int64
        Dim JumlahReimburse As Int64
        Dim JumlahOutStanding As Int64

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_DepositOperasional ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DepositOperasional ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Deposit Operasional
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)

        Do While dr.Read
            NomorID_Tabel += 1
            AngkaBPDO = dr.Item("Angka_BPDO") 'Jangan dihapus...!!!
            NomorBPDO = dr.Item("Nomor_BPDO") 'Jangan dihapus...!!!
            JumlahDepositOperasional = dr.Item("Jumlah_Transaksi")

            'Bayar Data Pembayaran :
            JumlahTalangan = 0
            cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP = '" & NomorBPDO & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahTalangan += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaTalangan = JumlahDepositOperasional - JumlahTalangan

            'Bayar  Data Pencairan :
            JumlahReimburse = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPDO & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahReimburse += drBAYAR.Item("Jumlah_Bayar")
            Loop
            JumlahOutStanding = JumlahTalangan - JumlahReimburse

            If SisaTalangan > 0 Or JumlahOutStanding > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Deposit Operasional :
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_DepositOperasional VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                dr.Item("Angka_BPDO") & "', '" &
                                dr.Item("Nomor_BPDO") & "', '" &
                                dr.Item("Nomor_Bukti") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Bukti")) & "', '" &
                                dr.Item("Nomor_Faktur_Pajak") & "', '" &
                                dr.Item("Kode_Lawan_Transaksi") & "', '" &
                                dr.Item("Nama_Lawan_Transaksi") & "', '" &
                                dr.Item("Kode_Customer") & "', '" &
                                dr.Item("Nama_Customer") & "', '" &
                                dr.Item("Nomor_Urut_Produk") & "', '" &
                                dr.Item("COA_Produk") & "', '" &
                                dr.Item("Nama_Produk") & "', '" &
                                dr.Item("Nomor_Referensi_Produk") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Referensi_Produk")) & "', '" &
                                dr.Item("Jumlah_Harga_Produk") & "', '" &
                                dr.Item("Jumlah_Transaksi") & "', '" &
                                dr.Item("Jumlah_Reimburse") & "', '" &
                                dr.Item("Potongan_Reimburse") & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                If StatusSuntingDatabase = True And NomorBPDO <> NomorBPDO_Sebelumnya Then

                    Dim KeteranganBayar = Kosongan
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Kosongan

                    'Simpan value Jumlah_Bayar ke Data Bank-Cash Out
                    If JumlahTalangan > 0 Then
                        NomorIdKK += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                    " '" & NomorIdKK & "', " &
                                    " '" & NomorIdKK & "', " &
                                    " '" & "KK-" & TahunBukuDitutup & "-" & NomorIdKK & "', " &
                                    " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & Kategori_PengeluaranTunai & "', " &
                                    " '" & Peruntukan_DepositOperasional & "', " &
                                    " '" & dr.Item("Kode_Lawan_Transaksi") & "', " &
                                    " '" & dr.Item("Nama_Lawan_Transaksi") & "', " &
                                    " '" & NomorBPDO & "', " &
                                    " '" & dr.Item("Nomor_Bukti") & "', " &
                                    " '" & TanggalFormatSimpan(dr.Item("Tanggal_Bukti")) & "', " &
                                    " '" & dr.Item("Keterangan") & "', " &
                                    " '" & JumlahDepositOperasional & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & JumlahTalangan & "', " &
                                    " '" & JumlahTalangan & "', " &
                                    " '" & 0 & "', " &
                                    " '" & KodeTautanCOA_DepositOperasional & "', " &
                                    " '" & KodeTautanCOA_Kas & "', " &
                                    " '" & BiayaAdministrasiBank & "', " &
                                    " '" & DitanggungOleh & "', " &
                                    " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & Status_Dibayar & "', " &
                                    " '" & Status_Dibayar & "', " &
                                    " '" & JenisPajak_Non & "', " &
                                    " '" & KodeSetoran_Non & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & "Rekap Pembayaran Tahun " & TahunBukuAktif & "." & "', " &
                                    " '" & 0 & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & TanggalKosongSimpan & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & 0 & "', " &
                                    " '" & UserAktif & "' " &
                                    " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    End If

                    'Simpan value Jumlah_Cair ke Data Bank-Cash In
                    If JumlahReimburse > 0 Then
                        NomorIdKM += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPenerimaan VALUES ( " &
                                    " '" & NomorIdKM & "', " &
                                    " '" & NomorIdKM & "', " &
                                    " '" & "KM-" & TahunBukuDitutup & "-" & NomorIdKM & "', " &
                                    " '" & TanggalFormatSimpan(TanggalBukuDitutup) & "', " &
                                    " '" & Kategori_PenerimaanTunai & "', " &
                                    " '" & Peruntukan_DepositOperasional & "', " &
                                    " '" & dr.Item("Kode_Customer") & "', " &
                                    " '" & dr.Item("Nama_Customer") & "', " &
                                    " '" & dr.Item("Nomor_BPDO") & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & dr.Item("Nomor_Bukti") & "', " &
                                    " '" & TanggalFormatSimpan(dr.Item("Tanggal_Bukti")) & "', " &
                                    " '" & dr.Item("Keterangan") & "', " &
                                    " '" & JumlahTalangan & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & JumlahReimburse & "', " &
                                    " '" & 0 & "', " &
                                    " '" & KodeTautanCOA_Kas & "', " &
                                    " '" & KodeTautanCOA_DepositOperasional & "', " &
                                    " '" & BiayaAdministrasiBank & "', " &
                                    " '" & DitanggungOleh & "', " &
                                    " '" & JenisPajak_Non & "', " &
                                    " '" & KodeSetoran_Non & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & 0 & "', " &
                                    " '" & "Rekap Pencairan Tahun " & TahunBukuAktif & "." & "', " &
                                    " '" & 0 & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & TanggalKosongSimpan & "', " &
                                    " '" & Kosongan & "', " &
                                    " '" & 0 & "', " &
                                    " '" & UserAktif & "' " &
                                    " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    End If

                End If

            End If
            NomorBPDO_Sebelumnya = NomorBPDO
        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengisian Saldo Awal Piutang Afiliasi Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Piutang Afiliasi Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Pesan_Gagal(pesan_TutupBukuGagal)
            Return
        End If


    End Sub



End Module
