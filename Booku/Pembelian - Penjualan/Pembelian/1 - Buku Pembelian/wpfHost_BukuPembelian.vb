Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Lokal
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPembelian_Lokal
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pembelian"
        Inisialisasi()
        Me.Content = usc_BukuPembelian_Lokal
    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPembelian_Lokal.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Impor
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPembelian_Impor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pembelian - Impor"
        Inisialisasi()
        Me.Content = usc_BukuPembelian_Impor
    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Impor = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPembelian_Impor.RefreshTampilanData()
    End Sub

End Class
