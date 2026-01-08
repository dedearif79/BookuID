Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainBundelanPengajuanPengeluaran

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_bundelpengajuanpengeluaran " &
                                   " WHERE Nomor_Bundel = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim NomorBundel = NomorPatokan_Cetak
        Dim TanggalBundel = TanggalFormatTampilan(dr.Item("Tanggal_Bundel"))
        Dim Catatan = Kosongan ' dr.Item("Catatan")
        AksesDatabase_Transaksi(Tutup)

        lbl_NomorBundel.Text = NomorBundel
        lbl_TanggalBundel.Text = TanggalBundel

        TampilkanDataTabel()

        'lbl_Notes.Text = Catatan

        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorBundel.Text = Kosongan
        lbl_TanggalBundel.Text = Kosongan

        'lbl_Notes.Text = Kosongan
        txt_TotalPengajuan.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim NomorPengajuan
        Dim TanggalPengajuan = Kosongan
        Dim KodeLawanTransaksi
        Dim NamaLawanTransaksi
        Dim JumlahLembar_PerBaris As Int64
        Dim JumlahPengajuan_PerBaris As Int64
        Dim JumlahDisetujui_PerBaris As Int64

        Dim TotalPengajuan As Int64 = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_bundelpengajuanpengeluaran " &
                                   " WHERE Nomor_Bundel = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NomorPengajuan = dr.Item("Nomor_KK_Per_Baris")
            cmdTELUSUR = New OdbcCommand(" SELECT Tanggal_KK FROM tbl_BuktiPengeluaran WHERE Nomor_KK = '" & NomorPengajuan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows Then TanggalPengajuan = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_KK"))
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi_Per_Baris")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi_Per_Baris")
            JumlahLembar_PerBaris = dr.Item("Jumlah_Invoice_Per_Baris")
            JumlahPengajuan_PerBaris = dr.Item("Jumlah_Pengajuan_Per_Baris")
            TotalPengajuan += JumlahPengajuan_PerBaris
            JumlahDisetujui_PerBaris = 0
            datatabelUtama.Rows.Add(NomorUrut, NomorPengajuan, TanggalPengajuan, KodeLawanTransaksi, NamaLawanTransaksi,
                                    JumlahLembar_PerBaris, JumlahPengajuan_PerBaris, JumlahDisetujui_PerBaris)
        Loop

        If NomorUrut < MinimumBaris Then
            Do While NomorUrut < MinimumBaris
                NomorUrut += 1
                datatabelUtama.Rows.Add()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

        txt_TotalPengajuan.Text = TotalPengajuan

    End Sub


    Private Sub txt_TotalPengajuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalPengajuan.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalPengajuan)
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
    Dim Nomor_Pengajuan As New DataGridTextColumn
    Dim Tanggal_Pengajuan As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Jumlah_Lembar As New DataGridTextColumn
    Dim Jumlah_Diajukan As New DataGridTextColumn
    Dim Jumlah_Disetujui As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_Pengajuan")
        datatabelUtama.Columns.Add("Tanggal_Pengajuan")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Lembar", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Diajukan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Disetujui")

        StyleTabelNota_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Pengajuan, "Nomor_Pengajuan", "Nomor Pengajuan", 156, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pengajuan, "Tanggal_Pengajuan", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Nama Lawan Transaksi", 183, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Lembar, "Jumlah_Lembar", "Jumlah Lembar", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Diajukan, "Jumlah_Diajukan", "Jumlah Diajukan", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Disetujui, "Jumlah_Disetujui", "Jumlah Disetujui", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

    End Sub

End Class

