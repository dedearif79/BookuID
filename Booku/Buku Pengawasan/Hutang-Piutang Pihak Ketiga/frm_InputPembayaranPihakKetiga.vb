Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembayaranPihakKetiga

    Public FungsiForm
    Public JudulForm
    Public ProsesSuntingDatabase As Boolean

    Public HutangPiutang
    Dim TabelPengawasan
    Dim TabelAngsuran
    Dim KolomNomorBP
    Dim KolomCOASaranaPembayaran
    Dim AwalanBP

    Public JumlahBarisAngsuran
    Public AngsuranKe_Array() As Integer
    Public AngsuranKe As String
    Public Referensi
    Dim NomorBP
    Public NomorIdBayar As Integer
    Public TanggalPencairan
    Public NomorKontrak
    Public KodeKreditur
    Public NamaKreditur
    Dim Pokok
    Dim BagiHasil
    Public JumlahPPh
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim Denda
    Dim JumlahDibayarkan
    Public TanggalBayar
    Public SaranaPembayaran
    Public Keterangan
    Public NomorJVBayar

    Public JenisPPh
    Public KodeSetoran

    Dim COASaranaPembayaran
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh As String

    Dim JenisJurnal

    Dim QueryAmbilData

    Public TermasukHutangTahunIni As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Select Case HutangPiutang
            Case hp_Hutang
                TabelPengawasan = "tbl_PengawasanHutangPihakKetiga"
                TabelAngsuran = "tbl_JadwalAngsuranHutangPihakKetiga"
                KolomNomorBP = "Nomor_BPHPK"
                KolomCOASaranaPembayaran = "COA_Kredit"
                AwalanBP = AwalanBPHPK_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPHPK"
            Case hp_Piutang
                TabelPengawasan = "tbl_PengawasanPiutangPihakKetiga"
                TabelAngsuran = "tbl_JadwalAngsuranPiutangPihakKetiga"
                KolomNomorBP = "Nomor_BPPPK"
                KolomCOASaranaPembayaran = "COA_Debet"
                AwalanBP = AwalanBPPPK_PlusTahunBuku
                lbl_NomorBP.Text = "Nomor BPPPK"
            Case Else
                PesanUntukProgrammer("Tentukan dulu, hutang atau piutang..!!!")
        End Select

        Dim AngsuranKe_Index = 0
        Do While AngsuranKe_Index < AngsuranKe_Array.Length
            If AngsuranKe_Index = 0 Then
                AngsuranKe = AngsuranKe_Array(AngsuranKe_Index).ToString
            Else
                AngsuranKe = AngsuranKe & ", " & AngsuranKe_Array(AngsuranKe_Index).ToString
            End If
            AngsuranKe_Index += 1
        Loop

        '------------------------------------------------------------------------------------------------
        'Baik Fungsi Form TAMBAH maupun EDIT, membutuhkan data ini. 
        'Jadi, sudah benar ini disimpan disini. Jangan dipindahkan ke Fungsi Form EDIT..!)
        AksesDatabase_Transaksi(Buka)
        If FungsiForm = FungsiForm_TAMBAH Then QueryAmbilData =
            " SELECT * FROM " & TabelAngsuran & " WHERE " & KolomNomorBP & " = '" & NomorBP & "' "
        If FungsiForm = FungsiForm_EDIT Then QueryAmbilData =
            " SELECT * FROM " & TabelAngsuran & " WHERE Nomor_JV = '" & NomorJVBayar & "' "
        cmd = New OdbcCommand(QueryAmbilData, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Denda = 0
        Do While dr.Read
            Denda += dr.Item("Denda")
            JenisPPh = dr.Item("Jenis_PPh")
            KodeSetoran = dr.Item("Kode_Setoran")
            COASaranaPembayaran = dr.Item(KolomCOASaranaPembayaran)
            BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
            DitanggungOleh = dr.Item("Ditanggung_Oleh")
            Keterangan = dr.Item("Keterangan")
        Loop
        AksesDatabase_Transaksi(Tutup)
        '------------------------------------------------------------------------------------------------

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Angsuran " & HutangPiutang & " Pihak Ketiga" & " - " & NamaKreditur
            btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Angsuran " & HutangPiutang & " Pihak Ketiga" & " - " & NamaKreditur
            btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
            txt_Denda.Text = Denda
            txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            txt_Keterangan.Text = Keterangan
            cmb_SaranaPembayaran.Text = KonversiCOAKeSaranaPembayaran(COASaranaPembayaran)
        End If

        Me.Text = JudulForm

        lbl_AngsuranKe.Text = "Angsuran Ke : " & AngsuranKe


        ProsesLoadingForm = False

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        HutangPiutang = Kosongan

        frm_InputTransaksi.PenyimpananSukses = False

        AngsuranKe = Kosongan
        NomorIdBayar = 0
        NomorJVBayar = 0
        Referensi = Nothing

        txt_NomorBP.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        lbl_BiayaAdministrasiBank.Visible = False
        txt_BiayaAdministrasiBank.Visible = False
        dtp_TanggalBayar.Enabled = True

        txt_NomorBP.Text = Kosongan
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_BiayaAdministrasiBank.Text = Kosongan
        DitanggungOleh = Kosongan
        txt_Pokok.Text = Kosongan
        txt_BagiHasil.Text = Kosongan
        txt_Denda.Text = Kosongan
        JumlahPPh = 0
        PPhDitanggung = 0
        txt_PPhDipotong.Text = Kosongan
        txt_JumlahDibayarkan.Text = Kosongan

        dtp_TanggalBayar.Value = Today
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub

    Private Sub txt_NomorBP_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBP.TextChanged
        NomorBP = txt_NomorBP.Text
    End Sub
    Private Sub txt_NomorBP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Pokok_TextChanged(sender As Object, e As EventArgs) Handles txt_Pokok.TextChanged
        Pokok = AmbilAngka(txt_Pokok.Text)
        Perhitungan_JumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_Pokok)
    End Sub
    Private Sub txt_Pokok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Pokok.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_BagiHasil_TextChanged(sender As Object, e As EventArgs) Handles txt_BagiHasil.TextChanged
        BagiHasil = AmbilAngka(txt_BagiHasil.Text)
        Perhitungan_JumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_BagiHasil)
    End Sub
    Private Sub txt_BagiHasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BagiHasil.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung)
    End Sub
    Private Sub txt_PPhDitanggung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        Perhitungan_JumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_PPhDipotong)
    End Sub
    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDipotong.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Denda_TextChanged(sender As Object, e As EventArgs) Handles txt_Denda.TextChanged
        Denda = AmbilAngka(txt_Denda.Text)
        Perhitungan_JumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_Denda)
    End Sub
    Private Sub txt_Denda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Denda.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_JumlahDibayarkan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDibayarkan.TextChanged
        JumlahDibayarkan = AmbilAngka(txt_JumlahDibayarkan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahDibayarkan)
    End Sub
    Private Sub txt_JumlahDibayarkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDibayarkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub Perhitungan_JumlahDibayarkan()
        txt_JumlahDibayarkan.Text = Pokok + BagiHasil - PPhDipotong + Denda
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
        JenisJurnal = KonversiSaranaPembayaranKeJenisJurnal(SaranaPembayaran)
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
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If SaranaPembayaran = Kosongan Then
            MsgBox("Silakan pilih 'Sarana Pembayaran'")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        frm_InputTransaksi.ResetForm()
        ProsesIsiValueForm = True
        frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGPIHAKKETIGA
        frm_InputTransaksi.FungsiForm = FungsiForm
        frm_InputTransaksi.JenisPPh = JenisPPh
        frm_InputTransaksi.KodeSetoran = KodeSetoran
        If HutangPiutang = hp_Hutang Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_OUT
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranHutangPihakKetiga
            frm_InputTransaksi.NomorBPHPK = NomorBP
            If JenisPPh <> Kosongan And JenisPPh <> JenisPPh_NonPPh Then
                frm_InputTransaksi.COAHutangPPh = PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran)
                frm_InputTransaksi.COABiayaPPh = PenentuanCOA_BiayaPPh(JenisPPh)
            End If
        End If
        If HutangPiutang = hp_Piutang Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_IN
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PencairanPiutangPihakKetiga
            frm_InputTransaksi.NomorBPPPK = NomorBP
            If JenisPPh <> Kosongan And JenisPPh <> JenisPPh_NonPPh Then
                frm_InputTransaksi.COAHutangPPh = PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh)
                frm_InputTransaksi.COAPenghasilan = KodeTautanCOA_PenjualanLainnya
            End If
            If BiayaAdministrasiBank > 0 Then
                frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
                'Maksudnya, setiap ada Biaya Administrasi dalam pencairan, maka sudah pasti itu ditanggung oleh Perusahaan.
                'Kalau ditanggung oleh lawan transaksi, maka tidak perlu ada pencatatan Biaya Administrasi Bank,
                'Dalam arti, ya dikosongkan saja, mau berapa pun Biaya Administrasinya.
            End If
        End If
        If FungsiForm = FungsiForm_EDIT Then jur_NomorJV = NomorJVBayar
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangTahunIni
        frm_InputTransaksi.txt_Referensi.Text = NomorBP 'Sementara...!
        frm_InputTransaksi.NomorPembelian = Kosongan
        frm_InputTransaksi.txt_TanggalInvoice.Text = TanggalPencairan
        frm_InputTransaksi.txt_NomorInvoice.Text = NomorKontrak
        frm_InputTransaksi.txt_NomorFakturPajak.Text = Kosongan
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeKreditur
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaKreditur
        frm_InputTransaksi.AngsuranKe = AngsuranKe
        frm_InputTransaksi.JumlahBarisAngsuranPihakKetiga = JumlahBarisAngsuran
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahDibayarkan
        frm_InputTransaksi.JumlahMutasiBankCash = JumlahDibayarkan
        frm_InputTransaksi.JumlahPokokAngsuran = Pokok
        frm_InputTransaksi.BungaBagiHasilAngsuran = BagiHasil
        frm_InputTransaksi.DendaAngsuran = Denda
        frm_InputTransaksi.JumlahPPhTerutang = JumlahPPh
        frm_InputTransaksi.JumlahPPhDitanggung = PPhDitanggung
        'Jumlah PPh Dipotong jangan dimasukkan...!!!
        If COASaranaPembayaran >= KodeAkun_Bank_Awal And COASaranaPembayaran <= kodeakun_Bank_Akhir Then
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh
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