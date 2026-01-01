Imports System.Windows.Forms.Integration

Public Class frm_Kurs

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Kurs Tengah - Bank Indonesia"
        Me.Text = JudulForm

        usc_Kurs = New wpfUsc_Kurs
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_Kurs
        Me.Controls.Add(host)

    End Sub

End Class