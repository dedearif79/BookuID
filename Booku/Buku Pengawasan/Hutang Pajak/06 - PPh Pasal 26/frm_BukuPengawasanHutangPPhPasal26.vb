Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal26

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang PPh Pasal 26"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal26
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal26 = New wpfUsc_BukuPengawasanHutangPPhPasal26
        usc_BukuPengawasanHutangPPhPasal26.JenisPajak = JenisPajak_PPhPasal26
        usc_BukuPengawasanHutangPPhPasal26.AwalanBP = AwalanBPHP26
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal26_100
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal26_101
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal26_102
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal26_103
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal26_104
        usc_BukuPengawasanHutangPPhPasal26.COAHutangPajak_105 = KodeTautanCOA_HutangPPhPasal26_105
        usc_BukuPengawasanHutangPPhPasal26.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal26.cmb_TahunPajak, TahunBukuAktif)
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal26.cmb_MasaPajak, usc_BukuPengawasanHutangPPhPasal26.MasaPajak_Rekap)
        EksekusiKode = True
        usc_BukuPengawasanHutangPPhPasal26.TampilkanData()
    End Sub

End Class