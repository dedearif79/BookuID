Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_ListDataPemegangSaham_X

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Public JalurMasuk
    Public FungsiForm
    Public JudulForm

    Dim NamaPemegangSaham
    Dim NIK

    Public NamaPemegangSaham_Terseleksi
    Public NIK_Terseleksi

    Dim Cari_PemegangSaham
    Dim PilihKodeMitra

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Me.Text = JudulForm

        TampilkanData()

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan               'Penting...!!! Jangan dihapus...!!!
        txt_CariPemegangSaham.Text = Kosongan

        NIK_Terseleksi = Kosongan
        NamaPemegangSaham_Terseleksi = Kosongan

        ProsesResetForm = False

    End Sub

    Sub TampilkanData()

        'Filter Pencarian
        Dim FilterCariPemegangSaham = " "
        If Cari_PemegangSaham <> Kosongan Then
            FilterCariPemegangSaham = " AND ( Nama LIKE '%" & Cari_PemegangSaham & "%' ) "
        End If

        'Filter Keseluruhan :
        FilterData = FilterCariPemegangSaham

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_PemegangSaham WHERE NIK <> '' " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NIK = dr.Item("NIK")
            NamaPemegangSaham = dr.Item("Nama")
            DataTabelUtama.Rows.Add(NIK, NamaPemegangSaham)
        Loop

        AksesDatabase_General(Tutup)


        BersihkanSeleksi()
        txt_CariPemegangSaham.Focus()

    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
    End Sub

    Private Sub txt_CariKodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_CariPemegangSaham.TextChanged
        Cari_PemegangSaham = txt_CariPemegangSaham.Text
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
        NIK_Terseleksi = DataTabelUtama.Item("NIK_", Baris_Terseleksi).Value
        NamaPemegangSaham_Terseleksi = DataTabelUtama.Item("Nama_Pemegang_Saham", Baris_Terseleksi).Value
        txt_CariPemegangSaham.Text = Kosongan
        Me.Close()
    End Sub

End Class