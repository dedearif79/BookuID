Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangPihakKetiga

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Pihak Ketiga"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangPihakKetiga
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangPihakKetiga = New wpfUsc_BukuPengawasanPiutangPihakKetiga
        usc_BukuPengawasanPiutangPihakKetiga.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA
        usc_BukuPengawasanPiutangPihakKetiga.JudulForm = JudulForm
        usc_BukuPengawasanPiutangPihakKetiga.COAPiutang = KodeTautanCOA_PiutangPihakKetiga
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangPihakKetiga.RefreshTampilanData()
    End Sub

End Class