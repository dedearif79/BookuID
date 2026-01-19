Imports System.Windows
Imports System.Windows.Controls
Imports bcomm

Public Class wpfWin_InputProduk_SJBAST

    ' === PUBLIC PROPERTIES ===
    Public Property JudulForm As String
    Public Property FungsiForm As String
    Public Property JalurMasuk As String
    Public Property NomorUrutProduk As Integer
    Public Property JumlahProduk_Maksimal As Decimal

    ' === GUARD FLAGS ===
    Dim ProsesLoadingForm As Boolean
    Dim ProsesResetForm As Boolean

    ' === VARIABEL NILAI ===
    Dim NamaProduk As String
    Dim DeskripsiProduk As String
    Dim JumlahProduk As Decimal
    Dim SatuanProduk As String
    Dim Keterangan As String

    Dim TabelNota As DataTable


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_NomorUrut.IsReadOnly = True
        txt_NamaProduk.IsReadOnly = True
        txt_DeskripsiProduk.IsReadOnly = True
        txt_Satuan.IsReadOnly = True
        txt_Keterangan.IsReadOnly = True
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Barang/Jasa"
            btn_Tambahkan.Content = "Tambahkan"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Barang/Jasa"
            btn_Tambahkan.Content = "Perbarui"
        End If

        Title = JudulForm

        txt_NamaProduk.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        JalurMasuk = Kosongan
        txt_NomorUrut.Text = Kosongan
        txt_NamaProduk.Text = Kosongan
        txt_DeskripsiProduk.Text = Kosongan
        txt_JumlahProduk.Text = Kosongan
        txt_Satuan.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        NomorUrutProduk = 0
        NamaProduk = Kosongan
        DeskripsiProduk = Kosongan
        JumlahProduk = 0
        SatuanProduk = Kosongan
        Keterangan = Kosongan
        JumlahProduk_Maksimal = 0

        ProsesResetForm = False

    End Sub


    Private Sub txt_NomorUrut_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorUrut.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        NomorUrutProduk = AmbilAngka(txt_NomorUrut.Text)
    End Sub


    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProduk.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        NamaProduk = txt_NamaProduk.Text
    End Sub


    Private Sub txt_DeskripsiProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DeskripsiProduk.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        DeskripsiProduk = txt_DeskripsiProduk.Text
    End Sub


    Private Sub txt_JumlahProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahProduk.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        JumlahProduk = AmbilAngka(txt_JumlahProduk.Text)
    End Sub


    Private Sub txt_Satuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Satuan.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        SatuanProduk = txt_Satuan.Text
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        If ProsesLoadingForm OrElse ProsesResetForm Then Return
        Keterangan = txt_Keterangan.Text
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click

        ' === VALIDASI INPUT ===
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

        ' === TENTUKAN TABEL NOTA ===
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

        ' === UPDATE DATA KE TABEL ===
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


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class
