Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Public Class wpfUsc_DesainNotaDebet

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_DepositOperasional " &
                                   " WHERE Angka_BPDO = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim KodeCustomer = dr.Item("Kode_Customer")
        Dim NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
        Dim AlamatCustomer = AmbilValue_AlamatMitra(KodeCustomer)
        Dim NomorNotaDebet = dr.Item("Nomor_Bukti")
        Dim TanggalNotaDebet = dr.Item("Tanggal_Bukti")

        Dim Keterangan = dr.Item("Keterangan")


        AksesDatabase_Transaksi(Tutup)

        TampilkanDataTabel()

        lbl_NomorBukti.Text = NomorNotaDebet
        lbl_TanggalBukti.Text = TanggalNotaDebet

        lbl_TanggalJatuhTempo.Text = Kosongan '(Ini diisi apa nantinya..?)
        lbl_TanggalDiterima.Text = Kosongan '(Ini diisi apa nantinya..?)

        lbl_NamaCustomer.Text = NamaCustomer
        lbl_AlamatCustomer.Text = AlamatCustomer

        lbl_Notes.Text = Keterangan

        lbl_Direktur.Text = "Direktur"
        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorBukti.Text = Kosongan
        lbl_TanggalBukti.Text = Kosongan
        lbl_TanggalJatuhTempo.Text = Kosongan
        lbl_TanggalDiterima.Text = Kosongan
        lbl_NamaCustomer.Text = Kosongan
        lbl_AlamatCustomer.Text = Kosongan

        lbl_Notes.Text = Kosongan

        txt_JumlahHargaKeseluruhan.Text = Kosongan

        'lbl_TotalTagihanTerbilang.Text = Kosongan

        lbl_User.Text = Kosongan
        lbl_NamaUser.Text = Kosongan
        lbl_Manager.Text = Kosongan
        lbl_NamaManager.Text = Kosongan
        lbl_Direktur.Text = Kosongan
        lbl_NamaDirektur.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim KodeAkun
        Dim NamaProduk
        Dim NomorReferensiProduk
        Dim TanggalReferensiProduk
        Dim JumlahHargaProduk
        Dim JumlahHargaKeseluruhan = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_DepositOperasional " &
                                   " WHERE Angka_BPDO = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            KodeAkun = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            NomorReferensiProduk = dr.Item("Nomor_Referensi_Produk")
            TanggalReferensiProduk = TanggalFormatTampilan(dr.Item("Tanggal_Referensi_Produk"))
            JumlahHargaProduk = dr.Item("Jumlah_Harga_Produk")
            JumlahHargaKeseluruhan += JumlahHargaProduk
            datatabelUtama.Rows.Add(NomorUrut, KodeAkun, NamaProduk, NomorReferensiProduk, TanggalReferensiProduk, JumlahHargaProduk)
        Loop

        If NomorUrut < MinimumBaris Then
            Do While NomorUrut < MinimumBaris
                NomorUrut += 1
                datatabelUtama.Rows.Add()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

        txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan

    End Sub

    Private Sub txt_JumlahHargaKeseluruhan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHargaKeseluruhan.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHargaKeseluruhan)
    End Sub


    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut_Produk As New DataGridTextColumn
    Dim COA_Produk As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Nomor_Referensi_Produk As New DataGridTextColumn
    Dim Tanggal_Referensi_Produk As New DataGridTextColumn
    Dim Jumlah_Harga_Produk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut_Produk")
        datatabelUtama.Columns.Add("COA_Produk")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Nomor_Referensi_Produk")
        datatabelUtama.Columns.Add("Tanggal_Referensi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Harga_Produk", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nomor_Urut_Produk, "Nomor_Urut_Produk", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, COA_Produk, "COA_Produk", "Kode Akun", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 321, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nomor_Referensi_Produk, "Nomor_Referensi_Produk", "Nomor Referensi", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Tanggal_Referensi_Produk, "Tanggal_Referensi_Produk", "Tanggal Referensi", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Jumlah_Harga_Produk, "Jumlah_Harga_Produk", "Jumlah", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

    End Sub

End Class
