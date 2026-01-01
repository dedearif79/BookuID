Imports System.Windows.Forms.Integration

Public Class frm_BukuPenjualanEceran

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Penjualan Eceran"
        Me.Text = JudulForm

        usc_BukuPenjualanEceran = New wpfUsc_BukuPenjualanEceran
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPenjualanEceran
        Me.Controls.Add(host)

    End Sub

End Class