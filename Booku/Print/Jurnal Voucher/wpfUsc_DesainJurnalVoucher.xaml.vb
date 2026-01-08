Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainJurnalVoucher

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                   " WHERE Nomor_JV = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim NomorJV = NomorPatokan_Cetak
        Dim TanggalJV = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
        Dim JenisJurnal = dr.Item("Jenis_Jurnal")
        Dim Referensi = dr.Item("Referensi")
        Dim TanggalInvoice = TampilanBundelan(dr.Item("Tanggal_Invoice"))
        Dim NomorInvoice = TampilanBundelan(dr.Item("Nomor_Invoice"))
        Dim NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
        Dim KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
        Dim NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
        Dim UsernameEntry = dr.Item("Username_Entry")
        Dim NamaUserEntry = dr.Item("Nama_User_Entry")
        Dim NamaProduk = dr.Item("Nama_Produk")
        Dim Catatan = PenghapusEnter(dr.Item("Uraian_Transaksi"))

        Dim NamaManagerKeuangan = "Nama Manager Keuangan"  '(Ini hanya sementara. Nanti harus diambil dari database Struktur Organisasi Perusahaan)

        AksesDatabase_Transaksi(Tutup)

        If Referensi = Kosongan Then Referensi = StripKosong
        If TanggalInvoice = Kosongan Then TanggalInvoice = StripKosong
        If NomorInvoice = Kosongan Then NomorInvoice = StripKosong
        If NomorFakturPajak = Kosongan Then NomorFakturPajak = StripKosong
        If NamaLawanTransaksi = Kosongan Then NamaLawanTransaksi = StripKosong

        lbl_NomorJV.Text = NomorJV
        lbl_TanggalJV.Text = TanggalJV
        lbl_JenisJurnal.Text = JenisJurnal
        lbl_Referensi.Text = Referensi
        lbl_TanggalInvoice.Text = TanggalInvoice
        lbl_NomorInvoice.Text = NomorInvoice
        lbl_NomorFakturPajak.Text = NomorFakturPajak
        lbl_NamaLawanTransaksi.Text = NamaLawanTransaksi

        TampilkanDataTabel()

        If NamaProduk <> Kosongan Then
            pnl_NamaProduk.Visibility = Visibility.Visible
            lbl_NamaProduk.Text = NamaProduk
        Else
            pnl_NamaProduk.Visibility = Visibility.Collapsed
        End If

        If Catatan <> Kosongan Then
            pnl_UraianTransaksi.Visibility = Visibility.Visible
            lbl_UraianTransaksi.Text = Catatan
        Else
            pnl_UraianTransaksi.Visibility = Visibility.Collapsed
        End If

        lbl_DataEntry.Text = "Data Entry"
        lbl_NamaUserEntry.Text = NamaUserEntry
        lbl_ManagerKeuangan.Text = "Manager Keuangan"
        lbl_NamaManagerKeuangan.Text = NamaManagerKeuangan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorJV.Text = Kosongan
        lbl_TanggalJV.Text = Kosongan
        lbl_JenisJurnal.Text = Kosongan
        lbl_Referensi.Text = Kosongan
        lbl_TanggalInvoice.Text = Kosongan
        lbl_NomorInvoice.Text = Kosongan
        lbl_NomorFakturPajak.Text = Kosongan
        lbl_NamaLawanTransaksi.Text = Kosongan

        lbl_NamaProduk.Text = Kosongan
        lbl_UraianTransaksi.Text = Kosongan

        lbl_DataEntry.Text = Kosongan
        lbl_NamaUserEntry.Text = Kosongan
        lbl_Manager.Text = Kosongan
        lbl_NamaManager.Text = Kosongan
        lbl_ManagerKeuangan.Text = Kosongan
        lbl_NamaManagerKeuangan.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim KodeAkun
        Dim NamaAkun
        Dim KodeMataUang As String
        Dim Kurs As Decimal
        Dim DK
        Dim Debet As Int64
        Dim Kredit As Int64
        Dim TotalDebet As Int64 = 0
        Dim TotalKredit As Int64 = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                   " WHERE Nomor_JV = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            KodeAkun = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            Kurs = dr.Item("Kurs")
            Debet = AmbilValue_NilaiMataUang_WithCOA(KodeAkun, KodeMataUang, Kurs, dr.Item("Jumlah_Debet"))
            Kredit = AmbilValue_NilaiMataUang_WithCOA(KodeAkun, KodeMataUang, Kurs, dr.Item("Jumlah_Kredit"))
            DK = dr.Item("D_K")
            If Kredit > 0 Then NamaAkun = PenjorokNamaAkun & NamaAkun
            datatabelUtama.Rows.Add(NomorUrut, KodeAkun, NamaAkun, DK, Debet, Kredit)
            TotalDebet += Debet
            TotalKredit += Kredit
        Loop

        If NomorUrut < MinimumBaris Then
            Do While NomorUrut < MinimumBaris
                NomorUrut += 1
                datatabelUtama.Rows.Add()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

        txt_TotalDebet.Text = TotalDebet
        txt_TotalKredit.Text = TotalKredit

    End Sub


    Private Sub txt_TotalDebet_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalDebet.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalDebet)
    End Sub


    Private Sub txt_TotalKredit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalKredit.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalKredit)
    End Sub



    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

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
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Debet_ As New DataGridTextColumn
    Dim Kredit_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Debet_", GetType(Int64))
        datatabelUtama.Columns.Add("Kredit_", GetType(Int64))

        StyleTabelNota_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 360, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_, "Debet_", "Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_, "Kredit_", "Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
    End Sub

End Class
