Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangKaryawan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Karyawan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangKaryawan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangKaryawan = New wpfUsc_BukuPengawasanPiutangKaryawan
        usc_BukuPengawasanPiutangKaryawan.NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGKARYAWAN
        usc_BukuPengawasanPiutangKaryawan.COAPiutang = KodeTautanCOA_PiutangKaryawan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangKaryawan.RefreshTampilanData()
    End Sub

End Class