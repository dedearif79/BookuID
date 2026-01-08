Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanHutangPPhPasal26_X

    Public JudulForm
    Public JenisPajak
    Public JenisTampilan
    Public JenisTampilan_ALL = "ALL"
    Public JenisTampilan_REKAP = "REKAP"
    Public JenisTampilan_DETAIL = "DETAIL"
    Public MasaPajak_All = "ALL"
    Public MasaPajak_Rekap = "REKAP"
    Public MasaPajak_Angka As Integer

    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NamaJasa_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim NPWP_Terseleksi
    Dim DPP_Terseleksi
    Dim PPhPasal26_Terseleksi
    Dim PPhPasal26_100_Terseleksi
    Dim PPhPasal26_101_Terseleksi
    Dim PPhPasal26_102_Terseleksi
    Dim PPhPasal26_103_Terseleksi
    Dim PPhPasal26_104_Terseleksi
    Dim PPhPasal26_105_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar_100_Terseleksi
    Dim JumlahBayar_101_Terseleksi
    Dim JumlahBayar_102_Terseleksi
    Dim JumlahBayar_103_Terseleksi
    Dim JumlahBayar_104_Terseleksi
    Dim JumlahBayar_105_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaHutang_100_Terseleksi
    Dim SisaHutang_101_Terseleksi
    Dim SisaHutang_102_Terseleksi
    Dim SisaHutang_103_Terseleksi
    Dim SisaHutang_104_Terseleksi
    Dim SisaHutang_105_Terseleksi
    Dim SisaHutang_Terseleksi
    Dim Keterangan_Terseleksi

    Dim SisaHutang_SaatCutOff

    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim MasaPajak = Kosongan
    Dim TahunPajakSebelumIni

    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorID
    Dim NomorBulan
    Dim NomorBPHP
    Dim Bulan = Kosongan
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim NPWP
    Dim KodeSupplier
    Dim NamaSupplier
    Dim DPP100
    Dim DPP101
    Dim DPP102
    Dim DPP103
    Dim DPP104
    Dim DPP105
    Dim DPP
    Dim RekapPerBulan_DPP_100
    Dim RekapPerBulan_DPP_101
    Dim RekapPerBulan_DPP_102
    Dim RekapPerBulan_DPP_103
    Dim RekapPerBulan_DPP_104
    Dim RekapPerBulan_DPP_105
    Dim RekapPerBulan_DPP
    Dim PPhPasal26_100
    Dim PPhPasal26_101
    Dim PPhPasal26_102
    Dim PPhPasal26_103
    Dim PPhPasal26_104
    Dim PPhPasal26_105
    Dim PPhPasal26
    Dim RekapPerBulan_PPhPasal26_100
    Dim RekapPerBulan_PPhPasal26_101
    Dim RekapPerBulan_PPhPasal26_102
    Dim RekapPerBulan_PPhPasal26_103
    Dim RekapPerBulan_PPhPasal26_104
    Dim RekapPerBulan_PPhPasal26_105
    Dim RekapPerBulan_PPhPasal26
    Dim JumlahTagihan
    Dim TanggalTransaksi
    Dim JumlahBayar_100
    Dim JumlahBayar_101
    Dim JumlahBayar_102
    Dim JumlahBayar_103
    Dim JumlahBayar_104
    Dim JumlahBayar_105
    Dim JumlahBayar
    Dim SisaHutang
    Dim JenisKodeSetoran
    Dim Keterangan

    Dim TotalDPP_100
    Dim TotalDPP_101
    Dim TotalDPP_102
    Dim TotalDPP_103
    Dim TotalDPP_104
    Dim TotalDPP_105
    Dim TotalDPP
    Dim TotalTagihan_100
    Dim TotalTagihan_101
    Dim TotalTagihan_102
    Dim TotalTagihan_103
    Dim TotalTagihan_104
    Dim TotalTagihan_105
    Dim TotalTagihan
    Dim TotalBayar_100
    Dim TotalBayar_101
    Dim TotalBayar_102
    Dim TotalBayar_103
    Dim TotalBayar_104
    Dim TotalBayar_105
    Dim TotalBayar
    Dim TotalSisaHutang

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim NomorInvoice_Sebelumnya

    Dim KodeSetoran_UntukBayar
    Dim KodeSetoran_UntukTabel

    Dim SumberData
    Dim SumberData_SisaHutangPajak = "Sisa Hutang Pajak"
    'Dim SumberData_Pembelian = "Pembelian"
    'Dim SumberData_PembayaranHutang = "Pembayaran Hutang"
    'Dim SumberData_AngsuranHutangBank = "Angsuran Hutang Bank"
    'Dim SumberData_AngsuranHutangLeasing = "Angsuran Hutang Leasing"
    'Dim SumberData_AngsuranHutangPihakKetiga = "Angsuran Hutang Pihak Ketiga"
    'Dim SumberData_AngsuranHutangAfiliasi = "Angsuran Hutang Afiliasi"

    Dim JumlahBarisDalamSatuBulan = 0

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        JenisPajak = JenisPajak_PPhPasal26

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_LihatJurnal.Visible = False
            pnl_CRUD.Visible = True
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.Visible = True
            pnl_CRUD.Visible = False
            grb_InfoSaldo.Text = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        TahunPajak = TahunBukuAktif
        KontenCombo_TahunPajak()
        KontenCombo_MasaPajak()

        Sub_JenisTampilan_REKAP()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        cmb_MasaPajak.Text = Kosongan 'Ini Penting...!!! Jangan dihapus...!!! Agar ketika ada perubahan teks/value pada cmb_TahunPajak tidak mengekseskusi Sub TampilkanData...!!!
        KontenCombo_TahunPajak() 'Sengaja pakai Sub KontenCombo, untuk me-refresh List Tahun Pajak, barangkali ada update data untuk Tahun Pajak Terlama
        cmb_MasaPajak.Text = MasaPajak_Rekap
    End Sub

    Sub KontenCombo_MasaPajak()
        cmb_MasaPajak.Items.Clear()
        cmb_MasaPajak.Items.Add(MasaPajak_All)
        cmb_MasaPajak.Items.Add(MasaPajak_Rekap)
        cmb_MasaPajak.Items.Add(Bulan_Januari)
        cmb_MasaPajak.Items.Add(Bulan_Februari)
        cmb_MasaPajak.Items.Add(Bulan_Maret)
        cmb_MasaPajak.Items.Add(Bulan_April)
        cmb_MasaPajak.Items.Add(Bulan_Mei)
        cmb_MasaPajak.Items.Add(Bulan_Juni)
        cmb_MasaPajak.Items.Add(Bulan_Juli)
        cmb_MasaPajak.Items.Add(Bulan_Agustus)
        cmb_MasaPajak.Items.Add(Bulan_September)
        cmb_MasaPajak.Items.Add(Bulan_Oktober)
        cmb_MasaPajak.Items.Add(Bulan_Nopember)
        cmb_MasaPajak.Items.Add(Bulan_Desember)
        cmb_MasaPajak.Text = MasaPajak_Rekap
    End Sub

    Sub KontenCombo_TahunPajak()

        TahunHutangPajakTerlama = AmbilTahunTerlama_SisaHutangPajak(JenisPajak)
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.Text = TahunPajak

    End Sub

    Sub TampilkanData()

        btn_DetailPembayaran.Enabled = False

        'Judul Halaman :
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        If MasaPajak = Kosongan Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)
        DataTabelUtama.Columns("DPP_100").Visible = False
        DataTabelUtama.Columns("DPP_101").Visible = False
        DataTabelUtama.Columns("DPP_102").Visible = False
        DataTabelUtama.Columns("DPP_103").Visible = False
        DataTabelUtama.Columns("DPP_104").Visible = False
        DataTabelUtama.Columns("DPP_105").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_100").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_101").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_102").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_103").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_104").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_26_105").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_100").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_101").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_102").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_103").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_104").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_105").Visible = False

        'Data Tabel :
        NomorUrut = 0
        NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
        TanggalTransaksi = Kosongan
        NomorInvoice = Kosongan
        NomorFakturPajak = Kosongan
        NamaJasa = Kosongan
        NPWP = Kosongan
        KodeSupplier = Kosongan
        NamaSupplier = Kosongan
        DPP = 0
        Keterangan = Kosongan

        TotalDPP_100 = 0
        TotalDPP_101 = 0
        TotalDPP_102 = 0
        TotalDPP_103 = 0
        TotalDPP_104 = 0
        TotalDPP_105 = 0
        TotalDPP = 0

        TotalTagihan_100 = 0
        TotalTagihan_101 = 0
        TotalTagihan_102 = 0
        TotalTagihan_103 = 0
        TotalTagihan_104 = 0
        TotalTagihan_105 = 0
        TotalTagihan = 0

        TotalBayar_100 = 0
        TotalBayar_101 = 0
        TotalBayar_102 = 0
        TotalBayar_103 = 0
        TotalBayar_104 = 0
        TotalBayar_105 = 0
        TotalBayar = 0

        TotalSisaHutang = 0


        'TAMPILAN REKAP : ---------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_REKAP Then

            SumberData = Kosongan

            Index_BarisTabel = 0
            NomorBulan = 0

            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabaseTransaksi = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                NomorBulan = AmbilAngka(NomorBulan) + 1
                Bulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP26 & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulan_DPP_100 = 0
                RekapPerBulan_DPP_101 = 0
                RekapPerBulan_DPP_102 = 0
                RekapPerBulan_DPP_103 = 0
                RekapPerBulan_DPP_104 = 0
                RekapPerBulan_DPP_105 = 0
                RekapPerBulan_DPP = 0

                RekapPerBulan_PPhPasal26_100 = 0
                RekapPerBulan_PPhPasal26_101 = 0
                RekapPerBulan_PPhPasal26_102 = 0
                RekapPerBulan_PPhPasal26_103 = 0
                RekapPerBulan_PPhPasal26_104 = 0
                RekapPerBulan_PPhPasal26_105 = 0
                RekapPerBulan_PPhPasal26 = 0

                JumlahBayar_100 = 0
                JumlahBayar_101 = 0
                JumlahBayar_102 = 0
                JumlahBayar_103 = 0
                JumlahBayar_104 = 0
                JumlahBayar_105 = 0
                JumlahBayar = 0
                SisaHutang = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika [ Tahun Buku LAMPAU ] atau [ Tahun Buku NORMAL Tapi Tahun Pajaknya Tidak Sama Dengan Tahun Buku Aktif ]:
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Or
                    (JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak <> TahunBukuAktif) _
                    Then
                    SumberData = SumberData_SisaHutangPajak
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak = '" & JenisPajak & "' AND " &
                                          " DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    Do While dr.Read
                        DPP = dr.Item("DPP")
                        PPhPasal26 = dr.Item("Jumlah_Hutang")
                        AmbilValue_PerKodeSetoran()
                    Loop
                End If

                'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
                If JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak = TahunBukuAktif Then

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                          " AND Kode_Setoran                       <> '" & KodeSetoran_101 & "' " & '(Dividen jangan dimasukkan).
                                          " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar                        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
                    If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak
                    BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                    Do While dr.Read
                        PPhPasal26 = dr.Item("PPh_Terutang")
                        If PPhPasal26 > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                      " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                      KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                DPP = PPhPasal26 / (drPEMB.Item("Tarif_PPh") / 100)
                            Else
                                DPP = dr.Item("Bagi_Hasil")
                            End If
                            AmbilValue_PerKodeSetoran()
                        End If
                    Loop

                    'Dividen :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                          " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                          " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
                    If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak
                    BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                    Do While dr.Read
                        PPhPasal26 = dr.Item("PPh_Terutang")
                        If PPhPasal26 > 0 Then
                            DPP = dr.Item("Jumlah_Dividen")
                            SumberData = JenisJasa_Dividen
                            AmbilValue_PerKodeSetoran()
                        End If
                    Loop

                End If

                'Data Pembayaran :
                If RekapPerBulan_PPhPasal26 > 0 Then
                    Dim TahunTelusurPembayaran = TahunPajak
                    Dim PencegahLoopingTahunPajakLampau = 0
                    Do While TahunTelusurPembayaran <= TahunBukuAktif
                        If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
                        If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
                        If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                            cmd = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                                  " WHERE Nomor_BP      = '" & NomorBPHP & "' " &
                                                  " AND Status_Invoice  = '" & Status_Dibayar & "' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                            dr_ExecuteReader()
                            Do While dr.Read
                                JumlahBayar += dr.Item("Jumlah_Bayar")
                                Select Case dr.Item("Kode_Setoran")
                                    Case KodeSetoran_100
                                        JumlahBayar_100 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_101
                                        JumlahBayar_101 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_102
                                        JumlahBayar_102 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_103
                                        JumlahBayar_103 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_104
                                        JumlahBayar_104 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_105
                                        JumlahBayar_105 += dr.Item("Jumlah_Bayar")
                                End Select
                                If JumlahBayar >= RekapPerBulan_PPhPasal26 Then Exit Do
                            Loop
                            TutupDatabaseTransaksi_Alternatif()
                        End If
                        If JumlahBayar >= RekapPerBulan_PPhPasal26 Then Exit Do
                        PencegahLoopingTahunPajakLampau += 1
                        TahunTelusurPembayaran += 1
                    Loop

                End If

                TotalDPP_100 += AmbilAngka(RekapPerBulan_DPP_100)
                TotalDPP_101 += AmbilAngka(RekapPerBulan_DPP_101)
                TotalDPP_102 += AmbilAngka(RekapPerBulan_DPP_102)
                TotalDPP_103 += AmbilAngka(RekapPerBulan_DPP_103)
                TotalDPP_104 += AmbilAngka(RekapPerBulan_DPP_104)
                TotalDPP_105 += AmbilAngka(RekapPerBulan_DPP_105)
                TotalDPP += AmbilAngka(RekapPerBulan_DPP)

                TotalTagihan_100 += AmbilAngka(RekapPerBulan_PPhPasal26_100)
                TotalTagihan_101 += AmbilAngka(RekapPerBulan_PPhPasal26_101)
                TotalTagihan_102 += AmbilAngka(RekapPerBulan_PPhPasal26_102)
                TotalTagihan_103 += AmbilAngka(RekapPerBulan_PPhPasal26_103)
                TotalTagihan_104 += AmbilAngka(RekapPerBulan_PPhPasal26_104)
                TotalTagihan_105 += AmbilAngka(RekapPerBulan_PPhPasal26_105)
                TotalTagihan += AmbilAngka(RekapPerBulan_PPhPasal26)

                TotalBayar_100 += AmbilAngka(JumlahBayar_100)
                TotalBayar_101 += AmbilAngka(JumlahBayar_101)
                TotalBayar_102 += AmbilAngka(JumlahBayar_102)
                TotalBayar_103 += AmbilAngka(JumlahBayar_103)
                TotalBayar_104 += AmbilAngka(JumlahBayar_104)
                TotalBayar_105 += AmbilAngka(JumlahBayar_105)
                TotalBayar += AmbilAngka(JumlahBayar)

                SisaHutang = AmbilAngka(RekapPerBulan_PPhPasal26) - AmbilAngka(JumlahBayar)
                TotalSisaHutang += AmbilAngka(SisaHutang)

                TambahBaris()

            Loop

            AksesDatabase_Transaksi(Tutup)

            Baris_KetetapanPajak()

            'Baris TOTAL untuk Jenis Tampilan REKAP :
            If TotalTagihan = 0 Then TotalTagihan = StripKosong
            If TotalBayar = 0 Then TotalBayar = StripKosong
            If TotalSisaHutang = 0 Then TotalSisaHutang = StripKosong
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, "T O T A L",
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    TotalDPP_100, TotalDPP_101, TotalDPP_102, TotalDPP_103, TotalDPP_104, TotalDPP_105, TotalDPP,
                                    TotalTagihan_100, TotalTagihan_101, TotalTagihan_102, TotalTagihan_103, TotalTagihan_104, TotalTagihan_105, TotalTagihan,
                                    TotalBayar_100, TotalBayar_101, TotalBayar_102, TotalBayar_103, TotalBayar_104, TotalBayar_105, TotalBayar, TotalSisaHutang, KodeSetoran_UntukTabel, Kosongan, Kosongan)

            If TotalDPP_100 > 0 Then DataTabelUtama.Columns("DPP_100").Visible = True
            If TotalDPP_101 > 0 Then DataTabelUtama.Columns("DPP_101").Visible = True
            If TotalDPP_102 > 0 Then DataTabelUtama.Columns("DPP_102").Visible = True
            If TotalDPP_103 > 0 Then DataTabelUtama.Columns("DPP_103").Visible = True
            If TotalDPP_104 > 0 Then DataTabelUtama.Columns("DPP_104").Visible = True
            If TotalDPP_105 > 0 Then DataTabelUtama.Columns("DPP_105").Visible = True

            If TotalTagihan_100 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_100").Visible = True
            If TotalTagihan_101 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_101").Visible = True
            If TotalTagihan_102 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_102").Visible = True
            If TotalTagihan_103 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_103").Visible = True
            If TotalTagihan_104 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_104").Visible = True
            If TotalTagihan_105 > 0 Then DataTabelUtama.Columns("PPh_Pasal_26_105").Visible = True

            If TotalBayar_100 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_100").Visible = True
            If TotalBayar_101 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_101").Visible = True
            If TotalBayar_102 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_102").Visible = True
            If TotalBayar_103 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_103").Visible = True
            If TotalBayar_104 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_104").Visible = True
            If TotalBayar_105 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_105").Visible = True

        End If


        'TAMPILAN DETAIL : ---------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_DETAIL Then

            SumberData = Kosongan

            NomorBulan = KonversiBulanKeNomor_String(MasaPajak)
            Bulan = MasaPajak

            RekapPerBulan_DPP_100 = 0
            RekapPerBulan_DPP_101 = 0
            RekapPerBulan_DPP_102 = 0
            RekapPerBulan_DPP_103 = 0
            RekapPerBulan_DPP_104 = 0
            RekapPerBulan_DPP_105 = 0
            RekapPerBulan_DPP = 0

            RekapPerBulan_PPhPasal26_100 = 0
            RekapPerBulan_PPhPasal26_101 = 0
            RekapPerBulan_PPhPasal26_102 = 0
            RekapPerBulan_PPhPasal26_103 = 0
            RekapPerBulan_PPhPasal26_104 = 0
            RekapPerBulan_PPhPasal26_105 = 0
            RekapPerBulan_PPhPasal26 = 0

            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak

            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            If StatusKoneksiDatabaseTransaksi_Alternatif = False Then Return

            'Jika Tahun Pajak = Tahun Buku LAMPAU :
            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then
                SumberData = SumberData_SisaHutangPajak
                cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                      " WHERE Jenis_Pajak = '" & JenisPajak & "' AND " &
                                      " DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorID = dr.Item("Nomor_ID")
                    TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                    NamaJasa = dr.Item("Nama_Jasa")
                    KodeSupplier = dr.Item("Kode_Supplier")
                    AmbilValue_NamaDanNPWPSupplier()
                    DPP = dr.Item("DPP")
                    PPhPasal26 = dr.Item("Jumlah_Hutang")
                    AmbilValue_PerKodeSetoran()
                    Keterangan = dr.Item("Keterangan")
                    TambahBaris()
                Loop
            End If

            'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                      " AND Kode_Setoran                       <> '" & KodeSetoran_101 & "' " & '(Dividen jangan dimasukkan).
                                      " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                      " AND Jumlah_Bayar                        > 0 " &
                                      " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    PPhPasal26 = dr.Item("PPh_Terutang")
                    If PPhPasal26 > 0 Then
                        SumberData = dr.Item("Peruntukan")
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        NomorInvoice = dr.Item("Nomor_Invoice")
                        KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                        AmbilValue_NamaDanNPWPSupplier()
                        Keterangan = dr.Item("Catatan")
                        Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                        Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                      " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                      KoneksiDatabaseTransaksi_Alternatif)
                        Dim drPEMB = cmdPEMB.ExecuteReader
                        drPEMB.Read()
                        If drPEMB.HasRows Then
                            TanggalInvoice = TanggalFormatTampilan(drPEMB.Item("Tanggal_Invoice"))
                            NomorInvoice = drPEMB.Item("Nomor_Invoice")
                            NomorFakturPajak = drPEMB.Item("Nomor_Faktur_Pajak")
                            NamaJasa = drPEMB.Item("Nama_Produk")
                            DPP = PPhPasal26 / (drPEMB.Item("Tarif_PPh") / 100)
                        Else
                            NomorFakturPajak = Kosongan
                            NamaJasa = Kosongan
                            DPP = dr.Item("Bagi_Hasil")
                        End If
                        AmbilValue_PerKodeSetoran()
                        TambahBaris()
                    End If
                Loop

                'Dividen :
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                      " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                      " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    PPhPasal26 = dr.Item("PPh_Terutang")
                    If PPhPasal26 > 0 Then
                        SumberData = JenisJasa_Dividen
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                        TanggalInvoice = TanggalTransaksi
                        NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                        KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                        NamaSupplier = AmbilValue_NamaPemegangSaham(KodeSupplier)
                        NPWP = AmbilValue_NPWPPemegangSaham(KodeSupplier)
                        Keterangan = dr.Item("Keterangan")
                        NamaJasa = JenisJasa_Dividen
                        DPP = dr.Item("Jumlah_Dividen")
                        AmbilValue_PerKodeSetoran()
                        TambahBaris()
                    End If
                Loop

            End If

            TutupDatabaseTransaksi_Alternatif()

            'Urutkan berdasarkan Tanggal :
            '(Ini penting karena pengambilan Data bersumber pada 2 tabel (Data Pembelian Tunai, dan Data Pembayaran Hutang Usaha)
            Dim KolomTanggalTransaksi As DataGridViewColumn = DataTabelUtama.Columns("Tanggal_Transaksi")
            DataTabelUtama.Sort(KolomTanggalTransaksi, System.ComponentModel.ListSortDirection.Ascending)
            NomorUrut = 0
            For Each row As DataGridViewRow In DataTabelUtama.Rows
                NomorUrut += 1
                row.Cells("Nomor_Urut").Value = NomorUrut
            Next

            'Baris TOTAL untuk Jenis Tampilan DETAIL :
            If DataTabelUtama.RowCount > 0 Then
                DataTabelUtama.Rows.Add()
                DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, "T O T A L   ",
                                        RekapPerBulan_PPhPasal26_100, RekapPerBulan_PPhPasal26_101, RekapPerBulan_PPhPasal26_102, RekapPerBulan_PPhPasal26_103, RekapPerBulan_PPhPasal26_104, RekapPerBulan_PPhPasal26_105, RekapPerBulan_PPhPasal26, Kosongan, Kosongan, Kosongan, KodeSetoran_UntukTabel, Kosongan, Kosongan)
            End If


        End If



        'JENIS TAMPILAN ALL : ---------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_ALL Then

            SumberData = Kosongan

            NomorBulan = 0

            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak

            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            If StatusKoneksiDatabaseTransaksi_Alternatif = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                JumlahBarisDalamSatuBulan = 0
                NomorBulan = AmbilAngka(NomorBulan) + 1
                Bulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP26 & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulan_DPP_100 = 0
                RekapPerBulan_DPP_101 = 0
                RekapPerBulan_DPP_102 = 0
                RekapPerBulan_DPP_103 = 0
                RekapPerBulan_DPP_104 = 0
                RekapPerBulan_DPP_105 = 0
                RekapPerBulan_DPP = 0

                RekapPerBulan_PPhPasal26_100 = 0
                RekapPerBulan_PPhPasal26_101 = 0
                RekapPerBulan_PPhPasal26_102 = 0
                RekapPerBulan_PPhPasal26_103 = 0
                RekapPerBulan_PPhPasal26_104 = 0
                RekapPerBulan_PPhPasal26_105 = 0
                RekapPerBulan_PPhPasal26 = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika Tahun Pajak = Tahun Buku LAMPAU :
                If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then
                    SumberData = SumberData_SisaHutangPajak
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak = '" & JenisPajak & "' AND " &
                                          " DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        NomorID = dr.Item("Nomor_ID")
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        NomorInvoice = dr.Item("Nomor_Invoice")
                        NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                        NamaJasa = dr.Item("Nama_Jasa")
                        KodeSupplier = dr.Item("Kode_Supplier")
                        AmbilValue_NamaDanNPWPSupplier()
                        DPP = dr.Item("DPP")
                        PPhPasal26 = dr.Item("Jumlah_Hutang")
                        AmbilValue_PerKodeSetoran()
                        Keterangan = dr.Item("Keterangan")
                        TambahBaris()
                    Loop
                End If

                'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
                If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                          " AND Kode_Setoran                       <> '" & KodeSetoran_101 & "' " & '(Dividen jangan dimasukkan).
                                          " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar                        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPhPasal26 = dr.Item("PPh_Terutang")
                        If PPhPasal26 > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                            AmbilValue_NamaDanNPWPSupplier()
                            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                            NomorInvoice = dr.Item("Nomor_Invoice")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                          " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                          " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                          KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                NomorFakturPajak = drPEMB.Item("Nomor_Faktur_Pajak")
                                NamaJasa = drPEMB.Item("Nama_Produk")
                                DPP = PPhPasal26 / (drPEMB.Item("Tarif_PPh") / 100)
                            Else
                                NomorFakturPajak = Kosongan
                                NamaJasa = Kosongan
                                DPP = dr.Item("Bagi_Hasil")
                            End If
                            AmbilValue_PerKodeSetoran()
                            Keterangan = dr.Item("Catatan")
                            TambahBaris()
                        End If
                    Loop

                    'Dividen :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                          " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                          " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPhPasal26 = dr.Item("PPh_Terutang")
                        If PPhPasal26 > 0 Then
                            SumberData = JenisJasa_Dividen
                            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                            TanggalInvoice = TanggalTransaksi
                            NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                            KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                            NamaSupplier = AmbilValue_NamaPemegangSaham(KodeSupplier)
                            NPWP = AmbilValue_NPWPPemegangSaham(KodeSupplier)
                            Keterangan = dr.Item("Keterangan")
                            NamaJasa = JenisJasa_Dividen
                            DPP = dr.Item("Jumlah_Dividen")
                            AmbilValue_PerKodeSetoran()
                            TambahBaris()
                        End If
                    Loop

                End If

                TotalTagihan += RekapPerBulan_PPhPasal26

                If JumlahBarisDalamSatuBulan > 0 Then
                    'Header Bulan :
                    DataTabelUtama.Rows.Insert(DataTabelUtama.RowCount - JumlahBarisDalamSatuBulan, Kosongan, Kosongan, Kosongan, Kosongan, Bulan)

                    'Baris REKAP PERBULAN untuk Jenis Tampilan ALL :
                    DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, "       Total " & Bulan, Kosongan,
                                        RekapPerBulan_PPhPasal26_100, RekapPerBulan_PPhPasal26_101, RekapPerBulan_PPhPasal26_102, RekapPerBulan_PPhPasal26_103, RekapPerBulan_PPhPasal26_104, RekapPerBulan_PPhPasal26_105,
                                        RekapPerBulan_PPhPasal26, Kosongan, Kosongan, Kosongan, KodeSetoran_UntukTabel, Kosongan, Kosongan)
                    DataTabelUtama.Rows.Add()
                End If

            Loop

            TutupDatabaseTransaksi_Alternatif()

            If DataTabelUtama.RowCount > 0 Then
                'Baris TOTAL untuk Jenis Tampilan ALL :
                If TotalTagihan = 0 Then TotalTagihan = StripKosong
                DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, "       TOTAL TAGIHAN ", Kosongan,
                                    TotalTagihan_100, TotalTagihan_101, TotalTagihan_102, TotalTagihan_103, TotalTagihan_104, TotalTagihan_105, TotalTagihan, Kosongan, Kosongan, Kosongan, KodeSetoran_UntukTabel, Kosongan, Kosongan)
            End If

        End If


        If JenisTampilan = JenisTampilan_ALL Or JenisTampilan = JenisTampilan_REKAP Then

            AksesDatabase_Transaksi(Buka)

            'Hitung Total Tagihan Selama Sebelum Cut Off :
            Dim TotalTagihan_SelamaSebelumCutOff = 0
            cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                  " WHERE Jenis_Pajak = '" & JenisPajak & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                TotalTagihan_SelamaSebelumCutOff += dr.Item("Jumlah_Hutang")
            Loop

            'Hitung Total Pembayaran Selama Sebelum Cut Off :
            Dim TotalBayar_SelamaSebelumCutOff = 0
            cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Jenis_Pajak = '" & JenisPajak & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                TotalBayar_SelamaSebelumCutOff += dr.Item("Jumlah_Bayar")
            Loop

            AksesDatabase_Transaksi(Tutup)

            'Hitung Saldo Akhir Saat Cut Off :
            SisaHutang_SaatCutOff = TotalTagihan_SelamaSebelumCutOff - TotalBayar_SelamaSebelumCutOff

            'Tampilkan Grup/Panel Info Saldo :
            TampilkanInfoSaldo()

        End If


        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = SisaHutang_SaatCutOff
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                AmbilValue_SaldoAwalBerdasarkanList()
                AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                CekKesesuaianSaldoAwal()
                txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
        End Select

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()

        If JenisTampilan <> JenisTampilan_REKAP And PPhPasal26 = 0 Then Return

        NomorUrut += 1
        JumlahBarisDalamSatuBulan += 1

        If JenisTampilan = JenisTampilan_DETAIL Then JumlahTagihan = PPhPasal26
        If JenisTampilan = JenisTampilan_ALL Then JumlahTagihan = PPhPasal26
        If JenisTampilan = JenisTampilan_REKAP Then
            DPP = RekapPerBulan_DPP
            JumlahTagihan = RekapPerBulan_PPhPasal26
        End If

        If AmbilAngka(RekapPerBulan_DPP_100) = 0 Then RekapPerBulan_DPP_100 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_101) = 0 Then RekapPerBulan_DPP_101 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_102) = 0 Then RekapPerBulan_DPP_102 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_103) = 0 Then RekapPerBulan_DPP_103 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_104) = 0 Then RekapPerBulan_DPP_104 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_105) = 0 Then RekapPerBulan_DPP_105 = StripKosong
        If AmbilAngka(DPP) = 0 Then DPP = StripKosong

        If AmbilAngka(RekapPerBulan_PPhPasal26_100) = 0 Then RekapPerBulan_PPhPasal26_100 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal26_101) = 0 Then RekapPerBulan_PPhPasal26_101 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal26_102) = 0 Then RekapPerBulan_PPhPasal26_102 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal26_103) = 0 Then RekapPerBulan_PPhPasal26_103 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal26_104) = 0 Then RekapPerBulan_PPhPasal26_104 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal26_105) = 0 Then RekapPerBulan_PPhPasal26_105 = StripKosong
        If AmbilAngka(JumlahTagihan) = 0 Then JumlahTagihan = StripKosong

        If AmbilAngka(JumlahBayar_100) = 0 Then JumlahBayar_100 = StripKosong
        If AmbilAngka(JumlahBayar_101) = 0 Then JumlahBayar_101 = StripKosong
        If AmbilAngka(JumlahBayar_102) = 0 Then JumlahBayar_102 = StripKosong
        If AmbilAngka(JumlahBayar_103) = 0 Then JumlahBayar_103 = StripKosong
        If AmbilAngka(JumlahBayar_104) = 0 Then JumlahBayar_104 = StripKosong
        If AmbilAngka(JumlahBayar_105) = 0 Then JumlahBayar_105 = StripKosong
        If AmbilAngka(JumlahBayar) = 0 Then JumlahBayar = StripKosong

        If AmbilAngka(SisaHutang) = 0 Then SisaHutang = StripKosong

        DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, Bulan,
                                TanggalTransaksi, TanggalInvoice, NomorInvoice, NomorFakturPajak, NamaJasa, NPWP, KodeSupplier, NamaSupplier,
                                RekapPerBulan_DPP_100, RekapPerBulan_DPP_101, RekapPerBulan_DPP_102, RekapPerBulan_DPP_103, RekapPerBulan_DPP_104, RekapPerBulan_DPP_105, DPP,
                                RekapPerBulan_PPhPasal26_100, RekapPerBulan_PPhPasal26_101, RekapPerBulan_PPhPasal26_102, RekapPerBulan_PPhPasal26_103, RekapPerBulan_PPhPasal26_104, RekapPerBulan_PPhPasal26_105,
                                JumlahTagihan, JumlahBayar_100, JumlahBayar_101, JumlahBayar_102, JumlahBayar_103, JumlahBayar_104, JumlahBayar_105, JumlahBayar, SisaHutang, KodeSetoran_UntukTabel, JenisKodeSetoran, Keterangan)

        If JenisTampilan = JenisTampilan_REKAP Then
            If RekapPerBulan_PPhPasal26 > 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If RekapPerBulan_PPhPasal26 = 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If

        Index_BarisTabel += 1
        'Application.DoEvents()

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        grb_Pembayaran.Visible = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_DetailPembayaran.Enabled = True
    End Sub

    Sub AmbilValue_NamaDanNPWPSupplier()
        AksesDatabase_General(Buka)
        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                      " WHERE Kode_Mitra = '" & KodeSupplier & "' ", KoneksiDatabaseGeneral)
        drTELUSUR2_ExecuteReader()
        drTELUSUR2.Read()
        If drTELUSUR2.HasRows Then
            NamaSupplier = drTELUSUR2.Item("Nama_Mitra")
            NPWP = drTELUSUR2.Item("NPWP")
        End If
        AksesDatabase_General(Tutup)
    End Sub

    Sub AmbilValue_PerKodeSetoran()

        KodeSetoran_UntukTabel = dr.Item("Kode_Setoran")
        JenisKodeSetoran = PenentuanJenisKodeSetoran(JenisPajak, KodeSetoran_UntukTabel)

        Select Case KodeSetoran_UntukTabel
            Case KodeSetoran_100 'Sewa Asset Luar Negeri
                DPP100 = DPP
                RekapPerBulan_DPP_100 = AmbilAngka(RekapPerBulan_DPP_100) + DPP
                PPhPasal26_100 = PPhPasal26
                RekapPerBulan_PPhPasal26_100 = AmbilAngka(RekapPerBulan_PPhPasal26_100) + PPhPasal26
            Case KodeSetoran_101 'Dividen
                DPP101 = DPP
                RekapPerBulan_DPP_101 = AmbilAngka(RekapPerBulan_DPP_101) + DPP
                PPhPasal26_101 = PPhPasal26
                RekapPerBulan_PPhPasal26_101 = AmbilAngka(RekapPerBulan_PPhPasal26_101) + PPhPasal26
            Case KodeSetoran_102 'Bunga
                DPP102 = DPP
                RekapPerBulan_DPP_102 = AmbilAngka(RekapPerBulan_DPP_102) + DPP
                PPhPasal26_102 = PPhPasal26
                RekapPerBulan_PPhPasal26_102 = AmbilAngka(RekapPerBulan_PPhPasal26_102) + PPhPasal26
            Case KodeSetoran_103 'Royalty
                DPP103 = DPP
                RekapPerBulan_DPP_103 = AmbilAngka(RekapPerBulan_DPP_103) + DPP
                PPhPasal26_103 = PPhPasal26
                RekapPerBulan_PPhPasal26_103 = AmbilAngka(RekapPerBulan_PPhPasal26_103) + PPhPasal26
            Case KodeSetoran_104 'Jasa
                DPP104 = DPP
                RekapPerBulan_DPP_104 = AmbilAngka(RekapPerBulan_DPP_104) + DPP
                PPhPasal26_104 = PPhPasal26
                RekapPerBulan_PPhPasal26_104 = AmbilAngka(RekapPerBulan_PPhPasal26_104) + PPhPasal26
            Case KodeSetoran_105 'Laba Pajak BUT
                DPP105 = DPP
                RekapPerBulan_DPP_105 = AmbilAngka(RekapPerBulan_DPP_105) + DPP
                PPhPasal26_105 = PPhPasal26
                RekapPerBulan_PPhPasal26_105 = AmbilAngka(RekapPerBulan_PPhPasal26_105) + PPhPasal26
        End Select

        RekapPerBulan_DPP += DPP
        RekapPerBulan_PPhPasal26 += PPhPasal26

    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        If TahunPajak = TahunBukuAktif Then
            Dim JumlahTagihan_SA = 0
            Dim JumlahBayar_SA = 0
            Dim Tahun_SA = TahunBukuAktif - 1
            AksesDatabase_Transaksi(Buka)
            cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                         " WHERE Jenis_Pajak = '" & JenisPajak & "' ", KoneksiDatabaseTransaksi)
            drTAGIHAN_ExecuteReader()
            Do While drTAGIHAN.Read
                JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Hutang")
            Loop
            AksesDatabase_Transaksi(Tutup)
            Dim TahunTelusurPembayaran = TahunCutOff
            Do While TahunTelusurPembayaran <= TahunBukuKemarin
                TahunBuku_Alternatif = TahunTelusurPembayaran
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPajak " &
                                           " WHERE Jenis_Pajak = '" & JenisPajak & "' ", KoneksiDatabaseTransaksi_Alternatif)
                drBAYAR_ExecuteReader()
                Do While drBAYAR.Read
                    JumlahBayar_SA += drBAYAR.Item("Jumlah_Bayar")
                Loop
                TutupDatabaseTransaksi_Alternatif()
                TahunTelusurPembayaran += 1
            Loop
            SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        Dim JumlahPenyesuaian_DEBET = 0
        Dim JumlahPenyesuaian_KREDIT = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal26 & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal26 & "' " &
                              " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            If dr.HasRows Then
                JumlahPenyesuaian_DEBET += dr.Item("Jumlah_Debet")
                JumlahPenyesuaian_KREDIT += dr.Item("Jumlah_Kredit")
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        JumlahPenyesuaianSaldo = JumlahPenyesuaian_KREDIT - JumlahPenyesuaian_DEBET
        SaldoAwal_BerdasarkanCOA_PlusPenyesuaian = SaldoAwal_BerdasarkanCOA + JumlahPenyesuaianSaldo
        txt_AJP.Text = JumlahPenyesuaianSaldo
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal26 & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAkhir_BerdasarkanCOA = dr.Item("Saldo_Awal")
        AksesDatabase_Transaksi(Tutup)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
    End Sub


    Sub Baris_KetetapanPajak()

        Dim JenisPajak_YangDitelusuri = JenisPajak
        Dim NomorBPHP_KetetapanPajak = Kosongan
        Dim JumlahTagihan_KetetapanPajak
        Dim JumlahBayar_KetetapanPajak
        Dim SisaHutang_KetetapanPajak

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak " &
                              " WHERE Jenis_Pajak = '" & JenisPajak_YangDitelusuri & "' ",
                              KoneksiDatabaseTransaksi_Alternatif)
        dr_ExecuteReader()
        JumlahTagihan_KetetapanPajak = 0
        Do While dr.Read
            NomorBPHP_KetetapanPajak = dr.Item("Nomor_BPHP")
            JumlahTagihan_KetetapanPajak += dr.Item("Pokok_Pajak")
        Loop

        'Data Pembayaran Pokok Pajak :
        JumlahBayar_KetetapanPajak = 0
        cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPajak " &
                                   " WHERE Nomor_BPHP LIKE '%" & AwalanBPKP & "%' " &
                                   " AND Jenis_Pajak = '" & JenisPajak_YangDitelusuri & "' ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        drBAYAR_ExecuteReader()
        Do While drBAYAR.Read
            JumlahBayar_KetetapanPajak += drBAYAR.Item("Jumlah_Bayar")
        Loop

        TutupDatabaseTransaksi_Alternatif()

        SisaHutang_KetetapanPajak = JumlahTagihan_KetetapanPajak - JumlahBayar_KetetapanPajak

        If JumlahTagihan_KetetapanPajak = 0 Then JumlahTagihan_KetetapanPajak = StripKosong
        If JumlahBayar_KetetapanPajak = 0 Then JumlahBayar_KetetapanPajak = StripKosong
        If SisaHutang_KetetapanPajak = 0 Then SisaHutang_KetetapanPajak = StripKosong

        DataTabelUtama.Rows.Add()
        DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, "Ketetapan Pajak",
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, JumlahTagihan_KetetapanPajak,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak,
                                    Kosongan, Kosongan, Kosongan)

        TotalTagihan += AmbilAngka(JumlahTagihan_KetetapanPajak)
        TotalBayar += AmbilAngka(JumlahBayar_KetetapanPajak)
        TotalSisaHutang += AmbilAngka(SisaHutang_KetetapanPajak)

    End Sub

    Sub CekKesesuaianSaldoAwal()
        If SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian Then
            KesesuaianSaldoAwal = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        If SaldoAkhir_BerdasarkanList = SaldoAkhir_BerdasarkanCOA Then
            KesesuaianSaldoAkhir = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub

    Private Sub cmb_MasaPajak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak.SelectedIndexChanged
    End Sub
    Private Sub cmb_MasaPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_MasaPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_MasaPajak_TextChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak.TextChanged

        MasaPajak = cmb_MasaPajak.Text

        MasaPajak_Angka = KonversiBulanKeAngka(MasaPajak)

        If ProsesLoadingForm = False Then
            Select Case MasaPajak
                Case MasaPajak_All
                    Sub_JenisTampilan_ALL()
                Case MasaPajak_Rekap
                    Sub_JenisTampilan_REKAP()
                Case Else
                    Sub_JenisTampilan_DETAIL()
            End Select
        End If

    End Sub

    Private Sub cmb_TahunPajak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.SelectedIndexChanged
    End Sub
    Private Sub cmb_TahunPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_TahunPajak_TextChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.TextChanged

        TahunPajak = cmb_TahunPajak.Text
        TahunPajakSebelumIni = TahunPajak - 1

        If TahunPajak > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            lbl_MasaPajak.Enabled = True
            cmb_MasaPajak.Enabled = True
        End If

        If TahunPajak = TahunBukuAktif Then
            TahunPajakSamaDenganTahunBukuAktif = True
        Else
            TahunPajakSamaDenganTahunBukuAktif = False
        End If

        If ProsesLoadingForm = False Then
            If MasaPajak = MasaPajak_Rekap Then Sub_JenisTampilan_REKAP()
            If MasaPajak <> MasaPajak_Rekap Then cmb_MasaPajak.Text = MasaPajak_Rekap
        End If

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Sub Sub_JenisTampilan_ALL()
        JenisTampilan = JenisTampilan_ALL
        JudulForm = "Daftar Transaksi PPh Pasal 26"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_REKAP()
        JenisTampilan = JenisTampilan_REKAP
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 26"
        VisibilitasObjek_REKAP()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_DETAIL()
        JenisTampilan = JenisTampilan_DETAIL
        JudulForm = "Daftar Transaksi PPh Pasal 26"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub VisibilitasObjek_REKAP()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        DataTabelUtama.Columns("Nomor_BPHP").Visible = False
        DataTabelUtama.Columns("Bulan_").Visible = True
        DataTabelUtama.Columns("Bulan_").HeaderText = "Masa Pajak"
        DataTabelUtama.Columns("Tanggal_Transaksi").Visible = False
        DataTabelUtama.Columns("Tanggal_Invoice").Visible = False
        DataTabelUtama.Columns("Nomor_Invoice").Visible = False
        DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = False
        DataTabelUtama.Columns("Nama_Jasa").Visible = False
        DataTabelUtama.Columns("NPWP_").Visible = False
        DataTabelUtama.Columns("Nama_Supplier").Visible = False
        'DataTabelUtama.Columns("DPP_").Visible = False
        DataTabelUtama.Columns("DPP_").HeaderText = "Bruto"
        DataTabelUtama.Columns("Jumlah_Bayar_Pajak").Visible = True
        DataTabelUtama.Columns("Sisa_Hutang_Pajak").Visible = True
        DataTabelUtama.Columns("Kode_Setoran").Visible = False
        DataTabelUtama.Columns("Jenis_Kode_Setoran").Visible = False
        DataTabelUtama.Columns("Keterangan_").Visible = False
    End Sub

    Sub VisibilitasObjek_DETAIL()
        grb_InfoSaldo.Visible = False
        grb_InfoSaldo.Visible = False
        lbl_SaldoAwalBerdasarkanCOA.Visible = False
        txt_SaldoAwalBerdasarkanCOA.Visible = False
        lbl_AJP.Visible = False
        txt_AJP.Visible = False
        grb_Pembayaran.Visible = False
        DataTabelUtama.Columns("Nomor_BPHP").Visible = False
        DataTabelUtama.Columns("Bulan_").Visible = False
        DataTabelUtama.Columns("Tanggal_Transaksi").Visible = True
        DataTabelUtama.Columns("Tanggal_Invoice").Visible = True
        DataTabelUtama.Columns("Nomor_Invoice").Visible = True
        DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = True
        DataTabelUtama.Columns("Nama_Jasa").Visible = True
        DataTabelUtama.Columns("NPWP_").Visible = True
        DataTabelUtama.Columns("Nama_Supplier").Visible = True
        'DataTabelUtama.Columns("DPP_").Visible = True
        DataTabelUtama.Columns("DPP_").HeaderText = "DPP"
        DataTabelUtama.Columns("Jumlah_Bayar_Pajak").Visible = False
        DataTabelUtama.Columns("Sisa_Hutang_Pajak").Visible = False
        DataTabelUtama.Columns("Kode_Setoran").Visible = True
        DataTabelUtama.Columns("Jenis_Kode_Setoran").Visible = True
        DataTabelUtama.Columns("Keterangan_").Visible = True
    End Sub

    Private Sub btn_Input_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        If JenisTampilan = JenisTampilan_REKAP Then frm_InputHutangPPhPasal26.BulanTransaksi = Today.Month
        If JenisTampilan = JenisTampilan_DETAIL Then frm_InputHutangPPhPasal26.BulanTransaksi = MasaPajak_Angka
        frm_InputHutangPPhPasal26.FungsiForm = FungsiForm_TAMBAH
        frm_InputHutangPPhPasal26.ResetForm()
        frm_InputHutangPPhPasal26.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputHutangPPhPasal26.BulanTransaksi = MasaPajak_Angka
        frm_InputHutangPPhPasal26.FungsiForm = FungsiForm_EDIT
        frm_InputHutangPPhPasal26.ResetForm()
        ProsesLoadingForm = True
        frm_InputHutangPPhPasal26.NomorId = NomorID_Terseleksi
        frm_InputHutangPPhPasal26.dtp_TanggalTransaksi.Value = TanggalTransaksi_Terseleksi
        frm_InputHutangPPhPasal26.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        frm_InputHutangPPhPasal26.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        frm_InputHutangPPhPasal26.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        frm_InputHutangPPhPasal26.txt_NamaJasa.Text = NamaJasa_Terseleksi
        frm_InputHutangPPhPasal26.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        frm_InputHutangPPhPasal26.txt_DPP.Text = DPP_Terseleksi
        frm_InputHutangPPhPasal26.txt_PPhPasal26.Text = PPhPasal26_Terseleksi
        frm_InputHutangPPhPasal26.txt_Keterangan.Text = Keterangan_Terseleksi
        ProsesLoadingForm = False
        frm_InputHutangPPhPasal26.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_HutangPajak " &
                              " WHERE Nomor_ID = '" & NomorID_Terseleksi & "'",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

        AksesDatabase_Transaksi(Tutup)

    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If SisaHutang_Terseleksi <= 0 Then
            MsgBox("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim JumlahTagihan As Int64 = 0
        Dim SisaHutang As Int64 = 0
        Dim JumlahBayar
        Dim KodeSetoran = Kosongan
        JumlahBayar = 0
        Select Case True
            Case rdb_KodeSetoran_100.Checked
                If SisaHutang_100_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-100 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_100_Terseleksi
                SisaHutang = SisaHutang_100_Terseleksi
                JumlahBayar = JumlahBayar_100_Terseleksi
                KodeSetoran = KodeSetoran_100
            Case rdb_KodeSetoran_101.Checked
                If SisaHutang_101_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-101 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_101_Terseleksi
                SisaHutang = SisaHutang_101_Terseleksi
                JumlahBayar = JumlahBayar_101_Terseleksi
                KodeSetoran = KodeSetoran_101
            Case rdb_KodeSetoran_102.Checked
                If SisaHutang_102_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-102 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_102_Terseleksi
                SisaHutang = SisaHutang_102_Terseleksi
                JumlahBayar = JumlahBayar_102_Terseleksi
                KodeSetoran = KodeSetoran_102
            Case rdb_KodeSetoran_103.Checked
                If SisaHutang_103_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-103 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_103_Terseleksi
                SisaHutang = SisaHutang_103_Terseleksi
                JumlahBayar = JumlahBayar_103_Terseleksi
                KodeSetoran = KodeSetoran_103
            Case rdb_KodeSetoran_104.Checked
                If SisaHutang_104_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-104 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_104_Terseleksi
                SisaHutang = SisaHutang_104_Terseleksi
                JumlahBayar = JumlahBayar_104_Terseleksi
                KodeSetoran = KodeSetoran_104
            Case rdb_KodeSetoran_105.Checked
                If SisaHutang_105_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 26 Kode-105 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal26_105_Terseleksi
                SisaHutang = SisaHutang_105_Terseleksi
                JumlahBayar = JumlahBayar_105_Terseleksi
                KodeSetoran = KodeSetoran_105
        End Select

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, Kosongan, "Pembayaran " & JenisPajak & " - " & KodeSetoran & " - " & Bulan_Terseleksi, NomorBPHP_Terseleksi,
                                JumlahTagihan, 0, 0, 0, JumlahBayar, SisaHutang,
                                JenisPajak, KodeSetoran, 0, 0, 0,
                                SisaHutang, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangPajak.ResetForm()
        frm_InputPembayaranHutangPajak.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangPajak.JenisPajak = JenisPajak
        frm_InputPembayaranHutangPajak.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangPajak.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangPajak.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NomorIdBayar = NomorIdPembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NPPHP = Referensi_Terseleksi
        IsiValueForm_InputPembayaran()
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe = BarisKe + 1
        Loop
        frm_InputPembayaranHutangPajak.txt_JumlahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = JumlahTerutang - HitungBayar
        frm_InputPembayaranHutangPajak.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranHutangPajak.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangPajak.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangPajak.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

    End Sub

    Dim JumlahTerutang As Int64
    Sub IsiValueForm_InputPembayaran()
        frm_InputPembayaranHutangPajak.txt_NomorBPHP.Text = NomorBPHP_Terseleksi
        frm_InputPembayaranHutangPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        If rdb_KodeSetoran_100.Checked = True Then JumlahTerutang = PPhPasal26_100_Terseleksi
        If rdb_KodeSetoran_101.Checked = True Then JumlahTerutang = PPhPasal26_101_Terseleksi
        If rdb_KodeSetoran_102.Checked = True Then JumlahTerutang = PPhPasal26_102_Terseleksi
        If rdb_KodeSetoran_103.Checked = True Then JumlahTerutang = PPhPasal26_103_Terseleksi
        If rdb_KodeSetoran_104.Checked = True Then JumlahTerutang = PPhPasal26_104_Terseleksi
        If rdb_KodeSetoran_105.Checked = True Then JumlahTerutang = PPhPasal26_105_Terseleksi
        frm_InputPembayaranHutangPajak.txt_JumlahTerutang.Text = JumlahTerutang
        frm_InputPembayaranHutangPajak.KodeSetoran = KodeSetoran_UntukBayar
    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranHutangPajak :
        If StatusKoneksiDatabaseTransaksi = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        'Hapus Data di tbl_PengajuanPembayaranHutangPajak :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangPajak " &
                                  " WHERE Nomor_Pengajuan = '" & Referensi_Terseleksi & "' " &
                                  " AND Nomor_BPHP = '" & NomorBPHP_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        'Hapus Data di tbl_Transaksi (Jurnal) :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                  " WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub rdb_KodeSetoran_100_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_100.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_101_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_101.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_102_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_102.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_103_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_103.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_104_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_104.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_105_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_105.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Sub LogikaKodeSetoran()
        If rdb_KodeSetoran_100.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_100
        If rdb_KodeSetoran_101.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_101
        If rdb_KodeSetoran_102.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_102
        If rdb_KodeSetoran_103.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_103
        If rdb_KodeSetoran_104.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_104
        If rdb_KodeSetoran_105.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_105
        If rdb_KodeSetoran_100.Checked = True _
            Or rdb_KodeSetoran_101.Checked = True _
            Or rdb_KodeSetoran_102.Checked = True _
            Or rdb_KodeSetoran_103.Checked = True _
            Or rdb_KodeSetoran_104.Checked = True _
            Or rdb_KodeSetoran_105.Checked = True _
            Then
            TampilkanDataPembayaran()
        Else
            dgv_DetailBayar.Enabled = False
            KodeSetoran_UntukBayar = KodeSetoran_Non
        End If
    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As EventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak
        frm_DetailPembayaranPajak.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_HapusPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        Referensi_Terseleksi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_HapusPembayaran.Enabled = True
            btn_EditPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        Else
            btn_HapusPembayaran.Enabled = False
            btn_EditPembayaran.Enabled = False
            btn_LihatJurnal.Enabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    Sub TampilkanInfoSaldo()
        If TahunPajak = TahunBukuAktif Then
            grb_InfoSaldo.Visible = True
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_SaldoAwalBerdasarkanCOA.Visible = True
                txt_SaldoAwalBerdasarkanCOA.Visible = True
                lbl_AJP.Visible = True
                txt_AJP.Visible = True
            Else
                lbl_SaldoAwalBerdasarkanCOA.Visible = False
                txt_SaldoAwalBerdasarkanCOA.Visible = False
                lbl_AJP.Visible = False
                txt_AJP.Visible = False
            End If
        Else
            grb_InfoSaldo.Visible = False
            lbl_SaldoAwalBerdasarkanCOA.Visible = False
            txt_SaldoAwalBerdasarkanCOA.Visible = False
            lbl_AJP.Visible = False
            txt_AJP.Visible = False
        End If
    End Sub

    Sub ResetTampilanDataPembayaran()
        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False
        dgv_DetailBayar.Enabled = False
        dgv_DetailBayar.Rows.Clear()
        KodeSetoran_UntukBayar = KodeSetoran_Non
        btn_InputPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
        rdb_KodeSetoran_100.Checked = False
        rdb_KodeSetoran_101.Checked = False
        rdb_KodeSetoran_102.Checked = False
        rdb_KodeSetoran_103.Checked = False
        rdb_KodeSetoran_104.Checked = False
        rdb_KodeSetoran_105.Checked = False
    End Sub

    Sub TampilkanDataPembayaran()

        dgv_DetailBayar.Enabled = True
        btn_InputPembayaran.Enabled = True

        dgv_DetailBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar = 0
        Dim TotalBayar = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP      = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Kode_Setoran    = '" & KodeSetoran_UntukBayar & "' " &
                                      " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                                      " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJV_Pembayaran = dr.Item("Nomor_JV")
                    Else
                        NomorJV_Pembayaran = 0
                    End If
                    dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJV_Pembayaran)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
            PencegahLoopingTahunPajakLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        dgv_DetailBayar.ClearSelection()
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub

    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", Baris_Terseleksi).Value)
        Bulan_Terseleksi = DataTabelUtama.Item("Bulan_", Baris_Terseleksi).Value
        NomorBPHP_Terseleksi = DataTabelUtama.Item("Nomor_BPHP", Baris_Terseleksi).Value
        TanggalTransaksi_Terseleksi = DataTabelUtama.Item("Tanggal_Transaksi", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", Baris_Terseleksi).Value
        NamaJasa_Terseleksi = DataTabelUtama.Item("Nama_Jasa", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
        NPWP_Terseleksi = DataTabelUtama.Item("NPWP_", Baris_Terseleksi).Value
        DPP_Terseleksi = AmbilAngka(DataTabelUtama.Item("DPP_", Baris_Terseleksi).Value)
        PPhPasal26_100_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_100", Baris_Terseleksi).Value)
        PPhPasal26_101_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_101", Baris_Terseleksi).Value)
        PPhPasal26_102_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_102", Baris_Terseleksi).Value)
        PPhPasal26_103_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_103", Baris_Terseleksi).Value)
        PPhPasal26_104_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_104", Baris_Terseleksi).Value)
        PPhPasal26_105_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26_105", Baris_Terseleksi).Value)
        PPhPasal26_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26", Baris_Terseleksi).Value)
        JumlahTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_26", Baris_Terseleksi).Value)
        JumlahBayar_100_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_100", Baris_Terseleksi).Value)
        JumlahBayar_101_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_101", Baris_Terseleksi).Value)
        JumlahBayar_102_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_102", Baris_Terseleksi).Value)
        JumlahBayar_103_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_103", Baris_Terseleksi).Value)
        JumlahBayar_104_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_104", Baris_Terseleksi).Value)
        JumlahBayar_105_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_105", Baris_Terseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Pajak", Baris_Terseleksi).Value)
        SisaHutang_100_Terseleksi = PPhPasal26_100_Terseleksi - JumlahBayar_100_Terseleksi
        SisaHutang_101_Terseleksi = PPhPasal26_101_Terseleksi - JumlahBayar_101_Terseleksi
        SisaHutang_102_Terseleksi = PPhPasal26_102_Terseleksi - JumlahBayar_102_Terseleksi
        SisaHutang_103_Terseleksi = PPhPasal26_103_Terseleksi - JumlahBayar_103_Terseleksi
        SisaHutang_104_Terseleksi = PPhPasal26_104_Terseleksi - JumlahBayar_104_Terseleksi
        SisaHutang_105_Terseleksi = PPhPasal26_105_Terseleksi - JumlahBayar_105_Terseleksi
        SisaHutang_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang_Pajak", Baris_Terseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value

        If JenisTampilan = JenisTampilan_DETAIL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU And NomorID_Terseleksi = 0 Then BersihkanSeleksi()
        End If

        If JenisTampilan = JenisTampilan_REKAP Then
            If JumlahTagihan_Terseleksi > 0 Then
                ResetTampilanDataPembayaran()
                rdb_KodeSetoran_100.Enabled = False
                rdb_KodeSetoran_101.Enabled = False
                rdb_KodeSetoran_102.Enabled = False
                rdb_KodeSetoran_103.Enabled = False
                rdb_KodeSetoran_104.Enabled = False
                rdb_KodeSetoran_105.Enabled = False
                If PPhPasal26_100_Terseleksi > 0 Then rdb_KodeSetoran_100.Enabled = True
                If PPhPasal26_101_Terseleksi > 0 Then rdb_KodeSetoran_101.Enabled = True
                If PPhPasal26_102_Terseleksi > 0 Then rdb_KodeSetoran_102.Enabled = True
                If PPhPasal26_103_Terseleksi > 0 Then rdb_KodeSetoran_103.Enabled = True
                If PPhPasal26_104_Terseleksi > 0 Then rdb_KodeSetoran_104.Enabled = True
                If PPhPasal26_105_Terseleksi > 0 Then rdb_KodeSetoran_105.Enabled = True
            Else
                If Bulan_Terseleksi <> JenisPajak_KetetapanPajak Then BersihkanSeleksi()
                TampilkanInfoSaldo()
            End If
        End If

        If JenisTampilan = JenisTampilan_DETAIL Then
            If NomorUrut_Terseleksi > 0 Then
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
            Else
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
            End If
        End If

    End Sub
    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        If JenisTampilan = JenisTampilan_DETAIL Or JenisTampilan = JenisTampilan_ALL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    If NomorID_Terseleksi = 0 Then
                        btn_Input_Click(sender, e)
                    Else
                        btn_Edit_Click(sender, e)
                    End If
                End If
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    'belum ada codingnya.
                End If
            End If
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                'belum ada codingnya.
            End If
        End If
        If JenisTampilan = JenisTampilan_REKAP And cmb_MasaPajak.Enabled = True Then
            If NomorUrut_Terseleksi <> 0 Then cmb_MasaPajak.Text = DataTabelUtama.Item("Bulan_", Baris_Terseleksi).Value
        End If
        If Bulan_Terseleksi = JenisPajak_KetetapanPajak Then
            frm_BukuPengawasanKetetapanPajak.MdiParent = frm_BOOKU
            frm_BukuPengawasanKetetapanPajak.Show()
            frm_BukuPengawasanKetetapanPajak.Focus()
            usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak
        End If
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanList)
    End Sub
    Private Sub txt_SaldoBerdasarkanList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanList.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub
    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As EventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox(txt_SelisihSaldo)
    End Sub
    Private Sub txt_SelisihSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SelisihSaldo.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_Sesuaikan_Click(sender As Object, e As EventArgs) Handles btn_Sesuaikan.Click

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            Dim NamaAkun_BiayaSelisihPencatatan
            Dim NamaAkun_HutangPPhPasal26
            Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
            PengisianValue_NamaAkun()
            NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
            KodeAkun_Tembak = KodeTautanCOA_HutangPPhPasal26
            PengisianValue_NamaAkun()
            NamaAkun_HutangPPhPasal26 = NamaAkun_Tembak
            frm_InputJurnal.ResetForm()
            frm_InputJurnal.JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
            frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            If JumlahPenyesuaian > 0 Then
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Kosongan)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangPPhPasal26,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal26, "K", Kosongan, JumlahPenyesuaian)
            End If
            If JumlahPenyesuaian < 0 Then
                JumlahPenyesuaian = -(JumlahPenyesuaian)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_HutangPPhPasal26,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Kosongan)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal26, "K", Kosongan, JumlahPenyesuaian)
            End If
            frm_InputJurnal.DataTabelUtama.Item("Debet", 3).Value = JumlahPenyesuaian
            frm_InputJurnal.DataTabelUtama.Item("Kredit", 3).Value = JumlahPenyesuaian
            frm_InputJurnal.lbl_StatusBalance.ForeColor = Color.Green
            frm_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            frm_InputJurnal.dtp_TanggalJurnal.Value = AwalTahunBukuAktif
            frm_InputJurnal.cmb_JenisJurnal.Text = JenisJurnal_AdjusmentSaldoAwal
            BeginInvoke(Sub() frm_InputJurnal.dtp_TanggalJurnal.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.cmb_JenisJurnal.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_TambahTransaksi.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_Reset.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = True)
            BeginInvoke(Sub() frm_InputJurnal.JumlahBarisJurnal = 2)
            BeginInvoke(Sub() MsgBox("Silakan buat Jurnal Penyesuaian (Adjusment)."))
            frm_InputJurnal.ShowDialog()
            If frm_InputJurnal.JurnalTersimpan = True Then
                UpdateNotifikasi()
                TampilkanData()
            End If
        End If

    End Sub

    Public Sub UpdateNotifikasi()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1, " &
                              " Status_Dieksekusi = 1 " &
                              " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGPPHPASAL26 & "' " &
                              " AND Pesan = '" & teks_SilakanSesuaikanSaldo & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        frm_BOOKU.IsiKontenNotifikasi()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoAwalBerdasarkanCOA)
    End Sub
    Private Sub txt_SaldoAwalBerdasarkanCOA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As EventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox(txt_AJP)
    End Sub

    Private Sub txt_AJP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AJP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        KunciUkuranForm(Me, 1320, 630)
    End Sub

End Class