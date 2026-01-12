Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal26
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal26
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 26"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal26
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal26 = New wpfUsc_BukuPengawasanHutangPPhPasal26
        usc_BukuPengawasanHutangPPhPasal26.JenisPajak = JenisPajak_PPhPasal26
        usc_BukuPengawasanHutangPPhPasal26.AwalanBP = AwalanBPHP26
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal26_100
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal26_101
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal26_102
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal26_103
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal26_104
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_105 = KodeTautanCOA_HutangPPhPasal26_105
        usc_BukuPengawasanHutangPPhPasal26.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
    End Sub

End Class
