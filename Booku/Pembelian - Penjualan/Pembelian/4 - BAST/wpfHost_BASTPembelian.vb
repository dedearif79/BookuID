Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BASTPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: BAST Pembelian (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_BASTPembelian
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "BAST Pembelian"
        Inisialisasi()
        Me.Content = usc_BASTPembelian
    End Sub

    Sub Inisialisasi()
        usc_BASTPembelian = New wpfUsc_BASTPembelian With {
            .JudulForm = JudulForm
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BASTPembelian.RefreshTampilanData()
    End Sub

End Class
