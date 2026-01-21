Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPenjualanEceran
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPenjualanEceran
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Penjualan Eceran"
        Inisialisasi()
        Me.Content = usc_BukuPenjualanEceran
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualanEceran = New wpfUsc_BukuPenjualanEceran
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualanEceran.RefreshTampilanData()
    End Sub

End Class
