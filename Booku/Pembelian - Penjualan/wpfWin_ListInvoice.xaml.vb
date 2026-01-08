Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListInvoice

    Dim JudulForm As String
    Dim QueryTampilan As String
    Dim FilterData As String
    Dim TabelDatabase As String
    Dim Kolom_KodeMitra As String

    'Konstanta Fungsi Form Invoice :
    Public FungsiForm_InvoicePembelian As String = "Invoice Pembelian"
    Public FungsiForm_InvoicePenjualan As String = "Invoice Penjualan"

    'Public Properties - Parameter Input :
    Public JalurMasuk As String
    Public FungsiForm As String
    Public JenisPPN As String
    Public PerlakuanPPN As String
    Public FilterMitra_Aktif As Boolean = True
    Public NamaMitra_Filter As String
    Public PilihTanggalInvoice As String
    Public PilihYangSudahDijurnal As Boolean = False

    'Public Properties - Return Values :
    Public NomorInvoice_Terseleksi As String
    Public TanggalInvoice_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public KodeProject_Terseleksi As String

    Dim CariInvoice As String
    Dim PilihKodeMitra As String
    Dim Mitra_Semua As String = "Semua"
    Dim TanggalInvoice_Semua As String = "Semua"
    Dim NomorInvoice_Sebelumnya As String


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If FungsiForm = FungsiForm_InvoicePembelian Then
            JudulForm = "Daftar Invoice Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase = "tbl_Pembelian_Invoice"
            Kolom_KodeMitra = "Kode_Supplier"
            Nomor_Invoice.Header = "Nomor Invoice"
            Tanggal_Invoice.Header = "Tanggal Invoice"
            Kode_Mitra.Header = "Kode Supplier"
            Nama_Mitra.Header = "Supplier"
            Alamat_Mitra.Header = "Alamat Supplier"
        End If

        If FungsiForm = FungsiForm_InvoicePenjualan Then
            JudulForm = "Daftar Invoice Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase = "tbl_Penjualan_Invoice"
            Kolom_KodeMitra = "Kode_Customer"
            Nomor_Invoice.Header = "Nomor Invoice"
            Tanggal_Invoice.Header = "Tanggal Invoice"
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
        txt_CariInvoice.Focus()

    End Sub


    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case FungsiForm
            Case FungsiForm_InvoicePembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
            Case FungsiForm_InvoicePenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
            Case Else
                PesanUntukProgrammer("Fungsi Form belum ditentukan")
                Return
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub


    Sub TampilkanData()

        If String.IsNullOrEmpty(FungsiForm) Or String.IsNullOrEmpty(TabelDatabase) Then Return

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

        'Filter Tanggal :
        Dim FilterTanggal = " "
        If Not String.IsNullOrEmpty(PilihTanggalInvoice) AndAlso PilihTanggalInvoice <> TanggalInvoice_Semua Then
            FilterTanggal = " AND Tanggal_Invoice = '" & TanggalFormatSimpan(PilihTanggalInvoice) & "' "
        End If

        'Filter Sudah Dijurnal :
        Dim FilterSudahDijurnal = " "
        If PilihYangSudahDijurnal = True Then FilterSudahDijurnal = " AND Nomor_JV > 0 "

        'Filter Pencarian :
        Dim FilterCariInvoice = " "
        If Not String.IsNullOrEmpty(CariInvoice) Then
            FilterCariInvoice = " AND ( Nomor_Invoice LIKE '%" & CariInvoice & "%' ) "
        End If

        'Filter Keseluruhan :
        FilterData = FilterJenisPPN & FilterPerlakuanPPN & FilterMitra & FilterTanggal & FilterSudahDijurnal & FilterCariInvoice

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase &
            " WHERE Nomor_ID <> 0 " & FilterData &
            " ORDER BY Angka_Invoice "

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        NomorInvoice_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            Dim NomorInvoice = dr.Item("Nomor_Invoice")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                Dim TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                Dim KodeMitra = dr.Item(Kolom_KodeMitra)
                Dim NamaMitra = Kosongan
                Dim AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                Dim KodeProject = "Belum ada Coding untuk Ini."
                datatabelUtama.Rows.Add(NomorInvoice, TanggalInvoice, KodeMitra, NamaMitra, AlamatMitra, KodeProject)
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()
        txt_CariInvoice.Focus()

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
        NomorInvoice_Terseleksi = Kosongan
        TanggalInvoice_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        KodeProject_Terseleksi = Kosongan
    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        TabelDatabase = Kosongan
        FilterMitra_Aktif = True
        NamaMitra_Filter = Kosongan
        txt_CariInvoice.Text = Kosongan
        PilihTanggalInvoice = TanggalInvoice_Semua
        PilihYangSudahDijurnal = False

        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        btn_Pilih.IsEnabled = False

        ProsesResetForm = False

    End Sub


    Private Sub txt_CariInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariInvoice.TextChanged
        CariInvoice = txt_CariInvoice.Text
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

        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice_")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice_")
        KodeMitra_Terseleksi = rowviewUtama("Kode_Mitra_")
        NamaMitra_Terseleksi = rowviewUtama("Nama_Mitra_")
        AlamatMitra_Terseleksi = rowviewUtama("Alamat_Mitra_")
        KodeProject_Terseleksi = rowviewUtama("Kode_Project_")

        txt_CariInvoice.Text = Kosongan
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

    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Kode_Mitra As New DataGridTextColumn
    Dim Nama_Mitra As New DataGridTextColumn
    Dim Alamat_Mitra As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_Invoice_")
        datatabelUtama.Columns.Add("Tanggal_Invoice_")
        datatabelUtama.Columns.Add("Kode_Mitra_")
        datatabelUtama.Columns.Add("Nama_Mitra_")
        datatabelUtama.Columns.Add("Alamat_Mitra_")
        datatabelUtama.Columns.Add("Kode_Project_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice_", "Nomor Invoice", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice_", "Tanggal Invoice", 90, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mitra, "Kode_Mitra_", "Kode Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Mitra, "Nama_Mitra_", "Mitra", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_Mitra, "Alamat_Mitra_", "Alamat Mitra", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project_", "Kode Project", 150, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
