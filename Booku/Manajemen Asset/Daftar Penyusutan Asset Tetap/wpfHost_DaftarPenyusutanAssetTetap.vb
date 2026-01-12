Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DaftarPenyusutanAssetTetap
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Daftar Penyusutan Asset Tetap (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DaftarPenyusutanAssetTetap
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Daftar Penyusutan Asset Tetap"
        Inisialisasi()
        Me.Content = usc_DaftarPenyusutanAssetTetap
    End Sub

    Sub Inisialisasi()
        usc_DaftarPenyusutanAssetTetap = New wpfUsc_DaftarPenyusutanAssetTetap
        usc_DaftarPenyusutanAssetTetap.JalurMasuk = Halaman_MENUUTAMA
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DaftarPenyusutanAssetTetap.RefreshTampilanData()
    End Sub

End Class
