Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputGaji

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

    Private Sub frm_InputDataAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Gaji"
            PilihanPPh_Rutin = Kosongan
            cmb_Bulan.Enabled = True
            btn_Reset.Enabled = True
            grb_Rutin.Enabled = True
            grb_Pesangon.Enabled = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Gaji"
            cmb_Bulan.Enabled = False
            btn_Reset.Enabled = False
            grb_Rutin.Enabled = False
            grb_Pesangon.Enabled = False
            If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang Then rdb_PPhDipotongDitunjangRutin.Checked = True
            If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung Then rdb_PPhDitanggungRutin.Checked = True
            If PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang Then rdb_PPhDipotongDitunjangPesangon.Checked = True
            If PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung Then rdb_PPhDitanggungPesangon.Checked = True
            IsiValueForm()
        End If

        If FungsiForm = Nothing Then
            PesanUntukProgrammer("Tentukan Status Form Dulu..!")
            Me.Close()
        End If

        Me.Text = JudulForm

        Select Case JalurMasuk
            Case Halaman_BUKUPENGAWASANTURUNANGAJI
                KetersediaanKolomTerkaitBpjsKesehatan()
        End Select

    End Sub

    Sub IsiValueForm()

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
        cmb_Bulan.Text = Nothing
    End Sub

    Public Sub ResetForm()

        PilihanPPh_Rutin = Kosongan
        PilihanPPh_Pesangon = Kosongan
        AdaPPhPesangon = False
        KontenCombo_Bulan()
        cmb_Bulan.Enabled = True
        dtp_TanggalTransaksi.Value = New Date(TahunBukuAktif, BulanIni, 24)

        rdb_PPhDipotongDitunjangRutin.Checked = False
        rdb_PPhDitanggungRutin.Checked = False
        rdb_PPhDipotongDitunjangPesangon.Checked = False
        rdb_PPhDitanggungPesangon.Checked = False

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

        txt_GajiBagianAdministrasi.Text = Kosongan
        txt_GajiBagianAdministrasi2.Text = Kosongan
        txt_GajiBagianAdministrasi3.Text = Kosongan
        txt_GajiBagianAdministrasi4.Text = Kosongan
        txt_ThrBonusAdministrasi.Text = Kosongan
        txt_TunjanganPPh21Administrasi.Text = Kosongan
        txt_BpjsTkJkkJkmAdministrasi.Text = Kosongan
        txt_BpjsTkJhtIpAdministrasi.Text = Kosongan
        txt_BPJSKesehatanAdministrasi.Text = Kosongan
        txt_AsuransiKaryawanAdministrasi.Text = Kosongan
        txt_PesangonKaryawanAdministrasi.Text = Kosongan
        txt_JumlahGajiBagianAdministrasi.Text = Kosongan
        txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text = Kosongan

        txt_PotonganHutangBpjsKesehatan.Text = Kosongan
        txt_PotonganHutangBpjsKetenagakerjaan.Text = Kosongan
        txt_PotonganHutangKoperasi.Text = Kosongan
        txt_PotonganHutangPPhPasal21Rutin.Text = Kosongan
        txt_PotonganHutangPPhPasal21Pesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Enabled = False
        txt_PotonganHutangPPhPasal21Rutin.Enabled = False
        lbl_PotonganHutangPPhPasal21Pesangon.Enabled = False
        txt_PotonganHutangPPhPasal21Pesangon.Enabled = False
        txt_PotonganHutangSerikat.Text = Kosongan
        txt_PotonganKasbonKaryawan.Text = Kosongan
        txt_PotonganLainnya.Text = Kosongan
        txt_JumlahPotongan.Text = Kosongan
        txt_JumlahGajiDibayarkan.Text = Kosongan
        txt_PPhDitanggungRutin.Text = Kosongan
        txt_PPhDitanggungPesangon.Text = Kosongan
        lbl_PPhDitanggungRutin.Enabled = False
        txt_PPhDitanggungRutin.Enabled = False
        lbl_PPhDitanggungPesangon.Enabled = False
        txt_PPhDitanggungPesangon.Enabled = False
        txt_Keterangan.Text = Kosongan

        BukaSemuaKolom()
        KetersediaanKolomTerkaitVisibilitasCOA()

    End Sub

    Sub BukaSemuaKolom()
        cmb_Bulan.Enabled = True
        dtp_TanggalTransaksi.Enabled = True
        txt_GajiBagianProduksi.Enabled = True
        txt_GajiBagianProduksi2.Enabled = True
        txt_GajiBagianProduksi3.Enabled = True
        txt_GajiBagianProduksi4.Enabled = True
        txt_ThrBonusProduksi.Enabled = True
        txt_JumlahGajiBagianProduksi.Enabled = True
        txt_GajiBagianAdministrasi.Enabled = True
        txt_GajiBagianAdministrasi2.Enabled = True
        txt_GajiBagianAdministrasi3.Enabled = True
        txt_GajiBagianAdministrasi4.Enabled = True
        txt_ThrBonusAdministrasi.Enabled = True
        txt_JumlahGajiBagianAdministrasi.Enabled = True
        txt_PotonganHutangBpjsKesehatan.Enabled = True
        txt_PotonganHutangBpjsKetenagakerjaan.Enabled = True
        txt_PotonganHutangKoperasi.Enabled = True
        If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang Then txt_PotonganHutangPPhPasal21Rutin.Enabled = True
        txt_PotonganHutangSerikat.Enabled = True
        txt_PotonganKasbonKaryawan.Enabled = True
        txt_PotonganLainnya.Enabled = True
        txt_JumlahPotongan.Enabled = True
        txt_JumlahGajiDibayarkan.Enabled = True
        If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung Then txt_PPhDitanggungRutin.Enabled = True
    End Sub

    Sub TutupSemuaKolom()
        cmb_Bulan.Enabled = False
        dtp_TanggalTransaksi.Enabled = False
        txt_GajiBagianProduksi.Enabled = False
        txt_GajiBagianProduksi2.Enabled = False
        txt_GajiBagianProduksi3.Enabled = False
        txt_GajiBagianProduksi4.Enabled = False
        txt_ThrBonusProduksi.Enabled = False
        txt_JumlahGajiBagianProduksi.Enabled = False
        txt_GajiBagianAdministrasi.Enabled = False
        txt_GajiBagianAdministrasi2.Enabled = False
        txt_GajiBagianAdministrasi3.Enabled = False
        txt_GajiBagianAdministrasi4.Enabled = False
        txt_ThrBonusAdministrasi.Enabled = False
        txt_JumlahGajiBagianAdministrasi.Enabled = False
        txt_PotonganHutangBpjsKesehatan.Enabled = False
        txt_PotonganHutangBpjsKetenagakerjaan.Enabled = False
        txt_PotonganHutangKoperasi.Enabled = False
        txt_PotonganHutangPPhPasal21Rutin.Enabled = False
        txt_PotonganHutangSerikat.Enabled = False
        txt_PotonganKasbonKaryawan.Enabled = False
        txt_PotonganLainnya.Enabled = False
        txt_JumlahPotongan.Enabled = False
        txt_JumlahGajiDibayarkan.Enabled = False
        txt_PPhDitanggungRutin.Enabled = False
    End Sub

    Public Sub KetersediaanKolomTerkaitBpjsKesehatan()
        TutupSemuaKolom()
        dtp_TanggalTransaksi.Enabled = True
        txt_PotonganHutangBpjsKesehatan.Enabled = True
        txt_Keterangan.Enabled = True
    End Sub

    Sub KetersediaanKolomTerkaitBpjsKetenagakerjaan()
        TutupSemuaKolom()
        dtp_TanggalTransaksi.Enabled = True
        txt_PotonganHutangBpjsKetenagakerjaan.Enabled = True
        txt_Keterangan.Enabled = True
    End Sub

    Sub KetersediaanKolomTerkaitVisibilitasCOA()

        AksesDatabase_General(Buka)

        'Bagian Produksi : -----------------------------------------------------------------------------------------------------------

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianProduksi.Enabled = True
                txt_GajiBagianProduksi.Enabled = True
            Else
                lbl_GajiBagianProduksi.Enabled = False
                txt_GajiBagianProduksi.Enabled = False
            End If
            lbl_GajiBagianProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianProduksi.Enabled = False
            txt_GajiBagianProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi2 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianProduksi2.Enabled = True
                txt_GajiBagianProduksi2.Enabled = True
            Else
                lbl_GajiBagianProduksi2.Enabled = False
                txt_GajiBagianProduksi2.Enabled = False
            End If
            lbl_GajiBagianProduksi2.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianProduksi2.Enabled = False
            txt_GajiBagianProduksi2.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi3 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianProduksi3.Enabled = True
                txt_GajiBagianProduksi3.Enabled = True
            Else
                lbl_GajiBagianProduksi3.Enabled = False
                txt_GajiBagianProduksi3.Enabled = False
            End If
            lbl_GajiBagianProduksi3.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianProduksi3.Enabled = False
            txt_GajiBagianProduksi3.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi4 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianProduksi4.Enabled = True
                txt_GajiBagianProduksi4.Enabled = True
            Else
                lbl_GajiBagianProduksi4.Enabled = False
                txt_GajiBagianProduksi4.Enabled = False
            End If
            lbl_GajiBagianProduksi4.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianProduksi4.Enabled = False
            txt_GajiBagianProduksi4.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaThrBonusProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_ThrBonusProduksi.Enabled = True
                txt_ThrBonusProduksi.Enabled = True
            Else
                lbl_ThrBonusProduksi.Enabled = False
                txt_ThrBonusProduksi.Enabled = False
            End If
            lbl_ThrBonusProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_ThrBonusProduksi.Enabled = False
            txt_ThrBonusProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaTunjanganPPh21Produksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_TunjanganPPh21Produksi.Enabled = True
                txt_TunjanganPPh21Produksi.Enabled = True
            Else
                lbl_TunjanganPPh21Produksi.Enabled = False
                txt_TunjanganPPh21Produksi.Enabled = False
            End If
            lbl_TunjanganPPh21Produksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_TunjanganPPh21Produksi.Enabled = False
            txt_TunjanganPPh21Produksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsTkJkkJkmProduksi.Enabled = True
                txt_BpjsTkJkkJkmProduksi.Enabled = True
            Else
                lbl_BpjsTkJkkJkmProduksi.Enabled = False
                txt_BpjsTkJkkJkmProduksi.Enabled = False
            End If
            lbl_BpjsTkJkkJkmProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsTkJkkJkmProduksi.Enabled = False
            txt_BpjsTkJkkJkmProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJhtIpProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsTkJhtIpProduksi.Enabled = True
                txt_BpjsTkJhtIpProduksi.Enabled = True
            Else
                lbl_BpjsTkJhtIpProduksi.Enabled = False
                txt_BpjsTkJhtIpProduksi.Enabled = False
            End If
            lbl_BpjsTkJhtIpProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsTkJhtIpProduksi.Enabled = False
            txt_BpjsTkJhtIpProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsKesehatanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsKesehatanProduksi.Enabled = True
                txt_BpjsKesehatanProduksi.Enabled = True
            Else
                lbl_BpjsKesehatanProduksi.Enabled = False
                txt_BpjsKesehatanProduksi.Enabled = False
            End If
            lbl_BpjsKesehatanProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsKesehatanProduksi.Enabled = False
            txt_BpjsKesehatanProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaAsuransiKaryawanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_AsuransiKaryawanProduksi.Enabled = True
                txt_AsuransiKaryawanProduksi.Enabled = True
            Else
                lbl_AsuransiKaryawanProduksi.Enabled = False
                txt_AsuransiKaryawanProduksi.Enabled = False
            End If
            lbl_AsuransiKaryawanProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_AsuransiKaryawanProduksi.Enabled = False
            txt_AsuransiKaryawanProduksi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaPesangonKaryawanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PesangonKaryawanProduksi.Enabled = True
                txt_PesangonKaryawanProduksi.Enabled = True
            Else
                lbl_PesangonKaryawanProduksi.Enabled = False
                txt_PesangonKaryawanProduksi.Enabled = False
            End If
            lbl_PesangonKaryawanProduksi.Text = dr.Item("Nama_Akun")
        Else
            lbl_PesangonKaryawanProduksi.Enabled = False
            txt_PesangonKaryawanProduksi.Enabled = False
        End If




        'Bagian Administrasi : -----------------------------------------------------------------------------------------------------------

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianAdministrasi.Enabled = True
                txt_GajiBagianAdministrasi.Enabled = True
            Else
                lbl_GajiBagianAdministrasi.Enabled = False
                txt_GajiBagianAdministrasi.Enabled = False
            End If
            lbl_GajiBagianAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianAdministrasi.Enabled = False
            txt_GajiBagianAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi2 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianAdministrasi2.Enabled = True
                txt_GajiBagianAdministrasi2.Enabled = True
            Else
                lbl_GajiBagianAdministrasi2.Enabled = False
                txt_GajiBagianAdministrasi2.Enabled = False
            End If
            lbl_GajiBagianAdministrasi2.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianAdministrasi2.Enabled = False
            txt_GajiBagianAdministrasi2.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi3 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianAdministrasi3.Enabled = True
                txt_GajiBagianAdministrasi3.Enabled = True
            Else
                lbl_GajiBagianAdministrasi3.Enabled = False
                txt_GajiBagianAdministrasi3.Enabled = False
            End If
            lbl_GajiBagianAdministrasi3.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianAdministrasi3.Enabled = False
            txt_GajiBagianAdministrasi3.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi4 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_GajiBagianAdministrasi4.Enabled = True
                txt_GajiBagianAdministrasi4.Enabled = True
            Else
                lbl_GajiBagianAdministrasi4.Enabled = False
                txt_GajiBagianAdministrasi4.Enabled = False
            End If
            lbl_GajiBagianAdministrasi4.Text = dr.Item("Nama_Akun")
        Else
            lbl_GajiBagianAdministrasi4.Enabled = False
            txt_GajiBagianAdministrasi4.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaThrBonusAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_ThrBonusAdministrasi.Enabled = True
                txt_ThrBonusAdministrasi.Enabled = True
            Else
                lbl_ThrBonusAdministrasi.Enabled = False
                txt_ThrBonusAdministrasi.Enabled = False
            End If
            lbl_ThrBonusAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_ThrBonusAdministrasi.Enabled = False
            txt_ThrBonusAdministrasi.Enabled = False
        End If


        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaTunjanganPPh21Administrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_TunjanganPPh21Administrasi.Enabled = True
                txt_TunjanganPPh21Administrasi.Enabled = True
            Else
                lbl_TunjanganPPh21Administrasi.Enabled = False
                txt_TunjanganPPh21Administrasi.Enabled = False
            End If
            lbl_TunjanganPPh21Administrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_TunjanganPPh21Administrasi.Enabled = False
            txt_TunjanganPPh21Administrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsTkJkkJkmAdministrasi.Enabled = True
                txt_BpjsTkJkkJkmAdministrasi.Enabled = True
            Else
                lbl_BpjsTkJkkJkmAdministrasi.Enabled = False
                txt_BpjsTkJkkJkmAdministrasi.Enabled = False
            End If
            lbl_BpjsTkJkkJkmAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsTkJkkJkmAdministrasi.Enabled = False
            txt_BpjsTkJkkJkmAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsTkJhtIpAdministrasi.Enabled = True
                txt_BpjsTkJhtIpAdministrasi.Enabled = True
            Else
                lbl_BpjsTkJhtIpAdministrasi.Enabled = False
                txt_BpjsTkJhtIpAdministrasi.Enabled = False
            End If
            lbl_BpjsTkJhtIpAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsTkJhtIpAdministrasi.Enabled = False
            txt_BpjsTkJhtIpAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsKesehatanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_BpjsKesehatanAdministrasi.Enabled = True
                txt_BPJSKesehatanAdministrasi.Enabled = True
            Else
                lbl_BpjsKesehatanAdministrasi.Enabled = False
                txt_BPJSKesehatanAdministrasi.Enabled = False
            End If
            lbl_BpjsKesehatanAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_BpjsKesehatanAdministrasi.Enabled = False
            txt_BPJSKesehatanAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_AsuransiKaryawanAdministrasi.Enabled = True
                txt_AsuransiKaryawanAdministrasi.Enabled = True
            Else
                lbl_AsuransiKaryawanAdministrasi.Enabled = False
                txt_AsuransiKaryawanAdministrasi.Enabled = False
            End If
            lbl_AsuransiKaryawanAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_AsuransiKaryawanAdministrasi.Enabled = False
            txt_AsuransiKaryawanAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaPesangonKaryawanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PesangonKaryawanAdministrasi.Enabled = True
                txt_PesangonKaryawanAdministrasi.Enabled = True
            Else
                lbl_PesangonKaryawanAdministrasi.Enabled = False
                txt_PesangonKaryawanAdministrasi.Enabled = False
            End If
            lbl_PesangonKaryawanAdministrasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_PesangonKaryawanAdministrasi.Enabled = False
            txt_PesangonKaryawanAdministrasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangBpjsKesehatan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganHutangBpjsKesehatan.Enabled = True
                txt_PotonganHutangBpjsKesehatan.Enabled = True
            Else
                lbl_PotonganHutangBpjsKesehatan.Enabled = False
                txt_PotonganHutangBpjsKesehatan.Enabled = False
            End If
            lbl_PotonganHutangBpjsKesehatan.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangBpjsKesehatan.Enabled = False
            txt_PotonganHutangBpjsKesehatan.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangBpjsKetenagakerjaan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganHutangBpjsKetenagakerjaan.Enabled = True
                txt_PotonganHutangBpjsKetenagakerjaan.Enabled = True
            Else
                lbl_PotonganHutangBpjsKetenagakerjaan.Enabled = False
                txt_PotonganHutangBpjsKetenagakerjaan.Enabled = False
            End If
            lbl_PotonganHutangBpjsKetenagakerjaan.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangBpjsKetenagakerjaan.Enabled = False
            txt_PotonganHutangBpjsKetenagakerjaan.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangKoperasiKaryawan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganHutangKoperasi.Enabled = True
                txt_PotonganHutangKoperasi.Enabled = True
            Else
                lbl_PotonganHutangKoperasi.Enabled = False
                txt_PotonganHutangKoperasi.Enabled = False
            End If
            lbl_PotonganHutangKoperasi.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangKoperasi.Enabled = False
            txt_PotonganHutangKoperasi.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangPPhPasal21_100 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            'lbl_PotonganHutangPPhPasal21Rutin.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangPPhPasal21Rutin.Enabled = False
            txt_PotonganHutangPPhPasal21Rutin.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangPPhPasal21_401 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            'lbl_PotonganHutangPPhPasal21Pesangon.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangPPhPasal21Pesangon.Enabled = False
            txt_PotonganHutangPPhPasal21Pesangon.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangSerikat & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganHutangSerikat.Enabled = True
                txt_PotonganHutangSerikat.Enabled = True
            Else
                lbl_PotonganHutangSerikat.Enabled = False
                txt_PotonganHutangSerikat.Enabled = False
            End If
            lbl_PotonganHutangSerikat.Text = dr.Item("Nama_Akun")
        Else
            lbl_PotonganHutangSerikat.Enabled = False
            txt_PotonganHutangSerikat.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_PiutangKaryawan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganKasbonKaryawan.Enabled = True
                txt_PotonganKasbonKaryawan.Enabled = True
            Else
                lbl_PotonganKasbonKaryawan.Enabled = False
                txt_PotonganKasbonKaryawan.Enabled = False
            End If
            lbl_PotonganKasbonKaryawan.Text = "Kasbon Karyawan" 'Tidak mengikuti Nama Akun (Pengecualian)
        Else
            lbl_PotonganKasbonKaryawan.Enabled = False
            txt_PotonganKasbonKaryawan.Enabled = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangLancarLainnya & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then
                lbl_PotonganLainnya.Enabled = True
                txt_PotonganLainnya.Enabled = True
            Else
                lbl_PotonganLainnya.Enabled = False
                txt_PotonganLainnya.Enabled = False
            End If
            lbl_PotonganLainnya.Text = "Potongan Lainnya" 'Tidak mengikuti Nama Akun (Pengecualian)
        Else
            lbl_PotonganLainnya.Enabled = False
            txt_PotonganLainnya.Enabled = False
        End If

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
        txt_PotonganHutangBpjsKesehatan.Text = BpjsKesehatanProduksi + BpjsKesehatanAdministrasi
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
                Pilihan = MessageBox.Show("Apakah ada PPh untuk Pesangon..?", "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbYes Then
                    AdaPPhPesangon = True
                    grb_Pesangon.Enabled = True
                Else
                    AdaPPhPesangon = False
                    grb_Pesangon.Enabled = False
                    rdb_PPhDipotongDitunjangPesangon.Checked = False
                    rdb_PPhDitanggungPesangon.Checked = False
                    lbl_PotonganHutangPPhPasal21Pesangon.Enabled = False
                    txt_PotonganHutangPPhPasal21Pesangon.Enabled = False
                    lbl_PPhDitanggungPesangon.Enabled = False
                    txt_PPhDitanggungPesangon.Enabled = False
                End If
            End If
        Else
            AdaPPhPesangon = False
            grb_Pesangon.Enabled = False
            rdb_PPhDipotongDitunjangPesangon.Checked = False
            rdb_PPhDitanggungPesangon.Checked = False
            lbl_PotonganHutangPPhPasal21Pesangon.Enabled = False
            txt_PotonganHutangPPhPasal21Pesangon.Enabled = False
            lbl_PPhDitanggungPesangon.Enabled = False
            txt_PPhDitanggungPesangon.Enabled = False
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

    Private Sub cmb_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Bulan.SelectedIndexChanged
    End Sub
    Private Sub cmb_Bulan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Bulan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_Bulan_TextChanged(sender As Object, e As EventArgs) Handles cmb_Bulan.TextChanged
        Bulan = cmb_Bulan.Text
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
        dtp_TanggalTransaksi.Value = New Date(TahunBukuAktif, BulanAngka, 24)
    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        If dtp_TanggalTransaksi.Value.Year <> TahunBukuAktif Then
            dtp_TanggalTransaksi.Value = New Date(TahunBukuAktif, dtp_TanggalTransaksi.Value.Month, dtp_TanggalTransaksi.Value.Day)
        End If
        TanggalTransaksi = dtp_TanggalTransaksi.Value
    End Sub

    Private Sub rdb_PPhDipotongDitunjangRutin_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PPhDipotongDitunjangRutin.CheckedChanged
        If rdb_PPhDipotongDitunjangRutin.Checked = True Then SusunanForm_PPhDipotongDitunjang_Rutin()
    End Sub

    Private Sub rdb_PPhDitanggungPesangon_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PPhDitanggungRutin.CheckedChanged
        If rdb_PPhDitanggungRutin.Checked = True Then SusunanForm_PPhDitanggung_Rutin()
    End Sub

    Sub SusunanForm_PPhDipotongDitunjang_Rutin()
        PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang
        txt_PPhDitanggungRutin.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Enabled = True
        txt_PotonganHutangPPhPasal21Rutin.Enabled = True
        lbl_PPhDitanggungRutin.Enabled = False
        txt_PPhDitanggungRutin.Enabled = False
    End Sub

    Sub SusunanForm_PPhDitanggung_Rutin()
        PilihanPPh_Rutin = PilihanPPh_PPhDitanggung
        txt_PotonganHutangPPhPasal21Rutin.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Rutin.Enabled = False
        txt_PotonganHutangPPhPasal21Rutin.Enabled = False
        lbl_PPhDitanggungRutin.Enabled = True
        txt_PPhDitanggungRutin.Enabled = True
    End Sub

    Private Sub rdb_PPhDipotongDitunjangPesangon_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PPhDipotongDitunjangPesangon.CheckedChanged
        If rdb_PPhDipotongDitunjangPesangon.Checked = True Then SusunanForm_PPhDipotongDitunjang_Pesangon()
    End Sub

    Private Sub rdb_PPhDitanggungPesangon_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdb_PPhDitanggungPesangon.CheckedChanged
        If rdb_PPhDitanggungPesangon.Checked = True Then SusunanForm_PPhDitanggung_Pesangon()
    End Sub

    Sub SusunanForm_PPhDipotongDitunjang_Pesangon()
        PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang
        txt_PPhDitanggungPesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Pesangon.Enabled = True
        txt_PotonganHutangPPhPasal21Pesangon.Enabled = True
        lbl_PPhDitanggungPesangon.Enabled = False
        txt_PPhDitanggungPesangon.Enabled = False
    End Sub

    Sub SusunanForm_PPhDitanggung_Pesangon()
        PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung
        txt_PotonganHutangPPhPasal21Pesangon.Text = Kosongan
        lbl_PotonganHutangPPhPasal21Pesangon.Enabled = False
        txt_PotonganHutangPPhPasal21Pesangon.Enabled = False
        lbl_PPhDitanggungPesangon.Enabled = True
        txt_PPhDitanggungPesangon.Enabled = True
    End Sub

    Private Sub txt_GajiBagianProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianProduksi.TextChanged
        GajiBagianProduksi = AmbilAngka(txt_GajiBagianProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_GajiBagianProduksi)
    End Sub

    Private Sub txt_GajiBagianProduksi2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianProduksi2.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianProduksi2_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianProduksi2.TextChanged
        GajiBagianProduksi2 = AmbilAngka(txt_GajiBagianProduksi2.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_GajiBagianProduksi2)
    End Sub

    Private Sub txt_GajiBagianProduksi3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianProduksi3.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianProduksi3_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianProduksi3.TextChanged
        GajiBagianProduksi3 = AmbilAngka(txt_GajiBagianProduksi3.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_GajiBagianProduksi3)
    End Sub

    Private Sub txt_GajiBagianProduksi4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianProduksi4.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianProduksi4_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianProduksi4.TextChanged
        GajiBagianProduksi4 = AmbilAngka(txt_GajiBagianProduksi4.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_GajiBagianProduksi4)
    End Sub

    Private Sub txt_ThrBonusProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_ThrBonusProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_ThrBonusProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_ThrBonusProduksi.TextChanged
        ThrBonusProduksi = AmbilAngka(txt_ThrBonusProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_ThrBonusProduksi)
    End Sub

    Private Sub txt_TunjanganPPh21Produksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TunjanganPPh21Produksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_TunjanganPPh21Produksi_TextChanged(sender As Object, e As EventArgs) Handles txt_TunjanganPPh21Produksi.TextChanged
        TunjanganPPh21Produksi = AmbilAngka(txt_TunjanganPPh21Produksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_TunjanganPPh21Produksi)
    End Sub

    Private Sub txt_BpjsTkJkkJkmProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJkkJkmProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJkkJkmProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJkkJkmProduksi.TextChanged
        BpjsTkJkkJkmProduksi = AmbilAngka(txt_BpjsTkJkkJkmProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox(txt_BpjsTkJkkJkmProduksi)
    End Sub

    Private Sub txt_BpjsTkJhtIpProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJhtIpProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJhtIpProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJhtIpProduksi.TextChanged
        BpjsTkJhtIpProduksi = AmbilAngka(txt_BpjsTkJhtIpProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox(txt_BpjsTkJhtIpProduksi)
    End Sub

    Private Sub txt_BPJSKesehatanProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsKesehatanProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BPJSKesehatanProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsKesehatanProduksi.TextChanged
        BpjsKesehatanProduksi = AmbilAngka(txt_BpjsKesehatanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox(txt_BpjsKesehatanProduksi)
    End Sub

    Private Sub txt_AsuransiKaryawanProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AsuransiKaryawanProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_AsuransiKaryawanProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_AsuransiKaryawanProduksi.TextChanged
        AsuransiKaryawanProduksi = AmbilAngka(txt_AsuransiKaryawanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_AsuransiKaryawanProduksi)
    End Sub

    Private Sub txt_PesangonKaryawanProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PesangonKaryawanProduksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PesangonKaryawanProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_PesangonKaryawanProduksi.TextChanged
        PesangonKaryawanProduksi = AmbilAngka(txt_PesangonKaryawanProduksi.Text)
        Perhitungan_JumlahGajiBagianProduksi()
        PemecahRibuanUntukTextBox(txt_PesangonKaryawanProduksi)
    End Sub
    Private Sub txt_PesangonKaryawanProduksi_Leave(sender As Object, e As EventArgs) Handles txt_PesangonKaryawanProduksi.Leave
        Perhitungan_PPhPesangon()
    End Sub


    Private Sub txt_JumlahGajiBagianProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahGajiBagianProduksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_JumlahGajiBagianProduksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahGajiBagianProduksi.TextChanged
        JumlahGajiBagianProduksi = AmbilAngka(txt_JumlahGajiBagianProduksi.Text)
        Perhitungan_JumlahGajiKotor()
        PemecahRibuanUntukTextBox(txt_JumlahGajiBagianProduksi)
    End Sub

    Private Sub txt_BpjsTkJhtIpProduksiDibayarKaryawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJhtIpProduksiDibayarKaryawan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJhtIpProduksiDibayarKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJhtIpProduksiDibayarKaryawan.TextChanged
        BpjsTkJhtIpProduksiDibayarKaryawan = AmbilAngka(txt_BpjsTkJhtIpProduksiDibayarKaryawan.Text)
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox(txt_BpjsTkJhtIpProduksiDibayarKaryawan)
    End Sub


    Private Sub txt_GajiBagianAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianAdministrasi.TextChanged
        GajiBagianAdministrasi = AmbilAngka(txt_GajiBagianAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_GajiBagianAdministrasi)
    End Sub

    Private Sub txt_GajiBagianAdministrasi2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianAdministrasi2.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianAdministrasi2_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianAdministrasi2.TextChanged
        GajiBagianAdministrasi2 = AmbilAngka(txt_GajiBagianAdministrasi2.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_GajiBagianAdministrasi2)
    End Sub

    Private Sub txt_GajiBagianAdministrasi3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianAdministrasi3.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianAdministrasi3_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianAdministrasi3.TextChanged
        GajiBagianAdministrasi3 = AmbilAngka(txt_GajiBagianAdministrasi3.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_GajiBagianAdministrasi3)
    End Sub

    Private Sub txt_GajiBagianAdministrasi4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_GajiBagianAdministrasi4.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianAdministrasi4_TextChanged(sender As Object, e As EventArgs) Handles txt_GajiBagianAdministrasi4.TextChanged
        GajiBagianAdministrasi4 = AmbilAngka(txt_GajiBagianAdministrasi4.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_GajiBagianAdministrasi4)
    End Sub

    Private Sub txt_GajiBagianAdministrasi5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_ThrBonusAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_GajiBagianAdministrasi5_TextChanged(sender As Object, e As EventArgs) Handles txt_ThrBonusAdministrasi.TextChanged
        ThrBonusAdministrasi = AmbilAngka(txt_ThrBonusAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_ThrBonusAdministrasi)
    End Sub

    Private Sub txt_TunjanganPPh21Administrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TunjanganPPh21Administrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_TunjanganPPh21Administrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_TunjanganPPh21Administrasi.TextChanged
        TunjanganPPh21Administrasi = AmbilAngka(txt_TunjanganPPh21Administrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_TunjanganPPh21Administrasi)
    End Sub

    Private Sub txt_BpjsTkJkkJkmAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJkkJkmAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJkkJkmAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJkkJkmAdministrasi.TextChanged
        BpjsTkJkkJkmAdministrasi = AmbilAngka(txt_BpjsTkJkkJkmAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox(txt_BpjsTkJkkJkmAdministrasi)
    End Sub

    Private Sub txt_BpjsTkJhtIpAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJhtIpAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJhtIpAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJhtIpAdministrasi.TextChanged
        BpjsTkJhtIpAdministrasi = AmbilAngka(txt_BpjsTkJhtIpAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKetenagakerjaan()
        PemecahRibuanUntukTextBox(txt_BpjsTkJhtIpAdministrasi)
    End Sub

    Private Sub txt_BPJSKesehatanAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BPJSKesehatanAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BPJSKesehatanAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_BPJSKesehatanAdministrasi.TextChanged
        BpjsKesehatanAdministrasi = AmbilAngka(txt_BPJSKesehatanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PerhitunganHutangBpjsKesehatan()
        PemecahRibuanUntukTextBox(txt_BPJSKesehatanAdministrasi)
    End Sub

    Private Sub txt_AsuransiKaryawanAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AsuransiKaryawanAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_AsuransiKaryawanAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_AsuransiKaryawanAdministrasi.TextChanged
        AsuransiKaryawanAdministrasi = AmbilAngka(txt_AsuransiKaryawanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_AsuransiKaryawanAdministrasi)
    End Sub

    Private Sub txt_PesangonKaryawanAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PesangonKaryawanAdministrasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PesangonKaryawanAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_PesangonKaryawanAdministrasi.TextChanged
        PesangonKaryawanAdministrasi = AmbilAngka(txt_PesangonKaryawanAdministrasi.Text)
        Perhitungan_JumlahGajiBagianAdministrasi()
        PemecahRibuanUntukTextBox(txt_PesangonKaryawanAdministrasi)
    End Sub
    Private Sub txt_PesangonKaryawanAdministrasi_Leave(sender As Object, e As EventArgs) Handles txt_PesangonKaryawanAdministrasi.Leave
        Perhitungan_PPhPesangon()
    End Sub

    Private Sub txt_JumlahGajiBagianAdministrasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahGajiBagianAdministrasi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_JumlahGajiBagianAdministrasi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahGajiBagianAdministrasi.TextChanged
        JumlahGajiBagianAdministrasi = AmbilAngka(txt_JumlahGajiBagianAdministrasi.Text)
        Perhitungan_JumlahGajiKotor()
        PemecahRibuanUntukTextBox(txt_JumlahGajiBagianAdministrasi)
    End Sub

    Private Sub txt_BpjsTkJhtIpAdministrasiDibayarKaryawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BpjsTkJhtIpAdministrasiDibayarKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.TextChanged
        BpjsTkJhtIpAdministrasiDibayarKaryawan = AmbilAngka(txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text)
        PemecahRibuanUntukTextBox(txt_BpjsTkJhtIpAdministrasiDibayarKaryawan)
        PerhitunganHutangBpjsKetenagakerjaan()
    End Sub

    Private Sub txt_PotonganHutangBpjsKesehatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangBpjsKesehatan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_PotonganHutangBpjsKesehatan_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangBpjsKesehatan.TextChanged
        PotonganHutangBpjsKesehatan = AmbilAngka(txt_PotonganHutangBpjsKesehatan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangBpjsKesehatan)
    End Sub

    Private Sub txt_PotonganHutangBpjsKetenagakerjaan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangBpjsKetenagakerjaan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_PotonganHutangBpjsKetenagakerjaan_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangBpjsKetenagakerjaan.TextChanged
        PotonganHutangBpjsKetenagakerjaan = AmbilAngka(txt_PotonganHutangBpjsKetenagakerjaan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangBpjsKetenagakerjaan)
    End Sub

    Private Sub txt_PotonganHutangKoperasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangKoperasi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganHutangKoperasi_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangKoperasi.TextChanged
        PotonganHutangKoperasi = AmbilAngka(txt_PotonganHutangKoperasi.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangKoperasi)
    End Sub

    Private Sub txt_PotonganHutangPPhPasal21Rutin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangPPhPasal21Rutin.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganHutangPPhPasal21Rutin_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangPPhPasal21Rutin.TextChanged
        PotonganHutangPPhPasal21Rutin = AmbilAngka(txt_PotonganHutangPPhPasal21Rutin.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangPPhPasal21Rutin)
    End Sub

    Private Sub txt_PotonganHutangPPhPasal21Pesangon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangPPhPasal21Pesangon.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganHutangPPhPasal21Pesangon_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangPPhPasal21Pesangon.TextChanged
        PotonganHutangPPhPasal21Pesangon = AmbilAngka(txt_PotonganHutangPPhPasal21Pesangon.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangPPhPasal21Pesangon)
    End Sub

    Private Sub txt_PotonganHutangSerikat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganHutangSerikat.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganHutangSerikat_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganHutangSerikat.TextChanged
        PotonganHutangSerikat = AmbilAngka(txt_PotonganHutangSerikat.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganHutangSerikat)
    End Sub

    Private Sub txt_PotonganKasbonKaryawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganKasbonKaryawan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganKasbonKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganKasbonKaryawan.TextChanged
        PotonganKasbonKaryawan = AmbilAngka(txt_PotonganKasbonKaryawan.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganKasbonKaryawan)
    End Sub

    Private Sub txt_PotonganLainnya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PotonganLainnya.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PotonganLainnya_TextChanged(sender As Object, e As EventArgs) Handles txt_PotonganLainnya.TextChanged
        PotonganLainnya = AmbilAngka(txt_PotonganLainnya.Text)
        Perhitungan_JumlahPotongan()
        PemecahRibuanUntukTextBox(txt_PotonganLainnya)
    End Sub

    Private Sub txt_JumlahPotongan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPotongan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_JumlahPotongan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPotongan.TextChanged
        JumlahPotongan = AmbilAngka(txt_JumlahPotongan.Text)
        Perhitungan_JumlahGajiDibayarkan()
        PemecahRibuanUntukTextBox(txt_JumlahPotongan)
    End Sub

    Private Sub txt_JumlahGajiDibayarkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahGajiDibayarkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_JumlahGajiDibayarkan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahGajiDibayarkan.TextChanged
        JumlahGajiDibayarkan = AmbilAngka(txt_JumlahGajiDibayarkan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahGajiDibayarkan)
    End Sub

    Private Sub txt_PPhDitanggungRutin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggungRutin.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDitanggungRutin_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggungRutin.TextChanged
        PPhDitanggungRutin = AmbilAngka(txt_PPhDitanggungRutin.Text)
        JumlahPPhDitanggung()
        PemecahRibuanUntukTextBox(txt_PPhDitanggungRutin)
    End Sub

    Private Sub txt_PPhDitanggungPesangon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggungPesangon.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDitanggungPesangon_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggungPesangon.TextChanged
        PPhDitanggungPesangon = AmbilAngka(txt_PPhDitanggungPesangon.Text)
        JumlahPPhDitanggung()
        PemecahRibuanUntukTextBox(txt_PPhDitanggungPesangon)
    End Sub

    Sub JumlahPPhDitanggung()
        PPhDitanggung = PPhDitanggungRutin + PPhDitanggungPesangon
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub



    'Tombol :
    '======================================================================================
    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        If cmb_Bulan.Text = Kosongan Then
            MsgBox("Silakan pilih 'Bulan'")
            cmb_Bulan.Focus()
            Return
        End If

        If PilihanPPh_Rutin = Kosongan Then
            MsgBox("Silakan tentukan Pilihan PPh Rutin : " & Enter1Baris &
                   "- PPh Dipotong/Ditunjang, atau " & Enter1Baris &
                   "- PPh Ditanggung")
            Return
        End If

        If PilihanPPh_Rutin = PilihanPPh_PPhDipotongDitunjang And PotonganHutangPPhPasal21Rutin = 0 Then
            MsgBox("Silakan isi kolom Hutang PPh Pasal 21 - Rutin..!")
            txt_PotonganHutangPPhPasal21Rutin.Focus()
            Return
        End If

        If PilihanPPh_Rutin = PilihanPPh_PPhDitanggung And PPhDitanggungRutin = 0 Then
            MsgBox("Silakan isi kolom Jumlah PPh Ditanggung - Rutin..!")
            txt_PPhDitanggungRutin.Focus()
            Return
        End If

        If AdaPPhPesangon = True Then

            If PilihanPPh_Pesangon = Kosongan Then
                MsgBox("Silakan tentukan Pilihan PPh Pesangon : " & Enter1Baris &
                       "- PPh Dipotong/Ditunjang, atau " & Enter1Baris &
                       "- PPh Ditanggung")
                Return
            End If

            If PilihanPPh_Pesangon = PilihanPPh_PPhDipotongDitunjang And PotonganHutangPPhPasal21Pesangon = 0 Then
                MsgBox("Silakan isi kolom Hutang PPh Pasal 21 - Pesangon..!")
                txt_PotonganHutangPPhPasal21Pesangon.Focus()
                Return
            End If

            If PilihanPPh_Pesangon = PilihanPPh_PPhDitanggung And PPhDitanggungPesangon = 0 Then
                MsgBox("Silakan isi kolom Jumlah PPh Ditanggung - Pesangon..!")
                txt_PPhDitanggungPesangon.Focus()
                Return
            End If

        End If

        If JumlahGajiBagianProduksi + JumlahGajiBagianAdministrasi = 0 And JumlahGajiDibayarkan <> 0 Then
            MsgBox("Silakan isi form dengan benar.")
            Return
        End If

        If JumlahGajiDibayarkan < 0 Then
            MsgBox("Silakan isi form dengan benar.")
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then Pilihan = MessageBox.Show(teks_DataAkanDisimpanDiBukuPengawasanDanJurnal, "Perhatian..!", MessageBoxButtons.YesNo)
        If FungsiForm = FungsiForm_EDIT Then Pilihan = MessageBox.Show(teks_PerubahanDataAkanBerpengaruhPadaJurnal, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

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
                MsgBox("Data BERHASIL disimpan.")
                If usc_BukuPengawasanHutangBPJSKesehatan.StatusAktif Then usc_BukuPengawasanHutangBPJSKesehatan.TampilkanData()
                If usc_BukuPengawasanHutangBPJSKetenagakerjaan.StatusAktif Then usc_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
                If usc_BukuPengawasanHutangKoperasiKaryawan.StatusAktif Then usc_BukuPengawasanHutangKoperasiKaryawan.TampilkanData()
                If usc_BukuPengawasanHutangSerikat.StatusAktif Then usc_BukuPengawasanHutangSerikat.TampilkanData()
                If usc_BukuPengawasanGaji.StatusAktif Then usc_BukuPengawasanGaji.TampilkanData()
                Me.Close()
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data BERHASIL diedit.")
                Me.Close()
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            If FungsiForm = FungsiForm_EDIT Then MsgBox("Data GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click

        Dim PilihReset = MessageBox.Show("Yakin akan me-reset form..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihReset = vbNo Then Return

        ResetForm()

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class