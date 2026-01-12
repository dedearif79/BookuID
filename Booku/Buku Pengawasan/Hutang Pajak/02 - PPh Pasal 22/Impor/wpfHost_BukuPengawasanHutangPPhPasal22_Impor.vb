Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal22_Impor
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal22_Impor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan PPh Pasal 22 - Impor"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal22_Impor
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal22_Impor = New wpfUsc_BukuPengawasanHutangPPhPasal22_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.JenisPajak = JenisPajak_PPhPasal22_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.COAPajak = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL22_IMPOR
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal22_Impor.RefreshTampilanData()
    End Sub

End Class
