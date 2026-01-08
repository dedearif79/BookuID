Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangAfiliasi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangAfiliasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangAfiliasi = New wpfUsc_BukuPengawasanPiutangAfiliasi
        usc_BukuPengawasanPiutangAfiliasi.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGAFILIASI
        usc_BukuPengawasanPiutangAfiliasi.JudulForm = JudulForm
        usc_BukuPengawasanPiutangAfiliasi.COAPiutang = KodeTautanCOA_PiutangAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangAfiliasi.RefreshTampilanData()
    End Sub

End Class