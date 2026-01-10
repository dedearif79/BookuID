Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input

Public Class wpfWin_InputGaji

    Public JudulForm
    Public JalurMasuk
    Public FungsiForm
    Public PilihanPPh_Rutin
    Public PilihanPPh_Pesangon
    Public PilihanPPh_PPhDipotongDitunjang = "PPh Dipotong Ditunjang"
    Public PilihanPPh_PPhDitanggung = "PPh Ditanggung"

    Public NomorID
    Dim Bulan
    Dim TanggalTransaksi

    Dim GajiBagianProduksi
    Dim GajiBagianProduksi2
    Dim GajiBagianProduksi3
    Dim GajiBagianProduksi4
    Dim ThrBonusProduksi
    Dim TunjanganPPh21Produksi
    Dim BpjsTkJkkJkmProduksi
    Dim BpjsTkJhtIpProduksi
    Dim BpjsKesehatanProduksi
    Dim AsuransiKaryawanProduksi
    Dim PesangonKaryawanProduksi
    Dim JumlahGajiBagianProduksi
    Dim BpjsTkJhtIpProduksiDibayarKaryawan
    Dim BpjsKesehatanProduksiDibayarKaryawan

    Dim GajiBagianAdministrasi
    Dim GajiBagianAdministrasi2
    Dim GajiBagianAdministrasi3
    Dim GajiBagianAdministrasi4
    Dim ThrBonusAdministrasi
    Dim TunjanganPPh21Administrasi
    Dim BpjsTkJkkJkmAdministrasi
    Dim BpjsTkJhtIpAdministrasi
    Dim BpjsKesehatanAdministrasi
    Dim AsuransiKaryawanAdministrasi
    Dim PesangonKaryawanAdministrasi
    Dim JumlahGajiBagianAdministrasi
    Dim BpjsTkJhtIpAdministrasiDibayarKaryawan
    Dim BpjsKesehatanAdministrasiDibayarKaryawan

    Dim JumlahGajiKotor
    Dim PotonganHutangBpjsKesehatan
    Dim PotonganHutangBpjsKetenagakerjaan
    Dim PotonganHutangKoperasi
    Dim PotonganHutangPPhPasal21Rutin
    Dim PotonganHutangPPhPasal21Pesangon
    Dim PotonganHutangSerikat
    Dim PotonganKasbonKaryawan
    Dim PotonganLainnya
    Dim JumlahPotongan
    Dim JumlahGajiDibayarkan
    Dim PPhDitanggungRutin
    Dim PPhDitanggungPesangon
    Dim PPhDitanggung
    Public NomorJVGaji
    Dim Keterangan

    Dim JumlahPesangon

    Public AdaPPhPesangon As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StyleWindowDialogWPF_Sizable(Me)

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Gaji"
            PilihanPPh_Rutin = Kosongan
            btn_Reset.IsEnabled = True
            grb_Rutin.isenabled = True
            grb_Pesangon.isenabled = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Gaji"
            btn_Reset.IsEnabled = False
            grb_Rutin.isenabled = False
            grb_Pesangon.isenabled = False
            If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang Then rdb_PPhDipotongDitunjangRutin.IsChecked = True
            If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung Then rdb_PPhDitanggungRutin.IsChecked = True
            If PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang Then rdb_PPhDipotongDitunjangPesangon.IsChecked = True
            If PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung Then rdb_PPhDitanggungPesangon.IsChecked = True
            IsiValueForm()
        End If

        If FungsiForm = Kosongan Then
            PesanUntukProgrammer("Tentukan Fungsi Form Dulu..!")
            Me.Close()
        End If

        Title = JudulForm

        Select Case JalurMasuk
            Case Halaman_BUKUPENGAWASANTURUNANGAJI
                KetersediaanKolomTerkaitBpjsKesehatan()
        End Select

        ProsesLoadingForm = False

    End Sub



    Sub IsiValueForm()

    End Sub

    Public Sub ResetForm()

        PilihanPPh_Rutin = Kosongan
        PilihanPPh_Pesangon = Kosongan
        AdaPPhPesangon = False
        KontenCombo_Bulan()
        cmb_Bulan.IsEnabled = True
        dtp_TanggalTransaksi.SelectedDate = New Date(TahunBukuAktif, BulanIni, 24)

        rdb_PPhDipotongDitunjangRutin.IsChecked = False
        rdb_PPhDitanggungRutin.IsChecked = False
        rdb_PPhDipotongDitunjangPesangon.IsChecked = False
        rdb_PPhDitanggungPesangon.IsChecked = False

        txt_GajiBagianProduksi.Text = Kosongan
        txt_GajiBagianProduksi2.Text = Kosongan
        txt_GajiBagianProduksi3.Text = Kosongan
        txt_GajiBagianProduksi4.Text = Kosongan
        txt_ThrBonusProduksi.Text = Kosongan
        txt_TunjanganPPh21Produksi.Text = Kosongan
        txt_BpjsTkJkkJkmProduksi.Text = Kosongan
        txt_BpjsTkJhtIpProduksi.Text = Kosongan
        txt_BpjsKesehatanProduksi.Text = Kosongan
        txt_AsuransiKaryawanProduksi.Text = Kosongan
        txt_PesangonKaryawanProduksi.Text = Kosongan
        txt_JumlahGajiBagianProduksi.Text = Kosongan
        txt_BpjsTkJhtIpProduksiDibayarKaryawan.Text = Kosongan
        txt_BpjsKesehatanProduksiDibayarKaryawan.Text = Kosongan

        txt_GajiBagianAdministrasi.Text = Kosongan
        txt_GajiBagianAdministrasi2.Text = Kosongan
        txt_GajiBagianAdministrasi3.Text = Kosongan
        txt_GajiBagianAdministrasi4.Text = Kosongan
        txt_ThrBonusAdministrasi.Text = Kosongan
        txt_TunjanganPPh21Administrasi.Text = Kosongan
        txt_BpjsTkJkkJkmAdministrasi.Text = Kosongan
        txt_BpjsTkJhtIpAdministrasi.Text = Kosongan
        txt_BpjsKesehatanAdministrasi.Text = Kosongan
        txt_AsuransiKaryawanAdministrasi.Text = Kosongan
        txt_PesangonKaryawanAdministrasi.Text = Kosongan
        txt_JumlahGajiBagianAdministrasi.Text = Kosongan
        txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text = Kosongan
        txt_BpjsKesehatanAdministrasiDibayarKaryawan.Text = Kosongan

        txt_PotonganHutangBpjsKesehatan.Text = Kosongan
        txt_PotonganHutangBpjsKetenagakerjaan.Text = Kosongan
        txt_PotonganHutangKoperasi.Text = Kosongan
        txt_PotonganHutangPPhPasal21Rutin.Text = Kosongan
        txt_PotonganHutangPPhPasal21Pesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Collapsed
        txt_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Collapsed
        lbl_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
        txt_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
        txt_PotonganHutangSerikat.Text = Kosongan
        txt_PotonganKasbonKaryawan.Text = Kosongan
        txt_PotonganLainnya.Text = Kosongan
        txt_JumlahPotongan.Text = Kosongan
        txt_JumlahGajiDibayarkan.Text = Kosongan
        txt_PPhDitanggungRutin.Text = Kosongan
        txt_PPhDitanggungPesangon.Text = Kosongan
        lbl_PPhDitanggungRutin.Visibility = Visibility.Collapsed
        txt_PPhDitanggungRutin.Visibility = Visibility.Collapsed
        lbl_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
        txt_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        BukaSemuaKolom()
        VisibilitasKolomKolom()

    End Sub


    Sub KontenCombo_Bulan()
        cmb_Bulan.Items.Clear()
        cmb_Bulan.Items.Add(Bulan_Januari)
        cmb_Bulan.Items.Add(Bulan_Februari)
        cmb_Bulan.Items.Add(Bulan_Maret)
        cmb_Bulan.Items.Add(Bulan_April)
        cmb_Bulan.Items.Add(Bulan_Mei)
        cmb_Bulan.Items.Add(Bulan_Juni)
        cmb_Bulan.Items.Add(Bulan_Juli)
        cmb_Bulan.Items.Add(Bulan_Agustus)
        cmb_Bulan.Items.Add(Bulan_September)
        cmb_Bulan.Items.Add(Bulan_Oktober)
        cmb_Bulan.Items.Add(Bulan_Nopember)
        cmb_Bulan.Items.Add(Bulan_Desember)
        cmb_Bulan.Text = Kosongan
    End Sub


    Sub BukaSemuaKolom()
        cmb_Bulan.IsEnabled = True
        dtp_TanggalTransaksi.IsEnabled = True
        txt_GajiBagianProduksi.IsEnabled = True
        txt_GajiBagianProduksi2.IsEnabled = True
        txt_GajiBagianProduksi3.IsEnabled = True
        txt_GajiBagianProduksi4.IsEnabled = True
        txt_ThrBonusProduksi.IsEnabled = True
        txt_JumlahGajiBagianProduksi.IsEnabled = True
        txt_GajiBagianAdministrasi.IsEnabled = True
        txt_GajiBagianAdministrasi2.IsEnabled = True
        txt_GajiBagianAdministrasi3.IsEnabled = True
        txt_GajiBagianAdministrasi4.IsEnabled = True
        txt_ThrBonusAdministrasi.IsEnabled = True
        txt_JumlahGajiBagianAdministrasi.IsEnabled = True
        txt_PotonganHutangBpjsKesehatan.IsEnabled = True
        txt_PotonganHutangBpjsKetenagakerjaan.IsEnabled = True
        txt_PotonganHutangKoperasi.IsEnabled = True
        If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang Then txt_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Visible
        txt_PotonganHutangSerikat.IsEnabled = True
        txt_PotonganKasbonKaryawan.IsEnabled = True
        txt_PotonganLainnya.IsEnabled = True
        txt_JumlahPotongan.IsEnabled = True
        txt_JumlahGajiDibayarkan.IsEnabled = True
        If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung Then txt_PPhDitanggungRutin.Visibility = Visibility.Visible
    End Sub

    Sub TutupSemuaKolom()
        cmb_Bulan.IsEnabled = False
        dtp_TanggalTransaksi.IsEnabled = False
        txt_GajiBagianProduksi.IsEnabled = False
        txt_GajiBagianProduksi2.IsEnabled = False
        txt_GajiBagianProduksi3.IsEnabled = False
        txt_GajiBagianProduksi4.IsEnabled = False
        txt_ThrBonusProduksi.IsEnabled = False
        txt_JumlahGajiBagianProduksi.IsEnabled = False
        txt_GajiBagianAdministrasi.IsEnabled = False
        txt_GajiBagianAdministrasi2.IsEnabled = False
        txt_GajiBagianAdministrasi3.IsEnabled = False
        txt_GajiBagianAdministrasi4.IsEnabled = False
        txt_ThrBonusAdministrasi.IsEnabled = False
        txt_JumlahGajiBagianAdministrasi.IsEnabled = False
        txt_PotonganHutangBpjsKesehatan.IsEnabled = False
        txt_PotonganHutangBpjsKetenagakerjaan.IsEnabled = False
        txt_PotonganHutangKoperasi.IsEnabled = False
        txt_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Collapsed
        txt_PotonganHutangSerikat.IsEnabled = False
        txt_PotonganKasbonKaryawan.IsEnabled = False
        txt_PotonganLainnya.IsEnabled = False
        txt_JumlahPotongan.IsEnabled = False
        txt_JumlahGajiDibayarkan.IsEnabled = False
        txt_PPhDitanggungRutin.Visibility = Visibility.Collapsed
    End Sub

    Public Sub KetersediaanKolomTerkaitBpjsKesehatan()
        TutupSemuaKolom()
        dtp_TanggalTransaksi.IsEnabled = True
        txt_PotonganHutangBpjsKesehatan.IsEnabled = True
        txt_Keterangan.IsEnabled = True
    End Sub

    Sub KetersediaanKolomTerkaitBpjsKetenagakerjaan()
        TutupSemuaKolom()
        dtp_TanggalTransaksi.IsEnabled = True
        txt_PotonganHutangBpjsKetenagakerjaan.IsEnabled = True
        txt_Keterangan.IsEnabled = True
    End Sub


    Sub VisibilitasKolomBerdasarkanCOA(Label As TextBlock, Teks As TextBox, COA As String)

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                Label.Visibility = Visibility.Visible
                Teks.Visibility = Visibility.Visible
            Else
                Label.Visibility = Visibility.Collapsed
                Teks.Visibility = Visibility.Collapsed
            End If
            If Not (COA = KodeTautanCOA_HutangPPhPasal21_100 Or COA = KodeTautanCOA_HutangPPhPasal21_401) Then
                '(PPh 21 Rutin dan PPh 21 Pesangon, tidak mengambil value Nama Akun dari Data COA)
                Label.Text = dr.Item("Nama_Akun")
            End If
        Else
            Label.Visibility = Visibility.Collapsed
            Teks.Visibility = Visibility.Collapsed
        End If

    End Sub

    Sub VisibilitasKolomKolom()

        AksesDatabase_General(Buka)

        'Bagian Produksi : -----------------------------------------------------------------------------------------------------------------------
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianProduksi, txt_GajiBagianProduksi, KodeTautanCOA_BiayaGajiProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianProduksi2, txt_GajiBagianProduksi2, KodeTautanCOA_BiayaGajiProduksi2)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianProduksi3, txt_GajiBagianProduksi3, KodeTautanCOA_BiayaGajiProduksi3)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianProduksi4, txt_GajiBagianProduksi4, KodeTautanCOA_BiayaGajiProduksi4)
        VisibilitasKolomBerdasarkanCOA(lbl_ThrBonusProduksi, txt_ThrBonusProduksi, KodeTautanCOA_BiayaThrBonusProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_TunjanganPPh21Produksi, txt_TunjanganPPh21Produksi, KodeTautanCOA_BiayaTunjanganPPh21Produksi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsTkJkkJkmProduksi, txt_BpjsTkJkkJkmProduksi, KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsTkJhtIpProduksi, txt_BpjsTkJhtIpProduksi, KodeTautanCOA_BiayaBpjsTkJhtIpProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsKesehatanProduksi, txt_BpjsKesehatanProduksi, KodeTautanCOA_BiayaBpjsKesehatanProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_AsuransiKaryawanProduksi, txt_AsuransiKaryawanProduksi, KodeTautanCOA_BiayaAsuransiKaryawanProduksi)
        VisibilitasKolomBerdasarkanCOA(lbl_PesangonKaryawanProduksi, txt_PesangonKaryawanProduksi, KodeTautanCOA_BiayaPesangonKaryawanProduksi)

        'Bagian Administrasi : -------------------------------------------------------------------------------------------------------------------
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianAdministrasi, txt_GajiBagianAdministrasi, KodeTautanCOA_BiayaGajiAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianAdministrasi2, txt_GajiBagianAdministrasi2, KodeTautanCOA_BiayaGajiAdministrasi2)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianAdministrasi3, txt_GajiBagianAdministrasi3, KodeTautanCOA_BiayaGajiAdministrasi3)
        VisibilitasKolomBerdasarkanCOA(lbl_GajiBagianAdministrasi4, txt_GajiBagianAdministrasi4, KodeTautanCOA_BiayaGajiAdministrasi4)
        VisibilitasKolomBerdasarkanCOA(lbl_ThrBonusAdministrasi, txt_ThrBonusAdministrasi, KodeTautanCOA_BiayaThrBonusAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_TunjanganPPh21Administrasi, txt_TunjanganPPh21Administrasi, KodeTautanCOA_BiayaTunjanganPPh21Administrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsTkJkkJkmAdministrasi, txt_BpjsTkJkkJkmAdministrasi, KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsTkJhtIpAdministrasi, txt_BpjsTkJhtIpAdministrasi, KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_BpjsKesehatanAdministrasi, txt_BpjsKesehatanAdministrasi, KodeTautanCOA_BiayaBpjsKesehatanAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_AsuransiKaryawanAdministrasi, txt_AsuransiKaryawanAdministrasi, KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi)
        VisibilitasKolomBerdasarkanCOA(lbl_PesangonKaryawanAdministrasi, txt_PesangonKaryawanAdministrasi, KodeTautanCOA_BiayaPesangonKaryawanAdministrasi)

        'Potongan : ------------------------------------------------------------------------------------------------------------------------------
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangBpjsKesehatan, txt_PotonganHutangBpjsKesehatan, KodeTautanCOA_HutangBpjsKesehatan)
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangBpjsKetenagakerjaan, txt_PotonganHutangBpjsKetenagakerjaan, KodeTautanCOA_HutangBpjsKetenagakerjaan)
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangKoperasi, txt_PotonganHutangKoperasi, KodeTautanCOA_HutangKoperasiKaryawan)
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangPPhPasal21Rutin, txt_PotonganHutangPPhPasal21Rutin, KodeTautanCOA_HutangPPhPasal21_100)          'Tidak mengambil value Nama Akun
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangPPhPasal21Pesangon, txt_PotonganHutangPPhPasal21Pesangon, KodeTautanCOA_HutangPPhPasal21_401)    'Tidak mengambil value Nama Akun
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganHutangSerikat, txt_PotonganHutangSerikat, KodeTautanCOA_HutangSerikat)
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganKasbonKaryawan, txt_PotonganKasbonKaryawan, KodeTautanCOA_PiutangKaryawan)
        VisibilitasKolomBerdasarkanCOA(lbl_PotonganLainnya, txt_PotonganLainnya, KodeTautanCOA_HutangLancarLainnya)

        AksesDatabase_General(Tutup)

    End Sub



    Public Sub SistemPenomoranOtomatis_ID_Gaji()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT Nomor_ID FROM tbl_PengawasanGaji WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_PengawasanGaji) ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            NomorID = 1
        Else
            NomorID = dr.Item("Nomor_ID") + 1
        End If
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Sub Perhitungan_JumlahGajiBagianProduksi()
        txt_JumlahGajiBagianProduksi.Text _
            = GajiBagianProduksi _
            + GajiBagianProduksi2 _
            + GajiBagianProduksi3 _
            + GajiBagianProduksi4 _
            + ThrBonusProduksi _
            + TunjanganPPh21Produksi _
            + BpjsTkJkkJkmProduksi _
            + BpjsTkJhtIpProduksi _
            + BpjsKesehatanProduksi _
            + AsuransiKaryawanProduksi _
            + PesangonKaryawanProduksi
    End Sub

    Sub Perhitungan_JumlahGajiBagianAdministrasi()
        txt_JumlahGajiBagianAdministrasi.Text _
            = GajiBagianAdministrasi _
            + GajiBagianAdministrasi2 _
            + GajiBagianAdministrasi3 _
            + GajiBagianAdministrasi4 _
            + ThrBonusAdministrasi _
            + TunjanganPPh21Administrasi _
            + BpjsTkJkkJkmAdministrasi _
            + BpjsTkJhtIpAdministrasi _
            + BpjsKesehatanAdministrasi _
            + AsuransiKaryawanAdministrasi _
            + PesangonKaryawanAdministrasi
    End Sub

    Sub PerhitunganHutangBpjsKesehatan()
        txt_PotonganHutangBpjsKesehatan.Text = BpjsKesehatanProduksi + BpjsKesehatanAdministrasi + BpjsKesehatanAdministrasiDibayarKaryawan + BpjsKesehatanProduksiDibayarKaryawan
    End Sub

    Sub PerhitunganHutangBpjsKetenagakerjaan()
        txt_PotonganHutangBpjsKetenagakerjaan.Text _
            = BpjsTkJkkJkmProduksi _
            + BpjsTkJhtIpProduksi _
            + BpjsTkJkkJkmAdministrasi _
            + BpjsTkJhtIpAdministrasi _
            + BpjsTkJhtIpProduksiDibayarKaryawan _
            + BpjsTkJhtIpAdministrasiDibayarKaryawan
    End Sub

    Sub Perhitungan_JumlahGajiKotor()
        JumlahGajiKotor _
            = JumlahGajiBagianProduksi _
            + JumlahGajiBagianAdministrasi
        Perhitungan_JumlahGajiDibayarkan()
    End Sub

    Sub Perhitungan_PPhPesangon()

        JumlahPesangon = PesangonKaryawanProduksi + PesangonKaryawanAdministrasi

        If JumlahPesangon > 50000000 Then
            If FungsiForm = FungsiForm_TAMBAH Then
                If TanyaKonfirmasi("Apakah ada PPh untuk Pesangon?") Then
                    AdaPPhPesangon = True
                    grb_Pesangon.IsEnabled = True
                Else
                    AdaPPhPesangon = False
                    grb_Pesangon.IsEnabled = False
                    rdb_PPhDipotongDitunjangPesangon.IsChecked = False
                    rdb_PPhDitanggungPesangon.IsChecked = False
                    lbl_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
                    txt_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
                    lbl_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
                    txt_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
                End If
            End If
        Else
            AdaPPhPesangon = False
            grb_Pesangon.IsEnabled = False
            rdb_PPhDipotongDitunjangPesangon.IsChecked = False
            rdb_PPhDitanggungPesangon.IsChecked = False
            lbl_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
            txt_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
            lbl_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
            txt_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
        End If

        Dim HutangPPhPasal21Pesangon As Int64

        Dim Batasan_50Juta = 50000000
        Dim Batasan_100Juta = 100000000
        Dim Batasan_500Juta = 500000000

        Dim Tarif_5Persen = 5 / 100
        Dim Tarif_15Persen = 15 / 100
        Dim Tarif_25Persen = 25 / 100

        Dim JumlahPPh_Tarif5 As Int64 = 0
        Dim JumlahPPh_Tarif15 As Int64 = 0
        Dim JumlahPPh_Tarif25 As Int64 = 0

        Dim SisaDasarPengenaan As Int64

        If JumlahPesangon <= Batasan_50Juta Then
            '<= 50 Juta :
            HutangPPhPasal21Pesangon = 0
        ElseIf JumlahPesangon <= Batasan_100Juta Then
            '50 - 100 Juta :
            SisaDasarPengenaan = JumlahPesangon - Batasan_50Juta
            JumlahPPh_Tarif5 = SisaDasarPengenaan * Tarif_5Persen
        ElseIf JumlahPesangon <= Batasan_500Juta Then
            '100 - 500 Juta :
            JumlahPPh_Tarif5 = Batasan_50Juta * Tarif_5Persen
            SisaDasarPengenaan = JumlahPesangon - Batasan_100Juta
            JumlahPPh_Tarif15 = SisaDasarPengenaan * Tarif_15Persen
        Else
            '> 500 Juta :
            JumlahPPh_Tarif5 = Batasan_50Juta * Tarif_5Persen
            JumlahPPh_Tarif15 = (Batasan_500Juta - Batasan_100Juta) * Tarif_15Persen
            SisaDasarPengenaan = JumlahPesangon - (Batasan_500Juta)
            JumlahPPh_Tarif25 = SisaDasarPengenaan * Tarif_25Persen
        End If

        HutangPPhPasal21Pesangon = JumlahPPh_Tarif5 + JumlahPPh_Tarif15 + JumlahPPh_Tarif25

        'txt_PotonganHutangPPhPasal21Pesangon.Text = HutangPPhPasal21Pesangon '(Sementara ini belum dibutuhkan perhitungan, jadi kode ini tidak dieksekusi)

    End Sub

    Sub Perhitungan_JumlahPotongan()
        txt_JumlahPotongan.Text _
            = PotonganHutangBpjsKesehatan _
            + PotonganHutangBpjsKetenagakerjaan _
            + PotonganHutangKoperasi _
            + PotonganHutangPPhPasal21Rutin _
            + PotonganHutangPPhPasal21Pesangon _
            + PotonganHutangSerikat _
            + PotonganKasbonKaryawan _
            + PotonganLainnya
    End Sub

    Sub Perhitungan_JumlahGajiDibayarkan()
        txt_JumlahGajiDibayarkan.Text = JumlahGajiKotor - JumlahPotongan
    End Sub



    Private Sub cmb_Bulan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Bulan.SelectionChanged
        Bulan = cmb_Bulan.SelectedValue
        Dim BulanAngka As Integer = 0 'Harus ada angka 0 sebagai value default jika bulan belum terpilih.
        If Bulan = Bulan_Januari Then BulanAngka = 1
        If Bulan = Bulan_Februari Then BulanAngka = 2
        If Bulan = Bulan_Maret Then BulanAngka = 3
        If Bulan = Bulan_April Then BulanAngka = 4
        If Bulan = Bulan_Mei Then BulanAngka = 5
        If Bulan = Bulan_Juni Then BulanAngka = 6
        If Bulan = Bulan_Juli Then BulanAngka = 7
        If Bulan = Bulan_Agustus Then BulanAngka = 8
        If Bulan = Bulan_September Then BulanAngka = 9
        If Bulan = Bulan_Oktober Then BulanAngka = 10
        If Bulan = Bulan_Nopember Then BulanAngka = 11
        If Bulan = Bulan_Desember Then BulanAngka = 12
        If BulanAngka = 0 Then BulanAngka = BulanIni
        dtp_TanggalTransaksi.SelectedDate = New Date(TahunBukuAktif, BulanAngka, 24)
    End Sub


    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = dtp_TanggalTransaksi.SelectedDate
        End If
    End Sub


    Private Sub rdb_PPhDipotongDitunjangRutin_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDipotongDitunjangRutin.Checked
        SusunanForm_PPhDipotongDitunjang_Rutin()
    End Sub
    Private Sub rdb_PPhDipotongDitunjangRutin_UnChecked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDipotongDitunjangRutin.Unchecked
    End Sub
    Sub SusunanForm_PPhDipotongDitunjang_Rutin()
        PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang
        txt_PPhDitanggungRutin.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Visible
        txt_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Visible
        lbl_PPhDitanggungRutin.Visibility = Visibility.Collapsed
        txt_PPhDitanggungRutin.Visibility = Visibility.Collapsed
    End Sub


    Private Sub rdb_PPhDitanggungRutin_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDitanggungRutin.Checked
        SusunanForm_PPhDitanggung_Rutin()
    End Sub
    Private Sub rdb_PPhDitanggungRutin_UnChecked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDitanggungRutin.Unchecked
    End Sub
    Sub SusunanForm_PPhDitanggung_Rutin()
        PilihanPPh_Rutin = PilihanPPh_PPhDitanggung
        txt_PotonganHutangPPhPasal21Rutin.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Collapsed
        txt_PotonganHutangPPhPasal21Rutin.Visibility = Visibility.Collapsed
        lbl_PPhDitanggungRutin.Visibility = Visibility.Visible
        txt_PPhDitanggungRutin.Visibility = Visibility.Visible
    End Sub


    Private Sub rdb_PPhDipotongDitunjangPesangon_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDipotongDitunjangPesangon.Checked
        SusunanForm_PPhDipotongDitunjang_Pesangon()
    End Sub
    Private Sub rdb_PPhDipotongDitunjangPesangon_UnChecked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDipotongDitunjangPesangon.Unchecked
    End Sub
    Sub SusunanForm_PPhDipotongDitunjang_Pesangon()
        PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang
        txt_PPhDitanggungPesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Visible
        txt_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Visible
        lbl_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
        txt_PPhDitanggungPesangon.Visibility = Visibility.Collapsed
    End Sub


    Private Sub rdb_PPhDitanggungPesangon_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDitanggungPesangon.Checked
        SusunanForm_PPhDitanggung_Pesangon()
    End Sub
    Private Sub rdb_PPhDitanggungPesangon_UnChecked(sender As Object, e As RoutedEventArgs) Handles rdb_PPhDitanggungPesangon.Unchecked
    End Sub
    Sub SusunanForm_PPhDitanggung_Pesangon()
        PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung
        txt_PotonganHutangPPhPasal21Pesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
        txt_PotonganHutangPPhPasal21Pesangon.Visibility = Visibility.Collapsed
        lbl_PPhDitanggungPesangon.visibility = visibility.visible
        txt_PPhDitanggungPesangon.Visibility = Visibility.Visible
    End Sub


    '---------------------- Bagian Produksi ----------------------
    Private Sub txt_GajiBagianProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianProduksi.TextChanged
        GajiBagianProduksi = AmbilAngka(txt_GajiBagianProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianProduksi)
    End Sub
    Private Sub txt_GajiBagianProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianProduksi.PreviewTextInput
    End Sub


    Private Sub txt_GajiBagianProduksi2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianProduksi2.TextChanged
        GajiBagianProduksi2 = AmbilAngka(txt_GajiBagianProduksi2.Text)
        Perhitungan_JumlahGajiBagianproduksi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianProduksi2)
    End Sub
    Private Sub txt_GajiBagianProduksi2_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianProduksi2.PreviewTextInput
    End Sub


    Private Sub txt_GajiBagianProduksi3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianProduksi3.TextChanged
        GajiBagianProduksi3 = AmbilAngka(txt_GajiBagianProduksi3.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianProduksi3)
    End Sub
    Private Sub txt_GajiBagianProduksi3_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianProduksi3.PreviewTextInput

    End Sub


    Private Sub txt_GajiBagianProduksi4_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianProduksi4.TextChanged
        GajiBagianProduksi4 = AmbilAngka(txt_GajiBagianProduksi4.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianProduksi4)
    End Sub
    Private Sub txt_GajiBagianProduksi4_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianProduksi4.PreviewTextInput
    End Sub


    Private Sub txt_ThrBonusProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_ThrBonusProduksi.TextChanged
        ThrBonusProduksi = AmbilAngka(txt_ThrBonusProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_ThrBonusProduksi)
    End Sub
    Private Sub txt_ThrBonusProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_ThrBonusProduksi.PreviewTextInput
    End Sub


    Private Sub txt_TunjanganPPh21Produksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TunjanganPPh21Produksi.TextChanged
        TunjanganPPh21Produksi = AmbilAngka(txt_TunjanganPPh21Produksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_TunjanganPPh21Produksi)
    End Sub
    Private Sub txt_TunjanganPPh21Produksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TunjanganPPh21Produksi.PreviewTextInput
    End Sub


    Private Sub txt_BpjsTkJkkJkmProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJkkJkmProduksi.TextChanged
        BpjsTkJkkJkmProduksi = AmbilAngka(txt_BpjsTkJkkJkmProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJkkJkmProduksi)
    End Sub
    Private Sub txt_BpjsTkJkkJkmProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJkkJkmProduksi.PreviewTextInput
    End Sub


    Private Sub txt_BpjsTkJhtIpProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJhtIpProduksi.TextChanged
        BpjsTkJhtIpProduksi = AmbilAngka(txt_BpjsTkJhtIpProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJhtIpProduksi)
    End Sub
    Private Sub txt_BpjsTkJhtIpProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJhtIpProduksi.PreviewTextInput
    End Sub


    Private Sub txt_BpjsKesehatanProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsKesehatanProduksi.TextChanged
        BpjsKesehatanProduksi = AmbilAngka(txt_BpjsKesehatanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsKesehatanProduksi)
    End Sub
    Private Sub txt_BpjsKesehatanProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsKesehatanProduksi.PreviewTextInput
    End Sub


    Private Sub txt_AsuransiKaryawanProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AsuransiKaryawanProduksi.TextChanged
        AsuransiKaryawanProduksi = AmbilAngka(txt_AsuransiKaryawanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_AsuransiKaryawanProduksi)
    End Sub
    Private Sub txt_AsuransiKaryawanProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_AsuransiKaryawanProduksi.PreviewTextInput
    End Sub


    Private Sub txt_PesangonKaryawanProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PesangonKaryawanProduksi.TextChanged
        PesangonKaryawanProduksi = AmbilAngka(txt_PesangonKaryawanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox_WPF(txt_PesangonKaryawanProduksi)
    End Sub
    Private Sub txt_PesangonKaryawanProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PesangonKaryawanProduksi.PreviewTextInput
    End Sub
    Private Sub txt_PesangonKaryawanProduksi_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_PesangonKaryawanProduksi.LostFocus
        Perhitungan_PPhPesangon()
    End Sub


    Private Sub txt_JumlahGajiBagianProduksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahGajiBagianProduksi.TextChanged
        JumlahGajiBagianProduksi = AmbilAngka(txt_JumlahGajiBagianProduksi.Text)
        Perhitungan_JumlahGajiKotor()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahGajiBagianProduksi)
    End Sub
    Private Sub txt_JumlahGajiBagianProduksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahGajiBagianProduksi.PreviewTextInput
    End Sub

    Private Sub txt_BpjsTkJhtIpProduksiDibayarKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJhtIpProduksiDibayarKaryawan.TextChanged
        BpjsTkJhtIpProduksiDibayarKaryawan = AmbilAngka(txt_BpjsTkJhtIpProduksiDibayarKaryawan.Text)
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJhtIpProduksiDibayarKaryawan)
    End Sub
    Private Sub txt_BpjsTkJhtIpProduksiDibayarKaryawan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJhtIpProduksiDibayarKaryawan.PreviewTextInput
    End Sub

    Private Sub txt_BpjsKesehatanProduksiDibayarKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsKesehatanProduksiDibayarKaryawan.TextChanged
        BpjsKesehatanProduksiDibayarKaryawan = AmbilAngka(txt_BpjsKesehatanProduksiDibayarKaryawan.Text)
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsKesehatanProduksiDibayarKaryawan)
    End Sub
    Private Sub txt_BpjsKesehatanProduksiDibayarKaryawan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsKesehatanProduksiDibayarKaryawan.PreviewTextInput
    End Sub


    '---------------------- Bagian Administrasi ----------------------
    Private Sub txt_GajiBagianAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianAdministrasi.TextChanged
        GajiBagianAdministrasi = AmbilAngka(txt_GajiBagianAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianAdministrasi)
    End Sub
    Private Sub txt_GajiBagianAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianAdministrasi.PreviewTextInput
    End Sub


    Private Sub txt_GajiBagianAdministrasi2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianAdministrasi2.TextChanged
        GajiBagianAdministrasi2 = AmbilAngka(txt_GajiBagianAdministrasi2.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianAdministrasi2)
    End Sub
    Private Sub txt_GajiBagianAdministrasi2_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianAdministrasi2.PreviewTextInput
    End Sub


    Private Sub txt_GajiBagianAdministrasi3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianAdministrasi3.TextChanged
        GajiBagianAdministrasi3 = AmbilAngka(txt_GajiBagianAdministrasi3.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianAdministrasi3)
    End Sub
    Private Sub txt_GajiBagianAdministrasi3_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianAdministrasi3.PreviewTextInput
    End Sub


    Private Sub txt_GajiBagianAdministrasi4_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_GajiBagianAdministrasi4.TextChanged
        GajiBagianAdministrasi4 = AmbilAngka(txt_GajiBagianAdministrasi4.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_GajiBagianAdministrasi4)
    End Sub
    Private Sub txt_GajiBagianAdministrasi4_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_GajiBagianAdministrasi4.PreviewTextInput
    End Sub


    Private Sub txt_ThrBonusAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_ThrBonusAdministrasi.TextChanged
        ThrBonusAdministrasi = AmbilAngka(txt_ThrBonusAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_ThrBonusAdministrasi)
    End Sub
    Private Sub txt_ThrBonusAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_ThrBonusAdministrasi.PreviewTextInput
    End Sub


    Private Sub txt_TunjanganPPh21Administrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TunjanganPPh21Administrasi.TextChanged
        TunjanganPPh21Administrasi = AmbilAngka(txt_TunjanganPPh21Administrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_TunjanganPPh21Administrasi)
    End Sub
    Private Sub txt_TunjanganPPh21Administrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TunjanganPPh21Administrasi.PreviewTextInput
    End Sub


    Private Sub txt_BpjsTkJkkJkmAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJkkJkmAdministrasi.TextChanged
        BpjsTkJkkJkmAdministrasi = AmbilAngka(txt_BpjsTkJkkJkmAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJkkJkmAdministrasi)
    End Sub
    Private Sub txt_BpjsTkJkkJkmAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJkkJkmAdministrasi.PreviewTextInput
    End Sub


    Private Sub txt_BpjsTkJhtIpAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJhtIpAdministrasi.TextChanged
        BpjsTkJhtIpAdministrasi = AmbilAngka(txt_BpjsTkJhtIpAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJhtIpAdministrasi)
    End Sub
    Private Sub txt_BpjsTkJhtIpAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJhtIpAdministrasi.PreviewTextInput

    End Sub


    Private Sub txt_BpjsKesehatanAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsKesehatanAdministrasi.TextChanged
        BpjsKesehatanAdministrasi = AmbilAngka(txt_BpjsKesehatanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsKesehatanAdministrasi)
    End Sub
    Private Sub txt_BpjsKesehatanAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsKesehatanAdministrasi.PreviewTextInput

    End Sub


    Private Sub txt_AsuransiKaryawanAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AsuransiKaryawanAdministrasi.TextChanged
        AsuransiKaryawanAdministrasi = AmbilAngka(txt_AsuransiKaryawanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_AsuransiKaryawanAdministrasi)
    End Sub
    Private Sub txt_AsuransiKaryawanAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_AsuransiKaryawanAdministrasi.PreviewTextInput

    End Sub


    Private Sub txt_PesangonKaryawanAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PesangonKaryawanAdministrasi.TextChanged
        PesangonKaryawanAdministrasi = AmbilAngka(txt_PesangonKaryawanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox_WPF(txt_PesangonKaryawanAdministrasi)
    End Sub
    Private Sub txt_PesangonKaryawanAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PesangonKaryawanAdministrasi.PreviewTextInput

    End Sub
    Private Sub txt_PesangonKaryawanAdministrasi_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_PesangonKaryawanAdministrasi.LostFocus
        Perhitungan_PPhPesangon()
    End Sub


    Private Sub txt_JumlahGajiBagianAdministrasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahGajiBagianAdministrasi.TextChanged
        JumlahGajiBagianAdministrasi = AmbilAngka(txt_JumlahGajiBagianAdministrasi.Text)
        Perhitungan_JumlahGajiKotor()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahGajiBagianAdministrasi)
    End Sub
    Private Sub txt_JumlahGajiBagianAdministrasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahGajiBagianAdministrasi.PreviewTextInput

    End Sub

    Private Sub txt_BpjsTkJhtIpAdministrasiDibayarKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.TextChanged
        BpjsTkJhtIpAdministrasiDibayarKaryawan = AmbilAngka(txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text)
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsTkJhtIpAdministrasiDibayarKaryawan)
    End Sub
    Private Sub txt_BpjsTkJhtIpAdministrasiDibayarKaryawan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.PreviewTextInput
    End Sub

    Private Sub txt_BpjsKesehatanAdministrasiDibayarKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BpjsKesehatanAdministrasiDibayarKaryawan.TextChanged
        BpjsKesehatanAdministrasiDibayarKaryawan = AmbilAngka(txt_BpjsKesehatanAdministrasiDibayarKaryawan.Text)
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox_WPF(txt_BpjsKesehatanAdministrasiDibayarKaryawan)
    End Sub
    Private Sub txt_BpjsKesehatanAdministrasiDibayarKaryawan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_BpjsKesehatanAdministrasiDibayarKaryawan.PreviewTextInput
    End Sub


    Private Sub txt_PotonganHutangBpjsKesehatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangBpjsKesehatan.TextChanged
        PotonganHutangBpjsKesehatan = AmbilAngka(txt_PotonganHutangBpjsKesehatan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangBpjsKesehatan)
    End Sub
    Private Sub txt_PotonganHutangBpjsKesehatan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangBpjsKesehatan.PreviewTextInput

    End Sub


    Private Sub txt_PotonganHutangBpjsKetenagakerjaan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangBpjsKetenagakerjaan.TextChanged
        PotonganHutangBpjsKetenagakerjaan = AmbilAngka(txt_PotonganHutangBpjsKetenagakerjaan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangBpjsKetenagakerjaan)
    End Sub
    Private Sub txt_PotonganHutangBpjsKetenagakerjaan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangBpjsKetenagakerjaan.PreviewTextInput

    End Sub


    Private Sub txt_PotonganHutangKoperasi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangKoperasi.TextChanged
        PotonganHutangKoperasi = AmbilAngka(txt_PotonganHutangKoperasi.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangKoperasi)
    End Sub
    Private Sub txt_PotonganHutangKoperasi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangKoperasi.PreviewTextInput

    End Sub


    Private Sub txt_PotonganHutangPPhPasal21Rutin_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangPPhPasal21Rutin.TextChanged
        PotonganHutangPPhPasal21Rutin = AmbilAngka(txt_PotonganHutangPPhPasal21Rutin.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangPPhPasal21Rutin)
    End Sub
    Private Sub txt_PotonganHutangPPhPasal21Rutin_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangPPhPasal21Rutin.PreviewTextInput

    End Sub


    Private Sub txt_PotonganHutangPPhPasal21Pesangon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangPPhPasal21Pesangon.TextChanged
        PotonganHutangPPhPasal21Pesangon = AmbilAngka(txt_PotonganHutangPPhPasal21Pesangon.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangPPhPasal21Pesangon)
    End Sub
    Private Sub txt_PotonganHutangPPhPasal21Pesangon_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangPPhPasal21Pesangon.PreviewTextInput

    End Sub


    Private Sub txt_PotonganHutangSerikat_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganHutangSerikat.TextChanged
        PotonganHutangSerikat = AmbilAngka(txt_PotonganHutangSerikat.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganHutangSerikat)
    End Sub
    Private Sub txt_PotonganHutangSerikat_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganHutangSerikat.PreviewTextInput

    End Sub


    Private Sub txt_PotonganKasbonKaryawan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganKasbonKaryawan.TextChanged
        PotonganKasbonKaryawan = AmbilAngka(txt_PotonganKasbonKaryawan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganKasbonKaryawan)
    End Sub
    Private Sub txt_PotonganKasbonKaryawan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganKasbonKaryawan.PreviewTextInput

    End Sub


    Private Sub txt_PotonganLainnya_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PotonganLainnya.TextChanged
        PotonganLainnya = AmbilAngka(txt_PotonganLainnya.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox_WPF(txt_PotonganLainnya)
    End Sub
    Private Sub txt_PotonganLainnya_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PotonganLainnya.PreviewTextInput

    End Sub


    Private Sub txt_JumlahPotongan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPotongan.TextChanged
        JumlahPotongan = AmbilAngka(txt_JumlahPotongan.Text)
        Perhitungan_JumlahGajiDibayarkan()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahPotongan)
    End Sub
    Private Sub txt_JumlahPotongan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahPotongan.PreviewTextInput

    End Sub


    Private Sub txt_JumlahGajiDibayarkan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahGajiDibayarkan.TextChanged
        JumlahGajiDibayarkan = AmbilAngka(txt_JumlahGajiDibayarkan.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahGajiDibayarkan)
    End Sub
    Private Sub txt_JumlahGajiDibayarkan_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahGajiDibayarkan.PreviewTextInput

    End Sub


    Private Sub txt_PPhDitanggungRutin_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggungRutin.TextChanged
        PPhDitanggungRutin = AmbilAngka(txt_PPhDitanggungRutin.Text)
        JumlahPPhDitanggung()
        PemecahRibuanUntukTextBox_WPF(txt_PPhDitanggungRutin)
    End Sub
    Private Sub txt_PPhDitanggungRutin_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhDitanggungRutin.PreviewTextInput

    End Sub


    Private Sub txt_PPhDitanggungPesangon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggungPesangon.TextChanged
        PPhDitanggungPesangon = AmbilAngka(txt_PPhDitanggungPesangon.Text)
        JumlahPPhDitanggung()
        PemecahRibuanUntukTextBox_WPF(txt_PPhDitanggungPesangon)
    End Sub
    Private Sub txt_PPhDitanggungPesangon_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhDitanggungPesangon.PreviewTextInput

    End Sub
    Sub JumlahPPhDitanggung()
        PPhDitanggung = PPhDitanggungRutin + PPhDitanggungPesangon
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click


        If cmb_Bulan.Text = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Bulan'.")
            cmb_Bulan.Focus()
            Return
        End If

        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
        End If

        If PilihanPPh_Rutin = Kosongan Then
            Pesan_Peringatan("Silakan tentukan Pilihan PPh Rutin : " & Enter1Baris &
                   "- PPh Dipotong/Ditunjang, atau " & Enter1Baris &
                   "- PPh Ditanggung")
            Return
        End If

        If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang And PotonganHutangPPhPasal21Rutin = 0 Then
            Pesan_Peringatan("Silakan isi kolom Hutang PPh Pasal 21 - Rutin.")
            txt_PotonganHutangPPhPasal21Rutin.Focus()
            Return
        End If

        If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung And PPhDitanggungRutin = 0 Then
            Pesan_Peringatan("Silakan isi kolom Jumlah PPh Ditanggung - Rutin.")
            txt_PPhDitanggungRutin.Focus()
            Return
        End If

        If AdaPPhPesangon = True Then

            If PilihanPPh_Pesangon = Kosongan Then
                Pesan_Peringatan("Silakan tentukan Pilihan PPh Pesangon : " & Enter1Baris &
                       "- PPh Dipotong/Ditunjang, atau " & Enter1Baris &
                       "- PPh Ditanggung")
                Return
            End If

            If PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang And PotonganHutangPPhPasal21Pesangon = 0 Then
                Pesan_Peringatan("Silakan isi kolom Hutang PPh Pasal 21 - Pesangon.")
                txt_PotonganHutangPPhPasal21Pesangon.Focus()
                Return
            End If

            If PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung And PPhDitanggungPesangon = 0 Then
                Pesan_Peringatan("Silakan isi kolom Jumlah PPh Ditanggung - Pesangon.")
                txt_PPhDitanggungPesangon.Focus()
                Return
            End If

        End If

        If JumlahGajiBagianProduksi + JumlahGajiBagianAdministrasi = 0 And JumlahGajiDibayarkan <> 0 Then
            Pesan_Peringatan("Silakan isi form dengan benar.")
            Return
        End If

        If JumlahGajiDibayarkan < 0 Then
            Pesan_Peringatan("Silakan isi form dengan benar.")
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            If Not TanyaKonfirmasi(teks_DataAkanDisimpanDiBukuPengawasanDanJurnal) Then Return
        End If
        If FungsiForm = FungsiForm_EDIT Then
            If Not TanyaKonfirmasi(teks_PerubahanDataAkanBerpengaruhPadaJurnal) Then Return
        End If

        Dim QueryPenyimpananPengeditan = Nothing

        'TrialBalance_Mentahkan()

        StatusSuntingDatabase = False 'Wajib Default False. Jangan dihapus...!!!

        If FungsiForm = FungsiForm_TAMBAH Then

            SistemPenomoranOtomatis_ID_Gaji()
            SistemPenomoranOtomatis_NomorJV()
            NomorJVGaji = jur_NomorJV

            QueryPenyimpananPengeditan =
                " INSERT INTO tbl_PengawasanGaji VALUES ( " &
                " '" & NomorID & "', " &
                " '" & Bulan & "', " &
                " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " '" & GajiBagianProduksi & "', " &
                " '" & GajiBagianProduksi2 & "', " &
                " '" & GajiBagianProduksi3 & "', " &
                " '" & GajiBagianProduksi4 & "', " &
                " '" & ThrBonusProduksi & "', " &
                " '" & TunjanganPPh21Produksi & "', " &
                " '" & BpjsTkJkkJkmProduksi & "', " &
                " '" & BpjsTkJhtIpProduksi & "', " &
                " '" & BpjsKesehatanProduksi & "', " &
                " '" & AsuransiKaryawanProduksi & "', " &
                " '" & PesangonKaryawanProduksi & "', " &
                " '" & JumlahGajiBagianProduksi & "', " &
                " '" & BpjsTkJhtIpProduksiDibayarKaryawan & "', " &
                " '" & BpjsKesehatanProduksiDibayarKaryawan & "', " &
                " '" & GajiBagianAdministrasi & "', " &
                " '" & GajiBagianAdministrasi2 & "', " &
                " '" & GajiBagianAdministrasi3 & "', " &
                " '" & GajiBagianAdministrasi4 & "', " &
                " '" & ThrBonusAdministrasi & "', " &
                " '" & TunjanganPPh21Administrasi & "', " &
                " '" & BpjsTkJkkJkmAdministrasi & "', " &
                " '" & BpjsTkJhtIpAdministrasi & "', " &
                " '" & BpjsKesehatanAdministrasi & "', " &
                " '" & AsuransiKaryawanAdministrasi & "', " &
                " '" & PesangonKaryawanAdministrasi & "', " &
                " '" & JumlahGajiBagianAdministrasi & "', " &
                " '" & BpjsTkJhtIpAdministrasiDibayarKaryawan & "', " &
                " '" & BpjsKesehatanAdministrasiDibayarKaryawan & "', " &
                " '" & JumlahGajiKotor & "', " &
                " '" & PotonganHutangBpjsKesehatan & "', " &
                " '" & PotonganHutangBpjsKetenagakerjaan & "', " &
                " '" & PotonganHutangKoperasi & "', " &
                " '" & PotonganHutangPPhPasal21Rutin & "', " &
                " '" & PotonganHutangPPhPasal21Pesangon & "', " &
                " '" & PotonganHutangSerikat & "', " &
                " '" & PotonganKasbonKaryawan & "', " &
                " '" & PotonganLainnya & "', " &
                " '" & JumlahPotongan & "', " &
                " '" & JumlahGajiDibayarkan & "', " &
                " '" & PPhDitanggungRutin & "', " &
                " '" & PPhDitanggungPesangon & "', " &
                " '" & NomorJVGaji & "', " &
                " '" & Keterangan & "' ) "

        End If

        If FungsiForm = FungsiForm_EDIT Then

            QueryPenyimpananPengeditan =
                " UPDATE tbl_PengawasanGaji SET " &
                " Bulan                                         = '" & Bulan & "', " &
                " Tanggal_Transaksi                             = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " Gaji_Bagian_Produksi                          = '" & GajiBagianProduksi & "', " &
                " Gaji_Bagian_Produksi_2                        = '" & GajiBagianProduksi2 & "', " &
                " Gaji_Bagian_Produksi_3                        = '" & GajiBagianProduksi3 & "', " &
                " Gaji_Bagian_Produksi_4                        = '" & GajiBagianProduksi4 & "', " &
                " THR_Bonus_Produksi                            = '" & ThrBonusProduksi & "', " &
                " Tunjangan_PPh_21_Produksi                     = '" & TunjanganPPh21Produksi & "', " &
                " BPJS_TK_JKK_JKM_Produksi                      = '" & BpjsTkJkkJkmProduksi & "', " &
                " BPJS_TK_JHT_IP_Produksi                       = '" & BpjsTkJhtIpProduksi & "', " &
                " BPJS_Kesehatan_Produksi                       = '" & BpjsKesehatanProduksi & "', " &
                " Asuransi_Karyawan_Produksi                    = '" & AsuransiKaryawanProduksi & "', " &
                " Pesangon_Karyawan_Produksi                    = '" & PesangonKaryawanProduksi & "', " &
                " Jumlah_Gaji_Bagian_Produksi                   = '" & JumlahGajiBagianProduksi & "', " &
                " BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan      = '" & BpjsTkJhtIpProduksiDibayarKaryawan & "', " &
                " BPJS_Kesehatan_Produksi_Dibayar_Karyawan      = '" & BpjsKesehatanProduksiDibayarKaryawan & "', " &
                " Gaji_Bagian_Administrasi                      = '" & GajiBagianAdministrasi & "', " &
                " Gaji_Bagian_Administrasi_2                    = '" & GajiBagianAdministrasi2 & "', " &
                " Gaji_Bagian_Administrasi_3                    = '" & GajiBagianAdministrasi3 & "', " &
                " Gaji_Bagian_Administrasi_4                    = '" & GajiBagianAdministrasi4 & "', " &
                " THR_Bonus_Administrasi                        = '" & ThrBonusAdministrasi & "', " &
                " Tunjangan_PPh_21_Administrasi                 = '" & TunjanganPPh21Administrasi & "', " &
                " BPJS_TK_JKK_JKM_Administrasi                  = '" & BpjsTkJkkJkmAdministrasi & "', " &
                " BPJS_TK_JHT_IP_Administrasi                   = '" & BpjsTkJhtIpAdministrasi & "', " &
                " BPJS_Kesehatan_Administrasi                   = '" & BpjsKesehatanAdministrasi & "', " &
                " Asuransi_Karyawan_Administrasi                = '" & AsuransiKaryawanAdministrasi & "', " &
                " Pesangon_Karyawan_Administrasi                = '" & PesangonKaryawanAdministrasi & "', " &
                " Jumlah_Gaji_Bagian_Administrasi               = '" & JumlahGajiBagianAdministrasi & "', " &
                " BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan  = '" & BpjsTkJhtIpAdministrasiDibayarKaryawan & "', " &
                " BPJS_Kesehatan_Administrasi_Dibayar_Karyawan  = '" & BpjsKesehatanAdministrasiDibayarKaryawan & "', " &
                " Jumlah_Gaji_Kotor                             = '" & JumlahGajiKotor & "', " &
                " Potongan_Hutang_BPJS_Kesehatan                = '" & PotonganHutangBpjsKesehatan & "', " &
                " Potongan_Hutang_BPJS_Ketenagakerjaan          = '" & PotonganHutangBpjsKetenagakerjaan & "', " &
                " Potongan_Hutang_Koperasi                      = '" & PotonganHutangKoperasi & "', " &
                " Potongan_Hutang_PPh_Pasal_21_Rutin            = '" & PotonganHutangPPhPasal21Rutin & "', " &
                " Potongan_Hutang_PPh_Pasal_21_Pesangon         = '" & PotonganHutangPPhPasal21Pesangon & "', " &
                " Potongan_Hutang_Serikat                       = '" & PotonganHutangSerikat & "', " &
                " Potongan_Kasbon_Karyawan                      = '" & PotonganKasbonKaryawan & "', " &
                " Potongan_Lainnya                              = '" & PotonganLainnya & "', " &
                " Jumlah_Potongan                               = '" & JumlahPotongan & "', " &
                " Jumlah_Gaji_Dibayarkan                        = '" & JumlahGajiDibayarkan & "', " &
                " PPh_Ditanggung_Rutin                          = '" & PPhDitanggungRutin & "', " &
                " PPh_Ditanggung_Pesangon                       = '" & PPhDitanggungPesangon & "', " &
                " Nomor_JV                                      = '" & NomorJVGaji & "', " &
                " Keterangan                                    = '" & Keterangan & "' " &
                " WHERE Nomor_ID                                = '" & NomorID & "' "

        End If

        'Proses Simpan/Edit :
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryPenyimpananPengeditan, KoneksiDatabaseTransaksi)
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_Transaksi(Tutup)

        'Jika Form Berfungsi untuk EDIT, maka hapus dulu data Jurnal yang lama, sebelum menyimpan data Jurnal yang Baru :
        If FungsiForm = FungsiForm_EDIT Then
            If StatusSuntingDatabase = True Then
                jur_NomorJV = NomorJVGaji  'Ini jangan dihapus. Penting untuk value saat mengisi Data Jurnal
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteNonQuery()
                    StatusSuntingDatabase = True
                Catch ex As Exception
                    StatusSuntingDatabase = False
                End Try
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        'Penyimpanan Data ke Jurnal (tbl_Transaksi) :
        If StatusSuntingDatabase = True Then

            '====================================================================================
            'PENYIMPANAN JURNAL :
            '====================================================================================
            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
            jur_JenisJurnal = JenisJurnal_Gaji
            jur_KodeLawanTransaksi = KodeLawanTransaksi_Karyawan
            jur_NamaLawanTransaksi = NamaLawanTransaksi_Karyawan
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeTautanCOA_BiayaGajiProduksi, GajiBagianProduksi)
            ___jurDebet(KodeTautanCOA_BiayaGajiProduksi2, GajiBagianProduksi2)
            ___jurDebet(KodeTautanCOA_BiayaGajiProduksi3, GajiBagianProduksi3)
            ___jurDebet(KodeTautanCOA_BiayaGajiProduksi4, GajiBagianProduksi4)
            ___jurDebet(KodeTautanCOA_BiayaThrBonusProduksi, ThrBonusProduksi)
            ___jurDebet(KodeTautanCOA_BiayaTunjanganPPh21Produksi, TunjanganPPh21Produksi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi, BpjsTkJkkJkmProduksi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsTkJhtIpProduksi, BpjsTkJhtIpProduksi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsKesehatanProduksi, BpjsKesehatanProduksi)
            ___jurDebet(KodeTautanCOA_BiayaAsuransiKaryawanProduksi, AsuransiKaryawanProduksi)
            ___jurDebet(KodeTautanCOA_BiayaPesangonKaryawanProduksi, PesangonKaryawanProduksi)
            ___jurDebet(KodeTautanCOA_BiayaGajiAdministrasi, GajiBagianAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaGajiAdministrasi2, GajiBagianAdministrasi2)
            ___jurDebet(KodeTautanCOA_BiayaGajiAdministrasi3, GajiBagianAdministrasi3)
            ___jurDebet(KodeTautanCOA_BiayaGajiAdministrasi4, GajiBagianAdministrasi4)
            ___jurDebet(KodeTautanCOA_BiayaThrBonusAdministrasi, ThrBonusAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaTunjanganPPh21Administrasi, TunjanganPPh21Administrasi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi, BpjsTkJkkJkmAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi, BpjsTkJhtIpAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaBpjsKesehatanAdministrasi, BpjsKesehatanAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi, AsuransiKaryawanAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaPesangonKaryawanAdministrasi, PesangonKaryawanAdministrasi)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal21, PPhDitanggung)
            _______jurKredit(KodeTautanCOA_HutangBpjsKesehatan, PotonganHutangBpjsKesehatan)
            _______jurKredit(KodeTautanCOA_HutangBpjsKetenagakerjaan, PotonganHutangBpjsKetenagakerjaan)
            _______jurKredit(KodeTautanCOA_HutangKoperasiKaryawan, PotonganHutangKoperasi)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_100, PotonganHutangPPhPasal21Rutin)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_401, PotonganHutangPPhPasal21Pesangon)
            _______jurKredit(KodeTautanCOA_HutangSerikat, PotonganHutangSerikat)
            _______jurKredit(KodeTautanCOA_PiutangKaryawan, PotonganKasbonKaryawan)
            _______jurKredit(KodeTautanCOA_HutangLancarLainnya, PotonganLainnya)
            _______jurKredit(KodeTautanCOA_HutangGaji, JumlahGajiDibayarkan)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_100, PPhDitanggungRutin)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_401, PPhDitanggungPesangon)

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                StatusSuntingDatabase = True
            Else
                StatusSuntingDatabase = False
            End If

            ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        End If

        'Report :
        If StatusSuntingDatabase = True Then
            If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then
                Pesan_Sukses("Data berhasil disimpan.")
                If usc_BukuPengawasanHutangBPJSKesehatan.StatusAktif Then usc_BukuPengawasanHutangBPJSKesehatan.TampilkanData()
                If usc_BukuPengawasanHutangBPJSKetenagakerjaan.StatusAktif Then usc_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
                If usc_BukuPengawasanHutangKoperasiKaryawan.StatusAktif Then usc_BukuPengawasanHutangKoperasiKaryawan.TampilkanData()
                If usc_BukuPengawasanHutangSerikat.StatusAktif Then usc_BukuPengawasanHutangSerikat.TampilkanData()
                If usc_BukuPengawasanGaji.StatusAktif Then usc_BukuPengawasanGaji.TampilkanData()
                Me.Close()
            End If
            If FungsiForm = FungsiForm_EDIT Then
                Pesan_Sukses("Data berhasil diedit.")
                Me.Close()
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then Pesan_Gagal("Data gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            If FungsiForm = FungsiForm_EDIT Then Pesan_Gagal("Data gagal diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Reset_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reset.Click
        If Not TanyaKonfirmasi("Yakin ingin me-reset form?") Then Return
        ResetForm()
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub





    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_Bulan.IsReadOnly = True
        txt_JumlahGajiBagianProduksi.IsReadOnly = True
        txt_JumlahGajiBagianAdministrasi.IsReadOnly = True
        txt_JumlahPotongan.IsReadOnly = True
        txt_JumlahGajiDibayarkan.IsReadOnly = True
    End Sub

End Class
