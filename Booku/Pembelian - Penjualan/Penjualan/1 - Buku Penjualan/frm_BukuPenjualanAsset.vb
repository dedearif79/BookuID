Imports System.Windows.Forms.Integration

Public Class frm_BukuPenjualanAsset

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Penjualan - Asset"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPenjualan_Asset
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPenjualan_Asset = New wpfUsc_BukuPenjualan
        usc_BukuPenjualan_Asset.JudulForm = JudulForm
        usc_BukuPenjualan_Asset.JenisPenjualan = usc_BukuPenjualan_Asset.JenisPenjualan_Asset
        usc_BukuPenjualan_Asset.DestinasiPenjualan = DestinasiPenjualan_Lokal
    End Sub

End Class
