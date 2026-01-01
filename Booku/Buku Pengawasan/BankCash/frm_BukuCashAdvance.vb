Imports System.Windows.Forms.Integration

Public Class frm_BukuCashAdvance

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Baris-baris Coding yang ada di sini jangan dijadikan acuan untuk di-copy pada Modul Baru...!!!!!!!!!!
        'Dia beda tersendiri....!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        Style_HalamanModul(Me)

        JudulForm = "Buku Cash Advance"
        Me.Text = JudulForm

        usc_BukuCashAdvance = New wpfUsc_BukuBesar
        usc_BukuCashAdvance.FungsiModul = Halaman_BUKUCASHADVANCE
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuCashAdvance
        Me.Controls.Add(host)

    End Sub

End Class