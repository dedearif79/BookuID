Imports System.Windows
Imports bcomm

Public Class wpfWin_CeklisBulan

    Public BulanTerceklis_Awal As String
    Public BulanTerceklis_Akhir As String
    Public LanjutkanProses As Boolean

    Private ProsesResetForm As Boolean

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If chk_Januari.IsEnabled = True Then chk_Januari.IsChecked = True
        If chk_Februari.IsEnabled = True Then chk_Februari.IsChecked = True
        If chk_Maret.IsEnabled = True Then chk_Maret.IsChecked = True
        If chk_April.IsEnabled = True Then chk_April.IsChecked = True
        If chk_Mei.IsEnabled = True Then chk_Mei.IsChecked = True
        If chk_Juni.IsEnabled = True Then chk_Juni.IsChecked = True
        If chk_Juli.IsEnabled = True Then chk_Juli.IsChecked = True
        If chk_Agustus.IsEnabled = True Then chk_Agustus.IsChecked = True
        If chk_September.IsEnabled = True Then chk_September.IsChecked = True
        If chk_Oktober.IsEnabled = True Then chk_Oktober.IsChecked = True
        If chk_Nopember.IsEnabled = True Then chk_Nopember.IsChecked = True
        If chk_Desember.IsEnabled = True Then chk_Desember.IsChecked = True
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        chk_Januari.IsChecked = False
        chk_Februari.IsChecked = False
        chk_Maret.IsChecked = False
        chk_April.IsChecked = False
        chk_Mei.IsChecked = False
        chk_Juni.IsChecked = False
        chk_Juli.IsChecked = False
        chk_Agustus.IsChecked = False
        chk_September.IsChecked = False
        chk_Oktober.IsChecked = False
        chk_Nopember.IsChecked = False
        chk_Desember.IsChecked = False

        chk_Januari.IsEnabled = True
        chk_Februari.IsEnabled = True
        chk_Maret.IsEnabled = True
        chk_April.IsEnabled = True
        chk_Mei.IsEnabled = True
        chk_Juni.IsEnabled = True
        chk_Juli.IsEnabled = True
        chk_Agustus.IsEnabled = True
        chk_September.IsEnabled = True
        chk_Oktober.IsEnabled = True
        chk_Nopember.IsEnabled = True
        chk_Desember.IsEnabled = True

        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan

        LanjutkanProses = False

        ProsesResetForm = False

    End Sub

    Private Sub chk_Januari_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Januari.Checked, chk_Januari.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Februari_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Februari.Checked, chk_Februari.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Maret_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Maret.Checked, chk_Maret.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_April_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_April.Checked, chk_April.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Mei_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Mei.Checked, chk_Mei.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Juni_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Juni.Checked, chk_Juni.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Juli_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Juli.Checked, chk_Juli.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Agustus_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Agustus.Checked, chk_Agustus.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_September_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_September.Checked, chk_September.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Oktober_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Oktober.Checked, chk_Oktober.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Nopember_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Nopember.Checked, chk_Nopember.Unchecked
        LogikaCeklis()
    End Sub

    Private Sub chk_Desember_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles chk_Desember.Checked, chk_Desember.Unchecked
        LogikaCeklis()
    End Sub

    Sub LogikaCeklis()

        If ProsesResetForm = False Then

            If chk_Desember.IsChecked = True And chk_Nopember.IsEnabled = True Then chk_Nopember.IsChecked = True
            If chk_Nopember.IsChecked = True And chk_Oktober.IsEnabled = True Then chk_Oktober.IsChecked = True
            If chk_Oktober.IsChecked = True And chk_September.IsEnabled = True Then chk_September.IsChecked = True
            If chk_September.IsChecked = True And chk_Agustus.IsEnabled = True Then chk_Agustus.IsChecked = True
            If chk_Agustus.IsChecked = True And chk_Juli.IsEnabled = True Then chk_Juli.IsChecked = True
            If chk_Juli.IsChecked = True And chk_Juni.IsEnabled = True Then chk_Juni.IsChecked = True
            If chk_Juni.IsChecked = True And chk_Mei.IsEnabled = True Then chk_Mei.IsChecked = True
            If chk_Mei.IsChecked = True And chk_April.IsEnabled = True Then chk_April.IsChecked = True
            If chk_April.IsChecked = True And chk_Maret.IsEnabled = True Then chk_Maret.IsChecked = True
            If chk_Maret.IsChecked = True And chk_Februari.IsEnabled = True Then chk_Februari.IsChecked = True
            If chk_Februari.IsChecked = True And chk_Januari.IsEnabled = True Then chk_Januari.IsChecked = True

            If chk_Januari.IsChecked = False And chk_Januari.IsEnabled = True Then chk_Februari.IsChecked = False
            If chk_Februari.IsChecked = False And chk_Februari.IsEnabled = True Then chk_Maret.IsChecked = False
            If chk_Maret.IsChecked = False And chk_Maret.IsEnabled = True Then chk_April.IsChecked = False
            If chk_April.IsChecked = False And chk_April.IsEnabled = True Then chk_Mei.IsChecked = False
            If chk_Mei.IsChecked = False And chk_Mei.IsEnabled = True Then chk_Juni.IsChecked = False
            If chk_Juni.IsChecked = False And chk_Juni.IsEnabled = True Then chk_Juli.IsChecked = False
            If chk_Juli.IsChecked = False And chk_Juli.IsEnabled = True Then chk_Agustus.IsChecked = False
            If chk_Agustus.IsChecked = False And chk_Agustus.IsEnabled = True Then chk_September.IsChecked = False
            If chk_September.IsChecked = False And chk_September.IsEnabled = True Then chk_Oktober.IsChecked = False
            If chk_Oktober.IsChecked = False And chk_Oktober.IsEnabled = True Then chk_Nopember.IsChecked = False
            If chk_Nopember.IsChecked = False And chk_Nopember.IsEnabled = True Then chk_Desember.IsChecked = False

        End If

        Dim NomorBulan = 0
        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan

        Do While NomorBulan < 12
            NomorBulan += 1
            Select Case NomorBulan
                Case 1
                    If chk_Januari.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 2
                    If chk_Februari.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 3
                    If chk_Maret.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 4
                    If chk_April.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 5
                    If chk_Mei.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 6
                    If chk_Juni.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 7
                    If chk_Juli.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 8
                    If chk_Agustus.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 9
                    If chk_September.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 10
                    If chk_Oktober.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 11
                    If chk_Nopember.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 12
                    If chk_Desember.IsChecked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = Kosongan
                        Return
                    End If
            End Select
        Loop

        NomorBulan -= 1

        Do While NomorBulan < 12
            NomorBulan += 1
            Select Case NomorBulan
                Case 1
                    If chk_Januari.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 2
                    If chk_Februari.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 3
                    If chk_Maret.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 4
                    If chk_April.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 5
                    If chk_Mei.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 6
                    If chk_Juni.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 7
                    If chk_Juli.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 8
                    If chk_Agustus.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 9
                    If chk_September.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 10
                    If chk_Oktober.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 11
                    If chk_Nopember.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 12
                    If chk_Desember.IsChecked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
            End Select
        Loop

    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click
        If BulanTerceklis_Awal = Kosongan Or BulanTerceklis_Akhir = Kosongan Then
            Pesan_Peringatan("Silakan pilih salah satu atau beberapa 'Bulan' untuk diposting.")
        Else
            LanjutkanProses = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        LanjutkanProses = False
        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan
        Me.Close()
    End Sub

End Class
