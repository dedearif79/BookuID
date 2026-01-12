Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangAfiliasi
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangAfiliasi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangAfiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangAfiliasi = New wpfUsc_BukuPengawasanHutangAfiliasi
        usc_BukuPengawasanHutangAfiliasi.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGAFILIASI
        usc_BukuPengawasanHutangAfiliasi.JudulForm = JudulForm
        usc_BukuPengawasanHutangAfiliasi.COAHutang = KodeTautanCOA_HutangAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangAfiliasi.RefreshTampilanData()
    End Sub

End Class
