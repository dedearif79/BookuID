Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_Impor_AUD

    Public JudulForm
    Public KodeMataUang = KodeMataUang_AUD

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_Impor_AUD
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_AUD = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_AUD.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_AUD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_AUD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_AUD.RefreshTampilanData()
    End Sub

End Class