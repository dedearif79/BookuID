Imports System.Windows.Forms.Integration

Public Class frm_LaporanNeraca

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Neraca"
        Me.Text = JudulForm

        usc_LaporanNeraca = New wpfUsc_LaporanNeraca
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanNeraca
        Me.Controls.Add(host)

    End Sub

End Class