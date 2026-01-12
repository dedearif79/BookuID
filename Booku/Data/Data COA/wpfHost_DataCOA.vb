Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataCOA
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data COA (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataCOA
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data COA"
        Inisialisasi()
        Me.Content = usc_DataCOA
    End Sub

    Sub Inisialisasi()
        usc_DataCOA = New wpfUsc_DataCOA
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataCOA.RefreshTampilanData()
    End Sub

End Class
