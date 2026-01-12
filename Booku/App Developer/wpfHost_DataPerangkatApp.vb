Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataPerangkatApp
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Perangkat App (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataPerangkatApp
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Perangkat Aplikasi"
        Inisialisasi()
        Me.Content = usc_DataPerangkatApp
    End Sub

    Sub Inisialisasi()
        usc_DataPerangkatApp = New wpfUsc_DataPerangkatApp
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataPerangkatApp.RefreshTampilanData()
    End Sub

End Class
