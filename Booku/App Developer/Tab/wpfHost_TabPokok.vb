Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_TabPokok
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Tab Pokok (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_TabPokok
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Tab Pokok"
        Inisialisasi()
        Me.Content = usc_TabPokok
    End Sub

    Sub Inisialisasi()
        usc_TabPokok = New wpfUsc_TabPokok
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        ' usc_TabPokok tidak memiliki RefreshTampilanData
    End Sub

End Class
