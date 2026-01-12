Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPihakKetiga
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPihakKetiga
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Pihak Ketiga"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPihakKetiga
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPihakKetiga = New wpfUsc_BukuPengawasanHutangPihakKetiga
        usc_BukuPengawasanHutangPihakKetiga.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA
        usc_BukuPengawasanHutangPihakKetiga.JudulForm = JudulForm
        usc_BukuPengawasanHutangPihakKetiga.COAHutang = KodeTautanCOA_HutangPihakKetiga
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPihakKetiga.RefreshTampilanData()
    End Sub

End Class
