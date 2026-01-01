Imports System.Windows.Forms.Integration

Public Class frm_BukuPettyCash

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Baris-baris Coding yang ada di sini jangan dijadikan acuan untuk di-copy pada Modul Baru...!!!!!!!!!!
        'Dia beda tersendiri....!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        Style_HalamanModul(Me)

        JudulForm = "Buku Petty Cash"
        Me.Text = JudulForm

        usc_BukuPettyCash = New wpfUsc_BukuBesar
        usc_BukuPettyCash.FungsiModul = Halaman_BUKUPETTYCASH
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPettyCash
        Me.Controls.Add(host)

    End Sub

End Class