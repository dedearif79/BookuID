Imports System.Windows
Imports bcomm

Public Class wpfWin_PilihJenisProdukInduk

    ' === PUBLIC PROPERTIES (RETURN VALUE) ===
    Public JenisProduk_Induk As String
    Public Lanjutkan As Boolean


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ResetForm()
    End Sub


    Public Sub ResetForm()

        JenisProduk_Induk = Kosongan
        Lanjutkan = False

        'Ceklis Pilihan :
        rdb_Barang.IsChecked = False
        rdb_Jasa.IsChecked = False
        rdb_BarangDanJasa.IsChecked = False
        rdb_JasaKonstruksi.IsChecked = False

        'Ketersediaan Tombol :
        btn_Kembali.IsEnabled = True
        btn_Lanjutkan.IsEnabled = False

    End Sub


    Sub PilihJenisProduk()

        If rdb_Barang.IsChecked = True Then JenisProduk_Induk = JenisProduk_Barang
        If rdb_Jasa.IsChecked = True Then JenisProduk_Induk = JenisProduk_Jasa
        If rdb_BarangDanJasa.IsChecked = True Then JenisProduk_Induk = JenisProduk_BarangDanJasa
        If rdb_JasaKonstruksi.IsChecked = True Then JenisProduk_Induk = JenisProduk_JasaKonstruksi

        If rdb_Barang.IsChecked = True Or
           rdb_Jasa.IsChecked = True Or
           rdb_BarangDanJasa.IsChecked = True Or
           rdb_JasaKonstruksi.IsChecked = True Then
            btn_Lanjutkan.IsEnabled = True
        Else
            btn_Lanjutkan.IsEnabled = False
        End If

    End Sub


    Private Sub rdb_Barang_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Barang.Checked
        PilihJenisProduk()
    End Sub

    Private Sub rdb_Jasa_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_Jasa.Checked
        PilihJenisProduk()
    End Sub

    Private Sub rdb_BarangDanJasa_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_BarangDanJasa.Checked
        PilihJenisProduk()
    End Sub

    Private Sub rdb_JasaKonstruksi_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_JasaKonstruksi.Checked
        PilihJenisProduk()
    End Sub


    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click
        Lanjutkan = True
        Me.Close()
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        Lanjutkan = False
        Me.Close()
    End Sub

End Class
