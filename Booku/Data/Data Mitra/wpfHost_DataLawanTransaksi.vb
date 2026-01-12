Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataLawanTransaksi
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data Lawan Transaksi / Mitra (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataLawanTransaksi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data Mitra/Lawan Transaksi"
        Inisialisasi()
        Me.Content = usc_DataLawanTransaksi
    End Sub

    Sub Inisialisasi()
        usc_DataLawanTransaksi = New wpfUsc_DataLawanTransaksi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataLawanTransaksi.RefreshTampilanData()
    End Sub

End Class
