Imports bcomm
Imports System.Data.Odbc

Module mdl_PrintSupport

    Public NomorPatokan_Cetak
    Sub Cetak(JenisFormCetak As String, NomorPatokan As String, TampilkanHeader As Boolean, TampilkanFooter As Boolean)
        NomorPatokan_Cetak = NomorPatokan
        win_Pratinjau = New wpfWin_Pratinjau
        win_Pratinjau.ResetForm()
        win_Pratinjau.TampilkanHeader = TampilkanHeader
        win_Pratinjau.TampilkanFooter = TampilkanFooter
        win_Pratinjau.JenisFormCetak = JenisFormCetak
        win_Pratinjau.JudulForm = "Pratinjau " & JenisFormCetak & " - " & NomorPatokan
        win_Pratinjau.ShowDialog()
    End Sub

End Module
