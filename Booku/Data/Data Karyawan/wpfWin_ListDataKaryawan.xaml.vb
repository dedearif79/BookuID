Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListDataKaryawan

    Dim JudulForm
    Dim QueryTampilan
    Dim FilterData

    Public JalurMasuk
    Public FungsiForm

    Public NomorIDKaryawan_Terseleksi As String
    Public NIK_Terseleksi As String
    Public NamaKaryawan_Terseleksi As String
    Public Jabatan_Terseleksi As String


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        JudulForm = "Daftar Karyawan"
        Title = JudulForm

        RefreshTampilanData()

        txt_CariKaryawan.Focus()

    End Sub

    Sub TampilkanData()

        'Filter Pencarian
        Dim FilterCariKaryawan = " "
        If txt_CariKaryawan.Text <> Kosongan Then
            Dim Srch = txt_CariKaryawan.Text
            Dim clm_NomorID = " Nomor_ID_Karyawan LIKE '%" & Srch & "%' "
            Dim clm_Nama = " OR Nama_Karyawan LIKE '%" & Srch & "%' "
            FilterCariKaryawan = " AND (" & clm_NomorID & clm_Nama & ") "
        End If

        'Filter Keseluruhan :
        FilterData = FilterCariKaryawan

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_DataKaryawan WHERE Status_Aktif = 1 " & FilterData

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Nama_Karyawan ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            Dim NomorIDKaryawan = dr.Item("Nomor_ID_Karyawan")
            Dim NIK = dr.Item("NIK")
            Dim NamaKaryawan = dr.Item("Nama_Karyawan")
            Dim Jabatan = dr.Item("Jabatan")
            datatabelUtama.Rows.Add(NomorIDKaryawan, NIK, NamaKaryawan, Jabatan)
        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()
        txt_CariKaryawan.Focus()

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
        txt_CariKaryawan.Text = Kosongan
        btn_Pilih.IsEnabled = False

        BarisTerseleksi = -1
        NomorIDKaryawan_Terseleksi = Kosongan
        NIK_Terseleksi = Kosongan
        NamaKaryawan_Terseleksi = Kosongan
        Jabatan_Terseleksi = Kosongan

        datatabelUtama.Rows.Clear()

        ProsesResetForm = False

    End Sub


    Private Sub txt_CariKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariKaryawan.TextChanged
        btn_Pilih.IsEnabled = False
        TampilkanData()
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

        NomorIDKaryawan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID_Karyawan")
        NIK_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "NIK_")
        NamaKaryawan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Karyawan")
        Jabatan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jabatan_")

        txt_CariKaryawan.Text = Kosongan
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

    Dim Nomor_ID_Karyawan As New DataGridTextColumn
    Dim NIK_ As New DataGridTextColumn
    Dim Nama_Karyawan As New DataGridTextColumn
    Dim Jabatan_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_ID_Karyawan")
        datatabelUtama.Columns.Add("NIK_")
        datatabelUtama.Columns.Add("Nama_Karyawan")
        datatabelUtama.Columns.Add("Jabatan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID_Karyawan, "Nomor_ID_Karyawan", "Nomor ID", 100, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NIK_, "NIK_", "NIK", 120, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Karyawan, "Nama_Karyawan", "Nama Karyawan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jabatan_, "Jabatan_", "Jabatan", 120, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

End Class
