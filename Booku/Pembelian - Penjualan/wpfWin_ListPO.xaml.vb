Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListPO

    Dim JudulForm As String
    Dim QueryTampilan As String
    Dim FilterData As String
    Dim TabelDatabase As String
    Dim Kolom_KodeMitra As String

    'Konstanta Sisi PO :
    Public Sisi_POPembelian As String = "PO Pembelian"
    Public Sisi_POPenjualan As String = "PO Penjualan"

    'Public Properties - Parameter Input :
    Public JalurMasuk As String
    Public Sisi As String
    Public JenisProduk_Induk As String
    Public JenisPPN As String
    Public PerlakuanPPN As String
    Public MetodePembayaran As String
    Public FilterMitra_Aktif As Boolean = True
    Public NamaMitra_Filter As String

    'Public Properties - Return Values :
    Public NomorPO_Terseleksi As String
    Public TanggalPO_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public KodeProject_Terseleksi As String
    Public JenisProdukInduk_Terseleksi As String

    'List Nomor PO yang harus disingkirkan (diset oleh caller) :
    Public ListNomorPO_Singkirkan As New List(Of String)

    Dim CariPO As String
    Dim PilihKodeMitra As String
    Dim Mitra_Semua As String = "Semua"
    Dim NomorPO_Sebelumnya As String


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If Sisi = Sisi_POPembelian Then
            JudulForm = "Daftar PO Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase = "tbl_Pembelian_PO"
            Kolom_KodeMitra = "Kode_Supplier"
            Nomor_PO.Header = "Nomor PO"
            Tanggal_PO.Header = "Tanggal PO"
            Kode_Mitra.Header = "Kode Supplier"
            Nama_Mitra.Header = "Supplier"
            Alamat_Mitra.Header = "Alamat Supplier"
        End If

        If Sisi = Sisi_POPenjualan Then
            JudulForm = "Daftar PO Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase = "tbl_Penjualan_PO"
            Kolom_KodeMitra = "Kode_Customer"
            Nomor_PO.Header = "Nomor PO"
            Tanggal_PO.Header = "Tanggal PO"
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
        txt_CariPO.Focus()

    End Sub


    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case Sisi
            Case Sisi_POPembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "' ", KoneksiDatabaseGeneral)
            Case Sisi_POPenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "' ", KoneksiDatabaseGeneral)
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub


    Sub TampilkanData()

        If String.IsNullOrEmpty(Sisi) Or String.IsNullOrEmpty(TabelDatabase) Then Return

        'Filter Jenis Produk :
        Dim FilterJenisProduk = " "
        If JalurMasuk = Form_INPUTSURATJALANPENJUALAN Or JalurMasuk = Form_INPUTSURATJALANPEMBELIAN Then
            If String.IsNullOrEmpty(JenisProduk_Induk) Then
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_BarangDanJasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_Barang & "' ) "
            Else
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
            End If
        End If
        If JalurMasuk = Form_INPUTBASTPENJUALAN Or JalurMasuk = Form_INPUTBASTPEMBELIAN Then
            If String.IsNullOrEmpty(JenisProduk_Induk) Then
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_BarangDanJasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_Jasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_JasaKonstruksi & "' ) "
            Else
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
            End If
        End If

        'Filter Jenis PPN :
        Dim FilterJenisPPN As String
        If String.IsNullOrEmpty(JenisPPN) Then
            FilterJenisPPN = " "
        Else
            FilterJenisPPN = " AND Jenis_PPN = '" & JenisPPN & "' "
        End If

        'Filter Perlakuan PPN :
        Dim FilterPerlakuanPPN As String
        If String.IsNullOrEmpty(PerlakuanPPN) Then
            FilterPerlakuanPPN = " "
        Else
            FilterPerlakuanPPN = " AND Perlakuan_PPN = '" & PerlakuanPPN & "' "
        End If

        'Filter Mitra :
        Dim FilterMitra = " "
        If PilihKodeMitra = Mitra_Semua Or String.IsNullOrEmpty(PilihKodeMitra) Then
            FilterMitra = " "
        Else
            FilterMitra = " AND " & Kolom_KodeMitra & " = '" & PilihKodeMitra & "' "
        End If

        'Filter Pencarian :
        Dim FilterCariPO = " "
        If Not String.IsNullOrEmpty(CariPO) Then
            FilterCariPO = " AND ( Nomor_PO LIKE '%" & CariPO & "%' ) "
        End If

        'Filter Metode Pembayaran :
        Dim FilterMetodePembayaran As String
        If MetodePembayaran = Pilihan_Semua Or String.IsNullOrEmpty(MetodePembayaran) Then
            FilterMetodePembayaran = " "
        Else
            FilterMetodePembayaran = " AND Metode_Pembayaran = '" & MetodePembayaran & "' "
        End If

        'Filter Keseluruhan :
        FilterData = FilterMitra & FilterCariPO & FilterJenisProduk & FilterJenisPPN & FilterPerlakuanPPN & FilterMetodePembayaran

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase &
            " WHERE Kontrol <> '" & Status_Closed & "' " & FilterData &
            " ORDER BY Angka_PO "

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        NomorPO_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            Dim NomorPO = dr.Item("Nomor_PO")
            If NomorPO <> NomorPO_Sebelumnya Then
                Dim TanggalPO = TanggalFormatTampilan(dr.Item("Tanggal_PO"))
                Dim KodeMitra = Kosongan
                KodeMitra = dr.Item(Kolom_KodeMitra)
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim NamaMitra = drTELUSUR.Item("Nama_Mitra")
                Dim AlamatMitra = drTELUSUR.Item("Alamat")
                AksesDatabase_General(Tutup)
                Dim KodeProject = dr.Item("Kode_Project_Produk")
                Dim JenisProdukInduk_List = dr.Item("Jenis_Produk_Induk")
                datatabelUtama.Rows.Add(NomorPO, TanggalPO, KodeMitra, NamaMitra, AlamatMitra, KodeProject, JenisProdukInduk_List)
            End If
            NomorPO_Sebelumnya = NomorPO
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Singkirkan PO yang sudah Dipakai (dari list yang diset oleh caller) :
        If ListNomorPO_Singkirkan IsNot Nothing AndAlso ListNomorPO_Singkirkan.Count > 0 Then
            For Each NomorPO_Singkirkan In ListNomorPO_Singkirkan
                For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
                    If datatabelUtama.Rows(i)("Nomor_PO_").ToString() = NomorPO_Singkirkan Then
                        datatabelUtama.Rows.RemoveAt(i)
                    End If
                Next
            Next
        End If

        BersihkanSeleksi()
        txt_CariPO.Focus()

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
        NomorPO_Terseleksi = Kosongan
        TanggalPO_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        KodeProject_Terseleksi = Kosongan
        JenisProdukInduk_Terseleksi = Kosongan
    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        Sisi = Kosongan
        TabelDatabase = Kosongan
        FilterMitra_Aktif = True
        NamaMitra_Filter = Kosongan
        txt_CariPO.Text = Kosongan

        MetodePembayaran = Pilihan_Semua

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        ListNomorPO_Singkirkan = New List(Of String)

        btn_Pilih.IsEnabled = False

        ProsesResetForm = False

    End Sub


    Private Sub txt_CariPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariPO.TextChanged
        CariPO = txt_CariPO.Text
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

        NomorPO_Terseleksi = rowviewUtama("Nomor_PO_")
        TanggalPO_Terseleksi = rowviewUtama("Tanggal_PO_")
        KodeMitra_Terseleksi = rowviewUtama("Kode_Mitra_")
        NamaMitra_Terseleksi = rowviewUtama("Nama_Mitra_")
        AlamatMitra_Terseleksi = rowviewUtama("Alamat_Mitra_")
        KodeProject_Terseleksi = rowviewUtama("Kode_Project_")
        JenisProdukInduk_Terseleksi = rowviewUtama("Jenis_Produk_Induk_")

        txt_CariPO.Text = Kosongan
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

    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Mitra As New DataGridTextColumn
    Dim Nama_Mitra As New DataGridTextColumn
    Dim Alamat_Mitra As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Jenis_Produk_Induk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_PO_")
        datatabelUtama.Columns.Add("Tanggal_PO_")
        datatabelUtama.Columns.Add("Kode_Mitra_")
        datatabelUtama.Columns.Add("Nama_Mitra_")
        datatabelUtama.Columns.Add("Alamat_Mitra_")
        datatabelUtama.Columns.Add("Kode_Project_")
        datatabelUtama.Columns.Add("Jenis_Produk_Induk_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO_", "Nomor PO", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO_", "Tanggal PO", 80, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mitra, "Kode_Mitra_", "Kode Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Mitra, "Nama_Mitra_", "Mitra", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_Mitra, "Alamat_Mitra_", "Alamat Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project_", "Kode Project", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk_Induk, "Jenis_Produk_Induk_", "Jenis Produk Induk", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
