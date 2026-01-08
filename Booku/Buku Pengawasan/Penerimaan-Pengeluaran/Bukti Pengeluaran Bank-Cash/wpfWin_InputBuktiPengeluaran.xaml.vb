Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfWin_InputBuktiPengeluaran

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Dim QueryTampilan

    Public NomorID
    Dim Kategori
    Dim Peruntukan
    Public AngkaKK
    Dim NomorKK
    Dim TanggalKK
    Dim NomorBundel
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
    Dim Kurs As Decimal
    Public NomorBP
    Dim JumlahTagihanPerBaris
    Dim AngsuranKe
    Dim PokokPerBaris
    Dim BagiHasilPerBaris
    Dim SudahDibayarPerBaris
    Dim SisaTagihanPerBaris
    Dim JumlahPengajuanPerBaris
    Dim JumlahDisetujuiPerBaris
    Dim JenisPajakPerBaris As String    '(Ini jangan dirubah...!)
    Dim KodeSetoranPerBaris As String   '(Ini jangan dirubah...!)
    Dim PPhTerutangPerBaris As Int64
    Dim PPhDitanggungPerBaris As Int64
    Dim PPhDipotongPerBaris As Int64

    Dim NomorUrutInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim UraianInvoice_Terseleksi
    Dim NomorBP_Terseleksi
    Dim JumlahTagihanPerBaris_Terseleksi
    Dim SudahDibayarPerBaris_Terseleksi
    Dim SisaTagihanPerBaris_Terseleksi
    Dim JumlahPengajuanPerBaris_Terseleksi
    Dim JumlahDisetujuiPerBaris_Terseleksi

    Dim JumlahTagihan
    Dim Pokok
    Dim BagiHasil
    Dim Denda
    Dim SudahDibayar
    Dim SisaTagihan
    Dim JumlahPengajuan_Tabel
    Dim JumlahBayar_Tabel
    Public JumlahPengajuan_Total
    Public JumlahBayar_Total
    Dim TanggalBayar
    Dim RekeningPenerima
    Dim AtasNamaPenerima
    Dim StatusInvoice
    Public StatusPengajuan
    Dim PPhTerutang
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim Catatan

    Dim JumlahInvoice
    Dim NomorInvoice_Bundel
    Dim TanggalInvoice_Bundel
    Dim NamaProduk_Bundel

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

    'Pajak-pajak Impor :
    Dim Jumlah_BeaMasuk_Impor As Int64
    Dim Jumlah_PPhPasal22_Impor As Int64
    Dim Jumlah_PPNMasukan_Impor As Int64

    Dim KodePajak

    Public NomorJV_Sebelumnya
    Public NomorJV

    Public PembayaranViaBank As Boolean
    Public PembayaranDenganInvoice As Boolean
    Public PembayaranTerjadwal As Boolean
    Public JumlahAngsuranTerjadwal
    Dim TabelJadwalAngsuran
    Dim KolomBPJadwalAngsuran

    Public JenisPajak
    Public KodeSetoran

    Dim BiayaProvisi

    Public AdaPenyimpanan As Boolean

    Public DenganPengajuan As Boolean
    Public TahunBukuNormalTanpaPengajuan As Boolean

    Dim MataUang As String
    Dim LokasiLawanTransaksi As String
    Dim MitraLokal As Boolean
    Dim MitraLuarNegeri As Boolean

    Public KodeMataUang As String
    Public MataUang_IDR As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If FungsiForm = FungsiForm_TAMBAH Then
                win_MetodeInputBayar = New wpfWin_MetodeInputBayar
                win_MetodeInputBayar.ShowDialog()
                DenganPengajuan = win_MetodeInputBayar.DenganPengajuan
                If Not win_MetodeInputBayar.LanjutkanProses Then Close()
            Else
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" SELECT Nomor_Bundel, Status_Invoice FROM tbl_BuktiPengeluaran " &
                                  " WHERE Angka_KK = '" & AngkaKK & "' ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                dr.Read()
                If (dr.Item("Nomor_Bundel") = Kosongan) And (dr.Item("Status_Invoice") = Status_Dibayar) Then
                    DenganPengajuan = False
                Else
                    DenganPengajuan = True
                End If
                AksesDatabase_Transaksi(Tutup)
            End If
        Else
            DenganPengajuan = False
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL And Not DenganPengajuan Then
            TahunBukuNormalTanpaPengajuan = True
        Else
            TahunBukuNormalTanpaPengajuan = False
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Pengajuan Pengeluaran Bank-Cash"
            SistemPenomoranOtomatis_KK()
            If JalurMasuk = JalurUtama Then
                cmb_Kategori.IsEnabled = True
                cmb_Peruntukan.IsEnabled = True
                btn_PilihMitra.IsEnabled = True
                txt_KodeLawanTransaksi.IsEnabled = True
                txt_NamaLawanTransaksi.IsEnabled = True
            Else
                cmb_Kategori.IsEnabled = False
                cmb_Peruntukan.IsEnabled = False
                btn_PilihMitra.IsEnabled = False
                txt_KodeLawanTransaksi.IsEnabled = False
                txt_NamaLawanTransaksi.IsEnabled = False
            End If
            btn_Singkirkan.Visibility = Visibility.Visible
        Else
            btn_PilihMitra.IsEnabled = False
            txt_KodeLawanTransaksi.IsEnabled = False
            txt_NamaLawanTransaksi.IsEnabled = False
        End If

        If FungsiForm = FungsiForm_EDIT _
            Or FungsiForm = FungsiForm_UPDATEPERSETUJUAN _
            Or FungsiForm = FungsiForm_POSTING _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            JudulForm = "Edit Pengajuan Pengeluaran Bank-Cash"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Angka_KK = '" & AngkaKK & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            NomorUrutInvoice = 0
            Do While dr.Read
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                JenisPajakPerBaris = dr.Item("Jenis_Pajak")     '2 baris ini disimpan paling awal untuk melancarkan penentuan COA Pajak
                KodeSetoranPerBaris = dr.Item("Kode_Setoran")   'Idem
                'Data Form :
                If NomorUrutInvoice < 1 Then
                    IsiValueComboBypassTerkunci(cmb_Kategori, dr.Item("Kategori"))
                    IsiValueComboBypassTerkunci(cmb_Peruntukan, dr.Item("Peruntukan"))
                    dtp_TanggalKK.SelectedDate = dr.Item("Tanggal_KK")
                    NomorBundel = dr.Item("Nomor_Bundel")
                    txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
                    txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
                    cmb_SaranaPembayaran.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
                    txt_BiayaAdministrasiBank.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Biaya_Administrasi_Bank"))
                    cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar")) 'Ini biarkan jangan pakai dtp_...!!!!
                    IsiValueElemenRichTextBox(txt_Catatan, dr.Item("Catatan"))
                    txt_NomorKK.Text = dr.Item("Nomor_KK") 'Supaya aman, ini disimpan di akhir sini.
                End If
                'Data Tabel :
                NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
                TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                UraianInvoicePerBaris = PenghapusEnter(dr.Item("Uraian_Invoice"))
                NomorBP = dr.Item("Nomor_BP")
                txt_Kurs.Text = dr.Item("Kurs")
                JumlahTagihanPerBaris = dr.Item("Jumlah_Tagihan")
                AngsuranKe = dr.Item("Angsuran_Ke")
                PokokPerBaris = dr.Item("Pokok")
                BagiHasilPerBaris = dr.Item("Bagi_Hasil")
                Perhitungan_SudahDibayarPerBaris()
                JumlahPengajuanPerBaris = dr.Item("Jumlah_Pengajuan")
                StatusPengajuan = dr.Item("Status_Pengajuan")
                LogikaPemilihanSaranaPembayaran()
                dtp_TanggalBayar.Text = Kosongan
                JumlahDisetujuiPerBaris = 0
                Select Case FungsiForm
                    Case FungsiForm_EDIT
                        btn_Singkirkan.Visibility = Visibility.Visible
                    Case FungsiForm_UPDATEPERSETUJUAN
                        If StatusPengajuan = Status_Disetujui Then
                            JumlahDisetujuiPerBaris = dr.Item("Jumlah_Bayar")
                        Else
                            JumlahDisetujuiPerBaris = JumlahPengajuanPerBaris
                        End If
                        btn_Tolak.Visibility = Visibility.Visible
                        btn_Simpan.Content = teks_Perbarui
                    Case FungsiForm_POSTING
                        JumlahDisetujuiPerBaris = dr.Item("Jumlah_Bayar")
                        If StatusPengajuan = Status_Disetujui Then
                            dtp_TanggalBayar.Text = Kosongan
                        Else
                            'Ini maksudnya dalam rangka ngedit data yang sudah pernah diposting ke Jurnal :
                            NomorJV_Sebelumnya = dr.Item("Nomor_JV")
                            txt_RekeningPenerima.Text = dr.Item("Rekening_Penerima")
                            txt_AtasNamaPenerima.Text = dr.Item("Atas_Nama_Penerima")
                            dtp_TanggalBayar.Text = TanggalBayar
                        End If
                        btn_Simpan.Content = teks_Posting
                    Case FungsiForm_LIHAT
                        JumlahDisetujuiPerBaris = dr.Item("Jumlah_Bayar")
                        dtp_TanggalBayar.Text = TanggalBayar
                        txt_RekeningPenerima.Text = dr.Item("Rekening_Penerima")
                        txt_AtasNamaPenerima.Text = dr.Item("Atas_Nama_Penerima")
                End Select
                If TahunBukuNormalTanpaPengajuan Then NomorJV_Sebelumnya = dr.Item("Nomor_JV")
                SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
                If Peruntukan = Peruntukan_PembayaranHutangUsaha_NonAfiliasi Then
                    If Not DenganPengajuan Then JumlahDisetujuiPerBaris = dr.Item("Jumlah_Bayar")
                    PPhTerutangPerBaris = AmbilValue_PPhTerutangBerdasarkanInvoicePembelian(NomorInvoicePerBaris, JumlahDisetujuiPerBaris)
                    PPhDitanggungPerBaris = AmbilValue_PPhDitanggungBerdasarkanInvoicePembelian(NomorInvoicePerBaris, JumlahDisetujuiPerBaris)
                    PPhDipotongPerBaris = AmbilValue_PPhDipotongBerdasarkanInvoicePembelian(NomorInvoicePerBaris, JumlahDisetujuiPerBaris)
                    PesanUntukProgrammer("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" & Enter2Baris &
                                         "Setelah selesai perbaikan data, Logika ini harus dihapus...!!!!!!" & Enter2Baris &
                                         "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")
                Else
                    PPhTerutangPerBaris = dr.Item("PPh_Terutang")
                    PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
                    PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
                End If
                txt_Denda.Text = dr.Item("Denda")
                If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                    txt_KodePajak.Text = dr.Item("Nomor_Invoice")
                    If FungsiForm = FungsiForm_POSTING Then
                        If StatusPengajuan = Status_Disetujui Then
                            txt_KodePajak.Text = Kosongan
                        End If
                    End If
                    If FungsiForm = FungsiForm_LIHAT Then
                        If StatusPengajuan = Status_Dibayar Then
                            lbl_KodePajak.Text = "Kode NTPN"
                        Else
                            lbl_KodePajak.Text = "Kode Billing"
                        End If
                        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then lbl_KodePajak.Text = "Kode NTPN"
                    End If
                End If
                TambahBaris()
            Loop
            AksesDatabase_Transaksi(Tutup)
            Perhitungan_Tabel()
        End If

        If FungsiForm = FungsiForm_UPDATEPERSETUJUAN _
            Or FungsiForm = FungsiForm_POSTING _
            Or FungsiForm = FungsiForm_LIHAT _
            Then
            Select Case FungsiForm
                Case FungsiForm_UPDATEPERSETUJUAN
                    JudulForm = "Persetujuan Pengajuan Pengeluaran Bank-Cash"
                Case FungsiForm_POSTING
                    JudulForm = "Posting Jurnal Pengeluaran Bank-Cash"
                Case FungsiForm_LIHAT
                    JudulForm = "Pengajuan Pengeluaran Bank-Cash"
            End Select
            txt_NomorKK.IsEnabled = False
            dtp_TanggalKK.IsEnabled = False
            btn_PilihMitra.IsEnabled = False
            cmb_SaranaPembayaran.IsEnabled = False
            LogikaVisibilitasBank()
            lbl_Denda.IsEnabled = False
            txt_Denda.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
            Jumlah_Disetujui_Per_Baris.Visibility = Visibility.Visible
        Else
            If Kategori <> Kategori_Pemindahbukuan Then
                dtp_TanggalKK.IsEnabled = True
                cmb_SaranaPembayaran.IsEnabled = True
            End If
            If JalurMasuk = JalurUtama Then btn_PilihMitra.IsEnabled = True
            If TahunBukuNormalTanpaPengajuan Then
                If COATermasukBank(COAKredit) Then grb_Bank.Visibility = Visibility.Visible
            Else
                grb_Bank.Visibility = Visibility.Collapsed
            End If
            btn_Singkirkan.IsEnabled = True
            Jumlah_Disetujui_Per_Baris.Visibility = Visibility.Collapsed
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            JudulForm = "Bukti Pengeluaran Bank-Cash"
            grb_Bank.IsEnabled = False
            lbl_KodePajak.IsEnabled = False
            txt_KodePajak.IsEnabled = False
            dtp_TanggalBayar.IsEnabled = False
            txt_Catatan.IsEnabled = False
            btn_Cetak.Visibility = Visibility.Visible
            btn_Simpan.IsEnabled = False
            btn_Batal.Content = teks_Tutup
            If StatusPengajuan = Status_Dibayar Or StatusPengajuan = Status_Disetujui Then
                Jumlah_Disetujui_Per_Baris.Visibility = Visibility.Visible
            Else
                Jumlah_Disetujui_Per_Baris.Visibility = Visibility.Collapsed
            End If
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        If TahunBukuNormalTanpaPengajuan Then
            If FungsiForm = FungsiForm_TAMBAH Then JudulForm = "Input Pengeluaran Bank-Cash"
            If FungsiForm = FungsiForm_EDIT Then JudulForm = "Edit Pengeluaran Bank-Cash"
        End If

        Title = JudulForm

        LogikaTampilanKolom()

        If FungsiForm = FungsiForm_TAMBAH _
            Or FungsiForm = FungsiForm_EDIT _
            Or (FungsiForm = FungsiForm_LIHAT And (StatusPengajuan = Status_Open Or StatusPengajuan = Status_Dicetak Or StatusPengajuan = Status_Dibundel)) _
            Then
            VisibilitasTotalDisetujui(False)
        End If

        If FungsiForm = FungsiForm_POSTING Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) Then
            lbl_TanggalBayar.Visibility = Visibility.Visible
            dtp_TanggalBayar.Visibility = Visibility.Visible
            If MitraLokal Then
                lbl_TotalDisetujui_IDR.Text = "Total Bayar"
            Else
                lbl_TotalDisetujui_IDR.Text = "Total Bayar (IDR)"
            End If
        End If

        If FungsiForm = FungsiForm_POSTING And Not TahunBukuNormalTanpaPengajuan Then txt_Kurs.IsReadOnly = True

        If FungsiForm = FungsiForm_TAMBAH And TahunBukuNormalTanpaPengajuan Then
            lbl_KodePajak.Text = "Kode NTPN"
            Perhitungan_Tabel()
        End If

        ProsesLoadingForm = False

        BersihkanSeleksi()

        If KodeMataUang = Kosongan Then
            PesanUntukProgrammer("Kode Mata Uang belum ditentukan...!!!")
            KodeMataUang = KodeMataUang_IDR
        End If

    End Sub

    Sub LogikaPemilihanSaranaPembayaran()
        If COATermasukBank(COAKredit) Then
            If Peruntukan = Peruntukan_BankGaransi Then
                txt_RekeningPenerima.Text = Kosongan
                txt_AtasNamaPenerima.Text = Kosongan
            Else
                txt_RekeningPenerima.Text = AmbilValue_RekeningMitra(KodeLawanTransaksi)
                txt_AtasNamaPenerima.Text = AmbilValue_AtasNamaRekeningMitra(KodeLawanTransaksi)
            End If
            If Kategori = Kategori_Pemindahbukuan Then
                If COATermasukBank(COADebet) Then txt_RekeningPenerima.Text = AmbilValue_NamaAkun(COADebet)
                lbl_RekeningPenerima.Visibility = Visibility.Collapsed
                txt_RekeningPenerima.Visibility = Visibility.Collapsed
                lbl_AtasNamaPenerima.Visibility = Visibility.Collapsed
                txt_AtasNamaPenerima.Visibility = Visibility.Collapsed
            End If
        End If
    End Sub

    Sub LogikaTampilanKolom()
        If PembayaranTerjadwal Then
            Angsuran_Ke.Visibility = Visibility.Visible
            Uraian_Invoice_Per_Baris.Visibility = Visibility.Collapsed
            Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
            Pokok_Per_Baris.Visibility = Visibility.Visible
            Bagi_Hasil_Per_Baris.Visibility = Visibility.Visible
            Sudah_Dibayar_Per_Baris.Visibility = Visibility.Collapsed
            Sisa_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
            If PPhTerutang > 0 Then
                Jenis_Pajak_Per_Baris.Visibility = Visibility.Visible
                PPh_Terutang_Per_Baris.Visibility = Visibility.Visible
                PPh_Ditanggung_Per_Baris.Visibility = Visibility.Visible
                PPh_Dipotong_Per_Baris.Visibility = Visibility.Visible
            Else
                Jenis_Pajak_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Terutang_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Ditanggung_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Dipotong_Per_Baris.Visibility = Visibility.Collapsed
            End If
        Else
            Angsuran_Ke.Visibility = Visibility.Collapsed
            If PembayaranDenganInvoice = True Then
                Nomor_Invoice_Per_Baris.Visibility = Visibility.Visible
                Tanggal_Invoice_Per_Baris.Visibility = Visibility.Visible
            Else
                Nomor_Invoice_Per_Baris.Visibility = Visibility.Collapsed
                Tanggal_Invoice_Per_Baris.Visibility = Visibility.Collapsed
            End If
            Uraian_Invoice_Per_Baris.Visibility = Visibility.Visible
            Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Visible
            Pokok_Per_Baris.Visibility = Visibility.Collapsed
            Bagi_Hasil_Per_Baris.Visibility = Visibility.Collapsed
            Sudah_Dibayar_Per_Baris.Visibility = Visibility.Visible
            Sisa_Tagihan_Per_Baris.Visibility = Visibility.Visible
            If LevelUserAktif < LevelUser_99_AppDeveloper Then
                Jenis_Pajak_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Terutang_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Ditanggung_Per_Baris.Visibility = Visibility.Collapsed
                PPh_Dipotong_Per_Baris.Visibility = Visibility.Collapsed
            Else
                Jenis_Pajak_Per_Baris.Visibility = Visibility.Visible
                PPh_Terutang_Per_Baris.Visibility = Visibility.Visible
                PPh_Ditanggung_Per_Baris.Visibility = Visibility.Visible
                PPh_Dipotong_Per_Baris.Visibility = Visibility.Visible
            End If
        End If
        If Kategori = Kategori_PengeluaranTunai _
            Or Kategori = Kategori_Pemindahbukuan _
            Or Kategori = Kategori_Investasi _
            Then
            Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
            Pokok_Per_Baris.Visibility = Visibility.Collapsed
            Bagi_Hasil_Per_Baris.Visibility = Visibility.Collapsed
            Sudah_Dibayar_Per_Baris.Visibility = Visibility.Collapsed
            Sisa_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
            If Peruntukan = Peruntukan_DepositOperasional Then
                Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Visible
                Sudah_Dibayar_Per_Baris.Visibility = Visibility.Visible
                Sisa_Tagihan_Per_Baris.Visibility = Visibility.Visible
            End If
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Or DenganPengajuan = False Then
            Jumlah_Pengajuan_Per_Baris.Header = "Jumlah" & Enter1Baris & "Bayar"
            If MitraLokal Then
                lbl_TotalPengajuan_IDR.Text = "Total Bayar"
            Else
                lbl_TotalPengajuan_IDR.Text = "Total Bayar (IDR)"
            End If
            lbl_TotalPengajuan_MUA.Text = "Total Bayar (" & KodeMataUang & ")"
        Else
            Jumlah_Pengajuan_Per_Baris.Header = "Jumlah" & Enter1Baris & "Pengajuan"
            If MitraLokal Then
                lbl_TotalPengajuan_IDR.Text = "Total Pengajuan"
            Else
                lbl_TotalPengajuan_IDR.Text = "Total Pengajuan (IDR)"
            End If
            lbl_TotalPengajuan_MUA.Text = "Total Pengajuan (" & KodeMataUang & ")"
        End If
        If Peruntukan = Peruntukan_PembayaranHutangPajak Then
            If JenisPajak = JenisPajak_PajakPajakImpor Then
                Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
                Sudah_Dibayar_Per_Baris.Visibility = Visibility.Collapsed
                Sisa_Tagihan_Per_Baris.Visibility = Visibility.Collapsed
            Else
                Jumlah_Tagihan_Per_Baris.Visibility = Visibility.Visible
                Sudah_Dibayar_Per_Baris.Visibility = Visibility.Visible
                Sisa_Tagihan_Per_Baris.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub LogikaLokasiLawanTransaksi()
        txt_TotalPengajuan_MUA.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
        txt_TotalPengajuan_IDR.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        txt_TotalDisetujui_MUA.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
        txt_TotalDisetujui_IDR.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        If MitraLokal Then
            MataUang_IDR = True
            Buat_DataTabelUtama_Lokal()
            VisibilitasKolomKurs(False)
            MataUang = MataUang_Rupiah
            txt_Denda.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_Kurs.Text = 1
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(Pokok)
            FormatUlangAngkaKeBilanganBulat(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahPengajuanPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahDisetujuiPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahDisetujuiPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahPengajuanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahDisetujuiPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahDisetujuiPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihan)
            FormatUlangAngkaKeBilanganBulat(SudahDibayar)
            FormatUlangAngkaKeBilanganBulat(SisaTagihan)
            FormatUlangAngkaKeBilanganBulat(JumlahPengajuan_Tabel)
            FormatUlangAngkaKeBilanganBulat(JumlahBayar_Tabel)
            FormatUlangAngkaKeBilanganBulat(BiayaAdministrasiBank)
        Else
            MataUang_IDR = False
            Buat_DataTabelUtama_Asing()
            VisibilitasKolomKurs(True)
            MataUang = MataUang_Asing
            txt_Denda.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Kurs.Text = Kosongan
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(Pokok)
            FormatUlangAngkaKeBilanganDesimal(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahPengajuanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahDisetujuiPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(JumlahPengajuanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(JumlahDisetujuiPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihan)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayar)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihan)
            FormatUlangAngkaKeBilanganDesimal(JumlahPengajuan_Tabel)
            FormatUlangAngkaKeBilanganDesimal(JumlahBayar_Tabel)
            FormatUlangAngkaKeBilanganDesimal(BiayaAdministrasiBank)
        End If
    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        KodeMataUang = Kosongan
        MataUang_IDR = True
        AngkaKK = 0
        MataUang = MataUang_Rupiah
        Kurs = 1
        VisibilitasKolomKurs(False)
        MitraLokal = True
        MitraLuarNegeri = False

        KontenCombo_Kategori()
        cmb_Peruntukan.Items.Clear()    'Jangan dihapus..!
        cmb_Peruntukan.SelectedValue = Kosongan  'Jangan dihapus..!
        NomorUrutInvoice = 0
        txt_NomorKK.IsEnabled = False
        txt_NomorKK.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalKK)
        NomorBundel = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPembayaran, KodeMataUang)
        COADebet = Kosongan
        COAKredit = Kosongan
        Reset_grb_Bank()
        KosongkanDaftarTagihan()
        txt_Kurs.Text = 1
        txt_Denda.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalBayar)
        lbl_RekeningPenerima.Visibility = Visibility.Visible
        txt_RekeningPenerima.Visibility = Visibility.Visible
        lbl_AtasNamaPenerima.Visibility = Visibility.Visible
        txt_AtasNamaPenerima.Visibility = Visibility.Visible
        lbl_RekeningPenerima.IsEnabled = False
        txt_RekeningPenerima.IsEnabled = False
        lbl_AtasNamaPenerima.IsEnabled = False
        txt_AtasNamaPenerima.IsEnabled = False
        txt_RekeningPenerima.Text = Kosongan
        txt_AtasNamaPenerima.Text = Kosongan
        StatusInvoice = Kosongan
        StatusPengajuan = Kosongan
        KosongkanValueElemenRichTextBox(txt_Catatan)

        LogikaTombol_Cetak()

        cmb_Peruntukan.IsEnabled = True
        dtp_TanggalKK.IsEnabled = True
        btn_PilihMitra.IsEnabled = True
        cmb_SaranaPembayaran.IsEnabled = True
        txt_Kurs.IsReadOnly = False 'Ini jangan dihapus. Ini sebagai default awal, karena ada perubahan disesuaikan kondisi.
        lbl_Denda.Visibility = Visibility.Collapsed
        txt_Denda.Visibility = Visibility.Collapsed
        lbl_KodePajak.Visibility = Visibility.Collapsed
        txt_KodePajak.Visibility = Visibility.Collapsed
        lbl_Denda.IsEnabled = True
        txt_Denda.IsEnabled = True
        lbl_KodePajak.IsEnabled = True
        txt_KodePajak.IsEnabled = True
        txt_KodePajak.Text = Kosongan
        VisibilitasTotalPengajuan(True)
        VisibilitasTotalDisetujui(True)
        lbl_TotalDisetujui_IDR.Text = "Total Disetujui"
        lbl_TanggalBayar.Visibility = Visibility.Collapsed
        dtp_TanggalBayar.Visibility = Visibility.Collapsed

        VisibilitasTabel(False)
        btn_Simpan.IsEnabled = True
        btn_Simpan.Content = teks_Simpan
        btn_Cetak.Visibility = Visibility.Collapsed
        btn_Batal.Content = teks_Batal
        btn_Singkirkan.Visibility = Visibility.Collapsed
        btn_Tolak.Visibility = Visibility.Collapsed

        NomorInvoice_Bundel = Kosongan
        TanggalInvoice_Bundel = Kosongan
        NamaProduk_Bundel = Kosongan

        NomorID = 0
        NomorJV = 0

        PembayaranViaBank = False
        PembayaranDenganInvoice = True
        PembayaranTerjadwal = False
        JumlahAngsuranTerjadwal = 0

        JenisPajak = Kosongan
        KodeSetoran = Kosongan

        Jumlah_BeaMasuk_Impor = 0
        Jumlah_PPhPasal22_Impor = 0
        Jumlah_PPNMasukan_Impor = 0

        BiayaProvisi = 0

        AdaPenyimpanan = False

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_KK()

        If FungsiForm = FungsiForm_TAMBAH Then AngkaKK = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_BuktiPengeluaran", "Angka_KK") + 1
        If dtp_TanggalKK.Text <> Kosongan Then
            NomorKK = AwalanKK & AngkaKK & "/" & KonversiSaranaPembayaranKeNamaAkun(SaranaPembayaran) & "/" &
                    KonversiAngkaKeStringDuaDigit(dtp_TanggalKK.SelectedDate.Value.Month) & "/" & TahunBukuAktif
            txt_NomorKK.Text = NomorKK
        End If

    End Sub


    Sub KodePembukaDataTagihan()
        KosongkanDaftarTagihan()
        NomorUrutInvoice = 0
        JumlahDisetujuiPerBaris = 0
        JenisPajakPerBaris = Kosongan
        KodeSetoranPerBaris = Kosongan
        PPhTerutangPerBaris = 0
        PPhDitanggungPerBaris = 0
        PPhDipotongPerBaris = 0
        AksesDatabase_Transaksi(Buka)
    End Sub

    Sub KodePenutupDataTagihan()
        AksesDatabase_Transaksi(Tutup)
        Perhitungan_Tabel()
        BersihkanSeleksi()
        If Not (ProsesLoadingForm Or ProsesResetForm Or ProsesIsiValueForm) _
            Or JalurMasuk <> JalurUtama _
            Then
        End If
    End Sub

    Sub TambahkanDataTagihanHutangUsaha()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_Pembelian_Invoice " &
            " WHERE Kode_Supplier     = '" & KodeLawanTransaksi & "' " &
            " AND Jenis_Pembelian     = '" & JenisPembelian_Tempo & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_Pembelian = '" & KonversiNomorBPHUKeNomorPembelian(NomorBP) & "' "
        QueryTampilan &= " ORDER BY Angka_Invoice, Nomor_ID "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorInvoice_Sebelumnya = Kosongan
        Dim Retur As Int64
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Catatan"))
            NomorBP = KonversiNomorPembelianKeNomorBPHU(dr.Item("Nomor_Pembelian"))
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            JumlahTagihanPerBaris = dr.Item("Total_Tagihan") - Retur
            JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
            KodeSetoranPerBaris = dr.Item("Kode_Setoran")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
            JumlahPengajuanPerBaris = SisaTagihanPerBaris
            PPhTerutangPerBaris = dr.Item("PPh_Terutang")
            PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
            PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
            RasioPPh()
            If NomorInvoicePerBaris <> NomorInvoice_Sebelumnya And SisaTagihanPerBaris > 0 Then TambahBaris()
            NomorInvoice_Sebelumnya = NomorInvoicePerBaris
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanHutangUsahaAfiliasi()
        TambahkanDataTagihanHutangUsaha() 'Sementara ini ngambil dari sub ini saja dulu. Karena sama saja algoritmanya.
    End Sub


    Sub TambahkanDataTagihanHutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPemegangSaham WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHPS")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
            JumlahPengajuanPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangKaryawan WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHK")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
            JumlahPengajuanPerBaris = SisaTagihanPerBaris
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHB")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                         " WHERE Nomor_BPHB = '" & NomorBP & "' " &
                                         " AND Tanggal_Bayar = '" & TanggalKosongSimpan & "' " &
                                         " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
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
                JumlahPengajuanPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                JumlahDisetujuiPerBaris = 0
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHL")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangLeasing " &
                                         " WHERE Nomor_BPHL = '" & NomorBP & "' " &
                                         " AND Tanggal_Bayar = '" & TanggalKosongSimpan & "' " &
                                         " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                AngsuranKe = drTELUSUR.Item("Angsuran_Ke")
                PokokPerBaris = drTELUSUR.Item("Pokok")
                BagiHasilPerBaris = drTELUSUR.Item("Bagi_Hasil")
                PPhTerutangPerBaris = drTELUSUR.Item("Jumlah_PPh")
                PPhDitanggungPerBaris = drTELUSUR.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = drTELUSUR.Item("PPh_Dipotong")
                JumlahPengajuanPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                JumlahDisetujuiPerBaris = 0
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangPihakKetiga()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangPihakKetiga " &
                                         " WHERE Nomor_BPHPK = '" & NomorBP & "' " &
                                         " AND Tanggal_Bayar = '" & TanggalKosongSimpan & "' " &
                                         " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
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
                JumlahPengajuanPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                JumlahDisetujuiPerBaris = 0
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangAfiliasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangAfiliasi " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHA = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHA")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                         " WHERE Nomor_BPHA = '" & NomorBP & "' " &
                                         " AND Tanggal_Bayar = '" & TanggalKosongSimpan & "' " &
                                         " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
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
                JumlahPengajuanPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                JumlahDisetujuiPerBaris = 0
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataTagihanHutangDividen()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangDividen WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHD = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Akta_Notaris")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHD")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Dividen") - dr.Item("PPh_Dipotong")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
            JumlahPengajuanPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPengeluaranPiutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangPemegangSaham " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPPS")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPengeluaranPiutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangKaryawan " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPengeluaranPiutangPihakKetiga()
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPPK")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPengeluaranPiutangAfiliasi()
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPA")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataDepositOperasional()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_DepositOperasional " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPDO = '" & NomorBP & "' "
        QueryTampilan &= "  ORDER BY Angka_BPDO "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorBP_Sebelumnya = Kosongan
        Do While dr.Read
            NomorBP = dr.Item("Nomor_BPDO")
            If NomorBP <> NomorBP_Sebelumnya Then
                NomorInvoicePerBaris = dr.Item("Nomor_Bukti")
                TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
                UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
                JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
                Perhitungan_SudahDibayarPerBaris()
                SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
                JumlahPengajuanPerBaris = SisaTagihanPerBaris
                If SisaTagihanPerBaris > 0 Then TambahBaris()
            End If
            NomorBP_Sebelumnya = NomorBP
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPBG")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
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
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPAL")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Transaksi")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahPengajuanPerBaris = JumlahTagihanPerBaris
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub



    Sub RasioPPh()
        Dim PPhTerutang_SudahDibayar = 0
        Dim PPhDitanggung_SudahDibayar = 0
        Dim PPhDipotong_SudahDibayar = 0
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                     " WHERE Nomor_BP       = '" & NomorBP & "' " &
                                     " AND Kode_Setoran     = '" & KodeSetoranPerBaris & "' " &
                                     " AND Angka_KK       < '" & AngkaKK & "' " &
                                     " AND Status_Pengajuan = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
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
        NomorUrutInvoice += 1
        datatabelUtama.Rows.Add(NomorUrutInvoice, NomorInvoicePerBaris, TanggalInvoicePerBaris, UraianInvoicePerBaris, NomorBP,
                                JumlahTagihanPerBaris, AngsuranKe, PokokPerBaris, BagiHasilPerBaris, SudahDibayarPerBaris, SisaTagihanPerBaris,
                                JenisPajakPerBaris, KodeSetoranPerBaris, PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris,
                                JumlahPengajuanPerBaris, JumlahDisetujuiPerBaris,
                                PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris)
        PesanUntukProgrammer("Jenis Pajak : " & JenisPajak & Enter2Baris &
                             "Jenis Pajak Perbaris : " & JenisPajakPerBaris)
    End Sub


    Sub BarisTotal()
        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, " T O T A L ", Kosongan,
                                JumlahTagihan, Kosongan, Pokok, BagiHasil, SudahDibayar, SisaTagihan,
                                Kosongan, Kosongan, PPhTerutang, PPhDitanggung, PPhDipotong,
                                JumlahPengajuan_Tabel, JumlahBayar_Tabel)
    End Sub


    Sub Perhitungan_SudahDibayarPerBaris()
        SudahDibayarPerBaris = 0
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP         = '" & NomorBP & "' " &
                                   " AND Kode_Setoran       = '" & KodeSetoranPerBaris & "' " &
                                   " AND Angka_KK           < '" & AngkaKK & "' " &
                                   " AND Status_Invoice     = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
        drBAYAR_ExecuteReader()
        Do While drBAYAR.Read
            SudahDibayarPerBaris += drBAYAR.Item("Jumlah_Bayar")
        Loop
    End Sub


    Sub Perhitungan_Tabel()
        JumlahTagihan = 0
        Pokok = 0
        BagiHasil = 0
        SudahDibayar = 0
        SisaTagihan = 0
        JumlahPengajuan_Tabel = 0
        JumlahBayar_Tabel = 0
        PPhTerutang = 0
        PPhDitanggung = 0
        PPhDipotong = 0
        JumlahInvoice = NomorUrutInvoice
        JumlahAngsuranTerjadwal = JumlahInvoice
        If MitraLuarNegeri Then
            FormatUlangAngkaKeBilanganDesimal(JumlahBayar_Tabel)
        End If
        If datatabelUtama.Rows.Count > JumlahInvoice Then
            PesanUntukProgrammer("Ada algoritma remove row, di sini." &
                                 Enter2Baris & "Entah apa maksud dan alasannya? Lupa lagi euy." &
                                 Enter2Baris & "Sub : Perhitungan_Tabel()")
            datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
            datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
        End If
        For Each row As DataRow In datatabelUtama.Rows
            If PembayaranTerjadwal Then
                If MitraLokal Then JumlahTagihan = AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
                If MitraLuarNegeri Then JumlahTagihan = AmbilAngka_Asing(row("Jumlah_Tagihan_Per_Baris").ToString)
            Else
                If MitraLokal Then JumlahTagihan += AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
                If MitraLuarNegeri Then JumlahTagihan += AmbilAngka_Asing(row("Jumlah_Tagihan_Per_Baris").ToString)
            End If
            If MitraLokal Then
                Pokok += AmbilAngka(row("Pokok_Per_Baris").ToString)
                BagiHasil += AmbilAngka(row("Bagi_Hasil_Per_Baris").ToString)
                SudahDibayar += AmbilAngka(row("Sudah_Dibayar_Per_Baris").ToString)
                SisaTagihan += AmbilAngka(row("Sisa_Tagihan_Per_Baris").ToString)
                JumlahPengajuan_Tabel += AmbilAngka(row("Jumlah_Pengajuan_Per_Baris").ToString)
                JumlahBayar_Tabel += AmbilAngka(row("Jumlah_Disetujui_Per_Baris").ToString)
            Else
                Pokok += AmbilAngka_Asing(row("Pokok_Per_Baris").ToString)
                BagiHasil += AmbilAngka_Asing(row("Bagi_Hasil_Per_Baris").ToString)
                SudahDibayar += AmbilAngka_Asing(row("Sudah_Dibayar_Per_Baris").ToString)
                SisaTagihan += AmbilAngka_Asing(row("Sisa_Tagihan_Per_Baris").ToString)
                JumlahPengajuan_Tabel += AmbilAngka_Asing(row("Jumlah_Pengajuan_Per_Baris").ToString)
                JumlahBayar_Tabel += AmbilAngka_Asing(row("Jumlah_Disetujui_Per_Baris").ToString)
            End If
            PPhTerutang += AmbilAngka(row("PPh_Terutang_Per_Baris").ToString)
            PPhDitanggung += AmbilAngka(row("PPh_Ditanggung_Per_Baris").ToString)
            PPhDipotong += AmbilAngka(row("PPh_Dipotong_Per_Baris").ToString)
        Next
        If JumlahInvoice = 0 Then
            VisibilitasTabel(False)
        Else
            VisibilitasTabel(True)
            If JumlahInvoice > 1 Then BarisTotal()
        End If
        Perhitungan_Total()
        LogikaTampilanKolom()
        LogikaTombol_Cetak()
    End Sub
    Sub Perhitungan_Total()
        Dim JumlahPengajuan_Tabel_IDR As Int64 = AmbilValue_NilaiMataUang_BulatKeAtas(KodeMataUang, Kurs, JumlahPengajuan_Tabel)
        Dim JumlahBayar_Tabel_IDR As Int64 = AmbilValue_NilaiMataUang_BulatKeAtas(KodeMataUang, Kurs, JumlahBayar_Tabel)
        txt_TotalPengajuan_IDR.Text = JumlahPengajuan_Tabel_IDR + Denda
        txt_TotalPengajuan_MUA.Text = JumlahPengajuan_Tabel + Denda
        If FungsiForm = FungsiForm_UPDATEPERSETUJUAN _
            Or FungsiForm = FungsiForm_POSTING _
            Or (FungsiForm = FungsiForm_LIHAT And (StatusPengajuan = Status_Disetujui Or StatusPengajuan = Status_Dibayar)) _
            Or TahunBukuNormalTanpaPengajuan _
            Then
            If TahunBukuNormalTanpaPengajuan Then
                JumlahBayar_Tabel = JumlahPengajuan_Tabel
                JumlahBayar_Tabel_IDR = JumlahPengajuan_Tabel_IDR
            End If
            If JumlahBayar_Tabel = 0 Then
                txt_TotalDisetujui_IDR.Text = 0
                txt_TotalDisetujui_MUA.Text = 0
            Else
                txt_TotalDisetujui_IDR.Text = FormatUlangInt64(JumlahBayar_Tabel_IDR + Denda)
                If Not MitraLokal Then txt_TotalDisetujui_MUA.Text = JumlahBayar_Tabel + Denda
            End If
        End If
        Perhitungan_ValueBank()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Singkirkan.IsEnabled = False
        btn_Tolak.IsEnabled = False
    End Sub

    Sub VisibilitasTabel(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_Kanan.Visibility = Visibility.Visible
            pnl_Pemisah.Visibility = Visibility.Visible
        Else
            pnl_Kanan.Visibility = Visibility.Collapsed
            pnl_Pemisah.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomKurs(Visibilitas As Boolean)
        lbl_Kurs.Visibility = Visibility.Collapsed
        txt_Kurs.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_Kurs.Visibility = Visibility.Visible
            txt_Kurs.Visibility = Visibility.Visible
        Else
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTotalPengajuan(Visibilitas As Boolean)
        txt_TotalPengajuan_MUA.Visibility = Visibility.Collapsed
        lbl_TotalPengajuan_MUA.Visibility = Visibility.Collapsed
        lbl_TotalPengajuan_IDR.Visibility = Visibility.Collapsed
        txt_TotalPengajuan_IDR.Visibility = Visibility.Collapsed
        If MitraLokal Then
            If FungsiForm = FungsiForm_POSTING Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) Or (Not DenganPengajuan) Then
                lbl_TotalPengajuan_IDR.Text = "Total Bayar"
            Else
                lbl_TotalPengajuan_IDR.Text = "Total Pengajuan"
            End If
        Else
            If FungsiForm = FungsiForm_POSTING Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) Or (Not DenganPengajuan) Then
                lbl_TotalPengajuan_MUA.Text = "Total Bayar (" & KodeMataUang & ")"
                lbl_TotalPengajuan_IDR.Text = "Total Bayar (IDR)"
            Else
                lbl_TotalPengajuan_MUA.Text = "Total Pengajuan (" & KodeMataUang & ")"
                lbl_TotalPengajuan_IDR.Text = "Total Pengajuan (IDR)"
            End If
        End If
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_TotalPengajuan_IDR.Visibility = Visibility.Visible
                txt_TotalPengajuan_IDR.Visibility = Visibility.Visible
            End If
            If Not MitraLokal Then
                txt_TotalPengajuan_MUA.Visibility = Visibility.Visible
                lbl_TotalPengajuan_MUA.Visibility = Visibility.Visible
            End If
        Else
            lbl_TotalPengajuan_IDR.Visibility = Visibility.Collapsed
            txt_TotalPengajuan_IDR.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTotalDisetujui(Visibilitas As Boolean)
        lbl_TotalDisetujui_IDR.Visibility = Visibility.Collapsed
        txt_TotalDisetujui_IDR.Visibility = Visibility.Collapsed
        lbl_TotalDisetujui_MUA.Visibility = Visibility.Collapsed
        txt_TotalDisetujui_MUA.Visibility = Visibility.Collapsed
        If MitraLokal Then
            If FungsiForm = FungsiForm_POSTING Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) Or (Not DenganPengajuan) Then
                lbl_TotalDisetujui_IDR.Text = "Total Bayar"
            Else
                lbl_TotalDisetujui_IDR.Text = "Total Disetujui"
            End If
        Else
            If FungsiForm = FungsiForm_POSTING Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) Or (Not DenganPengajuan) Then
                lbl_TotalDisetujui_MUA.Text = "Total Bayar (" & KodeMataUang & ")"
                lbl_TotalDisetujui_IDR.Text = "Total Bayar (IDR)"
            Else
                lbl_TotalDisetujui_MUA.Text = "Total Disetujui (" & KodeMataUang & ")"
                lbl_TotalDisetujui_IDR.Text = "Total Disetujui (IDR)"
            End If
        End If
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                lbl_TotalDisetujui_IDR.Visibility = Visibility.Visible
                txt_TotalDisetujui_IDR.Visibility = Visibility.Visible
            End If
            If Not MitraLokal Then
                lbl_TotalDisetujui_MUA.Visibility = Visibility.Visible
                txt_TotalDisetujui_MUA.Visibility = Visibility.Visible
            End If
        Else
            lbl_TotalDisetujui_IDR.Visibility = Visibility.Collapsed
            txt_TotalDisetujui_IDR.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasBiayaAdministrasiBank(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_BiayaAdministrasiBank.Visibility = Visibility.Visible
            lbl_DitanggungOleh.Visibility = Visibility.Visible
            lbl_TotalBank.Visibility = Visibility.Visible
            txt_BiayaAdministrasiBank.Visibility = Visibility.Visible
            cmb_DitanggungOleh.Visibility = Visibility.Visible
            txt_TotalBank.Visibility = Visibility.Visible
        Else
            lbl_BiayaAdministrasiBank.Visibility = Visibility.Collapsed
            lbl_DitanggungOleh.Visibility = Visibility.Collapsed
            lbl_TotalBank.Visibility = Visibility.Collapsed
            txt_BiayaAdministrasiBank.Visibility = Visibility.Collapsed
            cmb_DitanggungOleh.Visibility = Visibility.Collapsed
            txt_TotalBank.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub KontenCombo_Kategori()
        KontenCombo_KategoriPengeluaran_Public(cmb_Kategori)
        cmb_Kategori.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPembayaranHutang()
        KontenCombo_PeruntukanPembayaranHutang_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPengeluaranTunai()
        KontenCombo_PeruntukanPengeluaranTunai_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPemindahbukuan()
        KontenCombo_PeruntukanPemindahbukuan_Pulic(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanInvestasi()
        KontenCombo_PeruntukanInvestasi_Public(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub



    Private Sub cmb_Kategori_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kategori.SelectionChanged
        Kategori = cmb_Kategori.SelectedValue
        Select Case Kategori
            Case Kategori_PembayaranHutang
                KontenCombo_PeruntukanPembayaranHutang()
            Case Kategori_PengeluaranTunai
                KontenCombo_PeruntukanPengeluaranTunai()
            Case Kategori_Pemindahbukuan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pemindahbukuan, silakan melalui Buku Pengawasan terkait.")
                    Return
                Else
                    KontenComboSaranaPembayaran_Public_WPF(cmb_Peruntukan, KodeMataUang)
                End If
            Case Kategori_Investasi
                KontenCombo_PeruntukanInvestasi()
            Case Kosongan
                cmb_Peruntukan.Items.Clear()
                cmb_Peruntukan.SelectedValue = Kosongan
            Case Kategori_Try
                KodePembukaDataTagihan()
                NomorUrutInvoice = 0
                Do While NomorUrutInvoice <= 50
                    NomorUrutInvoice += 1
                    datatabelUtama.Rows.Add(NomorUrutInvoice, "Percobaan", "Apa weeeeh...!")
                Loop
                KodePenutupDataTagihan()
        End Select
    End Sub

    Private Sub cmb_Peruntukan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Peruntukan.SelectionChanged

        Peruntukan = cmb_Peruntukan.SelectedValue

        If Peruntukan <> Kosongan Then
            If Kategori = Kategori_Pemindahbukuan Then
                COADebet = KonversiSaranaPembayaranKeCOA(Peruntukan)
            Else
                If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                    COADebet = PenentuanCOA_HutangPajak(JenisPajak, KodeSetoran)
                    If ProsesLoadingForm = True Then COADebet = PenentuanCOA_HutangPajak(JenisPajakPerBaris, KodeSetoranPerBaris)
                Else
                    COADebet = KonversiPeruntukanKeCOA(Peruntukan)
                End If
            End If
        End If

        'PesanUntukProgrammer("COA Debet : " & COADebet)

        KosongkanDaftarTagihan()

        txt_KodeLawanTransaksi.Text = Kosongan

        PembayaranDenganInvoice = True
        PembayaranTerjadwal = False

        Nomor_Invoice_Per_Baris.Header = "Nomor Dokumen"
        Tanggal_Invoice_Per_Baris.Header = "Tanggal Dokumen"
        TabelJadwalAngsuran = Kosongan
        KolomBPJadwalAngsuran = Kosongan

        VisibilitasBiayaAdministrasiBank(True)
        Select Case Peruntukan
            'PEMBAYARAN HUTANG : -----------------------------------
            Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                Nomor_Invoice_Per_Baris.Header = "Nomor Invoice"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Invoice"
            Case Peruntukan_PembayaranHutangUsaha_Afiliasi
                Nomor_Invoice_Per_Baris.Header = "Nomor Invoice"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Invoice"
            Case Peruntukan_PembayaranHutangBank
                Nomor_Invoice_Per_Baris.Header = "Nomor Kontrak"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Kontrak"
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangBank"
                KolomBPJadwalAngsuran = "Nomor_BPHB"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangLeasing
                Nomor_Invoice_Per_Baris.Header = "Nomor Kontrak"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Kontrak"
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangLeasing"
                KolomBPJadwalAngsuran = "Nomor_BPHL"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangPihakKetiga
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangPihakKetiga"
                KolomBPJadwalAngsuran = "Nomor_BPHPK"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangAfiliasi
                TabelJadwalAngsuran = "tbl_JadwalAngsuranHutangAfiliasi"
                KolomBPJadwalAngsuran = "Nomor_BPHA"
                PembayaranTerjadwal = True
            Case Peruntukan_PembayaranHutangGaji
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Gaji, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangBPJSKesehatan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang BPJS Kesehatan, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang BPJS Ketenagakerjaan, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangKoperasiKaryawan
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Koperasi Karyawan, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangSerikat
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Serikat, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
            Case Peruntukan_PembayaranHutangPajak
                VisibilitasBiayaAdministrasiBank(False)
                If Not (ProsesResetForm Or ProsesIsiValueForm Or ProsesLoadingForm) Then
                    PesanPemberitahuan("Untuk Pengajuan Pembayaran Hutang Pajak, silakan melalui Buku Pengawasan terkait.")
                    cmb_Peruntukan.SelectedValue = Kosongan
                    Return
                End If
                PembayaranDenganInvoice = False
                If ProsesLoadingForm = True Then
                    If JenisPajakPerBaris = JenisPajak_BeaMasukImpor _
                        Or JenisPajakPerBaris = JenisPajak_PPhPasal22_Impor _
                        Or JenisPajakPerBaris = JenisPajak_PPN_Impor _
                        Then
                        JenisPajak = JenisPajak_PajakPajakImpor
                    Else
                        JenisPajak = JenisPajakPerBaris
                    End If
                    KodeSetoran = KodeSetoranPerBaris
                End If
                lbl_KodePajak.Visibility = Visibility.Visible
                txt_KodePajak.Visibility = Visibility.Visible
                If FungsiForm = FungsiForm_POSTING Then
                    lbl_KodePajak.Text = "Kode NTPN"
                Else
                    lbl_KodePajak.Text = "Kode Billing"
                End If
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then lbl_KodePajak.Text = "Kode NTPN"
            Case Peruntukan_PembayaranHutangDividen
                Nomor_Invoice_Per_Baris.Header = "Nomor Akta Notaris"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Akta Notaris"
            'PENGELUARAN TUNAI : -----------------------------------
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
            'INVESTASI : -----------------------------------
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

        If PembayaranTerjadwal = True Then
            lbl_Denda.Visibility = Visibility.Visible
            txt_Denda.Visibility = Visibility.Visible
        End If

        If PembayaranDenganInvoice = False Then
            If Not (ProsesResetForm Or ProsesLoadingForm Or ProsesIsiValueForm) Then cmb_Peruntukan.SelectedValue = Kosongan
        End If

    End Sub


    Private Sub txt_NomorKK_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorKK.TextChanged
        NomorKK = txt_NomorKK.Text
    End Sub


    Private Sub dtp_TanggalKK_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalKK.SelectedDateChanged
        If dtp_TanggalKK.Text <> Kosongan Then
            LogikaUmumInputanTanggal(dtp_TanggalKK)
            TanggalKK = TanggalFormatTampilan(dtp_TanggalKK.SelectedDate)
            If ProsesLoadingForm = False Then SistemPenomoranOtomatis_KK()
            If KodeMataUang <> Kosongan Then
                If KodeMataUang = KodeMataUang_IDR Then
                    txt_Kurs.Text = 1
                Else
                    txt_Kurs.Text = AmbilValue_KursTengahBI(KodeMataUang, TanggalKK)
                End If
            End If
        End If
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        SistemPenomoranOtomatis_KK() 'Ini Jangan dihapus. Sangat dibutuhkan..!
        If KodeLawanTransaksi = Kosongan Then
            KosongkanDaftarTagihan()
            txt_NamaLawanTransaksi.Text = Kosongan
            cmb_SaranaPembayaran.SelectedValue = Kosongan
        Else
            If MitraSebagaiPerusahaanLuarNegeri(KodeLawanTransaksi) = True Then
                MitraLokal = False
                MitraLuarNegeri = True
                LokasiLawanTransaksi = LokasiWP_LuarNegeri
            Else
                MitraLokal = True
                MitraLuarNegeri = False
                LokasiLawanTransaksi = LokasiWP_DalamNegeri
            End If
            LogikaLokasiLawanTransaksi()
            Select Case Peruntukan
                'Pembayaran Hutang :
                Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangUsaha()
                Case Peruntukan_PembayaranHutangUsaha_Afiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangUsahaAfiliasi()
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
                Case Peruntukan_PembayaranHutangDividen
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanHutangDividen()
                Case Peruntukan_PembayaranHutangLancarLainnya
                    PesanUntukProgrammer("Belum ada coding untuk pilihan ini..!!!")
                'Pengeluaran Tunai :
                Case Peruntukan_PiutangPemegangSaham
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPengeluaranPiutangPemegangSaham()
                Case Peruntukan_PiutangKaryawan
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaKaryawan(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPengeluaranPiutangKaryawan()
                Case Peruntukan_PiutangPihakKetiga
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPengeluaranPiutangPihakKetiga()
                Case Peruntukan_PiutangAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPengeluaranPiutangAfiliasi()
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
        VisibilitasTotalPengajuan(True)
        VisibilitasTotalDisetujui(True)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        Select Case Peruntukan
            'Pembayaran Hutang :
            Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Semua)
            Case Peruntukan_PembayaranHutangUsaha_Afiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua)
            Case Peruntukan_PembayaranHutangPemegangSaham
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            Case Peruntukan_PembayaranHutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_PembayaranHutangBank
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya)
            Case Peruntukan_PembayaranHutangLeasing
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya)
            Case Peruntukan_PembayaranHutangPihakKetiga
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Tidak)
            Case Peruntukan_PembayaranHutangAfiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Tidak)
            Case Peruntukan_PembayaranHutangDividen
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            'Pengeluaran Tunai :
            Case Peruntukan_PiutangPemegangSaham
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            Case Peruntukan_PiutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NIK_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_PiutangPihakKetiga
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Tidak)
            Case Peruntukan_PiutangAfiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Tidak)
            Case Peruntukan_DepositOperasional
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
            Case Peruntukan_BankGaransi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
        End Select
        'Investasi :
        If Kategori = Kategori_Investasi Then
            BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
        End If
    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub cmb_SaranaPembayaran_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPembayaran.SelectionChanged
        SaranaPembayaran = cmb_SaranaPembayaran.SelectedValue
        COAKredit = KonversiSaranaPembayaranKeCOA(SaranaPembayaran)
        LogikaPemilihanSaranaPembayaran()
        LogikaVisibilitasBank()
        If Not ProsesLoadingForm Then SistemPenomoranOtomatis_KK()
    End Sub
    Sub LogikaVisibilitasBank()
        If COATermasukBank(COAKredit) Then
            PembayaranViaBank = True
            If FungsiForm = FungsiForm_POSTING _
                Or (FungsiForm = FungsiForm_LIHAT And StatusPengajuan = Status_Dibayar) _
                Or TahunBukuNormalTanpaPengajuan _
                Then
                grb_Bank.Visibility = Visibility.Visible
            End If
            KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
            If Not (ProsesResetForm Or ProsesLoadingForm Or ProsesIsiValueForm) Then Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
    End Sub


    Private Sub btn_Singkirkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Singkirkan.Click
        SingkirkanData()
    End Sub
    Sub SingkirkanData()
        Pilihan = MessageBox.Show("Yakin akan menyingkirkan item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        NomorUrutInvoice = 0
        If PembayaranTerjadwal Then
            Do While 1 = 1
                Try
                    datatabelUtama.Rows.RemoveAt(BarisTerseleksi)
                Catch ex As Exception
                    Exit Do
                End Try
            Loop
        Else
            datatabelUtama.Rows.RemoveAt(BarisTerseleksi)
            Try
                datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
                datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
            Catch ex As Exception
            End Try
        End If
        For Each row As DataRow In datatabelUtama.Rows
            NomorUrutInvoice += 1
            row("Nomor_Urut_Invoice") = NomorUrutInvoice
        Next
        Perhitungan_Tabel()
        BersihkanSeleksi()
    End Sub

    Private Sub btn_Tolak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tolak.Click
        Pilihan = MessageBox.Show("Yakin akan menolak item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        If PembayaranTerjadwal Then
            Dim i = BarisTerseleksi
            Do While i <= JumlahInvoice
                datatabelUtama.Rows(i)("Jumlah_Disetujui_Per_Baris") = 0
                i += 1
            Loop
        Else
            rowviewUtama("Jumlah_Disetujui_Per_Baris") = 0
        End If
        Perhitungan_Tabel()
    End Sub


    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Visibility = Visibility.Collapsed
        txt_RekeningPenerima.Text = Kosongan
        txt_AtasNamaPenerima.Text = Kosongan
        txt_BiayaAdministrasiBank.Text = Kosongan
        cmb_DitanggungOleh.SelectedValue = Kosongan
        cmb_DitanggungOleh.IsEnabled = False
        If Not (COATermasukBank(COAKredit)) Then
            txt_JumlahTransfer.Text = Kosongan
            txt_TotalBank.Text = Kosongan
        End If
    End Sub

    Private Sub txt_BiayaAdministrasiBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
        BiayaAdministrasiBank = Ambilangka_MultiCurrency(KodeMataUang, txt_BiayaAdministrasiBank)
        If BiayaAdministrasiBank = 0 Then
            cmb_DitanggungOleh.IsEnabled = False
            cmb_DitanggungOleh.SelectedValue = Kosongan
        Else
            cmb_DitanggungOleh.IsEnabled = True
            If Kategori = Kategori_Pemindahbukuan Then
                cmb_DitanggungOleh.SelectedValue = DitanggungOleh_Perusahaan
                cmb_DitanggungOleh.IsEnabled = False
            End If
        End If
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub cmb_DitanggungOleh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DitanggungOleh.SelectionChanged
        DitanggungOleh = cmb_DitanggungOleh.SelectedValue
        If ProsesLoadingForm = False And ProsesIsiValueForm = False Then Perhitungan_ValueBank()
    End Sub

    Private Sub txt_JumlahTransfer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransfer.TextChanged
        JumlahTransfer = Ambilangka_MultiCurrency(KodeMataUang, txt_JumlahTransfer)
    End Sub


    Private Sub txt_TotalBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalBank.TextChanged
        TotalBank = Ambilangka_MultiCurrency(KodeMataUang, txt_TotalBank)
    End Sub


    Sub Perhitungan_ValueBank()
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_OUT, JumlahBayar_Total, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
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
        If JumlahInvoice > 1 And BarisTerseleksi >= JumlahBaris - 2 Then
            BersihkanSeleksi()
            Return
        End If
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorUrutInvoice_Terseleksi = AmbilAngka(rowviewUtama("Nomor_Urut_Invoice").ToString)
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice_Per_Baris")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice_Per_Baris")
        UraianInvoice_Terseleksi = rowviewUtama("Uraian_Invoice_Per_Baris")
        NomorBP_Terseleksi = rowviewUtama("Nomor_BP_Per_Baris")
        If MataUang_IDR Then
            JumlahTagihanPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Tagihan_Per_Baris").ToString)
            SudahDibayarPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Sudah_Dibayar_Per_Baris").ToString)
            SisaTagihanPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Tagihan_Per_Baris").ToString)
            JumlahPengajuanPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Pengajuan_Per_Baris").ToString)
            JumlahDisetujuiPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Disetujui_Per_Baris").ToString)
        Else
            JumlahTagihanPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Tagihan_Per_Baris").ToString)
            SudahDibayarPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sudah_Dibayar_Per_Baris").ToString)
            SisaTagihanPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sisa_Tagihan_Per_Baris").ToString)
            JumlahPengajuanPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Pengajuan_Per_Baris").ToString)
            JumlahDisetujuiPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Disetujui_Per_Baris").ToString)
        End If

        If JumlahInvoice > 1 Then
            If NomorUrutInvoice_Terseleksi > 0 Then
                btn_Singkirkan.IsEnabled = True
                btn_Tolak.IsEnabled = True
            Else
                btn_Singkirkan.IsEnabled = False
                btn_Tolak.IsEnabled = False
            End If
        End If

        If Not IsNothing(KolomTerseleksi) Then

            'If NomorUrutInvoice_Terseleksi > 0 _
            'And (KolomTerseleksi.DisplayIndex = Jumlah_Pengajuan_Per_Baris.DisplayIndex _
            'Or KolomTerseleksi.DisplayIndex = Jumlah_Disetujui_Per_Baris.DisplayIndex) _
            If NomorUrutInvoice_Terseleksi > 0 _
                Then

                If (Kategori = Kategori_PengeluaranTunai And Peruntukan <> Peruntukan_DepositOperasional) Then Return
                'If Kategori = Kategori_Pemindahbukuan Then Return
                If Kategori = Kategori_Investasi Then Return
                If PembayaranTerjadwal Then Return
                If JenisPajak = JenisPajak_PajakPajakImpor Then Return
                If FungsiForm = FungsiForm_POSTING Then Return
                If FungsiForm = FungsiForm_LIHAT Then Return
                If dtp_TanggalKK.Text = Kosongan Then
                    PesanPeringatan("Silakan isi kolom 'Tanggal Bank Cash In' terlebih dahulu.")
                    dtp_TanggalKK.Focus()
                    Return
                End If

                win_InputJumlahBankCashInOut = New wpfWin_InputJumlahBankCashInOut
                win_InputJumlahBankCashInOut.ResetForm()
                win_InputJumlahBankCashInOut.FungsiForm = FungsiForm
                win_InputJumlahBankCashInOut.KodeMataUang = KodeMataUang
                win_InputJumlahBankCashInOut.LogikaKodeMataUang()
                win_InputJumlahBankCashInOut.PeruntukanPembayaran = Peruntukan
                win_InputJumlahBankCashInOut.NomorInvoice = NomorInvoice_Terseleksi
                win_InputJumlahBankCashInOut.NomorBP = NomorBP_Terseleksi
                win_InputJumlahBankCashInOut.txt_JumlahTagihan.Text = JumlahTagihanPerBaris_Terseleksi
                win_InputJumlahBankCashInOut.txt_SudahDibayar.Text = SudahDibayarPerBaris_Terseleksi
                win_InputJumlahBankCashInOut.txt_Kurs.Text = Kurs
                If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_EDIT Then
                    If DenganPengajuan Then
                        win_InputJumlahBankCashInOut.JudulForm = "Jumlah Pengajuan"
                    Else
                        win_InputJumlahBankCashInOut.JudulForm = "Jumlah Bayar"
                    End If
                    win_InputJumlahBankCashInOut.txt_Jumlah.Text = JumlahPengajuanPerBaris_Terseleksi
                End If
                If FungsiForm = FungsiForm_UPDATEPERSETUJUAN Then
                    win_InputJumlahBankCashInOut.JudulForm = "Jumlah Disetujui"
                    win_InputJumlahBankCashInOut.txt_Jumlah.Text = JumlahDisetujuiPerBaris_Terseleksi
                End If
                win_InputJumlahBankCashInOut.PPhTerutang_ValueAwal_dB = rowviewUtama("PPh_Terutang_Per_Baris_Value_Awal_dB")
                win_InputJumlahBankCashInOut.PPhDitanggung_ValueAwal_dB = rowviewUtama("PPh_Ditanggung_Per_Baris_Value_Awal_dB")
                win_InputJumlahBankCashInOut.PPhDipotong_ValueAwal_dB = rowviewUtama("PPh_Dipotong_Per_Baris_Value_Awal_dB")
                win_InputJumlahBankCashInOut.ShowDialog()
                If win_InputJumlahBankCashInOut.Proses = True Then
                    If FungsiForm = FungsiForm_TAMBAH Or FungsiForm = FungsiForm_EDIT Then
                        If win_InputJumlahBankCashInOut.JumlahInputan > 0 Then
                            rowviewUtama("Jumlah_Pengajuan_Per_Baris") = win_InputJumlahBankCashInOut.JumlahInputan
                            Perhitungan_Tabel()
                        Else
                            SingkirkanData()
                        End If
                    End If
                    If FungsiForm = FungsiForm_UPDATEPERSETUJUAN Then
                        rowviewUtama("Jumlah_Disetujui_Per_Baris") = win_InputJumlahBankCashInOut.JumlahInputan
                        Perhitungan_Tabel()
                    End If
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        'rowviewUtama("Jenis_Pajak_Per_Baris") = win_InputJumlahBankCashInOut .JenisPajak         'Ini malah bikin masalah, dan sepertinya juga tidak perlu.
                        'rowviewUtama("Kode_Setoran_Per_Baris") = win_InputJumlahBankCashInOut .KodeSetoran       'Suatu saat nanti, hapus saja jika memang penonaktifan ini tidak menimbulkan masalah.
                        rowviewUtama("PPh_Terutang_Per_Baris") = win_InputJumlahBankCashInOut.PPhTerutang_UntukIsiValue
                        rowviewUtama("PPh_Ditanggung_Per_Baris") = win_InputJumlahBankCashInOut.PPhDitanggung_UntukIsiValue
                        rowviewUtama("PPh_Dipotong_Per_Baris") = win_InputJumlahBankCashInOut.PPhDipotong_UntukIsiValue
                    End If
                End If

            End If 'Setelah baris ini, jangan ada koding lagi di dalam sub ini. ======================================================

        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Untuk Saat ini, belum ada Coding di sini...!
    End Sub


    Sub KosongkanDaftarTagihan()
        datatabelUtama.Rows.Clear()
    End Sub


    Private Sub txt_Kurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
        Perhitungan_Total()
    End Sub


    Private Sub txt_Denda_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Denda.TextChanged
        If MitraLokal Then
            Denda = AmbilAngka(txt_Denda.Text)
        Else
            Denda = AmbilAngka_Asing(txt_Denda.Text)
        End If
        Perhitungan_Total()
    End Sub


    Private Sub txt_KodePajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodePajak.TextChanged
        KodePajak = txt_KodePajak.Text
    End Sub


    Private Sub txt_TotalPengajuan_IDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalPengajuan_IDR.TextChanged
        If MitraLokal Then
            FormatUlangAngkaKeBilanganBulat(JumlahPengajuan_Total)
            JumlahPengajuan_Total = AmbilAngka(txt_TotalPengajuan_IDR.Text)
        End If
    End Sub


    Private Sub txt_TotalPengajuan_MUA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalPengajuan_MUA.TextChanged
        If Not MitraLokal Then
            FormatUlangAngkaKeBilanganDesimal(JumlahPengajuan_Total)
            JumlahPengajuan_Total = AmbilAngka_Asing(txt_TotalPengajuan_MUA.Text)
        End If
    End Sub


    Private Sub txt_TotalDisetujui_IDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalDisetujui_IDR.TextChanged
        If MitraLokal Then
            FormatUlangAngkaKeBilanganBulat(JumlahBayar_Total)
            JumlahBayar_Total = AmbilAngka(txt_TotalDisetujui_IDR.Text)
        End If
    End Sub

    Private Sub txt_TotalDisetujui_MUA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalDisetujui_MUA.TextChanged
        If Not MitraLokal Then
            FormatUlangAngkaKeBilanganDesimal(JumlahBayar_Total)
            JumlahBayar_Total = AmbilAngka_Asing(txt_TotalDisetujui_MUA.Text)
        End If
    End Sub

    Private Sub dtp_TanggalBayar_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBayar.SelectedDateChanged
        If dtp_TanggalBayar.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBayar)
            TanggalBayar = TanggalFormatTampilan(dtp_TanggalBayar.SelectedDate)
        End If
    End Sub

    Private Sub txt_Catatan_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub


    Private Sub txt_RekeningPenerima_TextChanged(sender As Object, e As RoutedEventArgs) Handles txt_RekeningPenerima.TextChanged
        RekeningPenerima = txt_RekeningPenerima.Text
    End Sub

    Private Sub txt_AtasNamaPenerima_TextChanged(sender As Object, e As RoutedEventArgs) Handles txt_AtasNamaPenerima.TextChanged
        AtasNamaPenerima = txt_AtasNamaPenerima.Text
    End Sub


    Private Sub btn_Cetak_Click_1(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Pilihan = MessageBox.Show("Saat proses pratinjau/pencetakan, sistem akan menyimpan data secara otomatis." & Enter2Baris &
                                  "Lanjutkan..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        SimpanData()
        ProsesCetak()
    End Sub
    Sub ProsesCetak()
        Cetak(JenisFormCetak_PengajuanPengeluaran, NomorKK, False, False)
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                              " Status_Invoice      = '" & Status_Dicetak & "', " &
                              " Status_Pengajuan    = '" & Status_Dicetak & "' " &
                              " WHERE Nomor_KK    = '" & NomorKK & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        If StatusSuntingDatabase = True Then
            usc_BukuPengawasanBuktiPengeluaranBankCash.TampilkanData()
            Me.Close()
        End If
        PesanPemberitahuan("Ceritanya data sudah dicetak ya....!")
    End Sub
    Sub LogikaTombol_Cetak()
        If Peruntukan = Kosongan _
            Or KodeLawanTransaksi = Kosongan _
            Or SaranaPembayaran = Kosongan _
            Or datatabelUtama.Rows.Count = 0 _
            Then
            btn_Cetak.Visibility = Visibility.Collapsed
        Else
            If FungsiForm = FungsiForm_TAMBAH _
                Or FungsiForm = FungsiForm_CETAK _
                Or FungsiForm = FungsiForm_LIHAT _
                Then
                btn_Cetak.Visibility = Visibility.Visible
            End If
        End If
    End Sub


    Private Sub btn_Simpan_Click_1(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click
        SimpanData()
        If AdaPenyimpanan Then Close()
    End Sub
    Private Sub SimpanData()
        AdaPenyimpanan = False
        'Pengisian Ulang Value :
        NomorInvoice_Bundel = Kosongan
        TanggalInvoice_Bundel = Kosongan
        NamaProduk_Bundel = Kosongan
        StatusSuntingDatabase = False '(Ini untuk kebutuhan logika tutup/tidak form. Jangan dihapus..!)

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

        If dtp_TanggalKK.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Bank Cash Out'.")
            dtp_TanggalKK.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If Kurs = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_Kurs, "Kurs")
            Return
        End If

        If SaranaPembayaran = Kosongan Then
            PesanPeringatan("Silakan pilih 'Sarana Pembayaran'.")
            cmb_SaranaPembayaran.Focus()
            Return
        End If

        If PembayaranViaBank Then

            If FungsiForm = FungsiForm_POSTING And Kategori <> Kategori_Pemindahbukuan Then
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

        If datatabelUtama.Rows.Count = 0 Then
            PesanPeringatan("Silakan isi 'Tabel Pembayaran'.")
            Return
        End If

        If Catatan = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeksKaya(txt_Catatan, "Catatan")
            Return
        End If

        If FungsiForm = FungsiForm_POSTING Then

            If dtp_TanggalBayar.Text = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Tanggal Bayar'.")
                dtp_TanggalBayar.Focus()
                Return
            End If

        End If

        If Peruntukan = Peruntukan_PembayaranHutangPajak And KodePajak = Kosongan Then

            If FungsiForm = FungsiForm_POSTING Then
                PesanPeringatan("Silakan isi kolom 'Kode NTPN'.")
            Else
                PesanPeringatan("Silakan isi kolom 'Kode Billing'.")
            End If

            txt_KodePajak.Focus()
            Return

        End If

        If Peruntukan = Peruntukan_PembayaranHutangUsaha_NonAfiliasi Then
            Select Case KodeMataUang
                Case KodeMataUang_IDR
                    COADebet = KodeTautanCOA_HutangUsaha_NonAfiliasi
                Case KodeMataUang_USD
                    COADebet = KodeTautanCOA_HutangUsaha_USD
                Case KodeMataUang_AUD
                    COADebet = KodeTautanCOA_HutangUsaha_AUD
                Case KodeMataUang_JPY
                    COADebet = KodeTautanCOA_HutangUsaha_JPY
                Case KodeMataUang_CNY
                    COADebet = KodeTautanCOA_HutangUsaha_CNY
                Case KodeMataUang_EUR
                    COADebet = KodeTautanCOA_HutangUsaha_EUR
                Case KodeMataUang_SGD
                    COADebet = KodeTautanCOA_HutangUsaha_SGD
                Case KodeMataUang_GBP
                    COADebet = KodeTautanCOA_HutangUsaha_GBP
            End Select
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If Not KodeMataUang = KodeMataUang_IDR Then
                Dim TanggalTransaksi = TanggalBayar
                If TahunBukuNormalTanpaPengajuan Then TanggalTransaksi = TanggalKK
                JurnalAdjusment_Forex(COAKredit, TanggalTransaksi)
                JurnalAdjusment_Forex(COADebet, TanggalTransaksi)
            End If
        End If

        StatusSuntingDatabase = True

        AksesDatabase_Transaksi(Buka)

        NomorJV = 0
        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                StatusPengajuan = Status_Open
                TanggalBayar = TanggalKosong
            Case FungsiForm_EDIT
                HapusDataPengajuanLama()
                StatusPengajuan = Status_Open
                TanggalBayar = TanggalKosong
            Case FungsiForm_UPDATEPERSETUJUAN
                HapusDataPengajuanLama()
                StatusPengajuan = Status_Disetujui
                TanggalBayar = TanggalKosong
            Case FungsiForm_POSTING
                HapusDataPengajuanLama()
                If StatusPengajuan = Status_Disetujui Then
                    'Jika belum pernah dijurnal :
                    SistemPenomoranOtomatis_NomorJV()
                    NomorJV = jur_NomorJV
                Else
                    'Jika sudah pernah dijurnal, dan hendak mengeditnya :
                    jur_NomorJV = NomorJV_Sebelumnya
                    NomorJV = NomorJV_Sebelumnya
                    HapusJurnal_BerdasarkanNomorJV(NomorJV)
                End If
                StatusPengajuan = Status_Dibayar
        End Select

        If TahunBukuNormalTanpaPengajuan Then
            If FungsiForm = FungsiForm_TAMBAH Then
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
            End If
            If FungsiForm = FungsiForm_EDIT Then
                'Jika sudah pernah dijurnal, dan hendak mengeditnya :
                HapusDataPengajuanLama()
                jur_NomorJV = NomorJV_Sebelumnya
                NomorJV = NomorJV_Sebelumnya
                HapusJurnal_BerdasarkanNomorJV(NomorJV)
            End If
        End If

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BuktiPengeluaran")

        For Each row As DataRow In datatabelUtama.Rows 'Awal Loop =========================================================

            If AmbilAngka(row("Nomor_Urut_Invoice").ToString) = 0 Then Exit For

            NomorID += 1
            NomorBP = row("Nomor_BP_Per_Baris")
            NomorInvoicePerBaris = row("Nomor_Invoice_Per_Baris")
            TanggalInvoicePerBaris = row("Tanggal_Invoice_Per_Baris")
            If TanggalInvoicePerBaris = Kosongan Then TanggalInvoicePerBaris = TanggalKosong
            UraianInvoicePerBaris = row("Uraian_Invoice_Per_Baris")
            AngsuranKe = AmbilAngka(row("Angsuran_Ke").ToString)
            If MataUang_IDR Then
                PokokPerBaris = AmbilAngka(row("Pokok_Per_Baris").ToString)
                BagiHasilPerBaris = AmbilAngka(row("Bagi_Hasil_Per_Baris").ToString)
                JumlahTagihanPerBaris = AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
                JumlahPengajuanPerBaris = AmbilAngka(row("Jumlah_Pengajuan_Per_Baris").ToString)
                JumlahDisetujuiPerBaris = AmbilAngka(row("Jumlah_Disetujui_Per_Baris").ToString)
            Else
                PokokPerBaris = AmbilAngka_Asing(row("Pokok_Per_Baris").ToString)
                BagiHasilPerBaris = AmbilAngka_Asing(row("Bagi_Hasil_Per_Baris").ToString)
                JumlahTagihanPerBaris = AmbilAngka_Asing(row("Jumlah_Tagihan_Per_Baris").ToString)
                JumlahPengajuanPerBaris = AmbilAngka_Asing(row("Jumlah_Pengajuan_Per_Baris").ToString)
                JumlahDisetujuiPerBaris = AmbilAngka_Asing(row("Jumlah_Disetujui_Per_Baris").ToString)
            End If
            JenisPajakPerBaris = row("Jenis_Pajak_Per_Baris").ToString
            KodeSetoranPerBaris = row("Kode_Setoran_Per_Baris").ToString
            PPhTerutangPerBaris = AmbilAngka(row("PPh_Terutang_Per_Baris").ToString)
            PPhDitanggungPerBaris = AmbilAngka(row("PPh_Ditanggung_Per_Baris").ToString)
            PPhDipotongPerBaris = PPhTerutangPerBaris - PPhDitanggungPerBaris '(Ini jangan dirubah. Supaya hasilnya lebih akurat ketika angkanya tidak bulat)

            'Pajak-pajak Impor :
            If JenisPajakPerBaris = JenisPajak_BeaMasukImpor Then Jumlah_BeaMasuk_Impor = JumlahTagihanPerBaris
            If JenisPajakPerBaris = JenisPajak_PPhPasal22_Impor Then Jumlah_PPhPasal22_Impor = JumlahTagihanPerBaris
            If JenisPajakPerBaris = JenisPajak_PPN_Impor Then Jumlah_PPNMasukan_Impor = JumlahTagihanPerBaris

            If JenisTahunBuku = JenisTahunBuku_LAMPAU Or TahunBukuNormalTanpaPengajuan Then
                StatusPengajuan = Status_Dibayar
                JumlahDisetujuiPerBaris = JumlahPengajuanPerBaris
                TanggalBayar = TanggalKK
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    NomorJV = 0
                    jur_NomorJV = 0
                End If
            End If

            If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                NomorInvoicePerBaris = KodePajak
            End If

            StatusInvoice = StatusPengajuan
            If JumlahDisetujuiPerBaris = 0 _
                And (FungsiForm = FungsiForm_POSTING Or FungsiForm = FungsiForm_UPDATEPERSETUJUAN) _
                Then
                StatusInvoice = Status_Ditolak
            End If

            'PesanUntukProgrammer("Fungsi Form : " & FungsiForm & Enter2Baris &
            '                     "Jenis Tahun Buku : " & JenisTahunBuku & Enter2Baris &
            '                     "Dengan Pengajuan : " & DenganPengajuan & Enter2Baris &
            '                     "Tahun Normal Tanpa Pengajuan : " & TahunBukuNormalTanpaPengajuan & Enter2Baris &
            '                     "Status Invoice : " & StatusInvoice & Enter2Baris &
            '                     "Status Pengajuan : " & StatusPengajuan & Enter2Baris &
            '                     "Nomor JV : " & NomorJV & Enter2Baris &
            '                     "Jur_Nomor JV : " & jur_NomorJV)

            'PesanUntukProgrammer("Tanggal KK : " & TanggalKK & Enter2Baris &
            '                     "Tanggal Invoice : " & TanggalInvoicePerBaris & Enter2Baris &
            '                     "Tanggal Bayar : " & TanggalBayar)

            PesanUntukProgrammer("Jumlah Tagihan : " & JumlahTagihanPerBaris & Enter2Baris &
                                 "Sudah Dibayar : " & SudahDibayarPerBaris & Enter2Baris &
                                 "Sisa Tagihan : " & SisaTagihanPerBaris & Enter2Baris &
                                 "Jumlah Pengajuan : " & JumlahPengajuanPerBaris & Enter2Baris &
                                 "Jumlah Disetujui : " & JumlahDisetujuiPerBaris)

            Dim JumlahTagihanPerBaris_Simpan As String
            Dim PokokPerBaris_Simpan As String
            Dim BagiHasilPerBaris_Simpan As String
            Dim JumlahPengajuanPerBaris_Simpan As String
            Dim JumlahDisetujuiPerBaris_Simpan As String
            Dim BiayaAdministrasiBank_Simpan As String
            If MataUang_IDR Then
                JumlahTagihanPerBaris_Simpan = JumlahTagihanPerBaris
                PokokPerBaris_Simpan = PokokPerBaris
                BagiHasilPerBaris_Simpan = BagiHasilPerBaris
                JumlahPengajuanPerBaris_Simpan = JumlahPengajuanPerBaris
                JumlahDisetujuiPerBaris_Simpan = JumlahDisetujuiPerBaris
                BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
            Else
                JumlahTagihanPerBaris_Simpan = DesimalFormatSimpan(JumlahTagihanPerBaris)
                PokokPerBaris_Simpan = DesimalFormatSimpan(PokokPerBaris)
                BagiHasilPerBaris_Simpan = DesimalFormatSimpan(BagiHasilPerBaris)
                JumlahPengajuanPerBaris_Simpan = DesimalFormatSimpan(JumlahPengajuanPerBaris)
                JumlahDisetujuiPerBaris_Simpan = DesimalFormatSimpan(JumlahDisetujuiPerBaris)
                BiayaAdministrasiBank_Simpan = DesimalFormatSimpan(BiayaAdministrasiBank)
            End If

            cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPengeluaran VALUES ( " &
                                        " '" & NomorID & "', " &
                                        " '" & AngkaKK & "', " &
                                        " '" & NomorKK & "', " &
                                        " '" & TanggalFormatSimpan(TanggalKK) & "', " &
                                        " '" & NomorBundel & "', " &
                                        " '" & Kategori & "', " &
                                        " '" & Peruntukan & "', " &
                                        " '" & KodeLawanTransaksi & "', " &
                                        " '" & NamaLawanTransaksi & "', " &
                                        " '" & NomorBP & "', " &
                                        " '" & NomorInvoicePerBaris & "', " &
                                        " '" & TanggalFormatSimpan(TanggalInvoicePerBaris) & "', " &
                                        " '" & UraianInvoicePerBaris & "', " &
                                        " '" & KodeMataUang & "', " &
                                        " '" & DesimalFormatSimpan(Kurs) & "', " &
                                        " '" & JumlahTagihanPerBaris_Simpan & "', " &
                                        " '" & AngsuranKe & "', " &
                                        " '" & PokokPerBaris_Simpan & "', " &
                                        " '" & BagiHasilPerBaris_Simpan & "', " &
                                        " '" & JumlahPengajuanPerBaris_Simpan & "', " &
                                        " '" & JumlahDisetujuiPerBaris_Simpan & "', " &
                                        " '" & 0 & "', " &
                                        " '" & COADebet & "', " &
                                        " '" & COAKredit & "', " &
                                        " '" & BiayaAdministrasiBank_Simpan & "', " &
                                        " '" & DitanggungOleh & "', " &
                                        " '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                                        " '" & RekeningPenerima & "', " &
                                        " '" & AtasNamaPenerima & "', " &
                                        " '" & StatusInvoice & "', " &
                                        " '" & StatusPengajuan & "', " &
                                        " '" & JenisPajakPerBaris & "', " &
                                        " '" & KodeSetoranPerBaris & "', " &
                                        " '" & PPhTerutangPerBaris & "', " &
                                        " '" & PPhDitanggungPerBaris & "', " &
                                        " '" & PPhDipotongPerBaris & "', " &
                                        " '" & Catatan & "', " &
                                        " '" & NomorJV & "', " &
                                        " '" & Kosongan & "', " &
                                        " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                        " '" & Kosongan & "', " &
                                        " '" & 0 & "', " &
                                        " '" & UserAktif & "' ) ",
                                        KoneksiDatabaseTransaksi)
            cmdSIMPAN_ExecuteNonQuery()

            'Pembundelan Nomor dan Tanggal Invoice :
            If PembayaranTerjadwal Or Peruntukan = Peruntukan_PembayaranHutangPajak Then
                NomorInvoice_Bundel = NomorInvoicePerBaris
                TanggalInvoice_Bundel = TanggalInvoicePerBaris
            Else
                If NomorInvoice_Bundel = Kosongan Then
                    NomorInvoice_Bundel = NomorInvoicePerBaris
                    TanggalInvoice_Bundel = TanggalInvoicePerBaris
                    NamaProduk_Bundel = AmbilValue_ListProdukBerdasarkanInvoicePembelian(NomorInvoicePerBaris)
                Else
                    NomorInvoice_Bundel &= SlashGanda_Pemisah & NomorInvoicePerBaris
                    TanggalInvoice_Bundel &= SlashGanda_Pemisah & TanggalInvoicePerBaris
                    NamaProduk_Bundel &= SlashGanda_Pemisah & AmbilValue_ListProdukBerdasarkanInvoicePembelian(NomorInvoicePerBaris)
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
                Case Else
                    'Ini jangan dihapus..!
                    'Ini berfungsi untuk mereset kembali variabel yang tidak termasuk Jenis Pajak dan Kode Setoran yang sudah tercantum.
                    JenisPajakPerBaris = Kosongan
                    KodeSetoranPerBaris = Kosongan
            End Select
            PPhDipotong_Total += PPhDipotongPerBaris

            If StatusPengajuan = Status_Dibayar Then

                If PembayaranTerjadwal Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    If TabelJadwalAngsuran = Kosongan Then PesanUntukProgrammer("Tabel Jadwal Angsuran belum ditentukan...!!!")
                    If KolomBPJadwalAngsuran = Kosongan Then PesanUntukProgrammer("Kolom BP Jadwal Angsuran belum ditentukan...!!!")
                    cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                          " Tanggal_Bayar                           = '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                                          " Denda                                   = '" & 0 & "', " &
                                          " COA_Kredit                              = '" & COAKredit & "', " &
                                          " Biaya_Administrasi_Bank                 = '" & BiayaAdministrasiBank & "', " &
                                          " Ditanggung_Oleh                         = '" & DitanggungOleh & "', " &
                                          " Keterangan                              = '" & Catatan & "', " &
                                          " Nomor_JV                                = '" & NomorJV & "', " &
                                          " User                                    = '" & UserAktif & "' " &
                                          " WHERE " & KolomBPJadwalAngsuran & "     = '" & NomorBP & "' " &
                                          " AND Angsuran_Ke                         = '" & AngsuranKe & "' ",
                                          KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                End If

                If Kategori = Kategori_PengeluaranTunai Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    Dim Pembebanan As String = Kosongan
                    If DitanggungOleh = DitanggungOleh_LawanTransaksi Then Pembebanan = Pembebanan_Dipotong
                    Select Case Peruntukan
                        Case Peruntukan_PiutangPemegangSaham
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangPemegangSaham SET " &
                                                  " COA_Kredit              = '" & COAKredit & "', " &
                                                  " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                                  " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                                  " Pembebanan              = '" & Pembebanan & "', " &
                                                  " Nomor_JV                = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPPS       = '" & NomorBP & "' ",
                                                  KoneksiDatabaseTransaksi)
                            cmd_ExecuteNonQuery()
                        Case Peruntukan_PiutangKaryawan
                            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPiutangKaryawan SET " &
                                                  " COA_Kredit              = '" & COAKredit & "', " &
                                                  " Biaya_Administrasi_Bank = '" & BiayaAdministrasiBank & "', " &
                                                  " Ditanggung_Oleh         = '" & DitanggungOleh & "', " &
                                                  " Pembebanan              = '" & Pembebanan & "', " &
                                                  " Nomor_JV                = '" & NomorJV & "' " &
                                                  " WHERE Nomor_BPPK        = '" & NomorBP & "' ",
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
                            'Belum ada kebutuhan coding di sini.
                            'Tapi ga apa-apa, disiapkan saja kerangkanya.
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
                    cmd = New OdbcCommand(
                        " UPDATE tbl_Pemindahbukuan SET " &
                        " Tanggal_Transaksi   = '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                        " Nomor_JV            = '" & NomorJV & "' " &
                        " WHERE Nomor_BPPB    = '" & NomorBP & "' ",
                        KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                End If

                If Kategori = Kategori_Investasi Then
                    '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                    cmd = New OdbcCommand(
                        " UPDATE tbl_AktivaLainnya SET " &
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
            If StatusPengajuan = Status_Dibayar Then
                cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                      " Denda                                   = '" & Denda & "' " &
                                      " WHERE " & KolomBPJadwalAngsuran & "     = '" & NomorBP & "' " &
                                      " AND Angsuran_Ke                         = '" & AngsuranKe & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
            End If
        End If

        'Jika Jumlah Disetujui = 0 (dalam hal ini, meminjam variabel Jumlah Bayar), maka sama saja pengajuan tersebut ditolak,
        'karena tidak ada 1 tagihan pun yang dikabulkan.
        If FungsiForm = FungsiForm_UPDATEPERSETUJUAN And JumlahBayar_Tabel = 0 Then
            cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                  " Status_Pengajuan    = '" & Status_Ditolak & "' " &
                                  " WHERE Nomor_KK      = '" & NomorKK & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        If FungsiForm = FungsiForm_POSTING Then
            'Ubah status Bundel menjadi Closed, meskipun belum semua pengajuan (yang sudah disetujui) diposing ke Jurnal.
            cmd = New OdbcCommand(" UPDATE tbl_BundelPengajuanPengeluaran SET " &
                                  " Status              = '" & Status_Closed & "' " &
                                  " WHERE Nomor_Bundel  = '" & NomorBundel & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        'PesanUntukProgrammer("Dengan Pengajuan : " & DenganPengajuan & Enter2Baris &
        '                     "Tahun Normal Tanpa Pengajuan : " & TahunBukuNormalTanpaPengajuan & Enter2Baris &
        '                     "Nomor JV : " & NomorJV & Enter2Baris &
        '                     "jurNomorJV : " & jur_NomorJV)

        'Penyimpanan Jurnal :
        Dim AdaPenyimpananJurnal As Boolean = False

        If FungsiForm = FungsiForm_POSTING Or TahunBukuNormalTanpaPengajuan Then AdaPenyimpananJurnal = True

        If AdaPenyimpananJurnal Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalBayar)
            jur_KodeMataUang = KodeMataUang
            jur_Kurs = Kurs
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
            If Kategori = Kategori_Pemindahbukuan Then jur_JenisJurnal = JenisJurnal_Pemindahbukuan
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoice_Bundel
            jur_NomorInvoice = NomorInvoice_Bundel
            jur_NamaProduk = NamaProduk_Bundel
            jur_KodeLawanTransaksi = KodeLawanTransaksi
            jur_NamaLawanTransaksi = NamaLawanTransaksi
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            If JenisPajak = JenisPajak_PajakPajakImpor Then
                jur_JenisJurnal = JenisJurnal_PajakImpor
                jur_Referensi = NomorBP
                jur_NomorFakturPajak = AmbilValue_NomorFakturPajakBerdasarkanNomorInvoicePembelian(NomorBP)
                jur_TanggalInvoice = TanggalFormatTampilan(AmbilValue_TanggalInvoiceBerdasarkanNomorInvoicePembelian(NomorBP))
            End If

            Dim JumlahDebet, JumlahKredit As Decimal
            If PembayaranTerjadwal Then
                JumlahDebet = Pokok
                JumlahKredit = Pokok + BagiHasil + Denda - PPhDipotong_Total
            Else
                JumlahDebet = JumlahBayar_Tabel + PPhDipotong_Total
                JumlahKredit = JumlahBayar_Tabel + BiayaProvisi
            End If

            'PesanUntukProgrammer("Jumlah Debet : " & JumlahDebet & Enter2Baris &
            '                     "Jumlah Kredit : " & JumlahKredit)

            If Not JenisPajak = JenisPajak_PajakPajakImpor Then
                Jumlah_BeaMasuk_Impor = 0
                Jumlah_PPhPasal22_Impor = 0
                Jumlah_PPNMasukan_Impor = 0
            End If

            'Simpan Jurnal :
            ___jurDebet(COADebet, JumlahDebet)
            ___jurDebet(KodeTautanCOA_BiayaDendaBank, Denda)
            ___jurDebet(KodeTautanCOA_BiayaBungaBank, BagiHasil)
            jur_Kurs = 1
            jur_KodeMataUang = KodeMataUang_IDR
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal21, PPhDitanggung_Pasal21)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal23, PPhDitanggung_Pasal23)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal26, PPhDitanggung_Pasal26)
            ___jurDebet(KodeTautanCOA_BiayaPPhPasal42, PPhDitanggung_Pasal42)
            ___jurDebet(KodeTautanCOA_BeaMasuk_Impor, Jumlah_BeaMasuk_Impor)
            ___jurDebet(KodeTautanCOA_PPhPasal22DibayarDimuka_Impor, Jumlah_PPhPasal22_Impor)
            ___jurDebet(KodeTautanCOA_PPNMasukan_Impor, Jumlah_PPNMasukan_Impor)
            ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaProvisi)
            jur_Kurs = Kurs
            jur_KodeMataUang = KodeMataUang
            ___jurDebet(KodeTautanCOA_BiayaAdministrasiBank, BiayaAdministrasiBank)
            jur_Kurs = 1
            jur_KodeMataUang = KodeMataUang_IDR
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
            jur_Kurs = Kurs
            jur_KodeMataUang = KodeMataUang
            _______jurKreditBankCashOUT(DitanggungOleh, COAKredit, JumlahKredit, JumlahTransfer, BiayaAdministrasiBank)
            KoreksiSelisihJurnal(jur_NomorJV) 'Ini harus disimpan langsung di ujung penyimpanan Jurnal, tidak boleh diseling oleh baris kode yang lain

            UpdateDataPajakImporPadaInvoicePembelian()

        End If

        PesanUntukProgrammer("Logika pesan penyimpanan harus diperbaiki...!!!!!")

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Or FungsiForm = FungsiForm_UPDATEPERSETUJUAN Then pesan_DataTerpilihBerhasilDiperbarui()
            If FungsiForm = FungsiForm_POSTING Then
                If StatusPengajuan = Status_Disetujui Then
                    PesanSukses("Data Pengajuan BERHASIL dikirim ke Jurnal.")
                Else
                    PesanSukses("Data Pengajuan dan Jurnal BERHASIL diperbarui.")
                End If
            End If
            AdaPenyimpanan = True
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                pesan_DataGagalDisimpan()
            Else
                PesanPeringatan("Data GAGAL diperbarui..!")
            End If
            AdaPenyimpanan = False
        End If

    End Sub

    Sub HapusDataPengajuanLama()
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPengeluaran " &
                                   " WHERE Angka_KK = '" & AngkaKK & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
    End Sub

    Sub UpdateDataPajakImporPadaInvoicePembelian()
        If Not JenisPajak = JenisPajak_PajakPajakImpor Then Return
        If Not StatusSuntingDatabase Then Return
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Pembelian_Invoice SET " &
                              " Tanggal_Bayar_Pajak_Impor   = '" & TanggalFormatSimpan(TanggalBayar) & "', " &
                              " Nomor_JV_Bayar_Pajak_Impor  = '" & NomorJV & "' " &
                              " WHERE Nomor_Invoice         = '" & NomorBP & "' ",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        cmb_Kategori.IsReadOnly = True
        cmb_Peruntukan.IsReadOnly = True
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_TotalPengajuan_IDR.IsReadOnly = True
        txt_TotalDisetujui_IDR.IsReadOnly = True
        cmb_SaranaPembayaran.IsReadOnly = True
        cmb_DitanggungOleh.IsReadOnly = True
        txt_JumlahTransfer.IsReadOnly = True
        txt_TotalBank.IsReadOnly = True
    End Sub

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice_Per_Baris As New DataGridTextColumn
    Dim Tanggal_Invoice_Per_Baris As New DataGridTextColumn
    Dim Uraian_Invoice_Per_Baris As New DataGridTextColumn
    Dim Nomor_BP_Per_Baris As New DataGridTextColumn
    Dim Jumlah_Tagihan_Per_Baris As New DataGridTextColumn
    Dim Angsuran_Ke As New DataGridTextColumn
    Dim Pokok_Per_Baris As New DataGridTextColumn
    Dim Bagi_Hasil_Per_Baris As New DataGridTextColumn
    Dim Sudah_Dibayar_Per_Baris As New DataGridTextColumn
    Dim Sisa_Tagihan_Per_Baris As New DataGridTextColumn
    Dim Jenis_Pajak_Per_Baris As New DataGridTextColumn
    Dim Kode_Setoran_Per_Baris As New DataGridTextColumn
    Dim PPh_Terutang_Per_Baris As New DataGridTextColumn
    Dim PPh_Ditanggung_Per_Baris As New DataGridTextColumn
    Dim PPh_Dipotong_Per_Baris As New DataGridTextColumn
    Dim Jumlah_Pengajuan_Per_Baris As New DataGridTextColumn
    Dim Jumlah_Disetujui_Per_Baris As New DataGridTextColumn
    Dim PPh_Terutang_Per_Baris_Value_Awal_dB As New DataGridTextColumn
    Dim PPh_Ditanggung_Per_Baris_Value_Awal_dB As New DataGridTextColumn
    Dim PPh_Dipotong_Per_Baris_Value_Awal_dB As New DataGridTextColumn

    Sub Buat_DataTabelUtama()
        datatabelUtama = New DataTable
    End Sub

    Sub Buat_DataTabelUtama_Lokal()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()
        datatabelUtama.Columns.Add("Nomor_Urut_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Tanggal_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Uraian_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Nomor_BP_Per_Baris")
        datatabelUtama.Columns.Add("Jumlah_Tagihan_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Angsuran_Ke")
        datatabelUtama.Columns.Add("Pokok_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Bagi_Hasil_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Sudah_Dibayar_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Tagihan_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_Pajak_Per_Baris", GetType(String))
        datatabelUtama.Columns.Add("Kode_Setoran_Per_Baris", GetType(String))
        datatabelUtama.Columns.Add("PPh_Terutang_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Pengajuan_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Disetujui_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Terutang_Per_Baris_Value_Awal_dB", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Per_Baris_Value_Awal_dB", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong_Per_Baris_Value_Awal_dB", GetType(Int64))

        datagridUtama.Columns.Clear()
        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut_Invoice, "Nomor_Urut_Invoice", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice_Per_Baris, "Nomor_Invoice_Per_Baris", "Nomor Invoice", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice_Per_Baris, "Tanggal_Invoice_Per_Baris", "Tanggal Invoice", 72, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Invoice_Per_Baris, "Uraian_Invoice_Per_Baris", "Uraian", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BP_Per_Baris, "Nomor_BP_Per_Baris", "Nomor BP", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Tagihan_Per_Baris, "Jumlah_Tagihan_Per_Baris", "Jumlah Tagihan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angsuran_Ke, "Angsuran_Ke", "Angsuran Ke", 72, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pokok_Per_Baris, "Pokok_Per_Baris", "Pokok", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bagi_Hasil_Per_Baris, "Bagi_Hasil_Per_Baris", "Bunga/Bagi Hasil", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sudah_Dibayar_Per_Baris, "Sudah_Dibayar_Per_Baris", "Sudah" & Enter1Baris & "Dibayar", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan_Per_Baris, "Sisa_Tagihan_Per_Baris", "Sisa" & Enter1Baris & "Tagihan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Pajak_Per_Baris, "Jenis_Pajak_Per_Baris", "Jenis Pajak", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Setoran_Per_Baris, "Kode_Setoran_Per_Baris", "Kode Setoran", 51, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris, "PPh_Terutang_Per_Baris", "PPh" & Enter1Baris & "Terutang", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris, "PPh_Ditanggung_Per_Baris", "PPh" & Enter1Baris & "Ditanggung", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris, "PPh_Dipotong_Per_Baris", "PPh" & Enter1Baris & "Dipotong", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pengajuan_Per_Baris, "Jumlah_Pengajuan_Per_Baris", "Jumlah" & Enter1Baris & "Pengajuan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Disetujui_Per_Baris, "Jumlah_Disetujui_Per_Baris", "Jumlah" & Enter1Baris & "Disetujui", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris_Value_Awal_dB, "PPh_Terutang_Per_Baris_Value_Awal_dB", "PPh Terutang (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris_Value_Awal_dB, "PPh_Ditanggung_Per_Baris_Value_Awal_dB", "PPh Ditanggung (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris_Value_Awal_dB, "PPh_Dipotong_Per_Baris_Value_Awal_dB", "PPh Dipotong (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub Buat_DataTabelUtama_Asing()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()
        datatabelUtama.Columns.Add("Nomor_Urut_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Tanggal_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Uraian_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Nomor_BP_Per_Baris")
        datatabelUtama.Columns.Add("Jumlah_Tagihan_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Angsuran_Ke")
        datatabelUtama.Columns.Add("Pokok_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Bagi_Hasil_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Sudah_Dibayar_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Sisa_Tagihan_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Jenis_Pajak_Per_Baris", GetType(String))
        datatabelUtama.Columns.Add("Kode_Setoran_Per_Baris", GetType(String))
        datatabelUtama.Columns.Add("PPh_Terutang_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Pengajuan_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("Jumlah_Disetujui_Per_Baris", GetType(Decimal))
        datatabelUtama.Columns.Add("PPh_Terutang_Per_Baris_Value_Awal_dB", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Per_Baris_Value_Awal_dB", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong_Per_Baris_Value_Awal_dB", GetType(Int64))

        datagridUtama.Columns.Clear()
        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut_Invoice, "Nomor_Urut_Invoice", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice_Per_Baris, "Nomor_Invoice_Per_Baris", "Nomor Invoice", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice_Per_Baris, "Tanggal_Invoice_Per_Baris", "Tanggal Invoice", 72, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Invoice_Per_Baris, "Uraian_Invoice_Per_Baris", "Uraian", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BP_Per_Baris, "Nomor_BP_Per_Baris", "Nomor BP", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Tagihan_Per_Baris, "Jumlah_Tagihan_Per_Baris", "Jumlah Tagihan", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angsuran_Ke, "Angsuran_Ke", "Angsuran Ke", 72, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pokok_Per_Baris, "Pokok_Per_Baris", "Pokok", 87, FormatDesimal, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bagi_Hasil_Per_Baris, "Bagi_Hasil_Per_Baris", "Bunga/Bagi Hasil", 87, FormatDesimal, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sudah_Dibayar_Per_Baris, "Sudah_Dibayar_Per_Baris", "Sudah" & Enter1Baris & "Dibayar", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan_Per_Baris, "Sisa_Tagihan_Per_Baris", "Sisa" & Enter1Baris & "Tagihan", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Pajak_Per_Baris, "Jenis_Pajak_Per_Baris", "Jenis Pajak", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Setoran_Per_Baris, "Kode_Setoran_Per_Baris", "Kode Setoran", 51, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris, "PPh_Terutang_Per_Baris", "PPh" & Enter1Baris & "Terutang", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris, "PPh_Ditanggung_Per_Baris", "PPh" & Enter1Baris & "Ditanggung", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris, "PPh_Dipotong_Per_Baris", "PPh" & Enter1Baris & "Dipotong", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pengajuan_Per_Baris, "Jumlah_Pengajuan_Per_Baris", "Jumlah" & Enter1Baris & "Pengajuan", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Disetujui_Per_Baris, "Jumlah_Disetujui_Per_Baris", "Jumlah" & Enter1Baris & "Disetujui", 87, FormatDesimal, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris_Value_Awal_dB, "PPh_Terutang_Per_Baris_Value_Awal_dB", "PPh Terutang (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris_Value_Awal_dB, "PPh_Ditanggung_Per_Baris_Value_Awal_dB", "PPh Ditanggung (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris_Value_Awal_dB, "PPh_Dipotong_Per_Baris_Value_Awal_dB", "PPh Dipotong (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
