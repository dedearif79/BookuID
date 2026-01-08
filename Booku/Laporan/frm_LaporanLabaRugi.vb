Imports System.Windows.Forms.Integration

Public Class frm_LaporanLabaRugi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Laporan Rugi-Laba"
        Me.Text = JudulForm

        usc_LaporanLabaRugi = New wpfUsc_LaporanLabaRugi
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanLabaRugi
        Me.Controls.Add(host)

    End Sub

End Class