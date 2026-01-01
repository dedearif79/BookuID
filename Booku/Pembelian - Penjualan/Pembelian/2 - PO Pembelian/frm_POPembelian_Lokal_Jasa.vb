Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Lokal_Jasa

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian - Jasa"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Lokal_Jasa
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_Jasa = New wpfUsc_POPembelian
        usc_POPembelian_Lokal_Jasa.JudulForm = JudulForm
        usc_POPembelian_Lokal_Jasa.AsalPembelian = AsalPembelian_Lokal
        usc_POPembelian_Lokal_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Lokal_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Jasa.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
