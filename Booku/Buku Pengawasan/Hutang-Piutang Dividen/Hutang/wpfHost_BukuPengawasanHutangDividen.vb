Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangDividen
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangDividen
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Dividen"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangDividen
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangDividen = New wpfUsc_BukuPengawasanHutangDividen
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangDividen.RefreshTampilanData()
    End Sub

End Class
