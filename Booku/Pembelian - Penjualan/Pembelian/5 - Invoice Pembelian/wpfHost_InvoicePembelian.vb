Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_InvoicePembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' =============================================================================
' DENGAN PO
' =============================================================================

' ---------------------------------------------------------------------
' VARIAN 1: Dengan PO - Lokal - Rutin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_DenganPO_Lokal_Rutin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian - Rutin"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_DenganPO_Lokal_Rutin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Lokal_Rutin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_DenganPO_Lokal_Rutin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Dengan PO - Lokal - Termin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_DenganPO_Lokal_Termin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian - Termin"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_DenganPO_Lokal_Termin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Lokal_Termin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_DenganPO_Lokal_Termin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Dengan PO - Impor - Rutin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_DenganPO_Impor_Rutin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian - Impor - Rutin"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_DenganPO_Impor_Rutin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Rutin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Rutin.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Dengan PO - Impor - Termin
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_DenganPO_Impor_Termin
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Impor - Termin"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_DenganPO_Impor_Termin
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Termin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Termin.RefreshTampilanData()
    End Sub

End Class


' =============================================================================
' TANPA PO
' =============================================================================

' ---------------------------------------------------------------------
' VARIAN 5: Tanpa PO - Lokal - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Lokal_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Barang"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Lokal_Barang
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Tanpa PO - Lokal - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Lokal_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Jasa"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Lokal_Jasa
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 7: Tanpa PO - Lokal - Barang dan Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Barang dan Jasa"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_BarangDanJasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 8: Tanpa PO - Lokal - Jasa Konstruksi
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Jasa Konstruksi"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_JasaKonstruksi,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 9: Tanpa PO - Impor - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Impor_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Impor - Barang"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Impor_Barang
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Impor_Barang = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Impor_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 10: Tanpa PO - Impor - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_InvoicePembelian_TanpaPO_Impor_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Invoice Pembelian Tanpa PO - Impor - Jasa"
        Inisialisasi()
        Me.Content = usc_InvoicePembelian_TanpaPO_Impor_Jasa
    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Impor_Jasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.RefreshTampilanData()
    End Sub

End Class
