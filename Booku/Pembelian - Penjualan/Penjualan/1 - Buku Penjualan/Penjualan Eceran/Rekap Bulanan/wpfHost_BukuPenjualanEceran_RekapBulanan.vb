Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPenjualanEceran
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPenjualanEceran_RekapBulanan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Rekap Penjualan Bulanan"
        Inisialisasi()
        Me.Content = usc_BukuPenjualanEceran_RekapBulanan
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualanEceran_RekapBulanan = New wpfUsc_BukuPenjualanEceran_RekapBulanan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualanEceran_RekapBulanan.RefreshTampilanData()
    End Sub

End Class
