Imports System.Windows.Forms.Integration

Public Class frm_LaporanLabaRugi_Bulanan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Laporan Rugi/Laba Bulanan"
        Me.Text = JudulForm

        usc_LaporanLabaRugi_Bulanan = New wpfUsc_LaporanLabaRugi_Bulanan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanLabaRugi_Bulanan
        Me.Controls.Add(host)

    End Sub

End Class