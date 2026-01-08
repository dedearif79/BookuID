Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_DenganPO_Ekspor_Termin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Ekspor - Termin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_DenganPO_Ekspor_Termin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.JudulForm = JudulForm
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.InvoiceDenganPO = True
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.JenisProduk_Menu = JenisProduk_Barang
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin.MetodePembayaran = MetodePembayaran_Termin
    End Sub

End Class
