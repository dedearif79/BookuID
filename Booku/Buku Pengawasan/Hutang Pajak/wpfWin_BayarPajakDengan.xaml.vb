Imports System.Windows
Imports bcomm

Public Class wpfWin_BayarPajakDengan

    Public BayarDengan As String
    Public BayarDengan_BankCash As String = "Bank/Cash"
    Public BayarDengan_KetetapanPajak As String = "Ketetapan Pajak"
    Public BayarDengan_LebihBayar As String = "Lebih Bayar"
    Public BayarDengan_Pemindahbukuan As String = "Pemindahbukuan"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        BayarDengan = Kosongan
        rdb_BankCash.IsChecked = True
    End Sub

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        rdb_LebihBayar.IsEnabled = False        'Ini dikunci karena belum ada konsepnya. Tapi dimunculkan dulu supaya tidak lupa, bahwa harus ada pilihan ini.
        rdb_Pemindahbukuan.IsEnabled = False    'Ini dikunci karena belum ada konsepnya. Tapi dimunculkan dulu supaya tidak lupa, bahwa harus ada pilihan ini.
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_Lanjutkan.Click
        If rdb_BankCash.IsChecked = True Then BayarDengan = BayarDengan_BankCash
        If rdb_KetetapanPajak.IsChecked = True Then BayarDengan = BayarDengan_KetetapanPajak
        If rdb_LebihBayar.IsChecked = True Then BayarDengan = BayarDengan_LebihBayar
        If rdb_Pemindahbukuan.IsChecked = True Then BayarDengan = BayarDengan_Pemindahbukuan
        Me.Close()
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        BayarDengan = Kosongan
        Me.Close()
    End Sub

End Class
