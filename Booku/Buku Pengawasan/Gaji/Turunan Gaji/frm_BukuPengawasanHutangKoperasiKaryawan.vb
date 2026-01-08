Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangKoperasiKaryawan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = usc_BukuPengawasanHutangKoperasiKaryawan.JudulForm_HutangKoperasiKaryawan
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangKoperasiKaryawan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangKoperasiKaryawan = New wpfUsc_BukuPengawasanTurunanGaji
        usc_BukuPengawasanHutangKoperasiKaryawan.JudulForm = JudulForm
        usc_BukuPengawasanHutangKoperasiKaryawan.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN
        usc_BukuPengawasanHutangKoperasiKaryawan.COAHutang = KodeTautanCOA_HutangKoperasiKaryawan
        usc_BukuPengawasanHutangKoperasiKaryawan.TabelPengawasan = "tbl_PengawasanHutangKoperasiKaryawan"
        usc_BukuPengawasanHutangKoperasiKaryawan.AwalanBPH = AwalanBPHKK
        usc_BukuPengawasanHutangKoperasiKaryawan.KolomPotongan = "Potongan_Hutang_Koperasi"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangKoperasiKaryawan.RefreshTampilanData()
    End Sub

End Class