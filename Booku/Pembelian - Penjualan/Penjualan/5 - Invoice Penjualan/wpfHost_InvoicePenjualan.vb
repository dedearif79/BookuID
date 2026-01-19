Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_InvoicePenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' =============================================================================
' DENGAN PO
' =============================================================================

' ---------------------------------------------------------------------
' VARIAN 1: Dengan PO - Lokal - Rutin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_DenganPO_Lokal_Rutin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Rutin"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_DenganPO_Lokal_Rutin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Dengan PO - Lokal - Termin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_DenganPO_Lokal_Termin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Termin"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_DenganPO_Lokal_Termin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Lokal_Termin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Lokal_Termin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Dengan PO - Ekspor - Rutin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_DenganPO_Ekspor_Rutin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan Ekspor - Rutin"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_DenganPO_Ekspor_Rutin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Dengan PO - Ekspor - Termin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_DenganPO_Ekspor_Termin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan Ekspor - Termin"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_DenganPO_Ekspor_Termin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.RefreshTampilanData()
    End Sub

End Class


' =============================================================================
' TANPA PO
' =============================================================================

' ---------------------------------------------------------------------
' VARIAN 5: Tanpa PO - Lokal - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_TanpaPO_Lokal_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Tanpa PO - Barang"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_TanpaPO_Lokal_Barang
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Tanpa PO - Lokal - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_TanpaPO_Lokal_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Tanpa PO - Jasa"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_TanpaPO_Lokal_Jasa
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 7: Tanpa PO - Lokal - Barang dan Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Tanpa PO - Barang dan Jasa"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_BarangDanJasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 8: Tanpa PO - Lokal - Jasa Konstruksi
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan - Tanpa PO - Jasa Konstruksi"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_JasaKonstruksi,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 9: Tanpa PO - Ekspor
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePenjualan_TanpaPO_Ekspor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Penjualan Tanpa PO - Ekspor"
        Inisialisasi()
        Me.Content = usc_InvoicePenjualan_TanpaPO_Ekspor
    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Ekspor = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Ekspor.RefreshTampilanData()
    End Sub

End Class
