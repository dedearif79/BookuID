Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputBankGaransi

    Public JudulForm
    Public FungsiForm
    Public ProsesSuntingDatabase As Boolean

    Public NomorID
    Dim NomorBPBG
    Dim NomorKontrak
    Dim TanggalTransaksi
    Dim NamaBank
    Dim Keperluan
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahTransaksi
    Dim BiayaProvisi
    Dim Keterangan
    Public NomorJV_Transaksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Bank Garansi"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Bank Garansi"
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        ProsesSuntingDatabase = False

        NomorID = 0
        dtp_TanggalTransaksi.SelectedDate = Today
        txt_NomorBPBG.Text = Kosongan
        txt_NomorKontrak.Text = Kosongan
        txt_NamaBank.Text = Kosongan
        txt_Keperluan.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        txt_BiayaProvisi.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        NomorJV_Transaksi = 0

        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BankGaransi") + 1
        NomorBPBG = AwalanBPBG_PlusTahunBuku & NomorID
        txt_NomorBPBG.Text = NomorBPBG

    End Sub


    Private Sub dtp_TanggalTransaksi_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
    End Sub


    Private Sub txt_NomorBPBG_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBPBG.TextChanged
        NomorBPBG = txt_NomorBPBG.Text
    End Sub


    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub


    Private Sub txt_NamaBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaBank.TextChanged
        NamaBank = txt_NamaBank.Text
    End Sub


    Private Sub txt_Keperluan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keperluan.TextChanged
        Keperluan = txt_Keperluan.Text
    End Sub


    Private Sub txt_KodeLawanTransaksi_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles txt_KodeLawanTransaksi.PreviewMouseLeftButtonUp
        btn_PilihMitra_Click(sender, e)
    End Sub

    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Pilihan_Semua
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeLawanTransaksi.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
    End Sub


    Private Sub txt_BiayaProvisi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaProvisi.TextChanged
        BiayaProvisi = AmbilAngka(txt_BiayaProvisi.Text)
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Beberapa Variabel :
        TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
        Keterangan = txt_Keterangan.Text

        'Validasi Form :
        If NomorKontrak = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Kontrak'.")
            txt_NomorKontrak.Focus()
            Return
        End If

        If NamaBank = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Bank'.")
            txt_NamaBank.Focus()
            Return
        End If

        If Keperluan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Keperluan'.")
            txt_Keperluan.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        ProsesSuntingDatabase = False

        AksesDatabase_Transaksi(Buka)

        If FungsiForm = FungsiForm_TAMBAH Then

            cmd = New OdbcCommand(" INSERT INTO tbl_BankGaransi VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPBG & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & NamaBank & "', " &
                                  " '" & Keperluan & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & BiayaProvisi & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & TanggalKosongSimpan & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV_Transaksi & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try

        End If

        If FungsiForm = FungsiForm_EDIT Then

            cmd = New OdbcCommand(" UPDATE tbl_BankGaransi SET " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " Nomor_Kontrak           = '" & NomorKontrak & "', " &
                                  " Nama_Bank               = '" & NamaBank & "', " &
                                  " Keperluan               = '" & Keperluan & "', " &
                                  " Kode_Lawan_Transaksi    = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi    = '" & NamaLawanTransaksi & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Biaya_Provisi           = '" & BiayaProvisi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " Nomor_JV_Transaksi      = '" & NomorJV_Transaksi & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPBG        = '" & NomorBPBG & "' ", KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try

        End If

        AksesDatabase_Transaksi(Tutup)

        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuBankGaransi.StatusAktif Then usc_BukuBankGaransi.TampilkanData()
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
        txt_NomorKontrak.MaxLength = 99
        txt_NamaBank.MaxLength = 99
        txt_Keperluan.MaxLength = 99
    End Sub

End Class
