Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListLawanTransaksi

    Dim JudulForm
    Dim QueryTampilan
    Dim FilterData

    Public PilihJenisLawanTransaksi
    Public PilihLembagaKeuangan
    Public PilihPemegangSaham
    Public PilihAfiliasi
    Public PilihLokasiWP

    Public JalurMasuk

    Public KodeMitraTerseleksi As String
    Public NamaMitraTerseleksi As String
    Public NPWPTerseleksi As String
    Public JenisWPTerseleksi As String
    Public AlamatMitraTerseleksi As String

    Dim JenisLawanTransaksi

    Public TampilkanDataPemegangSaham As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If PilihJenisLawanTransaksi = Mitra_Supplier Then
            JudulForm = "Daftar Supplier"
            lbl_CariMitra.Text = "Cari Supplier :"
            btn_TambahDataMitra.Content = "Tambah Supplier"
            Kode_Mitra.Header = "Kode Supplier"
            Nama_Mitra.Header = "Nama Supplier"
            cmb_JenisLawanTransaksi.SelectedValue = Mitra_Supplier
            cmb_JenisLawanTransaksi.IsEnabled = False
        End If
        If PilihJenisLawanTransaksi = Mitra_Customer Then
            JudulForm = "Daftar Customer"
            lbl_CariMitra.Text = "Cari Customer :"
            btn_TambahDataMitra.Content = "Tambah Customer"
            Kode_Mitra.Header = "Kode Customer"
            Nama_Mitra.Header = "Nama Customer"
            cmb_JenisLawanTransaksi.SelectedValue = Mitra_Customer
            cmb_JenisLawanTransaksi.IsEnabled = False
        End If
        If PilihJenisLawanTransaksi = Pilihan_Semua Then
            JudulForm = "Daftar Mitra"
            lbl_CariMitra.Text = "Cari Mitra :"
            btn_TambahDataMitra.Content = "Tambah Mitra"
            Kode_Mitra.Header = "Kode Mitra"
            Nama_Mitra.Header = "Nama Mitra"
            cmb_JenisLawanTransaksi.SelectedValue = Pilihan_Semua
            cmb_JenisLawanTransaksi.IsEnabled = True
        End If

        Title = JudulForm

        RefreshTampilanData()

        txt_CariMitra.Focus()

    End Sub

    Sub TampilkanData()

        'Filter Kategori :
        Dim FilterJenisLawanTransaksi = " "
        If JenisLawanTransaksi = Pilihan_Semua Then FilterJenisLawanTransaksi = " "
        If JenisLawanTransaksi = Mitra_Supplier Then FilterJenisLawanTransaksi = " AND Supplier = 1 "
        If JenisLawanTransaksi = Mitra_Customer Then FilterJenisLawanTransaksi = " AND Customer = 1 "

        'Filter Lembaga Keuangan :
        Dim FilterLembagaKeuangan = " "
        If PilihLembagaKeuangan = Pilihan_Semua Then FilterLembagaKeuangan = " "
        If PilihLembagaKeuangan = Pilihan_Ya Then FilterLembagaKeuangan = " AND Keuangan = 1 "
        If PilihLembagaKeuangan = Pilihan_Tidak Then FilterLembagaKeuangan = " AND Keuangan = 0 "

        'Filter PemegangSaham :
        Dim FilterPemegangSaham = " "
        If PilihPemegangSaham = Pilihan_Semua Then FilterPemegangSaham = " "
        If PilihPemegangSaham = Pilihan_Ya Then FilterPemegangSaham = " AND Pemegang_Saham = 1 "
        If PilihPemegangSaham = Pilihan_Tidak Then FilterPemegangSaham = " AND Pemegang_Saham = 0 "

        'Filter Afiliasi :
        Dim FilterAfiliasi = " "
        If PilihAfiliasi = Pilihan_Semua Then FilterAfiliasi = " "
        If PilihAfiliasi = Pilihan_Ya Then FilterAfiliasi = " AND Afiliasi = 1 "
        If PilihAfiliasi = Pilihan_Tidak Then FilterAfiliasi = " AND Afiliasi = 0 "

        'Filter Lokasi :
        Dim FilterLokasi = " "
        If PilihLokasiWP = Pilihan_Semua Then FilterLokasi = " "
        If PilihLokasiWP = LokasiWP_DalamNegeri Then FilterLokasi = " AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "'"
        If PilihLokasiWP = LokasiWP_LuarNegeri Then FilterLokasi = " AND Lokasi_WP = '" & LokasiWP_LuarNegeri & "'"

        'Filter Pencarian
        Dim FilterPencarian = " "
        If txt_CariMitra.Text <> "" Then
            Dim Srch = txt_CariMitra.Text
            Dim clm_KodeMitra = " Kode_Mitra LIKE '%" & Srch & "%' "
            Dim clm_NamaMitra = " OR Nama_Mitra LIKE '%" & Srch & "%' "
            FilterPencarian = " AND (" & clm_KodeMitra & clm_NamaMitra & ") "
        End If

        'Query Tampilan :
        FilterData = FilterJenisLawanTransaksi & FilterLembagaKeuangan & FilterPemegangSaham & FilterAfiliasi & FilterLokasi & FilterPencarian
        QueryTampilan = " SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra <> 'X' " & FilterData

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Kode_Mitra ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            Dim KodeMitra = dr.Item("Kode_Mitra")
            Dim NamaMitra = dr.Item("Nama_Mitra")
            Dim NPWP = dr.Item("NPWP")
            Dim JenisWP = dr.Item("Jenis_WP")
            Dim SebagaiSupplier = dr.Item("Supplier")
            Dim SebagaiCustomer = dr.Item("Customer")
            Dim AlamatMitra = dr.Item("Alamat")
            datatabelUtama.Rows.Add(KodeMitra, NamaMitra, NPWP, JenisWP, SebagaiSupplier, SebagaiCustomer, AlamatMitra)
        Loop
        AksesDatabase_General(Tutup)
        If PilihJenisLawanTransaksi = Pilihan_Semua _
            And PilihLembagaKeuangan = Pilihan_Semua _
            And PilihPemegangSaham = Pilihan_Semua _
            And PilihAfiliasi = Pilihan_Semua _
            Then
            datatabelUtama.Rows.Add(KodeLawanTransaksi_Karyawan, NamaLawanTransaksi_Karyawan)
            datatabelUtama.Rows.Add(KodeLawanTransaksi_Internal, NamaLawanTransaksi_Internal)
        End If
        BersihkanSeleksi()
        txt_CariMitra.Focus()

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
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        KontenComboKategori()
        PilihJenisLawanTransaksi = Pilihan_Semua
        PilihLembagaKeuangan = Pilihan_Semua
        PilihPemegangSaham = Pilihan_Semua
        PilihAfiliasi = Pilihan_Semua
        PilihLokasiWP = Pilihan_Semua
        TampilkanDataPemegangSaham = False

        lbl_CariMitra.Text = "Cari Mitra :"
        txt_CariMitra.Text = Kosongan
        btn_TambahDataMitra.Content = "Tambah Mitra"
        btn_Pilih.IsEnabled = False

        BarisTerseleksi = -1
        KodeMitraTerseleksi = Kosongan
        NamaMitraTerseleksi = Kosongan
        NPWPTerseleksi = Kosongan
        JenisWPTerseleksi = Kosongan
        AlamatMitraTerseleksi = Kosongan

        datatabelUtama.Rows.Clear()

        ProsesResetForm = False

    End Sub

    Sub KontenComboKategori()
        cmb_JenisLawanTransaksi.IsEnabled = True
        cmb_JenisLawanTransaksi.Items.Clear()
        cmb_JenisLawanTransaksi.Items.Add(Pilihan_Semua)
        cmb_JenisLawanTransaksi.Items.Add(Mitra_Supplier)
        cmb_JenisLawanTransaksi.Items.Add(Mitra_Customer)
        cmb_JenisLawanTransaksi.SelectedValue = Pilihan_Semua
    End Sub


    Private Sub txt_CariMitra_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariMitra.TextChanged
        btn_Pilih.IsEnabled = False
        TampilkanData()
    End Sub

    Private Sub cmb_JenisLawanTransaksi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisLawanTransaksi.SelectionChanged
        If cmb_JenisLawanTransaksi.SelectedItem IsNot Nothing Then
            JenisLawanTransaksi = cmb_JenisLawanTransaksi.SelectedItem.ToString()
            TampilkanData()
        End If
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


    Private Sub btn_TambahDataMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahDataMitra.Click
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputLawanTransaksi.ShowDialog()
        RefreshTampilanData()
    End Sub

    Private Sub btn_Pilih_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pilih.Click

        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If

        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If rowviewUtama Is Nothing Then Return

        KodeMitraTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Mitra")
        NamaMitraTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Mitra")
        NPWPTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "NPWP_")
        JenisWPTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_WP")
        AlamatMitraTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Alamat_Mitra")
        Dim SebagaiSupplier = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Supplier_"))
        Dim SebagaiCustomer = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Customer_"))

        If PilihJenisLawanTransaksi = Mitra_Supplier And SebagaiSupplier = 0 Then
            If TanyaKonfirmasi(NamaMitraTerseleksi & " belum tercatat sebagai SUPPLIER di database." & Enter2Baris & "Ingin mengedit data " & NamaMitraTerseleksi & "?") Then
                win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
                win_InputLawanTransaksi.ResetForm()
                win_InputLawanTransaksi.FungsiForm = FungsiForm_EDIT
                win_InputLawanTransaksi.txt_KodeLawanTransaksi.Text = KodeMitraTerseleksi
                win_InputLawanTransaksi.ShowDialog()
                RefreshTampilanData()
            End If
            Return
        End If

        If PilihJenisLawanTransaksi = Mitra_Customer And SebagaiCustomer = 0 Then
            If TanyaKonfirmasi(NamaMitraTerseleksi & " belum tercatat sebagai CUSTOMER di database." & Enter2Baris & "Ingin mengedit data " & NamaMitraTerseleksi & "?") Then
                win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
                win_InputLawanTransaksi.ResetForm()
                win_InputLawanTransaksi.FungsiForm = FungsiForm_EDIT
                win_InputLawanTransaksi.txt_KodeLawanTransaksi.Text = KodeMitraTerseleksi
                win_InputLawanTransaksi.ShowDialog()
                RefreshTampilanData()
            End If
            Return
        End If

        txt_CariMitra.Text = Kosongan
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

    Dim Kode_Mitra As New DataGridTextColumn
    Dim Nama_Mitra As New DataGridTextColumn
    Dim NPWP_ As New DataGridTextColumn
    Dim Jenis_WP As New DataGridTextColumn
    Dim Supplier_ As New DataGridTextColumn
    Dim Customer_ As New DataGridTextColumn
    Dim Alamat_Mitra As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Kode_Mitra")
        datatabelUtama.Columns.Add("Nama_Mitra")
        datatabelUtama.Columns.Add("NPWP_")
        datatabelUtama.Columns.Add("Jenis_WP")
        datatabelUtama.Columns.Add("Supplier_")
        datatabelUtama.Columns.Add("Customer_")
        datatabelUtama.Columns.Add("Alamat_Mitra")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mitra, "Kode_Mitra", "Kode Mitra", 80, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Mitra, "Nama_Mitra", "Nama Mitra", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NPWP_, "NPWP_", "NPWP", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_WP, "Jenis_WP", "Jenis WP", 70, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Supplier_, "Supplier_", "Supplier", 60, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Customer_, "Customer_", "Customer", 60, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_Mitra, "Alamat_Mitra", "Alamat", 200, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
