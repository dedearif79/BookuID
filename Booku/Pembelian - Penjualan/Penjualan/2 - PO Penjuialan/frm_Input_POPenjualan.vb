Imports bcomm
Imports System.Data.Odbc

Public Class frm_Input_POPenjualan

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
    Dim Kontrol

    'Variabel Tabel Produk :
    Dim NomorUrutProduk
    Dim JenisProduk_PerItem
    Dim KodeProjectProduk
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
    Dim KodeProjectProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi

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


    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        KontenCombo_JenisPPh()

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input PO Penjualan - " & JenisProduk_Induk
            AngkaPO = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_PO", "Angka_PO") + 1
            lbl_SJBAST.Visible = False
            dgv_SJBAST.Visible = False
            lbl_Invoice.Visible = False
            dgv_Invoice.Visible = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit PO Penjualan - " & JenisProduk_Induk
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                  " WHERE Kode_Mitra = '" & KodeCustomer & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            txt_AlamatCustomer.Text = dr.Item("Alamat")
            AksesDatabase_General(Tutup)
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                  " WHERE Angka_PO = '" & AngkaPO & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_TermOfPayment.Text = dr.Item("Term_Of_Payment")
                txt_KeteranganToP.Text = dr.Item("Keterangan_ToP")
                cmb_JenisPPh.Text = dr.Item("Jenis_PPh")
                txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
                txt_BiayaTransportasiPenjualan.Text = dr.Item("Biaya_Transportasi")
            End If
            AksesDatabase_Transaksi(Tutup)
            NomorPO_Lama = NomorPO
            lbl_SJBAST.Visible = True
            dgv_SJBAST.Visible = True
            lbl_Invoice.Visible = True
            dgv_Invoice.Visible = True
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

        StyleTabelUtama(DataTabelUtama)
        StyleTabelUtama(dgv_SJBAST)
        StyleTabelUtama(dgv_Invoice)

        BeginInvoke(Sub() BersihkanSeleksi_TabelProduk())
        BeginInvoke(Sub() BersihkanSeleksi_TabelSJBAST())
        BeginInvoke(Sub() BersihkanSeleksi_TabelInvoice())
        BeginInvoke(Sub() txt_NomorPO.Focus())

        ProsesLoadingForm = False

        If FungsiForm <> FungsiForm_TAMBAH Then Perhitungan()

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        AngkaPO = 0
        txt_NomorPO.Text = Kosongan
        dtp_TanggalPO.Value = Today
        JenisProduk_Induk = Kosongan
        txt_TermOfPayment.Text = Kosongan
        txt_KeteranganToP.Text = Kosongan
        txt_KodeProject.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_AlamatCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_Catatan.Text = Kosongan
        rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = False
        rdb_TanggalJangkaWaktuPenyelesaian.Checked = False
        txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        lbl_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_DPPBarang.Text = Kosongan
        txt_DPPJasa.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalTagihan_Kotor.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_BiayaTransportasiPenjualan.Text = Kosongan
        txt_TotalTagihan.Text = Kosongan
        KontenCombo_Kontrol()
        Kosongkan_TabelProduk()
        Kosongkan_TabelSJBAST()
        kosongkan_TabelInvoice()
        btn_Simpan.Enabled = True

        ProsesResetForm = False

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
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
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
        If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False And JenisPPN <> JenisPPN_NonPPN Then
            cmb_PerlakuanPPN.Text = Kosongan
            MsgBox("Silakan pilih 'Perlakuan PPN'.")
            cmb_PerlakuanPPN.Focus()
        End If
    End Sub

    Sub KontenCombo_Kontrol()
        cmb_Kontrol.Items.Clear()
        cmb_Kontrol.Items.Add(Status_Open)
        cmb_Kontrol.Items.Add(Status_Closed)
        cmb_Kontrol.Text = Status_Open
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        If JenisProduk_Induk = JenisProduk_Barang Then
            If PerlakuanPPN = PerlakuanPPN_Dipungut Then
                AktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_Pasal22_Lokal
            Else
                NonAktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_NonPPh
            End If
        Else
            AktifkanPPh()
            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                cmb_JenisPPh.Text = JenisPPh_Pasal42
                txt_TarifPPh.Enabled = True
            Else
                cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
                cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
                cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
                cmb_JenisPPh.Items.Add(JenisPPh_Pasal26)
                cmb_JenisPPh.Items.Add(JenisPPh_NonPPh)
                cmb_JenisPPh.Text = Kosongan
                txt_TarifPPh.Enabled = False
            End If
        End If
    End Sub

    Sub AktifkanPPh()
        lbl_PPh.Enabled = True
        cmb_JenisPPh.Enabled = True
        lbl_PersenPPh.Enabled = True
        lbl_PPhDitanggung.Enabled = True
        lbl_PPhDipotong.Enabled = True
        txt_PPhTerutang.Enabled = True
        txt_PPhDitanggung.Enabled = True
        txt_PPhDipotong.Enabled = True
    End Sub

    Sub NonAktifkanPPh()
        lbl_PPh.Enabled = False
        cmb_JenisPPh.Enabled = False
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
    End Sub

    Sub Kosongkan_TabelSJBAST()
        dgv_SJBAST.Rows.Clear()
        JumlahSJBAST = 0
    End Sub

    Sub kosongkan_TabelInvoice()
        dgv_Invoice.Rows.Clear()
        JumlahInvoice = 0
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
        dgv_SJBAST.ClearSelection()
        JumlahSJBAST = dgv_SJBAST.RowCount
    End Sub

    Sub BersihkanSeleksi_TabelInvoice()
        BarisInvoice_Terseleksi = -1
        NomorInvoice_Terseleksi = Kosongan
        dgv_Invoice.ClearSelection()
        JumlahInvoice = dgv_Invoice.RowCount
    End Sub


    Sub KondisiFormSetelahPerubahan()
        BersihkanSeleksi_TabelProduk()
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
            DPP = HitunganHarga_Relatif
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

        PPhDipotong = PPhTerutang - PPhDitanggung
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

        If Diskon_Rp = 0 Then
            lbl_Diskon.Enabled = False
            txt_Diskon_Rp.Enabled = False
        Else
            lbl_Diskon.Enabled = True
            txt_Diskon_Rp.Enabled = True
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

    Private Sub txt_NomorPO_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorPO.TextChanged
        NomorPO = txt_NomorPO.Text
    End Sub
    Private Sub txt_NomorPO_Leave(sender As Object, e As EventArgs) Handles txt_NomorPO.Leave
        If NomorPO = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor PO'.")
            txt_NomorPO.Focus()
            Return
        End If
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

    Private Sub dtp_TanggalPO_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPO.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalPO)
        TanggalPO = dtp_TanggalPO.Value
        KondisiFormSetelahPerubahan()
        txt_TarifPPN.Text = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalPO)
    End Sub

    Private Sub txt_TermOfPayment_TextChanged(sender As Object, e As EventArgs) Handles txt_TermOfPayment.TextChanged
        TermOfPayment = AmbilAngka(txt_TermOfPayment.Text)
        PemecahRibuanUntukTextBox(txt_TermOfPayment)
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_TermOfPayment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TermOfPayment.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_KeteranganToP_TextChanged(sender As Object, e As EventArgs) Handles txt_KeteranganToP.TextChanged
        KeteranganToP = txt_KeteranganToP.Text
    End Sub


    Private Sub txt_KodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeProject.TextChanged
        KodeProjectProduk = txt_KodeProject.Text
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_KodeProject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeProject.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub btn_PilihKodeProject_Click(sender As Object, e As EventArgs) Handles btn_PilihKodeProject.Click
        If txt_KodeProject.Text = Kosongan Then
            frm_ListDataProject.ResetForm()
        Else
            frm_ListDataProject.KodeProject_Terseleksi = txt_KodeProject.Text
        End If
        frm_ListDataProject.ShowDialog()
        txt_KodeProject.Text = frm_ListDataProject.KodeProject_Terseleksi
    End Sub



    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
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
        txt_TarifPPh.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
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
        txt_TarifPPh.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        If JenisProduk_Induk = JenisProduk_Barang Then
            If PerlakuanPPN = PerlakuanPPN_Dipungut Then
                AktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_Pasal22_Lokal
                lbl_PPhDitanggung.Enabled = False
                txt_PPhDitanggung.Enabled = False
                txt_PPhDitanggung.Text = Kosongan
            Else
                NonAktifkanPPh()
                cmb_JenisPPh.Text = JenisPPh_NonPPh
            End If
        End If
        Perhitungan()
    End Sub
    Private Sub cmb_PerlakuanPPN_Leave(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.Leave
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then
            If JenisPPN <> JenisPPN_NonPPN And PerlakuanPPN = Kosongan Then
                MsgBox("Silakan pilih 'Perlakuan PPN'.")
                cmb_PerlakuanPPN.Focus()
            End If
        End If
    End Sub

    Private Sub cmb_Kontrol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Kontrol.SelectedIndexChanged
    End Sub
    Private Sub cmb_Kontrol_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Kontrol.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_Kontrol_TextChanged(sender As Object, e As EventArgs) Handles cmb_Kontrol.TextChanged
        Kontrol = cmb_Kontrol.Text
        KondisiFormSetelahPerubahan()
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
        KodeProjectProduk_Terseleksi = DataTabelUtama.Item("Kode_Project_Produk", Baris_Terseleksi).Value
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
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTPOPENJUALAN
        frm_InputProduk_Nota.ShowDialog()
        Perhitungan()
        BersihkanSeleksi_TabelProduk()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputProduk_Nota.ResetForm()
        frm_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        frm_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        frm_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        frm_InputProduk_Nota.JenisProduk_PerItem = JenisProdukPerItem_Terseleksi
        frm_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        frm_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        frm_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        frm_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        frm_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        frm_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        frm_InputProduk_Nota.txt_KodeProject.Text = KodeProjectProduk_Terseleksi
        If KodeProjectProduk_Terseleksi = Kosongan Then frm_InputProduk_Nota.cmb_Peruntukan.Text = Peruntukan_NonProject
        If KodeProjectProduk_Terseleksi <> Kosongan Then frm_InputProduk_Nota.cmb_Peruntukan.Text = Peruntukan_Project
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTPOPENJUALAN
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

    Private Sub rdb_JumlahHariJangkaWaktuPenyelesaian_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_JumlahHariJangkaWaktuPenyelesaian.CheckedChanged
        If rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = True Then
            txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = True
            lbl_JumlahHariJangkaWaktuPenyelesaian.Enabled = True
            dtp_TanggalJangkaWaktuPenyelesaian.Enabled = False
        Else
            txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
            lbl_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub rdb_TanggalJangkaWaktuPenyelesaian_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_TanggalJangkaWaktuPenyelesaian.CheckedChanged
        If rdb_TanggalJangkaWaktuPenyelesaian.Checked = True Then
            txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
            txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
            lbl_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
            dtp_TanggalJangkaWaktuPenyelesaian.Enabled = True
        Else
            dtp_TanggalJangkaWaktuPenyelesaian.Enabled = False
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_JumlahHariJangkaWaktuPenyelesaian_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHariJangkaWaktuPenyelesaian.TextChanged
        JumlahHari_JangkaWaktuPenyelesaian = AmbilAngka(txt_JumlahHariJangkaWaktuPenyelesaian.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHariJangkaWaktuPenyelesaian)
        If JumlahHari_JangkaWaktuPenyelesaian > 0 Then
            rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_JumlahHariJangkaWaktuPenyelesaian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHariJangkaWaktuPenyelesaian.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahHariJangkaWaktuPenyelesaian_Click(sender As Object, e As EventArgs) Handles txt_JumlahHariJangkaWaktuPenyelesaian.Click
        rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = True
    End Sub

    Private Sub dtp_TanggalJangkaWaktuPenyelesaian_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJangkaWaktuPenyelesaian.ValueChanged
        Tanggal_JangkaWaktuPenyelesaian = dtp_TanggalJangkaWaktuPenyelesaian.Value
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub dtp_TanggalJangkaWaktuPenyelesaian_Click(sender As Object, e As EventArgs) Handles dtp_TanggalJangkaWaktuPenyelesaian.Click
        rdb_TanggalJangkaWaktuPenyelesaian.Checked = True
    End Sub

    Private Sub txt_JumlahHargaKeseluruhan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHargaKeseluruhan.TextChanged
        JumlahHargaKeseluruhan = AmbilAngka(txt_JumlahHargaKeseluruhan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHargaKeseluruhan)
    End Sub
    Private Sub txt_JumlahHargaKeseluruhan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHargaKeseluruhan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As EventArgs) Handles txt_Diskon_Persen.TextChanged
        'If txt_Diskon_Persen.Text = "," Then
        '    txt_Diskon_Persen.Text = Kosongan
        '    Return
        'End If
        'If txt_Diskon_Persen.Text = Kosongan Then
        '    Diskon_Persen = 0
        'Else
        '    Diskon_Persen = txt_Diskon_Persen.Text
        'End If
        'If Diskon_Persen > 100 Then
        '    MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
        '    txt_Diskon_Persen.Text = Kosongan
        '    txt_Diskon_Persen.Focus()
        '    Return
        'End If
        'KondisiFormSetelahPerubahan()
        'Perhitungan()
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
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then
            If (JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_Jasa) And JenisPPh = JenisPPh_NonPPh Then
                MsgBox("Pilihan ini haris dikonsultasikan dengan pihak terkait.")
            End If
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
            MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        txt_PPhTerutang.Text = DPP * Persen(TarifPPh)
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub
    Private Sub txt_TarifPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh.KeyPress
        HanyaBolehInputAngkaDesimalPlus(sender, e)
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
        If PPhDipotong < 0 Then
            txt_PPhDitanggung.Text = 0
            txt_PPhDitanggung.Focus()
            MsgBox("Silakan isi kolom 'PPh Ditanggung' dengan benar!")
            Return
        End If
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
        Perhitungan()
    End Sub

    Private Sub txt_TotalTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalTagihan.TextChanged
        TotalTagihan = AmbilAngka(txt_TotalTagihan.Text)
        PemecahRibuanUntukTextBox(txt_TotalTagihan)
    End Sub
    Private Sub txt_TotalTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalTagihan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        Catatan = txt_Catatan.Text              'Pengisian ulang value untuk RichTexBox
        KeteranganToP = txt_KeteranganToP.Text  'Pengisian ulang value untuk RichTexBox

        If NomorPO = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor PO'")
            txt_NomorPO.Focus()
            Return
        End If

        If KodeProjectProduk = Kosongan Then
            MsgBox("Silakan isi kolom 'Kode Proyek'.")
            txt_KodeProject.Focus()
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

        If JenisPPh = Kosongan Then
            MsgBox("Silakan pilih 'Jenis PPh'.")
            cmb_JenisPPh.Focus()
            Return
            'Else
            '    If TarifPPh = 0 Then
            '        MsgBox("Silakan isi kolom 'Tarif PPh'.")
            '        txt_TarifPPh.Focus()
            '        Return
            '    End If
        End If

        If JenisPPh <> JenisPPh_NonPPh And TarifPPh = 0 Then
            MsgBox("Silakan isi 'Tarif PPh'.")
            txt_TarifPPh.Focus()
            Return
        End If

        If rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = False And rdb_TanggalJangkaWaktuPenyelesaian.Checked = False Then
            MsgBox("Silakan isi kolom 'Jatuh TempoPenyelesaian'.")
            Return
        End If

        If rdb_JumlahHariJangkaWaktuPenyelesaian.Checked = True Then
            If JumlahHari_JangkaWaktuPenyelesaian = 0 Then
                MsgBox("Silakan isi kolom 'Jumlah Hari'.")
                txt_JumlahHariJangkaWaktuPenyelesaian.Focus()
                Return
            End If
            Tanggal_JangkaWaktuPenyelesaian = TanggalKosongSimpan
        Else
            Tanggal_JangkaWaktuPenyelesaian = TanggalFormatTampilan(dtp_TanggalJangkaWaktuPenyelesaian.Value)
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

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_PO")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorID += 1
                JenisProduk_PerItem = DataTabelUtama.Item("Jenis_Produk_Per_Item", NomorUrutProduk - 1).Value
                KodeProjectProduk = DataTabelUtama.Item("Kode_Project_Produk", NomorUrutProduk - 1).Value
                NamaProduk = DataTabelUtama.Item("Nama_Produk", NomorUrutProduk - 1).Value
                DeskripsiProduk = DataTabelUtama.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value
                JumlahProduk_PerItem = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", NomorUrutProduk - 1).Value)
                SatuanProduk = DataTabelUtama.Item("Satuan_Produk", NomorUrutProduk - 1).Value
                HargaSatuan = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", NomorUrutProduk - 1).Value)
                DiskonPerItem_Persen = Microsoft.VisualBasic.Replace(DataTabelUtama.Item("Diskon_Per_Item_Persen", NomorUrutProduk - 1).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHarga = AmbilAngka(DataTabelUtama.Item("Total_Harga", NomorUrutProduk - 1).Value)
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
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProduk_PerItem & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHarga & "', " &
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
                    " '" & Catatan & "', " &
                    " '" & Kontrol & "', " &
                    " '" & UserAktif & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit Do
            Loop

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
                'frm_SuratJalanPenjualan.TampilkanData()
                'frm_BASTPenjualan.TampilkanData()
            End If
        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            ResetForm()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

End Class