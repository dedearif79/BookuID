Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_JurnalAdjusment_HPP

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim KodeAkun
    Dim NamaAkun

    Dim KodeAkun_Terseleksi As String
    Dim NamaAkun_Terseleksi As String
    Dim Label_Terseleksi As String
    Dim BulanAngka_Terseleksi As Integer
    Dim KodeMataUang_Terseleksi As String
    Dim JenisAdjusment_Terseleksi As String


    Dim lbl_TombolJanuari As String
    Dim lbl_TombolFebruari As String
    Dim lbl_TombolMaret As String
    Dim lbl_TombolApril As String
    Dim lbl_TombolMei As String
    Dim lbl_TombolJuni As String
    Dim lbl_TombolJuli As String
    Dim lbl_TombolAgustus As String
    Dim lbl_TombolSeptember As String
    Dim lbl_TombolOktober As String
    Dim lbl_TombolNopember As String
    Dim lbl_TombolDesember As String

    Dim lbl_TombolJanuari_X As String
    Dim lbl_TombolFebruari_X As String
    Dim lbl_TombolMaret_X As String
    Dim lbl_TombolApril_X As String
    Dim lbl_TombolMei_X As String
    Dim lbl_TombolJuni_X As String
    Dim lbl_TombolJuli_X As String
    Dim lbl_TombolAgustus_X As String
    Dim lbl_TombolSeptember_X As String
    Dim lbl_TombolOktober_X As String
    Dim lbl_TombolNopember_X As String
    Dim lbl_TombolDesember_X As String

    Dim Adjusment_PemakaianBahanPenolong As String = "Pemakaian Bahan Penolong"
    Dim Adjusment_PemakaianBahanBaku As String = "Pemakaian Bahan Baku"
    Dim Adjusment_BiayaBahanBaku As String = "Biaya Bahan Baku"
    Dim Adjusment_BiayaTenagaKerjaLangsung As String = "Biaya Tenaga Kerja Langsung"
    Dim Adjusment_BiayaOverheadPabrik As String = "Biaya Overhead Pabrik"
    Dim Adjusment_BiayaProduksi As String = "Biaya Produksi"
    Dim Adjusment_HargaPokokProduksi As String = "Harga Pokok Produksi"
    Dim Adjusment_HargaPokokPenjualan As String = "Harga Pokok Penjualan"


    Public AdjusmentBulanBukuAktifSudahLengkap As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_Adjusment_HPP.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_TombolForm.Visibility = Visibility.Collapsed

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Dim Kolom As String = "Tanggal_Transaksi"
    Dim TabelDanKriteria As String
    Dim BulanTertuaAngka As Integer
    Sub TampilkanData()

        AdjusmentBulanBukuAktifSudahLengkap = True

        KetersediaanMenuHalaman(pnl_Halaman, False)

        UpdateInfoBulanBukuAktif()

        'Data Tabel :
        datatabelUtama.Clear()

        ResetTombol()

        'Pemakaian Bahan Penolong :
        TambahBarisAdjusment(Adjusment_PemakaianBahanPenolong, KodeTautanCOA_PersediaanBahanPenolong)

        'Pemakaian Bahan Baku :
        TambahBarisAdjusment(Adjusment_PemakaianBahanBaku, KodeTautanCOA_PersediaanBahanBaku_Lokal)

        'Biaya Bahan Baku : 
        TambahBarisAdjusment(Adjusment_BiayaBahanBaku, KodeTautanCOA_BiayaBahanBaku)

        'Biaya Tenaga Kerja Langsung :
        TambahBarisAdjusment(Adjusment_BiayaTenagaKerjaLangsung, KodeTautanCOA_BiayaTenagaKerjaLangsung)

        'Biaya Overhead Pabrik :
        TambahBarisAdjusment(Adjusment_BiayaOverheadPabrik, KodeTautanCOA_BiayaOverheadPabrik)

        'Biaya Produksi :
        TambahBarisAdjusment(Adjusment_BiayaProduksi, KodeTautanCOA_BiayaProduksi)

        'Harga Pokok Produksi :
        TambahBarisAdjusment(Adjusment_HargaPokokProduksi, KodeTautanCOA_HargaPokokProduksi)

        'Harga Pokok Penjualan :
        TambahBarisAdjusment(Adjusment_HargaPokokPenjualan, KodeTautanCOA_HargaPokokPenjualan)

        BersihkanSeleksi()

    End Sub


    Sub TambahBarisAdjusment(Adjusment As String, COA As String)
        TabelDanKriteria = " tbl_Transaksi WHERE COA = '" & COA & "' " & " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' "
        BulanTertuaAngka = AmbilValue_BulanTertuaAngka(TabelDanKriteria, Kolom)
        If BulanTertuaAngka > 12 Then BulanTertuaAngka = 12
        AlgoritmaTombol(Adjusment)
        datatabelUtama.Rows.Add(Adjusment, COA,
                                lbl_TombolJanuari, lbl_TombolFebruari, lbl_TombolMaret, lbl_TombolApril, lbl_TombolMei, lbl_TombolJuni,
        lbl_TombolJuli, lbl_TombolAgustus, lbl_TombolSeptember, lbl_TombolOktober, lbl_TombolNopember, lbl_TombolDesember)
        Terabas()
        Dim lbl_Tombol As String = Kosongan
        Select Case BulanTerakhirDitutup + 1
            Case 1
                lbl_Tombol = lbl_TombolJanuari
            Case 2
                lbl_Tombol = lbl_TombolFebruari
            Case 3
                lbl_Tombol = lbl_TombolMaret
            Case 4
                lbl_Tombol = lbl_TombolApril
            Case 5
                lbl_Tombol = lbl_TombolMei
            Case 6
                lbl_Tombol = lbl_TombolJuni
            Case 7
                lbl_Tombol = lbl_TombolJuli
            Case 8
                lbl_Tombol = lbl_TombolAgustus
            Case 9
                lbl_Tombol = lbl_TombolSeptember
            Case 10
                lbl_Tombol = lbl_TombolOktober
            Case 11
                lbl_Tombol = lbl_TombolNopember
            Case 12
                lbl_Tombol = lbl_TombolDesember
        End Select
        If lbl_Tombol = teks_Posting Then AdjusmentBulanBukuAktifSudahLengkap = False
    End Sub


    Sub AlgoritmaTombol(Adjusment As String)
        lbl_TombolJanuari_X = lbl_TombolJanuari
        lbl_TombolFebruari_X = lbl_TombolFebruari
        lbl_TombolMaret_X = lbl_TombolMaret
        lbl_TombolApril_X = lbl_TombolApril
        lbl_TombolMei_X = lbl_TombolMei
        lbl_TombolJuni_X = lbl_TombolJuni
        lbl_TombolJuli_X = lbl_TombolJuli
        lbl_TombolAgustus_X = lbl_TombolAgustus
        lbl_TombolSeptember_X = lbl_TombolSeptember
        lbl_TombolOktober_X = lbl_TombolOktober
        lbl_TombolNopember_X = lbl_TombolNopember
        lbl_TombolDesember_X = lbl_TombolDesember
        ResetTombol()
        If BulanTertuaAngka = 0 Then
            If lbl_TombolJanuari_X = teks_Lihat Then lbl_TombolJanuari = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolJanuari = teks_Posting
        End If
        If BulanTertuaAngka >= 1 Then
            lbl_TombolJanuari = teks_Lihat
            If lbl_TombolFebruari_X = teks_Lihat Then lbl_TombolFebruari = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolFebruari = teks_Posting
        End If
        If BulanTertuaAngka >= 2 Then
            lbl_TombolFebruari = teks_Lihat
            If lbl_TombolMaret_X = teks_Lihat Then lbl_TombolMaret = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolMaret = teks_Posting
        End If
        If BulanTertuaAngka >= 3 Then
            lbl_TombolMaret = teks_Lihat
            If lbl_TombolApril_X = teks_Lihat Then lbl_TombolApril = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolApril = teks_Posting
        End If
        If BulanTertuaAngka >= 4 Then
            lbl_TombolApril = teks_Lihat
            If lbl_TombolMei_X = teks_Lihat Then lbl_TombolMei = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolMei = teks_Posting
        End If
        If BulanTertuaAngka >= 5 Then
            lbl_TombolMei = teks_Lihat
            If lbl_TombolJuni_X = teks_Lihat Then lbl_TombolJuni = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolJuni = teks_Posting
        End If
        If BulanTertuaAngka >= 6 Then
            lbl_TombolJuni = teks_Lihat
            If lbl_TombolJuli_X = teks_Lihat Then lbl_TombolJuli = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolJuli = teks_Posting
        End If
        If BulanTertuaAngka >= 7 Then
            lbl_TombolJuli = teks_Lihat
            If lbl_TombolAgustus_X = teks_Lihat Then lbl_TombolAgustus = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolAgustus = teks_Posting
        End If
        If BulanTertuaAngka >= 8 Then
            lbl_TombolAgustus = teks_Lihat
            If lbl_TombolSeptember_X = teks_Lihat Then lbl_TombolSeptember = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolSeptember = teks_Posting
        End If
        If BulanTertuaAngka >= 9 Then
            lbl_TombolSeptember = teks_Lihat
            If lbl_TombolOktober_X = teks_Lihat Then lbl_TombolOktober = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolOktober = teks_Posting
        End If
        If BulanTertuaAngka >= 10 Then
            lbl_TombolOktober = teks_Lihat
            If lbl_TombolNopember_X = teks_Lihat Then lbl_TombolNopember = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolNopember = teks_Posting
        End If
        If BulanTertuaAngka >= 11 Then
            lbl_TombolNopember = teks_Lihat
            If lbl_TombolDesember_X = teks_Lihat Then lbl_TombolDesember = teks_Posting
            If Adjusment = Adjusment_PemakaianBahanPenolong Then lbl_TombolDesember = teks_Posting
        End If
        If BulanTertuaAngka = 12 Then
            lbl_TombolDesember = teks_Lihat
        End If
        AlgoritmaTombolBerdasarkanBulanTerakhirDitutup()
        AlgoritmaTombolUntukTahunBukuBerjalan()
    End Sub

    Sub ResetTombol()
        lbl_TombolJanuari = Kosongan
        lbl_TombolFebruari = Kosongan
        lbl_TombolMaret = Kosongan
        lbl_TombolApril = Kosongan
        lbl_TombolMei = Kosongan
        lbl_TombolJuni = Kosongan
        lbl_TombolJuli = Kosongan
        lbl_TombolAgustus = Kosongan
        lbl_TombolSeptember = Kosongan
        lbl_TombolOktober = Kosongan
        lbl_TombolNopember = Kosongan
        lbl_TombolDesember = Kosongan
    End Sub

    Sub AlgoritmaTombolBerdasarkanBulanTerakhirDitutup()
        If BulanTerakhirDitutup = 0 Then lbl_TombolFebruari = Kosongan
        If BulanTerakhirDitutup <= 1 Then lbl_TombolMaret = Kosongan
        If BulanTerakhirDitutup <= 2 Then lbl_TombolApril = Kosongan
        If BulanTerakhirDitutup <= 3 Then lbl_TombolMei = Kosongan
        If BulanTerakhirDitutup <= 4 Then lbl_TombolJuni = Kosongan
        If BulanTerakhirDitutup <= 5 Then lbl_TombolJuli = Kosongan
        If BulanTerakhirDitutup <= 6 Then lbl_TombolAgustus = Kosongan
        If BulanTerakhirDitutup <= 7 Then lbl_TombolSeptember = Kosongan
        If BulanTerakhirDitutup <= 8 Then lbl_TombolOktober = Kosongan
        If BulanTerakhirDitutup <= 9 Then lbl_TombolNopember = Kosongan
        If BulanTerakhirDitutup <= 10 Then lbl_TombolDesember = Kosongan
    End Sub

    Sub AlgoritmaTombolUntukTahunBukuBerjalan()
        If TahunBukuAktif = TahunIni Then
            If BulanIni = 1 Then lbl_TombolJanuari = Kosongan
            If BulanIni <= 2 Then lbl_TombolFebruari = Kosongan
            If BulanIni <= 3 Then lbl_TombolMaret = Kosongan
            If BulanIni <= 4 Then lbl_TombolApril = Kosongan
            If BulanIni <= 5 Then lbl_TombolMei = Kosongan
            If BulanIni <= 6 Then lbl_TombolJuni = Kosongan
            If BulanIni <= 7 Then lbl_TombolJuli = Kosongan
            If BulanIni <= 8 Then lbl_TombolAgustus = Kosongan
            If BulanIni <= 9 Then lbl_TombolSeptember = Kosongan
            If BulanIni <= 10 Then lbl_TombolOktober = Kosongan
            If BulanIni <= 11 Then lbl_TombolNopember = Kosongan
            If BulanIni <= 12 Then lbl_TombolDesember = Kosongan
        End If
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

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

        Label_Terseleksi = rowviewUtama("Adjusment_HPP")

        KodeMataUang_Terseleksi = AmbilValue_KodeMataUang_BerdasarkanCOA(KodeAkun_Terseleksi)

        Dim PenyesuaianIndexBulan As Integer = 1 ' Ini harus disesuaikan dengan jumlah kolom pada tabel.

        BulanAngka_Terseleksi = KolomTerseleksi.DisplayIndex - PenyesuaianIndexBulan

    End Sub


    Private Sub btn_TombolTabel_Click(sender As Object, e As RoutedEventArgs)

        Dim btn = DirectCast(sender, Button)
        Dim rowView = TryCast(btn.DataContext, DataRowView)
        Dim BulanAngka As Integer = CInt(btn.Tag)
        If rowView Is Nothing Then Return
        Dim Adjusment As String = rowView("Adjusment_HPP")
        Dim COA As String = rowView("COA_")
        Dim lbl_Tombol As String = btn.Content

        If lbl_Tombol = teks_Posting Then
            AlgoritmaAdjusment(Adjusment, BulanAngka)
        Else
            LihatJurnalAdjusment(COA, BulanAngka)
        End If

    End Sub



    Sub AlgoritmaAdjusment(AdjusmentHPP As String, BulanAngka As Integer)
        jur_StatusPenyimpananJurnal_PerBaris = False
        Select Case AdjusmentHPP
            Case Adjusment_PemakaianBahanPenolong
                usc_BahanPenolong = New wpfUsc_StockOpname
                usc_BahanPenolong.JenisStok_Menu = JenisStok_BahanPenolong
                usc_BahanPenolong.JenisPengecekan_Menu = usc_BahanPenolong.JenisPengecekan_CekFisik
                usc_BahanPenolong.RefreshTampilanData()
                usc_BahanPenolong.DorongKeJurnal()
            Case Adjusment_PemakaianBahanBaku
                usc_BahanBaku = New wpfUsc_StockOpname
                usc_BahanBaku.JenisStok_Menu = JenisStok_BahanBaku
                usc_BahanBaku.JenisPengecekan_Menu = usc_BahanBaku.JenisPengecekan_CekFisik
                usc_BahanBaku.RefreshTampilanData()
                usc_BahanBaku.DorongKeJurnal()
            Case Adjusment_BiayaBahanBaku
                If ModusAplikasi = "CLASSIC" Then
                    frm_BOOKU.AdjusmentHPP_BiayaBahanBaku()
                Else
                    AdjusmentHPP_BiayaBahanBaku()
                End If
            Case Adjusment_BiayaTenagaKerjaLangsung
                If ModusAplikasi = "CLASSIC" Then
                    frm_BOOKU.AdjusmentHPP_BiayaTenagaKerjaLangsung()
                Else
                    AdjusmentHPP_BiayaTenagaKerjaLangsung()
                End If
            Case Adjusment_BiayaOverheadPabrik
                If ModusAplikasi = "CLASSIC" Then
                    frm_BOOKU.AdjusmentHPP_BiayaOverheadPabrik()
                Else
                    AdjusmentHPP_BiayaOverheadPabrik()
                End If
            Case Adjusment_BiayaProduksi
                If ModusAplikasi = "CLASSIC" Then
                    frm_BOOKU.AdjusmentHPP_BiayaProduksi()
                Else
                    AdjusmentHPP_BiayaProduksi()
                End If
            Case Adjusment_HargaPokokProduksi
                usc_BarangDalamProses_CekFisik = New wpfUsc_StockOpname
                usc_BarangDalamProses_CekFisik.JenisStok_Menu = JenisStok_BarangDalamProses
                usc_BarangDalamProses_CekFisik.JenisPengecekan_Menu = usc_BarangDalamProses_CekFisik.JenisPengecekan_CekFisik
                usc_BarangDalamProses_CekFisik.RefreshTampilanData()
                usc_BarangDalamProses_CekFisik.DorongKeJurnal()
            Case Adjusment_HargaPokokPenjualan
                usc_BarangJadi = New wpfUsc_StockOpname
                usc_BarangJadi.JenisStok_Menu = JenisStok_BarangJadi
                usc_BarangJadi.JenisPengecekan_Menu = usc_BarangJadi.JenisPengecekan_CekFisik
                usc_BarangJadi.RefreshTampilanData()
                usc_BarangJadi.DorongKeJurnal()
        End Select
        If jur_StatusPenyimpananJurnal_PerBaris Then TampilkanData()
    End Sub


    Sub LihatJurnalAdjusment(COA As String, BulanAngka As Integer)
        Dim TanggalAdjusment As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
        Dim NomorJV As Int64 = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                              " WHERE   Valid              <> '" & _X_ & "' " &
                              " AND     Jenis_Jurnal        = '" & JenisJurnal_AdjusmentHPP & "' " &
                              " AND     Tanggal_Transaksi   = '" & TanggalFormatSimpan(TanggalAdjusment) & "' " &
                              " AND     COA                 = '" & COA & " ' " &
                              " AND     D_K                 = '" & dk_D & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorJV = dr.Item("Nomor_JV")
        End If
        AksesDatabase_Transaksi(Tutup)
        LihatJurnal(NomorJV)
    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub


    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Adjusment_HPP As New DataGridTextColumn
    Dim COA_ As New DataGridTextColumn
    Dim Januari_ As New DataGridTextColumn
    Dim Februari_ As New DataGridTextColumn
    Dim Maret_ As New DataGridTextColumn
    Dim April_ As New DataGridTextColumn
    Dim Mei_ As New DataGridTextColumn
    Dim Juni_ As New DataGridTextColumn
    Dim Juli_ As New DataGridTextColumn
    Dim Agustus_ As New DataGridTextColumn
    Dim September_ As New DataGridTextColumn
    Dim Oktober_ As New DataGridTextColumn
    Dim Nopember_ As New DataGridTextColumn
    Dim Desember_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Adjusment_HPP")
        datatabelUtama.Columns.Add("COA_")
        datatabelUtama.Columns.Add("Januari_")
        datatabelUtama.Columns.Add("Februari_")
        datatabelUtama.Columns.Add("Maret_")
        datatabelUtama.Columns.Add("April_")
        datatabelUtama.Columns.Add("Mei_")
        datatabelUtama.Columns.Add("Juni_")
        datatabelUtama.Columns.Add("Juli_")
        datatabelUtama.Columns.Add("Agustus_")
        datatabelUtama.Columns.Add("September_")
        datatabelUtama.Columns.Add("Oktober_")
        datatabelUtama.Columns.Add("Nopember_")
        datatabelUtama.Columns.Add("Desember_")

        Dim LebarKolomBulan As Integer = 72

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Adjusment_HPP, "Adjusment_HPP", "Adjusment", 222, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_, "COA_", "COA", 63, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Januari_", "Januari", LebarKolomBulan, 1, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Februari_", "Februari", LebarKolomBulan, 2, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Maret_", "Maret", LebarKolomBulan, 3, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "April_", "April", LebarKolomBulan, 4, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Mei_", "Mei", LebarKolomBulan, 5, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Juni_", "Juni", LebarKolomBulan, 6, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Juli_", "Juli", LebarKolomBulan, 7, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Agustus_", "Agustus", LebarKolomBulan, 8, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "September_", "September", LebarKolomBulan, 9, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Oktober_", "Oktober", LebarKolomBulan, 10, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Nopember_", "Nopember", LebarKolomBulan, 11, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Desember_", "Desember", LebarKolomBulan, 12, AddressOf btn_TombolTabel_Click)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub



End Class


