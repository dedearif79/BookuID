Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainBuktiPengeluaran

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_KK = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
        Dim NamaLawanTransaksi = AmbilValue_NamaMitra(KodeLawanTransaksi)
        Dim AlamatLawanTransaksi = AmbilValue_AlamatMitra(KodeLawanTransaksi)
        Dim RekeningLawanTransaksi = AmbilValue_RekeningMitra(KodeLawanTransaksi)
        Dim AtasNamaRekeningLawanTransaksi = AmbilValue_AtasNamaRekeningMitra(KodeLawanTransaksi)
        Dim NomorKK = NomorPatokan_Cetak
        Dim TanggalKK = TanggalFormatTampilan(dr.Item("Tanggal_KK"))
        Dim COAKredit = dr.Item("COA_Kredit")
        Dim SaranaPembayaran = KonversiCOAKeSaranaPembayaran(COAKredit)
        Dim Catatan = dr.Item("Catatan")
        AksesDatabase_Transaksi(Tutup)

        If COATermasukBank(COAKredit) Then
            brd_Bank.Background = New SolidColorBrush(Colors.Black)
            lbl_RekeningPenerima.Text = RekeningLawanTransaksi
            lbl_AtasNamaPenerima.Text = AtasNamaRekeningLawanTransaksi
        Else
            Select Case COAKredit
                Case KodeTautanCOA_CashAdvance
                    brd_CashAdvance.Background = New SolidColorBrush(Colors.Black)
                Case KodeTautanCOA_Kas
                    brd_Kas.Background = New SolidColorBrush(Colors.Black)
                Case KodeTautanCOA_PettyCashAdministrasi
                    brd_PettyCash.Background = New SolidColorBrush(Colors.Black)
            End Select
        End If

        lbl_NomorKK.Text = NomorKK
        lbl_TanggalKK.Text = TanggalKK
        lbl_SaranaPembayaran.Text = SaranaPembayaran
        lbl_NamaLawanTransaksi.Text = NamaLawanTransaksi
        lbl_AlamatLawanTransaksi.Text = AlamatLawanTransaksi

        TampilkanDataTabel()

        lbl_Notes.Text = Catatan

        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        brd_Bank.Background = New SolidColorBrush(Colors.Transparent)
        brd_CashAdvance.Background = New SolidColorBrush(Colors.Transparent)
        brd_Kas.Background = New SolidColorBrush(Colors.Transparent)
        brd_PettyCash.Background = New SolidColorBrush(Colors.Transparent)

        lbl_NomorKK.Text = Kosongan
        lbl_TanggalKK.Text = Kosongan
        lbl_SaranaPembayaran.Text = Kosongan
        lbl_NamaLawanTransaksi.Text = Kosongan
        lbl_AlamatLawanTransaksi.Text = Kosongan
        lbl_RekeningPenerima.Text = Kosongan
        lbl_AtasNamaPenerima.Text = Kosongan

        lbl_Notes.Text = Kosongan
        txt_TotalPengajuan.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim NomorInvoice
        Dim TanggalInvoice
        Dim UraianInvoice As String
        Dim JumlahDiajukan As Int64
        Dim JumlahDisetujui As Int64

        Dim TotalPengajuan As Int64 = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_KK = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            UraianInvoice = PenghapusEnter(dr.Item("Uraian_Invoice"))
            JumlahDiajukan = dr.Item("Jumlah_Pengajuan")
            TotalPengajuan += JumlahDiajukan
            JumlahDisetujui = 0
            datatabelUtama.Rows.Add(NomorUrut, NomorInvoice, TanggalInvoice, UraianInvoice, JumlahDiajukan, JumlahDisetujui)
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
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Uraian_Invoice As New DataGridTextColumn
    Dim Jumlah_Diajukan As New DataGridTextColumn
    Dim Jumlah_Disetujui As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Uraian_Invoice")
        datatabelUtama.Columns.Add("Jumlah_Diajukan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Disetujui")

        StyleTabelNota_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 165, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Invoice, "Uraian_Invoice", "Uraian", 237, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Diajukan, "Jumlah_Diajukan", "Jumlah Diajukan", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Disetujui, "Jumlah_Disetujui", "Jumlah Disetujui", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

    End Sub

End Class
