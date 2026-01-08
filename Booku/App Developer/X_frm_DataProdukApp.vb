Imports bcomm
Imports MySql.Data.MySqlClient

Public Class X_frm_DataProdukApp

    Dim QueryTampilan
    Dim FilterData
    Dim BarisTerseleksi

    Private Sub frm_DataProduk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        'Query Tampilan :
        FilterData = Nothing
        QueryTampilan = " SELECT * FROM tbl_produk WHERE Nomor_Seri_Produk <> 'X' " & FilterData

        'Style Tabel :
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        Dim NomorSeriProduk, IDCustomer, JumlahPerangkat, StatusTerpakai

        BukaDatabasePublic()
        If StatusKoneksiDatabase = False Then Return
        cmdPublic = New MySqlCommand(QueryTampilan, KoneksiDatabasePublic)
        drPublic = cmdPublic.ExecuteReader
        Do While drPublic.Read
            Try
                NomorSeriProduk = drPublic.Item("Nomor_Seri_Produk")
                IDCustomer = drPublic.Item("ID_Customer")
                JumlahPerangkat = drPublic.Item("Jumlah_Perangkat")
                StatusTerpakai = drPublic.Item("Status_Terpakai")
                DataGridView.Rows.Add(NomorSeriProduk, IDCustomer, JumlahPerangkat, StatusTerpakai)
            Catch ex As Exception
                DataGridView.Rows.Clear()
                pesan_AdaMasalahDenganKoneksiDatabase()
                Return
            End Try
        Loop
        TutupDatabasePublic()

        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Status_Terpakai").Value = 1 Then row.DefaultCellStyle.ForeColor = Color.Red
            If row.Cells("Status_Terpakai").Value = 0 Then row.DefaultCellStyle.ForeColor = Color.Black
        Next

        DataGridView.ClearSelection()
        BarisTerseleksi = -1

    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        X_frm_InputProdukApp.ResetForm()
        X_frm_InputProdukApp.FungsiForm = FungsiForm_TAMBAH
        X_frm_InputProdukApp.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim NomorSeriProdukTerseleksi = DataGridView.Item("Nomor_Seri_Produk", BarisTerseleksi).Value
        Dim IDCustomerTerseleksi = DataGridView.Item("ID_Customer", BarisTerseleksi).Value
        Dim JumlahPerangkatTerseleksi = DataGridView.Item("Jumlah_Perangkat", BarisTerseleksi).Value
        Dim StatusTerpakaiTerseleksi = DataGridView.Item("Status_Terpakai", BarisTerseleksi).Value
        X_frm_InputProdukApp.ResetForm()
        X_frm_InputProdukApp.FungsiForm = FungsiForm_EDIT
        X_frm_InputProdukApp.txt_NomorSeriProduk.Text = NomorSeriProdukTerseleksi
        X_frm_InputProdukApp.txt_IDCustomer.Text = IDCustomerTerseleksi
        X_frm_InputProdukApp.txt_JumlahPerangkat.Text = JumlahPerangkatTerseleksi
        X_frm_InputProdukApp.txt_StatusTerpakai.Text = StatusTerpakaiTerseleksi
        X_frm_InputProdukApp.ShowDialog()
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim NomorSeriProdukTerseleksi = DataGridView.Item("Nomor_Seri_Produk", BarisTerseleksi).Value
        Dim StatusTerpakaiTerseleksi = DataGridView.Item("Status_Terpakai", BarisTerseleksi).Value

        If StatusTerpakaiTerseleksi = 1 Then
            MsgBox("Produk ini sudah terpakai. Tidak dapat dihapus.")
            Return
        End If

        Dim PilihHapus = MessageBox.Show("Yakin akan menghapus Produk Nomor Seri " & NomorSeriProdukTerseleksi & " ?", "Perhatian..!", MessageBoxButtons.YesNo)

        If PilihHapus = vbYes Then
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand("DELETE FROM tbl_produk WHERE Nomor_Seri_Produk = '" & NomorSeriProdukTerseleksi & "' ", KoneksiDatabasePublic)
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