Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputJadwalAngsuranAfiliasi


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public HutangPiutang
    Dim TabelAngsuran
    Dim KolomNomorBP
    Dim AwalanBP
    Dim KolomCOASaranaPembayaran

    Public NomorJV
    Public NomorID
    Public NomorBP
    Public KodeLawanTransaksi
    Dim SebagaiLembagaKeuangan As Boolean
    Dim LokasiLawanTransaksi
    Dim JenisPPh
    Dim KodeSetoran
    Dim AngsuranKe
    Dim TanggalJatuhTempo
    Dim Pokok
    Dim BagiHasil
    Dim TarifPPh As Decimal
    Dim JumlahPPh
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim JumlahDibayarkan
    Dim SaldoAkhir


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case HutangPiutang
            Case hp_Hutang
                TabelAngsuran = "tbl_JadwalAngsuranHutangAfiliasi"
                KolomNomorBP = "Nomor_BPHA"
                AwalanBP = AwalanBPHA_PlusTahunBuku
                KolomCOASaranaPembayaran = "COA_Kredit"
            Case hp_Piutang
                TabelAngsuran = "tbl_JadwalAngsuranPiutangAfiliasi"
                KolomNomorBP = "Nomor_BPPA"
                AwalanBP = AwalanBPPA_PlusTahunBuku
                KolomCOASaranaPembayaran = "COA_Debet"
            Case Else
                PesanUntukProgrammer("Tentukan dulu, hutang atau piutang..!!!")
        End Select

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Keuangan") = 1 Then SebagaiLembagaKeuangan = True
            If dr.Item("Keuangan") = 0 Then SebagaiLembagaKeuangan = False
            LokasiLawanTransaksi = dr.Item("Lokasi_WP")
        End If
        AksesDatabase_General(Tutup)

        If LokasiLawanTransaksi = LokasiWP_LuarNegeri Then
            grb_BiayaPPh.IsEnabled = False
            grb_BiayaPPh.Header = "Biaya PPh :"
            JenisPPh = JenisPPh_Pasal26
            KodeSetoran = KodeSetoran_102
        Else
            If SebagaiLembagaKeuangan = True Then
                grb_BiayaPPh.IsEnabled = False
                grb_BiayaPPh.Header = "Biaya PPh :"
                txt_TarifPPh.Text = Kosongan
                JenisPPh = Kosongan
                KodeSetoran = Kosongan
            Else
                grb_BiayaPPh.IsEnabled = True
                grb_BiayaPPh.Header = "PPh Pasal 23 :"
                txt_TarifPPh.Text = "15"
                JenisPPh = JenisPPh_Pasal23
                KodeSetoran = KodeSetoran_102
            End If
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Jadwal Angsuran " & HutangPiutang & " Afiliasi"
            btn_Simpan.Content = teks_Simpan
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE " & KolomNomorBP & " = '" & NomorBP & "' " &
                                  " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                AngsuranKe = dr.Item("Angsuran_Ke")
            Loop
            txt_AngsuranKe.Text = AngsuranKe + 1
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Jadwal Angsuran " & HutangPiutang & " Afiliasi"
            btn_Simpan.Content = teks_Perbarui
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE " & KolomNomorBP & " = '" & NomorBP & "' " &
                                  " AND Angsuran_Ke = '" & AngsuranKe & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorJV = 0
        KodeLawanTransaksi = Kosongan
        SebagaiLembagaKeuangan = False
        txt_AngsuranKe.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalJatuhTempo)
        txt_Pokok.Text = Kosongan
        txt_BagiHasil.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        txt_JumlahPPh.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_JumlahDibayarkan.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Private Sub txt_AngsuranKe_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AngsuranKe.TextChanged
        AngsuranKe = AmbilAngka(txt_AngsuranKe.Text)
    End Sub
    Private Sub txt_AngsuranKe_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_AngsuranKe.PreviewTextInput
    End Sub


    Private Sub dtp_TanggalJatuhTempo_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.Text <> Kosongan Then
            TanggalJatuhTempo = TanggalFormatTampilan(dtp_TanggalJatuhTempo.SelectedDate)
        End If
    End Sub


    Private Sub txt_Pokok_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pokok.TextChanged
        Pokok = AmbilAngka(txt_Pokok.Text)
        PerhitunganJumlahDibayarkan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_Pokok_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Pokok.PreviewTextInput
        
    End Sub


    Private Sub txt_BagiHasil_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BagiHasil.TextChanged
        BagiHasil = AmbilAngka(txt_BagiHasil.Text)
        PerhitunganPPh()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_BagiHasil_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BagiHasil.PreviewTextInput
        
    End Sub


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        TarifPPh = AmbilAngka_Desimal(txt_TarifPPh.Text)
        If TarifPPh > 20 Then
            PesanPeringatan("Silakan isi kolom 'Tarif PPh' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        PerhitunganPPh()
    End Sub


    Private Sub txt_JumlahPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPPh.TextChanged
        JumlahPPh = AmbilAngka(txt_JumlahPPh.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        If PPhDitanggung > JumlahPPh Then
            PesanPeringatan("Silakan isi kolom 'PPh Pasal 23 Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Text = Kosongan
            txt_PPhDitanggung.Focus()
            Return
        End If
        PerhitunganPPh()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_PPhDitanggung_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhDitanggung.PreviewTextInput
        
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PerhitunganJumlahDibayarkan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub


    Sub PerhitunganPPh()
        txt_JumlahPPh.Text = BagiHasil * Persen(TarifPPh)
        txt_PPhDipotong.Text = JumlahPPh - PPhDitanggung
        PerhitunganJumlahDibayarkan()
    End Sub


    Private Sub txt_JumlahDibayarkan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahDibayarkan.TextChanged
        JumlahDibayarkan = AmbilAngka(txt_JumlahDibayarkan.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub


    Sub PerhitunganJumlahDibayarkan()
        txt_JumlahDibayarkan.Text = Pokok + BagiHasil - PPhDipotong
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If Pokok = 0 Then
            PesanPeringatan("Silakan isi kolom 'Pokok'.")
            txt_Pokok.Focus()
            Return
        End If

        If grb_BiayaPPh.IsEnabled = True And TarifPPh = 0 Then
            PesanPeringatan("Silakan isi kolom 'Tarif PPh'.")
            txt_TarifPPh.Focus()
            Return
        End If

        If PPhDitanggung > JumlahPPh Then
            PesanPeringatan("Silakan isi kolom 'PPh Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelAngsuran) + 1

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan =
                " INSERT INTO " & TabelAngsuran & " VALUES ( " &
                " '" & NomorID & "', " &
                " '" & NomorBP & "', " &
                " '" & KodeLawanTransaksi & "', " &
                " '" & AngsuranKe & "', " &
                " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                " '" & TanggalKosongSimpan & "', " &
                " '" & Pokok & "', " &
                " '" & BagiHasil & "', " &
                " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                " '" & JumlahPPh & "', " &
                " '" & PPhDitanggung & "', " &
                " '" & PPhDipotong & "', " &
                " '" & JumlahDibayarkan & "', " &
                " '" & 0 & "', " &
                " '" & JenisPPh & "', " &
                " '" & KodeSetoran & "', " &
                " '" & Kosongan & "', " &
                " '" & 0 & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & NomorJV & "', " &
                " '" & Kosongan & "', " &
                " '" & TanggalKosongSimpan & "', " &
                " '" & Kosongan & "', " &
                " '" & 0 & "', " &
                " '" & UserAktif & "' ) "
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE " & TabelAngsuran & " SET " &
                                  KolomNomorBP & "              = '" & NomorBP & "', " &
                                  " Kode_Lawan_Transaksi        = '" & KodeLawanTransaksi & "', " &
                                  " Angsuran_Ke                 = '" & AngsuranKe & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Tanggal_Bayar               = '" & TanggalKosongSimpan & "', " &
                                  " Pokok                       = '" & Pokok & "', " &
                                  " Bagi_Hasil                  = '" & BagiHasil & "', " &
                                  " Tarif_PPh                   = '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " Jumlah_PPh                  = '" & JumlahPPh & "', " &
                                  " PPh_Ditanggung              = '" & PPhDitanggung & "', " &
                                  " PPh_Dipotong                = '" & PPhDipotong & "', " &
                                  " Jumlah_Dibayarkan           = '" & JumlahDibayarkan & "', " &
                                  " Denda                       = '" & 0 & "', " &
                                  " Jenis_PPh                   = '" & JenisPPh & "', " &
                                  " Kode_Setoran                = '" & KodeSetoran & "', " &
                                  KolomCOASaranaPembayaran & "  = '" & Kosongan & "', " &
                                  " Biaya_Administrasi_Bank     = '" & 0 & "', " &
                                  " Ditanggung_Oleh             = '" & Kosongan & "', " &
                                  " Nomor_JV                    = '" & 0 & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If HutangPiutang = hp_Hutang Then usc_BukuPengawasanHutangAfiliasi.TampilkanData_JadwalAngsuran()
            If HutangPiutang = hp_Piutang Then usc_BukuPengawasanPiutangAfiliasi.TampilkanData_JadwalAngsuran()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If


    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_JumlahPPh.IsReadOnly = True
        txt_PPhDipotong.IsReadOnly = True
        txt_JumlahDibayarkan.IsReadOnly = True
    End Sub


End Class
