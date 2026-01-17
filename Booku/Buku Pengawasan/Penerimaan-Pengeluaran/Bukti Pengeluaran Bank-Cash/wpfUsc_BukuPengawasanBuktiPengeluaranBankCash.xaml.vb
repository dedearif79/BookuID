Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_BukuPengawasanBuktiPengeluaranBankCash

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim NomorUrut
    Dim NomorID
    Dim AngkaKK_Sebelumnya
    Dim AngkaKK
    Dim NomorKK
    Dim TanggalKK
    Dim Peruntukan
    Dim NomorBundel
    Dim PeruntukanPembayaran
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahInvoicePerBaris
    Dim SaranaPembayaran
    Dim RekeningPenerima
    Dim AtasNamaPenerima
    Dim PenerimaPembayaran
    Dim NomorBP
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim JumlahTagihan
    Dim BiayaAdministrasiBank
    Dim Denda
    Dim JumlahPengajuan
    Dim JumlahBayar
    Dim TanggalBayar
    Dim KodeAkun
    Dim NamaAkun
    Dim StatusPengajuan
    Dim Uraian
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim AngkaKK_Terseleksi
    Dim NomorKK_Terseleksi
    Dim TanggalKK_Terseleksi
    Dim Peruntukan_Terseleksi
    Dim NomorBundel_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahInvoicePerBaris_Terseleksi
    Dim SaranaPembayaran_Terseleksi
    Dim COAKredit_Terseleksi
    Dim PenerimaPembayaran_Terseleksi
    Dim NomorBP_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim BiayaAdministrasiBank_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim TanggalBayar_Terseleksi
    Dim KodeAkun_Terseleksi
    Dim NamaAkun_Terseleksi
    Dim StatusPengajuan_Terseleksi
    Dim Uraian_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    'Variabel Filter :
    Dim Pilih_Kategori
    Dim Pilih_Peruntukan
    Dim Pilih_KodeLawanTransaksi
    Dim Pilih_SaranaPembayaran

    ' Flag untuk mencegah multiple loading bersamaan
    Private SedangMemuatData As Boolean = False


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True
        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_BukuPengawasanBuktiPengeluaranBankCash.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_FilterData.Visibility = Visibility.Visible

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_Kategori()
        KontenCombo_LawanTransaksi()
        KontenCombo_SaranaPembayaran()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData

    ''' <summary>
    ''' Menampilkan data dengan async pattern.
    ''' Baris data ditampilkan satu per satu seperti sistem lama,
    ''' dengan loading window yang tetap responsive (animasi berputar).
    ''' </summary>
    Async Sub TampilkanDataAsync()

        ' Guard clause
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return

        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)

        ' Beri waktu UI untuk menampilkan loading window
        Await Task.Delay(50)

        Try
            ' Style Tabel
            Terabas()
            datatabelUtama.Rows.Clear()

            ' Build filter
            Dim FilterData As String = BuildFilterQuery()

            ' Variabel untuk grouping
            Dim i = 0
            NomorUrut = 0
            AngkaKK_Sebelumnya = 0
            JumlahTagihan = 0
            JumlahPengajuan = 0
            JumlahBayar = 0
            JumlahInvoicePerBaris = 0

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE DATE_FORMAT(Tanggal_KK, '%Y') = '" & TahunBukuAktif & "' " & FilterData &
                                  " ORDER BY Angka_KK ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()

            Do While dr.Read
                AngkaKK = dr.Item("Angka_KK")
                If i > 0 And AngkaKK <> AngkaKK_Sebelumnya Then
                    TambahBaris()
                    ' Yield untuk memberi kesempatan UI refresh (animasi loading + tampil baris)
                    Await Task.Yield()
                End If

                Try
                    NomorKK = dr.Item("Nomor_KK")
                Catch ex As Exception
                    PesanUntukProgrammer("MASALAH YANG INI BELUM DISELESAIKAN....!!!!")
                    Exit Do
                End Try

                TanggalKK = TanggalFormatTampilan(dr.Item("Tanggal_KK"))
                Peruntukan = dr.Item("Peruntukan")
                NomorBundel = dr.Item("Nomor_Bundel")
                PeruntukanPembayaran = dr.Item("Peruntukan")
                KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
                SaranaPembayaran = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
                RekeningPenerima = dr.Item("Rekening_Penerima")
                AtasNamaPenerima = dr.Item("Atas_Nama_Penerima")

                If RekeningPenerima <> Kosongan Then
                    If AtasNamaPenerima <> Kosongan Then
                        PenerimaPembayaran = RekeningPenerima & " a.n. " & AtasNamaPenerima
                    Else
                        PenerimaPembayaran = RekeningPenerima
                    End If
                Else
                    PenerimaPembayaran = StripKosong
                End If

                NomorBP = dr.Item("Nomor_BP")

                If PeruntukanPembayaran = Peruntukan_PembayaranHutangBank _
                    Or PeruntukanPembayaran = Peruntukan_PembayaranHutangLeasing _
                    Or PeruntukanPembayaran = Peruntukan_PembayaranHutangAfiliasi _
                    Or PeruntukanPembayaran = Peruntukan_PembayaranHutangPihakKetiga _
                    Or PeruntukanPembayaran = Peruntukan_PembayaranHutangPajak _
                    Then
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                Else
                    If NomorInvoice = Kosongan Then
                        NomorInvoice = dr.Item("Nomor_Invoice")
                    Else
                        NomorInvoice &= SlashGanda_Pemisah & dr.Item("Nomor_Invoice")
                    End If
                    If TanggalInvoice = Kosongan Then
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        If TanggalInvoice = TanggalKosong Then TanggalInvoice = Kosongan
                    Else
                        TanggalInvoice &= SlashGanda_Pemisah & TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    End If
                End If

                JumlahTagihan += KonversiDesimalKeInt64BulatKeAtas(dr.Item("Kurs") * dr.Item("Jumlah_Tagihan"))
                JumlahPengajuan += KonversiDesimalKeInt64BulatKeAtas(dr.Item("Kurs") * dr.Item("Jumlah_Pengajuan"))
                JumlahBayar += KonversiDesimalKeInt64BulatKeAtas(dr.Item("Kurs") * dr.Item("Jumlah_Bayar"))
                TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
                Denda = dr.Item("Denda")
                KodeAkun = dr.Item("COA_Debet")
                NamaAkun = AmbilValue_NamaAkun(KodeAkun)
                StatusPengajuan = dr.Item("Status_Pengajuan")
                Uraian = PenghapusEnter(dr.Item("Catatan"))
                NomorJV = dr.Item("Nomor_JV")
                User = dr.Item("User")
                AngkaKK_Sebelumnya = AngkaKK
                i += 1
                JumlahInvoicePerBaris += 1
            Loop

            If i > 0 Then TambahBaris()

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync")

        Finally
            BersihkanSeleksi()
            ' Enable UI dan tutup loading
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False
        End Try

    End Sub


    ''' <summary>
    ''' Build filter query dari variabel filter yang sudah diset.
    ''' </summary>
    Private Function BuildFilterQuery() As String
        Dim FilterKategori As String = Spasi1
        If Pilih_Kategori IsNot Nothing AndAlso Pilih_Kategori.ToString <> Kosongan AndAlso Pilih_Kategori.ToString <> Pilihan_Semua Then
            FilterKategori = " AND Kategori = '" & Pilih_Kategori.ToString & "' "
        End If

        Dim FilterPeruntukan As String = Spasi1
        If Pilih_Peruntukan IsNot Nothing AndAlso Pilih_Peruntukan.ToString <> Kosongan AndAlso Pilih_Peruntukan.ToString <> Pilihan_Semua Then
            FilterPeruntukan = " AND Peruntukan = '" & Pilih_Peruntukan.ToString & "' "
        End If

        Dim FilterLawanTransaksi As String = Spasi1
        If Pilih_KodeLawanTransaksi IsNot Nothing AndAlso Pilih_KodeLawanTransaksi.ToString <> Kosongan AndAlso Pilih_KodeLawanTransaksi.ToString <> Pilihan_Semua Then
            FilterLawanTransaksi = " AND Kode_Lawan_Transaksi = '" & Pilih_KodeLawanTransaksi.ToString & "' "
        End If

        Dim FilterSaranaPembayaran As String = Spasi1
        If Pilih_SaranaPembayaran IsNot Nothing AndAlso Pilih_SaranaPembayaran.ToString <> Kosongan AndAlso Pilih_SaranaPembayaran.ToString <> Pilihan_Semua Then
            FilterSaranaPembayaran = " AND COA_Kredit = '" & Pilih_SaranaPembayaran.ToString & "' "
        End If

        Return FilterKategori & FilterPeruntukan & FilterLawanTransaksi & FilterSaranaPembayaran
    End Function


    ''' <summary>
    ''' Wrapper untuk backward compatibility.
    ''' Dipanggil dari form lain yang masih menggunakan nama lama.
    ''' </summary>
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        JumlahPengajuan += Denda
        If JumlahBayar > 0 Then JumlahBayar += Denda
        If Not StatusPengajuan = Status_Dibayar Then
            JumlahBayar = 0
            TanggalBayar = StripKosong
        End If
        datatabelUtama.Rows.Add(NomorUrut, AngkaKK_Sebelumnya, NomorKK, TanggalKK, Peruntukan, NomorBundel, KodeLawanTransaksi, NamaLawanTransaksi, JumlahInvoicePerBaris, SaranaPembayaran, PenerimaPembayaran,
                                NomorBP, NomorInvoice, TanggalInvoice, JumlahTagihan, BiayaAdministrasiBank, JumlahBayar, TanggalBayar,
                                KodeAkun, NamaAkun, StatusPengajuan, Uraian, NomorJV, User)
        Dim i = NomorUrut - 1
        NomorInvoice = Kosongan
        TanggalInvoice = Kosongan
        JumlahTagihan = 0
        JumlahPengajuan = 0
        JumlahBayar = 0
        JumlahInvoicePerBaris = 0
        Terabas()
    End Sub

    ''' <summary>
    ''' Membersihkan seleksi tanpa mengubah state loading.
    ''' Digunakan oleh TampilkanDataAsync() karena loading dihandle oleh MuatDataDenganLoading().
    ''' </summary>
    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_Cetak.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_LihatBundelan.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Membersihkan seleksi dan mengaktifkan kembali UI.
    ''' Digunakan untuk backward compatibility dengan kode lama.
    ''' </summary>
    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True, False)
    End Sub

    Sub KontenCombo_Kategori()
        KontenCombo_KategoriPengeluaran_Public(cmb_Kategori)
        cmb_Kategori.Items.Add(Pilihan_Semua)
        cmb_Kategori.SelectedValue = Pilihan_Semua
    End Sub

    Sub Kontencombo_PeruntukanSemua()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanPembayaranHutang()
        KontenCombo_PeruntukanPembayaranHutang_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanPengeluaranTunai()
        KontenCombo_PeruntukanPengeluaranTunai_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanPemindahbukuan()
        KontenCombo_PeruntukanPemindahbukuan_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanInvestasi()
        KontenCombo_PeruntukanInvestasi_Public(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub



    Sub KontenCombo_LawanTransaksi()
        cmb_LawanTransaksi.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT Nama_Mitra FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_LawanTransaksi.Items.Add(Pilihan_Semua)
        Do While dr.Read
            cmb_LawanTransaksi.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        cmb_LawanTransaksi.Items.Add(Pilihan_Semua)
        cmb_LawanTransaksi.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub

    Sub KontenCombo_SaranaPembayaran()
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang_Semua)
        cmb_SaranaPembayaran.Items.Add(Pilihan_Semua)
        cmb_SaranaPembayaran.SelectedValue = Pilihan_Semua
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Bundelan_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_Bundelan.Click
        host_BundelPengajuanPengeluaranBankCash = New wpfHost_BundelPengajuanPengeluaranBankCash
        PesanUntukProgrammer("Ada keanehan di sini : Masalah JUDUL FORM")
        win_BOOKU.BukaUserControlDalamTab(usc_BundelPengajuanPengeluaranBankCash, host_BundelPengajuanPengeluaranBankCash.JudulForm)
    End Sub



    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_LihatBundelan_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatBundelan.Click
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_LIHAT
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.AdaPenyimpanan Then TampilkanData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_EDIT
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.AdaPenyimpanan Then TampilkanData()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_General(Buka)
        AksesDatabase_Transaksi(Buka)

        TransactionBegin_General()
        TransactionBegin_Transaksi()

        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPengeluaran " &
                                   " WHERE Angka_KK = '" & AngkaKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery_Transaction_Transaksi()

        If StatusSuntingDatabase = True Then HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        'Reset/Nol-kan (0) value NomorJV pada Buku Pengawasan Terkait :
        If StatusSuntingDatabase = True Then

            Dim TabelPengawasan = Kosongan
            Dim KolomNomorJV = "Nomor_JV"
            Dim PengosonganDataTambahan = Kosongan
            Dim KoneksiDatabaseTemporal As OdbcConnection = KoneksiDatabaseTransaksi
            Dim SumberDatabase As String = "Database Transaksi"

            Select Case Peruntukan_Terseleksi
                'Pembayaran Hutang :
                Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangUsaha_Afiliasi
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangBiaya
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangGaji
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangBPJSKesehatan
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangKoperasiKaryawan
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangSerikat
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangPihakKetiga
                    TabelPengawasan = "tbl_JadwalAngsuranHutangPihakKetiga"
                    PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
                Case Peruntukan_PembayaranHutangKaryawan
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangLancarLainnya
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangLeasing
                    TabelPengawasan = "tbl_JadwalAngsuranHutangLeasing"
                    PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
                Case Peruntukan_PembayaranHutangBank
                    TabelPengawasan = "tbl_JadwalAngsuranHutangBank"
                    PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
                Case Peruntukan_PembayaranHutangPemegangSaham
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_PembayaranHutangAfiliasi
                    TabelPengawasan = "tbl_JadwalAngsuranHutangAfiliasi"
                    PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
                Case Peruntukan_PembayaranHutangPajak
                    TabelPengawasan = "tbl_Pembelian_Invoice"
                    PengosonganDataTambahan =
                    ", Tanggal_Bayar_Pajak_Impor    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Nomor_JV_Bayar_Pajak_Impor   = 0 "
                    KolomNomorJV = "Nomor_JV_Bayar_Pajak_Impor"
                Case Peruntukan_PembayaranHutangDividen
                    'Belum ada kebutuhan Coding untuk Case ini
                'Pengeluaran Tunai :
                Case Peruntukan_InvoiceTunai
                    TabelPengawasan = "tbl_Pembelian_Invoice"
                Case Peruntukan_PiutangPemegangSaham
                    TabelPengawasan = "tbl_PengawasanPiutangPemegangSaham"
                Case Peruntukan_PiutangKaryawan
                    TabelPengawasan = "tbl_PengawasanPiutangKaryawan"
                Case Peruntukan_PiutangPihakKetiga
                    TabelPengawasan = "tbl_PengawasanPiutangPihakKetiga"
                Case Peruntukan_PiutangAfiliasi
                    TabelPengawasan = "tbl_PengawasanPiutangAfiliasi"
                Case Peruntukan_DepositOperasional
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_BankGaransi
                    'Belum ada kebutuhan Coding untuk Case ini
                'Investasi
                Case Peruntukan_InvestasiDeposito
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_InvestasiSuratBerharga
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_InvestasiLogamMulia
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_InvestasiPadaPerusahaanAnak
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Peruntukan_InvestasiGoodWill
                    'Belum ada kebutuhan Coding untuk Case ini
                Case Else
                    PesanUntukProgrammer("Value NomorJV pada Tabel Pengawasan belum di-RESET Ke Nol (0)...!!!!!!!")
            End Select

            If TabelPengawasan <> Kosongan Then
                cmdHAPUS = New OdbcCommand(" UPDATE " & TabelPengawasan &
                                           " SET    " & KolomNomorJV & " = 0 " &
                                           PengosonganDataTambahan & " WHERE  " & KolomNomorJV & " = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTemporal)
                If SumberDatabase = "Database Transaksi" Then cmdHAPUS_ExecuteNonQuery_Transaction_Transaksi()
                If SumberDatabase = "Database General" Then cmdHAPUS_ExecuteNonQuery_Transaction_General()
            End If

        End If

        TransactionCommit_Transaksi()
        TransactionCommit_General()
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub



    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Select Case StatusPengajuan_Terseleksi
            Case Status_Open
                ProsesCetak()
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                      " Status_Invoice      = '" & Status_Dicetak & "', " &
                                      " Status_Pengajuan    = '" & Status_Dicetak & "' " &
                                      " WHERE Nomor_KK      = '" & NomorKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                If StatusSuntingDatabase = True Then TampilkanData()
            Case Status_Dicetak
                If Not TanyaKonfirmasi("Data sudah pernah dicetak." & Enter2Baris & "Ingin mencetak ulang?") Then Return
                ProsesCetak()
            Case Status_Dibundel
                'PesanPemberitahuan("Data sudah dibundel..!" & "Jika ingin mencetaknya, silakan lepas data terlebih dahulu dari bundelan.")
                If Not TanyaKonfirmasi("Data sudah pernah dicetak." & Enter2Baris & "Ingin mencetak ulang?") Then Return
                ProsesCetak()
            Case Status_Disetujui
                'PesanPemberitahuan("Data sudah disetujui, dan tidak perlu dicetak lagi.")
                If Not TanyaKonfirmasi("Data sudah pernah dicetak." & Enter2Baris & "Ingin mencetak ulang?") Then Return
                ProsesCetak()
            Case Status_Dibayar
                'PesanPemberitahuan("Data sudah diposting ke Jurnal, dan tidak perlu dicetak lagi.")
                If Not TanyaKonfirmasi("Data sudah pernah dicetak." & Enter2Baris & "Ingin mencetak ulang?") Then Return
                ProsesCetak()
        End Select
    End Sub
    Sub ProsesCetak()
        Cetak(JenisFormCetak_PengajuanPengeluaran, NomorKK_Terseleksi, True, False)
    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As RoutedEventArgs) Handles btn_Posting.Click

        If Not (StatusPengajuan_Terseleksi = Status_Disetujui Or StatusPengajuan_Terseleksi = Status_Dibayar) Then
            Pesan_Peringatan("Data tidak dapat diposting ke Jurnal karena belum disetujui." & Enter2Baris &
                             "Silakan setujui terlebih dahulu.")
            Return
        End If

        If COAKredit_Terseleksi = KodeTautanCOA_CashAdvance And StatusPengajuan_Terseleksi <> Status_Dibayar Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                  " WHERE Angka_KK = '" & AngkaKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            Dim JumlahDisetujui = dr.Item("Jumlah_Bayar")
            AksesDatabase_Transaksi(Tutup)
            If JumlahDisetujui > SaldoAkhirCOA(KodeTautanCOA_CashAdvance) Then
                Pesan_Peringatan("Saldo " & AmbilValue_NamaAkun(KodeTautanCOA_CashAdvance) & " tidak mencukupi." & Enter2Baris &
                                 "Silakan posting terlebih dahulu pemindahbukuan untuk akun " & AmbilValue_NamaAkun(KodeTautanCOA_CashAdvance) & ".")
                Return
            End If
        End If

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_POSTING
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_Kategori_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kategori.SelectionChanged
        Pilih_Kategori = cmb_Kategori.SelectedValue
        Select Case Pilih_Kategori
            Case Pilihan_Semua
                Kontencombo_PeruntukanSemua()
            Case Kategori_PembayaranHutang
                KontenCombo_PeruntukanPembayaranHutang()
            Case Kategori_PengeluaranTunai
                KontenCombo_PeruntukanPengeluaranTunai()
            Case Kategori_Pemindahbukuan
                KontenCombo_PeruntukanPemindahbukuan()
            Case Kategori_Investasi
                KontenCombo_PeruntukanInvestasi()
        End Select
    End Sub

    Private Sub cmb_Peruntukan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Peruntukan.SelectionChanged
        Pilih_Peruntukan = cmb_Peruntukan.SelectedValue
        If Pilih_Peruntukan = Kosongan Then Return
        TampilkanData()
    End Sub


    Private Sub cmb_LawanTransaksi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_LawanTransaksi.SelectionChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_LawanTransaksi.SelectedValue & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeLawanTransaksi = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_LawanTransaksi.SelectedValue = Pilihan_Semua Then Pilih_KodeLawanTransaksi = Pilihan_Semua
        TampilkanData()
    End Sub


    Private Sub cmb_SaranaPembeyaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        Pilih_SaranaPembayaran = KonversiSaranaPembayaranKeCOA(cmb_SaranaPembayaran.SelectedValue)
        TampilkanData()
    End Sub


    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        AngkaKK_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Angka_KK").ToString)
        NomorKK_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_KK")
        TanggalKK_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_KK")
        Peruntukan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Peruntukan_")
        NomorBundel_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Bundel")
        KodeLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Lawan_Transaksi")
        JumlahInvoicePerBaris_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Invoice")
        SaranaPembayaran_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Sarana_Pembayaran")
        COAKredit_Terseleksi = KonversiSaranaPembayaranKeCOA(SaranaPembayaran_Terseleksi)
        PenerimaPembayaran_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Penerima_Pembayaran")
        NomorBP_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_BP")
        NomorInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Invoice")
        TanggalInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Invoice")
        JumlahTagihan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Tagihan").ToString)
        BiayaAdministrasiBank_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Biaya_Administrasi_Bank").ToString)
        JumlahBayar_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Bayar").ToString)
        TanggalBayar_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Bayar")
        KodeAkun_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Kredit")
        NamaAkun_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Akun")
        StatusPengajuan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Status_")
        Uraian_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Uraian_")
        NomorJV_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV").ToString)
        User_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "User_")

        If AngkaKK_Terseleksi > 0 Then
            If StatusPengajuan_Terseleksi = Status_Open Or StatusPengajuan_Terseleksi = Status_Dicetak Then
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
                btn_Cetak.IsEnabled = True
            Else
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
                btn_Cetak.IsEnabled = False
            End If
            If NomorBundel_Terseleksi = Kosongan Then
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            End If
            btn_Cetak.IsEnabled = True
            btn_Posting.IsEnabled = True
            btn_LihatBundelan.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            btn_Cetak.IsEnabled = False
            btn_Posting.IsEnabled = False
            btn_LihatBundelan.IsEnabled = False
        End If

        If NomorJV_Terseleksi > 0 Then
            If NomorBundel_Terseleksi <> Kosongan Then btn_Edit.IsEnabled = False
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_LihatBundelan_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Nomor_JV")) > 0 Then
            e.Row.Foreground = clrTeksPrimer
        Else
            e.Row.Foreground = clrNeutral500
        End If
    End Sub



    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut As New DataGridTextColumn
    Dim Angka_KK As New DataGridTextColumn
    Dim Nomor_KK As New DataGridTextColumn
    Dim Tanggal_KK As New DataGridTextColumn
    Dim Peruntukan_ As New DataGridTextColumn
    Dim Nomor_Bundel As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Jumlah_Invoice As New DataGridTextColumn
    Dim Sarana_Pembayaran As New DataGridTextColumn
    Dim Penerima_Pembayaran As New DataGridTextColumn
    Dim Nomor_BP As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Jumlah_Tagihan As New DataGridTextColumn
    Dim Biaya_Administrasi_Bank As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim COA_Kredit As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Status_ As New DataGridTextColumn
    Dim Uraian_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Angka_KK")
        datatabelUtama.Columns.Add("Nomor_KK")
        datatabelUtama.Columns.Add("Tanggal_KK")
        datatabelUtama.Columns.Add("Peruntukan_")
        datatabelUtama.Columns.Add("Nomor_Bundel")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Invoice", GetType(Int64))
        datatabelUtama.Columns.Add("Sarana_Pembayaran")
        datatabelUtama.Columns.Add("Penerima_Pembayaran")
        datatabelUtama.Columns.Add("Nomor_BP")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Jumlah_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Administrasi_Bank", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Bayar")
        datatabelUtama.Columns.Add("COA_Kredit")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Status_")
        datatabelUtama.Columns.Add("Uraian_")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_KK, "Angka_KK", "Angka KK", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_KK, "Nomor_KK", "Nomor Bukti", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_KK, "Tanggal_KK", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Peruntukan_, "Peruntukan_", "Peruntukan", 123, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Bundel, "Nomor_Bundel", "Nomor Bundel", 150, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Lawan Transaksi", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Invoice, "Jumlah_Invoice", "Jumlah Berkas", 63, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sarana_Pembayaran, "Sarana_Pembayaran", "Sarana Pembayaran", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penerima_Pembayaran, "Penerima_Pembayaran", "Penerima Pembayaran", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BP, "Nomor_BP", "Nomor BP", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Tagihan, "Jumlah_Tagihan", "Jumlah Tagihan", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Administrasi_Bank, "Biaya_Administrasi_Bank", "Biaya Adm Bank", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal Bayar", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Kredit, "COA_Kredit", "COA Kredit", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_, "Status_", "Status", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_, "Uraian_", "Uraian", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub datagridUtma_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
