Imports bcomm
Imports MySql.Data.MySqlClient

Public Class X_frm_DataPerangkatApp

    Dim QueryTampilan
    Dim FilterData
    Dim BarisTerseleksi

    Private Sub frm_DataPerangkatApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        'Query Tampilan :
        FilterData = Nothing
        QueryTampilan = " SELECT * FROM tbl_perangkat WHERE Nomor_Seri_Produk <> 'X' " & FilterData

        'Style Tabel :
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        Dim NomorUrut, IDKomputer, NomorSeriProduk

        BukaDatabasePublic()
        If StatusKoneksiDatabase = False Then Return
        cmdPublic = New MySqlCommand(QueryTampilan, KoneksiDatabasePublic)
        drPublic = cmdPublic.ExecuteReader
        Do While drPublic.Read
            Try
                NomorUrut = drPublic.Item("Nomor_ID")
                IDKomputer = drPublic.Item("ID_Komputer")
                NomorSeriProduk = drPublic.Item("Nomor_Seri_Produk")
                DataGridView.Rows.Add(NomorUrut, IDKomputer, NomorSeriProduk)
            Catch ex As Exception
                DataGridView.Rows.Clear()
                pesan_AdaMasalahDenganKoneksiDatabase()
                Return
            End Try
        Loop
        TutupDatabasePublic()

        DataGridView.ClearSelection()
        BarisTerseleksi = -1

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim NomorUrutTerseleksi = DataGridView.Item("Nomor_ID", BarisTerseleksi).Value

        Dim PilihHapus = MessageBox.Show("Yakin akan menghapus Perangkat nomor " & NomorUrutTerseleksi & " ?", "Perhatian..!", MessageBoxButtons.YesNo)

        If PilihHapus = vbYes Then
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand("DELETE FROM tbl_perangkat WHERE Nomor_ID = '" & NomorUrutTerseleksi & "' ", KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
                MsgBox("Data berhasil dihapus.")
            Catch ex As Exception
                MsgBox("Data gagal dihapus.")
            End Try
            TutupDatabasePublic()
            TampilkanData()
        End If

    End Sub

End Class