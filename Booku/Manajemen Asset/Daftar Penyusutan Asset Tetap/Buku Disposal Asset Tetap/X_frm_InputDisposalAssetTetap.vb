Imports bcomm

Public Class X_frm_InputDisposalAssetTetap

    Public BulanDisposal_Angka
    Public TanggalTerakhirDijurnal

    Public KodeAsset
    Public TanggalDisposal
    Public NamaAktiva
    Public NilaiSisaBuku
    Public Selisih
    Public NomorFakturPajak
    Public Keterangan

    Public KelompokHarta

    Public HargaPerolehan
    Public HPP
    Public AkumulasiPenyusutan
    Public COA_AkumulasiPenyusutan
    Public NamaAkun_AkumulasiPenyusutan
    Public COA_AssetDisposal
    Public NomorBeritaAcaraDisposal
    Public NomorJV_Closing

    Public NomorBeritaAcaraDisposalSudahAda As Boolean


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If BulanDisposal_Angka = 1 Then
            TanggalTerakhirDijurnal = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunBukuAktif - 1)
        Else
            TanggalTerakhirDijurnal = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDisposal_Angka - 1, TahunBukuAktif)
        End If

        lbl_PerTanggal_1.Text = "( per " & TanggalTerakhirDijurnal & " )"
        lbl_PerTanggal_2.Text = "( per " & TanggalTerakhirDijurnal & " )"

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
        dtp_TanggalDisposal.Value = Today
        txt_NamaAktiva.Text = Kosongan
        txt_NomorBeritaAcaraDisposal.Text = Kosongan
        txt_HargaPerolehan.Text = Kosongan
        txt_AkumulasiPenyusutan.Text = Kosongan
        txt_COA_AkumulasiPenyusutan.Text = Kosongan
        txt_NamaAkun_AkumulasiPenyusutan.Text = Kosongan
        txt_NilaiSisaBuku.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        HPP = 0
        COA_AssetDisposal = Kosongan
        NomorJV_Closing = 0
        NomorBeritaAcaraDisposalSudahAda = False

        ProsesResetForm = False

    End Sub

    Private Sub dtp_TanggalDisposal_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalDisposal.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalDisposal)
        Dim BulanInputan = dtp_TanggalDisposal.Value.Month
        KunciBulanDanTahun_HarusSama(dtp_TanggalDisposal, BulanDisposal_Angka, TahunBukuAktif)
        If ProsesResetForm = False And BulanInputan <> BulanDisposal_Angka Then
            MsgBox("Tanggal Disposal dikunci hanya untuk bulan '" & KonversiAngkaKeBulanString(BulanDisposal_Angka) & "'." & Enter2Baris &
                   "Silahkan Jurnal Penyusutan terlebih dahulu sebelum Disposal Asset, " &
                   "atau hubungi Costumer Service.")
            Return
        End If
        TanggalDisposal = dtp_TanggalDisposal.Value
    End Sub

    Private Sub txt_NamaAktiva_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAktiva.TextChanged
        NamaAktiva = txt_NamaAktiva.Text
    End Sub
    Private Sub txt_NamaAktiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAktiva.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorBeritaAcaraDisposal_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBeritaAcaraDisposal.TextChanged
        NomorBeritaAcaraDisposal = txt_NomorBeritaAcaraDisposal.Text
    End Sub
    Private Sub txt_NomorBeritaAcaraDisposal_Leave(sender As Object, e As EventArgs) Handles txt_NomorBeritaAcaraDisposal.Leave
        If NomorBeritaAcaraDisposal <> Kosongan Then
            AksesDatabase_General(Buka)
            cmd = New Odbc.OdbcCommand(" SELECT Kode_Closing FROM tbl_DataAsset " &
                                       " WHERE Kode_Closing = '" & NomorBeritaAcaraDisposal & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                NomorBeritaAcaraDisposalSudahAda = True
            Else
                NomorBeritaAcaraDisposalSudahAda = False
            End If
            AksesDatabase_General(Tutup)
        End If
        If NomorBeritaAcaraDisposalSudahAda = True Then
            MsgBox("Nomor Disposal sudah pernah diinput." & Enter2Baris &
                   "Silakan input nomor yang lain.")
            txt_NomorBeritaAcaraDisposal.Text = Kosongan
            txt_NomorBeritaAcaraDisposal.Focus()
            Return
        End If
    End Sub

    Private Sub dtp_TanggalPerolehan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPerolehan.ValueChanged
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

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If NomorBeritaAcaraDisposalSudahAda = True Then Return


        Pilihan = MessageBox.Show("Lanjutkan Disposal Asset..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'Pengisian Ulang Variabel :
        Keterangan = txt_Keterangan.Text
        HPP = NilaiSisaBuku

        If NomorBeritaAcaraDisposal = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor Disposal'.")
            txt_NomorBeritaAcaraDisposal.Focus()
            Return
        End If

        If KelompokHarta <> KelompokHarta_Tanah And COA_AkumulasiPenyusutan = Kosongan Then
            MsgBox("Silakan pilih Kode Akun untuk 'Akumulasi Penyusutan'.")
            txt_COA_AkumulasiPenyusutan.Focus()
            Return
        End If

        SimpanJurnalClosing()
        If jur_StatusPenyimpananJurnal_Lengkap = True Then UpdateDataAsset()
        If StatusSuntingDatabase = True Then
            MsgBox("Data Disposal Asset BERHASIL disimpan dan diposting ke Jurnal.")
            If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.RefreshTampilanData()
            Me.Close()
        Else
            MsgBox("Data Disposal Asset GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Sub SimpanJurnalClosing()

        'Pengisian Ulang Variabel :
        Keterangan = txt_Keterangan.Text

        ResetValueJurnal()
        SistemPenomoranOtomatis_NomorJV()
        NomorJV_Closing = jur_NomorJV
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalDisposal)
        jur_JenisJurnal = JenisJurnal_DisposalAsset
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = NomorFakturPajak
        jur_KodeLawanTransaksi = Kosongan
        jur_NamaLawanTransaksi = Kosongan
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        'Eliminasi Beberapa Baris Jurnal :
        If Not (KelompokHarta <> KelompokHarta_Tanah) Then AkumulasiPenyusutan = 0

        'Simpan Jurnal
        ___jurDebet(KodeTautanCOA_HPPPenjualanDisposalAsset, HPP)
        ___jurDebet(COA_AkumulasiPenyusutan, AkumulasiPenyusutan)
        _______jurKredit(COA_AssetDisposal, HargaPerolehan)

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            jur_StatusPenyimpananJurnal_Lengkap = True
        Else
            jur_StatusPenyimpananJurnal_Lengkap = False
            PesanUntukProgrammer("Data Asset GAGAL diposting ke Jurnal...!!!")
        End If

        ResetValueJurnal()

    End Sub

    Sub UpdateDataAsset()
        AksesDatabase_General(Buka)
        cmd = New Odbc.OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                   " Tanggal_Closing        = '" & TanggalFormatSimpan(TanggalDisposal) & "', " &
                                   " Harga_Jual             = '" & 0 & "', " &
                                   " Akumulasi_Penyusutan   = '" & AkumulasiPenyusutan & "', " &
                                   " Kode_Closing           = '" & NomorBeritaAcaraDisposal & "', " &
                                   " Keterangan             = '" & Keterangan & "', " &
                                   " Nomor_JV_Closing       = '" & NomorJV_Closing & "' " &
                                   " WHERE Kode_Asset       = '" & KodeAsset & "' ",
                                   KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        AksesDatabase_General(Tutup)
        If StatusSuntingDatabase = False Then PesanUntukProgrammer("Data Asset GAGAL diperbarui...!!!")
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class