Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm


Public Class wpfUsc_DaftarPemegangSaham

    Public StatusAktif
    Public JudulForm
    Public KesesuaianJurnal


    Dim NomorUrut
    'Dim NomorID
    Dim KodePemegangSaham
    Dim NamaPemegangSaham
    Dim NPWP
    Dim Alamat
    Dim JenisPS
    Dim LokasiPS
    Dim JumlahLembar
    Dim HargaPerLembar
    Dim JumlahSaham
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan

    Dim NomorUrut_Terseleksi
    Dim KodePemegangSaham_Terseleksi

    Dim NomorIDDetail_Terseleksi
    Dim NomorJV_Terseleksi

    Dim NomorIDDetail
    Dim TanggalTransaksi
    Dim JumlahLembarDetail
    Dim HargaPerLembarDetail
    Dim JumlahDebet
    Dim JumlahKredit
    Dim SaldoSaham
    Dim KeteranganDetail
    Dim NomorJV

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True


        StatusAktif = True

        lbl_JudulForm.Text = frm_DaftarPemegangSaham.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub TampilkanData()

        KesesuaianJurnal = True

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read
            KodePemegangSaham = dr.Item("Kode_Mitra")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Modal " &
                                         " WHERE Kode_Pemegang_Saham = '" & KodePemegangSaham & "' ", KoneksiDatabaseGeneral)
            drTELUSUR_ExecuteReader()
            JumlahLembar = 0
            JumlahSaham = 0
            Do While drTELUSUR.Read
                Dim JumlahLembarPerBaris = drTELUSUR.Item("Jumlah_Lembar")
                HargaPerLembar = drTELUSUR.Item("Harga_Per_Lembar")
                Dim JumlahSahamPerBaris = JumlahLembarPerBaris * HargaPerLembar
                JumlahLembar += JumlahLembarPerBaris
                JumlahSaham += JumlahSahamPerBaris
            Loop
            If JumlahSaham > 0 Then
                NamaPemegangSaham = dr.Item("Nama_Mitra")
                NPWP = dr.Item("NPWP")
                Alamat = PenghapusEnter(dr.Item("Alamat"))
                JenisPS = dr.Item("Jenis_WP")
                LokasiPS = dr.Item("Lokasi_WP")
                RekeningBank = dr.Item("Rekening_Bank")
                AtasNama = dr.Item("Atas_Nama")
                TambahBaris()
            End If
        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, KodePemegangSaham, NamaPemegangSaham, NPWP, Alamat, JenisPS, LokasiPS,
                                JumlahLembar, HargaPerLembar, JumlahSaham,
                                RekeningBank, AtasNama, Catatan)
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_LihatJurnal.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub


    Sub TampilkanData_Detail()

        pnl_SidebarKanan.Visibility = Visibility.Visible

        'Data Tabel :
        datatabelDetail.Rows.Clear()

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Modal " &
                              " WHERE Kode_Pemegang_Saham = '" & KodePemegangSaham_Terseleksi & "' ",
                              KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        JumlahLembarDetail = 0
        SaldoSaham = 0
        Do While dr.Read
            NomorIDDetail = dr.Item("Nomor_ID")
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            JumlahLembarDetail = dr.Item("Jumlah_Lembar")
            HargaPerLembarDetail = dr.Item("Harga_Per_Lembar")
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            SaldoSaham = SaldoSaham + JumlahKredit - JumlahDebet
            KeteranganDetail = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV = dr.Item("Nomor_JV")
            datatabelDetail.Rows.Add(NomorIDDetail, TanggalTransaksi, JumlahLembarDetail, HargaPerLembarDetail, JumlahDebet, JumlahKredit, SaldoSaham, KeteranganDetail, NomorJV)
        Loop

        BersihkanSeleksi_Detail()

    End Sub


    Sub BersihkanSeleksi_Detail()
        BarisDetail_Terseleksi = -1
        JumlahBarisDetail = datatabelDetail.Rows.Count
        datagridDetail.SelectedIndex = -1
        datagridDetail.SelectedItem = Nothing
        datagridDetail.SelectedCells.Clear()
        btn_LihatJurnal.IsEnabled = False
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_DaftarSaham_Click(sender As Object, e As RoutedEventArgs) Handles btn_DaftarSaham.Click
        win_DaftarSaham = New wpfWin_DaftarSaham
        win_DaftarSaham.ShowDialog()
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
        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        KodePemegangSaham_Terseleksi = rowviewUtama("Kode_Pemegang_Saham")

        If BarisTerseleksi >= 0 Then
            TampilkanData_Detail()
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        '(Belum ada Coding)
    End Sub


    Private Sub datagridDetail_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridDetail.SelectionChanged
    End Sub
    Private Sub datagridDetail_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridDetail.PreviewMouseLeftButtonUp
        HeaderKolomDetail = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomDetail IsNot Nothing Then
            datagridDetail.SelectedIndex = -1
            datagridDetail.SelectedItem = Nothing
            datagridDetail.SelectedCells.Clear()
            BarisDetail_Terseleksi = -1
            btn_LihatJurnal.IsEnabled = False
            NomorJV_Terseleksi = 0
        End If
    End Sub
    Private Sub datagridDetail_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridDetail.SelectedCellsChanged

        KolomTerseleksiDetail = datagridDetail.CurrentColumn
        BarisDetail_Terseleksi = datagridDetail.SelectedIndex
        If BarisDetail_Terseleksi < 0 Then Return
        rowviewDetail = TryCast(datagridDetail.SelectedItem, DataRowView)
        If Not rowviewDetail IsNot Nothing Then Return

        NomorIDDetail_Terseleksi = AmbilAngka(rowviewDetail("Nomor_ID_Detail").ToString)
        NomorJV_Terseleksi = rowviewDetail("Nomor_JV")

        If BarisDetail_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridDetail_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridDetail.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
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
    'Dim Nomor_ID As New DataGridTextColumn
    Dim Kode_Pemegang_Saham As New DataGridTextColumn
    Dim Nama_Pemegang_Saham As New DataGridTextColumn
    Dim NPWP_ As New DataGridTextColumn
    Dim Alamat_ As New DataGridTextColumn
    Dim Jenis_PS As New DataGridTextColumn
    Dim Lokasi_PS As New DataGridTextColumn
    Dim Jumlah_Lembar As New DataGridTextColumn
    Dim Harga_Per_Lembar As New DataGridTextColumn
    Dim Jumlah_Saham As New DataGridTextColumn
    Dim Rekening_Bank As New DataGridTextColumn
    Dim Atas_Nama As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        'datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Kode_Pemegang_Saham")
        datatabelUtama.Columns.Add("Nama_Pemegang_Saham")
        datatabelUtama.Columns.Add("NPWP_")
        datatabelUtama.Columns.Add("Alamat_")
        datatabelUtama.Columns.Add("Jenis_PS")
        datatabelUtama.Columns.Add("Lokasi_PS")
        datatabelUtama.Columns.Add("Jumlah_Lembar", GetType(Int64))
        datatabelUtama.Columns.Add("Harga_Per_Lembar", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Saham", GetType(Int64))
        datatabelUtama.Columns.Add("Rekening_Bank")
        datatabelUtama.Columns.Add("Atas_Nama")
        datatabelUtama.Columns.Add("Catatan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        'TambahkanKolomDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Pemegang_Saham, "Kode_Pemegang_Saham", "Kode Pemegang Saham", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Pemegang_Saham, "Nama_Pemegang_Saham", "Nama" & Enter1Baris & "Pemegang Saham", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NPWP_, "NPWP_", "NPWP", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_, "Alamat_", "Alamat", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PS, "Jenis_PS", "Jenis PS", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Lokasi_PS, "Lokasi_PS", "Lokasi PS", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Lembar, "Jumlah_Lembar", "Jumlah" & Enter1Baris & "Lembar", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Per_Lembar, "Harga_Per_Lembar", "Harga" & Enter1Baris & "Per Lembar", 72, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Saham, "Jumlah_Saham", "Jumlah Saham", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Rekening_Bank, "Rekening_Bank", "Rekening Bank", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Atas_Nama, "Atas_Nama", "Atas Nama", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 210, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    'Tabel Detail :
    Public datatabelDetail As DataTable
    Public dataviewDetail As DataView
    Public rowviewDetail As DataRowView
    Public newRowDetail As DataRow
    Public HeaderKolomDetail As DataGridColumnHeader
    Public KolomTerseleksiDetail As DataGridColumn
    Public BarisDetail_Terseleksi As Integer
    Public JumlahBarisDetail As Integer

    Dim Nomor_ID_Detail As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Jumlah_Lembar_Detail As New DataGridTextColumn
    Dim Harga_Per_Lembar_Detail As New DataGridTextColumn
    Dim Jumlah_Debet As New DataGridTextColumn
    Dim Jumlah_Kredit As New DataGridTextColumn
    Dim Saldo_Saham As New DataGridTextColumn
    Dim Keterangan_Detail As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelDetail()

        datatabelDetail = New DataTable
        datatabelDetail.Columns.Add("Nomor_ID_Detail")
        datatabelDetail.Columns.Add("Tanggal_Transaksi")
        datatabelDetail.Columns.Add("Jumlah_Lembar_Detail", GetType(Int64))
        datatabelDetail.Columns.Add("Harga_Per_Lembar_Detail", GetType(Int64))
        datatabelDetail.Columns.Add("Jumlah_Debet", GetType(Int64))
        datatabelDetail.Columns.Add("Jumlah_Kredit", GetType(Int64))
        datatabelDetail.Columns.Add("Saldo_Saham", GetType(Int64))
        datatabelDetail.Columns.Add("Keterangan_Detail")
        datatabelDetail.Columns.Add("Nomor_JV")

        StyleTabelPembantu_WPF(datagridDetail, datatabelDetail, dataviewDetail)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Nomor_ID_Detail, "Nomor_ID_Detail", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Jumlah_Lembar_Detail, "Jumlah_Lembar_Detail", "Jumlah" & Enter1Baris & "Lembar", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Harga_Per_Lembar_Detail, "Harga_Per_Lembar_Detail", "Harga" & Enter1Baris & "Per Lembar", 72, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Jumlah_Debet, "Jumlah_Debet", "Debet", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Jumlah_Kredit, "Jumlah_Kredit", "Kredit", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Saldo_Saham, "Saldo_Saham", "Saldo", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Keterangan_Detail, "Keterangan_Detail", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridDetail, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelDetail()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_AJP.IsReadOnly = True
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub

End Class
