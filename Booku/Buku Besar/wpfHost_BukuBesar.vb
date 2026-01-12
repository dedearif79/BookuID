Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuBesar (modul Buku Besar umum)
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' Buku Besar (Umum)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuBesar
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Besar"
        Inisialisasi()
        Me.Content = usc_BukuBesar
    End Sub

    Sub Inisialisasi()
        usc_BukuBesar = New wpfUsc_BukuBesar
        usc_BukuBesar.FungsiModul = Halaman_BUKUBESAR
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuBesar.RefreshTampilanData()
    End Sub

End Class
