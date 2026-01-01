Imports bcomm
Imports System.Data.Odbc

Public Class frm_KodeDivisi

    Dim KodeDivisi_Angka
    Dim KodeDivisi
    Dim Divisi


    Private Sub frm_KodeDivisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SistemPenomoranOtomatis_KodeDivisi()

        TampilkanData()

    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset ORDER BY Kode_Divisi", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            KodeDivisi = dr.Item("Kode_Divisi")
            Divisi = dr.Item("Divisi")
            DataGridView.Rows.Add(KodeDivisi, Divisi)
        Loop

        AksesDatabase_General(Tutup)

        DataGridView.ClearSelection()

    End Sub

    Sub SistemPenomoranOtomatis_KodeDivisi()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset WHERE Kode_Divisi IN (SELECT MAX(Kode_Divisi) FROM tbl_DivisiAsset ) ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            KodeDivisi_Angka = AmbilAngka(dr.Item("Kode_Divisi"))
        Else
            KodeDivisi_Angka = 0
        End If
        AksesDatabase_General(Tutup)
        KodeDivisi_Angka = KodeDivisi_Angka + 1
        If KodeDivisi_Angka < 100 Then KodeDivisi = "0" & KodeDivisi_Angka
        If KodeDivisi_Angka < 10 Then KodeDivisi = "00" & KodeDivisi_Angka
        txt_KodeDivisi.Text = KodeDivisi
    End Sub

    Private Sub txt_Divisi_TextChanged(sender As Object, e As EventArgs) Handles txt_Divisi.TextChanged
        If txt_Divisi.Text <> Nothing Then
            btn_Simpan.Enabled = True
        Else
            btn_Simpan.Enabled = False
        End If
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        KodeDivisi = txt_KodeDivisi.Text
        Divisi = txt_Divisi.Text

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset WHERE Divisi = '" & Divisi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Divisi '" & Divisi & "' sudah ada." & Enter2Baris & "Silakan ketik nama yang lain.")
            txt_Divisi.Text = Nothing
            txt_Divisi.Focus()
            Return
        End If
        AksesDatabase_General(Tutup)


        Dim PilihSimpan = MessageBox.Show("Setelah disimpan, Kode Divisi tidak akan bisa dihapus dan diedit...!!!" & Enter2Baris & "Yakin akan menyimpan..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihSimpan = vbNo Then Return

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" INSERT INTO tbl_DivisiAsset VALUES ( '" & KodeDivisi & "', '" & Divisi & "' )", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            SistemPenomoranOtomatis_KodeDivisi()
            txt_Divisi.Text = Nothing
            TampilkanData()
            MsgBox("Kode Divisi berhasil disimpan.")
        Else
            MsgBox("Kode Divisi gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

End Class