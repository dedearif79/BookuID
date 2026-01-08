Imports System.Data.Odbc
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms.Integration
Imports bcomm
Imports Windows.Win32.System


Public Module mdl_PublicSub

    'DATA AWAL LOADING APLIKASI :
    Public Sub DataAwalLoadingAplikasi()

        'Tittle & Semua Data Company
        IsiValueDataCompany()

        'Value Tautan COA
        IsiValueTautanCOA()

        'Value Jenis Jurnal Berdasarkan COA
        IsiValueJenisJurnal_SaranaPembayaran()

        'Data Set COA
        IsiDsCOA()

        'Pertanggalan :
        If TahunIni Mod 4 = 0 Then
            TahunKabisat_TahunIni = True
        Else
            TahunKabisat_TahunIni = False
        End If
        If BulanIni = "01" Then TanggalAkhirBulan = "31"
        If BulanIni = "02" Then
            If TahunKabisat_TahunIni = True Then
                TanggalAkhirBulan = "29"
            Else
                TanggalAkhirBulan = "28"
            End If
        End If
        If BulanIni = "03" Then TanggalAkhirBulan = "31"
        If BulanIni = "04" Then TanggalAkhirBulan = "30"
        If BulanIni = "05" Then TanggalAkhirBulan = "31"
        If BulanIni = "06" Then TanggalAkhirBulan = "30"
        If BulanIni = "07" Then TanggalAkhirBulan = "31"
        If BulanIni = "08" Then TanggalAkhirBulan = "31"
        If BulanIni = "09" Then TanggalAkhirBulan = "30"
        If BulanIni = "10" Then TanggalAkhirBulan = "31"
        If BulanIni = "11" Then TanggalAkhirBulan = "30"
        If BulanIni = "12" Then TanggalAkhirBulan = "31"

    End Sub


    Public Sub IsiValueDataCompany()

        AksesDatabase_General(Buka)

        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_Company ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        ID_Customer = drKhusus.Item("ID_Customer")
        NamaPerusahaan = drKhusus.Item("Nama_Perusahaan")
        TaglinePerusahaan = drKhusus.Item("Tagline")
        NamaDirekturPerusahaan = drKhusus.Item("Nama_Direktur")
        NPWPPerusahaan = drKhusus.Item("NPWP")
        TanggalExpireSEPerusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_Expire_SE"))
        TanggalExpireSBUPerusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_Expire_SBU"))
        AlamatPerusahaan = drKhusus.Item("Alamat")
        EmailPerusahaan = drKhusus.Item("Email")
        KodeKPP_Perusahaan = drKhusus.Item("Kode_KPP")
        PICPerusahaan = drKhusus.Item("PIC")
        NomorSKTPerusahaan = drKhusus.Item("Nomor_SKT")
        TanggalSKTPerusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_SKT"))
        EmailDJPO_Perusahaan = drKhusus.Item("Email_DJPO")
        PasswordDJPO_Perusahaan = DekripsiTeks(drKhusus.Item("Password_DJPO"))
        TanggalPKP_Perusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_PKP"))
        AmbilValue_PerusahaanSebagaiPKP()
        AmbilValue_PerusahaanSebagaiPemotongPPh()
        TanggalExpireSEPerusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_Expire_SE"))
        Password_eFaktur_Perusahaan = DekripsiTeks(drKhusus.Item("Password_E_Faktur"))
        KodeAktivasiPerusahaan = DekripsiTeks(drKhusus.Item("Kode_Aktivasi"))
        PassphrasePerusahaan = DekripsiTeks(drKhusus.Item("Passphrase"))
        LevelPJK_Perusahaan = drKhusus.Item("Level_PJK")
        TanggalExpireSBUPerusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_Expire_SBU"))
        'Kelompok Kolom-kolom Baru atau yang mengalami perubahan harus pakai Try :
        Try
            JenisUsahaPerusahaan = drKhusus.Item("Jenis_Usaha")
            JenisWPPerusahaan = drKhusus.Item("Jenis_WP")
            NomorSuketUMKM_Perusahaan = drKhusus.Item("Nomor_Suket_UMKM")
            TanggalSuketUMKM_Perusahaan = TanggalFormatTampilan(drKhusus.Item("Tanggal_Suket_UMKM"))
            AmbilValue_PerusahaanSebagaiUMKM()
        Catch ex As Exception
        End Try

        cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_SC ", KoneksiDatabaseGeneral)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        JumlahPerangkat = DekripsiAngka(drKhusus.Item("SC_01"))
        If ValidasiDekripsiAngka = False Then
            MsgBox(teks_SistemTerkunci_HubungiDeveloper & Enter2Baris & "Error : SC_01")
            End
        End If
        Dim Value_SistemApprovalPerusahaan = DekripsiTeks(drKhusus.Item("SC_02"))
        If Value_SistemApprovalPerusahaan = Pilihan_YA_ Then
            SistemApprovalPerusahaan = True
        Else
            SistemApprovalPerusahaan = False
        End If
        TahunCutOff = DekripsiAngka(drKhusus.Item("SC_03"))
        If ValidasiDekripsiAngka = False Then
            MsgBox(teks_SistemTerkunci_HubungiDeveloper & Enter2Baris & "Error : SC_03")
            End
        End If
        SistemCOA = DekripsiTeks(drKhusus.Item("SC_04"))
        If Not (SistemCOA = SistemCOA_StandarAplikasi Or SistemCOA = SistemCOA_Customize) Then
            MsgBox(teks_SistemTerkunci_HubungiDeveloper & Enter2Baris & "Error : SC_04")
            End
        End If

        AksesDatabase_General(Tutup)

    End Sub

    Sub AmbilValue_PerusahaanSebagaiUMKM()
        If NomorSuketUMKM_Perusahaan = Kosongan Then PerusahaanSebagaiUMKM = False
        If NomorSuketUMKM_Perusahaan <> Kosongan Then PerusahaanSebagaiUMKM = True
    End Sub

    Sub AmbilValue_PerusahaanSebagaiPKP()
        If TanggalPKP_Perusahaan <> TanggalKosong Then PerusahaanSebagaiPKP = True
        If TanggalPKP_Perusahaan = TanggalKosong Then PerusahaanSebagaiPKP = False
    End Sub

    Sub AmbilValue_PerusahaanSebagaiPemotongPPh()
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT Pemotong_PPh FROM tbl_Company ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.Item("Pemotong_PPh") = 1 Then
            PerusahaanSebagaiPemotongPPh = True
        Else
            PerusahaanSebagaiPemotongPPh = False
        End If
        TutupDatabaseGeneral_Kondisional()
    End Sub

    Public Sub IsiValueJenisJurnal_SaranaPembayaran()
        'AksesDatabase_General(Buka)
        'cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_COA WHERE " & FilterListCOA_SaranaPembayaran & " AND Visibilitas = '" & Pilihan_Ya & "' ", KoneksiDatabaseGeneral)
        'dr = cmd.ExecuteReader
        'AksesDatabase_General(Tutup)
    End Sub

    'KUNCI INPUTAN HANYA BOLEH ANGKA
    Public Sub HanyaBolehInputAngkaPlus(sender As Object, e As KeyPressEventArgs)
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    'KUNCI INPUTAN HANYA BOLEH ANGKA
    Public Sub HanyaBolehInputAngkaPlusMinus(sender As Object, e As KeyPressEventArgs)
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack Or e.KeyChar = "-") Then e.Handled = True
    End Sub

    'KUNCI INPUTAN HANYA BOLEH ANGKA DESIMAL PLUS
    Public Sub HanyaBolehInputAngkaDesimalPlus(sender As Object, e As KeyPressEventArgs)
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack Or e.KeyChar = ",") Then e.Handled = True
    End Sub

    'KUNCI INPUTAN HANYA BOLEH NOMOR. Sebetulnya sama saja dengan fungsi di atas (HANYA BOLEH INPUT ANGKA). Ini hanya variasi kode saja.
    Public Sub HanyaBolehInputNomor(sender As Object, e As KeyPressEventArgs)
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    'KUNCI TOTAL INPUTAN
    Public Sub KunciTotalInputan(sender As Object, e As KeyPressEventArgs)
        Dim KarakterYangDiizinkan = ""
        e.Handled = Not (KarakterYangDiizinkan.Contains(e.KeyChar))
    End Sub

    'KUNCI INPUTAN DARI KARAKTER TERTENTU
    Public Sub TolakKarakterTertentu(sender As Object, e As KeyPressEventArgs)
        Dim KarakterYangTidakDiizinkan = "`~'/*+"
        e.Handled = (KarakterYangTidakDiizinkan.Contains(e.KeyChar))
    End Sub

    'KUNCI INPUTAN : HANYA BOLEH ANGKA DAN STRIP :
    Public Sub HanyaBoleh_Huruf_Angka_dan_Strip(sender As Object, e As KeyPressEventArgs)
        Dim KarakterYangDiizinkan
        KarakterYangDiizinkan = "1234567890-"
        KarakterYangDiizinkan = KarakterYangDiizinkan & "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        KarakterYangDiizinkan = KarakterYangDiizinkan & "abcdefghijklmnopqrstuvwxyz"
        KarakterYangDiizinkan = KarakterYangDiizinkan & Chr(8) '(Backspase)
        e.Handled = Not (KarakterYangDiizinkan.Contains(e.KeyChar))
    End Sub


    'KUNCI INPUTAN DARI KARAKTER TERTENTU
    Public Sub BukaKunciInputan(sender As Object, e As KeyPressEventArgs)
        Dim KarakterYangTidakDiizinkan = ""
        e.Handled = (KarakterYangTidakDiizinkan.Contains(e.KeyChar))
    End Sub

    'KUNCI TAHUN - HARUS SAMA DENGAN TAHUN BUKU AKTIF :
    Public Sub KunciTahun_HarusSamaDenganTahunBukuAktif(ByVal Tanggal As DateTimePicker)
        If Tanggal.Value.Year <> TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.Value.Day) = 29 And AmbilAngka(Tanggal.Value.Month) = 2 Then
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, 28)
            Else
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, Tanggal.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, hanya untuk Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI TAHUN - TIDAK BOLEH LEBIH DARI TAHUN BUKU AKTIF :
    Public Sub KunciTahun_TidakBolehLebihDariTahunBukuAktif(ByVal Tanggal As DateTimePicker)
        If Tanggal.Value.Year > TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.Value.Day) = 29 And AmbilAngka(Tanggal.Value.Month) = 2 Then
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, 28)
            Else
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, Tanggal.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh melebihi Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI TAHUN - TIDAK BOLEH LEBIH DARI TAHUN BUKU AKTIF :
    Public Sub KunciTahun_TidakBolehKurangDariTahunBukuAktif(ByVal Tanggal As DateTimePicker)
        If Tanggal.Value.Year < TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.Value.Day) = 29 And AmbilAngka(Tanggal.Value.Month) = 2 Then
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, 28)
            Else
                Tanggal.Value = New Date(TahunBukuAktif, Tanggal.Value.Month, Tanggal.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh kurang dari Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI BULAN DAN TAHUN : HARUS SAMA
    Public Sub KunciBulanDanTahun_HarusSama(ByVal Tanggal As DateTimePicker, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim Tahun = Tanggal.Value.Year
        Dim Bulan = Tanggal.Value.Month
        Dim Hari As Integer = AmbilAngka(Tanggal.Value.Day)
        If Bulan = 0 Or Hari = 0 Then Return
        If Bulan <> BulanPengunci Or Tahun <> TahunPengunci Then
            Bulan = BulanPengunci
            Tahun = TahunPengunci
            Dim PembatasHari As Integer = AmbilAngka(Left(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan, Tahun), 2))
            If Hari > PembatasHari Then Hari = PembatasHari
            Tanggal.Value = New Date(Tahun, Bulan, Hari)
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, hanya untuk Bulan " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI BULAN DAN TAHUN : TIDAK BOLEH LEBIH
    Public Sub KunciBulanDanTahun_TidakBolehLebih(ByVal Tanggal As DateTimePicker, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim ValiditasInputan As Boolean = True
        Dim Tahun = Tanggal.Value.Year
        Dim Bulan = Tanggal.Value.Month
        If Tahun > TahunPengunci Then
            Tahun = TahunPengunci
            ValiditasInputan = False
        End If
        If Bulan > BulanPengunci And Tahun = TahunPengunci Then
            Bulan = BulanPengunci
            ValiditasInputan = False
        End If
        Tanggal.Value = New Date(Tahun, Bulan, Tanggal.Value.Day)
        If ValiditasInputan = False Then
            Dim BulanString = Nothing
            Select Case Bulan
                Case 1
                    BulanString = Bulan_Januari
                Case 2
                    BulanString = Bulan_Februari
                Case 3
                    BulanString = Bulan_Maret
                Case 4
                    BulanString = Bulan_April
                Case 5
                    BulanString = Bulan_Mei
                Case 6
                    BulanString = Bulan_Juni
                Case 7
                    BulanString = Bulan_Juli
                Case 8
                    BulanString = Bulan_Agustus
                Case 9
                    BulanString = Bulan_September
                Case 10
                    BulanString = Bulan_Oktober
                Case 11
                    BulanString = Bulan_Nopember
                Case 12
                    BulanString = Bulan_Desember
            End Select
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh melebihi Bulan " & BulanString & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI TANGGAL, BULAN DAN TAHUN : TIDAK BOLEH LEBIH DARI
    Public Sub KunciTanggalBulanDanTahun_TidakBolehLebihDari(ByVal Tanggal As DateTimePicker, ByVal HariPengunci As Integer, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim ValiditasInputan As Boolean = True
        Dim Tahun = Tanggal.Value.Year
        Dim Bulan = Tanggal.Value.Month
        Dim Hari = Tanggal.Value.Day
        If Tahun > TahunPengunci Then
            Tahun = TahunPengunci
            ValiditasInputan = False
        End If
        If Bulan > BulanPengunci And Tahun = TahunPengunci Then
            Bulan = BulanPengunci
            ValiditasInputan = False
        End If
        If Hari > HariPengunci And Bulan = BulanPengunci And Tahun = TahunPengunci Then
            Hari = HariPengunci
            ValiditasInputan = False
        End If
        Tanggal.Value = New Date(Tahun, Bulan, Hari)
        If ValiditasInputan = False Then
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh melebihi Tanggal " & Hari & " " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub

    'KUNCI TANGGAL, BULAN DAN TAHUN : TIDAK BOLEH KURANG DARI
    Public Sub KunciTanggalBulanDanTahun_TidakBolehKurangDari(ByVal Tanggal As DateTimePicker, ByVal HariPengunci As Integer, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim ValiditasInputan As Boolean = True
        Dim Tahun = Tanggal.Value.Year
        Dim Bulan = Tanggal.Value.Month
        Dim Hari = Tanggal.Value.Day
        If Tahun < TahunPengunci Then
            Tahun = TahunPengunci
            ValiditasInputan = False
        End If
        If Bulan < BulanPengunci And Tahun = TahunPengunci Then
            Bulan = BulanPengunci
            ValiditasInputan = False
        End If
        If Hari < HariPengunci And Bulan = BulanPengunci And Tahun = TahunPengunci Then
            Hari = HariPengunci
            ValiditasInputan = False
        End If
        Tanggal.Value = New Date(Tahun, Bulan, Hari)
        If ValiditasInputan = False Then
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh melebihi Tanggal " & Hari & " " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub

    'PENENTUAN DEBET/KREDIT COA
    Public Function PenentuanDK_COA(COA)
        Dim DK = Kosongan
        Select Case AmbilAngka(AmbilTeksKiri(COA, 1))
            Case 1
                DK = dk_D
            Case 2
                DK = dk_K
            Case 3
                DK = dk_K
            Case 4
                DK = dk_K
            Case 5
                DK = dk_D
            Case 6
                DK = dk_D
            Case 7
                DK = dk_K
            Case 8
                DK = dk_D
        End Select
        Return DK
    End Function
    Public Function PenentuanDebetKreditCOA(COA)
        Dim DebetKredit = Kosongan
        If PenentuanDK_COA(COA) = dk_D Then DebetKredit = "Debet"
        If PenentuanDK_COA(COA) = dk_K Then DebetKredit = "Kredit"
        Return DebetKredit
    End Function
    Public Function PenentuanDEBETKREDIT_COA(COA)
        Dim DEBETKREDIT = Kosongan
        If PenentuanDK_COA(COA) = dk_D Then DEBETKREDIT = "DEBET"
        If PenentuanDK_COA(COA) = dk_K Then DEBETKREDIT = "KREDIT"
        Return DEBETKREDIT
    End Function

    'AMBIL VALUE NAMA MITRA (SUPPLIER/CUSTOMER) :
    Public Function AmbilValue_NamaMitra(ByVal KodeMitra As String) As String
        Dim NamaMitra = Kosongan
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Nama_Mitra FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                NamaMitra = drMitra.Item("Nama_Mitra")
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return NamaMitra
    End Function
    Public Function AmbilValue_NamaSupplier(KodeSupplier As String) As String
        Return AmbilValue_NamaMitra(KodeSupplier)
    End Function
    Public Function AmbilValue_NamaCustomer(KodeCustomer As String) As String
        Return AmbilValue_NamaMitra(KodeCustomer)
    End Function
    Public Function AmbilValue_NamaPemegangSaham(ByVal KodePemegangSaham As String) As String
        Return AmbilValue_NamaMitra(KodePemegangSaham)
    End Function



    'AMBIL VALUE NPWP MITRA (SUPPLIER/CUSTOMER) :
    Public Function AmbilValue_NPWPMitra(ByVal KodeMitra As String) As String
        Dim NPWP = Kosongan
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT NPWP FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                NPWP = drMitra.Item("NPWP")
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return NPWP
    End Function
    Public Function AmbilValue_NPWPSupplier(KodeSupplier As String) As String
        Return AmbilValue_NamaMitra(KodeSupplier)
    End Function
    Public Function AmbilValue_NPWPCustomer(KodeCustomer As String) As String
        Return AmbilValue_NamaMitra(KodeCustomer)
    End Function
    Public Function AmbilValue_NPWPPemegangSaham(ByVal KodePemegangSaham As String) As String
        Return AmbilValue_NamaMitra(KodePemegangSaham)
    End Function


    'AMBIL VALUE PIC MITRA (SUPPLIER/CUSTOMER) :
    Public Function AmbilValue_PICMitra(ByVal KodeMitra As String) As String
        Dim PICMitra = Kosongan
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT PIC FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                PICMitra = drMitra.Item("PIC")
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return PICMitra
    End Function
    Public Function AmbilValue_PICSupplier(KodeSupplier As String) As String
        Return AmbilValue_PICMitra(KodeSupplier)
    End Function
    Public Function AmbilValue_PICCustomer(KodeSupplier As String) As String
        Return AmbilValue_PICMitra(KodeSupplier)
    End Function


    'AMBIL VALUE ALAMAT MITRA (SUPPLIER/CUSTOMER) :
    Public Function AmbilValue_AlamatMitra(ByVal KodeMitra As String) As String
        Dim AlamatMitra = Kosongan
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Alamat FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                AlamatMitra = drMitra.Item("Alamat")
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return AlamatMitra
    End Function
    Public Function AmbilValue_AlamatSupplier(KodeSupplier As String) As String
        Return AmbilValue_AlamatMitra(KodeSupplier)
    End Function
    Public Function AmbilValue_AlamatCustomer(KodeSupplier As String) As String
        Return AmbilValue_AlamatMitra(KodeSupplier)
    End Function


    'AMBIL VALUE REKENING LAWAN TRANSAKSI :
    Public Function AmbilValue_RekeningMitra(ByVal KodeLawanTransaksi As String) As String
        Dim RekeningLawanTransaksi = Kosongan
        Dim cmdLT As OdbcCommand
        Dim drLT As OdbcDataReader
        If KodeLawanTransaksi <> Kosongan Then
            Select Case KodeLawanTransaksi
                Case KodeLawanTransaksi_BPJS_KS
                    RekeningLawanTransaksi = NamaLawanTransaksi_BpjsKesehatan
                Case KodeLawanTransaksi_BPJS_TK
                    RekeningLawanTransaksi = NamaLawanTransaksi_BpjsKetenagakerjaan
                Case KodeLawanTransaksi_Karyawan
                    RekeningLawanTransaksi = "[ REKENING KOLEKTIF ]"
                Case KodeLawanTransaksi_KoperasiKaryawan
                    RekeningLawanTransaksi = "[ REKENING BELUM TERDAFTAR ]"
                Case KodeLawanTransaksi_SerikatPekerja
                    RekeningLawanTransaksi = "[ REKENING BELUM TERDAFTAR ]"
                Case KodeLawanTransaksi_DJP
                    RekeningLawanTransaksi = NamaLawanTransaksi_DJP
                Case Else
                    RekeningLawanTransaksi = Kosongan
            End Select
            BukaDatabaseGeneral_Kondisional()
            'Cari Rekening di Data Mitra :
            If RekeningLawanTransaksi = Kosongan Then
                cmdLT = New OdbcCommand(" SELECT Rekening_Bank FROM tbl_LawanTransaksi " &
                                        " WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
                drLT = cmdLT.ExecuteReader
                drLT.Read()
                If drLT.HasRows Then
                    RekeningLawanTransaksi = drLT.Item("Rekening_Bank")
                Else
                    RekeningLawanTransaksi = Kosongan
                End If
            End If
            'Cari Rekening di Data Karyawan :
            If RekeningLawanTransaksi = Kosongan Then
                cmdLT = New OdbcCommand(" SELECT Rekening_Bank FROM tbl_DataKaryawan " &
                                        " WHERE Nomor_ID_Karyawan = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
                drLT = cmdLT.ExecuteReader
                drLT.Read()
                If drLT.HasRows Then
                    RekeningLawanTransaksi = drLT.Item("Rekening_Bank")
                Else
                    RekeningLawanTransaksi = Kosongan
                End If
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return RekeningLawanTransaksi
    End Function


    'AMBIL VALUE ATAS NAMA REKENING LAWAN TRANSAKSI :
    Public Function AmbilValue_AtasNamaRekeningMitra(ByVal KodeLawanTransaksi As String) As String
        Dim AtasNamaRekeningLawanTransaksi = Kosongan
        Dim cmdLT As OdbcCommand
        Dim drLT As OdbcDataReader
        If KodeLawanTransaksi <> Kosongan Then
            Select Case KodeLawanTransaksi
                Case KodeLawanTransaksi_BPJS_KS
                    AtasNamaRekeningLawanTransaksi = NamaLawanTransaksi_BpjsKesehatan
                Case KodeLawanTransaksi_BPJS_TK
                    AtasNamaRekeningLawanTransaksi = NamaLawanTransaksi_BpjsKetenagakerjaan
                Case KodeLawanTransaksi_Karyawan
                    AtasNamaRekeningLawanTransaksi = "[ REKENING KOLEKTIF ]"
                Case KodeLawanTransaksi_KoperasiKaryawan
                    AtasNamaRekeningLawanTransaksi = "[ REKENING BELUM TERDAFTAR ]"
                Case KodeLawanTransaksi_SerikatPekerja
                    AtasNamaRekeningLawanTransaksi = "[ REKENING BELUM TERDAFTAR ]"
                Case KodeLawanTransaksi_DJP
                    AtasNamaRekeningLawanTransaksi = NamaLawanTransaksi_DJP
                Case Else
                    AtasNamaRekeningLawanTransaksi = Kosongan
            End Select
            BukaDatabaseGeneral_Kondisional()
            'Cari Rekening di Data Mitra :
            If AtasNamaRekeningLawanTransaksi = Kosongan Then
                cmdLT = New OdbcCommand(" SELECT Atas_Nama FROM tbl_LawanTransaksi " &
                                        " WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
                drLT = cmdLT.ExecuteReader
                drLT.Read()
                If drLT.HasRows Then
                    AtasNamaRekeningLawanTransaksi = drLT.Item("Atas_Nama")
                Else
                    AtasNamaRekeningLawanTransaksi = Kosongan
                End If
            End If
            'Cari Rekening di Data Karyawan :
            If AtasNamaRekeningLawanTransaksi = Kosongan Then
                cmdLT = New OdbcCommand(" SELECT Atas_Nama FROM tbl_DataKaryawan " &
                                        " WHERE Nomor_ID_Karyawan = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
                drLT = cmdLT.ExecuteReader
                drLT.Read()
                If drLT.HasRows Then
                    AtasNamaRekeningLawanTransaksi = drLT.Item("Atas_Nama")
                Else
                    AtasNamaRekeningLawanTransaksi = Kosongan
                End If
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return AtasNamaRekeningLawanTransaksi
    End Function


    'AMBIL VALUE MITRA SEBAGAI PKP
    Public Function MitraSebagaiPKP(KodeMitra As String) As Boolean
        Dim SebagaiPKP As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT PKP FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If AmbilAngka(drMitra.Item("PKP")) = 1 Then SebagaiPKP = True
                If AmbilAngka(drMitra.Item("PKP")) = 0 Then SebagaiPKP = False
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SebagaiPKP
    End Function
    Public Function SupplierSebagaiPKP(KodeSupplier As String) As Boolean
        Return MitraSebagaiPKP(KodeSupplier)
    End Function
    Public Function CustomerSebagaiPKP(KodeCustomer As String) As Boolean
        Return MitraSebagaiPKP(KodeCustomer)
    End Function


    'AMBIL VALUE MITRA SEBAGAI UMKM
    Public Function MitraSebagaiUMKM(KodeMitra As String) As Boolean
        Dim SebagaiUMKM As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT UMKM FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If AmbilAngka(drMitra.Item("UMKM")) = 1 Then SebagaiUMKM = True
                If AmbilAngka(drMitra.Item("UMKM")) = 0 Then SebagaiUMKM = False
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SebagaiUMKM
    End Function
    Public Function SupplierSebagaiUMKM(KodeSupplier As String) As Boolean
        Return MitraSebagaiUMKM(KodeSupplier)
    End Function
    Public Function CustomerSebagaiUMKM(KodeCustomer As String) As Boolean
        Return MitraSebagaiUMKM(KodeCustomer)
    End Function


    'AMBIL VALUE MITRA SEBAGAI PEMOTONG PPh
    Public Function MitraSebagaiPemotongPPh(KodeMitra As String) As Boolean
        Dim SebagaiPemotongPPh As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Pemotong_PPh FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If AmbilAngka(drMitra.Item("Pemotong_PPh")) = 1 Then SebagaiPemotongPPh = True
                If AmbilAngka(drMitra.Item("Pemotong_PPh")) = 0 Then SebagaiPemotongPPh = False
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SebagaiPemotongPPh
    End Function
    Public Function SupplierSebagaiPemotongPPh(KodeSupplier As String) As Boolean
        Return MitraSebagaiPemotongPPh(KodeSupplier)
    End Function
    Public Function CustomerSebagaiPemotongPPh(KodeCustomer As String) As Boolean
        Return MitraSebagaiPemotongPPh(KodeCustomer)
    End Function


    'AMBIL VALUE MITRA SEBAGAI AFILIASI :
    Public Function MitraSebagaiAfiliasi(KodeMitra As String) As Boolean
        Dim SebagaiAfiliasi As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Afiliasi FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If AmbilAngka(drMitra.Item("Afiliasi")) = 1 Then SebagaiAfiliasi = True
                If AmbilAngka(drMitra.Item("Afiliasi")) = 0 Then SebagaiAfiliasi = False
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SebagaiAfiliasi
    End Function
    Public Function SupplierSebagaiAfiliasi(KodeSupplier As String) As Boolean
        Return MitraSebagaiAfiliasi(KodeSupplier)
    End Function
    Public Function CustomerSebagaiAfiliasi(KodeCustomer As String) As Boolean
        Return MitraSebagaiAfiliasi(KodeCustomer)
    End Function

    'AMBIL VALUE MITRA SEBAGAI PERUSAHAAN LUAR NEGERI :
    Public Function MitraSebagaiPerusahaanLuarNegeri(KodeMitra As String) As Boolean
        Dim SebagaiPerusahaanLuarNegeri As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Lokasi_WP FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If drMitra.Item("Lokasi_WP") = LokasiWP_LuarNegeri Then
                    SebagaiPerusahaanLuarNegeri = True
                Else
                    SebagaiPerusahaanLuarNegeri = False
                End If
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SebagaiPerusahaanLuarNegeri
    End Function
    Public Function SupplierSebagaiPerusahaanLuarNegeri(KodeSupplier As String) As Boolean
        Return MitraSebagaiPerusahaanLuarNegeri(KodeSupplier)
    End Function
    Public Function CustomerSebagaiPerusahaanLuarNegeri(KodeCustomer As String) As Boolean
        Return MitraSebagaiPerusahaanLuarNegeri(KodeCustomer)
    End Function
    Public Function AmbilValue_PemegangDariSahamLuarNegeri(KodePemegangSaham As String) As Boolean
        Return MitraSebagaiPerusahaanLuarNegeri(KodePemegangSaham)
    End Function


    'AMBIL VALUE MITRA BERBADAN HUKUM :
    Public Function MitraBerbadanHukum(KodeMitra As String) As Boolean
        Dim BerbadanHukum As Boolean
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        If KodeMitra <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdMitra = New OdbcCommand(" SELECT Jenis_WP FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeMitra & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then
                If drMitra.Item("Jenis_WP") = JenisWP_BadanHukum Then
                    BerbadanHukum = True
                Else
                    BerbadanHukum = False
                End If
            Else
                PesanUntukProgrammer("Kode Mitra tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return BerbadanHukum
    End Function
    Public Function SupplierBerbadanHukum(KodeSupplier As String) As Boolean
        Return MitraBerbadanHukum(KodeSupplier)
    End Function
    Public Function CustomerBerbadanHukum(KodeCustomer As String) As Boolean
        Return MitraBerbadanHukum(KodeCustomer)
    End Function
    Public Function AmbilValue_PemegangSahamBerbadanHukum(KodePemegangSaham As String) As Boolean
        Return MitraBerbadanHukum(KodePemegangSaham)
    End Function


    'AMBIL VALUE NAMA KARYAWAN :
    Public Function AmbilValue_NamaKaryawan(ByVal NomorIDKaryawan As String) As String
        Dim NamaKaryawan = Kosongan
        Dim cmdKaryawan As OdbcCommand
        Dim drKaryawan As OdbcDataReader
        If NomorIDKaryawan <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKaryawan = New OdbcCommand(" SELECT Nama_Karyawan FROM tbl_DataKaryawan WHERE Nomor_ID_Karyawan = '" & NomorIDKaryawan & "' ", KoneksiDatabaseGeneral)
            drKaryawan = cmdKaryawan.ExecuteReader
            drKaryawan.Read()
            If drKaryawan.HasRows Then
                NamaKaryawan = drKaryawan.Item("Nama_Karyawan")
            Else
                PesanUntukProgrammer("Nomor ID Karyawan tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return NamaKaryawan
    End Function

    'AMBIL VALUE JABATAN KARYAWAN :
    Public Function AmbilValue_JabatanKaryawan(ByVal NomorIDKaryawan As String) As String
        Dim JabatanKaryawan = Kosongan
        Dim cmdKaryawan As OdbcCommand
        Dim drKaryawan As OdbcDataReader
        If NomorIDKaryawan <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKaryawan = New OdbcCommand(" SELECT Jabatan FROM tbl_DataKaryawan WHERE Nomor_ID_Karyawan = '" & NomorIDKaryawan & "' ", KoneksiDatabaseGeneral)
            drKaryawan = cmdKaryawan.ExecuteReader
            drKaryawan.Read()
            If drKaryawan.HasRows Then
                JabatanKaryawan = drKaryawan.Item("Jabatan")
            Else
                PesanUntukProgrammer("Nomor ID Karyawan tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return JabatanKaryawan
    End Function


    'AMBIL VALUE NAMA AKUN :
    Public Function AmbilValue_NamaAkun(ByVal COA As String) As String
        Dim NamaAkun = Kosongan
        Dim cmdAkun As OdbcCommand
        Dim drAkun As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdAkun = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drAkun = cmdAkun.ExecuteReader
            drAkun.Read()
            If drAkun.HasRows Then
                NamaAkun = drAkun.Item("Nama_Akun")
                jur_AkunTerdaftar = True
            Else
                jur_AkunTerdaftar = False
                PesanUntukProgrammer("Akun tidak terdaftar...!!!" & Enter2Baris &
                                     "Pesan ini berasal dari Sub Function 'AmbilValue_NamaAkun'.")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return NamaAkun
    End Function


    'AMBIL VALUE KODE MATA UANG BERDASARKAN COA :
    Public Function AmbilValue_KodeMataUang_BerdasarkanCOA(ByVal COA As String) As String
        Dim KodeMataUang = Kosongan
        Dim cmdAkun As OdbcCommand
        Dim drAkun As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdAkun = New OdbcCommand(" SELECT Kode_Mata_Uang FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drAkun = cmdAkun.ExecuteReader
            drAkun.Read()
            If drAkun.HasRows Then
                KodeMataUang = drAkun.Item("Kode_Mata_Uang")
                jur_AkunTerdaftar = True
            Else
                jur_AkunTerdaftar = False
                PesanUntukProgrammer("Akun tidak terdaftar...!!!" & Enter2Baris &
                                     "Pesan ini berasal dari Sub Function 'AmbilValue_KodeMataUang_BerdasarkanCOA'.")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return KodeMataUang
    End Function



    'COA / SARANA PEMBAYARAN TERMASUK BANK :
    Public Function COATermasukBank(COA)
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukBank As Boolean
        If AmbilAngka(COA) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COA) <= kodeakun_Bank_Akhir _
            Then
            TermasukBank = True
        Else
            TermasukBank = False
        End If
        Return TermasukBank
    End Function
    Public Function SaranaPembayaranTermasukBank(SaranaPembayaran)
        Dim COA = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        Dim TermasukBank As Boolean
        If AmbilAngka(COA) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COA) <= kodeakun_Bank_Akhir _
            Then
            TermasukBank = True
        Else
            TermasukBank = False
        End If
        Return TermasukBank
    End Function


    'COA / SARANA PEMBAYARAN TERMASUK PETTY CASH :
    Public Function COATermasukPettyCash(COA)
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukPettyCash As Boolean
        If AmbilAngka(COA) >= KodeAkun_PettyCash_Awal _
            And AmbilAngka(COA) <= kodeakun_PettyCash_Akhir _
            Then
            TermasukPettyCash = True
        Else
            TermasukPettyCash = False
        End If
        Return TermasukPettyCash
    End Function
    Public Function SaranaPembayaranTermasukPettyCash(SaranaPembayaran)
        Dim COA = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        Dim TermasukPettyCash As Boolean
        If AmbilAngka(COA) >= KodeAkun_PettyCash_Awal _
            And AmbilAngka(COA) <= kodeakun_PettyCash_Akhir _
            Then
            TermasukPettyCash = True
        Else
            TermasukPettyCash = False
        End If
        Return TermasukPettyCash
    End Function


    'COA TERMASUK ASSET :
    Public Function COATermasukAsset(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukAsset As Boolean
        If AmbilTeksKiri(COA, 2) = "12" And AmbilTeksKanan(COA, 1).ToString = "0" Then
            TermasukAsset = True
            If AmbilTeksKiri(COA, 3) = "129" Then TermasukAsset = False  '(Pengecualian)
        Else
            TermasukAsset = False
        End If
        Return TermasukAsset
    End Function


    'COA TERMASUK ASSET DAN BANGUNAN:
    Public Function COATermasukAssetTanahDanBangunan(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukAssetTanahDanBangunan As Boolean
        If AmbilTeksKiri(COA, 3) = "121" Or AmbilTeksKiri(COA, 3) = "122" Then
            TermasukAssetTanahDanBangunan = True
        Else
            TermasukAssetTanahDanBangunan = False
        End If
        Return TermasukAssetTanahDanBangunan
    End Function


    'COA TERMASUK BIAYA DIBAYAR DIMUKA:
    Public Function COATermasukBiayaDibayarDimuka(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukBiayaDibayarDimuka As Boolean
        If AmbilTeksKiri(COA, 3) = "116" Then
            TermasukBiayaDibayarDimuka = True
        Else
            TermasukBiayaDibayarDimuka = False
        End If
        Return TermasukBiayaDibayarDimuka
    End Function

    'COA TERMASUK NERACA :
    Public Function COATermasukNeraca(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukNeraca As Boolean
        If AmbilTeksKiri(COA, 1) = "1" Or AmbilTeksKiri(COA, 1) = "2" Or AmbilTeksKiri(COA, 1) = "3" Then
            TermasukNeraca = True
        Else
            TermasukNeraca = False
        End If
        Return TermasukNeraca
    End Function

    'COA TERMASUK LABA/RUGI (PENDAPATAN DAN BIAYA) :
    Public Function COATermasukLabaRugi(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukLabaRugi As Boolean
        If AmbilTeksKiri(COA, 1) = "4" Or AmbilTeksKiri(COA, 1) = "5" Or AmbilTeksKiri(COA, 1) = "6" Or AmbilTeksKiri(COA, 1) = "7" Or AmbilTeksKiri(COA, 1) = "8" Then
            TermasukLabaRugi = True
        Else
            TermasukLabaRugi = False
        End If
        Return TermasukLabaRugi
    End Function

    'COA TERMASUK PENDAPATAN :
    Public Function COATermasukPendapatan(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukPendapatan As Boolean
        If AmbilTeksKiri(COA, 1) = "4" Or AmbilTeksKiri(COA, 1) = "7" Then
            TermasukPendapatan = True
        Else
            TermasukPendapatan = False
        End If
        Return TermasukPendapatan
    End Function

    'COA TERMASUK BIAYA :
    Public Function COATermasukBiaya(COA) As Boolean
        COA = Left(COA, JumlahDigitCOA)
        Dim TermasukBiaya As Boolean
        If AmbilTeksKiri(COA, 1) = "5" Or AmbilTeksKiri(COA, 1) = "6" Or AmbilTeksKiri(COA, 1) = "8" Then
            TermasukBiaya = True
        Else
            TermasukBiaya = False
        End If
        Return TermasukBiaya
    End Function

    'KEPALA COA :
    Public Function KepalaCOA(COA As String, Kepala As String) As Boolean
        Dim JumlahDigitKepalaCOA As Integer = Len(Kepala)
        Dim Termasuk As Boolean
        COA = Left(COA, JumlahDigitCOA)
        If AmbilTeksKiri(COA, JumlahDigitKepalaCOA) = Kepala Then
            Termasuk = True
        Else
            Termasuk = False
        End If
        Return Termasuk
    End Function


    'KONVERSI COA KE SARANA PEMBAYARAN :
    Public Function KonversiCOAKeSaranaPembayaran(ByVal COA As String) As String
        Dim NamaAkun = Kosongan
        Dim cmdAkun As OdbcCommand
        Dim drAkun As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdAkun = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drAkun = cmdAkun.ExecuteReader
            drAkun.Read()
            If drAkun.HasRows Then
                NamaAkun = drAkun.Item("Nama_Akun")
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return COA & StripKosong & NamaAkun
    End Function

    'KONVERSI SARANA PEMBAYARAN KE COA :
    Public Function KonversiSaranaPembayaranKeCOA(ByVal SaranaPembayaran As String) As String
        Dim COA = Left(SaranaPembayaran, JumlahDigitCOA)
        Return COA
    End Function

    'KONVERSI SARANA PEMBAYARAN KE NAMA AKUN :
    Public Function KonversiSaranaPembayaranKeNamaAkun(ByVal SaranaPembayaran As String) As String
        Dim COA = Left(SaranaPembayaran, JumlahDigitCOA)
        Dim NamaAkun = AmbilValue_NamaAkun(COA)
        Return NamaAkun
    End Function


    'KONVERSI PERUNTUKAN PEMBAYARAN KE COA :
    Public Function KonversiPeruntukanKeCOA(ByVal Peruntukan As String) As String
        Dim COA = Kosongan
        Select Case Peruntukan
            '======== PENERIMAAN= ================================
            'Pencairan Piutang :
            Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                COA = KodeTautanCOA_PiutangUsaha_NonAfiliasi
            Case Peruntukan_PencairanPiutangUsaha_Afiliasi
                COA = KodeTautanCOA_PiutangUsaha_Afiliasi
            Case Peruntukan_PencairanPiutangAfiliasi
                COA = KodeTautanCOA_PiutangAfiliasi
            Case Peruntukan_PencairanPiutangPihakKetiga
                COA = KodeTautanCOA_PiutangPihakKetiga
            Case Peruntukan_PencairanPiutangPemegangSaham
                COA = KodeTautanCOA_PiutangPemegangSaham
            Case Peruntukan_PencairanPiutangDividen
                COA = KodeTautanCOA_PiutangDividen
            Case Peruntukan_PencairanPiutangKaryawan
                COA = KodeTautanCOA_PiutangKaryawan
            Case Peruntukan_PencairanPiutangLainnya
                COA = KodeTautanCOA_PiutangLainnya
            'Penerimaan Tunai :
            Case Peruntukan_HutangAfiliasi
                COA = KodeTautanCOA_HutangAfiliasi
            Case Peruntukan_HutangPihakKetiga
                COA = KodeTautanCOA_HutangPihakKetiga
            Case Peruntukan_HutangPemegangSaham
                COA = KodeTautanCOA_HutangPemegangSaham
            Case Peruntukan_HutangKaryawan
                COA = KodeTautanCOA_HutangKaryawan
            Case Peruntukan_HutangBank
                COA = KodeTautanCOA_PiutangLainnya '(Ini sudah benar menggunakan COA Piutang Lainnya, bukan COA Hutang Bank)
            Case Peruntukan_HutangLeasing
                COA = KodeTautanCOA_PiutangLainnya '(Ini sudah benar menggunakan COA Piutang Lainnya, bukan COA Hutang Leasing)
            '======== PENGELUARAN ================================
            'Pembayaran Hutang :
            Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                COA = KodeTautanCOA_HutangUsaha_NonAfiliasi
            Case Peruntukan_PembayaranHutangUsaha_Afiliasi
                COA = KodeTautanCOA_HutangUsaha_Afiliasi
            Case Peruntukan_PembayaranHutangBiaya
                COA = KodeTautanCOA_HutangBiaya
            Case Peruntukan_PembayaranHutangGaji
                COA = KodeTautanCOA_HutangGaji
            Case Peruntukan_PembayaranHutangBPJSKesehatan
                COA = KodeTautanCOA_HutangBpjsKesehatan
            Case Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                COA = KodeTautanCOA_HutangBpjsKetenagakerjaan
            Case Peruntukan_PembayaranHutangKoperasiKaryawan
                COA = KodeTautanCOA_HutangKoperasiKaryawan
            Case Peruntukan_PembayaranHutangSerikat
                COA = KodeTautanCOA_HutangSerikat
            Case Peruntukan_PembayaranHutangPihakKetiga
                COA = KodeTautanCOA_HutangPihakKetiga
            Case Peruntukan_PembayaranHutangKaryawan
                COA = KodeTautanCOA_HutangKaryawan
            Case Peruntukan_PembayaranHutangLancarLainnya
                COA = KodeTautanCOA_HutangLancarLainnya
            Case Peruntukan_PembayaranHutangLeasing
                COA = KodeTautanCOA_HutangLeasing
            Case Peruntukan_PembayaranHutangBank
                COA = KodeTautanCOA_HutangBank
            Case Peruntukan_PembayaranHutangPemegangSaham
                COA = KodeTautanCOA_HutangPemegangSaham
            Case Peruntukan_PembayaranHutangAfiliasi
                COA = KodeTautanCOA_HutangAfiliasi
            Case Peruntukan_PembayaranHutangPajak
                COA = Kosongan
            Case Peruntukan_PembayaranHutangDividen
                COA = KodeTautanCOA_HutangDividen
            'Pengeluaran Tunai :
            Case Peruntukan_PiutangPemegangSaham
                COA = KodeTautanCOA_PiutangPemegangSaham
            Case Peruntukan_PiutangKaryawan
                COA = KodeTautanCOA_PiutangKaryawan
            Case Peruntukan_PiutangPihakKetiga
                COA = KodeTautanCOA_PiutangPihakKetiga
            Case Peruntukan_PiutangAfiliasi
                COA = KodeTautanCOA_PiutangAfiliasi
            Case Peruntukan_DepositOperasional
                COA = KodeTautanCOA_HutangDeposit
            Case Peruntukan_BankGaransi
                COA = KodeTautanCOA_BankGaransi
            'Investasi:
            Case Peruntukan_InvestasiModal
                COA = KodeTautanCOA_Modal
            Case Peruntukan_InvestasiDeposito
                COA = KodeTautanCOA_InvestasiDeposito
            Case Peruntukan_InvestasiSuratBerharga
                COA = KodeTautanCOA_InvestasiSuratBerharga
            Case Peruntukan_InvestasiLogamMulia
                COA = KodeTautanCOA_InvestasiLogamMulia
            Case Peruntukan_InvestasiPadaPerusahaanAnak
                COA = KodeTautanCOA_InvestasiPadaPerusahaanAnak
            Case Peruntukan_InvestasiGoodWill
                COA = KodeTautanCOA_InvestasiGoodWill
        End Select

        If COA = Kosongan And Peruntukan <> Peruntukan_PembayaranHutangPajak Then
            PesanUntukProgrammer("COA/Peruntukan belum terdaftar..!")
        End If
        Return COA
    End Function



    'KONVERSI SARANA PEMBAYARAN KE JENIS JURNAL :
    Public Function KonversiSaranaPembayaranKeJenisJurnal(ByVal SaranaPembayaran As String) As String
        Dim JenisJurnal = Mid(SaranaPembayaran, JumlahDigitCOA + Len(StripPemisah) + 1)
        Return JenisJurnal
    End Function


    'PENENTUAN COA BIAYA PENYUSUTAN :
    Public Function PenentuanCOA_BiayaPenyusutan(ByVal COA_Asset) As String
        Dim COA_BiayaPenyusutan = Kosongan
        If COA_Asset <> Kosongan Then
            Select Case COA_Asset
                Case 12210
                    COA_BiayaPenyusutan = 61621
                Case 12220
                    COA_BiayaPenyusutan = 61622
                Case 12230
                    COA_BiayaPenyusutan = 53321
                Case 12240
                    COA_BiayaPenyusutan = 53322
                Case 12310
                    COA_BiayaPenyusutan = 61631
                Case 12320
                    COA_BiayaPenyusutan = 53331
                Case 12330
                    COA_BiayaPenyusutan = 53332
                Case 12340
                    COA_BiayaPenyusutan = 61632
                Case 12410
                    COA_BiayaPenyusutan = 61641
                Case 12420
                    COA_BiayaPenyusutan = 53341
                Case 12430
                    COA_BiayaPenyusutan = 53342
                Case 12500
                    COA_BiayaPenyusutan = 61651
                Case Else
                    COA_BiayaPenyusutan = Kosongan
            End Select

        End If
        Return COA_BiayaPenyusutan.ToString
    End Function


    'PENENTUAN COA AKUMULASI PENYUSUTAN :
    Public Function PenentuanCOA_AkumulasiPenyusutan(ByVal COA_Asset As String) As String
        Dim COA_AkumulasiPenyusutan As String = Kosongan
        If COA_Asset <> Kosongan Then COA_AkumulasiPenyusutan = Left(COA_Asset, JumlahDigitCOA - 1).ToString & "1".ToString
        If AmbilTeksKiri(COA_Asset, 3) = "121" Then
            COA_AkumulasiPenyusutan = Kosongan '(Tanah tidak ada penyusutannya).
        End If
        Return COA_AkumulasiPenyusutan
    End Function


    'PENENTUAN COA AKUMULASI PENYUSUTAN :
    Public Function AmbilValue_COAAkumulasiPenyusutan_DariDataAsset(ByVal KodeAsset As String) As String
        Dim COA_AkumulasiPenyusutan As String = Kosongan
        If KodeAsset <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            Dim cmdKhusus = New OdbcCommand(" SELECT COA_Akumulasi_Penyusutan FROM tbl_DataAsset " &
                                            " WHERE Kode_Asset = '" & KodeAsset & "' ", KoneksiDatabaseGeneral)
            Dim drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                COA_AkumulasiPenyusutan = drKhusus.Item("COA_Akumulasi_Penyusutan")
            Else
                PesanUntukProgrammer("Asset tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return COA_AkumulasiPenyusutan
    End Function


    'PENENTUAN COA HUTANG PAJAK :
    Public Function PenentuanCOA_HutangPajak(ByVal JenisPajak As String, KodeSetoran As String) As String

        Dim COA_HutangPajak As String = Kosongan

        JenisPajak = KonversiJenisPPhKeJenisPajak(JenisPajak)

        'PesanUntukProgrammer(JenisPajak & StripKosong & KodeSetoran)

        Select Case JenisPajak
            Case JenisPajak_PPhPasal21
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal21
                If KodeSetoran = KodeSetoran_100 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal21_100
                If KodeSetoran = KodeSetoran_401 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal21_401
            Case JenisPajak_PPhPasal22_Lokal
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal22
            Case JenisPajak_PPhPasal22_Impor
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal22
            Case JenisPajak_PPhPasal23
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23
                If KodeSetoran = KodeSetoran_100 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23_100
                If KodeSetoran = KodeSetoran_101 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23_101
                If KodeSetoran = KodeSetoran_102 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23_102
                If KodeSetoran = KodeSetoran_103 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23_103
                If KodeSetoran = KodeSetoran_104 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal23_104
            Case JenisPajak_PPhPasal42
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42
                If KodeSetoran = KodeSetoran_402 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42_402
                If KodeSetoran = KodeSetoran_403 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42_403
                If KodeSetoran = KodeSetoran_409 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42_409
                If KodeSetoran = KodeSetoran_419 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42_419
            Case JenisPajak_PPhPasal25
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal25
            Case JenisPajak_PPhPasal26
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26
                If KodeSetoran = KodeSetoran_402 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal42_402
                If KodeSetoran = KodeSetoran_100 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_100
                If KodeSetoran = KodeSetoran_101 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_101
                If KodeSetoran = KodeSetoran_102 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_102
                If KodeSetoran = KodeSetoran_103 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_103
                If KodeSetoran = KodeSetoran_104 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_104
                If KodeSetoran = KodeSetoran_105 Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal26_105
            Case JenisPajak_PPhPasal29
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPhPasal29
            Case JenisPajak_PPN
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPN
                If KodeSetoran = KodeSetoran_100 Then COA_HutangPajak = KodeTautanCOA_HutangPPN_100
                If KodeSetoran = KodeSetoran_101 Then COA_HutangPajak = KodeTautanCOA_HutangPPN_101
                If KodeSetoran = KodeSetoran_102 Then COA_HutangPajak = KodeTautanCOA_HutangPPN_102
                If KodeSetoran = KodeSetoran_103 Then COA_HutangPajak = KodeTautanCOA_HutangPPN_103
            Case JenisPajak_PPN_Impor
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangPPN_Impor
            Case JenisPajak_KetetapanPajak
                If KodeSetoran = KodeSetoran_Non Then COA_HutangPajak = KodeTautanCOA_HutangKetetapanPajak
        End Select

        If COA_HutangPajak = Kosongan Then PesanUntukProgrammer("Jenis PPh tidak terdaftar dalam penentuan COA Hutang Pajak...!!!" & Enter2Baris &
                                                                "Jenis PPh : " & JenisPajak)

        Return COA_HutangPajak

    End Function


    'PENENTUAN COA BIAYA PPh
    Public Function PenentuanCOA_BiayaPPh(ByVal JenisPPh As String) As String
        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPPh)
        Dim COA_BiayaPPh = Kosongan
        If JenisPPh = JenisPPh_Pasal21 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal21
        If JenisPPh = JenisPPh_Pasal22_Lokal Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal22
        If JenisPPh = JenisPPh_Pasal23 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal23
        If JenisPPh = JenisPPh_Pasal24 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal24
        If JenisPPh = JenisPPh_Pasal42 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal42
        If JenisPPh = JenisPPh_Pasal25 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal25
        If JenisPPh = JenisPPh_Pasal26 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal26
        If JenisPPh = JenisPPh_Pasal29 Then COA_BiayaPPh = KodeTautanCOA_BiayaPPhPasal29
        Return COA_BiayaPPh
    End Function

    'PENENTUAN COA PPh Dibayar Dimuka :
    Public Function PenentuanCOA_PPhDibayarDimuka(ByVal JenisPPh As String) As String
        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPPh)
        Dim COA_BiayaPPh = Kosongan
        If JenisPPh = JenisPPh_Pasal21 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal21DibayarDimuka
        'If JenisPPh = JenisPPh_Pasal22 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal22DibayarDimuka
        If JenisPPh = JenisPPh_Pasal23 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal23DibayarDimuka
        If JenisPPh = JenisPPh_Pasal42 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal42DibayarDimuka
        If JenisPPh = JenisPPh_Pasal25 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal25DibayarDimuka
        'If JenisPPh = JenisPPh_Pasal26 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal26DibayarDimuka
        'If JenisPPh = JenisPPh_Pasal29 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal29DibayarDimuka
        Return COA_BiayaPPh
    End Function

    'PENENTUAN COA PPh Dibayar Dimuka - BP Belum Diterima
    Public Function PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(ByVal JenisPPh As String) As String
        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPPh)
        Dim COA_BiayaPPh = Kosongan
        If JenisPPh = JenisPPh_Pasal23 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima
        If JenisPPh = JenisPPh_Pasal42 Then COA_BiayaPPh = KodeTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima
        Return COA_BiayaPPh
    End Function


    'KONVERSI VARIABEL : Jenis Pajak --> Jenis PPh
    Public Function KonversiJenisPajakKeJenisPPh(ByVal JenisPajak As String) As String
        Dim JenisPPh = JenisPajak
        If JenisPajak = JenisPajak_PPhPasal21 Then JenisPPh = JenisPPh_Pasal21
        If JenisPajak = JenisPajak_PPhPasal22_Lokal Then JenisPPh = JenisPPh_Pasal22_Lokal
        If JenisPajak = JenisPajak_PPhPasal22_Impor Then JenisPPh = JenisPPh_Pasal22_Impor
        If JenisPajak = JenisPajak_PPhPasal23 Then JenisPPh = JenisPPh_Pasal23
        If JenisPajak = JenisPajak_PPhPasal42 Then JenisPPh = JenisPPh_Pasal42
        If JenisPajak = JenisPajak_PPhPasal25 Then JenisPPh = JenisPPh_Pasal25
        If JenisPajak = JenisPajak_PPhPasal26 Then JenisPPh = JenisPPh_Pasal26
        If JenisPajak = JenisPajak_PPhPasal29 Then JenisPPh = JenisPPh_Pasal29
        'Pemaksaan :
        If JenisPajak = JenisPPh_Pasal21 Then JenisPPh = JenisPPh_Pasal21
        If JenisPajak = JenisPPh_Pasal22_Lokal Then JenisPPh = JenisPPh_Pasal22_Lokal
        If JenisPajak = JenisPPh_Pasal22_Impor Then JenisPPh = JenisPPh_Pasal22_Impor
        If JenisPajak = JenisPPh_Pasal23 Then JenisPPh = JenisPPh_Pasal23
        If JenisPajak = JenisPPh_Pasal42 Then JenisPPh = JenisPPh_Pasal42
        If JenisPajak = JenisPPh_Pasal25 Then JenisPPh = JenisPPh_Pasal25
        If JenisPajak = JenisPPh_Pasal26 Then JenisPPh = JenisPPh_Pasal26
        If JenisPajak = JenisPPh_Pasal29 Then JenisPPh = JenisPPh_Pasal29
        Return JenisPPh
    End Function

    'KONVERSI VARIABEL : Jenis PPh --> Jenis Pajak
    Public Function KonversiJenisPPhKeJenisPajak(ByVal JenisPPh As String) As String
        Dim JenisPajak = JenisPPh
        If JenisPPh = JenisPPh_Pasal21 Then JenisPajak = JenisPajak_PPhPasal21
        If JenisPPh = JenisPPh_Pasal22_Lokal Then JenisPajak = JenisPajak_PPhPasal22_Lokal
        If JenisPPh = JenisPPh_Pasal22_Impor Then JenisPajak = JenisPajak_PPhPasal22_Impor
        If JenisPPh = JenisPPh_Pasal23 Then JenisPajak = JenisPajak_PPhPasal23
        If JenisPPh = JenisPPh_Pasal42 Then JenisPajak = JenisPajak_PPhPasal42
        If JenisPPh = JenisPPh_Pasal25 Then JenisPajak = JenisPajak_PPhPasal25
        If JenisPPh = JenisPPh_Pasal26 Then JenisPajak = JenisPajak_PPhPasal26
        If JenisPPh = JenisPPh_Pasal29 Then JenisPajak = JenisPajak_PPhPasal29
        If JenisPPh = JenisPPh_NonPPh Then JenisPajak = Kosongan
        'Pemaksaan :
        If JenisPPh = JenisPajak_PPhPasal21 Then JenisPajak = JenisPajak_PPhPasal21
        If JenisPPh = JenisPajak_PPhPasal22_Lokal Then JenisPajak = JenisPajak_PPhPasal22_Lokal
        If JenisPPh = JenisPajak_PPhPasal22_Impor Then JenisPajak = JenisPajak_PPhPasal22_Impor
        If JenisPPh = JenisPajak_PPhPasal23 Then JenisPajak = JenisPajak_PPhPasal23
        If JenisPPh = JenisPajak_PPhPasal42 Then JenisPajak = JenisPajak_PPhPasal42
        If JenisPPh = JenisPajak_PPhPasal25 Then JenisPajak = JenisPajak_PPhPasal25
        If JenisPPh = JenisPajak_PPhPasal26 Then JenisPajak = JenisPajak_PPhPasal26
        If JenisPPh = JenisPajak_PPhPasal29 Then JenisPajak = JenisPajak_PPhPasal29
        Return JenisPajak
    End Function


    'JENIS KODE SETORAN :
    Public Function PenentuanJenisKodeSetoran(ByVal JenisPPh As String, ByVal KodeSetoran As String)
        Dim JenisKodeSetoran = Kosongan
        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPPh)
        Select Case JenisPPh
            Case JenisPPh_Pasal21
                Select Case KodeSetoran
                    Case KodeSetoran_100
                        JenisKodeSetoran = PPhPasal21_100_SetoranMasa
                    Case KodeSetoran_401
                        JenisKodeSetoran = PPhPasal21_401_Pesangon
                End Select
            Case JenisPPh_Pasal23
                Select Case KodeSetoran
                    Case KodeSetoran_100
                        JenisKodeSetoran = PPhPasal23_100_SewaAsset
                    Case KodeSetoran_101
                        JenisKodeSetoran = PPhPasal23_101_Dividen
                    Case KodeSetoran_102
                        JenisKodeSetoran = PPhPasal23_102_Bunga
                    Case KodeSetoran_103
                        JenisKodeSetoran = PPhPasal23_103_Royalty
                    Case KodeSetoran_104
                        JenisKodeSetoran = PPhPasal23_104_JasaLainnya
                End Select
            Case JenisPPh_Pasal42
                Select Case KodeSetoran
                    Case KodeSetoran_402
                        JenisKodeSetoran = PPhPasal42_402_PengalihanTanahBangunan
                    Case KodeSetoran_403
                        JenisKodeSetoran = PPhPasal42_403_SewaTanahBangunan
                    Case KodeSetoran_409
                        JenisKodeSetoran = PPhPasal42_409_JasaKonstruksi
                    Case KodeSetoran_419
                        JenisKodeSetoran = PPhPasal42_419_Dividen
                End Select
            Case JenisPPh_Pasal26
                Select Case KodeSetoran
                    Case KodeSetoran_100
                        JenisKodeSetoran = PPhPasal26_100_SewaAssetLuarNegeri
                    Case KodeSetoran_101
                        JenisKodeSetoran = PPhPasal26_101_Dividen
                    Case KodeSetoran_102
                        JenisKodeSetoran = PPhPasal26_102_Bunga
                    Case KodeSetoran_103
                        JenisKodeSetoran = PPhPasal26_103_Royalty
                    Case KodeSetoran_104
                        JenisKodeSetoran = PPhPasal26_104_Jasa
                    Case KodeSetoran_105
                        JenisKodeSetoran = PPhPasal26_105_LabaPajakBUT
                End Select
        End Select
        Return JenisKodeSetoran
    End Function

    'KONVERSI VARIABEL : Jenis Pajak --> Awalan BP
    Public Function KonversiJenisPajakKeAwalanBP(ByVal JenisPajak As String) As String
        KonversiJenisPPhKeJenisPajak(JenisPajak)
        Dim AwalanBP = Kosongan
        If JenisPajak = JenisPajak_PPhPasal21 Then AwalanBP = AwalanBPHP21
        'If JenisPajak = JenisPajak_PPhPasal22 Then AwalanBP = AwalanBPHP22
        If JenisPajak = JenisPajak_PPhPasal23 Then AwalanBP = AwalanBPHP23
        If JenisPajak = JenisPajak_PPhPasal42 Then AwalanBP = AwalanBPHP42
        If JenisPajak = JenisPajak_PPhPasal25 Then AwalanBP = AwalanBPHP25
        If JenisPajak = JenisPajak_PPhPasal26 Then AwalanBP = AwalanBPHP26
        'If JenisPajak = JenisPajak_PPhPasal29 Then AwalanBP = AwalanBPHP29
        If JenisPajak = JenisPajak_KetetapanPajak Then AwalanBP = AwalanBPKP
        Return AwalanBP
    End Function

    'KONVERSI VARIABEL : Jenis Pajak --> Awalan BP + Tahun Buku
    Public Function KonversiJenisPajakKeAwalanBPPlusTahunBuku(ByVal JenisPajak As String) As String
        KonversiJenisPPhKeJenisPajak(JenisPajak)
        Dim AwalanBPPlusTahunBuku = Kosongan
        If JenisPajak = JenisPajak_PPhPasal21 Then AwalanBPPlusTahunBuku = AwalanBPHP21_PlusTahunBuku
        'If JenisPajak = JenisPajak_PPhPasal22 Then AwalanBP = AwalanBPHP22_PlusTahunBuku
        If JenisPajak = JenisPajak_PPhPasal23 Then AwalanBPPlusTahunBuku = AwalanBPHP23_PlusTahunBuku
        If JenisPajak = JenisPajak_PPhPasal42 Then AwalanBPPlusTahunBuku = AwalanBPHP42_PlusTahunBuku
        If JenisPajak = JenisPajak_PPhPasal25 Then AwalanBPPlusTahunBuku = AwalanBPHP25_PlusTahunBuku
        If JenisPajak = JenisPajak_PPhPasal26 Then AwalanBPPlusTahunBuku = AwalanBPHP26_PlusTahunBuku
        'If JenisPajak = JenisPajak_PPhPasal29 Then AwalanBP = AwalanBPHP29_PlusTahunBuku
        If JenisPajak = JenisPajak_KetetapanPajak Then AwalanBPPlusTahunBuku = AwalanBPKP_PlusTahunBuku
        Return AwalanBPPlusTahunBuku
    End Function


    'AMBIL VALUE TARIF PPN :
    Public Function AmbilValue_TarifPPNBerdasarkanTanggal(TanggalTransaksi As Date) As Decimal
        Dim TarifPPN As Decimal = 0
        Dim TanggalKenaikanJadi11Persen As New Date(2022, 4, 1)
        Dim TanggalKenaikanJadi12Persen As New Date(2025, 1, 1)
        If TanggalTransaksi < TanggalKenaikanJadi11Persen Then TarifPPN = 10
        If TanggalTransaksi >= TanggalKenaikanJadi11Persen Then TarifPPN = 11
        If TanggalTransaksi >= TanggalKenaikanJadi12Persen Then TarifPPN = 11
        Return TarifPPN
    End Function


    'AMBIL VALUE TARIF PPN :
    Public Sub LogikaPPN(TanggalTransaksi As Date, DPP As Int64, TarifPPN As Decimal, ByRef DPP_11Per12 As Int64, ByRef TarifPPN_11Per12 As Decimal)
        TarifPPN = 0
        Dim TanggalKenaikanJadi11Persen As New Date(2022, 4, 1)
        Dim TanggalKenaikanJadi12Persen As New Date(2025, 1, 1)
        If TanggalTransaksi < TanggalKenaikanJadi11Persen Then
            TarifPPN = 10
            DPP_11Per12 = 0         'Ini Jangan dihapus...!
            TarifPPN_11Per12 = 0    'Ini Jangan dihapus...!
        End If
        If TanggalTransaksi >= TanggalKenaikanJadi11Persen Then
            TarifPPN = 11
            DPP_11Per12 = 0         'Ini Jangan dihapus...!
            TarifPPN_11Per12 = 0    'Ini Jangan dihapus...!
        End If
        If TanggalTransaksi >= TanggalKenaikanJadi12Persen Then
            TarifPPN = 11
            DPP_11Per12 = DPP * (11 / 12)
            TarifPPN_11Per12 = 12
        End If
    End Sub


    'AMBIL VALUE TARIF PPN :
    Public Sub LogikaTampilanPPN_UntukCetakNota(TanggalTransaksi As Date, ByRef DPP_Tampilan As Int64, ByRef TarifPPN_Tampilan As Decimal)
        TarifPPN_Tampilan = 0
        Dim TanggalKenaikanJadi11Persen As New Date(2022, 4, 1)
        Dim TanggalKenaikanJadi12Persen As New Date(2025, 1, 1)
        If TanggalTransaksi < TanggalKenaikanJadi11Persen Then
            TarifPPN_Tampilan = 10
        End If
        If TanggalTransaksi >= TanggalKenaikanJadi11Persen Then
            TarifPPN_Tampilan = 11
        End If
        If TanggalTransaksi >= TanggalKenaikanJadi12Persen Then
            TarifPPN_Tampilan = 12
            DPP_Tampilan = Math.Round(DPP_Tampilan * (11 / 12))
        End If
    End Sub


    'AMBIL VALUE TARIF PPN - STRING :
    Public Function AmbilValue_TarifPPN_String(TanggalTransaksi As Date) As String
        Dim TarifPPN As String = Kosongan
        Dim TanggalKenaikanJadi11Persen As New Date(2022, 4, 1)
        Dim TanggalKenaikanJadi12Persen As New Date(2025, 1, 1)
        If TanggalTransaksi < TanggalKenaikanJadi11Persen Then TarifPPN = "10 %"
        If TanggalTransaksi >= TanggalKenaikanJadi11Persen Then TarifPPN = "11 %"
        If TanggalTransaksi >= TanggalKenaikanJadi12Persen Then TarifPPN = "12 %"
        Return TarifPPN
    End Function


    'LOGIKA TW / TL :
    Public Function LogikaTWTL(ByVal JenisPajak As String, ByVal MasaPajak_Angka As Integer, ByVal TahunPajak As Integer, TanggalBayar As String) As String
        Dim HariDeadlineBayar = 10
        KonversiJenisPPhKeJenisPajak(JenisPajak)
        Dim TanggalBayar_Date As Date = TanggalBayar
        Dim BulanDeadlineBayar As Integer = MasaPajak_Angka + 1
        Dim TahunDeadlineBayar = TahunPajak
        If BulanDeadlineBayar = 13 Then
            BulanDeadlineBayar = 1
            TahunDeadlineBayar += 1
        End If
        If JenisPajak = JenisPajak_PPhPasal25 Then HariDeadlineBayar = 15
        Dim TanggalDeadlineBayar As Date = New Date(TahunDeadlineBayar, BulanDeadlineBayar, HariDeadlineBayar)
        If JenisPajak = JenisPajak_PPN Then TanggalDeadlineBayar = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDeadlineBayar, TahunDeadlineBayar)
        Dim ValueTWTL = Kosongan
        If TanggalBayar_Date <= TanggalDeadlineBayar Then
            ValueTWTL = TWTL_TepatWaktu
        Else
            ValueTWTL = TWTL_Terlambat
        End If
        Return ValueTWTL
    End Function


    ' FORMAT ULANG DESIMAL (Menghilangkan ,0000 yang terlalu panjang)
    Public Function FormatUlangDesimal_X(ByVal Decimal_ToString As String) As Decimal
        ' Tangani kondisi null, kosong, atau NaN lebih ringkas
        If String.IsNullOrWhiteSpace(Decimal_ToString) OrElse
       Decimal_ToString.Equals("NaN", StringComparison.OrdinalIgnoreCase) Then
            Return 0D
        End If

        ' Bersihkan format aneh seperti ",00", ",000", dst.
        Decimal_ToString = Decimal_ToString.Replace(",0000", ",0") _
                                         .Replace(",000", ",0") _
                                         .Replace(",00", ",0") _
                                         .Replace(",0", ",0")

        ' Hapus koma di akhir jika hanya nol
        If Decimal_ToString.EndsWith(",0") Then
            Decimal_ToString = Decimal_ToString.TrimEnd("0"c, ","c)
        End If

        ' Coba konversi aman
        Dim hasil As Decimal
        If Decimal.TryParse(Decimal_ToString, Globalization.NumberStyles.Any,
                        Globalization.CultureInfo.InvariantCulture, hasil) Then
            Return hasil
        Else
            Return 0D
        End If
    End Function


    'FORMAT ULANG DESIMAL (Menghilangkan ,0000 yang terlalu panjang)
    Public Function FormatUlangDesimal_Prosentase(ByVal Decimal_ToString As String) As Decimal
        If Decimal_ToString = Nothing Then Return 0
        If Decimal_ToString = Kosongan Then Return 0
        If Decimal_ToString = "" Then Return 0
        If String.IsNullOrEmpty(Decimal_ToString) Then Return 0
        If Decimal_ToString = "NaN" Then Return 0
        Dim HasilFormat As Decimal = 0
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        Decimal_ToString = Replace(Decimal_ToString, ",00", ",0")
        'Decimal_ToString = Left(Decimal_ToString, 5)
        If Right(Decimal_ToString, 2) = ",0" Then Decimal_ToString = Replace(Decimal_ToString, ",0", "")
        HasilFormat = Decimal_ToString
        Return HasilFormat
    End Function

    'PEMBULATAN DESIMAL 2 DIGIT :
    Public Function PembulatanDesimal2Digit(ByVal nilaiString As String) As String
        Dim NolString As String = "0,00"
        If nilaiString = "NaN" Then Return NolString
        If IsDBNull(nilaiString) Then Return NolString
        If nilaiString = Nothing Then Return NolString
        If nilaiString = Kosongan Then Return NolString
        Dim nilai As Decimal = nilaiString
        Dim hasil As Decimal = Math.Round(nilai, 2, MidpointRounding.AwayFromZero)
        Return hasil.ToString("0.00")   ' selalu 2 digit
    End Function

    'PERSEN (%)
    Function Persen(Angka As Decimal) As Decimal
        Return Angka / 100
    End Function

    'PEMISAH RIBUAN UNTUK ANGKA FORMAT STRING
    Public Function PemisahRibuan(ByVal value As String) As String
        Dim rval As String = String.Empty
        If value = "" Then value = "xyZ0"
        Dim AngkaTerpisah
        Dim coll As MatchCollection = Regex.Matches(value, "\d+")
        For Each a As Match In coll
            rval += a.ToString()
        Next
        rval = StrReverse(rval)
        AngkaTerpisah = Left(rval, 3)
        If Len(rval) > 3 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 4, 3)
        If Len(rval) > 6 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 7, 3)
        If Len(rval) > 9 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 10, 3)
        If Len(rval) > 12 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 13, 3)
        If Len(rval) > 15 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 16, 3)
        If Len(rval) > 18 Then AngkaTerpisah = AngkaTerpisah & "." & Mid(rval, 19, 3)
        If rval = "0" Or rval = "" Then AngkaTerpisah = StripKosong
        Return StrReverse(AngkaTerpisah)
    End Function

    'FORMAT TANGGAL UNTUK DISIMPAN DI DATABASE MySQL
    Public Function TanggalFormatSimpan(TanggalKiriman As String) As String
        Dim TanggalKosongKirimBalik As String = "1900-01-01"
        If String.IsNullOrEmpty(TanggalKiriman) Then Return TanggalKosongKirimBalik
        If TanggalKiriman = StripKosong Then Return TanggalKosongKirimBalik
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            Return TanggalKosongKirimBalik
        End Try
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Tahun & "-" & Bulan & "-" & Hari
        Return TanggalHasil_KirimBalik
    End Function

    'FORMAT TANGGAL UNTUK DIKIRIM KE UI DATEPICKER WPF
    Public Function TanggalFormatWPF(TanggalKiriman As String) As String
        Dim TanggalKosongKirimBalik As String = "1900-01-01"
        If String.IsNullOrEmpty(TanggalKiriman) Then Return TanggalKosongKirimBalik
        If TanggalKiriman = StripKosong Then Return TanggalKosongKirimBalik
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            Return TanggalKosongKirimBalik
        End Try
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Tahun & "-" & Bulan & "-" & Hari
        Return TanggalHasil_KirimBalik
    End Function

    'FORMAT TANGGAL UNTUK DITAMPILKAN (Hanya Tanggal Saja)
    Public Function TanggalFormatTampilan(TanggalKiriman As String) As String
        Dim TanggalKosongKirimBalik As String = "01-01-1900"
        If String.IsNullOrEmpty(TanggalKiriman) Then Return TanggalKosongKirimBalik
        If TanggalKiriman = StripKosong Then Return TanggalKosongKirimBalik
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            Return TanggalKosongKirimBalik
        End Try
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Hari & "-" & Bulan & "-" & Tahun
        'If TanggalHasil_KirimBalik = TanggalKosong Then TanggalHasil_KirimBalik = Kosongan
        Return TanggalHasil_KirimBalik
    End Function

    'FORMAT TANGGAL UNTUK DITAMPILKAN DI TABEL ATAU KOLOM (Hanya Tanggal Saja)
    Public Function TanggalFormatTampilan_Kosongan(TanggalKiriman As String) As String
        Dim TanggalKosongKirimBalik As String = Kosongan
        If String.IsNullOrEmpty(TanggalKiriman) Then Return TanggalKosongKirimBalik
        If TanggalKiriman = StripKosong Then Return TanggalKosongKirimBalik
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            Return TanggalKosongKirimBalik
        End Try
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Hari & "-" & Bulan & "-" & Tahun
        'If TanggalHasil_KirimBalik = TanggalKosong Then TanggalHasil_KirimBalik = Kosongan
        Return TanggalHasil_KirimBalik
    End Function

    'FORMAT TANGGAL UNTUK DITAMPILKAN DI TABEL ATAU KOLOM (Hanya Tanggal Saja)
    Public Function TanggalFormatTampilan_StripKosong(TanggalKiriman As String) As String
        Dim TanggalKosongKirimBalik As String = StripKosong
        If String.IsNullOrEmpty(TanggalKiriman) Then Return TanggalKosongKirimBalik
        If TanggalKiriman = StripKosong Then Return TanggalKosongKirimBalik
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            Return TanggalKosongKirimBalik
        End Try
        Dim Tahun As String = Format(TanggalMentah, "yyyy").ToString
        Dim Bulan As String = Format(TanggalMentah, "MM").ToString
        Dim Hari As String = Format(TanggalMentah, "dd").ToString
        Dim TanggalHasil_KirimBalik As String = Hari & "-" & Bulan & "-" & Tahun
        'If TanggalHasil_KirimBalik = TanggalKosong Then TanggalHasil_KirimBalik = Kosongan
        Return TanggalHasil_KirimBalik
    End Function

    Public Function AmbilHari_DariTanggal(TanggalKiriman As String) As Integer
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            PesanUntukProgrammer("Kesalahan dalam pengiriman Data Tanggal." & Enter2Baris &
                                  "Sub/Function : AmbilHari_DariTanggal()" & Enter2Baris &
                                  "File : mdl_PublicSub")
            Return 0
        End Try
        Dim Hari As Integer = AmbilAngka(Format(TanggalMentah, "dd").ToString)
        Return Hari
    End Function

    Public Function AmbilBulanAngka_DariTanggal(TanggalKiriman As String) As Integer
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            PesanUntukProgrammer("Kesalahan dalam pengiriman Data Tanggal." & Enter2Baris &
                                  "Sub/Function : AmbilBulanAngka_DariTanggal()" & Enter2Baris &
                                  "File : mdl_PublicSub")
            Return 0
        End Try
        Dim Bulan As Integer = Format(TanggalMentah, "MM").ToString
        Return Bulan
    End Function

    Public Function AmbilBulanTerbilang_DariTanggal(TanggalKiriman As String) As String
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            PesanUntukProgrammer("Kesalahan dalam pengiriman Data Tanggal." & Enter2Baris &
                                  "Sub/Function : AmbilBulanTerbilang_DariTanggal()" & Enter2Baris &
                                  "File : mdl_PublicSub")
            Return 0
        End Try
        Dim BulanAngka As Integer = AmbilAngka(Format(TanggalMentah, "MM").ToString)
        Dim BulanTerbilang As String = KonversiAngkaKeBulanString(BulanAngka)
        Return BulanAngka
    End Function

    Public Function AmbilTahun_DariTanggal(TanggalKiriman As String) As Integer
        Dim TanggalMentah As Date
        Try
            TanggalMentah = TanggalKiriman
        Catch ex As Exception
            PesanUntukProgrammer("Kesalahan dalam pengiriman Data Tanggal." & Enter2Baris &
                                  "Sub/Function : AmbilTahun_DariTanggal()" & Enter2Baris &
                                  "File : mdl_PublicSub")
            Return 0
        End Try
        Dim Tahun As String = AmbilAngka(Format(TanggalMentah, "yyyy").ToString)
        Return Tahun
    End Function


    'FORMAT DESIMAL UNTUK DISIMPAN :
    Public Function DesimalFormatSimpan(ByVal valueDecimal As Decimal) As String
        Dim KirimBalik_Desimal As String
        KirimBalik_Desimal = Replace(valueDecimal, ",", ".")
        Return KirimBalik_Desimal
    End Function

    'Tanggal Terakhir untuk Masing-masing Bulan
    Public Function AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan As String, Tahun As Integer) As String
        Dim Hasil = Nothing
        Dim TahunKabisat As Boolean
        If Tahun Mod 4 = 0 Then
            TahunKabisat = True
        Else
            TahunKabisat = False
        End If
        If AmbilAngka(Bulan) = 1 Or Bulan = Bulan_Januari Then Hasil = "31/01/" & Tahun.ToString
        If AmbilAngka(Bulan) = 2 Or Bulan = Bulan_Februari Then
            If TahunKabisat = True Then
                Hasil = "29/02/" & Tahun.ToString
            Else
                Hasil = "28/02/" & Tahun.ToString
            End If
        End If
        If AmbilAngka(Bulan) = 3 Or Bulan = Bulan_Maret Then Hasil = "31/03/" & Tahun.ToString
        If AmbilAngka(Bulan) = 4 Or Bulan = Bulan_April Then Hasil = "30/04/" & Tahun.ToString
        If AmbilAngka(Bulan) = 5 Or Bulan = Bulan_Mei Then Hasil = "31/05/" & Tahun.ToString
        If AmbilAngka(Bulan) = 6 Or Bulan = Bulan_Juni Then Hasil = "30/06/" & Tahun.ToString
        If AmbilAngka(Bulan) = 7 Or Bulan = Bulan_Juli Then Hasil = "31/07/" & Tahun.ToString
        If AmbilAngka(Bulan) = 8 Or Bulan = Bulan_Agustus Then Hasil = "31/08/" & Tahun.ToString
        If AmbilAngka(Bulan) = 9 Or Bulan = Bulan_September Then Hasil = "30/09/" & Tahun.ToString
        If AmbilAngka(Bulan) = 10 Or Bulan = Bulan_Oktober Then Hasil = "31/10/" & Tahun.ToString
        If AmbilAngka(Bulan) = 11 Or Bulan = Bulan_Nopember Then Hasil = "30/11/" & Tahun.ToString
        If AmbilAngka(Bulan) = 12 Or Bulan = Bulan_Desember Then Hasil = "31/12/" & Tahun.ToString
        Return Hasil
    End Function

    'Tanggal Terakhir dari sebuah Tanggal :
    Public Function AmbilTanggalAkhirBulan_BerdasarkanTanggalLengkap(Tanggal As Date) As String
        Dim Bulan = Tanggal.Month
        Dim Tahun = Tanggal.Year
        Return AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan, Tahun)
    End Function

    'FORMAT TANGGAL UNTUK DI-EXPORT KE TABEL EXCEL
    Public Function TanggalFormatExport(ByVal IsiTanggal As String) As String
        Dim Hasil As String = String.Empty
        Dim coll As MatchCollection = Regex.Matches(IsiTanggal, "\d+")
        For Each a As Match In coll
            Hasil += a.ToString()
        Next
        Dim Tahun = Microsoft.VisualBasic.Mid(Hasil, 5, 4)
        Dim Bulan = Microsoft.VisualBasic.Mid(Hasil, 3, 2)
        Dim Hari = Microsoft.VisualBasic.Left(Hasil, 2)
        Hasil = Bulan & "-" & Hari & "-" & Tahun
        Return Hasil
    End Function

    'ANGKA TERBILANG :
    Public Function AngkaTerbilang(ByVal value As String) As String
        Dim Angka As String = Kosongan
        If Angka = Kosongan Then Angka = "0"
        Dim coll As MatchCollection = Regex.Matches(value, "\d+")
        For Each a As Match In coll
            Angka += a.ToString()
        Next
        Angka = Convert.ToInt64(Angka)
        Dim AngkaHuruf = Kosongan
        Dim AngkaHurufGeneral = Kosongan
        Dim AngkaString = Angka.ToString
        Dim AngkaStringTerbalik = StrReverse(AngkaString)
        Dim JumlahDigit As Integer = AngkaString.Length
        Dim Pengulangan = JumlahDigit
        Dim DigitKe As Integer = 0
        Do While Pengulangan > 0
            DigitKe = DigitKe + 1
            AngkaHuruf = Mid(AngkaString, Pengulangan, 1)
            If AngkaHuruf = "0" Then
                AngkaHuruf = Kosongan
                If JumlahDigit = 1 Then AngkaHuruf = "Nol "
            End If
            If AngkaHuruf = "1" Then AngkaHuruf = "Satu "
            If AngkaHuruf = "2" Then AngkaHuruf = "Dua "
            If AngkaHuruf = "3" Then AngkaHuruf = "Tiga "
            If AngkaHuruf = "4" Then AngkaHuruf = "Empat "
            If AngkaHuruf = "5" Then AngkaHuruf = "Lima "
            If AngkaHuruf = "6" Then AngkaHuruf = "Enam "
            If AngkaHuruf = "7" Then AngkaHuruf = "Tujuh "
            If AngkaHuruf = "8" Then AngkaHuruf = "Delapan "
            If AngkaHuruf = "9" Then AngkaHuruf = "Sembilan "
            If (DigitKe = 2 Or DigitKe = 5 Or DigitKe = 8 Or DigitKe = 11 Or DigitKe = 14 Or DigitKe = 17) And AngkaHuruf <> "" Then
                AngkaHuruf = AngkaHuruf & "Puluh "
                If AngkaHuruf = "Satu Puluh " Then AngkaHuruf = "Sepuluh "
            End If
            If (DigitKe = 3 Or DigitKe = 6 Or DigitKe = 9 Or DigitKe = 12 Or DigitKe = 15 Or DigitKe = 18) And AngkaHuruf <> "" Then
                AngkaHuruf = AngkaHuruf & "Ratus "
                If AngkaHuruf = "Satu Ratus " Then AngkaHuruf = "Seratus "
            End If
            If DigitKe = 4 Then
                If Mid(AngkaStringTerbalik, 4, 3) = "000" Then
                    AngkaHuruf = AngkaHuruf
                Else
                    AngkaHuruf = AngkaHuruf & "Ribu "
                    If AngkaHuruf = "Satu Ribu " And JumlahDigit = 4 Then AngkaHuruf = "Seribu "
                End If
            End If
            If DigitKe = 7 Then
                If Mid(AngkaStringTerbalik, 7, 3) = "000" Then
                    AngkaHuruf = AngkaHuruf
                Else
                    AngkaHuruf = AngkaHuruf & "Juta "
                End If
            End If
            If DigitKe = 10 Then
                If Mid(AngkaStringTerbalik, 10, 3) = "000" Then
                    AngkaHuruf = AngkaHuruf
                Else
                    AngkaHuruf = AngkaHuruf & "Milyar "
                End If
            End If
            If DigitKe = 13 Then
                If Mid(AngkaStringTerbalik, 13, 3) = "000" Then
                    AngkaHuruf = AngkaHuruf
                Else
                    AngkaHuruf = AngkaHuruf & "Trilyun "
                End If
            End If
            If DigitKe = 16 Then
                If Mid(AngkaStringTerbalik, 16, 3) = "000" Then
                    AngkaHuruf = AngkaHuruf
                Else
                    AngkaHuruf = AngkaHuruf & "Bilyun "
                End If
            End If
            AngkaHurufGeneral = AngkaHuruf & AngkaHurufGeneral
            If Left(AngkaHurufGeneral, 13) = "Sepuluh Satu " Then AngkaHurufGeneral = "Sebelas " & Mid(AngkaHurufGeneral, 14)
            If Left(AngkaHurufGeneral, 12) = "Sepuluh Dua " Then AngkaHurufGeneral = "Dua Belas " & Mid(AngkaHurufGeneral, 13)
            If Left(AngkaHurufGeneral, 13) = "Sepuluh Tiga " Then AngkaHurufGeneral = "Tiga Belas " & Mid(AngkaHurufGeneral, 14)
            If Left(AngkaHurufGeneral, 14) = "Sepuluh Empat " Then AngkaHurufGeneral = "Empat Belas " & Mid(AngkaHurufGeneral, 15)
            If Left(AngkaHurufGeneral, 13) = "Sepuluh Lima " Then AngkaHurufGeneral = "Lima Belas " & Mid(AngkaHurufGeneral, 14)
            If Left(AngkaHurufGeneral, 13) = "Sepuluh Enam " Then AngkaHurufGeneral = "Enam Belas " & Mid(AngkaHurufGeneral, 14)
            If Left(AngkaHurufGeneral, 14) = "Sepuluh Tujuh " Then AngkaHurufGeneral = "Tujuh Belas " & Mid(AngkaHurufGeneral, 15)
            If Left(AngkaHurufGeneral, 16) = "Sepuluh Delapan " Then AngkaHurufGeneral = "Delapan Belas " & Mid(AngkaHurufGeneral, 17)
            If Left(AngkaHurufGeneral, 17) = "Sepuluh Sembilan " Then AngkaHurufGeneral = "Sembilan Belas " & Mid(AngkaHurufGeneral, 18)
            Pengulangan -= 1
        Loop
        AngkaHurufGeneral = Microsoft.VisualBasic.Left(AngkaHurufGeneral, AngkaHurufGeneral.Length - 1) '(Ini untuk menghapus spasi paling akhir).
        Return AngkaHurufGeneral
    End Function

    'ANGKA TERBILANG RUPIAH
    Public Function AngkaTerbilangRupiah(ByVal value As String) As String
        Dim AngkaTerbilang_Murni As String = AngkaTerbilang(value)
        Dim AngkaTerbilang_Rupiah = AngkaTerbilang_Murni & " Rupiah"
        Return AngkaTerbilang_Rupiah
    End Function

    'ANGKA TERBILANG RUPIAH DENGAN KURUNG
    Public Function AngkaTerbilangRupiahDenganKurung(ByVal value As String) As String
        Dim AngkaTerbilang_Murni As String = AngkaTerbilang(value)
        Dim AngkaTerbilangrRupiah_DenganKurung = "(" & AngkaTerbilang_Murni & " Rupiah)"
        Return AngkaTerbilangrRupiah_DenganKurung
    End Function

    'BULAN TERBILANG :
    Public Function BulanTerbilang(ByVal BulanAngka_String As String)
        Dim BulanAngka As Integer = AmbilAngka(BulanAngka_String)
        Dim Bulan = Nothing
        Select Case BulanAngka
            Case 1 : Bulan = Bulan_Januari
            Case 2 : Bulan = Bulan_Februari
            Case 3 : Bulan = Bulan_Maret
            Case 4 : Bulan = Bulan_April
            Case 5 : Bulan = Bulan_Mei
            Case 6 : Bulan = Bulan_Juni
            Case 7 : Bulan = Bulan_Juli
            Case 8 : Bulan = Bulan_Agustus
            Case 9 : Bulan = Bulan_September
            Case 10 : Bulan = Bulan_Oktober
            Case 11 : Bulan = Bulan_Nopember
            Case 12 : Bulan = Bulan_Desember
            Case Else : PesanUntukProgrammer("Ada kesalahan pada penomoran bulan." & Enter2Baris &
                                             "Bulan Angka : " & BulanAngka)
        End Select
        Return Bulan
    End Function

    'KONVERSI BULAN KE ANGKA :
    Public Function KonversiBulanKeAngka(ByVal NamaBulan As String)
        Dim BulanAngka As Integer = 0
        Select Case NamaBulan
            Case Bulan_Januari
                BulanAngka = 1
            Case Bulan_Februari
                BulanAngka = 2
            Case Bulan_Maret
                BulanAngka = 3
            Case Bulan_April
                BulanAngka = 4
            Case Bulan_Mei
                BulanAngka = 5
            Case Bulan_Juni
                BulanAngka = 6
            Case Bulan_Juli
                BulanAngka = 7
            Case Bulan_Agustus
                BulanAngka = 8
            Case Bulan_September
                BulanAngka = 9
            Case Bulan_Oktober
                BulanAngka = 10
            Case Bulan_Nopember
                BulanAngka = 11
            Case Bulan_Desember
                BulanAngka = 12
        End Select
        Return BulanAngka
    End Function

    'KONVERSI BULAN KE ANGKA (DALAM BENTUK STRING) :
    Public Function KonversiBulanKeNomor_String(ByVal NamaBulan As String)
        Dim BulanAngka_String As String = Nothing
        Dim BulanAngka As Integer = KonversiBulanKeAngka(NamaBulan)
        BulanAngka_String = BulanAngka.ToString
        If Microsoft.VisualBasic.Len(BulanAngka_String) = 1 Then BulanAngka_String = "0" & BulanAngka_String
        Return BulanAngka_String
    End Function


    'KONVERSI ANGKA KE BULAN :
    Public Function KonversiAngkaKeBulanString(ByVal BulanAngka As Integer) As String
        Dim BulanString As String = 0
        Select Case BulanAngka
            Case 1 : BulanString = Bulan_Januari
            Case 2 : BulanString = Bulan_Februari
            Case 3 : BulanString = Bulan_Maret
            Case 4 : BulanString = Bulan_April
            Case 5 : BulanString = Bulan_Mei
            Case 6 : BulanString = Bulan_Juni
            Case 7 : BulanString = Bulan_Juli
            Case 8 : BulanString = Bulan_Agustus
            Case 9 : BulanString = Bulan_September
            Case 10 : BulanString = Bulan_Oktober
            Case 11 : BulanString = Bulan_Nopember
            Case 12 : BulanString = Bulan_Desember
            Case Else : PesanUntukProgrammer("Ada kesalahan pada penomoran bulan." & Enter2Baris & "Nomor Bulan : " & BulanAngka)
        End Select
        Return BulanString
    End Function

    'BULAN ROMAWI :
    Public Function BulanRomawi(ByVal BulanAngka_String As String)
        Dim BulanAngka As Integer = AmbilAngka(BulanAngka_String)
        Dim Bulan = Nothing
        Select Case BulanAngka
            Case 1 : Bulan = Romawi_1
            Case 2 : Bulan = Romawi_2
            Case 3 : Bulan = Romawi_3
            Case 4 : Bulan = Romawi_4
            Case 5 : Bulan = Romawi_5
            Case 6 : Bulan = Romawi_6
            Case 7 : Bulan = Romawi_7
            Case 8 : Bulan = Romawi_8
            Case 9 : Bulan = Romawi_9
            Case 10 : Bulan = Romawi_10
            Case 11 : Bulan = Romawi_11
            Case 12 : Bulan = Romawi_12
        End Select
        Return Bulan
    End Function


    'KONVERSI ANGKA KE STRING 2 DIGIT :
    Public Function KonversiAngkaKeStringDuaDigit(ByVal Angka As Integer) As String
        Dim NomorStringDuaDigit As String = Angka.ToString
        If Len(NomorStringDuaDigit) = 1 Then NomorStringDuaDigit = "0" & NomorStringDuaDigit
        If Len(NomorStringDuaDigit) > 2 Then NomorStringDuaDigit = Left(NomorStringDuaDigit, 2)
        Return NomorStringDuaDigit
    End Function

    'TANGGAL AKHIR BULAN :
    Public Function TanggalAkhirBulan_Case(ByVal Tahun As Integer, ByVal NomorBulan As Integer)
        Dim TanggalAkhirBulan_Hasil As Date
        Select Case NomorBulan
            Case 1
                TanggalAkhirBulan_Hasil = New Date(Tahun, 1, 31)
            Case 2
                If Tahun Mod 4 = 0 Then
                    TanggalAkhirBulan_Hasil = New Date(Tahun, 2, 29)
                Else
                    TanggalAkhirBulan_Hasil = New Date(Tahun, 2, 28)
                End If
            Case 3
                TanggalAkhirBulan_Hasil = New Date(Tahun, 3, 31)
            Case 4
                TanggalAkhirBulan_Hasil = New Date(Tahun, 4, 30)
            Case 5
                TanggalAkhirBulan_Hasil = New Date(Tahun, 5, 31)
            Case 6
                TanggalAkhirBulan_Hasil = New Date(Tahun, 6, 30)
            Case 7
                TanggalAkhirBulan_Hasil = New Date(Tahun, 7, 31)
            Case 8
                TanggalAkhirBulan_Hasil = New Date(Tahun, 8, 31)
            Case 9
                TanggalAkhirBulan_Hasil = New Date(Tahun, 9, 30)
            Case 10
                TanggalAkhirBulan_Hasil = New Date(Tahun, 10, 31)
            Case 11
                TanggalAkhirBulan_Hasil = New Date(Tahun, 11, 30)
            Case 12
                TanggalAkhirBulan_Hasil = New Date(Tahun, 12, 31)
        End Select
        Return TanggalAkhirBulan_Hasil
    End Function

    'KONVERSI KELOMPOK HARTA KE ANGKA :
    Public Function KonversiKelompokHartaKeAngka(ByVal KelompokHarta As String) As Integer
        Dim KelompokHarta_Angka = 0
        Select Case KelompokHarta
            Case KelompokHarta_1
                KelompokHarta_Angka = 1
            Case KelompokHarta_2
                KelompokHarta_Angka = 2
            Case KelompokHarta_3
                KelompokHarta_Angka = 3
            Case KelompokHarta_4
                KelompokHarta_Angka = 4
            Case KelompokHarta_BangunanPermanen
                KelompokHarta_Angka = 5
            Case KelompokHarta_BangunanTidakPermanen
                KelompokHarta_Angka = 6
            Case KelompokHarta_Tanah
                KelompokHarta_Angka = 7
        End Select
        Return KelompokHarta_Angka
    End Function

    'KONVERSI ANGKA KE KELOMPOK HARTA:
    Public Function KonversiAngkaKeKelompokHarta(ByVal AngkaKelompokHarta As Integer) As String
        Dim KelompokHarta As String = Kosongan
        Select Case AngkaKelompokHarta
            Case 1
                KelompokHarta = KelompokHarta_1
            Case 2
                KelompokHarta = KelompokHarta_2
            Case 3
                KelompokHarta = KelompokHarta_3
            Case 4
                KelompokHarta = KelompokHarta_4
            Case 5
                KelompokHarta = KelompokHarta_BangunanPermanen
            Case 6
                KelompokHarta = KelompokHarta_BangunanTidakPermanen
            Case 7
                KelompokHarta = KelompokHarta_Tanah
            Case Else
                PesanUntukProgrammer("Kelompok Harta tidak terdaftar..!")
        End Select
        Return KelompokHarta
    End Function

    'KONVERSI KELOMPOK HARTA KE MASA MANFAAT :
    Public Function KonversiKelompokHartaKeMasaManfaat(ByVal KelompokHarta As String) As Integer
        Dim MasaManfaat = 0
        Select Case KelompokHarta
            Case KelompokHarta_1
                MasaManfaat = 4
            Case KelompokHarta_2
                MasaManfaat = 8
            Case KelompokHarta_3
                MasaManfaat = 16
            Case KelompokHarta_4
                MasaManfaat = 20
            Case KelompokHarta_BangunanPermanen
                MasaManfaat = 20
            Case KelompokHarta_BangunanTidakPermanen
                MasaManfaat = 10
            Case KelompokHarta_Tanah
                MasaManfaat = 0
        End Select
        Return MasaManfaat
    End Function

    'KONVERSI KELOMPOK HARTA_ANGKA KE MASA MANFAAT :
    Public Function KonversiKelompokHartaAngkaKeMasaManfaat(ByVal KelompokHartaAngka As Integer) As Integer
        Dim MasaManfaat = 0
        Select Case KelompokHartaAngka
            Case 1
                MasaManfaat = 4
            Case 2
                MasaManfaat = 8
            Case 3
                MasaManfaat = 16
            Case 4
                MasaManfaat = 20
            Case 5
                MasaManfaat = 20
            Case 6
                MasaManfaat = 10
            Case 7
                MasaManfaat = 0
        End Select
        Return MasaManfaat
    End Function

    'AMBIL VALUE DIVISI ASSET :
    Function AmbilValue_DivisiAsset(KodeDivisi As String) As String
        Dim DivisiAsset = Kosongan
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT Divisi FROM tbl_DivisiAsset WHERE Kode_Divisi = '" & KodeDivisi & "' ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            DivisiAsset = drKhusus.Item("Divisi")
        Else
            PesanUntukProgrammer("Kode Divisi tidak terdaftar...!!!")
        End If
        TutupDatabaseGeneral_Kondisional()
        Return DivisiAsset
    End Function




    'Sub Simpan Data Transaksi/Jurnal :
    Public jur_Status
    Public jur_NomorID
    Public jur_NomorJV
    Public jur_NomorJV_GagalSimpan
    Public jur_COA
    Public jur_NamaAkun
    Public jur_AkunTerdaftar As Boolean
    Public jur_TanggalTransaksi As Date
    Public jur_TahunTransaksiSesuai As Boolean = True 'Harus True dari awal...!!! Jangan dikosongkan.
    Public jur_JenisJurnal
    Public jur_KodeDokumen
    Public jur_NomorPO
    Public jur_KodeProject
    Public jur_NamaProduk
    Public jur_Referensi
    Public jur_Bundelan
    Public jur_TanggalInvoice
    Public jur_NomorInvoice
    Public jur_NomorFakturPajak
    Public jur_KodeLawanTransaksi
    Public jur_NamaLawanTransaksi
    Public jur_KodeMataUang
    Public jur_Kurs As Decimal
    Public jur_DK
    Public dk_D = "D"
    Public dk_K = "K"
    Public dk_DEBET_ = "DEBET"
    Public dk_KREDIT_ = "KREDIT"
    Public dk_Debet = "Debet"
    Public dk_Kredit = "Kredit"
    Public jur_JumlahMutasi As Decimal
    Public jur_JumlahDebet As Decimal
    Public jur_JumlahKredit As Decimal
    Public jur_UraianTransaksi
    Public jur_QueryPenyimpanan
    Public jur_Direct
    Public jur_StatusApprove
    Public jur_UsernameEntry
    Public jur_NamaUserEntry
    Public jur_UsernameApprove
    Public jur_NamaUserApprove
    Public jur_StatusPenyimpananJurnal_PerBaris As Boolean
    Public jur_StatusPenyimpananJurnal_Lengkap As Boolean
    Public Sub ___jurDebet(COA As String, JumlahMutasi As Decimal)
        If JumlahMutasi = 0 Or COA = Kosongan Then Return
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            jur_StatusPenyimpananJurnal_Lengkap = False
            Return
        End If
        jur_DK = dk_D
        jur_COA = COA
        jur_JumlahMutasi = JumlahMutasi
        Dim KodeMataUang_Asal As String = jur_KodeMataUang
        Dim Kurs_Asal As Decimal = jur_Kurs
        If COA = KodeTautanCOA_BiayaSelisihPencatatan _     'Ini jangan dihapus..! Karena sangat mungkin COA IDR masuk ke jurnal MUA
            Or COA = KodeTautanCOA_LabaRugiSelisihKurs _    'Idem
            Then                                            'Idem
            jur_KodeMataUang = KodeMataUang_IDR             'Idem
            jur_Kurs = 1                                    'Idem
        End If                                              'Idem
        ______________________________________SimpanJurnal_PerBaris()
        jur_KodeMataUang = KodeMataUang_Asal    'Pemulihan value variabel
        jur_Kurs = Kurs_Asal                    'Idem
    End Sub
    Public Sub _______jurKredit(COA As String, JumlahMutasi As Decimal)
        If JumlahMutasi = 0 Or COA = Kosongan Then Return
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            jur_StatusPenyimpananJurnal_Lengkap = False
            Return
        End If
        jur_DK = dk_K
        jur_COA = COA
        jur_JumlahMutasi = JumlahMutasi
        Dim KodeMataUang_Asal As String = jur_KodeMataUang
        Dim Kurs_Asal As Decimal = jur_Kurs
        If COA = KodeTautanCOA_BiayaSelisihPencatatan _     'Ini jangan dihapus..! Karena sangat mungkin COA IDR masuk ke jurnal MUA
            Or COA = KodeTautanCOA_LabaRugiSelisihKurs _    'Idem
            Then                                            'Idem
            jur_KodeMataUang = KodeMataUang_IDR             'Idem
            jur_Kurs = 1                                    'Idem
        End If                                              'Idem
        ______________________________________SimpanJurnal_PerBaris()
        jur_KodeMataUang = KodeMataUang_Asal    'Pemulihan value variabel
        jur_Kurs = Kurs_Asal                    'Idem
    End Sub
    Public Sub _______jurKreditBankCashOUT(DitanggungOleh As String, COASaranaPembayaran As String, JumlahKredit As Decimal, JumlahTransfer As Decimal, BiayaAdministrasiBank As Decimal)
        If BiayaAdministrasiBank > 0 Then 'Jika Ada Biaya Administrasi Bank
            If DitanggungOleh = DitanggungOleh_LawanTransaksi Then
                _______jurKredit(COASaranaPembayaran, JumlahTransfer)
                _______jurKredit(COASaranaPembayaran, BiayaAdministrasiBank)
                _______jurKredit(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
            End If
            If DitanggungOleh = DitanggungOleh_Perusahaan Then
                _______jurKredit(COASaranaPembayaran, JumlahKredit)
                _______jurKredit(COASaranaPembayaran, BiayaAdministrasiBank)
            End If
        Else 'Normal, tanpa Biaya Administrasi Bank :
            _______jurKredit(COASaranaPembayaran, JumlahKredit)
        End If
    End Sub
    Public Sub ___jurDebetBankCashIN(DitanggungOleh As String, COASaranaPembayaran As String, JumlahDebet As Decimal, JumlahTransfer As Decimal, BiayaAdministrasiBank As Decimal)
        If BiayaAdministrasiBank > 0 Then 'Dengan Biaya Administrasi Bank
            If DitanggungOleh = DitanggungOleh_LawanTransaksi Then
                ___jurDebet(COASaranaPembayaran, JumlahTransfer)
            End If
            If DitanggungOleh = DitanggungOleh_Perusahaan Then
                ___jurDebet(COASaranaPembayaran, JumlahTransfer)
                ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
            End If
        Else 'Normal, tanpa Biaya Administrasi Bank :
            ___jurDebet(COASaranaPembayaran, JumlahDebet)
        End If
    End Sub
    Public Sub ______________________________________SimpanJurnal_PerBaris()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return '<-- Cegah penyimpanan Jurnal untuk Tahun Buku LAMPAU...!!!
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            jur_StatusPenyimpananJurnal_Lengkap = False
            Return
        End If
        Select Case jur_DK
            Case dk_D
                jur_JumlahDebet = jur_JumlahMutasi
                jur_JumlahKredit = 0
            Case dk_K
                jur_JumlahDebet = 0
                jur_JumlahKredit = jur_JumlahMutasi
        End Select
        jur_NomorID += 1
        jur_NamaAkun = AmbilValue_NamaAkun(jur_COA)
        jur_UsernameEntry = UserAktif
        jur_NamaUserEntry = NamaUserAktif
        LogikaApprovalJurnal()
        If jur_JenisJurnal = Nothing Then
            MsgBox("Jenis Jurnal belum ditentukan." & Enter2Baris & "Penyimpanan Jurnal dibatalkan.")
            jur_StatusPenyimpananJurnal_PerBaris = False
            jur_StatusPenyimpananJurnal_Lengkap = False
            Return
        End If
        AksesDatabase_Transaksi(Buka)
        jur_QueryPenyimpanan = " INSERT INTO tbl_Transaksi VALUES ( '" &
            jur_NomorID & "', '" &
            jur_NomorJV & "', '" &
            jur_COA & "', '" &
            jur_NamaAkun & "', '" &
            TanggalFormatSimpan(jur_TanggalTransaksi) & "', '" &
            jur_JenisJurnal & "', '" &
            jur_KodeDokumen & "', '" &
            jur_NomorPO & "', '" &
            jur_KodeProject & "', '" &
            jur_NamaProduk & "', '" &
            jur_Referensi & "', '" &
            jur_Bundelan & "', '" &
            jur_TanggalInvoice & "', '" &
            jur_NomorInvoice & "', '" &
            jur_NomorFakturPajak & "', '" &
            jur_KodeLawanTransaksi & "', '" &
            jur_NamaLawanTransaksi & "', '" &
            jur_KodeMataUang & "', '" &
            DesimalFormatSimpan(jur_Kurs) & "', '" &
            jur_DK & "', '" &
            DesimalFormatSimpan(jur_JumlahDebet) & "', '" &
            DesimalFormatSimpan(jur_JumlahKredit) & "', '" &
            jur_UraianTransaksi & "', '" &
            jur_Direct & "', '" &
            jur_StatusApprove & "', '" &
            Kosongan & "', '" &
            jur_UsernameEntry & "', '" &
            jur_NamaUserEntry & "', '" &
            jur_UsernameApprove & "', '" &
            jur_NamaUserApprove & "' ) "
        cmd = New OdbcCommand(jur_QueryPenyimpanan, KoneksiDatabaseTransaksi)
        Try
            If jur_NomorJV = jur_NomorJV_GagalSimpan _
                Or jur_AkunTerdaftar = False _
                Or jur_TahunTransaksiSesuai = False _
                Then
                jur_StatusPenyimpananJurnal_PerBaris = False
                If jur_AkunTerdaftar = False Then
                    PesanUntukProgrammer("COA tidak terdaftar...!!!" & Enter2Baris &
                                         "INI PESAN DARI SUB PENYIMPANAN JURNAL." & Enter2Baris &
                                         "COA : " & jur_COA)
                End If
            Else
                cmd_ExecuteNonQuery()    '<-------- EKSEKUSI PENYIMPANAN ---||
                jur_StatusPenyimpananJurnal_PerBaris = True
            End If
        Catch ex As Exception
            jur_StatusPenyimpananJurnal_PerBaris = False
        End Try
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            jur_StatusPenyimpananJurnal_Lengkap = False
            jur_NomorJV_GagalSimpan = jur_NomorJV
            Dim cmdHapusJurnal = New OdbcCommand("DELETE FROM tbl_transaksi WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmdHapusJurnal.ExecuteNonQuery()
        End If
        AksesDatabase_Transaksi(Tutup)
        TampilkanPaksaCOA(jur_COA)
        'ResetValueJurnal() Sudah tidak relevan di sini. Sub 'ResetValueJurnal' dieksekusi Sebelum Penjurnalan di masing-masing Inputan.
        jur_JumlahMutasi = 0
        jur_JumlahDebet = 0
        jur_JumlahKredit = 0
    End Sub

    Public Sub ResetValueJurnal()
        SistemPenomoranOtomatis_ID_Jurnal()
        jur_JenisJurnal = Kosongan
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_NamaProduk = Kosongan
        jur_Bundelan = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NomorFakturPajak = Kosongan
        jur_KodeLawanTransaksi = Kosongan
        jur_NamaLawanTransaksi = Kosongan
        jur_KodeMataUang = KodeMataUang_IDR
        jur_Kurs = 1
        jur_UraianTransaksi = Kosongan
        jur_Direct = 0
        jur_StatusApprove = 0
        '------------------------------------------
        jur_TahunTransaksiSesuai = True
        jur_StatusPenyimpananJurnal_PerBaris = True
        jur_StatusPenyimpananJurnal_Lengkap = True
    End Sub


    Public Sub SetMataUangIDR_UntukSimpanJurnalPerBaris()
        jur_KodeMataUang = KodeMataUang_IDR
        jur_Kurs = 1
    End Sub

    Public Sub LogikaApprovalJurnal()
        If SistemApprovalPerusahaan = True Then
            jur_StatusApprove = 0
            jur_UsernameApprove = Nothing
            jur_NamaUserApprove = Nothing
        Else
            jur_StatusApprove = 1
            jur_UsernameApprove = UserAktif
            jur_NamaUserApprove = NamaUserAktif
        End If
        If StatusImportJurnal = True Then
            jur_StatusApprove = 1
            jur_UsernameApprove = UserAktif
            jur_NamaUserApprove = NamaUserAktif
        End If
    End Sub

    Public KodeAkun_Tembak
    Public NamaAkun_Tembak
    Public Sub PengisianValue_NamaAkun()
        BukaDatabaseGeneral_Kondisional()
        Dim cmdAKUN = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA WHERE COA = '" & KodeAkun_Tembak & "' ", KoneksiDatabaseGeneral)
        Dim drAKUN = cmdAKUN.ExecuteReader
        drAKUN.Read()
        If drAKUN.HasRows Then
            NamaAkun_Tembak = drAKUN.Item("Nama_Akun")
            jur_AkunTerdaftar = True
        Else
            jur_AkunTerdaftar = False
        End If
        TutupDatabaseGeneral_Kondisional()
    End Sub

    Public Sub SistemPenomoranOtomatis_ID_Jurnal()
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nomor_ID FROM tbl_Transaksi WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_Transaksi) ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        Dim jur_NomorID_Lama
        If Not drKHUSUS.HasRows Then
            jur_NomorID_Lama = 0
        Else
            jur_NomorID_Lama = drKHUSUS.Item("Nomor_ID")
        End If
        TutupDatabaseTransaksi_Kondisional()
        jur_NomorID = jur_NomorID_Lama 'Nanti akan ditambahkan 1 pada saat penyimpanan transaksi/jurnal
    End Sub

    Public Sub SistemPenomoranOtomatis_NomorJV()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            jur_NomorJV = 0
            Return
        End If
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi WHERE Nomor_JV IN (SELECT MAX(Nomor_JV) FROM tbl_Transaksi) ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then
            jur_NomorJV = drKHUSUS.Item("Nomor_JV") + 1
        Else
            jur_NomorJV = 1
        End If
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    Public Sub KoreksiSelisihJurnal(NomorJV)

        Dim COA As String
        Dim KodeMataUang As String = Kosongan
        Dim Kurs As Decimal
        Dim DK As String
        Dim JumlahDebet As Decimal
        Dim JumlahKredit As Decimal
        Dim JumlahDebet_IDR As Int64
        Dim JumlahKredit_IDR As Int64

        Dim TotalDebet_IDR As Int64 = 0
        Dim TotalKredit_IDR As Int64 = 0
        Dim Selisih_IDR As Integer

        Dim JumlahBarisDebet As Integer = 0
        Dim JumlahBarisKredit As Integer = 0

        BukaDatabaseTransaksi_Kondisional()

        Dim cmdKHUSUS = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader

        Dim tabelJurnalAwal As New DataTable
        Dim TabelJurnalBaru As New DataTable
        tabelJurnalAwal.Columns.Clear()
        tabelJurnalAwal.Rows.Clear()
        tabelJurnalAwal.Columns.Add("COA_", GetType(String))
        tabelJurnalAwal.Columns.Add("Kode_Mata_Uang", GetType(String))
        tabelJurnalAwal.Columns.Add("D_K", GetType(String))
        tabelJurnalAwal.Columns.Add("Kurs_", GetType(Decimal))
        tabelJurnalAwal.Columns.Add("Jumlah_Debet", GetType(Decimal))
        tabelJurnalAwal.Columns.Add("Jumlah_Kredit", GetType(Decimal))
        TabelJurnalBaru = tabelJurnalAwal.Clone()

        Do While drKHUSUS.Read
            COA = drKHUSUS.Item("COA")
            KodeMataUang = drKHUSUS.Item("Kode_Mata_Uang")
            DK = drKHUSUS.Item("D_K")
            Kurs = drKHUSUS.Item("Kurs")
            JumlahDebet = drKHUSUS.Item("Jumlah_Debet")
            JumlahKredit = drKHUSUS.Item("Jumlah_Kredit")
            JumlahDebet_IDR = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, JumlahDebet)
            JumlahKredit_IDR = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, JumlahKredit)
            If DK = dk_D Then
                TotalDebet_IDR += JumlahDebet_IDR
                JumlahBarisDebet += 1
            Else
                TotalKredit_IDR += JumlahKredit_IDR
                JumlahBarisKredit += 1
            End If
            tabelJurnalAwal.Rows.Add(COA, KodeMataUang, DK, Kurs, JumlahDebet, JumlahKredit)
        Loop

        TutupDatabaseTransaksi_Kondisional()

        Selisih_IDR = TotalDebet_IDR - TotalKredit_IDR

        'PesanUntukProgrammer(
        '    "Jumlah Baris Debet : " & JumlahBarisDebet & Enter2Baris &
        '    "Jumlah Baris Kredit : " & JumlahBarisKredit & Enter2Baris &
        '    "Total Debet : " & TotalDebet_IDR & Enter2Baris &
        '    "Total Kredit : " & TotalKredit_IDR & Enter2Baris &
        '    "Selisih : " & Selisih_IDR & Enter2Baris &
        '    "")

        If TotalDebet_IDR = 0 Then Return '(Ini berarti tidak ada jurnal yang terbaca)

        If Selisih_IDR = 0 Then Return

        Dim NomorUrutJurnal As Integer = 0
        For Each row As DataRow In tabelJurnalAwal.Rows
            NomorUrutJurnal += 1
            If row("D_K") = dk_D Then
                TabelJurnalBaru.Rows.Add(row("COA_"), row("Kode_Mata_Uang"), dk_D, row("Kurs_"), row("Jumlah_Debet"), 0)
            Else
                TabelJurnalBaru.Rows.Add(row("COA_"), row("Kode_Mata_Uang"), dk_K, row("Kurs_"), 0, row("Jumlah_Kredit"))
            End If
            If NomorUrutJurnal = JumlahBarisDebet Then
                NomorUrutJurnal += 1
                If Selisih_IDR < 0 Then
                    TabelJurnalBaru.Rows.Add(KodeTautanCOA_BiayaSelisihPencatatan, KodeMataUang_IDR, dk_D, 1, -Selisih_IDR, 0)
                End If
            End If
        Next
        If Selisih_IDR > 0 Then
            TabelJurnalBaru.Rows.Add(KodeTautanCOA_BiayaSelisihPencatatan, KodeMataUang_IDR, dk_K, 1, 0, Selisih_IDR)
        End If

        HapusJurnal_BerdasarkanNomorJV(NomorJV)

        SistemPenomoranOtomatis_ID_Jurnal()

        jur_NomorJV = NomorJV

        For Each row As DataRow In TabelJurnalBaru.Rows
            COA = row("COA_")
            KodeMataUang = row("Kode_Mata_Uang")
            Kurs = AmbilAngka_Desimal(row("Kurs_"))
            DK = row("D_K")
            JumlahDebet = AmbilAngka_Desimal(row("Jumlah_Debet"))
            JumlahKredit = AmbilAngka_Desimal(row("Jumlah_Kredit"))
            jur_KodeMataUang = KodeMataUang
            jur_Kurs = Kurs
            If DK = dk_D Then
                ___jurDebet(COA, JumlahDebet)
            Else
                _______jurKredit(COA, JumlahKredit)
            End If
        Next

    End Sub

    Public Function AmbilValue_ID_CPU() As String
        Dim Komputer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
                                      "{impersonationLevel = impersonate}!\\" &
                                      Komputer & "\root\cimv2")
        Dim Processor As Object = wmi.ExecQuery("Select * from Win32_Processor")
        Dim idcpu As String = ""
        For Each CPU As Object In Processor
            idcpu = CPU.ProcessorId
        Next
        Return idcpu
    End Function

    Public ProsesTutupBuku As Boolean = False

    Public StatusTrialBalance As Boolean = True 'Biarkan Default True. Nanti saat loading juga akan ada logika-logika yang sesuai dengan yang seharusnya.
    Public Sub TrialBalance_Mentahkan()
        'INI SUDAH TIDAK PENTING LAGI....!!!
        'If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return '(Tahun Buku Lampau Tidak Butuh Trial Balance)
        'If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Return 'Trial Balance Tidak dibutuhkan untuk Tahun Buku Backup
        'BukaDatabaseGeneral_Kondisional()
        'If StatusKoneksiDatabaseGeneral = False Then Return
        'Dim cmdKondisional = New OdbcCommand(" UPDATE tbl_InfoData SET Trial_Balance = 0 WHERE Tahun_Buku = '" & TahunBukuAktif & "' ", KoneksiDatabaseGeneral)
        'Try
        '    cmdKondisional.ExecuteNonQuery()
        '    StatusSuntingDatabase = True
        '    StatusTrialBalance = False
        'Catch ex As Exception
        '    StatusSuntingDatabase = False
        'End Try
        'TutupDatabaseGeneral_Kondisional()
        'usc_TutupBuku.btn_TrialBalance.IsEnabled = True
        'If JenisTahunBuku <> JenisTahunBuku_LAMPAU Then usc_TutupBuku.btn_TransferSaldoDanTutupBuku.IsEnabled = False
    End Sub

    Public Sub TrialBalance_Matangkan()
        BukaDatabaseGeneral_Kondisional()
        If StatusKoneksiDatabaseGeneral = False Then Return
        Dim cmdKondisional = New OdbcCommand(" UPDATE tbl_InfoData SET Trial_Balance = 1 WHERE Tahun_Buku = '" & TahunBukuAktif & "' ", KoneksiDatabaseGeneral)
        cmdKondisional.ExecuteNonQuery()
        TutupDatabaseGeneral_Kondisional()
        StatusTrialBalance = True
    End Sub


    Public notif_NomorID
    Public notif_Jenis
    Public notif_Waktu
    Public notif_Notifikasi
    Public notif_HalamanTarget
    Public notif_Pesan
    Public notif_StatusDibaca
    Public notif_StatusDieksekusi
    Public Sub SimpanNotifikasi()
        Dim QueryPenyimpanan = " INSERT INTO tbl_Notifikasi VALUES ( " &
            " '" & notif_NomorID & "', " &
            " '" & notif_Jenis & "', " &
            " '" & TanggalFormatSimpan(notif_Waktu) & "', " &
            " '" & notif_Notifikasi & "', " &
            " '" & notif_HalamanTarget & "', " &
            " '" & notif_Pesan & "', " &
            " '" & notif_StatusDibaca & "', " &
            " '" & notif_StatusDieksekusi & "' ) "

        If ProsesTutupBuku = False Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        Else
            BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi_Alternatif)
            cmd_ExecuteNonQuery()
            TutupDatabaseTransaksi_Alternatif()
        End If

        'Reset Value setelah Penyimpanan
        notif_Jenis = Kosongan
        notif_Waktu = Today
        notif_Notifikasi = Kosongan
        notif_HalamanTarget = Kosongan
        notif_Pesan = Kosongan
        notif_StatusDibaca = 0
        notif_StatusDieksekusi = 0
    End Sub

    Public Sub TampilkanPanelNotifikasi()
        frm_BOOKU.pnl_Notifikasi.Visible = True
        frm_BOOKU.mnu_Notifikasi.Text = "Tutup"
        frm_BOOKU.IsiKontenNotifikasi()
        VisibilitasNotifikasi = True
    End Sub

    Public Sub TutupPanelNotifikasi()
        frm_BOOKU.pnl_Notifikasi.Visible = False
        frm_BOOKU.mnu_Notifikasi.Text = "Notifikasi"
        VisibilitasNotifikasi = False
    End Sub

    'Fitur Dalam Pengembangan
    Public Sub FiturDalamPengembangan()
        MsgBox("Mohon maaf." & Enter2Baris & "Menu/Fitur ini masih dalam tahap pengembangan.")
    End Sub

    'Fitur Belum Bisa Digunakan
    Public Sub FiturBelumBisaDigunakan()
        MsgBox("Mohon maaf." & Enter2Baris & "Menu/Fitur ini belum bisa digunakan.")
    End Sub

    Public Sub MenuDalamPerbaikan()
        MsgBox("Mohon maaf." & Enter2Baris & "Menu ini sedang dalam perbaikan.")
    End Sub

    Public Sub FiturDalamPerbaikan()
        MsgBox("Mohon maaf." & Enter2Baris & "Fitur ini sedang dalam perbaikan.")
    End Sub

    Public Sub KelolaDataPembayaranDiBukuPengawasanPenerimaan()
        Dim Pesan As String = "Data pembayaran tidak dapat dikelola di halaman ini." & Enter2Baris &
            "Silakan pergi ke Buku Pengawasan Bukti Penerimaan Bank-Cash untuk mengedit/hapus data ini." & Enter2Baris &
            "Lanjut ke Buku Pengawasan?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbYes Then frm_BOOKU.BukaModul_BukuPengawasanBuktiPenerimaanBankCash()
    End Sub

    Public Sub KelolaDataPembayaranDiBukuPengawasanPengeluaran()
        Dim Pesan As String = "Data pembayaran tidak dapat dikelola di halaman ini." & Enter2Baris &
            "Silakan pergi ke Buku Pengawasan Bukti Pengeluaran Bank-Cash untuk mengedit/hapus data ini." & Enter2Baris &
            "Lanjut ke Buku Pengawasan?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbYes Then frm_BOOKU.BukaModul_BukuPengawasanBuktiPengeluaranBankCash()
    End Sub


    Public Sub LoginGagal()
        StatusLogin = False
        frm_BOOKU.StatusMenuLevel_1_Operator() 'Jangan dihapus..!!! Ini penting, untuk meng-enabled menu-menu tertentu yang sudah di-enabled di level user paling rendah. Supaya tidak ada pengulangan coding di sini.
        frm_BOOKU.StatusMenuPosisiLogout()
        KeluarDariSemuaModul()
    End Sub

    Public Sub BeriKeteranganKomputerTerdaftar()
        Dim DataKeteranganKomputerTerdaftar
        DataKeteranganKomputerTerdaftar = HeaderConfig &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            EnkripsiTeks("TERDAFTAR") & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            EnkripsiTeks(ID_CPU) &
            FooterConfig
        Try
            My.Computer.FileSystem.WriteAllText(FilePathRegistrasiPerangkat, DataKeteranganKomputerTerdaftar, False)
            My.Computer.FileSystem.WriteAllText(FilePathRegistrasiPerangkat_Backup, DataKeteranganKomputerTerdaftar, False) 'File Backup dibutuhkan untuk antisipasi. Khawatir ada trouble saat registrasi ke-2 yang menyebabkan perangkat di-unreg.
            ProsesRegistrasiPerangkat = True
            ProsesRegistrasiPerusahaan = True
        Catch ex As Exception
            ProsesRegistrasiPerangkat = False
            ProsesRegistrasiPerusahaan = False
        End Try
    End Sub

    Public Sub BeriKeteranganKomputerTidakTerdaftar()
        Dim DataKeteranganKomputerTidakTerdaftar
        DataKeteranganKomputerTidakTerdaftar = HeaderConfig &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            AcakKarakter(213) & Enter1Baris &
            EnkripsiTeks(ID_CPU) &
            FooterConfig
        Try
            My.Computer.FileSystem.WriteAllText(FilePathRegistrasiPerangkat, DataKeteranganKomputerTidakTerdaftar, False)
        Catch ex As Exception
        End Try
        ProsesRegistrasiPerangkat = False    '|| Sudah Benar disini.......!!!
        ProsesRegistrasiPerusahaan = False   '|| Jangan dipindah-pindah...!!!
    End Sub

    Public Sub BeriKeteranganVersiDanApdetPerangkat()
        Dim DataKeteranganVersiDanApdet
        DataKeteranganVersiDanApdet = HeaderConfig &
            "DJdkf798iudfhkjfhdfk^=D" & Enter1Baris &
            EnkripsiAngkaAES1(VersiBooku_SisiAplikasi) & Enter1Baris &
            Enter1Baris &
            "kf25542438dfjdsfjkh&sdrf%" & Enter1Baris &
            EnkripsiAngkaAES1(ApdetBooku_SisiAplikasi) & Enter1Baris &
            Enter1Baris &
            FooterConfig
        Try
            My.Computer.FileSystem.WriteAllText(FilePathVersiDanApdetAplikasi, DataKeteranganVersiDanApdet, False)
            ProsesRegistrasiPerangkat = True
            ProsesRegistrasiPerusahaan = True
        Catch ex As Exception
            ProsesRegistrasiPerangkat = False
            ProsesRegistrasiPerusahaan = False
        End Try
    End Sub

    Sub KeluarDariSemuaModul()
        frm_POPembelian_Lokal_Barang.Close()
        frm_POPembelian_Lokal_Jasa.Close()
        frm_POPembelian_Lokal_BarangDanJasa.Close()
        frm_POPembelian_Lokal_JasaKonstruksi.Close()
        frm_POPembelian_Lokal_Semua.Close()
        frm_POPembelian_Impor_Barang.Close()
        frm_POPembelian_Impor_Jasa.Close()
        frm_POPembelian_Impor_Semua.Close()
        frm_SuratJalanPembelian.Close()
        frm_BASTPembelian.Close()
        frm_InvoicePembelian.Close()
        frm_InvoicePembelian_DenganPO_Lokal_Rutin.Close()
        frm_InvoicePembelian_DenganPO_Lokal_Termin.Close()
        frm_InvoicePembelian_DenganPO_Impor_Rutin.Close()
        frm_InvoicePembelian_DenganPO_Impor_Termin.Close()
        frm_InvoicePembelian_TanpaPO_Lokal_Barang.Close()
        frm_InvoicePembelian_TanpaPO_Lokal_Jasa.Close()
        frm_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Close()
        frm_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Close()
        frm_InvoicePembelian_TanpaPO_Impor_Barang.Close()
        frm_InvoicePembelian_TanpaPO_Impor_Jasa.Close()
        frm_BukuPembelian.Close()
        frm_BukuPembelian_lokal.Close()
        frm_BukuPembelian_Impor.Close()
        frm_ReturPembelian.Close()
        frm_POPenjualan_Lokal_Barang.Close()
        frm_POPenjualan_Lokal_Jasa.Close()
        frm_POPenjualan_Lokal_BarangDanJasa.Close()
        frm_POPenjualan_Lokal_JasaKonstruksi.Close()
        frm_POPenjualan_Lokal_Semua.Close()
        frm_POPenjualan_Ekspor.Close()
        frm_SuratJalanPenjualan.Close()
        frm_BASTPenjualan.Close()
        frm_InvoicePenjualan.Close()
        frm_InvoicePenjualan_DenganPO_Lokal_Rutin.Close()
        frm_InvoicePenjualan_DenganPO_Lokal_Termin.Close()
        frm_InvoicePenjualan_DenganPO_Ekspor_Rutin.Close()
        frm_InvoicePenjualan_DenganPO_Ekspor_Termin.Close()
        frm_InvoicePenjualan_Asset.Close()
        frm_InvoicePenjualan_TanpaPO_Lokal_Barang.Close()
        frm_InvoicePenjualan_TanpaPO_Lokal_Jasa.Close()
        frm_InvoicePenjualan_TanpaPO_Lokal_BarangdanJasa.Close()
        frm_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Close()
        frm_InvoicePenjualan_TanpaPO_Ekspor.Close()
        frm_BukuPenjualan.Close()
        frm_BukuPenjualan_Lokal.Close()
        frm_BukuPenjualan_Ekspor.Close()
        frm_ReturPenjualan.Close()
        frm_BukuBesar.Close()
        frm_BukuKas.Close()
        frm_BukuBank.Close()
        frm_BukuCashAdvance.Close()
        frm_BukuPettyCash.Close()
        frm_BukuBankGaransi.Close()
        frm_StockOpname.Close()
        frm_BahanPenolong.Close()
        frm_BahanBaku.Close()
        frm_BarangDalamProses_CekFisik.Close()
        frm_BarangDalamProses_TarikanData.Close()
        frm_BarangJadi.Close()
        frm_JurnalUmum.Close()
        frm_JurnalAdjusment.Close()
        frm_Adjusment_PenyusutanAsset.Close()
        frm_Adjusment_Amortisasi.Close()
        frm_Adjusment_Forex.Close()
        frm_Adjusment_HPP.Close()
        win_Pengaturan.Close()
        frm_SaldoAwalHutangUsaha.Close()
        frm_DaftarAmortisasiBiaya.Close()
        frm_DaftarPenyusutanAssetTetap.Close()
        frm_BukuDisposalAssetTetap.Close()
        frm_DataUser.Close()
        frm_DataCOA.Close()
        frm_DataMitra.Close()
        frm_DataKaryawan.Close()
        frm_DaftarPemegangSaham.Close()
        frm_DataPemegangSaham.Close()
        frm_DataProject.Close()
        frm_Kurs.Close()
        frm_DPHU.Close()
        frm_BBHU.Close()
        frm_LaporanTrialBalance.Close()
        frm_LaporanNeracaLajur.Close()
        frm_LaporanHPP.Close()
        frm_LaporanNeraca_Bulanan.Close()
        frm_LaporanNeraca_Tahunan.Close()
        frm_LaporanLabaRugi_Bulanan.Close()
        frm_LaporanLabaRugi_Tahunan.Close()
        frm_BukuPengawasanPemindahbukuan.Close()
        frm_BukuPenjualanEceran.Close()
        frm_BukuPengawasanAktivaLainnya.Close()
        frm_BukuPengawasanPiutangUsaha.Close()
        frm_BukuPengawasanHutangUsaha.Close()
        frm_BukuPengawasanHutangUsaha_Afiliasi.Close()
        frm_BukuPengawasanHutangUsaha_NonAfiliasi.Close()
        frm_BukuPengawasanHutangUsaha_Impor_USD.Close()
        frm_BukuPengawasanHutangUsaha_Impor_AUD.Close()
        frm_BukuPengawasanHutangUsaha_Impor_JPY.Close()
        frm_BukuPengawasanHutangUsaha_Impor_CNY.Close()
        frm_BukuPengawasanHutangUsaha_Impor_EUR.Close()
        frm_BukuPengawasanHutangUsaha_Impor_SGD.Close()
        frm_BukuPengawasanHutangUsaha_Impor_GBP.Close()
        frm_BukuPengawasanBuktiPengeluaranBankCash.Close()
        frm_BukuPengawasanBuktiPenerimaanBankCash.Close()
        frm_BundelPengajuanPengeluaranBankCash.Close()
        frm_BukuPengawasanHutangKaryawan.Close()
        frm_BukuPengawasanHutangPemegangSaham.Close()
        frm_BukuPengawasanPiutangPemegangSaham.Close()
        frm_BukuPengawasanPiutangUsaha.Close()
        frm_BukuPengawasanPiutangUsaha_Afiliasi.Close()
        frm_BukuPengawasanPiutangUsaha_NonAfiliasi.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_USD.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_AUD.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_JPY.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_CNY.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_EUR.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_SGD.Close()
        frm_BukuPengawasanPiutangUsaha_Ekspor_GBP.Close()
        frm_BukuPengawasanPiutangKaryawan.Close()
        frm_BukuPengawasanHutangBank.Close()
        frm_BukuPengawasanHutangLeasing.Close()
        frm_BukuPengawasanHutangPihakKetiga.Close()
        frm_BukuPengawasanPiutangPihakKetiga.Close()
        frm_BukuPengawasanHutangAfiliasi.Close()
        frm_BukuPengawasanPiutangAfiliasi.Close()
        frm_BukuPengawasanHutangDividen.Close()
        frm_BukuPengawasanPiutangDividen.Close()
        frm_BukuPengawasanDepositOperasional.Close()
        frm_BukuPengawasanHutangPPhPasal21.Close()
        frm_BukuPengawasanHutangPPhPasal22_Impor.Close()
        frm_BukuPengawasanHutangPPhPasal22_Lokal.Close()
        frm_BukuPengawasanHutangPPhPasal23.Close()
        frm_BukuPengawasanHutangPPhPasal25.Close()
        frm_BukuPengawasanHutangPPhPasal26.Close()
        frm_BukuPengawasanHutangPPhPasal42.Close()
        frm_BukuPengawasanPelaporanPPN.Close()
        frm_BukuPengawasanKetetapanPajak.Close()
        frm_BukuPengawasanPajakImpor.Close()
        frm_BukuPengawasanGaji.Close()
        frm_BukuPengawasanHutangBPJSKesehatan.Close()
        frm_BukuPengawasanHutangBPJSKetenagakerjaan.Close()
        frm_BukuPengawasanHutangKoperasiKaryawan.Close()
        frm_BukuPengawasanHutangSerikat.Close()
        frm_BukuPengawasanBuktiPotongPPh_Paid.Close()
        frm_BukuPengawasanBuktiPotongPPh_Prepaid.Close()
        frm_TutupBuku.Close()
        'TechnicalSupport
        frm_phpMyAdmin.Close()
        'App Developer :
        frm_ManajemenAplikasi.Close()
        frm_ManajemenClient.Close()
        frm_ManajemenKurs.Close()
        frm_Kurs.Close()
        frm_DataProdukApp.Close()
        frm_DataPerangkatApp.Close()
        frm_DataVoucherApp.Close()
        frm_TabPokok.Close()
        frm_TryApp.Close()
        TutupPanelNotifikasi()
    End Sub

    Sub PosisiBug1()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 1")
    End Sub

    Sub PosisiBug2()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 2")
    End Sub

    Sub PosisiBug3()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 3")
    End Sub

    Sub PosisiBug4()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 4")
    End Sub

    Sub PosisiBug5()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 5")
    End Sub

    Sub PosisiBug6()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 6")
    End Sub

    Sub PosisiBug7()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 7")
    End Sub

    Sub PosisiBug8()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 8")
    End Sub

    Sub PosisiBug9()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Posisi Bug 9")
    End Sub

    Sub YeuhDiDieu()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then MsgBox("Yeuh...! Di dieu...!!!!")
    End Sub

    Sub KontenComboDaftarBank_Public(ByVal DaftarBank As ComboBox)
        DaftarBank.Items.Clear()
        Dim KodeAkun_Bank
        Dim NamaAkun_Bank
        Dim Item_Bank
        AksesDatabase_General(Buka)
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_Bank, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_Bank = drKhusus.Item("COA")
            NamaAkun_Bank = drKhusus.Item("Nama_Akun")
            Item_Bank = KodeAkun_Bank & StripPemisah & NamaAkun_Bank
            DaftarBank.Items.Add(Item_Bank)
        Loop
        AksesDatabase_General(Tutup)
        DaftarBank.Text = Nothing
        DaftarBank.SelectedValue = Nothing
    End Sub

    Sub KontenComboSaranaPembayaran_Public(ByVal SaranaPembayaran As ComboBox)
        SaranaPembayaran.Items.Clear()
        Dim KodeAkun_SaranaPembayaran
        Dim NamaAkun_SaranaPembayaran
        Dim Item_SaranaPembayaran
        AksesDatabase_General(Buka)
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_SaranaPembayaran, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_SaranaPembayaran = drKhusus.Item("COA")
            NamaAkun_SaranaPembayaran = drKhusus.Item("Nama_Akun")
            Item_SaranaPembayaran = KodeAkun_SaranaPembayaran & StripPemisah & NamaAkun_SaranaPembayaran
            SaranaPembayaran.Items.Add(Item_SaranaPembayaran)
        Loop
        AksesDatabase_General(Tutup)
        SaranaPembayaran.Text = Kosongan
        SaranaPembayaran.SelectedValue = Kosongan
    End Sub

    Public DitanggungOleh_Perusahaan = "Perusahaan"
    Public DitanggungOleh_LawanTransaksi = "Lawan Transaksi"
    Sub KontenComboDitanggungOleh_Public(ByVal DitanggungOleh As ComboBox)
        DitanggungOleh.Items.Clear()
        DitanggungOleh.Items.Add(DitanggungOleh_Perusahaan)
        DitanggungOleh.Items.Add(DitanggungOleh_LawanTransaksi)
    End Sub

    Public Pembebanan_Dipotong = "Dipotong"
    Public Pembebanan_Ditambahkan = "Ditambahkan"
    Public Pembebanan_Diganti = "Diganti"
    Sub KontenComboPembebanan_Public(ByVal Pembebanan As ComboBox)
        Pembebanan.Items.Clear()
        Pembebanan.Items.Add(Pembebanan_Dipotong)
        Pembebanan.Items.Add(Pembebanan_Ditambahkan)
    End Sub

    Sub KontenComboJenisJurnal_Public(ByVal JenisJurnal As ComboBox)
        JenisJurnal.Items.Clear()

        AksesDatabase_General(Buka)
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_SaranaPembayaran, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            Dim JenisJurnal_Item = drKhusus.Item("Nama_Akun")
            JenisJurnal.Items.Add(JenisJurnal_Item)
        Loop
        AksesDatabase_General(Tutup)

        JenisJurnal.Items.Add(JenisJurnal_HutangBank)
        JenisJurnal.Items.Add(JenisJurnal_HutangLeasing)
        JenisJurnal.Items.Add(JenisJurnal_HutangPihakKetiga)
        JenisJurnal.Items.Add(JenisJurnal_PiutangPihakKetiga)
        JenisJurnal.Items.Add(JenisJurnal_HutangAfiliasi)
        JenisJurnal.Items.Add(JenisJurnal_PiutangAfiliasi)
        JenisJurnal.Items.Add(JenisJurnal_Dividen)
        JenisJurnal.Items.Add(JenisJurnal_Pembelian)
        JenisJurnal.Items.Add(JenisJurnal_Penjualan)
        JenisJurnal.Items.Add(JenisJurnal_ReturPembelian)
        JenisJurnal.Items.Add(JenisJurnal_ReturPenjualan)
        JenisJurnal.Items.Add(JenisJurnal_Asset)
        JenisJurnal.Items.Add(JenisJurnal_DisposalAsset)
        JenisJurnal.Items.Add(JenisJurnal_BangunanDalamPenyelesaian)
        JenisJurnal.Items.Add(JenisJurnal_Gaji)
        JenisJurnal.Items.Add(JenisJurnal_CekBupot)
        JenisJurnal.Items.Add(JenisJurnal_HutangKaryawan)
        JenisJurnal.Items.Add(JenisJurnal_PiutangKaryawan)
        JenisJurnal.Items.Add(JenisJurnal_HutangPemegangSaham)
        JenisJurnal.Items.Add(JenisJurnal_PiutangPemegangSaham)
        JenisJurnal.Items.Add(JenisJurnal_Amortisasi)
        JenisJurnal.Items.Add(JenisJurnal_Penyusutan)
        JenisJurnal.Items.Add(JenisJurnal_PBk)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentForex)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentGaji)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentHPP)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentPajak)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentSaldoAwal)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentSelisih)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentLainnya)
        JenisJurnal.Text = Kosongan
        JenisJurnal.SelectedValue = Kosongan

    End Sub

    Sub KontenCombo_JenisJasa_Public(cmb_JenisJasa As ComboBox, LokasiWP As String)
        cmb_JenisJasa.Items.Clear()
        cmb_JenisJasa.Items.Add(JenisJasa_JasaLainnya)
        cmb_JenisJasa.Items.Add(JenisJasa_SewaTanahDanAtauBangunan)
        cmb_JenisJasa.Items.Add(JenisJasa_SewaAssetSelainTanahBangunan)
        cmb_JenisJasa.Items.Add(JenisJasa_BungaBagiHasil)
        cmb_JenisJasa.Items.Add(JenisJasa_Royalty)
        cmb_JenisJasa.Items.Add(JenisJasa_Dividen)
        If LokasiWP = LokasiWP_LuarNegeri Then cmb_JenisJasa.Items.Add(JenisJasa_LabaPajakBUT)
        cmb_JenisJasa.Items.Add(JenisJasa_Lainnya)
        cmb_JenisJasa.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeSetoran_Public(cmb_KodeSetoran As ComboBox, JenisPPh As String)
        cmb_KodeSetoran.Enabled = True
        cmb_KodeSetoran.Items.Clear()
        Select Case JenisPPh
            Case JenisPPh_Pasal21
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_401)
                If ProsesLoadingForm = False And ProsesResetForm = False Then cmb_KodeSetoran.DroppedDown = True
            Case JenisPPh_Pasal23
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_101)
                cmb_KodeSetoran.Items.Add(KodeSetoran_102)
                cmb_KodeSetoran.Items.Add(KodeSetoran_103)
                cmb_KodeSetoran.Items.Add(KodeSetoran_104)
                If ProsesLoadingForm = False And ProsesResetForm = False Then cmb_KodeSetoran.DroppedDown = True
            Case JenisPPh_Pasal42
                cmb_KodeSetoran.Items.Add(KodeSetoran_402)
                cmb_KodeSetoran.Items.Add(KodeSetoran_403)
                cmb_KodeSetoran.Items.Add(KodeSetoran_409)
                cmb_KodeSetoran.Items.Add(KodeSetoran_419)
                If ProsesLoadingForm = False And ProsesResetForm = False Then cmb_KodeSetoran.DroppedDown = True
            Case JenisPPh_Pasal26
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_101)
                cmb_KodeSetoran.Items.Add(KodeSetoran_102)
                cmb_KodeSetoran.Items.Add(KodeSetoran_103)
                cmb_KodeSetoran.Items.Add(KodeSetoran_104)
                cmb_KodeSetoran.Items.Add(KodeSetoran_105)
                If ProsesLoadingForm = False And ProsesResetForm = False Then cmb_KodeSetoran.DroppedDown = True
            Case Else
                cmb_KodeSetoran.Enabled = False
        End Select
        cmb_KodeSetoran.Text = Kosongan
    End Sub

    Sub PenentuanJenisPPhDanKodeSetoranDanTarifPPh_Public _
        (LokasiWP As String, JenisWP As String, JenisJasa As String, cmb_JenisPPh As ComboBox, cmb_KodeSetoran As ComboBox, txt_TarifPPh As TextBox)

        cmb_JenisPPh.Items.Clear()
        cmb_KodeSetoran.Items.Clear()

        Dim JenisPPh = Kosongan
        Dim KodeSetoran = Kosongan
        Dim TarifPPh = 2

        If LokasiWP = LokasiWP_DalamNegeri Then
            Select Case JenisJasa
                Case JenisJasa_JasaLainnya
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal21
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_104
                        TarifPPh = 2
                    End If
                Case JenisJasa_JasaKonstruksi
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_409
                        TarifPPh = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_409
                        TarifPPh = 2
                    End If
                Case JenisJasa_SewaAssetSelainTanahBangunan
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2
                    End If
                Case JenisJasa_SewaTanahDanAtauBangunan
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_403
                        TarifPPh = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_403
                        TarifPPh = 10
                    End If
                Case JenisJasa_BungaBagiHasil
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_102
                        TarifPPh = 15
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_102
                        TarifPPh = 15
                    End If
                Case JenisJasa_Royalty
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_103
                        TarifPPh = 15
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_103
                        TarifPPh = 15
                    End If
                Case JenisJasa_Dividen
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_419
                        TarifPPh = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_101
                        TarifPPh = 15
                    End If
                Case JenisJasa_Lainnya
                    cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
                    cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
                    cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                    End If
                    KodeSetoran = Kosongan
            End Select
        End If

        If LokasiWP = LokasiWP_LuarNegeri Then
            Select Case JenisJasa
                Case JenisJasa_JasaLainnya
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_104
                    TarifPPh = 20
                Case JenisJasa_JasaKonstruksi
                    JenisPPh = JenisPPh_Pasal42
                    KodeSetoran = KodeSetoran_409
                    TarifPPh = 2
                Case JenisJasa_SewaAssetSelainTanahBangunan
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_100
                    TarifPPh = 20
                Case JenisJasa_SewaTanahDanAtauBangunan
                    JenisPPh = JenisPPh_Pasal42
                    KodeSetoran = KodeSetoran_403
                    TarifPPh = 2
                Case JenisJasa_BungaBagiHasil
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_102
                    TarifPPh = 20
                Case JenisJasa_Royalty
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_103
                    TarifPPh = 20
                Case JenisJasa_Dividen
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_101
                    TarifPPh = 20
                Case JenisJasa_LabaPajakBUT
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_105
                    TarifPPh = 20
            End Select
        End If

        If JenisJasa = Kosongan Then
            cmb_JenisPPh.Text = Kosongan
            cmb_KodeSetoran.Text = Kosongan
        End If

        cmb_JenisPPh.Text = JenisPPh
        cmb_KodeSetoran.Text = KodeSetoran
        txt_TarifPPh.Text = TarifPPh

    End Sub


    Sub Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        MsgBox("Selisih ini nanti akan disesuaikan pada awal Pembukuan Tahun " & (TahunBukuAktif + 1).ToString & " melalui Jurnal Adjusment.")
    End Sub


    Public DatabaseTransaksi = "Database Transaksi"
    Public DatabaseGeneral = "Database General"
    Public Function AmbilNomorIdTerakhir(ByVal KelompokDatabase As String, ByVal NamaTabel As String)
        Dim NomorID_Terakhir = 0
        If KelompokDatabase = DatabaseTransaksi Then
            BukaDatabaseTransaksi_Kondisional()
            Dim cmdKhusus = New OdbcCommand(" SELECT Nomor_ID FROM " & NamaTabel & " WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) " &
                                  " FROM " & NamaTabel & ") ", KoneksiDatabaseTransaksi)
            Dim drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If Not drKhusus.HasRows Then
                NomorID_Terakhir = 0
            Else
                NomorID_Terakhir = drKhusus.Item("Nomor_ID")
            End If
            TutupDatabaseTransaksi_Kondisional()
        End If
        If KelompokDatabase = DatabaseGeneral Then
            BukaDatabaseGeneral_Kondisional()
            Dim cmdKhusus = New OdbcCommand(" SELECT Nomor_ID FROM " & NamaTabel & " WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) " &
                                  " FROM " & NamaTabel & ") ", KoneksiDatabaseGeneral)
            Dim drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                NomorID_Terakhir = drKhusus.Item("Nomor_ID")
            Else
                NomorID_Terakhir = 0
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return (NomorID_Terakhir)
    End Function

    Public Function AmbilNomorTerakhir(ByVal KelompokDatabase As String, ByVal NamaTabel As String, ByVal NamaKolom As String)
        Dim NomorID_Terakhir = 0
        If KelompokDatabase = DatabaseTransaksi Then
            BukaDatabaseTransaksi_Kondisional()
            Dim cmdKhusus = New OdbcCommand(" SELECT " & NamaKolom & " FROM " & NamaTabel &
                                  " WHERE " & NamaKolom & " IN (SELECT MAX(" & NamaKolom & ") " &
                                  " FROM " & NamaTabel & ") ", KoneksiDatabaseTransaksi)
            Dim drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If Not drKhusus.HasRows Then
                NomorID_Terakhir = 0
            Else
                NomorID_Terakhir = drKhusus.Item(NamaKolom)
            End If
            TutupDatabaseTransaksi_Kondisional()
        End If
        If KelompokDatabase = DatabaseGeneral Then
            BukaDatabaseGeneral_Kondisional()
            Dim cmdKhusus = New OdbcCommand(" SELECT " & NamaKolom & " FROM " & NamaTabel &
                                  " WHERE " & NamaKolom & " IN (SELECT MAX(" & NamaKolom & ") " &
                                  " FROM " & NamaTabel & ") ", KoneksiDatabaseGeneral)
            Dim drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                NomorID_Terakhir = drKhusus.Item(NamaKolom)
            Else
                NomorID_Terakhir = 0
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return (NomorID_Terakhir)
    End Function

    Public Function AmbilTahunTerlama_SisaHutangPajak(ByVal JenisPajak As String)
        Dim TahunTerlama
        BukaDatabaseTransaksi_Alternatif_Kondisional(TahunCutOff)
        Dim cmdKhusus = New OdbcCommand(" SELECT Tanggal_Transaksi FROM tbl_HutangPajak " &
                              " WHERE Tanggal_Transaksi IN (SELECT MIN(Tanggal_Transaksi) tbl_HutangPajak ) " &
                              " AND Jenis_Pajak = '" & JenisPajak & "' " &
                              " ORDER BY Tanggal_Transaksi ASC ",
                              KoneksiDatabaseTransaksi_Alternatif)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            TahunTerlama = Mid(drKhusus.Item("Tanggal_Transaksi").ToString, 7, 4)
        Else
            TahunTerlama = TahunBukuAktif
        End If
        TutupDatabaseTransaksi_Alternatif_Kondisional()
        Return TahunTerlama
    End Function

    Public Function AmbilTahunTerlama_BerdasarkanKolomTanggal(ByVal TahunDataBase As Integer, ByVal NamaTabel As String, ByVal NamaKolomTanggal As String) As Integer
        Dim TahunTerlama As Integer
        BukaDatabaseTransaksi_Alternatif_Kondisional(TahunDataBase)
        Dim cmdKhusus = New OdbcCommand(" SELECT " & NamaKolomTanggal & " FROM " & NamaTabel &
                              " WHERE " & NamaKolomTanggal & " IN (SELECT MIN(" & NamaKolomTanggal & ") " & NamaTabel & " ) " &
                              " ORDER BY " & NamaKolomTanggal & " ASC ",
                              KoneksiDatabaseTransaksi_Alternatif)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            TahunTerlama = AmbilAngka(Mid(drKhusus.Item(NamaKolomTanggal).ToString, 7, 4))
        Else
            TahunTerlama = TahunBukuAktif
        End If
        TutupDatabaseTransaksi_Alternatif_Kondisional()
        Return TahunTerlama
    End Function

    Public Function AmbilTahunTerlama_BerdasarkanKolomTahun(ByVal TahunDataBase As Integer, ByVal NamaTabel As String, ByVal NamaKolomTahun As String) As Integer
        Dim TahunTerlama As Integer
        BukaDatabaseTransaksi_Alternatif_Kondisional(TahunDataBase)
        Dim cmdKhusus = New OdbcCommand(" SELECT " & NamaKolomTahun & " FROM " & NamaTabel &
                              " WHERE " & NamaKolomTahun & " IN (SELECT MIN(" & NamaKolomTahun & ") " & NamaTabel & " ) " &
                              " ORDER BY " & NamaKolomTahun & " ASC ",
                              KoneksiDatabaseTransaksi_Alternatif)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            TahunTerlama = drKhusus.Item(NamaKolomTahun)
        Else
            TahunTerlama = TahunBukuAktif
        End If
        TutupDatabaseTransaksi_Alternatif_Kondisional()
        Return TahunTerlama
    End Function


    Public Sub PesanUmum(ByVal IsiPesan As String)
        MsgBox(IsiPesan)
    End Sub

    Public Sub PesanPeringatan(ByVal IsiPesan As String)
        MsgBox(IsiPesan)
    End Sub

    Public Sub PesanPemberitahuan(ByVal IsiPesan As String)
        MsgBox(IsiPesan)
    End Sub

    Public Sub PesanSukses(ByVal IsiPesan As String)
        MsgBox(IsiPesan)
    End Sub

    Public Sub PesanError(ByVal IsiPesan As String)
        MsgBox(IsiPesan)
    End Sub

    Public Sub PesanUntukProgrammer(ByVal IsiPesan As String)
        If LevelUserAktif >= LevelUser_81_TimIT Then
            MsgBox("Pesan untuk PROGRAMMER :" & Enter2Baris & IsiPesan)
        End If
    End Sub

    Public Sub PeringatanKeras()
        PesanUntukProgrammer("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter1Baris &
                             "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
    End Sub

    Public Sub PesanKhususPCDeveloper(ByVal IsiPesan As String)
        If ID_CPU = "BFEBFBFF000906A3" Then
            MsgBox("Pesan Khusus di PC Developer :" & Enter2Baris & IsiPesan)
        End If
    End Sub

    Sub BukaPengaturanCompanyProfile()
        win_Pengaturan = New wpfWin_Pengaturan
        win_Pengaturan.FungsiForm = "PENGATURAN"
        win_Pengaturan.tab_Pengaturan.SelectedIndex = 1
        win_Pengaturan.ShowDialog()
    End Sub

    Sub Notifikasi_AwalMasukTahunBuku()

        'Notifikasi Expire Sertifikat Elektronik (SE) :
        Dim SelisihHariExpireSE As Int64 = DateDiff("d", TanggalIni_Date, TanggalExpireSEPerusahaan)
        If SelisihHariExpireSE <= 0 Then
            PesanPeringatan("Tanggal Expire Sertifikat Elektronik sudah habis..!." & Enter2Baris &
                            "Segera perbarui..!")
            BukaPengaturanCompanyProfile()
        ElseIf SelisihHariExpireSE <= 15 Then
            PesanPemberitahuan("Tanggal Expire Sertifikat Elektronik tinggal " & SelisihHariExpireSE & " hari lagi." & Enter2Baris &
                               "Segera perbarui..!")
            BukaPengaturanCompanyProfile()
        End If

        'Notifikasi Expire Sertifikat Badan Usaha (SBU) :
        Dim SelisihHariExpireSBU As Int64 = DateDiff("d", TanggalIni_Date, TanggalExpireSBUPerusahaan)
        If SelisihHariExpireSBU <= 0 Then
            PesanPeringatan("Tanggal Expire Sertifikat Badan Usaha (SBU) sudah habis..!." & Enter2Baris &
                            "Segera perbarui..!")
            BukaPengaturanCompanyProfile()
        ElseIf SelisihHariExpireSBU <= 60 Then
            PesanPemberitahuan("Tanggal Expire Sertifikat Badan Usaha (SBU) tinggal " & SelisihHariExpireSBU & " hari lagi." & Enter2Baris &
                               "Segera perbarui..!")
            BukaPengaturanCompanyProfile()
        End If

    End Sub

    Sub KontenComboBulan_Public(ComboBulan As ComboBox)
        ComboBulan.Items.Clear()
        ComboBulan.Items.Add(Bulan_Januari)
        ComboBulan.Items.Add(Bulan_Februari)
        ComboBulan.Items.Add(Bulan_Maret)
        ComboBulan.Items.Add(Bulan_April)
        ComboBulan.Items.Add(Bulan_Mei)
        ComboBulan.Items.Add(Bulan_Juni)
        ComboBulan.Items.Add(Bulan_Juli)
        ComboBulan.Items.Add(Bulan_Agustus)
        ComboBulan.Items.Add(Bulan_September)
        ComboBulan.Items.Add(Bulan_Oktober)
        ComboBulan.Items.Add(Bulan_Nopember)
        ComboBulan.Items.Add(Bulan_Desember)
    End Sub


    Public NomorJV_SaatPengecekanJurnal As Int64
    Function AmbilValue_BulanTertuaAngka(TabelDanKriteria As String, Kolom As String) As Integer
        NomorJV_SaatPengecekanJurnal = 0
        Dim Tanggal_Date As Date
        Dim BulanTertuaAngka As Integer = 0
        AksesDatabase_Transaksi(Buka)
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nomor_JV, " & Kolom & " FROM " & TabelDanKriteria & " ORDER BY " & Kolom, KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            Tanggal_Date = drKHUSUS.Item(Kolom)
            BulanTertuaAngka = AmbilAngka(Tanggal_Date.Month)
            NomorJV_SaatPengecekanJurnal = drKHUSUS.Item("Nomor_JV")
        Loop
        AksesDatabase_Transaksi(Tutup)
        Return BulanTertuaAngka
    End Function

    Sub KontenComboBulanDibatasi_Public(ComboBulan As ComboBox, BatasBulanAngka As Integer)
        Dim BulanAngkaTelusur As Integer = 0
        ComboBulan.Items.Clear()
        If BatasBulanAngka = 0 Then Return
        If BatasBulanAngka > 12 Then BatasBulanAngka = 12
        Do While BulanAngkaTelusur < BatasBulanAngka
            BulanAngkaTelusur += 1
            ComboBulan.Items.Add(KonversiAngkaKeBulanString(BulanAngkaTelusur))
        Loop
    End Sub

    Function AmbilValueBoolean_JualAsset(NomorInvoice)
        Dim JualAsset As Boolean = False
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Asset FROM tbl_Penjualan_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then
            If drKHUSUS.Item("Asset") = 1 Then
                JualAsset = True
            Else
                JualAsset = False
            End If
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JualAsset
    End Function

    Function CekKeberadaanJurnal_DiBulanTertentu(COA As String, JenisJurnal As String, BulanAngka As Integer)
        If BulanAngka < 0 Or BulanAngka > 12 Then
            PesanUntukProgrammer("Angka bulan salah...!!!" & Enter2Baris &
                                 "(Angka : " & BulanAngka & ")")
            Return False
        End If
        BukaDatabaseTransaksi_Kondisional()
        Dim AdaJurnal As Boolean
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                              " WHERE   Valid                                  <> '" & _X_ & "' " &
                              " AND     COA                                     = '" & COA & "' " &
                              " AND     Jenis_Jurnal                            = '" & JenisJurnal & "' " &
                              " AND     DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" &
                              KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then
            AdaJurnal = True
            NomorJV_SaatPengecekanJurnal = drKHUSUS.Item("Nomor_JV")
        Else
            AdaJurnal = False
            NomorJV_SaatPengecekanJurnal = 0
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return AdaJurnal
    End Function

    Function CekKeberadaanJurnal_DiTanggalTertentu(COA As String, JenisJurnal As String, Tanggal As Date)
        BukaDatabaseTransaksi_Kondisional()
        Dim AdaJurnal As Boolean
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                              " WHERE   Valid                                      <> '" & _X_ & "' " &
                              " AND     COA                                         = '" & COA & "' " &
                              " AND     Jenis_Jurnal                                = '" & JenisJurnal & "' " &
                              " AND     DATE_FORMAT(Tanggal_Transaksi, '%Y-%m-%d')  = '" & TanggalFormatSimpan(Tanggal) & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then
            AdaJurnal = True
            NomorJV_SaatPengecekanJurnal = drKHUSUS.Item("Nomor_JV")
        Else
            AdaJurnal = False
            NomorJV_SaatPengecekanJurnal = 0
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return AdaJurnal
    End Function

    Function AmbilValue_MutasiCOA_PadaJurnal(COA As String, NomorJV As Int64) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim JumlahMutasi As Int64 = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid      <> '" & _X_ & "' " &
                                        " AND   Nomor_JV    = '" & NomorJV & "' " &
                                        " AND   COA         = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then
            Dim JumlahDebet = AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Debet"))
            Dim JumlahKredit = AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Kredit"))
            JumlahMutasi = JumlahDebet - JumlahKredit
        End If
        TutupDatabaseTransaksi_Kondisional()
        If JumlahMutasi < 0 Then JumlahMutasi = 0 - JumlahMutasi
        Return JumlahMutasi
    End Function

    Function JumlahDebet_PadaSuatuJurnal(COA As String, NomorJV As Int64) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim JumlahDebet As Int64 = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid      <> '" & _X_ & "' " &
                                        " AND   Nomor_JV    = '" & NomorJV & "' " &
                                        " AND COA           = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then JumlahDebet = AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Debet"))
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahDebet
    End Function

    Function JumlahDebet_PadaSuatuJurnal_MUA(COA As String, NomorJV As Int64) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim JumlahDebet As Decimal = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid          <> '" & _X_ & "' " &
                                        " AND   Nomor_JV        = '" & NomorJV & "' " &
                                        " AND Kode_Mata_Uang   <> '" & KodeMataUang_IDR & "' " &
                                        " AND COA               = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then JumlahDebet = drKHUSUS.Item("Jumlah_Debet")
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahDebet
    End Function

    Function JumlahKredit_PadaSuatuJurnal(COA As String, NomorJV As Int64) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim JumlahKredit As Int64 = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid          <> '" & _X_ & "' " &
                                        " AND   Nomor_JV        = '" & NomorJV & "' " &
                                        " AND   COA             = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then JumlahKredit = AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Kredit"))
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahKredit
    End Function

    Function JumlahKredit_PadaSuatuJurnal_MUA(COA As String, NomorJV As Int64) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim JumlahKredit As Decimal = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid          <> '" & _X_ & "' " &
                                        " AND   Nomor_JV        = '" & NomorJV & "' " &
                                        " AND   Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' " &
                                        " AND   COA             = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        drKHUSUS.Read()
        If drKHUSUS.HasRows Then JumlahKredit = drKHUSUS.Item("Jumlah_Kredit")
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahKredit
    End Function

    Function TotalDebetCOA_BulanTertentu(COA As String, BulanAngka As Integer) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Int64 = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid                                  <> '" & _X_ & "' " &
                                        " AND   COA                                     = '" & COA & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" &
                                        KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalDebet += AmbilValue_NilaiMataUang(drKhusus.Item("Kode_Mata_Uang"), drKhusus.Item("Kurs"), drKhusus.Item("Jumlah_Debet"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalDebet
    End Function

    Function TotalDebetCOA_BulanTertentu_MUA(COA, BulanAngka) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Decimal = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid                                  <> '" & _X_ & "' " &
                                        " AND   COA                                     = '" & COA & "' " &
                                        " AND   Kode_Mata_Uang                         <> '" & KodeMataUang_IDR & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" &
                                        KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalDebet += drKhusus.Item("Jumlah_Debet")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalDebet
    End Function

    Function TotalKreditCOA_BulanTertentu(COA, BulanAngka) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalKredit As Int64 = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid                                  <> '" & _X_ & "' " &
                                        " AND   COA                                     = '" & COA & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" &
                                        KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalKredit += AmbilValue_NilaiMataUang(drKhusus.Item("Kode_Mata_Uang"), drKhusus.Item("Kurs"), drKhusus.Item("Jumlah_Kredit"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalKredit
    End Function

    Function TotalKreditCOA_BulanTertentu_MUA(COA, BulanAngka) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalKredit As Decimal = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid                                  <> '" & _X_ & "' " &
                                        " AND   COA                                     = '" & COA & "' " &
                                        " AND   Kode_Mata_Uang                         <> '" & KodeMataUang_IDR & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" &
                                        KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalKredit += drKhusus.Item("Jumlah_Kredit")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalKredit
    End Function

    Function TotalDebetCOA(COA) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Int64 = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid  <> '" & _X_ & "' " &
                                        " AND   COA     = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalDebet += AmbilValue_NilaiMataUang(drKhusus.Item("Kode_Mata_Uang"), drKhusus.Item("Kurs"), drKhusus.Item("Jumlah_Debet"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalDebet
    End Function

    Function TotalDebetCOA_MUA(COA) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Decimal = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Jumlah_Debet FROM tbl_Transaksi " &
                                        " WHERE Valid          <> '" & _X_ & "' " &
                                        " AND   COA             = '" & COA & "' " &
                                        " AND   Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' ",
                                        KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalDebet += drKhusus.Item("Jumlah_Debet")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalDebet
    End Function

    Function TotalKreditCOA(COA) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalKredit As Int64 = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid  <> '" & _X_ & "' " &
                                        " AND   COA     = '" & COA & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalKredit += AmbilValue_NilaiMataUang(drKhusus.Item("Kode_Mata_Uang"), drKhusus.Item("Kurs"), drKhusus.Item("Jumlah_Kredit"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalKredit
    End Function

    Function TotalKreditCOA_MUA(COA) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalKredit As Decimal = 0
        Dim cmdKhusus = New OdbcCommand(" SELECT Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid          <> '" & _X_ & "' " &
                                        " AND   COA             = '" & COA & "' " &
                                        " AND   Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' ",
                                        KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            TotalKredit += drKhusus.Item("Jumlah_Kredit")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TotalKredit
    End Function


    'AMBIL VALUE SALDO AWAL BULAN COA :
    Public Function SaldoAwalBulanCOA(ByVal COA As String, BulanAngka As Integer) As Int64
        Dim KodeMataUang As String
        Dim Kurs As Decimal
        Dim SaldoAwalBulan As Int64
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            If BulanAngka = 1 Then
                cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Saldo_Awal " &
                                            " FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            Else
                cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Saldo_" & KonversiAngkaKeBulanString(BulanAngka - 1) &
                                            " FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            End If
            drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                KodeMataUang = drKhusus.Item("Kode_Mata_Uang")
                If KodeMataUang = KodeMataUang_IDR Then
                    Kurs = 1
                Else
                    Kurs = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                End If
                If BulanAngka = 1 Then
                    SaldoAwalBulan = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, drKhusus.Item("Saldo_Awal"))
                Else
                    SaldoAwalBulan = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, drKhusus.Item("Saldo_" & KonversiAngkaKeBulanString(BulanAngka - 1)))
                End If
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SaldoAwalBulan
    End Function



    'AMBIL VALUE SALDO AKHIR BULAN COA :
    Public Function SaldoAkhirBulanCOA(ByVal COA As String, BulanAngka As Integer) As Int64
        Dim KodeMataUang As String
        Dim Kurs As Decimal
        Dim SaldoAkhirBulan As Int64
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Saldo_" & KonversiAngkaKeBulanString(BulanAngka) &
                                        " FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                KodeMataUang = drKhusus.Item("Kode_Mata_Uang")
                If KodeMataUang = KodeMataUang_IDR Then
                    Kurs = 1
                Else
                    Kurs = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                End If
                SaldoAkhirBulan = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, drKhusus.Item("Saldo_" & KonversiAngkaKeBulanString(BulanAngka)))
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SaldoAkhirBulan
    End Function


    'AMBIL VALUE SALDO AWAL TAHUN COA :
    Public Function SaldoAwalTahunCOA(ByVal COA As String) As Int64
        Dim KodeMataUang As String
        Dim Kurs As Decimal
        Dim SaldoAwalTahun As Int64
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Saldo_Awal FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                KodeMataUang = drKhusus.Item("Kode_Mata_Uang")
                If KodeMataUang = KodeMataUang_IDR Then
                    Kurs = 1
                Else
                    Kurs = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                End If
                SaldoAwalTahun = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, drKhusus.Item("Saldo_Awal"))
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SaldoAwalTahun
    End Function

    Public Function KursTengahBI_AkhirTahunIni(KodeMataUang As String) As Decimal
        Dim Kurs As Decimal = 0
        If KodeMataUang = KodeMataUang_IDR Then Return 1
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        cmdKhusus = New OdbcCommand(" SELECT Desember FROM tbl_KursAkhirBulan " &
                                    " WHERE Kode_Mata_Uang = '" & KodeMataUang & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            Kurs = drKhusus.Item("Desember")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return Kurs
    End Function


    Public Function KursTengahBI_AkhirTahunLalu(KodeMataUang As String) As Decimal
        Dim Kurs As Decimal = 0
        If KodeMataUang = KodeMataUang_IDR Then Return 1
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        cmdKhusus = New OdbcCommand(" SELECT Akhir_Tahun_Lalu FROM tbl_KursAkhirBulan " &
                                    " WHERE Kode_Mata_Uang = '" & KodeMataUang & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            Kurs = drKhusus.Item("Akhir_Tahun_Lalu")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return Kurs
    End Function

    Public Function KursTengahBI_AkhirBulan(KodeMataUang As String, BulanAngka As Integer) As Decimal
        Dim Kurs As Decimal = 0
        If KodeMataUang = KodeMataUang_IDR Then Return 1
        Dim BulanTerbilang As String = KonversiAngkaKeBulanString(BulanAngka)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        cmdKhusus = New OdbcCommand(" SELECT " & BulanTerbilang & " FROM tbl_KursAkhirBulan " &
                                    " WHERE Kode_Mata_Uang = '" & KodeMataUang & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            Kurs = drKhusus.Item(BulanTerbilang)
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return Kurs
    End Function

    'AMBIL VALUE SALDO AWAL TAHUN COA :
    Public Function SaldoAwalTahunCOA_MUA(ByVal COA As String) As Decimal
        Dim SaldoAwalTahun As Decimal
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKhusus = New OdbcCommand(" SELECT Saldo_Awal FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                SaldoAwalTahun = drKhusus.Item("Saldo_Awal")
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SaldoAwalTahun
    End Function

    'AMBIL VALUE SALDO AKHIR COA :
    Public Function SaldoAkhirCOA(ByVal COA As String) As Int64
        Dim Saldo As Int64 = SaldoAwalTahunCOA(COA) + TotalDebetCOA(COA) - TotalKreditCOA(COA)
        If COATermasukKREDIT(COA) = True Then Saldo = -Saldo
        Return Saldo
    End Function

    'AMBIL VALUE SALDO AKHIR TAHUN BUKU LAMPAU COA :
    Public Function SaldoAkhirTahunBukuLampauCOA(ByVal COA As String) As Int64
        Dim KodeMataUang As String
        Dim Kurs As Decimal
        Dim SaldoAwalTahunBukuLampau As Int64
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        If COA <> Kosongan Then
            BukaDatabaseGeneral_Kondisional()
            cmdKhusus = New OdbcCommand(" SELECT Kode_Mata_Uang, Saldo_Awal FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drKhusus = cmdKhusus.ExecuteReader
            drKhusus.Read()
            If drKhusus.HasRows Then
                KodeMataUang = drKhusus.Item("Kode_Mata_Uang")
                If KodeMataUang = KodeMataUang_IDR Then
                    Kurs = 1
                Else
                    Kurs = KursTengahBI_AkhirTahunIni(KodeMataUang)
                End If
                SaldoAwalTahunBukuLampau = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, drKhusus.Item("Saldo_Awal"))
            Else
                PesanUntukProgrammer("Akun tidak terdaftar...!!!")
            End If
            TutupDatabaseGeneral_Kondisional()
        End If
        Return SaldoAwalTahunBukuLampau
    End Function

    'AMBIL VALUE SALDO AKHIR COA SAMPAI AKHIR BULAN TERTENTU:
    Public Function SaldoAkhirCOA_SampaiAkhirBulanTertentu(ByVal COA As String, BulanAngka As Integer) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Int64 = 0
        Dim TotalKredit As Int64 = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid                                   <> '" & _X_ & "' " &
                                        " AND   COA                                      = '" & COA & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') <= '" & TahunBukuAktif & "-" &
                                        KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            TotalDebet += AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Debet"))
            TotalKredit += AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Kredit"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Dim Saldo As Int64 = SaldoAwalTahunCOA(COA) + TotalDebet - TotalKredit
        If COATermasukKREDIT(COA) = True Then Saldo = -Saldo
        Return Saldo
    End Function

    'AMBIL VALUE SALDO AKHIR COA SAMPAI AKHIR BULAN TERTENTU - TANPA KURS:
    Public Function SaldoAkhirCOA_SampaiAkhirBulanTertentu_MUA(ByVal COA As String, BulanAngka As Integer) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Decimal = 0
        Dim TotalKredit As Decimal = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid                                   <> '" & _X_ & "' " &
                                        " AND   COA                                      = '" & COA & "' " &
                                        " AND   Kode_Mata_Uang                          <> '" & KodeMataUang_IDR & "' " &
                                        " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') <= '" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(BulanAngka) & "' ",
                                        KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            'PesanUntukProgrammer("Total Debet : " & TotalDebet & Enter2Baris &
            '    "Total Kredit : " & TotalKredit)
            TotalDebet += drKHUSUS.Item("Jumlah_Debet")
            TotalKredit += drKHUSUS.Item("Jumlah_Kredit")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Dim Saldo As Decimal = SaldoAwalTahunCOA_MUA(COA) + TotalDebet - TotalKredit
        If COATermasukKREDIT(COA) = True Then Saldo = -Saldo
        Return Saldo
    End Function

    'AMBIL VALUE SALDO AKHIR COA SAMPAI TANGGAL TERTENTU :
    Public Function SaldoAkhirCOA_SampaiTanggalTertentu(ByVal COA As String, Tanggal As Date) As Int64
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Int64 = 0
        Dim TotalKredit As Int64 = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid              <> '" & _X_ & "' " &
                                        " AND   COA                 = '" & COA & "' " &
                                        " AND   Tanggal_Transaksi  <= '" & TanggalFormatSimpan(Tanggal) & "' " &
                                        " ORDER BY Tanggal_Transaksi ASC ",
                                        KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            TotalDebet += AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Debet"))
            TotalKredit += AmbilValue_NilaiMataUang(drKHUSUS.Item("Kode_Mata_Uang"), drKHUSUS.Item("Kurs"), drKHUSUS.Item("Jumlah_Kredit"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Dim Saldo As Int64 = SaldoAwalTahunCOA(COA) + TotalDebet - TotalKredit
        If COATermasukKREDIT(COA) = True Then Saldo = -Saldo
        Return Saldo
    End Function

    'AMBIL VALUE SALDO AKHIR COA SAMPAI TANGGAL TERTENTU - MUA (TANPA KURS):
    Public Function SaldoAkhirCOA_SampaiTanggalTertentu_MUA(ByVal COA As String, Tanggal As Date) As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim TotalDebet As Decimal = 0
        Dim TotalKredit As Decimal = 0
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi " &
                                        " WHERE Valid           <> '" & _X_ & "' " &
                                        " AND   COA              = '" & COA & "' " &
                                        " AND Kode_Mata_Uang    <> '" & KodeMataUang_IDR & "' " &
                                        " AND Tanggal_Transaksi <= '" & TanggalFormatSimpan(Tanggal) & "' " &
                                        " ORDER BY Tanggal_Transaksi ASC ",
                                        KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            'PesanUntukProgrammer("Total Debet : " & TotalDebet & Enter2Baris &
            '    "Total Kredit : " & TotalKredit)
            TotalDebet += drKHUSUS.Item("Jumlah_Debet")
            TotalKredit += drKHUSUS.Item("Jumlah_Kredit")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Dim Saldo As Decimal = SaldoAwalTahunCOA_MUA(COA) + TotalDebet - TotalKredit
        If COATermasukKREDIT(COA) = True Then Saldo = -Saldo
        Return Saldo
    End Function

    'TANGGAL TERAKHIR TRANSAKSI SUATU AKUN (COA)"
    Public Function TanggalTerakhirTransaksiCOA(COA As String, Tanggal As Date) As Date
        Dim TanggalTerakhirTransaksi As Date
        BukaDatabaseTransaksi_Kondisional()
        Dim Kueri As String = " SELECT COA, Tanggal_Transaksi FROM tbl_Transaksi " &
            " WHERE Valid             <> '" & _X_ & "' " &
            " AND   COA                = '" & COA & "' " &
            " AND   Tanggal_Transaksi <= '" & TanggalFormatSimpan(Tanggal) & "' " &
            " ORDER BY Tanggal_Transaksi ASC "
        Dim cmdKHUSUS = New OdbcCommand(Kueri, KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Dim JumlahLoop = 0
        Do While drKHUSUS.Read
            TanggalTerakhirTransaksi = drKHUSUS.Item("Tanggal_Transaksi")
            JumlahLoop += 1
        Loop
        TutupDatabaseTransaksi_Kondisional()
        If JumlahLoop = 0 Then TanggalTerakhirTransaksi = TanggalKosong
        Return TanggalTerakhirTransaksi
    End Function



    'TANGGAL TERAKHIR TRANSAKSI SUATU AKUN (COA) - DI BULAN TERTENTU ATAU SEBELUMNYA "
    Public Function TanggalTerakhirTransaksiCOA_DiBulanTertentuaAtauSebelumnya(COA As String, BulanAngka As Integer) As Date
        Dim TanggalTerakhirTransaksi As Date
        BukaDatabaseTransaksi_Kondisional()
        Dim Kueri As String = " SELECT COA, Tanggal_Transaksi FROM tbl_Transaksi " &
            " WHERE Valid                                   <> '" & _X_ & "' " &
            " AND   COA                                      = '" & COA & "' " &
            " AND   DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') <= '" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(BulanAngka) & "' " &
            " ORDER BY Tanggal_Transaksi ASC "
        Dim cmdKHUSUS = New OdbcCommand(Kueri, KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Dim JumlahLoop = 0
        Do While drKHUSUS.Read
            TanggalTerakhirTransaksi = drKHUSUS.Item("Tanggal_Transaksi")
            JumlahLoop += 1
        Loop
        TutupDatabaseTransaksi_Kondisional()
        If JumlahLoop = 0 Then TanggalTerakhirTransaksi = TanggalKosong
        Return TanggalTerakhirTransaksi
    End Function

    'BUANG JURNAL BERDASARKAN NOMOR JV :
    Public Sub BuangJurnal_BerdasarkanNomorJV(NomorJV)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" UPDATE tbl_transaksi SET Valid = '" & _X_ & "' " &
                                        " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS JURNAL BERDASARKAN NOMOR JV :
    Public Sub HapusJurnal_BerdasarkanNomorJV(NomorJV As Integer)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_transaksi " &
                                        " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS JURNAL ADJUSMENT BULAN TERTENTU :
    Public Sub HapusJurnalAdjusmentBulanTertentu(JurnalAdjusment As String, BulanAngka As Integer)
        Dim TanggalAkhirBulan As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_transaksi " &
                                        " WHERE Jenis_Jurnal = '" & JurnalAdjusment & "' " &
                                        " AND Tanggal_Transaksi = '" & TanggalFormatSimpan(TanggalAkhirBulan) & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub


    'HAPUS DATA ASSET BERDASARKAN NOMOR PEMBELIAN :
    Public Sub HapusDataAsset_BerdasarkanNomorPembelian(NomorPembelian)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_DataAsset WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub

    'HAPUS DATA ASSET BERDASARKAN NOMOR ID :
    Public Sub HapusDataAsset_BerdasarkanNomorID(NomorID)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_DataAsset WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub

    'HAPUS DATA TABEL BERDASARKAN NOMOR ID, untuk dbTransaksi :
    Public Sub HapusDataTabel_BerdasarkanNomorID_dbTransaksi(Tabel As String, NomorID As Int64)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM " & Tabel & " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA TABEL BERDASARKAN NOMOR ID, untuk dbGeneral :
    Public Sub HapusDataTabel_BerdasarkanNomorID_dbGeneral(Tabel As String, NomorID As Int64)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM " & Tabel & " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub


    'HAPUS DATA TABEL BERDASARKAN NOMOR JV, untuk dbTransaksi :
    Public Sub HapusDataTabel_BerdasarkanNomorJV_dbTransaksi(Tabel As String, NomorJV As Int64)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM " & Tabel & " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA TABEL BERDASARKAN NOMOR JV, untuk dbGeneral :
    Public Sub HapusDataTabel_BerdasarkanNomorJV_dbGeneral(Tabel As String, NomorJV As Int64)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM " & Tabel & " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub

    'HAPUS DATA AMORTISASI BERDASARKAN NOMOR PEMBELIAN :
    Public Sub HapusDataAmortisasi_BerdasarkanNomorPembelian(NomorPembelian)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_AmortisasiBiaya WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub

    'HAPUS DATA AMORTISASI BERDASARKAN NOMOR ID :
    Public Sub HapusDataAmortisasi_BerdasarkanNomorID(NomorID)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_AmortisasiBiaya WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub

    'HAPUS DATA PEMBELIAN BERDASARKAN NOMOR PEMBELIAN:
    Public Sub HapusDataPembelian_BerdasarkanNomorPembelian(NomorPembelian)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA PEMBELIAN BERDASARKAN NOMOR INVOICE:
    Public Sub HapusDataPembelian_BerdasarkanNomorInvoice(NomorInvoice)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA PEMBELIAN BERDASARKAN ANGKA INVOICE:
    Public Sub HapusDataPembelian_BerdasarkanAngkaInvoice(AngkaInvoice)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice WHERE Angka_Invoice = '" & AngkaInvoice & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub


    'HAPUS DATA PENGELUARAN BERDASARKAN NOMOR JV:
    Public Sub HapusDataPengeluaran_BerdasarkanNomorID(NomorID)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_BuktiPengeluaran WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub


    'HAPUS DATA PENJUALAN BERDASARKAN NOMOR PEnjualAN:
    Public Sub HapusDataPenjualan_BerdasarkanNomorPenjualan(NomorPenjualan)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice WHERE Nomor_Penjualan = '" & NomorPenjualan & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA PENJUALAN BERDASARKAN NOMOR INVOICE:
    Public Sub HapusDataPenjualan_BerdasarkanNomorInvoice(NomorInvoice)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    'HAPUS DATA PENJUALAN BERDASARKAN ANGKA INVOICE:
    Public Sub HapusDataPenjualan_BerdasarkanAngkaInvoice(AngkaInvoice)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice WHERE Angka_Invoice = '" & AngkaInvoice & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub


    'EDIT NOMOR JV PADA INVOICE PEMBELIAN:
    Public Sub EditNomorJV_PadaInvoicePembelian(NomorInvoice As String, NomorJV As Int64)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" UPDATE    tbl_Pembelian_Invoice " &
                                        " SET       Nomor_JV        = '" & NomorJV & "' " &
                                        " WHERE     Nomor_Invoice   = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Try
            cmdKhusus.Transaction = Transaction_Transaksi
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub


    Public Function CekDataPengajuanBerdasarkanNomorBP(NomorBP) As Boolean
        Dim Adapengajuan As Boolean
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                        " WHERE Nomor_BP = '" & NomorBP & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKhusus.ExecuteReader
        If drKhusus.HasRows Then
            Adapengajuan = True
        Else
            Adapengajuan = False
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return Adapengajuan
    End Function


    Public Function AmbilValue_TanggalPOBerdasarkanNomorPOPembelian(NomorPO As String) As String
        Dim TanggalPO = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Tanggal_PO FROM tbl_Pembelian_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then TanggalPO = drKhusus.Item("Tanggal_PO")
        TutupDatabaseTransaksi_Kondisional()
        Return TanggalPO
    End Function


    Public Function AmbilValue_TanggalPOBerdasarkanNomorPOPenjualan(NomorPO As String) As String
        Dim TanggalPO = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Tanggal_PO FROM tbl_Penjualan_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then TanggalPO = drKhusus.Item("Tanggal_PO")
        TutupDatabaseTransaksi_Kondisional()
        Return TanggalPO
    End Function



    'HAPUS DATA ASSET BERDASARKAN NOMOR ID :
    Public Sub TampilkanPaksaCOA(COA As String)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" UPDATE tbl_COA SET Visibilitas = '" & Pilihan_Ya & "' WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        Try
            cmdKhusus.Transaction = Transaction_General
            cmdKhusus.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseGeneral_Kondisional()
    End Sub


    Sub Perhitungan_ValueBank_Public(ByVal KodeMataUang As String,
                                     ByVal AlurTransaksi As String,
                                     ByVal JumlahMutasiBankCash As Decimal,
                                     ByRef JumlahTransfer As Decimal,
                                     ByVal BiayaAdministrasiBank As Decimal,
                                     ByRef TotalBank As Decimal,
                                     ByVal DitanggungOleh As String) '(Perhatikan ByVal dan ByRef..! Jangan dirubah ya..!

        If AlurTransaksi = AlurTransaksi_OUT Then
            Select Case DitanggungOleh
                Case DitanggungOleh_LawanTransaksi
                    JumlahTransfer = JumlahMutasiBankCash - BiayaAdministrasiBank
                    TotalBank = JumlahMutasiBankCash
                Case DitanggungOleh_Perusahaan
                    JumlahTransfer = JumlahMutasiBankCash
                    TotalBank = JumlahTransfer + BiayaAdministrasiBank
                Case Else 'Kalau algoritma Bank sudah sehat, Case Else harus dihapus...!!!
                    JumlahTransfer = JumlahMutasiBankCash
                    TotalBank = JumlahMutasiBankCash
            End Select
        End If

        If AlurTransaksi = AlurTransaksi_IN Then
            Select Case DitanggungOleh
                Case DitanggungOleh_LawanTransaksi
                    JumlahTransfer = JumlahMutasiBankCash
                Case DitanggungOleh_Perusahaan
                    JumlahTransfer = JumlahMutasiBankCash - BiayaAdministrasiBank
                Case Else 'Kalau algoritma Bank sudah sehat, Case Else harus dihapus...!!!
                    JumlahTransfer = JumlahMutasiBankCash
            End Select
            TotalBank = JumlahTransfer
        End If

        If KodeMataUang = KodeMataUang_IDR Then
            FormatUlangAngkaKeBilanganBulat(JumlahMutasiBankCash)
            FormatUlangAngkaKeBilanganBulat(JumlahTransfer)
            FormatUlangAngkaKeBilanganBulat(BiayaAdministrasiBank)
            FormatUlangAngkaKeBilanganBulat(TotalBank)
        Else
            FormatUlangAngkaKeBilanganDesimal(JumlahMutasiBankCash)
            FormatUlangAngkaKeBilanganDesimal(JumlahTransfer)
            FormatUlangAngkaKeBilanganDesimal(BiayaAdministrasiBank)
            FormatUlangAngkaKeBilanganDesimal(TotalBank)
        End If

    End Sub


    Public Function KonversiNomorPembelianKeNomorInvoice(NomorPembelian)
        Dim NomorInvoice = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Invoice FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            NomorInvoice = drKhusus.Item("Nomor_Invoice")
        Else
            PesanUntukProgrammer("Nomor Pembelian " & NomorPembelian & " tidak terdaftar..!")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return NomorInvoice
    End Function

    Public Function KonversiNomorInvoiceKeNomorPembelian(NomorInvoice)
        Dim NomorPembelian = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Pembelian FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            NomorPembelian = drKhusus.Item("Nomor_Pembelian")
        Else
            PesanUntukProgrammer("Nomor Invoice " & NomorInvoice & " tidak terdaftar..!")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return NomorPembelian
    End Function

    Public Function KonversiNomorPenjualanKeNomorInvoice(NomorPenjualan)
        Dim NomorInvoice = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Invoice FROM tbl_Penjualan_Invoice " &
                                        " WHERE Nomor_Penjualan = '" & NomorPenjualan & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            NomorInvoice = drKhusus.Item("Nomor_Invoice")
        Else
            PesanUntukProgrammer("Nomor Penjualan " & NomorPenjualan & " tidak terdaftar..!")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return NomorInvoice
    End Function

    Public Function KonversiNomorInvoiceKeNomorPenjualan(NomorInvoice)
        Dim NomorPenjualan = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Penjualan FROM tbl_Penjualan_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            NomorPenjualan = drKhusus.Item("Nomor_Penjualan")
        Else
            PesanUntukProgrammer("Nomor Invoice " & NomorInvoice & " tidak terdaftar..!")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return NomorPenjualan
    End Function

    Public Function KonversiNomorPembelianKeNomorBPHU(NomorPembelian)
        If NomorPembelian Is Nothing OrElse NomorPembelian Is DBNull.Value Then
            Return Kosongan
        End If
        Dim s As String = Convert.ToString(NomorPembelian) 'aman untuk banyak tipe
        If String.IsNullOrWhiteSpace(s) Then Return Kosongan
        Return GantiTeks(NomorPembelian, AwalanPEMB, AwalanBPHU)
    End Function

    Public Function KonversiNomorBPHUKeNomorPembelian(NomorBPHU)
        Return GantiTeks(NomorBPHU, AwalanBPHU, AwalanPEMB)
    End Function

    Public Function KonversiNomorPenjualanKeNomorBPPU(NomorPenjualan)
        Return GantiTeks(NomorPenjualan, AwalanPENJ, AwalanBPPU)
    End Function

    Public Function KonversiNomorBPPUKeNomorPenjualan(NomorBPPU)
        Return GantiTeks(NomorBPPU, AwalanBPPU, AwalanPENJ)
    End Function

    Public Function AmbilValue_NomorKKBerdasarkanNomorBP(NomorBP As String)
        Dim NomorKK = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_KK FROM tbl_BuktiPengeluaran " &
                                        " WHERE Nomor_BP = '" & NomorBP & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then NomorKK = drKhusus.Item("Nomor_KK")
        TutupDatabaseTransaksi_Kondisional()
        Return NomorKK
    End Function


    'AMBIL VALUE NOMOR INVOICE BERDASARKAN NOMOR PEMBELIAN
    Public Function AmbilValue_NomorInvoiceBerdasarkanNomorPembelian(NomorPembelian)
        Dim NomorInvoice As String = 0
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Invoice FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            NomorInvoice = drKhusus.Item("Nomor_Invoice")
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return NomorInvoice
    End Function

    'AMBIL VALUE TANGGAL INVOICE BERDASARKAN NOMOR PEMBELIAN
    Public Function AmbilValue_TanggalInvoiceBerdasarkanNomorPembelian(NomorPembelian)
        Dim TanggalInvoice As String = 0
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Tanggal_Invoice FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            TanggalInvoice = TanggalFormatTampilan(drKhusus.Item("Tanggal_Invoice"))
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return TanggalInvoice
    End Function

    'AMBIL VALUE ID AMORTISASI BERDASARKAN NOMOR PEMBELIAN
    Public Function AmbilValue_IdAmortisasiBerdasarkanNomorPembelianDanNamaProduk(NomorPembelian, NamaProduk)
        Dim IdAmortisasi = 0
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT Nomor_ID FROM tbl_AmortisasiBiaya " &
                                        " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                        " AND Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            IdAmortisasi = drKhusus.Item("Nomor_ID")
        End If
        TutupDatabaseGeneral_Kondisional()
        Return IdAmortisasi
    End Function



    'AMBIL VALUE ID AMORTISASI BERDASARKAN NOMOR PEMBELIAN
    Public Function AmbilValue_TanggalMulaiAmortisasiBerdasarkanNomorPembelianDanNamaProduk(NomorPembelian, NamaProduk)
        Dim TanggalMulai As String = TanggalKosong
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT Tanggal_Mulai FROM tbl_AmortisasiBiaya " &
                                        " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                        " AND Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            TanggalMulai = TanggalFormatTampilan(drKhusus.Item("Tanggal_Mulai"))
        End If
        TutupDatabaseGeneral_Kondisional()
        Return TanggalMulai
    End Function


    Sub AmbilValue_JenisWP_dan_LokasiWP(ByVal KodeLawanTransaksi, ByRef JenisWP, ByRef LokasiWP)
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            JenisWP = drKhusus.Item("Jenis_WP")
            LokasiWP = drKhusus.Item("Lokasi_WP")
        End If
        TutupDatabaseGeneral_Kondisional()
    End Sub

    Sub SusunUlangNomorID_DataAsset()
        Dim NomorID_Lama = 0
        Dim KodeAsset_Lama = Kosongan
        Dim NomorID_Baru = 0
        Dim KodeAsset_Baru = Kosongan
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT Nomor_ID, Kode_Asset FROM tbl_DataAsset ORDER BY Nomor_ID ", KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            NomorID_Lama = drKhusus.Item("Nomor_ID")
            KodeAsset_Lama = drKhusus.Item("Kode_Asset")
            NomorID_Baru += 1
            KodeAsset_Baru = AmbilTeksKiri(KodeAsset_Lama, PanjangTeks(KodeAsset_Lama) - PanjangTeks(NomorID_Lama)) & NomorID_Baru
            cmdEDIT = New OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                      " Nomor_ID        = '" & NomorID_Baru & "', " &
                                      " Kode_Asset      = '" & KodeAsset_Baru & "' " &
                                      " WHERE Nomor_ID  = '" & NomorID_Lama & "' ", KoneksiDatabaseGeneral)
            cmdEDIT_ExecuteNonQuery()
        Loop
        TutupDatabaseGeneral_Kondisional()
    End Sub



    Function AmbilTeksKanan(Teks As String, JumlahKarakter As Integer) As String
        Return Right(Teks, JumlahKarakter).ToString
    End Function

    Function AmbilTeksTengah(Teks As String, Posisi As Integer, JumlahKarakter As Integer) As String
        Return Mid(Teks, Posisi, JumlahKarakter).ToString
    End Function

    Function AmbilTeksTengahTakTerbatas(Teks As String, Posisi As Integer) As String
        Return Mid(Teks, Posisi).ToString
    End Function

    Function AmbilTeksKiri(Teks As String, JumlahKarakter As Integer) As String
        Return Left(Teks, JumlahKarakter).ToString
    End Function

    Function GantiTeks(Teks As String, TeksYangDiganti As String, TeksPengganti As String) As String
        If Teks = Nothing Then Return Kosongan
        Teks = If(Teks, "")
        TeksYangDiganti = If(TeksYangDiganti, "")
        TeksPengganti = If(TeksPengganti, "")
        If TeksYangDiganti = "" Then Return Teks 'hindari Replace dengan pola kosong
        Return Replace(Teks, TeksYangDiganti, TeksPengganti).ToString
    End Function

    Function PanjangTeks(Teks As String)
        Return Len(Teks)
    End Function

    Function TampilanBundelan(Teks As String) As String
        Dim TeksBundel As String = Kosongan
        Dim PanjangTeks As Integer = InStr(Teks, SlashGanda_Pemisah)
        If PanjangTeks > 0 Then
            TeksBundel = Left(Teks, PanjangTeks) & " ++"
        Else
            TeksBundel = Teks
        End If
        Return TeksBundel
    End Function

    Sub BelumAdaCoding(Pesan As String)
        PesanUntukProgrammer("Belum ada Coding untuk " & Pesan & "..!!!")
    End Sub

    Function PenghapusEnter(TeksMentah As String)
        Dim TeksBaru As String = TeksMentah
        TeksBaru = Replace(TeksBaru, Chr(13) & Chr(10), " ")
        TeksBaru = Replace(TeksBaru, Chr(13), " ")
        TeksBaru = Replace(TeksBaru, Chr(10), " ")
        If TeksBaru Is Nothing Then TeksBaru = Kosongan
        Return TeksBaru
    End Function


    Sub EksporDataGridViewKeCSV(datagridBahanEkspor As DataGridView)
        frm_ProgressExport_CSV.BahanExport = datagridBahanEkspor
        frm_ProgressExport_CSV.ShowDialog()
    End Sub


    Function TahunBukuSudahStabil(TahunBuku) As Boolean
        Dim SudahStabil As Boolean
        If TahunBuku >= TahunCutOff + 2 Then SudahStabil = True
        Return SudahStabil
    End Function

    Sub BypassUpdater()
        win_Updater = New wpfWin_Updater
        win_Updater.ShowDialog()
        If ProsesUpdate_Aplikasi = False Then
            PesanPeringatan("Ups..! Prose update gagal..!" & Enter2Baris &
                            "Aplikasi tetap dijalankan dengan versi yang belum diperbarui.")
            HapusFolder(FolderTempUpdater)
        End If
    End Sub



    Sub RefreshTampilanDataAsset()
        If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.RefreshTampilanData()
    End Sub

    Sub RefreshTampilanAmortisasiBiaya()
        If usc_DaftarAmortisasiBiaya.StatusAktif Then usc_DaftarAmortisasiBiaya.RefreshTampilanData()
    End Sub

    Sub RefreshTampilanPOPembelian()
        If usc_POPembelian_Lokal_Barang.StatusAktif Then usc_POPembelian_Lokal_Barang.TampilkanData()
        If usc_POPembelian_Lokal_Jasa.StatusAktif Then usc_POPembelian_Lokal_Jasa.TampilkanData()
        If usc_POPembelian_Lokal_BarangDanJasa.StatusAktif Then usc_POPembelian_Lokal_BarangDanJasa.TampilkanData()
        If usc_POPembelian_Lokal_JasaKonstruksi.StatusAktif Then usc_POPembelian_Lokal_JasaKonstruksi.TampilkanData()
        If usc_POPembelian_Lokal_Semua.StatusAktif Then usc_POPembelian_Lokal_Semua.TampilkanData()
        If usc_POPembelian_Impor_Barang.StatusAktif Then usc_POPembelian_Impor_Barang.TampilkanData()
        If usc_POPembelian_Impor_Jasa.StatusAktif Then usc_POPembelian_Impor_Jasa.TampilkanData()
        If usc_POPembelian_Impor_Semua.StatusAktif Then usc_POPembelian_Impor_Semua.TampilkanData()
    End Sub

    Sub RefreshTampilanSJBASTPembelian()
        If usc_SuratJalanPembelian.StatusAktif Then usc_SuratJalanPembelian.TampilkanData()
        If usc_BASTPembelian.StatusAktif Then usc_BASTPembelian.TampilkanData()
    End Sub

    Sub RefreshTampilanInvoicePembelian()
        If usc_InvoicePembelian_DenganPO_Lokal_Rutin.StatusAktif Then usc_InvoicePembelian_DenganPO_Lokal_Rutin.TampilkanData()
        If usc_InvoicePembelian_DenganPO_Lokal_Termin.StatusAktif Then usc_InvoicePembelian_DenganPO_Lokal_Termin.TampilkanData()
        If usc_InvoicePembelian_DenganPO_Impor_Rutin.StatusAktif Then usc_InvoicePembelian_DenganPO_Impor_Rutin.TampilkanData()
        If usc_InvoicePembelian_DenganPO_Impor_Termin.StatusAktif Then usc_InvoicePembelian_DenganPO_Impor_Termin.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Lokal_Barang.StatusAktif Then usc_InvoicePembelian_TanpaPO_Lokal_Barang.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Lokal_Jasa.StatusAktif Then usc_InvoicePembelian_TanpaPO_Lokal_Jasa.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.StatusAktif Then usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.StatusAktif Then usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Impor_Barang.StatusAktif Then usc_InvoicePembelian_TanpaPO_Impor_Barang.TampilkanData()
        If usc_InvoicePembelian_TanpaPO_Impor_Jasa.StatusAktif Then usc_InvoicePembelian_TanpaPO_Impor_Jasa.TampilkanData()
    End Sub

    Sub RefreshTampilanBukuPembelian()
        If usc_BukuPembelian_Lokal.StatusAktif Then usc_BukuPembelian_Lokal.TampilkanData()
        If usc_BukuPembelian_Impor.StatusAktif Then usc_BukuPembelian_Impor.TampilkanData()
    End Sub

    Sub RefreshTampilanBukuPengawasanHutangUsaha()
        If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
        If usc_BukuPengawasanHutangUsaha_Afiliasi.StatusAktif Then usc_BukuPengawasanHutangUsaha_Afiliasi.TampilkanData()
        If usc_BukuPengawasanHutangUsaha_NonAfiliasi.StatusAktif Then usc_BukuPengawasanHutangUsaha_NonAfiliasi.TampilkanData()
    End Sub


    Sub RefreshTampilanPOPenjualan()
        If usc_POPenjualan_Barang.StatusAktif Then usc_POPenjualan_Barang.TampilkanData()
        If usc_POPenjualan_Jasa.StatusAktif Then usc_POPenjualan_Jasa.TampilkanData()
        If usc_POPenjualan_BarangDanJasa.StatusAktif Then usc_POPenjualan_BarangDanJasa.TampilkanData()
        If usc_POPenjualan_JasaKonstruksi.StatusAktif Then usc_POPenjualan_JasaKonstruksi.TampilkanData()
        If usc_POPenjualan_Semua.StatusAktif Then usc_POPenjualan_Semua.TampilkanData()
        If usc_POPenjualan_Ekspor.StatusAktif Then usc_POPenjualan_Ekspor.TampilkanData()
    End Sub

    Sub RefreshTampilanSJBASTPenjualan()
        If usc_SuratJalanPenjualan.StatusAktif Then usc_SuratJalanPenjualan.TampilkanData()
        If usc_BASTPenjualan.StatusAktif Then usc_BASTPenjualan.TampilkanData()
    End Sub

    Sub RefreshTampilanInvoicePenjualan()
        If usc_InvoicePenjualan_DenganPO_Lokal_Rutin.StatusAktif Then usc_InvoicePenjualan_DenganPO_Lokal_Rutin.TampilkanData()
        If usc_InvoicePenjualan_DenganPO_Lokal_Termin.StatusAktif Then usc_InvoicePenjualan_DenganPO_Lokal_Termin.TampilkanData()
        If usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.StatusAktif Then usc_InvoicePenjualan_DenganPO_Ekspor_Rutin.TampilkanData()
        If usc_InvoicePenjualan_DenganPO_Ekspor_Termin.StatusAktif Then usc_InvoicePenjualan_DenganPO_Ekspor_Termin.TampilkanData()
        If usc_InvoicePenjualan_TanpaPO_Lokal_Barang.StatusAktif Then usc_InvoicePenjualan_TanpaPO_Lokal_Barang.TampilkanData()
        If usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.StatusAktif Then usc_InvoicePenjualan_TanpaPO_Lokal_Jasa.TampilkanData()
        If usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.StatusAktif Then usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.TampilkanData()
        If usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.StatusAktif Then usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.TampilkanData()
        If usc_InvoicePenjualan_TanpaPO_Ekspor.StatusAktif Then usc_InvoicePenjualan_TanpaPO_Ekspor.TampilkanData()
        If usc_InvoicePenjualan_Asset.StatusAktif Then usc_InvoicePenjualan_Asset.TampilkanData()
    End Sub

    Sub RefreshTampilanBukuPengawasanPiutangUsaha()
        If usc_BukuPengawasanPiutangUsaha.StatusAktif Then usc_BukuPengawasanPiutangUsaha.TampilkanData()
        If usc_BukuPengawasanPiutangUsaha_Afiliasi.StatusAktif Then usc_BukuPengawasanPiutangUsaha_Afiliasi.TampilkanData()
        If usc_BukuPengawasanPiutangUsaha_NonAfiliasi.StatusAktif Then usc_BukuPengawasanPiutangUsaha_NonAfiliasi.TampilkanData()
    End Sub

    Sub RefreshTampilanBukuPenjualan()
        If usc_BukuPenjualan_Lokal.StatusAktif Then usc_BukuPenjualan_Lokal.TampilkanData()
        If usc_BukuPenjualan_Ekspor.StatusAktif Then usc_BukuPenjualan_Ekspor.TampilkanData()
    End Sub


    Function AmbilValue_JumlahHargaKeseluruhanAsingBerdasarkanNomorPOPenjualan(NomorPO As String) As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing FROM tbl_Penjualan_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaKeseluruhanAsing += (JumlahProduk * HargaSatuanAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahHargaKeseluruhanAsing
    End Function

    Function AmbilValue_DiskonAsingBerdasarkanNomorPOPenjualan(NomorPO As String) As Decimal
        Dim DiskonAsing As Decimal = 0
        Dim JumlahHargaAsing As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        Dim DiskonPersen As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing, Diskon_Per_Item FROM tbl_Penjualan_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaAsing = JumlahProduk * HargaSatuanAsing
            JumlahHargaKeseluruhanAsing += JumlahHargaAsing
            DiskonPersen = drKhusus.Item("Diskon_Per_Item")
            DiskonAsing += ((DiskonPersen / 100) * JumlahHargaAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return DiskonAsing
    End Function

    Function AmbilValue_JumlahHargaKeseluruhanAsingBerdasarkanNomorInvoicePenjualan(NomorInvoice As String) As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing FROM tbl_Penjualan_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaKeseluruhanAsing += (JumlahProduk * HargaSatuanAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahHargaKeseluruhanAsing
    End Function

    Function AmbilValue_DiskonAsingBerdasarkanNomorInvoicePenjualan(NomorInvoice As String) As Decimal
        Dim DiskonAsing As Decimal = 0
        Dim JumlahHargaAsing As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        Dim DiskonPersen As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan, Diskon_Per_Item FROM tbl_Penjualan_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan")
            JumlahHargaAsing = JumlahProduk * HargaSatuanAsing
            JumlahHargaKeseluruhanAsing += JumlahHargaAsing
            DiskonPersen = drKhusus.Item("Diskon_Per_Item")
            DiskonAsing += ((DiskonPersen / 100) * JumlahHargaAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return DiskonAsing
    End Function

    Function AmbilValue_JumlahHargaKeseluruhanAsingBerdasarkanNomorPOPembelian(NomorPO As String) As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing FROM tbl_Pembelian_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaKeseluruhanAsing += (JumlahProduk * HargaSatuanAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahHargaKeseluruhanAsing
    End Function

    Function AmbilValue_DiskonAsingBerdasarkanNomorPOPembelian(NomorPO As String) As Decimal
        Dim DiskonAsing As Decimal = 0
        Dim JumlahHargaAsing As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        Dim DiskonPersen As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing, Diskon_Per_Item FROM tbl_Pembelian_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaAsing = JumlahProduk * HargaSatuanAsing
            JumlahHargaKeseluruhanAsing += JumlahHargaAsing
            DiskonPersen = drKhusus.Item("Diskon_Per_Item")
            DiskonAsing += ((DiskonPersen / 100) * JumlahHargaAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return DiskonAsing
    End Function

    Function AmbilValue_JumlahHargaKeseluruhanAsingBerdasarkanNomorInvoicePembelian(NomorInvoice As String) As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan_Asing FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan_Asing")
            JumlahHargaKeseluruhanAsing += (JumlahProduk * HargaSatuanAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahHargaKeseluruhanAsing
    End Function

    Function AmbilValue_DiskonAsingBerdasarkanNomorInvoicePembelian(NomorInvoice As String) As Decimal
        Dim DiskonAsing As Decimal = 0
        Dim JumlahHargaAsing As Decimal
        Dim JumlahHargaKeseluruhanAsing As Decimal = 0
        Dim JumlahProduk As Integer
        Dim HargaSatuanAsing As Decimal
        Dim DiskonPersen As Decimal
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Jumlah_Produk, Harga_Satuan, Diskon_Per_Item FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        Do While drKhusus.Read()
            JumlahProduk = drKhusus.Item("Jumlah_Produk")
            HargaSatuanAsing = drKhusus.Item("Harga_Satuan")
            JumlahHargaAsing = JumlahProduk * HargaSatuanAsing
            JumlahHargaKeseluruhanAsing += JumlahHargaAsing
            DiskonPersen = drKhusus.Item("Diskon_Per_Item")
            DiskonAsing += ((DiskonPersen / 100) * JumlahHargaAsing)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return DiskonAsing
    End Function

    Function AmbilValue_NomorFakturPajakBerdasarkanNomorInvoicePembelian(NomorInvoice As String) As String
        Dim NomorFakturPajak As String
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Nomor_Faktur_Pajak FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            NomorFakturPajak = drKhusus.Item("Nomor_Faktur_Pajak")
        Else
            NomorFakturPajak = Kosongan
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return NomorFakturPajak
    End Function

    Function AmbilValue_TanggalInvoiceBerdasarkanNomorInvoicePembelian(NomorInvoice As String) As Date
        Dim TanggalInvoice As Date
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHusus = New OdbcCommand(" SELECT Tanggal_Invoice FROM tbl_Pembelian_Invoice " &
                                        " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        Dim drKhusus = cmdKHusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then
            TanggalInvoice = drKhusus.Item("Tanggal_Invoice")
        Else
            TanggalInvoice = Kosongan
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return TanggalInvoice
    End Function


    Function DaftarCOALawan(ByVal NomorJV As String, ByVal DK As String) As String
        Dim COALawan As String = Kosongan
        Dim COALawan_Satuan As String
        Dim DKLawan As String
        If DK = dk_D Then
            DKLawan = dk_K
        Else
            DKLawan = dk_D
        End If
        Dim cmdKHUSUS = New OdbcCommand(" SELECT COA FROM tbl_Transaksi " &
                                        " WHERE Nomor_JV    = '" & NomorJV & "' " &
                                        " AND D_K           = '" & DKLawan & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            COALawan_Satuan = drKHUSUS.Item("COA")
            If COALawan = Kosongan Then
                COALawan &= COALawan_Satuan
            Else
                COALawan &= SlashGanda_Pemisah & COALawan_Satuan
            End If
        Loop
        Return COALawan
    End Function


    Function DaftarNamaAkunLawan(ByVal NomorJV As String, ByVal DK As String) As String
        Dim NamaAkunLawan As String = Kosongan
        Dim NamaAkunLawan_Satuan As String
        Dim DKLawan As String
        If DK = dk_D Then
            DKLawan = dk_K
        Else
            DKLawan = dk_D
        End If
        Dim cmdKHUSUS = New OdbcCommand(" SELECT Nama_Akun FROM tbl_Transaksi " &
                                        " WHERE Nomor_JV    = '" & NomorJV & "' " &
                                        " AND D_K           = '" & DKLawan & "' ", KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Do While drKHUSUS.Read
            NamaAkunLawan_Satuan = drKHUSUS.Item("Nama_Akun")
            If NamaAkunLawan = Kosongan Then
                NamaAkunLawan &= NamaAkunLawan_Satuan
            Else
                NamaAkunLawan &= SlashGanda_Pemisah & NamaAkunLawan_Satuan
            End If
        Loop
        Return NamaAkunLawan
    End Function


    Sub UpdateInfoBulanBukuAktif()
        'Ini harus masing-masing, walau pun codingnya hampir sama dengan Function BulanBukuAktif AmbilValue_BulanBukuAktif()
        BukaDatabaseGeneral_Kondisional()
        Dim BulanTelusur As Integer = 1 'Ini Harus dimulai dari 1. Jangan nol (0)
        Dim KolomBulan As String
        Do While BulanTelusur <= 12 'Ini sudah benar pakai tanda '<='
            KolomBulan = "Saldo_" & KonversiAngkaKeBulanString(BulanTelusur)
            Dim cmdKHUSUS = New OdbcCommand(
                " SELECT " & KolomBulan &
                " FROM tbl_COA " &
                " WHERE Visibilitas = '" & Pilihan_Ya & "' ", KoneksiDatabaseGeneral)
            Dim drKHUSUS = cmdKHUSUS.ExecuteReader
            Dim TotalSaldo As Int64 = 0
            Do While drKHUSUS.Read
                TotalSaldo += drKHUSUS.Item(KolomBulan)
            Loop
            If TotalSaldo = 0 Then Exit Do
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            BulanTelusur += 1 'Ini sudah benar di bawah, untuk menghasilkan BulanBukuAktif = 13, yang artinya Pembukuan di TahunBukuAktif sudah ditutup semua
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Loop
        BulanBukuAktif = BulanTelusur
        BulanTerakhirDitutup = BulanTelusur - 1
        TutupDatabaseGeneral_Kondisional()
    End Sub

    Sub RefreshForm(frm As Form)
        If frm IsNot Nothing Then
            Dim stateAwal = frm.WindowState
            frm.WindowState = FormWindowState.Minimized
            Terabas()
            frm.WindowState = stateAwal
        End If
    End Sub



End Module
