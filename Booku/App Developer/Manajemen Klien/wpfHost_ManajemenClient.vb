Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_ManajemenClient
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Manajemen Client (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_ManajemenClient
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Manajemen Client"
        Inisialisasi()
        Me.Content = usc_ManajemenClient
    End Sub

    Sub Inisialisasi()
        usc_ManajemenClient = New wpfUsc_ManajemenClient
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_ManajemenClient.RefreshTampilanData()
    End Sub

End Class
