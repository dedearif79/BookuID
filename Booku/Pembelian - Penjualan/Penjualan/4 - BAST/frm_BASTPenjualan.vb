Imports System.Windows.Forms.Integration

Public Class frm_BASTPenjualan


    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Berita Serah Terima Acara (BAST) Penjualan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BASTPenjualan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BASTPenjualan = New wpfUsc_BASTPenjualan
        usc_BASTPenjualan.JudulForm = JudulForm
    End Sub

End Class
