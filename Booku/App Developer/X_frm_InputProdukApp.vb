Imports bcomm
Imports MySql.Data.MySqlClient

Public Class X_frm_InputProdukApp

    Public FungsiForm
    Dim JumlahDigitCOA = 5
    Dim ProsesSuntingDatabase As Boolean

    Private Sub frm_InputProdukApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input Produk App"
            txt_NomorSeriProduk.Enabled = True
            txt_IDCustomer.Enabled = True
            btn_Simpan.Text = "Simpan"
            btn_Reset.Enabled = True
        End If
        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Produk App"
            If txt_StatusTerpakai.Text = 0 Then
                txt_NomorSeriProduk.Enabled = True
                txt_IDCustomer.Enabled = True
            Else
                txt_NomorSeriProduk.Enabled = False
                txt_IDCustomer.Enabled = False
            End If
            btn_Simpan.Text = "Simpan"
            btn_Reset.Enabled = False
        End If

    End Sub

    Public Sub ResetForm()

        txt_NomorSeriProduk.Text = Nothing
        txt_IDCustomer.Text = Nothing
        txt_JumlahPerangkat.Text = Nothing
        txt_StatusTerpakai.Text = Nothing

    End Sub

    Private Sub txt_JumlahPerangkat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPerangkat.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_StatusTerpakai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_StatusTerpakai.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click

        ResetForm()

    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        If txt_IDCustomer.Text.Length <> 8 Then
            MsgBox("Jumlah Digit 'ID Customer' harus 8" & Enter2Baris & "Silakan perbaiki.")
            Return
        End If


        Dim QuerySimpan = " INSERT INTO tbl_produk VALUES ('" &
                                         txt_NomorSeriProduk.Text & "', '" &
                                         txt_IDCustomer.Text & "', '" &
                                         txt_JumlahPerangkat.Text & "', '" &
                                         txt_StatusTerpakai.Text & "') "

        Dim QueryEdit = " UPDATE tbl_produk SET " &
                        " ID_Customer = '" & txt_IDCustomer.Text & "', " &
                        " Jumlah_Perangkat = '" & txt_JumlahPerangkat.Text & "', " &
                        " Status_Terpakai = '" & txt_StatusTerpakai.Text & "' " &
                        " WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' "


        BukaDatabasePublic()
        If StatusKoneksiDatabase = False Then Return

        If FungsiForm = FungsiForm_TAMBAH Then
            cmdPublic = New MySqlCommand(QuerySimpan, KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
                MsgBox("Data Produk berhasil disimpan.")
                usc_DataProdukApp.RefreshTampilanData()
                ResetForm()
            Catch ex As Exception
                MsgBox("Data Produk gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
        End If

        If FungsiForm = FungsiForm_EDIT Then
            cmdPublic = New MySqlCommand(QueryEdit, KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
                MsgBox("Data Produk berhasil diedit.")
                usc_DataProdukApp.RefreshTampilanData()
                Me.Close()
            Catch ex As Exception
                MsgBox("Data Produk gagal diedit." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
        End If

        TutupDatabasePublic()

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class