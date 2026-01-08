Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputPemindahbukuan

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPPB
    Dim TanggalBPPB
    Dim TanggalTransaksi
    Dim COAKredit
    Dim KodeMataUang_Kredit As String
    Dim KursBI_Kredit As Decimal
    Dim KursBank_Kredit As Decimal
    Dim JumlahKredit_MUA As Decimal
    Dim JumlahKredit_IDR As Int64
    Dim COADebet
    Dim KodeMataUang_Debet As String
    Dim KursBI_Debet As Decimal
    Dim KursBank_Debet As Decimal
    Dim JumlahDebet_MUA As Decimal
    Dim JumlahDebet_IDR As Int64
    Dim LabaRugiSelisihKurs As Int64
    Dim Penanggungjawab
    Dim UraianTransaksi

    Public NomorJV
    Public DenganPengajuan As Boolean
    Public TahunBukuNormalTanpaPengajuan As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH _
            Then
            win_MetodeInputBayar = New wpfWin_MetodeInputBayar
            win_MetodeInputBayar.ShowDialog()
            DenganPengajuan = win_MetodeInputBayar.DenganPengajuan
            If Not win_MetodeInputBayar.LanjutkanProses Then Close()
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And DenganPengajuan = False Then
            TahunBukuNormalTanpaPengajuan = True
        Else
            TahunBukuNormalTanpaPengajuan = False
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Pengajuan Pemindahbukuan"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Pengajuan Pemindahbukuan"
        End If

        If TahunBukuNormalTanpaPengajuan Then
            If FungsiForm = FungsiForm_TAMBAH Then
                JudulForm = "Input Pemindahbukuan"
            End If
            If FungsiForm = FungsiForm_EDIT Then
                JudulForm = "Edit Pemindahbukuan"
            End If
            lbl_TanggalBPPB.Text = "Tanggal Pemindahbukuan"
        Else
            lbl_TanggalBPPB.Text = "Tanggal Pengajuan"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Pemindahbukuan " &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NomorBPPB.Text = dr.Item("Nomor_BPPB")
                dtp_TanggalBPPB.SelectedDate = TanggalFormatWPF(dr.Item("Tanggal_BPPB"))
                cmb_DariBuku.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
                txt_KursBI_Kredit.Text = dr.Item("Kurs_BI_Kredit") 'Supaya cepet, ngambil dari database lokal, tidak ngambil dari server
                txt_KursBank_Kredit.Text = dr.Item("Kurs_Bank_Kredit")
                JumlahKredit_MUA = dr.Item("Jumlah_Kredit")
                JumlahKredit_IDR = AmbilValue_NilaiMataUang(KodeMataUang_Kredit, KursBI_Kredit, JumlahKredit_MUA)
                If KodeMataUang_Kredit = KodeMataUang_IDR Then
                    txt_JumlahKredit.Text = JumlahKredit_IDR
                Else
                    txt_JumlahKredit.Text = JumlahKredit_MUA
                End If
                cmb_KeBuku.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
                txt_KursBI_Debet.Text = dr.Item("Kurs_BI_Debet") 'Supaya cepet, ngambil dari database lokal, tidak ngambil dari server
                txt_KursBank_Debet.Text = dr.Item("Kurs_Bank_Debet")
                JumlahDebet_MUA = dr.Item("Jumlah_Debet")
                JumlahDebet_IDR = AmbilValue_NilaiMataUang(KodeMataUang_Debet, KursBI_Debet, JumlahDebet_MUA)
                If KodeMataUang_Debet = KodeMataUang_IDR Then
                    txt_JumlahDebet.Text = JumlahDebet_IDR
                Else
                    txt_JumlahDebet.Text = JumlahDebet_MUA
                End If
                txt_Penanggungjawab.Text = dr.Item("Penanggungjawab")
                IsiValueElemenRichTextBox(txt_UraianTransaksi, dr.Item("Uraian_Transaksi"))
                NomorJV = dr.Item("Nomor_JV")
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        'PesanUntukProgrammer(
        '    "Kode Mata Uang - Kredit : " & KodeMataUang_Kredit & Enter2Baris &
        '    "Jumlah MUA - Kredit : " & JumlahKredit_MUA & Enter2Baris &
        '    "Jumlah IDR - Kredit : " & JumlahKredit_IDR & Enter2Baris &
        '    "Kode Mata Uang - Debet : " & KodeMataUang_Debet & Enter2Baris &
        '    "Jumlah MUA - Debet : " & JumlahDebet_MUA & Enter2Baris &
        '    "Jumlah IDR - Debet : " & JumlahDebet_IDR & Enter2Baris &
        '    "")

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        KodeMataUang_Kredit = Kosongan
        KodeMataUang_Debet = Kosongan

        NomorID = 0
        txt_NomorBPPB.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalBPPB)
        TanggalTransaksi = TanggalKosong
        KontenComboSaranaPembayaran_Public_WPF(cmb_DariBuku, KodeMataUang_Semua)
        COAKredit = Kosongan
        txt_KursBI_Kredit.Text = Kosongan
        txt_KursBank_Kredit.Text = Kosongan
        txt_JumlahKredit.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_KeBuku, KodeMataUang_Semua)
        COADebet = Kosongan
        txt_KursBI_Debet.Text = Kosongan
        txt_KursBank_Debet.Text = Kosongan
        txt_JumlahDebet.Text = Kosongan
        txt_JumlahDebetIDR.Text = Kosongan
        txt_LabaRugiSelisihKurs.Text = Kosongan
        txt_Penanggungjawab.Text = Kosongan
        txt_JumlahKredit.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_UraianTransaksi)
        NomorJV = 0

        lbl_KursBI_Kredit.Visibility = Visibility.Collapsed
        lbl_KursBank_Kredit.Visibility = Visibility.Collapsed
        lbl_JumlahKredit.Visibility = Visibility.Collapsed
        lbl_JumlahKreditIDR.Visibility = Visibility.Collapsed
        lbl_KursBI_Debet.Visibility = Visibility.Collapsed
        lbl_KursBank_Debet.Visibility = Visibility.Collapsed
        lbl_JumlahDebet.Visibility = Visibility.Collapsed
        lbl_JumlahDebetIDR.Visibility = Visibility.Collapsed
        lbl_LabaRugiSelisihKurs.Visibility = Visibility.Collapsed
        txt_KursBI_Kredit.Visibility = Visibility.Collapsed
        txt_KursBank_Kredit.Visibility = Visibility.Collapsed
        txt_JumlahKredit.Visibility = Visibility.Collapsed
        txt_JumlahKreditIDR.Visibility = Visibility.Collapsed
        txt_KursBI_Debet.Visibility = Visibility.Collapsed
        txt_KursBank_Debet.Visibility = Visibility.Collapsed
        txt_JumlahDebet.Visibility = Visibility.Collapsed
        txt_JumlahDebetIDR.Visibility = Visibility.Collapsed
        txt_LabaRugiSelisihKurs.Visibility = Visibility.Collapsed

        ProsesResetForm = False

    End Sub



    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pemindahbukuan") + 1
        NomorBPPB = AwalanBPPB_PlusTahunBuku & NomorID
        txt_NomorBPPB.Text = NomorBPPB

    End Sub



    Private Sub txt_NomorBPPB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBPPB.TextChanged
        NomorBPPB = txt_NomorBPPB.Text
    End Sub


    Private Sub dtp_TanggalBPPB_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBPPB.SelectedDateChanged
        If dtp_TanggalBPPB.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBPPB)
            TanggalBPPB = TanggalFormatTampilan(dtp_TanggalBPPB.SelectedDate)
            LogikaKodeMataUang_Debet()
            LogikaKodeMataUang_Kredit()
        End If
    End Sub


    Private Sub cmb_DariBuku_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DariBuku.SelectionChanged
        If dtp_TanggalBPPB.Text = Kosongan Then
            cmb_DariBuku.SelectedValue = Kosongan
            PesanPeringatan("Silakan isi kolom 'Tanggal Pemindahbukuan'")
            dtp_TanggalBPPB.Focus()
            Return
        End If
        cmb_KeBuku.SelectedValue = Kosongan
        COAKredit = KonversiSaranaPembayaranKeCOA(cmb_DariBuku.SelectedValue)
        KodeMataUang_Kredit = AmbilValue_KodeMataUang_BerdasarkanCOA(COAKredit)
        txt_JumlahKredit.Text = Kosongan
        LogikaKodeMataUang_Kredit()
    End Sub

    Private Sub txt_KursBI_Kredit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursBI_Kredit.TextChanged
        KursBI_Kredit = AmbilAngka_Desimal(txt_KursBI_Kredit.Text)
        Perhitungan()
    End Sub

    Private Sub txt_KursBank_Kredit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursBank_Kredit.TextChanged
        KursBank_Kredit = AmbilAngka_Desimal(txt_KursBank_Kredit.Text)
    End Sub

    Private Sub txt_JumlahKredit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahKredit.TextChanged
        If KodeMataUang_Kredit = KodeMataUang_IDR Then
            JumlahKredit_MUA = AmbilAngka(txt_JumlahKredit.Text)
        Else
            JumlahKredit_MUA = AmbilAngka_Asing(txt_JumlahKredit.Text)
        End If
        JumlahKredit_IDR = AmbilValue_NilaiMataUang(KodeMataUang_Kredit, KursBI_Kredit, JumlahKredit_MUA)
        txt_JumlahKreditIDR.Text = JumlahKredit_IDR
        If KodeMataUang_Kredit = KodeMataUang_IDR And KodeMataUang_Debet = KodeMataUang_IDR Then
            txt_JumlahDebet.Text = JumlahKredit_IDR
        End If
        Perhitungan()
    End Sub

    Private Sub txt_JumlahKreditIDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahKreditIDR.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahKreditIDR)
    End Sub

    Sub LogikaKodeMataUang_Kredit()
        If KodeMataUang_Kredit = Kosongan Then
            lbl_JumlahKredit.Visibility = Visibility.Collapsed
            lbl_JumlahKreditIDR.Visibility = Visibility.Collapsed
            txt_JumlahKredit.Visibility = Visibility.Collapsed
            txt_JumlahKreditIDR.Visibility = Visibility.Collapsed
            txt_KursBI_Kredit.Text = Kosongan
            Return
        Else
            If KodeMataUang_Kredit = KodeMataUang_IDR Then
                txt_KursBI_Kredit.Text = 1
                txt_KursBank_Kredit.Text = 1
                lbl_KursBI_Kredit.Visibility = Visibility.Collapsed
                lbl_KursBank_Kredit.Visibility = Visibility.Collapsed
                lbl_JumlahKredit.Text = "Jumlah Kredit"
                lbl_JumlahKreditIDR.Visibility = Visibility.Collapsed
                txt_KursBI_Kredit.Visibility = Visibility.Collapsed
                txt_KursBank_Kredit.Visibility = Visibility.Collapsed
                txt_JumlahKreditIDR.Visibility = Visibility.Collapsed
                txt_JumlahKredit.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            Else
                If Not ProsesLoadingForm Then txt_KursBI_Kredit.Text = AmbilValue_KursTengahBI(KodeMataUang_Kredit, TanggalBPPB)
                txt_KursBank_Kredit.Text = Kosongan
                lbl_KursBI_Kredit.Visibility = Visibility.Visible
                lbl_KursBank_Kredit.Visibility = Visibility.Visible
                lbl_JumlahKredit.Text = "Jumlah Kredit (" & KodeMataUang_Kredit & ")"
                lbl_JumlahKreditIDR.Visibility = Visibility.Visible
                txt_KursBI_Kredit.Visibility = Visibility.Visible
                txt_KursBank_Kredit.Visibility = Visibility.Visible
                txt_JumlahKreditIDR.Visibility = Visibility.Visible
                txt_JumlahKredit.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            End If
            lbl_JumlahKredit.Visibility = Visibility.Visible
            txt_JumlahKredit.Visibility = Visibility.Visible
            txt_JumlahKredit.Focus()
        End If
        LogikaAntarKodeMataUang()
    End Sub


    Private Sub cmb_KeBuku_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KeBuku.SelectionChanged
        If dtp_TanggalBPPB.Text = Kosongan Then
            cmb_KeBuku.SelectedValue = Kosongan
            PesanPeringatan("Silakan isi kolom 'Tanggal Pemindahbukuan'")
            dtp_TanggalBPPB.Focus()
            Return
        End If
        COADebet = KonversiSaranaPembayaranKeCOA(cmb_KeBuku.SelectedValue)
        KodeMataUang_Debet = AmbilValue_KodeMataUang_BerdasarkanCOA(COADebet)
        txt_JumlahDebet.Text = Kosongan
        LogikaKodeMataUang_Debet()
    End Sub

    Private Sub txt_KursBI_Debet_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursBI_Debet.TextChanged
        KursBI_Debet = AmbilAngka_Desimal(txt_KursBI_Debet.Text)
        Perhitungan()
    End Sub

    Private Sub txt_KursBank_Debet_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursBank_Debet.TextChanged
        KursBank_Debet = AmbilAngka_Desimal(txt_KursBank_Debet.Text)
    End Sub

    Private Sub txt_JumlahDebet_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahDebet.TextChanged
        If KodeMataUang_Debet = KodeMataUang_IDR Then
            JumlahDebet_MUA = AmbilAngka(txt_JumlahDebet.Text)
        Else
            JumlahDebet_MUA = AmbilAngka_Asing(txt_JumlahDebet.Text)
        End If
        JumlahDebet_IDR = AmbilValue_NilaiMataUang(KodeMataUang_Debet, KursBI_Debet, JumlahDebet_MUA)
        txt_JumlahDebetIDR.Text = JumlahDebet_IDR
        Perhitungan()
    End Sub

    Private Sub txt_JumlahDebetIDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahDebetIDR.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahDebetIDR)
    End Sub

    Sub LogikaKodeMataUang_Debet()
        txt_JumlahDebet.IsReadOnly = False
        If KodeMataUang_Debet = Kosongan Then
            lbl_JumlahDebet.Visibility = Visibility.Collapsed
            lbl_JumlahDebetIDR.Visibility = Visibility.Collapsed
            txt_JumlahDebet.Visibility = Visibility.Collapsed
            txt_JumlahDebetIDR.Visibility = Visibility.Collapsed
            txt_KursBI_Debet.Text = Kosongan
            Return
        Else
            If KodeMataUang_Debet = KodeMataUang_IDR Then
                txt_KursBI_Debet.Text = 1
                txt_KursBank_Debet.Text = 1
                lbl_KursBI_Debet.Visibility = Visibility.Collapsed
                lbl_KursBank_Debet.Visibility = Visibility.Collapsed
                lbl_JumlahDebet.Text = "Jumlah Debet"
                lbl_JumlahDebetIDR.Visibility = Visibility.Collapsed
                txt_KursBI_Debet.Visibility = Visibility.Collapsed
                txt_KursBank_Debet.Visibility = Visibility.Collapsed
                txt_JumlahDebetIDR.Visibility = Visibility.Collapsed
                txt_JumlahDebet.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
                If KodeMataUang_Kredit = KodeMataUang_IDR Then
                    txt_JumlahDebet.IsReadOnly = True
                    txt_JumlahDebet.Text = JumlahKredit_IDR
                End If
            Else
                If Not ProsesLoadingForm Then txt_KursBI_Debet.Text = AmbilValue_KursTengahBI(KodeMataUang_Debet, TanggalBPPB)
                txt_KursBank_Debet.Text = Kosongan
                lbl_KursBI_Debet.Visibility = Visibility.Visible
                lbl_KursBank_Debet.Visibility = Visibility.Visible
                lbl_JumlahDebet.Text = "Jumlah Debet (" & KodeMataUang_Debet & ")"
                lbl_JumlahDebetIDR.Visibility = Visibility.Visible
                txt_KursBI_Debet.Visibility = Visibility.Visible
                txt_KursBank_Debet.Visibility = Visibility.Visible
                txt_JumlahDebetIDR.Visibility = Visibility.Visible
                txt_JumlahDebet.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            End If
            lbl_JumlahDebet.Visibility = Visibility.Visible
            txt_JumlahDebet.Visibility = Visibility.Visible
            txt_JumlahDebet.Focus()
        End If
        LogikaAntarKodeMataUang()
    End Sub

    Sub LogikaAntarKodeMataUang()
        If (KodeMataUang_Kredit <> KodeMataUang_IDR And KodeMataUang_Kredit <> Kosongan) Or (KodeMataUang_Debet <> KodeMataUang_IDR And KodeMataUang_Debet <> Kosongan) Then
            lbl_LabaRugiSelisihKurs.Visibility = Visibility.Visible
            txt_LabaRugiSelisihKurs.Visibility = Visibility.Visible
        Else
            lbl_LabaRugiSelisihKurs.Visibility = Visibility.Collapsed
            txt_LabaRugiSelisihKurs.Visibility = Visibility.Collapsed
            txt_LabaRugiSelisihKurs.Text = Kosongan
        End If
        Perhitungan()
    End Sub

    Sub Perhitungan()
        txt_LabaRugiSelisihKurs.Text = JumlahDebet_IDR - JumlahKredit_IDR
    End Sub


    Private Sub txt_LabaRugiSelisihKurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_LabaRugiSelisihKurs.TextChanged
        LabaRugiSelisihKurs = AmbilAngka(txt_LabaRugiSelisihKurs.Text)
        PemecahRibuanUntukTextBox_WPF(txt_LabaRugiSelisihKurs)
    End Sub


    Private Sub txt_Penanggungjawab_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Penanggungjawab.TextChanged
        Penanggungjawab = txt_Penanggungjawab.Text
    End Sub


    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = IsiValueVariabelRichTextBox(txt_UraianTransaksi)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalBPPB.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalBPPB, "Tanggal Pengajuan")
            Return
        End If


        If COAKredit = Kosongan Then
            PesanPeringatan("Silakan pilih 'Dari Buku'.")
            cmb_DariBuku.Focus()
            Return
        End If

        If COADebet = Kosongan Then
            PesanPeringatan("Silakan pilih 'Ke 'Buku'.")
            cmb_DariBuku.Focus()
            Return
        End If

        If Penanggungjawab = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Penanggungjawab'.")
            txt_Penanggungjawab.Focus()
            Return
        End If

        If JumlahKredit_MUA = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Kredit'.")
            txt_JumlahKredit.Focus()
            Return
        End If

        If JumlahDebet_MUA = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Debet'.")
            txt_JumlahDebet.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            If TahunBukuNormalTanpaPengajuan Then
                TanggalTransaksi = TanggalBPPB
                If Not KodeMataUang_Kredit = KodeMataUang_IDR Then JurnalAdjusment_Forex(COAKredit, TanggalTransaksi)
                If Not KodeMataUang_Debet = KodeMataUang_IDR Then JurnalAdjusment_Forex(COADebet, TanggalTransaksi)
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
            End If

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_Pemindahbukuan VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPPB & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBPPB) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & COAKredit & "', " &
                                  " '" & KodeMataUang_Kredit & "', " &
                                  " '" & DesimalFormatSimpan(KursBI_Kredit) & "', " &
                                  " '" & DesimalFormatSimpan(KursBank_Kredit) & "', " &
                                  " '" & DesimalFormatSimpan(JumlahKredit_MUA) & "', " &
                                  " '" & COADebet & "', " &
                                  " '" & KodeMataUang_Debet & "', " &
                                  " '" & DesimalFormatSimpan(KursBI_Debet) & "', " &
                                  " '" & DesimalFormatSimpan(KursBank_Debet) & "', " &
                                  " '" & DesimalFormatSimpan(JumlahDebet_MUA) & "', " &
                                  " '" & Penanggungjawab & "', " &
                                  " '" & UraianTransaksi & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            If TahunBukuNormalTanpaPengajuan Then
                jur_NomorJV = NomorJV
                TanggalTransaksi = TanggalBPPB
            End If

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_Pemindahbukuan SET " &
                                  " Nomor_BPPB              = '" & NomorBPPB & "', " &
                                  " Tanggal_BPPB            = '" & TanggalFormatSimpan(TanggalBPPB) & "', " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " COA_Kredit              = '" & COAKredit & "', " &
                                  " Kode_Mata_Uang_Kredit   = '" & KodeMataUang_Kredit & "', " &
                                  " Kurs_BI_Kredit          = '" & DesimalFormatSimpan(KursBI_Kredit) & "', " &
                                  " Kurs_Bank_Kredit        = '" & DesimalFormatSimpan(KursBank_Kredit) & "', " &
                                  " Jumlah_Kredit           = '" & DesimalFormatSimpan(JumlahKredit_MUA) & "', " &
                                  " COA_Debet               = '" & COADebet & "', " &
                                  " Kode_Mata_Uang_Debet    = '" & KodeMataUang_Debet & "', " &
                                  " Kurs_BI_Debet           = '" & DesimalFormatSimpan(KursBI_Debet) & "', " &
                                  " Kurs_Bank_Debet         = '" & DesimalFormatSimpan(KursBank_Debet) & "', " &
                                  " Jumlah_Debet            = '" & DesimalFormatSimpan(JumlahDebet_MUA) & "', " &
                                  " Penanggungjawab         = '" & Penanggungjawab & "', " &
                                  " Uraian_Transaksi        = '" & UraianTransaksi & "', " &
                                  " Nomor_JV                = '" & NomorJV & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPPB        = '" & NomorBPPB & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            HapusJurnal_BerdasarkanNomorJV(NomorJV)

        End If

        Dim LabaSelisihKurs As Decimal = 0
        Dim RugiSelisihKurs As Decimal = 0

        If LabaRugiSelisihKurs > 0 Then LabaSelisihKurs = LabaRugiSelisihKurs
        If LabaRugiSelisihKurs < 0 Then RugiSelisihKurs = -LabaRugiSelisihKurs

        'Simpan Jurnal :
        If TahunBukuNormalTanpaPengajuan Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
            'jur_JenisJurnal = AmbilValue_NamaAkun(COAKredit)
            jur_JenisJurnal = JenisJurnal_Pemindahbukuan
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = Kosongan
            jur_NomorInvoice = Kosongan
            jur_NamaProduk = Kosongan
            jur_KodeLawanTransaksi = KodeLawanTransaksi_Internal
            jur_NamaLawanTransaksi = NamaLawanTransaksi_Internal
            jur_UraianTransaksi = UraianTransaksi
            jur_Direct = 0

            'Simpan Jurnal - Debet :
            jur_Kurs = KursBI_Debet
            jur_KodeMataUang = KodeMataUang_Debet
            ___jurDebet(COADebet, JumlahDebet_MUA)
            ___jurDebet(KodeTautanCOA_LabaRugiSelisihKurs, RugiSelisihKurs)

            'Simpan Jurnal - Kredit :
            jur_Kurs = KursBI_Kredit
            jur_KodeMataUang = KodeMataUang_Kredit
            _______jurKredit(COAKredit, JumlahKredit_MUA)
            _______jurKredit(KodeTautanCOA_LabaRugiSelisihKurs, LabaSelisihKurs)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuPengawasanPemindahbukuan.StatusAktif Then usc_BukuPengawasanPemindahbukuan.TampilkanData()
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
        txt_NomorBPPB.IsReadOnly = True
        cmb_DariBuku.IsReadOnly = True
        txt_KursBI_Kredit.IsReadOnly = True
        cmb_KeBuku.IsReadOnly = True
        txt_KursBI_Debet.IsReadOnly = True
        txt_JumlahDebetIDR.IsReadOnly = True
        txt_LabaRugiSelisihKurs.IsReadOnly = True
    End Sub

End Class
