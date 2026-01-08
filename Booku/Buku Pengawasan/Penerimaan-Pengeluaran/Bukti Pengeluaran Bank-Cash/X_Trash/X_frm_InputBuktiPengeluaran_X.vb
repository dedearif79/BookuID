Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_InputBuktiPengeluaran_X

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Dim QueryTampilan

    Public Tahapan

    Public NomorID
    Dim Kategori
    Dim Peruntukan
    Public AngkaKK
    Dim NomorKK
    Dim TanggalKK
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim COADebet
    Dim SaranaPembayaran
    Dim COAKredit
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim JumlahTransfer
    Dim TotalBank

    Public NomorUrutInvoice
    Dim NomorInvoicePerBaris
    Dim TanggalInvoicePerBaris
    Dim UraianInvoicePerBaris
    Public NomorBP
    Dim JumlahTagihanPerBaris
    Dim AngsuranKe
    Dim PokokPerBaris
    Dim BagiHasilPerBaris
    Dim SudahDibayarPerBaris
    Dim SisaTagihanPerBaris
    Dim JumlahPengajuanBayarPerBaris
    Dim JumlahBayarPerBaris
    Dim JenisPajakPerBaris
    Dim KodeSetoranPerBaris
    Dim PPhTerutangPerBaris
    Dim PPhDitanggungPerBaris
    Dim PPhDipotongPerBaris

    Dim BarisTerseleksi
    Dim NomorUrutInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim UraianInvoice_Terseleksi
    Dim NomorBP_Terseleksi
    Dim JumlahTagihanPerBaris_Terseleksi
    Dim SudahDibayarPerBaris_Terseleksi
    Dim SisaTagihanPerBaris_Terseleksi
    Dim JumlahPengajuanBayarPerBaris_Terseleksi
    Dim JumlahBayarPerBaris_Terseleksi

    Dim JumlahTagihan
    Dim Pokok
    Dim BagiHasil
    Dim Denda
    Dim SudahDibayar
    Dim SisaTagihan
    Dim JumlahPengajuanBayar
    Dim JumlahBayar
    Dim TanggalBayar
    Dim RekeningPenerima
    Dim AtasNamaPenerima
    Public Status
    Dim PPhTerutang
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim Catatan

    Dim JumlahInvoice
    Dim NomorInvoice_Bundel
    Dim TanggalInvoice_Bundel

    Dim PPhTerutang_Pasal21_100
    Dim PPhTerutang_Pasal21_401
    Dim PPhTerutang_Pasal23_100
    Dim PPhTerutang_Pasal23_101
    Dim PPhTerutang_Pasal23_102
    Dim PPhTerutang_Pasal23_103
    Dim PPhTerutang_Pasal23_104
    Dim PPhTerutang_Pasal42_402
    Dim PPhTerutang_Pasal42_403
    Dim PPhTerutang_Pasal42_409
    Dim PPhTerutang_Pasal42_419
    Dim PPhTerutang_Pasal26_100
    Dim PPhTerutang_Pasal26_101
    Dim PPhTerutang_Pasal26_102
    Dim PPhTerutang_Pasal26_103
    Dim PPhTerutang_Pasal26_104
    Dim PPhTerutang_Pasal26_105
    Dim PPhDitanggung_Pasal21
    Dim PPhDitanggung_Pasal23
    Dim PPhDitanggung_Pasal26
    Dim PPhDitanggung_Pasal42
    Dim PPhDipotong_Total

    Dim KodePajak

    Public NomorJV_Sebelumnya
    Public NomorJV

    Public PembayaranViaBank As Boolean
    Public PembayaranDenganInvoice As Boolean
    Public PembayaranTerjadwal As Boolean
    Public JumlahAngsuranTerjadwal
    Dim TabelJadwalAngsuran
    Dim KolomBPHJadwalAngsuran

    Public JenisPajak
    Public KodeSetoran

    Dim BiayaProvisi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Pengajuan Pengeluaran Bank-Cash"
            SistemPenomoranOtomatis_KK()
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_PROSES _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            JudulForm = "Edit Pengajuan Pengeluaran Bank-Cash"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran WHERE Angka_KK = '" & AngkaKK & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            NomorUrutInvoice = 0
            Do While dr.Read
                'Data Form :
                JenisPajakPerBaris = dr.Item("Jenis_Pajak")     '2 baris ini disimpan paling awal untuk melancarkan penentuan COA Pajak
                KodeSetoranPerBaris = dr.Item("Kode_Setoran")   '2 baris ini disimpan paling awal untuk melancarkan penentuan COA Pajak
                cmb_Peruntukan.Text = dr.Item("Peruntukan")
                txt_NomorKK.Text = dr.Item("Nomor_KK")
                dtp_TanggalKK.Value = dr.Item("Tanggal_KK")
                txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
                txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
                cmb_SaranaPembayaran.Text = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
                txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
                cmb_DitanggungOleh.Text = dr.Item("Ditanggung_Oleh")
                TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar")) 'Ini biarkan jangan pakai dtp_...!!!!
                txt_Catatan.Text = dr.Item("Catatan")
                'Data Tabel :
                NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
                TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                UraianInvoicePerBaris = dr.Item("Uraian_Invoice")
                NomorBP = dr.Item("Nomor_BP")
                JumlahTagihanPerBaris = dr.Item("Jumlah_Tagihan")
                AngsuranKe = dr.Item("Angsuran_Ke")
                PokokPerBaris = dr.Item("Pokok")
                BagiHasilPerBaris = dr.Item("Bagi_Hasil")
                Perhitungan_JumlahBayarPerBaris()
                JumlahPengajuanBayarPerBaris = dr.Item("Jumlah_Pengajuan")
                Status = dr.Item("Status")
                If COATermasukBank(SaranaPembayaran) Then
                    If Peruntukan = Peruntukan_BankGaransi Then
                        txt_RekeningPenerima.Text = Kosongan
                        txt_AtasNamaPenerima.Text = Kosongan
                    Else
                        txt_RekeningPenerima.Text = AmbilValue_RekeningMitra(KodeLawanTransaksi)
                        txt_AtasNamaPenerima.Text = AmbilValue_AtasNamaRekeningMitra(KodeLawanTransaksi)
                    End If
                    If Kategori = Kategori_Pemindahbukuan Then
                        If COATermasukBank(COADebet) = True Then txt_RekeningPenerima.Text = AmbilValue_NamaAkun(COADebet)
                    End If
                End If
                Select Case FungsiForm
                    Case FungsiForm_EDIT
                        JumlahBayarPerBaris = 0
                    Case FungsiForm_PROSES
                        If Status = Status_Diproses Then
                            JumlahBayarPerBaris = JumlahPengajuanBayarPerBaris
                            dtp_TanggalBayar.Value = Today
                        Else
                            'Ini maksudnya dalam rangka ngedit data yang sudah disetujui :
                            JumlahBayarPerBaris = dr.Item("Jumlah_Bayar")
                            SudahDibayarPerBaris -= JumlahBayarPerBaris
                            dtp_TanggalBayar.Value = TanggalBayar
                            NomorJV_Sebelumnya = dr.Item("Nomor_JV")
                            txt_RekeningPenerima.Text = dr.Item("Rekening_Penerima")
                            txt_AtasNamaPenerima.Text = dr.Item("Atas_Nama_Penerima")
                        End If
                    Case FungsiForm_LIHAT
                        JumlahBayarPerBaris = dr.Item("Jumlah_Bayar")
                        SudahDibayarPerBaris -= JumlahBayarPerBaris
                        dtp_TanggalBayar.Value = TanggalBayar
                        txt_RekeningPenerima.Text = dr.Item("Rekening_Penerima")
                        txt_AtasNamaPenerima.Text = dr.Item("Atas_Nama_Penerima")
                End Select
                SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
                PPhTerutangPerBaris = dr.Item("PPh_Terutang")
                PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
                txt_Denda.Text = dr.Item("Denda")
                If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                    txt_KodePajak.Text = dr.Item("Nomor_Invoice")
                    If FungsiForm = FungsiForm_PROSES Then
                        If Status = Status_Diproses Then
                            txt_KodePajak.Text = Kosongan
                        End If
                    End If
                    If FungsiForm = FungsiForm_LIHAT Then
                        If Status = Status_Dibayar Then
                            lbl_KodePajak.Text = "Kode NTPN"
                        Else
                            lbl_KodePajak.Text = "Kode Billing"
                        End If
                    End If
                End If
                TambahBaris()
            Loop
            AksesDatabase_Transaksi(Tutup)
            Perhitungan()
            StyleTabelUtama(DataTabelUtama)
        End If

        If FungsiForm = FungsiForm_PROSES _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            JudulForm = "Persetujuan Pengajuan Pengeluaran Bank-Cash"
            cmb_Kategori.Enabled = False
            cmb_Peruntukan.Enabled = False
            txt_NomorKK.Enabled = False
            dtp_TanggalKK.Enabled = False
            txt_KodeLawanTransaksi.Enabled = False
            btn_PilihMitra.Enabled = False
            txt_NamaLawanTransaksi.Enabled = False
            cmb_SaranaPembayaran.Enabled = False
            If COATermasukBank(SaranaPembayaran) Then
                grb_Bank.Visible = True
            Else
                Reset_grb_Bank()
                grb_Bank.Visible = False
            End If
            btn_Singkirkan.Enabled = False
            DataTabelUtama.Columns("Jumlah_Bayar_Per_Baris").Visible = True
            lbl_TanggalBayar.Enabled = True
            dtp_TanggalBayar.Enabled = True
            lbl_Rekening.Enabled = True
            txt_RekeningPenerima.Enabled = True
            lbl_AtasNama.Enabled = True
            txt_AtasNamaPenerima.Enabled = True
            btn_Simpan.Text = teks_Posting
            Me.Width = 949
        Else
            txt_NomorKK.Enabled = True
            If Kategori <> Kategori_Pemindahbukuan Then
                cmb_Kategori.Enabled = True
                cmb_Peruntukan.Enabled = True
                dtp_TanggalKK.Enabled = True
                cmb_SaranaPembayaran.Enabled = True
            End If
            txt_KodeLawanTransaksi.Enabled = True
            btn_PilihMitra.Enabled = True
            txt_NamaLawanTransaksi.Enabled = True
            grb_Bank.Visible = False
            btn_Singkirkan.Enabled = True
            DataTabelUtama.Columns("Jumlah_Bayar_Per_Baris").Visible = False
            lbl_TanggalBayar.Enabled = False
            dtp_TanggalBayar.Enabled = False
            lbl_Rekening.Enabled = False
            txt_RekeningPenerima.Enabled = False
            lbl_AtasNama.Enabled = False
            txt_AtasNamaPenerima.Enabled = False
            btn_Simpan.Text = teks_Simpan
            Me.Width = 868
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            JudulForm = "Bukti Pengeluaran Bank-Cash"
            grb_Bank.Enabled = False
            lbl_Denda.Enabled = False
            txt_Denda.Enabled = False
            lbl_KodePajak.Enabled = False
            txt_KodePajak.Enabled = False
            txt_Catatan.Enabled = False
            dtp_TanggalBayar.Enabled = False
            btn_Pratinjau.Enabled = True
            btn_Simpan.Enabled = False
            btn_Batal.Text = teks_Tutup
            If Status = Status_Dibayar Then
                DataTabelUtama.Columns("Jumlah_Bayar_Per_Baris").Visible = True
                Me.Width = 949
            Else
                DataTabelUtama.Columns("Jumlah_Bayar_Per_Baris").Visible = False
                Me.Width = 868
            End If
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Me.Text = JudulForm

        LogikaTampilanKolom()

        ProsesLoadingForm = False

        BeginInvoke(Sub() BersihkanSeleksi())

    End Sub

    Sub LogikaTampilanKolom()
        If PembayaranTerjadwal Then
            DataTabelUtama.Columns("Uraian_Invoice_Per_Baris").Visible = False
            DataTabelUtama.Columns("Jumlah_Tagihan_Per_Baris").Visible = False
            DataTabelUtama.Columns("Pokok_Per_Baris").Visible = True
            DataTabelUtama.Columns("Bagi_Hasil_Per_Baris").Visible = True
            DataTabelUtama.Columns("Sudah_Dibayar_Per_Baris").Visible = False
            DataTabelUtama.Columns("Sisa_Tagihan_Per_Baris").Visible = False
            If PPhTerutang > 0 Then
                DataTabelUtama.Columns("Jenis_Pajak_Per_Baris").Visible = True
                DataTabelUtama.Columns("PPh_Terutang_Per_Baris").Visible = True
                DataTabelUtama.Columns("PPh_Ditanggung_Per_Baris").Visible = True
                DataTabelUtama.Columns("PPh_Dipotong_Per_Baris").Visible = True
            Else
                DataTabelUtama.Columns("Jenis_Pajak_Per_Baris").Visible = False
                DataTabelUtama.Columns("PPh_Terutang_Per_Baris").Visible = False
                DataTabelUtama.Columns("PPh_Ditanggung_Per_Baris").Visible = False
                DataTabelUtama.Columns("PPh_Dipotong_Per_Baris").Visible = False
            End If
        Else
            If PembayaranDenganInvoice = True Then
                DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").Visible = True
                DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").Visible = True
            Else
                DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").Visible = False
                DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").Visible = False
            End If
            DataTabelUtama.Columns("Uraian_Invoice_Per_Baris").Visible = True
            DataTabelUtama.Columns("Jumlah_Tagihan_Per_Baris").Visible = True
            DataTabelUtama.Columns("Pokok_Per_Baris").Visible = False
            DataTabelUtama.Columns("Bagi_Hasil_Per_Baris").Visible = False
            DataTabelUtama.Columns("Sudah_Dibayar_Per_Baris").Visible = True
            DataTabelUtama.Columns("Sisa_Tagihan_Per_Baris").Visible = True
            DataTabelUtama.Columns("Jenis_Pajak_Per_Baris").Visible = False
            DataTabelUtama.Columns("PPh_Terutang_Per_Baris").Visible = False
            DataTabelUtama.Columns("PPh_Ditanggung_Per_Baris").Visible = False
            DataTabelUtama.Columns("PPh_Dipotong_Per_Baris").Visible = False
        End If
        If Kategori = Kategori_PengeluaranTunai _
            Or Kategori = Kategori_Pemindahbukuan _
            Or Kategori = Kategori_Investasi _
            Then
            DataTabelUtama.Columns("Jumlah_Tagihan_Per_Baris").Visible = False
            DataTabelUtama.Columns("Pokok_Per_Baris").Visible = False
            DataTabelUtama.Columns("Bagi_Hasil_Per_Baris").Visible = False
            DataTabelUtama.Columns("Sudah_Dibayar_Per_Baris").Visible = False
            DataTabelUtama.Columns("Sisa_Tagihan_Per_Baris").Visible = False
            If Peruntukan = Peruntukan_DepositOperasional Then
                DataTabelUtama.Columns("Jumlah_Tagihan_Per_Baris").Visible = True
                DataTabelUtama.Columns("Sudah_Dibayar_Per_Baris").Visible = True
                DataTabelUtama.Columns("Sisa_Tagihan_Per_Baris").Visible = True
            End If
        End If
    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan

        KontenCombo_Kategori()
        cmb_Peruntukan.Items.Clear()    'Jangan dihapus..!
        cmb_Peruntukan.Text = Kosongan  'Jangan dihapus..!
        NomorUrutInvoice = 0
        txt_NomorKK.Text = Kosongan
        dtp_TanggalKK.Value = Today
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        KontenComboSaranaPembayaran_Public(cmb_SaranaPembayaran)
        COADebet = Kosongan
        COAKredit = Kosongan
        Reset_grb_Bank()
        KosongkanDaftarTagihan()
        txt_Denda.Text = Kosongan
        dtp_TanggalBayar.Value = Today
        txt_RekeningPenerima.Text = Kosongan
        txt_AtasNamaPenerima.Text = Kosongan
        Status = Kosongan
        txt_Catatan.Text = Kosongan

        LogikaTombol_Pratinjau()

        cmb_Peruntukan.Enabled = True
        txt_NomorKK.Enabled = True
        dtp_TanggalKK.Enabled = True
        txt_KodeLawanTransaksi.Enabled = True
        btn_PilihMitra.Enabled = True
        txt_NamaLawanTransaksi.Enabled = True
        cmb_SaranaPembayaran.Enabled = True
        lbl_Denda.Visible = False
        txt_Denda.Visible = False
        lbl_KodePajak.Visible = False
        txt_KodePajak.Visible = False
        lbl_Denda.Enabled = True
        txt_Denda.Enabled = True
        lbl_KodePajak.Enabled = True
        txt_KodePajak.Enabled = True
        txt_KodePajak.Text = Kosongan
        DataTabelUtama.ColumnHeadersVisible = False
        btn_Simpan.Enabled = True
        btn_Simpan.Text = teks_Simpan
        btn_Pratinjau.Enabled = False
        btn_Batal.Text = teks_Batal

        NomorInvoice_Bundel = Kosongan
        TanggalInvoice_Bundel = Kosongan

        NomorID = 0
        NomorJV = 0

        PembayaranViaBank = False
        PembayaranDenganInvoice = True
        PembayaranTerjadwal = False
        JumlahAngsuranTerjadwal = 0

        JenisPajak = Kosongan
        KodeSetoran = Kosongan

        BiayaProvisi = 0

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_KK()

        If FungsiForm = FungsiForm_TAMBAH Then AngkaKK = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_BuktiPengeluaran", "Angka_KK") + 1
        NomorKK = AwalanKK & AngkaKK & "/" & KonversiSaranaPembayaranKeNamaAkun(SaranaPembayaran) & "/" &
                    KonversiAngkaKeStringDuaDigit(dtp_TanggalKK.Value.Month) & "/" & TahunBukuAktif
        txt_NomorKK.Text = NomorKK

    End Sub


    Sub KodePembukaDataTagihan()
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)
        NomorUrutInvoice = 0
        JumlahBayarPerBaris = 0
        JenisPajakPerBaris = Kosongan
        KodeSetoranPerBaris = Kosongan
        PPhTerutangPerBaris = 0
        PPhDitanggungPerBaris = 0
        PPhDipotongPerBaris = 0
        AksesDatabase_Transaksi(Buka)
    End Sub

    Sub KodePenutupDataTagihan()
        AksesDatabase_Transaksi(Tutup)
        Perhitungan()
        BersihkanSeleksi()
    End Sub

    Sub TambahkanDataTagihanHutangUsaha()
        KodePembukaDataTagihan()
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Kode_Supplier = '" & KodeLawanTransaksi & "' " &
                              " AND Jenis_Pembelian = '" & JenisPembelian_Tempo & "' " &
                              " ORDER BY Angka_Invoice, Nomor_ID ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorInvoice_Sebelumnya = Kosongan
        Dim Retur As Int64
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            UraianInvoicePerBaris = dr.Item("Catatan")
            NomorBP = KonversiNomorPembelianKeNomorBPHU(dr.Item("Nomor_Pembelian"))
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            JumlahTagihanPerBaris = dr.Item("Total_Tagihan") - Retur
            Perhitungan_JumlahBayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahPengajuanBayarPerBaris = SisaTagihanPerBaris
            JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
            KodeSetoranPerBaris = dr.Item("Kode_Setoran")
            PPhTerutangPerBaris = dr.Item("PPh_Terutang")
            PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
            PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
            RasioPPh()
            If NomorInvoicePerBaris <> NomorInvoice_Sebelumnya And SisaTagihanPerBaris > 0 Then TambahBaris()
            NomorInvoice_Sebelumnya = NomorInvoicePerBaris
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPemegangSaham WHERE NIK = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Dokumen")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHPS")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            Perhitungan_JumlahBayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahPengajuanBayarPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangKaryawan WHERE Nomor_ID_Karyawan = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Dokumen")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            Perhitungan_JumlahBayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahPengajuanBayarPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangBank()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangBank WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHB = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHB")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                         " WHERE Nomor_BPHB = '" & NomorBP & "' " &
                                         " AND Nomor_JV = 0 ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                AngsuranKe = drTELUSUR.Item("Angsuran_Ke")
                PokokPerBaris = drTELUSUR.Item("Pokok")
                BagiHasilPerBaris = drTELUSUR.Item("Bagi_Hasil")
                JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(drTELUSUR.Item("Jenis_PPh"))
                KodeSetoranPerBaris = drTELUSUR.Item("Kode_Setoran")
                PPhTerutangPerBaris = drTELUSUR.Item("Jumlah_PPh")
                PPhDitanggungPerBaris = drTELUSUR.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = drTELUSUR.Item("PPh_Dipotong")
                JumlahPengajuanBayarPerBaris = PokokPerBaris + BagiHasilPerBaris
                JumlahBayarPerBaris = JumlahPengajuanBayarPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangLeasing()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangLeasing " &
            " WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHL = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHL")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangLeasing " &
                                         " WHERE Nomor_BPHL = '" & NomorBP & "' " &
                                         " AND Nomor_JV = 0 ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                AngsuranKe = drTELUSUR.Item("Angsuran_Ke")
                PokokPerBaris = drTELUSUR.Item("Pokok")
                BagiHasilPerBaris = drTELUSUR.Item("Bagi_Hasil")
                PPhTerutangPerBaris = drTELUSUR.Item("Jumlah_PPh")
                PPhDitanggungPerBaris = drTELUSUR.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = drTELUSUR.Item("PPh_Dipotong")
                JumlahPengajuanBayarPerBaris = PokokPerBaris + BagiHasilPerBaris
                JumlahBayarPerBaris = JumlahPengajuanBayarPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangPihakKetiga()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
            " WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangPihakKetiga " &
                                         " WHERE Nomor_BPHPK = '" & NomorBP & "' " &
                                         " AND Nomor_JV = 0 ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                AngsuranKe = drTELUSUR.Item("Angsuran_Ke")
                PokokPerBaris = drTELUSUR.Item("Pokok")
                BagiHasilPerBaris = drTELUSUR.Item("Bagi_Hasil")
                JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(drTELUSUR.Item("Jenis_PPh"))
                KodeSetoranPerBaris = drTELUSUR.Item("Kode_Setoran")
                PPhTerutangPerBaris = drTELUSUR.Item("Jumlah_PPh")
                PPhDitanggungPerBaris = drTELUSUR.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = drTELUSUR.Item("PPh_Dipotong")
                JumlahPengajuanBayarPerBaris = PokokPerBaris + BagiHasilPerBaris
                JumlahBayarPerBaris = JumlahPengajuanBayarPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangAfiliasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangAfiliasi " &
            " WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHA = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPHA")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                         " WHERE Nomor_BPHA = '" & NomorBP & "' " &
                                         " AND Nomor_JV = 0 ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                AngsuranKe = drTELUSUR.Item("Angsuran_Ke")
                PokokPerBaris = drTELUSUR.Item("Pokok")
                BagiHasilPerBaris = drTELUSUR.Item("Bagi_Hasil")
                JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(drTELUSUR.Item("Jenis_PPh"))
                KodeSetoranPerBaris = drTELUSUR.Item("Kode_Setoran")
                PPhTerutangPerBaris = drTELUSUR.Item("Jumlah_PPh")
                PPhDitanggungPerBaris = drTELUSUR.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = drTELUSUR.Item("PPh_Dipotong")
                JumlahPengajuanBayarPerBaris = PokokPerBaris + BagiHasilPerBaris
                JumlahBayarPerBaris = JumlahPengajuanBayarPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPiutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangPemegangSaham " &
            " WHERE NIK = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Dokumen")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPPPS")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPiutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangKaryawan " &
            " WHERE Nomor_ID_Karyawan = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Dokumen")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPiutangPihakKetiga()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangPihakKetiga " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPPPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPiutangAfiliasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangAfiliasi " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPA = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPPA")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataDepositOperasional()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_DepositOperasional " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPDO = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Bukti")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPDO")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
            Perhitungan_JumlahBayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahPengajuanBayarPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataBankGaransi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_BankGaransi " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV_Transaksi = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPBG = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPBG")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataInvestasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_AktivaLainnya " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        Select Case Peruntukan
            Case Peruntukan_InvestasiDeposito
                QueryTampilan += " AND COA_Debet = '" & KodeTautanCOA_InvestasiDeposito & "' "
            Case Peruntukan_InvestasiSuratBerharga
                QueryTampilan += " AND COA_Debet = '" & KodeTautanCOA_InvestasiSuratBerharga & "' "
            Case Peruntukan_InvestasiLogamMulia
                QueryTampilan += " AND COA_Debet = '" & KodeTautanCOA_InvestasiLogamMulia & "' "
            Case Peruntukan_InvestasiPadaPerusahaanAnak
                QueryTampilan += " AND COA_Debet = '" & KodeTautanCOA_InvestasiPadaPerusahaanAnak & "' "
            Case Peruntukan_InvestasiGoodWill
                QueryTampilan += " AND COA_Debet = '" & KodeTautanCOA_InvestasiGoodWill & "' "
            Case Else
                QueryTampilan += " AND COA_Debet = 'X' " 'Ini Jangan dihapus...!!!
        End Select
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPAL = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Bukti")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
            UraianInvoicePerBaris = dr.Item("Keterangan")
            NomorBP = dr.Item("Nomor_BPAL")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanBayarPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub



    Sub RasioPPh()
        Dim PPhTerutang_SudahDibayar = 0
        Dim PPhDitanggung_SudahDibayar = 0
        Dim PPhDipotong_SudahDibayar = 0
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                     " WHERE Nomor_BP    = '" & NomorBP & "' " &
                                     " AND Status        = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            PPhTerutang_SudahDibayar += drTELUSUR.Item("PPh_Terutang")
            PPhDitanggung_SudahDibayar += drTELUSUR.Item("PPh_Ditanggung")
            PPhDipotong_SudahDibayar += drTELUSUR.Item("PPh_Dipotong")
        Loop
        PPhTerutangPerBaris -= PPhTerutang_SudahDibayar
        PPhDitanggungPerBaris -= PPhDitanggung_SudahDibayar
        PPhDipotongPerBaris -= PPhDipotong_SudahDibayar
    End Sub



    Sub TambahBaris()
        If AmbilAngka(SudahDibayarPerBaris) = 0 Then SudahDibayarPerBaris = StripKosong
        NomorUrutInvoice += 1
        DataTabelUtama.Rows.Add(NomorUrutInvoice, NomorInvoicePerBaris, TanggalInvoicePerBaris, UraianInvoicePerBaris, NomorBP,
                                JumlahTagihanPerBaris, AngsuranKe, PokokPerBaris, BagiHasilPerBaris, SudahDibayarPerBaris, SisaTagihanPerBaris,
                                JenisPajakPerBaris, KodeSetoranPerBaris, PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris,
                                JumlahPengajuanBayarPerBaris, JumlahBayarPerBaris,
                                PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris)
    End Sub


    Sub BarisTotal()
        If JumlahTagihan = 0 Then JumlahTagihan = StripKosong
        'If Pokok = 0 Then Pokok = StripKosong
        'If BagiHasil = 0 Then BagiHasil = StripKosong
        'If SudahDibayar = 0 Then SudahDibayar = StripKosong
        'If SisaTagihan = 0 Then SisaTagihan = StripKosong
        'If JumlahPengajuanBayar = 0 Then JumlahPengajuanBayar = StripKosong
        'If JumlahBayar = 0 Then JumlahBayar = StripKosong
        DataTabelUtama.Rows.Add()
        DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, " T O T A L ", Kosongan,
                                JumlahTagihan, Kosongan, Pokok, BagiHasil, SudahDibayar, SisaTagihan,
                                Kosongan, Kosongan, PPhTerutang, PPhDitanggung, PPhDipotong,
                                JumlahPengajuanBayar, JumlahBayar)
    End Sub


    Sub Perhitungan_JumlahBayarPerBaris()
        SudahDibayarPerBaris = 0
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP = '" & NomorBP & "' " &
                                   " AND Status = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
        drBAYAR_ExecuteReader()
        Do While drBAYAR.Read
            SudahDibayarPerBaris += drBAYAR.Item("Jumlah_Bayar")
        Loop
        If SudahDibayarPerBaris = 0 Then SudahDibayarPerBaris = StripKosong
    End Sub


    Sub Perhitungan()
        JumlahTagihan = 0
        Pokok = 0
        BagiHasil = 0
        SudahDibayar = 0
        SisaTagihan = 0
        JumlahPengajuanBayar = 0
        JumlahBayar = 0
        PPhTerutang = 0
        PPhDitanggung = 0
        PPhDipotong = 0
        JumlahInvoice = NomorUrutInvoice
        JumlahAngsuranTerjadwal = JumlahInvoice
        If DataTabelUtama.RowCount > JumlahInvoice Then
            DataTabelUtama.Rows.RemoveAt(DataTabelUtama.RowCount - 1)
            DataTabelUtama.Rows.RemoveAt(DataTabelUtama.RowCount - 1)
        End If
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If PembayaranTerjadwal Then
                JumlahTagihan = AmbilAngka(row.Cells("Jumlah_Tagihan_Per_Baris").Value)
            Else
                JumlahTagihan += AmbilAngka(row.Cells("Jumlah_Tagihan_Per_Baris").Value)
            End If
            Pokok += AmbilAngka(row.Cells("Pokok_Per_Baris").Value)
            BagiHasil += AmbilAngka(row.Cells("Bagi_Hasil_Per_Baris").Value)
            SudahDibayar += AmbilAngka(row.Cells("Sudah_Dibayar_Per_Baris").Value)
            SisaTagihan += AmbilAngka(row.Cells("Sisa_Tagihan_Per_Baris").Value)
            JumlahPengajuanBayar += AmbilAngka(row.Cells("Jumlah_pengajuan_Bayar_Per_Baris").Value)
            JumlahBayar += AmbilAngka(row.Cells("Jumlah_Bayar_Per_Baris").Value)
            PPhTerutang += AmbilAngka(row.Cells("PPh_Terutang_Per_Baris").Value)
            PPhDitanggung += AmbilAngka(row.Cells("PPh_Ditanggung_Per_Baris").Value)
            PPhDipotong += AmbilAngka(row.Cells("PPh_Dipotong_Per_Baris").Value)
        Next
        If JumlahInvoice = 0 Then
            DataTabelUtama.ColumnHeadersVisible = False
        Else
            DataTabelUtama.ColumnHeadersVisible = True
            If JumlahInvoice > 1 Then BarisTotal()
        End If
        LogikaTampilanKolom()
        Perhitungan_ValueBank()
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Singkirkan.Enabled = False
    End Sub

    Sub KontenCombo_Kategori()
        cmb_Kategori.Enabled = True
        cmb_Peruntukan.Enabled = True
        cmb_Kategori.Items.Clear()
        cmb_Kategori.Items.Add(Kategori_PembayaranHutang)
        cmb_Kategori.Items.Add(Kategori_PengeluaranTunai)
        cmb_Kategori.Items.Add(Kategori_Pemindahbukuan)
        cmb_Kategori.Items.Add(Kategori_Investasi)
        cmb_Kategori.Text = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPembayaranHutang()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangUsaha_NonAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBiaya)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangGaji)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBPJSKesehatan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBPJSKetenagakerjaan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangKoperasiKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangSerikat)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangLancarLainnya)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangLeasing)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBank)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPajak)
        cmb_Peruntukan.Text = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPengeluaranTunai()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_DepositOperasional)
        cmb_Peruntukan.Items.Add(Peruntukan_BankGaransi)
        cmb_Peruntukan.Text = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPemindahbukuan()
        'cmb_PeruntukanPembayaran.Items.Clear()
        'cmb_PeruntukanPembayaran.Items.Add(Peruntukan_PettyCash)
        'cmb_PeruntukanPembayaran.Items.Add(Peruntukan_Kas)
        'cmb_PeruntukanPembayaran.Items.Add(Peruntukan_Bank)
        'cmb_PeruntukanPembayaran.Items.Add(Peruntukan_CashAdvance)
        'cmb_PeruntukanPembayaran.Text = Kosongan
        KontenComboSaranaPembayaran_Public(cmb_Peruntukan)
    End Sub

    Sub KontenCombo_PeruntukanInvestasi()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiDeposito)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiSuratBerharga)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiLogamMulia)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiPadaPerusahaanAnak)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiGoodWill)
        cmb_Peruntukan.Text = Kosongan
    End Sub



    Private Sub cmb_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Kategori.SelectedIndexChanged
    End Sub
    Private Sub cmb_Kategori_TextChanged(sender As Object, e As EventArgs) Handles cmb_Kategori.TextChanged
        Kategori = cmb_Kategori.Text
        If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
            Select Case Kategori
                Case Kategori_PembayaranHutang
                    KontenCombo_PeruntukanPembayaranHutang()
                Case Kategori_PengeluaranTunai
                    KontenCombo_PeruntukanPengeluaranTunai()
                Case Kategori_Pemindahbukuan
                    If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                        PesanPemberitahuan("Untuk Pengajuan Pemindahbukuan, silakan melalui Buku Pengawasan terkait.")
                        Return
                    End If
                    'KontenCombo_PeruntukanPemindahbukuan()
                Case Kategori_Investasi
                    KontenCombo_PeruntukanInvestasi()
                Case Kosongan
                    cmb_Peruntukan.Items.Clear()
                    cmb_Peruntukan.Text = Kosongan
            End Select
        End If
    End Sub
    Private Sub cmb_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Kategori.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_Peruntukan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Peruntukan.SelectedIndexChanged
    End Sub
    Private Sub cmb_Peruntukan_TextChanged(sender As Object, e As EventArgs) Handles cmb_Peruntukan.TextChanged

        Peruntukan = cmb_Peruntukan.Text

        If Peruntukan <> Kosongan Then COADebet = KonversiPeruntukanKeCOA(Peruntukan)

        KosongkanDaftarTagihan()

        txt_KodeLawanTransaksi.Text = Kosongan

        LogikaTombol_Pratinjau()

        PembayaranDenganInvoice = True
        PembayaranTerjadwal = False

        DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").HeaderText = "Nomor Dokumen"
        DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").HeaderText = "Tanggal Dokumen"

        Select Case Peruntukan
            'Pembayaran Hutang :
            Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").HeaderText = "Nomor Invoice"
                DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").HeaderText = "Tanggal Invoice"
            Case Peruntukan_PembayaranHutangBank
                DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").HeaderText = "Nomor Kontrak"
                DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").HeaderText = "Tanggal Kontrak"
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangBank"
                KolomBPHJadwalAngsuran = "Nomor_BPHB"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangLeasing
                DataTabelUtama.Columns("Nomor_Invoice_Per_Baris").HeaderText = "Nomor Kontrak"
                DataTabelUtama.Columns("Tanggal_Invoice_Per_Baris").HeaderText = "Tanggal Kontrak"
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangLeasing"
                KolomBPHJadwalAngsuran = "Nomor_BPHL"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangPihakKetiga
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangPihakKetiga"
                KolomBPHJadwalAngsuran = "Nomor_BPHPK"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangAfiliasi
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangAfiliasi"
                KolomBPHJadwalAngsuran = "Nomor_BPHA"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangGaji
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Gaji, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangBPJSKesehatan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang BPJS Kesehatan, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang BPJS Ketenagakerjaan, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangKoperasiKaryawan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Koperasi Karyawan, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangSerikat
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Serikat, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangPajak
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Pajak, silakan melalui Buku Pengawasan terkait.")
                    Return
                End If
                PembayaranDenganInvoice = False
                If ProsesLoadingForm = True Then
                    JenisPajak = JenisPajakPerBaris
                    KodeSetoran = KodeSetoranPerBaris
                End If
                COADebet = PenentuanCOA_HutangPajak(JenisPajak, KodeSetoran)
                lbl_KodePajak.Visible = True
                txt_KodePajak.Visible = True
                If FungsiForm = FungsiForm_PROSES Then
                    lbl_KodePajak.Text = "Kode NTPN"
                Else
                    lbl_KodePajak.Text = "Kode Billing"
                End If
            'Pengeluaran Tunai :
            Case Peruntukan_PiutangPemegangSaham
                'Belum ada kode
            Case Peruntukan_PiutangKaryawan
                'Belum ada kode
            Case Peruntukan_PiutangPihakKetiga
                'Belum ada kode
            Case Peruntukan_PiutangAfiliasi
                'Belum ada kode
            Case Peruntukan_DepositOperasional
                'Belum ada kode
            Case Peruntukan_BankGaransi
                'Belum ada kode
            'Investasi :
            Case Peruntukan_InvestasiDeposito
                'Belum ada kode
            Case Peruntukan_InvestasiSuratBerharga
                'Belum ada kode
            Case Peruntukan_InvestasiLogamMulia
                'Belum ada kode
            Case Peruntukan_InvestasiPadaPerusahaanAnak
                'Belum ada kode
            Case Peruntukan_InvestasiGoodWill
                'Belum ada kode
        End Select

        If ProsesLoadingForm Or ProsesIsiValueForm Then
            If Peruntukan = Peruntukan_PembayaranHutangUsaha_NonAfiliasi _
            Or Peruntukan = Peruntukan_PembayaranHutangBiaya _
            Or Peruntukan = Peruntukan_PembayaranHutangGaji _
            Or Peruntukan = Peruntukan_PembayaranHutangBPJSKesehatan _
            Or Peruntukan = Peruntukan_PembayaranHutangBPJSKetenagakerjaan _
            Or Peruntukan = Peruntukan_PembayaranHutangKoperasiKaryawan _
            Or Peruntukan = Peruntukan_PembayaranHutangSerikat _
            Or Peruntukan = Peruntukan_PembayaranHutangPihakKetiga _
            Or Peruntukan = Peruntukan_PembayaranHutangKaryawan _
            Or Peruntukan = Peruntukan_PembayaranHutangLancarLainnya _
            Or Peruntukan = Peruntukan_PembayaranHutangLeasing _
            Or Peruntukan = Peruntukan_PembayaranHutangBank _
            Or Peruntukan = Peruntukan_PembayaranHutangPemegangSaham _
            Or Peruntukan = Peruntukan_PembayaranHutangAfiliasi _
            Or Peruntukan = Peruntukan_PembayaranHutangPajak _
            Then
                cmb_Kategori.Text = Kategori_PembayaranHutang
            ElseIf Peruntukan = Peruntukan_PiutangPemegangSaham _
            Or Peruntukan = Peruntukan_PiutangKaryawan _
            Or Peruntukan = Peruntukan_PiutangPihakKetiga _
            Or Peruntukan = Peruntukan_PiutangAfiliasi _
            Or Peruntukan = Peruntukan_DepositOperasional _
            Or Peruntukan = Peruntukan_BankGaransi _
            Then
                cmb_Kategori.Text = Kategori_PengeluaranTunai
            ElseIf Peruntukan = Peruntukan_InvestasiDeposito _
            Or Peruntukan = Peruntukan_InvestasiSuratBerharga _
            Or Peruntukan = Peruntukan_InvestasiLogamMulia _
            Or Peruntukan = Peruntukan_InvestasiPadaPerusahaanAnak _
            Then
                cmb_Kategori.Text = Kategori_Investasi
            Else
                cmb_Kategori.Text = Kategori_Pemindahbukuan
                COADebet = KonversiSaranaPembayaranKeCOA(Peruntukan)
            End If
        End If

        If PembayaranTerjadwal = True Then
            lbl_Denda.Visible = True
            txt_Denda.Visible = True
        Else
            lbl_Denda.Visible = False
            txt_Denda.Visible = False
        End If

        If PembayaranDenganInvoice = False Then
            If Not (ProsesResetForm Or ProsesLoadingForm Or ProsesIsiValueForm) Then BeginInvoke(Sub() cmb_Peruntukan.Text = Kosongan)
        End If

    End Sub
    Private Sub cmb_PeruntukanPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Peruntukan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_NomorKK_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorKK.TextChanged
        NomorKK = txt_NomorKK.Text
    End Sub
    Private Sub txt_NomorKK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorKK.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub dtp_TanggalKK_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalKK.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalKK)
        TanggalKK = TanggalFormatTampilan(dtp_TanggalKK.Value)
        If ProsesLoadingForm = False Then SistemPenomoranOtomatis_KK()
    End Sub


    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        If KodeLawanTransaksi = Kosongan Then
            KosongkanDaftarTagihan()
            txt_NamaLawanTransaksi.Text = Kosongan
            cmb_SaranaPembayaran.Text = Kosongan
        Else
            Select Case Peruntukan
                'Pembayaran Hutang :
                Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangUsaha()
                Case Peruntukan_PembayaranHutangPemegangSaham
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangPemegangSaham()
                Case Peruntukan_PembayaranHutangKaryawan
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaKaryawan(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangKaryawan()
                Case Peruntukan_PembayaranHutangBank
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangBank()
                Case Peruntukan_PembayaranHutangLeasing
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangLeasing()
                Case Peruntukan_PembayaranHutangPihakKetiga
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangPihakKetiga()
                Case Peruntukan_PembayaranHutangAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangAfiliasi()
                Case Peruntukan_PembayaranHutangGaji
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Karyawan
                Case Peruntukan_PembayaranHutangBPJSKesehatan
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_BpjsKesehatan
                Case Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_BpjsKetenagakerjaan
                Case Peruntukan_PembayaranHutangKoperasiKaryawan
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_KoperasiKaryawan
                Case Peruntukan_PembayaranHutangSerikat
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_SerikatPekerja
                Case Peruntukan_PembayaranHutangPajak
                    txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_DJP
                'Pengeluaran Tunai :
                Case Peruntukan_PiutangPemegangSaham
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPiutangPemegangSaham()
                Case Peruntukan_PiutangKaryawan
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaKaryawan(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPiutangKaryawan()
                Case Peruntukan_PiutangPihakKetiga
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPiutangPihakKetiga()
                Case Peruntukan_PiutangAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPiutangAfiliasi()
                Case Peruntukan_DepositOperasional
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataDepositOperasional()
                Case Peruntukan_BankGaransi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataBankGaransi()
                'Investasi :
                Case Peruntukan_InvestasiDeposito
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataBankGaransi()
                Case Peruntukan_InvestasiSuratBerharga
                    'Belum ada kode
                Case Peruntukan_InvestasiLogamMulia
                    'Belum ada kode
                Case Peruntukan_InvestasiPadaPerusahaanAnak
                    'Belum ada kode
                Case Peruntukan_InvestasiGoodWill
                    'Belum ada kode
            End Select
            If Kategori = Kategori_Pemindahbukuan Then txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Internal
            If Kategori = Kategori_Investasi Then
                txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataInvestasi()
            End If
        End If
        LogikaTombol_Pratinjau()
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        Select Case Peruntukan
            'Pembayaran Hutang :
            Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_PembayaranHutangPemegangSaham
                'frm_ListDataPemegangSaham.ResetForm()
                'If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataPemegangSaham.NIK_Terseleksi = txt_KodeLawanTransaksi.Text
                'frm_ListDataPemegangSaham.ShowDialog()
                'txt_KodeLawanTransaksi.Text = frm_ListDataPemegangSaham.NIK_Terseleksi
            Case Peruntukan_PembayaranHutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_PembayaranHutangBank
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
                frm_ListMitra.PilihLembagaKeuangan = Pilihan_Ya
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_PembayaranHutangLeasing
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
                frm_ListMitra.PilihLembagaKeuangan = Pilihan_Ya
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_PembayaranHutangPihakKetiga
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_PembayaranHutangAfiliasi
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            'Pengeluaran Tunai :
            Case Peruntukan_PiutangPemegangSaham
                'frm_ListDataPemegangSaham.ResetForm()
                'If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataPemegangSaham.NIK_Terseleksi = txt_KodeLawanTransaksi.Text
                'frm_ListDataPemegangSaham.ShowDialog()
                'txt_KodeLawanTransaksi.Text = frm_ListDataPemegangSaham.NIK_Terseleksi
            Case Peruntukan_PiutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NIK_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_PiutangPihakKetiga
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_PiutangAfiliasi
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_DepositOperasional
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
            Case Peruntukan_BankGaransi
                frm_ListMitra.ResetForm()
                frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
                frm_ListMitra.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
        End Select
        If Kategori = Kategori_Investasi Then
            frm_ListMitra.ResetForm()
            frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
            If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            frm_ListMitra.ShowDialog()
            txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
        End If
    End Sub

    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub



    Private Sub cmb_SaranaPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.SelectedIndexChanged
    End Sub
    Private Sub cmb_SaranaPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SaranaPembayaran.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_SaranaPembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmb_SaranaPembayaran.TextChanged
        SaranaPembayaran = cmb_SaranaPembayaran.Text
        COAKredit = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        If COATermasukBank(COAKredit) Then
            PembayaranViaBank = True
            grb_Bank.Enabled = True
            KontenComboDitanggungOleh_Public(cmb_DitanggungOleh)
            If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
        If ProsesLoadingForm = False Then SistemPenomoranOtomatis_KK()
        LogikaTombol_Pratinjau()
    End Sub


    Private Sub btn_Singkirkan_Click(sender As Object, e As EventArgs) Handles btn_Singkirkan.Click
        Pilihan = MessageBox.Show("Yakin akan menyingkirkan item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        NomorUrutInvoice = 0
        If PembayaranTerjadwal Then
            Do While 1 = 1
                Try
                    DataTabelUtama.Rows.RemoveAt(BarisTerseleksi)
                Catch ex As Exception
                    Exit Do
                End Try
            Loop
        Else
            DataTabelUtama.Rows.Remove(DataTabelUtama.CurrentRow)
            Try
                DataTabelUtama.Rows.RemoveAt(DataTabelUtama.RowCount - 1)
                DataTabelUtama.Rows.RemoveAt(DataTabelUtama.RowCount - 1)
            Catch ex As Exception
            End Try
        End If
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorUrutInvoice += 1
            row.Cells("Nomor_Urut_Invoice").Value = NomorUrutInvoice
        Next
        Perhitungan()
        BersihkanSeleksi()
    End Sub



    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Enabled = False
        txt_RekeningPenerima.Enabled = True
        txt_AtasNamaPenerima.Enabled = True
        txt_RekeningPenerima.Text = Kosongan
        txt_AtasNamaPenerima.Text = Kosongan
        txt_BiayaAdministrasiBank.Text = Kosongan
        cmb_DitanggungOleh.Text = Kosongan
        If Not (AmbilAngka(COAKredit) >= KodeAkun_Bank_Awal _
            And AmbilAngka(COAKredit) <= kodeakun_Bank_Akhir) _
            Then
            txt_JumlahTransfer.Text = Kosongan
            'txt_TotalBank.Text = Nothing
        End If
    End Sub

    Private Sub txt_BiayaAdministrasiBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaAdministrasiBank.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = AmbilAngka(txt_BiayaAdministrasiBank.Text)
        PemecahRibuanUntukTextBox(txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.Enabled = False
            cmb_DitanggungOleh.Text = Kosongan
        Else
            cmb_DitanggungOleh.Enabled = True
            If Kategori = Kategori_Pemindahbukuan Then
                cmb_DitanggungOleh.Text = DitanggungOleh_Perusahaan
                cmb_DitanggungOleh.Enabled = False
            End If
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DitanggungOleh.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_DitanggungOleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.SelectedIndexChanged
    End Sub
    Private Sub cmb_DitanggungOleh_TextChanged(sender As Object, e As EventArgs) Handles cmb_DitanggungOleh.TextChanged
        DitanggungOleh = cmb_DitanggungOleh.Text
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub txt_JumlahTransfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransfer.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahTransfer_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransfer.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahTransfer)
    End Sub

    Public KodeMataUang = KodeMataUang_IDR
    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, AmbilAngka(JumlahBayar), JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
    End Sub



    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index

        NomorUrutInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut_Invoice", BarisTerseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice_Per_Baris", BarisTerseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice_Per_Baris", BarisTerseleksi).Value
        UraianInvoice_Terseleksi = DataTabelUtama.Item("Uraian_Invoice_Per_Baris", BarisTerseleksi).Value
        NomorBP_Terseleksi = DataTabelUtama.Item("Nomor_BP_Per_Baris", BarisTerseleksi).Value
        JumlahTagihanPerBaris_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Tagihan_Per_Baris", BarisTerseleksi).Value)
        SudahDibayarPerBaris_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sudah_Dibayar_Per_Baris", BarisTerseleksi).Value)
        SisaTagihanPerBaris_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Tagihan_Per_Baris", BarisTerseleksi).Value)
        JumlahPengajuanBayarPerBaris_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Pengajuan_Bayar_Per_Baris", BarisTerseleksi).Value)
        JumlahBayarPerBaris_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar_Per_Baris", BarisTerseleksi).Value)

        If JumlahInvoice > 1 Then
            If NomorUrutInvoice_Terseleksi > 0 Then
                btn_Singkirkan.Enabled = True
            Else
                btn_Singkirkan.Enabled = False
            End If
        End If

        If NomorUrutInvoice_Terseleksi > 0 _
            And PembayaranTerjadwal = False _
            And (DataTabelUtama.Columns(e.ColumnIndex).Name = "Jumlah_Pengajuan_Bayar_Per_Baris" _
            Or DataTabelUtama.Columns(e.ColumnIndex).Name = "Jumlah_Bayar_Per_Baris") _
            Then

            If (Kategori = Kategori_PengeluaranTunai And Peruntukan <> Peruntukan_DepositOperasional) _
                Or Kategori = Kategori_Pemindahbukuan _
                Or Kategori = Kategori_Investasi _
                Then Return

            frm_InputJumlah.ResetForm()
            frm_InputJumlah.FungsiForm = FungsiForm
            frm_InputJumlah.PeruntukanPembayaran = Peruntukan
            frm_InputJumlah.NomorInvoice = NomorInvoice_Terseleksi
            frm_InputJumlah.NomorBP = NomorBP_Terseleksi
            frm_InputJumlah.txt_JumlahTagihan.Text = JumlahTagihanPerBaris_Terseleksi
            frm_InputJumlah.txt_SudahDibayar.Text = SudahDibayarPerBaris_Terseleksi
            If FungsiForm <> FungsiForm_PROSES Then
                'Pengajuan :
                frm_InputJumlah.JudulForm = "Jumlah Pengajuan"
                frm_InputJumlah.txt_Jumlah.Text = JumlahPengajuanBayarPerBaris_Terseleksi
            Else
                'Proses :
                frm_InputJumlah.JudulForm = "Jumlah Disetujui"
                frm_InputJumlah.txt_Jumlah.Text = JumlahBayarPerBaris_Terseleksi
                If Status <> Status_Diproses Then
                    'Jika dalam rangka ngedit data yang sudah disetujui :
                End If
            End If
            frm_InputJumlah.PPhTerutang_ValueAwal_dB = DataTabelUtama.Item("PPh_Terutang_Per_Baris_Value_Awal_dB", BarisTerseleksi).Value
            frm_InputJumlah.PPhDitanggung_ValueAwal_dB = DataTabelUtama.Item("PPh_Ditanggung_Per_Baris_Value_Awal_dB", BarisTerseleksi).Value
            frm_InputJumlah.PPhDipotong_ValueAwal_dB = DataTabelUtama.Item("PPh_Dipotong_Per_Baris_Value_Awal_dB", BarisTerseleksi).Value
            frm_InputJumlah.ShowDialog()
            If frm_InputJumlah.DialogResult = DialogResult.OK Then
                If FungsiForm <> FungsiForm_PROSES Then
                    If frm_InputJumlah.JumlahInputan > 0 Then
                        DataTabelUtama.Item("Jumlah_Pengajuan_Bayar_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.JumlahInputan
                        Perhitungan()
                    Else
                        btn_Singkirkan_Click(sender, e)
                    End If
                Else
                    DataTabelUtama.Item("Jumlah_Bayar_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.JumlahInputan
                    Perhitungan()
                End If
                DataTabelUtama.Item("Jenis_Pajak_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.JenisPajak
                DataTabelUtama.Item("Kode_Setoran_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.KodeSetoran
                DataTabelUtama.Item("PPh_Terutang_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.PPhTerutang_UntukIsiValue
                DataTabelUtama.Item("PPh_Ditanggung_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.PPhDitanggung_UntukIsiValue
                DataTabelUtama.Item("PPh_Dipotong_Per_Baris", BarisTerseleksi).Value = frm_InputJumlah.PPhDipotong_UntukIsiValue
            End If

        End If 'Setelah baris ini, jangan ada koding lagi di dalam sub ini.

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub

    Sub KosongkanDaftarTagihan()
        DataTabelUtama.Rows.Clear()
    End Sub


    Private Sub txt_Denda_TextChanged(sender As Object, e As EventArgs) Handles txt_Denda.TextChanged
        Denda = AmbilAngka(txt_Denda.Text)
        PemecahRibuanUntukTextBox(txt_Denda)
    End Sub
    Private Sub txt_Denda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Denda.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_KodePajak_TextChanged(sender As Object, e As EventArgs) Handles txt_KodePajak.TextChanged
        KodePajak = txt_KodePajak.Text
    End Sub


    Private Sub dtp_TanggalBayar_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBayar.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBayar)
        TanggalBayar = TanggalFormatTampilan(dtp_TanggalBayar.Value)
    End Sub

    Private Sub txt_Catatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
    End Sub


    Private Sub txt_RekeningPenerima_TextChanged(sender As Object, e As EventArgs) Handles txt_RekeningPenerima.TextChanged
        RekeningPenerima = txt_RekeningPenerima.Text
    End Sub
    Private Sub txt_RekeningPenerima_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_RekeningPenerima.KeyPress
        If Not Peruntukan = Peruntukan_BankGaransi Then
            KunciTotalInputan(sender, e)
        End If
    End Sub

    Private Sub txt_AtasNamaPenerima_TextChanged(sender As Object, e As EventArgs) Handles txt_AtasNamaPenerima.TextChanged
        AtasNamaPenerima = txt_AtasNamaPenerima.Text
    End Sub
    Private Sub txt_AtasNamaPenerima_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AtasNamaPenerima.KeyPress
        If Not (Peruntukan = Peruntukan_BankGaransi Or Kategori = Kategori_Pemindahbukuan) Then
            KunciTotalInputan(sender, e)
        End If
    End Sub




    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click

    End Sub
    Sub LogikaTombol_Pratinjau()
        If Peruntukan = Kosongan _
            Or KodeLawanTransaksi = Kosongan _
            Or SaranaPembayaran = Kosongan _
            Or DataTabelUtama.RowCount = 0 _
            Then
            btn_Pratinjau.Enabled = False
        Else
            btn_Pratinjau.Enabled = True
        End If
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Value :
        ProsesTutupForm = False
        Catatan = txt_Catatan.Text
        TanggalKK = TanggalFormatTampilan(dtp_TanggalKK.Value)
        NomorInvoice_Bundel = Kosongan
        TanggalInvoice_Bundel = Kosongan

        PPhTerutang_Pasal21_100 = 0
        PPhTerutang_Pasal21_401 = 0
        PPhTerutang_Pasal23_100 = 0
        PPhTerutang_Pasal23_101 = 0
        PPhTerutang_Pasal23_102 = 0
        PPhTerutang_Pasal23_103 = 0
        PPhTerutang_Pasal23_104 = 0
        PPhTerutang_Pasal42_402 = 0
        PPhTerutang_Pasal42_403 = 0
        PPhTerutang_Pasal42_409 = 0
        PPhTerutang_Pasal42_419 = 0
        PPhTerutang_Pasal26_100 = 0
        PPhTerutang_Pasal26_101 = 0
        PPhTerutang_Pasal26_102 = 0
        PPhTerutang_Pasal26_103 = 0
        PPhTerutang_Pasal26_104 = 0
        PPhTerutang_Pasal26_105 = 0
        PPhDitanggung_Pasal21 = 0
        PPhDitanggung_Pasal23 = 0
        PPhDitanggung_Pasal26 = 0
        PPhDitanggung_Pasal42 = 0
        PPhDipotong_Total = 0

        'Validasi Form
        If Peruntukan = Kosongan Then
            PesanPeringatan("Silakan pilih 'Peruntukan Pembayaran'.")
            cmb_Peruntukan.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If SaranaPembayaran = Kosongan Then
            PesanPeringatan("Silakan pilih 'Sarana Pembayaran'.")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        If PembayaranViaBank And COATermasukBank(COADebet) Then

            If FungsiForm = FungsiForm_PROSES Then
                If RekeningPenerima = Kosongan Then
                    PesanPeringatan("Silakan isi kolom 'Rekening'.")
                    txt_RekeningPenerima.Focus()
                    Return
                End If
                If AtasNamaPenerima = Kosongan Then
                    PesanPeringatan("Silakan isi kolom 'Atas Nama'.")
                    txt_AtasNamaPenerima.Focus()
                    Return
                End If
            End If

            If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
                PesanPeringatan("Silakan pilih 'Ditanggung Oleh'.")
                cmb_DitanggungOleh.Focus()
                Return
            End If

        End If

        If DataTabelUtama.RowCount = 0 Then
            PesanPeringatan("Silakan isi 'Tabel Pembayaran'.")
            Return
        End If

        If FungsiForm = FungsiForm_PROSES Then

            If AmbilAngka(JumlahBayar) = 0 Then
                PesanPeringatan("Silakan isi kolom 'Jumlah Bayar' per baris invoice.")
                Return
            End If

        End If

        If Peruntukan = Peruntukan_PembayaranHutangPajak And KodePajak = Kosongan Then

            If FungsiForm = FungsiForm_PROSES Then
                PesanPeringatan("Silakan isi kolom 'Kode NTPN'.")
            Else
                PesanPeringatan("Silakan isi kolom 'Kode Billing'.")
            End If

            txt_KodePajak.Focus()
            Return

        End If


        StatusSuntingDatabase = True

        AksesDatabase_Transaksi(Buka)

        Select Case FungsiForm

            Case FungsiForm_TAMBAH

                Status = Status_Diproses
                TanggalBayar = TanggalKosong
                NomorJV = 0

            Case FungsiForm_EDIT

                HapusDataPengajuanLama()
                Status = Status_Diproses
                TanggalBayar = TanggalKosong
                NomorJV = 0

            Case FungsiForm_PROSES

                HapusDataPengajuanLama()

                If Status <> Status_Dibayar Then
                    'Jika masih dalam proses :
                    SistemPenomoranOtomatis_NomorJV()
                    NomorJV = jur_NomorJV
                Else
                    'Jika sudah pernah disetujui, dan hendak mengeditnya :
                    NomorJV = NomorJV_Sebelumnya
                    HapusJurnal_BerdasarkanNomorJV(NomorJV)
                End If

                Status = Status_Dibayar

        End Select

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BuktiPengeluaran")

        For Each row As DataGridViewRow In DataTabelUtama.Rows 'Awal Loop =========================================================

            If AmbilAngka(row.Cells("Nomor_Urut_Invoice").Value) = 0 Then Exit For

            NomorID += 1
            NomorBP = row.Cells("Nomor_BP_Per_Baris").Value
            NomorInvoicePerBaris = row.Cells("Nomor_Invoice_Per_Baris").Value
            TanggalInvoicePerBaris = row.Cells("Tanggal_Invoice_Per_Baris").Value
            If TanggalInvoicePerBaris = Kosongan Then TanggalInvoicePerBaris = TanggalKosong
            UraianInvoicePerBaris = row.Cells("Uraian_Invoice_Per_Baris").Value
            JumlahTagihanPerBaris = AmbilAngka(row.Cells("Jumlah_Tagihan_Per_Baris").Value)
            AngsuranKe = AmbilAngka(row.Cells("Angsuran_Ke").Value)
            PokokPerBaris = AmbilAngka(row.Cells("Pokok_Per_Baris").Value)
            BagiHasilPerBaris = AmbilAngka(row.Cells("Bagi_Hasil_Per_Baris").Value)
            JumlahPengajuanBayarPerBaris = AmbilAngka(row.Cells("Jumlah_Pengajuan_Bayar_Per_Baris").Value)
            JumlahBayarPerBaris = AmbilAngka(row.Cells("Jumlah_Bayar_Per_Baris").Value)
            JenisPajakPerBaris = row.Cells("Jenis_Pajak_Per_Baris").Value
            KodeSetoranPerBaris = row.Cells("Kode_Setoran_Per_Baris").Value
            PPhTerutangPerBaris = AmbilAngka(row.Cells("PPh_Terutang_Per_Baris").Value)
            PPhDitanggungPerBaris = AmbilAngka(row.Cells("PPh_Ditanggung_Per_Baris").Value)
            PPhDipotongPerBaris = AmbilAngka(row.Cells("PPh_Dipotong_Per_Baris").Value)

            If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                NomorInvoicePerBaris = KodePajak
            End If

            cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                        " '" & NomorID & "', " &
                                        " '" & AngkaKK & "', " &
                                        " '" & NomorKK & "', " &
                                        " '" & TanggalFormatSimpan(TanggalKK) & "', " &
                                        " '" & Kosongan & "', " &
                                        " '" & Kategori & "', " &
                                        " '" & Peruntukan & "', " &
                                        " '" & KodeLawanTransaksi & "', " &
                                        " '" & NamaLawanTransaksi & "', " &
                                        " '" & NomorBP & "', " &
                                        " '" & NomorInvoicePerBaris & "', " &
                                        " '" & TanggalFormatSimpan(TanggalInvoicePerBaris) & "', " &
                                        " '" & UraianInvoicePerBaris & "', " &
                                        " '" & JumlahTagihanPerBaris & "', " &
                                        " '" & AngsuranKe & "', " &
                                        " '" & PokokPerBaris & "', " &
                                        " '" & BagiHasilPerBaris & "', " &
                                        " '" & JumlahPengajuanBayarPerBaris & "', " &
                                        " '" & JumlahBayarPerBaris & "', " &
                                        " '" & 0 & "', " &
                                        " '" & COADebet & "', " &
                                        " '" & COAKredit & "', " &
                                        " '" & BiayaAdministrasiBank & "', " &
                                        " '" & DitanggungOleh & "', " &
                                        " '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                                        " '" & RekeningPenerima & "', " &
                                        " '" & AtasNamaPenerima & "', " &
                                        " '" & Status & "', " &
                                        " '" & JenisPajakPerBaris & "', " &
                                        " '" & KodeSetoranPerBaris & "', " &
                                        " '" & PPhTerutangPerBaris & "', " &
                                        " '" & PPhDitanggungPerBaris & "', " &
                                        " '" & PPhDipotongPerBaris & "', " &
                                        " '" & Catatan & "', " &
                                        " '" & NomorJV & "', " &
                                        " '" & UserAktif & "' ) ",
                                        KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()

            'Pembundelan Nomor dan Tanggal Invoice :
            If PembayaranTerjadwal Then
                NomorInvoice_Bundel = NomorInvoicePerBaris
                TanggalInvoice_Bundel = TanggalInvoicePerBaris
            Else
                If NomorInvoice_Bundel = Kosongan Then
                    NomorInvoice_Bundel = NomorInvoicePerBaris
                    TanggalInvoice_Bundel = TanggalInvoicePerBaris
                Else
                    NomorInvoice_Bundel &= SlashGanda_Pemisah & NomorInvoicePerBaris
                    TanggalInvoice_Bundel &= SlashGanda_Pemisah & TanggalInvoicePerBaris
                End If
            End If

            If Not PembayaranDenganInvoice Then
                'NomorInvoice_Bundel = Kosongan
                TanggalInvoice_Bundel = Kosongan
            End If

            'Pembundelan PPh Terutang dan PPh Ditanggung Berdasarkan Jenis PPh :
            Select Case JenisPajakPerBaris
                Case JenisPajak_PPhPasal21
                    PPhDitanggung_Pasal21 += PPhDitanggungPerBaris
                    Select Case KodeSetoranPerBaris
                        Case KodeSetoran_100
                            PPhTerutang_Pasal21_100 += PPhTerutangPerBaris
                        Case KodeSetoran_401
                            PPhTerutang_Pasal21_401 += PPhTerutangPerBaris
                    End Select
                Case JenisPajak_PPhPasal23
                    PPhDitanggung_Pasal23 += PPhDitanggungPerBaris
                    Select Case KodeSetoranPerBaris
                        Case KodeSetoran_100
                            PPhTerutang_Pasal23_100 += PPhTerutangPerBaris
                        Case KodeSetoran_101
                            PPhTerutang_Pasal23_101 += PPhTerutangPerBaris
                        Case KodeSetoran_102
                            PPhTerutang_Pasal23_102 += PPhTerutangPerBaris
                        Case KodeSetoran_103
                            PPhTerutang_Pasal23_103 += PPhTerutangPerBaris
                        Case KodeSetoran_104
                            PPhTerutang_Pasal23_104 += PPhTerutangPerBaris
                    End Select
                Case JenisPajak_PPhPasal26
                    PPhDitanggung_Pasal26 += PPhDitanggungPerBaris
                    Select Case KodeSetoranPerBaris
                        Case KodeSetoran_100
                            PPhTerutang_Pasal26_100 += PPhTerutangPerBaris
                        Case KodeSetoran_101
                            PPhTerutang_Pasal26_101 += PPhTerutangPerBaris
                        Case KodeSetoran_102
                            PPhTerutang_Pasal26_102 += PPhTerutangPerBaris
                        Case KodeSetoran_103
                            PPhTerutang_Pasal26_103 += PPhTerutangPerBaris
                        Case KodeSetoran_104
                            PPhTerutang_Pasal26_104 += PPhTerutangPerBaris
                        Case KodeSetoran_105
                            PPhTerutang_Pasal26_105 += PPhTerutangPerBaris
                    End Select
                Case JenisPajak_PPhPasal42
                    PPhDitanggung_Pasal42 += PPhDitanggungPerBaris
                    Select Case KodeSetoranPerBaris
                        Case KodeSetoran_402
                            PPhTerutang_Pasal42_402 += PPhTerutangPerBaris
                        Case KodeSetoran_403
                            PPhTerutang_Pasal42_403 += PPhTerutangPerBaris
                        Case KodeSetoran_409
                            PPhTerutang_Pasal42_409 += PPhTerutangPerBaris
                        Case KodeSetoran_419
                            PPhTerutang_Pasal42_419 += PPhTerutangPerBaris
                    End Select
            End Select
            PPhDipotong_Total += PPhDipotongPerBaris

            If Status = Status_Dibayar Then

                If PembayaranTerjadwal Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                          " Tanggal_Bayar                           = '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                                          " Denda                                   = '" & 0 & "', " &
                                          " COA_Kredit                              = '" & COAKredit & "', " &
                                          " Biaya_Administrasi_Bank                 = '" & BiayaAdministrasiBank & "', " &
                                          " Ditanggung_Oleh                         = '" & DitanggungOleh & "', " &
                                          " Keterangan                              = '" & Catatan & "', " &
                                          " Nomor_JV                                = '" & NomorJV & "', " &
                                          " User                                    = '" & UserAktif & "' " &
                                          " WHERE " & KolomBPHJadwalAngsuran & "    = '" & NomorBP & "' " &
                                          " AND Angsuran_Ke                         = '" & AngsuranKe & "' ",
                                          KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                End If

                If Kategori = Kategori_PengeluaranTunai Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    Select Case Peruntukan
                        Case Peruntukan_PiutangPemegangSaham
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangPemegangSaham SET " &
                                                  " COA_Kredit          = '" & COAKredit & "', " &
                                                  " Nomor_JV            = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPPS   = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                        Case Peruntukan_PiutangKaryawan
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangKaryawan SET " &
                                                  " COA_Kredit          = '" & COAKredit & "', " &
                                                  " Nomor_JV            = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPK    = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                        Case Peruntukan_PiutangPihakKetiga
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangPihakKetiga SET " &
                                                  " COA_Kredit          = '" & COAKredit & "', " &
                                                  " Nomor_JV            = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPPK   = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                        Case Peruntukan_PiutangAfiliasi
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangAfiliasi SET " &
                                                  " COA_Kredit          = '" & COAKredit & "', " &
                                                  " Nomor_JV            = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPA    = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                        Case Peruntukan_DepositOperasional
                            'Untuk sementara, belum butuh coding.
                        Case Peruntukan_BankGaransi
                            cmd = New OdbcCommand(" SELECT Biaya_Provisi FROM tbl_BankGaransi " &
                                                  " WHERE Nomor_BPBG = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            dr_Read()
                            If dr.HasRows Then BiayaProvisi = dr.Item("Biaya_Provisi")
                            cmd = New OdbcCommand(" UPDATE tbl_BankGaransi SET " &
                                                  " COA_Kredit          = '" & COAKredit & "', " &
                                                  " Nomor_JV_Transaksi  = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPBG    = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                    End Select
                End If

                If Kategori = Kategori_Pemindahbukuan Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    cmd = New OdbcCommand(" UPDATE tbl_Pemindahbukuan SET " &
                                          " Tanggal_Transaksi   = '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                                          " Nomor_JV            = '" & NomorJV & "' " &
                                          " WHERE Nomor_BPPB    = '" & NomorBP & "' ",
                                          KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                End If

                If Kategori = Kategori_Investasi Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    cmd = New OdbcCommand(" UPDATE tbl_AktivaLainnya SET " &
                                          " COA_Kredit          = '" & COAKredit & "', " &
                                          " Nomor_JV            = '" & NomorJV & "' " &
                                          " WHERE Nomor_BPAL    = '" & NomorBP & "' ",
                                          KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                End If

            End If

        Next 'Akhir Loop ==========================================================================================================

        If PembayaranTerjadwal Then
            '(Ini sudah benar posisinya di luar Loop-ForEachNext. Jangan dimasukkan)
            '(Ini fungsinya untuk menyimpan data Denda pada baris terakhir bundelan angsuran)
            cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                  " Denda           = '" & Denda & "' " &
                                  " WHERE Nomor_ID  = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            If Status = Status_Dibayar Then
                cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                      " Denda                                   = '" & Denda & "' " &
                                      " WHERE " & KolomBPHJadwalAngsuran & "    = '" & NomorBP & "' " &
                                      " AND Angsuran_Ke                         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
            End If
        End If

        AksesDatabase_Transaksi(Tutup)

        'Penyimpanan Jurnal :
        If Status = Status_Dibayar Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalKK)
            Select Case Peruntukan
                Case Peruntukan_PiutangPemegangSaham
                    jur_JenisJurnal = JenisJurnal_PiutangPemegangSaham
                Case Peruntukan_PiutangKaryawan
                    jur_JenisJurnal = JenisJurnal_PiutangKaryawan
                Case Peruntukan_PiutangPihakKetiga
                    jur_JenisJurnal = JenisJurnal_PiutangPihakKetiga
                Case Peruntukan_PiutangAfiliasi
                    jur_JenisJurnal = JenisJurnal_PiutangAfiliasi
                Case Else
                    jur_JenisJurnal = KonversiSaranaPembayaranKeJenisJurnal(SaranaPembayaran)
            End Select
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoice_Bundel
            jur_NomorInvoice = NomorInvoice_Bundel
            jur_KodeLawanTransaksi = KodeLawanTransaksi
            jur_NamaLawanTransaksi = NamaLawanTransaksi
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            'If Pokok = StripKosong Then Pokok = 0
            'If BagiHasil = StripKosong Then BagiHasil = 0
            Dim JumlahDebet, JumlahKredit
            If PembayaranTerjadwal Then
                JumlahDebet = Pokok
                JumlahKredit = Pokok + BagiHasil + Denda - PPhDipotong_Total
            Else
                JumlahDebet = JumlahBayar + PPhDipotong_Total
                JumlahKredit = JumlahBayar + BiayaProvisi
            End If

            'Simpan Jurnal :
            ___jurDebet(COADebet, JumlahDebet)
            ___jurDebet(KodeTautanCOA_BiayaDendaBank, Denda)
            ___jurDebet(KodeTautanCOA_BiayaBungaBank, BagiHasil)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal21, PPhDitanggung_Pasal21)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal23, PPhDitanggung_Pasal23)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal26, PPhDitanggung_Pasal26)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal42, PPhDitanggung_Pasal42)
            ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaProvisi)
            ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_100, PPhTerutang_Pasal21_100)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal21_401, PPhTerutang_Pasal21_401)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal23_100, PPhTerutang_Pasal23_100)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal23_101, PPhTerutang_Pasal23_101)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal23_102, PPhTerutang_Pasal23_102)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal23_103, PPhTerutang_Pasal23_103)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal23_104, PPhTerutang_Pasal23_104)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_100, PPhTerutang_Pasal26_100)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_101, PPhTerutang_Pasal26_101)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_102, PPhTerutang_Pasal26_102)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_103, PPhTerutang_Pasal26_103)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_104, PPhTerutang_Pasal26_104)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal26_105, PPhTerutang_Pasal26_105)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal42_402, PPhTerutang_Pasal42_402)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal42_403, PPhTerutang_Pasal42_403)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal42_409, PPhTerutang_Pasal42_409)
            _______jurKredit(KodeTautanCOA_HutangPPhPasal42_419, PPhTerutang_Pasal42_419)
            _______jurKreditBankCashOUT(DitanggungOleh, COAKredit, JumlahKredit, JumlahTransfer, BiayaAdministrasiBank)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If FungsiForm = FungsiForm_PROSES Then
                If Status = Status_PROSES Then
                    PesanSukses("Data Pengajuan BERHASIL disetujui dan dikirim ke Jurnal.")
                Else
                    PesanSukses("Data Pengajuan dan Jurnal BERHASIL diperbarui.")
                End If
            End If
            X_frm_BukuPengawasanBuktiPengeluaranBankCash_X.TampilkanData()
            ProsesTutupForm = True
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
            If FungsiForm = FungsiForm_PROSES Then
                PesanPeringatan("Data GAGAL diperbarui..!")
            End If
        End If

    End Sub

    Sub HapusDataPengajuanLama()
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPengeluaran " &
                                   " WHERE Angka_KK = '" & AngkaKK & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ProsesTutupForm = True
    End Sub

    Dim ProsesTutupForm As Boolean = True
    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If ProsesTutupForm = False Then
            e.Cancel = True
        End If

    End Sub

End Class