Imports bcomm
Imports System.Data.Odbc

Public Class frm_DataPemegangSaham

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim Nama
    Dim NIK
    Dim NPWP
    Dim Alamat
    Dim JenisPS
    Dim LokasiPS
    Dim JumlahLembar
    Dim HargaPerLembar
    Dim JumlahSaham
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim Nama_Terseleksi
    Dim NIK_Terseleksi
    Dim NPWP_Terseleksi
    Dim Alamat_Terseleksi
    Dim JenisPS_Terseleksi
    Dim LokasiPS_Terseleksi
    Dim JumlahLembar_Terseleksi
    Dim HargaPerLembar_Terseleksi
    Dim JumlahSaham_Terseleksi
    Dim RekeningBank_Terseleksi
    Dim AtasNama_Terseleksi
    Dim Catatan_Terseleksi


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

        'Data Tabel:
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PemegangSaham ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            Nama = dr.Item("Nama")
            NIK = dr.Item("NIK")
            NPWP = dr.Item("NPWP")
            Alamat = dr.Item("Alamat")
            JenisPS = dr.Item("Jenis_PS")
            LokasiPS = dr.Item("Lokasi_PS")
            JumlahLembar = 0
            HargaPerLembar = 0
            JumlahSaham = 0
            RekeningBank = dr.Item("Rekening_Bank")
            AtasNama = dr.Item("Atas_Nama")
            Catatan = dr.Item("Catatan")

            DataTabelUtama.Rows.Add(NomorUrut, NomorID, Nama, NIK, NPWP, Alamat, JenisPS, LokasiPS,
                                    JumlahLembar, HargaPerLembar, JumlahSaham, RekeningBank, AtasNama, Catatan)

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

        frm_InputDataPemegangSaham.ResetForm()
        frm_InputDataPemegangSaham.FungsiForm = FungsiForm_TAMBAH
        frm_InputDataPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputDataPemegangSaham.ResetForm()
        frm_InputDataPemegangSaham.FungsiForm = FungsiForm_EDIT
        frm_InputDataPemegangSaham.NomorID = NomorID_Terseleksi
        frm_InputDataPemegangSaham.txt_Nama.Text = Nama_Terseleksi
        frm_InputDataPemegangSaham.txt_NIK.Text = NIK_Terseleksi
        frm_InputDataPemegangSaham.txt_NPWP.Text = NPWP_Terseleksi
        frm_InputDataPemegangSaham.txt_Alamat.Text = Alamat_Terseleksi
        frm_InputDataPemegangSaham.cmb_JenisPS.Text = JenisPS_Terseleksi
        frm_InputDataPemegangSaham.cmb_LokasiPS.Text = LokasiPS_Terseleksi
        frm_InputDataPemegangSaham.txt_JumlahLembar.Text = JumlahLembar_Terseleksi
        frm_InputDataPemegangSaham.txt_HargaPerlembar.Text = HargaPerLembar_Terseleksi
        frm_InputDataPemegangSaham.txt_JumlahSaham.Text = JumlahSaham_Terseleksi
        frm_InputDataPemegangSaham.txt_RekeningBank.Text = RekeningBank_Terseleksi
        frm_InputDataPemegangSaham.txt_AtasNama.Text = AtasNama_Terseleksi
        frm_InputDataPemegangSaham.txt_Catatan.Text = Catatan_Terseleksi
        frm_InputDataPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_PemegangSaham WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseGeneral)
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
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_ID", BarisTerseleksi).Value)
        Nama_Terseleksi = DataTabelUtama("Nama_", BarisTerseleksi).Value
        NIK_Terseleksi = DataTabelUtama("NIK_", BarisTerseleksi).Value
        NPWP_Terseleksi = DataTabelUtama("NPWP_", BarisTerseleksi).Value
        Alamat_Terseleksi = DataTabelUtama("Alamat_", BarisTerseleksi).Value
        JenisPS_Terseleksi = DataTabelUtama("Jenis_PS", BarisTerseleksi).Value
        LokasiPS_Terseleksi = DataTabelUtama("Lokasi_PS", BarisTerseleksi).Value
        JumlahLembar_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Lembar", BarisTerseleksi).Value)
        HargaPerLembar_Terseleksi = AmbilAngka(DataTabelUtama("Harga_Per_Lembar", BarisTerseleksi).Value)
        JumlahSaham_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Saham", BarisTerseleksi).Value)
        RekeningBank_Terseleksi = DataTabelUtama("Rekening_Bank", BarisTerseleksi).Value
        AtasNama_Terseleksi = DataTabelUtama("Atas_Nama", BarisTerseleksi).Value
        Catatan_Terseleksi = DataTabelUtama("Catatan_", BarisTerseleksi).Value

        If NomorID_Terseleksi > 0 Then
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