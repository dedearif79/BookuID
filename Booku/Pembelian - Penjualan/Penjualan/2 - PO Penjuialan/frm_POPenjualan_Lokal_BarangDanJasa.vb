Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Lokal_BarangDanJasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan - Barang dan Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_BarangDanJasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_BarangDanJasa = New wpfUsc_POPenjualan
        usc_POPenjualan_BarangDanJasa.JudulForm = JudulForm
        usc_POPenjualan_BarangDanJasa.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_POPenjualan_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPenjualan_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
