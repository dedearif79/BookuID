Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataUser
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data User (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataUser
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data User"
        Inisialisasi()
        Me.Content = usc_DataUser
    End Sub

    Sub Inisialisasi()
        usc_DataUser = New wpfUsc_DataUser
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataUser.RefreshTampilanData()
    End Sub

End Class
