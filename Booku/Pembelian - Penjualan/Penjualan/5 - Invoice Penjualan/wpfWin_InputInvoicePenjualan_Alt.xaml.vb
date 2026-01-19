Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives

Public Class wpfWin_InputInvoicePenjualan_Alt


    Public JudulForm
    Public FungsiForm
    Public NomorID
    Public InvoiceDenganPO As Boolean

    Public NomorJV
    Public COADebet
    Public JumlahDebet

    Public MetodePembayaran
    Public BasisPerhitunganTermin

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN
    Public KodeMataUang
    Dim Kurs As Decimal

    Dim LokasiWP As String
    Dim JenisJasa As String

    Dim EksekusiPerhitungan_BiayaTransportasi As Boolean

    Public KunciTanggalInvoice As Boolean

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
    Dim JumlahNota As Int64
    Dim UangMukaPlusTermin_Persen As Decimal
    Dim UangMukaPlusTermin_Rp As Int64                          '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim Termin_Persen As Decimal
    Dim Termin_Rp As Int64                     '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
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
    Dim OngkosKirim As Int64                    '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim PPhDitanggung_BerdasarkanPO As Int64    '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim TotalTagihan As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturDPP As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public ReturPPN As Int64                   '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Dim JumlahPiutangUsaha As Int64

    Dim DPP_11Per12 As Int64
    Dim TarifPPN_11Per12


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
    Dim TotalHargaPerItem As Int64

    'Asing :
    Dim HargaSatuan_Asing As Decimal
    Dim DiskonAsing As Decimal
    Dim TotalHargaPerItem_Asing As Decimal
    Dim JumlahHargaKeseluruhan_Asing As Decimal
    Dim Termin_Asing As Decimal
    Dim TotalTagihan_Kotor_Asing As Decimal
    Dim JumlahNota_Asing As Decimal
    Dim Insurance As Decimal
    Dim Freight As Decimal
    Dim TotalTagihan_Asing As Decimal
    Dim JumlahPiutangUsaha_Asing As Decimal
    Dim UangMukaPlusTermin_Asing As Decimal

    'Variabel Tabel Index :
    Dim NomorUrutProduk_Terseleksi
    Dim JenisProdukPerItem_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim KodeProjectProduk_Terseleksi
    Dim TotalHarga_Terseleksi
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

    Public KodeSetoran

    Public JualAsset As Boolean
    Dim Asset As Integer
    Public KelompokHarta

    Dim COAPenjualanBarangAtauAsset
    Dim COAJasa

    Public PenyimpananInvoicePenjualan As Boolean

    Public AdaPenyimpananjurnal As Boolean

    Public AdaPPh As Boolean

    Public TahapTermin

    Dim MataUang

    Dim MitraLuarNegeri

    Public DestinasiPenjualan
    Public PenjualanLokal As Boolean
    Public PenjualanEkspor As Boolean

    Public JenisRelasi As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        'If LevelUserAktif < LevelUser_81_TimIT Then
        '    MenuDalamPerbaikan()
        '    Close()
        '    Return
        'End If

        ProsesLoadingForm = True
        EksekusiLogikaAdaPPh = False

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
        End If

        LogikaDestinasiPenjualan()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            lbl_NomorFakturPajak.Visibility = Visibility.Visible
            txt_NomorFakturPajak.Visibility = Visibility.Visible
        Else
            lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
            txt_NomorFakturPajak.Visibility = Visibility.Collapsed
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            EksekusiKodeLogikaPPN = True
            SistemPenomoranOtomatis_InvoicePenjualan()
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                JenisProduk_Induk = Kosongan
                Do While JenisProduk_Induk = Kosongan
                    win_PilihJenisProdukInduk = New wpfWin_PilihJenisProdukInduk
                    win_PilihJenisProdukInduk.ResetForm()
                    win_PilihJenisProdukInduk.ShowDialog()
                    JenisProduk_Induk = win_PilihJenisProdukInduk.JenisProduk_Induk
                    If JenisProduk_Induk = Kosongan Then
                        Pesan_Peringatan("Silakan pilih Jenis Produk..!")
                    End If
                Loop
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua And Not InvoiceDenganPO Then
                PesanUntukProgrammer("Jenis Produk belum ditentukan..!")
                Me.Close()
                Return
            End If
            If JenisProduk_Induk = Kosongan Or JenisProduk_Induk = JenisProduk_Semua Then
                JudulForm = "Input Invoice Penjualan"
            Else
                JudulForm = "Input Invoice Penjualan - " & JenisProduk_Induk
            End If
            NP = "N"
            If MetodePembayaran = MetodePembayaran_Termin Then VisibilitasTabelSJBAST(False)
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_PEMBETULAN _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            JudulForm = "Edit Invoice Penjualan - " & JenisProduk_Induk
            NomorInvoiceLama = NomorInvoice
            dtp_TanggalInvoice.IsEnabled = False
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
                NPLama = "P" & AmbilAngka_TanpaMinus(Right(NomorInvoiceLama, 3))
                PembetulanKe = AmbilAngka_TanpaMinus(Right(NomorInvoiceLama, 3)) + 1
                NP = "P" & PembetulanKe
                txt_NomorInvoice.Text = Replace(NomorInvoiceLama, NPLama, NP)
            End If
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            JudulForm = "Invoice Penjualan - " & JenisProduk_Induk
            btn_Batal.Content = teks_Tutup
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("FungsiForm belum ditentukan..!!!")
        If MetodePembayaran = Kosongan Then PesanUntukProgrammer("Metode Pembayaran belum ditentukan...!!!")

        If NP = "N" Then
            lbl_TanggalInvoice.Text = "Tanggal Invoice"
        Else
            lbl_TanggalInvoice.Text = "Tanggal Pembetulan"
        End If

        Title = JudulForm

        LogikaTampilanKolomDPPBarangDanJasa()

        btn_Tambahkan.Visibility = Visibility.Visible
        If MetodePembayaran = MetodePembayaran_Normal Then
            VisibilitasBarisPO(False)
            VisibilitasTabelSJBAST(True)
            If InvoiceDenganPO Then
                If PenjualanLokal Then pnl_CRUDProduk.Visibility = Visibility.Collapsed
                If PenjualanEkspor Then
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

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            txt_Referensi.Text = dr.Item("Referensi")
            txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            If InvoiceDenganPO Then
                IsiValueComboBypassTerkunci(cmb_JenisPPN, dr.Item("Jenis_PPN"))
                IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, dr.Item("Perlakuan_PPN"))
            Else
                KontenCombo_JenisPPN()
                cmb_JenisPPN.SelectedValue = dr.Item("Jenis_PPN")
                cmb_PerlakuanPPN.SelectedValue = dr.Item("Perlakuan_PPN")
            End If
            cmb_KodeMataUang.SelectedValue = dr.Item("Kode_Mata_Uang")
            txt_Kurs.Text = dr.Item("Kurs")
            If MitraLuarNegeri Then
                txt_NomorPO.Text = dr.Item("Nomor_PO_Produk")
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
                        If PenjualanLokal Then
                            Termin_Rp = FormatUlangInt64(dr.Item("Termin"))
                            Termin_Persen = PembulatanDesimal2Digit((Termin_Rp / JumlahNota) * 100)
                        Else
                            Termin_Asing = dr.Item("Termin")
                            Termin_Persen = PembulatanDesimal2Digit((Termin_Asing / JumlahNota_Asing) * 100)
                        End If
                End Select
                If JenisPPN = JenisPPN_Include Then
                    Termin_Rp = dr.Item("Total_Tagihan_Kotor")
                Else
                    If PenjualanLokal Then Termin_Rp = dr.Item("Dasar_Pengenaan_Pajak")
                    If PenjualanEkspor Then Termin_Asing = dr.Item("Total_Tagihan_Kotor")
                End If
                VisibilitasBarisTermin(True)
            End If
            txt_Termin_Persen.Text = Termin_Persen
            If PenjualanLokal Then txt_Termin_Rp.Text = Termin_Rp
            If PenjualanEkspor Then txt_Termin_Rp.Text = Termin_Asing
            JumlahHargaKeseluruhan_Asing = JumlahNota_Asing
            'Awal Coding untuk logika baru PPN : -----------------------------------------------
            txt_TarifPPN.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPN"))
            txt_DasarPengenaanPajak.Text = FormatUlangInt64(dr.Item("Dasar_Pengenaan_Pajak"))
            txt_TotalTagihan_Kotor.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Total_Tagihan_Kotor"))
            txt_PPN.Text = FormatUlangInt64(dr.Item("PPN"))
            'Akhir Coding untuk logika baru PPN : ----------------------------------------------
            txt_PPhTerutang.Text = dr.Item("PPh_Terutang") 'Ini jangan dihapus...!
            IsiValueComboBypassTerkunci(cmb_JenisPPh, dr.Item("Jenis_PPh"))
            txt_TarifPPh.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPh"))
            If TarifPPh > 0 Then AdaPPh = True
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            txt_TotalTagihan.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Total_Tagihan"))
            JenisPenjualan = dr.Item("Jenis_Penjualan")
            If JenisPenjualan = JenisPenjualan_Tunai Then KondisiForm_JenisPenjualanTunai()
            cmb_SaranaPembayaran.SelectedValue = dr.Item("Sarana_Pembayaran")
            txt_BiayaAdministrasiBank.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Biaya_Administrasi_Bank"))
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            txt_OngkosKirim.Text = dr.Item("Biaya_Transportasi")
            txt_Insurance.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Insurance"))
            txt_Freight.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Freight"))
            Asset = dr.Item("Asset")
            If Asset = 1 Then JualAsset = True
            If Asset = 0 Then JualAsset = False
            If TahapTermin = TahapTermin_UangMuka Then VisibilitasTabelSJBAST(False)
        End If
        AksesDatabase_Transaksi(Tutup)

        If JualAsset = True Then
            Asset = 1
            txt_PPhDitanggung.IsEnabled = False
            txt_PPhDipotong.IsEnabled = False
        Else
            Asset = 0
        End If

        CekNomorSJBAST()

        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            If Not MitraLuarNegeri Then
                lbl_DPPBarang.Visibility = Visibility.Visible
                lbl_DPPJasa.Visibility = Visibility.Visible
                txt_DPPBarang.Visibility = Visibility.Visible
                txt_DPPJasa.Visibility = Visibility.Visible
            End If
        Else
            lbl_DPPBarang.Visibility = Visibility.Collapsed
            lbl_DPPJasa.Visibility = Visibility.Collapsed
            txt_DPPBarang.Visibility = Visibility.Collapsed
            txt_DPPJasa.Visibility = Visibility.Collapsed
        End If

        ProsesLoadingForm = False

        If PerusahaanSebagaiPKP And PenjualanLokal And TarifPPN < 10 Then
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        End If


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
        End If

        EksekusiLogikaAdaPPh = True

        If FungsiForm <> FungsiForm_TAMBAH Then Perhitungan()

        SembunyikanElemenPPN_UntukPenjualanEkspor()  'Coding ini aman disimpan di akhr. Sebab, penjualan ekspor tidak ada PPN-nya.

    End Sub


    Sub IsiTabelProduk()
        Dim NomorSJBAST
        Dim TanggalDiterimaSJBAST
        Dim TanggalSJBAST
        Dim JenisProdukPerItem
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
        Dim NomorUrut = 0
        Dim HargaSatuan_Asing
        Dim JumlahHargaPerItem_Asing
        Dim DiskonPerItem_Asing
        Dim TotalHargaPerItem_Asing
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
            TanggalDiterimaSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_Diterima_SJ_BAST_Produk"))
            NomorPO = dr.Item("Nomor_PO_Produk")
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
            HargaSatuan_Asing = dr.Item("Harga_Satuan")
            JumlahHargaPerItem_Asing = JumlahProduk * HargaSatuan_Asing
            DiskonPerItem_Asing = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem_Asing = JumlahHargaPerItem_Asing - DiskonPerItem_Asing
            JumlahNota_Asing += TotalHargaPerItem_Asing
            KodeProject = dr.Item("Kode_Project_Produk")
            datatabelUtama.Rows.Add(NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST,
                                    NomorPO, NamaProduk, DeskripsiProduk,
                                    JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                                    DiskonPerItem_Persen & " %", DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProject)
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
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            IsiValueComboBypassTerkunci(cmb_JenisPPN, dr.Item("Jenis_PPN"))
            cmb_PerlakuanPPN.SelectedValue = dr.Item("Perlakuan_PPN")
            If MetodePembayaran = MetodePembayaran_Termin Or Not InvoiceDenganPO Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
                TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima_SJ_BAST_Produk"))
            End If
            Dim Tabel
            Dim KolomNomor
            Dim KolomTanggal
            If AmbilTeksKiri(NomorSJBAST, 2) = AmbilTeksKiri(AwalanSJ, 2) Then
                Tabel = "tbl_Penjualan_SJ"
                KolomNomor = "Nomor_SJ"
                KolomTanggal = "Tanggal_SJ"
            Else
                Tabel = "tbl_Penjualan_BAST"
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
                If MetodePembayaran = MetodePembayaran_Termin Or Not InvoiceDenganPO Then
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
        If InvoiceDenganPO And MetodePembayaran = MetodePembayaran_Normal Then Kosongkan_TabelProduk()
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

    Dim EksekusiKodeSubPerhitungan As Boolean   'Ini untuk mencegah looping
    Dim JumlahLoopPerhitungan = 0              'Ini hanya untuk keperluan mengetahui jumlah loop pada Sub Perhitungan
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

        If Not AdaPPh Then txt_TarifPPh.Text = Kosongan

        'JumlahLoopPerhitungan += 1
        'PesanUntukProgrammer("Jumlah Loop Perhitungan : " & JumlahLoopPerhitungan & Enter2Baris &
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
            If row("Jenis_produk_Per_Item") = JenisProduk_Jasa Then DPPJasa += AmbilAngka(row("Total_Harga"))
        Next

        If PenjualanLokal Then
            JumlahNota = JumlahHargaKeseluruhan - Diskon_Rp
            txt_JumlahNota.Text = JumlahNota
        Else
            JumlahNota_Asing = JumlahHargaKeseluruhan_Asing - DiskonAsing
            txt_JumlahNota.Text = JumlahNota_Asing
        End If

        RasioDPPBarang = DPPBarang / HitunganHarga_Relatif

        If MetodePembayaran = MetodePembayaran_Normal Then
            txt_Termin_Persen.Text = 100
            If PenjualanLokal Then
                txt_Termin_Rp.Text = HitunganHarga_Relatif
            Else
                txt_Termin_Rp.Text = JumlahNota_Asing
            End If
        Else
            txt_Termin_Persen.Text = Termin_Persen
            If PenjualanLokal Then
                txt_Termin_Rp.Text = Termin_Rp
                DPPBarang = Termin_Rp * RasioDPPBarang
            Else
                txt_Termin_Rp.Text = JumlahNota_Asing * (Termin_Persen / 100)
                'If JenisProduk_Induk = JenisProduk_Jasa Then DPPJasa_Asing = Termin_Asing
            End If
        End If

        HitunganHarga_Relatif = Termin_Rp

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            txt_Diskon_Rp.Text = Diskon_Rp
            If PenjualanLokal Then
                txt_DasarPengenaanPajak.Text = HitunganHarga_Relatif
            Else
                txt_DasarPengenaanPajak.Text = 0
                'txt_DPPJasa.Text = DPPJasa_Asing
            End If
        End If

        LogikaTarifPPN()
        If PenjualanLokal Then
            Select Case JenisPPN
                Case JenisPPN_NonPPN
                    PPN = 0
                    TotalTagihan_Kotor = DPP
                Case JenisPPN_Exclude
                    PPN = DPP * Persen(TarifPPN)
                    Select Case PerlakuanPPN
                        Case PerlakuanPPN_Dibayar
                            TotalTagihan_Kotor = DPP + PPN
                        Case PerlakuanPPN_Dipungut
                            TotalTagihan_Kotor = DPP
                        Case PerlakuanPPN_TidakDipungut
                            TotalTagihan_Kotor = DPP
                        Case PerlakuanPPN_Ditanggung
                            TotalTagihan_Kotor = DPP
                    End Select
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
                        If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP
                        If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP
                    End If
                    '--------------------------------------------------------------------------------
                    txt_Diskon_Rp.Text = Diskon_Rp
            End Select
        Else
            TotalTagihan_Kotor_Asing = JumlahNota_Asing
            If MetodePembayaran = MetodePembayaran_Termin Then TotalTagihan_Kotor_Asing = Termin_Asing + Insurance + Freight
        End If

        If JenisProduk_Induk = JenisProduk_JasaKonstruksi Or JenisProduk_Induk = JenisProduk_Jasa Then
            DPPJasa = DPP
            DPPBarang = 0
        ElseIf JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            DPPJasa = DPP - DPPBarang
        ElseIf JenisProduk_Induk = JenisProduk_Barang Then
            If PenjualanLokal Then DPPBarang = DPP
        End If

        PPhTerutang = DPPJasa * Persen(TarifPPh)
        If JenisProduk_Induk = JenisProduk_Barang And PerlakuanPPN = PerlakuanPPN_Dipungut Then PPhTerutang = DPP * Persen(TarifPPh)
        If JualAsset = True Then PPhTerutang = DPP * Persen(TarifPPh)

        If JumlahHargaKeseluruhan > 0 Then
            txt_DPPBarang.Text = DPPBarang
            txt_DPPJasa.Text = DPPJasa
        End If

        If PenjualanLokal Then
            txt_DasarPengenaanPajak.Text = DPP
            txt_PPhTerutang.Text = PPhTerutang
        End If

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

        PerhitunganFinal()

    End Sub
    Sub PerhitunganFinal()

        If JualAsset = True Then
            PPhDipotong = 0
            PPhDitanggung = 0
        Else
            PPhDipotong = PPhTerutang - PPhDitanggung
        End If

        If PenjualanLokal Then
            txt_PPhDipotong.Text = PPhDipotong
            TotalTagihan = TotalTagihan_Kotor + OngkosKirim - PPhDipotong
            JumlahPiutangUsaha = TotalTagihan_Kotor + OngkosKirim
        Else
            txt_DasarPengenaanPajak.Text = 0
            TotalTagihan_Asing = TotalTagihan_Kotor_Asing + Freight + Insurance
            JumlahPiutangUsaha_Asing = TotalTagihan_Asing
        End If

        'PesanUntukProgrammer(
        '    "Total Tagihan Kotor : " & TotalTagihan_Kotor & Enter2Baris &
        '    "Jummlah Piutang Usaha : " & JumlahPiutangUsaha & Enter2Baris &
        '    "")

        'If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
        If PerlakuanPPN = Kosongan And (JenisPPN <> JenisPPN_NonPPN Or JenisPPN <> Kosongan) Then
            txt_TarifPPN.Text = Kosongan
            txt_PPN.Text = Kosongan
            If PenjualanLokal Then
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
                txt_TotalTagihan.Text = TotalTagihan
            End If
        Else
            If PenjualanLokal Then
                txt_PPN.Text = PPN
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
                txt_TotalTagihan.Text = TotalTagihan
            Else
                txt_PPN.Text = 0
                txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor_Asing
                txt_TotalTagihan.Text = TotalTagihan_Asing
            End If
        End If

        If TahapTermin = TahapTermin_Pelunasan Then
            VisibilitasUangMukaPlusTermin(True)
            txt_UangMukaPlusTermin_Persen.Text = (100 - Termin_Persen)
            If PenjualanLokal Then txt_UangMukaPlusTermin_Rp.Text = JumlahNota - Termin_Rp
            If PenjualanEkspor Then txt_UangMukaPlusTermin_Rp.Text = JumlahNota_Asing - Termin_Asing
        Else
            VisibilitasUangMukaPlusTermin(False)
            txt_UangMukaPlusTermin_Persen.Text = Kosongan
            txt_UangMukaPlusTermin_Rp.Text = Kosongan
        End If

        BarisTotalTabel()

        If InvoiceDenganPO And PPhTerutang = 0 Then
            EksekusiKode = False
            cmb_JenisPPh.SelectedValue = Kosongan
            EksekusiKode = True
        Else
            If JenisPPh = Kosongan Or JenisPPh = JenisPPh_NonPPh Then
                JenisPPh = JenisPPh_NonPPh
            End If
        End If

        If AdaPPh Then
            VisibilitasElemenPPh(True)
            If PPhTerutang = 0 Then VisibilitasPPhDitanggungDipotong(False) 'Ini ada kaitannya dengan Jenis Jasa : Lainnya. Jadi jangan semudah itu dirubah.
            If JenisPPh = JenisPPh_Pasal22_Lokal Then VisibilitasPPhDitanggungDipotong(False)
        Else
            VisibilitasElemenPPh(False)
        End If

        LogikaTampilanPPN()

        'EksekusiKodeLogikaPPN = True 'Ini harus paling ujung, dan tidak boleh dihapus.
        EksekusiKodeSubPerhitungan = True

        SembunyikanElemenPPN_UntukPenjualanEkspor()  'Coding ini aman disimpan di akhr. Sebab, penjualan ekspor tidak ada PPN-nya.

        grd_KolomPerhitungan.Visibility = Visibility.Visible

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
        txt_UangMukaPlusTermin_Persen.Text = Kosongan
        txt_UangMukaPlusTermin_Rp.Text = Kosongan
        txt_Termin_Persen.Text = Kosongan
        txt_Termin_Rp.Text = Kosongan
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
        txt_TotalTagihan.Text = Kosongan
        'Asing :
        txt_Insurance.Text = Kosongan
        txt_Freight.Text = Kosongan
    End Sub

    Sub OngkosKirim_BerdasarkanSuratJalan() 'Sub ini sudah tidak berguna. Nanti hapus saja
        If EksekusiPerhitungan_BiayaTransportasi = True Then
            OngkosKirim = 0
            For Each row As DataRow In datatabelSJBAST.Rows
                OngkosKirim += AmbilAngka(row("Biaya_Transportasi"))
            Next
            txt_OngkosKirim.Text = OngkosKirim
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        NomorJV = 0

        MataUang = Kosongan

        NomorInvoiceLama = Kosongan

        MetodePembayaran = Kosongan
        DestinasiPenjualan = DestinasiPenjualan_Lokal
        JenisRelasi = Pilihan_Semua

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan
        JenisPPh = Kosongan

        InvoiceDenganPO = True

        KunciTanggalInvoice = False

        NomorID = 0
        AngkaInvoice = 0
        txt_NomorInvoice.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Visibility = Visibility.Collapsed
        NP = Kosongan
        KosongkanDatePicker(dtp_TanggalInvoice)
        VisibilitasBarisPO(False)
        VisibilitasUangMukaPlusTermin(False)
        rdb_JumlahHariJatuhTempo.IsChecked = False
        rdb_TanggalJatuhTempo.IsChecked = False
        txt_JumlahHariJatuhTempo.IsEnabled = False
        lbl_JumlahHariJatuhTempo.IsEnabled = False
        txt_JumlahHariJatuhTempo.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
        KondisiForm_JenisPenjualanTempo()
        dtp_TanggalJatuhTempo.IsEnabled = False
        KosongkanDatePicker(dtp_TanggalJatuhTempo)
        txt_Referensi.Text = Kosongan
        KontenCombo_JenisInvoice()
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_AlamatCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        btn_TambahSJBAST.IsEnabled = False
        VisibilitasTabelSJBAST(True)
        KosongkanValueElemenRichTextBox(txt_Catatan)
        'datagridTotal.Visibility = Visibility.Collapsed
        VisibilitasBarisDiskon(False)
        VisibilitasBarisTermin(False)
        NomorFakturPajak = Kosongan
        KontenCombo_JenisPPh()
        VisibilitasElemenPPh(False)
        txt_TarifPPN.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        AdaPPh = False
        txt_Kurs.Text = Kosongan
        txt_OngkosKirim.Text = Kosongan
        KetersediaanTombolHitung(False)
        KetersediaanTombolSimpan(False)
        btn_Batal.Content = teks_Batal
        ReturDPP = 0
        ReturPPN = 0
        Kosongkan_TabelSJBAST()
        Kosongkan_TabelProduk()
        cmb_JenisPPh.Text = Kosongan '(Jangan dihapus..!)

        lbl_PPh.IsEnabled = True
        txt_TarifPPh.IsEnabled = True
        txt_PPhTerutang.IsEnabled = True
        txt_PPhDitanggung.IsEnabled = True
        txt_PPhDipotong.IsEnabled = True

        lbl_KodeMataUang.Visibility = Visibility.Collapsed
        cmb_KodeMataUang.Visibility = Visibility.Collapsed
        lbl_Kurs.Visibility = Visibility.Collapsed
        txt_Kurs.Visibility = Visibility.Collapsed

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

        TahapTermin = Kosongan
        BasisPerhitunganTermin = Kosongan

        NomorSJBAST_Aktif = Kosongan
        TanggalSJBAST_Aktif = Kosongan
        TanggalDiterimaSJBAST_Aktif = Kosongan

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

    Sub VisibilitasBarisDiskon(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon_Persen.Visibility = Visibility.Collapsed
            lbl_PersenDiskon.Visibility = Visibility.Collapsed
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

    Sub KontenCombo_JenisInvoice()
        cmb_JenisInvoice.Items.Clear()
        cmb_JenisInvoice.Items.Add(JenisInvoice_Biasa)
        'Untuk Jenis Invoice Gabungan, hanya baru bisa digunakan untuk Jenis Produk Induk : Barang
        If JenisProduk_Induk = JenisProduk_Barang Then cmb_JenisInvoice.Items.Add(JenisInvoice_Gabungan)
        cmb_JenisInvoice.SelectedValue = JenisInvoice_Biasa '(Defaultnya ini. Jangan kosongan...!)
    End Sub

    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        If PerusahaanSebagaiPKP = True And Not MitraLuarNegeri Then
            cmb_JenisPPN.Items.Add(JenisPPN_Exclude)
            cmb_JenisPPN.Items.Add(JenisPPN_Include)
            cmb_JenisPPN.Text = Kosongan
        Else
            IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN_NonPPN)
            SembunyikanElemenPajak()
        End If
    End Sub

    Sub LogikaTarifPPN()
        If ProsesLoadingForm Then Return
        If Not EksekusiKodeLogikaPPN Then Return
        If dtp_TanggalInvoice.Text = Kosongan Then Return
        If PerusahaanSebagaiPKP And Not MitraLuarNegeri Then
            txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalInvoice)
        Else
            txt_TarifPPN.Text = Kosongan
        End If
    End Sub
    Sub LogikaTampilanPPN()
        If ProsesLoadingForm Then Return
        If Not EksekusiKodeLogikaPPN Then Return
        Dim TampilkanKolom As Boolean = False
        If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PenjualanLokal) Then TampilkanKolom = True
        If Not PerusahaanSebagaiPKP Or MitraLuarNegeri Then
            txt_TarifPPN.Text = Kosongan
            txt_TarifPPN_11Per12.Text = Kosongan
            txt_PPN.Text = Kosongan
            Return
        End If
        If dtp_TanggalInvoice.Text = Kosongan Then Return
        LogikaPPN(dtp_TanggalInvoice.SelectedDate, DPP, TarifPPN, DPP_11Per12, TarifPPN_11Per12)
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
            If PenjualanLokal Then
                If TampilkanKolom Then txt_TarifPPN_11Per12.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak_11Per12.Text = DPP_11Per12
            Else
                txt_DasarPengenaanPajak_11Per12.Text = Kosongan
            End If
        Else
            If TampilkanKolom Then txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            If PenjualanLokal Then
                If TampilkanKolom Then txt_TarifPPN.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak.Text = DPP
            End If
            txt_DasarPengenaanPajak_11Per12.Text = Kosongan
        End If
        If TampilkanKolom Then
            If PenjualanEkspor Then
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
        If PenjualanLokal Then
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
        KosongkanItemCombo(cmb_PerlakuanPPN)
        txt_TarifPPN.Text = Kosongan
    End Sub

    Sub SembunyikanElemenPPN_UntukPenjualanEkspor()
        If PenjualanEkspor Then
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Ditanggung)
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal22_Lokal)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal26)
        cmb_JenisPPh.Items.Add(JenisPPh_NonPPh)
        cmb_JenisPPh.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeMataUangAsing()
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang)
    End Sub


    Dim EksekusiLogikaAdaPPh As Boolean
    Sub LogikaAdaPPh(Ada As Boolean)
        If Not EksekusiLogikaAdaPPh Then Return
        If JenisProduk_Induk = Kosongan Then Return
        AdaPPh = False
        If JenisProduk_Induk = JenisProduk_Barang Then
            If PerlakuanPPN = PerlakuanPPN_Dipungut Then
                AdaPPh = True
            Else
                AdaPPh = False
            End If
        Else
            AdaPPh = True
        End If
        If Ada = False Then AdaPPh = False
        If Not MitraSebagaiPemotongPPh(KodeCustomer) Then AdaPPh = False
        If InvoiceDenganPO Then
            'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
            Return '(Jika Invoice menggunakan PO, maka value-value dibawah sudah diisi dari SJ/BAST)
        End If
        If AdaPPh = True Then
            If JenisProduk_Induk = JenisProduk_Barang Then
                IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal22_Lokal)
                txt_TarifPPh.Text = PembulatanDesimal2Digit(1.5)
            Else
                PenentuanJenisPPh_DanTarifPPh_Penjualan(MitraLuarNegeri, JenisProduk_Induk, cmb_JenisPPh, txt_TarifPPh)
            End If
        Else
            IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_NonPPh)
            txt_TarifPPh.Text = Kosongan
        End If
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Sub VisibilitasElemenPPh(Visibilitas As Boolean)
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Or (JenisTahunBuku = JenisTahunBuku_LAMPAU And PenjualanLokal) Then
                lbl_PPh.Visibility = Visibility.Visible
                cmb_JenisPPh.Visibility = Visibility.Visible
                txt_TarifPPh.Visibility = Visibility.Visible
                lbl_PersenPPh.Visibility = Visibility.Visible
                txt_PPhTerutang.Visibility = Visibility.Visible
                VisibilitasPPhDitanggungDipotong(True)
            End If
        Else
            lbl_PPh.Visibility = Visibility.Collapsed
            cmb_JenisPPh.Visibility = Visibility.Collapsed
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

    Sub SistemPenomoranOtomatis_InvoicePenjualan()

        'If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return
        If FungsiForm = FungsiForm_TAMBAH Then AngkaInvoice = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_Invoice", "Angka_Invoice") + 1
        If dtp_TanggalInvoice.Text <> Kosongan Then
            NomorInvoice = AwalanINV & AngkaInvoice.ToString & "-" &
                BulanRomawi(dtp_TanggalInvoice.SelectedDate.Value.Month) & "-" &
                dtp_TanggalInvoice.SelectedDate.Value.Year
        End If
        txt_NomorInvoice.Text = NomorInvoice
        NomorPenjualan = AwalanPENJ_PlusTahunBuku & AngkaInvoice

    End Sub


    Private Sub cmb_JenisInvoice_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisInvoice.SelectionChanged
        JenisInvoice = cmb_JenisInvoice.SelectedValue
        If JenisInvoice = JenisInvoice_Biasa _
            And JumlahBarisSJBAST > 0 _
            And ProsesResetForm = False _
            Then
            Kosongkan_TabelSJBAST()
            Pesan_Peringatan("Daftar Surat Jalan / BAST telah dikosongkan." & Enter2Baris & "Silakan isi kembali.")
            btn_TambahSJBAST.Focus()
        End If
        KunciTanggalInvoice = False
        If JenisInvoice = JenisInvoice_Gabungan Then btn_TambahSJBAST.IsEnabled = True
    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub


    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
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
            If ProsesIsiValueForm = False And ProsesResetForm = False And NP = "N" Then
                If FungsiForm <> FungsiForm_PEMBETULAN Then SistemPenomoranOtomatis_InvoicePenjualan()
            End If
            KondisiFormSetelahPerubahan()
            If InvoiceDenganPO And EksekusiKode And NP = "N" Then
                Kosongkan_TabelSJBAST()
                If txt_NomorPO.Text <> Kosongan Then
                    txt_NomorPO.Text = Kosongan
                    PesanPemberitahuan("Kolom 'Nomor PO' telah dikosongkan. Silakan isi kembali.")
                End If
            End If
            LogikaJenisPenjualan()
            PenentuanKurs()
            'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
        End If
    End Sub


    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        txt_NamaCustomer.Text = AmbilValue_NamaMitra(KodeCustomer)
        txt_PICCustomer.Text = AmbilValue_PICMitra(KodeCustomer)
        txt_AlamatCustomer.Text = AmbilValue_AlamatMitra(KodeCustomer)
        If KodeCustomer = Kosongan Then
            btn_TambahSJBAST.IsEnabled = False
        Else
            btn_TambahSJBAST.IsEnabled = True
        End If
        If MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then
            MitraLuarNegeri = True
            LokasiWP = LokasiWP_LuarNegeri
        Else
            MitraLuarNegeri = False
            LokasiWP = LokasiWP_DalamNegeri
        End If
        KontenCombo_JenisPPN()
        txt_NomorPO.Text = Kosongan
        Kosongkan_TabelSJBAST()
        Kosongkan_TabelProduk()
        LogikaAdaPPh(True)
        LogikaDestinasiPenjualan()
        If MetodePembayaran = MetodePembayaran_Normal Then VisibilitasTabelSJBAST(True)
        If MetodePembayaran = MetodePembayaran_Termin Then VisibilitasTabelSJBAST(False)
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub btn_PilihCustomer_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        If ((MetodePembayaran = MetodePembayaran_Termin Or Not InvoiceDenganPO) And dtp_TanggalInvoice.Text = Kosongan) _
            Or ((PenjualanEkspor And InvoiceDenganPO) And dtp_TanggalInvoice.Text = Kosongan) _
            Then
            PesanPeringatan("Silakan isi 'Tanggal Invoice' terlebih dahulu.")
            dtp_TanggalInvoice.Focus()
            Return
        End If
        PesanUntukProgrammer("Jenis Relasi : " & JenisRelasi & Enter2Baris &
                             "Destinasi : " & DestinasiPenjualan)
        If PenjualanLokal Then
            If JenisRelasi = JenisRelasi_Afiliasi Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua)
            If JenisRelasi = JenisRelasi_NonAfiliasi Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Semua)
            If JenisRelasi = Pilihan_Semua Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, LokasiWP_DalamNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
        End If
        If PenjualanEkspor Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, LokasiWP_LuarNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
    End Sub
    Private Sub txt_PICCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PICCustomer.TextChanged
    End Sub
    Private Sub txt_AlamatCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AlamatCustomer.TextChanged
    End Sub

    Sub LogikaDestinasiPenjualan()
        If PenjualanLokal Then
            MataUang = MataUang_Rupiah
            IsiValueComboBypassTerkunci(cmb_KodeMataUang, KodeMataUang_IDR)
            cmb_KodeMataUang.SelectedValue = KodeMataUang_IDR
            lbl_KodeMataUang.Visibility = Visibility.Collapsed
            cmb_KodeMataUang.Visibility = Visibility.Collapsed
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Text = 1
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
            lbl_DPP.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            lbl_PPN.Visibility = Visibility.Visible
            lbl_PersenPPN.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
            lbl_OngkosKirim.Visibility = Visibility.Visible
            txt_OngkosKirim.Visibility = Visibility.Visible
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Visible
            txt_TotalTagihan_Kotor.Visibility = Visibility.Visible
            'txt_TotalTagihan.Visibility = Visibility.Visible
            'Kolom-kolom Kanan - Asing :
            lbl_Insurance.Visibility = Visibility.Collapsed
            txt_Insurance.Visibility = Visibility.Collapsed
            lbl_Freight.Visibility = Visibility.Collapsed
            txt_Freight.Visibility = Visibility.Collapsed
            'Styling :
            txt_JumlahNota.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_Diskon_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_UangMukaPlusTermin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_Termin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_DPPBarang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_DPPJasa.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_DasarPengenaanPajak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_DasarPengenaanPajak_11Per12.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_PPN.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_TotalTagihan_Kotor.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_PPhTerutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_PPhDitanggung.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_PPhDipotong.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_Insurance.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_TotalTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
        End If
        If PenjualanEkspor Then
            MataUang = MataUang_Asing
            KontenCombo_KodeMataUangAsing()
            'If KodeMataUang = KodeMataUang_IDR Or KodeMataUang = Kosongan Then cmb_KodeMataUang.SelectedValue = KodeMataUang_USD '(Default :USD)
            lbl_KodeMataUang.Visibility = Visibility.Visible
            cmb_KodeMataUang.Visibility = Visibility.Visible
            lbl_Kurs.Visibility = Visibility.Visible
            txt_Kurs.Visibility = Visibility.Visible
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
            lbl_DPP.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            lbl_DPPBarang.Visibility = Visibility.Collapsed
            txt_DPPBarang.Visibility = Visibility.Collapsed
            lbl_DPPJasa.Visibility = Visibility.Collapsed
            txt_DPPJasa.Visibility = Visibility.Collapsed
            lbl_PPN.Visibility = Visibility.Collapsed
            lbl_PersenPPN.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            txt_PPN.Visibility = Visibility.Collapsed
            lbl_OngkosKirim.Visibility = Visibility.Collapsed
            txt_OngkosKirim.Visibility = Visibility.Collapsed
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            txt_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            'txt_TotalTagihan.Visibility = Visibility.Collapsed
            'Kolom-kolom Kanan - Asing :
            lbl_Insurance.Visibility = Visibility.Collapsed 'Sebetulnya kolom-kolom ini seharusnya dihapus saja. Tapi khawatir nanti dibutuhkan lagi, jadi sementara ini cukup disembunyikan saja.
            txt_Insurance.Visibility = Visibility.Collapsed 'Idem
            lbl_Freight.Visibility = Visibility.Collapsed   'Idem
            txt_Freight.Visibility = Visibility.Collapsed   'Idem
            'Lainnya :
            If InvoiceDenganPO Then VisibilitasBarisPO(True)
            VisibilitasTabelSJBAST(False)
            'Styling :
            txt_JumlahNota.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Diskon_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_UangMukaPlusTermin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Termin_Rp.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DPPBarang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DPPJasa.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DasarPengenaanPajak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_DasarPengenaanPajak_11Per12.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_PPN.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_TotalTagihan_Kotor.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_PPhTerutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_PPhDitanggung.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_PPhDipotong.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Insurance.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_TotalTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
        End If
    End Sub


    Private Sub txt_NomorPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorPO.TextChanged
        NomorPOProduk = txt_NomorPO.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_PilihPO_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihPO.Click
        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan pilih 'Supplier' terlebih dahulu.")
            btn_PilihMitra.Focus()
            Return
        End If
        win_ListPO = New wpfWin_ListPO
        win_ListPO.ResetForm()
        win_ListPO.Sisi = win_ListPO.Sisi_POPenjualan
        win_ListPO.NamaMitra_Filter = NamaCustomer
        win_ListPO.FilterMitra_Aktif = False
        win_ListPO.MetodePembayaran = MetodePembayaran
        win_ListPO.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        win_ListPO.ShowDialog()
        EksekusiLogikaAdaPPh = False
        If String.IsNullOrEmpty(win_ListPO.NomorPO_Terseleksi) Then Return
        ProsesIsiValueForm = True
        Kosongkan_TabelProduk()
        txt_NomorPO.Text = win_ListPO.NomorPO_Terseleksi
        IsiValueComboBypassTerkunci(cmb_JenisPPN, AmbilValue_JenisPPNBerdasarkanPOPenjualan(NomorPOProduk))
        IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, AmbilValue_PerlakuanPPNBerdasarkanPOPenjualan(NomorPOProduk))
        JenisProduk_Induk = AmbilValue_JenisProdukIndukBerdasarkanPOPenjualan(NomorPOProduk)
        LogikaTampilanKolomDPPBarangDanJasa()
        IsiTabelProduk_BerdasarkanPO()
        If PenjualanLokal Then txt_JumlahNota.Text = JumlahNota
        If PenjualanEkspor Then txt_JumlahNota.Text = JumlahNota_Asing
        'PesanUntukProgrammer("Metode Pembayaran : " & MetodePembayaran & Enter2Baris &
        '                     "Tahap Termin : " & TahapTermin)
        If MetodePembayaran = MetodePembayaran_Termin Then
            LogikaTermin()
            If Not POMasihTersedia Then Return
        End If
        LogikaTarifPPN()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO = '" & NomorPOProduk & "' ", KoneksiDatabaseTransaksi)
        dr_Read()
        If dr.HasRows Then
            IsiValueComboBypassTerkunci(cmb_JenisPPh, dr.Item("Jenis_PPh"))
            cmb_KodeMataUang.SelectedValue = dr.Item("Kode_Mata_Uang")
            'Awal Coding untuk logika baru PPN : -----------------------------------------------
            txt_Termin_Persen.Text = Termin_Persen
            If PenjualanLokal Then txt_Termin_Rp.Text = Termin_Rp
            If PenjualanEkspor Then txt_Termin_Rp.Text = Termin_Asing
            txt_DPPBarang.Text = dr.Item("DPP_Barang")
            txt_DPPJasa.Text = dr.Item("DPP_Jasa")
            txt_DasarPengenaanPajak.Text = dr.Item("Dasar_Pengenaan_Pajak")
            txt_PPN.Text = dr.Item("PPN")
            txt_TotalTagihan_Kotor.Text = DPP + PPN
            txt_TarifPPN.Text = PembulatanDesimal2Digit((100 * PPN) / DPP)
            txt_TarifPPh.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPh"))
            txt_PPhTerutang.Text = dr.Item("PPh_Terutang") * Termin_Persen / 100
            txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung") * Termin_Persen / 100
            txt_PPhDipotong.Text = dr.Item("PPh_Dipotong") * Termin_Persen / 100
            txt_OngkosKirim.Text = dr.Item("Biaya_Transportasi")
        End If
        AksesDatabase_Transaksi(Tutup)
        ProsesIsiValueForm = False
        EksekusiLogikaAdaPPh = True
        LogikaAdaPPh(True)
        'Perhitungan()
        If PerusahaanSebagaiPKP And PenjualanLokal And TarifPPN < 10 Then
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        End If
        SembunyikanElemenPPN_UntukPenjualanEkspor()
        If MetodePembayaran = MetodePembayaran_Termin Then
            If TahapTermin = TahapTermin_UangMuka Then
                VisibilitasTabelSJBAST(False)
                Kosongkan_TabelSJBAST()
            Else
                VisibilitasTabelSJBAST(True)
                TambahSJBAST_Manual()
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
                    KosongkanItemCombo(cmb_PerlakuanPPN)
                    Kosongkan_TabelProduk()
                    Return
                End If
            End If
        End If
    End Sub
    Sub IsiTabelProduk_BerdasarkanPO()
        Dim TanggalDiterimaSJBAST
        Dim JenisProdukPerItem
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
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
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
            DiskonPerItem_Persen = dr.Item("Diskon_Per_Item")
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = JumlahHargaPerItem - DiskonPerItem_Rp
            JumlahNota += TotalHargaPerItem
            KodeProject = dr.Item("Kode_Project_Produk")
            'Asing :
            HargaSatuan_Asing = dr.Item("Harga_Satuan_Asing")
            JumlahHargaPerItem_Asing = JumlahProduk * HargaSatuan_Asing
            DiskonPeritem_Asing = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem_Asing = JumlahHargaPerItem_Asing - DiskonPeritem_Asing
            JumlahNota_Asing += TotalHargaPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPO, NamaProduk, DeskripsiProduk,
                JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                (PembulatanDesimal2Digit(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, DiskonPeritem_Asing, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProject)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub
    Dim POMasihTersedia As Boolean
    Sub LogikaTermin()
        POMasihTersedia = True
        If Not MetodePembayaran = MetodePembayaran_Termin Then Return
        Dim JumlahTermin As Integer = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPOProduk & "' ", KoneksiDatabaseTransaksi)
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
            If PenjualanLokal Then
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
        If PenjualanLokal Then
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
                If PenjualanLokal Then Termin_Persen = (Termin_Rp / JumlahNota) * 100
                If PenjualanEkspor Then Termin_Persen = (Termin_Asing / JumlahNota_Asing) * 100
        End Select
        VisibilitasBarisTermin(True)
    End Sub

    Function CekInvoice(TahapTermin)
        Dim InvoiceSudahAda = False
        BukaDatabaseTransaksi_Kondisional()
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
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
        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            lbl_DPPBarang.Visibility = Visibility.Visible
            lbl_DPPJasa.Visibility = Visibility.Visible
            txt_DPPBarang.Visibility = Visibility.Visible
            txt_DPPJasa.Visibility = Visibility.Visible
        Else
            lbl_DPPBarang.Visibility = Visibility.Collapsed
            lbl_DPPJasa.Visibility = Visibility.Collapsed
            txt_DPPBarang.Visibility = Visibility.Collapsed
            txt_DPPJasa.Visibility = Visibility.Collapsed
        End If
    End Sub


    Private Sub cmb_JenisPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPN.SelectionChanged
        JenisPPN = cmb_JenisPPN.SelectedValue
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
        LogikaAdaPPh(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub cmb_PerlakuanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PerlakuanPPN.SelectionChanged
        PerlakuanPPN = cmb_PerlakuanPPN.SelectedValue
        LogikaAdaPPh(True)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL And dtp_TanggalInvoice.Text = Kosongan Then
            cmb_KodeMataUang.SelectedValue = Kosongan
            If Not (ProsesLoadingForm Or ProsesResetForm) Then PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalInvoice, "Tanggal Invoice")
            Return
        End If
        KodeMataUang = cmb_KodeMataUang.SelectedValue
        PenentuanKurs()
        If DestinasiPenjualan = DestinasiPenjualan_Ekspor Then
            Harga_Satuan_Asing.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item_Asing.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Asing.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga_Asing.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    'Private Sub txt_KodeMataUang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeMataUang.TextChanged
    '    KodeMataUang = txt_KodeMataUang.Text
    '    LogikaDestinasiPenjualan()
    'End Sub
    'Private Sub txt_KodeMataUang_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KodeMataUang.PreviewTextInput
    '    Dim regex As New Regex("^[a-zA-Z]+$")
    '    If Not regex.IsMatch(e.Text) Then
    '        e.Handled = True ' Mencegah input yang tidak sesuai
    '    End If
    'End Sub
    'Private Sub txt_KodeMataUang_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_KodeMataUang.PreviewKeyDown
    '    If e.Key = Key.Space Then
    '        e.Handled = True ' Mencegah input spasi
    '    End If
    'End Sub


    Private Sub txt_Kurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub rdb_JumlahHariJatuhTempo_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_JumlahHariJatuhTempo.Checked
        If rdb_JumlahHariJatuhTempo.IsChecked = True Then
            txt_JumlahHariJatuhTempo.IsEnabled = True
            txt_JumlahHariJatuhTempo.Focus()
            lbl_JumlahHariJatuhTempo.IsEnabled = True
            dtp_TanggalJatuhTempo.Text = Kosongan
            dtp_TanggalJatuhTempo.IsEnabled = False
            LogikaJenisPenjualan()
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
    End Sub


    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.Text <> Kosongan And dtp_TanggalInvoice.Text <> Kosongan Then
            KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(dtp_TanggalJatuhTempo,
                                                               dtp_TanggalInvoice.SelectedDate.Value.Day,
                                                               dtp_TanggalInvoice.SelectedDate.Value.Month,
                                                               dtp_TanggalInvoice.SelectedDate.Value.Year)
            TanggalJatuhTempo = dtp_TanggalJatuhTempo.SelectedDate
            KondisiFormSetelahPerubahan()
            LogikaJenisPenjualan()
        End If
    End Sub


    Sub LogikaJenisPenjualan()
        If dtp_TanggalInvoice.Text = Kosongan Then
            If NP = "N" Then KondisiForm_JenisPenjualanTempo()
        Else
            If dtp_TanggalJatuhTempo.SelectedDate = dtp_TanggalInvoice.SelectedDate Then '(Sengaja pakai variabel dtp_****..! Jangan diganti...!!!)
                If NP = "N" Then KondisiForm_JenisPenjualanTunai()
            Else
                If NP = "N" Then KondisiForm_JenisPenjualanTempo()
            End If
        End If
    End Sub
    Sub KondisiForm_JenisPenjualanTunai()
        JenisPenjualan = JenisPenjualan_Tunai
        'lbl_SaranaPembayaran.Visibility = Visibility.Visible   '(Walaupun tunai, tidak munculkan, karena Sarana Pembayaran nanti adanya di Form Bukti Penerimaan)
        'cmb_SaranaPembayaran.Visibility = Visibility.Visible   '(Tapi ini tidak dihapus, karena khawatir suatu saat nanti sistemnya berubah lagi).
    End Sub
    Sub KondisiForm_JenisPenjualanTempo()
        JenisPenjualan = JenisPenjualan_Tempo
        lbl_SaranaPembayaran.Visibility = Visibility.Collapsed
        cmb_SaranaPembayaran.Visibility = Visibility.Collapsed
        Reset_grb_Bank()
        cmb_SaranaPembayaran.SelectedValue = Kosongan
        LogikaCOADebet_UntukNonTunai()
    End Sub
    Sub LogikaCOADebet_UntukNonTunai()
        If JenisJasa = JenisJasa_Dividen Or JenisJasa = JenisJasa_LabaPajakBUT Then
            COADebet = KodeTautanCOA_PiutangPemegangSaham
        Else
            LogikaCOADebet_UntukNonTunai_Sub()
        End If
        If JenisProduk_Induk = JenisProduk_Barang Then
            LogikaCOADebet_UntukNonTunai_Sub()
        End If
        If MitraLuarNegeri Then COADebet = KodeTautanCOA_PiutangUsaha_USD
    End Sub
    Sub LogikaCOADebet_UntukNonTunai_Sub()
        If MitraSebagaiAfiliasi(KodeCustomer) Then
            COADebet = KodeTautanCOA_PiutangUsaha_Afiliasi
        Else
            Select Case KodeMataUang
                Case KodeMataUang_IDR
                    COADebet = KodeTautanCOA_PiutangUsaha_NonAfiliasi
                Case KodeMataUang_USD
                    COADebet = KodeTautanCOA_PiutangUsaha_USD
                Case KodeMataUang_AUD
                    COADebet = KodeTautanCOA_PiutangUsaha_AUD
                Case KodeMataUang_JPY
                    COADebet = KodeTautanCOA_PiutangUsaha_JPY
                Case KodeMataUang_CNY
                    COADebet = KodeTautanCOA_PiutangUsaha_CNY
                Case KodeMataUang_EUR
                    COADebet = KodeTautanCOA_PiutangUsaha_EUR
                Case KodeMataUang_SGD
                    COADebet = KodeTautanCOA_PiutangUsaha_SGD
                Case KodeMataUang_GBP
                    COADebet = KodeTautanCOA_PiutangUsaha_GBP
            End Select
        End If
    End Sub

    Private Sub txt_Referensi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Referensi.TextChanged
        Referensi = txt_Referensi.Text
    End Sub


    Private Sub cmb_SaranaPembayaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        SaranaPembayaran = cmb_SaranaPembayaran.SelectedValue
        COADebet = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If COATermasukBank(COADebet) Then
            grb_Bank.Visibility = Visibility.Visible
            PembayaranViaBank = True
            KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
            Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
        KondisiFormSetelahPerubahan()
    End Sub



    Dim SaranaPembayaran As String
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


    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = Ambilangka_MultiCurrency(LokasiWP, txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.IsEnabled = False
            cmb_DitanggungOleh.SelectedValue = Kosongan
        Else
            cmb_DitanggungOleh.IsEnabled = True
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
        KondisiFormSetelahPerubahan()
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

    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_IN, TotalTagihan, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
    End Sub

    Private Sub btn_TambahSJBAST_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahSJBAST.Click

        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan pilih 'Customer' terlebih dahulu.")
            btn_PilihMitra.Focus()
            Return
        End If

        If MetodePembayaran = MetodePembayaran_Normal Then
            If InvoiceDenganPO Then
                TambahSJBAST_FromList()
            Else
                TambahSJBAST_Manual()
            End If
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            TambahSJBAST_Manual()
        End If

        BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_TabelSJBAST()
        KondisiFormSetelahPerubahan()

    End Sub

    Sub TambahSJBAST_Manual()
        win_InputSJBASTManual = New wpfWin_InputSJBASTManual
        win_InputSJBASTManual.ResetForm()
        If Not MitraLuarNegeri Then
            win_InputSJBASTManual.Label_Nomor = "Nomor SJ/BAST :"
            win_InputSJBASTManual.Label_Tanggal = "Tanggal SJ/BAST :"
            win_InputSJBASTManual.Label_TanggalDiterima = "Tanggal Diterima :"
        Else
            win_InputSJBASTManual.Label_Nomor = "Nomor BL/AWB :"
            win_InputSJBASTManual.Label_Tanggal = "Tanggal BL/AWB :"
            win_InputSJBASTManual.Label_TanggalDiterima = "Tanggal Fiat Muat :"
        End If
        win_InputSJBASTManual.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        win_InputSJBASTManual.TanggalDiterima = TanggalInvoice
        win_InputSJBASTManual.TanggalSJBAST = TanggalInvoice
        win_InputSJBASTManual.ShowDialog()
        BersihkanSeleksi_TabelSJBAST()
    End Sub

    Sub TambahSJBAST_FromList()

        PosisiBug1()

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
        If JumlahBarisSJBAST > 0 And
            (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi) Then
            'Untuk Saat ini, hanya produk 'Barang dan Jasa' dan 'Jasa Kontstruksi' yang menggunakan filter ini.
            'Kemungkinannya, nanti semua jenis produk harus difilter seperti ini.
            'Nunggu kepastian dari pa Aris dulu.
            'Kalau sudah OK bahwa semuanya harus difilter seperti ini, maka Logikanya cukup 'Jumlah BAST > 0' saja.
            win_ListSJBAST.NomorPO_HarusSama = True
            win_ListSJBAST.NomorPO_YangHarusDisamakan = NomorPO_TerakhirDitambahkan
        End If
        For Each row As DataRow In datatabelSJBAST.Rows
            win_ListSJBAST.ListNomorSJBAST_Singkirkan.Add(row("Nomor_SJ_BAST").ToString())
        Next
        win_ListSJBAST.ShowDialog()                                             '<---- Buka Form List SJ/BAST
        EksekusiLogikaAdaPPh = False
        NomorSJBAST = win_ListSJBAST.NomorSJBAST_Terseleksi
        If NomorSJBAST = Kosongan Then Return
        NomorSJBAST_TerakhirDitambahkan = NomorSJBAST
        NomorPO_TerakhirDitambahkan = win_ListSJBAST.NomorPO_Terseleksi
        TanggalSJBAST = win_ListSJBAST.TanggalSJBAST_Terseleksi
        TanggalDiterimaSJBAST = win_ListSJBAST.TanggalDiterima_Terseleksi
        Dim TelusurSJBAST = Kosongan
        NomorPOProduk = Kosongan
        'Cegah Input SJBAST lebih dari satu kali :
        For Each row As DataRow In datatabelSJBAST.Rows
            TelusurSJBAST = row("Nomor_SJ_BAST")
            If TelusurSJBAST = NomorSJBAST Then
                PesanPeringatan("Nomor SJ/BAST ini sudah diinput..!")
                Return
            End If
        Next
        If JumlahBarisSJBAST > 0 Then
            If win_ListSJBAST.NomorPO_Terseleksi <> datatabelSJBAST.Rows(0)("Nomor_PO").ToString() Then
                Pesan_Peringatan("Nomor Surat Jalan / BAST yang berbeda PO tidak dapat ditambahkan ke dalam daftar..!")
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                         " WHERE Nomor_PO = '" & NomorPOProduk & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            JenisProdukPerItem = drTELUSUR.Item("Jenis_Produk_Per_Item")
            KodeProjectProduk = drTELUSUR.Item("Kode_Project_Produk")
            cmb_KodeMataUang.SelectedValue = drTELUSUR.Item("Kode_Mata_Uang")
            HargaSatuan = drTELUSUR.Item("Harga_Satuan")
            HargaSatuan_Asing = drTELUSUR.Item("Harga_Satuan_Asing")
            Dim JumlahHargaPerItem As Int64 = JumlahProduk_PerItem * HargaSatuan
            Dim JumlahHargaPerItem_Asing As Decimal = JumlahProduk_PerItem * HargaSatuan_Asing
            DiskonPerItem_Persen = PembulatanDesimal2Digit(drTELUSUR.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            Dim DiskonPerItem_Asing As Decimal = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            Dim TotalHargaPerItem As Int64 = JumlahHargaPerItem - DiskonPerItem_Rp
            Dim TotalHargaPerItem_Asing As Decimal = JumlahHargaPerItem_Asing - DiskonPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPOProduk, NamaProduk, DeskripsiProduk,
                                    JumlahProduk_PerItem, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                                    DiskonPerItem_Persen, DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProjectProduk)
            DPPJasa_BerdasarkanPO = drTELUSUR.Item("DPP_Jasa")
            TarifPPNManual = PembulatanDesimal2Digit((100 * drTELUSUR.Item("PPN")) / drTELUSUR.Item("Dasar_Pengenaan_Pajak"))
            JenisPPh = drTELUSUR.Item("Jenis_PPh")
            TarifPPh = PembulatanDesimal2Digit(drTELUSUR.Item("Tarif_PPh"))
            PPhTerutang = drTELUSUR.Item("PPh_Terutang")
            PPhDitanggung_BerdasarkanPO = drTELUSUR.Item("PPh_Ditanggung")
            If TarifPPh > 0 Then AdaPPh = True
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
        Title = JudulForm
        IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN)
        IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, PerlakuanPPN)
        IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh)
        If JenisProdukPerItem <> JenisProduk_Barang Then
            txt_TarifPPh.Text = PembulatanDesimal2Digit(TarifPPh)
        End If
        'Untuk Value PPh Ditanggung, ada di Sub Perhitungan. Jangan ditempatkan di sini.
        'Untuk value Biaya Transportasi Penjualan, sudah ada di Sub Perhitungan tersendiri.
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
        Dim BarisUntukDihapus As New List(Of DataGridRow)
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
        If Not InvoiceDenganPO Then KosongkanKolomPerhitungan()
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
            If MetodePembayaran = MetodePembayaran_Termin Or Not InvoiceDenganPO Then
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
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        DeskripsiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Deskripsi_Produk")
        JumlahProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk"))
        SatuanProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Satuan_Produk")
        HargaSatuan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan"))
        DiskonPerItem_Persen_Terseleksi = Replace(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Rp"))
        KodeProjectProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project_Produk")
        TotalHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga"))
        'Asing :
        HargaSatuan_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan_Asing"))
        JumlahHarga_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga_Per_Item_Asing"))
        DiskonPerItem_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Asing"))
        TotalHarga_Asing_Terseleksi = AmbilAngka_Asing(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga_Asing"))

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


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If PenjualanLokal Then
                If JumlahBarisSJBAST = 0 And Not InvoiceDenganPO Then
                    PesanPeringatan("Silakan isi terlebih dahulu tabel Surat Jalan/BAST")
                    btn_TambahSJBAST.Focus()
                    Return
                End If
            End If
        End If
        If PerusahaanSebagaiPKP And JenisPPN = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Jenis PPN' terlebih dahulu.")
            cmb_JenisPPN.Focus()
            Return
        End If
        If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Perlakuan PPN' terlebih dahulu.")
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
        win_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        win_InputProduk_Nota.NomorSJBAST = NomorSJBAST_Aktif
        win_InputProduk_Nota.TanggalSJBAST = TanggalSJBAST_Aktif
        win_InputProduk_Nota.TanggalDiterimaSJBAST = TanggalDiterimaSJBAST_Aktif
        win_InputProduk_Nota.ShowDialog()
        If win_InputProduk_Nota.Proses = False Then Return
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
        win_InputProduk_Nota.JalurMasuk = Form_INPUTINVOICEPENJUALAN
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
        If Not TanyaKonfirmasi("Yakin akan menyingkirkan item terpilih?") Then Return
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
        If PenjualanLokal Then JumlahNota = AmbilAngka(txt_JumlahNota.Text)
        If PenjualanEkspor Then JumlahNota_Asing = AmbilAngka_Asing(txt_JumlahNota.Text)
    End Sub


    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Diskon_Persen, Diskon_Persen)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_Diskon_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Diskon_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Rp.TextChanged
        If PenjualanLokal Then Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
        If PenjualanEkspor Then DiskonAsing = AmbilAngka_Asing(txt_Diskon_Rp.Text)
    End Sub


    Private Sub txt_UangMukaPlusTermin_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMukaPlusTermin_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_UangMukaPlusTermin_Persen, UangMukaPlusTermin_Persen)
        KondisiFormSetelahPerubahan()
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_UangMukaPlusTermin_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_UangMukaPlusTermin_Persen.PreviewTextInput
    End Sub
    Private Sub txt_UangMukaPlusTermin_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMukaPlusTermin_Rp.TextChanged
        If PenjualanLokal Then UangMukaPlusTermin_Rp = AmbilAngka(txt_UangMukaPlusTermin_Rp.Text)
        If PenjualanEkspor Then UangMukaPlusTermin_Asing = AmbilAngka_Asing(txt_UangMukaPlusTermin_Rp.Text)
    End Sub


    Private Sub txt_Termin_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Termin_Persen, Termin_Persen)
        KondisiFormSetelahPerubahan()
        'If BasisPerhitunganTermin = BasisPerhitunganTermin_Prosentase Then Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_Termin_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Termin_Persen.PreviewTextInput
    End Sub
    Private Sub txt_Termin_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin_Rp.TextChanged
        If PenjualanLokal Then Termin_Rp = AmbilAngka(txt_Termin_Rp.Text)
        If PenjualanEkspor Then Termin_Asing = AmbilAngka_Asing(txt_Termin_Rp.Text)
    End Sub


    Private Sub txt_DPPBarang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPBarang.TextChanged
        DPPBarang = AmbilAngka(txt_DPPBarang.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_DPPJasa_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPJasa.TextChanged
        If PenjualanLokal Then DPPJasa = AmbilAngka(txt_DPPJasa.Text)
        'If Penjualanekspor Then DPPJasa_Asing = AmbilAngka_Asing(txt_DPPJasa.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
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
        KetersediaanTombolHitung(True)
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
        If PenjualanLokal Then TotalTagihan_Kotor = AmbilAngka(txt_TotalTagihan_Kotor.Text)
        If PenjualanEkspor Then TotalTagihan_Kotor_Asing = AmbilAngka_Asing(txt_TotalTagihan_Kotor.Text)
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


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        TextBoxFormatPersen_WPF(txt_TarifPPh, TarifPPh)
        If TarifPPh > 100 Then
            Pesan_Peringatan("Silakan isi kolom 'Diskon' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub
    Private Sub txt_TarifPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPh.PreviewTextInput
    End Sub


    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        EksekusiKodeLogikaPPN = False
        If PenjualanLokal Then PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        'PerhitunganFinal()
        If PPhDipotong < 0 Then
            txt_PPhDitanggung.Text = 0
            txt_PPhDitanggung.Focus()
            Pesan_Peringatan("Silakan isi kolom 'PPh Ditanggung' dengan benar!")
            Return
        End If
        KetersediaanTombolHitung(True)
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        If PenjualanLokal Then PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
    End Sub


    Private Sub txt_OngkosKirim_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_OngkosKirim.TextChanged
        EksekusiKodeLogikaPPN = False
        If PenjualanLokal Then OngkosKirim = AmbilAngka(txt_OngkosKirim.Text)
        KetersediaanTombolHitung(True)
        'PerhitunganFinal()
    End Sub



    Private Sub txt_Insurance_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Insurance.TextChanged
        If PenjualanEkspor Then Insurance = AmbilAngka_Asing(txt_Insurance.Text)
        EksekusiKodeLogikaPPN = False
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub txt_Freight_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Freight.TextChanged
        If PenjualanEkspor Then Freight = AmbilAngka_Asing(txt_Freight.Text)
        EksekusiKodeLogikaPPN = False
        KetersediaanTombolHitung(True)
        'Perhitungan() 'Dinonaktifkan - Konsep On-Demand Calculation (Gunakan tombol Hitung)
    End Sub


    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan.TextChanged
        If PenjualanLokal Then TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
        If PenjualanEkspor Then TotalTagihan_Asing = AmbilAngka_Asing(txt_TotalTagihan.Text)
    End Sub


    Private Sub btn_Hitung_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hitung.Click
        Perhitungan()
        KetersediaanTombolHitung(False)
        KetersediaanTombolSimpan(True)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'PesanUntukProgrammer("Total Tagihan : " & TotalTagihan & Enter2Baris &
        '                     "Total Tagihan Kotor : " & TotalTagihan_Kotor & Enter2Baris &
        '                     "Jumlah Piutang Usaha : " & JumlahPiutangUsaha & Enter2Baris & Enter2Baris &
        '                     "DPP - Asing : " & DPPAsing & Enter2Baris &
        '                     "Total Tagihan - Asing : " & TotalTagihan_Asing & Enter2Baris &
        '                     "Total Tagihan Kotor - Asing : " & TotalTagihan_Kotor_Asing & Enter2Baris &
        '                     "Jumlah Piutang Usaha - Asing : " & JumlahPiutangUsaha_Asing)

        If dtp_TanggalInvoice.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal Invoice'.")
            dtp_TanggalInvoice.Focus()
            Return
        End If

        If txt_NomorInvoice.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Invoice'.")
            txt_NomorInvoice.Focus()
            Return
        End If

        If NP = "N" Then
            TanggalInvoice = dtp_TanggalInvoice.SelectedDate
            TanggalPembetulan = TanggalInvoice
        Else
            TanggalPembetulan = dtp_TanggalInvoice.SelectedDate
        End If
        TanggalLapor = TanggalKosong

        If NP = Kosongan Then
            PesanUntukProgrammer("Ada masalah...! Value NP = 'Kosongan'...!" & Enter2Baris & "Perbaiki Coding-nya...!!!")
            Return
        End If

        If datagridSJBAST.Visibility = Visibility.Visible Then
            If JumlahBarisSJBAST = 0 And InvoiceDenganPO Then
                Pesan_Peringatan("Silakan input 'Surat Jalan / BAST'.")
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

        If JenisPPN = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Jenis PPN'.")
            cmb_JenisPPN.Focus()
            Return
        End If

        If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Perlakuan PPN'.")
            cmb_PerlakuanPPN.Focus()
            Return
        End If

        If KodeMataUang = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_KodeMataUang, "Kode Mata Uang")
            Return
        End If

        If Kurs = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_Kurs, "Kurs")
            Return
        End If

        If JumlahProduk = 0 Then
            Pesan_Peringatan("Silakan tambahkan data 'Barang/Jasa'.")
            btn_Tambahkan.Focus()
            Return
        End If

        If AdaPPh Or cmb_JenisPPh.Visibility = Visibility.Visible Then
            If cmb_JenisPPh.Text = Kosongan Then
                Pesan_Peringatan("Silakan pilih 'Jenis PPh'.")
                cmb_JenisPPh.Focus()
                Return
            End If
            If TarifPPh = 0 Then
                Pesan_Peringatan("Silakan isi 'Tarif PPh'.")
                txt_TarifPPh.Focus()
                Return
            End If
        Else
            JenisPPh = JenisPPh_NonPPh
        End If

        If JenisPenjualan = JenisPenjualan_Tempo Then LogikaCOADebet_UntukNonTunai() '(Pengisian Ulang Value COADebet, untuk antisipasi)

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            If Not InvoiceDenganPO Then

                'If JualAsset = False And KodeProjectProduk = Kosongan Then
                '    MsgBox("Silakan isi kolom 'Kode Project'.")
                '    Return
                'End If

            End If

        End If

        AdaPenyimpananjurnal = False '(Harus Dibikin False dulu)

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN Then
                If JenisPPN = JenisPPN_NonPPN Then
                    If Not MitraLuarNegeri Then AdaPenyimpananjurnal = True
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

        If KodeCustomer = Nothing Then
            Pesan_Peringatan("silakan isi data 'Customer'.")
            Return
        End If

        If rdb_JumlahHariJatuhTempo.IsChecked = False And rdb_TanggalJatuhTempo.IsChecked = False Then
            Pesan_Peringatan("Silakan isi kolom 'Jatuh Tempo'.")
            rdb_JumlahHariJatuhTempo.IsChecked = True
            Return
        End If

        If rdb_JumlahHariJatuhTempo.IsChecked = True Then
            If JumlahHariJatuhTempo = 0 Then
                Pesan_Peringatan("Silakan isi kolom 'Jumlah Hari'.")
                txt_JumlahHariJatuhTempo.Focus()
                Return
            End If
            TanggalJatuhTempo = TanggalKosong
        End If

        If rdb_TanggalJatuhTempo.IsChecked = True Then
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

        If JualAsset = True Then Asset = 1
        If JualAsset = False Then Asset = 0

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If (FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_PEMBETULAN) _
            And AdaPenyimpananjurnal = True _
            Then
            If Not KodeMataUang = KodeMataUang_IDR Then JurnalAdjusment_Forex(COADebet, TanggalInvoice)
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        Dim SudahAdaJurnal As Boolean = False

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
            If NomorJV > 0 Then SudahAdaJurnal = True

            AksesDatabase_Transaksi(Tutup)

        End If

        If DestinasiPenjualan = AsalPembelian_Lokal Then

            If JenisProduk_Induk = JenisProduk_BarangDanJasa Then

                If DPPJasa = 0 Then JenisProduk_Induk = JenisProduk_Barang
                If DPPBarang = 0 Then JenisProduk_Induk = JenisProduk_Jasa

            End If

        End If

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_Invoice")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Dim HargaSatuan_Simpan As Decimal
            Dim JumlahHargaKeseluruhan_Simpan As Decimal
            Dim TotalHargaPerItem_Simpan As Decimal
            Dim Diskon_Simpan As Decimal
            Dim TotalTagihan_Kotor_Simpan As Decimal
            Dim TotalTagihan_Simpan As Decimal
            Dim JumlahPiutangUsaha_Simpan As Decimal
            Dim BiayaAdministrasiBank_Simpan As Decimal
            Dim ReturDPP_Simpan As Decimal
            Dim ReturPPN_Simpan As Decimal

            Dim Termin_Simpan As Decimal = 0 'Harus pakai nol
            Select Case BasisPerhitunganTermin
                Case BasisPerhitunganTermin_Prosentase
                    Termin_Simpan = Termin_Persen
                Case BasisPerhitunganTermin_Nominal
                    If PenjualanLokal Then Termin_Simpan = Termin_Rp
                    If PenjualanEkspor Then Termin_Simpan = Termin_Asing
            End Select

            For Each row As DataRow In datatabelUtama.Rows 'Awal Loop ========================================================

                NomorJV = 0 'Karena sementara ini tidak ada penjurnalan di sini, maka NomorJV di-nol-kan sejak di sini. Nanti baris ini bisa dihapus kalau ada kebijakan ada penjurnalan di sini lagi.

                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = row("Jenis_Produk_Per_Item")
                NomorSJBAST = row("Nomor_SJ_BAST_Produk")
                TanggalSJBAST = row("Tanggal_SJ_BAST_Produk")
                TanggalDiterimaSJBAST = row("Tanggal_Diterima_SJ_BAST_Produk")
                KonversiDBNullJadiTanggalKosong(TanggalSJBAST)
                KonversiDBNullJadiTanggalKosong(TanggalDiterimaSJBAST)
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
                DiskonPerItem_Persen = Replace(row("Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHargaPerItem = AmbilAngka(row("Total_Harga"))
                TotalHargaPerItem_Asing = AmbilAngka_Asing(row("Total_Harga_Asing"))
                KodeProjectProduk = row("Kode_Project_Produk")
                If PenjualanLokal Then
                    HargaSatuan_Simpan = HargaSatuan
                    TotalHargaPerItem_Simpan = TotalHargaPerItem
                    JumlahHargaKeseluruhan_Simpan = JumlahHargaKeseluruhan
                    Diskon_Simpan = Diskon_Rp
                    TotalTagihan_Kotor_Simpan = TotalTagihan_Kotor
                    TotalTagihan_Simpan = TotalTagihan
                    JumlahPiutangUsaha_Simpan = JumlahPiutangUsaha
                    BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
                    ReturDPP_Simpan = ReturDPP
                    ReturPPN_Simpan = ReturPPN
                Else
                    HargaSatuan_Simpan = HargaSatuan_Asing
                    TotalHargaPerItem_Simpan = TotalHargaPerItem_Asing
                    JumlahHargaKeseluruhan_Simpan = JumlahHargaKeseluruhan_Asing
                    Diskon_Simpan = DiskonAsing
                    TotalTagihan_Kotor_Simpan = TotalTagihan_Kotor_Asing
                    TotalTagihan_Simpan = TotalTagihan_Asing
                    JumlahPiutangUsaha_Simpan = JumlahPiutangUsaha_Asing
                    BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
                    ReturDPP_Simpan = 0
                    ReturPPN_Simpan = 0
                End If
                If MetodePembayaran = MetodePembayaran_Normal Then Termin_Simpan = 100 '(Kenapa pakai angka 100? Karena tanpa termin itu berarti pembayaran full 100%)
                QueryPenyimpanan = " INSERT INTO tbl_Penjualan_Invoice VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaInvoice & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & JenisInvoice & "', " &
                    " '" & MetodePembayaran & "', " &
                    " '" & BasisPerhitunganTermin & "', " &
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
                    " '" & KodeMataUang & "', " &
                    " '" & DesimalFormatSimpan(Kurs) & "', " &
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
                    " '" & DesimalFormatSimpan(HargaSatuan_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & DesimalFormatSimpan(TotalHargaPerItem_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(JumlahHargaKeseluruhan_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Diskon_Persen) & "', " &
                    " '" & TahapTermin & "', " &
                    " '" & DesimalFormatSimpan(Termin_Simpan) & "', " &
                    " '" & DPPBarang & "', " &
                    " '" & DPPJasa & "', " &
                    " '" & DPP & "', " &
                    " '" & NomorFakturPajak & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
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
                    " '" & JumlahPiutangUsaha_Simpan & "', " &
                    " '" & JenisPenjualan & "', " &
                    " '" & COADebet & "', " &
                    " '" & SaranaPembayaran & "', " &
                    " '" & DesimalFormatSimpan(BiayaAdministrasiBank_Simpan) & "', " &
                    " '" & DitanggungOleh & "', " &
                    " '" & OngkosKirim & "', " &
                    " '" & DesimalFormatSimpan(Insurance) & "', " &
                    " '" & DesimalFormatSimpan(Freight) & "', " &
                    " '" & DesimalFormatSimpan(ReturDPP_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(ReturPPN_Simpan) & "', " &
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
                If StatusKoneksiDatabase = False Then Exit For

            Next

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

        'Untuk sementara tidak ada penjurnalan di sini.
        'Logikanya terlalu rumit.
        'Jadi Jurnalnya nanti harus diposting ulang saja.
        '====================================================================================
        '   PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                       |
        '====================================================================================

        'If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0


        'If StatusSuntingDatabase = True _
        '    And JenisTahunBuku = JenisTahunBuku_NORMAL _
        '    And AdaPenyimpananjurnal = True _
        '    Then

        '    ResetValueJurnal()
        '    jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
        '    If JualAsset Then
        '        jur_JenisJurnal = JenisJurnal_Asset
        '    Else
        '        If MitraLuarNegeri Then
        '            jur_JenisJurnal = JenisJurnal_PenjualanEkspor
        '        Else
        '            jur_JenisJurnal = JenisJurnal_Penjualan
        '        End If
        '    End If
        '    jur_KodeDokumen = Kosongan
        '    jur_NomorPO = Kosongan
        '    jur_KodeProject = Kosongan
        '    jur_Referensi = Kosongan
        '    jur_TanggalInvoice = TanggalFormatTampilan(TanggalInvoice) 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
        '    jur_NomorInvoice = NomorInvoice
        '    jur_NamaProduk = AmbilValue_ListProdukBerdasarkanInvoicePenjualan(NomorInvoice)
        '    jur_NomorFakturPajak = NomorFakturPajak
        '    jur_KodeLawanTransaksi = KodeCustomer
        '    jur_NamaLawanTransaksi = NamaCustomer
        '    jur_UraianTransaksi = Catatan
        '    jur_Direct = 0

        '    AksesDatabase_General(Buka)
        '    cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
        '                          " WHERE Kode_Closing = '" & NomorPenjualan & "' ",
        '                          KoneksiDatabaseGeneral)
        '    dr_ExecuteReader()
        '    dr.Read()
        '    If dr.HasRows Then
        '        KelompokHarta = KonversiAngkaKeKelompokHarta(dr.Item("Kelompok_Harta"))
        '    End If
        '    AksesDatabase_General(Tutup)

        '    If JualAsset Then
        '        If KelompokHarta = KelompokHarta_Tanah Then
        '            COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetTanahBangunan
        '        Else
        '            COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetLainnya
        '        End If
        '    End If

        '    If JualAsset = False Then COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanBarang_Manufaktur

        '    If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
        '        COAJasa = KodeTautanCOA_PenjualanJasaKonstruksi
        '    Else
        '        COAJasa = KodeTautanCOA_PenjualanJasa
        '    End If

        '    If FungsiForm = FungsiForm_TAMBAH _
        '        Or FungsiForm = FungsiForm_EDIT _
        '        Then

        '        Dim BiayaDibayarDimuka As Int64 = OngkosKirim ' + Nanti biaya-biaya lainnya kalau ada...!!! Sekarang cuma 1 aja...!!!
        '        Dim TotalTagihan_MUA As Int64 = KonversiJumlahAsingKeRupiah(Kurs, TotalTagihan_Asing)
        '        Dim OngkosKirim_MUA As Int64 = KonversiJumlahAsingKeRupiah(Kurs, OngkosKirim_Asing)
        '        Dim BiayaAsuransiPenjualan_MUA As Int64 = KonversiJumlahAsingKeRupiah(Kurs, BiayaAsuransiPenjualan_Asing)
        '        Dim BiayaDibayarDimuka_MUA As Int64 = OngkosKirim_MUA + BiayaAsuransiPenjualan_MUA
        '        Dim HargaJualEkspor_MUA As Int64 = TotalTagihan_MUA - BiayaDibayarDimuka_MUA

        '        If JenisPenjualan = JenisPenjualan_Tunai Then
        '            JumlahDebet = TotalTagihan
        '            JumlahPiutangUsaha = 0
        '        Else
        '            JumlahDebet = JumlahPiutangUsaha
        '            PPhTerutang = 0
        '            PPhDipotong = 0
        '            'Penjelasan : PPh Terutang dan PPh Dipotong untuk transaksi Tempo (Non-Tunai)...
        '            '...nanti dimunculkannya saat Jurnal Pencairan Piutang secara proporsional.
        '        End If

        '        'Eliminasi Baris Jurnal Tertentu :
        '        If JualAsset = False Then PPhTerutang = 0

        '        'Simpan Jurnal :
        '        If Not MitraLuarNegeri Then
        '            ___jurDebetBankCashIN(DitanggungOleh, COADebet, JumlahDebet, JumlahTransfer, BiayaAdministrasiBank)
        '            ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang)
        '            ___jurDebet(PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh), PPhDipotong) '(PPh Prepaid)
        '            _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang)
        '            _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN)
        '            _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang)
        '            _______jurKredit(COAJasa, DPPJasa)
        '            _______jurKredit(KodeTautanCOA_BiayaDibayarDimuka, BiayaDibayarDimuka)
        '        Else
        '            ___jurDebet(KodeTautanCOA_PiutangUsaha_Ekspor, TotalTagihan_MUA)
        '            _______jurKredit(KodeTautanCOA_PenjualanEkspor, HargaJualEkspor_MUA)
        '            _______jurKredit(KodeTautanCOA_BiayaDibayarDimuka_MUA, BiayaDibayarDimuka_MUA)
        '        End If

        '    End If

        '    If FungsiForm = FungsiForm_PEMBETULAN Then

        '        'PENYESUAIAN VALUE :
        '        Dim TanggalInvoiceLama = Kosongan
        '        Dim DPPBarang_InvoiceLama
        '        Dim DPPJasa_InvoiceLama
        '        Dim PPN_InvoiceLama
        '        Dim PPhTerutang_InvoiceLama
        '        Dim OngkosKirim_InvoiceLama
        '        Dim TotalTagihanKotor_InvoiceLama
        '        Dim DPPBarang_Selisih
        '        Dim DPPJasa_Selisih
        '        Dim PPN_Selisih
        '        Dim PPhTerutang_Selisih
        '        Dim OngkosKirim_Selisih
        '        Dim TotalTagihanKotor_Selisih

        '        'Ambil Value dari Data Lama :
        '        AksesDatabase_Transaksi(Buka)
        '        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
        '                                   " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ",
        '                                   KoneksiDatabaseTransaksi)
        '        dr_ExecuteReader()
        '        DPPBarang_InvoiceLama = 0
        '        DPPJasa_InvoiceLama = 0
        '        PPN_InvoiceLama = 0
        '        PPhTerutang_InvoiceLama = 0
        '        OngkosKirim_InvoiceLama = 0
        '        TotalTagihanKotor_InvoiceLama = 0
        '        Do While dr.Read()
        '            TanggalInvoiceLama = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
        '            If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
        '                DPPBarang_InvoiceLama += dr.Item("Total_Harga_Per_Item")
        '            Else
        '                DPPJasa_InvoiceLama += dr.Item("Total_Harga_Per_Item")
        '            End If
        '            PPN_InvoiceLama = dr.Item("PPN")
        '            PPhTerutang_InvoiceLama = dr.Item("PPh_Terutang")
        '            OngkosKirim_InvoiceLama = dr.Item("Biaya_Transportasi")
        '            TotalTagihanKotor_InvoiceLama = DPPBarang_InvoiceLama + DPPJasa_InvoiceLama + PPN_InvoiceLama
        '        Loop
        '        AksesDatabase_Transaksi(Tutup)
        '        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoiceLama)

        '        'Pengisian Value Selisih :
        '        DPPBarang_Selisih = DPPBarang_InvoiceLama - DPPBarang
        '        DPPJasa_Selisih = DPPJasa_InvoiceLama - DPPJasa
        '        PPN_Selisih = PPN_InvoiceLama - PPN
        '        PPhTerutang_Selisih = PPhTerutang_InvoiceLama - PPhTerutang
        '        OngkosKirim_Selisih = OngkosKirim_InvoiceLama - OngkosKirim
        '        TotalTagihanKotor_Selisih = TotalTagihanKotor_InvoiceLama - TotalTagihan_Kotor

        '        'Jika Value Baru Lebih Besar atau Sama dengan Value Lama : (Positif atau Nol)
        '        If TotalTagihan_Kotor >= TotalTagihanKotor_InvoiceLama Then

        '            'Pembalikan Value Minus (-) Menjadi Plus (+)
        '            TotalTagihanKotor_Selisih = 0 - TotalTagihanKotor_Selisih
        '            PPhTerutang_Selisih = 0 - PPhTerutang_Selisih
        '            PPN_Selisih = 0 - PPN_Selisih
        '            DPPBarang_Selisih = 0 - DPPBarang_Selisih
        '            DPPJasa_Selisih = 0 - DPPJasa_Selisih
        '            OngkosKirim_Selisih = 0 - OngkosKirim_Selisih

        '            PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

        '            'Eliminasi Beberapa Baris Jurnal :
        '            If Not (JualAsset = True And (PPhTerutang > 0 Or JumlahPiutangUsaha = TotalTagihanKotor_InvoiceLama)) Then PPhTerutang_Selisih = 0
        '            If Not (PPN > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then PPN_Selisih = 0
        '            If Not (DPPBarang > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPBarang_Selisih = 0
        '            If Not (DPPJasa > 0 Or TotalTagihan_Kotor = TotalTagihanKotor_InvoiceLama) Then DPPJasa_Selisih = 0
        '            If Not (OngkosKirim > 0) Then OngkosKirim_Selisih = 0

        '            'Simpan Jurnal:
        '            ___jurDebet(KodeTautanCOA_PiutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)
        '            ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)
        '            _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
        '            _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
        '            _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
        '            _______jurKredit(COAJasa, DPPJasa_Selisih)
        '            _______jurKredit(KodeTautanCOA_BiayaDibayarDimuka, OngkosKirim_Selisih) '<---- Untuk lebih aman, nanti ini diganti dengan BiayaDibayarDimuka_Selisih..!!!

        '        End If

        '        'Jika Value Baru Lebih Kecil dari Value Lama : (Negatif)
        '        If TotalTagihan_Kotor < TotalTagihanKotor_InvoiceLama Then

        '            PesanUntukProgrammer("Tentukan Logika Pencairan Disini..!")

        '            'Eliminasi Beberapa Baris Jurnal :
        '            If Not (JualAsset = True) Then PPhTerutang_Selisih = 0

        '            'Simpan Jurnal :
        '            ___jurDebet(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
        '            ___jurDebet(COAJasa, DPPJasa_Selisih)
        '            ___jurDebet(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
        '            ___jurDebet(KodeTautanCOA_BiayaDibayarDimuka, OngkosKirim_Selisih) '<---- Untuk lebih aman, nanti ini diganti dengan BiayaDibayarDimuka_Selisih..!!!
        '            ___jurDebet(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
        '            _______jurKredit(KodeTautanCOA_PiutangUsaha_NonAfiliasi, TotalTagihanKotor_Selisih)
        '            _______jurKredit(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)

        '        End If

        '    End If

        '    If jur_StatusPenyimpananJurnal_PerBaris = True Then
        '        jur_StatusPenyimpananJurnal_Lengkap = True
        '    Else
        '        jur_StatusPenyimpananJurnal_Lengkap = False
        '    End If

        '    ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        'End If

        'If StatusSuntingDatabase = True Then
        '    UpdateStatusPO()
        '    RefreshTampilanBukuPenjualan()
        '    RefreshTampilanInvoicePenjualan()
        '    RefreshTampilanBukuPengawasanPiutangUsaha()
        '    RefreshTampilanSJBASTPenjualan()
        '    If AdaPenyimpananjurnal = True Then
        '        If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
        '        If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
        '    Else
        '        If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
        '        If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
        '    End If
        '    If JualAsset = True Then
        '        frm_JualAsset.NomorPenjualanAsset = NomorPenjualan
        '    Else
        '        Pilihan = MessageBox.Show("Apakah Anda ingin mencetaknya..?", "Perhatian..!", MessageBoxButton.YesNo, MessageBoxImage.Question)
        '        If Pilihan = MessageBoxResult.Yes Then Cetak(JenisFormCetak_Invoice, NomorInvoice, True, False)
        '    End If
        '    ResetForm()
        '    PenyimpananInvoicePenjualan = True
        '    Me.Close()
        'Else
        '    PenyimpananInvoicePenjualan = False
        '    If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
        '    If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        'End If

        If StatusSuntingDatabase = True Then
            Try
                UpdateStatusPO()
                UpdateTampilanTabel_SeluruhInvoicePenjualan()
                UpdateTampilanTabel_SeluruhBukuPenjualan()
                UpdateTampilanTabel_SeluruhBukuPengawasanPiutangUsaha()
                RefreshTampilanSJBASTPenjualan()
            Catch ex As Exception
                WriteException(ex, "RefreshTampilanSetelahSimpanInvoicePenjualan")
            End Try
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then
                pesan_DataTerpilihBerhasilDiperbarui()
                If SudahAdaJurnal Then
                    PesanPemberitahuan("Jurnal terkait Invoice ini telah dihapus." & Enter2Baris &
                                       "Silakan posting ulang di menu Buku Penjualan.")
                End If
            End If
            If JualAsset = True Then
                win_JualAsset.NomorPenjualanAsset = NomorPenjualan
            Else
                If TanyaKonfirmasi("Apakah Anda ingin mencetaknya?") Then Cetak(JenisFormCetak_Invoice, NomorInvoice, True, False)
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

    Sub UpdateTampilanTabel_SeluruhInvoicePenjualan()

        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_DenganPO_Lokal_Rutin)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_DenganPO_Lokal_Termin)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_DenganPO_Ekspor_Rutin)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_DenganPO_Ekspor_Termin)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_TanpaPO_Lokal_Barang)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_TanpaPO_Lokal_Jasa)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_TanpaPO_Ekspor)
        UpdateTampilanTabel_PerUscInvoicePenjualan(usc_InvoicePenjualan_Asset)

    End Sub

    Sub UpdateTampilanTabel_PerUscInvoicePenjualan(Usc As wpfUsc_InvoicePenjualan)
        If Not Usc.StatusAktif Then Return
        Dim TanggalJatuhTempo_Kirim As String = TanggalFormatTampilan(TanggalJatuhTempo)
        If TanggalJatuhTempo = TanggalKosong Then TanggalJatuhTempo_Kirim = JumlahHariJatuhTempo & " hari"
        Dim TanggalPembetulan_Kirim As String = TanggalFormatTampilan(TanggalPembetulan)
        If NP = "N" Then TanggalPembetulan_Kirim = StripKosong
        Usc.JenisInvoice = JenisInvoice
        Usc.JenisProduk = JenisProduk_Induk
        Usc.AngkaInvoice = AngkaInvoice
        Usc.NomorInvoice = NomorInvoice
        Usc.NomorPenjualan = NomorPenjualan
        Usc.NP = NP
        Usc.TanggalInvoice = TanggalFormatTampilan(TanggalInvoice)
        Usc.TanggalPembetulan = TanggalFormatTampilan(TanggalPembetulan)
        Usc.Tanggallapor = TanggalFormatTampilan(TanggalLapor)
        Usc.JatuhTempo = TanggalJatuhTempo_Kirim
        Usc.NomorSJBAST = NomorSJBAST
        Usc.TanggalSJBAST = TanggalFormatTampilan(TanggalSJBAST)
        Usc.NomorPO = NomorPOProduk
        Usc.TanggalPO = TanggalFormatTampilan(AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk))
        Usc.KodeProject = KodeProjectProduk
        Usc.KodeCustomer = KodeCustomer
        Usc.NamaCustomer = NamaCustomer
        Usc.KodeMataUang = KodeMataUang
        Usc.JumlahHarga = JumlahHargaKeseluruhan
        Usc.JumlahHarga_Asing = JumlahHargaKeseluruhan_Asing
        Usc.DiskonRp = Diskon_Rp
        Usc.DiskonAsing = DiskonAsing
        Usc.ProsentaseTermin_String = Termin_Persen
        Usc.DasarPengenaanPajak = DPP
        Usc.NomorFakturPajak = NomorFakturPajak
        Usc.JenisPPN = JenisPPN
        Usc.PPN = PPN
        Usc.TagihanKotor = TotalTagihan_Kotor
        Usc.TagihanKotor_Asing = TotalTagihan_Kotor_Asing
        Usc.PPhDipotong = PPhDipotong
        Usc.BiayaLainnya = OngkosKirim
        Usc.BiayaLainnya_Asing = Freight + Insurance
        Usc.TotalTagihan = TotalTagihan
        Usc.TotalTagihan_Asing = TotalTagihan_Asing
        Usc.ReturDPP = ReturDPP
        Usc.ReturPPN = ReturDPP
        Usc.Retur = ReturDPP + ReturPPN
        Usc.Catatan = PenghapusEnter(Catatan)
        Usc.NomorJV = NomorJV
        If FungsiForm = FungsiForm_TAMBAH Then Usc.TambahBaris()
        If FungsiForm = FungsiForm_EDIT Then
            Usc.NomorInvoiceLama = NomorInvoiceLama
            Usc.UpdateBaris()
        End If
    End Sub

    Sub UpdateTampilanTabel_SeluruhBukuPenjualan()
        UpdateTampilanTabel_PerUscBukuPenjualan(usc_BukuPenjualan_Lokal)
        UpdateTampilanTabel_PerUscBukuPenjualan(usc_BukuPenjualan_Ekspor)
        UpdateTampilanTabel_PerUscBukuPenjualan(usc_BukuPenjualan_Asset)
    End Sub

    Sub UpdateTampilanTabel_PerUscBukuPenjualan(Usc As wpfUsc_BukuPenjualan)
        If Not Usc.StatusAktif Then Return
        Dim TanggalPembetulan_Kirim As String = TanggalFormatTampilan(TanggalPembetulan)
        If NP = "N" Then TanggalPembetulan_Kirim = StripKosong
        Usc.NomorPenjualan = NomorPenjualan
        Usc.JenisInvoice = JenisInvoice
        Usc.JenisProduk = JenisProduk_Induk
        Usc.AngkaInvoice = AngkaInvoice
        Usc.NomorInvoice = NomorInvoice
        Usc.NP = NP
        Usc.NomorFakturPajak = NomorFakturPajak
        Usc.TanggalInvoice = TanggalFormatTampilan(TanggalInvoice)
        Usc.TanggalPembetulan = TanggalPembetulan_Kirim
        Usc.KodeCustomer = KodeCustomer
        Usc.NamaCustomer = NamaCustomer
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
        Usc.BiayaLainnya = OngkosKirim
        Usc.BiayaLainnya_Asing = Freight + Insurance
        Usc.TagihanBruto = TotalTagihan_Kotor
        Usc.TagihanBruto_Asing = TotalTagihan_Kotor_Asing
        Usc.Retur = ReturDPP + ReturPPN
        Usc.TagihanNetto = TotalTagihan
        Usc.NomorSJBAST = NomorSJBAST
        Usc.TanggalSJBAST = TanggalFormatTampilan(TanggalSJBAST)
        Usc.NomorPO = NomorPOProduk
        Usc.TanggalPO = TanggalFormatTampilan(AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk))
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

    Sub UpdateTampilanTabel_SeluruhBukuPengawasanPiutangUsaha()
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Afiliasi)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_NonAfiliasi)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_USD)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD)
        UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(usc_BukuPengawasanPiutangUsaha_Ekspor_GBP)
    End Sub

    Sub UpdateTampilanTabel_PerUscBukuPengawasanPiutangUsaha(Usc As wpfUsc_BukuPengawasanPiutangUsaha)
        If Not Usc.StatusAktif Then Return
        Usc.JenisRelasi = JenisRelasi
        Usc.NomorBPPU = NomorPenjualan
        Usc.NomorPenjualan = NomorPenjualan
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
        Usc.TanggalPO = TanggalFormatTampilan(AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPOProduk))
        Usc.KodeProject = KodeProjectProduk
        Usc.NamaProduk = NamaProduk
        Usc.KodeCustomer = KodeCustomer
        Usc.NamaCustomer = NamaCustomer
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
        Usc.BiayaBiaya_Asing = Freight + Insurance
        Usc.Retur = ReturDPP + ReturPPN
        Usc.TagihanNetto = TotalTagihan
        Usc.JumlahPiutangUsaha = JumlahPiutangUsaha
        Usc.JumlahPiutang_Asing = JumlahPiutangUsaha_Asing
        Usc.KeteranganJatuhTempo = TanggalFormatTampilan(TanggalJatuhTempo)
        Usc.Catatan = PenghapusEnter(Catatan)
        Usc.NomorJV_Penjualan = NomorJV
        'Untuk invoice baru, pembayaran = 0 dan sisa = jumlah piutang
        If FungsiForm = FungsiForm_TAMBAH Then
            Usc.TanggalBayar_Arr = Kosongan
            Usc.JumlahBayarTagihan = 0
            Usc.JumlahBayar_Asing = 0
            Usc.SisaTagihan = TotalTagihan
            Usc.SisaPiutangUsaha = JumlahPiutangUsaha
            Usc.SisaPiutang_Asing = JumlahPiutangUsaha_Asing
            Usc.SisaPiutang_Asing_IDR = JumlahPiutangUsaha_Asing * Kurs
            Usc.LOS = los_OS
            Usc.Referensi = Kosongan
            Usc.TambahBaris()
        End If
        If FungsiForm = FungsiForm_EDIT Then
            Usc.NomorInvoiceLama = NomorInvoiceLama
            Usc.UpdateBaris()
        End If
    End Sub

    Sub UpdateStatusPO()
        If MetodePembayaran = MetodePembayaran_Termin Then
            If FungsiForm = FungsiForm_TAMBAH Then
                Dim StatusKontrolPO As String
                If TahapTermin = TahapTermin_Pelunasan Then
                    StatusKontrolPO = Status_Closed
                Else
                    StatusKontrolPO = Status_Used
                End If
                UpdateStatusKontrolPOPenjualan(txt_NomorPO.Text, StatusKontrolPO)
            End If
        Else
            If MitraLuarNegeri Then
                PesanUntukProgrammer("Update Status PO Penjualan Ekspor")
            End If
        End If
        RefreshTampilanPOPenjualan()
    End Sub




    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        'Buat_DataTabelTotal()
        Buat_DataTabelSJBAST()
        cmb_JenisInvoice.IsReadOnly = True
        txt_NomorInvoice.IsReadOnly = True
        txt_NomorPO.IsReadOnly = True
        txt_KodeCustomer.IsReadOnly = True
        txt_NamaCustomer.IsReadOnly = True
        txt_PICCustomer.IsReadOnly = True
        txt_AlamatCustomer.IsReadOnly = True
        cmb_JenisPPN.IsReadOnly = True
        cmb_PerlakuanPPN.IsReadOnly = True
        cmb_KodeMataUang.IsReadOnly = True
        cmb_SaranaPembayaran.IsReadOnly = True
        cmb_DitanggungOleh.IsReadOnly = True
        txt_TotalBank.IsReadOnly = True
        cmb_JenisPPh.IsReadOnly = True
        txt_TarifPPN.IsReadOnly = False '(Ada Tarif PPN yang tidak Normal)
        txt_TarifPPN_11Per12.IsReadOnly = True
        txt_JumlahNota.IsReadOnly = True
        txt_Diskon_Rp.IsReadOnly = True
        txt_UangMukaPlusTermin_Persen.IsReadOnly = True
        txt_UangMukaPlusTermin_Rp.IsReadOnly = True
        txt_Termin_Persen.IsReadOnly = True
        txt_Termin_Rp.IsReadOnly = True
        txt_DPPBarang.IsReadOnly = True
        txt_DPPJasa.IsReadOnly = True
        txt_DasarPengenaanPajak.IsReadOnly = True
        txt_DasarPengenaanPajak_11Per12.IsReadOnly = True
        txt_TarifPPN.IsReadOnly = False
        txt_TarifPPN_11Per12.IsReadOnly = False
        txt_PPN.IsReadOnly = True
        txt_TotalTagihan_Kotor.IsReadOnly = True
        txt_PPhTerutang.IsReadOnly = True
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

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Produk_Per_Item")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Tanggal_Diterima_SJ_BAST_Produk")
        datatabelUtama.Columns.Add("Nomor_PO_Produk")
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


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk_Per_Item, "Jenis_Produk_Per_Item", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST_Produk, "Nomor_SJ_BAST_Produk", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST_Produk, "Tanggal_SJ_BAST_Produk", "Tanggal SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Diterima_SJ_BAST_Produk, "Tanggal_Diterima_SJ_BAST_Produk", "Tanggal Diterima", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO_Produk, "Nomor_PO_Produk", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan, "Harga_Satuan", "Harga Satuan", 93, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan_Asing, "Harga_Satuan_Asing", "Harga Satuan", 93, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Per_Item, "Jumlah_Harga_Per_Item", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Per_Item_Asing, "Jumlah_Harga_Per_Item_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Persen, "Diskon_Per_Item_Persen", "Diskon" & Enter1Baris & "(%)", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Rp, "Diskon_Per_Item_Rp", "Diskon", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Asing, "Diskon_Per_Item_Asing", "Diskon", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Harga, "Total_Harga", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Harga_Asing, "Total_Harga_Asing", "Total", 111, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project_Produk, "Kode_Project_Produk", "Kode Project", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)

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
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Baris_Total, "Baris_Total", "No.", 618, FormatString, TengahTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Jumlah_Harga_Keseluruhan, "Jumlah_Harga_Keseluruhan", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Persen_Keseluruhan, "Diskon_Persen_Keseluruhan", "Diskon" & Enter1Baris & "(%)", 72, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Rp_Keseluruhan, "Diskon_Rp_Keseluruhan", "Diskon" & Enter1Baris & "(Rp)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Total_Harga_Keseluruhan, "Total_Harga_Keseluruhan", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    datatabelTotal.Rows.Add("TOTAL", 0, Kosongan, 0, 0)

    'End Sub

End Class
