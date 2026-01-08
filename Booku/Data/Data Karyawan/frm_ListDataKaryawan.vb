Imports bcomm
Imports System.Data.Odbc

Public Class frm_ListDataKaryawan

    Dim QueryTampilan
    Dim Baris_Terseleksi
    Dim FilterData
    Public JalurMasuk
    Public FungsiForm
    Public JudulForm

    Dim NomorIDKaryawan
    Dim NIK
    Dim NamaKaryawan
    Dim Jabatan

    Public NomorIDKaryawan_Terseleksi
    Public NIK_Terseleksi
    Public NamaKaryawan_Terseleksi
    Public Jabatan_Terseleksi

    Dim Cari_Karyawan
    Dim PilihKodeMitra

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Me.Text = JudulForm

        TampilkanData()

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan               'Penting...!!! Jangan dihapus...!!!
        txt_CariKaryawan.Text = Kosongan

        NomorIDKaryawan_Terseleksi = Kosongan
        NIK_Terseleksi = Kosongan
        NamaKaryawan_Terseleksi = Kosongan
        Jabatan_Terseleksi = Kosongan

        ProsesResetForm = False

    End Sub

    Sub TampilkanData()

        'Filter Pencarian
        Dim FilterCariKaryawan = " "
        If Cari_Karyawan <> Kosongan Then
            FilterCariKaryawan = " AND ( Nama_Karyawan LIKE '%" & Cari_Karyawan & "%' ) "
        End If

        'Filter Keseluruhan :
        FilterData = FilterCariKaryawan

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_DataKaryawan WHERE Status_Aktif = 1 " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NomorIDKaryawan = dr.Item("Nomor_ID_Karyawan")
            NIK = dr.Item("NIK")
            NamaKaryawan = dr.Item("Nama_Karyawan")
            Jabatan = dr.Item("Jabatan")
            DataTabelUtama.Rows.Add(NomorIDKaryawan, NIK, NamaKaryawan, Jabatan)
        Loop

        AksesDatabase_General(Tutup)


        BersihkanSeleksi()
        txt_CariKaryawan.Focus()

    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
    End Sub

    Private Sub txt_CariKodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_CariKaryawan.TextChanged
        Cari_Karyawan = txt_CariKaryawan.Text
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        If Baris_Terseleksi >= 0 Then
            btn_Pilih.Enabled = True
        Else
            btn_Pilih.Enabled = False
        End If
    End Sub
    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        btn_Pilih_Click(sender, e)
    End Sub

    Private Sub btn_Pilih_Click(sender As Object, e As EventArgs) Handles btn_Pilih.Click
        NomorIDKaryawan_Terseleksi = DataTabelUtama.Item("Nomor_ID_Karyawan", Baris_Terseleksi).Value
        NIK_Terseleksi = DataTabelUtama.Item("NIK_", Baris_Terseleksi).Value
        NamaKaryawan_Terseleksi = DataTabelUtama.Item("Nama_Karyawan", Baris_Terseleksi).Value
        Jabatan_Terseleksi = DataTabelUtama.Item("Jabatan_", Baris_Terseleksi).Value
        txt_CariKaryawan.Text = Kosongan
        Me.Close()
    End Sub

End Class