Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputStockOpname

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

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Stock Opname " & JenisStok
            dtp_TanggalPengecekan.SelectedDate = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanPengunci_Angka, TahunBukuAktif)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Stock Opname " & JenisStok
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0

        dtp_TanggalPengecekan.IsEnabled = True
        KosongkanDatePicker(dtp_TanggalPengecekan)
        txt_NamaBarang.IsEnabled = True
        txt_NamaBarang.Text = Kosongan
        txt_JumlahBarang.Text = Kosongan
        txt_Satuan.IsEnabled = True
        txt_Satuan.Text = Kosongan
        txt_HargaSatuan.IsEnabled = True
        txt_HargaSatuan.Text = Kosongan
        txt_JumlahHarga.Text = Kosongan
        KontenCombo_Asal()
        txt_Lokasi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_Asal()
        cmb_Asal.Items.Clear()
        cmb_Asal.Items.Add(val_Lokal)
        cmb_Asal.Items.Add(val_Import)
        Select Case JenisStok
            Case JenisStok_BahanPenolong
                VisibilitasComboAsal(True)
                IsiValueComboBypassTerkunci(cmb_Asal, val_Lokal)
            Case JenisStok_BahanBaku
                cmb_Asal.Text = Kosongan
            Case JenisStok_BarangDalamProses
                VisibilitasComboAsal(False)
                cmb_Asal.Text = Kosongan
            Case JenisStok_BarangJadi
                VisibilitasComboAsal(False)
                cmb_Asal.Text = Kosongan
        End Select
    End Sub


    Sub VisibilitasComboAsal(Visibilitas As Boolean)
        lbl_Asal.Visibility = Visibility.Collapsed
        cmb_Asal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_Asal.Visibility = Visibility.Visible
            cmb_Asal.Visibility = Visibility.Visible
        Else
            lbl_Asal.Visibility = Visibility.Collapsed
            cmb_Asal.Visibility = Visibility.Collapsed
        End If
    End Sub




    Private Sub dtp_TanggalPengecekan_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalPengecekan.SelectedDateChanged
        If dtp_TanggalPengecekan.Text <> Kosongan Then
            KunciBulanDanTahun_HarusSama_WPF(dtp_TanggalPengecekan, BulanPengunci_Angka, TahunBukuAktif)
            TanggalPengecekan = dtp_TanggalPengecekan.SelectedDate
        End If
    End Sub


    Private Sub txt_NamaBarang_TextChanged(sender As Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txt_NamaBarang.TextChanged
        NamaBarang = txt_NamaBarang.Text
    End Sub


    Private Sub txt_JumlahBarang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBarang.TextChanged
        JumlahBarang = AmbilAngka(txt_JumlahBarang.Text)
        PerhitunganJumlahHarga()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahBarang_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahBarang.PreviewTextInput
              
    End Sub


    Private Sub txt_Satuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Satuan.TextChanged
        Satuan = txt_Satuan.Text

    End Sub


    Private Sub txt_HargaSatuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HargaSatuan.TextChanged
        HargaSatuan = AmbilAngka(txt_HargaSatuan.Text)
        PerhitunganJumlahHarga()
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_HargaSatuan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_HargaSatuan.PreviewTextInput
              
    End Sub


    Private Sub txt_JumlahHarga_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHarga.TextChanged
        JumlahHarga = AmbilAngka(txt_JumlahHarga.Text)
        PemecahRibuanUntukTextBox_WPF(CType(sender, TextBox))
    End Sub
    Private Sub txt_JumlahHarga_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahHarga.PreviewTextInput
              
    End Sub
    Sub PerhitunganJumlahHarga()
        txt_JumlahHarga.Text = JumlahBarang * HargaSatuan
    End Sub


    Private Sub cmb_Asal_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Asal.SelectionChanged
        Asal = cmb_Asal.SelectedValue
    End Sub


    Private Sub txt_Lokasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Lokasi.TextChanged
        Lokasi = txt_Lokasi.Text
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalPengecekan.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalPengecekan, "Tanggal Pengecekan")
            Return
        End If

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

        If Asal = Kosongan And cmb_Asal.Visibility = Visibility.Visible Then
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


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_JumlahHarga.IsReadOnly = True
        cmb_Asal.IsReadOnly = True
    End Sub

End Class
