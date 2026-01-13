Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListDataProject

    ' === VARIABEL FORM ===
    Dim JudulForm As String
    Dim QueryTampilan As String
    Dim FilterData As String

    ' === PUBLIC PROPERTIES (KONFIGURASI) ===
    Public JalurMasuk As String
    Public FungsiForm As String

    ' === PUBLIC PROPERTIES (RETURN VALUE) ===
    Public KodeProject_Terseleksi As String
    Public NomorPO_Terseleksi As String
    Public KodeCustomer_Terseleksi As String
    Public NamaCustomer_Terseleksi As String
    Public NilaiProject_Terseleksi As Decimal

    ' === VARIABEL FILTER ===
    Dim Cari_KodeProject As String
    Dim PilihKodeMitra As String
    Dim Mitra_Semua As String = "Semua"

    ' === DATATABLE & DATAGRID ===
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer

    ' === KOLOM DATAGRID ===
    Dim Kode_Project As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Nilai_Project As New DataGridTextColumn


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        JudulForm = "Daftar Project"
        Title = JudulForm

        KontenCombo_Customer()
        RefreshTampilanData()

        txt_CariKodeProject.Focus()

    End Sub


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Nilai_Project", GetType(Decimal))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 132, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 132, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Customer", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nilai_Project, "Nilai_Project", "Nilai Project", 100, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub KontenCombo_Customer()
        cmb_Customer.Items.Clear()
        cmb_Customer.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            cmb_Customer.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
        cmb_Customer.SelectedIndex = 0
    End Sub


    Sub TampilkanData()

        ' Filter Customer
        Dim FilterMitra = " "
        If PilihKodeMitra = Mitra_Semua OrElse PilihKodeMitra = Kosongan Then
            FilterMitra = " "
        Else
            FilterMitra = " AND Kode_Customer = '" & PilihKodeMitra & "' "
        End If

        ' Filter Pencarian
        Dim FilterCariKodeProject = " "
        If Cari_KodeProject <> Kosongan Then
            FilterCariKodeProject = " AND ( Kode_Project LIKE '%" & Cari_KodeProject & "%' ) "
        End If

        ' Filter Keseluruhan
        FilterData = FilterMitra & FilterCariKodeProject

        ' Query Tampilan
        QueryTampilan = " SELECT * FROM tbl_DataProject WHERE Status = '" & Status_Open & "' " & FilterData

        ' Data Tabel
        datatabelUtama.Rows.Clear()
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            Dim KodeProject = dr.Item("Kode_Project")
            Dim NomorPO = dr.Item("Nomor_PO")
            Dim KodeCustomer = dr.Item("Kode_Customer")
            Dim NamaCustomer = dr.Item("Nama_Customer")
            Dim NilaiProject = dr.Item("Nilai_Project")
            datatabelUtama.Rows.Add(KodeProject, NomorPO, KodeCustomer, NamaCustomer, NilaiProject)
        Loop

        ' Tambah baris "Campuran"
        datatabelUtama.Rows.Add("Campuran", Kosongan, Kosongan, Kosongan, 0)

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()
        txt_CariKodeProject.Focus()

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

        FungsiForm = Kosongan
        txt_CariKodeProject.Text = Kosongan
        cmb_Customer.SelectedIndex = 0
        btn_Pilih.IsEnabled = False

        lbl_FilterCustomer.IsEnabled = True
        cmb_Customer.IsEnabled = True

        BarisTerseleksi = -1
        KodeProject_Terseleksi = Kosongan
        NomorPO_Terseleksi = Kosongan
        KodeCustomer_Terseleksi = Kosongan
        NamaCustomer_Terseleksi = Kosongan
        NilaiProject_Terseleksi = 0

        datatabelUtama.Rows.Clear()

        ProsesResetForm = False

    End Sub


    ' ==================== EVENT HANDLER ====================

    Private Sub txt_CariKodeProject_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariKodeProject.TextChanged
        Cari_KodeProject = txt_CariKodeProject.Text
        btn_Pilih.IsEnabled = False
        TampilkanData()
    End Sub


    Private Sub cmb_Customer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Customer.SelectionChanged
        If cmb_Customer.SelectedItem Is Nothing Then Return

        Dim NamaMitraTerpilih = cmb_Customer.SelectedItem.ToString()

        If NamaMitraTerpilih = Mitra_Semua Then
            PilihKodeMitra = Mitra_Semua
        Else
            ' Cari kode mitra berdasarkan nama
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & NamaMitraTerpilih & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            If StatusKoneksiDatabase = False Then Return
            If dr.Read() Then
                PilihKodeMitra = dr.Item("Kode_Mitra").ToString()
            End If
            AksesDatabase_General(Tutup)
        End If

        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
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
        If rowviewUtama Is Nothing Then Return

        btn_Pilih.IsEnabled = True

    End Sub


    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        btn_Pilih_Click(sender, Nothing)
    End Sub


    Private Sub btn_Pilih_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pilih.Click

        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.")
            Return
        End If

        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If rowviewUtama Is Nothing Then Return

        KodeProject_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project")
        NomorPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_PO")
        KodeCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Customer")
        NamaCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Customer")
        NilaiProject_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nilai_Project"))

        txt_CariKodeProject.Text = Kosongan
        Me.Close()

    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


End Class
