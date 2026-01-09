Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputHutangBankLeasing

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public BankLeasing
    Dim TabelPengawasan
    Dim TabelAngsuran
    Dim KolomNomorBPH
    Dim AwalanNomorBPH
    Dim COAKredit
    Dim JenisJurnal

    Public NomorID
    Public NomorBP
    Public NomorJV_Persetujuan
    Public NomorJV_Pencairan
    Dim TanggalPersetujuan
    Dim KodeKreditur
    Dim NamaKreditur
    Dim JenisWP_Kreditur
    Dim JumlahPinjaman
    Dim TanggalJatuhTempo
    Dim NomorKontrak
    Dim TanggalPencairan
    Dim BiayaAdministrasiKontrak
    Dim BiayaNotaris
    Dim BiayaPPh
    Dim Keterangan

    Dim SudahDicairkan As Boolean
    Dim JumlahPencairan
    Dim BankPencairan
    Dim KodeAkun_BankPencairan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If BankLeasing = Kosongan Then PesanUntukProgrammer("Tentukan dulu, ini untuk Bank atau Leasing..???!!!!")

        If BankLeasing = bl_Bank Then
            lbl_NomorBP.Text = "Nomor BPHB"
            TabelPengawasan = "tbl_PengawasanHutangBank"
            TabelAngsuran = "tbl_JadwalAngsuranHutangBank"
            KolomNomorBPH = "Nomor_BPHB"
            AwalanNomorBPH = AwalanBPHB_PlusTahunBuku
            COAKredit = KodeTautanCOA_HutangBank
            JenisJurnal = JenisJurnal_HutangBank
        End If

        If BankLeasing = bl_Leasing Then
            lbl_NomorBP.Text = "Nomor BPHL"
            TabelPengawasan = "tbl_PengawasanHutangLeasing"
            TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing"
            KolomNomorBPH = "Nomor_BPHL"
            AwalanNomorBPH = AwalanBPHL_PlusTahunBuku
            COAKredit = KodeTautanCOA_HutangLeasing
            JenisJurnal = JenisJurnal_HutangLeasing
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            JudulForm = "Input Hutang " & BankLeasing
            SistemPenomoranOtomatis()

        End If

        If FungsiForm = FungsiForm_EDIT Then

            JudulForm = "Edit Hutang " & BankLeasing
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                dtp_TanggalPencairan.SelectedDate = TanggalFormatWPF(dr.Item("Tanggal_Pencairan"))
                TanggalPencairan = TanggalFormatTampilan(dtp_TanggalPencairan.SelectedDate)
                txt_BiayaAdministrasiKontrak.Text = dr.Item("Biaya_Administrasi_Kontrak")
                txt_BiayaNotaris.Text = dr.Item("Biaya_Notaris")
                txt_BiayaPPh.Text = dr.Item("Biaya_PPh")
                KodeAkun_BankPencairan = dr.Item("COA_Debet")
                NomorJV_Pencairan = dr.Item("Nomor_JV_Pencairan")
            End If
            AksesDatabase_Transaksi(Tutup)
            If NomorJV_Pencairan = 0 Then
                dtp_TanggalPencairan.Text = Kosongan
                TanggalPencairan = TanggalKosong
                grb_Pencairan.Visibility = Visibility.Collapsed
            Else
                grb_Pencairan.Visibility = Visibility.Visible
                cmb_BankPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(KodeAkun_BankPencairan)
            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            grb_Pencairan.Visibility = Visibility.Visible
            SudahDicairkan = True
        Else
            grb_Pencairan.Visibility = Visibility.Collapsed
            SudahDicairkan = False
        End If

        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        BankLeasing = Kosongan

        NomorID = 0
        NomorBP = Kosongan
        NomorJV_Persetujuan = 0
        NomorJV_Pencairan = 0
        KosongkanDatePicker(dtp_TanggalPersetujuan)
        txt_KodeKreditur.Text = Kosongan
        txt_NamaKreditur.Text = Kosongan
        JenisWP_Kreditur = Kosongan
        txt_JumlahPinjaman.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalJatuhTempo)
        txt_NomorKontrak.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalPencairan)
        KontenComboDaftarBank_Public_WPF(cmb_BankPencairan)
        txt_BiayaAdministrasiKontrak.Text = Kosongan
        txt_BiayaNotaris.Text = Kosongan
        txt_BiayaPPh.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        SudahDicairkan = False
        JumlahPencairan = 0
        KodeAkun_BankPencairan = Kosongan
        TanggalPencairan = TanggalKosong

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan) + 1
        txt_NomorBP.Text = AwalanNomorBPH & NomorID

    End Sub



    Private Sub dtp_TanggalPersetujuan_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPersetujuan.SelectedDateChanged
        If dtp_TanggalPersetujuan.Text <> Kosongan Then
            KunciTahun_TidakBolehKurangDariTahunBukuAktif_WPF(dtp_TanggalPersetujuan)
            TanggalPersetujuan = TanggalFormatTampilan(dtp_TanggalPersetujuan.SelectedDate)
        End If
    End Sub


    Private Sub txt_NomorBP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub


    Private Sub txt_KodeKreditur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeKreditur.TextChanged
        KodeKreditur = txt_KodeKreditur.Text
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeKreditur & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JenisWP_Kreditur = dr.Item("Jenis_WP")
        End If
        AksesDatabase_General(Tutup)
        If JenisWP_Kreditur = Kosongan Then PesanUntukProgrammer("Jenis PPh belum ditentukan...!!!")
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        If txt_KodeKreditur.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeKreditur.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaKreditur.Text
        End If
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Mitra_Supplier
        win_ListLawanTransaksi.PilihLembagaKeuangan = Pilihan_Ya
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeKreditur.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaKreditur.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
    End Sub

    Private Sub txt_NamaKreditur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaKreditur.TextChanged
        NamaKreditur = txt_NamaKreditur.Text
    End Sub

    Private Sub txt_JumlahPinjaman_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPinjaman.TextChanged
        JumlahPinjaman = AmbilAngka(txt_JumlahPinjaman.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahPinjaman)
    End Sub
    Private Sub txt_JumlahPinjaman_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahPinjaman.PreviewTextInput
        
    End Sub

    Private Sub dtp_TanggalJatuhTempo_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.Text <> Kosongan Then
            KunciTahun_TidakBolehKurangDariTahunBukuAktif_WPF(dtp_TanggalJatuhTempo)
            TanggalJatuhTempo = TanggalFormatTampilan(dtp_TanggalJatuhTempo.SelectedDate)
        End If
    End Sub


    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub


    Private Sub dtp_TanggalPencairan_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPencairan.SelectedDateChanged
        If dtp_TanggalPencairan.Text <> Kosongan Then
            KunciTahun_TidakBolehKurangDariTahunBukuAktif_WPF(dtp_TanggalPencairan)
            TanggalPencairan = TanggalFormatTampilan(dtp_TanggalPencairan.SelectedDate)
        End If
    End Sub


    Private Sub cmb_BankPencairan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_BankPencairan.SelectionChanged
        BankPencairan = cmb_BankPencairan.SelectedValue
    End Sub


    Private Sub txt_BiayaAdministrasiKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiKontrak.TextChanged
        BiayaAdministrasiKontrak = AmbilAngka(txt_BiayaAdministrasiKontrak.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_BiayaAdministrasiKontrak)
    End Sub
    Private Sub txt_BiayaAdministrasiKontrak_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BiayaAdministrasiKontrak.PreviewTextInput
        
    End Sub


    Private Sub txt_BiayaNotaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaNotaris.TextChanged
        BiayaNotaris = AmbilAngka(txt_BiayaNotaris.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_BiayaNotaris)
    End Sub
    Private Sub txt_BiayaNotaris_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BiayaNotaris.PreviewTextInput
        
    End Sub


    Private Sub txt_BiayaPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaPPh.TextChanged
        Perhitungan()
        BiayaPPh = AmbilAngka(txt_BiayaPPh.Text)
        PemecahRibuanUntukTextBox_WPF(txt_BiayaPPh)
    End Sub
    Private Sub txt_BiayaPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BiayaPPh.PreviewTextInput
        
    End Sub


    Sub Perhitungan()
        JumlahPencairan = JumlahPinjaman - (BiayaAdministrasiKontrak + BiayaNotaris)
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalPersetujuan.Text = Kosongan Then
            PesanPeringatan("Silakan isi Tanggal Persetujuan.")
            dtp_TanggalPersetujuan.Focus()
            Return
        End If

        If KodeKreditur = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Kreditur'.")
            txt_KodeKreditur.Focus()
            Return
        End If

        If JumlahPinjaman = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Jumlah Pinjaman'.")
            txt_JumlahPinjaman.Focus()
            Return
        End If

        If dtp_TanggalJatuhTempo.Text = Kosongan Then
            PesanPeringatan("Silakan isi Tanggal JatuhTempo.")
            dtp_TanggalJatuhTempo.Focus()
            Return
        End If

        If NomorKontrak = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Nomor Kontrak'.")
            txt_NomorKontrak.Focus()
            Return
        End If

        If SudahDicairkan = True Then

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then

                If BankPencairan = Kosongan Then
                    Pesan_Peringatan("Silakan pilih 'Bank' untuk pencairan.")
                    cmb_BankPencairan.Focus()
                    Return
                End If

            End If

        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                SistemPenomoranOtomatis_NomorJV()
                NomorJV_Persetujuan = jur_NomorJV
            Else
                NomorJV_Persetujuan = 0
            End If

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBP & "', " &
                                  " '" & KodeKreditur & "', " &
                                  " '" & NamaKreditur & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPersetujuan) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPencairan) & "', " &
                                  " '" & JumlahPinjaman & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & BiayaAdministrasiKontrak & "', " &
                                  " '" & BiayaNotaris & "', " &
                                  " '" & BiayaPPh & "', " &
                                  " '" & KodeAkun_BankPencairan & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV_Persetujuan & "', " &
                                  " '" & NomorJV_Pencairan & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  KolomNomorBPH & "             = '" & NomorBP & "', " &
                                  " Kode_Kreditur               = '" & KodeKreditur & "', " &
                                  " Nama_Kreditur               = '" & NamaKreditur & "', " &
                                  " Tanggal_Persetujuan         = '" & TanggalFormatSimpan(TanggalPersetujuan) & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Tanggal_Pencairan           = '" & TanggalFormatSimpan(TanggalPencairan) & "', " &
                                  " Jumlah_Pinjaman             = '" & JumlahPinjaman & "', " &
                                  " Nomor_Kontrak               = '" & NomorKontrak & "', " &
                                  " Biaya_Administrasi_Kontrak  = '" & BiayaAdministrasiKontrak & "', " &
                                  " Biaya_Notaris               = '" & BiayaNotaris & "', " &
                                  " Biaya_PPh                   = '" & BiayaPPh & "', " &
                                  " COA_Debet                   = '" & KodeAkun_BankPencairan & "', " &
                                  " Keterangan                  = '" & Keterangan & "', " &
                                  " Nomor_JV_Persetujuan        = '" & NomorJV_Persetujuan & "', " &
                                  " Nomor_JV_Pencairan          = '" & NomorJV_Pencairan & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Perbarui Data Angsuran :
            cmd = New OdbcCommand(" UPDATE " & TabelAngsuran & " SET " &
                                  " Kode_Kreditur               = '" & KodeKreditur & "' " &
                                  " WHERE " & KolomNomorBPH & " = '" & NomorBP & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()


            'Hapus dulu Data Jurnal yang Lama :
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then

                cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                           " WHERE Nomor_JV = '" & NomorJV_Persetujuan & "' ", KoneksiDatabaseTransaksi)
                cmdHAPUS_ExecuteNonQuery()

                jur_NomorJV = NomorJV_Persetujuan

                AksesDatabase_Transaksi(Tutup)

            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And StatusSuntingDatabase = True Then

            '================= JURNAL PERSETUJUAN ========================
            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalPersetujuan)
            jur_JenisJurnal = JenisJurnal
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = Kosongan
            jur_NomorInvoice = Kosongan
            jur_NomorFakturPajak = Kosongan
            jur_KodeLawanTransaksi = KodeKreditur
            jur_NamaLawanTransaksi = NamaKreditur
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeTautanCOA_PiutangLainnya, JumlahPinjaman)
            _______jurKredit(COAKredit, JumlahPinjaman)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If usc_BukuPengawasanHutangBank.StatusAktif Then usc_BukuPengawasanHutangBank.TampilkanData()
            If usc_BukuPengawasanHutangLeasing.StatusAktif Then usc_BukuPengawasanHutangLeasing.TampilkanData()
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
        txt_KodeKreditur.IsReadOnly = True
        txt_NamaKreditur.IsReadOnly = True
        grb_Pencairan.Visibility = Visibility.Collapsed
    End Sub

End Class
