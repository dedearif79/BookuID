Imports System.Windows.Forms.Integration
Imports bcomm


Public Class frm_JurnalAdjusment

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Jurnal Adjusment - " & TahunBukuAktif
        Me.Text = JudulForm

        usc_JurnalAdjusment = New wpfUsc_JurnalAdjusment
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_JurnalAdjusment
        Me.Controls.Add(host)

    End Sub

End Class