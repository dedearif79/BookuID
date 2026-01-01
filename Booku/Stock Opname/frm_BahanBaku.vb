Imports System.Windows.Forms.Integration

Public Class frm_BahanBaku

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname Bahan Baku"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BahanBaku
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BahanBaku = New wpfUsc_StockOpname
        usc_BahanBaku.NamaFormHalaman = Me
        usc_BahanBaku.JenisStok_Menu = JenisStok_BahanBaku
        usc_BahanBaku.JenisPengecekan_Menu = usc_BahanBaku.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BahanBaku.RefreshTampilanData()
    End Sub

End Class