Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangPihakKetiga
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPiutangPihakKetiga
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Pihak Ketiga"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangPihakKetiga
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangPihakKetiga = New wpfUsc_BukuPengawasanPiutangPihakKetiga
        usc_BukuPengawasanPiutangPihakKetiga.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA
        usc_BukuPengawasanPiutangPihakKetiga.JudulForm = JudulForm
        usc_BukuPengawasanPiutangPihakKetiga.COAPiutang = KodeTautanCOA_PiutangPihakKetiga
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangPihakKetiga.RefreshTampilanData()
    End Sub

End Class
