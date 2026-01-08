Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangPemegangSaham

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Pemegang Saham"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangPemegangSaham
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangPemegangSaham = New wpfUsc_BukuPengawasanPiutangPemegangSaham
        usc_BukuPengawasanPiutangPemegangSaham.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM
        usc_BukuPengawasanPiutangPemegangSaham.COAPiutang = KodeTautanCOA_PiutangPemegangSaham
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangPemegangSaham.RefreshTampilanData()
    End Sub

End Class