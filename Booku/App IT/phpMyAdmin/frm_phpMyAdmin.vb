Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Integration
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class frm_phpMyAdmin

    Public JudulForm

    Private Sub frm_phpMyAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "phpMyAdmin"
        Me.Text = JudulForm

        usc_phpMyAdmin = New wpfUsc_WebBrowser
        usc_phpMyAdmin.AlamatURL = urlPhpMyAdmin
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_phpMyAdmin
        Me.Controls.Add(host)

    End Sub

End Class