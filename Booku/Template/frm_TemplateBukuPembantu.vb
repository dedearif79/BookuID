Imports bcomm

Public Class frm_TemplateBukuPembantu

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Dim BarisTerseleksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        TampilkanData()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True
        FungsiForm = Kosongan


        ProsesResetForm = False

    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)


        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
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


End Class