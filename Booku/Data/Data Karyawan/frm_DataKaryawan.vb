Imports System.Windows.Forms.Integration

Public Class frm_DataKaryawan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Data Karyawan"
        Me.Text = JudulForm

        usc_DataKaryawan = New wpfUsc_DataKaryawan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataKaryawan
        Me.Controls.Add(host)

    End Sub

End Class
