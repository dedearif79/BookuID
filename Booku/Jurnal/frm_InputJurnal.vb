Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputJurnal

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public AngkaNomorJV
    Dim BarisTerseleksi
    Public JumlahBarisJurnal
    Dim JumlahDebet As Int64
    Dim JumlahKredit As Int64
    Public TotalDebet As Int64
    Public TotalKredit As Int64
    Dim StatusBalance = ""
    Dim COA
    Dim DK = Nothing
    Dim TahunJurnal
    Public JurnalTersimpan As Boolean 'Ini Penting...!!! Ada kaitannya dengan form lain.

    Dim TanggalJurnal
    Dim JenisJurnal
    Dim KodeDokumen
    Dim Bundelan
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTannsaksi
    Dim UraianTransaksi

    Public BisaDiedit As Boolean

    Private Sub frm_InputJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                JudulForm = "Input Jurnal"
                SistemPenomoranOtomatis_NomorJV()
                AngkaNomorJV = jur_NomorJV
                btn_Reset.Enabled = True
            Case FungsiForm_EDIT
                JudulForm = "Edit Jurnal"
                btn_Reset.Enabled = False
                IsiValue_UntukEdit()
                jur_NomorJV = AngkaNomorJV
        End Select

        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then
            txt_TanggalInvoice.Enabled = False
        Else
            txt_TanggalInvoice.Enabled = True
        End If

        If JalurMasuk = Menu_JurnalAdjusment Then
            dtp_TanggalJurnal.Enabled = False
            cmb_JenisJurnal.Enabled = False
            txt_KodeDokumen.Enabled = False
            txt_Bundelan.Enabled = False
            txt_TanggalInvoice.Enabled = False
            txt_NomorInvoice.Enabled = False
            txt_NomorFakturPajak.Enabled = False
            txt_KodeLawanTransaksi.Enabled = False
            btn_PilihMitra.Enabled = False
            txt_NamaLawanTransaksi.Enabled = False
            btn_TambahTransaksi.Enabled = False
            btn_Reset.Enabled = False
            BisaDiedit = False
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
        lbl_NomorJV.Text = "Nomor : " & AwalanNomorJV & AngkaNomorJV

        dtp_TanggalJurnal.Focus()

        BeginInvoke(Sub() BersihkanSeleksi())

        JurnalTersimpan = False 'Default False ini harus dieksekusi saat loading, untuk jaga-jaga. Jangan dihapus...!!!

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        JurnalTersimpan = False 'Default False ini juga harus dieksekusi saat reset form, untuk jaga-jaga. Jangan dihapus...!!!

        BisaDiedit = True

        JalurMasuk = Nothing

        cmb_JenisJurnal.Enabled = True
        txt_KodeDokumen.Enabled = True
        txt_Bundelan.Enabled = True
        txt_TanggalInvoice.Enabled = True
        txt_NomorInvoice.Enabled = True
        txt_NomorFakturPajak.Enabled = True
        txt_KodeLawanTransaksi.Enabled = True
        btn_PilihMitra.Enabled = True
        txt_NamaLawanTransaksi.Enabled = True
        txt_UraianTransaksi.Enabled = True
        btn_TambahTransaksi.Enabled = True
        btn_Edit.Enabled = True
        btn_Hapus.Enabled = True
        btn_Simpan.Enabled = False
        btn_Reset.Enabled = True

        dtp_TanggalJurnal.Value = Today
        KontenComboJenisJurnal_Public(cmb_JenisJurnal)
        txt_KodeDokumen.Text = Nothing
        txt_Bundelan.Text = Nothing
        txt_TanggalInvoice.Text = Nothing
        txt_NomorInvoice.Text = Nothing
        txt_NomorFakturPajak.Text = Nothing
        txt_NamaLawanTransaksi.Text = Nothing
        txt_UraianTransaksi.Text = Nothing

        DataTabelUtama.Rows.Clear()
        BersihkanSeleksi()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        JumlahBarisJurnal = 0
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!

        TampilkanData()

        ProsesResetForm = False

    End Sub

    Sub TampilkanData()

        Dim Baris = 0
        TotalDebet = 0
        TotalKredit = 0
        Do While Baris < JumlahBarisJurnal
            JumlahDebet = AmbilAngka(DataTabelUtama.Item("Debet_", Baris).Value)
            JumlahKredit = AmbilAngka(DataTabelUtama.Item("Kredit_", Baris).Value)
            TotalDebet += JumlahDebet
            TotalKredit += JumlahKredit
            Baris += 1
        Loop
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        BersihkanSeleksi()

        NotifBalance()

    End Sub

    Sub IsiValue_UntukEdit()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Dim NomorUrut = 0
        Dim COA
        Dim NamaAkun
        Dim DK
        Dim JumlahDebet
        Dim JumlahKredit
        TotalDebet = 0
        TotalKredit = 0
        DataTabelUtama.Rows.Clear()
        Do While dr.Read
            dtp_TanggalJurnal.Value = dr.Item("Tanggal_Transaksi")
            cmb_JenisJurnal.Text = dr.Item("Jenis_Jurnal")
            txt_KodeDokumen.Text = dr.Item("Referensi")
            txt_Bundelan.Text = dr.Item("Bundelan")
            txt_TanggalInvoice.Text = dr.Item("Tanggal_Invoice")
            txt_NomorInvoice.Text = dr.Item("Nomor_Invoice")
            txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
            txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
            txt_UraianTransaksi.Text = dr.Item("Uraian_Transaksi")
            NomorUrut = NomorUrut + 1
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            DK = dr.Item("D_K")
            If DK = "K" Then NamaAkun = PenjorokNamaAkun + NamaAkun
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            TotalDebet = TotalDebet + JumlahDebet
            TotalKredit = TotalKredit + JumlahKredit
            If JumlahDebet = 0 Then JumlahDebet = StripKosong
            If JumlahKredit = 0 Then JumlahKredit = StripKosong
            DataTabelUtama.Rows.Add(NomorUrut, COA, NamaAkun, DK, JumlahDebet, JumlahKredit)
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        JumlahBarisJurnal = NomorUrut
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        NotifBalance()
    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        BarisTerseleksi = -1
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Sub NotifBalance()

        If TotalDebet = TotalKredit Then
            StatusBalance = "Tidak Ada Selisih"
            lbl_StatusBalance.ForeColor = Color.Green
        Else
            StatusBalance = "Ada Selisih"
            lbl_StatusBalance.ForeColor = Color.Red
        End If

        lbl_StatusBalance.Text = StatusBalance

        Dim clm_TotalDebet
        If TotalDebet = 0 Then
            clm_TotalDebet = StripKosong
        Else
            clm_TotalDebet = TotalDebet
        End If

        Dim clm_TotalKredit
        If TotalKredit = 0 Then
            clm_TotalKredit = StripKosong
        Else
            clm_TotalKredit = TotalKredit
        End If

        DataTabelUtama.Item("Nama_Akun", JumlahBarisJurnal + 1).Value = "J  U  M  L  A  H"
        DataTabelUtama.Item("Debet_", JumlahBarisJurnal + 1).Value = clm_TotalDebet
        DataTabelUtama.Item("Kredit_", JumlahBarisJurnal + 1).Value = clm_TotalKredit

        If TotalKredit = 0 And TotalDebet > 0 Then
            lbl_StatusBalance.Text = "Tidak ada Kredit"
        Else
        End If
        If TotalDebet = 0 And TotalKredit > 0 Then
            lbl_StatusBalance.Text = "Tidak ada Debet"
        End If

        If JumlahBarisJurnal = 0 Then
            lbl_StatusBalance.ForeColor = Color.Red
            lbl_StatusBalance.Text = "Tidak ada Transaksi"
        End If

        LogikaTombolSimpan()

    End Sub

    Sub LogikaTombolSimpan()
        If lbl_StatusBalance.Text = "Tidak Ada Selisih" And JenisJurnal <> Nothing Then
            btn_Simpan.Enabled = True
        Else
            btn_Simpan.Enabled = False
        End If
    End Sub

    Private Sub dtp_TanggalJurnal_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJurnal.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalJurnal)
        TanggalJurnal = Microsoft.VisualBasic.Left(dtp_TanggalJurnal.Text, 10)
        TahunJurnal = dtp_TanggalJurnal.Value.Year
    End Sub
    Private Sub dtp_TanggalJurnal_Leave(sender As Object, e As EventArgs) Handles dtp_TanggalJurnal.Leave
        If TahunJurnal <> TahunBukuAktif Then
            MsgBox("Silakan isi kolom 'Tanggal Bayar' dengan benar." & Enter2Baris & "Sesuaikan tahunnya dengan Tahun Buku Aktif.")
            dtp_TanggalJurnal.Focus()
        End If
        If dtp_TanggalJurnal.Value > Today Then
            MsgBox("Tanggal Bayar yang Anda input melebihi hari ini." & Enter2Baris & "Silakan isi kolom 'Tanggal Bayar' dengan benar.")
            dtp_TanggalJurnal.Focus()
            Return
        End If
    End Sub

    Private Sub cmb_JenisJurnal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJurnal_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.TextChanged
        JenisJurnal = cmb_JenisJurnal.Text
        LogikaTombolSimpan()
    End Sub
    Private Sub cmb_JenisJurnal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJurnal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_KodeDokumen_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeDokumen.TextChanged
        KodeDokumen = txt_KodeDokumen.Text
    End Sub

    Private Sub txt_Bundelan_TextChanged(sender As Object, e As EventArgs) Handles txt_Bundelan.TextChanged
        Bundelan = txt_Bundelan.Text
    End Sub

    Private Sub txt_TanggalInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_TanggalInvoice.TextChanged
        TanggalInvoice = txt_TanggalInvoice.Text
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
    End Sub
    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click

        frm_ListLawanTransaksi.FungsiForm = "SEMUA"
        If txt_KodeLawanTransaksi.Text = Nothing Then
            frm_ListLawanTransaksi.ResetForm()
        Else
            frm_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            frm_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        frm_ListLawanTransaksi.ShowDialog()
        txt_NamaLawanTransaksi.Text = frm_ListLawanTransaksi.NamaMitraTerseleksi
        txt_KodeLawanTransaksi.Text = frm_ListLawanTransaksi.KodeMitraTerseleksi

    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTannsaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = txt_UraianTransaksi.Text
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        BarisTerseleksi = DataTabelUtama.CurrentRow.Index

        If BarisTerseleksi < JumlahBarisJurnal Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

        If BisaDiedit = False Then
            btn_TambahTransaksi.Enabled = False
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

    End Sub

    Private Sub btn_TambahTransaksi_Click(sender As Object, e As EventArgs) Handles btn_TambahTransaksi.Click

        frm_InputJurnalPerTransaksi.ResetForm()
        frm_InputJurnalPerTransaksi.ShowDialog()

        If frm_InputJurnalPerTransaksi.TombolPenutup = "OK" Then

            Dim COATerseleksi = frm_InputJurnalPerTransaksi.COATerseleksi
            Dim NamaAkunTerseleksi = frm_InputJurnalPerTransaksi.NamaAkunTerseleksi
            Dim DK = frm_InputJurnalPerTransaksi.DK
            Dim JumlahTransaksi As Int64 = frm_InputJurnalPerTransaksi.JumlahTransaksi
            Dim JumlahDebet
            Dim JumlahKredit
            If DK = "D" Then
                JumlahDebet = JumlahTransaksi
                JumlahKredit = StripKosong
            Else
                JumlahDebet = StripKosong
                JumlahKredit = JumlahTransaksi
                NamaAkunTerseleksi = PenjorokNamaAkun + NamaAkunTerseleksi
            End If

            Select Case DK
                Case dk_D
                    Dim DK_Telusur
                    BarisTerseleksi = 0
                    For Each row As DataGridViewRow In DataTabelUtama.Rows
                        DK_Telusur = row.Cells("D_K").Value
                        If DK_Telusur <> "D" Then Exit For
                        BarisTerseleksi += 1
                    Next
                Case dk_K
                    BarisTerseleksi = JumlahBarisJurnal
            End Select

            JumlahBarisJurnal += 1

            DataTabelUtama.Rows.Insert(BarisTerseleksi, Nothing, COATerseleksi, NamaAkunTerseleksi, DK, JumlahDebet, JumlahKredit)

            TampilkanData()

            Dim NomorUrut = 0
            For Each row As DataGridViewRow In DataTabelUtama.Rows
                NomorUrut += 1
                row.Cells("Nomor_Urut").Value = NomorUrut
                If NomorUrut = JumlahBarisJurnal Then Exit For
            Next

        End If

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        If (BarisTerseleksi < 0) Or (BarisTerseleksi >= JumlahBarisJurnal) Then
            MsgBox("Tidak ada baris terseleksi.")
            Return
        End If

        Dim COATerseleksi = DataTabelUtama.Item("Kode_Akun", BarisTerseleksi).Value
        Dim NamaAkunTerseleksi = DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value
        Dim DK = DataTabelUtama.Item("D_K", BarisTerseleksi).Value
        JumlahDebet = AmbilAngka(DataTabelUtama.Item("Debet_", BarisTerseleksi).Value)
        JumlahKredit = AmbilAngka(DataTabelUtama.Item("Kredit_", BarisTerseleksi).Value)
        frm_InputJurnalPerTransaksi.ResetForm()
        frm_InputJurnalPerTransaksi.txt_COA.Text = COATerseleksi
        frm_InputJurnalPerTransaksi.txt_NamaAkun.Text = NamaAkunTerseleksi
        If DK = "D" Then
            frm_InputJurnalPerTransaksi.cmb_DK.Text = "DEBET"
            frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahDebet
        End If
        If DK = "K" Then
            frm_InputJurnalPerTransaksi.cmb_DK.Text = "KREDIT"
            frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahKredit
        End If

        'Reset form Input Jurnal, ada di atas. Jangan taruh di sini...!!!
        frm_InputJurnalPerTransaksi.ShowDialog()

        DK = frm_InputJurnalPerTransaksi.DK
        Dim JumlahTransaksi As Int64 = AmbilAngka(frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text)
        COATerseleksi = frm_InputJurnalPerTransaksi.COATerseleksi
        NamaAkunTerseleksi = frm_InputJurnalPerTransaksi.NamaAkunTerseleksi
        If DK = "D" Then
            DataTabelUtama.Item("D_K", BarisTerseleksi).Value = "D"
            DataTabelUtama.Item("Debet_", BarisTerseleksi).Value = JumlahTransaksi
            DataTabelUtama.Item("Kredit_", BarisTerseleksi).Value = StripKosong
        End If
        If DK = "K" Then
            DataTabelUtama.Item("D_K", BarisTerseleksi).Value = "K"
            DataTabelUtama.Item("Debet_", BarisTerseleksi).Value = StripKosong
            DataTabelUtama.Item("Kredit_", BarisTerseleksi).Value = JumlahTransaksi
            NamaAkunTerseleksi = PenjorokNamaAkun + NamaAkunTerseleksi
        End If
        DataTabelUtama.Item("Kode_Akun", BarisTerseleksi).Value = COATerseleksi
        DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value = NamaAkunTerseleksi

        TampilkanData()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        If (BarisTerseleksi < 0) Or (BarisTerseleksi >= JumlahBarisJurnal) Then
            MsgBox("Tidak ada baris terseleksi.")
            Return
        End If

        DataTabelUtama.Rows.Remove(DataTabelUtama.CurrentRow)

        JumlahBarisJurnal -= 1
        Dim Baris = 0
        Do While Baris < JumlahBarisJurnal
            DataTabelUtama.Item("Nomor_Urut", Baris).Value = Baris + 1
            Baris = Baris + 1
        Loop

        TampilkanData()

    End Sub

    Private Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        JurnalTersimpan = False 'Default : Wajib False
        jur_StatusPenyimpananJurnal_Lengkap = False

        If JalurMasuk = Nothing Then
            PesanUntukProgrammer("Jalur Masuk belum ditentukan...!")
            Return
        End If

        If TotalDebet <> TotalKredit Then
            MsgBox("Jurnal tidak dapat diposting karena ADA SELISIH." &
                   Enter2Baris & "Silakan dikoreksi kembali.")
            Return
        End If

        If cmb_JenisJurnal.Text = Nothing Then
            MsgBox("Silakan tentukan 'Jenis Jurnal'.")
            cmb_JenisJurnal.Focus()
            Return
        End If

        If UraianTransaksi = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Uraian'.")
            txt_UraianTransaksi.Focus()
            Return
        End If

        Pilihan = MessageBox.Show("Yakin data sudah benar..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'Jika form berfungsi untuk mengedit, maka hapus data sebelumnya :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            jur_NomorJV = AngkaNomorJV
        End If

        '====================================================================================
        'PENYIMPANAN JURNAL :
        '====================================================================================
        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(dtp_TanggalJurnal.Value)
        jur_JenisJurnal = JenisJurnal
        jur_KodeDokumen = KodeDokumen
        jur_Referensi = KodeDokumen
        jur_Bundelan = Bundelan
        jur_TanggalInvoice = TanggalInvoice
        jur_NomorInvoice = NomorInvoice
        jur_NomorFakturPajak = NomorFakturPajak
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTannsaksi
        jur_UraianTransaksi = UraianTransaksi
        If JalurMasuk = Halaman_JURNALUMUM Then
            jur_Direct = 1
        Else
            jur_Direct = 0
        End If

        Dim Baris = 0
        Do While Baris < JumlahBarisJurnal
            jur_COA = DataTabelUtama.Item("Kode_Akun", Baris).Value
            jur_DK = DataTabelUtama.Item("D_K", Baris).Value
            If jur_DK = dk_D Then jur_JumlahMutasi = AmbilAngka(DataTabelUtama.Item("Debet_", Baris).Value)
            If jur_DK = dk_K Then jur_JumlahMutasi = AmbilAngka(DataTabelUtama.Item("Kredit_", Baris).Value)
            ______________________________________SimpanJurnal_PerBaris()
            Baris += 1
        Loop

        Select Case JalurMasuk
            Case Halaman_JURNALUMUM
                If usc_JurnalUmum.StatusAktif Then usc_JurnalUmum.TampilkanData()
            Case Halaman_BUKUBESAR
                If usc_BukuBesar.StatusAktif = True Then usc_BukuBesar.TampilkanData()
            Case Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
                If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
            Case Halaman_BUKUPENGAWASANTURUNANGAJI
                If usc_BukuPengawasanGaji.StatusAktif Then usc_BukuPengawasanGaji.RefreshTampilanData()
        End Select

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            JurnalTersimpan = True
            MsgBox("Jurnal BERHASIL disimpan.")
            Me.Close()
        Else
            JurnalTersimpan = False
            MsgBox("Jurnal GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click

        Me.Close()

    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        KunciUkuranForm(Me, 616, 714)
    End Sub

End Class