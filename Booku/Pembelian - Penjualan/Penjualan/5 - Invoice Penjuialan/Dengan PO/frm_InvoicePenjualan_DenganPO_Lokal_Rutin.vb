Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_DenganPO_Lokal_Rutin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Rutin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_DenganPO_Lokal_Rutin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.JudulForm = JudulForm
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.InvoiceDenganPO = True
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.JenisProduk_Menu = JenisProduk_Semua
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class