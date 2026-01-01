Imports System.Windows.Forms.Integration

Public Class frm_LaporanNeraca_Tahunan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Neraca Tahunan"
        Me.Text = JudulForm

        usc_LaporanNeraca_Tahunan = New wpfUsc_LaporanNeraca_Tahunan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanNeraca_Tahunan
        Me.Controls.Add(host)

    End Sub

End Class