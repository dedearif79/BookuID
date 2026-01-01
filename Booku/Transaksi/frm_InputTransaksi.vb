Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputTransaksi

    Public JudulForm
    Public JalurMasuk
    Public AlurTransaksi
    Public FungsiForm

    Public JenisJurnal
    Public COAUtama
    Public COASaranaPembayaran
    Dim NamaAkun

    Public JumlahMutasiCOAUTama As Int64
    Public JumlahMutasiBankCash As Int64 '(Jumlah Debet atau Jumlah Kredit untuk BankCash)

    Public JumlahPPhTerutang
    Public JumlahPPhDitanggung
    Public JumlahPPhDipotong
    Public COAHutangPPh
    Public COABiayaPPh
    Public COAPPhPrepaid
    Public COAPenghasilan

    Public JumlahPokokAngsuran
    Public DendaAngsuran
    Public BungaBagiHasilAngsuran

    Dim JenisTransaksi
    Dim Referensi As String = Nothing
    Dim SaranaPembayaran
    Dim TanggalInvoice As String 'Meskipun tanggal, ini memakai format text (bukan datetime), karena satu data transaksi bisa banyak invoice.
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahTransaksi
    Dim TanggalTransaksi
    Dim TahunTransaksi
    Dim BiayaAdministrasiBank
    Public DitanggungOleh
    Dim JumlahTransfer
    Dim TotalBank
    Dim UraianTransaksi

    Public NPPHU As String = Nothing
    Public NomorBPHB
    Public NomorBPHL
    Public NomorBPHPK
    Public NomorBPPPK
    Public NomorBPHA
    Public NomorBPPA
    Public NomorBPHU
    Public NomorPembelian
    Public NomorBPPU
    Public NomorPenjualan
    Public NomorBPHP
    Public NomorBPHK
    Public NomorBPPK
    Public NomorBPHPS
    Public NomorBPPPS
    Public NTPN As String = Nothing
    Public NPPHP As String = Nothing
    Public NPPG As String = Nothing
    Public NPPHKS As String = Nothing
    Public NPPHTK As String = Nothing
    Public BulanPembayaran
    Public MasaPajak
    Public Bundelan = Nothing
    Public NomorID
    Public PembayaranKe As Integer
    Public AngsuranKe As String
    Public TermasukHutangTahunBukuAktif As Boolean

    Public JumlahBarisAngsuranBankLeasing
    Public JumlahBarisAngsuranPihakKetiga
    Public JumlahBarisAngsuranAfiliasi

    Public JenisPajak   'Ingat...! Harap Bedakan antara variabel JenisPajak dengan JenisPPh..!
    Public JenisPPh     'Ingat...! Harap Bedakan antara variabel JenisPajak dengan JenisPPh..!
    Public KodeSetoran

    Public PencairanPiutangJualAsset As Boolean

    Dim QueryPenyimpanan

    Public PenyimpananSukses As Boolean

    Public KodeMataUang As String

    Private Sub frm_InputTransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If KodeMataUang = Kosongan Then PesanUntukProgrammer("Kode Mata Uang Belum Ditentukan....!!!!")

        ProsesLoadingForm = True

        PenyimpananSukses = False 'Default : False

        If KodeTautanCOA_PettyCashAdministrasi = Nothing _
            Or KodeTautanCOA_Kas = Nothing _
            Or KodeTautanCOA_CashAdvance = Nothing _
            Or KodeTautanCOA_HutangUsaha_NonAfiliasi = Nothing _
            Or KodeTautanCOA_BiayaAdministrasiBank = Nothing _
            Then 'Kemungkinan masih ada yang harus ditambahkan di sini.
            MsgBox("Untuk menggunakan fitur ini, silakan lengkapi terlebih dahulu Tautan COA untuk :" & Enter1Baris &
                   "- KAS" & Enter1Baris &
                   "- PETTY CASH" & Enter1Baris &
                   "- CASH ADVANCE" & Enter1Baris &
                   "- HUTANG USAHA" & Enter1Baris &
                   "- BIAYA ADMINISTRASI BANK" & Enter1Baris &
                   "- dsb." & Enter1Baris &
                   "di menu : Data --> Data COA --> Tautan COA." & Enter1Baris &
                   "")
            Me.Close()
        End If

        If JalurMasuk = Halaman_MENUUTAMA Then
            btn_Reset.Enabled = True
            txt_Referensi.Enabled = True
            btn_PilihReferensi.Enabled = True
            btn_Tutup.Text = "Tutup"
        Else
            btn_Tutup.Text = "Batal"
        End If

        If JalurMasuk = Halaman_BAHANJURNAL _
            Or JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA _
            Or JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGPAJAK _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANGAJI _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKESEHATAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKETENAGAKERJAAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGUSAHA _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGKARYAWAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGBANKLEASING _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGPIHAKKETIGA _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGAFILIASI _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGPEMEGANGSAHAM _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL21 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL23 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL25 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL26 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL29 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL42 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPN _
            Or JalurMasuk = Halaman_INPUTTRANSAKSIKAS _
            Then
            btn_Reset.Enabled = False
            cmb_JenisTransaksi.Enabled = False
            txt_Referensi.Enabled = False
            btn_PilihReferensi.Enabled = False
            cmb_SaranaPembayaran.Enabled = False
            btn_Reset.Enabled = False
        End If

        If JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGPAJAK _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL21 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL23 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL25 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL26 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL29 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL42 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPN _
            Then
            lbl_NomorInvoice.Text = "NTPN"
            txt_NomorInvoice.Enabled = True
            lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
        End If

        If AlurTransaksi = AlurTransaksi_OUT Then
            lbl_JumlahTransfer.Text = "Jumlah Transfer"
        End If
        If AlurTransaksi = AlurTransaksi_IN Then
            'txt_Referensi.Enabled = True
            btn_PilihReferensi.Visible = False
            lbl_JumlahTransfer.Text = "Jumlah Pencairan"
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Transaksi " & AlurTransaksi
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Transaksi " & AlurTransaksi
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = False

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = FungsiForm_TAMBAH '(Default : TAMBAH)
        AlurTransaksi = AlurTransaksi_OUT  '(Default : OUT)
        KodeMataUang = KodeMataUang_IDR
        If LevelUserAktif = LevelUser_99_AppDeveloper Then KodeMataUang = Kosongan
        KontenComboJenisTransaksi()
        cmb_DitanggungOleh.Text = Kosongan 'Ini penting..! Jangan dihapus..!
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        cmb_JenisJurnal.Enabled = True
        KontenComboJenisJurnal_Public(cmb_JenisJurnal)
        jur_NomorJV = 0
        NomorID = 0
        cmb_JenisTransaksi.Enabled = True
        txt_Referensi.Enabled = True
        btn_PilihReferensi.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
        lbl_COA.Text = "Kode Akun"
        txt_COA.Enabled = False
        txt_COA.Text = Nothing
        lbl_NamaAkun.Text = "Nama Akun"
        txt_NamaAkun.Enabled = False
        txt_NamaAkun.Text = Nothing
        txt_Referensi.Text = Nothing
        lbl_NomorInvoice.Text = "Nomor Invoice/Nota"
        txt_NomorInvoice.Enabled = False
        txt_NomorInvoice.Text = Nothing
        lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
        txt_NomorFakturPajak.Enabled = False
        txt_NomorFakturPajak.Text = Nothing
        txt_KodeLawanTransaksi.Enabled = False
        txt_KodeLawanTransaksi.Text = Nothing
        txt_NamaLawanTransaksi.Enabled = False
        txt_NamaLawanTransaksi.Text = Nothing
        txt_JumlahTransaksi.Enabled = False
        txt_JumlahTransaksi.Text = Nothing
        dtp_TanggalTransaksi.Enabled = True
        dtp_TanggalTransaksi.Value = Today
        grb_Bank.Enabled = False
        Reset_grb_Bank()
        txt_UraianTransaksi.Enabled = True
        txt_UraianTransaksi.Text = Nothing
        cmb_JenisTransaksi.Focus()
        btn_Reset.Enabled = True
        COAUtama = Kosongan
        COASaranaPembayaran = Kosongan
        COAHutangPPh = Kosongan
        Referensi = Nothing
        NTPN = Nothing
        NPPHU = Nothing
        NPPHP = Nothing
        NPPG = Nothing
        NPPHKS = Nothing
        NPPHTK = Nothing
        MasaPajak = Nothing
        BulanPembayaran = Nothing
        Bundelan = Nothing
        JenisPajak = Nothing
        JenisPPh = Nothing
        KodeSetoran = Kosongan
        BiayaAdministrasiBank = 0
        JumlahMutasiBankCash = 0
        JumlahMutasiCOAUTama = 0
        JumlahPPhTerutang = 0
        JumlahPPhDitanggung = 0
        JumlahPPhDipotong = 0
        JumlahPokokAngsuran = 0
        DendaAngsuran = 0
        BungaBagiHasilAngsuran = 0
        JumlahBarisAngsuranBankLeasing = 0
        JumlahBarisAngsuranPihakKetiga = 0
        JumlahBarisAngsuranAfiliasi = 0

        PencairanPiutangJualAsset = False

        ProsesResetForm = False

    End Sub

    Sub KontenComboJenisTransaksi()
        cmb_JenisTransaksi.Items.Clear()
        Select Case AlurTransaksi
            Case AlurTransaksi_IN
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_DanaMasukLainnya)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PencairanPiutangUsaha)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PencairanPiutangKaryawan)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PencairanPiutangPemegangSaham)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PencairanPiutangPihakKetiga)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PencairanPiutangAfiliasi)
            Case AlurTransaksi_OUT
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranGaji)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranBpjsKesehatan)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranBpjsKetenagakerjaan)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangBank)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangUsaha)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangPemegangSaham)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangAfiliasi)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangPihakKetiga)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangKaryawan)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PembayaranHutangPajak)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_UangMukaPembelian)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_PemberianPiutangKas)
                cmb_JenisTransaksi.Items.Add(JenisTransaksi_DanaKeluarLainnya)
        End Select
        cmb_JenisTransaksi.Text = Kosongan
        cmb_JenisTransaksi.SelectedValue = Kosongan
    End Sub

    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi, JumlahMutasiBankCash, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click
        'Sub ini jangan diisi apa-apa lagi. Sudah cukup ini saja....!!!
        SimpanData()
    End Sub

    Public Sub SimpanData()

        If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
            MsgBox("Silakan pilih 'Ditanggung Oleh'.")
            cmb_DitanggungOleh.Focus()
            Return
        End If

        If COASaranaPembayaran = Kosongan Then
            PesanUntukProgrammer("COA Sarana Pembayaran belum tertaut...!!!")
        End If

        PenyimpananSukses = False 'Default : False

        'Validasi Form Input

        If Referensi = Nothing Then
            MsgBox("Pilih Referensi..!")
            txt_Referensi.Focus()
            Return
        End If
        If JumlahTransaksi = 0 Then
            MsgBox("Isi Jumlah Transaksi..!")
            txt_JumlahTransaksi.Focus()
            Return
        End If
        If JenisJurnal = Nothing Then
            MsgBox("Silakan pilih 'Jenis Jurnal'..!")
            cmb_JenisJurnal.Focus()
            Return
        End If

        'Validasi Khusus, berdasarkan Jenis Transaksi :
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPajak Then
            If NomorInvoice = Nothing Then
                MsgBox("Silakan isi kolom 'NTPN'")
                txt_NomorInvoice.Focus()
                Return
            End If
        End If

        Perhitungan_ValueBank() 'Sub Ini sengaja dipanggil lagi untuk memastikan Beberapa Variabel terisi dengan benar. Jangan dihapus..!!!

        If JalurMasuk = Halaman_MENUUTAMA _
            Or JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA _
            Then
            Dim PilihSimpan = MessageBox.Show("Periksa kembali data terutama 'Tanggal Transaksi' sebelum disimpan." & Enter2Baris & "Yakin akan menyimpan data transaksi ini..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihSimpan = vbNo Then Return
        End If

        'Pengisian Value Variabel (Memang tidak semua. Karena ada yang sudah terisi duluan).
        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV() 'Ini sudah benar posisinya di paling atas. Penting untuk kebutuhan identifikasi Nomor_JV pada beberapa logika dan atau fungsi lain di baris-baris berikutnya.
        End If
        Dim TanggalTransaksiSimpan = TanggalFormatSimpan(dtp_TanggalTransaksi.Value)
        NPPHU = Referensi
        NPPHP = Referensi
        NPPG = Referensi
        NPPHKS = Referensi
        NPPHTK = Referensi

        'TES KONEKSI DATABASE :
        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabase = False Then Return
        AksesDatabase_Transaksi(Tutup)

        'JENIS TRANSAKSI : PEMBAYARAN GAJI =====================================================================================
        'Untuk Sementara ini, tidak menggunakan sistem PENGAJUAN PEMBAYARAN, sebagaimana yang berlaku pada Sistem Pembayaran Hutang Usaha.
        'Kemungkinan nanti sistem ini akan disesuaikan dengan sistem Pembayaran Hutang Usaha.
        If JenisTransaksi = JenisTransaksi_PembayaranGaji Then

            Dim NomorIdPembayaranGaji As Int64 = AmbilAngka(Microsoft.VisualBasic.Mid(NPPG, PanjangTeks_AwalanNPPG_PlusTahunBuku_Plus1))

            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" INSERT INTO tbl_PembayaranGaji VALUES ( " &
                                      " '" & NomorIdPembayaranGaji & "', " &
                                      " '" & BulanPembayaran & "', " &
                                      " '" & NPPG & "', " &
                                      " '" & TanggalTransaksiSimpan & "', " &
                                      " '" & JumlahTransaksi & "', " &
                                      " '" & COASaranaPembayaran & "', " &
                                      " '" & BiayaAdministrasiBank & "', " &
                                      " '" & DitanggungOleh & "', " &
                                      " '" & UraianTransaksi & "', " &
                                      " '" & jur_NomorJV & "', " &
                                      " '" & UserAktif & "' " &
                                      " ) ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            If FungsiForm = FungsiForm_EDIT Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_PembayaranGaji SET " &
                                      " Bulan = '" & BulanPembayaran & "', " &
                                      " NPPG = '" & NPPG & "', " &
                                      " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                      " Jumlah_Bayar = '" & JumlahTransaksi & "', " &
                                      " COA_Kredit = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh = '" & DitanggungOleh & "', " &
                                      " Keterangan = '" & UraianTransaksi & "', " &
                                      " Nomor_JV = '" & jur_NomorJV & "', " &
                                      " User = '" & UserAktif & "' " &
                                      " WHERE Nomor_ID = '" & NomorIdPembayaranGaji & "' ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        'JENIS TRANSAKSI : PEMBAYARAN BPJS KESEHATAN =====================================================================================
        'Untuk Sementara ini, tidak menggunakan sistem PENGAJUAN PEMBAYARAN, sebagaimana yang berlaku pada Sistem Pembayaran Hutang Usaha.
        'Kemungkinan nanti sistem ini akan disesuaikan dengan sistem Pembayaran Hutang Usaha.
        If JenisTransaksi = JenisTransaksi_PembayaranBpjsKesehatan Then

            Dim NomorIdPembayaranBpjsKesehatan As Int64 = AmbilAngka(Microsoft.VisualBasic.Mid(NPPHKS, PanjangTeks_AwalanNPPHKS_PlusTahunBuku_Plus1))

            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" INSERT INTO tbl_PembayaranBpjsKesehatan VALUES ( " &
                                      " '" & NomorIdPembayaranBpjsKesehatan & "', " &
                                      " '" & BulanPembayaran & "', " &
                                      " '" & NPPHKS & "', " &
                                      " '" & TanggalTransaksiSimpan & "', " &
                                      " '" & JumlahTransaksi & "', " &
                                      " '" & COASaranaPembayaran & "', " &
                                      " '" & BiayaAdministrasiBank & "', " &
                                      " '" & DitanggungOleh & "', " &
                                      " '" & UraianTransaksi & "', " &
                                      " '" & jur_NomorJV & "', " &
                                      " '" & UserAktif & "' " &
                                      " ) ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            If FungsiForm = FungsiForm_EDIT Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_PembayaranBpjsKesehatan SET " &
                                      " Bulan = '" & BulanPembayaran & "', " &
                                      " NPPHKS = '" & NPPHKS & "', " &
                                      " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                      " Jumlah_Bayar = '" & JumlahTransaksi & "', " &
                                      " COA_Kredit = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh = '" & DitanggungOleh & "', " &
                                      " Keterangan = '" & UraianTransaksi & "', " &
                                      " Nomor_JV = '" & jur_NomorJV & "', " &
                                      " User = '" & UserAktif & "' " &
                                      " WHERE Nomor_ID = '" & NomorIdPembayaranBpjsKesehatan & "' ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        'JENIS TRANSAKSI : PEMBAYARAN BPJS KETENAGAKERJAAN =====================================================================================
        'Untuk Sementara ini, tidak menggunakan sistem PENGAJUAN PEMBAYARAN, sebagaimana yang berlaku pada Sistem Pembayaran Hutang Usaha.
        'Kemungkinan nanti sistem ini akan disesuaikan dengan sistem Pembayaran Hutang Usaha.
        If JenisTransaksi = JenisTransaksi_PembayaranBpjsKetenagakerjaan Then

            Dim NomorIdPembayaranBpjsKetenagakerjaan As Int64 = AmbilAngka(Microsoft.VisualBasic.Mid(NPPHTK, PanjangTeks_AwalanNPPHTK_PlusTahunBuku_Plus1))

            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" INSERT INTO tbl_PembayaranBpjsKetenagakerjaan VALUES ( " &
                                      " '" & NomorIdPembayaranBpjsKetenagakerjaan & "', " &
                                      " '" & BulanPembayaran & "', " &
                                      " '" & NPPHTK & "', " &
                                      " '" & TanggalTransaksiSimpan & "', " &
                                      " '" & JumlahTransaksi & "', " &
                                      " '" & COASaranaPembayaran & "', " &
                                      " '" & BiayaAdministrasiBank & "', " &
                                      " '" & DitanggungOleh & "', " &
                                      " '" & UraianTransaksi & "', " &
                                      " '" & jur_NomorJV & "', " &
                                      " '" & UserAktif & "' " &
                                      " ) ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            If FungsiForm = FungsiForm_EDIT Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_PembayaranBpjsKetenagakerjaan SET " &
                                      " Bulan = '" & BulanPembayaran & "', " &
                                      " NPPHKT = '" & NPPHTK & "', " &
                                      " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                      " Jumlah_Bayar = '" & JumlahTransaksi & "', " &
                                      " COA_Kredit = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh = '" & DitanggungOleh & "', " &
                                      " Keterangan = '" & UraianTransaksi & "', " &
                                      " Nomor_JV = '" & jur_NomorJV & "', " &
                                      " User = '" & UserAktif & "' " &
                                      " WHERE Nomor_ID = '" & NomorIdPembayaranBpjsKetenagakerjaan & "' ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        'JENIS TRANSAKSI : PEMBAYARAN HUTANG BANK ==============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangBank Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranBankLeasing - 1) As Integer
            frm_InputPembayaranHutangBankLeasing.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranBankLeasing
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranBankLeasing = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran 'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranHutangBank SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Kredit              = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPHB        = '" & NomorBPHB & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If

        'JENIS TRANSAKSI : PEMBAYARAN HUTANG LEASING ==============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangLeasing Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranBankLeasing - 1) As Integer
            frm_InputPembayaranHutangBankLeasing.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranBankLeasing
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranBankLeasing = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran  'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranHutangLeasing SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Kredit              = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPHL        = '" & NomorBPHL & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If


        'JENIS TRANSAKSI : PEMBAYARAN HUTANG AFILIASI ==============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangAfiliasi Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranAfiliasi - 1) As Integer
            frm_InputPembayaranAfiliasi.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranAfiliasi
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranAfiliasi = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran   'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranHutangAfiliasi SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Kredit              = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPHA        = '" & NomorBPHA & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If


        'JENIS TRANSAKSI : PEMBAYARAN HUTANG PIHAK KETIGA ==============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPihakKetiga Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranPihakKetiga - 1) As Integer
            frm_InputPembayaranPihakKetiga.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranPihakKetiga
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranPihakKetiga = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran   'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranHutangPihakKetiga SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Kredit              = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPHPK       = '" & NomorBPHPK & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If


        'JENIS TRANSAKSI : PEMBAYARAN HUTANG KARYAWAN =============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangKaryawan Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PembayaranHutangKaryawan") + 1

            AksesDatabase_Transaksi(Buka)
            Select Case FungsiForm
                Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                    QueryPenyimpanan = " INSERT INTO tbl_PembayaranHutangKaryawan VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPHK & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahTransaksi & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & UserAktif & "' ) "
                Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                    QueryPenyimpanan = " UPDATE tbl_PembayaranHutangKaryawan SET " &
                            " Nomor_BPHK                = '" & NomorBPHK & "', " &
                            " Nomor_ID_Karyawan         = '" & KodeLawanTransaksi & "', " &
                            " Nama_Karyawan             = '" & NamaLawanTransaksi & "', " &
                            " Tanggal_Angsuran          = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Angsuran           = '" & JumlahTransaksi & "', " &
                            " COA_Kredit                = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
            End Select
            cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'JENIS TRANSAKSI : PEMBAYARAN HUTANG PEMEGANG SAHAM =============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPemegangSaham Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PembayaranHutangPemegangSaham") + 1

            AksesDatabase_Transaksi(Buka)
            Select Case FungsiForm
                Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                    QueryPenyimpanan = " INSERT INTO tbl_PembayaranHutangPemegangSaham VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPHPS & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahTransaksi & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & UserAktif & "' ) "
                Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                    QueryPenyimpanan = " UPDATE tbl_PembayaranHutangPemegangSaham SET " &
                            " Nomor_BPHPS               = '" & NomorBPHPS & "', " &
                            " NIK                       = '" & KodeLawanTransaksi & "', " &
                            " Nama                      = '" & NamaLawanTransaksi & "', " &
                            " Tanggal_Angsuran          = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Angsuran           = '" & JumlahTransaksi & "', " &
                            " COA_Kredit                = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
            End Select
            cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'JENIS TRANSAKSI : PEMBAYARAN HUTANG USAHA =============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangUsaha Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PembayaranHutangUsaha") + 1

            'Penyimpanan data ke Tabel BPHU, yang tentunya memungkinkan lebih dari 1 record, berdasarkan Nomor Pengajuan (NPPHU)
            Dim Baris = 0
            Dim JumlahBarisBPHU = 0
            'Dim KodeSupplier = Nothing
            'Dim NamaSupplier = Nothing
            Dim JumlahBayar As Int64
            Dim JumlahCicilan As Int64 = 0 'Ini dibutuhkan untuk mengetahui lunas atau belumnya PEMBELIAN.
            Dim LoopPenyimpanan = Nothing
            AksesDatabase_Transaksi(Buka)
            If SistemApprovalPerusahaan = True Then
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha " &
                                      " WHERE Nomor_Pengajuan = '" & NPPHU & "' ", KoneksiDatabaseTransaksi)
            Else
                cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi) 'Ini hanya formalitas saja.
            End If
            dr = cmd.ExecuteReader
            Do While dr.Read
                Baris = Baris + 1
                If SistemApprovalPerusahaan = True Then
                    NomorBPHU = dr.Item("Nomor_BPHU")
                    NomorPembelian = dr.Item("Nomor_Pembelian")
                    KodeLawanTransaksi = dr.Item("Kode_Supplier")
                    NamaLawanTransaksi = dr.Item("Nama_Supplier")
                    JumlahBayar = AmbilAngka(dr.Item("Jumlah_Bayar"))
                Else
                    NomorBPHU = NomorBPHU                   'Baris ini sebetulnya ini tidak perlu. Tapi tidak apa-apa diketik, supaya tidak bingung.
                    NomorPembelian = NomorPembelian         'Baris ini sebetulnya ini tidak perlu. Tapi tidak apa-apa diketik, supaya tidak bingung.
                    KodeLawanTransaksi = KodeLawanTransaksi 'Baris ini sebetulnya ini tidak perlu. Tapi tidak apa-apa diketik, supaya tidak bingung.
                    NamaLawanTransaksi = NamaLawanTransaksi 'Baris ini sebetulnya ini tidak perlu. Tapi tidak apa-apa diketik, supaya tidak bingung.
                    JumlahBayar = JumlahTransaksi
                End If
                Select Case FungsiForm
                    Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                        QueryPenyimpanan = " INSERT INTO tbl_PembayaranHutangUsaha VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPHU & "', " &
                            " '" & NomorPembelian & "', " &
                            " '" & JenisPPh & "', " &
                            " '" & KodeSetoran & "', " &
                            " '" & NPPHU & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahBayar & "', " &
                            " '" & JumlahPPhTerutang & "', " &
                            " '" & JumlahPPhDipotong & "', " &
                            " '" & JumlahMutasiBankCash & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & Kosongan & "', " &
                            " '" & TanggalKosongSimpan & "', " &
                            " '" & Kosongan & "', " &
                            " '" & 0 & "', " &
                            " '" & UserAktif & "' ) "
                    Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                        QueryPenyimpanan = " UPDATE tbl_PembayaranHutangUsaha SET " &
                            " Jenis_PPh                 = '" & JenisPPh & "', " &
                            " Kode_Setoran              = '" & KodeSetoran & "', " &
                            " Tanggal_Bayar             = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Bayar              = '" & JumlahBayar & "', " &
                            " Jumlah_PPh_Terutang       = '" & JumlahPPhTerutang & "', " &
                            " Jumlah_PPh_Dipotong       = '" & JumlahPPhDipotong & "', " &
                            " Jumlah_Kredit             = '" & JumlahMutasiBankCash & "', " &
                            " COA_Kredit                = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
                End Select
                cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmdSIMPAN_ExecuteNonQuery()
                If Baris = 1 Then
                    Bundelan = NomorBPHU
                Else
                    Bundelan = Bundelan & SlashGanda_Pemisah & NomorBPHU
                End If
                NomorID += 1
                If SistemApprovalPerusahaan = False Then Exit Do
            Loop
            AksesDatabase_Transaksi(Tutup)

            'Penyimpanan/Edit Data Pengajuan Pembayaran Hutang Usaha secara otomatis, Untuk yang Non-Approval :
            If SistemApprovalPerusahaan = False Then
                AksesDatabase_Transaksi(Buka)
                Select Case FungsiForm
                    Case FungsiForm_TAMBAH 'Jika Form berfungsi untuk TAMBAH :
                        Dim NomorID_NPPHU As Int64
                        Dim AngkaNPPHU
                        cmd = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha " &
                                              " WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_PengajuanPembayaranHutangUsaha) ",
                                              KoneksiDatabaseTransaksi)
                        dr = cmd.ExecuteReader
                        dr.Read()
                        If Not dr.HasRows Then
                            NomorID_NPPHU = 0
                        Else
                            NomorID_NPPHU = dr.Item("Nomor_ID")
                        End If
                        NomorID_NPPHU = NomorID_NPPHU + 1
                        AngkaNPPHU = Microsoft.VisualBasic.Mid(NPPHU, PanjangTeks_AwalanNPPHU_PlusTahunBuku_Plus1)
                        cmd = New OdbcCommand(" INSERT INTO tbl_PengajuanPembayaranHutangUsaha VALUES ( '" &
                                              NomorID_NPPHU & "', '" &
                                              AngkaNPPHU & "', '" &
                                              NPPHU & "', '" &
                                              TanggalTransaksiSimpan & "', '" &
                                              UserAktif & "', '" &
                                              NamaUserAktif & "', '" &
                                              TanggalKosongSimpan & "', '" &
                                              Nothing & "', '" &
                                              Nothing & "', '" &
                                              TanggalTransaksiSimpan & "', '" &
                                              UserAktif & "', '" &
                                              NamaUserAktif & "', '" &
                                              KodeLawanTransaksi & "', '" &
                                              NamaLawanTransaksi & "', '" &
                                              NomorBPHU & "', '" &
                                              NomorPembelian & "', '" &
                                              Nothing & "', '" &
                                              JumlahTransaksi & "', '" &
                                              Nothing & "', '" &
                                              Nothing & "', '" &
                                              SaranaPembayaran & "', '" &
                                              TanggalTransaksiSimpan & "', 'DIBAYAR', '" &
                                              UraianTransaksi & "' ) ", KoneksiDatabaseTransaksi)
                        cmd.ExecuteNonQuery()
                    Case FungsiForm_EDIT 'Jika Form berfungsi untuk EDIT :
                        cmd = New OdbcCommand(" UPDATE tbl_PengajuanPembayaranHutangUsaha SET " &
                                              " Tanggal_Pengajuan = '" & TanggalTransaksiSimpan & "', " &
                                              " Tanggal_Disetujui_Direktur = '" & TanggalTransaksiSimpan & "', " &
                                              " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                              " Jumlah_Bayar = '" & JumlahTransaksi & "', " &
                                              " Sarana_Pembayaran = '" & SaranaPembayaran & "', " &
                                              " Catatan = '" & UraianTransaksi & "' " &
                                              " WHERE Nomor_Pengajuan = '" & NPPHU & "' AND Nomor_BPHU = '" & NomorBPHU & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd.ExecuteNonQuery()
                End Select
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        'JENIS TRANSAKSI : PEMBAYARAN HUTANG PAJAK =============================================================================
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPajak Then
            Dim NomorID_NPPHP As Int64
            Dim AngkaNPPHP
            NomorID_NPPHP = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PengajuanPembayaranHutangPajak") + 1
            AngkaNPPHP = Microsoft.VisualBasic.Mid(NPPHP, PanjangTeks_AwalanNPPHP_PlusTahunBuku_Plus1)

            If SistemApprovalPerusahaan = False Then 'Jika Sistem Non-Approval, maka lakukan terlebih dahulu penyimpanan data Pengajuan
                AksesDatabase_Transaksi(Buka)
                Select Case FungsiForm
                    Case FungsiForm_TAMBAH
                        cmd = New OdbcCommand(" INSERT INTO tbl_PengajuanPembayaranHutangPajak VALUES ( " &
                                              " '" & NomorID_NPPHP & "', " &
                                              " '" & AngkaNPPHP & "', " &
                                              " '" & NPPHP & "', " &
                                              " '" & TanggalTransaksiSimpan & "', " &
                                              " '" & UserAktif & "', " &
                                              " '" & NamaUserAktif & "', " &
                                              " '" & TanggalKosongSimpan & "', " &
                                              " '" & Nothing & "', " &
                                              " '" & Nothing & "', " &
                                              " '" & TanggalTransaksiSimpan & "', " &
                                              " '" & UserAktif & "', " &
                                              " '" & NamaUserAktif & "', " &
                                              " '" & NomorBPHP & "', " &
                                              " '" & JenisPajak & "', " &
                                              " '" & NTPN & "', " &
                                              " '" & JumlahTransaksi & "', " &
                                              " '" & TanggalTransaksiSimpan & "', " &
                                              " '" & SaranaPembayaran & "', " &
                                              " '" & Nothing & "', " &
                                              " '" & Nothing & "', " &
                                              " 'DIBAYAR', " &
                                              " '" & UraianTransaksi & "' ) ",
                                              KoneksiDatabaseTransaksi)
                    Case FungsiForm_EDIT
                        cmd = New OdbcCommand(" UPDATE tbl_PengajuanPembayaranHutangPajak SET " &
                                              " Tanggal_Pengajuan = '" & TanggalTransaksiSimpan & "', " &
                                              " Tanggal_Disetujui_Direktur = '" & TanggalTransaksiSimpan & "', " &
                                              " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                              " Kode_Billing = '" & NTPN & "', " &
                                              " Jumlah_Bayar = '" & JumlahTransaksi & "', " &
                                              " Sarana_Pembayaran = '" & SaranaPembayaran & "', " &
                                              " Catatan = '" & UraianTransaksi & "' " &
                                              " WHERE Nomor_Pengajuan = '" & NPPHP & "' AND Nomor_BPHP = '" & NomorBPHP & "' ",
                                              KoneksiDatabaseTransaksi)
                End Select
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            Bundelan = Nothing
            NomorBPHP = Nothing
            Dim JumlahBayar
            Dim MasaPajak_Loop = Nothing

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PembayaranHutangPajak")

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangPajak " &
                                  " WHERE Nomor_Pengajuan = '" & NPPHP & "' ",
                                  KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            Dim Baris = 0
            Do While dr.Read
                Baris += 1
                NomorBPHP = dr.Item("Nomor_BPHP")
                MasaPajak_Loop = BulanTerbilang(AmbilAngka_TanpaMinus(Microsoft.VisualBasic.Right(NomorBPHP, 2)))
                JumlahBayar = dr.Item("Jumlah_Bayar")
                If FungsiForm = FungsiForm_TAMBAH Then
                    NomorID += 1
                    QueryPenyimpanan = " INSERT INTO tbl_PembayaranHutangPajak VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPHP & "', " &
                            " '" & MasaPajak_Loop & "', " &
                            " '" & NPPHP & "', " &
                            " '" & NTPN & "', " &
                            " '" & JenisPajak & "', " &
                            " '" & KodeSetoran & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahBayar & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & UserAktif & "' ) "
                End If
                If FungsiForm = FungsiForm_EDIT Then
                    QueryPenyimpanan = " UPDATE tbl_PembayaranHutangPajak SET " &
                            " Masa_Pajak                = '" & MasaPajak_Loop & "', " &
                            " NTPN                      = '" & NTPN & "', " &
                            " Jenis_Pajak               = '" & JenisPajak & "', " &
                            " Kode_Setoran              = '" & KodeSetoran & "', " &
                            " Tanggal_Bayar             = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Bayar              = '" & JumlahBayar & "', " &
                            " COA_Kredit                = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE NPPHP               = '" & NPPHP & "' AND Nomor_BPHP = '" & NomorBPHP & "' "
                End If
                cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmdSIMPAN_ExecuteNonQuery()
                If Baris = 1 Then
                    Bundelan = NomorBPHP
                Else
                    Bundelan = Bundelan & SlashGanda_Pemisah & NomorBPHP
                End If
            Loop
            AksesDatabase_Transaksi(Tutup)
        End If

        'JENIS TRANSAKSI : UANG MUKA PEMBELIAN =================================================================================
        If JenisTransaksi = JenisTransaksi_UangMukaPembelian Then
            'Belum ada coding
        End If

        'JENIS TRANSAKSI : PEMBERIANPIUTANGKAS =================================================================================
        If JenisTransaksi = JenisTransaksi_PemberianPiutangKas Then
            'Belum ada coding
        End If

        'JENIS TRANSAKSI : DANA KELUAR LAINNYA =================================================================================
        If JenisTransaksi = JenisTransaksi_DanaKeluarLainnya Then
            'Belum ada coding
        End If


        'JENIS TRANSAKSI : PENCAIRAN PIUTANG AFILIASI ==============================================================================
        If JenisTransaksi = JenisTransaksi_PencairanPiutangAfiliasi Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranAfiliasi - 1) As Integer
            frm_InputPembayaranAfiliasi.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranAfiliasi
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranAfiliasi = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran   'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranPiutangAfiliasi SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Debet               = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPPA        = '" & NomorBPPA & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If

        'JENIS TRANSAKSI : PENCAIRAN PIUTANG PIHAK KETIGA ==============================================================================
        If JenisTransaksi = JenisTransaksi_PencairanPiutangPihakKetiga Then

            Dim AngsuranKe_Array(JumlahBarisAngsuranPihakKetiga - 1) As Integer
            frm_InputPembayaranPihakKetiga.AngsuranKe_Array.CopyTo(AngsuranKe_Array, 0)
            Dim AngsuranKe = 0
            Dim Denda_Loop As Int64 = 0

            AksesDatabase_Transaksi(Buka)

            Dim AngsuranKe_Index = 0
            Do While AngsuranKe_Index < JumlahBarisAngsuranPihakKetiga
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index)
                If JumlahBarisAngsuranPihakKetiga = AngsuranKe_Index + 1 Then Denda_Loop = DendaAngsuran   'Value Denda diisikan pada baris angsuran terakhir.
                cmd = New OdbcCommand(" UPDATE tbl_JadwalAngsuranPiutangPihakKetiga SET " &
                                      " Tanggal_Bayar           = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                      " Denda                   = '" & Denda_Loop & "', " &
                                      " Jenis_PPh               = '" & JenisPPh & "', " &
                                      " Kode_Setoran            = '" & KodeSetoran & "', " &
                                      " COA_Debet               = '" & COASaranaPembayaran & "', " &
                                      " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                      " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                      " Keterangan              = '" & UraianTransaksi & "', " &
                                      " Nomor_JV                = '" & jur_NomorJV & "', " &
                                      " User                    = '" & UserAktif & "' " &
                                      " WHERE Nomor_BPPPK       = '" & NomorBPPPK & "' " &
                                      " AND Angsuran_Ke         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AngsuranKe_Index += 1
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If


        'JENIS TRANSAKSI : PENCAIRAN PIUTANG KARYAWAN =============================================================================
        If JenisTransaksi = JenisTransaksi_PencairanPiutangKaryawan Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PencairanPiutangKaryawan") + 1

            AksesDatabase_Transaksi(Buka)
            Select Case FungsiForm
                Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                    QueryPenyimpanan = " INSERT INTO tbl_PencairanPiutangKaryawan VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPPK & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahTransaksi & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & UserAktif & "' ) "
                Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                    QueryPenyimpanan = " UPDATE tbl_PencairanPiutangKaryawan SET " &
                            " Nomor_BPPK                = '" & NomorBPPK & "', " &
                            " Nomor_ID_Karyawan         = '" & KodeLawanTransaksi & "', " &
                            " Nama_Karyawan             = '" & NamaLawanTransaksi & "', " &
                            " Tanggal_Angsuran          = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Angsuran           = '" & JumlahTransaksi & "', " &
                            " COA_Debet                 = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
            End Select
            cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'JENIS TRANSAKSI : PENCAIRAN PIUTANG PEMEGANG SAHAM =============================================================================
        If JenisTransaksi = JenisTransaksi_PencairanPiutangPemegangSaham Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PencairanPiutangPemegangSaham") + 1

            AksesDatabase_Transaksi(Buka)
            Select Case FungsiForm
                Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                    QueryPenyimpanan = " INSERT INTO tbl_PencairanPiutangPemegangSaham VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPPPS & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahTransaksi & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & UserAktif & "' ) "
                Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                    QueryPenyimpanan = " UPDATE tbl_PencairanPiutangPemegangSaham SET " &
                            " Nomor_BPHPS               = '" & NomorBPPPS & "', " &
                            " NIK                       = '" & KodeLawanTransaksi & "', " &
                            " Nama                      = '" & NamaLawanTransaksi & "', " &
                            " Tanggal_Angsuran          = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Angsuran           = '" & JumlahTransaksi & "', " &
                            " COA_Debet                 = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
            End Select
            cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'JENIS TRANSAKSI : PENCAIRAN PIUTANG USAHA =============================================================================
        If JenisTransaksi = JenisTransaksi_PencairanPiutangUsaha Then

            If FungsiForm = FungsiForm_TAMBAH Then NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PencairanPiutangUsaha") + 1

            'Penyimpanan data ke Tabel BPHU, yang tentunya memungkinkan lebih dari 1 record, berdasarkan Nomor Pengajuan (NPPHU)
            Dim Baris = 0
            Dim JumlahBarisBPPU = 0
            Dim JumlahBayar As Int64
            Dim JumlahCicilan As Int64 = 0 'Ini dibutuhkan untuk mengetahui lunas atau belumnya PEMBELIAN.
            'Dim cmdLunas As OdbcCommand
            Dim LoopPenyimpanan = Nothing
            JumlahBayar = JumlahTransaksi
            AksesDatabase_Transaksi(Buka)
            Select Case FungsiForm
                Case FungsiForm_TAMBAH 'Jika Form Berfungsi untuk TAMBAH DATA :
                    QueryPenyimpanan = " INSERT INTO tbl_PencairanPiutangUsaha VALUES ( " &
                            " '" & NomorID & "', " &
                            " '" & NomorBPPU & "', " &
                            " '" & NomorPenjualan & "', " &
                            " '" & JenisPPh & "', " &
                            " '" & Referensi & "', " &
                            " '" & KodeLawanTransaksi & "', " &
                            " '" & NamaLawanTransaksi & "', " &
                            " '" & TanggalTransaksiSimpan & "', " &
                            " '" & JumlahBayar & "', " &
                            " '" & JumlahPPhTerutang & "', " &
                            " '" & JumlahPPhDipotong & "', " &
                            " '" & JumlahMutasiBankCash & "', " &
                            " '" & COASaranaPembayaran & "', " &
                            " '" & BiayaAdministrasiBank & "', " &
                            " '" & DitanggungOleh & "', " &
                            " '" & UraianTransaksi & "', " &
                            " '" & jur_NomorJV & "', " &
                            " '" & Kosongan & "', " &
                            " '" & TanggalKosongSimpan & "', " &
                            " '" & Kosongan & "', " &
                            " '" & 0 & "', " &
                            " '" & UserAktif & "' ) "
                Case FungsiForm_EDIT 'Jika Form Berfungsi untuk EDIT DATA :
                    QueryPenyimpanan = " UPDATE tbl_PencairanPiutangUsaha SET " &
                            " Jenis_PPh                 = '" & JenisPPh & "', " &
                            " Referensi                 = '" & Referensi & "', " &
                            " Tanggal_Bayar             = '" & TanggalTransaksiSimpan & "', " &
                            " Jumlah_Bayar              = '" & JumlahBayar & "', " &
                            " Jumlah_PPh_Terutang       = '" & JumlahPPhTerutang & "', " &
                            " Jumlah_PPh_Dipotong       = '" & JumlahPPhDipotong & "', " &
                            " Jumlah_Debet              = '" & JumlahMutasiBankCash & "', " &
                            " COA_Debet                 = '" & COASaranaPembayaran & "', " &
                            " Biaya_Administrasi_Bank   = '" & BiayaAdministrasiBank & "', " &
                            " Ditanggung_Oleh           = '" & DitanggungOleh & "', " &
                            " Keterangan                = '" & UraianTransaksi & "', " &
                            " User                      = '" & UserAktif & "' " &
                            " WHERE Nomor_ID            = '" & NomorID & "' "
            End Select
            cmdSIMPAN = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'PENYIMPANAN/EDIT DATA TRANSAKSI/JURNAL :
        'Jika terjadi perbedaan Tahun Transaksi, maka penjurnalan (penyimpanan data transaksi) disimpan ke database TahunBuku yang sesuai dengan Tahun Transaksi.
        'Untuk itu, dilakukan perubahan koneksi database TahunBuku secara sementara.
        'Setelah penyimpanan sukses, maka koneksi database dikembalikan ke TahunBukuAktif.


        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                  " WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        '====================================================================================
        'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku NORMAL
        '====================================================================================

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalTransaksiSimpan
            jur_JenisJurnal = JenisJurnal
            jur_Referensi = Referensi
            jur_Bundelan = Bundelan
            jur_TanggalInvoice = TanggalInvoice
            jur_NomorInvoice = NomorInvoice
            jur_NomorFakturPajak = NomorFakturPajak
            jur_KodeLawanTransaksi = KodeLawanTransaksi
            jur_NamaLawanTransaksi = NamaLawanTransaksi
            jur_UraianTransaksi = UraianTransaksi
            If JalurMasuk = Halaman_INPUTTRANSAKSIKAS _
                Or JalurMasuk = Halaman_INPUTTRANSAKSIPETTYCASH _
                Or JalurMasuk = Halaman_INPUTTRANSAKSIBANK _
                Then
                jur_Direct = 1
            Else
                jur_Direct = 0
            End If

            JumlahMutasiCOAUTama = JumlahTransaksi

            If JenisTransaksi = JenisTransaksi_PembayaranHutangBank _
                Or JenisTransaksi = JenisTransaksi_PembayaranHutangLeasing _
                Or JenisTransaksi = JenisTransaksi_PembayaranHutangPihakKetiga _
                Or JenisTransaksi = JenisTransaksi_PencairanPiutangPihakKetiga _
                Or JenisTransaksi = JenisTransaksi_PembayaranHutangAfiliasi _
                Or JenisTransaksi = JenisTransaksi_PencairanPiutangAfiliasi _
                Then JumlahMutasiCOAUTama = JumlahPokokAngsuran


            'Simpan Jurnal :
            If AlurTransaksi = AlurTransaksi_OUT Then '============= TRANSAKSI OUT ====================================================
                ___jurDebet(COAUtama, JumlahMutasiCOAUTama)
                ___jurDebet(KodeTautanCOA_BiayaBungaBank, BungaBagiHasilAngsuran)
                ___jurDebet(KodeTautanCOA_BiayaDendaBank, DendaAngsuran)
                ___jurDebet(COABiayaPPh, JumlahPPhDitanggung)
                ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
                _______jurKredit(COAHutangPPh, JumlahPPhTerutang)
                _______jurKreditBankCashOUT(DitanggungOleh, COASaranaPembayaran, JumlahMutasiBankCash, JumlahTransfer, BiayaAdministrasiBank)
            End If '--------------------------------------------------------------------------------------------------------------------

            If AlurTransaksi = AlurTransaksi_IN Then '============= TRANSAKSI IN =======================================================
                ''Eliminasi Baris Jurnal Tertentu :
                If PencairanPiutangJualAsset = True Then
                    JumlahPPhDipotong = 0
                    JumlahPPhDitanggung = 0
                Else
                    If JenisTransaksi <> JenisTransaksi_PencairanPiutangUsaha Then JumlahPPhTerutang = 0
                End If
                ___jurDebetBankCashIN(DitanggungOleh, COASaranaPembayaran, JumlahMutasiBankCash, JumlahTransfer, BiayaAdministrasiBank)
                ___jurDebet(COABiayaPPh, JumlahPPhTerutang)
                ___jurDebet(COAPPhPrepaid, JumlahPPhTerutang) '(PPh Dibayar Dimuka / Prepaid)
                _______jurKredit(COAHutangPPh, JumlahPPhTerutang)
                _______jurKredit(KodeTautanCOA_PenghasilanBungaDendaPinjaman, (BungaBagiHasilAngsuran + DendaAngsuran))
                _______jurKredit(COAPenghasilan, JumlahPPhDitanggung)
                _______jurKredit(COAUtama, JumlahMutasiCOAUTama)
            End If '--------------------------------------------------------------------------------------------------------------------

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                jur_StatusPenyimpananJurnal_Lengkap = True
                If usc_JurnalUmum.StatusAktif Then usc_JurnalUmum.TampilkanData()
            Else
                jur_StatusPenyimpananJurnal_Lengkap = False
            End If

        Else

            jur_StatusPenyimpananJurnal_Lengkap = True
            '(Untuk Tahun Buku LAMPAU, tidak ada penyimpanan Jurnal, maka anggap saja penyimpanan SUKSES...!!!)
            '(Maka, value status penyimpanannya dibikin True)

        End If

        If jur_StatusPenyimpananJurnal_Lengkap = True Then
            PenyimpananSukses = True
        Else
            PenyimpananSukses = False
        End If

        '----------------------------------------- BATAS AKHIR CODING PENYIMPANAN DATA JURNAL -----------------------------------------



        'Jika Jenis Transaksi : Pembayaran Gaji :
        'Update data di Tabel Pengajuan Pembayaran dengan memberikan nilai "DIBAYAR" pada kolom Status, sebagai tanda sudah dibayar :
        If JenisTransaksi = JenisTransaksi_PembayaranGaji Then usc_BukuPengawasanGaji.TampilkanData()


        'Jika Jenis Transaksi : Pembayaran BPJS Kesehatan :
        'Update data di Tabel Pengajuan Pembayaran dengan memberikan nilai "DIBAYAR" pada kolom Status, sebagai tanda sudah dibayar :
        If usc_BukuPengawasanGaji.StatusAktif Then usc_BukuPengawasanGaji.RefreshTampilanData()


        'Jika Jenis Transaksi : Pembayaran HutangUsaha :
        'Update data di Tabel Pengajuan Pembayaran dengan memberikan nilai "DIBAYAR" pada kolom Status, sebagai tanda sudah dibayar :
        If JenisTransaksi = JenisTransaksi_PembayaranHutangUsaha Then
            AksesDatabase_Transaksi(Buka)
            cmd = New Odbc.OdbcCommand(" UPDATE tbl_PengajuanPembayaranHutangUsaha SET Status = 'DIBAYAR', " &
                                       " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                       " Catatan = '" & UraianTransaksi & "' " &
                                       " WHERE Nomor_Pengajuan = '" & NPPHU & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'Jika Jenis Transaksi : Pembayaran Hutang Pihak Ketiga :
        If usc_BukuPengawasanHutangPihakKetiga.StatusAktif Then usc_BukuPengawasanHutangPihakKetiga.TampilkanData()

        'Jika Jenis Transaksi : Pembayaran Hutang Pajak :
        'Update data di Tabel Pengajuan Pembayaran dengan memberikan nilai "DIBAYAR" pada kolom Status, sebagai tanda sudah dibayar :
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPajak Then
            AksesDatabase_Transaksi(Buka)
            cmd = New Odbc.OdbcCommand(" UPDATE tbl_PengajuanPembayaranHutangPajak SET Status = 'DIBAYAR', " &
                                       " Tanggal_Bayar = '" & TanggalTransaksiSimpan & "', " &
                                       " Catatan = '" & UraianTransaksi & "' " &
                                       " WHERE Nomor_Pengajuan = '" & NPPHP & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            'frm_LembarPengajuanPembayaranHutangPajak.TampilkanData()
        End If

        If JenisTransaksi = JenisTransaksi_UangMukaPembelian Then
            'Belum ada coding
        End If

        If JenisTransaksi = JenisTransaksi_PemberianPiutangKas Then
            'Belum ada coding
        End If

        If JenisTransaksi = JenisTransaksi_DanaKeluarLainnya Then
            'Belum ada coding
        End If

        If JenisTransaksi = JenisTransaksi_PencairanPiutangUsaha Then
            If usc_BukuPengawasanPiutangUsaha.StatusAktif Then usc_BukuPengawasanPiutangUsaha.TampilkanData()
        End If

        If PenyimpananSukses = True Then
            MsgBox("Data Transaksi BERHASIL disimpan.")
        Else
            MsgBox("Data Transaksi GAGAL disimpan.")
        End If

        If JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANGAJI _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKESEHATAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKETENAGAKERJAAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGUSAHA _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGKARYAWAN _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGBANKLEASING _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGPIHAKKETIGA _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGAFILIASI _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGPEMEGANGSAHAM _
            Or JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGPAJAK _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL21 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL23 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL25 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL26 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL29 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL42 _
            Or JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPPN _
            Or JalurMasuk = Halaman_INPUTTRANSAKSIKAS _
            Then
            Me.Close()
        End If

    End Sub

    Private Sub cmb_JenisTransaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisTransaksi.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisTransaksi_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisTransaksi.TextChanged

        txt_Referensi.Clear()
        JenisTransaksi = cmb_JenisTransaksi.Text

        'Kelompot Jenis Transaksi OUT :
        If JenisTransaksi = JenisTransaksi_PembayaranGaji Then COAUtama = KodeTautanCOA_HutangGaji
        If JenisTransaksi = JenisTransaksi_PembayaranBpjsKesehatan Then COAUtama = KodeTautanCOA_HutangBpjsKesehatan
        If JenisTransaksi = JenisTransaksi_PembayaranBpjsKetenagakerjaan Then COAUtama = KodeTautanCOA_HutangBpjsKetenagakerjaan
        If JenisTransaksi = JenisTransaksi_PembayaranHutangUsaha Then COAUtama = KodeTautanCOA_HutangUsaha_NonAfiliasi
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPemegangSaham Then COAUtama = KodeTautanCOA_HutangPemegangSaham
        If JenisTransaksi = JenisTransaksi_PembayaranHutangAfiliasi Then COAUtama = KodeTautanCOA_HutangAfiliasi
        If JenisTransaksi = JenisTransaksi_PembayaranHutangBank Then COAUtama = KodeTautanCOA_HutangBank
        If JenisTransaksi = JenisTransaksi_PembayaranHutangLeasing Then COAUtama = KodeTautanCOA_HutangLeasing
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPihakKetiga Then COAUtama = KodeTautanCOA_HutangPihakKetiga
        If JenisTransaksi = JenisTransaksi_PembayaranHutangKaryawan Then COAUtama = KodeTautanCOA_HutangKaryawan
        If JenisTransaksi = JenisTransaksi_PembayaranHutangPajak Then COAUtama = PenentuanCOA_HutangPajak(JenisPajak, KodeSetoran)
        If JenisTransaksi = JenisTransaksi_UangMukaPembelian Then COAUtama = Kosongan 'COA Belum ada Nilainya
        If JenisTransaksi = JenisTransaksi_PemberianPiutangKas Then COAUtama = Kosongan 'COA Belum ada Nilainya
        If JenisTransaksi = JenisTransaksi_DanaKeluarLainnya Then COAUtama = Kosongan 'COA Belum ada Nilainya

        'Kelompot Jenis Transaksi IN :
        If JenisTransaksi = JenisTransaksi_PencairanPiutangAfiliasi Then COAUtama = KodeTautanCOA_PiutangAfiliasi
        If JenisTransaksi = JenisTransaksi_PencairanPiutangKaryawan Then COAUtama = KodeTautanCOA_PiutangKaryawan
        If JenisTransaksi = JenisTransaksi_PencairanPiutangPihakKetiga Then COAUtama = KodeTautanCOA_PiutangPihakKetiga
        If JenisTransaksi = JenisTransaksi_PencairanPiutangPemegangSaham Then COAUtama = KodeTautanCOA_PiutangPemegangSaham
        If JenisTransaksi = JenisTransaksi_PencairanPiutangUsaha Then COAUtama = KodeTautanCOA_PiutangUsaha_NonAfiliasi
        If JenisTransaksi = JenisTransaksi_DanaMasukLainnya Then COAUtama = Kosongan 'COA Belum ada Nilainya

    End Sub

    Private Sub txt_Referensi_TextChanged(sender As Object, e As EventArgs) Handles txt_Referensi.TextChanged
        Referensi = txt_Referensi.Text
    End Sub
    Private Sub txt_Referensi_Click(sender As Object, e As EventArgs) Handles txt_Referensi.Click
        btn_PilihReferensi_Click(sender, e)
    End Sub
    Private Sub txt_Referensi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Referensi.KeyPress
        If AlurTransaksi = AlurTransaksi_OUT Then KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihReferensi_Click(sender As Object, e As EventArgs) Handles btn_PilihReferensi.Click
        MsgBox("Fitur ini belum bisa digunakan." & Enter2Baris & "Untuk sementara, input pembayaran melalui modul Pengajuan Pembayaran.")
        Return
        If JalurMasuk = Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA Then
        End If
        If JenisTransaksi = "Pembayaran Hutang Usaha" Then
            'frm_ListReferensiPengajuanPembayaranHutangUsaha.ShowDialog()
        End If
    End Sub

    Private Sub cmb_SaranaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SaranaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        txt_COA.Text = Microsoft.VisualBasic.Left(SaranaPembayaran, JumlahDigitCOA)
        If AmbilAngka(COASaranaPembayaran) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COASaranaPembayaran) <= kodeakun_Bank_Akhir _
            Then
            grb_Bank.Enabled = True
            KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
            If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
    End Sub

    Private Sub txt_COA_TextChanged(sender As Object, e As EventArgs) Handles txt_COA.TextChanged
        COASaranaPembayaran = txt_COA.Text
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA " &
                              " WHERE COA = '" & COASaranaPembayaran & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            txt_NamaAkun.Text = Nothing
        Else
            txt_NamaAkun.Text = dr.Item("Nama_Akun")
        End If
        AksesDatabase_General(Tutup)
    End Sub
    Private Sub txt_COA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
        cmb_JenisJurnal.Text = NamaAkun
    End Sub
    Private Sub txt_NamaAkun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TanggalInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_TanggalInvoice.TextChanged
        TanggalInvoice = txt_TanggalInvoice.Text
    End Sub
    Private Sub txt_TanggalInvoice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TanggalInvoice.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
        NTPN = txt_NomorInvoice.Text
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalTransaksi)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalTransaksi)
        TanggalTransaksi = Microsoft.VisualBasic.Left(dtp_TanggalTransaksi.Value, 10)
        TahunTransaksi = Format(dtp_TanggalTransaksi.Value, "yyyy")
    End Sub

    Sub Reset_grb_Bank()
        grb_Bank.Enabled = False
        txt_BiayaAdministrasiBank.Text = Nothing
        cmb_DitanggungOleh.Text = Nothing
        If Not (AmbilAngka(COASaranaPembayaran) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COASaranaPembayaran) <= kodeakun_Bank_Akhir) _
            Then
            txt_JumlahTransfer.Text = Nothing
            txt_TotalBank.Text = Nothing
        End If
    End Sub

    Private Sub txt_BiayaAdministrasiBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaAdministrasiBank.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.Enabled = False
            cmb_DitanggungOleh.Text = Kosongan
        Else
            If AlurTransaksi = AlurTransaksi_OUT Then cmb_DitanggungOleh.Enabled = True
            If AlurTransaksi = AlurTransaksi_IN Then cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DitanggungOleh.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_DitanggungOleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.SelectedIndexChanged
    End Sub
    Private Sub cmb_DitanggungOleh_TextChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.TextChanged
        DitanggungOleh = cmb_DitanggungOleh.Text
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub txt_JumlahTransfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransfer.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahTransfer_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransfer.TextChanged
        'JumlahTransfer = AmbilAngka(txt_JumlahTransfer.Text)   '(Ini gak perlu. Karena sudah ada di perhitungan).
        PemecahRibuanUntukTextBox(txt_JumlahTransfer)
    End Sub

    Private Sub txt_TotalBank_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalBank.TextChanged
        TotalBank = AmbilAngka(txt_TotalBank.Text)
        PemecahRibuanUntukTextBox(txt_TotalBank)
    End Sub
    Private Sub txt_TotalBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalBank.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = txt_UraianTransaksi.Text
    End Sub

    Private Sub cmb_JenisJurnal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJurnal_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.TextChanged
        JenisJurnal = cmb_JenisJurnal.Text
    End Sub
    Private Sub cmb_JenisJurnal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJurnal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class