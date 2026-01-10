Imports bcomm
Imports System.Data.Odbc

Public Class X_BukuPengawasanHutangUsaha_BAK

    Public JalurMasuk
    Public JudulForm
    Public JudulForm_SaldoAkhirHutangUsaha = "Saldo Akhir Hutang Usaha"
    Public JudulForm_SaldoAwalHutangUsaha = "Saldo Awal Hutang Usaha"
    Public JudulForm_BukuPengawasanHutangUsaha = "Buku Pengawasan Hutang Usaha"
    Dim QueryTampilan As String
    Dim QueryTampilanHutangTahunLalu As String
    Dim QueryTampilanHutangTahunAktif As String
    Dim BarisTerseleksi As Integer
    Dim NomorJV_Pembelian_Terseleksi
    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim NPPHU_Terseleksi
    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim NomorPembelian_Terseleksi As String
    Dim NomorBPHU_Terseleksi As String
    Dim JumlahHutangUsaha_Terseleksi
    Dim JumlahPPhDipotong_Terseleksi

    Dim clm_Pilih As CheckBox = Nothing
    Dim StatusProses
    Dim NomorBPHU
    Dim NomorPembelian
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim KodeSupplier
    Dim NamaSupplier
    Dim TanggalFakturPajak
    Dim NomorFakturPajak
    Dim DPP
    Dim PPN
    Dim NamaBarang1, NamaBarang2
    Dim NamaBarang
    Dim NamaJasa1, NamaJasa2, NamaJasa3
    Dim NamaJasa
    Dim PPhDipotong
    Dim JumlahTagihan
    Dim JumlahHutangUsaha
    Dim DueDate
    Dim JumlahBayar_TahunLalu
    Dim JumlahBayar_TahunBukuAktif
    Dim JumlahBayar
    Dim SisaHutangUsaha
    Dim Keterangan
    Dim NomorJV
    Dim StatusJurnal

    Dim TambahRecord As Boolean
    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim BarisIndex

    Dim Total_SisaHutangUsaha
    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean

    Private Sub frm_BukuPengawasanHutangUsaha_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        'User Level Manager dan Direktur tidak dapat mengakses tombol AJUKAN PEMBAYARAN dan kolom PILIH
        If LevelUserAktif = LevelUser_01_Operator Then
            btn_Pembayaran.Visible = True
            DataTabelUtama.Columns("Pilih_").Visible = True
        Else
            btn_Pembayaran.Visible = False
            DataTabelUtama.Columns("Pilih_").Visible = False
        End If

        If SistemApprovalPerusahaan = True Then
            btn_Pembayaran.Text = tombol_AJUKANPEMBAYARAN
            btn_Pembayaran.Visible = True
        Else
            btn_Pembayaran.Visible = True
            btn_Pembayaran.Text = tombol_PEMBAYARAN
            btn_Pembayaran.Visible = False
        End If

        If JalurMasuk = Halaman_BUKUBESAR Then
            Me.Visible = False
        Else
            Me.Visible = True
        End If

        If JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA Then
            'frm_LembarPengajuanPembayaranHutangUsaha.Close()
            BeginInvoke(Sub() MsgBox("Silakan pilih/ceklis beberapa data di tabel BUKU PENGAWASAN HUTANG USAHA yang akan diajukan pembayaran. Kemudian klik '" & tombol_AJUKANPEMBAYARAN & "'."))
            JalurMasuk = Halaman_MENUUTAMA
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = JudulForm_SaldoAkhirHutangUsaha
            btn_LihatJurnal.Visible = False
            btn_InputHutangUsaha.Visible = True
            DataTabelUtama.Columns("Status_Proses").Visible = False
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
            lbl_SaldoAwalBerdasarkanCOA.Visible = False
            txt_SaldoAwalBerdasarkanCOA.Visible = False
            lbl_AJP.Visible = False
            txt_AJP.Visible = False
            btn_SaldoAwalHutangUsaha.Visible = False
            btn_DPHU.Visible = False
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = JudulForm_BukuPengawasanHutangUsaha
            btn_LihatJurnal.Visible = True
            btn_InputHutangUsaha.Visible = False
            DataTabelUtama.Columns("Status_Proses").Visible = True
            grb_InfoSaldo.Text = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
            lbl_SaldoAwalBerdasarkanCOA.Visible = True
            txt_SaldoAwalBerdasarkanCOA.Visible = True
            lbl_AJP.Visible = True
            txt_AJP.Visible = True
            btn_SaldoAwalHutangUsaha.Visible = True
            btn_DPHU.Visible = True
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        BeginInvoke(Sub() BersihkanSeleksi())

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        KontenComboStatusProses()
    End Sub

    Public Sub TampilkanData()

        SaldoAwal_BerdasarkanList = 0
        Total_SisaHutangUsaha = 0

        'Query Tampilan :
        QueryTampilanHutangTahunLalu =
            " SELECT * FROM tbl_Pembelian " &
            " WHERE Jenis_Pembelian = 'TEMPO' AND (Tanggal_Invoice < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " &
            " ORDER BY Tanggal_Invoice "

        QueryTampilanHutangTahunAktif =
            " SELECT * FROM tbl_Pembelian " &
            " WHERE Jenis_Pembelian = 'TEMPO' AND (Tanggal_Invoice >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " &
            " ORDER BY Tanggal_Invoice "


        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)
        dgv_DetailBayar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        BarisIndex = 0

        AksesDatabase_Transaksi(Buka)

        '---------------------------------------------------------------
        'Data Tabel Sisa Hutang Usaha Tahun Lalu :
        '---------------------------------------------------------------
        QueryTampilan = QueryTampilanHutangTahunLalu
        DataTabel()

        '---------------------------------------------------------------
        'Data Tabel BPHU Tahun Buku Aktif :
        '---------------------------------------------------------------
        If JudulForm <> JudulForm_SaldoAwalHutangUsaha Then
            QueryTampilan = QueryTampilanHutangTahunAktif
            DataTabel()
        End If

        AksesDatabase_Transaksi(Tutup)

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = Total_SisaHutangUsaha
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
                AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                CekKesesuaianSaldoAwal()
                txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
        End Select

        BersihkanSeleksi()
        JumlahBaris = DataTabelUtama.RowCount
        txt_TotalHutangUsaha.Text = Total_SisaHutangUsaha

    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        BarisTerseleksi = -1
        dgv_DetailBayar.Rows.Clear()
        grb_Pembayaran.Visible = False
        grb_DetailBayar.Visible = False
        grb_InfoSaldo.Visible = True
        btn_LihatJurnal.Enabled = False
        btn_Pembayaran.Enabled = False
        NomorJV_Pembayaran_Terseleksi = 0
    End Sub

    Sub DataTabel()

        NomorBPHU = Nothing
        NomorPembelian = Nothing
        TanggalInvoice = Nothing
        NomorInvoice = Nothing
        KodeSupplier = Nothing
        NamaSupplier = Nothing
        TanggalFakturPajak = Nothing
        NomorFakturPajak = Nothing
        DPP = Nothing
        PPN = Nothing
        NamaBarang = Nothing
        NamaJasa = Nothing
        PPhDipotong = Nothing
        JumlahTagihan = Nothing
        DueDate = Nothing
        JumlahBayar_TahunBukuAktif = 0
        SisaHutangUsaha = Nothing
        StatusProses = Nothing
        Keterangan = Nothing
        NomorJV = 0
        StatusJurnal = 0
        TambahRecord = False
        JumlahTagihan = 0
        JumlahBayar_TahunBukuAktif = 0
        SisaHutangUsaha = 0

        Dim cmdBAYAR As OdbcCommand
        Dim drBAYAR As OdbcDataReader

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read
            NomorPembelian = dr.Item("Nomor_Pembelian")
            NomorBPHU = AwalanBPHU & Microsoft.VisualBasic.Mid(NomorPembelian, PanjangTeks_AwalanBPHU_Plus1)
            TanggalInvoice = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Invoice"), 10)
            NomorInvoice = dr.Item("Nomor_Invoice")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            TanggalFakturPajak = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Faktur_Pajak"), 10)
            If TanggalFakturPajak = TanggalKosong Then
                TanggalFakturPajak = StripKosong
                NomorFakturPajak = StripKosong
            Else
                TanggalFakturPajak = TanggalFakturPajak
                NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            End If
            DPP = dr.Item("DPP")
            PPN = dr.Item("PPN")
            NamaBarang1 = dr.Item("Nama_Barang_1")
            NamaBarang2 = dr.Item("Nama_Barang_2")
            If NamaBarang2 <> Nothing Then
                NamaBarang2 = SlashGanda_Pemisah & NamaBarang2
            End If
            NamaBarang = NamaBarang1 & NamaBarang2
            NamaJasa1 = dr.Item("Nama_Jasa_1")
            NamaJasa2 = dr.Item("Nama_Jasa_2")
            If NamaJasa2 <> Nothing Then
                NamaJasa2 = SlashGanda_Pemisah & NamaJasa2
            End If
            NamaJasa3 = dr.Item("Nama_Jasa_3")
            If NamaJasa3 <> Nothing Then
                NamaJasa3 = SlashGanda_Pemisah & NamaJasa3
            End If
            NamaJasa = NamaJasa1 & NamaJasa2 & NamaJasa3
            PPhDipotong = AmbilAngka(dr.Item("Jumlah_PPh_Dipotong"))
            JumlahTagihan = AmbilAngka(dr.Item("Jumlah_Tagihan"))
            JumlahHutangUsaha = AmbilAngka(dr.Item("Jumlah_Hutang_Usaha"))
            JumlahBayar_TahunLalu = 0
            JumlahBayar_TahunBukuAktif = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                                       " WHERE Nomor_BPHU = '" & NomorBPHU & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR = cmdBAYAR.ExecuteReader
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_Bayar") < TanggalFormatSimpan(AwalTahunBukuAktif) Then JumlahBayar_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                If drBAYAR.Item("Tanggal_Bayar") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then JumlahBayar_TahunBukuAktif += drBAYAR.Item("Jumlah_Bayar")
            Loop
            JumlahBayar = JumlahBayar_TahunLalu + JumlahBayar_TahunBukuAktif
            DueDate = Microsoft.VisualBasic.Left(dr.Item("Due_Date"), 10)
            SisaHutangUsaha = JumlahHutangUsaha - JumlahBayar
            Total_SisaHutangUsaha += SisaHutangUsaha
            If JudulForm = JudulForm_SaldoAwalHutangUsaha Then JumlahBayar = JumlahBayar_TahunLalu
            Keterangan = dr.Item("Keterangan")
            NomorJV = dr.Item("Nomor_JV")
            If QueryTampilan = QueryTampilanHutangTahunLalu Then
                StatusJurnal = 1
                SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanList + JumlahHutangUsaha - JumlahBayar_TahunLalu
            End If
            If QueryTampilan = QueryTampilanHutangTahunAktif Then
                Dim cmdJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                " WHERE Referensi = '" & NomorPembelian & "' ",
                                                KoneksiDatabaseTransaksi)
                Dim drJURNAL = cmdJURNAL.ExecuteReader
                drJURNAL.Read()
                If drJURNAL.HasRows Then
                    StatusJurnal = drJURNAL.Item("Status_Approve")
                Else
                    StatusJurnal = 0
                End If
            End If
            Dim StatusLunas = dr.Item("Status_Lunas")
            If StatusJurnal = 0 Then
                StatusProses = "BELUM DIJURNAL"
            End If
            If StatusJurnal = 1 Then
                If StatusLunas = "LUNAS" Then
                    StatusProses = dr.Item("Status_Lunas")
                Else
                    Dim cmdPROSES = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha " &
                                                    " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                    " ORDER BY Nomor_ID ",
                                                    KoneksiDatabaseTransaksi)
                    Dim drPROSES = cmdPROSES.ExecuteReader
                    StatusProses = "OPEN"
                    Do While drPROSES.Read
                        Dim StatusProses = drPROSES.Item("Status")
                        If StatusProses = "PROSES" Or StatusProses = "CHECKED" Or StatusProses = "DISETUJUI" Then
                            StatusProses = "ON PROCESS"
                        End If
                        If StatusProses = "DIBATALKAN" Or StatusProses = "DITOLAK" Or StatusProses = "DIBAYAR" Then
                            StatusProses = "OPEN"
                        End If
                    Loop
                End If
            End If
            If AmbilAngka(DPP) = 0 Then DPP = StripKosong
            If AmbilAngka(PPN) = 0 Then PPN = StripKosong
            If AmbilAngka(PPhDipotong) = 0 Then PPhDipotong = StripKosong
            If AmbilAngka(JumlahTagihan) = 0 Then JumlahTagihan = StripKosong
            If AmbilAngka(JumlahHutangUsaha) = 0 Then JumlahHutangUsaha = StripKosong
            If AmbilAngka(JumlahBayar) = 0 Then JumlahBayar = StripKosong
            If AmbilAngka(SisaHutangUsaha) = 0 Then SisaHutangUsaha = StripKosong

            TambahBaris()

        Loop

    End Sub

    Sub TambahBaris()
        Dim Filter = cmb_StatusProses.Text
        If Filter = "SEMUA" Then TambahBarisSesuaiFilter()
        If StatusProses = Filter Then
            If Filter = "BELUM DIJURNAL" Then TambahBarisSesuaiFilter()
            If Filter = "OPEN" Then TambahBarisSesuaiFilter()
            If Filter = "ON PROCESS" Then TambahBarisSesuaiFilter()
            If Filter = "LUNAS" Then TambahBarisSesuaiFilter()
        End If
    End Sub

    Sub TambahBarisSesuaiFilter()
        DataTabelUtama.Rows.Add(clm_Pilih, StatusProses, NomorBPHU, NomorPembelian, TanggalInvoice,
              NomorInvoice, KodeSupplier, NamaSupplier, TanggalFakturPajak,
              NomorFakturPajak, DPP, PPN, NamaBarang,
              NamaJasa, PPhDipotong, JumlahTagihan, JumlahHutangUsaha, DueDate,
              JumlahBayar, SisaHutangUsaha, Keterangan, NomorJV, StatusJurnal)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            'Coloring
            If Microsoft.VisualBasic.Left(DataTabelUtama.Item("Nomor_BPHU", BarisIndex).Value, PanjangTeks_AwalanBPHU_PlusTahunBuku) <> AwalanBPHU_PlusTahunBuku Then
                DataTabelUtama.Rows(BarisIndex).DefaultCellStyle.ForeColor = WarnaAlternatif_1
            Else
                DataTabelUtama.Rows(BarisIndex).DefaultCellStyle.ForeColor = WarnaTegas
            End If
            'Logika Baris Yang Bisa Diceklis :
            If DataTabelUtama.Item("Status_Proses", BarisIndex).Value = "OPEN" Then
                DataTabelUtama.Rows(BarisIndex).ReadOnly = False
            Else
                DataTabelUtama.Rows(BarisIndex).ReadOnly = True
            End If
        End If
        BarisIndex += 1
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        Dim JumlahPenyesuaian_DEBET = 0
        Dim JumlahPenyesuaian_KREDIT = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' " &
                              " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            If dr.HasRows Then
                JumlahPenyesuaian_DEBET = JumlahPenyesuaian_DEBET + dr.Item("Jumlah_Debet")
                JumlahPenyesuaian_KREDIT = JumlahPenyesuaian_KREDIT + dr.Item("Jumlah_Kredit")
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
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAkhir_BerdasarkanCOA = dr.Item("Saldo_Awal")
        AksesDatabase_Transaksi(Tutup)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
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

    Sub KontenComboStatusProses()
        cmb_StatusProses.Items.Clear()
        cmb_StatusProses.Items.Add("SEMUA")
        cmb_StatusProses.Items.Add("BELUM DIJURNAL")
        cmb_StatusProses.Items.Add("OPEN")
        If SistemApprovalPerusahaan = True Then cmb_StatusProses.Items.Add("ON PROCESS")
        cmb_StatusProses.Items.Add("LUNAS")
        cmb_StatusProses.Text = "SEMUA"
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_StatusProses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_StatusProses.SelectedIndexChanged
    End Sub
    Private Sub cmb_StatusProses_TextChanged(sender As Object, e As EventArgs) Handles cmb_StatusProses.TextChanged
        TampilkanData()
    End Sub
    Private Sub cmb_StatusProses_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_StatusProses.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorPembelian_Terseleksi = DataTabelUtama.Item("Nomor_Pembelian", BarisTerseleksi).Value
        NomorJV_Pembelian_Terseleksi = DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value
        NomorBPHU_Terseleksi = DataTabelUtama.Item("Nomor_BPHU", BarisTerseleksi).Value
        JumlahHutangUsaha_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Hutang_Usaha", BarisTerseleksi).Value)
        JumlahPPhDipotong_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_PPh_Dipotong", BarisTerseleksi).Value)
        NomorJV_Pembayaran_Terseleksi = 0
        Dim TotalJumlahBayar = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", BarisTerseleksi).Value)
        Dim SisaHutang = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang", BarisTerseleksi).Value)
        If Microsoft.VisualBasic.Left(NomorBPHU_Terseleksi, 10) = AwalanBPHU_PlusTahunBuku Then
            TermasukHutangTahunIni_Terseleksi = True
        Else
            TermasukHutangTahunIni_Terseleksi = False
        End If
        dgv_DetailBayar.Visible = True
        dgv_DetailBayar.Rows.Clear()

        If JudulForm <> JudulForm_SaldoAwalHutangUsaha Then TampilkanData_Pembayaran()

        If TermasukHutangTahunIni_Terseleksi = True Then btn_LihatJurnal.Enabled = True
        If TermasukHutangTahunIni_Terseleksi = False Then btn_LihatJurnal.Enabled = False
        btn_Pembayaran.Enabled = True
        txt_JumlahHutangUsaha.Text = JumlahHutangUsaha_Terseleksi
        txt_TotalBayar.Text = TotalJumlahBayar
        If SisaHutang = 0 Then
            txt_SisaHutang.Text = StripKosong
        Else
            txt_SisaHutang.Text = SisaHutang
        End If

    End Sub

    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Pembelian_Terseleksi = 0
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        NPPHU_Terseleksi = dgv_DetailBayar.Item("NPPHU_", BarisBayar_Terseleksi).Value
        NomorJV_Pembelian_Terseleksi = 0
        If BarisBayar_Terseleksi >= 0 Then
            btn_EditPembayaran.Enabled = True
            btn_HapusPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If NPPHU_Terseleksi = Nothing Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    Private Sub txt_TotalBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalBayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalBayar.TextChanged
        Try
            If txt_TotalBayar.Text.Trim() <> Nothing Then
                txt_TotalBayar.Text = CDec(txt_TotalBayar.Text).ToString("N0")
                txt_TotalBayar.SelectionStart = txt_TotalBayar.TextLength
            End If
        Catch ex As Exception
        End Try
        If AmbilAngka(txt_TotalBayar.Text) <= AmbilAngka(txt_JumlahHutangUsaha.Text) Then
            txt_TotalBayar.ForeColor = WarnaTegas
        Else
            txt_TotalBayar.ForeColor = WarnaPeringatan
        End If
    End Sub

    Private Sub txt_SisaHutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SisaHutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SisaHutang_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaHutang.TextChanged
        PemecahRibuanUntukTextBox(txt_SisaHutang)
        If AmbilAngka(txt_SisaHutang.Text) >= 0 Then
            txt_SisaHutang.ForeColor = WarnaTegas
        Else
            txt_SisaHutang.ForeColor = WarnaPeringatan
        End If
    End Sub

    Private Sub txt_JumlahTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHutangUsaha.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahHutangUsaha)
    End Sub

    Private Sub btn_Pembayaran_Click(sender As Object, e As EventArgs) Handles btn_Pembayaran.Click

        If SistemApprovalPerusahaan = True _
            Or btn_Pembayaran.Text = tombol_AJUKANPEMBAYARAN _
            Then

            Dim Baris = 0
            Dim ID = 0
            Dim JumlahTerceklis = 0

            'Hapus data temporary
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_PP_Temporary ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            Do While Baris < JumlahBaris
                Dim NomorBPHU = DataTabelUtama.Item("Nomor_BPHU", Baris).Value
                Dim NomorPembelian = DataTabelUtama.Item("Nomor_Pembelian", Baris).Value
                Dim KodeSupplier = DataTabelUtama.Item("Kode_Supplier", Baris).Value
                Dim NamaSupplier = DataTabelUtama.Item("Nama_Supplier", Baris).Value
                Dim SisaHutang = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang", Baris).Value)
                Dim TanggalInvoice = DataTabelUtama.Item("Tanggal_Invoice", Baris).Value
                Dim NomorInvoice = DataTabelUtama.Item("Nomor_Invoice", Baris).Value
                Dim Harga = DataTabelUtama.Item("DPP", Baris).Value
                Dim PPN = DataTabelUtama.Item("PPN", Baris).Value
                Dim PPhDipotong = DataTabelUtama.Item("Jumlah_PPh_Dipotong", Baris).Value
                Dim DueDate = DataTabelUtama.Item("Due_Date", Baris).Value
                Dim Ceklis As Boolean = DataTabelUtama.Item("Pilih_", Baris).Value
                If Ceklis = True Then
                    If SisaHutang = 0 Then
                        MsgBox(NomorBPHU & " tidak masuk ke dalam list Pengajuan Pembayaran. Status : LUNAS.")
                    Else
                        ID = ID + 1
                        AksesDatabase_Transaksi(Buka)
                        cmd = New OdbcCommand(" INSERT INTO tbl_PP_Temporary VALUES ( '" &
                                              ID & "', '" &
                                              NomorBPHU & "', '" &
                                              NomorPembelian & "', '" &
                                              KodeSupplier & "', '" &
                                              NamaSupplier & "', '" &
                                              SisaHutang & "', '" &
                                              TanggalInvoice & "', '" &
                                              NomorInvoice & "', '" &
                                              Harga & "', '" &
                                              PPN & "', '" &
                                              PPhDipotong & "', '" &
                                              DueDate & "' ) ",
                                              KoneksiDatabaseTransaksi)
                        cmd.ExecuteNonQuery()
                        AksesDatabase_Transaksi(Tutup)
                        JumlahTerceklis = JumlahTerceklis + 1
                    End If
                End If
                Baris = Baris + 1
            Loop

            If JumlahTerceklis = 0 Then
                MsgBox("Silakan pilih (ceklis) data yang akan dimasukkan ke dalam List Pengajuan Pembayaran.")
                Return
            End If

            'frm_ListPengajuanPembayaranHutangUsaha.ShowDialog()

        End If

        If SistemApprovalPerusahaan = False _
            Or btn_Pembayaran.Text = tombol_PEMBAYARAN _
            Then

        End If

    End Sub

    Public Sub btn_SaldoAwalHutangUsaha_Click(sender As Object, e As EventArgs) Handles btn_SaldoAwalHutangUsaha.Click

        Select Case JudulForm
            Case JudulForm_BukuPengawasanHutangUsaha
                JudulForm = JudulForm_SaldoAwalHutangUsaha
                btn_SaldoAwalHutangUsaha.Text = JudulForm_BukuPengawasanHutangUsaha
                btn_DPHU.Visible = False
                btn_LihatJurnal.Visible = False
                'grb_InfoSaldo.Visible = True
                DataTabelUtama.Columns("Status_Proses").Visible = False
            Case JudulForm_SaldoAwalHutangUsaha
                JudulForm = JudulForm_BukuPengawasanHutangUsaha
                btn_SaldoAwalHutangUsaha.Text = JudulForm_SaldoAwalHutangUsaha
                btn_DPHU.Visible = True
                btn_LihatJurnal.Visible = True
                'grb_InfoSaldo.Visible = False
                DataTabelUtama.Columns("Status_Proses").Visible = True
        End Select

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        KontenComboStatusProses()

    End Sub

    Private Sub txt_TotalHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalHutangUsaha.TextChanged
        PemecahRibuanUntukTextBox(txt_TotalHutangUsaha)
    End Sub
    Private Sub txt_TotalHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_DPHU_Click(sender As Object, e As EventArgs) Handles btn_DPHU.Click
        RefreshTampilanData() 'Ini penting. Untuk me-refersh tampilan data. Khawatir ada perubahan data sebelum event ini.
        frm_DPHU.ShowDialog()
    End Sub

    Private Sub btn_InputHutangUsaha_Click(sender As Object, e As EventArgs) Handles btn_InputHutangUsaha.Click
        frm_InputPembelian.ResetForm()
        frm_InputPembelian.FungsiForm = FungsiForm_TAMBAH
        frm_InputPembelian.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        'frm_JurnalVoucher.ResetForm()
        'frm_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        'If NomorJV_Pembelian_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembelian_Terseleksi
        'ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembayaran_Terseleksi
        'Else
        '    MsgBox("Data terpilih BELUM masuk JURNAL.")
        '    Return
        'End If
        'frm_JurnalVoucher.ShowDialog()
    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If BarisTerseleksi < 0 Then
            MsgBox("Tidak ada baris data terseleksi.")
            Return
        End If

        Dim SisaHutangBarisTerseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang", BarisTerseleksi).Value)

        If SisaHutangBarisTerseleksi <= 0 Then
            MsgBox("Data terpilih sudah LUNAS.")
            Return
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And
            DataTabelUtama.Item("Status_Proses", BarisTerseleksi).Value = "BELUM DIJURNAL" Then
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If

        frm_InputPembayaranHutangUsaha.ResetForm()
        frm_InputPembayaranHutangUsaha.FungsiForm = FungsiForm_TAMBAH
        frm_InputPembayaranHutangUsaha.AlurTransaksi = frm_InputPembayaranHutangUsaha.AlurTransaksi_PembayaranHutangUsaha
        frm_InputPembayaranHutangUsaha.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangUsaha.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (JumlahBarisBayar + 1).ToString
        IsiValueFormInputPembayaranHutangUsaha()
        frm_InputPembayaranHutangUsaha.txt_JumlahTelahDibayar.Text = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", BarisTerseleksi).Value)
        frm_InputPembayaranHutangUsaha.txt_SisaHutang.Text = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang", BarisTerseleksi).Value)
        frm_InputPembayaranHutangUsaha.ShowDialog()

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangUsaha.ResetForm()
        frm_InputPembayaranHutangUsaha.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangUsaha.AlurTransaksi = frm_InputPembayaranHutangUsaha.AlurTransaksi_PembayaranHutangUsaha
        frm_InputPembayaranHutangUsaha.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangUsaha.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangUsaha.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangUsaha.Referensi = dgv_DetailBayar.Item("NPPHU_", BarisBayar_Terseleksi).Value
        IsiValueFormInputPembayaranHutangUsaha()
        frm_InputPembayaranHutangUsaha.NomorIdBayar = NomorIdPembayaran_Terseleksi
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe = BarisKe + 1
        Loop
        frm_InputPembayaranHutangUsaha.txt_JumlahTelahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangUsaha.txt_SisaHutang.Text = JumlahHutangUsaha_Terseleksi - HitungBayar
        frm_InputPembayaranHutangUsaha.txt_JumlahBayarSekarang.Text = NominalBayar
        frm_InputPembayaranHutangUsaha.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangUsaha.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangUsaha.ShowDialog()

    End Sub

    Sub IsiValueFormInputPembayaranHutangUsaha()

        frm_InputPembayaranHutangUsaha.txt_NomorBPHU.Text = DataTabelUtama.Item("Nomor_BPHU", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.txt_NomorPembelian.Text = DataTabelUtama.Item("Nomor_Pembelian", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.TanggalInvoice = DataTabelUtama.Item("Tanggal_Invoice", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.NomorInvoice = DataTabelUtama.Item("Nomor_Invoice", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.KodeMitra = DataTabelUtama.Item("Kode_Supplier", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.NamaMitra = DataTabelUtama.Item("Nama_Supplier", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.TanggalFakturPajak = DataTabelUtama.Item("Tanggal_Faktur_Pajak", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.NomorFakturPajak = DataTabelUtama.Item("Nomor_Faktur_Pajak", BarisTerseleksi).Value
        frm_InputPembayaranHutangUsaha.txt_JumlahTerutang.Text = JumlahHutangUsaha_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPhDipotong_Total = JumlahPPhDipotong_Terseleksi

    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranHutangUsaha :
        cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangUsaha " &
                              " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_PengajuanPembayaranHutangUsaha :
        cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangUsaha " &
                              " WHERE Nomor_Pengajuan = '" & NPPHU_Terseleksi & "' " &
                              " AND Nomor_BPHU = '" & NomorBPHU_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_Transaksi (Jurnal) :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                              " WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        TampilkanData()
        'frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
        'usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
        'Nanti tambahkan Sub Tampilkan data untuk PPh Pasal 4 (2)

        MsgBox("Data terpilih BERHASIL dihapus.")

    End Sub

    Sub TampilkanData_Pembayaran()

        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                              " WHERE Nomor_Pembelian = '" & NomorPembelian_Terseleksi & "' AND Jumlah_Bayar > 0 " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Bayar"), 10)
            Dim NPPHU = dr.Item("NPPHU")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            Dim KeteranganBayar = dr.Item("Keterangan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, NPPHU, JumlahBayar, KeteranganBayar, NomorJVBayar)
        Loop
        AksesDatabase_Transaksi(Tutup)
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

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
            Dim NamaAkun_HutangUsaha
            Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
            PengisianValue_NamaAkun()
            NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
            KodeAkun_Tembak = KodeTautanCOA_HutangUsaha_NonAfiliasi
            PengisianValue_NamaAkun()
            NamaAkun_HutangUsaha = NamaAkun_Tembak
            frm_InputJurnal.ResetForm()
            frm_InputJurnal.JalurMasuk = Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
            frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            If JumlahPenyesuaian > 0 Then
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           NamaAkun_BiayaSelisihPencatatan, dk_D, JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangUsaha_NonAfiliasi,
                                                           PenjorokNamaAkun & NamaAkun_HutangUsaha, dk_K, Nothing, JumlahPenyesuaian)
            End If
            If JumlahPenyesuaian < 0 Then
                JumlahPenyesuaian = -(JumlahPenyesuaian)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_HutangUsaha_NonAfiliasi,
                                                           NamaAkun_BiayaSelisihPencatatan, dk_D, JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           PenjorokNamaAkun & NamaAkun_HutangUsaha, dk_K, Nothing, JumlahPenyesuaian)
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
                UpdateNotifikasi
                TampilkanData()
            End If
        End If

    End Sub

    Public Sub UpdateNotifikasi()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1, " &
                              " Status_Dieksekusi = 1 " &
                              " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI & "' " &
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
        KunciUkuranForm(Me, 1320, 63)
    End Sub

End Class