Imports System.Windows.Forms.Integration

Public Class frm_BukuPembelian_Impor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pembelian - Impor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPembelian_Impor
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Impor = New wpfUsc_BukuPembelian
        usc_BukuPembelian_Impor.JudulForm = JudulForm
        usc_BukuPembelian_Impor.AsalPembelian = AsalPembelian_Impor
    End Sub

End Class
