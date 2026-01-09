Imports bcomm

Public Class frm_InputProduk_Nota

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
    Public HargaSatuan
    Public DiskonPerItem_Persen As Decimal
    Public DiskonPerItem_Rp As Int64 '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
    Public JumlahHarga
    Public TotalHarga
    Public Peruntukan
    Public KodeProject

    Dim KunciInputanKolom

    Dim Tabel As DataGridView

    Public Proses As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Select Case JalurMasuk
            Case Form_INPUTPOPEMBELIAN
                Tabel = frm_Input_POPembelian.DataTabelUtama
            Case Form_INPUTINVOICEPEMBELIAN
                Tabel = frm_Input_InvoicePembelian.DataTabelUtama
            Case Form_INPUTRETURPEMBELIAN
                Tabel = frm_Input_ReturPembelian.DataTabelUtama
            Case Form_INPUTPOPENJUALAN
                Tabel = frm_Input_POPenjualan.DataTabelUtama
            Case Form_INPUTINVOICEPENJUALAN
                Tabel = frm_Input_InvoicePenjualan.DataTabelUtama
            Case Form_INPUTRETURPENJUALAN
                Tabel = frm_Input_ReturPenjualan.DataTabelUtama
            Case Else
                PesanUntukProgrammer("Jalur masuk form belum ditentukan...!!!")
                Return
        End Select

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Produk"
            btn_Tambahkan.Text = "Tambahkan"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Produk"
            btn_Tambahkan.Text = "Perbarui"
        End If

        Me.Text = JudulForm

        If JalurMasuk <> Form_INPUTPOPENJUALAN _
            And JalurMasuk <> Form_INPUTPOPEMBELIAN _
            And Not (JalurMasuk = Form_INPUTINVOICEPEMBELIAN And frm_Input_InvoicePembelian.NP <> "N") _
            And Not (JalurMasuk = Form_INPUTINVOICEPENJUALAN And frm_Input_InvoicePenjualan.NP <> "N") _
            And InvoiceDenganPO = True _
            Then
            KunciInputanKolom = True
        Else
            KunciInputanKolom = False
        End If

        If JalurMasuk = Form_INPUTRETURPEMBELIAN Or JalurMasuk = Form_INPUTRETURPENJUALAN Then
            KunciInputanKolom = False
            lbl_NomorUrut.Enabled = False
            txt_NomorUrut.Enabled = False
            cmb_JenisProduk.Enabled = False
            lbl_NamaProduk.Enabled = False
            txt_NamaProduk.Enabled = False
            lbl_Deskripsi.Enabled = False
            txt_DeskripsiProduk.Enabled = False
            lbl_JumlahProduk.Text = "Jumlah yang Diretur"
            lbl_Satuan.Enabled = False
            txt_Satuan.Enabled = False
            lbl_keteranganSatuan.Enabled = False
            lbl_HargaSatuan.Enabled = False
            txt_HargaSatuan.Enabled = False
            lbl_JumlahHarga.Enabled = False
            txt_JumlahHarga.Enabled = False
            lbl_Diskon.Enabled = False
            txt_DiskonPerItem_Persen.Enabled = False
            lbl_Persen.Enabled = False
            txt_DiskonPerItem_Rp.Enabled = False
            lbl_TotalHarga.Enabled = False
            txt_TotalHarga.Enabled = False
            lbl_Persen.Enabled = False
            cmb_Peruntukan.Enabled = False
            lbl_KodeProject.Enabled = False
            txt_KodeProject.Enabled = False
            btn_PilihKodeProject.Enabled = False
            lbl_Peruntukan.Visible = False
            cmb_Peruntukan.Visible = False
        End If

        KontenCombo_JenisProduk()
        KetersediaanKolom_KodeProject()

        BeginInvoke(Sub() txt_NamaProduk.Focus())

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        Proses = False

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
        txt_NamaProduk.Enabled = True
        txt_NamaProduk.Text = Kosongan
        txt_DeskripsiProduk.Text = Kosongan
        lbl_JumlahProduk.Text = "Jumlah"
        txt_JumlahProduk.Text = Kosongan
        txt_Satuan.Text = Kosongan
        txt_HargaSatuan.Text = Kosongan
        txt_DiskonPerItem_Persen.Text = Kosongan
        txt_DiskonPerItem_Rp.Text = Kosongan
        txt_TotalHarga.Text = Kosongan
        cmb_JenisProduk.Text = Kosongan 'Ini Penting...!!! Jangan dihapus...!!!
        KontenCombo_Peruntukan()
        txt_KodeProject.Text = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KetersediaanSemuaObjek()
        lbl_NomorUrut.Enabled = True
        txt_NomorUrut.Enabled = True
        cmb_JenisProduk.Enabled = True
        lbl_NamaProduk.Enabled = True
        lbl_Deskripsi.Enabled = True
        txt_DeskripsiProduk.Enabled = True
        txt_NamaProduk.Enabled = True
        lbl_JumlahProduk.Enabled = True
        txt_JumlahProduk.Enabled = True
        lbl_Satuan.Enabled = True
        txt_Satuan.Enabled = True
        lbl_keteranganSatuan.Enabled = True
        lbl_HargaSatuan.Enabled = True
        txt_HargaSatuan.Enabled = True
        lbl_JumlahHarga.Enabled = True
        txt_JumlahHarga.Enabled = True
        lbl_Diskon.Enabled = True
        txt_DiskonPerItem_Persen.Enabled = True
        lbl_Persen.Enabled = True
        txt_DiskonPerItem_Rp.Enabled = True
        lbl_TotalHarga.Enabled = True
        txt_TotalHarga.Enabled = True
        lbl_Persen.Enabled = True
        cmb_Peruntukan.Enabled = True
        lbl_KodeProject.Enabled = True
        txt_KodeProject.Enabled = True
        btn_PilihKodeProject.Enabled = True
    End Sub

    Sub KontenCombo_JenisProduk()
        cmb_JenisProduk.Items.Clear()
        If JenisProduk_Induk <> Kosongan Then
            If JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                cmb_JenisProduk.Items.Add(JenisProduk_Barang)
                cmb_JenisProduk.Items.Add(JenisProduk_Jasa)
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then cmb_JenisProduk.Items.Add(JenisProduk_BarangDanJasa)
                If FungsiForm = FungsiForm_TAMBAH Then cmb_JenisProduk.Text = Kosongan
                If FungsiForm = FungsiForm_EDIT Then cmb_JenisProduk.Text = JenisProduk_PerItem
                cmb_JenisProduk.Enabled = True
                lbl_NamaProduk.Text = "Nama Barang/Jasa"
            Else
                cmb_JenisProduk.Text = JenisProduk_Induk
                cmb_JenisProduk.Enabled = False
                If JenisProduk_Induk = JenisProduk_Barang Then lbl_NamaProduk.Text = "Nama Barang"
                If JenisProduk_Induk = JenisProduk_Jasa Then lbl_NamaProduk.Text = "Nama Jasa"
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then lbl_NamaProduk.Text = "Nama Barang/Jasa"
            End If
        End If
    End Sub

    Sub KontenCombo_Peruntukan()
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_Project)
        cmb_Peruntukan.Items.Add(Peruntukan_NonProject)
        cmb_Peruntukan.Text = Kosongan
        lbl_Peruntukan.Enabled = True
        lbl_Peruntukan.Visible = True
        cmb_Peruntukan.Enabled = True
        cmb_Peruntukan.Visible = True
    End Sub

    Sub Perhitungan()

        JumlahHarga = JumlahProduk * HargaSatuan

        DiskonPerItem_Rp = JumlahHarga * (DiskonPerItem_Persen / 100)

        TotalHarga = JumlahHarga - DiskonPerItem_Rp

        txt_JumlahHarga.Text = JumlahHarga
        txt_DiskonPerItem_Rp.Text = DiskonPerItem_Rp
        txt_TotalHarga.Text = TotalHarga

    End Sub

    Private Sub txt_NomorUrut_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorUrut.TextChanged
        NomorUrutProduk = AmbilAngka(txt_NomorUrut.Text)
    End Sub
    Private Sub txt_NomorUrut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorUrut.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_JenisProduk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisProduk.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisProduk_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk.TextChanged
        JenisProduk_PerItem = cmb_JenisProduk.Text
        'If JenisProduk_PerItem = JenisProduk_Jasa And FungsiForm = FungsiForm_TAMBAH Then
        '    For Each row As DataGridViewRow In Tabel.Rows
        '        If row.Cells("Jenis_Produk_Per_Item").Value = JenisProduk_Jasa Then
        '            txt_NamaProduk.Enabled = False
        '            txt_NamaProduk.Text = row.Cells("Nama_Produk").Value
        '        End If
        '    Next
        'Else
        '    txt_NamaProduk.Enabled = True
        '    'txt_NamaProduk.Text = Kosongan '(Ini kenapa harus dikosongkan ya..? Lupa lagi euy...! Sementara dinonaktifkan dulu dah, sampai inget lagi alasannya apa.)
        'End If
    End Sub

    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub
    Private Sub txt_NamaProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaProduk.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            BukaKunciInputan(sender, e)
        End If
    End Sub

    Private Sub txt_DeskripsiProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_DeskripsiProduk.TextChanged
        DeskripsiProduk = txt_DeskripsiProduk.Text
    End Sub
    Private Sub txt_DeskripsiProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DeskripsiProduk.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            BukaKunciInputan(sender, e)
        End If
    End Sub

    Private Sub txt_JumlahProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahProduk.TextChanged
        JumlahProduk = AmbilAngka(txt_JumlahProduk.Text)
        PemecahRibuanUntukTextBox(txt_JumlahProduk)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahProduk.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaPlus(sender, e)
        End If
    End Sub

    Private Sub txt_Satuan_TextChanged(sender As Object, e As EventArgs) Handles txt_Satuan.TextChanged
        SatuanProduk = txt_Satuan.Text
    End Sub
    Private Sub txt_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Satuan.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            BukaKunciInputan(sender, e)
        End If
    End Sub

    Private Sub txt_HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles txt_HargaSatuan.TextChanged
        HargaSatuan = AmbilAngka(txt_HargaSatuan.Text)
        PemecahRibuanUntukTextBox(txt_HargaSatuan)
        Perhitungan()
    End Sub
    Private Sub txt_HargaSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_HargaSatuan.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaPlus(sender, e)
        End If
    End Sub

    Private Sub txt_JumlahHarga_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahHarga.TextChanged
        JumlahHarga = AmbilAngka(txt_JumlahHarga.Text)
        PemecahRibuanUntukTextBox(txt_JumlahHarga)
    End Sub
    Private Sub txt_JumlahHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahHarga.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DiskonPerItem_Persen_TextChanged(sender As Object, e As EventArgs) Handles txt_DiskonPerItem_Persen.TextChanged
        If txt_DiskonPerItem_Persen.Text = "," Then
            txt_DiskonPerItem_Persen.Text = Kosongan
            Return
        End If
        If txt_DiskonPerItem_Persen.Text = Kosongan Then
            DiskonPerItem_Persen = 0
        Else
            DiskonPerItem_Persen = txt_DiskonPerItem_Persen.Text
        End If
        If DiskonPerItem_Persen > 100 Then
            Pesan_Peringatan("Silakan isi kolom 'Diskon' dengan benar.")
            txt_DiskonPerItem_Persen.Text = Kosongan
            txt_DiskonPerItem_Persen.Focus()
            Return
        End If
        Perhitungan()
    End Sub
    Private Sub txt_DiskonPerItem_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DiskonPerItem_Persen.KeyPress
        If KunciInputanKolom = True Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaDesimalPlus(sender, e)
        End If
    End Sub

    Private Sub txt_DiskonPerItem_Rp_TextChanged(sender As Object, e As EventArgs) Handles txt_DiskonPerItem_Rp.TextChanged
        DiskonPerItem_Rp = AmbilAngka(txt_DiskonPerItem_Rp.Text)
        PemecahRibuanUntukTextBox(txt_DiskonPerItem_Rp)
    End Sub
    Private Sub txt_DiskonPerItem_Rp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DiskonPerItem_Rp.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalHarga_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalHarga.TextChanged
        TotalHarga = AmbilAngka(txt_TotalHarga.Text)
        PemecahRibuanUntukTextBox(txt_TotalHarga)
    End Sub
    Private Sub txt_TotalHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalHarga.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_Peruntukan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Peruntukan.SelectedIndexChanged
    End Sub
    Private Sub cmb_Peruntukan_TextChanged(sender As Object, e As EventArgs) Handles cmb_Peruntukan.TextChanged
        Peruntukan = cmb_Peruntukan.Text
        KetersediaanKolom_KodeProject()
    End Sub
    Private Sub cmb_Peruntukan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Peruntukan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_KodeProject_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeProject.TextChanged
        KodeProject = txt_KodeProject.Text
    End Sub
    Private Sub txt_KodeProject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeProject.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihKodeProject_Click(sender As Object, e As EventArgs) Handles btn_PilihKodeProject.Click
        frm_ListDataProject.ResetForm()
        If txt_KodeProject.Text <> Kosongan Then
            frm_ListDataProject.KodeProject_Terseleksi = txt_KodeProject.Text
        End If
        frm_ListDataProject.ShowDialog()
        txt_KodeProject.Text = frm_ListDataProject.KodeProject_Terseleksi
    End Sub

    Sub KetersediaanKolom_KodeProject()
        If Peruntukan = Peruntukan_Project Then
            lbl_KodeProject.Visible = True
            txt_KodeProject.Visible = True
            btn_PilihKodeProject.Visible = True
        Else
            lbl_KodeProject.Visible = False
            txt_KodeProject.Visible = False
            txt_KodeProject.Text = Kosongan
            btn_PilihKodeProject.Visible = False
        End If
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As EventArgs) Handles btn_Tambahkan.Click

        If JenisProduk_PerItem = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Jenis Produk'.")
            cmb_JenisProduk.Focus()
            Return
        End If

        If NamaProduk = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Nama Barang/Jasa'.")
            txt_NamaProduk.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Jumlah'.")
            txt_JumlahProduk.Focus()
            Return
        End If

        If HargaSatuan = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Harga Satuan'.")
            txt_HargaSatuan.Focus()
            Return
        End If

        If cmb_Peruntukan.Visible = True And Peruntukan = Kosongan Then
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
                Tabel.Rows.Add(NomorUrutProduk, JenisProduk_PerItem,
                               NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                               JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHarga, KodeProject)
            End If
            If JalurMasuk = Form_INPUTPOPENJUALAN Then
                Tabel.Rows.Add(NomorUrutProduk, JenisProduk_PerItem,
                               NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                               JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHarga, KodeProject)
            End If
            If JalurMasuk = Form_INPUTINVOICEPEMBELIAN Then
                Tabel.Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, Kosongan, COA_PerProduk,
                               NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                               JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHarga, KodeProject)
            End If
            If JalurMasuk = Form_INPUTINVOICEPENJUALAN Then
                Tabel.Rows.Add(NomorUrutProduk, JenisProduk_PerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, Kosongan,
                               NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                               JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHarga, KodeProject)
            End If
        End If

        If FungsiForm = FungsiForm_EDIT Then
            If Tabel.RowCount > 0 Then '(Ini untuk mencegah eksekusi kode di bawah, saat jalur masuk melalui form dialog WPF).
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
        End If

        Proses = True
        Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Proses = False
        Me.Close()
    End Sub

End Class