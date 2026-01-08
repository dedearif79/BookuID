Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangPemegangSaham

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Pemegang Saham"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangPemegangSaham
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangPemegangSaham = New wpfUsc_BukuPengawasanHutangPemegangSaham
        usc_BukuPengawasanHutangPemegangSaham.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM
        usc_BukuPengawasanHutangPemegangSaham.COAHutang = KodeTautanCOA_HutangPemegangSaham
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangPemegangSaham.RefreshTampilanData()
    End Sub

End Class