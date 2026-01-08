Imports System.Windows.Forms.Integration

Public Class frm_BarangDalamProses_CekFisik

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname Barang Dalam Proses (Cek Fisik)"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BarangDalamProses_CekFisik
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BarangDalamProses_CekFisik = New wpfUsc_StockOpname
        usc_BarangDalamProses_CekFisik.NamaFormHalaman = Me
        usc_BarangDalamProses_CekFisik.JenisStok_Menu = JenisStok_BarangDalamProses
        usc_BarangDalamProses_CekFisik.JenisPengecekan_Menu = usc_BarangDalamProses_CekFisik.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BarangDalamProses_CekFisik.RefreshTampilanData()
    End Sub

End Class