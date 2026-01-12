Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangKaryawan
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanPiutangKaryawan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Karyawan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangKaryawan
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangKaryawan = New wpfUsc_BukuPengawasanPiutangKaryawan
        usc_BukuPengawasanPiutangKaryawan.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGKARYAWAN
        usc_BukuPengawasanPiutangKaryawan.JudulForm = JudulForm
        usc_BukuPengawasanPiutangKaryawan.COAPiutang = KodeTautanCOA_PiutangKaryawan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangKaryawan.RefreshTampilanData()
    End Sub

End Class
