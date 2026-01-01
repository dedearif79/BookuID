Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputDataKaryawan


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Dim NomorID_Tabel
    Dim TanggalRegistrasi
    Dim NomorIDKaryawan
    Dim NIK
    Dim NamaKaryawan
    Dim Jabatan
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan
    Dim StatusAktif

    Dim NomorIDSudahTerdaftar As Boolean
    Dim NIKSudahTerdaftar As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Karyawan"
            txt_NomorIDKaryawan.Enabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Karyawan"
            txt_NomorIDKaryawan.Enabled = False

        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")




        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub KontenCombo_Jabatan()
        cmb_Jabatan.Items.Clear()
        cmb_Jabatan.Text = Kosongan
    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID_Tabel = 0

        txt_NomorIDKaryawan.Text = Kosongan
        txt_NIK.Text = Kosongan
        dtp_TanggalRegistrasi.Value = Today
        txt_NamaKaryawan.Text = Kosongan
        KontenCombo_Jabatan()
        txt_RekeningBank.Text = Kosongan
        txt_AtasNama.Text = Kosongan
        txt_Catatan.Text = Kosongan
        chk_StatusAktif.Checked = True

        NomorIDSudahTerdaftar = False
        NIKSudahTerdaftar = False

        ProsesResetForm = False

    End Sub


    Private Sub txt_NomorIDKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorIDKaryawan.TextChanged
        NomorIDKaryawan = txt_NomorIDKaryawan.Text
    End Sub
    Private Sub txt_NomorIDKaryawan_Leave(sender As Object, e As EventArgs) Handles txt_NomorIDKaryawan.Leave
        If FungsiForm = FungsiForm_TAMBAH And NomorIDKaryawan <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID_Karyawan FROM tbl_DataKaryawan " &
                                  " WHERE Nomor_ID_Karyawan = '" & NomorIDKaryawan & "' ",
                                  KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                NomorIDSudahTerdaftar = True
            Else
                NomorIDSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NomorIDSudahTerdaftar = True Then
                MsgBox("Nomor ID sudah pernah didaftarkan." & Enter2Baris &
                       "Silakan input nomor yang lain.")
                txt_NomorIDKaryawan.Text = Kosongan
                txt_NomorIDKaryawan.Focus()
                Return
            End If
        End If
    End Sub

    Private Sub txt_NIK_TextChanged(sender As Object, e As EventArgs) Handles txt_NIK.TextChanged
        NIK = txt_NIK.Text
    End Sub
    Private Sub txt_NIK_Leave(sender As Object, e As EventArgs) Handles txt_NIK.Leave
        If NIK <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_ID_Karyawan, NIK FROM tbl_DataKaryawan " &
                                  " WHERE NIK = '" & NIK & "' ",
                                  KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                Dim NomorID_Registrasi_Telusur = dr.Item("Nomor_ID_Karyawan")
                If NomorID_Registrasi_Telusur <> NomorIDKaryawan Then
                    NIKSudahTerdaftar = True
                Else
                    NIKSudahTerdaftar = False
                End If
            Else
                NIKSudahTerdaftar = False
            End If
            AksesDatabase_General(Tutup)
            If NIKSudahTerdaftar = True Then
                MsgBox("NIK sudah pernah didaftarkan untuk karyawan yang lain." & Enter2Baris &
                       "Silakan input NIK yang lain.")
                txt_NIK.Text = Kosongan
                txt_NIK.Focus()
                Return
            End If
        End If
    End Sub

    Private Sub dtp_TanggalRegistrasi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalRegistrasi.ValueChanged
        TanggalRegistrasi = TanggalFormatTampilan(dtp_TanggalRegistrasi.Value)
    End Sub

    Private Sub txt_NamaKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaKaryawan.TextChanged
        NamaKaryawan = txt_NamaKaryawan.Text
    End Sub

    Private Sub cmb_Jabatan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Jabatan.SelectedIndexChanged
    End Sub
    Private Sub cmb_Jabatan_TextChanged(sender As Object, e As EventArgs) Handles cmb_Jabatan.TextChanged
        Jabatan = cmb_Jabatan.Text
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

    Private Sub chk_StatusAktif_CheckedChanged(sender As Object, e As EventArgs) Handles chk_StatusAktif.CheckedChanged
        If chk_StatusAktif.Checked = True Then StatusAktif = 1
        If chk_StatusAktif.Checked = False Then StatusAktif = 0
    End Sub
    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Beberapa Variabel :
        Catatan = txt_Catatan.Text

        If NomorIDSudahTerdaftar = True Or NIKSudahTerdaftar = True Then Return

        'Validasi Kolom-kolom tertentu :
        If NomorIDKaryawan = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor ID'.")
            txt_NomorIDKaryawan.Focus()
            Return
        End If

        If NIK = Kosongan Then
            MsgBox("Silakan isi kolom 'NIK'.")
            txt_NIK.Focus()
            Return
        End If

        If NamaKaryawan = Kosongan Then
            MsgBox("Silakan isi kolom 'Nama Karyawan'.")
            txt_NamaKaryawan.Focus()
            Return
        End If

        If Jabatan = Kosongan Then
            MsgBox("Silakan pilih 'Jabatan'.")
            cmb_Jabatan.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID_Tabel = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataKaryawan") + 1

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_DataKaryawan VALUES ( " &
                                  " '" & NomorID_Tabel & "', " &
                                  " '" & TanggalFormatSimpan(TanggalRegistrasi) & "', " &
                                  " '" & NomorIDKaryawan & "', " &
                                  " '" & NIK & "', " &
                                  " '" & NamaKaryawan & "', " &
                                  " '" & Jabatan & "', " &
                                  " '" & RekeningBank & "', " &
                                  " '" & AtasNama & "', " &
                                  " '" & Catatan & "', " &
                                  " '" & StatusAktif & "' ) ",
                                  KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_DataKaryawan SET " &
                                  " Tanggal_Registrasi          = '" & TanggalFormatSimpan(TanggalRegistrasi) & "', " &
                                  " NIK                         = '" & NIK & "', " &
                                  " Nama_Karyawan               = '" & NamaKaryawan & "', " &
                                  " Jabatan                     = '" & Jabatan & "', " &
                                  " Rekening_Bank               = '" & RekeningBank & "', " &
                                  " Atas_Nama                   = '" & AtasNama & "', " &
                                  " Catatan                     = '" & Catatan & "', " &
                                  " Status_Aktif                = '" & StatusAktif & "' " &
                                  " WHERE Nomor_ID_Karyawan     = '" & NomorIDKaryawan & "' ",
                                  KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            frm_DataKaryawan.TampilkanData()
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