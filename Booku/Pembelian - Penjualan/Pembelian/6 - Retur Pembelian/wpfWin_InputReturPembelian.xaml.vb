Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports Org.BouncyCastle.Math.EC
Imports DocumentFormat.OpenXml.Drawing

Public Class wpfWin_InputReturPembelian

    Public JudulForm
    Public FungsiForm
    Public NomorID

    Public NomorJV

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    'Variabel Kolom :
    Public AngkaRetur
    Public NomorRetur
    Dim NomorRetur_Lama
    Dim TanggalRetur
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim KodeProjectProduk
    Dim KodeSupplier
    Dim NamaSupplier
    Dim Catatan
    Dim JumlahNota As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64
    Dim DPP As Int64
    Dim PPN As Int64
    Dim TotalRetur As Int64

    Dim DPP_11Per12 As Int64
    Dim TarifPPN_11Per12


    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim JenisProdukPerItem
    Dim COAProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem
    Dim SatuanProduk
    Dim HargaSatuan
    Dim DiskonPerItem_Persen As Decimal
    Dim DiskonPerItem_Rp
    Dim TotalHarga

    'Variabel Tabel Index :
    Dim NomorUrutProduk_Terseleksi
    Dim JenisProdukPerItem_Terseleksi
    Dim COAProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahBaris_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi

    'Variabel Tabel Invoice - Index :
    Dim BarisInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi

    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Public TarifPPN As Decimal
    Dim TarifPPN_TerakhirDitambahkan As Decimal

    Dim Koreksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Retur Pembelian"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Retur Pembelian"
            Perhitungan()
            JumlahBarisInvoice = datatabelInvoice.Rows.Count
            NomorRetur_Lama = NomorRetur
        End If

        btn_TambahInvoice.IsEnabled = False
        BersihkanSeleksi_TabelInvoice()
        BersihkanSeleksi_TabelProduk()

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorJV = 0

        JenisProduk_Induk = Kosongan
        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan

        NomorID = 0
        AngkaRetur = 0
        txt_NomorRetur.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalRetur)
        txt_KodeSupplier.Text = Kosongan
        txt_NamaSupplier.Text = Kosongan
        btn_SingkirkanInvoice.IsEnabled = False
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        VisibilitasComboJenisPPN(False)
        VisibilitasComboPerlakuanPPN(False)
        KosongkanValueElemenRichTextBox(txt_Catatan)
        txt_JumlahNota.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_TarifPPN.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalRetur.Text = Kosongan
        Kosongkan_TabelProduk()

        TarifPPN = 0
        TarifPPN_TerakhirDitambahkan = 0

        Koreksi = Kosongan

        ProsesResetForm = False

    End Sub



    Sub SistemPenomoranOtomatis_ReturPembelian()
        If dtp_TanggalRetur.Text <> Kosongan Then
            If FungsiForm = FungsiForm_TAMBAH Then AngkaRetur = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Pembelian_Retur", "Angka_Retur") + 1 'Logika FungsiFofm Jangan dihapus...!!!
            NomorRetur = AwalanNR & AngkaRetur.ToString & "-" & BulanRomawi(dtp_TanggalRetur.SelectedDate.Value.Month) & "-" & dtp_TanggalRetur.SelectedDate.Value.Year
            txt_NomorRetur.Text = NomorRetur
        End If
    End Sub




    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        cmb_JenisPPN.Items.Add(JenisPPN_NonPPN)
        cmb_JenisPPN.Items.Add(JenisPPN_Exclude)
        cmb_JenisPPN.Items.Add(JenisPPN_Include)
        cmb_JenisPPN.Text = Kosongan
    End Sub

    Sub LogikaTampilanPPN()
        LogikaPPN(TanggalInvoice, DPP, TarifPPN, DPP_11Per12, TarifPPN_11Per12)
        If TarifPPN_11Per12 > 0 Then
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Visible
            txt_TarifPPN_11Per12.Visibility = Visibility.Visible
            lbl_DPP.Text = "Dasar Pengenaan Pajak"
            txt_DasarPengenaanPajak_11Per12.Text = DPP_11Per12
            txt_TarifPPN_11Per12.Text = TarifPPN_11Per12
        Else
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak_11Per12.Visibility = Visibility.Collapsed
            txt_TarifPPN_11Per12.Visibility = Visibility.Collapsed
            lbl_DPP.Text = "Dasar Pengenaan Pajak"
            txt_DasarPengenaanPajak_11Per12.Text = 0
            txt_TarifPPN_11Per12.Text = 0
        End If
    End Sub


    Sub KontenCombo_PerlakuanPPN_Kosongan()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
        VisibilitasComboPerlakuanPPN(False)
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)
        VisibilitasComboPerlakuanPPN(True)
    End Sub


    Sub VisibilitasComboJenisPPN(Visibilitas As Boolean)
        lbl_JenisPPN.Visibility = Visibility.Collapsed
        cmb_JenisPPN.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_JenisPPN.Visibility = Visibility.Visible
            cmb_JenisPPN.Visibility = Visibility.Visible
        Else
            lbl_JenisPPN.Visibility = Visibility.Collapsed
            cmb_JenisPPN.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasComboPerlakuanPPN(Visibilitas As Boolean)
        lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
        cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_PerlakuanPPN.Visibility = Visibility.Visible
            cmb_PerlakuanPPN.Visibility = Visibility.Visible
        Else
            lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
            cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomKolomPPN(Visibilitas As Boolean)
        lbl_PPN.Visibility = Visibility.Collapsed
        txt_TarifPPN.Visibility = Visibility.Collapsed
        lbl_PersenPPN.Visibility = Visibility.Collapsed
        txt_PPN.Visibility = Visibility.Collapsed
        lbl_DPP.Visibility = Visibility.Collapsed
        txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_PPN.Visibility = Visibility.Visible
            txt_TarifPPN.Visibility = Visibility.Visible
            lbl_PersenPPN.Visibility = Visibility.Visible
            txt_PPN.Visibility = Visibility.Visible
            lbl_DPP.Visibility = Visibility.Visible
            txt_DasarPengenaanPajak.Visibility = Visibility.Visible
            LogikaTampilanPPN()
        Else
            lbl_PPN.Visibility = Visibility.Collapsed
            txt_TarifPPN.Visibility = Visibility.Collapsed
            lbl_PersenPPN.Visibility = Visibility.Collapsed
            txt_PPN.Visibility = Visibility.Collapsed
            lbl_DPP.Visibility = Visibility.Collapsed
            txt_DasarPengenaanPajak.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasDiskon(Visibilitas As Boolean)
        lbl_Diskon.Visibility = Visibility.Collapsed
        txt_Diskon_Persen.Visibility = Visibility.Collapsed
        lbl_PersenDiskon.Visibility = Visibility.Collapsed
        txt_Diskon_Rp.Visibility = Visibility.Collapsed
        If Visibilitas Then
            lbl_Diskon.Visibility = Visibility.Visible
            txt_Diskon_Persen.Visibility = Visibility.Visible
            lbl_PersenDiskon.Visibility = Visibility.Visible
            txt_Diskon_Rp.Visibility = Visibility.Visible
        Else
            lbl_Diskon.Visibility = Visibility.Collapsed
            txt_Diskon_Persen.Visibility = Visibility.Collapsed
            lbl_PersenDiskon.Visibility = Visibility.Collapsed
            txt_Diskon_Rp.Visibility = Visibility.Collapsed
        End If
    End Sub



    Sub Kosongkan_TabelProduk()
        datatabelUtama.Rows.Clear()
        JumlahBaris = 0
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then Perhitungan()
    End Sub
    Sub Kosongkan_TabelInvoice()
        datatabelInvoice.Rows.Clear()
        JumlahBarisInvoice = 0
        Kosongkan_TabelProduk()
        VisibilitasComboJenisPPN(False)
        VisibilitasComboPerlakuanPPN(False)
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        BarisTerseleksi = -1
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
    End Sub

    Sub BersihkanSeleksi_TabelInvoice()
        BersihkanSeleksi_WPF(datagridInvoice, datatabelInvoice, BarisInvoice_Terseleksi, JumlahBarisInvoice)
        BarisInvoice_Terseleksi = -1
        NomorInvoice_Terseleksi = Kosongan
        btn_SingkirkanInvoice.IsEnabled = False
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_TabelProduk()
    End Sub

    Sub KondisiFormSetelahPerubahan()
        BersihkanSeleksi()
    End Sub

    Sub Perhitungan()

        JumlahBaris = datatabelUtama.Rows.Count
        JumlahNota = 0
        Diskon_Rp = 0
        HitunganHarga_Relatif = 0

        For Each row As DataRow In datatabelUtama.Rows
            HitunganHarga_Relatif += AmbilAngka(row("Total_Harga"))
            JumlahNota += AmbilAngka(row("Jumlah_Harga_Per_Item"))
            Diskon_Rp += AmbilAngka(row("Diskon_Per_Item_Rp"))
        Next

        Dim RasioPPNInclude = 100 / (100 + TarifPPN)

        If JumlahBaris = 0 Then
            txt_JumlahNota.Text = Kosongan
            txt_Diskon_Persen.Text = Kosongan
            txt_Diskon_Rp.Text = Kosongan
            txt_DasarPengenaanPajak.Text = Kosongan
            txt_PPN.Text = Kosongan
            txt_TotalRetur.Text = Kosongan
        End If

        If JenisPPN = Kosongan Then JenisPPN = JenisPPN_NonPPN

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            DPP = HitunganHarga_Relatif
            txt_JumlahNota.Text = JumlahNota
            txt_Diskon_Rp.Text = Diskon_Rp
            txt_DasarPengenaanPajak.Text = HitunganHarga_Relatif
        End If

        Select Case JenisPPN
            Case JenisPPN_NonPPN
                PPN = 0
                TotalRetur = DPP
            Case JenisPPN_Exclude
                PPN = DPP * Persen(TarifPPN)
                If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalRetur = DPP + PPN
                If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalRetur = DPP
                If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalRetur = DPP
            Case JenisPPN_Include
                If HitunganHarga_Relatif = 0 Then
                    Diskon_Rp = 0
                    DPP = 0
                    PPN = 0
                    TotalRetur = 0
                Else
                    TotalRetur = HitunganHarga_Relatif
                    PPN = TotalRetur - (TotalRetur * RasioPPNInclude)
                    DPP = TotalRetur - PPN
                    JumlahNota = DPP + Diskon_Rp
                    If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalRetur = DPP + PPN
                    If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalRetur = DPP
                    If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalRetur = DPP
                End If
                '---------------------------------------------------------
                txt_Diskon_Rp.Text = Diskon_Rp
                txt_JumlahNota.Text = JumlahNota
        End Select

        txt_DasarPengenaanPajak.Text = DPP
        txt_PPN.Text = PPN
        txt_TotalRetur.Text = TotalRetur

        If PPN > 0 Then
            VisibilitasKolomKolomPPN(True)
        Else
            VisibilitasKolomKolomPPN(False)
        End If

        If Diskon_Rp > 0 Then
            VisibilitasDiskon(True)
        Else
            VisibilitasDiskon(False)
        End If



    End Sub





    Private Sub txt_NomorRetur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorRetur.TextChanged
        NomorRetur = txt_NomorRetur.Text
        '(Ini jangan dihapus. Ini penting untuk dieksekusi saat edit.)
    End Sub


    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalRetur.SelectedDateChanged
        If dtp_TanggalRetur.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalRetur)
            TanggalRetur = dtp_TanggalRetur.SelectedDate
            If ProsesIsiValueForm = False And ProsesResetForm = False Then SistemPenomoranOtomatis_ReturPembelian()
            KondisiFormSetelahPerubahan()
        End If
    End Sub


    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        txt_NamaSupplier.Text = AmbilValue_NamaMitra(KodeSupplier)
        txt_PICSupplier.Text = AmbilValue_PICMitra(KodeSupplier)
        txt_AlamatSupplier.Text = AmbilValue_AlamatMitra(KodeSupplier)
        If KodeSupplier = Kosongan Then
            btn_TambahInvoice.IsEnabled = False
        Else
            btn_TambahInvoice.IsEnabled = True
            cmb_JenisPPN.SelectedValue = Kosongan
            cmb_PerlakuanPPN.SelectedValue = Kosongan
        End If
        Kosongkan_TabelInvoice()
    End Sub
    Private Sub btn_PilihSupplier_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_PICSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PICSupplier.TextChanged
    End Sub
    Private Sub txt_AlamatSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AlamatSupplier.TextChanged
    End Sub


    Private Sub btn_TambahInvoice_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahInvoice.Click

        win_ListInvoice = New wpfWin_ListInvoice()
        win_ListInvoice.ResetForm()
        win_ListInvoice.FungsiForm = win_ListInvoice.FungsiForm_InvoicePembelian
        win_ListInvoice.NamaMitra_Filter = NamaSupplier
        win_ListInvoice.FilterMitra_Aktif = False
        If JumlahBarisInvoice > 0 Then
            win_ListInvoice.JenisPPN = JenisPPN
            win_ListInvoice.PerlakuanPPN = PerlakuanPPN
        End If
        win_ListInvoice.JalurMasuk = Form_INPUTINVOICEPEMBELIAN
        win_ListInvoice.PilihYangSudahDijurnal = True
        win_ListInvoice.ShowDialog()                                             '<---- Buka Form List Invoice
        NomorInvoice = win_ListInvoice.NomorInvoice_Terseleksi
        If NomorInvoice = Kosongan Then Return
        TanggalInvoice = win_ListInvoice.TanggalInvoice_Terseleksi
        KodeProjectProduk = win_ListInvoice.KodeProject_Terseleksi
        Dim TelusurInvoice = Kosongan
        For Each row As DataRow In datatabelInvoice.Rows  '(Cegah Input Invoice lebih dari satu kali)
            TelusurInvoice = row("Nomor_Invoice")
            If TelusurInvoice = NomorInvoice Then
                Pesan_Peringatan("Nomor Invoice ini sudah ditambahkan!")
                Return
            End If
        Next
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = datatabelUtama.Rows.Count
        Dim PenambahanProdukPadaSesiIni = 0
        Do While dr.Read
            NomorUrutProduk += 1
            COAProduk = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk_PerItem = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            HargaSatuan = dr.Item("Harga_Satuan")
            Dim JumlahHarga_PerItem As Int64 = JumlahProduk_PerItem * HargaSatuan
            DiskonPerItem_Persen = FormatUlangDesimal_Prosentase(dr.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHarga_PerItem * (DiskonPerItem_Persen / 100)
            TarifPPN = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPN"))
            If TarifPPN <> TarifPPN_TerakhirDitambahkan And JumlahBarisInvoice > 0 Then
                PesanPeringatan("Invoice ditolak..!" & Enter2Baris & "Tarif PPN pada Invoice yang Anda pilih tidak sama dengan Invoice sebelumnya.")
                Return
            End If
            TarifPPN_TerakhirDitambahkan = TarifPPN
            txt_TarifPPN.Text = FormatUlangDesimal_Prosentase(TarifPPN)
            Dim TotalHarga_PerItem As Int64 = JumlahHarga_PerItem - DiskonPerItem_Rp
            If JenisProdukPerItem = JenisProduk_Barang Then
                Dim SJ_Telusur = dr.Item("Nomor_SJ_BAST_Produk")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                                         " WHERE Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim NomorPO_Telusur = Kosongan
                If drTELUSUR.HasRows Then NomorPO_Telusur = drTELUSUR.Item("Nomor_PO_Produk")
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                          " WHERE Nomor_PO = '" & NomorPO_Telusur & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then KodeProjectProduk = drTELUSUR2.Item("Kode_Project_Produk")
                datatabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorInvoice, TanggalInvoice,
                                        COAProduk, NamaProduk, DeskripsiProduk,
                                        JumlahProduk_PerItem, SatuanProduk, HargaSatuan, JumlahHarga_PerItem,
                                        DiskonPerItem_Persen, DiskonPerItem_Rp, TotalHarga_PerItem, KodeProjectProduk)              '<-- Penambahan Baris Produk
                PenambahanProdukPadaSesiIni += 1
            End If
        Loop
        If PenambahanProdukPadaSesiIni = 0 Then 'Jika tidak ada penambahan produk (akibat penyortiran), maka hapus baris terakhir pada Tabel Invoice.
            PesanPemberitahuan("Tidak ada barang yang bisa ditambahkan pada Invoice ini.")
            If datatabelInvoice.Rows.Count > 0 Then
                'dgv_Invoice.Rows.RemoveAt(dgv_Invoice.RowCount - 1)
                'BersihkanSeleksi_TabelProduk()
                Pesan_Informasi("Tidak ada produk yang bisa ditambahkan pada Invoice ini.")
            End If
        Else
            datatabelInvoice.Rows.Add(NomorInvoice, TanggalInvoice, KodeProjectProduk)        '<-- Penambahan Baris Invoice
            JumlahBarisInvoice = datatabelInvoice.Rows.Count
            BersihkanSeleksi_TabelInvoice()
            cmd = New OdbcCommand(" Select * FROM tbl_Pembelian_Invoice " &
                                  " WHERE Nomor_Invoice = '" & NomorInvoice & "' ",
                                  KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                JenisPPN = dr.Item("Jenis_PPN")
                PerlakuanPPN = dr.Item("Perlakuan_PPN")
            Else
                pesan_AdaKesalahanTeknis_Database(Kosongan)
                Return
            End If
            VisibilitasComboJenisPPN(True)
            IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN)
            IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, PerlakuanPPN)
            Perhitungan()
        End If
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
        AksesDatabase_Transaksi(Tutup)
        LogikaTombolTambahInvoice()
    End Sub

    Sub LogikaTombolTambahInvoice()
        If JumlahBarisInvoice >= 1 Then
            btn_TambahInvoice.IsEnabled = False
            'Logika ini dipakai, karena untuk saat ini 1 Retur hanya mampu menampung 1 Invoice saja.
            'Ke depannya nanti bisa dikembangkan bahwa 1 Retur bisa menampung lebih dari 1 Invoice.
        End If
    End Sub

    Private Sub btn_SingkirkanInvoice_Click(sender As Object, e As RoutedEventArgs) Handles btn_SingkirkanInvoice.Click
        If JumlahBarisInvoice = 0 Then Return
        Dim NomorInvoice_UntukDihapus = rowviewInvoice("Nomor_Invoice")
        rowviewInvoice.Delete()
        BersihkanSeleksi_TabelInvoice()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = datatabelUtama.Rows(i)
            If row("Nomor_Invoice_Produk") = NomorInvoice_UntukDihapus Then
                row.Delete()
            End If
        Next
        NomorUrutProduk = 0
        For Each row As DataRow In datatabelUtama.Rows
            NomorUrutProduk += 1
            row("Nomor_Urut") = NomorUrutProduk
        Next
        BersihkanSeleksi_TabelProduk()
        If JumlahBarisInvoice = 0 Then
            JenisPPN = Kosongan
            PerlakuanPPN = Kosongan
            cmb_JenisPPN.SelectedValue = Kosongan
            KontenCombo_PerlakuanPPN_Kosongan()
            VisibilitasComboJenisPPN(False)
            VisibilitasComboPerlakuanPPN(False)
            btn_TambahInvoice.IsEnabled = True
        End If
        Perhitungan()
    End Sub



    Private Sub datagridInvoice_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridInvoice.SelectionChanged
    End Sub
    Private Sub datagridInvoice_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridInvoice.PreviewMouseLeftButtonUp
        HeaderKolomInvoice = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomInvoice IsNot Nothing Then
            BersihkanSeleksi_TabelInvoice()
        End If
    End Sub
    Private Sub datagridInvoice_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridInvoice.SelectedCellsChanged

        KolomTerseleksiInvoice = datagridInvoice.CurrentColumn
        BarisTerseleksiInvoice = datagridInvoice.SelectedIndex
        If BarisTerseleksiInvoice < 0 Then Return
        rowviewInvoice = TryCast(datagridInvoice.SelectedItem, DataRowView)
        If Not rowviewInvoice IsNot Nothing Then Return

        If datatabelInvoice.Rows.Count = 0 Then Return
        NomorInvoice_Terseleksi = rowviewInvoice("Nomor_Invoice")
        If BarisTerseleksiInvoice >= 0 Then
            btn_SingkirkanInvoice.IsEnabled = True
        Else
            btn_SingkirkanInvoice.IsEnabled = False
        End If

    End Sub
    Private Sub datagridInvoice_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridInvoice.MouseDoubleClick
    End Sub



    Private Sub cmb_JenisPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPN.SelectionChanged
        JenisPPN = cmb_JenisPPN.SelectedValue
        If JenisPPN = JenisPPN_NonPPN Or JenisPPN = Kosongan Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        If ProsesResetForm = False And ProsesIsiValueForm = False Then Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub cmb_PerlakuanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PerlakuanPPN.SelectionChanged
        PerlakuanPPN = cmb_PerlakuanPPN.SelectedValue
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then Perhitungan()
    End Sub


    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub


    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi_TabelProduk()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorUrutProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        JenisProdukPerItem_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_Produk_Per_item")
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        DeskripsiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Deskripsi_Produk")
        JumlahBaris_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk"))
        SatuanProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Satuan_Produk")
        HargaSatuan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Satuan"))
        DiskonPerItem_Persen_Terseleksi = GantiTeks(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Per_Item_Rp"))
        TotalHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Harga"))

        If BarisTerseleksi >= 0 Then
            btn_Perbaiki.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        Else
            btn_Perbaiki.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If BarisTerseleksi >= 0 Then
            btn_Perbaiki_Click(sender, e)
        End If
    End Sub


    Private Sub btn_Perbaiki_Click(sender As Object, e As RoutedEventArgs) Handles btn_Perbaiki.Click
        win_InputProduk_Nota = New wpfWin_InputProduk_Nota
        win_InputProduk_Nota.ResetForm()
        win_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        win_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        win_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        win_InputProduk_Nota.JenisProduk_PerItem = JenisProdukPerItem_Terseleksi
        win_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        win_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        win_InputProduk_Nota.txt_JumlahProduk.Text = JumlahBaris_Terseleksi
        win_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        win_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        win_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        win_InputProduk_Nota.JalurMasuk = Form_INPUTRETURPEMBELIAN
        win_InputProduk_Nota.ShowDialog()
        If win_InputProduk_Nota.Proses = False Then Return
        If datatabelUtama.Rows.Count > 0 Then '(Ini untuk mencegah eksekusi kode di bawah, saat jalur masuk melalui form dialog WPF).
            rowviewUtama("Jenis_Produk_Per_Item") = win_InputProduk_Nota.JenisProduk_PerItem
            rowviewUtama("Nama_Produk") = win_InputProduk_Nota.NamaProduk
            rowviewUtama("Deskripsi_Produk") = win_InputProduk_Nota.DeskripsiProduk
            rowviewUtama("Jumlah_Produk") = win_InputProduk_Nota.JumlahProduk
            rowviewUtama("Satuan_Produk") = win_InputProduk_Nota.SatuanProduk
            rowviewUtama("Harga_Satuan") = win_InputProduk_Nota.HargaSatuan
            rowviewUtama("Jumlah_Harga_Per_Item") = win_InputProduk_Nota.JumlahHarga
            rowviewUtama("Diskon_Per_Item_Persen") = (FormatUlangDesimal_Prosentase(win_InputProduk_Nota.DiskonPerItem_Persen) & " %")
            rowviewUtama("Diskon_Per_Item_Rp") = win_InputProduk_Nota.DiskonPerItem_Rp
            rowviewUtama("Total_Harga") = win_InputProduk_Nota.TotalHarga
            rowviewUtama("Kode_Project_Produk") = win_InputProduk_Nota.KodeProject
            If win_InputProduk_Nota.JenisProduk_PerItem = JenisProduk_Jasa Then
                For Each row As DataRow In datatabelUtama.Rows
                    If row("Jenis_Produk_Per_Item") = JenisProduk_Jasa Then row("Nama_Produk") = win_InputProduk_Nota.NamaProduk
                Next
            End If
        End If
        Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub btn_Singkirkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Singkirkan.Click
        If Not TanyaKonfirmasi("Yakin ingin menyingkirkan item terpilih?") Then Return
        rowviewUtama.Delete()
        Dim i = 0
        For Each row As DataRow In datatabelUtama.Rows
            i += 1
            row("Nomor_Urut") = i
        Next
        Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub txt_JumlahNota_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahNota.TextChanged
        JumlahNota = AmbilAngka(txt_JumlahNota.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahNota)
    End Sub
    Private Sub txt_JumlahNota_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahNota.PreviewTextInput
              
    End Sub


    Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_Diskon_Persen, Diskon_Persen)
        KondisiFormSetelahPerubahan()
        Perhitungan()
    End Sub
    Private Sub txt_Diskon_Persen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Diskon_Persen.PreviewTextInput
    End Sub


    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Diskon_Rp.TextChanged
        Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
        PemecahRibuanUntukTextBox_WPF(txt_Diskon_Rp)
    End Sub
    Private Sub txt_Diskon_Rp_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Diskon_Rp.PreviewTextInput
              
    End Sub


    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DasarPengenaanPajak)
    End Sub
    Private Sub txt_DasarPengenaanPajak_11Per12_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DasarPengenaanPajak_11Per12.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_DasarPengenaanPajak_11Per12)
    End Sub


    Private Sub txt_TarifPPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPN.TextChanged
        TextBoxFormatPersen_WPF(txt_TarifPPN, TarifPPN)
        Perhitungan()
    End Sub
    Private Sub txt_TarifPPN_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TarifPPN.PreviewTextInput
    End Sub


    Private Sub txt_PPN_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPN.TextChanged
        PPN = AmbilAngka(txt_PPN.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPN)
    End Sub
    Private Sub txt_PPN_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPN.PreviewTextInput
              
    End Sub


    Private Sub txt_TotalRetur_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalRetur.TextChanged
        TotalRetur = AmbilAngka(txt_TotalRetur.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalRetur)
    End Sub
    Private Sub txt_TotalRetur_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TotalRetur.PreviewTextInput
              
    End Sub





    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalRetur.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalRetur, "Tanggal Retur")
            Return
        End If

        If NomorRetur = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Nomor Retur'.")
            txt_NomorRetur.Focus()
            Return
        End If

        If JumlahBaris = 0 Then
            Pesan_Peringatan("Silakan tambahkan data 'Barang/Jasa'.")
            btn_TambahInvoice.Focus()
            Return
        End If

        If KodeSupplier = Nothing Then
            Pesan_Peringatan("Silakan isi data 'Supplier'.")
            Return
        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            'Hapus Data Retur Pembelian :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_Retur " &
                                       " WHERE Angka_Retur = '" & AngkaRetur & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()

            HapusJurnal_BerdasarkanNomorJV(NomorJV)
            jur_NomorJV = NomorJV

            AksesDatabase_Transaksi(Tutup)

        End If

        Dim NomorInvoiceJurnal = Kosongan
        Dim NomorInvoiceJurnalSebelumnya = Kosongan
        Dim TanggalInvoiceJurnal = Kosongan
        Dim KodeProjectJurnal = Kosongan

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pembelian_Retur")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahBaris
                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = datatabelUtama.Rows(NomorUrutProduk - 1)("Jenis_Produk_Per_Item")
                JenisProdukPerItem = datatabelUtama.Rows(NomorUrutProduk - 1)("Jenis_Produk_Per_Item")
                NomorInvoice = datatabelUtama.Rows(NomorUrutProduk - 1)("Nomor_Invoice_Produk")
                TanggalInvoice = datatabelUtama.Rows(NomorUrutProduk - 1)("Tanggal_Invoice_Produk")
                KodeProjectProduk = datatabelUtama.Rows(NomorUrutProduk - 1)("Kode_Project_Produk")
                NamaProduk = datatabelUtama.Rows(NomorUrutProduk - 1)("Nama_Produk")
                DeskripsiProduk = datatabelUtama.Rows(NomorUrutProduk - 1)("Deskripsi_Produk")
                JumlahProduk_PerItem = AmbilAngka(datatabelUtama.Rows(NomorUrutProduk - 1)("Jumlah_Produk"))
                SatuanProduk = datatabelUtama.Rows(NomorUrutProduk - 1)("Satuan_Produk")
                HargaSatuan = AmbilAngka(datatabelUtama.Rows(NomorUrutProduk - 1)("Harga_Satuan"))
                DiskonPerItem_Persen = GantiTeks(datatabelUtama.Rows(NomorUrutProduk - 1)("Diskon_Per_Item_Persen"), " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHarga = AmbilAngka(datatabelUtama.Rows(NomorUrutProduk - 1)("Total_Harga"))
                If IsDBNull(KodeProjectProduk) Then KodeProjectProduk = Kosongan
                If IsDBNull(DeskripsiProduk) Then DeskripsiProduk = Kosongan
                QueryPenyimpanan = " INSERT INTO tbl_Pembelian_Retur VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaRetur & "', " &
                    " '" & NomorRetur & "', " &
                    " '" & TanggalFormatSimpan(TanggalRetur) & "', " &
                    " '" & KodeSupplier & "', " &
                    " '" & NamaSupplier & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProdukPerItem & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & COAProduk & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHarga & "', " &
                    " '" & JumlahNota & "', " &
                    " '" & Diskon_Rp & "', " &
                    " '" & DPP & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & DesimalFormatSimpan(TarifPPN) & "', " &
                    " '" & PPN & "', " &
                    " '" & TotalRetur & "', " &
                    " '" & Catatan & "', " &
                    " '" & NomorJV & "', " &
                    " '" & UserAktif & "', " &
                    " '" & Koreksi & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit Do
                If NomorInvoice <> NomorInvoiceJurnalSebelumnya Then
                    If NomorInvoiceJurnal = Kosongan Then
                        NomorInvoiceJurnal = NomorInvoice
                        TanggalInvoiceJurnal = TanggalInvoice
                        KodeProjectJurnal = KodeProjectProduk
                    Else
                        NomorInvoiceJurnal &= SlashGanda_Pemisah & NomorInvoice
                        TanggalInvoiceJurnal &= SlashGanda_Pemisah & TanggalInvoice
                        KodeProjectJurnal &= SlashGanda_Pemisah & KodeProjectProduk
                    End If
                End If
                NomorInvoiceJurnalSebelumnya = NomorInvoice
            Loop

            AksesDatabase_Transaksi(Tutup)

        End If

        'Jika ada perubahan Nomor Retur :
        If StatusSuntingDatabase = True And NomorRetur_Lama <> NomorRetur Then
            AksesDatabase_Transaksi(Buka)
            AksesDatabase_Transaksi(Tutup)
            If FungsiForm = FungsiForm_EDIT Then PesanUntukProgrammer("Susuri semua tabel yang mengandung Nomor Retur, dan edit..!!!")
        End If

        'Isi Value Retur pada Masing-masing Invoice :
        If StatusSuntingDatabase = True Then
            NomorUrutProduk = 0
            Dim NomorInvoiceSebelumnya = Kosongan
            Dim ReturDPPPerInvoice As Int64 = 0
            Dim ReturPPNPerInvoice As Int64 = 0
            AksesDatabase_Transaksi(Buka)
            Do While NomorUrutProduk < JumlahBaris
                NomorUrutProduk += 1
                NomorInvoice = datatabelUtama.Rows(NomorUrutProduk - 1)("Nomor_Invoice_Produk")
                If NomorUrutProduk > 1 And NomorInvoice <> NomorInvoiceSebelumnya Then
                    cmd = New OdbcCommand(" UPDATE tbl_Pembelian_Invoice SET " &
                                          " Retur_DPP           = '" & ReturDPPPerInvoice & "', " &
                                          " Retur_PPN           = '" & ReturPPNPerInvoice & "' " &
                                          " WHERE Nomor_Invoice = '" & NomorInvoiceSebelumnya & "' ", KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                    ReturDPPPerInvoice = 0
                    ReturPPNPerInvoice = 0
                End If
                ReturDPPPerInvoice += AmbilAngka(datatabelUtama.Rows(NomorUrutProduk - 1)("Total_Harga"))
                ReturPPNPerInvoice = ReturDPPPerInvoice * Persen(TarifPPN)
                NomorInvoiceSebelumnya = NomorInvoice
            Loop
            cmd = New OdbcCommand(" UPDATE tbl_Pembelian_Invoice SET " &
                                  " Retur_DPP           = '" & ReturDPPPerInvoice & "', " &
                                  " Retur_PPN           = '" & ReturPPNPerInvoice & "' " &
                                  " WHERE Nomor_Invoice = '" & NomorInvoiceSebelumnya & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        '====================================================================================
        'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                          |
        '====================================================================================

        'Isi Value Jumlah Debet COA Retur Pembelian Bahan Baku Lokal :
        Dim JumlahDebet_ReturPembelianBahanBakuLokal = 0
        For Each row As DataRow In datatabelUtama.Rows
            If row("COA_Produk") = KodeTautanCOA_PembelianBahanBaku_Lokal Then
                JumlahDebet_ReturPembelianBahanBakuLokal += AmbilAngka(row("Total_Harga"))
            End If
        Next

        'Isi Value Jumlah Debet COA Retur Pembelian Bahan Baku Import :
        Dim JumlahDebet_ReturPembelianBahanBakuImport = 0
        For Each row As DataRow In datatabelUtama.Rows
            If row("COA_Produk") = KodeTautanCOA_PembelianBahanBaku_Import Then
                JumlahDebet_ReturPembelianBahanBakuImport += AmbilAngka(row("Total_Harga"))
            End If
        Next

        'Isi Value Jumlah Debet COA Retur Pembelian Bahan Baku Import :
        Dim JumlahDebet_ReturPembelianBahanPenolong = 0
        For Each row As DataRow In datatabelUtama.Rows
            If row("COA_Produk") = KodeTautanCOA_PembelianBahanPenolong Then
                JumlahDebet_ReturPembelianBahanPenolong += AmbilAngka(row("Total_Harga"))
            End If
        Next

        'Isi Value Jumlah Debet COA Retur Pembelian Lainnya :
        Dim JumlahDebet_ReturPembelianLainnya = 0
        For Each row As DataRow In datatabelUtama.Rows
            If row("COA_Produk") <> KodeTautanCOA_PembelianBahanBaku_Lokal _
                And row("COA_Produk") <> KodeTautanCOA_PembelianBahanBaku_Import _
                And row("COA_Produk") <> KodeTautanCOA_PembelianBahanPenolong _
                Then
                JumlahDebet_ReturPembelianLainnya += AmbilAngka(row("Total_Harga"))
            End If
        Next

        If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0

        If StatusSuntingDatabase = True And JenisTahunBuku = JenisTahunBuku_NORMAL Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
            jur_JenisJurnal = JenisJurnal_ReturPembelian
            jur_NomorPO = Kosongan
            jur_KodeProject = KodeProjectJurnal
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoiceJurnal 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
            jur_NomorInvoice = NomorInvoiceJurnal
            jur_NomorFakturPajak = NomorRetur
            jur_KodeLawanTransaksi = KodeSupplier
            jur_NamaLawanTransaksi = NamaSupplier
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeTautanCOA_HutangUsaha_NonAfiliasi, TotalRetur)
            _______jurKredit(KodeTautanCOA_ReturPembelianBahanBaku_Lokal, JumlahDebet_ReturPembelianBahanBakuLokal)
            _______jurKredit(KodeTautanCOA_ReturPembelianBahanBaku_Import, JumlahDebet_ReturPembelianBahanBakuImport)
            _______jurKredit(KodeTautanCOA_ReturPembelianBahanPenolong, JumlahDebet_ReturPembelianBahanPenolong)
            _______jurKredit(KodeTautanCOA_ReturPembelianLainnya, JumlahDebet_ReturPembelianLainnya)
            _______jurKredit(KodeTautanCOA_PPNMasukan_Lokal, PPN)

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                jur_StatusPenyimpananJurnal_Lengkap = True
            Else
                jur_StatusPenyimpananJurnal_Lengkap = False
            End If

            ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        End If

        If StatusSuntingDatabase = True Then
            frm_ReturPembelian.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If


    End Sub



    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Dim PanjangKolomCOAProduk As Integer
    Dim PanjangKolomTotal As Integer
    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        PanjangKolomCOAProduk = 57
        PanjangKolomTotal = 675
        Buat_DataTabelUtama()
        'Buat_DataTabelTotal()
        Buat_DataTabelInvoice()
        txt_NomorRetur.IsReadOnly = True
        txt_KodeSupplier.IsReadOnly = True
        txt_NamaSupplier.IsReadOnly = True
        txt_PICSupplier.IsReadOnly = True
        txt_AlamatSupplier.IsReadOnly = True
        cmb_JenisPPN.IsReadOnly = True
        cmb_PerlakuanPPN.IsReadOnly = True
        txt_JumlahNota.IsReadOnly = True
        txt_Diskon_Persen.IsReadOnly = True
        txt_Diskon_Rp.IsReadOnly = True
        txt_DasarPengenaanPajak.IsReadOnly = True
        txt_TarifPPN.IsReadOnly = True
        txt_DasarPengenaanPajak_11Per12.IsReadOnly = True
        txt_TarifPPN_11Per12.IsReadOnly = True
        txt_PPN.IsReadOnly = True
        txt_TotalRetur.IsReadOnly = True
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
        scv_Kanan.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub


    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut As New DataGridTextColumn
    Dim Jenis_Produk_Per_Item As New DataGridTextColumn
    Dim Nomor_Invoice_Produk As New DataGridTextColumn
    Dim Tanggal_Invoice_Produk As New DataGridTextColumn
    Dim COA_Produk As New DataGridTextColumn     '<----------------------------------- Baru '
    Dim Nama_Produk As New DataGridTextColumn
    Dim Deskripsi_Produk As New DataGridTextColumn
    Dim Jumlah_Produk As New DataGridTextColumn
    Dim Satuan_Produk As New DataGridTextColumn
    Dim Harga_Satuan As New DataGridTextColumn
    Dim Jumlah_Harga_Per_Item As New DataGridTextColumn
    Dim Diskon_Per_Item_Persen As New DataGridTextColumn
    Dim Diskon_Per_Item_Rp As New DataGridTextColumn
    Dim Total_Harga As New DataGridTextColumn
    Dim Kode_Project_Produk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Produk_Per_Item")
        datatabelUtama.Columns.Add("Nomor_Invoice_Produk")
        datatabelUtama.Columns.Add("Tanggal_Invoice_Produk")
        datatabelUtama.Columns.Add("COA_Produk")     '<----------------------------------- Baru
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Deskripsi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk", GetType(Int64))
        datatabelUtama.Columns.Add("Satuan_Produk")
        datatabelUtama.Columns.Add("Harga_Satuan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Per_Item", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Per_Item_Persen")
        datatabelUtama.Columns.Add("Diskon_Per_Item_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Kode_Project_Produk")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk_Per_Item, "Jenis_Produk_Per_Item", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice_Produk, "Nomor_Invoice_Produk", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice_Produk, "Tanggal_Invoice_Produk", "Tanggal Invoice", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Produk, "COA_Produk", "COA", PanjangKolomCOAProduk, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 51, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan, "Harga_Satuan", "Harga Satuan", 93, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Per_Item, "Jumlah_Harga_Per_Item", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Persen, "Diskon_Per_Item_Persen", "Diskon" & Enter1Baris & "(%)", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Per_Item_Rp, "Diskon_Per_Item_Rp", "Diskon" & Enter1Baris & "(Rp)", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Harga, "Total_Harga", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project_Produk, "Kode_Project_Produk", "Kode Project", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


    'Pembuatan Tabel Invoice :
    Public datatabelInvoice As DataTable
    Public dataviewInvoice As DataView
    Public rowviewInvoice As DataRowView
    Public newRowInvoice As DataRow
    Public HeaderKolomInvoice As DataGridColumnHeader
    Public KolomTerseleksiInvoice As DataGridColumn
    Public BarisTerseleksiInvoice As Integer
    Public JumlahBarisInvoice As Integer

    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Sub Buat_DataTabelInvoice()

        datatabelInvoice = New DataTable
        datatabelInvoice.Columns.Add("Nomor_Invoice")
        datatabelInvoice.Columns.Add("Tanggal_Invoice")
        datatabelInvoice.Columns.Add("Kode_Project")

        StyleTabelUtama_WPF(datagridInvoice, datatabelInvoice, dataviewInvoice)
        TambahkanKolomTextBoxDataGrid_WPF(datagridInvoice, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridInvoice, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridInvoice, Kode_Project, "Kode_Project", "Kode Proyek", 135, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    ''Tabel Total :
    'Public datatabelTotal As DataTable
    'Public dataviewTotal As DataView
    'Public rowviewTotal As DataRowView

    'Dim Baris_Total As New DataGridTextColumn
    'Dim Jumlah_Harga_Keseluruhan As New DataGridTextColumn
    'Dim Diskon_Persen_Keseluruhan As New DataGridTextColumn
    'Dim Diskon_Rp_Keseluruhan As New DataGridTextColumn
    'Dim Total_Harga_Keseluruhan As New DataGridTextColumn
    'Sub Buat_DataTabelTotal()

    '    datatabelTotal = New DataTable
    '    datatabelTotal.Columns.Add("Baris_Total")
    '    datatabelTotal.Columns.Add("Jumlah_Harga_Keseluruhan", GetType(Int64))
    '    datatabelTotal.Columns.Add("Diskon_Persen_Keseluruhan")
    '    datatabelTotal.Columns.Add("Diskon_Rp_Keseluruhan", GetType(Int64))
    '    datatabelTotal.Columns.Add("Total_Harga_Keseluruhan", GetType(Int64))

    '    StyleTabelTotal_WPF(datagridTotal, datatabelTotal, dataviewTotal)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Baris_Total, "Baris_Total", "No.", PanjangKolomTotal, FormatString, TengahTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Jumlah_Harga_Keseluruhan, "Jumlah_Harga_Keseluruhan", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Persen_Keseluruhan, "Diskon_Persen_Keseluruhan", "Diskon" & Enter1Baris & "(%)", 72, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Diskon_Rp_Keseluruhan, "Diskon_Rp_Keseluruhan", "Diskon" & Enter1Baris & "(Rp)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    TambahkanKolomTextBoxDataGrid_WPF(datagridTotal, Total_Harga_Keseluruhan, "Total_Harga_Keseluruhan", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
    '    datatabelTotal.Rows.Add("TOTAL", 0, Kosongan, 0, 0)

    'End Sub

End Class