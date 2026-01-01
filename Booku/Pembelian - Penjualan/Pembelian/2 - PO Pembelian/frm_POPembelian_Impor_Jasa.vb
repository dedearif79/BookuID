Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Impor_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian Impor - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Impor_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Impor_Jasa = New wpfUsc_POPembelian
        usc_POPembelian_Impor_Jasa.JudulForm = JudulForm
        usc_POPembelian_Impor_Jasa.AsalPembelian = AsalPembelian_Impor
        usc_POPembelian_Impor_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Impor_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
