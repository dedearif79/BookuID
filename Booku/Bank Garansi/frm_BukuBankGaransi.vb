Imports bcomm
Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_BukuBankGaransi

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPBG
    Dim NomorKontrak
    Dim TanggalTransaksi
    Dim NamaBank
    Dim Keperluan
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahTransaksi
    Dim BiayaProvisi
    Dim TanggalPencairan
    Dim Keterangan
    Dim NomorJV_Transaksi
    Dim NomorJV_Pencairan
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPBG_Terseleksi
    Dim NomorKontrak_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim NamaBank_Terseleksi
    Dim Keperluan_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim BiayaProvisi_Terseleksi
    Dim TanggalPencairan_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Transaksi_Terseleksi
    Dim NomorJV_Pencairan_Terseleksi
    Dim User_Terseleksi

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

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_BankGaransi ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPBG = dr.Item("Nomor_BPBG")
            NomorKontrak = dr.Item("Nomor_Kontrak")
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            NamaBank = dr.Item("Nama_Bank")
            Keperluan = dr.Item("Keperluan")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            BiayaProvisi = dr.Item("Biaya_Provisi")
            TanggalPencairan = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            If TanggalPencairan = TanggalKosong Then TanggalPencairan = StripKosong
            Keterangan = dr.Item("Keterangan")
            NomorJV_Transaksi = dr.Item("Nomor_JV_Transaksi")
            NomorJV_Pencairan = dr.Item("Nomor_JV_Pencairan")
            User = dr.Item("User")
            DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPBG, NomorKontrak, TanggalTransaksi, NamaBank, Keperluan,
                                    KodeLawanTransaksi, NamaLawanTransaksi, JumlahTransaksi, BiayaProvisi,
                                    TanggalPencairan, Keterangan, NomorJV_Transaksi, NomorJV_Pencairan, User)
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnalTransaksi.Enabled = False
        btn_LihatJurnalPencairan.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_LihatJurnalTransaksi_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnalTransaksi.Click
        LihatJurnal(NomorJV_Transaksi_Terseleksi)
    End Sub

    Private Sub btn_LihatJurnalPencairan_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnalPencairan.Click
        LihatJurnal(NomorJV_Pencairan_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        frm_InputBankGaransi.ResetForm()
        frm_InputBankGaransi.FungsiForm = FungsiForm_TAMBAH
        frm_InputBankGaransi.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputBankGaransi.ResetForm()
        frm_InputBankGaransi.FungsiForm = FungsiForm_EDIT
        frm_InputBankGaransi.NomorID = NomorID_Terseleksi
        frm_InputBankGaransi.NomorJV_Transaksi = NomorJV_Transaksi_Terseleksi
        frm_InputBankGaransi.dtp_TanggalTransaksi.Value = TanggalTransaksi_Terseleksi
        frm_InputBankGaransi.txt_NomorBPBG.Text = NomorBPBG_Terseleksi
        frm_InputBankGaransi.txt_NomorKontrak.Text = NomorKontrak_Terseleksi
        frm_InputBankGaransi.txt_NamaBank.Text = NamaBank_Terseleksi
        frm_InputBankGaransi.txt_Keperluan.Text = Keperluan_Terseleksi
        frm_InputBankGaransi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        frm_InputBankGaransi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Terseleksi
        frm_InputBankGaransi.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        frm_InputBankGaransi.txt_BiayaProvisi.Text = BiayaProvisi_Terseleksi
        frm_InputBankGaransi.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputBankGaransi.ShowDialog()

    End Sub

    Private Sub btn_Cairkan_Click(sender As Object, e As EventArgs) Handles btn_Cairkan.Click
        FiturBelumBisaDigunakan()
        Return
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_BankGaransi " &
                              " WHERE Nomor_BPBG = '" & NomorBPBG_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

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
        NomorBPBG_Terseleksi = DataTabelUtama("Nomor_BPBG", BarisTerseleksi).Value
        NomorKontrak_Terseleksi = DataTabelUtama("Nomor_Kontrak", BarisTerseleksi).Value
        TanggalTransaksi_Terseleksi = DataTabelUtama("Tanggal_Transaksi", BarisTerseleksi).Value
        NamaBank_Terseleksi = DataTabelUtama("Nama_Bank", BarisTerseleksi).Value
        Keperluan_Terseleksi = DataTabelUtama("Keperluan_", BarisTerseleksi).Value
        KodeLawanTransaksi_Terseleksi = DataTabelUtama("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaLawanTransaksi_Terseleksi = DataTabelUtama("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        JumlahTransaksi_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Transaksi", BarisTerseleksi).Value)
        BiayaProvisi_Terseleksi = AmbilAngka(DataTabelUtama("Biaya_Provisi", BarisTerseleksi).Value)
        TanggalPencairan_Terseleksi = DataTabelUtama("Tanggal_Pencairan", BarisTerseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        NomorJV_Transaksi_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV_Transaksi", BarisTerseleksi).Value)
        NomorJV_Pencairan_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV_Pencairan", BarisTerseleksi).Value)
        User_Terseleksi = AmbilAngka(DataTabelUtama("User_", BarisTerseleksi).Value)

        If NomorID_Terseleksi > 0 Then
            If NomorJV_Transaksi_Terseleksi > 0 Then
                btn_LihatJurnalTransaksi.Enabled = True
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
            Else
                btn_LihatJurnalTransaksi.Enabled = False
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
            End If
            If NomorJV_Pencairan_Terseleksi > 0 Then
                btn_LihatJurnalPencairan.Enabled = True
            Else
                btn_LihatJurnalPencairan.Enabled = False
            End If
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