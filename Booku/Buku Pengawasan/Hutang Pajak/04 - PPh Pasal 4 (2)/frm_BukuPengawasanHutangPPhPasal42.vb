Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPPhPasal42

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang PPh Pasal 4 (2)"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPPhPasal42
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanHutangPPhPasal42 = New wpfUsc_BukuPengawasanHutangPPhPasal42
        usc_BukuPengawasanHutangPPhPasal42.JenisPajak = JenisPajak_PPhPasal42
        usc_BukuPengawasanHutangPPhPasal42.AwalanBP = AwalanBPHP42
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_402 = KodeTautanCOA_HutangPPhPasal42_402
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_403 = KodeTautanCOA_HutangPPhPasal42_403
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_409 = KodeTautanCOA_HutangPPhPasal42_409
        usc_BukuPengawasanHutangPPhPasal42.COAHutangPajak_419 = KodeTautanCOA_HutangPPhPasal42_419
        usc_BukuPengawasanHutangPPhPasal42.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        EksekusiKode = False
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal42.cmb_TahunPajak, TahunBukuAktif)
        IsiValueComboBypassTerkunci(usc_BukuPengawasanHutangPPhPasal42.cmb_MasaPajak, usc_BukuPengawasanHutangPPhPasal42.MasaPajak_Rekap)
        EksekusiKode = True
        usc_BukuPengawasanHutangPPhPasal42.TampilkanData()
    End Sub

End Class