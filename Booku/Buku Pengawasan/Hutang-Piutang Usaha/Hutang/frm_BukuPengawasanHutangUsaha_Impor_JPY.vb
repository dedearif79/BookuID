Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_Impor_JPY

    Public JudulForm
    Public KodeMataUang = KodeMataUang_JPY

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_Impor_JPY
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_JPY = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_JPY.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_JPY.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_JPY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_JPY.RefreshTampilanData()
    End Sub

End Class