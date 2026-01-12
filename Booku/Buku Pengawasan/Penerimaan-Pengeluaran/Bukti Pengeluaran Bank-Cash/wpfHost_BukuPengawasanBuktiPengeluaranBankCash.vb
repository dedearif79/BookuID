Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanBuktiPengeluaranBankCash
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanBuktiPengeluaranBankCash
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Bukti Pengeluaran Bank-Cash"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanBuktiPengeluaranBankCash
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPengeluaranBankCash = New wpfUsc_BukuPengawasanBuktiPengeluaranBankCash
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPengeluaranBankCash.RefreshTampilanData()
    End Sub

End Class
