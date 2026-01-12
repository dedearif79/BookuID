Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_Adjusment_Amortisasi
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Adjusment Amortisasi (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_AdjusmentAmortisasi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Adjusment Amortisasi"
        Inisialisasi()
        Me.Content = usc_Adjusment_Amortisasi
    End Sub

    Sub Inisialisasi()
        usc_Adjusment_Amortisasi = New wpfUsc_Adjusment_Amortisasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_Adjusment_Amortisasi.RefreshTampilanData()
    End Sub

End Class
