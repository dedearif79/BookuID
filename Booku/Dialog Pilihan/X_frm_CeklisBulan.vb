Imports bcomm

Public Class frm_CeklisBulan

    Public BulanTerceklis_Awal
    Public BulanTerceklis_Akhir
    Public LanjutkanProses As Boolean

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If chk_Januari.Enabled = True Then chk_Januari.Checked = True
        If chk_Februari.Enabled = True Then chk_Februari.Checked = True
        If chk_Maret.Enabled = True Then chk_Maret.Checked = True
        If chk_April.Enabled = True Then chk_April.Checked = True
        If chk_Mei.Enabled = True Then chk_Mei.Checked = True
        If chk_Juni.Enabled = True Then chk_Juni.Checked = True
        If chk_Juli.Enabled = True Then chk_Juli.Checked = True
        If chk_Agustus.Enabled = True Then chk_Agustus.Checked = True
        If chk_September.Enabled = True Then chk_September.Checked = True
        If chk_Oktober.Enabled = True Then chk_Oktober.Checked = True
        If chk_Nopember.Enabled = True Then chk_Nopember.Checked = True
        If chk_Desember.Enabled = True Then chk_Desember.Checked = True

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        chk_Januari.Checked = False
        chk_Februari.Checked = False
        chk_Maret.Checked = False
        chk_April.Checked = False
        chk_Mei.Checked = False
        chk_Juni.Checked = False
        chk_Juli.Checked = False
        chk_Agustus.Checked = False
        chk_September.Checked = False
        chk_Oktober.Checked = False
        chk_Nopember.Checked = False
        chk_Desember.Checked = False

        chk_Januari.Enabled = True
        chk_Februari.Enabled = True
        chk_Maret.Enabled = True
        chk_April.Enabled = True
        chk_Mei.Enabled = True
        chk_Juni.Enabled = True
        chk_Juli.Enabled = True
        chk_Agustus.Enabled = True
        chk_September.Enabled = True
        chk_Oktober.Enabled = True
        chk_Nopember.Enabled = True
        chk_Desember.Enabled = True

        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan

        LanjutkanProses = False

        ProsesResetForm = False

    End Sub

    Private Sub chk_Januari_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Januari.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Februari_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Februari.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Maret_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Maret.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_April_CheckedChanged(sender As Object, e As EventArgs) Handles chk_April.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Mei_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Mei.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Juni_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Juni.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Juli_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Juli.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Agustus_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Agustus.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_September_CheckedChanged(sender As Object, e As EventArgs) Handles chk_September.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Oktober_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Oktober.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Nopember_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Nopember.CheckedChanged
        LogikaCeklis()
    End Sub

    Private Sub chk_Desember_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Desember.CheckedChanged
        LogikaCeklis()
    End Sub

    Sub LogikaCeklis()

        If ProsesResetForm = False Then

            If chk_Desember.Checked = True And chk_Nopember.Enabled = True Then chk_Nopember.Checked = True
            If chk_Nopember.Checked = True And chk_Oktober.Enabled = True Then chk_Oktober.Checked = True
            If chk_Oktober.Checked = True And chk_September.Enabled = True Then chk_September.Checked = True
            If chk_September.Checked = True And chk_Agustus.Enabled = True Then chk_Agustus.Checked = True
            If chk_Agustus.Checked = True And chk_Juli.Enabled = True Then chk_Juli.Checked = True
            If chk_Juli.Checked = True And chk_Juni.Enabled = True Then chk_Juni.Checked = True
            If chk_Juni.Checked = True And chk_Mei.Enabled = True Then chk_Mei.Checked = True
            If chk_Mei.Checked = True And chk_April.Enabled = True Then chk_April.Checked = True
            If chk_April.Checked = True And chk_Maret.Enabled = True Then chk_Maret.Checked = True
            If chk_Maret.Checked = True And chk_Februari.Enabled = True Then chk_Februari.Checked = True
            If chk_Februari.Checked = True And chk_Januari.Enabled = True Then chk_Januari.Checked = True

            If chk_Januari.Checked = False And chk_Januari.Enabled = True Then chk_Februari.Checked = False
            If chk_Februari.Checked = False And chk_Februari.Enabled = True Then chk_Maret.Checked = False
            If chk_Maret.Checked = False And chk_Maret.Enabled = True Then chk_April.Checked = False
            If chk_April.Checked = False And chk_April.Enabled = True Then chk_Mei.Checked = False
            If chk_Mei.Checked = False And chk_Mei.Enabled = True Then chk_Juni.Checked = False
            If chk_Juni.Checked = False And chk_Juni.Enabled = True Then chk_Juli.Checked = False
            If chk_Juli.Checked = False And chk_Juli.Enabled = True Then chk_Agustus.Checked = False
            If chk_Agustus.Checked = False And chk_Agustus.Enabled = True Then chk_September.Checked = False
            If chk_September.Checked = False And chk_September.Enabled = True Then chk_Oktober.Checked = False
            If chk_Oktober.Checked = False And chk_Oktober.Enabled = True Then chk_Nopember.Checked = False
            If chk_Nopember.Checked = False And chk_Nopember.Enabled = True Then chk_Desember.Checked = False

        End If

        Dim NomorBulan = 0
        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan

        Do While NomorBulan < 12
            NomorBulan += 1
            Select Case NomorBulan
                Case 1
                    If chk_Januari.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 2
                    If chk_Februari.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 3
                    If chk_Maret.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 4
                    If chk_April.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 5
                    If chk_Mei.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 6
                    If chk_Juni.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 7
                    If chk_Juli.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 8
                    If chk_Agustus.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 9
                    If chk_September.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 10
                    If chk_Oktober.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 11
                    If chk_Nopember.Checked = True Then
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan)
                        Exit Do
                    Else
                        BulanTerceklis_Awal = KonversiAngkaKeBulanString(NomorBulan + 1)
                    End If
                Case 12
                    If chk_Desember.Checked = True Then
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
                    If chk_Januari.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 2
                    If chk_Februari.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 3
                    If chk_Maret.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 4
                    If chk_April.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 5
                    If chk_Mei.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 6
                    If chk_Juni.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 7
                    If chk_Juli.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 8
                    If chk_Agustus.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 9
                    If chk_September.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 10
                    If chk_Oktober.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 11
                    If chk_Nopember.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
                Case 12
                    If chk_Desember.Checked = True Then
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan)
                    Else
                        BulanTerceklis_Akhir = KonversiAngkaKeBulanString(NomorBulan - 1)
                        Exit Do
                    End If
            End Select
        Loop

    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click
        'PesanUntukProgrammer("Awal : " & BulanTerceklis_Awal & Enter2Baris &
        '                     "Akhir : " & BulanTerceklis_Akhir)
        If BulanTerceklis_Awal = Kosongan Or BulanTerceklis_Akhir = Kosongan Then
            Lanjutkan = False
            MsgBox("Silakan pilih salah satu atau beberapa 'Bulan' untuk diposting.")
        Else
            LanjutkanProses = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        LanjutkanProses = False
        BulanTerceklis_Awal = Kosongan
        BulanTerceklis_Akhir = Kosongan
        Me.Close()
    End Sub

End Class