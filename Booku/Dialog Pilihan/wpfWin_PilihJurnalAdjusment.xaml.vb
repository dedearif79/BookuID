Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Media.Animation
Imports bcomm

Public Class wpfWin_PilihJurnalAdjusment

    Public LanjutkanProses As Boolean
    Public AdjusmentBulanBukuAktifSudahLengkap

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        CekSeluruhAdjusmentAkhirBulan()

        ProsesLoadingForm = False

    End Sub


    Sub CekSeluruhAdjusmentAkhirBulan()

        IsEnabled = False

        AdjusmentBulanBukuAktifSudahLengkap = True

        'Cek Adjusment Penyusutan :
        frm_Adjusment_PenyusutanAsset.CekAdjusment()
        If usc_Adjusment_PenyusutanAsset.AdjusmentBulanBukuAktifSudahLengkap Then
            rdb_AdjusmentPenyusutan.IsEnabled = False
            rdb_AdjusmentHPP.IsEnabled = True
        Else
            AdjusmentBulanBukuAktifSudahLengkap = False
            rdb_AdjusmentHPP.IsEnabled = False
        End If

        'Cek Adjusment Amortisasi :
        frm_Adjusment_Amortisasi.CekAdjusment()
        If usc_Adjusment_Amortisasi.AdjusmentBulanBukuAktifSudahLengkap Then
            rdb_AdjusmentAmortisasi.IsEnabled = False
            rdb_AdjusmentHPP.IsEnabled = True
        Else
            AdjusmentBulanBukuAktifSudahLengkap = False
            rdb_AdjusmentHPP.IsEnabled = False
        End If

        'Cek Adjusment Forex :
        frm_Adjusment_Forex.CekAdjusment()
        If usc_Adjusment_Forex.AdjusmentBulanBukuAktifSudahLengkap Then
            rdb_AdjusmentForex.IsEnabled = False
            rdb_AdjusmentHPP.IsEnabled = True
        Else
            AdjusmentBulanBukuAktifSudahLengkap = False
            rdb_AdjusmentHPP.IsEnabled = False
        End If

        'Cek Adjusment HPP :
        frm_Adjusment_HPP.CekAdjusment()
        If usc_JurnalAdjusment_HPP.AdjusmentBulanBukuAktifSudahLengkap Then
            rdb_AdjusmentHPP.IsEnabled = False
        Else
            AdjusmentBulanBukuAktifSudahLengkap = False
        End If

        If AdjusmentBulanBukuAktifSudahLengkap Then
            btn_Lanjutkan.IsEnabled = False
            PesanPemberitahuan("Adjusment Akhir Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & " " & TahunBukuAktif & " sudah lengkap." & Enter2Baris &
                               "Sistem akan melakukan proses Trial Balance secara otomatis, dan pembukuan " & KonversiAngkaKeBulanString(BulanBukuAktif) & " " & TahunBukuAktif & " sudah bisa ditutup.")
            Close()
        End If

        IsEnabled = True

    End Sub

    Sub ResetForm()

        IsEnabled = True
        LanjutkanProses = False
        AdjusmentBulanBukuAktifSudahLengkap = False

        'Ketersediaan Pilihan :
        rdb_AdjusmentPenyusutan.IsEnabled = True
        rdb_AdjusmentAmortisasi.IsEnabled = True
        rdb_AdjusmentForex.IsEnabled = True
        rdb_AdjusmentHPP.IsEnabled = True

        'Ceklis Pilihan :
        rdb_AdjusmentPenyusutan.IsChecked = False
        rdb_AdjusmentAmortisasi.IsChecked = False
        rdb_AdjusmentForex.IsChecked = False
        rdb_AdjusmentHPP.IsChecked = False

        'Ketersediaan Tombol :
        btn_Kembali.IsEnabled = True
        btn_Lanjutkan.IsEnabled = True

    End Sub


    Sub CekKeberadaanJurnalAdjusment_Penyusutan()

    End Sub



    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        IsEnabled = False

        LanjutkanProses = True

        If rdb_AdjusmentPenyusutan.IsChecked = True Then
            frm_BOOKU.BukaHalamanAdjusmentPenyusutanAsset()
        End If

        If rdb_AdjusmentAmortisasi.IsChecked = True Then
            frm_BOOKU.BukaHalamanAdjusmentAmortisasi()
        End If

        If rdb_AdjusmentForex.IsChecked = True Then
            frm_Adjusment_Forex.MdiParent = frm_BOOKU
            frm_Adjusment_Forex.Show()
            frm_Adjusment_Forex.Focus()
        End If

        If rdb_AdjusmentHPP.IsChecked = True Then
            frm_Adjusment_HPP.MdiParent = frm_BOOKU
            frm_Adjusment_HPP.Show()
            frm_Adjusment_HPP.Focus()
        End If

        Me.Close()

    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        LanjutkanProses = False
        Me.Close()
    End Sub

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
