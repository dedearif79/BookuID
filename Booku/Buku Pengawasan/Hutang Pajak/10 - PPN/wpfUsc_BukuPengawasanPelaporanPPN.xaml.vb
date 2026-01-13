Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanPelaporanPPN

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

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
    Public TahunBukuSumberData

    Public NamaHalaman As String = "Buku Pengawasan Pelaporan PPN"
    Public AwalanBP As String = AwalanBPHPPN
    Public COAHutangPajak As String

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim Bulan_Terseleksi
    Dim TanggalLapor_Terseleksi
    Dim NomorIDLapor_Terseleksi
    Dim NP_Lapor_Terseleksi
    Dim PPNNKL_Terseleksi As Int64
    Dim SelisihPembetulanPPNNKL_Terseleksi As Int64

    Dim JumlahTagihanPajak_Terseleksi As Int64
    Dim JumlahBayarPajak_Terseleksi As Int64
    Dim SisaHutangPajak_Terseleksi As Int64
    Dim Keterangan_Terseleksi

    Dim SisaHutang_SaatCutOff

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

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
    Dim ReturPenjualan As Int64
    Dim PajakKeluaran_Dibayar As Int64
    Dim PajakKeluaran_Dipungut As Int64
    Dim PajakKeluaran_TidakDipungut As Int64
    Dim PajakKeluaran_Retur As Int64
    Dim PajakKeluaran_Jumlah As Int64
    Dim ReturPembelian As Int64
    Dim PajakMasukan_Impor As Int64
    Dim PajakMasukan_DalamNegeri As Int64
    Dim PajakMasukan_KompensasiSebelumnya As Int64
    Dim PajakMasukan_KompensasiPembetulan As Int64
    Dim PajakMasukan_Retur As Int64
    Dim PajakMasukan_Jumlah As Int64
    Dim PPNNKL As Int64
    Dim SelisihPembetulan_PPNNKL As Int64
    Dim KompensasiKe
    Dim PPNTidakDapatDikreditkan As Int64
    Dim JumlahTagihan_PPN As Int64
    Dim JumlahBayar_PPN As Int64
    Dim SisaHutang_PPN As Int64
    Dim PeredaranUsaha_Lokal As Int64
    Dim PeredaranUsaha_Ekspor As Int64
    Dim PeredaranUsaha_Jumlah As Int64
    Dim Keterangan

    'Variabel Total :
    Dim Total_ReturPenjualan As Int64
    Dim Total_PajakKeluaran_Retur As Int64
    Dim Total_PeredaranUsaha_Lokal As Int64
    Dim Total_PeredaranUsaha_Ekspor As Int64
    Dim Total_PeredaranUsaha_Jumlah As Int64
    Dim Total_PajakKeluaran_Dibayar As Int64
    Dim Total_PajakKeluaran_Dipungut As Int64
    Dim Total_PajakKeluaran_TidakDipungut As Int64
    Dim Total_PajakKeluaran_Jumlah As Int64
    Dim Total_ReturPembelian As Int64
    Dim Total_PajakMasukan_Retur As Int64
    Dim Total_PajakMasukan_Impor As Int64
    Dim Total_PajakMasukan_DalamNegeri As Int64
    Dim Total_PajakMasukan_KompSebelumnya As Int64
    Dim Total_PajakMasukan_KompPembetulan As Int64
    Dim Total_PajakMasukan_Jumlah As Int64
    Dim Total_PPNNKL As Int64
    Dim Total_SelisihPembetulan_PPNNKL As Int64
    Dim Total_PPNTidakDapatDikreditkan As Int64
    Dim Total_TagihanPPN As Int64 = 0
    Dim Total_JumlahBayarPPN As Int64 = 0
    Dim Total_SisaHutangPPN As Int64 = 0

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim TermasukHutangTahunIni_Terseleksi As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        JenisPajak = JenisPajak_PPN
        COAHutangPajak = KodeTautanCOA_HutangPPN

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            VisibilitasTombolJurnal(False)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            VisibilitasTombolJurnal(True)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        TahunPajak = TahunBukuAktif
        EksekusiKode = False
        KontenCombo_TahunPajak()
        KontenCombo_MasaPajak()
        EksekusiKode = True

        Sub_JenisTampilan_REKAP()

        ProsesLoadingForm = False

        'datagridUtama.SelectionUnit = DataGridSelectionUnit.FullRow 'Ini style khusus, karena ada masalah yang belum diketahui

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_TahunPajak()
        cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
        EksekusiKode = True
        TampilkanData()
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
        cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
    End Sub


    Sub KontenCombo_TahunPajak()
        TahunHutangPajakTerlama = TahunBukuAktif - 1
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.SelectedValue = TahunPajak
    End Sub


    Sub TampilkanData()

        If EksekusiKode = False Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)
        VisibilitasInfoSaldo(False)

        lbl_JudulForm.Text = JudulForm
        If MasaPajak = Nothing Then Return

        KesesuaianJurnal = True

        datatabelUtama.Rows.Clear()

        NomorUrut = 0
        NomorID = 0
        Keterangan = Kosongan
        Total_TagihanPPN = 0
        Total_JumlahBayarPPN = 0
        Total_SisaHutangPPN = 0

        If JenisTampilan = JenisTampilan_REKAP Then

            Index_BarisTabel = 0
            NomorBulan = 0

            TanggalLapor = Kosongan

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
            Total_SelisihPembetulan_PPNNKL = 0
            Total_PPNTidakDapatDikreditkan = 0

            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabaseTransaksi = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                NomorBulan = AmbilAngka(NomorBulan) + 1
                NamaBulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHPPN & TahunPajak & "-" & NomorBulan.ToString

                ResetVariabelPPN()

                PajakMasukan_KompensasiSebelumnya = 0
                If NomorBulan = 1 Then
                    PajakMasukan_KompensasiSebelumnya = 0
                Else
                    If PPNNKL < 0 Then PajakMasukan_KompensasiSebelumnya = 0 - PPNNKL
                End If

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                If JenisTahunBuku = JenisTahunBuku_NORMAL And TahunPajak = TahunBukuAktif Then
                    HitungDataPPN_TahunBukuNormal()
                End If

                HitungDataPembayaran()

                SisaHutang_PPN = JumlahTagihan_PPN - JumlahBayar_PPN

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
                Total_PPNNKL += PPNNKL
                Total_SelisihPembetulan_PPNNKL += SelisihPembetulan_PPNNKL
                Total_PeredaranUsaha_Lokal += PeredaranUsaha_Lokal
                Total_PeredaranUsaha_Ekspor += PeredaranUsaha_Ekspor
                Total_ReturPenjualan += ReturPenjualan
                Total_PeredaranUsaha_Jumlah += PeredaranUsaha_Jumlah
                Total_PPNTidakDapatDikreditkan += PPNTidakDapatDikreditkan
                Total_TagihanPPN += JumlahTagihan_PPN
                Total_JumlahBayarPPN += JumlahBayar_PPN
                Total_SisaHutangPPN += SisaHutang_PPN

                TambahBaris()

            Loop

            AksesDatabase_Transaksi(Tutup)

            Baris_KetetapanPajak()

            'Baris TOTAL :
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(
                Kosongan, Kosongan, Kosongan, teks_TOTAL_,
                Kosongan, Kosongan, Kosongan, Kosongan,
                Total_PajakKeluaran_Dibayar, Total_PajakKeluaran_Dipungut, Total_PajakKeluaran_TidakDipungut, Total_PajakKeluaran_Retur, Total_PajakKeluaran_Jumlah,
                Total_PajakMasukan_Impor, Total_PajakMasukan_DalamNegeri, Total_PajakMasukan_Retur, Total_PajakMasukan_KompSebelumnya, Total_PajakMasukan_KompPembetulan, Total_PajakMasukan_Jumlah,
                Total_PPNNKL, Total_SelisihPembetulan_PPNNKL, Kosongan, Total_JumlahBayarPPN, Total_SisaHutangPPN, Total_PPNTidakDapatDikreditkan,
                Total_PeredaranUsaha_Lokal, Total_PeredaranUsaha_Ekspor, Total_ReturPenjualan, Total_PeredaranUsaha_Jumlah, Kosongan)

        End If

        VisibilitasInfoSaldo(True)

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = Total_SisaHutangPPN
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                If Not TahunBukuSudahStabil(TahunBukuAktif) Then
                    AmbilValue_SaldoAwalBerdasarkanList()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                    CekKesesuaianSaldoAwal()
                    txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
                    txt_TotalTabel.Text = SaldoAwal_BerdasarkanList + Total_TagihanPPN - Total_JumlahBayarPPN
                Else
                    txt_TotalTabel.Text = SaldoAwal_BerdasarkanCOA + Total_TagihanPPN - Total_JumlahBayarPPN
                End If
        End Select

        lbl_TotalTabel.Text = "Saldo Akhir " & TahunPajak & " : "

        BersihkanSeleksi()

    End Sub


    Sub ResetVariabelPPN()
        ReturPenjualan = 0
        PeredaranUsaha_Lokal = 0
        PeredaranUsaha_Ekspor = 0
        PajakKeluaran_Dibayar = 0
        PajakKeluaran_Dipungut = 0
        PajakKeluaran_TidakDipungut = 0
        PajakKeluaran_Retur = 0
        PajakKeluaran_Jumlah = 0
        ReturPembelian = 0
        PajakMasukan_Impor = 0
        PajakMasukan_DalamNegeri = 0
        PajakMasukan_KompensasiPembetulan = 0
        PajakMasukan_Retur = 0
        PajakMasukan_Jumlah = 0
        PPNNKL = 0
        SelisihPembetulan_PPNNKL = 0
        PPNTidakDapatDikreditkan = 0
        PeredaranUsaha_Jumlah = 0
        JumlahTagihan_PPN = 0
        JumlahBayar_PPN = 0
        SisaHutang_PPN = 0
    End Sub


    Sub HitungDataPPN_TahunBukuNormal()

        Dim TarifPPNPenjualan As Decimal = 11 / 100

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_JV > 0 " &
                              " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
        KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Select Case dr.Item("Perlakuan_PPN")
                Case PerlakuanPPN_Dibayar
                    PajakKeluaran_Dibayar += dr.Item("PPN")
                Case PerlakuanPPN_Dipungut
                    PajakKeluaran_Dipungut += dr.Item("PPN")
                Case PerlakuanPPN_TidakDipungut
                    PajakKeluaran_TidakDipungut += dr.Item("PPN")
            End Select
            PeredaranUsaha_Lokal += dr.Item("Dasar_Pengenaan_Pajak")
        Loop

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Retur " &
                              " WHERE Nomor_JV > 0 " &
                              " AND DATE_FORMAT(Tanggal_Retur, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            ReturPenjualan += dr.Item("Total_Harga_Per_Item")
            PajakKeluaran_Retur += (TarifPPNPenjualan * dr.Item("Total_Harga_Per_Item"))
        Loop

        PeredaranUsaha_Jumlah = PeredaranUsaha_Lokal + PeredaranUsaha_Ekspor - ReturPenjualan
        PajakKeluaran_Jumlah = PajakKeluaran_Dibayar + PajakKeluaran_Dipungut + PajakKeluaran_TidakDipungut - PajakKeluaran_Retur

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_JV > 0 " &
                              " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            PajakMasukan_DalamNegeri += dr.Item("PPN")
        Loop

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                              " WHERE Nomor_JV > 0 " &
                              " AND DATE_FORMAT(Tanggal_Retur, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            PajakMasukan_Retur += dr.Item("PPN")
        Loop

        PajakMasukan_Jumlah = PajakMasukan_Impor + PajakMasukan_DalamNegeri + PajakMasukan_KompensasiSebelumnya + PajakMasukan_KompensasiPembetulan - PajakMasukan_Retur

        PPNNKL = PajakKeluaran_Jumlah - PajakMasukan_Jumlah
        If PPNNKL > 0 Then
            JumlahTagihan_PPN = PPNNKL
        Else
            JumlahTagihan_PPN = 0
        End If

        IsiValue_DataPelaporan()

    End Sub


    Sub HitungDataPembayaran()
        If JumlahTagihan_PPN > 0 Then
            Dim TahunTelusurPembayaran = TahunPajak
            Dim TahunSumberDataPembayaran = 0
            Dim PencegahLoopingTahunPajakLampau = 0
            Do While TahunTelusurPembayaran <= TahunBukuAktif
                If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
                If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
                If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                    BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                    cmd = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                          " WHERE Nomor_BP          = '" & NomorBPHP & "' " &
                                          " AND Status_Invoice      = '" & Status_Dibayar & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        JumlahBayar_PPN += dr.Item("Jumlah_Bayar")
                        If JumlahBayar_PPN >= JumlahTagihan_PPN Then Exit Do
                    Loop
                    TutupDatabaseTransaksi_Alternatif()
                End If
                If JumlahBayar_PPN >= JumlahTagihan_PPN Then Exit Do
                PencegahLoopingTahunPajakLampau += 1
                TahunTelusurPembayaran += 1
            Loop
        End If
    End Sub


    Sub IsiValue_DataPelaporan()
        BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_PengawasanPelaporanPajak " &
                                      " WHERE Bulan     = '" & NomorBulan & " ' " &
                                      " AND Jenis_Pajak = '" & JenisPajak & " ' ", KoneksiDatabaseTransaksi_Alternatif)
        drTELUSUR2_ExecuteReader()
        drTELUSUR2.Read()
        If drTELUSUR2.HasRows Then
            TanggalLapor = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_Lapor"))
            NomorIDLapor = drTELUSUR2.Item("Nomor_ID")
            NP_Lapor = drTELUSUR2.Item("N_P")
        Else
            TanggalLapor = Kosongan
            NomorIDLapor = 0
            NP_Lapor = Kosongan
        End If

        If TanggalLapor = Kosongan Or NP_Lapor <> "N" Then
            TWTL_Lapor = Kosongan
        Else
            Dim TanggalLapor_Date As Date = TanggalLapor
            Dim BulanDeadlineLapor = AmbilAngka(NomorBulan) + 1
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
        TutupDatabaseTransaksi_Alternatif()
    End Sub


    Sub TambahBaris()

        NomorUrut += 1

        datatabelUtama.Rows.Add(
            NomorUrut, NomorID, NomorBPHP, NamaBulan,
            TanggalLapor, NomorIDLapor, TWTL_Lapor, NP_Lapor,
            PajakKeluaran_Dibayar, PajakKeluaran_Dipungut, PajakKeluaran_TidakDipungut, PajakKeluaran_Retur, PajakKeluaran_Jumlah,
            PajakMasukan_Impor, PajakMasukan_DalamNegeri, PajakMasukan_Retur, PajakMasukan_KompensasiSebelumnya, PajakMasukan_KompensasiPembetulan, PajakMasukan_Jumlah,
            PPNNKL, SelisihPembetulan_PPNNKL, KompensasiKe, JumlahBayar_PPN, SisaHutang_PPN, PPNTidakDapatDikreditkan,
            PeredaranUsaha_Lokal, PeredaranUsaha_Ekspor, ReturPenjualan, PeredaranUsaha_Jumlah, Keterangan)

        Index_BarisTabel += 1
        Terabas()

    End Sub


    Sub Baris_KetetapanPajak()

        Dim NomorBPHP_KetetapanPajak = Kosongan
        Dim JumlahTagihan_KetetapanPajak As Int64
        Dim JumlahBayar_KetetapanPajak As Int64
        Dim SisaHutang_KetetapanPajak As Int64

        BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak " &
                              " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                              KoneksiDatabaseTransaksi_Alternatif)
        dr_ExecuteReader()
        JumlahTagihan_KetetapanPajak = 0
        Do While dr.Read
            NomorBPHP_KetetapanPajak = dr.Item("Nomor_BPHP")
            JumlahTagihan_KetetapanPajak += dr.Item("Pokok_Pajak")
        Loop

        JumlahBayar_KetetapanPajak = 0
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP     LIKE '%" & AwalanBPKP & "%' " &
                                   " AND Jenis_Pajak    = '" & JenisPajak & "' " &
                                   " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        drBAYAR_ExecuteReader()
        Do While drBAYAR.Read
            JumlahBayar_KetetapanPajak += drBAYAR.Item("Jumlah_Bayar")
        Loop

        TutupDatabaseTransaksi_Alternatif()

        SisaHutang_KetetapanPajak = JumlahTagihan_KetetapanPajak - JumlahBayar_KetetapanPajak

        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(
            Kosongan, Kosongan, Kosongan, "Ketetapan Pajak",
            Kosongan, Kosongan, Kosongan, Kosongan,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0,
            JumlahTagihan_KetetapanPajak, 0, Kosongan, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak, 0,
            0, 0, 0, 0, Kosongan)

        Total_TagihanPPN += JumlahTagihan_KetetapanPajak
        Total_JumlahBayarPPN += JumlahBayar_KetetapanPajak
        Total_SisaHutangPPN += SisaHutang_KetetapanPajak

    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_LihatJurnal.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Private Sub cmb_TahunPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunPajak.SelectionChanged

        TahunPajak = AmbilAngka(cmb_TahunPajak.SelectedValue)
        TahunPajakSebelumIni = TahunPajak - 1

        If TahunPajak > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
            TahunBukuSumberData = TahunPajak
            VisibilitasTombolDetailPembayaran(True)
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
            TahunBukuSumberData = TahunCutOff
            VisibilitasTombolDetailPembayaran(False)
        End If

        If TahunPajak = TahunBukuAktif Then
            TahunPajakSamaDenganTahunBukuAktif = True
        Else
            TahunPajakSamaDenganTahunBukuAktif = False
        End If

        If EksekusiKode = True Then
            If MasaPajak = MasaPajak_Rekap Then Sub_JenisTampilan_REKAP()
            If MasaPajak <> MasaPajak_Rekap Then cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
        End If

    End Sub


    Private Sub cmb_MasaPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MasaPajak.SelectionChanged

        MasaPajak = cmb_MasaPajak.SelectedValue
        MasaPajak_Angka = KonversiBulanKeAngka(MasaPajak)

        If EksekusiKode = True And ProsesLoadingForm = False Then
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


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_HasilAkhir_Click(sender As Object, e As RoutedEventArgs) Handles btn_HasilAkhir.Click
        FiturBelumBisaDigunakan()
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
        btn_EditSPT.IsEnabled = False
        btn_HapusSPT.IsEnabled = False
    End Sub

    Sub VisibilitasObjek_DETAIL()
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        grb_Pembayaran.Visibility = Visibility.Collapsed
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        If Visibilitas Then
            brd_LihatJurnal.Visibility = Visibility.Visible
            btn_LihatJurnal.Visibility = Visibility.Visible
        Else
            brd_LihatJurnal.Visibility = Visibility.Collapsed
            btn_LihatJurnal.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolDetailPembayaran(Visibilitas As Boolean)
        brd_DetailPembayaran.Visibility = Visibility.Collapsed
        btn_DetailPembayaran.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_DetailPembayaran.Visibility = Visibility.Visible
                btn_DetailPembayaran.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If TahunPajakSamaDenganTahunBukuAktif Then
                grb_InfoSaldo.Visibility = Visibility.Visible
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                    pnl_TotalTabel.Visibility = Visibility.Visible
                End If
            End If
        End If
    End Sub

    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub


    Private Sub btn_InputSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputSPT.Click
        win_InputLaporPajak = New wpfWin_InputLaporPajak
        win_InputLaporPajak.ResetForm()
        win_InputLaporPajak.FungsiForm = FungsiForm_TAMBAH
        win_InputLaporPajak.JenisPajak = JenisPajak_PPN
        BukaFormInputLaporPajak()
    End Sub

    Private Sub btn_EditSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditSPT.Click
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

    Private Sub btn_HapusSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusSPT.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus laporan terpilih?" & Enter2Baris &
                                  "Catatan :" & Enter1Baris &
                                  "Data invoice tidak akan terhapus pada event ini.") Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanPelaporanPajak " &
                              " WHERE Nomor_ID = '" & NomorIDLapor_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaHutangPajak_Terseleksi <= 0 Then
            Pesan_Informasi("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
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
        win_InputBuktiPengeluaran.datatabelUtama.Rows.Add(1, Kosongan, Kosongan, "Pembayaran " & JenisPajak & " " & Bulan_Terseleksi, NomorBPHP_Terseleksi,
                                JumlahTagihanPajak_Terseleksi, 0, 0, 0, JumlahBayarPajak_Terseleksi, SisaHutangPajak_Terseleksi,
                                JenisPajak, KodeSetoran_Non, 0, 0, 0,
                                SisaHutangPajak_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub

    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailPembayaran.Click
        win_DetailPembayaranPajak = New wpfWin_DetailPembayaranPajak
        win_DetailPembayaranPajak.ResetForm()
        win_DetailPembayaranPajak.JenisPajak = JenisPajak
        win_DetailPembayaranPajak.ShowDialog()
    End Sub


    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return
        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID"))
        Bulan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Bulan_")
        If Bulan_Terseleksi = Kosongan Or Bulan_Terseleksi = teks_TOTAL_ Then
            BersihkanSeleksi()
            Return
        End If
        NomorBPHP_Terseleksi = rowviewUtama("Nomor_BPHP")
        TanggalLapor_Terseleksi = rowviewUtama("Tanggal_Lapor")
        NomorIDLapor_Terseleksi = rowviewUtama("Nomor_ID_Lapor")
        NP_Lapor_Terseleksi = rowviewUtama("N_P_Lapor")
        PPNNKL_Terseleksi = AmbilAngka(rowviewUtama("PPN_NKL"))
        SelisihPembetulanPPNNKL_Terseleksi = AmbilAngka(rowviewUtama("Selisih_Pembetulan"))

        JumlahTagihanPajak_Terseleksi = PPNNKL_Terseleksi
        JumlahBayarPajak_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar"))
        SisaHutangPajak_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Hutang"))

        Keterangan_Terseleksi = rowviewUtama("Keterangan_")

        If JenisTampilan = JenisTampilan_REKAP Then
            If PPNNKL_Terseleksi > 0 And NomorUrut_Terseleksi > 0 Then
                If NP_Lapor_Terseleksi = "N" Then
                    ResetTampilanDataPembayaran()
                Else
                    TampilkanInfoSaldo()
                End If
                grb_LaporSPT.IsEnabled = True
                If TanggalLapor_Terseleksi = Kosongan Then
                    If SisaHutangPajak_Terseleksi = 0 Then
                        btn_InputSPT.IsEnabled = True
                    Else
                        btn_InputSPT.IsEnabled = False
                    End If
                    btn_EditSPT.IsEnabled = False
                    btn_HapusSPT.IsEnabled = False
                Else
                    btn_InputSPT.IsEnabled = False
                    btn_EditSPT.IsEnabled = True
                    btn_HapusSPT.IsEnabled = True
                End If
            Else
                pnl_SidebarKanan.Visibility = Visibility.Collapsed
                'If Bulan_Terseleksi <> JenisPajak_KetetapanPajak Then BersihkanSeleksi()
                TampilkanInfoSaldo()
            End If
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        If JenisTampilan = JenisTampilan_REKAP And cmb_MasaPajak.IsEnabled = True Then
            If NomorUrut_Terseleksi <> 0 Then cmb_MasaPajak.SelectedValue = rowviewUtama("Bulan_")
        End If
        If Bulan_Terseleksi = JenisPajak_KetetapanPajak Then
            win_BOOKU.BukaModul_BukuPengawasanKetetapanPajak()
            usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak_PPN
        End If
    End Sub


    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            btn_LihatJurnal.IsEnabled = False
            BersihkanSeleksiTabelPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = rowviewBayar("Nomor_JV_Bayar")
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
            btn_EditBayar.IsEnabled = False
            btn_HapusBayar.IsEnabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            btn_EditBayar.IsEnabled = False
            btn_HapusBayar.IsEnabled = False
        End If
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
    End Sub


    Sub TampilkanInfoSaldo()
        grb_InfoSaldo.Visibility = Visibility.Visible
        grb_Pembayaran.Visibility = Visibility.Collapsed
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
        Else
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub ResetTampilanDataPembayaran()
        pnl_SidebarKanan.Visibility = Visibility.Visible
        grb_Pembayaran.Visibility = Visibility.Visible
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        TampilkanDataPembayaran()
    End Sub

    Sub TampilkanDataPembayaran()

        datatabelBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar As Int64 = 0
        Dim TWTLBayar = Kosongan
        Dim TotalBayar As Int64 = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran
        Dim TahunSumberDataPembayaran = 0

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP          = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Status_Invoice      = '" & Status_Dibayar & "' " &
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
                    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, TWTLBayar, KeteranganBayar, NomorJV_Pembayaran)
                    If TotalBayar >= JumlahTagihanPajak_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihanPajak_Terseleksi Then Exit Do
            PencegahLoopingTahunPajakLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        BersihkanSeleksiTabelPembayaran()

    End Sub

    Sub BersihkanSeleksiTabelPembayaran()
        BersihkanSeleksi_WPF(datagridBayar, datatabelBayar, BarisBayar_Terseleksi, JumlahBarisBayar)
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
        VisibilitasTabelPembayaran()
    End Sub


    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        If TahunPajak = TahunBukuAktif Then
            SaldoAwal_BerdasarkanList = Total_SisaHutangPPN
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak, SaldoAkhir_BerdasarkanCOA, txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub CekKesesuaianSaldoAwal()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian, KesesuaianSaldoAwal,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_SaldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList, SaldoAkhir_BerdasarkanCOA, KesesuaianSaldoAkhir,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_SaldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP)
    End Sub

    Private Sub txt_TotalTabel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel)
    End Sub

    '=======================================================================================================================================


    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_BPHP As New DataGridTextColumn
    Dim Bulan_ As New DataGridTextColumn
    Dim Tanggal_Lapor As New DataGridTextColumn
    Dim Nomor_ID_Lapor As New DataGridTextColumn
    Dim TW_TL_Lapor As New DataGridTextColumn
    Dim N_P_Lapor As New DataGridTextColumn
    Dim Pajak_Keluaran_Dibayar As New DataGridTextColumn
    Dim Pajak_Keluaran_Dipungut As New DataGridTextColumn
    Dim Pajak_Keluaran_Tidak_Dipungut As New DataGridTextColumn
    Dim Pajak_Keluaran_Retur As New DataGridTextColumn
    Dim Pajak_Keluaran_Jumlah As New DataGridTextColumn
    Dim Pajak_Masukan_Impor As New DataGridTextColumn
    Dim Pajak_Masukan_Dalam_Negeri As New DataGridTextColumn
    Dim Pajak_Masukan_Retur As New DataGridTextColumn
    Dim Pajak_Masukan_Kompensasi_Sebelumnya As New DataGridTextColumn
    Dim Pajak_Masukan_Kompensasi_Pembetulan As New DataGridTextColumn
    Dim Pajak_Masukan_Jumlah As New DataGridTextColumn
    Dim PPN_NKL As New DataGridTextColumn
    Dim Selisih_Pembetulan As New DataGridTextColumn
    Dim Kompensasi_Ke As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Sisa_Hutang As New DataGridTextColumn
    Dim PPN_Tidak_Dapat_Dikreditkan As New DataGridTextColumn
    Dim Peredaran_Usaha_Lokal As New DataGridTextColumn
    Dim Peredaran_Usaha_Ekspor As New DataGridTextColumn
    Dim Retur_Penjualan As New DataGridTextColumn
    Dim Peredaran_Usaha_Jumlah As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPHP")
        datatabelUtama.Columns.Add("Bulan_")
        datatabelUtama.Columns.Add("Tanggal_Lapor")
        datatabelUtama.Columns.Add("Nomor_ID_Lapor")
        datatabelUtama.Columns.Add("TW_TL_Lapor")
        datatabelUtama.Columns.Add("N_P_Lapor")
        datatabelUtama.Columns.Add("Pajak_Keluaran_Dibayar", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Keluaran_Dipungut", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Keluaran_Tidak_Dipungut", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Keluaran_Retur", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Keluaran_Jumlah", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Dalam_Negeri", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Retur", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Kompensasi_Sebelumnya", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Kompensasi_Pembetulan", GetType(Int64))
        datatabelUtama.Columns.Add("Pajak_Masukan_Jumlah", GetType(Int64))
        datatabelUtama.Columns.Add("PPN_NKL", GetType(Int64))
        datatabelUtama.Columns.Add("Selisih_Pembetulan", GetType(Int64))
        datatabelUtama.Columns.Add("Kompensasi_Ke")
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Hutang", GetType(Int64))
        datatabelUtama.Columns.Add("PPN_Tidak_Dapat_Dikreditkan", GetType(Int64))
        datatabelUtama.Columns.Add("Peredaran_Usaha_Lokal", GetType(Int64))
        datatabelUtama.Columns.Add("Peredaran_Usaha_Ekspor", GetType(Int64))
        datatabelUtama.Columns.Add("Retur_Penjualan", GetType(Int64))
        datatabelUtama.Columns.Add("Peredaran_Usaha_Jumlah", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 33, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "ID", 33, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPHP, "Nomor_BPHP", "Nomor BPHP", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bulan_, "Bulan_", "Bulan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Lapor, "Tanggal_Lapor", "Lapor", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID_Lapor, "Nomor_ID_Lapor", "ID Lapor", 33, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, TW_TL_Lapor, "TW_TL_Lapor", "TW" & Enter1Baris & "TL", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, N_P_Lapor, "N_P_Lapor", "N/P", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Keluaran_Dibayar, "Pajak_Keluaran_Dibayar", "Dibayar", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Keluaran_Dipungut, "Pajak_Keluaran_Dipungut", "Dipungut", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Keluaran_Tidak_Dipungut, "Pajak_Keluaran_Tidak_Dipungut", "Tidak Dipungut", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Keluaran_Retur, "Pajak_Keluaran_Retur", "Retur Keluaran", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Keluaran_Jumlah, "Pajak_Keluaran_Jumlah", "Jumlah Keluaran", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Impor, "Pajak_Masukan_Impor", "Impor", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Dalam_Negeri, "Pajak_Masukan_Dalam_Negeri", "Dalam Negeri", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Retur, "Pajak_Masukan_Retur", "Retur Masukan", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Kompensasi_Sebelumnya, "Pajak_Masukan_Kompensasi_Sebelumnya", "Komp. Sebelumnya", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Kompensasi_Pembetulan, "Pajak_Masukan_Kompensasi_Pembetulan", "Komp. Pembetulan", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pajak_Masukan_Jumlah, "Pajak_Masukan_Jumlah", "Jumlah Masukan", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_NKL, "PPN_NKL", "PPN NKL", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Selisih_Pembetulan, "Selisih_Pembetulan", "Selisih" & Enter1Baris & "Pembetulan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kompensasi_Ke, "Kompensasi_Ke", "Kompensasi" & Enter1Baris & "Ke", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Hutang, "Sisa_Hutang", "Sisa Hutang", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_Tidak_Dapat_Dikreditkan, "PPN_Tidak_Dapat_Dikreditkan", "PPN Tidak Dapat" & Enter1Baris & "Dikreditkan", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Peredaran_Usaha_Lokal, "Peredaran_Usaha_Lokal", "Peredaran" & Enter1Baris & "Lokal", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Peredaran_Usaha_Ekspor, "Peredaran_Usaha_Ekspor", "Peredaran" & Enter1Baris & "Ekspor", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_Penjualan, "Retur_Penjualan", "Retur" & Enter1Baris & "Penjualan", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Peredaran_Usaha_Jumlah, "Peredaran_Usaha_Jumlah", "Jumlah" & Enter1Baris & "Peredaran", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomTerseleksiBayar As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim TW_TL_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("TW_TL_Bayar")
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 93, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, TW_TL_Bayar, "TW_TL_Bayar", "TW/TL", 48, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 33, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        cmb_TahunPajak.IsReadOnly = True
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
        pnl_Footer.Width = LebarKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
