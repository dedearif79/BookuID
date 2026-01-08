Imports System.Windows.Forms.Integration

Public Class frm_BundelPengajuanPengeluaranBankCash

    Public JudulForm

    Private Sub frm_BundelPengajuanPengeluaranBankCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Bundel Pengajuan Pengeluaran Bank-Cash"
        Me.Text = JudulForm

        usc_BundelPengajuanPengeluaranBankCash = New wpfUsc_BundelPengajuanPengeluaranBankCash
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BundelPengajuanPengeluaranBankCash
        Me.Controls.Add(host)

    End Sub

End Class