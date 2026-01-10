Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputHutangPiutangKaryawan

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Dim NomorID
    Dim TanggalPinjam
    Dim NomorBP
    Dim NomorDokumen
    Dim NomorIDKaryawan
    Dim NamaKaryawan
    Dim Jabatan
    Dim JumlahPinjaman
    Dim SaldoAwal
    Dim Keterangan
    Public NomorJV

    Public HutangPiutang = Kosongan
    Dim TabelPengawasan
    Dim KolomNomorBP
    Dim AwalanNomorBP
    Dim KolomCOASaranaPembayaran
    Dim COAUtama

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!!!!")
        If HutangPiutang = Kosongan Then PesanUntukProgrammer("Tentukan dulu, Hutang atau Piutang...!!!")

        If HutangPiutang = hp_Hutang Then
            TabelPengawasan = "tbl_PengawasanHutangKaryawan"
            KolomNomorBP = "Nomor_BPHK"
            KolomCOASaranaPembayaran = "COA_Debet"
            COAUtama = KodeTautanCOA_HutangKaryawan
            AwalanNomorBP = AwalanBPHK_PlusTahunBuku
            lbl_NomorBP.Text = "Nomor BPHK"
            lbl_SaranaPembayaran.Text = "Sarana Pencairan"
        End If

        If HutangPiutang = hp_Piutang Then
            TabelPengawasan = "tbl_PengawasanPiutangKaryawan"
            KolomNomorBP = "Nomor_BPPK"
            KolomCOASaranaPembayaran = "COA_Kredit"
            COAUtama = KodeTautanCOA_PiutangKaryawan
            AwalanNomorBP = AwalanBPPK_PlusTahunBuku
            lbl_NomorBP.Text = "Nomor BPPK"
            lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
            lbl_SaranaPembayaran.Enabled = False
            cmb_SaranaPembayaran.Enabled = False
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input " & HutangPiutang & " Karyawan"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit " & HutangPiutang & " Karyawan"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM  " & TabelPengawasan &
                                  " WHERE " & KolomNomorBP & " = '" & NomorBP & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                COASaranaPembayaran = dr.Item(KolomCOASaranaPembayaran)
                txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
                cmb_DitanggungOleh.Text = dr.Item("Ditanggung_Oleh")
            End If
            AksesDatabase_Transaksi(Tutup)
            cmb_SaranaPembayaran.Text = KonversiCOAKeSaranaPembayaran(COASaranaPembayaran)
            If HutangPiutang = hp_Hutang And DitanggungOleh = DitanggungOleh_Perusahaan Then
                txt_JumlahPinjaman.Text = JumlahPinjaman - BiayaAdministrasiBank
            End If
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            txt_SaldoAwal.Enabled = True
            lbl_SaldoAwal.Text = "Saldo Akhir"
        Else
            txt_SaldoAwal.Enabled = False
            lbl_SaldoAwal.Text = "Saldo Awal"
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

        dtp_TanggalPinjam.Value = Today
        txt_NomorBP.Text = Kosongan
        txt_NomorIDKaryawan.Text = Kosongan
        txt_NamaKaryawan.Text = Kosongan
        txt_Jabatan.Text = Kosongan
        txt_JumlahPinjaman.Text = Kosongan
        txt_SaldoAwal.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        lbl_SaranaPembayaran.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        Reset_grb_Bank()

        JumlahHutang = 0
        JumlahPinjaman = 0
        COASaranaPembayaran = Kosongan
        COAUtama = Kosongan

        NomorJV = 0

        ProsesResetForm = False

    End Sub

    Private Sub dtp_TanggalPinjam_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPinjam.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalPinjam)
        TanggalPinjam = TanggalFormatTampilan(dtp_TanggalPinjam.Value)
    End Sub

    Private Sub txt_NomorBPPK_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub
    Private Sub txt_NomorBPPK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorDokumen_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorDokumen.TextChanged
        NomorDokumen = txt_NomorDokumen.Text
    End Sub

    Private Sub txt_NomorIDKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorIDKaryawan.TextChanged
        NomorIDKaryawan = txt_NomorIDKaryawan.Text
    End Sub
    Private Sub txt_NomorIDKaryawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorIDKaryawan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_NomorIDKaryawan_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txt_NomorIDKaryawan.MouseDoubleClick
        btn_PilihIDKaryawan_Click(sender, e)
    End Sub

    Private Sub btn_PilihIDKaryawan_Click(sender As Object, e As EventArgs) Handles btn_PilihIDKaryawan.Click
        'If NomorIDKaryawan = Kosongan Then
        '    X_frm_ListDataKaryawan.ResetForm()
        'Else
        '    X_frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi = NomorIDKaryawan
        '    X_frm_ListDataKaryawan.NamaKaryawan_Terseleksi = NamaKaryawan
        '    X_frm_ListDataKaryawan.Jabatan_Terseleksi = Jabatan
        'End If
        'X_frm_ListDataKaryawan.ShowDialog()
        'txt_NomorIDKaryawan.Text = X_frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
        'txt_NamaKaryawan.Text = X_frm_ListDataKaryawan.NamaKaryawan_Terseleksi
        'txt_Jabatan.Text = X_frm_ListDataKaryawan.Jabatan_Terseleksi
    End Sub

    Private Sub txt_NamaKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaKaryawan.TextChanged
        NamaKaryawan = txt_NamaKaryawan.Text
    End Sub
    Private Sub txt_NamaKaryawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaKaryawan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Jabatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Jabatan.TextChanged
        Jabatan = txt_Jabatan.Text
    End Sub
    Private Sub txt_Jabatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Jabatan.KeyPress
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
    'Ketika perusahaan meminjam uang sebesar 1.000.000 ke Karyawan, kemudian ada Biaya Administrasi Bank sebesar 5.000,- saat transfer uang tersebut,
    'dan biaya tersebut ditanggung oleh PERUSAHAAN, maka Hutang Perusahaan ke Karyawan menjadi 1.005.000,-
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
        Dim JumlahHutangPiutang As Int64 = 0
        If HutangPiutang = hp_Hutang Then JumlahHutangPiutang = JumlahHutang
        If HutangPiutang = hp_Piutang Then JumlahHutangPiutang = JumlahPiutang

        'Validasi Kolom-kolom Tertentu :
        If NomorIDKaryawan = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor ID Karyawan'.")
            txt_NomorIDKaryawan.Focus()
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

        'If cmb_SaranaPembayaran.Enabled = True And SaranaPembayaran = Kosongan Then
        '    MsgBox("Silakan pilih 'Sarana Pembayaran'.")
        '    cmb_SaranaPembayaran.Focus()
        '    Return
        'End If

        If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
            MsgBox("Silakan pilih 'Ditanggung Oleh'.")
            cmb_DitanggungOleh.Focus()
            Return
        End If

        NomorJV = 0 '(Tidak ada penjurnalan pada Input Data Piutang)

        If FungsiForm = FungsiForm_TAMBAH Then

            'If HutangPiutang = hp_Hutang Then
            '    SistemPenomoranOtomatis_NomorJV()
            '    NomorJV = jur_NomorJV
            'Else
            'End If

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO " & TabelPengawasan & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBP & "', " &
                                  " '" & NomorDokumen & "', " &
                                  " '" & NomorIDKaryawan & "', " &
                                  " '" & NamaKaryawan & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " '" & JumlahHutangPiutang & "', " &
                                  " '" & SaldoAwal & "', " &
                                  " '" & COASaranaPembayaran & "', " &
                                  " '" & BiayaAdministrasiBank & "', " &
                                  " '" & DitanggungOleh & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            'jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Buka)

            'Update Data pada Tabel : tbl_PengawasanPiutangKaryawan 
            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  " Nomor_Dokumen               = '" & NomorDokumen & "', " &
                                  " Nomor_ID_Karyawan           = '" & NomorIDKaryawan & "', " &
                                  " Nama_Karyawan               = '" & NamaKaryawan & "', " &
                                  " Tanggal_Transaksi           = '" & TanggalFormatSimpan(TanggalPinjam) & "', " &
                                  " Jumlah_Pinjaman             = '" & JumlahHutangPiutang & "', " &
                                  " Saldo_Awal                  = '" & SaldoAwal & "', " &
                                  KolomCOASaranaPembayaran & "  = '" & COASaranaPembayaran & "', " &
                                  " Biaya_Administrasi_Bank     = '" & BiayaAdministrasiBank & "', " &
                                  " Ditanggung_Oleh             = '" & DitanggungOleh & "', " &
                                  " Keterangan                  = '" & Keterangan & "', " &
                                  " Nomor_JV                    = '" & NomorJV & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE " & KolomNomorBP & "  = '" & NomorBP & "' ", KoneksiDatabaseTransaksi)
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
        '    If HutangPiutang = hp_Hutang Then PesanUntukProgrammer("Data GAGAL diposting ke Jurnal...!!!")
        'End If

        'ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuPengawasanHutangKaryawan.StatusAktif Then usc_BukuPengawasanHutangKaryawan.TampilkanData()
            If usc_BukuPengawasanPiutangKaryawan.StatusAktif Then usc_BukuPengawasanPiutangKaryawan.TampilkanData()
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
        jur_JenisJurnal = JenisJurnal_HutangKaryawan
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = NomorIDKaryawan
        jur_NamaLawanTransaksi = NamaKaryawan
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
        jur_JenisJurnal = JenisJurnal_PiutangKaryawan
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = NomorIDKaryawan
        jur_NamaLawanTransaksi = NamaKaryawan
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

    Private Sub lbl_SaldoAwal_Click(sender As Object, e As EventArgs) Handles lbl_SaldoAwal.Click

    End Sub
End Class
