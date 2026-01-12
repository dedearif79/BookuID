Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_Adjusment_Forex (wpfUsc_JurnalAdjusment_Forex)
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Adjusment Forex (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_AdjusmentForex
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Adjusment Forex"
        Inisialisasi()
        Me.Content = usc_Adjusment_Forex
    End Sub

    Sub Inisialisasi()
        usc_Adjusment_Forex = New wpfUsc_JurnalAdjusment_Forex
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_Adjusment_Forex.RefreshTampilanData()
    End Sub

End Class
