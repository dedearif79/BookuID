Imports bcomm
Imports System.Data.Odbc

Public Class frm_ListInvoice

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Dim TabelDatabase
    Dim Kolom_KodeMitra
    Public JalurMasuk
    Public FungsiForm
    Public JudulForm
    Public FungsiForm_InvoicePembelian = "Invoice Pembelian"
    Public FungsiForm_InvoicePenjualan = "Invoice Penjualan"
    Public NomorInvoice_Terseleksi As String
    Public TanggalInvoice_Terseleksi As String
    Public KodeMitra_Terseleksi As String
    Public NamaMitra_Terseleksi As String
    Public AlamatMitra_Terseleksi As String
    Public KodeProject_Terseleksi As String

    Public JenisPPN
    Public PerlakuanPPN

    Dim CariInvoice
    Dim PilihKodeMitra
    Public PilihTanggalInvoice
    Public PilihYangSudahDijurnal As Boolean

    Dim Mitra_Semua = "Semua"
    Dim TanggalInvoice_Semua = "Semua"

    Dim FilterJenisPPN
    Dim FilterPerlakuanPPN
    Dim FilterCariInvoice = " "
    Dim FilterMitra = " "
    Dim FilterTanggal = " "
    Dim FilterSudahDijurnal = " "

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_InvoicePembelian Then
            JudulForm = "Daftar Invoice Pembelian"
            lbl_FilterMitra.Text = "Filter Supplier :"
            TabelDatabase = "tbl_Pembelian_Invoice"
            Kolom_KodeMitra = "Kode_Supplier"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Supplier"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Supplier"
            DataTabelUtama.Columns("Alamat_Mitra").HeaderText = "Alamat Supplier"
        End If

        If FungsiForm = FungsiForm_InvoicePenjualan Then
            JudulForm = "Daftar Invoice Penjualan"
            lbl_FilterMitra.Text = "Filter Customer :"
            TabelDatabase = "tbl_Penjualan_Invoice"
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

        FungsiForm = Kosongan           'Penting...!!! Jangan dihapus...!!!
        TabelDatabase = Kosongan        'Penting...!!! Jangan dihapus...!!!
        lbl_FilterMitra.Enabled = True  'Penting...!!! Jangan dihapus...!!!
        cmb_Mitra.Enabled = True        'Penting...!!! Jangan dihapus...!!!
        txt_CariInvoice.Text = Kosongan
        cmb_Mitra.Text = Pilihan_Semua
        PilihTanggalInvoice = TanggalInvoice_Semua
        PilihYangSudahDijurnal = False

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_Mitra()
        cmb_Mitra.Items.Clear()
        cmb_Mitra.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        Select Case FungsiForm
            Case FungsiForm_InvoicePembelian
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
            Case FungsiForm_InvoicePenjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
            Case Kosongan
                PesanUntukProgrammer("Fungsi Form belum ditentukan")
                Return
        End Select
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Mitra.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Sub TampilkanData()

        If FungsiForm = Kosongan Or TabelDatabase = Kosongan Then Return

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
        If PilihKodeMitra <> Mitra_Semua Then FilterMitra = " AND " & Kolom_KodeMitra & " = '" & PilihKodeMitra & "' "

        'Filter Tanggal :
        If PilihTanggalInvoice <> TanggalInvoice_Semua Then FilterTanggal = " AND Tanggal_Invoice = '" & TanggalFormatSimpan(PilihTanggalInvoice) & "' "

        'Filter Sudah Dijurnal :
        If PilihYangSudahDijurnal = True Then FilterSudahDijurnal = " AND Nomor_JV > 0 "

        'Filter Pencarian
        If CariInvoice <> Kosongan Then FilterCariInvoice = " AND ( Nomor_Invoice LIKE '%" & CariInvoice & "%' ) "

        'Filter Keseluruhan :
        FilterData = FilterJenisPPN & FilterPerlakuanPPN & FilterMitra & FilterTanggal & FilterSudahDijurnal  '& FilterCariInvoice

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM " & TabelDatabase &
            " WHERE Nomor_ID <> 0 " & FilterData &
            " ORDER BY Angka_Invoice "

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        Dim NomorInvoice_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            Dim NomorInvoice = dr.Item("Nomor_Invoice")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                Dim TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                Dim KodeMitra = dr.Item(Kolom_KodeMitra)
                Dim NamaMitra = Kosongan
                Dim AlamatMitra = Kosongan
                AksesDatabase_General(Buka)
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NamaMitra = drTELUSUR.Item("Nama_Mitra")
                    AlamatMitra = drTELUSUR.Item("Alamat")
                End If
                AksesDatabase_General(Tutup)
                Dim KodeProject = "Belum ada Coding untuk Ini."  '<---- Untuk saat ini belum diperlukan. Karena ada metode lain untuk mendapatkan value Kode Project.
                DataTabelUtama.Rows.Add(NomorInvoice, TanggalInvoice, KodeMitra, NamaMitra, AlamatMitra, KodeProject)
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi()
        txt_CariInvoice.Focus()

    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        NomorInvoice_Terseleksi = Kosongan
        TanggalInvoice_Terseleksi = Kosongan
        KodeMitra_Terseleksi = Kosongan
        NamaMitra_Terseleksi = Kosongan
        AlamatMitra_Terseleksi = Kosongan
        KodeProject_Terseleksi = Kosongan
    End Sub

    Private Sub txt_CariInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_CariInvoice.TextChanged
        CariInvoice = txt_CariInvoice.Text
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
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        KodeMitra_Terseleksi = DataTabelUtama.Item("Kode_Mitra", Baris_Terseleksi).Value
        NamaMitra_Terseleksi = DataTabelUtama.Item("Nama_Mitra", Baris_Terseleksi).Value
        AlamatMitra_Terseleksi = DataTabelUtama.Item("Alamat_Mitra", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        txt_CariInvoice.Text = Kosongan
        Me.Close()
    End Sub

End Class