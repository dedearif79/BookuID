Imports bcomm
Imports System.Windows


Public Class wpfWin_Registrasi_IsiDataUser

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

    End Sub

    Public Sub ResetForm()

        txt_Username.Text = Nothing
        txt_Password.Password = Nothing
        txt_NamaUser.Text = Nothing

    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        If txt_Username.Text = Nothing Then
            Pesan_Peringatan("Silakan isi kolom 'Username'")
            txt_Username.Focus()
            Return
        End If

        If txt_Password.Password = Nothing Then
            Pesan_Peringatan("Silakan isi kolom 'Password'")
            txt_Password.Focus()
            Return
        End If

        If txt_Password.Password.Length < 8 Then
            Pesan_Peringatan("Karakter Password jangan kurang dari 8 digit.")
            txt_Password.Focus()
            Return
        End If

        If txt_NamaUser.Text = Nothing Then
            Pesan_Peringatan("Silakan isi kolom 'Nama Lengkap'")
            txt_NamaUser.Focus()
            Return
        End If

        Lanjutkan = True
        Me.Close()

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
