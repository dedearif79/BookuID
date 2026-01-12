Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangDividen
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPiutangDividen
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Dividen"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangDividen
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangDividen = New wpfUsc_BukuPengawasanPiutangDividen
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangDividen.RefreshTampilanData()
    End Sub

End Class
