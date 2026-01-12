Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_SuratJalanPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Surat Jalan Pembelian (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_SuratJalanPembelian
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Surat Jalan Pembelian"
        Inisialisasi()
        Me.Content = usc_SuratJalanPembelian
    End Sub

    Sub Inisialisasi()
        usc_SuratJalanPembelian = New wpfUsc_SuratJalanPembelian With {
            .JudulForm = JudulForm
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_SuratJalanPembelian.RefreshTampilanData()
    End Sub

End Class
