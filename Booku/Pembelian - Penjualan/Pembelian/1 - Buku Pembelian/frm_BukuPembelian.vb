Imports bcomm
Imports System.Data.Odbc

Public Class frm_BukuPembelian

    Public KesesuaianJurnal As Boolean
    Public JudulForm
    Dim QueryTampilan

    'Variabel Kolom Tabel :
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorPembelian
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
    Dim KodeSupplier
    Dim NamaSupplier
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
    Dim NomorPembelian_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NP_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim TanggalPembetulan_Terseleksi
    Dim MasaJatuhTempo_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
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
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim InvoiceDenganPO As Boolean
    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Filter :
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeSupplier
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

    Public AsalPembelian
    Dim PembelianLokal As Boolean
    Dim PembelianImpor As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        If AsalPembelian = AsalPembelian_Lokal Then
            PembelianLokal = True
            PembelianImpor = False
        Else
            PembelianLokal = False
            PembelianImpor = True
        End If

        JudulForm = "Buku Pembelian"

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisTampilan()
        KontenCombo_JenisProduk_Induk()
        KontenCombo_Supplier()
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

    Sub KontenCombo_Supplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaSupplier = dr.Item("Nama_Mitra")
            cmb_Supplier.Items.Add(NamaSupplier)
        Loop
        cmb_Supplier.Text = Pilihan_Semua
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

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.Text <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'Filter Supplier :
        Dim FilterSupplier = " "
        If cmb_Supplier.Text <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        'Filter Jatuh Tempo :
        Dim FilterJatuhTempo = " "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterSupplier & FilterJatuhTempo

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_Pembelian_Invoice " &
            " WHERE Nomor_Invoice <> 'X' " & FilterData & " ORDER BY Angka_Invoice "

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
            NomorPO = Kosongan
            TanggalPO = Kosongan
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            NomorPembelian = dr.Item("Nomor_Pembelian")
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorInvoice = dr.Item("Nomor_Invoice")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice WHERE " &
                                         " Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                KodeProject = Kosongan
                NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
                'Surat Jalan : ---------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
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
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project_Produk")
                        End If
                    End If
                End If
                'BAST : ------------------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_BAST " &
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
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
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
        If PembelianLokal And MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then Return
        If PembelianImpor And Not MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then Return
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, NomorPembelian, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NP, NomorFakturPajak, TanggalInvoice, TanggalPembetulan,
                                KodeSupplier, NamaSupplier,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, JenisPPN, PerlakuanPPN, PPN, PPhDipotong, TagihanBruto, Retur, TagihanNetto,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject,
                                MasaJatuhTempo, KeteranganJatuhTempo, Catatan, NomorJV)
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

    Private Sub cmb_Supplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Supplier.SelectedIndexChanged
    End Sub
    Private Sub cmb_Supplier_TextChanged(sender As Object, e As EventArgs) Handles cmb_Supplier.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Supplier.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeSupplier = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Supplier.Text = Pilihan_Semua Then Pilih_KodeSupplier = Pilihan_Semua
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
            'frm_DorongInvoiceKeJurnal.ResetForm()
            'frm_DorongInvoiceKeJurnal.NomorInvoice = NomorInvoice_Terseleksi
            'frm_DorongInvoiceKeJurnal.NP = NP_Terseleksi
            'frm_DorongInvoiceKeJurnal.ShowDialog()
            PesanUntukProgrammer("Tidak ada pendorongan ke Jurnal di Modul Pembelian...!" & Enter2Baris &
                                  "Nanti harus diperbaiki lagi algoritmanya ya...!")
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
        NomorPembelian_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Pembelian", Baris_Terseleksi).Value)
        JenisInvoice_Terseleksi = DataTabelUtama.Item("Jenis_Invoice", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        AngkaInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Invoice", Baris_Terseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        NP_Terseleksi = DataTabelUtama.Item("N_P", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        TanggalPembetulan_Terseleksi = DataTabelUtama.Item("Tanggal_Pembetulan", Baris_Terseleksi).Value
        MasaJatuhTempo_Terseleksi = DataTabelUtama.Item("Masa_Jatuh_Tempo", Baris_Terseleksi).Value
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
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
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value


        If AngkaInvoice_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
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
    End Sub

End Class