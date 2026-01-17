Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls

Public Class wpfWin_InputAmortisasiBiaya

    Public FungsiForm
    Public JudulForm
    Public JalurMasuk
    Public IdAmortisasi As Integer
    Dim TahunTransaksi_String As String
    Dim TahunTransaksi As Integer
    Dim BulanTransaksi_String As String
    Dim TahunMulaiAmortisasi As Integer
    Public KodeAsset As String
    Public KodeAsset_SebelumDiedit

    Dim COAAmortisasi
    Dim NamaAkun_Amortisasi
    Dim NamaProduk
    Dim COABiaya
    Dim NamaAkun_Biaya
    Dim MasaAmortisasi
    Dim TanggalTransaksi
    Dim TanggalMulai
    Dim JumlahTransaksi
    Dim AmortisasiPerBulan = JumlahTransaksi / MasaAmortisasi
    Dim Keterangan

    Dim JumlahProduk

    Dim EksekusiTutupForm As Boolean
    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case FungsiForm
            Case FungsiForm_EDIT
                JudulForm = "Edit Data Amortisasi Biaya"
                IsiValueForm()
            Case FungsiForm_TAMBAH
                JudulForm = "Input Data Amortisasi Biaya"
            Case Else
                PesanUntukProgrammer("Tentukan Status Form Dulu..!")
                Close()
                Return 'Jangan dihapusss...!!!
        End Select

        If JalurMasuk = Halaman_AMORTISASIBIAYA Then
            lbl_KodeAsset.Visibility = Visibility.Visible
            txt_KodeAsset.Visibility = Visibility.Visible
            lbl_NamaProduk.Visibility = Visibility.Visible
            txt_NamaProduk.Visibility = Visibility.Visible
            txt_JumlahTransaksi.IsEnabled = True
            lbl_JumlahTransaksi.Visibility = Visibility.Visible
            txt_JumlahTransaksi.Visibility = Visibility.Visible
            lbl_TanggalTransaksi.Visibility = Visibility.Visible
            dtp_TanggalTransaksi.Visibility = Visibility.Visible
            grb_AkunAmortisasi.IsEnabled = True
            btn_Batal.IsEnabled = True
            btn_Simpan.Content = teks_Simpan
        End If

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            EksekusiTutupForm = False
            txt_KodeAsset.Text = Kosongan
            lbl_KodeAsset.Visibility = Visibility.Collapsed
            txt_KodeAsset.Visibility = Visibility.Collapsed
            lbl_NamaProduk.Visibility = Visibility.Collapsed
            txt_NamaProduk.Visibility = Visibility.Collapsed
            lbl_JumlahTransaksi.Visibility = Visibility.Collapsed
            txt_JumlahTransaksi.Visibility = Visibility.Collapsed
            lbl_TanggalTransaksi.Visibility = Visibility.Collapsed
            dtp_TanggalTransaksi.Visibility = Visibility.Collapsed
            grb_AkunAmortisasi.IsEnabled = False
            btn_Batal.Visibility = Visibility.Collapsed
            btn_Simpan.Content = teks_Tambahkan_
        End If

        If FungsiForm = Kosongan Then
            PesanUntukProgrammer("Tentukan Fungsi Form Dulu..!")
            Close()
            Return 'Jangan dihapusss...!!!
        End If

        If JalurMasuk = Kosongan Then
            PesanUntukProgrammer("Tentukan Jalur Masuk dulu..!")
            Close()
            Return 'Jangan dihapusss...!!!
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        EksekusiTutupForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        txt_KodeAsset.Text = "[ Otomatis ]"
        txt_COA_Amortisasi.Text = Kosongan
        txt_NamaAkun_Amortisasi.Text = Kosongan
        txt_NamaProduk.Text = Kosongan
        txt_COA_Biaya.Text = Kosongan
        txt_NamaAkun_Biaya.Text = Kosongan
        txt_MasaAmortisasi.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalTransaksi)
        txt_JumlahTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)
        btn_Simpan.Content = teks_Simpan
        btn_Batal.Content = teks_Batal
        SistemPenomoranOtomatis_KodeAsset() 'Ini sudah benar di posisi paling bawah. Jangan dipindah.

        ProsesResetForm = False

    End Sub


    Sub IsiValueForm()

    End Sub


    Sub SistemPenomoranOtomatis_KodeAsset()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_AmortisasiBiaya WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_AmortisasiBiaya ) ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            IdAmortisasi = dr.Item("Nomor_ID") + 1
        Else
            IdAmortisasi = 1
        End If
        AksesDatabase_General(Tutup)
        KodeAsset_Value()
    End Sub

    Sub KodeAsset_Value()
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            TahunTransaksi_String = dtp_TanggalTransaksi.SelectedDate.Value.Year
            BulanTransaksi_String = dtp_TanggalTransaksi.SelectedDate.Value.Month
            KodeAsset = txt_COA_Amortisasi.Text & "-" & TahunTransaksi_String & "-" & BulanTransaksi_String & "-" & IdAmortisasi
            txt_KodeAsset.Text = KodeAsset
            If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then txt_KodeAsset.Text = Kosongan
        End If
    End Sub



    Private Sub txt_KodeAsset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAsset.TextChanged
        KodeAsset = txt_KodeAsset.Text
    End Sub

    Private Sub txt_COA_Amortisasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA_Amortisasi.TextChanged
        COAAmortisasi = txt_COA_Amortisasi.Text
        txt_NamaAkun_Amortisasi.Text = AmbilValue_NamaAkun(COAAmortisasi)
        KodeAsset_Value()
    End Sub
    Sub PenentuanCOA_BiayaAmortisasi()
        Select Case COAAmortisasi
            Case KodeTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53118' OR " &
                    " COA = '53119' OR " &
                    " COA = '61501' ) "
            Case KodeTautanCOA_SewaMesinDanPeralatanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53111' OR " &
                    " COA = '61502' ) "
            Case KodeTautanCOA_SewaKendaraanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53112' OR " &
                    " COA = '61503' ) "
            Case KodeTautanCOA_BiayaRenovasiDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53120' OR " &
                    " COA = '61404' ) "
            Case KodeTautanCOA_BiayaPendirianPerusahaan
                FilterListCOA_BiayaAmortisasi = " AND COA = '61214' "
            Case KodeTautanCOA_AsuransiDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '52110' OR " &
                    " COA = '53115' OR " &
                    " COA = '53116' OR " &
                    " COA = '53117' OR " &
                    " COA = '61110' OR " &
                    " COA = '61210' OR " &
                    " COA = '61211' OR " &
                    " COA = '61212' ) "
            Case KodeTautanCOA_SewaAssetLainnyaDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND COA = '61506' "
        End Select
    End Sub

    Private Sub btn_PilihCOA_Amortisasi_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_Amortisasi.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Amortisasi
        If txt_COA_Amortisasi.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_Amortisasi.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Amortisasi.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COA_Amortisasi.Text = win_ListCOA.COATerseleksi
    End Sub

    Private Sub txt_NamaAkun_Amortisasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_Amortisasi.TextChanged
        NamaAkun_Amortisasi = txt_NamaAkun_Amortisasi.Text
    End Sub

    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub

    Private Sub txt_COA_Biaya_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA_Biaya.TextChanged
        COABiaya = txt_COA_Biaya.Text
        txt_NamaAkun_Biaya.Text = AmbilValue_NamaAkun(COABiaya)
    End Sub

    Private Sub btn_PilihCOA_Biaya_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_Biaya.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_BiayaAmortisasi
        If txt_COA_Biaya.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_Biaya.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Biaya.Text
        End If
        PenentuanCOA_BiayaAmortisasi()
        win_ListCOA.ShowDialog()
        txt_COA_Biaya.Text = win_ListCOA.COATerseleksi
        FilterListCOA_BiayaAmortisasi = Kosongan '(Reset)
    End Sub

    Private Sub txt_NamaAkun_Biaya_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_Biaya.TextChanged
        NamaAkun_Biaya = txt_NamaAkun_Biaya.Text
    End Sub

    Private Sub txt_MasaAmortisasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_MasaAmortisasi.TextChanged
        MasaAmortisasi = AmbilAngka(txt_MasaAmortisasi.Text)
    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
            KodeAsset_Value()
            TahunTransaksi_String = dtp_TanggalTransaksi.SelectedDate.Value.Year
            TahunTransaksi = AmbilAngka(TahunTransaksi_String)
        End If
    End Sub

    Private Sub dtp_TanggalMulai_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalMulai.SelectedDateChanged
        If dtp_TanggalMulai.Text <> Kosongan Then
            If dtp_TanggalTransaksi.Text = Kosongan Then
                PesanPeringatan("Silakan isi 'Tanggal Transaksi' terlebih dahulu.")
                dtp_TanggalMulai.Text = Kosongan
                dtp_TanggalTransaksi.Focus()
                Return
            End If
            TahunMulaiAmortisasi = dtp_TanggalMulai.SelectedDate.Value.Year
            If (TahunMulaiAmortisasi - TahunTransaksi) > 1 Then
                PesanPeringatan("Jarak antara 'Tanggal Transaksi' dan 'Tanggal Mulai Amortisasi' terlalu jauh." & Enter2Baris & "Silakan isi dengan benar.")
                dtp_TanggalMulai.Text = Kosongan
                dtp_TanggalTransaksi.Focus()
                Return
            End If
            KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(dtp_TanggalMulai, AmbilHari_DariTanggal(TanggalTransaksi), AmbilBulanAngka_DariTanggal(TanggalTransaksi), AmbilTahun_DariTanggal(TanggalTransaksi))
            TanggalMulai = TanggalFormatTampilan(dtp_TanggalMulai.SelectedDate)
            dtp_TanggalMulai.SelectedDate = TanggalFormatWPF(AmbilTanggalAkhirBulan_BerdasarkanTanggalLengkap(TanggalMulai))
            TanggalMulai = TanggalFormatTampilan(dtp_TanggalMulai.SelectedDate)
        End If
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahTransaksi)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If txt_COA_Biaya.Text = Kosongan Then
                PesanPeringatan("Silakan pilih 'Akun Biaya'.")
                txt_COA_Biaya.Focus()
                Return
            End If
            If dtp_TanggalMulai.Text = Kosongan Then
                PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalMulai, "Tanggal Mulai Amortisasi")
                Return
            End If
            If MasaAmortisasi < 2 Then
                PesanPeringatan("Masa amortisasi jangan kurang dari 2 bulan.")
                txt_MasaAmortisasi.Focus()
                Return
            End If
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = txt_COA_Biaya.Text
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Tanggal_Mulai_Amortisasi") = TanggalFormatSimpan(dtp_TanggalMulai.SelectedDate)
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = txt_MasaAmortisasi.Text
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = Kosongan
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = Kosongan
            EksekusiTutupForm = True
            Me.Close()
            Return 'Jangan dihapussss!!!!!
        End If

        AmortisasiPerBulan = JumlahTransaksi / MasaAmortisasi

        If COAAmortisasi = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Kode Akun Amortisasi'.")
            txt_COA_Amortisasi.Focus()
            Return
        End If

        If NamaProduk = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaProduk, "Nama Produk")
            Return
        End If

        If COABiaya = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Kode Akun Biaya'.")
            txt_COA_Biaya.Focus()
            Return
        End If

        If AmbilAngka(txt_MasaAmortisasi.Text) = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Masa Amortisasi'.")
            txt_MasaAmortisasi.Focus()
            Return
        End If

        If MasaAmortisasi < 2 Then
            PesanPeringatan("Masa amortisasi jangan kurang dari 2 bulan.")
            txt_MasaAmortisasi.Focus()
            Return
        End If


        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Perolehan")
            Return
        End If

        If dtp_TanggalMulai.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalMulai, "Tanggal Mulai Amortisasi")
            Return
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            If TahunTransaksi_String > TahunCutOff Then
                Pesan_Peringatan("Untuk 'Transaksi' setelah 'Tanggal Cut Off' (31-12-" & TahunCutOff & "), silakan diinput sesuai Tahun Bukunya masing-masing. ")
                Return
            End If
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TahunTransaksi_String <> TahunBukuAktif Then
                Pesan_Peringatan("'Tahun Transaksi' tidak sesuai dengan 'Tahun Buku Aktif'")
                Return
            End If
        End If

        If txt_JumlahTransaksi.Text = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        'TrialBalance_Mentahkan()

        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_AmortisasiBiaya VALUES ( " &
                                  " '" & IdAmortisasi & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & COAAmortisasi & "', " &
                                  " '" & NamaAkun_Amortisasi & "', " &
                                  " '" & NamaProduk & "', " &
                                  " '" & COABiaya & "', " &
                                  " '" & NamaAkun_Biaya & "', " &
                                  " '" & MasaAmortisasi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalMulai) & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & Keterangan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then

            'Edit Data Amortisasi Biaya :
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" UPDATE tbl_AmortisasiBiaya SET " &
                                  " Kode_Asset              = '" & KodeAsset & "', " &
                                  " COA_Amortisasi          = '" & COAAmortisasi & "', " &
                                  " Nama_Akun_Amortisasi    = '" & NamaAkun_Amortisasi & "', " &
                                  " Nama_Produk             = '" & NamaProduk & "', " &
                                  " COA_Biaya               = '" & COABiaya & "', " &
                                  " Nama_Akun_Biaya         = '" & NamaAkun_Biaya & "', " &
                                  " Masa_Amortisasi         = '" & MasaAmortisasi & "', " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " Tanggal_Mulai           = '" & TanggalFormatSimpan(TanggalMulai) & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Keterangan & "' " &
                                  " WHERE Nomor_ID          = '" & IdAmortisasi & "' ",
                                  KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal :
            If StatusSuntingDatabase = True Then
                'Hapus Data Jurnal :
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE from tbl_Transaksi " &
                                      " WHERE Bundelan = '" & KodeAsset_SebelumDiedit & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_DaftarAmortisasiBiaya.StatusAktif Then usc_DaftarAmortisasiBiaya.TampilkanData()
            Close()
            Return 'Jangan dihapus....!!!
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Close()
    End Sub

    Private Sub TutupForm(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If ProsesKeluarAplikasi Then Return
        If Not EksekusiTutupForm Then
            If Not TanyaKonfirmasi("Yakin ingin menutup form ini?") Then
                e.Cancel = True
                Return
            End If
            If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
                If txt_COA_Biaya.Text = Kosongan Or txt_MasaAmortisasi.Text = Kosongan Then
                    PesanPeringatan("Anda belum melengkapi 'Data Amortisasi'." & Enter2Baris & "COA Amortisasi dibatalkan..!")
                    win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Produk") = Kosongan
                    win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = Kosongan
                    win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Tanggal_Mulai_Amortisasi") = Kosongan
                    win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = Kosongan
                End If
            End If
        End If
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeAsset.IsReadOnly = True
        txt_COA_Amortisasi.IsReadOnly = True
        txt_NamaAkun_Amortisasi.IsHitTestVisible = True
        txt_COA_Biaya.IsReadOnly = True
        txt_NamaAkun_Biaya.IsReadOnly = True
        MaxWidth = StandarLebarLayar
        MaxHeight = StandarTinggiLayar
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub

End Class
