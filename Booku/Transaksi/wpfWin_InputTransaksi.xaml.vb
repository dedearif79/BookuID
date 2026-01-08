Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputTransaksi

    Public FungsiForm
    Public JalurMasuk
    Public JudulForm
    Public NomorJV
    Dim ProsesSuntingDatabase As Boolean

    Dim AlurTransaksi
    Dim NomorBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Public COAUtama
    Public NamaAkunUtama
    Dim COALawan
    Dim NamaAkunLawan
    Dim JumlahTransaksi_MUA As Decimal
    Dim JumlahTransaksi_IDR As Int64
    Dim JumlahTransaksi
    Dim NamaProduk
    Dim TanggalTransaksi
    Dim Uraian
    Dim TahunTransaksi
    Public SaranaPembayaran
    Public KodeMataUang As String
    Dim Kurs As Decimal
    Dim MatauangIDR As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If KodeMataUang = Kosongan Then PesanUntukProgrammer("Kode Mata Uang Belum Ditentukan....!!!!")

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")
        If JalurMasuk = Kosongan Then PesanUntukProgrammer("Jalur Masuk belum ditentukan...!!!")

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Transaksi"
            cmb_KodeMataUang.IsEnabled = True
            btn_PilihCOA.IsEnabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Transaksi"
            cmb_KodeMataUang.IsEnabled = False  'Ini sengaja dibikin false, supaya jangan ada perubahan Kode Mata Uang...
            btn_PilihCOA.IsEnabled = False      '... dan COA. Karena bisa menimbulkan kekacauan pada Jurnal Adjusment Forex.
        End If

        If JalurMasuk = Halaman_MENUUTAMA Then
            VisibilitasSaranaPembayaran(True)
            Select Case AlurTransaksi
                Case AlurTransaksi_IN
                    lbl_SaranaPembayaran.Text = "Sarana Pencairan"
                Case AlurTransaksi_OUT
                    lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
            End Select
            lbl_KodeLawanAkun.Text = "Kode Akun"
            lbl_NamaLawanAkun.Text = "Nama Akun"
        Else
            VisibilitasSaranaPembayaran(False)
            lbl_KodeLawanAkun.Text = "Kode Lawan Akun"
            lbl_NamaLawanAkun.Text = "Nama Lawan Akun"
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        COAUtama = Kosongan
        NomorJV = 0
        KodeMataUang = KodeMataUang_IDR
        MatauangIDR = True
        If LevelUserAktif = LevelUser_99_AppDeveloper Then KodeMataUang = Kosongan
        KontenComboAlurTransaksi()
        KontenCombo_KodeMataUangAsing()
        KosongkanDatePicker(dtp_TanggalTransaksi)
        txt_NomorBukti.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_COALawan.Text = Kosongan
        txt_NamaLawanAkun.Text = Kosongan
        txt_JumlahTransaksi_IDR.Text = Kosongan
        txt_NamaProduk.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
        grb_Bank.Visibility = Visibility.Collapsed
        KosongkanValueElemenRichTextBox(txt_Uraian)

        ProsesResetForm = False

    End Sub


    Sub VisibilitasSaranaPembayaran(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_SaranaPembayaran.Visibility = Visibility.Visible
            cmb_SaranaPembayaran.Visibility = Visibility.Visible
        Else
            lbl_SaranaPembayaran.Visibility = Visibility.Collapsed
            cmb_SaranaPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KontenComboAlurTransaksi()
        cmb_AlurTransaksi.Items.Clear()
        cmb_AlurTransaksi.Items.Add(AlurTransaksi_IN)
        cmb_AlurTransaksi.Items.Add(AlurTransaksi_OUT)
        cmb_AlurTransaksi.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeMataUangAsing()
        KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang)
    End Sub


    Private Sub cmb_AlurTransaksi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_AlurTransaksi.SelectionChanged
        AlurTransaksi = cmb_AlurTransaksi.SelectedValue
    End Sub


    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = dtp_TanggalTransaksi.SelectedDate
            TahunTransaksi = dtp_TanggalTransaksi.SelectedDate.Value.Year
            PenentuanKurs()
        End If
    End Sub


    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub


    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        'If JenisTahunBuku = JenisTahunBuku_NORMAL And dtp_TanggalTransaksi.Text = Kosongan Then
        '    cmb_KodeMataUang.SelectedValue = Kosongan
        '    If Not (ProsesLoadingForm Or ProsesResetForm Or ProsesIsiValueForm) Then PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
        '    Return
        'End If
        KodeMataUang = cmb_KodeMataUang.SelectedValue
        PenentuanKurs()
        txt_COALawan.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
    End Sub
    Sub PenentuanKurs()
        If KodeMataUang = Kosongan Then Return
        If KodeMataUang = KodeMataUang_IDR Then
            MatauangIDR = True
            txt_Kurs.Visibility = Visibility.Collapsed
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_JumlahTransaksi_MUA.Visibility = Visibility.Collapsed
            lbl_JumlahTransaksi_MUA.Visibility = Visibility.Collapsed
            lbl_JumlahTransaksi_IDR.Text = "Jumlah Transaksi"
            txt_JumlahTransaksi_IDR.IsReadOnly = False
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_Kurs.Text = 1
        Else
            MatauangIDR = False
            txt_Kurs.Visibility = Visibility.Visible
            lbl_Kurs.Visibility = Visibility.Visible
            txt_JumlahTransaksi_MUA.Visibility = Visibility.Visible
            lbl_JumlahTransaksi_MUA.Visibility = Visibility.Visible
            lbl_JumlahTransaksi_MUA.Text = "Jumlah Transaksi (" & KodeMataUang & ")"
            lbl_JumlahTransaksi_IDR.Text = "Jumlah Transaksi (IDR)"
            txt_JumlahTransaksi_IDR.IsReadOnly = True
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            If dtp_TanggalTransaksi.Text <> Kosongan Then txt_Kurs.Text = AmbilValue_KursTengahBI(KodeMataUang, TanggalTransaksi)
        End If
    End Sub

    Private Sub txt_Kurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
        If KodeLawanTransaksi <> Kosongan And NamaLawanTransaksi = Kosongan Then txt_NamaLawanTransaksi.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
    End Sub
    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_COALawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COALawan.TextChanged
        COALawan = txt_COALawan.Text
        txt_NamaLawanAkun.Text = AmbilValue_NamaAkun(COALawan)
    End Sub
    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        If KodeMataUang = Kosongan Then
            PesanPeringatan("Silakan pilih 'Kode Mata Uang' terlebih dahulu.")
            Return
        End If
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Semua
        If txt_COALawan.Text <> Kosongan Then win_ListCOA.COATerseleksi = txt_COALawan.Text
        If txt_NamaLawanAkun.Text <> Kosongan Then win_ListCOA.NamaAkunTerseleksi = txt_NamaLawanAkun.Text
        win_ListCOA.ShowDialog()
        txt_COALawan.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaLawanAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanAkun.TextChanged
        NamaAkunLawan = txt_NamaLawanAkun.Text
    End Sub


    Private Sub txt_JumlahTransaksi_MUA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi_MUA.TextChanged
        If Not MatauangIDR Then
            JumlahTransaksi_MUA = AmbilAngka_Asing(txt_JumlahTransaksi_MUA.Text)
            FormatUlangAngkaKeBilanganDesimal(JumlahTransaksi)
            JumlahTransaksi = JumlahTransaksi_MUA
            txt_JumlahTransaksi_IDR.Text = FormatUlangInt64(AmbilValue_NilaiMataUang(KodeMataUang, Kurs, JumlahTransaksi_MUA))
        End If
    End Sub

    Private Sub txt_JumlahTransaksi_IDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi_IDR.TextChanged
        JumlahTransaksi_IDR = AmbilAngka(txt_JumlahTransaksi_IDR.Text)
        If MatauangIDR Then
            FormatUlangAngkaKeBilanganBulat(JumlahTransaksi)
            JumlahTransaksi = FormatUlangInt64(JumlahTransaksi_IDR)
        End If
    End Sub


    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub


    Private Sub cmb_SaranaPembayaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        SaranaPembayaran = cmb_SaranaPembayaran.SelectedValue
        COAUtama = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If COATermasukBank(COAUtama) And JalurMasuk = Halaman_MENUUTAMA Then
            grb_Bank.Visibility = Visibility.Visible
            KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
            Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
    End Sub
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim JumlahTransfer
    Dim TotalBank
    Sub Perhitungan_ValueBank()
        If MatauangIDR Then
            FormatUlangAngkaKeBilanganBulat(BiayaAdministrasiBank)
            FormatUlangAngkaKeBilanganBulat(JumlahTransfer)
            FormatUlangAngkaKeBilanganBulat(TotalBank)
        Else
            FormatUlangAngkaKeBilanganDesimal(BiayaAdministrasiBank)
            FormatUlangAngkaKeBilanganDesimal(JumlahTransfer)
            FormatUlangAngkaKeBilanganDesimal(TotalBank)
        End If
        If AlurTransaksi = AlurTransaksi_IN Then Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_IN, AmbilAngka(JumlahTransaksi), JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)      'Sengaja pakai logika ini
        If AlurTransaksi = AlurTransaksi_OUT Then Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, AmbilAngka(JumlahTransaksi), JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)    'Supaya ketika dicopy tidak menimbulkan kekeliruan yang tidak disengaja
        txt_JumlahTransfer.Text = FormatUlangInt64(JumlahTransfer)
        txt_TotalBank.Text = FormatUlangInt64(TotalBank)
    End Sub
    Sub Reset_grb_Bank()
        grb_Bank.Visibility = Visibility.Collapsed
        txt_BiayaAdministrasiBank.Text = Kosongan
        cmb_DitanggungOleh.SelectedValue = Kosongan
        cmb_DitanggungOleh.IsEnabled = False
        If Not (COATermasukBank(COAUtama)) Then
            txt_JumlahTransfer.Text = Kosongan
            txt_TotalBank.Text = Kosongan
        End If
        txt_JumlahTransfer.IsEnabled = False
        txt_TotalBank.IsEnabled = False
    End Sub
    Private Sub txt_BiayaAdministrasiBank_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox_WPF(txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.IsEnabled = False
            cmb_DitanggungOleh.SelectedValue = Kosongan
        Else
            cmb_DitanggungOleh.IsEnabled = True
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub
    Private Sub txt_BiayaAdministrasiBank_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BiayaAdministrasiBank.PreviewTextInput
        
    End Sub

    Private Sub cmb_DitanggungOleh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DitanggungOleh.SelectionChanged
        DitanggungOleh = cmb_DitanggungOleh.SelectedValue
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub txt_JumlahTransfer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransfer.TextChanged
        JumlahTransfer = AmbilAngka(txt_JumlahTransfer.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahTransfer)
    End Sub
    Private Sub txt_JumlahTransfer_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahTransfer.PreviewTextInput
        
    End Sub

    Private Sub txt_TotalBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalBank.TextChanged
        TotalBank = AmbilAngka(txt_TotalBank.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalBank)
    End Sub




    Private Sub txt_Uraian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Uraian.TextChanged
        Uraian = IsiValueVariabelRichTextBox(txt_Uraian)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If AlurTransaksi = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_AlurTransaksi, "Alur Transaksi")
            Return
        End If

        If COALawan = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_COALawan, "Akun")
            Return
        End If

        If JumlahTransaksi_IDR = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_JumlahTransaksi_IDR, "Jumlah Transaksi")
            Return
        End If

        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
            Return
        End If

        If Uraian = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeksKaya(txt_Uraian, "Uraian")
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JurnalAdjusment_Forex(COAUtama, TanggalTransaksi)
            JurnalAdjusment_Forex(COALawan, TanggalTransaksi)
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then
            jur_NomorJV = NomorJV
            HapusJurnal_BerdasarkanNomorJV(jur_NomorJV)
        End If


        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
        jur_JenisJurnal = KonversiCOAKeSaranaPembayaran(COAUtama)
        jur_KodeMataUang = KodeMataUang
        jur_Kurs = Kurs
        jur_Referensi = NomorBukti
        jur_UraianTransaksi = Uraian
        jur_NamaProduk = NamaProduk
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_Direct = 1

        'Simpan Jurnal :
        Select Case AlurTransaksi
            Case AlurTransaksi_IN
                If COATermasukDEBET(COAUtama) Then JurnalDEBET()
                If COATermasukKREDIT(COAUtama) Then JurnalKREDIT()
            Case AlurTransaksi_OUT
                If COATermasukDEBET(COAUtama) Then JurnalKREDIT()
                If COATermasukKREDIT(COAUtama) Then JurnalDEBET()
        End Select


        If jur_StatusPenyimpananJurnal_Lengkap = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            Me.Close()
            Select Case JalurMasuk
                Case Halaman_MENUUTAMA
                    'Belum ada kebutuhan Coding di sini.
                Case Halaman_BUKUKAS
                    If usc_BukuKas.StatusAktif Then usc_BukuKas.TampilkanData()
                Case Halaman_BUKUBANK
                    If usc_BukuBank.StatusAktif Then usc_BukuBank.TampilkanData()
                Case Halaman_BUKUCASHADVANCE
                    If usc_BukuCashAdvance.StatusAktif Then usc_BukuCashAdvance.TampilkanData()
                Case Halaman_BUKUPETTYCASH
                    If usc_BukuPettyCash.StatusAktif Then usc_BukuPettyCash.TampilkanData()
            End Select
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                pesan_DataGagalDisimpan()
            Else
                PesanPeringatan("Data GAGAL diperbarui..!")
            End If
        End If

    End Sub


    Sub JurnalDEBET()
        ___jurDebetBankCashIN(DitanggungOleh, COAUtama, JumlahTransaksi, JumlahTransfer, BiayaAdministrasiBank)
        _______jurKredit(COALawan, JumlahTransaksi)
    End Sub

    Sub JurnalKREDIT()
        ___jurDebet(COALawan, JumlahTransaksi)
        _______jurKreditBankCashOUT(DitanggungOleh, COAUtama, JumlahTransaksi, JumlahTransfer, BiayaAdministrasiBank)
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_AlurTransaksi.IsReadOnly = True
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_COALawan.IsReadOnly = True
        txt_NamaLawanAkun.IsReadOnly = True
    End Sub

End Class
