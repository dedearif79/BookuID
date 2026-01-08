Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputStockOpname

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Public JenisStok
    Dim TanggalPengecekan
    Dim NamaBarang
    Dim JumlahBarang
    Dim Satuan
    Dim HargaSatuan
    Dim JumlahHarga
    Dim Asal
    Dim Lokasi
    Dim Keterangan

    Public BulanPengunci_Angka

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Stock Opname " & JenisStok
            dtp_TanggalPengecekan.Value = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanPengunci_Angka, TahunBukuAktif)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Stock Opname " & JenisStok
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Me.Text = JudulForm

        Select Case JenisStok
            Case JenisStok_BahanPenolong
                lbl_Asal.Enabled = True
                cmb_Asal.Enabled = False
                cmb_Asal.Text = val_Lokal
            Case JenisStok_BahanBaku
                lbl_Asal.Enabled = True
                cmb_Asal.Enabled = True
                If FungsiForm = FungsiForm_TAMBAH Then cmb_Asal.Text = Kosongan
            Case JenisStok_BarangDalamProses
                cmb_Asal.Enabled = False
                lbl_Asal.Enabled = False
                cmb_Asal.Text = Kosongan
            Case JenisStok_BarangJadi
                cmb_Asal.Enabled = False
                lbl_Asal.Enabled = False
                cmb_Asal.Text = Kosongan
        End Select

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JenisStok = Kosongan

        NomorID = 0


        dtp_TanggalPengecekan.Enabled = True
        dtp_TanggalPengecekan.Value = TanggalIni
        txt_NamaBarang.Enabled = True
        txt_NamaBarang.Text = Kosongan
        txt_JumlahBarang.Text = Kosongan
        txt_Satuan.Enabled = True
        txt_Satuan.Text = Kosongan
        txt_HargaSatuan.Enabled = True
        txt_HargaSatuan.Text = Kosongan
        txt_JumlahHarga.Text = Kosongan
        KontenCombo_Asal()
        txt_Lokasi.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_Asal()
        cmb_Asal.Enabled = True
        cmb_Asal.Items.Clear()
        cmb_Asal.Items.Add(val_Lokal)
        cmb_Asal.Items.Add(val_Import)
        cmb_Asal.Text = Kosongan
    End Sub


    Private Sub dtp_TanggalPengecekan_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalPengecekan.ValueChanged
        KunciBulanDanTahun_HarusSama(dtp_TanggalPengecekan, BulanPengunci_Angka, TahunBukuAktif)
        TanggalPengecekan = dtp_TanggalPengecekan.Value
    End Sub


    Private Sub txt_NamaBarang_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaBarang.TextChanged
        NamaBarang = txt_NamaBarang.Text
    End Sub


    Private Sub txt_JumlahBarang_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBarang.TextChanged
        JumlahBarang = AmbilAngka(txt_JumlahBarang.Text)
        PerhitunganJumlahHarga()
        PemecahRibuanUntukTextBox(txt_JumlahBarang)
    End Sub
    Private Sub txt_JumlahBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahBarang.KeyPress
        HanyaBolehInputAngkaPlusMinus(sender, e)
    End Sub


    Private Sub txt_Satuan_TextChanged(sender As Object, e As EventArgs) Handles txt_Satuan.TextChanged
        Satuan = txt_Satuan.Text
    End Sub


    Private Sub txt_HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaSatuan.TextChanged
        HargaSatuan = AmbilAngka(txt_HargaSatuan.Text)
        PerhitunganJumlahHarga()
        PemecahRibuanUntukTextBox(txt_HargaSatuan)
    End Sub
    Private Sub txt_HargaSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaSatuan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_JumlahHarga_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHarga.TextChanged
        JumlahHarga = AmbilAngka(txt_JumlahHarga.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHarga)
    End Sub
    Private Sub txt_JumlahHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHarga.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub PerhitunganJumlahHarga()
        txt_JumlahHarga.Text = JumlahBarang * HargaSatuan
    End Sub


    Private Sub cmb_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Asal.SelectedIndexChanged
        Asal = cmb_Asal.Text
    End Sub
    Private Sub cmb_Asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Asal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_Lokasi_TextChanged(sender As Object, e As EventArgs) Handles txt_Lokasi.TextChanged
        Lokasi = txt_Lokasi.Text
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Varibel :
        Keterangan = txt_Keterangan.Text
        Asal = cmb_Asal.Text

        If NamaBarang = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Barang'.")
            txt_NamaBarang.Focus()
            Return
        End If

        If JumlahBarang = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Barang'.")
            txt_JumlahBarang.Focus()
            Return
        End If

        If Satuan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Satuan'.")
            txt_Satuan.Focus()
            Return
        End If

        If HargaSatuan = 0 Then
            PesanPeringatan("Silakan isi kolom 'Harga Satuan'.")
            txt_HargaSatuan.Focus()
            Return
        End If

        If Asal = Kosongan And cmb_Asal.Enabled = True Then
            PesanPeringatan("Silakan pilih 'Asal'.")
            cmb_Asal.Focus()
            Return
        End If

        If Lokasi = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Lokasi'.")
            txt_Lokasi.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_StockOpname") + 1

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_StockOpname VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & JenisStok & "', " &
                                  " '" & TanggalFormatSimpan(TanggalPengecekan) & "', " &
                                  " '" & NamaBarang & "', " &
                                  " '" & JumlahBarang & "', " &
                                  " '" & Satuan & "', " &
                                  " '" & HargaSatuan & "', " &
                                  " '" & JumlahHarga & "', " &
                                  " '" & Asal & "', " &
                                  " '" & Lokasi & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & val_Normal & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_StockOpname SET " &
                                  " Jenis_Stok          = '" & JenisStok & "', " &
                                  " Tanggal_Pengecekan  = '" & TanggalFormatSimpan(TanggalPengecekan) & "', " &
                                  " Nama_Barang         = '" & NamaBarang & "', " &
                                  " Jumlah_Barang       = '" & JumlahBarang & "', " &
                                  " Satuan              = '" & Satuan & "', " &
                                  " Harga_Satuan        = '" & HargaSatuan & "', " &
                                  " Jumlah_Harga        = '" & JumlahHarga & "', " &
                                  " Asal                = '" & Asal & "', " &
                                  " Lokasi              = '" & Lokasi & "', " &
                                  " Keterangan          = '" & Keterangan & "', " &
                                  " Jenis_Data          = '" & val_Normal & "', " &
                                  " User                = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID      = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BahanPenolong.StatusAktif Then usc_BahanPenolong.TampilkanData()
            If usc_BahanBaku.StatusAktif Then usc_BahanBaku.TampilkanData()
            If usc_BarangDalamProses_CekFisik.StatusAktif Then usc_BarangDalamProses_CekFisik.TampilkanData()
            If usc_BarangDalamProses_TarikanData.StatusAktif Then usc_BarangDalamProses_TarikanData.TampilkanData()
            If usc_BarangJadi.StatusAktif Then usc_BarangJadi.TampilkanData()
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