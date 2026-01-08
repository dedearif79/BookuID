Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangBPJSKesehatan

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = usc_BukuPengawasanHutangBPJSKesehatan.JudulForm_HutangBPJSKesehatan
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangBPJSKesehatan
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangBPJSKesehatan = New wpfUsc_BukuPengawasanTurunanGaji
        usc_BukuPengawasanHutangBPJSKesehatan.JudulForm = JudulForm
        usc_BukuPengawasanHutangBPJSKesehatan.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN
        usc_BukuPengawasanHutangBPJSKesehatan.COAHutang = KodeTautanCOA_HutangBpjsKesehatan
        usc_BukuPengawasanHutangBPJSKesehatan.TabelPengawasan = "tbl_PengawasanHutangBpjsKesehatan"
        usc_BukuPengawasanHutangBPJSKesehatan.AwalanBPH = AwalanBPHKS
        usc_BukuPengawasanHutangBPJSKesehatan.KolomPotongan = "Potongan_Hutang_BPJS_Kesehatan"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangBPJSKesehatan.RefreshTampilanData()
    End Sub

End Class