Imports System.Windows.Forms.Integration

Public Class frm_LaporanTrialBalance

    Public JudulForm
    Public JalurMasuk

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Trial Balance"
        Me.Text = JudulForm

        usc_LaporanTrialBalance = New wpfUsc_LaporanTrialBalance
        usc_LaporanTrialBalance.JalurMasuk = JalurMasuk
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanTrialBalance
        Me.Controls.Add(host)

    End Sub

End Class