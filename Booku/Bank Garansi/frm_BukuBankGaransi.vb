Imports System.Windows.Forms.Integration

Public Class frm_BukuBankGaransi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Bank Garansi"
        Me.Text = JudulForm

        usc_BukuBankGaransi = New wpfUsc_BukuBankGaransi
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuBankGaransi
        Me.Controls.Add(host)

    End Sub

End Class
