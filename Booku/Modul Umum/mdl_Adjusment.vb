Imports System.Data.Odbc
Imports bcomm


Module mdl_Adjusment



    Public JumlahMutasiAdjusmentHPP As Int64
    Public NomorUrutJurnalAdjusmentHPP
    Sub SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka As Integer, COA_Awal As Integer, COA_Akhir As Integer)
        JumlahMutasiAdjusmentHPP = 0
        Dim JumlahKredit As Int64 = 0
        AksesDatabase_General(Buka)
        Dim COA As Integer
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_COA " &
                                     " WHERE COA BETWEEN " & COA_Awal &
                                     " AND " & COA_Akhir &
                                     " ORDER BY COA ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            COA = drTELUSUR.Item("COA")
            JumlahKredit _
                = TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) _
                - TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka)
            If JumlahKredit <> 0 Then
                NomorUrutJurnalAdjusmentHPP += 1
                TambahBarisKredit_BahanJurnal(COA, JumlahKredit)
                JumlahMutasiAdjusmentHPP += JumlahKredit
            End If
        Loop
        AksesDatabase_General(Tutup)
    End Sub


    Sub AdjusmentHPP_BiayaBahanBaku()

        Dim BulanTerakhirJurnalAdjusment_BiayaBahanBaku = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaBahanBaku & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaBahanBaku = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Bahan Baku sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaBahanBaku + 1

        Dim BulanTerakhirJurnalAdjusment_PersediaanBahanBaku = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_PersediaanBahanBaku_Lokal & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanAdjusment_Angka > BulanTerakhirJurnalAdjusment_PersediaanBahanBaku Then
            PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Baku untuk Bulan '" &
                                              KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment.")
            win_BOOKU.BukaModul_StockOpname_BahanBaku()
            Return
        End If

        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaBahanBaku, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, KodeTautanCOA_BiayaTransportasiPembelianBb_Lokal, KodeTautanCOA_BiayaPemakaianBahanBaku_Import)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaBahanBaku, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Bahan Baku' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Bahan Baku untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        End If
        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                        '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub


    Sub AdjusmentHPP_BiayaTenagaKerjaLangsung()

        Dim BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaTenagaKerjaLangsung & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Tenaga Kerja Langsung sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung + 1
        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaTenagaKerjaLangsung, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, KodeTautanCOA_BiayaGajiProduksi, 52199)

        If JumlahMutasiAdjusmentHPP = 0 Then
            IsiValueComboBypassTerkunci(usc_BukuPengawasanGaji.cmb_TahunTelusurData, TahunBukuAktif)
            IsiValueComboBypassTerkunci(usc_BukuPengawasanGaji.cmb_Bulan, KonversiAngkaKeBulanString(BulanAdjusment_Angka))
            PesanPemberitahuan("Silakan dorong terlebih dahulu Jurnal Gaji untuk Bulan '" &
                                                 KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment.")
            win_BOOKU.BukaModul_BukuPengawasanGaji()
            Return
        End If

        Pilihan = MessageBox.Show("Anda akan melakukan Adjusment HPP Biaya Tenaga Kerja Langsung untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'." & Enter2Baris &
                                  "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                        '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub



    Sub AdjusmentHPP_BiayaOverheadPabrik()

        Dim BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaOverheadPabrik & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Overhead Pabrik sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik + 1

        Dim BulanTerakhirJurnalAdjusment_PersediaanBahanPenolong = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_PersediaanBahanPenolong & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanAdjusment_Angka > BulanTerakhirJurnalAdjusment_PersediaanBahanPenolong Then
            PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Penolong untuk Bulan '" &
                                              KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment.")
            win_BOOKU.BukaModul_StockOpname_BahanPenolong()
            Return
        End If

        Dim COA_BiayaOverhead_Awal = 53102
        Dim COA_BiayaOverhead_Akhir = 53999

        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaOverheadPabrik, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, COA_BiayaOverhead_Awal, COA_BiayaOverhead_Akhir)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaOverheadPabrik, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Overhead Pabrik' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Overhead Pabrik untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'." & Enter2Baris
        End If

        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP   '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub


    Sub AdjusmentHPP_BiayaProduksi()

        Dim BulanTerakhirJurnalAdjusment_BiayaProduksi = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaProduksi & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaProduksi = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Produksi sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaProduksi + 1
        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaProduksi, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        JumlahMutasiAdjusmentHPP = 0
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaBahanBaku, BulanAdjusment_Angka)
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaTenagaKerjaLangsung, BulanAdjusment_Angka)
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaOverheadPabrik, BulanAdjusment_Angka)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaProduksi, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Produksi' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Produksi untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        End If
        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP   '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub
    Sub TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(COA, BulanAdjusment_Angka)
        Dim JumlahKredit _
            = TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) _
            - TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka)
        If JumlahKredit <> 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(COA, JumlahKredit)
            JumlahMutasiAdjusmentHPP += JumlahKredit
        End If
        PesanUntukProgrammer("COA : " & COA & Enter2Baris &
                             "Nama Akun : " & AmbilValue_NamaAkun(COA) & Enter2Baris &
                             "Total Debet : " & TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) & Enter2Baris &
                             "Total Kredit : " & TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka) & Enter2Baris &
                             "Jumlah Kredit : " & JumlahKredit & Enter2Baris &
                             "")
    End Sub



End Module
