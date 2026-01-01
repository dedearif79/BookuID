Imports System.Windows.Forms.Integration

Public Class frm_BukuPenjualan_Ekspor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Penjualan - Ekspor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPenjualan_Ekspor
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Ekspor = New wpfUsc_BukuPenjualan
        usc_BukuPenjualan_Ekspor.JudulForm = JudulForm
        usc_BukuPenjualan_Ekspor.JenisPenjualan = usc_BukuPenjualan_Ekspor.JenisPenjualan_Rutin
        usc_BukuPenjualan_Ekspor.DestinasiPenjualan = DestinasiPenjualan_Ekspor
    End Sub

End Class
