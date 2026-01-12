Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_LaporanHPP
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Laporan HPP (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_LaporanHPP
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Laporan HPP"
        Inisialisasi()
        Me.Content = usc_LaporanHPP
    End Sub

    Sub Inisialisasi()
        usc_LaporanHPP = New wpfUsc_LaporanHPP
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_LaporanHPP.RefreshTampilanData()
    End Sub

End Class
