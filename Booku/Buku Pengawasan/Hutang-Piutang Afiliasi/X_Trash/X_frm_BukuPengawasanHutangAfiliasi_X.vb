Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanHutangAfiliasi_X

    Dim JudulForm

    Dim Baris_Terseleksi
    Dim jadwal_BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPHA
    Dim KodeKreditur
    Dim NamaKreditur
    Dim TanggalPinjam
    Dim TanggalJatuhTempo
    Dim NomorKontrak
    Dim SaldoAwal
    Dim JumlahBayar
    Dim SaldoAkhir
    Dim Keterangan
    Dim NomorJV

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPHA_Terseleksi
    Dim KodeKreditur_Terseleksi
    Dim NamaKreditur_Terseleksi
    Dim LembagaKeuangan_Terseleksi
    Dim TanggalPinjam_Terseleksi
    Dim TanggalJatuhTempo_Terseleksi
    Dim NomorKontrak_Terseleksi
    Dim SaldoAwal_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SaldoAkhir_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim jadwal_NomorUrut
    Dim jadwal_Ceklis As Boolean
    Dim jadwal_NomorID
    Dim jadwal_TanggalJatuhTempo
    Dim jadwal_TanggalBayar
    Dim jadwal_Pokok
    Dim jadwal_BagiHasil
    Dim jadwal_PPhDitanggung
    Dim jadwal_PPhDipotong
    Dim jadwal_Jumlah
    Dim jadwal_SaldoAkhir
    Dim jadwal_NomorJV

    Dim jadwal_SudahLengkap As Boolean
    Dim jadwal_SaldoAkhirBarisAkhir

    Dim jadwal_NomorUrut_Terseleksi
    Dim jadwal_NomorID_Terseleksi
    Dim jadwal_TanggalJatuhTempo_Terseleksi
    Dim jadwal_TanggalBayar_Terseleksi
    Dim jadwal_Pokok_Terseleksi
    Dim jadwal_BagiHasil_Terseleksi
    Dim jadwal_PPhDitanggung_Terseleksi
    Dim jadwal_PPhDipotong_Terseleksi
    Dim jadwal_Jumlah_Terseleksi
    Dim jadwal_SaldoAkhir_Terseleksi
    Dim jadwal_NomorJV_Terseleksi
    Dim jadwal_Ceklis_Terseleksi As Boolean

    Dim JumlahBaris_TabelUtama
    Dim JumlahBaris_TabelJadwalAngsuran
    Dim JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar
    Dim TermasukHutangTahunIni_Terseleksi

    Dim JumlahBarisYangAkanDiakses
    Dim AngsuranKe_Array() As Integer
    Dim AngsuranKe_Index
    Dim AngsuranKe As String

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            grb_Pembayaran.Visible = False
            btn_LihatJurnal.Visible = False
            btn_LihatJurnalJadwal.Visible = False
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            grb_Pembayaran.Visible = True
            btn_LihatJurnal.Visible = True
            btn_LihatJurnalJadwal.Visible = True
        End If

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub RefreshTampilanData_JadwalAngsuran()
        TampilkanData_JadwalAngsuran()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        Dim Index_BarisTabel = 0
        NomorUrut = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangAfiliasi " &
                              " ORDER BY Tanggal_Transaksi ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPHA = dr.Item("Nomor_BPHA")
            KodeKreditur = dr.Item("Kode_Lawan_Transaksi")
            NamaKreditur = dr.Item("Nama_Lawan_Transaksi")
            TanggalPinjam = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            TanggalJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            If TanggalJatuhTempo = TanggalKosong Then TanggalJatuhTempo = StripKosong
            NomorKontrak = dr.Item("Nomor_Kontrak")
            SaldoAwal = dr.Item("Saldo_Awal")

            'Data Pembayaran :
            JumlahBayar = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                       " WHERE Nomor_BPHA = '" & NomorBPHA & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Dim NomorJVBayar
            Do While drBAYAR.Read
                NomorJVBayar = drBAYAR.Item("Nomor_JV")
                If NomorJVBayar > 0 Then JumlahBayar += drBAYAR.Item("Pokok")
            Loop

            SaldoAkhir = SaldoAwal - JumlahBayar
            Keterangan = dr.Item("Keterangan")
            NomorJV = dr.Item("Nomor_JV")

            If JumlahBayar = 0 Then JumlahBayar = StripKosong
            If SaldoAkhir = 0 Then SaldoAkhir = StripKosong

            DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHA, KodeKreditur, NamaKreditur,
                                    TanggalPinjam, TanggalJatuhTempo, NomorKontrak,
                                    SaldoAwal, JumlahBayar, SaldoAkhir, Keterangan, NomorJV)

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                If TanggalPinjam = StripKosong Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
            End If

            Index_BarisTabel += 1

        Loop

        AksesDatabase_Transaksi(Tutup)

        JumlahBaris_TabelUtama = DataTabelUtama.RowCount

        BersihkanSeleksi()

    End Sub

    Sub TampilkanData_JadwalAngsuran()

        grb_JadwalAngsuranHutang.Enabled = True
        grb_Pembayaran.Enabled = True
        grb_JadwalAngsuranHutang.Text = "Jadwal Angsuran Hutang Afiliasi - " & NamaKreditur_Terseleksi & " :"
        dgv_JadwalAngsuran.Visible = True

        'Style Tabel :
        dgv_JadwalAngsuran.Rows.Clear()
        StyleTabelUtama(dgv_JadwalAngsuran)

        'Data Tabel :
        Dim Index_BarisTabel = 0
        jadwal_NomorUrut = 0
        JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar = 0
        Dim jadwal_SaldoAkhirSebelumnya = SaldoAwal_Terseleksi

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                              " WHERE Nomor_BPHA = '" & NomorBPHA_Terseleksi & "' " &
                              " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            jadwal_NomorUrut = dr.Item("Angsuran_Ke")
            jadwal_Ceklis = False
            jadwal_NomorID = dr.Item("Nomor_ID")
            jadwal_TanggalJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            jadwal_TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            If jadwal_TanggalBayar <> TanggalKosong Then
                JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar += 1
            Else
                jadwal_TanggalBayar = StripKosong
            End If
            jadwal_Pokok = dr.Item("Pokok")
            jadwal_BagiHasil = dr.Item("Bagi_Hasil")
            jadwal_PPhDitanggung = dr.Item("PPh_Ditanggung")
            jadwal_PPhDipotong = dr.Item("PPh_Dipotong")
            jadwal_Jumlah = jadwal_Pokok + jadwal_BagiHasil - jadwal_PPhDipotong
            jadwal_SaldoAkhir = jadwal_SaldoAkhirSebelumnya - jadwal_Pokok
            jadwal_NomorJV = dr.Item("Nomor_JV")

            If jadwal_Pokok = 0 Then jadwal_Pokok = StripKosong
            If jadwal_BagiHasil = 0 Then jadwal_BagiHasil = StripKosong
            If jadwal_PPhDitanggung = 0 Then jadwal_PPhDitanggung = StripKosong
            If jadwal_PPhDipotong = 0 Then jadwal_PPhDipotong = StripKosong
            If jadwal_SaldoAkhir = 0 Then jadwal_SaldoAkhir = StripKosong

            dgv_JadwalAngsuran.Rows.Add(jadwal_NomorUrut, jadwal_Ceklis, jadwal_NomorID, jadwal_TanggalJatuhTempo, jadwal_TanggalBayar,
                                        jadwal_Pokok, jadwal_BagiHasil, jadwal_PPhDitanggung, jadwal_PPhDipotong,
                                        jadwal_Jumlah, jadwal_SaldoAkhir, jadwal_NomorJV)

            jadwal_SaldoAkhirSebelumnya = AmbilAngka(jadwal_SaldoAkhir)

            If jadwal_TanggalBayar = StripKosong Then dgv_JadwalAngsuran.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar

            Index_BarisTabel += 1

        Loop

        AksesDatabase_Transaksi(Tutup)

        JumlahBaris_TabelJadwalAngsuran = dgv_JadwalAngsuran.RowCount

        jadwal_SaldoAkhirBarisAkhir = AmbilAngka(jadwal_SaldoAkhir)

        If JumlahBaris_TabelJadwalAngsuran > 0 Then
            If jadwal_SaldoAkhirBarisAkhir <= 0 Then jadwal_SudahLengkap = True
            If jadwal_SaldoAkhirBarisAkhir > 0 Then jadwal_SudahLengkap = False
        Else
            jadwal_SudahLengkap = False
        End If

        BersihkanSeleksi_JadwalAngsuran()

        If jadwal_SaldoAkhirBarisAkhir < 0 Then
            BeginInvoke(Sub() MsgBox("Saldo Akhir Jadwal Angsuran pada hutang ini kurang dari 0 (nol)." & Enter2Baris &
                                     "Silakan perbaiki 'Jadwal Angsuran'..!"))
        End If

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Posting.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_EditHutang.Enabled = False
        btn_HapusHutang.Enabled = False
        grb_JadwalAngsuranHutang.Enabled = False
        grb_JadwalAngsuranHutang.Text = "Jadwal Angsuran Hutang :"
        dgv_JadwalAngsuran.Rows.Clear()
        dgv_JadwalAngsuran.Visible = False
        grb_Pembayaran.Enabled = False
    End Sub

    Sub BersihkanSeleksi_JadwalAngsuran()
        jadwal_BarisTerseleksi = -1
        dgv_JadwalAngsuran.ClearSelection()
        btn_LihatJurnalJadwal.Enabled = False
        btn_EditJadwal.Enabled = False
        btn_HapusJadwal.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
    End Sub

    Sub BersihkanSeluruhCeklis()
        For Each row As DataGridViewRow In dgv_JadwalAngsuran.Rows
            row.Cells("Jadwal_Ceklis_").Value = False
        Next
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_RefreshJadwal_Click(sender As Object, e As EventArgs) Handles btn_RefreshJadwal.Click
        RefreshTampilanData_JadwalAngsuran()
    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click

        Dim win_InputBuktiPenerimaan As New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PenerimaanTunai
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_HutangAfiliasi
        win_InputBuktiPenerimaan.NomorBP = NomorBPHA_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
        win_InputBuktiPenerimaan.TambahkanDataPenerimaanHutangAfiliasi()
        win_InputBuktiPenerimaan.ShowDialog()

    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_LihatJurnalJadwal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnalJadwal.Click
        LihatJurnal(jadwal_NomorJV_Terseleksi)
    End Sub

    Private Sub btn_InputHutang_Click(sender As Object, e As EventArgs) Handles btn_InputHutang.Click

        Dim win_InputHutangPiutangAfiliasi As New wpfWin_InputHutangPiutangAfiliasi
        win_InputHutangPiutangAfiliasi.ResetForm()
        win_InputHutangPiutangAfiliasi.HutangPiutang = hp_Hutang
        win_InputHutangPiutangAfiliasi.FungsiForm = FungsiForm_TAMBAH
        win_InputHutangPiutangAfiliasi.ShowDialog()


    End Sub

    Private Sub btn_EditHutang_Click(sender As Object, e As EventArgs) Handles btn_EditHutang.Click

        If NomorJV_Terseleksi > 0 Then
            MsgBox("Data terpilih sudah diposting. Tidak dapat diedit/hapus..!")
            Return
        End If

        Dim win_InputHutangPiutangAfiliasi As New wpfWin_InputHutangPiutangAfiliasi
        win_InputHutangPiutangAfiliasi.ResetForm()
        ProsesIsiValueForm = True
        win_InputHutangPiutangAfiliasi.HutangPiutang = hp_Hutang
        win_InputHutangPiutangAfiliasi.FungsiForm = FungsiForm_EDIT
        win_InputHutangPiutangAfiliasi.NomorID = NomorID_Terseleksi
        win_InputHutangPiutangAfiliasi.txt_NomorBP.Text = NomorBPHA_Terseleksi
        win_InputHutangPiutangAfiliasi.NomorJV = NomorJV_Terseleksi
        win_InputHutangPiutangAfiliasi.dtp_TanggalPinjam.Text = TanggalPinjam_Terseleksi
        win_InputHutangPiutangAfiliasi.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
        win_InputHutangPiutangAfiliasi.txt_NamaLawanTransaksi.Text = NamaKreditur_Terseleksi
        win_InputHutangPiutangAfiliasi.dtp_TanggalJatuhTempo.Text = TanggalJatuhTempo_Terseleksi
        win_InputHutangPiutangAfiliasi.txt_NomorKontrak.Text = NomorKontrak_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangPiutangAfiliasi.txt_Keterangan, Keterangan_Terseleksi)
        ProsesIsiValueForm = False
        win_InputHutangPiutangAfiliasi.ShowDialog()

    End Sub

    Private Sub btn_HapusHutang_Click(sender As Object, e As EventArgs) Handles btn_HapusHutang.Click

        If NomorJV_Terseleksi > 0 Then
            MsgBox("Data terpilih sudah diposting. Tidak dapat diedit/hapus..!")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangAfiliasi " &
                              " WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                              " WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", Baris_Terseleksi).Value)
        NomorBPHA_Terseleksi = DataTabelUtama.Item("Nomor_BPHA", Baris_Terseleksi).Value
        KodeKreditur_Terseleksi = DataTabelUtama.Item("Kode_Kreditur", Baris_Terseleksi).Value
        NamaKreditur_Terseleksi = DataTabelUtama.Item("Nama_Kreditur", Baris_Terseleksi).Value
        TanggalPinjam_Terseleksi = DataTabelUtama.Item("Tanggal_Pinjam", Baris_Terseleksi).Value
        TanggalJatuhTempo_Terseleksi = DataTabelUtama.Item("Jatuh_Tempo", Baris_Terseleksi).Value
        If TanggalJatuhTempo_Terseleksi = StripKosong Then TanggalJatuhTempo_Terseleksi = TanggalKosong
        NomorKontrak_Terseleksi = DataTabelUtama.Item("Nomor_Kontrak", Baris_Terseleksi).Value
        SaldoAwal_Terseleksi = AmbilAngka(DataTabelUtama.Item("Saldo_Awal", Baris_Terseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        SaldoAkhir_Terseleksi = AmbilAngka(DataTabelUtama.Item("Saldo_Akhir", Baris_Terseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeKreditur_Terseleksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            LembagaKeuangan_Terseleksi = dr.Item("Keuangan")
        End If
        AksesDatabase_General(Tutup)

        If KodeKreditur_Terseleksi <> Kosongan Then
            TampilkanData_JadwalAngsuran()
            If NomorJV_Terseleksi > 0 Then
                btn_Posting.Enabled = False
                btn_LihatJurnal.Enabled = True
            Else
                btn_Posting.Enabled = True
                btn_LihatJurnal.Enabled = False
            End If
            If JumlahBayar_Terseleksi > 0 Then
                btn_EditHutang.Enabled = False
                btn_HapusHutang.Enabled = False
            Else
                btn_EditHutang.Enabled = True
                If JumlahBaris_TabelJadwalAngsuran > 0 Then
                    btn_HapusHutang.Enabled = False
                Else
                    btn_HapusHutang.Enabled = True
                End If
            End If
        Else
            BersihkanSeleksi()
        End If

        If AmbilTahun_DariTanggal(TanggalPinjam_Terseleksi) = TahunBukuAktif Then TermasukHutangTahunIni_Terseleksi = True
        If AmbilTahun_DariTanggal(TanggalPinjam_Terseleksi) <> TahunBukuAktif Then TermasukHutangTahunIni_Terseleksi = False

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_EditHutang_Click(sender, e)
    End Sub

    Private Sub btn_InputJadwal_Click(sender As Object, e As EventArgs) Handles btn_InputJadwal.Click

        If NomorJV_Terseleksi = 0 Then
            MsgBox("Data terpilih belum diposting. Tidak dapat menginput jadwal..!")
            Return
        End If

        BersihkanSeluruhCeklis()

        If jadwal_SudahLengkap = True Then
            MsgBox("Jadwal sudah lengkap..!" & Enter2Baris & "Anda sudah tidak dapat menginput 'Jadwal Angsuran' pada hutang ini.")
            Return
        End If

        X_frm_InputJadwalAngsuranAfiliasi.ResetForm()
        X_frm_InputJadwalAngsuranAfiliasi.HutangPiutang = hp_Hutang
        X_frm_InputJadwalAngsuranAfiliasi.FungsiForm = FungsiForm_TAMBAH
        X_frm_InputJadwalAngsuranAfiliasi.NomorBP = NomorBPHA_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.KodeLawanTransaksi = KodeKreditur_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.ShowDialog()

    End Sub

    Private Sub btn_EditJadwal_Click(sender As Object, e As EventArgs) Handles btn_EditJadwal.Click

        BersihkanSeluruhCeklis()

        If btn_EditJadwal.Enabled = False Then Return

        X_frm_InputJadwalAngsuranAfiliasi.ResetForm()
        X_frm_InputJadwalAngsuranAfiliasi.HutangPiutang = hp_Hutang
        X_frm_InputJadwalAngsuranAfiliasi.FungsiForm = FungsiForm_EDIT
        X_frm_InputJadwalAngsuranAfiliasi.NomorID = jadwal_NomorID_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.NomorBP = NomorBPHA_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.NomorJV = jadwal_NomorJV_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.KodeLawanTransaksi = KodeKreditur_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.txt_AngsuranKe.Text = jadwal_NomorUrut_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.dtp_TanggalJatuhTempo.Text = jadwal_TanggalJatuhTempo_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.txt_Pokok.Text = jadwal_Pokok_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.txt_BagiHasil.Text = jadwal_BagiHasil_Terseleksi
        X_frm_InputJadwalAngsuranAfiliasi.ShowDialog()

    End Sub

    Private Sub btn_HapusJadwal_Click(sender As Object, e As EventArgs) Handles btn_HapusJadwal.Click

        BersihkanSeluruhCeklis()

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_JadwalAngsuranHutangAfiliasi " &
                              " WHERE Nomor_ID = '" & jadwal_NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData_JadwalAngsuran()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If



    End Sub


    Private Sub dgv_JadwalAngsuran_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_JadwalAngsuran.CellContentClick
    End Sub
    Private Sub dgv_JadwalAngsuran_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_JadwalAngsuran.ColumnHeaderMouseClick
        BersihkanSeleksi_JadwalAngsuran()
    End Sub
    Private Sub dgv_JadwalAngsuran_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_JadwalAngsuran.CellClick

        If dgv_JadwalAngsuran.RowCount = 0 Then Return
        jadwal_BarisTerseleksi = dgv_JadwalAngsuran.CurrentRow.Index
        jadwal_Ceklis_Terseleksi = dgv_JadwalAngsuran.Item("Jadwal_Ceklis_", jadwal_BarisTerseleksi).Value
        jadwal_NomorUrut_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Nomor_Urut", jadwal_BarisTerseleksi).Value)
        jadwal_NomorID_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Nomor_ID", jadwal_BarisTerseleksi).Value)
        jadwal_TanggalJatuhTempo_Terseleksi = dgv_JadwalAngsuran.Item("Jadwal_Jatuh_Tempo", jadwal_BarisTerseleksi).Value
        jadwal_TanggalBayar_Terseleksi = dgv_JadwalAngsuran.Item("Jadwal_Tanggal_Bayar", jadwal_BarisTerseleksi).Value
        jadwal_Pokok_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Pokok_", jadwal_BarisTerseleksi).Value)
        jadwal_BagiHasil_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Bagi_Hasil", jadwal_BarisTerseleksi).Value)
        jadwal_PPhDitanggung_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Ditanggung", jadwal_BarisTerseleksi).Value)
        jadwal_PPhDipotong_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Dipotong", jadwal_BarisTerseleksi).Value)
        jadwal_Jumlah_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Jumlah_", jadwal_BarisTerseleksi).Value)
        jadwal_SaldoAkhir_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Saldo_Akhir", jadwal_BarisTerseleksi).Value)
        jadwal_NomorJV_Terseleksi = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Nomor_JV", jadwal_BarisTerseleksi).Value)

        If jadwal_NomorID_Terseleksi > 0 Then
            If jadwal_TanggalBayar_Terseleksi <> StripKosong Then 'Jika sudah ada pembayaran
                btn_LihatJurnalJadwal.Enabled = True
                btn_EditJadwal.Enabled = False
                btn_HapusJadwal.Enabled = False
                btn_EditPembayaran.Enabled = True
                If JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar = jadwal_BarisTerseleksi + 1 Then
                    btn_HapusPembayaran.Enabled = True
                Else
                    btn_HapusPembayaran.Enabled = False
                End If
            Else
                btn_LihatJurnalJadwal.Enabled = False
                btn_EditJadwal.Enabled = True
                If JumlahBaris_TabelJadwalAngsuran = jadwal_BarisTerseleksi + 1 Then
                    btn_HapusJadwal.Enabled = True
                Else
                    btn_HapusJadwal.Enabled = False
                End If
                btn_EditPembayaran.Enabled = False
                btn_HapusPembayaran.Enabled = False
            End If
        Else
            BersihkanSeleksi_JadwalAngsuran()
        End If

        If dgv_JadwalAngsuran.Columns(e.ColumnIndex).Name = "Jadwal_Ceklis_" Then
            AlgoritmaCeklis()
        End If

    End Sub
    Private Sub dgv_JadwalAngsuran_DoubleClick(sender As Object, e As EventArgs) Handles dgv_JadwalAngsuran.DoubleClick
        If dgv_JadwalAngsuran.RowCount = 0 Or jadwal_BarisTerseleksi < 0 Then Return
        If jadwal_NomorJV_Terseleksi = 0 Then
            btn_EditJadwal_Click(sender, e)
        Else
            btn_LihatJurnalJadwal_Click(sender, e)
        End If
    End Sub
    Sub AlgoritmaCeklis()
        If jadwal_Ceklis_Terseleksi = False Then
            For Each row As DataGridViewRow In dgv_JadwalAngsuran.Rows
                If row.Cells("Jadwal_Tanggal_Bayar").Value <> StripKosong Then row.Cells("Jadwal_Ceklis_").Value = False
                If row.Cells("Jadwal_Tanggal_Bayar").Value = StripKosong Then row.Cells("Jadwal_Ceklis_").Value = True
                If row.Index = jadwal_BarisTerseleksi Then Exit For
            Next
        Else
            For Each row As DataGridViewRow In dgv_JadwalAngsuran.Rows
                If row.Index >= jadwal_BarisTerseleksi Then row.Cells("Jadwal_Ceklis_").Value = False
            Next
        End If
    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        Dim IndexBaris = 0
        Dim JumlahBarisTerceklis = 0
        Dim Ceklis As Boolean = False
        Do While IndexBaris < JumlahBaris_TabelJadwalAngsuran
            Ceklis = dgv_JadwalAngsuran.Item("Jadwal_Ceklis_", IndexBaris).Value
            If Ceklis = True Then JumlahBarisTerceklis += 1
            IndexBaris += 1
        Loop
        If JumlahBarisTerceklis = 0 Then JumlahBarisTerceklis = 1

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangAfiliasi
        win_InputBuktiPengeluaran.JumlahAngsuranTerjadwal = JumlahBarisTerceklis
        win_InputBuktiPengeluaran.NomorBP = NomorBPHA_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then
            BersihkanSeluruhCeklis()
            RefreshSetelahBayar()
        End If

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        AlgoritmaPemilihanBaris()

        If JumlahBarisYangAkanDiakses > 1 Then
            Pilihan = MessageBox.Show("Anda akan mengedit " & JumlahBarisYangAkanDiakses & " baris angsuran yang sudah dibayar, " &
                                      "yaitu angsuran ke : " & AngsuranKe & "." & Enter2Baris &
                                      "Lanjutkan..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then
                BersihkanSeluruhCeklis()
                Return
            End If
        End If

        frm_InputPembayaranAfiliasi.ResetForm()
        frm_InputPembayaranAfiliasi.HutangPiutang = hp_Hutang
        frm_InputPembayaranAfiliasi.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranAfiliasi.dtp_TanggalBayar.Value = jadwal_TanggalBayar_Terseleksi
        frm_InputPembayaranAfiliasi.NomorJVBayar = jadwal_NomorJV_Terseleksi
        IsiValueFormInputAngsuran()
        BersihkanSeluruhCeklis()
        RefreshSetelahBayar()

    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        AlgoritmaPemilihanBaris()

        If JumlahBarisYangAkanDiakses > 1 Then
            Pilihan = MessageBox.Show("Anda akan menghapus " & JumlahBarisYangAkanDiakses & " baris angsuran yang sudah dibayar, " &
                                      "yaitu angsuran ke : " & AngsuranKe & "." & Enter2Baris &
                                      "Lanjutkan..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then
                BersihkanSeluruhCeklis()
                Return
            End If
        End If

        FiturBelumBisaDigunakan()

        PesanUntukProgrammer("Jangan lupa, hapus juga JURNAL-nya...!!!!!!!!!!!!!!!!!!!!")

        BersihkanSeluruhCeklis()
        RefreshSetelahBayar()

    End Sub

    Sub AlgoritmaPemilihanBaris()

        Array.Resize(AngsuranKe_Array, JumlahBaris_TabelJadwalAngsuran)

        BersihkanSeluruhCeklis()

        AngsuranKe_Index = 0
        JumlahBarisYangAkanDiakses = 0
        For Each row As DataGridViewRow In dgv_JadwalAngsuran.Rows
            If row.Cells("Jadwal_Nomor_JV").Value = jadwal_NomorJV_Terseleksi Then
                AngsuranKe_Array(AngsuranKe_Index) = row.Cells("Jadwal_Nomor_Urut").Value
                row.Cells("Jadwal_Ceklis_").Value = True
                JumlahBarisYangAkanDiakses += 1
                AngsuranKe_Index += 1
            End If
        Next

        Array.Resize(AngsuranKe_Array, JumlahBarisYangAkanDiakses)

        AngsuranKe_Index = 0 'Ini jangan dihapus...! Karena memang harus di-nol-kan (reset) lagi.
        AngsuranKe = Kosongan
        Do While AngsuranKe_Index < AngsuranKe_Array.Length
            If AngsuranKe = Kosongan Then
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
            Else
                AngsuranKe = AngsuranKe & ", " & AngsuranKe_Array(AngsuranKe_Index)
            End If
            AngsuranKe_Index += 1
        Loop

    End Sub

    Sub IsiValueFormInputAngsuran()

        Array.Resize(AngsuranKe_Array, JumlahBaris_TabelJadwalAngsuran)
        frm_InputPembayaranAfiliasi.txt_NomorBP.Text = NomorBPHA_Terseleksi
        frm_InputPembayaranAfiliasi.NomorKontrak = NomorKontrak_Terseleksi
        frm_InputPembayaranAfiliasi.KodeKreditur = KodeKreditur_Terseleksi
        frm_InputPembayaranAfiliasi.NamaKreditur = NamaKreditur_Terseleksi
        frm_InputPembayaranAfiliasi.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        Dim Pokok As Int64 = 0
        Dim BagiHasil As Int64 = 0
        Dim PPhDitanggung As Int64 = 0
        Dim PPhDipotong As Int64 = 0
        Dim IndexBaris = 0
        Dim Ceklis As Boolean = False
        Dim JumlahBarisTerceklis = 0
        Do While IndexBaris < JumlahBaris_TabelJadwalAngsuran
            Ceklis = dgv_JadwalAngsuran.Item("Jadwal_Ceklis_", IndexBaris).Value
            If Ceklis = True Then
                AngsuranKe_Array(JumlahBarisTerceklis) = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Nomor_Urut", IndexBaris).Value)
                Pokok += AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Pokok_", IndexBaris).Value)
                BagiHasil += AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Bagi_Hasil", IndexBaris).Value)
                PPhDitanggung += AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Ditanggung", IndexBaris).Value)
                PPhDipotong += AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Dipotong", IndexBaris).Value)
                JumlahBarisTerceklis += 1
            End If
            IndexBaris += 1
        Loop
        If JumlahBarisTerceklis = 0 Then
            jadwal_BarisTerseleksi = 0
            For Each row As DataGridViewRow In dgv_JadwalAngsuran.Rows
                If row.Cells("Jadwal_Tanggal_Bayar").Value = StripKosong Then Exit For
                jadwal_BarisTerseleksi += 1
            Next
            If jadwal_BarisTerseleksi >= JumlahBaris_TabelJadwalAngsuran Then
                If JumlahBaris_TabelJadwalAngsuran = 0 Then
                    MsgBox("Silakan input terlebih dahulu 'Jadwal Angsuran'..!")
                Else
                    MsgBox("'Jadwal Angsuran' sudah dibayarkan semua...!")
                End If
                Return
            End If
            JumlahBarisTerceklis = 1
            AngsuranKe_Array(0) _
                = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Nomor_Urut", jadwal_BarisTerseleksi).Value)
            Pokok = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Pokok_", jadwal_BarisTerseleksi).Value)
            BagiHasil = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_Bagi_Hasil", jadwal_BarisTerseleksi).Value)
            PPhDitanggung = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Ditanggung", jadwal_BarisTerseleksi).Value)
            PPhDipotong = AmbilAngka(dgv_JadwalAngsuran.Item("Jadwal_PPh_Dipotong", jadwal_BarisTerseleksi).Value)
        End If
        frm_InputPembayaranAfiliasi.JumlahBarisAngsuran = JumlahBarisTerceklis
        Array.Resize(AngsuranKe_Array, JumlahBarisTerceklis)
        Array.Resize(frm_InputPembayaranAfiliasi.AngsuranKe_Array, JumlahBarisTerceklis)
        AngsuranKe_Array.CopyTo(frm_InputPembayaranAfiliasi.AngsuranKe_Array, 0)
        frm_InputPembayaranAfiliasi.txt_Pokok.Text = Pokok
        frm_InputPembayaranAfiliasi.txt_BagiHasil.Text = BagiHasil
        frm_InputPembayaranAfiliasi.txt_PPhDitanggung.Text = PPhDitanggung
        frm_InputPembayaranAfiliasi.txt_PPhDipotong.Text = PPhDipotong
        frm_InputPembayaranAfiliasi.JumlahPPh = PPhDitanggung + PPhDipotong
        frm_InputPembayaranAfiliasi.ShowDialog()

    End Sub

    Sub RefreshSetelahBayar()
        If frm_InputTransaksi.PenyimpananSukses = True Then
            TampilkanData_JadwalAngsuran()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal21 Then frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal22 Then frm_BukuPengawasanHutangPPhPasal22.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal23 Then frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal42 Then frm_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal25 Then frm_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal26 Then frm_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal29 Then frm_BukuPengawasanHutangPPhPasal29.RefreshTampilanData()
        End If
    End Sub

End Class