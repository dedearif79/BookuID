Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanLabaRugi_Bulanan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Laba Rugi Bulanan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanLabaRugi_Bulanan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Laba Rugi - Bulanan"
        Inisialisasi()
        Me.Content = usc_LaporanLabaRugi_Bulanan
    End Sub

    Sub Inisialisasi()
        usc_LaporanLabaRugi_Bulanan = New wpfUsc_LaporanLabaRugi_Bulanan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanLabaRugi_Bulanan.RefreshTampilanData()
    End Sub

End Class
