Imports System.Windows.Forms.Integration

Public Class frm_BarangDalamProses_TarikanData

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname Barang Dalam Proses (Tarikan Data)"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BarangDalamProses_TarikanData
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BarangDalamProses_TarikanData = New wpfUsc_StockOpname
        usc_BarangDalamProses_TarikanData.JenisStok_Menu = JenisStok_BarangDalamProses
        usc_BarangDalamProses_TarikanData.JenisPengecekan_Menu = usc_BarangDalamProses_TarikanData.JenisPengecekan_TarikanData
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangDalamProses_TarikanData.RefreshTampilanData()
    End Sub

End Class
