Imports bcomm

Public Class frm_InputHutangPPhPasal21_Gaji

    Dim JudulForm
    Dim PPhPasal21_Januari
    Dim PPhPasal21_Februari
    Dim PPhPasal21_Maret
    Dim PPhPasal21_April
    Dim PPhPasal21_Mei
    Dim PPhPasal21_Juni
    Dim PPhPasal21_Juli
    Dim PPhPasal21_Agustus
    Dim PPhPasal21_September
    Dim PPhPasal21_Oktober
    Dim PPhPasal21_Nopember
    Dim PPhPasal21_Desember

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        JudulForm = "Input Hutang PPh Pasal 21 (Gaji) - Tahun " & TahunPajak

        Me.Text = JudulForm

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_Januari.Text = Nothing
        txt_Februari.Text = Nothing
        txt_Maret.Text = Nothing
        txt_April.Text = Nothing
        txt_Mei.Text = Nothing
        txt_Juni.Text = Nothing
        txt_Juli.Text = Nothing
        txt_Agustus.Text = Nothing
        txt_September.Text = Nothing
        txt_Oktober.Text = Nothing
        txt_Nopember.Text = Nothing
        txt_Desember.Text = Nothing

        ProsesResetForm = False

    End Sub


    Private Sub txt_Januari_TextChanged(sender As Object, e As EventArgs) Handles txt_Januari.TextChanged
        PPhPasal21_Januari = AmbilAngka(txt_Januari.Text)
        PemecahRibuanUntukTextBox(txt_Januari)
    End Sub

    Private Sub txt_Februari_TextChanged(sender As Object, e As EventArgs) Handles txt_Februari.TextChanged
        PPhPasal21_Februari = AmbilAngka(txt_Februari.Text)
        PemecahRibuanUntukTextBox(txt_Februari)
    End Sub

    Private Sub txt_Maret_TextChanged(sender As Object, e As EventArgs) Handles txt_Maret.TextChanged
        PPhPasal21_Maret = AmbilAngka(txt_Maret.Text)
        PemecahRibuanUntukTextBox(txt_Maret)
    End Sub

    Private Sub txt_April_TextChanged(sender As Object, e As EventArgs) Handles txt_April.TextChanged
        PPhPasal21_April = AmbilAngka(txt_April.Text)
        PemecahRibuanUntukTextBox(txt_April)
    End Sub

    Private Sub txt_Mei_TextChanged(sender As Object, e As EventArgs) Handles txt_Mei.TextChanged
        PPhPasal21_Mei = AmbilAngka(txt_Mei.Text)
        PemecahRibuanUntukTextBox(txt_Mei)
    End Sub

    Private Sub txt_Juni_TextChanged(sender As Object, e As EventArgs) Handles txt_Juni.TextChanged
        PPhPasal21_Juni = AmbilAngka(txt_Juni.Text)
        PemecahRibuanUntukTextBox(txt_Juni)
    End Sub

    Private Sub txt_Juli_TextChanged(sender As Object, e As EventArgs) Handles txt_Juli.TextChanged
        PPhPasal21_Juli = AmbilAngka(txt_Juli.Text)
        PemecahRibuanUntukTextBox(txt_Juli)
    End Sub

    Private Sub txt_Agustus_TextChanged(sender As Object, e As EventArgs) Handles txt_Agustus.TextChanged
        PPhPasal21_Agustus = AmbilAngka(txt_Agustus.Text)
        PemecahRibuanUntukTextBox(txt_Agustus)
    End Sub

    Private Sub txt_September_TextChanged(sender As Object, e As EventArgs) Handles txt_September.TextChanged
        PPhPasal21_September = AmbilAngka(txt_September.Text)
        PemecahRibuanUntukTextBox(txt_September)
    End Sub

    Private Sub txt_Oktober_TextChanged(sender As Object, e As EventArgs) Handles txt_Oktober.TextChanged
        PPhPasal21_Oktober = AmbilAngka(txt_Oktober.Text)
        PemecahRibuanUntukTextBox(txt_Oktober)
    End Sub

    Private Sub txt_Nopember_TextChanged(sender As Object, e As EventArgs) Handles txt_Nopember.TextChanged
        PPhPasal21_Nopember = AmbilAngka(txt_Nopember.Text)
        PemecahRibuanUntukTextBox(txt_Nopember)
    End Sub

    Private Sub txt_Desember_TextChanged(sender As Object, e As EventArgs) Handles txt_Desember.TextChanged
        PPhPasal21_Desember = AmbilAngka(txt_Desember.Text)
        PemecahRibuanUntukTextBox(txt_Desember)
    End Sub


    Private Sub txt_Januari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Januari.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Februari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Februari.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Maret_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Maret.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_April_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_April.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Mei_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Mei.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Juni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Juni.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Juli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Juli.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Agustus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Agustus.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_September_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_September.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Oktober_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Oktober.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Nopember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Nopember.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Desember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Desember.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        TrialBalance_Mentahkan()

        Dim NomorBulan
        Dim Bulan
        Dim NomorIdTransaksi
        Dim TanggalTransaksi As Date
        Dim TexBox_Gaji As TextBox = txt_Januari
        Dim PPhPasal21

        NomorBulan = 0

        AksesDatabase_Transaksi(Buka)
        'Hapus Semua Data Hutang PPh Pasal 21 Gaji Tahun Pajak :
        cmdHAPUS = New Odbc.OdbcCommand(" DELETE FROM tbl_HutangPajak " &
                                        " WHERE Jenis_Pajak                         = '" & JenisPajak_PPhPasal21 & "' " &
                                        " AND Nama_Jasa                             = '" & teks_Gaji & "' " &
                                        " AND DATE_FORMAT(Tanggal_Transaksi, '%Y')  = '" & TahunPajak & "' ",
                                        KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        NomorIdTransaksi = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_HutangPajak")

        AksesDatabase_Transaksi(Buka)

        Do While AmbilAngka(NomorBulan) < 12
            NomorBulan = AmbilAngka(NomorBulan) + 1
            Bulan = BulanTerbilang(NomorBulan)
            If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                NomorBulan = "0" & NomorBulan.ToString
            Else
                NomorBulan = NomorBulan.ToString
            End If
            Select Case Bulan
                Case Bulan_Januari
                    TexBox_Gaji = txt_Januari
                    TanggalTransaksi = New Date(TahunPajak, 1, 31)
                Case Bulan_Februari
                    TexBox_Gaji = txt_Februari
                    If TahunPajak Mod 4 = 0 Then
                        TanggalTransaksi = New Date(TahunPajak, 2, 29)
                    Else
                        TanggalTransaksi = New Date(TahunPajak, 2, 28)
                    End If
                Case Bulan_Maret
                    TexBox_Gaji = txt_Maret
                    TanggalTransaksi = New Date(TahunPajak, 3, 31)
                Case Bulan_April
                    TexBox_Gaji = txt_April
                    TanggalTransaksi = New Date(TahunPajak, 4, 30)
                Case Bulan_Mei
                    TexBox_Gaji = txt_Mei
                    TanggalTransaksi = New Date(TahunPajak, 5, 31)
                Case Bulan_Juni
                    TexBox_Gaji = txt_Juni
                    TanggalTransaksi = New Date(TahunPajak, 6, 30)
                Case Bulan_Juli
                    TexBox_Gaji = txt_Juli
                    TanggalTransaksi = New Date(TahunPajak, 7, 31)
                Case Bulan_Agustus
                    TexBox_Gaji = txt_Agustus
                    TanggalTransaksi = New Date(TahunPajak, 8, 31)
                Case Bulan_September
                    TexBox_Gaji = txt_September
                    TanggalTransaksi = New Date(TahunPajak, 9, 30)
                Case Bulan_Oktober
                    TexBox_Gaji = txt_Oktober
                    TanggalTransaksi = New Date(TahunPajak, 10, 31)
                Case Bulan_Nopember
                    TexBox_Gaji = txt_Nopember
                    TanggalTransaksi = New Date(TahunPajak, 11, 30)
                Case Bulan_Desember
                    TexBox_Gaji = txt_Desember
                    TanggalTransaksi = New Date(TahunPajak, 12, 31)
            End Select

            PPhPasal21 = AmbilAngka(TexBox_Gaji.Text)

            If AmbilAngka(TexBox_Gaji.Text) > 0 Then
                NomorIdTransaksi += 1
                cmdSIMPAN = New Odbc.OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                 " '" & NomorIdTransaksi & "', " &
                                                 " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                                 " '" & TanggalKosongSimpan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & teks_Gaji & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & 0 & "', " &
                                                 " '" & JenisPajak_PPhPasal21 & "', " &
                                                 " '" & KodeSetoran_100 & "', " &
                                                 " '" & PPhPasal21 & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & 0 & "', " &
                                                 " '" & UserAktif & "' ) ",
                                                 KoneksiDatabaseTransaksi)
                Try
                    cmdSIMPAN.ExecuteNonQuery()
                    StatusSuntingDatabase = True
                Catch ex As Exception
                    StatusSuntingDatabase = False
                    Exit Do
                End Try

            End If

        Loop

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            MsgBox("Data BERHASIL disimpan.")
            If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()
            Me.Close()
        Else
            MsgBox("Data GAGAL disimpan." & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class