Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataProject
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Project (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataProject
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Project"
        Inisialisasi()
        Me.Content = usc_DataProject
    End Sub

    Sub Inisialisasi()
        usc_DataProject = New wpfUsc_DataProject
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataProject.RefreshTampilanData()
    End Sub

End Class
