Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_NonAfiliasi

    Public JudulForm
    Public KodeMataUang = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Non Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_NonAfiliasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.AsalPembelian = AsalPembelian_Lokal
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.JenisRelasi_Induk = JenisRelasi_NonAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.RefreshTampilanData()
    End Sub

End Class