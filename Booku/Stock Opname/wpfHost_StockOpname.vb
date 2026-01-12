Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk Stock Opname
' Semua menggunakan wpfUsc_StockOpname dengan JenisStok_Menu berbeda
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' Stock Opname - Bahan Penolong
' ---------------------------------------------------------------------
Public Class wpfHost_StockOpname_BahanPenolong
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Stock Opname Bahan Penolong"
        Inisialisasi()
        Me.Content = usc_BahanPenolong
    End Sub

    Sub Inisialisasi()
        usc_BahanPenolong = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BahanPenolong
        }
        usc_BahanPenolong.JenisPengecekan_Menu = usc_BahanPenolong.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BahanPenolong.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' Stock Opname - Bahan Baku
' ---------------------------------------------------------------------
Public Class wpfHost_StockOpname_BahanBaku
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Stock Opname Bahan Baku"
        Inisialisasi()
        Me.Content = usc_BahanBaku
    End Sub

    Sub Inisialisasi()
        usc_BahanBaku = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BahanBaku
        }
        usc_BahanBaku.JenisPengecekan_Menu = usc_BahanBaku.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BahanBaku.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' Stock Opname - Barang Jadi
' ---------------------------------------------------------------------
Public Class wpfHost_StockOpname_BarangJadi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Stock Opname Barang Jadi"
        Inisialisasi()
        Me.Content = usc_BarangJadi
    End Sub

    Sub Inisialisasi()
        usc_BarangJadi = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangJadi
        }
        usc_BarangJadi.JenisPengecekan_Menu = usc_BarangJadi.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangJadi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' Stock Opname - Barang Dalam Proses (Cek Fisik)
' ---------------------------------------------------------------------
Public Class wpfHost_StockOpname_BarangDalamProses_CekFisik
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Stock Opname Barang Dalam Proses (Cek Fisik)"
        Inisialisasi()
        Me.Content = usc_BarangDalamProses_CekFisik
    End Sub

    Sub Inisialisasi()
        usc_BarangDalamProses_CekFisik = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangDalamProses
        }
        usc_BarangDalamProses_CekFisik.JenisPengecekan_Menu = usc_BarangDalamProses_CekFisik.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangDalamProses_CekFisik.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' Stock Opname - Barang Dalam Proses (Tarikan Data)
' ---------------------------------------------------------------------
Public Class wpfHost_StockOpname_BarangDalamProses_TarikanData
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Stock Opname Barang Dalam Proses (Tarikan Data)"
        Inisialisasi()
        Me.Content = usc_BarangDalamProses_TarikanData
    End Sub

    Sub Inisialisasi()
        usc_BarangDalamProses_TarikanData = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangDalamProses
        }
        usc_BarangDalamProses_TarikanData.JenisPengecekan_Menu = usc_BarangDalamProses_TarikanData.JenisPengecekan_TarikanData
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangDalamProses_TarikanData.RefreshTampilanData()
    End Sub

End Class
