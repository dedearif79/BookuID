Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembayaranGaji

    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean

    Public NPPG
    Public Bulan
    Public NomorIdBayar As Integer
    Public SaranaPembayaran
    Public SisaHutang As Int64
    Public TanggalBayar
    Public JumlahBayar As Int64
    Public Keterangan
    Public NomorJVBayar
    Public TermasukHutangTahunIni As Boolean

    Dim COAKredit
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh

    Dim JenisJurnal

    Private Sub frm_InputPembayaranGaji_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SisaHutang = txt_SisaPembayaran.Text

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input Pembayaran Gaji"
            btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand("SELECT * FROM tbl_PembayaranGaji WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_PembayaranGaji) ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                NPPG = AwalanNPPG_PlusTahunBuku & "1"
            Else
                NPPG = AwalanNPPG_PlusTahunBuku & (dr.Item("Nomor_ID") + 1).ToString
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Pembayaran Gaji"
            btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranGaji WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
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
            cmb_SaranaPembayaran.Text = dr.Item("COA") & StripPemisah & dr.Item("Nama_Akun")
            AksesDatabase_General(Tutup)
        End If

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        NomorIdBayar = 0
        NPPG = Nothing

        txt_Bulan.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        txt_JumlahGaji.Enabled = False
        txt_JumlahGajiDibayar.Enabled = False
        txt_SisaPembayaran.Enabled = False
        dtp_TanggalBayar.Enabled = True
        txt_JumlahBayar.Enabled = True
        txt_Keterangan.Enabled = True

        txt_Bulan.Text = Nothing
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_JumlahGaji.Text = Nothing
        txt_JumlahGajiDibayar.Text = Nothing
        txt_SisaPembayaran.Text = Nothing
        dtp_TanggalBayar.Value = Today
        txt_JumlahBayar.Text = Nothing
        txt_Keterangan.Text = ""

        ProsesResetForm = False

    End Sub

    Private Sub txt_Bulan_TextChanged(sender As Object, e As EventArgs) Handles txt_Bulan.TextChanged
        Bulan = txt_Bulan.Text
    End Sub

    Private Sub txt_JumlahGaji_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahGaji.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahGaji)
    End Sub
    Private Sub txt_JumlahGaji_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahGaji.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahGajiDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahGajiDibayar.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahGajiDibayar)
    End Sub
    Private Sub txt_JumlahGajiDibayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahGajiDibayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SisaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaPembayaran.TextChanged
        PemecahRibuanUntukTextBox(txt_SisaPembayaran)
        SisaHutang = AmbilAngka(txt_SisaPembayaran.Text)
    End Sub
    Private Sub txt_SisaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SisaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBayar.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahBayar)
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        If JumlahBayar > SisaHutang And FungsiForm = FungsiForm_TAMBAH Then
            MsgBox("Nominal Jumlah Bayar melebihi Sisa Hutang." & Enter2Baris & _
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

        frm_InputTransaksi.ResetForm()
        frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANGAJI
        frm_InputTransaksi.FungsiForm = FungsiForm
        If FungsiForm = FungsiForm_EDIT Then jur_NomorJV = NomorJVBayar
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangTahunIni
        frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranGaji
        frm_InputTransaksi.txt_Referensi.Text = NPPG
        frm_InputTransaksi.BulanPembayaran = Bulan
        frm_InputTransaksi.NomorPembelian = Nothing
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_TanggalInvoice.Text = Nothing
        frm_InputTransaksi.txt_NomorInvoice.Text = Nothing
        frm_InputTransaksi.txt_NomorFakturPajak.Text = Nothing
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Karyawan
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Karyawan
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahBayar
        If COAKredit >= KodeAkun_Bank_Awal And COAKredit <= kodeakun_Bank_Akhir Then
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh
            frm_InputTransaksi.Perhitungan_ValueBank()
        Else
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = Nothing
            frm_InputTransaksi.cmb_DitanggungOleh.Text = Nothing
        End If
        frm_InputTransaksi.dtp_TanggalTransaksi.Value = TanggalBayar
        frm_InputTransaksi.txt_UraianTransaksi.Text = Keterangan
        frm_InputTransaksi.cmb_JenisJurnal.Text = JenisJurnal
        frm_InputTransaksi.cmb_JenisJurnal.Enabled = False

        frm_InputTransaksi.ShowDialog()
        If frm_InputTransaksi.PenyimpananSukses = True Then Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class