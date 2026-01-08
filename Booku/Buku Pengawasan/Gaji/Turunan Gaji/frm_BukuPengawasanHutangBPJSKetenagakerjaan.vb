Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangBPJSKetenagakerjaan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = usc_BukuPengawasanHutangBPJSKetenagakerjaan.JudulForm_HutangBPJSKetenagakerjaan
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangBPJSKetenagakerjaan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangBPJSKetenagakerjaan = New wpfUsc_BukuPengawasanTurunanGaji
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.JudulForm = JudulForm
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.COAHutang = KodeTautanCOA_HutangBpjsKetenagakerjaan
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.TabelPengawasan = "tbl_PengawasanHutangBpjsKetenagakerjaan"
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.AwalanBPH = AwalanBPHTK
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.KolomPotongan = "Potongan_Hutang_BPJS_Ketenagakerjaan"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangBPJSKetenagakerjaan.RefreshTampilanData()
    End Sub

End Class