Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfWin_InputBuktiPenerimaan

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Dim QueryTampilan

    Public NomorID
    Dim Kategori
    Dim Peruntukan
    Public AngkaKM
    Dim NomorKM
    Dim TanggalKM
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim COAKredit
    Dim SaranaPencairan
    Dim COADebet
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim Pembebanan
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
    Dim JumlahCairPerBaris
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
    Dim JumlahCairPerBaris_Terseleksi

    Dim JumlahTagihan
    Dim Pokok
    Dim BagiHasil
    Dim Denda
    Dim SudahDibayar
    Dim SisaTagihan
    Dim JumlahCair_Tabel
    Public JumlahPengajuan_Total
    Public JumlahCair_Total
    Dim RekeningPenerima
    Dim AtasNamaPenerima
    Dim PPhTerutang As Int64
    Dim PPhDitanggung As Int64
    Dim PPhDipotong As Int64
    Dim Catatan

    Dim JumlahInvoice
    Dim NomorInvoice_Bundel
    Dim TanggalInvoice_Bundel
    Dim NamaProduk_Bundel

    Dim PPhTerutang_Pasal21 As Int64
    Dim PPhTerutang_Pasal23 As Int64
    Dim PPhTerutang_Pasal42 As Int64
    Dim PPhTerutang_Pasal26 As Int64
    Dim PPhDitanggung_Pasal21 As Int64
    Dim PPhDitanggung_Pasal23 As Int64
    Dim PPhDitanggung_Pasal26 As Int64
    Dim PPhDitanggung_Pasal42 As Int64
    Dim PPhDitanggung_Total As Int64
    Dim PPhDipotong_Total As Int64

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

    'Variabel untuk Bank Garansi
    Dim BiayaProvisi

    'Variabel untuk Hutang Bank/Leasing :
    Dim BiayaAdministrasiKontrak
    Dim BiayaNotarisKontrak
    Dim BiayaPPhKontrak

    'Variabel untuk Investasi Modal :
    Dim JumlahLembarSaham
    Dim HargaPerLembarSaham
    Dim JumlahSaham

    Public InvoiceTunai As Boolean

    Public AdaPenyimpanan As Boolean

    Dim PerluAdaTabel As Boolean

    Dim MataUang As String
    Dim MitraLokal As Boolean
    Dim MitraLuarNegeri As Boolean
    Dim LokasiLawanTransaksi As String

    Public KodeMataUang As String
    Public MataUang_IDR As Boolean
    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If KodeMataUang = Kosongan Then
            PesanUntukProgrammer("Kode Mata Uang belum ditentukan...!!!")
            KodeMataUang = KodeMataUang_IDR
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Penerimaan Bank-Cash"
            SistemPenomoranOtomatis_KM()
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
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                  " WHERE Angka_KM = '" & AngkaKM & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            NomorUrutInvoice = 0
            Do While dr.Read
                'Data Form :
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                JenisPajakPerBaris = dr.Item("Jenis_Pajak")     '2 baris ini disimpan paling awal untuk melancarkan penentuan COA Pajak
                KodeSetoranPerBaris = dr.Item("Kode_Setoran")   '2 baris ini disimpan paling awal untuk melancarkan penentuan COA Pajak
                If NomorUrutInvoice < 1 Then
                    cmb_Kategori.SelectedValue = dr.Item("Kategori")
                    cmb_Peruntukan.SelectedValue = dr.Item("Peruntukan")
                    dtp_TanggalKM.SelectedDate = dr.Item("Tanggal_KM")
                    txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
                    txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
                    cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
                    txt_BiayaAdministrasiBank.Text = PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang, dr.Item("Biaya_Administrasi_Bank"))
                    cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
                    IsiValueElemenRichTextBox(txt_Catatan, dr.Item("Catatan"))
                    txt_NomorKM.Text = dr.Item("Nomor_KM") 'Supaya aman, ini disimpan di akhir sini.
                End If
                'Data Tabel :
                NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
                TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                UraianInvoicePerBaris = PenghapusEnter(dr.Item("Uraian_Invoice"))
                NomorBP = dr.Item("Nomor_BP")
                txt_Kurs.Text = dr.Item("Kurs")
                If Peruntukan = Peruntukan_InvestasiModal Then
                    AksesDatabase_General(Buka)
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Modal WHERE Nomor_BPM = '" & NomorBP & "' ", KoneksiDatabaseGeneral)
                    drTELUSUR_ExecuteReader()
                    drTELUSUR.Read()
                    If drTELUSUR.HasRows Then
                        txt_JumlahLembarSaham.Text = drTELUSUR.Item("Jumlah_Lembar")
                        cmb_HargaPerLembarSaham.SelectedValue = drTELUSUR.Item("Harga_Per_Lembar")
                    End If
                    AksesDatabase_General(Tutup)
                    VisibilitasModal(True)
                End If
                JumlahTagihanPerBaris = dr.Item("Jumlah_Tagihan")
                AngsuranKe = dr.Item("Angsuran_Ke")
                PokokPerBaris = dr.Item("Pokok")
                BagiHasilPerBaris = dr.Item("Bagi_Hasil")
                Perhitungan_SudahDibayarPerBaris()
                JumlahCairPerBaris = 0
                Select Case FungsiForm
                    Case FungsiForm_EDIT
                        JudulForm = "Edit Penerimaan Bank-Cash"
                        btn_Singkirkan.Visibility = Visibility.Visible
                    Case FungsiForm_LIHAT
                        JudulForm = "Penerimaan Bank-Cash"
                        JumlahCairPerBaris = dr.Item("Jumlah_Bayar")
                End Select
                SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
                JumlahCairPerBaris = dr.Item("Jumlah_Bayar")
                PPhTerutangPerBaris = dr.Item("PPh_Terutang")
                PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
                If PerluAdaTabel Then TambahBaris()
                NomorJV_Sebelumnya = dr.Item("Nomor_JV")
                txt_Denda.Text = dr.Item("Denda")
                txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            Loop
            AmbilValueDariTabelPengawasan()
            AksesDatabase_Transaksi(Tutup)
            Perhitungan_Tabel()
            If Peruntukan = Peruntukan_HutangPihakKetiga _
                Or Peruntukan = Peruntukan_HutangKaryawan _
                Or Peruntukan = Peruntukan_HutangPemegangSaham _
                Or Peruntukan = Peruntukan_HutangAfiliasi _
                Then
                LogikaPembebanan()
            End If
            cmb_Kategori.IsEnabled = False
            cmb_Peruntukan.IsEnabled = False
            btn_PilihMitra.IsEnabled = False
            txt_KodeLawanTransaksi.IsEnabled = False
            txt_NamaLawanTransaksi.IsEnabled = False
        End If

        If FungsiForm = FungsiForm_LIHAT _
            Then
            txt_NomorKM.IsEnabled = False
            dtp_TanggalKM.IsEnabled = False
            btn_PilihMitra.IsEnabled = False
            cmb_SaranaPencairan.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
            lbl_Denda.IsEnabled = False
            txt_Denda.IsEnabled = False
        Else
            dtp_TanggalKM.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            grb_Bank.IsEnabled = False
            txt_Catatan.IsEnabled = False
            btn_Simpan.IsEnabled = False
            btn_Batal.Content = teks_Tutup
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Me.Title = JudulForm

        LogikaTampilanKolom()

        ProsesLoadingForm = False

        BersihkanSeleksi()

    End Sub


    Sub AmbilValueDariTabelPengawasan()
        Select Case Peruntukan
            Case Peruntukan_HutangAfiliasi
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangAfiliasi " &
                    " WHERE Nomor_BPHA = '" & NomorBP & "' "
            Case Peruntukan_HutangKaryawan
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangKaryawan " &
                    " WHERE Nomor_BPHK = '" & NomorBP & "' "
            Case Peruntukan_HutangPemegangSaham
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangPemegangSaham " &
                    " WHERE Nomor_BPHPS = '" & NomorBP & "' "
            Case Peruntukan_HutangPihakKetiga
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
                    " WHERE Nomor_BPHPK = '" & NomorBP & "' "
            Case Peruntukan_HutangBank
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangBank " &
                    " WHERE Nomor_BPHB = '" & NomorBP & "' "
            Case Peruntukan_HutangLeasing
                QueryTelusur = " SELECT * FROM tbl_PengawasanHutangLeasing " &
                    " WHERE Nomor_BPHL = '" & NomorBP & "' "
            Case Else
                Return
        End Select
        cmdTELUSUR = New OdbcCommand(QueryTelusur, KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        drTELUSUR.Read()
        If Peruntukan = Peruntukan_HutangBank Or Peruntukan = Peruntukan_HutangLeasing Then
            If drTELUSUR.HasRows Then
                txt_BiayaAdministrasiKontrak.Text = drTELUSUR.Item("Biaya_Administrasi_Kontrak")
                txt_BiayaNotarisKontrak.Text = drTELUSUR.Item("Biaya_Notaris")
                txt_BiayaPPhKontrak.Text = drTELUSUR.Item("Biaya_PPh")
            End If
            VisibilitasBiayaKontrakBankLeasing(True)
            grb_Bank.Visibility = Visibility.Collapsed
        End If
        If Peruntukan = Peruntukan_HutangPihakKetiga _
            Or Peruntukan = Peruntukan_HutangKaryawan _
            Or Peruntukan = Peruntukan_HutangPemegangSaham _
            Or Peruntukan = Peruntukan_HutangAfiliasi _
            Then
            Pembebanan = drTELUSUR.Item("Pembebanan")
            cmb_SaranaPencairan.IsEnabled = False
            grb_Bank.IsEnabled = False
            VisibilitasSaldoAwalHutang(True)
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
            Jenis_Pajak_Per_Baris.Visibility = Visibility.Collapsed
            PPh_Terutang_Per_Baris.Visibility = Visibility.Collapsed
            PPh_Ditanggung_Per_Baris.Visibility = Visibility.Collapsed
            PPh_Dipotong_Per_Baris.Visibility = Visibility.Collapsed
        End If
        If Kategori = Kategori_PenerimaanTunai _
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
    End Sub


    Sub LogikaLokasiLawanTransaksi()
        txt_TotalCair_MUA.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
        txt_TotalCair_IDR.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        If MitraLokal Then
            txt_TotalCair_MUA.Visibility = Visibility.Collapsed
            lbl_TotalCair_MUA.Visibility = Visibility.Collapsed
            lbl_TotalCair_IDR.Text = "Total Cair"
            Buat_DataTabelUtama_Lokal()
            VisibilitasKolomKurs(False)
            MataUang_IDR = True
            MataUang = MataUang_Rupiah
            txt_Denda.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaAdministrasiKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaNotarisKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaPPhKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_SaldoAwalHutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            JumlahCairPerBaris = 0
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(Pokok)
            FormatUlangAngkaKeBilanganBulat(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihan)
            FormatUlangAngkaKeBilanganBulat(SudahDibayar)
            FormatUlangAngkaKeBilanganBulat(SisaTagihan)
            FormatUlangAngkaKeBilanganBulat(JumlahCairPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahCair_Tabel)
            FormatUlangAngkaKeBilanganBulat(BiayaAdministrasiBank)
        Else
            txt_TotalCair_MUA.Visibility = Visibility.Visible
            lbl_TotalCair_MUA.Visibility = Visibility.Visible
            lbl_TotalCair_MUA.Text = "Total Cair (" & KodeMataUang & ")"
            lbl_TotalCair_IDR.Text = "Total Cair (IDR)"
            Buat_DataTabelUtama_Asing()
            VisibilitasKolomKurs(True)
            MataUang_IDR = False
            MataUang = MataUang_Asing
            txt_Denda.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaAdministrasiKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaNotarisKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaPPhKontrak.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_SaldoAwalHutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_BiayaAdministrasiBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
            txt_JumlahTransfer.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_TotalBank.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(Pokok)
            FormatUlangAngkaKeBilanganDesimal(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihan)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayar)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihan)
            FormatUlangAngkaKeBilanganDesimal(JumlahCairPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahCair_Tabel)
            FormatUlangAngkaKeBilanganDesimal(BiayaAdministrasiBank)
        End If
    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        KodeMataUang = Kosongan
        MataUang_IDR = False
        AngkaKM = 0
        PerluAdaTabel = True
        MataUang = MataUang_Rupiah
        Kurs = 1
        VisibilitasKolomKurs(False)
        MitraLokal = True
        MitraLuarNegeri = False

        KontenCombo_Kategori()
        cmb_Peruntukan.Items.Clear()    'Jangan dihapus..!
        cmb_Peruntukan.SelectedValue = Kosongan  'Jangan dihapus..!
        NomorUrutInvoice = 0
        txt_NomorKM.IsEnabled = False
        txt_NomorKM.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalKM)
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        lbl_Denda.Visibility = Visibility.Collapsed
        txt_Denda.Visibility = Visibility.Collapsed
        lbl_Denda.IsEnabled = True
        txt_Denda.IsEnabled = True
        txt_Kurs.Text = 1
        txt_Denda.Text = Kosongan
        KontenComboSaranaPembayaran_Public_WPF(cmb_SaranaPencairan, KodeMataUang)
        COAKredit = Kosongan
        COADebet = Kosongan
        txt_BiayaAdministrasiKontrak.Text = Kosongan
        txt_BiayaNotarisKontrak.Text = Kosongan
        txt_BiayaPPhKontrak.Text = Kosongan
        txt_JumlahLembarSaham.Text = Kosongan
        KontenCombo_HargaPerlembarSaham()
        JumlahSaham = 0
        Reset_grb_Bank()
        txt_SaldoAwalHutang.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Visibility = Visibility.Collapsed
        KosongkanDaftarTagihan()
        KosongkanValueElemenRichTextBox(txt_Catatan)

        cmb_Peruntukan.IsEnabled = True
        dtp_TanggalKM.IsEnabled = True
        btn_PilihMitra.IsEnabled = True
        cmb_SaranaPencairan.IsEnabled = True
        txt_TotalCair_IDR.Visibility = Visibility.Visible

        VisibilitasTabel(False)
        btn_Simpan.IsEnabled = True
        btn_Simpan.Content = teks_Simpan
        btn_Batal.Content = teks_Batal
        btn_Singkirkan.Visibility = Visibility.Collapsed

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

        BiayaProvisi = 0

        InvoiceTunai = False

        AdaPenyimpanan = False

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_KM()

        If FungsiForm = FungsiForm_TAMBAH Then AngkaKM = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_BuktiPenerimaan", "Angka_KM") + 1
        If dtp_TanggalKM.Text <> Kosongan Then
            NomorKM = AwalanKM & AngkaKM & "/" & KonversiSaranaPembayaranKeNamaAkun(SaranaPencairan) & "/" &
                    KonversiAngkaKeStringDuaDigit(dtp_TanggalKM.SelectedDate.Value.Month) & "/" & TahunBukuAktif
            txt_NomorKM.Text = NomorKM
        End If

    End Sub


    Sub KodePembukaDataTagihan()
        KosongkanDaftarTagihan()
        NomorUrutInvoice = 0
        JumlahCairPerBaris = 0
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

    Sub TambahkanDataTagihanPiutangUsaha()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Kode_Customer     = '" & KodeLawanTransaksi & "' " &
            " AND Jenis_Penjualan     = '" & JenisPenjualan_Tempo & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_Penjualan = '" & KonversiNomorBPPUKeNomorPenjualan(NomorBP) & "' "
        QueryTampilan &= " ORDER BY Angka_Invoice, Nomor_ID "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorInvoice_Sebelumnya = Kosongan
        Dim Retur As Int64
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Catatan"))
            NomorBP = KonversiNomorPenjualanKeNomorBPPU(dr.Item("Nomor_Penjualan"))
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            JumlahTagihanPerBaris = dr.Item("Total_Tagihan") - Retur
            JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
            KodeSetoranPerBaris = dr.Item("Kode_Setoran")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - SudahDibayarPerBaris
            JumlahCairPerBaris = SisaTagihanPerBaris
            If MitraLokal Then
                PPhTerutangPerBaris = dr.Item("PPh_Terutang")
                PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
                PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
                RasioPPh()
            Else
                PPhTerutangPerBaris = 0
                PPhDitanggungPerBaris = 0
                PPhDipotongPerBaris = 0
            End If
            If NomorInvoicePerBaris <> NomorInvoice_Sebelumnya And SisaTagihanPerBaris > 0 Then TambahBaris()
            NomorInvoice_Sebelumnya = NomorInvoicePerBaris
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanPiutangUsahaAfiliasi()
        TambahkanDataTagihanPiutangUsaha() 'Sementara ini ngambil dari sub ini saja dulu. Karena sama saja algoritmanya.
    End Sub


    Sub TambahkanDataTagihanPiutangAfiliasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangAfiliasi " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPA = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPA")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangAfiliasi " &
                                         " WHERE Nomor_BPPA = '" & NomorBP & "' " &
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
                JumlahCairPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanPiutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangPemegangSaham WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPPS")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahCairPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanPiutangDividen()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangDividen WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPD = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Akta_Notaris")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPD")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Dividen") - dr.Item("PPh_Dipotong")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahCairPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanPiutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangKaryawan WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPK")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            Perhitungan_SudahDibayarPerBaris()
            SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
            JumlahCairPerBaris = SisaTagihanPerBaris
            If SisaTagihanPerBaris > 0 Then TambahBaris()
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataTagihanPiutangPihakKetiga()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanPiutangPihakKetiga " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPPPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPPPK")
            JumlahTagihanPerBaris = dr.Item("Saldo_Awal")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangPihakKetiga " &
                                         " WHERE Nomor_BPPPK = '" & NomorBP & "' " &
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
                JumlahCairPerBaris = PokokPerBaris + BagiHasilPerBaris - PPhDipotongPerBaris
                TambahBaris()
                If JalurMasuk <> JalurUtama And NomorUrutInvoice >= JumlahAngsuranTerjadwal Then Exit Do
            Loop
        Loop
        KodePenutupDataTagihan()
    End Sub


    Sub TambahkanDataInvoiceTunai()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Kode_Customer     = '" & KodeLawanTransaksi & "' " &
            " AND Jenis_Penjualan     = '" & JenisPenjualan_Tunai & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_Invoice = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Angka_Invoice, Nomor_ID ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim Retur As Int64
        'Dim NomorInvoice_Sebelumnya = Kosongan     '(Tidak memakai konsep loop, karena hanya boleh menampilkan satu baris Invoice...!)
        'Do While dr.Read                           '(Tapi ini tidak dihapus dulu karena khawatir nanti dibutuhkan lagi.)
        '    NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
        '    TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
        '    UraianInvoicePerBaris = PenghapusEnter(dr.Item("Catatan"))
        '    Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
        '    JumlahTagihanPerBaris = dr.Item("Total_Tagihan") - Retur
        '    JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
        '    KodeSetoranPerBaris = dr.Item("Kode_Setoran")
        '    JumlahCairPerBaris = JumlahTagihanPerBaris
        '    PPhTerutangPerBaris = dr.Item("PPh_Terutang")
        '    PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
        '    PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
        '    RasioPPh()
        '    If NomorInvoicePerBaris <> NomorInvoice_Sebelumnya Then TambahBaris()
        '    NomorInvoice_Sebelumnya = NomorInvoicePerBaris
        'Loop
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Invoice")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Catatan"))
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            JumlahTagihanPerBaris = dr.Item("Total_Tagihan") - Retur
            JenisPajakPerBaris = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
            KodeSetoranPerBaris = dr.Item("Kode_Setoran")
            JumlahCairPerBaris = JumlahTagihanPerBaris
            PPhTerutangPerBaris = dr.Item("PPh_Terutang")
            PPhDitanggungPerBaris = dr.Item("PPh_Ditanggung")
            PPhDipotongPerBaris = dr.Item("PPh_Dipotong")
            TambahBaris()
            RasioPPh()
        End If
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPenerimaanHutangPemegangSaham()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPemegangSaham " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPS = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHPS")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            Pembebanan = dr.Item("Pembebanan")
            TambahBaris()
        End If
        cmb_SaranaPencairan.IsEnabled = False
        grb_Bank.IsEnabled = False
        KodePenutupDataTagihan()
        LogikaPembebanan()
    End Sub

    Sub TambahkanDataPenerimaanHutangKaryawan()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangKaryawan " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            Pembebanan = dr.Item("Pembebanan")
            TambahBaris()
        End If
        cmb_SaranaPencairan.IsEnabled = False
        grb_Bank.IsEnabled = False
        KodePenutupDataTagihan()
        LogikaPembebanan()
    End Sub

    Sub TambahkanDataPenerimaanHutangPihakKetiga()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHPK = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHPK")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            Pembebanan = dr.Item("Pembebanan")
            TambahBaris()
        End If
        cmb_SaranaPencairan.IsEnabled = False
        grb_Bank.IsEnabled = False
        KodePenutupDataTagihan()
        LogikaPembebanan()
    End Sub

    Sub TambahkanDataPenerimaanHutangAfiliasi()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangAfiliasi " &
            " WHERE Kode_Lawan_Transaksi = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHA = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHA")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            txt_BiayaAdministrasiBank.Text = dr.Item("Biaya_Administrasi_Bank")
            cmb_DitanggungOleh.SelectedValue = dr.Item("Ditanggung_Oleh")
            Pembebanan = dr.Item("Pembebanan")
            TambahBaris()
        End If
        cmb_SaranaPencairan.IsEnabled = False
        grb_Bank.IsEnabled = False
        KodePenutupDataTagihan()
        LogikaPembebanan()
    End Sub

    Sub TambahkanDataPenerimaanHutangLancarLainnya()

    End Sub

    Sub TambahkanDataPenerimaanHutangLeasing()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangLeasing " &
            " WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV_Pencairan = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHL = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Persetujuan"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHL")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            TambahBaris()
            VisibilitasBiayaKontrakBankLeasing(True)
            txt_BiayaAdministrasiKontrak.Text = dr.Item("Biaya_Administrasi_Kontrak")
            txt_BiayaNotarisKontrak.Text = dr.Item("Biaya_Notaris")
            txt_BiayaPPhKontrak.Text = dr.Item("Biaya_PPh")
        End If
        grb_Bank.Visibility = Visibility.Collapsed
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPenerimaanHutangBank()
        KodePembukaDataTagihan()
        QueryTampilan = " SELECT * FROM tbl_PengawasanHutangBank " &
            " WHERE Kode_Kreditur = '" & KodeLawanTransaksi & "' " &
            " AND Nomor_JV_Pencairan = 0 "
        If JalurMasuk <> JalurUtama Then QueryTampilan += " AND Nomor_BPHB = '" & NomorBP & "' "
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorInvoicePerBaris = dr.Item("Nomor_Kontrak")
            TanggalInvoicePerBaris = TanggalFormatTampilan(dr.Item("Tanggal_Persetujuan"))
            UraianInvoicePerBaris = PenghapusEnter(dr.Item("Keterangan"))
            NomorBP = dr.Item("Nomor_BPHB")
            JumlahTagihanPerBaris = dr.Item("Jumlah_Pinjaman")
            SisaTagihanPerBaris = JumlahTagihanPerBaris
            JumlahCairPerBaris = JumlahTagihanPerBaris
            cmb_SaranaPencairan.SelectedValue = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Debet"))
            TambahBaris()
            VisibilitasBiayaKontrakBankLeasing(True)
            txt_BiayaAdministrasiKontrak.Text = dr.Item("Biaya_Administrasi_Kontrak")
            txt_BiayaNotarisKontrak.Text = dr.Item("Biaya_Notaris")
            txt_BiayaPPhKontrak.Text = dr.Item("Biaya_PPh")
        End If
        grb_Bank.Visibility = Visibility.Collapsed
        KodePenutupDataTagihan()
    End Sub

    Sub TambahkanDataPenerimaanHutangLembagaKeuanganNonBank()

    End Sub

    Sub TambahkanDataDepositOperasional()
        KodePembukaDataTagihan()
        Dim JumlahTalanganPerBaris
        QueryTampilan = " SELECT * FROM tbl_DepositOperasional " &
            " WHERE Kode_Customer = '" & KodeLawanTransaksi & "' "
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
                cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                           " WHERE Nomor_BP         = '" & NomorBP & "' " &
                                           " AND Kode_Setoran       = '" & KodeSetoranPerBaris & "' " &
                                           " AND Status_Invoice     = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
                drBAYAR_ExecuteReader()
                JumlahTalanganPerBaris = 0
                Do While drBAYAR.Read
                    JumlahTalanganPerBaris += drBAYAR.Item("Jumlah_Bayar")
                Loop
                JumlahTagihanPerBaris = JumlahTalanganPerBaris
                Perhitungan_SudahDibayarPerBaris()
                SisaTagihanPerBaris = JumlahTagihanPerBaris - AmbilAngka(SudahDibayarPerBaris)
                JumlahCairPerBaris = SisaTagihanPerBaris
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
                QueryTampilan += " AND COA_Kredit = '" & KodeTautanCOA_InvestasiDeposito & "' "
            Case Peruntukan_InvestasiSuratBerharga
                QueryTampilan += " AND COA_Kredit = '" & KodeTautanCOA_InvestasiSuratBerharga & "' "
            Case Peruntukan_InvestasiLogamMulia
                QueryTampilan += " AND COA_Kredit = '" & KodeTautanCOA_InvestasiLogamMulia & "' "
            Case Peruntukan_InvestasiPadaPerusahaanAnak
                QueryTampilan += " AND COA_Kredit = '" & KodeTautanCOA_InvestasiPadaPerusahaanAnak & "' "
            Case Peruntukan_InvestasiGoodWill
                QueryTampilan += " AND COA_Kredit = '" & KodeTautanCOA_InvestasiGoodWill & "' "
            Case Else
                QueryTampilan += " AND COA_Kredit = 'X' " 'Ini Jangan dihapus...!!!
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
            TambahBaris()
        End If
        KodePenutupDataTagihan()
    End Sub



    Sub RasioPPh()
        Dim PPhTerutang_SudahDibayar = 0
        Dim PPhDitanggung_SudahDibayar = 0
        Dim PPhDipotong_SudahDibayar = 0
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                     " WHERE Nomor_BP       = '" & NomorBP & "' " &
                                     " AND Kode_Setoran     = '" & KodeSetoranPerBaris & "' " &
                                     " AND Angka_KM         < '" & AngkaKM & "' ", KoneksiDatabaseTransaksi)
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
        If MitraLokal Then
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(Pokok)
            FormatUlangAngkaKeBilanganBulat(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganBulat(JumlahTagihan)
            FormatUlangAngkaKeBilanganBulat(SudahDibayar)
            FormatUlangAngkaKeBilanganBulat(SisaTagihan)
            FormatUlangAngkaKeBilanganBulat(JumlahCairPerBaris)
            FormatUlangAngkaKeBilanganBulat(JumlahCair_Tabel)
        Else
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(Pokok)
            FormatUlangAngkaKeBilanganDesimal(BagiHasilPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayarPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihanPerBaris_Terseleksi)
            FormatUlangAngkaKeBilanganDesimal(JumlahTagihan)
            FormatUlangAngkaKeBilanganDesimal(SudahDibayar)
            FormatUlangAngkaKeBilanganDesimal(SisaTagihan)
            FormatUlangAngkaKeBilanganDesimal(JumlahCairPerBaris)
            FormatUlangAngkaKeBilanganDesimal(JumlahCair_Tabel)
        End If
        If Not PerluAdaTabel Then Return
        NomorUrutInvoice += 1
        'PesanUntukProgrammer(
        '    "Kode Mata Uang : " & KodeMataUang & Enter2Baris &
        '    "Nomor Urut Invoice : " & NomorUrutInvoice)
        datatabelUtama.Rows.Add(NomorUrutInvoice, NomorInvoicePerBaris, TanggalInvoicePerBaris, UraianInvoicePerBaris, NomorBP,
                                JumlahTagihanPerBaris, AngsuranKe, PokokPerBaris, BagiHasilPerBaris, SudahDibayarPerBaris, SisaTagihanPerBaris,
                                JenisPajakPerBaris, KodeSetoranPerBaris, PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris,
                                JumlahCairPerBaris,
                                PPhTerutangPerBaris, PPhDitanggungPerBaris, PPhDipotongPerBaris)
    End Sub


    Sub BarisTotal()
        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, " T O T A L ", Kosongan,
                                JumlahTagihan, Kosongan, Pokok, BagiHasil, SudahDibayar, SisaTagihan,
                                Kosongan, Kosongan, PPhTerutang, PPhDitanggung, PPhDipotong,
                                JumlahCair_Tabel)
    End Sub


    Sub Perhitungan_SudahDibayarPerBaris()
        SudahDibayarPerBaris = 0
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPenerimaan " &
                                   " WHERE Nomor_BP         = '" & NomorBP & "' " &
                                   " AND Kode_Setoran       = '" & KodeSetoranPerBaris & "' " &
                                   " AND Angka_KM           < '" & AngkaKM & "' ", KoneksiDatabaseTransaksi)
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
        JumlahCair_Tabel = 0
        PPhTerutang = 0
        PPhDitanggung = 0
        PPhDipotong = 0
        JumlahInvoice = NomorUrutInvoice
        JumlahAngsuranTerjadwal = JumlahInvoice
        If datatabelUtama.Rows.Count > JumlahInvoice Then
            datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
            datatabelUtama.Rows.RemoveAt(datatabelUtama.Rows.Count - 1)
        End If
        For Each row As DataRow In datatabelUtama.Rows
            If PembayaranTerjadwal Then
                JumlahTagihan = AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
            Else
                JumlahTagihan += AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
            End If
            If MitraLokal Then
                Pokok += AmbilAngka(row("Pokok_Per_Baris").ToString)
                BagiHasil += AmbilAngka(row("Bagi_Hasil_Per_Baris").ToString)
                SudahDibayar += AmbilAngka(row("Sudah_Dibayar_Per_Baris").ToString)
                SisaTagihan += AmbilAngka(row("Sisa_Tagihan_Per_Baris").ToString)
                JumlahCair_Tabel += AmbilAngka(row("Jumlah_Cair_Per_Baris").ToString)
            Else
                Pokok += AmbilAngka_Asing(row("Pokok_Per_Baris").ToString)
                BagiHasil += AmbilAngka_Asing(row("Bagi_Hasil_Per_Baris").ToString)
                SudahDibayar += AmbilAngka_Asing(row("Sudah_Dibayar_Per_Baris").ToString)
                SisaTagihan += AmbilAngka_Asing(row("Sisa_Tagihan_Per_Baris").ToString)
                JumlahCair_Tabel += AmbilAngka_Asing(row("Jumlah_Cair_Per_Baris").ToString)
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
    End Sub
    Sub Perhitungan_Total()
        Dim JumlahCair_Tabel_IDR As Int64 = AmbilValue_NilaiMataUang_BulatKeAtas(KodeMataUang, Kurs, JumlahCair_Tabel)
        If Peruntukan = Peruntukan_InvestasiModal Then
            JumlahSaham = JumlahLembarSaham * HargaPerLembarSaham
            txt_TotalCair_IDR.Text = JumlahSaham
        Else
            If JumlahCair_Tabel = 0 Then
                txt_TotalCair_IDR.Text = 0
                txt_TotalCair_MUA.Text = 0
            Else
                txt_TotalCair_IDR.Text = FormatUlangInt64(JumlahCair_Tabel_IDR + Denda - BiayaAdministrasiKontrak - BiayaNotarisKontrak)
                If Not MitraLokal Then txt_TotalCair_MUA.Text = JumlahCair_Tabel + Denda - BiayaAdministrasiKontrak - BiayaNotarisKontrak
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

    Sub VisibilitasSaldoAwalHutang(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_SaldoAwalHutang.Visibility = Visibility.Visible
            txt_SaldoAwalHutang.Visibility = Visibility.Visible
        Else
            lbl_SaldoAwalHutang.Visibility = Visibility.Collapsed
            txt_SaldoAwalHutang.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasBiayaKontrakBankLeasing(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_BiayaAdministrasiKontrak.Visibility = Visibility.Visible
            txt_BiayaAdministrasiKontrak.Visibility = Visibility.Visible
            lbl_BiayaNotarisKontrak.Visibility = Visibility.Visible
            txt_BiayaNotarisKontrak.Visibility = Visibility.Visible
            lbl_BiayaPPhKontrak.Visibility = Visibility.Visible
            txt_BiayaPPhKontrak.Visibility = Visibility.Visible
        Else
            lbl_BiayaAdministrasiKontrak.Visibility = Visibility.Collapsed
            txt_BiayaAdministrasiKontrak.Visibility = Visibility.Collapsed
            lbl_BiayaNotarisKontrak.Visibility = Visibility.Collapsed
            txt_BiayaNotarisKontrak.Visibility = Visibility.Collapsed
            lbl_BiayaPPhKontrak.Visibility = Visibility.Collapsed
            txt_BiayaPPhKontrak.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasModal(Visibilitas)
        If Visibilitas Then
            lbl_JumlahLembarSaham.Visibility = Visibility.Visible
            txt_JumlahLembarSaham.Visibility = Visibility.Visible
            lbl_HargaPerLembarSaham.Visibility = Visibility.Visible
            cmb_HargaPerLembarSaham.Visibility = Visibility.Visible
        Else
            lbl_JumlahLembarSaham.Visibility = Visibility.Collapsed
            txt_JumlahLembarSaham.Visibility = Visibility.Collapsed
            lbl_HargaPerLembarSaham.Visibility = Visibility.Collapsed
            cmb_HargaPerLembarSaham.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub KontenCombo_Kategori()
        KontenCombo_KategoriPenerimaan_Public(cmb_Kategori)
        cmb_Kategori.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPencairanPiutang()
        KontenCombo_PeruntukanPencairanPiutang_Public(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPenerimaanTunai()
        KontenCombo_PeruntukanPenerimaanTunai_Public(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanInvestasi()
        KontenCombo_PeruntukanInvestasi_Public(cmb_Peruntukan)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPengembalian()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_LebihBayarPPhBadan)
        cmb_Peruntukan.Items.Add(Peruntukan_KelebihanPenerimaanPembayaranHutang)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub


    Sub KontenCombo_HargaPerlembarSaham()
        cmb_HargaPerLembarSaham.Items.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DaftarSaham ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Dim ItemSaham As String = Kosongan
        Do While dr.Read
            ItemSaham = PemecahRibuanUntukString(dr.Item("Harga"))
            cmb_HargaPerLembarSaham.Items.Add(ItemSaham)
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub cmb_Kategori_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kategori.SelectionChanged
        Kategori = cmb_Kategori.SelectedValue
        Select Case Kategori
            Case Kategori_PencairanPiutang
                KontenCombo_PeruntukanPencairanPiutang()
            Case Kategori_PenerimaanTunai
                KontenCombo_PeruntukanPenerimaanTunai()
            Case Kategori_Investasi
                KontenCombo_PeruntukanInvestasi()
            Case Kategori_Pengembalian
                KontenCombo_PeruntukanPengembalian()
            Case Kategori_Try
                KodePembukaDataTagihan()
                Dim i = 0
                Do While i <= 50
                    i += 1
                    datatabelUtama.Rows.Add(i, "Percobaan", "Apa weeeeh...!")
                Loop
                KodePenutupDataTagihan()
        End Select
    End Sub


    Private Sub cmb_Peruntukan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Peruntukan.SelectionChanged

        Peruntukan = cmb_Peruntukan.SelectedValue

        InvoiceTunai = False

        If Peruntukan <> Kosongan Then

            If Peruntukan = Peruntukan_InvoiceTunai Then
                InvoiceTunai = True
                COAKredit = Kosongan
            ElseIf Kategori = Kategori_Pemindahbukuan Then
                COAKredit = KonversiSaranaPembayaranKeCOA(Peruntukan)
            Else
                COAKredit = KonversiPeruntukanKeCOA(Peruntukan)
            End If

        End If

        KosongkanDaftarTagihan()

        txt_KodeLawanTransaksi.Text = Kosongan

        PembayaranDenganInvoice = True
        PembayaranTerjadwal = False
        PerluAdaTabel = True
        Nomor_Invoice_Per_Baris.Header = "Nomor Dokumen"
        Tanggal_Invoice_Per_Baris.Header = "Tanggal Dokumen"
        TabelJadwalAngsuran = Kosongan
        KolomBPJadwalAngsuran = Kosongan
        VisibilitasModal(False)

        Select Case Peruntukan
            'Pencairan Piutang :
            Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                Nomor_Invoice_Per_Baris.Header = "Nomor Invoice"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Invoice"
            Case Peruntukan_PencairanPiutangUsaha_Afiliasi
                Nomor_Invoice_Per_Baris.Header = "Nomor Invoice"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Invoice"
            Case Peruntukan_PencairanPiutangAfiliasi
                TabelJadwalAngsuran = "tbl_JadwalAngsuranPiutangAfiliasi"
                KolomBPJadwalAngsuran = "Nomor_BPPA"
                PembayaranTerjadwal = True
            Case Peruntukan_PencairanPiutangPihakKetiga
                TabelJadwalAngsuran = "tbl_JadwalAngsuranPiutangPihakKetiga"
                KolomBPJadwalAngsuran = "Nomor_BPPPK"
                PembayaranTerjadwal = True
            Case Peruntukan_PencairanPiutangDividen
                Nomor_Invoice_Per_Baris.Header = "Nomor Akta Notaris"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Akta Notaris"
            Case Peruntukan_PencairanPiutangLainnya
                PesanUntukProgrammer("Belum ada Koding untuk pilihan ini...!")
            'Penerimaan Tunai :
            Case Peruntukan_InvoiceTunai
                'PesanPemberitahuan("Untuk Transaksi Penjualan Tunai, tidak disediakan di sini. " &
                '                   Enter2Baris & "Silakan input langsung di Invoce")
                Nomor_Invoice_Per_Baris.Header = "Nomor Invoice"
                Tanggal_Invoice_Per_Baris.Header = "Tanggal Invoice"
            Case Peruntukan_HutangPemegangSaham
                'Belum ada kode
            Case Peruntukan_HutangKaryawan
                'Belum ada kode
            Case Peruntukan_HutangPihakKetiga
                'Belum ada kode
            Case Peruntukan_HutangAfiliasi
                'Belum ada kode
            Case Peruntukan_DepositOperasional
                'Belum ada kode
            Case Peruntukan_BankGaransi
                'Belum ada kode
            'Investasi :
            Case Peruntukan_InvestasiModal
                PerluAdaTabel = False
                VisibilitasModal(True)
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


    Private Sub txt_NomorKM_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorKM.TextChanged
        NomorKM = txt_NomorKM.Text
    End Sub


    Private Sub dtp_TanggalKM_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalKM.SelectedDateChanged
        If dtp_TanggalKM.Text <> Kosongan Then
            LogikaUmumInputanTanggal(dtp_TanggalKM)
            TanggalKM = TanggalFormatTampilan(dtp_TanggalKM.SelectedDate)
            If ProsesLoadingForm = False Then SistemPenomoranOtomatis_KM()
            If KodeMataUang <> Kosongan Then
                If KodeMataUang = KodeMataUang_IDR Then
                    txt_Kurs.Text = 1
                Else
                    txt_Kurs.Text = AmbilValue_KursTengahBI(KodeMataUang, TanggalKM)
                End If
            End If
        End If
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        SistemPenomoranOtomatis_KM() 'Ini Jangan dihapus. Sangat dibutuhkan..!
        lbl_NomorFakturPajak.Visibility = Visibility.Collapsed
        txt_NomorFakturPajak.Visibility = Visibility.Collapsed
        If KodeLawanTransaksi = Kosongan Then
            KosongkanDaftarTagihan()
            txt_NamaLawanTransaksi.Text = Kosongan
            cmb_SaranaPencairan.SelectedValue = Kosongan
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
                'Pencairan Piutang :
                Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangUsaha()
                Case Peruntukan_PencairanPiutangUsaha_Afiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangUsahaAfiliasi()
                Case Peruntukan_PencairanPiutangAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangAfiliasi()
                Case Peruntukan_PencairanPiutangPihakKetiga
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangPihakKetiga()
                Case Peruntukan_PencairanPiutangPemegangSaham
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangPemegangSaham()
                Case Peruntukan_PencairanPiutangKaryawan
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaKaryawan(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangKaryawan()
                Case Peruntukan_PencairanPiutangDividen
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataTagihanPiutangDividen()
                Case Peruntukan_PencairanPiutangLainnya
                    PesanUntukProgrammer("Belum ada coding untuk pilihan ini..!!!")
                'Pengeluaran Tunai :
                Case Peruntukan_InvoiceTunai
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataInvoiceTunai()
                    If PerusahaanSebagaiPKP Then
                        lbl_NomorFakturPajak.Visibility = Visibility.Visible
                        txt_NomorFakturPajak.Visibility = Visibility.Visible
                    End If
                Case Peruntukan_UangMukaJangkaPanjang
                    PesanUntukProgrammer("Belum ada coding untuk PILIHAN ini...!!!")
                Case Peruntukan_HutangOngkosKirimPenjualan
                    PesanUntukProgrammer("Belum ada coding untuk PILIHAN ini...!!!")
                Case Peruntukan_HutangKoperasiKaryawan
                    PesanUntukProgrammer("Belum ada coding untuk PILIHAN ini...!!!")
                Case Peruntukan_HutangSerikat
                    PesanUntukProgrammer("Belum ada coding untuk PILIHAN ini...!!!")
                Case Peruntukan_HutangPihakKetiga
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangPihakKetiga()
                Case Peruntukan_HutangKaryawan
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaKaryawan(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangKaryawan()
                Case Peruntukan_HutangLancarLainnya
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangLancarLainnya()
                Case Peruntukan_HutangLeasing
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangLeasing()
                Case Peruntukan_HutangBank
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangBank()
                Case Peruntukan_HutangLembagaKeuanganNonBank
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangLembagaKeuanganNonBank()
                Case Peruntukan_HutangPemegangSaham
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaPemegangSaham(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangPemegangSaham()
                Case Peruntukan_HutangAfiliasi
                    txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
                    If Not (ProsesLoadingForm Or ProsesResetForm) Then TambahkanDataPenerimaanHutangAfiliasi()
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
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        Select Case Peruntukan
            'Pencairan Piutang :
            Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Semua)
            Case Peruntukan_PencairanPiutangUsaha_Afiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua)
            Case Peruntukan_PencairanPiutangAfiliasi
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Tidak)
            Case Peruntukan_PencairanPiutangPihakKetiga
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Tidak)
            Case Peruntukan_PencairanPiutangPemegangSaham
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            Case Peruntukan_PencairanPiutangDividen
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            Case Peruntukan_PencairanPiutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_PencairanPiutangLainnya
                PesanUntukProgrammer("Belum ada Koding untuk pilihan ini...!")
            '----------------
            'Penerimaan Tunai :
            Case Peruntukan_InvoiceTunai
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
            Case Peruntukan_UangMukaJangkaPanjang
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangOngkosKirimPenjualan
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangKoperasiKaryawan
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangSerikat
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangPihakKetiga
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak, Pilihan_Tidak)
            Case Peruntukan_HutangKaryawan
                frm_ListDataKaryawan.ResetForm()
                If txt_KodeLawanTransaksi.Text <> Kosongan Then frm_ListDataKaryawan.NIK_Terseleksi = txt_KodeLawanTransaksi.Text
                frm_ListDataKaryawan.ShowDialog()
                txt_KodeLawanTransaksi.Text = frm_ListDataKaryawan.NomorIDKaryawan_Terseleksi
            Case Peruntukan_HutangLancarLainnya
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangLeasing
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya)
            Case Peruntukan_HutangBank
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya)
            Case Peruntukan_HutangLembagaKeuanganNonBank
                PesanUntukProgrammer("Belum ada Coding untuk Pilihan ini...!")
            Case Peruntukan_HutangPemegangSaham
                BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Ya, Pilihan_Semua, Pilihan_Tidak)
            Case Peruntukan_HutangAfiliasi
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


    Private Sub txt_BiayaAdministrasiKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiKontrak.TextChanged
        If MitraLokal Then
            BiayaAdministrasiKontrak = AmbilAngka(txt_BiayaAdministrasiKontrak.Text)
        Else
            BiayaAdministrasiKontrak = AmbilAngka_Asing(txt_BiayaAdministrasiKontrak.Text)
        End If
        Perhitungan_Total()
    End Sub


    Private Sub txt_BiayaNotarisKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaNotarisKontrak.TextChanged
        If MitraLokal Then
            BiayaNotarisKontrak = AmbilAngka(txt_BiayaNotarisKontrak.Text)
        Else
            BiayaNotarisKontrak = AmbilAngka_Asing(txt_BiayaNotarisKontrak.Text)
        End If
        Perhitungan_Total()
    End Sub


    Private Sub txt_BiayaPPhKontrak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaPPhKontrak.TextChanged
        BiayaPPhKontrak = AmbilAngka(txt_BiayaPPhKontrak.Text)
        Perhitungan_Total()
    End Sub


    Private Sub txt_JumlahLembarSaham_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahLembarSaham.TextChanged
        JumlahLembarSaham = AmbilAngka(txt_JumlahLembarSaham.Text)
        Perhitungan_Total()
    End Sub


    Private Sub cmb_HargaPerLembarSaham_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_HargaPerLembarSaham.SelectionChanged
        HargaPerLembarSaham = AmbilAngka(cmb_HargaPerLembarSaham.SelectedValue)
        Perhitungan_Total()
    End Sub


    Private Sub txt_TotalCair_IDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalCair_IDR.TextChanged
        If MitraLokal Then
            FormatUlangAngkaKeBilanganBulat(JumlahCair_Total)
            JumlahCair_Total = AmbilAngka(txt_TotalCair_IDR.Text)
        End If
    End Sub

    Private Sub txt_TotalCair_MUA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalCair_MUA.TextChanged
        If Not MitraLokal Then
            FormatUlangAngkaKeBilanganDesimal(JumlahCair_Total)
            JumlahCair_Total = AmbilAngka_Asing(txt_TotalCair_MUA.Text)
        End If
    End Sub


    Private Sub cmb_SaranaPencairan_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SaranaPencairan.SelectionChanged
        SaranaPencairan = cmb_SaranaPencairan.SelectedValue
        COADebet = KonversiSaranaPembayaranKeCOA(SaranaPencairan)
        If ProsesLoadingForm = False Then SistemPenomoranOtomatis_KM()
        LogikaVisibilitasBank()
    End Sub
    Sub LogikaVisibilitasBank()
        If COATermasukBank(COADebet) Then
            PembayaranViaBank = True
            grb_Bank.Visibility = Visibility.Visible
            KontenComboDitanggungOleh_Public_WPF(cmb_DitanggungOleh)
            Perhitungan_ValueBank()
        Else
            Reset_grb_Bank()
        End If
        If Peruntukan = Peruntukan_HutangBank Or Peruntukan = Peruntukan_HutangLeasing Then
            grb_Bank.Visibility = Visibility.Collapsed
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

    Sub Reset_grb_Bank()
        PembayaranViaBank = False
        grb_Bank.Visibility = Visibility.Collapsed
        txt_BiayaAdministrasiBank.Text = Kosongan
        cmb_DitanggungOleh.SelectedValue = Kosongan
        cmb_DitanggungOleh.IsEnabled = False
        Pembebanan = Kosongan
        If Not (COATermasukBank(COADebet)) Then
            txt_JumlahTransfer.Text = Kosongan
            txt_TotalBank.Text = Kosongan
        End If
    End Sub

    Private Sub txt_BiayaAdministrasiBank_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_BiayaAdministrasiBank.TextChanged
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
        Perhitungan_ValueBank_Public(KodeMataUang, AlurTransaksi_IN, JumlahCair_Total, JumlahTransfer, BiayaAdministrasiBank, TotalBank, DitanggungOleh)
        txt_JumlahTransfer.Text = JumlahTransfer
        txt_TotalBank.Text = TotalBank
    End Sub

    Sub LogikaPembebanan()
        If Pembebanan = Kosongan Then
            txt_SaldoAwalHutang.Text = JumlahCair_Total
        Else
            txt_JumlahTransfer.Text = JumlahCair_Total
            Select Case Pembebanan
                Case Pembebanan_Dipotong
                    SaldoAwalHutang = JumlahTransfer
                    txt_JumlahTransfer.Text = JumlahTransfer - BiayaAdministrasiBank
                Case Pembebanan_Ditambahkan
                    SaldoAwalHutang = JumlahTransfer + BiayaAdministrasiBank
                    txt_JumlahTransfer.Text = JumlahTransfer
                Case Pembebanan_Diganti
                    SaldoAwalHutang = JumlahTransfer
                    txt_JumlahTransfer.Text = JumlahTransfer
            End Select
            txt_SaldoAwalHutang.Text = SaldoAwalHutang
            txt_TotalBank.Text = JumlahTransfer
        End If
        VisibilitasSaldoAwalHutang(True)
    End Sub


    Dim SaldoAwalHutang
    Private Sub txt_SaldoAwalHutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalHutang.TextChanged
        SaldoAwalHutang = AmbilAngka(txt_SaldoAwalHutang.Text)
    End Sub

    Dim NomorFakturPajak
    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
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
        JumlahTagihanPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Tagihan_Per_Baris").ToString)
        SudahDibayarPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sudah_Dibayar_Per_Baris").ToString)
        SisaTagihanPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sisa_Tagihan_Per_Baris").ToString)
        JumlahCairPerBaris_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Cair_Per_Baris").ToString)

        If JumlahInvoice > 1 Then
            If NomorUrutInvoice_Terseleksi > 0 Then
                btn_Singkirkan.IsEnabled = True
            Else
                btn_Singkirkan.IsEnabled = False
            End If
        End If

        'If NomorUrutInvoice_Terseleksi > 0 And KolomTerseleksi.DisplayIndex = Jumlah_Cair_Per_Baris.DisplayIndex _
        If NomorUrutInvoice_Terseleksi > 0 _
            Then

            If (Kategori = Kategori_PenerimaanTunai And Peruntukan <> Peruntukan_DepositOperasional) Then Return
            If Kategori = Kategori_Investasi Then Return
            If PembayaranTerjadwal Then Return
            If FungsiForm = FungsiForm_LIHAT Then Return
            If dtp_TanggalKM.Text = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Tanggal Bank Cash In' terlebih dahulu.")
                dtp_TanggalKM.Focus()
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
            win_InputJumlahBankCashInOut.JudulForm = "Jumlah Cair"
            win_InputJumlahBankCashInOut.txt_Jumlah.Text = JumlahCairPerBaris_Terseleksi
            win_InputJumlahBankCashInOut.PPhTerutang_ValueAwal_dB = rowviewUtama("PPh_Terutang_Per_Baris_Value_Awal_dB")
            win_InputJumlahBankCashInOut.PPhDitanggung_ValueAwal_dB = rowviewUtama("PPh_Ditanggung_Per_Baris_Value_Awal_dB")
            win_InputJumlahBankCashInOut.PPhDipotong_ValueAwal_dB = rowviewUtama("PPh_Dipotong_Per_Baris_Value_Awal_dB")
            win_InputJumlahBankCashInOut.ShowDialog()
            If win_InputJumlahBankCashInOut.Proses = True Then
                rowviewUtama("Jumlah_Cair_Per_Baris") = win_InputJumlahBankCashInOut.JumlahInputan
                Perhitungan_Tabel()
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    'rowviewUtama("Jenis_Pajak_Per_Baris") = win_InputJumlahBankCashInOut .JenisPajak         'Ini malah bikin masalah, dan sepertinya juga tidak perlu.
                    'rowviewUtama("Kode_Setoran_Per_Baris") = win_InputJumlahBankCashInOut .KodeSetoran       'Suatu saat nanti, hapus saja jika memang penonaktifan ini tidak menimbulkan masalah.
                    rowviewUtama("PPh_Terutang_Per_Baris") = win_InputJumlahBankCashInOut.PPhTerutang_UntukIsiValue
                    rowviewUtama("PPh_Ditanggung_Per_Baris") = win_InputJumlahBankCashInOut.PPhDitanggung_UntukIsiValue
                    rowviewUtama("PPh_Dipotong_Per_Baris") = win_InputJumlahBankCashInOut.PPhDipotong_UntukIsiValue
                End If
            End If

        End If 'Setelah baris ini, jangan ada koding lagi di dalam sub ini. ======================================================

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Untuk Saat ini, belum ada Coding di sini...!
    End Sub


    Sub KosongkanDaftarTagihan()
        datatabelUtama.Rows.Clear()
    End Sub


    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click
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

        PPhTerutang_Pasal21 = 0
        PPhTerutang_Pasal23 = 0
        PPhTerutang_Pasal42 = 0
        PPhTerutang_Pasal26 = 0
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

        If dtp_TanggalKM.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Bank Cash In'.")
            dtp_TanggalKM.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If Peruntukan = Peruntukan_InvestasiModal Then

            If JumlahLembarSaham = 0 Then
                PesanPeringatan("Silakan isi kolom 'Jumlah Lembar Saham'.")
                txt_JumlahLembarSaham.Focus()
                Return
            End If

            If HargaPerLembarSaham = 0 Then
                PesanPeringatan("Silakan pilih 'Harga Perlembar Saham'.")
                cmb_HargaPerLembarSaham.Focus()
                Return
            End If

        End If

        If Kurs = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_Kurs, "Kurs")
            Return
        End If

        If SaranaPencairan = Kosongan Then
            PesanPeringatan("Silakan pilih 'Sarana Pencairan'.")
            cmb_SaranaPencairan.Focus()
            Return
        End If

        If PembayaranViaBank Then

            If BiayaAdministrasiBank > 0 And DitanggungOleh = Kosongan Then
                PesanPeringatan("Silakan pilih 'Ditanggung Oleh'.")
                cmb_DitanggungOleh.Focus()
                Return
            End If

        End If

        If txt_NomorFakturPajak.Visibility = Visibility.Visible Then
            If NomorFakturPajak = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Nomor Faktur Pajak'.")
                txt_NomorFakturPajak.Focus()
                Return
            End If
        End If

        If PerluAdaTabel Then

            If datatabelUtama.Rows.Count = 0 Then
                PesanPeringatan("Silakan isi 'Tabel Pembayaran'.")
                Return
            End If

        End If

        If Catatan = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeksKaya(txt_Catatan, "Catatan")
            Return
        End If

        If Peruntukan = Peruntukan_PencairanPiutangUsaha_NonAfiliasi Then
            Select Case KodeMataUang
                Case KodeMataUang_IDR
                    COAKredit = KodeTautanCOA_PiutangUsaha_NonAfiliasi
                Case KodeMataUang_USD
                    COAKredit = KodeTautanCOA_PiutangUsaha_USD
                Case KodeMataUang_AUD
                    COAKredit = KodeTautanCOA_PiutangUsaha_AUD
                Case KodeMataUang_JPY
                    COAKredit = KodeTautanCOA_PiutangUsaha_JPY
                Case KodeMataUang_CNY
                    COAKredit = KodeTautanCOA_PiutangUsaha_CNY
                Case KodeMataUang_EUR
                    COAKredit = KodeTautanCOA_PiutangUsaha_EUR
                Case KodeMataUang_SGD
                    COAKredit = KodeTautanCOA_PiutangUsaha_SGD
                Case KodeMataUang_GBP
                    COAKredit = KodeTautanCOA_PiutangUsaha_GBP
            End Select
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If Not KodeMataUang = KodeMataUang_IDR Then
                JurnalAdjusment_Forex(COADebet, TanggalKM)
                JurnalAdjusment_Forex(COAKredit, TanggalKM)
            End If
        End If

        StatusSuntingDatabase = True

        AksesDatabase_Transaksi(Buka)

        NomorJV = 0
        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
            Case FungsiForm_EDIT
                jur_NomorJV = NomorJV_Sebelumnya
                NomorJV = NomorJV_Sebelumnya
                HapusDataPengajuanLama()
                HapusJurnal_BerdasarkanNomorJV(NomorJV)
        End Select

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BuktiPenerimaan")

        For Each row As DataRow In datatabelUtama.Rows 'Awal Loop =========================================================

            If AmbilAngka(row("Nomor_Urut_Invoice").ToString) = 0 Then Exit For

            NomorBP = row("Nomor_BP_Per_Baris")
            NomorInvoicePerBaris = row("Nomor_Invoice_Per_Baris")
            TanggalInvoicePerBaris = row("Tanggal_Invoice_Per_Baris")
            UraianInvoicePerBaris = row("Uraian_Invoice_Per_Baris")
            JumlahTagihanPerBaris = AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
            AngsuranKe = AmbilAngka(row("Angsuran_Ke").ToString)
            If MataUang_IDR Then
                PokokPerBaris = AmbilAngka(row("Pokok_Per_Baris").ToString)
                BagiHasilPerBaris = AmbilAngka(row("Bagi_Hasil_Per_Baris").ToString)
                JumlahTagihanPerBaris = AmbilAngka(row("Jumlah_Tagihan_Per_Baris").ToString)
                JumlahCairPerBaris = AmbilAngka(row("Jumlah_Cair_Per_Baris").ToString)
            Else
                PokokPerBaris = AmbilAngka_Asing(row("Pokok_Per_Baris").ToString)
                BagiHasilPerBaris = AmbilAngka_Asing(row("Bagi_Hasil_Per_Baris").ToString)
                JumlahTagihanPerBaris = AmbilAngka_Asing(row("Jumlah_Tagihan_Per_Baris").ToString)
                JumlahCairPerBaris = AmbilAngka_Asing(row("Jumlah_Cair_Per_Baris").ToString)
            End If


            JenisPajakPerBaris = row("Jenis_Pajak_Per_Baris").ToString
            KodeSetoranPerBaris = row("Kode_Setoran_Per_Baris").ToString
            PPhTerutangPerBaris = AmbilAngka(row("PPh_Terutang_Per_Baris").ToString)
            PPhDitanggungPerBaris = AmbilAngka(row("PPh_Ditanggung_Per_Baris").ToString)
            PPhDipotongPerBaris = PPhTerutangPerBaris - PPhDitanggungPerBaris '(Ini jangan dirubah. Supaya hasilnya lebih akurat ketika angkanya tidak bulat)

            If Peruntukan = Peruntukan_PembayaranHutangPajak Then
                NomorInvoicePerBaris = KodePajak
            End If

            SimpanDataPerBaris()

            'Pembundelan Nomor dan Tanggal Invoice :
            If PembayaranTerjadwal Then
                NomorInvoice_Bundel = NomorInvoicePerBaris
                TanggalInvoice_Bundel = TanggalInvoicePerBaris
            Else
                If NomorInvoice_Bundel = Kosongan Then
                    NomorInvoice_Bundel = NomorInvoicePerBaris
                    TanggalInvoice_Bundel = TanggalInvoicePerBaris
                    NamaProduk_Bundel = AmbilValue_ListProdukBerdasarkanInvoicePenjualan(NomorInvoicePerBaris)
                Else
                    NomorInvoice_Bundel &= SlashGanda_Pemisah & NomorInvoicePerBaris
                    TanggalInvoice_Bundel &= SlashGanda_Pemisah & TanggalInvoicePerBaris
                    NamaProduk_Bundel &= SlashGanda_Pemisah & AmbilValue_ListProdukBerdasarkanInvoicePenjualan(NomorInvoicePerBaris)
                End If
            End If

            If Not PembayaranDenganInvoice Then
                'NomorInvoice_Bundel = Kosongan
                TanggalInvoice_Bundel = Kosongan
            End If

            'Pembundelan PPh Terutang dan PPh Ditanggung Berdasarkan Jenis PPh :
            Select Case JenisPajakPerBaris
                Case JenisPajak_PPhPasal21
                    PPhTerutang_Pasal21 += PPhTerutangPerBaris
                    PPhDitanggung_Pasal21 += PPhDitanggungPerBaris
                Case JenisPajak_PPhPasal23
                    PPhTerutang_Pasal23 += PPhTerutangPerBaris
                    PPhDitanggung_Pasal23 += PPhDitanggungPerBaris
                Case JenisPajak_PPhPasal26
                    PPhTerutang_Pasal26 += PPhTerutangPerBaris
                    PPhDitanggung_Pasal26 += PPhDitanggungPerBaris
                Case JenisPajak_PPhPasal42
                    PPhDitanggung_Pasal42 += PPhDitanggungPerBaris
                    PPhTerutang_Pasal42 += PPhTerutangPerBaris
                Case Else
                    'Ini jangan dihapus..!
                    'Ini berfungsi untuk mereset kembali variabel yang tidak termasuk Jenis Pajak dan Kode Setoran yang sudah tercantum.
                    JenisPajakPerBaris = Kosongan
                    KodeSetoranPerBaris = Kosongan
            End Select
            PPhDitanggung_Total += PPhDitanggungPerBaris
            PPhDipotong_Total += PPhDipotongPerBaris

            If PembayaranTerjadwal Then
                '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                If TabelJadwalAngsuran = Kosongan Then PesanUntukProgrammer("Tabel Jadwal Angsuran belum ditentukan...!!!")
                If KolomBPJadwalAngsuran = Kosongan Then PesanUntukProgrammer("Kolom BP Jadwal Angsuran belum ditentukan...!!!")
                cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                      " Tanggal_Bayar                           = '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                      " Denda                                   = '" & 0 & "', " &
                                      " COA_Debet                               = '" & COADebet & "', " &
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

            If Kategori = Kategori_PenerimaanTunai Then
                '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                Select Case Peruntukan
                    Case Peruntukan_InvoiceTunai
                        cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Invoice SET " &
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV            = '" & NomorJV & "' " &
                                              " WHERE Nomor_Invoice = '" & NomorInvoicePerBaris & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangPemegangSaham
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangPemegangSaham SET " &
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV            = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHPS   = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangKaryawan
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangKaryawan SET " &
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV            = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHK    = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangPihakKetiga
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangPihakKetiga SET " &
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV            = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHPK   = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangAfiliasi
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangAfiliasi SET " &
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV            = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHA    = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangBank
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangBank SET " &
                                              " COA_Debet                   = '" & COADebet & "', " &
                                              " Tanggal_Pencairan           = '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                              " Biaya_Administrasi_Kontrak  = '" & BiayaAdministrasiKontrak & "', " &
                                              " Biaya_Notaris               = '" & BiayaNotarisKontrak & "', " &
                                              " Biaya_PPh                   = '" & BiayaPPhKontrak & "', " &
                                              " Nomor_JV_Pencairan          = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHB            = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                    Case Peruntukan_HutangLeasing
                        cmd = New OdbcCommand(" UPDATE tbl_PengawasanHutangLeasing SET " &
                                              " COA_Debet                   = '" & COADebet & "', " &
                                              " Tanggal_Pencairan           = '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                              " Biaya_Administrasi_Kontrak  = '" & BiayaAdministrasiKontrak & "', " &
                                              " Biaya_Notaris               = '" & BiayaNotarisKontrak & "', " &
                                              " Biaya_PPh                   = '" & BiayaPPhKontrak & "', " &
                                              " Nomor_JV_Pencairan          = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPHL            = '" & NomorBP & "' ",
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
                                              " COA_Debet           = '" & COADebet & "', " &
                                              " Nomor_JV_Transaksi  = '" & NomorJV & "' " &
                                              " WHERE Nomor_BPBG    = '" & NomorBP & "' ",
                                              KoneksiDatabaseTransaksi)
                        cmd_ExecuteNonQuery()
                End Select
            End If

            If Kategori = Kategori_Investasi Then
                '(Ini sudah benar posisinya di dalam Loop-ForEachNext. Jangan dikeluarkan)
                cmd = New OdbcCommand(" UPDATE tbl_AktivaLainnya SET " &
                                      " COA_Debet           = '" & COADebet & "', " &
                                      " Nomor_JV            = '" & NomorJV & "' " &
                                      " WHERE Nomor_BPAL    = '" & NomorBP & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
            End If

        Next 'Akhir Loop ==========================================================================================================

        If Peruntukan = Peruntukan_InvestasiModal Then

            AksesDatabase_General(Buka)

            If FungsiForm = FungsiForm_TAMBAH Then
                Dim NomorIDModal = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_Modal") + 1
                NomorBP = AwalanBPM_PlusTahunBuku & NomorIDModal
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_Modal VALUES ( " &
                                            " '" & NomorIDModal & "', " &
                                            " '" & NomorBP & "', " &
                                            " '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                            " '" & KodeLawanTransaksi & "', " &
                                            " '" & NamaLawanTransaksi & "', " &
                                            " '" & JumlahLembarSaham & "', " &
                                            " '" & HargaPerLembarSaham & "', " &
                                            " '" & COADebet & "', " &
                                            " '" & 0 & "', " &
                                            " '" & JumlahSaham & "', " &
                                            " '" & Catatan & "', " &
                                            " '" & NomorJV & "', " &
                                            " '" & UserAktif & "' ) ",
                                            KoneksiDatabaseGeneral)
            End If

            If FungsiForm = FungsiForm_EDIT Then
                cmdSIMPAN = New OdbcCommand(" UPDATE tbl_Modal SET " &
                                            " Tanggal_Transaksi     = '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                            " Kode_Pemegang_Saham   = '" & KodeLawanTransaksi & "', " &
                                            " Nama_Pemegang_Saham   = '" & NamaLawanTransaksi & "', " &
                                            " Jumlah_Lembar         = '" & JumlahLembarSaham & "', " &
                                            " Harga_Per_Lembar      = '" & HargaPerLembarSaham & "', " &
                                            " COA                   = '" & COADebet & "', " &
                                            " Jumlah_Kredit         = '" & JumlahSaham & "', " &
                                            " Keterangan            = '" & Catatan & "', " &
                                            " Nomor_JV              = '" & NomorJV & "', " &
                                            " User                  = '" & UserAktif & "' " &
                                            " WHERE Nomor_BPM       = '" & NomorBP & "' ",
                                            KoneksiDatabaseGeneral)
            End If

            cmdSIMPAN_ExecuteNonQuery()

            AksesDatabase_General(Tutup)

            JumlahCairPerBaris = JumlahSaham
            JumlahCair_Tabel = JumlahSaham
            SimpanDataPerBaris()

            NamaProduk_Bundel = teks_Saham

        End If

        If PembayaranTerjadwal Then
            '(Ini sudah benar posisinya di luar Loop-ForEachNext. Jangan dimasukkan)
            '(Ini fungsinya untuk menyimpan data Denda pada baris terakhir bundelan angsuran)
            cmd = New OdbcCommand(" UPDATE tbl_BuktiPenerimaan SET " &
                                  " Denda           = '" & Denda & "' " &
                                  " WHERE Nomor_ID  = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            cmd = New OdbcCommand(" UPDATE " & TabelJadwalAngsuran & " SET " &
                                  " Denda                                   = '" & Denda & "' " &
                                  " WHERE " & KolomBPJadwalAngsuran & "    = '" & NomorBP & "' " &
                                  " AND Angsuran_Ke                         = '" & AngsuranKe & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If InvoiceTunai Then
            SimpanJurnalnvoiceTunai()
        Else
            SimpanJurnal()
        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
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

    Sub SimpanDataPerBaris()

        Dim JumlahTagihanPerBaris_Simpan As String
        Dim PokokPerBaris_Simpan As String
        Dim BagiHasilPerBaris_Simpan As String
        Dim JumlahCairPerBaris_Simpan As String
        Dim BiayaAdministrasiBank_Simpan As String
        If MataUang_IDR Then
            JumlahTagihanPerBaris_Simpan = JumlahTagihanPerBaris
            PokokPerBaris_Simpan = PokokPerBaris
            BagiHasilPerBaris_Simpan = BagiHasilPerBaris
            JumlahCairPerBaris_Simpan = JumlahCairPerBaris
            BiayaAdministrasiBank_Simpan = BiayaAdministrasiBank
        Else
            JumlahTagihanPerBaris_Simpan = DesimalFormatSimpan(JumlahTagihanPerBaris)
            PokokPerBaris_Simpan = DesimalFormatSimpan(PokokPerBaris)
            BagiHasilPerBaris_Simpan = DesimalFormatSimpan(BagiHasilPerBaris)
            JumlahCairPerBaris_Simpan = DesimalFormatSimpan(JumlahCairPerBaris)
            BiayaAdministrasiBank_Simpan = DesimalFormatSimpan(BiayaAdministrasiBank)
        End If

        NomorID += 1
        If TanggalInvoicePerBaris = Kosongan Then TanggalInvoicePerBaris = TanggalKosong
        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_BuktiPenerimaan VALUES ( " &
                                        " '" & NomorID & "', " &
                                        " '" & AngkaKM & "', " &
                                        " '" & NomorKM & "', " &
                                        " '" & TanggalFormatSimpan(TanggalKM) & "', " &
                                        " '" & Kategori & "', " &
                                        " '" & Peruntukan & "', " &
                                        " '" & KodeLawanTransaksi & "', " &
                                        " '" & NamaLawanTransaksi & "', " &
                                        " '" & NomorBP & "', " &
                                        " '" & NomorFakturPajak & "', " &
                                        " '" & NomorInvoicePerBaris & "', " &
                                        " '" & TanggalFormatSimpan(TanggalInvoicePerBaris) & "', " &
                                        " '" & UraianInvoicePerBaris & "', " &
                                        " '" & KodeMataUang & "', " &
                                        " '" & DesimalFormatSimpan(Kurs) & "', " &
                                        " '" & JumlahTagihanPerBaris_Simpan & "', " &
                                        " '" & AngsuranKe & "', " &
                                        " '" & PokokPerBaris_Simpan & "', " &
                                        " '" & BagiHasilPerBaris_Simpan & "', " &
                                        " '" & JumlahCairPerBaris_Simpan & "', " &
                                        " '" & 0 & "', " &
                                        " '" & COADebet & "', " &
                                        " '" & COAKredit & "', " &
                                        " '" & BiayaAdministrasiBank_Simpan & "', " &
                                        " '" & DitanggungOleh & "', " &
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

    End Sub

    Sub SimpanJurnal()

        'Penyimpanan Jurnal :
        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalKM)
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
                jur_JenisJurnal = KonversiSaranaPembayaranKeJenisJurnal(SaranaPencairan)
        End Select
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = TanggalInvoice_Bundel
        jur_NomorInvoice = NomorInvoice_Bundel
        jur_NamaProduk = NamaProduk_Bundel
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_KodeMataUang = KodeMataUang
        jur_Kurs = Kurs
        jur_UraianTransaksi = Catatan
        jur_Direct = 0

        Dim JumlahDebet, JumlahKredit As Decimal
        Dim JumlahPembebanan As Decimal = 0
        If PembayaranTerjadwal Then
            JumlahDebet = Pokok + BagiHasil + Denda - PPhDipotong_Total '(???????)
            JumlahKredit = Pokok
        Else
            JumlahDebet = JumlahCair_Total
            JumlahKredit = JumlahCair_Tabel + PPhDipotong_Total
            Select Case Pembebanan
                Case Pembebanan_Diganti
                    JumlahPembebanan = BiayaAdministrasiBank
                Case Pembebanan_Ditambahkan
                    JumlahKredit += BiayaAdministrasiBank
            End Select
        End If

        'Simpan Jurnal :

        '(Ini sementara ya..! Nanti harus diperbaiki..!!!)

        Dim COAPPhPasal21 = Kosongan
        Dim COAPPhPasal23 = Kosongan
        Dim COAPPhPasal26 = Kosongan
        Dim COAPPhPasal42 = Kosongan
        Dim PencairanPiutangJualAsset As Boolean = False
        If PencairanPiutangJualAsset = True Then
            PPhDipotong_Total = 0
            PPhDitanggung_Total = 0
            COAPPhPasal21 = KodeTautanCOA_BiayaPPhPasal21
            COAPPhPasal23 = KodeTautanCOA_BiayaPPhPasal23
            COAPPhPasal26 = KodeTautanCOA_BiayaPPhPasal26
            COAPPhPasal42 = KodeTautanCOA_BiayaPPhPasal24
        Else
            'JumlahPPhTerutang = 0
            COAPPhPasal23 = KodeTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima '(PPh Dibayar Dimuka / Prepaid)
            COAPPhPasal42 = KodeTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima '(PPh Dibayar Dimuka / Prepaid)
        End If

        PesanUntukProgrammer("Jumlah Debet : " & JumlahDebet & Enter2Baris &
                             "Jumlah Kredit : " & JumlahKredit)

        ___jurDebetBankCashIN(DitanggungOleh, COADebet, JumlahDebet, JumlahTransfer, BiayaAdministrasiBank)
        jur_Kurs = 1
        jur_KodeMataUang = KodeMataUang_IDR
        ___jurDebet(COAPPhPasal21, PPhTerutang_Pasal21)
        ___jurDebet(COAPPhPasal23, PPhTerutang_Pasal23)
        ___jurDebet(COAPPhPasal26, PPhTerutang_Pasal26)
        ___jurDebet(COAPPhPasal42, PPhTerutang_Pasal42)
        ___jurDebet(KodeTautanCOA_BiayaAdministrasiPerjanjian, BiayaAdministrasiKontrak)
        ___jurDebet(KodeTautanCOA_BiayaSertifikasiDanLegalitas, BiayaNotarisKontrak)
        ___jurDebet(KodeTautanCOA_BiayaPPhPasal21, BiayaPPhKontrak)
        jur_Kurs = Kurs
        jur_KodeMataUang = KodeMataUang
        _______jurKredit(KodeTautanCOA_PenghasilanBungaDendaPinjaman, (BagiHasil + Denda))
        '_______jurKredit(COAHutangPPh, JumlahPPhTerutang) 'Ini nanti diaktifkan kalau sudah ada algoritma penjualan Asset
        jur_Kurs = 1
        jur_KodeMataUang = KodeMataUang_IDR
        _______jurKredit(KodeTautanCOA_HutangPPhPasal21, BiayaPPhKontrak)
        _______jurKredit(KodeTautanCOA_PenghasilanLainnya, PPhDitanggung_Total)
        jur_Kurs = Kurs
        jur_KodeMataUang = KodeMataUang
        _______jurKredit(COAKredit, JumlahKredit)
        _______jurKredit(COAKredit, JumlahPembebanan)
        KoreksiSelisihJurnal(jur_NomorJV) 'Ini harus disimpan langsung di ujung penyimpanan Jurnal, tidak boleh diseling oleh baris kode yang lain

    End Sub



    Sub SimpanJurnalnvoiceTunai()

        Dim NomorPenjualan = Kosongan
        Dim JenisProduk_Induk = Kosongan
        Dim TotalHarga As Int64
        Dim MetodePembayaran = Kosongan
        Dim TahapTermin = Kosongan
        Dim TerminPersen As Decimal = 0
        Dim DPPBarangTermin As Int64
        Dim DPPJasaTermin As Int64
        Dim DPPTermin = 0
        Dim DebetPelunasan = 0
        Dim DPPBarang As Int64
        Dim DPPJasa As Int64
        Dim DPP As Int64 = 0
        Dim TarifPPN As Decimal
        Dim PPN As Int64
        Dim JenisPPN = Kosongan
        Dim PerlakuanPPN = Kosongan
        Dim JualAsset As Boolean
        Dim Asset As Integer = 0
        Dim KelompokHarta = Kosongan
        Dim BiayaTransportasiPenjualan As Int64

        Dim COAPenjualanBarangAtauAsset = Kosongan
        Dim COAJasa = Kosongan

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorInvoicePerBaris & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        DPPBarang = 0
        DPPJasa = 0
        BiayaTransportasiPenjualan = 0
        Do While dr.Read()
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            TotalHarga = dr.Item("Jumlah_Harga_Keseluruhan") - dr.Item("Diskon")
            MetodePembayaran = dr.Item("Metode_Pembayaran")
            TahapTermin = dr.Item("Tahap_Termin")
            TerminPersen = FormatUlangDesimal_Prosentase(dr.Item("Termin"))
            If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
                DPPBarang += dr.Item("Total_Harga_Per_Item")
            Else
                DPPJasa += dr.Item("Total_Harga_Per_Item")
            End If
            DPP = DPPBarang + DPPJasa
            DPPBarangTermin = dr.Item("DPP_Barang")
            DPPJasaTermin = dr.Item("DPP_Jasa")
            DPPTermin = dr.Item("Dasar_Pengenaan_Pajak")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            TarifPPN = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPN"))
            PPN = dr.Item("PPN")
            If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0
            BiayaTransportasiPenjualan = dr.Item("Biaya_Transportasi")
            Asset = dr.Item("Asset")
        Loop
        AksesDatabase_Transaksi(Tutup)

        If COATermasukBank(COADebet) Then
            PembayaranViaBank = True
            Perhitungan_ValueBank()
        Else
            PembayaranViaBank = False
        End If

        If Asset = 1 Then JualAsset = True
        If Asset = 0 Then JualAsset = False

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset WHERE Kode_Closing = '" & NomorPenjualan & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            KelompokHarta = KonversiAngkaKeKelompokHarta(dr.Item("Kelompok_Harta"))
        End If
        AksesDatabase_General(Tutup)

        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoicePerBaris)
        If JualAsset = False Then jur_JenisJurnal = JenisJurnal_Penjualan
        If JualAsset = True Then jur_JenisJurnal = JenisJurnal_Asset
        jur_TanggalInvoice = TanggalInvoicePerBaris  'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
        jur_NomorInvoice = NomorInvoicePerBaris
        jur_NamaProduk = NamaProduk_Bundel
        jur_NomorFakturPajak = NomorFakturPajak
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_UraianTransaksi = Catatan
        jur_Direct = 0

        Dim PPhTerutangAsset As Int64 = 0

        If JualAsset Then
            If KelompokHarta = KelompokHarta_Tanah Then
                COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetTanahBangunan
                PPhTerutangAsset = PPhTerutang
            Else
                COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetLainnya
            End If
        Else
            COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanBarang_Manufaktur
            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                COAJasa = KodeTautanCOA_PenjualanJasaKonstruksi
            Else
                COAJasa = KodeTautanCOA_PenjualanJasa
            End If
        End If

        If JenisPPN = JenisPPN_Include Then
            Dim RasioDPPBarang As Decimal = DPPBarang / TotalHarga
            DPP = HitungDPPUntukPPNInclude(TotalHarga, TarifPPN)
            DPPBarang = DPP * RasioDPPBarang
            DPPJasa = DPP - DPPBarang
        End If

        If MetodePembayaran = MetodePembayaran_Termin Then
            If TahapTermin = TahapTermin_Pelunasan Then
                DebetPelunasan = DPP - DPPTermin '(Ini belum dicoba untuk PPN Include!)
                DPPTermin = 0
            Else
                DebetPelunasan = 0
                DPPBarang = 0
                DPPJasa = 0
            End If
        Else
            DPPTermin = 0
        End If

        'Simpan Jurnal :
        ___jurDebetBankCashIN(DitanggungOleh, COADebet, JumlahTagihan, JumlahTransfer, BiayaAdministrasiBank)
        ___jurDebet(KodeTautanCOA_UangMukaPenjualan, DebetPelunasan)
        '___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang)   '(Ini sengaja dibiarkan dulu dan tidak dihapus, karena masih bingung. Khawatir nanti dibutuhkan).
        ___jurDebet(PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(KonversiJenisPajakKeJenisPPh(JenisPajakPerBaris)), PPhTerutang)
        _______jurKredit(PenentuanCOA_HutangPajak(KonversiJenisPajakKeJenisPPh(JenisPajakPerBaris), KodeSetoran), PPhTerutangAsset)
        _______jurKredit(KodeTautanCOA_PenghasilanLainnya, PPhDitanggung)
        _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN)
        _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang)
        _______jurKredit(COAJasa, DPPJasa)
        _______jurKredit(KodeTautanCOA_UangMukaPenjualan, DPPTermin)
        _______jurKredit(KodeTautanCOA_PenjualanLainnya, BiayaTransportasiPenjualan)

    End Sub

    Sub HapusDataPengajuanLama()
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPenerimaan " &
                                   " WHERE Angka_KM = '" & AngkaKM & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
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
        cmb_HargaPerLembarSaham.IsReadOnly = True
        cmb_SaranaPencairan.IsReadOnly = True
        cmb_DitanggungOleh.IsReadOnly = True
        txt_JumlahTransfer.IsReadOnly = True
        txt_TotalBank.IsReadOnly = True
        cmb_SaranaPencairan.IsEnabled = True
        grb_Bank.IsEnabled = True
        lbl_TotalBank.Visibility = Visibility.Collapsed '(Di sisi PENERIMAAN tidak dibutuhkan info Total Bank)
        txt_TotalBank.Visibility = Visibility.Collapsed '(Di sisi PENERIMAAN tidak dibutuhkan info Total Bank)
        txt_SaldoAwalHutang.IsReadOnly = True
        VisibilitasModal(False)
        VisibilitasSaldoAwalHutang(False)
        VisibilitasBiayaKontrakBankLeasing(False)
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
    Dim Jumlah_Cair_Per_Baris As New DataGridTextColumn
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
        datatabelUtama.Columns.Add("Jumlah_Cair_Per_Baris", GetType(Int64))
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sudah_Dibayar_Per_Baris, "Sudah_Dibayar_Per_Baris", "Sudah Dibayar", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan_Per_Baris, "Sisa_Tagihan_Per_Baris", "Sisa Tagihan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Pajak_Per_Baris, "Jenis_Pajak_Per_Baris", "Jenis Pajak", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Setoran_Per_Baris, "Kode_Setoran_Per_Baris", "Kode Setoran", 51, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris, "PPh_Terutang_Per_Baris", "PPh Terutang", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris, "PPh_Ditanggung_Per_Baris", "PPh Ditanggung", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris, "PPh_Dipotong_Per_Baris", "PPh Dipotong", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Cair_Per_Baris, "Jumlah_Cair_Per_Baris", "Jumlah Cair", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
        datatabelUtama.Columns.Add("Jumlah_Cair_Per_Baris", GetType(Decimal))
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sudah_Dibayar_Per_Baris, "Sudah_Dibayar_Per_Baris", "Sudah Dibayar", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan_Per_Baris, "Sisa_Tagihan_Per_Baris", "Sisa Tagihan", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Pajak_Per_Baris, "Jenis_Pajak_Per_Baris", "Jenis Pajak", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Setoran_Per_Baris, "Kode_Setoran_Per_Baris", "Kode Setoran", 51, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris, "PPh_Terutang_Per_Baris", "PPh Terutang", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris, "PPh_Ditanggung_Per_Baris", "PPh Ditanggung", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris, "PPh_Dipotong_Per_Baris", "PPh Dipotong", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Cair_Per_Baris, "Jumlah_Cair_Per_Baris", "Jumlah Cair", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang_Per_Baris_Value_Awal_dB, "PPh_Terutang_Per_Baris_Value_Awal_dB", "PPh Terutang (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Per_Baris_Value_Awal_dB, "PPh_Ditanggung_Per_Baris_Value_Awal_dB", "PPh Ditanggung (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong_Per_Baris_Value_Awal_dB, "PPh_Dipotong_Per_Baris_Value_Awal_dB", "PPh Dipotong (dB)", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
