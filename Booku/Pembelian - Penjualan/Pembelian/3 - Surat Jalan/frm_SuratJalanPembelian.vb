Imports System.Windows.Forms.Integration

Public Class frm_SuratJalanPembelian

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Surat Jalan Pembelian"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_SuratJalanPembelian
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_SuratJalanPembelian = New wpfUsc_SuratJalanPembelian
        usc_SuratJalanPembelian.JudulForm = JudulForm
    End Sub

End Class
