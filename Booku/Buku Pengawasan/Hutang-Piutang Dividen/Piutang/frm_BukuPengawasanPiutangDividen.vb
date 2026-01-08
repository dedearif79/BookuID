Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangDividen

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Dividen"
        Me.Text = JudulForm

        usc_BukuPengawasanPiutangDividen = New wpfUsc_BukuPengawasanPiutangDividen
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangDividen
        Me.Controls.Add(host)

    End Sub

End Class