Imports bcomm
Imports System.Windows

Public Class wpfWin_InputSJBASTManual

    'Public Properties - Parameter Input :
    Public JalurMasuk As String

    'Public Properties untuk set label text dari caller :
    Public Label_Nomor As String = "Nomor SJ/BAST :"
    Public Label_Tanggal As String = "Tanggal SJ/BAST :"
    Public Label_TanggalDiterima As String = "Tanggal Diterima :"

    'Public Properties untuk set tanggal dari caller :
    Public TanggalSJBAST As Date = Today
    Public TanggalDiterima As Date = Today

    'Variabel Internal :
    Dim NomorSJBAST As String


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If String.IsNullOrEmpty(JalurMasuk) Then
            PesanUntukProgrammer("Jalur Masuk belum ditentukan...!!!")
        End If

        'Set label text :
        lbl_Nomor.Text = Label_Nomor
        lbl_Tanggal.Text = Label_Tanggal
        lbl_TanggalDiterima.Text = Label_TanggalDiterima

        'Set tanggal :
        dtp_TanggalSJBAST.SelectedDate = TanggalFormatWPF(TanggalSJBAST)
        dtp_TanggalDiterima.SelectedDate = TanggalFormatWPF(TanggalDiterima)

        txt_NomorSJBAST.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        JalurMasuk = Kosongan
        Label_Nomor = "Nomor SJ/BAST :"
        Label_Tanggal = "Tanggal SJ/BAST :"
        Label_TanggalDiterima = "Tanggal Diterima :"
        TanggalSJBAST = Today
        TanggalDiterima = Today
        NomorSJBAST = Kosongan
        txt_NomorSJBAST.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Private Sub txt_NomorSJBAST_TextChanged(sender As Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txt_NomorSJBAST.TextChanged
        NomorSJBAST = txt_NomorSJBAST.Text
    End Sub


    Private Sub dtp_TanggalSJBAST_SelectedDateChanged(sender As Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles dtp_TanggalSJBAST.SelectedDateChanged
        If dtp_TanggalSJBAST.SelectedDate.HasValue Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalSJBAST)
            If dtp_TanggalDiterima.SelectedDate.HasValue Then
                KunciTanggalBulanDanTahun_TidakBolehLebihDari_WPF(dtp_TanggalSJBAST, dtp_TanggalDiterima.SelectedDate.Value.Day, dtp_TanggalDiterima.SelectedDate.Value.Month, dtp_TanggalDiterima.SelectedDate.Value.Year)
            End If
        End If
    End Sub


    Private Sub dtp_TanggalDiterima_SelectedDateChanged(sender As Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles dtp_TanggalDiterima.SelectedDateChanged
        If dtp_TanggalDiterima.SelectedDate.HasValue Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalDiterima)
        End If
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click

        Dim TanggalSJBAST_Formatted As String = TanggalFormatTampilan(dtp_TanggalSJBAST.SelectedDate.Value)
        Dim TanggalDiterima_Formatted As String = TanggalFormatTampilan(dtp_TanggalDiterima.SelectedDate.Value)

        Select Case JalurMasuk
            Case Form_INPUTINVOICEPEMBELIAN
                win_InputInvoicePembelian.datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST_Formatted, TanggalDiterima_Formatted, Kosongan, 0)
            Case Form_INPUTINVOICEPENJUALAN
                win_InputInvoicePenjualan.datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST_Formatted, TanggalDiterima_Formatted, Kosongan, 0)
        End Select

        Me.Close()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub


End Class
