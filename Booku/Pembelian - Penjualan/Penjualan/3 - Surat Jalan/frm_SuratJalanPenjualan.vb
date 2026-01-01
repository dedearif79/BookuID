Imports System.Windows.Forms.Integration

Public Class frm_SuratJalanPenjualan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Surat Jalan Penjualan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_SuratJalanPenjualan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_SuratJalanPenjualan = New wpfUsc_SuratJalanPenjualan
        usc_SuratJalanPenjualan.JudulForm = JudulForm
    End Sub

End Class
