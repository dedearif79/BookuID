Imports System.Windows.Forms.Integration

Public Class frm_BukuPembelian_lokal

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pembelian"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPembelian_Lokal
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian
        usc_BukuPembelian_Lokal.JudulForm = JudulForm
        usc_BukuPembelian_Lokal.AsalPembelian = AsalPembelian_Lokal
    End Sub

End Class
