Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_TanpaPO_Ekspor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan Tanpa PO - Ekspor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_TanpaPO_Ekspor
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Ekspor = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_TanpaPO_Ekspor.JudulForm = JudulForm
        usc_InvoicePenjualan_TanpaPO_Ekspor.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_InvoicePenjualan_TanpaPO_Ekspor.InvoiceDenganPO = False
        usc_InvoicePenjualan_TanpaPO_Ekspor.JenisProduk_Menu = JenisProduk_Barang
        usc_InvoicePenjualan_TanpaPO_Ekspor.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class