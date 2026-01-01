Imports System.Windows.Forms.Integration

Public Class frm_BukuBesar

    Public JudulForm
    Public AkunTerpilih

    Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Baris-baris Coding yang ada di sini jangan dijadikan acuan untuk di-copy pada Modul Baru...!!!!!!!!!!
        'Dia beda tersendiri....!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        Style_HalamanModul(Me)

        JudulForm = "Buku Besar"
        Me.Text = JudulForm

        usc_BukuBesar = New wpfUsc_BukuBesar
        usc_BukuBesar.FungsiModul = Halaman_BUKUBESAR
        usc_BukuBesar.txt_COA.Text = AkunTerpilih
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuBesar
        Me.Controls.Add(host)

    End Sub

End Class