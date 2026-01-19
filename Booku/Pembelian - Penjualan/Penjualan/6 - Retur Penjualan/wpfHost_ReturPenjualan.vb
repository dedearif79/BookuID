Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_ReturPenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Retur Penjualan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_ReturPenjualan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Retur Penjualan"
        Inisialisasi()
        Me.Content = usc_ReturPenjualan
    End Sub

    Sub Inisialisasi()
        usc_ReturPenjualan = New wpfUsc_ReturPenjualan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        ' usc_ReturPenjualan tidak memiliki RefreshTampilanData
    End Sub

End Class
