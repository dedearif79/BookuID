Imports System.Windows.Controls
Imports System.Windows
Imports System.Data.Odbc
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient

Public Class wpfUsc_CompanyProfile

    Dim Nama
    Dim Tagline
    Dim NPWP
    Dim NamaDirektur
    Dim JenisUsaha
    Dim JenisWP
    Dim Alamat
    Dim Email
    Dim PIC
    Dim NomorSKT
    Dim TanggalSKT
    Dim KodeKPP
    Dim PemotongPPh
    Dim EmailAksesDJPO
    Dim PasswordAksesDJPO
    Dim NomorSuketUMKM
    Dim TanggalSuketUMKM
    Dim TanggalPKP
    Dim TanggalExpireSE
    Dim Password_eFaktur
    Dim KodeAktivasi
    Dim Passphrase
    Dim LevelPJK
    Dim TanggalExpireSBU

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        KontenComboJenisUsahaPerusahaan()
        KontenComboJenisWPPerusahaan()
        KontenComboPemotongPPh()
        KontenComboLevelJK()
        chk_TanggalExpireApp.IsChecked = True
        chk_TanggalExpireApp.IsEnabled = False
        chk_TanggalPKP.IsChecked = False
        lbl_TanggalPKP.IsEnabled = False
        dtp_TanggalPKP.IsEnabled = False
        chk_TanggalExpireSE.IsChecked = False
        lbl_TanggalExpireSE.IsEnabled = False
        dtp_TanggalExpireSE.IsEnabled = False
        chk_TanggalExpireSBU.IsChecked = False
        lbl_TanggalExpireSBU.IsEnabled = False
        dtp_TanggalExpireSBU.IsEnabled = False

        txt_NomorSeriProduk.Text = NomorSeriProduk
        txt_IDCustomer.Text = ID_Customer
        txt_NamaPerusahaan.Text = NamaPerusahaan
        txt_Tagline.Text = TaglinePerusahaan
        cmb_JenisUsahaPerusahaan.SelectedValue = JenisUsahaPerusahaan
        cmb_JenisWPPerusahaan.SelectedValue = JenisWPPerusahaan
        txt_NPWP.Text = NPWPPerusahaan
        txt_NamaDirektur.Text = NamaDirekturPerusahaan
        IsiValueElemenRichTextBox(txt_Alamat, AlamatPerusahaan)
        txt_Email.Text = EmailPerusahaan
        txt_PIC.Text = PICPerusahaan
        txt_NomorSKT.Text = NomorSKTPerusahaan
        If PerusahaanSebagaiPemotongPPh Then cmb_PemotongPPh.SelectedValue = Pilihan_Ya
        If Not PerusahaanSebagaiPemotongPPh Then cmb_PemotongPPh.SelectedValue = Pilihan_Tidak
        IsiGambarLogoPerusahaan(img_LogoPerusahaan)
        dtp_TanggalSKT.SelectedDate = TanggalFormatWPF(TanggalSKTPerusahaan)
        txt_KodeKPP.Text = KodeKPP_Perusahaan
        dtp_TanggalExpireApp.SelectedDate = TanggalFormatWPF(AppExpire)
        txt_EmailAksesDJPO.Text = EmailDJPO_Perusahaan
        txt_PasswordAksesDJPO.Text = PasswordDJPO_Perusahaan
        txt_NomorSuketUMKM.Text = NomorSuketUMKM_Perusahaan
        If Not NomorSuketUMKM_Perusahaan = Kosongan Then
            dtp_TanggalSuketUMKM.IsEnabled = True
            dtp_TanggalSuketUMKM.SelectedDate = TanggalFormatWPF(TanggalSuketUMKM_Perusahaan)
        Else
            dtp_TanggalSuketUMKM.IsEnabled = False
            dtp_TanggalSuketUMKM.Text = Kosongan
        End If
        If TanggalPKP_Perusahaan = TanggalKosong Then
            lbl_TanggalPKP.Foreground = clrNeutral500
            chk_TanggalPKP.IsChecked = False
            dtp_TanggalPKP.Text = Kosongan
        Else
            lbl_TanggalPKP.Foreground = clrTeksPrimer
            chk_TanggalPKP.IsChecked = True
            dtp_TanggalPKP.SelectedDate = TanggalFormatWPF(TanggalPKP_Perusahaan)
        End If
        If TanggalExpireSEPerusahaan = TanggalTakTerbatas Then
            lbl_TanggalExpireSE.Foreground = clrNeutral500
            chk_TanggalExpireSE.IsChecked = False
            dtp_TanggalExpireSE.Text = Kosongan
        Else
            lbl_TanggalExpireSE.Foreground = clrTeksPrimer
            chk_TanggalExpireSE.IsChecked = True
            dtp_TanggalExpireSE.SelectedDate = TanggalFormatWPF(TanggalExpireSEPerusahaan)
        End If
        txt_Password_eFaktur.Text = Password_eFaktur_Perusahaan
        txt_KodeAktivasi.Text = KodeAktivasiPerusahaan
        txt_Passphrase.Text = PassphrasePerusahaan
        LevelPJK = LevelPJK_Perusahaan
        chk_PJK.IsChecked = True
        Select Case LevelPJK
            Case 0
                cmb_LevelPJK.Text = Kosongan
                chk_PJK.IsChecked = False
            Case 1
                cmb_LevelPJK.SelectedValue = Level_Kecil
            Case 2
                cmb_LevelPJK.SelectedValue = Level_Menengah
            Case 3
                cmb_LevelPJK.SelectedValue = Level_Besar
        End Select
        If TanggalExpireSBUPerusahaan = TanggalTakTerbatas Then
            lbl_TanggalExpireSBU.Foreground = clrNeutral500
            chk_TanggalExpireSBU.IsChecked = False
            dtp_TanggalExpireSBU.Text = Kosongan
        Else
            lbl_TanggalExpireSBU.Foreground = clrTeksPrimer
            chk_TanggalExpireSBU.IsChecked = True
            dtp_TanggalExpireSBU.SelectedDate = TanggalFormatWPF(TanggalExpireSBUPerusahaan)
        End If
        btn_Simpan.IsEnabled = False

        ProsesLoadingForm = False

    End Sub

    Sub KondisiPerubahanForm()
        If ProsesLoadingForm Then Return '(Ini jangan dihapus. Ini dibutuhkan untuk logika).
        win_Pengaturan.AdaPerubahanForm = True
        btn_Simpan.IsEnabled = True
    End Sub


    Sub KontenComboJenisUsahaPerusahaan()
        cmb_JenisUsahaPerusahaan.Items.Clear()
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_IndustriManufactur)
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_Dagang)
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_Jasa)
        cmb_JenisUsahaPerusahaan.Text = Kosongan
    End Sub

    Sub KontenComboJenisWPPerusahaan()
        cmb_JenisWPPerusahaan.Items.Clear()
        cmb_JenisWPPerusahaan.Items.Add(JenisWP_OrangPribadi)
        cmb_JenisWPPerusahaan.Items.Add(JenisWP_BadanHukum)
        cmb_JenisWPPerusahaan.Text = Kosongan
    End Sub

    Sub KontenComboPemotongPPh()
        cmb_PemotongPPh.Items.Add(Pilihan_Ya)
        cmb_PemotongPPh.Items.Add(Pilihan_Tidak)
    End Sub


    Sub KontenComboLevelJK()
        cmb_LevelPJK.Items.Clear()
        cmb_LevelPJK.Items.Add(Level_Kecil)
        cmb_LevelPJK.Items.Add(Level_Menengah)
        cmb_LevelPJK.Items.Add(Level_Besar)
        cmb_LevelPJK.Text = Kosongan
    End Sub




    Private Sub txt_NamaPerusahaan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaPerusahaan.TextChanged
        Nama = txt_NamaPerusahaan.Text
        KondisiPerubahanForm()
    End Sub

    Private Sub txt_Tagline_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Tagline.TextChanged
        Tagline = txt_Tagline.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub cmb_JenisPerusahaan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisUsahaPerusahaan.SelectionChanged
        JenisUsaha = cmb_JenisUsahaPerusahaan.SelectedValue
        KondisiPerubahanForm()
    End Sub


    Private Sub cmb_JenisWPPerusahaan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisWPPerusahaan.SelectionChanged
        JenisWP = cmb_JenisWPPerusahaan.SelectedValue
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_NPWP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_NamaDirektur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaDirektur.TextChanged
        NamaDirektur = txt_NamaDirektur.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_Alamat_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Alamat.TextChanged
        Alamat = IsiValueVariabelRichTextBox(txt_Alamat)
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_Email_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Email.TextChanged
        Email = txt_Email.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_PIC_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PIC.TextChanged
        PIC = txt_PIC.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_NomorSKT_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorSKT.TextChanged
        NomorSKT = txt_NomorSKT.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub dtp_TanggalSKT_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalSKT.SelectedDateChanged
        If Not dtp_TanggalSKT.Text = Kosongan Then
            TanggalSKT = dtp_TanggalSKT.SelectedDate
            KondisiPerubahanForm()
        End If
    End Sub


    Private Sub txt_KodeKPP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeKPP.TextChanged
        KodeKPP = txt_KodeKPP.Text
        KondisiPerubahanForm()
    End Sub
    Private Sub txt_KodeKPP_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KodeKPP.PreviewTextInput
        
    End Sub


    Private Sub cmb_PemotongPPh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PemotongPPh.SelectionChanged
        If cmb_PemotongPPh.SelectedValue = Pilihan_Ya Then PemotongPPh = 1
        If cmb_PemotongPPh.SelectedValue = Pilihan_Tidak Then PemotongPPh = 0
        KondisiPerubahanForm()
    End Sub

    Dim ofd_BukaFileLogo As OpenFileDialog
    Private Sub btn_GantiLogo_Click(sender As Object, e As RoutedEventArgs) Handles btn_GantiLogo.Click

        Dim ProsesGantiLogo As Boolean = True

        ofd_BukaFileLogo = New OpenFileDialog
        ofd_BukaFileLogo.FileName = Kosongan
        ofd_BukaFileLogo.Filter = "Gambar JPG (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        ofd_BukaFileLogo.Title = "Pilih File Gambar (Hanya Format JPG)"
        ofd_BukaFileLogo.Multiselect = False
        ofd_BukaFileLogo.ShowDialog()
        If ofd_BukaFileLogo.FileName = Kosongan Then Return

        Dim FilePathBahanLogo As String = ofd_BukaFileLogo.FileName

        KosongkanLogoPerusahaan(img_LogoPerusahaan)

        HapusFile(FilePathLogoPerusahaan)

        SalinFile(FilePathBahanLogo, FilePathLogoPerusahaan)

        PesanUntukProgrammer("Sumber : " & FilePathBahanLogo & Enter2Baris &
                             "Target : " & FilePathLogoPerusahaan & Enter2Baris &
                             "Proses Salin : " & SalinFileBerhasil)

        If SalinFileBerhasil = True Then
            ProsesGantiLogo = True
        Else
            ProsesGantiLogo = False
        End If

        If ProsesGantiLogo = True Then
            IsiGambarLogoPerusahaan(img_LogoPerusahaan)
        Else
            PesanPeringatan("Logo gagal diganti!")
        End If


    End Sub


    Private Sub chk_TanggalExpireApp_Checked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireApp.Checked
        'Belum ada kebutuhkan Coding di sini...!
    End Sub
    Private Sub chk_TanggalExpireApp_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireApp.Unchecked
        'Belum ada kebutuhkan Coding di sini...!
    End Sub
    Private Sub dtp_TanggalExpireApp_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalExpireApp.SelectedDateChanged
        'Belum ada kebutuhkan Coding di sini...!
    End Sub


    Private Sub txt_EmailAksesDJPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_EmailAksesDJPO.TextChanged
        EmailAksesDJPO = txt_EmailAksesDJPO.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_PasswordAksesDJPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PasswordAksesDJPO.TextChanged
        PasswordAksesDJPO = txt_PasswordAksesDJPO.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_NomorSuketUMKM_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorSuketUMKM.TextChanged
        NomorSuketUMKM = txt_NomorSuketUMKM.Text
        KondisiPerubahanForm()
        If NomorSuketUMKM = Kosongan Then
            dtp_TanggalSuketUMKM.IsEnabled = False
            dtp_TanggalSuketUMKM.Text = Kosongan
        Else
            dtp_TanggalSuketUMKM.IsEnabled = True
        End If
    End Sub


    Private Sub dtp_TanggalSuketUMKM_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalSuketUMKM.SelectedDateChanged
        If Not dtp_TanggalSuketUMKM.Text = Kosongan Then
            TanggalSuketUMKM = dtp_TanggalSuketUMKM.SelectedDate
        Else
            TanggalSuketUMKM = TanggalKosong
        End If
        KondisiPerubahanForm()
    End Sub


    Private Sub chk_TanggalPKP_Checked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalPKP.Checked
        lbl_TanggalPKP.Foreground = clrTeksPrimer
        dtp_TanggalPKP.IsEnabled = True
        KondisiPerubahanForm()
    End Sub
    Private Sub chk_TanggalPKP_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalPKP.Unchecked
        lbl_TanggalPKP.Foreground = clrNeutral500
        dtp_TanggalPKP.IsEnabled = False
        dtp_TanggalPKP.Text = Kosongan
        KondisiPerubahanForm()
    End Sub
    Private Sub dtp_TanggalPKP_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPKP.SelectedDateChanged
        If Not dtp_TanggalPKP.Text = Kosongan Then
            TanggalPKP = dtp_TanggalPKP.SelectedDate
        Else
            TanggalPKP = TanggalKosong
        End If
        KondisiPerubahanForm()
    End Sub


    Private Sub chk_TanggalExpireSE_Checked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireSE.Checked
        lbl_TanggalExpireSE.Foreground = clrTeksPrimer
        dtp_TanggalExpireSE.IsEnabled = True
        KondisiPerubahanForm()
    End Sub
    Private Sub chk_TanggalExpireSE_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireSE.Unchecked
        lbl_TanggalExpireSE.Foreground = clrNeutral500
        dtp_TanggalExpireSE.IsEnabled = False
        dtp_TanggalExpireSE.Text = Kosongan
        KondisiPerubahanForm()
    End Sub
    Private Sub dtp_TanggalExpireSE_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalExpireSE.SelectedDateChanged
        If Not dtp_TanggalExpireSE.Text = Kosongan Then
            TanggalExpireSE = dtp_TanggalExpireSE.SelectedDate
        Else
            TanggalExpireSE = TanggalTakTerbatas
        End If
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_Password_eFaktur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Password_eFaktur.TextChanged
        Password_eFaktur = txt_Password_eFaktur.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_KodeAktivasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAktivasi.TextChanged
        KodeAktivasi = txt_KodeAktivasi.Text
        KondisiPerubahanForm()
    End Sub


    Private Sub txt_Passphrase_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Passphrase.TextChanged
        Passphrase = txt_Passphrase.Text
        KondisiPerubahanForm()
    End Sub

    Private Sub chk_PJK_Checked(sender As Object, e As RoutedEventArgs) Handles chk_PJK.Checked
        lbl_PJK.Foreground = clrTeksPrimer
        cmb_LevelPJK.IsEnabled = True
        chk_TanggalExpireSBU.IsEnabled = True
        KondisiPerubahanForm()
    End Sub
    Private Sub chk_PJK_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_PJK.Unchecked
        lbl_PJK.Foreground = clrNeutral500
        cmb_LevelPJK.IsEnabled = False
        cmb_LevelPJK.SelectedValue = Kosongan
        chk_TanggalExpireSBU.IsChecked = False
        chk_TanggalExpireSBU.IsEnabled = False
        KondisiPerubahanForm()
    End Sub
    Private Sub cmb_LevelPJK_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_LevelPJK.SelectionChanged
        LevelPJK = cmb_LevelPJK.SelectedValue
        Select Case LevelPJK
            Case Kosongan
                LevelPJK = 0
            Case Level_Kecil
                LevelPJK = 1
            Case Level_Menengah
                LevelPJK = 2
            Case Level_Besar
                LevelPJK = 3
        End Select
        KondisiPerubahanForm()
    End Sub



    Private Sub chk_TanggalExpireSBU_Checked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireSBU.Checked
        lbl_TanggalExpireSBU.Foreground = clrTeksPrimer
        dtp_TanggalExpireSBU.IsEnabled = True
        KondisiPerubahanForm()
    End Sub
    Private Sub chk_TanggalExpireSBU_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_TanggalExpireSBU.Unchecked
        lbl_TanggalExpireSBU.Foreground = clrNeutral500
        dtp_TanggalExpireSBU.IsEnabled = False
        dtp_TanggalExpireSBU.Text = Kosongan
        KondisiPerubahanForm()
    End Sub
    Private Sub dtp_TanggalExpireSBU_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalExpireSBU.SelectedDateChanged
        If Not dtp_TanggalExpireSBU.Text = Kosongan Then
            TanggalExpireSBU = dtp_TanggalExpireSBU.SelectedDate
        Else
            TanggalExpireSBU = TanggalTakTerbatas
        End If
        KondisiPerubahanForm()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If Nama = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaPerusahaan, "Nama Perusahaan")
            Return
        End If

        If NPWP = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NPWP, "NPWP")
            Return
        End If

        If NamaDirektur = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaDirektur, "Nama Direktur")
            Return
        End If

        If Alamat = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeksKaya(txt_Alamat, "Alamat")
            Return
        End If

        If Email = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_Email, "Email")
            Return
        End If

        If PIC = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_PIC, "PIC")
            Return
        End If

        If Not (KodeKPP.Length = 3) Then
            PesanPeringatan("Silakan isi kolom Kode KPP dengan benar..!" & Enter2Baris & "(3 digit angka)")
            txt_KodeKPP.Focus()
            Return
        End If

        If NomorSuketUMKM <> Kosongan Then
            If dtp_TanggalSuketUMKM.Text = Kosongan Then
                PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalSuketUMKM, "Tanggal Suket UMKM")
                Return
            End If
        End If

        If chk_TanggalPKP.IsChecked = True Then
            If dtp_TanggalPKP.Text = Kosongan Then
                PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalPKP, "Tanggal PKP")
                Return
            End If
        End If

        If chk_TanggalExpireSE.IsChecked = True Then
            If dtp_TanggalExpireSE.Text = Kosongan Then
                PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalExpireSE, "Tanggal ExpireSE")
                Return
            End If
        End If

        If chk_PJK.IsChecked = True And LevelPJK = 0 Then
            PesanPeringatan_SilakanPilihCombo(cmb_LevelPJK, "Level Perusahaan Jasa Konstruksi")
            Return
        End If

        If chk_TanggalExpireSBU.IsChecked = True Then
            If dtp_TanggalExpireSBU.Text = Kosongan Then
                PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalExpireSBU, "Tanggal ExpireSBU")
                Return
            End If
        End If

        If chk_TanggalPKP.IsChecked = False Then TanggalPKP = TanggalKosong
        If chk_TanggalExpireSE.IsChecked = False Then TanggalExpireSE = TanggalTakTerbatas
        If chk_TanggalExpireSBU.IsChecked = False Then TanggalExpireSBU = TanggalTakTerbatas

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Company SET " &
                              " Nama_Perusahaan     = '" & Nama & "', " &
                              " Tagline             = '" & Tagline & "', " &
                              " Jenis_Usaha         = '" & JenisUsaha & "', " &
                              " Jenis_WP            = '" & JenisWP & "', " &
                              " NPWP                = '" & NPWP & "', " &
                              " Nama_Direktur       = '" & NamaDirektur & "', " &
                              " Alamat              = '" & Alamat & "', " &
                              " Email               = '" & Email & "', " &
                              " PIC                 = '" & PIC & "', " &
                              " Nomor_SKT           = '" & NomorSKT & "', " &
                              " Tanggal_SKT         = '" & TanggalFormatSimpan(TanggalSKT) & "', " &
                              " Kode_KPP            = '" & KodeKPP & "', " &
                              " Pemotong_PPh        = '" & PemotongPPh & "', " &
                              " Email_DJPO          = '" & EmailAksesDJPO & "', " &
                              " Password_DJPO       = '" & EnkripsiTeks(PasswordAksesDJPO) & "', " &
                              " Nomor_Suket_UMKM    = '" & NomorSuketUMKM & "', " &
                              " Tanggal_Suket_UMKM  = '" & TanggalFormatSimpan(TanggalSuketUMKM) & "', " &
                              " Tanggal_PKP         = '" & TanggalFormatSimpan(TanggalPKP) & "', " &
                              " Tanggal_Expire_SE   = '" & TanggalFormatSimpan(TanggalExpireSE) & "', " &
                              " Password_E_Faktur   = '" & EnkripsiTeks(Password_eFaktur) & "', " &
                              " Kode_Aktivasi       = '" & EnkripsiTeks(KodeAktivasi) & "', " &
                              " Passphrase          = '" & EnkripsiTeks(Passphrase) & "', " &
                              " Level_PJK           = '" & LevelPJK & "', " &
                              " Tanggal_Expire_SBU  = '" & TanggalFormatSimpan(TanggalExpireSBU) & "' ",
                              KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        If StatusSuntingDatabase = True Then
            BukaDatabaseDasar()
            cmdMySQL = New MySqlCommand(" UPDATE tbl_ListCompany SET Nama_Perusahaan = '" & Nama & "' WHERE ID_Customer = '" & ID_Customer & "' ", KoneksiDatabaseDasar)
            Try
                cmdMySQL.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabaseDasar()
        End If

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            'Perbarui seluruh variabel Data Company :
            NamaPerusahaan = Nama
            TaglinePerusahaan = Tagline
            NamaDirekturPerusahaan = NamaDirektur
            NPWPPerusahaan = NPWP
            JenisUsahaPerusahaan = JenisUsaha
            JenisWPPerusahaan = JenisWP
            TanggalExpireSEPerusahaan = TanggalFormatTampilan(TanggalExpireSE)
            TanggalExpireSBUPerusahaan = TanggalFormatTampilan(TanggalExpireSBU)
            AlamatPerusahaan = Alamat
            EmailPerusahaan = Email
            PICPerusahaan = PIC
            NomorSKTPerusahaan = NomorSKT
            TanggalSKTPerusahaan = TanggalFormatTampilan(TanggalSKT)
            KodeKPP_Perusahaan = KodeKPP
            EmailDJPO_Perusahaan = EmailAksesDJPO
            PasswordDJPO_Perusahaan = PasswordAksesDJPO
            NomorSuketUMKM_Perusahaan = NomorSuketUMKM
            TanggalSuketUMKM_Perusahaan = TanggalSuketUMKM
            AmbilValue_PerusahaanSebagaiUMKM()
            TanggalPKP_Perusahaan = TanggalPKP
            AmbilValue_PerusahaanSebagaiPKP()
            TanggalExpireSEPerusahaan = TanggalExpireSE
            Password_eFaktur_Perusahaan = Password_eFaktur
            KodeAktivasiPerusahaan = KodeAktivasi
            PassphrasePerusahaan = Passphrase
            LevelPJK_Perusahaan = LevelPJK
            TanggalExpireSBUPerusahaan = TanggalFormatTampilan(TanggalExpireSBU)
            If PemotongPPh = 0 Then PerusahaanSebagaiPemotongPPh = False
            If PemotongPPh = 1 Then PerusahaanSebagaiPemotongPPh = True
            PesanUmum("Data Company Profile BERHASIl diperbarui.")
            btn_Simpan.IsEnabled = False
            win_Pengaturan.AdaPerubahanForm = False
        Else
            PesanError("Company Profile GAGAL diperbarui..!!!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        'Belum ada kebutuhan coding.
    End Sub


    Sub New()
        InitializeComponent()
        txt_NomorSeriProduk.IsReadOnly = True
        txt_IDCustomer.IsReadOnly = True
        cmb_JenisUsahaPerusahaan.IsReadOnly = True
        cmb_PemotongPPh.IsReadOnly = True
        cmb_LevelPJK.IsReadOnly = True
        'dtp_TanggalSKT.IsEnabled = False
        dtp_TanggalExpireApp.IsEnabled = False
    End Sub

End Class
