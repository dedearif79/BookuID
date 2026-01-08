Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_ListPO

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Dim TabelDatabase
    Dim Kolom_KodeMitra
    Public JalurMasuk
    Public Sisi
    Public JudulForm
    Public Sisi_POPembelian = "PO Pembelian"
    Public Sisi_POPenjualan = "PO Penjualan"
    Public NomorPO_Terseleksi As String
    Public TanggalPO_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public KodeProject_Terseleksi As String
    Public JenisProdukInduk_Terseleksi As String

    Dim CariPO
    Dim PilihKodeMitra

    Dim Mitra_Semua = "Semua"

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    Dim NomorPO_Singkirkan
    Dim NomorPO_Sebelumnya

    Public MetodePembayaran

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If Sisi = Sisi_POPembelian Then
            JudulForm = "Daftar PO Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase = "tbl_Pembelian_PO"
            Kolom_KodeMitra = "Kode_Supplier"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Supplier"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Supplier"
            DataTabelUtama.Columns("Alamat_Mitra").HeaderText = "Alamat Supplier"
        End If

        If Sisi = Sisi_POPenjualan Then
            JudulForm = "Daftar PO Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase = "tbl_Penjualan_PO"
            Kolom_KodeMitra = "Kode_Customer"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Customer"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Customer"
            DataTabelUtama.Columns("Alamat_Mitra").HeaderText = "Alamat Customer"
        End If

        Me.Text = JudulForm

        KontenCombo_Mitra()

        TampilkanData()

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        Sisi = Kosongan           'Penting...!!! Jangan dihapus...!!!
        TabelDatabase = Kosongan        'Penting...!!! Jangan dihapus...!!!
        lbl_FilterMitra.Enabled = True  'Penting...!!! Jangan dihapus...!!!
        cmb_Mitra.Enabled = True        'Penting...!!! Jangan dihapus...!!!
        txt_CariPO.Text = Kosongan
        cmb_Mitra.Text = Pilihan_Semua

        MetodePembayaran = Pilihan_Semua

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        DialogResult = DialogResult.None

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case Sisi
            Case Sisi_POPembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "' ", KoneksiDatabaseGeneral)
            Case Sisi_POPenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "' ", KoneksiDatabaseGeneral)
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Sub TampilkanData()

        If Sisi = Kosongan Or TabelDatabase = Kosongan Then Return

        'Filter Jenis Produk :
        Dim FilterJenisProduk = " "
        If JalurMasuk = Form_INPUTSURATJALANPENJUALAN Or JalurMasuk = Form_INPUTSURATJALANPEMBELIAN Then
            If JenisProduk_Induk = Kosongan Then
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_BarangDanJasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_Barang & "' ) "
            Else
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
            End If
        End If
        If JalurMasuk = Form_INPUTBASTPENJUALAN Or JalurMasuk = Form_INPUTBASTPEMBELIAN Then
            If JenisProduk_Induk = Kosongan Then
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_BarangDanJasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_Jasa & "' " &
                    " OR Jenis_Produk_Induk = '" & JenisProduk_JasaKonstruksi & "' ) "
            Else
                FilterJenisProduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
            End If
        End If

        'Filter Jenis PPN :
        Dim FilterJenisPPN
        If JenisPPN = Kosongan Then
            FilterJenisPPN = " "
        Else
            FilterJenisPPN = " AND Jenis_PPN = '" & JenisPPN & "' "
        End If

        'Filter Perlakuan PPN :
        Dim FilterPerlakuanPPN
        If PerlakuanPPN = Kosongan Then
            FilterPerlakuanPPN = " "
        Else
            FilterPerlakuanPPN = " AND Perlakuan_PPN = '" & PerlakuanPPN & "' "
        End If

        'Filter Mitra :
        Dim FilterMitra = " "
        If PilihKodeMitra = Mitra_Semua Then
            FilterMitra = " "
        Else
            FilterMitra = " AND " & Kolom_KodeMitra & " = '" & PilihKodeMitra & "' "
        End If

        'Filter Pencarian
        Dim FilterCariPO = " "
        If CariPO <> Kosongan Then
            FilterCariPO = " AND ( Nomor_PO LIKE '%" & CariPO & "%' ) "
        End If

        'Filter Metode Pembayaran
        Dim FilterMedodePembayaran
        If MetodePembayaran = Pilihan_Semua Then
            FilterMedodePembayaran = " "
        Else
            FilterMedodePembayaran = " AND Metode_Pembayaran = '" & MetodePembayaran & "' "
        End If

        'Filter Keseluruhan :
        FilterData = FilterMitra & FilterCariPO & FilterJenisProduk & FilterJenisPPN & FilterPerlakuanPPN & FilterMedodePembayaran

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase &
            " WHERE Kontrol <> '" & Status_Closed & "' " & FilterData &
            " ORDER BY Angka_PO "

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorPO_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            Dim NomorPO = dr.Item("Nomor_PO")
            If NomorPO <> NomorPO_Sebelumnya Then
                Dim TanggalPO = TanggalFormatTampilan(dr.Item("Tanggal_PO"))
                Dim KodeMitra = Kosongan
                KodeMitra = dr.Item(Kolom_KodeMitra)
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim NamaMitra = drTELUSUR.Item("Nama_Mitra")
                Dim AlamatMitra = drTELUSUR.Item("Alamat")
                Dim LokasiMitra = drTELUSUR.Item("Lokasi_WP")
                AksesDatabase_General(Tutup)
                Dim KodeProject = dr.Item("Kode_Project_Produk")
                Dim JenisProdukInduk_List = dr.Item("Jenis_Produk_Induk")
                DataTabelUtama.Rows.Add(NomorPO, TanggalPO, KodeMitra, NamaMitra, AlamatMitra, KodeProject, JenisProdukInduk_List)
            End If
            NomorPO_Sebelumnya = NomorPO
        Loop
        AksesDatabase_Transaksi(Tutup)

        NomorPO_Singkirkan = Kosongan

        'Singkirkan PO yang sudah Dipakai, Jika Menggunakan Jalur Masuk Form Input Surat Jalan :
        If JalurMasuk = Form_INPUTSURATJALANPENJUALAN Then
            For Each row As DataGridViewRow In frm_Input_SuratJalanPenjualan.dgv_PO.Rows
                NomorPO_Singkirkan = row.Cells("Nomor_PO").Value
                For Each row2 As DataGridViewRow In DataTabelUtama.Rows
                    If row2.Cells("Nomor_PO").Value = NomorPO_Singkirkan Then
                        DataTabelUtama.Rows.Remove(row2)
                    End If
                Next
            Next
        End If

        'Singkirkan PO yang sudah Dipakai, Jika Menggunakan Jalur Masuk Form Input BAST :
        If JalurMasuk = Form_INPUTBASTPENJUALAN Then
            For Each row As DataGridViewRow In frm_Input_BASTPenjualan.dgv_PO.Rows
                NomorPO_Singkirkan = row.Cells("Nomor_PO").Value
                For Each row2 As DataGridViewRow In DataTabelUtama.Rows
                    If row2.Cells("Nomor_PO").Value = NomorPO_Singkirkan Then
                        DataTabelUtama.Rows.Remove(row2)
                    End If
                Next
            Next
        End If

        BersihkanSeleksi()
        txt_CariPO.Focus()

    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        NomorPO_Terseleksi = Kosongan
        TanggalPO_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        KodeProject_Terseleksi = Kosongan
        JenisProdukInduk_Terseleksi = Kosongan
    End Sub

    Private Sub txt_CariPO_TextChanged(sender As Object, e As EventArgs) Handles txt_CariPO.TextChanged
        CariPO = txt_CariPO.Text
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub

    Private Sub cmb_Mitra_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Mitra.SelectedIndexChanged
    End Sub
    Private Sub cmb_Mitra_TextChanged(sender As Object, e As EventArgs) Handles cmb_Mitra.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Mitra.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then PilihKodeMitra = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Mitra.Text = Mitra_Semua Then PilihKodeMitra = Mitra_Semua
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        If Baris_Terseleksi >= 0 Then
            btn_Pilih.Enabled = True
        Else
            btn_Pilih.Enabled = False
        End If
    End Sub
    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        btn_Pilih_Click(sender, e)
    End Sub

    Private Sub btn_Pilih_Click(sender As Object, e As EventArgs) Handles btn_Pilih.Click
        If Baris_Terseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        KodeMitra_Terseleksi = DataTabelUtama.Item("Kode_Mitra", Baris_Terseleksi).Value
        NamaMitra_Terseleksi = DataTabelUtama.Item("Nama_Mitra", Baris_Terseleksi).Value
        AlamatMitra_Terseleksi = DataTabelUtama.Item("Alamat_Mitra", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        JenisProdukInduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk_Induk", Baris_Terseleksi).Value
        txt_CariPO.Text = Kosongan
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class
