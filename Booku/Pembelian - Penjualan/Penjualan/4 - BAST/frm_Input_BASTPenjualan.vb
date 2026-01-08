Imports bcomm
Imports System.Data.Odbc

Public Class frm_Input_BASTPenjualan

    Public JudulForm
    Public FungsiForm
    Public NomorID

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    'Variabel Kolom :
    Public AngkaBAST
    Public NomorBAST
    Dim NomorBAST_Lama
    Dim TanggalBAST
    Dim YangMenyerahkan
    Dim YangMenerima
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeCustomer
    Dim NamaCustomer
    Dim Catatan

    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim KodeProjectProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem
    Dim SatuanProduk
    Dim KeteranganProduk

    'Variabel Tabel Produk - Index :
    Dim BarisProduk_Terseleksi
    Dim NomorUrutProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim JumlahProduk_Maksimal_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim KeteranganProduk_Terseleksi

    'Variabel Tabel PO - Index :
    Dim BarisPO_Terseleksi
    Dim NomorPO_Terseleksi

    Dim JumlahProduk
    Dim JumlahPO

    Dim NomorPO_Sebelumnya = Kosongan
    Dim StatusPO = Kosongan

    Dim Koreksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Berita Acara Serah Terima (BAST)"
            SistemPenomoranOtomatis_BAST()
            lbl_NamaPenerima.Enabled = False
            lbl_TanggalDiterima.Enabled = False
            txt_YangMenerima.Enabled = False
            dtp_TanggalDiterima.Enabled = False
            btn_Pratinjau.Visible = False
            btn_Cetak.Visible = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Berita Acara Serah Terima (BAST)"
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                  " WHERE Kode_Mitra = '" & KodeCustomer & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            txt_AlamatCustomer.Text = dr.Item("Alamat")
            AksesDatabase_General(Tutup)
            NomorBAST_Lama = NomorBAST
            lbl_NamaPenerima.Enabled = True
            lbl_TanggalDiterima.Enabled = True
            txt_YangMenerima.Enabled = True
            dtp_TanggalDiterima.Enabled = True
            lbl_JenisPPN.Enabled = False
            lbl_PerlakuanPPN.Enabled = False
            cmb_JenisPPN.Enabled = False
            cmb_PerlakuanPPN.Enabled = False
            btn_Pratinjau.Visible = True
            btn_Cetak.Visible = True
        End If

        Me.Text = JudulForm

        StyleTabelUtama(DataTabelUtama)
        StyleTabelUtama(dgv_PO)

        BeginInvoke(Sub() BersihkanSeleksi_TabelProduk())
        BeginInvoke(Sub() BersihkanSeleksi_TabelPO())
        BeginInvoke(Sub() txt_NomorBAST.Focus())

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan
        JenisProduk_Induk = Kosongan

        NomorID = 0
        AngkaBAST = 0
        txt_NomorBAST.Text = Kosongan
        dtp_TanggalBAST.Value = Today
        txt_YangMenyerahkan.Text = Kosongan
        txt_YangMenerima.Text = Kosongan
        dtp_TanggalDiterima.Value = Today
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_AlamatCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_Catatan.Text = Kosongan
        Kosongkan_TabelProduk()
        Kosongkan_TabelPO()
        btn_Pratinjau.Enabled = True
        btn_Cetak.Enabled = True
        btn_Simpan.Enabled = True
        Koreksi = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_JenisPPN()
        cmb_JenisPPN.Items.Clear()
        cmb_JenisPPN.Items.Add(JenisPPN_NonPPN)
        cmb_JenisPPN.Items.Add(JenisPPN_Exclude)
        cmb_JenisPPN.Items.Add(JenisPPN_Include)
        cmb_JenisPPN.Text = Kosongan
        lbl_JenisPPN.Enabled = True
        cmb_JenisPPN.Enabled = True
    End Sub

    Sub KontenCombo_PerlakuanPPN_Kosongan()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
    End Sub

    Sub KontenCombo_PerlakuanPPN_NonPPN()
        lbl_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub

    Sub KontenCombo_PerlakuanPPN_AdaPPN()
        lbl_PerlakuanPPN.Enabled = True
        cmb_PerlakuanPPN.Enabled = True
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dibayar)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_Dipungut)
        cmb_PerlakuanPPN.Items.Add(PerlakuanPPN_TidakDipungut)
    End Sub

    Sub SistemPenomoranOtomatis_BAST()

        If FungsiForm = FungsiForm_TAMBAH Then AngkaBAST = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_BAST", "Angka_BAST") + 1
        NomorBAST = AwalanBAST & AngkaBAST.ToString & "-" & BulanRomawi(dtp_TanggalBAST.Value.Month) & "-" & dtp_TanggalBAST.Value.Year
        txt_NomorBAST.Text = NomorBAST

    End Sub

    Sub Kosongkan_TabelProduk()
        DataTabelUtama.Rows.Clear()
        JumlahProduk = 0
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        BarisProduk_Terseleksi = -1
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        DataTabelUtama.ClearSelection()
        JumlahProduk = DataTabelUtama.RowCount
    End Sub

    Sub BersihkanSeleksi_TabelPO()
        BarisPO_Terseleksi = -1
        NomorPO_Terseleksi = Kosongan
        btn_SingkirkanPO.Enabled = False
        dgv_PO.ClearSelection()
        JumlahPO = dgv_PO.RowCount
    End Sub

    Sub Kosongkan_TabelPO()
        dgv_PO.Rows.Clear()
    End Sub

    Sub KondisiFormSetelahPerubahan()
        If ProsesLoadingForm = True Or ProsesResetForm = True Or ProsesIsiValueForm = True Then Return
        BersihkanSeleksi_TabelProduk()
        btn_Pratinjau.Enabled = False
        btn_Cetak.Enabled = False
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

    Private Sub txt_NomorBAST_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBAST.TextChanged
        NomorBAST = txt_NomorBAST.Text
    End Sub
    Private Sub txt_NomorBAST_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBAST.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_TanggalBAST_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBAST.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBAST)
        TanggalBAST = dtp_TanggalBAST.Value
        If ProsesIsiValueForm = False And ProsesResetForm = False Then SistemPenomoranOtomatis_BAST()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_YangMenyerahkan_TextChanged(sender As Object, e As EventArgs) Handles txt_YangMenyerahkan.TextChanged
        YangMenyerahkan = txt_YangMenyerahkan.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_YangMenerima_TextChanged(sender As Object, e As EventArgs) Handles txt_YangMenerima.TextChanged
        YangMenerima = txt_YangMenerima.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub dtp_TanggalDiterima_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalDiterima.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalDiterima)
        TanggalDiterima = dtp_TanggalDiterima.Value
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        If KodeCustomer = Kosongan Then
            btn_TambahPO.Enabled = False
        Else
            btn_TambahPO.Enabled = True
        End If
        Kosongkan_TabelPO()
    End Sub
    Private Sub txt_KodeCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Mitra_Customer
        If txt_KodeCustomer.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeCustomer.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaCustomer.Text
            win_ListLawanTransaksi.AlamatMitraTerseleksi = txt_AlamatCustomer.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeCustomer.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaCustomer.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
        txt_AlamatCustomer.Text = win_ListLawanTransaksi.AlamatMitraTerseleksi
        'If ProsesResetForm = False And ProsesLoadingForm = False And KodeCustomer <> Kosongan Then btn_TambahPO_Click(sender, e)
    End Sub

    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_NamaCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AlamatCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_AlamatCustomer.TextChanged
    End Sub
    Private Sub txt_AlamatCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AlamatCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.TextChanged
        JenisPPN = cmb_JenisPPN.Text
        If JenisPPN = JenisPPN_NonPPN Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub cmb_PerlakuanPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_PerlakuanPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PerlakuanPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PerlakuanPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.TextChanged
        PerlakuanPPN = cmb_PerlakuanPPN.Text
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi_TabelProduk()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        BarisProduk_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrutProduk_Terseleksi = DataTabelUtama.Item("Nomor_Urut", BarisProduk_Terseleksi).Value
        NamaProduk_Terseleksi = DataTabelUtama.Item("Nama_Produk", BarisProduk_Terseleksi).Value
        DeskripsiProduk_Terseleksi = DataTabelUtama.Item("Deskripsi_Produk", BarisProduk_Terseleksi).Value
        JumlahProduk_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", BarisProduk_Terseleksi).Value)
        JumlahProduk_Maksimal_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk_Maksimal", BarisProduk_Terseleksi).Value)
        SatuanProduk_Terseleksi = DataTabelUtama.Item("Satuan_Produk", BarisProduk_Terseleksi).Value
        KeteranganProduk_Terseleksi = DataTabelUtama.Item("Keterangan_Produk", BarisProduk_Terseleksi).Value

        If BarisProduk_Terseleksi >= 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick

        If DataTabelUtama.RowCount = 0 Then Return

        If BarisProduk_Terseleksi >= 0 Then
            btn_Edit_Click(sender, e)
        End If

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputProduk_SJBAST.ResetForm()
        frm_InputProduk_SJBAST.FungsiForm = FungsiForm_EDIT
        frm_InputProduk_SJBAST.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        frm_InputProduk_SJBAST.txt_NamaProduk.Text = NamaProduk_Terseleksi
        frm_InputProduk_SJBAST.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        frm_InputProduk_SJBAST.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        frm_InputProduk_SJBAST.JumlahProduk_Maksimal = JumlahProduk_Maksimal_Terseleksi
        frm_InputProduk_SJBAST.txt_Satuan.Text = SatuanProduk_Terseleksi
        frm_InputProduk_SJBAST.txt_Keterangan.Text = KeteranganProduk_Terseleksi
        frm_InputProduk_SJBAST.JalurMasuk = Form_INPUTBASTPENJUALAN
        frm_InputProduk_SJBAST.ShowDialog()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        Pilihan = MessageBox.Show("Yakin akan menghapus item terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        Dim NomorUrut = 0
        DataTabelUtama.Rows.Remove(DataTabelUtama.CurrentRow)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorUrut += 1
            row.Cells("Nomor_Urut").Value = NomorUrut
        Next
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_Catatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub dgv_PO_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_PO.CellContentClick
    End Sub
    Private Sub dgv_PO_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_PO.ColumnHeaderMouseClick
        BersihkanSeleksi_TabelPO()
    End Sub
    Private Sub dgv_PO_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_PO.CellClick
        If dgv_PO.RowCount = 0 Then Return
        BarisPO_Terseleksi = dgv_PO.CurrentRow.Index
        NomorPO_Terseleksi = dgv_PO.Item("Nomor_PO", BarisPO_Terseleksi).Value
        If BarisPO_Terseleksi >= 0 Then
            btn_SingkirkanPO.Enabled = True
        Else
            btn_SingkirkanPO.Enabled = False
        End If
    End Sub

    Private Sub btn_TambahPO_Click(sender As Object, e As EventArgs) Handles btn_TambahPO.Click
        win_ListPO = New wpfWin_ListPO
        win_ListPO.ResetForm()
        win_ListPO.Sisi = win_ListPO.Sisi_POPenjualan
        win_ListPO.NamaMitra_Filter = NamaCustomer
        win_ListPO.FilterMitra_Aktif = False
        win_ListPO.JenisProduk_Induk = JenisProduk_Induk
        win_ListPO.JenisPPN = JenisPPN
        win_ListPO.PerlakuanPPN = PerlakuanPPN
        win_ListPO.JalurMasuk = Form_INPUTBASTPENJUALAN
        For Each row As DataGridViewRow In dgv_PO.Rows
            If row.Cells("Nomor_PO").Value IsNot Nothing Then
                win_ListPO.ListNomorPO_Singkirkan.Add(row.Cells("Nomor_PO").Value.ToString())
            End If
        Next
        win_ListPO.ShowDialog()
        NomorPO = win_ListPO.NomorPO_Terseleksi
        TanggalPO = win_ListPO.TanggalPO_Terseleksi
        JenisProduk_Induk = win_ListPO.JenisProdukInduk_Terseleksi
        If NomorPO = Kosongan Then Return
        Dim TelusurPO = Kosongan
        For Each row As DataGridViewRow In dgv_PO.Rows
            TelusurPO = row.Cells("Nomor_PO").Value
            If TelusurPO = NomorPO Then
                MsgBox("Nomor PO ini sudah diinput..!")
                Return
            End If
        Next
        dgv_PO.Rows.Add(NomorPO, TanggalPO, win_ListPO.KodeProject_Terseleksi)        '<-- Penambahan Baris PO
        BersihkanSeleksi_TabelPO() 'Ini Jangan dihapus...! Penting..!
        AksesDatabase_Transaksi(Buka)
        If JenisProduk_Induk = JenisProduk_BarangDanJasa Then 'Jika Jenis Produk Induknya 'Barang dan Jasa', maka yang ditambahkan hanya Produk Jasa saja.
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO = '" & NomorPO & "' " &
                              " AND Jenis_Produk_Per_Item = '" & JenisProduk_Jasa & "' ", KoneksiDatabaseTransaksi)
        Else 'Selainnya (Jasa dan Jasa Konstruksi), tambahkan semua.
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        End If
        dr_ExecuteReader()
        NomorUrutProduk = DataTabelUtama.RowCount
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim JumlahProduk_Maksimal
        Dim JumlahBarisProduk = 0
        Do While dr.Read
            KodeProjectProduk = dr.Item("Kode_Project_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk_Dipesan = dr.Item("Jumlah_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                          " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                          " AND Nama_Produk = '" & NamaProduk & "' ",
                                          KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            JumlahProduk_Dieksekusi = 0
            Do While drTELUSUR2.Read
                JumlahProduk_Dieksekusi += drTELUSUR2.Item("Jumlah_Produk")
            Loop
            JumlahProduk_Maksimal = JumlahProduk_Dipesan - JumlahProduk_Dieksekusi
            SatuanProduk = dr.Item("Satuan_Produk")
            KeteranganProduk = Kosongan
            If JumlahProduk_Maksimal > 0 Then
                NomorUrutProduk += 1
                DataTabelUtama.Rows.Add(NomorUrutProduk, NomorPO, TanggalPO,
                                        NamaProduk, DeskripsiProduk, JumlahProduk_Maksimal, JumlahProduk_Maksimal,
                                        SatuanProduk, KodeProjectProduk, KeteranganProduk)
                JumlahBarisProduk += 1
                KondisiFormSetelahPerubahan()
            End If
        Loop
        If JumlahBarisProduk = 0 Then
            For Each row As DataGridViewRow In dgv_PO.Rows
                If row.Cells("Nomor_PO").Value = NomorPO Then
                    dgv_PO.Rows.Remove(row)
                End If
            Next
            MsgBox("Produk dengan Nomor PO ini sudah dikerjakan semua.")
            Return
        End If
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
        Else
            pesan_AdaKesalahanTeknis_Database(Kosongan)
            Return
        End If
        btn_TambahPO.Enabled = False
        cmb_JenisPPN.Text = JenisPPN
        cmb_PerlakuanPPN.Text = PerlakuanPPN
        lbl_JenisPPN.Enabled = False
        lbl_PerlakuanPPN.Enabled = False
        cmb_JenisPPN.Enabled = False
        cmb_PerlakuanPPN.Enabled = False
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
    End Sub

    Private Sub btn_SingkirkanPO_Click(sender As Object, e As EventArgs) Handles btn_SingkirkanPO.Click
        Dim NomorPO_UntukDihapus = dgv_PO.Item("Nomor_PO", BarisPO_Terseleksi).Value
        dgv_PO.Rows.Remove(dgv_PO.CurrentRow)
        BersihkanSeleksi_TabelPO()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Nomor_PO_Produk").Value = NomorPO_UntukDihapus Then
                BarisUntukDihapus.Add(row)
            End If
        Next
        For Each row As DataGridViewRow In BarisUntukDihapus
            DataTabelUtama.Rows.Remove(row)
        Next
        NomorUrutProduk = 0
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorUrutProduk += 1
            row.Cells("Nomor_Urut").Value = NomorUrutProduk
        Next
        BersihkanSeleksi_TabelProduk()
        If JumlahPO = 0 Then
            JenisProduk_Induk = Kosongan
            JenisPPN = Kosongan
            PerlakuanPPN = Kosongan
            cmb_JenisPPN.Text = Kosongan
            KontenCombo_PerlakuanPPN_Kosongan()
            lbl_JenisPPN.Enabled = True
            cmb_JenisPPN.Enabled = True
            btn_TambahPO.Enabled = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        Catatan = txt_Catatan.Text 'Pengisian ulang value untuk RichTexBox

        If JumlahPO = 0 Then
            MsgBox("Silakan input 'PO'.")
            btn_TambahPO.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            MsgBox("Silakan tambahkan data 'Barang/Jasa'.")
            Return
        End If

        If KodeCustomer = Nothing Then
            MsgBox("silakan isi data 'Customer'.")
            Return
        End If

        If YangMenerima = Kosongan Then
            TanggalDiterima = TanggalKosong
        Else 'Sudah benar pakai ELSE...! Jangan dihapus...!
            TanggalDiterima = dtp_TanggalDiterima.Value
        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        'Jika Form berfungsi untuk EDIT, maka HAPUS data sebelumnya, untuk nantinya diganti dengan data yang baru.
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_BAST " &
                                       " WHERE Angka_BAST = '" & AngkaBAST & "' ",
                                       KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_BAST")

            AksesDatabase_Transaksi(Buka)


            NomorPO_Sebelumnya = Kosongan
            StatusPO = Kosongan

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorID += 1
                NomorPO = DataTabelUtama.Item("Nomor_PO_Produk", NomorUrutProduk - 1).Value
                TanggalPO = DataTabelUtama.Item("Tanggal_PO_Produk", NomorUrutProduk - 1).Value
                KodeProjectProduk = DataTabelUtama.Item("Kode_Project_Produk", NomorUrutProduk - 1).Value
                NamaProduk = DataTabelUtama.Item("Nama_Produk", NomorUrutProduk - 1).Value
                DeskripsiProduk = DataTabelUtama.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value
                JumlahProduk_PerItem = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", NomorUrutProduk - 1).Value)
                SatuanProduk = DataTabelUtama.Item("Satuan_Produk", NomorUrutProduk - 1).Value
                KeteranganProduk = DataTabelUtama.Item("Keterangan_Produk", NomorUrutProduk - 1).Value
                QueryPenyimpanan = " INSERT INTO tbl_Penjualan_BAST VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaBAST & "', " &
                    " '" & NomorBAST & "', " &
                    " '" & TanggalFormatSimpan(TanggalBAST) & "', " &
                    " '" & JenisProduk_Induk & "', " &
                    " '" & YangMenyerahkan & "', " &
                    " '" & YangMenerima & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterima) & "', " &
                    " '" & KodeCustomer & "', " &
                    " '" & NamaCustomer & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & NomorPO & "', " &
                    " '" & TanggalFormatSimpan(TanggalPO) & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & KeteranganProduk & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & Catatan & "', " &
                    " '" & UserAktif & "', " &
                    " '" & Koreksi & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit Do
                If NomorUrutProduk > 1 And NomorPO <> NomorPO_Sebelumnya Then UpdateStatusPO()
                NomorPO_Sebelumnya = NomorPO
            Loop

            UpdateStatusPO()

            AksesDatabase_Transaksi(Tutup)

        End If

        'Jika ada perubahan Nomor BAST :
        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_EDIT And NomorBAST_Lama <> NomorBAST Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Invoice " &
                                      " SET Nomor_SJ_BAST_Produk = '" & NomorBAST & "', Tanggal_SJ_BAST_Produk = '" & TanggalFormatSimpan(TanggalBAST) & "' " &
                                      " WHERE Nomor_SJ_BAST_Produk = '" & NomorBAST_Lama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        If StatusSuntingDatabase = True Then
            'frm_BASTPenjualan.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Sub UpdateStatusPO()

        Dim NomorPO_Update = NomorPO_Sebelumnya
        Dim NamaProduk_Update

        Dim JumlahItemProdukYangBelumDikim = 0
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO = '" & NomorPO_Update & "'",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            JumlahProduk_Dipesan = dr.Item("Jumlah_Produk")
            NamaProduk_Update = dr.Item("Nama_Produk")
            JumlahProduk_Dieksekusi = 0
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO_Update & "' " &
                                         " AND Nama_Produk = '" & NamaProduk_Update & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO_Update & "' " &
                                         " AND Nama_Produk = '" & NamaProduk_Update & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            If JumlahProduk_Dipesan > JumlahProduk_Dieksekusi Then JumlahItemProdukYangBelumDikim += 1
        Loop

        'Jika masih ada produk yang belum dikirim/dikerjakan, maka Satus PO masih OPEN
        'Jika sudah terkirim semua, maka Status PO sudah CLOSED
        If JumlahItemProdukYangBelumDikim > 0 Then
            StatusPO = Status_Open
        Else
            StatusPO = Status_Closed
        End If

        cmdEDIT = New OdbcCommand(" UPDATE  tbl_Penjualan_PO " &
                                  " SET     Kontrol   = '" & StatusPO & "' " &
                                  " WHERE   Nomor_PO  = '" & NomorPO_Update & "' ",
                                  KoneksiDatabaseTransaksi)
        cmdEDIT_ExecuteNonQuery()

    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        Cetak(JenisFormCetak_BAST, AngkaBAST, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        'IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_BAST, AngkaBAST, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_CETAK
        'IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        AngkaBAST_Cetak = AngkaBAST
        TampilkanHalamanCetak_BAST()
    End Sub

End Class