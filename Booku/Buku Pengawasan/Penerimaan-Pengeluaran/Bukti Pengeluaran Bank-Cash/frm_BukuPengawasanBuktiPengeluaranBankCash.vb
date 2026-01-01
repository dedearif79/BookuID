Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanBuktiPengeluaranBankCash

    Public JudulForm

    Private Sub frm_BukuPengawasanBuktiPengeluaranBankCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Bukti Pengeluaran Bank-Cash"
        Me.Text = JudulForm

        usc_BukuPengawasanBuktiPengeluaranBankCash = New wpfUsc_BukuPengawasanBuktiPengeluaranBankCash
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanBuktiPengeluaranBankCash
        Me.Controls.Add(host)

    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize

    End Sub

End Class