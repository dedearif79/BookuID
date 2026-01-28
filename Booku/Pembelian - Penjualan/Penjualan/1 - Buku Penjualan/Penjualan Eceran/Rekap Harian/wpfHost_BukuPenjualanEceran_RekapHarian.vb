Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPenjualanEceran
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPenjualanEceran_RekapHarian
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Rekap Penjualan Harian"
        Inisialisasi()
        Me.Content = usc_BukuPenjualanEceran_RekapHarian
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualanEceran_RekapHarian = New wpfUsc_BukuPenjualanEceran_RekapHarian
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualanEceran_RekapHarian.RefreshTampilanData()
    End Sub

End Class
