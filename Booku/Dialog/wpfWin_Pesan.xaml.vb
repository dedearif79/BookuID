Imports System.Windows
Imports System.Windows.Media
Imports bcomm

Public Class wpfWin_Pesan

#Region "Konstanta Kategori"

    ' Konstanta untuk kategori pesan (menghindari masalah visibility enum)
    Public Const Kategori_Informasi As Integer = 0
    Public Const Kategori_Sukses As Integer = 1
    Public Const Kategori_Peringatan As Integer = 2
    Public Const Kategori_Gagal As Integer = 3
    Public Const Kategori_KesalahanTeknis As Integer = 4

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Kategori pesan (gunakan konstanta Kategori_*)
    ''' </summary>
    Public Property Kategori As Integer = Kategori_Informasi

    ''' <summary>
    ''' Isi pesan yang akan ditampilkan
    ''' </summary>
    Public Property IsiPesan As String = Kosongan

    ''' <summary>
    ''' Hasil konfirmasi (True = Ya, False = Tidak)
    ''' </summary>
    Public Property HasilKonfirmasi As Boolean = False

    ''' <summary>
    ''' Apakah menampilkan tombol Tidak (untuk konfirmasi)
    ''' </summary>
    Public Property TampilkanTombolTidak As Boolean = False

#End Region

#Region "Warna Tema"

    ' Warna header berdasarkan kategori
    Private ReadOnly WarnaSukses As Color = CType(ColorConverter.ConvertFromString("#388E3C"), Color)      ' Hijau
    Private ReadOnly WarnaInformasi As Color = CType(ColorConverter.ConvertFromString("#388E3C"), Color)  ' Hijau
    Private ReadOnly WarnaPeringatan As Color = CType(ColorConverter.ConvertFromString("#F57C00"), Color) ' Orange/Amber
    Private ReadOnly WarnaGagal As Color = CType(ColorConverter.ConvertFromString("#D32F2F"), Color)      ' Merah
    Private ReadOnly WarnaKonfirmasi As Color = CType(ColorConverter.ConvertFromString("#1976D2"), Color) ' Biru

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()

        ' CATATAN: Jangan panggil StyleWindowDialogWPF_Dasar() karena akan mengubah
        ' WindowStyle ke SingleBorderWindow yang konflik dengan AllowsTransparency=True.
        ' Hanya tambahkan handler untuk centering window.
        AddHandler Me.SizeChanged,
            Sub()
                Me.Left = (SystemParameters.PrimaryScreenWidth - Me.ActualWidth) / 2
                Me.Top = (SystemParameters.PrimaryScreenHeight - Me.ActualHeight) / 2
            End Sub
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Set tampilan berdasarkan kategori
        SetTampilanKategori()

        ' Set isi pesan
        txt_Pesan.Text = IsiPesan

        ' Tampilkan tombol Tidak jika konfirmasi
        If TampilkanTombolTidak Then
            btn_Tidak.Visibility = Visibility.Visible
            btn_OK.Content = "Ya"
            ' Gunakan warna konfirmasi (biru)
            bdr_Header.Background = New SolidColorBrush(WarnaKonfirmasi)
            txt_Judul.Text = "Konfirmasi"
            TampilkanIcon("Question")
        End If

        ' Set focus ke tombol OK
        btn_OK.Focus()
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click
        HasilKonfirmasi = True
        Me.Close()
    End Sub

    Private Sub btn_Tidak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tidak.Click
        HasilKonfirmasi = False
        Me.Close()
    End Sub

    ' Memungkinkan drag window dari header
    Private Sub bdr_Header_MouseLeftButtonDown(sender As Object, e As Input.MouseButtonEventArgs) Handles bdr_Header.MouseLeftButtonDown
        If e.LeftButton = Input.MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Set tampilan (warna header, icon, judul) berdasarkan kategori pesan
    ''' </summary>
    Private Sub SetTampilanKategori()
        Select Case Kategori
            Case Kategori_Sukses
                bdr_Header.Background = New SolidColorBrush(WarnaSukses)
                txt_Judul.Text = "Berhasil"
                TampilkanIcon("Info")

            Case Kategori_Informasi
                bdr_Header.Background = New SolidColorBrush(WarnaInformasi)
                txt_Judul.Text = "Informasi"
                TampilkanIcon("Info")

            Case Kategori_Peringatan
                bdr_Header.Background = New SolidColorBrush(WarnaPeringatan)
                txt_Judul.Text = "Peringatan"
                TampilkanIcon("Warning")

            Case Kategori_Gagal
                bdr_Header.Background = New SolidColorBrush(WarnaGagal)
                txt_Judul.Text = "Gagal"
                TampilkanIcon("Error")

            Case Kategori_KesalahanTeknis
                bdr_Header.Background = New SolidColorBrush(WarnaGagal)
                txt_Judul.Text = "Kesalahan Teknis"
                TampilkanIcon("Error")

            Case Else
                bdr_Header.Background = New SolidColorBrush(WarnaInformasi)
                txt_Judul.Text = "Informasi"
                TampilkanIcon("Info")
        End Select
    End Sub

    ''' <summary>
    ''' Tampilkan icon berdasarkan tipe
    ''' </summary>
    Private Sub TampilkanIcon(tipeIcon As String)
        ' Sembunyikan semua icon
        vbx_IconInfo.Visibility = Visibility.Collapsed
        vbx_IconWarning.Visibility = Visibility.Collapsed
        vbx_IconError.Visibility = Visibility.Collapsed
        vbx_IconQuestion.Visibility = Visibility.Collapsed

        ' Tampilkan icon yang sesuai
        Select Case tipeIcon
            Case "Info"
                vbx_IconInfo.Visibility = Visibility.Visible
            Case "Warning"
                vbx_IconWarning.Visibility = Visibility.Visible
            Case "Error"
                vbx_IconError.Visibility = Visibility.Visible
            Case "Question"
                vbx_IconQuestion.Visibility = Visibility.Visible
        End Select
    End Sub

#End Region

End Class
