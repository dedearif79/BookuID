Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPelaporanPPN

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Pelaporan PPN"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPelaporanPPN
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPelaporanPPN = New wpfUsc_BukuPengawasanPelaporanPPN
        usc_BukuPengawasanPelaporanPPN.JenisPajak = JenisPajak_PPN
        usc_BukuPengawasanPelaporanPPN.AwalanBP = AwalanBPHPPN
        usc_BukuPengawasanPelaporanPPN.COAHutangPajak = KodeTautanCOA_HutangPPN
        usc_BukuPengawasanPelaporanPPN.NamaHalaman = Halaman_BUKUPENGAWASANPELAPORANPPN
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanPelaporanPPN.cmb_TahunPajak, TahunBukuAktif)
        EksekusiKode = True
        usc_BukuPengawasanPelaporanPPN.TampilkanData()
    End Sub

End Class
