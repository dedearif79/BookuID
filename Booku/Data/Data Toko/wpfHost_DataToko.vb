Imports System.Windows.Controls
' =====================================================================
' WPF Host untuk wpfUsc_DataToko
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Toko (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataToko
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Toko"
        Inisialisasi()
        Me.Content = usc_DataToko
    End Sub

    Sub Inisialisasi()
        usc_DataToko = New wpfUsc_DataToko
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataToko.RefreshTampilanData()
    End Sub


End Class
