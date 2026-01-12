Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangPemegangSaham
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangPemegangSaham
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Pemegang Saham"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangPemegangSaham
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPemegangSaham = New wpfUsc_BukuPengawasanHutangPemegangSaham
        usc_BukuPengawasanHutangPemegangSaham.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM
        usc_BukuPengawasanHutangPemegangSaham.JudulForm = JudulForm
        usc_BukuPengawasanHutangPemegangSaham.COAHutang = KodeTautanCOA_HutangPemegangSaham
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPemegangSaham.RefreshTampilanData()
    End Sub

End Class
