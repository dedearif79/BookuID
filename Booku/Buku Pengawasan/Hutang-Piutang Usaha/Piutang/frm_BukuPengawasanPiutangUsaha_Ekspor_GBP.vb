Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangUsaha_Ekspor_GBP

    Public JudulForm
    Public KodeMataUang = KodeMataUang_GBP

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha_Ekspor_GBP
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.KodeMataUang = KodeMataUang_GBP
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.RefreshTampilanData()
    End Sub

End Class