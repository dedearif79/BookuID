Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_InputAmortisasiBiaya_X

    Public FungsiForm
    Public JalurMasuk
    Public IdAmortisasi As Integer
    Dim TahunTransaksi As String
    Dim BulanTransaksi As String
    Public KodeAsset As String
    Public KodeAsset_SebelumDiedit


    Dim COAAmortisasi
    Dim NamaAkun_Amortisasi
    Dim COABiaya
    Dim NamaAkun_Biaya
    Dim MasaAmortisasi
    Dim TanggalTransaksi
    Dim JumlahTransaksi
    Dim AmortisasiPerBulan = JumlahTransaksi / MasaAmortisasi
    Dim Keterangan

    Dim JumlahProduk

    Private Sub frm_InputAmortisasiBiaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Select Case FungsiForm
            Case FungsiForm_EDIT
                Me.Text = "Edit Data Amortisasi Biaya"
                btn_Reset.Enabled = False
                IsiValueForm()
            Case FungsiForm_TAMBAH
                Me.Text = "Input Data Amortisasi Biaya"
                btn_Reset.Enabled = True
            Case Else
                PesanUntukProgrammer("Tentukan Status Form Dulu..!")
                Me.Close()
        End Select

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            txt_NamaAkun_Biaya.Enabled = False
            txt_KodeAsset.Enabled = False
            txt_KodeAsset.Text = Kosongan
            txt_JumlahTransaksi.Enabled = False
            btn_Reset.Enabled = False
            btn_Simpan.Enabled = False
            grb_AkunAmortisasi.Enabled = False
            dtp_TanggalTransaksi.Enabled = False
            btn_Tutup.Text = teks_Tambahkan_
        End If

        If JalurMasuk = Halaman_AMORTISASIBIAYA Then
            txt_NamaAkun_Biaya.Enabled = True
            txt_JumlahTransaksi.Enabled = True
            btn_Simpan.Enabled = True
            grb_AkunAmortisasi.Enabled = True
            dtp_TanggalTransaksi.Enabled = True
            btn_Tutup.Text = teks_Tutup
        End If

        If FungsiForm = Kosongan Then
            PesanUntukProgrammer("Tentukan Fungsi Form Dulu..!")
            Me.Close()
        End If

        If JalurMasuk = Kosongan Then
            PesanUntukProgrammer("Tentukan Jalur Masuk dulu..!")
            Me.Close()
        End If

        ProsesLoadingForm = False

    End Sub

    Sub IsiValueForm()

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        txt_COA_Amortisasi.Text = Nothing
        txt_NamaAkun_Amortisasi.Text = Nothing
        txt_COA_Biaya.Text = Nothing
        txt_NamaAkun_Biaya.Text = Nothing
        txt_MasaAmortisasi.Text = Nothing
        dtp_TanggalTransaksi.Value = Today
        txt_JumlahTransaksi.Text = Nothing
        txt_Keterangan.Text = Nothing
        btn_Tutup.Text = teks_Tutup
        SistemPenomoranOtomatis_KodeAsset() 'Ini sudah benar di posisi paling bawah. Jangan dipindah.

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_KodeAsset()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_AmortisasiBiaya WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_AmortisasiBiaya ) ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            IdAmortisasi = dr.Item("Nomor_ID") + 1
        Else
            IdAmortisasi = 1
        End If
        AksesDatabase_General(Tutup)
        KodeAsset_Value()
    End Sub

    Sub KodeAsset_Value()
        TahunTransaksi = Format(dtp_TanggalTransaksi.Value, "yyyy")
        BulanTransaksi = Format(dtp_TanggalTransaksi.Value, "MM")
        KodeAsset = txt_COA_Amortisasi.Text & "-" & TahunTransaksi & "-" & BulanTransaksi & "-" & IdAmortisasi
        txt_KodeAsset.Text = KodeAsset
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then txt_KodeAsset.Text = Kosongan
    End Sub


    Private Sub txt_KodeAsset_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeAsset.TextChanged
        KodeAsset = txt_KodeAsset.Text
    End Sub

    Private Sub txt_COA_Amortisasi_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_Amortisasi.TextChanged
        COAAmortisasi = txt_COA_Amortisasi.Text
        txt_NamaAkun_Amortisasi.Text = AmbilValue_NamaAkun(COAAmortisasi)
        KodeAsset_Value()
    End Sub
    Private Sub txt_COA_Amortisasi_Click(sender As Object, e As EventArgs) Handles txt_COA_Amortisasi.Click
    End Sub
    Private Sub txt_COA_Amortisasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_Amortisasi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub PenentuanCOA_BiayaAmortisasi()
        Select Case COAAmortisasi
            Case KodeTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53118' OR " &
                    " COA = '53119' OR " &
                    " COA = '61501' ) "
            Case KodeTautanCOA_SewaMesinDanPeralatanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53111' OR " &
                    " COA = '61502' ) "
            Case KodeTautanCOA_SewaKendaraanDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53112' OR " &
                    " COA = '61503' ) "
            Case KodeTautanCOA_BiayaRenovasiDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '53120' OR " &
                    " COA = '61404' ) "
            Case KodeTautanCOA_BiayaPendirianPerusahaan
                FilterListCOA_BiayaAmortisasi = " AND COA = '61214' "
            Case KodeTautanCOA_AsuransiDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND ( " &
                    " COA = '52110' OR " &
                    " COA = '53115' OR " &
                    " COA = '53116' OR " &
                    " COA = '53117' OR " &
                    " COA = '61110' OR " &
                    " COA = '61210' OR " &
                    " COA = '61211' OR " &
                    " COA = '61212' ) "
            Case KodeTautanCOA_SewaAssetLainnyaDibayarDimuka
                FilterListCOA_BiayaAmortisasi = " AND COA = '61506' "
        End Select
    End Sub

    Private Sub btn_PilihCOA_Amortisasi_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_Amortisasi.Click
        frm_ListCOA.ResetForm()
        frm_ListCOA.ListAkun = ListAkun_Amortisasi
        If txt_COA_Amortisasi.Text <> Kosongan Then
            frm_ListCOA.COATerseleksi = txt_COA_Amortisasi.Text
            frm_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Amortisasi.Text
        End If
        frm_ListCOA.ShowDialog()
        txt_COA_Amortisasi.Text = frm_ListCOA.COATerseleksi
    End Sub


    Private Sub txt_NamaAkun_Amortisasi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_Amortisasi.TextChanged
        NamaAkun_Amortisasi = txt_NamaAkun_Amortisasi.Text
    End Sub
    Private Sub txt_NamaAkun_Amortisasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun_Amortisasi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_COA_Biaya_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_Biaya.TextChanged
        COABiaya = txt_COA_Biaya.Text
        txt_NamaAkun_Biaya.Text = AmbilValue_NamaAkun(COABiaya)
    End Sub
    Private Sub txt_COA_Biaya_Click(sender As Object, e As EventArgs) Handles txt_COA_Biaya.Click
        btn_PilihCOA_Biaya_Click(sender, e)
    End Sub
    Private Sub txt_COA_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_Biaya.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOA_Biaya_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_Biaya.Click
        frm_ListCOA.ResetForm()
        frm_ListCOA.ListAkun = ListAkun_BiayaAmortisasi
        If txt_COA_Biaya.Text <> Kosongan Then
            frm_ListCOA.COATerseleksi = txt_COA_Biaya.Text
            frm_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Biaya.Text
        End If
        PenentuanCOA_BiayaAmortisasi()
        frm_ListCOA.ShowDialog()
        txt_COA_Biaya.Text = frm_ListCOA.COATerseleksi
        FilterListCOA_BiayaAmortisasi = Kosongan '(Reset)
    End Sub

    Private Sub txt_NamaAkun_Biaya_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_Biaya.TextChanged
        NamaAkun_Biaya = txt_NamaAkun_Biaya.Text
    End Sub
    Private Sub txt_NamaAkun_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun_Biaya.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_MasaAmortisasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_MasaAmortisasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_MasaAmortisasi_TextChanged(sender As Object, e As EventArgs) Handles txt_MasaAmortisasi.TextChanged
        MasaAmortisasi = AmbilAngka(txt_MasaAmortisasi.Text)
        PemecahRibuanUntukTextBox(txt_MasaAmortisasi)
    End Sub
    Private Sub txt_MasaAmortisasi_Leave(sender As Object, e As EventArgs) Handles txt_MasaAmortisasi.Leave
    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalTransaksi)
        TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.Value)
        KodeAsset_Value()
        TahunTransaksi = Format(dtp_TanggalTransaksi.Value, "yyyy")
    End Sub

    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransaksi)
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi Ulang Value :
        Keterangan = txt_Keterangan.Text
        AmortisasiPerBulan = JumlahTransaksi / MasaAmortisasi

        'Validasi Form :
        If COAAmortisasi = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Amortisasi'.")
            txt_COA_Amortisasi.Focus()
            Return
        End If
        If COABiaya = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Biaya'.")
            txt_COA_Biaya.Focus()
            Return
        End If
        If AmbilAngka(txt_MasaAmortisasi.Text) = 0 Then
            MsgBox("Silakan isi kolom 'Masa Amortisasi'.")
            txt_MasaAmortisasi.Focus()
            Return
        End If
        If MasaAmortisasi < 13 Then
            PesanPeringatan("Masa amortisasi harus lebih dari 12 bulan.")
            txt_MasaAmortisasi.Focus()
            Return
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            If TahunTransaksi > TahunCutOff Then
                MsgBox("Untuk 'Transaksi' setelah 'Tanggal Cut Off' (31-12-" & TahunCutOff & "), silakan diinput sesuai Tahun Bukunya masing-masing. ")
                Return
            End If
        End If
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TahunTransaksi <> TahunBukuAktif Then
                MsgBox("'Tahun Transaksi' tidak sesuai dengan 'Tahun Buku Aktif'")
                Return
            End If
        End If
        If txt_JumlahTransaksi.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        TrialBalance_Mentahkan()

        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_AmortisasiBiaya VALUES ( " &
                                  " '" & IdAmortisasi & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & COAAmortisasi & "', " &
                                  " '" & NamaAkun_Amortisasi & "', " &
                                  " '" & COABiaya & "', " &
                                  " '" & NamaAkun_Biaya & "', " &
                                  " '" & MasaAmortisasi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & Keterangan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then

            'Edit Data Amortisasi Biaya :
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" UPDATE tbl_AmortisasiBiaya SET " &
                                  " Kode_Asset              = '" & KodeAsset & "', " &
                                  " COA_Amortisasi          = '" & COAAmortisasi & "', " &
                                  " Nama_Akun_Amortisasi    = '" & NamaAkun_Amortisasi & "', " &
                                  " COA_Biaya               = '" & COABiaya & "', " &
                                  " Nama_Akun_Biaya         = '" & NamaAkun_Biaya & "', " &
                                  " Masa_Amortisasi         = '" & MasaAmortisasi & "', " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Keterangan & "' " &
                                  " WHERE Nomor_ID          = '" & IdAmortisasi & "' ",
                                  KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal :
            If StatusSuntingDatabase = True Then
                'Hapus Data Jurnal :
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE from tbl_Transaksi " &
                                      " WHERE Bundelan = '" & KodeAsset_SebelumDiedit & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                If usc_DaftarAmortisasiBiaya.StatusAktif Then usc_DaftarAmortisasiBiaya.TampilkanData()
                MsgBox("Data BERHASIL disimpan.")
                ResetForm()
            End If
            If FungsiForm = FungsiForm_EDIT Then
                If usc_DaftarAmortisasiBiaya.StatusAktif Then usc_DaftarAmortisasiBiaya.TampilkanData()
                MsgBox("Data BERHASIL diedit.")
                Me.Close()
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                MsgBox("Data GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End If

    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        If MasaAmortisasi < 13 Then
            PesanPeringatan("Masa amortisasi harus lebih dari 12 bulan.")
            txt_MasaAmortisasi.Focus()
            Return
        End If
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If txt_COA_Biaya.Text = Kosongan Then
                PesanPeringatan("Silakan pilih 'Akun Biaya'.")
                txt_COA_Biaya.Focus()
                Return
            End If
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = txt_COA_Biaya.Text
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = txt_MasaAmortisasi.Text
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = Kosongan
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = Kosongan
        End If
        Me.Close()
    End Sub


    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If txt_COA_Biaya.Text = Kosongan Or txt_MasaAmortisasi.Text = Kosongan Then
                PesanPeringatan("Anda belum melengkapi 'Data Amortisasi'." & Enter2Baris & "COA Amortisasi dibatalkan..!")
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Produk") = Kosongan
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = Kosongan
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = Kosongan
            End If
        End If
    End Sub

End Class