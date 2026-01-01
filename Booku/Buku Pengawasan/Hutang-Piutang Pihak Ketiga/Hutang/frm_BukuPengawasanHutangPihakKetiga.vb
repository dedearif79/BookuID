Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPihakKetiga

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Pihak Ketiga"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPihakKetiga
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPihakKetiga = New wpfUsc_BukuPengawasanHutangPihakKetiga
        usc_BukuPengawasanHutangPihakKetiga.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA
        usc_BukuPengawasanHutangPihakKetiga.JudulForm = JudulForm
        usc_BukuPengawasanHutangPihakKetiga.COAHutang = KodeTautanCOA_HutangPihakKetiga
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPihakKetiga.RefreshTampilanData()
    End Sub

End Class