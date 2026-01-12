Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanNeraca_Tahunan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Neraca Tahunan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanNeraca_Tahunan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Neraca - Tahunan"
        Inisialisasi()
        Me.Content = usc_LaporanNeraca_Tahunan
    End Sub

    Sub Inisialisasi()
        usc_LaporanNeraca_Tahunan = New wpfUsc_LaporanNeraca_Tahunan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanNeraca_Tahunan.RefreshTampilanData()
    End Sub

End Class
