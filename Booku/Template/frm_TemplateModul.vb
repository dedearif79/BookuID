Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_TemplateModul

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        FiturBelumBisaDigunakan()

        'TrialBalance_Mentahkan() 'Sub Ini kirim ke form INPUT
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        FiturBelumBisaDigunakan()

        'TrialBalance_Mentahkan() 'Sub ini kirim ke form EDIT
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        FiturBelumBisaDigunakan()

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'TrialBalance_Mentahkan()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class