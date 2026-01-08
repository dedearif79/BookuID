Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal25

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang PPh Pasal 25"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal25
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal25 = New wpfUsc_BukuPengawasanHutangPPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.JenisPajak = JenisPajak_PPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.AwalanBP = AwalanBPHP25
        usc_BukuPengawasanHutangPPhPasal25.COAHutangPajak = KodeTautanCOA_HutangPPhPasal25
        usc_BukuPengawasanHutangPPhPasal25.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal25.cmb_TahunPajak, TahunBukuAktif)
        EksekusiKode = True
        usc_BukuPengawasanHutangPPhPasal25.TampilkanData()
    End Sub

End Class