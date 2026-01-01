Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal22_Impor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan PPh Pasal 22 - Impor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal22_Impor
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal22_Impor = New wpfUsc_BukuPengawasanHutangPPhPasal22_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.JenisPajak = JenisPajak_PPhPasal22_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.COAPajak = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor
        usc_BukuPengawasanHutangPPhPasal22_Impor.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL22_IMPOR
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal22_Impor.TampilkanData()
    End Sub

End Class