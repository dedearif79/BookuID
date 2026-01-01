Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPajakImpor

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Pajak-pajak Impor"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPajakImpor
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanPajakImpor = New wpfUsc_BukuPengawasanPajakImpor
        usc_BukuPengawasanPajakImpor.JenisPajak = JenisPajak_PajakPajakImpor
        usc_BukuPengawasanPajakImpor.AwalanBP = AwalanBPHP23
        usc_BukuPengawasanPajakImpor.COABeaMasukImpor = KodeTautanCOA_BeaMasuk_Impor
        usc_BukuPengawasanPajakImpor.COAPPhPasal22Impor = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor
        usc_BukuPengawasanPajakImpor.COAPPNMasukanImpor = KodeTautanCOA_PPNMasukan_Impor
        usc_BukuPengawasanPajakImpor.NamaHalaman = Halaman_BUKUPENGAWASANPAJAKPAJAKIMPOR
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPajakImpor.TampilkanData()
    End Sub

End Class