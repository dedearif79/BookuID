Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BundelPengajuanPengeluaranBankCash
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Bundel Pengajuan Pengeluaran Bank Cash (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_BundelPengajuanPengeluaranBankCash
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Bundel Pengajuan Pengeluaran"
        Inisialisasi()
        Me.Content = usc_BundelPengajuanPengeluaranBankCash
    End Sub

    Sub Inisialisasi()
        usc_BundelPengajuanPengeluaranBankCash = New wpfUsc_BundelPengajuanPengeluaranBankCash
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BundelPengajuanPengeluaranBankCash.RefreshTampilanData()
    End Sub

End Class
