Imports System.Windows.Forms.Integration

Public Class frm_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Invoice Penjualan - Tanpa PO - Jasa Konstruksi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePenjualan
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.JudulForm = JudulForm
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.InvoiceDenganPO = False
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.JenisProduk_Menu = JenisProduk_JasaKonstruksi
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.MetodePembayaran = MetodePembayaran_Normal
    End Sub

End Class