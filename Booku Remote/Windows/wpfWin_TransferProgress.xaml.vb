Option Explicit On
Option Strict On

Imports System.Windows
Imports BookuID.Styles

''' <summary>
''' Window untuk menampilkan progress transfer file.
''' </summary>
Class wpfWin_TransferProgress

#Region "Properties"

    ''' <summary>ID transfer yang sedang dipantau</summary>
    Public Property TransferId As String = ""

    ''' <summary>Nama file yang ditransfer</summary>
    Public Property NamaFile As String = ""

    ''' <summary>Ukuran file total</summary>
    Public Property UkuranFile As Long = 0

    ''' <summary>True jika transfer sukses</summary>
    Public Property TransferSukses As Boolean = False

    ''' <summary>True jika transfer dibatalkan oleh user</summary>
    Public Property Dibatalkan As Boolean = False

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        StyleWindowDialogWPF_TanpaTombolX(Me)
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Subscribe ke events dari mdl_TransferBerkas
        AddHandler mdl_TransferBerkas.TransferProgressChanged, AddressOf OnProgressChanged
        AddHandler mdl_TransferBerkas.TransferCompleted, AddressOf OnTransferCompleted

        ' Set info file
        lbl_NamaFile.Text = NamaFile
        lbl_InfoFile.Text = mdl_TransferBerkas.FormatUkuranFile(UkuranFile)

        WriteLog($"[PROGRESS] Window dibuka untuk transfer: {TransferId}")
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe events
        RemoveHandler mdl_TransferBerkas.TransferProgressChanged, AddressOf OnProgressChanged
        RemoveHandler mdl_TransferBerkas.TransferCompleted, AddressOf OnTransferCompleted
    End Sub

#End Region

#Region "Button Handlers"

    Private Async Sub btn_Batalkan_Click(sender As Object, e As RoutedEventArgs)
        If MessageBox.Show("Yakin ingin membatalkan transfer?", "Konfirmasi",
                          MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            Dibatalkan = True
            btn_Batalkan.IsEnabled = False
            lbl_Status.Text = "Membatalkan..."

            ' Kirim paket batal
            Await mdl_KoneksiJaringan.BatalkanTransferAsync(TransferId, "Dibatalkan oleh user")
        End If
    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Handler ketika progress berubah.
    ''' </summary>
    Private Sub OnProgressChanged(transferId As String, persentase As Double, kecepatan As Double, estimasiWaktu As Double)
        ' Filter hanya untuk transfer ini
        If transferId <> Me.TransferId Then Return

        ' Marshal ke UI thread
        Dispatcher.Invoke(Sub()
                              pgb_Progress.Value = persentase
                              lbl_Persentase.Text = $"{persentase:F1}%"

                              ' Kecepatan
                              lbl_Kecepatan.Text = $"{mdl_TransferBerkas.FormatUkuranFile(CLng(kecepatan))}/s"

                              ' Estimasi waktu
                              If estimasiWaktu > 0 Then
                                  lbl_Waktu.Text = mdl_TransferBerkas.FormatWaktu(estimasiWaktu)
                              Else
                                  lbl_Waktu.Text = "-"
                              End If

                              ' Bytes info
                              Dim transfer = mdl_TransferBerkas.GetTransfer(transferId)
                              If transfer IsNot Nothing Then
                                  lbl_BytesInfo.Text = $"{mdl_TransferBerkas.FormatUkuranFile(transfer.BytesTerkirim)} / {mdl_TransferBerkas.FormatUkuranFile(transfer.UkuranFile)}"
                              End If

                              lbl_Status.Text = "Mentransfer..."
                          End Sub)
    End Sub

    ''' <summary>
    ''' Handler ketika transfer selesai.
    ''' </summary>
    Private Sub OnTransferCompleted(transferId As String, sukses As Boolean, pesan As String)
        ' Filter hanya untuk transfer ini
        If transferId <> Me.TransferId Then Return

        ' Marshal ke UI thread
        Dispatcher.Invoke(Sub()
                              TransferSukses = sukses

                              If sukses Then
                                  pgb_Progress.Value = 100
                                  lbl_Persentase.Text = "100%"
                                  lbl_Status.Text = "Selesai!"
                                  lbl_Kecepatan.Text = "-"
                                  lbl_Waktu.Text = "-"
                                  btn_Batalkan.Content = "Tutup"
                              Else
                                  lbl_Status.Text = pesan
                                  btn_Batalkan.Content = "Tutup"
                              End If

                              btn_Batalkan.IsEnabled = True

                              ' Ubah behavior tombol untuk tutup
                              RemoveHandler btn_Batalkan.Click, AddressOf btn_Batalkan_Click
                              AddHandler btn_Batalkan.Click, Sub(s, ev) Me.Close()
                          End Sub)
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Reset form ke kondisi awal.
    ''' </summary>
    Public Sub ResetForm()
        TransferId = ""
        NamaFile = ""
        UkuranFile = 0
        TransferSukses = False
        Dibatalkan = False

        pgb_Progress.Value = 0
        lbl_Persentase.Text = "0%"
        lbl_Status.Text = "Memulai..."
        lbl_Kecepatan.Text = "0 KB/s"
        lbl_Waktu.Text = "-"
        lbl_BytesInfo.Text = "0 KB / 0 KB"
        btn_Batalkan.Content = "Batalkan"
        btn_Batalkan.IsEnabled = True
    End Sub

    ''' <summary>
    ''' Set info transfer.
    ''' </summary>
    Public Sub SetTransferInfo(id As String, nama As String, ukuran As Long)
        TransferId = id
        NamaFile = nama
        UkuranFile = ukuran
    End Sub

#End Region

End Class
