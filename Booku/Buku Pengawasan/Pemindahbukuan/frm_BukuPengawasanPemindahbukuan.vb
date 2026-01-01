Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPemindahbukuan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Pemindahbukuan"
        Me.Text = JudulForm

        usc_BukuPengawasanPemindahbukuan = New wpfUsc_BukuPengawasanPemindahbukuan
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPemindahbukuan
        Me.Controls.Add(host)

    End Sub

End Class