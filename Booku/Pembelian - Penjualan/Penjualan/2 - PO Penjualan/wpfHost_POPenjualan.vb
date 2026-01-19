Imports System.Windows
Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_POPenjualan
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Lokal - Barang
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Lokal_Barang
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan - Barang"
        Inisialisasi()
        Me.Content = usc_POPenjualan_Barang
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Barang = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_Barang.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Lokal - Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Lokal_Jasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan - Jasa"
        Inisialisasi()
        Me.Content = usc_POPenjualan_Jasa
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Jasa = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPenjualan_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_Jasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Lokal - Barang dan Jasa
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Lokal_BarangDanJasa
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan - Barang dan Jasa"
        Inisialisasi()
        Me.Content = usc_POPenjualan_BarangDanJasa
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_BarangDanJasa = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPenjualan_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_BarangDanJasa.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Lokal - Jasa Konstruksi
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Lokal_JasaKonstruksi
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan - Jasa Konstruksi"
        Inisialisasi()
        Me.Content = usc_POPenjualan_JasaKonstruksi
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_JasaKonstruksi = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPenjualan_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_JasaKonstruksi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 5: Lokal - Semua
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Lokal_Semua
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan"
        Inisialisasi()
        Me.Content = usc_POPenjualan_Semua
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Semua = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPenjualan_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPenjualan_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_Semua.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Ekspor
' ---------------------------------------------------------------------
Public Class wpfHost_POPenjualan_Ekspor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "PO Penjualan - Ekspor"
        Inisialisasi()
        Me.Content = usc_POPenjualan_Ekspor
    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Ekspor = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor
        }
        usc_POPenjualan_Ekspor.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Ekspor.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Ekspor.VisibilitasFilterJenisProdukInduk(False)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_POPenjualan_Ekspor.RefreshTampilanData()
    End Sub

End Class
