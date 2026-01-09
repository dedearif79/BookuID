Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm


Public Class wpfUsc_BukuPenjualanEceran

    Public StatusAktif
    Public JudulForm
    Public KesesuaianJurnal



    Dim NomorUrut
    Dim NomorID
    Dim TanggalTransaksi
    Dim JumlahKas
    Dim JumlahBank
    Dim JumlahTransaksi
    Dim JumlahDPP
    Dim JumlahPPN
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim JumlahKas_Terseleksi
    Dim JumlahBank_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim JumlahDPP_Terseleksi
    Dim JumlahPPN_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        StatusAktif = True

        lbl_JudulForm.Text = frm_BukuPenjualanEceran.JudulForm

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

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PenjualanEceran ORDER BY Tanggal_Transaksi ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read
            NomorID = dr.Item("Nomor_ID")
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            JumlahKas = dr.Item("Jumlah_Kas")
            JumlahBank = dr.Item("Jumlah_Bank")
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            JumlahDPP = dr.Item("DPP")
            JumlahPPN = dr.Item("PPN")
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            TambahBaris()
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub


    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorID, TanggalTransaksi, JumlahKas, JumlahBank, JumlahTransaksi, JumlahDPP, JumlahPPN, Keterangan, NomorJV, User)
    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputPenjualanEceran = New wpfWin_InputPenjualanEceran
        win_InputPenjualanEceran.ResetForm()
        win_InputPenjualanEceran.FungsiForm = FungsiForm_TAMBAH
        win_InputPenjualanEceran.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputPenjualanEceran = New wpfWin_InputPenjualanEceran
        win_InputPenjualanEceran.ResetForm()
        win_InputPenjualanEceran.FungsiForm = FungsiForm_EDIT
        win_InputPenjualanEceran.NomorID = NomorID_Terseleksi
        win_InputPenjualanEceran.NomorJV = NomorJV_Terseleksi
        win_InputPenjualanEceran.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi_Terseleksi)
        win_InputPenjualanEceran.txt_JumlahKas.Text = JumlahKas_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank.Text = JumlahBank_Terseleksi
        IsiValueElemenRichTextBox(win_InputPenjualanEceran.txt_Keterangan, Keterangan_Terseleksi)
        win_InputPenjualanEceran.ShowDialog()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        'Hapus Data Penjualan Eceran :
        HapusDataTabel_BerdasarkanNomorID_dbTransaksi("tbl_PenjualanEceran", NomorID_Terseleksi)

        'Hapus Data Jurnal :
        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

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
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID"))
        TanggalTransaksi_Terseleksi = rowviewUtama("Tanggal_Transaksi")
        JumlahKas_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Kas"))
        JumlahBank_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank"))
        JumlahTransaksi_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Transaksi"))
        JumlahDPP_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_DPP"))
        JumlahPPN_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_PPN"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))
        User_Terseleksi = rowviewUtama("User_")

        If BarisTerseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        '(Belum ada Coding)
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
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Jumlah_Kas As New DataGridTextColumn
    Dim Jumlah_Bank As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Jumlah_DPP As New DataGridTextColumn
    Dim Jumlah_PPN As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Kas", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_DPP", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_PPN", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kas, "Jumlah_Kas", "Kas", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank, "Jumlah_Bank", "Bank", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_DPP, "Jumlah_DPP", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_PPN, "Jumlah_PPN", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Catatan", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
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
