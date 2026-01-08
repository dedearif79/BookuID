Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_DataPerangkatApp

    Public JudulForm

    Private Sub frm_DataPerangkatApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Style_HalamanModul(Me)

        JudulForm = "Data Perangkat Aplikasi"
        Me.Text = JudulForm

        usc_DataPerangkatApp = New wpfUsc_DataPerangkatApp
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataPerangkatApp
        Me.Controls.Add(host)

    End Sub

End Class
