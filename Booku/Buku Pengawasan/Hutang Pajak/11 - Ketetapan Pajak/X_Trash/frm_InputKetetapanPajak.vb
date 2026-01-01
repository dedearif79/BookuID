Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputKetetapanPajak


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Public NomorBPHP
    Dim Nomor As String
    Dim KodeJenisPajak As String
    Dim JenisPajak
    Dim MasaPajak_Awal
    Dim MasaPajak_Akhir
    Dim TahunPajak_Inputan As Integer
    Dim NomorKetetapan
    Dim TanggalKetetapan
    Dim TahunKetetapan
    Public KodeAkun_PokokPajak
    Public KodeAkun_Sanksi
    Dim NamaAkun_PokokPajak
    Dim NamaAkun_Sanksi
    Dim PokokPajak
    Dim Sanksi
    Dim JumlahKetetapan
    Dim Keterangan
    Public NomorJV

    Dim BulanAngka_Awal As Integer
    Dim BulanAngka_Akhir As Integer


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Ketetapan Pajak"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Ketetapan Pajak"
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA " &
                                  " WHERE COA = '" & KodeAkun_PokokPajak & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NamaAkun_PokokPajak.Text = dr.Item("Nama_Akun")
            End If
            AksesDatabase_General(Tutup)
        End If

        Me.Text = JudulForm

        BeginInvoke(Sub() txt_Nomor.Focus())

        ProsesLoadingForm = False

    End Sub

    Sub KontenCombo_MasaPajak_Awal()
        cmb_MasaPajak_Awal.Items.Clear()
        cmb_MasaPajak_Awal.Items.Add(Bulan_Januari)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Februari)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Maret)
        cmb_MasaPajak_Awal.Items.Add(Bulan_April)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Mei)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Juni)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Juli)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Agustus)
        cmb_MasaPajak_Awal.Items.Add(Bulan_September)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Oktober)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Nopember)
        cmb_MasaPajak_Awal.Items.Add(Bulan_Desember)
        cmb_MasaPajak_Awal.Text = Kosongan
    End Sub

    Sub KontenCombo_MasaPajak_Akhir()

        cmb_MasaPajak_Akhir.Items.Clear()

        If BulanAngka_Awal <= 1 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Januari)
        If BulanAngka_Awal <= 2 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Februari)
        If BulanAngka_Awal <= 3 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Maret)
        If BulanAngka_Awal <= 4 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_April)
        If BulanAngka_Awal <= 5 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Mei)
        If BulanAngka_Awal <= 6 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Juni)
        If BulanAngka_Awal <= 7 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Juli)
        If BulanAngka_Awal <= 8 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Agustus)
        If BulanAngka_Awal <= 9 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_September)
        If BulanAngka_Awal <= 10 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Oktober)
        If BulanAngka_Awal <= 11 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Nopember)
        If BulanAngka_Awal <= 12 Then cmb_MasaPajak_Akhir.Items.Add(Bulan_Desember)

        If BulanAngka_Awal > BulanAngka_Akhir Then cmb_MasaPajak_Akhir.Text = Kosongan

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorBPHP = Kosongan
        txt_Nomor.Text = Kosongan
        txt_KodeJenisPajak.Text = Kosongan
        KontenCombo_MasaPajak_Awal()
        cmb_MasaPajak_Akhir.Items.Clear()
        cmb_MasaPajak_Akhir.Text = Kosongan
        txt_TahunPajak_Inputan.Text = Kosongan
        dtp_TanggalKetetapan.Value = Today
        txt_NomorKetetapan.Text = Kosongan
        txt_PokokPajak.Text = Kosongan
        lbl_COA_PokokPajak.Enabled = False
        txt_KodeAkun_PokokPajak.Text = Kosongan
        txt_NamaAkun_PokokPajak.Text = Kosongan
        txt_KodeAkun_PokokPajak.Enabled = False
        txt_NamaAkun_PokokPajak.Enabled = False
        txt_Sanksi.Text = Kosongan
        lbl_COA_Sanksi.Enabled = False
        txt_KodeAkun_Sanksi.Text = Kosongan
        txt_NamaAkun_Sanksi.Text = Kosongan
        txt_KodeAkun_Sanksi.Enabled = False
        txt_NamaAkun_Sanksi.Enabled = False
        txt_Keterangan.Text = Kosongan
        txt_JumlahKetetapan.Text = Kosongan
        NomorJV = 0

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_NomorKetetapan()

        If ProsesResetForm = False Then

            Dim NomorID_Lama As Integer = 0

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Nomor, Nomor_ID FROM tbl_KetetapanPajak " &
                                  " WHERE Nomor = '" & Nomor & "' " &
                                  " AND Kode_Jenis_Pajak = '" & KodeJenisPajak & "' " &
                                  " AND Tahun_Pajak = '" & TahunKetetapan & "' ",
                                  KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then NomorID_Lama = dr.Item("Nomor_ID")
            AksesDatabase_Transaksi(Tutup)

            If NomorID <> NomorID_Lama Then
                MsgBox("Kombinasi Nomor, Kode Jenis Pajak dan Tahun Ketetapan sudah ada." & Enter2Baris &
                       "Silakan isi kolom-kolom tersebut dengan data yang lain..!")
                txt_Nomor.Text = Kosongan
                txt_KodeJenisPajak.Text = Kosongan
                txt_TahunPajak_Inputan.Text = Kosongan
                txt_NomorKetetapan.Text = Kosongan
                txt_Nomor.Focus()
                Return
            End If

        End If

        If Nomor <> Kosongan And KodeJenisPajak <> Kosongan And TahunPajak_Inputan <> 0 _
            Then
            txt_NomorKetetapan.Text =
                Nomor & "/" &
                KodeJenisPajak & "/" &
                Microsoft.VisualBasic.Right(TahunPajak_Inputan, 2) & "/" &
                KodeKPP_Perusahaan & "/" &
                Microsoft.VisualBasic.Right(AmbilTahun_DariTanggal(TanggalKetetapan), 2)
        Else
            txt_NomorKetetapan.Text = Kosongan
        End If

    End Sub

    Private Sub dtp_TanggalKetetapan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalKetetapan.ValueChanged
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalKetetapan)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalKetetapan)
        TanggalKetetapan = dtp_TanggalKetetapan.Value
        TahunKetetapan = dtp_TanggalKetetapan.Value.Year
    End Sub

    Private Sub dtp_TanggalKetetapan_Leave(sender As Object, e As EventArgs) Handles dtp_TanggalKetetapan.Leave
        SistemPenomoranOtomatis_NomorKetetapan()
    End Sub

    Private Sub txt_Nomor_TextChanged(sender As Object, e As EventArgs) Handles txt_Nomor.TextChanged
        Nomor = txt_Nomor.Text
    End Sub
    Private Sub txt_Nomor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Nomor.KeyPress
        HanyaBolehInputNomor(sender, e)
    End Sub
    Private Sub txt_Nomor_Leave(sender As Object, e As EventArgs) Handles txt_Nomor.Leave
        SistemPenomoranOtomatis_NomorKetetapan()
    End Sub

    Private Sub txt_KodeJenisPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeJenisPajak.TextChanged
        KodeJenisPajak = txt_KodeJenisPajak.Text
        If KodeJenisPajak = Kosongan Then
            txt_JenisPajak.Text = Kosongan
        End If
    End Sub
    Private Sub txt_KodeJenisPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeJenisPajak.KeyPress
        HanyaBolehInputNomor(sender, e)
    End Sub
    Private Sub txt_KodeJenisPajak_Leave(sender As Object, e As EventArgs) Handles txt_KodeJenisPajak.Leave
        If ProsesLoadingForm = False And ProsesResetForm = False Then
            If KodeJenisPajak <> Kosongan Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_JenisPajak " &
                                      " WHERE Kode_Jenis_Pajak = '" & KodeJenisPajak & "' ", KoneksiDatabaseGeneral)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    txt_JenisPajak.Text = dr.Item("Jenis_Pajak")
                    SistemPenomoranOtomatis_NomorKetetapan()
                Else
                    txt_KodeJenisPajak.Text = Kosongan
                    txt_JenisPajak.Text = Kosongan
                    MsgBox("'Kode Jenis Pajak' tidak terdaftar di sistem." & Enter2Baris & "Silakan input kode yang sesuai.")
                    txt_KodeJenisPajak.Focus()
                End If
                AksesDatabase_General(Tutup)
            End If
        End If
    End Sub

    Private Sub txt_JenisPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_JenisPajak.TextChanged
        JenisPajak = txt_JenisPajak.Text
    End Sub
    Private Sub txt_JenisPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JenisPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_MasaPajak_Awal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak_Awal.SelectedIndexChanged
    End Sub
    Private Sub cmb_MasaPajak_Awal_TextChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak_Awal.TextChanged
        MasaPajak_Awal = cmb_MasaPajak_Awal.Text
        BulanAngka_Awal = KonversiBulanKeAngka(MasaPajak_Awal)
        KontenCombo_MasaPajak_Akhir()
    End Sub
    Private Sub cmb_MasaPajak_Awal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_MasaPajak_Awal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_MasaPajak_Akhir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak_Akhir.SelectedIndexChanged
    End Sub
    Private Sub cmb_MasaPajak_Akhir_TextChanged(sender As Object, e As EventArgs) Handles cmb_MasaPajak_Akhir.TextChanged
        MasaPajak_Akhir = cmb_MasaPajak_Akhir.Text
        BulanAngka_Akhir = KonversiBulanKeAngka(MasaPajak_Akhir)
    End Sub
    Private Sub cmb_MasaPajak_Akhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_MasaPajak_Akhir.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TahunPajak_Inputan_TextChanged(sender As Object, e As EventArgs) Handles txt_TahunPajak_Inputan.TextChanged
        TahunPajak_Inputan = AmbilAngka(txt_TahunPajak_Inputan.Text)
    End Sub
    Private Sub txt_TahunPajak_Inputan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TahunPajak_Inputan.KeyPress
        HanyaBolehInputNomor(sender, e)
    End Sub
    Private Sub txt_TahunPajak_Inputan_Leave(sender As Object, e As EventArgs) Handles txt_TahunPajak_Inputan.Leave
        Dim JumlahKarakterTahunPajak As Integer = Microsoft.VisualBasic.Len(TahunPajak_Inputan.ToString)
        If JumlahKarakterTahunPajak <> 4 Then
            MsgBox("Silakan isi kolom 'Tahun Pajak' dengan benar..!")
            txt_TahunPajak_Inputan.Focus()
            Return
        End If
        SistemPenomoranOtomatis_NomorKetetapan()
    End Sub

    Private Sub txt_NomorKetetapan_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorKetetapan.TextChanged
        NomorKetetapan = txt_NomorKetetapan.Text
    End Sub
    Private Sub txt_NomorKetetapan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorKetetapan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PokokPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_PokokPajak.TextChanged
        PokokPajak = AmbilAngka(txt_PokokPajak.Text)
        If PokokPajak = 0 Then
            lbl_COA_PokokPajak.Enabled = False
            txt_KodeAkun_PokokPajak.Enabled = False
            txt_KodeAkun_PokokPajak.Text = Kosongan
            txt_NamaAkun_PokokPajak.Enabled = False
        Else
            lbl_COA_PokokPajak.Enabled = True
            txt_KodeAkun_PokokPajak.Enabled = True
            txt_NamaAkun_PokokPajak.Enabled = True
        End If
        Perhitungan_JumlahKetetapan()
        PemecahRibuanUntukTextBox(txt_PokokPajak)
    End Sub
    Private Sub txt_PokokPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PokokPajak.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_KodeAkun_PokokPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeAkun_PokokPajak.TextChanged
        KodeAkun_PokokPajak = txt_KodeAkun_PokokPajak.Text
        If KodeAkun_PokokPajak = Kosongan Then txt_NamaAkun_PokokPajak.Text = Kosongan
    End Sub
    Private Sub txt_KodeAkun_PokokPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeAkun_PokokPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_KodeAkun_PokokPajak_Enter(sender As Object, e As EventArgs) Handles txt_KodeAkun_PokokPajak.Enter
        PilihCOA_PokokPajak()
    End Sub
    Private Sub txt_KodeAkun_PokokPajak_DoubleClick(sender As Object, e As EventArgs) Handles txt_KodeAkun_PokokPajak.DoubleClick
        PilihCOA_PokokPajak()
    End Sub

    Private Sub PilihCOA_PokokPajak()
        If txt_KodeAkun_PokokPajak.Text = Kosongan Then
            frm_ListCOA.ResetForm()
        Else
            frm_ListCOA.COATerseleksi = KodeAkun_PokokPajak
            frm_ListCOA.NamaAkunTerseleksi = NamaAkun_PokokPajak
        End If
        frm_ListCOA.ListAkun = ListAkun_PokokPajak
        frm_ListCOA.ShowDialog()
        txt_KodeAkun_PokokPajak.Text = frm_ListCOA.COATerseleksi
        txt_NamaAkun_PokokPajak.Text = frm_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_NamaAkun_PokokPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_PokokPajak.TextChanged
        NamaAkun_PokokPajak = txt_NamaAkun_PokokPajak.Text
    End Sub
    Private Sub txt_NamaAkun_PokokPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun_PokokPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Sanksi_TextChanged(sender As Object, e As EventArgs) Handles txt_Sanksi.TextChanged
        Sanksi = AmbilAngka(txt_Sanksi.Text)
        If Sanksi = 0 Then
            lbl_COA_Sanksi.Enabled = False
            txt_KodeAkun_Sanksi.Enabled = False
            txt_KodeAkun_Sanksi.Text = Kosongan
            txt_NamaAkun_Sanksi.Enabled = False
        Else
            lbl_COA_Sanksi.Enabled = True
            txt_KodeAkun_Sanksi.Text = KodeTautanCOA_BiayaKetetapanPajak
            txt_KodeAkun_Sanksi.Enabled = True
            txt_NamaAkun_Sanksi.Enabled = True
        End If
        Perhitungan_JumlahKetetapan()
        PemecahRibuanUntukTextBox(txt_Sanksi)
    End Sub
    Private Sub txt_Sanksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Sanksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_KodeAkun_Sanksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeAkun_Sanksi.TextChanged
        KodeAkun_Sanksi = txt_KodeAkun_Sanksi.Text
        If KodeAkun_Sanksi = Kosongan Then
            txt_NamaAkun_Sanksi.Text = Kosongan
        Else
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA WHERE COA = '" & KodeAkun_Sanksi & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_NamaAkun_Sanksi.Text = dr.Item("Nama_Akun")
            End If
            AksesDatabase_General(Tutup)
        End If
    End Sub
    Private Sub txt_KodeAkun_Sanksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeAkun_Sanksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NamaAkun_Sanksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_Sanksi.TextChanged
        NamaAkun_Sanksi = txt_NamaAkun_Sanksi.Text
    End Sub

    Private Sub txt_NamaAkun_Sanksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun_Sanksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Sub Perhitungan_JumlahKetetapan()
        txt_JumlahKetetapan.Text = PokokPajak + Sanksi
    End Sub

    Private Sub txt_JumlahKetetapan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahKetetapan.TextChanged
        JumlahKetetapan = AmbilAngka(txt_JumlahKetetapan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahKetetapan)
    End Sub
    Private Sub txt_JumlahKetetapan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahKetetapan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Variabel2 Tertentu :
        Keterangan = txt_Keterangan.Text

        If Nomor = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor'.")
            txt_Nomor.Focus()
            Return
        End If

        If KodeJenisPajak = Kosongan Then
            MsgBox("Silakan isi kolom 'Kode Jenis Pajak'.")
            txt_KodeJenisPajak.Focus()
            Return
        End If

        If MasaPajak_Awal = Kosongan Then
            MsgBox("Silakan pilih 'Masa Pajak'.")
            cmb_MasaPajak_Awal.Focus()
            Return
        End If

        If MasaPajak_Akhir = Kosongan Then
            MsgBox("Silakan pilih 'Masa Pajak'.")
            cmb_MasaPajak_Akhir.Focus()
            Return
        End If

        If TahunPajak_Inputan = 0 Then
            MsgBox("Silakan isi kolom 'Tahun Pajak'.")
            txt_TahunPajak_Inputan.Focus()
            Return
        End If

        If PokokPajak + Sanksi = 0 Then
            MsgBox("Silakan isi kolom 'Pokok Pajak' dan/atau kolom 'Sanksi'.")
            txt_PokokPajak.Focus()
            Return
        End If

        If PokokPajak > 0 And KodeAkun_PokokPajak = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun' untuk Pokok Pajak.")
            txt_KodeAkun_PokokPajak.Focus()
            Return
        End If

        If JumlahKetetapan = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Ketetapan'.")
            txt_JumlahKetetapan.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_KetetapanPajak") + 1

            NomorBPHP = AwalanBPKP_PlusTahunBuku & NomorID

            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_KetetapanPajak VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & Nomor & "', " &
                                  " '" & KodeJenisPajak & "', " &
                                  " '" & JenisPajak & "', " &
                                  " '" & MasaPajak_Awal & "', " &
                                  " '" & MasaPajak_Akhir & "', " &
                                  " '" & TahunPajak_Inputan & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKetetapan) & "', " &
                                  " '" & NomorKetetapan & "', " &
                                  " '" & NomorBPHP & "', " &
                                  " '" & KodeAkun_PokokPajak & "', " &
                                  " '" & JumlahKetetapan & "', " &
                                  " '" & PokokPajak & "', " &
                                  " '" & Sanksi & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "') ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_KetetapanPajak SET " &
                                  " Nomor                   = '" & Nomor & "', " &
                                  " Kode_Jenis_Pajak        = '" & KodeJenisPajak & "', " &
                                  " Jenis_Pajak             = '" & JenisPajak & "', " &
                                  " Masa_Pajak_Awal         = '" & MasaPajak_Awal & "', " &
                                  " Masa_Pajak_Akhir        = '" & MasaPajak_Akhir & "', " &
                                  " Tahun_Pajak             = '" & TahunPajak_Inputan & "', " &
                                  " Tanggal_Ketetapan       = '" & TanggalFormatSimpan(TanggalKetetapan) & "', " &
                                  " Nomor_Ketetapan         = '" & NomorKetetapan & "', " &
                                  " Nomor_BPHP              = '" & NomorBPHP & "', " &
                                  " Kode_Akun_Pokok_Pajak   = '" & KodeAkun_PokokPajak & "', " &
                                  " Jumlah_Ketetapan        = '" & JumlahKetetapan & "', " &
                                  " Pokok_Pajak             = '" & PokokPajak & "', " &
                                  " Sanksi                  = '" & Sanksi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " Nomor_JV                = '" & NomorJV & "' " &
                                  " WHERE Nomor_ID          = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Hapus Data Jurnal yang Lama :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                       " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()

            jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalKetetapan)
            jur_JenisJurnal = JenisJurnal_Pembelian
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = Kosongan
            jur_NomorInvoice = Kosongan
            jur_NomorFakturPajak = Kosongan
            jur_KodeLawanTransaksi = KodeLawanTransaksi_DJP
            jur_NamaLawanTransaksi = NamaLawanTransaksi_DJP
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeAkun_PokokPajak, PokokPajak)
            ___jurDebet(KodeAkun_Sanksi, Sanksi)
            _______jurKredit(PenentuanCOA_HutangPajak(JenisPajak, KodeSetoran_Non), PokokPajak)
            _______jurKredit(KodeTautanCOA_HutangKetetapanPajak, Sanksi)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If usc_BukuPengawasanKetetapanPajak.StatusAktif Then usc_BukuPengawasanKetetapanPajak.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class