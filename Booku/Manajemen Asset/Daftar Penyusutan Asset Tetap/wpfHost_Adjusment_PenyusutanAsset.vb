Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_Adjusment_PenyusutanAsset
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Adjusment Penyusutan Asset (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_AdjusmentPenyusutanAsset
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Adjusment Penyusutan Asset"
        Inisialisasi()
        Me.Content = usc_Adjusment_PenyusutanAsset
    End Sub

    Sub Inisialisasi()
        usc_Adjusment_PenyusutanAsset = New wpfUsc_Adjusment_PenyusutanAsset
    End Sub

    Sub CekAdjusment()
        Inisialisasi()
        usc_Adjusment_PenyusutanAsset.RefreshTampilanData()
    End Sub

End Class
