Imports System.Windows.Forms.Integration

Public Class frm_JurnalUmum

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Jurnal Umum"
        Me.Text = JudulForm

        usc_JurnalUmum = New wpfUsc_JurnalUmum
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_JurnalUmum
        Me.Controls.Add(host)

    End Sub

End Class