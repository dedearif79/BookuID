Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangSerikat

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = usc_BukuPengawasanHutangSerikat.JudulForm_HutangSerikat
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangSerikat
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangSerikat = New wpfUsc_BukuPengawasanTurunanGaji
        usc_BukuPengawasanHutangSerikat.JudulForm = JudulForm
        usc_BukuPengawasanHutangSerikat.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGSERIKAT
        usc_BukuPengawasanHutangSerikat.COAHutang = KodeTautanCOA_HutangSerikat
        usc_BukuPengawasanHutangSerikat.TabelPengawasan = "tbl_PengawasanHutangSerikat"
        usc_BukuPengawasanHutangSerikat.AwalanBPH = AwalanBPHS
        usc_BukuPengawasanHutangSerikat.KolomPotongan = "Potongan_Hutang_Serikat"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangSerikat.RefreshTampilanData()
    End Sub

End Class