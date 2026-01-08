Imports bcomm
Imports System.Data.Odbc

Public Class frm_BukuPengawasanPemindahbukuan_BAK

    Dim QueryTampilan As String
    Dim JumlahBaris As Int64
    Dim BarisTerseleksi As Integer
    Dim FilterDariAkun
    Dim FilterKeAkun
    Dim FilterData
    Public PilihanFilter
    Dim FilterViewLevel
    Dim StatusSuntingData As Boolean
    Dim cmdCOA, cmdCOABANK As OdbcCommand
    Dim drCOA, drCOABANK As OdbcDataReader
    Dim ItemCmb_COA, ItemCmb_NamaAkun

    'Data Tabel :
    Dim NomorUrut
    Dim NomorID
    Dim NomorBPPB
    Dim TanggalBPPB
    Dim NomorKK
    Dim COAKredit
    Dim COADebet
    Dim DariBuku
    Dim KeBuku
    Dim Penanggungjawab
    Dim TanggalTransaksi
    Dim JumlahTransaksi
    Dim UraianTransaksi
    Dim NomorJV
    Dim User

    'Data Baris Tabel Terseleksi :
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPPB_Terseleksi
    Dim TanggalBPPB_Terseleksi
    Dim NomorKK_Terseleksi
    Dim COAKredit_Terseleksi
    Dim COADebet_Terseleksi
    Dim DariBuku_Terseleksi
    Dim KeBuku_Terseleksi
    Dim Penanggungjawab_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim UraianTransaksi_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Dim TotalTransaksiPemindahbukuan As Int64

    Private Sub frm_BukuPengawasanPemindabukuan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        KontenComboDariCOA()
        KontenComboKeCOA()
    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Dari Akun :
        If cmb_DariBuku.Text = "Semua" Then
            FilterDariAkun = " "
        Else
            FilterDariAkun = " AND COA_Kredit = '" & KonversiSaranaPembayaranKeCOA(cmb_DariBuku.Text) & "' "
        End If

        'Filter Ke Akun :
        If cmb_KeBuku.Text = "Semua" Then
            FilterKeAkun = " "
        Else
            FilterKeAkun = " AND COA_Debet = '" & KonversiSaranaPembayaranKeCOA(cmb_KeBuku.Text) & "' "
        End If

        'Query Tampilan :
        FilterData = FilterDariAkun & FilterKeAkun
        QueryTampilan = " SELECT * FROM tbl_Pemindahbukuan WHERE Nomor_BPPB <> 'X' " & FilterData

        TotalTransaksiPemindahbukuan = 0
        NomorUrut = 0

        AksesDatabase_Transaksi(Buka)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER by Nomor_ID ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPPB = dr.Item("Nomor_BPPB")
            TanggalBPPB = TanggalFormatTampilan(dr.Item("Tanggal_BPPB"))
            NomorKK = AmbilValue_NomorKKBerdasarkanNomorBP(NomorBPPB)
            COAKredit = dr.Item("COA_Kredit")
            COADebet = dr.Item("COA_Debet")
            DariBuku = KonversiCOAKeSaranaPembayaran(COAKredit)
            KeBuku = KonversiCOAKeSaranaPembayaran(COADebet)
            Penanggungjawab = dr.Item("Penanggungjawab")
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            If TanggalTransaksi = TanggalKosong Then TanggalTransaksi = StripKosong
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            UraianTransaksi = dr.Item("Uraian_Transaksi")
            NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPPB, TanggalBPPB, NomorKK, COAKredit, COADebet, DariBuku, KeBuku, Penanggungjawab,
                                    TanggalTransaksi, JumlahTransaksi, UraianTransaksi, NomorJV, User)
            If NomorJV = 0 Then
                DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaPudar
            Else
                TotalTransaksiPemindahbukuan += JumlahTransaksi
                DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaTegas
            End If
        Loop
        AksesDatabase_General(Tutup)
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

        If TotalTransaksiPemindahbukuan = 0 Then
            txt_TotalTransaksiPBk.Text = StripKosong
        Else
            txt_TotalTransaksiPBk.Text = TotalTransaksiPemindahbukuan
        End If

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Ajukan.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Sub KontenComboDariCOA()
        KontenComboSaranaPembayaran_Public(cmb_DariBuku)
        cmb_DariBuku.Items.Add("Semua")
        cmb_DariBuku.Text = "Semua"
    End Sub

    Sub KontenComboKeCOA()
        KontenComboSaranaPembayaran_Public(cmb_KeBuku)
        cmb_KeBuku.Items.Add("Semua")
        cmb_KeBuku.Text = "Semua"
    End Sub


    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index

        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", BarisTerseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", BarisTerseleksi).Value)
        NomorBPPB_Terseleksi = DataTabelUtama.Item("Nomor_BPPB", BarisTerseleksi).Value
        TanggalBPPB_Terseleksi = DataTabelUtama.Item("Tanggal_BPPB", BarisTerseleksi).Value
        NomorKK_Terseleksi = DataTabelUtama.Item("Nomor_KK", BarisTerseleksi).Value
        COAKredit_Terseleksi = DataTabelUtama.Item("COA_Kredit", BarisTerseleksi).Value
        COADebet_Terseleksi = DataTabelUtama.Item("COA_Debet", BarisTerseleksi).Value
        DariBuku_Terseleksi = DataTabelUtama.Item("Dari_Buku", BarisTerseleksi).Value
        KeBuku_Terseleksi = DataTabelUtama.Item("Ke_Buku", BarisTerseleksi).Value
        Penanggungjawab_Terseleksi = DataTabelUtama.Item("Penanggungjawab_", BarisTerseleksi).Value
        TanggalTransaksi_Terseleksi = DataTabelUtama.Item("Tanggal_Transaksi", BarisTerseleksi).Value
        JumlahTransaksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Transaksi", BarisTerseleksi).Value)
        UraianTransaksi_Terseleksi = DataTabelUtama.Item("Uraian_Transaksi", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value)
        User_Terseleksi = DataTabelUtama.Item("User_", BarisTerseleksi).Value


        If NomorJV_Terseleksi > 0 Then
            btn_Ajukan.Enabled = False
            btn_LihatJurnal.Enabled = True
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
            If NomorKK_Terseleksi = Kosongan Then
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
            End If
        Else
            btn_Ajukan.Enabled = True
            btn_LihatJurnal.Enabled = False
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

        If NomorID_Terseleksi = 0 Then BersihkanSeleksi()

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_DariBuku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DariBuku.SelectedIndexChanged
    End Sub
    Private Sub cmb_DariBuku_TextChanged(sender As Object, e As EventArgs) Handles cmb_DariBuku.TextChanged
        TampilkanData()
    End Sub

    Private Sub cmb_KeBuku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KeBuku.SelectedIndexChanged
    End Sub
    Private Sub cmb_KeBuku_TextChanged(sender As Object, e As EventArgs) Handles cmb_KeBuku.TextChanged
        TampilkanData()
    End Sub

    Private Sub txt_TotalTransaksiPBk_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalTransaksiPBk.TextChanged
        PemecahRibuanUntukTextBox(txt_TotalTransaksiPBk)
    End Sub

    Private Sub btn_Ajukan_Click(sender As Object, e As EventArgs) Handles btn_Ajukan.Click

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.dtp_TanggalKK.IsEnabled = False
        win_InputBuktiPengeluaran.dtp_TanggalKK.SelectedDate = TanggalFormatWPF(TanggalBPPB_Terseleksi)
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_Pemindahbukuan
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = KeBuku_Terseleksi
        win_InputBuktiPengeluaran.cmb_SaranaPembayaran.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_SaranaPembayaran.SelectedValue = DariBuku_Terseleksi
        win_InputBuktiPengeluaran.NomorBP = NomorBPPB_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Internal
        If JumlahTransaksi_Terseleksi = 0 Then JumlahTransaksi_Terseleksi = StripKosong
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, NomorBPPB_Terseleksi, TanggalBPPB_Terseleksi, UraianTransaksi_Terseleksi, NomorBPPB_Terseleksi,
                                JumlahTransaksi_Terseleksi, 0, 0, 0, 0, JumlahTransaksi_Terseleksi,
                                Kosongan, Kosongan, 0, 0, 0,
                                JumlahTransaksi_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        'frm_InputPemindahbukuan.ResetForm()
        'frm_InputPemindahbukuan.FungsiForm = FungsiForm_TAMBAH
        'frm_InputPemindahbukuan.ShowDialog()
        win_InputPemindahbukuan = New wpfWin_InputPemindahbukuan
        win_InputPemindahbukuan.ResetForm()
        win_InputPemindahbukuan.FungsiForm = FungsiForm_TAMBAH
        win_InputPemindahbukuan.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        'frm_InputPemindahbukuan.ResetForm()
        'frm_InputPemindahbukuan.FungsiForm = FungsiForm_EDIT
        'frm_InputPemindahbukuan.NomorID = NomorID_Terseleksi
        'frm_InputPemindahbukuan.txt_NomorBPPB.Text = NomorBPPB_Terseleksi
        'frm_InputPemindahbukuan.dtp_TanggalBPPB.Value = TanggalBPPB_Terseleksi
        'frm_InputPemindahbukuan.cmb_DariBuku.Text = DariBuku_Terseleksi
        'frm_InputPemindahbukuan.cmb_KeBuku.Text = KeBuku_Terseleksi
        'frm_InputPemindahbukuan.txt_Penanggungjawab.Text = Penanggungjawab_Terseleksi
        'frm_InputPemindahbukuan.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        'frm_InputPemindahbukuan.txt_UraianTransaksi.Text = UraianTransaksi_Terseleksi
        'frm_InputPemindahbukuan.ShowDialog()

        win_InputPemindahbukuan = New wpfWin_InputPemindahbukuan
        win_InputPemindahbukuan.ResetForm()
        win_InputPemindahbukuan.FungsiForm = FungsiForm_EDIT
        win_InputPemindahbukuan.NomorID = NomorID_Terseleksi
        win_InputPemindahbukuan.txt_NomorBPPB.Text = NomorBPPB_Terseleksi
        win_InputPemindahbukuan.dtp_TanggalBPPB.SelectedDate = TanggalFormatWPF(TanggalBPPB_Terseleksi)
        win_InputPemindahbukuan.cmb_DariBuku.SelectedValue = DariBuku_Terseleksi
        win_InputPemindahbukuan.cmb_KeBuku.SelectedValue = KeBuku_Terseleksi
        win_InputPemindahbukuan.txt_Penanggungjawab.Text = Penanggungjawab_Terseleksi
        win_InputPemindahbukuan.txt_JumlahKredit.Text = JumlahTransaksi_Terseleksi
        IsiValueElemenRichTextBox(win_InputPemindahbukuan.txt_UraianTransaksi, UraianTransaksi_Terseleksi)
        win_InputPemindahbukuan.NomorJV = NomorJV_Terseleksi
        win_InputPemindahbukuan.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Data terpilih akan dihapus dari Tabel ini dan Tabel Jurnal." & Enter2Baris &
                                "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return


        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabaseTransaksi = False Then
            MsgBox("Data terpilih GAGAL dihapus. " & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        'Hapus Data Pengajuan Pemindahbukuan
        cmd = New OdbcCommand(" DELETE FROM tbl_Pemindahbukuan WHERE Nomor_BPPB = '" & NomorBPPB_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        TampilkanData()

        MsgBox("Data terpilih berhasil DIHAPUS dari Tabel ini dan Tabel Jurnal.")

    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


End Class