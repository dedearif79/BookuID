Imports bcomm
Imports System.Data.Odbc

Public Class frm_Input_ReturPenjualan

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
    Dim KodeCustomer
    Dim NamaCustomer
    Dim Catatan
    Dim JumlahHargaKeseluruhan As Int64
    Dim Diskon_Persen As Decimal
    Dim Diskon_Rp As Int64
    Dim DPP As Int64
    Dim PPN As Int64
    Dim TotalRetur As Int64

    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim JenisProdukPerItem
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem
    Dim SatuanProduk
    Dim HargaSatuan
    Dim DiskonPerItem_Persen As Decimal
    Dim DiskonPerItem_Rp
    Dim TotalHarga

    'Variabel Tabel Index :
    Dim Baris_Terseleksi
    Dim NomorUrutProduk_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim DiskonPerItem_Persen_Terseleksi As Decimal
    Dim DiskonPerItem_Rp_Terseleksi
    Dim TotalHarga_Terseleksi

    'Variabel Tabel Invoice - Index :
    Dim BarisInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi

    Dim JumlahProduk
    Dim JumlahInvoice

    Dim HitunganHarga_Relatif As Int64 'Kenapa menggunakan istilah 'Relatif'..? Karena value Variabel ini bisa dimasukkan ke mana saja.

    Dim TarifPPN As Decimal
    Dim TarifPPN_TerakhirDitambahkan As Decimal

    Dim Koreksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Retur Penjualan"
            AngkaRetur = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Penjualan_Retur", "Angka_Retur") + 1
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Retur Penjualan"
            lbl_PPN.Text = "PPN " & TarifPPN & " %"
            Perhitungan()
            NomorRetur_Lama = NomorRetur
        End If

        Me.Text = JudulForm

        StyleTabelUtama(DataTabelUtama)
        StyleTabelUtama(dgv_Invoice)

        BeginInvoke(Sub() txt_NomorRetur.Focus())

        BeginInvoke(Sub() BersihkanSeleksi())

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
        dtp_TanggalRetur.Value = Today
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        KontenCombo_JenisPPN()
        KontenCombo_PerlakuanPPN_Kosongan()
        txt_Catatan.Text = Kosongan
        txt_JumlahHargaKeseluruhan.Text = Kosongan
        txt_Diskon_Persen.Text = Kosongan
        txt_Diskon_Rp.Text = Kosongan
        txt_DasarPengenaanPajak.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_TotalRetur.Text = Kosongan
        Kosongkan_TabelProduk()

        TarifPPN = 0
        TarifPPN_TerakhirDitambahkan = 0
        lbl_PPN.Text = "PPN"

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


    Sub Kosongkan_TabelProduk()
        DataTabelUtama.Rows.Clear()
        JumlahProduk = 0
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then Perhitungan()
    End Sub
    Sub Kosongkan_TabelInvoice()
        dgv_Invoice.Rows.Clear()
        JumlahInvoice = 0
        Kosongkan_TabelProduk()
    End Sub

    Sub BersihkanSeleksi_TabelProduk()
        Baris_Terseleksi = -1
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        DataTabelUtama.ClearSelection()
        JumlahProduk = DataTabelUtama.RowCount
    End Sub

    Sub BersihkanSeleksi_TabelInvoice()
        BarisInvoice_Terseleksi = -1
        NomorInvoice_Terseleksi = Kosongan
        btn_SingkirkanInvoice.Enabled = False
        dgv_Invoice.ClearSelection()
        JumlahInvoice = dgv_Invoice.RowCount
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        DataTabelUtama.ClearSelection()
        JumlahProduk = DataTabelUtama.RowCount
    End Sub

    Sub KondisiFormSetelahPerubahan()
        BersihkanSeleksi()
    End Sub

    Sub Perhitungan()

        JumlahHargaKeseluruhan = 0
        Diskon_Rp = 0
        HitunganHarga_Relatif = 0

        For Each row As DataGridViewRow In DataTabelUtama.Rows
            HitunganHarga_Relatif += AmbilAngka(row.Cells("Total_Harga").Value)
            JumlahHargaKeseluruhan += AmbilAngka(row.Cells("Jumlah_Harga_Per_Item").Value)
            Diskon_Rp += AmbilAngka(row.Cells("Diskon_Per_Item_Rp").Value)
        Next

        Dim RasioPPNInclude = 100 / (100 + TarifPPN)

        If JenisPPN = Kosongan Then
            txt_JumlahHargaKeseluruhan.Text = Kosongan
            txt_Diskon_Persen.Text = Kosongan
            txt_Diskon_Rp.Text = Kosongan
            txt_DasarPengenaanPajak.Text = Kosongan
            txt_PPN.Text = Kosongan
            txt_TotalRetur.Text = Kosongan
        End If

        If JenisPPN = JenisPPN_Exclude Or JenisPPN = JenisPPN_NonPPN Then
            DPP = HitunganHarga_Relatif
            txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
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
                    JumlahHargaKeseluruhan = DPP + Diskon_Rp
                    If PerlakuanPPN = PerlakuanPPN_Dibayar Then TotalRetur = DPP + PPN
                    If PerlakuanPPN = PerlakuanPPN_Dipungut Then TotalRetur = DPP
                    If PerlakuanPPN = PerlakuanPPN_TidakDipungut Then TotalRetur = DPP
                End If
                '---------------------------------------------------------
                txt_Diskon_Rp.Text = Diskon_Rp
                txt_JumlahHargaKeseluruhan.Text = JumlahHargaKeseluruhan
        End Select

        txt_DasarPengenaanPajak.Text = DPP

        If PerlakuanPPN = Kosongan And JenisPPN <> JenisPPN_NonPPN Then
            txt_PPN.Text = Kosongan
            txt_TotalRetur.Text = Kosongan
        Else
            txt_PPN.Text = PPN
            txt_TotalRetur.Text = TotalRetur
        End If

        If Diskon_Rp = 0 Then
            lbl_Diskon.Enabled = False
            txt_Diskon_Rp.Enabled = False
        Else
            lbl_Diskon.Enabled = True
            txt_Diskon_Rp.Enabled = True
        End If

        JumlahProduk = DataTabelUtama.RowCount

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

    Private Sub txt_NomorRetur_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorRetur.TextChanged
        NomorRetur = txt_NomorRetur.Text
    End Sub

    Private Sub dtp_TanggalRetur_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalRetur.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalRetur)
        TanggalRetur = dtp_TanggalRetur.Value
        KondisiFormSetelahPerubahan()
    End Sub

    'Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
    '    NomorInvoice = txt_NomorInvoice.Text
    '    KondisiFormSetelahPerubahan()
    'End Sub
    'Private Sub txt_NomorInvoice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorInvoice.KeyPress
    '    KunciTotalInputan(sender, e)
    'End Sub
    'Private Sub txt_NomorInvoice_Click(sender As Object, e As EventArgs) Handles txt_NomorInvoice.Click
    '    btn_PilihInvoice_Click(sender, e)
    'End Sub

    'Private Sub btn_PilihInvoice_Click(sender As Object, e As EventArgs) Handles btn_PilihInvoice.Click
    '    frm_ListInvoice.ResetForm()
    '    frm_ListInvoice.FungsiForm = frm_ListInvoice.FungsiForm_InvoicePenjualan
    '    If txt_NomorInvoice.Text = Nothing Then
    '        frm_ListInvoice.BersihkanSeleksi()
    '    Else
    '        frm_ListInvoice.NomorInvoice_Terseleksi = txt_NomorInvoice.Text
    '        frm_ListInvoice.TanggalInvoice_Terseleksi = AmbilTanggal(dtp_TanggalInvoice.Value)
    '        frm_ListInvoice.KodeProject_Terseleksi = txt_KodeProject.Text
    '        frm_ListInvoice.KodeMitra_Terseleksi = txt_KodeCustomer.Text
    '        frm_ListInvoice.NamaMitra_Terseleksi = txt_NamaCustomer.Text
    '        frm_ListInvoice.AlamatMitra_Terseleksi = txt_AlamatCustomer.Text
    '    End If
    '    frm_ListInvoice.ShowDialog()
    '    txt_NomorInvoice.Text = frm_ListInvoice.NomorInvoice_Terseleksi
    '    dtp_TanggalInvoice.Text = frm_ListInvoice.TanggalInvoice_Terseleksi
    '    txt_KodeProject.Text = frm_ListInvoice.KodeProject_Terseleksi
    '    txt_KodeCustomer.Text = frm_ListInvoice.KodeMitra_Terseleksi
    '    txt_NamaCustomer.Text = frm_ListInvoice.NamaMitra_Terseleksi
    '    txt_AlamatCustomer.Text = frm_ListInvoice.AlamatMitra_Terseleksi
    'End Sub

    'Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
    '    TanggalInvoice = dtp_TanggalInvoice.Value
    '    KondisiFormSetelahPerubahan()
    'End Sub
    'Private Sub dtp_TanggalInvoice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtp_TanggalInvoice.KeyPress
    '    'KunciTotalInputan(sender, e)
    'End Sub

    'Private Sub txt_KodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeProject.TextChanged
    '    KodeProject = txt_KodeProject.Text
    '    KondisiFormSetelahPerubahan()
    'End Sub
    'Private Sub txt_KodeProject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeProject.KeyPress
    '    KunciTotalInputan(sender, e)
    'End Sub

    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        If KodeCustomer = Kosongan Then
            btn_TambahInvoice.Enabled = False
        Else
            btn_TambahInvoice.Enabled = True
        End If
        Kosongkan_TabelInvoice()
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
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeCustomer.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaCustomer.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
        'If ProsesResetForm = False And ProsesLoadingForm = False And KodeCustomer <> Kosongan Then btn_TambahInvoice_Click(sender, e)
    End Sub

    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
        KondisiFormSetelahPerubahan()
    End Sub
    Private Sub txt_NamaCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    'Private Sub txt_AlamatCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_AlamatCustomer.TextChanged
    'End Sub
    'Private Sub txt_AlamatCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AlamatCustomer.KeyPress
    '    KunciTotalInputan(sender, e)
    'End Sub

    Private Sub cmb_JenisPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPN.TextChanged
        JenisPPN = cmb_JenisPPN.Text
        If JenisPPN = JenisPPN_NonPPN Then
            KontenCombo_PerlakuanPPN_NonPPN()
        Else
            KontenCombo_PerlakuanPPN_AdaPPN()
        End If
        If ProsesResetForm = False And ProsesIsiValueForm = False Then Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub cmb_PerlakuanPPN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.SelectedIndexChanged
    End Sub
    Private Sub cmb_PerlakuanPPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_PerlakuanPPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_PerlakuanPPN_TextChanged(sender As Object, e As EventArgs) Handles cmb_PerlakuanPPN.TextChanged
        PerlakuanPPN = cmb_PerlakuanPPN.Text
        If ProsesResetForm = False And ProsesIsiValueForm = False And ProsesLoadingForm = False Then Perhitungan()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrutProduk_Terseleksi = DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk_Per_item", Baris_Terseleksi).Value
        NamaProduk_Terseleksi = DataTabelUtama.Item("Nama_Produk", Baris_Terseleksi).Value
        DeskripsiProduk_Terseleksi = DataTabelUtama.Item("Deskripsi_Produk", Baris_Terseleksi).Value
        JumlahProduk_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", Baris_Terseleksi).Value)
        SatuanProduk_Terseleksi = DataTabelUtama.Item("Satuan_Produk", Baris_Terseleksi).Value
        HargaSatuan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", Baris_Terseleksi).Value)
        DiskonPerItem_Persen_Terseleksi = GantiTeks(DataTabelUtama.Item("Diskon_Per_Item_Persen", Baris_Terseleksi).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonPerItem_Rp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Per_Item_Rp", Baris_Terseleksi).Value)
        TotalHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Total_Harga", Baris_Terseleksi).Value)

        If Baris_Terseleksi >= 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick

        If DataTabelUtama.RowCount = 0 Then Return

        If Baris_Terseleksi >= 0 Then
            btn_Edit_Click(sender, e)
        End If

    End Sub

    'Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
    '    frm_InputProduk_Nota.ResetForm()
    '    frm_InputProduk_Nota.txt_NomorUrut.Text = JumlahProduk + 1
    '    frm_InputProduk_Nota.FungsiForm = FungsiForm_TAMBAH
    '    frm_InputProduk_Nota.JalurMasuk = Form_INPUTRETURPENJUALAN
    '    frm_InputProduk_Nota.ShowDialog()
    '    Perhitungan()
    '    BersihkanSeleksi()
    'End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputProduk_Nota.ResetForm()
        frm_InputProduk_Nota.FungsiForm = FungsiForm_EDIT
        frm_InputProduk_Nota.JenisProduk_Induk = JenisProduk_Induk
        frm_InputProduk_Nota.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        frm_InputProduk_Nota.cmb_JenisProduk.Text = JenisProduk_Terseleksi
        frm_InputProduk_Nota.txt_NamaProduk.Text = NamaProduk_Terseleksi
        frm_InputProduk_Nota.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        frm_InputProduk_Nota.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        frm_InputProduk_Nota.txt_Satuan.Text = SatuanProduk_Terseleksi
        frm_InputProduk_Nota.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        frm_InputProduk_Nota.txt_DiskonPerItem_Persen.Text = DiskonPerItem_Persen_Terseleksi
        frm_InputProduk_Nota.JalurMasuk = Form_INPUTRETURPENJUALAN
        BeginInvoke(Sub() frm_InputProduk_Nota.txt_JumlahProduk.Focus())
        frm_InputProduk_Nota.ShowDialog()
        Perhitungan()
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
        Perhitungan()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub txt_Catatan_TextChanged(sender As Object, e As EventArgs) Handles txt_Catatan.TextChanged
        Catatan = txt_Catatan.Text
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub dgv_Invoice_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Invoice.CellContentClick
    End Sub
    Private Sub dgv_Invoice_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_Invoice.ColumnHeaderMouseClick
        BersihkanSeleksi_TabelInvoice()
    End Sub
    Private Sub dgv_Invoice_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Invoice.CellClick
        If dgv_Invoice.RowCount = 0 Then Return
        BarisInvoice_Terseleksi = dgv_Invoice.CurrentRow.Index
        NomorInvoice_Terseleksi = dgv_Invoice.Item("Nomor_Invoice", BarisInvoice_Terseleksi).Value
        If BarisInvoice_Terseleksi >= 0 Then
            btn_SingkirkanInvoice.Enabled = True
        Else
            btn_SingkirkanInvoice.Enabled = False
        End If
    End Sub


    Private Sub btn_TambahInvoice_Click(sender As Object, e As EventArgs) Handles btn_TambahInvoice.Click

        win_ListInvoice = New wpfWin_ListInvoice
        win_ListInvoice.ResetForm()
        win_ListInvoice.BersihkanSeleksi()
        win_ListInvoice.FungsiForm = win_ListInvoice.FungsiForm_InvoicePenjualan
        win_ListInvoice.cmb_Mitra.Text = NamaCustomer
        win_ListInvoice.lbl_FilterMitra.IsEnabled = False
        win_ListInvoice.cmb_Mitra.IsEnabled = False
        win_ListInvoice.JenisPPN = JenisPPN
        win_ListInvoice.PerlakuanPPN = PerlakuanPPN
        win_ListInvoice.JalurMasuk = Form_INPUTINVOICEPENJUALAN
        win_ListInvoice.PilihYangSudahDijurnal = True
        win_ListInvoice.ShowDialog()                                             '<---- Buka Form List Invoice
        NomorInvoice = win_ListInvoice.NomorInvoice_Terseleksi
        If NomorInvoice = Kosongan Then Return
        TanggalInvoice = win_ListInvoice.TanggalInvoice_Terseleksi
        KodeProjectProduk = win_ListInvoice.KodeProject_Terseleksi
        Dim TelusurInvoice = Kosongan
        'Cegah Input Invoice lebih dari satu kali :
        For Each row As DataGridViewRow In dgv_Invoice.Rows
            TelusurInvoice = row.Cells("Nomor_Invoice").Value
            If TelusurInvoice = NomorInvoice Then
                MsgBox("Nomor Invoice ini sudah diinput..!")
                Return
            End If
        Next
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = DataTabelUtama.RowCount
        Dim PenambahanProdukPadaSesiIni = 0
        Do While dr.Read
            NomorUrutProduk += 1
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
            If TarifPPN <> TarifPPN_TerakhirDitambahkan And JumlahInvoice > 0 Then
                PesanPeringatan("Invoice ditolak..!" & Enter2Baris & "Tarif PPN pada Invoice yang Anda tambahkan tidak sama dengan Invoice sebelumnya.")
                Return
            End If
            TarifPPN_TerakhirDitambahkan = TarifPPN
            lbl_PPN.Text = "PPN " & TarifPPN & " %"
            Dim TotalHarga_PerItem As Int64 = JumlahHarga_PerItem - DiskonPerItem_Rp
            If JenisProdukPerItem = JenisProduk_Barang Then
                Dim SJ_Telusur = dr.Item("Nomor_SJ_BAST_Produk")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                         " WHERE Nama_Produk = '" & NamaProduk & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim NomorPO_Telusur = Kosongan
                If drTELUSUR.HasRows Then NomorPO_Telusur = drTELUSUR.Item("Nomor_PO_Produk")
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                          " WHERE Nomor_PO = '" & NomorPO_Telusur & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then KodeProjectProduk = drTELUSUR2.Item("Kode_Project_Produk")
                DataTabelUtama.Rows.Add(NomorUrutProduk, JenisProdukPerItem, NomorInvoice, TanggalInvoice,
                                        NamaProduk, DeskripsiProduk,
                                        JumlahProduk_PerItem, SatuanProduk, HargaSatuan, JumlahHarga_PerItem,
                                        DiskonPerItem_Persen, DiskonPerItem_Rp, TotalHarga_PerItem, KodeProjectProduk)              '<-- Penambahan Baris Produk
                PenambahanProdukPadaSesiIni += 1
            End If
        Loop
        If PenambahanProdukPadaSesiIni = 0 Then 'Jika tidak ada penambahan produk (akibat penyortiran), maka hapus baris terakhir pada Tabel Invoice.
            If dgv_Invoice.RowCount > 0 Then
                'dgv_Invoice.Rows.RemoveAt(dgv_Invoice.RowCount - 1)
                'BersihkanSeleksi_TabelProduk()
                MsgBox("Tidak ada produk yang bisa ditambahkan pada Invoice ini.")
            End If
        Else
            dgv_Invoice.Rows.Add(NomorInvoice, TanggalInvoice, KodeProjectProduk)        '<-- Penambahan Baris Invoice
            BersihkanSeleksi_TabelInvoice()
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
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
            cmb_JenisPPN.Text = JenisPPN
            cmb_PerlakuanPPN.Text = PerlakuanPPN
            lbl_JenisPPN.Enabled = False
            lbl_PerlakuanPPN.Enabled = False
            cmb_JenisPPN.Enabled = False
            cmb_PerlakuanPPN.Enabled = False
            Perhitungan()
        End If
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Private Sub btn_SingkirkanInvoice_Click(sender As Object, e As EventArgs) Handles btn_SingkirkanInvoice.Click
        Dim NomorInvoice_UntukDihapus = dgv_Invoice.Item("Nomor_Invoice", BarisInvoice_Terseleksi).Value
        dgv_Invoice.Rows.Remove(dgv_Invoice.CurrentRow)
        BersihkanSeleksi_TabelInvoice()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Nomor_Invoice_Produk").Value = NomorInvoice_UntukDihapus Then
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
        If JumlahInvoice = 0 Then
            JenisPPN = Kosongan
            PerlakuanPPN = Kosongan
            cmb_JenisPPN.Text = Kosongan
            KontenCombo_PerlakuanPPN_Kosongan()
            lbl_JenisPPN.Enabled = True
            cmb_JenisPPN.Enabled = True
            btn_TambahInvoice.Enabled = True
        End If
        Perhitungan()
    End Sub

    Private Sub txt_JumlahHargaKeseluruhan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHargaKeseluruhan.TextChanged
        JumlahHargaKeseluruhan = AmbilAngka(txt_JumlahHargaKeseluruhan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHargaKeseluruhan)
    End Sub
    Private Sub txt_JumlahHargaKeseluruhan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHargaKeseluruhan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    'Private Sub txt_Diskon_Persen_TextChanged(sender As Object, e As EventArgs) Handles txt_Diskon_Persen.TextChanged
    '    If txt_Diskon_Persen.Text = "," Then
    '        txt_Diskon_Persen.Text = Kosongan
    '        Return
    '    End If
    '    If txt_Diskon_Persen.Text = Kosongan Then
    '        Diskon_Persen = 0
    '    Else
    '        Diskon_Persen = txt_Diskon_Persen.Text
    '    End If
    '    If Diskon_Persen > 100 Then
    '        MsgBox("Silakan isi kolom 'Diskon' dengan benar.")
    '        txt_Diskon_Persen.Text = Kosongan
    '        txt_Diskon_Persen.Focus()
    '        Return
    '    End If
    '    KondisiFormSetelahPerubahan()
    '    If ProsesResetForm = False And ProsesIsiValueForm = False Then Perhitungan()
    'End Sub

    'Private Sub txt_Diskon_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Diskon_Persen.KeyPress
    '    HanyaBolehInputAngkaDesimalPlus(sender, e)
    'End Sub

    Private Sub txt_Diskon_Rp_TextChanged(sender As Object, e As EventArgs) Handles txt_Diskon_Rp.TextChanged
        Diskon_Rp = AmbilAngka(txt_Diskon_Rp.Text)
        PemecahRibuanUntukTextBox(txt_Diskon_Rp)
    End Sub
    Private Sub txt_Diskon_Rp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Diskon_Rp.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DasarPengenaanPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_DasarPengenaanPajak.TextChanged
        DPP = AmbilAngka(txt_DasarPengenaanPajak.Text)
        PemecahRibuanUntukTextBox(txt_DasarPengenaanPajak)
    End Sub
    Private Sub txt_DasarPengenaanPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DasarPengenaanPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As EventArgs) Handles txt_PPN.TextChanged
        PPN = AmbilAngka(txt_PPN.Text)
        PemecahRibuanUntukTextBox(txt_PPN)
    End Sub
    Private Sub txt_PPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPN.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalRetur_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalRetur.TextChanged
        TotalRetur = AmbilAngka(txt_TotalRetur.Text)
        PemecahRibuanUntukTextBox(txt_TotalRetur)
    End Sub
    Private Sub txt_TotalRetur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalRetur.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        Catatan = txt_Catatan.Text              'Pengisian ulang value untuk RichTexBox

        If NomorRetur = Kosongan Then
            MsgBox("Silakan isi kolom 'Nomor Retur'.")
            txt_NomorRetur.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            MsgBox("Silakan tambahkan data 'Barang/Jasa'.")
            btn_TambahInvoice.Focus()
            Return
        End If

        If KodeCustomer = Nothing Then
            MsgBox("silakan isi data 'Customer'.")
            Return
        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            'Hapus Data Retur Penjualan :
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_Retur " &
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

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Penjualan_Retur")

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorID += 1
                JenisProdukPerItem = DataTabelUtama.Item("Jenis_Produk_Per_Item", NomorUrutProduk - 1).Value
                NomorInvoice = DataTabelUtama.Item("Nomor_Invoice_Produk", NomorUrutProduk - 1).Value
                TanggalInvoice = DataTabelUtama.Item("Tanggal_Invoice_Produk", NomorUrutProduk - 1).Value
                KodeProjectProduk = DataTabelUtama.Item("Kode_Project_Produk", NomorUrutProduk - 1).Value
                NamaProduk = DataTabelUtama.Item("Nama_Produk", NomorUrutProduk - 1).Value
                DeskripsiProduk = DataTabelUtama.Item("Deskripsi_Produk", NomorUrutProduk - 1).Value
                JumlahProduk_PerItem = AmbilAngka(DataTabelUtama.Item("Jumlah_Produk", NomorUrutProduk - 1).Value)
                SatuanProduk = DataTabelUtama.Item("Satuan_Produk", NomorUrutProduk - 1).Value
                HargaSatuan = AmbilAngka(DataTabelUtama.Item("Harga_Satuan", NomorUrutProduk - 1).Value)
                DiskonPerItem_Persen = Microsoft.VisualBasic.Replace(DataTabelUtama.Item("Diskon_Per_Item_Persen", NomorUrutProduk - 1).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
                TotalHarga = AmbilAngka(DataTabelUtama.Item("Total_Harga", NomorUrutProduk - 1).Value)
                QueryPenyimpanan = " INSERT INTO tbl_Penjualan_Retur VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaRetur & "', " &
                    " '" & NomorRetur & "', " &
                    " '" & TanggalFormatSimpan(TanggalRetur) & "', " &
                    " '" & KodeCustomer & "', " &
                    " '" & NamaCustomer & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & JenisProdukPerItem & "', " &
                    " '" & NomorInvoice & "', " &
                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & HargaSatuan & "', " &
                    " '" & DesimalFormatSimpan(DiskonPerItem_Persen) & "', " &
                    " '" & TotalHarga & "', " &
                    " '" & JumlahHargaKeseluruhan & "', " &
                    " '" & Diskon_Rp & "', " &
                    " '" & DPP & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
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
            Do While NomorUrutProduk < JumlahProduk
                NomorUrutProduk += 1
                NomorInvoice = DataTabelUtama.Item("Nomor_Invoice_Produk", NomorUrutProduk - 1).Value
                If NomorUrutProduk > 1 And NomorInvoice <> NomorInvoiceSebelumnya Then
                    cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Invoice SET " &
                                          " Retur_DPP           = '" & ReturDPPPerInvoice & "', " &
                                          " Retur_PPN           = '" & ReturPPNPerInvoice & "' " &
                                          " WHERE Nomor_Invoice = '" & NomorInvoiceSebelumnya & "' ", KoneksiDatabaseTransaksi)
                    cmd_ExecuteNonQuery()
                    ReturDPPPerInvoice = 0
                    ReturPPNPerInvoice = 0
                End If
                ReturDPPPerInvoice += AmbilAngka(DataTabelUtama.Item("Total_Harga", NomorUrutProduk - 1).Value)
                ReturPPNPerInvoice = ReturDPPPerInvoice * Persen(TarifPPN)
                NomorInvoiceSebelumnya = NomorInvoice
            Loop
            cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Invoice SET " &
                                  " Retur_DPP           = '" & ReturDPPPerInvoice & "', " &
                                  " Retur_PPN           = '" & ReturPPNPerInvoice & "' " &
                                  " WHERE Nomor_Invoice = '" & NomorInvoiceSebelumnya & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        '====================================================================================
        'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                          |
        '====================================================================================

        If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0

        If StatusSuntingDatabase = True And JenisTahunBuku = JenisTahunBuku_NORMAL Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
            jur_JenisJurnal = JenisJurnal_ReturPenjualan
            jur_NomorPO = Kosongan
            jur_KodeProject = KodeProjectJurnal
            jur_Referensi = Kosongan
            jur_TanggalInvoice = TanggalInvoiceJurnal 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
            jur_NomorInvoice = NomorInvoiceJurnal
            jur_NomorFakturPajak = NomorRetur
            jur_KodeLawanTransaksi = KodeCustomer
            jur_NamaLawanTransaksi = NamaCustomer
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            'Simpan Jurnal :
            ___jurDebet(KodeTautanCOA_ReturPenjualan, DPP)
            ___jurDebet(KodeTautanCOA_PPNKeluaran, PPN)
            _______jurKredit(KodeTautanCOA_PiutangUsaha_NonAfiliasi, TotalRetur)

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                jur_StatusPenyimpananJurnal_Lengkap = True
            Else
                jur_StatusPenyimpananJurnal_Lengkap = False
            End If

            ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        End If

        If StatusSuntingDatabase = True Then
            frm_ReturPenjualan.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

End Class
