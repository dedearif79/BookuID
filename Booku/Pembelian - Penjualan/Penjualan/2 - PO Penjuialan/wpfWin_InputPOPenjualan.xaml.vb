Imports System.Data.Odbc
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Threading
Imports bcomm
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Drawing.Diagrams
Imports Org.BouncyCastle.Math.EC

Public Class wpfWin_InputPOPenjualan

    Public JudulForm
    Public FungsiForm
    Public NomorID

    Public JenisProduk_Induk

    'Variabel Kolom :
    Public AngkaPO
    Dim NomorPO
    Dim NomorPO_Lama
    Dim TanggalPO
    Dim TermOfPayment
    Dim KeteranganToP
    Dim JumlahHari_JangkaWaktuPenyelesaian
    Dim Tanggal_JangkaWaktuPenyelesaian
    Dim KodeCustomer
    Dim NamaCustomer
    Dim Catatan
    Dim JumlahHargaKeseluruhan As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64
    Dim DPP As Int64
    Dim DPPBarang As Int64
    Dim DPPJasa As Int64
    Dim JenisPPN
    Dim PerlakuanPPN
    Dim KodeMataUang
    Dim TarifPPN
    Dim PPN As Int64
    Dim TotalTagihan_Kotor As Int64
    Dim JenisPPh
    Dim TarifPPh As Decimal
    Dim PPhTerutang As Int64
    Dim PPhDitanggung As Int64
    Dim PPhDipotong As Int64
    Dim BiayaTransportasiPenjualan As Int64
    Dim TotalTagihan As Int64
    Dim TotalTagihan_Asing As Decimal
    Dim Kontrol

    Dim DPP_11Per12 As Int64
    Dim TarifPPN_11Per12


    'Variabel Tabel Produk :
    Dim NomorUrutProduk
    Dim JenisProduk_PerItem
    Dim KodeProjectProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem As Integer
    Dim SatuanProduk
    Dim DiskonPerItem_Persen As Decimal
    Dim HargaSatuan As Int64
    Dim JumlahHargaPerItem As Int64
    Dim DiskonPerItem_Rp As Int64
    Dim TotalHargaPerItem As Int64

    Dim JumlahHargaKeseluruhan_Asing As Decimal
    Dim DiskonAsing As Decimal
    Dim HargaSatuan_Asing As Decimal
    Dim JumlahHargaPerItem_Asing As Decimal
    Dim DiskonPerItem_Asing As Decimal
    Dim TotalHargaPerItem_Asing As Decimal

    'Variabel Tabel Index :
    Dim NomorUrutProduk_Terseleksi
    Dim JenisProdukPerItem_Terseleksi
    Dim KodeProjectProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi

    Dim HargaSatuan_Asing_Terseleksi
    Dim JumlahHarga_Asing_Terseleksi
    Dim DiskonPerItem_Asing_Terseleksi
    Dim TotalHarga_Asing_Terseleksi

    Dim JumlahProduk

    'Variabel Tabel SJBAST - Index :
    Dim BarisSJBAST_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim JumlahSJBAST

    'Variabel Tabel Invoice - Index :
    Dim BarisInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim JumlahInvoice

    'Dim JenisPPN_Sebelumnya

    Dim AdaPPh As Boolean

    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Dim MataUang As String

    Dim MitraLuarNegeri As Boolean

    Public DestinasiPenjualan
    Dim PenjualanLokal As Boolean
    Dim PenjualanEkspor As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        'If LevelUserAktif < LevelUser_81_TimIT Then
        '    MenuDalamPerbaikan()
        '    Close()
        '    Return
        'End If

        ProsesLoadingForm = True

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
        End If

        EksekusiLogikaAdaPPh = False

        KontenCombo_JenisPPh()

        If FungsiForm = FungsiForm_TAMBAH Then
            EksekusiKodeLogikaPPN = True
            JudulForm = "Input PO Penjualan - " & JenisProduk_Induk
            AngkaPO = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_PO", "Angka_PO") + 1
            IsiValueComboBypassTerkunci(cmb_Kontrol, Status_Open)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit PO Penjualan - " & JenisProduk_Induk
            AmbilValue_AlamatCustomer(KodeCustomer)
            AksesDatabase_General(Tutup)
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                  " WHERE Angka_PO = '" & AngkaPO & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                cmb_KodeMataUang.SelectedValue = dr.Item("Kode_Mata_Uang")
                txt_TermOfPayment.Text = dr.Item("Term_Of_Payment")
                txt_KeteranganToP.Text = dr.Item("Keterangan_ToP")
                cmb_MetodePembayaran.SelectedValue = dr.Item("Metode_Pembayaran")
                cmb_BasisPerhitunganTermin.SelectedValue = dr.Item("Basis_Perhitungan_Termin")
                Select Case BasisPerhitunganTermin
                    Case BasisPerhitunganTermin_Prosentase
                        txt_UangMuka_Persen.Text = dr.Item("Uang_Muka")
                        txt_Termin1_Persen.Text = dr.Item("Termin_1")
                        txt_Termin2_Persen.Text = dr.Item("Termin_2")
                        txt_Pelunasan_Persen.Text = dr.Item("Pelunasan")
                    Case BasisPerhitunganTermin_Nominal
                        If PenjualanLokal Then
                            txt_UangMuka_Rp.Text = FormatUlangInt64(dr.Item("Uang_Muka"))
                            txt_Termin1_Rp.Text = FormatUlangInt64(dr.Item("Termin_1"))
                            txt_Termin2_Rp.Text = FormatUlangInt64(dr.Item("Termin_2"))
                            txt_Pelunasan_Rp.Text = FormatUlangInt64(dr.Item("Pelunasan"))
                        Else
                            txt_UangMuka_Asing.Text = dr.Item("Uang_Muka")
                            txt_Termin1_Asing.Text = dr.Item("Termin_1")
                            txt_Termin2_Asing.Text = dr.Item("Termin_2")
                            txt_Pelunasan_Asing.Text = dr.Item("Pelunasan")
                        End If
                End Select
                JumlahTermin = dr.Item("Jumlah_Termin")
                'Awal Coding untuk logika baru PPN : -----------------------------------------------
                txt_DPPBarang.Text = dr.Item("DPP_Barang")
                txt_DPPJasa.Text = dr.Item("DPP_Jasa")
                txt_DasarPengenaanPajak.Text = dr.Item("Dasar_Pengenaan_Pajak")
                txt_PPN.Text = dr.Item("PPN")
                txt_TotalTagihan_Kotor.Text = DPP + PPN
                txt_TarifPPN.Text = PembulatanDesimal2Digit((100 * PPN) / DPP)
                'Akhir Coding untuk logika baru PPN : ----------------------------------------------
                IsiValueComboBypassTerkunci(cmb_JenisPPh, dr.Item("Jenis_PPh"))
                txt_TarifPPh.Text = PembulatanDesimal2Digit(dr.Item("Tarif_PPh"))
                If TarifPPh > 0 Then AdaPPh = True
                txt_PPhTerutang.Text = dr.Item("PPh_Terutang") 'Ini jangan dihapus...!
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
                txt_BiayaTransportasiPenjualan.Text = dr.Item("Biaya_Transportasi")
            End If
            AksesDatabase_Transaksi(Tutup)
            NomorPO_Lama = NomorPO
        End If

        Title = JudulForm

        LogikaDestinasiPenjualan()

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

        If PenjualanEkspor Then
            Perhitungan()
            txt_TotalTagihan_Asing.Text = JumlahNota_Asing
        End If

        If FungsiForm <> FungsiForm_TAMBAH Then
            EksekusiKodeLogikaPPN = False
            If PerusahaanSebagaiPKP And PenjualanLokal And TarifPPN < 10 Then
                txt_TarifPPN.Visibility = Visibility.Visible
                txt_DasarPengenaanPajak.Visibility = Visibility.Visible
                txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
                txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            End If
            If TarifPPN >= 10 Then
                EksekusiKodeLogikaPPN = True
                LogikaTampilanPPN()
                EksekusiKodeLogikaPPN = False
            End If
        End If

        EksekusiLogikaAdaPPh = True

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        MataUang = Kosongan
        MitraLuarNegeri = False

        NomorID = 0
        AngkaPO = 0
        txt_NomorPO.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalPO)
        JenisProduk_Induk = Kosongan
        txt_TermOfPayment.Text = Kosongan
        txt_KeteranganToP.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_AlamatCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
        rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = False
        rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = False
        txt_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
        lbl_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
        txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
        KontenCombo_MetodePembayaran()
        'datagridTotal.Visibility = Visibility.Collapsed
        VisibilitasKolomTermin(False)
        KosongkanKolomUangMukaDanTermin()
        KosongkanValueElemenRichTextBox(txt_Catatan)
        KosongkanKolomPerhitungan()
        lbl_BiayaTransportasiPenjualan.Visibility = Visibility.Collapsed
        txt_BiayaTransportasiPenjualan.Visibility = Visibility.Collapsed
        txt_BiayaTransportasiPenjualan.Text = Kosongan
        VisibilitasBarisDiskon(False)
        txt_TarifPPN.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        AdaPPh = False
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()
        Kosongkan_TabelInvoice()
        KontenCombo_Kontrol()
        btn_Simpan.IsEnabled = True
        VisibilitasElemenPPh(False)

        EksekusiKodeLogikaPPN = False
        EksekusiKodeSubPerhitungan = True

        ProsesResetForm = False

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


    Sub IsiTabelProduk()
        JumlahNota = 0
        JumlahNota_Asing = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Angka_PO = '" & AngkaPO & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = 0
        Do While dr.Read
            NomorUrutProduk += 1
            JenisProduk_PerItem = dr.Item("Jenis_Produk_Per_Item")
            KodeProjectProduk = dr.Item("Kode_Project_Produk")
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
            HargaSatuan_Asing = dr.Item("Harga_Satuan_Asing")
            JumlahHargaPerItem_Asing = JumlahProduk * HargaSatuan_Asing
            DiskonPerItem_Asing = JumlahHargaPerItem_Asing * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem_Asing = JumlahHargaPerItem_Asing - DiskonPerItem_Asing
            JumlahNota += TotalHargaPerItem
            JumlahNota_Asing += TotalHargaPerItem_Asing
            datatabelUtama.Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NamaProduk, DeskripsiProduk,
                                    JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing, JumlahHargaPerItem, JumlahHargaPerItem_Asing,
                                    (PembulatanDesimal2Digit(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHargaPerItem, TotalHargaPerItem_Asing, KodeProjectProduk)
        Loop
        AksesDatabase_Transaksi(Tutup)
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
        If dtp_TanggalPO.Text = Kosongan Then Return
        If PerusahaanSebagaiPKP And Not MitraLuarNegeri Then
            txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalPO)
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
        If dtp_TanggalPO.Text = Kosongan Then Return
        LogikaPPN(dtp_TanggalPO.SelectedDate, DPP, TarifPPN, DPP_11Per12, TarifPPN_11Per12)
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

    Sub SembunyikanElemenPajak()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        KosongkanItemCombo(cmb_PerlakuanPPN)
        txt_TarifPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        lbl_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Visibility = Visibility.Visible
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Ditanggung)
        If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False And JenisPPN <> JenisPPN_NonPPN Then
            cmb_PerlakuanPPN.Text = Kosongan
            'MsgBox("Silakan pilih 'Perlakuan PPN'.")
            cmb_PerlakuanPPN.Focus()
        End If
    End Sub

    Sub KontenCombo_MetodePembayaran()
        cmb_MetodePembayaran.Items.Clear()
        cmb_MetodePembayaran.Items.Add(MetodePembayaran_Normal)
        cmb_MetodePembayaran.Items.Add(MetodePembayaran_Termin)
        cmb_MetodePembayaran.Text = Kosongan
    End Sub


    Sub KontenCombo_BasisPerhitunganTermin()
        cmb_BasisPerhitunganTermin.Items.Clear()
        cmb_BasisPerhitunganTermin.Items.Add(BasisPerhitunganTermin_Prosentase)
        cmb_BasisPerhitunganTermin.Items.Add(BasisPerhitunganTermin_Nominal)
        cmb_BasisPerhitunganTermin.Text = Kosongan
    End Sub


    Sub KontenCombo_Kontrol()
        cmb_Kontrol.Items.Clear()
        cmb_Kontrol.Items.Add(Status_Open)
        cmb_Kontrol.Items.Add(Status_Closed)
        cmb_Kontrol.SelectedValue = Status_Open 'Ini sudah benar pakai .SelectedValue. Jangan dirubah ke .Text.
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


    Dim EksekusiLogikaAdaPPh As Boolean
    Sub LogikaAdaPPh(Ada As Boolean)
        If Not EksekusiLogikaAdaPPh Then Return
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
        Perhitungan()
    End Sub


    Sub VisibilitasElemenPPh(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_PPh.Visibility = Visibility.Visible
            cmb_JenisPPh.Visibility = Visibility.Visible
            txt_TarifPPh.Visibility = Visibility.Visible
            lbl_PersenPPh.Visibility = Visibility.Visible
            txt_PPhTerutang.Visibility = Visibility.Visible
            VisibilitasPPhDitanggungDipotong(True)
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

    Sub Kosongkan_TabelProduk()
        datatabelUtama.Rows.Clear()
        JumlahProduk = 0
    End Sub

    Sub Kosongkan_TabelSJBAST()
        'dgv_SJBAST.Rows.Clear()
        'JumlahSJBAST = 0
    End Sub

    Sub Kosongkan_TabelInvoice()
        'dgv_Invoice.Rows.Clear()
        'JumlahInvoice = 0
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
        JumlahProduk = datatabelUtama.Rows.Count
    End Sub

    Sub BersihkanSeleksi_TabelSJBAST()
        'BarisSJBAST_Terseleksi = -1
        'NomorSJBAST_Terseleksi = Kosongan
        'dgv_SJBAST.ClearSelection()
        'JumlahSJBAST = dgv_SJBAST.RowCount
    End Sub

    Sub BersihkanSeleksi_TabelInvoice()
        'BarisInvoice_Terseleksi = -1
        'NomorInvoice_Terseleksi = Kosongan
        'dgv_Invoice.ClearSelection()
        'JumlahInvoice = dgv_Invoice.RowCount
    End Sub


    Sub KondisiFormSetelahPerubahan()
        BersihkanSeleksi_TabelProduk()
    End Sub


    Dim EksekusiKodeSubPerhitungan As Boolean   'Ini untuk mencegah looping
    Dim JumlahLoopPerhitunngan = 0              'Ini hanya untuk keperluan mengetahui jumlah loop pada Sub Perhitungan
    Sub Perhitungan()

        JumlahProduk = datatabelUtama.Rows.Count

        If ProsesResetForm = True _
            Or ProsesIsiValueForm = True _
            Or ProsesLoadingForm = True _
            Or EksekusiKode = False _
            Or JenisPPN = Kosongan _
            Or JumlahProduk = 0 _
            Then Return

        Dim RasioDPPBarang

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

        JumlahNota = JumlahHargaKeseluruhan - Diskon_Rp
        JumlahNota_Asing = JumlahHargaKeseluruhan_Asing - DiskonAsing

        txt_JumlahNota.Text = JumlahNota
        TotalTagihan_Asing = JumlahNota_Asing

        RasioDPPBarang = DPPBarang / HitunganHarga_Relatif

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            DPP = HitunganHarga_Relatif
            txt_Diskon_Rp.Text = Diskon_Rp
            txt_DasarPengenaanPajak.Text = HitunganHarga_Relatif
        End If

        LogikaTarifPPN()
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
                    PPN = HitungPPNInclude(TotalTagihan_Kotor, TarifPPN)
                    DPP = TotalTagihan_Kotor - PPN
                    DPPBarang = DPP * RasioDPPBarang
                    If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalTagihan_Kotor = DPP + PPN
                    If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalTagihan_Kotor = DPP
                    If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalTagihan_Kotor = DPP
                End If
                '---------------------------------------------------------
                txt_Diskon_Rp.Text = Diskon_Rp
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

        If PenjualanLokal Then
            txt_DasarPengenaanPajak.Text = DPP
            txt_PPhTerutang.Text = PPhTerutang
        End If

        PPhDipotong = PPhTerutang - PPhDitanggung

        PerhitunganFinal()

    End Sub
    Sub PerhitunganFinal()

        txt_PPhDipotong.Text = PPhDipotong
        TotalTagihan = TotalTagihan_Kotor - PPhDipotong + BiayaTransportasiPenjualan
        If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
            txt_PPN.Text = Kosongan
            txt_TotalTagihan_Kotor.Text = Kosongan
            txt_TotalTagihan.Text = Kosongan
        Else
            txt_PPN.Text = PPN
            txt_TotalTagihan_Kotor.Text = TotalTagihan_Kotor
            txt_TotalTagihan.Text = TotalTagihan
        End If
        txt_TotalTagihan_Asing.Text = TotalTagihan_Asing

        'PesanUntukProgrammer("DPP Barang : " & DPPBarang & Enter2Baris &
        '                     "DPP Jasa : " & DPPJasa & Enter2Baris &
        '                     "Jenis PPh : " & JenisPPh & Enter2Baris &
        '                     "Tarif PPh : " & TarifPPh & Enter2Baris &
        '                     "PPh Terutang : " & PPhTerutang)

        BarisTotalTabel()
        LogikaUangMukaDanTermin()

        If AdaPPh Then
            VisibilitasElemenPPh(True)
            If PPhTerutang = 0 Then VisibilitasPPhDitanggungDipotong(False) 'Ini ada kaitannya dengan Jenis Jasa : Lainnya. Jadi jangan semudah itu dirubah.
            If JenisPPh = JenisPPh_Pasal22_Lokal Then VisibilitasPPhDitanggungDipotong(False)
        Else
            VisibilitasElemenPPh(False)
        End If

        LogikaTampilanPPN()

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
        txt_TotalTagihan.Text = Kosongan
        txt_TotalTagihan_Asing.Text = Kosongan
    End Sub



    Private Sub txt_NomorPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorPO.TextChanged
        NomorPO = txt_NomorPO.Text
    End Sub
    Private Sub txt_NomorPO_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_NomorPO.LostFocus
        'If NomorPO = Kosongan Then
        '    MsgBox("Silakan isi kolom 'Nomor PO'.")
        '    txt_NomorPO.Focus()
        '    Return
        'End If
        If FungsiForm = FungsiForm_TAMBAH Then
            'Periksa/Validasi Nomor PO
            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" SELECT Nomor_PO FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    txt_NomorPO.Text = Kosongan
                    MsgBox("Nomor PO '" & NomorPO & "' sudah ada." & Enter2Baris & "Silakan input Nomor PO yang lain.")
                End If
                AksesDatabase_Transaksi(Tutup)
            End If
        End If
    End Sub


    Private Sub dtp_TanggalPO_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPO.SelectedDateChanged
        If dtp_TanggalPO.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalPO)
            TanggalPO = TanggalFormatTampilan(dtp_TanggalPO.SelectedDate)
            KondisiFormSetelahPerubahan()
        End If
    End Sub


    Private Sub txt_TermOfPayment_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TermOfPayment.TextChanged
        TermOfPayment = AmbilAngka(txt_TermOfPayment.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TermOfPayment)
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_KeteranganToP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KeteranganToP.TextChanged
        KeteranganToP = txt_KeteranganToP.Text
    End Sub


    Private Sub txt_KodeCustomer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        txt_NamaCustomer.Text = AmbilValue_NamaMitra(KodeCustomer)
        txt_PICCustomer.Text = AmbilValue_PICMitra(KodeCustomer)
        txt_AlamatCustomer.Text = AmbilValue_AlamatMitra(KodeCustomer)
        If MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then
            MitraLuarNegeri = True
        Else
            MitraLuarNegeri = False
        End If
        KontenCombo_JenisPPN()
        Kosongkan_TabelProduk()
        KosongkanKolomPerhitungan()
        LogikaAdaPPh(True)
        LogikaDestinasiPenjualan()
        VisibilitasKolomTermin(True)
    End Sub
    Private Sub btn_PilihCustomer_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        If PenjualanLokal Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, LokasiWP_DalamNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
        If PenjualanEkspor Then BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, LokasiWP_LuarNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaCustomer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaCustomer.TextChanged
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
            'Kolom-kolom Kanan :
            lbl_DPP.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            lbl_PPN.Visibility = Visibility.Visible
            lbl_PersenPPN.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
            lbl_JumlahNota.Visibility = Visibility.Visible
            txt_JumlahNota.Visibility = Visibility.Visible
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Visible
            txt_TotalTagihan_Kotor.Visibility = Visibility.Visible
            txt_TotalTagihan.Visibility = Visibility.Visible
            txt_TotalTagihan_Asing.Visibility = Visibility.Collapsed
            'Kolom-kolom Tabel :
            Harga_Satuan.Visibility = Visibility.Visible
            Jumlah_Harga_Per_Item.Visibility = Visibility.Visible
            Diskon_Per_Item_Rp.Visibility = Visibility.Visible
            Total_Harga.Visibility = Visibility.Visible
            Harga_Satuan.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Rp.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
            Harga_Satuan_Asing.Visibility = Visibility.Collapsed
            Jumlah_Harga_Per_Item_Asing.Visibility = Visibility.Collapsed
            Diskon_Per_Item_Asing.Visibility = Visibility.Collapsed
            Total_Harga_Asing.Visibility = Visibility.Collapsed
        End If
        If PenjualanEkspor Then
            MataUang = MataUang_Asing
            KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang)
            'If KodeMataUang = KodeMataUang_IDR Or KodeMataUang = Kosongan Then cmb_KodeMataUang.SelectedValue = KodeMataUang_USD '(Default :USD)
            lbl_KodeMataUang.Visibility = Visibility.Visible
            cmb_KodeMataUang.Visibility = Visibility.Visible
            'Kolom-kolom Kanan :
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
            lbl_JumlahNota.Visibility = Visibility.Collapsed
            txt_JumlahNota.Visibility = Visibility.Collapsed
            lbl_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            txt_TotalTagihan_Kotor.Visibility = Visibility.Collapsed
            txt_TotalTagihan.Visibility = Visibility.Collapsed
            txt_TotalTagihan_Asing.Visibility = Visibility.Visible
            'Kolom-kolom Tabel :
            Harga_Satuan_Asing.Visibility = Visibility.Visible
            Jumlah_Harga_Per_Item_Asing.Visibility = Visibility.Visible
            Diskon_Per_Item_Asing.Visibility = Visibility.Visible
            Total_Harga_Asing.Visibility = Visibility.Visible
            Harga_Satuan_Asing.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item_Asing.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Asing.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga_Asing.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
            Harga_Satuan.Visibility = Visibility.Collapsed
            Jumlah_Harga_Per_Item.Visibility = Visibility.Collapsed
            Diskon_Per_Item_Rp.Visibility = Visibility.Collapsed
            Total_Harga.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub cmb_JenisPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPN.SelectionChanged
        JenisPPN = cmb_JenisPPN.SelectedValue
        If JenisPPN = JenisPPN_NonPPN Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        KondisiFormSetelahPerubahan()
        LogikaAdaPPh(True)
    End Sub


    Private Sub cmb_PerlakuanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PerlakuanPPN.SelectionChanged
        PerlakuanPPN = cmb_PerlakuanPPN.SelectedValue
        LogikaAdaPPh(True)
    End Sub


    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        KodeMataUang = cmb_KodeMataUang.SelectedValue
        If KodeMataUang <> KodeMataUang_IDR Then
            Harga_Satuan_Asing.Header = "Harga Satuan" & Enter1Baris & "(" & KodeMataUang & ")"
            Jumlah_Harga_Per_Item_Asing.Header = "Jumlah Harga" & Enter1Baris & "(" & KodeMataUang & ")"
            Diskon_Per_Item_Asing.Header = "Diskon" & Enter1Baris & "(" & KodeMataUang & ")"
            Total_Harga_Asing.Header = "Total" & Enter1Baris & "(" & KodeMataUang & ")"
        End If
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



    Private Sub rdb_JumlahHariJangkaWaktuPenyelesaian_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_JumlahHariJangkaWaktuPenyelesaian.Checked
        If rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True Then
            txt_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = True
            lbl_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = True
            dtp_TanggalJangkaWaktuPenyelesaian.Text = Kosongan
            dtp_TanggalJangkaWaktuPenyelesaian.IsEnabled = False
        Else
            txt_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
            lbl_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub rdb_TanggalJangkaWaktuPenyelesaian_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_TanggalJangkaWaktuPenyelesaian.Checked
        If rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = True Then
            txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
            txt_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
            lbl_JumlahHariJangkaWaktuPenyelesaian.IsEnabled = False
            dtp_TanggalJangkaWaktuPenyelesaian.IsEnabled = True
        Else
            dtp_TanggalJangkaWaktuPenyelesaian.IsEnabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub txt_JumlahHariJangkaWaktuPenyelesaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHariJangkaWaktuPenyelesaian.TextChanged
        JumlahHari_JangkaWaktuPenyelesaian = AmbilAngka(txt_JumlahHariJangkaWaktuPenyelesaian.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHariJangkaWaktuPenyelesaian)
        If JumlahHari_JangkaWaktuPenyelesaian > 0 Then
            rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_JumlahHariJangkaWaktuPenyelesaian_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles txt_JumlahHariJangkaWaktuPenyelesaian.MouseDown
        rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True
    End Sub


    Private Sub dtp_TanggalJangkaWaktuPenyelesaian_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJangkaWaktuPenyelesaian.SelectedDateChanged
        If dtp_TanggalJangkaWaktuPenyelesaian.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalJangkaWaktuPenyelesaian)
            Tanggal_JangkaWaktuPenyelesaian = TanggalFormatTampilan(dtp_TanggalJangkaWaktuPenyelesaian.SelectedDate)
        End If
    End Sub
    Private Sub dtp_TanggalJangkaWaktuPenyelesaian_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles dtp_TanggalJangkaWaktuPenyelesaian.MouseDown
        rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = True
    End Sub

    Private Sub cmb_MetodePembayaran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MetodePembayaran.SelectionChanged
        MetodePembayaran = cmb_MetodePembayaran.SelectedValue
        BasisPerhitunganTermin = Kosongan
        KosongkanKolomUangMukaDanTermin()
        If MetodePembayaran = MetodePembayaran_Termin Then
            txt_UangMuka_Persen.IsReadOnly = True
            txt_Termin1_Persen.IsReadOnly = True
            txt_Termin2_Persen.IsReadOnly = True
            txt_UangMuka_Rp.IsReadOnly = True
            txt_Termin1_Rp.IsReadOnly = True
            txt_Termin2_Rp.IsReadOnly = True
            txt_UangMuka_Asing.IsReadOnly = True
            txt_Termin1_Asing.IsReadOnly = True
            txt_Termin2_Asing.IsReadOnly = True
            KontenCombo_BasisPerhitunganTermin()
        End If
        VisibilitasKolomTermin(True)
    End Sub

    Private Sub cmb_BasisPerhitunganTermin_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_BasisPerhitunganTermin.SelectionChanged
        BasisPerhitunganTermin = cmb_BasisPerhitunganTermin.SelectedValue
        KosongkanKolomUangMukaDanTermin()
        Select Case BasisPerhitunganTermin
            Case BasisPerhitunganTermin_Prosentase
                txt_UangMuka_Persen.IsReadOnly = False
                txt_Termin1_Persen.IsReadOnly = False
                txt_Termin2_Persen.IsReadOnly = False
                txt_UangMuka_Rp.IsReadOnly = True
                txt_Termin1_Rp.IsReadOnly = True
                txt_Termin2_Rp.IsReadOnly = True
                txt_UangMuka_Asing.IsReadOnly = True
                txt_Termin1_Asing.IsReadOnly = True
                txt_Termin2_Asing.IsReadOnly = True
                If Not ProsesLoadingForm Then txt_UangMuka_Persen.Focus()
            Case BasisPerhitunganTermin_Nominal
                txt_UangMuka_Persen.IsReadOnly = True
                txt_Termin1_Persen.IsReadOnly = True
                txt_Termin2_Persen.IsReadOnly = True
                txt_UangMuka_Rp.IsReadOnly = False
                txt_Termin1_Rp.IsReadOnly = False
                txt_Termin2_Rp.IsReadOnly = False
                txt_UangMuka_Asing.IsReadOnly = False
                txt_Termin1_Asing.IsReadOnly = False
                txt_Termin2_Asing.IsReadOnly = False
                If Not ProsesLoadingForm Then
                    If PenjualanLokal Then txt_UangMuka_Rp.Focus()
                    If PenjualanEkspor Then txt_UangMuka_Asing.Focus()
                End If
        End Select
    End Sub

    Sub VisibilitasKolomTermin(Visibilitas As Boolean)
        'Basis Perhitungan termin :
        lbl_BasisPerhitunganTermin.Visibility = Visibility.Collapsed
        cmb_BasisPerhitunganTermin.Visibility = Visibility.Collapsed
        'Uang Muka :
        lbl_UangMuka.Visibility = Visibility.Collapsed
        txt_UangMuka_Persen.Visibility = Visibility.Collapsed
        txt_UangMuka_Persen.Text = Kosongan
        lbl_PersenUangMuka.Visibility = Visibility.Collapsed
        txt_UangMuka_Rp.Visibility = Visibility.Collapsed
        txt_UangMuka_Asing.Visibility = Visibility.Collapsed
        'Termin 1 :
        lbl_Termin1.Visibility = Visibility.Collapsed
        txt_Termin1_Persen.Visibility = Visibility.Collapsed
        txt_Termin1_Persen.Text = Kosongan
        lbl_PersenTermin1.Visibility = Visibility.Collapsed
        txt_Termin1_Rp.Visibility = Visibility.Collapsed
        txt_Termin1_Asing.Visibility = Visibility.Collapsed
        'Termin 2 :
        lbl_Termin2.Visibility = Visibility.Collapsed
        txt_Termin2_Persen.Visibility = Visibility.Collapsed
        txt_Termin2_Persen.Text = Kosongan
        lbl_PersenTermin2.Visibility = Visibility.Collapsed
        txt_Termin2_Rp.Visibility = Visibility.Collapsed
        txt_Termin2_Asing.Visibility = Visibility.Collapsed
        'Pelunasan :
        lbl_Pelunasan.Visibility = Visibility.Collapsed
        txt_Pelunasan_Persen.Visibility = Visibility.Collapsed
        txt_Pelunasan_Persen.Text = Kosongan
        lbl_PersenPelunasan.Visibility = Visibility.Collapsed
        txt_Pelunasan_Rp.Visibility = Visibility.Collapsed
        txt_Pelunasan_Asing.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If MetodePembayaran = MetodePembayaran_Termin Then
                'Basis Perhitungan termin :
                lbl_BasisPerhitunganTermin.Visibility = Visibility.Visible
                cmb_BasisPerhitunganTermin.Visibility = Visibility.Visible
                'Uang Muka :
                lbl_UangMuka.Visibility = Visibility.Visible
                txt_UangMuka_Persen.Visibility = Visibility.Visible
                txt_UangMuka_Persen.Text = Kosongan
                lbl_PersenUangMuka.Visibility = Visibility.Visible
                If Not MitraLuarNegeri Then txt_UangMuka_Rp.Visibility = Visibility.Visible
                If MitraLuarNegeri Then txt_UangMuka_Asing.Visibility = Visibility.Visible
                'Termin 1 :
                lbl_Termin1.Visibility = Visibility.Visible
                txt_Termin1_Persen.Visibility = Visibility.Visible
                txt_Termin1_Persen.Text = Kosongan
                lbl_PersenTermin1.Visibility = Visibility.Visible
                If Not MitraLuarNegeri Then txt_Termin1_Rp.Visibility = Visibility.Visible
                If MitraLuarNegeri Then txt_Termin1_Asing.Visibility = Visibility.Visible
                'Termin 2 :
                lbl_Termin2.Visibility = Visibility.Visible
                txt_Termin2_Persen.Visibility = Visibility.Visible
                txt_Termin2_Persen.Text = Kosongan
                lbl_PersenTermin2.Visibility = Visibility.Visible
                If Not MitraLuarNegeri Then txt_Termin2_Rp.Visibility = Visibility.Visible
                If MitraLuarNegeri Then txt_Termin2_Asing.Visibility = Visibility.Visible
                'Pelunasan :
                lbl_Pelunasan.Visibility = Visibility.Visible
                txt_Pelunasan_Persen.Visibility = Visibility.Visible
                txt_Pelunasan_Persen.Text = Kosongan
                lbl_PersenPelunasan.Visibility = Visibility.Visible
                If Not MitraLuarNegeri Then txt_Pelunasan_Rp.Visibility = Visibility.Visible
                If MitraLuarNegeri Then txt_Pelunasan_Asing.Visibility = Visibility.Visible
            End If
        End If
    End Sub


    Private Sub txt_UangMuka_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMuka_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_UangMuka_Persen, UangMuka_Persen)
        If BasisPerhitunganTermin = BasisPerhitunganTermin_Prosentase Then
            txt_Termin1_Persen.Text = Kosongan
            txt_Termin2_Persen.Text = Kosongan
            Pelunasan_Persen = 100 - UangMuka_Persen
            txt_Pelunasan_Persen.Text = Pelunasan_Persen
            Perhitungan()
        End If
    End Sub
    Private Sub txt_UangMuka_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_UangMuka_Persen.PreviewTextInput
    End Sub


    Private Sub txt_UangMuka_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMuka_Rp.TextChanged
        If PenjualanLokal Then
            UangMuka_Rp = AmbilAngka(txt_UangMuka_Rp.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                txt_Termin1_Rp.Text = Kosongan
                txt_Termin2_Rp.Text = Kosongan
                Pelunasan_Rp = JumlahNota - UangMuka_Rp
                txt_Pelunasan_Rp.Text = Pelunasan_Rp
                Perhitungan()
            End If
        End If
    End Sub

    Private Sub txt_UangMuka_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UangMuka_Asing.TextChanged
        If PenjualanEkspor Then
            UangMuka_Asing = AmbilAngka_Asing(txt_UangMuka_Asing.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                txt_Termin1_Asing.Text = Kosongan
                txt_Termin2_Asing.Text = Kosongan
                Pelunasan_Asing = JumlahNota_Asing - UangMuka_Asing
                txt_Pelunasan_Asing.Text = Pelunasan_Asing
                Perhitungan()
            End If
        End If
    End Sub

    Private Sub txt_Termin1_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin1_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Termin1_Persen, Termin1_Persen)
        If BasisPerhitunganTermin = BasisPerhitunganTermin_Prosentase Then
            txt_Termin2_Persen.Text = Kosongan
            Pelunasan_Persen = 100 - (UangMuka_Persen + Termin1_Persen)
            txt_Pelunasan_Persen.Text = Pelunasan_Persen
            Perhitungan()
        End If
    End Sub
    Private Sub txt_Termin1_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Termin1_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Termin1_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin1_Rp.TextChanged
        If PenjualanLokal Then
            Termin1_Rp = AmbilAngka(txt_Termin1_Rp.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                txt_Termin2_Rp.Text = Kosongan
                Pelunasan_Rp = JumlahNota - (UangMuka_Rp + Termin1_Rp)
                txt_Pelunasan_Rp.Text = Pelunasan_Rp
                Perhitungan()
            End If
        End If
    End Sub

    Private Sub txt_Termin1_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin1_Asing.TextChanged
        If PenjualanEkspor Then
            Termin1_Asing = AmbilAngka_Asing(txt_Termin1_Asing.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                txt_Termin2_Asing.Text = Kosongan
                Pelunasan_Asing = JumlahNota_Asing - (UangMuka_Asing + Termin1_Asing)
                txt_Pelunasan_Asing.Text = Pelunasan_Asing
                Perhitungan()
            End If
        End If
    End Sub

    Private Sub txt_Termin2_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin2_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Termin2_Persen, Termin2_Persen)
        If BasisPerhitunganTermin = BasisPerhitunganTermin_Prosentase Then
            If Termin2_Persen > 0 Then
                If Termin1_Persen = 0 Then
                    PesanPeringatan("Silakan isi kolom 'Termin 1' terlebih dahulu..!")
                    txt_Termin2_Persen.Text = Kosongan
                    txt_Termin1_Persen.Focus()
                    Return
                End If
            End If
            Pelunasan_Persen = 100 - (UangMuka_Persen + Termin1_Persen + Termin2_Persen)
            txt_Pelunasan_Persen.Text = Pelunasan_Persen
            Perhitungan()
        End If
    End Sub
    Private Sub txt_Termin2_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Termin2_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Termin2_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin2_Rp.TextChanged
        If PenjualanLokal Then
            Termin2_Rp = AmbilAngka(txt_Termin2_Rp.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                If Termin2_Rp > 0 Then
                    If Termin1_Rp = 0 Then
                        PesanPeringatan("Silakan isi kolom 'Termin 1' terlebih dahulu..!")
                        txt_Termin2_Rp.Text = Kosongan
                        txt_Termin1_Rp.Focus()
                        Return
                    End If
                End If
                Pelunasan_Rp = JumlahNota - (UangMuka_Rp + Termin1_Rp + Termin2_Rp)
                txt_Pelunasan_Rp.Text = Pelunasan_Rp
                Perhitungan()
            End If
        End If
    End Sub

    Private Sub txt_Termin2_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Termin2_Asing.TextChanged
        If PenjualanEkspor Then
            Termin2_Asing = AmbilAngka_Asing(txt_Termin2_Asing.Text)
            If BasisPerhitunganTermin = BasisPerhitunganTermin_Nominal Then
                If Termin2_Asing > 0 Then
                    If Termin1_Asing = 0 Then
                        PesanPeringatan("Silakan isi kolom 'Termin 1' terlebih dahulu..!")
                        txt_Termin2_Asing.Text = Kosongan
                        txt_Termin1_Asing.Focus()
                        Return
                    End If
                End If
                Pelunasan_Asing = JumlahNota_Asing - (UangMuka_Asing + Termin1_Asing + Termin2_Asing)
                txt_Pelunasan_Asing.Text = Pelunasan_Asing
                Perhitungan()
            End If
        End If
    End Sub


    Private Sub txt_Pelunasan_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pelunasan_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Pelunasan_Persen, Pelunasan_Persen)
    End Sub
    Private Sub txt_Pelunasan_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Pelunasan_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Pelunasan_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pelunasan_Rp.TextChanged
        Pelunasan_Rp = AmbilAngka(txt_Pelunasan_Rp.Text)
    End Sub


    Private Sub txt_Pelunasan_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pelunasan_Asing.TextChanged
        Pelunasan_Asing = AmbilAngka_Asing(txt_Pelunasan_Asing.Text)
    End Sub


    Dim JumlahNota As Int64
    Dim JumlahNota_Asing As Decimal
    Dim TotalProsentasePembayaran As Decimal
    Dim MetodePembayaran
    Dim BasisPerhitunganTermin
    Dim UangMuka_Persen As Decimal
    Dim Termin1_Persen As Decimal
    Dim Termin2_Persen As Decimal
    Dim Pelunasan_Persen As Decimal
    Dim UangMuka_Asing As Decimal
    Dim Termin1_Asing As Decimal
    Dim Termin2_Asing As Decimal
    Dim Pelunasan_Asing As Decimal
    Dim UangMuka_Rp As Int64
    Dim Termin1_Rp As Int64
    Dim Termin2_Rp As Int64
    Dim Pelunasan_Rp As Int64
    Dim JumlahTermin
    Sub LogikaUangMukaDanTermin()

        JumlahTermin = 0

        If Not MetodePembayaran = MetodePembayaran_Termin Then Return

        Select Case BasisPerhitunganTermin
            Case BasisPerhitunganTermin_Prosentase
                LogikaUangMukaDanTermin_BasisProsentase()
            Case BasisPerhitunganTermin_Nominal
                LogikaUangMukaDanTermin_BasisNominal()
        End Select

    End Sub

    Sub LogikaUangMukaDanTermin_BasisProsentase()

        TotalProsentasePembayaran = UangMuka_Persen + Termin1_Persen + Termin2_Persen + Pelunasan_Persen

        If PenjualanLokal Then
            UangMuka_Rp = (UangMuka_Persen / 100) * JumlahNota
            Termin1_Rp = (Termin1_Persen / 100) * JumlahNota
            Termin2_Rp = (Termin2_Persen / 100) * JumlahNota
            Pelunasan_Rp = JumlahNota - (UangMuka_Rp + Termin1_Rp + Termin2_Rp)
            If Pelunasan_Rp <= 0 Then
                txt_Pelunasan_Persen.Text = Kosongan
                txt_UangMuka_Persen.Focus()
                PesanPeringatan("Silakan isi kolom 'Termin' dengan benar..!")
                KosongkanKolomUangMukaDanTermin()
                Return
            End If
        Else
            UangMuka_Asing = (UangMuka_Persen / 100) * JumlahNota_Asing
            Termin1_Asing = (Termin1_Persen / 100) * JumlahNota_Asing
            Termin2_Asing = (Termin2_Persen / 100) * JumlahNota_Asing
            Pelunasan_Asing = JumlahNota_Asing - (UangMuka_Asing + Termin1_Asing + Termin2_Asing)
            If Pelunasan_Asing <= 0 Then
                txt_Pelunasan_Persen.Text = Kosongan
                txt_UangMuka_Persen.Focus()
                PesanPeringatan("Silakan isi kolom 'Termin' dengan benar..!")
                KosongkanKolomUangMukaDanTermin()
                Return
            End If
        End If

        If UangMuka_Persen > 0 Then JumlahTermin += 1
        If Termin1_Persen > 0 Then JumlahTermin += 1
        If Termin2_Persen > 0 Then JumlahTermin += 1
        If Pelunasan_Persen > 0 Then JumlahTermin += 1

        If PenjualanLokal Then
            txt_UangMuka_Rp.Text = UangMuka_Rp
            txt_Termin1_Rp.Text = Termin1_Rp
            txt_Termin2_Rp.Text = Termin2_Rp
            txt_Pelunasan_Rp.Text = Pelunasan_Rp
        Else
            txt_UangMuka_Asing.Text = PembulatanDesimal2Digit(UangMuka_Asing)
            txt_Termin1_Asing.Text = PembulatanDesimal2Digit(Termin1_Asing)
            txt_Termin2_Asing.Text = PembulatanDesimal2Digit(Termin2_Asing)
            txt_Pelunasan_Asing.Text = PembulatanDesimal2Digit(Pelunasan_Asing)
        End If

    End Sub

    Sub LogikaUangMukaDanTermin_BasisNominal()

        TotalProsentasePembayaran = UangMuka_Persen + Termin1_Persen + Termin2_Persen + Pelunasan_Persen

        If PenjualanLokal Then
            UangMuka_Persen = PembulatanDesimal2Digit((UangMuka_Rp / JumlahNota) * 100)
            Termin1_Persen = PembulatanDesimal2Digit((Termin1_Rp / JumlahNota) * 100)
            Termin2_Persen = PembulatanDesimal2Digit((Termin2_Rp / JumlahNota) * 100)
            Pelunasan_Persen = 100 - (UangMuka_Persen + Termin1_Persen + Termin2_Persen)
            If Pelunasan_Persen <= 0 Then
                txt_Pelunasan_Rp.Text = Kosongan
                txt_UangMuka_Rp.Focus()
                PesanPeringatan("Silakan isi kolom 'Termin' dengan benar..!")
                KosongkanKolomUangMukaDanTermin()
                Return
            End If
        Else
            UangMuka_Persen = PembulatanDesimal2Digit((UangMuka_Asing / JumlahNota_Asing) * 100)
            Termin1_Persen = PembulatanDesimal2Digit((Termin1_Asing / JumlahNota_Asing) * 100)
            Termin2_Persen = PembulatanDesimal2Digit((Termin2_Asing / JumlahNota_Asing) * 100)
            Pelunasan_Persen = 100 - (UangMuka_Persen + Termin1_Persen + Termin2_Persen)
            If Pelunasan_Persen <= 0 Then
                txt_Pelunasan_Asing.Text = Kosongan
                txt_UangMuka_Asing.Focus()
                PesanPeringatan("Silakan isi kolom 'Termin' dengan benar..!")
                KosongkanKolomUangMukaDanTermin()
                Return
            End If
        End If

        If UangMuka_Persen > 0 Then JumlahTermin += 1
        If Termin1_Persen > 0 Then JumlahTermin += 1
        If Termin2_Persen > 0 Then JumlahTermin += 1
        If Pelunasan_Persen > 0 Then JumlahTermin += 1

        txt_UangMuka_Persen.Text = UangMuka_Persen
        txt_Termin1_Persen.Text = Termin1_Persen
        txt_Termin2_Persen.Text = Termin2_Persen
        txt_Pelunasan_Persen.Text = Pelunasan_Persen

    End Sub

    Sub KosongkanKolomUangMukaDanTermin()
        'Persen :
        txt_UangMuka_Persen.Text = Kosongan
        txt_Termin1_Persen.Text = Kosongan
        txt_Termin2_Persen.Text = Kosongan
        txt_Pelunasan_Persen.Text = Kosongan
        'Rupiah :
        txt_UangMuka_Rp.Text = Kosongan
        txt_Termin1_Rp.Text = Kosongan
        txt_Termin2_Rp.Text = Kosongan
        txt_Pelunasan_Rp.Text = Kosongan
        'Asing :
        txt_UangMuka_Asing.Text = Kosongan
        txt_Termin1_Asing.Text = Kosongan
        txt_Termin2_Asing.Text = Kosongan
        txt_Pelunasan_Asing.Text = Kosongan
    End Sub



    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub


    Private Sub cmb_Kontrol_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kontrol.SelectionChanged
        Kontrol = cmb_Kontrol.SelectedValue
        KondisiFormSetelahPerubahan()
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
        KodeProjectProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project_Produk")
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        DeskripsiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Deskripsi_Produk")
        JumlahProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk"))
        SatuanProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Satuan_Produk")
        DiskonPerItem_Persen_Terseleksi = Replace(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        HargaSatuan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan"))
        JumlahHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga_Per_Item"))
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Rp"))
        TotalHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga"))

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
        btn_Perbaiki_Click(sender, e)
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click
        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan pilih 'Customer' terlebih dahulu.")
            txt_KodeCustomer.Focus()
            Return
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
        win_InputProduk_Nota = New wpfWin_InputProduk_Nota
        win_InputProduk_Nota.ResetForm()
        win_InputProduk_Nota.MataUang = MataUang
        win_InputProduk_Nota.txt_NomorUrut.Text = JumlahProduk + 1
        win_InputProduk_Nota.FungsiForm = FungsiForm_TAMBAH
        win_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        win_InputProduk_Nota.JalurMasuk = Form_INPUTPOPENJUALAN
        win_InputProduk_Nota.ShowDialog()
        If win_InputProduk_Nota.Proses = False Then Return
        Perhitungan()
        BersihkanSeleksi_TabelProduk()
    End Sub

    Private Sub btn_Perbaiki_Click(sender As Object, e As RoutedEventArgs) Handles btn_Perbaiki.Click
        win_InputProduk_Nota = New wpfWin_InputProduk_Nota
        win_InputProduk_Nota.ResetForm()
        win_InputProduk_Nota.MataUang = MataUang
        win_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        win_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        win_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        win_InputProduk_Nota.JenisProduk_PerItem = JenisProdukPerItem_Terseleksi
        win_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        win_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        win_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        win_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        win_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        win_InputProduk_Nota.txt_HargaSatuan_Asing.Text = HargaSatuan_Asing_Terseleksi
        win_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        win_InputProduk_Nota.txt_KodeProject.Text = KodeProjectProduk_Terseleksi
        If KodeProjectProduk_Terseleksi = Kosongan Then win_InputProduk_Nota.cmb_Peruntukan.SelectedValue = Peruntukan_NonProject
        If KodeProjectProduk_Terseleksi <> Kosongan Then win_InputProduk_Nota.cmb_Peruntukan.SelectedValue = Peruntukan_Project
        win_InputProduk_Nota.JalurMasuk = Form_INPUTPOPENJUALAN
        win_InputProduk_Nota.ShowDialog()
        If win_InputProduk_Nota.Proses = False Then Return
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
        Perhitungan()
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
        Perhitungan()
        KondisiFormSetelahPerubahan()
        BarisTotalTabel()
    End Sub



    Private Sub txt_JumlahNota_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahNota.TextChanged
        JumlahNota = AmbilAngka(txt_JumlahNota.Text)
    End Sub


    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Diskon_Persen, Diskon_Persen)
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub
    Private Sub txt_Diskon_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Diskon_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Rp.TextChanged
        Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
    End Sub


    Private Sub txt_DPPBarang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPBarang.TextChanged
        DPPBarang = AmbilAngka(txt_DPPBarang.Text)
    End Sub


    Private Sub txt_DPPJasa_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPJasa.TextChanged
        DPPJasa = AmbilAngka(txt_DPPJasa.Text)
    End Sub


    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
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
            Perhitungan()
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
    End Sub
    Private Sub txt_TarifPPN_11Per12_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_TarifPPN_11Per12.PreviewKeyDown
        EksekusiKodeLogikaPPN = False
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPN.TextChanged
        PPN = AmbilAngka(txt_PPN.Text)
    End Sub


    Private Sub txt_TotalTagihan_Kotor_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan_Kotor.TextChanged
        TotalTagihan_Kotor = AmbilAngka(txt_TotalTagihan_Kotor.Text)
    End Sub


    Private Sub cmb_JenisPPh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPh.SelectionChanged
        JenisPPh = cmb_JenisPPh.SelectedValue
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then
            If (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_Jasa) And JenisPPh = JenisPPh_NonPPh Then
                PesanUntukProgrammer("Pilihan ini haris dikonsultasikan dengan pihak terkait.")
            End If
        End If
    End Sub


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        TextBoxFormatPersen_WPF(txt_TarifPPh, TarifPPh)
        If TarifPPh > 100 Then
            MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        txt_PPhTerutang.Text = DPP * Persen(TarifPPh)
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub
    Private Sub txt_TarifPPh_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPh.PreviewTextInput
    End Sub


    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        EksekusiKodeLogikaPPN = False
        If PenjualanLokal Then PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        PerhitunganFinal()
        If PPhDipotong < 0 Then
            txt_PPhDitanggung.Text = 0
            txt_PPhDitanggung.Focus()
            MsgBox("Silakan isi kolom 'PPh Ditanggung' dengan benar!")
            Return
        End If
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        If PenjualanLokal Then PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
    End Sub


    Private Sub txt_BiayaTransportasiPenjualan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaTransportasiPenjualan.TextChanged
        EksekusiKodeLogikaPPN = False
        BiayaTransportasiPenjualan = AmbilAngka(txt_BiayaTransportasiPenjualan.Text)
        PerhitunganFinal()
    End Sub


    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTagihan.TextChanged
        TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If NomorPO = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor PO'")
            txt_NomorPO.Focus()
            Return
        End If

        If dtp_TanggalPO.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal PO'.")
            dtp_TanggalPO.Focus()
            Return
        End If

        If JenisPPN = Kosongan Then
            PesanPeringatan("Silakan pilih 'Jenis PPN'.")
            cmb_JenisPPN.Focus()
            Return
        End If

        If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
            PesanPeringatan("Silakan pilih 'Perlakuan PPN'.")
            cmb_PerlakuanPPN.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            MsgBox("Silakan tambahkan data 'Barang/Jasa'.")
            Return
        End If

        If KodeCustomer = Nothing Then
            MsgBox("silakan isi data 'Customer'.")
            Return
        End If

        If KodeMataUang = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_KodeMataUang, "Kode Mata Uang")
            Return
        End If

        If AdaPPh Then
            If JenisPPh = Kosongan Then
                MsgBox("Silakan pilih 'Jenis PPh'.")
                cmb_JenisPPh.Focus()
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

        If rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = False And rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = False Then
            MsgBox("Silakan isi kolom 'Jatuh TempoPenyelesaian'.")
            Return
        End If

        If rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True Then
            If JumlahHari_JangkaWaktuPenyelesaian = 0 Then
                MsgBox("Silakan isi kolom 'Jumlah Hari'.")
                txt_JumlahHariJangkaWaktuPenyelesaian.Focus()
                Return
            End If
            Tanggal_JangkaWaktuPenyelesaian = TanggalKosongSimpan
        End If

        If rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = True Then
            If dtp_TanggalJangkaWaktuPenyelesaian.Text = Kosongan Then
                PesanPeringatan("Silakan isi 'Tanggal Jatuh Tempo Penyelesaian'.")
                dtp_TanggalJangkaWaktuPenyelesaian.Focus()
                Return
            End If
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then

            If TotalProsentasePembayaran <> 100 Or Pelunasan_Persen = 100 Then
                PesanPeringatan("Silakan isi kolom 'Termin' dengan benar..!")
                txt_UangMuka_Persen.Focus()
                KosongkanKolomUangMukaDanTermin()
                Return
            End If

        End If

        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then
            If DPPBarang = 0 Then
                PesanPeringatan("Silakan tambahkan produk barang.")
                Return
            End If
            If DPPJasa = 0 Then
                PesanPeringatan("Silakan tambahkan produk jasa.")
                Return
            End If
        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_PO " &
                                       " WHERE Angka_PO = '" & AngkaPO & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If


        If StatusSuntingDatabase = True Then

            Dim JenisPPh_Simpan = JenisPPh
            Dim TarifPPh_Simpan As Decimal = TarifPPh
            Dim PPhTerutang_Simpan As Int64 = PPhTerutang
            Dim PPhDipotong_Simpan As Int64 = PPhDipotong
            Dim PPhDitanggung_Simpan As Int64 = PPhDitanggung

            Dim UangMuka_Simpan As Decimal = 0
            Dim Termin1_Simpan As Decimal = 0
            Dim Termin2_Simpan As Decimal = 0
            Dim Pelunasan_Simpan As Decimal = 0

            Select Case BasisPerhitunganTermin
                Case BasisPerhitunganTermin_Prosentase
                    UangMuka_Simpan = UangMuka_Persen
                    Termin1_Simpan = Termin1_Persen
                    Termin2_Simpan = Termin2_Persen
                    Pelunasan_Simpan = Pelunasan_Persen
                Case BasisPerhitunganTermin_Nominal
                    If PenjualanLokal Then
                        UangMuka_Simpan = UangMuka_Rp
                        Termin1_Simpan = Termin1_Rp
                        Termin2_Simpan = Termin2_Rp
                        Pelunasan_Simpan = Pelunasan_Rp
                    Else
                        UangMuka_Simpan = UangMuka_Asing
                        Termin1_Simpan = Termin1_Asing
                        Termin2_Simpan = Termin2_Asing
                        Pelunasan_Simpan = Pelunasan_Asing
                    End If
            End Select

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_PO")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            For Each row As DataRow In datatabelUtama.Rows 'Awal Loop ========================================================

                If AmbilAngka(row("Nomor_Urut").ToString) = 0 Then Exit For

                NomorUrutProduk += 1
                NomorID += 1
                JenisProduk_PerItem = row("Jenis_Produk_Per_Item")
                KodeProjectProduk = row("Kode_Project_Produk")
                NamaProduk = row("Nama_Produk")
                DeskripsiProduk = row("Deskripsi_Produk")
                JumlahProduk_PerItem = AmbilAngka(row("Jumlah_Produk"))
                SatuanProduk = row("Satuan_Produk")
                HargaSatuan = AmbilAngka(row("Harga_Satuan"))
                HargaSatuan_Asing = AmbilAngka_Asing(row("Harga_Satuan_Asing"))
                DiskonPerItem_Persen = Replace(row("Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHargaPerItem = AmbilAngka(row("Total_Harga"))
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                    DPPBarang = 0
                    DPPJasa = DPP
                End If
                QueryPenyimpanan = " INSERT INTO tbl_Penjualan_PO VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaPO & "', " &
                    " '" & NomorPO & "', " &
                    " '" & TanggalFormatSimpan(TanggalPO) & "', " &
                    " '" & JenisProduk_Induk & "', " &
                    " '" & TermOfPayment & "', " &
                    " '" & KeteranganToP & "', " &
                    " '" & JumlahHari_JangkaWaktuPenyelesaian & "', " &
                    " '" & TanggalFormatSimpan(Tanggal_JangkaWaktuPenyelesaian) & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & KodeCustomer & "', " &
                    " '" & NamaCustomer & "', " &
                    " '" & KodeMataUang & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProduk_PerItem & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(HargaSatuan_Asing) & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHargaPerItem & "', " &
                    " '" & JumlahHargaKeseluruhan & "', " &
                    " '" & Diskon_Rp & "', " &
                    " '" & DPPBarang & "', " &
                    " '" & DPPJasa & "', " &
                    " '" & DPP & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & PPN & "', " &
                    " '" & JenisPPh_Simpan & "', " &
                    " '" & DesimalFormatSimpan(TarifPPh_Simpan) & "', " &
                    " '" & PPhTerutang_Simpan & "', " &
                    " '" & PPhDitanggung_Simpan & "', " &
                    " '" & PPhDipotong_Simpan & "', " &
                    " '" & BiayaTransportasiPenjualan & "', " &
                    " '" & TotalTagihan & "', " &
                    " '" & MetodePembayaran & "', " &
                    " '" & BasisPerhitunganTermin & "', " &
                    " '" & DesimalFormatSimpan(UangMuka_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Termin1_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Termin2_Simpan) & "', " &
                    " '" & DesimalFormatSimpan(Pelunasan_Simpan) & "', " &
                    " '" & JumlahTermin & "', " &
                    " '" & Catatan & "', " &
                    " '" & Kontrol & "', " &
                    " '" & UserAktif & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit For

            Next

            AksesDatabase_Transaksi(Tutup)

        End If

        'Jika ada perubahan Nomor PO :
        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_EDIT And NomorPO_Lama <> NomorPO Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_Penjualan_SJ " &
                                      " SET Nomor_PO_Produk = '" & NomorPO & "', Tanggal_PO_Produk = '" & TanggalFormatSimpan(TanggalPO) & "' " &
                                      " WHERE Nomor_PO_Produk = '" & NomorPO_Lama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                'Ubah Nomor PO yang ada di Data BAST :
                cmd = New OdbcCommand(" UPDATE tbl_Penjualan_BAST " &
                                      " SET Nomor_PO_Produk = '" & NomorPO & "', Tanggal_PO_Produk = '" & TanggalFormatSimpan(TanggalPO) & "' " &
                                      " WHERE Nomor_PO_Produk = '" & NomorPO_Lama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                RefreshTampilanSJBASTPenjualan()
            End If
        End If

        If StatusSuntingDatabase = True Then
            RefreshTampilanPOPenjualan()
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        'Buat_DataTabelTotal()
        txt_KodeCustomer.IsReadOnly = True
        txt_NamaCustomer.IsReadOnly = True
        txt_PICCustomer.IsReadOnly = True
        txt_AlamatCustomer.IsReadOnly = True
        cmb_JenisPPN.IsReadOnly = True
        cmb_PerlakuanPPN.IsReadOnly = True
        cmb_KodeMataUang.IsReadOnly = True
        txt_Pelunasan_Rp.IsReadOnly = True
        txt_Pelunasan_Persen.IsReadOnly = True
        cmb_Kontrol.IsReadOnly = True
        cmb_JenisPPh.IsReadOnly = True
        txt_Diskon_Persen.Visibility = Visibility.Collapsed
        txt_TarifPPN.IsReadOnly = False '(Ada Tarif PPN yang tidak Normal)
        txt_TarifPPN_11Per12.IsReadOnly = False
        txt_JumlahNota.IsReadOnly = True
        txt_Diskon_Rp.IsReadOnly = True
        txt_DPPBarang.IsReadOnly = True
        txt_DPPJasa.IsReadOnly = True
        txt_DasarPengenaanPajak.IsReadOnly = True
        txt_DasarPengenaanPajak_11Per12.IsReadOnly = True
        txt_PPN.IsReadOnly = True
        txt_TotalTagihan_Kotor.IsReadOnly = True
        txt_PPhTerutang.IsReadOnly = True
        txt_PPhDipotong.IsReadOnly = True
        txt_TotalTagihan.IsReadOnly = True
        txt_TotalTagihan_Asing.IsReadOnly = True
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
        scv_Kanan.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub

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
