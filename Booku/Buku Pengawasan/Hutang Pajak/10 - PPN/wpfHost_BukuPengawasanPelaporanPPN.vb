Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPelaporanPPN
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPelaporanPPN
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Pelaporan PPN"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPelaporanPPN
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPelaporanPPN = New wpfUsc_BukuPengawasanPelaporanPPN
        usc_BukuPengawasanPelaporanPPN.JenisPajak = JenisPajak_PPN
        usc_BukuPengawasanPelaporanPPN.AwalanBP = AwalanBPHPPN
        usc_BukuPengawasanPelaporanPPN.COAHutangPajak = KodeTautanCOA_HutangPPN
        usc_BukuPengawasanPelaporanPPN.NamaHalaman = Halaman_BUKUPENGAWASANPELAPORANPPN
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPelaporanPPN.RefreshTampilanData()
    End Sub

End Class
