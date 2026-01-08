Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Class wpfWin_InputHutangPiutangDividen

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim TanggalAktaNotaris
    Dim NomorBP
    Dim NomorAktaNotaris
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahDividen As Int64
    Dim JenisPPh
    Dim KodeSetoran
    Dim TarifPPh As Double
    Dim JumlahPPh As Int64
    Dim PPhDitanggung As Int64
    Dim PPhDipotong As Int64
    Dim HutangPiutangDividen As Int64
    Dim TanggalJatuhTempo
    Dim Keterangan
    Public NomorJV

    Public HutangPiutang
    Dim TabelPengawasan
    Dim KolomNomorBP
    Dim AwalanNomorBP
    Dim COABiayaPPh
    Dim COAHutangPPh

    Dim AdaJatuhTemponya As Boolean

    Dim SaranaPembayaran


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!!!!")
        If HutangPiutang = Kosongan Then PesanUntukProgrammer("Hutang/Piutang belum ditentukan...!!!!!!")

        Select Case HutangPiutang
            Case hp_Hutang
                TabelPengawasan = "tbl_PengawasanHutangDividen"
                KolomNomorBP = "Nomor_BPHD"
                AwalanNomorBP = AwalanBPHD_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPPD"
                lbl_KodeLawanTransaksi.Text = "Kode Pemegang Saham"
                lbl_NamaLawanTransaksi.Text = "Nama Pemegang Saham"
                lbl_HutangPiutangDividen.Text = "Hutang Dividen"
            Case hp_Piutang
                TabelPengawasan = "tbl_PengawasanPiutangDividen"
                KolomNomorBP = "Nomor_BPPD"
                AwalanNomorBP = AwalanBPPD_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPPD"
                lbl_KodeLawanTransaksi.Text = "Kode Perusahaan"
                lbl_NamaLawanTransaksi.Text = "Nama Perusahaan"
                lbl_HutangPiutangDividen.Text = "Piutang Dividen"
            Case Else
                PesanUntukProgrammer("Tentukan dulu, Hutang atau Piutang...!!!")
        End Select

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input " & HutangPiutang & " Dividen"
            btn_Simpan.Content = teks_Simpan
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit " & HutangPiutang & " Dividen"
            btn_Simpan.Content = teks_Perbarui
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                'txt_JumlahDividen.Text = dr.Item("Jumlah_Dividen") '(Ini tidak perlu...!!!)
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        NomorID = Kosongan
        HutangPiutang = Kosongan

        KosongkanDatePicker(dtp_TanggalAktaNotaris)
        txt_NomorBP.Text = Kosongan
        txt_NomorAktaNotaris.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_JumlahDividen.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        lbl_JumlahPPh.Text = "Jumlah PPh"
        txt_JumlahPPh.Text = Kosongan
        Perhitungan() 'Ini jangan dihapus ya. Ini diperlukan untuk memicu logika Visibitas Kolom-kolom PPh
        KosongkanDatePicker(dtp_TanggalJatuhTempo)
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        JumlahDividen = 0
        AdaJatuhTemponya = False 'Ini Penting..! Jangan Dihapus..!

        NomorJV = 0

        COABiayaPPh = Kosongan
        COAHutangPPh = Kosongan

        JenisPPh = Kosongan
        KodeSetoran = Kosongan

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan) + 1
        NomorBP = AwalanNomorBP & NomorID
        txt_NomorBP.Text = NomorBP

    End Sub


    Private Sub dtp_TanggalAktaNotaris_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalAktaNotaris.SelectedDateChanged
        If dtp_TanggalAktaNotaris.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalAktaNotaris)
            TanggalAktaNotaris = TanggalFormatTampilan(dtp_TanggalAktaNotaris.SelectedDate)
        End If
    End Sub


    Private Sub txt_NomorBP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub


    Private Sub txt_NomorAktaNotaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorAktaNotaris.TextChanged
        NomorAktaNotaris = txt_NomorAktaNotaris.Text
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        If HutangPiutang = hp_Hutang Then txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
        If HutangPiutang = hp_Piutang Then txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
        LogikaPPh()
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        If HutangPiutang = hp_Hutang Then
            BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
        End If
        If HutangPiutang = hp_Piutang Then
            BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Tidak)
        End If
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_JumlahDividen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahDividen.TextChanged
        JumlahDividen = AmbilAngka(txt_JumlahDividen.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahDividen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahDividen.PreviewTextInput
              
    End Sub


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        TarifPPh = AmbilAngka(txt_TarifPPh.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_TarifPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPh.PreviewTextInput
              
    End Sub


    Private Sub txt_JumlahPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPPh.TextChanged
        JumlahPPh = AmbilAngka(txt_JumlahPPh.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahPPh.PreviewTextInput
              
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_PPhDitanggung_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhDitanggung.PreviewTextInput
              
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_PPhDipotong_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhDipotong.PreviewTextInput
              
    End Sub


    Private Sub txt_HutangPiutangDividen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HutangPiutangDividen.TextChanged
        HutangPiutangDividen = AmbilAngka(txt_HutangPiutangDividen.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_HutangPiutangDividen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_HutangPiutangDividen.PreviewTextInput
              
    End Sub


    Private Sub dtp_TanggalJatuhTempo_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.Text <> Kosongan Then
            TanggalJatuhTempo = TanggalFormatTampilan(dtp_TanggalJatuhTempo.SelectedDate)
        End If
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Sub Perhitungan()
        If TarifPPh = 0 Then
            lbl_JumlahPPh.Visibility = Visibility.Collapsed
            lbl_PPhDipotong.Visibility = Visibility.Collapsed
            lbl_PPhDitanggung.Visibility = Visibility.Collapsed
            txt_JumlahPPh.Visibility = Visibility.Collapsed
            txt_PPhDipotong.Visibility = Visibility.Collapsed
            txt_PPhDitanggung.Visibility = Visibility.Collapsed
            txt_PPhDitanggung.Text = Kosongan
        Else
            lbl_JumlahPPh.Visibility = Visibility.Visible
            lbl_PPhDipotong.Visibility = Visibility.Visible
            lbl_PPhDitanggung.Visibility = Visibility.Visible
            txt_JumlahPPh.Visibility = Visibility.Visible
            txt_PPhDipotong.Visibility = Visibility.Visible
            txt_PPhDitanggung.Visibility = Visibility.Visible
        End If
        txt_JumlahPPh.Text = CLng(Persen(TarifPPh) * JumlahDividen)
        txt_PPhDipotong.Text = JumlahPPh - PPhDitanggung
        txt_HutangPiutangDividen.Text = JumlahDividen - PPhDipotong
    End Sub


    Sub LogikaPPh()

        If KodeLawanTransaksi = Kosongan Then Return

        If HutangPiutang = hp_Hutang Then
            If AmbilValue_PemegangDariSahamLuarNegeri(KodeLawanTransaksi) Then
                COABiayaPPh = KodeTautanCOA_BiayaPPhPasal26
                COAHutangPPh = KodeTautanCOA_HutangPPhPasal26_101
                JenisPPh = JenisPPh_Pasal26
                KodeSetoran = KodeSetoran_101
                lbl_JumlahPPh.Text = "PPh Pasal 26 - 101"
            Else
                If AmbilValue_PemegangSahamBerbadanHukum(KodeLawanTransaksi) Then
                    COABiayaPPh = KodeTautanCOA_BiayaPPhPasal23
                    COAHutangPPh = KodeTautanCOA_HutangPPhPasal23_101
                    JenisPPh = JenisPPh_Pasal23
                    KodeSetoran = KodeSetoran_101
                    lbl_JumlahPPh.Text = "PPh Pasal 23 - 101"
                Else
                    COABiayaPPh = KodeTautanCOA_BiayaPPhPasal42
                    COAHutangPPh = KodeTautanCOA_HutangPPhPasal42_419
                    JenisPPh = JenisPPh_Pasal42
                    KodeSetoran = KodeSetoran_419
                    lbl_JumlahPPh.Text = "PPh Pasal 4 (2) - 419"
                End If
            End If
        End If

        If HutangPiutang = hp_Piutang Then
            COABiayaPPh = KodeTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima
            COAHutangPPh = KodeTautanCOA_HutangPPhPasal23_101
            JenisPPh = JenisPPh_Pasal23
            KodeSetoran = KodeSetoran_101
            lbl_JumlahPPh.Text = "PPh Pasal 23 - 101"
        End If

    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click


        'Validasi Kolom-kolom Tertentu :
        If dtp_TanggalAktaNotaris.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Akta Notaris'.")
            dtp_TanggalAktaNotaris.Focus()
            Return
        End If

        If NomorAktaNotaris = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Akta Notaris'.")
            txt_NomorAktaNotaris.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Pemegang Saham'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahDividen = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Dividen'.")
            txt_JumlahDividen.Focus()
            Return
        End If

        If dtp_TanggalJatuhTempo.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Jatuh Tempo'.")
            dtp_TanggalJatuhTempo.Focus()
            Return
        End If


        If FungsiForm = FungsiForm_TAMBAH Then

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
            Else
                NomorJV = 0
            End If

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBP & "', " &
                                  " '" & TanggalFormatSimpan(TanggalAktaNotaris) & "', " &
                                  " '" & NomorAktaNotaris & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & JumlahDividen & "', " &
                                  " '" & JenisPPh & "', " &
                                  " '" & KodeSetoran & "', " &
                                  " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " '" & JumlahPPh & "', " &
                                  " '" & PPhDitanggung & "', " &
                                  " '" & PPhDipotong & "', " &
                                  " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Buka)

            'Update Data pada Tabel : tbl_PengawasanPiutangAfiliasi 
            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  KolomNomorBP & "              = '" & NomorBP & "', " &
                                  " Tanggal_Akta_Notaris        = '" & TanggalFormatSimpan(TanggalAktaNotaris) & "', " &
                                  " Nomor_Akta_Notaris          = '" & NomorAktaNotaris & "', " &
                                  " Kode_Lawan_Transaksi        = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi        = '" & NamaLawanTransaksi & "', " &
                                  " Jumlah_Dividen              = '" & JumlahDividen & "', " &
                                  " Jenis_PPh                   = '" & JenisPPh & "', " &
                                  " Kode_Setoran                = '" & KodeSetoran & "', " &
                                  " Tarif_PPh                   = '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " PPh_Terutang                = '" & JumlahPPh & "', " &
                                  " PPh_Ditanggung              = '" & PPhDitanggung & "', " &
                                  " PPh_Dipotong                = '" & PPhDipotong & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Keterangan                  = '" & Keterangan & "', " &
                                  " Nomor_JV                    = '" & NomorJV & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Hapus Jurnal Terkait : tbl_Transaksi
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And StatusSuntingDatabase = True Then

            '====================== JURNAL ========================
            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalAktaNotaris)
            jur_JenisJurnal = JenisJurnal_Dividen
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = Kosongan
            jur_NomorInvoice = Kosongan
            jur_NomorFakturPajak = Kosongan
            jur_KodeLawanTransaksi = KodeLawanTransaksi
            jur_NamaLawanTransaksi = NamaLawanTransaksi
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            If HutangPiutang = hp_Hutang Then

                'Simpan Jurnal :
                ___jurDebet(KodeTautanCOA_LabaDitahan, JumlahDividen)
                ___jurDebet(COABiayaPPh, PPhDitanggung)
                _______jurKredit(COAHutangPPh, JumlahPPh)
                _______jurKredit(KodeTautanCOA_HutangDividen, HutangPiutangDividen)

            End If

            If HutangPiutang = hp_Piutang Then

                'Simpan Jurnal :
                ___jurDebet(KodeTautanCOA_PiutangDividen, HutangPiutangDividen)
                ___jurDebet(COABiayaPPh, JumlahPPh)
                _______jurKredit(KodeTautanCOA_PenghasilanLainnya, PPhDitanggung)
                _______jurKredit(KodeTautanCOA_PenghasilanDividen, JumlahDividen)

            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If HutangPiutang = hp_Hutang And usc_BukuPengawasanHutangDividen.StatusAktif Then
                usc_BukuPengawasanHutangDividen.TampilkanData()
            End If
            If HutangPiutang = hp_Piutang And usc_BukuPengawasanPiutangDividen.StatusAktif Then
                usc_BukuPengawasanPiutangDividen.TampilkanData()
            End If
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_NomorBP.IsReadOnly = True
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_JumlahPPh.IsReadOnly = True
        txt_PPhDipotong.IsReadOnly = True
        txt_HutangPiutangDividen.IsReadOnly = True
        txt_TarifPPh.MaxLength = 2
    End Sub

End Class
