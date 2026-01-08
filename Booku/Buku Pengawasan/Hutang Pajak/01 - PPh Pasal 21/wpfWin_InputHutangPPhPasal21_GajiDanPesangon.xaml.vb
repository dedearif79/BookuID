Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputHutangPPhPasal21_GajiDanPesangon

    Dim JudulForm
    'Januari 
    Dim DPPGajiJanuari As Int64
    Dim PPhGajiJanuari As Int64
    Dim DPPPesangonJanuari As Int64
    Dim PPhPesangonJanuari As Int64
    'Februari 
    Dim DPPGajiFebruari As Int64
    Dim PPhGajiFebruari As Int64
    Dim DPPPesangonFebruari As Int64
    Dim PPhPesangonFebruari As Int64
    'Maret 
    Dim DPPGajiMaret As Int64
    Dim PPhGajiMaret As Int64
    Dim DPPPesangonMaret As Int64
    Dim PPhPesangonMaret As Int64
    'April 
    Dim DPPGajiApril As Int64
    Dim PPhGajiApril As Int64
    Dim DPPPesangonApril As Int64
    Dim PPhPesangonApril As Int64
    'Mei 
    Dim DPPGajiMei As Int64
    Dim PPhGajiMei As Int64
    Dim DPPPesangonMei As Int64
    Dim PPhPesangonMei As Int64
    'Juni 
    Dim DPPGajiJuni As Int64
    Dim PPhGajiJuni As Int64
    Dim DPPPesangonJuni As Int64
    Dim PPhPesangonJuni As Int64
    'Juli 
    Dim DPPGajiJuli As Int64
    Dim PPhGajiJuli As Int64
    Dim DPPPesangonJuli As Int64
    Dim PPhPesangonJuli As Int64
    'Agustus 
    Dim DPPGajiAgustus As Int64
    Dim PPhGajiAgustus As Int64
    Dim DPPPesangonAgustus As Int64
    Dim PPhPesangonAgustus As Int64
    'September 
    Dim DPPGajiSeptember As Int64
    Dim PPhGajiSeptember As Int64
    Dim DPPPesangonSeptember As Int64
    Dim PPhPesangonSeptember As Int64
    'Oktober 
    Dim DPPGajiOktober As Int64
    Dim PPhGajiOktober As Int64
    Dim DPPPesangonOktober As Int64
    Dim PPhPesangonOktober As Int64
    'Nopember 
    Dim DPPGajiNopember As Int64
    Dim PPhGajiNopember As Int64
    Dim DPPPesangonNopember As Int64
    Dim PPhPesangonNopember As Int64
    'Desember 
    Dim DPPGajiDesember As Int64
    Dim PPhGajiDesember As Int64
    Dim DPPPesangonDesember As Int64
    Dim PPhPesangonDesember As Int64

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        JudulForm = "Input Hutang PPh Pasal 21 (Gaji dan Pesangon) - Tahun " & TahunPajak
        Title = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()
        'Januari :
        txt_DPPGaji_Januari.Text = Kosongan
        txt_PPhGaji_Januari.Text = Kosongan
        txt_DPPPesangon_Januari.Text = Kosongan
        txt_PPhPesangon_Januari.Text = Kosongan
        'Februari :
        txt_DPPGaji_Februari.Text = Kosongan
        txt_PPhGaji_Februari.Text = Kosongan
        txt_DPPPesangon_Februari.Text = Kosongan
        txt_PPhPesangon_Februari.Text = Kosongan
        'Maret :
        txt_DPPGaji_Maret.Text = Kosongan
        txt_PPhGaji_Maret.Text = Kosongan
        txt_DPPPesangon_Maret.Text = Kosongan
        txt_PPhPesangon_Maret.Text = Kosongan
        'April :
        txt_DPPGaji_April.Text = Kosongan
        txt_PPhGaji_April.Text = Kosongan
        txt_DPPPesangon_April.Text = Kosongan
        txt_PPhPesangon_April.Text = Kosongan
        'Mei :
        txt_DPPGaji_Mei.Text = Kosongan
        txt_PPhGaji_Mei.Text = Kosongan
        txt_DPPPesangon_Mei.Text = Kosongan
        txt_PPhPesangon_Mei.Text = Kosongan
        'Juni :
        txt_DPPGaji_Juni.Text = Kosongan
        txt_PPhGaji_Juni.Text = Kosongan
        txt_DPPPesangon_Juni.Text = Kosongan
        txt_PPhPesangon_Juni.Text = Kosongan
        'Juli :
        txt_DPPGaji_Juli.Text = Kosongan
        txt_PPhGaji_Juli.Text = Kosongan
        txt_DPPPesangon_Juli.Text = Kosongan
        txt_PPhPesangon_Juli.Text = Kosongan
        'Agustus :
        txt_DPPGaji_Agustus.Text = Kosongan
        txt_PPhGaji_Agustus.Text = Kosongan
        txt_DPPPesangon_Agustus.Text = Kosongan
        txt_PPhPesangon_Agustus.Text = Kosongan
        'September :
        txt_DPPGaji_September.Text = Kosongan
        txt_PPhGaji_September.Text = Kosongan
        txt_DPPPesangon_September.Text = Kosongan
        txt_PPhPesangon_September.Text = Kosongan
        'Oktober :
        txt_DPPGaji_Oktober.Text = Kosongan
        txt_PPhGaji_Oktober.Text = Kosongan
        txt_DPPPesangon_Oktober.Text = Kosongan
        txt_PPhPesangon_Oktober.Text = Kosongan
        'Nopember :
        txt_DPPGaji_Nopember.Text = Kosongan
        txt_PPhGaji_Nopember.Text = Kosongan
        txt_DPPPesangon_Nopember.Text = Kosongan
        txt_PPhPesangon_Nopember.Text = Kosongan
        'Desember :
        txt_DPPGaji_Desember.Text = Kosongan
        txt_PPhGaji_Desember.Text = Kosongan
        txt_DPPPesangon_Desember.Text = Kosongan
        txt_PPhPesangon_Desember.Text = Kosongan
    End Sub


    'Januari :
    Private Sub txt_DPPGaji_Januari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Januari.TextChanged
        DPPGajiJanuari = AmbilAngka(txt_DPPGaji_Januari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Januari)
    End Sub
    Private Sub txt_DPPGaji_Januari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Januari.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Januari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Januari.TextChanged
        PPhGajiJanuari = AmbilAngka(txt_PPhGaji_Januari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Januari)
    End Sub
    Private Sub txt_PPhGaji_Januari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Januari.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Januari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Januari.TextChanged
        DPPPesangonJanuari = AmbilAngka(txt_DPPPesangon_Januari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Januari)
    End Sub
    Private Sub txt_DPPPesangon_Januari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Januari.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Januari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Januari.TextChanged
        PPhPesangonJanuari = AmbilAngka(txt_PPhPesangon_Januari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Januari)
    End Sub
    Private Sub txt_PPhPesangon_Januari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Januari.PreviewTextInput
              
    End Sub



    'Februari :
    Private Sub txt_DPPGaji_Februari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Februari.TextChanged
        DPPGajiFebruari = AmbilAngka(txt_DPPGaji_Februari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Februari)
    End Sub
    Private Sub txt_DPPGaji_Februari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Februari.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Februari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Februari.TextChanged
        PPhGajiFebruari = AmbilAngka(txt_PPhGaji_Februari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Februari)
    End Sub
    Private Sub txt_PPhGaji_Februari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Februari.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Februari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Februari.TextChanged
        DPPPesangonFebruari = AmbilAngka(txt_DPPPesangon_Februari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Februari)
    End Sub
    Private Sub txt_DPPPesangon_Februari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Februari.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Februari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Februari.TextChanged
        PPhPesangonFebruari = AmbilAngka(txt_PPhPesangon_Februari.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Februari)
    End Sub
    Private Sub txt_PPhPesangon_Februari_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Februari.PreviewTextInput
              
    End Sub



    'Maret :
    Private Sub txt_DPPGaji_Maret_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Maret.TextChanged
        DPPGajiMaret = AmbilAngka(txt_DPPGaji_Maret.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Maret)
    End Sub
    Private Sub txt_DPPGaji_Maret_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Maret.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Maret_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Maret.TextChanged
        PPhGajiMaret = AmbilAngka(txt_PPhGaji_Maret.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Maret)
    End Sub
    Private Sub txt_PPhGaji_Maret_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Maret.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Maret_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Maret.TextChanged
        DPPPesangonMaret = AmbilAngka(txt_DPPPesangon_Maret.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Maret)
    End Sub
    Private Sub txt_DPPPesangon_Maret_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Maret.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Maret_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Maret.TextChanged
        PPhPesangonMaret = AmbilAngka(txt_PPhPesangon_Maret.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Maret)
    End Sub
    Private Sub txt_PPhPesangon_Maret_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Maret.PreviewTextInput
              
    End Sub



    'April :
    Private Sub txt_DPPGaji_April_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_April.TextChanged
        DPPGajiApril = AmbilAngka(txt_DPPGaji_April.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_April)
    End Sub
    Private Sub txt_DPPGaji_April_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_April.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_April_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_April.TextChanged
        PPhGajiApril = AmbilAngka(txt_PPhGaji_April.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_April)
    End Sub
    Private Sub txt_PPhGaji_April_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_April.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_April_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_April.TextChanged
        DPPPesangonApril = AmbilAngka(txt_DPPPesangon_April.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_April)
    End Sub
    Private Sub txt_DPPPesangon_April_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_April.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_April_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_April.TextChanged
        PPhPesangonApril = AmbilAngka(txt_PPhPesangon_April.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_April)
    End Sub
    Private Sub txt_PPhPesangon_April_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_April.PreviewTextInput
              
    End Sub



    'Mei :
    Private Sub txt_DPPGaji_Mei_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Mei.TextChanged
        DPPGajiMei = AmbilAngka(txt_DPPGaji_Mei.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Mei)
    End Sub
    Private Sub txt_DPPGaji_Mei_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Mei.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Mei_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Mei.TextChanged
        PPhGajiMei = AmbilAngka(txt_PPhGaji_Mei.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Mei)
    End Sub
    Private Sub txt_PPhGaji_Mei_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Mei.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Mei_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Mei.TextChanged
        DPPPesangonMei = AmbilAngka(txt_DPPPesangon_Mei.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Mei)
    End Sub
    Private Sub txt_DPPPesangon_Mei_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Mei.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Mei_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Mei.TextChanged
        PPhPesangonMei = AmbilAngka(txt_PPhPesangon_Mei.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Mei)
    End Sub
    Private Sub txt_PPhPesangon_Mei_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Mei.PreviewTextInput
              
    End Sub



    'Juni :
    Private Sub txt_DPPGaji_Juni_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Juni.TextChanged
        DPPGajiJuni = AmbilAngka(txt_DPPGaji_Juni.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Juni)
    End Sub
    Private Sub txt_DPPGaji_Juni_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Juni.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Juni_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Juni.TextChanged
        PPhGajiJuni = AmbilAngka(txt_PPhGaji_Juni.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Juni)
    End Sub
    Private Sub txt_PPhGaji_Juni_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Juni.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Juni_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Juni.TextChanged
        DPPPesangonJuni = AmbilAngka(txt_DPPPesangon_Juni.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Juni)
    End Sub
    Private Sub txt_DPPPesangon_Juni_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Juni.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Juni_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Juni.TextChanged
        PPhPesangonJuni = AmbilAngka(txt_PPhPesangon_Juni.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Juni)
    End Sub
    Private Sub txt_PPhPesangon_Juni_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Juni.PreviewTextInput
              
    End Sub



    'Juli :
    Private Sub txt_DPPGaji_Juli_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Juli.TextChanged
        DPPGajiJuli = AmbilAngka(txt_DPPGaji_Juli.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Juli)
    End Sub
    Private Sub txt_DPPGaji_Juli_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Juli.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Juli_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Juli.TextChanged
        PPhGajiJuli = AmbilAngka(txt_PPhGaji_Juli.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Juli)
    End Sub
    Private Sub txt_PPhGaji_Juli_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Juli.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Juli_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Juli.TextChanged
        DPPPesangonJuli = AmbilAngka(txt_DPPPesangon_Juli.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Juli)
    End Sub
    Private Sub txt_DPPPesangon_Juli_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Juli.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Juli_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Juli.TextChanged
        PPhPesangonJuli = AmbilAngka(txt_PPhPesangon_Juli.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Juli)
    End Sub
    Private Sub txt_PPhPesangon_Juli_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Juli.PreviewTextInput
              
    End Sub



    'Agustus :
    Private Sub txt_DPPGaji_Agustus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Agustus.TextChanged
        DPPGajiAgustus = AmbilAngka(txt_DPPGaji_Agustus.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Agustus)
    End Sub
    Private Sub txt_DPPGaji_Agustus_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Agustus.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Agustus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Agustus.TextChanged
        PPhGajiAgustus = AmbilAngka(txt_PPhGaji_Agustus.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Agustus)
    End Sub
    Private Sub txt_PPhGaji_Agustus_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Agustus.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Agustus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Agustus.TextChanged
        DPPPesangonAgustus = AmbilAngka(txt_DPPPesangon_Agustus.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Agustus)
    End Sub
    Private Sub txt_DPPPesangon_Agustus_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Agustus.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Agustus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Agustus.TextChanged
        PPhPesangonAgustus = AmbilAngka(txt_PPhPesangon_Agustus.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Agustus)
    End Sub
    Private Sub txt_PPhPesangon_Agustus_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Agustus.PreviewTextInput
              
    End Sub



    'September :
    Private Sub txt_DPPGaji_September_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_September.TextChanged
        DPPGajiSeptember = AmbilAngka(txt_DPPGaji_September.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_September)
    End Sub
    Private Sub txt_DPPGaji_September_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_September.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_September_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_September.TextChanged
        PPhGajiSeptember = AmbilAngka(txt_PPhGaji_September.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_September)
    End Sub
    Private Sub txt_PPhGaji_September_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_September.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_September_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_September.TextChanged
        DPPPesangonSeptember = AmbilAngka(txt_DPPPesangon_September.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_September)
    End Sub
    Private Sub txt_DPPPesangon_September_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_September.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_September_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_September.TextChanged
        PPhPesangonSeptember = AmbilAngka(txt_PPhPesangon_September.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_September)
    End Sub
    Private Sub txt_PPhPesangon_September_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_September.PreviewTextInput
              
    End Sub



    'Oktober :
    Private Sub txt_DPPGaji_Oktober_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Oktober.TextChanged
        DPPGajiOktober = AmbilAngka(txt_DPPGaji_Oktober.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Oktober)
    End Sub
    Private Sub txt_DPPGaji_Oktober_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Oktober.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Oktober_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Oktober.TextChanged
        PPhGajiOktober = AmbilAngka(txt_PPhGaji_Oktober.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Oktober)
    End Sub
    Private Sub txt_PPhGaji_Oktober_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Oktober.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Oktober_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Oktober.TextChanged
        DPPPesangonOktober = AmbilAngka(txt_DPPPesangon_Oktober.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Oktober)
    End Sub
    Private Sub txt_DPPPesangon_Oktober_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Oktober.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Oktober_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Oktober.TextChanged
        PPhPesangonOktober = AmbilAngka(txt_PPhPesangon_Oktober.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Oktober)
    End Sub
    Private Sub txt_PPhPesangon_Oktober_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Oktober.PreviewTextInput
              
    End Sub



    'Nopember :
    Private Sub txt_DPPGaji_Nopember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Nopember.TextChanged
        DPPGajiNopember = AmbilAngka(txt_DPPGaji_Nopember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Nopember)
    End Sub
    Private Sub txt_DPPGaji_Nopember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Nopember.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Nopember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Nopember.TextChanged
        PPhGajiNopember = AmbilAngka(txt_PPhGaji_Nopember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Nopember)
    End Sub
    Private Sub txt_PPhGaji_Nopember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Nopember.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Nopember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Nopember.TextChanged
        DPPPesangonNopember = AmbilAngka(txt_DPPPesangon_Nopember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Nopember)
    End Sub
    Private Sub txt_DPPPesangon_Nopember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Nopember.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Nopember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Nopember.TextChanged
        PPhPesangonNopember = AmbilAngka(txt_PPhPesangon_Nopember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Nopember)
    End Sub
    Private Sub txt_PPhPesangon_Nopember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Nopember.PreviewTextInput
              
    End Sub



    'Desember :
    Private Sub txt_DPPGaji_Desember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPGaji_Desember.TextChanged
        DPPGajiDesember = AmbilAngka(txt_DPPGaji_Desember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPGaji_Desember)
    End Sub
    Private Sub txt_DPPGaji_Desember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPGaji_Desember.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhGaji_Desember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhGaji_Desember.TextChanged
        PPhGajiDesember = AmbilAngka(txt_PPhGaji_Desember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhGaji_Desember)
    End Sub
    Private Sub txt_PPhGaji_Desember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhGaji_Desember.PreviewTextInput
              
    End Sub

    Private Sub txt_DPPPesangon_Desember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPPPesangon_Desember.TextChanged
        DPPPesangonDesember = AmbilAngka(txt_DPPPesangon_Desember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPPPesangon_Desember)
    End Sub
    Private Sub txt_DPPPesangon_Desember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPPPesangon_Desember.PreviewTextInput
              
    End Sub

    Private Sub txt_PPhPesangon_Desember_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhPesangon_Desember.TextChanged
        PPhPesangonDesember = AmbilAngka(txt_PPhPesangon_Desember.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhPesangon_Desember)
    End Sub
    Private Sub txt_PPhPesangon_Desember_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_PPhPesangon_Desember.PreviewTextInput
              
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Semua Data Hutang PPh Pasal 21 - Gaji :
        cmdHAPUS = New Odbc.OdbcCommand(" DELETE FROM tbl_HutangPajak " &
                                        " WHERE Jenis_Pajak                         = '" & JenisPajak_PPhPasal21 & "' " &
                                        " AND Nama_Jasa                             = '" & teks_Gaji & "' " &
                                        " AND DATE_FORMAT(Tanggal_Transaksi, '%Y')  = '" & TahunPajak & "' ",
                                        KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        'Hapus Semua Data Hutang PPh Pasal 401 - Pesangon :
        cmdHAPUS = New Odbc.OdbcCommand(" DELETE FROM tbl_HutangPajak " &
                                        " WHERE Jenis_Pajak                         = '" & JenisPajak_PPhPasal21 & "' " &
                                        " AND Nama_Jasa                             = '" & teks_Pesangon & "' " &
                                        " AND DATE_FORMAT(Tanggal_Transaksi, '%Y')  = '" & TahunPajak & "' ",
                                        KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        Dim BulanAngka As Integer
        Dim Bulan As String
        Dim NomorIdTransaksi As Int64
        Dim TanggalTransaksi As Date

        Dim DPP As Int64
        Dim PPh As Int64

        NomorIdTransaksi = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_HutangPajak")

        AksesDatabase_Transaksi(Buka)

        'Penyimpanan Data Gaji :
        BulanAngka = 0
        Do While BulanAngka < 12
            BulanAngka += 1
            Bulan = KonversiAngkaKeBulanString(BulanAngka)
            Select Case Bulan
                Case Bulan_Januari
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Januari, TahunPajak)
                    DPP = DPPGajiJanuari
                    PPh = PPhGajiJanuari
                Case Bulan_Februari
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Februari, TahunPajak)
                    DPP = DPPGajiFebruari
                    PPh = PPhGajiFebruari
                Case Bulan_Maret
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Maret, TahunPajak)
                    DPP = DPPGajiMaret
                    PPh = PPhGajiMaret
                Case Bulan_April
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_April, TahunPajak)
                    DPP = DPPGajiApril
                    PPh = PPhGajiApril
                Case Bulan_Mei
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Mei, TahunPajak)
                    DPP = DPPGajiMei
                    PPh = PPhGajiMei
                Case Bulan_Juni
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Juni, TahunPajak)
                    DPP = DPPGajiJuni
                    PPh = PPhGajiJuni
                Case Bulan_Juli
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Juli, TahunPajak)
                    DPP = DPPGajiJuli
                    PPh = PPhGajiJuli
                Case Bulan_Agustus
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Agustus, TahunPajak)
                    DPP = DPPGajiAgustus
                    PPh = PPhGajiAgustus
                Case Bulan_September
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_September, TahunPajak)
                    DPP = DPPGajiSeptember
                    PPh = PPhGajiSeptember
                Case Bulan_Oktober
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Oktober, TahunPajak)
                    DPP = DPPGajiOktober
                    PPh = PPhGajiOktober
                Case Bulan_Nopember
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Nopember, TahunPajak)
                    DPP = DPPGajiNopember
                    PPh = PPhGajiNopember
                Case Bulan_Desember
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Desember, TahunPajak)
                    DPP = DPPGajiDesember
                    PPh = PPhGajiDesember
            End Select
            If DPP > 0 Or PPh > 0 Then
                NomorIdTransaksi += 1
                cmdSIMPAN = New Odbc.OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                 " '" & NomorIdTransaksi & "', " &
                                                 " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                                 " '" & TanggalKosongSimpan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & teks_Gaji & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & DPP & "', " &
                                                 " '" & JenisPajak_PPhPasal21 & "', " &
                                                 " '" & KodeSetoran_100 & "', " &
                                                 " '" & PPh & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & 0 & "', " &
                                                 " '" & UserAktif & "' ) ",
                                                 KoneksiDatabaseTransaksi)
                cmdSIMPAN_ExecuteNonQuery()
            End If
        Loop

        'Penyimpanan Data Pesangon :
        BulanAngka = 0
        Do While BulanAngka < 12
            BulanAngka += 1
            Bulan = KonversiAngkaKeBulanString(BulanAngka)
            Select Case Bulan
                Case Bulan_Januari
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Januari, TahunPajak)
                    DPP = DPPPesangonJanuari
                    PPh = PPhPesangonJanuari
                Case Bulan_Februari
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Februari, TahunPajak)
                    DPP = DPPPesangonFebruari
                    PPh = PPhPesangonFebruari
                Case Bulan_Maret
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Maret, TahunPajak)
                    DPP = DPPPesangonMaret
                    PPh = PPhPesangonMaret
                Case Bulan_April
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_April, TahunPajak)
                    DPP = DPPPesangonApril
                    PPh = PPhPesangonApril
                Case Bulan_Mei
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Mei, TahunPajak)
                    DPP = DPPPesangonMei
                    PPh = PPhPesangonMei
                Case Bulan_Juni
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Juni, TahunPajak)
                    DPP = DPPPesangonJuni
                    PPh = PPhPesangonJuni
                Case Bulan_Juli
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Juli, TahunPajak)
                    DPP = DPPPesangonJuli
                    PPh = PPhPesangonJuli
                Case Bulan_Agustus
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Agustus, TahunPajak)
                    DPP = DPPPesangonAgustus
                    PPh = PPhPesangonAgustus
                Case Bulan_September
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_September, TahunPajak)
                    DPP = DPPPesangonSeptember
                    PPh = PPhPesangonSeptember
                Case Bulan_Oktober
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Oktober, TahunPajak)
                    DPP = DPPPesangonOktober
                    PPh = PPhPesangonOktober
                Case Bulan_Nopember
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Nopember, TahunPajak)
                    DPP = DPPPesangonNopember
                    PPh = PPhPesangonNopember
                Case Bulan_Desember
                    TanggalTransaksi = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Desember, TahunPajak)
                    DPP = DPPPesangonDesember
                    PPh = PPhPesangonDesember
            End Select
            If DPP > 0 Or PPh > 0 Then
                NomorIdTransaksi += 1
                cmdSIMPAN = New Odbc.OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                 " '" & NomorIdTransaksi & "', " &
                                                 " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                                 " '" & TanggalKosongSimpan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & teks_Pesangon & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & DPP & "', " &
                                                 " '" & JenisPajak_PPhPasal21 & "', " &
                                                 " '" & KodeSetoran_401 & "', " &
                                                 " '" & PPh & "', " &
                                                 " '" & Kosongan & "', " &
                                                 " '" & 0 & "', " &
                                                 " '" & UserAktif & "' ) ",
                                                 KoneksiDatabaseTransaksi)
                cmdSIMPAN_ExecuteNonQuery()
            End If
        Loop

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            MsgBox("Data BERHASIL disimpan.")
            If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()
            Me.Close()
        Else
            MsgBox("Data GAGAL disimpan." & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
