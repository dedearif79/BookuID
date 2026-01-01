Imports bcomm
Imports System.Windows.Controls
Imports System.Windows

Public Class wpfWin_Pratinjau

    Public JenisFormCetak
    Public JudulForm
    Public TampilkanHeader As Boolean
    Public TampilkanFooter As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If TampilkanHeader = True Then
            usc_DesainHeader = New wpfUsc_DesainHeader
            DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainHeader)
        End If

        Select Case JenisFormCetak
            Case JenisFormCetak_PO
                usc_DesainPO = New wpfUsc_DesainPO
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainPO)
            Case JenisFormCetak_SuratJalan
                usc_DesainSuratJalan = New wpfUsc_DesainSuratJalan
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainSuratJalan)
            Case JenisFormCetak_BAST
                usc_DesainBAST = New wpfUsc_DesainBAST
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainBAST)
            Case JenisFormCetak_Invoice
                usc_DesainInvoice = New wpfUsc_DesainInvoice
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainInvoice)
            Case JenisFormCetak_NotaRetur
                usc_DesainNotaRetur = New wpfUsc_DesainNotaRetur
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainNotaRetur)
            Case JenisFormCetak_NotaDebet
                usc_DesainNotaDebet = New wpfUsc_DesainNotaDebet
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainNotaDebet)
            Case JenisFormCetak_PengajuanPengeluaran
                usc_DesainBuktiPengeluaran = New wpfUsc_DesainBuktiPengeluaran
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainBuktiPengeluaran)
            Case JenisFormCetak_BundelanPengajuanPengeluaran
                usc_DesainBundelanPengajuanPengeluaran = New wpfUsc_DesainBundelanPengajuanPengeluaran
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainBundelanPengajuanPengeluaran)
            Case JenisFormCetak_JurnalVoucher
                usc_DesainJurnalVoucher = New wpfUsc_DesainJurnalVoucher
                DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainJurnalVoucher)
            Case Kosongan
                PesanUntukProgrammer("Jenis Form belum ditentukan...!!!")
                Me.Close()
                Return
        End Select

        If TampilkanFooter = True Then
            usc_DesainFooter = New wpfUsc_DesainFooter
            DirectCast(grd_Host.Children(0), StackPanel).Children.Add(usc_DesainFooter)
        End If

        If JudulForm = Kosongan Then PesanUntukProgrammer("Judul Form belum ditentukan...!!!")
        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        JenisFormCetak = Kosongan
        JudulForm = Kosongan
        TampilkanHeader = False
        TampilkanFooter = False

    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        CetakPanel(pnl_Kanvas)
    End Sub


    Sub New()
        InitializeComponent()
    End Sub

End Class
