Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_POPembelian_Lokal_JasaKonstruksi


    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "PO Pembelian - Jasa Konstruksi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_POPembelian_Lokal_JasaKonstruksi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_POPembelian_Lokal_JasaKonstruksi = New wpfUsc_POPembelian
        usc_POPembelian_Lokal_JasaKonstruksi.JudulForm = JudulForm
        usc_POPembelian_Lokal_JasaKonstruksi.AsalPembelian = AsalPembelian_Lokal
        usc_POPembelian_Lokal_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPembelian_Lokal_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
    End Sub

End Class
