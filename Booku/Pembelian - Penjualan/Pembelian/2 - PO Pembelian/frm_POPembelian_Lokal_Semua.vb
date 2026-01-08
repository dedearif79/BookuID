Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Lokal_Semua

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Lokal_Semua
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Semua = New wpfUsc_POPembelian
        usc_POPembelian_Lokal_Semua.JudulForm = JudulForm
        usc_POPembelian_Lokal_Semua.AsalPembelian = AsalPembelian_Lokal
        usc_POPembelian_Lokal_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Lokal_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Lokal_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

End Class
