Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPemindahbukuan
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPemindahbukuan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Pemindahbukuan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPemindahbukuan
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPemindahbukuan = New wpfUsc_BukuPengawasanPemindahbukuan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPemindahbukuan.RefreshTampilanData()
    End Sub

End Class
