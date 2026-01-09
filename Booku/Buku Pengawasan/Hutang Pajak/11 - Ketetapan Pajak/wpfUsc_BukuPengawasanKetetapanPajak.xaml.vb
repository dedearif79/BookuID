Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanKetetapanPajak

    Public StatusAktif As Boolean = False

    Public JudulForm
    Public JenisPajak
    Dim DataDitampilkanSemua As Boolean

    Dim TahunTunggakanPajak
    Dim TahunTunggakanPajakTerlama

    Public NamaHalaman As String
    Public AwalanBP As String
    Public COAHutangPajak As String

    Dim NomorUrut
    Dim NomorID
    Dim KodeAkun_PokokPajak
    Dim Nomor
    Dim KodeJenisPajak
    Dim JenisPajakPerBaris
    Dim MasaPajak_Awal
    Dim MasaPajak_Akhir
    Dim MasaPajak
    'Dim TahunTunggakanPajak_Tabel (INi sudah tidak diperlukan. Nanti hapus saja....!)
    Dim TanggalKetetapan
    Dim NomorKetetapan
    Dim NomorBPHP
    Dim JumlahKetetapan
    Dim PokokPajak
    Dim Sanksi
    Dim JumlahTagihan
    Dim JumlahBayar
    Dim SisaTagihan
    Dim StatusLunas
    Dim Keterangan
    Dim NomorJV

    Dim TotalTagihan_PokokPajak
    Dim TotalTagihan_Sanksi
    Dim TotalTagihan_JumlahKetetapan
    Dim TotalBayar
    Dim Total_SisaTagihan

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim KodeAkun_PokokPajak_Terseleksi
    Dim Nomor_Terseleksi
    Dim KodeJenisPajak_Terseleksi
    Dim JenisPajakPerBaris_Terseleksi
    Dim MasaPajak_Awal_Terseleksi
    Dim MasaPajak_Akhir_Terseleksi
    Dim MasaPajak_Terseleksi
    Dim TahunTunggakanPajak_Terseleksi
    Dim TahunPenerbitan_Terseleksi
    Dim TanggalKetetapan_Terseleksi
    Dim NomorKetetapan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim JumlahKetetapan_Terseleksi
    Dim PokokPajak_Terseleksi
    Dim Sanksi_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaTagihan_Terseleksi
    Dim Keterangan_Terseleksi
    Dim StatusLunas_Terseleksi
    Dim NomorJV_Terseleksi

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim TahunPenerbitanTerlama

    Dim SisaHutang_SaatCutOff

    Dim PilihBayar
    Dim PilihBayar_Pokok = "Bayar Pokok"
    Dim PilihBayar_Sanksi = "Bayar Sanksi"

    Dim Pilihan_TahunTunggakanPajak As String
    Dim Pilihan_TahunPenerbitan As String
    Dim Pilihan_JenisPajak
    Dim Pilihan_StatusLunas

    Dim TahunPenerbitan

    Dim FilterData
    Dim FilterTahunPajak
    Dim FilterTahunPenerbitan
    Dim FilterJenisPajak

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    'Kolom Lapor :
    Dim TanggalLapor
    Dim NomorIDLapor
    Dim TWTL_Lapor
    Dim NP_Lapor
    Dim TanggalLapor_Terseleksi
    Dim NomorIDLapor_Terseleksi
    Dim NP_Lapor_Terseleksi

    Dim Bulan_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True
        Terabas()

        ProsesLoadingForm = True

        VisibilitasTombolJurnal(True)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If


        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_TahunTunggakanPajak()
        KontenCombo_TahunPenerbitan()
        KontenCombo_PilihanJenisPajak()
        KontenCombo_PilihanStatusLunas()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub

    Dim EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If EksekusiTampilanData = False Then Return

        LogikaTampilan()

        If cmb_PilihanJenisPajak.Text = Kosongan Then Return
        If cmb_PilihanStatusLunas.Text = Kosongan Then Return

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Filter Tahun Pajak :
        If Pilihan_TahunTunggakanPajak = Pilihan_Semua Then
            FilterTahunPajak = Spasi1
        Else
            FilterTahunPajak = " AND Tahun_Pajak = '" & Pilihan_TahunTunggakanPajak & "' "
        End If

        'Filter Tahun Pajak :
        If Pilihan_TahunPenerbitan = Pilihan_Semua Then
            FilterTahunPenerbitan = Spasi1
        Else
            FilterTahunPenerbitan = " AND DATE_FORMAT(Tanggal_Ketetapan, '%Y') = '" & Pilihan_TahunPenerbitan & "' "
        End If

        'Filter Jenis Pajak :
        If Pilihan_JenisPajak = Pilihan_Semua Then
            FilterJenisPajak = Kosongan
        Else
            FilterJenisPajak = " AND Jenis_Pajak = '" & Pilihan_JenisPajak & "' "
        End If

        'Filter Data :
        FilterData = FilterTahunPajak & FilterTahunPenerbitan & FilterJenisPajak

        'Data Tabel :
        NomorUrut = 0
        TotalTagihan_PokokPajak = 0
        TotalTagihan_Sanksi = 0
        TotalTagihan_JumlahKetetapan = 0
        TotalBayar = 0
        Total_SisaTagihan = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak WHERE Nomor_ID > 0 " & FilterData, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPHP = dr.Item("Nomor_BPHP")
            KodeAkun_PokokPajak = dr.Item("Kode_Akun_Pokok_Pajak")
            Nomor = dr.Item("Nomor")
            KodeJenisPajak = dr.Item("Kode_Jenis_Pajak")
            JenisPajakPerBaris = dr.Item("Jenis_Pajak")
            MasaPajak_Awal = dr.Item("Masa_Pajak_Awal")
            MasaPajak_Akhir = dr.Item("Masa_Pajak_Akhir")
            If MasaPajak_Awal = MasaPajak_Akhir Then
                MasaPajak = MasaPajak_Awal
            Else
                MasaPajak = MasaPajak_Awal & " - " & MasaPajak_Akhir
            End If
            TahunTunggakanPajak = dr.Item("Tahun_Pajak")
            NomorKetetapan = dr.Item("Nomor_Ketetapan")
            TanggalKetetapan = TanggalFormatTampilan(dr.Item("Tanggal_Ketetapan"))
            TahunPenerbitan = AmbilTahun_DariTanggal(TanggalKetetapan)
            JumlahKetetapan = dr.Item("Jumlah_Ketetapan")
            PokokPajak = dr.Item("Pokok_Pajak")
            Sanksi = dr.Item("Sanksi")
            JumlahTagihan = JumlahKetetapan

            'Data Pembayaran :
            JumlahBayar = 0
            Dim TahunTelusurPembayaran = TahunPenerbitan
            Dim PencegahLoopingTahunPajakLampau = 0
            Do While TahunTelusurPembayaran <= TahunBukuAktif
                If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
                If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
                If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                    BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                    cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                               " WHERE Nomor_BP         = '" & NomorBPHP & "' " &
                                               " AND Status_Invoice     = '" & Status_Dibayar & "' ",
                                               KoneksiDatabaseTransaksi_Alternatif)
                    drBAYAR_ExecuteReader()
                    Do While drBAYAR.Read
                        JumlahBayar += drBAYAR.Item("Jumlah_Bayar")
                        If JumlahBayar >= PokokPajak Then Exit Do
                    Loop
                    TutupDatabaseTransaksi_Alternatif()
                End If
                If JumlahBayar >= PokokPajak Then Exit Do
                PencegahLoopingTahunPajakLampau += 1
                TahunTelusurPembayaran += 1
            Loop

            SisaTagihan = JumlahTagihan - JumlahBayar
            If SisaTagihan = 0 Then
                StatusLunas = StatusLunas_Lunas
            Else
                StatusLunas = StatusLunas_BelumLunas
            End If
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV = dr.Item("Nomor_JV")

            If Pilihan_StatusLunas = Pilihan_Semua Then
                TambahBaris()
            Else
                If Pilihan_StatusLunas = StatusLunas Then TambahBaris()
            End If

        Loop

        AksesDatabase_Transaksi(Tutup)

        If AmbilAngka(Total_SisaTagihan) <= 0 Then
            StatusLunas = StatusLunas_LUNAS_
        Else
            StatusLunas = Kosongan
        End If

        'Baris Total :
        If NomorUrut > 0 Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, "   T O T A L  ",
                                    Kosongan, Kosongan, Kosongan, Kosongan,
                                    TotalTagihan_PokokPajak, TotalTagihan_Sanksi, TotalTagihan_JumlahKetetapan,
                                    TotalBayar, Total_SisaTagihan,
                                    StatusLunas, Kosongan, 0)
        End If

        '-----------------------------------------------------------------------------------------------------------------------------
        AksesDatabase_Transaksi(Buka)

        'Hitung Total Tagihan Selama Sebelum Cut Off :
        Dim TotalTagihan_SelamaSebelumCutOff As Int64 = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalTagihan_SelamaSebelumCutOff += dr.Item("Jumlah_Ketetapan")
        Loop

        'Hitung Total Pembayaran Selama Sebelum Cut Off :
        Dim TotalBayar_SelamaSebelumCutOff As Int64 = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP   LIKE '" & AwalanBP & "%' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalBayar_SelamaSebelumCutOff += dr.Item("Jumlah_Bayar")
        Loop

        AksesDatabase_Transaksi(Tutup)

        SisaHutang_SaatCutOff = TotalTagihan_SelamaSebelumCutOff - TotalBayar_SelamaSebelumCutOff
        '-----------------------------------------------------------------------------------------------------------------------------

        lbl_TotalTabel.Text = "Saldo Akhir " & TahunBukuAktif & " : "

        If DataDitampilkanSemua Then
            VisibilitasInfoSaldo(True)
            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    SaldoAkhir_BerdasarkanList = SisaHutang_SaatCutOff
                    txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                    AmbilValue_SaldoAkhirBerdasarkanCOA()
                    CekKesesuaianSaldoAkhir()
                    txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
                Case JenisTahunBuku_NORMAL
                    'Penjelasan : Variabel di bawah ini untuk mendapatkan jumlah bayar atas hutang pajak tahun sebelum TahunBukuAktif,
                    'tapi dibayarkan pada tahun ini (TahunBukuAktif).
                    Dim TotalBayar_UntukHutangPajakTahunSebelumIni As Int64 = 0
                    AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_Non, TotalBayar_UntukHutangPajakTahunSebelumIni)
                    If Not TahunBukuSudahStabil(TahunBukuAktif) Then
                        AmbilValue_SaldoAwalBerdasarkanList()
                        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                        CekKesesuaianSaldoAwal()
                        txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
                        txt_TotalTabel.Text = SaldoAwal_BerdasarkanList + TotalTagihan_JumlahKetetapan - (TotalBayar + TotalBayar_UntukHutangPajakTahunSebelumIni)
                    Else
                        txt_TotalTabel.Text = SaldoAwal_BerdasarkanCOA + TotalTagihan_JumlahKetetapan - (TotalBayar + TotalBayar_UntukHutangPajakTahunSebelumIni)
                    End If
            End Select
        Else
            VisibilitasInfoSaldo(False)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()

        TotalTagihan_PokokPajak += PokokPajak
        TotalTagihan_Sanksi += Sanksi
        TotalTagihan_JumlahKetetapan += JumlahKetetapan
        TotalBayar += JumlahBayar
        Total_SisaTagihan += SisaTagihan

        If StatusLunas = StatusLunas_BelumLunas Then StatusLunas = Kosongan

        datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, KodeAkun_PokokPajak,
                                Nomor, KodeJenisPajak, JenisPajakPerBaris,
                                MasaPajak_Awal, MasaPajak_Akhir, MasaPajak,
                                TahunTunggakanPajak, TahunPenerbitan, NomorKetetapan, TanggalKetetapan,
                                PokokPajak, Sanksi, JumlahKetetapan,
                                JumlahBayar, SisaTagihan,
                                StatusLunas, Keterangan, NomorJV)

    End Sub


    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_DetailPembayaran.IsEnabled = True
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub


    Sub LogikaTampilan()
        If Pilihan_TahunTunggakanPajak = Pilihan_Semua _
            And Pilihan_TahunPenerbitan = Pilihan_Semua _
            And Pilihan_JenisPajak = Pilihan_Semua _
            And Pilihan_StatusLunas = Pilihan_Semua _
            Then
            DataDitampilkanSemua = True
        Else
            DataDitampilkanSemua = False
        End If
    End Sub


    Sub KontenCombo_TahunTunggakanPajak()

        TahunTunggakanPajakTerlama = TahunBukuAktif - 1 '(Sementara begini dulu, karena belum ditentukan sistemnya).
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunTunggakanPajak.Items.Clear()
        Do While ListTahunPajak >= TahunTunggakanPajakTerlama
            cmb_TahunTunggakanPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunTunggakanPajak.Items.Add(Pilihan_Semua)
        cmb_TahunTunggakanPajak.SelectedValue = Pilihan_Semua

    End Sub

    Sub KontenCombo_TahunPenerbitan()

        'TahunTunggakanPajakTerlama = AmbilTahunTerlama("tbl_SisaHutangPPN", "Tanggal_Transaksi")
        TahunPenerbitanTerlama = TahunBukuAktif - 1 '(Sementara begini dulu, karena belum ditentukan sistemnya).
        Dim ListTahunPenerbitan = TahunBukuAktif

        cmb_TahunPenerbitan.Items.Clear()
        Do While ListTahunPenerbitan >= TahunPenerbitanTerlama
            cmb_TahunPenerbitan.Items.Add(ListTahunPenerbitan)
            ListTahunPenerbitan -= 1
        Loop
        cmb_TahunPenerbitan.Items.Add(Pilihan_Semua)
        cmb_TahunPenerbitan.SelectedValue = Pilihan_Semua

    End Sub

    Sub KontenCombo_PilihanJenisPajak()
        cmb_PilihanJenisPajak.Items.Clear()
        cmb_PilihanJenisPajak.Items.Add(Pilihan_Semua)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal21)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal22_Lokal)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal23)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal42)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal25)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPhPasal26)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPN)
        cmb_PilihanJenisPajak.Items.Add(JenisPajak_PPN_Impor)
        cmb_PilihanJenisPajak.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_PilihanStatusLunas()
        cmb_PilihanStatusLunas.Items.Clear()
        cmb_PilihanStatusLunas.Items.Add(Pilihan_Semua)
        cmb_PilihanStatusLunas.Items.Add(StatusLunas_Lunas)
        cmb_PilihanStatusLunas.Items.Add(StatusLunas_BelumLunas)
        cmb_PilihanStatusLunas.SelectedValue = Pilihan_Semua
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        brd_LihatJurnal.Visibility = Visibility.Collapsed
        btn_LihatJurnal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_LihatJurnal.Visibility = Visibility.Visible
                btn_LihatJurnal.Visibility = Visibility.Visible
            End If
        Else
            brd_LihatJurnal.Visibility = Visibility.Collapsed
            btn_LihatJurnal.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolCRUD(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_CRUD.Visibility = Visibility.Visible
        Else
            pnl_CRUD.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolDetailPembayaran(Visibilitas As Boolean)
        brd_DetailPembayaran.Visibility = Visibility.Collapsed
        btn_DetailPembayaran.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_DetailPembayaran.Visibility = Visibility.Visible
                btn_DetailPembayaran.Visibility = Visibility.Visible
            End If
        Else
            brd_DetailPembayaran.Visibility = Visibility.Collapsed
            btn_DetailPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolUpdateBayar(Visibilitas As Boolean)
        If Visibilitas Then
            btn_EditBayar.Visibility = Visibility.Visible
            btn_HapusBayar.Visibility = Visibility.Visible
        Else
            btn_EditBayar.Visibility = Visibility.Collapsed
            btn_HapusBayar.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If DataDitampilkanSemua Then
                grb_InfoSaldo.Visibility = Visibility.Visible
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                    pnl_TotalTabel.Visibility = Visibility.Visible
                End If
            End If
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        If KodeKPP_Perusahaan = Kosongan Then
            PesanPeringatan("Untuk dapat mengaktifkan menu ini, silakan lengkapi terlebih dahulu data 'Kode KPP' pada menu 'Company Profile'.")
            Return
        End If

        win_InputKetetapanPajak = New wpfWin_InputKetetapanPajak
        win_InputKetetapanPajak.ResetForm()
        win_InputKetetapanPajak.FungsiForm = FungsiForm_TAMBAH
        win_InputKetetapanPajak.ShowDialog()

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        If JumlahBayar_Terseleksi > 0 Then
            Pesan_Peringatan("Nomor Ketetapan ini tidak dapat diedit, karena sudah ada data pembayaran." & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu seluruh data pembayaran yang terkait dengan Nomor Ketetapan ini.")
            Return
        End If

        win_InputKetetapanPajak = New wpfWin_InputKetetapanPajak
        win_InputKetetapanPajak.ResetForm()
        win_InputKetetapanPajak.FungsiForm = FungsiForm_EDIT
        win_InputKetetapanPajak.NomorID = NomorID_Terseleksi
        win_InputKetetapanPajak.txt_Nomor.Text = Nomor_Terseleksi
        win_InputKetetapanPajak.txt_KodeJenisPajak.Text = KodeJenisPajak_Terseleksi
        win_InputKetetapanPajak.txt_JenisPajak.Text = JenisPajakPerBaris_Terseleksi
        win_InputKetetapanPajak.cmb_MasaPajak_Awal.SelectedValue = MasaPajak_Awal_Terseleksi
        win_InputKetetapanPajak.cmb_MasaPajak_Akhir.SelectedValue = MasaPajak_Akhir_Terseleksi
        win_InputKetetapanPajak.txt_TahunPajak_Inputan.Text = TahunTunggakanPajak_Terseleksi
        win_InputKetetapanPajak.txt_NomorKetetapan.Text = NomorKetetapan_Terseleksi
        win_InputKetetapanPajak.NomorBPHP = NomorBPHP_Terseleksi
        win_InputKetetapanPajak.dtp_TanggalKetetapan.SelectedDate = TanggalFormatWPF(TanggalKetetapan_Terseleksi)
        win_InputKetetapanPajak.txt_KodeAkun_PokokPajak.Text = KodeAkun_PokokPajak_Terseleksi
        win_InputKetetapanPajak.txt_PokokPajak.Text = PokokPajak_Terseleksi
        win_InputKetetapanPajak.txt_Sanksi.Text = Sanksi_Terseleksi
        win_InputKetetapanPajak.txt_JumlahKetetapan.Text = JumlahKetetapan_Terseleksi
        IsiValueElemenRichTextBox(win_InputKetetapanPajak.txt_Keterangan, Keterangan_Terseleksi)
        win_InputKetetapanPajak.NomorJV = NomorJV_Terseleksi
        win_InputKetetapanPajak.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If JumlahBayar_Terseleksi > 0 Then
            Pesan_Peringatan("Nomor Ketetapan ini tidak dapat dihapus, karena sudah ada data pembayaran." & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu seluruh data pembayaran yang terkait dengan Nomor Ketetapan ini.")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_KetetapanPajak " &
                              " WHERE Nomor_BPHP = '" & NomorBPHP_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub btn_DetailPembayaran_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak
        frm_DetailPembayaranPajak.ShowDialog()
    End Sub


    Private Sub cmb_TahunTunggakanPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunTunggakanPajak.SelectionChanged

        Pilihan_TahunTunggakanPajak = cmb_TahunTunggakanPajak.SelectedValue

        If Pilihan_TahunTunggakanPajak = Pilihan_Semua Then
            TahunTunggakanPajak = 0 'Value ini nanti diisi di sub TampilkanData()
        Else
            TahunTunggakanPajak = Pilihan_TahunTunggakanPajak
        End If

        TampilkanData()

    End Sub


    Private Sub cmb_TahunPenerbitan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunPenerbitan.SelectionChanged

        Pilihan_TahunPenerbitan = cmb_TahunPenerbitan.SelectedValue

        If Pilihan_TahunPenerbitan = Pilihan_Semua Then
            TahunPenerbitan = 0 'Value ini nanti diisi di sub TampilkanData()
        Else
            TahunPenerbitan = Pilihan_TahunPenerbitan
        End If

        TampilkanData()

    End Sub


    Private Sub cmb_PilihanJenisPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PilihanJenisPajak.SelectionChanged
        Pilihan_JenisPajak = cmb_PilihanJenisPajak.SelectedValue
        TampilkanData()
    End Sub


    Private Sub cmb_PilihanStatusLunas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PilihanStatusLunas.SelectionChanged
        Pilihan_StatusLunas = cmb_PilihanStatusLunas.SelectedValue
        TampilkanData()
    End Sub


    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return
        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        If NomorUrut_Terseleksi = 0 Then
            BersihkanSeleksi()
            Return
        End If
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        KodeAkun_PokokPajak_Terseleksi = rowviewUtama("Kode_Akun_Pokok_Pajak")
        Nomor_Terseleksi = rowviewUtama("Nomor_")
        KodeJenisPajak_Terseleksi = rowviewUtama("Kode_Jenis_Pajak")
        JenisPajakPerBaris_Terseleksi = rowviewUtama("Jenis_Pajak")
        MasaPajak_Awal_Terseleksi = rowviewUtama("Masa_Pajak_Awal")
        MasaPajak_Akhir_Terseleksi = rowviewUtama("Masa_Pajak_Akhir")
        MasaPajak_Terseleksi = rowviewUtama("Masa_Pajak")
        TahunTunggakanPajak_Terseleksi = rowviewUtama("Tahun_Pajak")
        TahunPenerbitan_Terseleksi = rowviewUtama("Tahun_Penerbitan")
        TanggalKetetapan_Terseleksi = rowviewUtama("Tanggal_Ketetapan")
        NomorKetetapan_Terseleksi = rowviewUtama("Nomor_Ketetapan")
        NomorBPHP_Terseleksi = rowviewUtama("Nomor_BPHP")
        JumlahKetetapan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Ketetapan"))
        PokokPajak_Terseleksi = AmbilAngka(rowviewUtama("Pokok_Pajak"))
        Sanksi_Terseleksi = AmbilAngka(rowviewUtama("Sanksi_"))
        JumlahTagihan_Terseleksi = JumlahKetetapan_Terseleksi
        JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar"))
        SisaTagihan_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Tagihan"))
        StatusLunas_Terseleksi = rowviewUtama("Status_Lunas")
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))

        If JumlahTagihan_Terseleksi > 0 Then
            ResetTampilanDataPembayaran()
            btn_LihatJurnal.IsEnabled = True
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub






    Sub ResetTampilanDataPembayaran()
        pnl_SidebarKanan.Visibility = Visibility.Visible
        TampilkanDataPembayaran()
    End Sub

    Sub TampilkanDataPembayaran()

        pnl_DataGridBayar.Visibility = Visibility.Visible

        datatabelBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar As Int64 = 0
        Dim TWTLBayar = Kosongan
        Dim TotalBayar As Int64 = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran
        Dim TahunSumberDataPembayaran = 0

        Dim TahunTelusurPembayaran = TahunPenerbitan_Terseleksi
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP          = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Status_Invoice      = '" & Status_Dibayar & "' " &
                                      " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJV_Pembayaran = dr.Item("Nomor_JV")
                    Else
                        NomorJV_Pembayaran = 0
                    End If
                    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, TWTLBayar, KeteranganBayar, NomorJV_Pembayaran)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
            PencegahLoopingTahunPajakLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        BersihkanSeleksiTabelPembayaran()

    End Sub

    Sub BersihkanSeleksiTabelPembayaran()
        BersihkanSeleksi_WPF(datagridBayar, datatabelBayar, BarisBayar_Terseleksi, JumlahBarisBayar)
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
        VisibilitasTabelPembayaran()
    End Sub



    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            btn_LihatJurnal.IsEnabled = False
            BersihkanSeleksiTabelPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorJV_Terseleksi = 0
        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = rowviewBayar("Nomor_JV_Bayar")
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
            btn_EditBayar.IsEnabled = False
            btn_HapusBayar.IsEnabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            'btn_EditPembayaran.isenabled = False
            'btn_HapusPembayaran.isenabled = False
        End If
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub



    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaTagihan_Terseleksi <= 0 Then
            Pesan_Informasi("Data terpilih sudah dibayar seluruhnya.")
            Return
        End If


        Dim TanggalTagihanPajak = TanggalKetetapan_Terseleksi
        '(Variabel ini tidak ada kaitannya dengan Invoice dari DJP. Ini hanya untuk kepentingan algoritma.)

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran_Non
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, NomorKetetapan_Terseleksi, TanggalTagihanPajak, "Pembayaran Denda Pajak", NomorBPHP_Terseleksi,
                                JumlahTagihan_Terseleksi, 0, 0, 0, JumlahBayar_Terseleksi, SisaTagihan_Terseleksi,
                                JenisPajak, KodeSetoran_Non, 0, 0, 0,
                                SisaTagihan_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub

    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub




    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_BPHP As New DataGridTextColumn
    Dim Kode_Akun_Pokok_Pajak As New DataGridTextColumn
    Dim Nomor_ As New DataGridTextColumn
    Dim Kode_Jenis_Pajak As New DataGridTextColumn
    Dim Jenis_Pajak As New DataGridTextColumn
    Dim Masa_Pajak_Awal As New DataGridTextColumn
    Dim Masa_Pajak_Akhir As New DataGridTextColumn
    Dim Masa_Pajak As New DataGridTextColumn
    Dim Tahun_Pajak As New DataGridTextColumn
    Dim Tahun_Penerbitan As New DataGridTextColumn
    Dim Nomor_Ketetapan As New DataGridTextColumn
    Dim Tanggal_Ketetapan As New DataGridTextColumn
    Dim Pokok_Pajak As New DataGridTextColumn
    Dim Sanksi_ As New DataGridTextColumn
    Dim Jumlah_Ketetapan As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Sisa_Tagihan As New DataGridTextColumn
    Dim Status_Lunas As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPHP")
        datatabelUtama.Columns.Add("Kode_Akun_Pokok_Pajak")
        datatabelUtama.Columns.Add("Nomor_")
        datatabelUtama.Columns.Add("Kode_Jenis_Pajak")
        datatabelUtama.Columns.Add("Jenis_Pajak")
        datatabelUtama.Columns.Add("Masa_Pajak_Awal")
        datatabelUtama.Columns.Add("Masa_Pajak_Akhir")
        datatabelUtama.Columns.Add("Masa_Pajak")
        datatabelUtama.Columns.Add("Tahun_Pajak")
        datatabelUtama.Columns.Add("Tahun_Penerbitan")
        datatabelUtama.Columns.Add("Nomor_Ketetapan")
        datatabelUtama.Columns.Add("Tanggal_Ketetapan")
        datatabelUtama.Columns.Add("Pokok_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Sanksi_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Ketetapan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Status_Lunas")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPHP, "Nomor_BPHP", "Nomor BPHP", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun_Pokok_Pajak, "Kode_Akun_Pokok_Pajak", "Kode Akun", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_, "Nomor_", "Nomor", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Jenis_Pajak, "Kode_Jenis_Pajak", "Kode Jenis Pajak", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Pajak, "Jenis_Pajak", "Jenis Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Pajak_Awal, "Masa_Pajak_Awal", "Masa Awal", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Pajak_Akhir, "Masa_Pajak_Akhir", "Masa Akhir", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Pajak, "Masa_Pajak", "Masa Pajak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tahun_Pajak, "Tahun_Pajak", "Tahun Pajak", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tahun_Penerbitan, "Tahun_Penerbitan", "Tahun Penerbitan", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Ketetapan, "Nomor_Ketetapan", "Nomor Ketetapan", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Ketetapan, "Tanggal_Ketetapan", "Tanggal Ketetapan", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pokok_Pajak, "Pokok_Pajak", "Pokok Pajak", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sanksi_, "Sanksi_", "Sanksi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Ketetapan, "Jumlah_Ketetapan", "Jumlah Ketetapan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan, "Sisa_Tagihan", "Sisa Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Lunas, "Status_Lunas", "Status Lunas", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 72, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomTerseleksiBayar As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim TW_TL_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("TW_TL_Bayar")
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, TW_TL_Bayar, "TW_TL_Bayar", "TW/TL", 45, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        cmb_TahunTunggakanPajak.IsReadOnly = True
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
        pnl_Footer.Width = LebarKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub


    Sub AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran As String, ByRef SaldoAwal_BerdasarkanList As Int64, txt_SaldoBerdasarkanList As TextBox)
        If DataDitampilkanSemua Then
            Dim JumlahTagihan_SA As Int64
            Dim JumlahBayar_SA As Int64
            Dim TahunTelusur_SA = TahunBukuAktif - 1
            If TahunTelusur_SA = TahunCutOff Then
                JumlahTagihan_SA = 0
                BukaDatabaseTransaksi_Alternatif(TahunTelusur_SA)
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                         " WHERE Jenis_Pajak    = '" & JenisPajak & "' " &
                                         " AND Kode_Setoran     = '" & KodeSetoran & "' ",
                                         KoneksiDatabaseTransaksi_Alternatif)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                TutupDatabaseTransaksi_Alternatif()
            Else
                'Ini tidak diperlukan, karena ketika TahunBukuAktif sudah 2 tahun dari TahunCutOff, maka tidak perlu ada lagi pengecekan keseimbangan,
                'karena sudah dipastikan akan sesuai antara data finance dengan data akuntansi.
            End If
            AmbilValue_JumlahBayarPajakTahunBukuKemarin_Public(AwalanBP, KodeSetoran, JumlahBayar_SA)
            SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Sub AmbilValue_SaldoAwalBerdasarkanList()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_Non, SaldoAwal_BerdasarkanList, txt_SaldoBerdasarkanList)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak, SaldoAkhir_BerdasarkanCOA, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub CekKesesuaianSaldoAwal()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian, KesesuaianSaldoAwal,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList, SaldoAkhir_BerdasarkanCOA, KesesuaianSaldoAkhir,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP)
    End Sub

    Private Sub txt_TotalTabel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel)
    End Sub

    '=======================================================================================================================================

End Class
