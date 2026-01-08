Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_DenganPO_Ekspor_Rutin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Ekspor - Rutin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_DenganPO_Ekspor_Rutin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.JudulForm = JudulForm
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.InvoiceDenganPO = True
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.JenisProduk_Menu = JenisProduk_Barang
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class