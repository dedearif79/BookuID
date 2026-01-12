Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangPemegangSaham
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPiutangPemegangSaham
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Pemegang Saham"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangPemegangSaham
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangPemegangSaham = New wpfUsc_BukuPengawasanPiutangPemegangSaham
        usc_BukuPengawasanPiutangPemegangSaham.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM
        usc_BukuPengawasanPiutangPemegangSaham.JudulForm = JudulForm
        usc_BukuPengawasanPiutangPemegangSaham.COAPiutang = KodeTautanCOA_PiutangPemegangSaham
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangPemegangSaham.RefreshTampilanData()
    End Sub

End Class
