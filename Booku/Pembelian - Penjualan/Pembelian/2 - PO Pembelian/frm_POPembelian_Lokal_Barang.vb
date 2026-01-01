Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Lokal_Barang

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian - Barang"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Lokal_Barang
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Barang = New wpfUsc_POPembelian
        usc_POPembelian_Lokal_Barang.JudulForm = JudulForm
        usc_POPembelian_Lokal_Barang.AsalPembelian = AsalPembelian_Lokal
        usc_POPembelian_Lokal_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Lokal_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Barang.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
