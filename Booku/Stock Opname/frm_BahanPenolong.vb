Imports System.Windows.Forms.Integration

Public Class frm_BahanPenolong

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname Bahan Penolong"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BahanPenolong
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BahanPenolong = New wpfUsc_StockOpname
        usc_BahanPenolong.NamaFormHalaman = Me
        usc_BahanPenolong.JenisStok_Menu = JenisStok_BahanPenolong
        usc_BahanPenolong.JenisPengecekan_Menu = usc_BahanPenolong.JenisPengecekan_CekFisik
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BahanPenolong.RefreshTampilanData()
    End Sub

End Class