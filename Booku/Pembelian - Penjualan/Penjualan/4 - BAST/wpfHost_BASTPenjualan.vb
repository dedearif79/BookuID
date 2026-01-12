Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BASTPenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: BAST Penjualan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_BASTPenjualan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Berita Serah Terima Acara (BAST) Penjualan"
        Inisialisasi()
        Me.Content = usc_BASTPenjualan
    End Sub

    Sub Inisialisasi()
        usc_BASTPenjualan = New wpfUsc_BASTPenjualan With {
            .JudulForm = JudulForm
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BASTPenjualan.RefreshTampilanData()
    End Sub

End Class
