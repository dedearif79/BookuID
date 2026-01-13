Imports System.Windows
Imports bcomm

Public Class wpfWin_PilihJurnal_DataAsset

    Dim NomorJV As Object
    Public JalurMasuk As String

    ' Properties untuk Nomor JV per Bulan
    Public AngkaNomorJV_Januari As Object
    Public AngkaNomorJV_Februari As Object
    Public AngkaNomorJV_Maret As Object
    Public AngkaNomorJV_April As Object
    Public AngkaNomorJV_Mei As Object
    Public AngkaNomorJV_Juni As Object
    Public AngkaNomorJV_Juli As Object
    Public AngkaNomorJV_Agustus As Object
    Public AngkaNomorJV_September As Object
    Public AngkaNomorJV_Oktober As Object
    Public AngkaNomorJV_Nopember As Object
    Public AngkaNomorJV_Desember As Object

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    Public Sub ResetForm()
        ' Reset IsEnabled RadioButton
        rdb_Januari.IsEnabled = False
        rdb_Januari.IsChecked = False
        rdb_Februari.IsEnabled = False
        rdb_Februari.IsChecked = False
        rdb_Maret.IsEnabled = False
        rdb_Maret.IsChecked = False
        rdb_April.IsEnabled = False
        rdb_April.IsChecked = False
        rdb_Mei.IsEnabled = False
        rdb_Mei.IsChecked = False
        rdb_Juni.IsEnabled = False
        rdb_Juni.IsChecked = False
        rdb_Juli.IsEnabled = False
        rdb_Juli.IsChecked = False
        rdb_Agustus.IsEnabled = False
        rdb_Agustus.IsChecked = False
        rdb_September.IsEnabled = False
        rdb_September.IsChecked = False
        rdb_Oktober.IsEnabled = False
        rdb_Oktober.IsChecked = False
        rdb_Nopember.IsEnabled = False
        rdb_Nopember.IsChecked = False
        rdb_Desember.IsEnabled = False
        rdb_Desember.IsChecked = False

        ' Reset Tombol
        btn_Lanjutkan.IsEnabled = False

        ' Reset Variabel
        NomorJV = Nothing
        AngkaNomorJV_Januari = Nothing
        AngkaNomorJV_Februari = Nothing
        AngkaNomorJV_Maret = Nothing
        AngkaNomorJV_April = Nothing
        AngkaNomorJV_Mei = Nothing
        AngkaNomorJV_Juni = Nothing
        AngkaNomorJV_Juli = Nothing
        AngkaNomorJV_Agustus = Nothing
        AngkaNomorJV_September = Nothing
        AngkaNomorJV_Oktober = Nothing
        AngkaNomorJV_Nopember = Nothing
        AngkaNomorJV_Desember = Nothing
    End Sub

    Sub LogikaTombolLanjutkan()
        btn_Lanjutkan.IsEnabled = True
    End Sub

    ' Event Checked untuk RadioButton
    Private Sub rdb_Januari_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Januari.Checked
        If rdb_Januari.IsChecked = True Then
            NomorJV = AngkaNomorJV_Januari
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Februari_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Februari.Checked
        If rdb_Februari.IsChecked = True Then
            NomorJV = AngkaNomorJV_Februari
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Maret_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Maret.Checked
        If rdb_Maret.IsChecked = True Then
            NomorJV = AngkaNomorJV_Maret
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_April_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_April.Checked
        If rdb_April.IsChecked = True Then
            NomorJV = AngkaNomorJV_April
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Mei_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Mei.Checked
        If rdb_Mei.IsChecked = True Then
            NomorJV = AngkaNomorJV_Mei
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Juni_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Juni.Checked
        If rdb_Juni.IsChecked = True Then
            NomorJV = AngkaNomorJV_Juni
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Juli_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Juli.Checked
        If rdb_Juli.IsChecked = True Then
            NomorJV = AngkaNomorJV_Juli
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Agustus_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Agustus.Checked
        If rdb_Agustus.IsChecked = True Then
            NomorJV = AngkaNomorJV_Agustus
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_September_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_September.Checked
        If rdb_September.IsChecked = True Then
            NomorJV = AngkaNomorJV_September
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Oktober_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Oktober.Checked
        If rdb_Oktober.IsChecked = True Then
            NomorJV = AngkaNomorJV_Oktober
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Nopember_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Nopember.Checked
        If rdb_Nopember.IsChecked = True Then
            NomorJV = AngkaNomorJV_Nopember
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Desember_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Desember.Checked
        If rdb_Desember.IsChecked = True Then
            NomorJV = AngkaNomorJV_Desember
            LogikaTombolLanjutkan()
        End If
    End Sub

    ' Tombol Lanjutkan (Lihat >>)
    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click
        LihatJurnal(NomorJV)
    End Sub

    ' Tombol Kembali
    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        Me.Close()
    End Sub

End Class
