Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanGaji
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanGaji
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Gaji"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanGaji
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanGaji = New wpfUsc_BukuPengawasanGaji
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanGaji.RefreshTampilanData()
    End Sub

End Class
