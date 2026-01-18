Imports bcomm
Imports System.Windows.Controls
Imports System.Windows
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks

Public Class wpfUsc_BukuPengawasanBuktiPenerimaanBankCash

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim AngkaKM_Sebelumnya
    Dim AngkaKM
    Dim NomorKM
    Dim TanggalKM
    Dim Peruntukan
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahInvoicePerBaris
    Dim SaranaPencairan
    Dim NomorBP
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim JumlahTagihan
    Dim BiayaAdministrasiBank
    Dim Pembebanan
    Dim BiayaAdministrasiKontrak
    Dim BiayaNotaris
    Dim Denda
    Dim JumlahBayar
    Dim JumlahDiterima
    Dim KodeAkun
    Dim NamaAkun
    Dim Uraian
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim AngkaKM_Terseleksi
    Dim NomorKM_Terseleksi
    Dim TanggalKM_Terseleksi
    Dim Peruntukan_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahInvoicePerBaris_Terseleksi
    Dim SaranaPencairan_Terseleksi
    Dim COAKredit_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim JumlahDiterima_Terseleksi
    Dim KodeAkun_Terseleksi
    Dim NamaAkun_Terseleksi
    Dim Uraian_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi


    'Variabel Filter :
    Dim Pilih_Kategori
    Dim Pilih_Peruntukan
    Dim Pilih_KodeLawanTransaksi
    Dim Pilih_SaranaPencairan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_BukuPengawasanBuktiPenerimaanBankCash.JudulForm
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
        KontenCombo_SaranaPencairan()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData

    Async Sub TampilkanDataAsync()

        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Style Tabel :
            Terabas()
            datatabelUtama.Rows.Clear()

            'Data Tabel :
            Dim i = 0
            NomorUrut = 0
            AngkaKM_Sebelumnya = 0

            JumlahTagihan = 0
            JumlahBayar = 0
            JumlahInvoicePerBaris = 0

            Dim FilterData As String = Kosongan

            'Filter Kategori :
            Dim FilterKategori As String
            Select Case Pilih_Kategori
                Case Kosongan
                    FilterKategori = Spasi1
                Case Pilihan_Semua
                    FilterKategori = Spasi1
                Case Else
                    FilterKategori = " AND Kategori = '" & Pilih_Kategori & "' "
            End Select

            'Filter Peruntukan :
            Dim FilterPeruntukan As String
            Select Case Pilih_Peruntukan
                Case Kosongan
                    FilterPeruntukan = Spasi1
                Case Pilihan_Semua
                    FilterPeruntukan = Spasi1
                Case Else
                    FilterPeruntukan = " AND Peruntukan = '" & Pilih_Peruntukan & "' "
            End Select

            'Filter Lawan Transaksi :
            Dim FilterLawanTransaksi As String = Kosongan
            Select Case Pilih_KodeLawanTransaksi
                Case Kosongan
                    FilterLawanTransaksi = Spasi1
                Case Pilihan_Semua
                    FilterLawanTransaksi = Spasi1
                Case Else
                    FilterLawanTransaksi = " AND Kode_Lawan_Transaksi = '" & Pilih_KodeLawanTransaksi & "' "
            End Select

            'Filter Sarana Pembayaran :
            Dim FilterSaranaPencairan = Spasi1
            Select Case Pilih_SaranaPencairan
                Case Kosongan
                    FilterSaranaPencairan = Spasi1
                Case Pilihan_Semua
                    FilterSaranaPencairan = Spasi1
                Case Else
                    FilterSaranaPencairan = " AND COA_Debet = '" & Pilih_SaranaPencairan & "' "
            End Select

            FilterData = FilterKategori & FilterPeruntukan & FilterLawanTransaksi & FilterSaranaPencairan

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                  " WHERE DATE_FORMAT(Tanggal_KM, '%Y') = '" & TahunBukuAktif & "' " & FilterData &
                                  " ORDER BY Angka_KM ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                AngkaKM = dr.Item("Angka_KM")
                If i > 0 And AngkaKM <> AngkaKM_Sebelumnya Then TambahBaris()
                NomorKM = dr.Item("Nomor_KM")
                TanggalKM = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
                Peruntukan = dr.Item("Peruntukan")
                KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
                SaranaPencairan = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
                NomorBP = dr.Item("Nomor_BP")
                If Peruntukan = Peruntukan_PembayaranHutangBank _
                    Or Peruntukan = Peruntukan_PembayaranHutangLeasing _
                    Or Peruntukan = Peruntukan_PembayaranHutangAfiliasi _
                    Or Peruntukan = Peruntukan_PembayaranHutangPihakKetiga _
                    Then
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                Else
                    If NomorInvoice = Kosongan Then
                        NomorInvoice = dr.Item("Nomor_Invoice")
                    Else
                        NomorInvoice &= SlashGanda_Pemisah & Convert.ToChar(13) & dr.Item("Nomor_Invoice")
                    End If
                    If TanggalInvoice = Kosongan Then
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        If TanggalInvoice = TanggalKosong Then TanggalInvoice = Kosongan
                    Else
                        TanggalInvoice &= SlashGanda_Pemisah & Convert.ToChar(13) & TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    End If
                End If
                JumlahTagihan += dr.Item("Jumlah_Tagihan")
                JumlahBayar += dr.Item("Jumlah_Bayar")
                BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
                Denda = dr.Item("Denda")
                KodeAkun = dr.Item("COA_Kredit")
                NamaAkun = AmbilValue_NamaAkun(KodeAkun)
                Uraian = PenghapusEnter(dr.Item("Catatan"))
                NomorJV = dr.Item("Nomor_JV")
                User = dr.Item("User")
                AngkaKM_Sebelumnya = AngkaKM
                i += 1
                JumlahInvoicePerBaris += 1
                Await Task.Yield()
            Loop

            If i > 0 Then TambahBaris()

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanBuktiPenerimaanBankCash")

        Finally
            BersihkanSeleksi_SetelahLoading()

        End Try

    End Sub

    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        AmbilValueTambahan()
        If JumlahBayar > 0 Then
            JumlahBayar += Denda
            If Pembebanan = Pembebanan_Ditambahkan Then
                JumlahDiterima = JumlahBayar - (BiayaAdministrasiKontrak + BiayaNotaris)
            Else
                JumlahDiterima = JumlahBayar - (BiayaAdministrasiKontrak + BiayaNotaris + BiayaAdministrasiBank)
            End If
        End If
        datatabelUtama.Rows.Add(NomorUrut, AngkaKM_Sebelumnya, NomorKM, TanggalKM, Peruntukan, KodeLawanTransaksi, NamaLawanTransaksi,
                                JumlahInvoicePerBaris, SaranaPencairan, NomorBP,
                                NomorInvoice, TanggalInvoice, JumlahTagihan, JumlahBayar, JumlahDiterima,
                                KodeAkun, NamaAkun, Uraian, NomorJV, User)
        Dim i = NomorUrut - 1
        NomorInvoice = Kosongan
        TanggalInvoice = Kosongan
        JumlahTagihan = 0
        JumlahBayar = 0
        BiayaAdministrasiKontrak = 0
        BiayaNotaris = 0
        JumlahDiterima = 0
        JumlahInvoicePerBaris = 0
        Terabas()
    End Sub

    Sub AmbilValueTambahan()
        Dim AdaTelusuran As Boolean = True
        Select Case Peruntukan
            Case Peruntukan_HutangAfiliasi
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangAfiliasi " &
                               " WHERE Nomor_BPHA = '" & NomorBP & "' "
            Case Peruntukan_HutangKaryawan
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangKaryawan " &
                               " WHERE Nomor_BPHK = '" & NomorBP & "' "
            Case Peruntukan_HutangPemegangSaham
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangPemegangSaham " &
                               " WHERE Nomor_BPHPS = '" & NomorBP & "' "
            Case Peruntukan_HutangPihakKetiga
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
                               " WHERE Nomor_BPHPK = '" & NomorBP & "' "
            Case Peruntukan_HutangBank
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangBank " &
                               " WHERE Nomor_BPHB = '" & NomorBP & "' "
            Case Peruntukan_HutangLeasing
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangLeasing " &
                               " WHERE Nomor_BPHL = '" & NomorBP & "' "
            Case Else
                AdaTelusuran = False
        End Select
        If AdaTelusuran = True Then
            cmdTELUSUR = New OdbcCommand(QueryTelusur, KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If Peruntukan = Peruntukan_HutangBank _
                Or Peruntukan = Peruntukan_HutangLeasing _
                Then
                If drTELUSUR.HasRows Then
                    BiayaAdministrasiKontrak = drTELUSUR.Item("Biaya_Administrasi_Kontrak")
                    BiayaNotaris = drTELUSUR.Item("Biaya_Notaris")
                End If
            End If
            If Peruntukan = Peruntukan_HutangPihakKetiga _
                Or Peruntukan = Peruntukan_HutangKaryawan _
                Or Peruntukan = Peruntukan_HutangPemegangSaham _
                Or Peruntukan = Peruntukan_HutangAfiliasi _
                Then
                If drTELUSUR.HasRows Then
                    Pembebanan = drTELUSUR.Item("Pembebanan")
                End If
            End If
        End If
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_LihatBundelan.IsEnabled = False
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub

    Sub KontenCombo_Kategori()
        KontenCombo_KategoriPenerimaan_Public(cmb_Kategori)
        cmb_Kategori.Items.Add(Pilihan_Semua)
        cmb_Kategori.SelectedValue = Pilihan_Semua
    End Sub

    Sub Kontencombo_PeruntukanSemua()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub
    Sub KontenCombo_PeruntukanPencairanPiutang()
        KontenCombo_PeruntukanPencairanPiutang_Public(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanPenerimaanTunai()
        KontenCombo_PeruntukanPenerimaanTunai_Public(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanInvestasi()
        KontenCombo_PeruntukanInvestasi_Public(cmb_Peruntukan)
        cmb_Peruntukan.Items.Add(Pilihan_Semua)
        cmb_Peruntukan.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PeruntukanPengembalian()
        KontenCombo_PeruntukanPengembalian_Public(cmb_Peruntukan)
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

    Sub KontenCombo_SaranaPencairan()
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPencairan, KodeMataUang_Semua)
        cmb_SaranaPencairan.Items.Add(Pilihan_Semua)
        cmb_SaranaPencairan.SelectedValue = Pilihan_Semua
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_LihatBundelan_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatBundelan.Click
        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_LIHAT
        win_InputBuktiPenerimaan.JalurMasuk = JalurUtama
        win_InputBuktiPenerimaan.AngkaKM = AngkaKM_Terseleksi
        win_InputBuktiPenerimaan.ShowDialog()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.JalurMasuk = JalurUtama
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.AdaPenyimpanan Then TampilkanData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_EDIT
        win_InputBuktiPenerimaan.JalurMasuk = JalurUtama
        win_InputBuktiPenerimaan.AngkaKM = AngkaKM_Terseleksi
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.AdaPenyimpanan Then TampilkanData()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_General(Buka)
        AksesDatabase_Transaksi(Buka)

        'Hapus Data Bukti Penerimaan :
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPenerimaan " &
                                   " WHERE Angka_KM = '" & AngkaKM_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        'Hapus Data Jurnal :
        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        'Reset/Nol-kan (0) value NomorJV pada Buku Pengawasan Terkait :
        Dim TabelPengawasan = Kosongan
        Dim KolomNomorJV = "Nomor_JV"
        Dim PengosonganDataTambahan = Kosongan
        Dim KoneksiDatabaseTemporal As OdbcConnection = KoneksiDatabaseTransaksi
        Select Case Peruntukan_Terseleksi
            'PENCAIRAN PIUTANG : =======================================================
            Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                'Belum ada kebutuhan Coding untuk Case ini
            Case Peruntukan_PencairanPiutangUsaha_Afiliasi
                'Belum ada kebutuhan Coding untuk Case ini
            Case Peruntukan_PencairanPiutangAfiliasi
                TabelPengawasan = "tbl_JadwalAngsuranPiutangAfiliasi"
                PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
            Case Peruntukan_PencairanPiutangPihakKetiga
                TabelPengawasan = "tbl_JadwalAngsuranPiutangPihakKetiga"
                PengosonganDataTambahan =
                    ", Tanggal_Bayar    = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Denda            = 0 "
            Case Peruntukan_PencairanPiutangPemegangSaham
                'Belum ada kebutuhan Coding untuk Case ini
            Case Peruntukan_PencairanPiutangDividen
                'Belum ada kebutuhan Coding untuk Case ini
            Case Peruntukan_PencairanPiutangKaryawan
                'Belum ada kebutuhan Coding untuk Case ini
            Case Peruntukan_PencairanPiutangLainnya
                PesanUntukProgrammer("Value NomorJV pada Tabel Pengawasan belum di-RESET Ke Nol (0)...!!!!!!!")
            'PENERIMAAN TUNAI : ========================================================
            Case Peruntukan_InvoiceTunai
                TabelPengawasan = "tbl_Penjualan_Invoice"
            Case Peruntukan_HutangAfiliasi
                TabelPengawasan = "tbl_PengawasanHutangAfiliasi"
            Case Peruntukan_HutangKaryawan
                TabelPengawasan = "tbl_PengawasanHutangKaryawan"
            Case Peruntukan_HutangPemegangSaham
                TabelPengawasan = "tbl_PengawasanHutangPemegangSaham"
            Case Peruntukan_HutangPihakKetiga
                TabelPengawasan = "tbl_PengawasanHutangPihakKetiga"
            Case Peruntukan_HutangBank
                TabelPengawasan = "tbl_PengawasanHutangBank"
                KolomNomorJV = "Nomor_JV_Pencairan"
                PengosonganDataTambahan =
                    ", Tanggal_Pencairan            = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Biaya_Administrasi_Kontrak   = 0, " &
                    "  Biaya_Notaris                = 0, " &
                    "  Biaya_PPh                    = 0 "
            Case Peruntukan_HutangLeasing
                TabelPengawasan = "tbl_PengawasanHutangLeasing"
                KolomNomorJV = "Nomor_JV_Pencairan"
                PengosonganDataTambahan =
                    ", Tanggal_Pencairan            = '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                    "  Biaya_Administrasi_Kontrak   = 0, " &
                    "  Biaya_Notaris                = 0, " &
                    "  Biaya_PPh                    = 0 "
            Case Peruntukan_InvestasiModal
                TabelPengawasan = "tbl_Modal"
                KoneksiDatabaseTemporal = KoneksiDatabaseGeneral
            Case Else
                PesanUntukProgrammer("Value NomorJV pada Tabel Pengawasan belum di-RESET Ke Nol (0)...!!!!!!!")
        End Select

        If TabelPengawasan <> Kosongan Then
            cmdHAPUS = New OdbcCommand(" UPDATE " & TabelPengawasan &
                                       " SET    " & KolomNomorJV & " = 0 " &
                                       PengosonganDataTambahan & " WHERE  " & KolomNomorJV & " = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTemporal)
            cmdHAPUS_ExecuteNonQuery()
        End If

        If Peruntukan = Peruntukan_InvestasiModal Then HapusDataTabel_BerdasarkanNomorJV_dbGeneral("tbl_Modal", 0) '<--- Hapus Data di Buku Pengawasan Modal

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
        FiturBelumBisaDigunakan()
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Private Sub cmb_Kategori_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kategori.SelectionChanged
        Pilih_Kategori = cmb_Kategori.SelectedValue
        Select Case Pilih_Kategori
            Case Pilihan_Semua
                Kontencombo_PeruntukanSemua()
            Case Kategori_PencairanPiutang
                KontenCombo_PeruntukanPencairanPiutang()
            Case Kategori_PenerimaanTunai
                KontenCombo_PeruntukanPenerimaanTunai()
            Case Kategori_Investasi
                KontenCombo_PeruntukanInvestasi()
            Case Kategori_Pengembalian
                KontenCombo_PeruntukanPengembalian()
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


    Private Sub cmb_SaranaPencairan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPencairan.SelectionChanged
        Pilih_SaranaPencairan = KonversiSaranaPembayaranKeCOA(cmb_SaranaPencairan.SelectedValue)
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
        AngkaKM_Terseleksi = AmbilAngka(rowviewUtama("Angka_KM").ToString)
        NomorKM_Terseleksi = rowviewUtama("Nomor_KM")
        TanggalKM_Terseleksi = rowviewUtama("Tanggal_KM")
        Peruntukan_Terseleksi = rowviewUtama("Peruntukan_")
        KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = rowviewUtama("Nama_Lawan_Transaksi")
        JumlahInvoicePerBaris_Terseleksi = rowviewUtama("Jumlah_Invoice")
        SaranaPencairan_Terseleksi = rowviewUtama("Sarana_Pencairan")
        COAKredit_Terseleksi = KonversiSaranaPembayaranKeCOA(SaranaPencairan_Terseleksi)
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        JumlahTagihan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Tagihan").ToString)
        JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar").ToString)
        JumlahDiterima_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Diterima").ToString)
        KodeAkun_Terseleksi = rowviewUtama("COA_Debet")
        NamaAkun_Terseleksi = rowviewUtama("Nama_Akun")
        Uraian_Terseleksi = rowviewUtama("Uraian_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV").ToString)
        User_Terseleksi = rowviewUtama("User_")

        If AngkaKM_Terseleksi > 0 Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            btn_LihatBundelan.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            btn_LihatBundelan.IsEnabled = False
        End If

        If NomorJV_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_LihatBundelan_Click(sender, e)
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
    Dim Angka_KM As New DataGridTextColumn
    Dim Nomor_KM As New DataGridTextColumn
    Dim Tanggal_KM As New DataGridTextColumn
    Dim Peruntukan_ As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Jumlah_Invoice As New DataGridTextColumn
    Dim Sarana_Pencairan As New DataGridTextColumn
    Dim Nomor_BP As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Jumlah_Tagihan As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Jumlah_Diterima As New DataGridTextColumn
    Dim COA_Debet As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Uraian_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Angka_KM")
        datatabelUtama.Columns.Add("Nomor_KM")
        datatabelUtama.Columns.Add("Tanggal_KM")
        datatabelUtama.Columns.Add("Peruntukan_")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Invoice", GetType(Int64))
        datatabelUtama.Columns.Add("Sarana_Pencairan")
        datatabelUtama.Columns.Add("Nomor_BP")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Jumlah_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Diterima", GetType(Int64))
        datatabelUtama.Columns.Add("COA_Debet")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Uraian_")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_KM, "Angka_KM", "Angka KM", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_KM, "Nomor_KM", "Nomor Bukti", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_KM, "Tanggal_KM", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Peruntukan_, "Peruntukan_", "Peruntukan", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Lawan Transaksi", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Invoice, "Jumlah_Invoice", "Jumlah Berkas", 63, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sarana_Pencairan, "Sarana_Pencairan", "Sarana Pencairan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BP, "Nomor_BP", "Nomor BP", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Tagihan, "Jumlah_Tagihan", "Jumlah Tagihan", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Cair", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Diterima, "Jumlah_Diterima", "Jumlah Diterima", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Debet, "COA_Debet", "COA Debet", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_, "Uraian_", "Uraian", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
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
