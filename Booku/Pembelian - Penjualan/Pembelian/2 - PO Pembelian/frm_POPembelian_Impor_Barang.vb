Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Impor_Barang

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian Impor - Barang"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Impor_Barang
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Barang = New wpfUsc_POPembelian
        usc_POPembelian_Impor_Barang.JudulForm = JudulForm
        usc_POPembelian_Impor_Barang.AsalPembelian = AsalPembelian_Impor
        usc_POPembelian_Impor_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Impor_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
