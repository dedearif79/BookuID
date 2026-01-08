Imports System.Windows.Forms.Integration

Public Class frm_DaftarPemegangSaham

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Daftar Pemegang Saham"
        Me.Text = JudulForm

        usc_DaftarPemegangSaham = New wpfUsc_DaftarPemegangSaham
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DaftarPemegangSaham
        Me.Controls.Add(host)

    End Sub

End Class