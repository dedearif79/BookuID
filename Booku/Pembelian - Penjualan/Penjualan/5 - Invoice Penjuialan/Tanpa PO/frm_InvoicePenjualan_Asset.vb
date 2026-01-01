Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_Asset

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Asset"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_Asset
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_Asset = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_Asset.JudulForm = JudulForm
        usc_InvoicePenjualan_Asset.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_InvoicePenjualan_Asset.InvoiceDenganPO = True
        usc_InvoicePenjualan_Asset.JenisProduk_Menu = JenisProduk_Semua
        usc_InvoicePenjualan_Asset.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class