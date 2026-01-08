Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembelian

    Public FungsiForm
    Public StatusJurnal As Int64
    Public InputBayar
    'Public JumlahTransaksi_Trx, JumlahDPP_Trx, JumlahPPN_Trx, JumlahPPhDitanggung_Trx, JumlahPPh21_Trx, JumlahPPh23_Trx, JumlahPPh42_Trx As Int64
    Public AngkaNomorJVPembelian
    Public COAKredit
    Public COABiayaTransportasiPembelian
    Public COAPPN

    Dim JenisWP
    Dim LokasiWP

    Dim TahunTransaksi
    Dim TahunInvoice

    'Variabel Inputan :
    Dim NomorPembelian
    Dim NomorPO
    Dim TanggalPO
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim TanggalTransaksi
    Dim TanggalTransaksiSamaDenganTanggalInvoice As Boolean
    Dim KodeDokumen
    Dim KodeSupplier
    Dim NamaSupplier
    Dim NPWP
    Dim NomorFakturPajak
    Dim TanggalFakturPajak
    Dim DPPPPN
    Dim TarifPPN As Decimal
    Dim JumlahPPN
    Dim JenisPPN = Nothing
    Dim PPNDikreditkan
    Dim DibiayakanDikapitalisasi

    Dim NamaBarang1
    Public COABarang1
    Dim NamaAkunBarang1
    Dim DPPBarang1

    Dim NamaBarang2
    Public COABarang2
    Dim NamaAkunBarang2
    Dim DPPBarang2

    Dim NamaJasa1
    Public COAJasa1
    Dim NamaAkunJasa1
    Dim DPPJasa1
    Dim JenisJasa1
    Dim JenisPPh1
    Dim TarifPPh1 As Decimal
    Dim PPhTerutang1
    Dim PPhDitanggung1
    Dim PPhDipotong1

    Dim NamaJasa2
    Public COAJasa2
    Dim NamaAkunJasa2
    Dim DPPJasa2
    Dim JenisJasa2
    Dim JenisPPh2
    Dim TarifPPh2 As Decimal
    Dim PPhTerutang2
    Dim PPhDitanggung2
    Dim PPhDipotong2

    Dim NamaJasa3
    Public COAJasa3
    Dim NamaAkunJasa3
    Dim DPPJasa3
    Dim JenisJasa3
    Dim JenisPPh3
    Dim TarifPPh3 As Decimal
    Dim PPhTerutang3
    Dim PPhDitanggung3
    Dim PPhDipotong3

    Dim JumlahDPPBarang
    Dim JumlahDPPJasa
    Dim TotalDPP
    Dim PPNMasukan
    Dim JumlahPPhTerutang
    Dim JumlahPPhDitanggung
    Dim JumlahPPhDipotong
    Dim JumlahTagihan
    Dim JumlahHutangUsaha
    Dim DueDate
    Dim KodeProject
    Dim SaranaPembayaran
    Public COABank
    Dim NamaAkunBank
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim BiayaTransportasiPembelian
    Dim Keterangan

    Dim JumlahTransfer

    Private Sub frm_InputPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If KodeTautanCOA_PettyCashAdministrasi = Nothing _
            Or KodeTautanCOA_Kas = Nothing _
            Or KodeTautanCOA_PettyCashAdministrasi = Nothing _
            Or KodeTautanCOA_CashAdvance = Nothing _
            Or KodeTautanCOA_HutangUsaha_NonAfiliasi = Nothing _
            Or KodeTautanCOA_PPNMasukan_Lokal = Nothing _
            Or KodeTautanCOA_HutangPPhPasal21 = Nothing _
            Or KodeTautanCOA_HutangPPhPasal23 = Nothing _
            Or KodeTautanCOA_HutangPPhPasal42 = Nothing _
            Or KodeTautanCOA_BiayaAdministrasiBank = Nothing _
            Then
            MsgBox("Untuk menggunakan fitur ini, silakan lengkapi terlebih dahulu Tautan COA untuk :" & Enter1Baris &
                   "- KAS" & Enter1Baris &
                   "- PETTY CASH" & Enter1Baris &
                   "- CASH ADVANCE" & Enter1Baris &
                   "- HUTANG USAHA" & Enter1Baris &
                   "- PPN MASUKAN" & Enter1Baris &
                   "- HUTANG PPH PASAL 21" & Enter1Baris &
                   "- HUTANG PPH PASAL 23" & Enter1Baris &
                   "- HUTANG PPH PASAL 4 (2)" & Enter1Baris &
                   "- BIAYA PPH" & Enter1Baris &
                   "- BIAYA ADMINISTRASI BANK" & Enter1Baris &
                   "- dsb." & Enter1Baris &
                   "di menu : Data --> Data COA --> Tautan COA." & Enter1Baris &
                   "")
            Me.Close()
        End If

        If FungsiForm = FungsiForm_TAMBAH Then 'Jika Form Digunakan untuk TAMBAH DATA, maka berlaku hal di bawah ini :
            'Terkait Ketika Belum Mengisi Nomor Faktur Pajak, maka inputan di bawah ini dinonaktifkan.
            Me.Text = "Input Data Pembelian"
            dtp_TanggalFakturPajak.Enabled = False
            dtp_TanggalFakturPajak.Visible = False
            rdb_TarifPPN_Normal.Enabled = False
            rdb_TarifPPN_Efektif.Enabled = False
            btn_Simpan.Visible = True
            btn_Reset.Visible = True
            btn_Tutup.Text = "Tutup"
            SistemPenomoranOtomatis_Pembelian()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data Pembelian"
            btn_Reset.Enabled = False
            btn_Simpan.Visible = True
            btn_Reset.Visible = True
            btn_Tutup.Text = "Batal"
        End If

        If FungsiForm = "DETAIL" Then
            Me.Text = "Data Pembelian"
            txt_PO_Nomor.Enabled = False
            txt_NomorSJBAST.Enabled = False
            dtp_TanggalSJBAST.Enabled = False
            txt_NomorInvoice.Enabled = False
            dtp_TanggalInvoice.Enabled = False
            grb_TanggalTransaksi.Enabled = False
            txt_KodeDokumen.Enabled = False
            txt_KodeSupplier.Enabled = False
            btn_PilihMitra.Enabled = False
            txt_NamaSupplier.Enabled = False
            txt_NPWP.Enabled = False
            txt_NomorFakturPajak.Enabled = False
            dtp_TanggalFakturPajak.Enabled = False
            txt_DPPPPN.Enabled = False
            rdb_TarifPPN_Normal.Enabled = False
            rdb_TarifPPN_Efektif.Enabled = False
            txt_TarifPPN.Enabled = False
            txt_JumlahPPN.Enabled = False
            cmb_PPNDikreditkan.Enabled = False
            cmb_DibiayakanDikapitalisasi.Enabled = False
            chk_NamaBarang1.Enabled = False
            txt_NamaBarang1.Enabled = False
            txt_NamaAkunBarang1.Enabled = False
            btn_PilihCOABarang1.Enabled = False
            txt_DPPBarang1.Enabled = False
            chk_NamaBarang2.Enabled = False
            txt_NamaBarang2.Enabled = False
            txt_NamaAkunBarang2.Enabled = False
            btn_PilihCOABarang2.Enabled = False
            txt_DPPBarang2.Enabled = False
            chk_NamaJasa1.Enabled = False
            txt_NamaJasa1.Enabled = False
            txt_NamaAkunJasa1.Enabled = False
            btn_PilihCOAJasa1.Enabled = False
            txt_DPPJasa1.Enabled = False
            cmb_JenisJasa1.Enabled = False
            cmb_JenisPPh1.Enabled = False
            txt_TarifPPh1.Enabled = False
            txt_PPhTerutang1.Enabled = False
            txt_PPhDitanggung1.Enabled = False
            chk_NamaJasa2.Enabled = False
            txt_NamaJasa2.Enabled = False
            txt_NamaAkunJasa2.Enabled = False
            btn_PilihCOAJasa2.Enabled = False
            txt_DPPJasa2.Enabled = False
            cmb_JenisJasa2.Enabled = False
            cmb_JenisPPh2.Enabled = False
            txt_TarifPPh2.Enabled = False
            txt_PPhTerutang2.Enabled = False
            txt_PPhDitanggung2.Enabled = False
            chk_NamaJasa3.Enabled = False
            txt_NamaJasa3.Enabled = False
            txt_NamaAkunJasa3.Enabled = False
            btn_PilihCOAJasa3.Enabled = False
            txt_DPPJasa3.Enabled = False
            cmb_JenisJasa3.Enabled = False
            cmb_JenisPPh3.Enabled = False
            txt_TarifPPh3.Enabled = False
            txt_PPhTerutang3.Enabled = False
            txt_PPhDitanggung3.Enabled = False
            dtp_DueDate.Enabled = False
            txt_KodeProject.Enabled = False
            cmb_JenisPembelian.Enabled = False
            cmb_SaranaPembayaran.Enabled = False
            lbl_SaranaPembayaran.Enabled = False
            grb_Bank.Enabled = False
            txt_BiayaTransportasiPembelian.Enabled = False
            txt_Keterangan.Enabled = False
            btn_Simpan.Visible = False
            btn_Reset.Visible = False
            btn_Tutup.Text = "Tutup"
        End If

    End Sub 'Load ============================

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click

        Dim PilihReset = MessageBox.Show("Yakin akan me-reset form..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihReset = vbNo Then Return

        ResetForm()

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_PO_Nomor.Enabled = True
        dtp_TanggalPO.Enabled = True
        txt_NomorSJBAST.Enabled = True
        dtp_TanggalSJBAST.Enabled = True
        txt_NomorInvoice.Enabled = True
        dtp_TanggalInvoice.Enabled = True
        grb_TanggalTransaksi.Enabled = True
        rdb_SamaDenganTanggalInvoice.Checked = False
        rdb_TidakSama.Checked = False
        dtp_TanggalTransaksi.Enabled = False
        txt_KodeDokumen.Enabled = True
        txt_KodeSupplier.Enabled = True
        btn_PilihMitra.Enabled = False 'Untuk Sementara, dibikin false aja. Karena pemilihan Kode Supplier saat ini otomatis dari input PO.
        txt_NamaSupplier.Enabled = True
        txt_NPWP.Enabled = True
        txt_NomorFakturPajak.Enabled = True
        dtp_TanggalFakturPajak.Enabled = False
        txt_DPPPPN.Enabled = True
        rdb_TarifPPN_Normal.Enabled = False
        rdb_TarifPPN_Efektif.Enabled = False
        txt_TarifPPN.Enabled = False
        txt_JumlahPPN.Enabled = False
        cmb_PPNDikreditkan.Enabled = False
        cmb_DibiayakanDikapitalisasi.Visible = False
        cmb_DibiayakanDikapitalisasi.Enabled = True
        chk_NamaBarang1.Enabled = True
        txt_NamaBarang1.Enabled = False
        txt_NamaAkunBarang1.Enabled = False
        btn_PilihCOABarang1.Enabled = False
        txt_DPPBarang1.Enabled = False
        chk_NamaBarang2.Enabled = True
        txt_NamaBarang2.Enabled = False
        txt_NamaAkunBarang2.Enabled = False
        btn_PilihCOABarang2.Enabled = False
        txt_DPPBarang2.Enabled = False
        chk_NamaJasa1.Enabled = True
        txt_NamaJasa1.Enabled = False
        txt_NamaAkunJasa1.Enabled = False
        btn_PilihCOAJasa1.Enabled = False
        txt_DPPJasa1.Enabled = False
        cmb_JenisJasa1.Enabled = False
        cmb_JenisPPh1.Enabled = False
        txt_TarifPPh1.Enabled = False
        txt_PPhTerutang1.Enabled = False
        txt_PPhDitanggung1.Enabled = False
        txt_PPhDipotong1.Enabled = False
        chk_NamaJasa2.Enabled = True
        txt_NamaJasa2.Enabled = False
        txt_NamaAkunJasa2.Enabled = False
        btn_PilihCOAJasa2.Enabled = False
        txt_DPPJasa2.Enabled = False
        cmb_JenisJasa2.Enabled = False
        cmb_JenisPPh2.Enabled = False
        txt_TarifPPh2.Enabled = False
        txt_PPhTerutang2.Enabled = False
        txt_PPhDitanggung2.Enabled = False
        txt_PPhDipotong2.Enabled = False
        chk_NamaJasa3.Enabled = True
        txt_NamaJasa3.Enabled = False
        txt_NamaAkunJasa3.Enabled = False
        btn_PilihCOAJasa3.Enabled = False
        txt_DPPJasa3.Enabled = False
        cmb_JenisJasa3.Enabled = False
        cmb_JenisPPh3.Enabled = False
        txt_TarifPPh3.Enabled = False
        txt_PPhTerutang3.Enabled = False
        txt_PPhDitanggung3.Enabled = False
        txt_PPhDipotong3.Enabled = False
        dtp_DueDate.Enabled = True
        txt_KodeProject.Enabled = True
        cmb_JenisPembelian.Enabled = True
        cmb_SaranaPembayaran.Visible = False
        lbl_SaranaPembayaran.Visible = False
        cmb_SaranaPembayaran.Enabled = True
        lbl_SaranaPembayaran.Enabled = True
        txt_BiayaTransportasiPembelian.Enabled = True
        txt_Keterangan.Enabled = True
        '-----------------------------------------------
        StatusJurnal = 0
        AngkaNomorJVPembelian = 0
        txt_NomorPembelian.Text = Nothing
        txt_PO_Nomor.Text = Nothing
        dtp_TanggalPO.Value = Today
        txt_NomorSJBAST.Text = Nothing
        dtp_TanggalSJBAST.Value = Today
        txt_NomorInvoice.Text = Nothing
        dtp_TanggalInvoice.Value = Today
        dtp_TanggalTransaksi.Value = Today
        txt_KodeDokumen.Text = Nothing
        txt_KodeSupplier.Text = Nothing
        txt_NamaSupplier.Text = Nothing
        txt_NPWP.Text = Nothing
        txt_NomorFakturPajak.Text = Nothing
        dtp_TanggalFakturPajak.Value = Today
        txt_DPPPPN.Text = Nothing
        rdb_TarifPPN_Normal.Checked = False
        rdb_TarifPPN_Efektif.Checked = False
        txt_TarifPPN.Text = Nothing
        txt_TarifPPN.Enabled = False
        txt_JumlahPPN.Text = Nothing
        cmb_PPNDikreditkan.Items.Clear()
        cmb_PPNDikreditkan.Text = Nothing
        cmb_DibiayakanDikapitalisasi.Text = Nothing
        chk_NamaBarang1.Checked = False
        chk_NamaBarang2.Checked = False
        COABarang1 = Nothing
        COABarang2 = Nothing
        chk_NamaJasa1.Checked = False
        chk_NamaJasa2.Checked = False
        chk_NamaJasa3.Checked = False
        COAJasa1 = Nothing
        COAJasa2 = Nothing
        COAJasa3 = Nothing
        cmb_JenisPPh1.Items.Clear()
        cmb_JenisPPh1.Text = Nothing
        cmb_JenisPPh2.Items.Clear()
        cmb_JenisPPh2.Text = Nothing
        cmb_JenisPPh3.Items.Clear()
        cmb_JenisPPh3.Text = Nothing
        txt_KodeProject.Text = Nothing
        cmb_JenisPembelian.Text = Nothing
        txt_TotalDPP.Text = Nothing
        txt_PPNMasukan.Text = Nothing
        txt_JumlahTagihan.Text = Nothing
        dtp_DueDate.Value = Today
        KontenCombo_JenisPembelian()
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        Reset_grb_Bank()
        COAKredit = Nothing
        txt_BiayaTransportasiPembelian.Text = Nothing
        txt_Keterangan.Text = Nothing
        btn_Simpan.Text = "Simpan"
        btn_Simpan.Enabled = True
        txt_PO_Nomor.Focus()

        ProsesResetForm = False

    End Sub


    'Sistem penomoran untuk Nomor Pembelian :
    Private Sub SistemPenomoranOtomatis_Pembelian()
        'Dim Penomoran As String
        'Dim NomorID_UntukPenomoran
        'Dim AwalanPEMB_UntukPenomoran = AwalanPEMB & TahunInvoice.ToString & "-"
        'Penomoran = Nothing
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian " &
        '                      " WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_Pembelian) AND Nomor_Pembelian LIKE '" & AwalanPEMB_UntukPenomoran & "%' ",
        '                      KoneksiDatabaseTransaksi)
        'dr = cmd.ExecuteReader
        'dr.Read()
        'If dr.HasRows Then
        '    NomorID_UntukPenomoran = AmbilAngka(dr.Item("Nomor_ID")) + 1
        '    Penomoran = AwalanPEMB_UntukPenomoran & NomorID_UntukPenomoran.ToString
        'Else
        '    Penomoran = AwalanPEMB_UntukPenomoran & 1.ToString
        'End If
        'AksesDatabase_Transaksi(Tutup)
        'txt_NomorPembelian.Text = Penomoran
    End Sub

    Sub FilterListAkun()
        win_ListCOA.ListAkun = ListAkun_Semua
        'Untuk sementara belum ada filter list akun. Sementara semua ditampilkan dulu.
        'Suatu saat nanti harus ada filter.
        'Sengaja dibuat dari sekarang, untuk mewadahi dulu.
    End Sub

    Sub LogikaKenaikanTarifPajak()
        Dim TahunFakturPajak, BulanFakturPajak As Int64
        TahunFakturPajak = Year(dtp_TanggalFakturPajak.Value)
        BulanFakturPajak = Month(dtp_TanggalFakturPajak.Value)
        If rdb_TarifPPN_Normal.Checked = True Then
            If ((TahunFakturPajak = 2022 And BulanFakturPajak < 4) Or TahunFakturPajak < 2022) Then
                txt_TarifPPN.Text = 10
            Else
                txt_TarifPPN.Text = 11
            End If
            txt_TarifPPN.Enabled = False
        Else
            txt_TarifPPN.Enabled = True
        End If
    End Sub

    Sub LogikaJenisPPN()
        JenisPPN = Nothing
        If rdb_TarifPPN_Normal.Checked = True Then JenisPPN = "Normal"
        If rdb_TarifPPN_Efektif.Checked = True Then JenisPPN = "Efektif"
    End Sub

    Sub KontenCombo_PPNDikreditkan()
        cmb_PPNDikreditkan.Items.Clear()
        cmb_PPNDikreditkan.Items.Add(Keterangan_Ya)
        cmb_PPNDikreditkan.Items.Add(Keterangan_Tidak)
        If cmb_PPNDikreditkan.Text = Nothing Then
            cmb_PPNDikreditkan.Text = Keterangan_Ya
        End If
    End Sub

    Sub KontenCombo_DibiayakanDikapitalisasi()
        cmb_DibiayakanDikapitalisasi.Items.Clear()
        cmb_DibiayakanDikapitalisasi.Items.Add(PilihanPPN_Dibiayakan)
        cmb_DibiayakanDikapitalisasi.Items.Add(PilihanPPN_Dikapitalisasi)
        cmb_DibiayakanDikapitalisasi.Text = PilihanPPN_Dibiayakan
    End Sub

    Sub KontenCombo_JenisJasa1()
        cmb_JenisJasa1.Items.Clear()
        cmb_JenisJasa1.Items.Add(JenisJasa_JasaLainnya)
        cmb_JenisJasa1.Items.Add(JenisJasa_JasaKonstruksi)
        cmb_JenisJasa1.Items.Add(JenisJasa_SewaAssetSelainTanahBangunan)
        cmb_JenisJasa1.Items.Add(JenisJasa_SewaTanahDanAtauBangunan)
        cmb_JenisJasa1.Items.Add(JenisJasa_BungaBagiHasil)
        cmb_JenisJasa1.Items.Add(JenisJasa_Royalty)
        cmb_JenisJasa1.Items.Add(JenisJasa_Dividen)
        cmb_JenisJasa1.Items.Add(JenisJasa_Lainnya)
        cmb_JenisJasa1.Text = Nothing
    End Sub

    Sub KontenCombo_JenisJasa2()
        cmb_JenisJasa2.Items.Clear()
        cmb_JenisJasa2.Items.Add(JenisJasa_JasaLainnya)
        cmb_JenisJasa2.Items.Add(JenisJasa_JasaKonstruksi)
        cmb_JenisJasa2.Items.Add(JenisJasa_SewaAssetSelainTanahBangunan)
        cmb_JenisJasa2.Items.Add(JenisJasa_SewaTanahDanAtauBangunan)
        cmb_JenisJasa2.Items.Add(JenisJasa_BungaBagiHasil)
        cmb_JenisJasa2.Items.Add(JenisJasa_Royalty)
        cmb_JenisJasa2.Items.Add(JenisJasa_Dividen)
        cmb_JenisJasa2.Items.Add(JenisJasa_Lainnya)
        cmb_JenisJasa2.Text = Nothing
    End Sub

    Sub KontenCombo_JenisJasa3()
        cmb_JenisJasa3.Items.Clear()
        cmb_JenisJasa3.Items.Add(JenisJasa_JasaLainnya)
        cmb_JenisJasa3.Items.Add(JenisJasa_JasaKonstruksi)
        cmb_JenisJasa3.Items.Add(JenisJasa_SewaAssetSelainTanahBangunan)
        cmb_JenisJasa3.Items.Add(JenisJasa_SewaTanahDanAtauBangunan)
        cmb_JenisJasa3.Items.Add(JenisJasa_BungaBagiHasil)
        cmb_JenisJasa3.Items.Add(JenisJasa_Royalty)
        cmb_JenisJasa3.Items.Add(JenisJasa_Dividen)
        cmb_JenisJasa3.Items.Add(JenisJasa_Lainnya)
        cmb_JenisJasa3.Text = Nothing
    End Sub

    Sub KontenCombo_JenisPPh1()  'Konten ComboBox PPh Terutang 1 :
        cmb_JenisPPh1.Items.Clear()
        Select Case LokasiWP
            Case LokasiWP_DalamNegeri
                If cmb_JenisJasa1.Text = JenisJasa_JasaLainnya Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal21
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 2
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_JasaKonstruksi Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal42
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal42
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_SewaAssetSelainTanahBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 2
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_SewaTanahDanAtauBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal42
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal42
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 10
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_BungaBagiHasil Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_Royalty Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_Dividen Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = False
                        txt_TarifPPh1.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa1.Text = JenisJasa_Lainnya Then
                    cmb_JenisPPh1.Items.Add(JenisPPh_Pasal21)
                    cmb_JenisPPh1.Items.Add(JenisPPh_Pasal23)
                    cmb_JenisPPh1.Items.Add(JenisPPh_Pasal42)
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh1.Text = JenisPPh_Pasal23
                        txt_TarifPPh1.Enabled = True
                        txt_TarifPPh1.Text = Nothing
                    End If
                End If
            Case LokasiWP_LuarNegeri
                cmb_JenisPPh1.Text = "Pasal 26"
                txt_TarifPPh1.Enabled = True
                txt_TarifPPh1.Text = Nothing
        End Select
        If cmb_JenisJasa1.Text = Nothing Then
            cmb_JenisPPh1.Text = Nothing
            txt_TarifPPh1.Enabled = False
            txt_TarifPPh1.Text = Nothing
        End If
        If FungsiForm = "DETAIL" Then txt_TarifPPh1.Enabled = False
    End Sub

    Sub KontenCombo_JenisPPh2()  'Konten ComboBox PPh Terutang 2 :
        cmb_JenisPPh2.Items.Clear()
        Select Case LokasiWP
            Case LokasiWP_DalamNegeri
                If cmb_JenisJasa2.Text = JenisJasa_JasaLainnya Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal21
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 2
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_JasaKonstruksi Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal42
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal42
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_SewaAssetSelainTanahBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 2
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_SewaTanahDanAtauBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal42
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal42
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 10
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_BungaBagiHasil Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_Royalty Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_Dividen Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = False
                        txt_TarifPPh2.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa2.Text = JenisJasa_Lainnya Then
                    cmb_JenisPPh2.Items.Add(JenisPPh_Pasal21)
                    cmb_JenisPPh2.Items.Add(JenisPPh_Pasal23)
                    cmb_JenisPPh2.Items.Add(JenisPPh_Pasal42)
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh2.Text = JenisPPh_Pasal23
                        txt_TarifPPh2.Enabled = True
                        txt_TarifPPh2.Text = Nothing
                    End If
                End If
            Case LokasiWP_LuarNegeri
                cmb_JenisPPh2.Text = "Pasal 26"
                txt_TarifPPh2.Enabled = True
                txt_TarifPPh2.Text = Nothing
        End Select
        If cmb_JenisJasa2.Text = Nothing Then
            cmb_JenisPPh2.Text = Nothing
            txt_TarifPPh2.Enabled = False
            txt_TarifPPh2.Text = Nothing
        End If
        If FungsiForm = "DETAIL" Then txt_TarifPPh2.Enabled = False
    End Sub

    Sub KontenCombo_JenisPPh3()  'Konten ComboBox PPh Terutang 3 :
        cmb_JenisPPh3.Items.Clear()
        Select Case LokasiWP
            Case LokasiWP_DalamNegeri
                If cmb_JenisJasa3.Text = JenisJasa_JasaLainnya Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal21
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 2
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_JasaKonstruksi Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal42
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal42
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_SewaAssetSelainTanahBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 2
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_SewaTanahDanAtauBangunan Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal42
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal42
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 10
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_BungaBagiHasil Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_Royalty Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_Dividen Then
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = False
                        txt_TarifPPh3.Text = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                End If
                If cmb_JenisJasa3.Text = JenisJasa_Lainnya Then
                    cmb_JenisPPh3.Items.Add(JenisPPh_Pasal21)
                    cmb_JenisPPh3.Items.Add(JenisPPh_Pasal23)
                    cmb_JenisPPh3.Items.Add(JenisPPh_Pasal42)
                    If JenisWP = JenisWP_OrangPribadi Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        cmb_JenisPPh3.Text = JenisPPh_Pasal23
                        txt_TarifPPh3.Enabled = True
                        txt_TarifPPh3.Text = Nothing
                    End If
                End If
            Case LokasiWP_LuarNegeri
                cmb_JenisPPh3.Text = "Pasal 26"
                txt_TarifPPh3.Enabled = True
                txt_TarifPPh3.Text = Nothing
        End Select
        If cmb_JenisJasa3.Text = Nothing Then
            cmb_JenisPPh3.Text = Nothing
            txt_TarifPPh3.Enabled = False
            txt_TarifPPh3.Text = Nothing
        End If
        If FungsiForm = "DETAIL" Then txt_TarifPPh3.Enabled = False
    End Sub

    Sub KontenCombo_JenisPembelian()  'Konten ComboBox Jenis Pembelian :
        cmb_JenisPembelian.Items.Clear()
        cmb_JenisPembelian.Items.Add(JenisPembelian_Tempo)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then cmb_JenisPembelian.Text = JenisPembelian_Tempo
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            cmb_JenisPembelian.Items.Add(JenisPembelian_Tunai)
            cmb_JenisPembelian.Text = Nothing
        End If
    End Sub

    'Perhitungan JumlahPPN
    Sub PerhitunganJumlahPPN()
        JumlahPPN = DPPPPN * Persen(TarifPPN)
        txt_JumlahPPN.Text = JumlahPPN
        txt_PPNMasukan.Text = JumlahPPN
    End Sub

    'Perhitungan PPh Terutang 1
    Sub PerhitunganPPhTerutang1()
        PPhTerutang1 = DPPJasa1 * (TarifPPh1 / 100)
        txt_PPhTerutang1.Text = PPhTerutang1
        PerhitunganPPhDipotong1()
    End Sub

    'Perhitungan PPh Terutang 2
    Sub PerhitunganPPhTerutang2()
        PPhTerutang2 = DPPJasa2 * (TarifPPh2 / 100)
        txt_PPhTerutang2.Text = PPhTerutang2
        PerhitunganPPhDipotong2()
    End Sub

    'Perhitungan PPh Terutang 3
    Sub PerhitunganPPhTerutang3()
        PPhTerutang3 = DPPJasa3 * (TarifPPh3 / 100)
        txt_PPhTerutang3.Text = PPhTerutang3
        PerhitunganPPhDipotong3()
    End Sub

    'Perhitungan PPh Dipotong 1 :
    Sub PerhitunganPPhDipotong1()
        PPhDipotong1 = AmbilAngka(txt_PPhTerutang1.Text) - AmbilAngka(txt_PPhDitanggung1.Text)
        txt_PPhDipotong1.Text = PPhDipotong1
    End Sub

    'Perhitungan PPh Dipotong 2 :
    Sub PerhitunganPPhDipotong2()
        PPhDipotong2 = AmbilAngka(txt_PPhTerutang2.Text) - AmbilAngka(txt_PPhDitanggung2.Text)
        txt_PPhDipotong2.Text = PPhDipotong2
    End Sub

    'Perhitungan PPh Dipotong 3 :
    Sub PerhitunganPPhDipotong3()
        PPhDipotong3 = AmbilAngka(txt_PPhTerutang3.Text) - AmbilAngka(txt_PPhDitanggung3.Text)
        txt_PPhDipotong3.Text = PPhDipotong3
    End Sub

    'Perhitungan Total PPh Ditanggung
    Sub PerhitunganJumlahPPhDitanggung()
        JumlahPPhDitanggung = AmbilAngka(txt_PPhDitanggung1.Text) + AmbilAngka(txt_PPhDitanggung2.Text) + AmbilAngka(txt_PPhDitanggung3.Text)
        txt_JumlahPPhDitanggung.Text = JumlahPPhDitanggung
    End Sub

    'Perhitungan Jumlah DPP Barang
    Sub PerhitunganJumlahDPPBarang()
        DPPBarang1 = AmbilAngka(txt_DPPBarang1.Text)
        DPPBarang2 = AmbilAngka(txt_DPPBarang2.Text)
        JumlahDPPBarang = DPPBarang1 + DPPBarang2
        txt_JumlahDPPBarang.Text = JumlahDPPBarang
        PerhitunganTotalDPP()
    End Sub

    'Perhitungan Jumlah DPP Jasa
    Sub PerhitunganJumlahDPPJasa()
        JumlahDPPJasa = DPPJasa1 + DPPJasa2 + DPPJasa3
        txt_JumlahDPPJasa.Text = JumlahDPPJasa
        PerhitunganTotalDPP()
    End Sub

    'Perhitungan Total DPP
    Sub PerhitunganTotalDPP()
        TotalDPP = JumlahDPPBarang + JumlahDPPJasa
        txt_TotalDPP.Text = TotalDPP
    End Sub

    'Perhitungan Jumlah PPh Terutang dan Jumlah Tagihan
    Sub PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
        DPPPPN = AmbilAngka(txt_DPPPPN.Text)
        JumlahPPN = AmbilAngka(txt_JumlahPPN.Text)
        'PPhTerutang1 = AmbilAngka(txt_PPhTerutang1.Text)                   'Baris-baris ini sebetulnya sudah tidak diperlukan.
        'PPhTerutang2 = AmbilAngka(txt_PPhTerutang2.Text)                   'Tapi ga apa-apa biarkan dulu, ...
        'PPhTerutang3 = AmbilAngka(txt_PPhTerutang3.Text)                   '... karena khawatir ada masalah kalau dihapus.
        'JumlahPPhDitanggung = AmbilAngka(txt_JumlahPPhDitanggung.Text)     'Kalau nanti sudah terbukti tidak ada masalah, ya hapus saja nanti.
        JumlahPPhTerutang = PPhTerutang1 + PPhTerutang2 + PPhTerutang3
        JumlahPPhDipotong = JumlahPPhTerutang - JumlahPPhDitanggung
        JumlahHutangUsaha = TotalDPP + JumlahPPN + BiayaTransportasiPembelian + JumlahPPhDitanggung
        JumlahTagihan = TotalDPP + JumlahPPN + BiayaTransportasiPembelian - JumlahPPhDipotong
        txt_JumlahPPhTerutang.Text = JumlahPPhTerutang
        txt_JumlahPPhDipotong.Text = JumlahPPhDipotong
        txt_JumlahTagihan.Text = JumlahTagihan
    End Sub

    Private Sub txt_NomorPembelian_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorPembelian.TextChanged
        NomorPembelian = txt_NomorPembelian.Text
    End Sub

    Private Sub txt_PO_Nomor_TextChanged(sender As Object, e As EventArgs) Handles txt_PO_Nomor.TextChanged
        NomorPO = txt_PO_Nomor.Text
    End Sub
    Private Sub txt_PO_Nomor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PO_Nomor.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_PO_Nomor_Click(sender As Object, e As EventArgs) Handles txt_PO_Nomor.Click
        btn_PilihPO_Click(sender, e)
    End Sub

    Private Sub btn_PilihPO_Click(sender As Object, e As EventArgs) Handles btn_PilihPO.Click
        win_ListPO = New wpfWin_ListPO
        win_ListPO.ResetForm()
        win_ListPO.Sisi = win_ListPO.Sisi_POPembelian
        win_ListPO.ShowDialog()
        If Not String.IsNullOrEmpty(win_ListPO.NomorPO_Terseleksi) Then
            txt_PO_Nomor.Text = win_ListPO.NomorPO_Terseleksi
            dtp_TanggalPO.Text = win_ListPO.TanggalPO_Terseleksi
            txt_KodeProject.Text = win_ListPO.KodeProject_Terseleksi
            txt_KodeSupplier.Text = win_ListPO.KodeMitra_Terseleksi
            txt_NamaSupplier.Text = win_ListPO.NamaMitra_Terseleksi
        End If
    End Sub

    Private Sub dtp_TanggalPO_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPO.ValueChanged
        TanggalPO = dtp_TanggalPO.Value
    End Sub

    Private Sub txt_NomorSJBAST_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorSJBAST.TextChanged
        NomorSJBAST = txt_NomorSJBAST.Text
    End Sub

    Private Sub dtp_TanggalSJBAST_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalSJBAST.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalSJBAST)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalSJBAST)
        TanggalSJBAST = dtp_TanggalSJBAST.Value
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub
    Private Sub txt_NomorInvoice_Leave(sender As Object, e As EventArgs) Handles txt_NomorInvoice.Leave
        If FungsiForm = FungsiForm_TAMBAH And txt_NomorInvoice.Text <> Nothing Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand("SELECT * FROM tbl_Pembelian WHERE Nomor_Invoice = '" & txt_NomorInvoice.Text & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Nomor Invoice sudah ada. Silakan masukkan Nomor Invoice yang lain..!")
                txt_NomorInvoice.Clear()
                txt_NomorInvoice.Focus()
            End If
            AksesDatabase_Transaksi(Tutup)
        End If
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalInvoice)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalInvoice)
        TanggalInvoice = dtp_TanggalInvoice.Value
        TahunInvoice = dtp_TanggalInvoice.Value.Year
        If FungsiForm = FungsiForm_TAMBAH Then SistemPenomoranOtomatis_Pembelian()
    End Sub
    Private Sub dtp_TanggalInvoice_Leave(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.Leave
        If rdb_SamaDenganTanggalInvoice.Checked = True Then
            dtp_TanggalTransaksi.Value = dtp_TanggalInvoice.Value
        End If
    End Sub

    Private Sub rdb_SamaDenganTanggalInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_SamaDenganTanggalInvoice.CheckedChanged
        If rdb_SamaDenganTanggalInvoice.Checked = True Then
            dtp_TanggalTransaksi.Enabled = False
            dtp_TanggalTransaksi.Value = dtp_TanggalInvoice.Value
            TanggalTransaksiSamaDenganTanggalInvoice = True
        End If
    End Sub

    Private Sub rdb_TidakSama_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_TidakSama.CheckedChanged
        If rdb_TidakSama.Checked = True Then
            dtp_TanggalTransaksi.Enabled = True
            TanggalTransaksiSamaDenganTanggalInvoice = False
        End If
    End Sub

    Private Sub grb_TanggalTransaksi_Enter(sender As Object, e As EventArgs) Handles grb_TanggalTransaksi.Enter
    End Sub
    Private Sub grb_TanggalTransaksi_Leave(sender As Object, e As EventArgs) Handles grb_TanggalTransaksi.Leave
        'Tanggal Transaksi tidak mungkin melebihi Tanggal Invoice
        If dtp_TanggalTransaksi.Value > dtp_TanggalInvoice.Value Then
            MsgBox("'Tanggal Transaksi' melebihi 'Tanggal Invoice'." & Enter2Baris & " Silakan koreksi kembali.")
            dtp_TanggalTransaksi.Focus()
            Return
        End If
        If dtp_TanggalTransaksi.Value = dtp_TanggalInvoice.Value Then
            rdb_SamaDenganTanggalInvoice.Checked = True
        End If
    End Sub
    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalTransaksi)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalTransaksi)
        TanggalTransaksi = dtp_TanggalTransaksi.Value
        If TanggalTransaksi = TanggalInvoice Then
            rdb_SamaDenganTanggalInvoice.Checked = True
        Else
            rdb_TidakSama.Checked = True
        End If
        TahunTransaksi = dtp_TanggalTransaksi.Value.Year
    End Sub

    Private Sub txt_KodeDokumen_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeDokumen.TextChanged
        KodeDokumen = txt_KodeDokumen.Text
    End Sub

    Private Sub txt_KodeSupplier_Click(sender As Object, e As EventArgs) Handles txt_KodeSupplier.Click
        'btn_PilihMitra_Click(sender, e)
        'Sementara dinon-aktifkan dulu. Karena pemilihan Kode Supplier saat ini otomatis diambil dari Input PO.
    End Sub
    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeSupplier & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            JenisWP = dr.Item("Jenis_WP")
            LokasiWP = dr.Item("Lokasi_WP")
            txt_NamaSupplier.Text = dr.Item("Nama_Mitra")
            txt_NPWP.Text = dr.Item("NPWP")
        End If
        AksesDatabase_General(Tutup)
        If chk_NamaJasa1.Checked = True Then KontenCombo_JenisPPh1()
        If chk_NamaJasa2.Checked = True Then KontenCombo_JenisPPh2()
        If chk_NamaJasa3.Checked = True Then KontenCombo_JenisPPh3()
        If LokasiWP = LokasiWP_DalamNegeri Then
            COABiayaTransportasiPembelian = KodeTautanCOA_BiayaTransportasiPembelianBb_Lokal
            COAPPN = KodeTautanCOA_PPNMasukan_Lokal
        End If
        If LokasiWP = LokasiWP_LuarNegeri Then
            COABiayaTransportasiPembelian = KodeTautanCOA_BiayaTransportasi_Import
            COAPPN = KodeTautanCOA_PPNMasukan_Impor
        End If
    End Sub
    Private Sub txt_KodeSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Mitra_Supplier
        If txt_KodeSupplier.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeSupplier.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaSupplier.Text
            win_ListLawanTransaksi.NPWPTerseleksi = txt_NPWP.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeSupplier.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaSupplier.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
        txt_NPWP.Text = win_ListLawanTransaksi.NPWPTerseleksi
    End Sub


    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_NamaSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NPWP_TextChanged(sender As Object, e As EventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
    End Sub
    Private Sub txt_NPWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NPWP.KeyPress
        KunciTotalInputan(sender, e)
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
        If NomorFakturPajak = Nothing Then
            dtp_TanggalFakturPajak.Enabled = False
            dtp_TanggalFakturPajak.Visible = False
            rdb_TarifPPN_Normal.Enabled = False
            rdb_TarifPPN_Normal.Checked = False
            rdb_TarifPPN_Efektif.Enabled = False
            rdb_TarifPPN_Efektif.Checked = False
            txt_TarifPPN.Enabled = False
            txt_TarifPPN.Text = Nothing
            cmb_PPNDikreditkan.Items.Clear()
            cmb_PPNDikreditkan.Text = Nothing
            cmb_PPNDikreditkan.Enabled = False
            JenisPPN = Nothing
        Else
            If FungsiForm = "DETAIL" Then
                dtp_TanggalFakturPajak.Enabled = False
                dtp_TanggalFakturPajak.Visible = False
                rdb_TarifPPN_Normal.Enabled = False
                rdb_TarifPPN_Normal.Checked = False
                rdb_TarifPPN_Efektif.Enabled = False
                rdb_TarifPPN_Efektif.Checked = False
                txt_TarifPPN.Enabled = False
            Else
                dtp_TanggalFakturPajak.Enabled = True
                dtp_TanggalFakturPajak.Visible = True
                rdb_TarifPPN_Normal.Enabled = True
                rdb_TarifPPN_Efektif.Enabled = True
                If rdb_TarifPPN_Efektif.Checked = False Then
                    rdb_TarifPPN_Normal.Checked = True
                End If
                cmb_PPNDikreditkan.Enabled = True
                KontenCombo_PPNDikreditkan()
            End If
        End If
        If TanggalFormatTampilan(TanggalFakturPajak) = TanggalKosong Then
            EksekusiKode = False
            dtp_TanggalFakturPajak.Value = TanggalInvoice
            EksekusiKode = True
        End If
    End Sub
    Private Sub txt_NomorFakturPajak_Leave(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.Leave
        If FungsiForm = FungsiForm_TAMBAH And txt_NomorFakturPajak.Text <> Nothing Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand("SELECT * FROM tbl_Pembelian WHERE Nomor_Faktur_Pajak = '" & txt_NomorFakturPajak.Text & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Nomor Faktur Pajak sudah pernah diinput. Silakan masukkan Nomor Faktur Pajak yang lain..!")
                txt_NomorFakturPajak.Clear()
                txt_NomorFakturPajak.Focus()
            End If
            AksesDatabase_Transaksi(Tutup)
        End If
    End Sub

    Private Sub dtp_TanggalFakturPajak_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalFakturPajak.ValueChanged
        If TanggalFormatTampilan(dtp_TanggalFakturPajak.Value) <> TanggalKosong Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalFakturPajak)
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalFakturPajak)
        End If
        TanggalFakturPajak = dtp_TanggalFakturPajak.Value
        If rdb_TarifPPN_Normal.Checked = True Then
            LogikaKenaikanTarifPajak()
        End If
    End Sub

    Private Sub txt_DPPPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPPPN.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPPPN_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPPPN.TextChanged
        DPPPPN = AmbilAngka(txt_DPPPPN.Text)
        PemecahRibuanUntukTextBox(txt_DPPPPN)
        PerhitunganJumlahPPN()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub rdb_TarifPPN_Normal_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_TarifPPN_Normal.CheckedChanged
        LogikaJenisPPN()
        LogikaKenaikanTarifPajak()
    End Sub

    Private Sub rdb_TarifPPN_Efektif_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_TarifPPN_Efektif.CheckedChanged
        LogikaJenisPPN()
        txt_TarifPPN.Enabled = True
    End Sub

    Private Sub txt_TarifPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPN.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
    End Sub
    Private Sub txt_TarifPPN_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPN.TextChanged
        If txt_TarifPPN.Text = "," Then
            txt_TarifPPN.Text = Kosongan
            Return
        End If
        If txt_TarifPPN.Text = Kosongan Then
            TarifPPN = 0
        Else
            TarifPPN = txt_TarifPPN.Text
        End If
        PerhitunganJumlahPPN()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_JumlahPPN_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPN.TextChanged
        JumlahPPN = AmbilAngka(txt_JumlahPPN.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPPN)
    End Sub

    Private Sub cmb_PPNDikreditkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PPNDikreditkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PPNDikreditkan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PPNDikreditkan.SelectedIndexChanged
    End Sub
    Private Sub cmb_PPNDikreditkan_TextChanged(sender As Object, e As EventArgs) Handles cmb_PPNDikreditkan.TextChanged
        PPNDikreditkan = cmb_PPNDikreditkan.Text
        If PPNDikreditkan = Keterangan_Ya Or PPNDikreditkan = Kosongan Then
            DibiayakanDikapitalisasi = Kosongan
            cmb_DibiayakanDikapitalisasi.Visible = False
        Else
            KontenCombo_DibiayakanDikapitalisasi()
            cmb_DibiayakanDikapitalisasi.Visible = True
        End If
    End Sub

    Private Sub cmb_DibiayakanDikapitalisasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DibiayakanDikapitalisasi.SelectedIndexChanged
    End Sub
    Private Sub cmb_DibiayakanDikapitalisasi_TextChanged(sender As Object, e As EventArgs) Handles cmb_DibiayakanDikapitalisasi.TextChanged
        If PPNDikreditkan = Keterangan_Tidak Then
            DibiayakanDikapitalisasi = cmb_DibiayakanDikapitalisasi.Text
        Else
            DibiayakanDikapitalisasi = Kosongan
        End If
    End Sub

    Private Sub chk_NamaBarang1_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NamaBarang1.CheckedChanged
        If chk_NamaBarang1.Checked = True Then
            txt_NamaBarang1.Enabled = True
            txt_NamaAkunBarang1.Enabled = True
            btn_PilihCOABarang1.Enabled = True
            txt_DPPBarang1.Enabled = True
        Else
            txt_NamaBarang1.Enabled = False
            txt_NamaBarang1.Text = Nothing
            txt_NamaAkunBarang1.Enabled = False
            txt_NamaAkunBarang1.Text = Nothing
            btn_PilihCOABarang1.Enabled = False
            txt_DPPBarang1.Enabled = False
            txt_DPPBarang1.Text = Nothing
        End If
        PerhitunganJumlahDPPBarang()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_NamaBarang1_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaBarang1.TextChanged
        NamaBarang1 = txt_NamaBarang1.Text
    End Sub

    Private Sub txt_NamaAkunBarang1_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunBarang1.TextChanged
        NamaAkunBarang1 = txt_NamaAkunBarang1.Text
    End Sub
    Private Sub txt_NamaAkunBarang1_Click(sender As Object, e As EventArgs) Handles txt_NamaAkunBarang1.Click
        btn_PilihCOABarang1_Click(sender, e)
    End Sub
    Private Sub txt_NamaAkunBarang1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunBarang1.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOABarang1_Click(sender As Object, e As EventArgs) Handles btn_PilihCOABarang1.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        FilterListAkun()
        If txt_NamaAkunBarang1.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COABarang1
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunBarang1.Text
        End If
        win_ListCOA.ShowDialog()
        COABarang1 = win_ListCOA.COATerseleksi
        txt_NamaAkunBarang1.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_DPPBarang1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPBarang1.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPBarang1_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPBarang1.TextChanged
        DPPBarang1 = AmbilAngka(txt_DPPBarang1.Text)
        PemecahRibuanUntukTextBox(txt_DPPBarang1)
        PerhitunganJumlahDPPBarang()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub chk_NamaBarang2_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NamaBarang2.CheckedChanged
        If chk_NamaBarang1.Checked = False Then
            chk_NamaBarang2.Checked = False
        End If
        If chk_NamaBarang2.Checked = True Then
            txt_NamaBarang2.Enabled = True
            txt_NamaAkunBarang2.Enabled = True
            btn_PilihCOABarang2.Enabled = True
            txt_DPPBarang2.Enabled = True
        Else
            txt_NamaBarang2.Enabled = False
            txt_NamaBarang2.Text = Nothing
            txt_NamaAkunBarang2.Enabled = False
            txt_NamaAkunBarang2.Text = Nothing
            btn_PilihCOABarang2.Enabled = False
            txt_JumlahDPPBarang.Text = Nothing
            txt_DPPBarang2.Enabled = False
            txt_DPPBarang2.Text = Nothing
        End If
        PerhitunganJumlahDPPBarang()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_NamaBarang2_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaBarang2.TextChanged
        NamaBarang2 = txt_NamaBarang2.Text
    End Sub

    Private Sub txt_NamaAkunBarang2_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunBarang2.TextChanged
        NamaAkunBarang2 = txt_NamaAkunBarang2.Text
    End Sub
    Private Sub txt_NamaAkunBarang2_Click(sender As Object, e As EventArgs) Handles txt_NamaAkunBarang2.Click
        btn_PilihCOABarang2_Click(sender, e)
    End Sub
    Private Sub txt_NamaAkunBarang2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunBarang2.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub btn_PilihCOABarang2_Click(sender As Object, e As EventArgs) Handles btn_PilihCOABarang2.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        FilterListAkun()
        If txt_NamaAkunBarang2.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COABarang2
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunBarang2.Text
        End If
        win_ListCOA.ShowDialog()
        COABarang2 = win_ListCOA.COATerseleksi
        txt_NamaAkunBarang2.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_DPPBarang2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPBarang2.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPBarang2_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPBarang2.TextChanged
        DPPBarang2 = AmbilAngka(txt_DPPBarang2.Text)
        PemecahRibuanUntukTextBox(txt_DPPBarang2)
        PerhitunganJumlahDPPBarang()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub chk_NamaJasa1_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NamaJasa1.CheckedChanged
        If chk_NamaJasa1.Checked = True Then
            If txt_KodeSupplier.Text = Nothing Then
                MsgBox("Silakan isi terlebih dahulu kolom 'Kode Supplier'.")
                chk_NamaJasa1.Checked = False
                txt_KodeSupplier.Focus()
                Return
            End If
            txt_NamaJasa1.Enabled = True
            txt_NamaAkunJasa1.Enabled = True
            btn_PilihCOAJasa1.Enabled = True
            txt_DPPJasa1.Enabled = True
            KontenCombo_JenisJasa1()
            cmb_JenisJasa1.Enabled = True
            KontenCombo_JenisPPh1()
            cmb_JenisPPh1.Enabled = True
            'txt_TarifPPh1.Enabled = True
        Else
            txt_NamaJasa1.Enabled = False
            txt_NamaJasa1.Text = Nothing
            txt_NamaAkunJasa1.Enabled = False
            txt_NamaAkunJasa1.Text = Nothing
            btn_PilihCOAJasa1.Enabled = False
            txt_DPPJasa1.Enabled = False
            txt_DPPJasa1.Text = Nothing
            cmb_JenisJasa1.Enabled = False
            cmb_JenisJasa1.Items.Clear()
            cmb_JenisJasa1.Text = Nothing
            cmb_JenisPPh1.Enabled = False
            cmb_JenisPPh1.Items.Clear()
            cmb_JenisPPh1.Text = Nothing
            txt_TarifPPh1.Enabled = False
            txt_TarifPPh1.Text = Nothing
            txt_PPhTerutang1.Enabled = False
            txt_PPhTerutang1.Text = Nothing
            txt_PPhDitanggung1.Enabled = False
            txt_PPhDitanggung1.Text = Nothing
        End If
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_NamaJasa1_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaJasa1.TextChanged
        NamaJasa1 = txt_NamaJasa1.Text
    End Sub

    Private Sub txt_NamaAkunJasa1_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa1.TextChanged
        NamaAkunJasa1 = txt_NamaAkunJasa1.Text
    End Sub
    Private Sub txt_NamaAkunJasa1_Click(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa1.Click
        btn_PilihCOAJasa1_Click(sender, e)
    End Sub
    Private Sub txt_NamaAkunJasa1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunJasa1.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOAJasa1_Click(sender As Object, e As EventArgs) Handles btn_PilihCOAJasa1.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        FilterListAkun()
        If txt_NamaAkunJasa1.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COAJasa1
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunJasa1.Text
        End If
        win_ListCOA.ShowDialog()
        COAJasa1 = win_ListCOA.COATerseleksi
        txt_NamaAkunJasa1.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_DPPJasa1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPJasa1.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPJasa1_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPJasa1.TextChanged
        DPPJasa1 = AmbilAngka(txt_DPPJasa1.Text)
        PemecahRibuanUntukTextBox(txt_DPPJasa1)
        PerhitunganJumlahDPPJasa()
        PerhitunganPPhTerutang1()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub cmb_JenisJasa1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa1.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJasa1_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa1.TextChanged
        JenisJasa1 = cmb_JenisJasa1.Text
        KontenCombo_JenisPPh1()
    End Sub
    Private Sub cmb_JenisJasa1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJasa1.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPh1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPh1.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPPh1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh1.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPh1_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh1.TextChanged
        JenisPPh1 = cmb_JenisPPh1.Text
    End Sub

    Private Sub txt_TarifPPh1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh1.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
    End Sub
    Private Sub txt_TarifPPh1_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPh1.TextChanged
        If txt_TarifPPh1.Text = "," Then
            txt_TarifPPh1.Text = Kosongan
            Return
        End If
        If txt_TarifPPh1.Text = Kosongan Then
            TarifPPh1 = 0
        Else
            TarifPPh1 = txt_TarifPPh1.Text
        End If
        PerhitunganPPhTerutang1()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_PPhTerutang1_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang1.TextChanged
        PPhTerutang1 = AmbilAngka(txt_PPhTerutang1.Text)
        PemecahRibuanUntukTextBox(txt_PPhTerutang1)
        If txt_PPhTerutang1.Text <> Nothing Then
            txt_PPhDitanggung1.Enabled = True
        Else
            txt_PPhDitanggung1.Enabled = False
            txt_PPhDitanggung1.Text = Nothing
        End If
    End Sub

    Private Sub txt_PPhDitanggung1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung1.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDitanggung1_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung1.TextChanged
        PPhDitanggung1 = AmbilAngka(txt_PPhDitanggung1.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung1)
        If PPhDitanggung1 > PPhTerutang1 Then
            MsgBox("Silakan isi kolom PPh Ditanggung dengan benar..!")
            txt_PPhDitanggung1.Clear()
            txt_PPhDitanggung1.Focus()
        End If
        PerhitunganPPhDipotong1()
        PerhitunganJumlahPPhDitanggung()
    End Sub

    Private Sub txt_PPhDipotong1_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong1.TextChanged
        PPhDipotong1 = AmbilAngka(txt_PPhDipotong1.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong1)
    End Sub

    Private Sub chk_NamaJasa2_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NamaJasa2.CheckedChanged
        If chk_NamaJasa1.Checked = False Then
            chk_NamaJasa2.Checked = False
        End If
        If chk_NamaJasa2.Checked = True Then
            If txt_KodeSupplier.Text = Nothing Then
                MsgBox("Silakan isi terlebih dahulu kolom 'Kode Supplier'.")
                chk_NamaJasa2.Checked = False
                txt_KodeSupplier.Focus()
                Return
            End If
            txt_NamaJasa2.Enabled = True
            txt_NamaAkunJasa2.Enabled = True
            btn_PilihCOAJasa2.Enabled = True
            txt_DPPJasa2.Enabled = True
            KontenCombo_JenisJasa2()
            cmb_JenisJasa2.Enabled = True
            KontenCombo_JenisPPh2()
            cmb_JenisPPh2.Enabled = True
            'txt_TarifPPh2.Enabled = True
        Else
            txt_NamaJasa2.Enabled = False
            txt_NamaJasa2.Text = Nothing
            txt_NamaAkunJasa2.Enabled = False
            txt_NamaAkunJasa2.Text = Nothing
            btn_PilihCOAJasa2.Enabled = False
            txt_DPPJasa2.Enabled = False
            txt_DPPJasa2.Text = Nothing
            cmb_JenisJasa2.Enabled = False
            cmb_JenisJasa2.Items.Clear()
            cmb_JenisJasa2.Text = Nothing
            cmb_JenisPPh2.Enabled = False
            cmb_JenisPPh2.Items.Clear()
            cmb_JenisPPh2.Text = Nothing
            txt_TarifPPh2.Enabled = False
            txt_TarifPPh2.Text = Nothing
            txt_PPhTerutang2.Enabled = False
            txt_PPhTerutang2.Text = Nothing
            txt_PPhDitanggung2.Enabled = False
            txt_PPhDitanggung2.Text = Nothing
        End If
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_NamaJasa2_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaJasa2.TextChanged
        NamaJasa2 = txt_NamaJasa2.Text
    End Sub

    Private Sub txt_NamaAkunJasa2_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa2.TextChanged
        NamaAkunJasa2 = txt_NamaAkunJasa2.Text
    End Sub
    Private Sub txt_NamaAkunJasa2_Click(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa2.Click
        btn_PilihCOAJasa2_Click(sender, e)
    End Sub
    Private Sub txt_NamaAkunJasa2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunJasa2.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOAJasa2_Click(sender As Object, e As EventArgs) Handles btn_PilihCOAJasa2.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        FilterListAkun()
        If txt_NamaAkunJasa2.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COAJasa2
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunJasa2.Text
        End If
        win_ListCOA.ShowDialog()
        COAJasa2 = win_ListCOA.COATerseleksi
        txt_NamaAkunJasa2.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_DPPJasa2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPJasa2.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPJasa2_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPJasa2.TextChanged
        DPPJasa2 = AmbilAngka(txt_DPPJasa2.Text)
        PemecahRibuanUntukTextBox(txt_DPPJasa2)
        PerhitunganJumlahDPPJasa()
        PerhitunganPPhTerutang2()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub cmb_JenisJasa2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa2.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJasa2_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa2.TextChanged
        JenisJasa2 = cmb_JenisJasa2.Text
        KontenCombo_JenisPPh2()
    End Sub
    Private Sub cmb_JenisJasa2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJasa2.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPh2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPh2.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPPh2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh2.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPh2_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh2.TextChanged
        JenisPPh2 = cmb_JenisPPh2.Text
    End Sub

    Private Sub txt_TarifPPh2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh2.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
    End Sub
    Private Sub txt_TarifPPh2_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPh2.TextChanged
        If txt_TarifPPh2.Text = "," Then
            txt_TarifPPh2.Text = Kosongan
            Return
        End If
        If txt_TarifPPh2.Text = Kosongan Then
            TarifPPh2 = 0
        Else
            TarifPPh2 = txt_TarifPPh2.Text
        End If
        PerhitunganPPhTerutang2()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_PPhTerutang2_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang2.TextChanged
        PPhTerutang2 = AmbilAngka(txt_PPhTerutang2.Text)
        PemecahRibuanUntukTextBox(txt_PPhTerutang2)
        If txt_PPhTerutang2.Text <> Nothing Then
            txt_PPhDitanggung2.Enabled = True
        Else
            txt_PPhDitanggung2.Enabled = False
            txt_PPhDitanggung2.Text = Nothing
        End If
    End Sub

    Private Sub txt_PPhDitanggung2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung2.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDitanggung2_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung2.TextChanged
        PPhDitanggung2 = AmbilAngka(txt_PPhDitanggung2.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung2)
        If PPhDitanggung2 > PPhTerutang2 Then
            MsgBox("Silakan isi kolom PPh Ditanggung dengan benar..!")
            txt_PPhDitanggung2.Clear()
            txt_PPhDitanggung2.Focus()
        End If
        PerhitunganPPhDipotong2()
        PerhitunganJumlahPPhDitanggung()
    End Sub

    Private Sub txt_PPhDipotong2_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong2.TextChanged
        PPhDipotong2 = AmbilAngka(txt_PPhDipotong2.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong2)
    End Sub

    Private Sub chk_NamaJasa3_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NamaJasa3.CheckedChanged
        If chk_NamaJasa2.Checked = False Then
            chk_NamaJasa3.Checked = False
        End If
        If chk_NamaJasa3.Checked = True Then
            If txt_KodeSupplier.Text = Nothing Then
                MsgBox("Silakan isi terlebih dahulu kolom 'Kode Supplier'.")
                chk_NamaJasa3.Checked = False
                txt_KodeSupplier.Focus()
                Return
            End If
            txt_NamaJasa3.Enabled = True
            txt_NamaAkunJasa3.Enabled = True
            btn_PilihCOAJasa3.Enabled = True
            txt_DPPJasa3.Enabled = True
            KontenCombo_JenisJasa3()
            cmb_JenisJasa3.Enabled = True
            KontenCombo_JenisPPh3()
            cmb_JenisPPh3.Enabled = True
            'txt_TarifPPh3.Enabled = True
        Else
            txt_NamaJasa3.Enabled = False
            txt_NamaJasa3.Text = Nothing
            txt_NamaAkunJasa3.Enabled = False
            txt_NamaAkunJasa3.Text = Nothing
            btn_PilihCOAJasa3.Enabled = False
            txt_DPPJasa3.Enabled = False
            txt_DPPJasa3.Text = Nothing
            cmb_JenisJasa3.Enabled = False
            cmb_JenisJasa3.Items.Clear()
            cmb_JenisJasa3.Text = Nothing
            cmb_JenisPPh3.Enabled = False
            cmb_JenisPPh3.Items.Clear()
            cmb_JenisPPh3.Text = Nothing
            txt_TarifPPh3.Enabled = False
            txt_TarifPPh3.Text = Nothing
            txt_PPhTerutang3.Enabled = False
            txt_PPhTerutang3.Text = Nothing
            txt_PPhDitanggung3.Enabled = False
            txt_PPhDitanggung3.Text = Nothing
        End If
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_NamaJasa3_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaJasa3.TextChanged
        NamaJasa3 = txt_NamaJasa3.Text
    End Sub

    Private Sub txt_NamaAkunJasa3_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa3.TextChanged
        NamaAkunJasa3 = txt_NamaAkunJasa3.Text
    End Sub
    Private Sub txt_NamaAkunJasa3_Click(sender As Object, e As EventArgs) Handles txt_NamaAkunJasa3.Click
        btn_PilihCOAJasa3_Click(sender, e)
    End Sub
    Private Sub txt_NamaAkunJasa3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunJasa3.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOAJasa3_Click(sender As Object, e As EventArgs) Handles btn_PilihCOAJasa3.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        FilterListAkun()
        If txt_NamaAkunJasa3.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COAJasa3
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunJasa3.Text
        End If
        win_ListCOA.ShowDialog()
        COAJasa3 = win_ListCOA.COATerseleksi
        txt_NamaAkunJasa3.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_DPPJasa3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPPJasa3.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPPJasa3_TextChanged(sender As Object, e As EventArgs) Handles txt_DPPJasa3.TextChanged
        DPPJasa3 = AmbilAngka(txt_DPPJasa3.Text)
        PemecahRibuanUntukTextBox(txt_DPPJasa3)
        PerhitunganJumlahDPPJasa()
        PerhitunganPPhTerutang3()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub cmb_JenisJasa3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa3.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJasa3_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJasa3.TextChanged
        JenisJasa3 = cmb_JenisJasa3.Text
        KontenCombo_JenisPPh3()
    End Sub
    Private Sub cmb_JenisJasa3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJasa3.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPh3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPh3.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPPh3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh3.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPh3_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh3.TextChanged
        JenisPPh3 = cmb_JenisPPh3.Text
    End Sub

    Private Sub txt_TarifPPh3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh3.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
    End Sub
    Private Sub txt_TarifPPh3_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPh3.TextChanged
        If txt_TarifPPh3.Text = "," Then
            txt_TarifPPh3.Text = Kosongan
            Return
        End If
        If txt_TarifPPh3.Text = Kosongan Then
            TarifPPh3 = 0
        Else
            TarifPPh3 = txt_TarifPPh3.Text
        End If
        PerhitunganPPhTerutang3()
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_PPhTerutang3_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang3.TextChanged
        PPhTerutang3 = txt_PPhTerutang3.Text
        PemecahRibuanUntukTextBox(txt_PPhTerutang3)
        If txt_PPhTerutang3.Text <> Nothing Then
            txt_PPhDitanggung3.Enabled = True
        Else
            txt_PPhDitanggung3.Enabled = False
            txt_PPhDitanggung3.Text = Nothing
        End If
    End Sub

    Private Sub txt_PPhDitanggung3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung3.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDitanggung3_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung3.TextChanged
        PPhDitanggung3 = AmbilAngka(txt_PPhDitanggung3.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung3)
        If PPhDitanggung3 > PPhTerutang3 Then
            MsgBox("Silakan isi kolom PPh Ditanggung dengan benar..!")
            txt_PPhDitanggung3.Clear()
            txt_PPhDitanggung3.Focus()
        End If
        PerhitunganPPhDipotong3()
        PerhitunganJumlahPPhDitanggung()
    End Sub

    Private Sub txt_PPhDipotong3_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong3.TextChanged
        PPhDipotong3 = AmbilAngka(txt_PPhDipotong3.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong3)
    End Sub

    Private Sub txt_JumlahDPPBarang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDPPBarang.TextChanged
        JumlahDPPBarang = AmbilAngka(txt_JumlahDPPBarang.Text)
        PemecahRibuanUntukTextBox(txt_JumlahDPPBarang)
    End Sub

    Private Sub txt_JumlahDPPJasa_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDPPJasa.TextChanged
        JumlahDPPJasa = AmbilAngka(txt_JumlahDPPJasa.Text)
        PemecahRibuanUntukTextBox(txt_JumlahDPPJasa)
        PerhitunganJumlahDPPBarang()
    End Sub

    Private Sub txt_TotalDPP_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalDPP.TextChanged
        TotalDPP = AmbilAngka(txt_TotalDPP.Text)
        PemecahRibuanUntukTextBox(txt_TotalDPP)
    End Sub

    Private Sub txt_PPNMasukan_TextChanged(sender As Object, e As EventArgs) Handles txt_PPNMasukan.TextChanged
        PPNMasukan = AmbilAngka(txt_PPNMasukan.Text)
        PemecahRibuanUntukTextBox(txt_PPNMasukan)
    End Sub

    Private Sub txt_JumlahPPhTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPhTerutang.TextChanged
        JumlahPPhTerutang = AmbilAngka(txt_JumlahPPhTerutang.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPPhTerutang)
    End Sub

    Private Sub txt_JumlahPPhDitanggung_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPhDitanggung.TextChanged
        JumlahPPhDitanggung = AmbilAngka(txt_JumlahPPhDitanggung.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPPhDitanggung)
        If JumlahPPhDitanggung > JumlahPPhTerutang Then
            MsgBox("Silakan isi kolom PPh Ditanggung dengan benar..!")
            txt_JumlahPPhDitanggung.Text = Nothing
            txt_JumlahPPhDitanggung.Focus()
        End If
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_JumlahPPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPhDipotong.TextChanged
        JumlahPPhDipotong = AmbilAngka(txt_JumlahPPhDipotong.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPPhDipotong)
    End Sub

    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTagihan.TextChanged
        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTagihan)
    End Sub

    Private Sub dtp_DueDate_ValueChanged(sender As Object, e As EventArgs) Handles dtp_DueDate.ValueChanged
        KunciTahun_TidakBolehKurangDariTahunBukuAktif(dtp_DueDate)
        DueDate = dtp_DueDate.Value
    End Sub

    Private Sub txt_KodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeProject.TextChanged
        KodeProject = txt_KodeProject.Text
    End Sub
    Private Sub txt_KodeProject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeProject.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPembelian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPembelian.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPembelian_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPembelian.TextChanged
        JenisPembelian = cmb_JenisPembelian.Text
        If JenisPembelian = JenisPembelian_Tunai Then
            KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
            cmb_SaranaPembayaran.Visible = True
            lbl_SaranaPembayaran.Visible = True
            cmb_SaranaPembayaran.Focus()
            cmb_SaranaPembayaran.DroppedDown = True
        End If
        If JenisPembelian = JenisPembelian_Tempo Then
            cmb_SaranaPembayaran.Visible = False
            lbl_SaranaPembayaran.Visible = False
            cmb_SaranaPembayaran.Text = Nothing
            Reset_grb_Bank()
            COAKredit = KodeTautanCOA_HutangUsaha_NonAfiliasi '(COA Hutang Usaha)
        End If
    End Sub

    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        COAKredit = Microsoft.VisualBasic.Left(SaranaPembayaran, JumlahDigitCOA)
        If AmbilAngka(COAKredit) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COAKredit) <= kodeakun_Bank_Akhir _
            Then
            grb_Bank.Enabled = True
            KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
            PerhitunganValueBank()
        Else
            Reset_grb_Bank()
        End If
    End Sub

    Sub Reset_grb_Bank()
        grb_Bank.Enabled = False
        txt_BiayaAdministrasiBank.Text = Nothing
        KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
    End Sub

    Private Sub txt_BiayaAdministrasiBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaAdministrasiBank.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If AmbilAngka(txt_BiayaAdministrasiBank.Text) = 0 Then
            cmb_DitanggungOleh.Enabled = False
            cmb_DitanggungOleh.Text = Nothing
        Else
            cmb_DitanggungOleh.Enabled = True
            cmb_DitanggungOleh.Text = DitanggungOleh_LawanTransaksi
        End If
        PerhitunganValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.SelectedIndexChanged
    End Sub
    Private Sub cmb_DitanggungOleh_TextChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.TextChanged
        DitanggungOleh = cmb_DitanggungOleh.Text
        PerhitunganValueBank()
    End Sub

    Private Sub txt_BiayaTransportasiPembelian_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaTransportasiPembelian.TextChanged
        BiayaTransportasiPembelian = AmbilAngka(txt_BiayaTransportasiPembelian.Text)
        PemecahRibuanUntukTextBox(txt_BiayaTransportasiPembelian)
        PerhitunganJumlahPPhTerutang_JumlahPPhDipotong_JumlahTagihan_JumlahHutangUsaha()
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub


    Public Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click 'PENYIMPANAN DATA

        'Jika Tanggal Faktur Pajak Disabled (akibat tidak ada PPN)
        If dtp_TanggalFakturPajak.Visible = False Then TanggalFakturPajak = TanggalKosong

        'Logika Validasi FORM INPUT:

        'Nomor PO :
        If txt_PO_Nomor.Text = Kosongan Then
            Pilihan = MessageBox.Show("Lanjutkan penyimpanan tanpa Nomor PO..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then
                txt_PO_Nomor.Focus()
                Return
            End If
            TanggalPO = TanggalKosong
        End If

        'Tanggal Transaksi :
        If rdb_SamaDenganTanggalInvoice.Checked = False And rdb_TidakSama.Checked = False Then
            MsgBox("Silakan tentukan 'Tanggal Transaksi")
            rdb_SamaDenganTanggalInvoice.Focus()
            Return
        End If

        'Tanggal SJ/BAST >= Tanggal Invoice
        If dtp_TanggalSJBAST.Value < dtp_TanggalInvoice.Value Then
            MsgBox("'Tanggal Surat Jalan' mendahului 'Tanggal Invoice'." & Enter2Baris & " Silakan koreksi kembali.")
            dtp_TanggalSJBAST.Focus()
            Return
        End If

        'Tanggal Transaksi <= Tanggal Invoice
        If dtp_TanggalTransaksi.Value > dtp_TanggalInvoice.Value Then
            MsgBox("'Tanggal Transaksi' melebihi 'Tanggal Invoice'." & Enter2Baris & " Silakan koreksi kembali.")
            dtp_TanggalTransaksi.Focus()
            Return
        End If

        'Tanggal Faktur Pajak harus SAMA dengan Tanggal Invoice..!
        If dtp_TanggalFakturPajak.Visible = True Then
            If TanggalFakturPajak <> TanggalInvoice Then
                MsgBox("'Tanggal Faktur Pajak' tidak sama dengan 'Tanggal Invoice'." & Enter2Baris & " Silakan koreksi kembali.")
                dtp_TanggalFakturPajak.Focus()
                Return
            End If
        End If

        'Nomor Invoice/Nota tidak boleh kosong
        If NomorInvoice = Nothing Then
            MsgBox("'Nomor Invoice/Nota' tidak boleh kosong..!")
            txt_NomorInvoice.Focus()
            Return
        End If
        'Kode Dokumen tidak boleh kosong
        If KodeDokumen = Nothing Then
            MsgBox("'Kode Dokumen' tidak boleh kosong..!")
            txt_KodeDokumen.Focus()
            Return
        End If
        'Kode Supplier tidak boleh kosong
        If KodeSupplier = Nothing Then
            MsgBox("'Data Supplier' tidak boleh kosong..!")
            txt_KodeSupplier.Focus()
            Return
        End If
        'Nama Supplier tidak oleh kosong
        If NamaSupplier = Nothing Then
            MsgBox("'Nama Supplier' tidak boleh kosong..!")
            txt_NamaSupplier.Focus()
            Return
        End If
        'Jumlah/DPP PPN tidak boleh kosong
        If DPPPPN = Nothing Then
            MsgBox("'Jumlah/DPP PPN' tidak boleh kosong")
            txt_DPPPPN.Focus()
            Return
        End If
        'Kolom Nama Barang/Jasa tidak boleh kosong
        If chk_NamaBarang1.Checked = True Then
            If txt_NamaBarang1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Barang 1'")
                txt_NamaBarang1.Focus()
                Return
            End If
            If txt_NamaAkunBarang1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Akun'")
                txt_NamaAkunBarang1.Focus()
                Return
            End If
            If txt_DPPBarang1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'DPP'")
                txt_DPPBarang1.Focus()
                Return
            End If
        End If
        If chk_NamaBarang2.Checked = True Then
            If txt_NamaBarang2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Barang 2'")
                txt_NamaBarang2.Focus()
                Return
            End If
            If txt_NamaAkunBarang2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Akun'")
                txt_NamaAkunBarang2.Focus()
                Return
            End If
            If txt_DPPBarang2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'DPP'")
                txt_DPPBarang2.Focus()
                Return
            End If
        End If
        If chk_NamaJasa1.Checked = True Then
            If txt_NamaJasa1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Jasa 1'")
                txt_NamaJasa1.Focus()
                Return
            End If
            If txt_NamaAkunJasa1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Akun'")
                txt_NamaAkunJasa1.Focus()
                Return
            End If
            If txt_DPPJasa1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'DPP'")
                txt_DPPJasa1.Focus()
                Return
            End If
            If cmb_JenisJasa1.Text = Nothing Then
                MsgBox("Silakan pilih kolom 'Objek'")
                cmb_JenisJasa1.Focus()
                Return
            End If
            If txt_TarifPPh1.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Tarif PPh'")
                txt_TarifPPh1.Focus()
                Return
            End If
        End If
        If chk_NamaJasa2.Checked = True Then
            If txt_NamaJasa2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Jasa 2'")
                txt_NamaJasa2.Focus()
                Return
            End If
            If txt_NamaAkunJasa2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Akun'")
                txt_NamaAkunJasa2.Focus()
                Return
            End If
            If txt_DPPJasa2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'DPP'")
                txt_DPPJasa2.Focus()
                Return
            End If
            If cmb_JenisJasa2.Text = Nothing Then
                MsgBox("Silakan pilih kolom 'Objek'")
                cmb_JenisJasa2.Focus()
                Return
            End If
            If txt_TarifPPh2.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Tarif PPh'")
                txt_TarifPPh2.Focus()
                Return
            End If
        End If
        If chk_NamaJasa3.Checked = True Then
            If txt_NamaJasa3.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Jasa 3'")
                txt_NamaJasa3.Focus()
                Return
            End If
            If txt_NamaAkunJasa3.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Nama Akun'")
                txt_NamaAkunJasa3.Focus()
                Return
            End If
            If txt_DPPJasa3.Text = Nothing Then
                MsgBox("Silakan isi kolom 'DPP'")
                txt_DPPJasa3.Focus()
                Return
            End If
            If cmb_JenisJasa3.Text = Nothing Then
                MsgBox("Silakan pilih kolom 'Objek'")
                cmb_JenisJasa3.Focus()
                Return
            End If
            If txt_TarifPPh3.Text = Nothing Then
                MsgBox("Silakan isi kolom 'Tarif PPh'")
                txt_TarifPPh3.Focus()
                Return
            End If
        End If
        If txt_NamaBarang1.Text = Nothing And txt_NamaBarang2.Text = Nothing And txt_NamaJasa1.Text = Nothing Then
            MsgBox("Silakan isi Nama Barang atau Jasa")
            Return
        End If

        'Jumlah DPP Barang & Jasa harus sama dengan Jumlah DPP.
        If (JumlahDPPBarang + JumlahDPPJasa) <> DPPPPN Then
            MsgBox("Jumlah DPP Barang dan/atau Jasa tidak sama dengan Jumlah/DPP PPN yang Anda input. Silakan koreksi kembali.")
            txt_DPPPPN.Focus()
            Return
        End If

        If JenisPembelian = Nothing Then
            MsgBox("Silakan pilih 'Jenis Pembelian'")
            cmb_JenisPembelian.Focus()
            cmb_JenisPembelian.DroppedDown = True
            Return
        End If

        If JenisPembelian = JenisPembelian_Tunai Then
            If SaranaPembayaran = Nothing Then
                MsgBox("Silakan pilih 'Sarana Pembayaran'")
                cmb_SaranaPembayaran.Focus()
                cmb_SaranaPembayaran.DroppedDown = True
                Return
            End If
        End If

        'Akhir Coding Validasi Form Input ---------------------------------------------------------------------------------------------------------------------------

        If FungsiForm = FungsiForm_EDIT Then
            Pilihan = MessageBox.Show("Jika Anda melakukan pengeditan pada data pembelian ini, maka seluruh data pembayaran yang terkait dengannya akan dihapus!" & Enter2Baris &
                                      "Lanjutkan proses edit..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then Return
        End If

        'Perhitungan PPh Terutang Per Pasal, dan Deklarasi Variabel Jumlah DPP dan Jumlah PPN
        Dim JumlahPPh21, JumlahPPh23, JumlahPPh4Ayat2 As Int64

        'Jumlah PPh Pasal 21 :
        JumlahPPh21 = 0
        If JenisPPh1 = JenisPPh_Pasal21 Then JumlahPPh21 += PPhTerutang1
        If JenisPPh2 = JenisPPh_Pasal21 Then JumlahPPh21 += PPhTerutang2
        If JenisPPh3 = JenisPPh_Pasal21 Then JumlahPPh21 += PPhTerutang3

        'Jumlah PPh Pasal 23 :
        JumlahPPh23 = 0
        If JenisPPh1 = JenisPPh_Pasal23 Then JumlahPPh23 += PPhTerutang1
        If JenisPPh2 = JenisPPh_Pasal23 Then JumlahPPh23 += PPhTerutang2
        If JenisPPh3 = JenisPPh_Pasal23 Then JumlahPPh23 += PPhTerutang3

        'Jumlah PPh Pasal 4 (2) :
        JumlahPPh4Ayat2 = 0
        If JenisPPh1 = JenisPPh_Pasal42 Then JumlahPPh4Ayat2 += PPhTerutang1
        If JenisPPh2 = JenisPPh_Pasal42 Then JumlahPPh4Ayat2 += PPhTerutang2
        If JenisPPh3 = JenisPPh_Pasal42 Then JumlahPPh4Ayat2 += PPhTerutang3

        JenisPPh1 = Nothing
        If chk_NamaJasa1.Checked = True Then
            JenisPPh1 = cmb_JenisPPh1.Text
        End If
        JenisPPh2 = Nothing
        If chk_NamaJasa2.Checked = True Then
            JenisPPh2 = cmb_JenisPPh2.Text
        End If
        JenisPPh3 = Nothing
        If chk_NamaJasa3.Checked = True Then
            JenisPPh3 = cmb_JenisPPh3.Text
        End If

        Dim TanggalIniSimpan = TanggalFormatSimpan(Today)
        Dim TanggalPOSimpan = TanggalFormatSimpan(TanggalPO)
        Dim TanggalSJBASTSimpan = TanggalFormatSimpan(TanggalSJBAST)
        Dim TanggalInvoiceSimpan = TanggalFormatSimpan(TanggalInvoice)
        Dim TanggalTransaksiSimpan = TanggalFormatSimpan(TanggalTransaksi)
        Dim TanggalFakturPajakSimpan = TanggalFormatSimpan(TanggalFakturPajak)
        Dim DueDateSimpan = TanggalFormatSimpan(DueDate)

        Dim StatusLunas
        If cmb_JenisPembelian.Text = JenisPembelian_Tunai Then
            StatusLunas = "LUNAS"
        Else
            StatusLunas = Nothing
        End If

        TrialBalance_Mentahkan()

        Dim QueryPenyimpanan = Nothing

        'Jika Bermaksud Menambah Data :
        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            AngkaNomorJVPembelian = jur_NomorJV
            StatusJurnal = 1
            QueryPenyimpanan = " INSERT INTO tbl_Pembelian VALUES ( '" &
                                Mid(NomorPembelian, PanjangTeks_AwalanPEMB_PlusTahunBuku_Plus1) & "', '" &
                                NomorPembelian & "', '" &
                                NomorPO & "', '" &
                                TanggalPOSimpan & "', '" &
                                TanggalIniSimpan & "', '" &
                                NomorSJBAST & "', '" &
                                TanggalSJBASTSimpan & "', '" &
                                NomorInvoice & "', '" &
                                TanggalInvoiceSimpan & "', '" &
                                TanggalTransaksiSimpan & "', '" &
                                KodeDokumen & "', '" &
                                KodeSupplier & "', '" &
                                NamaSupplier & "', '" &
                                NPWP & "', '" &
                                NomorFakturPajak & "', '" &
                                TanggalFakturPajakSimpan & "', '" &
                                DPPPPN & "', '" &
                                JenisPPN & "', '" &
                                Microsoft.VisualBasic.Replace(TarifPPN, ",", ".") & "', '" &
                                JumlahPPN & "', '" &
                                PPNDikreditkan & "', '" &
                                DibiayakanDikapitalisasi & "', '" &
                                NamaBarang1 & "', '" &
                                COABarang1 & "', '" &
                                DPPBarang1 & "', '" &
                                NamaBarang2 & "', '" &
                                COABarang2 & "', '" &
                                DPPBarang2 & "', '" &
                                NamaJasa1 & "', '" &
                                COAJasa1 & "', '" &
                                DPPJasa1 & "', '" &
                                JenisJasa1 & "', '" &
                                JenisPPh1 & "', '" &
                                Microsoft.VisualBasic.Replace(TarifPPh1, ",", ".") & "', '" &
                                PPhTerutang1 & "', '" &
                                PPhDitanggung1 & "', '" &
                                PPhDipotong1 & "', '" &
                                NamaJasa2 & "', '" &
                                COAJasa2 & "', '" &
                                DPPJasa2 & "', '" &
                                JenisJasa2 & "', '" &
                                JenisPPh2 & "', '" &
                                Microsoft.VisualBasic.Replace(TarifPPh2, ",", ".") & "', '" &
                                PPhTerutang2 & "', '" &
                                PPhDitanggung2 & "', '" &
                                PPhDipotong2 & "', '" &
                                NamaJasa3 & "', '" &
                                COAJasa3 & "', '" &
                                DPPJasa3 & "', '" &
                                JenisJasa3 & "', '" &
                                JenisPPh3 & "', '" &
                                Microsoft.VisualBasic.Replace(TarifPPh3, ",", ".") & "', '" &
                                PPhTerutang3 & "', '" &
                                PPhDitanggung3 & "', '" &
                                PPhDipotong3 & "', '" &
                                JumlahDPPBarang & "', '" &
                                JumlahDPPJasa & "', '" &
                                JumlahPPhTerutang & "', '" &
                                JumlahPPhDitanggung & "', '" &
                                JumlahPPhDipotong & "', '" &
                                JumlahTagihan & "', '" &
                                JumlahHutangUsaha & "', '" &
                                DueDateSimpan & "', '" &
                                KodeProject & "', '" &
                                JenisPembelian & "', '" &
                                SaranaPembayaran & "', '" &
                                BiayaAdministrasiBank & "', '" &
                                DitanggungOleh & "', '" &
                                COAKredit & "', '" &
                                BiayaTransportasiPembelian & "', '" &
                                Keterangan & "', '" &
                                AngkaNomorJVPembelian & "', '" &
                                StatusLunas & "', '" &
                                StatusJurnal & "', '" &
                                UserAktif & "' ) "
        End If

        'Jika Bermaksud Mengedit Data :
        If FungsiForm = FungsiForm_EDIT Then
            If AngkaNomorJVPembelian = 0 Then 'Jika belum dijurnal, atau sudah pernah dijurnal, tapi terhapus.
                SistemPenomoranOtomatis_NomorJV()
                AngkaNomorJVPembelian = jur_NomorJV
                StatusJurnal = 1
            End If
            If AngkaNomorJVPembelian > 0 Then jur_NomorJV = AngkaNomorJVPembelian 'Jika sudah dijurnal, dan masih ada jurnalnya.
            QueryPenyimpanan = " UPDATE tbl_Pembelian SET " &
                    " PO_Nomor                      = '" & NomorPO & "', " &
                    " Tanggal_PO                    = '" & TanggalPOSimpan & "', " &
                    " Tanggal_Input_Edit            = '" & TanggalIniSimpan & "', " &
                    " Nomor_SJ_BAST                 = '" & NomorSJBAST & "', " &
                    " Tanggal_SJ_BAST               = '" & TanggalSJBASTSimpan & "', " &
                    " Nomor_Invoice                 = '" & NomorInvoice & "', " &
                    " Tanggal_Invoice               = '" & TanggalInvoiceSimpan & "', " &
                    " Tanggal_Transaksi             = '" & TanggalTransaksiSimpan & "', " &
                    " Kode_Dokumen                  = '" & KodeDokumen & "', " &
                    " Kode_Supplier                 = '" & KodeSupplier & "', " &
                    " Nama_Supplier                 = '" & NamaSupplier & "', " &
                    " NPWP                          = '" & NPWP & "', " &
                    " Nomor_Faktur_Pajak            = '" & NomorFakturPajak & "', " &
                    " Tanggal_Faktur_Pajak          = '" & TanggalFakturPajakSimpan & "', " &
                    " DPP                           = '" & DPPPPN & "', " &
                    " Jenis_PPN                     = '" & JenisPPN & "', " &
                    " Tarif_PPN                     = '" & DesimalFormatSimpan(TarifPPN) & "', " &
                    " PPN                           = '" & JumlahPPN & "', " &
                    " PPN_Dikreditkan               = '" & PPNDikreditkan & "', " &
                    " Dibiayakan_Dikapitalisasi     = '" & DibiayakanDikapitalisasi & "', " &
                    " Nama_Barang_1                 = '" & NamaBarang1 & "', " &
                    " COA_Barang_1                  = '" & COABarang1 & "', " &
                    " DPP_Barang_1                  = '" & DPPBarang1 & "', " &
                    " Nama_Barang_2                 = '" & NamaBarang2 & "', " &
                    " COA_Barang_2                  = '" & COABarang2 & "', " &
                    " DPP_Barang_2                  = '" & DPPBarang2 & "', " &
                    " Nama_Jasa_1                   = '" & NamaJasa1 & "', " &
                    " COA_Jasa_1                    = '" & COAJasa1 & "', " &
                    " DPP_Jasa_1                    = '" & DPPJasa1 & "', " &
                    " Jenis_Jasa_1                  = '" & JenisJasa1 & "', " &
                    " Jenis_PPh_1                   = '" & JenisPPh1 & "', " &
                    " Tarif_PPh_1                   = '" & DesimalFormatSimpan(TarifPPh1) & "', " &
                    " PPh_Terutang_1                = '" & PPhTerutang1 & "', " &
                    " PPh_Ditanggung_1              = '" & PPhDitanggung1 & "', " &
                    " PPh_Dipotong_1                = '" & PPhDipotong1 & "', " &
                    " Nama_Jasa_2                   = '" & NamaJasa2 & "', " &
                    " COA_Jasa_2                    = '" & COAJasa2 & "', " &
                    " DPP_Jasa_2                    = '" & DPPJasa2 & "', " &
                    " Jenis_Jasa_2                  = '" & JenisJasa2 & "', " &
                    " Jenis_PPh_2                   = '" & JenisPPh2 & "', " &
                    " Tarif_PPh_2                   = '" & DesimalFormatSimpan(TarifPPh2) & "', " &
                    " PPh_Terutang_2                = '" & PPhTerutang2 & "', " &
                    " PPh_Ditanggung_2              = '" & PPhDitanggung2 & "', " &
                    " PPh_Dipotong_2                = '" & PPhDipotong2 & "', " &
                    " Nama_Jasa_3                   = '" & NamaJasa3 & "', " &
                    " COA_Jasa_3                    = '" & COAJasa3 & "', " &
                    " DPP_Jasa_3                    = '" & DPPJasa3 & "', " &
                    " Jenis_Jasa_3                  = '" & JenisJasa3 & "', " &
                    " Jenis_PPh_3                   = '" & JenisPPh3 & "', " &
                    " Tarif_PPh_3                   = '" & DesimalFormatSimpan(TarifPPh3) & "', " &
                    " PPh_Terutang_3                = '" & PPhTerutang3 & "', " &
                    " PPh_Ditanggung_3              = '" & PPhDitanggung3 & "', " &
                    " PPh_Dipotong_3                = '" & PPhDipotong3 & "', " &
                    " Jumlah_DPP_Barang             = '" & JumlahDPPBarang & "', " &
                    " Jumlah_DPP_Jasa               = '" & JumlahDPPJasa & "', " &
                    " Jumlah_PPh_Terutang           = '" & JumlahPPhTerutang & "', " &
                    " Jumlah_PPh_Ditanggung         = '" & JumlahPPhDitanggung & "', " &
                    " Jumlah_PPh_Dipotong           = '" & JumlahPPhDipotong & "', " &
                    " Jumlah_Tagihan                = '" & JumlahTagihan & "', " &
                    " Jumlah_Hutang_Usaha           = '" & JumlahHutangUsaha & "', " &
                    " Due_Date                      = '" & DueDateSimpan & "', " &
                    " Kode_Project                  = '" & KodeProject & "', " &
                    " Jenis_Pembelian               = '" & JenisPembelian & "', " &
                    " Sarana_Pembayaran             = '" & SaranaPembayaran & "', " &
                    " Biaya_Administrasi_Bank       = '" & BiayaAdministrasiBank & "', " &
                    " Ditanggung_Oleh               = '" & DitanggungOleh & "', " &
                    " COA_Kredit                    = '" & COAKredit & "', " &
                    " Biaya_Transportasi            = '" & BiayaTransportasiPembelian & "', " &
                    " Keterangan                    = '" & Keterangan & "', " &
                    " Nomor_JV                      = '" & AngkaNomorJVPembelian & "', " &
                    " Status_Lunas                  = '" & StatusLunas & "', " &
                    " Status_Jurnal                 = '" & StatusJurnal & "', " &
                    " User                          = '" & UserAktif & "' " &
                    " WHERE Nomor_Pembelian         = '" & NomorPembelian & "' "
        End If

        'Proses Penyimpanan/Update Database ada di sini :
        Dim ProsesPenyimpananDataPembelian As Boolean
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
        Try
            cmd.ExecuteNonQuery()
            ProsesPenyimpananDataPembelian = True
        Catch ex As Exception
            ProsesPenyimpananDataPembelian = False
        End Try
        AksesDatabase_Transaksi(Tutup)

        If ProsesPenyimpananDataPembelian = True Then

            If FungsiForm = FungsiForm_TAMBAH Then 'Jika form digunakan untuk TAMBAH data baru, maka :
                'Belum ada Coding
            End If

            If FungsiForm = FungsiForm_EDIT Then

                'Hapus Data Jurnal Pembelian yang Lama :
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)

                'Hapus Semua Data Pembayaran (Data Finance berikut Jurnalnya) yang terkait dengan Data Pembelian Ini :

                AksesDatabase_Transaksi(Buka)

                'Hapus Semua Data Jurnal Pembayaran terkait Data Pembelian ini :
                cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ",
                                      KoneksiDatabaseTransaksi)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    Dim NomorJVPembayaran = dr.Item("Nomor_JV")
                    Dim cmdHapusJurnalPembayaran = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJVPembayaran & "' ", KoneksiDatabaseTransaksi)
                    cmdHapusJurnalPembayaran.ExecuteNonQuery()
                Loop

                'Hapus Semua Data di tbl_PembayaranHutangUsaha terkait Data Pembelian ini :
                cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangUsaha " &
                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()

                'Hapus Data di tbl_PengajuanPembayaranHutangUsaha terkait Data Pembelian ini :
                cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangUsaha " &
                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()

                AksesDatabase_Transaksi(Tutup)


            End If

            'frm_BukuPembelian.TampilkanData()
            'frm_POPembelian.TampilkanData()
            'If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
            'frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'Nanti tambahkan Sub Tampilkan data untuk PPh Pasal 4 (2)

            '====================================================================================
            'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.
            '====================================================================================

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then

                ResetValueJurnal()
                jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
                jur_JenisJurnal = JenisJurnal_Pembelian
                jur_KodeDokumen = KodeDokumen
                jur_NomorPO = NomorPO
                jur_KodeProject = KodeProject
                jur_Referensi = NomorPembelian
                jur_TanggalInvoice = Microsoft.VisualBasic.Left(TanggalInvoice, 10)
                jur_NomorInvoice = NomorInvoice
                jur_NomorFakturPajak = NomorFakturPajak
                jur_KodeLawanTransaksi = KodeSupplier
                jur_NamaLawanTransaksi = NamaSupplier
                jur_UraianTransaksi = Keterangan
                jur_Direct = 0

                If PPNDikreditkan = Keterangan_Tidak Then
                    If DibiayakanDikapitalisasi = PilihanPPN_Dibiayakan Then COAPPN = KodeTautanCOA_BiayaPPN
                    If DibiayakanDikapitalisasi = PilihanPPN_Dikapitalisasi Then COAPPN = KodeTautanCOA_Bangunan
                End If

                Dim JumlahPencairan As Int64 = 0
                If JenisPembelian = JenisPembelian_Tunai Then JumlahPencairan = JumlahTagihan
                If JenisPembelian = JenisPembelian_Tempo Then JumlahPencairan = JumlahHutangUsaha

                'Eliminasi Beberapa Baris Jurnal :
                If Not (JenisPembelian = JenisPembelian_Tunai) Then
                    JumlahPPh21 = 0
                    JumlahPPh23 = 0
                    JumlahPPh4Ayat2 = 0
                End If

                'Simpan Jurnal :
                ___jurDebet(COABarang1, DPPBarang1)
                ___jurDebet(COABarang2, DPPBarang2)
                ___jurDebet(COAJasa1, DPPJasa1)
                ___jurDebet(COAJasa2, DPPJasa2)
                ___jurDebet(COAJasa3, DPPJasa3)
                ___jurDebet(COAPPN, JumlahPPN)
                '___jurDebet(KodeTautanCOA_BiayaPPh, JumlahPPhDitanggung)
                ___jurDebet(COABiayaTransportasiPembelian, BiayaTransportasiPembelian)
                ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
                _______jurKreditBankCashOUT(DitanggungOleh, COAKredit, JumlahPencairan, JumlahTransfer, BiayaAdministrasiBank)
                _______jurKredit(KodeTautanCOA_HutangPPhPasal21, JumlahPPh21)
                _______jurKredit(KodeTautanCOA_HutangPPhPasal23, JumlahPPh23)
                _______jurKredit(KodeTautanCOA_HutangPPhPasal42, JumlahPPh4Ayat2)

                If jur_StatusPenyimpananJurnal_PerBaris = True Then
                    jur_StatusPenyimpananJurnal_Lengkap = True
                Else
                    jur_StatusPenyimpananJurnal_Lengkap = False
                    AksesDatabase_Transaksi(Buka)
                    cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
                    cmd.ExecuteNonQuery()
                    cmd = New OdbcCommand(" UPDATE tbl_Pembelian SET Nomor_JV = 0, Status_Jurnal = 0 WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
                    cmd.ExecuteNonQuery()
                    AksesDatabase_Transaksi(Tutup)
                End If

                ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

            End If

        End If

        If ProsesPenyimpananDataPembelian = True Then
            frm_BukuPembelian.TampilkanData()
            If jur_StatusPenyimpananJurnal_Lengkap = True Then
                If usc_JurnalUmum.StatusAktif Then usc_JurnalUmum.TampilkanData()
                If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data Pembelian BERHASIL Disimpan ke Tabel Pembelian dan Dikirim ke Jurnal.")
                If FungsiForm = FungsiForm_EDIT Then MsgBox("Data Pembelian BERHASIL Diedit di Tabel Pembelian dan Tabel Jurnal.")
            Else
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data Pembelian BERHASIL Disimpan ke Tabel Pembelian, tapi GAGAL dikirim ke Jurnal karena ada kesalahan teknis." & Enter2Baris &
                        "Tidak perlu khawatir. Anda bisa memposting kembali data tersebut ke Jurnal di lain kesempatan.")
                    If FungsiForm = FungsiForm_EDIT Then MsgBox("Data Pembelian BERHASIL Diedit di Tabel Pembelian, tapi Jurnalnya terhapus karena kesalahan teknis." & Enter2Baris &
                        "Tidak perlu khawatir. Anda bisa memposting kembali data tersebut ke Jurnal di lain kesempatan.")
                End If
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data Pembelian BERHASIL Disimpan.")
                    If FungsiForm = FungsiForm_EDIT Then MsgBox("Data Pembelian BERHASIL Diedit.")
                End If
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data Pembelian GAGAL Disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            If FungsiForm = FungsiForm_EDIT Then MsgBox("Data Pembelian GAGAL Diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

        Me.Close()

    End Sub 'AKHIR SUB PENYIMPANAN DATA

    Sub PerhitunganValueBank()
        If DitanggungOleh = DitanggungOleh_LawanTransaksi Then
            JumlahTransfer = JumlahTagihan - BiayaAdministrasiBank
        End If
        If cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan Then
            JumlahTransfer = JumlahTagihan
        End If
        If DitanggungOleh = Nothing Then 'Ini akan terjadi jika SARANA PEMBAYARAN <> BANK
            JumlahTransfer = JumlahTagihan
        End If
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        TutupForm()
    End Sub

    Private Sub TutupForm()
        Pilihan = MessageBox.Show("Yakin akan menutup form..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then
            Return
        End If
        If Pilihan = vbYes Then
            frm_BukuPembelian.BersihkanSeleksi()
            Me.Close()
        End If
    End Sub

End Class