Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputDataPemegangSaham

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim Nama
    Dim NIK
    Dim NPWP
    Dim Alamat
    Dim JenisPS
    Dim LokasiPS
    Dim JumlahLembar
    Dim HargaPerLembar
    Dim JumlahSaham
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan

    Dim NIKSudahTerdaftar As Boolean
    Dim NPWPSudahTerdaftar As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Pemegang Saham"
            txt_NIK.Enabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Pemegang Saham"
            txt_NIK.Enabled = False
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")



        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0
        txt_Nama.Text = Kosongan
        txt_NIK.Text = Kosongan
        txt_NPWP.Text = Kosongan
        txt_Alamat.Text = Kosongan
        KontenCombo_JenisPS()
        KontenCombo_LokasiPS()
        txt_JumlahLembar.Text = Kosongan
        txt_HargaPerlembar.Text = Kosongan
        txt_JumlahSaham.Text = Kosongan
        txt_RekeningBank.Text = Kosongan
        txt_AtasNama.Text = Kosongan
        txt_Catatan.Text = Kosongan

        NIKSudahTerdaftar = False
        NPWPSudahTerdaftar = False


        ProsesResetForm = False

    End Sub



    Sub KontenCombo_JenisPS()
        cmb_JenisPS.Items.Clear()
        cmb_JenisPS.Items.Add(JenisPS_OrangPribadi)
        cmb_JenisPS.Items.Add(JenisPS_BadanHukum)
        cmb_JenisPS.Text = Kosongan
    End Sub


    Sub KontenCombo_LokasiPS()
        cmb_LokasiPS.Items.Clear()
        cmb_LokasiPS.Items.Add(LokasiPS_DalamNegeri)
        cmb_LokasiPS.Items.Add(LokasiPS_LuarNegeri)
        cmb_LokasiPS.Text = Kosongan
    End Sub


    Private Sub txt_Nama_TextChanged(sender As Object, e As EventArgs) Handles txt_Nama.TextChanged
        Nama = txt_Nama.Text
    End Sub


    Private Sub txt_NIK_TextChanged(sender As Object, e As EventArgs) Handles txt_NIK.TextChanged
        NIK = txt_NIK.Text
    End Sub
    Private Sub txt_NIK_Leave(sender As Object, e As EventArgs) Handles txt_NIK.Leave
        If NIK <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID, NIK FROM tbl_PemegangSaham " &
                                  " WHERE NIK = '" & NIK & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                Dim NomorID_Telusur
                NomorID_Telusur = dr.Item("Nomor_ID")
                If NomorID_Telusur <> NomorID Then
                    NIKSudahTerdaftar = True
                Else
                    NIKSudahTerdaftar = False
                End If
            Else
                NIKSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NIKSudahTerdaftar = True Then
                MsgBox("NIK sudah terdaftar." & Enter2Baris & "Silakan input NIK yang lain.")
                txt_NIK.Text = Kosongan
                txt_NIK.Focus()
                Return
            End If
        End If
    End Sub

    Private Sub txt_NPWP_TextChanged(sender As Object, e As EventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
    End Sub
    Private Sub txt_NPWP_Leave(sender As Object, e As EventArgs) Handles txt_NPWP.Leave
        If NPWP <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID, NPWP FROM tbl_PemegangSaham " &
                                  " WHERE NPWP = '" & NPWP & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                Dim NomorID_Telusur
                NomorID_Telusur = dr.Item("Nomor_ID")
                If NomorID_Telusur <> NomorID Then
                    NPWPSudahTerdaftar = True
                Else
                    NPWPSudahTerdaftar = False
                End If
            Else
                NPWPSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NPWPSudahTerdaftar = True Then
                MsgBox("NPWP sudah terdaftar." & Enter2Baris & "Silakan input NPWP yang lain.")
                txt_NPWP.Text = Kosongan
                txt_NPWP.Focus()
                Return
            End If
        End If
    End Sub


    Private Sub txt_Alamat_TextChanged(sender As Object, e As EventArgs) Handles txt_Alamat.TextChanged
        Alamat = txt_Alamat.Text
    End Sub


    Private Sub cmb_JenisPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPS.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPS.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPS_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPS.TextChanged
        JenisPS = cmb_JenisPS.Text
    End Sub


    Private Sub cmb_LokasiPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_LokasiPS.SelectedIndexChanged
    End Sub
    Private Sub cmb_LokasiPS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_LokasiPS.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_LokasiPS_TextChanged(sender As Object, e As EventArgs) Handles cmb_LokasiPS.TextChanged
        LokasiPS = cmb_LokasiPS.Text
    End Sub


    Private Sub txt_JumlahLembar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahLembar.TextChanged
        JumlahLembar = AmbilAngka(txt_JumlahLembar.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox(txt_JumlahLembar)
    End Sub
    Private Sub txt_JumlahLembar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahLembar.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_HargaPerlembar_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaPerlembar.TextChanged
        HargaPerLembar = AmbilAngka(txt_HargaPerlembar.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox(txt_HargaPerlembar)
    End Sub
    Private Sub txt_HargaPerlembar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaPerlembar.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_JumlahSaham_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahSaham.TextChanged
        JumlahSaham = AmbilAngka(txt_JumlahSaham.Text)
        PemecahRibuanUntukTextBox(txt_JumlahSaham)
    End Sub
    Private Sub txt_JumlahSaham_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahSaham.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub Perhitungan()
        txt_JumlahSaham.Text = JumlahLembar * HargaPerLembar
    End Sub


    Private Sub txt_RekeningBank_TextChanged(sender As Object, e As EventArgs) Handles txt_RekeningBank.TextChanged
        RekeningBank = txt_RekeningBank.Text
    End Sub


    Private Sub txt_AtasNama_TextChanged(sender As Object, e As EventArgs) Handles txt_AtasNama.TextChanged
        AtasNama = txt_AtasNama.Text
    End Sub


    Private Sub txt_Catatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi ulang variabel tertentu :
        Alamat = txt_Alamat.Text
        Catatan = txt_Catatan.Text


        If NIKSudahTerdaftar = True Then Return
        If NPWPSudahTerdaftar = True Then Return

        If JenisPS = Kosongan Then
            PesanPeringatan("Silakan pilih 'Jenis Pemegang Saham'.")
            cmb_JenisPS.Focus()
            Return
        End If

        If LokasiPS = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lokasi Pemegang Saham'.")
            cmb_LokasiPS.Focus()
            Return
        End If

        If JumlahLembar = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Lembar'.")
            txt_JumlahLembar.Focus()
            Return
        End If

        If HargaPerLembar = 0 Then
            MsgBox("Silakan isi kolom 'Harga Perlembar'.")
            txt_HargaPerlembar.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_PemegangSaham") + 1

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_PemegangSaham VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & Nama & "', " &
                                  " '" & NIK & "', " &
                                  " '" & NPWP & "', " &
                                  " '" & Alamat & "', " &
                                  " '" & JenisPS & "', " &
                                  " '" & LokasiPS & "', " &
                                  " '" & JumlahLembar & "', " &
                                  " '" & HargaPerLembar & "', " &
                                  " '" & JumlahSaham & "', " &
                                  " '" & RekeningBank & "', " &
                                  " '" & AtasNama & "', " &
                                  " '" & Catatan & "' ) ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()

            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_PemegangSaham SET " &
                                  " Nama                = '" & Nama & "', " &
                                  " NIK                 = '" & NIK & "', " &
                                  " NPWP                = '" & NPWP & "', " &
                                  " Alamat              = '" & Alamat & "', " &
                                  " Jenis_PS            = '" & JenisPS & "', " &
                                  " Lokasi_PS           = '" & LokasiPS & "', " &
                                  " Jumlah_Lembar       = '" & JumlahLembar & "', " &
                                  " Harga_Per_Lembar    = '" & HargaPerLembar & "', " &
                                  " Jumlah_Saham        = '" & JumlahSaham & "', " &
                                  " Rekening_Bank       = '" & RekeningBank & "', " &
                                  " Atas_Nama           = '" & AtasNama & "', " &
                                  " Catatan             = '" & Catatan & "' " &
                                  " WHERE Nomor_ID      = '" & NomorID & "' ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()

            AksesDatabase_General(Tutup)

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            frm_DataPemegangSaham.TampilkanData()
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