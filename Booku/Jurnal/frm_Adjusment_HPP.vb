Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_Adjusment_HPP

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Adjusment HPP - " & TahunBukuAktif
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_JurnalAdjusment_HPP
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_JurnalAdjusment_HPP = New wpfUsc_JurnalAdjusment_HPP
    End Sub

    Sub CekAdjusment()
        Inisialisasi()
        usc_JurnalAdjusment_HPP.TampilkanData()
    End Sub

End Class