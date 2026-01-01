Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputHutangBankLeasing

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public BankLeasing
    Dim TabelPengawasan
    Dim TabelAngsuran
    Dim KolomNomorBPH
    Dim AwalanNomorBPH
    Dim COAKredit
    Dim JenisJurnal

    Public NomorID
    Public NomorBPH
    Public NomorJV_Persetujuan
    Public NomorJV_Pencairan
    Dim TanggalPersetujuan
    Dim KodeKreditur
    Dim NamaKreditur
    Dim JenisWP_Kreditur
    Dim JumlahPinjaman
    Dim TanggalJatuhTempo
    Dim NomorKontrak
    Dim TanggalPencairan
    Dim BiayaAdministrasiKontrak
    Dim BiayaNotaris
    Dim BiayaPPh
    Dim Keterangan

    Dim SudahDicairkan As Boolean
    Dim JumlahPencairan
    Dim BankPencairan
    Dim KodeAkun_Bank

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If BankLeasing = Kosongan Then PesanUntukProgrammer("Tentukan dulu, ini untuk Bank atau Leasing..???!!!!")

        If BankLeasing = bl_Bank Then
            TabelPengawasan = "tbl_PengawasanHutangBank"
            TabelAngsuran = "tbl_JadwalAngsuranHutangBank"
            KolomNomorBPH = "Nomor_BPHB"
            AwalanNomorBPH = AwalanBPHB_PlusTahunBuku
            COAKredit = KodeTautanCOA_HutangBank
            JenisJurnal = JenisJurnal_HutangBank
        End If

        If BankLeasing = bl_Leasing Then
            TabelPengawasan = "tbl_PengawasanHutangLeasing"
            TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing"
            KolomNomorBPH = "Nomor_BPHL"
            AwalanNomorBPH = AwalanBPHL_PlusTahunBuku
            COAKredit = KodeTautanCOA_HutangLeasing
            JenisJurnal = JenisJurnal_HutangLeasing
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            JudulForm = "Input Hutang " & BankLeasing
            SistemPenomoranOtomatis()

        End If

        If FungsiForm = FungsiForm_EDIT Then

            JudulForm = "Edit Hutang " & BankLeasing
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                dtp_TanggalPencairan.Value = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
                TanggalPencairan = TanggalFormatTampilan(dtp_TanggalPencairan.Value)
                txt_BiayaAdministrasiKontrak.Text = dr.Item("Biaya_Administrasi_Kontrak")
                txt_BiayaNotaris.Text = dr.Item("Biaya_Notaris")
                txt_BiayaPPh.Text = dr.Item("Biaya_PPh")
                KodeAkun_Bank = dr.Item("COA_Debet")
                NomorJV_Pencairan = dr.Item("Nomor_JV_Pencairan")
            End If
            AksesDatabase_Transaksi(Tutup)
            If NomorJV_Pencairan = 0 Then
                dtp_TanggalPencairan.Value = Today
                TanggalPencairan = TanggalKosong
                chk_Pencairan.Checked = False
            Else
                chk_Pencairan.Checked = True
                cmb_BankPencairan.Text = KonversiCOAKeSaranaPembayaran(KodeAkun_Bank)
            End If

        End If


        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            lbl_TanggalPersetujuan.Enabled = True
            lbl_JumlahPinjaman.Text = "Jumlah Pinjaman"
            dtp_TanggalPersetujuan.Visible = True
            chk_Pencairan.Enabled = True
            lbl_TanggalPencairan.Enabled = True
            dtp_TanggalPencairan.Visible = True
            lbl_BankPencairan.Enabled = True
            cmb_BankPencairan.Enabled = True
            lbl_BiayaAdministrasi.Enabled = True
            txt_BiayaAdministrasiKontrak.Enabled = True
            lbl_BiayaNotaris.Enabled = True
            txt_BiayaNotaris.Enabled = True
            lbl_BiayaPPh.Enabled = True
            txt_BiayaPPh.Enabled = True
        Else
            lbl_TanggalPersetujuan.Enabled = False
            dtp_TanggalPersetujuan.Visible = False
            lbl_JumlahPinjaman.Text = "Saldo Akhir"
            chk_Pencairan.Checked = True
            chk_Pencairan.Enabled = False
            lbl_TanggalPencairan.Enabled = False
            dtp_TanggalPencairan.Visible = False
            lbl_BankPencairan.Enabled = False
            cmb_BankPencairan.Enabled = False
            lbl_BiayaAdministrasi.Enabled = False
            txt_BiayaAdministrasiKontrak.Enabled = False
            lbl_BiayaNotaris.Enabled = False
            txt_BiayaNotaris.Enabled = False
            lbl_BiayaPPh.Enabled = False
            txt_BiayaPPh.Enabled = False
            TanggalPersetujuan = TanggalKosong
            TanggalPencairan = TanggalKosong
        End If


        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        BankLeasing = Kosongan

        NomorID = 0
        NomorBPH = Kosongan
        NomorJV_Persetujuan = 0
        NomorJV_Pencairan = 0
        dtp_TanggalPersetujuan.Value = Today
        txt_KodeKreditur.Text = Kosongan
        txt_NamaKreditur.Text = Kosongan
        JenisWP_Kreditur = Kosongan
        txt_JumlahPinjaman.Text = Kosongan
        dtp_TanggalJatuhTempo.Value = Today
        txt_NomorKontrak.Text = Kosongan
        chk_Pencairan.Checked = False
        dtp_TanggalPencairan.Value = Today
        KontenComboDaftarBank_Public(cmb_BankPencairan)
        txt_BiayaAdministrasiKontrak.Text = Kosongan
        txt_BiayaNotaris.Text = Kosongan
        txt_BiayaPPh.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        SudahDicairkan = False
        JumlahPencairan = 0
        KodeAkun_Bank = Kosongan

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan) + 1
        NomorBPH = AwalanNomorBPH & NomorID

    End Sub

    Private Sub dtp_TanggalPersetujuan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPersetujuan.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalPersetujuan)
        TanggalPersetujuan = dtp_TanggalPersetujuan.Value
    End Sub

    Private Sub txt_KodeKreditur_Click(sender As Object, e As EventArgs) Handles txt_KodeKreditur.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeKreditur_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeKreditur.TextChanged
        KodeKreditur = txt_KodeKreditur.Text
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeKreditur & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JenisWP_Kreditur = dr.Item("Jenis_WP")
        End If
        AksesDatabase_General(Tutup)
        If JenisWP_Kreditur = Kosongan Then PesanUntukProgrammer("Jenis PPh belum ditentukan...!!!")
    End Sub
    Private Sub txt_KodeKreditur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeKreditur.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.ResetForm()
        If txt_KodeKreditur.Text <> Kosongan Then
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeKreditur.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaKreditur.Text
        End If
        frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
        frm_ListMitra.PilihLembagaKeuangan = Pilihan_Ya
        frm_ListMitra.ShowDialog()
        txt_KodeKreditur.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaKreditur.Text = frm_ListMitra.NamaMitraTerseleksi
    End Sub

    Private Sub txt_NamaKreditur_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaKreditur.TextChanged
        NamaKreditur = txt_NamaKreditur.Text
    End Sub
    Private Sub txt_NamaKreditur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaKreditur.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahPinjaman_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPinjaman.TextChanged
        JumlahPinjaman = AmbilAngka(txt_JumlahPinjaman.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox(txt_JumlahPinjaman)
    End Sub
    Private Sub txt_JumlahPinjaman_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPinjaman.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.ValueChanged
        KunciTahun_TidakBolehKurangDariTahunBukuAktif(dtp_TanggalJatuhTempo)
        TanggalJatuhTempo = dtp_TanggalJatuhTempo.Value
    End Sub

    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub

    Private Sub chk_Pencairan_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Pencairan.CheckedChanged

        If chk_Pencairan.Checked = True Then
            grb_Pencairan.Enabled = True
            SudahDicairkan = True
            TanggalPencairan = dtp_TanggalPencairan.Value
        Else
            grb_Pencairan.Enabled = False
            SudahDicairkan = False
            TanggalPencairan = TanggalKosong
            cmb_BankPencairan.Text = Kosongan
            KodeAkun_Bank = Kosongan
            txt_BiayaAdministrasiKontrak.Text = Kosongan
            txt_BiayaNotaris.Text = Kosongan
            txt_BiayaPPh.Text = Kosongan
        End If

    End Sub

    Private Sub dtp_TanggalPencairan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPencairan.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalPencairan)
        TanggalPencairan = dtp_TanggalPencairan.Value
    End Sub

    Private Sub cmb_BankPencairan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BankPencairan.SelectedIndexChanged
    End Sub
    Private Sub cmb_BankPencairan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_BankPencairan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_BankPencairan_TextChanged(sender As Object, e As EventArgs) Handles cmb_BankPencairan.TextChanged
        BankPencairan = cmb_BankPencairan.Text
        KodeAkun_Bank = KonversiSaranaPembayaranKeCOA(BankPencairan)
    End Sub

    Private Sub txt_BiayaAdministrasiKontrak_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiKontrak.TextChanged
        BiayaAdministrasiKontrak = AmbilAngka(txt_BiayaAdministrasiKontrak.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiKontrak)
    End Sub
    Private Sub txt_BiayaAdministrasiKontrak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaAdministrasiKontrak.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_BiayaNotaris_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaNotaris.TextChanged
        BiayaNotaris = AmbilAngka(txt_BiayaNotaris.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox(txt_BiayaNotaris)
    End Sub
    Private Sub txt_BiayaNotaris_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaNotaris.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_BiayaPPh_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaPPh.TextChanged
        Perhitungan()
        BiayaPPh = AmbilAngka(txt_BiayaPPh.Text)
        PemecahRibuanUntukTextBox(txt_BiayaPPh)
    End Sub
    Private Sub txt_BiayaPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaPPh.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Sub Perhitungan()
        JumlahPencairan = JumlahPinjaman - (BiayaAdministrasiKontrak + BiayaNotaris)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Value (Ini penting..! Jangan dihapus..!!!) :
        '------------------------------------------------------------
        If chk_Pencairan.Checked = True Then
            SudahDicairkan = True
        Else
            SudahDicairkan = False
            TanggalPencairan = TanggalKosong
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            SudahDicairkan = True
            TanggalPersetujuan = TanggalKosong
            TanggalPencairan = TanggalKosong
        End If
        Keterangan = txt_Keterangan.Text
        '------------------------------------------------------------

        If KodeKreditur = Kosongan Then
            MsgBox("Silakan isi kolom 'Kreditur'.")
            txt_KodeKreditur.Focus()
            Return
        End If

        If JumlahPinjaman = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Pinjaman'.")
            txt_JumlahPinjaman.Focus()
            Return
        End If

        If NomorKontrak = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor Kontrak'.")
            txt_NomorKontrak.Focus()
            Return
        End If

        If SudahDicairkan = True Then

            If JenisTahunBuku = JenisTahunBuku_NORMAL Then

                If BankPencairan = Kosongan Then
                    MsgBox("Silakan pilih 'Bank' untuk pencairan.")
                    cmb_BankPencairan.Focus()
                    Return
                End If

            End If

        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            SistemPenomoranOtomatis_NomorJV()
            NomorJV_Persetujuan = jur_NomorJV

            If SudahDicairkan = True Then
                NomorJV_Pencairan = NomorJV_Persetujuan + 1
            Else
                NomorJV_Pencairan = 0 'Ini Sebetulnya tidak diperlukan. Tapi ga apa-apa buat jaga-jaga.
            End If

            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                NomorJV_Persetujuan = 0
                NomorJV_Pencairan = 0
            End If

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPH & "', " &
                                  " '" & KodeKreditur & "', " &
                                  " '" & NamaKreditur & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPersetujuan) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPencairan) & "', " &
                                  " '" & JumlahPinjaman & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & BiayaAdministrasiKontrak & "', " &
                                  " '" & BiayaNotaris & "', " &
                                  " '" & BiayaPPh & "', " &
                                  " '" & KodeAkun_Bank & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV_Persetujuan & "', " &
                                  " '" & NomorJV_Pencairan & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            Dim NomorJV_Pencairan_Lama = NomorJV_Pencairan

            If SudahDicairkan = True Then
                If NomorJV_Pencairan = 0 Then
                    SistemPenomoranOtomatis_NomorJV()
                    NomorJV_Pencairan = jur_NomorJV
                End If
            Else
                NomorJV_Pencairan = 0 'Ini diperlukan. Jangan dihapus..!!!
            End If

            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                NomorJV_Persetujuan = 0
                NomorJV_Pencairan = 0
            End If

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  KolomNomorBPH & "             = '" & NomorBPH & "', " &
                                  " Kode_Kreditur               = '" & KodeKreditur & "', " &
                                  " Nama_Kreditur               = '" & NamaKreditur & "', " &
                                  " Tanggal_Persetujuan         = '" & TanggalFormatSimpan(TanggalPersetujuan) & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Tanggal_Pencairan           = '" & TanggalFormatSimpan(TanggalPencairan) & "', " &
                                  " Jumlah_Pinjaman             = '" & JumlahPinjaman & "', " &
                                  " Nomor_Kontrak               = '" & NomorKontrak & "', " &
                                  " Biaya_Administrasi_Kontrak  = '" & BiayaAdministrasiKontrak & "', " &
                                  " Biaya_Notaris               = '" & BiayaNotaris & "', " &
                                  " Biaya_PPh                   = '" & BiayaPPh & "', " &
                                  " COA_Debet                   = '" & KodeAkun_Bank & "', " &
                                  " Keterangan                  = '" & Keterangan & "', " &
                                  " Nomor_JV_Persetujuan        = '" & NomorJV_Persetujuan & "', " &
                                  " Nomor_JV_Pencairan          = '" & NomorJV_Pencairan & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Perbarui Data Angsuran :
            cmd = New OdbcCommand(" UPDATE " & TabelAngsuran & " SET " &
                                  " Kode_Kreditur               = '" & KodeKreditur & "' " &
                                  " WHERE " & KolomNomorBPH & " = '" & NomorBPH & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()


            'Hapus dulu Data Jurnal yang Lama :
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then

                cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                           " WHERE Nomor_JV = '" & NomorJV_Persetujuan & "' ", KoneksiDatabaseTransaksi)
                cmdHAPUS_ExecuteNonQuery()

                cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                           " WHERE Nomor_JV = '" & NomorJV_Pencairan_Lama & "' ", KoneksiDatabaseTransaksi)
                cmdHAPUS_ExecuteNonQuery()

                jur_NomorJV = NomorJV_Persetujuan

                AksesDatabase_Transaksi(Tutup)

            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And StatusSuntingDatabase = True Then

            '================= JURNAL PERSETUJUAN ========================
            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalPersetujuan)
            jur_JenisJurnal = JenisJurnal
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = Kosongan
            jur_NomorInvoice = Kosongan
            jur_NomorFakturPajak = Kosongan
            jur_KodeLawanTransaksi = KodeKreditur
            jur_NamaLawanTransaksi = NamaKreditur
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeTautanCOA_PiutangLainnya, JumlahPinjaman)
            _______jurKredit(COAKredit, JumlahPinjaman)


            '================= JURNAL PENCAIRAN ========================

            If SudahDicairkan = True Then

                jur_NomorJV = NomorJV_Pencairan 'Ini tetap diposisikan di paling atas. Jangan disimpan di bawah Sub ResetValueJurnal()

                ResetValueJurnal()
                jur_TanggalTransaksi = TanggalFormatSimpan(TanggalPencairan)
                jur_JenisJurnal = JenisJurnal
                jur_KodeDokumen = Kosongan
                jur_NomorPO = Kosongan
                jur_KodeProject = Kosongan
                jur_Referensi = Kosongan
                jur_TanggalInvoice = Kosongan
                jur_NomorInvoice = Kosongan
                jur_NomorFakturPajak = Kosongan
                jur_KodeLawanTransaksi = KodeKreditur
                jur_NamaLawanTransaksi = NamaKreditur
                jur_UraianTransaksi = Keterangan
                jur_Direct = 0

                'Simpan Jurnal :
                ___jurDebet(KodeAkun_Bank, JumlahPencairan)
                ___jurDebet(KodeTautanCOA_BiayaAdministrasiPerjanjian, BiayaAdministrasiKontrak)
                ___jurDebet(KodeTautanCOA_BiayaSertifikasiDanLegalitas, BiayaNotaris)
                ___jurDebet(KodeTautanCOA_BiayaPPhPasal21, BiayaPPh)
                _______jurKredit(KodeTautanCOA_PiutangLainnya, JumlahPinjaman)
                _______jurKredit(KodeTautanCOA_HutangPPhPasal21, BiayaPPh)

            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If BankLeasing = bl_Bank Then usc_BukuPengawasanHutangBank.TampilkanData()
            If BankLeasing = bl_Leasing Then usc_BukuPengawasanHutangLeasing.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class