Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembayaranHutangPajak

    Public FungsiForm
    Public JudulForm
    Dim ProsesSuntingDatabase As Boolean

    Public NPPHP
    Public NomorBPHP
    Public Bulan
    Public NomorIdBayar As Integer
    Public SaranaPembayaran
    Public SisaHutang As Int64
    Public TanggalBayar
    Public JumlahBayar As Int64
    Public Keterangan
    Public NomorJVBayar
    Public TermasukHutangTahunIni As Boolean
    Public JenisPajak
    Public KodeSetoran
    Dim JalurMasukTransaksi

    Dim COAKredit
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh

    Dim JenisJurnal

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SisaHutang = txt_SisaHutang.Text

        JudulForm = "Pembayaran Hutang "

        Select Case JenisPajak
            Case JenisPajak_PPhPasal21
                JudulForm &= JenisPajak_PPhPasal21
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL21
            Case JenisPajak_PPhPasal23
                JudulForm &= JenisPajak_PPhPasal23
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL23
            Case JenisPajak_PPhPasal25
                JudulForm &= JenisPajak_PPhPasal25
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL25
            Case JenisPajak_PPhPasal26
                JudulForm &= JenisPajak_PPhPasal26
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL26
            Case JenisPajak_PPhPasal29
                JudulForm &= JenisPajak_PPhPasal29
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL29
            Case JenisPajak_PPhPasal42
                JudulForm &= JenisPajak_PPhPasal42
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPHPASAL42
            Case JenisPajak_PPN
                JudulForm &= JenisPajak_PPN
                JalurMasukTransaksi = Halaman_INPUTPEMBAYARANHUTANGPPN
        End Select

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input " & JudulForm
            btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Angka_NPPHP FROM tbl_PengajuanPembayaranHutangPajak " &
                                  " WHERE Angka_NPPHP IN (SELECT MAX(Angka_NPPHP) FROM tbl_PengajuanPembayaranHutangPajak) ",
                                  KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                NPPHP = AwalanNPPHP_PlusTahunBuku & "1"
            Else
                NPPHP = AwalanNPPHP_PlusTahunBuku & (dr.Item("Angka_NPPHP") + 1).ToString
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit " & JudulForm
            btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            COAKredit = dr.Item("COA_Kredit")
            BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
            DitanggungOleh = dr.Item("Ditanggung_Oleh")
            AksesDatabase_Transaksi(Tutup)
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COAKredit & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                cmb_SaranaPembayaran.Text = dr.Item("COA") & StripPemisah & dr.Item("Nama_Akun")
            Else
                PesanUntukProgrammer("COA tidak terdaftar..!")
            End If
            AksesDatabase_General(Tutup)
        End If

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        frm_InputTransaksi.PenyimpananSukses = False

        NomorIdBayar = 0
        NPPHP = Kosongan

        txt_MasaPajak.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        txt_JumlahTerutang.Enabled = False
        txt_JumlahDibayar.Enabled = False
        txt_SisaHutang.Enabled = False
        dtp_TanggalBayar.Enabled = True
        txt_JumlahBayar.Enabled = True
        txt_Keterangan.Enabled = True

        txt_MasaPajak.Text = Kosongan
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_JumlahTerutang.Text = Kosongan
        txt_JumlahDibayar.Text = Kosongan
        txt_SisaHutang.Text = Kosongan
        dtp_TanggalBayar.Value = Today
        txt_JumlahBayar.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        JenisPajak = Kosongan
        KodeSetoran = Kosongan

        ProsesResetForm = False

    End Sub

    Private Sub txt_NomorBPHP_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPHP.TextChanged
        NomorBPHP = txt_NomorBPHP.Text
    End Sub

    Private Sub txt_MasaPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_MasaPajak.TextChanged
        Bulan = txt_MasaPajak.Text
    End Sub

    Private Sub txt_JumlahTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTerutang.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahTerutang)
    End Sub
    Private Sub txt_JumlahTerutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTerutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDibayar.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahDibayar)
    End Sub
    Private Sub txt_JumlahDibayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDibayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SisaHutang_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaHutang.TextChanged
        PemecahRibuanUntukTextBox(txt_SisaHutang)
        SisaHutang = AmbilAngka(txt_SisaHutang.Text)
    End Sub
    Private Sub txt_SisaHutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SisaHutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBayar.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahBayar)
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        If JumlahBayar > SisaHutang And FungsiForm = FungsiForm_TAMBAH Then
            MsgBox("Nominal Jumlah Bayar melebihi Sisa Hutang." & Enter2Baris &
                   "Silakan isi kolom 'Jumlah Bayar' dengan benar.")
            txt_JumlahBayar.Text = Nothing
            txt_JumlahBayar.Focus()
        End If
    End Sub
    Private Sub txt_JumlahBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahBayar.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
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
        COAKredit = Microsoft.VisualBasic.Left(SaranaPembayaran, JumlahDigitCOA)
        JenisJurnal = Microsoft.VisualBasic.Mid(SaranaPembayaran, JumlahDigitCOA + Microsoft.VisualBasic.Len(StripPemisah) + 1)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If JumlahBayar = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Bayar'")
            txt_JumlahBayar.Focus()
            Return
        End If

        If cmb_SaranaPembayaran.Text = Nothing Then
            MsgBox("Silakan pilih 'Sarana Pembayaran'")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        If JenisPajak = Kosongan Then
            PesanUntukProgrammer("'Jenis Pajak' belum ditentukan...!!!")
            Return
        End If

        'If KodeSetoran = Kosongan Then
        '    PesanUntukProgrammer("'Kode Setoran' belum ditentukan...!!!")
        '    Return
        'End If

        frm_InputTransaksi.ResetForm()
        frm_InputTransaksi.JalurMasuk = JalurMasukTransaksi
        frm_InputTransaksi.FungsiForm = FungsiForm
        If FungsiForm = FungsiForm_EDIT Then jur_NomorJV = NomorJVBayar
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangTahunIni
        frm_InputTransaksi.JenisPajak = JenisPajak
        frm_InputTransaksi.KodeSetoran = KodeSetoran
        frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranHutangPajak
        frm_InputTransaksi.txt_Referensi.Text = NPPHP
        frm_InputTransaksi.NomorBPHP = NomorBPHP
        frm_InputTransaksi.MasaPajak = Bulan
        frm_InputTransaksi.NomorPembelian = Nothing
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_TanggalInvoice.Text = Nothing
        frm_InputTransaksi.txt_NomorInvoice.Text = Nothing
        If btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT Then frm_InputTransaksi.txt_NomorInvoice.Text = Nothing '(NTPN)
        If btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT NTPN FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Nomor_ID = '" & NomorIdBayar & "' ",
                                  KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            frm_InputTransaksi.txt_NomorInvoice.Text = dr.Item("NTPN") '(Untuk mengisi value NTPN)
            AksesDatabase_Transaksi(Tutup)
        End If
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_DJP
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahBayar
        frm_InputTransaksi.JumlahMutasiBankCash = JumlahBayar
        If COAKredit >= KodeAkun_Bank_Awal And COAKredit <= kodeakun_Bank_Akhir Then
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh
            frm_InputTransaksi.Perhitungan_ValueBank()
        Else
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = Nothing
            frm_InputTransaksi.cmb_DitanggungOleh.Text = Nothing
        End If
        frm_InputTransaksi.dtp_TanggalTransaksi.Value = TanggalBayar
        frm_InputTransaksi.txt_UraianTransaksi.Text = txt_Keterangan.Text '(Ini value-nya ngambil langsung dari TextBox. Entah kenapa ketika melalui variabel 'Keterangan' ga bisa..?)
        frm_InputTransaksi.cmb_JenisJurnal.Text = JenisJurnal
        frm_InputTransaksi.cmb_JenisJurnal.Enabled = False

        frm_InputTransaksi.ShowDialog()
        If frm_InputTransaksi.PenyimpananSukses = True Then Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class