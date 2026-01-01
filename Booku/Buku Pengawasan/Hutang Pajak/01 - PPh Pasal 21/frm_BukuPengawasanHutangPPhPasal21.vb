Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal21

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang PPh Pasal 21"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal21
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal21 = New wpfUsc_BukuPengawasanHutangPPhPasal21
        usc_BukuPengawasanHutangPPhPasal21.JenisPajak = JenisPajak_PPhPasal21
        usc_BukuPengawasanHutangPPhPasal21.AwalanBP = AwalanBPHP21
        usc_BukuPengawasanHutangPPhPasal21.COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal21_100
        usc_BukuPengawasanHutangPPhPasal21.COAHutangPajak_401 = KodeTautanCOA_HutangPPhPasal21_401
        usc_BukuPengawasanHutangPPhPasal21.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal21.cmb_TahunPajak, TahunBukuAktif)
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal21.cmb_MasaPajak, usc_BukuPengawasanHutangPPhPasal21.MasaPajak_Rekap)
        EksekusiKode = True
        usc_BukuPengawasanHutangPPhPasal21.TampilkanData()
    End Sub

End Class