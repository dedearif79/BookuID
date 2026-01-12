Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_ReturPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Retur Pembelian (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_ReturPembelian
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Retur Pembelian"
        Inisialisasi()
        Me.Content = usc_ReturPembelian
    End Sub

    Sub Inisialisasi()
        usc_ReturPembelian = New wpfUsc_ReturPembelian
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        ' usc_ReturPembelian tidak memiliki RefreshTampilanData
    End Sub

End Class
