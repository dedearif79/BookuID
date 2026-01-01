Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanPiutangPemegangSaham_X

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPPPS
    Dim NomorDokumen
    Dim NIK
    Dim NamaPemegangSaham
    Dim TanggalPinjam
    Dim JumlahPiutang
    Dim SaldoAwal
    Dim JumlahAngsuran
    Dim SaldoAkhir
    Dim Keterangan
    Dim NomorJV

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPPPS_Terseleksi
    Dim NomorDokumen_Terseleksi
    Dim NIK_Terseleksi
    Dim NamaPemegangSaham_Terseleksi
    Dim TanggalPinjam_Terseleksi
    Dim JumlahPiutang_Terseleksi
    Dim SaldoAwal_Terseleksi
    Dim JumlahAngsuran_Terseleksi
    Dim SaldoAkhir_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim Referensi_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer

    Dim TermasukPiutangTahunIni_Terseleksi As Boolean


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
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangPemegangSaham ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPPPS = dr.Item("Nomor_BPPPS")
            NomorDokumen = dr.Item("Nomor_Kontrak")
            NIK = dr.Item("Kode_Lawan_Transaksi")
            NamaPemegangSaham = AmbilValue_NamaPemegangSaham(NIK)
            TanggalPinjam = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            JumlahPiutang = dr.Item("Jumlah_Pinjaman")
            SaldoAwal = dr.Item("Saldo_Awal")
            JumlahAngsuran = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP     = '" & NomorBPPPS & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahAngsuran += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SaldoAkhir = SaldoAwal - JumlahAngsuran
            Keterangan = dr.Item("Keterangan")
            NomorJV = dr.Item("Nomor_JV")

            If JumlahPiutang = 0 Then JumlahPiutang = StripKosong
            If JumlahAngsuran = 0 Then JumlahAngsuran = StripKosong
            If SaldoAkhir = 0 Then SaldoAkhir = StripKosong
            DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPPPS, NomorDokumen, NIK, NamaPemegangSaham, TanggalPinjam,
                                    JumlahPiutang, SaldoAwal, JumlahAngsuran, SaldoAkhir, Keterangan, NomorJV)
        Loop

        AksesDatabase_General(Tutup)
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_BukuPembantu.Enabled = False
        btn_LihatJurnal.Enabled = False
        grb_Pembayaran.Visible = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        NomorJV_Terseleksi = 0
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_BukuPembantu_Click(sender As Object, e As EventArgs) Handles btn_BukuPembantu.Click

        frm_BukuPembantuPiutangPemegangSaham.ResetForm()
        frm_BukuPembantuPiutangPemegangSaham.NomorBPPPS = NomorBPPPS_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.lbl_NamaPemegangSaham.Text = NamaPemegangSaham_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.lbl_NIK.Text = NIK_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.lbl_TanggalPinjaman.Text = TanggalPinjam_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.txt_JumlahPinjaman.Text = JumlahPiutang_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.txt_SaldoAwal.Text = SaldoAwal_Terseleksi
        frm_BukuPembantuPiutangPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        Dim win_InputHutangPiutangPemegangSaham As New wpfWin_InputHutangPiutangPemegangSaham
        win_InputHutangPiutangPemegangSaham.ResetForm()
        win_InputHutangPiutangPemegangSaham.HutangPiutang = hp_Piutang
        win_InputHutangPiutangPemegangSaham.FungsiForm = FungsiForm_TAMBAH
        win_InputHutangPiutangPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        'Dim SudahAdaAngsuran As Boolean = False
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_PencairanPiutangPemegangSaham WHERE Nomor_BPPPS = '" & NomorBPPPS_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        'dr_ExecuteReader()
        'dr.Read()
        'If dr.HasRows Then SudahAdaAngsuran = True
        'AksesDatabase_Transaksi(Tutup)

        'If SudahAdaAngsuran = True Then
        '    MsgBox("Sudah ada angsuran. Data tidak dapat diperbarui.")
        '    Return
        'End If

        If NomorJV_Terseleksi > 0 Then
            MsgBox("Data terpilih sudah diposting. Tidak dapat diedit/hapus..!")
            Return
        End If

        Dim win_InputHutangPiutangPemegangSaham As New wpfWin_InputHutangPiutangPemegangSaham
        win_InputHutangPiutangPemegangSaham.ResetForm()
        ProsesIsiValueForm = True
        win_InputHutangPiutangPemegangSaham.HutangPiutang = hp_Piutang
        win_InputHutangPiutangPemegangSaham.FungsiForm = FungsiForm_EDIT
        win_InputHutangPiutangPemegangSaham.NomorID = NomorID_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_NomorBP.Text = NomorBPPPS_Terseleksi
        win_InputHutangPiutangPemegangSaham.NomorJV = NomorJV_Terseleksi
        win_InputHutangPiutangPemegangSaham.dtp_TanggalPinjam.Text = TanggalPinjam_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_KodeLawanTransaksi.Text = NIK_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_NamaLawanTransaksi.Text = NamaPemegangSaham_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_JumlahPinjaman.Text = SaldoAwal_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangPiutangPemegangSaham.txt_Keterangan, Keterangan_Terseleksi)
        ProsesIsiValueForm = False
        win_InputHutangPiutangPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        'Dim SudahAdaAngsuran As Boolean = False
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_PencairanPiutangPemegangSaham WHERE Nomor_BPPPS = '" & NomorBPPPS_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        'dr_ExecuteReader()
        'dr.Read()
        'If dr.HasRows Then SudahAdaAngsuran = True
        'AksesDatabase_Transaksi(Tutup)

        'If SudahAdaAngsuran = True Then
        '    MsgBox("Sudah ada angsuran. Data tidak dapat dihapus.")
        '    Return
        'End If

        If NomorJV_Terseleksi > 0 Then
            MsgBox("Data terpilih sudah diposting. Tidak dapat diedit/hapus..!")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data Piutang :
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangPemegangSaham WHERE Nomor_BPPPS = '" & NomorBPPPS_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        'Hapus Jurnal :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PengeluaranTunai
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PiutangPemegangSaham
        win_InputBuktiPengeluaran.NomorBP = NomorBPPPS_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = NIK_Terseleksi
        win_InputBuktiPengeluaran.TambahkanDataPengeluaranPiutangPemegangSaham()
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        'frm_JurnalVoucher.ResetForm()
        'frm_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        'If NomorJV_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Terseleksi
        'ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembayaran_Terseleksi
        'Else
        '    MsgBox("Data terpilih BELUM masuk JURNAL.")
        '    Return
        'End If
        'frm_JurnalVoucher.ShowDialog()
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
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
        NomorBPPPS_Terseleksi = DataTabelUtama("Nomor_BPPPS", BarisTerseleksi).Value
        NomorDokumen_Terseleksi = DataTabelUtama("Nomor_Dokumen", BarisTerseleksi).Value
        NIK_Terseleksi = DataTabelUtama("NIK_", BarisTerseleksi).Value
        NamaPemegangSaham_Terseleksi = DataTabelUtama("Nama_Pemegang_Saham", BarisTerseleksi).Value
        TanggalPinjam_Terseleksi = DataTabelUtama("Tanggal_Pinjam", BarisTerseleksi).Value
        JumlahPiutang_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Piutang", BarisTerseleksi).Value)
        SaldoAwal_Terseleksi = AmbilAngka(DataTabelUtama("Saldo_Awal", BarisTerseleksi).Value)
        JumlahAngsuran_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Angsuran", BarisTerseleksi).Value)
        SaldoAkhir_Terseleksi = AmbilAngka(DataTabelUtama("Saldo_Akhir", BarisTerseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV", BarisTerseleksi).Value)

        NomorJV_Pembayaran_Terseleksi = 0
        Dim TotalJumlahBayar = AmbilAngka(DataTabelUtama.Item("Jumlah_Angsuran", BarisTerseleksi).Value)
        If Microsoft.VisualBasic.Left(NomorBPPPS_Terseleksi, PanjangTeks_AwalanBPPPS_PlusTahunBuku) = AwalanBPPPS_PlusTahunBuku Then
            TermasukPiutangTahunIni_Terseleksi = True
        Else
            TermasukPiutangTahunIni_Terseleksi = False
        End If

        If NomorBPPPS_Terseleksi <> Kosongan Then
            TampilkanData_Pencairan()
            btn_BukuPembantu.Enabled = True
            If TermasukPiutangTahunIni_Terseleksi = True Then btn_LihatJurnal.Enabled = True
        Else
            BersihkanSeleksi()
        End If

        If NomorJV_Terseleksi > 0 Then
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
            btn_Posting.Enabled = False
            If TermasukPiutangTahunIni_Terseleksi = True Then btn_LihatJurnal.Enabled = True
        Else
            If TermasukPiutangTahunIni_Terseleksi = True Then btn_Posting.Enabled = True
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

        If TermasukPiutangTahunIni_Terseleksi = False Then
            btn_Posting.Enabled = False
            btn_LihatJurnal.Enabled = False
        End If


    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        btn_BukuPembantu_Click(sender, e)
    End Sub


    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If NomorJV_Terseleksi = 0 Then
            MsgBox("Data terpilih belum diposting. Tidak dapat menginput pencairan..!")
            Return
        End If

        If BarisTerseleksi < 0 Then
            MsgBox("Tidak ada baris data terseleksi.")
            Return
        End If

        If SaldoAkhir_Terseleksi <= 0 Then
            MsgBox("Data terpilih sudah LUNAS.")
            Return
        End If

        Dim win_InputBuktiPenerimaan As New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PencairanPiutang
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangPemegangSaham
        win_InputBuktiPenerimaan.NomorBP = NomorBPPPS_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = NIK_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.DialogResult = DialogResult.OK Then TampilkanData()

    End Sub


    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangPiutangPemegangSaham.ResetForm()
        frm_InputPembayaranHutangPiutangPemegangSaham.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangPiutangPemegangSaham.HutangPiutang = hp_Piutang
        frm_InputPembayaranHutangPiutangPemegangSaham.TermasukHutangPiutangTahunIni = TermasukPiutangTahunIni_Terseleksi
        frm_InputPembayaranHutangPiutangPemegangSaham.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangPiutangPemegangSaham.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangPiutangPemegangSaham.Referensi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        IsiValueFormInputPembayaranPiutangPemegangSaham()
        frm_InputPembayaranHutangPiutangPemegangSaham.NomorIdBayar = NomorIdPembayaran_Terseleksi
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe += 1
        Loop
        frm_InputPembayaranHutangPiutangPemegangSaham.txt_JumlahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangPiutangPemegangSaham.txt_SaldoAkhir.Text = SaldoAwal_Terseleksi - HitungBayar
        frm_InputPembayaranHutangPiutangPemegangSaham.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranHutangPiutangPemegangSaham.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangPiutangPemegangSaham.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangPiutangPemegangSaham.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

    End Sub


    Sub IsiValueFormInputPembayaranPiutangPemegangSaham()

        frm_InputPembayaranHutangPiutangPemegangSaham.txt_NomorBP.Text = NomorBPPPS_Terseleksi
        frm_InputPembayaranHutangPiutangPemegangSaham.txt_SaldoAwal.Text = SaldoAwal_Terseleksi
        frm_InputPembayaranHutangPiutangPemegangSaham.NIK = NIK_Terseleksi
        frm_InputPembayaranHutangPiutangPemegangSaham.NamaPemegangSaham = NamaPemegangSaham_Terseleksi

    End Sub


    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PencairanPiutangPemegangSaham :
        cmd = New OdbcCommand(" DELETE FROM tbl_PencairanPiutangPemegangSaham " &
                              " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()


        'Hapus Data di tbl_Transaksi (Jurnal) :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                  " WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If



    End Sub


    Sub TampilkanData_Pencairan()

        dgv_DetailBayar.Rows.Clear()
        grb_Pembayaran.Visible = True
        'grb_InfoSaldo.Visible = False

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP      = '" & NomorBPPPS_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
        Loop
        AksesDatabase_Transaksi(Tutup)
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

    End Sub


    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
        btn_BukuPembantu.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Pembayaran_Terseleksi = 0
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorJV_Terseleksi = 0
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        Referensi_Terseleksi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        If BarisBayar_Terseleksi >= 0 Then
            btn_BukuPembantu.Enabled = True
            btn_LihatJurnal.Enabled = True
            btn_EditPembayaran.Enabled = True
            btn_HapusPembayaran.Enabled = True
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
    End Sub
    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub


End Class