Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanTrialBalance
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Trial Balance (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanTrialBalance
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Trial Balance"
        Inisialisasi()
        Me.Content = usc_LaporanTrialBalance
    End Sub

    Sub Inisialisasi()
        usc_LaporanTrialBalance = New wpfUsc_LaporanTrialBalance
        usc_LaporanTrialBalance.JalurMasuk = Halaman_MENUUTAMA
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanTrialBalance.RefreshTampilanData()
    End Sub

End Class
