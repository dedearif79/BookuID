Imports System.Windows.Forms.Integration

Public Class frm_LaporanLabaRugi_Tahunan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Laporan Rugi/Laba Tahunan"
        Me.Text = JudulForm

        usc_LaporanLabaRugi_Tahunan = New wpfUsc_LaporanLabaRugi_Tahunan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanLabaRugi_Tahunan
        Me.Controls.Add(host)

    End Sub

End Class