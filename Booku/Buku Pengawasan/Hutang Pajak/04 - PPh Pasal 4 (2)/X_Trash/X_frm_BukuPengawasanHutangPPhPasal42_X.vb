Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanHutangPPhPasal42_X

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
    Dim TanggalTransaksi_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NamaJasa_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim NPWP_Terseleksi
    Dim DPP_Terseleksi
    Dim PPhPasal42_402_Terseleksi
    Dim PPhPasal42_403_Terseleksi
    Dim PPhPasal42_409_Terseleksi
    Dim PPhPasal42_419_Terseleksi
    Dim PPhPasal42_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar_402_Terseleksi
    Dim JumlahBayar_403_Terseleksi
    Dim JumlahBayar_409_Terseleksi
    Dim JumlahBayar_419_Terseleksi
    Dim JumlahBayarPajak_Terseleksi
    Dim SisaHutang_402_Terseleksi
    Dim SisaHutang_403_Terseleksi
    Dim SisaHutang_409_Terseleksi
    Dim SisaHutang_419_Terseleksi
    Dim SisaHutangPajak_Terseleksi
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

    Dim MasaPajak = Nothing
    Dim TahunPajakSebelumIni

    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorID
    Dim NomorBulan
    Dim NomorBPHP
    Dim Bulan = Nothing
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim NPWP
    Dim KodeSupplier
    Dim NamaSupplier
    Dim DPP402
    Dim DPP403
    Dim DPP409
    Dim DPP419
    Dim DPP
    Dim RekapPerBulan_DPP_402
    Dim RekapPerBulan_DPP_403
    Dim RekapPerBulan_DPP_409
    Dim RekapPerBulan_DPP_419
    Dim RekapPerBulan_DPP
    Dim PPhPasal42_402
    Dim PPhPasal42_403
    Dim PPhPasal42_409
    Dim PPhPasal42_419
    Dim PPhPasal42
    Dim RekapPerBulan_PPhPasal42_402
    Dim RekapPerBulan_PPhPasal42_403
    Dim RekapPerBulan_PPhPasal42_409
    Dim RekapPerBulan_PPhPasal42_419
    Dim RekapPerBulan_PPhPasal42
    Dim JumlahTagihan
    Dim TanggalTransaksi
    Dim JumlahBayar_402
    Dim JumlahBayar_403
    Dim JumlahBayar_409
    Dim JumlahBayar_419
    Dim JumlahBayar_PPhPasal42
    Dim SisaHutang_PPhPasal42
    Dim JenisKodeSetoran
    Dim Keterangan

    Dim TotalDPP_402
    Dim TotalDPP_403
    Dim TotalDPP_409
    Dim TotalDPP_419
    Dim TotalDPP
    Dim TotalTagihan_402
    Dim TotalTagihan_403
    Dim TotalTagihan_409
    Dim TotalTagihan_419
    Dim TotalTagihan_PPhPasal42
    Dim TotalBayar_402
    Dim TotalBayar_403
    Dim TotalBayar_409
    Dim TotalBayar_419
    Dim TotalBayar_PPhPasal42
    Dim TotalSisaHutang_402
    Dim TotalSisaHutang_403
    Dim TotalSisaHutang_409
    Dim TotalSisaHutang_419
    Dim TotalSisaHutang_PPhPasal42

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

    Dim JumlahBarisDalamSatuBulan = 0

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        JenisPajak = JenisPajak_PPhPasal42

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
        cmb_MasaPajak.Text = Nothing 'Ini Penting...!!! Jangan dihapus...!!! Agar ketika ada perubahan teks/value pada cmb_TahunPajak tidak mengekseskusi Sub TampilkanData...!!!
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

        TahunHutangPajakTerlama = AmbilTahunTerlama_SisaHutangPajak(JenisPPh_Pasal42)
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

        If MasaPajak = Nothing Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)
        DataTabelUtama.Columns("DPP_402").Visible = False
        DataTabelUtama.Columns("DPP_403").Visible = False
        DataTabelUtama.Columns("DPP_409").Visible = False
        DataTabelUtama.Columns("DPP_419").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_42_402").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_42_403").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_42_409").Visible = False
        DataTabelUtama.Columns("PPh_Pasal_42_419").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_402").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_403").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_409").Visible = False
        DataTabelUtama.Columns("Jumlah_Bayar_419").Visible = False

        'Data Tabel :
        NomorUrut = 0
        NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
        TanggalTransaksi = Nothing
        NomorInvoice = Nothing
        NomorFakturPajak = Nothing
        NamaJasa = Nothing
        NPWP = Nothing
        KodeSupplier = Nothing
        NamaSupplier = Nothing
        DPP = Nothing
        Keterangan = Nothing

        TotalDPP_402 = 0
        TotalDPP_403 = 0
        TotalDPP_409 = 0
        TotalDPP_419 = 0
        TotalDPP = 0

        TotalTagihan_402 = 0
        TotalTagihan_403 = 0
        TotalTagihan_409 = 0
        TotalTagihan_419 = 0
        TotalTagihan_PPhPasal42 = 0

        TotalBayar_402 = 0
        TotalBayar_403 = 0
        TotalBayar_409 = 0
        TotalBayar_419 = 0
        TotalBayar_PPhPasal42 = 0

        TotalSisaHutang_402 = 0
        TotalSisaHutang_403 = 0
        TotalSisaHutang_409 = 0
        TotalSisaHutang_419 = 0
        TotalSisaHutang_PPhPasal42 = 0

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
                NomorBPHP = AwalanBPHP42 & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulan_DPP_402 = 0
                RekapPerBulan_DPP_403 = 0
                RekapPerBulan_DPP_409 = 0
                RekapPerBulan_DPP_419 = 0
                RekapPerBulan_DPP = 0

                RekapPerBulan_PPhPasal42_402 = 0
                RekapPerBulan_PPhPasal42_403 = 0
                RekapPerBulan_PPhPasal42_409 = 0
                RekapPerBulan_PPhPasal42_419 = 0
                RekapPerBulan_PPhPasal42 = 0

                JumlahBayar_402 = 0
                JumlahBayar_403 = 0
                JumlahBayar_409 = 0
                JumlahBayar_419 = 0
                JumlahBayar_PPhPasal42 = 0

                SisaHutang_PPhPasal42 = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika [ Tahun Buku LAMPAU ] atau [ Tahun Buku NORMAL Tapi Tahun Pajaknya Tidak Sama Dengan Tahun Buku Aktif ]:
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Or
                    (JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak <> TahunBukuAktif) _
                    Then
                    SumberData = SumberData_SisaHutangPajak
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    Do While dr.Read
                        DPP = dr.Item("DPP")
                        PPhPasal42 = dr.Item("Jumlah_Hutang")
                        AmbilValue_PerKodeSetoran()
                    Loop
                End If

                'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
                If JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak = TahunBukuAktif Then

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                          " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
                                          " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar                        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
                    If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak
                    BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                    Do While dr.Read
                        PPhPasal42 = dr.Item("PPh_Terutang")
                        If PPhPasal42 > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                      " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                      KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                DPP = PPhPasal42 / (drPEMB.Item("Tarif_PPh") / 100)
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
                        PPhPasal42 = dr.Item("PPh_Terutang")
                        If PPhPasal42 > 0 Then
                            DPP = dr.Item("Jumlah_Dividen")
                            SumberData = JenisJasa_Dividen
                            AmbilValue_PerKodeSetoran()
                        End If
                    Loop

                End If

                'Data Pembayaran :
                If RekapPerBulan_PPhPasal42 > 0 Then
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
                                JumlahBayar_PPhPasal42 += dr.Item("Jumlah_Bayar")
                                Select Case dr.Item("Kode_Setoran")
                                    Case KodeSetoran_402
                                        JumlahBayar_402 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_403
                                        JumlahBayar_403 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_409
                                        JumlahBayar_409 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_419
                                        JumlahBayar_419 += dr.Item("Jumlah_Bayar")
                                End Select
                                If JumlahBayar_PPhPasal42 >= RekapPerBulan_PPhPasal42 Then Exit Do
                            Loop
                            TutupDatabaseTransaksi_Alternatif()
                        End If
                        If JumlahBayar_PPhPasal42 >= RekapPerBulan_PPhPasal42 Then Exit Do
                        PencegahLoopingTahunPajakLampau += 1
                        TahunTelusurPembayaran += 1
                    Loop

                End If

                TotalDPP_402 += AmbilAngka(RekapPerBulan_DPP_402)
                TotalDPP_403 += AmbilAngka(RekapPerBulan_DPP_403)
                TotalDPP_409 += AmbilAngka(RekapPerBulan_DPP_409)
                TotalDPP_419 += AmbilAngka(RekapPerBulan_DPP_419)
                TotalDPP += AmbilAngka(RekapPerBulan_DPP)

                TotalTagihan_402 += AmbilAngka(RekapPerBulan_PPhPasal42_402)
                TotalTagihan_403 += AmbilAngka(RekapPerBulan_PPhPasal42_403)
                TotalTagihan_409 += AmbilAngka(RekapPerBulan_PPhPasal42_409)
                TotalTagihan_419 += AmbilAngka(RekapPerBulan_PPhPasal42_419)
                TotalTagihan_PPhPasal42 += AmbilAngka(RekapPerBulan_PPhPasal42)

                TotalBayar_402 += AmbilAngka(JumlahBayar_402)
                TotalBayar_403 += AmbilAngka(JumlahBayar_403)
                TotalBayar_409 += AmbilAngka(JumlahBayar_409)
                TotalBayar_419 += AmbilAngka(JumlahBayar_419)
                TotalBayar_PPhPasal42 += AmbilAngka(JumlahBayar_PPhPasal42)

                SisaHutang_PPhPasal42 = AmbilAngka(RekapPerBulan_PPhPasal42) - AmbilAngka(JumlahBayar_PPhPasal42)
                TotalSisaHutang_PPhPasal42 += AmbilAngka(SisaHutang_PPhPasal42)

                TambahBaris()

            Loop

            AksesDatabase_Transaksi(Tutup)

            Baris_KetetapanPajak()

            'Baris TOTAL untuk Jenis Tampilan REKAP :
            If TotalTagihan_PPhPasal42 = 0 Then TotalTagihan_PPhPasal42 = StripKosong
            If TotalBayar_PPhPasal42 = 0 Then TotalBayar_PPhPasal42 = StripKosong
            If TotalSisaHutang_PPhPasal42 = 0 Then TotalSisaHutang_PPhPasal42 = StripKosong
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, "T O T A L",
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    TotalDPP_402, TotalDPP_403, TotalDPP_409, TotalDPP_419, TotalDPP,
                                    TotalTagihan_402,
                                    TotalTagihan_403,
                                    TotalTagihan_409,
                                    TotalTagihan_419,
                                    TotalTagihan_PPhPasal42,
                                    TotalBayar_402,
                                    TotalBayar_403,
                                    TotalBayar_409,
                                    TotalBayar_419,
                                    TotalBayar_PPhPasal42,
                                    TotalSisaHutang_PPhPasal42, Nothing)

            If TotalDPP_402 > 0 Then DataTabelUtama.Columns("DPP_402").Visible = True
            If TotalDPP_403 > 0 Then DataTabelUtama.Columns("DPP_403").Visible = True
            If TotalDPP_409 > 0 Then DataTabelUtama.Columns("DPP_409").Visible = True
            If TotalDPP_419 > 0 Then DataTabelUtama.Columns("DPP_419").Visible = True

            If TotalTagihan_402 > 0 Then DataTabelUtama.Columns("PPh_Pasal_42_402").Visible = True
            If TotalTagihan_403 > 0 Then DataTabelUtama.Columns("PPh_Pasal_42_403").Visible = True
            If TotalTagihan_409 > 0 Then DataTabelUtama.Columns("PPh_Pasal_42_409").Visible = True
            If TotalTagihan_419 > 0 Then DataTabelUtama.Columns("PPh_Pasal_42_419").Visible = True

            If TotalBayar_402 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_402").Visible = True
            If TotalBayar_403 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_403").Visible = True
            If TotalBayar_409 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_409").Visible = True
            If TotalBayar_419 > 0 Then DataTabelUtama.Columns("Jumlah_Bayar_419").Visible = True

        End If


        'TAMPILAN DETAIL : ---------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_DETAIL Then

            SumberData = Kosongan

            NomorBulan = KonversiBulanKeNomor_String(MasaPajak)
            Bulan = MasaPajak

            RekapPerBulan_DPP_402 = 0
            RekapPerBulan_DPP_403 = 0
            RekapPerBulan_DPP_409 = 0
            RekapPerBulan_DPP_419 = 0
            RekapPerBulan_DPP = 0

            RekapPerBulan_PPhPasal42_402 = 0
            RekapPerBulan_PPhPasal42_403 = 0
            RekapPerBulan_PPhPasal42_409 = 0
            RekapPerBulan_PPhPasal42_419 = 0
            RekapPerBulan_PPhPasal42 = 0

            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak

            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            If StatusKoneksiDatabaseTransaksi_Alternatif = False Then Return

            'Jika Tahun Pajak = Tahun Buku LAMPAU :
            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then
                SumberData = SumberData_SisaHutangPajak
                cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                      " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                                      " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
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
                    PPhPasal42 = dr.Item("Jumlah_Hutang")
                    AmbilValue_PerKodeSetoran()
                    Keterangan = dr.Item("Keterangan")
                    TambahBaris()
                Loop
            End If


            'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                      " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
                                      " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                      " AND Jumlah_Bayar                        > 0 " &
                                      " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    PPhPasal42 = dr.Item("PPh_Terutang")
                    If PPhPasal42 > 0 Then
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
                            DPP = PPhPasal42 / (drPEMB.Item("Tarif_PPh") / 100)
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
                    PPhPasal42 = dr.Item("PPh_Terutang")
                    If PPhPasal42 > 0 Then
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
                DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Kosongan, Kosongan, Kosongan, Kosongan, "T O T A L   ",
                                        RekapPerBulan_PPhPasal42_402,
                                        RekapPerBulan_PPhPasal42_403,
                                        RekapPerBulan_PPhPasal42_409,
                                        RekapPerBulan_PPhPasal42_419,
                                        RekapPerBulan_PPhPasal42,
                                        Nothing, Nothing, Nothing)
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
                NomorBPHP = AwalanBPHP42 & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulan_DPP_402 = 0
                RekapPerBulan_DPP_403 = 0
                RekapPerBulan_DPP_409 = 0
                RekapPerBulan_DPP_419 = 0
                RekapPerBulan_DPP = 0

                RekapPerBulan_PPhPasal42_402 = 0
                RekapPerBulan_PPhPasal42_403 = 0
                RekapPerBulan_PPhPasal42_409 = 0
                RekapPerBulan_PPhPasal42_419 = 0
                RekapPerBulan_PPhPasal42 = 0
                JumlahBayar_PPhPasal42 = 0
                SisaHutang_PPhPasal42 = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika Tahun Pajak = Tahun Buku LAMPAU :
                If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then
                    SumberData = SumberData_SisaHutangPajak
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
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
                        PPhPasal42 = dr.Item("Jumlah_Hutang")
                        AmbilValue_PerKodeSetoran()
                        Keterangan = dr.Item("Keterangan")
                        TambahBaris()
                    Loop
                End If

                'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
                If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                          " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
                                          " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar                        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPhPasal42 = dr.Item("PPh_Terutang")
                        If PPhPasal42 > 0 Then
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
                                DPP = PPhPasal42 / (drPEMB.Item("Tarif_PPh") / 100)
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
                        PPhPasal42 = dr.Item("PPh_Terutang")
                        If PPhPasal42 > 0 Then
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

                TotalTagihan_PPhPasal42 += RekapPerBulan_PPhPasal42

                If JumlahBarisDalamSatuBulan > 0 Then
                    'Header Bulan :
                    DataTabelUtama.Rows.Insert(DataTabelUtama.RowCount - JumlahBarisDalamSatuBulan, Nothing, Nothing, Nothing, Nothing, Bulan)

                    'Baris REKAP PERBULAN untuk Jenis Tampilan ALL :
                    DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Kosongan, Kosongan, Kosongan, Kosongan, "       Total " & Bulan,
                                        Nothing,
                                        RekapPerBulan_PPhPasal42_402,
                                        RekapPerBulan_PPhPasal42_403,
                                        RekapPerBulan_PPhPasal42_409,
                                        RekapPerBulan_PPhPasal42_419,
                                        RekapPerBulan_PPhPasal42,
                                        Nothing, Nothing, Nothing)
                    DataTabelUtama.Rows.Add()
                End If

            Loop

            TutupDatabaseTransaksi_Alternatif()

            If DataTabelUtama.RowCount > 0 Then
                'Baris TOTAL untuk Jenis Tampilan ALL :
                If TotalTagihan_PPhPasal42 = 0 Then TotalTagihan_PPhPasal42 = StripKosong
                DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Kosongan, Kosongan, Kosongan, Kosongan, "       TOTAL TAGIHAN ",
                                    Nothing,
                                    TotalTagihan_402,
                                    TotalTagihan_403,
                                    TotalTagihan_409,
                                    TotalTagihan_419,
                                    TotalTagihan_PPhPasal42,
                                    Nothing, Nothing, Nothing)
            End If

        End If


        If JenisTampilan = JenisTampilan_ALL Or JenisTampilan = JenisTampilan_REKAP Then

            AksesDatabase_Transaksi(Buka)

            'Hitung Total Tagihan Selama Sebelum Cut Off :
            Dim TotalTagihan_SelamaSebelumCutOff = 0
            cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                  " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                                  KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                TotalTagihan_SelamaSebelumCutOff += dr.Item("Jumlah_Hutang")
            Loop

            'Hitung Total Pembayaran Selama Sebelum Cut Off :
            Dim TotalBayar_SelamaSebelumCutOff = 0
            cmd = New OdbcCommand(" SELECT * FROM X_tbl_PembayaranHutangPajak_X " &
                                  " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                                  KoneksiDatabaseTransaksi)
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

        If JenisTampilan <> JenisTampilan_REKAP And PPhPasal42 = 0 Then Return

        NomorUrut += 1
        JumlahBarisDalamSatuBulan += 1

        If JenisTampilan = JenisTampilan_DETAIL Then JumlahTagihan = PPhPasal42
        If JenisTampilan = JenisTampilan_ALL Then JumlahTagihan = PPhPasal42
        If JenisTampilan = JenisTampilan_REKAP Then
            DPP = RekapPerBulan_DPP
            JumlahTagihan = RekapPerBulan_PPhPasal42
        End If

        If AmbilAngka(RekapPerBulan_DPP_402) = 0 Then RekapPerBulan_DPP_402 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_403) = 0 Then RekapPerBulan_DPP_403 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_409) = 0 Then RekapPerBulan_DPP_409 = StripKosong
        If AmbilAngka(RekapPerBulan_DPP_419) = 0 Then RekapPerBulan_DPP_419 = StripKosong
        If AmbilAngka(DPP) = 0 Then DPP = StripKosong

        If AmbilAngka(RekapPerBulan_PPhPasal42_402) = 0 Then RekapPerBulan_PPhPasal42_402 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal42_403) = 0 Then RekapPerBulan_PPhPasal42_403 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal42_409) = 0 Then RekapPerBulan_PPhPasal42_409 = StripKosong
        If AmbilAngka(RekapPerBulan_PPhPasal42_419) = 0 Then RekapPerBulan_PPhPasal42_419 = StripKosong
        If AmbilAngka(JumlahTagihan) = 0 Then JumlahTagihan = StripKosong

        If AmbilAngka(JumlahBayar_402) = 0 Then JumlahBayar_402 = StripKosong
        If AmbilAngka(JumlahBayar_403) = 0 Then JumlahBayar_403 = StripKosong
        If AmbilAngka(JumlahBayar_409) = 0 Then JumlahBayar_409 = StripKosong
        If AmbilAngka(JumlahBayar_419) = 0 Then JumlahBayar_419 = StripKosong
        If AmbilAngka(JumlahBayar_PPhPasal42) = 0 Then JumlahBayar_PPhPasal42 = StripKosong

        If AmbilAngka(SisaHutang_PPhPasal42) = 0 Then SisaHutang_PPhPasal42 = StripKosong

        DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, Bulan,
                                TanggalTransaksi, TanggalInvoice, NomorInvoice, NomorFakturPajak, NamaJasa, NPWP, KodeSupplier, NamaSupplier,
                                RekapPerBulan_DPP_402,
                                RekapPerBulan_DPP_403,
                                RekapPerBulan_DPP_409,
                                RekapPerBulan_DPP_419,
                                DPP,
                                RekapPerBulan_PPhPasal42_402,
                                RekapPerBulan_PPhPasal42_403,
                                RekapPerBulan_PPhPasal42_409,
                                RekapPerBulan_PPhPasal42_419,
                                JumlahTagihan,
                                JumlahBayar_402,
                                JumlahBayar_403,
                                JumlahBayar_409,
                                JumlahBayar_419,
                                JumlahBayar_PPhPasal42,
                                SisaHutang_PPhPasal42, KodeSetoran_UntukTabel, JenisKodeSetoran, Keterangan)
        If JenisTampilan = JenisTampilan_REKAP Then
            If RekapPerBulan_PPhPasal42 > 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If RekapPerBulan_PPhPasal42 = 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If
        Index_BarisTabel += 1
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
            Case KodeSetoran_402 'Pengalihan Tanah dan/atau Bangunan
                DPP402 = DPP
                RekapPerBulan_DPP_402 = AmbilAngka(RekapPerBulan_DPP_402) + DPP
                PPhPasal42_402 = PPhPasal42
                RekapPerBulan_PPhPasal42_402 = AmbilAngka(RekapPerBulan_PPhPasal42_402) + PPhPasal42_402
            Case KodeSetoran_403 'Sewa Tanah dan/atau Bangunan
                DPP403 = DPP
                RekapPerBulan_DPP_403 = AmbilAngka(RekapPerBulan_DPP_403) + DPP
                PPhPasal42_403 = PPhPasal42
                RekapPerBulan_PPhPasal42_403 = AmbilAngka(RekapPerBulan_PPhPasal42_403) + PPhPasal42_403
            Case KodeSetoran_409 'Jasa Konstruksi
                DPP409 = DPP
                RekapPerBulan_DPP_409 = AmbilAngka(RekapPerBulan_DPP_409) + DPP
                PPhPasal42_409 = PPhPasal42
                RekapPerBulan_PPhPasal42_409 = AmbilAngka(RekapPerBulan_PPhPasal42_409) + PPhPasal42_409
            Case KodeSetoran_419 'Dividen
                DPP419 = DPP
                RekapPerBulan_DPP_419 = AmbilAngka(RekapPerBulan_DPP_419) + DPP
                PPhPasal42_419 = PPhPasal42
                RekapPerBulan_PPhPasal42_419 = AmbilAngka(RekapPerBulan_PPhPasal42_419) + PPhPasal42_419
        End Select

        RekapPerBulan_DPP += DPP
        RekapPerBulan_PPhPasal42 += PPhPasal42

    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        If TahunPajak = TahunBukuAktif Then
            Dim JumlahTagihan_SA = 0
            Dim JumlahBayar_SA = 0
            Dim Tahun_SA = TahunBukuAktif - 1
            AksesDatabase_Transaksi(Buka)
            cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                         " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                                         KoneksiDatabaseTransaksi)
            drTAGIHAN_ExecuteReader()
            Do While drTAGIHAN.Read
                JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Hutang")
            Loop
            AksesDatabase_Transaksi(Tutup)
            Dim TahunTelusurPembayaran = TahunCutOff
            Do While TahunTelusurPembayaran <= TahunBukuKemarin
                TahunBuku_Alternatif = TahunTelusurPembayaran
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmdBAYAR = New OdbcCommand(" SELECT * FROM X_tbl_PembayaranHutangPajak_X " &
                                           " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                                           KoneksiDatabaseTransaksi_Alternatif)
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
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal42 & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal42 & "' " &
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
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal42 & "' ",
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
        cmdBAYAR = New OdbcCommand(" SELECT * FROM X_tbl_PembayaranHutangPajak_X " &
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
        DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, "Ketetapan Pajak",
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing,
                                    JumlahTagihan_KetetapanPajak,
                                    Nothing, Nothing, Nothing, Nothing,
                                    JumlahBayar_KetetapanPajak,
                                    SisaHutang_KetetapanPajak, Nothing)

        TotalTagihan_PPhPasal42 += AmbilAngka(JumlahTagihan_KetetapanPajak)
        TotalBayar_PPhPasal42 += AmbilAngka(JumlahBayar_KetetapanPajak)
        TotalSisaHutang_PPhPasal42 += AmbilAngka(SisaHutang_KetetapanPajak)

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
        JudulForm = "Daftar Transaksi PPh Pasal 4 (2)"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_REKAP()
        JenisTampilan = JenisTampilan_REKAP
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 4 (2)"
        VisibilitasObjek_REKAP()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_DETAIL()
        JenisTampilan = JenisTampilan_DETAIL
        JudulForm = "Daftar Transaksi PPh Pasal 4 (2)"
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

        If JenisTampilan = JenisTampilan_REKAP Then frm_InputHutangPPhPasal42.BulanTransaksi = Today.Month
        If JenisTampilan = JenisTampilan_DETAIL Then frm_InputHutangPPhPasal42.BulanTransaksi = MasaPajak_Angka
        frm_InputHutangPPhPasal42.FungsiForm = FungsiForm_TAMBAH
        frm_InputHutangPPhPasal42.ResetForm()
        frm_InputHutangPPhPasal42.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputHutangPPhPasal42.BulanTransaksi = MasaPajak_Angka
        frm_InputHutangPPhPasal42.FungsiForm = FungsiForm_EDIT
        frm_InputHutangPPhPasal42.ResetForm()
        ProsesLoadingForm = True
        frm_InputHutangPPhPasal42.NomorId = NomorID_Terseleksi
        frm_InputHutangPPhPasal42.dtp_TanggalTransaksi.Value = TanggalTransaksi_Terseleksi
        frm_InputHutangPPhPasal42.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        frm_InputHutangPPhPasal42.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        frm_InputHutangPPhPasal42.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        frm_InputHutangPPhPasal42.txt_NamaJasa.Text = NamaJasa_Terseleksi
        frm_InputHutangPPhPasal42.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        frm_InputHutangPPhPasal42.txt_DPP.Text = DPP_Terseleksi
        frm_InputHutangPPhPasal42.txt_PPhPasal42.Text = PPhPasal42_Terseleksi
        frm_InputHutangPPhPasal42.txt_Keterangan.Text = Keterangan_Terseleksi
        ProsesLoadingForm = False
        frm_InputHutangPPhPasal42.ShowDialog()

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

        If SisaHutangPajak_Terseleksi <= 0 Then
            MsgBox("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim JumlahTagihan As Int64 = 0
        Dim SisaHutang As Int64 = 0
        Dim JumlahBayar
        Dim KodeSetoran = Kosongan
        JumlahBayar = 0
        Select Case True
            Case rdb_KodeSetoran_402.Checked
                If SisaHutang_402_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-402 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal42_402_Terseleksi
                SisaHutang = SisaHutang_402_Terseleksi
                JumlahBayar = JumlahBayar_402_Terseleksi
                KodeSetoran = KodeSetoran_402
            Case rdb_KodeSetoran_403.Checked
                If SisaHutang_403_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-403 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal42_403_Terseleksi
                SisaHutang = SisaHutang_403_Terseleksi
                JumlahBayar = JumlahBayar_403_Terseleksi
                KodeSetoran = KodeSetoran_403
            Case rdb_KodeSetoran_409.Checked
                If SisaHutang_409_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-409 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal42_409_Terseleksi
                SisaHutang = SisaHutang_409_Terseleksi
                JumlahBayar = JumlahBayar_409_Terseleksi
                KodeSetoran = KodeSetoran_409
            Case rdb_KodeSetoran_419.Checked
                If SisaHutang_419_Terseleksi <= 0 Then
                    MsgBox("Hutang PPh Pasal 23 Kode-419 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPhPasal42_419_Terseleksi
                SisaHutang = SisaHutang_419_Terseleksi
                JumlahBayar = JumlahBayar_419_Terseleksi
                KodeSetoran = KodeSetoran_419
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
        frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = JumlahTagihan_Terseleksi - HitungBayar
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
        If rdb_KodeSetoran_402.Checked = True Then JumlahTerutang = PPhPasal42_402_Terseleksi
        If rdb_KodeSetoran_403.Checked = True Then JumlahTerutang = PPhPasal42_403_Terseleksi
        If rdb_KodeSetoran_409.Checked = True Then JumlahTerutang = PPhPasal42_409_Terseleksi
        If rdb_KodeSetoran_419.Checked = True Then JumlahTerutang = PPhPasal42_419_Terseleksi
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

        'Hapus Data di X_tbl_PembayaranHutangPajak_X :
        If StatusKoneksiDatabaseTransaksi = True Then
            cmd = New OdbcCommand(" DELETE FROM X_tbl_PembayaranHutangPajak_X " &
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

    Private Sub rdb_KodeSetoran_402_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_402.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_403_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_403.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_409_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_409.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_419_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_KodeSetoran_419.CheckedChanged
        LogikaKodeSetoran()
    End Sub
    Sub LogikaKodeSetoran()
        If rdb_KodeSetoran_402.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_402
        If rdb_KodeSetoran_403.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_403
        If rdb_KodeSetoran_409.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_409
        If rdb_KodeSetoran_419.Checked = True Then KodeSetoran_UntukBayar = KodeSetoran_419
        If rdb_KodeSetoran_402.Checked = True _
            Or rdb_KodeSetoran_403.Checked = True _
            Or rdb_KodeSetoran_409.Checked = True _
            Or rdb_KodeSetoran_419.Checked = True _
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
        rdb_KodeSetoran_402.Checked = False
        rdb_KodeSetoran_403.Checked = False
        rdb_KodeSetoran_409.Checked = False
        rdb_KodeSetoran_419.Checked = False
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
        PPhPasal42_402_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42_402", Baris_Terseleksi).Value)
        PPhPasal42_403_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42_403", Baris_Terseleksi).Value)
        PPhPasal42_409_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42_409", Baris_Terseleksi).Value)
        PPhPasal42_419_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42_419", Baris_Terseleksi).Value)
        PPhPasal42_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42", Baris_Terseleksi).Value)
        JumlahTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Pasal_42", Baris_Terseleksi).Value)
        JumlahBayar_402_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_402", Baris_Terseleksi).Value)
        JumlahBayar_403_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_403", Baris_Terseleksi).Value)
        JumlahBayar_409_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_409", Baris_Terseleksi).Value)
        JumlahBayar_419_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_419", Baris_Terseleksi).Value)
        JumlahBayarPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Pajak", Baris_Terseleksi).Value)
        SisaHutang_402_Terseleksi = PPhPasal42_402_Terseleksi - JumlahBayar_402_Terseleksi
        SisaHutang_403_Terseleksi = PPhPasal42_403_Terseleksi - JumlahBayar_403_Terseleksi
        SisaHutang_409_Terseleksi = PPhPasal42_409_Terseleksi - JumlahBayar_409_Terseleksi
        SisaHutang_419_Terseleksi = PPhPasal42_419_Terseleksi - JumlahBayar_419_Terseleksi
        SisaHutangPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang_Pajak", Baris_Terseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value

        If JenisTampilan = JenisTampilan_DETAIL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU And NomorID_Terseleksi = 0 Then BersihkanSeleksi()
        End If

        If JenisTampilan = JenisTampilan_REKAP Then
            If JumlahTagihan_Terseleksi > 0 Then
                ResetTampilanDataPembayaran()
                rdb_KodeSetoran_402.Enabled = False
                rdb_KodeSetoran_403.Enabled = False
                rdb_KodeSetoran_409.Enabled = False
                rdb_KodeSetoran_419.Enabled = False
                If PPhPasal42_402_Terseleksi > 0 Then rdb_KodeSetoran_402.Enabled = True
                If PPhPasal42_403_Terseleksi > 0 Then rdb_KodeSetoran_403.Enabled = True
                If PPhPasal42_409_Terseleksi > 0 Then rdb_KodeSetoran_409.Enabled = True
                If PPhPasal42_419_Terseleksi > 0 Then rdb_KodeSetoran_419.Enabled = True
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
            Dim NamaAkun_HutangPPhPasal42
            Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
            PengisianValue_NamaAkun()
            NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
            KodeAkun_Tembak = KodeTautanCOA_HutangPPhPasal42
            PengisianValue_NamaAkun()
            NamaAkun_HutangPPhPasal42 = NamaAkun_Tembak
            frm_InputJurnal.ResetForm()
            frm_InputJurnal.JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
            frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            If JumlahPenyesuaian > 0 Then
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangPPhPasal42,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal42, "K", Nothing, JumlahPenyesuaian)
            End If
            If JumlahPenyesuaian < 0 Then
                JumlahPenyesuaian = -(JumlahPenyesuaian)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_HutangPPhPasal42,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal42, "K", Nothing, JumlahPenyesuaian)
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
                              " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGPPHPASAL42 & "' " &
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