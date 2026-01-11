Imports System.Windows.Forms.Integration

Public Class frm_BarangJadi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname Barang Jadi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BarangJadi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BarangJadi = New wpfUsc_StockOpname
        usc_BarangJadi.JenisStok_Menu = JenisStok_BarangJadi
        usc_BarangJadi.JenisPengecekan_Menu = usc_BarangJadi.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangJadi.RefreshTampilanData()
    End Sub

End Class
