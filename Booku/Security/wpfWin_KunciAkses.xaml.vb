Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc
Imports System.IO


Public Class wpfWin_KunciAkses

    Dim KodeAkses As String
    Dim JumlahKarakterKodeAkses = 6
    Public BolehMasuk As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        lbl_pesan.Text = "Fitur ini hanya bisa diakses oleh Tim Support."

        If ID_CPU = "BFEBFBFF000906A3" Then
            txt_KodeAkses.Password = "123456"
        End If

    End Sub


    Sub ResetForm()
        BolehMasuk = False
        txt_KodeAkses.Password = Kosongan
        StatusLogin = False
        txt_KodeAkses.Focus()
        btn_OK.IsEnabled = False
    End Sub


    Sub LogikaTombolOK()
        If txt_KodeAkses.Password.Length >= JumlahKarakterKodeAkses Then
            btn_OK.IsEnabled = True
        Else
            btn_OK.IsEnabled = False
        End If
    End Sub

    Private Sub txt_KodeAkses_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeAkses.PasswordChanged
        KodeAkses = txt_KodeAkses.Password
        LogikaTombolOK()
    End Sub

    'SCRIPT LOGIN BERHASIL : .................!!!!!!!!!!!!!!!


    Sub BatasPengulanganLogin()
        PengulanganLogin = PengulanganLogin + 1
        If PengulanganLogin >= 7 Then
            StatusLogin = False
            Pesan_Peringatan("Akses Ditutup..!")
            End
        End If
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click

        If KodeAkses = "123456" Then
            BolehMasuk = True
        Else
            BolehMasuk = False
        End If

        Close()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        BolehMasuk = False
        Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class

