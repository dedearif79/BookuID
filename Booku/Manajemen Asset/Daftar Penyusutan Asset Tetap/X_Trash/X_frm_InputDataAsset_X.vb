Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_InputDataAsset_X

    Public FungsiForm
    Public JalurMasuk
    Public IdAsset As Integer
    Dim KodeDivisi As String
    Dim TahunPerolehan As String
    Dim BulanPerolehan As String
    Public KodeAsset_SebelumDiedit

    Public KodeAsset As String
    Public NomorPembelian As String
    Dim NamaAktiva
    Dim COA_Asset
    Dim COA_AkumulasiPenyusutan
    Dim COA_BiayaPenyusutan
    Dim KelompokHarta
    Dim MasaManfaat
    Dim Divisi
    Dim TanggalPerolehan
    Dim HargaPerolehan
    Dim Keterangan

    Private Sub frm_InputDataAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data Asset"
            btn_Reset.Enabled = False
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input Data Asset"
            btn_Reset.Enabled = True
        End If

        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            txt_NamaAktiva.Enabled = False
            txt_KodeAsset.Enabled = False
            txt_KodeAsset.Text = Kosongan
            dtp_TanggalPerolehan.Enabled = False
            txt_HargaPerolehan.Enabled = False
            btn_Reset.Enabled = False
            btn_Simpan.Enabled = False
            btn_Tutup.Text = teks_Tambahkan_
        End If

        If JalurMasuk = Halaman_DATAASSETTETAP Then
            txt_NamaAktiva.Enabled = True
            dtp_TanggalPerolehan.Enabled = True
            txt_HargaPerolehan.Enabled = True
            btn_Simpan.Enabled = True
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

    End Sub

    Sub KontenComboKelompokHarta()
        cmb_KelompokHarta.Items.Clear()
        cmb_KelompokHarta.Items.Add(KelompokHarta_1)
        cmb_KelompokHarta.Items.Add(KelompokHarta_2)
        cmb_KelompokHarta.Items.Add(KelompokHarta_3)
        cmb_KelompokHarta.Items.Add(KelompokHarta_4)
        cmb_KelompokHarta.Items.Add(KelompokHarta_BangunanPermanen)
        cmb_KelompokHarta.Items.Add(KelompokHarta_BangunanTidakPermanen)
        cmb_KelompokHarta.Items.Add(KelompokHarta_Tanah)
        cmb_KelompokHarta.Text = Kosongan
    End Sub

    Sub KontenComboDivisi()
        cmb_Divisi.Items.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            cmb_Divisi.Items.Add(dr.Item("Kode_Divisi") & " - " & dr.Item("Divisi"))
        Loop
        AksesDatabase_General(Tutup)
        cmb_Divisi.Text = Kosongan
    End Sub


    Public Sub ResetForm()
        ProsesResetForm = True
        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        NomorPembelian = Kosongan
        KontenComboKelompokHarta()
        grb_AkunAsset.Enabled = False
        grb_AkunBiayaPenyusutan.Enabled = False
        grb_AkunAkumulasiPenyusutan.Enabled = False
        txt_NamaAktiva.Text = Kosongan
        txt_COA_Asset.Text = Kosongan
        txt_NamaAkun_Asset.Text = Kosongan
        txt_COA_BiayaPenyusutan.Text = Kosongan
        txt_NamaAkun_BiayaPenyusutan.Text = Kosongan
        txt_MasaManfaat.Text = Kosongan
        txt_TarifPenyusutan.Text = Kosongan
        KontenComboDivisi()
        KodeDivisi = Kosongan
        dtp_TanggalPerolehan.Value = Today
        txt_HargaPerolehan.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        btn_Tutup.Text = teks_Tutup
        SistemPenomoranOtomatis_KodeAsset() 'Ini sudah benar di posisi paling bawah. Jangan dipindah.
        ProsesResetForm = False
    End Sub

    Public Sub SistemPenomoranOtomatis_KodeAsset()
        IdAsset = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataAsset") + 1
        KodeAsset_Value()
    End Sub

    Private Sub txt_KodeAsset_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeAsset.TextChanged
        KodeAsset = txt_KodeAsset.Text
    End Sub
    Private Sub txt_KodeAsset_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeAsset.KeyPress
        HanyaBoleh_Huruf_Angka_dan_Strip(sender, e)
    End Sub
    Private Sub txt_KodeAsset_Leave(sender As Object, e As EventArgs) Handles txt_KodeAsset.Leave
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeAsset.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim NamaMitra = dr.Item("Nama_Mitra")
            MsgBox("Kode '" & txt_KodeAsset.Text & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaMitra & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
            txt_KodeAsset.Text = Kosongan
            txt_KodeAsset.Focus()
            AksesDatabase_General(Tutup)
            Return
        End If
        AksesDatabase_General(Tutup)
    End Sub
    Sub KodeAsset_Value()
        If KodeDivisi = Kosongan Then
            KodeDivisi = "000"
        End If
        TahunPerolehan = Format(dtp_TanggalPerolehan.Value, "yyyy")
        BulanPerolehan = Format(dtp_TanggalPerolehan.Value, "MM")
        KodeAsset = COA_Asset & "-" & KodeDivisi & "-" & TahunPerolehan & "-" & BulanPerolehan & "-" & IdAsset
        txt_KodeAsset.Text = KodeAsset
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then txt_KodeAsset.Text = Kosongan
    End Sub

    Private Sub txt_NamaAktiva_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAktiva.TextChanged
        NamaAktiva = txt_NamaAktiva.Text
    End Sub
    Private Sub txt_NamaAktiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAktiva.KeyPress
        TolakKarakterTertentu(sender, e)
    End Sub

    Private Sub cmb_KelompokHarta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KelompokHarta.SelectedIndexChanged
    End Sub
    Private Sub cmb_KelompokHarta_TextChanged(sender As Object, e As EventArgs) Handles cmb_KelompokHarta.TextChanged
        KelompokHarta = cmb_KelompokHarta.Text
        If JalurMasuk = Halaman_DATAASSETTETAP Then
            txt_COA_Asset.Text = Kosongan
            txt_COA_BiayaPenyusutan.Text = Kosongan
        End If
        txt_MasaManfaat.Text = KonversiKelompokHartaKeMasaManfaat(KelompokHarta)
        If KelompokHarta <> Kosongan And JalurMasuk = Halaman_DATAASSETTETAP Then
            grb_AkunAsset.Enabled = True
        End If
        If KelompokHarta = KelompokHarta_Tanah Then
            txt_TarifPenyusutan.Text = 0
        Else
            txt_TarifPenyusutan.Text = 100 / AmbilAngka(txt_MasaManfaat.Text)
        End If
    End Sub

    Private Sub txt_COA_Asset_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_Asset.TextChanged
        COA_Asset = txt_COA_Asset.Text
        txt_NamaAkun_Asset.Text = AmbilValue_NamaAkun(COA_Asset)
        If KelompokHarta <> KelompokHarta_Tanah Then
            txt_COA_BiayaPenyusutan.Text = PenentuanCOA_BiayaPenyusutan(COA_Asset)
            txt_COA_AkumulasiPenyusutan.Text = PenentuanCOA_AkumulasiPenyusutan(COA_Asset)
        End If
        KodeAsset_Value()
    End Sub
    Private Sub txt_COA_Asset_Click(sender As Object, e As EventArgs) Handles txt_COA_Asset.Click
        btn_PilihCOA_Asset_Click(sender, e)
    End Sub
    Private Sub txt_COA_Asset_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_Asset.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub btn_PilihCOA_Asset_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_Asset.Click
        frm_ListCOA.ResetForm()
        If KelompokHarta = KelompokHarta_Tanah Then
            frm_ListCOA.ListAkun = ListAkun_AssetTanah
        Else
            frm_ListCOA.ListAkun = ListAkun_AssetTetap_SelainTanah
        End If
        If txt_COA_Asset.Text <> Kosongan Then
            frm_ListCOA.COATerseleksi = txt_COA_Asset.Text
            frm_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_Asset.Text
        End If
        frm_ListCOA.ShowDialog()
        txt_COA_Asset.Text = frm_ListCOA.COATerseleksi
    End Sub

    Private Sub txt_NamaAkun_Asset_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_Asset.TextChanged
    End Sub

    Private Sub txt_COA_BiayaPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_BiayaPenyusutan.TextChanged
        COA_BiayaPenyusutan = txt_COA_BiayaPenyusutan.Text
        txt_NamaAkun_BiayaPenyusutan.Text = AmbilValue_NamaAkun(COA_BiayaPenyusutan)
    End Sub
    Private Sub txt_COA_BiayaPenyusutan_Click(sender As Object, e As EventArgs) Handles txt_COA_BiayaPenyusutan.Click
        btn_PilihCOA_BiayaPenyusutan_Click(sender, e)
    End Sub
    Private Sub txt_COA_BiayaPenyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_BiayaPenyusutan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOA_BiayaPenyusutan_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_BiayaPenyusutan.Click
        frm_ListCOA.ResetForm()
        frm_ListCOA.ListAkun = ListAkun_BiayaPenyusutan
        If txt_COA_BiayaPenyusutan.Text <> Kosongan Then
            frm_ListCOA.COATerseleksi = txt_COA_BiayaPenyusutan.Text
            frm_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_BiayaPenyusutan.Text
        End If
        frm_ListCOA.ShowDialog()
        txt_COA_BiayaPenyusutan.Text = frm_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_BiayaPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_BiayaPenyusutan.TextChanged
    End Sub


    Private Sub txt_COA_AkumulasiPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_COA_AkumulasiPenyusutan.TextChanged
        COA_AkumulasiPenyusutan = txt_COA_AkumulasiPenyusutan.Text
        txt_NamaAkun_AkumulasiPenyusutan.Text = AmbilValue_NamaAkun(COA_AkumulasiPenyusutan)
    End Sub
    Private Sub txt_COA_AkumulasiPenyusutan_Click(sender As Object, e As EventArgs) Handles txt_COA_AkumulasiPenyusutan.Click
        btn_PilihCOA_AkumulasiPenyusutan_Click(sender, e)
    End Sub
    Private Sub txt_COA_AkumulasiPenyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA_AkumulasiPenyusutan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOA_AkumulasiPenyusutan_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA_AkumulasiPenyusutan.Click
        frm_ListCOA.ResetForm()
        frm_ListCOA.ListAkun = ListAkun_AkumulasiPenyusutan
        If txt_COA_AkumulasiPenyusutan.Text <> Kosongan Then
            frm_ListCOA.COATerseleksi = txt_COA_AkumulasiPenyusutan.Text
            frm_ListCOA.NamaAkunTerseleksi = txt_NamaAkun_AkumulasiPenyusutan.Text
        End If
        frm_ListCOA.ShowDialog()
        txt_COA_AkumulasiPenyusutan.Text = frm_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_AkumulasiPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun_AkumulasiPenyusutan.TextChanged
    End Sub




    Private Sub txt_MasaManfaat_TextChanged(sender As Object, e As EventArgs) Handles txt_MasaManfaat.TextChanged
        MasaManfaat = AmbilAngka(txt_MasaManfaat.Text)
        PemecahRibuanUntukTextBox(txt_MasaManfaat)
    End Sub

    Private Sub txt_TarifPenyusutan_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPenyusutan.TextChanged

    End Sub

    Private Sub cmb_Divisi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Divisi.SelectedIndexChanged
    End Sub
    Private Sub cmb_Divisi_TextChanged(sender As Object, e As EventArgs) Handles cmb_Divisi.TextChanged
        Divisi = AmbilTeksTengahTakTerbatas(cmb_Divisi.Text, 7)
        KodeDivisi = AmbilTeksKiri(cmb_Divisi.Text, 3)
        KodeAsset_Value()
    End Sub

    Private Sub dtp_TanggalPerolehan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPerolehan.ValueChanged
        TanggalPerolehan = TanggalFormatTampilan(dtp_TanggalPerolehan.Value)
        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                If dtp_TanggalPerolehan.Value.Year > TahunBukuAktif Then
                    dtp_TanggalPerolehan.Value = New Date(TahunBukuAktif, dtp_TanggalPerolehan.Value.Month, dtp_TanggalPerolehan.Value.Day)
                End If
            Case JenisTahunBuku_NORMAL
                If dtp_TanggalPerolehan.Value.Year <> TahunBukuAktif Then
                    dtp_TanggalPerolehan.Value = New Date(TahunBukuAktif, dtp_TanggalPerolehan.Value.Month, dtp_TanggalPerolehan.Value.Day)
                End If
        End Select
        TahunPerolehan = dtp_TanggalPerolehan.Value.Year
        KodeAsset_Value()
    End Sub

    Private Sub txt_HargaPerolehan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaPerolehan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_HargaPerolehan_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaPerolehan.TextChanged
        HargaPerolehan = AmbilAngka(txt_HargaPerolehan.Text)
        PemecahRibuanUntukTextBox(txt_HargaPerolehan)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian ulang value :
        Keterangan = txt_Keterangan.Text

        'Validasi Form :
        If KelompokHarta = Kosongan Then
            MsgBox("Silakan pilih 'Kelompok Harta'.")
            cmb_KelompokHarta.Focus()
            Return
        End If
        If NamaAktiva = Kosongan Then
            MsgBox("Silakan isi kolom 'Nama Aktiva'.")
            txt_NamaAktiva.Focus()
            Return
        End If
        If COA_Asset = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Asset'.")
            txt_COA_Asset.Focus()
            Return
        End If
        If grb_AkunBiayaPenyusutan.Enabled = True And COA_BiayaPenyusutan = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Biaya Penyusutan'.")
            txt_COA_BiayaPenyusutan.Focus()
            Return
        End If
        If grb_AkunAkumulasiPenyusutan.Enabled = True And COA_AkumulasiPenyusutan = Kosongan Then
            MsgBox("Silakan pilih 'Kode Akun Akumulasi Penyusutan'.")
            txt_COA_AkumulasiPenyusutan.Focus()
            Return
        End If
        If cmb_Divisi.Text = Kosongan Then
            MsgBox("Silakan pilih 'Divisi'.")
            cmb_Divisi.Focus()
            Return
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            If TahunPerolehan > TahunCutOff Then
                MsgBox("Untuk 'Transaksi' setelah 'Tanggal Cut Off' (31-12-" & TahunCutOff & "), silakan diinput sesuai Tahun Bukunya masing-masing. ")
                Return
            End If
        End If
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TahunPerolehan <> TahunBukuAktif Then
                MsgBox("'Tahun Transaksi' tidak sesuai dengan 'Tahun Buku Aktif'")
                Return
            End If
        End If
        If HargaPerolehan = 0 Then
            MsgBox("Silakan isi kolom 'Harga Perolehan'.")
            txt_HargaPerolehan.Focus()
            Return
        End If
        Dim KelompokHarta_Angka = KonversiKelompokHartaKeAngka(KelompokHarta)

        TrialBalance_Mentahkan()

        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_DataAsset VALUES ( " &
                                  " '" & IdAsset & "', " &
                                  " '" & NomorPembelian & "', " &
                                  " '" & KodeAsset & "', " &
                                  " '" & NamaAktiva & "', " &
                                  " '" & COA_Asset & "', " &
                                  " '" & COA_BiayaPenyusutan & "', " &
                                  " '" & COA_AkumulasiPenyusutan & "', " &
                                  " '" & KelompokHarta_Angka & "', " &
                                  " '" & MasaManfaat & "', " &
                                  " '" & KodeDivisi & "', " &
                                  " '" & Divisi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                                  " '" & HargaPerolehan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Keterangan & "' " &
                                  " ) ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal Bundelan, terhitung dari bulan terjadinya transaksi sampai seterusnya.
            If StatusSuntingDatabase = True Then
                Dim cmdSUSURJURNAL As OdbcCommand
                Dim drSUSURJURNAL As OdbcDataReader
                Dim NomorJV_HarusDihapus = Kosongan
                AksesDatabase_Transaksi(Buka)
                cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                      " WHERE COA = '" & COA_BiayaPenyusutan & "' " &
                                      " AND Tanggal_Transaksi >= '" & TanggalFormatSimpan(TanggalPerolehan) & "' ",
                                      KoneksiDatabaseTransaksi)
                drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
                Do While drSUSURJURNAL.Read
                    NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                    HapusJurnal_BerdasarkanNomorJV(NomorJV_HarusDihapus)
                Loop
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                  " Kode_Asset                  = '" & KodeAsset & "', " &
                                  " Nama_Aktiva                 = '" & NamaAktiva & "', " &
                                  " COA_Asset                   = '" & COA_Asset & "', " &
                                  " COA_Biaya_Penyusutan        = '" & COA_BiayaPenyusutan & "', " &
                                  " COA_Akumulasi_Penyusutan    = '" & COA_AkumulasiPenyusutan & "', " &
                                  " Kelompok_Harta              = '" & KelompokHarta_Angka & "', " &
                                  " Masa_Manfaat                = '" & MasaManfaat & "', " &
                                  " Kode_Divisi                 = '" & KodeDivisi & "', " &
                                  " Divisi                      = '" & Divisi & "', " &
                                  " Tanggal_Perolehan           = '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                                  " Harga_Perolehan             = '" & HargaPerolehan & "', " &
                                  " Keterangan                  = '" & Keterangan & "' " &
                                  " WHERE Nomor_ID              = '" & IdAsset & "' " _
                                  , KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

            'Hapus Data Jurnal Bundelan Terkait :
            If StatusSuntingDatabase = True Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE from tbl_Transaksi " &
                                      " WHERE Bundelan LIKE '%" & KodeAsset_SebelumDiedit & "%' ",
                                      KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If

            'Hapus Lagi Data Jurnal Bundelan, terhitung dari bulan terjadinya transaksi sampai seterusnya.
            'Karena sangat mungkin ada perubahan tanggal transaksi saat pengeditan, dimana Bulan Transaksi yang baru ternyata lebih awal dari Bulan Transaksi sebelum diedit.
            If StatusSuntingDatabase = True Then
                Dim cmdSUSURJURNAL As OdbcCommand
                Dim drSUSURJURNAL As OdbcDataReader
                Dim NomorJV_HarusDihapus = Kosongan
                AksesDatabase_Transaksi(Buka)
                cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                                 " WHERE COA = '" & COA_BiayaPenyusutan & "' " &
                                                 " AND Tanggal_Transaksi >= '" & TanggalFormatSimpan(TanggalPerolehan) & "' ",
                                                 KoneksiDatabaseTransaksi)
                drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
                Do While drSUSURJURNAL.Read
                    NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                    HapusJurnal_BerdasarkanNomorJV(NomorJV_HarusDihapus)
                Loop
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                MsgBox("Data Asset BERHASIL disimpan.")
                If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.TampilkanData()
                ResetForm()
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Asset BERHASIL diedit.")
                If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.TampilkanData()
                Me.Close()
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                MsgBox("Data Asset GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Asset GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End If

    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If cmb_KelompokHarta.Text = Kosongan Then
                PesanPeringatan("Silakan pilih kolom 'Kelompok Harta'.")
                cmb_KelompokHarta.Focus()
                Return
            End If
            If cmb_Divisi.Text = Kosongan Then
                PesanPeringatan("Silakan pilih kolom 'Divisi'.")
                cmb_Divisi.Focus()
                Return
            End If
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = KonversiKelompokHartaKeAngka(cmb_KelompokHarta.Text)
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = AmbilTeksKiri(cmb_Divisi.Text, 3)
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Biaya") = Kosongan
            win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Masa_Amortisasi") = Kosongan
        End If
        Me.Close()
    End Sub

    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
            If cmb_KelompokHarta.Text = Kosongan Or cmb_Divisi.Text = Kosongan Then
                PesanPeringatan("Anda belum melengkapi 'Data Asset'." & Enter2Baris & "COA Asset dibatalkan..!")
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("COA_Produk") = Kosongan
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kelompok_Asset") = Kosongan
                win_InputInvoicePembelian.datatabelUtama.Rows(win_InputInvoicePembelian.BarisTerseleksi)("Kode_Divisi_Asset") = Kosongan
            End If
        End If
    End Sub

End Class