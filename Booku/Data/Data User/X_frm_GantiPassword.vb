Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_GantiPassword

    Dim PasswordLama
    Dim PasswordBaru
    
    Private Sub frm_GantiPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ResetForm()
        txt_PasswordLama.Text = Nothing
        txt_PasswordBaru.Text = Nothing
    End Sub

    Private Sub btn_Ganti_Click(sender As Object, e As EventArgs) Handles btn_Ganti.Click

        If txt_PasswordBaru.Text = Nothing Then
            MsgBox("Silakan ketikkan 'Password Baru'")
            txt_PasswordBaru.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_User WHERE username = '" & UserAktif & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        PasswordLama = DekripsiTeks(dr.Item("Password"))
        TutupDatabaseDasar()
        PasswordBaru = txt_PasswordBaru.Text

        If PasswordLama <> txt_PasswordLama.Text Then
            MsgBox("Password lama salah." & Enter2Baris & "Silakan ulangi lagi.")
            txt_PasswordLama.Text = Nothing
            txt_PasswordLama.Focus()
            Return
        End If

        If PasswordLama = PasswordBaru Then
            MsgBox("Password baru sama dengan password lama." & Enter2Baris & "Silakan ulangi lagi.")
            txt_PasswordBaru.Text = Nothing
            txt_PasswordBaru.Focus()
            Return
        End If

        If Microsoft.VisualBasic.Len(txt_PasswordBaru.Text) < 8 Then
            MsgBox("Karakter Password jangan kurang dari 8 digit.")
            txt_PasswordBaru.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_User SET Password = '" & EnkripsiTeks(PasswordBaru) & "' WHERE username = '" & UserAktif & "' ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Ganti Password Sukses..!")
            Me.Close()
        Catch ex As Exception
            MsgBox("Ganti Password Gagal..!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End Try
        AksesDatabase_General(Tutup)

    End Sub

End Class