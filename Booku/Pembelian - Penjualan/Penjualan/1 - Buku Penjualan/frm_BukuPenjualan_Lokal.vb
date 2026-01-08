Imports System.Windows.Forms.Integration

Public Class frm_BukuPenjualan_lokal

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Penjualan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPenjualan_Lokal
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Lokal = New wpfUsc_BukuPenjualan
        usc_BukuPenjualan_Lokal.JudulForm = JudulForm
        usc_BukuPenjualan_Lokal.JenisPenjualan = usc_BukuPenjualan_Lokal.JenisPenjualan_Rutin
        usc_BukuPenjualan_Lokal.DestinasiPenjualan = DestinasiPenjualan_Lokal
    End Sub

End Class
