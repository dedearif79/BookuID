Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc
Imports System.IO


Public Class wpfWin_Login

    Dim UsernameInput As String
    Dim PasswordInput As String
    Dim Password As String


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        If ID_CPU = "BFEBFBFF000906A3" Then
            txt_UsernameInput.Text = UsernameAppDeveloper
            txt_PasswordInput.Password = DekripsiTeks(PasswordAppDeveloper_Enk)
            SalinFile(Path.Combine("C:\BookuID\Booku\Attach\Notes", "0003.conf"), Path.Combine("D:\VB .Net Project\BookuID\Booku\bin\Debug\net8.0-windows\Attach\Notes", "0003.conf"))
        End If
        PerbaruiVariabelTerkaitServer()

    End Sub


    Sub ResetForm()
        chk_TampilkanPassword.IsChecked = False
        txt_PasswordInput.Password = Kosongan
        txt_UsernameInput.Text = Kosongan
        UsernameInput = Kosongan
        PasswordInput = "Udlkcmdjd0298*74hdldj$%123654"
        StatusLogin = False
        txt_UsernameInput.Focus()
        btn_OK.IsEnabled = False
    End Sub


    Sub LogikaTombolOK()
        If txt_UsernameInput.Text <> Kosongan And txt_PasswordInput.Password.Length >= 8 Then
            btn_OK.IsEnabled = True
        Else
            btn_OK.IsEnabled = False
        End If
    End Sub

    Private Sub txt_UsernameInput_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UsernameInput.TextChanged
        UsernameInput = txt_UsernameInput.Text
        LogikaTombolOK()
    End Sub

    Private Sub txt_PasswordInput_TextChanged(sender As Object, e As EventArgs) Handles txt_PasswordInput.PasswordChanged
        PasswordInput = txt_PasswordInput.Password
        LogikaTombolOK()
    End Sub

    'SCRIPT LOGIN BERHASIL : .................!!!!!!!!!!!!!!!
    Sub LoginSukses()

        StatusLogin = True
        PengulanganLogin = 0

        win_BOOKU.mnu_AppDeveloper.Visibility = Visibility.Collapsed

        Select Case LevelUserAktif
            Case LevelUser_01_Operator
                win_BOOKU.StatusMenuLevel_1_Operator()
            Case LevelUser_02_Manager
                win_BOOKU.StatusMenuLevel_2_Manager()
            Case LevelUser_03_Direktur
                win_BOOKU.StatusMenuLevel_3_Direktur()
            Case levelUser_04_GeneralUser
                win_BOOKU.StatusMenuLevel_4_GeneralUser()
            Case LevelUser_09_SuperUser
                win_BOOKU.StatusMenuLevel_9_SuperUser()
            Case LevelUser_81_TimIT
                win_BOOKU.StatusMenuLevel_81_TimIT()
            Case LevelUser_99_AppDeveloper
                win_BOOKU.StatusMenuLevel_99_AppDeveloper()
        End Select

        If LevelUserAktif >= LevelUser_09_SuperUser Then
            win_BOOKU.mnu_DataUser.IsEnabled = True
        Else
            win_BOOKU.mnu_DataUser.IsEnabled = False
        End If

        KeteranganCluster = Nothing
        If ClusterFinance = 1 Then KeteranganCluster = KeteranganCluster & "Finance  -  "
        If ClusterAccounting = 1 Then KeteranganCluster = KeteranganCluster & "Accounting  -  "
        KeteranganCluster = StrReverse(Mid(StrReverse(KeteranganCluster), 6))

        win_BOOKU.mnu_GantiPeran.Visibility = Visibility.Collapsed
        win_BOOKU.mnu_PeranTimIT.Visibility = Visibility.Collapsed
        win_BOOKU.mnu_PeranAppDeveloper.Visibility = Visibility.Collapsed
        If JabatanUserAktif = JabatanUser_AppDeveloper _
            Or JabatanUserAktif = JabatanUser_TimIT _
            Or JabatanUserAktif = JabatanUser_SuperUser _
            Or JabatanUserAktif = JabatanUser_GeneralUser _
            Then
            If SistemApprovalPerusahaan = True Then win_BOOKU.mnu_GantiPeran.Visibility = Visibility.Visible
            If JabatanUserAktif = JabatanUser_SuperUser _
                Or JabatanUserAktif = JabatanUser_GeneralUser _
                Then
                LevelUserAktif = LevelUser_03_Direktur
                win_BOOKU.mnu_PeranOperator.IsEnabled = True
                win_BOOKU.mnu_PeranManager.IsEnabled = True
                win_BOOKU.mnu_PeranDirektur.IsEnabled = False
                win_BOOKU.mnu_PeranTimIT.IsEnabled = True
                win_BOOKU.mnu_PeranAppDeveloper.IsEnabled = True
                'win_BOOKU.tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_Direktur & "  |  " & KeteranganCluster
                If SistemApprovalPerusahaan = True Then Pesan_Informasi("Anda login sebagai DIREKTUR.")
            End If
            If JabatanUserAktif = JabatanUser_TimIT Then
                win_BOOKU.mnu_PeranTimIT.Visibility = Visibility.Visible
                win_BOOKU.mnu_PeranOperator.IsEnabled = True
                win_BOOKU.mnu_PeranManager.IsEnabled = True
                win_BOOKU.mnu_PeranDirektur.IsEnabled = True
                win_BOOKU.mnu_PeranTimIT.IsEnabled = False
                win_BOOKU.mnu_PeranAppDeveloper.IsEnabled = True
                'win_BOOKU.tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_TimIT & "  |  " & KeteranganCluster
            End If
            If JabatanUserAktif = JabatanUser_AppDeveloper Then
                win_BOOKU.mnu_PeranTimIT.Visibility = Visibility.Visible
                win_BOOKU.mnu_PeranAppDeveloper.Visibility = Visibility.Visible
                win_BOOKU.mnu_PeranOperator.IsEnabled = True
                win_BOOKU.mnu_PeranManager.IsEnabled = True
                win_BOOKU.mnu_PeranDirektur.IsEnabled = True
                win_BOOKU.mnu_PeranTimIT.IsEnabled = True
                win_BOOKU.mnu_PeranAppDeveloper.IsEnabled = False
                'win_BOOKU.tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_AppDeveloper & "  |  " & KeteranganCluster
            End If
        End If

        'If SistemApprovalPerusahaan = False Then win_BOOKU.tls_UserAktif.Text = "User  :  " & NamaUserAktif



        txt_UsernameInput.Focus()
        Me.Close()

    End Sub

    Sub BatasPengulanganLogin()
        PengulanganLogin += 1
        If PengulanganLogin >= 7 Then
            StatusLogin = False
            Pesan_Gagal("Login gagal.")
            End
        End If
    End Sub

    Private Sub chk_TampilkanPassword_Checked_1(sender As Object, e As RoutedEventArgs) Handles chk_TampilkanPassword.Checked
        txt_PasswordInput.PasswordChar = Nothing
    End Sub
    Private Sub chk_TampilkanPassword_Unchecked(sender As Object, e As RoutedEventArgs) Handles chk_TampilkanPassword.Unchecked
        txt_PasswordInput.PasswordChar = "*"
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click

        Password = Nothing

        If UsernameInput = Nothing Then
            Pesan_Peringatan("Ketikkan Username.")
            BatasPengulanganLogin()
            txt_UsernameInput.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = True Then
            cmd = New OdbcCommand(" SELECT * FROM tbl_User WHERE Username = '" & UsernameInput & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Or UsernameInput = UsernameAppDeveloper Or UsernameInput = UsernameTimIT Then
                If PasswordInput = Nothing Then
                    Pesan_Peringatan("Ketikkan Password.")
                    txt_PasswordInput.Focus()
                    AksesDatabase_General(Tutup)
                    BatasPengulanganLogin()
                    Return
                End If
                If dr.HasRows And (UsernameInput <> UsernameAppDeveloper Or UsernameInput <> UsernameTimIT) Then
                    Password = DekripsiTeks(dr.Item("Password"))
                    UserAktif = UsernameInput
                    LevelUserAktif = dr.Item("Level")
                    NamaUserAktif = dr.Item("Nama")
                    JabatanUserAktif = dr.Item("Jabatan")
                    ClusterFinance = dr.Item("Cluster_Finance")
                    ClusterAccounting = dr.Item("Cluster_Accounting")
                    StatusAktifUser = dr.Item("Status_Aktif")
                    AksesDatabase_General(Tutup)
                    If StatusAktifUser <> 1 Then
                        Pesan_Peringatan("Mohon maaf, status kepenggunaan Anda di aplikasi ini sudah tidak aktif." & Enter2Baris & "Silakan ajukan kembali keaktifan dari status kepenggunaan Anda.")
                        ResetForm()
                        Return
                    End If
                End If
                If UsernameInput = UsernameAppDeveloper Then
                    Password = PasswordAppDeveloper
                    UserAktif = UsernameAppDeveloper
                    LevelUserAktif = LevelUser_99_AppDeveloper
                    NamaUserAktif = NamaUserAppDeveloper
                    JabatanUserAktif = JabatanUser_AppDeveloper
                    ClusterFinance = 1
                    ClusterAccounting = 1
                    StatusAktifUser = 1
                End If
                If UsernameInput = UsernameTimIT Then
                    Password = PasswordTimIT
                    UserAktif = UsernameTimIT
                    LevelUserAktif = LevelUser_81_TimIT
                    NamaUserAktif = NamaUserTimIT
                    JabatanUserAktif = JabatanUser_TimIT
                    ClusterFinance = 1
                    ClusterAccounting = 1
                    StatusAktifUser = 1
                End If
            Else
                Pesan_Peringatan("Username tidak ditemukan.")
                txt_UsernameInput.Clear()
                txt_PasswordInput.Clear()
                txt_UsernameInput.Focus()
                BatasPengulanganLogin()
                AksesDatabase_General(Tutup)
                Return
            End If
        Else 'Jika belum ada koneksi Database General :
            If UsernameInput = UsernameAppDeveloper Or UsernameInput = UsernameTimIT Then
                If PasswordInput = Nothing Then
                    Pesan_Peringatan("Ketikkan Password.")
                    txt_PasswordInput.Focus()
                    AksesDatabase_General(Tutup)
                    BatasPengulanganLogin()
                    Return
                End If
                If UsernameInput = UsernameAppDeveloper Then
                    Password = PasswordAppDeveloper
                    UserAktif = UsernameAppDeveloper
                    LevelUserAktif = LevelUser_99_AppDeveloper
                    NamaUserAktif = NamaUserAppDeveloper
                    JabatanUserAktif = JabatanUser_AppDeveloper
                    ClusterFinance = 1
                    ClusterAccounting = 1
                    StatusAktifUser = 1
                End If
                If UsernameInput = UsernameTimIT Then
                    Password = PasswordTimIT
                    UserAktif = UsernameTimIT
                    LevelUserAktif = LevelUser_81_TimIT
                    NamaUserAktif = NamaUserTimIT
                    JabatanUserAktif = JabatanUser_TimIT
                    ClusterFinance = 1
                    ClusterAccounting = 1
                    StatusAktifUser = 1
                End If
            Else
                Pesan_Peringatan("Username tidak ditemukan." & Enter2Baris & "Atau kemungkinan DATABASE belum terkoneksi.")
                txt_UsernameInput.Clear()
                txt_PasswordInput.Clear()
                txt_UsernameInput.Focus()
                BatasPengulanganLogin()
                AksesDatabase_General(Tutup)
                Return
            End If
        End If

        If PasswordInput = Password Then
            LoginSukses()
            Return
        Else
            Pesan_Peringatan("Password salah." & Enter2Baris & "Lupa password? Silakan hubungi Super User atau Developer Aplikasi.")
            txt_PasswordInput.Clear()
            txt_PasswordInput.Focus()
            BatasPengulanganLogin()
            Return
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        chk_TampilkanPassword.Visibility = Visibility.Collapsed
    End Sub

End Class
