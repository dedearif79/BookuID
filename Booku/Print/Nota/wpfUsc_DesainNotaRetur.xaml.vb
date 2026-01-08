Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainNotaRetur

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                                   " WHERE Angka_Retur = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim KodeSupplier = dr.Item("Kode_Supplier")
        Dim NamaSupplier = AmbilValue_NamaMitra(KodeSupplier)
        Dim AlamatSupplier = AmbilValue_AlamatMitra(KodeSupplier)
        Dim NomorRetur = dr.Item("Nomor_Retur")
        Dim TanggalRetur = dr.Item("Tanggal_Retur")
        Dim NomorInvoice = dr.Item("Nomor_Invoice_Produk")
        Dim TanggalInvoice = dr.Item("Tanggal_Invoice_Produk")
        Dim TanggalJatuhTempo = Kosongan
        Dim TanggalSJBAST = Kosongan
        Dim NomorSJBAST = Kosongan
        Dim NomorSJBAST_Satuan = Kosongan
        Dim NomorSJBAST_Sebelumnya = Kosongan
        Dim NomorPO = Kosongan
        Dim TanggalPO = Kosongan
        Dim KodeProject = Kosongan
        Dim KodeProject_Satuan = Kosongan
        Dim KodeProject_Sebelumnya = Kosongan
        cmdTELUSUR = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                          " WHERE Nomor_Invoice = '" & NomorInvoice & "' ",
        KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
            If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                If NomorSJBAST = Kosongan Then
                    NomorSJBAST = NomorSJBAST_Satuan
                Else
                    NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                End If
                QueryTelusur2 = " SELECT * FROM tbl_Pembelian_SJ WHERE Nomor_SJ = '" & NomorSJBAST_Satuan & "' "
                cmdTELUSUR2 = New OdbcCommand(QueryTelusur2, KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                Do While drTELUSUR2.Read()
                    NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                    TanggalPO = drTELUSUR2.Item("Tanggal_PO_Produk")
                    cmdTELUSUR3 = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                       " WHERE Nomor_PO = '" & NomorPO & "' ",
                                                       KoneksiDatabaseTransaksi)
                    drTELUSUR3_ExecuteReader()
                    drTELUSUR3.Read()
                    KodeProject_Satuan = drTELUSUR3.Item("Kode_Project_Produk")
                    If KodeProject_Satuan <> KodeProject_Sebelumnya Then
                        If KodeProject = Kosongan Then
                            KodeProject = KodeProject_Satuan
                        Else
                            KodeProject &= SlashGanda_Pemisah & KodeProject_Satuan
                        End If
                    End If
                    KodeProject_Sebelumnya = KodeProject_Satuan
                Loop
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
        Loop
        Dim JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Dim Diskon_Rupiah As Int64 = dr.Item("Diskon")
        Dim DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        Dim PPN = dr.Item("PPN")
        Dim TotalRetur = dr.Item("Total_Retur")
        Dim TotalRetur_Terbilang = AngkaTerbilangRupiah(TotalRetur)
        Dim Catatan = dr.Item("Catatan")
        AksesDatabase_Transaksi(Tutup)

        lbl_NomorRetur.Text = NomorRetur
        lbl_NomorPO.Text = NomorPO
        lbl_NomorInvoice.Text = NomorInvoice

        lbl_TanggalRetur.Text = TanggalRetur
        lbl_TanggalPO.Text = TanggalPO
        lbl_TanggalInvoice.Text = TanggalInvoice


        lbl_NamaSupplier.Text = NamaSupplier
        lbl_AlamatSupplier.Text = AlamatSupplier

        TampilkanDataTabel()

        lbl_Notes.Text = Catatan

        Dim TarifPPN_Tampilan As Decimal = 0
        LogikaTampilanPPN_UntukCetakNota(TanggalInvoice, DasarPengenaanPajak, TarifPPN_Tampilan)

        txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
        txt_Diskon.Text = Diskon_Rupiah
        txt_DasarPengenaanPajak.Text = DasarPengenaanPajak
        txt_PPN.Text = PPN
        txt_TotalTagihan.Text = TotalRetur
        lbl_TotalTagihanTerbilang.Text = TotalRetur_Terbilang

        If Diskon_Rupiah > 0 Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon.Visibility = Visibility.Visible
        End If

        lbl_Direktur.Text = "Direktur"
        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        lbl_KodeProject.Text = KodeProject

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorRetur.Text = Kosongan
        lbl_NomorPO.Text = Kosongan
        lbl_NomorInvoice.Text = Kosongan

        lbl_TanggalRetur.Text = Kosongan
        lbl_TanggalPO.Text = kodeakun_Bank_Akhir
        lbl_TanggalInvoice.Text = Kosongan

        lbl_NamaSupplier.Text = Kosongan
        lbl_AlamatSupplier.Text = Kosongan

        lbl_Notes.Text = Kosongan

        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan

        lbl_Diskon.Visibility = Visibility.Collapsed
        txt_Diskon.Visibility = Visibility.Collapsed

        lbl_TotalTagihanTerbilang.Text = Kosongan

        lbl_User.Text = Kosongan
        lbl_NamaUser.Text = Kosongan
        lbl_Manager.Text = Kosongan
        lbl_NamaManager.Text = Kosongan
        lbl_Direktur.Text = Kosongan
        lbl_NamaDirektur.Text = Kosongan

        lbl_KodeProject.Text = Kosongan

    End Sub


    Sub TampilkanDataTabel()

        Dim MinimumBaris = 5
        Dim NomorUrut = 0
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan
        Dim TotalHarga

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                                   " WHERE Angka_Retur = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            TotalHarga = JumlahProduk * HargaSatuan
            datatabelUtama.Rows.Add(NomorUrut, NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan, TotalHarga)
        Loop

        If NomorUrut < MinimumBaris Then
            Do While NomorUrut < MinimumBaris
                NomorUrut += 1
                datatabelUtama.Rows.Add()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

    End Sub

    Private Sub txt_JumlahHargaKeseluruhan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHargaKeseluruhan.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHargaKeseluruhan)
    End Sub

    Private Sub txt_Diskon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Diskon)
    End Sub

    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_DasarPengenaanPajak)
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPN.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPN)
    End Sub

    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTagihan)
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
    Dim Harga_Satuan As New DataGridTextColumn
    Dim Total_Harga As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Deskripsi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk", GetType(Int64))
        datatabelUtama.Columns.Add("Satuan_Produk")
        datatabelUtama.Columns.Add("Harga_Satuan", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Harga", GetType(Int64))

        StyleTabelNota_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 231, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 174, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 51, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Harga_Satuan, "Harga_Satuan", "Harga Satuan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_Nota_WPF(datagridUtama, Total_Harga, "Total_Harga", "Total", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()

        InitializeComponent()

        Buat_DataTabelUtama()

    End Sub

End Class
