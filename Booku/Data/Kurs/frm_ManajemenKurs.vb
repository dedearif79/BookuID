Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_ManajemenKurs

    Public JudulForm

    Private Sub frm_ManajemenUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Manajemen Kurs"
        Me.Text = JudulForm

        usc_ManajemenKurs = New wpfUsc_ManajemenKurs
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_ManajemenKurs
        Me.Controls.Add(host)

    End Sub

End Class