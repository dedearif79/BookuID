Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient

Public Class wpfUsc_DataPerangkatApp

    Public StatusAktif As Boolean = False

    Dim NomorUrut

    Dim NomorID_Terseleksi
    Dim IDKomputer_Terseleksi
    Dim NomorSeriProduk_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_DataPerangkatApp.JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub


    Public Sub RefreshTampilanData()
        EksekusiTampilanData = False
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Public EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If Not EksekusiTampilanData Then Return

        'Data Tabel :
        datatabelUtama.Clear()
        NomorUrut = 0
        Terabas()

        BukaDatabasePublic()
        If StatusKoneksiDatabase = False Then Return

        cmdPublic = New MySqlCommand(" SELECT * FROM tbl_perangkat WHERE Nomor_Seri_Produk <> 'X' ", KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        If Not StatusKoneksiDatabase Then Return

        Do While drPublic.Read

            NomorUrut += 1
            Dim NomorID = drPublic.Item("Nomor_ID")
            Dim IDKomputer = drPublic.Item("ID_Komputer")
            Dim NomorSeriProduk = drPublic.Item("Nomor_Seri_Produk")

            datatabelUtama.Rows.Add(NomorUrut, NomorID, IDKomputer, NomorSeriProduk)
            Terabas()

        Loop

        TutupDatabasePublic()

        BersihkanSeleksi()

    End Sub


    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        pnl_CRUD.IsEnabled = False
        txt_TotalTabel.Text = JumlahBaris
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        Dim Pesan As String = "Yakin akan menghapus Perangkat nomor " & NomorID_Terseleksi & " ?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand("DELETE FROM tbl_perangkat WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabasePublic)
        Try
            cmdPublic.ExecuteNonQuery()
            pesan_DataTerpilihBerhasilDihapus()
        Catch ex As Exception
            pesan_DataTerpilihGagalDihapus()
        End Try
        TutupDatabasePublic()
        TampilkanData()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
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

        NomorID_Terseleksi = rowviewUtama("Nomor_ID")
        IDKomputer_Terseleksi = rowviewUtama("ID_Komputer")
        NomorSeriProduk_Terseleksi = rowviewUtama("Nomor_Seri_Produk")

        If BarisTerseleksi >= 0 Then
            pnl_CRUD.IsEnabled = True
        Else
            pnl_CRUD.IsEnabled = False
        End If

    End Sub


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
    Dim ID_Komputer As New DataGridTextColumn
    Dim Nomor_Seri_Produk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("ID_Komputer")
        datatabelUtama.Columns.Add("Nomor_Seri_Produk")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 80, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, ID_Komputer, "ID_Komputer", "ID Komputer", 200, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Seri_Produk, "Nomor_Seri_Produk", "Nomor Seri Produk", 210, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        txt_TotalTabel.IsReadOnly = True
        txt_TotalTabel.HorizontalContentAlignment = HorizontalAlignment.Right
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub

End Class
