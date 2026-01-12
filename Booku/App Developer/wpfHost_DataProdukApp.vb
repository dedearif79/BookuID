Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataProdukApp
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Produk App (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataProdukApp
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Produk Aplikasi"
        Inisialisasi()
        Me.Content = usc_DataProdukApp
    End Sub

    Sub Inisialisasi()
        usc_DataProdukApp = New wpfUsc_DataProdukApp
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataProdukApp.RefreshTampilanData()
    End Sub

End Class
