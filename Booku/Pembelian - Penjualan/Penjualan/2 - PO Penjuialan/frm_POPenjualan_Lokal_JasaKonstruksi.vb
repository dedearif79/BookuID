Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Lokal_JasaKonstruksi


    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan - Jasa Konstruksi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_JasaKonstruksi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_JasaKonstruksi = New wpfUsc_POPenjualan
        usc_POPenjualan_JasaKonstruksi.JudulForm = JudulForm
        usc_POPenjualan_JasaKonstruksi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_POPenjualan_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPenjualan_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
