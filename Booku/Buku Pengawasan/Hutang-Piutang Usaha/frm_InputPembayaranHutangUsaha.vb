Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembayaranHutangUsaha

    Public FungsiForm
    Public JudulForm
    Public AlurTransaksi
    Public AlurTransaksi_PembayaranHutangUsaha = "Pembayaran Hutang Usaha"
    Public AlurTransaksi_PencairanPiutangUsaha = "Pencairan Piutang Usaha"
    Public ProsesSuntingDatabase As Boolean

    Public Referensi
    Public NomorBPHU
    Public NomorBPPU
    Public NomorPembelian
    Public NomorPenjualan
    Public NomorIdBayar As Integer
    Public TanggalInvoice
    Public NomorInvoice
    Public KodeMitra
    Public NamaMitra
    Public TanggalFakturPajak
    Public NomorFakturPajak
    Public JumlahTerutang
    Public JumlahPPh_Total
    Public JumlahPPhDitanggung_Total
    Public JumlahPPhDipotong_Total
    Public JumlahTelahDibayar
    Public JumlahPPh_Dibayar As Int64                '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahPPhDitanggung_Dibayar
    Public JumlahPPhDipotong_Dibayar
    Public JumlahBayarSekarang
    Public JumlahPPh As Int64                        '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahPPhDitanggung As Int64              '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahPPh_ValueAwal_dB As Int64           '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahPPhDipotong_ValueAwal_dB As Int64   '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahPPhDipotong As Int64                '(Ini sudah benar As Int64..!!! Jangan dirubah...!!! Karena variabel ini harus angka bulat tanpa koma/pecahan...!!!).
    Public JumlahMutasiBankCash
    Public JenisPPh
    Public KodeSetoran
    Public SisaHutang
    Public TanggalBayar
    Public SaranaPembayaran
    Public Keterangan
    Public NomorJVBayar
    Public TermasukHutangTahunIni As Boolean

    Dim COASaranaPembayaran
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh As String

    Dim PencairanPiutangJualAsset As Boolean

    Dim JenisJurnal

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If AlurTransaksi = Kosongan Then
            PesanUntukProgrammer("Tentukan Alur Pembayaran (Hutang/Piutang).")
            Me.Close()
        End If

        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then
            lbl_NomorBPHU.Text = "Nomor BPHU"
            lbl_NomorPembelian.Text = "Nomor Pembelian"
            lbl_JumlahTerutang.Text = "Jumlah Terutang"
            lbl_SisaHutang.Text = "Sisa Hutang"
            lbl_JumlahBayar.Text = "Jumlah Bayar"
            lbl_JumlahBankCash.Text = "Jumlah Dibayarkan"
        End If

        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
            lbl_NomorBPHU.Text = "Nomor BPPU"
            lbl_NomorPembelian.Text = "Nomor Penjualan"
            lbl_JumlahTerutang.Text = "Jumlah Piutang"
            lbl_SisaHutang.Text = "Sisa Piutang"
            lbl_JumlahBayar.Text = "Jumlah Piutang Dibayar"
            lbl_JumlahBankCash.Text = "Jumlah Diterima"
        End If

        SisaHutang = txt_SisaHutang.Text

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input " & AlurTransaksi
            btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
            Select Case AlurTransaksi
                Case AlurTransaksi_PembayaranHutangUsaha
                    AksesDatabase_Transaksi(Buka)
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha " &
                                          " WHERE Angka_NPPHU IN (SELECT MAX(Angka_NPPHU) " &
                                          " FROM tbl_PengajuanPembayaranHutangUsaha) ", KoneksiDatabaseTransaksi)
                    dr = cmd.ExecuteReader
                    dr.Read()
                    If Not dr.HasRows Then
                        Referensi = AwalanNPPHU_PlusTahunBuku & "1"
                    Else
                        Referensi = AwalanNPPHU_PlusTahunBuku & (dr.Item("Angka_NPPHU") + 1).ToString
                    End If
                    AksesDatabase_Transaksi(Tutup)
                Case AlurTransaksi_PencairanPiutangUsaha
                    Referensi = txt_NomorBPHU.Text
            End Select
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit " & AlurTransaksi
            btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
            Dim KolomCOA = Kosongan
            Dim KolomMutasi = Kosongan
            AksesDatabase_Transaksi(Buka)
            Select Case AlurTransaksi
                Case AlurTransaksi_PembayaranHutangUsaha
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                                          " WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
                    KolomCOA = "COA_Kredit"
                    KolomMutasi = "Jumlah_Kredit"

                Case AlurTransaksi_PencairanPiutangUsaha
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PencairanPiutangUsaha " &
                                          " WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
                    KolomCOA = "COA_Debet"
                    KolomMutasi = "Jumlah_Debet"
            End Select
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                JumlahPPhDipotong_ValueAwal_dB = dr.Item("Jumlah_PPh_Dipotong")
                JumlahPPh_ValueAwal_dB = dr.Item("Jumlah_PPh_Terutang")
                JumlahPPhDipotong = dr.Item("Jumlah_PPh_Dipotong")
                JumlahPPh = dr.Item("Jumlah_PPh_Terutang")
                JumlahMutasiBankCash = dr.Item(KolomMutasi)
                txt_JumlahPPhDipotong.Text = JumlahPPhDipotong
                txt_JumlahBankCash.Text = JumlahMutasiBankCash
                COASaranaPembayaran = dr.Item(KolomCOA)
                DitanggungOleh = dr.Item("Ditanggung_Oleh")
                txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank") 'Baris ini harus di posisi persis sebelum AksesDatabase_Transaksi(Tutup)
            End If
            AksesDatabase_Transaksi(Tutup)
            cmb_SaranaPembayaran.Text = COASaranaPembayaran & StripPemisah & AmbilValue_NamaAkun(COASaranaPembayaran)
        End If

        Me.Text = JudulForm

        'Isi Value Jenis PPh dan Boolean JualAsset :
        Dim TabelInvoice = Kosongan
        AksesDatabase_Transaksi(Buka)
        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then TabelInvoice = "tbl_Pembelian_Invoice"
        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then TabelInvoice = "tbl_Penjualan_Invoice"
        cmd = New OdbcCommand(" SELECT * FROM " & TabelInvoice &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        JenisPPh = dr.Item("Jenis_PPh")
        KodeSetoran = dr.Item("Kode_Setoran")
        AksesDatabase_Transaksi(Tutup)

        PencairanPiutangJualAsset = AmbilValueBoolean_JualAsset(NomorInvoice)

        ProsesLoadingForm = False

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        frm_InputTransaksi.PenyimpananSukses = False

        AlurTransaksi = Kosongan

        NomorIdBayar = 0
        Referensi = Nothing

        txt_NomorBPHU.Enabled = False
        txt_NomorPembelian.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        lbl_BiayaAdministrasiBank.Visible = False
        txt_BiayaAdministrasiBank.Visible = False
        txt_JumlahTerutang.Enabled = False
        txt_JumlahTelahDibayar.Enabled = False
        txt_SisaHutang.Enabled = False
        dtp_TanggalBayar.Enabled = True
        txt_JumlahBayarSekarang.Enabled = True
        txt_JumlahPPhDipotong.Enabled = True
        txt_JumlahBankCash.Enabled = True
        txt_Keterangan.Enabled = True

        txt_NomorBPHU.Text = Nothing
        txt_NomorPembelian.Text = Nothing
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_BiayaAdministrasiBank.Text = Kosongan
        DitanggungOleh = Kosongan
        txt_JumlahTerutang.Text = Nothing
        txt_JumlahTelahDibayar.Text = Nothing
        txt_SisaHutang.Text = Nothing
        dtp_TanggalBayar.Value = Today
        txt_JumlahBayarSekarang.Text = Nothing
        txt_JumlahPPhDipotong.Text = Nothing
        txt_JumlahBankCash.Text = Nothing
        txt_Keterangan.Text = Kosongan

        JumlahPPh = 0
        JumlahPPh_Total = 0
        JumlahPPh_Dibayar = 0
        JumlahPPh_ValueAwal_dB = 0
        JumlahPPhDitanggung = 0
        JumlahPPhDitanggung_Total = 0
        JumlahPPhDitanggung_Dibayar = 0
        JumlahPPhDipotong = 0
        JumlahPPhDipotong_Total = 0
        JumlahPPhDipotong_Dibayar = 0
        JumlahPPhDipotong_ValueAwal_dB = 0

        JenisPPh = Kosongan
        KodeSetoran = Kosongan

        PencairanPiutangJualAsset = False

        ProsesResetForm = False

    End Sub

    Sub PerhitunganRincianPembayaran()

        Dim RasioBayar As Decimal
        Dim QueryTelusur = Kosongan

        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then
            RasioBayar = JumlahBayarSekarang / JumlahTerutang
            If JumlahBayarSekarang >= SisaHutang Then '(Jika pembayaran untuk pelunasan) :
                QueryTelusur = " SELECT * FROM tbl_PembayaranHutangUsaha WHERE Nomor_BPHU = '" & NomorBPHU & "' "
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(QueryTelusur, KoneksiDatabaseTransaksi)
                dr = cmd.ExecuteReader
                JumlahPPhDipotong_Dibayar = 0
                JumlahPPh_Dibayar = 0
                JumlahPPhDitanggung_Dibayar = 0
                Do While dr.Read
                    JumlahPPhDipotong_Dibayar += dr.Item("Jumlah_PPh_Dipotong")
                    JumlahPPh_Dibayar += dr.Item("Jumlah_PPh_Terutang")
                Loop
                AksesDatabase_Transaksi(Tutup)
                If FungsiForm = FungsiForm_EDIT Then
                    JumlahPPhDipotong_Dibayar -= JumlahPPhDipotong_ValueAwal_dB
                    JumlahPPh_Dibayar -= JumlahPPh_ValueAwal_dB
                End If
                JumlahPPhDitanggung_Dibayar = JumlahPPh_Dibayar - JumlahPPhDipotong_Dibayar
                JumlahPPhDipotong = JumlahPPhDipotong_Total - JumlahPPhDipotong_Dibayar
                JumlahPPh = JumlahPPh_Total - JumlahPPh_Dibayar
                'JumlahPPhDitanggung = JumlahPPhDitanggung_Total - JumlahPPhDitanggung_Dibayar
            Else '(Jika belum pelunasan) :
                JumlahPPhDipotong = JumlahPPhDipotong_Total * RasioBayar
                JumlahPPh = JumlahPPh_Total * RasioBayar
                'JumlahPPhDitanggung = JumlahPPh - JumlahPPhDipotong
            End If
            JumlahPPhDitanggung = JumlahPPh - JumlahPPhDipotong
            txt_JumlahPPhDipotong.Text = JumlahPPhDipotong
            JumlahMutasiBankCash = JumlahBayarSekarang - JumlahPPhDipotong
        End If

        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
            RasioBayar = JumlahBayarSekarang / JumlahTerutang
            If JumlahBayarSekarang >= SisaHutang Then '(Jika pembayaran untuk pelunasan) :
                QueryTelusur = " SELECT * FROM tbl_PencairanPiutangUsaha WHERE Nomor_BPPU = '" & NomorBPPU & "' "
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(QueryTelusur, KoneksiDatabaseTransaksi)
                dr = cmd.ExecuteReader
                JumlahPPhDipotong_Dibayar = 0
                JumlahPPh_Dibayar = 0
                JumlahPPhDitanggung_Dibayar = 0
                Do While dr.Read
                    JumlahPPhDipotong_Dibayar += dr.Item("Jumlah_PPh_Dipotong")
                    JumlahPPh_Dibayar += dr.Item("Jumlah_PPh_Terutang")
                Loop
                AksesDatabase_Transaksi(Tutup)
                If FungsiForm = FungsiForm_EDIT Then
                    JumlahPPhDipotong_Dibayar -= JumlahPPhDipotong_ValueAwal_dB
                    JumlahPPh_Dibayar -= JumlahPPh_ValueAwal_dB
                End If
                JumlahPPhDitanggung_Dibayar = JumlahPPh_Dibayar - JumlahPPhDipotong_Dibayar
                JumlahPPhDipotong = JumlahPPhDipotong_Total - JumlahPPhDipotong_Dibayar
                JumlahPPh = JumlahPPh_Total - JumlahPPh_Dibayar
                'JumlahPPhDitanggung = JumlahPPhDitanggung_Total - JumlahPPhDitanggung_Dibayar
            Else '(Jika belum pelunasan) :
                JumlahPPhDipotong = JumlahPPhDipotong_Total * RasioBayar
                JumlahPPh = JumlahPPh_Total * RasioBayar
                'JumlahPPhDitanggung = JumlahPPh - JumlahPPhDipotong
            End If
            JumlahPPhDitanggung = JumlahPPh - JumlahPPhDipotong
            txt_JumlahPPhDipotong.Text = JumlahPPhDipotong
            JumlahMutasiBankCash = JumlahBayarSekarang - JumlahPPhDipotong
        End If

        txt_JumlahBankCash.Text = JumlahMutasiBankCash

    End Sub

    Private Sub txt_NomorBPHU_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPHU.TextChanged
        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then
            NomorBPHU = txt_NomorBPHU.Text
            NomorBPPU = Kosongan
        End If
        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
            NomorBPPU = txt_NomorBPHU.Text
            NomorBPHU = Kosongan
        End If
    End Sub

    Private Sub txt_NomorPembelian_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorPembelian.TextChanged
        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then
            NomorPembelian = txt_NomorPembelian.Text
            NomorPenjualan = Kosongan
        End If
        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
            NomorPenjualan = txt_NomorPembelian.Text
            NomorPembelian = Kosongan
        End If
    End Sub

    Private Sub txt_JumlahTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTerutang.TextChanged
        JumlahTerutang = AmbilAngka(txt_JumlahTerutang.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTerutang)
    End Sub
    Private Sub txt_JumlahTerutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTerutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTelahDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTelahDibayar.TextChanged
        JumlahTelahDibayar = AmbilAngka(txt_JumlahTelahDibayar.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTelahDibayar)
    End Sub
    Private Sub txt_JumlahTelahDibayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTelahDibayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SisaHutang_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaHutang.TextChanged
        SisaHutang = AmbilAngka(txt_SisaHutang.Text)
        PemecahRibuanUntukTextBox(txt_SisaHutang)
    End Sub
    Private Sub txt_SisaHutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SisaHutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahBayarSekarang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBayarSekarang.TextChanged
        JumlahBayarSekarang = AmbilAngka(txt_JumlahBayarSekarang.Text)
        PemecahRibuanUntukTextBox(txt_JumlahBayarSekarang)
        If JumlahBayarSekarang > SisaHutang And FungsiForm = FungsiForm_TAMBAH Then
            MsgBox("Nominal Jumlah Bayar melebihi Sisa Hutang." & Enter2Baris &
                   "Silakan isi kolom 'Jumlah Bayar' dengan benar.")
            txt_JumlahBayarSekarang.Text = Nothing
            txt_JumlahBayarSekarang.Focus()
        End If
        If ProsesLoadingForm = False And ProsesResetForm = False Then PerhitunganRincianPembayaran()
    End Sub
    Private Sub txt_JumlahBayarSekarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahBayarSekarang.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_JumlahBankCash_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBankCash.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahBankCash)
    End Sub
    Private Sub txt_JumlahBankCash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahBankCash.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahPPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPhDipotong.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahPPhDipotong)
    End Sub
    Private Sub txt_JumlahPPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPPhDipotong.KeyPress
        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then KunciTotalInputan(sender, e)
        If PencairanPiutangJualAsset = True Then KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_TanggalBayar_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBayar.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalBayar)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBayar)
        If dtp_TanggalBayar.Value > Today Then
            MsgBox("Tanggal Bayar melebihi hari ini." & Enter2Baris & "Silakan isi kolom 'Tanggal Bayar' dengan benar.")
            dtp_TanggalBayar.Value = Today
            dtp_TanggalBayar.Focus()
            Return
        End If
        TanggalBayar = dtp_TanggalBayar.Value
    End Sub

    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SaranaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        JenisJurnal = AmbilTeksTengahTakTerbatas(SaranaPembayaran, JumlahDigitCOA + PanjangTeks(StripPemisah) + 1)
        COASaranaPembayaran = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If AmbilAngka(COASaranaPembayaran) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COASaranaPembayaran) <= kodeakun_Bank_Akhir _
            Then
            lbl_BiayaAdministrasiBank.Visible = True
            txt_BiayaAdministrasiBank.Visible = True
        Else
            lbl_BiayaAdministrasiBank.Visible = False
            txt_BiayaAdministrasiBank.Visible = False
            txt_BiayaAdministrasiBank.Text = Kosongan
        End If
    End Sub

    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        If BiayaAdministrasiBank = 0 Then DitanggungOleh = Kosongan
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        PerhitunganRincianPembayaran()
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If JumlahBayarSekarang = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Bayar'")
            txt_JumlahBayarSekarang.Focus()
            Return
        End If

        If SaranaPembayaran = Nothing Then
            MsgBox("Silakan pilih 'Sarana Pembayaran'")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        frm_InputTransaksi.ResetForm()
        ProsesIsiValueForm = True
        frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGUSAHA
        frm_InputTransaksi.FungsiForm = FungsiForm
        If AlurTransaksi = AlurTransaksi_PembayaranHutangUsaha Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_OUT
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranHutangUsaha
            frm_InputTransaksi.NomorBPHU = NomorBPHU
            frm_InputTransaksi.NomorPembelian = NomorPembelian
        End If
        If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_IN
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PencairanPiutangUsaha
            frm_InputTransaksi.NomorBPPU = NomorBPPU
            frm_InputTransaksi.NomorPenjualan = NomorPenjualan
        End If
        If FungsiForm = FungsiForm_EDIT Then
            frm_InputTransaksi.NomorID = NomorIdBayar
            jur_NomorJV = NomorJVBayar
        End If
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangTahunIni
        frm_InputTransaksi.txt_Referensi.Text = Referensi
        frm_InputTransaksi.txt_TanggalInvoice.Text = TanggalInvoice
        frm_InputTransaksi.txt_NomorInvoice.Text = NomorInvoice
        frm_InputTransaksi.txt_NomorFakturPajak.Text = NomorFakturPajak
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeMitra
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaMitra
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahBayarSekarang
        frm_InputTransaksi.JumlahMutasiBankCash = JumlahMutasiBankCash
        frm_InputTransaksi.JumlahPPhDipotong = JumlahPPhDipotong
        frm_InputTransaksi.JumlahPPhDitanggung = JumlahPPhDitanggung
        frm_InputTransaksi.JumlahPPhTerutang = JumlahPPh
        frm_InputTransaksi.JenisPPh = JenisPPh
        frm_InputTransaksi.KodeSetoran = KodeSetoran
        Select Case AlurTransaksi
            Case AlurTransaksi_PembayaranHutangUsaha
                frm_InputTransaksi.COAHutangPPh = PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran)
                frm_InputTransaksi.COABiayaPPh = PenentuanCOA_BiayaPPh(JenisPPh)
            Case AlurTransaksi_PencairanPiutangUsaha
                If PencairanPiutangJualAsset = True Then
                    frm_InputTransaksi.PencairanPiutangJualAsset = True
                    frm_InputTransaksi.COAHutangPPh = PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran)
                    frm_InputTransaksi.COABiayaPPh = KodeTautanCOA_BiayaPPhPasal42_402
                Else
                    frm_InputTransaksi.PencairanPiutangJualAsset = False
                    frm_InputTransaksi.COAPPhPrepaid = PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh) '(PPh Prepaid)
                End If
                frm_InputTransaksi.COAPenghasilan = KodeTautanCOA_PenghasilanLainnya
                PesanUntukProgrammer(KodeTautanCOA_PenghasilanLainnya & " - " & JumlahPPhDitanggung)
        End Select
        If COASaranaPembayaran >= KodeAkun_Bank_Awal And COASaranaPembayaran <= kodeakun_Bank_Akhir Then
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh
            If AlurTransaksi = AlurTransaksi_PencairanPiutangUsaha Then
                If BiayaAdministrasiBank > 0 Then
                    frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
                    'Maksudnya, setiap ada Biaya Administrasi dalam pencairan, maka sudah pasti itu ditanggung oleh Perusahaan.
                    'Kalau ditanggung oleh lawan transaksi, maka tidak perlu ada pencatatan Biaya Administrasi Bank,
                    'Dalam arti, ya dikosongkan saja, mau berapa pun Biaya Administrasinya.
                End If
            End If
            frm_InputTransaksi.Perhitungan_ValueBank()
        Else
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = Kosongan
            frm_InputTransaksi.cmb_DitanggungOleh.Text = Kosongan
        End If
        frm_InputTransaksi.dtp_TanggalTransaksi.Value = TanggalBayar
        frm_InputTransaksi.txt_UraianTransaksi.Text = Keterangan
        frm_InputTransaksi.cmb_JenisJurnal.Text = JenisJurnal
        frm_InputTransaksi.cmb_JenisJurnal.Enabled = False
        ProsesIsiValueForm = False

        frm_InputTransaksi.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then
            Me.Close()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class