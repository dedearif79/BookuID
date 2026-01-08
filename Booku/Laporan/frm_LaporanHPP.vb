Imports System.Windows.Forms.Integration

Public Class frm_LaporanHPP

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Laporan HPP"
        Me.Text = JudulForm

        usc_LaporanHPP = New wpfUsc_LaporanHPP
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanHPP
        Me.Controls.Add(host)

    End Sub

End Class