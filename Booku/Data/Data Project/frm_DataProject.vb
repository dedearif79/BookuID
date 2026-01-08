Imports System.Windows.Forms.Integration

Public Class frm_DataProject

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Data Project"
        Me.Text = JudulForm

        usc_DataProject = New wpfUsc_DataProject
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataProject
        Me.Controls.Add(host)

    End Sub

End Class