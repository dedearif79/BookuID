Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangKaryawan
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanHutangKaryawan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Karyawan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangKaryawan
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangKaryawan = New wpfUsc_BukuPengawasanHutangKaryawan
        usc_BukuPengawasanHutangKaryawan.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGKARYAWAN
        usc_BukuPengawasanHutangKaryawan.JudulForm = JudulForm
        usc_BukuPengawasanHutangKaryawan.COAHutang = KodeTautanCOA_HutangKaryawan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangKaryawan.RefreshTampilanData()
    End Sub

End Class
