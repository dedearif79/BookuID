Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient

Public Class wpfUsc_DataProdukApp

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim NomorUrut

    Dim NomorSeriProduk_Terseleksi
    Dim IDCustomer_Terseleksi
    Dim JumlahPerangkat_Terseleksi
    Dim StatusTerpakai_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_DataProdukApp.JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
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

        cmdPublic = New MySqlCommand(" SELECT * FROM tbl_produk WHERE Nomor_Seri_Produk <> 'X' ", KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        If Not StatusKoneksiDatabase Then Return

        Do While drPublic.Read

            NomorUrut += 1
            Dim NomorSeriProduk = drPublic.Item("Nomor_Seri_Produk")
            Dim IDCustomer = drPublic.Item("ID_Customer")
            Dim JumlahPerangkat = drPublic.Item("Jumlah_Perangkat")
            Dim StatusTerpakai = drPublic.Item("Status_Terpakai")

            datatabelUtama.Rows.Add(NomorUrut, NomorSeriProduk, IDCustomer, JumlahPerangkat, StatusTerpakai)
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


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambah.Click
        win_InputProdukApp = New wpfWin_InputProdukApp
        win_InputProdukApp.ResetForm()
        win_InputProdukApp.FungsiForm = FungsiForm_TAMBAH
        win_InputProdukApp.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputProdukApp = New wpfWin_InputProdukApp
        win_InputProdukApp.ResetForm()
        win_InputProdukApp.FungsiForm = FungsiForm_EDIT
        win_InputProdukApp.txt_NomorSeriProduk.Text = NomorSeriProduk_Terseleksi
        win_InputProdukApp.txt_IDCustomer.Text = IDCustomer_Terseleksi
        win_InputProdukApp.txt_JumlahPerangkat.Text = JumlahPerangkat_Terseleksi
        win_InputProdukApp.txt_StatusTerpakai.Text = StatusTerpakai_Terseleksi
        win_InputProdukApp.ShowDialog()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If StatusTerpakai_Terseleksi = 1 Then
            PesanPeringatan("Produk ini sudah terpakai. Tidak dapat dihapus.")
            Return
        End If

        Dim Pesan As String = "Yakin akan menghapus Produk Nomor Seri " & NomorSeriProduk_Terseleksi & " ?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand("DELETE FROM tbl_produk WHERE Nomor_Seri_Produk = '" & NomorSeriProduk_Terseleksi & "' ", KoneksiDatabasePublic)
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

        NomorSeriProduk_Terseleksi = rowviewUtama("Nomor_Seri_Produk")
        IDCustomer_Terseleksi = rowviewUtama("ID_Customer")
        JumlahPerangkat_Terseleksi = rowviewUtama("Jumlah_Perangkat")
        StatusTerpakai_Terseleksi = rowviewUtama("Status_Terpakai")

        If BarisTerseleksi >= 0 Then
            pnl_CRUD.IsEnabled = True
        Else
            pnl_CRUD.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi >= 0 Then
            btn_Edit_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If e.Row.Item("Status_Terpakai").ToString() = "1" Then
            e.Row.Foreground = WarnaPeringatan_WPF
        Else
            e.Row.Foreground = WarnaTeksStandar_WPF
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
    Dim Nomor_Seri_Produk As New DataGridTextColumn
    Dim ID_Customer As New DataGridTextColumn
    Dim Jumlah_Perangkat As New DataGridTextColumn
    Dim Status_Terpakai As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_Seri_Produk")
        datatabelUtama.Columns.Add("ID_Customer")
        datatabelUtama.Columns.Add("Jumlah_Perangkat")
        datatabelUtama.Columns.Add("Status_Terpakai")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Seri_Produk, "Nomor_Seri_Produk", "Nomor Seri Produk", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, ID_Customer, "ID_Customer", "ID Customer", 100, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Perangkat, "Jumlah_Perangkat", "Jumlah Perangkat", 100, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Terpakai, "Status_Terpakai", "Status Terpakai", 100, FormatAngka, TengahTengah, KunciUrut, Terlihat)

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
    End Sub

End Class
