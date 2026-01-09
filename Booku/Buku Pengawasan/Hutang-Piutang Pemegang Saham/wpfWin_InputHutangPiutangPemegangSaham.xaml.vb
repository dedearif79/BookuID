Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputHutangPiutangPemegangSaham

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim TanggalPinjam
    Dim NomorBP
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahPinjaman
    Dim NomorKontrak
    Dim Keterangan
    Public NomorJV

    Public HutangPiutang = Kosongan
    Dim TabelPengawasan
    Dim KolomNomorBP
    Dim AwalanNomorBP
    Dim KolomCOASaranaPembayaran
    Dim COAUtama

    Dim SaranaPembayaran
    Public KodeMataUang

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!!!!")

        Select Case HutangPiutang
            Case hp_Hutang
                TabelPengawasan = "tbl_PengawasanHutangPemegangSaham"
                KolomNomorBP = "Nomor_BPHPS"
                KolomCOASaranaPembayaran = "COA_Debet"
                COAUtama = KodeTautanCOA_HutangPemegangSaham
                AwalanNomorBP = AwalanBPHPS_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPHPS"
                lbl_SaranaPembayaran.Text = "Sarana Pencairan"
                VisibilitasSaranaPembayaran(True)
            Case hp_Piutang
                TabelPengawasan = "tbl_PengawasanPiutangPemegangSaham"
                KolomNomorBP = "Nomor_BPPPS"
                KolomCOASaranaPembayaran = "COA_Kredit"
                COAUtama = KodeTautanCOA_PiutangPemegangSaham
                AwalanNomorBP = AwalanBPPPS_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPPPS"
                lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
                VisibilitasSaranaPembayaran(False)
            Case Else
                PesanUntukProgrammer("Tentukan dulu, Hutang atau Piutang...!!!")
        End Select

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input " & HutangPiutang & " Pemegang Saham"
            btn_Simpan.Content = teks_Simpan
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit " & HutangPiutang & " Pemegang Saham"
            btn_Simpan.Content = teks_Perbarui
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NomorKontrak.Text = dr.Item("Nomor_Kontrak")
                txt_JumlahPinjaman.Text = dr.Item("Jumlah_Pinjaman")
                COASaranaPembayaran = dr.Item(KolomCOASaranaPembayaran)
                If HutangPiutang = hp_Hutang Then
                    cmb_SaranaPembayaran.SelectedValue = KonversiCOAKeSaranaPembayaran(COASaranaPembayaran)
                Else
                    cmb_SaranaPembayaran.SelectedValue = Kosongan
                End If
                txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
                cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
                cmb_Pembebanan.SelectedValue = dr.Item("Pembebanan")
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then txt_SaldoAwal.Text = dr.Item("Saldo_Awal")
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            txt_SaldoAwal.IsReadOnly = True
        Else
            txt_SaldoAwal.IsReadOnly = False
        End If

        Me.Title = JudulForm

        ProsesLoadingForm = False


    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = Kosongan

        KodeMataUang = KodeMataUang_IDR
        KosongkanDatePicker(dtp_TanggalPinjam)
        txt_NomorBP.IsEnabled = False
        txt_NomorBP.Text = Kosongan
        txt_KodeLawanTransaksi.IsEnabled = False
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.IsEnabled = False
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_JumlahPinjaman.Text = Kosongan
        txt_NomorKontrak.Text = Kosongan
        lbl_SaranaPembayaran.IsEnabled = True
        cmb_SaranaPembayaran.IsEnabled = True
        KontenCombo_SaranaPembayaran()
        Reset_grb_Bank()
        lbl_TotalBank.Visibility = Visibility.Collapsed
        txt_TotalBank.Visibility = Visibility.Collapsed
        txt_TotalBank.IsEnabled = False

        KosongkanValueElemenRichTextBox(txt_Keterangan)

        JumlahPinjaman = 0
        COAUtama = Kosongan
        COASaranaPembayaran = Kosongan

        NomorJV = 0

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan) + 1
        NomorBP = AwalanNomorBP & NomorID
        txt_NomorBP.Text = NomorBP

    End Sub

    Sub KontenCombo_SaranaPembayaran()
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
    End Sub


    Sub VisibilitasSaranaPembayaran(Visibilitas As Boolean)
        lbl_SaranaPembayaran.Visibility = Visibility.Collapsed
        cmb_SaranaPembayaran.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_SaranaPembayaran.Visibility = Visibility.Visible
                cmb_SaranaPembayaran.Visibility = Visibility.Visible
            End If
        Else
            lbl_SaranaPembayaran.Visibility = Visibility.Collapsed
            cmb_SaranaPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub


    Private Sub dtp_TanggalPinjam_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPinjam.SelectedDateChanged
        If dtp_TanggalPinjam.Text <> Kosongan Then
            LogikaUmumInputanTanggal(dtp_TanggalPinjam)
            TanggalPinjam = TanggalFormatTampilan(dtp_TanggalPinjam.SelectedDate)
        End If
    End Sub

    Private Sub txt_NomorBP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub



    Private Sub txt_JumlahPinjaman_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPinjaman.TextChanged
        JumlahPinjaman = AmbilAngka(txt_JumlahPinjaman.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahPinjaman_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahPinjaman.PreviewTextInput
              
    End Sub


    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwal.TextChanged
        SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_SaldoAwal_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_SaldoAwal.PreviewTextInput
              
    End Sub
    'Private Sub txt_SaldoAwal_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_SaldoAwal.LostFocus
    '    If JenisTahunBuku = JenisTahunBuku_LAMPAU And SaldoAwal < JumlahPinjaman Then
    '        MsgBox("Silakan isi kolom 'Saldo' dengan benar.")
    '        txt_SaldoAwal.Text = JumlahPinjaman
    '        txt_SaldoAwal.Focus()
    '        Return
    '    End If
    'End Sub


    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As RoutedEventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub


    Dim COASaranaPembayaran
    Private Sub cmb_SaranaPembayaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        SaranaPembayaran = cmb_SaranaPembayaran.SelectedValue
        COASaranaPembayaran = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If COATermasukBank(COASaranaPembayaran) Then
            grb_Bank.Visibility = Visibility.Visible
            PembayaranViaBank = True
            KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
            Perhitungan()
        Else
            Reset_grb_Bank()
        End If
    End Sub


    '==================================================== PERHITUNGAN ====================================================
    Dim JumlahPencairan
    Dim SaldoAwal
    'Kenapa Harus ada Saldo Awal..? Apa bedanya dengan Jumlah Pinjaman..?
    'Jadi Gini... Contoh :
    'Ketika perusahaan meminjam uang sebesar 1.000.000 ke Lawan Transaksi, kemudian ada Biaya Administrasi Bank sebesar 5.000,- saat transfer uang tersebut,
    'dan biaya tersebut ditanggung oleh PERUSAHAAN, maka Saldo Awal Hutang Perusahaan ke Lawan Transaksi menjadi 1.005.000,-
    'jika pembebanannya ditambahkan ke hutang.
    Sub Perhitungan()

        SaldoAwal = JumlahPinjaman

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return

        txt_JumlahTransfer.Text = JumlahPinjaman

        If PembayaranViaBank = True Then
            Select Case HutangPiutang
                Case hp_Hutang
                    Select Case DitanggungOleh
                        Case DitanggungOleh_LawanTransaksi
                            SaldoAwal = JumlahTransfer
                            txt_JumlahTransfer.Text = JumlahTransfer
                        Case DitanggungOleh_Perusahaan
                            Select Case Pembebanan
                                Case Pembebanan_Dipotong
                                    SaldoAwal = JumlahTransfer
                                    txt_JumlahTransfer.Text = JumlahTransfer - BiayaAdministrasiBank
                                Case Pembebanan_Ditambahkan
                                    SaldoAwal = JumlahTransfer + BiayaAdministrasiBank
                                    txt_JumlahTransfer.Text = JumlahTransfer
                            End Select
                    End Select
                    txt_SaldoAwal.Text = SaldoAwal
                Case hp_Piutang
                    txt_JumlahTransfer.Text = JumlahTransfer
                    Select Case DitanggungOleh
                        Case DitanggungOleh_LawanTransaksi
                            txt_JumlahTransfer.Text = JumlahTransfer - BiayaAdministrasiBank
                        Case DitanggungOleh_Perusahaan
                            txt_JumlahTransfer.Text = JumlahTransfer
                    End Select
                    JumlahPencairan = JumlahTransfer
                    txt_SaldoAwal.Text = SaldoAwal
            End Select
        Else
            SaldoAwal = JumlahPinjaman
            JumlahPencairan = JumlahPinjaman
            txt_SaldoAwal.Text = JumlahPinjaman
        End If

    End Sub


    Dim PembayaranViaBank As Boolean
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim Pembebanan
    Dim JumlahTransfer
    Dim TotalBank
    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Visibility = Visibility.Collapsed
        txt_BiayaAdministrasiBank.Text = Kosongan
        cmb_DitanggungOleh.IsEnabled = False
        cmb_DitanggungOleh.SelectedValue = Kosongan
        lbl_Pembebanan.Visibility = Visibility.Collapsed
        cmb_Pembebanan.Visibility = Visibility.Collapsed
        cmb_Pembebanan.SelectedValue = Kosongan
        txt_JumlahTransfer.Text = Kosongan
        txt_TotalBank.Text = Kosongan
    End Sub
    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, AmbilAngka(JumlahPinjaman), JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
    End Sub
    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.IsEnabled = False
            cmb_DitanggungOleh.SelectedValue = Kosongan
        Else
            cmb_DitanggungOleh.IsEnabled = True
        End If
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_BiayaAdministrasiBank)
    End Sub
    Private Sub txt_BiayaAdministrasiBank_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BiayaAdministrasiBank.PreviewTextInput
              
    End Sub
    Private Sub cmb_DitanggungOleh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DitanggungOleh.SelectionChanged
        DitanggungOleh = cmb_DitanggungOleh.SelectedValue
        Select Case HutangPiutang
            Case hp_Hutang ' =========== HUTANG ========================
                cmb_Pembebanan.IsEnabled = True
                Select Case DitanggungOleh
                    Case DitanggungOleh_Perusahaan
                        KontenComboPembebanan_Public_WPF(cmb_Pembebanan)
                        lbl_Pembebanan.Visibility = Visibility.Visible
                        cmb_Pembebanan.Visibility = Visibility.Visible
                        cmb_Pembebanan.SelectedValue = Pembebanan_Dipotong
                    Case DitanggungOleh_LawanTransaksi
                        txt_BiayaAdministrasiBank.Text = Kosongan
                        lbl_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.SelectedValue = Kosongan
                    Case Kosongan
                        lbl_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.SelectedValue = Kosongan
                End Select
            Case hp_Piutang ' =========== PIUTANG =======================
                cmb_Pembebanan.IsEnabled = False 'Untuk sementara Combo Pembebanan dinonaktifkan untuk SISI PIUTANG. Pilihannya dipaksa ke : "DIPOTONG"
                Select Case DitanggungOleh
                    Case DitanggungOleh_Perusahaan
                        lbl_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.SelectedValue = Kosongan
                    Case DitanggungOleh_LawanTransaksi
                        KontenComboPembebanan_Public_WPF(cmb_Pembebanan)
                        lbl_Pembebanan.Visibility = Visibility.Visible
                        cmb_Pembebanan.Visibility = Visibility.Visible
                        cmb_Pembebanan.SelectedValue = Pembebanan_Dipotong
                    Case Kosongan
                        lbl_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.Visibility = Visibility.Collapsed
                        cmb_Pembebanan.SelectedValue = Kosongan
                End Select
        End Select
        Perhitungan()
    End Sub
    Private Sub cmb_Pembebanan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Pembebanan.SelectionChanged
        Pembebanan = cmb_Pembebanan.SelectedValue
        Perhitungan()
    End Sub
    Private Sub txt_JumlahTransfer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransfer.TextChanged
        JumlahTransfer = AmbilAngka(txt_JumlahTransfer.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahTransfer_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahTransfer.PreviewTextInput
              
    End Sub
    Private Sub txt_TotalBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalBank.TextChanged
        TotalBank = AmbilAngka(txt_TotalBank.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_TotalBank_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TotalBank.PreviewTextInput
              
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    'Tombol CRUD :
    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Validasi Kolom-kolom Tertentu :
        If dtp_TanggalPinjam.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Pinjam'.")
            dtp_TanggalPinjam.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            If HutangPiutang = hp_Hutang Then Pesan_Peringatan("Silakan isi kolom 'Kreditur'.")
            If HutangPiutang = hp_Piutang Then Pesan_Peringatan("Silakan isi kolom 'Debitur'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahPinjaman = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Jumlah Pinjaman'.")
            txt_JumlahPinjaman.Focus()
            Return
        End If

        If txt_SaldoAwal.Text = Kosongan Then '(Sudah benar pakai txt_SaldoAwal, bukan SaldoAwal)
            Pesan_Peringatan("Silakan isi kolom 'Saldo Awal'.")
            txt_SaldoAwal.Focus()
            Return
        End If

        If cmb_SaranaPembayaran.Visibility = Visibility.Visible Then
            If HutangPiutang = hp_Hutang And SaranaPembayaran = Kosongan Then
                Pesan_Peringatan("Silakan pilih 'Sarana Pembayaran'.")
                cmb_SaranaPembayaran.Focus()
                Return
            End If
        End If

        If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Ditanggung Oleh'.")
            cmb_DitanggungOleh.Focus()
            Return
        End If

        NomorJV = 0 'Tidak ada Jurnal saat Input maupun Edit.

        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBP & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " '" & JumlahPinjaman & "', " &
                                  " '" & SaldoAwal & "', " &
                                  " '" & COASaranaPembayaran & "', " &
                                  " '" & BiayaAdministrasiBank & "', " &
                                  " '" & DitanggungOleh & "', " &
                                  " '" & Pembebanan & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            'Update Data pada Tabel : tbl_PengawasanPiutangPemegangSaham 
            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  KolomNomorBP & "              = '" & NomorBP & "', " &
                                  " Kode_Lawan_Transaksi        = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi        = '" & NamaLawanTransaksi & "', " &
                                  " Tanggal_Transaksi           = '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " Jumlah_Pinjaman             = '" & JumlahPinjaman & "', " &
                                  " Saldo_Awal                  = '" & SaldoAwal & "', " &
                                  " Nomor_Kontrak               = '" & NomorKontrak & "', " &
                                  KolomCOASaranaPembayaran & "  = '" & COASaranaPembayaran & "', " &
                                  " Biaya_Administrasi_Bank     = '" & BiayaAdministrasiBank & "', " &
                                  " Ditanggung_Oleh             = '" & DitanggungOleh & "', " &
                                  " Pembebanan                  = '" & Pembebanan & "', " &
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

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If HutangPiutang = hp_Hutang Then usc_BukuPengawasanHutangPemegangSaham.TampilkanData()
            If HutangPiutang = hp_Piutang Then usc_BukuPengawasanPiutangPemegangSaham.TampilkanData()
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
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_SaldoAwal.IsReadOnly = True
        cmb_SaranaPembayaran.IsReadOnly = True
        cmb_DitanggungOleh.IsReadOnly = True
        cmb_Pembebanan.IsReadOnly = True
        txt_JumlahTransfer.IsReadOnly = True
        txt_TotalBank.IsReadOnly = True
    End Sub

End Class
