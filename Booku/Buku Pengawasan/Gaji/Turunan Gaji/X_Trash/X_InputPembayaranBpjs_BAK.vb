Imports bcomm

Public Class X_InputPembayaranBpjs_BAK

    Public JudulForm
    Public JalurMasuk
    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean

    Public Referensi
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

        'If FungsiForm = FungsiForm_TAMBAH Then
        '    If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then _
        '        Me.Text = "Input Pembayaran BPJS Kesehatan"
        '    If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then _
        '        Me.Text = "Input Pembayaran BPJS Ketenagakerjaan"
        '    btn_Lanjutkan.Text = tombol_LANJUTKAN_INPUT
        '    AksesDatabase_Transaksi(Buka)
        '    If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then _
        '        cmd = New OdbcCommand("SELECT * FROM tbl_PembayaranBpjsKesehatan WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_PembayaranBpjsKesehatan) ", KoneksiDatabaseTransaksi)
        '    If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then _
        '        cmd = New OdbcCommand("SELECT * FROM tbl_PembayaranBpjsKetenagakerjaan WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_PembayaranBpjsKetenagakerjaan) ", KoneksiDatabaseTransaksi)
        '    dr = cmd.ExecuteReader
        '    dr.Read()
        '    If Not dr.HasRows Then
        '        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then _
        '            Referensi = AwalanNPPHKS_PlusTahunBuku & "1"
        '        If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then _
        '            Referensi = AwalanNPPHTK_PlusTahunBuku & "1"
        '    Else
        '        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then _
        '            Referensi = AwalanNPPHKS_PlusTahunBuku & (dr.Item("Nomor_ID") + 1).ToString
        '        If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then _
        '            Referensi = AwalanNPPHTK_PlusTahunBuku & (dr.Item("Nomor_ID") + 1).ToString
        '    End If
        '    AksesDatabase_Transaksi(Tutup)
        'End If

        'If FungsiForm = FungsiForm_EDIT Then
        '    If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then Me.Text = "Edit Pembayaran BPJS Kesehatan"
        '    If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then Me.Text = "Edit Pembayaran BPJS Ketenagakerjaan"
        '    btn_Lanjutkan.Text = tombol_LANJUTKAN_EDIT
        '    AksesDatabase_Transaksi(Buka)
        '    If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then _
        '        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranBpjsKesehatan WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
        '    If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then _
        '        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranBpjsKetenagakerjaan WHERE Nomor_ID = '" & NomorIdBayar & "' ", KoneksiDatabaseTransaksi)
        '    dr = cmd.ExecuteReader
        '    dr.Read()
        '    COAKredit = dr.Item("COA_Kredit")
        '    BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
        '    DitanggungOleh = dr.Item("Ditanggung_Oleh")
        '    AksesDatabase_Transaksi(Tutup)
        '    AksesDatabase_General(Buka)
        '    cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COAKredit & "' ", KoneksiDatabaseGeneral)
        '    dr = cmd.ExecuteReader
        '    dr.Read()
        '    cmb_SaranaPembayaran.Text = dr.Item("COA") & StripPemisah & dr.Item("Nama_Akun")
        '    AksesDatabase_General(Tutup)
        'End If

    End Sub

    Public Sub ResetForm()

        NomorIdBayar = 0
        Referensi = Nothing

        txt_Bulan.Enabled = False
        cmb_SaranaPembayaran.Enabled = True
        txt_JumlahTagihan.Enabled = False
        txt_JumlahDibayar.Enabled = False
        txt_SisaPembayaran.Enabled = False
        dtp_TanggalBayar.Enabled = True
        txt_JumlahBayar.Enabled = True
        txt_Keterangan.Enabled = True

        txt_Bulan.Text = Nothing
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        txt_JumlahTagihan.Text = Nothing
        txt_JumlahDibayar.Text = Nothing
        txt_SisaPembayaran.Text = Nothing
        dtp_TanggalBayar.Value = Today
        txt_JumlahBayar.Text = Nothing
        txt_Keterangan.Text = ""

    End Sub

    Private Sub txt_Bulan_TextChanged(sender As Object, e As EventArgs) Handles txt_Bulan.TextChanged
        Bulan = txt_Bulan.Text
    End Sub

    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTagihan.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahTagihan)
    End Sub
    Private Sub txt_JumlahTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTagihan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDibayar.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahDibayar)
    End Sub
    Private Sub txt_JumlahDibayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDibayar.KeyPress
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
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        PemecahRibuanUntukTextBox(txt_JumlahBayar)
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

        frm_InputTransaksi.ResetForm()
        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKESEHATAN
        'If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then frm_InputTransaksi.JalurMasuk = Halaman_INPUTPEMBAYARANBPJSKETENAGAKERJAAN
        frm_InputTransaksi.FungsiForm = FungsiForm
        If FungsiForm = FungsiForm_EDIT Then jur_NomorJV = NomorJVBayar
        frm_InputTransaksi.TermasukHutangTahunBukuAktif = TermasukHutangTahunIni
        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranBpjsKesehatan
        'If JalurMasuk = Halaman_BUKUPengawasanHutangBPJSKETENAGAKERJAAN Then frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_PembayaranBpjsKetenagakerjaan
        frm_InputTransaksi.txt_Referensi.Text = Referensi
        frm_InputTransaksi.BulanPembayaran = Bulan
        frm_InputTransaksi.NomorPembelian = Nothing
        frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        frm_InputTransaksi.txt_TanggalInvoice.Text = Nothing
        frm_InputTransaksi.txt_NomorInvoice.Text = Nothing
        frm_InputTransaksi.txt_NomorFakturPajak.Text = Nothing
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_BPJS_KS
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_BpjsKesehatan
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