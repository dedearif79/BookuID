Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_ManajemenClient

    Public JudulForm

    Private Sub frm_ManajemenUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Style_HalamanModul(Me)

        JudulForm = "Manajemen Klien"
        Me.Text = JudulForm

        usc_ManajemenClient = New wpfUsc_ManajemenClient
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_ManajemenClient
        Me.Controls.Add(host)

    End Sub

End Class