Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuDisposalAssetTetap
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Buku Disposal Asset Tetap (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuDisposalAssetTetap
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Disposal Asset Tetap"
        Inisialisasi()
        Me.Content = usc_BukuDisposalAssetTetap
    End Sub

    Sub Inisialisasi()
        usc_BukuDisposalAssetTetap = New wpfUsc_BukuDisposalAssetTetap
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuDisposalAssetTetap.RefreshTampilanData()
    End Sub

End Class
