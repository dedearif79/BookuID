Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuBankGaransi
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuBankGaransi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Bank Garansi"
        Inisialisasi()
        Me.Content = usc_BukuBankGaransi
    End Sub

    Sub Inisialisasi()
        usc_BukuBankGaransi = New wpfUsc_BukuBankGaransi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuBankGaransi.RefreshTampilanData()
    End Sub

End Class
