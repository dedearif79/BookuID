Imports bcomm
Imports Microsoft.Win32
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports ClosedXML.Excel
Imports System.Windows.Data

Public Class wpfWin_ProgresExport_EXCEL

    Public datagridBahanEkspor As DataGrid
    Public datatableBahanEkspor As DataTable
    Dim JumlahBarisBahanExport
    Dim JumlahBarisTerexport
    Dim ProsesExport As Boolean
    Dim JumlahKolom
    Dim Kolom As Integer
    Dim NamaFileExportEXCEL

    Dim excelRow As Integer
    Dim excelCol As Integer
    Dim workbook As XLWorkbook
    Dim worksheet

    Public SumberData As String
    Public SumberData_DataGrid As String = "DataGrid"
    Public SumberData_DataTable As String = "DataTable"


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case SumberData
            Case SumberData_DataTable
                ProsesEkspor_DataTable()
            Case SumberData_DataGrid
                ProsesEkspor_DataGrid()
        End Select

        ProsesLoadingForm = False
    End Sub

    Sub ProsesEkspor_DataTable()

        If datatableBahanEkspor Is Nothing Then
            Pesan_Peringatan("Sumber DataTable untuk ekspor belum diset.")
            TutupForm()
            Return
        End If

        JumlahBarisBahanExport = datatableBahanEkspor.Rows.Count
        If JumlahBarisBahanExport = 0 Then
            Pesan_Peringatan("Tidak ada bahan data yang akan di-export.")
            TutupForm()
            Return
        Else
            'Dialog Penyimpanan (Save As):
            sfd_Simpan.FileName = Kosongan
            sfd_Simpan.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd_Simpan.ShowDialog()
            If sfd_Simpan.FileName = Kosongan Then
                TutupForm()
                Return
            End If
        End If

        NamaFileExportEXCEL = sfd_Simpan.FileName.ToString

        pgb_Progress.Maximum = JumlahBarisBahanExport
        pgb_Progress.Value = 3
        lbl_ProgressBaris1.Visibility = Visibility.Visible
        lbl_ProgressBaris1.Text = "Harap tunggu. Proses ekspor sedang berjalan."

        workbook = New XLWorkbook
        worksheet = workbook.Worksheets.Add("Data Export")

        Try
            '=========================================================
            ' 2) HEADER (tetap mengikuti DataGrid: visible + lebar kolom)
            '=========================================================
            excelCol = 1
            For Each column As DataGridColumn In datagridBahanEkspor.Columns
                If column.Visibility = Visibility.Visible Then
                    Dim headerText As String =
                    If(TypeOf column.Header Is TextBlock,
                       DirectCast(column.Header, TextBlock).Text,
                       If(column.Header IsNot Nothing, column.Header.ToString(), ""))

                    worksheet.Cell(1, excelCol).Value = headerText
                    worksheet.Cell(1, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    'Lebar kolom (opsional, mengikuti DataGrid)
                    If column.ActualWidth > 0 Then
                        worksheet.Column(excelCol).Width = column.ActualWidth / 6.3
                    End If

                    excelCol += 1
                End If
            Next

            '=========================================================
            ' 3) DATA (ambil dari DataTable, mapping berdasarkan binding kolom)
            '=========================================================
            excelRow = 2

            For r As Integer = 0 To datatableBahanEkspor.Rows.Count - 1

                excelCol = 1

                For Each column As DataGridColumn In datagridBahanEkspor.Columns
                    If column.Visibility = Visibility.Visible Then

                        Dim namaKolomDT As String = AmbilNamaKolomDataTableDariKolomDataGrid(column)

                        Dim objVal As Object = Nothing

                        'Prioritas 1: jika binding ketemu dan kolom ada di DataTable
                        If Not String.IsNullOrWhiteSpace(namaKolomDT) AndAlso datatableBahanEkspor.Columns.Contains(namaKolomDT) Then
                            objVal = datatableBahanEkspor.Rows(r)(namaKolomDT)
                        Else
                            'Prioritas 2: fallback coba cocokkan dengan Header
                            Dim headerText As String =
                            If(TypeOf column.Header Is TextBlock,
                               DirectCast(column.Header, TextBlock).Text,
                               If(column.Header IsNot Nothing, column.Header.ToString(), ""))

                            If datatableBahanEkspor.Columns.Contains(headerText) Then
                                objVal = datatableBahanEkspor.Rows(r)(headerText)
                            End If
                        End If

                        Dim IsiCell As String = Kosongan
                        If objVal Is Nothing OrElse objVal Is DBNull.Value Then
                            IsiCell = Kosongan
                        Else
                            IsiCell = objVal.ToString()
                        End If

                        '=========================================================
                        ' 4) FORMAT (logika lama Anda tetap dipakai)
                        '=========================================================
                        If CellFormatTanggalStrip(IsiCell) Then
                            worksheet.Cell(excelRow, excelCol).Value = GantiTeks(IsiCell, "-", "/")
                            worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/MM/yyyy"
                            worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        ElseIf CellFormatTanggalSlash(IsiCell) Then
                            worksheet.Cell(excelRow, excelCol).Value = IsiCell
                            worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/MM/yyyy"
                            worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        ElseIf CellFormatAngka(IsiCell) Then
                            worksheet.Cell(excelRow, excelCol).Value = AmbilAngka(IsiCell)
                            worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        ElseIf IsiCell = StripKosong Then
                            worksheet.Cell(excelRow, excelCol).Value = Kosongan
                        Else
                            worksheet.Cell(excelRow, excelCol).Value = IsiCell
                        End If

                        excelCol += 1
                    End If
                Next

                excelRow += 1

                'Progress (opsional, kalau Anda update per baris)
                If pgb_Progress.Value < pgb_Progress.Maximum Then
                    pgb_Progress.Value += 1
                End If

            Next

            ProsesExport = True

        Catch ex As Exception
            ProsesExport = False
            'Saran: log kan ex.Message agar ketahuan kalau ada error “diam-diam”
            'TulisLog("ExportError", ex.ToString)
        End Try

        Bgw_Export.RunWorkerAsync()

    End Sub
    Private Function AmbilNamaKolomDataTableDariKolomDataGrid(col As DataGridColumn) As String

        'Paling sering: DataGridTextColumn / DataGridCheckBoxColumn (turunan DataGridBoundColumn)
        Dim boundCol = TryCast(col, DataGridBoundColumn)
        If boundCol IsNot Nothing Then
            Dim b = TryCast(boundCol.Binding, Binding)
            If b IsNot Nothing AndAlso b.Path IsNot Nothing Then
                Return b.Path.Path 'nama field
            End If
        End If

        'Fallback: kadang dipakai SortMemberPath
        If Not String.IsNullOrWhiteSpace(col.SortMemberPath) Then
            Return col.SortMemberPath
        End If

        Return ""
    End Function


    Sub ProsesEkspor_DataGrid()

        datagridBahanEkspor.EnableRowVirtualization = False

        JumlahBarisBahanExport = datagridBahanEkspor.Items.Count
        If JumlahBarisBahanExport = 0 Then
            Pesan_Peringatan("Tidak ada bahan data yang akan di-export.")
            TutupForm()
            Return
        Else
            'Dialog Penyimpanan (Save As) :
            sfd_Simpan.FileName = Kosongan
            sfd_Simpan.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd_Simpan.ShowDialog()
            If sfd_Simpan.FileName = Kosongan Then
                TutupForm()
                Return
            End If
        End If

        NamaFileExportEXCEL = sfd_Simpan.FileName.ToString
        pgb_Progress.Maximum = JumlahBarisBahanExport
        pgb_Progress.Value = 3
        lbl_ProgressBaris1.Visibility = Visibility.Visible
        lbl_ProgressBaris1.Text = "Harap tunggu. Proses ekspor sedang berjalan."

        workbook = New XLWorkbook
        worksheet = workbook.Worksheets.Add("Data Export")

        Try

            ' Menambahkan header dan menentukan lebar kolom
            excelCol = 1
            For Each column As DataGridColumn In datagridBahanEkspor.Columns
                If column.Visibility = Visibility.Visible Then
                    Dim headerText As String = If(TypeOf column.Header Is TextBlock, DirectCast(column.Header, TextBlock).Text, column.Header.ToString())
                    worksheet.Cell(1, excelCol).Value = headerText
                    worksheet.Cell(1, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    worksheet.Column(excelCol).Width = column.ActualWidth / 6.3 ' Konversi dari pixel ke karakter Excel, asumsi 6.3 pixel per karakter
                    excelCol += 1
                End If
            Next

            'Menambahkan Data
            excelRow = 2
            For Each item As Object In datagridBahanEkspor.Items
                excelCol = 1
                For Each column As DataGridColumn In datagridBahanEkspor.Columns
                    If column.Visibility = Visibility.Visible Then
                        Dim CellContent = column.GetCellContent(item)
                        If CellContent IsNot Nothing AndAlso TypeOf CellContent Is TextBlock Then
                            Dim IsiCell = DirectCast(CellContent, TextBlock).Text
                            If CellFormatTanggalStrip(IsiCell) Then 'Jika Cell Format Tanggal Strip (01-01-1900):
                                worksheet.Cell(excelRow, excelCol).Value = GantiTeks(IsiCell, "-", "/")
                                worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/mm/yyyy"
                                worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            ElseIf CellFormatTanggalSlash(IsiCell) Then 'Jika Cell Format Tanggal Slash (01/01/1900) :
                                worksheet.Cell(excelRow, excelCol).Value = IsiCell
                                worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/mm/yyyy"
                                worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            ElseIf CellFormatAngka_WPF(CellContent, IsiCell) Then 'Jika Cell Fornat Angka :
                                worksheet.Cell(excelRow, excelCol).Value = AmbilAngka(IsiCell)
                                worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            ElseIf CellFormatAngka(IsiCell) Then 'Untuk meng-cover, jika ada Cell Format Angka yang tidak rata kanan :
                                worksheet.Cell(excelRow, excelCol).Value = AmbilAngka(IsiCell)
                                worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                            ElseIf IsiCell = StripKosong Then 'Jika Cell berisi StripKosong ("-")
                                worksheet.Cell(excelRow, excelCol).Value = Kosongan
                            Else
                                worksheet.Cell(excelRow, excelCol).Value = IsiCell
                            End If
                        End If
                        excelCol += 1
                    End If
                Next
                excelRow += 1
            Next

            ProsesExport = True

        Catch ex As Exception

            ProsesExport = False

        End Try

        Bgw_Export.RunWorkerAsync()

    End Sub

    Sub ResetForm()

        pgb_Progress.Value = 0

        lbl_ProgressBaris1.Visibility = Visibility.Collapsed
        lbl_ProgressBaris2.Visibility = Visibility.Collapsed
        lbl_ProgressBaris3.Visibility = Visibility.Collapsed

        'lbl_ProgressBaris1.Foreground = New SolidColorBrush(Colors.Black)
        'lbl_ProgressBaris2.Foreground = New SolidColorBrush(Colors.Black)
        'lbl_ProgressBaris3.Foreground = New SolidColorBrush(Colors.Black)

        btn_Tutup.Content = "Batal"

    End Sub


    Sub TutupForm()
        Close()
    End Sub


    Private Sub Bgw_Export_DoWork(sender As Object, e As DoWorkEventArgs)

        System.Threading.Thread.Sleep(333)

    End Sub

    Private Sub Bgw_Export_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)

    End Sub


    Private Sub Bgw_Export_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

        btn_Tutup.Content = "Tutup"

        If ProsesExport = True Then
            Try
                workbook.SaveAs(NamaFileExportEXCEL) ' ( Menyimpan file )
                ProsesExport = True
            Catch ex As Exception
                ProsesExport = False
            End Try
        End If

        If ProsesExport = True Then
            pgb_Progress.Value = JumlahBarisBahanExport
            lbl_ProgressBaris1.Visibility = Visibility.Visible
            lbl_ProgressBaris1.Text = "Export data BERHASIL..!"
        Else
            lbl_ProgressBaris1.Visibility = Visibility.Visible
            lbl_ProgressBaris1.Text = "Export data GAGAL..!"
            'lbl_ProgressBaris1.Foreground = New SolidColorBrush(Colors.Red)
        End If

        datagridBahanEkspor.EnableRowVirtualization = True

    End Sub


    Dim sfd_Simpan As New Microsoft.Win32.SaveFileDialog
    Private WithEvents Bgw_Export As BackgroundWorker
    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)

        sfd_Simpan = New Microsoft.Win32.SaveFileDialog

        ' Inisialisasi BackgroundWorker
        Bgw_Export = New BackgroundWorker()
        Bgw_Export.WorkerReportsProgress = True
        Bgw_Export.WorkerSupportsCancellation = True

        ' Tambahkan event handlers
        AddHandler Bgw_Export.DoWork, AddressOf Bgw_Export_DoWork
        AddHandler Bgw_Export.RunWorkerCompleted, AddressOf Bgw_Export_RunWorkerCompleted
        AddHandler Bgw_Export.ProgressChanged, AddressOf Bgw_Export_ProgressChanged
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        TutupForm()
    End Sub


End Class
