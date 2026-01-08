Imports bcomm

Public Class X_frm_JualAsset

    Public JenisProduk_Induk

    Public BulanDijual_Angka
    Public TanggalTerakhirDijurnal

    Public KodeAsset
    Public TanggalTransaksi
    Public NomorUrutProduk
    Public JenisProduk_PerItem
    Public NomorSJBAST
    Public TanggalSJBAST
    Public TanggalDiterimaSJBAST
    Public NamaProduk
    Public DeskripsiProduk
    Public JumlahProduk
    Public SatuanProduk
    Public HargaSatuan
    Public NilaiSisaBuku
    Public JumlahHarga
    Public DiskonPerItem_Persen
    Public DiskonPerItem_Rp
    Public TotalHarga
    Public Selisih
    Public Keterangan
    Public AdaPPh As Boolean

    Public KelompokHarta

    Public HargaPerolehan
    Public HPP
    Public AkumulasiPenyusutan
    Public COA_AkumulasiPenyusutan
    Public NamaAkun_AkumulasiPenyusutan
    Public COA_AssetDijual
    Public NomorPenjualanAsset
    Public NomorJV_Closing

    Dim TanggalPerolehan
    Dim BulanPerolehan
    Dim TahunPerolehan

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If BulanDijual_Angka = 1 Then
            TanggalTerakhirDijurnal = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunBukuAktif - 1)
        Else
            TanggalTerakhirDijurnal = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDijual_Angka - 1, TahunBukuAktif)
        End If

        'Logika di bawah ini adalah untuk Asset yang baru dibeli kemudian dijual di Bulan dan Tahun yang sama,
        'dan tentunya belum ada Jurnal Penyusutan satu pun :
        If BulanDijual_Angka = BulanPerolehan And TahunPerolehan = TahunBukuAktif Then
            lbl_PerTanggal_1.Text = "( per " & TanggalPerolehan & " )"
            lbl_PerTanggal_2.Text = "( per " & TanggalPerolehan & " )"
        Else
            lbl_PerTanggal_1.Text = "( per " & TanggalTerakhirDijurnal & " )"
            lbl_PerTanggal_2.Text = "( per " & TanggalTerakhirDijurnal & " )"
        End If

        If KelompokHarta = KelompokHarta_Tanah Then
            grb_AkumulasiPenyusutan.Enabled = False
            txt_COA_AkumulasiPenyusutan.Text = Kosongan
            txt_NamaAkun_AkumulasiPenyusutan.Text = Kosongan
        Else
            grb_AkumulasiPenyusutan.Enabled = True
        End If

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        'Jangan me-reset Variabel 'BulanPengunci'...!!! Sengaja ditiadakan di sub ini...!!!
        KodeAsset = Kosongan
        NomorUrutProduk = 0
        JenisProduk_PerItem = Kosongan
        JumlahProduk = 0
        dtp_TanggalTransaksi.Value = Today
        txt_NamaAktiva.Text = Kosongan
        txt_DeskripsiProduk.Text = Kosongan
        txt_SatuanProduk.Text = Kosongan
        txt_HargaPerolehan.Text = Kosongan
        txt_AkumulasiPenyusutan.Text = Kosongan
        txt_COA_AkumulasiPenyusutan.Text = Kosongan
        txt_NamaAkun_AkumulasiPenyusutan.Text = Kosongan
        txt_NilaiSisaBuku.Text = Kosongan
        txt_HargaProduk.Text = Kosongan
        txt_Selisih.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        AdaPPh = False
        HPP = 0
        COA_AssetDijual = Kosongan
        NomorPenjualanAsset = Kosongan
        NomorJV_Closing = 0


        ProsesResetForm = False

    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalTransaksi)
        Dim BulanInputan = dtp_TanggalTransaksi.Value.Month
        KunciBulanDanTahun_HarusSama(dtp_TanggalTransaksi, BulanDijual_Angka, TahunBukuAktif)
        If ProsesResetForm = False And BulanInputan <> BulanDijual_Angka Then
            MsgBox("Tanggal Transaksi dikunci hanya untuk bulan '" & KonversiAngkaKeBulanString(BulanDijual_Angka) & "'." & Enter2Baris &
                   "Silahkan Jurnal Penyusutan terlebih dahulu sebelum transaksi Penjualan Asset, " &
                   "atau hubungi Costumer Service.")
            Return
        End If
        TanggalTransaksi = dtp_TanggalTransaksi.Value
    End Sub

    Private Sub txt_NamaAktiva_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAktiva.TextChanged
        NamaProduk = txt_NamaAktiva.Text
    End Sub
    Private Sub txt_NamaAktiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAktiva.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DeskripsiProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_DeskripsiProduk.TextChanged
        DeskripsiProduk = txt_DeskripsiProduk.Text
    End Sub

    Private Sub txt_SatuanProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_SatuanProduk.TextChanged
        SatuanProduk = txt_SatuanProduk.Text
    End Sub

    Private Sub dtp_TanggalPerolehan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPerolehan.ValueChanged
        TanggalPerolehan = TanggalFormatTampilan(dtp_TanggalPerolehan.Value)
        BulanPerolehan = AmbilBulanAngka_DariTanggal(dtp_TanggalPerolehan.Value)
        TahunPerolehan = AmbilTahun_DariTanggal(dtp_TanggalPerolehan.Value)
    End Sub
    Private Sub dtp_TanggalPerolehan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtp_TanggalPerolehan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_HargaPerolehan_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaPerolehan.TextChanged
        HargaPerolehan = AmbilAngka(txt_HargaPerolehan.Text)
        PemecahRibuanUntukTextBox(txt_HargaPerolehan)
    End Sub
    Private Sub txt_HargaPerolehan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaPerolehan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AkumulasiPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_AkumulasiPenyusutan.TextChanged
        AkumulasiPenyusutan = AmbilAngka(txt_AkumulasiPenyusutan.Text)
        PemecahRibuanUntukTextBox(txt_AkumulasiPenyusutan)
    End Sub
    Private Sub txt_AkumulasiPenyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AkumulasiPenyusutan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_COA_AkumulasiPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_AkumulasiPenyusutan.TextChanged
        COA_AkumulasiPenyusutan = txt_COA_AkumulasiPenyusutan.Text
        txt_NamaAkun_AkumulasiPenyusutan.Text = AmbilValue_NamaAkun(COA_AkumulasiPenyusutan)
    End Sub
    Private Sub txt_COA_AkumulasiPenyusutan_Click(sender As Object, e As EventArgs) Handles txt_COA_AkumulasiPenyusutan.Click
        btn_PilihCOA_Click(sender, e)
    End Sub
    Private Sub txt_COA_AkumulasiPenyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_AkumulasiPenyusutan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub btn_PilihCOA_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_Asset.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_AkumulasiPenyusutan
        If txt_COA_AkumulasiPenyusutan.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COA_AkumulasiPenyusutan.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_AkumulasiPenyusutan.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COA_AkumulasiPenyusutan.Text = win_ListCOA.COATerseleksi
    End Sub

    Private Sub txt_NamaAkun_AkumulasiPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_AkumulasiPenyusutan.TextChanged
        NamaAkun_AkumulasiPenyusutan = txt_NamaAkun_AkumulasiPenyusutan.Text
    End Sub
    Private Sub txt_NamaAkun_AkumulasiPenyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun_AkumulasiPenyusutan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NilaiSisaBuku_TextChanged(sender As Object, e As EventArgs) Handles txt_NilaiSisaBuku.TextChanged
        NilaiSisaBuku = AmbilAngka(txt_NilaiSisaBuku.Text)
        PemecahRibuanUntukTextBox(txt_NilaiSisaBuku)
    End Sub
    Private Sub txt_NilaiSisaBuku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NilaiSisaBuku.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_HargaProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaProduk.TextChanged
        HargaSatuan = AmbilAngka(txt_HargaProduk.Text)
        PemecahRibuanUntukTextBox(txt_HargaProduk)
        txt_Selisih.Text = HargaSatuan - NilaiSisaBuku
    End Sub
    Private Sub txt_HargaProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaProduk.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Selisih_TextChanged(sender As Object, e As EventArgs) Handles txt_Selisih.TextChanged
        Selisih = AmbilAngka(txt_Selisih.Text)
        PemecahRibuanUntukTextBox(txt_Selisih)
    End Sub
    Private Sub txt_Selisih_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Selisih.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        'Pengisian Ulang Variabel :
        Keterangan = txt_Keterangan.Text
        HPP = NilaiSisaBuku

        If KelompokHarta <> KelompokHarta_Tanah And COA_AkumulasiPenyusutan = Kosongan Then
            MsgBox("Silakan pilih Kode Akun untuk 'Akumulasi Penyusutan'.")
            txt_COA_AkumulasiPenyusutan.Focus()
            Return
        End If

        If HargaSatuan = 0 Then
            MsgBox("Silakan isi kolom 'Harga'.")
            txt_HargaProduk.Focus()
            Return
        End If

        NomorUrutProduk = 1
        NomorSJBAST = Kosongan
        TanggalSJBAST = TanggalFormatTampilan(Today)
        TanggalDiterimaSJBAST = TanggalFormatTampilan(Today)
        JumlahProduk = 1
        DiskonPerItem_Persen = 0
        DiskonPerItem_Rp = 0
        JumlahHarga = JumlahProduk * HargaSatuan
        TotalHarga = JumlahHarga - DiskonPerItem_Rp

        frm_Input_InvoicePenjualan.ResetForm()
        frm_Input_InvoicePenjualan.FungsiForm = FungsiForm_TAMBAH
        frm_Input_InvoicePenjualan.JenisProduk_Induk = JenisProduk_Induk
        frm_Input_InvoicePenjualan.InvoiceDenganPO = False
        frm_Input_InvoicePenjualan.JualAsset = True
        frm_Input_InvoicePenjualan.dtp_TanggalInvoice.Value = TanggalTransaksi
        frm_Input_InvoicePenjualan.DataTabelUtama.
            Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST,
                     NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                     JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHarga)
        If AdaPPh = True Then
            frm_Input_InvoicePenjualan.cmb_JenisPPh.Text = JenisPPh_Pasal42
            frm_Input_InvoicePenjualan.txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(2.5)
            frm_Input_InvoicePenjualan.KodeSetoran = KodeSetoran_402
        Else
            frm_Input_InvoicePenjualan.cmb_JenisPPh.Text = Kosongan
            frm_Input_InvoicePenjualan.txt_TarifPPh.Text = Kosongan
            frm_Input_InvoicePenjualan.KodeSetoran = KodeSetoran_Non
        End If
        frm_Input_InvoicePenjualan.txt_Catatan.Text = Keterangan
        frm_Input_InvoicePenjualan.KelompokHarta = KelompokHarta
        frm_Input_InvoicePenjualan.ShowDialog()
        If frm_Input_InvoicePenjualan.PenyimpananInvoicePenjualan = True Then
            If frm_Input_InvoicePenjualan.AdaPenyimpananjurnal = True Then SimpanJurnalClosing()
            UpdateDataAsset()
            frm_BOOKU.BukaModul_BukuPenjualanAsset()
            Me.Close()
        End If

    End Sub

    Sub SimpanJurnalClosing()

        'Pengisian Ulang Variabel :
        Keterangan = txt_Keterangan.Text

        ResetValueJurnal()
        SistemPenomoranOtomatis_NomorJV()
        NomorJV_Closing = jur_NomorJV
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
        jur_JenisJurnal = JenisJurnal_Asset
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = Kosongan
        jur_NamaLawanTransaksi = Kosongan
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        'Eliminasi Beberapa Baris Jurnal :
        If Not (KelompokHarta <> KelompokHarta_Tanah) Then AkumulasiPenyusutan = 0

        'Simpan Jurnal
        ___jurDebet(KodeTautanCOA_HPPPenjualanDisposalAsset, HPP)
        ___jurDebet(COA_AkumulasiPenyusutan, AkumulasiPenyusutan)
        _______jurKredit(COA_AssetDijual, HargaPerolehan)

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            jur_StatusPenyimpananJurnal_Lengkap = True
        Else
            jur_StatusPenyimpananJurnal_Lengkap = False
        End If

        ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

    End Sub

    Sub UpdateDataAsset()

        If StatusSuntingDatabase = True Then
            AksesDatabase_General(Buka)
            cmd = New Odbc.OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                       " Tanggal_Closing        = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                       " Harga_Jual             = '" & HargaSatuan & "', " &
                                       " Akumulasi_Penyusutan   = '" & AkumulasiPenyusutan & "', " &
                                       " Kode_Closing           = '" & NomorPenjualanAsset & "', " &
                                       " Keterangan             = '" & Keterangan & "', " &
                                       " Nomor_JV_Closing       = '" & NomorJV_Closing & "' " &
                                       " WHERE Kode_Asset       = '" & KodeAsset & "' ",
                                       KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)
        End If

        If StatusSuntingDatabase = True Then
            MsgBox("Data Asset BERHASIL diperbarui.")
            If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.RefreshTampilanData()
            PesanUntukProgrammer("NANTI DIBIKINKAN CODING OPSI UNTUK MENCETAK INVOICE, DISINI....!!!!!!")
        Else
            PesanUntukProgrammer("Data Asset GAGAL diperbarui.")
        End If

        'Pilihan = MessageBox.Show("Apakah Anda ingin mencetaknya..?", "Perhatian..!", MessageBoxButtons.YesNo)
        'If Pilihan = vbYes Then btn_Cetak_Click(sender, e)

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class