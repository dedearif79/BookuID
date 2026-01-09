Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm


Public Class wpfUsc_BukuPengawasanPajakImpor

    Public StatusAktif As Boolean = False

    Public JudulForm
    Public JenisPajak
    'Public JenisTampilan
    'Public JenisTampilan_ALL = "ALL"
    'Public JenisTampilan_REKAP = "REKAP"
    'Public JenisTampilan_DETAIL = "DETAIL"
    Public MasaPajak_All = "ALL"
    Public MasaPajak_Rekap = "REKAP"
    Public MasaPajak_Angka As Integer

    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama
    Public TahunBukuSumberData

    Public NamaHalaman As String
    Public AwalanBP As String
    Public COABeaMasukImpor As String
    Public COAPPhPasal22Impor As String
    Public COAPPNMasukanImpor As String

    Dim Baris_Terseleksi
    'Dim NomorUrut_Terseleksi
    'Dim NomorID_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    'Dim TanggalInvoice_Terseleksi
    'Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NamaJasa_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim NPWP_Terseleksi
    Dim DPP_Terseleksi
    Dim PPh_Terseleksi
    'Dim BeaMasukImpor_Terseleksi
    'Dim PPhPasal22Impor_Terseleksi
    'Dim PPNMasukanImpor_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar100_Terseleksi
    Dim JumlahBayar101_Terseleksi
    Dim JumlahBayar102_Terseleksi
    Dim JumlahBayar103_Terseleksi
    Dim JumlahBayar104_Terseleksi
    'Dim JumlahBayar_Terseleksi
    Dim SisaHutang100_Terseleksi
    Dim SisaHutang101_Terseleksi
    Dim SisaHutang102_Terseleksi
    Dim SisaHutang103_Terseleksi
    Dim SisaHutang104_Terseleksi
    Dim SisaHutang_Terseleksi
    Dim KodeSetoran_Terseleksi
    Dim Keterangan_Terseleksi

    'Kolom Lapor :
    Dim TanggalLapor
    Dim NomorIDLapor
    Dim TWTL_Lapor
    Dim NP_Lapor
    Dim TanggalLapor_Terseleksi
    Dim NomorIDLapor_Terseleksi
    Dim NP_Lapor_Terseleksi


    Dim SisaHutang_SaatCutOff_100
    Dim SisaHutang_SaatCutOff_101
    Dim SisaHutang_SaatCutOff_102
    Dim SisaHutang_SaatCutOff_103
    Dim SisaHutang_SaatCutOff_104

    Dim MasaPajak = Kosongan
    Dim TahunPajakSebelumIni


    Dim NomorUrut
    Dim NomorID
    Dim NomorPIB
    Dim TanggalPIB
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim BeaMasukImpor
    Dim PPhPasal22Impor
    Dim PPNMasukanImpor
    Dim JumlahPajakImpor
    Dim TanggalBayar
    Dim NomorJV_Bayar

    Dim TotalTagihan_PokokPajak
    Dim TotalTagihan_Sanksi
    Dim TotalTagihan_JumlahKetetapan
    Dim TotalBayar
    Dim Total_SisaTagihan

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorPIB_Terseleksi
    Dim TanggalPIB_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim BeaMasukImpor_Terseleksi
    Dim PPhPasal22Impor_Terseleksi
    Dim PPNMasukanImpor_Terseleksi
    Dim JumlahPajakImpor_Terseleksi
    Dim TanggalBayar_Terseleksi
    Dim NomorJV_Terseleksi



    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    'Dim NomorUrut
    'Dim NomorID
    'Dim NomorBulan
    'Dim NomorBPHP
    'Dim Bulan = Kosongan
    'Dim TanggalInvoice
    'Dim NomorInvoice
    Dim JenisPembelian
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim NPWP
    Dim KodeSupplier
    Dim NamaSupplier
    'Dim DPP100
    'Dim DPP101
    'Dim DPP102
    'Dim DPP103
    'Dim DPP104
    'Dim DPP
    'Dim RekapPerBulanDPP100
    'Dim RekapPerBulanDPP101
    'Dim RekapPerBulanDPP102
    'Dim RekapPerBulanDPP103
    'Dim RekapPerBulanDPP104
    'Dim RekapPerBulanDPP
    'Dim BeaMasukImpor
    'Dim PPhPasal22Impor
    'Dim PPNMasukanImpor
    Dim PPh
    Dim RekapPerBulanBeaMasukImpor
    Dim RekapPerBulanPPhPasal22Impor
    Dim RekapPerBulanPPNMasukanImpor
    Dim RekapPerBulanPPh103
    Dim RekapPerBulanPPh104
    Dim RekapPerBulanPPh
    Dim JumlahTagihan
    Dim TanggalTransaksi
    Dim JumlahBayar100
    Dim JumlahBayar101
    Dim JumlahBayar102
    Dim JumlahBayar103
    Dim JumlahBayar104
    'Dim JumlahBayar
    Dim SisaHutang
    Dim JenisKodeSetoran
    Dim Keterangan

    'Dim TotalDPP100
    'Dim TotalDPP101
    'Dim TotalDPP102
    'Dim TotalDPP103
    'Dim TotalDPP104
    'Dim TotalDPP
    Dim TotalTagihan100
    Dim TotalTagihan101
    Dim TotalTagihan102
    Dim TotalTagihan103
    Dim TotalTagihan104
    Dim TotalTagihan
    Dim TotalBayar100
    Dim TotalBayar101
    Dim TotalBayar102
    Dim TotalBayar103
    Dim TotalBayar104
    'Dim TotalBayar
    Dim TotalSisaHutang

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim NomorInvoice_Sebelumnya

    'Dim KodeSetoran_UntukBayar
    'Dim KodeSetoran_UntukTabel

    'Dim SumberData
    'Dim SumberData_SisaHutangPajak = "Sisa Hutang Pajak"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        JudulForm = frm_BukuPengawasanPajakImpor.JudulForm
        lbl_JudulForm.Text = frm_BukuPengawasanPajakImpor.JudulForm

        VisibilitasInfoSaldo(False)

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            VisibilitasTombolJurnal(False)
            VisibilitasTombolCRUD(True)
            VisibilitasTombolUpdateBayar(True)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            VisibilitasTombolJurnal(True)
            VisibilitasTombolCRUD(False)
            VisibilitasTombolUpdateBayar(False)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        'KontenCombo_TahunPajak()
        'KontenCombo_MasaPajak()

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub TampilkanData()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Judul Halaman :
        frm_BukuPengawasanPajakImpor.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        KesesuaianJurnal_100 = True
        KesesuaianJurnal_101 = True
        KesesuaianJurnal_102 = True

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel :
        NomorUrut = 0
        NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
        TanggalTransaksi = Kosongan
        NomorInvoice = Kosongan
        NomorInvoice_Sebelumnya = Kosongan
        NomorFakturPajak = Kosongan
        NamaJasa = Kosongan
        NPWP = Kosongan
        KodeSupplier = Kosongan
        NamaSupplier = Kosongan
        Keterangan = Kosongan

        TotalTagihan100 = 0
        TotalTagihan101 = 0
        TotalTagihan102 = 0
        TotalTagihan103 = 0
        TotalTagihan104 = 0
        TotalTagihan = 0

        TotalBayar100 = 0
        TotalBayar101 = 0
        TotalBayar102 = 0
        TotalBayar103 = 0
        TotalBayar104 = 0
        TotalBayar = 0

        TotalSisaHutang = 0


        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' " &
                              " AND Bea_Masuk > 0 ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()


        Do While dr.Read

            NomorID = dr.Item("Nomor_ID")
            NomorPIB = dr.Item("Nomor_Faktur_Pajak")
            TanggalPIB = TanggalFormatTampilan(dr.Item("Tanggal_Faktur_Pajak"))
            If TanggalPIB = TanggalKosong Then TanggalPIB = Kosongan
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            BeaMasukImpor = dr.Item("Bea_Masuk")
            If dr.Item("Jenis_PPh") = JenisPPh_Pasal22_Impor Then
                PPhPasal22Impor = dr.Item("PPh_Terutang")
            Else
                PPhPasal22Impor = 0
            End If
            PPNMasukanImpor = dr.Item("PPN")
            JumlahPajakImpor = BeaMasukImpor + PPhPasal22Impor + PPNMasukanImpor
            TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar_Pajak_Impor"))
            If TanggalBayar = TanggalKosong Then TanggalBayar = StripKosong
            NomorJV_Bayar = dr.Item("Nomor_JV_Bayar_Pajak_Impor")

            TambahBaris()

            NomorInvoice_Sebelumnya = NomorInvoice

        Loop

        'Select Case JenisTahunBuku
        '    Case JenisTahunBuku_LAMPAU
        '        'Kode Setoran : 100
        '        SaldoAkhir_BerdasarkanList_100 = SisaHutang_SaatCutOff_100
        '        txt_SaldoBerdasarkanList_100.Text = SaldoAkhir_BerdasarkanList_100
        '        AmbilValue_SaldoAkhirBerdasarkanCOA_100()
        '        CekKesesuaianSaldoAkhir_100()
        '        txt_SelisihSaldo_100.Text = SaldoAkhir_BerdasarkanList_100 - SaldoAkhir_BerdasarkanCOA_100
        '        'Kode Setoran : 101
        '        SaldoAkhir_BerdasarkanList_101 = SisaHutang_SaatCutOff_101
        '        txt_SaldoBerdasarkanList_101.Text = SaldoAkhir_BerdasarkanList_101
        '        AmbilValue_SaldoAkhirBerdasarkanCOA_101()
        '        CekKesesuaianSaldoAkhir_101()
        '        txt_SelisihSaldo_101.Text = SaldoAkhir_BerdasarkanList_101 - SaldoAkhir_BerdasarkanCOA_101
        '        'Kode Setoran : 102
        '        SaldoAkhir_BerdasarkanList_102 = SisaHutang_SaatCutOff_102
        '        txt_SaldoBerdasarkanList_102.Text = SaldoAkhir_BerdasarkanList_102
        '        AmbilValue_SaldoAkhirBerdasarkanCOA_102()
        '        CekKesesuaianSaldoAkhir_102()
        '        txt_SelisihSaldo_102.Text = SaldoAkhir_BerdasarkanList_102 - SaldoAkhir_BerdasarkanCOA_102
        '    Case JenisTahunBuku_NORMAL
        '        'Penjelasan : Variabel-variabel di bawah ini untuk mendapatkan jumlah bayar atas hutang pajak tahun sebelum TahunBukuAktif,
        '        'tapi dibayarkan pada tahun ini (TahunBukuAktif).
        '        Dim TotalBayar_UntukHutangPajakTahunSebelumIni_100 As Int64 = 0
        '        Dim TotalBayar_UntukHutangPajakTahunSebelumIni_101 As Int64 = 0
        '        Dim TotalBayar_UntukHutangPajakTahunSebelumIni_102 As Int64 = 0
        '        Dim TotalBayar_UntukHutangPajakTahunSebelumIni_103 As Int64 = 0
        '        Dim TotalBayar_UntukHutangPajakTahunSebelumIni_104 As Int64 = 0
        '        AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_100, TotalBayar_UntukHutangPajakTahunSebelumIni_100)
        '        AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_101, TotalBayar_UntukHutangPajakTahunSebelumIni_101)
        '        AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_102, TotalBayar_UntukHutangPajakTahunSebelumIni_102)
        '        AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_103, TotalBayar_UntukHutangPajakTahunSebelumIni_103)
        '        AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_104, TotalBayar_UntukHutangPajakTahunSebelumIni_104)
        '        If Not TahunBukuSudahStabil(TahunBukuAktif) Then
        '            'Kode Setoran : 100
        '            AmbilValue_SaldoAwalBerdasarkanList_100()
        '            AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_100()
        '            CekKesesuaianSaldoAwal_100()
        '            txt_SelisihSaldo_100.Text = SaldoAwal_BerdasarkanList_100 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100
        '            txt_TotalTabel_100.Text = SaldoAwal_BerdasarkanList_100 + TotalTagihan100 - (TotalBayar100 + TotalBayar_UntukHutangPajakTahunSebelumIni_100)
        '            'Kode Setoran : 101
        '            AmbilValue_SaldoAwalBerdasarkanList_101()
        '            AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_101()
        '            CekKesesuaianSaldoAwal_101()
        '            txt_SelisihSaldo_101.Text = SaldoAwal_BerdasarkanList_101 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101
        '            txt_TotalTabel_101.Text = SaldoAwal_BerdasarkanList_101 + TotalTagihan101 - (TotalBayar101 + TotalBayar_UntukHutangPajakTahunSebelumIni_101)
        '            'Kode Setoran : 102
        '            AmbilValue_SaldoAwalBerdasarkanList_102()
        '            AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_102()
        '            CekKesesuaianSaldoAwal_102()
        '            txt_SelisihSaldo_102.Text = SaldoAwal_BerdasarkanList_102 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_102
        '            txt_TotalTabel_102.Text = SaldoAwal_BerdasarkanList_102 + TotalTagihan102 - (TotalBayar102 + TotalBayar_UntukHutangPajakTahunSebelumIni_102)
        '        Else
        '            txt_TotalTabel_100.Text = SaldoAwal_BerdasarkanCOA_100 + TotalTagihan100 - (TotalBayar100 + TotalBayar_UntukHutangPajakTahunSebelumIni_100)
        '            txt_TotalTabel_101.Text = SaldoAwal_BerdasarkanCOA_101 + TotalTagihan101 - (TotalBayar101 + TotalBayar_UntukHutangPajakTahunSebelumIni_101)
        '            txt_TotalTabel_102.Text = SaldoAwal_BerdasarkanCOA_102 + TotalTagihan102 - (TotalBayar102 + TotalBayar_UntukHutangPajakTahunSebelumIni_102)
        '        End If
        '        txt_TotalTabel_Total.Text _
        '            = AmbilAngka(txt_TotalTabel_100.Text) _
        '            + AmbilAngka(txt_TotalTabel_101.Text) _
        '            + AmbilAngka(txt_TotalTabel_102.Text)
        'End Select

        'lbl_TotalTabel.Text = "Saldo Akhir " & TahunPajak & " : "

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        If NomorInvoice = NomorInvoice_Sebelumnya Then Return
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorPIB, TanggalPIB, NomorInvoice, TanggalInvoice, BeaMasukImpor, PPhPasal22Impor, PPNMasukanImpor, JumlahPajakImpor, TanggalBayar, NomorJV_Bayar)
        Index_BarisTabel += 1
        Terabas()
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_InputBayar.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        KetersediaanMenuHalaman(pnl_Halaman, True)
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

        'KodeSetoran_UntukTabel = dr.Item("Kode_Setoran")
        'JenisKodeSetoran = PenentuanJenisKodeSetoran(JenisPajak, KodeSetoran_UntukTabel)

        'Select Case KodeSetoran_UntukTabel
        '    Case KodeSetoran_100 'Sewa Asset
        '        BeaMasukImpor = PPh
        '        RekapPerBulanBeaMasukImpor += PPh
        '    Case KodeSetoran_101 'Dividen
        '        PPhPasal22Impor = PPh
        '        RekapPerBulanPPhPasal22Impor += PPh
        '    Case KodeSetoran_102 'Bunga
        '        PPNMasukanImpor = PPh
        '        RekapPerBulanPPNMasukanImpor += PPh
        'End Select

        'RekapPerBulanPPh += PPh

    End Sub

    Sub Baris_KetetapanPajak()

        Dim JenisPajak_YangDitelusuri = JenisPajak
        Dim NomorBPHP_KetetapanPajak = Kosongan
        Dim JumlahTagihan_KetetapanPajak
        Dim JumlahBayar_KetetapanPajak
        Dim SisaHutang_KetetapanPajak

        BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
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

        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, "Ketetapan Pajak",
                                Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 0, JumlahTagihan_KetetapanPajak,
                                0, 0, 0, 0, 0, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak)

        TotalTagihan += JumlahTagihan_KetetapanPajak
        TotalBayar += JumlahBayar_KetetapanPajak
        TotalSisaHutang += SisaHutang_KetetapanPajak

    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Sub VisibilitasObjek_REKAP()
        'btn_Edit.IsEnabled = False
        'btn_Hapus.IsEnabled = False
        'Nomor_BPHP.Visibility = Visibility.Collapsed
        'Bulan_.Visibility = Visibility.Visible
        'Bulan_.Header = "Masa Pajak"
        'Tanggal_Transaksi.Visibility = Visibility.Collapsed
        'Tanggal_Invoice.Visibility = Visibility.Collapsed
        'Nomor_Invoice.Visibility = Visibility.Collapsed
        'Nomor_Faktur_Pajak.Visibility = Visibility.Collapsed
        'Nama_Jasa.Visibility = Visibility.Collapsed
        'NPWP_.Visibility = Visibility.Collapsed
        'Nama_Supplier.Visibility = Visibility.Collapsed
        'DPP_.Header = "Jumlah" & Enter1Baris & "DPP"
        'Jumlah_Bayar_Pajak.Visibility = Visibility.Visible
        'Sisa_Hutang_Pajak.Visibility = Visibility.Visible
        'Kode_Setoran.Visibility = Visibility.Collapsed
        'Jenis_Kode_Setoran.Visibility = Visibility.Collapsed
        'Keterangan_.Visibility = Visibility.Collapsed
    End Sub

    Sub VisibilitasObjek_DETAIL()
        'grb_InfoSaldo.Visibility = Visibility.Collapsed
        'grb_InfoSaldo.Visibility = Visibility.Collapsed
        'pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        'pnl_SidebarKanan.Visibility = Visibility.Collapsed
        'Nomor_BPHP.Visibility = Visibility.Collapsed
        'Bulan_.Visibility = Visibility.Collapsed
        'Tanggal_Transaksi.Visibility = Visibility.Visible
        'Tanggal_Invoice.Visibility = Visibility.Visible
        'Nomor_Invoice.Visibility = Visibility.Visible
        'Nomor_Faktur_Pajak.Visibility = Visibility.Visible
        'Nama_Jasa.Visibility = Visibility.Visible
        'NPWP_.Visibility = Visibility.Visible
        'Nama_Supplier.Visibility = Visibility.Visible
        'DPP_.Visibility = Visibility.Visible
        'DPP_.Header = "DPP"
        'Jumlah_Bayar_Pajak.Visibility = Visibility.Collapsed
        'Sisa_Hutang_Pajak.Visibility = Visibility.Collapsed
        'Kode_Setoran.Visibility = Visibility.Visible
        'Jenis_Kode_Setoran.Visibility = Visibility.Visible
        'Keterangan_.Visibility = Visibility.Visible
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

    Sub VisibilitasTombolCRUD(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_CRUD.Visibility = Visibility.Visible
        Else
            pnl_CRUD.Visibility = Visibility.Collapsed
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
        Else
            brd_DetailPembayaran.Visibility = Visibility.Collapsed
            btn_DetailPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolUpdateBayar(Visibilitas As Boolean)
        If Visibilitas Then
            btn_EditBayar.Visibility = Visibility.Visible
            btn_HapusBayar.Visibility = Visibility.Visible
        Else
            btn_EditBayar.Visibility = Visibility.Collapsed
            btn_HapusBayar.Visibility = Visibility.Collapsed
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
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTabelPembayaran()
        'If JumlahBarisBayar > 0 Then
        '    datagridBayar.Visibility = Visibility.Visible
        'Else
        '    datagridBayar.Visibility = Visibility.Collapsed
        'End If
    End Sub



    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputHutangPajak = New wpfWin_InputHutangPajak
        win_InputHutangPajak.ResetForm()
        win_InputHutangPajak.FungsiForm = FungsiForm_TAMBAH
        IsiValueComboBypassTerkunci(win_InputHutangPajak.cmb_JenisPajak, JenisPajak)
        win_InputHutangPajak.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputHutangPajak = New wpfWin_InputHutangPajak
        win_InputHutangPajak.ResetForm()
        win_InputHutangPajak.FungsiForm = FungsiForm_EDIT
        win_InputHutangPajak.BulanTransaksi = MasaPajak_Angka
        ProsesLoadingForm = True
        win_InputHutangPajak.NomorId = NomorID_Terseleksi
        win_InputHutangPajak.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi_Terseleksi)
        win_InputHutangPajak.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        win_InputHutangPajak.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputHutangPajak.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        win_InputHutangPajak.txt_NamaJasa.Text = NamaJasa_Terseleksi
        win_InputHutangPajak.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputHutangPajak.txt_DPP.Text = DPP_Terseleksi
        IsiValueComboBypassTerkunci(win_InputHutangPajak.cmb_JenisPajak, JenisPajak)
        win_InputHutangPajak.cmb_KodeSetoran.SelectedValue = KodeSetoran_Terseleksi
        win_InputHutangPajak.txt_JumlahHutang.Text = PPh_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangPajak.txt_Keterangan, Keterangan_Terseleksi)
        ProsesLoadingForm = False
        win_InputHutangPajak.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

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



    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaHutang_Terseleksi <= 0 Then
            Pesan_Informasi("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim JumlahTagihan As Int64 = 0
        Dim SisaHutang As Int64 = 0
        Dim JumlahBayar
        Dim KodeSetoran = Kosongan

        JumlahBayar = 0
        JumlahTagihan = JumlahPajakImpor_Terseleksi
        SisaHutang = SisaHutang_Terseleksi
        KodeSetoran = KodeSetoran_Non

        Dim TanggalTagihanPajak = TanggalInvoice_Terseleksi
        NomorBPHP_Terseleksi = NomorInvoice_Terseleksi                              'Ini jangan dirubah...! Penting..! Ada patokan algoritma yang menggunakan ini.

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi                'Ini jangan dirubah...! Penting..! Ada patokan algoritma yang menggunakan ini.
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.datatabelUtama.Rows.Add(
            1, NomorInvoice_Terseleksi, TanggalTagihanPajak, "Pembayaran Bea Masuk Impor", NomorBPHP_Terseleksi,
            BeaMasukImpor_Terseleksi, 0, 0, 0, 0, BeaMasukImpor_Terseleksi,
            JenisPajak_BeaMasukImpor, KodeSetoran, 0, 0, 0,
            BeaMasukImpor_Terseleksi, 0)
        win_InputBuktiPengeluaran.datatabelUtama.Rows.Add(
            2, NomorInvoice_Terseleksi, TanggalTagihanPajak, "Pembayaran PPh Pasal 22 Impor", NomorBPHP_Terseleksi,
            PPhPasal22Impor_Terseleksi, 0, 0, 0, 0, PPhPasal22Impor_Terseleksi,
            JenisPajak_PPhPasal22_Impor, KodeSetoran_100, 0, 0, 0,
            PPhPasal22Impor_Terseleksi, 0)
        win_InputBuktiPengeluaran.datatabelUtama.Rows.Add(
            3, NomorInvoice_Terseleksi, TanggalTagihanPajak, "Pembayaran PPN Masukan Impor", NomorBPHP_Terseleksi,
            PPNMasukanImpor_Terseleksi, 0, 0, 0, 0, PPNMasukanImpor_Terseleksi,
            JenisPajak_PPN_Impor, KodeSetoran, 0, 0, 0,
            PPNMasukanImpor_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 3 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub



    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak
        frm_DetailPembayaranPajak.ShowDialog()
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
        If NomorUrut_Terseleksi = 0 Then
            BersihkanSeleksi()
            Return
        End If

        'NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        'Bulan_Terseleksi = rowviewUtama("Bulan_")
        'NomorBPHP_Terseleksi = rowviewUtama("Nomor_BPHP")
        'TanggalTransaksi_Terseleksi = rowviewUtama("Tanggal_Transaksi")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        'NomorFakturPajak_Terseleksi = rowviewUtama("Nomor_Faktur_Pajak")
        'NamaJasa_Terseleksi = rowviewUtama("Nama_Jasa")
        'KodeSupplier_Terseleksi = rowviewUtama("Kode_Supplier")
        'NamaSupplier_Terseleksi = rowviewUtama("Nama_Supplier")
        'NPWP_Terseleksi = rowviewUtama("NPWP_")
        'DPP_Terseleksi = AmbilAngka(rowviewUtama("DPP_"))
        'BeaMasukImpor_Terseleksi = AmbilAngka(rowviewUtama("PPh_100"))
        'PPhPasal22Impor_Terseleksi = AmbilAngka(rowviewUtama("PPh_101"))
        'PPNMasukanImpor_Terseleksi = AmbilAngka(rowviewUtama("PPh_102"))
        'PPh_Terseleksi = AmbilAngka(rowviewUtama("PPh_"))
        'JumlahTagihan_Terseleksi = AmbilAngka(rowviewUtama("PPh_"))
        BeaMasukImpor_Terseleksi = AmbilAngka(rowviewUtama("Bea_Masuk_Impor"))
        PPhPasal22Impor_Terseleksi = AmbilAngka(rowviewUtama("PPh_Pasal_22_Impor"))
        PPNMasukanImpor_Terseleksi = AmbilAngka(rowviewUtama("PPN_Masukan_Impor"))
        JumlahPajakImpor_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Pajak_Impor"))
        'JumlahBayar100_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_100"))
        'JumlahBayar101_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_101"))
        'JumlahBayar102_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_102"))
        'JumlahBayar103_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_103"))
        'JumlahBayar104_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_104"))
        'JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_Pajak"))
        'SisaHutang100_Terseleksi = BeaMasukImpor_Terseleksi - JumlahBayar100_Terseleksi
        'SisaHutang101_Terseleksi = PPhPasal22Impor_Terseleksi - JumlahBayar101_Terseleksi
        'SisaHutang102_Terseleksi = PPNMasukanImpor_Terseleksi - JumlahBayar102_Terseleksi
        SisaHutang_Terseleksi = JumlahPajakImpor_Terseleksi
        TanggalBayar_Terseleksi = rowviewUtama("Tanggal_Bayar")
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))
        'KodeSetoran_Terseleksi = rowviewUtama("Kode_Setoran")
        'Keterangan_Terseleksi = rowviewUtama("Keterangan_")

        ''Kolom Lapor :
        'TanggalLapor_Terseleksi = rowviewUtama("Tanggal_Lapor")
        'NomorIDLapor_Terseleksi = rowviewUtama("Nomor_ID_Lapor")
        'NP_Lapor_Terseleksi = rowviewUtama("N_P_Lapor")


        'If JenisTampilan = JenisTampilan_DETAIL Then
        '    If JenisTahunBuku = JenisTahunBuku_LAMPAU And NomorID_Terseleksi = 0 Then BersihkanSeleksi()
        'End If

        If JumlahPajakImpor_Terseleksi > 0 Then
            'ResetTampilanDataPembayaran()
        End If

        If NomorJV_Pembayaran_Terseleksi = 0 Then
            btn_InputBayar.IsEnabled = True
            btn_LihatJurnal.IsEnabled = False
        Else
            btn_InputBayar.IsEnabled = False
            btn_LihatJurnal.IsEnabled = True
        End If

        'If JenisTampilan = JenisTampilan_DETAIL Then
        '    If NomorUrut_Terseleksi > 0 Then
        '        btn_Edit.IsEnabled = True
        '        btn_Hapus.IsEnabled = True
        '    Else
        '        btn_Edit.IsEnabled = False
        '        btn_Hapus.IsEnabled = False
        '    End If
        'End If

        'If JenisTampilan = JenisTampilan_REKAP Then
        '    If JumlahBayar_Terseleksi > 0 And SisaHutang_Terseleksi <= 0 Then
        '        grb_LaporSPT.IsEnabled = True
        '        If TanggalLapor_Terseleksi = Kosongan Then
        '            btn_InputSPT.IsEnabled = True
        '            btn_EditSPT.IsEnabled = False
        '            btn_HapusSPT.IsEnabled = False
        '        Else
        '            btn_InputSPT.IsEnabled = False
        '            btn_EditSPT.IsEnabled = True
        '            btn_HapusSPT.IsEnabled = True
        '        End If
        '    Else
        '        grb_LaporSPT.IsEnabled = False
        '    End If
        'End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        'If JenisTampilan = JenisTampilan_DETAIL Or JenisTampilan = JenisTampilan_ALL Then
        '    If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
        '        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
        '            If NomorID_Terseleksi = 0 Then
        '                btn_Input_Click(sender, e)
        '            Else
        '                btn_Edit_Click(sender, e)
        '            End If
        '        End If
        '        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
        '            'belum ada codingnya.
        '        End If
        '    End If
        '    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
        '        'belum ada codingnya.
        '    End If
        'End If
        'If JenisTampilan = JenisTampilan_REKAP And cmb_MasaPajak.IsEnabled = True Then
        '    If NomorUrut_Terseleksi <> 0 Then cmb_MasaPajak.SelectedValue = datatabelUtama.Rows(BarisTerseleksi)("Bulan_")
        'End If
        'If Bulan_Terseleksi = JenisPajak_KetetapanPajak Then
        '    frm_BukuPengawasanKetetapanPajak.MdiParent = frm_BOOKU
        '    frm_BukuPengawasanKetetapanPajak.Show()
        '    frm_BukuPengawasanKetetapanPajak.Focus()
        '    usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak
        'End If
    End Sub


    'Private Sub rdb_KodeSetoran_100_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_100.Checked
    '    LogikaKodeSetoran()
    'End Sub
    'Private Sub rdb_KodeSetoran_101_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_101.Checked
    '    LogikaKodeSetoran()
    'End Sub
    'Private Sub rdb_KodeSetoran_102_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_102.Checked
    '    LogikaKodeSetoran()
    'End Sub
    'Sub LogikaKodeSetoran()
    '    If rdb_KodeSetoran_100.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_100
    '    If rdb_KodeSetoran_101.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_101
    '    If rdb_KodeSetoran_102.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_102
    '    If rdb_KodeSetoran_100.IsChecked = True _
    '        Or rdb_KodeSetoran_101.IsChecked = True _
    '        Or rdb_KodeSetoran_102.IsChecked = True _
    '        Then
    '        TampilkanDataPembayaran()
    '    Else
    '        KodeSetoran_UntukBayar = KodeSetoran_Non
    '    End If
    'End Sub



    'Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    'End Sub
    'Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
    '    HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
    '    If HeaderKolomBayar IsNot Nothing Then
    '        btn_LihatJurnal.IsEnabled = False
    '        BersihkanSeleksiTabelPembayaran()
    '    End If
    'End Sub
    'Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

    '    KolomTerseleksiBayar = datagridBayar.CurrentColumn
    '    BarisBayar_Terseleksi = datagridBayar.SelectedIndex
    '    If BarisBayar_Terseleksi < 0 Then Return
    '    rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
    '    If Not rowviewBayar IsNot Nothing Then Return

    '    NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
    '    NomorJV_Pembayaran_Terseleksi = rowviewBayar("Nomor_JV_Bayar")
    '    Referensi_Terseleksi = rowviewBayar("Referensi_")
    '    TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
    '    If BarisBayar_Terseleksi >= 0 Then
    '        btn_LihatJurnal.IsEnabled = True
    '        btn_EditBayar.IsEnabled = True
    '        btn_HapusBayar.IsEnabled = True
    '    Else
    '        btn_LihatJurnal.IsEnabled = False
    '        btn_EditBayar.IsEnabled = False
    '        btn_HapusBayar.IsEnabled = False
    '    End If
    '    If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
    '    If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
    '        'btn_EditPembayaran.isenabled = False
    '        'btn_HapusPembayaran.isenabled = False
    '    End If
    'End Sub
    'Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
    '    'Belum ada kebutuhan kode di sini.
    'End Sub




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
        win_InputLaporPajak.cmb_NP.SelectedValue = NP_Lapor_Terseleksi
        win_InputLaporPajak.dtp_TanggalLapor.SelectedDate = TanggalFormatWPF(TanggalLapor_Terseleksi)
        BukaFormInputLaporPajak()
    End Sub

    Sub BukaFormInputLaporPajak()
        ProsesIsiValueForm = True
        win_InputLaporPajak.JenisPajak = JenisPajak
        win_InputLaporPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        win_InputLaporPajak.NP = NP_Lapor_Terseleksi
        win_InputLaporPajak.txt_JumlahLebihBayar.Text = 0 'Untuk PPh, sementara di-nol-kan (0) dulu.
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

        PesanUntukProgrammer("Hapus juga data 'Tanggal Lapor' di masing-masing INVOICE terkait...!!! ")

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub




    'Sub ResetTampilanDataPembayaran()
    '    pnl_SidebarKanan.Visibility = Visibility.Visible
    '    datatabelBayar.Rows.Clear()
    'End Sub

    'Sub TampilkanDataPembayaran()

    '    pnl_DataGridBayar.Visibility = Visibility.Visible

    '    datatabelBayar.Rows.Clear()
    '    Dim Index_BarisTabelPembayaran = 0
    '    Dim NomorIdBayar
    '    Dim TanggalBayar
    '    Dim Referensi
    '    Dim JumlahBayar As Int64 = 0
    '    Dim TWTLBayar = Kosongan
    '    Dim TotalBayar As Int64 = 0
    '    Dim KeteranganBayar
    '    Dim NomorJV_Pembayaran
    '    Dim TahunSumberDataPembayaran = 0

    '    Dim TahunTelusurPembayaran = TahunPajak
    '    Dim PencegahLoopingTahunPajakLampau = 0
    '    Do While TahunTelusurPembayaran <= TahunBukuAktif
    '        If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
    '        If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
    '        If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
    '            BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
    '            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
    '                                  " WHERE Nomor_BP          = '" & NomorBPHP_Terseleksi & "' " &
    '                                  " AND Status_Invoice      = '" & Status_Dibayar & "' " &
    '                                  " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
    '            dr = cmd.ExecuteReader
    '            Do While dr.Read
    '                NomorIdBayar = dr.Item("Nomor_ID")
    '                TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
    '                Referensi = dr.Item("Nomor_KK")
    '                JumlahBayar = dr.Item("Jumlah_Bayar")
    '                TotalBayar += JumlahBayar
    '                KeteranganBayar = dr.Item("Catatan")
    '                If TahunTelusurPembayaran = TahunBukuAktif Then
    '                    NomorJV_Pembayaran = dr.Item("Nomor_JV")
    '                Else
    '                    NomorJV_Pembayaran = 0
    '                End If
    '                datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, TWTLBayar, KeteranganBayar, NomorJV_Pembayaran)
    '                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
    '                    If TahunTelusurPembayaran = TahunBukuAktif Then
    '                        'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
    '                    Else
    '                        'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
    '                    End If
    '                End If
    '                If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
    '                Index_BarisTabelPembayaran += 1
    '            Loop
    '            TutupDatabaseTransaksi_Alternatif()
    '        End If
    '        If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
    '        PencegahLoopingTahunPajakLampau += 1
    '        TahunTelusurPembayaran += 1
    '    Loop

    '    'BersihkanSeleksiTabelPembayaran()

    'End Sub

    'Sub BersihkanSeleksiTabelPembayaran()
    '    datagridBayar.SelectedIndex = -1
    '    datagridBayar.SelectedItem = Nothing
    '    datagridBayar.SelectedCells.Clear()
    '    BarisBayar_Terseleksi = -1
    '    JumlahBarisBayar = datatabelBayar.Rows.Count
    '    btn_EditBayar.IsEnabled = False
    '    btn_HapusBayar.IsEnabled = False
    '    NomorJV_Pembayaran_Terseleksi = 0
    '    VisibilitasTabelPembayaran()
    'End Sub


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
    Dim Nomor_PIB As New DataGridTextColumn
    Dim Tanggal_PIB As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Bea_Masuk_Impor As New DataGridTextColumn
    Dim PPh_Pasal_22_Impor As New DataGridTextColumn
    Dim PPN_Masukan_Impor As New DataGridTextColumn
    Dim Jumlah_Pajak_Impor As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_PIB")
        datatabelUtama.Columns.Add("Tanggal_PIB")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Bea_Masuk_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Pasal_22_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("PPN_Masukan_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Pajak_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Bayar")
        datatabelUtama.Columns.Add("Nomor_JV")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PIB, "Nomor_PIB", "Nomor PIB", 87, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PIB, "Tanggal_PIB", "Tanggal PIB", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 87, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bea_Masuk_Impor, "Bea_Masuk_Impor", "Bea Masuk Impor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Pasal_22_Impor, "PPh_Pasal_22_Impor", "PPh Pasal 22 Impor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_Masukan_Impor, "PPN_Masukan_Impor", "PPN Masukan Impor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pajak_Impor, "Jumlah_Pajak_Impor", "Jumlah Pajak-pajak Impor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal Bayar", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 72, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub


    ''Tabel Bayar :
    'Public datatabelBayar As DataTable
    'Public dataviewBayar As DataView
    'Public rowviewBayar As DataRowView
    'Public newRowBayar As DataRow
    'Public HeaderKolomBayar As DataGridColumnHeader
    'Public KolomTerseleksiBayar As DataGridColumn
    'Public BarisBayar_Terseleksi As Integer
    'Public JumlahBarisBayar As Integer

    'Dim Nomor_ID_Bayar As New DataGridTextColumn
    'Dim Tanggal_Bayar As New DataGridTextColumn
    'Dim Referensi_ As New DataGridTextColumn
    'Dim Nominal_Bayar As New DataGridTextColumn
    'Dim TW_TL_Bayar As New DataGridTextColumn
    'Dim Keterangan_Bayar As New DataGridTextColumn
    'Dim Nomor_JV_Bayar As New DataGridTextColumn

    'Sub Buat_DataTabelBayar()

    '    datatabelBayar = New DataTable
    '    datatabelBayar.Columns.Add("Nomor_ID_Bayar")
    '    datatabelBayar.Columns.Add("Tanggal_Bayar")
    '    datatabelBayar.Columns.Add("Referensi_")
    '    datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
    '    datatabelBayar.Columns.Add("TW_TL_Bayar")
    '    datatabelBayar.Columns.Add("Keterangan_Bayar")
    '    datatabelBayar.Columns.Add("Nomor_JV_Bayar")

    '    StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, TW_TL_Bayar, "TW_TL_Bayar", "TW/TL", 45, FormatString, TengahTengah, KunciUrut, Tersembunyi)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    'End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        'Buat_DataTabelBayar()
        datagridBayar.Visibility = Visibility.Collapsed
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        pnl_LaporSPT.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        '100
        txt_SaldoBerdasarkanList_100.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_100.IsReadOnly = True
        txt_SelisihSaldo_100.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_100.IsReadOnly = True
        txt_AJP_100.IsReadOnly = True
        txt_TotalTabel_100.IsReadOnly = True
        '101
        txt_SaldoBerdasarkanList_101.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_101.IsReadOnly = True
        txt_SelisihSaldo_101.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_101.IsReadOnly = True
        txt_AJP_101.IsReadOnly = True
        txt_TotalTabel_101.IsReadOnly = True
        '102
        txt_SaldoBerdasarkanList_102.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_102.IsReadOnly = True
        txt_SelisihSaldo_102.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_102.IsReadOnly = True
        txt_AJP_102.IsReadOnly = True
        txt_TotalTabel_102.IsReadOnly = True
        'Total
        txt_TotalTabel_Total.IsReadOnly = True
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
        StatusAktif = False
    End Sub


    Sub AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran As String, ByRef SaldoAwal_BerdasarkanList As Int64, txt_SaldoBerdasarkanList As TextBox)
        If TahunPajak = TahunBukuAktif Then
            Dim JumlahTagihan_SA As Int64
            Dim JumlahBayar_SA As Int64
            Dim TahunTelusur_SA = TahunBukuAktif - 1
            If TahunTelusur_SA = TahunCutOff Then
                JumlahTagihan_SA = 0
                BukaDatabaseTransaksi_Alternatif(TahunTelusur_SA)
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                         " WHERE Jenis_Pajak    = '" & JenisPajak & "' " &
                                         " AND Kode_Setoran     = '" & KodeSetoran & "' ",
                                         KoneksiDatabaseTransaksi_Alternatif)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                TutupDatabaseTransaksi_Alternatif()
            Else
                'Ini tidak diperlukan, karena ketika TahunBukuAktif sudah 2 tahun dari TahunCutOff, maka tidak perlu ada lagi pengecekan keseimbangan,
                'karena sudah dipastikan akan sesuai antara data finance dengan data akuntansi.
            End If
            AmbilValue_JumlahBayarPajakTahunBukuKemarin_Public(AwalanBP, KodeSetoran, JumlahBayar_SA)
            SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub







    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 100

    Public KesesuaianSaldoAwal_100 As Boolean
    Public KesesuaianSaldoAkhir_100 As Boolean
    Public KesesuaianJurnal_100 As Boolean

    Dim SaldoAwal_BerdasarkanList_100
    Dim SaldoAwal_BerdasarkanCOA_100
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100
    Dim SaldoAkhir_BerdasarkanList_100
    Dim SaldoAkhir_BerdasarkanCOA_100
    Dim JumlahPenyesuaianSaldo_100

    Sub AmbilValue_SaldoAwalBerdasarkanList_100()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_100, SaldoAwal_BerdasarkanList_100, txt_SaldoBerdasarkanList_100)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_100()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COABeaMasukImpor, SaldoAwal_BerdasarkanCOA_100, JumlahPenyesuaianSaldo_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100,
                                                                  txt_SaldoAwalBerdasarkanCOA_100, txt_AJP_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_100()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COABeaMasukImpor, SaldoAkhir_BerdasarkanCOA_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Sub CekKesesuaianSaldoAwal_100()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100, KesesuaianSaldoAwal_100,
                                      btn_Sesuaikan_100, txt_SaldoBerdasarkanList_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100, txt_SelisihSaldo_100)
    End Sub

    Sub CekKesesuaianSaldoAkhir_100()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_100, SaldoAkhir_BerdasarkanCOA_100, KesesuaianSaldoAkhir_100,
                                      btn_Sesuaikan_100, txt_SaldoBerdasarkanList_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100, txt_SelisihSaldo_100)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_100)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Private Sub txt_SelisihSaldo_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_100)
    End Sub

    Private Sub btn_Sesuaikan_100_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_100.Click
        SesuaikanSaldoAwal(NamaHalaman, COABeaMasukImpor, SaldoAwal_BerdasarkanList_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_100)
    End Sub

    Private Sub txt_AJP_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_100)
    End Sub

    Private Sub txt_TotalTabel_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_100)
    End Sub

    '=======================================================================================================================================



    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 101

    Public KesesuaianSaldoAwal_101 As Boolean
    Public KesesuaianSaldoAkhir_101 As Boolean
    Public KesesuaianJurnal_101 As Boolean

    Dim SaldoAwal_BerdasarkanList_101
    Dim SaldoAwal_BerdasarkanCOA_101
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101
    Dim SaldoAkhir_BerdasarkanList_101
    Dim SaldoAkhir_BerdasarkanCOA_101
    Dim JumlahPenyesuaianSaldo_101

    Sub AmbilValue_SaldoAwalBerdasarkanList_101()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_101, SaldoAwal_BerdasarkanList_101, txt_SaldoBerdasarkanList_101)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_101()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAPPhPasal22Impor, SaldoAwal_BerdasarkanCOA_101, JumlahPenyesuaianSaldo_101, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101,
                                                                  txt_SaldoAwalBerdasarkanCOA_101, txt_AJP_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_101()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAPPhPasal22Impor, SaldoAkhir_BerdasarkanCOA_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101)
    End Sub

    Sub CekKesesuaianSaldoAwal_101()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_101, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101, KesesuaianSaldoAwal_101,
                                      btn_Sesuaikan_101, txt_SaldoBerdasarkanList_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101, txt_SelisihSaldo_101)
    End Sub

    Sub CekKesesuaianSaldoAkhir_101()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_101, SaldoAkhir_BerdasarkanCOA_101, KesesuaianSaldoAkhir_101,
                                      btn_Sesuaikan_101, txt_SaldoBerdasarkanList_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101, txt_SelisihSaldo_101)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_101)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_101)
    End Sub

    Private Sub txt_SelisihSaldo_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_101)
    End Sub

    Private Sub btn_Sesuaikan_101_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_101.Click
        SesuaikanSaldoAwal(NamaHalaman, COAPPhPasal22Impor, SaldoAwal_BerdasarkanList_101, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_101)
    End Sub

    Private Sub txt_AJP_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_101)
    End Sub

    Private Sub txt_TotalTabel_101_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_101.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_101)
    End Sub

    '=======================================================================================================================================




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 102

    Public KesesuaianSaldoAwal_102 As Boolean
    Public KesesuaianSaldoAkhir_102 As Boolean
    Public KesesuaianJurnal_102 As Boolean

    Dim SaldoAwal_BerdasarkanList_102
    Dim SaldoAwal_BerdasarkanCOA_102
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_102
    Dim SaldoAkhir_BerdasarkanList_102
    Dim SaldoAkhir_BerdasarkanCOA_102
    Dim JumlahPenyesuaianSaldo_102

    Sub AmbilValue_SaldoAwalBerdasarkanList_102()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_102, SaldoAwal_BerdasarkanList_102, txt_SaldoBerdasarkanList_102)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_102()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAPPNMasukanImpor, SaldoAwal_BerdasarkanCOA_102, JumlahPenyesuaianSaldo_102, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_102,
                                                                  txt_SaldoAwalBerdasarkanCOA_102, txt_AJP_102, txt_saldoBerdasarkanCOA_PlusPenyesuaian_102)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_102()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAPPNMasukanImpor, SaldoAkhir_BerdasarkanCOA_102, txt_saldoBerdasarkanCOA_PlusPenyesuaian_102)
    End Sub

    Sub CekKesesuaianSaldoAwal_102()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_102, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_102, KesesuaianSaldoAwal_102,
                                      btn_Sesuaikan_102, txt_SaldoBerdasarkanList_102, txt_saldoBerdasarkanCOA_PlusPenyesuaian_102, txt_SelisihSaldo_102)
    End Sub

    Sub CekKesesuaianSaldoAkhir_102()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_102, SaldoAkhir_BerdasarkanCOA_102, KesesuaianSaldoAkhir_102,
                                      btn_Sesuaikan_102, txt_SaldoBerdasarkanList_102, txt_saldoBerdasarkanCOA_PlusPenyesuaian_102, txt_SelisihSaldo_102)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_102)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_102)
    End Sub

    Private Sub txt_SelisihSaldo_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_102)
    End Sub

    Private Sub btn_Sesuaikan_102_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_102.Click
        SesuaikanSaldoAwal(NamaHalaman, COAPPNMasukanImpor, SaldoAwal_BerdasarkanList_102, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_102)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_102)
    End Sub

    Private Sub txt_AJP_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_102)
    End Sub

    Private Sub txt_TotalTabel_102_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_102.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_102)
    End Sub

    '=======================================================================================================================================




    Private Sub txt_TotalTabel_Total_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_Total.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_Total)
    End Sub


End Class
