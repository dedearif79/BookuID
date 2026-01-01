Imports bcomm
Imports DocumentFormat.OpenXml.Drawing
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DesainPO

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        ProsesLoadingForm = True

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Angka_PO = '" & NomorPatokan_Cetak & "' ", KoneksiDatabaseTransaksi)
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
        Dim Attention = dr.Item("Attention")
        Dim NomorPO = dr.Item("Nomor_PO")
        Dim TanggalPO = dr.Item("Tanggal_PO")
        Dim TermOfPayment = dr.Item("Term_Of_Payment")
        Dim KeteranganToP = dr.Item("Keterangan_ToP")
        If AmbilAngka(TermOfPayment) > 0 Then
            TermOfPayment &= " Hari"
        Else
            TermOfPayment = Kosongan
        End If
        If TermOfPayment = Kosongan Then
            TermOfPayment = KeteranganToP
        Else
            If KeteranganToP <> Kosongan Then TermOfPayment = TermOfPayment & " / " & KeteranganToP
        End If
        Dim KodeProject = dr.Item("Kode_Project_Produk")
        Dim JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Dim Diskon_Persen = dr.Item("Diskon")
        Dim Diskon_Rupiah As Int64 = JumlahHargaKeseluruhan * (Diskon_Persen / 100)
        Dim DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        'Dim TarifPPN As Decimal = FormatUlangDesimal(dr.Item("Tarif_PPN"))
        Dim PPN = dr.Item("PPN")
        Dim JenisPPh = dr.Item("Jenis_PPh")
        Dim TarifPPh As String = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh")) & " %"
        Dim PPhTerutang = dr.Item("PPh_Terutang")
        Dim PPhDitanggung = dr.Item("PPh_Ditanggung")
        Dim PPhDipotong = dr.Item("PPh_Dipotong")
        Dim TotalTagihan = dr.Item("Total_Tagihan")
        Dim TotalTagihan_Terbilang = AngkaTerbilangRupiah(TotalTagihan)
        Dim Catatan = dr.Item("Catatan")
        Dim PembuatPO = dr.Item("Pembuat_PO")
        Dim MetodePembayaran = dr.Item("Metode_Pembayaran")
        Dim UangMuka_Persen = FormatUlangDesimal_Prosentase(dr.Item("Uang_Muka"))
        Dim Termin1_Persen = FormatUlangDesimal_Prosentase(dr.Item("Termin_1"))
        Dim Termin2_Persen = FormatUlangDesimal_Prosentase(dr.Item("Termin_2"))
        Dim Pelunasan_Persen = FormatUlangDesimal_Prosentase(dr.Item("Pelunasan"))
        Dim UangMuka_Rp As Int64
        Dim Termin1_Rp As Int64
        Dim Termin2_Rp As Int64
        Dim Pelunasan_Rp As Int64

        UangMuka_Rp = (UangMuka_Persen / 100) * DasarPengenaanPajak
        Termin1_Rp = (Termin1_Persen / 100) * DasarPengenaanPajak
        Termin2_Rp = (Termin2_Persen / 100) * DasarPengenaanPajak
        Pelunasan_Rp = DasarPengenaanPajak - (UangMuka_Rp + Termin1_Rp + Termin2_Rp)

        txt_UangMuka_Persen.Text = "(" & UangMuka_Persen & " %)"
        txt_Termin1_Persen.Text = "(" & Termin1_Persen & " %)"
        txt_Termin2_Persen.Text = "(" & Termin2_Persen & " %)"
        txt_Pelunasan_Persen.Text = "(" & Pelunasan_Persen & " %)"

        txt_UangMuka_Rp.Text = UangMuka_Rp
        txt_Termin1_Rp.Text = Termin1_Rp
        txt_Termin2_Rp.Text = Termin2_Rp
        txt_Pelunasan_Rp.Text = Pelunasan_Rp

        AksesDatabase_Transaksi(Tutup)

        lbl_NomorPO.Text = NomorPO
        lbl_TanggalPO.Text = TanggalPO
        lbl_TermOfPayment.Text = TermOfPayment
        lbl_KodeProject.Text = KodeProject
        lbl_NamaSupplier.Text = NamaSupplier
        lbl_AlamatSupplier.Text = AlamatSupplier

        If MetodePembayaran = MetodePembayaran_Normal Then
            pnl_Termin.Visibility = Visibility.Collapsed
        Else
            pnl_Termin.Visibility = Visibility.Visible
            If UangMuka_Rp > 0 Then
                lbl_UangMuka.Visibility = Visibility.Visible
                txt_UangMuka_Persen.Visibility = Visibility.Visible
                tkd_UangMuka.Visibility = Visibility.Visible
                txt_UangMuka_Rp.Visibility = Visibility.Visible
            Else
                lbl_UangMuka.Visibility = Visibility.Collapsed
                txt_UangMuka_Persen.Visibility = Visibility.Collapsed
                tkd_UangMuka.Visibility = Visibility.Collapsed
                txt_UangMuka_Rp.Visibility = Visibility.Collapsed
            End If
            If Termin1_Rp > 0 Then
                lbl_Termin1.Visibility = Visibility.Visible
                txt_Termin1_Persen.Visibility = Visibility.Visible
                tkd_Termin1.Visibility = Visibility.Visible
                txt_Termin1_Rp.Visibility = Visibility.Visible
            Else
                lbl_Termin1.Visibility = Visibility.Collapsed
                txt_Termin1_Persen.Visibility = Visibility.Collapsed
                tkd_Termin1.Visibility = Visibility.Collapsed
                txt_Termin1_Rp.Visibility = Visibility.Collapsed
            End If
            If Termin2_Rp > 0 Then
                lbl_Termin2.Visibility = Visibility.Visible
                txt_Termin2_Persen.Visibility = Visibility.Visible
                tkd_Termin2.Visibility = Visibility.Visible
                txt_Termin2_Rp.Visibility = Visibility.Visible
            Else
                lbl_Termin2.Visibility = Visibility.Collapsed
                txt_Termin2_Persen.Visibility = Visibility.Collapsed
                tkd_Termin2.Visibility = Visibility.Collapsed
                txt_Termin2_Rp.Visibility = Visibility.Collapsed
            End If
            If Pelunasan_Rp > 0 Then
                lbl_Pelunasan.Visibility = Visibility.Visible
                txt_Pelunasan_Persen.Visibility = Visibility.Visible
                tkd_Pelunasan.Visibility = Visibility.Visible
                txt_Pelunasan_Rp.Visibility = Visibility.Visible
            Else
                lbl_Pelunasan.Visibility = Visibility.Collapsed
                txt_Pelunasan_Persen.Visibility = Visibility.Collapsed
                tkd_Pelunasan.Visibility = Visibility.Collapsed
                txt_Pelunasan_Rp.Visibility = Visibility.Collapsed
            End If
        End If

        TampilkanDataTabel()

        lbl_Notes.Text = Catatan

        Dim TarifPPN_Tampilan As Decimal = 0
        LogikaTampilanPPN_UntukCetakNota(TanggalPO, DasarPengenaanPajak, TarifPPN_Tampilan)

        txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
        txt_Diskon.Text = Diskon_Rupiah
        txt_DasarPengenaanPajak.Text = DasarPengenaanPajak
        txt_PPN.Text = PPN
        txt_PPhTerutang.Text = PPhTerutang
        txt_PPhDitanggung.Text = PPhDitanggung
        txt_PPhDipotong.Text = PPhDipotong
        txt_TotalTagihan.Text = TotalTagihan
        lbl_TotalTagihanTerbilang.Text = TotalTagihan_Terbilang

        If Diskon_Rupiah > 0 Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon.Visibility = Visibility.Visible
        End If

        lbl_PPN.Text = "PPN (" & TarifPPN_Tampilan & " %)"

        If PPN > 0 Then
            lbl_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            lbl_PPN.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
        End If

        If PPhTerutang > 0 Then
            lbl_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
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

        lbl_PembuatPO.Text = "Pembuat PO"
        lbl_NamaPembuatPO.Text = PembuatPO

        lbl_Direktur.Text = "Direktur"
        lbl_NamaDirektur.Text = NamaDirekturPerusahaan

        ProsesLoadingForm = True

    End Sub

    Sub ResetForm()

        lbl_NomorPO.Text = Kosongan
        lbl_TanggalPO.Text = Kosongan
        lbl_TermOfPayment.Text = Kosongan
        lbl_KodeProject.Text = Kosongan
        lbl_NamaSupplier.Text = Kosongan
        lbl_AlamatSupplier.Text = Kosongan

        lbl_Notes.Text = Kosongan

        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan

        lbl_Diskon.Visibility = Visibility.Collapsed
        lbl_PPhTerutang.Visibility = Visibility.Collapsed
        lbl_PPhDitanggung.Visibility = Visibility.Collapsed
        lbl_PPhDipotong.Visibility = Visibility.Collapsed

        txt_Diskon.Visibility = Visibility.Collapsed
        txt_PPhTerutang.Visibility = Visibility.Collapsed
        txt_PPhDitanggung.Visibility = Visibility.Collapsed
        txt_PPhDipotong.Visibility = Visibility.Collapsed

        lbl_TotalTagihanTerbilang.Text = Kosongan

        lbl_PembuatPO.Text = Kosongan
        lbl_NamaPembuatPO.Text = Kosongan
        lbl_Manager.Text = Kosongan
        lbl_NamaManager.Text = Kosongan
        lbl_Direktur.Text = Kosongan
        lbl_NamaDirektur.Text = Kosongan

    End Sub


    Private Sub txt_UangMuka_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMuka_Rp.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_UangMuka_Rp)
    End Sub

    Private Sub txt_Termin1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin1_Rp.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Termin1_Rp)
    End Sub

    Private Sub txt_Termin2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin2_Rp.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Termin2_Rp)
    End Sub

    Private Sub txt_Pelunasan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pelunasan_Rp.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Pelunasan_Rp)
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
        cmd = New Odbc.OdbcCommand(" Select * FROM tbl_Pembelian_PO " &
                                   " WHERE Angka_PO = '" & NomorPatokan_Cetak & "' ",
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

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhTerutang)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhDitanggung)
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPhDipotong)
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
