Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangAfiliasi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangAfiliasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangAfiliasi = New wpfUsc_BukuPengawasanHutangAfiliasi
        usc_BukuPengawasanHutangAfiliasi.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGAFILIASI
        usc_BukuPengawasanHutangAfiliasi.JudulForm = JudulForm
        usc_BukuPengawasanHutangAfiliasi.COAHutang = KodeTautanCOA_HutangAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangAfiliasi.RefreshTampilanData()
    End Sub

End Class