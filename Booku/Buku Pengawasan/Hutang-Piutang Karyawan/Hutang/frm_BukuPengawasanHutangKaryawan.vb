Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangKaryawan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Karyawan"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangKaryawan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangKaryawan = New wpfUsc_BukuPengawasanHutangKaryawan
        usc_BukuPengawasanHutangKaryawan.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGKARYAWAN
        usc_BukuPengawasanHutangKaryawan.COAHutang = KodeTautanCOA_HutangKaryawan
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangKaryawan.RefreshTampilanData()
    End Sub

End Class