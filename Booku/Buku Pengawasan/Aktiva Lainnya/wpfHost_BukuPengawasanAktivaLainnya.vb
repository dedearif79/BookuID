Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanAktivaLainnya
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanAktivaLainnya
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Aktiva Lain-lain"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanAktivaLainnya
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanAktivaLainnya = New wpfUsc_BukuPengawasanAktivaLainnya
        usc_BukuPengawasanAktivaLainnya.NamaHalaman = Halaman_BUKUPENGAWASANAKTIVALAINNYA
        usc_BukuPengawasanAktivaLainnya.JudulForm = JudulForm
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanAktivaLainnya.RefreshTampilanData()
    End Sub

End Class
