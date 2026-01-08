Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_ListLawanTransaksi

    Dim QueryTampilan
    Dim BarisTerseleksi
    Dim FilterData
    Public JalurMasuk
    Public FungsiForm
    Public KodeMitraTerseleksi, NamaMitraTerseleksi

    Private Sub frm_COA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KontenComboKategori()
        If FungsiForm = "SEMUA" Then
            cmb_Kategori.Text = "SEMUA"
        End If
        RefreshTampilanData()
        txt_CariMitra.Focus()
    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        'Filter Kategori :
        Dim FilterKategori = " "
        If cmb_Kategori.Text = Pilihan_Semua Then FilterKategori = " "
        If cmb_Kategori.Text = Mitra_Supplier Then FilterKategori = " AND Supplier = 1 "
        If cmb_Kategori.Text = Mitra_Customer Then FilterKategori = " AND Customer = 1 "

        'Filter Pencarian
        Dim FilterPencarian = " "
        If txt_CariMitra.Text <> "" Then
            Dim Srch = txt_CariMitra.Text
            Dim clm_KodeMitra = " Kode_Mitra LIKE '%" & Srch & "%' "
            Dim clm_NamaMitra = " OR Nama_Mitra LIKE '%" & Srch & "%' "
            FilterPencarian = " AND (" & clm_KodeMitra & clm_NamaMitra & ") "
        End If

        'Query Tampilan :
        FilterData = FilterKategori & FilterPencarian
        QueryTampilan = " SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra <> 'X' " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        DataTabelUtama.Rows.Add(KodeLawanTransaksi_Internal, NamaLawanTransaksi_Internal)
        DataTabelUtama.Rows.Add(KodeLawanTransaksi_DJP, NamaLawanTransaksi_DJP)
        DataTabelUtama.Rows.Add(KodeLawanTransaksi_BPJS_KS, NamaLawanTransaksi_BpjsKesehatan)
        DataTabelUtama.Rows.Add(KodeLawanTransaksi_BPJS_TK, NamaLawanTransaksi_BpjsKetenagakerjaan)
        DataTabelUtama.Rows.Add(KodeLawanTransaksi_Karyawan, NamaLawanTransaksi_Karyawan)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Kode_Mitra ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader()
        Do While dr.Read
            Dim KodeMitra = dr.Item("Kode_Mitra")
            Dim NamaMitra = dr.Item("Nama_Mitra")
            DataTabelUtama.Rows.Add(KodeMitra, NamaMitra)
        Loop
        AksesDatabase_General(Tutup)
        DataTabelUtama.ClearSelection()
        txt_CariMitra.Focus()
    End Sub

    Public Sub ResetForm()
        lbl_CariMitra.Text = "Cari Mitra :"
        txt_CariMitra.Text = ""
        btn_Pilih.Enabled = False
        BarisTerseleksi = Nothing
        KodeMitraTerseleksi = Nothing
        NamaMitraTerseleksi = Nothing
        DataTabelUtama.ClearSelection()
    End Sub

    Sub KontenComboKategori()
        cmb_Kategori.Items.Clear()
        cmb_Kategori.Items.Add(Pilihan_Semua)
        cmb_Kategori.Items.Add(Mitra_Supplier)
        cmb_Kategori.Items.Add(Mitra_Customer)
    End Sub

    Private Sub txt_CariAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_CariMitra.TextChanged
        TampilkanData()
    End Sub

    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        DataTabelUtama_CellContentClick(sender, e)
    End Sub
    Private Sub DataTabelUtama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataTabelUtama.KeyPress
    End Sub
    Private Sub DataTabelUtama_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentDoubleClick
        btn_Pilih_Click(sender, e)
    End Sub
    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        If BarisTerseleksi >= 0 Then
            btn_Pilih.Enabled = True
        Else
            btn_Pilih.Enabled = False
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Private Sub cmb_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Kategori.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Kategori.SelectedIndexChanged
    End Sub
    Private Sub cmb_Kategori_TextChanged(sender As Object, e As EventArgs) Handles cmb_Kategori.TextChanged
        TampilkanData()
    End Sub

    Private Sub btn_TambahDataMitra_Click(sender As Object, e As EventArgs)
        frm_InputMitra.btn_Reset_Click(sender, e)
        frm_InputMitra.FungsiForm = FungsiForm_TAMBAH
        frm_InputMitra.ShowDialog()
    End Sub

    Private Sub btn_Pilih_Click(sender As Object, e As EventArgs) Handles btn_Pilih.Click
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        KodeMitraTerseleksi = DataTabelUtama.Item("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaMitraTerseleksi = DataTabelUtama.Item("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        txt_CariMitra.Text = ""
        Me.Close()
    End Sub
End Class