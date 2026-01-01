Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal23

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang PPh Pasal 23"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal23
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal23 = New wpfUsc_BukuPengawasanHutangPPhPasal23
        usc_BukuPengawasanHutangPPhPasal23.JenisPajak = JenisPajak_PPhPasal23
        usc_BukuPengawasanHutangPPhPasal23.AwalanBP = AwalanBPHP23
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal23_100
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal23_101
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal23_102
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal23_103
        usc_BukuPengawasanHutangPPhPasal23.COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal23_104
        usc_BukuPengawasanHutangPPhPasal23.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal23.cmb_TahunPajak, TahunBukuAktif)
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal23.cmb_MasaPajak, usc_BukuPengawasanHutangPPhPasal23.MasaPajak_Rekap)
        EksekusiKode = True
        usc_BukuPengawasanHutangPPhPasal23.TampilkanData()
    End Sub

End Class