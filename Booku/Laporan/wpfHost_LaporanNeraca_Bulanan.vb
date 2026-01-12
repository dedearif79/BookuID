Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanNeraca_Bulanan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Neraca Bulanan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanNeraca_Bulanan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Neraca - Bulanan"
        Inisialisasi()
        Me.Content = usc_LaporanNeraca_Bulanan
    End Sub

    Sub Inisialisasi()
        usc_LaporanNeraca_Bulanan = New wpfUsc_LaporanNeraca_Bulanan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanNeraca_Bulanan.RefreshTampilanData()
    End Sub

End Class
