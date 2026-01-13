Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls

Public Class wpfWin_InputTagihanTurunanGaji

    ' === PUBLIC PROPERTIES (KONFIGURASI) ===
    Public Property JudulForm As String
    Public Property TahunTelusurData As Integer

    ' === KONSTANTA JUDUL FORM ===
    Public Const JudulForm_BPJSKesehatan As String = "Buku Pengawasan Hutang BPJS Kesehatan"
    Public Const JudulForm_BPJSKetenagakerjaan As String = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
    Public Const JudulForm_HutangKoperasiKaryawan As String = "Buku Pengawasan Hutang Koperasi Karyawan"
    Public Const JudulForm_HutangSerikat As String = "Buku Pengawasan Hutang Serikat"

    ' === PUBLIC PROPERTIES (DATA INPUT) ===
    Public Property Bulan As String
    Public Property JumlahTagihan As Int64
    Public Property JumlahPotongan As Int64
    Public Property Keterangan As String

    ' === PUBLIC PROPERTIES (RETURN VALUE) ===
    Public Property TombolPenutup As String = "BATAL"

    ' === VARIABEL INTERNAL ===
    Dim TabelPengawasan As String

    ' === GUARD FLAGS ===
    Dim ProsesLoadingForm As Boolean


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        ' Tentukan tabel berdasarkan JudulForm
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

        Title = JudulForm

        ' Isi nilai dari property
        txt_Bulan.Text = Bulan
        txt_JumlahTagihan.Text = JumlahTagihan.ToString()
        txt_JumlahPotongan.Text = JumlahPotongan.ToString()
        IsiValueElemenRichTextBox(txt_Keterangan, Keterangan)

        txt_JumlahTagihan.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()
        JudulForm = Kosongan
        TahunTelusurData = 0
        Bulan = Kosongan
        JumlahTagihan = 0
        JumlahPotongan = 0
        Keterangan = Kosongan
        TombolPenutup = "BATAL"

        txt_Bulan.Text = Kosongan
        txt_JumlahTagihan.Text = Kosongan
        txt_JumlahPotongan.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)
    End Sub


    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTagihan.TextChanged
        If ProsesLoadingForm Then Return
        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
    End Sub


    Private Sub txt_JumlahPotongan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPotongan.TextChanged
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        If ProsesLoadingForm Then Return
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)

        If JumlahTagihan = 0 Then Keterangan = Kosongan

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
            Pesan_Sukses("Data berhasil disimpan.")
            If JudulForm = JudulForm_BPJSKesehatan AndAlso usc_BukuPengawasanHutangBPJSKesehatan.StatusAktif Then
                usc_BukuPengawasanHutangBPJSKesehatan.TampilkanData()
            End If
            If JudulForm = JudulForm_BPJSKetenagakerjaan AndAlso usc_BukuPengawasanHutangBPJSKetenagakerjaan.StatusAktif Then
                usc_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
            End If
            If JudulForm = JudulForm_HutangKoperasiKaryawan AndAlso usc_BukuPengawasanHutangKoperasiKaryawan.StatusAktif Then
                usc_BukuPengawasanHutangKoperasiKaryawan.TampilkanData()
            End If
            If JudulForm = JudulForm_HutangSerikat AndAlso usc_BukuPengawasanHutangSerikat.StatusAktif Then
                usc_BukuPengawasanHutangSerikat.TampilkanData()
            End If
            TombolPenutup = "SIMPAN"
            Me.Close()
        Else
            Pesan_Gagal("Data gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        TombolPenutup = "BATAL"
        Me.Close()
    End Sub

End Class
