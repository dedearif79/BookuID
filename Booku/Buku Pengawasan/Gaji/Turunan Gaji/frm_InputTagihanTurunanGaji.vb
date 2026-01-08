Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputTagihanTurunanGaji

    Public JudulForm
    Public JudulForm_BPJSKesehatan = "Buku Pengawasan Hutang BPJS Kesehatan"
    Public JudulForm_BPJSKetenagakerjaan = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
    Public JudulForm_HutangKoperasiKaryawan = "Buku Pengawasan Hutang Koperasi Karyawan"
    Public JudulForm_HutangSerikat = "Buku Pengawasan Hutang Serikat"
    Public Bulan
    Public JumlahTagihan
    Public JumlahPotongan
    Public Keterangan

    Public TahunTelusurData
    Dim TabelPengawasan

    Public TombolPenutup

    Private Sub frm_InputJurnalPerTransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case JudulForm
            Case JudulForm_BPJSKesehatan
                TabelPengawasan = "tbl_PengawasanHutangBpjsKesehatan"
            Case JudulForm_BPJSKetenagakerjaan
                TabelPengawasan = "tbl_PengawasanHutangBpjsKetenagakerjaan"
            Case JudulForm_HutangKoperasiKaryawan
                TabelPengawasan = "tbl_PengawasanHutangKoperasiKaryawan"
            Case JudulForm_HutangSerikat
                TabelPengawasan = "tbl_PengawasanHutangSerikat"
            Case Kosongan
                PesanUntukProgrammer("Judul Form belum ditentukan...!!!")
        End Select

        If TahunTelusurData = 0 Then PesanUntukProgrammer("Tahun Telusur Data belum ditentukan...!!!")

        Me.Text = JudulForm

    End Sub

    Public Sub ResetForm()
        JudulForm = Kosongan
        TahunTelusurData = 0
        txt_Bulan.Enabled = False
        txt_JumlahTagihan.Enabled = True
        txt_JumlahPotongan.Enabled = False
        txt_Bulan.Text = Kosongan
        txt_JumlahTagihan.Text = Kosongan
        txt_JumlahPotongan.Text = Kosongan
        txt_Keterangan.Text = Kosongan
    End Sub

    Private Sub txt_Bulan_TextChanged(sender As Object, e As EventArgs) Handles txt_Bulan.TextChanged
        Bulan = txt_Bulan.Text
    End Sub

    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTagihan.TextChanged
        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTagihan)
    End Sub

    Private Sub txt_JumlahPotongan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPotongan.TextChanged
        JumlahPotongan = AmbilAngka(txt_JumlahPotongan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPotongan)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        Keterangan = txt_Keterangan.Text

        TrialBalance_Mentahkan()

        If JumlahTagihan = 0 Then Keterangan = Nothing

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                              " Jumlah_Tagihan = '" & JumlahTagihan & "', " &
                              " Keterangan = '" & Keterangan & "' " &
                              " WHERE Bulan = '" & Bulan & "' " &
                              " AND Tahun = '" & TahunTelusurData & "' ", KoneksiDatabaseTransaksi)
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            MsgBox("Data BERHASIL disimpan.")
            If JudulForm = JudulForm_BPJSKesehatan Then usc_BukuPengawasanHutangBPJSKesehatan.TampilkanData()
            If JudulForm = JudulForm_BPJSKetenagakerjaan Then usc_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
            If JudulForm = JudulForm_HutangKoperasiKaryawan Then usc_BukuPengawasanHutangKoperasiKaryawan.TampilkanData()
            If JudulForm = JudulForm_HutangSerikat Then usc_BukuPengawasanHutangSerikat.TampilkanData()
            Me.Close()
        Else
            MsgBox("Data GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        TombolPenutup = "BATAL"
        Me.Close()
    End Sub

End Class