Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Ekspor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan - Ekspor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_Ekspor
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Ekspor = New wpfUsc_POPenjualan
        usc_POPenjualan_Ekspor.JudulForm = JudulForm
        usc_POPenjualan_Ekspor.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_POPenjualan_Ekspor.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Ekspor.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Ekspor.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
