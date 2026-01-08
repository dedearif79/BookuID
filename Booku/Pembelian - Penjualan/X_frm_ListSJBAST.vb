Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_ListSJBAST

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Dim TabelDatabase_SJBAST
    Dim TabelDatabase_PO
    Dim Kolom_KodeMitra
    Public JalurMasuk
    Public Sisi
    Public JudulForm
    Public SisiPembelian = "Sisi Pembelian"
    Public SisiPenjualan = "Sisi Penjualan"
    Public NomorSJBAST_Terseleksi As String
    Public TanggalSJBAST_Terseleksi As String
    Public JenisSurat_Terseleksi As String
    Public TanggalDiterima_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public NomorPO_Terseleksi As String
    Public OngkosKirim_Terseleksi As Int64

    Dim CariSJBAST
    Dim PilihKodeMitra
    Public PilihTanggalDiterimaSJBAST

    Public Mitra_Semua = "Semua"
    Public TanggalSJBAST_Semua As Date = TanggalKosong

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    'Variabel Tabel :
    Dim NomorSJBAST
    Dim NomorSJBAST_Singkirkan
    Dim NomorSJBAST_Sebelumnya
    Dim TanggalSJBAST
    Dim JenisSurat
    Dim TanggalDiterima
    Dim KodeMitra
    Dim NamaMitra
    Dim AlamatMitra
    Dim NomorPO
    Dim NomorPO_Satuan
    Dim NomorPO_Sebelumnya
    Dim BiayaTransportasi As Int64

    'Variabel Filter :
    Dim FilterJenisProdukInduk = " "
    Dim FilterJenisPPN
    Dim FilterPerlakuanPPN
    Dim FilterMitra
    Dim FilterTanggalSJBAST
    Dim FilterKetersediaan
    Dim FilterCariSJBAST = " "

    Public NomorPO_HarusSama As Boolean
    Public NomorPO_YangHarusDisamakan

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If Sisi = SisiPembelian Then
            JudulForm = "Daftar Surat Jalan / BAST - Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase_SJBAST = "tbl_Pembelian_SJ"
            Kolom_KodeMitra = "Kode_Supplier"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Supplier"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Supplier"
            DataTabelUtama.Columns("Alamat_Mitra").HeaderText = "Alamat Supplier"
        End If

        If Sisi = SisiPenjualan Then
            JudulForm = "Daftar Surat Jalan / BAST - Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase_SJBAST = "tbl_Penjualan_SJ"
            Kolom_KodeMitra = "Kode_Customer"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Customer"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Customer"
            DataTabelUtama.Columns("Alamat_Mitra").HeaderText = "Alamat Customer"
        End If

        Me.Text = JudulForm

        KontenCombo_Mitra()

        TampilkanData()

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            DataTabelUtama.Columns("Biaya_Transportasi").Visible = True
        Else
            DataTabelUtama.Columns("Biaya_Transportasi").Visible = False
        End If

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        Sisi = Kosongan           'Penting...!!! Jangan dihapus...!!!
        TabelDatabase_SJBAST = Kosongan 'Penting...!!! Jangan dihapus...!!!
        lbl_FilterMitra.Enabled = True  'Penting...!!! Jangan dihapus...!!!
        cmb_Mitra.Enabled = True        'Penting...!!! Jangan dihapus...!!!
        txt_CariSJBAST.Text = Kosongan
        cmb_Mitra.Text = Pilihan_Semua
        PilihTanggalDiterimaSJBAST = TanggalSJBAST_Semua

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        NomorPO_HarusSama = False
        NomorPO_YangHarusDisamakan = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case Sisi
            Case SisiPembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
            Case SisiPenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        If Sisi = Kosongan Or TabelDatabase_SJBAST = Kosongan Then Return

        'Filter Jenis Produk :
        If JenisProduk_Induk = JenisProduk_Semua Or JenisProduk_Induk = Kosongan Then
            FilterJenisProdukInduk = " "
        Else
            FilterJenisProdukInduk = " AND ( Jenis_Produk_Induk = '" & JenisProduk_Induk & "' ) "
        End If

        'Filter Jenis PPN :
        If JenisPPN = Kosongan Then
            FilterJenisPPN = " "
        Else
            FilterJenisPPN = " AND Jenis_PPN = '" & JenisPPN & "' "
        End If

        'Filter Perlakuan PPN :
        If PerlakuanPPN = Kosongan Then
            FilterPerlakuanPPN = " "
        Else
            FilterPerlakuanPPN = " AND Perlakuan_PPN = '" & PerlakuanPPN & "' "
        End If

        'Filter Mitra :
        If PilihKodeMitra = Mitra_Semua Then
            FilterMitra = " "
        Else
            FilterMitra = " AND " & Kolom_KodeMitra & " = '" & PilihKodeMitra & "' "
        End If

        'Filter Tanggal :
        If PilihTanggalDiterimaSJBAST = TanggalSJBAST_Semua Then
            FilterTanggalSJBAST = " "
        Else
            FilterTanggalSJBAST = " " & " AND Tanggal_Diterima = '" & TanggalFormatSimpan(PilihTanggalDiterimaSJBAST) & "' "
        End If


        'Filter Keseluruhan :
        FilterData = FilterMitra & FilterTanggalSJBAST & FilterJenisProdukInduk & FilterJenisPPN & FilterPerlakuanPPN

        PesanUntukProgrammer("Filter Data : " & FilterData)

        Select Case JenisProduk_Induk
            Case Kosongan
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_Semua
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_Barang
                TampilkanData_SJ()
            Case JenisProduk_Jasa
                TampilkanData_BAST()
            Case JenisProduk_BarangDanJasa
                TampilkanData_SJ()
                TampilkanData_BAST()
            Case JenisProduk_JasaKonstruksi
                TampilkanData_BAST()
        End Select

        AksesDatabase_Transaksi(Tutup)

        NomorSJBAST_Sebelumnya = Kosongan

        'Singkirkan SJ/BAST yang sudah tersimpan di data Invoice :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST_Singkirkan = dr.Item("Nomor_SJ_BAST_Produk")
            If NomorSJBAST_Singkirkan <> NomorSJBAST_Sebelumnya Then
                For Each row2 As DataGridViewRow In DataTabelUtama.Rows
                    If row2.Cells("Nomor_SJ_BAST").Value = NomorSJBAST_Singkirkan Then
                        DataTabelUtama.Rows.Remove(row2)
                    End If
                Next
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Singkirkan
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Singkirkan SJ/BAST yang sudah ditambahkan ke Tabel SJBAST pada Form Input Invoice Penjualan :
        If JalurMasuk = Form_INPUTINVOICEPENJUALAN Then
            For Each row As DataGridViewRow In frm_Input_InvoicePenjualan.dgv_SJBAST.Rows
                NomorSJBAST_Singkirkan = row.Cells("Nomor_SJ_BAST").Value
                For Each row2 As DataGridViewRow In DataTabelUtama.Rows
                    If row2.Cells("Nomor_SJ_BAST").Value = NomorSJBAST_Singkirkan Then
                        DataTabelUtama.Rows.Remove(row2)
                    End If
                Next
            Next
        End If

        'Singkirkan SJ/BAST yang Nomor PO-nya tidak sama, Jika List-nya harus sama dengan Nomor PO yang sama :
        If NomorPO_HarusSama = True Then
            Dim BarisUntukDihapus As New List(Of DataGridViewRow)
            For Each row As DataGridViewRow In DataTabelUtama.Rows
                If row.Cells("Nomor_PO").Value <> NomorPO_YangHarusDisamakan Then
                    BarisUntukDihapus.Add(row)
                End If
            Next
            For Each row As DataGridViewRow In BarisUntukDihapus
                DataTabelUtama.Rows.Remove(row)
            Next
        End If

        BersihkanSeleksi()
        txt_CariSJBAST.Focus()

    End Sub

    Sub TampilkanData_SJ()

        JenisSurat = "SJ"

        If Sisi = SisiPembelian Then
            TabelDatabase_SJBAST = "tbl_Pembelian_SJ"
            TabelDatabase_PO = "tbl_Pembelian_PO"
        End If
        If Sisi = SisiPenjualan Then
            TabelDatabase_SJBAST = "tbl_Penjualan_SJ"
            TabelDatabase_PO = "tbl_Penjualan_PO"
        End If

        'Filter Pencarian
        FilterCariSJBAST = " "
        If CariSJBAST <> Kosongan Then
            FilterCariSJBAST = " AND ( Nomor_SJ LIKE '%" & CariSJBAST & "%' ) "
        End If

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase_SJBAST &
            " WHERE Nama_Penerima <> '' " & FilterData & FilterCariSJBAST &
            " ORDER BY Angka_SJ "

        'Data Tabel :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        NomorSJBAST_Sebelumnya = Kosongan
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ")
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ"))
                TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
                KodeMitra = dr.Item(Kolom_KodeMitra)
                NamaMitra = Kosongan
                AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                             " WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_SJBAST &
                                              " WHERE Nomor_SJ = '" & NomorSJBAST & " ' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                NomorPO = Kosongan
                NomorPO_Sebelumnya = Kosongan
                BiayaTransportasi = 0
                Do While drTELUSUR2.Read
                    NomorPO_Satuan = drTELUSUR2.Item("Nomor_PO_Produk")
                    If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                        If NomorPO = Kosongan Then
                            NomorPO = NomorPO_Satuan
                        Else
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & NomorPO_Satuan
                        End If
                        AmbilValue_BiayaTransportasi()
                    End If
                    NomorPO_Sebelumnya = NomorPO_Satuan
                Loop
                TambahBaris()
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop

    End Sub

    Sub TampilkanData_BAST()

        JenisSurat = "BAST"

        If Sisi = SisiPembelian Then
            TabelDatabase_SJBAST = "tbl_Pembelian_BAST"
            TabelDatabase_PO = "tbl_Pembelian_PO"
        End If
        If Sisi = SisiPenjualan Then
            TabelDatabase_SJBAST = "tbl_Penjualan_BAST"
            TabelDatabase_PO = "tbl_Penjualan_PO"
        End If

        'Filter Pencarian
        FilterCariSJBAST = " "
        If CariSJBAST <> Kosongan Then
            FilterCariSJBAST = " AND ( Nomor_BAST LIKE '%" & CariSJBAST & "%' ) "
        End If

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase_SJBAST &
            " WHERE Yang_Menerima <> '' " & FilterData & FilterCariSJBAST &
            " ORDER BY Angka_BAST "

        'Data Tabel :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        NomorSJBAST_Sebelumnya = Kosongan
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_BAST")
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_BAST"))
                TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
                KodeMitra = dr.Item(Kolom_KodeMitra)
                NamaMitra = Kosongan
                AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                             " WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_SJBAST &
                                              " WHERE Nomor_BAST = '" & NomorSJBAST & " ' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                NomorPO = Kosongan
                NomorPO_Sebelumnya = Kosongan
                BiayaTransportasi = 0
                Do While drTELUSUR2.Read
                    NomorPO_Satuan = drTELUSUR2.Item("Nomor_PO_Produk")
                    If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                        If NomorPO = Kosongan Then
                            NomorPO = NomorPO_Satuan
                        Else
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & NomorPO_Satuan
                        End If
                        'AmbilValue_BiayaTransportasi()  'Tidak ada Biaya Transportasi pada Jasa...!!! Seandainya mau diadakan, posisinya harus di sini...!!!
                    End If
                    NomorPO_Sebelumnya = NomorPO_Satuan
                Loop
                TambahBaris()
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop

    End Sub

    Sub TambahBaris()
        If Sisi = SisiPembelian And AdaSJBASTdiDataInvoicePembelian(NomorSJBAST) Then Return
        If Sisi = SisiPenjualan And AdaSJBASTdiDataInvoicePenjualan(NomorSJBAST) Then Return
        DataTabelUtama.Rows.Add(NomorSJBAST, TanggalSJBAST, JenisSurat, TanggalDiterima, KodeMitra, NamaMitra, AlamatMitra, NomorPO, BiayaTransportasi)
    End Sub

    Sub AmbilValue_BiayaTransportasi()
        cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM " & TabelDatabase_PO &
                                      " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR3_ExecuteReader()
        drTELUSUR3.Read()
        If drTELUSUR3.HasRows Then
            BiayaTransportasi += drTELUSUR3.Item("Biaya_Transportasi")
        End If
    End Sub


    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        NomorSJBAST_Terseleksi = Kosongan
        TanggalSJBAST_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        NomorPO_Terseleksi = Kosongan
        OngkosKirim_Terseleksi = 0
    End Sub

    Private Sub txt_CariSJBAST_TextChanged(sender As Object, e As EventArgs) Handles txt_CariSJBAST.TextChanged
        CariSJBAST = txt_CariSJBAST.Text
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
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        JenisSurat_Terseleksi = DataTabelUtama.Item("Jenis_Surat", Baris_Terseleksi).Value
        TanggalDiterima_Terseleksi = DataTabelUtama.Item("Tanggal_Diterima", Baris_Terseleksi).Value
        KodeMitra_Terseleksi = DataTabelUtama.Item("Kode_Mitra", Baris_Terseleksi).Value
        NamaMitra_Terseleksi = DataTabelUtama.Item("Nama_Mitra", Baris_Terseleksi).Value
        AlamatMitra_Terseleksi = DataTabelUtama.Item("Alamat_Mitra", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        OngkosKirim_Terseleksi = AmbilAngka(DataTabelUtama.Item("Biaya_Transportasi", Baris_Terseleksi).Value)
        txt_CariSJBAST.Text = Kosongan
        Me.Close()
    End Sub

End Class