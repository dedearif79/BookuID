Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Class wpfWin_InputProduk_DepositOperasional

    Public FungsiForm
    Public NomorUrut

    Public KodeAkun
    Public NamaAkun
    Public NamaProduk
    Public NomorReferensi
    Public TanggalReferensi
    Public Jumlah

    Public Tambahkan As Boolean
    Public Perbarui As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Title = "Input Produk"
            btn_Tambahkan.Content = teks_Tambahkan
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Me.Title = "Edit Produk"
            btn_Tambahkan.Content = teks_Perbarui
        End If

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        FungsiForm = Kosongan
        NomorUrut = 0
        txt_KodeAkun.Text = Kosongan
        txt_NamaAkun.Text = Kosongan
        txt_NamaProduk.Text = Kosongan
        txt_NomorReferensi.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalReferensi)
        txt_Jumlah.Text = Kosongan

        Tambahkan = False
        Perbarui = False

    End Sub


    Private Sub txt_KodeAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeAkun.TextChanged
        KodeAkun = txt_KodeAkun.Text
        txt_NamaAkun.Text = AmbilValue_NamaAkun(KodeAkun)
    End Sub

    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_DepositOperasional
        If txt_KodeAkun.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_KodeAkun.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun.Text
        End If
        win_ListCOA.ShowDialog()
        txt_KodeAkun.Text = win_ListCOA.COATerseleksi
    End Sub

    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
    End Sub


    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub


    Private Sub txt_NomorReferensi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorReferensi.TextChanged
        NomorReferensi = txt_NomorReferensi.Text
    End Sub


    Private Sub dtp_TanggalReferensi_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalReferensi.SelectedDateChanged
        If dtp_TanggalReferensi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalReferensi)
            TanggalReferensi = TanggalFormatTampilan(dtp_TanggalReferensi.SelectedDate)
        End If
    End Sub


    Private Sub txt_Jumlah_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Jumlah.TextChanged
        Jumlah = AmbilAngka(txt_Jumlah.Text)
        PemecahRibuanUntukTextBox_WPF(txt_Jumlah)
    End Sub
    Private Sub txt_Jumlah_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_Jumlah.PreviewTextInput
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeAkun.IsReadOnly = True
        txt_NamaAkun.IsReadOnly = True
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click

        If KodeAkun = Kosongan Then
            PesanPeringatan("Silakan pilih 'Akun'.")
            btn_PilihCOA_Click(sender, e)
            Return
        End If

        If NamaProduk = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Barang/Jasa'.")
            txt_NamaProduk.Focus()
            Return
        End If

        If NomorReferensi = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Referensi'.")
            txt_NomorReferensi.Focus()
            Return
        End If

        If dtp_TanggalReferensi.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Referensi'.")
            dtp_TanggalReferensi.Focus()
            Return
        End If

        If Jumlah = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah'.")
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            Tambahkan = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Perbarui = True
        End If

        Me.Close()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class
