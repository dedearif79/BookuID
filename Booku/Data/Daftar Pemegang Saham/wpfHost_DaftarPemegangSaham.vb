Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DaftarPemegangSaham
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Daftar Pemegang Saham (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DaftarPemegangSaham
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Daftar Pemegang Saham"
        Inisialisasi()
        Me.Content = usc_DaftarPemegangSaham
    End Sub

    Sub Inisialisasi()
        usc_DaftarPemegangSaham = New wpfUsc_DaftarPemegangSaham
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DaftarPemegangSaham.RefreshTampilanData()
    End Sub

End Class
