Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanKetetapanPajak

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Ketetapan Pajak"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanKetetapanPajak
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanKetetapanPajak = New wpfUsc_BukuPengawasanKetetapanPajak
        usc_BukuPengawasanKetetapanPajak.JenisPajak = JenisPajak_KetetapanPajak
        usc_BukuPengawasanKetetapanPajak.AwalanBP = AwalanBPKP
        usc_BukuPengawasanKetetapanPajak.NamaHalaman = Halaman_BUKUPENGAWASANKETETAPANPAJAK
        usc_BukuPengawasanKetetapanPajak.COAHutangPajak = KodeTautanCOA_HutangKetetapanPajak
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanKetetapanPajak.cmb_TahunTunggakanPajak, TahunBukuAktif)
        EksekusiKode = True
        usc_BukuPengawasanKetetapanPajak.RefreshTampilanData()
    End Sub

End Class