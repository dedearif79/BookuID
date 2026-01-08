Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangUsaha_NonAfiliasi

    Public JudulForm
    Public KodeMataUang = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha - Non Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha_NonAfiliasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.JenisRelasi_Induk = JenisRelasi_NonAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.RefreshTampilanData()
    End Sub

End Class