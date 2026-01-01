Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_DataProject_BAK

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim KodeProject
    Dim NamaProject
    Dim NomorPO
    Dim KodeCustomer
    Dim NamaCustomer
    Dim NilaiProject
    Dim Keterangan
    Dim Status

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim KodeProject_Terseleksi
    Dim NamaProject_Terseleksi
    Dim NomorPO_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim NilaiProject_Terseleksi
    Dim Keterangan_Terseleksi
    Dim Status_Terseleksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)


        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DataProject ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            KodeProject = dr.Item("Kode_Project")
            NamaProject = dr.Item("Nama_Project")
            NomorPO = dr.Item("Nomor_PO")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            NilaiProject = dr.Item("Nilai_Project")
            Keterangan = dr.Item("Keterangan")
            Status = dr.Item("Status")

            DataTabelUtama.Rows.Add(NomorUrut, NomorID, KodeProject, NamaProject, NomorPO, KodeCustomer, NamaCustomer, NilaiProject, Keterangan, Status)

        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        frm_InputDataProject.ResetForm()
        frm_InputDataProject.FungsiForm = FungsiForm_TAMBAH
        frm_InputDataProject.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputDataProject.ResetForm()
        frm_InputDataProject.FungsiForm = FungsiForm_EDIT
        frm_InputDataProject.txt_KodeProject.Text = KodeProject_Terseleksi
        frm_InputDataProject.txt_NamaProject.Text = NamaProject_Terseleksi
        frm_InputDataProject.txt_NomorPO.Text = NomorPO_Terseleksi
        frm_InputDataProject.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        frm_InputDataProject.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        frm_InputDataProject.txt_NilaiProject.Text = NilaiProject_Terseleksi
        frm_InputDataProject.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputDataProject.cmb_Status.Text = Status_Terseleksi
        frm_InputDataProject.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_DataProject " &
                              " WHERE Kode_Project = '" & KodeProject_Terseleksi & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        NomorUrut_Terseleksi = 0
        NomorID_Terseleksi = 0
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", BarisTerseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", BarisTerseleksi).Value)
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", BarisTerseleksi).Value
        NamaProject_Terseleksi = DataTabelUtama.Item("Nama_Project", BarisTerseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", BarisTerseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", BarisTerseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", BarisTerseleksi).Value
        NilaiProject_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nilai_Project", BarisTerseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", BarisTerseleksi).Value
        Status_Terseleksi = DataTabelUtama.Item("Status_", BarisTerseleksi).Value

        If NomorUrut_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If BarisTerseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class