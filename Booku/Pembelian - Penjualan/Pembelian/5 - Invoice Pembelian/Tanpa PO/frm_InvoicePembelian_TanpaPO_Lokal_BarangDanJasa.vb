Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Tanpa PO - Barang dan Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.JudulForm = JudulForm
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.AsalPembelian = AsalPembelian_Lokal
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.InvoiceDenganPO = False
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.JenisProduk_Menu = JenisProduk_BarangDanJasa
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class