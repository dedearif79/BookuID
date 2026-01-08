Imports bcomm
Public Class frm_PilihBulan

    Public BulanTerpilih
    Public BulanTerpilih_Angka As Integer
    Public LanjutkanProses As Boolean

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        rdb_Januari.Checked = False
        rdb_Februari.Checked = False
        rdb_Maret.Checked = False
        rdb_April.Checked = False
        rdb_Mei.Checked = False
        rdb_Juni.Checked = False
        rdb_Juli.Checked = False
        rdb_Agustus.Checked = False
        rdb_September.Checked = False
        rdb_Oktober.Checked = False
        rdb_Nopember.Checked = False
        rdb_Desember.Checked = False

        rdb_Januari.Enabled = True
        rdb_Februari.Enabled = True
        rdb_Maret.Enabled = True
        rdb_April.Enabled = True
        rdb_Mei.Enabled = True
        rdb_Juni.Enabled = True
        rdb_Juli.Enabled = True
        rdb_Agustus.Enabled = True
        rdb_September.Enabled = True
        rdb_Oktober.Enabled = True
        rdb_Nopember.Enabled = True
        rdb_Desember.Enabled = True

        BulanTerpilih = Kosongan
        BulanTerpilih_Angka = 0
        LanjutkanProses = False

        ProsesResetForm = False

    End Sub

    Private Sub rdb_Januari_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Januari.CheckedChanged
        BulanTerpilih_Angka = 1
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Februari_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Februari.CheckedChanged
        BulanTerpilih_Angka = 2
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Maret_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Maret.CheckedChanged
        BulanTerpilih_Angka = 3
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_April_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_April.CheckedChanged
        BulanTerpilih_Angka = 4
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Mei_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Mei.CheckedChanged
        BulanTerpilih_Angka = 5
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Juni_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Juni.CheckedChanged
        BulanTerpilih_Angka = 6
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Juli_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Juli.CheckedChanged
        BulanTerpilih_Angka = 7
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Agustus_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Agustus.CheckedChanged
        BulanTerpilih_Angka = 8
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_September_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_September.CheckedChanged
        BulanTerpilih_Angka = 9
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Oktober_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Oktober.CheckedChanged
        BulanTerpilih_Angka = 10
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Nopember_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Nopember.CheckedChanged
        BulanTerpilih_Angka = 11
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub rdb_Desember_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Desember.CheckedChanged
        BulanTerpilih_Angka = 12
        BulanTerpilih = KonversiAngkaKeBulanString(BulanTerpilih_Angka)
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If BulanTerpilih_Angka = 0 Then
            MsgBox("Silakan pilih salah satu bulan..!")
            Return
        Else
            LanjutkanProses = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        LanjutkanProses = False
        Me.Close()
    End Sub

End Class