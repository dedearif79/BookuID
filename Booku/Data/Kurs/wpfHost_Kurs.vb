Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_Kurs
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Kurs (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataKurs
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Kurs"
        Inisialisasi()
        Me.Content = usc_Kurs
    End Sub

    Sub Inisialisasi()
        usc_Kurs = New wpfUsc_Kurs
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_Kurs.RefreshTampilanData()
    End Sub

End Class
