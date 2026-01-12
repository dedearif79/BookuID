Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_SuratJalanPenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Surat Jalan Penjualan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_SuratJalanPenjualan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Surat Jalan Penjualan"
        Inisialisasi()
        Me.Content = usc_SuratJalanPenjualan
    End Sub

    Sub Inisialisasi()
        usc_SuratJalanPenjualan = New wpfUsc_SuratJalanPenjualan With {
            .JudulForm = JudulForm
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_SuratJalanPenjualan.RefreshTampilanData()
    End Sub

End Class
