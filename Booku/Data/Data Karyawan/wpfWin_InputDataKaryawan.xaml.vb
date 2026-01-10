Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputDataKaryawan

    Public JudulForm
    Public FungsiForm
    Public ProsesSuntingDatabase As Boolean

    Dim NomorID_Tabel
    Dim TanggalRegistrasi
    Dim NomorIDKaryawan
    Dim NIK
    Dim NamaKaryawan
    Dim Jabatan
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan
    Dim StatusAktifKaryawan

    Dim NomorIDSudahTerdaftar As Boolean
    Dim NIKSudahTerdaftar As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Karyawan"
            txt_NomorIDKaryawan.IsReadOnly = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Karyawan"
            txt_NomorIDKaryawan.IsReadOnly = True
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        ProsesSuntingDatabase = False

        NomorID_Tabel = 0
        dtp_TanggalRegistrasi.SelectedDate = Today
        txt_NomorIDKaryawan.Text = Kosongan
        txt_NomorIDKaryawan.IsReadOnly = False
        txt_NIK.Text = Kosongan
        txt_NamaKaryawan.Text = Kosongan
        txt_Jabatan.Text = Kosongan
        txt_RekeningBank.Text = Kosongan
        txt_AtasNama.Text = Kosongan
        txt_Catatan.Text = Kosongan
        chk_StatusAktif.IsChecked = True

        NomorIDSudahTerdaftar = False
        NIKSudahTerdaftar = False

        ProsesResetForm = False

    End Sub



    Private Sub dtp_TanggalRegistrasi_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalRegistrasi.SelectedDateChanged
        TanggalRegistrasi = TanggalFormatTampilan(dtp_TanggalRegistrasi.SelectedDate)
    End Sub


    Private Sub txt_NomorIDKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorIDKaryawan.TextChanged
        NomorIDKaryawan = txt_NomorIDKaryawan.Text
    End Sub

    Private Sub txt_NomorIDKaryawan_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_NomorIDKaryawan.LostFocus
        If FungsiForm = FungsiForm_TAMBAH And NomorIDKaryawan <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID_Karyawan FROM tbl_DataKaryawan " &
                                  " WHERE Nomor_ID_Karyawan = '" & NomorIDKaryawan & "' ",
                                  KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                NomorIDSudahTerdaftar = True
            Else
                NomorIDSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NomorIDSudahTerdaftar = True Then
                PesanPeringatan("Nomor ID sudah pernah didaftarkan." & Enter2Baris &
                       "Silakan input nomor yang lain.")
                txt_NomorIDKaryawan.Text = Kosongan
                txt_NomorIDKaryawan.Focus()
                Return
            End If
        End If
    End Sub


    Private Sub txt_NIK_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NIK.TextChanged
        NIK = txt_NIK.Text
    End Sub

    Private Sub txt_NIK_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_NIK.LostFocus
        If NIK <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID_Karyawan, NIK FROM tbl_DataKaryawan " &
                                  " WHERE NIK = '" & NIK & "' ",
                                  KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                Dim NomorID_Registrasi_Telusur = dr.Item("Nomor_ID_Karyawan")
                If NomorID_Registrasi_Telusur <> NomorIDKaryawan Then
                    NIKSudahTerdaftar = True
                Else
                    NIKSudahTerdaftar = False
                End If
            Else
                NIKSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NIKSudahTerdaftar = True Then
                PesanPeringatan("NIK sudah pernah didaftarkan untuk karyawan yang lain." & Enter2Baris &
                       "Silakan input NIK yang lain.")
                txt_NIK.Text = Kosongan
                txt_NIK.Focus()
                Return
            End If
        End If
    End Sub


    Private Sub txt_NamaKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaKaryawan.TextChanged
        NamaKaryawan = txt_NamaKaryawan.Text
    End Sub


    Private Sub cmb_Jabatan_SelectionChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Jabatan.TextChanged
        Jabatan = txt_Jabatan.Text
    End Sub


    Private Sub txt_RekeningBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_RekeningBank.TextChanged
        RekeningBank = txt_RekeningBank.Text
    End Sub


    Private Sub txt_AtasNama_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AtasNama.TextChanged
        AtasNama = txt_AtasNama.Text
    End Sub


    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
    End Sub


    Private Sub chk_StatusAktif_Checked(sender As Object, e As RoutedEventArgs) Handles chk_StatusAktif.Checked
        StatusAktifKaryawan = 1
    End Sub

    Private Sub chk_StatusAktif_Unchecked(sender As Object, e As RoutedEventArgs) Handles chk_StatusAktif.Unchecked
        StatusAktifKaryawan = 0
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Beberapa Variabel :
        TanggalRegistrasi = TanggalFormatTampilan(dtp_TanggalRegistrasi.SelectedDate)
        Jabatan = txt_Jabatan.Text
        Catatan = txt_Catatan.Text

        If NomorIDSudahTerdaftar = True Or NIKSudahTerdaftar = True Then Return

        'Validasi Kolom-kolom tertentu :
        If NomorIDKaryawan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor ID'.")
            txt_NomorIDKaryawan.Focus()
            Return
        End If

        If NIK = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'NIK'.")
            txt_NIK.Focus()
            Return
        End If

        If NamaKaryawan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Karyawan'.")
            txt_NamaKaryawan.Focus()
            Return
        End If

        If Jabatan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Jabatan'.")
            txt_Jabatan.Focus()
            Return
        End If

        ProsesSuntingDatabase = False

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID_Tabel = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataKaryawan") + 1

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_DataKaryawan VALUES ( " &
                                  " '" & NomorID_Tabel & "', " &
                                  " '" & TanggalFormatSimpan(TanggalRegistrasi) & "', " &
                                  " '" & NomorIDKaryawan & "', " &
                                  " '" & NIK & "', " &
                                  " '" & NamaKaryawan & "', " &
                                  " '" & Jabatan & "', " &
                                  " '" & RekeningBank & "', " &
                                  " '" & AtasNama & "', " &
                                  " '" & Catatan & "', " &
                                  " '" & StatusAktifKaryawan & "' ) ",
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_DataKaryawan SET " &
                                  " Tanggal_Registrasi          = '" & TanggalFormatSimpan(TanggalRegistrasi) & "', " &
                                  " NIK                         = '" & NIK & "', " &
                                  " Nama_Karyawan               = '" & NamaKaryawan & "', " &
                                  " Jabatan                     = '" & Jabatan & "', " &
                                  " Rekening_Bank               = '" & RekeningBank & "', " &
                                  " Atas_Nama                   = '" & AtasNama & "', " &
                                  " Catatan                     = '" & Catatan & "', " &
                                  " Status_Aktif                = '" & StatusAktifKaryawan & "' " &
                                  " WHERE Nomor_ID_Karyawan     = '" & NomorIDKaryawan & "' ",
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

        End If

        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_DataKaryawan.StatusAktif Then usc_DataKaryawan.TampilkanData()
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
        txt_NomorIDKaryawan.MaxLength = 99
        txt_NIK.MaxLength = 99
        txt_NamaKaryawan.MaxLength = 99
        txt_RekeningBank.MaxLength = 99
        txt_AtasNama.MaxLength = 99
    End Sub

End Class
