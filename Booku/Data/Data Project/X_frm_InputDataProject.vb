Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputDataProject

    Public JudulForm
    Public FungsiForm

    Dim NomorID
    Dim KodeProject
    Dim NamaProject
    Dim NomorPO
    Dim KodeCustomer
    Dim NamaCustomer
    Dim NilaiProject
    Dim Keterangan
    Dim Status

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Kode Project"
            txt_KodeProject.Enabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Kode Project"
            txt_KodeProject.Enabled = False
        End If




        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True
        NomorID = 0
        txt_KodeProject.Text = Kosongan
        txt_NamaProject.Text = Kosongan
        txt_NomorPO.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_NilaiProject.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        KontenCombo_Status

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_Status()
        cmb_Status.Items.Clear()
        cmb_Status.Items.Add(Status_Open)
        cmb_Status.Items.Add(Status_Closed)
        cmb_Status.Text = Kosongan
    End Sub

    Private Sub txt_NomorPO_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorPO.TextChanged
        NomorPO = txt_NomorPO.Text
    End Sub

    Private Sub txt_KodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeProject.TextChanged
        KodeProject = txt_KodeProject.Text
    End Sub
    Private Sub txt_KodeProject_Leave(sender As Object, e As EventArgs) Handles txt_KodeProject.Leave
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand("SELECT Kode_Project FROM tbl_DataProject WHERE Kode_Project = '" & KodeProject & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            MsgBox("Kode Project '" & KodeProject & "' sudah ada." & Enter2Baris &
                   "Silakan isi Kode Project yang lain..!")
            txt_KodeProject.Text = Kosongan
            txt_KodeProject.Focus()
        End If
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub txt_NamaProject_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaProject.TextChanged
        NamaProject = txt_NamaProject.Text
    End Sub

    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
    End Sub
    Private Sub txt_KodeCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.ResetForm()
        frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Customer
        If txt_KodeCustomer.Text <> Kosongan Then
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeCustomer.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaCustomer.Text
        End If
        frm_ListMitra.ShowDialog()
        txt_KodeCustomer.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaCustomer.Text = frm_ListMitra.NamaMitraTerseleksi
    End Sub

    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
    End Sub
    Private Sub txt_NamaCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NilaiProject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NilaiProject.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_NilaiProject_TextChanged(sender As Object, e As EventArgs) Handles txt_NilaiProject.TextChanged
        NilaiProject = AmbilAngka(txt_NilaiProject.Text)
        PemecahRibuanUntukTextBox(txt_NilaiProject)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub cmb_Status_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Status.SelectedIndexChanged
    End Sub
    Private Sub cmb_Status_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Status.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_Status_TextChanged(sender As Object, e As EventArgs) Handles cmb_Status.TextChanged
        Status = cmb_Status.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Variabel2 Tertentu :
        Keterangan = txt_Keterangan.Text

        If KodeProject = Kosongan Then
            MsgBox("Silakan isi kolom 'Kode Project'.")
            txt_KodeProject.Focus()
            Return
        End If

        If KodeCustomer = Kosongan Then
            MsgBox("Silakan isi data 'Customer'.")
            btn_PilihMitra_Click(sender, e)
            Return
        End If

        If Status = Kosongan Then
            MsgBox("Silakan pilih 'Status'.")
            cmb_Status.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataProject") + 1

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_DataProject VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & KodeProject & "', " &
                                  " '" & NamaProject & "', " &
                                  " '" & NomorPO & "', " &
                                  " '" & KodeCustomer & "', " &
                                  " '" & NamaCustomer & "', " &
                                  " '" & NilaiProject & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & Status & "') ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()

            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_DataProject SET " &
                                  " Nama_Project        = '" & NamaProject & "', " &
                                  " Nomor_PO            = '" & NomorPO & "', " &
                                  " Kode_Customer       = '" & KodeCustomer & "', " &
                                  " Nama_Customer       = '" & NamaCustomer & "', " &
                                  " Nilai_Project       = '" & NilaiProject & "', " &
                                  " Keterangan          = '" & Keterangan & "', " &
                                  " Status              = '" & Status & "' " &
                                  " WHERE Kode_Project  = '" & KodeProject & "' ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()

            AksesDatabase_General(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_DataProject.StatusAktif Then usc_DataProject.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class