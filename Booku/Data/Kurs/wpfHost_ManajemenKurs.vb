Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_ManajemenKurs
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Manajemen Kurs (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_ManajemenKurs
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Manajemen Kurs"
        Inisialisasi()
        Me.Content = usc_ManajemenKurs
    End Sub

    Sub Inisialisasi()
        usc_ManajemenKurs = New wpfUsc_ManajemenKurs
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        ' usc_ManajemenKurs tidak memiliki RefreshTampilanData
    End Sub

End Class
