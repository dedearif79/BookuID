Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanHutangPPhPasal22_Impor

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm
    Public JenisPajak

    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama
    Public TahunBukuSumberData

    Public NamaHalaman As String
    Public AwalanBP As String
    Public COAPajak As String

    Dim Baris_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NamaJasa_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim NPWP_Terseleksi
    Dim DPP_Terseleksi
    Dim PPh_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar100_Terseleksi
    Dim SisaHutang100_Terseleksi
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

    Dim NomorUrut
    Dim NomorID
    Dim TanggalPIB
    Dim NomorPIB
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim PPhPasal22Impor
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
    Dim PPhPasal22Impor_Terseleksi
    Dim NomorJV_Terseleksi



    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim JenisPembelian
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim NPWP
    Dim KodeSupplier
    Dim NamaSupplier
    Dim PPh As Int64
    Dim RekapPerBulanPPhPasal22Impor As Int64
    Dim RekapPerBulanPPh As Int64
    Dim JumlahTagihan As Int64
    Dim TanggalTransaksi
    Dim JumlahBayar100 As Int64
    Dim SisaHutang As Int64
    Dim JenisKodeSetoran
    Dim Keterangan

    Dim TotalTagihan100 As Int64
    Dim TotalTagihan As Int64
    Dim TotalBayar100 As Int64
    Dim TotalSisaHutang As Int64

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim NomorInvoice_Sebelumnya

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        JudulForm = frm_BukuPengawasanHutangPPhPasal22_Impor.JudulForm
        lbl_JudulForm.Text = frm_BukuPengawasanHutangPPhPasal22_Impor.JudulForm

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

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub TampilkanData()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Judul Halaman :
        frm_BukuPengawasanHutangPPhPasal22_Impor.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        KesesuaianJurnal = True

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
        TotalTagihan = 0

        TotalBayar100 = 0
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
            If dr.Item("Jenis_PPh") = JenisPPh_Pasal22_Impor Then
                PPhPasal22Impor = dr.Item("PPh_Terutang")
            Else
                PPhPasal22Impor = 0
            End If
            NomorJV_Bayar = dr.Item("Nomor_JV_Bayar_Pajak_Impor")
            TambahBaris()

            NomorInvoice_Sebelumnya = NomorInvoice

        Loop

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        If NomorJV_Bayar = 0 Then Return
        If NomorInvoice = NomorInvoice_Sebelumnya Then Return
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorID, TanggalPIB, NomorPIB, TanggalInvoice, NomorInvoice, PPhPasal22Impor, NomorJV_Bayar)
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


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
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

        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        PPhPasal22Impor_Terseleksi = AmbilAngka(rowviewUtama("PPh_Pasal_22_Impor"))
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))

        If NomorJV_Pembayaran_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
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
    Dim Tanggal_PIB As New DataGridTextColumn
    Dim Nomor_PIB As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim PPh_Pasal_22_Impor As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Tanggal_PIB")
        datatabelUtama.Columns.Add("Nomor_PIB")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("PPh_Pasal_22_Impor", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_JV")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PIB, "Tanggal_PIB", "Tanggal PIB", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PIB, "Nomor_PIB", "Nomor PIB", 159, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 183, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Pasal_22_Impor, "PPh_Pasal_22_Impor", "PPh Pasal 22 Impor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 72, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

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
    'Kode Setoran : 101

    Public KesesuaianSaldoAwal_101 As Boolean
    Public KesesuaianSaldoAkhir_101 As Boolean
    Public KesesuaianJurnal As Boolean

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
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAPajak, SaldoAwal_BerdasarkanCOA_101, JumlahPenyesuaianSaldo_101, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101,
                                                                  txt_SaldoAwalBerdasarkanCOA_101, txt_AJP_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_101()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAPajak, SaldoAkhir_BerdasarkanCOA_101, txt_saldoBerdasarkanCOA_PlusPenyesuaian_101)
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
        SesuaikanSaldoAwal(NamaHalaman, COAPajak, SaldoAwal_BerdasarkanList_101, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_101)
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



    Private Sub txt_TotalTabel_Total_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_Total.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_Total)
    End Sub


End Class
