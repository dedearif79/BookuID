Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPembayaranHutangPiutangKaryawan

    Public FungsiForm
    Public JudulForm
    Public ProsesSuntingDatabase As Boolean

    Public Referensi
    Public NomorBP
    Public NomorIdBayar As Integer
    Public TanggalInvoice
    Public NomorIDKaryawan
    Public NamaKaryawan
    Public JumlahTerutang
    Public Jumlah_Dibayar
    Public JumlahBayar
    Public SisaHutang
    Public TanggalBayar
    Public SaranaPembayaran
    Public Keterangan
    Public NomorJVBayar
    Public TermasukHutangPiutangTahunIni As Boolean

    Dim COA_SaranaPembayaran
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh As String

    Dim JenisJurnal

    Public HutangPiutang
    Dim TabelPengawasan
    Dim TabelAngsuran
    Dim KolomNomorBP
    Dim KolomCOASaranaPembayaran

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If HutangPiutang = Kosongan Then PesanUntukProgrammer("Tentukan dulu, Hutang atau Piutang...!!!!")

        If HutangPiutang = hp_Hutang Then
            TabelPengawasan = "tbl_PengawasanHutangKaryawan"
            TabelAngsuran = "tbl_PembayaranHutangKaryawan"
            KolomNomorBP = "Nomor_BPHK"
            KolomCOASaranaPembayaran = "COA_Kredit"
            lbl_NomorBP.Text = "Nomor BPHK"
            lbl_JumlahBayar.Text = "Jumlah Bayar"
        End If

        If HutangPiutang = hp_Piutang Then
            TabelPengawasan = "tbl_PengawasanPiutangKaryawan"
            TabelAngsuran = "tbl_PencairanPiutangKaryawan"
            KolomNomorBP = "Nomor_BPPK"
            KolomCOASaranaPembayaran = "COA_Debet"
            lbl_NomorBP.Text = "Nomor BPPK"
            lbl_JumlahBayar.Text = "Jumlah Cair"
        End If

        SisaHutang = txt_SaldoAkhir.Text

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input "
            btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit "
            btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                          " WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            COA_SaranaPembayaran = dr.Item(KolomCOASaranaPembayaran)
            DitanggungOleh = dr.Item("Ditanggung_Oleh")
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank") 'Baris ini harus di posisi persis sebelum AksesDatabase_Transaksi(Tutup)
            AksesDatabase_Transaksi(Tutup)
            cmb_SaranaPembayaran.Text = KonversiCOAKeSaranaPembayaran(COA_SaranaPembayaran)
        End If

        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        HutangPiutang = Kosongan

        frm_InputTransaksi.PenyimpananSukses = False

        NomorIdBayar = 0
        Referensi = Nothing

        txt_NomorBP.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        lbl_BiayaAdministrasiBank.Visible = False
        txt_BiayaAdministrasiBank.Visible = False
        txt_SaldoAwal.Enabled = False
        txt_JumlahDibayar.Enabled = False
        txt_SaldoAkhir.Enabled = False
        dtp_TanggalBayar.Enabled = True
        txt_JumlahBayar.Enabled = True
        txt_Keterangan.Enabled = True

        txt_NomorBP.Text = Nothing
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_BiayaAdministrasiBank.Text = Kosongan
        DitanggungOleh = Kosongan
        txt_SaldoAwal.Text = Nothing
        txt_JumlahDibayar.Text = Nothing
        txt_SaldoAkhir.Text = Nothing
        dtp_TanggalBayar.Value = Today
        txt_JumlahBayar.Text = Nothing
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub

    Sub PerhitunganRincianPembayaran()

    End Sub

    Private Sub txt_NomorBP_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBP.TextChanged
        Referensi = txt_NomorBP.Text
        NomorBP = txt_NomorBP.Text
    End Sub
    Private Sub txt_NomorBP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwal.TextChanged
        JumlahTerutang = AmbilAngka(txt_SaldoAwal.Text)
        PemecahRibuanUntukTextBox(txt_SaldoAwal)
    End Sub
    Private Sub txt_SaldoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDibayar.TextChanged
        Jumlah_Dibayar = AmbilAngka(txt_JumlahDibayar.Text)
        PemecahRibuanUntukTextBox(txt_JumlahDibayar)
    End Sub
    Private Sub txt_JumlahDibayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDibayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAkhir_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAkhir.TextChanged
        SisaHutang = AmbilAngka(txt_SaldoAkhir.Text)
        PemecahRibuanUntukTextBox(txt_SaldoAkhir)
    End Sub
    Private Sub txt_SaldoAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAkhir.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBayar.TextChanged
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        PemecahRibuanUntukTextBox(txt_JumlahBayar)
        If JumlahBayar > SisaHutang And FungsiForm = FungsiForm_TAMBAH Then
            MsgBox("Nominal Jumlah Bayar melebihi Sisa Hutang." & Enter2Baris &
                   "Silakan isi kolom 'Jumlah Bayar' dengan benar.")
            txt_JumlahBayar.Text = Nothing
            txt_JumlahBayar.Focus()
        End If
        If ProsesLoadingForm = False And ProsesResetForm = False Then PerhitunganRincianPembayaran()
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
        JenisJurnal = Microsoft.VisualBasic.Mid(SaranaPembayaran, JumlahDigitCOA + Microsoft.VisualBasic.Len(StripPemisah) + 1)
        COA_SaranaPembayaran = Microsoft.VisualBasic.Left(SaranaPembayaran, JumlahDigitCOA)
        If AmbilAngka(COA_SaranaPembayaran) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COA_SaranaPembayaran) <= kodeakun_Bank_Akhir _
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

        If JumlahBayar = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Bayar'")
            txt_JumlahBayar.Focus()
            Return
        End If

        If SaranaPembayaran = Nothing Then
            MsgBox("Silakan pilih 'Sarana Pembayaran'")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        frm_InputTransaksi.ResetForm()
        ProsesIsiValueForm = True
        frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANHUTANGPIUTANGKARYAWAN
        frm_InputTransaksi.FungsiForm = FungsiForm
        If HutangPiutang = hp_Hutang Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_OUT
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranHutangKaryawan
            frm_InputTransaksi.NomorBPHK = NomorBP
        End If
        If HutangPiutang = hp_Piutang Then
            frm_InputTransaksi.AlurTransaksi = AlurTransaksi_IN
            frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PencairanPiutangKaryawan
            frm_InputTransaksi.NomorBPPK = NomorBP
            If BiayaAdministrasiBank > 0 Then
                frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
                'Maksudnya, setiap ada Biaya Administrasi dalam pencairan, maka sudah pasti itu ditanggung oleh Perusahaan.
                'Kalau ditanggung oleh lawan transaksi, maka tidak perlu ada pencatatan Biaya Administrasi Bank,
                'Dalam arti, ya dikosongkan saja, mau berapa pun Biaya Administrasinya.
            End If
        End If
        If FungsiForm = FungsiForm_EDIT Then
            frm_InputTransaksi.NomorID = NomorIdBayar
            jur_NomorJV = NomorJVBayar
        End If
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangPiutangTahunIni
        frm_InputTransaksi.txt_Referensi.Text = Referensi
        frm_InputTransaksi.txt_TanggalInvoice.Text = TanggalInvoice
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = NomorIDKaryawan
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaKaryawan
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahBayar
        If COA_SaranaPembayaran >= KodeAkun_Bank_Awal And COA_SaranaPembayaran <= kodeakun_Bank_Akhir Then
            frm_InputTransaksi.txt_BiayaAdministrasiBank.Text = BiayaAdministrasiBank
            frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh
            If HutangPiutang = hp_Piutang Then
                If BiayaAdministrasiBank > 0 Then frm_InputTransaksi.cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
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