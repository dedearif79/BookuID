Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal21
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal21
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 21"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal21
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal21 = New wpfUsc_BukuPengawasanHutangPPhPasal21
        usc_BukuPengawasanHutangPPhPasal21.JenisPajak = JenisPajak_PPhPasal21
        usc_BukuPengawasanHutangPPhPasal21.AwalanBP = AwalanBPHP21
        usc_BukuPengawasanHutangPPhPasal21.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal21_100
        usc_BukuPengawasanHutangPPhPasal21.COAHutangPajak_401 = KodeTautanCOA_HutangPPhPasal21_401
        usc_BukuPengawasanHutangPPhPasal21.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
    End Sub

End Class
