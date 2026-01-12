Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal23
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal23
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 23"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal23
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal23 = New wpfUsc_BukuPengawasanHutangPPhPasal23
        usc_BukuPengawasanHutangPPhPasal23.JenisPajak = JenisPajak_PPhPasal23
        usc_BukuPengawasanHutangPPhPasal23.AwalanBP = AwalanBPHP23
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal23_100
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal23_101
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal23_102
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal23_103
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal23_104
        usc_BukuPengawasanHutangPPhPasal23.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
    End Sub

End Class
