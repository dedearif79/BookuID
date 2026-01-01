Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangUsaha_Ekspor_JPY

    Public JudulForm
    Public KodeMataUang = KodeMataUang_JPY

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha_Ekspor_JPY
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KodeMataUang = KodeMataUang_JPY
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.RefreshTampilanData()
    End Sub

End Class