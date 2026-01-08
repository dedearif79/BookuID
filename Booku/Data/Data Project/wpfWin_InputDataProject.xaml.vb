Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc


Public Class wpfWin_InputDataProject

    Public JudulForm
    Public FungsiForm
    Public ProsesSuntingDatabase As Boolean

    Dim NomorID
    Dim KodeProject
    Dim NamaProject
    Dim NomorPO
    Dim KodeCustomer
    Dim NamaCustomer
    Dim NilaiProject
    Dim Keterangan
    Dim Status


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Kode Project"
            txt_KodeProject.IsReadOnly = False
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Kode Project"
            txt_KodeProject.IsReadOnly = True
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        ProsesSuntingDatabase = False

        NomorID = 0
        txt_KodeProject.Text = Kosongan
        txt_KodeProject.IsReadOnly = False
        txt_NamaProject.Text = Kosongan
        txt_NomorPO.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_NilaiProject.Text = Kosongan
        txt_Keterangan.Text = Kosongan
        KontenCombo_Status()

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_Status()
        cmb_Status.Items.Clear()
        cmb_Status.Items.Add(Status_Open)
        cmb_Status.Items.Add(Status_Closed)
        cmb_Status.Text = Kosongan
    End Sub


    Private Sub txt_NomorPO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorPO.TextChanged
        NomorPO = txt_NomorPO.Text
    End Sub


    Private Sub txt_KodeProject_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeProject.TextChanged
        KodeProject = txt_KodeProject.Text
    End Sub

    Private Sub txt_KodeProject_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_KodeProject.LostFocus
        If FungsiForm <> FungsiForm_TAMBAH Then Return
        If KodeProject = Kosongan Then Return

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand("SELECT Kode_Project FROM tbl_DataProject WHERE Kode_Project = '" & KodeProject & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            PesanPeringatan("Kode Project '" & KodeProject & "' sudah ada." & Enter2Baris &
                   "Silakan isi Kode Project yang lain..!")
            txt_KodeProject.Text = Kosongan
            txt_KodeProject.Focus()
        End If
        AksesDatabase_General(Tutup)
    End Sub


    Private Sub txt_NamaProject_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaProject.TextChanged
        NamaProject = txt_NamaProject.Text
    End Sub


    Private Sub txt_KodeCustomer_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles txt_KodeCustomer.PreviewMouseLeftButtonUp
        btn_PilihMitra_Click(sender, e)
    End Sub

    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub


    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
    End Sub


    Private Sub txt_NilaiProject_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_NilaiProject.PreviewTextInput
    End Sub

    Private Sub txt_NilaiProject_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NilaiProject.TextChanged
        NilaiProject = AmbilAngka(txt_NilaiProject.Text)
        PemecahRibuanUntukTextBox_WPF(txt_NilaiProject)
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub


    Private Sub cmb_Status_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Status.SelectionChanged
        Status = cmb_Status.SelectedItem
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Pengisian Ulang Variabel2 Tertentu :
        Keterangan = txt_Keterangan.Text

        If KodeProject = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Kode Project'.")
            txt_KodeProject.Focus()
            Return
        End If

        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan isi data 'Customer'.")
            btn_PilihMitra_Click(sender, e)
            Return
        End If

        If Status = Kosongan Or Status Is Nothing Then
            PesanPeringatan("Silakan pilih 'Status'.")
            cmb_Status.Focus()
            Return
        End If

        ProsesSuntingDatabase = False

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_DataProject") + 1

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_DataProject VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & KodeProject & "', " &
                                  " '" & NamaProject & "', " &
                                  " '" & NomorPO & "', " &
                                  " '" & KodeCustomer & "', " &
                                  " '" & NamaCustomer & "', " &
                                  " '" & NilaiProject & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & Status & "') ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try

            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_DataProject SET " &
                                  " Nama_Project        = '" & NamaProject & "', " &
                                  " Nomor_PO            = '" & NomorPO & "', " &
                                  " Kode_Customer       = '" & KodeCustomer & "', " &
                                  " Nama_Customer       = '" & NamaCustomer & "', " &
                                  " Nilai_Project       = '" & NilaiProject & "', " &
                                  " Keterangan          = '" & Keterangan & "', " &
                                  " Status              = '" & Status & "' " &
                                  " WHERE Kode_Project  = '" & KodeProject & "' ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try

            AksesDatabase_General(Tutup)

        End If

        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_DataProject.StatusAktif Then usc_DataProject.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_Status.IsReadOnly = True
        txt_KodeProject.MaxLength = 99
        txt_NamaProject.MaxLength = 99
        txt_NomorPO.MaxLength = 99
        txt_NilaiProject.MaxLength = 21
        txt_KodeCustomer.IsReadOnly = True
        txt_NamaCustomer.IsReadOnly = True
    End Sub

End Class
