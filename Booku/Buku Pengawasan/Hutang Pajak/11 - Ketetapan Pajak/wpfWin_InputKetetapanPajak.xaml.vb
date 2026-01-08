Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputKetetapanPajak

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Public NomorBPHP
    Dim Nomor As String
    Dim KodeJenisPajak As String
    Dim JenisPajak
    Dim MasaPajak_Awal
    Dim MasaPajak_Akhir
    Dim TahunPajak_Inputan As Integer
    Dim NomorKetetapan
    Dim TanggalKetetapan
    Dim TahunKetetapan
    Public KodeAkun_PokokPajak
    Public KodeAkun_Sanksi
    Dim NamaAkun_PokokPajak
    Dim NamaAkun_Sanksi
    Dim PokokPajak
    Dim Sanksi
    Dim JumlahKetetapan
    Dim Keterangan
    Public NomorJV

    Dim BulanAngka_Awal As Integer
    Dim BulanAngka_Akhir As Integer

    Dim KodeKombinasiPenomoran_Lama

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Ketetapan Pajak"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            KodeKombinasiPenomoran_Lama = Nomor & KodeJenisPajak & TahunPajak_Inputan
            JudulForm = "Edit Ketetapan Pajak"
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA " &
                                  " WHERE COA = '" & KodeAkun_PokokPajak & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NamaAkun_PokokPajak.Text = dr.Item("Nama_Akun")
            End If
            AksesDatabase_General(Tutup)
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub KontenCombo_MasaPajak_Awal()
        cmb_MasaPajak_Awal.Items.Clear()
        cmb_MasaPajak_Awal.Items.Add(Bulan_Januari)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Februari)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Maret)
        cmb_MasaPajak_Awal.Items.Add(Bulan_April)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Mei)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Juni)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Juli)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Agustus)
        cmb_MasaPajak_Awal.Items.Add(Bulan_September)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Oktober)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Nopember)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Desember)
        cmb_MasaPajak_Awal.Text = Kosongan
    End Sub

    Sub KontenCombo_MasaPajak_Akhir()

        cmb_MasaPajak_Akhir.Items.Clear()

        If BulanAngka_Awal <= 1 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Januari)
        If BulanAngka_Awal <= 2 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Februari)
        If BulanAngka_Awal <= 3 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Maret)
        If BulanAngka_Awal <= 4 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_April)
        If BulanAngka_Awal <= 5 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Mei)
        If BulanAngka_Awal <= 6 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Juni)
        If BulanAngka_Awal <= 7 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Juli)
        If BulanAngka_Awal <= 8 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Agustus)
        If BulanAngka_Awal <= 9 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_September)
        If BulanAngka_Awal <= 10 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Oktober)
        If BulanAngka_Awal <= 11 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Nopember)
        If BulanAngka_Awal <= 12 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Desember)

        If BulanAngka_Awal > BulanAngka_Akhir Then cmb_MasaPajak_Akhir.Text = Kosongan

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorBPHP = Kosongan
        txt_Nomor.Text = Kosongan
        txt_KodeJenisPajak.Text = Kosongan
        KontenCombo_MasaPajak_Awal()
        cmb_MasaPajak_Akhir.Items.Clear()
        cmb_MasaPajak_Akhir.Text = Kosongan
        txt_TahunPajak_Inputan.Text = Kosongan
        dtp_TanggalKetetapan.text = Kosongan
        txt_NomorKetetapan.Text = Kosongan
        txt_PokokPajak.Text = Kosongan
        txt_KodeAkun_PokokPajak.Text = Kosongan
        txt_NamaAkun_PokokPajak.Text = Kosongan
        txt_Sanksi.Text = Kosongan
        txt_KodeAkun_Sanksi.Text = Kosongan
        txt_NamaAkun_Sanksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)
        txt_JumlahKetetapan.Text = Kosongan
        NomorJV = 0

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_NomorKetetapan()

        If ProsesResetForm = True Then Return

        Dim KodeKombinasiPenomoran_Dibaca = Nomor & KodeJenisPajak & TahunPajak_Inputan
        Dim KombinasiNomorSudahAda As Boolean = False
        Dim LanjutkanPenomoran As Boolean = True

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak " &
                              " WHERE Nomor             = '" & Nomor & "' " &
                              " AND Kode_Jenis_Pajak    = '" & KodeJenisPajak & "' " &
                              " AND Tahun_Pajak         = '" & TahunPajak_Inputan & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then KombinasiNomorSudahAda = True
        AksesDatabase_Transaksi(Tutup)

        If FungsiForm = FungsiForm_TAMBAH Then
            If KombinasiNomorSudahAda Then LanjutkanPenomoran = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            If KombinasiNomorSudahAda And KodeKombinasiPenomoran_Lama <> KodeKombinasiPenomoran_Dibaca Then LanjutkanPenomoran = False
        End If

        If LanjutkanPenomoran = False Then
            MsgBox("Kombinasi Nomor, Kode Jenis Pajak dan Tahun Ketetapan sudah ada." & Enter2Baris &
                   "Silakan isi kolom-kolom tersebut dengan data yang lain..!")
            txt_Nomor.Text = Kosongan
            txt_KodeJenisPajak.Text = Kosongan
            txt_TahunPajak_Inputan.Text = Kosongan
            txt_NomorKetetapan.Text = Kosongan
            txt_Nomor.Focus()
            Return
        End If

        If Nomor <> Kosongan And KodeJenisPajak <> Kosongan And TahunPajak_Inputan <> 0 And dtp_TanggalKetetapan.Text <> Kosongan _
            Then
            txt_NomorKetetapan.Text =
                Nomor & "/" &
                KodeJenisPajak & "/" &
                Right(TahunPajak_Inputan, 2) & "/" &
                KodeKPP_Perusahaan & "/" &
                Right(AmbilTahun_DariTanggal(TanggalKetetapan), 2)
        Else
            txt_NomorKetetapan.Text = Kosongan
        End If

    End Sub




    Private Sub dtp_TanggalKetetapan_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalKetetapan.SelectedDateChanged
        If dtp_TanggalKetetapan.Text <> Kosongan Then
            KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_TanggalKetetapan)
            KunciTanggalBulanDanTahun_TidakBolehLebihDari_WPF(dtp_TanggalKetetapan, Today.Day, Today.Month, Today.Year)
            TanggalKetetapan = dtp_TanggalKetetapan.SelectedDate
            TahunKetetapan = dtp_TanggalKetetapan.SelectedDate.Value.Year
        End If
    End Sub


    Private Sub dtp_TanggalKetetapan_LostFocus(sender As Object, e As RoutedEventArgs) Handles dtp_TanggalKetetapan.LostFocus
        If dtp_TanggalKetetapan.Text <> Kosongan Then SistemPenomoranOtomatis_NomorKetetapan()
    End Sub


    Private Sub txt_Nomor_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Nomor.TextChanged
        Nomor = txt_Nomor.Text
    End Sub
    Private Sub txt_Nomor_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Nomor.PreviewTextInput
              
    End Sub
    Private Sub txt_Nomor_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_Nomor.LostFocus
        SistemPenomoranOtomatis_NomorKetetapan()
    End Sub


    Private Sub txt_KodeJenisPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeJenisPajak.TextChanged
        KodeJenisPajak = txt_KodeJenisPajak.Text
        If KodeJenisPajak = Kosongan Then
            txt_JenisPajak.Text = Kosongan
        End If
        If PanjangTeks(KodeJenisPajak) = 3 Then
            If AmbilTeksKiri(KodeJenisPajak.ToString, 1) = "1" Then
                grb_PokokPajak.Visibility = Visibility.Collapsed
            Else
                grb_PokokPajak.Visibility = Visibility.Visible
            End If
        End If
    End Sub
    Private Sub txt_KodeJenisPajak_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KodeJenisPajak.PreviewTextInput
              
    End Sub
    Private Sub txt_KodeJenisPajak_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_KodeJenisPajak.LostFocus
        If ProsesLoadingForm = False And ProsesResetForm = False Then
            If KodeJenisPajak <> Kosongan Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_JenisPajak " &
                                      " WHERE Kode_Jenis_Pajak = '" & KodeJenisPajak & "' ", KoneksiDatabaseGeneral)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    txt_JenisPajak.Text = dr.Item("Jenis_Pajak")
                    SistemPenomoranOtomatis_NomorKetetapan()
                Else
                    txt_KodeJenisPajak.Text = Kosongan
                    txt_JenisPajak.Text = Kosongan
                    MsgBox("'Kode Jenis Pajak' tidak terdaftar di sistem." & Enter2Baris & "Silakan input kode yang sesuai.")
                    txt_KodeJenisPajak.Focus()
                End If
                AksesDatabase_General(Tutup)
            End If
        End If
    End Sub


    Private Sub txt_JenisPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JenisPajak.TextChanged
        JenisPajak = txt_JenisPajak.Text
    End Sub


    Private Sub cmb_MasaPajak_Awal_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MasaPajak_Awal.SelectionChanged
        MasaPajak_Awal = cmb_MasaPajak_Awal.SelectedValue
        BulanAngka_Awal = KonversiBulanKeAngka(MasaPajak_Awal)
        KontenCombo_MasaPajak_Akhir()
    End Sub


    Private Sub cmb_MasaPajak_Akhir_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MasaPajak_Akhir.SelectionChanged
        MasaPajak_Akhir = cmb_MasaPajak_Akhir.SelectedValue
        BulanAngka_Akhir = KonversiBulanKeAngka(MasaPajak_Akhir)
    End Sub


    Private Sub txt_TahunPajak_Inputan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TahunPajak_Inputan.TextChanged
        TahunPajak_Inputan = AmbilAngka(txt_TahunPajak_Inputan.Text)
        If TahunPajak > TahunKetetapan Then
            PesanPeringatan("Silakan isi Tahun Pajak dengan benar!")
            txt_TahunPajak_Inputan.Text = Kosongan
            txt_TahunPajak_Inputan.Focus()
        End If
    End Sub
    Private Sub txt_TahunPajak_Inputan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TahunPajak_Inputan.PreviewTextInput
              
    End Sub
    Private Sub txt_TahunPajak_Inputan_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_TahunPajak_Inputan.LostFocus
        Dim JumlahKarakterTahunPajak As Integer = Microsoft.VisualBasic.Len(TahunPajak_Inputan.ToString)
        If JumlahKarakterTahunPajak <> 4 Then
            MsgBox("Silakan isi kolom 'Tahun Pajak' dengan benar..!")
            txt_TahunPajak_Inputan.Focus()
            Return
        End If
        SistemPenomoranOtomatis_NomorKetetapan()
    End Sub


    Private Sub txt_NomorKetetapan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorKetetapan.TextChanged
        NomorKetetapan = txt_NomorKetetapan.Text
    End Sub


    Private Sub txt_PokokPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PokokPajak.TextChanged
        PokokPajak = AmbilAngka(txt_PokokPajak.Text)
        Perhitungan_JumlahKetetapan()
        PemecahRibuanUntukTextBox_WPF(txt_PokokPajak)
    End Sub
    Private Sub txt_PokokPajak_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PokokPajak.PreviewTextInput
              
    End Sub


    Private Sub txt_KodeAkun_PokokPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAkun_PokokPajak.TextChanged
        KodeAkun_PokokPajak = txt_KodeAkun_PokokPajak.Text
        If KodeAkun_PokokPajak = Kosongan Then txt_NamaAkun_PokokPajak.Text = Kosongan
    End Sub
    Private Sub txt_KodeAkun_PokokPajak_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles txt_KodeAkun_PokokPajak.MouseLeftButtonDown
        btn_PilihCOA_PokokPajak_Click(sender, e)
    End Sub

    Private Sub btn_PilihCOA_PokokPajak_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA_PokokPajak.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        If txt_KodeAkun_PokokPajak.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = KodeAkun_PokokPajak
            win_ListCOA.NamaAkunTerseleksi = NamaAkun_PokokPajak
        End If
        win_ListCOA.ListAkun = ListAkun_PokokPajak
        win_ListCOA.ShowDialog()
        txt_KodeAkun_PokokPajak.Text = win_ListCOA.COATerseleksi
        txt_NamaAkun_PokokPajak.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub


    Private Sub txt_NamaAkun_PokokPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_PokokPajak.TextChanged
        NamaAkun_PokokPajak = txt_NamaAkun_PokokPajak.Text
    End Sub


    Private Sub txt_Sanksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Sanksi.TextChanged
        Sanksi = AmbilAngka(txt_Sanksi.Text)
        If Sanksi = 0 Then
            txt_KodeAkun_Sanksi.Text = Kosongan
        Else
            txt_KodeAkun_Sanksi.Text = KodeTautanCOA_BiayaKetetapanPajak
        End If
        Perhitungan_JumlahKetetapan()
        PemecahRibuanUntukTextBox_WPF(txt_Sanksi)
    End Sub
    Private Sub txt_Sanksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Sanksi.PreviewTextInput
              
    End Sub


    Private Sub txt_KodeAkun_Sanksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAkun_Sanksi.TextChanged
        KodeAkun_Sanksi = txt_KodeAkun_Sanksi.Text
        If KodeAkun_Sanksi = Kosongan Then
            txt_NamaAkun_Sanksi.Text = Kosongan
        Else
            txt_NamaAkun_Sanksi.Text = AmbilValue_NamaAkun(KodeAkun_Sanksi)
        End If
    End Sub


    Private Sub txt_NamaAkun_Sanksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun_Sanksi.TextChanged
        NamaAkun_Sanksi = txt_NamaAkun_Sanksi.Text
    End Sub


    Private Sub txt_JumlahKetetapan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahKetetapan.TextChanged
        JumlahKetetapan = AmbilAngka(txt_JumlahKetetapan.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahKetetapan)
    End Sub
    Private Sub txt_JumlahKetetapan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahKetetapan.PreviewTextInput
              
    End Sub
    Sub Perhitungan_JumlahKetetapan()
        txt_JumlahKetetapan.Text = PokokPajak + Sanksi
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If Nomor = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor'.")
            txt_Nomor.Focus()
            Return
        End If

        If KodeJenisPajak = Kosongan Then
            MsgBox("Silakan isi kolom 'Kode Jenis Pajak'.")
            txt_KodeJenisPajak.Focus()
            Return
        End If

        If MasaPajak_Awal = Kosongan Then
            MsgBox("Silakan pilih 'Masa Pajak'.")
            cmb_MasaPajak_Awal.Focus()
            Return
        End If

        If MasaPajak_Akhir = Kosongan Then
            MsgBox("Silakan pilih 'Masa Pajak'.")
            cmb_MasaPajak_Akhir.Focus()
            Return
        End If

        If TahunPajak_Inputan = 0 Then
            MsgBox("Silakan isi kolom 'Tahun Pajak'.")
            txt_TahunPajak_Inputan.Focus()
            Return
        End If

        If PokokPajak + Sanksi = 0 Then
            MsgBox("Silakan isi kolom 'Pokok Pajak' dan/atau kolom 'Sanksi'.")
            txt_PokokPajak.Focus()
            Return
        End If

        If PokokPajak > 0 And KodeAkun_PokokPajak = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun' untuk Pokok Pajak.")
            txt_KodeAkun_PokokPajak.Focus()
            Return
        End If

        If JumlahKetetapan = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Ketetapan'.")
            txt_JumlahKetetapan.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_KetetapanPajak") + 1

            NomorBPHP = AwalanBPKP_PlusTahunBuku & NomorID

            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_KetetapanPajak VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & Nomor & "', " &
                                  " '" & KodeJenisPajak & "', " &
                                  " '" & JenisPajak & "', " &
                                  " '" & MasaPajak_Awal & "', " &
                                  " '" & MasaPajak_Akhir & "', " &
                                  " '" & TahunPajak_Inputan & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKetetapan) & "', " &
                                  " '" & NomorKetetapan & "', " &
                                  " '" & NomorBPHP & "', " &
                                  " '" & KodeAkun_PokokPajak & "', " &
                                  " '" & JumlahKetetapan & "', " &
                                  " '" & PokokPajak & "', " &
                                  " '" & Sanksi & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "') ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_KetetapanPajak SET " &
                                  " Nomor                   = '" & Nomor & "', " &
                                  " Kode_Jenis_Pajak        = '" & KodeJenisPajak & "', " &
                                  " Jenis_Pajak             = '" & JenisPajak & "', " &
                                  " Masa_Pajak_Awal         = '" & MasaPajak_Awal & "', " &
                                  " Masa_Pajak_Akhir        = '" & MasaPajak_Akhir & "', " &
                                  " Tahun_Pajak             = '" & TahunPajak_Inputan & "', " &
                                  " Tanggal_Ketetapan       = '" & TanggalFormatSimpan(TanggalKetetapan) & "', " &
                                  " Nomor_Ketetapan         = '" & NomorKetetapan & "', " &
                                  " Nomor_BPHP              = '" & NomorBPHP & "', " &
                                  " Kode_Akun_Pokok_Pajak   = '" & KodeAkun_PokokPajak & "', " &
                                  " Jumlah_Ketetapan        = '" & JumlahKetetapan & "', " &
                                  " Pokok_Pajak             = '" & PokokPajak & "', " &
                                  " Sanksi                  = '" & Sanksi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " Nomor_JV                = '" & NomorJV & "' " &
                                  " WHERE Nomor_ID          = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Hapus Data Jurnal yang Lama :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                       " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()

            jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalKetetapan)
            jur_JenisJurnal = JenisJurnal_AdjusmentForex
            jur_JenisJurnal = JenisJurnal_KetetapanPajak
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalKetetapan
            jur_NomorInvoice = NomorKetetapan
            jur_NomorFakturPajak = Kosongan
            jur_KodeLawanTransaksi = KodeLawanTransaksi_DJP
            jur_NamaLawanTransaksi = NamaLawanTransaksi_DJP
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeAkun_PokokPajak, PokokPajak)
            ___jurDebet(KodeAkun_Sanksi, Sanksi)
            _______jurKredit(KodeTautanCOA_HutangKetetapanPajak, Sanksi + PokokPajak)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If usc_BukuPengawasanKetetapanPajak.StatusAktif Then usc_BukuPengawasanKetetapanPajak.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_JenisPajak.IsReadOnly = True
        txt_NomorKetetapan.IsReadOnly = True
        cmb_MasaPajak_Awal.IsReadOnly = True
        cmb_MasaPajak_Akhir.IsReadOnly = True
        txt_KodeAkun_PokokPajak.IsReadOnly = True
        txt_NamaAkun_PokokPajak.IsReadOnly = True
        txt_KodeAkun_Sanksi.IsReadOnly = True
        txt_NamaAkun_Sanksi.IsReadOnly = True
        btn_PilihCOA_Sanksi.IsEnabled = False
        txt_JumlahKetetapan.IsReadOnly = True
        grb_PokokPajak.Visibility = Visibility.Collapsed
    End Sub

End Class
