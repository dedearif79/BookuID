Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputDataAsset

    Public FungsiForm
    Public JudulForm
    Public JalurMasuk

    Public IdAsset As Integer
    Dim KodeDivisi As String
    Dim TahunPerolehan As String
    Dim BulanPerolehan As String
    Public KodeAsset_SebelumDiedit

    Public KodeAsset As String
    Public NomorPembelian As String
    Dim NamaAktiva
    Dim COA_Asset
    Dim COA_AkumulasiPenyusutan
    Dim COA_BiayaPenyusutan
    Dim KelompokHarta
    Dim MasaManfaat
    Dim Divisi
    Dim TanggalPerolehan
    Dim HargaPerolehan
    Dim Keterangan

    Dim EksekusiTutupForm As Boolean
    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Asset"
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Asset"
        End If

        If JalurMasuk = Halaman_DATAASSETTETAP Then
            txt_KodeAsset.Visibility = Visibility.Visible
            txt_NamaAktiva.Visibility = Visibility.Visible
            dtp_TanggalPerolehan.Visibility = Visibility.Visible
            txt_HargaPerolehan.Visibility = Visibility.Visible
            lbl_KodeAsset.Visibility = Visibility.Visible
            lbl_NamaAktiva.Visibility = Visibility.Visible
            lbl_TanggalPerolehan.Visibility = Visibility.Visible
            lbl_HargaPerolehan.Visibility = Visibility.Visible
            btn_Batal.Visibility = Visibility.Visible
            btn_Simpan.Content = teks_Simpan
        End If

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            EksekusiTutupForm = False
            txt_KodeAsset.Text = Kosongan
            txt_KodeAsset.Visibility = Visibility.Collapsed
            txt_NamaAktiva.Visibility = Visibility.Collapsed
            dtp_TanggalPerolehan.Visibility = Visibility.Collapsed
            txt_HargaPerolehan.Visibility = Visibility.Collapsed
            lbl_KodeAsset.Visibility = Visibility.Collapsed
            lbl_NamaAktiva.Visibility = Visibility.Collapsed
            lbl_TanggalPerolehan.Visibility = Visibility.Collapsed
            lbl_HargaPerolehan.Visibility = Visibility.Collapsed
            btn_Batal.Visibility = Visibility.Visible
            btn_Batal.Visibility = Visibility.Collapsed
            btn_Simpan.Content = teks_Tambahkan_
        End If

        If FungsiForm = Kosongan Then
            PesanUntukProgrammer("Tentukan Fungsi Form Dulu..!")
            Close()
            Return 'Jangan dihapus....!!!
        End If

        If JalurMasuk = Kosongan Then
            PesanUntukProgrammer("Tentukan Jalur Masuk dulu..!")
            Close()
            Return 'Jangan dihapus....!!!
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        EksekusiTutupForm = True
        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        NomorPembelian = Kosongan
        KontenComboKelompokHarta()
        grb_AkunAsset.IsEnabled = False
        grb_AkunBiayaPenyusutan.IsEnabled = False
        grb_AkunAkumulasiPenyusutan.IsEnabled = False
        txt_KodeAsset.Text = "[ Otomatis ]"
        txt_NamaAktiva.Text = Kosongan
        txt_COA_Asset.Text = Kosongan
        txt_NamaAkun_Asset.Text = Kosongan
        txt_COA_BiayaPenyusutan.Text = Kosongan
        txt_NamaAkun_BiayaPenyusutan.Text = Kosongan
        txt_MasaManfaat.Text = Kosongan
        txt_TarifPenyusutan.Text = Kosongan
        KontenComboDivisi()
        KodeDivisi = Kosongan
        KosongkanDatePicker(dtp_TanggalPerolehan)
        txt_HargaPerolehan.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)
        btn_Simpan.Content = teks_Simpan
        btn_Batal.Content = teks_Batal
        SistemPenomoranOtomatis_KodeAsset() 'Ini sudah benar di posisi paling bawah. Jangan dipindah.

        ProsesResetForm = False

    End Sub

    Sub KontenComboKelompokHarta()
        cmb_KelompokHarta.Items.Clear()
        cmb_KelompokHarta.Items.Add(KelompokHarta_1)
        cmb_KelompokHarta.Items.Add(KelompokHarta_2)
        cmb_KelompokHarta.Items.Add(KelompokHarta_3)
        cmb_KelompokHarta.Items.Add(KelompokHarta_4)
        cmb_KelompokHarta.Items.Add(KelompokHarta_BangunanPermanen)
        cmb_KelompokHarta.Items.Add(KelompokHarta_BangunanTidakPermanen)
        cmb_KelompokHarta.Items.Add(KelompokHarta_Tanah)
        cmb_KelompokHarta.Text = Kosongan
    End Sub

    Sub KontenComboDivisi()
        cmb_Divisi.Items.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            cmb_Divisi.Items.Add(dr.Item("Kode_Divisi") & " - " & dr.Item("Divisi"))
        Loop
        AksesDatabase_General(Tutup)
        cmb_Divisi.Text = Kosongan
    End Sub


    Public Sub SistemPenomoranOtomatis_KodeAsset()
        IdAsset = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataAsset") + 1
        KodeAsset_Value()
    End Sub




    Private Sub txt_KodeAsset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAsset.TextChanged
        KodeAsset = txt_KodeAsset.Text
    End Sub

    Private Sub txt_KodeAsset_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_KodeAsset.LostFocus
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeAsset.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim NamaMitra = dr.Item("Nama_Mitra")
            MsgBox("Kode '" & txt_KodeAsset.Text & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaMitra & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
            txt_KodeAsset.Text = Kosongan
            txt_KodeAsset.Focus()
            AksesDatabase_General(Tutup)
            Return
        End If
        AksesDatabase_General(Tutup)
    End Sub
    Sub KodeAsset_Value()
        If dtp_TanggalPerolehan.Text <> Kosongan Then
            If KodeDivisi = Kosongan Then
                KodeDivisi = "000"
            End If
            TahunPerolehan = dtp_TanggalPerolehan.SelectedDate.Value.Year
            BulanPerolehan = dtp_TanggalPerolehan.SelectedDate.Value.Month
            KodeAsset = COA_Asset & "-" & KodeDivisi & "-" & TahunPerolehan & "-" & BulanPerolehan & "-" & IdAsset
            txt_KodeAsset.Text = KodeAsset
        End If
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then txt_KodeAsset.Text = Kosongan
    End Sub


    Private Sub txt_NamaAktiva_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAktiva.TextChanged
        NamaAktiva = txt_NamaAktiva.Text
    End Sub


    Private Sub cmb_KelompokHarta_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KelompokHarta.SelectionChanged
        KelompokHarta = cmb_KelompokHarta.SelectedValue
        If JalurMasuk = Halaman_DATAASSETTETAP Then
            txt_COA_Asset.Text = Kosongan
            txt_COA_BiayaPenyusutan.Text = Kosongan
        End If
        txt_MasaManfaat.Text = KonversiKelompokHartaKeMasaManfaat(KelompokHarta) & " Tahun"
        If KelompokHarta <> Kosongan And JalurMasuk = Halaman_DATAASSETTETAP Then
            grb_AkunAsset.IsEnabled = True
        End If
        If KelompokHarta = KelompokHarta_Tanah Then
            txt_TarifPenyusutan.Text = "0 %"
        Else
            txt_TarifPenyusutan.Text = (100 / AmbilAngka(txt_MasaManfaat.Text)).ToString & " %"
        End If
    End Sub


    Private Sub txt_COA_Asset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA_Asset.TextChanged
        COA_Asset = txt_COA_Asset.Text
        txt_NamaAkun_Asset.Text = AmbilValue_NamaAkun(COA_Asset)
        If KelompokHarta <> KelompokHarta_Tanah Then
            txt_COA_BiayaPenyusutan.Text = PenentuanCOA_BiayaPenyusutan(COA_Asset)
            txt_COA_AkumulasiPenyusutan.Text = PenentuanCOA_AkumulasiPenyusutan(COA_Asset)
        End If
        KodeAsset_Value()
    End Sub
    Private Sub btn_PilihCOA_Asset_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_Asset.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        If KelompokHarta = KelompokHarta_Tanah Then
            win_ListCOA.ListAkun = ListAkun_AssetTanah
        Else
            win_ListCOA.ListAkun = ListAkun_AssetTetap_SelainTanah
        End If
        If txt_COA_Asset.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_Asset.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Asset.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COA_Asset.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_Asset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_Asset.TextChanged
    End Sub


    Private Sub txt_COA_BiayaPenyusutan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA_BiayaPenyusutan.TextChanged
        COA_BiayaPenyusutan = txt_COA_BiayaPenyusutan.Text
        txt_NamaAkun_BiayaPenyusutan.Text = AmbilValue_NamaAkun(COA_BiayaPenyusutan)
    End Sub
    Private Sub btn_PilihCOA_BiayaPenyusutan_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_BiayaPenyusutan.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_BiayaPenyusutan
        If txt_COA_BiayaPenyusutan.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_BiayaPenyusutan.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_BiayaPenyusutan.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COA_BiayaPenyusutan.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_BiayaPenyusutan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_BiayaPenyusutan.TextChanged
    End Sub


    Private Sub txt_COA_AkumulasiPenyusutan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA_AkumulasiPenyusutan.TextChanged
        COA_AkumulasiPenyusutan = txt_COA_AkumulasiPenyusutan.Text
        txt_NamaAkun_AkumulasiPenyusutan.Text = AmbilValue_NamaAkun(COA_AkumulasiPenyusutan)
    End Sub
    Private Sub btn_PilihCOA_AkumulasiPenyusutan_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_AkumulasiPenyusutan.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_AkumulasiPenyusutan
        If txt_COA_AkumulasiPenyusutan.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_AkumulasiPenyusutan.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_AkumulasiPenyusutan.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COA_AkumulasiPenyusutan.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_AkumulasiPenyusutan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_AkumulasiPenyusutan.TextChanged
    End Sub


    Private Sub txt_MasaManfaat_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_MasaManfaat.TextChanged
        MasaManfaat = AmbilAngka(txt_MasaManfaat.Text)
    End Sub


    Private Sub txt_TarifPenyusutan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPenyusutan.TextChanged

    End Sub


    Private Sub cmb_Divisi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Divisi.SelectionChanged
        Divisi = AmbilTeksTengahTakTerbatas(cmb_Divisi.SelectedValue, 7)
        KodeDivisi = AmbilTeksKiri(cmb_Divisi.SelectedValue, 3)
        KodeAsset_Value()
    End Sub


    Private Sub dtp_TanggalPerolehan_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPerolehan.SelectedDateChanged
        If dtp_TanggalPerolehan.Text <> Kosongan Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_TanggalPerolehan)
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalPerolehan)
            TanggalPerolehan = TanggalFormatTampilan(dtp_TanggalPerolehan.SelectedDate)
            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    If dtp_TanggalPerolehan.SelectedDate.Value.Year > TahunBukuAktif Then
                        dtp_TanggalPerolehan.SelectedDate = New Date(TahunBukuAktif, dtp_TanggalPerolehan.SelectedDate.Value.Month, dtp_TanggalPerolehan.SelectedDate.Value.Day)
                    End If
                Case JenisTahunBuku_NORMAL
                    If dtp_TanggalPerolehan.SelectedDate.Value.Year <> TahunBukuAktif Then
                        dtp_TanggalPerolehan.SelectedDate = New Date(TahunBukuAktif, dtp_TanggalPerolehan.SelectedDate.Value.Month, dtp_TanggalPerolehan.SelectedDate.Value.Day)
                    End If
            End Select
            TahunPerolehan = dtp_TanggalPerolehan.SelectedDate.Value.Year
            KodeAsset_Value()
        End If
    End Sub


    Private Sub txt_HargaPerolehan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HargaPerolehan.TextChanged
        HargaPerolehan = AmbilAngka(txt_HargaPerolehan.Text)
        PemecahRibuanUntukTextBox_WPF(txt_HargaPerolehan)
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If cmb_KelompokHarta.SelectedValue = Kosongan Then
                PesanPeringatan("Silakan pilih kolom 'Kelompok Harta'.")
                cmb_KelompokHarta.Focus()
                Return
            End If
            If cmb_Divisi.SelectedValue = Kosongan Then
                PesanPeringatan("Silakan pilih kolom 'Divisi'.")
                cmb_Divisi.Focus()
                Return
            End If
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = KonversiKelompokHartaKeAngka(cmb_KelompokHarta.SelectedValue)
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = AmbilTeksKiri(cmb_Divisi.SelectedValue, 3)
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = Kosongan
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = Kosongan
            EksekusiTutupForm = True
            Close()
            Return 'Ini jangan dihapus....!!!!
        End If

        If NamaAktiva = Kosongan Then
            MsgBox("Silakan isi kolom 'Nama Aktiva'.")
            txt_NamaAktiva.Focus()
            Return
        End If

        If KelompokHarta = Kosongan Then
            MsgBox("Silakan pilih 'Kelompok Harta'.")
            cmb_KelompokHarta.Focus()
            Return
        End If

        If COA_Asset = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Asset'.")
            txt_COA_Asset.Focus()
            Return
        End If

        If grb_AkunBiayaPenyusutan.IsEnabled = True And COA_BiayaPenyusutan = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Biaya Penyusutan'.")
            txt_COA_BiayaPenyusutan.Focus()
            Return
        End If

        If grb_AkunAkumulasiPenyusutan.IsEnabled = True And COA_AkumulasiPenyusutan = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Akumulasi Penyusutan'.")
            txt_COA_AkumulasiPenyusutan.Focus()
            Return
        End If

        If KodeDivisi = Kosongan Then
            MsgBox("Silakan pilih 'Divisi'.")
            cmb_Divisi.Focus()
            Return
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            If TahunPerolehan > TahunCutOff Then
                MsgBox("Untuk 'Transaksi' setelah 'Tanggal Cut Off' (31-12-" & TahunCutOff & "), silakan diinput sesuai Tahun Bukunya masing-masing. ")
                Return
            End If
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TahunPerolehan <> TahunBukuAktif Then
                MsgBox("'Tahun Transaksi' tidak sesuai dengan 'Tahun Buku Aktif'")
                Return
            End If
        End If

        If dtp_TanggalPerolehan.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalPerolehan, "Tanggal Perolehan")
            Return
        End If

        If HargaPerolehan = 0 Then
            MsgBox("Silakan isi kolom 'Harga Perolehan'.")
            txt_HargaPerolehan.Focus()
            Return
        End If

        Dim KelompokHarta_Angka = KonversiKelompokHartaKeAngka(KelompokHarta)

        'TrialBalance_Mentahkan()

        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_DataAsset VALUES ( " &
                                  " '" & IdAsset & "', " &
                                  " '" & NomorPembelian & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & NamaAktiva & "', " &
                                  " '" & COA_Asset & "', " &
                                  " '" & COA_BiayaPenyusutan & "', " &
                                  " '" & COA_AkumulasiPenyusutan & "', " &
                                  " '" & KelompokHarta_Angka & "', " &
                                  " '" & MasaManfaat & "', " &
                                  " '" & KodeDivisi & "', " &
                                  " '" & Divisi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                                  " '" & HargaPerolehan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Keterangan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal Bundelan, terhitung dari bulan terjadinya transaksi sampai seterusnya.
            If StatusSuntingDatabase = True Then
                Dim cmdSUSURJURNAL As OdbcCommand
                Dim drSUSURJURNAL As OdbcDataReader
                Dim NomorJV_HarusDihapus = Kosongan
                AksesDatabase_Transaksi(Buka)
                cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                      " WHERE COA = '" & COA_BiayaPenyusutan & "' " &
                                      " AND Tanggal_Transaksi >= '" & TanggalFormatSimpan(TanggalPerolehan) & "' ",
                                      KoneksiDatabaseTransaksi)
                drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
                Do While drSUSURJURNAL.Read
                    NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                    HapusJurnal_BerdasarkanNomorJV(NomorJV_HarusDihapus)
                Loop
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                  " Kode_Asset                  = '" & KodeAsset & "', " &
                                  " Nama_Aktiva                 = '" & NamaAktiva & "', " &
                                  " COA_Asset                   = '" & COA_Asset & "', " &
                                  " COA_Biaya_Penyusutan        = '" & COA_BiayaPenyusutan & "', " &
                                  " COA_Akumulasi_Penyusutan    = '" & COA_AkumulasiPenyusutan & "', " &
                                  " Kelompok_Harta              = '" & KelompokHarta_Angka & "', " &
                                  " Masa_Manfaat                = '" & MasaManfaat & "', " &
                                  " Kode_Divisi                 = '" & KodeDivisi & "', " &
                                  " Divisi                      = '" & Divisi & "', " &
                                  " Tanggal_Perolehan           = '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                                  " Harga_Perolehan             = '" & HargaPerolehan & "', " &
                                  " Keterangan                  = '" & Keterangan & "' " &
                                  " WHERE Nomor_ID              = '" & IdAsset & "' " _
                                  , KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal Bundelan Terkait :
            If StatusSuntingDatabase = True Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE from tbl_Transaksi " &
                                      " WHERE Bundelan LIKE '%" & KodeAsset_SebelumDiedit & "%' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            'Hapus Lagi Data Jurnal Bundelan, terhitung dari bulan terjadinya transaksi sampai seterusnya.
            'Karena sangat mungkin ada perubahan tanggal transaksi saat pengeditan, dimana Bulan Transaksi yang baru ternyata lebih awal dari Bulan Transaksi sebelum diedit.
            If StatusSuntingDatabase = True Then
                Dim cmdSUSURJURNAL As OdbcCommand
                Dim drSUSURJURNAL As OdbcDataReader
                Dim NomorJV_HarusDihapus = Kosongan
                AksesDatabase_Transaksi(Buka)
                cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                                 " WHERE COA = '" & COA_BiayaPenyusutan & "' " &
                                                 " AND Tanggal_Transaksi >= '" & TanggalFormatSimpan(TanggalPerolehan) & "' ",
                                                 KoneksiDatabaseTransaksi)
                drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
                Do While drSUSURJURNAL.Read
                    NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                    HapusJurnal_BerdasarkanNomorJV(NomorJV_HarusDihapus)
                Loop
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.TampilkanData()
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
            Pilihan = MessageBox.Show("Yakin ingin menutup form ini?", "PERHATIAN..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then e.Cancel = True
            If Pilihan = vbYes Then
                If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
                    If cmb_KelompokHarta.SelectedValue = Kosongan Or cmb_Divisi.SelectedValue = Kosongan Then
                        PesanPeringatan("Anda belum melengkapi 'Data Asset'." & Enter2Baris & "COA Asset dibatalkan..!")
                        win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Produk") = Kosongan
                        win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = Kosongan
                        win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = Kosongan
                    End If
                End If
            End If
        End If
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeAsset.IsReadOnly = True
        cmb_KelompokHarta.IsReadOnly = True
        txt_COA_Asset.IsReadOnly = True
        txt_NamaAkun_Asset.IsReadOnly = True
        txt_COA_BiayaPenyusutan.IsReadOnly = True
        txt_NamaAkun_BiayaPenyusutan.IsReadOnly = True
        txt_COA_AkumulasiPenyusutan.IsReadOnly = True
        txt_NamaAkun_AkumulasiPenyusutan.IsReadOnly = True
        cmb_Divisi.IsReadOnly = True
        txt_MasaManfaat.IsReadOnly = True
        txt_TarifPenyusutan.IsReadOnly = True
        MaxWidth = StandarLebarLayar
        MaxHeight = StandarTinggiLayar
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub

End Class
