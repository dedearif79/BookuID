Imports bcomm
Imports System.Windows
Imports System.Windows.Controls

Public Class wpfWin_InputJurnalPerTransaksi

    ' === PUBLIC PROPERTIES (KOMUNIKASI DENGAN PARENT) ===
    Public JalurMasuk As String
    Public FungsiForm As String
    Public COATerseleksi As String
    Public NamaAkunTerseleksi As String
    Public DK As String
    Public JumlahDebet As Int64
    Public JumlahKredit As Int64
    Public JumlahTransaksi As Int64
    Public TombolPenutup As String = "BATAL"

    ' === PUBLIC PROPERTIES (INPUT UNTUK MODE EDIT) ===
    Public KodeAkunInput As String
    Public NamaAkunInput As String
    Public DKInput As String            ' "D" atau "K"
    Public JumlahTransaksiInput As Int64

    ' === GUARD FLAGS ===
    Dim ProsesLoadingForm As Boolean = False
    Dim ProsesResetForm As Boolean = False

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)

        txt_NamaAkun.IsReadOnly = True
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                ResetForm()
                txt_COA.IsEnabled = True
                btn_PilihCOA.IsEnabled = True
                cmb_DK.IsEnabled = True
                txt_JumlahTransaksi.IsEnabled = True

            Case FungsiForm_EDIT
                ResetForm()
                IsiValueForm()
                txt_COA.IsEnabled = False
                btn_PilihCOA.IsEnabled = False
                cmb_DK.IsEnabled = True
                txt_JumlahTransaksi.IsEnabled = True
        End Select

        txt_COA.Focus()
        TombolPenutup = "BATAL"

        ProsesLoadingForm = False

    End Sub

    Sub IsiValueForm()
        ' Isi kontrol dari public properties input
        txt_COA.Text = KodeAkunInput
        txt_NamaAkun.Text = NamaAkunInput

        If DKInput = "D" Then
            cmb_DK.SelectedIndex = 0    ' DEBET
        ElseIf DKInput = "K" Then
            cmb_DK.SelectedIndex = 1    ' KREDIT
        End If

        txt_JumlahTransaksi.Text = JumlahTransaksiInput.ToString()
    End Sub

    Sub KontenComboDK()
        cmb_DK.Items.Clear()
        cmb_DK.Items.Add("DEBET")
        cmb_DK.Items.Add("KREDIT")
        cmb_DK.SelectedIndex = -1
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_COA.IsEnabled = True
        btn_PilihCOA.IsEnabled = True
        txt_NamaAkun.IsEnabled = False
        cmb_DK.IsEnabled = True
        txt_JumlahTransaksi.IsEnabled = True

        txt_COA.Text = Kosongan
        txt_NamaAkun.Text = Kosongan
        KontenComboDK()
        txt_JumlahTransaksi.Text = Kosongan

        COATerseleksi = Kosongan
        NamaAkunTerseleksi = Kosongan
        DK = Kosongan
        JumlahTransaksi = 0
        TombolPenutup = "BATAL"

        ProsesResetForm = False

    End Sub

    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Semua
        win_ListCOA.ShowDialog()
        txt_COA.Text = win_ListCOA.COATerseleksi
        txt_NamaAkun.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        If ProsesLoadingForm Then Return
        If ProsesResetForm Then Return
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click

        ' === VALIDASI INPUT ===
        If txt_COA.Text = Kosongan Then
            Pesan_Peringatan("Silakan isi 'Kode Akun'.")
            txt_COA.Focus()
            Return
        End If

        If txt_NamaAkun.Text = Kosongan Then
            Pesan_Peringatan("Silakan isi 'Nama Akun'.")
            txt_NamaAkun.Focus()
            Return
        End If

        ' === VALIDASI DUPLIKAT (untuk TAMBAH) ===
        If FungsiForm = FungsiForm_TAMBAH Then
            If win_InputJurnal.datatabelUtama IsNot Nothing Then
                For Each row As DataRow In win_InputJurnal.datatabelUtama.Rows
                    Dim KodeAkun = row("Kode_Akun").ToString()
                    If txt_COA.Text = KodeAkun Then
                        Pesan_Peringatan("Akun '" & txt_NamaAkun.Text & "' sudah ada pada jurnal ini." _
                                       & Enter2Baris & "Silakan input akun yang lain, atau edit akun yang sudah ada.")
                        Return
                    End If
                Next
            End If
        End If

        If cmb_DK.SelectedIndex < 0 Then
            Pesan_Peringatan("Silakan pilih 'Debet/Kredit'.")
            cmb_DK.Focus()
            Return
        End If

        If AmbilAngka(txt_JumlahTransaksi.Text) = 0 Then
            Pesan_Peringatan("Silakan isi 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Text = Kosongan
            txt_JumlahTransaksi.Focus()
            Return
        End If

        ' === SET RETURN VALUES ===
        COATerseleksi = txt_COA.Text
        NamaAkunTerseleksi = txt_NamaAkun.Text

        If cmb_DK.SelectedItem.ToString() = "DEBET" Then
            DK = "D"
        End If
        If cmb_DK.SelectedItem.ToString() = "KREDIT" Then
            DK = "K"
        End If

        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        TombolPenutup = "OK"
        Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        TombolPenutup = "BATAL"
        Me.Close()
    End Sub

    Private Sub cmb_DK_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DK.SelectionChanged
        If ProsesLoadingForm Then Return
        If ProsesResetForm Then Return
    End Sub

End Class
