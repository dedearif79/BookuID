Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_TanpaPO_Impor_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Tanpa PO - Impor - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_TanpaPO_Impor_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Impor_Jasa = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.JudulForm = JudulForm
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.AsalPembelian = AsalPembelian_Impor
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.InvoiceDenganPO = False
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.JenisProduk_Menu = JenisProduk_Jasa
        usc_InvoicePembelian_TanpaPO_Impor_Jasa.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class