Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_Registrasi_IsiDataUser

    Private Sub frm_BuatDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ResetForm()

    End Sub

    Public Sub ResetForm()

        txt_Username.Text = Nothing
        txt_Password.Text = Nothing
        txt_NamaUser.Text = Nothing

    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If txt_Username.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Username'")
            txt_Username.Focus()
            Return
        End If

        If txt_Password.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Password'")
            txt_Password.Focus()
            Return
        End If

        If Microsoft.VisualBasic.Len(txt_Password.Text) < 8 Then
            MsgBox("Karakter Password jangan kurang dari 8 digit.")
            txt_Password.Focus()
            Return
        End If

        If txt_NamaUser.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Nama Lengkap'")
            txt_NamaUser.Focus()
            Return
        End If

        Lanjutkan = True
        Me.Close()

    End Sub

End Class