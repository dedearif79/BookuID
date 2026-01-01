Imports System.Windows.Forms.Integration
Imports bcomm


Public Class frm_BukuPengawasanPiutangUsaha

    Public JudulForm
    Public KodeMataUang = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha.JenisRelasi_Induk = Pilihan_Semua
        usc_BukuPengawasanPiutangUsaha.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha.COAPiutang = Kosongan
        usc_BukuPengawasanPiutangUsaha.VisibilitasFilterJenisRelasi(True)
    End Sub

End Class