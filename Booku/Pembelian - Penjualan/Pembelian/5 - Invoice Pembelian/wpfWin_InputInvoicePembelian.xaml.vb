Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives

Public Class wpfWin_InputInvoicePembelian

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
    Public BasisPerhitunganTermin

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN
    Public KodeMataUang
    Dim Kurs As Decimal

    Dim JenisWP
    Dim LokasiWP
    Dim JenisJasa

    Dim EksekusiPerhitungan_OngkosKirim As Boolean

    Public KunciTanggalInvoice As Boolean

    'Variabel Kolom :
    Public AngkaInvoice
    Public NomorInvoice
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
    Dim TanggalSerahTerima
    Dim Loko
    Public KodeSupplier
    Dim NamaSupplier
    Dim JumlahHargaKeseluruhan As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JumlahNota As Int64
    Dim UangMukaPlusTermin_Persen As Decimal
    Dim UangMukaPlusTermin_Rp As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim Termin_Persen As Decimal
    Dim Termin_Rp As Int64                     '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPP As Int64                                '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPBarang As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa As Int64                            '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim DPPJasa_BerdasarkanPO As Int64              '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public NomorFakturPajak
    Dim TanggalFakturPajak
    Dim NomorSKB
    Dim TanggalSKB
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
    Dim BiayaMaterai As Int64                       '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan As Int64                       '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturDPP As Int64                           '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturPPN As Int64                           '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JumlahHutangUsaha
    Dim Catatan
    Dim KelompokAsset As Integer
    Dim KodeDivisiAsset As String
    Dim COABiaya
    Dim TanggalMulaiAmortisasi
    Dim MasaAmortisasi

    Dim DPP_11Per12 As Int64
    Dim TarifPPN_11Per12

    'Asing :
    Dim HargaSatuan_Asing As Decimal
    Dim JumlahHargaKeseluruhan_Asing As Decimal
    Dim DiskonAsing As Decimal
    Dim JumlahNota_Asing As Decimal
    Dim Termin_Asing As Decimal
    Dim Insurance As Decimal
    Dim Freight As Decimal
    Dim KursKMK As Decimal
    Dim BeaMasuk As Int64
    Dim TotalTagihan_Kotor_Asing As Decimal
    Dim TotalTagihan_Asing As Decimal
    Dim JumlahHutangUsaha_Asing As Decimal
    Dim UangMukaPlusTermin_Asing As Decimal

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
    Dim TotalHargaPerItem As Int64
    Dim TotalHargaPerItem_Asing As Decimal

    'Variabel Tabel Index :
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
    Dim TanggalMulaiAmortisasi_Terseleksi
    Dim MasaAmortisasi_Terseleksi
    'Asing :
    Dim HargaSatuan_Asing_Terseleksi
    Dim JumlahHarga_Asing_Terseleksi
    Dim DiskonPerItem_Asing_Terseleksi
    Dim TotalHarga_Asing_Terseleksi

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
    'Asing :
    Dim JumlahHargaAssetKeseluruhan_Asing As Decimal
    Dim OngkosKirimAssetKeseluruhan_Asing As Decimal
    Dim OngkosKirimAsset_PerItem_Asing As Decimal

    Private WithEvents TabelSusunCOA As New DataGridView

    Dim SupplierSebagaiPKP As Boolean
    Dim SupplierSebagaiUMKM As Boolean

    Dim PPNDikreditkan
    Dim PilihanPPN

    Dim AdaPPh As Boolean

    Public TahapTermin

    Dim MataUang As String

    Dim MitraLuarNegeri

    Public AsalPembelian
    Public PembelianLokal As Boolean
    Public PembelianImpor As Boolean

    Public JenisRelasi As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True
        EksekusiLogikaAdaPPh = False

        If AsalPembelian = AsalPembelian_Lokal Then
            PembelianLokal = True
            PembelianImpor = False
        Else
            PembelianLokal = False
            PembelianImpor = True
            LogikaAdaPPh(True)
        End If

        LogikaAsalPembelian()

        If FungsiForm = FungsiForm_TAMBAH Then
            EksekusiKodeLogikaPPN = True
            AngkaInvoice = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Pembelian_Invoice", "Angka_Invoice") + 1
            NomorPembelian = AwalanPEMB_PlusTahunBuku & AngkaInvoice
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                JenisProduk_Induk = Kosongan
                frm_PilihJenisProdukInduk.ShowDialog()
                JenisProduk_Induk = frm_PilihJenisProdukInduk.JenisProduk_Induk
                If frm_PilihJenisProdukInduk.Lanjutkan = False Then Me.Close()
            End If
            If (JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua) _
                And InvoiceDenganPO = False _
                Then
                PesanUntukProgrammer("Jenis Produk belum ditentukan..!")
                Me.Close()
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua Then
                JudulForm = "Input Invoice Pembelian"
            Else
                JudulForm = "Input Invoice Pembelian - " & JenisProduk_Induk
            End If
            NP = "N"
            If MetodePembayaran = MetodePembayaran_Termin Then VisibilitasTabelSJBAST(False)
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_PEMBETULAN _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            NomorInvoiceLama = NomorInvoice
            dtp_TanggalInvoice.IsEnabled = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Invoice Pembelian - " & JenisProduk_Induk
            dtp_TanggalInvoice.IsEnabled = False
            dtp_TanggalDiterimaInvoice.IsEnabled = False
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
                NPLama = "P" & AmbilAngka_TanpaMinus(Right(NomorInvoiceLama, 3))
                PembetulanKe = AmbilAngka_TanpaMinus(Right(NomorInvoiceLama, 3)) + 1
                NP = "P" & PembetulanKe
                txt_NomorInvoice.Text = Replace(NomorInvoiceLama, NPLama, NP)
            End If
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            JudulForm = "Invoice Pembelian - " & JenisProduk_Induk
            btn_Batal.Content = teks_Tutup
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("FungsiForm belum ditentukan..!!!")
        If MetodePembayaran = Kosongan Then PesanUntukProgrammer("Metode Pembayaran belum ditentukan...!!!")

        If NP = "N" Then
            lbl_TanggalInvoice.Text = "Tanggal Invoice"
        Else
            lbl_TanggalInvoice.Text = "Tanggal Pembetulan"
        End If

        LogikaVisibilitasJenisJasa()

        Title = JudulForm

        LogikaTampilanKolomDPPBarangDanJasa()

        btn_Tambahkan.Visibility = Visibility.Visible
        If MetodePembayaran = MetodePembayaran_Normal Then
            VisibilitasBarisPO(False)
            VisibilitasTabelSJBAST(True)
            If InvoiceDenganPO Then
                If PembelianLokal Then pnl_CRUDProduk.Visibility = Visibility.Collapsed
                If PembelianImpor Then
                    pnl_CRUDProduk.Visibility = Visibility.Visible
                    btn_Tambahkan.Visibility = Visibility.Collapsed
                    PesanUntukProgrammer("Visibilitas CRUD Item Produk masih perlu dikaji mengenai algoritma Jumlah yang sudah dikirim dan sisanya...!")
                End If
            Else
                pnl_CRUDProduk.Visibility = Visibility.Visible
            End If
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            VisibilitasBarisPO(True)
            pnl_CRUDProduk.Visibility = Visibility.Collapsed
            If TahapTermin = TahapTermin_UangMuka Then VisibilitasTabelSJBAST(False)
        End If

        BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_TabelSJBAST()

        If InvoiceDenganPO = True Then

            If PPhTerutang = 0 Then
                EksekusiKode = False
                KosongkanItemCombo(cmb_JenisPPh)
                EksekusiKode = True
            End If

        End If

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            txt_Referensi.Text = dr.Item("Referensi")
            txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            If InvoiceDenganPO Then
                IsiValueComboBypassTerkunci(cmb_JenisJasa, dr.Item("Jenis_Jasa"))
                IsiValueComboBypassTerkunci(cmb_JenisPPN, dr.Item("Jenis_PPN"))
                IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, dr.Item("Perlakuan_PPN"))
            Else
                KontenCombo_JenisJasa()
                cmb_JenisJasa.SelectedValue = dr.Item("Jenis_Jasa")
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then IsiValueComboBypassTerkunci(cmb_JenisJasa, JenisJasa_JasaKonstruksi)
                KontenCombo_JenisPPN()
                cmb_JenisPPN.SelectedValue = dr.Item("Jenis_PPN")
                cmb_PerlakuanPPN.SelectedValue = dr.Item("Perlakuan_PPN")
            End If
            cmb_KodeMataUang.SelectedValue = dr.Item("Kode_Mata_Uang")
            txt_Kurs.Text = dr.Item("Kurs")
            If MitraLuarNegeri Then
                txt_NomorPO.Text = dr.Item("Nomor_PO_Produk")
            End If
            cmb_PPNDikreditkan.SelectedValue = dr.Item("PPN_Dikreditkan")
            cmb_PilihanPPN.SelectedValue = dr.Item("Pilihan_PPN")
            If PerlakuanPPN = PerlakuanPPN_Dibebaskan Then
                txt_NomorSKB.Text = dr.Item("Nomor_SKB")
                dtp_TanggalSKB.SelectedDate = dr.Item("Tanggal_SKB")
            End If
            'Coding Baru Terkait Termin : ---------------------------------------------------------------------------
            txt_JumlahNota.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Jumlah_Harga_Keseluruhan"))
            If MetodePembayaran = MetodePembayaran_Termin Then
                txt_NomorPO.Text = dr.Item("Nomor_PO_Produk")
                TahapTermin = dr.Item("Tahap_Termin")
                lbl_Termin.Text = TahapTermin
                BasisPerhitunganTermin = dr.Item("Basis_Perhitungan_Termin")
                Select Case BasisPerhitunganTermin
                    Case BasisPerhitunganTermin_Prosentase
                        Termin_Persen = dr.Item("Termin")
                    Case BasisPerhitunganTermin_Nominal
                        If PembelianLokal Then
                            Termin_Rp = FormatUlangInt64(dr.Item("Termin"))
                            Termin_Persen = PembulatanDesimal2Digit((Termin_Rp / JumlahNota) * 100)
                        Else
                            Termin_Asing = dr.Item("Termin")
                            Termin_Persen = PembulatanDesimal2Digit((Termin_Asing / JumlahNota_Asing) * 100)
                        End If
                End Select
                PesanUntukProgrammer("Tahap Termin : " & TahapTermin & Enter2Baris &
                                     "Jumlah Nota : " & JumlahNota & Enter2Baris &
                                     "Jumlah Nota Asing : " & JumlahNota_Asing & Enter2Baris &
                                     "Termin Persen : " & Termin_Persen)
                If JenisPPN = JenisPPN_Include Then
                    Termin_Rp = dr.Item("Total_Tagihan_Kotor")
                Else
                    If PembelianLokal Then Termin_Rp = dr.Item("Dasar_Pengenaan_Pajak")
                    If PembelianImpor Then Termin_Asing = dr.Item("Total_Tagihan_Kotor")
                End If
                VisibilitasBarisTermin(True)
            End If
            txt_Termin_Persen.Text = Termin_Persen
            If PembelianLokal Then txt_Termin_Rp.Text = Termin_Rp
            If PembelianImpor Then txt_Termin_Rp.Text = Termin_Asing
            JumlahHargaKeseluruhan_Asing = JumlahNota_Asing
            'Awal Coding untuk logika baru PPN : -----------------------------------------------
            txt_JumlahNota.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Jumlah_Harga_Keseluruhan"))
            txt_Termin_Persen.Text = Termin_Persen
            If PembelianLokal Then txt_Termin_Rp.Text = Termin_Rp
            If PembelianImpor Then txt_Termin_Rp.Text = Termin_Asing
            txt_TarifPPN.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPN"))
            txt_DasarPengenaanPajak.Text = FormatUlangInt64(dr.Item("Dasar_Pengenaan_Pajak"))
            txt_TotalTagihan_Kotor.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Total_Tagihan_Kotor"))
            txt_PPN.Text = FormatUlangInt64(dr.Item("PPN"))
            txt_PPhTerutang.Text = FormatUlangInt64(dr.Item("PPh_Terutang")) 'Ini jangan dihapus...!)
            'PesanUntukProgrammer("Jenis PPN : " & JenisPPN & Enter2Baris &
            '                     "Termin Rp : " & Termin_Rp)
            'Akhir Coding untuk logika baru PPN : ----------------------------------------------
            IsiValueComboBypassTerkunci(cmb_JenisPPh, dr.Item("Jenis_PPh"))
            IsiValueComboBypassTerkunci(cmb_KodeSetoran, dr.Item("Kode_Setoran"))
            txt_TarifPPh.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPh"))
            If TarifPPh > 0 Then AdaPPh = True
            If JenisJasa = JenisJasa_Lainnya Then
                AdaPPh = True
                If TarifPPh = 0 Then txt_TarifPPh.Text = 0 'Jangan dihapus, walaupun kelihatannya seperti percuma. Ada masksudnya itu...!!!
            End If
            'PesanUntukProgrammer("Lokasi WP : " & LokasiWP)
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            txt_TotalTagihan.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Total_Tagihan"))
            JenisPembelian = dr.Item("Jenis_Pembelian")
            If JenisPembelian = JenisPembelian_Tunai Then KondisiForm_JenisPembelianTunai()
            dtp_TanggalDiterimaInvoice.SelectedDate = dr.Item("Tanggal_Diterima_Invoice")
            IsiValue_DateTimePicker_DariDatabaseMySQL("Tanggal_Serah_Terima", dtp_TanggalSerahTerima)
            cmb_Loko.SelectedValue = dr.Item("Loko")
            IsiValue_DateTimePicker_DariDatabaseMySQL("Tanggal_Faktur_Pajak", dtp_TanggalFakturPajak)
            cmb_SaranaPembayaran.SelectedValue = dr.Item("Sarana_Pembayaran")
            txt_BiayaAdministrasiBank.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Biaya_Administrasi_Bank"))
            txt_BiayaMaterai.Text = FormatUlangInt64(dr.Item("Biaya_Materai"))
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            txt_Insurance.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Insurance"))
            txt_Freight.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Freight"))
            txt_BeaMasuk.Text = FormatUlangInt64(dr.Item("Bea_Masuk"))
            txt_KursKMK.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Kurs_KMK"))
            txt_OngkosKirim.Text = FormatUlangInt64(dr.Item("Biaya_Transportasi"))
            If TahapTermin = TahapTermin_UangMuka Then VisibilitasTabelSJBAST(False)
            txt_JumlahHutangUsaha.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Jumlah_Hutang_Usaha"))
        End If
        AksesDatabase_Transaksi(Tutup)

        CekNomorSJBAST()

        ProsesLoadingForm = False

        If FungsiForm <> FungsiForm_TAMBAH Then
            EksekusiKodeLogikaPPN = False
            If TarifPPN < 10 Then
                txt_TarifPPN.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak.Visibility = Visibility.Visible
                txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
                txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            Else
                EksekusiKodeLogikaPPN = True
                LogikaTampilanPPN()
                EksekusiKodeLogikaPPN = False
            End If
            Perhitungan()
        End If

        If MetodePembayaran = MetodePembayaran_Normal Or TahapTermin = TahapTermin_Pelunasan Then
            VisibilitasKolomCOA(True)
        Else
            VisibilitasKolomCOA(False)
        End If

        EksekusiLogikaAdaPPh = True

        'PesanUntukProgrammer("Jenis Jasa : " & JenisJasa)
        'PesanUntukProgrammer("Jumlah Nota : " & JumlahNota & Enter2Baris &
        '                     "Jumlah Nota Asing : " & JumlahNota_Asing)

    End Sub

    Sub VisibilitasKolomCOA(Visibilitas)
        COA_Produk.Visibility = Visibility.Collapsed        'Harus dibikin Collapsed dulu..!
        'Baris_Total.Width = PanjangKolomTotal              'Harus dibikin Collapsed dulu..!
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                COA_Produk.Visibility = Visibility.Visible
                'Baris_Total.Width = PanjangKolomTotal + PanjangKolomCOAProduk
            End If
        Else
            COA_Produk.Visibility = Visibility.Collapsed
            'Baris_Total.Width = PanjangKolomTotal
        End If
        LogikaTampilanKolomImporTermin()
    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorJV = 0

        NomorInvoiceLama = Kosongan
        AsalPembelian = AsalPembelian_Lokal
        JenisRelasi = Pilihan_Semua

        MetodePembayaran = Kosongan

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
        KosongkanDatePicker(dtp_TanggalInvoice)
        KosongkanDatePicker(dtp_TanggalDiterimaInvoice)
        KosongkanDatePicker(dtp_TanggalSerahTerima)
        VisibilitasBarisPO(False)
        VisibilitasUangMukaPlusTermin(False)
        rdb_JumlahHariJatuhTempo.IsChecked = False
        rdb_TanggalJatuhTempo.IsChecked = False
        txt_JumlahHariJatuhTempo.IsEnabled = False
        lbl_JumlahHariJatuhTempo.IsEnabled = False
        txt_JumlahHariJatuhTempo.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
        KondisiForm_JenisPembelianTempo()
        dtp_TanggalInvoice.IsEnabled = True
        dtp_TanggalDiterimaInvoice.IsEnabled = True
        dtp_TanggalJatuhTempo.IsEnabled = False
        KosongkanDatePicker(dtp_TanggalJatuhTempo)
        txt_Referensi.Text = Kosongan
        KontenCombo_JenisInvoice()
        txt_KodeSupplier.Text = Kosongan
        txt_NamaSupplier.Text = Kosongan
        txt_AlamatSupplier.Text = Kosongan
        'KontenCombo_JenisPPN()
        cmb_JenisPPN.Text = Kosongan 'Ini jangan dihapus...! Penting..!
        KontenCombo_PerlakuanPPN_Kosongan()
        SembunyikanElemenPajak()
        txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalFakturPajak)
        lbl_NomorSKB.Visibility = Visibility.Collapsed
        txt_NomorSKB.Visibility = Visibility.Collapsed
        txt_NomorSKB.Text = Kosongan
        lbl_TanggalSKB.Visibility = Visibility.Collapsed
        dtp_TanggalSKB.Visibility = Visibility.Collapsed
        KosongkanDatePicker(dtp_TanggalSKB)
        btn_TambahSJBAST.IsEnabled = False
        VisibilitasTabelSJBAST(True)
        VisibilitasKolomSerahTerima(False)
        KosongkanValueElemenRichTextBox(txt_Catatan)
        'datagridTotal.Visibility = Visibility.Collapsed
        VisibilitasBarisDiskon(False)
        VisibilitasBarisTermin(False)
        KontenCombo_JenisPPh()
        VisibilitasElemenPPh(False)
        txt_Kurs.Text = Kosongan
        txt_TarifPPN.Text = Kosongan
        txt_BiayaMaterai.Text = Kosongan
        txt_OngkosKirim.Text = Kosongan
        txt_JumlahHutangUsaha.Text = Kosongan
        KetersediaanTombolHitung(False)
        KetersediaanTombolSimpan(False)
        btn_Batal.Content = teks_Batal
        ReturDPP = 0
        ReturPPN = 0
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()
        cmb_JenisPPh.Text = Kosongan '(Jangan dihapus..!)
        cmb_KodeSetoran.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        ReturDPP = 0
        ReturPPN = 0
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()

        Koreksi = Kosongan
        NomorSJBAST_TerakhirDitambahkan = Kosongan
        NomorPO_TerakhirDitambahkan = Kosongan

        TabelSusunCOA.Columns.Clear()
        TabelSusunCOA.Rows.Clear()
        TabelSusunCOA.Columns.Add("COA_Debet", "")
        TabelSusunCOA.Columns.Add("Jumlah_Debet", "")

        EksekusiPerhitungan_OngkosKirim = True

        TahapTermin = Kosongan
        BasisPerhitunganTermin = Kosongan

        KodeDivisiAsset = Kosongan
        KelompokAsset = 0
        COABiaya = Kosongan

        JumlahProdukAssetKeseluruhan = 0
        OngkosKirimAssetKeseluruhan = 0
        OngkosKirimAsset_PerItem = 0
        OngkosKirimAsset_PerItem_Asing = 0

        BiayaBiayaTambahan = 0

        BiayaKeperluanKantor = 0

        NomorSJBAST_Aktif = Kosongan
        TanggalSJBAST_Aktif = Kosongan
        TanggalDiterimaSJBAST_Aktif = Kosongan

        AdaPPh = False

        lbl_KodeMataUang.Visibility = Visibility.Collapsed
        cmb_KodeMataUang.Visibility = Visibility.Collapsed
        lbl_Kurs.Visibility = Visibility.Collapsed
        txt_Kurs.Visibility = Visibility.Collapsed

        lbl_TotalTagihanIDR.Visibility = Visibility.Collapsed
        txt_TotalTagihanIDR.Visibility = Visibility.Collapsed
        txt_TotalTagihanIDR.Text = Kosongan

        BukaFormPengajuanPengeluaranBankCash = False

        EksekusiKodeLogikaPPN = False
        EksekusiKodeSubPerhitungan = True

        ProsesResetForm = False

    End Sub

    Sub VisibilitasBarisPO(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_NomorPO.Visibility = Visibility.Visible
            txt_NomorPO.Visibility = Visibility.Visible
            btn_PilihPO.Visibility = Visibility.Visible
        Else
            If Not MitraLuarNegeri Then
                lbl_NomorPO.Visibility = Visibility.Collapsed
                txt_NomorPO.Visibility = Visibility.Collapsed
                btn_PilihPO.Visibility = Visibility.Collapsed
            End If
        End If
    End Sub

    Sub VisibilitasTabelSJBAST(Visibilitas As Boolean)
        lbl_SJBAST.Visibility = Visibility.Collapsed        'Harus dibikin collapsed dulu
        pnl_TombolSJBAST.Visibility = Visibility.Collapsed  'Harus dibikin collapsed dulu
        datagridSJBAST.Visibility = Visibility.Collapsed    'Harus dibikin collapsed dulu
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL And Not MitraLuarNegeri Then
                lbl_SJBAST.Visibility = Visibility.Visible
                pnl_TombolSJBAST.Visibility = Visibility.Visible
                datagridSJBAST.Visibility = Visibility.Visible
            End If
        Else
            lbl_SJBAST.Visibility = Visibility.Collapsed
            pnl_TombolSJBAST.Visibility = Visibility.Collapsed
            datagridSJBAST.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomJenisPPN(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_JenisPPN.Visibility = Visibility.Visible
            cmb_JenisPPN.Visibility = Visibility.Visible
        Else
            lbl_JenisPPN.Visibility = Visibility.Collapsed
            cmb_JenisPPN.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomSerahTerima(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_TanggalSerahTerima.Visibility = Visibility.Visible
            dtp_TanggalSerahTerima.Visibility = Visibility.Visible
            lbl_Loko.Visibility = Visibility.Visible
            cmb_Loko.Visibility = Visibility.Visible
        Else
            lbl_TanggalSerahTerima.Visibility = Visibility.Collapsed
            dtp_TanggalSerahTerima.Visibility = Visibility.Collapsed
            lbl_Loko.Visibility = Visibility.Collapsed
            cmb_Loko.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasBarisDiskon(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon_Persen.Visibility = Visibility.Visible
            lbl_PersenDiskon.Visibility = Visibility.Visible
            txt_Diskon_Rp.Visibility = Visibility.Visible
        Else
            lbl_Diskon.Visibility = Visibility.Collapsed
            txt_Diskon_Persen.Visibility = Visibility.Collapsed
            lbl_PersenDiskon.Visibility = Visibility.Collapsed
            txt_Diskon_Rp.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasBarisTermin(Visibilitas As Boolean)
        lbl_Termin.Visibility = Visibility.Collapsed
        txt_Termin_Persen.Visibility = Visibility.Collapsed
        lbl_PersenTermin.Visibility = Visibility.Collapsed
        txt_Termin_Rp.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_Termin.Visibility = Visibility.Visible
            txt_Termin_Persen.Visibility = Visibility.Visible
            lbl_PersenTermin.Visibility = Visibility.Visible
            txt_Termin_Rp.Visibility = Visibility.Visible
        End If
    End Sub

    Sub VisibilitasUangMukaPlusTermin(Visibilitas As Boolean)
        lbl_UangMukaPlusTermin.Visibility = Visibility.Collapsed
        txt_UangMukaPlusTermin_Persen.Visibility = Visibility.Collapsed
        lbl_PersenUangMukaPlusTermin.Visibility = Visibility.Collapsed
        txt_UangMukaPlusTermin_Rp.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_UangMukaPlusTermin.Visibility = Visibility.Visible
                txt_UangMukaPlusTermin_Persen.Visibility = Visibility.Visible
                lbl_PersenUangMukaPlusTermin.Visibility = Visibility.Visible
                txt_UangMukaPlusTermin_Rp.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub VisibilitasKolomKolomImport(Visibilitas As Boolean)
        If Not MitraLuarNegeri Then Return
        If Visibilitas Then
            'Kolom Kiri :
            lbl_TanggalSerahTerima.Visibility = Visibility.Visible
            lbl_Loko.Visibility = Visibility.Visible
            lbl_NomorFakturPajak.Visibility = Visibility.Visible
            lbl_TanggalFakturPajak.Visibility = Visibility.Visible
            dtp_TanggalSerahTerima.Visibility = Visibility.Visible
            cmb_Loko.Visibility = Visibility.Visible
            txt_NomorFakturPajak.Visibility = Visibility.Visible
            dtp_TanggalFakturPajak.Visibility = Visibility.Visible
            'Kolom Kanan :
            lbl_Insurance.Visibility = Visibility.Visible
            lbl_Freight.Visibility = Visibility.Visible
            lbl_BeaMasuk.Visibility = Visibility.Visible
            lbl_KursKMK.Visibility = Visibility.Visible
            lbl_DPP.Visibility = Visibility.Visible
            lbl_PPN.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            lbl_PersenPPN.Visibility = Visibility.Visible
            lbl_PPh.Visibility = Visibility.Visible
            cmb_JenisPPh.Visibility = Visibility.Visible
            cmb_KodeSetoran.Visibility = Visibility.Visible
            txt_TarifPPh.Visibility = Visibility.Visible
            lbl_PersenPPh.Visibility = Visibility.Visible
            txt_Insurance.Visibility = Visibility.Visible
            txt_Freight.Visibility = Visibility.Visible
            txt_BeaMasuk.Visibility = Visibility.Visible
            txt_KursKMK.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_PPhTerutang.Visibility = Visibility.Visible
            txt_TarifPPh.Visibility = Visibility.Visible
        Else
            'Kolom Kiri :
            lbl_TanggalSerahTerima.Visibility = Visibility.Collapsed
            lbl_Loko.Visibility = Visibility.Collapsed
            lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
            lbl_TanggalFakturPajak.Visibility = Visibility.Collapsed
            dtp_TanggalSerahTerima.Visibility = Visibility.Collapsed
            cmb_Loko.Visibility = Visibility.Collapsed
            txt_NomorFakturPajak.Visibility = Visibility.Collapsed
            dtp_TanggalFakturPajak.Visibility = Visibility.Collapsed
            'Kolom Kanan :
            lbl_Insurance.Visibility = Visibility.Collapsed
            lbl_Freight.Visibility = Visibility.Collapsed
            lbl_BeaMasuk.Visibility = Visibility.Collapsed
            lbl_KursKMK.Visibility = Visibility.Collapsed
            lbl_DPP.Visibility = Visibility.Collapsed
            lbl_PPN.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            lbl_PersenPPN.Visibility = Visibility.Collapsed
            lbl_PPh.Visibility = Visibility.Collapsed
            cmb_JenisPPh.Visibility = Visibility.Collapsed
            cmb_KodeSetoran.Visibility = Visibility.Collapsed
            txt_TarifPPh.Visibility = Visibility.Collapsed
            lbl_PersenPPh.Visibility = Visibility.Collapsed
            txt_Insurance.Visibility = Visibility.Collapsed
            txt_Freight.Visibility = Visibility.Collapsed
            txt_BeaMasuk.Visibility = Visibility.Collapsed
            txt_KursKMK.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
            txt_PPN.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            txt_PPhTerutang.Visibility = Visibility.Collapsed
            txt_TarifPPh.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub LogikaTampilanKolomImporTermin()
        If Not MitraLuarNegeri Then Return
        If MetodePembayaran = MetodePembayaran_Normal Then
            VisibilitasKolomKolomImport(True)
        Else
            If TahapTermin = TahapTermin_UangMuka Then
                VisibilitasKolomKolomImport(False)
            Else
                VisibilitasKolomKolomImport(True)
            End If
        End If
    End Sub

    Sub KontenCombo_JenisInvoice()
        cmb_JenisInvoice.Items.Clear()
        cmb_JenisInvoice.Items.Add(JenisInvoice_Biasa)
        'Untuk Jenis Invoice Gabungan, hanya baru bisa digunakan untuk Jenis Produk Induk : Barang
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisInvoice.Items.Add(JenisInvoice_Gabungan)
        cmb_JenisInvoice.SelectedValue = JenisInvoice_Biasa
    End Sub

    Sub KontenCombo_JenisJasa()
        KontenCombo_JenisJasa_Public_WPF(cmb_JenisJasa, LokasiWP)
    End Sub

    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        If SupplierSebagaiPKP Then
            cmb_JenisPPN.Items.Add(JenisPPN_Exclude)
            cmb_JenisPPN.Items.Add(JenisPPN_Include)
            cmb_JenisPPN.Text = Kosongan
            If MitraLuarNegeri Then IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN_Exclude)
        Else
            IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN_NonPPN)
            SembunyikanElemenPajak()
        End If
    End Sub

    Sub LogikaTarifPPN()
        If ProsesLoadingForm Then Return
        If Not EksekusiKodeLogikaPPN Then Return
        If dtp_TanggalInvoice.Text = Kosongan Then Return
        If (SupplierSebagaiPKP Or MitraLuarNegeri) And PerlakuanPPN <> PerlakuanPPN_Dibebaskan Then
            txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalInvoice)
        Else
            txt_TarifPPN.Text = Kosongan
        End If
    End Sub
    Sub LogikaTampilanPPN()
        If ProsesLoadingForm Then Return
        If Not EksekusiKodeLogikaPPN Then Return
        Dim TampilkanKolom As Boolean = False
        If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then TampilkanKolom = True
        If SupplierSebagaiPKP = False And MitraLuarNegeri = False Then
            txt_TarifPPN.Text = Kosongan
            txt_TarifPPN_11Per12.Text = Kosongan
            txt_PPN.Text = Kosongan
            Return
        End If
        If dtp_TanggalInvoice.Text = Kosongan Then Return
        LogikaPPN(dtp_TanggalInvoice.SelectedDate, DPP, TarifPPN, DPP_11Per12, TarifPPN_11Per12)
        'Sembunyikan dulu semuanya, baru tampilkan kolom-kolom yang seharusnya tampil, supaya tidak pusing :
        txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        txt_TarifPPN.Visibility = Visibility.Collapsed
        txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        'txt_TarifPPN.IsReadOnly = True
        If TarifPPN_11Per12 > 0 Then
            If TampilkanKolom Then
                txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Visible
            End If
            txt_TarifPPN_11Per12.Text = TarifPPN_11Per12
            If PembelianLokal Then
                If TampilkanKolom Then txt_TarifPPN_11Per12.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak_11Per12.Text = DPP_11Per12
            Else
                txt_DasarPengenaanPajak_11Per12.Text = Kosongan
            End If
        Else
            If TampilkanKolom Then txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            If PembelianLokal Then
                If TampilkanKolom Then txt_TarifPPN.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak.Text = DPP
            End If
            txt_DasarPengenaanPajak_11Per12.Text = Kosongan
        End If
        If TampilkanKolom Then
            If PembelianImpor Then
                txt_DasarPengenaanPajak.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
                txt_TarifPPN.Visibility = Visibility.Visible
                lbl_PersenPPN.Visibility = Visibility.Visible
                txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
                txt_TarifPPN.IsReadOnly = False
            End If
        End If
    End Sub

    Sub PenentuanKurs()
        If KodeMataUang = Kosongan Then Return
        If PembelianLokal Then
            txt_Kurs.Text = 1
        Else
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                txt_Kurs.Text = KursTengahBI_AkhirTahunIni(KodeMataUang)
            Else
                If Not ProsesLoadingForm Then txt_Kurs.Text = AmbilValue_KursTengahBI(KodeMataUang, TanggalInvoice)
            End If
        End If
    End Sub


    Sub SembunyikanElemenPajak()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        lbl_PPNDikreditkan.Visibility = Visibility.Collapsed
        cmb_PPNDikreditkan.Visibility = Visibility.Collapsed
        lbl_PilihanPPN.Visibility = Visibility.Collapsed
        cmb_PilihanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
        KosongkanItemCombo(cmb_PPNDikreditkan)
        KosongkanItemCombo(cmb_PilihanPPN)
        NonAktifkanFakturPajak()
        txt_TarifPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
        NonAktifkanFakturPajak()
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
        NonAktifkanFakturPajak()
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        If Not MitraLuarNegeri Then
            cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Ditanggung)
        Else
            cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibebaskan)
        End If
        'cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)              '(Untuk sisi Pembelian, hanya ada Perlakuan PPN Dibayar dan Ditanggung)
        'cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)         '(Untuk sisi Pembelian, hanya ada Perlakuan PPN Dibayar dan Ditanggung)
        cmb_PerlakuanPPN.SelectedValue = PerlakuanPPN_Dibayar
        AktifkanFakturPajak()
    End Sub

    Sub AktifkanFakturPajak()
        If Not SupplierSebagaiPKP Then Return
        lbl_NomorFakturPajak.Visibility = Visibility.Visible
        txt_NomorFakturPajak.Visibility = Visibility.Visible
        lbl_TanggalFakturPajak.Visibility = Visibility.Visible
        dtp_TanggalFakturPajak.Visibility = Visibility.Visible
    End Sub
    Sub NonAktifkanFakturPajak()
        If MitraLuarNegeri Then Return
        lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Visibility = Visibility.Collapsed
        lbl_TanggalFakturPajak.Visibility = Visibility.Collapsed
        dtp_TanggalFakturPajak.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Text = Kosongan
        dtp_TanggalFakturPajak.Text = Kosongan
    End Sub


    Sub KontenCombo_PPNDikreditkan()
        If Not PerusahaanSebagaiPKP Then Return
        If JenisPPN = Kosongan Or JenisPPN = JenisPPN_NonPPN Then
            lbl_PPNDikreditkan.Visibility = Visibility.Collapsed
            cmb_PPNDikreditkan.Visibility = Visibility.Collapsed
            KosongkanItemCombo(cmb_PPNDikreditkan)
        End If
        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_Include Then
            lbl_PPNDikreditkan.Visibility = Visibility.Visible
            cmb_PPNDikreditkan.Visibility = Visibility.Visible
            cmb_PPNDikreditkan.Items.Clear()
            cmb_PPNDikreditkan.Items.Add(Pilihan_Ya)
            cmb_PPNDikreditkan.Items.Add(Pilihan_Tidak)
            cmb_PPNDikreditkan.SelectedValue = (Pilihan_Ya)
        End If
    End Sub

    Sub KontenCombo_PilihanPPN()
        If Not PerusahaanSebagaiPKP Then Return
        If PPNDikreditkan = Pilihan_Tidak Then
            lbl_PilihanPPN.Visibility = Visibility.Visible
            cmb_PilihanPPN.Visibility = Visibility.Visible
            cmb_PilihanPPN.Items.Clear()
            cmb_PilihanPPN.Items.Add(PilihanPPN_Dikapitalisasi)
            cmb_PilihanPPN.Items.Add(PilihanPPN_Dibiayakan)
            cmb_PilihanPPN.SelectedValue = (PilihanPPN_Dikapitalisasi)
        End If
        If PPNDikreditkan = Pilihan_Ya Or PPNDikreditkan = Kosongan Then
            lbl_PilihanPPN.Visibility = Visibility.Collapsed
            cmb_PilihanPPN.Visibility = Visibility.Collapsed
            KosongkanItemCombo(cmb_PilihanPPN)
        End If
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal22_Lokal)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal22_Impor)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal26)
        cmb_JenisPPh.Items.Add(JenisPPh_NonPPh)
        cmb_JenisPPh.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeMataUangAsing()
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang)
    End Sub

    Sub KontenCombo_Loko()
        cmb_Loko.Items.Clear()
        cmb_Loko.Items.Add(Loko_PelabuhanPenjual)
        cmb_Loko.Items.Add(Loko_PelabuhanPembeli)
        cmb_Loko.Items.Add(Loko_GudangPembeli)
        cmb_Loko.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeSetoran()
        If PemilihanJenisPPh_Otomatis = True Then Return
        KontenCombo_KodeSetoran_Public_WPF(cmb_KodeSetoran, JenisPPh)
    End Sub

    Dim EksekusiLogikaAdaPPh As Boolean
    Sub LogikaAdaPPh(Ada As Boolean)
        If Not EksekusiLogikaAdaPPh Then Return
        If JenisProduk_Induk = Kosongan Then Return
        AdaPPh = False
        If JenisProduk_Induk = JenisProduk_Barang Then
            AdaPPh = False
            If AsalPembelian = AsalPembelian_Impor Then
                AdaPPh = True
                IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal22_Impor)
                IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_100)
                txt_TarifPPh.Text = 2.5
                Return
            End If
        Else
            AdaPPh = True
        End If
        If Ada = False Then AdaPPh = False
        If Not PerusahaanSebagaiPemotongPPh Then AdaPPh = False
        If InvoiceDenganPO Then
            'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
            Return '(Jika Invoice menggunakan PO, maka value-value dibawah sudah diisi dari SJ/BAST)
        End If
        If AdaPPh = True Then
            PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
        Else
            IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_NonPPh)
            IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_Non)
            txt_TarifPPh.Text = Kosongan
        End If
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub

    Sub PenentuanJenisPPhDanKodeSetoranDanTarifPPh()
        If ProsesResetForm = True _
            Or ProsesIsiValueForm = True _
            Or ProsesLoadingForm = True _
            Or EksekusiKode = False _
            Or KodeSupplier = Kosongan _
            Or JenisJasa = Kosongan _
            Or JenisPPN = Kosongan _
            Then
            KosongkanItemCombo(cmb_JenisPPh)
            KosongkanItemCombo(cmb_KodeSetoran)
            txt_TarifPPh.Text = Kosongan
            Return
        End If
        If KodeSupplier <> Kosongan And JenisJasa <> Kosongan And JenisPPN <> Kosongan Then
            PenentuanJenisPPhDanKodeSetoranDanTarifPPh_Pembelian(
                SupplierSebagaiUMKM, LokasiWP, JenisWP, JenisJasa, cmb_JenisPPh, cmb_KodeSetoran, txt_TarifPPh)
        End If
    End Sub


    Sub VisibilitasElemenPPh(Visibilitas As Boolean)
        txt_TarifPPh.Visibility = Visibility.Collapsed
        lbl_PersenPPh.Visibility = Visibility.Collapsed
        VisibilitasPPhDitanggungDipotong(False)
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then
                lbl_PPh.Visibility = Visibility.Visible
                cmb_JenisPPh.Visibility = Visibility.Visible
                cmb_KodeSetoran.Visibility = Visibility.Visible
                txt_TarifPPh.Visibility = Visibility.Visible
                lbl_PersenPPh.Visibility = Visibility.Visible
                If PembelianLokal Then
                    VisibilitasPPhDitanggungDipotong(True)
                End If
                txt_PPhTerutang.Visibility = Visibility.Visible
            End If
        Else
            lbl_PPh.Visibility = Visibility.Collapsed
            cmb_JenisPPh.Visibility = Visibility.Collapsed
            cmb_KodeSetoran.Visibility = Visibility.Collapsed
            txt_TarifPPh.Visibility = Visibility.Collapsed
            lbl_PersenPPh.Visibility = Visibility.Collapsed
            txt_PPhTerutang.Visibility = Visibility.Collapsed
            VisibilitasPPhDitanggungDipotong(False)
        End If
    End Sub

    Sub VisibilitasPPhDitanggungDipotong(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_PPhDitanggung.Visibility = Visibility.Visible
            lbl_PPhDipotong.Visibility = Visibility.Visible
            txt_PPhDitanggung.Visibility = Visibility.Visible
            txt_PPhDipotong.Visibility = Visibility.Visible
        Else
            lbl_PPhDitanggung.Visibility = Visibility.Collapsed
            lbl_PPhDipotong.Visibility = Visibility.Collapsed
            txt_PPhDitanggung.Visibility = Visibility.Collapsed
            txt_PPhDipotong.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub LogikaVisibilitasJenisJasa()
        If JenisProduk_Induk = JenisProduk_Jasa Or JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            lbl_JenisJasa.Visibility = Visibility.Visible
            cmb_JenisJasa.Visibility = Visibility.Visible
        Else
            lbl_JenisJasa.Visibility = Visibility.Collapsed
            cmb_JenisJasa.Visibility = Visibility.Collapsed
        End If
    End Sub




    Sub IsiTabelProduk()
        Dim NomorSJBAST
        Dim TanggalDiterimaSJBAST
        Dim TanggalSJBAST
        Dim JenisProdukPerItem
        Dim COAProduk
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan
        Dim JumlahHargaPerItem
        Dim DiskonPerItem_Persen As Decimal
        Dim DiskonPerItem_Rp As Int64  '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
        Dim TotalHargaPerItem
        Dim NomorPO
        Dim KodeProject
        Dim KelompokAsset
        Dim KodeDivisiAsset
        Dim COABiaya
        Dim TanggalMulaiAmortisasi
        Dim MasaAmortisasi
        Dim NomorUrut = 0
        'Asing
        Dim HargaSatuan_Asing As Decimal
        Dim JumlahHargaPerItem_Asing As Decimal
        Dim DiskonPerItem_Asing As Decimal
        Dim TotalHargaPerItem_Asing As Decimal
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
            TanggalDiterimaSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_Diterima_SJ_BAST_Produk"))
            NomorPO = dr.Item("Nomor_PO_Produk")
            COAProduk = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            JumlahHargaKeseluruhan += JumlahHargaPerItem
            DiskonPerItem_Persen = PembulatanDesimal2Digit(dr.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = JumlahHargaPerItem - DiskonPerItem_Rp
            JumlahNota += TotalHargaPerItem
            KodeProject = dr.Item("Kode_Project_Produk")
            KelompokAsset = dr.Item("Kelompok_Asset")
            KodeDivisiAsset = dr.Item("Kode_Divisi_Asset").ToString
            COABiaya = dr.Item("COA_Biaya")
            TanggalMulaiAmortisasi = AmbilValue_TanggalMulaiAmortisasiBerdasarkanNomorPembelianDanNamaProduk(NomorPembelian, NamaProduk)
            MasaAmortisasi = dr.Item("Masa_Amortisasi")
            'Asing :
            HargaSatuan_Asing = dr.Item("Harga_Satuan")
            JumlahHargaPerItem_Asing = JumlahProduk * HargaSatuan_Asing
            DiskonPerItem_Asing = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem_Asing = JumlahHargaPerItem_Asing - DiskonPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST,
                                    NomorPO, COAProduk, NamaProduk, DeskripsiProduk,
                                    JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                                    DiskonPerItem_Persen & " %", DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProject,
                                    KelompokAsset, KodeDivisiAsset, COABiaya, TanggalMulaiAmortisasi, MasaAmortisasi)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Sub IsiTabelSJBAST()
        Dim NomorSJBAST_Sebelumnya = Kosongan
        Dim NomorSJBAST = Kosongan
        Dim TanggalSJBAST = Kosongan
        Dim TanggalDiterima = Kosongan
        Dim NomorPO = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            IsiValueComboBypassTerkunci(cmb_JenisPPN, dr.Item("Jenis_PPN"))
            cmb_PerlakuanPPN.SelectedValue = dr.Item("Perlakuan_PPN")
            Dim Tabel
            Dim KolomNomor
            Dim KolomTanggal
            If Microsoft.VisualBasic.Left(NomorSJBAST, 2) = AwalanSJ Then
                Tabel = "tbl_Pembelian_SJ"
                KolomNomor = "Nomor_SJ"
                KolomTanggal = "Tanggal_SJ"
            Else
                Tabel = "tbl_Pembelian_BAST"
                KolomNomor = "Nomor_BAST"
                KolomTanggal = "Tanggal_BAST"
            End If
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM  " & Tabel &
                                             " WHERE " & KolomNomor & " = '" & NomorSJBAST & "' ",
                                             KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    TanggalSJBAST = TanggalFormatTampilan(drTELUSUR.Item(KolomTanggal))
                    TanggalDiterima = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Diterima"))
                    NomorPO = drTELUSUR.Item("Nomor_PO_Produk")
                End If
                If InvoiceDenganPO = False Or MetodePembayaran = MetodePembayaran_Termin Then
                    TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
                    TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima_SJ_BAST_Produk"))
                    NomorPO = txt_NomorPO.Text
                End If
                datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST, TanggalDiterima, NomorPO)
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi_TabelSJBAST()
    End Sub

    Sub Kosongkan_TabelProduk()
        datatabelUtama.Rows.Clear()
        JumlahProduk = 0
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        KosongkanKolomPerhitungan()
    End Sub
    Sub Kosongkan_TabelSJBAST()
        datatabelSJBAST.Rows.Clear()
        JumlahBarisSJBAST = 0
        If InvoiceDenganPO = True And MetodePembayaran = MetodePembayaran_Normal Then Kosongkan_TabelProduk()
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
        JumlahProduk = datatabelUtama.Rows.Count
    End Sub

    Sub BersihkanSeleksi_TabelSJBAST()
        BersihkanSeleksi_WPF(datagridSJBAST, datatabelSJBAST, BarisTerseleksiSJBAST, JumlahBarisSJBAST)
        NomorSJBAST_Terseleksi = Kosongan
        btn_SingkirkanSJBAST.IsEnabled = False
        JumlahBarisSJBAST = datatabelSJBAST.Rows.Count
    End Sub

    Sub KondisiFormSetelahPerubahan()
        If ProsesLoadingForm = True Or ProsesResetForm = True Or ProsesIsiValueForm = True Then Return
        grd_KolomPerhitungan.Visibility = Visibility.Collapsed
        BersihkanSeleksi_TabelProduk()
        KetersediaanTombolHitung(True)
    End Sub
    Sub KetersediaanTombolHitung(Tersedia As Boolean)
        btn_Hitung.IsEnabled = False
        If Tersedia Then
            If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_EDIT Then
                KetersediaanTombolSimpan(False)
                btn_Hitung.IsEnabled = True
            End If
        End If
    End Sub

    Sub KetersediaanTombolSimpan(Tersedia As Boolean)
        btn_Simpan.IsEnabled = False
        If Tersedia Then
            If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_EDIT Then btn_Simpan.IsEnabled = True
        End If
    End Sub

    Sub LogikaCOAKredit_UntukNonTunai()
        If JenisJasa = JenisJasa_Dividen Or JenisJasa = JenisJasa_LabaPajakBUT Then
            COAKredit = KodeTautanCOA_HutangPemegangSaham
        Else
            LogikaCOAKredit_UntukNonTunai_Sub()
        End If
        If JenisProduk_Induk = JenisProduk_Barang Then
            LogikaCOAKredit_UntukNonTunai_Sub()
        End If
    End Sub
    Sub LogikaCOAKredit_UntukNonTunai_Sub()
        Select Case KodeMataUang
            Case KodeMataUang_IDR
                If MitraSebagaiAfiliasi(KodeSupplier) Then
                    COAKredit = KodeTautanCOA_HutangUsaha_Afiliasi
                Else
                    COAKredit = KodeTautanCOA_HutangUsaha_NonAfiliasi
                End If
            Case KodeMataUang_USD
                COAKredit = KodeTautanCOA_HutangUsaha_USD
            Case KodeMataUang_AUD
                COAKredit = KodeTautanCOA_HutangUsaha_AUD
            Case KodeMataUang_JPY
                COAKredit = KodeTautanCOA_HutangUsaha_JPY
            Case KodeMataUang_CNY
                COAKredit = KodeTautanCOA_HutangUsaha_CNY
            Case KodeMataUang_EUR
                COAKredit = KodeTautanCOA_HutangUsaha_EUR
            Case KodeMataUang_SGD
                COAKredit = KodeTautanCOA_HutangUsaha_SGD
            Case KodeMataUang_GBP
                COAKredit = KodeTautanCOA_HutangUsaha_GBP
        End Select
    End Sub

    Dim EksekusiKodeSubPerhitungan As Boolean   'Ini untuk mencegah looping
    Dim JumlahLoopPerhitunngan = 0              'Ini hanya untuk keperluan mengetahui jumlah loop pada Sub Perhitungan
    Sub Perhitungan()

        JumlahProduk = datatabelUtama.Rows.Count

        If ProsesResetForm = True _
            Or ProsesIsiValueForm = True _
            Or ProsesLoadingForm = True _
            Or EksekusiKode = False _
            Or EksekusiKodeSubPerhitungan = False _
            Or JenisPPN = Kosongan _
            Or JumlahProduk = 0 _
            Then Return

        Dim RasioDPPBarang

        EksekusiKodeSubPerhitungan = False

        If Not AdaPPh Then txt_TarifPPh.Text = Kosongan

        'JumlahLoopPerhitunngan += 1
        'PesanUntukProgrammer("Jumlah Loop Perhitungan : " & JumlahLoopPerhitunngan & Enter2Baris &
        '                     "Tarif PPN : " & TarifPPN & Enter2Baris &
        '                     "Proses Loading Form : " & ProsesLoadingForm & Enter2Baris &
        '                     "Eksekusi Kode Logika PPN : " & EksekusiKodeLogikaPPN)

        JumlahHargaKeseluruhan = 0
        Diskon_Rp = 0
        HitunganHarga_Relatif = 0
        DPPBarang = 0
        DPPJasa = 0

        'Asing :
        JumlahHargaKeseluruhan_Asing = 0
        DiskonAsing = 0
        TotalTagihan_Asing = 0

        For Each row As DataRow In datatabelUtama.Rows
            JumlahHargaKeseluruhan += AmbilAngka(row("Jumlah_Harga_Per_Item"))
            JumlahHargaKeseluruhan_Asing += AmbilAngka_Asing(row("Jumlah_Harga_Per_Item_Asing"))
            Diskon_Rp += AmbilAngka(row("Diskon_Per_Item_Rp"))
            DiskonAsing += AmbilAngka_Asing(row("Diskon_Per_Item_Asing"))
            HitunganHarga_Relatif += AmbilAngka(row("Total_Harga"))
            If row("Jenis_produk_Per_Item") = JenisProduk_Barang Then DPPBarang += AmbilAngka(row("Total_Harga"))
            If row("Jenis_produk_Per_Item") = JenisProduk_Jasa Then
                DPPJasa += AmbilAngka(row("Total_Harga"))
            End If
        Next

        If PembelianLokal Then
            JumlahNota = JumlahHargaKeseluruhan - Diskon_Rp
            txt_JumlahNota.Text = JumlahNota
        Else
            JumlahNota_Asing = JumlahHargaKeseluruhan_Asing - DiskonAsing
            txt_JumlahNota.Text = JumlahNota_Asing
        End If

        RasioDPPBarang = DPPBarang / HitunganHarga_Relatif

        If MetodePembayaran = MetodePembayaran_Normal Then
            txt_Termin_Persen.Text = 100
            If PembelianLokal Then
                txt_Termin_Rp.Text = HitunganHarga_Relatif
            Else
                txt_Termin_Rp.Text = JumlahNota_Asing
            End If
        Else
            txt_Termin_Persen.Text = Termin_Persen
            If PembelianLokal Then
                txt_Termin_Rp.Text = Termin_Rp
                DPPBarang = Termin_Rp * RasioDPPBarang
            Else
                txt_Termin_Rp.Text = JumlahNota_Asing * (Termin_Persen / 100)
            End If
        End If

        HitunganHarga_Relatif = Termin_Rp

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            txt_Diskon_Rp.Text = Diskon_Rp
            If PembelianLokal Then
                txt_DasarPengenaanPajak.Text = HitunganHarga_Relatif
            End If
        End If

        If PembelianLokal Then
            LogikaTarifPPN()
            Select Case JenisPPN
                Case JenisPPN_NonPPN
                    PPN = 0
                    TotalTagihan_Kotor = DPP
                Case JenisPPN_Exclude
                    PPN = DPP * Persen(TarifPPN)
                    Select Case PerlakuanPPN
                        Case PerlakuanPPN_Dibayar
                            TotalTagihan_Kotor = DPP + PPN
                        Case PerlakuanPPN_Ditanggung
                            txt_TarifPPN.Text = Kosongan
                            PPN = 0
                            TotalTagihan_Kotor = DPP
                        Case PerlakuanPPN_Dibebaskan
                            txt_TarifPPN.Text = Kosongan
                            PPN = 0                     'Ini sebetulnya tidak perlu, tapi ga apa-apa buat jaga-jaga saja.
                            TotalTagihan_Kotor = DPP    'Ini sebetulnya tidak perlu, tapi ga apa-apa buat jaga-jaga saja.
                    End Select
                'If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP      '(Untuk sisi Pembelian, tidak ada Perlakuan PPN Dipungut)
                'If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP '(Untuk sisi Pembelian, tidak ada Perlakuan PPN Tidak Dipungut)
                Case JenisPPN_Include
                    If HitunganHarga_Relatif = 0 Then
                        Diskon_Rp = 0
                        DPP = 0
                        PPN = 0
                        TotalTagihan_Kotor = 0
                    Else
                        TotalTagihan_Kotor = HitunganHarga_Relatif
                        PPN = HitungPPNInclude(TotalTagihan_Kotor, TarifPPN)
                        DPP = TotalTagihan_Kotor - PPN
                        DPPBarang = DPP * RasioDPPBarang
                        If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalTagihan_Kotor = DPP + PPN
                        'If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP      '(Untuk sisi Pembelian, tidak ada Perlakuan PPN Dipungut)
                        'If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP '(Untuk sisi Pembelian, tidak ada Perlakuan PPN Tidak Dipungut)
                    End If
                    '---------------------------------------------------------
                    txt_Diskon_Rp.Text = Diskon_Rp
            End Select
            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Or JenisProduk_Induk = JenisProduk_Jasa Then
                DPPJasa = DPP
                DPPBarang = 0
            ElseIf JenisProduk_Induk = JenisProduk_BarangDanJasa Then
                DPPJasa = DPP - DPPBarang
            ElseIf JenisProduk_Induk = JenisProduk_Barang Then
                If PembelianLokal Then DPPBarang = DPP
            End If
            PPhTerutang = DPPJasa * Persen(TarifPPh)
        Else
            'DPPBarang = 0
            'DPPJasa = 0
            'TarifPPN = 0
            'TarifPPN_11Per12 = 0
            'TarifPPh = 0
            'txt_DPPBarang.Text = Kosongan
            'txt_DPPJasa.Text = Kosongan
            'txt_TarifPPN.Text = Kosongan
            'txt_TarifPPN_11Per12.Text = Kosongan
            'txt_TarifPPh.Text = Kosongan
            'txt_PPN.Text = Convert.ToInt64(DPP * Persen(TarifPPN))
            TotalTagihan_Kotor_Asing = JumlahNota_Asing + Insurance + Freight
            If MetodePembayaran = MetodePembayaran_Termin Then TotalTagihan_Kotor_Asing = Termin_Asing + Insurance + Freight
        End If


        'If JenisProduk_Induk = JenisProduk_Barang And PerlakuanPPN = PerlakuanPPN_Dipungut Then PPhTerutang = DPP * Persen(TarifPPh)
        '(Untuk sisi Pembelian, tidak ada Perlakuan PPN Dipungut)

        If JumlahHargaKeseluruhan > 0 Then
            txt_DPPBarang.Text = DPPBarang
            If PembelianLokal Then txt_DPPJasa.Text = DPPJasa
        End If

        If PembelianLokal Then
            txt_DasarPengenaanPajak.Text = DPP
            txt_PPhTerutang.Text = PPhTerutang
        End If

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
        End If

        txt_PPhDitanggung.Text = PPhDitanggung

        PerhitunganFinal()

    End Sub

    Sub PerhitunganFinal()

        PPhDipotong = PPhTerutang - PPhDitanggung
        txt_PPhDipotong.Text = PPhDipotong

        If PembelianLokal Then
            BiayaKeperluanKantor = BiayaMaterai                                             '(Untuk saat ini hanya segini). Kalau ada tambahan biaya keperluan kantor lainnya bisa ditambahkan di sini)
            BiayaBiayaTambahan = OngkosKirim + BiayaKeperluanKantor                         '(Untuk saat ini hanya segini). Kalau ada tambahan biaya lainnya bisa ditambahkan di sini)
            TotalTagihan = TotalTagihan_Kotor + BiayaBiayaTambahan - PPhDipotong
            JumlahHutangUsaha = TotalTagihan_Kotor + BiayaBiayaTambahan
        Else
            TotalTagihan_Asing = TotalTagihan_Kotor_Asing
            JumlahHutangUsaha_Asing = TotalTagihan_Kotor_Asing
        End If

        'If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
        If PerlakuanPPN = Kosongan And (JenisPPN <> JenisPPN_NonPPN Or JenisPPN <> Kosongan) Then
            txt_TarifPPN.Text = Kosongan
            txt_PPN.Text = Kosongan
            If PembelianLokal Then
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
                txt_TotalTagihan.Text = TotalTagihan
                txt_JumlahHutangUsaha.Text = JumlahHutangUsaha
            End If
        Else
            If PembelianLokal Then
                txt_PPN.Text = PPN
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
                txt_TotalTagihan.Text = TotalTagihan
                txt_JumlahHutangUsaha.Text = JumlahHutangUsaha
            Else
                txt_PPN.Text = Convert.ToInt64(DPP * Persen(TarifPPN))
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor_Asing
                txt_TotalTagihan.Text = TotalTagihan_Asing
                txt_JumlahHutangUsaha.Text = JumlahHutangUsaha_Asing
            End If
        End If

        If TahapTermin = TahapTermin_Pelunasan Then
            VisibilitasUangMukaPlusTermin(True)
            txt_UangMukaPlusTermin_Persen.Text = (100 - Termin_Persen)
            If PembelianLokal Then txt_UangMukaPlusTermin_Rp.Text = JumlahNota - Termin_Rp
            If PembelianImpor Then txt_UangMukaPlusTermin_Rp.Text = JumlahNota_Asing - Termin_Asing
        Else
            VisibilitasUangMukaPlusTermin(False)
            txt_UangMukaPlusTermin_Persen.Text = Kosongan
            txt_UangMukaPlusTermin_Rp.Text = Kosongan
        End If

        BarisTotalTabel()

        If AdaPPh Then
            VisibilitasElemenPPh(True)
            If PPhTerutang = 0 Then VisibilitasPPhDitanggungDipotong(False) 'Ini ada kaitannya dengan Jenis Jasa : Lainnya. Jadi jangan semudah itu dirubah.
            If JenisPPh = JenisPPh_Pasal22_Impor Then VisibilitasPPhDitanggungDipotong(False)
        Else
            VisibilitasElemenPPh(False)
        End If

        'PesanUntukProgrammer("DPP Barang : " & DPPBarang & Enter2Baris &
        '                     "DPP Jasa : " & DPPJasa & Enter2Baris &
        '                     "Jenis PPh : " & JenisPPh & Enter2Baris &
        '                     "Tarif PPh : " & TarifPPh & Enter2Baris &
        '                     "PPh Terutang : " & PPhTerutang)

        Perhitungan_ValueBank()

        If PembelianImpor Then
            PPhDitanggung = PPhTerutang
            'Untuk sementara, Pembelian impor logikanya itu PPh Ditanggung = PPh Terutang 100%. 
            'Ini logika sementara.
            'Jika ada perubahan logika, nanti harus disesuaikan lagi.
        End If

        LogikaTampilanPPN()

        If PembelianLokal Then
            txt_TotalTagihanIDR.Text = Kosongan
        Else
            txt_TotalTagihanIDR.Text = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TotalTagihan_Asing)
        End If

        'EksekusiKodeLogikaPPN = True 'Ini harus paling ujung, dan tidak boleh dihapus.
        EksekusiKodeSubPerhitungan = True

        grd_KolomPerhitungan.Visibility = Visibility.Visible

    End Sub

    Sub Algoritma_OngkosKirim()

        OngkosKirimAssetKeseluruhan = 0
        OngkosKirimAsset_PerItem = 0

        'Asing :
        OngkosKirimAssetKeseluruhan_Asing = 0
        OngkosKirimAsset_PerItem_Asing = 0

        JumlahProdukProduksiKeseluruhan = 0
        For Each row As DataRow In datatabelUtama.Rows
            If AmbilAngka(AmbilTeksKiri(row("COA_Produk"), 1)) = 5 Then
                JumlahProdukProduksiKeseluruhan += AmbilAngka(row("Jumlah_Produk"))
            End If
        Next

        JumlahProdukAdministrasiKeseluruhan = 0
        For Each row As DataRow In datatabelUtama.Rows
            If AmbilAngka(AmbilTeksKiri(row("COA_Produk"), 1)) = 6 Then
                JumlahProdukAdministrasiKeseluruhan += AmbilAngka(row("Jumlah_Produk"))
            End If
        Next

        JumlahProdukAssetKeseluruhan = 0
        JumlahHargaAssetKeseluruhan = 0
        'Asing :
        JumlahHargaAssetKeseluruhan_Asing = 0
        For Each row As DataRow In datatabelUtama.Rows
            If AmbilAngka(row("Kelompok_Asset")) > 0 Then
                JumlahProdukAssetKeseluruhan += AmbilAngka(row("Jumlah_Produk"))
                JumlahHargaAssetKeseluruhan += AmbilAngka(row("Total_Harga"))
                'Asing :
                JumlahHargaAssetKeseluruhan_Asing += AmbilAngka(row("Total_Harga_Asing"))
            End If
        Next

        If LokasiWP = LokasiWP_DalamNegeri Then
            If JumlahProdukProduksiKeseluruhan > 0 Then
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi
            ElseIf JumlahProdukAdministrasiKeseluruhan > 0 Then
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi
            ElseIf JumlahProdukAssetKeseluruhan > 0 Then
                OngkosKirimAssetKeseluruhan = OngkosKirim
                OngkosKirimAssetKeseluruhan_Asing = 0
            Else
                COAOngkosKirim = KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi
            End If
        End If

        If LokasiWP = LokasiWP_LuarNegeri Then
            COAOngkosKirim = KodeTautanCOA_BiayaTransportasi_Import
        End If

    End Sub


    Sub BarisTotalTabel()
        JumlahBaris = datatabelUtama.Rows.Count
        If JumlahBaris > 0 Then
            grd_KolomPerhitungan.Visibility = Visibility.Visible
        Else
            grd_KolomPerhitungan.Visibility = Visibility.Collapsed
        End If
        'If JumlahBaris > 1 Then
        '    datagridTotal.Visibility = Visibility.Visible
        'Else
        '    datagridTotal.Visibility = Visibility.Collapsed
        'End If
        'datatabelTotal.Rows(0)("Jumlah_Harga_Keseluruhan") = JumlahHargaKeseluruhan
        'datatabelTotal.Rows(0)("Diskon_Rp_Keseluruhan") = Diskon_Rp
        'datatabelTotal.Rows(0)("Total_Harga_Keseluruhan") = JumlahNota
    End Sub
    Sub KosongkanKolomPerhitungan()
        grd_KolomPerhitungan.Visibility = Visibility.Collapsed
        txt_JumlahNota.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_Termin_Persen.Text = Kosongan
        txt_UangMukaPlusTermin_Persen.Text = Kosongan
        txt_UangMukaPlusTermin_Rp.Text = Kosongan
        txt_Termin_Rp.Text = Kosongan
        txt_Insurance.Text = Kosongan
        txt_Freight.Text = Kosongan
        txt_KursKMK.Text = Kosongan
        txt_BeaMasuk.Text = Kosongan
        txt_DPPBarang.Text = Kosongan
        txt_DPPJasa.Text = Kosongan
        txt_TarifPPN.Text = Kosongan
        txt_TarifPPN_11Per12.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_DasarPengenaanPajak_11Per12.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalTagihan_Kotor.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_OngkosKirim.Text = Kosongan
        txt_BiayaMaterai.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan
        txt_JumlahHutangUsaha.Text = Kosongan
    End Sub

    Sub OngkosKirim_BerdasarkanSuratJalan()
        If EksekusiPerhitungan_OngkosKirim = True Then
            OngkosKirim = 0
            For Each row As DataRow In datatabelSJBAST.Rows
                OngkosKirim += AmbilAngka(row("Ongkos_Kirim"))
            Next
            txt_OngkosKirim.Text = OngkosKirim
        End If
    End Sub



    Private Sub cmb_JenisInvoice_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisInvoice.SelectionChanged
        JenisInvoice = cmb_JenisInvoice.SelectedValue
        If JenisInvoice = JenisInvoice_Biasa _
            And JumlahBarisSJBAST > 0 _
            And ProsesResetForm = False _
            Then
            Kosongkan_TabelSJBAST()
            MsgBox("Daftar Surat Jalan / BAST telah dikosongkan." & Enter2Baris & "Silakan isi kembali.")
            btn_TambahSJBAST.Focus()
        End If
        KunciTanggalInvoice = False
        If JenisInvoice = JenisInvoice_Gabungan Then btn_TambahSJBAST.IsEnabled = True
    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub
    Private Sub txt_NomorInvoice_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_NomorInvoice.LostFocus
        If NomorInvoice = Kosongan Then
            'PesanPeringatan("Silakan isi kolom 'Nomor Invoice'.")
            'txt_NomorInvoice.Focus()
            'Return
        End If
        'Periksa/Validasi Nomor Invoice
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Nomor_Invoice FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NomorInvoice.Text = Kosongan
                MsgBox("Nomor Invoice '" & NomorInvoice & "' sudah ada." & Enter2Baris & "Silakan input Nomor Invoice yang lain.")
                txt_NomorInvoice.Focus()
            End If
            AksesDatabase_Transaksi(Tutup)
        End If
    End Sub


    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalInvoice.SelectedDateChanged
        If dtp_TanggalInvoice.Text <> Kosongan Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_TanggalInvoice)
            Else
                KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalInvoice)
            End If
            If NP = "N" Then
                TanggalInvoice = dtp_TanggalInvoice.SelectedDate
                TanggalPembetulan = TanggalInvoice
            Else
                TanggalPembetulan = dtp_TanggalInvoice.SelectedDate
            End If
            KondisiFormSetelahPerubahan()
            If FungsiForm = FungsiForm_TAMBAH Then
                dtp_TanggalDiterimaInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice)
            End If
            LogikaTarifPPN()
            LogikaTampilanKolomImporTermin()
            PenentuanKurs()
        End If
    End Sub


    Private Sub dtp_TanggalDiterimaInvoice_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalDiterimaInvoice.SelectedDateChanged
        If dtp_TanggalDiterimaInvoice.Text <> Kosongan Then
            KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(dtp_TanggalDiterimaInvoice, dtp_TanggalInvoice.SelectedDate.Value.Day, dtp_TanggalInvoice.SelectedDate.Value.Month, dtp_TanggalInvoice.SelectedDate.Value.Year)
            TanggalDiterimaInvoice = dtp_TanggalDiterimaInvoice.SelectedDate
            DefaultValue_TanggalJatuhTempo()
            LogikaJenisPembelian()
        End If
    End Sub


    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        txt_NamaSupplier.Text = AmbilValue_NamaMitra(KodeSupplier)
        txt_PICSupplier.Text = AmbilValue_PICMitra(KodeSupplier)
        txt_AlamatSupplier.Text = AmbilValue_AlamatMitra(KodeSupplier)
        If MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then
            MitraLuarNegeri = True
        Else
            MitraLuarNegeri = False
        End If
        If KodeSupplier = Kosongan Then
            cmb_JenisJasa.IsEnabled = False
            btn_Tambahkan.IsEnabled = False
            btn_TambahSJBAST.IsEnabled = False
        Else
            cmb_JenisJasa.IsEnabled = True
            btn_Tambahkan.IsEnabled = True
            btn_TambahSJBAST.IsEnabled = True
        End If
        AmbilValue_JenisWP_dan_LokasiWP(KodeSupplier, JenisWP, LokasiWP)
        If MitraSebagaiPKP(KodeSupplier) Then
            SupplierSebagaiPKP = True
        Else
            SupplierSebagaiPKP = False
        End If
        If MitraSebagaiUMKM(KodeSupplier) Then
            SupplierSebagaiUMKM = True
        Else
            SupplierSebagaiUMKM = False
        End If
        LogikaTarifPPN()
        KontenCombo_JenisPPN()
        If Not InvoiceDenganPO Then
            KontenCombo_JenisJasa()
            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then IsiValueComboBypassTerkunci(cmb_JenisJasa, JenisJasa_JasaKonstruksi)
            If JenisProduk_Induk = JenisProduk_Barang Then KosongkanItemCombo(JenisJasa)
        End If
        txt_NomorPO.Text = Kosongan
        Kosongkan_TabelSJBAST()
        Kosongkan_TabelProduk()
        LogikaAdaPPh(True)
        LogikaAsalPembelian()
        If MetodePembayaran = MetodePembayaran_Normal Then VisibilitasTabelSJBAST(True)
        If MetodePembayaran = MetodePembayaran_Termin Then VisibilitasTabelSJBAST(False)
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub btn_PilihSupplier_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        If ((MetodePembayaran = MetodePembayaran_Termin Or Not InvoiceDenganPO) And dtp_TanggalInvoice.Text = Kosongan) _
            Or ((PembelianImpor And InvoiceDenganPO) And dtp_TanggalInvoice.Text = Kosongan) _
            Then
            PesanPeringatan("Silakan isi 'Tanggal Invoice' terlebih dahulu.")
            dtp_TanggalInvoice.Focus()
            Return
        End If
        'PesanUntukProgrammer("Jenis Relasi : " & JenisRelasi & Enter2Baris &
        '                     "Asal : " & AsalPembelian)
        If PembelianLokal Then
            If JenisRelasi = JenisRelasi_Afiliasi Then BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua)
            If JenisRelasi = JenisRelasi_NonAfiliasi Then BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Semua)
            If JenisRelasi = Pilihan_Semua Then BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, LokasiWP_DalamNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
        End If
        If PembelianImpor Then BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, LokasiWP_LuarNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_PICSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PICSupplier.TextChanged
    End Sub
    Private Sub txt_AlamatSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AlamatSupplier.TextChanged
    End Sub
    Sub LogikaAsalPembelian()
        If PembelianLokal Then
            MataUang = MataUang_Rupiah
            IsiValueComboBypassTerkunci(cmb_KodeMataUang, KodeMataUang_IDR)
            cmb_KodeMataUang.SelectedValue = KodeMataUang_IDR
            lbl_KodeMataUang.Visibility = Visibility.Collapsed
            cmb_KodeMataUang.Visibility = Visibility.Collapsed
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Text = 1
            lbl_TotalTagihanIDR.Visibility = Visibility.Collapsed
            txt_TotalTagihanIDR.Visibility = Visibility.Collapsed
            lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
            lbl_TanggalFakturPajak.Text = "Tanggal Faktur Pajak"
            lbl_DPP.Text = "Dasar Pengenaan Pajak"
            'Kolom-kolom Tabel - Lokal :
            Harga_Satuan.Visibility = Visibility.Visible
            Jumlah_Harga_Per_Item.Visibility = Visibility.Visible
            Diskon_Per_Item_Rp.Visibility = Visibility.Visible
            Total_Harga.Visibility = Visibility.Visible
            Harga_Satuan.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Rp.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
            'Kolom-kolom Tabel - Asing :
            Harga_Satuan_Asing.Visibility = Visibility.Collapsed
            Jumlah_Harga_Per_Item_Asing.Visibility = Visibility.Collapsed
            Diskon_Per_Item_Asing.Visibility = Visibility.Collapsed
            Total_Harga_Asing.Visibility = Visibility.Collapsed
            'Kolom-kolom Kanan - Lokal :
            lbl_PPN.Text = "PPN"
            lbl_PersenPPN.Visibility = Visibility.Visible
            lbl_OngkosKirim.Visibility = Visibility.Visible
            txt_OngkosKirim.Visibility = Visibility.Visible
            lbl_BiayaMaterai.Visibility = Visibility.Visible
            txt_BiayaMaterai.Visibility = Visibility.Visible
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Visible
            txt_TotalTagihan_Kotor.Visibility = Visibility.Visible
            VisibilitasKolomJumlahHutangUsaha(True)
            'Kolom-kolom Kanan - Asing :
            lbl_Insurance.Visibility = Visibility.Collapsed
            txt_Insurance.Visibility = Visibility.Collapsed
            lbl_Freight.Visibility = Visibility.Collapsed
            txt_Freight.Visibility = Visibility.Collapsed
            lbl_BeaMasuk.Visibility = Visibility.Collapsed
            txt_BeaMasuk.Visibility = Visibility.Collapsed
            lbl_KursKMK.Visibility = Visibility.Collapsed
            txt_KursKMK.Visibility = Visibility.Collapsed
            VisibilitasKolomSerahTerima(False)
            'Styling :
            txt_JumlahNota.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_Diskon_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_UangMukaPlusTermin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_Termin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_DPPBarang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_DPPJasa.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_DasarPengenaanPajak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_DasarPengenaanPajak_11Per12.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_PPN.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_TotalTagihan_Kotor.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_OngkosKirim.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaMaterai.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_TotalTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_JumlahHutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            VisibilitasKolomJenisPPN(True)
        Else
            MataUang = MataUang_Asing
            KontenCombo_KodeMataUangAsing()
            KontenCombo_Loko()
            'If KodeMataUang = KodeMataUang_IDR Or KodeMataUang = Kosongan Then cmb_KodeMataUang.SelectedValue = KodeMataUang_USD '(Default :USD)
            lbl_KodeMataUang.Visibility = Visibility.Visible
            cmb_KodeMataUang.Visibility = Visibility.Visible
            lbl_Kurs.Visibility = Visibility.Visible
            txt_Kurs.Visibility = Visibility.Visible
            lbl_TotalTagihanIDR.Visibility = Visibility.Visible
            txt_TotalTagihanIDR.Visibility = Visibility.Visible
            lbl_NomorFakturPajak.Text = "Nomor PIB"
            lbl_TanggalFakturPajak.Text = "Tanggal PIB"
            lbl_DPP.Text = "Nilai Impor"
            'Kolom-kolom Tabel - Lokal :
            Harga_Satuan.Visibility = Visibility.Collapsed
            Jumlah_Harga_Per_Item.Visibility = Visibility.Collapsed
            Diskon_Per_Item_Rp.Visibility = Visibility.Collapsed
            Total_Harga.Visibility = Visibility.Collapsed
            'Kolom-kolom Tabel - Asing :
            Harga_Satuan_Asing.Visibility = Visibility.Visible
            Jumlah_Harga_Per_Item_Asing.Visibility = Visibility.Visible
            Diskon_Per_Item_Asing.Visibility = Visibility.Visible
            Total_Harga_Asing.Visibility = Visibility.Visible
            'Harga_Satuan_Asing.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            'Jumlah_Harga_Per_Item_Asing.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            'Diskon_Per_Item_Asing.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            'Total_Harga_Asing.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
            'Kolom-kolom Kanan - Lokal :
            lbl_PPN.Text = "PPN Impor"
            txt_TarifPPN.Visibility = Visibility.Collapsed
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            lbl_PersenPPN.Visibility = Visibility.Collapsed
            If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then
                lbl_DPP.Visibility = Visibility.Visible
                lbl_PPN.Visibility = Visibility.Visible
                txt_TarifPPN.Visibility = Visibility.Visible
                lbl_PersenPPN.Visibility = Visibility.Visible
            Else
                lbl_DPP.Visibility = Visibility.Collapsed
                txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
                lbl_PPN.Visibility = Visibility.Collapsed
                txt_TarifPPN.Visibility = Visibility.Collapsed
                lbl_PersenPPN.Visibility = Visibility.Collapsed
            End If
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            lbl_OngkosKirim.Visibility = Visibility.Collapsed
            txt_OngkosKirim.Visibility = Visibility.Collapsed
            lbl_BiayaMaterai.Visibility = Visibility.Collapsed
            txt_BiayaMaterai.Visibility = Visibility.Collapsed
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            txt_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            VisibilitasKolomJumlahHutangUsaha(False)
            'Kolom-kolom Kanan - Asing :
            lbl_Insurance.Visibility = Visibility.Visible
            txt_Insurance.Visibility = Visibility.Visible
            lbl_Freight.Visibility = Visibility.Visible
            txt_Freight.Visibility = Visibility.Visible
            If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then
                lbl_BeaMasuk.Visibility = Visibility.Visible
                txt_BeaMasuk.Visibility = Visibility.Visible
            Else
                lbl_BeaMasuk.Visibility = Visibility.Collapsed
                txt_BeaMasuk.Visibility = Visibility.Collapsed
            End If
            lbl_KursKMK.Visibility = Visibility.Visible
            txt_KursKMK.Visibility = Visibility.Visible
            If InvoiceDenganPO Then VisibilitasBarisPO(True)
            VisibilitasTabelSJBAST(False)
            VisibilitasKolomSerahTerima(True)
            'Styling :
            txt_JumlahNota.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Diskon_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_UangMukaPlusTermin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Termin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DPPBarang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DPPJasa.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DasarPengenaanPajak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_DasarPengenaanPajak_11Per12.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_PPN.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_TotalTagihan_Kotor.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_OngkosKirim.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaMaterai.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_TotalTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_JumlahHutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            VisibilitasKolomJenisPPN(False)
        End If
        If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then
            txt_PPN.Visibility = Visibility.Visible
        Else
            txt_PPN.Visibility = Visibility.Collapsed
        End If
        AktifkanFakturPajak()
    End Sub




    Private Sub txt_NomorPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorPO.TextChanged
        NomorPOProduk = txt_NomorPO.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_PilihPO_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihPO.Click
        If KodeSupplier = Kosongan Then
            PesanPeringatan("Silakan pilih 'Supplier' terlebih dahulu.")
            btn_PilihMitra.Focus()
            Return
        End If
        win_ListPO = New wpfWin_ListPO
        win_ListPO.ResetForm()
        win_ListPO.Sisi = win_ListPO.Sisi_POPembelian
        win_ListPO.NamaMitra_Filter = NamaSupplier
        win_ListPO.FilterMitra_Aktif = False
        win_ListPO.MetodePembayaran = MetodePembayaran
        win_ListPO.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_ListPO.ShowDialog()
        EksekusiLogikaAdaPPh = False
        If String.IsNullOrEmpty(win_ListPO.NomorPO_Terseleksi) Then Return
        ProsesIsiValueForm = True
        Kosongkan_TabelProduk()
        txt_NomorPO.Text = win_ListPO.NomorPO_Terseleksi
        IsiValueComboBypassTerkunci(cmb_JenisPPN, AmbilValue_JenisPPNBerdasarkanPOPembelian(NomorPOProduk))
        IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, AmbilValue_PerlakuanPPNBerdasarkanPOPembelian(NomorPOProduk))
        JenisProduk_Induk = AmbilValue_JenisProdukIndukBerdasarkanPOPembelian(NomorPOProduk)
        LogikaTampilanKolomDPPBarangDanJasa()
        IsiTabelProduk_BerdasarkanPO()
        If PembelianLokal Then txt_JumlahNota.Text = JumlahNota
        If PembelianImpor Then txt_JumlahNota.Text = JumlahNota_Asing
        If MetodePembayaran = MetodePembayaran_Termin Then
            LogikaTermin()
            If Not POMasihTersedia Then Return
        End If
        LogikaTarifPPN()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                              " WHERE Nomor_PO = '" & NomorPOProduk & "' ", KoneksiDatabaseTransaksi)
        dr_Read()
        If dr.HasRows Then
            IsiValueComboBypassTerkunci(cmb_JenisJasa, dr.Item("Jenis_Jasa"))
            IsiValueComboBypassTerkunci(cmb_JenisPPh, dr.Item("Jenis_PPh"))
            IsiValueComboBypassTerkunci(cmb_KodeSetoran, dr.Item("Kode_Setoran"))
            cmb_KodeMataUang.SelectedValue = dr.Item("Kode_Mata_Uang")
            'Awal Coding untuk logika baru PPN : -----------------------------------------------
            txt_Termin_Persen.Text = Termin_Persen
            If PembelianLokal Then txt_Termin_Rp.Text = Termin_Rp
            If PembelianImpor Then txt_Termin_Rp.Text = Termin_Asing
            txt_DPPBarang.Text = dr.Item("DPP_Barang")
            txt_DPPJasa.Text = dr.Item("DPP_Jasa")
            txt_DasarPengenaanPajak.Text = dr.Item("Dasar_Pengenaan_Pajak")
            txt_PPN.Text = dr.Item("PPN")
            txt_TotalTagihan_Kotor.Text = DPP + PPN
            txt_TarifPPN.Text = PembulatanDesimal2Digit((100 * PPN) / DPP)
            'PesanUntukProgrammer("DPP : " & DPP & Enter2Baris &
            '                     "Tarif PPN : " & TarifPPN & Enter2Baris &
            '                     "PPN : " & PPN)
            'Akhir Coding untuk logika baru PPN : ----------------------------------------------
            txt_TarifPPh.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPh"))
            txt_PPhTerutang.Text = dr.Item("PPh_Terutang") * Termin_Persen / 100
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung") * Termin_Persen / 100
            txt_PPhDipotong.Text = dr.Item("PPh_Dipotong") * Termin_Persen / 100
            txt_OngkosKirim.Text = dr.Item("Biaya_Transportasi")
        End If
        AksesDatabase_Transaksi(Tutup)
        EksekusiLogikaAdaPPh = True
        LogikaAdaPPh(True)
        'EksekusiKodeLogikaPPN = False  'Ini jangan dihapus dulu. Supaya ketahuan posisinya, walau pun sudah dinonaktifkan. Siapa tahu nanti dibutuhkan lagi.
        'Perhitungan()                  'Ini jangan dihapus dulu. Supaya ketahuan posisinya, walau pun sudah dinonaktifkan. Siapa tahu nanti dibutuhkan lagi.
        If TarifPPN < 10 Then
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        End If
        'PesanUntukProgrammer("Metode Pembayaran : " & MetodePembayaran & Enter2Baris &
        '                     "Tahap Termin : " & TahapTermin)
        If MetodePembayaran = MetodePembayaran_Termin Then
            If TahapTermin = TahapTermin_UangMuka Then
                VisibilitasTabelSJBAST(False)
                Kosongkan_TabelSJBAST()
                ProsesIsiValueForm = True   '(Kembalikan ke True, setelah ada perubahan ke False di Sub sebelum baris ini)
            Else
                VisibilitasTabelSJBAST(True)
                TambahSJBAST_Manual()
                ProsesIsiValueForm = True   '(Kembalikan ke True, setelah ada perubahan ke False di Sub sebelum baris ini)
                CekNomorSJBAST()
                If JumlahBarisSJBAST > 0 Then
                    For Each row As DataRow In datatabelUtama.Rows
                        row("Nomor_SJ_BAST_Produk") = NomorSJBAST_Aktif
                        row("Tanggal_SJ_BAST_Produk") = TanggalSJBAST_Aktif
                        row("Tanggal_Diterima_SJ_BAST_Produk") = TanggalDiterimaSJBAST_Aktif
                    Next
                Else
                    PesanPeringatan("Surat Jalan tidak diisi. Input PO dibatalkan..!")
                    KosongkanItemCombo(cmb_JenisPPN)
                    SembunyikanElemenPajak()
                    Kosongkan_TabelProduk()
                    Return
                End If
            End If
            If TahapTermin = TahapTermin_Pelunasan Then
                VisibilitasKolomCOA(True)
            Else
                VisibilitasKolomCOA(False)
            End If
        End If
        LogikaVisibilitasJenisJasa()
        ProsesIsiValueForm = False
        If AdaPPh Then
            VisibilitasElemenPPh(True)
            If PPhTerutang = 0 Then VisibilitasPPhDitanggungDipotong(False) 'Ini ada kaitannya dengan Jenis Jasa : Lainnya. Jadi jangan semudah itu dirubah.
            If JenisPPh = JenisPPh_Pasal22_Impor Then VisibilitasPPhDitanggungDipotong(False)
        Else
            VisibilitasElemenPPh(False)
        End If
        rdb_JumlahHariJatuhTempo.IsChecked = True   'Ini buat ngegocek aja sementara, supaya muncul value di total tagihan.
        rdb_JumlahHariJatuhTempo.IsChecked = False  'Ke depannya coba nanti 3 baris ini dihapus, dan pakai logika yang lebih sehat.
        dtp_TanggalSerahTerima.Focus()              'Ini hanya sementara saja karena belum ketahuan penyebabnya.
        'PesanUntukProgrammer("DPP : " & DPP & Enter2Baris &
        '                     "Tarif PPN : " & TarifPPN & Enter2Baris &
        '                     "PPN : " & PPN)
    End Sub
    Sub IsiTabelProduk_BerdasarkanPO()
        Dim TanggalDiterimaSJBAST
        Dim JenisProdukPerItem
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan As Int64
        Dim JumlahHargaPerItem As Int64
        Dim DiskonPerItem_Persen As Decimal
        Dim DiskonPerItem_Rp As Int64  '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
        Dim TotalHargaPerItem As Int64
        Dim NomorPO
        Dim NomorSJBAST
        Dim TanggalSJBAST
        Dim KodeProject
        Dim NomorUrut = 0
        'Asing :
        Dim HargaSatuan_Asing As Decimal
        Dim JumlahHargaPerItem_Asing As Decimal
        Dim DiskonPeritem_Asing As Decimal
        Dim TotalHargaPerItem_Asing As Decimal
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                              " WHERE Nomor_PO = '" & NomorPOProduk & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = NomorSJBAST_Aktif
            TanggalSJBAST = TanggalSJBAST_Aktif
            TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Aktif
            NomorPO = NomorPOProduk
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            JumlahHargaKeseluruhan += JumlahHargaPerItem
            DiskonPerItem_Persen = PembulatanDesimal2Digit(dr.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = dr.Item("Total_Harga_Per_Item")
            JumlahNota += TotalHargaPerItem
            KodeProject = dr.Item("Kode_Project_Produk")
            'Asing :
            HargaSatuan_Asing = dr.Item("Harga_Satuan_Asing")
            JumlahHargaPerItem_Asing = JumlahProduk * HargaSatuan_Asing
            DiskonPeritem_Asing = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem_Asing = JumlahHargaPerItem_Asing - DiskonPeritem_Asing
            JumlahNota_Asing += TotalHargaPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST,
                                    NomorPO, Kosongan, NamaProduk, DeskripsiProduk,
                                    JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                                    DiskonPerItem_Persen & " %", DiskonPerItem_Rp, DiskonPerItem_Asing_Terseleksi, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProject,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub
    Dim POMasihTersedia As Boolean
    Sub LogikaTermin()
        POMasihTersedia = True
        If Not MetodePembayaran = MetodePembayaran_Termin Then Return
        Dim JumlahTermin As Integer = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPOProduk & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JumlahTermin = dr.Item("Jumlah_Termin")
            BasisPerhitunganTermin = dr.Item("Basis_Perhitungan_Termin")
            Select Case BasisPerhitunganTermin
                Case BasisPerhitunganTermin_Prosentase
                    LogikaTermin_BasisProsentase()
                Case BasisPerhitunganTermin_Nominal
                    LogikaTermin_BasisNominal()
            End Select
        End If
    End Sub
    Sub LogikaTermin_BasisProsentase()
        Dim UangMuka_Persen As Decimal = 0
        Dim Termin1_Persen As Decimal = 0
        Dim Termin2_Persen As Decimal = 0
        Dim Pelunasan_Persen As Decimal = 0
        If dr.HasRows Then
            UangMuka_Persen = dr.Item("Uang_Muka")
            Termin1_Persen = dr.Item("Termin_1")
            Termin2_Persen = dr.Item("Termin_2")
            Pelunasan_Persen = dr.Item("Pelunasan")
        End If
        AksesDatabase_Transaksi(Tutup)
        If UangMuka_Persen > 0 And CekInvoice(TahapTermin_UangMuka) = False Then
            TahapTermin = TahapTermin_UangMuka
            Termin_Persen = UangMuka_Persen
            IsiValueTermin()
        ElseIf Termin1_Persen > 0 And CekInvoice(TahapTermin_Termin1) = False Then
            TahapTermin = TahapTermin_Termin1
            Termin_Persen = Termin1_Persen
            IsiValueTermin()
        ElseIf Termin2_Persen > 0 And CekInvoice(TahapTermin_Termin2) = False Then
            TahapTermin = TahapTermin_Termin2
            Termin_Persen = Termin2_Persen
            IsiValueTermin()
        ElseIf Pelunasan_Persen > 0 And CekInvoice(TahapTermin_Pelunasan) = False Then
            TahapTermin = TahapTermin_Pelunasan
            Termin_Persen = Pelunasan_Persen
            IsiValueTermin()
        Else
            POMasihTersedia = False
            txt_NomorPO.Text = Kosongan
            Kosongkan_TabelSJBAST()
            Kosongkan_TabelProduk()
            PesanPeringatan("Invoice untuk PO ini sudah lengkap..!" & Enter2Baris &
                            "Silakan tambahkan PO yang lain.")
        End If
    End Sub
    Sub LogikaTermin_BasisNominal()
        Dim UangMuka_Rp As Int64 = 0
        Dim Termin1_Rp As Int64 = 0
        Dim Termin2_Rp As Int64 = 0
        Dim Pelunasan_Rp As Int64 = 0
        Dim UangMuka_Asing As Decimal = 0
        Dim Termin1_Asing As Decimal = 0
        Dim Termin2_Asing As Decimal = 0
        Dim Pelunasan_Asing As Decimal = 0
        If dr.HasRows Then
            If PembelianLokal Then
                UangMuka_Rp = dr.Item("Uang_Muka")
                Termin1_Rp = dr.Item("Termin_1")
                Termin2_Rp = dr.Item("Termin_2")
                Pelunasan_Rp = dr.Item("Pelunasan")
            Else
                UangMuka_Asing = dr.Item("Uang_Muka")
                Termin1_Asing = dr.Item("Termin_1")
                Termin2_Asing = dr.Item("Termin_2")
                Pelunasan_Asing = dr.Item("Pelunasan")
            End If
        End If
        AksesDatabase_Transaksi(Tutup)
        If PembelianLokal Then
            If UangMuka_Rp > 0 And CekInvoice(TahapTermin_UangMuka) = False Then
                TahapTermin = TahapTermin_UangMuka
                Termin_Rp = UangMuka_Rp
                IsiValueTermin()
            ElseIf Termin1_Rp > 0 And CekInvoice(TahapTermin_Termin1) = False Then
                TahapTermin = TahapTermin_Termin1
                Termin_Rp = Termin1_Rp
                IsiValueTermin()
            ElseIf Termin2_Rp > 0 And CekInvoice(TahapTermin_Termin2) = False Then
                TahapTermin = TahapTermin_Termin2
                Termin_Rp = Termin2_Rp
                IsiValueTermin()
            ElseIf Pelunasan_Rp > 0 And CekInvoice(TahapTermin_Pelunasan) = False Then
                TahapTermin = TahapTermin_Pelunasan
                Termin_Rp = Pelunasan_Rp
                IsiValueTermin()
            Else
                POMasihTersedia = False
                txt_NomorPO.Text = Kosongan
                Kosongkan_TabelSJBAST()
                Kosongkan_TabelProduk()
                PesanPeringatan("Invoice untuk PO ini sudah lengkap..!" & Enter2Baris &
                            "Silakan tambahkan PO yang lain.")
            End If
        Else
            If UangMuka_Asing > 0 And CekInvoice(TahapTermin_UangMuka) = False Then
                TahapTermin = TahapTermin_UangMuka
                Termin_Asing = UangMuka_Asing
                IsiValueTermin()
            ElseIf Termin1_Asing > 0 And CekInvoice(TahapTermin_Termin1) = False Then
                TahapTermin = TahapTermin_Termin1
                Termin_Asing = Termin1_Asing
                IsiValueTermin()
            ElseIf Termin2_Asing > 0 And CekInvoice(TahapTermin_Termin2) = False Then
                TahapTermin = TahapTermin_Termin2
                Termin_Asing = Termin2_Asing
                IsiValueTermin()
            ElseIf Pelunasan_Asing > 0 And CekInvoice(TahapTermin_Pelunasan) = False Then
                TahapTermin = TahapTermin_Pelunasan
                Termin_Asing = Pelunasan_Asing
                IsiValueTermin()
            Else
                POMasihTersedia = False
                txt_NomorPO.Text = Kosongan
                Kosongkan_TabelSJBAST()
                Kosongkan_TabelProduk()
                PesanPeringatan("Invoice untuk PO ini sudah lengkap..!" & Enter2Baris &
                            "Silakan tambahkan PO yang lain.")
            End If
        End If
    End Sub

    Sub IsiValueTermin()
        lbl_Termin.Text = TahapTermin
        Select Case BasisPerhitunganTermin
            Case BasisPerhitunganTermin_Prosentase
                Termin_Rp = JumlahNota * Termin_Persen / 100
                Termin_Asing = JumlahNota_Asing * Termin_Persen / 100
            Case BasisPerhitunganTermin_Nominal
                If PembelianLokal Then Termin_Persen = (Termin_Rp / JumlahNota) * 100
                If PembelianImpor Then Termin_Persen = (Termin_Asing / JumlahNota_Asing) * 100
        End Select
        VisibilitasBarisTermin(True)
    End Sub

    Function CekInvoice(TahapTermin)
        Dim InvoiceSudahAda = False
        BukaDatabaseTransaksi_Kondisional()
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_PO_Produk = '" & NomorPOProduk & "' " &
                              " AND Metode_Pembayaran = '" & MetodePembayaran_Termin & "' " &
                              " AND Tahap_Termin = '" & TahapTermin & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then InvoiceSudahAda = True
        TutupDatabaseTransaksi_Kondisional()
        Return InvoiceSudahAda
    End Function

    Sub LogikaTampilanKolomDPPBarangDanJasa()
        lbl_DPPBarang.Visibility = Visibility.Collapsed
        lbl_DPPJasa.Visibility = Visibility.Collapsed
        txt_DPPBarang.Visibility = Visibility.Collapsed
        txt_DPPJasa.Visibility = Visibility.Collapsed
        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            If PembelianLokal Then
                If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PembelianLokal) Then
                    lbl_DPPBarang.Visibility = Visibility.Visible
                    lbl_DPPJasa.Visibility = Visibility.Visible
                    txt_DPPBarang.Visibility = Visibility.Visible
                    txt_DPPJasa.Visibility = Visibility.Visible
                End If
            End If
        End If
    End Sub


    Private Sub cmb_JenisJasa_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisJasa.SelectionChanged
        JenisJasa = cmb_JenisJasa.SelectedValue
        LogikaAdaPPh(True)
        If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then
            If JenisJasa = JenisJasa_Dividen _
                Or JenisJasa = JenisJasa_LabaPajakBUT _
                Or JenisJasa = JenisJasa_BungaBagiHasil _
                Then
                IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN_NonPPN)
            Else
                KontenCombo_JenisPPN()
            End If
        End If
        KondisiFormSetelahPerubahan()
    End Sub




    Private Sub cmb_JenisPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPN.SelectionChanged
        JenisPPN = cmb_JenisPPN.SelectedValue
        If JenisPPN = JenisPPN_NonPPN Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        KontenCombo_PPNDikreditkan()
        KondisiFormSetelahPerubahan()
        LogikaAdaPPh(True)
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub cmb_PerlakuanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PerlakuanPPN.SelectionChanged
        PerlakuanPPN = cmb_PerlakuanPPN.SelectedValue
        If PerlakuanPPN = PerlakuanPPN_Dibebaskan Then
            txt_TarifPPN.Text = Kosongan
            txt_TarifPPN_11Per12.Text = Kosongan
            lbl_NomorSKB.Visibility = Visibility.Visible
            txt_NomorSKB.Visibility = Visibility.Visible
            lbl_TanggalSKB.Visibility = Visibility.Visible
            dtp_TanggalSKB.Visibility = Visibility.Visible
        Else
            txt_NomorSKB.Text = Kosongan
            dtp_TanggalSKB.Text = Kosongan
            lbl_NomorSKB.Visibility = Visibility.Collapsed
            txt_NomorSKB.Visibility = Visibility.Collapsed
            lbl_TanggalSKB.Visibility = Visibility.Collapsed
            dtp_TanggalSKB.Visibility = Visibility.Collapsed
        End If
        LogikaAdaPPh(True)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub

    Private Sub cmb_PPNDikreditkan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PPNDikreditkan.SelectionChanged
        PPNDikreditkan = cmb_PPNDikreditkan.SelectedValue
        KontenCombo_PilihanPPN()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub cmb_PilihanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PilihanPPN.SelectionChanged
        PilihanPPN = cmb_PilihanPPN.SelectedValue
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub
    Private Sub dtp_TanggalFakturPajak_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalFakturPajak.SelectedDateChanged
        If dtp_TanggalFakturPajak.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalInvoice)
            TanggalFakturPajak = dtp_TanggalFakturPajak.SelectedDate
        End If
    End Sub


    Private Sub txt_NomorSKB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorSKB.TextChanged
        NomorSKB = txt_NomorSKB.Text
    End Sub
    Private Sub dtp_TanggalSKB_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalSKB.SelectedDateChanged
        If dtp_TanggalSKB.Text <> Kosongan Then
            TanggalSKB = dtp_TanggalSKB.SelectedDate
        End If
    End Sub


    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL And dtp_TanggalInvoice.Text = Kosongan Then
            cmb_KodeMataUang.SelectedValue = Kosongan
            If Not (ProsesLoadingForm Or ProsesResetForm) Then PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalInvoice, "Tanggal Invoice")
            Return
        End If
        KodeMataUang = cmb_KodeMataUang.SelectedValue
        PenentuanKurs()
        If AsalPembelian = AsalPembelian_Impor Then
            Harga_Satuan_Asing.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item_Asing.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Asing.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga_Asing.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_Kurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_TotalTagihanIDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihanIDR.TextChanged

    End Sub


    Private Sub dtp_TanggalSerahTerima_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalSerahTerima.SelectedDateChanged
        If dtp_TanggalSerahTerima.Text = Kosongan Then
            TanggalSerahTerima = TanggalKosong
        Else
            KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(dtp_TanggalSerahTerima, dtp_TanggalInvoice.SelectedDate.Value.Day, dtp_TanggalInvoice.SelectedDate.Value.Month, dtp_TanggalInvoice.SelectedDate.Value.Year)
            TanggalSerahTerima = dtp_TanggalSerahTerima.SelectedDate
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub cmb_Loko_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Loko.SelectionChanged
        Loko = cmb_Loko.SelectedValue
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub rdb_JumlahHariJatuhTempo_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_JumlahHariJatuhTempo.Checked
        If rdb_JumlahHariJatuhTempo.IsChecked = True Then
            txt_JumlahHariJatuhTempo.IsEnabled = True
            txt_JumlahHariJatuhTempo.Focus()
            lbl_JumlahHariJatuhTempo.IsEnabled = True
            dtp_TanggalJatuhTempo.Text = Kosongan
            dtp_TanggalJatuhTempo.IsEnabled = False
            LogikaJenisPembelian()
        Else
            txt_JumlahHariJatuhTempo.IsEnabled = False
            lbl_JumlahHariJatuhTempo.IsEnabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub rdb_TanggalJatuhTempo_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_TanggalJatuhTempo.Checked
        If rdb_TanggalJatuhTempo.IsChecked = True Then
            txt_JumlahHariJatuhTempo.Text = Kosongan
            txt_JumlahHariJatuhTempo.IsEnabled = False
            lbl_JumlahHariJatuhTempo.IsEnabled = False
            dtp_TanggalJatuhTempo.IsEnabled = True
        Else
            dtp_TanggalJatuhTempo.IsEnabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub txt_JumlahHariJatuhTempo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHariJatuhTempo.TextChanged
        JumlahHariJatuhTempo = AmbilAngka(txt_JumlahHariJatuhTempo.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHariJatuhTempo)
        If JumlahHariJatuhTempo > 0 Then
            rdb_JumlahHariJatuhTempo.IsChecked = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.Text <> Kosongan And dtp_TanggalDiterimaInvoice.Text <> Kosongan Then
            KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(dtp_TanggalJatuhTempo,
                                                               dtp_TanggalDiterimaInvoice.SelectedDate.Value.Day,
                                                               dtp_TanggalDiterimaInvoice.SelectedDate.Value.Month,
                                                               dtp_TanggalDiterimaInvoice.SelectedDate.Value.Year)
            TanggalJatuhTempo = dtp_TanggalJatuhTempo.SelectedDate
            KondisiFormSetelahPerubahan()
            LogikaJenisPembelian()
        End If
    End Sub

    Sub LogikaJenisPembelian()
        If TanggalJatuhTempo = TanggalDiterimaInvoice Or TanggalJatuhTempo = TanggalInvoice Then
            If NP = "N" Then KondisiForm_JenisPembelianTunai()
        Else
            If NP = "N" Then KondisiForm_JenisPembelianTempo()
        End If
        LogikaTampilanKolomImporTermin()
    End Sub
    Dim JumlahHutangUsaha_Backup
    Sub KondisiForm_JenisPembelianTunai()
        JenisPembelian = JenisPembelian_Tunai
        VisibilitasSaranaPembayaran(True)
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
        VisibilitasKolomJumlahHutangUsaha(False)
    End Sub
    Sub KondisiForm_JenisPembelianTempo()
        JenisPembelian = JenisPembelian_Tempo
        VisibilitasSaranaPembayaran(False)
        Reset_grb_Bank()
        KosongkanItemCombo(cmb_SaranaPembayaran)
        LogikaCOAKredit_UntukNonTunai()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        VisibilitasKolomJumlahHutangUsaha(True)
    End Sub

    Sub VisibilitasSaranaPembayaran(Visibilitas)
        If Visibilitas Then
            lbl_SaranaPembayaran.Visibility = Visibility.Visible
            cmb_SaranaPembayaran.Visibility = Visibility.Visible
        Else
            lbl_SaranaPembayaran.Visibility = Visibility.Collapsed
            cmb_SaranaPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomJumlahHutangUsaha(Visibilitas)
        If Visibilitas And PembelianLokal Then
            lbl_JumlahHutangUsaha.Visibility = Visibility.Visible
            txt_JumlahHutangUsaha.Visibility = Visibility.Visible
        Else
            lbl_JumlahHutangUsaha.Visibility = Visibility.Collapsed
            txt_JumlahHutangUsaha.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub DefaultValue_TanggalJatuhTempo()
        If FungsiForm = FungsiForm_TAMBAH Or ProsesResetForm = True Then
            Dim Bulan_TanggalJatuhTempo = dtp_TanggalDiterimaInvoice.SelectedDate.Value.Month + 1
            Dim Tahun_TanggalJatuhTempo = dtp_TanggalDiterimaInvoice.SelectedDate.Value.Year
            If Bulan_TanggalJatuhTempo = 13 Then
                Bulan_TanggalJatuhTempo = 1
                Tahun_TanggalJatuhTempo += 1
            End If
            dtp_TanggalJatuhTempo.SelectedDate = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_TanggalJatuhTempo, Tahun_TanggalJatuhTempo)
        End If
    End Sub


    Private Sub txt_Referensi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Referensi.TextChanged
        Referensi = txt_Referensi.Text
    End Sub

    Private Sub cmb_SaranaPembayaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        SaranaPembayaran = cmb_SaranaPembayaran.SelectedValue
        COAKredit = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        'If COATermasukBank(COAKredit) Then
        '    grb_Bank.Visibility = Visibility.Visible
        '    PembayaranViaBank = True
        '    KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
        '    Perhitungan_ValueBank()
        'Else
        '    Reset_grb_Bank()
        'End If
        '(Untuk saat ini, Sarana Pembayaran Bank dinonaktifkan).
        KondisiFormSetelahPerubahan()
    End Sub


    Public SaranaPembayaran As String
    Dim BiayaAdministrasiBank As Decimal
    Dim DitanggungOleh As String
    Dim PembayaranViaBank As Boolean
    Dim JumlahTransfer As Decimal
    Dim TotalBank As Decimal
    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Visibility = Visibility.Collapsed
        cmb_DitanggungOleh.IsEnabled = False
        txt_BiayaAdministrasiBank.Text = Kosongan
        txt_JumlahTransfer.Text = Kosongan
        KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
    End Sub


    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, TotalTagihan, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
    End Sub


    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = Ambilangka_MultiCurrency(LokasiWP, txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.IsEnabled = False
            cmb_DitanggungOleh.SelectedValue = Kosongan
        Else
            cmb_DitanggungOleh.IsEnabled = True
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DitanggungOleh.SelectionChanged
        DitanggungOleh = cmb_DitanggungOleh.SelectedValue
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_JumlahTransfer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransfer.TextChanged
        JumlahTransfer = Ambilangka_MultiCurrency(LokasiWP, txt_JumlahTransfer)
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_TotalBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalBank.TextChanged
        TotalBank = Ambilangka_MultiCurrency(LokasiWP, txt_TotalBank)
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub btn_TambahSJBAST_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahSJBAST.Click

        If KodeSupplier = Kosongan Then
            PesanPeringatan("Silakan pilih 'Supplier' terlebih dahulu.")
            btn_PilihMitra.Focus()
            Return
        End If

        If MetodePembayaran = MetodePembayaran_Normal Then
            If InvoiceDenganPO = True Then
                TambahSJBAST_FromList()
            Else
                TambahSJBAST_Manual()
            End If
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            TambahSJBAST_Manual()
        End If

        CekNomorSJBAST()
        BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_TabelSJBAST()
        KondisiFormSetelahPerubahan()

    End Sub

    Sub TambahSJBAST_Manual()
        win_InputSJBASTManual = New wpfWin_InputSJBASTManual
        win_InputSJBASTManual.ResetForm()
        If Not SupplierSebagaiPerusahaanLuarNegeri(KodeSupplier) Then
            win_InputSJBASTManual.Label_Nomor = "Nomor SJ/BAST :"
            win_InputSJBASTManual.Label_Tanggal = "Tanggal SJ/BAST :"
            win_InputSJBASTManual.Label_TanggalDiterima = "Tanggal Diterima :"
        Else
            win_InputSJBASTManual.Label_Nomor = "Nomor BL/AWB :"
            win_InputSJBASTManual.Label_Tanggal = "Tanggal BL/AWB :"
            win_InputSJBASTManual.Label_TanggalDiterima = "Tanggal Fiat Muat :"
        End If
        win_InputSJBASTManual.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputSJBASTManual.TanggalDiterima = TanggalInvoice
        win_InputSJBASTManual.TanggalSJBAST = TanggalInvoice
        win_InputSJBASTManual.ShowDialog()
        BersihkanSeleksi_TabelSJBAST()
    End Sub

    Sub TambahSJBAST_FromList()

        win_ListSJBAST = New wpfWin_ListSJBAST
        win_ListSJBAST.ResetForm()
        win_ListSJBAST.Sisi = win_ListSJBAST.SisiPembelian
        win_ListSJBAST.NamaMitra_Filter = NamaSupplier
        win_ListSJBAST.FilterMitra_Aktif = False
        If KunciTanggalInvoice = True Then
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = TanggalFormatTampilan(TanggalInvoice)
        Else
            win_ListSJBAST.PilihTanggalDiterimaSJBAST = win_ListSJBAST.TanggalSJBAST_Semua
        End If
        win_ListSJBAST.JenisProduk_Induk = JenisProduk_Induk
        win_ListSJBAST.JenisPPN = JenisPPN
        win_ListSJBAST.PerlakuanPPN = PerlakuanPPN
        win_ListSJBAST.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        If JumlahBarisSJBAST > 0 And
            (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi) Then
            'Untuk Saat ini, hanya produk 'Barang dan Jasa' dan 'Jasa Kontstruksi' yang menggunakan filter ini.
            'Kemungkinannya, nanti semua jenis produk harus difilter seperti ini.
            'Nunggu kepastian dari pa Aris dulu.
            'Kalau sudah OK bahwa semuanya harus difilter seperti ini, maka Logikanya cukup 'Jumlah BAST > 0' saja.
            win_ListSJBAST.NomorPO_HarusSama = True
            win_ListSJBAST.NomorPO_YangHarusDisamakan = NomorPO_TerakhirDitambahkan
        End If
        'Tambahkan nomor SJBAST yang sudah ada ke daftar singkiran :
        For Each row As DataRow In datatabelSJBAST.Rows
            win_ListSJBAST.ListNomorSJBAST_Singkirkan.Add(row("Nomor_SJ_BAST").ToString())
        Next
        win_ListSJBAST.ShowDialog()                                             '<---- Buka Form List SJ/BAST
        EksekusiLogikaAdaPPh = False
        NomorSJBAST = win_ListSJBAST.NomorSJBAST_Terseleksi
        If String.IsNullOrEmpty(NomorSJBAST) Then Return
        NomorSJBAST_TerakhirDitambahkan = NomorSJBAST
        NomorPO_TerakhirDitambahkan = win_ListSJBAST.NomorPO_Terseleksi
        TanggalSJBAST = win_ListSJBAST.TanggalSJBAST_Terseleksi
        TanggalDiterimaSJBAST = win_ListSJBAST.TanggalDiterima_Terseleksi
        Dim JenisSurat = win_ListSJBAST.JenisSurat_Terseleksi
        NomorPOProduk = Kosongan
        If JumlahBarisSJBAST > 0 Then
            If win_ListSJBAST.NomorPO_Terseleksi <> datatabelSJBAST.Rows(0)("Nomor_PO").ToString() Then
                MsgBox("Nomor Surat Jalan / BAST yang berbeda PO tidak dapat ditambahkan ke dalam daftar..!")
                Return
            End If
        End If
        datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, win_ListSJBAST.NomorPO_Terseleksi, win_ListSJBAST.OngkosKirim_Terseleksi)        '<-- Penambahan Baris SJ/BAST
        If (JenisProduk_Induk = JenisProduk_Jasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi) And JenisInvoice = JenisInvoice_Biasa Then
            btn_TambahSJBAST.IsEnabled = False
        End If
        ProsesIsiValueForm = True
        BersihkanSeleksi_TabelSJBAST()
        Dim Tabel = Kosongan
        Dim Kolom = Kosongan
        Select Case JenisSurat
            Case GantiTeks(AwalanSJ, "-", Kosongan)
                Tabel = "tbl_Pembelian_SJ"
                Kolom = "Nomor_SJ"
            Case GantiTeks(AwalanBAST, "-", Kosongan)
                Tabel = "tbl_Pembelian_BAST"
                Kolom = "Nomor_BAST"
        End Select
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM " & Tabel &
                              " WHERE " & Kolom & " = '" & NomorSJBAST & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = datatabelUtama.Rows.Count
        Dim TarifPPNManual As Decimal
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
            cmb_KodeMataUang.SelectedValue = drTELUSUR.Item("Kode_Mata_Uang")
            HargaSatuan = drTELUSUR.Item("Harga_Satuan")
            HargaSatuan_Asing = drTELUSUR.Item("Harga_Satuan_Asing")
            Dim JumlahHarga_PerItem As Int64 = JumlahProduk_PerItem * HargaSatuan
            Dim JumlahHargaPerItem_Asing As Decimal = JumlahProduk_PerItem * HargaSatuan_Asing
            DiskonPerItem_Persen = PembulatanDesimal2Digit(drTELUSUR.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHarga_PerItem * (DiskonPerItem_Persen / 100)
            Dim DiskonPerItem_Asing As Decimal = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            Dim TotalHarga_PerItem As Int64 = JumlahHarga_PerItem - DiskonPerItem_Rp
            Dim TotalHargaPerItem_Asing As Decimal = JumlahHargaPerItem_Asing - DiskonPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPOProduk, Kosongan, NamaProduk, DeskripsiProduk,
                                    JumlahProduk_PerItem, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHarga_PerItem, JumlahHargaPerItem_Asing,
                                    DiskonPerItem_Persen, DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHarga_PerItem, TotalHargaPerItem_Asing, KodeProjectProduk,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan)
            DPPJasa_BerdasarkanPO = drTELUSUR.Item("DPP_Jasa")
            JenisJasa = AmbilValue_JenisJasaBerdasarkanPOPembelian(NomorPOProduk)
            TarifPPNManual = PembulatanDesimal2Digit((100 * drTELUSUR.Item("PPN")) / drTELUSUR.Item("Dasar_Pengenaan_Pajak"))
            JenisPPh = drTELUSUR.Item("Jenis_PPh")
            KodeSetoran = drTELUSUR.Item("Kode_Setoran")
            TarifPPh = PembulatanDesimal2Digit(drTELUSUR.Item("Tarif_PPh"))
            PPhTerutang = drTELUSUR.Item("PPh_Terutang")
            PPhDitanggung_BerdasarkanPO = drTELUSUR.Item("PPh_Ditanggung")
            If TarifPPh > 0 Then AdaPPh = True
            If TarifPPN < 10 Then
                txt_TarifPPN.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak.Visibility = Visibility.Visible
                txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
                txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            End If
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
        Title = JudulForm
        'IsiValueComboBypassTerkunci(cmb_JenisJasa, JenisJasa) (Ini bikin nge-loop, dan sebetulnya juga tidak diperlukan).
        IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN)
        IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, PerlakuanPPN)
        IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh)
        IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran)
        If JenisProdukPerItem <> JenisProduk_Barang Then
            txt_TarifPPh.Text = PembulatanDesimal2Digit(TarifPPh)
        End If
        'Untuk Value PPh Ditanggung, ada di Sub Perhitungan. Jangan ditempatkan di sini.
        'Untuk value Biaya Transportasi Pembelian, sudah ada di Sub Perhitungan tersendiri.
        txt_OngkosKirim.Text = OngkosKirim
        If JenisInvoice = JenisInvoice_Biasa Then
            EksekusiKode = False
            dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalDiterimaSJBAST)
            EksekusiKode = True
            KunciTanggalInvoice = True
        Else
            KunciTanggalInvoice = False
        End If
        AksesDatabase_Transaksi(Tutup)
        CekNomorSJBAST()
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
        EksekusiLogikaAdaPPh = True
        LogikaAdaPPh(True)
        ProsesIsiValueForm = False
        'PesanUntukProgrammer("Jenis Jasa : " & JenisJasa & Enter2Baris &
        '                     "Jenis Produk Per Item : " & JenisProdukPerItem & Enter2Baris &
        '                     "Ada PPh : " & AdaPPh & Enter2Baris &
        '                     "Jenis PPh : " & JenisPPh & Enter2Baris &
        '                     "Tarif PPh : " & TarifPPh & Enter2Baris &
        '                     "Kode Setoran : " & KodeSetoran)
        txt_TarifPPN.Text = TarifPPNManual
        EksekusiKodeLogikaPPN = False
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub

    Private Sub btn_SingkirkanSJBAST_Click(sender As Object, e As RoutedEventArgs) Handles btn_SingkirkanSJBAST.Click
        Dim NomorSJBAST_UntukDihapus = rowviewSJBAST("Nomor_SJ_BAST")
        rowviewSJBAST.Delete()
        BersihkanSeleksi_TabelSJBAST()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = datatabelUtama.Rows(i)
            If row("Nomor_SJ_BAST_Produk") = NomorSJBAST_UntukDihapus Then
                row.Delete()
            End If
        Next
        NomorUrutProduk = 0
        For Each row As DataRow In datatabelUtama.Rows
            NomorUrutProduk += 1
            row("Nomor_Urut") = NomorUrutProduk
        Next
        BersihkanSeleksi_TabelProduk()
        If JumlahBarisSJBAST = 0 Then
            If MetodePembayaran = MetodePembayaran_Normal Then
                KontenCombo_JenisPPN()
                KontenCombo_PerlakuanPPN_Kosongan()
                JenisPPN = Kosongan
                PerlakuanPPN = Kosongan
                KunciTanggalInvoice = False
            End If
            btn_TambahSJBAST.IsEnabled = True
        End If
        CekNomorSJBAST()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        If MetodePembayaran = MetodePembayaran_Termin Then
            PesanPeringatan("Anda mengahapus Surat Jalan/BAST." & Enter2Baris & "Nomor PO dibatalkan.")
            VisibilitasTabelSJBAST(False)
            txt_NomorPO.Text = Kosongan
            KosongkanKolomPerhitungan()
        End If
        BarisTotalTabel()
        KondisiFormSetelahPerubahan()
    End Sub

    Dim NomorSJBAST_Aktif
    Dim TanggalSJBAST_Aktif
    Dim TanggalDiterimaSJBAST_Aktif
    Sub CekNomorSJBAST()
        NomorSJBAST_Aktif = Kosongan
        TanggalSJBAST_Aktif = Kosongan
        TanggalDiterimaSJBAST_Aktif = Kosongan
        JumlahBarisSJBAST = 0
        For Each row As DataRow In datatabelSJBAST.Rows
            NomorSJBAST_Aktif = row("Nomor_SJ_BAST")
            TanggalSJBAST_Aktif = row("Tanggal_SJ_BAST")
            TanggalDiterimaSJBAST_Aktif = row("Tanggal_Diterima")
            JumlahBarisSJBAST += 1
        Next
        If JenisProduk_Induk = JenisProduk_Jasa Then
            If JumlahBarisSJBAST > 0 Then
                btn_TambahSJBAST.IsEnabled = False
            Else
                btn_TambahSJBAST.IsEnabled = True
            End If
        End If
        If JumlahBarisSJBAST > 0 Then
            dtp_TanggalInvoice.IsEnabled = False
            If InvoiceDenganPO = False Or MetodePembayaran = MetodePembayaran_Termin Then
                btn_TambahSJBAST.IsEnabled = False
            End If
        Else
            dtp_TanggalInvoice.IsEnabled = True
        End If
    End Sub


    Private Sub datagridSJBAST_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridSJBAST.SelectionChanged
    End Sub
    Private Sub datagridSJBAST_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridSJBAST.PreviewMouseLeftButtonUp
        HeaderKolomSJBAST = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomSJBAST IsNot Nothing Then
            BersihkanSeleksi_TabelSJBAST()
        End If
    End Sub
    Private Sub datagridSJBAST_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridSJBAST.SelectedCellsChanged

        KolomTerseleksiSJBAST = datagridSJBAST.CurrentColumn
        BarisTerseleksiSJBAST = datagridSJBAST.SelectedIndex
        If BarisTerseleksiSJBAST < 0 Then Return
        rowviewSJBAST = TryCast(datagridSJBAST.SelectedItem, DataRowView)
        If Not rowviewSJBAST IsNot Nothing Then Return

        If datatabelSJBAST.Rows.Count = 0 Then Return
        NomorSJBAST_Terseleksi = rowviewSJBAST("Nomor_SJ_BAST")
        If BarisTerseleksiSJBAST >= 0 Then
            btn_SingkirkanSJBAST.IsEnabled = True
        Else
            btn_SingkirkanSJBAST.IsEnabled = False
        End If

    End Sub
    Private Sub datagridSJBAST_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridSJBAST.MouseDoubleClick
    End Sub



    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub

    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi_TabelProduk()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorUrutProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        JenisProdukPerItem_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_Produk_Per_Item")
        NomorSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_SJ_BAST_Produk")
        TanggalSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_SJ_BAST_Produk")
        TanggalDiterimaSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Diterima_SJ_BAST_Produk")
        KodeProjectProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project_Produk")
        COAProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Produk")
        COAProdukTerseleksi_Sebelumnya = COAProduk_Terseleksi
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        DeskripsiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Deskripsi_Produk")
        JumlahProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk"))
        SatuanProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Satuan_Produk")
        HargaSatuan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan"))
        DiskonPerItem_Persen_Terseleksi = GantiTeks(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Rp"))
        TotalHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga"))
        KelompokAsset_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kelompok_Asset"))
        KodeDivisiAsset_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Divisi_Asset")
        COABiaya_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Biaya")
        TanggalMulaiAmortisasi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Mulai_Amortisasi")
        MasaAmortisasi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Masa_Amortisasi"))
        'Asing :
        HargaSatuan_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan_Asing"))
        JumlahHarga_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga_Per_Item_Asing"))
        DiskonPerItem_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Asing"))
        TotalHarga_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga_Asing"))

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If Not IsNothing(KolomTerseleksi) Then
                If NomorUrutProduk_Terseleksi > 0 And KolomTerseleksi.DisplayIndex = COA_Produk.DisplayIndex Then
                    win_ListCOA = New wpfWin_ListCOA
                    win_ListCOA.ResetForm()
                    If COAProduk_Terseleksi <> Kosongan Then
                        win_ListCOA.COATerseleksi = COAProduk_Terseleksi
                        win_ListCOA.txt_CariAkun.Text = COAProduk_Terseleksi
                    End If
                    win_ListCOA.ListAkun = ListAkun_Pembelian
                    win_ListCOA.ShowDialog()
                    COAProduk_Terseleksi = win_ListCOA.COATerseleksi
                    rowviewUtama("COA_Produk") = COAProduk_Terseleksi
                    'Jika Termasuk COA Asset :
                    If COATermasukAsset(COAProduk_Terseleksi) Then
                        If COAProduk_Terseleksi <> COAProdukTerseleksi_Sebelumnya _
                        Or KelompokAsset_Terseleksi = 0 _
                        Or KodeDivisiAsset_Terseleksi = Kosongan _
                        Then
                            BukaForm_DataAsset()
                            COATermasukBank(COAProduk_Terseleksi)
                        End If
                    Else
                        rowviewUtama("Kelompok_Asset") = 0
                        rowviewUtama("Kode_Divisi_Asset") = Kosongan
                    End If
                    'Jika Termasuk COA Biaya Dibayar Dimuka :
                    If COATermasukBiayaDibayarDimuka(COAProduk_Terseleksi) Then
                        If COAProduk_Terseleksi <> COAProdukTerseleksi_Sebelumnya _
                        Or COABiaya_Terseleksi = Kosongan _
                        Then
                            BukaForm_DataAmortisasiBiaya()
                        End If
                    Else
                        rowviewUtama("COA_Biaya") = Kosongan
                    End If
                    'Jika Termasuk Peruntukan Project :
                    If AmbilTeksKiri(COAProduk_Terseleksi, 1) = "5" Then
                        If KodeProjectProduk_Terseleksi = Kosongan Then
                            BukaForm_ListDataProject()
                        End If
                    Else
                        rowviewUtama("Kode_Project_Produk") = Kosongan
                    End If
                End If
            End If
        End If

        If BarisTerseleksi >= 0 Then
            btn_Perbaiki.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        Else
            btn_Perbaiki.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
        End If

        If NomorUrutProduk_Terseleksi > 0 Then
            btn_Perbaiki.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        Else
            btn_Perbaiki.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If InvoiceDenganPO Then Return
        If MetodePembayaran = MetodePembayaran_Termin Then Return
        btn_Perbaiki_Click(sender, e)
    End Sub


    Sub BukaForm_DataAsset()
        win_InputDataAsset = New wpfWin_InputDataAsset
        win_InputDataAsset.ResetForm()
        win_InputDataAsset.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputDataAsset.FungsiForm = FungsiForm
        win_InputDataAsset.txt_NamaAktiva.Text = NamaProduk_Terseleksi
        win_InputDataAsset.txt_COA_Asset.Text = COAProduk_Terseleksi
        win_InputDataAsset.txt_HargaPerolehan.Text = HargaSatuan_Terseleksi - (HargaSatuan_Terseleksi * DiskonPerItem_Persen_Terseleksi / 100)
        If COAProduk_Terseleksi = COAProdukTerseleksi_Sebelumnya Then
            If KelompokAsset_Terseleksi > 0 Then
                win_InputDataAsset.cmb_KelompokHarta.SelectedValue = KonversiAngkaKeKelompokHarta(KelompokAsset_Terseleksi)
            End If
            If KodeDivisiAsset_Terseleksi <> Kosongan Then
                win_InputDataAsset.cmb_Divisi.SelectedValue = KodeDivisiAsset_Terseleksi & " - " &
                AmbilValue_DivisiAsset(KodeDivisiAsset_Terseleksi)
            End If
        End If
        PesanPemberitahuan("Untuk pembelian asset, silakan lengkapi 'Data Asset' berikut ini.")
        win_InputDataAsset.ShowDialog()
    End Sub

    Sub BukaForm_DataAmortisasiBiaya()
        win_InputAmortisasiBiaya = New wpfWin_InputAmortisasiBiaya
        win_InputAmortisasiBiaya.ResetForm()
        win_InputAmortisasiBiaya.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputAmortisasiBiaya.FungsiForm = FungsiForm
        win_InputAmortisasiBiaya.txt_COA_Amortisasi.Text = COAProduk_Terseleksi
        win_InputAmortisasiBiaya.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalInvoice)
        If TanggalMulaiAmortisasi_Terseleksi <> Kosongan Then
            win_InputAmortisasiBiaya.dtp_TanggalMulai.SelectedDate = TanggalFormatWPF(TanggalMulaiAmortisasi_Terseleksi)
        End If
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
            rowviewUtama("Kode_Project_Produk") = Kosongan
            PesanPeringatan("Tidak ada 'Kode Project' yang dipilih." & Enter2Baris & "COA Kepala 5 dibatalkan..!")
            rowviewUtama("COA_Produk") = Kosongan
        Else
            rowviewUtama("Kode_Project_Produk") = frm_ListDataProject.KodeProject_Terseleksi
        End If
    End Sub



    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If PembelianLokal Then
                If InvoiceDenganPO = False And JumlahBarisSJBAST = 0 Then
                    PesanPeringatan("Silakan isi terlebih dahulu tabel Surat Jalan/BAST")
                    btn_TambahSJBAST.Focus()
                    Return
                End If
            End If
        End If
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
        CekNomorSJBAST()
        win_InputProduk_Nota = New wpfWin_InputProduk_Nota
        win_InputProduk_Nota.ResetForm()
        win_InputProduk_Nota.MataUang = MataUang
        win_InputProduk_Nota.txt_NomorUrut.Text = JumlahProduk + 1
        win_InputProduk_Nota.FungsiForm = FungsiForm_TAMBAH
        win_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        win_InputProduk_Nota.InvoiceDenganPO = InvoiceDenganPO
        win_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputProduk_Nota.NomorSJBAST = NomorSJBAST_Aktif
        win_InputProduk_Nota.TanggalSJBAST = TanggalSJBAST_Aktif
        win_InputProduk_Nota.TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Aktif
        win_InputProduk_Nota.ShowDialog()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        BersihkanSeleksi_TabelProduk()
    End Sub


    Private Sub btn_Perbaiki_Click(sender As Object, e As RoutedEventArgs) Handles btn_Perbaiki.Click
        win_InputProduk_Nota = New wpfWin_InputProduk_Nota
        win_InputProduk_Nota.ResetForm()
        win_InputProduk_Nota.MataUang = MataUang
        win_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        win_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        win_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        win_InputProduk_Nota.JenisProduk_PerItem = JenisProdukPerItem_Terseleksi
        win_InputProduk_Nota.NomorSJBAST = NomorSJBAST_Terseleksi
        win_InputProduk_Nota.TanggalSJBAST = TanggalSJBAST_Terseleksi
        win_InputProduk_Nota.TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Terseleksi
        win_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        win_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        win_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        win_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        win_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        win_InputProduk_Nota.txt_HargaSatuan_Asing.Text = HargaSatuan_Asing_Terseleksi
        win_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        If KodeProjectProduk_Terseleksi = Kosongan Then
            win_InputProduk_Nota.cmb_Peruntukan.SelectedValue = Peruntukan_NonProject
            win_InputProduk_Nota.txt_KodeProject.Text = Kosongan
        Else
            win_InputProduk_Nota.cmb_Peruntukan.SelectedValue = Peruntukan_Project
            win_InputProduk_Nota.txt_KodeProject.Text = KodeProjectProduk_Terseleksi
        End If
        win_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_InputProduk_Nota.ShowDialog()
        If win_InputProduk_Nota.Proses = False Then Return
        If datatabelUtama.Rows.Count > 0 Then '(Ini untuk mencegah eksekusi kode di bawah, saat jalur masuk melalui form dialog WPF).
            rowviewUtama("Jenis_Produk_Per_Item") = win_InputProduk_Nota.JenisProduk_PerItem
            rowviewUtama("Nama_Produk") = win_InputProduk_Nota.NamaProduk
            rowviewUtama("Deskripsi_Produk") = win_InputProduk_Nota.DeskripsiProduk
            rowviewUtama("Jumlah_Produk") = win_InputProduk_Nota.JumlahProduk
            rowviewUtama("Satuan_Produk") = win_InputProduk_Nota.SatuanProduk
            rowviewUtama("Harga_Satuan") = win_InputProduk_Nota.HargaSatuan
            rowviewUtama("Harga_Satuan_Asing") = win_InputProduk_Nota.HargaSatuan_Asing
            rowviewUtama("Jumlah_Harga_Per_Item") = win_InputProduk_Nota.JumlahHarga
            rowviewUtama("Jumlah_Harga_Per_Item_Asing") = win_InputProduk_Nota.JumlahHarga_Asing
            rowviewUtama("Diskon_Per_Item_Persen") = (PembulatanDesimal2Digit(win_InputProduk_Nota.DiskonPerItem_Persen) & " %")
            rowviewUtama("Diskon_Per_Item_Rp") = win_InputProduk_Nota.DiskonPerItem_Rp
            rowviewUtama("Diskon_Per_Item_Asing") = win_InputProduk_Nota.DiskonPerItem_Asing
            rowviewUtama("Total_Harga") = win_InputProduk_Nota.TotalHarga
            rowviewUtama("Total_Harga_Asing") = win_InputProduk_Nota.TotalHarga_Asing
            rowviewUtama("Kode_Project_Produk") = win_InputProduk_Nota.KodeProject
            If win_InputProduk_Nota.JenisProduk_PerItem = JenisProduk_Jasa Then
                For Each row As DataRow In datatabelUtama.Rows
                    If row("Jenis_Produk_Per_Item") = JenisProduk_Jasa Then row("Nama_Produk") = win_InputProduk_Nota.NamaProduk
                Next
            End If
        End If
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub btn_Singkirkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Singkirkan.Click
        Pilihan = MessageBox.Show("Yakin akan menyingkirkan item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        rowviewUtama.Delete()
        Dim i = 0
        For Each row As DataRow In datatabelUtama.Rows
            i += 1
            row("Nomor_Urut") = i
        Next
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        KondisiFormSetelahPerubahan()
        BarisTotalTabel()
    End Sub


    Private Sub txt_JumlahNota_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahNota.TextChanged
        If PembelianLokal Then JumlahNota = AmbilAngka(txt_JumlahNota.Text)
        If PembelianImpor Then JumlahNota_Asing = AmbilAngka_Asing(txt_JumlahNota.Text)
    End Sub


    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Diskon_Persen, Diskon_Persen)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_Diskon_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Diskon_Persen.PreviewTextInput
    End Sub

    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Rp.TextChanged
        If PembelianLokal Then Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
        If PembelianImpor Then DiskonAsing = AmbilAngka_Asing(txt_Diskon_Rp.Text)
    End Sub


    Private Sub txt_UangMukaPlusTermin_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMukaPlusTermin_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_UangMukaPlusTermin_Persen, UangMukaPlusTermin_Persen)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_UangMukaPlusTermin_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_UangMukaPlusTermin_Persen.PreviewTextInput
    End Sub

    Private Sub txt_UangMukaPlusTermin_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMukaPlusTermin_Rp.TextChanged
        If PembelianLokal Then UangMukaPlusTermin_Rp = AmbilAngka(txt_UangMukaPlusTermin_Rp.Text)
        If PembelianImpor Then UangMukaPlusTermin_Asing = AmbilAngka_Asing(txt_UangMukaPlusTermin_Rp.Text)
    End Sub


    Private Sub txt_Termin_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Termin_Persen, Termin_Persen)
        KondisiFormSetelahPerubahan()
        'If BasisPerhitunganTermin = BasisPerhitunganTermin_Prosentase Then Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_Termin_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Termin_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Termin_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin_Rp.TextChanged
        If PembelianLokal Then Termin_Rp = AmbilAngka(txt_Termin_Rp.Text)
        If PembelianImpor Then Termin_Asing = AmbilAngka_Asing(txt_Termin_Rp.Text)
    End Sub


    Private Sub txt_Insurance_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Insurance.TextChanged
        If PembelianImpor Then Insurance = AmbilAngka_Asing(txt_Insurance.Text)
        EksekusiKodeLogikaPPN = False
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub txt_Freight_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Freight.TextChanged
        If PembelianImpor Then Freight = AmbilAngka_Asing(txt_Freight.Text)
        EksekusiKodeLogikaPPN = False
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub txt_BeaMasuk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BeaMasuk.TextChanged
        If PembelianImpor Then BeaMasuk = AmbilAngka_Asing(txt_BeaMasuk.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_KursKMK_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursKMK.TextChanged
        If PembelianImpor Then KursKMK = AmbilAngka_Asing(txt_KursKMK.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_DPPBarang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPBarang.TextChanged
        DPPBarang = AmbilAngka(txt_DPPBarang.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_DPPJasa_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPJasa.TextChanged
        DPPJasa = AmbilAngka(txt_DPPJasa.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
        If AsalPembelian = AsalPembelian_Impor Then
            EksekusiKodeLogikaPPN = False
            'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        End If
        KetersediaanTombolHitung(True)
    End Sub
    Private Sub txt_DasarPengenaanPajak_11Per12_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak_11Per12.TextChanged
    End Sub

    Dim EksekusiKodeLogikaPPN As Boolean
    Private Sub txt_TarifPPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPN.TextChanged
        TarifPPN = PembulatanDesimal2Digit(txt_TarifPPN.Text)
        If TarifPPN > 12 Then
            PesanPeringatan("Silakan isi kolom 'Tarif PPN' dengan benar!")
            txt_TarifPPN.Text = Kosongan
            txt_TarifPPN.Focus()
            Return
        End If
        If Not EksekusiKodeLogikaPPN Then
            Perhitungan_DenganTarifPPN_Manual()
        End If
        TextBoxFormatPersen_WPF(txt_TarifPPN, TarifPPN)
        KetersediaanTombolHitung(True)
    End Sub
    Private Sub txt_TarifPPN_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPN.PreviewTextInput
        EksekusiKodeLogikaPPN = False
    End Sub
    Private Sub txt_TarifPPN_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_TarifPPN.PreviewKeyDown
        EksekusiKodeLogikaPPN = False
    End Sub
    Private Sub txt_TarifPPN_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_TarifPPN.LostFocus
        EksekusiKodeLogikaPPN = False
        Perhitungan_DenganTarifPPN_Manual()
        KetersediaanTombolHitung(True)
    End Sub
    Sub Perhitungan_DenganTarifPPN_Manual()
        If TarifPPN = 12 Then
            txt_PPN.Text = Convert.ToInt64(DPP * Persen(TarifPPN))
            txt_DasarPengenaanPajak_11Per12.Text = (11 / 12 * DPP)
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
            txt_TarifPPN.Text = 11
        Else
            txt_PPN.Text = Convert.ToInt64(DPP * Persen(TarifPPN))
            txt_DasarPengenaanPajak_11Per12.Text = DPP
            'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        End If
        'EksekusiKodeLogikaPPN = True
    End Sub
    Private Sub txt_TarifPPN_11Per12_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPN_11Per12.TextChanged
        If EksekusiKodeLogikaPPN Then Return
        txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        txt_TarifPPN.Visibility = Visibility.Visible
        txt_TarifPPN.Text = txt_TarifPPN_11Per12.Text
        txt_TarifPPN.Focus()
        txt_TarifPPN_11Per12.Text = Kosongan
        txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak.Visibility = Visibility.Visible
        KetersediaanTombolHitung(True)
    End Sub
    Private Sub txt_TarifPPN_11Per12_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_TarifPPN_11Per12.PreviewKeyDown
        EksekusiKodeLogikaPPN = False
    End Sub


    Private Sub txt_PPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPN.TextChanged
        PPN = AmbilAngka(txt_PPN.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_TotalTagihan_Kotor_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan_Kotor.TextChanged
        If PembelianLokal Then TotalTagihan_Kotor = AmbilAngka(txt_TotalTagihan_Kotor.Text)
        If PembelianImpor Then TotalTagihan_Kotor_Asing = AmbilAngka_Asing(txt_TotalTagihan_Kotor.Text)
    End Sub


    Private Sub cmb_JenisPPh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPh.SelectionChanged
        JenisPPh = cmb_JenisPPh.SelectedValue
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then
            If (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_Jasa) And JenisPPh = JenisPPh_NonPPh Then
                PesanUntukProgrammer("Pilihan ini harus dikonsultasikan dengan pihak terkait.")
            End If
        End If
        KetersediaanTombolHitung(True)
    End Sub

    Private Sub cmb_KodeSetoran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeSetoran.SelectionChanged
        KodeSetoran = cmb_KodeSetoran.SelectedValue
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        TextBoxFormatPersen_WPF(txt_TarifPPh, TarifPPh)
        If TarifPPh > 100 Then
            MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        If Not InvoiceDenganPO Then
            If JenisJasa = JenisJasa_Lainnya Then
                If TarifPPh = 0 Then
                    IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_NonPPh)
                    IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_Non)
                Else
                    If JenisWP = JenisWP_OrangPribadi Then
                        IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal21)
                        IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_100)
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal23)
                        IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_100)
                    End If
                End If
            End If
        End If
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_TarifPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPh.PreviewTextInput
    End Sub
    Private Sub txt_TarifPPh_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_TarifPPh.PreviewKeyDown
        EksekusiKodeLogikaPPN = False
    End Sub
    Private Sub txt_TarifPPh_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_TarifPPh.LostFocus
        EksekusiKodeLogikaPPN = False
    End Sub


    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        EksekusiKodeLogikaPPN = False
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        If PPhDipotong < 0 Then
            txt_PPhDitanggung.Text = 0
            txt_PPhDitanggung.Focus()
            MsgBox("Silakan isi kolom 'PPh Ditanggung' dengan benar!")
            Return
        End If
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
    End Sub


    Private Sub txt_OngkosKirim_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_OngkosKirim.TextChanged
        EksekusiKodeLogikaPPN = False
        If PembelianLokal Then OngkosKirim = AmbilAngka(txt_OngkosKirim.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_BiayaMaterai_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaMaterai.TextChanged
        EksekusiKodeLogikaPPN = False
        If PembelianLokal Then BiayaMaterai = AmbilAngka(txt_BiayaMaterai.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan.TextChanged
        If PembelianLokal Then TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
        If PembelianImpor Then TotalTagihan_Asing = AmbilAngka_Asing(txt_TotalTagihan.Text)
    End Sub

    Private Sub txt_JumlahHutangUsaha_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHutangUsaha.TextChanged
        If PembelianLokal Then JumlahHutangUsaha = AmbilAngka(txt_JumlahHutangUsaha.Text)
        If PembelianImpor Then JumlahHutangUsaha_Asing = AmbilAngka_Asing(txt_JumlahHutangUsaha.Text)
    End Sub


    Sub TambahBarisSusunJurnal(ByRef TabelSusunBarisJurnal_COASebelumnya As String, ByRef TabelSusunBarisJurnal_JumlahDebet As Decimal)

        'Tambah Baris COA :
        TabelSusunCOA.Rows.Add(TabelSusunBarisJurnal_COASebelumnya, TabelSusunBarisJurnal_JumlahDebet)

        'Jika ada selisih hitung karena permasalahan angka dibelakang koma, ...
        '...maka selisih tersebut dikoreksikan ke Harga Perolehan Asset paling akhir dalam satu COA.
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                                     " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                     " AND COA_Asset = '" & TabelSusunBarisJurnal_COASebelumnya & "' ", KoneksiDatabaseGeneral)
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
        Dim Selisih As Int64
        Selisih = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TabelSusunBarisJurnal_JumlahDebet) - HargaPerolehanAssetPerCOA
        Dim HargaPerolehanRevisi = HargaPerolehanSatuanTerakhirDitelusur + Selisih
        'PesanUntukProgrammer("Harga Perolehan Satuan : " & HargaPerolehanSatuan & Enter2Baris &
        '                     "Harga Perolehan Satuan Terakhir : " & HargaPerolehanSatuanTerakhirDitelusur & Enter2Baris &
        '                     "Harga Perolehan Total : " & HargaPerolehanAssetPerCOA & Enter2Baris &
        '                     "Jumlah Debet : " & TabelSusunBarisJurnal_JumlahDebet & Enter2Baris &
        '                     "Jumlah Debet (IDR): " & AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TabelSusunBarisJurnal_JumlahDebet) & Enter2Baris &
        '                     "Selisih : " & Selisih & Enter2Baris &
        '                     "Harga Perolehan Terakhir (Revisi) : " & HargaPerolehanRevisi)
        If AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TabelSusunBarisJurnal_JumlahDebet) <> HargaPerolehanAssetPerCOA Then
            cmdEDIT = New OdbcCommand(" UPDATE tbl_DataAsset SET Harga_Perolehan = '" & HargaPerolehanRevisi & "' " &
                                      " WHERE Nomor_ID = '" & NomorIdTerakhirDitelusur & "' ", KoneksiDatabaseGeneral)
            cmdEDIT_ExecuteNonQuery()
        End If
        TabelSusunBarisJurnal_JumlahDebet = 0   'Ini jangan dihapus...! Soalnya ini ByReff dan berfungsi untuk mereset (0) Jumlah Debet pada perhitungan selanjutnya

    End Sub


    Private Sub btn_Hitung_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hitung.Click
        Perhitungan()
        KetersediaanTombolHitung(False)
        KetersediaanTombolSimpan(True)
    End Sub


    Public BukaFormPengajuanPengeluaranBankCash As Boolean
    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        BukaFormPengajuanPengeluaranBankCash = False 'Pembaruan value Variabel

        AmbilValue_JenisWP_dan_LokasiWP(KodeSupplier, JenisWP, LokasiWP)
        Algoritma_OngkosKirim()

        If NomorInvoice = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Invoice'.")
            txt_NomorInvoice.Focus()
            Return
        End If

        If dtp_TanggalInvoice.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal Invoice'.")
            dtp_TanggalInvoice.Focus()
            Return
        End If

        If dtp_TanggalDiterimaInvoice.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal Diterima Invoice'.")
            dtp_TanggalDiterimaInvoice.Focus()
            Return
        End If

        If KodeSupplier = Kosongan Then
            MsgBox("silakan isi data 'Supplier'.")
            Return
        End If

        'PesanUntukProgrammer("Jenis Jasa : " & JenisJasa)

        If JenisProduk_Induk <> JenisProduk_Barang And JenisJasa = Kosongan Then
            cmb_JenisJasa.Focus()
            MsgBox("silakan pilih 'Jenis Jasa'.")
            Return
        End If

        If txt_NomorFakturPajak.Visibility = Visibility.Visible Then

            If NomorFakturPajak = Kosongan Then
                If PembelianLokal Then PesanPeringatan("Silakan isi 'Nomor Faktur Pajak'.")
                If PembelianImpor Then PesanPeringatan("Silakan isi 'Nomor PIB'.")
                txt_NomorFakturPajak.Focus()
                Return
            End If

            If dtp_TanggalFakturPajak.Text = Kosongan Then
                If PembelianLokal Then PesanPeringatan("Silakan isi 'Tanggal Faktur Pajak'.")
                If PembelianImpor Then PesanPeringatan("Silakan isi 'Tanggal PIB'.")
                dtp_TanggalFakturPajak.Focus()
                Return
            End If

            If AsalPembelian = AsalPembelian_Lokal And TanggalFakturPajak <> TanggalDiterimaInvoice Then
                PesanPeringatan("'Tanggal Faktur Pajak' harus sama dengan 'Tanggal Diterima Invioce'.")
                dtp_TanggalFakturPajak.Focus()
                Return
            End If

        Else

            TanggalFakturPajak = TanggalKosong
            NomorFakturPajak = Kosongan

        End If

        If dtp_TanggalFakturPajak.Text = Kosongan Then
            TanggalFakturPajak = TanggalKosong
            NomorFakturPajak = Kosongan
        End If

        If PerlakuanPPN = PerlakuanPPN_Dibebaskan Then

            If NomorSKB = Kosongan Then
                PesanPeringatan("Silakan isi 'Nomor SKB'.")
                txt_NomorSKB.Focus()
                Return
            End If

            If dtp_TanggalSKB.Text = Kosongan Then
                PesanPeringatan("Silakan isi 'Tanggal SKB'.")
                dtp_TanggalSKB.Focus()
                Return
            End If

        Else

            TanggalSKB = TanggalKosong
            NomorSKB = Kosongan

        End If

        If dtp_TanggalSKB.Text = Kosongan Then
            TanggalSKB = TanggalKosong
            NomorSKB = Kosongan
        End If

        If JenisJasa = JenisJasa_Lainnya Then       'Ini memang punya kaaka khusus...! Jangan diusik...!
            If txt_TarifPPh.Text = Kosongan Then    'Ini sudah benar pakai elemen txt_TarifPPh.Text....!!!!
                PesanPeringatan("Silakan isi kolom 'Tarif PPh'." & Enter2Baris &
                                "Jika tidak ada PPh-nya silakan isi dengan angka nol (0).")
                Return
            End If
            If TarifPPh = 0 Then
                AdaPPh = False
            Else
                AdaPPh = True
            End If
        End If

        If AdaPPh Then
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
        Else
            JenisPPh = JenisPPh_NonPPh
        End If

        If InvoiceDenganPO = False Then

        End If

        If KodeMataUang = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_KodeMataUang, "Kode Mata Uang")
            Return
        End If

        If Kurs = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_Kurs, "Kurs")
            Return
        End If

        Dim AdaCOA As Boolean = True
        For Each row As DataRow In datatabelUtama.Rows
            If AmbilAngka(row("Nomor_Urut")) <> 0 And AmbilAngka(row("COA_Produk")) = 0 Then
                AdaCOA = False
            End If
        Next

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If MetodePembayaran = MetodePembayaran_Normal Or TahapTermin = TahapTermin_Pelunasan Then
                If AdaCOA = False Then
                    MsgBox("Silakan isi kolom 'COA' pada masing-masing baris produk.")
                    Return
                End If
            End If
        End If

        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.SelectedDate
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.SelectedDate
        End If

        TanggalLapor = TanggalKosong

        If Not PerusahaanSebagaiPKP Then
            PPNDikreditkan = Pilihan_Tidak
            PilihanPPN = PilihanPPN_Dikapitalisasi
        End If

        Dim AdaPenyimpananjurnal As Boolean

        If NP = Kosongan Then
            PesanUntukProgrammer("Ada masalah...! Value NP = 'Kosongan'...!" & Enter2Baris & "Perbaiki Coding-nya...!!!")
            Return
        End If

        If JenisPembelian = JenisPembelian_Tunai Then
            If SaranaPembayaranTermasukPettyCash(SaranaPembayaran) Then
                JenisPembelian = JenisPembelian_Tunai
                BukaFormPengajuanPengeluaranBankCash = False
            ElseIf SaranaPembayaran = Kosongan Then
                PesanPeringatan("Silakan pilih 'Sarana Pembayaran'.")
                cmb_SaranaPembayaran.Focus()
                Return
            Else
                JenisPembelian = JenisPembelian_Tempo
                BukaFormPengajuanPengeluaranBankCash = True
                LogikaCOAKredit_UntukNonTunai()
            End If
            'If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
            '    MsgBox("Silakan pilih 'Ditanggung Oleh'.")
            '    cmb_DitanggungOleh.Focus()
            '    Return
            'End If
        Else
            LogikaCOAKredit_UntukNonTunai()
        End If

        'Untuk saat ini, setiap input invoice Pembelian, langsung otomatis di-Jurnal.
        'Setiap kali edit, langsung perbarui Jurnal.
        'Kecuali untuk Tahun Buku Lampau. Tidak ada penjurnalan
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then AdaPenyimpananjurnal = True
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then AdaPenyimpananjurnal = False

        If datagridSJBAST.Visibility = Visibility.Visible Then
            If JumlahBarisSJBAST = 0 And InvoiceDenganPO Then
                MsgBox("Silakan input 'Surat Jalan / BAST'.")
                btn_TambahSJBAST.Focus()
                Return
            End If
        End If

        If txt_NomorPO.Visibility = Visibility.Visible Then
            If txt_NomorPO.Text = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Nomor PO'.")
                txt_NomorPO.Focus()
                Return
            End If
        End If

        If JumlahProduk = 0 Then
            MsgBox("Silakan tambahkan data 'Barang/Jasa'.")
            btn_Tambahkan.Focus()
            Return
        End If

        If rdb_JumlahHariJatuhTempo.IsChecked = False And rdb_TanggalJatuhTempo.IsChecked = False Then
            MsgBox("Silakan isi kolom 'Jatuh Tempo'.")
            Return
        End If

        If rdb_JumlahHariJatuhTempo.IsChecked = True Then
            If JumlahHariJatuhTempo = 0 Then
                MsgBox("Silakan isi kolom 'Jumlah Hari'.")
                txt_JumlahHariJatuhTempo.Focus()
                Return
            End If
            TanggalJatuhTempo = TanggalKosongSimpan
        Else
            If dtp_TanggalJatuhTempo.Text = Kosongan Then
                PesanPeringatan("Silakan isi 'Tanggal Jatuh Tempo'.")
                dtp_TanggalJatuhTempo.Focus()
                Return
            End If
            TanggalJatuhTempo = TanggalFormatTampilan(dtp_TanggalJatuhTempo.SelectedDate)
        End If

        If Catatan = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeksKaya(txt_Catatan, "Catatan")
            Return
        End If

        If PembelianImpor Then

            If Not JenisProduk_Induk = JenisProduk_Jasa Then

                If dtp_TanggalSerahTerima.Visibility = Visibility.Visible Then
                    If dtp_TanggalSerahTerima.Text = Kosongan Then
                        PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalSerahTerima, "Tanggal Serah Terima")
                        Return
                    End If
                End If

                If DPP = 0 Then
                    '(Untuk Pembelian Impor, Kolom DPP diisi secara manual. Karena itu berpotensi kosong tanpa inputan)
                    Dim WajibIsiKolom_DPP As Boolean = False
                    If MetodePembayaran = MetodePembayaran_Normal Then
                        WajibIsiKolom_DPP = True
                    Else
                        If TahapTermin = TahapTermin_UangMuka Then
                            WajibIsiKolom_DPP = False
                        Else
                            WajibIsiKolom_DPP = True
                        End If
                    End If
                    If WajibIsiKolom_DPP Then
                        If PembelianLokal Then
                            PesanPeringatan_SilakanIsiKolomTeks(txt_DasarPengenaanPajak, "Dasar Pengenaan Pajak")
                        Else
                            PesanPeringatan_SilakanIsiKolomTeks(txt_DasarPengenaanPajak, "Nilai Impor")
                        End If
                        Return
                    End If
                End If

            End If

            If KursKMK = 0 Then
                Dim WajibIsiKolom_KursKMK As Boolean = False
                If MetodePembayaran = MetodePembayaran_Normal Then
                    WajibIsiKolom_KursKMK = True
                Else
                    If TahapTermin = TahapTermin_UangMuka Then
                        WajibIsiKolom_KursKMK = False
                    Else
                        WajibIsiKolom_KursKMK = True
                    End If
                End If
                If WajibIsiKolom_KursKMK Then
                    PesanPeringatan_SilakanIsiKolomTeks(txt_KursKMK, "Kurs KMK")
                    Return
                End If
            End If

        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If (FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN) _
            And AdaPenyimpananjurnal = True _
            Then
            If Not KodeMataUang = KodeMataUang_IDR Then
                JurnalAdjusment_Forex(COAKredit, TanggalInvoice)
            End If
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        BukaDatabaseTransaksiGeneral()

        If FungsiForm = FungsiForm_EDIT Then
            jur_NomorJV = NomorJV
            HapusDataPembelian_BerdasarkanAngkaInvoice(AngkaInvoice)
            HapusJurnal_BerdasarkanNomorJV(NomorJV)
            HapusDataAsset_BerdasarkanNomorPembelian(NomorPembelian)
            HapusDataAmortisasi_BerdasarkanNomorPembelian(NomorPembelian)
        End If

        If AsalPembelian = AsalPembelian_Lokal Then

            If JenisProduk_Induk = JenisProduk_BarangDanJasa Then

                If DPPJasa = 0 Then JenisProduk_Induk = JenisProduk_Barang
                If DPPBarang = 0 Then JenisProduk_Induk = JenisProduk_Jasa

            End If

        End If

        If StatusSuntingDatabase = True Then

            Me.IsEnabled = False

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pembelian_Invoice")

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Dim HargaSatuan_Simpan As Decimal
            Dim TotalHargaPerItem_Simpan As Decimal
            Dim JumlahHargaKeseluruhan_Simpan As Decimal
            Dim Diskon_Simpan As Decimal
            Dim TotalTagihan_Kotor_Simpan As Decimal
            Dim TotalTagihan_Simpan As Decimal
            Dim JumlahHutangUsaha_Simpan As Decimal
            Dim BiayaAdministrasiBank_Simpan As Decimal
            Dim ReturDPP_Simpan As Decimal
            Dim ReturPPN_Simpan As Decimal

            Dim Termin_Simpan As Decimal = 0 'Harus pakai nol
            Select Case BasisPerhitunganTermin
                Case BasisPerhitunganTermin_Prosentase
                    Termin_Simpan = Termin_Persen
                Case BasisPerhitunganTermin_Nominal
                    If PembelianLokal Then Termin_Simpan = Termin_Rp
                    If PembelianImpor Then Termin_Simpan = Termin_Asing
            End Select

            For Each row As DataRow In datatabelUtama.Rows 'Awal Loop ========================================================

                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = row("Jenis_Produk_Per_Item")
                NomorSJBAST = row("Nomor_SJ_BAST_Produk")
                TanggalSJBAST = row("Tanggal_SJ_BAST_Produk")
                TanggalDiterimaSJBAST = row("Tanggal_Diterima_SJ_BAST_Produk")
                KonversiDBNullJadiTanggalKosong(TanggalSJBAST)
                KonversiDBNullJadiTanggalKosong(TanggalDiterimaSJBAST)
                COAProduk_PerItem = row("COA_Produk")
                NomorPOProduk = row("Nomor_PO_Produk")
                NamaProduk = row("Nama_Produk")
                DeskripsiProduk = row("Deskripsi_Produk")
                JumlahProduk_PerItem = AmbilAngka(row("Jumlah_Produk"))
                SatuanProduk = row("Satuan_Produk")
                If IsDBNull(row("Harga_Satuan")) Then
                    HargaSatuan = 0
                Else
                    HargaSatuan = AmbilAngka(row("Harga_Satuan"))
                End If
                HargaSatuan_Asing = AmbilAngka_Asing(row("Harga_Satuan_Asing"))
                DiskonPerItem_Persen = GantiTeks(row("Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHargaPerItem = AmbilAngka(row("Total_Harga"))
                TotalHargaPerItem_Asing = AmbilAngka_Asing(row("Total_Harga_Asing"))
                KodeProjectProduk = row("Kode_Project_Produk")
                KodeDivisiAsset = row("Kode_Divisi_Asset")
                KelompokAsset = AmbilAngka(row("Kelompok_Asset"))
                COABiaya = row("COA_Biaya")
                TanggalMulaiAmortisasi = row("Tanggal_Mulai_Amortisasi")
                MasaAmortisasi = row("Masa_Amortisasi")
                If PembelianLokal Then
                    HargaSatuan_Simpan = HargaSatuan
                    TotalHargaPerItem_Simpan = TotalHargaPerItem
                    JumlahHargaKeseluruhan_Simpan = JumlahHargaKeseluruhan
                    Diskon_Simpan = Diskon_Rp
                    TotalTagihan_Kotor_Simpan = TotalTagihan_Kotor
                    TotalTagihan_Simpan = TotalTagihan
                    JumlahHutangUsaha_Simpan = JumlahHutangUsaha
                    BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
                    ReturDPP_Simpan = ReturDPP
                    ReturPPN_Simpan = ReturPPN
                    Insurance = 0   'Hanya untuk memastikan
                    Freight = 0     'Hanya untuk memastikan
                    BeaMasuk = 0    'Hanya untuk memastikan
                Else
                    HargaSatuan_Simpan = HargaSatuan_Asing
                    TotalHargaPerItem_Simpan = TotalHargaPerItem_Asing
                    JumlahHargaKeseluruhan_Simpan = JumlahHargaKeseluruhan_Asing
                    Diskon_Simpan = DiskonAsing
                    TotalTagihan_Kotor_Simpan = TotalTagihan_Kotor_Asing
                    TotalTagihan_Simpan = TotalTagihan_Asing
                    JumlahHutangUsaha_Simpan = JumlahHutangUsaha_Asing
                    BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
                    ReturDPP_Simpan = 0
                    ReturPPN_Simpan = 0
                End If
                If MetodePembayaran = MetodePembayaran_Normal Then Termin_Simpan = 100 '(Kenapa pakai angka 100? Karena tanpa termin itu berarti pembayaran full 100%)
                QueryPenyimpanan = " INSERT INTO tbl_Pembelian_Invoice VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaInvoice & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & JenisInvoice & "', " &
                    " '" & MetodePembayaran & "', " &
                    " '" & BasisPerhitunganTermin & "', " &
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
                    " '" & KodeMataUang & "', " &
                    " '" & DesimalFormatSimpan(Kurs) & "', " &
                    " '" & JenisJasa & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProdukPerItem & "', " &
                    " '" & NomorSJBAST & "', " &
                    " '" & TanggalFormatSimpan(TanggalSJBAST) & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterimaSJBAST) & "', " &
                    " '" & TanggalFormatSimpan(TanggalSerahTerima) & "', " &
                    " '" & Loko & "', " &
                    " '" & NomorPOProduk & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & COAProduk_PerItem & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & DesimalFormatSimpan(HargaSatuan_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & DesimalFormatSimpan(TotalHargaPerItem_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(JumlahHargaKeseluruhan_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Diskon_Simpan) & "', " &
                    " '" & TahapTermin & "', " &
                    " '" & DesimalFormatSimpan(Termin_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Insurance) & "', " &
                    " '" & DesimalFormatSimpan(Freight) & "', " &
                    " '" & DesimalFormatSimpan(BeaMasuk) & "', " &
                    " '" & DesimalFormatSimpan(KursKMK) & "', " &
                    " '" & DPPBarang & "', " &
                    " '" & DPPJasa & "', " &
                    " '" & DPP & "', " &
                    " '" & NomorFakturPajak & "', " &
                    " '" & TanggalFormatSimpan(TanggalFakturPajak) & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & PPNDikreditkan & "', " &
                    " '" & PilihanPPN & "', " &
                    " '" & NomorSKB & "', " &
                    " '" & TanggalFormatSimpan(TanggalSKB) & "', " &
                    " '" & DesimalFormatSimpan(TarifPPN) & "', " &
                    " '" & PPN & "', " &
                    " '" & DesimalFormatSimpan(TotalTagihan_Kotor_Simpan) & "', " &
                    " '" & JenisPPh & "', " &
                    " '" & KodeSetoran & "', " &
                    " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                    " '" & PPhTerutang & "', " &
                    " '" & PPhDitanggung & "', " &
                    " '" & PPhDipotong & "', " &
                    " '" & DesimalFormatSimpan(TotalTagihan_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(JumlahHutangUsaha_Simpan) & "', " &
                    " '" & JenisPembelian & "', " &
                    " '" & COAKredit & "', " &
                    " '" & SaranaPembayaran & "', " &
                    " '" & DesimalFormatSimpan(BiayaAdministrasiBank_Simpan) & "', " &
                    " '" & DitanggungOleh & "', " &
                    " '" & OngkosKirim & "', " &
                    " '" & BiayaMaterai & "', " &
                    " '" & DesimalFormatSimpan(ReturDPP_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(ReturPPN_Simpan) & "', " &
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
                    " '" & TanggalKosongSimpan & "', " &
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
                    If TotalHargaPerItem > 0 And OngkosKirimAssetKeseluruhan > 0 Then
                        OngkosKirimAsset_PerItem = TotalHargaPerItem * OngkosKirimAssetKeseluruhan / JumlahHargaAssetKeseluruhan
                    Else
                        OngkosKirimAsset_PerItem = 0
                    End If
                    If TotalHargaPerItem_Asing > 0 And OngkosKirimAssetKeseluruhan_Asing > 0 Then
                        OngkosKirimAsset_PerItem_Asing = TotalHargaPerItem_Asing * OngkosKirimAssetKeseluruhan_Asing / JumlahHargaAssetKeseluruhan_Asing
                    Else
                        OngkosKirimAsset_PerItem_Asing = 0
                    End If
                    Dim HargaPerolehan As Int64
                    Select Case AsalPembelian
                        Case AsalPembelian_Lokal
                            HargaPerolehan = Fix(TotalHargaPerItem + OngkosKirimAsset_PerItem) / JumlahProduk_PerItem
                            If JenisPPN = JenisPPN_Include Then
                                HargaPerolehan = Fix(HitungDPPUntukPPNInclude(TotalHargaPerItem, TarifPPN) + OngkosKirimAsset_PerItem) / JumlahProduk_PerItem
                            End If
                            If PilihanPPN = PilihanPPN_Dikapitalisasi Then
                                Select Case JenisPPN
                                    Case JenisPPN_Exclude
                                        HargaPerolehan = Fix((TotalHargaPerItem + (TotalHargaPerItem * Persen(TarifPPN)) + OngkosKirimAsset_PerItem) / JumlahProduk_PerItem)
                                    Case JenisPPN_Include
                                        HargaPerolehan = Fix((TotalHargaPerItem + OngkosKirimAsset_PerItem) / JumlahProduk_PerItem)
                                End Select
                            End If
                        Case AsalPembelian_Impor
                            HargaPerolehan _
                                = Fix((AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TotalHargaPerItem_Asing)) _
                                + (AmbilValue_NilaiMataUang(KodeMataUang, Kurs, OngkosKirimAsset_PerItem_Asing))) / JumlahProduk_PerItem
                    End Select
                    PesanUntukProgrammer("Harga Perolehan : " & HargaPerolehan)
                    Dim Keterangan = Kosongan
                    If COATermasukAssetTanahDanBangunan(COA_Asset) Then
                        HargaPerolehan = JumlahProduk_PerItem * HargaPerolehan
                        JumlahAsset_PerItem = 1
                    End If
                    Do While NomorUrutAsset < JumlahAsset_PerItem
                        NomorUrutAsset += 1
                        IdAsset += 1
                        KodeAsset = COA_Asset & "-" & KodeDivisiAsset & "-" & AmbilTahun_DariTanggal(TanggalPerolehan) &
                                "-" & KonversiAngkaKeStringDuaDigit(AmbilBulanAngka_DariTanggal(TanggalPerolehan)) & "-" & IdAsset
                        'PesanUntukProgrammer("Asal Pembelian : " & AsalPembelian & Enter2Baris &
                        '                     "Pilihan PPN : " & PilihanPPN & Enter2Baris &
                        '                     "Total Harga Peritem Asing: " & TotalHargaPerItem_Asing & Enter2Baris &
                        '                     "Ongkos Kirim Peritem Asing: " & OngkosKirimAsset_PerItem_Asing & Enter2Baris &
                        '                     "Jumlah Produk Peritem: " & JumlahProduk_PerItem & Enter2Baris &
                        '                     "Asset Ke : " & NomorUrutAsset & Enter2Baris &
                        '                     "Harga Perolehan : " & HargaPerolehan)
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
                    Dim JumlahTransaksi As Int64
                    If AsalPembelian = AsalPembelian_Lokal Then JumlahTransaksi = TotalHargaPerItem '/ JumlahProduk_PerItem
                    If AsalPembelian = AsalPembelian_Impor Then JumlahTransaksi = (AmbilValue_NilaiMataUang(KodeMataUang, Kurs, TotalHargaPerItem_Asing)) '/ JumlahProduk_PerItem
                    IdAmortisasi = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_AmortisasiBiaya") + 1
                    KodeAsset = COAAmortisasi & "-" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(AmbilBulanAngka_DariTanggal(TanggalInvoice)) & "-" & IdAmortisasi
                    cmd = New OdbcCommand(" INSERT INTO tbl_AmortisasiBiaya VALUES ( " &
                                  " '" & IdAmortisasi & "', " &
                                  " '" & NomorPembelian & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & COAAmortisasi & "', " &
                                  " '" & NamaAkunAmortisasi & "', " &
                                  " '" & NamaProduk & "', " &
                                  " '" & COABiaya & "', " &
                                  " '" & NamaAkunBiaya & "', " &
                                  " '" & MasaAmortisasi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalMulaiAmortisasi) & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & Kosongan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
                    cmd_ExecuteNonQuery()
                End If

                If StatusKoneksiDatabase = False Then
                    TutupDatabaseTransaksiGeneral()
                    PesanUntukProgrammer("Penyimpanan bermasalah...!!!")
                    Exit For
                End If

            Next 'Akhir Loop =================================================================================================

            dataviewUtama.Sort = "COA_Produk" '(Pengurutan Baris Tabel Berdasarkan COA)

            'Penghimpunan COA-COA yang sama ke dalam satu baris :
            Dim TabelSusunBarisJurnal_COASebelumnya = Kosongan
            Dim TabelSusunBarisJurnal_COA = Kosongan
            Dim TabelSusunNBarisJurnal_JumlahDebet As Decimal
            Dim Baris = 0
            For Each row As DataRowView In datagridUtama.Items
                Baris += 1
                TabelSusunBarisJurnal_COA = row("COA_Produk")
                If TabelSusunBarisJurnal_COA <> TabelSusunBarisJurnal_COASebelumnya And Baris > 1 Then
                    TambahBarisSusunJurnal(TabelSusunBarisJurnal_COASebelumnya, TabelSusunNBarisJurnal_JumlahDebet)
                End If
                TotalHargaPerItem = AmbilAngka(row("Total_Harga"))
                TotalHargaPerItem_Asing = AmbilAngka_Asing(row("Total_Harga_Asing"))
                If JumlahHargaAssetKeseluruhan > 0 Then
                    If TotalHargaPerItem > 0 And OngkosKirimAssetKeseluruhan > 0 Then
                        OngkosKirimAsset_PerItem = TotalHargaPerItem * OngkosKirimAssetKeseluruhan / JumlahHargaAssetKeseluruhan
                    Else
                        OngkosKirimAsset_PerItem = 0
                    End If
                    If TotalHargaPerItem_Asing > 0 And OngkosKirimAssetKeseluruhan_Asing > 0 Then
                        OngkosKirimAsset_PerItem_Asing = TotalHargaPerItem_Asing * OngkosKirimAssetKeseluruhan_Asing / JumlahHargaAssetKeseluruhan_Asing
                    Else
                        OngkosKirimAsset_PerItem_Asing = 0
                    End If
                Else
                    OngkosKirimAsset_PerItem = 0
                    OngkosKirimAsset_PerItem_Asing = 0
                End If
                If PilihanPPN = PilihanPPN_Dikapitalisasi Then TotalHargaPerItem += (TotalHargaPerItem * Persen(TarifPPN))
                If AsalPembelian = AsalPembelian_Lokal Then
                    If JenisPPN = JenisPPN_Include Then
                        TabelSusunNBarisJurnal_JumlahDebet += CDec(Math.Round((HitungDPPUntukPPNInclude(TotalHargaPerItem, TarifPPN) + OngkosKirimAsset_PerItem)))
                    Else
                        TabelSusunNBarisJurnal_JumlahDebet += CDec(Math.Round(TotalHargaPerItem + OngkosKirimAsset_PerItem))
                    End If
                End If
                If AsalPembelian = AsalPembelian_Impor Then TabelSusunNBarisJurnal_JumlahDebet += (TotalHargaPerItem_Asing + OngkosKirimAsset_PerItem_Asing)
                TabelSusunBarisJurnal_COASebelumnya = TabelSusunBarisJurnal_COA
                PesanUntukProgrammer("Kode Akun : " & TabelSusunBarisJurnal_COASebelumnya & Enter2Baris &
                                     "Nama Akun : " & AmbilValue_NamaAkun(TabelSusunBarisJurnal_COASebelumnya) & Enter2Baris &
                                     "Jumlah Debet : " & TabelSusunNBarisJurnal_JumlahDebet)
            Next
            TambahBarisSusunJurnal(TabelSusunBarisJurnal_COASebelumnya, TabelSusunNBarisJurnal_JumlahDebet)

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

                Dim COA_UangMukaPembelian = Kosongan
                Select Case KodeMataUang
                    Case KodeMataUang_IDR
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian
                    Case KodeMataUang_USD
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_USD
                    Case KodeMataUang_AUD
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_AUD
                    Case KodeMataUang_JPY
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_JPY
                    Case KodeMataUang_CNY
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_CNY
                    Case KodeMataUang_EUR
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_EUR
                    Case KodeMataUang_SGD
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_SGD
                    Case KodeMataUang_GBP
                        COA_UangMukaPembelian = KodeTautanCOA_UangMukaPembelian_Impor_GBP
                End Select

                If FungsiForm = FungsiForm_TAMBAH _
                    Or FungsiForm = FungsiForm_EDIT _
                    Then

                    Select Case JenisPembelian
                        Case JenisPembelian_Tunai
                            If PembelianLokal Then JumlahKredit = TotalTagihan
                            If PembelianImpor Then JumlahKredit = TotalTagihan_Asing
                        Case JenisPembelian_Tempo
                            If PembelianLokal Then JumlahKredit = JumlahHutangUsaha
                            If PembelianImpor Then JumlahKredit = JumlahHutangUsaha_Asing
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

                    If PembelianImpor Then '(Ini masih perlu dikaji lagi).
                        PPN = 0
                        PPhTerutang = 0
                    End If

                    Dim tabelJurnalDebet As New DataGridView
                    tabelJurnalDebet.Columns.Clear()
                    tabelJurnalDebet.Rows.Clear()
                    tabelJurnalDebet.Columns.Add("COA_Debet", "")
                    tabelJurnalDebet.Columns.Add("Jumlah_Debet", "")

                    'Simpan Jurnal :
                    Dim TotalDebetProduk As Decimal = 0
                    For Each row As DataGridViewRow In TabelSusunCOA.Rows '(Ini sudah benar pakai datagridview. Jangan dirubah).
                        Dim COADebet = row.Cells("COA_Debet").Value
                        Dim JumlahDebetProduk As Decimal
                        JumlahDebetProduk = AmbilAngka_Desimal(row.Cells("Jumlah_Debet").Value)
                        TotalDebetProduk += JumlahDebetProduk
                        If MetodePembayaran = MetodePembayaran_Termin And TahapTermin <> TahapTermin_Pelunasan Then JumlahDebetProduk = 0
                        tabelJurnalDebet.Rows.Add(COADebet, JumlahDebetProduk)
                    Next
                    Dim KreditPelunasan As Decimal = 0
                    Dim DPPTermin As Decimal = 0
                    If PembelianLokal Then
                        FormatUlangAngkaKeBilanganBulat(DPPTermin)
                        DPPTermin = FormatUlangInt64(DPP)
                    End If
                    If PembelianImpor Then DPPTermin = Termin_Asing
                    If MetodePembayaran = MetodePembayaran_Termin Then
                        If TahapTermin = TahapTermin_Pelunasan Then
                            KreditPelunasan = TotalDebetProduk - DPPTermin
                            DPPTermin = 0
                        Else
                            TotalDebetProduk = 0
                            KreditPelunasan = 0
                        End If
                    Else
                        DPPTermin = 0
                    End If

                    'PesanUntukProgrammer("DPP Termin : " & DPPTermin & Enter2Baris &
                    '                     "Kredit Pelunasan : " & KreditPelunasan)

                    If FungsiForm = FungsiForm_TAMBAH Then
                        If Not KodeMataUang = KodeMataUang_IDR Then
                            If (DPPTermin > 0 Or KreditPelunasan > 0) Then
                                JurnalAdjusment_Forex(COA_UangMukaPembelian, TanggalInvoice)
                            End If
                            PenyesuaianKembaliNomorJV()
                        End If
                    End If

                    PenyesuaianKembaliValueJurnal()

                    For Each row As DataGridViewRow In tabelJurnalDebet.Rows
                        ___jurDebet(row.Cells("COA_Debet").Value, AmbilAngka_Desimal(row.Cells("Jumlah_Debet").Value))
                    Next
                    ___jurDebet(COA_UangMukaPembelian, DPPTermin)
                    ___jurDebet(KodeTautanCOA_BiayaAsuransiImport, Insurance)
                    ___jurDebet(KodeTautanCOA_BiayaPengapalan, Freight)
                    ___jurDebet(COAPPN, PPN)
                    ___jurDebet(PenentuanCOA_BiayaPPh(JenisPPh), PPhDitanggung)
                    ___jurDebet(COAOngkosKirim, OngkosKirim)
                    ___jurDebet(KodeTautanCOA_BiayaPerlengkapanKantor, BiayaKeperluanKantor)
                    ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
                    _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang)
                    _______jurKreditBankCashOUT(DitanggungOleh, COAKredit, JumlahKredit, JumlahTransfer, BiayaAdministrasiBank)
                    _______jurKredit(COA_UangMukaPembelian, KreditPelunasan)
                    KoreksiSelisihJurnal(jur_NomorJV) 'Ini harus disimpan langsung di ujung penyimpanan Jurnal, tidak boleh diseling oleh baris kode yang lain

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
                    cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
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

                    'Pengisian Value Selisih :
                    DPPBarang_Selisih = DPPBarang_InvoiceLama - DPPBarang
                    DPPJasa_Selisih = DPPJasa_InvoiceLama - DPPJasa
                    PPN_Selisih = PPN_InvoiceLama - PPN
                    TotalTagihanKotor_Selisih = TotalTagihanKotor_InvoiceLama - TotalTagihan_Kotor

                    PenyesuaianKembaliNomorJV()
                    PenyesuaianKembaliValueJurnal()

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

            dataviewUtama.Sort = "Nomor_Urut" '(Kembalikan urutan Tabel Produk berdasarkan Nomor Urut)

            If StatusSuntingDatabase = True Then
                SusunUlangNomorID_DataAsset()
                If FungsiForm = FungsiForm_TAMBAH And MetodePembayaran = MetodePembayaran_Termin Then UpdateStatusPO()
                Try
                    UpdateTampilanTabel_SeluruhInvoicePembelian()
                    UpdateTampilanTabel_SeluruhBukuPembelian()
                    UpdateTampilanTabel_SeluruhBukuPengawasanHutangUsaha()
                    'RefreshTampilanSJBASTPembelian()
                    'RefreshTampilanDataAsset()
                    'RefreshTampilanAmortisasiBiaya()
                Catch ex As Exception
                    WriteException(ex, "RefreshTampilanSetelahSimpanInvoicePembelian")
                End Try
                If AdaPenyimpananjurnal = True Then
                    If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
                    If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
                Else
                    If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
                    If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
                End If
                Me.Close()
            Else
                If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
                If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
            End If

        End If

    End Sub

    Sub PenyesuaianKembaliNomorJV()
        SistemPenomoranOtomatis_NomorJV()
        EditNomorJV_PadaInvoicePembelian(NomorInvoice, jur_NomorJV)
    End Sub

    Sub PenyesuaianKembaliValueJurnal()
        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
        jur_JenisJurnal = JenisJurnal_Pembelian
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = TanggalFormatTampilan(TanggalInvoice) 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
        jur_NomorInvoice = NomorInvoice
        jur_NamaProduk = AmbilValue_ListProdukBerdasarkanInvoicePembelian(NomorInvoice)
        jur_NomorFakturPajak = NomorFakturPajak
        jur_KodeLawanTransaksi = KodeSupplier
        jur_NamaLawanTransaksi = NamaSupplier
        jur_KodeMataUang = KodeMataUang
        jur_Kurs = Kurs
        jur_UraianTransaksi = Catatan
        jur_Direct = 0
    End Sub

    Sub UpdateStatusPO()
        Dim StatusKontrolPO As String
        If TahapTermin = TahapTermin_Pelunasan Then
            StatusKontrolPO = Status_Closed
        Else
            StatusKontrolPO = Status_Used
        End If
        UpdateStatusKontrolPOPembelian(txt_NomorPO.Text, StatusKontrolPO)
        RefreshTampilanPOPembelian()
    End Sub




    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Dim PanjangKolomCOAProduk As Integer
    Dim PanjangKolomTotal As Integer
    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        PanjangKolomCOAProduk = 57
        PanjangKolomTotal = 618
        Buat_DataTabelUtama()
        'Buat_DataTabelTotal()
        Buat_DataTabelSJBAST()
        cmb_JenisInvoice.IsReadOnly = True
        txt_NomorPO.IsReadOnly = True
        txt_KodeSupplier.IsReadOnly = True
        txt_NamaSupplier.IsReadOnly = True
        txt_PICSupplier.IsReadOnly = True
        txt_AlamatSupplier.IsReadOnly = True
        cmb_JenisJasa.IsReadOnly = True
        cmb_JenisPPN.IsReadOnly = True
        cmb_PerlakuanPPN.IsReadOnly = True
        cmb_PPNDikreditkan.IsReadOnly = True
        cmb_PilihanPPN.IsReadOnly = True
        cmb_KodeMataUang.IsReadOnly = True
        cmb_SaranaPembayaran.IsReadOnly = True
        cmb_DitanggungOleh.IsReadOnly = True
        txt_TotalBank.IsReadOnly = True
        cmb_JenisPPh.IsReadOnly = True
        cmb_KodeSetoran.IsReadOnly = True
        txt_TarifPPN.IsReadOnly = False
        txt_TarifPPN_11Per12.IsReadOnly = False
        txt_TotalTagihanIDR.IsReadOnly = True
        txt_JumlahNota.IsReadOnly = True
        txt_Diskon_Rp.IsReadOnly = True
        txt_UangMukaPlusTermin_Persen.IsReadOnly = True
        txt_UangMukaPlusTermin_Rp.IsReadOnly = True
        txt_Termin_Persen.IsReadOnly = True
        txt_Termin_Rp.IsReadOnly = True
        txt_DPPBarang.IsReadOnly = True
        txt_DPPJasa.IsReadOnly = True
        txt_TotalTagihan_Kotor.IsReadOnly = True
        txt_PPhDipotong.IsReadOnly = True
        txt_OngkosKirim.IsReadOnly = False 'Ini dibuka kuncinya, karena bisa jadi pada kenyataannya ongkos kirim tidak sesuai dengan yang di PO.
        txt_TotalTagihan.IsReadOnly = True
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
        scv_Kanan.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub


    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut As New DataGridTextColumn
    Dim Jenis_Produk_Per_Item As New DataGridTextColumn
    Dim Nomor_SJ_BAST_Produk As New DataGridTextColumn
    Dim Tanggal_SJ_BAST_Produk As New DataGridTextColumn
    Dim Tanggal_Diterima_SJ_BAST_Produk As New DataGridTextColumn
    Dim Nomor_PO_Produk As New DataGridTextColumn
    Dim COA_Produk As New DataGridTextColumn     '<----------------------------------- Baru '
    Dim Nama_Produk As New DataGridTextColumn
    Dim Deskripsi_Produk As New DataGridTextColumn
    Dim Jumlah_Produk As New DataGridTextColumn
    Dim Satuan_Produk As New DataGridTextColumn
    Dim Harga_Satuan As New DataGridTextColumn
    Dim Harga_Satuan_Asing As New DataGridTextColumn
    Dim Jumlah_Harga_Per_Item As New DataGridTextColumn
    Dim Jumlah_Harga_Per_Item_Asing As New DataGridTextColumn
    Dim Diskon_Per_Item_Persen As New DataGridTextColumn
    Dim Diskon_Per_Item_Rp As New DataGridTextColumn
    Dim Diskon_Per_Item_Asing As New DataGridTextColumn
    Dim Total_Harga As New DataGridTextColumn
    Dim Total_Harga_Asing As New DataGridTextColumn
    Dim Kode_Project_Produk As New DataGridTextColumn
    Dim Kelompok_Asset As New DataGridTextColumn
    Dim Kode_Divisi_Asset As New DataGridTextColumn
    Dim COA_Biaya As New DataGridTextColumn
    Dim Tanggal_Mulai_Amortisasi As New DataGridTextColumn
    Dim Masa_Amortisasi As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Produk_Per_Item")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Tanggal_Diterima_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Nomor_PO_Produk")
        datatabelUtama.Columns.Add("COA_Produk")     '<----------------------------------- Baru
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Deskripsi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk", GetType(Int64))
        datatabelUtama.Columns.Add("Satuan_Produk")
        datatabelUtama.Columns.Add("Harga_Satuan", GetType(Int64))
        datatabelUtama.Columns.Add("Harga_Satuan_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Jumlah_Harga_Per_Item", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Per_Item_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Per_Item_Persen")
        datatabelUtama.Columns.Add("Diskon_Per_Item_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Per_Item_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Total_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Kode_Project_Produk")
        datatabelUtama.Columns.Add("Kelompok_Asset")
        datatabelUtama.Columns.Add("Kode_Divisi_Asset")
        datatabelUtama.Columns.Add("COA_Biaya")
        datatabelUtama.Columns.Add("Tanggal_Mulai_Amortisasi")
        datatabelUtama.Columns.Add("Masa_Amortisasi")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk_Per_Item, "Jenis_Produk_Per_Item", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST_Produk, "Nomor_SJ_BAST_Produk", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST_Produk, "Tanggal_SJ_BAST_Produk", "Tanggal SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Diterima_SJ_BAST_Produk, "Tanggal_Diterima_SJ_BAST_Produk", "Tanggal Diterima", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO_Produk, "Nomor_PO_Produk", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Produk, "COA_Produk", "COA", PanjangKolomCOAProduk, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 117, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 135, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan, "Harga_Satuan", "Harga Satuan", 93, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan_Asing, "Harga_Satuan_Asing", "Harga Satuan", 93, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Per_Item, "Jumlah_Harga_Per_Item", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Per_Item_Asing, "Jumlah_Harga_Per_Item_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Persen, "Diskon_Per_Item_Persen", "Diskon" & Enter1Baris & "(%)", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Rp, "Diskon_Per_Item_Rp", "Diskon" & Enter1Baris & "(Rp)", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Asing, "Diskon_Per_Item_Asing", "Diskon", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Harga, "Total_Harga", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Harga_Asing, "Total_Harga_Asing", "Total", 111, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project_Produk, "Kode_Project_Produk", "Kode Project", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kelompok_Asset, "Kelompok_Asset", "Kelompok Asset", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Divisi_Asset, "Kode_Divisi_Asset", "Kode Divisi Asset", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Biaya, "COA_Biaya", "COA Biaya", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Mulai_Amortisasi, "Tanggal_Mulai_Amortisasi", "Tanggal Mulai Amortisasi", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Amortisasi, "Masa_Amortisasi", "Masa Amortisasi", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


    'Pembuatan Tabel SJBAST :
    Public datatabelSJBAST As DataTable
    Public dataviewSJBAST As DataView
    Public rowviewSJBAST As DataRowView
    Public newRowSJBAST As DataRow
    Public HeaderKolomSJBAST As DataGridColumnHeader
    Public KolomTerseleksiSJBAST As DataGridColumn
    Public BarisTerseleksiSJBAST As Integer
    Public JumlahBarisSJBAST As Integer

    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_Diterima As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Biaya_Transportasi As New DataGridTextColumn
    Sub Buat_DataTabelSJBAST()

        datatabelSJBAST = New DataTable
        datatabelSJBAST.Columns.Add("Nomor_SJ_BAST")
        datatabelSJBAST.Columns.Add("Tanggal_SJ_BAST")
        datatabelSJBAST.Columns.Add("Tanggal_Diterima")
        datatabelSJBAST.Columns.Add("Nomor_PO")
        datatabelSJBAST.Columns.Add("Biaya_Transportasi", GetType(Int64))

        StyleTabelUtama_WPF(datagridSJBAST, datatabelSJBAST, dataviewSJBAST)
        TambahkanKolomTextBoxDataGrid_WPF(datagridSJBAST, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridSJBAST, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridSJBAST, Tanggal_Diterima, "Tanggal_Diterima", "Diterima", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridSJBAST, Nomor_PO, "Nomor_PO", "PO", 135, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridSJBAST, Biaya_Transportasi, "Biaya_Transportasi", "Biaya Transportasi", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub














    ''Tabel Total :
    'Public datatabelTotal As DataTable
    'Public dataviewTotal As DataView
    'Public rowviewTotal As DataRowView

    'Dim Baris_Total As New DataGridTextColumn
    'Dim Jumlah_Harga_Keseluruhan As New DataGridTextColumn
    'Dim Diskon_Persen_Keseluruhan As New DataGridTextColumn
    'Dim Diskon_Rp_Keseluruhan As New DataGridTextColumn
    'Dim Total_Harga_Keseluruhan As New DataGridTextColumn
    'Sub Buat_DataTabelTotal()

    '    datatabelTotal = New DataTable
    '    datatabelTotal.Columns.Add("Baris_Total")
    '    datatabelTotal.Columns.Add("Jumlah_Harga_Keseluruhan", GetType(Int64))
    '    datatabelTotal.Columns.Add("Diskon_Persen_Keseluruhan")
    '    datatabelTotal.Columns.Add("Diskon_Rp_Keseluruhan", GetType(Int64))
    '    datatabelTotal.Columns.Add("Total_Harga_Keseluruhan", GetType(Int64))

    '    StyleTabelTotal_WPF(datagridTotal, datatabelTotal, dataviewTotal)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Baris_Total, "Baris_Total", "No.", PanjangKolomTotal, FormatString, TengahTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Jumlah_Harga_Keseluruhan, "Jumlah_Harga_Keseluruhan", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Persen_Keseluruhan, "Diskon_Persen_Keseluruhan", "Diskon" & Enter1Baris & "(%)", 72, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Rp_Keseluruhan, "Diskon_Rp_Keseluruhan", "Diskon" & Enter1Baris & "(Rp)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Total_Harga_Keseluruhan, "Total_Harga_Keseluruhan", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    datatabelTotal.Rows.Add("TOTAL", 0, Kosongan, 0, 0)

    'End Sub

    Sub UpdateTampilanTabel_SeluruhInvoicePembelian()
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_DenganPO_Lokal_Rutin)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_DenganPO_Lokal_Termin)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_DenganPO_Impor_Rutin)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_DenganPO_Impor_Termin)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Lokal_Barang)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Lokal_Jasa)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Impor_Barang)
        UpdateTampilanTabel_PerUscInvoicePembelian(usc_InvoicePembelian_TanpaPO_Impor_Jasa)
    End Sub

    Sub UpdateTampilanTabel_PerUscInvoicePembelian(Usc As wpfUsc_InvoicePembelian)
        If Not Usc.StatusAktif Then Return
        Dim TanggalJatuhTempo_Kirim As String = TanggalFormatTampilan(TanggalJatuhTempo)
        If TanggalJatuhTempo = TanggalKosong Then TanggalJatuhTempo_Kirim = JumlahHariJatuhTempo & " hari"
        Dim TanggalPembetulan_Kirim As String = TanggalFormatTampilan(TanggalPembetulan)
        If NP = "N" Then TanggalPembetulan_Kirim = StripKosong
        Usc.JenisInvoice = JenisInvoice
        Usc.JenisProduk = JenisProduk_Induk
        Usc.AngkaInvoice = AngkaInvoice
        Usc.NomorInvoice = NomorInvoice
        Usc.NomorPembelian = NomorPembelian
        Usc.NP = NP
        Usc.TanggalInvoice = TanggalFormatTampilan(TanggalInvoice)
        Usc.TanggalPembetulan = TanggalPembetulan_Kirim
        Usc.Tanggallapor = TanggalFormatTampilan(TanggalLapor)
        Usc.JatuhTempo = TanggalJatuhTempo_Kirim
        Usc.NomorSJBAST = NomorSJBAST
        Usc.TanggalSJBAST = TanggalFormatTampilan(TanggalSJBAST)
        Usc.NomorPO = NomorPOProduk
        Usc.TanggalPO = AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk)
        Usc.NamaProduk = NamaProduk
        Usc.KodeProject = KodeProjectProduk
        Usc.KodeSupplier = KodeSupplier
        Usc.NamaSupplier = NamaSupplier
        Usc.KodeMataUang = KodeMataUang
        Usc.JumlahHarga = JumlahHargaKeseluruhan
        Usc.JumlahHarga_Asing = JumlahHargaKeseluruhan_Asing
        Usc.DiskonRp = Diskon_Rp
        Usc.DiskonAsing = DiskonAsing
        Usc.ProsentaseTermin_String = Termin_Persen & " %"
        Usc.DasarPengenaanPajak = DPP
        Usc.NomorFakturPajak = NomorFakturPajak
        Usc.JenisPPN = JenisPPN
        Usc.PPN = PPN
        Usc.TagihanKotor = TotalTagihan_Kotor
        Usc.PPhDipotong = PPhDipotong
        Usc.TotalTagihan = TotalTagihan
        Usc.TotalTagihan_Asing = TotalTagihan_Asing
        Usc.ReturDPP = ReturDPP
        Usc.ReturPPN = ReturPPN
        Usc.Retur = ReturDPP + ReturPPN
        Usc.Catatan = PenghapusEnter(Catatan)
        Usc.NomorJV = NomorJV
        If FungsiForm = FungsiForm_TAMBAH Then Usc.TambahBaris()
        If FungsiForm = FungsiForm_EDIT Then
            Usc.NomorInvoiceLama = NomorInvoiceLama
            Usc.UpdateBaris()
        End If
    End Sub

    Sub UpdateTampilanTabel_SeluruhBukuPembelian()
        UpdateTampilanTabel_PerUscBukuPembelian(usc_BukuPembelian_Lokal)
        UpdateTampilanTabel_PerUscBukuPembelian(usc_BukuPembelian_Impor)
    End Sub

    Sub UpdateTampilanTabel_PerUscBukuPembelian(Usc As wpfUsc_BukuPembelian)
        If Not Usc.StatusAktif Then Return
        Dim TanggalPembetulan_Kirim As String = TanggalFormatTampilan(TanggalPembetulan)
        If NP = "N" Then TanggalPembetulan_Kirim = StripKosong
        Usc.NomorPembelian = NomorPembelian
        Usc.JenisInvoice = JenisInvoice
        Usc.JenisProduk = JenisProduk_Induk
        Usc.AngkaInvoice = AngkaInvoice
        Usc.NomorInvoice = NomorInvoice
        Usc.NP = NP
        Usc.NomorFakturPajak = NomorFakturPajak
        Usc.TanggalInvoice = TanggalFormatTampilan(TanggalInvoice)
        Usc.TanggalPembetulan = TanggalPembetulan_Kirim
        Usc.KodeSupplier = KodeSupplier
        Usc.NamaSupplier = NamaSupplier
        Usc.KodeMataUang = KodeMataUang
        Usc.JumlahHarga = JumlahHargaKeseluruhan
        Usc.JumlahHarga_Asing = JumlahHargaKeseluruhan_Asing
        Usc.DiskonRp = Diskon_Rp
        Usc.DiskonAsing = DiskonAsing
        Usc.DasarPengenaanPajak = DPP
        Usc.JenisPPN = JenisPPN
        Usc.PerlakuanPPN = PerlakuanPPN
        Usc.PPN = PPN
        Usc.PPhDipotong = PPhDipotong
        Usc.BiayaLainnya = OngkosKirim + BiayaMaterai
        Usc.BiayaLainnya_Asing = Freight + Insurance
        Usc.TagihanBruto = TotalTagihan_Kotor
        Usc.TagihanBruto_Asing = TotalTagihan_Kotor_Asing
        Usc.Retur = ReturDPP + ReturPPN
        Usc.TagihanNetto = TotalTagihan
        Usc.NomorSJBAST = NomorSJBAST
        Usc.TanggalSJBAST = TanggalFormatTampilan(TanggalSJBAST)
        Usc.NomorPO = NomorPOProduk
        Usc.TanggalPO = AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk)
        Usc.KodeProject = KodeProjectProduk
        Usc.MasaJatuhTempo = JumlahHariJatuhTempo
        Usc.KeteranganJatuhTempo = TanggalFormatTampilan(TanggalJatuhTempo)
        Usc.KodeFP = Kosongan
        Usc.Catatan = PenghapusEnter(Catatan)
        Usc.NomorJV = NomorJV
        If FungsiForm = FungsiForm_TAMBAH Then Usc.TambahBaris()
        If FungsiForm = FungsiForm_EDIT Then
            Usc.NomorInvoiceLama = NomorInvoiceLama
            Usc.UpdateBaris()
        End If
    End Sub

    Sub UpdateTampilanTabel_SeluruhBukuPengawasanHutangUsaha()
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Afiliasi)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_NonAfiliasi)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_USD)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_AUD)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_JPY)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_CNY)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_EUR)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_SGD)
        UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(usc_BukuPengawasanHutangUsaha_Impor_GBP)
    End Sub

    Sub UpdateTampilanTabel_PerUscBukuPengawasanHutangUsaha(Usc As wpfUsc_BukuPengawasanHutangUsaha)
        If Not Usc.StatusAktif Then Return
        Usc.JenisRelasi = JenisRelasi
        Usc.NomorBPHU = NomorPembelian
        Usc.NomorPembelian = NomorPembelian
        Usc.JenisInvoice = JenisInvoice
        Usc.JenisProduk = JenisProduk_Induk
        Usc.AngkaInvoice = AngkaInvoice
        Usc.NomorInvoice = NomorInvoice
        Usc.NomorFakturPajak = NomorFakturPajak
        Usc.TanggalInvoice = TanggalFormatTampilan(TanggalInvoice)
        Usc.MasaJatuhTempo = JumlahHariJatuhTempo
        Usc.NomorSJBAST = NomorSJBAST
        Usc.TanggalSJBAST = TanggalFormatTampilan(TanggalSJBAST)
        Usc.NomorPO = NomorPOProduk
        Usc.TanggalPO = AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk)
        Usc.KodeProject = KodeProjectProduk
        Usc.NamaProduk = NamaProduk
        Usc.KodeSupplier = KodeSupplier
        Usc.NamaSupplier = NamaSupplier
        Usc.JumlahHarga = JumlahHargaKeseluruhan
        Usc.JumlahHarga_Asing = JumlahHargaKeseluruhan_Asing
        Usc.DiskonRp = Diskon_Rp
        Usc.DiskonAsing = DiskonAsing
        Usc.DasarPengenaanPajak = DPP
        Usc.JenisPPN = JenisPPN
        Usc.PPN = PPN
        Usc.JenisPPh = JenisPPh
        Usc.PPhTerutang = PPhTerutang
        Usc.PPhDitanggung = PPhDitanggung
        Usc.PPhDipotong = PPhDipotong
        Usc.TagihanBruto = TotalTagihan_Kotor
        Usc.BiayaTransportasi = OngkosKirim
        Usc.BiayaMaterai = BiayaMaterai
        Usc.BiayaBiaya_Asing = Freight + Insurance
        Usc.Retur = ReturDPP + ReturPPN
        Usc.TagihanNetto = TotalTagihan
        Usc.JumlahHutangUsaha = JumlahHutangUsaha
        Usc.JumlahHutang_Asing = JumlahHutangUsaha_Asing
        Usc.KeteranganJatuhTempo = TanggalFormatTampilan(TanggalJatuhTempo)
        Usc.Catatan = PenghapusEnter(Catatan)
        Usc.NomorJV_Pembelian = NomorJV
        'Untuk invoice baru, pembayaran = 0 dan sisa = jumlah hutang
        If FungsiForm = FungsiForm_TAMBAH Then
            Usc.TanggalBayar_Arr = Kosongan
            Usc.JumlahBayarTagihan = 0
            Usc.JumlahBayar_Asing = 0
            Usc.SisaTagihan = TotalTagihan
            Usc.SisaHutangUsaha = JumlahHutangUsaha
            Usc.SisaHutang_Asing = JumlahHutangUsaha_Asing
            Usc.SisaHutang_Asing_IDR = JumlahHutangUsaha_Asing * Kurs
            Usc.LOS = los_OS
            Usc.Referensi = Kosongan
            Usc.TambahBaris()
        End If
        If FungsiForm = FungsiForm_EDIT Then
            Usc.NomorInvoiceLama = NomorInvoiceLama
            Usc.UpdateBaris()
        End If
    End Sub

End Class
