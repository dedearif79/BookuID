Imports bcomm
Imports System.Data.Odbc

Public Class frm_TautanCOA

    Dim BarisTerseleksi
    Dim TautanCOATerseleksi
    Dim COATerseleksi
    Dim NamaAkunTerseleksi
    Dim QueryTampilan
    Dim NomorUrut
    Dim TautanCOA
    Dim COA
    Dim NamaAkun
    Dim StatusTautan
    Dim cmdCOA As OdbcCommand
    Dim drCOA As OdbcDataReader
    Dim JumlahTautan

    Private Sub frm_TautanCOA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()

        TampilkanData()

    End Sub


    Sub TampilkanData()

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_TautanCOA ORDER BY COA "

        'Style Tabel :
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        NomorUrut = 0
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut = NomorUrut + 1
            TautanCOA = dr.Item("Tautan_COA")
            COA = dr.Item("COA")
            If COA = Nothing Then COA = StripKosong
            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA " &
                                     " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader()
            If drCOA.HasRows Then
                NamaAkun = drCOA.Item("Nama_Akun")
                StatusTautan = "Sudah Tertaut"
            Else
                NamaAkun = "-"
                StatusTautan = "Belum Tertaut"
            End If
            DataGridView.Rows.Add(NomorUrut, TautanCOA, COA, NamaAkun, StatusTautan)
        Loop
        AksesDatabase_General(Tutup)
        DataGridView.ClearSelection()
        BarisTerseleksi = -1
        Pewarnaan()

        JumlahTautan = DataGridView.RowCount

        lbl_JumlahTautan.Text = JumlahTautan

    End Sub

    Sub Pewarnaan()
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Status_Tautan").Value = "Belum Tertaut" Then
                row.DefaultCellStyle.ForeColor = Color.Red
            Else
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub btn_EditTautan_Click(sender As Object, e As EventArgs) Handles btn_EditTautan.Click

        If BarisTerseleksi < 0 Then
            MsgBox("Tidak ada COA yang terseleksi")
            Return
        End If
        TautanCOATerseleksi = DataGridView.Item("Tautan_COA", BarisTerseleksi).Value
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_TautanCOA
        win_ListCOA.ShowDialog()
        COATerseleksi = win_ListCOA.COATerseleksi
        NamaAkunTerseleksi = win_ListCOA.NamaAkunTerseleksi

        If COATerseleksi = Nothing Then Return

        Try
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_TautanCOA SET COA = '" & COATerseleksi & "' WHERE Tautan_COA = '" & TautanCOATerseleksi & "' ", KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)
            DataGridView.Item("Kode_Akun", BarisTerseleksi).Value = COATerseleksi
            DataGridView.Item("Nama_Akun", BarisTerseleksi).Value = NamaAkunTerseleksi
            DataGridView.Item("Status_Tautan", BarisTerseleksi).Value = "Sudah Tertaut"
            Pewarnaan()
            IsiValueTautanCOA()
            MsgBox("COA telah berhasil ditautkan." & Enter2Baris & "Sebaiknya penautan ini tidak dirubah lagi jika sudah terjadi transaksi pada COA terkait.")
        Catch ex As Exception
            MsgBox("Penautan COA belum berhasil." & Enter2Baris & "Silakan dicoba lagi.")
        End Try

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
        BarisTerseleksi = DataGridView.CurrentRow.Index
    End Sub
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        BarisTerseleksi = DataGridView.CurrentRow.Index
    End Sub
    Private Sub DataGridView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        BarisTerseleksi = DataGridView.CurrentRow.Index
        btn_EditTautan_Click(sender, e)
    End Sub
    Private Sub DataGridView_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentDoubleClick
        BarisTerseleksi = DataGridView.CurrentRow.Index
        btn_EditTautan_Click(sender, e)
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class