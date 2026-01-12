Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanLabaRugi_Tahunan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Laba Rugi Tahunan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanLabaRugi_Tahunan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Laba Rugi - Tahunan"
        Inisialisasi()
        Me.Content = usc_LaporanLabaRugi_Tahunan
    End Sub

    Sub Inisialisasi()
        usc_LaporanLabaRugi_Tahunan = New wpfUsc_LaporanLabaRugi_Tahunan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanLabaRugi_Tahunan.RefreshTampilanData()
    End Sub

End Class
