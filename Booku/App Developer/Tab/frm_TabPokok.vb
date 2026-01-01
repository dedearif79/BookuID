Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Integration
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class frm_TabPokok

    Public JudulForm

    Private Sub frm_TabPokok_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Tab Pokok"
        Me.Text = JudulForm

        usc_TabPokok = New wpfUsc_TabPokok
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_TabPokok
        Me.Controls.Add(host)

    End Sub

End Class