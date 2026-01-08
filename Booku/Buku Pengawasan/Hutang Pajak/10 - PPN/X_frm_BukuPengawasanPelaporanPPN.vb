Imports System.Data.Odbc
Imports bcomm

Public Class X_frm_BukuPengawasanPelaporanPPN


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
    Dim NomorBPHP_Terseleksi
    Dim Bulan_Terseleksi
    Dim TanggalLapor_Terseleksi
    Dim NomorIDLapor_Terseleksi
    Dim NP_Lapor_Terseleksi
    Dim PPNNKL_Terseleksi
    Dim SelisihPembetulanPPNNKL_Terseleksi

    Dim JumlahTagihanPajak_Terseleksi
    Dim JumlahBayarPajak_Terseleksi
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

    Dim MasaPajak = Kosongan
    Dim TahunPajakSebelumIni

    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorID
    Dim NomorBulan
    Dim NomorBPHP
    Dim NamaBulan = Kosongan
    Dim TanggalLapor
    Dim NomorIDLapor
    Dim TWTL_Lapor
    Dim NP_Lapor
    Dim ReturPenjualan
    Dim PajakKeluaran_Dibayar As Int64
    Dim PajakKeluaran_Dipungut As Int64
    Dim PajakKeluaran_TidakDipungut As Int64
    Dim PajakKeluaran_Retur
    Dim PajakKeluaran_Jumlah As Int64
    Dim ReturPembelian
    Dim PajakMasukan_Impor As Int64
    Dim PajakMasukan_DalamNegeri As Int64
    Dim PajakMasukan_KompensasiSebelumnya As Int64
    Dim PajakMasukan_KompensasiPembetulan As Int64
    Dim PajakMasukan_Retur
    Dim PajakMasukan_Jumlah As Int64
    Dim PPNNKL As Int64
    Dim SelisihPembetulan_PPNNKL As Int64
    Dim KompensasiKe
    Dim PPNTidakDapatDikreditkan As Int64
    Dim JumlahTagihan_PPN
    Dim JumlahBayar_PPN
    Dim SisaHutang_PPN
    Dim PeredaranUsaha_Lokal As Int64
    Dim PeredaranUsaha_Ekspor As Int64
    Dim PeredaranUsaha_Jumlah As Int64
    Dim Keterangan

    'Variabel Pembetulan :
    Dim Pembetulan_ReturPenjualan As Int64
    Dim Pembetulan_PajakKeluaran_Dibayar As Int64
    Dim Pembetulan_PajakKeluaran_Dipungut As Int64
    Dim Pembetulan_PajakKeluaran_TidakDipungut As Int64
    Dim Pembetulan_PajakKeluaran_Retur As Int64
    Dim Pembetulan_PajakKeluaran_Jumlah As Int64
    Dim Pembetulan_PeredaranUsaha_Lokal As Int64
    Dim Pembetulan_PeredaranUsaha_Ekspor As Int64
    Dim Pembetulan_PeredaranUsaha_Jumlah As Int64
    Dim Pembetulan_PajakMasukan_Impor As Int64
    Dim Pembetulan_PajakMasukan_DalamNegeri As Int64
    Dim Pembetulan_PajakMasukan_KompensasiSebelumnya As Int64
    Dim Pembetulan_PajakMasukan_KompensasiPembetulan As Int64
    Dim Pembetulan_PajakMasukan_Retur As Int64
    Dim Pembetulan_PajakMasukan_Jumlah As Int64
    Dim Pembetulan_PPNNKL As Int64

    'Variabel Selisih Pembetulan :
    Dim SelisihPembetulan_ReturPenjualan As Int64
    Dim SelisihPembetulan_PajakKeluaran_Dibayar As Int64
    Dim SelisihPembetulan_PajakKeluaran_Dipungut As Int64
    Dim SelisihPembetulan_PajakKeluaran_TidakDipungut As Int64
    Dim SelisihPembetulan_PajakKeluaran_Retur As Int64
    Dim SelisihPembetulan_PajakKeluaran_Jumlah As Int64
    Dim SelisihPembetulan_PeredaranUsaha_Lokal As Int64
    Dim SelisihPembetulan_PeredaranUsaha_Ekspor As Int64
    Dim SelisihPembetulan_PeredaranUsaha_Jumlah As Int64
    Dim SelisihPembetulan_PajakMasukan_Impor As Int64
    Dim SelisihPembetulan_PajakMasukan_DalamNegeri As Int64
    Dim SelisihPembetulan_PajakMasukan_KompensasiSebelumnya As Int64
    Dim SelisihPembetulan_PajakMasukan_KompensasiPembetulan As Int64
    Dim SelisihPembetulan_PajakMasukan_Retur As Int64
    Dim SelisihPembetulan_PajakMasukan_Jumlah As Int64

    'Variabel Total :
    Dim Total_ReturPenjualan
    Dim Total_PajakKeluaran_Retur
    Dim Total_PeredaranUsaha_Lokal
    Dim Total_PeredaranUsaha_Ekspor
    Dim Total_PeredaranUsaha_Jumlah
    Dim Total_PajakKeluaran_Dibayar
    Dim Total_PajakKeluaran_Dipungut
    Dim Total_PajakKeluaran_TidakDipungut
    Dim Total_PajakKeluaran_Jumlah
    Dim Total_ReturPembelian
    Dim Total_PajakMasukan_Retur
    Dim Total_PajakMasukan_Impor
    Dim Total_PajakMasukan_DalamNegeri
    Dim Total_PajakMasukan_KompSebelumnya
    Dim Total_PajakMasukan_KompPembetulan
    Dim Total_PajakMasukan_Jumlah
    Dim Total_PPNNKL
    Dim Total_SelisihPembetulan_PeredaranUsaha_Jumlah
    Dim Total_PPNTidakDapatDikreditkan
    Dim Total_TagihanPPN = 0
    Dim Total_JumlahBayarPPN = 0
    Dim Total_SisaHutangPPN = 0

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        JenisPajak = JenisPajak_PPN

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_LihatJurnal.Visible = False
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.Visible = True
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

        'TahunHutangPajakTerlama = AmbilTahunTerlama("tbl_SisaHutangPPN", "Tanggal_Transaksi")
        TahunHutangPajakTerlama = TahunBukuAktif - 1 '(Sementara begini dulu, karena belum ditentukan sistemnya).
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.Text = TahunPajak

    End Sub

    Sub TampilkanData()

        'Judul Halaman :
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        If MasaPajak = Nothing Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0
        NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
        Keterangan = Kosongan
        Total_TagihanPPN = 0
        Total_JumlahBayarPPN = 0
        Total_SisaHutangPPN = 0


        'TAMPILAN REKAP : ---------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_REKAP Then

            Index_BarisTabel = 0
            NomorBulan = 0

            TanggalLapor = Kosongan '(Default : Kosongan)

            Total_ReturPenjualan = 0
            Total_PeredaranUsaha_Lokal = 0
            Total_PeredaranUsaha_Ekspor = 0
            Total_PeredaranUsaha_Jumlah = 0

            Total_PajakKeluaran_Dibayar = 0
            Total_PajakKeluaran_Dipungut = 0
            Total_PajakKeluaran_TidakDipungut = 0
            Total_PajakKeluaran_Retur = 0
            Total_PajakKeluaran_Jumlah = 0

            Total_ReturPembelian = 0

            Total_PajakMasukan_Impor = 0
            Total_PajakMasukan_DalamNegeri = 0
            Total_PajakMasukan_KompSebelumnya = 0
            Total_PajakMasukan_KompPembetulan = 0
            Total_PajakMasukan_Retur = 0
            Total_PajakMasukan_Jumlah = 0

            Total_PPNNKL = 0
            Total_SelisihPembetulan_PeredaranUsaha_Jumlah = 0
            Total_PPNTidakDapatDikreditkan = 0

            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabaseTransaksi = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                NomorBulan = AmbilAngka(NomorBulan) + 1
                NamaBulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHPPN & TahunPajak & "-" & NomorBulan.ToString

                ReturPenjualan = 0
                PeredaranUsaha_Lokal = 0
                PeredaranUsaha_Ekspor = 0

                PajakKeluaran_Dibayar = 0
                PajakKeluaran_Dipungut = 0
                PajakKeluaran_TidakDipungut = 0
                PajakKeluaran_Retur = 0

                ReturPembelian = 0

                PajakMasukan_Impor = 0
                PajakMasukan_DalamNegeri = 0
                PajakMasukan_KompensasiSebelumnya = 0
                PajakMasukan_KompensasiPembetulan = 0
                PajakMasukan_Retur = 0

                PajakMasukan_KompensasiSebelumnya = 0
                If NomorBulan = 1 Then
                    PajakMasukan_KompensasiSebelumnya = 0
                    'Nanti dibikin coding, bahwa [PPNNKL_Sebelumnya untuk Januari = PPNNKL Bulan Desember Tahun Sebelumnya].      <--- PERHATIAN....!!!!!!!!!!!!!!!!
                    'Nilau minus-nya dikonversi menjadi positif.
                    'Sementara begini dulu.
                Else
                    If PPNNKL < 0 Then PajakMasukan_KompensasiSebelumnya = 0 - PPNNKL
                End If

                PPNNKL = 0
                PPNTidakDapatDikreditkan = 0

                Pembetulan_PeredaranUsaha_Lokal = 0
                Pembetulan_PeredaranUsaha_Ekspor = 0
                Pembetulan_PeredaranUsaha_Jumlah = 0

                Pembetulan_PajakKeluaran_Dibayar = 0
                Pembetulan_PajakKeluaran_Dipungut = 0
                Pembetulan_PajakKeluaran_TidakDipungut = 0

                Pembetulan_PajakMasukan_Impor = 0
                Pembetulan_PajakMasukan_DalamNegeri = 0
                Pembetulan_PajakMasukan_KompensasiSebelumnya = 0
                Pembetulan_PajakMasukan_KompensasiPembetulan = 0

                'SelisihPembetulan_PajakKeluaran_Dibayar = 0
                'SelisihPembetulan_PajakKeluaran_Dipungut = 0
                'SelisihPembetulan_PajakKeluaran_TidakDipungut = 0
                'SelisihPembetulan_PajakKeluaran_Jumlah = 0
                'SelisihPembetulan_PeredaranUsaha_Jumlah = 0

                'SelisihPembetulan_PajakMasukan_Impor = 0
                'SelisihPembetulan_PajakMasukan_DalamNegeri = 0
                'SelisihPembetulan_PajakMasukan_KompensasiSebelumnya = 0
                'SelisihPembetulan_PajakMasukan_KompensasiPembetulan = 0

                JumlahBayar_PPN = 0
                SisaHutang_PPN = 0

                If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                    NomorBulan = "0" & NomorBulan.ToString
                Else
                    NomorBulan = NomorBulan.ToString
                End If

                'Jika [ Tahun Buku LAMPAU ] atau [ Tahun Buku NORMAL Tapi Tahun Pajaknya Tidak Sama Dengan Tahun Buku Aktif ]:
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Or
                    (JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak <> TahunBukuAktif) _
                    Then _
                    'cmd = New OdbcCommand(" SELECT PPh_Pasal_23 FROM tbl_SisaHutangPPN " &
                    '                  " WHERE DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                    '                  KoneksiDatabaseTransaksi)
                    'dr_ExecuteReader()
                    'Do While dr.Read
                    '    RekapPerBulan_PPN += dr.Item("PPh_Pasal_23")
                    'Loop
                End If

                'Jika [ Tahun Buku NORMAL ] dan [ Tahun Pajak = Tahun Buku Aktif ] :
                If JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak = TahunBukuAktif Then

                    Dim NomorInvoice
                    Dim NP_Invoice
                    Dim NomorInvoice_Sebelumnya
                    Dim PeredaranUsahaLokal_PerTransaksi As Int64
                    Dim PeredaranUsahaEkspor_PerTransaksi As Int64

                    Dim NomorInvoicePembetulan
                    Dim Pembetulan_PeredaranUsahaLokal_PerTransaksi As Int64
                    Dim Pembetulan_PeredaranUsahaEkspor_PerTransaksi As Int64

                    Dim TarifPPNPenjualan As Decimal = 11 / 100

                    'Data DPP (Peredaran Usaha ) & Pajak Keluaran :
                    '----------------------------------------------
                    Pembetulan_PeredaranUsaha_Lokal = 0
                    Pembetulan_PeredaranUsaha_Ekspor = 0
                    Pembetulan_PeredaranUsaha_Jumlah = 0

                    Pembetulan_PajakKeluaran_Dibayar = 0
                    Pembetulan_PajakKeluaran_Dipungut = 0
                    Pembetulan_PajakKeluaran_TidakDipungut = 0

                    cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                          " WHERE Nomor_JV > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                    KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    NomorInvoice_Sebelumnya = Kosongan
                    Do While dr.Read
                        NomorInvoice = dr.Item("Nomor_invoice")
                        NP_Invoice = dr.Item("N_P")
                        Dim NomorInvoice_Pembetulan = Kosongan
                        Dim NP_Pembetulan = Kosongan
                        If NP_Invoice = "N" Then
                            NomorInvoice_Pembetulan = NomorInvoice & "-P1"
                        Else
                            Dim PembetulanKe = AmbilAngka(NP_Invoice)
                            NP_Pembetulan = "P" & (PembetulanKe + 1)
                            NomorInvoice_Pembetulan = Microsoft.VisualBasic.Replace(NomorInvoice, NP_Invoice, NP_Pembetulan)
                        End If
                        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                                     " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' " &
                                                     " AND Nomor_JV > 0 ", KoneksiDatabaseTransaksi)
                        drTELUSUR_ExecuteReader()
                        drTELUSUR.Read()
                        If drTELUSUR.HasRows Then Continue Do
                        If NomorInvoice <> NomorInvoice_Sebelumnya Then
                            '---------------------------------------------------------------------
                            PeredaranUsahaLokal_PerTransaksi = (dr.Item("Dasar_Pengenaan_Pajak"))
                            PeredaranUsahaEkspor_PerTransaksi = 0 'Ini Belum dibikin codingnya. Nanti harus dibikin....!!!!!!
                            Select Case dr.Item("Perlakuan_PPN")
                                Case PerlakuanPPN_Dibayar
                                    PajakKeluaran_Dibayar += dr.Item("PPN")
                                Case PerlakuanPPN_Dipungut
                                    PajakKeluaran_Dipungut += dr.Item("PPN")
                                Case PerlakuanPPN_TidakDipungut
                                    PajakKeluaran_TidakDipungut += dr.Item("PPN")
                            End Select
                            PeredaranUsaha_Lokal += PeredaranUsahaLokal_PerTransaksi
                            PeredaranUsaha_Ekspor += PeredaranUsahaEkspor_PerTransaksi
                            '---------------------------------------------------------------------
                            NomorInvoicePembetulan = NomorInvoice & "-P1"
                            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                                         " WHERE Nomor_JV > 0 " &
                                                         " AND Nomor_Invoice = '" & NomorInvoicePembetulan & "' ",
                                                         KoneksiDatabaseTransaksi)
                            drTELUSUR_ExecuteReader()
                            drTELUSUR.Read()
                            If drTELUSUR.HasRows Then
                                Pembetulan_PeredaranUsahaLokal_PerTransaksi = (drTELUSUR.Item("Dasar_Pengenaan_Pajak"))
                                Pembetulan_PeredaranUsahaEkspor_PerTransaksi = 0 'Ini Belum dibikin codingnya. Nanti harus dibikin....!!!!!!
                                Select Case drTELUSUR.Item("Perlakuan_PPN")
                                    Case PerlakuanPPN_Dibayar
                                        Pembetulan_PajakKeluaran_Dibayar += drTELUSUR.Item("PPN")
                                    Case PerlakuanPPN_Dipungut
                                        Pembetulan_PajakKeluaran_Dipungut += drTELUSUR.Item("PPN")
                                    Case PerlakuanPPN_TidakDipungut
                                        Pembetulan_PajakKeluaran_TidakDipungut += drTELUSUR.Item("PPN")
                                End Select
                                Pembetulan_PeredaranUsaha_Lokal += Pembetulan_PeredaranUsahaLokal_PerTransaksi
                                Pembetulan_PeredaranUsaha_Ekspor += Pembetulan_PeredaranUsahaEkspor_PerTransaksi
                            Else
                                Select Case dr.Item("Perlakuan_PPN")
                                    Case PerlakuanPPN_Dibayar
                                        Pembetulan_PajakKeluaran_Dibayar += dr.Item("PPN")
                                    Case PerlakuanPPN_Dipungut
                                        Pembetulan_PajakKeluaran_Dipungut += dr.Item("PPN")
                                    Case PerlakuanPPN_TidakDipungut
                                        Pembetulan_PajakKeluaran_TidakDipungut += dr.Item("PPN")
                                End Select
                                Pembetulan_PeredaranUsaha_Lokal += PeredaranUsahaLokal_PerTransaksi
                                Pembetulan_PeredaranUsaha_Ekspor += PeredaranUsahaEkspor_PerTransaksi
                            End If
                        End If
                        NomorInvoice_Sebelumnya = NomorInvoice
                    Loop

                    'Data Retur Penjualan :
                    '----------------------
                    cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Retur " &
                                          " WHERE Nomor_JV > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Retur, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    Do While dr.Read
                        ReturPenjualan += dr.Item("Total_Harga_Per_Item")
                        PajakKeluaran_Retur += (TarifPPNPenjualan * dr.Item("Total_Harga_Per_Item"))
                    Loop

                    PeredaranUsaha_Jumlah = 0 _
                        + PeredaranUsaha_Lokal _
                        + PeredaranUsaha_Ekspor _
                        - ReturPenjualan

                    PajakKeluaran_Jumlah = 0 _
                        + PajakKeluaran_Dibayar _
                        + PajakKeluaran_Dipungut _
                        + PajakKeluaran_TidakDipungut _
                        - PajakKeluaran_Retur

                    Pembetulan_ReturPenjualan = ReturPenjualan             '\ Sementara begini dulu.
                    Pembetulan_PajakKeluaran_Retur = PajakKeluaran_Retur   '/ Ada kemungkinan nanti diganti codingnya.

                    Pembetulan_PeredaranUsaha_Jumlah = 0 _
                        + Pembetulan_PeredaranUsaha_Lokal _
                        + Pembetulan_PeredaranUsaha_Ekspor _
                        - Pembetulan_ReturPenjualan

                    Pembetulan_PajakKeluaran_Jumlah = 0 _
                        + Pembetulan_PajakKeluaran_Dibayar _
                        + Pembetulan_PajakKeluaran_Dipungut _
                        + Pembetulan_PajakKeluaran_TidakDipungut _
                        - Pembetulan_PajakKeluaran_Retur


                    'Data Untuk Pajak Masukan / Pembelian :
                    '--------------------------------------
                    cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                          " WHERE Nomor_JV > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi)
                    dr_ExecuteReader()
                    NomorInvoice_Sebelumnya = Kosongan
                    Do While dr.Read
                        NomorInvoice = dr.Item("Nomor_invoice")
                        If NomorInvoice <> NomorInvoice_Sebelumnya Then
                            If dr.Item("PPN_Dikreditkan") = Keterangan_Ya Then
                                PajakMasukan_DalamNegeri += dr.Item("PPN")
                            Else
                                PPNTidakDapatDikreditkan += dr.Item("PPN")
                            End If
                        End If
                        NomorInvoice_Sebelumnya = NomorInvoice
                    Loop

                    'Data Retur Pembelian :
                    '----------------------
                    '(Belum Bikin)


                    IsiValue_KompensasiPembetulan()

                    PajakMasukan_Jumlah = 0 _
                        + PajakMasukan_Impor _
                        + PajakMasukan_DalamNegeri _
                        - PajakMasukan_Retur _
                        + PajakMasukan_KompensasiSebelumnya _
                        + PajakMasukan_KompensasiPembetulan

                    'PPN NKL :
                    '---------
                    PPNNKL = PajakKeluaran_Dibayar - PajakKeluaran_Retur - PajakMasukan_Jumlah
                    Pembetulan_PPNNKL = Pembetulan_PajakKeluaran_Dibayar - PajakKeluaran_Retur - Pembetulan_PajakMasukan_Jumlah
                    SelisihPembetulan_PPNNKL = Pembetulan_PPNNKL - PPNNKL

                    Select Case PPNNKL
                        Case < 0
                            Keterangan = "Lebih Bayar"
                        Case 0
                            Keterangan = "Nihil"
                        Case > 0
                            Keterangan = "Kurang Bayar"
                    End Select

                    'Total :
                    '-------
                    Total_PajakKeluaran_Dibayar += PajakKeluaran_Dibayar
                    Total_PajakKeluaran_Dipungut += PajakKeluaran_Dipungut
                    Total_PajakKeluaran_TidakDipungut += PajakKeluaran_TidakDipungut
                    Total_PajakKeluaran_Retur += PajakKeluaran_Retur
                    Total_PajakKeluaran_Jumlah += PajakKeluaran_Jumlah

                    Total_PajakMasukan_Impor += PajakMasukan_Impor
                    Total_PajakMasukan_DalamNegeri += PajakMasukan_DalamNegeri
                    Total_PajakMasukan_KompSebelumnya += PajakMasukan_KompensasiSebelumnya
                    Total_PajakMasukan_KompPembetulan += PajakMasukan_KompensasiPembetulan
                    Total_PajakMasukan_Retur += PajakMasukan_Retur
                    Total_PajakMasukan_Jumlah += PajakMasukan_Jumlah

                    Total_PeredaranUsaha_Lokal += PeredaranUsaha_Lokal
                    Total_PeredaranUsaha_Ekspor += PeredaranUsaha_Ekspor
                    Total_PeredaranUsaha_Jumlah += PeredaranUsaha_Jumlah

                    Total_PPNNKL += PPNNKL
                    Total_PPNTidakDapatDikreditkan += PPNTidakDapatDikreditkan

                End If

                'Data Pembayaran :
                '-----------------
                If PPNNKL > 0 Then
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
                                JumlahBayar_PPN += dr.Item("Jumlah_Bayar")
                                If JumlahBayar_PPN >= PPNNKL Then Exit Do
                            Loop
                            TutupDatabaseTransaksi_Alternatif()
                        End If
                        If JumlahBayar_PPN >= PPNNKL Then Exit Do
                        PencegahLoopingTahunPajakLampau += 1
                        TahunTelusurPembayaran += 1
                    Loop
                    SisaHutang_PPN = AmbilAngka(PPNNKL) - AmbilAngka(JumlahBayar_PPN)
                    Total_TagihanPPN += AmbilAngka(PPNNKL)
                    Total_JumlahBayarPPN += AmbilAngka(JumlahBayar_PPN)
                    Total_SisaHutangPPN += AmbilAngka(SisaHutang_PPN)
                End If

                TambahBaris_Normal()
                If PajakKeluaran_Dibayar <> Pembetulan_PajakKeluaran_Dibayar _
                    Or PajakKeluaran_Dipungut <> Pembetulan_PajakKeluaran_Dipungut _
                    Or PajakKeluaran_TidakDipungut <> Pembetulan_PajakKeluaran_TidakDipungut _
                    Or PajakKeluaran_Retur <> Pembetulan_PajakKeluaran_Retur _
                    Or PeredaranUsaha_Lokal <> Pembetulan_PeredaranUsaha_Lokal _
                    Or PeredaranUsaha_Ekspor <> Pembetulan_PeredaranUsaha_Ekspor _
                    Or PajakMasukan_DalamNegeri <> Pembetulan_PajakMasukan_DalamNegeri _
                    Or PajakMasukan_Impor <> Pembetulan_PajakMasukan_Impor _
                    Then
                    TambahBaris_Pembetulan()
                End If

            Loop

            AksesDatabase_Transaksi(Tutup)

            Baris_KetetapanPajak()

            'Baris TOTAL untuk Jenis Tampilan REKAP :
            '----------------------------------------
            If Total_TagihanPPN = 0 Then Total_TagihanPPN = StripKosong
            If Total_JumlahBayarPPN = 0 Then Total_JumlahBayarPPN = StripKosong
            If Total_SisaHutangPPN = 0 Then Total_SisaHutangPPN = StripKosong
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Total_PajakKeluaran_Dibayar, Total_PajakKeluaran_Dipungut, Total_PajakKeluaran_TidakDipungut, Total_PajakKeluaran_Retur, Total_PajakKeluaran_Jumlah,
                                    Total_PajakMasukan_Impor, Total_PajakMasukan_DalamNegeri, Total_PajakMasukan_Retur, Total_PajakMasukan_KompSebelumnya,
                                    Total_PajakMasukan_KompPembetulan, Total_PajakMasukan_Jumlah,
                                    Total_PPNNKL, StripKosong, StripKosong, Total_JumlahBayarPPN, Total_SisaHutangPPN, Total_PPNTidakDapatDikreditkan,
                                    Total_PeredaranUsaha_Lokal, Total_PeredaranUsaha_Ekspor, Total_ReturPenjualan, Total_PeredaranUsaha_Jumlah, Keterangan)

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

        PesanUntukProgrammer("Perhitungan untuk RETUR PEMBELIAN belum diterapkan...!!!")

    End Sub

    Sub TambahBaris_Normal()
        NomorUrut += 1
        NP_Lapor = "N"
        IsiValue_DataPelaporan()
        JumlahTagihan_PPN = PPNNKL
        If AmbilAngka(JumlahTagihan_PPN) = 0 Then JumlahTagihan_PPN = StripKosong
        If AmbilAngka(JumlahBayar_PPN) = 0 Then JumlahBayar_PPN = StripKosong
        If AmbilAngka(SisaHutang_PPN) = 0 Then SisaHutang_PPN = StripKosong
        DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, NamaBulan, TanggalLapor, NomorIDLapor, TWTL_Lapor, NP_Lapor,
                                PajakKeluaran_Dibayar, PajakKeluaran_Dipungut, PajakKeluaran_TidakDipungut, PajakKeluaran_Retur, PajakKeluaran_Jumlah,
                                PajakMasukan_Impor, PajakMasukan_DalamNegeri, PajakMasukan_Retur, PajakMasukan_KompensasiSebelumnya, PajakMasukan_KompensasiPembetulan, PajakMasukan_Jumlah,
                                PPNNKL, 0, StripKosong, JumlahBayar_PPN, SisaHutang_PPN, PPNTidakDapatDikreditkan, PeredaranUsaha_Lokal, PeredaranUsaha_Ekspor, ReturPenjualan, PeredaranUsaha_Jumlah, Keterangan)
        If JenisTampilan = JenisTampilan_REKAP Then
            If PPNNKL > 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If PPNNKL = 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If
        ManajemenWarna()
        Index_BarisTabel += 1
        'Application.DoEvents()
    End Sub

    Sub TambahBaris_Pembetulan()
        NP_Lapor = "P"
        IsiValue_DataPelaporan()
        DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, TanggalLapor, NomorIDLapor, TWTL_Lapor, NP_Lapor,
                                Pembetulan_PajakKeluaran_Dibayar, Pembetulan_PajakKeluaran_Dipungut, Pembetulan_PajakKeluaran_TidakDipungut, PajakKeluaran_Retur, Pembetulan_PajakKeluaran_Jumlah,
                                Pembetulan_PajakMasukan_Impor, Pembetulan_PajakMasukan_DalamNegeri, 0, Pembetulan_PajakMasukan_KompensasiSebelumnya, Pembetulan_PajakMasukan_KompensasiPembetulan, Pembetulan_PajakMasukan_Jumlah,
                                Pembetulan_PPNNKL, SelisihPembetulan_PPNNKL, KompensasiKe, Kosongan, Kosongan, 0, Pembetulan_PeredaranUsaha_Lokal, Pembetulan_PeredaranUsaha_Ekspor, 0, Pembetulan_PeredaranUsaha_Jumlah, Kosongan)
        If JenisTampilan = JenisTampilan_REKAP Then
            If PPNNKL > 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If PPNNKL = 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If
        ManajemenWarna()
        Index_BarisTabel += 1
        'Application.DoEvents()
    End Sub

    Sub ManajemenWarna()
        If SelisihPembetulan_PPNNKL < 0 Then PengaturWaranaCell_Huruf(DataTabelUtama, "Selisih_Pembetulan", Index_BarisTabel, WarnaMerahSolid)
        If PPNNKL < 0 Then PengaturWaranaCell_Huruf(DataTabelUtama, "PPN_NKL", Index_BarisTabel, WarnaMerahSolid)
        If TWTL_Lapor = TWTL_Terlambat Then PengaturWaranaCell_Huruf(DataTabelUtama, "TW_TL_Lapor", Index_BarisTabel, WarnaMerahSolid)
    End Sub
    Sub IsiValue_DataPelaporan()

        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_PengawasanPelaporanPajak " &
                                      " WHERE Bulan     = '" & NomorBulan & " ' " &
                                      " AND Jenis_Pajak = '" & JenisPajak & " ' " &
                                      " AND N_P         = '" & NP_Lapor & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR2_ExecuteReader()
        drTELUSUR2.Read()
        If drTELUSUR2.HasRows Then
            TanggalLapor = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_Lapor"))
            NomorIDLapor = drTELUSUR2.Item("Nomor_ID")
            If NP_Lapor <> "N" Then
                KompensasiKe = drTELUSUR2.Item("Kompensasi_Ke_Tahun") & "-" & KonversiBulanKeAngka(drTELUSUR2.Item("Kompensasi_Ke_Bulan"))
            Else
                KompensasiKe = Kosongan
            End If
        Else
            TanggalLapor = Kosongan
            NomorIDLapor = 0
            KompensasiKe = Kosongan
        End If

        'Logika Tepat Waktu :
        If TanggalLapor = Kosongan Or NP_Lapor <> "N" Then
            TWTL_Lapor = Kosongan
        Else
            Dim TanggalLapor_Date As Date = TanggalLapor
            Dim BulanDeadlineLapor = NomorBulan + 1
            Dim TahunDeadlineLapor = TahunPajak
            If BulanDeadlineLapor = 13 Then
                BulanDeadlineLapor = 1
                TahunDeadlineLapor = TahunPajak + 1
            End If
            Dim TanggalDeadlineLapor As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDeadlineLapor, TahunDeadlineLapor)
            If TanggalLapor_Date <= TanggalDeadlineLapor Then
                TWTL_Lapor = TWTL_TepatWaktu
            Else
                TWTL_Lapor = TWTL_Terlambat
            End If
        End If

    End Sub

    Sub IsiValue_KompensasiPembetulan()

        'PesanUntukProgrammer(NomorBulan & " - " & TahunPajak)
        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_PengawasanPelaporanPajak " &
                                      " WHERE Kompensasi_Ke_Bulan = '" & NamaBulan & "' " &
                                      " AND   Kompensasi_Ke_Tahun = '" & TahunPajak & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR2_ExecuteReader()
        Do While drTELUSUR2.Read
            PajakMasukan_KompensasiPembetulan += drTELUSUR2.Item("Jumlah_Lebih_Bayar")
        Loop

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        grb_Pembayaran.Visible = False
        grb_LaporSPT.Enabled = False
        btn_EditSPT.Enabled = False
        btn_HapusSPT.Enabled = False
        btn_LihatJurnal.Enabled = False
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        'If TahunPajak = TahunBukuAktif Then
        '    Dim JumlahTagihan_SA = 0
        '    Dim JumlahBayar_SA = 0
        '    Dim Tahun_SA = TahunBukuAktif - 1
        '    AksesDatabase_Transaksi(Buka)
        '    cmdTAGIHAN = New OdbcCommand(" SELECT PPh_Pasal_23 FROM tbl_SisaHutangPPN ", KoneksiDatabaseTransaksi)
        '    drTAGIHAN_ExecuteReader()
        '    Do While drTAGIHAN.Read
        '        JumlahTagihan_SA += drTAGIHAN.Item("PPh_Pasal_23")
        '    Loop
        '    AksesDatabase_Transaksi(Tutup)
        '    Dim TahunTelusurPembayaran = TahunCutOff
        '    Do While TahunTelusurPembayaran <= TahunBukuKemarin
        '        TahunBuku_Alternatif = TahunTelusurPembayaran
        '        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        '        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_PembayaranHutangPajak ",
        '                                   KoneksiDatabaseTransaksi_Alternatif)
        '        drBAYAR_ExecuteReader()
        '        Do While drBAYAR.Read
        '            JumlahBayar_SA += drBAYAR.Item("Jumlah_Bayar")
        '        Loop
        '        TutupDatabaseTransaksi_Alternatif()
        '        TahunTelusurPembayaran += 1
        '    Loop
        '    SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
        '    txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        'End If
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        'Dim JumlahPenyesuaian_DEBET = 0
        'Dim JumlahPenyesuaian_KREDIT = 0
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
        '                      " WHERE COA = '" & KodeTautanCOA_HutangPPN & "' ",
        '                      KoneksiDatabaseTransaksi)
        'dr_ExecuteReader()
        'dr.Read()
        'SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        'txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        'cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
        '                      " WHERE COA = '" & KodeTautanCOA_HutangPPN & "' " &
        '                      " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
        '                      KoneksiDatabaseTransaksi)
        'dr_ExecuteReader()
        'Do While dr.Read
        '    If dr.HasRows Then
        '        JumlahPenyesuaian_DEBET += dr.Item("Jumlah_Debet")
        '        JumlahPenyesuaian_KREDIT += dr.Item("Jumlah_Kredit")
        '    End If
        'Loop
        'AksesDatabase_Transaksi(Tutup)
        'JumlahPenyesuaianSaldo = JumlahPenyesuaian_KREDIT - JumlahPenyesuaian_DEBET
        'SaldoAwal_BerdasarkanCOA_PlusPenyesuaian = SaldoAwal_BerdasarkanCOA + JumlahPenyesuaianSaldo
        'txt_AJP.Text = JumlahPenyesuaianSaldo
        'txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
        '                      " WHERE COA = '" & KodeTautanCOA_HutangPPN & "' ",
        '                      KoneksiDatabaseTransaksi)
        'dr_ExecuteReader()
        'dr.Read()
        'SaldoAkhir_BerdasarkanCOA = dr.Item("Saldo_Awal")
        'AksesDatabase_Transaksi(Tutup)
        'txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
    End Sub

    Sub Baris_KetetapanPajak()

        Dim JenisPajak_YangDitelusuri = JenisPajak_PPN
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
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP     LIKE '%" & AwalanBPKP & "%' " &
                                   " AND Jenis_Pajak    = '" & JenisPajak_YangDitelusuri & "' " &
                                   " AND Status_Invoice = '" & Status_Dibayar & "' ",
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
        DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, "Ketetapan Pajak", Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan,
                                JumlahTagihan_KetetapanPajak, Kosongan, Kosongan, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan)

        Total_PajakMasukan_Jumlah += AmbilAngka(JumlahTagihan_KetetapanPajak)
        Total_JumlahBayarPPN += AmbilAngka(JumlahBayar_KetetapanPajak)
        Total_SisaHutangPPN += AmbilAngka(SisaHutang_KetetapanPajak)

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

    Private Sub btn_HasilAkhir_Click(sender As Object, e As EventArgs) Handles btn_HasilAkhir.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub btn_KompensasiKe_Click(sender As Object, e As EventArgs)

    End Sub

    Sub Sub_JenisTampilan_ALL()
        JenisTampilan = JenisTampilan_ALL
        JudulForm = "Buku Pengawasan Pelaporan PPN"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_REKAP()
        JenisTampilan = JenisTampilan_REKAP
        JudulForm = "Buku Pengawasan Pelaporan PPN"
        VisibilitasObjek_REKAP()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_DETAIL()
        JenisTampilan = JenisTampilan_DETAIL
        JudulForm = "Buku Pengawasan Pelaporan PPN"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub VisibilitasObjek_REKAP()
        btn_EditSPT.Enabled = False
        btn_HapusSPT.Enabled = False
        'DataTabelUtama.Columns("Nomor_BPHP").Visible = False
        'DataTabelUtama.Columns("Bulan_").Visible = True
        'DataTabelUtama.Columns("Bulan_").HeaderText = "Masa Pajak"
        'DataTabelUtama.Columns("Tanggal_Transaksi").Visible = False
        'DataTabelUtama.Columns("Tanggal_Invoice").Visible = False
        'DataTabelUtama.Columns("Nomor_Invoice").Visible = False
        'DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = False
        'DataTabelUtama.Columns("Nama_Jasa").Visible = False
        'DataTabelUtama.Columns("NPWP_").Visible = False
        'DataTabelUtama.Columns("Nama_Supplier").Visible = False
        'DataTabelUtama.Columns("DPP_").Visible = False
        'DataTabelUtama.Columns("PPh_Pasal_23").HeaderText = "Jumlah Tagihan"
        'DataTabelUtama.Columns("Jumlah_Bayar_Pajak").Visible = True
        'DataTabelUtama.Columns("Sisa_Hutang_Pajak").Visible = True
        'DataTabelUtama.Columns("Keterangan_").Visible = False
    End Sub

    Sub VisibilitasObjek_DETAIL()
        grb_InfoSaldo.Visible = False
        grb_InfoSaldo.Visible = False
        lbl_SaldoAwalBerdasarkanCOA.Visible = False
        txt_SaldoAwalBerdasarkanCOA.Visible = False
        lbl_AJP.Visible = False
        txt_AJP.Visible = False
        grb_Pembayaran.Visible = False
    End Sub

    Private Sub btn_TambahSPT_Click(sender As Object, e As EventArgs) Handles btn_TambahSPT.Click
        win_InputLaporPajak = New wpfWin_InputLaporPajak
        win_InputLaporPajak.ResetForm()
        win_InputLaporPajak.FungsiForm = FungsiForm_TAMBAH
        win_InputLaporPajak.JenisPajak = JenisPajak_PPN
        BukaFormInputLaporPajak()
    End Sub

    Private Sub btn_EditSPT_Click(sender As Object, e As EventArgs) Handles btn_EditSPT.Click
        win_InputLaporPajak = New wpfWin_InputLaporPajak
        win_InputLaporPajak.ResetForm()
        win_InputLaporPajak.FungsiForm = FungsiForm_EDIT
        win_InputLaporPajak.JenisPajak = JenisPajak_PPN
        win_InputLaporPajak.NomorID = NomorIDLapor_Terseleksi
        win_InputLaporPajak.dtp_TanggalLapor.SelectedDate = TanggalFormatWPF(TanggalLapor_Terseleksi)
        BukaFormInputLaporPajak()
    End Sub

    Sub BukaFormInputLaporPajak()
        ProsesIsiValueForm = True
        win_InputLaporPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        IsiValueComboBypassTerkunci(win_InputLaporPajak.cmb_NP, NP_Lapor_Terseleksi)
        win_InputLaporPajak.txt_JumlahLebihBayar.Text = 0 - SelisihPembetulanPPNNKL_Terseleksi
        ProsesIsiValueForm = False
        win_InputLaporPajak.ShowDialog()
    End Sub

    Private Sub btn_HapusSPT_Click(sender As Object, e As EventArgs) Handles btn_HapusSPT.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus laporan terpilih..?" & Enter2Baris &
                                  "Catatan :" & Enter1Baris &
                                  "Data invoice tidak akan terhapus pada event ini.",
                                  "Perhatian..!", MessageBoxButtons.YesNo)

        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanPelaporanPajak " &
                              " WHERE Nomor_ID = '" & NomorIDLapor_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        PesanUntukProgrammer("Hapus juga data 'Tanggal Lapor' di masing-masing INVOICE terkait...!!! ")

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If SisaHutangPajak_Terseleksi <= 0 Then
            MsgBox("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak_PPN
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran_Non
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, Kosongan, "Pembayaran " & JenisPajak & " " & Bulan_Terseleksi, NomorBPHP_Terseleksi,
                                JumlahTagihanPajak_Terseleksi, 0, 0, 0, JumlahBayarPajak_Terseleksi, SisaHutangPajak_Terseleksi,
                                JenisPajak, KodeSetoran_Non, 0, 0, 0,
                                SisaHutangPajak_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangPajak.ResetForm()
        frm_InputPembayaranHutangPajak.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangPajak.JenisPajak = JenisPajak_PPN
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
        frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = JumlahTagihanPajak_Terseleksi - HitungBayar
        frm_InputPembayaranHutangPajak.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranHutangPajak.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangPajak.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangPajak.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

    End Sub

    Sub IsiValueForm_InputPembayaran()
        frm_InputPembayaranHutangPajak.txt_NomorBPHP.Text = NomorBPHP_Terseleksi
        frm_InputPembayaranHutangPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        frm_InputPembayaranHutangPajak.txt_JumlahTerutang.Text = JumlahTagihanPajak_Terseleksi
        frm_InputPembayaranHutangPajak.KodeSetoran = Kosongan
    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

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

    Sub TampilkanDataPembayaran()

        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False

        dgv_DetailBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar = 0
        Dim TWTL_Bayar = Kosongan
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran
        Dim TotalBayar = 0

        Dim TanggalPelunasan_Date As Date
        Dim BulanDeadlineBayar = NomorUrut_Terseleksi + 1
        Dim TahunDeadlineBayar = TahunPajak
        If BulanDeadlineBayar = 13 Then
            BulanDeadlineBayar = 1
            TahunDeadlineBayar = TahunPajak + 1
        End If
        Dim TanggalDeadlineBayar As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDeadlineBayar, TahunDeadlineBayar)

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP      = '" & NomorBPHP_Terseleksi & "' " &
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
                    TanggalPelunasan_Date = TanggalBayar
                    If TanggalPelunasan_Date <= TanggalDeadlineBayar Then
                        TWTL_Bayar = TWTL_TepatWaktu
                    Else
                        TWTL_Bayar = TWTL_Terlambat
                    End If
                    dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, TWTL_Bayar, KeteranganBayar, NomorJV_Pembayaran)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TWTL_Bayar = TWTL_Terlambat Then PengaturWaranaCell_Huruf(dgv_DetailBayar, "TW_TL_Bayar", Index_BarisTabelPembayaran, WarnaMerahSolid)
                    If TotalBayar >= JumlahTagihanPajak_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihanPajak_Terseleksi Then Exit Do
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
        Dim TelusurBulan = 0
        Do While Bulan_Terseleksi = Kosongan
            TelusurBulan += 1
            Bulan_Terseleksi = DataTabelUtama.Item("Bulan_", Baris_Terseleksi - TelusurBulan).Value
        Loop
        NomorBPHP_Terseleksi = DataTabelUtama.Item("Nomor_BPHP", Baris_Terseleksi).Value
        TanggalLapor_Terseleksi = DataTabelUtama.Item("Tanggal_Lapor", Baris_Terseleksi).Value
        NomorIDLapor_Terseleksi = DataTabelUtama.Item("Nomor_ID_Lapor", Baris_Terseleksi).Value
        NP_Lapor_Terseleksi = DataTabelUtama.Item("N_P_Lapor", Baris_Terseleksi).Value
        PPNNKL_Terseleksi = AmbilAngka(DataTabelUtama("PPN_NKL", Baris_Terseleksi).Value)
        SelisihPembetulanPPNNKL_Terseleksi = AmbilAngka(DataTabelUtama("Selisih_Pembetulan", Baris_Terseleksi).Value)


        JumlahTagihanPajak_Terseleksi = PPNNKL_Terseleksi
        JumlahBayarPajak_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Bayar", Baris_Terseleksi).Value)
        SisaHutangPajak_Terseleksi = AmbilAngka(DataTabelUtama("Sisa_Hutang", Baris_Terseleksi).Value)

        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value

        If JenisTampilan = JenisTampilan_DETAIL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU And NomorID_Terseleksi = 0 Then BersihkanSeleksi()
        End If

        If JenisTampilan = JenisTampilan_REKAP Then
            If PPNNKL_Terseleksi > 0 And NomorUrut_Terseleksi > 0 Then
                If NP_Lapor_Terseleksi = "N" Then
                    TampilkanDataPembayaran()
                Else
                    TampilkanInfoSaldo()
                End If
                grb_LaporSPT.Enabled = True
                If TanggalLapor_Terseleksi = Kosongan Then
                    If SisaHutangPajak_Terseleksi = 0 Then
                        btn_TambahSPT.Enabled = True
                    Else
                        btn_TambahSPT.Enabled = False
                    End If
                    btn_EditSPT.Enabled = False
                    btn_HapusSPT.Enabled = False
                Else
                    btn_TambahSPT.Enabled = False
                    btn_EditSPT.Enabled = True
                    btn_HapusSPT.Enabled = True
                End If
            Else
                If Bulan_Terseleksi <> JenisPajak_KetetapanPajak Then BersihkanSeleksi()
                TampilkanInfoSaldo()
            End If
        End If

        If JenisTampilan = JenisTampilan_DETAIL Then
            If NomorUrut_Terseleksi > 0 Then
                btn_EditSPT.Enabled = True
                btn_HapusSPT.Enabled = True
            Else
                btn_EditSPT.Enabled = False
                btn_HapusSPT.Enabled = False
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
                        btn_TambahSPT_Click(sender, e)
                    Else
                        btn_EditSPT_Click(sender, e)
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
            usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak_PPN
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

        ''JIKA JENIS TAHUN BUKU LAMPAU :
        'If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
        '    PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        'End If

        ''JIKA JENIS TAHUN BUKU NORMAL :
        'If JenisTahunBuku = JenisTahunBuku_NORMAL Then

        '    Dim NamaAkun_BiayaSelisihPencatatan
        '    Dim NamaAkun_HutangPPN
        '    Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
        '    KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
        '    PengisianValue_NamaAkun()
        '    NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
        '    KodeAkun_Tembak = KodeTautanCOA_HutangPPN
        '    PengisianValue_NamaAkun()
        '    NamaAkun_HutangPPN = NamaAkun_Tembak
        '    frm_InputJurnal.ResetForm()
        '    frm_InputJurnal.JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPN
        '    frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        '    If JumlahPenyesuaian > 0 Then
        '        frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan,
        '                                                   NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
        '        frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangPPN,
        '                                                   PenjorokNamaAkun & NamaAkun_HutangPPN, "K", Nothing, JumlahPenyesuaian)
        '    End If
        '    If JumlahPenyesuaian < 0 Then
        '        JumlahPenyesuaian = -(JumlahPenyesuaian)
        '        frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_HutangPPN,
        '                                                   NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
        '        frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_BiayaSelisihPencatatan,
        '                                                   PenjorokNamaAkun & NamaAkun_HutangPPN, "K", Nothing, JumlahPenyesuaian)
        '    End If
        '    frm_InputJurnal.DataTabelUtama.Item("Debet", 3).Value = JumlahPenyesuaian
        '    frm_InputJurnal.DataTabelUtama.Item("Kredit", 3).Value = JumlahPenyesuaian
        '    frm_InputJurnal.lbl_StatusBalance.ForeColor = Color.Green
        '    frm_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
        '    frm_InputJurnal.dtp_TanggalJurnal.Value = AwalTahunBukuAktif
        '    frm_InputJurnal.cmb_JenisJurnal.Text = JenisJurnal_AdjusmentSaldoAwal
        '    BeginInvoke(Sub() frm_InputJurnal.dtp_TanggalJurnal.Enabled = False)
        '    BeginInvoke(Sub() frm_InputJurnal.cmb_JenisJurnal.Enabled = False)
        '    BeginInvoke(Sub() frm_InputJurnal.btn_TambahTransaksi.Enabled = False)
        '    BeginInvoke(Sub() frm_InputJurnal.btn_Reset.Enabled = False)
        '    BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = True)
        '    BeginInvoke(Sub() frm_InputJurnal.JumlahBaris = 2)
        '    BeginInvoke(Sub() MsgBox("Silakan buat Jurnal Penyesuaian (Adjusment)."))
        '    frm_InputJurnal.ShowDialog()
        '    If frm_InputJurnal.JurnalTersimpan = True Then
        '        UpdateNotifikasi()
        '        TampilkanData()
        '    End If
        'End If

    End Sub

    Public Sub UpdateNotifikasi()
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
        '                      " Status_Dibaca = 1, " &
        '                      " Status_Dieksekusi = 1 " &
        '                      " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGPPN & "' " &
        '                      " AND Pesan = '" & teks_SilakanSesuaikanSaldo & "' ",
        '                      KoneksiDatabaseTransaksi)
        'cmd.ExecuteNonQuery()
        'AksesDatabase_Transaksi(Tutup)
        'frm_BOOKU.IsiKontenNotifikasi()
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

    Private Sub btn_Lapor_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lbl_TahunPajak_Click(sender As Object, e As EventArgs) Handles lbl_TahunPajak.Click

    End Sub
End Class