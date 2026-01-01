Imports System.Windows.Forms.Integration

Public Class frm_LaporanNeraca_Bulanan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Neraca Bulanan"
        Me.Text = JudulForm

        usc_LaporanNeraca_Bulanan = New wpfUsc_LaporanNeraca_Bulanan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanNeraca_Bulanan
        Me.Controls.Add(host)

    End Sub

End Class