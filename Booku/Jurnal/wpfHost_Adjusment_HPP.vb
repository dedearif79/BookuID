Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_Adjusment_HPP (wpfUsc_JurnalAdjusment_HPP)
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Adjusment HPP (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_AdjusmentHPP
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Adjusment HPP"
        Inisialisasi()
        Me.Content = usc_JurnalAdjusment_HPP
    End Sub

    Sub Inisialisasi()
        usc_JurnalAdjusment_HPP = New wpfUsc_JurnalAdjusment_HPP
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_JurnalAdjusment_HPP.RefreshTampilanData()
    End Sub

End Class
