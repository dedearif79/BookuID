Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainInvoice

    Dim InvoiceIDR As Boolean
    Dim InvoiceNonIDR As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorPatokan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)

        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Data tidak ditemukan..!!!")
            Return
        End If
        Dim MetodePembayaran = dr.Item("Metode_Pembayaran")
        Dim TahapTermin = dr.Item("Tahap_Termin")
        Dim JumlahTermin = 0
        Dim TerminPersen As Decimal = FormatUlangDesimal_Prosentase(dr.Item("Termin"))
        Dim KodeCustomer = dr.Item("Kode_Customer")
        Dim NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
        Dim AlamatCustomer = AmbilValue_AlamatMitra(KodeCustomer)
        Dim NomorInvoice = dr.Item("Nomor_Invoice")
        Dim TanggalInvoice = dr.Item("Tanggal_Invoice")
        Dim TanggalJatuhTempo = dr.Item("Tanggal_Jatuh_Tempo")
        Dim TanggalSJBAST = dr.Item("Tanggal_Diterima_SJ_BAST_Produk")
        Dim NomorSJBAST = Kosongan
        Dim NomorSJBAST_Satuan = Kosongan
        Dim NomorSJBAST_Sebelumnya = Kosongan
        Dim NomorPO = Kosongan
        Dim KodeProject = Kosongan
        Dim KodeProject_Satuan = Kosongan
        Dim KodeProject_Sebelumnya = Kosongan

        If MetodePembayaran = MetodePembayaran_Normal Then
            cmdTELUSUR = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                              " WHERE Nomor_Invoice = '" & NomorPatokan_Cetak & "' ",
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
                    If Microsoft.VisualBasic.Left(NomorSJBAST_Satuan, 2) = "SJ" Then
                        QueryTelusur2 = " SELECT * FROM tbl_Penjualan_SJ   WHERE Nomor_SJ   = '" & NomorSJBAST_Satuan & "' "
                    Else
                        QueryTelusur2 = " SELECT * FROM tbl_Penjualan_BAST WHERE Nomor_BAST = '" & NomorSJBAST_Satuan & "' "
                    End If
                    cmdTELUSUR2 = New OdbcCommand(QueryTelusur2, KoneksiDatabaseTransaksi)
                    drTELUSUR2_ExecuteReader()
                    Do While drTELUSUR2.Read()
                        NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                        cmdTELUSUR3 = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
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
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            NomorPO = dr.Item("Nomor_PO_Produk")
            JumlahTermin = AmbilValue_JumlahTerminBerdasarkanPOPenjualan(NomorPO)
        End If

        Dim KodeMataUang As String = dr.Item("Kode_Mata_Uang")
        If KodeMataUang = KodeMataUang_IDR Then
            InvoiceIDR = True
            InvoiceNonIDR = False
        Else
            InvoiceNonIDR = True
            InvoiceIDR = False
        End If
        Dim JumlahHargaKeseluruhan As Int64
        Dim Diskon_Rupiah As Int64
        Dim DasarPengenaanPajak As Int64
        Dim JenisPPN As String
        Dim TarifPPN As Decimal
        Dim PPN As Int64
        Dim TotalTagihanKotor As Int64
        Dim JenisPPh As String
        Dim TarifPPh As String
        Dim PPhTerutang As Int64
        Dim PPhDitanggung As Int64
        Dim PPhDipotong As Int64
        Dim OngkosKirim As Int64
        Dim TotalTagihan As Int64
        Dim TotalTagihan_Terbilang As String
        Dim Catatan As String
        If InvoiceNonIDR Then
            Convert.ToDecimal(JumlahHargaKeseluruhan)
            Convert.ToDecimal(TotalTagihanKotor)
            Convert.ToDecimal(TotalTagihan)
        End If
        JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Diskon_Rupiah = dr.Item("Diskon")
        DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        JenisPPN = dr.Item("Jenis_PPN")
        TarifPPN = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPN"))
        PPN = dr.Item("PPN")
        TotalTagihanKotor = dr.Item("Total_Tagihan_Kotor")
        JenisPPh = dr.Item("Jenis_PPh")
        TarifPPh = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh")) & " %"
        PPhTerutang = dr.Item("PPh_Terutang")
        PPhDitanggung = dr.Item("PPh_Ditanggung")
        PPhDipotong = dr.Item("PPh_Dipotong")
        OngkosKirim = dr.Item("Biaya_Transportasi")
        TotalTagihan = dr.Item("Total_Tagihan")
        TotalTagihan_Terbilang = AngkaTerbilangRupiah(TotalTagihan)
        Catatan = dr.Item("Catatan")
        AksesDatabase_Transaksi(Tutup)

        lbl_NomorInvoice.Text = NomorInvoice
        lbl_TanggalInvoice.Text = TanggalInvoice
        lbl_KodeProject.Text = KodeProject
        lbl_NomorSJBAST.Text = NomorSJBAST
        lbl_TanggalSJBAST.Text = TanggalSJBAST
        lbl_TanggalJatuhTempo.Text = TanggalJatuhTempo
        lbl_NamaCustomer.Text = NamaCustomer
        lbl_AlamatCustomer.Text = AlamatCustomer

        TampilkanDataTabel()

        lbl_Notes.Text = Catatan

        Dim TarifPPN_Tampilan As Decimal = 0
        LogikaTampilanPPN_UntukCetakNota(TanggalInvoice, DasarPengenaanPajak, TarifPPN_Tampilan)

        txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
        txt_Diskon.Text = Diskon_Rupiah
        txt_DasarPengenaanPajak.Text = DasarPengenaanPajak
        txt_PPN.Text = PPN
        txt_PPhTerutang.Text = PPhTerutang
        txt_PPhDitanggung.Text = PPhDitanggung
        txt_PPhDipotong.Text = PPhDipotong
        txt_OngkosKirim.Text = OngkosKirim
        txt_TotalTagihan.Text = TotalTagihan
        lbl_TotalTagihanTerbilang.Text = TotalTagihan_Terbilang

        If Diskon_Rupiah > 0 Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon.Visibility = Visibility.Visible
        End If

        If MetodePembayaran = MetodePembayaran_Normal Then
            lbl_Termin.Visibility = Visibility.Collapsed
            txt_Termin.Visibility = Visibility.Collapsed
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            lbl_Termin.Text = TahapTermin & " (" & TerminPersen & " %)"
            If JenisPPN = JenisPPN_Exclude Then txt_Termin.Text = DasarPengenaanPajak
            If JenisPPN = JenisPPN_Include Then txt_Termin.Text = TotalTagihanKotor
            lbl_Termin.Visibility = Visibility.Visible
            txt_Termin.Visibility = Visibility.Visible
        End If

        If TahapTermin = TahapTermin_Pelunasan Then
            lbl_UangMukaPlusTermin.Visibility = Visibility.Visible
            txt_UangMukaPlusTermin.Visibility = Visibility.Visible
            If JenisPPN = JenisPPN_Exclude Then txt_UangMukaPlusTermin.Text = JumlahHargaKeseluruhan - Diskon_Rupiah - DasarPengenaanPajak
            If JenisPPN = JenisPPN_Include Then txt_UangMukaPlusTermin.Text = JumlahHargaKeseluruhan - Diskon_Rupiah - TotalTagihanKotor
        Else
            lbl_UangMukaPlusTermin.Visibility = Visibility.Collapsed
            txt_UangMukaPlusTermin.Visibility = Visibility.Collapsed
            txt_UangMukaPlusTermin.Text = Kosongan
        End If

        lbl_PPN.Text = "PPN (" & TarifPPN & " %)"

        If PPN > 0 Then
            lbl_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            lbl_PPN.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
        End If

        If PPhTerutang > 0 Then
            'lbl_DasarPengenaanPajak.Visibility = Visibility.Visible
            'txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            lbl_PPhTerutang.Visibility = Visibility.Visible
            txt_PPhTerutang.Visibility = Visibility.Visible
        End If

        If PPhDitanggung > 0 Then
            lbl_PPhDitanggung.Visibility = Visibility.Visible
            txt_PPhDitanggung.Visibility = Visibility.Visible
            If PPhDipotong > 0 Then
                lbl_PPhDipotong.Visibility = Visibility.Visible
                txt_PPhDipotong.Visibility = Visibility.Visible
            End If
        End If

        If OngkosKirim > 0 Then
            lbl_OngkosKirim.Visibility = Visibility.Visible
            txt_OngkosKirim.Visibility = Visibility.Visible
        Else
            lbl_OngkosKirim.Visibility = Visibility.Collapsed
            txt_OngkosKirim.Visibility = Visibility.Collapsed
        End If

        lbl_Direktur.Text = "Direktur"
        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        InvoiceIDR = False
        InvoiceNonIDR = False

        lbl_NomorInvoice.Text = Kosongan
        lbl_TanggalInvoice.Text = Kosongan
        lbl_KodeProject.Text = Kosongan
        lbl_NomorSJBAST.Text = Kosongan
        lbl_TanggalSJBAST.Text = Kosongan
        lbl_TanggalJatuhTempo.Text = Kosongan
        lbl_NamaCustomer.Text = Kosongan
        lbl_AlamatCustomer.Text = Kosongan

        lbl_Notes.Text = Kosongan

        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon.Text = Kosongan
        txt_Termin.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan

        lbl_Diskon.Visibility = Visibility.Collapsed
        lbl_Termin.Visibility = Visibility.Collapsed
        lbl_DasarPengenaanPajak.Visibility = Visibility.Collapsed
        lbl_PPN.Visibility = Visibility.Collapsed
        lbl_PPhTerutang.Visibility = Visibility.Collapsed
        lbl_PPhDitanggung.Visibility = Visibility.Collapsed
        lbl_PPhDipotong.Visibility = Visibility.Collapsed

        txt_Diskon.Visibility = Visibility.Collapsed
        txt_Termin.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
        txt_PPN.Visibility = Visibility.Collapsed
        txt_PPhTerutang.Visibility = Visibility.Collapsed
        txt_PPhDitanggung.Visibility = Visibility.Collapsed
        txt_PPhDipotong.Visibility = Visibility.Collapsed

        lbl_TotalTagihanTerbilang.Text = Kosongan

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
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan
        Dim TotalHarga

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorPatokan_Cetak & "' ",
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

    Private Sub txt_UangMukaPlusTermin_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMukaPlusTermin.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_UangMukaPlusTermin)
    End Sub

    Private Sub txt_Termin_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Termin)
    End Sub

    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_DasarPengenaanPajak)
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPN.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPN)
    End Sub

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhTerutang)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhDitanggung)
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhDipotong)
    End Sub

    Private Sub txt_OngkosKirim_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_OngkosKirim.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_OngkosKirim)
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
