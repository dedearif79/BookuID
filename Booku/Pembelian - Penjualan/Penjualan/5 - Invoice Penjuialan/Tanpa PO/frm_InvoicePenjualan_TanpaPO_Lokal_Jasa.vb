Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_TanpaPO_Lokal_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Tanpa PO - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_TanpaPO_Lokal_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.JudulForm = JudulForm
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.InvoiceDenganPO = False
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.JenisProduk_Menu = JenisProduk_Jasa
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class