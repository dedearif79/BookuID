Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DaftarAmortisasiBiaya
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Daftar Amortisasi Biaya (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DaftarAmortisasiBiaya
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Daftar Amortisasi Biaya"
        Inisialisasi()
        Me.Content = usc_DaftarAmortisasiBiaya
    End Sub

    Sub Inisialisasi()
        usc_DaftarAmortisasiBiaya = New wpfUsc_DaftarAmortisasiBiaya
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DaftarAmortisasiBiaya.RefreshTampilanData()
    End Sub

End Class
