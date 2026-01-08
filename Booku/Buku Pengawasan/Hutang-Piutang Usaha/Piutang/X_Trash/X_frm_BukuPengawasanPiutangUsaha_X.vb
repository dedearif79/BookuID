Imports System.Data.Odbc
Imports bcomm

Public Class X_frm_BukuPengawasanPiutangUsaha_X

    Public JalurMasuk
    Public JudulForm
    Public JudulForm_SaldoAkhirPiutangUsaha = "Saldo Akhir Piutang Usaha"
    Public JudulForm_SaldoAwalPiutangUsaha = "Saldo Awal Piutang Usaha"
    Public JudulForm_BukuPengawasanPiutangUsaha = "Buku Pengawasan Piutang Usaha"

    Public KesesuaianJurnal As Boolean

    Dim QueryTampilan

    'Variabel Tabel :
    Dim NomorUrut
    Dim NomorBPPU
    Dim NomorPenjualan
    Dim JenisInvoice
    Dim JenisProduk
    Dim AngkaInvoice
    Dim AngkaInvoice_Sebelumnya
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim TanggalInvoice
    Dim MasaJatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeProject
    Dim KodeCustomer
    Dim NamaCustomer
    Dim CustomerSebagaiAfiliasi
    Dim JumlahHarga
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim JenisPPN
    Dim PPN
    Dim TagihanBruto
    Dim Retur
    Dim TagihanNetto
    Dim JumlahPiutangUsaha
    Dim SisaPiutangUsaha
    Dim JenisPPh
    Dim PPhTerutang
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim KeteranganJatuhTempo
    Dim BiayaAdministrasiBank
    Dim JumlahBayar
    Dim SisaTagihan
    Dim Catatan
    Dim NomorJV_Penjualan

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Public AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim MasaJatuhTempo_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim PPhTerutang_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim TagihanBruto_Terseleksi
    Dim Retur_Terseleksi
    Dim TagihanNetto_Terseleksi
    Dim JumlahPiutangUsaha_Terseleksi
    Dim SisaPiutangUsaha_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim BiayaAdministrasiBank_Terseleksi
    Dim Catatan_Terseleksi



    Dim NomorJV_Penjualan_Terseleksi
    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim Referensi_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim NomorPenjualan_Terseleksi As String
    Dim NomorBPPU_Terseleksi As String


    Dim TermasukHutangTahunIni_Terseleksi As Boolean


    Dim InvoiceDenganPO As Boolean

    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Pilihan Filter :
    Dim Pilih_JenisRelasi
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeCustomer
    Dim Pilih_JatuhTempo

    'Variabel Filter :
    Dim FilterJatuhTempo As Boolean
    Dim FilterRelasi As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha"

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub

    Sub KontenCombo_JenisProduk_Induk()
        cmb_JenisProduk_Induk.Items.Clear()
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Semua)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Barang)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Jasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_BarangDanJasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_JasaKonstruksi)
        cmb_JenisProduk_Induk.Text = JenisProduk_Semua
    End Sub

    Sub KontenCombo_JenisRelasi()
        cmb_JenisRelasi.Items.Clear()
        cmb_JenisRelasi.Items.Add(Pilihan_Semua)
        cmb_JenisRelasi.Items.Add(JenisRelasi_Afiliasi)
        cmb_JenisRelasi.Items.Add(JenisRelasi_NonAfiliasi)
        cmb_JenisRelasi.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_Customer()
        cmb_Customer.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        cmb_Customer.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaCustomer = dr.Item("Nama_Mitra")
            cmb_Customer.Items.Add(NamaCustomer)
        Loop
        cmb_Customer.Text = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub

    Sub KontenCombo_JatuhTempo()
        cmb_JatuhTempo.Items.Clear()
        cmb_JatuhTempo.Items.Add(JatuhTempo_Semua)
        cmb_JatuhTempo.Items.Add(JatuhTempo_Belum)
        cmb_JatuhTempo.Items.Add(JatuhTempo_JT)
        cmb_JatuhTempo.Text = JatuhTempo_Semua
    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisRelasi()
        KontenCombo_JenisProduk_Induk()
        KontenCombo_Customer()
        KontenCombo_JatuhTempo()
        EksekusiKode = True
        TampilkanData()
    End Sub

    Sub TampilkanData()

        If ProsesLoadingForm = True Or EksekusiKode = False Then Return
        If ProsesLoadingForm = True Then Return
        If ProsesResetForm = True Then Return
        If EksekusiKode = False Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.Text <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'Filter Customer :
        Dim FilterCustomer = " "
        If cmb_Customer.Text <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterCustomer

        'Query Tampilan :
        QueryTampilan =
            " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Nomor_JV > 0 AND Jenis_Penjualan = '" & JenisPenjualan_Tempo & "' " & FilterData &
            " ORDER BY Angka_Invoice "

        'Data Tabel :
        NomorUrut = 0
        AngkaInvoice_Sebelumnya = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            NomorPO = Kosongan
            TanggalPO = Kosongan
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorBPPU = AwalanBPPU_PlusTahunBuku & AngkaInvoice
            NomorPenjualan = AwalanPENJ_PlusTahunBuku & AngkaInvoice
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            MasaJatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If MasaJatuhTempo > 0 Then
                MasaJatuhTempo &= " hari"
            Else
                MasaJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice WHERE " &
                                         " Angka_Invoice = '" & AngkaInvoice & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                KodeProject = Kosongan
                NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
                'Surat Jalan : ---------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                              " WHERE Nomor_SJ = '" & NomorSJBAST_Satuan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then
                    If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                        If NomorSJBAST = Kosongan Then
                            NomorSJBAST = NomorSJBAST_Satuan
                            TanggalSJBAST = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project")
                        End If
                    End If
                End If
                'BAST : ------------------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                              " WHERE Nomor_BAST = '" & NomorSJBAST_Satuan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then
                    If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                        If NomorSJBAST = Kosongan Then
                            NomorSJBAST = NomorSJBAST_Satuan
                            TanggalSJBAST = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project")
                        End If
                    End If
                End If
                NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
            Loop
            If NomorSJBAST = Kosongan Then
                InvoiceDenganPO = False
            Else
                InvoiceDenganPO = True
            End If
            If InvoiceDenganPO = False Then KodeProject = dr.Item("Kode_Project_Produk")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            CustomerSebagaiAfiliasi = mdl_PublicSub.CustomerSebagaiAfiliasi(KodeCustomer)
            JenisPPN = dr.Item("Jenis_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonRp = dr.Item("Diskon")
            If DiskonRp = 0 Then DiskonRp = StripKosong
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPN = dr.Item("PPN")
            If PPN = 0 Then PPN = StripKosong
            TagihanBruto = dr.Item("Total_Tagihan")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            If Retur = 0 Then Retur = StripKosong
            TagihanNetto = AmbilAngka(TagihanBruto) - AmbilAngka(Retur)
            JumlahPiutangUsaha = AmbilAngka(DasarPengenaanPajak) + AmbilAngka(PPN) - AmbilAngka(Retur)
            Dim TanggalJatuhTempo_Date As Date
            Dim TanggalInvoice_Date As New Date(Microsoft.VisualBasic.Right(TanggalInvoice, 4),
                    Microsoft.VisualBasic.Mid(TanggalInvoice, 4, 2),
                    Microsoft.VisualBasic.Left(TanggalInvoice, 2))
            If Microsoft.VisualBasic.Right(MasaJatuhTempo, 2) = "ri" Then
                TanggalJatuhTempo_Date = TanggalInvoice_Date.AddDays(AmbilAngka(MasaJatuhTempo))
            Else
                TanggalJatuhTempo_Date = New Date(Microsoft.VisualBasic.Right(MasaJatuhTempo, 4),
                    Microsoft.VisualBasic.Mid(MasaJatuhTempo, 4, 2),
                    Microsoft.VisualBasic.Left(MasaJatuhTempo, 2))
            End If
            If TanggalJatuhTempo_Date >= Today And SisaPiutangUsaha > 0 Then
                KeteranganJatuhTempo = JatuhTempo_Belum
            Else
                KeteranganJatuhTempo = JatuhTempo_JT
            End If
            JumlahBayar = 0
            BiayaAdministrasiBank = 0
            Dim HutangPPh = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPPU & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
                BiayaAdministrasiBank += drBAYAR.Item("Biaya_Administrasi_Bank")
                HutangPPh += drBAYAR.Item("PPh_Dipotong")
            Loop
            'SisaPiutangUsaha = AmbilAngka(JumlahPiutangUsaha) - AmbilAngka(JumlahBayar) - AmbilAngka(BiayaAdministrasiBank) - HutangPPh
            SisaPiutangUsaha = AmbilAngka(JumlahPiutangUsaha) - AmbilAngka(JumlahBayar) - HutangPPh
            JenisPPh = dr.Item("Jenis_PPh")
            PPhTerutang = dr.Item("PPh_Terutang")
            PPhDipotong = dr.Item("PPh_Dipotong")
            If PPhDipotong = 0 Then PPhDipotong = StripKosong
            PPhDitanggung = dr.Item("PPh_Ditanggung")
            SisaTagihan = AmbilAngka(TagihanNetto) - AmbilAngka(JumlahBayar)
            Catatan = dr.Item("Catatan")
            NomorJV_Penjualan = dr.Item("Nomor_JV")
            If AngkaInvoice <> AngkaInvoice_Sebelumnya Then
                'Filter Relasi :
                FilterRelasi = False
                Select Case Pilih_JenisRelasi
                    Case Pilihan_Semua
                        FilterRelasi = True
                    Case JenisRelasi_Afiliasi
                        If CustomerSebagaiAfiliasi Then FilterRelasi = True
                    Case JenisRelasi_NonAfiliasi
                        If Not CustomerSebagaiAfiliasi Then FilterRelasi = True
                End Select
                'Filter Jatuh Tempo :
                FilterJatuhTempo = False
                If Pilih_JatuhTempo = JatuhTempo_Semua Then
                    FilterJatuhTempo = True
                Else
                    If Pilih_JatuhTempo = KeteranganJatuhTempo Then FilterJatuhTempo = True
                End If
                If FilterRelasi = True And FilterJatuhTempo = True Then TambahBaris()
            End If

            AngkaInvoice_Sebelumnya = AngkaInvoice

        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        If SisaPiutangUsaha <= 0 Then KeteranganJatuhTempo = Kosongan
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, NomorBPPU, NomorPenjualan, JenisInvoice, JenisProduk,
                                AngkaInvoice, NomorInvoice, NomorFakturPajak, TanggalInvoice, MasaJatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, KodeCustomer, NamaCustomer,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, JenisPPN, PPN, TagihanBruto, Retur,
                                JumlahPiutangUsaha, SisaPiutangUsaha, JenisPPh, PPhTerutang, PPhDitanggung, PPhDipotong, TagihanNetto,
                                KeteranganJatuhTempo, JumlahBayar, SisaTagihan, BiayaAdministrasiBank, Catatan, NomorJV_Penjualan)
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        grb_Pencairan.Visible = False
        btn_LihatJurnal.Enabled = False
        'Untuk Disable Tombol2 Tertentu
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_JenisRelasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisRelasi.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisRelasi_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisRelasi.TextChanged
        Pilih_JenisRelasi = cmb_JenisRelasi.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JenisRelasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisRelasi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_JenisProduk_Induk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisProduk_Induk_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.TextChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JenisProduk_Induk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisProduk_Induk.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_Customer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Customer.SelectedIndexChanged
    End Sub
    Private Sub cmb_Customer_TextChanged(sender As Object, e As EventArgs) Handles cmb_Customer.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Customer.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeCustomer = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Customer.Text = Pilihan_Semua Then Pilih_KodeCustomer = Pilihan_Semua
        TampilkanData()
    End Sub
    Private Sub cmb_Customer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Customer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_JatuhTempo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.SelectedIndexChanged
    End Sub
    Private Sub cmb_JatuhTempo_TextChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.TextChanged
        Pilih_JatuhTempo = cmb_JatuhTempo.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JatuhTempo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JatuhTempo.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click

        'Dim AdaJurnalRetur As Boolean = False

        'frm_JurnalVoucher.ResetForm()
        'frm_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        'If NomorJV_Penjualan_Terseleksi > 0 Then
        '    If Retur_Terseleksi > 0 Then
        '        AdaJurnalRetur = True
        '    Else
        '        AdaJurnalRetur = False
        '        frm_JurnalVoucher.Angka_NomorJV = NomorJV_Penjualan_Terseleksi
        '        frm_JurnalVoucher.ShowDialog()
        '    End If
        'ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembayaran_Terseleksi
        '    frm_JurnalVoucher.ShowDialog()
        'Else
        '    MsgBox("Data terpilih BELUM masuk JURNAL.")
        '    Return
        'End If

        'If AdaJurnalRetur = True Then
        '    AksesDatabase_Transaksi(Buka)
        '    cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Retur " &
        '                          " WHERE Nomor_Invoice_Produk = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        '    dr_ExecuteReader()
        '    dr.Read()
        '    Dim NomorJV_Retur = dr.Item("Nomor_JV")
        '    AksesDatabase_Transaksi(Tutup)
        '    frm_PilihJurnal_Penjualan_Retur.ResetForm()
        '    frm_PilihJurnal_Penjualan_Retur.NomorJV_Penjualan = NomorJV_Penjualan_Terseleksi
        '    frm_PilihJurnal_Penjualan_Retur.NomorJV_Retur = NomorJV_Retur
        '    frm_PilihJurnal_Penjualan_Retur.ShowDialog()
        'End If
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

        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorBPPU_Terseleksi = DataTabelUtama.Item("Nomor_BPPU", Baris_Terseleksi).Value
        NomorPenjualan_Terseleksi = DataTabelUtama.Item("Nomor_Penjualan", Baris_Terseleksi).Value
        JenisInvoice_Terseleksi = DataTabelUtama.Item("Jenis_Invoice", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        AngkaInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Invoice", Baris_Terseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        MasaJatuhTempo_Terseleksi = DataTabelUtama.Item("Masa_Jatuh_Tempo", Baris_Terseleksi).Value
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", Baris_Terseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", Baris_Terseleksi).Value
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Harga", Baris_Terseleksi).Value)
        DiskonRp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Rp", Baris_Terseleksi).Value)
        DasarPengenaanPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Dasar_Pengenaan_Pajak", Baris_Terseleksi).Value)
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", Baris_Terseleksi).Value
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        PPhTerutang_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Terutang", Baris_Terseleksi).Value)
        PPhDipotong_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Dipotong", Baris_Terseleksi).Value)
        TagihanBruto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Bruto", Baris_Terseleksi).Value)
        Retur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_", Baris_Terseleksi).Value)
        TagihanNetto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Netto", Baris_Terseleksi).Value)
        JumlahPiutangUsaha_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Piutang_Usaha", Baris_Terseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        SisaPiutangUsaha_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Piutang_Usaha", Baris_Terseleksi).Value)
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Penjualan_Terseleksi = DataTabelUtama.Item("Nomor_JV_Penjualan", Baris_Terseleksi).Value

        NomorJV_Pembayaran_Terseleksi = 0
        Dim TotalJumlahBayar = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        If Microsoft.VisualBasic.Left(NomorBPPU_Terseleksi, PanjangTeks_AwalanBPPU_PlusTahunBuku) = AwalanBPPU_PlusTahunBuku Then
            TermasukHutangTahunIni_Terseleksi = True
        Else
            TermasukHutangTahunIni_Terseleksi = False
        End If

        If AngkaInvoice_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            If JudulForm <> JudulForm_SaldoAwalPiutangUsaha Then TampilkanData_Pencairan()
            If TermasukHutangTahunIni_Terseleksi = True Then btn_LihatJurnal.Enabled = True
        End If

        If TermasukHutangTahunIni_Terseleksi = False Then btn_LihatJurnal.Enabled = False

        'txt_JumlahPiutangUsaha.Text = JumlahPiutangUsaha_Terseleksi
        'txt_TotalBayar.Text = TotalJumlahBayar
        'If SisaPiutang_Terseleksi = 0 Then
        '    txt_SisaPiutang.Text = StripKosong
        'Else
        '    txt_SisaPiutang.Text = SisaPiutang
        'End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
    End Sub

    Private Sub btn_InputPencairan_Click(sender As Object, e As EventArgs) Handles btn_InputPencairan.Click

        If Baris_Terseleksi < 0 Then
            MsgBox("Tidak ada baris data terseleksi.")
            Return
        End If

        If SisaPiutangUsaha_Terseleksi <= 0 Then
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
        If MitraSebagaiAfiliasi(KodeCustomer_Terseleksi) = True Then
            win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangUsaha_Afiliasi
        Else
            win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangUsaha_NonAfiliasi
        End If
        win_InputBuktiPenerimaan.NomorBP = NomorBPPU_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeCustomer_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.DialogResult = DialogResult.OK Then
            TampilkanData()
        End If

    End Sub

    Private Sub btn_EditPencairan_Click(sender As Object, e As EventArgs) Handles btn_EditPencairan.Click

        FiturBelumBisaDigunakan()
        Return

        'Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        'frm_InputPembayaranHutangUsaha.ResetForm()
        'frm_InputPembayaranHutangUsaha.FungsiForm = FungsiForm_EDIT
        'frm_InputPembayaranHutangUsaha.AlurTransaksi = frm_InputPembayaranHutangUsaha.AlurTransaksi_PencairanPiutangUsaha
        'frm_InputPembayaranHutangUsaha.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        'frm_InputPembayaranHutangUsaha.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        'frm_InputPembayaranHutangUsaha.lbl_PembayaranKe.Text = "Pencairan Ke-" & (BarisBayar_Terseleksi + 1).ToString
        'frm_InputPembayaranHutangUsaha.Referensi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        'IsiValueFormInputPembayaranPiutangUsaha()
        'frm_InputPembayaranHutangUsaha.NomorIdBayar = NomorIdPembayaran_Terseleksi
        'Dim BarisKe = 0
        'Dim HitungBayar = 0
        'Do While BarisKe < BarisBayar_Terseleksi
        '    HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
        '    BarisKe += 1
        'Loop
        'frm_InputPembayaranHutangUsaha.txt_JumlahTelahDibayar.Text = HitungBayar
        'frm_InputPembayaranHutangUsaha.txt_SisaHutang.Text = JumlahPiutangUsaha_Terseleksi - HitungBayar
        'frm_InputPembayaranHutangUsaha.txt_JumlahBayarSekarang.Text = NominalBayar
        'frm_InputPembayaranHutangUsaha.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        'frm_InputPembayaranHutangUsaha.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        ''Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        'frm_InputPembayaranHutangUsaha.ShowDialog()

        'If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

        'Dim win_InputBuktiPenerimaan As New wpfWin_InputBuktiPenerimaan
        'win_InputBuktiPenerimaan.ResetForm()
        'win_InputBuktiPenerimaan.FungsiForm = FungsiForm_EDIT
        'win_InputBuktiPenerimaan.JalurMasuk = JalurUtama
        'win_InputBuktiPenerimaan.AngkaKM = AngkaKM_Terseleksi
        'win_InputBuktiPenerimaan.ShowDialog()

    End Sub

    Sub IsiValueFormInputPembayaranPiutangUsaha()

        frm_InputPembayaranHutangUsaha.txt_NomorBPHU.Text = NomorBPPU_Terseleksi
        frm_InputPembayaranHutangUsaha.txt_NomorPembelian.Text = NomorPenjualan_Terseleksi
        frm_InputPembayaranHutangUsaha.TanggalInvoice = TanggalInvoice_Terseleksi
        frm_InputPembayaranHutangUsaha.NomorInvoice = NomorInvoice_Terseleksi
        frm_InputPembayaranHutangUsaha.KodeMitra = KodeCustomer_Terseleksi
        frm_InputPembayaranHutangUsaha.NamaMitra = NamaCustomer_Terseleksi
        frm_InputPembayaranHutangUsaha.TanggalFakturPajak = TanggalInvoice_Terseleksi  '(Tanggal Faktur Pajak harus sama dengan Tanggal Invoice)
        frm_InputPembayaranHutangUsaha.NomorFakturPajak = NomorFakturPajak_Terseleksi
        frm_InputPembayaranHutangUsaha.txt_JumlahTerutang.Text = JumlahPiutangUsaha_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPh_Total = PPhTerutang_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPhDipotong_Total = PPhDipotong_Terseleksi

    End Sub

    Private Sub btn_HapusPencairan_Click(sender As Object, e As EventArgs) Handles btn_HapusPencairan.Click

        FiturBelumBisaDigunakan()
        Return

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranPiutangUsaha :
        cmd = New OdbcCommand(" DELETE FROM tbl_PencairanPiutangUsaha " &
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
            'frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'Nanti tambahkan Sub Tampilkan data untuk PPh Pasal 4 (2)
            pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Sub TampilkanData_Pencairan()

        grb_Pencairan.Visible = True
        'grb_InfoSaldo.Visible = False

        dgv_DetailBayar.Visible = True
        dgv_DetailBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP      = '" & NomorBPPU_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            Dim PotonganPPh = dr.Item("PPh_Dipotong")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, PotonganPPh, KeteranganBayar, NomorJVBayar)
        Loop
        AksesDatabase_Transaksi(Tutup)


        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPencairan.Enabled = False
        btn_HapusPencairan.Enabled = False

    End Sub

    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_EditPencairan.Enabled = False
        btn_HapusPencairan.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Penjualan_Terseleksi = 0
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
        NomorJV_Penjualan_Terseleksi = 0
        If BarisBayar_Terseleksi >= 0 Then
            btn_EditPencairan.Enabled = True
            btn_HapusPencairan.Enabled = True
            btn_LihatJurnal.Enabled = True
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
    End Sub



    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class