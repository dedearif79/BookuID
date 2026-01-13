Imports bcomm
Imports System.Data.Odbc
Imports System.Windows.Controls


Module wpfMdl_SaldoDanPenyesuaian


    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COA As String,
                                                                  ByRef SaldoAwal_BerdasarkanCOA As Int64,
                                                                  ByRef JumlahPenyesuaianSaldo As Int64,
                                                                  ByRef SaldoAwal_BerdasarkanCOA_PlusPenyesuaian As Int64,
                                                                  txt_SaldoAwalBerdasarkanCOA As TextBox,
                                                                  txt_AJP As TextBox,
                                                                  txt_saldoBerdasarkanCOA_PlusPenyesuaian As TextBox)
        SaldoAwal_BerdasarkanCOA = SaldoAwalTahunCOA(COA)
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        Dim JumlahPenyesuaian_DEBET = 0
        Dim JumlahPenyesuaian_KREDIT = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & COA & "' " &
                              " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            If dr.HasRows Then
                JumlahPenyesuaian_DEBET += dr.Item("Jumlah_Debet")
                JumlahPenyesuaian_KREDIT += dr.Item("Jumlah_Kredit")
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        JumlahPenyesuaianSaldo = JumlahPenyesuaian_KREDIT - JumlahPenyesuaian_DEBET
        SaldoAwal_BerdasarkanCOA_PlusPenyesuaian = SaldoAwal_BerdasarkanCOA + JumlahPenyesuaianSaldo
        txt_AJP.Text = JumlahPenyesuaianSaldo
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    End Sub


    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COA As String, ByRef SaldoAkhir_BerdasarkanCOA As Int64,
                                                   txt_saldoBerdasarkanCOA_PlusPenyesuaian As TextBox)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then SaldoAkhir_BerdasarkanCOA = SaldoAkhirTahunBukuLampauCOA(COA)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then SaldoAkhir_BerdasarkanCOA = SaldoAkhirCOA(COA)
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
    End Sub


    Sub CekKesesuaianSaldoAwal_Public(ByRef SaldoAwal_BerdasarkanList As Int64,
                                      ByRef SaldoAwal_BerdasarkanCOA_PlusPenyesuaian As Int64,
                                      ByRef KesesuaianSaldoAwal As Boolean,
                                      btn_Sesuaikan As Button,
                                      txt_SaldoBerdasarkanList As TextBox,
                                      txt_saldoBerdasarkanCOA_PlusPenyesuaian As TextBox,
                                      txt_SelisihSaldo As TextBox)
        If SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian Then
            KesesuaianSaldoAwal = True
            btn_Sesuaikan.IsEnabled = False
            txt_SaldoBerdasarkanList.Foreground = WarnaTeksStandar_WPF
            txt_saldoBerdasarkanCOA_PlusPenyesuaian.Foreground = WarnaTeksStandar_WPF
            txt_SelisihSaldo.Foreground = WarnaTeksStandar_WPF
        Else
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.IsEnabled = True
            txt_SaldoBerdasarkanList.Foreground = WarnaPeringatan_WPF
            txt_saldoBerdasarkanCOA_PlusPenyesuaian.Foreground = WarnaPeringatan_WPF
            txt_SelisihSaldo.Foreground = WarnaPeringatan_WPF
        End If
    End Sub


    Sub CekKesesuaianSaldoAkhir_Public(ByRef SaldoAkhir_BerdasarkanList As Int64,
                                       ByRef SaldoAkhir_BerdasarkanCOA As Int64,
                                       ByRef KesesuaianSaldoAkhir As Boolean,
                                       btn_Sesuaikan As Button,
                                       txt_SaldoBerdasarkanList As TextBox,
                                       txt_saldoBerdasarkanCOA_PlusPenyesuaian As TextBox,
                                       txt_SelisihSaldo As TextBox)
        If SaldoAkhir_BerdasarkanList = SaldoAkhir_BerdasarkanCOA Then
            KesesuaianSaldoAkhir = True
            btn_Sesuaikan.IsEnabled = False
            txt_SaldoBerdasarkanList.Foreground = WarnaTeksStandar_WPF
            txt_saldoBerdasarkanCOA_PlusPenyesuaian.Foreground = WarnaTeksStandar_WPF
            txt_SelisihSaldo.Foreground = WarnaTeksStandar_WPF
        Else
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.IsEnabled = True
            txt_SaldoBerdasarkanList.Foreground = WarnaPeringatan_WPF
            txt_saldoBerdasarkanCOA_PlusPenyesuaian.Foreground = WarnaPeringatan_WPF
            txt_SelisihSaldo.Foreground = WarnaPeringatan_WPF
        End If
    End Sub

    'Jurnal Adjusment : Tanpa Kiriman Value Tanggal
    Sub JurnalAdjusment_TanpaKirimTanggal(JalurMasuk As String, COADebet As String, COAKredit As String, JumlahPenyesuaian As Int64, JenisJurnal As String)

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            Dim NamaAkunDebet = AmbilValue_NamaAkun(COADebet)
            Dim NamaAkunKredit = AmbilValue_NamaAkun(COAKredit)
            win_InputJurnal = New wpfWin_InputJurnal
            win_InputJurnal.ResetForm()
            win_InputJurnal.JalurMasuk = JalurMasuk
            win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            win_InputJurnal.datatabelUtama.Rows.Clear() '(Ini jangan dihapus...!!!)
            win_InputJurnal.datatabelUtama.Rows.Add(1, COADebet, NamaAkunDebet, dk_D, JumlahPenyesuaian, 0)
            win_InputJurnal.datatabelUtama.Rows.Add(2, COAKredit, PenjorokNamaAkun & NamaAkunKredit, dk_K, 0, JumlahPenyesuaian)
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Debet") = JumlahPenyesuaian
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Kredit") = JumlahPenyesuaian
            win_InputJurnal.lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
            win_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            win_InputJurnal.dtp_TanggalJurnal.Text = Kosongan
            IsiValueComboBypassTerkunci(win_InputJurnal.cmb_JenisJurnal, JenisJurnal)
            win_InputJurnal.dtp_TanggalJurnal.IsEnabled = True
            win_InputJurnal.grb_Transaksi.IsEnabled = False
            win_InputJurnal.btn_Input.IsEnabled = False
            win_InputJurnal.btn_Reset.IsEnabled = False
            win_InputJurnal.btn_Simpan.IsEnabled = True
            win_InputJurnal.JumlahBarisJurnal = 2
            win_InputJurnal.ShowDialog()
        End If

    End Sub


    'Jurnal Adjusment : Dengan Value Tanggal
    Sub JurnalAdjusment(JalurMasuk As String, TanggalJurnal As Date, COADebet As String, COAKredit As String, JumlahPenyesuaian As Int64, JenisJurnal As String)

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            Dim NamaAkunDebet = AmbilValue_NamaAkun(COADebet)
            Dim NamaAkunKredit = AmbilValue_NamaAkun(COAKredit)
            win_InputJurnal = New wpfWin_InputJurnal
            win_InputJurnal.ResetForm()
            win_InputJurnal.JalurMasuk = JalurMasuk
            win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            win_InputJurnal.datatabelUtama.Rows.Clear() '(Ini jangan dihapus...!!!)
            win_InputJurnal.datatabelUtama.Rows.Add(1, COADebet, NamaAkunDebet, dk_D, JumlahPenyesuaian, 0)
            win_InputJurnal.datatabelUtama.Rows.Add(2, COAKredit, PenjorokNamaAkun & NamaAkunKredit, dk_K, 0, JumlahPenyesuaian)
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Debet") = JumlahPenyesuaian
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Kredit") = JumlahPenyesuaian
            win_InputJurnal.lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
            win_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            win_InputJurnal.dtp_TanggalJurnal.SelectedDate = TanggalFormatWPF(TanggalJurnal)
            win_InputJurnal.cmb_JenisJurnal.SelectedValue = JenisJurnal
            win_InputJurnal.dtp_TanggalJurnal.IsEnabled = True
            win_InputJurnal.cmb_JenisJurnal.IsEnabled = False
            win_InputJurnal.grb_Transaksi.IsEnabled = False
            win_InputJurnal.btn_Input.IsEnabled = False
            win_InputJurnal.btn_Reset.IsEnabled = False
            win_InputJurnal.btn_Simpan.IsEnabled = True
            win_InputJurnal.JumlahBarisJurnal = 2
            win_InputJurnal.ShowDialog()
        End If

    End Sub

    'Jurnal Adjusment : Khusus Saldo Awal
    Sub SesuaikanSaldoAwal(Halaman As String, COAKoreksi As String, ByRef SaldoAwal_BerdasarkanList As Int64, ByRef SaldoAwal_BerdasarkanCOA_PlusPenyesuaian As Int64)

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_PenyesuaianSelisihSaldoAkhir_UntukTahunBukuLampau()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            Dim NamaAkunKoreksi
            Dim NamaAkun_BiayaSelisihPencatatan
            Dim JumlahPenyesuaian = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            NamaAkunKoreksi = AmbilValue_NamaAkun(COAKoreksi)
            NamaAkun_BiayaSelisihPencatatan = AmbilValue_NamaAkun(KodeTautanCOA_BiayaSelisihPencatatan)
            win_InputJurnal = New wpfWin_InputJurnal
            win_InputJurnal.ResetForm()
            win_InputJurnal.JalurMasuk = Halaman
            win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
            win_InputJurnal.datatabelUtama.Rows.Clear() '(Ini jangan dihapus...!!!)
            If JumlahPenyesuaian > 0 Then
                win_InputJurnal.newRow = win_InputJurnal.datatabelUtama.NewRow()
                win_InputJurnal.datatabelUtama.Rows.Add(1, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           NamaAkun_BiayaSelisihPencatatan, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnal.datatabelUtama.Rows.Add(2, COAKoreksi,
                                                           PenjorokNamaAkun & NamaAkunKoreksi, dk_K, 0, JumlahPenyesuaian)
            End If
            If JumlahPenyesuaian < 0 Then
                JumlahPenyesuaian = -(JumlahPenyesuaian)
                win_InputJurnal.datatabelUtama.Rows.Add(1, COAKoreksi,
                                                           NamaAkunKoreksi, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnal.datatabelUtama.Rows.Add(2, KodeTautanCOA_BiayaSelisihPencatatan,
                                                           PenjorokNamaAkun & NamaAkun_BiayaSelisihPencatatan, dk_K, 0, JumlahPenyesuaian)
            End If
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows.Add()
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Debet") = JumlahPenyesuaian
            win_InputJurnal.datatabelUtama.Rows(3)("Jumlah_Kredit") = JumlahPenyesuaian
            win_InputJurnal.lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
            win_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            win_InputJurnal.dtp_TanggalJurnal.SelectedDate = AwalTahunBukuAktif
            win_InputJurnal.cmb_JenisJurnal.SelectedValue = JenisJurnal_AdjusmentSaldoAwal
            win_InputJurnal.dtp_TanggalJurnal.IsEnabled = False
            win_InputJurnal.cmb_JenisJurnal.IsEnabled = False
            win_InputJurnal.grb_Transaksi.IsEnabled = False
            win_InputJurnal.btn_Reset.IsEnabled = False
            win_InputJurnal.btn_Simpan.IsEnabled = True
            win_InputJurnal.JumlahBarisJurnal = 2
            win_InputJurnal.ShowDialog()
            If win_InputJurnal.JurnalTersimpan = True Then
                UpdateNotifikasi_Eksekusi(Halaman)
            End If
        End If

    End Sub


    Sub AmbilValue_JumlahBayarPajakTahunBukuKemarin_Public(AwalanBPHP As String, KodeSetoran As String, ByRef JumlahBayar_SA As Int64)
        Dim TahunTelusurPembayaran = TahunCutOff
        Do While TahunTelusurPembayaran <= TahunBukuKemarin
            BukaDatabaseTransaksi_Alternatif(TahunTelusurPembayaran)
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP  LIKE '" & AwalanBPHP & "%' " &
                                       " AND Kode_Setoran   = '" & KodeSetoran & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi_Alternatif)
            drBAYAR_ExecuteReader()
            JumlahBayar_SA = 0
            Do While drBAYAR.Read
                JumlahBayar_SA += FormatUlangInt64(drBAYAR.Item("Jumlah_Bayar"))
            Loop
            TutupDatabaseTransaksi_Alternatif()
            TahunTelusurPembayaran += 1
        Loop
        'Penjelasan :
        'Ini adalah algoritma untuk mendapatkan value Total Bayar Pajak TahunBukuKemarin (1 tahun sebelum TahunBukuAktif)
        'Kepentingannya, salah satunya untuk mendapatkan value Saldo Akhir Hutang Pajak TahunKemarin,
        'atau Saldo Awal Hutang Pajak TahunBukuAktif, yang algoritmanya terletak di tempat lain.
    End Sub


    Sub AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBPHP As String, KodeSetoran As String, ByRef TotalBayar As Int64)
        AksesDatabase_Transaksi(Buka)
        cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP                      LIKE '" & AwalanBPHP & "%' " &
                                   " AND Kode_Setoran                       = '" & KodeSetoran & "' " &
                                   " AND DATE_FORMAT(Tanggal_Invoice, '%Y') < '" & TahunBukuAktif & "' " &
                                   " AND Status_Invoice                     = '" & Status_Dibayar & "' ",
                                   KoneksiDatabaseTransaksi)
        drBAYAR_ExecuteReader()
        TotalBayar = 0
        Do While drBAYAR.Read
            TotalBayar += drBAYAR.Item("Jumlah_Bayar")
        Loop
        AksesDatabase_Transaksi(Tutup)
        'Penjelasan :
        'Ini adalah algoritma untuk mendapatkan value Total Bayar atas Hutang Pajak tahun sebelum ini (sebelum TahunBukuAktif),
        'tapi dibayarkannya pada tahun ini (Tahun Buku Aktif).
    End Sub


    Sub SisaHutangPajak_SaatCutOff_Public(ByRef SisaHutang_SaatCutOff As Int64, AwalanBPHP As String, JenisPajak As String, KodeSetoran As String)

        AksesDatabase_Transaksi(Buka)

        'Hitung Total Tagihan Selama Sebelum Cut Off :
        Dim TotalTagihan_SelamaSebelumCutOff As Int64 = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                              " WHERE Jenis_Pajak   = '" & JenisPajak & "' " &
                              " AND Kode_Setoran    = '" & KodeSetoran & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalTagihan_SelamaSebelumCutOff += dr.Item("Jumlah_Hutang")
        Loop

        'Hitung Total Pembayaran Selama Sebelum Cut Off :
        Dim TotalBayar_SelamaSebelumCutOff As Int64 = 0
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP   LIKE '" & AwalanBPHP & "%' " &
                              " AND Kode_Setoran    = '" & KodeSetoran & "' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            TotalBayar_SelamaSebelumCutOff += dr.Item("Jumlah_Bayar")
        Loop

        AksesDatabase_Transaksi(Tutup)

        SisaHutang_SaatCutOff = TotalTagihan_SelamaSebelumCutOff - TotalBayar_SelamaSebelumCutOff

    End Sub


    Sub UpdateNotifikasi_Eksekusi(Halaman As String)
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1, " &
                              " Status_Dieksekusi = 1 " &
                              " WHERE Halaman_Target = '" & Halaman & "' " &
                              " AND Pesan = '" & teks_SilakanSesuaikanSaldo & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        win_BOOKU.IsiKontenNotifikasi()
    End Sub



End Module
