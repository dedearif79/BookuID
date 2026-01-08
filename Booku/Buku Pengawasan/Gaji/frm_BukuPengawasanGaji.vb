Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanGaji

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Gaji"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanGaji
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanGaji = New wpfUsc_BukuPengawasanGaji
        usc_BukuPengawasanGaji.NamaHalaman = Halaman_BUKUPENGAWASANGAJI
        usc_BukuPengawasanGaji.JudulForm = JudulForm
        usc_BukuPengawasanGaji.COAHutang = KodeTautanCOA_HutangGaji
    End Sub

    Sub CekKesesuaianSaldo()
        Inisialisasi()
        usc_BukuPengawasanGaji.RefreshTampilanData()
    End Sub

End Class