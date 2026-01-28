Imports System.Data.Odbc
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports bcomm

Public Class wpfWin_InputToko

    Public FungsiForm
    Public JudulForm
    Public StatusEdit

    Dim KodeToko
    Dim NamaToko
    Dim COAKas
    Dim NamaAkunKas
    Dim Alamat
    Dim Deskripsi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Toko"
            txt_KodeToko.IsReadOnly = False
        End If
        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Toko"
            txt_KodeToko.IsReadOnly = True
            IsiValueForm()
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm


        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesIsiValueForm = True

        FungsiForm = Kosongan
        txt_KodeToko.Text = Kosongan
        txt_NamaToko.Text = Kosongan
        txt_COAKas.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Alamat)
        KosongkanValueElemenRichTextBox(txt_Deskripsi)
        ProsesIsiValueForm = False

    End Sub



    Sub IsiValueForm()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Toko WHERE Kode_Toko = '" & txt_KodeToko.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        txt_NamaToko.Text = dr.Item("Nama_Toko")
        IsiValueElemenRichTextBox(txt_Alamat, dr.Item("Alamat"))
        txt_COAKas.Text = dr.Item("COA_Kas")
        IsiValueElemenRichTextBox(txt_Deskripsi, dr.Item("Deskripsi"))
        AksesDatabase_General(Tutup)
    End Sub



    Private Sub txt_KodeToko_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeToko.TextChanged
        KodeToko = txt_KodeToko.Text.ToUpper
    End Sub
    Private Sub txt_KodeToko_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KodeToko.PreviewTextInput
        Dim regex As New Regex("^[a-zA-Z0-9]+$")
        If Not regex.IsMatch(e.Text) Then
            e.Handled = True ' Mencegah input yang tidak sesuai
        End If
    End Sub
    Private Sub txt_KodeToko_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_KodeToko.PreviewKeyDown
        If e.Key = Key.Space Then
            e.Handled = True ' Mencegah input spasi
        End If
    End Sub
    Private Sub txt_KodeToko_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_KodeToko.LostFocus
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Toko WHERE Kode_Toko = '" & KodeToko & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Dim NamaToko = dr.Item("Nama_Toko")
                Pesan_Peringatan("Kode '" & KodeToko & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaToko & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
                txt_KodeToko.Text = Kosongan
                txt_KodeToko.Focus()
                AksesDatabase_General(Tutup)
                Return
            End If
            AksesDatabase_General(Tutup)
        End If
    End Sub


    Private Sub txt_NamaToko_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaToko.TextChanged
        NamaToko = txt_NamaToko.Text
    End Sub


    Private Sub txt_Alamat_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Alamat.TextChanged
        Alamat = IsiValueVariabelRichTextBox(txt_Alamat)
    End Sub


    Private Sub txt_COAKas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COAKas.TextChanged
        COAKas = txt_COAKas.Text
        txt_NamaAkunKas.Text = AmbilValue_NamaAkun(COAKas)
    End Sub
    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_KasOutlet
        If txt_COAKas.Text <> Kosongan Then win_ListCOA.COATerseleksi = txt_COAKas.Text
        If txt_NamaAkunKas.Text <> Kosongan Then win_ListCOA.NamaAkunTerseleksi = txt_NamaAkunKas.Text
        win_ListCOA.ShowDialog()
        txt_COAKas.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkunKas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkunKas.TextChanged
        NamaAkunKas = txt_NamaAkunKas.Text
    End Sub



    Private Sub txt_Deskripsi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Deskripsi.TextChanged
        Deskripsi = IsiValueVariabelRichTextBox(txt_Deskripsi)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If KodeToko = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_KodeToko, "Kode Toko")
            Return
        End If

        If KodeToko.Length < 3 Then
            PesanPeringatan("Kode Toko minimal 3 huruf")
            txt_KodeToko.Focus()
            Return
        End If

        If NamaToko = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaToko, "Nama Toko")
            Return
        End If

        If COAKas = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_COAKas, "COA Kas")
            Return
        End If

        AksesDatabase_General(Buka)

        Dim QuerySimpan = Kosongan

        If FungsiForm = FungsiForm_TAMBAH Then

            QuerySimpan = " INSERT INTO tbl_Toko VALUES ( " &
                " '" & KodeToko & "', " &
                " '" & NamaToko & "', " &
                " '" & Alamat & "', " &
                " '" & COAKas & "', " &
                " '" & Deskripsi & "' ) "

        End If

        If FungsiForm = FungsiForm_EDIT Then

            QuerySimpan = " UPDATE tbl_Toko SET " &
                " Nama_Toko         = '" & NamaToko & "', " &
                " Alamat            = '" & Alamat & "', " &
                " Coa_Kas           = '" & COAKas & "', " &
                " Deskripsi         = '" & Deskripsi & "' " &
                " WHERE Kode_Toko   = '" & KodeToko & "' "

        End If

        cmd = New OdbcCommand(QuerySimpan, KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase Then
            pesan_DataBerhasilDisimpan()
            If usc_DataToko.StatusAktif Then usc_DataToko.TampilkanData()
            Me.Close()
        Else
            pesan_DataGagalDisimpan()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub




    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeToko.MaxLength = 12
        txt_COAKas.IsReadOnly = True
    End Sub

End Class
