Imports bcomm
Imports System.Data.Odbc

Public Class frm_Input_InvoicePembelian

    Public JudulForm
    Public FungsiForm
    Public NomorID
    Public InvoiceDenganPO As Boolean

    Public NomorJV
    Public COAKredit
    Public JumlahKredit
    Public COAOngkosKirim
    Public COAPPN

    Public MetodePembayaran

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    Dim JenisWP
    Dim LokasiWP
    Dim JenisJasa

    Dim EksekusiPerhitungan_OngkosKirim As Boolean

    Public KunciTanggalInvoice As Boolean

    Dim KunciInputanTarifPPh As Boolean

    'Variabel Kolom :
    Public AngkaInvoice
    Dim NomorInvoice
    Dim NomorInvoiceLama
    Dim JenisInvoice
    Public NomorPembelian
    Public Referensi
    Public NP
    Public TanggalInvoice
    Dim TanggalDiterimaInvoice
    Public TanggalPembetulan
    Public TanggalLapor
    Dim JumlahHariJatuhTempo
    Dim TanggalJatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterimaSJBAST
    Dim KodeSupplier
    Dim NamaSupplier
    Dim JumlahHargaKeseluruhan As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPP As Int64                                '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPBarang As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa As Int64                            '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa_BerdasarkanPO As Int64              '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public NomorFakturPajak
    Dim TanggalFakturPajak
    Dim TarifPPN As Decimal
    Dim PPN As Int64                                '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan_Kotor As Int64                 '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JenisPPh
    Dim KodeSetoran
    Dim TarifPPh As Decimal
    Dim PPhTerutang As Int64                        '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDitanggung As Int64                      '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDipotong As Int64                        '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDitanggung_BerdasarkanPO As Int64        '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim OngkosKirim As Int64                        '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim OngkosKirim_SimpanJurnal As Int64           '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim BiayaMaterai As Int64                       '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan As Int64                       '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturDPP As Int64                           '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturPPN As Int64                           '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JumlahHutangUsaha
    Dim Catatan
    Dim KelompokAsset As Integer
    Dim KodeDivisiAsset As String
    Dim COABiaya
    Dim MasaAmortisasi



    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim COAProduk_PerItem
    Dim JenisProdukPerItem
    Dim NomorPOProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem As Integer
    Dim SatuanProduk
    Dim HargaSatuan As Int64
    Dim DiskonPerItem_Persen As Decimal
    Dim DiskonPerItem_Rp As Int64
    Dim TotalHarga_PerItem As Int64

    'Variabel Tabel Index :
    Public Baris_Terseleksi '(Ini harus public)
    Dim NomorUrutProduk_Terseleksi
    Dim KodeProjectProduk_Terseleksi
    Dim COAProduk_Terseleksi
    Dim COAProdukTerseleksi_Sebelumnya
    Dim JenisProdukPerItem_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi
    Dim KelompokAsset_Terseleksi As Integer
    Dim KodeDivisiAsset_Terseleksi As String
    Dim COABiaya_Terseleksi As String
    Dim MasaAmortisasi_Terseleksi

    'Variabel Tabel SJBAST - Index :
    Dim BarisSJBAST_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim TanggalDiterimaSJBAST_Terseleksi

    Dim JumlahProduk
    Dim NomorSJBAST_TerakhirDitambahkan
    Dim NomorPO_TerakhirDitambahkan

    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Dim Koreksi

    Dim PemilihanJenisPPh_Otomatis As Boolean

    Dim KodeProjectProduk

    Dim BiayaBiayaTambahan
    Dim BiayaKeperluanKantor

    Dim JumlahProdukProduksiKeseluruhan
    Dim JumlahProdukAdministrasiKeseluruhan
    Dim JumlahProdukAssetKeseluruhan

    Dim JumlahHargaAssetKeseluruhan
    Dim OngkosKirimAssetKeseluruhan
    Dim OngkosKirimAsset_PerItem

    Private WithEvents TabelSusunCOA As New DataGridView

    Dim SupplierSebagaiPKP As Boolean

    Dim PPNDikreditkan
    Dim PilihanPPN

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        KontenCombo_JenisPPh()

        If FungsiForm = FungsiForm_TAMBAH Then
            AngkaInvoice = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Pembelian_Invoice", "Angka_Invoice") + 1
            NomorPembelian = AwalanPEMB_PlusTahunBuku & AngkaInvoice
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                JenisProduk_Induk = Kosongan
                frm_PilihJenisProdukInduk.ShowDialog()
                JenisProduk_Induk = frm_PilihJenisProdukInduk.JenisProduk_Induk
                If frm_PilihJenisProdukInduk.Lanjutkan = False Then BeginInvoke(Sub() Me.Close())
            End If
            If (JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua) _
                And InvoiceDenganPO = False _
                Then
                PesanUntukProgrammer("Jenis Produk belum ditentukan..!")
                BeginInvoke(Sub() Me.Close())
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua Then
                JudulForm = "Input Invoice Pembelian"
            Else
                JudulForm = "Input Invoice Pembelian - " & JenisProduk_Induk
            End If
            NP = "N"
            BeginInvoke(Sub() txt_NomorInvoice.Focus())
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_PEMBETULAN _
            Then
            JudulForm = "Edit Invoice Pembelian - " & JenisProduk_Induk
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                  " WHERE Kode_Mitra = '" & KodeSupplier & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            txt_AlamatSupplier.Text = dr.Item("Alamat")
            AksesDatabase_General(Tutup)
            NomorInvoiceLama = NomorInvoice
            lbl_JenisPPN.Enabled = False
            lbl_PerlakuanPPN.Enabled = False
            cmb_JenisPPN.Enabled = False
            cmb_PerlakuanPPN.Enabled = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            dtp_TanggalInvoice.Enabled = False
            dtp_TanggalDiterimaInvoice.Enabled = False
        End If

        If FungsiForm = FungsiForm_PEMBETULAN Then
            JudulForm = "Pembetulan Invoice Pembelian - " & JenisProduk_Induk
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
                txt_NomorInvoice.Text = Replace(NomorInvoiceLama, NPLama, NP)
            End If
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            btn_Simpan.Enabled = False
            btn_Batal.Text = teks_Tutup
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("FungsiForm belum ditentukan..!!!")
        If MetodePembayaran = Kosongan Then PesanUntukProgrammer("Metode Pembayaran belum ditentukan...!!!")

        If NP = "N" Then
            lbl_TanggalInvoice.Text = "Tanggal Invoice"
        Else
            lbl_TanggalInvoice.Text = "Tanggal Pembetulan"
        End If

        VisibilitasJenisJasa()

        Me.Text = JudulForm

        If InvoiceDenganPO = True Then
            grb_Produk.Visible = False
        Else
            grb_Produk.Visible = True
        End If

        If InvoiceDenganPO = True Then

            If PPhTerutang = 0 Then
                EksekusiKode = False
                cmb_JenisPPh.Text = JenisPPh_NonPPh
                EksekusiKode = True
            End If

        End If

        If InvoiceDenganPO = False Then

            If JenisProduk_Induk = JenisProduk_Barang Then
                NonAktifkanPPh()
            Else
                AktifkanPPh()
            End If

        End If

        StyleTabelUtama(DataTabelUtama)
        StyleTabelUtama(dgv_SJBAST)

        BeginInvoke(Sub() BersihkanSeleksi_TabelProduk())
        BeginInvoke(Sub() BersihkanSeleksi_TabelSJBAST())

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            txt_Referensi.Text = dr.Item("Referensi")
            txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            cmb_JenisJasa.Text = dr.Item("Jenis_Jasa")
            cmb_JenisPPN.Text = dr.Item("Jenis_PPN")
            cmb_PerlakuanPPN.Text = dr.Item("Perlakuan_PPN")
            cmb_PPNDikreditkan.Text = dr.Item("PPN_Dikreditkan")
            cmb_PilihanPPN.Text = dr.Item("Pilihan_PPN")
            cmb_JenisPPh.Text = dr.Item("Jenis_PPh")
            cmb_KodeSetoran.Text = dr.Item("Kode_Setoran")
            txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            txt_TotalTagihan.Text = dr.Item("Total_Tagihan")
            JenisPembelian = dr.Item("Jenis_Pembelian")
            If JenisPembelian = JenisPembelian_Tunai Then KondisiForm_JenisPembelianTunai()
            dtp_TanggalDiterimaInvoice.Value = dr.Item("Tanggal_Diterima_Invoice")
            cmb_SaranaPembayaran.Text = dr.Item("Sarana_Pembayaran")
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            txt_BiayaMaterai.Text = dr.Item("Biaya_Materai")
            cmb_DitanggungOleh.Text = dr.Item("Ditanggung_Oleh")
            txt_OngkosKirim.Text = dr.Item("Biaya_Transportasi")
        End If
        AksesDatabase_Transaksi(Tutup)


        ProsesLoadingForm = False

        If FungsiForm <> FungsiForm_TAMBAH Then Perhitungan()

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        NomorJV = 0

        NomorInvoiceLama = Kosongan

        MetodePembayaran = Kosongan

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan
        JenisPPh = Kosongan

        KunciTanggalInvoice = False

        NomorID = 0
        AngkaInvoice = 0
        txt_NomorInvoice.Text = Kosongan
        NP = Kosongan
        dtp_TanggalInvoice.Value = Today
        TanggalInvoice = Today 'Ini jangan dihapus. Biarkan saja.
        dtp_TanggalDiterimaInvoice.Value = dtp_TanggalInvoice.Value 'Sudah Benar Begini..! Jangan Diganti 'Today'
        rdb_JumlahHariJatuhTempo.Checked = False
        rdb_TanggalJatuhTempo.Checked = False
        txt_JumlahHariJatuhTempo.Enabled = False
        lbl_JumlahHariJatuhTempo.Enabled = False
        txt_JumlahHariJatuhTempo.Text = Kosongan
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        KondisiForm_JenisPembelianTempo()
        txt_OngkosKirim.Text = Kosongan
        dtp_TanggalInvoice.Enabled = True
        dtp_TanggalDiterimaInvoice.Enabled = True
        dtp_TanggalJatuhTempo.Value = Today
        txt_Referensi.Text = Kosongan
        KontenCombo_JenisInvoice()
        txt_KodeSupplier.Text = Kosongan
        txt_NamaSupplier.Text = Kosongan
        txt_AlamatSupplier.Text = Kosongan
        KontenCombo_JenisJasa()
        KontenCombo_JenisPPN()
        cmb_JenisPPN.Text = Kosongan            'Ini jangan dihapus...! Penting..!
        KontenCombo_PerlakuanPPN_Kosongan()
        KontenCombo_PPNDikreditkan()
        KontenCombo_PilihanPPN()
        txt_Catatan.Text = Kosongan
        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_DPPBarang.Text = Kosongan
        txt_DPPJasa.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalTagihan_Kotor.Text = Kosongan
        cmb_JenisPPh.Text = Kosongan
        cmb_KodeSetoran.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        KunciInputanTarifPPh = True
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_OngkosKirim.Text = Kosongan
        txt_BiayaMaterai.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan
        txt_JumlahHutangUsaha.Text = Kosongan
        btn_Simpan.Enabled = True
        btn_Batal.Text = teks_Tutup
        ReturDPP = 0
        ReturPPN = 0
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()

        Koreksi = Kosongan
        NomorSJBAST_TerakhirDitambahkan = Kosongan
        NomorPO_TerakhirDitambahkan = Kosongan

        'StyleTabelPembantu(TabelSusunCOA)  [Padahal ini tidak perlu. Kenapa dulu dibikin ya..? Apa alasannya..?]
        TabelSusunCOA.Columns.Clear()
        TabelSusunCOA.Rows.Clear()
        TabelSusunCOA.Columns.Add("COA_Debet", "")
        TabelSusunCOA.Columns.Add("Jumlah_Debet", "")

        EksekusiPerhitungan_OngkosKirim = True

        KodeDivisiAsset = Kosongan
        KelompokAsset = 0
        COABiaya = Kosongan

        JumlahProdukAssetKeseluruhan = 0
        OngkosKirimAssetKeseluruhan = 0
        OngkosKirimAsset_PerItem = 0

        BiayaBiayaTambahan = 0
        BiayaKeperluanKantor = 0

        NomorSJBAST_Aktif = Kosongan
        TanggalSJBAST_Aktif = Kosongan
        TanggalDiterimaSJBAST_Aktif = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_JenisInvoice()
        cmb_JenisInvoice.Items.Clear()
        cmb_JenisInvoice.Items.Add(JenisInvoice_Biasa)
        'Untuk Jenis Invoice Gabungan, hanya baru bisa digunakan untuk Jenis Produk Induk : Barang
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisInvoice.Items.Add(JenisInvoice_Gabungan)
        cmb_JenisInvoice.Text = JenisInvoice_Biasa
    End Sub

    Sub KontenCombo_JenisJasa()
        KontenCombo_JenisJasa_Public(cmb_JenisJasa, LokasiWP)
    End Sub

    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        If SupplierSebagaiPKP = True Then
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
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
        NonAktifkanFakturPajak()
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisPPh.Text = JenisPPh_NonPPh
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        'cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)          '(Untuk Invoice Pembelian, hanya ada Perlakuan PPN Dibayar)
        'cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)     '(Untuk Invoice Pembelian, hanya ada Perlakuan PPN Dibayar)
        cmb_PerlakuanPPN.Text = PerlakuanPPN_Dibayar
        AktifkanFakturPajak()
    End Sub

    Sub AktifkanFakturPajak()
        lbl_NomorFakturPajak.Enabled = True
        txt_NomorFakturPajak.Enabled = True
        lbl_TanggalFakturPajak.Enabled = True
        dtp_TanggalFakturPajak.Enabled = True
    End Sub
    Sub NonAktifkanFakturPajak()
        lbl_NomorFakturPajak.Enabled = False
        txt_NomorFakturPajak.Enabled = False
        lbl_TanggalFakturPajak.Enabled = False
        dtp_TanggalFakturPajak.Enabled = False
        txt_NomorFakturPajak.Text = Kosongan
        dtp_TanggalFakturPajak.Value = Today
    End Sub


    Sub KontenCombo_PPNDikreditkan()
        If PerusahaanSebagaiPKP = False Then Return
        If JenisPPN = Kosongan Or JenisPPN = JenisPPN_NonPPN Then
            lbl_PPNDikreditkan.Enabled = False
            cmb_PPNDikreditkan.Enabled = False
            cmb_PPNDikreditkan.Text = Kosongan
        End If
        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_Include Then
            lbl_PPNDikreditkan.Enabled = True
            cmb_PPNDikreditkan.Enabled = True
            cmb_PPNDikreditkan.Items.Clear()
            cmb_PPNDikreditkan.Items.Add(Pilihan_Ya)
            cmb_PPNDikreditkan.Items.Add(Pilihan_Tidak)
            cmb_PPNDikreditkan.Text = (Pilihan_Ya)
        End If
    End Sub

    Sub KontenCombo_PilihanPPN()
        If PerusahaanSebagaiPKP = False Then Return
        If PPNDikreditkan = Pilihan_Tidak Then
            lbl_PilihanPPN.Enabled = True
            cmb_PilihanPPN.Enabled = True
            cmb_PilihanPPN.Items.Clear()
            cmb_PilihanPPN.Items.Add(PilihanPPN_Dikapitalisasi)
            cmb_PilihanPPN.Items.Add(PilihanPPN_Dibiayakan)
            cmb_PilihanPPN.Text = (PilihanPPN_Dikapitalisasi)
        End If
        If PPNDikreditkan = Pilihan_Ya Or PPNDikreditkan = Kosongan Then
            lbl_PilihanPPN.Enabled = False
            cmb_PilihanPPN.Enabled = False
            cmb_PilihanPPN.Text = Kosongan
        End If
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        If JenisProduk_Induk = JenisProduk_Barang Then
            NonAktifkanPPh()
            cmb_JenisPPh.Text = JenisPPh_NonPPh
        Else
            AktifkanPPh()
        End If
    End Sub

    Sub KontenCombo_KodeSetoran()
        If PemilihanJenisPPh_Otomatis = True Then Return
        KontenCombo_KodeSetoran_Public(cmb_KodeSetoran, JenisPPh)
    End Sub

    Sub VisibilitasJenisJasa()
        If JenisProduk_Induk = JenisProduk_Jasa Or JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            lbl_JenisJasa.Visible = True
            cmb_JenisJasa.Visible = True
        Else
            lbl_JenisJasa.Visible = False
            cmb_JenisJasa.Visible = False
        End If
    End Sub

    Sub AktifkanPPh()
        cmb_JenisPPh.Enabled = True
        cmb_KodeSetoran.Enabled = True
        lbl_PersenPPh.Enabled = True
        lbl_PPhDitanggung.Enabled = True
        lbl_PPhDipotong.Enabled = True
        txt_PPhTerutang.Enabled = True
        txt_PPhDitanggung.Enabled = True
        txt_PPhDipotong.Enabled = True
    End Sub

    Sub NonAktifkanPPh()
        cmb_JenisPPh.Enabled = False
        cmb_KodeSetoran.Enabled = False
        lbl_PersenPPh.Enabled = False
        lbl_PPhDitanggung.Enabled = False
        lbl_PPhDipotong.Enabled = False
        txt_PPhTerutang.Enabled = False
        txt_PPhDitanggung.Enabled = False
        txt_PPhDipotong.Enabled = False
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
    End Sub

    Sub LogikaCOAKredit_UntukNonTunai()
        If JenisJasa = JenisJasa_Dividen Or JenisJasa = JenisJasa_LabaPajakBUT Then
            COAKredit = KodeTautanCOA_HutangPemegangSaham
        Else
            If MitraSebagaiAfiliasi(KodeSupplier) Then
                COAKredit = KodeTautanCOA_HutangUsaha_Afiliasi
            Else
                COAKredit = KodeTautanCOA_HutangUsaha_NonAfiliasi
            End If
        End If
        If JenisProduk_Induk = JenisProduk_Barang Then
            If MitraSebagaiAfiliasi(KodeSupplier) Then
                COAKredit = KodeTautanCOA_HutangUsaha_Afiliasi
            Else
                COAKredit = KodeTautanCOA_HutangUsaha_NonAfiliasi
            End If
        End If
    End Sub

    Sub Perhitungan()

        JumlahProduk = DataTabelUtama.RowCount

        Algoritma_OngkosKirim()

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

        If JenisProduk_Induk = JenisProduk_Barang And PerlakuanPPN = PerlakuanPPN_Dipungut Then
            PPhTerutang = DPP * Persen(TarifPPh)
        End If

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
                cmd = New OdbcCommand(" SELECT PPh_Ditanggung FROM tbl_Pembelian_Invoice " &
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

        BiayaKeperluanKantor = BiayaMaterai                                     '(Untuk saat ini hanya segini). Kalau ada tambahan biaya keperluan kantor lainnya bisa ditambahkan di sini)
        BiayaBiayaTambahan = OngkosKirim + BiayaKeperluanKantor  '(Untuk saat ini hanya segini). Kalau ada tambahan biaya lainnya bisa ditambahkan di sini)

        PPhDipotong = PPhTerutang - PPhDitanggung
        txt_PPhDipotong.Text = PPhDipotong
        If FungsiForm = FungsiForm_TAMBAH Then OngkosKirim_BerdasarkanSuratJalan()
        TotalTagihan = TotalTagihan_Kotor + BiayaBiayaTambahan - PPhDipotong
        JumlahHutangUsaha = TotalTagihan_Kotor + BiayaBiayaTambahan
        If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
            txt_PPN.Text = Kosongan
            txt_TotalTagihan_Kotor.Text = Kosongan
            txt_TotalTagihan.Text = Kosongan
            txt_JumlahHutangUsaha.Text = Kosongan
        Else
            txt_PPN.Text = PPN
            txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
            txt_TotalTagihan.Text = TotalTagihan
            txt_JumlahHutangUsaha.Text = JumlahHutangUsaha
        End If

        If Diskon_Rp = 0 Then
            lbl_Diskon.Enabled = False
            txt_Diskon_Rp.Enabled = False
        Else
            lbl_Diskon.Enabled = True
            txt_Diskon_Rp.Enabled = True
        End If

        If JenisPPh = JenisPPh_NonPPh Then
            NonAktifkanPPh()
        Else
            AktifkanPPh()
        End If

        Perhitungan_ValueBank()

    End Sub

    Sub OngkosKirim_BerdasarkanSuratJalan()
        If EksekusiPerhitungan_OngkosKirim = True Then
            OngkosKirim = 0
            For Each row As DataGridViewRow In dgv_SJBAST.Rows
                OngkosKirim += AmbilAngka(row.Cells("Ongkos_Kirim").Value)
            Next
            txt_OngkosKirim.Text = OngkosKirim
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub txt_NomorInvoice_Leave(sender As Object, e As EventArgs) Handles txt_NomorInvoice.Leave
        'If NomorInvoice = Kosongan Then
        '    MsgBox("Silakan isi kolom 'Nomor Invoice'.")
        '    txt_NomorInvoice.Focus()
        '    Return
        'End If
        'Periksa/Validasi Nomor Invoice
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_Invoice FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NomorInvoice.Text = Kosongan
                MsgBox("Nomor Invoice '" & NomorInvoice & "' sudah ada." & Enter2Baris & "Silakan input Nomor Invoice yang lain.")
            End If
            AksesDatabase_Transaksi(Tutup)
        End If
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalInvoice)
        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.Value
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.Value
        End If
        KondisiFormSetelahPerubahan()
        If FungsiForm = FungsiForm_TAMBAH Then
            dtp_TanggalDiterimaInvoice.Value = TanggalInvoice
        End If
        txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(dtp_TanggalInvoice.Value) 'Ini biarkan menggunakan dtp_....!
    End Sub

    Private Sub dtp_TanggalDiterimaInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalDiterimaInvoice.ValueChanged
        KunciTanggalBulanDanTahun_TidakBolehKurangDari(dtp_TanggalDiterimaInvoice, dtp_TanggalInvoice.Value.Day, dtp_TanggalInvoice.Value.Month, dtp_TanggalInvoice.Value.Year)
        TanggalDiterimaInvoice = dtp_TanggalDiterimaInvoice.Value
        DefaultValue_TanggalJatuhTempo()
        LogikaJenisPembelian()
    End Sub

    Private Sub rdb_JumlahHariJatuhTempo_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_JumlahHariJatuhTempo.CheckedChanged
        If rdb_JumlahHariJatuhTempo.Checked = True Then
            txt_JumlahHariJatuhTempo.Enabled = True
            txt_JumlahHariJatuhTempo.Focus()
            lbl_JumlahHariJatuhTempo.Enabled = True
            dtp_TanggalJatuhTempo.Text = Kosongan
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
            DefaultValue_TanggalJatuhTempo()
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
        If JumlahHariJatuhTempo > 0 Then
            KondisiForm_JenisPembelianTempo()
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
        LogikaJenisPembelian()
    End Sub
    Private Sub dtp_TanggalJatuhTempo_Click(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.Click
        rdb_TanggalJatuhTempo.Checked = True
    End Sub
    Sub LogikaJenisPembelian()
        If TanggalJatuhTempo = TanggalDiterimaInvoice Then
            If NP = "N" Then KondisiForm_JenisPembelianTunai()
        Else
            If NP = "N" Then KondisiForm_JenisPembelianTempo()
        End If
    End Sub
    Dim JumlahHutangUsaha_Backup
    Sub KondisiForm_JenisPembelianTunai()
        JenisPembelian = JenisPembelian_Tunai
        lbl_SaranaPembayaran.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
        txt_JumlahHutangUsaha.Text = Kosongan
    End Sub
    Sub KondisiForm_JenisPembelianTempo()
        JenisPembelian = JenisPembelian_Tempo
        lbl_SaranaPembayaran.Enabled = False
        cmb_SaranaPembayaran.Enabled = False
        Reset_grb_Bank()
        cmb_SaranaPembayaran.Text = Kosongan
        LogikaCOAKredit_UntukNonTunai()
        Perhitungan()
    End Sub


    Sub DefaultValue_TanggalJatuhTempo()
        If FungsiForm = FungsiForm_TAMBAH Or ProsesResetForm = True Then
            Dim Bulan_TanggalJatuhTempo = dtp_TanggalDiterimaInvoice.Value.Month + 1
            Dim Tahun_TanggalJatuhTempo = dtp_TanggalDiterimaInvoice.Value.Year
            If Bulan_TanggalJatuhTempo = 13 Then
                Bulan_TanggalJatuhTempo = 1
                Tahun_TanggalJatuhTempo += 1
            End If
            dtp_TanggalJatuhTempo.Value = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_TanggalJatuhTempo, Tahun_TanggalJatuhTempo)
        End If
    End Sub

    Private Sub txt_Referensi_TextChanged(sender As Object, e As EventArgs) Handles txt_Referensi.TextChanged
        Referensi = txt_Referensi.Text
    End Sub

    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        COAKredit = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If AmbilAngka(COAKredit) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COAKredit) <= kodeakun_Bank_Akhir _
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
        txt_BiayaAdministrasiBank.Text = Kosongan
        txt_JumlahTransfer.Text = Kosongan
        KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
    End Sub

    Public KodeMataUang = KodeMataUang_IDR
    Sub Perhitungan_ValueBank()
        Dim TotalBank As Int64 = 0
        Dim JumlahMutasiBankCash As Int64 = TotalTagihan
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, JumlahMutasiBankCash, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
    End Sub

    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If AmbilAngka(txt_BiayaAdministrasiBank.Text) = 0 Then
            cmb_DitanggungOleh.Enabled = False
            cmb_DitanggungOleh.Text = Nothing
        Else
            cmb_DitanggungOleh.Enabled = True
            cmb_DitanggungOleh.Text = Kosongan
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

    Private Sub txt_OngkosKirim_TextChanged(sender As Object, e As EventArgs) Handles txt_OngkosKirim.TextChanged
        OngkosKirim = AmbilAngka(txt_OngkosKirim.Text)
        PemecahRibuanUntukTextBox(txt_OngkosKirim)
        Perhitungan_TanpaHitungOngkosKirimSuratJalan()
    End Sub

    Private Sub txt_BiayaMaterai_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaMaterai.TextChanged
        BiayaMaterai = AmbilAngka(txt_BiayaMaterai.Text)
        PemecahRibuanUntukTextBox(txt_BiayaMaterai)
        Perhitungan_TanpaHitungOngkosKirimSuratJalan()
    End Sub

    Sub Perhitungan_TanpaHitungOngkosKirimSuratJalan()
        EksekusiPerhitungan_OngkosKirim = False
        Perhitungan()
        EksekusiPerhitungan_OngkosKirim = True
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

    Private Sub txt_KodeSupplier_Click(sender As Object, e As EventArgs) Handles txt_KodeSupplier.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        If KodeSupplier = Kosongan Then
            cmb_JenisJasa.Enabled = False
            btn_TambahSJBAST.Enabled = False
        Else
            cmb_JenisJasa.Enabled = True
            btn_TambahSJBAST.Enabled = True
        End If
        Kosongkan_TabelSJBAST()
        AmbilValue_JenisWP_dan_LokasiWP(KodeSupplier, JenisWP, LokasiWP)
        KontenCombo_JenisJasa()
        If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then cmb_JenisJasa.Text = JenisJasa_JasaKonstruksi
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisJasa.Text = Kosongan
        PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
        SupplierSebagaiPKP = mdl_PublicSub.SupplierSebagaiPKP(KodeSupplier)
        KontenCombo_JenisPPN()
    End Sub
    Private Sub txt_KodeSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Sub Algoritma_OngkosKirim()

        OngkosKirimAssetKeseluruhan = 0
        OngkosKirimAsset_PerItem = 0
        OngkosKirim_SimpanJurnal = OngkosKirim

        JumlahProdukProduksiKeseluruhan = 0
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If AmbilAngka(AmbilTeksKiri(row.Cells("COA_Produk").Value, 1)) = 5 Then
                JumlahProdukProduksiKeseluruhan += AmbilAngka(row.Cells("Jumlah_Produk").Value)
            End If
        Next

        JumlahProdukAdministrasiKeseluruhan = 0
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If AmbilAngka(AmbilTeksKiri(row.Cells("COA_Produk").Value, 1)) = 6 Then
                JumlahProdukAdministrasiKeseluruhan += AmbilAngka(row.Cells("Jumlah_Produk").Value)
            End If
        Next

        JumlahProdukAssetKeseluruhan = 0
        JumlahHargaAssetKeseluruhan = 0
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If AmbilAngka(row.Cells("Kelompok_Asset").Value) > 0 Then
                JumlahProdukAssetKeseluruhan += AmbilAngka(row.Cells("Jumlah_Produk").Value)
                JumlahHargaAssetKeseluruhan += AmbilAngka(row.Cells("Total_Harga").Value)
            End If
        Next

        If LokasiWP = LokasiWP_DalamNegeri Then
            If JumlahProdukProduksiKeseluruhan > 0 Then
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi
            ElseIf JumlahProdukAdministrasiKeseluruhan > 0 Then
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi
            ElseIf JumlahProdukAssetKeseluruhan > 0 Then
                OngkosKirimAssetKeseluruhan = OngkosKirim
                OngkosKirim_SimpanJurnal = 0
            Else
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi
            End If
        End If

        If LokasiWP = LokasiWP_LuarNegeri Then
            COAOngkosKirim = KodeTautanCOA_BiayaTransportasi_Import
        End If

    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Mitra_Supplier
        If txt_KodeSupplier.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeSupplier.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaSupplier.Text
            win_ListLawanTransaksi.AlamatMitraTerseleksi = txt_AlamatSupplier.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeSupplier.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaSupplier.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
        txt_AlamatSupplier.Text = win_ListLawanTransaksi.AlamatMitraTerseleksi
        'If ProsesResetForm = False And ProsesLoadingForm = False And KodeSupplier <> Kosongan And InvoiceDenganPO = True Then btn_TambahSJBAST_Click(sender, e)
    End Sub

    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_NamaSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AlamatSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_AlamatSupplier.TextChanged
    End Sub
    Private Sub txt_AlamatSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AlamatSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisJasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJasa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJasa.KeyPress
    End Sub
    Private Sub cmb_JenisJasa_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa.TextChanged
        JenisJasa = cmb_JenisJasa.Text
        PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
        If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then
            If JenisJasa = JenisJasa_Dividen _
                Or JenisJasa = JenisJasa_LabaPajakBUT _
                Or JenisJasa = JenisJasa_BungaBagiHasil _
                Then
                cmb_JenisPPN.Items.Clear()
                cmb_JenisPPN.Text = JenisPPN_NonPPN
            Else
                KontenCombo_JenisPPN()
            End If
        End If
    End Sub

    Sub PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
        If ProsesResetForm = True _
            Or ProsesIsiValueForm = True _
            Or ProsesLoadingForm = True _
            Or EksekusiKode = False _
            Or KodeSupplier = Kosongan _
            Or (JenisJasa = Kosongan And JenisProduk_Induk <> JenisProduk_Barang) _
            Or JenisPPN = Kosongan _
            Then
            cmb_JenisPPh.Text = Kosongan
            cmb_KodeSetoran.Text = Kosongan
            txt_TarifPPh.Text = Kosongan
            Return
        End If
        PemilihanJenisPPh_Otomatis = True
        If JenisJasa <> Kosongan And JenisPPN <> Kosongan And KodeSupplier <> Kosongan Then
            PenentuanJenisPPhDanKodeSetoranDanTarifPPh_Public(
                LokasiWP, JenisWP, JenisJasa, cmb_JenisPPh, cmb_KodeSetoran, txt_TarifPPh)
        End If
        Perhitungan()
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
        KontenCombo_PPNDikreditkan()
        txt_TarifPPh.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        KondisiFormSetelahPerubahan()
        PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
    End Sub

    Private Sub cmb_PerlakuanPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_PerlakuanPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PerlakuanPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PerlakuanPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.TextChanged
        PerlakuanPPN = cmb_PerlakuanPPN.Text
        txt_TarifPPh.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        If JenisProduk_Induk = JenisProduk_Barang Then
            If PerlakuanPPN = PerlakuanPPN_Dipungut Then
                'AktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_Pasal22_Lokal
                lbl_PPhDitanggung.Enabled = False
                txt_PPhDitanggung.Enabled = False
                txt_PPhDitanggung.Text = Kosongan
            Else
                'NonAktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_NonPPh
            End If
        End If
        Perhitungan()
    End Sub

    Private Sub cmb_PPNDikreditkan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PPNDikreditkan.SelectedIndexChanged
    End Sub
    Private Sub cmb_PPNDikreditkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PPNDikreditkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PPNDikreditkan_TextChanged(sender As Object, e As EventArgs) Handles cmb_PPNDikreditkan.TextChanged
        PPNDikreditkan = cmb_PPNDikreditkan.Text
        KontenCombo_PilihanPPN()
    End Sub


    Private Sub cmb_PilihanPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PilihanPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_PilihanPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PilihanPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PilihanPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_PilihanPPN.TextChanged
        PilihanPPN = cmb_PilihanPPN.Text
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub dtp_TanggalFakturPajak_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalFakturPajak.ValueChanged
        TanggalFakturPajak = dtp_TanggalFakturPajak.Value
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
        KodeProjectProduk_Terseleksi = DataTabelUtama.Item("Kode_Project_Produk", Baris_Terseleksi).Value
        COAProduk_Terseleksi = DataTabelUtama.Item("COA_Produk", Baris_Terseleksi).Value
        COAProdukTerseleksi_Sebelumnya = COAProduk_Terseleksi
        NamaProduk_Terseleksi = DataTabelUtama.Item("Nama_Produk", Baris_Terseleksi).Value
        DeskripsiProduk_Terseleksi = DataTabelUtama.Item("Deskripsi_Produk", Baris_Terseleksi).Value
        JumlahProduk_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", Baris_Terseleksi).Value)
        SatuanProduk_Terseleksi = DataTabelUtama.Item("Satuan_Produk", Baris_Terseleksi).Value
        HargaSatuan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", Baris_Terseleksi).Value)
        DiskonPerItem_Persen_Terseleksi = GantiTeks(DataTabelUtama.Item("Diskon_Per_Item_Persen", Baris_Terseleksi).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Per_Item_Rp", Baris_Terseleksi).Value)
        TotalHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Total_Harga", Baris_Terseleksi).Value)
        KelompokAsset_Terseleksi = AmbilAngka(DataTabelUtama.Item("Kelompok_Asset", Baris_Terseleksi).Value)
        KodeDivisiAsset_Terseleksi = DataTabelUtama.Item("Kode_Divisi_Asset", Baris_Terseleksi).Value
        COABiaya_Terseleksi = DataTabelUtama.Item("COA_Biaya", Baris_Terseleksi).Value
        MasaAmortisasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Masa_Amortisasi", Baris_Terseleksi).Value)

        If DataTabelUtama.Columns(e.ColumnIndex).Name = "COA_Produk" Then

            win_ListCOA = New wpfWin_ListCOA
            win_ListCOA.ResetForm()
            If COAProduk_Terseleksi <> Kosongan Then
                win_ListCOA.COATerseleksi = COAProduk_Terseleksi
                win_ListCOA.txt_CariAkun.Text = COAProduk_Terseleksi
            End If
            win_ListCOA.ListAkun = ListAkun_Pembelian
            win_ListCOA.ShowDialog()
            COAProduk_Terseleksi = win_ListCOA.COATerseleksi
            DataTabelUtama.Item("COA_Produk", Baris_Terseleksi).Value = COAProduk_Terseleksi
            'Jika Termasuk COA Asset :
            If AmbilTeksKiri(COAProduk_Terseleksi, 2) = "12" And AmbilTeksKanan(COAProduk_Terseleksi, 1).ToString = "0" Then
                If COAProduk_Terseleksi <> COAProdukTerseleksi_Sebelumnya _
                    Or KelompokAsset_Terseleksi = 0 _
                    Or KodeDivisiAsset_Terseleksi = Kosongan _
                    Then
                    BukaForm_DataAsset()
                End If
            Else
                DataTabelUtama.Item("Kelompok_Asset", Baris_Terseleksi).Value = 0
                DataTabelUtama.Item("Kode_Divisi_Asset", Baris_Terseleksi).Value = Kosongan
            End If
            'Jika Termasuk COA Biaya Dibayar Dimuka :
            If AmbilTeksKiri(COAProduk_Terseleksi, 3) = "116" Then
                If COAProduk_Terseleksi <> COAProdukTerseleksi_Sebelumnya _
                    Or COABiaya_Terseleksi = Kosongan _
                    Then
                    BukaForm_DataAmortisasiBiaya()
                End If
            Else
                DataTabelUtama.Item("COA_Biaya", Baris_Terseleksi).Value = Kosongan
            End If
            'Jika Termasuk Peruntukan Project :
            If AmbilTeksKiri(COAProduk_Terseleksi, 1) = "5" Then
                If KodeProjectProduk_Terseleksi = Kosongan Then
                    BukaForm_ListDataProject()
                End If
            Else
                DataTabelUtama.Item("Kode_Project_Produk", Baris_Terseleksi).Value = Kosongan
            End If
        End If

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

    Sub BukaForm_DataAsset()
        'frm_InputDataAsset.ResetForm()
        'frm_InputDataAsset.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        'frm_InputDataAsset.FungsiForm = FungsiForm
        'frm_InputDataAsset.txt_NamaAktiva.Text = NamaProduk_Terseleksi
        'frm_InputDataAsset.txt_COA_Asset.Text = COAProduk_Terseleksi
        'frm_InputDataAsset.txt_HargaPerolehan.Text = HargaSatuan_Terseleksi - (HargaSatuan_Terseleksi * DiskonPerItem_Persen_Terseleksi / 100)
        'If COAProduk_Terseleksi = COAProdukTerseleksi_Sebelumnya Then
        '    If KelompokAsset_Terseleksi > 0 Then
        '        frm_InputDataAsset.cmb_KelompokHarta.Text = KonversiAngkaKeKelompokHarta(KelompokAsset_Terseleksi)
        '    End If
        '    If KodeDivisiAsset_Terseleksi <> Kosongan Then
        '        frm_InputDataAsset.cmb_Divisi.Text = KodeDivisiAsset_Terseleksi & " - " &
        '        AmbilValue_DivisiAsset(KodeDivisiAsset_Terseleksi)
        '    End If
        'End If
        'BeginInvoke(Sub() PesanPemberitahuan("Untuk pembelian asset, silakan lengkapi 'Data Asset' berikut ini."))
        'frm_InputDataAsset.ShowDialog()
    End Sub

    Sub BukaForm_DataAmortisasiBiaya()
        win_InputAmortisasiBiaya = New wpfWin_InputAmortisasiBiaya
        win_InputAmortisasiBiaya.ResetForm()
        win_InputAmortisasiBiaya.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputAmortisasiBiaya.FungsiForm = FungsiForm
        win_InputAmortisasiBiaya.txt_COA_Amortisasi.Text = COAProduk_Terseleksi
        win_InputAmortisasiBiaya.txt_JumlahTransaksi.Text = HargaSatuan_Terseleksi - (HargaSatuan_Terseleksi * DiskonPerItem_Persen_Terseleksi / 100)
        If COAProduk_Terseleksi = COAProdukTerseleksi_Sebelumnya Then
            If AmbilAngka(COABiaya_Terseleksi) > 0 Then
                win_InputAmortisasiBiaya.txt_COA_Biaya.Text = COABiaya_Terseleksi
            End If
            If AmbilAngka(MasaAmortisasi_Terseleksi) > 0 Then
                win_InputAmortisasiBiaya.txt_MasaAmortisasi.Text = MasaAmortisasi_Terseleksi
            End If
        End If
        PesanPemberitahuan("Untuk biaya dibayar dimuka, silakan lengkapi 'Data Amortisasi Biaya' berikut ini.")
        win_InputAmortisasiBiaya.ShowDialog()
    End Sub


    Sub BukaForm_ListDataProject()
        PesanPeringatan("Untuk COA Kepala 5 harus memilih 'Kode Project'")
        frm_ListDataProject.ResetForm()
        frm_ListDataProject.ShowDialog()
        If frm_ListDataProject.KodeProject_Terseleksi = Kosongan Then
            DataTabelUtama.Item("Kode_Project_Produk", Baris_Terseleksi).Value = Kosongan
            PesanPeringatan("Tidak ada 'Kode Project' yang dipilih." & Enter2Baris & "COA Kepala 5 dibatalkan..!")
            DataTabelUtama.Item("COA_Produk", Baris_Terseleksi).Value = Kosongan
        Else
            DataTabelUtama.Item("Kode_Project_Produk", Baris_Terseleksi).Value = frm_ListDataProject.KodeProject_Terseleksi
        End If
    End Sub



    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        CekNomorSJBAST()
        If JumlahSJBAST = 0 Then
            PesanPeringatan("Silakan isi terlebih dahulu Data Surat Jalan/BAST")
            Return
        End If
        frm_InputProduk_Nota.ResetForm()
        frm_InputProduk_Nota.txt_NomorUrut.Text = JumlahProduk + 1
        frm_InputProduk_Nota.FungsiForm = FungsiForm_TAMBAH
        frm_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        frm_InputProduk_Nota.InvoiceDenganPO = InvoiceDenganPO
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        frm_InputProduk_Nota.NomorSJBAST = NomorSJBAST_Aktif
        frm_InputProduk_Nota.TanggalSJBAST = TanggalSJBAST_Aktif
        frm_InputProduk_Nota.TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Aktif
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
        frm_InputProduk_Nota.COA_PerProduk = COAProduk_Terseleksi
        frm_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        frm_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        frm_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        frm_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        frm_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        frm_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        frm_InputProduk_Nota.InvoiceDenganPO = InvoiceDenganPO
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
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
        If InvoiceDenganPO = True Then
            TambahSJBAST_InvoiceDenganPO()
        Else
            TambahSJBAST_InvoiceTanpaPO()
        End If
        CekNomorSJBAST()
        BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_TabelSJBAST()
        Perhitungan()
    End Sub

    Sub TambahSJBAST_InvoiceDenganPO()
        win_ListSJBAST = New wpfWin_ListSJBAST
        win_ListSJBAST.ResetForm()
        win_ListSJBAST.Sisi = win_ListSJBAST.SisiPembelian
        win_ListSJBAST.NamaMitra_Filter = NamaSupplier
        win_ListSJBAST.FilterMitra_Aktif = False
        If KunciTanggalInvoice = True Then
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = TanggalInvoice
        Else
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = win_ListSJBAST.TanggalSJBAST_Semua
        End If
        win_ListSJBAST.JenisProduk_Induk = JenisProduk_Induk
        win_ListSJBAST.JenisPPN = JenisPPN
        win_ListSJBAST.PerlakuanPPN = PerlakuanPPN
        win_ListSJBAST.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
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
        Dim JenisSurat = win_ListSJBAST.JenisSurat_Terseleksi
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
        Select Case JenisSurat
            Case AwalanSJ
                Tabel = "tbl_Pembelian_SJ"
                Kolom = "Nomor_SJ"
            Case AwalanBAST
                Tabel = "tbl_Pembelian_BAST"
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
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
            DataTabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPOProduk, Kosongan, NamaProduk, DeskripsiProduk,
                                    JumlahProduk_PerItem, SatuanProduk, HargaSatuan, JumlahHarga_PerItem,
                                    DiskonPerItem_Persen, DiskonPerItem_Rp, TotalHarga_PerItem, KodeProjectProduk)
            DPPJasa_BerdasarkanPO = drTELUSUR.Item("DPP_Jasa")
            JenisJasa = drTELUSUR.Item("Jenis_Jasa")
            JenisPPh = drTELUSUR.Item("Jenis_PPh")
            KodeSetoran = drTELUSUR.Item("Kode_Setoran")
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
        JudulForm = "Input Invoice Pembelian - " & JenisProduk_Induk
        Me.Text = JudulForm
        VisibilitasJenisJasa()
        cmb_JenisJasa.Text = JenisJasa
        cmb_JenisPPN.Text = JenisPPN
        cmb_PerlakuanPPN.Text = PerlakuanPPN
        cmb_JenisPPh.Text = JenisPPh
        cmb_KodeSetoran.Text = KodeSetoran
        txt_TarifPPh.Text = TarifPPh
        txt_PPhTerutang.Text = PPhTerutang
        'Untuk Value PPh Ditanggung, ada di Sub Perhitungan. Jangan ditempatkan di sini.
        'Untuk value Biaya Transportasi Pembelian, sudah ada di Sub Perhitungan tersendiri.
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
    End Sub

    Sub TambahSJBAST_InvoiceTanpaPO()
        win_InputSJBASTManual = New wpfWin_InputSJBASTManual
        win_InputSJBASTManual.ResetForm()
        win_InputSJBASTManual.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputSJBASTManual.TanggalDiterima = TanggalInvoice
        win_InputSJBASTManual.TanggalSJBAST = TanggalInvoice
        win_InputSJBASTManual.ShowDialog()
    End Sub


    Dim NomorSJBAST_Aktif
    Dim TanggalSJBAST_Aktif
    Dim TanggalDiterimaSJBAST_Aktif
    Dim JumlahSJBAST
    Sub CekNomorSJBAST()
        NomorSJBAST_Aktif = Kosongan
        TanggalSJBAST_Aktif = Kosongan
        TanggalDiterimaSJBAST_Aktif = Kosongan
        JumlahSJBAST = 0
        For Each row As DataGridViewRow In dgv_SJBAST.Rows
            NomorSJBAST_Aktif = row.Cells("Nomor_SJ_BAST").Value
            TanggalSJBAST_Aktif = row.Cells("Tanggal_SJ_BAST").Value
            TanggalDiterimaSJBAST_Aktif = row.Cells("Tanggal_Diterima").Value
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
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
        Perhitungan()
        CekNomorSJBAST()
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
        PemilihanJenisPPh_Otomatis = False
        KontenCombo_KodeSetoran()
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

    Private Sub cmb_KodeSetoran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KodeSetoran.SelectedIndexChanged
    End Sub
    Private Sub cmb_KodeSetoran_TextChanged(sender As Object, e As EventArgs) Handles cmb_KodeSetoran.TextChanged
        KodeSetoran = cmb_KodeSetoran.Text
    End Sub
    Private Sub cmb_KodeSetoran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_KodeSetoran.KeyPress
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
        LogikaKunciInputanTarifPPh()
        If KunciInputanTarifPPh = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaDesimalPlus(sender, e)
        End If
    End Sub

    Sub LogikaKunciInputanTarifPPh()
        If JenisPPh = JenisPPh_Pasal26 Or JenisJasa = JenisJasa_Lainnya Then
            KunciInputanTarifPPh = False
        Else
            KunciInputanTarifPPh = True
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
        If PPhTerutang > 0 Then
            BukaKunciInputan(sender, e)
        Else
            KunciTotalInputan(sender, e)
        End If
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong)
    End Sub
    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDipotong.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalTagihan.TextChanged
        TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
        PemecahRibuanUntukTextBox(txt_TotalTagihan)
    End Sub
    Private Sub txt_TotalTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalTagihan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHutangUsaha.TextChanged
        JumlahHutangUsaha = AmbilAngka(txt_JumlahHutangUsaha.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHutangUsaha)
    End Sub
    Private Sub txt_JumlahHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Sub TambahBarisSusunCOA(ByRef TabelSusunCOA_COASebelumnya, ByRef TabelSusunCOA_JumlahDebet)

        'Tambah Baris COA :
        TabelSusunCOA.Rows.Add(TabelSusunCOA_COASebelumnya, TabelSusunCOA_JumlahDebet)

        'Jika ada selisih hitung karena permasalahan angka dibelakang koma, ...
        '...maka selisih tersebut dikoreksikan ke Harga Perolehan Asset paling akhir dalam satu COA.
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                                     " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                     " AND COA_Asset = '" & TabelSusunCOA_COASebelumnya & "' ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Dim HargaPerolehanAssetPerCOA As Int64 = 0
        Dim NomorIdTerakhirDitelusur As Int64 = 0
        Dim HargaPerolehanSatuan As Int64 = 0
        Do While drTELUSUR.Read
            NomorIdTerakhirDitelusur = drTELUSUR.Item("Nomor_ID")
            HargaPerolehanSatuan = drTELUSUR.Item("Harga_Perolehan")
            HargaPerolehanAssetPerCOA += HargaPerolehanSatuan
        Loop
        Dim HargaPerolehanSatuanTerakhirDitelusur = HargaPerolehanSatuan
        Dim Selisih = TabelSusunCOA_JumlahDebet - HargaPerolehanAssetPerCOA
        Dim HargaPerolehanRevisi = HargaPerolehanSatuanTerakhirDitelusur + Selisih
        If TabelSusunCOA_JumlahDebet <> HargaPerolehanAssetPerCOA Then
            cmdEDIT = New OdbcCommand(" UPDATE tbl_DataAsset SET Harga_Perolehan = '" & HargaPerolehanRevisi & "' " &
                                      " WHERE Nomor_ID = '" & NomorIdTerakhirDitelusur & "' ", KoneksiDatabaseGeneral)
            cmdEDIT_ExecuteNonQuery()
        End If
        TabelSusunCOA_JumlahDebet = 0

    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Beberapa value karena beberapa alasan. Jangan dihapus..!!!
        JenisPPN = cmb_JenisPPN.Text
        JenisPPh = cmb_JenisPPh.Text
        AmbilValue_JenisWP_dan_LokasiWP(KodeSupplier, JenisWP, LokasiWP)
        Algoritma_OngkosKirim()


        'Validasi Form:

        If NomorInvoice = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor Invoice'.")
            txt_NomorInvoice.Focus()
            Return
        End If

        If JenisPembelian = JenisPembelian_Tunai Then
            If SaranaPembayaran = Kosongan Then
                Pilihan = MessageBox.Show("Sarana Pembayaran tidak dipilih. Data invoice ini disimpan sebagai Hutang Usaha." & Enter2Baris &
                                          "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then
                    cmb_SaranaPembayaran.Focus()
                    Return
                End If
                JenisPembelian = JenisPembelian_Tempo
            Else
                COAKredit = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
                '(Pengisian Ulang Value COAKredit, untuk antisipasi)
            End If
            If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
                MsgBox("Silakan pilih 'Ditanggung Oleh'.")
                cmb_DitanggungOleh.Focus()
                Return
            End If
        Else
            LogikaCOAKredit_UntukNonTunai()
            '(Pengisian Ulang Value COAKredit, untuk antisipasi)
        End If

        If KodeSupplier = Kosongan Then
            MsgBox("silakan isi data 'Supplier'.")
            Return
        End If

        If JenisProduk_Induk <> JenisProduk_Barang And JenisJasa = Kosongan Then
            cmb_JenisJasa.Focus()
            MsgBox("silakan pilih 'Jenis Jasa'.")
            Return
        End If

        If JenisProduk_Induk <> JenisProduk_Barang Then
            If JenisPPh = Kosongan Then
                MsgBox("Silakan pilih 'Jenis PPh'.")
                cmb_JenisPPh.Focus()
                Return
            End If
            If KodeSetoran = Kosongan Then
                MsgBox("Silakan pilih 'Kode Setoran'.")
                cmb_KodeSetoran.Focus()
                Return
            End If
            If TarifPPh = 0 Then
                MsgBox("Silakan isi 'Tarif PPh'.")
                txt_TarifPPh.Focus()
                Return
            End If
        End If

        If InvoiceDenganPO = False Then

        End If

        Dim AdaCOA As Boolean = True
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If AmbilAngka(row.Cells("Nomor_Urut").Value) <> 0 And AmbilAngka(row.Cells("COA_Produk").Value) = 0 Then
                AdaCOA = False
            End If
        Next

        If AdaCOA = False Then
            MsgBox("Silakan isi kolom COA pada masing-masing baris produk.")
            DataTabelUtama.Focus()
            Return
        End If

        'Pengisian Ulang Variabel-variabel Tertentu :
        Catatan = txt_Catatan.Text
        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.Value
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.Value
        End If
        TanggalLapor = TanggalKosong
        If PerusahaanSebagaiPKP = False Then
            PPNDikreditkan = Pilihan_Tidak
            PilihanPPN = PilihanPPN_Dikapitalisasi
        End If

        Dim AdaPenyimpananjurnal As Boolean

        If NP = Kosongan Then
            PesanUntukProgrammer("Ada masalah...! Value NP = 'Kosongan'...!" & Enter2Baris & "Perbaiki Coding-nya...!!!")
            Return
        End If

        If JenisPPN <> JenisPPN_NonPPN Then
            If NomorFakturPajak = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Nomor Faktur Pajak'.")
                txt_NomorFakturPajak.Focus()
                Return
            End If
            If TanggalFakturPajak <> TanggalInvoice Then
                PesanPeringatan("Tanggal Faktur Pajak tidak sama dengan Tanggal Invoice..!")
                Return
            End If
        End If

        'Untuk saat ini, setiap input invoice Pembelian, langsung otomatis di-Jurnal.
        'Setiap kali edit, langsung perbarui Jurnal.
        'Kecuali untuk Tahun Buku Lampau. Tidak ada penjurnalan
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then AdaPenyimpananjurnal = True
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

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If (FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN) _
            And AdaPenyimpananjurnal = True _
            Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        BukaDatabaseTransaksiGeneral()

        If FungsiForm = FungsiForm_EDIT Then
            jur_NomorJV = NomorJV
            HapusDataPembelian_BerdasarkanAngkaInvoice(AngkaInvoice)
            HapusJurnal_BerdasarkanNomorJV(NomorJV)
            HapusDataAsset_BerdasarkanNomorPembelian(NomorPembelian)
        End If

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pembelian_Invoice")

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = DataTabelUtama.Item("Jenis_Produk_Per_Item", NomorUrutProduk - 1).Value
                NomorSJBAST = DataTabelUtama.Item("Nomor_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                TanggalSJBAST = DataTabelUtama.Item("Tanggal_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                TanggalDiterimaSJBAST = DataTabelUtama.Item("Tanggal_Diterima_SJ_BAST_Produk", NomorUrutProduk - 1).Value
                COAProduk_PerItem = DataTabelUtama.Item("COA_Produk", NomorUrutProduk - 1).Value
                NomorPOProduk = DataTabelUtama.Item("Nomor_PO_Produk", NomorUrutProduk - 1).Value
                NamaProduk = DataTabelUtama.Item("Nama_Produk", NomorUrutProduk - 1).Value
                DeskripsiProduk = DataTabelUtama.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value
                JumlahProduk_PerItem = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", NomorUrutProduk - 1).Value)
                SatuanProduk = DataTabelUtama.Item("Satuan_Produk", NomorUrutProduk - 1).Value
                HargaSatuan = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", NomorUrutProduk - 1).Value)
                DiskonPerItem_Persen = GantiTeks(DataTabelUtama.Item("Diskon_Per_Item_Persen", NomorUrutProduk - 1).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHarga_PerItem = AmbilAngka(DataTabelUtama.Item("Total_Harga", NomorUrutProduk - 1).Value)
                KodeDivisiAsset = DataTabelUtama.Item("Kode_Divisi_Asset", NomorUrutProduk - 1).Value
                KelompokAsset = AmbilAngka(DataTabelUtama.Item("Kelompok_Asset", NomorUrutProduk - 1).Value)
                COABiaya = DataTabelUtama.Item("COA_Biaya", NomorUrutProduk - 1).Value
                MasaAmortisasi = DataTabelUtama.Item("Masa_Amortisasi", NomorUrutProduk - 1).Value
                QueryPenyimpanan = " INSERT INTO tbl_Pembelian_Invoice VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaInvoice & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & JenisInvoice & "', " &
                    " '" & MetodePembayaran & "', " &
                    " '" & NomorPembelian & "', " &
                    " '" & Referensi & "', " &
                    " '" & NP & "', " &
                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterimaInvoice) & "', " &
                    " '" & TanggalFormatSimpan(TanggalPembetulan) & "', " &
                    " '" & TanggalFormatSimpan(TanggalLapor) & "', " &
                    " '" & JumlahHariJatuhTempo & "', " &
                    " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                    " '" & JenisProduk_Induk & "', " &
                    " '" & KodeSupplier & "', " &
                    " '" & NamaSupplier & "', " &
                    " '" & JenisJasa & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProdukPerItem & "', " &
                    " '" & NomorSJBAST & "', " &
                    " '" & TanggalFormatSimpan(TanggalSJBAST) & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterimaSJBAST) & "', " &
                    " '" & NomorPOProduk & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & COAProduk_PerItem & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHarga_PerItem & "', " &
                    " '" & JumlahHargaKeseluruhan & "', " &
                    " '" & Diskon_Rp & "', " &
                    " '" & DPP & "', " &
                    " '" & NomorFakturPajak & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & PPNDikreditkan & "', " &
                    " '" & PilihanPPN & "', " &
                    " '" & DesimalFormatSimpan(TarifPPN) & "', " &
                    " '" & PPN & "', " &
                    " '" & JenisPPh & "', " &
                    " '" & KodeSetoran & "', " &
                    " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                    " '" & PPhTerutang & "', " &
                    " '" & PPhDitanggung & "', " &
                    " '" & PPhDipotong & "', " &
                    " '" & TotalTagihan & "', " &
                    " '" & JumlahHutangUsaha & "', " &
                    " '" & JenisPembelian & "', " &
                    " '" & COAKredit & "', " &
                    " '" & SaranaPembayaran & "', " &
                    " '" & BiayaAdministrasiBank & "', " &
                    " '" & DitanggungOleh & "', " &
                    " '" & OngkosKirim & "', " &
                    " '" & BiayaMaterai & "', " &
                    " '" & ReturDPP & "', " &
                    " '" & ReturPPN & "', " &
                    " '" & Catatan & "', " &
                    " '" & KodeDivisiAsset & "', " &
                    " '" & KelompokAsset & "', " &
                    " '" & COABiaya & "', " &
                    " '" & MasaAmortisasi & "', " &
                    " '" & NomorJV & "', " &
                    " '" & Kosongan & "', " &
                    " '" & TanggalKosongSimpan & "', " &
                    " '" & Kosongan & "', " &
                    " '" & 0 & "', " &
                    " '" & UserAktif & "', " &
                    " '" & Koreksi & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()

                'Simpan Data Asset :
                If KelompokAsset > 0 Then
                    Dim KodeAsset = Kosongan
                    Dim JumlahAsset_PerItem = JumlahProduk_PerItem
                    Dim NomorUrutAsset = 0
                    Dim IdAsset = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataAsset")
                    Dim NamaAktiva = NamaProduk
                    Dim COA_Asset = COAProduk_PerItem
                    Dim COA_AkumulasiPenyusutan = PenentuanCOA_AkumulasiPenyusutan(COA_Asset)
                    Dim COA_BiayaPenyusutan = PenentuanCOA_BiayaPenyusutan(COA_Asset)
                    Dim MasaManfaat = KonversiKelompokHartaAngkaKeMasaManfaat(KelompokAsset)
                    Dim Divisi = AmbilValue_DivisiAsset(KodeDivisiAsset)
                    Dim TanggalPerolehan = TanggalFormatSimpan(TanggalInvoice)
                    OngkosKirimAsset_PerItem = TotalHarga_PerItem * OngkosKirimAssetKeseluruhan / JumlahHargaAssetKeseluruhan
                    Dim HargaPerolehan As Int64 = Fix(TotalHarga_PerItem + OngkosKirimAsset_PerItem) / JumlahProduk_PerItem
                    If PilihanPPN = PilihanPPN_Dikapitalisasi Then
                        HargaPerolehan += Fix(TotalHarga_PerItem * Persen(TarifPPN) / JumlahProduk_PerItem)
                    End If
                    Dim Keterangan = Kosongan
                    Do While NomorUrutAsset < JumlahAsset_PerItem
                        NomorUrutAsset += 1
                        IdAsset += 1
                        KodeAsset = COA_Asset & "-" & KodeDivisiAsset & "-" & AmbilTahun_DariTanggal(TanggalPerolehan) &
                        "-" & KonversiAngkaKeStringDuaDigit(AmbilBulanAngka_DariTanggal(TanggalPerolehan)) & "-" & IdAsset
                        cmd = New OdbcCommand(" INSERT INTO tbl_DataAsset VALUES ( " &
                                  " '" & IdAsset & "', " &
                                  " '" & NomorPembelian & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & NamaAktiva & "', " &
                                  " '" & COA_Asset & "', " &
                                  " '" & COA_BiayaPenyusutan & "', " &
                                  " '" & COA_AkumulasiPenyusutan & "', " &
                                  " '" & KelompokAsset & "', " &
                                  " '" & MasaManfaat & "', " &
                                  " '" & KodeDivisiAsset & "', " &
                                  " '" & Divisi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                                  " '" & HargaPerolehan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Keterangan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
                        cmd_ExecuteNonQuery()
                    Loop
                    'Hapus Data Jurnal :
                    PesanUntukProgrammer("Hapus jurnal penyusutan terkait..!!!")
                End If

                'Simpan Data Amortisasi Biaya :
                If AmbilAngka(COABiaya) > 0 Then
                    Dim IdAmortisasi
                    Dim COAAmortisasi = COAProduk_PerItem
                    Dim NamaAkunAmortisasi = AmbilValue_NamaAkun(COAAmortisasi)
                    Dim NamaAkunBiaya = AmbilValue_NamaAkun(COABiaya)
                    Dim KodeAsset
                    Dim JumlahTransaksi = TotalHarga_PerItem / JumlahProduk_PerItem
                    If FungsiForm = FungsiForm_TAMBAH Then
                        IdAmortisasi = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_AmortisasiBiaya") + 1
                        KodeAsset = COAAmortisasi & "-" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(AmbilBulanAngka_DariTanggal(TanggalInvoice)) & "-" & IdAmortisasi
                        cmd = New OdbcCommand(" INSERT INTO tbl_AmortisasiBiaya VALUES ( " &
                                  " '" & IdAmortisasi & "', " &
                                  " '" & NomorPembelian & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & COAAmortisasi & "', " &
                                  " '" & NamaAkunAmortisasi & "', " &
                                  " '" & COABiaya & "', " &
                                  " '" & NamaAkunBiaya & "', " &
                                  " '" & MasaAmortisasi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & Kosongan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
                        cmd_ExecuteNonQuery()
                    End If

                    If FungsiForm = FungsiForm_EDIT Then
                        IdAmortisasi = AmbilValue_IdAmortisasiBerdasarkanNomorPembelianDanNamaProduk(NomorPembelian, NamaProduk)
                        KodeAsset = COAAmortisasi & "-" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(AmbilBulanAngka_DariTanggal(TanggalInvoice)) & "-" & IdAmortisasi
                        cmd = New OdbcCommand(" UPDATE tbl_AmortisasiBiaya SET " &
                                  " Kode_Asset              = '" & KodeAsset & "', " &
                                  " COA_Amortisasi          = '" & COAAmortisasi & "', " &
                                  " Nama_Akun_Amortisasi    = '" & NamaAkunAmortisasi & "', " &
                                  " COA_Biaya               = '" & COABiaya & "', " &
                                  " Nama_Akun_Biaya         = '" & NamaAkunBiaya & "', " &
                                  " Masa_Amortisasi         = '" & MasaAmortisasi & "', " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Kosongan & "' " &
                                  " WHERE Nomor_ID          = '" & IdAmortisasi & "' ",
                                  KoneksiDatabaseGeneral)
                        cmd_ExecuteNonQuery()

                        'Hapus Data Jurnal :
                        PesanUntukProgrammer("Hapus jurnal penyusutan terkait..!!!")

                    End If

                End If

                If StatusKoneksiDatabase = False Then
                    TutupDatabaseTransaksiGeneral()
                    PesanUntukProgrammer("Penyimpanan bermasalah...!!!")
                    Exit Do
                End If

            Loop

            'Pengurutan Baris Tabel Berdasarkan COA :
            DataTabelUtama.Sort(DataTabelUtama.Columns("COA_Produk"), ComponentModel.ListSortDirection.Ascending)

            'Penghimpunan COA-COA yang sama ke dalam satu baris :
            Dim TabelSusunCOA_COASebelumnya = Kosongan
            Dim TabelSusunCOA_COA = Kosongan
            Dim TabelSusunCOA_JumlahDebet As Int64 = 0
            Dim Baris = 0
            For Each row As DataGridViewRow In DataTabelUtama.Rows
                Baris += 1
                TabelSusunCOA_COA = row.Cells("COA_Produk").Value
                If TabelSusunCOA_COA <> TabelSusunCOA_COASebelumnya And Baris > 1 Then
                    TambahBarisSusunCOA(TabelSusunCOA_COASebelumnya, TabelSusunCOA_JumlahDebet)
                End If
                TotalHarga_PerItem = AmbilAngka(row.Cells("Total_Harga").Value)
                If JumlahHargaAssetKeseluruhan > 0 Then
                    OngkosKirimAsset_PerItem = TotalHarga_PerItem * OngkosKirimAssetKeseluruhan / JumlahHargaAssetKeseluruhan
                Else
                    OngkosKirimAsset_PerItem = 0
                End If
                If PilihanPPN = PilihanPPN_Dikapitalisasi Then TotalHarga_PerItem += (TotalHarga_PerItem * Persen(TarifPPN))
                TabelSusunCOA_JumlahDebet += (TotalHarga_PerItem + OngkosKirimAsset_PerItem)
                TabelSusunCOA_COASebelumnya = TabelSusunCOA_COA
            Next
            TambahBarisSusunCOA(TabelSusunCOA_COASebelumnya, TabelSusunCOA_JumlahDebet)

        End If


        'Jika ada perubahan Nomor Invoice : Rename Nomor Invoice yang ada di Data Retur
        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_EDIT And NomorInvoiceLama <> NomorInvoice Then
                cmd = New OdbcCommand(" UPDATE tbl_Pembelian_Retur " &
                                      " SET Nomor_Invoice_Produk = '" & NomorInvoice & "', " &
                                      " Tanggal_Invoice_Produk = '" & TanggalFormatSimpan(TanggalInvoice) & "' " &
                                      " WHERE Nomor_Invoice_Produk = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
            End If
        End If

        TutupDatabaseTransaksiGeneral()

        '====================================================================================
        'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                          |
        '====================================================================================

        If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0

        If StatusSuntingDatabase = True _
            And JenisTahunBuku = JenisTahunBuku_NORMAL _
            And AdaPenyimpananjurnal = True _
            Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
            jur_JenisJurnal = JenisJurnal_Pembelian
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoice 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
            jur_NomorInvoice = NomorInvoice
            jur_NomorFakturPajak = NomorFakturPajak
            jur_KodeLawanTransaksi = KodeSupplier
            jur_NamaLawanTransaksi = NamaSupplier
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            If FungsiForm = FungsiForm_TAMBAH _
                Or FungsiForm = FungsiForm_EDIT _
                Then

                Select Case JenisPembelian
                    Case JenisPembelian_Tunai
                        JumlahKredit = TotalTagihan
                    Case JenisPembelian_Tempo
                        JumlahKredit = JumlahHutangUsaha
                        PPhTerutang = 0
                        PPhDitanggung = 0
                        'Penjelasan : PPh Terutang dan PPh Ditanggung untuk transaksi Tempo (Non-Tunai)...
                        '...nanti dimunculkannya saat Jurnal Pembayaran Hutang secara proporsional.
                End Select

                'Penentuan COA PPN Berdasarkan PPN Dikreditkan, Pilihan PPN dan Lokasi WP:
                Select Case PPNDikreditkan
                    Case Pilihan_Ya
                        Select Case LokasiWP
                            Case LokasiWP_DalamNegeri
                                COAPPN = KodeTautanCOA_PPNMasukan_Lokal
                            Case LokasiWP_LuarNegeri
                                COAPPN = KodeTautanCOA_PPNMasukan_Impor
                        End Select
                    Case Pilihan_Tidak
                        Select Case PilihanPPN
                            Case PilihanPPN_Dibiayakan
                                COAPPN = KodeTautanCOA_BiayaPPN
                            Case PilihanPPN_Dikapitalisasi
                                PPN = 0
                        End Select
                End Select

                'Simpan Jurnal :
                For Each row As DataGridViewRow In TabelSusunCOA.Rows
                    Dim COADebet = row.Cells("COA_Debet").Value
                    Dim JumlahDebet As Int64 = AmbilAngka(row.Cells("Jumlah_Debet").Value)
                    ___jurDebet(COADebet, JumlahDebet)
                Next
                ___jurDebet(COAPPN, PPN)
                ___jurDebet(PenentuanCOA_BiayaPPh(JenisPPh), PPhDitanggung)
                ___jurDebet(COAOngkosKirim, OngkosKirim_SimpanJurnal)
                ___jurDebet(KodeTautanCOA_BiayaPerlengkapanKantor, BiayaKeperluanKantor)
                ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
                _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang)
                _______jurKreditBankCashOUT(DitanggungOleh, COAKredit, JumlahKredit, JumlahTransfer, BiayaAdministrasiBank)

            End If

            If FungsiForm = FungsiForm_PEMBETULAN Then

                'PENYESUAIAN VALUE :
                Dim TanggalInvoiceLama = Kosongan
                Dim DPPBarang_InvoiceLama
                Dim DPPJasa_InvoiceLama
                Dim PPN_InvoiceLama
                Dim TotalTagihanKotor_InvoiceLama
                Dim DPPBarang_Selisih
                Dim DPPJasa_Selisih
                Dim PPN_Selisih
                Dim TotalTagihanKotor_Selisih

                'Ambil Value dari Data Lama :
                AksesDatabase_Transaksi(Buka)
                cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                           " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ",
                                           KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                DPPBarang_InvoiceLama = 0
                DPPJasa_InvoiceLama = 0
                PPN_InvoiceLama = 0
                TotalTagihanKotor_InvoiceLama = 0
                Do While dr.Read()
                    TanggalInvoiceLama = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
                        DPPBarang_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    Else
                        DPPJasa_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    End If
                    PPN_InvoiceLama = dr.Item("PPN")
                    TotalTagihanKotor_InvoiceLama = DPPBarang_InvoiceLama + DPPJasa_InvoiceLama + PPN_InvoiceLama
                Loop
                AksesDatabase_Transaksi(Tutup)
                jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoiceLama)

                'Pengisian Value Selisih :
                DPPBarang_Selisih = DPPBarang_InvoiceLama - DPPBarang
                DPPJasa_Selisih = DPPJasa_InvoiceLama - DPPJasa
                PPN_Selisih = PPN_InvoiceLama - PPN
                TotalTagihanKotor_Selisih = TotalTagihanKotor_InvoiceLama - TotalTagihan_Kotor

                'Jika Value Baru Lebih Besar atau Sama dengan Value Lama : (Positif atau Nol)
                If TotalTagihan_Kotor >= TotalTagihanKotor_InvoiceLama Then

                    'Pembalikan Value Minus (-) Menjadi Plus (+)
                    PPN_Selisih = 0 - PPN_Selisih
                    DPPBarang_Selisih = 0 - DPPBarang_Selisih
                    DPPJasa_Selisih = 0 - DPPJasa_Selisih
                    TotalTagihanKotor_Selisih = 0 - TotalTagihanKotor_Selisih

                    PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

                    'Eliminasi Baris Jurnal Tertentu :
                    If Not (PPN > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then PPN_Selisih = 0
                    If Not (DPPBarang > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPBarang_Selisih = 0
                    If Not (DPPJasa > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPJasa_Selisih = 0

                    'Simpan Jurnal :
                    ___jurDebet(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
                    ___jurDebet(KodeTautanCOA_Bangunan, DPPBarang_Selisih)
                    ___jurDebet(KodeTautanCOA_Bangunan, DPPJasa_Selisih)
                    _______jurKredit(KodeTautanCOA_HutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)

                End If

                'Jika Value Baru Lebih Kecil dari Value Lama : (Negatif)
                If TotalTagihan_Kotor < TotalTagihanKotor_InvoiceLama Then

                    PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

                    'Simpan Jurnal :
                    ___jurDebet(KodeTautanCOA_HutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)
                    ___jurDebet(KodeTautanCOA_Bangunan, DPPBarang_Selisih)
                    ___jurDebet(KodeTautanCOA_Bangunan, DPPJasa_Selisih)
                    _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN_Selisih)

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
            SusunUlangNomorID_DataAsset()
            frm_InvoicePembelian.RefreshTampilanData()
            'frm_BukuPembelian.RefreshTampilanData()
            'frm_BukuPengawasanHutangUsaha.RefreshTampilanData()
            'If JenisPPh = JenisPPh_Pasal21 Then frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            ''If JenisPPh = JenisPPh_Pasal22 Then frm_BukuPengawasanHutangPPhPasal22.RefreshTampilanData()
            'If JenisPPh = JenisPPh_Pasal23 Then frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'If JenisPPh = JenisPPh_Pasal42 Then frm_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            'If JenisPPh = JenisPPh_Pasal25 Then frm_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            'If JenisPPh = JenisPPh_Pasal26 Then frm_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
            ''If JenisPPh = JenisPPh_Pasal29 Then frm_BukuPengawasanHutangPPhPasal29.RefreshTampilanData()
            If AdaPenyimpananjurnal = True Then
                If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
                If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            Else
                If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
                If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            End If
            'Pilihan = MessageBox.Show("Apakah Anda ingin mencetaknya..?", "Perhatian..!", MessageBoxButtons.YesNo)
            'If Pilihan = vbYes Then btn_Cetak_Click(sender, e)
            ResetForm()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

End Class