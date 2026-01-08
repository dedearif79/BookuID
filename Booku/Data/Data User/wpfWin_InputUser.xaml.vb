Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputUser

    Public FungsiForm
    Public JudulForm
    Public ProsesSuntingDatabase As Boolean

    Dim UsernameInput
    Dim PasswordInput
    Dim NamaLengkapInput
    Dim JabatanInput
    Dim LevelInput
    Dim ClusterFinanceInput
    Dim ClusterAccountingInput
    Dim StatusAktifInput


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data User"
            txt_Username.IsReadOnly = False
            btn_Reset.IsEnabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data User"
            txt_Username.IsReadOnly = True
            btn_Reset.IsEnabled = False

            ' Jika user sedang mengedit dirinya sendiri
            If txt_Username.Text = UserAktif Then
                cmb_Jabatan.IsEnabled = False
                cmb_StatusAktif.IsEnabled = False
            Else
                cmb_Jabatan.IsEnabled = True
                cmb_StatusAktif.IsEnabled = True
            End If
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesIsiValueForm = True

        FungsiForm = Kosongan
        ProsesSuntingDatabase = False

        txt_Username.Text = Kosongan
        txt_Username.IsReadOnly = False

        txt_Password.Password = Kosongan

        txt_NamaLengkap.Text = Kosongan

        KontenComboJabatan()
        cmb_Jabatan.IsEnabled = True

        If SistemApprovalPerusahaan = True Then
            chk_ClusterFinance.IsEnabled = True
            chk_ClusterAccounting.IsEnabled = True
        Else
            chk_ClusterFinance.IsEnabled = False
            chk_ClusterAccounting.IsEnabled = False
        End If
        chk_ClusterFinance.IsChecked = False
        chk_ClusterAccounting.IsChecked = False

        KontenComboStatusAktif()
        cmb_StatusAktif.IsEnabled = True

        btn_Reset.IsEnabled = True

        ProsesIsiValueForm = False

    End Sub


    Sub KontenComboJabatan()
        cmb_Jabatan.Items.Clear()
        cmb_Jabatan.Items.Add(JabatanUser_SuperUser)
        If SistemApprovalPerusahaan = True Then
            cmb_Jabatan.Items.Add(JabatanUser_Direktur)
            cmb_Jabatan.Items.Add(JabatanUser_Manager)
            cmb_Jabatan.Items.Add(JabatanUser_Operator)
        Else
            cmb_Jabatan.Items.Add(JabatanUser_GeneralUser)
        End If
        cmb_Jabatan.Text = Kosongan
    End Sub


    Sub KontenComboStatusAktif()
        cmb_StatusAktif.Items.Clear()
        cmb_StatusAktif.Items.Add(Pilihan_YA_)
        cmb_StatusAktif.Items.Add(Pilihan_TIDAK_)
        cmb_StatusAktif.SelectedValue = Pilihan_YA_
    End Sub


    Private Sub cmb_Jabatan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Jabatan.SelectionChanged
        If ProsesIsiValueForm Then Return
        If ProsesLoadingForm Then Return

        Dim JabatanTerpilih = cmb_Jabatan.SelectedItem

        If JabatanTerpilih IsNot Nothing Then
            If JabatanTerpilih.ToString() = JabatanUser_SuperUser _
                Or JabatanTerpilih.ToString() = JabatanUser_Direktur _
                Or JabatanTerpilih.ToString() = JabatanUser_GeneralUser Then
                chk_ClusterFinance.IsEnabled = False
                chk_ClusterFinance.IsChecked = True
                chk_ClusterAccounting.IsEnabled = False
                chk_ClusterAccounting.IsChecked = True
            Else
                chk_ClusterFinance.IsEnabled = True
                chk_ClusterFinance.IsChecked = False
                chk_ClusterAccounting.IsEnabled = True
                chk_ClusterAccounting.IsChecked = False
            End If
        End If
    End Sub


    Private Sub chk_ClusterFinance_Checked(sender As Object, e As RoutedEventArgs) Handles chk_ClusterFinance.Checked
        If ProsesIsiValueForm Then Return
        If ProsesLoadingForm Then Return

        Dim JabatanTerpilih = cmb_Jabatan.SelectedItem
        If JabatanTerpilih IsNot Nothing Then
            If JabatanTerpilih.ToString() <> JabatanUser_SuperUser _
                And JabatanTerpilih.ToString() <> JabatanUser_GeneralUser _
                And JabatanTerpilih.ToString() <> JabatanUser_Direktur Then
                chk_ClusterAccounting.IsChecked = False
            End If
        End If
    End Sub


    Private Sub chk_ClusterAccounting_Checked(sender As Object, e As RoutedEventArgs) Handles chk_ClusterAccounting.Checked
        If ProsesIsiValueForm Then Return
        If ProsesLoadingForm Then Return

        Dim JabatanTerpilih = cmb_Jabatan.SelectedItem
        If JabatanTerpilih IsNot Nothing Then
            If JabatanTerpilih.ToString() <> JabatanUser_SuperUser _
                And JabatanTerpilih.ToString() <> JabatanUser_GeneralUser _
                And JabatanTerpilih.ToString() <> JabatanUser_Direktur Then
                chk_ClusterFinance.IsChecked = False
            End If
        End If
    End Sub


    Private Sub btn_Reset_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reset.Click
        ResetForm()
        FungsiForm = FungsiForm_TAMBAH
        Title = "Input Data User"
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        ' Validasi Username
        If txt_Username.Text = Kosongan Or txt_Username.Text = Nothing Then
            PesanPeringatan("Username harap diisi.")
            txt_Username.Focus()
            Return
        End If

        ' Validasi ketersediaan Username (hanya untuk TAMBAH)
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_User WHERE Username = '" & txt_Username.Text & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                PesanPeringatan("Username telah terdaftar." & Enter2Baris & "Silakan ketikkan Username yang lain.")
                txt_Username.Focus()
                AksesDatabase_General(Tutup)
                Return
            End If
            AksesDatabase_General(Tutup)
        End If

        ' Validasi Password
        If txt_Password.Password = Kosongan Or txt_Password.Password = Nothing Then
            PesanPeringatan("Password harap diisi.")
            txt_Password.Focus()
            Return
        End If

        If Microsoft.VisualBasic.Len(txt_Password.Password) < 8 Then
            PesanPeringatan("Karakter Password jangan kurang dari 8 digit.")
            txt_Password.Focus()
            Return
        End If

        ' Validasi Nama Lengkap
        If txt_NamaLengkap.Text = Kosongan Or txt_NamaLengkap.Text = Nothing Then
            PesanPeringatan("Nama Lengkap harap diisi.")
            txt_NamaLengkap.Focus()
            Return
        End If

        ' Validasi Jabatan
        If cmb_Jabatan.Text = Kosongan Or cmb_Jabatan.Text = Nothing Then
            PesanPeringatan("Silakan pilih Jabatan user.")
            cmb_Jabatan.Focus()
            Return
        End If

        ' Validasi Cluster
        If chk_ClusterFinance.IsChecked = False And chk_ClusterAccounting.IsChecked = False Then
            PesanPeringatan("Silakan pilih Cluster user.")
            Return
        End If

        ' Deklarasi Variabel
        UsernameInput = txt_Username.Text
        PasswordInput = txt_Password.Password
        JabatanInput = cmb_Jabatan.SelectedValue
        NamaLengkapInput = txt_NamaLengkap.Text

        LevelInput = 0
        If JabatanInput = JabatanUser_SuperUser Then LevelInput = LevelUser_09_SuperUser
        If JabatanInput = JabatanUser_GeneralUser Then LevelInput = levelUser_04_GeneralUser
        If JabatanInput = JabatanUser_Direktur Then LevelInput = LevelUser_03_Direktur
        If JabatanInput = JabatanUser_Manager Then LevelInput = LevelUser_02_Manager
        If JabatanInput = JabatanUser_Operator Then LevelInput = LevelUser_01_Operator

        If chk_ClusterFinance.IsChecked = True Then
            ClusterFinanceInput = 1
        Else
            ClusterFinanceInput = 0
        End If

        If chk_ClusterAccounting.IsChecked = True Then
            ClusterAccountingInput = 1
        Else
            ClusterAccountingInput = 0
        End If

        StatusAktifInput = 0
        If cmb_StatusAktif.SelectedValue = Pilihan_YA_ Then StatusAktifInput = 1
        If cmb_StatusAktif.SelectedValue = Pilihan_TIDAK_ Then StatusAktifInput = 0

        ' Proses Simpan/Edit
        ProsesSuntingDatabase = False

        AksesDatabase_General(Buka)

        ' Jika form berfungsi untuk menambah data/record
        If FungsiForm = FungsiForm_TAMBAH Then
            cmd = New OdbcCommand(" INSERT INTO tbl_User VALUES ('" &
                                  UsernameInput & "', '" &
                                  EnkripsiTeks(PasswordInput) & "', '" &
                                  LevelInput & "', '" &
                                  NamaLengkapInput & "', '" &
                                  JabatanInput & "', '" &
                                  ClusterFinanceInput & "', '" &
                                  ClusterAccountingInput & "', '" &
                                  StatusAktifInput & "' ) ",
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
        End If

        ' Jika form berfungsi untuk mengedit data/record
        If FungsiForm = FungsiForm_EDIT Then
            cmd = New OdbcCommand(" UPDATE tbl_User SET Password = '" & EnkripsiTeks(PasswordInput) & "', " &
                                  " Level = '" & LevelInput & "', " &
                                  " Nama = '" & NamaLengkapInput & "', " &
                                  " Jabatan = '" & JabatanInput & "', " &
                                  " Cluster_Finance = '" & ClusterFinanceInput & "', " &
                                  " Cluster_Accounting = '" & ClusterAccountingInput & "', " &
                                  " Status_Aktif = '" & StatusAktifInput & "' " &
                                  " WHERE Username = '" & UsernameInput & "' ",
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
        End If

        AksesDatabase_General(Tutup)

        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                ResetForm()
                FungsiForm = FungsiForm_TAMBAH
                Title = "Input Data User"
                PesanPemberitahuan("Data User berhasil disimpan.")
            End If
            If FungsiForm = FungsiForm_EDIT Then
                PesanPemberitahuan("Data User berhasil diedit.")
                Me.Close()
            End If
        Else
            PesanPeringatan("Data User gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_Jabatan.IsReadOnly = True
        cmb_StatusAktif.IsReadOnly = True
        txt_Username.MaxLength = 30
        txt_Password.MaxLength = 50
        txt_NamaLengkap.MaxLength = 100
    End Sub

End Class
