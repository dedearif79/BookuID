Imports System.Windows.Forms.Integration

Public Class frm_BASTPembelian


    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Berita Serah Terima Acara (BAST) Pembelian"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BASTPembelian
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BASTPembelian = New wpfUsc_BASTPembelian
        usc_BASTPembelian.JudulForm = JudulForm
    End Sub

End Class
