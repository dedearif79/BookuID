Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanKetetapanPajak
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanKetetapanPajak
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Ketetapan Pajak"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanKetetapanPajak
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanKetetapanPajak = New wpfUsc_BukuPengawasanKetetapanPajak
        usc_BukuPengawasanKetetapanPajak.JenisPajak = JenisPajak_KetetapanPajak
        usc_BukuPengawasanKetetapanPajak.AwalanBP = AwalanBPKP
        usc_BukuPengawasanKetetapanPajak.NamaHalaman = Halaman_BUKUPENGAWASANKETETAPANPAJAK
        usc_BukuPengawasanKetetapanPajak.COAHutangPajak = KodeTautanCOA_HutangKetetapanPajak
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanKetetapanPajak.RefreshTampilanData()
    End Sub

End Class
