Imports System.Windows.Forms.Integration

Public Class frm_Loading

    Private Sub frm_Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        usc_Loading = New wpfUsc_Loading
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_Loading
        Me.Controls.Add(host)

    End Sub


End Class