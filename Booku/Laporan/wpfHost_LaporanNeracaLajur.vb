Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanNeracaLajur
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan Neraca Lajur (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanNeracaLajur
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan Neraca Lajur"
        Inisialisasi()
        Me.Content = usc_LaporanNeracaLajur
    End Sub

    Sub Inisialisasi()
        usc_LaporanNeracaLajur = New wpfUsc_LaporanNeracaLajur
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanNeracaLajur.RefreshTampilanData()
    End Sub

End Class
