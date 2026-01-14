Imports bcomm
Imports System.Windows
Imports System.Data.Odbc


Public Class wpfWin_GantiPassword

    Dim PasswordLama
    Dim PasswordBaru


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        txt_PasswordLama.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()
        txt_PasswordLama.Password = Nothing
        txt_PasswordBaru.Password = Nothing
    End Sub


    Private Sub btn_Ganti_Click(sender As Object, e As RoutedEventArgs) Handles btn_Ganti.Click

        If txt_PasswordLama.Password = Nothing Or txt_PasswordLama.Password = Kosongan Then
            PesanPeringatan("Silakan ketikkan 'Password Lama'.")
            txt_PasswordLama.Focus()
            Return
        End If

        If txt_PasswordBaru.Password = Nothing Or txt_PasswordBaru.Password = Kosongan Then
            PesanPeringatan("Silakan ketikkan 'Password Baru'.")
            txt_PasswordBaru.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_User WHERE username = '" & UserAktif & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        PasswordLama = DekripsiTeks(dr.Item("Password"))
        AksesDatabase_General(Tutup)
        PasswordBaru = txt_PasswordBaru.Password

        If PasswordLama <> txt_PasswordLama.Password Then
            PesanPeringatan("Password lama salah." & Enter2Baris & "Silakan ulangi lagi.")
            txt_PasswordLama.Password = Nothing
            txt_PasswordLama.Focus()
            Return
        End If

        If PasswordLama = PasswordBaru Then
            PesanPeringatan("Password baru sama dengan password lama." & Enter2Baris & "Silakan ulangi lagi.")
            txt_PasswordBaru.Password = Nothing
            txt_PasswordBaru.Focus()
            Return
        End If

        If txt_PasswordBaru.Password.Length < 8 Then
            PesanPeringatan("Karakter Password jangan kurang dari 8 digit.")
            txt_PasswordBaru.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_User SET Password = '" & EnkripsiTeks(PasswordBaru) & "' WHERE username = '" & UserAktif & "' ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            PesanPemberitahuan("Ganti Password Sukses..!")
            Me.Close()
        Catch ex As Exception
            PesanPeringatan("Ganti Password Gagal..!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End Try
        AksesDatabase_General(Tutup)

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        txt_PasswordLama.MaxLength = 50
        txt_PasswordBaru.MaxLength = 50
    End Sub

End Class
