Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputUser

    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean

    Private Sub frm_InputUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data User"
            txt_Username.Enabled = False
            If txt_Username.Text = UserAktif Then
                'txt_Password.Enabled = True
                cmb_Jabatan.Enabled = False
                cmb_StatusAktif.Enabled = False
            Else
                'txt_Password.Enabled = False
                cmb_Jabatan.Enabled = True
                cmb_StatusAktif.Enabled = True
            End If
            btn_Reset.Enabled = False
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            ResetForm()
            Me.Text = "Input Data User"
        End If

    End Sub

    Public Sub ResetForm()
        txt_Username.Enabled = True
        txt_Username.Text = Nothing
        txt_Password.Enabled = True
        txt_Password.Text = Nothing
        txt_NamaLengkap.Text = Nothing
        KontenComboJabatan()
        cmb_Jabatan.Enabled = True
        If SistemApprovalPerusahaan = True Then
            chk_ClusterFinance.Enabled = True
            chk_ClusterAccounting.Enabled = True
        Else
            chk_ClusterFinance.Enabled = False
            chk_ClusterAccounting.Enabled = False
        End If
        chk_ClusterFinance.Checked = False
        chk_ClusterAccounting.Checked = False
        KontenComboStatusAktif()
        cmb_StatusAktif.Enabled = True
        btn_Reset.Enabled = True
        txt_Username.Focus()
    End Sub

    Sub KontenComboJabatan()
        cmb_Jabatan.Items.Clear()
        cmb_Jabatan.Items.Add(JabatanUser_SuperUser)
        If SistemApprovalPerusahaan = True Then
            cmb_Jabatan.Items.Add(JabatanUser_Direktur)
            cmb_Jabatan.Items.Add(JabatanUser_Manager)
            cmb_Jabatan.Items.Add(JabatanUser_Operator)
        Else
            cmb_Jabatan.Items.Add(JabatanUser_GeneralUser)
        End If
        cmb_Jabatan.Text = Nothing
    End Sub

    Sub KontenComboStatusAktif()
        cmb_StatusAktif.Items.Clear()
        cmb_StatusAktif.Items.Add("YA")
        cmb_StatusAktif.Items.Add("TIDAK")
        cmb_StatusAktif.Text = "YA"
    End Sub

    Private Sub cmb_Jabatan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Jabatan.SelectedIndexChanged
        If cmb_Jabatan.Text = JabatanUser_SuperUser _
            Or cmb_Jabatan.Text = JabatanUser_Direktur _
            Or cmb_Jabatan.Text = JabatanUser_GeneralUser _
            Then
            chk_ClusterFinance.Enabled = False
            chk_ClusterFinance.Checked = True
            chk_ClusterAccounting.Enabled = False
            chk_ClusterAccounting.Checked = True
        Else
            chk_ClusterFinance.Enabled = True
            chk_ClusterFinance.Checked = False
            chk_ClusterAccounting.Enabled = True
            chk_ClusterAccounting.Checked = False
        End If
    End Sub

    Private Sub chk_Fincance_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ClusterFinance.CheckedChanged
        If chk_ClusterFinance.Checked = True _
            And cmb_Jabatan.Text <> JabatanUser_SuperUser _
            And cmb_Jabatan.Text <> JabatanUser_GeneralUser _
            And cmb_Jabatan.Text <> JabatanUser_Direktur _
            Then
            chk_ClusterAccounting.Checked = False
        End If
    End Sub

    Private Sub chk_Accounting_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ClusterAccounting.CheckedChanged
        If chk_ClusterFinance.Checked = True _
            And cmb_Jabatan.Text <> JabatanUser_SuperUser _
            And cmb_Jabatan.Text <> JabatanUser_GeneralUser _
            And cmb_Jabatan.Text <> JabatanUser_Direktur _
            Then
            chk_ClusterFinance.Checked = False
        End If
    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Validasi Username :
        If txt_Username.Text = Nothing Then
            MsgBox("Username harap diisi.")
            txt_Username.Focus()
            Return
        End If
        If FungsiForm = FungsiForm_TAMBAH Then '(Validasi ketersediaan Username)
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_User WHERE Username = '" & txt_Username.Text & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                MsgBox("Username telah terdaftar." & Enter2Baris & "Silakan ketikkan Username yang lain.")
                txt_Username.Focus()
                Return
            End If
            AksesDatabase_General(Tutup)
        End If

        'Validasi Password :
        If txt_Password.Text = Nothing Then
            MsgBox("Password harap diisi.")
            txt_Password.Focus()
            Return
        End If

        If Microsoft.VisualBasic.Len(txt_Password.Text) < 8 Then
            MsgBox("Karakter Password jangan kurang dari 8 digit.")
            txt_Password.Focus()
            Return
        End If

        'Validasi Nama Lengkap :
        If txt_NamaLengkap.Text = Nothing Then
            MsgBox("Nama Lengkap harap diisi.")
            txt_NamaLengkap.Focus()
            Return
        End If

        'Validasi Jabatan :
        If cmb_Jabatan.Text = Nothing Then
            MsgBox("Silakan pilih Jabatan user.")
            cmb_Jabatan.Focus()
            Return
        End If

        'Validasi Cluster :
        If chk_ClusterFinance.Checked = False And chk_ClusterAccounting.Checked = False Then
            MsgBox("Silakan pilih Cluster user.")
            Return
        End If

        'Deklarasi Variabel :
        Dim UsernameInput = txt_Username.Text
        Dim PasswordInput = txt_Password.Text
        Dim JabatanUserInput = cmb_Jabatan.Text
        Dim LevelUserInput = 0
        If JabatanUserInput = JabatanUser_SuperUser Then LevelUserInput = LevelUser_09_SuperUser
        If JabatanUserInput = JabatanUser_GeneralUser Then LevelUserInput = levelUser_04_GeneralUser
        If JabatanUserInput = JabatanUser_Direktur Then LevelUserInput = LevelUser_03_Direktur
        If JabatanUserInput = JabatanUser_Manager Then LevelUserInput = LevelUser_02_Manager
        If JabatanUserInput = JabatanUser_Operator Then LevelUserInput = LevelUser_01_Operator
        Dim NamaLengkapInput = txt_NamaLengkap.Text
        Dim ClusterFinanceInput
        If chk_ClusterFinance.Checked = True Then
            ClusterFinanceInput = 1
        Else
            ClusterFinanceInput = 0
        End If
        Dim ClusterAccountingInput
        If chk_ClusterAccounting.Checked = True Then
            ClusterAccountingInput = 1
        Else
            ClusterAccountingInput = 0
        End If
        Dim StatusAktifInput = 0
        If cmb_StatusAktif.Text = "YA" Then StatusAktifInput = 1
        If cmb_StatusAktif.Text = "TIDAK" Then StatusAktifInput = 0

        'Proses Simpan/Edit
        Dim ProsesSuntingDatabaseUser As Boolean = False

        'Jika form berfungsi untuk menambah data/record :
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_User VALUES ('" & _
                                  UsernameInput & "', '" & _
                                  EnkripsiTeks(PasswordInput) & "', '" & _
                                  LevelUserInput & "', '" & _
                                  NamaLengkapInput & "', '" & _
                                  JabatanUserInput & "', '" & _
                                  ClusterFinanceInput & "', '" & _
                                  ClusterAccountingInput & "', '" & _
                                  StatusAktifInput & "' ) ", _
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabaseUser = True
            Catch ex As Exception
                ProsesSuntingDatabaseUser = False
            End Try
            AksesDatabase_General(Tutup)
        End If


        'Jika form berfungsi untuk mengedit data/record :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_User SET Password = '" & EnkripsiTeks(PasswordInput) & "', " & _
                                  " Level = '" & LevelUserInput & "', " & _
                                  " Nama = '" & NamaLengkapInput & "', " & _
                                  " Jabatan = '" & JabatanUserInput & "', " & _
                                  " Cluster_Finance = '" & ClusterFinanceInput & "', " & _
                                  " Cluster_Accounting = '" & ClusterAccountingInput & "', " & _
                                  " Status_Aktif = '" & StatusAktifInput & "' " & _
                                  " WHERE Username = '" & UsernameInput & "' " _
                                  , KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabaseUser = True
            Catch ex As Exception
                ProsesSuntingDatabaseUser = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        If ProsesSuntingDatabaseUser = True Then
            'frm_DataUser.RefreshTampilanData()
            'If FungsiForm = FungsiForm_TAMBAH Then
            '    ResetForm()
            '    MsgBox("Data User berhasil disimpan.")
            'End If
            'If FungsiForm = FungsiForm_EDIT Then
            '    MsgBox("Data User berhasil diedit.")
            '    Me.Close()
            'End If
        Else
            MsgBox("Data User gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class