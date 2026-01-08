Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangDividen

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Dividen"
        Me.Text = JudulForm

        usc_BukuPengawasanHutangDividen = New wpfUsc_BukuPengawasanHutangDividen
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangDividen
        Me.Controls.Add(host)

    End Sub

End Class