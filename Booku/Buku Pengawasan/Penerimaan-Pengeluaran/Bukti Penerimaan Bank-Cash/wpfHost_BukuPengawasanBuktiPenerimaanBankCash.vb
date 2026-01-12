Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanBuktiPenerimaanBankCash
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanBuktiPenerimaanBankCash
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Bukti Penerimaan Bank-Cash"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanBuktiPenerimaanBankCash
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPenerimaanBankCash = New wpfUsc_BukuPengawasanBuktiPenerimaanBankCash
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPenerimaanBankCash.RefreshTampilanData()
    End Sub

End Class
