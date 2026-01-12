Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPPhPasal42
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPPhPasal42
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang PPh Pasal 4 (2)"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPPhPasal42
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal42 = New wpfUsc_BukuPengawasanHutangPPhPasal42
        usc_BukuPengawasanHutangPPhPasal42.JenisPajak = JenisPajak_PPhPasal42
        usc_BukuPengawasanHutangPPhPasal42.AwalanBP = AwalanBPHP42
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_402 = KodeTautanCOA_HutangPPhPasal42_402
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_403 = KodeTautanCOA_HutangPPhPasal42_403
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_409 = KodeTautanCOA_HutangPPhPasal42_409
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_419 = KodeTautanCOA_HutangPPhPasal42_419
        usc_BukuPengawasanHutangPPhPasal42.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
    End Sub

End Class
