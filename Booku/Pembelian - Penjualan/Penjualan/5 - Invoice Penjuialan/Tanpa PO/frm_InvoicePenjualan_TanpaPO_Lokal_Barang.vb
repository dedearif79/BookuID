Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_TanpaPO_Lokal_Barang

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Tanpa PO - Barang"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_TanpaPO_Lokal_Barang
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.JudulForm = JudulForm
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.InvoiceDenganPO = False
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.JenisProduk_Menu = JenisProduk_Barang
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class