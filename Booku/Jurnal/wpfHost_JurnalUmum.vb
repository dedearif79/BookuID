Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_JurnalUmum
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Jurnal Umum (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_JurnalUmum
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Jurnal Umum"
        Inisialisasi()
        Me.Content = usc_JurnalUmum
    End Sub

    Sub Inisialisasi()
        usc_JurnalUmum = New wpfUsc_JurnalUmum
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_JurnalUmum.RefreshTampilanData()
    End Sub

End Class
