Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_DenganPO_Lokal_Termin

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian - Termin"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_DenganPO_Lokal_Termin
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_DenganPO_Lokal_Termin = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_DenganPO_Lokal_Termin.JudulForm = JudulForm
        usc_InvoicePembelian_DenganPO_Lokal_Termin.AsalPembelian = AsalPembelian_Lokal
        usc_InvoicePembelian_DenganPO_Lokal_Termin.InvoiceDenganPO = True
        usc_InvoicePembelian_DenganPO_Lokal_Termin.JenisProduk_Menu = JenisProduk_Semua
        usc_InvoicePembelian_DenganPO_Lokal_Termin.MetodePembayaran = MetodePembayaran_Termin
    End Sub

End Class