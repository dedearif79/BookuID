Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal25
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal25
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 25"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal25
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal25 = New wpfUsc_BukuPengawasanHutangPPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.JenisPajak = JenisPajak_PPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.AwalanBP = AwalanBPHP25
        usc_BukuPengawasanHutangPPhPasal25.COAHutangPajak = KodeTautanCOA_HutangPPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
    End Sub

End Class
