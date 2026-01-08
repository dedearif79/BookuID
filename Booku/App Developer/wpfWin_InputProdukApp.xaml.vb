Imports bcomm
Imports System.Windows
Imports System.Windows.Input
Imports MySql.Data.MySqlClient

Public Class wpfWin_InputProdukApp

    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Title = "Input Produk App"
            txt_NomorSeriProduk.IsEnabled = True
            txt_IDCustomer.IsEnabled = True
            btn_Simpan.Content = "Simpan"
            btn_Reset.IsEnabled = True
        End If
        If FungsiForm = FungsiForm_EDIT Then
            Me.Title = "Edit Produk App"
            If txt_StatusTerpakai.Text = "0" Then
                txt_NomorSeriProduk.IsEnabled = True
                txt_IDCustomer.IsEnabled = True
            Else
                txt_NomorSeriProduk.IsEnabled = False
                txt_IDCustomer.IsEnabled = False
            End If
            btn_Simpan.Content = "Simpan"
            btn_Reset.IsEnabled = False
        End If

        txt_NomorSeriProduk.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()
        txt_NomorSeriProduk.Text = Nothing
        txt_IDCustomer.Text = Nothing
        txt_JumlahPerangkat.Text = Nothing
        txt_StatusTerpakai.Text = Nothing
    End Sub


    Private Sub txt_JumlahPerangkat_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahPerangkat.PreviewTextInput
        HanyaBolehInputAngkaPlus_WPF(txt_JumlahPerangkat, e)
    End Sub


    Private Sub txt_StatusTerpakai_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_StatusTerpakai.PreviewTextInput
        HanyaBolehInputAngkaPlus_WPF(txt_StatusTerpakai, e)
    End Sub


    Private Sub btn_Reset_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If txt_IDCustomer.Text.Length <> 8 Then
            PesanPeringatan("Jumlah Digit 'ID Customer' harus 8" & Enter2Baris & "Silakan perbaiki.")
            txt_IDCustomer.Focus()
            Return
        End If

        Dim QuerySimpan = " INSERT INTO tbl_produk VALUES ('" &
                                         txt_NomorSeriProduk.Text & "', '" &
                                         txt_IDCustomer.Text & "', '" &
                                         txt_JumlahPerangkat.Text & "', '" &
                                         txt_StatusTerpakai.Text & "') "

        Dim QueryEdit = " UPDATE tbl_produk SET " &
                        " ID_Customer = '" & txt_IDCustomer.Text & "', " &
                        " Jumlah_Perangkat = '" & AmbilAngka(txt_JumlahPerangkat.Text) & "', " &
                        " Status_Terpakai = '" & AmbilAngka(txt_StatusTerpakai.Text) & "' " &
                        " WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' "


        BukaDatabasePublic()
        If StatusKoneksiDatabase = False Then Return

        If FungsiForm = FungsiForm_TAMBAH Then
            cmdPublic = New MySqlCommand(QuerySimpan, KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
                PesanPemberitahuan("Data Produk berhasil disimpan.")
                usc_DataProdukApp.RefreshTampilanData()
                ResetForm()
            Catch ex As Exception
                PesanPeringatan("Data Produk gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
        End If

        If FungsiForm = FungsiForm_EDIT Then
            cmdPublic = New MySqlCommand(QueryEdit, KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
                PesanPemberitahuan("Data Produk berhasil diedit.")
                usc_DataProdukApp.RefreshTampilanData()
                Me.Close()
            Catch ex As Exception
                PesanPeringatan("Data Produk gagal diedit." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
        End If

        TutupDatabasePublic()

    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
