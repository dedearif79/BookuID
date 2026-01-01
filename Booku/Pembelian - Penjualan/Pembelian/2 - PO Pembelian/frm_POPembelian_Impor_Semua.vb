Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Impor_Semua

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian Impor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Impor_Semua
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Semua = New wpfUsc_POPembelian
        usc_POPembelian_Impor_Semua.JudulForm = JudulForm
        usc_POPembelian_Impor_Semua.AsalPembelian = AsalPembelian_Impor
        usc_POPembelian_Impor_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Impor_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Impor_Semua.VisibilitasFilterJenisProdukInduk(True)
    End Sub

End Class
