Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangAfiliasi
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPiutangAfiliasi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangAfiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangAfiliasi = New wpfUsc_BukuPengawasanPiutangAfiliasi
        usc_BukuPengawasanPiutangAfiliasi.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGAFILIASI
        usc_BukuPengawasanPiutangAfiliasi.JudulForm = JudulForm
        usc_BukuPengawasanPiutangAfiliasi.COAPiutang = KodeTautanCOA_PiutangAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangAfiliasi.RefreshTampilanData()
    End Sub

End Class
