Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanDepositOperasional_X

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPDO
    Dim NomorBukti
    Dim TanggalBukti
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahTransaksi
    Dim JumlahTalangan
    Dim SisaTalangan
    Dim JumlahReimburse
    Dim PotonganReimburse
    Dim Keterangan
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPDO_Terseleksi
    Dim NomorBukti_Terseleksi
    Dim TanggalBukti_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim JumlahTalangan_Terseleksi
    Dim SisaTalangan_Terseleksi
    Dim JumlahReimburse_Terseleksi
    Dim PotonganReimburse_Terseleksi
    Dim Keterangan_Terseleksi
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

        cmd = New OdbcCommand(" SELECT * FROM tbl_DepositOperasional ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorID = dr.Item("Nomor_ID")
            NomorBPDO = dr.Item("Nomor_BPDO")
            NomorBukti = dr.Item("Nomor_Bukti")
            TanggalBukti = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            JumlahTalangan = 0
            cmdTELUSUR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                         " WHERE Nomor_BP = '" & NomorBPDO & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                JumlahTalangan += drTELUSUR.Item("Jumlah_Bayar")
            Loop
            SisaTalangan = JumlahTransaksi - JumlahTalangan
            JumlahReimburse = dr.Item("Jumlah_Reimburse")
            PotonganReimburse = dr.Item("Potongan_Reimburse")
            Keterangan = dr.Item("Keterangan")
            'NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            TambahBaris()
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        If JumlahTransaksi = 0 Then JumlahTransaksi = StripKosong
        If JumlahTalangan = 0 Then JumlahTalangan = StripKosong
        If SisaTalangan = 0 Then SisaTalangan = StripKosong
        If JumlahReimburse = 0 Then JumlahReimburse = StripKosong
        If PotonganReimburse = 0 Then PotonganReimburse = StripKosong
        DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPDO, NomorBukti, TanggalBukti, NomorFakturPajak,
                                    KodeLawanTransaksi, NamaLawanTransaksi, KodeCustomer, NamaCustomer,
                                    JumlahTransaksi, JumlahTalangan, SisaTalangan, JumlahReimburse, PotonganReimburse,
                                    Keterangan, User)
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_LihatJurnal.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click

        'frm_JurnalVoucher.ResetForm()
        'frm_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        'frm_JurnalVoucher.Angka_NomorJV = NomorJV_Terseleksi
        'frm_JurnalVoucher.ShowDialog()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        Dim win_InputDepositOperasional As New wpfWin_InputDepositOperasional
        win_InputDepositOperasional.ResetForm()
        win_InputDepositOperasional.FungsiForm = FungsiForm_TAMBAH
        win_InputDepositOperasional.ShowDialog()

        'frm_InputDepositOperasional.ResetForm()
        'frm_InputDepositOperasional.FungsiForm = FungsiForm_TAMBAH
        'frm_InputDepositOperasional.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim win_InputDepositOperasional As New wpfWin_InputDepositOperasional
        win_InputDepositOperasional.ResetForm()
        win_InputDepositOperasional.FungsiForm = FungsiForm_EDIT
        win_InputDepositOperasional.NomorID = NomorID_Terseleksi
        win_InputDepositOperasional.txt_NomorBPDO.Text = NomorBPDO_Terseleksi
        win_InputDepositOperasional.txt_NomorBukti.Text = NomorBukti_Terseleksi
        win_InputDepositOperasional.dtp_TanggalBukti.SelectedDate = TanggalFormatWPF(TanggalBukti_Terseleksi)
        win_InputDepositOperasional.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        win_InputDepositOperasional.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        win_InputDepositOperasional.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Terseleksi
        win_InputDepositOperasional.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        win_InputDepositOperasional.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        win_InputDepositOperasional.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        IsiValueElemenRichTextBox(win_InputDepositOperasional.txt_Keterangan, Keterangan_Terseleksi)
        win_InputDepositOperasional.ShowDialog()

        'frm_InputDepositOperasional.ResetForm()
        'frm_InputDepositOperasional.FungsiForm = FungsiForm_EDIT
        'frm_InputDepositOperasional.NomorID = NomorID_Terseleksi
        ''frm_InputDepositOperasional.NomorJV = NomorJV_Terseleksi
        'frm_InputDepositOperasional.txt_NomorBPDO.Text = NomorBPDO_Terseleksi
        'frm_InputDepositOperasional.txt_NomorBukti.Text = NomorBukti_Terseleksi
        'frm_InputDepositOperasional.dtp_TanggalBukti.Value = TanggalBukti_Terseleksi
        'frm_InputDepositOperasional.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        'frm_InputDepositOperasional.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        'frm_InputDepositOperasional.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Terseleksi
        'frm_InputDepositOperasional.txt_UraianTransaksi.Text = UraianTransaksi_Terseleksi
        'frm_InputDepositOperasional.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        'frm_InputDepositOperasional.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        'frm_InputDepositOperasional.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        'frm_InputDepositOperasional.txt_Keterangan.Text = Keterangan_Terseleksi
        'frm_InputDepositOperasional.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_DepositOperasional " &
                              " WHERE Nomor_BPDO = '" & NomorBPDO_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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
        NomorBPDO_Terseleksi = DataTabelUtama("Nomor_BPDO", BarisTerseleksi).Value
        NomorBukti_Terseleksi = DataTabelUtama("Nomor_Bukti", BarisTerseleksi).Value
        TanggalBukti_Terseleksi = DataTabelUtama("Tanggal_Bukti", BarisTerseleksi).Value
        NomorFakturPajak_Terseleksi = DataTabelUtama("Nomor_Faktur_Pajak", BarisTerseleksi).Value
        KodeLawanTransaksi_Terseleksi = DataTabelUtama("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaLawanTransaksi_Terseleksi = DataTabelUtama("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama("Kode_Customer", BarisTerseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama("Nama_Customer", BarisTerseleksi).Value
        JumlahTransaksi_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Transaksi", BarisTerseleksi).Value)
        JumlahTalangan_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Talangan", BarisTerseleksi).Value)
        SisaTalangan_Terseleksi = AmbilAngka(DataTabelUtama("Sisa_Talangan", BarisTerseleksi).Value)
        JumlahReimburse_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Reimburse", BarisTerseleksi).Value)
        PotonganReimburse_Terseleksi = AmbilAngka(DataTabelUtama("Potongan_Reimburse", BarisTerseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        'NomorJV_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV", BarisTerseleksi).Value)
        User_Terseleksi = DataTabelUtama("User_", BarisTerseleksi).Value

        If NomorID_Terseleksi > 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
            'If NomorJV_Terseleksi > 0 Then
            '    btn_LihatJurnal.Enabled = True
            '    btn_Edit.Enabled = False
            '    btn_Hapus.Enabled = False
            'Else
            '    btn_LihatJurnal.Enabled = False
            '    btn_Edit.Enabled = True
            '    btn_Hapus.Enabled = True
            'End If
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