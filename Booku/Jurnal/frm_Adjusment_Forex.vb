Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_Adjusment_Forex

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Adjusment Forex - " & TahunBukuAktif
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_Adjusment_Forex
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_Adjusment_Forex = New wpfUsc_JurnalAdjusment_Forex
    End Sub

    Sub CekAdjusment()
        Inisialisasi()
        usc_Adjusment_Forex.TampilkanData()
    End Sub

End Class