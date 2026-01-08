Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanHutangPPhPasal25_X

    Public JudulForm
    Public JenisPajak

    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim Bulan_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayarPajak_Terseleksi
    Dim SisaHutangPajak_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Hutang_Terseleksi

    Dim SisaHutang_SaatCutOff

    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim TahunPajakSebelumIni

    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim NomorID
    Dim NomorBulan
    Dim NomorBPHP
    Dim Bulan = Nothing
    Dim PPhPasal25
    Dim RekapPerBulan_PPhPasal25
    Dim JumlahTagihan
    Dim TotalTagihan_PPhPasal25
    Dim TotalBayar_PPhPasal25
    Dim TotalSisaHutang_PPhPasal25
    Dim TanggalTransaksi
    Dim JumlahBayar_PPhPasal25
    Dim SisaHutang_PPhPasal25
    Dim Keterangan
    Dim NomorJV_Hutang

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        JenisPajak = JenisPajak_PPhPasal25

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_LihatJurnal.Visible = False
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.Visible = True
            grb_InfoSaldo.Text = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False

        TahunPajak = TahunBukuAktif
        KontenCombo_TahunPajak()

        TampilkanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        KontenCombo_TahunPajak() 'Sengaja pakai Sub KontenCombo, untuk me-refresh List Tahun Pajak, barangkali ada update data untuk Tahun Pajak Terlama
    End Sub


    Sub KontenCombo_TahunPajak()

        TahunHutangPajakTerlama = AmbilTahunTerlama_BerdasarkanKolomTanggal(TahunCutOff, "tbl_HutangPPhPasal25", "Tanggal_Transaksi")
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.Text = TahunPajak

    End Sub

    Sub TampilkanData()

        btn_DetailPembayaran.Enabled = False

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorBulan = 0
        TotalTagihan_PPhPasal25 = 0
        TotalBayar_PPhPasal25 = 0
        TotalSisaHutang_PPhPasal25 = 0

        Do While AmbilAngka(NomorBulan) < 12

            NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
            NomorBulan = AmbilAngka(NomorBulan) + 1
            Bulan = BulanTerbilang(NomorBulan)
            NomorBPHP = AwalanBPHP25 & TahunPajak & "-" & NomorBulan.ToString
            TanggalTransaksi = Nothing
            RekapPerBulan_PPhPasal25 = 0
            JumlahBayar_PPhPasal25 = 0
            SisaHutang_PPhPasal25 = 0
            Keterangan = Nothing
            NomorJV_Hutang = 0

            NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak

            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

            cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPPhPasal25 " &
                                  " WHERE DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                  KoneksiDatabaseTransaksi_Alternatif)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                NomorID = dr.Item("Nomor_ID")
                TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                RekapPerBulan_PPhPasal25 = dr.Item("Jumlah_Tagihan")
                Keterangan = dr.Item("Keterangan")
                NomorJV_Hutang = dr.Item("Nomor_JV")
            End If

            TutupDatabaseTransaksi_Alternatif()

            'Data Pembayaran :
            If AmbilAngka(RekapPerBulan_PPhPasal25) > 0 Then
                Dim TahunTelusurPembayaran = TahunPajak
                Dim PencegahLoopingTahunPajakLampau = 0
                Do While TahunTelusurPembayaran <= TahunBukuAktif
                    If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
                    If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
                    If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                        cmd = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                              " WHERE Nomor_BP      = '" & NomorBPHP & "' " &
                                              " AND Status_Invoice  = '" & Status_Dibayar & "' ",
                                              KoneksiDatabaseTransaksi_Alternatif)
                        dr_ExecuteReader()
                        Do While dr.Read
                            JumlahBayar_PPhPasal25 += dr.Item("Jumlah_Bayar")
                            If JumlahBayar_PPhPasal25 >= RekapPerBulan_PPhPasal25 Then Exit Do
                        Loop
                        TutupDatabaseTransaksi_Alternatif()
                    End If
                    If JumlahBayar_PPhPasal25 >= RekapPerBulan_PPhPasal25 Then Exit Do
                    PencegahLoopingTahunPajakLampau += 1
                    TahunTelusurPembayaran += 1
                Loop
            End If

            SisaHutang_PPhPasal25 = AmbilAngka(RekapPerBulan_PPhPasal25) - AmbilAngka(JumlahBayar_PPhPasal25)
            TotalTagihan_PPhPasal25 = AmbilAngka(TotalTagihan_PPhPasal25) + AmbilAngka(RekapPerBulan_PPhPasal25)
            TotalBayar_PPhPasal25 = AmbilAngka(TotalBayar_PPhPasal25) + AmbilAngka(JumlahBayar_PPhPasal25)
            TotalSisaHutang_PPhPasal25 = AmbilAngka(TotalSisaHutang_PPhPasal25) + AmbilAngka(SisaHutang_PPhPasal25)

            If AmbilAngka(RekapPerBulan_PPhPasal25) = 0 Then RekapPerBulan_PPhPasal25 = StripKosong
            If AmbilAngka(JumlahBayar_PPhPasal25) = 0 Then JumlahBayar_PPhPasal25 = StripKosong
            If AmbilAngka(SisaHutang_PPhPasal25) = 0 Then SisaHutang_PPhPasal25 = StripKosong

            DataTabelUtama.Rows.Add(AmbilAngka(NomorBulan), NomorID, NomorBPHP, Bulan, TanggalTransaksi,
                                    RekapPerBulan_PPhPasal25, JumlahBayar_PPhPasal25, SisaHutang_PPhPasal25, Keterangan, NomorJV_Hutang)

        Loop

        Baris_KetetapanPajak()

        'Baris TOTAL untuk Jenis Tampilan REKAP :
        If AmbilAngka(TotalTagihan_PPhPasal25) = 0 Then TotalTagihan_PPhPasal25 = StripKosong
        If AmbilAngka(TotalBayar_PPhPasal25) = 0 Then TotalBayar_PPhPasal25 = StripKosong
        If AmbilAngka(TotalSisaHutang_PPhPasal25) = 0 Then TotalSisaHutang_PPhPasal25 = StripKosong
        DataTabelUtama.Rows.Add()
        DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, "T O T A L", Nothing,
                                TotalTagihan_PPhPasal25, TotalBayar_PPhPasal25, TotalSisaHutang_PPhPasal25, Nothing, Nothing)

        AksesDatabase_Transaksi(Buka)

        'Hitung Total Tagihan Selama Sebelum Cut Off :
        Dim TotalTagihan_SelamaSebelumCUtOff = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPPhPasal25 ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalTagihan_SelamaSebelumCUtOff += dr.Item("Jumlah_Tagihan")
        Loop

        'Hitung Total Pembayaran Selama Sebelum Cut Off :
        Dim TotalBayar_SelamaSebelumCutOff = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                                  " AND Status_Invoice = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalBayar_SelamaSebelumCutOff += dr.Item("Jumlah_Bayar")
        Loop

        AksesDatabase_Transaksi(Tutup)

        'Hitung Saldo Akhir Saat Cut Off :
        SisaHutang_SaatCutOff = TotalTagihan_SelamaSebelumCUtOff - TotalBayar_SelamaSebelumCutOff

        'Tampilkan Grup/Panel Info Saldo :
        TampilkanInfoSaldo()


        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = SisaHutang_SaatCutOff
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                AmbilValue_SaldoAwalBerdasarkanList()
                AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                CekKesesuaianSaldoAwal()
                txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
        End Select

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        grb_Pembayaran.Visible = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_DetailPembayaran.Enabled = True
        TampilkanInfoSaldo()
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        If TahunPajak = TahunBukuAktif Then
            Dim JumlahTagihan_SA = 0
            Dim JumlahBayar_SA = 0
            Dim Tahun_SA = TahunBukuAktif - 1
            Dim TahunTelusur = TahunCutOff
            Do While TahunTelusur <= TahunBukuKemarin
                TahunBuku_Alternatif = TahunTelusur
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPPhPasal25 ", KoneksiDatabaseTransaksi_Alternatif)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Tagihan")
                Loop
                cmdBAYAR = New OdbcCommand(" SELECT * FROM X_tbl_PembayaranHutangPajak_X " &
                                           " WHERE Jenis_Pajak = '" & JenisPajak & "' ",
                                           KoneksiDatabaseTransaksi_Alternatif)
                drBAYAR_ExecuteReader()
                Do While drBAYAR.Read
                    JumlahBayar_SA += drBAYAR.Item("Jumlah_Bayar")
                Loop
                TutupDatabaseTransaksi_Alternatif()
                TahunTelusur += 1
            Loop
            SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        Dim JumlahPenyesuaian_DEBET = 0
        Dim JumlahPenyesuaian_KREDIT = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal25 & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal25 & "' " &
                              " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            If dr.HasRows Then
                JumlahPenyesuaian_DEBET += dr.Item("Jumlah_Debet")
                JumlahPenyesuaian_KREDIT += dr.Item("Jumlah_Kredit")
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        JumlahPenyesuaianSaldo = JumlahPenyesuaian_KREDIT - JumlahPenyesuaian_DEBET
        SaldoAwal_BerdasarkanCOA_PlusPenyesuaian = SaldoAwal_BerdasarkanCOA + JumlahPenyesuaianSaldo
        txt_AJP.Text = JumlahPenyesuaianSaldo
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangPPhPasal25 & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAkhir_BerdasarkanCOA = dr.Item("Saldo_Awal")
        AksesDatabase_Transaksi(Tutup)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
    End Sub

    Sub Baris_KetetapanPajak()

        'Dim JenisPajak_YangDitelusuri = JenisPajak
        'Dim NomorBPHP_KetetapanPajak = Kosongan
        'Dim JumlahTagihan_KetetapanPajak
        'Dim JumlahBayar_KetetapanPajak
        'Dim SisaHutang_KetetapanPajak

        'BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak " &
        '                      " WHERE Jenis_Pajak = '" & JenisPajak_YangDitelusuri & "' ",
        '                      KoneksiDatabaseTransaksi_Alternatif)
        'dr_ExecuteReader()
        'JumlahTagihan_KetetapanPajak = 0
        'Do While dr.Read
        '    NomorBPHP_KetetapanPajak = dr.Item("Nomor_BPHP")
        '    JumlahTagihan_KetetapanPajak += dr.Item("Pokok_Pajak")
        'Loop

        ''Data Pembayaran Pokok Pajak :
        'JumlahBayar_KetetapanPajak = 0
        'cmdBAYAR = New OdbcCommand(" SELECT * FROM X_tbl_PembayaranHutangPajak_X " &
        '                           " WHERE Nomor_BPHP LIKE '%" & AwalanBPKP & "%' " &
        '                           " AND Jenis_Pajak = '" & JenisPajak_YangDitelusuri & "' ",
        '                           KoneksiDatabaseTransaksi_Alternatif)
        'drBAYAR_ExecuteReader()
        'Do While drBAYAR.Read
        '    JumlahBayar_KetetapanPajak += drBAYAR.Item("Jumlah_Bayar")
        'Loop

        'TutupDatabaseTransaksi_Alternatif()

        'SisaHutang_KetetapanPajak = JumlahTagihan_KetetapanPajak - JumlahBayar_KetetapanPajak

        'If JumlahTagihan_KetetapanPajak = 0 Then JumlahTagihan_KetetapanPajak = StripKosong
        'If JumlahBayar_KetetapanPajak = 0 Then JumlahBayar_KetetapanPajak = StripKosong
        'If SisaHutang_KetetapanPajak = 0 Then SisaHutang_KetetapanPajak = StripKosong

        'DataTabelUtama.Rows.Add()
        'DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, JenisPajak_KetetapanPajak, Nothing,
        '                        JumlahTagihan_KetetapanPajak, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak, Nothing, Nothing)

        'TotalTagihan_PPhPasal25 += AmbilAngka(JumlahTagihan_KetetapanPajak)
        'TotalBayar_PPhPasal25 += AmbilAngka(JumlahBayar_KetetapanPajak)
        'TotalSisaHutang_PPhPasal25 += AmbilAngka(SisaHutang_KetetapanPajak)

    End Sub

    Sub CekKesesuaianSaldoAwal()
        If SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian Then
            KesesuaianSaldoAwal = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        If SaldoAkhir_BerdasarkanList = SaldoAkhir_BerdasarkanCOA Then
            KesesuaianSaldoAkhir = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub

    Private Sub cmb_TahunPajak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.SelectedIndexChanged
    End Sub
    Private Sub cmb_TahunPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_TahunPajak_TextChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.TextChanged

        TahunPajak = cmb_TahunPajak.Text
        TahunPajakSebelumIni = TahunPajak - 1

        If TahunPajak > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
        End If

        If TahunPajak = TahunBukuAktif Then
            TahunPajakSamaDenganTahunBukuAktif = True
            pnl_CRUD.Visible = True
        Else
            TahunPajakSamaDenganTahunBukuAktif = False
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then pnl_CRUD.Visible = True
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then pnl_CRUD.Visible = False
        End If

        If ProsesLoadingForm = False Then TampilkanData()

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Input_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        frm_InputHutangPPhPasal25.ResetForm()
        frm_InputHutangPPhPasal25.FungsiForm = FungsiForm_TAMBAH
        frm_InputHutangPPhPasal25.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputHutangPPhPasal25.FungsiForm = FungsiForm_EDIT
        frm_InputHutangPPhPasal25.ResetForm()
        frm_InputHutangPPhPasal25.NomorId = NomorID_Terseleksi
        frm_InputHutangPPhPasal25.cmb_Tahun.Text = TahunPajak
        frm_InputHutangPPhPasal25.cmb_MasaPajak.Text = Bulan_Terseleksi
        frm_InputHutangPPhPasal25.txt_JumlahTerutang.Text = JumlahTagihan_Terseleksi
        frm_InputHutangPPhPasal25.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputHutangPPhPasal25.NomorJVHutang = NomorJV_Hutang_Terseleksi
        frm_InputHutangPPhPasal25.ShowDialog()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data Hutang di Tabel tbl_HutangPPhPasal25 :
        cmd = New OdbcCommand(" DELETE FROM tbl_HutangPPhPasal25 " &
                              " WHERE Nomor_ID = '" & NomorID_Terseleksi & "'",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        'Hapus Data di Jurnal / tbl_Transaksi :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Hutang_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            MsgBox("Data Terpilih BERHASIL Dihapus.")
        Else
            MsgBox("Data Terpilih GAGAL Dihapus!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If SisaHutangPajak_Terseleksi <= 0 Then
            MsgBox("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran_Non
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, Kosongan, "Pembayaran " & JenisPajak & " " & Bulan_Terseleksi, NomorBPHP_Terseleksi,
                                JumlahTagihan_Terseleksi, 0, 0, 0, JumlahBayarPajak_Terseleksi, SisaHutangPajak_Terseleksi,
                                JenisPajak, KodeSetoran_Non, 0, 0, 0,
                                SisaHutangPajak_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangPajak.ResetForm()
        frm_InputPembayaranHutangPajak.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangPajak.JenisPajak = JenisPajak
        frm_InputPembayaranHutangPajak.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangPajak.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangPajak.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NomorIdBayar = NomorIdPembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NPPHP = Referensi_Terseleksi
        IsiValueForm_InputPembayaran()
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe = BarisKe + 1
        Loop
        frm_InputPembayaranHutangPajak.txt_JumlahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = JumlahTagihan_Terseleksi - HitungBayar
        frm_InputPembayaranHutangPajak.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranHutangPajak.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangPajak.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangPajak.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

    End Sub

    Sub IsiValueForm_InputPembayaran()
        frm_InputPembayaranHutangPajak.txt_NomorBPHP.Text = NomorBPHP_Terseleksi
        frm_InputPembayaranHutangPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        frm_InputPembayaranHutangPajak.txt_JumlahTerutang.Text = JumlahTagihan_Terseleksi
        frm_InputPembayaranHutangPajak.KodeSetoran = Kosongan
    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranHutangPajak :
        If StatusKoneksiDatabaseTransaksi = True Then
            cmd = New OdbcCommand(" DELETE FROM X_tbl_PembayaranHutangPajak_X " &
                                  " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        'Hapus Data di tbl_PengajuanPembayaranHutangPajak :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangPajak " &
                                  " WHERE Nomor_Pengajuan = '" & Referensi_Terseleksi & "' " &
                                  " AND Nomor_BPHP = '" & NomorBPHP_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

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
            pesan_DataTerpilihBerhasilDihapus()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As EventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak
        frm_DetailPembayaranPajak.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_HapusPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        Referensi_Terseleksi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_HapusPembayaran.Enabled = True
            btn_EditPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        Else
            btn_HapusPembayaran.Enabled = False
            btn_EditPembayaran.Enabled = False
            btn_LihatJurnal.Enabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    Sub TampilkanInfoSaldo()
        If TahunPajak = TahunBukuAktif Then
            grb_InfoSaldo.Visible = True
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_SaldoAwalBerdasarkanCOA.Visible = True
                txt_SaldoAwalBerdasarkanCOA.Visible = True
                lbl_AJP.Visible = True
                txt_AJP.Visible = True
            Else
                lbl_SaldoAwalBerdasarkanCOA.Visible = False
                txt_SaldoAwalBerdasarkanCOA.Visible = False
                lbl_AJP.Visible = False
                txt_AJP.Visible = False
            End If
        Else
            grb_InfoSaldo.Visible = False
            lbl_SaldoAwalBerdasarkanCOA.Visible = False
            txt_SaldoAwalBerdasarkanCOA.Visible = False
            lbl_AJP.Visible = False
            txt_AJP.Visible = False
        End If
    End Sub

    Sub TampilkanDataPembayaran()

        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False

        dgv_DetailBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar = 0
        Dim TotalBayar = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP      = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Kode_Setoran    = '" & KodeSetoran_Non & "' " &
                                      " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                                      " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJV_Pembayaran = dr.Item("Nomor_JV")
                    Else
                        NomorJV_Pembayaran = 0
                    End If
                    dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJV_Pembayaran)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
            PencegahLoopingTahunPajakLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        dgv_DetailBayar.ClearSelection()
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

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
        Bulan_Terseleksi = DataTabelUtama.Item("Bulan_", Baris_Terseleksi).Value
        NomorBPHP_Terseleksi = DataTabelUtama.Item("Nomor_BPHP", Baris_Terseleksi).Value
        TanggalTransaksi_Terseleksi = DataTabelUtama.Item("Tanggal_Transaksi", Baris_Terseleksi).Value
        JumlahTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Tagihan", Baris_Terseleksi).Value)
        JumlahBayarPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Pajak", Baris_Terseleksi).Value)
        SisaHutangPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang_Pajak", Baris_Terseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value
        NomorJV_Hutang_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value)

        If NomorUrut_Terseleksi > 0 And JumlahTagihan_Terseleksi > 0 Then
            TampilkanDataPembayaran()
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            grb_Pembayaran.Visible = False
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
            'Jangan Pakai Sub : BersihkanSeleksi()...!!!
        End If

    End Sub

    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        If NomorID_Terseleksi = 0 And NomorUrut_Terseleksi > 0 Then btn_Input_Click(sender, e)
        If NomorID_Terseleksi > 0 And NomorUrut_Terseleksi > 0 Then btn_Edit_Click(sender, e)
        If Bulan_Terseleksi = JenisPajak_KetetapanPajak Then
            frm_BukuPengawasanKetetapanPajak.MdiParent = frm_BOOKU
            frm_BukuPengawasanKetetapanPajak.Show()
            frm_BukuPengawasanKetetapanPajak.Focus()
            usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak
        End If
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanList)
    End Sub
    Private Sub txt_SaldoBerdasarkanList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanList.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub
    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As EventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox(txt_SelisihSaldo)
    End Sub
    Private Sub txt_SelisihSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SelisihSaldo.KeyPress
        KunciTotalInputan(sender, e)
    End Sub



    Private Sub btn_Sesuaikan_Click(sender As Object, e As EventArgs) Handles btn_Sesuaikan.Click

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            Dim NamaAkun_BiayaSelisihPencatatan
            Dim NamaAkun_HutangPPhPasal25
            Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
            PengisianValue_NamaAkun()
            NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
            KodeAkun_Tembak = KodeTautanCOA_HutangPPhPasal25
            PengisianValue_NamaAkun()
            NamaAkun_HutangPPhPasal25 = NamaAkun_Tembak
            frm_InputJurnal.ResetForm()
            frm_InputJurnal.JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
            frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            If JumlahPenyesuaian > 0 Then
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangPPhPasal25,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal25, "K", Nothing, JumlahPenyesuaian)
            End If
            If JumlahPenyesuaian < 0 Then
                JumlahPenyesuaian = -(JumlahPenyesuaian)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_HutangPPhPasal25,
                                                           NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           PenjorokNamaAkun & NamaAkun_HutangPPhPasal25, "K", Nothing, JumlahPenyesuaian)
            End If
            frm_InputJurnal.DataTabelUtama.Item("Debet", 3).Value = JumlahPenyesuaian
            frm_InputJurnal.DataTabelUtama.Item("Kredit", 3).Value = JumlahPenyesuaian
            frm_InputJurnal.lbl_StatusBalance.ForeColor = Color.Green
            frm_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            frm_InputJurnal.dtp_TanggalJurnal.Value = AwalTahunBukuAktif
            frm_InputJurnal.cmb_JenisJurnal.Text = JenisJurnal_AdjusmentSaldoAwal
            BeginInvoke(Sub() frm_InputJurnal.dtp_TanggalJurnal.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.cmb_JenisJurnal.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_TambahTransaksi.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_Reset.Enabled = False)
            BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = True)
            BeginInvoke(Sub() frm_InputJurnal.JumlahBarisJurnal = 2)
            BeginInvoke(Sub() MsgBox("Silakan buat Jurnal Penyesuaian (Adjusment)."))
            frm_InputJurnal.ShowDialog()
            If frm_InputJurnal.JurnalTersimpan = True Then
                UpdateNotifikasi()
                TampilkanData()
            End If
        End If

    End Sub

    Public Sub UpdateNotifikasi()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1, " &
                              " Status_Dieksekusi = 1 " &
                              " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGPPHPASAL25 & "' " &
                              " AND Pesan = '" & teks_SilakanSesuaikanSaldo & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        frm_BOOKU.IsiKontenNotifikasi()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoAwalBerdasarkanCOA)
    End Sub
    Private Sub txt_SaldoAwalBerdasarkanCOA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As EventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox(txt_AJP)
    End Sub

    Private Sub txt_AJP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AJP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        KunciUkuranForm(Me, 1320, 630)
    End Sub

End Class