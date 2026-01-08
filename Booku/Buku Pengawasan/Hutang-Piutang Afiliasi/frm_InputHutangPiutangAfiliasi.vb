Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputHutangPiutangAfiliasi


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim TanggalPinjam
    Dim NomorBP
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahPinjaman
    Dim SaldoAwal
    Dim TanggalJatuhTempo
    Dim NomorKontrak
    Dim Keterangan
    Public NomorJV

    Public HutangPiutang = Kosongan
    Dim TabelPengawasan
    Dim KolomNomorBP
    Dim AwalanNomorBP
    Dim KolomCOASaranaPembayaran
    Dim COAUtama

    Dim AdaJatuhTemponya As Boolean


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!!!!")

        Select Case HutangPiutang
            Case hp_Hutang
                TabelPengawasan = "tbl_PengawasanHutangAfiliasi"
                KolomNomorBP = "Nomor_BPHA"
                KolomCOASaranaPembayaran = "COA_Debet"
                COAUtama = KodeTautanCOA_HutangAfiliasi
                AwalanNomorBP = AwalanBPHA_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPHA"
                lbl_KodeLawanTransaksi.Text = "Kode Kreditur"
                lbl_NamaLawanTransaksi.Text = "Nama Kreditur"
                lbl_SaranaPembayaran.Text = "Sarana Pencairan"
            Case hp_Piutang
                TabelPengawasan = "tbl_PengawasanPiutangAfiliasi"
                KolomNomorBP = "Nomor_BPPA"
                KolomCOASaranaPembayaran = "COA_Kredit"
                COAUtama = KodeTautanCOA_PiutangAfiliasi
                AwalanNomorBP = AwalanBPPA_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPPA"
                lbl_KodeLawanTransaksi.Text = "Kode Debitur"
                lbl_NamaLawanTransaksi.Text = "Nama Debitur"
                lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
            Case Else
                PesanUntukProgrammer("Tentukan dulu, Hutang atau Piutang...!!!")
        End Select

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input " & HutangPiutang & " Afiliasi"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit " & HutangPiutang & " Afiliasi"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                COASaranaPembayaran = dr.Item(KolomCOASaranaPembayaran)
                txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
                cmb_DitanggungOleh.Text = dr.Item("Ditanggung_Oleh")
                If TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo")) = TanggalKosong Then
                    AdaJatuhTemponya = False
                    chk_JatuhTempo.Checked = False
                    dtp_TanggalJatuhTempo.Value = TanggalIni
                Else
                    AdaJatuhTemponya = True
                    chk_JatuhTempo.Checked = True
                End If
            End If
            AksesDatabase_Transaksi(Tutup)
            If HutangPiutang = hp_Hutang Then
                cmb_SaranaPembayaran.Text = KonversiCOAKeSaranaPembayaran(COASaranaPembayaran)
                If DitanggungOleh = DitanggungOleh_Perusahaan Then
                    txt_JumlahPinjaman.Text = JumlahPinjaman - BiayaAdministrasiBank
                End If
            Else
                cmb_SaranaPembayaran.Text = Kosongan
            End If
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            dtp_TanggalPinjam.Visible = True
            lbl_SaranaPembayaran.Enabled = True
            cmb_SaranaPembayaran.Enabled = True
            If HutangPiutang = hp_Piutang Then
                lbl_SaranaPembayaran.Enabled = False
                cmb_SaranaPembayaran.Enabled = False
            End If
        Else
            dtp_TanggalPinjam.Visible = False
            lbl_SaranaPembayaran.Enabled = False
            cmb_SaranaPembayaran.Enabled = False
        End If

        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan) + 1
        NomorBP = AwalanNomorBP & NomorID
        txt_NomorBP.Text = NomorBP

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = Kosongan

        dtp_TanggalPinjam.Value = Today
        txt_NomorBP.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_JumlahPinjaman.Text = Kosongan
        chk_JatuhTempo.Checked = False
        dtp_TanggalJatuhTempo.Enabled = False
        dtp_TanggalJatuhTempo.Value = Today
        txt_NomorKontrak.Text = Kosongan
        lbl_SaranaPembayaran.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        Reset_grb_Bank()
        txt_Keterangan.Text = Kosongan

        JumlahPinjaman = 0
        JumlahHutang = 0
        JumlahPiutang = 0
        JumlahPencairan = 0
        COAUtama = Kosongan
        COASaranaPembayaran = Kosongan
        AdaJatuhTemponya = False 'Ini Penting..! Jangan Dihapus..!

        NomorJV = 0

        ProsesResetForm = False

    End Sub


    Private Sub dtp_TanggalPinjam_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPinjam.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalPinjam)
        TanggalPinjam = TanggalFormatTampilan(dtp_TanggalPinjam.Value)
    End Sub


    Private Sub txt_NomorBP_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub
    Private Sub txt_NomorBP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.ResetForm()
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        If HutangPiutang = hp_Hutang Then frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
        If HutangPiutang = hp_Piutang Then frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Customer
        frm_ListMitra.PilihAfiliasi = Pilihan_Ya
        frm_ListMitra.PilihLembagaKeuangan = Pilihan_Tidak
        frm_ListMitra.ShowDialog()
        txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaLawanTransaksi.Text = frm_ListMitra.NamaMitraTerseleksi
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
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


    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwal.TextChanged
        SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU And SaldoAwal > JumlahPinjaman Then
            MsgBox("Silakan isi kolom 'Saldo' dengan benar.")
            txt_SaldoAwal.Text = Kosongan
            txt_SaldoAwal.Focus()
            Return
        End If
        PemecahRibuanUntukTextBox(txt_SaldoAwal)
    End Sub
    Private Sub txt_SaldoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwal.KeyPress
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            HanyaBolehInputAngkaPlus(sender, e)
        Else
            KunciTotalInputan(sender, e)
        End If
    End Sub


    Private Sub chk_JatuhTempo_CheckedChanged(sender As Object, e As EventArgs) Handles chk_JatuhTempo.CheckedChanged
        If chk_JatuhTempo.Checked = True Then
            AdaJatuhTemponya = True
            dtp_TanggalJatuhTempo.Enabled = True
        Else
            AdaJatuhTemponya = False
            dtp_TanggalJatuhTempo.Enabled = False
            dtp_TanggalJatuhTempo.Value = TanggalIni
        End If
    End Sub


    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.ValueChanged
        If AdaJatuhTemponya = True Then KunciTahun_TidakBolehKurangDariTahunBukuAktif(dtp_TanggalJatuhTempo)
        TanggalJatuhTempo = dtp_TanggalJatuhTempo.Value
    End Sub


    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub


    Dim SaranaPembayaran
    Dim PembayaranViaBank As Boolean = False
    Dim COASaranaPembayaran
    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        COASaranaPembayaran = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If AmbilAngka(COASaranaPembayaran) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COASaranaPembayaran) <= kodeakun_Bank_Akhir _
            Then
            grb_Bank.Enabled = True
            PembayaranViaBank = True
            KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
            Perhitungan()
        Else
            Reset_grb_Bank()
        End If
    End Sub
    Private Sub cmb_SaranaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SaranaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Enabled = False
        txt_BiayaAdministrasiBank.Text = Kosongan
        txt_JumlahTransfer.Text = Kosongan
        KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
    End Sub


    '==================================================== PERHITUNGAN ====================================================
    Dim JumlahTransfer
    Dim JumlahPencairan
    Dim JumlahHutang
    Dim JumlahPiutang
    'Kenapa Harus ada Jumlah Hutang dan Jumlah Piutang..? Apa bedanya dengan Jumlah Pinjaman..?
    'Jadi Gini... Contoh :
    'Ketika perusahaan meminjam uang sebesar 1.000.000 ke Afiliasi, kemudian ada Biaya Administrasi Bank sebesar 5.000,- saat transfer uang tersebut,
    'dan biaya tersebut ditanggung oleh PERUSAHAAN, maka Hutang Perusahaan ke Afiliasi menjadi 1.005.000,-
    Sub Perhitungan()

        If HutangPiutang = hp_Hutang Then JumlahHutang = JumlahPinjaman
        If HutangPiutang = hp_Piutang Then JumlahPiutang = JumlahPinjaman

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return

        JumlahTransfer = JumlahPinjaman

        If PembayaranViaBank = True Then
            Select Case HutangPiutang
                Case hp_Hutang
                    txt_JumlahTransfer.Text = JumlahTransfer
                    Select Case DitanggungOleh
                        Case DitanggungOleh_LawanTransaksi
                            JumlahHutang = JumlahTransfer
                        Case DitanggungOleh_Perusahaan
                            JumlahHutang = JumlahTransfer + BiayaAdministrasiBank
                    End Select
                    txt_SaldoAwal.Text = JumlahHutang
                Case hp_Piutang
                    txt_JumlahTransfer.Text = JumlahTransfer
                    Select Case DitanggungOleh
                        Case DitanggungOleh_LawanTransaksi
                            txt_JumlahTransfer.Text = JumlahTransfer - BiayaAdministrasiBank
                        Case DitanggungOleh_Perusahaan
                            txt_JumlahTransfer.Text = JumlahTransfer
                    End Select
                    JumlahPencairan = JumlahTransfer
                    txt_SaldoAwal.Text = JumlahPiutang
            End Select
        Else
            If HutangPiutang = hp_Hutang Then JumlahHutang = JumlahPinjaman
            If HutangPiutang = hp_Piutang Then JumlahPiutang = JumlahPinjaman
            JumlahPencairan = JumlahPinjaman
            txt_SaldoAwal.Text = JumlahPinjaman
        End If

    End Sub


    Dim BiayaAdministrasiBank
    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.Enabled = False
            cmb_DitanggungOleh.Text = Kosongan
        Else
            cmb_DitanggungOleh.Enabled = True
        End If
        Perhitungan()
    End Sub


    Dim DitanggungOleh
    Private Sub cmb_DitanggungOleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.SelectedIndexChanged
    End Sub
    Private Sub cmb_DitanggungOleh_TextChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.TextChanged
        DitanggungOleh = cmb_DitanggungOleh.Text
        Perhitungan()
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


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Beberapa Value :
        Keterangan = txt_Keterangan.Text
        TanggalJatuhTempo = dtp_TanggalJatuhTempo.Value
        Dim JumlahHutangPiutang As Int64 = 0
        If HutangPiutang = hp_Hutang Then JumlahHutangPiutang = JumlahHutang
        If HutangPiutang = hp_Piutang Then JumlahHutangPiutang = JumlahPiutang

        'Validasi Kolom-kolom Tertentu :
        If KodeLawanTransaksi = Kosongan Then
            If HutangPiutang = hp_Hutang Then MsgBox("Silakan isi kolom 'Kreditur'.")
            If HutangPiutang = hp_Piutang Then MsgBox("Silakan isi kolom 'Debitur'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahPinjaman = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Pinjaman'.")
            txt_JumlahPinjaman.Focus()
            Return
        End If

        If SaldoAwal = 0 Then
            MsgBox("Silakan isi kolom 'Saldo Awal'.")
            txt_SaldoAwal.Focus()
            Return
        End If

        If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
            MsgBox("Silakan pilih 'Ditanggung Oleh'.")
            cmb_DitanggungOleh.Focus()
            Return
        End If

        If AdaJatuhTemponya = False Then TanggalJatuhTempo = TanggalKosong

        NomorJV = 0 '(Tidak ada penjurnalan pada Input Data)

        If FungsiForm = FungsiForm_TAMBAH Then

            'SistemPenomoranOtomatis_NomorJV()
            'NomorJV = jur_NomorJV


            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBP & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " '" & JumlahHutangPiutang & "', " &
                                  " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & COASaranaPembayaran & "', " &
                                  " '" & BiayaAdministrasiBank & "', " &
                                  " '" & DitanggungOleh & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            'jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Buka)

            'Update Data pada Tabel : tbl_PengawasanPiutangAfiliasi 
            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  KolomNomorBP & "              = '" & NomorBP & "', " &
                                  " Kode_Lawan_Transaksi        = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi        = '" & NamaLawanTransaksi & "', " &
                                  " Tanggal_Transaksi           = '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " Jumlah_Pinjaman             = '" & JumlahHutangPiutang & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Nomor_Kontrak               = '" & NomorKontrak & "', " &
                                  KolomCOASaranaPembayaran & "  = '" & COASaranaPembayaran & "', " &
                                  " Biaya_Administrasi_Bank     = '" & BiayaAdministrasiBank & "', " &
                                  " Ditanggung_Oleh             = '" & DitanggungOleh & "', " &
                                  " Keterangan                  = '" & Keterangan & "', " &
                                  " Nomor_JV                    = '" & NomorJV & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Hapus Jurnal Terkait : tbl_Transaksi
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)
        End If

        'If HutangPiutang = hp_Hutang Then SimpanJurnal_Hutang()
        'If HutangPiutang = hp_Piutang Then SimpanJurnal_Piutang()
        'If jur_StatusPenyimpananJurnal_PerBaris = True Then
        '    jur_StatusPenyimpananJurnal_Lengkap = True
        'Else
        '    jur_StatusPenyimpananJurnal_Lengkap = False
        '    PesanUntukProgrammer("Data GAGAL diposting ke Jurnal...!!!")
        'End If
        'ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If HutangPiutang = hp_Hutang Then usc_BukuPengawasanHutangAfiliasi.TampilkanData()
            If HutangPiutang = hp_Piutang Then usc_BukuPengawasanPiutangAfiliasi.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Sub SimpanJurnal_Hutang()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return

        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalPinjam)
        jur_JenisJurnal = JenisJurnal_HutangAfiliasi
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        'Jurnal Debet :
        If PembayaranViaBank = True Then ___jurDebetBankCashIN(DitanggungOleh, COASaranaPembayaran, JumlahPencairan, JumlahTransfer, BiayaAdministrasiBank)
        If PembayaranViaBank = False Then ___jurDebet(COASaranaPembayaran, JumlahPinjaman)
        'Jurnal Kredit :
        _______jurKredit(COAUtama, JumlahHutang)

    End Sub


    Sub SimpanJurnal_Piutang()

        If NomorJV = 0 Then Return '(Tidak ada penjurnalan pada Input Data Piutang)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return

        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalPinjam)
        jur_JenisJurnal = JenisJurnal_PiutangAfiliasi
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        'Jurnal Debet :
        ___jurDebet(COAUtama, JumlahPinjaman)
        ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
        'Jurnal Kredit :
        If PembayaranViaBank = True Then _______jurKreditBankCashOUT(DitanggungOleh, COASaranaPembayaran, JumlahPencairan, JumlahTransfer, BiayaAdministrasiBank)
        If PembayaranViaBank = False Then _______jurKredit(COASaranaPembayaran, JumlahPinjaman)

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub


End Class
