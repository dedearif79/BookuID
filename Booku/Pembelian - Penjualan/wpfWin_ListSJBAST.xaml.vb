Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListSJBAST

    Dim JudulForm As String
    Dim QueryTampilan As String
    Dim FilterData As String
    Dim TabelDatabase_SJBAST As String
    Dim TabelDatabase_PO As String
    Dim Kolom_KodeMitra As String

    'Konstanta Sisi :
    Public SisiPembelian As String = "Sisi Pembelian"
    Public SisiPenjualan As String = "Sisi Penjualan"

    'Public Properties - Parameter Input :
    Public JalurMasuk As String
    Public Sisi As String
    Public JenisProduk_Induk As String
    Public JenisPPN As String
    Public PerlakuanPPN As String
    Public FilterMitra_Aktif As Boolean = True
    Public NamaMitra_Filter As String
    Public PilihTanggalDiterimaSJBAST As Date
    Public NomorPO_HarusSama As Boolean
    Public NomorPO_YangHarusDisamakan As String

    'Public Properties - Return Values :
    Public NomorSJBAST_Terseleksi As String
    Public TanggalSJBAST_Terseleksi As String
    Public JenisSurat_Terseleksi As String
    Public TanggalDiterima_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public NomorPO_Terseleksi As String
    Public OngkosKirim_Terseleksi As Int64

    'List Nomor SJBAST yang harus disingkirkan (diset oleh caller) :
    Public ListNomorSJBAST_Singkirkan As New List(Of String)

    Public CariSJBAST As String
    Public PilihKodeMitra As String
    Public Mitra_Semua As String = "Semua"
    Public TanggalSJBAST_Semua As Date = TanggalKosong

    'Variabel Tabel :
    Dim NomorSJBAST As String
    Dim NomorSJBAST_Sebelumnya As String
    Dim TanggalSJBAST As String
    Dim JenisSurat As String
    Dim TanggalDiterima As String
    Dim KodeMitra As String
    Dim NamaMitra As String
    Dim AlamatMitra As String
    Dim NomorPO As String
    Dim NomorPO_Satuan As String
    Dim NomorPO_Sebelumnya As String
    Dim BiayaTransportasi As Int64

    'Variabel Filter :
    Dim FilterJenisProdukInduk As String = " "
    Dim FilterJenisPPN As String
    Dim FilterPerlakuanPPN As String
    Dim FilterMitra As String
    Dim FilterTanggalSJBAST As String
    Dim FilterKetersediaan As String
    Dim FilterCariSJBAST As String = " "


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If Sisi = SisiPembelian Then
            JudulForm = "Daftar Surat Jalan / BAST - Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase_SJBAST = "tbl_Pembelian_SJ"
            Kolom_KodeMitra = "Kode_Supplier"
            Nomor_SJ_BAST.Header = "Nomor SJ/BAST"
            Tanggal_SJ_BAST.Header = "Tanggal SJ/BAST"
            Kode_Mitra.Header = "Kode Supplier"
            Nama_Mitra.Header = "Supplier"
            Alamat_Mitra.Header = "Alamat Supplier"
        End If

        If Sisi = SisiPenjualan Then
            JudulForm = "Daftar Surat Jalan / BAST - Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase_SJBAST = "tbl_Penjualan_SJ"
            Kolom_KodeMitra = "Kode_Customer"
            Nomor_SJ_BAST.Header = "Nomor SJ/BAST"
            Tanggal_SJ_BAST.Header = "Tanggal SJ/BAST"
            Kode_Mitra.Header = "Kode Customer"
            Nama_Mitra.Header = "Customer"
            Alamat_Mitra.Header = "Alamat Customer"
        End If

        Title = JudulForm

        'Set status filter mitra :
        lbl_FilterMitra.IsEnabled = FilterMitra_Aktif
        cmb_Mitra.IsEnabled = FilterMitra_Aktif

        KontenCombo_Mitra()

        'Set filter mitra jika sudah ditentukan :
        If Not String.IsNullOrEmpty(NamaMitra_Filter) Then
            cmb_Mitra.SelectedValue = NamaMitra_Filter
        Else
            cmb_Mitra.SelectedValue = Mitra_Semua
        End If

        RefreshTampilanData()

        'Visibilitas kolom Biaya Transportasi :
        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            Biaya_Transportasi.Visibility = Visibility.Visible
        Else
            Biaya_Transportasi.Visibility = Visibility.Hidden
        End If

        txt_CariSJBAST.Focus()

    End Sub


    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case Sisi
            Case SisiPembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
            Case SisiPenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub


    Sub TampilkanData()

        datatabelUtama.Rows.Clear()

        If String.IsNullOrEmpty(Sisi) Or String.IsNullOrEmpty(TabelDatabase_SJBAST) Then Return

        'Filter Jenis Produk :
        If JenisProduk_Induk = JenisProduk_Semua Or String.IsNullOrEmpty(JenisProduk_Induk) Then
            FilterJenisProdukInduk = " "
        Else
            FilterJenisProdukInduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
        End If

        'Filter Jenis PPN :
        If String.IsNullOrEmpty(JenisPPN) Then
            FilterJenisPPN = " "
        Else
            FilterJenisPPN = " AND Jenis_PPN = '" & JenisPPN & "' "
        End If

        'Filter Perlakuan PPN :
        If String.IsNullOrEmpty(PerlakuanPPN) Then
            FilterPerlakuanPPN = " "
        Else
            FilterPerlakuanPPN = " AND Perlakuan_PPN = '" & PerlakuanPPN & "' "
        End If

        'Filter Mitra :
        If PilihKodeMitra = Mitra_Semua Or String.IsNullOrEmpty(PilihKodeMitra) Then
            FilterMitra = " "
        Else
            FilterMitra = " AND " & Kolom_KodeMitra & " = '" & PilihKodeMitra & "' "
        End If

        'Filter Tanggal :
        If PilihTanggalDiterimaSJBAST = TanggalSJBAST_Semua Then
            FilterTanggalSJBAST = " "
        Else
            FilterTanggalSJBAST = " " & " AND Tanggal_Diterima = '" & TanggalFormatSimpan(PilihTanggalDiterimaSJBAST) & "' "
        End If

        'Filter Keseluruhan :
        FilterData = FilterMitra & FilterTanggalSJBAST & FilterJenisProdukInduk & FilterJenisPPN & FilterPerlakuanPPN

        Select Case JenisProduk_Induk
            Case Kosongan
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_Semua
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_Barang
                TampilkanData_SJ()
            Case JenisProduk_Jasa
                TampilkanData_BAST()
            Case JenisProduk_BarangDanJasa
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_JasaKonstruksi
                TampilkanData_BAST()
        End Select

        AksesDatabase_Transaksi(Tutup)

        NomorSJBAST_Sebelumnya = Kosongan

        'Singkirkan SJ/BAST yang sudah tersimpan di data Invoice :
        Dim NomorSJBAST_Singkirkan As String
        AksesDatabase_Transaksi(Buka)
        If Sisi = SisiPenjualan Then
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice ", KoneksiDatabaseTransaksi)
        ElseIf Sisi = SisiPembelian Then
            cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian ", KoneksiDatabaseTransaksi)
        End If
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST_Singkirkan = dr.Item("Nomor_SJ_BAST_Produk")
            If NomorSJBAST_Singkirkan <> NomorSJBAST_Sebelumnya Then
                For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
                    If datatabelUtama.Rows(i)("Nomor_SJ_BAST_").ToString() = NomorSJBAST_Singkirkan Then
                        datatabelUtama.Rows.RemoveAt(i)
                    End If
                Next
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Singkirkan
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Singkirkan SJ/BAST yang sudah ditambahkan (dari list yang diset oleh caller) :
        If ListNomorSJBAST_Singkirkan IsNot Nothing AndAlso ListNomorSJBAST_Singkirkan.Count > 0 Then
            For Each NomorSJBAST_Hapus In ListNomorSJBAST_Singkirkan
                For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
                    If datatabelUtama.Rows(i)("Nomor_SJ_BAST_").ToString() = NomorSJBAST_Hapus Then
                        datatabelUtama.Rows.RemoveAt(i)
                    End If
                Next
            Next
        End If

        'Singkirkan SJ/BAST yang Nomor PO-nya tidak sama, Jika List-nya harus sama dengan Nomor PO yang sama :
        If NomorPO_HarusSama = True Then
            For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
                If datatabelUtama.Rows(i)("Nomor_PO_").ToString() <> NomorPO_YangHarusDisamakan Then
                    datatabelUtama.Rows.RemoveAt(i)
                End If
            Next
        End If

        BersihkanSeleksi()
        txt_CariSJBAST.Focus()

    End Sub


    Sub TampilkanData_SJ()

        JenisSurat = "SJ"

        If Sisi = SisiPembelian Then
            TabelDatabase_SJBAST = "tbl_Pembelian_SJ"
            TabelDatabase_PO = "tbl_Pembelian_PO"
        End If
        If Sisi = SisiPenjualan Then
            TabelDatabase_SJBAST = "tbl_Penjualan_SJ"
            TabelDatabase_PO = "tbl_Penjualan_PO"
        End If

        'Filter Pencarian :
        FilterCariSJBAST = " "
        If Not String.IsNullOrEmpty(CariSJBAST) Then
            FilterCariSJBAST = " AND ( Nomor_SJ LIKE '%" & CariSJBAST & "%' ) "
        End If

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase_SJBAST &
            " WHERE Nama_Penerima <> '' " & FilterData & FilterCariSJBAST &
            " ORDER BY Angka_SJ "

        'Data Tabel :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        NomorSJBAST_Sebelumnya = Kosongan
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ")
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ"))
                TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
                KodeMitra = dr.Item(Kolom_KodeMitra)
                NamaMitra = Kosongan
                AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                             " WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_SJBAST &
                                              " WHERE Nomor_SJ = '" & NomorSJBAST & " ' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                NomorPO = Kosongan
                NomorPO_Sebelumnya = Kosongan
                BiayaTransportasi = 0
                Do While drTELUSUR2.Read
                    NomorPO_Satuan = drTELUSUR2.Item("Nomor_PO_Produk")
                    If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                        If NomorPO = Kosongan Then
                            NomorPO = NomorPO_Satuan
                        Else
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & NomorPO_Satuan
                        End If
                        AmbilValue_BiayaTransportasi()
                    End If
                    NomorPO_Sebelumnya = NomorPO_Satuan
                Loop
                TambahBaris()
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop

    End Sub


    Sub TampilkanData_BAST()

        JenisSurat = "BAST"

        If Sisi = SisiPembelian Then
            TabelDatabase_SJBAST = "tbl_Pembelian_BAST"
            TabelDatabase_PO = "tbl_Pembelian_PO"
        End If
        If Sisi = SisiPenjualan Then
            TabelDatabase_SJBAST = "tbl_Penjualan_BAST"
            TabelDatabase_PO = "tbl_Penjualan_PO"
        End If

        'Filter Pencarian :
        FilterCariSJBAST = " "
        If Not String.IsNullOrEmpty(CariSJBAST) Then
            FilterCariSJBAST = " AND ( Nomor_BAST LIKE '%" & CariSJBAST & "%' ) "
        End If

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase_SJBAST &
            " WHERE Yang_Menerima <> '' " & FilterData & FilterCariSJBAST &
            " ORDER BY Angka_BAST "

        'Data Tabel :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        NomorSJBAST_Sebelumnya = Kosongan
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_BAST")
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_BAST"))
                TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
                KodeMitra = dr.Item(Kolom_KodeMitra)
                NamaMitra = Kosongan
                AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                             " WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_SJBAST &
                                              " WHERE Nomor_BAST = '" & NomorSJBAST & " ' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                NomorPO = Kosongan
                NomorPO_Sebelumnya = Kosongan
                BiayaTransportasi = 0
                Do While drTELUSUR2.Read
                    NomorPO_Satuan = drTELUSUR2.Item("Nomor_PO_Produk")
                    If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                        If NomorPO = Kosongan Then
                            NomorPO = NomorPO_Satuan
                        Else
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & NomorPO_Satuan
                        End If
                    End If
                    NomorPO_Sebelumnya = NomorPO_Satuan
                Loop
                TambahBaris()
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop

    End Sub


    Sub TambahBaris()
        If Sisi = SisiPembelian And AdaSJBASTdiDataInvoicePembelian(NomorSJBAST) Then Return
        If Sisi = SisiPenjualan And AdaSJBASTdiDataInvoicePenjualan(NomorSJBAST) Then Return
        datatabelUtama.Rows.Add(NomorSJBAST, TanggalSJBAST, JenisSurat, TanggalDiterima, KodeMitra, NamaMitra, AlamatMitra, NomorPO, BiayaTransportasi)
    End Sub


    Sub AmbilValue_BiayaTransportasi()
        cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_PO &
                                      " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR3_ExecuteReader()
        drTELUSUR3.Read()
        If drTELUSUR3.HasRows Then
            BiayaTransportasi += drTELUSUR3.Item("Biaya_Transportasi")
        End If
    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Pilih.IsEnabled = False
        NomorSJBAST_Terseleksi = Kosongan
        TanggalSJBAST_Terseleksi = Kosongan
        JenisSurat_Terseleksi = Kosongan
        TanggalDiterima_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        NomorPO_Terseleksi = Kosongan
        OngkosKirim_Terseleksi = 0
    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        Sisi = Kosongan
        TabelDatabase_SJBAST = Kosongan
        FilterMitra_Aktif = True
        NamaMitra_Filter = Kosongan
        txt_CariSJBAST.Text = Kosongan
        PilihTanggalDiterimaSJBAST = TanggalSJBAST_Semua

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        NomorPO_HarusSama = False
        NomorPO_YangHarusDisamakan = Kosongan

        ListNomorSJBAST_Singkirkan = New List(Of String)

        btn_Pilih.IsEnabled = False

        ProsesResetForm = False

    End Sub


    Private Sub txt_CariSJBAST_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariSJBAST.TextChanged
        CariSJBAST = txt_CariSJBAST.Text
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub


    Private Sub cmb_Mitra_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Mitra.SelectionChanged
        If cmb_Mitra.SelectedValue Is Nothing Then Return
        Dim NamaMitraTerpilih = cmb_Mitra.SelectedValue.ToString()

        If NamaMitraTerpilih = Mitra_Semua Then
            PilihKodeMitra = Mitra_Semua
        Else
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & NamaMitraTerpilih & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            If StatusKoneksiDatabase = False Then Return
            dr.Read()
            If dr.HasRows Then PilihKodeMitra = dr.Item("Kode_Mitra")
            AksesDatabase_General(Tutup)
        End If

        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
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

        btn_Pilih.IsEnabled = True

    End Sub


    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        btn_Pilih_Click(sender, Nothing)
    End Sub


    Private Sub btn_Pilih_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pilih.Click

        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If

        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If rowviewUtama Is Nothing Then Return

        NomorSJBAST_Terseleksi = rowviewUtama("Nomor_SJ_BAST_")
        TanggalSJBAST_Terseleksi = rowviewUtama("Tanggal_SJ_BAST_")
        JenisSurat_Terseleksi = rowviewUtama("Jenis_Surat_")
        TanggalDiterima_Terseleksi = rowviewUtama("Tanggal_Diterima_")
        KodeMitra_Terseleksi = rowviewUtama("Kode_Mitra_")
        NamaMitra_Terseleksi = rowviewUtama("Nama_Mitra_")
        AlamatMitra_Terseleksi = rowviewUtama("Alamat_Mitra_")
        NomorPO_Terseleksi = rowviewUtama("Nomor_PO_")
        OngkosKirim_Terseleksi = AmbilAngka(rowviewUtama("Biaya_Transportasi_"))

        txt_CariSJBAST.Text = Kosongan
        Me.Close()

    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub


    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer

    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Jenis_Surat As New DataGridTextColumn
    Dim Tanggal_Diterima As New DataGridTextColumn
    Dim Kode_Mitra As New DataGridTextColumn
    Dim Nama_Mitra As New DataGridTextColumn
    Dim Alamat_Mitra As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Biaya_Transportasi As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_SJ_BAST_")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST_")
        datatabelUtama.Columns.Add("Jenis_Surat_")
        datatabelUtama.Columns.Add("Tanggal_Diterima_")
        datatabelUtama.Columns.Add("Kode_Mitra_")
        datatabelUtama.Columns.Add("Nama_Mitra_")
        datatabelUtama.Columns.Add("Alamat_Mitra_")
        datatabelUtama.Columns.Add("Nomor_PO_")
        datatabelUtama.Columns.Add("Biaya_Transportasi_", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST_", "Nomor SJ/BAST", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST_", "Tanggal SJ/BAST", 80, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Surat, "Jenis_Surat_", "Jenis Surat", 60, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Diterima, "Tanggal_Diterima_", "Tanggal Diterima", 80, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mitra, "Kode_Mitra_", "Kode Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Mitra, "Nama_Mitra_", "Mitra", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_Mitra, "Alamat_Mitra_", "Alamat Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO_", "Nomor PO", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Transportasi, "Biaya_Transportasi_", "Biaya Transportasi", 100, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
