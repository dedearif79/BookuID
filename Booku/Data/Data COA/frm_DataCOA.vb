Imports System.Windows.Forms.Integration

Public Class frm_DataCOA

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Data COA"
        Me.Text = JudulForm

        usc_DataCOA = New wpfUsc_DataCOA
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataCOA
        Me.Controls.Add(host)

    End Sub

End Class