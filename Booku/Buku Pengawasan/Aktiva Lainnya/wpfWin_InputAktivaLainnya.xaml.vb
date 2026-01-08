Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputAktivaLainnya

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPAL
    Dim NomorBukti
    Dim TanggalBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim COADebet
    Dim COAKredit
    Dim NamaAkun
    Dim JumlahTransaksi
    Dim Keterangan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Aktiva Lain-lain"
            btn_Simpan.Content = teks_Simpan
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Aktiva Lain-lain"
            btn_Simpan.Content = teks_Perbarui
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0
        txt_NomorBPAL.Text = Kosongan
        txt_NomorBPAL.IsEnabled = False
        txt_NomorBukti.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalBukti)
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_KodeLawanTransaksi.IsEnabled = False
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.IsEnabled = False
        KosongkanValueElemenRichTextBox(txt_UraianTransaksi)
        txt_COADebet.Text = Kosongan
        txt_COADebet.IsEnabled = False
        txt_NamaAkun.Text = Kosongan
        txt_NamaAkun.IsEnabled = False
        txt_JumlahTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_AktivaLainnya") + 1
        NomorBPAL = AwalanBPAL_PlusTahunBuku & NomorID
        txt_NomorBPAL.Text = NomorBPAL

    End Sub


    Private Sub txt_NomorBPAL_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBPAL.TextChanged
        NomorBPAL = txt_NomorBPAL.Text
    End Sub


    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub


    Private Sub dtp_TanggalBukti_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBukti.SelectedDateChanged
        If dtp_TanggalBukti.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBukti)
            TanggalBukti = TanggalFormatTampilan(dtp_TanggalBukti.SelectedDate)
        End If
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = IsiValueVariabelRichTextBox(txt_UraianTransaksi)
    End Sub


    Private Sub txt_COADebet_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COADebet.TextChanged
        COADebet = txt_COADebet.Text
        txt_NamaAkun.Text = AmbilValue_NamaAkun(COADebet)
    End Sub


    Private Sub btn_PilihCOADebet_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOADebet.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_AktivaLainnya
        If txt_COADebet.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COADebet.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COADebet.Text = win_ListCOA.COATerseleksi
    End Sub


    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
    End Sub
    Private Sub txt_JumlahTransaksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahTransaksi.PreviewTextInput
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Isi Ulang Value :
        UraianTransaksi = IsiValueVariabelRichTextBox(txt_UraianTransaksi)
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)

        'Validasi Form :
        If NomorBukti = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Bukti'.")
            txt_NomorBukti.Focus()
            Return
        End If

        If dtp_TanggalBukti.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Bukti'.")
            dtp_TanggalBukti.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If COADebet = Kosongan Then
            PesanPeringatan("Silakan pilih 'Akun'.")
            txt_COADebet.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If


        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_AktivaLainnya VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPAL & "', " &
                                  " '" & NomorBukti & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & UraianTransaksi & "', " &
                                  " '" & COADebet & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_AktivaLainnya SET " &
                                  " Nomor_Bukti             = '" & NomorBukti & "', " &
                                  " Tanggal_Bukti           = '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " Kode_Lawan_Transaksi    = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi    = '" & NamaLawanTransaksi & "', " &
                                  " Uraian_Transaksi        = '" & UraianTransaksi & "', " &
                                  " COA_Debet               = '" & COADebet & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPAL        = '" & NomorBPAL & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuPengawasanAktivaLainnya.StatusAktif Then usc_BukuPengawasanAktivaLainnya.TampilkanData()
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
    End Sub

End Class
