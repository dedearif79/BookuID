Imports bcomm
Imports System.Windows
Imports System.Data.Odbc
Imports DocumentFormat.OpenXml.Drawing
Imports MySql.Data.MySqlClient

Public Class wpfWin_UpdateKurs_Bulanan

    Public JudulForm
    Public NomorID

    Public TahunKurs As Integer
    Public KodeMataUang As String
    Dim KursAkhirTahunLalu As Decimal
    Dim KursAkhirJanuari As Decimal
    Dim KursAkhirFebruari As Decimal
    Dim KursAkhirMaret As Decimal
    Dim KursAkhirApril As Decimal
    Dim KursAkhirMei As Decimal
    Dim KursAkhirJuni As Decimal
    Dim KursAkhirJuli As Decimal
    Dim KursAkhirAgustus As Decimal
    Dim KursAkhirSeptember As Decimal
    Dim KursAkhirOktober As Decimal
    Dim KursAkhirNopember As Decimal
    Dim KursAkhirDesember As Decimal

    Public Proses As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand(" SELECT * FROM tbl_kursakhirbulan WHERE ID = '" & NomorID & "' ", KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        drPublic.Read()
        If drPublic.HasRows Then
            txt_KodeMataUang.Text = drPublic.Item("Kode_Mata_Uang")
            txt_KursAkhirTahunLalu.Text = drPublic.Item("Akhir_Tahun_Lalu")
            txt_KursAkhirJanuari.Text = drPublic.Item("Januari")
            txt_KursAkhirFebruari.Text = drPublic.Item("Februari")
            txt_KursAkhirMaret.Text = drPublic.Item("Maret")
            txt_KursAkhirApril.Text = drPublic.Item("April")
            txt_KursAkhirMei.Text = drPublic.Item("Mei")
            txt_KursAkhirJuni.Text = drPublic.Item("Juni")
            txt_KursAkhirJuli.Text = drPublic.Item("Juli")
            txt_KursAkhirAgustus.Text = drPublic.Item("Agustus")
            txt_KursAkhirSeptember.Text = drPublic.Item("September")
            txt_KursAkhirOktober.Text = drPublic.Item("Oktober")
            txt_KursAkhirNopember.Text = drPublic.Item("Nopember")
            txt_KursAkhirDesember.Text = drPublic.Item("Desember")
        End If
        TutupDatabasePublic()

        JudulForm = "Kurs Tengah BI - " & KodeMataUang & " - " & TahunKurs

        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        JudulForm = Kosongan
        NomorID = 0
        KodeMataUang = Kosongan
        TahunKurs = 0
        Proses = False

        txt_KodeMataUang.Text = Kosongan
        txt_KursAkhirJanuari.Text = Kosongan
        txt_KursAkhirFebruari.Text = Kosongan
        txt_KursAkhirMaret.Text = Kosongan
        txt_KursAkhirApril.Text = Kosongan
        txt_KursAkhirMei.Text = Kosongan
        txt_KursAkhirJuni.Text = Kosongan
        txt_KursAkhirJuli.Text = Kosongan
        txt_KursAkhirAgustus.Text = Kosongan
        txt_KursAkhirSeptember.Text = Kosongan
        txt_KursAkhirOktober.Text = Kosongan
        txt_KursAkhirNopember.Text = Kosongan

    End Sub



    Private Sub txt_KodeMataUang_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KodeMataUang.TextChanged
        KodeMataUang = txt_KodeMataUang.Text
    End Sub


    Private Sub txt_KursAkhirTahunLalu_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirTahunLalu.TextChanged
        KursAkhirTahunLalu = AmbilAngka_Desimal(txt_KursAkhirTahunLalu.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirTahunLalu_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirTahunLalu.Click
        txt_KursAkhirTahunLalu.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunKurs - 1), txt_KursAkhirTahunLalu)
    End Sub


    Private Sub txt_KursAkhirJanuari_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirJanuari.TextChanged
        KursAkhirJanuari = AmbilAngka_Desimal(txt_KursAkhirJanuari.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirJanuari_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirJanuari.Click
        txt_KursAkhirJanuari.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(1, TahunKurs), txt_KursAkhirJanuari)
    End Sub


    Private Sub txt_KursAkhirFebruari_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirFebruari.TextChanged
        KursAkhirFebruari = AmbilAngka_Desimal(txt_KursAkhirFebruari.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirFebruari_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirFebruari.Click
        txt_KursAkhirFebruari.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(2, TahunKurs), txt_KursAkhirFebruari)
    End Sub


    Private Sub txt_KursAkhirMaret_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirMaret.TextChanged
        KursAkhirMaret = AmbilAngka_Desimal(txt_KursAkhirMaret.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirMaret_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirMaret.Click
        txt_KursAkhirMaret.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(3, TahunKurs), txt_KursAkhirMaret)
    End Sub


    Private Sub txt_KursAkhirApril_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirApril.TextChanged
        KursAkhirApril = AmbilAngka_Desimal(txt_KursAkhirApril.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirApril_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirApril.Click
        txt_KursAkhirApril.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(4, TahunKurs), txt_KursAkhirApril)
    End Sub


    Private Sub txt_KursAkhirMei_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirMei.TextChanged
        KursAkhirMei = AmbilAngka_Desimal(txt_KursAkhirMei.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirMei_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirMei.Click
        txt_KursAkhirMei.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(5, TahunKurs), txt_KursAkhirMei)
    End Sub


    Private Sub txt_KursAkhirJuni_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirJuni.TextChanged
        KursAkhirJuni = AmbilAngka_Desimal(txt_KursAkhirJuni.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirJuni_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirJuni.Click
        txt_KursAkhirJuni.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(6, TahunKurs), txt_KursAkhirJuni)
    End Sub


    Private Sub txt_KursAkhirJuli_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirJuli.TextChanged
        KursAkhirJuli = AmbilAngka_Desimal(txt_KursAkhirJuli.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirJuli_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirJuli.Click
        txt_KursAkhirJuli.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(7, TahunKurs), txt_KursAkhirJuli)
    End Sub


    Private Sub txt_KursAkhirAgustus_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirAgustus.TextChanged
        KursAkhirAgustus = AmbilAngka_Desimal(txt_KursAkhirAgustus.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirAgustus_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirAgustus.Click
        txt_KursAkhirAgustus.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(8, TahunKurs), txt_KursAkhirAgustus)
    End Sub


    Private Sub txt_KursAkhirSeptember_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirSeptember.TextChanged
        KursAkhirSeptember = AmbilAngka_Desimal(txt_KursAkhirSeptember.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirSeptember_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirSeptember.Click
        txt_KursAkhirSeptember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(9, TahunKurs), txt_KursAkhirSeptember)
    End Sub


    Private Sub txt_KursAkhirOktober_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirOktober.TextChanged
        KursAkhirOktober = AmbilAngka_Desimal(txt_KursAkhirOktober.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirOktober_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirOktober.Click
        txt_KursAkhirOktober.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(10, TahunKurs), txt_KursAkhirOktober)
    End Sub


    Private Sub txt_KursAkhirNopember_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirNopember.TextChanged
        KursAkhirNopember = AmbilAngka_Desimal(txt_KursAkhirNopember.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirNopember_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirNopember.Click
        txt_KursAkhirNopember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(11, TahunKurs), txt_KursAkhirNopember)
    End Sub


    Private Sub txt_KursAkhirDesember_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_KursAkhirDesember.TextChanged
        KursAkhirDesember = AmbilAngka_Desimal(txt_KursAkhirDesember.Text)
    End Sub
    Private Sub btn_UpdateKursAkhirDesember_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursAkhirDesember.Click
        txt_KursAkhirDesember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunKurs), txt_KursAkhirDesember)
    End Sub


    Sub UpdateKursTengah_BI()

        txt_KursAkhirTahunLalu.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunKurs - 1), txt_KursAkhirTahunLalu)
        Jeda(333)
        txt_KursAkhirJanuari.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(1, TahunKurs), txt_KursAkhirJanuari)
        Jeda(333)
        txt_KursAkhirFebruari.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(2, TahunKurs), txt_KursAkhirFebruari)
        Jeda(333)
        txt_KursAkhirMaret.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(3, TahunKurs), txt_KursAkhirMaret)
        Jeda(333)
        txt_KursAkhirApril.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(4, TahunKurs), txt_KursAkhirApril)
        Jeda(333)
        txt_KursAkhirMei.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(5, TahunKurs), txt_KursAkhirMei)
        Jeda(333)
        txt_KursAkhirJuni.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(6, TahunKurs), txt_KursAkhirJuni)
        Jeda(333)
        txt_KursAkhirJuli.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(7, TahunKurs), txt_KursAkhirJuli)
        Jeda(333)
        txt_KursAkhirAgustus.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(8, TahunKurs), txt_KursAkhirAgustus)
        Jeda(333)
        txt_KursAkhirSeptember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(9, TahunKurs), txt_KursAkhirSeptember)
        Jeda(333)
        txt_KursAkhirOktober.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(10, TahunKurs), txt_KursAkhirOktober)
        Jeda(333)
        txt_KursAkhirNopember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(11, TahunKurs), txt_KursAkhirNopember)
        Jeda(333)
        txt_KursAkhirDesember.Text = AmbilValue_KursTengahBI(KodeMataUang, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(12, TahunKurs), txt_KursAkhirDesember)

    End Sub




    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        BukaDatabasePublic()
        If Not StatusKoneksiDatabasePublic Then
            pesan_AdaMasalahDenganKoneksiInternet()
            Return
        End If

        cmdPublic = New MySqlCommand(" UPDATE tbl_kursakhirbulan SET " &
                                     " Akhir_Tahun_Lalu = '" & DesimalFormatSimpan(KursAkhirTahunLalu) & "', " &
                                     " Januari          = '" & DesimalFormatSimpan(KursAkhirJanuari) & "', " &
                                     " Februari         = '" & DesimalFormatSimpan(KursAkhirFebruari) & "', " &
                                     " Maret            = '" & DesimalFormatSimpan(KursAkhirMaret) & "', " &
                                     " April            = '" & DesimalFormatSimpan(KursAkhirApril) & "', " &
                                     " Mei              = '" & DesimalFormatSimpan(KursAkhirMei) & "', " &
                                     " Juni             = '" & DesimalFormatSimpan(KursAkhirJuni) & "', " &
                                     " Juli             = '" & DesimalFormatSimpan(KursAkhirJuli) & "', " &
                                     " Agustus          = '" & DesimalFormatSimpan(KursAkhirAgustus) & "', " &
                                     " September        = '" & DesimalFormatSimpan(KursAkhirSeptember) & "', " &
                                     " Oktober          = '" & DesimalFormatSimpan(KursAkhirOktober) & "', " &
                                     " Nopember         = '" & DesimalFormatSimpan(KursAkhirNopember) & "', " &
                                     " Desember         = '" & DesimalFormatSimpan(KursAkhirDesember) & "'  " &
                                     " WHERE ID         = '" & NomorID & "' ", KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()

        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            pesan_DataTerpilihBerhasilDiperbarui()
            Proses = True
            Close()
        Else
            pesan_AdaMasalahDenganKoneksiInternet()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Proses = False
        Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeMataUang.IsReadOnly = True
    End Sub


End Class
