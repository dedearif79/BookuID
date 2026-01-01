Imports System.Windows.Forms.Integration

Public Class frm_BukuKas

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Baris-baris Coding yang ada di sini jangan dijadikan acuan untuk di-copy pada Modul Baru...!!!!!!!!!!
        'Dia beda tersendiri....!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        Style_HalamanModul(Me)

        JudulForm = "Buku Kas"
        Me.Text = JudulForm

        usc_BukuKas = New wpfUsc_BukuBesar
        usc_BukuKas.FungsiModul = Halaman_BUKUKAS
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuKas
        Me.Controls.Add(host)

    End Sub

End Class