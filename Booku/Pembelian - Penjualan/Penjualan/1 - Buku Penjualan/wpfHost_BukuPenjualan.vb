Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Lokal
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPenjualan_Lokal
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Penjualan"
        Inisialisasi()
        Me.Content = usc_BukuPenjualan_Lokal
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Lokal = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_BukuPenjualan_Lokal.JenisPenjualan = usc_BukuPenjualan_Lokal.JenisPenjualan_Rutin
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualan_Lokal.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Ekspor
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPenjualan_Ekspor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Penjualan - Ekspor"
        Inisialisasi()
        Me.Content = usc_BukuPenjualan_Ekspor
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Ekspor = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor
        }
        usc_BukuPenjualan_Ekspor.JenisPenjualan = usc_BukuPenjualan_Ekspor.JenisPenjualan_Rutin
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualan_Ekspor.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Asset
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPenjualan_Asset
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Penjualan - Asset"
        Inisialisasi()
        Me.Content = usc_BukuPenjualan_Asset
    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Asset = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_BukuPenjualan_Asset.JenisPenjualan = usc_BukuPenjualan_Asset.JenisPenjualan_Asset
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPenjualan_Asset.RefreshTampilanData()
    End Sub

End Class
