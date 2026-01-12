Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPajakImpor
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPajakImpor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Pajak-pajak Impor"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPajakImpor
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPajakImpor = New wpfUsc_BukuPengawasanPajakImpor
        usc_BukuPengawasanPajakImpor.JenisPajak = JenisPajak_PajakPajakImpor
        usc_BukuPengawasanPajakImpor.AwalanBP = AwalanBPHP23
        usc_BukuPengawasanPajakImpor.COABeaMasukImpor = KodeTautanCOA_BeaMasuk_Impor
        usc_BukuPengawasanPajakImpor.COAPPhPasal22Impor = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor
        usc_BukuPengawasanPajakImpor.COAPPNMasukanImpor = KodeTautanCOA_PPNMasukan_Impor
        usc_BukuPengawasanPajakImpor.NamaHalaman = Halaman_BUKUPENGAWASANPAJAKPAJAKIMPOR
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPajakImpor.RefreshTampilanData()
    End Sub

End Class
