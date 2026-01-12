Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk modul Tahun Buku
' =====================================================================


' ---------------------------------------------------------------------
' Tutup Buku
' ---------------------------------------------------------------------
Public Class wpfHost_TutupBuku
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Tutup Buku"
        Inisialisasi()
        Me.Content = usc_TutupBuku
    End Sub

    Sub Inisialisasi()
        usc_TutupBuku = New wpfUsc_TutupBuku
        usc_TutupBuku.ResetForm()
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        ' wpfUsc_TutupBuku tidak memiliki RefreshTampilanData
    End Sub

End Class
