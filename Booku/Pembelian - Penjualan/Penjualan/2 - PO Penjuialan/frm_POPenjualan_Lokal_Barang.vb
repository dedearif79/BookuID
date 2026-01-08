Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Lokal_Barang

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan - Barang"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_Barang
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Barang = New wpfUsc_POPenjualan
        usc_POPenjualan_Barang.JudulForm = JudulForm
        usc_POPenjualan_Barang.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_POPenjualan_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
