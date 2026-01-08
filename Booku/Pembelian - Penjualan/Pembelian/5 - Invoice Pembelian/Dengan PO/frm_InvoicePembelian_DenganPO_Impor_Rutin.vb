Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_DenganPO_Impor_Rutin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian - Impor - Rutin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_DenganPO_Impor_Rutin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Rutin = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_DenganPO_Impor_Rutin.JudulForm = JudulForm
        usc_InvoicePembelian_DenganPO_Impor_Rutin.AsalPembelian = AsalPembelian_Impor
        usc_InvoicePembelian_DenganPO_Impor_Rutin.InvoiceDenganPO = True
        usc_InvoicePembelian_DenganPO_Impor_Rutin.JenisProduk_Menu = JenisProduk_Semua
        usc_InvoicePembelian_DenganPO_Impor_Rutin.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class