Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangBankLeasing
' 1 file berisi 2 varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Hutang Bank
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangBank
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Bank"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangBank
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangBank = New wpfUsc_BukuPengawasanHutangBankLeasing
        usc_BukuPengawasanHutangBank.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBANK
        usc_BukuPengawasanHutangBank.BankLeasing = bl_Bank
        usc_BukuPengawasanHutangBank.JudulForm = JudulForm
        usc_BukuPengawasanHutangBank.COAHutang = KodeTautanCOA_HutangBank
        usc_BukuPengawasanHutangBank.TabelPengawasan = "tbl_PengawasanHutangBank"
        usc_BukuPengawasanHutangBank.TabelAngsuran = "tbl_JadwalAngsuranHutangBank"
        usc_BukuPengawasanHutangBank.KolomNomorBPH = "Nomor_BPHB"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangBank.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Hutang Leasing
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangLeasing
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Leasing"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangLeasing
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangLeasing = New wpfUsc_BukuPengawasanHutangBankLeasing
        usc_BukuPengawasanHutangLeasing.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGLEASING
        usc_BukuPengawasanHutangLeasing.BankLeasing = bl_Leasing
        usc_BukuPengawasanHutangLeasing.JudulForm = JudulForm
        usc_BukuPengawasanHutangLeasing.COAHutang = KodeTautanCOA_HutangLeasing
        usc_BukuPengawasanHutangLeasing.TabelPengawasan = "tbl_PengawasanHutangLeasing"
        usc_BukuPengawasanHutangLeasing.TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing"
        usc_BukuPengawasanHutangLeasing.KolomNomorBPH = "Nomor_BPHL"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangLeasing.RefreshTampilanData()
    End Sub

End Class
