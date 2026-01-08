Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_ManajemenAplikasi

    Public JudulForm

    Private Sub frm_ManajemenUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Manajemen Aplikasi"
        Me.Text = JudulForm

        usc_ManajemenAplikasi = New wpfUsc_ManajemenAplikasi
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_ManajemenAplikasi
        Me.Controls.Add(host)

    End Sub

End Class