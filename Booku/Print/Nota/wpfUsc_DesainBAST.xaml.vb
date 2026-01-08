Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainBAST

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                   " WHERE Angka_BAST = '" & NomorPatokan_Cetak & "' ",
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
        Dim NomorSJBAST = dr.Item("Nomor_BAST")
        Dim TanggalSJBAST = dr.Item("Tanggal_BAST")
        Dim NomorPO = Kosongan
        Dim NomorPO_Satuan = Kosongan
        Dim NomorPO_Sebelumnya = Kosongan
        Dim TanggalPO = Kosongan
        Dim KodeProject = Kosongan
        Dim YangMenyerahkan = dr.Item("Yang_Menyerahkan")
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                     " WHERE Nomor_BAST = '" & NomorSJBAST & "' ",
                                     KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read()
            NomorPO_Satuan = drTELUSUR.Item("Nomor_PO_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                          " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            drTELUSUR2.Read()
            If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                If NomorPO = Kosongan Then
                    NomorPO = NomorPO_Satuan
                    TanggalPO = drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject = drTELUSUR2.Item("Kode_Project_Produk")
                Else
                    NomorPO &= "<br/>" & NomorPO_Satuan
                    TanggalPO &= "<br/>" & drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject &= "<br/>" & drTELUSUR2.Item("Kode_Project_Produk")
                End If
            End If
            NomorPO_Sebelumnya = NomorPO_Satuan
        Loop
        Dim Catatan = dr.Item("Catatan")
        AksesDatabase_Transaksi(Tutup)

        lbl_NomorBAST.Text = NomorSJBAST
        lbl_TanggalBAST.Text = TanggalSJBAST
        lbl_NomorPO.Text = NomorPO
        lbl_TanggalPO.Text = TanggalPO
        lbl_KodeProject.Text = KodeProject
        lbl_NamaCustomer.Text = NamaCustomer
        lbl_AlamatCustomer.Text = AlamatCustomer

        TampilkanDataTabel()

        lbl_Notes.Text = Catatan

        Dim Isian = "..............................................."

        lbl_TanggalDiterima_Label.Text = "Tanggal Diterima"
        lbl_TanggalDIterima_Isian.Text = Isian
        lbl_Penerima.Text = "Yang Menerima"
        lbl_NamaPenerima.Text = Isian
        lbl_Pengirim.Text = "Pengirim"
        lbl_NamaPengirim.Text = YangMenyerahkan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorBAST.Text = Kosongan
        lbl_TanggalBAST.Text = Kosongan
        lbl_NomorPO.Text = Kosongan
        lbl_TanggalPO.Text = Kosongan
        lbl_KodeProject.Text = Kosongan
        lbl_NamaCustomer.Text = Kosongan
        lbl_AlamatCustomer.Text = Kosongan

        lbl_Notes.Text = Kosongan

        lbl_TanggalDiterima_Label.Text = Kosongan
        lbl_TanggalDIterima_Isian.Text = Kosongan
        lbl_Penerima.Text = Kosongan
        lbl_NamaPenerima.Text = Kosongan
        lbl_Pengirim.Text = Kosongan
        lbl_NamaPengirim.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim Keterangan

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                   " WHERE Angka_BAST = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            Keterangan = dr.Item("Catatan")
            datatabelUtama.Rows.Add(NomorUrut, NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, Keterangan)
        Loop

        If NomorUrut < MinimumBaris Then
            Do While NomorUrut < MinimumBaris
                NomorUrut += 1
                datatabelUtama.Rows.Add()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

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
    Dim Nama_Produk As New DataGridTextColumn
    Dim Deskripsi_Produk As New DataGridTextColumn
    Dim Jumlah_Produk As New DataGridTextColumn
    Dim Satuan_Produk As New DataGridTextColumn
    Dim Keterangan_Produk As New DataGridTextColumn
    Dim Ceklis_Produk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Deskripsi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk", GetType(Int64))
        datatabelUtama.Columns.Add("Satuan_Produk")
        datatabelUtama.Columns.Add("Keterangan_Produk")
        datatabelUtama.Columns.Add("Ceklis_Produk")

        StyleTabelNota_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Jasa", 231, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 174, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 51, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Keterangan_Produk, "Keterangan_Produk", "Keterangan", 138, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Ceklis_Produk, "Ceklis_Produk", "Checklist", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
    End Sub

    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

    End Sub

End Class
