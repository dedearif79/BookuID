Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanKetetapanPajak_X

    Dim NomorUrut
    Dim NomorID
    Dim KodeAkun_PokokPajak
    Dim Nomor
    Dim KodeJenisPajak
    Dim JenisPajak
    Dim MasaPajak_Awal
    Dim MasaPajak_Akhir
    Dim MasaPajak
    Dim TahunPajak_Tabel
    Dim TanggalKetetapan
    Dim NomorKetetapan
    Dim NomorBPHP
    Dim JumlahKetetapan
    Dim PokokPajak
    Dim Sanksi
    Dim JumlahTagihan
    Dim JumlahBayarPokok
    Dim JumlahBayarSanksi
    Dim JumlahBayar
    Dim SisaTagihan
    Dim StatusLunas
    Dim Keterangan
    Dim NomorJV

    Dim TotalTagihan_PokokPajak
    Dim TotalTagihan_Sanksi
    Dim TotalTagihan_JumlahKetetapan
    Dim TotalBayar_Pokok
    Dim TotalBayar_Sanksi
    Dim TotalBayar
    Dim Total_SisaTagihan

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim KodeAkun_PokokPajak_Terseleksi
    Dim Nomor_Terseleksi
    Dim KodeJenisPajak_Terseleksi
    Dim JenisPajak_Terseleksi
    Dim MasaPajak_Awal_Terseleksi
    Dim MasaPajak_Akhir_Terseleksi
    Dim MasaPajak_Terseleksi
    Dim TahunPajak_Terseleksi
    Dim TanggalKetetapan_Terseleksi
    Dim NomorKetetapan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim JumlahKetetapan_Terseleksi
    Dim PokokPajak_Terseleksi
    Dim Sanksi_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayarPokok_Terseleksi
    Dim JumlahBayarSanksi_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaTagihanPokok_Terseleksi
    Dim SisaTagihanSanksi_Terseleksi
    Dim SisaTagihan_Terseleksi
    Dim Keterangan_Terseleksi
    Dim StatusLunas_Terseleksi
    Dim NomorJV_Terseleksi

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama
    Dim TahunPajakSebelumIni

    Dim TahunPenerbitanTerlama
    Dim TahunPenerbitanSebelumIni

    Dim SisaHutang_SaatCutOff

    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim PilihBayar
    Dim PilihBayar_Pokok = "Bayar Pokok"
    Dim PilihBayar_Sanksi = "Bayar Sanksi"

    Dim Pilihan_JenisPajak
    Dim Pilihan_StatusLunas

    Dim TahunPenerbitan

    Dim FilterData
    Dim FilterJenisPajak

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_LihatJurnal.Visible = False
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.Visible = True
            grb_InfoSaldo.Text = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        TahunPajak = TahunBukuAktif
        TahunPenerbitan = TahunBukuAktif
        KontenCombo_TahunPajak()
        KontenCombo_TahunPenerbitan()
        KontenCombo_PilihanJenisPajak()
        KontenCombo_PilihanStatusLunas()

        ProsesLoadingForm = False

        lbl_Warning_1.Visible = False
        lbl_Warning_2.Visible = False
        lbl_Warning_3.Visible = False
    End Sub

    Sub RefreshTampilanData()
        KontenCombo_PilihanJenisPajak()
    End Sub

    Sub TampilkanData()

        If cmb_PilihanJenisPajak.Text = Kosongan Then Return
        If cmb_PilihanStatusLunas.Text = Kosongan Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Jenis Pajak :
        If Pilihan_JenisPajak = Pilihan_Semua Then
            FilterJenisPajak = Kosongan
        Else
            FilterJenisPajak = " AND Jenis_Pajak = '" & Pilihan_JenisPajak & "' "
        End If

        'Filter Data :
        FilterData = FilterJenisPajak

        'Data Tabel :
        NomorUrut = 0
        TotalTagihan_PokokPajak = 0
        TotalTagihan_Sanksi = 0
        TotalTagihan_JumlahKetetapan = 0
        TotalBayar_Pokok = 0
        TotalBayar_Sanksi = 0
        TotalBayar = 0
        Total_SisaTagihan = 0

        AksesDatabase_Transaksi(Buka)

        TahunBuku_Alternatif = TahunPajak 'Ini nanti diganti dengan 2 baris Coding yang di bawah ini :
        'If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then TahunBuku_Alternatif = TahunCutOff
        'If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then TahunBuku_Alternatif = TahunPajak

        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak WHERE Nomor_ID > 0 " & FilterData, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPHP = dr.Item("Nomor_BPHP")
            KodeAkun_PokokPajak = dr.Item("Kode_Akun_Pokok_Pajak")
            Nomor = dr.Item("Nomor")
            KodeJenisPajak = dr.Item("Kode_Jenis_Pajak")
            JenisPajak = dr.Item("Jenis_Pajak")
            MasaPajak_Awal = dr.Item("Masa_Pajak_Awal")
            MasaPajak_Akhir = dr.Item("Masa_Pajak_Akhir")
            If MasaPajak_Awal = MasaPajak_Akhir Then
                MasaPajak = MasaPajak_Awal
            Else
                MasaPajak = MasaPajak_Awal & " - " & MasaPajak_Akhir
            End If
            TahunPajak_Tabel = dr.Item("Tahun_Pajak")
            NomorKetetapan = dr.Item("Nomor_Ketetapan")
            TanggalKetetapan = TanggalFormatTampilan(dr.Item("Tanggal_Ketetapan"))
            JumlahKetetapan = dr.Item("Jumlah_Ketetapan")
            PokokPajak = dr.Item("Pokok_Pajak")
            Sanksi = dr.Item("Sanksi")
            JumlahTagihan = JumlahKetetapan

            'Data Pembayaran Pokok Pajak :
            JumlahBayarPokok = 0
            If PokokPajak > 0 Then
                Dim TahunTelusurPembayaran = TahunPajak
                Dim PencegahLoopingTahunPajakLampau = 0
                Do While TahunTelusurPembayaran <= TahunBukuAktif
                    If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
                    If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
                    If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                                   " WHERE Nomor_BP         = '" & NomorBPHP & "' " &
                                                   " AND Status_Invoice     = '" & Status_Dibayar & "' " &
                                                   " AND Jenis_Pajak        = '" & JenisPajak & "' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                        drBAYAR_ExecuteReader()
                        Do While drBAYAR.Read
                            JumlahBayarPokok += drBAYAR.Item("Jumlah_Bayar")
                            If JumlahBayarPokok >= PokokPajak Then Exit Do
                        Loop
                        TutupDatabaseTransaksi_Alternatif()
                    End If
                    If JumlahBayarPokok >= PokokPajak Then Exit Do
                    PencegahLoopingTahunPajakLampau += 1
                    TahunTelusurPembayaran += 1
                Loop
            End If

            'Data Pembayaran Sanksi Administrasi :
            JumlahBayarSanksi = 0
            If Sanksi > 0 Then
                Dim TahunTelusurPembayaran = TahunPajak
                Dim PencegahLoopingTahunPajakLampau = 0
                Do While TahunTelusurPembayaran <= TahunBukuAktif
                    If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
                    If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
                    If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                                   " WHERE Nomor_BP         = '" & NomorBPHP & "' " &
                                                   " AND Status_Invoice     = '" & Status_Dibayar & "' " &
                                                   " AND Jenis_Pajak        = '" & JenisPajak_KetetapanPajak & "' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                        drBAYAR_ExecuteReader()
                        Do While drBAYAR.Read
                            JumlahBayarSanksi += drBAYAR.Item("Jumlah_Bayar")
                            If JumlahBayarSanksi >= Sanksi Then Exit Do
                        Loop
                        TutupDatabaseTransaksi_Alternatif()
                    End If
                    If JumlahBayarSanksi >= Sanksi Then Exit Do
                    PencegahLoopingTahunPajakLampau += 1
                    TahunTelusurPembayaran += 1
                Loop
            End If

            'Jumlah Pembayaran :
            JumlahBayar = JumlahBayarPokok + JumlahBayarSanksi

            SisaTagihan = JumlahTagihan - JumlahBayar
            If SisaTagihan = 0 Then
                StatusLunas = StatusLunas_Lunas
            Else
                StatusLunas = StatusLunas_BelumLunas
            End If
            Keterangan = dr.Item("Keterangan")
            NomorJV = dr.Item("Nomor_JV")

            If Pilihan_StatusLunas = Pilihan_Semua Then
                TambahBaris()
            Else
                If Pilihan_StatusLunas = StatusLunas Then TambahBaris()
            End If

        Loop

        AksesDatabase_Transaksi(Tutup)

        If Total_SisaTagihan = 0 Then Total_SisaTagihan = StripKosong

        If AmbilAngka(Total_SisaTagihan) <= 0 Then
            StatusLunas = StatusLunas_LUNAS_
        Else
            StatusLunas = Kosongan
        End If

        'Baris Total :
        If NomorUrut > 0 Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, "   T O T A L  ",
                                    Kosongan, Kosongan, Kosongan,
                                    TotalTagihan_PokokPajak, TotalTagihan_Sanksi, TotalTagihan_JumlahKetetapan,
                                    TotalBayar_Pokok, TotalBayar_Sanksi, TotalBayar, Total_SisaTagihan,
                                    StatusLunas, Kosongan, Kosongan)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()

        TotalTagihan_PokokPajak += PokokPajak
        TotalTagihan_Sanksi += Sanksi
        TotalTagihan_JumlahKetetapan += JumlahKetetapan
        TotalBayar_Pokok += JumlahBayarPokok
        TotalBayar_Sanksi += JumlahBayarSanksi
        TotalBayar += JumlahBayar
        Total_SisaTagihan += SisaTagihan

        If StatusLunas = StatusLunas_BelumLunas Then StatusLunas = Kosongan

        If PokokPajak = 0 Then PokokPajak = StripKosong
        If Sanksi = 0 Then Sanksi = StripKosong
        If JumlahKetetapan = 0 Then JumlahKetetapan = StripKosong
        If SisaTagihan = 0 Then SisaTagihan = StripKosong
        If JumlahBayarPokok = 0 Then JumlahBayarPokok = StripKosong
        If JumlahBayarSanksi = 0 Then JumlahBayarSanksi = StripKosong
        If JumlahBayar = 0 Then JumlahBayar = StripKosong

        DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, KodeAkun_PokokPajak,
                                    Nomor, KodeJenisPajak, JenisPajak,
                                    MasaPajak_Awal, MasaPajak_Akhir, MasaPajak,
                                    TahunPajak_Tabel, NomorKetetapan, TanggalKetetapan,
                                    PokokPajak, Sanksi, JumlahKetetapan,
                                    JumlahBayarPokok, JumlahBayarSanksi, JumlahBayar, SisaTagihan,
                                    StatusLunas, Keterangan, NomorJV)

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        grb_Pembayaran.Visible = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_DetailPembayaran.Enabled = True
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_TahunPajak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.SelectedIndexChanged
    End Sub
    Private Sub cmb_TahunPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_TahunPajak_TextChanged(sender As Object, e As EventArgs) Handles cmb_TahunPajak.TextChanged

        TahunPajak = cmb_TahunPajak.Text
        TahunPajakSebelumIni = TahunPajak - 1

        If TahunPajak > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
        End If

        If TahunPajak = TahunBukuAktif Then
            TahunPajakSamaDenganTahunBukuAktif = True
        Else
            TahunPajakSamaDenganTahunBukuAktif = False
        End If

        TampilkanData()

    End Sub
    Sub KontenCombo_TahunPajak()

        'TahunHutangPajakTerlama = AmbilTahunTerlama("tbl_SisaHutangPPN", "Tanggal_Transaksi")
        TahunHutangPajakTerlama = TahunBukuAktif - 1 '(Sementara begini dulu, karena belum ditentukan sistemnya).
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.Text = TahunPajak

    End Sub

    Private Sub cmb_TahunPenerbitan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunPenerbitan.SelectedIndexChanged
    End Sub
    Private Sub cmb_TahunPenerbitan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunPenerbitan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_TahunPenerbitan_TextChanged(sender As Object, e As EventArgs) Handles cmb_TahunPenerbitan.TextChanged

        TahunPenerbitan = cmb_TahunPenerbitan.Text
        TahunPenerbitanSebelumIni = TahunPenerbitan - 1

        If TahunPenerbitan > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
        End If

        TampilkanData()

    End Sub
    Sub KontenCombo_TahunPenerbitan()

        'TahunHutangPajakTerlama = AmbilTahunTerlama("tbl_SisaHutangPPN", "Tanggal_Transaksi")
        TahunPenerbitanTerlama = TahunBukuAktif - 1 '(Sementara begini dulu, karena belum ditentukan sistemnya).
        Dim ListTahunPenerbitan = TahunBukuAktif

        cmb_TahunPenerbitan.Items.Clear()
        Do While ListTahunPenerbitan >= TahunPenerbitanTerlama
            cmb_TahunPenerbitan.Items.Add(ListTahunPenerbitan)
            ListTahunPenerbitan -= 1
        Loop
        cmb_TahunPenerbitan.Text = TahunPenerbitan

    End Sub

    Private Sub cmb_PilihanJenisPajak_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PilihanJenisPajak.SelectedIndexChanged
    End Sub
    Private Sub cmb_PilihanJenisPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PilihanJenisPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PilihanJenisPajak_TextChanged(sender As Object, e As EventArgs) Handles cmb_PilihanJenisPajak.TextChanged
        Pilihan_JenisPajak = cmb_PilihanJenisPajak.Text
        TampilkanData()
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
        cmb_PilihanJenisPajak.Text = Pilihan_Semua
    End Sub

    Private Sub cmb_PilihanStatusLunas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PilihanStatusLunas.SelectedIndexChanged
    End Sub
    Private Sub cmb_PilihanStatusLunas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PilihanStatusLunas.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PilihanStatusLunas_TextChanged(sender As Object, e As EventArgs) Handles cmb_PilihanStatusLunas.TextChanged
        Pilihan_StatusLunas = cmb_PilihanStatusLunas.Text
        TampilkanData()
    End Sub
    Sub KontenCombo_PilihanStatusLunas()
        cmb_PilihanStatusLunas.Items.Clear()
        cmb_PilihanStatusLunas.Items.Add(Pilihan_Semua)
        cmb_PilihanStatusLunas.Items.Add(StatusLunas_Lunas)
        cmb_PilihanStatusLunas.Items.Add(StatusLunas_BelumLunas)
        cmb_PilihanStatusLunas.SelectedValue = Pilihan_Semua
    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As EventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak_KetetapanPajak
        frm_DetailPembayaranPajak.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Dim JenisPajak_UntukFormInput
    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If SisaTagihan_Terseleksi <= 0 Then
            MsgBox("Tagihan ini sudah dibayar seluruhnya.")
            Return
        End If

        Dim SisaTagihan As Int64 = 0
        Dim JumlahBayar As Int64 = 0
        Select Case True
            Case rdb_BayarPokok.Checked
                If SisaTagihanPokok_Terseleksi <= 0 Then
                    MsgBox("Tagihan Pokok ini sudah dibayar seluruhnya.")
                    Return
                End If
                SisaTagihan = SisaTagihanPokok_Terseleksi
                JumlahBayar = JumlahBayarPokok_Terseleksi
                JenisPajak_UntukFormInput = JenisPajak_Terseleksi
            Case rdb_BayarSanksi.Checked
                If SisaTagihanSanksi_Terseleksi <= 0 Then
                    MsgBox("Tagihan Sanksi ini sudah dibayar seluruhnya.")
                    Return
                End If
                SisaTagihan = SisaTagihanSanksi_Terseleksi
                JumlahBayar = JumlahBayarSanksi_Terseleksi
                JenisPajak_UntukFormInput = JenisPajak_KetetapanPajak
        End Select

        'frm_InputPembayaranHutangPajak.ResetForm()
        'ProsesLoadingForm = True
        'frm_InputPembayaranHutangPajak.FungsiForm = FungsiForm_TAMBAH
        'frm_InputPembayaranHutangPajak.JenisPajak = JenisPajak_UntukFormInput
        'frm_InputPembayaranHutangPajak.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        'frm_InputPembayaranHutangPajak.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (JumlahBarisBayar + 1).ToString
        'IsiValueForm_InputPembayaran()
        'frm_InputPembayaranHutangPajak.txt_JumlahDibayar.Text = JumlahBayar
        'frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = SisaTagihan
        'ProsesLoadingForm = False
        'frm_InputPembayaranHutangPajak.ShowDialog()
        'If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

        PesanUntukProgrammer("CODING belum dibikin...!!!" & Enter2Baris & "INGAT...!!! Ada Pokok dan Sanksi...!!!")

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        Select Case True
            Case rdb_BayarPokok.Checked
                JenisPajak_UntukFormInput = JenisPajak_Terseleksi
            Case rdb_BayarSanksi.CheckAlign
                JenisPajak_UntukFormInput = JenisPajak_KetetapanPajak
        End Select

        frm_InputPembayaranHutangPajak.ResetForm()
        frm_InputPembayaranHutangPajak.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangPajak.JenisPajak = JenisPajak_UntukFormInput
        frm_InputPembayaranHutangPajak.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangPajak.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangPajak.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NomorIdBayar = NomorIdPembayaran_Terseleksi
        frm_InputPembayaranHutangPajak.NPPHP = Referensi_Terseleksi
        IsiValueForm_InputPembayaran()
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe = BarisKe + 1
        Loop
        frm_InputPembayaranHutangPajak.txt_JumlahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangPajak.txt_SisaHutang.Text = JumlahTerutang - HitungBayar
        frm_InputPembayaranHutangPajak.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranHutangPajak.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangPajak.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangPajak.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then RefreshTampilanData()

    End Sub

    Dim JumlahTerutang
    Sub IsiValueForm_InputPembayaran()
        frm_InputPembayaranHutangPajak.txt_NomorBPHP.Text = NomorBPHP_Terseleksi
        frm_InputPembayaranHutangPajak.txt_MasaPajak.Text = MasaPajak_Terseleksi
        If rdb_BayarPokok.Checked = True Then JumlahTerutang = PokokPajak_Terseleksi
        If rdb_BayarSanksi.Checked = True Then JumlahTerutang = Sanksi_Terseleksi
        frm_InputPembayaranHutangPajak.txt_JumlahTerutang.Text = JumlahTerutang
        frm_InputPembayaranHutangPajak.KodeSetoran = Kosongan
    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranHutangPajak :
        If StatusKoneksiDatabaseTransaksi = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        'Hapus Data di tbl_PengajuanPembayaranHutangPajak :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangPajak " &
                                  " WHERE Nomor_Pengajuan = '" & Referensi_Terseleksi & "' " &
                                  " AND Nomor_BPHP = '" & NomorBPHP_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        'Hapus Data di tbl_Transaksi (Jurnal) :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                  " WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub btn_Input_Click(sender As Object, e As EventArgs) Handles btn_Input.Click

        frm_InputKetetapanPajak.ResetForm()
        frm_InputKetetapanPajak.FungsiForm = FungsiForm_TAMBAH
        frm_InputKetetapanPajak.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        If JumlahBayar_Terseleksi > 0 Then
            MsgBox("Nomor Ketetapan ini tidak dapat diedit, karena sudah ada data pembayaran." & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu seluruh data pembayaran yang terkait dengan Nomor Ketetapan ini.")
            Return
        End If

        frm_InputKetetapanPajak.ResetForm()
        frm_InputKetetapanPajak.FungsiForm = FungsiForm_EDIT
        frm_InputKetetapanPajak.NomorID = NomorID_Terseleksi
        frm_InputKetetapanPajak.txt_Nomor.Text = Nomor_Terseleksi
        frm_InputKetetapanPajak.txt_KodeJenisPajak.Text = KodeJenisPajak_Terseleksi
        frm_InputKetetapanPajak.txt_JenisPajak.Text = JenisPajak_Terseleksi
        frm_InputKetetapanPajak.cmb_MasaPajak_Awal.Text = MasaPajak_Awal_Terseleksi
        frm_InputKetetapanPajak.cmb_MasaPajak_Akhir.Text = MasaPajak_Akhir_Terseleksi
        frm_InputKetetapanPajak.txt_TahunPajak_Inputan.Text = TahunPajak_Terseleksi
        frm_InputKetetapanPajak.txt_NomorKetetapan.Text = NomorKetetapan_Terseleksi
        frm_InputKetetapanPajak.NomorBPHP = NomorBPHP_Terseleksi
        frm_InputKetetapanPajak.dtp_TanggalKetetapan.Value = TanggalKetetapan_Terseleksi
        frm_InputKetetapanPajak.txt_KodeAkun_PokokPajak.Text = KodeAkun_PokokPajak_Terseleksi
        frm_InputKetetapanPajak.txt_PokokPajak.Text = PokokPajak_Terseleksi
        frm_InputKetetapanPajak.txt_Sanksi.Text = Sanksi_Terseleksi
        frm_InputKetetapanPajak.txt_JumlahKetetapan.Text = JumlahKetetapan_Terseleksi
        frm_InputKetetapanPajak.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputKetetapanPajak.NomorJV = NomorJV_Terseleksi
        frm_InputKetetapanPajak.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        If JumlahBayar_Terseleksi > 0 Then
            MsgBox("Nomor Ketetapan ini tidak dapat dihapus, karena sudah ada data pembayaran." & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu seluruh data pembayaran yang terkait dengan Nomor Ketetapan ini.")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

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

    Private Sub rdb_BayarPokok_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_BayarPokok.CheckedChanged
        LogikaPilihBayar()
    End Sub

    Private Sub rdb_BayarSanksi_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_BayarSanksi.CheckedChanged
        LogikaPilihBayar()
    End Sub

    Sub LogikaPilihBayar()
        If rdb_BayarPokok.Checked = True Then PilihBayar = PilihBayar_Pokok
        If rdb_BayarSanksi.Checked = True Then PilihBayar = PilihBayar_Sanksi
        If rdb_BayarPokok.Checked Or rdb_BayarSanksi.Checked Then
            TampilkanDataPembayaran()
        Else
            dgv_DetailBayar.Enabled = False
            PilihBayar = Kosongan
        End If
    End Sub

    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_HapusPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Terseleksi = 0
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        Referensi_Terseleksi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        NomorJV_Terseleksi = 0
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_HapusPembayaran.Enabled = True
            btn_EditPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        Else
            btn_HapusPembayaran.Enabled = False
            btn_EditPembayaran.Enabled = False
            btn_LihatJurnal.Enabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    Sub TampilkanInfoSaldo()
        If TahunPajak = TahunBukuAktif Then
            grb_InfoSaldo.Visible = True
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_SaldoAwalBerdasarkanCOA.Visible = True
                txt_SaldoAwalBerdasarkanCOA.Visible = True
                lbl_AJP.Visible = True
                txt_AJP.Visible = True
            Else
                lbl_SaldoAwalBerdasarkanCOA.Visible = False
                txt_SaldoAwalBerdasarkanCOA.Visible = False
                lbl_AJP.Visible = False
                txt_AJP.Visible = False
            End If
        Else
            grb_InfoSaldo.Visible = False
            lbl_SaldoAwalBerdasarkanCOA.Visible = False
            txt_SaldoAwalBerdasarkanCOA.Visible = False
            lbl_AJP.Visible = False
            txt_AJP.Visible = False
        End If
    End Sub

    Sub ResetTampilanDataPembayaran()
        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False
        dgv_DetailBayar.Enabled = False
        dgv_DetailBayar.Rows.Clear()
        PilihBayar = Kosongan
        btn_InputPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
        rdb_BayarPokok.Checked = False
        rdb_BayarSanksi.Checked = False
    End Sub

    Sub TampilkanDataPembayaran()

        dgv_DetailBayar.Enabled = True
        btn_InputPembayaran.Enabled = True

        dgv_DetailBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar = 0
        Dim TotalBayar = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran
        Dim JenisPajak_UntukPenelusuran = Kosongan

        If rdb_BayarPokok.Checked = True Then JenisPajak_UntukPenelusuran = JenisPajak_Terseleksi
        If rdb_BayarSanksi.Checked = True Then JenisPajak_UntukPenelusuran = JenisPajak_KetetapanPajak

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunBuku_Alternatif = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunBuku_Alternatif = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP          = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Jenis_Pajak         = '" & JenisPajak_UntukPenelusuran & "' " &
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
                    dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJV_Pembayaran)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            dgv_DetailBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
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

        dgv_DetailBayar.ClearSelection()
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        NomorUrut_Terseleksi = 0
        NomorID_Terseleksi = 0
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", Baris_Terseleksi).Value)
        KodeAkun_PokokPajak_Terseleksi = DataTabelUtama.Item("Kode_Akun_Pokok_Pajak", Baris_Terseleksi).Value
        Nomor_Terseleksi = DataTabelUtama.Item("Nomor_", Baris_Terseleksi).Value
        KodeJenisPajak_Terseleksi = DataTabelUtama.Item("Kode_Jenis_Pajak", Baris_Terseleksi).Value
        JenisPajak_Terseleksi = DataTabelUtama.Item("Jenis_Pajak", Baris_Terseleksi).Value
        MasaPajak_Awal_Terseleksi = DataTabelUtama.Item("Masa_Pajak_Awal", Baris_Terseleksi).Value
        MasaPajak_Akhir_Terseleksi = DataTabelUtama.Item("Masa_Pajak_Akhir", Baris_Terseleksi).Value
        MasaPajak_Terseleksi = DataTabelUtama.Item("Masa_Pajak", Baris_Terseleksi).Value
        TahunPajak_Terseleksi = DataTabelUtama.Item("Tahun_Pajak", Baris_Terseleksi).Value
        TanggalKetetapan_Terseleksi = DataTabelUtama.Item("Tanggal_Ketetapan", Baris_Terseleksi).Value
        NomorKetetapan_Terseleksi = DataTabelUtama.Item("Nomor_Ketetapan", Baris_Terseleksi).Value
        NomorBPHP_Terseleksi = DataTabelUtama.Item("Nomor_BPHP", Baris_Terseleksi).Value
        JumlahKetetapan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Ketetapan", Baris_Terseleksi).Value)
        PokokPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Pokok_Pajak", Baris_Terseleksi).Value)
        Sanksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sanksi_", Baris_Terseleksi).Value)
        JumlahTagihan_Terseleksi = JumlahKetetapan_Terseleksi
        JumlahBayarPokok_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Pokok", Baris_Terseleksi).Value)
        JumlahBayarSanksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Sanksi", Baris_Terseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        SisaTagihanPokok_Terseleksi = PokokPajak_Terseleksi - JumlahBayarPokok_Terseleksi
        SisaTagihanSanksi_Terseleksi = Sanksi_Terseleksi - JumlahBayarSanksi_Terseleksi
        SisaTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Tagihan", Baris_Terseleksi).Value)
        StatusLunas_Terseleksi = DataTabelUtama.Item("Status_Lunas", Baris_Terseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value)

        If JumlahTagihan_Terseleksi > 0 Then
            ResetTampilanDataPembayaran()
            btn_LihatJurnal.Enabled = True
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
            rdb_BayarPokok.Enabled = False
            rdb_BayarSanksi.Enabled = False
            If PokokPajak_Terseleksi > 0 Then rdb_BayarPokok.Enabled = True
            If Sanksi_Terseleksi > 0 Then rdb_BayarSanksi.Enabled = True
        Else
            BersihkanSeleksi()
            TampilkanInfoSaldo()
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class