Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanTurunanGaji
' 1 file berisi 4 varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Hutang BPJS Kesehatan
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangBPJSKesehatan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang BPJS Kesehatan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangBPJSKesehatan
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


' ---------------------------------------------------------------------
' VARIAN 2: Hutang BPJS Ketenagakerjaan
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangBPJSKetenagakerjaan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangBPJSKetenagakerjaan
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


' ---------------------------------------------------------------------
' VARIAN 3: Hutang Koperasi Karyawan
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangKoperasiKaryawan
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Koperasi Karyawan"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangKoperasiKaryawan
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


' ---------------------------------------------------------------------
' VARIAN 4: Hutang Serikat
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangSerikat
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Serikat"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangSerikat
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
