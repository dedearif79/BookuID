Imports System.Windows
Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_POPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Lokal - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Lokal_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian - Barang"
        Inisialisasi()
        Me.Content = usc_POPembelian_Lokal_Barang
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Barang = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Lokal_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Lokal_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Lokal - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Lokal_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian - Jasa"
        Inisialisasi()
        Me.Content = usc_POPembelian_Lokal_Jasa
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Jasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Lokal_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Lokal_Jasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Lokal - Barang dan Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Lokal_BarangDanJasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian - Barang dan Jasa"
        Inisialisasi()
        Me.Content = usc_POPembelian_Lokal_BarangDanJasa
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_BarangDanJasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPembelian_Lokal_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Lokal_BarangDanJasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Lokal - Jasa Konstruksi
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Lokal_JasaKonstruksi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian - Jasa Konstruksi"
        Inisialisasi()
        Me.Content = usc_POPembelian_Lokal_JasaKonstruksi
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_JasaKonstruksi = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPembelian_Lokal_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Lokal_JasaKonstruksi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 5: Lokal - Semua
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Lokal_Semua
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian"
        Inisialisasi()
        Me.Content = usc_POPembelian_Lokal_Semua
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Semua = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Lokal_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Lokal_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Lokal_Semua.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Impor - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Impor_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian Impor - Barang"
        Inisialisasi()
        Me.Content = usc_POPembelian_Impor_Barang
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Barang = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Impor_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Impor_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 7: Impor - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Impor_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian Impor - Jasa"
        Inisialisasi()
        Me.Content = usc_POPembelian_Impor_Jasa
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Jasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Impor_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Impor_Jasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 8: Impor - Semua
' ---------------------------------------------------------------------
Public Class wpfHost_POPembelian_Impor_Semua
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Pembelian Impor"
        Inisialisasi()
        Me.Content = usc_POPembelian_Impor_Semua
    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Semua = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Impor_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Impor_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPembelian_Impor_Semua.RefreshTampilanData()
    End Sub

End Class
