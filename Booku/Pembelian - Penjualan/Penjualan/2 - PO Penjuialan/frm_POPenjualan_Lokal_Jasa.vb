Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Lokal_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Jasa = New wpfUsc_POPenjualan
        usc_POPenjualan_Jasa.JudulForm = JudulForm
        usc_POPenjualan_Jasa.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_POPenjualan_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPenjualan_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
