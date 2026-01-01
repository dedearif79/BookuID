Imports bcomm
Imports System.Data.Odbc

Public Class frm_ListDataProject

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Public JalurMasuk
    Public FungsiForm
    Public JudulForm
    Public NomorPO As String
    Public KodeCustomer As String
    Public NamaCustomer As String
    Public KodeProject As String
    Public NilaiProject
    Public NomorPO_Terseleksi As String
    Public KodeCustomer_Terseleksi As String
    Public NamaCustomer_Terseleksi As String
    Public KodeProject_Terseleksi As String
    Public NilaiProject_Terseleksi

    Dim Cari_KodeProject
    Dim PilihKodeMitra

    Dim Mitra_Semua = "Semua"

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        KontenCombo_Customer()

        TampilkanData()

        BeginInvoke(Sub() BersihkanSeleksi())

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan               'Penting...!!! Jangan dihapus...!!!
        lbl_FilterCustomer.Enabled = True   'Penting...!!! Jangan dihapus...!!!
        cmb_Customer.Enabled = True         'Penting...!!! Jangan dihapus...!!!
        txt_CariKodeProject.Text = Kosongan
        cmb_Customer.Text = Pilihan_Semua
        NomorPO_Terseleksi = Kosongan
        KodeCustomer_Terseleksi = Kosongan
        NamaCustomer_Terseleksi = Kosongan
        KodeProject_Terseleksi = Kosongan
        NilaiProject_Terseleksi = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_Customer()
        cmb_Customer.Items.Clear()
        cmb_Customer.Items.Add(Mitra_Semua)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            cmb_Customer.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Sub TampilkanData()

        'Filter Mitra :
        Dim FilterMitra = " "
        If PilihKodeMitra = Mitra_Semua Then
            FilterMitra = " "
        Else
            FilterMitra = " AND Kode_Customer = '" & PilihKodeMitra & "' "
        End If

        'Filter Pencarian
        Dim FilterCariKodeProject = " "
        If Cari_KodeProject <> Kosongan Then
            FilterCariKodeProject = " AND ( Kode_Project LIKE '%" & Cari_KodeProject & "%' ) "
        End If

        'Filter Keseluruhan :
        FilterData = FilterMitra & FilterCariKodeProject

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_DataProject WHERE Status = '" & Status_Open & "' " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            KodeProject = dr.Item("Kode_Project")
            NomorPO = dr.Item("Nomor_PO")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            NilaiProject = dr.Item("Nilai_Project")
            DataTabelUtama.Rows.Add(KodeProject, NomorPO, KodeCustomer, NamaCustomer, NilaiProject)
        Loop
        DataTabelUtama.Rows.Add("Campuran")
        AksesDatabase_General(Tutup)


        BersihkanSeleksi()
        txt_CariKodeProject.Focus()

    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
    End Sub

    Private Sub txt_CariKodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_CariKodeProject.TextChanged
        Cari_KodeProject = txt_CariKodeProject.Text
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub

    Private Sub cmb_Customer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Customer.SelectedIndexChanged
    End Sub
    Private Sub cmb_Customer_TextChanged(sender As Object, e As EventArgs) Handles cmb_Customer.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Customer.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then PilihKodeMitra = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Customer.Text = Mitra_Semua Then PilihKodeMitra = Mitra_Semua
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
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", Baris_Terseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", Baris_Terseleksi).Value
        NilaiProject_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nilai_Project", Baris_Terseleksi).Value)
        txt_CariKodeProject.Text = Kosongan
        Me.Close()
    End Sub

    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

End Class