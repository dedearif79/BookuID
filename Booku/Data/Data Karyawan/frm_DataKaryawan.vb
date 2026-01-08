Imports bcomm
Imports System.Data.Odbc

Public Class frm_DataKaryawan

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim TanggalRegistrasi
    Dim NomorIDKaryawan
    Dim NIK
    Dim NamaKaryawan
    Dim Jabatan
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan
    Dim StatusAktif

    Dim TanggalRegistrasi_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorIDKaryawan_Terseleksi
    Dim NIK_Terseleksi
    Dim NamaKaryawan_Terseleksi
    Dim Jabatan_Terseleksi
    Dim RekeningBank_Terseleksi
    Dim AtasNama_Terseleksi
    Dim Catatan_Terseleksi
    Dim StatusAktif_Terseleksi


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
        Dim Index_BarisTabel = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DataKaryawan ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            TanggalRegistrasi = TanggalFormatTampilan(dr.Item("Tanggal_Registrasi"))
            NomorIDKaryawan = dr.Item("Nomor_ID_Karyawan")
            NIK = dr.Item("NIK")
            NamaKaryawan = dr.Item("Nama_Karyawan")
            Jabatan = dr.Item("Jabatan")
            RekeningBank = dr.Item("Rekening_Bank")
            AtasNama = dr.Item("Atas_Nama")
            Catatan = dr.Item("Catatan")
            StatusAktif = dr.Item("Status_Aktif")

            DataTabelUtama.Rows.Add(NomorUrut, TanggalRegistrasi, NomorIDKaryawan, NIK, NamaKaryawan, Jabatan, RekeningBank, AtasNama, Catatan, StatusAktif)

            If StatusAktif = 1 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If StatusAktif = 0 Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
            Index_BarisTabel += 1

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

        frm_InputDataKaryawan.ResetForm()
        frm_InputDataKaryawan.FungsiForm = FungsiForm_TAMBAH
        frm_InputDataKaryawan.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputDataKaryawan.ResetForm()
        frm_InputDataKaryawan.FungsiForm = FungsiForm_EDIT
        frm_InputDataKaryawan.dtp_TanggalRegistrasi.Value = TanggalRegistrasi_Terseleksi
        frm_InputDataKaryawan.txt_NomorIDKaryawan.Text = NomorIDKaryawan_Terseleksi
        frm_InputDataKaryawan.txt_NIK.Text = NIK_Terseleksi
        frm_InputDataKaryawan.txt_NamaKaryawan.Text = NamaKaryawan_Terseleksi
        frm_InputDataKaryawan.cmb_Jabatan.Text = Jabatan_Terseleksi
        frm_InputDataKaryawan.txt_RekeningBank.Text = RekeningBank_Terseleksi
        frm_InputDataKaryawan.txt_AtasNama.Text = AtasNama_Terseleksi
        frm_InputDataKaryawan.txt_Catatan.Text = Catatan_Terseleksi
        If StatusAktif_Terseleksi = 1 Then frm_InputDataKaryawan.chk_StatusAktif.Checked = True
        If StatusAktif_Terseleksi = 0 Then frm_InputDataKaryawan.chk_StatusAktif.Checked = False
        frm_InputDataKaryawan.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_DataKaryawan " &
                              " WHERE Nomor_ID_Karyawan   = '" & NomorIDKaryawan_Terseleksi & "' ",
                              KoneksiDatabaseGeneral)
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
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_Urut", BarisTerseleksi).Value)
        TanggalRegistrasi_Terseleksi = DataTabelUtama("Tanggal_Registrasi", BarisTerseleksi).Value
        NomorIDKaryawan_Terseleksi = DataTabelUtama("Nomor_ID_Karyawan", BarisTerseleksi).Value
        NIK_Terseleksi = DataTabelUtama("NIK_", BarisTerseleksi).Value
        NamaKaryawan_Terseleksi = DataTabelUtama("Nama_Karyawan", BarisTerseleksi).Value
        Jabatan_Terseleksi = DataTabelUtama("Jabatan_", BarisTerseleksi).Value
        RekeningBank_Terseleksi = DataTabelUtama("Rekening_Bank", BarisTerseleksi).Value
        AtasNama_Terseleksi = DataTabelUtama("Atas_Nama", BarisTerseleksi).Value
        Catatan_Terseleksi = DataTabelUtama("Catatan_", BarisTerseleksi).Value
        StatusAktif_Terseleksi = AmbilAngka(DataTabelUtama("Status_Aktif", BarisTerseleksi).Value)

        If BarisTerseleksi >= 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class