Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanBuktiPenerimaanBankCash

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Bukti Penerimaan Bank-Cash"
        Me.Text = JudulForm

        usc_BukuPengawasanBuktiPenerimaanBankCash = New wpfUsc_BukuPengawasanBuktiPenerimaanBankCash
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanBuktiPenerimaanBankCash
        Me.Controls.Add(host)

    End Sub

End Class