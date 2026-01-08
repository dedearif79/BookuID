Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangUsaha_Afiliasi

    Public JudulForm
    Public KodeMataUang = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha - Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha_Afiliasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Afiliasi = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Afiliasi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha_Afiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Afiliasi.JenisRelasi_Induk = JenisRelasi_Afiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Afiliasi.RefreshTampilanData()
    End Sub

End Class