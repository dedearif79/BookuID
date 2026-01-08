Imports System.Windows.Forms.Integration

Public Class frm_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Pembelian Tanpa PO - Jasa Konstruksi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePembelian
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.JudulForm = JudulForm
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.AsalPembelian = AsalPembelian_Lokal
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.InvoiceDenganPO = False
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.JenisProduk_Menu = JenisProduk_JasaKonstruksi
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class