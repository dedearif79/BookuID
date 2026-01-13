Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc
Imports Mysqlx

Public Class wpfWin_InputProduk_Nota

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public NomorUrutProduk
    Public JenisProduk_Induk
    Public JenisProduk_PerItem
    Public InvoiceDenganPO
    Public NomorSJBAST
    Public TanggalSJBAST
    Public TanggalDiterimaSJBAST
    Public COA_PerProduk
    Public NamaProduk
    Public DeskripsiProduk
    Public JumlahProduk
    Public SatuanProduk
    Public DiskonPerItem_Persen As Decimal
    Public HargaSatuan
    Public DiskonPerItem_Rp As Int64 '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public JumlahHarga
    Public TotalHarga
    Public HargaSatuan_Asing As Decimal
    Public DiskonPerItem_Asing As Decimal
    Public JumlahHarga_Asing As Decimal
    Public TotalHarga_Asing As Decimal
    Public Peruntukan
    Public KodeProject

    Public MataUang

    Dim KunciInputanKolom

    Dim Tabel As New DataGridView
    Dim TabelWPF As New DataTable

    Public Proses As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case JalurMasuk
            Case Form_INPUTPOPEMBELIAN
                TabelWPF = win_InputPOPembelian.datatabelUtama
            Case Form_INPUTINVOICEPEMBELIAN
                TabelWPF = win_InputInvoicePembelian.datatabelUtama
            Case Form_INPUTRETURPEMBELIAN
                TabelWPF = win_InputReturPembelian.datatabelUtama
            Case Form_INPUTPOPENJUALAN
                TabelWPF = win_InputPOPenjualan.datatabelUtama
            Case Form_INPUTINVOICEPENJUALAN
                TabelWPF = win_InputInvoicePenjualan.datatabelUtama
            Case Form_INPUTRETURPENJUALAN
                TabelWPF = win_InputReturPenjualan.datatabelUtama
            Case Else
                PesanUntukProgrammer("Jalur masuk form belum ditentukan...!!!")
                Return
        End Select 'Yang baris-baris hijau saat ini tidak diperlukan. Suatu saat nanti dihapus saja.

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Produk"
            btn_Tambahkan.Content = "Tambahkan"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Produk"
            btn_Tambahkan.Content = "Perbarui"
        End If

        If FungsiForm = Kosongan Then
            PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")
            Close()
            Return
        End If

        If MataUang = Kosongan Then
            PesanUntukProgrammer("Mata Uang belum ditentukan...!!!")
            Close()
            Return
        End If

        Title = JudulForm

        If JalurMasuk <> Form_INPUTPOPENJUALAN _
            And JalurMasuk <> Form_INPUTPOPEMBELIAN _
            And Not (JalurMasuk = Form_INPUTINVOICEPEMBELIAN And win_InputInvoicePembelian.NP <> "N") _
            And Not (JalurMasuk = Form_INPUTINVOICEPENJUALAN And win_InputInvoicePenjualan.NP <> "N") _
            And InvoiceDenganPO = True _
            Then
            KunciInputanKolom = True
        Else
            KunciInputanKolom = False
        End If

        If JalurMasuk = Form_INPUTRETURPEMBELIAN Or JalurMasuk = Form_INPUTRETURPENJUALAN Then
            KunciInputanKolom = False
            lbl_NomorUrut.IsEnabled = False
            txt_NomorUrut.IsEnabled = False
            cmb_JenisProduk.IsEnabled = False
            lbl_NamaProduk.IsEnabled = False
            txt_NamaProduk.IsEnabled = False
            lbl_Deskripsi.IsEnabled = False
            txt_DeskripsiProduk.IsEnabled = False
            lbl_JumlahProduk.Text = "Jumlah yang Diretur"
            lbl_Satuan.IsEnabled = False
            txt_Satuan.IsEnabled = False
            lbl_KeteranganSatuan.IsEnabled = False
            lbl_HargaSatuan.IsEnabled = False
            txt_HargaSatuan.IsEnabled = False
            lbl_JumlahHarga.IsEnabled = False
            txt_JumlahHarga.IsEnabled = False
            lbl_Diskon.IsEnabled = False
            txt_DiskonPerItem_Persen.IsEnabled = False
            lbl_Persen.IsEnabled = False
            txt_DiskonPerItem_Rp.IsEnabled = False
            lbl_TotalHarga.IsEnabled = False
            txt_TotalHarga.IsEnabled = False
            lbl_Persen.IsEnabled = False
            cmb_Peruntukan.IsEnabled = False
            lbl_KodeProject.IsEnabled = False
            txt_KodeProject.IsEnabled = False
            btn_PilihKodeProject.IsEnabled = False
            lbl_Peruntukan.Visibility = Visibility.Collapsed
            cmb_Peruntukan.Visibility = Visibility.Collapsed
        End If

        KontenCombo_JenisProduk()
        KetersediaanKolom_KodeProject()

        ProsesLoadingForm = False

        If KunciInputanKolom Then
            txt_NamaProduk.IsReadOnly = True
            txt_DeskripsiProduk.IsReadOnly = True
            txt_JumlahProduk.IsReadOnly = True
            txt_Satuan.IsReadOnly = True
            txt_HargaSatuan.IsReadOnly = True
            txt_HargaSatuan_Asing.IsReadOnly = True
            txt_DiskonPerItem_Persen.IsReadOnly = True
        Else
            txt_NamaProduk.IsReadOnly = False
            txt_DeskripsiProduk.IsReadOnly = False
            txt_JumlahProduk.IsReadOnly = False
            txt_Satuan.IsReadOnly = False
            txt_HargaSatuan.IsReadOnly = False
            txt_HargaSatuan_Asing.IsReadOnly = False
            txt_DiskonPerItem_Persen.IsReadOnly = False
        End If

        LogikaMataUang()

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        Proses = False

        MataUang = Kosongan

        KetersediaanSemuaObjek()
        InvoiceDenganPO = True
        KunciInputanKolom = False
        JalurMasuk = Kosongan
        JenisProduk_Induk = Kosongan
        txt_NomorUrut.Text = Kosongan
        NomorSJBAST = Kosongan
        TanggalSJBAST = TanggalFormatTampilan(TanggalKosong)
        TanggalDiterimaSJBAST = TanggalFormatTampilan(TanggalKosong)
        COA_PerProduk = Kosongan
        txt_NamaProduk.IsEnabled = True
        txt_NamaProduk.Text = Kosongan
        txt_DeskripsiProduk.Text = Kosongan
        lbl_JumlahProduk.Text = "Jumlah"
        txt_JumlahProduk.Text = Kosongan
        txt_Satuan.Text = Kosongan
        txt_DiskonPerItem_Persen.Text = Kosongan
        txt_HargaSatuan.Text = Kosongan
        txt_JumlahHarga.Text = Kosongan
        txt_DiskonPerItem_Rp.Text = Kosongan
        txt_TotalHarga.Text = Kosongan
        txt_HargaSatuan_Asing.Text = Kosongan
        txt_JumlahHarga_Asing.Text = Kosongan
        txt_DiskonPerItem_Asing.Text = Kosongan
        txt_TotalHarga_Asing.Text = Kosongan
        cmb_JenisProduk.Text = Kosongan 'Ini Penting...!!! Jangan dihapus...!!!
        KontenCombo_Peruntukan()
        txt_KodeProject.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Sub LogikaMataUang()
        If MataUang = MataUang_Rupiah Then
            txt_HargaSatuan.Visibility = Visibility.Visible
            txt_JumlahHarga.Visibility = Visibility.Visible
            txt_DiskonPerItem_Rp.Visibility = Visibility.Visible
            txt_TotalHarga.Visibility = Visibility.Visible
            txt_HargaSatuan_Asing.Visibility = Visibility.Collapsed
            txt_JumlahHarga_Asing.Visibility = Visibility.Collapsed
            txt_DiskonPerItem_Asing.Visibility = Visibility.Collapsed
            txt_TotalHarga_Asing.Visibility = Visibility.Collapsed
            txt_HargaSatuan.IsReadOnly = False
            txt_HargaSatuan_Asing.IsReadOnly = True
        Else
            txt_HargaSatuan.Visibility = Visibility.Collapsed
            txt_JumlahHarga.Visibility = Visibility.Collapsed
            txt_DiskonPerItem_Rp.Visibility = Visibility.Collapsed
            txt_TotalHarga.Visibility = Visibility.Collapsed
            txt_HargaSatuan_Asing.Visibility = Visibility.Visible
            txt_JumlahHarga_Asing.Visibility = Visibility.Visible
            txt_DiskonPerItem_Asing.Visibility = Visibility.Visible
            txt_TotalHarga_Asing.Visibility = Visibility.Visible
            txt_HargaSatuan.IsReadOnly = True
            txt_HargaSatuan_Asing.IsReadOnly = False
        End If
    End Sub

    Sub KetersediaanSemuaObjek()
        lbl_NomorUrut.IsEnabled = True
        txt_NomorUrut.IsEnabled = True
        cmb_JenisProduk.IsEnabled = True
        lbl_NamaProduk.IsEnabled = True
        lbl_Deskripsi.IsEnabled = True
        txt_DeskripsiProduk.IsEnabled = True
        txt_NamaProduk.IsEnabled = True
        lbl_JumlahProduk.IsEnabled = True
        txt_JumlahProduk.IsEnabled = True
        lbl_Satuan.IsEnabled = True
        txt_Satuan.IsEnabled = True
        lbl_KeteranganSatuan.IsEnabled = True
        lbl_HargaSatuan.IsEnabled = True
        txt_HargaSatuan.IsEnabled = True
        lbl_JumlahHarga.IsEnabled = True
        txt_JumlahHarga.IsEnabled = True
        lbl_Diskon.IsEnabled = True
        txt_DiskonPerItem_Persen.IsEnabled = True
        lbl_Persen.IsEnabled = True
        txt_DiskonPerItem_Rp.IsEnabled = True
        lbl_TotalHarga.IsEnabled = True
        txt_TotalHarga.IsEnabled = True
        lbl_Persen.IsEnabled = True
        cmb_Peruntukan.IsEnabled = True
        lbl_KodeProject.IsEnabled = True
        txt_KodeProject.IsEnabled = True
        btn_PilihKodeProject.IsEnabled = True
    End Sub



    Sub KontenCombo_JenisProduk()
        cmb_JenisProduk.Items.Clear()
        If JenisProduk_Induk <> Kosongan Then
            If JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                cmb_JenisProduk.Items.Add(JenisProduk_Barang)
                cmb_JenisProduk.Items.Add(JenisProduk_Jasa)
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then cmb_JenisProduk.Items.Add(JenisProduk_BarangDanJasa)
                If FungsiForm = FungsiForm_TAMBAH Then cmb_JenisProduk.Text = Kosongan
                If FungsiForm = FungsiForm_EDIT Then cmb_JenisProduk.SelectedValue = JenisProduk_PerItem
                cmb_JenisProduk.IsEnabled = True
                lbl_NamaProduk.Text = "Nama Barang/Jasa"
            Else
                cmb_JenisProduk.IsEnabled = False
                Select Case JenisProduk_Induk
                    Case JenisProduk_Barang
                        lbl_NamaProduk.Text = "Nama Barang"
                        IsiValueComboBypassTerkunci(cmb_JenisProduk, JenisProduk_Barang)
                    Case JenisProduk_Jasa
                        lbl_NamaProduk.Text = "Nama Jasa"
                        IsiValueComboBypassTerkunci(cmb_JenisProduk, JenisProduk_Jasa)
                End Select
            End If
        End If
        If JalurMasuk = Form_INPUTRETURPEMBELIAN Or JalurMasuk = Form_INPUTRETURPENJUALAN Then
            IsiValueComboBypassTerkunci(cmb_JenisProduk, JenisProduk_Barang)
        End If
    End Sub


    Sub KontenCombo_Peruntukan()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_Project)
        cmb_Peruntukan.Items.Add(Peruntukan_NonProject)
        cmb_Peruntukan.Text = Kosongan
        lbl_Peruntukan.IsEnabled = True
        lbl_Peruntukan.Visibility = Visibility.Visible
        cmb_Peruntukan.IsEnabled = True
        cmb_Peruntukan.Visibility = Visibility.Visible
    End Sub


    Sub Perhitungan()

        'Rupiah :
        JumlahHarga = JumlahProduk * HargaSatuan
        DiskonPerItem_Rp = JumlahHarga * (DiskonPerItem_Persen / 100)
        TotalHarga = JumlahHarga - DiskonPerItem_Rp

        txt_JumlahHarga.Text = JumlahHarga
        txt_DiskonPerItem_Rp.Text = DiskonPerItem_Rp
        txt_TotalHarga.Text = TotalHarga

        'Asing :
        JumlahHarga_Asing = JumlahProduk * HargaSatuan_Asing
        DiskonPerItem_Asing = JumlahHarga_Asing * (DiskonPerItem_Persen / 100)
        TotalHarga_Asing = JumlahHarga_Asing - DiskonPerItem_Asing

        txt_JumlahHarga_Asing.Text = JumlahHarga_Asing
        txt_DiskonPerItem_Asing.Text = DiskonPerItem_Asing
        txt_TotalHarga_Asing.Text = TotalHarga_Asing

    End Sub




    Private Sub txt_NomorUrut_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorUrut.TextChanged
        NomorUrutProduk = AmbilAngka(txt_NomorUrut.Text)
    End Sub


    Private Sub cmb_JenisProduk_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisProduk.SelectionChanged
        JenisProduk_PerItem = cmb_JenisProduk.SelectedValue
    End Sub


    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub


    Private Sub txt_DeskripsiProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DeskripsiProduk.TextChanged
        DeskripsiProduk = txt_DeskripsiProduk.Text
    End Sub


    Private Sub txt_JumlahProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahProduk.TextChanged
        JumlahProduk = AmbilAngka(txt_JumlahProduk.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahProduk)
        Perhitungan()
    End Sub


    Private Sub txt_Satuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Satuan.TextChanged
        SatuanProduk = txt_Satuan.Text
    End Sub


    Private Sub txt_HargaSatuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HargaSatuan.TextChanged
        HargaSatuan = AmbilAngka(txt_HargaSatuan.Text)
        PemecahRibuanUntukTextBox_WPF(txt_HargaSatuan)
        Perhitungan()
    End Sub
    Private Sub txt_HargaSatuan_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HargaSatuan_Asing.TextChanged
        HargaSatuan_Asing = AmbilAngka_Asing(txt_HargaSatuan_Asing.Text)
        Perhitungan()
    End Sub


    Private Sub txt_JumlahHarga_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHarga.TextChanged
        JumlahHarga = AmbilAngka(txt_JumlahHarga.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHarga)
    End Sub
    Private Sub txt_JumlahHarga_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHarga_Asing.TextChanged
        JumlahHarga_Asing = AmbilAngka_Asing(txt_JumlahHarga_Asing.Text)
    End Sub


    Private Sub txt_DiskonPerItem_Persen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DiskonPerItem_Persen.TextChanged
        TextBoxFormatPersen_WPF(txt_DiskonPerItem_Persen, DiskonPerItem_Persen)
        Perhitungan()
    End Sub


    Private Sub txt_DiskonPerItem_Rp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DiskonPerItem_Rp.TextChanged
        DiskonPerItem_Rp = AmbilAngka(txt_DiskonPerItem_Rp.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DiskonPerItem_Rp)
    End Sub
    Private Sub txt_DiskonPerItem_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DiskonPerItem_Asing.TextChanged
        DiskonPerItem_Asing = AmbilAngka_Asing(txt_DiskonPerItem_Asing.Text)
    End Sub


    Private Sub txt_TotalHarga_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalHarga.TextChanged
        TotalHarga = AmbilAngka(txt_TotalHarga.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalHarga)
    End Sub
    Private Sub txt_TotalHarga_Asing_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalHarga_Asing.TextChanged
        TotalHarga_Asing = AmbilAngka_Asing(txt_TotalHarga_Asing.Text)
    End Sub


    Private Sub cmb_Peruntukan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Peruntukan.SelectionChanged
        Peruntukan = cmb_Peruntukan.SelectedValue
        KetersediaanKolom_KodeProject()
    End Sub


    Private Sub txt_KodeProject_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeProject.TextChanged
        KodeProject = txt_KodeProject.Text
    End Sub


    Private Sub btn_PilihKodeProject_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihKodeProject.Click
        win_ListDataProject = New wpfWin_ListDataProject
        win_ListDataProject.ResetForm()
        If txt_KodeProject.Text <> Kosongan Then
            win_ListDataProject.KodeProject_Terseleksi = txt_KodeProject.Text
        End If
        win_ListDataProject.ShowDialog()
        txt_KodeProject.Text = win_ListDataProject.KodeProject_Terseleksi
    End Sub

    Sub KetersediaanKolom_KodeProject()
        If Peruntukan = Peruntukan_Project Then
            lbl_KodeProject.Visibility = Visibility.Visible
            txt_KodeProject.Visibility = Visibility.Visible
            btn_PilihKodeProject.Visibility = Visibility.Visible
        Else
            lbl_KodeProject.Visibility = Visibility.Collapsed
            txt_KodeProject.Visibility = Visibility.Collapsed
            btn_PilihKodeProject.Visibility = Visibility.Collapsed
            txt_KodeProject.Text = Kosongan
        End If
    End Sub




    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click

        If JenisProduk_PerItem = Kosongan Then
            PesanPeringatan("Silakan pilih 'Jenis Produk'.")
            cmb_JenisProduk.Focus()
            Return
        End If

        If NamaProduk = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Barang/Jasa'.")
            txt_NamaProduk.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah'.")
            txt_JumlahProduk.Focus()
            Return
        End If

        If MataUang = MataUang_Rupiah Then
            If HargaSatuan = 0 Then
                PesanPeringatan("Silakan isi kolom 'Harga Satuan'.")
                txt_HargaSatuan.Focus()
                Return
            End If
        Else
            If HargaSatuan_Asing = 0 Then
                PesanPeringatan("Silakan isi kolom 'Harga Satuan'.")
                txt_HargaSatuan_Asing.Focus()
                Return
            End If
        End If

        If cmb_Peruntukan.Visibility = Visibility.Visible And Peruntukan = Kosongan Then
            PesanPeringatan("Silakan pilih 'Peruntukan'.")
            cmb_Peruntukan.Focus()
            Return
        End If

        If Peruntukan = Peruntukan_Project And KodeProject = Kosongan Then
            PesanPeringatan("Silakan pilih 'Kode Project'")
            txt_KodeProject.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            If JalurMasuk = Form_INPUTPOPEMBELIAN Then
                TabelWPF.Rows.Add(NomorUrutProduk, JenisProduk_PerItem,
                                  NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing,
                                  JumlahHarga, JumlahHarga_Asing, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"),
                                  DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHarga, TotalHarga_Asing, KodeProject)
            End If
            If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
                TabelWPF.Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, Kosongan, COA_PerProduk,
                                  NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing,
                                  JumlahHarga, JumlahHarga_Asing, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHarga, TotalHarga_Asing, KodeProject,
                                  Kosongan, Kosongan, Kosongan, Kosongan) '(Yang Kosongan jangan dihapus...!!! Supaya tidak Nothing/DBNull).
            End If
            If JalurMasuk = Form_INPUTPOPENJUALAN Then
                TabelWPF.Rows.Add(NomorUrutProduk, JenisProduk_PerItem,
                                  NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing,
                                  JumlahHarga, JumlahHarga_Asing, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"),
                                  DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHarga, TotalHarga_Asing, KodeProject)
            End If
            If JalurMasuk = Form_INPUTINVOICEPENJUALAN Then
                TabelWPF.Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, Kosongan,
                                  NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan, HargaSatuan_Asing,
                                  JumlahHarga, JumlahHarga_Asing, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"),
                                  DiskonPerItem_Rp, DiskonPerItem_Asing, TotalHarga, TotalHarga_Asing, KodeProject)
            End If
        End If

        If FungsiForm = FungsiForm_EDIT And Tabel.RowCount > 0 Then
            'Ini untuk konsep lama (WinForm).
            Tabel.Item("Jenis_Produk_Per_Item", NomorUrutProduk - 1).Value = JenisProduk_PerItem
            Tabel.Item("Nama_Produk", NomorUrutProduk - 1).Value = NamaProduk
            Tabel.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value = DeskripsiProduk
            Tabel.Item("Jumlah_Produk", NomorUrutProduk - 1).Value = JumlahProduk
            Tabel.Item("Satuan_Produk", NomorUrutProduk - 1).Value = SatuanProduk
            Tabel.Item("Harga_Satuan", NomorUrutProduk - 1).Value = HargaSatuan
            Tabel.Item("Jumlah_Harga_Per_Item", NomorUrutProduk - 1).Value = JumlahHarga
            Tabel.Item("Diskon_Per_Item_Persen", NomorUrutProduk - 1).Value = (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %")
            Tabel.Item("Diskon_Per_Item_Rp", NomorUrutProduk - 1).Value = DiskonPerItem_Rp
            Tabel.Item("Total_Harga", NomorUrutProduk - 1).Value = TotalHarga
            Tabel.Item("Kode_Project_Produk", NomorUrutProduk - 1).Value = KodeProject
            If JenisProduk_PerItem = JenisProduk_Jasa Then
                For Each row As DataGridViewRow In Tabel.Rows
                    If row.Cells("Jenis_Produk_Per_Item").Value = JenisProduk_Jasa Then row.Cells("Nama_Produk").Value = NamaProduk
                Next
            End If
        End If

        Proses = True
        Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Proses = False
        Me.Close()
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_NomorUrut.IsReadOnly = True
        cmb_JenisProduk.IsReadOnly = True
        txt_HargaSatuan.IsReadOnly = True
        txt_HargaSatuan_Asing.IsReadOnly = True
        txt_JumlahHarga.IsReadOnly = True
        txt_JumlahHarga_Asing.IsReadOnly = True
        txt_DiskonPerItem_Rp.IsReadOnly = True
        txt_DiskonPerItem_Asing.IsReadOnly = True
        txt_TotalHarga.IsReadOnly = True
        txt_TotalHarga_Asing.IsReadOnly = True
        cmb_Peruntukan.IsReadOnly = True
        txt_KodeProject.IsReadOnly = True
    End Sub

End Class
