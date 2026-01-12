Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataKaryawan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Karyawan (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataKaryawan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Karyawan"
        Inisialisasi()
        Me.Content = usc_DataKaryawan
    End Sub

    Sub Inisialisasi()
        usc_DataKaryawan = New wpfUsc_DataKaryawan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataKaryawan.RefreshTampilanData()
    End Sub

End Class
