Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports BookuID.Styles

''' <summary>
''' Window untuk browse file lokal atau remote untuk transfer.
''' </summary>
Class wpfWin_FileBrowser

#Region "Model untuk DataGrid"

    ''' <summary>Model untuk menampilkan item di DataGrid</summary>
    Public Class FileItem
        Public Property Nama As String = ""
        Public Property IsFolder As Boolean = False
        Public Property Ukuran As Long = 0
        Public Property TanggalModifikasi As DateTime = DateTime.Now
        Public Property PathLengkap As String = ""

        Public ReadOnly Property Icon As String
            Get
                Return If(IsFolder, "📁", "📄")
            End Get
        End Property

        Public ReadOnly Property UkuranFormatted As String
            Get
                If IsFolder Then Return ""
                Return mdl_TransferBerkas.FormatUkuranFile(Ukuran)
            End Get
        End Property

        Public ReadOnly Property TanggalFormatted As String
            Get
                Return TanggalModifikasi.ToString("yyyy-MM-dd HH:mm")
            End Get
        End Property
    End Class

#End Region

#Region "Properties"

    ''' <summary>True jika mode browse file lokal (upload)</summary>
    Public Property ModeLokal As Boolean = True

    ''' <summary>True jika koneksi via Relay (mode Internet)</summary>
    Public Property ModeViaRelay As Boolean = False

    ''' <summary>Path folder saat ini</summary>
    Public Property PathSaatIni As String = ""

    ''' <summary>Path parent folder</summary>
    Public Property PathParent As String = ""

    ''' <summary>File yang dipilih untuk transfer</summary>
    Public Property FileTerpilih As FileItem = Nothing

    ''' <summary>True jika user menekan tombol Transfer</summary>
    Public Property HasilTransfer As Boolean = False

#End Region

#Region "Private Fields"

    Private _items As New ObservableCollection(Of FileItem)
    Private _sedangMemuat As Boolean = False

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        datagridUtama.ItemsSource = _items
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Subscribe ke event sesuai mode koneksi
        If ModeViaRelay Then
            ' Mode Internet: gunakan event dari mdl_KoneksiRelay
            AddHandler mdl_KoneksiRelay.DaftarFolderDiterimaViaRelay, AddressOf OnDaftarFolderDiterima
        Else
            ' Mode LAN: gunakan event dari mdl_KoneksiJaringan
            AddHandler mdl_KoneksiJaringan.DaftarFolderDiterima, AddressOf OnDaftarFolderDiterima
        End If

        ' Load folder awal
        MuatFolder("")
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe event sesuai mode koneksi
        If ModeViaRelay Then
            RemoveHandler mdl_KoneksiRelay.DaftarFolderDiterimaViaRelay, AddressOf OnDaftarFolderDiterima
        Else
            RemoveHandler mdl_KoneksiJaringan.DaftarFolderDiterima, AddressOf OnDaftarFolderDiterima
        End If
    End Sub

#End Region

#Region "Button Handlers"

    Private Sub rdo_Mode_Checked(sender As Object, e As RoutedEventArgs)
        ' Guard: Jangan proses jika kontrol belum diinisialisasi (dipanggil dari InitializeComponent)
        If lbl_Selected Is Nothing Then Return

        ModeLokal = (rdo_ModeLokal.IsChecked.GetValueOrDefault(False))
        FileTerpilih = Nothing
        lbl_Selected.Text = "(tidak ada)"
        btn_Transfer.IsEnabled = False
        MuatFolder("")
    End Sub

    Private Sub btn_Up_Click(sender As Object, e As RoutedEventArgs)
        If Not String.IsNullOrEmpty(PathParent) Then
            MuatFolder(PathParent)
        ElseIf Not String.IsNullOrEmpty(PathSaatIni) Then
            ' Kembali ke root
            MuatFolder("")
        End If
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs)
        MuatFolder(PathSaatIni)
    End Sub

    Private Sub btn_Transfer_Click(sender As Object, e As RoutedEventArgs)
        If FileTerpilih IsNot Nothing AndAlso Not FileTerpilih.IsFolder Then
            HasilTransfer = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs)
        HasilTransfer = False
        Me.Close()
    End Sub

#End Region

#Region "DataGrid Events"

    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        FileTerpilih = TryCast(datagridUtama.SelectedItem, FileItem)

        If FileTerpilih IsNot Nothing Then
            If FileTerpilih.IsFolder Then
                lbl_Selected.Text = $"Folder: {FileTerpilih.Nama}"
                btn_Transfer.IsEnabled = False
            Else
                lbl_Selected.Text = $"{FileTerpilih.Nama} ({FileTerpilih.UkuranFormatted})"
                btn_Transfer.IsEnabled = True
            End If
        Else
            lbl_Selected.Text = "(tidak ada)"
            btn_Transfer.IsEnabled = False
        End If
    End Sub

    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Dim item = TryCast(datagridUtama.SelectedItem, FileItem)
        If item Is Nothing Then Return

        If item.IsFolder Then
            ' Masuk ke folder
            MuatFolder(item.PathLengkap)
        Else
            ' Double-click file = langsung transfer
            FileTerpilih = item
            HasilTransfer = True
            Me.Close()
        End If
    End Sub

#End Region

#Region "Load Folder"

    ''' <summary>
    ''' Memuat daftar file dan folder.
    ''' </summary>
    ''' <param name="path">Path folder (kosong = root)</param>
    Private Sub MuatFolder(path As String)
        If _sedangMemuat Then Return
        _sedangMemuat = True

        _items.Clear()
        pnl_Loading.Visibility = Visibility.Visible
        btn_Transfer.IsEnabled = False
        FileTerpilih = Nothing
        lbl_Selected.Text = "(tidak ada)"

        If ModeLokal Then
            ' Mode lokal - langsung baca dari filesystem
            MuatFolderLokal(path)
        Else
            ' Mode remote - kirim request ke Host (sesuai mode koneksi)
            Task.Run(Async Function()
                         If ModeViaRelay Then
                             ' Mode Internet: Kirim via Relay
                             Await mdl_KoneksiRelay.KirimPermintaanDaftarFolderViaRelayAsync(path)
                         Else
                             ' Mode LAN: Kirim langsung
                             Await mdl_KoneksiJaringan.KirimPermintaanDaftarFolderAsync(path)
                         End If
                     End Function)
        End If
    End Sub

    ''' <summary>
    ''' Memuat folder lokal.
    ''' </summary>
    Private Sub MuatFolderLokal(path As String)
        Try
            Dim response As cls_PayloadResponDaftarFolder

            If String.IsNullOrEmpty(path) Then
                ' Tampilkan daftar drive
                response = mdl_TransferBerkas.DapatkanDaftarDrive()
            Else
                response = mdl_TransferBerkas.DapatkanDaftarFolder(path)
            End If

            TampilkanHasil(response)

        Catch ex As Exception
            WriteLog($"[BROWSER] Error muat folder lokal: {ex.Message}")
            MessageBox.Show($"Gagal memuat folder: {ex.Message}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
            _sedangMemuat = False
            pnl_Loading.Visibility = Visibility.Collapsed
        End Try
    End Sub

    ''' <summary>
    ''' Handler ketika daftar folder diterima dari Host.
    ''' </summary>
    Private Sub OnDaftarFolderDiterima(payload As cls_PayloadResponDaftarFolder)
        ' Marshal ke UI thread
        Dispatcher.Invoke(Sub()
                              TampilkanHasil(payload)
                          End Sub)
    End Sub

    ''' <summary>
    ''' Menampilkan hasil daftar folder.
    ''' </summary>
    Private Sub TampilkanHasil(response As cls_PayloadResponDaftarFolder)
        _items.Clear()

        If Not response.Sukses Then
            MessageBox.Show($"Gagal memuat folder: {response.Pesan}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
            _sedangMemuat = False
            pnl_Loading.Visibility = Visibility.Collapsed
            Return
        End If

        PathSaatIni = response.Path
        PathParent = response.ParentPath
        txt_Path.Text = If(String.IsNullOrEmpty(PathSaatIni), "Drives", PathSaatIni)

        ' Tambahkan items
        If response.Items IsNot Nothing Then
            ' Sort: folder dulu, kemudian file
            Dim sorted = response.Items.OrderByDescending(Function(x) x.IsFolder).
                                        ThenBy(Function(x) x.Nama)

            For Each item In sorted
                Dim fileItem As New FileItem With {
                    .Nama = item.Nama,
                    .IsFolder = item.IsFolder,
                    .Ukuran = item.Ukuran,
                    .TanggalModifikasi = item.TanggalModifikasi,
                    .PathLengkap = item.PathLengkap
                }
                _items.Add(fileItem)
            Next
        End If

        _sedangMemuat = False
        pnl_Loading.Visibility = Visibility.Collapsed
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Reset form ke kondisi awal.
    ''' </summary>
    Public Sub ResetForm()
        ModeLokal = True
        rdo_ModeLokal.IsChecked = True
        rdo_ModeRemote.IsChecked = False
        PathSaatIni = ""
        PathParent = ""
        FileTerpilih = Nothing
        HasilTransfer = False
        _items.Clear()
        lbl_Selected.Text = "(tidak ada)"
        btn_Transfer.IsEnabled = False
    End Sub

#End Region

End Class
