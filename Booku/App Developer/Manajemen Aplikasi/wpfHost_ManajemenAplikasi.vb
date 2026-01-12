Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_ManajemenAplikasi
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Manajemen Aplikasi (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_ManajemenAplikasi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Manajemen Aplikasi"
        Inisialisasi()
        Me.Content = usc_ManajemenAplikasi
    End Sub

    Sub Inisialisasi()
        usc_ManajemenAplikasi = New wpfUsc_ManajemenAplikasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_ManajemenAplikasi.RefreshTampilanData()
    End Sub

End Class
