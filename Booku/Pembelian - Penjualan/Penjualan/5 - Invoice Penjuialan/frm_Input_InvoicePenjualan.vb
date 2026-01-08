Imports bcomm
Imports System.Data.Odbc

Public Class frm_Input_InvoicePenjualan

    Public JudulForm
    Public FungsiForm
    Public NomorID
    Public InvoiceDenganPO As Boolean

    Public NomorJV
    Public COADebet
    Public JumlahDebet



    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    Dim JenisJasa

    Dim EksekusiPerhitungan_BiayaTransportasi As Boolean

    Public KunciTanggalInvoice As Boolean

    Dim KunciInputanTarifPPh As Boolean

    'Variabel Kolom :
    Public AngkaInvoice
    Dim NomorInvoice
    Dim NomorInvoiceLama
    Dim JenisInvoice
    Public NomorPenjualan
    Public Referensi
    Public NP
    Public TanggalInvoice
    Public TanggalPembetulan
    Public TanggalLapor
    Dim JumlahHariJatuhTempo
    Dim TanggalJatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterimaSJBAST
    Dim KodeCustomer
    Dim NamaCustomer
    Dim Catatan
    Dim JumlahHargaKeseluruhan As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64                      '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPP As Int64                            '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPBarang As Int64                      '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa As Int64                        '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa_BerdasarkanPO As Int64          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public NomorFakturPajak
    Dim TarifPPN As Decimal
    Dim PPN As Int64                            '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan_Kotor As Int64             '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JenisPPh
    Dim TarifPPh As Decimal
    Dim PPhTerutang As Int64                    '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDitanggung As Int64                  '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDipotong As Int64                    '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim BiayaTransportasiPenjualan As Int64     '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDitanggung_BerdasarkanPO As Int64    '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturDPP As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturPPN As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JumlahPiutangUsaha As Int64



    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim JenisProdukPerItem
    Dim KodeProjectProduk
    Dim NomorPOProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem As Integer
    Dim SatuanProduk
    Dim HargaSatuan As Int64
    Dim DiskonPerItem_Persen As Decimal
    Dim DiskonPerItem_Rp As Int64
    Dim TotalHarga As Int64

    'Variabel Tabel Index :
    Dim Baris_Terseleksi
    Dim NomorUrutProduk_Terseleksi
    Dim JenisProdukPerItem_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi

    'Variabel Tabel SJBAST - Index :
    Dim BarisSJBAST_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim TanggalDiterimaSJBAST_Terseleksi

    Dim JumlahProduk
    Dim JumlahSJBAST
    Dim NomorSJBAST_TerakhirDitambahkan
    Dim NomorPO_TerakhirDitambahkan

    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Dim Koreksi

    Public KodeSetoran

    Public JualAsset As Boolean
    Dim Asset As Integer
    Public KelompokHarta

    Dim COAPenjualanBarangAtauAsset
    Dim COAJasa

    Public PenyimpananInvoicePenjualan As Boolean

    Public AdaPenyimpananjurnal As Boolean


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_InvoicePenjualan()
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                JenisProduk_Induk = Kosongan
                Do While JenisProduk_Induk = Kosongan
                    frm_PilihJenisProdukInduk.ShowDialog()
                    JenisProduk_Induk = frm_PilihJenisProdukInduk.JenisProduk_Induk
                    If JenisProduk_Induk = Kosongan Then
                        MsgBox("Silakan pilih Jenis Produk..!")
                    End If
                Loop
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua And InvoiceDenganPO = False Then
                PesanUntukProgrammer("Jenis Produk belum ditentukan..!")
                BeginInvoke(Sub() Me.Close())
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua Then
                JudulForm = "Input Invoice Penjualan"
            Else
                JudulForm = "Input Invoice Penjualan - " & JenisProduk_Induk
            End If
            NP = "N"
            btn_Pratinjau.Visible = False
            btn_Cetak.Visible = False
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_PEMBETULAN _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            JudulForm = "Edit Invoice Penjualan - " & JenisProduk_Induk
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                  " WHERE Kode_Mitra = '" & KodeCustomer & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            txt_AlamatCustomer.Text = dr.Item("Alamat")
            AksesDatabase_General(Tutup)
            btn_Pratinjau.Visible = True
            btn_Pratinjau.Enabled = True
            btn_Cetak.Visible = True
            btn_Cetak.Enabled = True
            NomorInvoiceLama = NomorInvoice
            lbl_JenisPPN.Enabled = False
            lbl_PerlakuanPPN.Enabled = False
            cmb_JenisPPN.Enabled = False
            cmb_PerlakuanPPN.Enabled = False
        End If

        If FungsiForm = FungsiForm_PEMBETULAN Then
            JudulForm = "Pembetulan Invoice Penjualan - " & JenisProduk_Induk
            NomorInvoiceLama = NomorInvoice
            Dim PembetulanKe As Integer = 0
            Dim NPLama = Kosongan
            If NP = "N" Then
                PembetulanKe = 1
                NP = "P" & PembetulanKe
                txt_NomorInvoice.Text = NomorInvoiceLama & "-" & NP
            Else
                NPLama = "P" & AmbilAngka_TanpaMinus(Microsoft.VisualBasic.Right(NomorInvoiceLama, 3))
                PembetulanKe = AmbilAngka_TanpaMinus(Microsoft.VisualBasic.Right(NomorInvoiceLama, 3)) + 1
                NP = "P" & PembetulanKe
                txt_NomorInvoice.Text = Microsoft.VisualBasic.Replace(NomorInvoiceLama, NPLama, NP)
            End If
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            btn_Simpan.Enabled = False
            btn_Batal.Text = teks_Tutup
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("FungsiForm belum ditentukan..!!!")

        If NP = "N" Then
            lbl_TanggalInvoice.Text = "Tanggal Invoice"
        Else
            lbl_TanggalInvoice.Text = "Tanggal Pembetulan"
        End If

        Me.Text = JudulForm

        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            lbl_DPPBarang.Visible = True
            lbl_DPPJasa.Visible = True
            txt_DPPBarang.Visible = True
            txt_DPPJasa.Visible = True
        Else
            lbl_DPPBarang.Visible = False
            lbl_DPPJasa.Visible = False
            txt_DPPBarang.Visible = False
            txt_DPPJasa.Visible = False
        End If

        If InvoiceDenganPO = True Then
            grb_Produk.Visible = False
            lbl_SJBAST.Text = "Surat Jalan / BAST :"
            btn_TambahSJBAST.Visible = True
            btn_SingkirkanSJBAST.Visible = True
            dgv_SJBAST.Visible = True
        Else
            grb_Produk.Visible = True
            lbl_SJBAST.Text = "Kode Project :"
            btn_TambahSJBAST.Visible = False
            btn_SingkirkanSJBAST.Visible = False
            dgv_SJBAST.Visible = False
        End If

        StyleTabelUtama(DataTabelUtama)
        StyleTabelUtama(dgv_SJBAST)

        BeginInvoke(Sub() BersihkanSeleksi_TabelProduk())
        BeginInvoke(Sub() BersihkanSeleksi_TabelSJBAST())

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            txt_Referensi.Text = dr.Item("Referensi")
            cmb_JenisPPN.Text = dr.Item("Jenis_PPN")
            cmb_PerlakuanPPN.Text = dr.Item("Perlakuan_PPN")
            cmb_JenisPPh.Text = dr.Item("Jenis_PPh")
            txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            txt_TotalTagihan.Text = dr.Item("Total_Tagihan")
            JenisPenjualan = dr.Item("Jenis_Penjualan")
            If JenisPenjualan = JenisPenjualan_Tunai Then KondisiForm_JenisPenjualanTunai()
            cmb_SaranaPembayaran.Text = dr.Item("Sarana_Pembayaran")
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            cmb_DitanggungOleh.Text = dr.Item("Ditanggung_Oleh")
            txt_BiayaTransportasiPenjualan.Text = dr.Item("Biaya_Transportasi")
            Asset = dr.Item("Asset")
            If Asset = 1 Then JualAsset = True
            If Asset = 0 Then JualAsset = False
        End If
        AksesDatabase_Transaksi(Tutup)

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            dgv_SJBAST.Columns("Biaya_Transportasi").Visible = True
        Else
            dgv_SJBAST.Columns("Biaya_Transportasi").Visible = False
        End If

        If JualAsset = True Then
            Asset = 1
            txt_PPhDitanggung.Enabled = False
            txt_PPhDipotong.Enabled = False
        Else
            Asset = 0
        End If

        ProsesLoadingForm = False

        If FungsiForm <> FungsiForm_TAMBAH Then Perhitungan()

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        NomorJV = 0

        NomorInvoiceLama = Kosongan

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan
        JenisPPh = Kosongan

        InvoiceDenganPO = True

        KunciTanggalInvoice = False

        NomorID = 0
        AngkaInvoice = 0
        txt_NomorInvoice.Text = Kosongan
        NP = Kosongan
        dtp_TanggalInvoice.Value = Today
        rdb_JumlahHariJatuhTempo.Checked = False
        rdb_TanggalJatuhTempo.Checked = False
        txt_JumlahHariJatuhTempo.Enabled = False
        lbl_JumlahHariJatuhTempo.Enabled = False
        txt_JumlahHariJatuhTempo.Text = Kosongan
        'dtp_TanggalInvoice.Enabled = False
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        KondisiForm_JenisPenjualanTempo()
        dtp_TanggalJatuhTempo.Value = Today
        txt_Referensi.Text = Kosongan
        KontenCombo_JenisInvoice()
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_AlamatCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_Catatan.Text = Kosongan
        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_DPPBarang.Text = Kosongan
        txt_DPPJasa.Text = Kosongan
        NomorFakturPajak = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalTagihan_Kotor.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        KunciInputanTarifPPh = True
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_BiayaTransportasiPenjualan.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan
        btn_Simpan.Enabled = True
        btn_Batal.Text = teks_Batal
        ReturDPP = 0
        ReturPPN = 0
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()
        cmb_JenisPPh.Text = Kosongan '(Jangan dihapus..!)

        lbl_PPh.Enabled = True
        cmb_JenisPPh.Enabled = True
        txt_TarifPPh.Enabled = True
        txt_PPhTerutang.Enabled = True
        txt_PPhDitanggung.Enabled = True
        txt_PPhDipotong.Enabled = True

        Koreksi = Kosongan
        NomorSJBAST_TerakhirDitambahkan = Kosongan
        NomorPO_TerakhirDitambahkan = Kosongan
        KodeSetoran = KodeSetoran_Non

        JualAsset = False
        Asset = 0
        KelompokHarta = Kosongan

        PenyimpananInvoicePenjualan = False
        'Value 'AdaPenyimpananJurnal' jangan direset menjadi False. Biarkan apa adanya...!!!

        EksekusiPerhitungan_BiayaTransportasi = True

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_JenisInvoice()
        cmb_JenisInvoice.Items.Clear()
        cmb_JenisInvoice.Items.Add(JenisInvoice_Biasa)
        'Untuk Jenis Invoice Gabungan, hanya baru bisa digunakan untuk Jenis Produk Induk : Barang
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisInvoice.Items.Add(JenisInvoice_Gabungan)
        cmb_JenisInvoice.Text = JenisInvoice_Biasa
    End Sub

    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        If PerusahaanSebagaiPKP = True Then
            cmb_JenisPPN.Items.Add(JenisPPN_Exclude)
            cmb_JenisPPN.Items.Add(JenisPPN_Include)
            cmb_JenisPPN.Text = Kosongan
        Else
            cmb_JenisPPN.Items.Add(JenisPPN_NonPPN)
            cmb_JenisPPN.Text = JenisPPN_NonPPN
        End If
        lbl_JenisPPN.Enabled = True
        cmb_JenisPPN.Enabled = True
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        lbl_PerlakuanPPN.Enabled = True
        cmb_PerlakuanPPN.Enabled = True
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal26)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
    End Sub

    Sub SistemPenomoranOtomatis_InvoicePenjualan()

        If FungsiForm = FungsiForm_TAMBAH Then AngkaInvoice = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_Invoice", "Angka_Invoice") + 1
        NomorInvoice = AwalanINV & AngkaInvoice.ToString & "-" & BulanRomawi(dtp_TanggalInvoice.Value.Month) & "-" & dtp_TanggalInvoice.Value.Year
        txt_NomorInvoice.Text = NomorInvoice
        NomorPenjualan = AwalanPENJ_PlusTahunBuku & AngkaInvoice

    End Sub

    Sub Kosongkan_TabelProduk()
        DataTabelUtama.Rows.Clear()
        JumlahProduk = 0
        Perhitungan()
    End Sub
    Sub Kosongkan_TabelSJBAST()
        dgv_SJBAST.Rows.Clear()
        JumlahSJBAST = 0
        If InvoiceDenganPO = True Then Kosongkan_TabelProduk()
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        Baris_Terseleksi = -1
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        DataTabelUtama.ClearSelection()
        JumlahProduk = DataTabelUtama.RowCount
    End Sub

    Sub BersihkanSeleksi_TabelSJBAST()
        BarisSJBAST_Terseleksi = -1
        NomorSJBAST_Terseleksi = Kosongan
        btn_SingkirkanSJBAST.Enabled = False
        dgv_SJBAST.ClearSelection()
        JumlahSJBAST = dgv_SJBAST.RowCount
    End Sub

    Sub KondisiFormSetelahPerubahan()
        If ProsesLoadingForm = True Or ProsesResetForm = True Or ProsesIsiValueForm = True Then Return
        BersihkanSeleksi_TabelProduk()
        btn_Pratinjau.Enabled = False
        btn_Cetak.Enabled = False
    End Sub

    Sub Perhitungan()

        JumlahProduk = DataTabelUtama.RowCount

        If ProsesResetForm = True _
            Or ProsesIsiValueForm = True _
            Or ProsesLoadingForm = True _
            Or EksekusiKode = False _
            Or JenisPPN = Kosongan _
            Or JumlahProduk = 0 _
            Then Return

        Dim RasioDPPBarang
        Dim RasioPPNInclude

        JumlahHargaKeseluruhan = 0
        Diskon_Rp = 0
        HitunganHarga_Relatif = 0
        DPPBarang = 0
        DPPJasa = 0

        For Each row As DataGridViewRow In DataTabelUtama.Rows
            JumlahHargaKeseluruhan += AmbilAngka(row.Cells("Jumlah_Harga_Per_Item").Value)
            Diskon_Rp += AmbilAngka(row.Cells("Diskon_Per_Item_Rp").Value)
            HitunganHarga_Relatif += AmbilAngka(row.Cells("Total_Harga").Value)
            If row.Cells("Jenis_produk_Per_Item").Value = JenisProduk_Barang Then DPPBarang += AmbilAngka(row.Cells("Total_Harga").Value)
            If row.Cells("Jenis_produk_Per_Item").Value = JenisProduk_Jasa Then DPPJasa += AmbilAngka(row.Cells("Total_Harga").Value)
        Next

        RasioDPPBarang = DPPBarang / HitunganHarga_Relatif
        RasioPPNInclude = 100 / (100 + TarifPPN)

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
            txt_Diskon_Rp.Text = Diskon_Rp
            txt_DasarPengenaanPajak.Text = HitunganHarga_Relatif
        End If

        Select Case JenisPPN
            Case JenisPPN_NonPPN
                PPN = 0
                TotalTagihan_Kotor = DPP
            Case JenisPPN_Exclude
                PPN = DPP * Persen(TarifPPN)
                If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalTagihan_Kotor = DPP + PPN
                If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP
                If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP
            Case JenisPPN_Include
                If HitunganHarga_Relatif = 0 Then
                    Diskon_Rp = 0
                    DPP = 0
                    PPN = 0
                    TotalTagihan_Kotor = 0
                Else
                    TotalTagihan_Kotor = HitunganHarga_Relatif
                    PPN = TotalTagihan_Kotor - (TotalTagihan_Kotor * RasioPPNInclude)
                    DPP = TotalTagihan_Kotor - PPN
                    DPPBarang = DPP * RasioDPPBarang
                    JumlahHargaKeseluruhan = DPP + Diskon_Rp
                    If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalTagihan_Kotor = DPP + PPN
                    If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP
                    If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP
                End If
                '---------------------------------------------------------
                txt_Diskon_Rp.Text = Diskon_Rp
                txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
        End Select

        If JenisProduk_Induk = JenisProduk_JasaKonstruksi Or JenisProduk_Induk = JenisProduk_Jasa Then
            DPPJasa = DPP
            DPPBarang = 0
        Else
            DPPJasa = DPP - DPPBarang
        End If

        PPhTerutang = DPPJasa * Persen(TarifPPh)
        If JenisProduk_Induk = JenisProduk_Barang And PerlakuanPPN = PerlakuanPPN_Dipungut Then PPhTerutang = DPP * Persen(TarifPPh)
        If JualAsset = True Then PPhTerutang = DPP * Persen(TarifPPh)

        If JumlahHargaKeseluruhan > 0 Then
            txt_DPPBarang.Text = DPPBarang
            txt_DPPJasa.Text = DPPJasa
        End If

        txt_DasarPengenaanPajak.Text = DPP
        txt_PPhTerutang.Text = PPhTerutang

        If JumlahProduk > 0 And DPPJasa > 0 And DPPJasa_BerdasarkanPO > 0 Then
            If ProsesLoadingForm = False And ProsesIsiValueForm = False And ProsesResetForm = False Then
                PPhDitanggung = PPhDitanggung_BerdasarkanPO * (DPPJasa / DPPJasa_BerdasarkanPO)
            Else
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" SELECT PPh_Ditanggung FROM tbl_Penjualan_Invoice " &
                                      " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                dr.Read()
                PPhDitanggung = dr.Item("PPh_Ditanggung")
                AksesDatabase_Transaksi(Tutup)
            End If
            EksekusiKode = False
            txt_PPhDitanggung.Text = PPhDitanggung
            EksekusiKode = True
        End If

        If JualAsset = True Then
            PPhDipotong = 0
            PPhDitanggung = 0
        Else
            PPhDipotong = PPhTerutang - PPhDitanggung
        End If
        txt_PPhDipotong.Text = PPhDipotong

        If FungsiForm = FungsiForm_TAMBAH Then Perhitungan_BiayaTransportasi()
        TotalTagihan = TotalTagihan_Kotor + BiayaTransportasiPenjualan - PPhDipotong
        JumlahPiutangUsaha = TotalTagihan_Kotor + BiayaTransportasiPenjualan

        If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
            txt_PPN.Text = Kosongan
            txt_TotalTagihan_Kotor.Text = Kosongan
            txt_TotalTagihan.Text = Kosongan
        Else
            txt_PPN.Text = PPN
            txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
            txt_TotalTagihan.Text = TotalTagihan
        End If

        If Diskon_Rp = 0 Then
            lbl_Diskon.Enabled = False
            txt_Diskon_Rp.Enabled = False
        Else
            lbl_Diskon.Enabled = True
            txt_Diskon_Rp.Enabled = True
        End If

        If InvoiceDenganPO = True And PPhTerutang = 0 Then
            EksekusiKode = False
            cmb_JenisPPh.Text = JenisPPh_NonPPh
            EksekusiKode = True
            KunciInputanTarifPPh = True
        End If

        If InvoiceDenganPO = False Then
            If cmb_JenisPPh.Text = Kosongan Or cmb_JenisPPh.Text = JenisPPh_NonPPh Then
                cmb_JenisPPh.Enabled = False
                JenisPPh = JenisPPh_NonPPh
            Else
                KontenCombo_JenisPPh()
                cmb_JenisPPh.Enabled = True
                KunciInputanTarifPPh = False
            End If
        End If

        If JenisPPh = JenisPPh_NonPPh Then
            lbl_PPh.Enabled = False
            cmb_JenisPPh.Enabled = False
            txt_TarifPPh.Enabled = False
            txt_PPhTerutang.Enabled = False
            txt_PPhDitanggung.Enabled = False
            txt_PPhDipotong.Enabled = False
        Else
            lbl_PPh.Enabled = True
            cmb_JenisPPh.Enabled = True
            txt_TarifPPh.Enabled = True
            txt_PPhTerutang.Enabled = True
            If JualAsset = False Then
                txt_PPhDitanggung.Enabled = True
                txt_PPhDipotong.Enabled = True
            End If
        End If

    End Sub

    Sub Perhitungan_BiayaTransportasi()
        If EksekusiPerhitungan_BiayaTransportasi = True Then
            BiayaTransportasiPenjualan = 0
            For Each row As DataGridViewRow In dgv_SJBAST.Rows
                BiayaTransportasiPenjualan += AmbilAngka(row.Cells("Biaya_Transportasi").Value)
            Next
            txt_BiayaTransportasiPenjualan.Text = BiayaTransportasiPenjualan
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub
    Private Sub txt_NomorInvoice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorInvoice.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalInvoice)
        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.Value
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.Value
        End If
        If ProsesIsiValueForm = False And ProsesResetForm = False And NP = "N" Then
            If FungsiForm <> FungsiForm_PEMBETULAN Then SistemPenomoranOtomatis_InvoicePenjualan()
        End If
        KondisiFormSetelahPerubahan()
        If InvoiceDenganPO = True And EksekusiKode = True And NP = "N" Then
            Kosongkan_TabelSJBAST()
        End If
        LogikaJenisPenjualan()
        txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalInvoice)
    End Sub

    Private Sub rdb_JumlahHariJatuhTempo_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_JumlahHariJatuhTempo.CheckedChanged
        If rdb_JumlahHariJatuhTempo.Checked = True Then
            txt_JumlahHariJatuhTempo.Enabled = True
            txt_JumlahHariJatuhTempo.Focus()
            lbl_JumlahHariJatuhTempo.Enabled = True
            dtp_TanggalJatuhTempo.Enabled = False
        Else
            txt_JumlahHariJatuhTempo.Enabled = False
            lbl_JumlahHariJatuhTempo.Enabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub rdb_TanggalJatuhTempo_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_TanggalJatuhTempo.CheckedChanged
        If rdb_TanggalJatuhTempo.Checked = True Then
            txt_JumlahHariJatuhTempo.Text = Kosongan
            txt_JumlahHariJatuhTempo.Enabled = False
            lbl_JumlahHariJatuhTempo.Enabled = False
            dtp_TanggalJatuhTempo.Enabled = True
        Else
            dtp_TanggalJatuhTempo.Enabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_JumlahHariJatuhTempo_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHariJatuhTempo.TextChanged
        JumlahHariJatuhTempo = AmbilAngka(txt_JumlahHariJatuhTempo.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHariJatuhTempo)
        If JumlahHariJatuhTempo > 0 Then
            rdb_JumlahHariJatuhTempo.Checked = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_JumlahHariJatuhTempo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHariJatuhTempo.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahHariJatuhTempo_Click(sender As Object, e As EventArgs) Handles txt_JumlahHariJatuhTempo.Click
        rdb_JumlahHariJatuhTempo.Checked = True
    End Sub


    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.ValueChanged
        TanggalJatuhTempo = dtp_TanggalJatuhTempo.Value
        KondisiFormSetelahPerubahan()
        LogikaJenisPenjualan()
    End Sub
    Private Sub dtp_TanggalJatuhTempo_Click(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.Click
        rdb_TanggalJatuhTempo.Checked = True
    End Sub
    Sub LogikaJenisPenjualan()
        If dtp_TanggalJatuhTempo.Value = dtp_TanggalInvoice.Value Then '(Sengaja pakai variabel dtp_****..! Jangan diganti...!!!)
            If NP = "N" Then KondisiForm_JenisPenjualanTunai()
        Else
            If NP = "N" Then KondisiForm_JenisPenjualanTempo()
        End If
    End Sub
    Sub KondisiForm_JenisPenjualanTunai()
        JenisPenjualan = JenisPenjualan_Tunai
        lbl_SaranaPembayaran.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
    End Sub
    Sub KondisiForm_JenisPenjualanTempo()
        JenisPenjualan = JenisPenjualan_Tempo
        lbl_SaranaPembayaran.Enabled = False
        cmb_SaranaPembayaran.Enabled = False
        Reset_grb_Bank()
        cmb_SaranaPembayaran.Text = Kosongan
        LogikaCOADebet_UntukNonTunai()
    End Sub
    Sub LogikaCOADebet_UntukNonTunai()
        If JenisJasa = JenisJasa_Dividen Or JenisJasa = JenisJasa_LabaPajakBUT Then
            COADebet = KodeTautanCOA_PiutangPemegangSaham
        Else
            If MitraSebagaiAfiliasi(KodeCustomer) Then
                COADebet = KodeTautanCOA_PiutangUsaha_Afiliasi
            Else
                COADebet = KodeTautanCOA_PiutangUsaha_NonAfiliasi
            End If
        End If
        If JenisProduk_Induk = JenisProduk_Barang Then
            If MitraSebagaiAfiliasi(KodeCustomer) Then
                COADebet = KodeTautanCOA_PiutangUsaha_Afiliasi
            Else
                COADebet = KodeTautanCOA_PiutangUsaha_NonAfiliasi
            End If
        End If
    End Sub


    Private Sub txt_Referensi_TextChanged(sender As Object, e As EventArgs) Handles txt_Referensi.TextChanged
        Referensi = txt_Referensi.Text
    End Sub

    Private Sub cmb_JenisInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisInvoice.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisInvoice_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisInvoice.TextChanged
        JenisInvoice = cmb_JenisInvoice.Text
        If JenisInvoice = JenisInvoice_Biasa _
            And JumlahSJBAST > 0 _
            And ProsesResetForm = False _
            Then
            Kosongkan_TabelSJBAST()
            MsgBox("Daftar Surat Jalan / BAST telah dikosongkan." & Enter2Baris & "Silakan isi kembali.")
            btn_TambahSJBAST.Focus()
        End If
        KunciTanggalInvoice = False
        If JenisInvoice = JenisInvoice_Gabungan Then btn_TambahSJBAST.Enabled = True
    End Sub

    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        If KodeCustomer = Kosongan Then
            btn_TambahSJBAST.Enabled = False
        Else
            btn_TambahSJBAST.Enabled = True
        End If
        Kosongkan_TabelSJBAST()
    End Sub
    Private Sub txt_KodeCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Mitra_Customer
        If txt_KodeCustomer.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeCustomer.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaCustomer.Text
            win_ListLawanTransaksi.AlamatMitraTerseleksi = txt_AlamatCustomer.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeCustomer.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaCustomer.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
        txt_AlamatCustomer.Text = win_ListLawanTransaksi.AlamatMitraTerseleksi
        'If ProsesResetForm = False And ProsesLoadingForm = False And KodeCustomer <> Kosongan And InvoiceDenganPO = True Then btn_TambahSJBAST_Click(sender, e)
    End Sub

    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_NamaCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AlamatCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_AlamatCustomer.TextChanged
    End Sub
    Private Sub txt_AlamatCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AlamatCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.TextChanged
        JenisPPN = cmb_JenisPPN.Text
        If JenisPPN = JenisPPN_NonPPN Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        If JualAsset = False Then
            txt_TarifPPh.Text = Kosongan
            txt_PPhDitanggung.Text = Kosongan
        End If
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub

    Private Sub cmb_PerlakuanPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_PerlakuanPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PerlakuanPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PerlakuanPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.TextChanged
        PerlakuanPPN = cmb_PerlakuanPPN.Text
        If JualAsset = False Then
            txt_TarifPPh.Text = Kosongan
            txt_PPhDitanggung.Text = Kosongan
            If JenisProduk_Induk = JenisProduk_Barang Then
                If PerlakuanPPN = PerlakuanPPN_Dipungut Then
                    cmb_JenisPPh.Text = JenisPPh_Pasal22_Lokal
                    lbl_PPhDitanggung.Enabled = False
                    txt_PPhDitanggung.Enabled = False
                    txt_PPhDitanggung.Text = Kosongan
                Else
                    cmb_JenisPPh.Text = JenisPPh_NonPPh
                End If
            End If
        End If
        Perhitungan()
    End Sub

    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        COADebet = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If AmbilAngka(COADebet) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COADebet) <= kodeakun_Bank_Akhir _
            Then
            grb_Bank.Enabled = True
            PembayaranViaBank = True
            KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
            Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
    End Sub
    Private Sub cmb_SaranaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SaranaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Dim SaranaPembayaran
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim PembayaranViaBank As Boolean
    Dim JumlahTransfer
    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Enabled = False
        cmb_DitanggungOleh.Enabled = False
        txt_BiayaAdministrasiBank.Text = Kosongan
        txt_JumlahTransfer.Text = Kosongan
        KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
    End Sub

    Public KodeMataUang = KodeMataUang_IDR
    Sub Perhitungan_ValueBank()
        Dim TotalBank As Int64 = 0
        Dim JumlahMutasiBankCash As Int64 = TotalTagihan
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_IN, JumlahMutasiBankCash, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
    End Sub

    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.Text = Kosongan
        Else
            cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
        End If
        Perhitungan_ValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.SelectedIndexChanged
    End Sub
    Private Sub cmb_DitanggungOleh_TextChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.TextChanged
        DitanggungOleh = cmb_DitanggungOleh.Text
        Perhitungan_ValueBank()
    End Sub
    Private Sub cmb_DitanggungOleh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DitanggungOleh.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTransfer_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransfer.TextChanged
        JumlahTransfer = AmbilAngka(txt_JumlahTransfer.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransfer)
    End Sub
    Private Sub txt_JumlahTransfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransfer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi_TabelProduk()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrutProduk_Terseleksi = DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value
        JenisProdukPerItem_Terseleksi = DataTabelUtama.Item("Jenis_Produk_Per_Item", Baris_Terseleksi).Value
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST_Produk", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST_Produk", Baris_Terseleksi).Value
        TanggalDiterimaSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_Diterima_SJ_BAST_Produk", Baris_Terseleksi).Value
        NamaProduk_Terseleksi = DataTabelUtama.Item("Nama_Produk", Baris_Terseleksi).Value
        DeskripsiProduk_Terseleksi = DataTabelUtama.Item("Deskripsi_Produk", Baris_Terseleksi).Value
        JumlahProduk_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", Baris_Terseleksi).Value)
        SatuanProduk_Terseleksi = DataTabelUtama.Item("Satuan_Produk", Baris_Terseleksi).Value
        HargaSatuan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", Baris_Terseleksi).Value)
        DiskonPerItem_Persen_Terseleksi = Microsoft.VisualBasic.Replace(DataTabelUtama.Item("Diskon_Per_Item_Persen", Baris_Terseleksi).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Per_Item_Rp", Baris_Terseleksi).Value)
        TotalHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Total_Harga", Baris_Terseleksi).Value)

        If Baris_Terseleksi >= 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick

        If DataTabelUtama.RowCount = 0 Then Return

        If Baris_Terseleksi >= 0 Then
            btn_Edit_Click(sender, e)
        End If

    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        If JenisPPN = Kosongan Then
            MsgBox("Silakan pilih 'Jenis PPN' terlebih dahulu.")
            cmb_JenisPPN.Focus()
            Return
        End If
        If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
            MsgBox("Silakan pilih 'Perlakuan PPN' terlebih dahulu.")
            cmb_PerlakuanPPN.Focus()
            Return
        End If
        frm_InputProduk_Nota.ResetForm()
        frm_InputProduk_Nota.txt_NomorUrut.Text = JumlahProduk + 1
        frm_InputProduk_Nota.FungsiForm = FungsiForm_TAMBAH
        frm_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        frm_InputProduk_Nota.InvoiceDenganPO = InvoiceDenganPO
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        frm_InputProduk_Nota.ShowDialog()
        Perhitungan()
        BersihkanSeleksi_TabelProduk()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputProduk_Nota.ResetForm()
        frm_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        frm_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        frm_InputProduk_Nota.cmb_JenisProduk.Text = JenisProdukPerItem_Terseleksi
        frm_InputProduk_Nota.NomorSJBAST = NomorSJBAST_Terseleksi
        frm_InputProduk_Nota.TanggalSJBAST = TanggalSJBAST_Terseleksi
        frm_InputProduk_Nota.TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Terseleksi
        frm_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        frm_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        frm_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        frm_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        frm_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        frm_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        frm_InputProduk_Nota.ShowDialog()
        Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        Pilihan = MessageBox.Show("Yakin akan menghapus item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        Dim NomorUrut = 0
        DataTabelUtama.Rows.Remove(DataTabelUtama.CurrentRow)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorUrut += 1
            row.Cells("Nomor_Urut").Value = NomorUrut
        Next
        Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_Catatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub dgv_SJBAST_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_SJBAST.CellContentClick
    End Sub
    Private Sub dgv_SJBAST_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_SJBAST.ColumnHeaderMouseClick
        BersihkanSeleksi_TabelSJBAST()
    End Sub
    Private Sub dgv_SJBAST_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_SJBAST.CellClick
        If dgv_SJBAST.RowCount = 0 Then Return
        BarisSJBAST_Terseleksi = dgv_SJBAST.CurrentRow.Index
        NomorSJBAST_Terseleksi = dgv_SJBAST.Item("Nomor_SJ_BAST", BarisSJBAST_Terseleksi).Value
        If BarisSJBAST_Terseleksi >= 0 Then
            btn_SingkirkanSJBAST.Enabled = True
        Else
            btn_SingkirkanSJBAST.Enabled = False
        End If
    End Sub

    Private Sub btn_TambahSJBAST_Click(sender As Object, e As EventArgs) Handles btn_TambahSJBAST.Click

        win_ListSJBAST = New wpfWin_ListSJBAST
        win_ListSJBAST.ResetForm()
        win_ListSJBAST.Sisi = win_ListSJBAST.SisiPenjualan
        win_ListSJBAST.NamaMitra_Filter = NamaCustomer
        win_ListSJBAST.FilterMitra_Aktif = False
        If KunciTanggalInvoice = True Then
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = TanggalFormatTampilan(TanggalInvoice)
        Else
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = win_ListSJBAST.TanggalSJBAST_Semua
        End If
        win_ListSJBAST.JenisProduk_Induk = JenisProduk_Induk
        win_ListSJBAST.JenisPPN = JenisPPN
        win_ListSJBAST.PerlakuanPPN = PerlakuanPPN
        win_ListSJBAST.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        If JumlahSJBAST > 0 And
            (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi) Then
            'Untuk Saat ini, hanya produk 'Barang dan Jasa' dan 'Jasa Kontstruksi' yang menggunakan filter ini.
            'Kemungkinannya, nanti semua jenis produk harus difilter seperti ini.
            'Nunggu kepastian dari pa Aris dulu.
            'Kalau sudah OK bahwa semuanya harus difilter seperti ini, maka Logikanya cukup 'Jumlah BAST > 0' saja.
            win_ListSJBAST.NomorPO_HarusSama = True
            win_ListSJBAST.NomorPO_YangHarusDisamakan = NomorPO_TerakhirDitambahkan
        End If
        'Tambahkan nomor SJBAST yang sudah ada ke daftar singkiran :
        For Each row As DataGridViewRow In dgv_SJBAST.Rows
            If row.Cells("Nomor_SJ_BAST").Value IsNot Nothing Then
                win_ListSJBAST.ListNomorSJBAST_Singkirkan.Add(row.Cells("Nomor_SJ_BAST").Value.ToString())
            End If
        Next
        win_ListSJBAST.ShowDialog()                                             '<---- Buka Form List SJ/BAST
        NomorSJBAST = win_ListSJBAST.NomorSJBAST_Terseleksi
        If String.IsNullOrEmpty(NomorSJBAST) Then Return
        NomorSJBAST_TerakhirDitambahkan = NomorSJBAST
        NomorPO_TerakhirDitambahkan = win_ListSJBAST.NomorPO_Terseleksi
        TanggalSJBAST = win_ListSJBAST.TanggalSJBAST_Terseleksi
        TanggalDiterimaSJBAST = win_ListSJBAST.TanggalDiterima_Terseleksi
        NomorPOProduk = Kosongan
        If JumlahSJBAST > 0 Then
            If win_ListSJBAST.NomorPO_Terseleksi <> dgv_SJBAST.Item("Nomor_PO", 0).Value Then
                MsgBox("Nomor Surat Jalan / BAST yang berbeda PO tidak dapat ditambahkan ke dalam daftar..!")
                Return
            End If
        End If
        dgv_SJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, win_ListSJBAST.NomorPO_Terseleksi, win_ListSJBAST.OngkosKirim_Terseleksi)        '<-- Penambahan Baris SJ/BAST
        If (JenisProduk_Induk = JenisProduk_Jasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi) And JenisInvoice = JenisInvoice_Biasa Then
            btn_TambahSJBAST.Enabled = False
        End If
        BersihkanSeleksi_TabelSJBAST()
        Dim Tabel = Kosongan
        Dim Kolom = Kosongan
        Select Case AmbilTeksKiri(NomorSJBAST, 2)
            Case "SJ"
                Tabel = "tbl_Penjualan_SJ"
                Kolom = "Nomor_SJ"
            Case "BA"
                Tabel = "tbl_Penjualan_BAST"
                Kolom = "Nomor_BAST"
        End Select
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM " & Tabel &
                              " WHERE " & Kolom & " = '" & NomorSJBAST & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = DataTabelUtama.RowCount
        Do While dr.Read
            NomorUrutProduk += 1
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            NomorPOProduk = dr.Item("Nomor_PO_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk_PerItem = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                         " WHERE Nomor_PO = '" & NomorPOProduk & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            JenisProdukPerItem = drTELUSUR.Item("Jenis_Produk_Per_Item")
            KodeProjectProduk = drTELUSUR.Item("Kode_Project_Produk")
            HargaSatuan = drTELUSUR.Item("Harga_Satuan")
            Dim JumlahHarga_PerItem As Int64 = JumlahProduk_PerItem * HargaSatuan
            DiskonPerItem_Persen = FormatUlangDesimal_Prosentase(drTELUSUR.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHarga_PerItem * (DiskonPerItem_Persen / 100)
            Dim TotalHarga_PerItem As Int64 = JumlahHarga_PerItem - DiskonPerItem_Rp
            DataTabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPOProduk, NamaProduk, DeskripsiProduk,
                                    JumlahProduk_PerItem, SatuanProduk, HargaSatuan, JumlahHarga_PerItem,
                                    DiskonPerItem_Persen, DiskonPerItem_Rp, TotalHarga_PerItem, KodeProjectProduk)
            DPPJasa_BerdasarkanPO = drTELUSUR.Item("DPP_Jasa")
            JenisPPh = drTELUSUR.Item("Jenis_PPh")
            TarifPPh = FormatUlangDesimal_Prosentase(drTELUSUR.Item("Tarif_PPh"))
            PPhTerutang = drTELUSUR.Item("PPh_Terutang")
            PPhDitanggung_BerdasarkanPO = drTELUSUR.Item("PPh_Ditanggung")
        Loop
        cmd = New OdbcCommand(" SELECT * FROM " & Tabel &
                              " WHERE " & Kolom & " = '" & NomorSJBAST & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
        Else
            pesan_AdaKesalahanTeknis_Database(Kosongan)
            Return
        End If
        JudulForm = "Input Invoice Penjualan - " & JenisProduk_Induk
        Me.Text = JudulForm
        cmb_JenisPPN.Text = JenisPPN
        cmb_PerlakuanPPN.Text = PerlakuanPPN
        cmb_JenisPPh.Text = JenisPPh
        txt_TarifPPh.Text = TarifPPh
        txt_PPhTerutang.Text = PPhTerutang
        'Untuk Value PPh Ditanggung, ada di Sub Perhitungan. Jangan ditempatkan di sini.
        'Untuk value Biaya Transportasi Penjualan, sudah ada di Sub Perhitungan tersendiri.
        txt_BiayaTransportasiPenjualan.Text = BiayaTransportasiPenjualan
        lbl_JenisPPN.Enabled = False
        lbl_PerlakuanPPN.Enabled = False
        cmb_JenisPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        If JenisInvoice = JenisInvoice_Biasa Then
            EksekusiKode = False
            dtp_TanggalInvoice.Value = TanggalDiterimaSJBAST
            EksekusiKode = True
            KunciTanggalInvoice = True
        Else
            KunciTanggalInvoice = False
        End If
        AksesDatabase_Transaksi(Tutup)
        CekNomorSJBAST()
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
        Perhitungan()
    End Sub

    Private Sub btn_SingkirkanSJBAST_Click(sender As Object, e As EventArgs) Handles btn_SingkirkanSJBAST.Click
        Dim NomorSJBAST_UntukDihapus = dgv_SJBAST.Item("Nomor_SJ_BAST", BarisSJBAST_Terseleksi).Value
        dgv_SJBAST.Rows.Remove(dgv_SJBAST.CurrentRow)
        BersihkanSeleksi_TabelSJBAST()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Nomor_SJ_BAST_Produk").Value = NomorSJBAST_UntukDihapus Then
                BarisUntukDihapus.Add(row)
            End If
        Next
        For Each row As DataGridViewRow In BarisUntukDihapus
            DataTabelUtama.Rows.Remove(row)
        Next
        NomorUrutProduk = 0
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorUrutProduk += 1
            row.Cells("Nomor_Urut").Value = NomorUrutProduk
        Next
        BersihkanSeleksi_TabelProduk()
        If JumlahSJBAST = 0 Then
            JenisPPN = Kosongan
            PerlakuanPPN = Kosongan
            cmb_JenisPPN.Text = Kosongan
            KontenCombo_PerlakuanPPN_Kosongan()
            lbl_JenisPPN.Enabled = True
            cmb_JenisPPN.Enabled = True
            KunciTanggalInvoice = False
            btn_TambahSJBAST.Enabled = True
        End If
        Perhitungan()
    End Sub


    Sub CekNomorSJBAST()
        JumlahSJBAST = 0
        For Each row As DataGridViewRow In dgv_SJBAST.Rows
            JumlahSJBAST += 1
        Next
        If JenisProduk_Induk = JenisProduk_Jasa Then
            If JumlahSJBAST > 0 Then
                btn_TambahSJBAST.Enabled = False
            Else
                btn_TambahSJBAST.Enabled = True
            End If
        End If
        If JumlahSJBAST > 0 Then
            dtp_TanggalInvoice.Enabled = False
        Else
            dtp_TanggalInvoice.Enabled = True
        End If
    End Sub


    Private Sub txt_JumlahHargaKeseluruhan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHargaKeseluruhan.TextChanged
        JumlahHargaKeseluruhan = AmbilAngka(txt_JumlahHargaKeseluruhan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHargaKeseluruhan)
    End Sub
    Private Sub txt_JumlahHargaKeseluruhan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHargaKeseluruhan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As EventArgs) Handles txt_Diskon_Persen.TextChanged
        If txt_Diskon_Persen.Text = "," Then
            txt_Diskon_Persen.Text = Kosongan
            Return
        End If
        If txt_Diskon_Persen.Text = Kosongan Then
            Diskon_Persen = 0
        Else
            Diskon_Persen = txt_Diskon_Persen.Text
        End If
        If Diskon_Persen > 100 Then
            MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
            txt_Diskon_Persen.Text = Kosongan
            txt_Diskon_Persen.Focus()
            Return
        End If
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub

    Private Sub txt_DiskonPerItem_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Diskon_Persen.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
    End Sub

    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As EventArgs) Handles txt_Diskon_Rp.TextChanged
        Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
        PemecahRibuanUntukTextBox(txt_Diskon_Rp)
    End Sub
    Private Sub txt_Diskon_Rp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Diskon_Rp.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DPPBarang_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPBarang.TextChanged
        DPPBarang = AmbilAngka(txt_DPPBarang.Text)
        PemecahRibuanUntukTextBox(txt_DPPBarang)
    End Sub
    Private Sub txt_DPPBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPBarang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DPPJasa_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPJasa.TextChanged
        DPPJasa = AmbilAngka(txt_DPPJasa.Text)
        PemecahRibuanUntukTextBox(txt_DPPJasa)
    End Sub
    Private Sub txt_DPPJasa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPJasa.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
        PemecahRibuanUntukTextBox(txt_DasarPengenaanPajak)
    End Sub
    Private Sub txt_DasarPengenaanPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DasarPengenaanPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TarifPPN_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPN.TextChanged
        TarifPPN = AmbilAngka(txt_TarifPPN.Text)
        Perhitungan()
    End Sub
    Private Sub txt_TarifPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPN.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As EventArgs) Handles txt_PPN.TextChanged
        PPN = AmbilAngka(txt_PPN.Text)
        PemecahRibuanUntukTextBox(txt_PPN)
    End Sub
    Private Sub txt_PPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalTagihan_Kotor_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalTagihan_Kotor.TextChanged
        TotalTagihan_Kotor = AmbilAngka(txt_TotalTagihan_Kotor.Text)
        PemecahRibuanUntukTextBox(txt_TotalTagihan_Kotor)
    End Sub
    Private Sub txt_TotalTagihan_Kotor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalTagihan_Kotor.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPh_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh.TextChanged
        JenisPPh = cmb_JenisPPh.Text
        If JenisPPh = Kosongan Or JenisPPh = JenisPPh_NonPPh Then
            txt_TarifPPh.Enabled = False
            txt_TarifPPh.Text = Kosongan
        Else
            txt_TarifPPh.Enabled = True
        End If
    End Sub
    Private Sub cmb_JenisPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPh.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPh.TextChanged
        If txt_TarifPPh.Text = "," Then
            txt_TarifPPh.Text = Kosongan
            Return
        End If
        If txt_TarifPPh.Text = Kosongan Then
            TarifPPh = 0
        Else
            TarifPPh = txt_TarifPPh.Text
        End If
        If TarifPPh > 100 Then
            MsgBox("Silakan isi kolom 'Tarif PPh' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        txt_PPhTerutang.Text = DPP * Persen(TarifPPh)
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub
    Private Sub txt_TarifPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh.KeyPress
        If KunciInputanTarifPPh = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaDesimalPlus(sender, e)
        End If
    End Sub

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
        PemecahRibuanUntukTextBox(txt_PPhTerutang)
    End Sub
    Private Sub txt_PPhTerutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhTerutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung)
        Perhitungan()
    End Sub
    Private Sub txt_PPhDitanggung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung.KeyPress
        If InvoiceDenganPO = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaPlus(sender, e)
        End If
    End Sub
    Private Sub txt_PPhDitanggung_Leave(sender As Object, e As EventArgs) Handles txt_PPhDitanggung.Leave
        'PesanUntukProgrammer("Ini normalkan kembali...!!!")
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong)
    End Sub
    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDipotong.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_BiayaTransportasiPenjualan_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaTransportasiPenjualan.TextChanged
        BiayaTransportasiPenjualan = AmbilAngka(txt_BiayaTransportasiPenjualan.Text)
        PemecahRibuanUntukTextBox(txt_BiayaTransportasiPenjualan)
        EksekusiPerhitungan_BiayaTransportasi = False
        Perhitungan()
        EksekusiPerhitungan_BiayaTransportasi = True
    End Sub

    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalTagihan.TextChanged
        TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
        PemecahRibuanUntukTextBox(txt_TotalTagihan)
    End Sub
    Private Sub txt_TotalTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalTagihan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Variabel-variabel Tertentu :
        Catatan = txt_Catatan.Text      '(Jangan dihapus..!)
        JenisPPN = cmb_JenisPPN.Text    '(Jangan dihapus..!)
        JenisPPh = cmb_JenisPPh.Text    '(Jangan dihapus..!)

        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.Value
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.Value
        End If
        TanggalLapor = TanggalKosong

        If NP = Kosongan Then
            PesanUntukProgrammer("Ada masalah...! Value NP = 'Kosongan'...!" & Enter2Baris & "Perbaiki Coding-nya...!!!")
            Return
        End If

        If JenisPPN = Kosongan Then
            MsgBox("Silakan pilih 'Jenis PPN'.")
            cmb_JenisPPN.Focus()
            Return
        End If

        If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
            MsgBox("Silakan pilih 'Perlakuan PPN'.")
            cmb_PerlakuanPPN.Focus()
            Return
        End If

        If JenisPenjualan = JenisPenjualan_Tunai Then
            If SaranaPembayaran = Kosongan Then
                MsgBox("Silakan pilih 'Sarana Pembayaran'.")
                cmb_SaranaPembayaran.Focus()
                Return
            Else
                COADebet = Microsoft.VisualBasic.Left(SaranaPembayaran, JumlahDigitCOA)
                '(Pengisian Ulang Value COADebet, untuk antisipasi)
            End If
            If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
                MsgBox("Silakan pilih 'Ditanggung Oleh'.")
                cmb_DitanggungOleh.Focus()
                Return
            End If
        Else
            LogikaCOADebet_UntukNonTunai()
            '(Pengisian Ulang Value COADebet, untuk antisipasi)
        End If

        If InvoiceDenganPO = False Then

            If JualAsset = False And KodeProjectProduk = Kosongan Then
                MsgBox("Silakan isi kolom 'Kode Project'.")
                Return
            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN Then
                If JenisPPN = JenisPPN_NonPPN Then
                    AdaPenyimpananjurnal = True
                Else
                    AdaPenyimpananjurnal = False
                End If
            End If

            If FungsiForm = FungsiForm_EDIT Then
                If NomorJV > 0 Then
                    AdaPenyimpananjurnal = True
                Else
                    AdaPenyimpananjurnal = False
                End If
            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then AdaPenyimpananjurnal = False

        If JumlahSJBAST = 0 And InvoiceDenganPO = True Then
            MsgBox("Silakan input 'Surat Jalan / BAST'.")
            btn_TambahSJBAST.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            MsgBox("Silakan tambahkan data 'Barang/Jasa'.")
            btn_Tambah.Focus()
            Return
        End If

        If KodeCustomer = Nothing Then
            MsgBox("silakan isi data 'Customer'.")
            Return
        End If

        If rdb_JumlahHariJatuhTempo.Checked = False And rdb_TanggalJatuhTempo.Checked = False Then
            MsgBox("Silakan isi kolom 'Jatuh Tempo'.")
            Return
        End If

        If rdb_JumlahHariJatuhTempo.Checked = True Then
            If JumlahHariJatuhTempo = 0 Then
                MsgBox("Silakan isi kolom 'Jumlah Hari'.")
                txt_JumlahHariJatuhTempo.Focus()
                Return
            End If
            TanggalJatuhTempo = TanggalKosongSimpan
        Else
            TanggalJatuhTempo = TanggalFormatTampilan(dtp_TanggalJatuhTempo.Value)
        End If

        If JualAsset = True Then Asset = 1
        If JualAsset = False Then Asset = 0

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If (FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN) _
            And AdaPenyimpananjurnal = True _
            Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            'Hapus Data Invoice yang lama :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice " &
                                       " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()

            'Hapus Data Jurnal Penjualan yang Lama :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                       " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()
            jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_Invoice")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = DataTabelUtama.Item("Jenis_Produk_Per_Item", NomorUrutProduk - 1).Value
                NomorSJBAST = DataTabelUtama.Item("Nomor_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                TanggalSJBAST = DataTabelUtama.Item("Tanggal_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                TanggalDiterimaSJBAST = DataTabelUtama.Item("Tanggal_Diterima_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                NomorPOProduk = DataTabelUtama.Item("Nomor_PO_Produk", NomorUrutProduk - 1).Value
                NamaProduk = DataTabelUtama.Item("Nama_Produk", NomorUrutProduk - 1).Value
                DeskripsiProduk = DataTabelUtama.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value
                JumlahProduk_PerItem = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", NomorUrutProduk - 1).Value)
                SatuanProduk = DataTabelUtama.Item("Satuan_Produk", NomorUrutProduk - 1).Value
                HargaSatuan = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", NomorUrutProduk - 1).Value)
                DiskonPerItem_Persen = Microsoft.VisualBasic.Replace(DataTabelUtama.Item("Diskon_Per_Item_Persen", NomorUrutProduk - 1).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHarga = AmbilAngka(DataTabelUtama.Item("Total_Harga", NomorUrutProduk - 1).Value)
                QueryPenyimpanan = " INSERT INTO tbl_Penjualan_Invoice VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaInvoice & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & JenisInvoice & "', " &
                    " '" & NomorPenjualan & "', " &
                    " '" & Referensi & "', " &
                    " '" & NP & "', " &
                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                    " '" & TanggalFormatSimpan(TanggalPembetulan) & "', " &
                    " '" & TanggalFormatSimpan(TanggalLapor) & "', " &
                    " '" & JumlahHariJatuhTempo & "', " &
                    " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                    " '" & JenisProduk_Induk & "', " &
                    " '" & KodeCustomer & "', " &
                    " '" & NamaCustomer & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProdukPerItem & "', " &
                    " '" & NomorSJBAST & "', " &
                    " '" & TanggalFormatSimpan(TanggalSJBAST) & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterimaSJBAST) & "', " &
                    " '" & NomorPOProduk & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHarga & "', " &
                    " '" & JumlahHargaKeseluruhan & "', " &
                    " '" & Diskon_Rp & "', " &
                    " '" & DPP & "', " &
                    " '" & NomorFakturPajak & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & DesimalFormatSimpan(TarifPPN) & "', " &
                    " '" & PPN & "', " &
                    " '" & JenisPPh & "', " &
                    " '" & KodeSetoran & "', " &
                    " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                    " '" & PPhTerutang & "', " &
                    " '" & PPhDitanggung & "', " &
                    " '" & PPhDipotong & "', " &
                    " '" & TotalTagihan & "', " &
                    " '" & JumlahPiutangUsaha & "', " &
                    " '" & JenisPenjualan & "', " &
                    " '" & COADebet & "', " &
                    " '" & SaranaPembayaran & "', " &
                    " '" & BiayaAdministrasiBank & "', " &
                    " '" & DitanggungOleh & "', " &
                    " '" & BiayaTransportasiPenjualan & "', " &
                    " '" & ReturDPP & "', " &
                    " '" & ReturPPN & "', " &
                    " '" & Catatan & "', " &
                    " '" & Asset & "', " &
                    " '" & NomorJV & "', " &
                    " '" & Kosongan & "', " &
                    " '" & TanggalKosongSimpan & "', " &
                    " '" & Kosongan & "', " &
                    " '" & 0 & "', " &
                    " '" & UserAktif & "', " &
                    " '" & Koreksi & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit Do
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If

        'Jika ada perubahan Nomor Invoice : Rename Nomor Invoice yang ada di Data Retur
        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_EDIT And NomorInvoiceLama <> NomorInvoice Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Retur " &
                                      " SET Nomor_Invoice_Produk = '" & NomorInvoice & "', " &
                                      " Tanggal_Invoice_Produk = '" & TanggalFormatSimpan(TanggalInvoice) & "' " &
                                      " WHERE Nomor_Invoice_Produk = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        '====================================================================================
        '   PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                       |
        '====================================================================================

        If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0

        If StatusSuntingDatabase = True _
            And JenisTahunBuku = JenisTahunBuku_NORMAL _
            And AdaPenyimpananjurnal = True _
            Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
            If JualAsset = False Then jur_JenisJurnal = JenisJurnal_Penjualan
            If JualAsset = True Then jur_JenisJurnal = JenisJurnal_Asset
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoice 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
            jur_NomorInvoice = NomorInvoice
            jur_NomorFakturPajak = NomorFakturPajak
            jur_KodeLawanTransaksi = KodeCustomer
            jur_NamaLawanTransaksi = NamaCustomer
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                                  " WHERE Kode_Closing = '" & NomorPenjualan & "' ",
                                  KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                KelompokHarta = KonversiAngkaKeKelompokHarta(dr.Item("Kelompok_Harta"))
            End If
            AksesDatabase_General(Tutup)

            If JualAsset = True Then
                If KelompokHarta = KelompokHarta_Tanah Then
                    COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetTanahBangunan
                Else
                    COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetLainnya
                End If
            End If

            If JualAsset = False Then COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanBarang_Manufaktur

            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then COAJasa = KodeTautanCOA_PenjualanJasaKonstruksi
            If JenisProduk_Induk <> JenisProduk_JasaKonstruksi Then COAJasa = KodeTautanCOA_PenjualanJasa

            If FungsiForm = FungsiForm_TAMBAH _
                Or FungsiForm = FungsiForm_EDIT _
                Then

                If JenisPenjualan = JenisPenjualan_Tunai Then
                    JumlahDebet = TotalTagihan
                    JumlahPiutangUsaha = 0
                Else
                    JumlahDebet = JumlahPiutangUsaha
                    PPhTerutang = 0
                    PPhDipotong = 0
                    'Penjelasan : PPh Terutang dan PPh Dipotong untuk transaksi Tempo (Non-Tunai)...
                    '...nanti dimunculkannya saat Jurnal Pencairan Piutang secara proporsional.
                End If

                'Eliminasi Baris Jurnal Tertentu :
                If JualAsset = False Then PPhTerutang = 0

                'Simpan Jurnal :
                ___jurDebetBankCashIN(DitanggungOleh, COADebet, JumlahDebet, JumlahTransfer, BiayaAdministrasiBank)
                ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang)
                ___jurDebet(PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh), PPhDipotong) '(PPh Prepaid)
                _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang)
                _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN)
                _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang)
                _______jurKredit(COAJasa, DPPJasa)
                _______jurKredit(KodeTautanCOA_PenjualanLainnya, BiayaTransportasiPenjualan)

            End If

            If FungsiForm = FungsiForm_PEMBETULAN Then

                'PENYESUAIAN VALUE :
                Dim TanggalInvoiceLama = Kosongan
                Dim DPPBarang_InvoiceLama
                Dim DPPJasa_InvoiceLama
                Dim PPN_InvoiceLama
                Dim PPhTerutang_InvoiceLama
                Dim BiayaTransportasiPenjualan_InvoiceLama
                Dim TotalTagihanKotor_InvoiceLama
                Dim DPPBarang_Selisih
                Dim DPPJasa_Selisih
                Dim PPN_Selisih
                Dim PPhTerutang_Selisih
                Dim BiayaTransportasiPenjualan_Selisih
                Dim TotalTagihanKotor_Selisih

                'Ambil Value dari Data Lama :
                AksesDatabase_Transaksi(Buka)
                cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                           " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ",
                                           KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                DPPBarang_InvoiceLama = 0
                DPPJasa_InvoiceLama = 0
                PPN_InvoiceLama = 0
                PPhTerutang_InvoiceLama = 0
                BiayaTransportasiPenjualan_InvoiceLama = 0
                TotalTagihanKotor_InvoiceLama = 0
                Do While dr.Read()
                    TanggalInvoiceLama = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
                        DPPBarang_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    Else
                        DPPJasa_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    End If
                    PPN_InvoiceLama = dr.Item("PPN")
                    PPhTerutang_InvoiceLama = dr.Item("PPh_Terutang")
                    BiayaTransportasiPenjualan_InvoiceLama = dr.Item("Biaya_Transportasi")
                    TotalTagihanKotor_InvoiceLama = DPPBarang_InvoiceLama + DPPJasa_InvoiceLama + PPN_InvoiceLama
                Loop
                AksesDatabase_Transaksi(Tutup)
                jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoiceLama)

                'Pengisian Value Selisih :
                DPPBarang_Selisih = DPPBarang_InvoiceLama - DPPBarang
                DPPJasa_Selisih = DPPJasa_InvoiceLama - DPPJasa
                PPN_Selisih = PPN_InvoiceLama - PPN
                PPhTerutang_Selisih = PPhTerutang_InvoiceLama - PPhTerutang
                BiayaTransportasiPenjualan_Selisih = BiayaTransportasiPenjualan_InvoiceLama - BiayaTransportasiPenjualan
                TotalTagihanKotor_Selisih = TotalTagihanKotor_InvoiceLama - TotalTagihan_Kotor

                'Jika Value Baru Lebih Besar atau Sama dengan Value Lama : (Positif atau Nol)
                If TotalTagihan_Kotor >= TotalTagihanKotor_InvoiceLama Then

                    'Pembalikan Value Minus (-) Menjadi Plus (+)
                    TotalTagihanKotor_Selisih = 0 - TotalTagihanKotor_Selisih
                    PPhTerutang_Selisih = 0 - PPhTerutang_Selisih
                    PPN_Selisih = 0 - PPN_Selisih
                    DPPBarang_Selisih = 0 - DPPBarang_Selisih
                    DPPJasa_Selisih = 0 - DPPJasa_Selisih
                    BiayaTransportasiPenjualan_Selisih = 0 - BiayaTransportasiPenjualan_Selisih

                    PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

                    'Eliminasi Beberapa Baris Jurnal :
                    If Not (JualAsset = True And (PPhTerutang > 0 Or JumlahPiutangUsaha = TotalTagihanKotor_InvoiceLama)) Then PPhTerutang_Selisih = 0
                    If Not (PPN > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then PPN_Selisih = 0
                    If Not (DPPBarang > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPBarang_Selisih = 0
                    If Not (DPPJasa > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPJasa_Selisih = 0
                    If Not (BiayaTransportasiPenjualan > 0) Then BiayaTransportasiPenjualan_Selisih = 0

                    'Simpan Jurnal:
                    ___jurDebet(KodeTautanCOA_PiutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)
                    ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)
                    _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
                    _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
                    _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
                    _______jurKredit(COAJasa, DPPJasa_Selisih)
                    _______jurKredit(KodeTautanCOA_PenjualanLainnya, BiayaTransportasiPenjualan_Selisih)

                End If

                'Jika Value Baru Lebih Kecil dari Value Lama : (Negatif)
                If TotalTagihan_Kotor < TotalTagihanKotor_InvoiceLama Then

                    PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

                    'Eliminasi Beberapa Baris Jurnal :
                    If Not (JualAsset = True) Then PPhTerutang_Selisih = 0

                    'Simpan Jurnal :
                    ___jurDebet(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
                    ___jurDebet(COAJasa, DPPJasa_Selisih)
                    ___jurDebet(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
                    ___jurDebet(KodeTautanCOA_PenjualanLainnya, BiayaTransportasiPenjualan_Selisih)
                    ___jurDebet(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
                    _______jurKredit(KodeTautanCOA_PiutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)
                    _______jurKredit(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)

                End If

            End If

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                jur_StatusPenyimpananJurnal_Lengkap = True
            Else
                jur_StatusPenyimpananJurnal_Lengkap = False
            End If

            ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        End If

        If StatusSuntingDatabase = True Then
            frm_InvoicePenjualan.TampilkanData()
            frm_BukuPenjualan.TampilkanData()
            If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
            If AdaPenyimpananjurnal = True Then
                If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
                If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            Else
                If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
                If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            End If
            If JualAsset = True Then
                win_JualAsset.NomorPenjualanAsset = NomorPenjualan
            Else
                Pilihan = MessageBox.Show("Apakah Anda ingin mencetaknya..?", "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbYes Then btn_Cetak_Click(sender, e)
            End If
            ResetForm()
            PenyimpananInvoicePenjualan = True
            Me.Close()
        Else
            PenyimpananInvoicePenjualan = False
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        frm_Cetak.FungsiForm = FungsiForm_CETAK
        IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        NomorInvoicePenjualan_Cetak = NomorInvoice
        TampilkanHalamanCetak_InvoicePenjualan()
    End Sub

End Class