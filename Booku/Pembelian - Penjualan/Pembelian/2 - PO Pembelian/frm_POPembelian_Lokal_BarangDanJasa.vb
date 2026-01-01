Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Lokal_BarangDanJasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian - Barang dan Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Lokal_BarangDanJasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_BarangDanJasa = New wpfUsc_POPembelian
        usc_POPembelian_Lokal_BarangDanJasa.JudulForm = JudulForm
        usc_POPembelian_Lokal_BarangDanJasa.AsalPembelian = AsalPembelian_Lokal
        usc_POPembelian_Lokal_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPembelian_Lokal_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
