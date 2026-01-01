Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_DenganPO_Impor_Termin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Impor - Termin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_DenganPO_Impor_Termin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Impor_Termin = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_DenganPO_Impor_Termin.JudulForm = JudulForm
        usc_InvoicePembelian_DenganPO_Impor_Termin.AsalPembelian = AsalPembelian_Impor
        usc_InvoicePembelian_DenganPO_Impor_Termin.InvoiceDenganPO = True
        usc_InvoicePembelian_DenganPO_Impor_Termin.JenisProduk_Menu = JenisProduk_Semua
        usc_InvoicePembelian_DenganPO_Impor_Termin.MetodePembayaran = MetodePembayaran_Termin
    End Sub

End Class