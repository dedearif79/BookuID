Imports System.Windows
Imports bcomm

Public Class wpfWin_PilihBulan

    Public BulanTerpilih As String
    Public BulanTerpilih_Angka As Integer
    Public LanjutkanProses As Boolean

    Private ProsesResetForm As Boolean

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        rdb_Januari.IsChecked = False
        rdb_Februari.IsChecked = False
        rdb_Maret.IsChecked = False
        rdb_April.IsChecked = False
        rdb_Mei.IsChecked = False
        rdb_Juni.IsChecked = False
        rdb_Juli.IsChecked = False
        rdb_Agustus.IsChecked = False
        rdb_September.IsChecked = False
        rdb_Oktober.IsChecked = False
        rdb_Nopember.IsChecked = False
        rdb_Desember.IsChecked = False

        rdb_Januari.IsEnabled = True
        rdb_Februari.IsEnabled = True
        rdb_Maret.IsEnabled = True
        rdb_April.IsEnabled = True
        rdb_Mei.IsEnabled = True
        rdb_Juni.IsEnabled = True
        rdb_Juli.IsEnabled = True
        rdb_Agustus.IsEnabled = True
        rdb_September.IsEnabled = True
        rdb_Oktober.IsEnabled = True
        rdb_Nopember.IsEnabled = True
        rdb_Desember.IsEnabled = True

        BulanTerpilih = Kosongan
        BulanTerpilih_Angka = 0
        LanjutkanProses = False

        ProsesResetForm = False

    End Sub

    Private Sub rdb_Januari_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Januari.Checked
        BulanTerpilih_Angka = 1
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Februari_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Februari.Checked
        BulanTerpilih_Angka = 2
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Maret_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Maret.Checked
        BulanTerpilih_Angka = 3
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_April_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_April.Checked
        BulanTerpilih_Angka = 4
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Mei_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Mei.Checked
        BulanTerpilih_Angka = 5
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Juni_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Juni.Checked
        BulanTerpilih_Angka = 6
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Juli_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Juli.Checked
        BulanTerpilih_Angka = 7
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Agustus_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Agustus.Checked
        BulanTerpilih_Angka = 8
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_September_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_September.Checked
        BulanTerpilih_Angka = 9
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Oktober_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Oktober.Checked
        BulanTerpilih_Angka = 10
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Nopember_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Nopember.Checked
        BulanTerpilih_Angka = 11
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Desember_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Desember.Checked
        BulanTerpilih_Angka = 12
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click
        If BulanTerpilih_Angka = 0 Then
            MsgBox("Silakan pilih salah satu bulan..!")
            Return
        Else
            LanjutkanProses = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        LanjutkanProses = False
        Me.Close()
    End Sub

End Class
