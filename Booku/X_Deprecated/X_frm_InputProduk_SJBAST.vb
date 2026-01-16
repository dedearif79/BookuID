Imports bcomm


Public Class X_frm_InputProduk_SJBAST

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public NomorUrutProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk
    Dim SatuanProduk
    Dim Keterangan

    Dim TabelNota As New DataTable

    Public JumlahProduk_Maksimal

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Barang/Jasa"
            btn_Tambahkan.Text = "Tambahkan"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Barang/Jasa"
            btn_Tambahkan.Text = "Perbarui"
        End If

        Me.Text = JudulForm

        BeginInvoke(Sub() txt_NamaProduk.Focus())

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        JalurMasuk = Kosongan
        txt_NomorUrut.Text = Kosongan
        txt_NamaProduk.Text = Kosongan
        txt_DeskripsiProduk.Text = Kosongan
        txt_JumlahProduk.Text = Kosongan
        txt_Satuan.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub

    Private Sub txt_NomorUrut_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorUrut.TextChanged
        NomorUrutProduk = AmbilAngka(txt_NomorUrut.Text)
    End Sub
    Private Sub txt_NomorUrut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorUrut.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub
    Private Sub txt_NamaProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaProduk.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_DeskripsiProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_DeskripsiProduk.TextChanged
        DeskripsiProduk = txt_DeskripsiProduk.Text
    End Sub
    Private Sub txt_DeskripsiProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DeskripsiProduk.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahProduk.TextChanged
        JumlahProduk = AmbilAngka(txt_JumlahProduk.Text)
        PemecahRibuanUntukTextBox(txt_JumlahProduk)
    End Sub
    Private Sub txt_JumlahProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahProduk.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Satuan_TextChanged(sender As Object, e As EventArgs) Handles txt_Satuan.TextChanged
        SatuanProduk = txt_Satuan.Text
    End Sub
    Private Sub txt_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Satuan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Tambahkan_Click(sender As Object, e As EventArgs) Handles btn_Tambahkan.Click

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

        If JumlahProduk > JumlahProduk_Maksimal Then
            Dim Dieksekusi As String = Kosongan
            If JalurMasuk = Form_INPUTSURATJALANPENJUALAN Then Dieksekusi = "dikirim"
            If JalurMasuk = Form_INPUTBASTPENJUALAN Then Dieksekusi = "dikerjakan"
            Pesan_Peringatan("Terkait produk ini, jumlah maksimal yang boleh diisi adalah " & JumlahProduk_Maksimal & " " & SatuanProduk &
                   ", sesuai dengan apa yang sudah tercatat di PO dan yang sudah " & Dieksekusi & ".")
            txt_JumlahProduk.Focus()
            Return
        End If

        Select Case JalurMasuk
            Case Form_INPUTSURATJALANPENJUALAN
                TabelNota = win_InputSuratJalanPenjualan.datatabelUtama
            Case Form_INPUTBASTPENJUALAN
                TabelNota = win_InputBASTPenjualan.datatabelUtama
            Case Form_INPUTSURATJALANPEMBELIAN
                TabelNota = win_InputSuratJalanPembelian.datatabelUtama
            Case Form_INPUTBASTPEMBELIAN
                TabelNota = win_InputBASTPembelian.datatabelUtama
            Case Else
                PesanUntukProgrammer("Jalur masuk form belum ditentukan...!!!")
                Return
        End Select


        If FungsiForm = FungsiForm_EDIT Then
            TabelNota.Rows(NomorUrutProduk - 1)("Nama_Produk") = NamaProduk
            TabelNota.Rows(NomorUrutProduk - 1)("Deskripsi_Produk") = DeskripsiProduk
            TabelNota.Rows(NomorUrutProduk - 1)("Jumlah_Produk") = JumlahProduk
            TabelNota.Rows(NomorUrutProduk - 1)("Satuan_Produk") = SatuanProduk
            TabelNota.Rows(NomorUrutProduk - 1)("Keterangan_Produk") = Keterangan
        End If

        ResetForm()
        Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class