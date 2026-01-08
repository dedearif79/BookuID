Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPenjualan_Lokal_Semua

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Penjualan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPenjualan_Semua
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPenjualan_Semua = New wpfUsc_POPenjualan
        usc_POPenjualan_Semua.JudulForm = JudulForm
        usc_POPenjualan_Semua.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_POPenjualan_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPenjualan_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPenjualan_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

End Class
