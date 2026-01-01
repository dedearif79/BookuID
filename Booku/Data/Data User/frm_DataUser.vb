Imports bcomm
Imports System.Data.Odbc

Public Class frm_DataUser

    Dim BarisTerseleksi

    Private Sub frm_TemplateModul_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshTampilanData()
    End Sub

    Sub TampilkanData()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand("SELECT * FROM tbl_User ORDER BY Level DESC", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader()
        Do While dr.Read
            Dim clm_Username = dr.Item("Username")
            Dim clm_Password = DekripsiTeks(dr.Item("Password"))
            Dim clm_Level = dr.Item("Level")
            Dim clm_NamaLengkap = dr.Item("Nama")
            Dim clm_Jabatan = dr.Item("Jabatan")
            Dim clm_Cluster = Nothing
            Dim clm_Finance = dr.Item("Cluster_Finance")
            Dim clm_Accounting = dr.Item("Cluster_Accounting")
            If clm_Finance = 1 Then clm_Cluster = clm_Cluster & "Finance  &  "
            If clm_Accounting = 1 Then clm_Cluster = clm_Cluster & "Accounting  &  "
            Dim ClusterReverse = Microsoft.VisualBasic.StrReverse(clm_Cluster)
            Dim clm_StatusAktif = Nothing
            If dr.Item("Status_Aktif") = 1 Then
                clm_StatusAktif = "YA"
            Else
                clm_StatusAktif = "TIDAK"
            End If
            clm_Cluster = Microsoft.VisualBasic.StrReverse(Microsoft.VisualBasic.Mid(ClusterReverse, 6))
            DataGridView.Rows.Add(clm_Username, clm_Password, clm_Level, clm_NamaLengkap, clm_Jabatan, clm_Cluster, clm_Finance, clm_Accounting, clm_StatusAktif)
        Loop
        AksesDatabase_General(Tutup)
        DataGridView.ClearSelection()

        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Status_Aktif").Value = "YA" Then
                row.DefaultCellStyle.ForeColor = Color.Black
            Else
                row.DefaultCellStyle.ForeColor = Color.Red
            End If
        Next

    End Sub

    Sub RefreshTampilanData()
        DataGridView.Rows.Clear()
        TampilkanData()
        BarisTerseleksi = -1
        btn_Blokir.Enabled = False
        btn_Edit.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim UsernameTerseleksi = DataGridView.Item("Username_", BarisTerseleksi).Value
        Dim StatusAktifTerseleksi = DataGridView.Item("Status_Aktif", BarisTerseleksi).Value
        btn_Edit.Enabled = True
        If UsernameTerseleksi = UserAktif Then
            btn_Blokir.Enabled = False
        Else
            btn_Blokir.Enabled = True
        End If
        If StatusAktifTerseleksi = "YA" Then
            btn_Blokir.Text = "Blokir"
        Else
            btn_Blokir.Text = "Aktifkan"
        End If
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        frm_InputUser.FungsiForm = FungsiForm_TAMBAH
        frm_InputUser.ResetForm()
        frm_InputUser.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputUser.FungsiForm = FungsiForm_EDIT
        frm_InputUser.ResetForm()
        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim UsernameTerseleksi = DataGridView.Item("Username_", BarisTerseleksi).Value
        Dim PasswordUserTerseleksi = DataGridView.Item("Password_User", BarisTerseleksi).Value
        Dim NamaUserTerseleksi = DataGridView.Item("Nama_User", BarisTerseleksi).Value
        Dim JabatanUserTerseleksi = DataGridView.Item("Jabatan_User", BarisTerseleksi).Value
        Dim ClusterFinanceTerseleksi = DataGridView.Item("Cluster_Finance", BarisTerseleksi).Value
        Dim ClusterAccountingTerseleksi = DataGridView.Item("Cluster_Accounting", BarisTerseleksi).Value
        Dim StatusAktifTerseleksi = DataGridView.Item("Status_Aktif", BarisTerseleksi).Value
        frm_InputUser.txt_Username.Text = UsernameTerseleksi
        frm_InputUser.txt_Password.Text = PasswordUserTerseleksi
        frm_InputUser.txt_NamaLengkap.Text = NamaUserTerseleksi
        frm_InputUser.cmb_Jabatan.Text = JabatanUserTerseleksi
        If ClusterFinanceTerseleksi = 1 Then
            frm_InputUser.chk_ClusterFinance.Checked = True
        Else
            frm_InputUser.chk_ClusterFinance.Checked = False
        End If
        If ClusterAccountingTerseleksi = 1 Then
            frm_InputUser.chk_ClusterAccounting.Checked = True
        Else
            frm_InputUser.chk_ClusterAccounting.Checked = False
        End If
        frm_InputUser.cmb_StatusAktif.Text = StatusAktifTerseleksi
        frm_InputUser.ShowDialog()
    End Sub

    Private Sub btn_Blokir_Click(sender As Object, e As EventArgs) Handles btn_Blokir.Click
        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim UsernameTerseleksi = DataGridView.Item("Username_", BarisTerseleksi).Value
        Dim NamaUserTerseleksi = DataGridView.Item("Nama_User", BarisTerseleksi).Value
        Dim StatusAktifEdit As Integer
        Dim JenisEksekusi = Nothing
        If btn_Blokir.Text = "Blokir" Then
            StatusAktifEdit = 0
            JenisEksekusi = "memblokir"
        Else
            StatusAktifEdit = 1
            JenisEksekusi = "mengaktifkan"
        End If
        Dim PilihCetakSekarang = MessageBox.Show("Yakin akan " & JenisEksekusi & " user '" & NamaUserTerseleksi & "'?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihCetakSekarang = vbNo Then Return
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_User SET Status_Aktif = '" & StatusAktifEdit & "' WHERE Username = '" & UsernameTerseleksi & "' ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            RefreshTampilanData()
            If StatusAktifEdit = 1 Then
                MsgBox("User '" & NamaUserTerseleksi & "' berhasil diaktifkan.")
            Else
                MsgBox("User '" & NamaUserTerseleksi & "' berhasil diblokir.")
            End If
        Catch ex As Exception
            If StatusAktifEdit = 1 Then
                MsgBox("User '" & NamaUserTerseleksi & "' gagal diaktifkan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            Else
                MsgBox("User '" & NamaUserTerseleksi & "' gagal diblokir." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End Try
        AksesDatabase_General(Tutup)
    End Sub

End Class