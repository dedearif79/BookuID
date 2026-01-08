Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives


Public Class wpfWin_JurnalVoucher

    Public FungsiForm
    Public NomorJV
    Public Angka_NomorJV
    Public JenisJurnal
    Dim Kurs As Decimal
    Dim TotalDebet As Int64
    Dim TotalKredit As Int64
    Dim StatusBalance = Kosongan
    Dim Referensi = Kosongan
    Dim NomorBP = Kosongan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_JURNALAPPROVE Then
            If SistemApprovalPerusahaan = True Then btn_Setujui.Visibility = Visibility.Visible
            btn_Batal.Content = teks_Batal
        End If

        If FungsiForm = FungsiForm_INFOJURNAL Then
            btn_Setujui.Visibility = Visibility.Collapsed
            btn_Batal.Content = teks_Tutup
        End If

        If SistemApprovalPerusahaan Then
            lbl_NamaUserApprove.Visibility = Visibility.Visible
            txt_NamaUserApprove.Visibility = Visibility.Visible
            btn_Setujui.Visibility = Visibility.Visible
        Else
            lbl_NamaUserApprove.Visibility = Visibility.Collapsed
            txt_NamaUserApprove.Visibility = Visibility.Collapsed
            btn_Setujui.Visibility = Visibility.Collapsed
        End If

        NomorJV = AwalanNomorJV & Angka_NomorJV
        txt_NomorJV.Text = NomorJV

        TampilkanData()

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = FungsiForm_INFOJURNAL '(Ini Defaultnya).

        txt_NomorJV.Text = Kosongan
        txt_JenisJurnal.Text = Kosongan
        txt_TanggalJurnal.Text = Kosongan
        txt_Referensi.Text = Kosongan
        txt_TanggalInvoice.Text = Kosongan
        txt_NomorInvoice.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        txt_LawanTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_NamaProduk)
        KosongkanValueElemenRichTextBox(txt_UraianTransaksi)
        btn_Cetak.Visibility = Visibility.Visible
        btn_Batal.Content = teks_Batal
        lbl_NomorFaktur.Text = "Nomor Faktur Pajak"

        ProsesResetForm = False

    End Sub



    Dim NomorUrut = 0
    Dim COA = Kosongan
    Dim NamaAkun = Kosongan
    Dim DK = Kosongan
    Dim JumlahDebet As Int64 = 0
    Dim JumlahKredit As Int64 = 0
    Dim Keterangan = Kosongan
    Dim NomorID
    Dim TanggalTransaksi = StripKosong
    Dim TanggalInvoice = StripKosong
    Dim NomorInvoice = StripKosong
    Dim NomorFakturPajak = StripKosong
    Dim KodeLawanTransaksi = Kosongan
    Dim NamaLawanTransaksi = StripKosong
    Dim NamaProduk = Kosongan
    Dim NamaUserEntry = StripKosong
    Dim NamaUserApprove = StripKosong
    Sub TampilkanData()

        NomorUrut = 0
        TotalDebet = 0
        TotalKredit = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & Angka_NomorJV & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        datatabelUtama.Rows.Clear()
        Do While dr.Read
            NomorUrut += 1
            COA = dr.Item("COA")
            NamaAkun = AmbilValue_NamaAkun(COA)
            Kurs = dr.Item("Kurs")
            DK = dr.Item("D_K")
            JumlahDebet = AmbilValue_NilaiMataUang_WithCOA(COA, dr.Item("Kode_Mata_Uang"), Kurs, dr.Item("Jumlah_Debet"))
            JumlahKredit = AmbilValue_NilaiMataUang_WithCOA(COA, dr.Item("Kode_Mata_Uang"), Kurs, dr.Item("Jumlah_Kredit"))
            If DK = "K" Then NamaAkun = PenjorokNamaAkun & NamaAkun
            TotalDebet += JumlahDebet
            TotalKredit += JumlahKredit
            Keterangan = dr.Item("Uraian_Transaksi")
            NomorID = dr.Item("Nomor_ID")
            If Not (JumlahDebet = 0 And JumlahKredit = 0) Then
                datatabelUtama.Rows.Add(NomorUrut, COA, NamaAkun, DK, JumlahDebet, JumlahKredit, Keterangan, NomorID)
                JumlahBaris = datatabelUtama.Rows.Count 'Jangan dihapus, dan jangan dipindahkan..!!!
            End If
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            Referensi = dr.Item("Referensi")
            NomorBP = dr.Item("Bundelan")
            JenisJurnal = dr.Item("Jenis_Jurnal")
            TanggalInvoice = TampilanBundelan(dr.Item("Tanggal_Invoice"))
            NomorInvoice = TampilanBundelan(dr.Item("Nomor_Invoice"))
            NamaProduk = dr.Item("Nama_Produk")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            NamaUserEntry = dr.Item("Nama_User_Entry")
            NamaUserApprove = dr.Item("Nama_User_Approve")
        Loop
        AksesDatabase_Transaksi(Tutup)
        datatabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        datatabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!

        NotifBalance()


        txt_TanggalJurnal.Text = TanggalTransaksi
        txt_TanggalInvoice.Text = TanggalInvoice
        txt_NomorInvoice.Text = NomorInvoice
        txt_Referensi.Text = Referensi
        txt_NomorFakturPajak.Text = NomorFakturPajak
        txt_LawanTransaksi.Text = NamaLawanTransaksi
        IsiValueElemenRichTextBox(txt_NamaProduk, NamaProduk)
        txt_NamaUserEntry.Text = NamaUserEntry
        txt_NamaUserApprove.Text = NamaUserApprove
        txt_JenisJurnal.Text = JenisJurnal
        IsiValueElemenRichTextBox(txt_UraianTransaksi, Keterangan)

        Select Case JenisJurnal
            Case JenisJurnal_PembelianImpor
                lbl_NomorFaktur.Text = "Nomor PIB"
            Case JenisJurnal_PenjualanEkspor
                lbl_NomorFaktur.Text = "Nomor PEB"
            Case JenisJurnal_PajakImpor
                lbl_NomorFaktur.Text = "Nomor PIB"
            Case Else
                lbl_NomorFaktur.Text = "Nomor Faktur Pajak"
        End Select

    End Sub

    Sub NotifBalance()

        If TotalDebet = TotalKredit Then
            StatusBalance = "Tidak Ada Selisih"
            lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
            btn_Cetak.IsEnabled = True
            btn_Setujui.IsEnabled = True
        Else
            StatusBalance = "Ada Selisih"
            lbl_StatusBalance.Foreground = WarnaMerahSolid_WPF
            btn_Cetak.IsEnabled = False
            btn_Setujui.IsEnabled = False
        End If

        lbl_StatusBalance.Text = StatusBalance

        datatabelUtama.Rows(JumlahBaris + 1)("Nama_Akun") = "J  U  M  L  A  H"
        datatabelUtama.Rows(JumlahBaris + 1)("Jumlah_Debet") = TotalDebet
        datatabelUtama.Rows(JumlahBaris + 1)("Jumlah_Kredit") = TotalKredit
        datatabelUtama.Rows(JumlahBaris + 1)("Keterangan_") = StatusBalance

        For Each item As DataRowView In datagridUtama.Items
            Dim dataRowView As DataRowView = CType(item, DataRowView)
            Dim Keterangan As String = dataRowView("keterangan_").ToString()
            If Not String.IsNullOrEmpty(Keterangan) Then
                Dim row As DataGridRow = CType(datagridUtama.ItemContainerGenerator.ContainerFromItem(item), DataGridRow)
                If row IsNot Nothing Then
                    Dim cell As DataGridCell = CType(datagridUtama.Columns(6).GetCellContent(row).Parent, DataGridCell)
                    If Keterangan = "Ada Selisih" Then cell.Foreground = WarnaMerahSolid_WPF
                    If Keterangan = "Tidak Ada Selisih" Then cell.Foreground = WarnaHijauSolid_WPF
                End If
            End If
        Next

    End Sub


    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        'NomorUrut_Terseleksi = rowviewUtama("Nomor_Urut")

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
    End Sub


    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_JurnalVoucher, Angka_NomorJV, True, False)
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Private Sub btn_Setujui_Click(sender As Object, e As RoutedEventArgs) Handles btn_Setujui.Click

        'If TotalDebet <> TotalKredit Then
        '    MsgBox("Data tidak dapat disimpan karena ADA SELISIH. Silakan dikoreksi kembali.")
        '    Return
        'End If

        'Dim PilihSetujui = MessageBox.Show("Data yang Anda setujui tidak akan bisa diedit lagi." & Enter2Baris & "Yakin akan menyetujui..?", "Perhatian..!", MessageBoxButtons.YesNo)

        'If PilihSetujui = vbYes Then
        '    TrialBalance_Mentahkan() 'PENTING...!!!
        '    Dim Baris = 0
        '    Dim NomorID
        '    Dim COA
        '    Dim DK = ""
        '    Dim JumlahDebet As Int64 = 0
        '    Dim JumlahKredit As Int64 = 0
        '    Dim QueryEdit, QueryEdit1, QueryEdit2, QueryEdit3
        '    AksesDatabase_Transaksi(Buka)
        '    Do While Baris < JumlahBaris
        '        NomorID = datatabelUtama.Item("Nomor_ID", Baris).Value
        '        COA = datatabelUtama.Item("Kode_Akun", Baris).Value
        '        DK = datatabelUtama.Item("D_K", Baris).Value
        '        JumlahDebet = AmbilAngka(datatabelUtama.Item("Jumlah_Debet", Baris).Value)
        '        JumlahKredit = AmbilAngka(datatabelUtama.Item("Jumlah_Kredit", Baris).Value)
        '        QueryEdit1 = " UPDATE tbl_Transaksi SET "
        '        QueryEdit2 = " COA = '" & COA & "', D_K = '" & DK & "', Jumlah_Debet = '" & JumlahDebet & "', Jumlah_Kredit = '" & JumlahKredit & "', "
        '        QueryEdit3 = " Status_Approve = 1, Username_Approve = '" & UserAktif & "', Nama_User_Approve = '" & NamaUserAktif & "' WHERE Nomor_ID = '" & NomorID & "' "
        '        QueryEdit = QueryEdit1 & QueryEdit2 & QueryEdit3
        '        cmd = New OdbcCommand(QueryEdit, KoneksiDatabaseTransaksi)
        '        cmd.ExecuteNonQuery()
        '        Baris = Baris + 1
        '    Loop
        '    AksesDatabase_Transaksi(Tutup)

        '    'Update Status Jurnal di Tabel Pembelian, menjadi bernilai '1', agar bisa tampil di Buku Pengawasan Hutang Usaha.
        '    AksesDatabase_Transaksi(Buka)
        '    cmd = New OdbcCommand(" UPDATE tbl_Pembelian SET Status_Jurnal = 1 WHERE Nomor_JV = '" & Angka_NomorJV & "' ", KoneksiDatabaseTransaksi)
        '    cmd.ExecuteNonQuery()
        '    AksesDatabase_Transaksi(Tutup)

        '    MsgBox("Jurnal dengan Nomor " & NomorJV & " telah disetujui.")
        '    frm_BukuPembelian.TampilkanData()
        '    frm_BukuPengawasanHutangUsaha.TampilkanData()
        '    frm_JurnalUmum.TampilkanData()
        '    Me.Close()
        'End If

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        txt_NomorJV.IsReadOnly = True
        txt_JenisJurnal.IsReadOnly = True
        txt_TanggalJurnal.IsReadOnly = True
        txt_Referensi.IsReadOnly = True
        txt_TanggalInvoice.IsReadOnly = True
        txt_NomorInvoice.IsReadOnly = True
        txt_NomorFakturPajak.IsReadOnly = True
        txt_LawanTransaksi.IsReadOnly = True
        txt_NamaProduk.IsReadOnly = True
        txt_UraianTransaksi.IsReadOnly = True
        txt_NamaUserEntry.IsReadOnly = True
        txt_NamaUserApprove.IsReadOnly = True
    End Sub



    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Jumlah_Debet As New DataGridTextColumn
    Dim Jumlah_Kredit As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Jumlah_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_ID")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Debet, "Jumlah_Debet", "Jumlah Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kredit, "Jumlah_Kredit", "Jumlah Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatString, KananTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
