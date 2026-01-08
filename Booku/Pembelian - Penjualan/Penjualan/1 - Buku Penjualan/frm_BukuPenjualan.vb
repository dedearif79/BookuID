Imports bcomm
Imports System.Data.Odbc

Public Class frm_BukuPenjualan

    Public KesesuaianJurnal As Boolean
    Public JudulForm
    Dim QueryTampilan

    'Variabel Kolom Tabel :
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorPenjualan
    Dim JenisInvoice
    Dim JenisProduk
    Dim AngkaInvoice
    Dim NomorInvoice_Sebelumnya
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim NP
    Dim TanggalInvoice
    Dim TanggalPembetulan
    Dim MasaJatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeProject
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahHarga
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim JenisPPN
    Dim PerlakuanPPN
    Dim PPN
    Dim PPhDipotong
    Dim TagihanBruto
    Dim Retur
    Dim TagihanNetto
    Dim KeteranganJatuhTempo
    Dim KodeFP
    Dim Catatan
    Dim NomorJV

    'Variabel Total :
    Dim Total_JumlahHarga
    Dim Total_DiskonRp
    Dim Total_DasarPengenaanPajak
    Dim Total_PPN
    Dim Total_PPhDipotong
    Dim Total_TagihanBruto
    Dim Total_Retur
    Dim Total_TagihanNetto

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorPenjualan_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NP_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim TanggalPembetulan_Terseleksi
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
    Dim JenisPPN_Terseleksi
    Dim PerlakuanPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim TagihanBruto_Terseleksi
    Dim Retur_Terseleksi
    Dim TagihanNetto_Terseleksi
    Dim KodeFP_Terseleksi
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim InvoiceDenganPO As Boolean
    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Filter :
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeCustomer
    Dim Pilih_JatuhTempo

    'Tombol Jurnal
    Dim TombolJurnal
    Dim TombolJurnal_Jurnal = "Jurnal"
    Dim TombolJurnal_Dorong = "Dorong ke Jurnal"
    Dim TombolJurnal_Lihat = "Lihat Jurnal"

    'Jenis Tampilan :
    Public JenisTampilan
    Public JenisTampilan_Semua = "Semua"
    Public JenisTampilan_HasilAkhir = "Hasil Akhir"

    'Jenis Penjualan :
    Public JenisPenjualan
    Public JenisPenjualan_Rutin = "Rutin"
    Public JenisPenjualan_Asset = "Asset"

    Public DestinasiPenjualan As String
    Dim PenjualanLokal As Boolean
    Dim PenjualanEkspor As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
        End If

        If JenisPenjualan = Kosongan Then JenisPenjualan = JenisPenjualan_Rutin
        If JenisPenjualan = JenisPenjualan_Rutin Then JudulForm = "Buku Penjualan"
        If JenisPenjualan = JenisPenjualan_Asset Then JudulForm = "Buku Penjualan Asset Tetap"

        If PerusahaanSebagaiPKP = True Then
            DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = True
        Else
            DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = False
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisTampilan()
        KontenCombo_JenisProduk_Induk()
        KontenCombo_Customer()
        KontenCombo_JatuhTempo()
        EksekusiKode = True
        TampilkanData()
    End Sub

    Sub KontenCombo_JenisTampilan()
        cmb_JenisTampilan.Items.Clear()
        cmb_JenisTampilan.Items.Add(JenisTampilan_Semua)
        cmb_JenisTampilan.Items.Add(JenisTampilan_HasilAkhir)
        cmb_JenisTampilan.Text = JenisTampilan_Semua
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

    Sub KontenCombo_Customer()
        cmb_Customer.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
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

    Sub TampilkanData()

        If ProsesLoadingForm = True Then Return
        If ProsesResetForm = True Then Return
        If EksekusiKode = False Then Return
        If JenisPenjualan = Kosongan Then JenisPenjualan = JenisPenjualan_Rutin
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

        'Filter Jatuh Tempo :
        Dim FilterJatuhTempo = " "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterCustomer & FilterJatuhTempo

        'Query Tampilan :
        If JenisPenjualan = JenisPenjualan_Rutin Then
            QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Nomor_Invoice <> 'X' " & FilterData & " ORDER BY Angka_Invoice "
        End If

        If JenisPenjualan = JenisPenjualan_Asset Then
            QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Asset = 1 " & FilterData & " ORDER BY Angka_Invoice "
        End If

        'Data Tabel :
        Index_BarisTabel = 0
        NomorUrut = 0
        NomorInvoice_Sebelumnya = Kosongan

        'Total :
        Total_JumlahHarga = 0
        Total_DiskonRp = 0
        Total_DasarPengenaanPajak = 0
        Total_PPN = 0
        Total_PPhDipotong = 0
        Total_TagihanBruto = 0
        Total_Retur = 0
        Total_TagihanNetto = 0

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
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorInvoice = dr.Item("Nomor_Invoice")
            NP = dr.Item("N_P")
            Dim NomorInvoice_Pembetulan = Kosongan
            Dim NP_Pembetulan = Kosongan
            If NP = "N" Then
                NomorInvoice_Pembetulan = NomorInvoice & "-P1"
            Else
                Dim PembetulanKe = AmbilAngka(NP)
                NP_Pembetulan = "P" & (PembetulanKe + 1)
                NomorInvoice_Pembetulan = Microsoft.VisualBasic.Replace(NomorInvoice, NP, NP_Pembetulan)
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                         " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows And JenisTampilan = JenisTampilan_HasilAkhir Then Continue Do
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            If NP = "N" Then
                TanggalPembetulan = StripKosong
            Else
                TanggalPembetulan = TanggalFormatTampilan(dr.Item("Tanggal_Pembetulan"))
            End If
            MasaJatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If MasaJatuhTempo > 0 Then
                MasaJatuhTempo &= " hari"
            Else
                MasaJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice WHERE " &
                                         " Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
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
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project_Produk")
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
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project_Produk")
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
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonRp = dr.Item("Diskon")
            If DiskonRp = 0 Then DiskonRp = StripKosong
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            PPN = dr.Item("PPN")
            If PPN = 0 Then PPN = StripKosong
            PPhDipotong = dr.Item("PPh_Dipotong")
            If PPhDipotong = 0 Then PPhDipotong = StripKosong
            TagihanBruto = dr.Item("Total_Tagihan")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            TagihanNetto = TagihanBruto - Retur
            If Retur = 0 Then Retur = StripKosong
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
            If TanggalJatuhTempo_Date >= Today Then
                KeteranganJatuhTempo = JatuhTempo_Belum
            Else
                KeteranganJatuhTempo = JatuhTempo_JT
            End If
            KodeFP = Microsoft.VisualBasic.Left(NomorFakturPajak, 2)
            Catatan = dr.Item("Catatan")
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                If Pilih_JatuhTempo = JatuhTempo_Semua Then
                    TambahBaris()
                Else
                    If Pilih_JatuhTempo = KeteranganJatuhTempo Then TambahBaris()
                End If
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan,
                                Total_JumlahHarga, Total_DiskonRp, Total_DasarPengenaanPajak, Kosongan, Kosongan,
                                Total_PPN, Total_PPhDipotong, Total_TagihanBruto, Total_Retur, Total_TagihanNetto,
                                Kosongan)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        If PenjualanLokal And MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then Return
        If PenjualanEkspor And Not MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then Return
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, NomorPenjualan, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NP, NomorFakturPajak, TanggalInvoice, TanggalPembetulan,
                                KodeCustomer, NamaCustomer,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, JenisPPN, PerlakuanPPN, PPN, PPhDipotong, TagihanBruto, Retur, TagihanNetto,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject,
                                MasaJatuhTempo, KeteranganJatuhTempo, KodeFP, Catatan, NomorJV)
        If NomorJV > 0 Then
            If NP = "N" Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If NP <> "N" Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaBiruSolid
        Else
            DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If
        'Akumulasi/Total :
        Total_JumlahHarga += AmbilAngka(JumlahHarga)
        Total_DiskonRp += AmbilAngka(DiskonRp)
        Total_DasarPengenaanPajak += AmbilAngka(DasarPengenaanPajak)
        Total_PPN += AmbilAngka(PPN)
        Total_PPhDipotong += AmbilAngka(PPhDipotong)
        Total_TagihanBruto += AmbilAngka(TagihanBruto)
        Total_Retur += AmbilAngka(Retur)
        Total_TagihanNetto += AmbilAngka(TagihanNetto)
        Index_BarisTabel += 1
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Pratinjau.Enabled = False
        btn_Cetak.Enabled = False
        btn_Jurnal.Enabled = False
        btn_Jurnal.Text = TombolJurnal_Jurnal
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_JenisTampilan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisTampilan.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisTampilan_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisTampilan.TextChanged
        JenisTampilan = cmb_JenisTampilan.Text
        TampilkanData()
    End Sub

    Private Sub cmb_JenisProduk_Induk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisProduk_Induk_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.TextChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.Text
        TampilkanData()
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

    Private Sub cmb_JatuhTempo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.SelectedIndexChanged
    End Sub
    Private Sub cmb_JatuhTempo_TextChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.TextChanged
        Pilih_JatuhTempo = cmb_JatuhTempo.Text
        TampilkanData()
    End Sub

    Private Sub btn_Jurnal_Click(sender As Object, e As EventArgs) Handles btn_Jurnal.Click

        If TombolJurnal = TombolJurnal_Dorong Then

            Select Case AmbilValue_JenisPenjualanBerdasarkanInvoicePenjualan(NomorInvoice_Terseleksi)
                Case JenisPenjualan_Tunai
                    win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
                    win_InputBuktiPenerimaan.ResetForm()
                    ProsesIsiValueForm = True
                    win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
                    win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
                    win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PenerimaanTunai
                    win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
                    win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_InvoiceTunai
                    win_InputBuktiPenerimaan.NomorBP = NomorInvoice_Terseleksi
                    win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeCustomer_Terseleksi
                    ProsesIsiValueForm = False
                    win_InputBuktiPenerimaan.ShowDialog()
                    If win_InputBuktiPenerimaan.DialogResult = DialogResult.OK Then
                        TampilkanData()
                    End If
                Case JenisPenjualan_Tempo
                    win_DorongInvoiceKeJurnal = New wpfWin_DorongInvoiceKeJurnal
                    win_DorongInvoiceKeJurnal.ResetForm()
                    win_DorongInvoiceKeJurnal.NomorInvoice = NomorInvoice_Terseleksi
                    win_DorongInvoiceKeJurnal.NP = NP_Terseleksi
                    win_DorongInvoiceKeJurnal.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
                    win_DorongInvoiceKeJurnal.ShowDialog()
            End Select

        End If
        If TombolJurnal = TombolJurnal_Lihat Then
            LihatJurnal(NomorJV_Terseleksi)
        End If

    End Sub
    Private Sub btn_Jurnal_TextChanged(sender As Object, e As EventArgs) Handles btn_Jurnal.TextChanged
        TombolJurnal = btn_Jurnal.Text
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
        NomorPenjualan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Penjualan", Baris_Terseleksi).Value)
        JenisInvoice_Terseleksi = DataTabelUtama.Item("Jenis_Invoice", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        AngkaInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Invoice", Baris_Terseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", Baris_Terseleksi).Value
        NP_Terseleksi = DataTabelUtama.Item("N_P", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        TanggalPembetulan_Terseleksi = DataTabelUtama.Item("Tanggal_Pembetulan", Baris_Terseleksi).Value
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
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PerlakuanPPN_Terseleksi = DataTabelUtama.Item("Perlakuan_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        PPhDipotong_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Dipotong", Baris_Terseleksi).Value)
        TagihanBruto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Bruto", Baris_Terseleksi).Value)
        Retur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_", Baris_Terseleksi).Value)
        TagihanNetto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Netto", Baris_Terseleksi).Value)
        KodeFP_Terseleksi = DataTabelUtama.Item("Kode_FP", Baris_Terseleksi).Value
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value


        If AngkaInvoice_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Pratinjau.Enabled = True
            btn_Cetak.Enabled = True
            btn_Jurnal.Enabled = True
            If NomorJV_Terseleksi = 0 Then
                btn_Jurnal.Text = TombolJurnal_Dorong
            Else
                btn_Jurnal.Text = TombolJurnal_Lihat
            End If
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Pratinjau_Click(sender, e)
    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        Cetak(JenisFormCetak_Invoice, NomorInvoice_Terseleksi, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        'IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_Invoice, NomorInvoice_Terseleksi, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_CETAK
        'IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        NomorInvoicePenjualan_Cetak = NomorInvoice_Terseleksi
        TampilkanHalamanCetak_InvoicePenjualan()
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class