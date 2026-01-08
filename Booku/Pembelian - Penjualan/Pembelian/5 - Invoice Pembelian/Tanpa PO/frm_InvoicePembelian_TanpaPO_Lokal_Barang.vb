Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_TanpaPO_Lokal_Barang

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Tanpa PO - Barang"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_TanpaPO_Lokal_Barang
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.JudulForm = JudulForm
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.AsalPembelian = AsalPembelian_Lokal
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.InvoiceDenganPO = False
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.JenisProduk_Menu = JenisProduk_Barang
        usc_InvoicePembelian_TanpaPO_Lokal_Barang.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class