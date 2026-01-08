Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_TanpaPO_Lokal_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Tanpa PO - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_TanpaPO_Lokal_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.JudulForm = JudulForm
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.AsalPembelian = AsalPembelian_Lokal
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.InvoiceDenganPO = False
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.JenisProduk_Menu = JenisProduk_Jasa
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class