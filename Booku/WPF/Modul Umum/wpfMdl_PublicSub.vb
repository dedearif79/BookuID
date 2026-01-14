Imports System.Data.Odbc
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports bcomm
Imports ClosedXML.Excel
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SixLabors.Fonts.Unicode

Module wpfMdl_PublicSub

    Sub BersihkanSeleksi_WPF(datagrid As DataGrid, datatabel As DataTable, ByRef BarisTerseleksi As Int64, ByRef JumlahBaris As Int64)
        JumlahBaris = datatabel.Rows.Count
        BarisTerseleksi = -1
        datagrid.SelectedIndex = -1
        datagrid.SelectedItem = Nothing
        datagrid.SelectedCells.Clear()
    End Sub


    Public Sub KosongkanDatePicker(dtp As DatePicker)
        dtp.SelectedDate = Nothing
    End Sub


    ' Fungsi untuk mendapatkan cell dari DataGrid
    Public Function GetCell(dataGrid As DataGrid, rowIndex As Integer, columnIndex As Integer) As DataGridCell
        Dim row As DataGridRow = DirectCast(dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex), DataGridRow)
        If row IsNot Nothing Then
            Dim presenter As DataGridCellsPresenter = FindVisualChild(Of DataGridCellsPresenter)(row)
            If presenter IsNot Nothing Then
                Return DirectCast(presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex), DataGridCell)
            End If
        End If
        Return Nothing
    End Function

    ' Fungsi untuk mendapatkan indeks kolom berdasarkan nama
    Public Function GetColumnIndexByName(dataGrid As DataGrid, columnName As String) As Integer
        For Each column As DataGridColumn In dataGrid.Columns
            If column.Header.ToString().Equals(columnName) Then
                Return dataGrid.Columns.IndexOf(column)
            End If
        Next
        Return -1
    End Function

    ' Helper function untuk mencari child visual dari objek visual
    Public Function FindVisualChild(Of T As Visual)(parent As Visual) As T
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
            Dim child As Visual = DirectCast(VisualTreeHelper.GetChild(parent, i), Visual)
            If TypeOf child Is T Then
                Return DirectCast(child, T)
            Else
                Dim result As T = FindVisualChild(Of T)(child)
                If result IsNot Nothing Then
                    Return result
                End If
            End If
        Next
        Return Nothing
    End Function

    Sub PewarnaanCellFormatTeks(Kolom As DataGridTextColumn, row As DataGridRow, Warna As SolidColorBrush)
        Dim cellcontent As New TextBlock
        cellcontent = TryCast(Kolom.GetCellContent(row), TextBlock)
        If cellcontent IsNot Nothing Then cellcontent.Foreground = Warna
    End Sub


    Sub KetersediaanHalamanUtama(Tersedia As Boolean)
        win_BOOKU.IsEnabled = Tersedia
    End Sub

    Sub KetersediaanMenuUtama(Tersedia As Boolean)
        ' Menu utama WPF tidak memiliki kontrol IsEnabled global
        ' Fungsi ini sudah tidak diperlukan setelah migrasi full WPF
    End Sub

    Sub KetersediaanMenuHalaman(pnl_Halaman As Panel, Tersedia As Boolean)
        If Tersedia Then
            KetersediaanHalamanUtama(True)
            TutupLoading()
            pnl_Halaman.IsEnabled = True
        Else
            KetersediaanHalamanUtama(False)
            pnl_Halaman.IsEnabled = False
            TampilkanLoading()
        End If
        Forms.Application.DoEvents()
    End Sub

    Sub TampilkanLoading()
        Try
            If win_Loading IsNot Nothing AndAlso win_Loading.StatusAktif Then
                win_Loading.Close()
            End If
            win_Loading = New wpfWin_Loading

            ' Set owner dan posisi tengah secara manual
            Dim helper = New Interop.WindowInteropHelper(win_Loading)
            Dim ownerHelper = New Interop.WindowInteropHelper(win_BOOKU)
            helper.Owner = ownerHelper.Handle
            ' Hitung posisi tengah dari win_BOOKU
            win_Loading.WindowStartupLocation = WindowStartupLocation.Manual
            win_Loading.Left = win_BOOKU.Left + (win_BOOKU.Width - win_Loading.Width) / 2
            win_Loading.Top = win_BOOKU.Top + (win_BOOKU.Height - win_Loading.Height) / 2

            win_Loading.Topmost = False
            win_Loading.Show()
        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanLoading")
        End Try
    End Sub

    Sub TutupLoading()
        Try
            If win_Loading IsNot Nothing AndAlso win_Loading.IsLoaded Then
                win_Loading.Topmost = False
                win_Loading.StatusAktif = False
                win_Loading.Close()
            End If
        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TutupLoading")
        End Try
    End Sub

    Sub KontenComboJenisJurnal_Public_WPF(ByVal JenisJurnal As ComboBox)
        JenisJurnal.Items.Clear()

        AksesDatabase_General(Buka)
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_SaranaPembayaran, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            Dim JenisJurnal_Item = drKhusus.Item("Nama_Akun")
            JenisJurnal.Items.Add(JenisJurnal_Item)
        Loop
        AksesDatabase_General(Tutup)

        JenisJurnal.Items.Add(JenisJurnal_HutangBank)
        JenisJurnal.Items.Add(JenisJurnal_HutangLeasing)
        JenisJurnal.Items.Add(JenisJurnal_HutangPihakKetiga)
        JenisJurnal.Items.Add(JenisJurnal_PiutangPihakKetiga)
        JenisJurnal.Items.Add(JenisJurnal_HutangAfiliasi)
        JenisJurnal.Items.Add(JenisJurnal_PiutangAfiliasi)
        JenisJurnal.Items.Add(JenisJurnal_Dividen)
        JenisJurnal.Items.Add(JenisJurnal_Pembelian)
        JenisJurnal.Items.Add(JenisJurnal_PembelianImpor)
        JenisJurnal.Items.Add(JenisJurnal_Penjualan)
        JenisJurnal.Items.Add(JenisJurnal_PenjualanEkspor)
        JenisJurnal.Items.Add(JenisJurnal_ReturPembelian)
        JenisJurnal.Items.Add(JenisJurnal_ReturPenjualan)
        JenisJurnal.Items.Add(JenisJurnal_Asset)
        JenisJurnal.Items.Add(JenisJurnal_DisposalAsset)
        JenisJurnal.Items.Add(JenisJurnal_BangunanDalamPenyelesaian)
        JenisJurnal.Items.Add(JenisJurnal_Gaji)
        JenisJurnal.Items.Add(JenisJurnal_CekBupot)
        JenisJurnal.Items.Add(JenisJurnal_HutangKaryawan)
        JenisJurnal.Items.Add(JenisJurnal_PiutangKaryawan)
        JenisJurnal.Items.Add(JenisJurnal_HutangPemegangSaham)
        JenisJurnal.Items.Add(JenisJurnal_PiutangPemegangSaham)
        JenisJurnal.Items.Add(JenisJurnal_Amortisasi)
        JenisJurnal.Items.Add(JenisJurnal_Penyusutan)
        JenisJurnal.Items.Add(JenisJurnal_PBk)
        JenisJurnal.Items.Add(JenisJurnal_KetetapanPajak)
        JenisJurnal.Items.Add(JenisJurnal_PajakImpor)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentForex)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentGaji)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentHPP)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentPajak)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentSaldoAwal)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentSelisih)
        JenisJurnal.Items.Add(JenisJurnal_AdjusmentLainnya)
        JenisJurnal.Text = Kosongan
        JenisJurnal.SelectedValue = Kosongan

    End Sub

    Sub KontenCombo_JenisJasa_Public_WPF(cmb_JenisJasa As ComboBox, LokasiWP As String)
        cmb_JenisJasa.Items.Clear()
        cmb_JenisJasa.Items.Add(JenisJasa_JasaLainnya)
        cmb_JenisJasa.Items.Add(JenisJasa_SewaTanahDanAtauBangunan)
        cmb_JenisJasa.Items.Add(JenisJasa_SewaAssetSelainTanahBangunan)
        cmb_JenisJasa.Items.Add(JenisJasa_BungaBagiHasil)
        cmb_JenisJasa.Items.Add(JenisJasa_Royalty)
        cmb_JenisJasa.Items.Add(JenisJasa_Dividen)
        If LokasiWP = LokasiWP_LuarNegeri Then cmb_JenisJasa.Items.Add(JenisJasa_LabaPajakBUT)
        If LokasiWP = LokasiWP_DalamNegeri Then cmb_JenisJasa.Items.Add(JenisJasa_Lainnya)
        cmb_JenisJasa.Text = Kosongan
    End Sub

    Sub KontenCombo_KodeSetoran_Public_WPF(cmb_KodeSetoran As ComboBox, JenisPPh As String)
        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPPh)
        cmb_KodeSetoran.Items.Clear()
        Select Case JenisPPh
            Case JenisPPh_Pasal21
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_401)
            Case JenisPPh_Pasal22_Lokal
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
            Case JenisPPh_Pasal22_Impor
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
            Case JenisPPh_Pasal23
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_101)
                cmb_KodeSetoran.Items.Add(KodeSetoran_102)
                cmb_KodeSetoran.Items.Add(KodeSetoran_103)
                cmb_KodeSetoran.Items.Add(KodeSetoran_104)
            Case JenisPPh_Pasal42
                cmb_KodeSetoran.Items.Add(KodeSetoran_402)
                cmb_KodeSetoran.Items.Add(KodeSetoran_403)
                cmb_KodeSetoran.Items.Add(KodeSetoran_409)
                cmb_KodeSetoran.Items.Add(KodeSetoran_419)
            Case JenisPPh_Pasal26
                cmb_KodeSetoran.Items.Add(KodeSetoran_100)
                cmb_KodeSetoran.Items.Add(KodeSetoran_101)
                cmb_KodeSetoran.Items.Add(KodeSetoran_102)
                cmb_KodeSetoran.Items.Add(KodeSetoran_103)
                cmb_KodeSetoran.Items.Add(KodeSetoran_104)
                cmb_KodeSetoran.Items.Add(KodeSetoran_105)
        End Select
        cmb_KodeSetoran.Text = Kosongan
    End Sub


    Sub PenentuanJenisPPhDanKodeSetoranDanTarifPPh_Pembelian _
        (SupplierSebagaiUMKM As Boolean, LokasiWP As String, JenisWP As String, JenisJasa As String, cmb_JenisPPh As ComboBox, cmb_KodeSetoran As ComboBox, txt_TarifPPh As TextBox)

        Dim JenisPPh = Kosongan
        Dim KodeSetoran = Kosongan
        Dim TarifPPh As Decimal = 2

        If LokasiWP = LokasiWP_DalamNegeri Then
            Select Case JenisJasa
                Case JenisJasa_JasaLainnya
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal21
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2.5
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_104
                        TarifPPh = 2
                    End If
                    If SupplierSebagaiUMKM Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_423
                        TarifPPh = 0.5
                    End If
                Case JenisJasa_JasaKonstruksi
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_409
                        TarifPPh = 4
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_409
                        TarifPPh = 4
                    End If
                Case JenisJasa_SewaAssetSelainTanahBangunan
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_100
                        TarifPPh = 2
                    End If
                Case JenisJasa_SewaTanahDanAtauBangunan
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_403
                        TarifPPh = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_403
                        TarifPPh = 10
                    End If
                Case JenisJasa_BungaBagiHasil
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_102
                        TarifPPh = 15
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_102
                        TarifPPh = 15
                    End If
                Case JenisJasa_Royalty
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_103
                        TarifPPh = 15
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_103
                        TarifPPh = 15
                    End If
                Case JenisJasa_Dividen
                    If JenisWP = JenisWP_OrangPribadi Then
                        JenisPPh = JenisPPh_Pasal42
                        KodeSetoran = KodeSetoran_419
                        TarifPPh = 10
                    End If
                    If JenisWP = JenisWP_BadanHukum Then
                        JenisPPh = JenisPPh_Pasal23
                        KodeSetoran = KodeSetoran_101
                        TarifPPh = 15
                    End If
                Case JenisJasa_Lainnya
                    JenisPPh = JenisPPh_NonPPh
                    KodeSetoran = KodeSetoran_Non
                    TarifPPh = 0
            End Select
        End If

        If LokasiWP = LokasiWP_LuarNegeri Then
            Select Case JenisJasa
                Case JenisJasa_JasaLainnya
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_104
                    TarifPPh = 20
                Case JenisJasa_JasaKonstruksi
                    JenisPPh = JenisPPh_Pasal42
                    KodeSetoran = KodeSetoran_409
                    TarifPPh = 4
                Case JenisJasa_SewaAssetSelainTanahBangunan
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_100
                    TarifPPh = 20
                Case JenisJasa_SewaTanahDanAtauBangunan
                    JenisPPh = JenisPPh_Pasal42
                    KodeSetoran = KodeSetoran_403
                    TarifPPh = 4
                Case JenisJasa_BungaBagiHasil
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_102
                    TarifPPh = 20
                Case JenisJasa_Royalty
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_103
                    TarifPPh = 20
                Case JenisJasa_Dividen
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_101
                    TarifPPh = 20
                Case JenisJasa_LabaPajakBUT
                    JenisPPh = JenisPPh_Pasal26
                    KodeSetoran = KodeSetoran_105
                    TarifPPh = 20
            End Select
        End If
        If JenisJasa = Kosongan Then
            cmb_JenisPPh.Text = Kosongan
            cmb_KodeSetoran.Text = Kosongan
        End If

        IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran)
        IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh)

        If TarifPPh = 0 Then
            txt_TarifPPh.Text = Kosongan
        Else
            txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(TarifPPh)
        End If

    End Sub


    Sub PenentuanJenisPPh_DanTarifPPh_Penjualan(MitraLuarNegeri As Boolean, JenisProduk_Induk As String, cmb_JenisPPh As ComboBox, txt_TarifPPh As TextBox)
        If MitraLuarNegeri Then
            IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal26)
            If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(20)
            txt_TarifPPh.IsEnabled = True
        Else
            If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal42)
                If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(4)
                txt_TarifPPh.IsEnabled = True
            Else
                If JenisWPPerusahaan = JenisWP_OrangPribadi Then
                    IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal21)
                    If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(2.5)
                End If
                If JenisWPPerusahaan = JenisWP_BadanHukum Then
                    IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal23)
                    If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(2)
                End If
                If PerusahaanSebagaiUMKM Then
                    IsiValueComboBypassTerkunci(cmb_JenisPPh, JenisPPh_Pasal42)
                    If ProsesLoadingForm = False And ProsesResetForm = False And ProsesIsiValueForm = False Then txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(0.5)
                End If
            End If
        End If
    End Sub



    'PEMECAH RIBUAN UNTUK STRING  :
    Public Function PemecahRibuanUntukString(Angka As Integer) As String
        Return Angka.ToString("N0", New CultureInfo("id-ID"))
    End Function


    'PEMECAH RIBUAN UNTUK TEXTBOX :
    Public Sub PemecahRibuanUntukTextBox_WPF(teks As TextBox)

        teks.MaxLength = 21
        teks.HorizontalContentAlignment = HorizontalAlignment.Right

        If teks IsNot Nothing Then
            ' Ambil teks dari TextBox dan hilangkan spasi berlebih
            Dim teksBaru As String = teks.Text.Trim()

            ' Cek apakah teks memiliki minus di awal
            Dim isNegative As Boolean = teksBaru.StartsWith("-")

            ' Hapus semua minus yang tidak ada di awal
            teksBaru = teksBaru.TrimStart("-") ' Hilangkan semua minus di awal dulu
            teksBaru = teksBaru.Replace("-", "") ' Hilangkan minus yang mungkin ada di tengah

            ' Tambahkan kembali satu minus jika awalnya memang negatif
            If isNegative Then teksBaru = "-" & teksBaru

            ' Hapus semua karakter selain angka dan satu minus di awal
            Dim teksBersih As String = New String(teksBaru.Where(Function(c) Char.IsDigit(c) OrElse (c = "-" AndAlso teksBaru.IndexOf("-") = 0)).ToArray())

            ' Jika ada angka, format dengan pemisah ribuan
            If teksBersih.Length > 0 AndAlso teksBersih <> "-" Then
                Dim angka As Double
                If Double.TryParse(teksBersih, angka) Then
                    teksBersih = angka.ToString("N0") ' Format angka dengan pemisah ribuan
                    If isNegative Then teksBersih = "-" & teksBersih ' Pastikan minus tetap ada di awal
                End If
            End If

            If teksBersih = 0.ToString Then teks.Text = Kosongan 'Harus pakai ToString...!

            ' Cek apakah teks berubah
            If teksBaru <> teksBersih Then
                ' Simpan posisi kursor sebelum perubahan
                Dim caretPosition As Integer = teks.SelectionStart

                ' Perbarui teks di TextBox
                teks.Text = teksBersih.Replace("--", "-")

                ' Kembalikan posisi kursor ke akhir teks
                teks.SelectionStart = teks.Text.Length
            End If

        End If

    End Sub

    'PEMECAH RIBUAN UNTUK TEXTBOX :
    Public Sub TextBoxFormatPersen_WPF(teks As TextBox, ByRef ValueDesimal As Decimal)
        teks.MaxLength = 5
        Dim FormatValid As Boolean = True
        Dim TeksString As String = teks.Text
        Dim jumlahKoma As Integer = TeksString.Count(Function(c) c = ","c)
        If jumlahKoma > 1 Then FormatValid = False
        'If TeksString = "0" Then teks.Text = Kosongan
        If TeksString = "00" Then teks.Text = "0"
        If TeksString = "," Then teks.Text = Kosongan
        If teks.Text = Kosongan Then
            ValueDesimal = 0
        Else
            If FormatValid Then ValueDesimal = TeksString
            If ValueDesimal > 100 Then FormatValid = False
        End If
        If FormatValid = False Then
            teks.Text = Kosongan
            PesanPeringatan("Silakan isi kolom dengan benar.")
            teks.Focus()
        End If
        teks.CaretIndex = teks.Text.Length ' Menempatkan kursor kembali ke posisi terakhir
        teks.HorizontalContentAlignment = HorizontalAlignment.Right
    End Sub

    'KUNCI INPUTAN HANYA BOLEH ANGKA :
    Public Sub HanyaBolehInputAngka_WPF(teks As TextBox, e As TextCompositionEventArgs)
        ' Izinkan input angka atau tanda minus (-) hanya di awal teks
        If Not (Char.IsDigit(e.Text, 0) OrElse (e.Text = "-" AndAlso teks.SelectionStart = 0 AndAlso Not teks.Text.Contains("-"))) Then
            e.Handled = True
        End If
    End Sub

    'KUNCI INPUTAN HANYA BOLEH ANGKA PLUS :
    Public Sub HanyaBolehInputAngkaPlus_WPF(teks As TextBox, e As TextCompositionEventArgs)
        If Not Char.IsDigit(e.Text, e.Text.Length - 1) Then
            e.Handled = True
        End If
    End Sub


    'KUNCI INPUTAN HANYA BOLEH ANGKA DESIMAL:
    Public Sub HanyaBolehInputAngkaDesimalPlus_WPF(teks As TextBox, e As TextCompositionEventArgs)
        If Not (Char.IsDigit(e.Text, 0) Or e.Text = ",") Then
            e.Handled = True ' Mencegah karakter tidak valid masuk
        End If
    End Sub

    Sub KontenComboKas_WPF(ByVal cmb_Kas As ComboBox)
        cmb_Kas.Items.Clear()
        Dim KodeAkun_Kas
        Dim NamaAkun_Kas
        Dim Item_Kas
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_Kas, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_Kas = drKhusus.Item("COA")
            NamaAkun_Kas = drKhusus.Item("Nama_Akun")
            Item_Kas = KodeAkun_Kas & StripPemisah & NamaAkun_Kas
            cmb_Kas.Items.Add(Item_Kas)
        Loop
        TutupDatabaseGeneral_Kondisional()
        cmb_Kas.Text = Kosongan
        cmb_Kas.SelectedValue = Kosongan
    End Sub

    Sub KontenComboPettyCash_WPF(ByVal cmb_PettyCash As ComboBox)
        cmb_PettyCash.Items.Clear()
        Dim KodeAkun_PettyCash
        Dim NamaAkun_PettyCash
        Dim Item_PettyCash
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_PettyCash, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_PettyCash = drKhusus.Item("COA")
            NamaAkun_PettyCash = drKhusus.Item("Nama_Akun")
            Item_PettyCash = KodeAkun_PettyCash & StripPemisah & NamaAkun_PettyCash
            cmb_PettyCash.Items.Add(Item_PettyCash)
        Loop
        TutupDatabaseGeneral_Kondisional()
        cmb_PettyCash.Text = Kosongan
        cmb_PettyCash.SelectedValue = Kosongan
    End Sub

    Sub KontenComboCashAdvance_WPF(ByVal cmb_CashAdvance As ComboBox)
        cmb_CashAdvance.Items.Clear()
        Dim KodeAkun_CashAdvance
        Dim NamaAkun_CashAdvance
        Dim Item_CashAdvance
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_CashAdvance, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_CashAdvance = drKhusus.Item("COA")
            NamaAkun_CashAdvance = drKhusus.Item("Nama_Akun")
            Item_CashAdvance = KodeAkun_CashAdvance & StripPemisah & NamaAkun_CashAdvance
            cmb_CashAdvance.Items.Add(Item_CashAdvance)
        Loop
        TutupDatabaseGeneral_Kondisional()
        cmb_CashAdvance.Text = Kosongan
        cmb_CashAdvance.SelectedValue = Kosongan
    End Sub

    Sub KontenComboBank_WPF(ByVal cmb_Bank As ComboBox)
        cmb_Bank.Items.Clear()
        Dim KodeAkun_Bank
        Dim NamaAkun_Bank
        Dim Item_Bank
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_Bank, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_Bank = drKhusus.Item("COA")
            NamaAkun_Bank = drKhusus.Item("Nama_Akun")
            Item_Bank = KodeAkun_Bank & StripPemisah & NamaAkun_Bank
            cmb_Bank.Items.Add(Item_Bank)
        Loop
        TutupDatabaseGeneral_Kondisional()
        cmb_Bank.Text = Kosongan
        cmb_Bank.SelectedValue = Kosongan
    End Sub

    Sub KontenComboSaranaPembayaran_Public_WPF(ByVal cmb_SaranaPembayaran As ComboBox, KodeMataUang As String)
        If KodeMataUang = Kosongan Then KodeMataUang = KodeMataUang_IDR
        Dim FilterKodeMataUang As String
        If KodeMataUang = Pilihan_Semua Then
            FilterKodeMataUang = Kosongan
        Else
            FilterKodeMataUang = " AND Kode_Mata_Uang = '" & KodeMataUang & " ' "
        End If
        cmb_SaranaPembayaran.Items.Clear()
        Dim KodeAkun_SaranaPembayaran
        Dim NamaAkun_SaranaPembayaran
        Dim Item_SaranaPembayaran
        BukaDatabaseGeneral_Kondisional()
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA " &
                                        " WHERE Visibilitas = '" & Pilihan_Ya & "' " &
                                        FilterKodeMataUang &
                                        FilterListCOA_SaranaPembayaran,
                                        KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_SaranaPembayaran = drKhusus.Item("COA")
            NamaAkun_SaranaPembayaran = drKhusus.Item("Nama_Akun")
            Item_SaranaPembayaran = KodeAkun_SaranaPembayaran & StripPemisah & NamaAkun_SaranaPembayaran
            cmb_SaranaPembayaran.Items.Add(Item_SaranaPembayaran)
        Loop
        TutupDatabaseGeneral_Kondisional()
        cmb_SaranaPembayaran.Text = Kosongan
        cmb_SaranaPembayaran.SelectedValue = Kosongan
    End Sub

    Sub KontenComboDitanggungOleh_Public_WPF(ByVal DitanggungOleh As ComboBox)
        DitanggungOleh.Items.Clear()
        DitanggungOleh.Items.Add(DitanggungOleh_Perusahaan)
        DitanggungOleh.Items.Add(DitanggungOleh_LawanTransaksi)
    End Sub

    Sub KontenComboPembebanan_Public_WPF(ByVal Pembebanan As ComboBox)
        Pembebanan.Items.Clear()
        Pembebanan.Items.Add(Pembebanan_Diganti)
        Pembebanan.Items.Add(Pembebanan_Dipotong)
        Pembebanan.Items.Add(Pembebanan_Ditambahkan)
    End Sub

    Sub KontenComboDaftarBank_Public_WPF(ByVal DaftarBank As ComboBox)
        DaftarBank.Items.Clear()
        Dim KodeAkun_Bank
        Dim NamaAkun_Bank
        Dim Item_Bank
        AksesDatabase_General(Buka)
        Dim cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_Bank, KoneksiDatabaseGeneral)
        Dim drKhusus = cmdKhusus.ExecuteReader
        Do While drKhusus.Read
            KodeAkun_Bank = drKhusus.Item("COA")
            NamaAkun_Bank = drKhusus.Item("Nama_Akun")
            Item_Bank = KodeAkun_Bank & StripPemisah & NamaAkun_Bank
            DaftarBank.Items.Add(Item_Bank)
        Loop
        AksesDatabase_General(Tutup)
        DaftarBank.Text = Kosongan
        DaftarBank.SelectedValue = Kosongan
    End Sub

    'KUNCI TAHUN - HARUS SAMA DENGAN TAHUN BUKU AKTIF :
    Public Sub KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(ByVal Tanggal As DatePicker)
        If Tanggal.SelectedDate.Value.Year <> TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.SelectedDate.Value.Day) = 29 And AmbilAngka(Tanggal.SelectedDate.Value.Month) = 2 Then
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, 28)
            Else
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, Tanggal.SelectedDate.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, hanya untuk Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub


    'KUNCI TAHUN - TIDAK BOLEH LEBIH DARI TAHUN BUKU AKTIF :
    Public Sub KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(ByVal Tanggal As DatePicker)
        If Tanggal.SelectedDate.Value.Year > TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.SelectedDate.Value.Day) = 29 And AmbilAngka(Tanggal.SelectedDate.Value.Month) = 2 Then
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, 28)
            Else
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, Tanggal.SelectedDate.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh lebih dari Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub


    'KUNCI TAHUN - TIDAK BOLEH KURANG DARI TAHUN BUKU AKTIF :
    Public Sub KunciTahun_TidakBolehKurangDariTahunBukuAktif_WPF(ByVal Tanggal As DatePicker)
        If Tanggal.SelectedDate.Value.Year < TahunBukuAktif Then
            If TahunKabisat_TahunBukuAktif = False And AmbilAngka(Tanggal.SelectedDate.Value.Day) = 29 And AmbilAngka(Tanggal.SelectedDate.Value.Month) = 2 Then
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, 28)
            Else
                Tanggal.SelectedDate = New Date(TahunBukuAktif, Tanggal.SelectedDate.Value.Month, Tanggal.SelectedDate.Value.Day)
            End If
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                If EksekusiKode = True Then
                    PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh kurang dari Tahun " & TahunBukuAktif & ".")
                End If
                Tanggal.Focus()
            End If
        End If
    End Sub

    Public Sub KunciTanggalBulanDanTahun_TidakBolehLebihDari_WPF(ByVal Tanggal As DatePicker, ByVal HariPengunci As Integer, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim ValiditasInputan As Boolean = True
        Dim Tahun = Tanggal.SelectedDate.Value.Year
        Dim Bulan = Tanggal.SelectedDate.Value.Month
        Dim Hari = Tanggal.SelectedDate.Value.Day
        If Tahun > TahunPengunci Then
            Tahun = TahunPengunci
            ValiditasInputan = False
        End If
        If Bulan > BulanPengunci And Tahun = TahunPengunci Then
            Bulan = BulanPengunci
            ValiditasInputan = False
        End If
        If Hari > HariPengunci And Bulan = BulanPengunci And Tahun = TahunPengunci Then
            Hari = HariPengunci
            ValiditasInputan = False
        End If
        Tanggal.SelectedDate = New Date(Tahun, Bulan, Hari)
        If ValiditasInputan = False Then
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh melebihi Tanggal " & Hari & " " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub



    'KUNCI BULAN DAN TAHUN : HARUS SAMA
    Public Sub KunciBulanDanTahun_HarusSama_WPF(ByVal Tanggal As DatePicker, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim Tahun = Tanggal.SelectedDate.Value.Year
        Dim Bulan = Tanggal.SelectedDate.Value.Month
        Dim Hari As Integer = AmbilAngka(Tanggal.SelectedDate.Value.Day)
        If Bulan = 0 Or Hari = 0 Then Return
        If Bulan <> BulanPengunci Or Tahun <> TahunPengunci Then
            Bulan = BulanPengunci
            Tahun = TahunPengunci
            Dim PembatasHari As Integer = AmbilAngka(Left(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan, Tahun), 2))
            If Hari > PembatasHari Then Hari = PembatasHari
            Tanggal.SelectedDate = New Date(Tahun, Bulan, Hari)
            If ProsesResetForm = False And ProsesLoadingForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, hanya untuk Bulan " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                Tanggal.Focus()
            End If
        End If
    End Sub

    Public Sub KunciTanggalBulanDanTahun_TidakBolehKurangDari_WPF(ByVal dtp_Tanggal As DatePicker, ByVal HariPengunci As Integer, ByVal BulanPengunci As Integer, ByVal TahunPengunci As Integer)
        Dim ValiditasInputan As Boolean = True
        Dim Tahun = dtp_Tanggal.SelectedDate.Value.Year
        Dim Bulan = dtp_Tanggal.SelectedDate.Value.Month
        Dim Hari = dtp_Tanggal.SelectedDate.Value.Day
        If Tahun < TahunPengunci Then
            Tahun = TahunPengunci
            ValiditasInputan = False
        End If
        If Bulan < BulanPengunci And Tahun = TahunPengunci Then
            Bulan = BulanPengunci
            ValiditasInputan = False
        End If
        If Hari < HariPengunci And Bulan = BulanPengunci And Tahun = TahunPengunci Then
            Hari = HariPengunci
            ValiditasInputan = False
        End If
        dtp_Tanggal.SelectedDate = New Date(Tahun, Bulan, Hari)
        If ValiditasInputan = False Then
            If ProsesResetForm = False And ProsesLoadingForm = False And ProsesIsiValueForm = False Then
                PesanUntukProgrammer("Inputan tanggal dikunci, tidak boleh kurang dari Tanggal " & Hari & " " & KonversiAngkaKeBulanString(Bulan) & " " & TahunPengunci & ".")
                dtp_Tanggal.Focus()
            End If
        End If
    End Sub

    Sub LogikaUmumInputanTanggal(dtp_Tanggal As DatePicker)
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_Tanggal)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_Tanggal)
    End Sub

    Function IsiValueVariabelRichTextBox(txt_Keterangan As RichTextBox)
        Dim textRange As New TextRange(txt_Keterangan.Document.ContentStart, txt_Keterangan.Document.ContentEnd)
        Dim Konten As String = textRange.Text
        Replace(Konten, Convert.ToChar(10), Kosongan)
        Return Konten
    End Function

    Sub IsiValueElemenRichTextBox(txt_Keterangan As RichTextBox, Konten As String)
        Dim txtRange As New TextRange(txt_Keterangan.Document.ContentStart, txt_Keterangan.Document.ContentEnd)
        txtRange.Text = If(Konten, String.Empty)
    End Sub

    Sub KosongkanValueElemenRichTextBox(ByRef txt_Keterangan As RichTextBox)
        txt_Keterangan.Document = New FlowDocument
    End Sub

    Public KananAtas = "Kanan Atas"
    Public KananTengah = "Kanan Tengah"
    Public KananBawah = "Kanan Bawah"
    Public TengahAtas = "Tengah Atas"
    Public TengahTengah = "Tengah Tengah"
    Public TengahBawah = "Tengah Bawah"
    Public KiriAtas = "Kiri Atas"
    Public KiriTengah = "Kiri Tengah"
    Public KiriBawah = "Kiri Bawah"
    Public Urut = "Urut"
    Public KunciUrut = "Kunci Urut"
    Public FormatAngka = "Format Angka"
    Public FormatDesimal = "Format Desimal"
    Public FormatString = "Format String"
    Public Terlihat = "Terlihat"
    Public TerlihatKhususProgrammer = "Terlihat Khusus Programmer"
    Public Tersembunyi = "Tersembunyi"
    Sub TambahkanKolomDataGridView(Tabel As DataGridView, NamaKolom As String, JudulKolom As String, Lebar As Integer, FormatTeks As String, Rata As String, ModeUrut As String, Visibilitas As String)
        Tabel.Columns.Add(NamaKolom, JudulKolom)
        Tabel.Columns(NamaKolom).Width = Lebar
        If FormatTeks = FormatAngka Then Tabel.Columns(NamaKolom).DefaultCellStyle.Format = "N0"
        Select Case Rata
            Case KananAtas
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
            Case KananTengah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Case KananBawah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            Case TengahAtas
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            Case TengahTengah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Case TengahBawah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            Case KiriAtas
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            Case KiriTengah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Case KiriBawah
                Tabel.Columns(NamaKolom).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End Select
        If ModeUrut = KunciUrut Then Tabel.Columns(NamaKolom).SortMode = DataGridViewColumnSortMode.NotSortable
        If Visibilitas = Terlihat Then
            Tabel.Columns(NamaKolom).Visible = True
        Else
            Tabel.Columns(NamaKolom).Visible = False
        End If
        If Visibilitas = TerlihatKhususProgrammer Then
            If LevelUserAktif >= LevelUser_99_AppDeveloper Then
                Tabel.Columns(NamaKolom).Visible = True
            Else
                Tabel.Columns(NamaKolom).Visible = False
            End If
        End If
    End Sub

    Sub TambahkanKolomTextBoxDataGrid_WPF(Tabel As DataGrid, Kolom As DataGridTextColumn, BindingKolom As String, JudulKolom As String,
                                          Lebar As Integer, FormatTeks As String, Rata As String, ModeUrut As String, Visibilitas As String)

        Dim PaddingHorizontal = 3
        Dim PaddingVertikal = 2

        Kolom.Header = JudulKolom
        Kolom.Width = Lebar
        Select Case FormatTeks
            Case FormatString
                Kolom.Binding = New Binding(BindingKolom)
            Case FormatAngka
                Kolom.Binding = New Binding(BindingKolom) With {.Converter = New cls_NumberConverter}
            Case FormatDesimal
                Kolom.Binding = New Binding(BindingKolom) With {.Converter = New cls_DecimalConverter}
        End Select
        Kolom.ElementStyle = New Style(GetType(TextBlock))
        Select Case Rata
            Case KananAtas
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case KananTengah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case KananBawah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case TengahAtas
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case TengahTengah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case TengahBawah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case KiriAtas
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left))
            Case KiriTengah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left))
            Case KiriBawah
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left))
        End Select
        If Visibilitas = Terlihat Then Kolom.Visibility = Visibility.Visible
        If Visibilitas = Tersembunyi Then Kolom.Visibility = Visibility.Collapsed
        If Visibilitas = TerlihatKhususProgrammer Then
            If LevelUserAktif >= LevelUser_99_AppDeveloper Then
                Kolom.Visibility = Visibility.Visible
            Else
                Kolom.Visibility = Visibility.Collapsed
            End If
        End If
        Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.PaddingProperty, New Thickness(PaddingHorizontal, PaddingVertikal, PaddingHorizontal, PaddingVertikal)))
        Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.PaddingProperty, New Thickness(PaddingHorizontal, PaddingVertikal, PaddingHorizontal, PaddingVertikal)))
        Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.TextWrappingProperty, TextWrapping.NoWrap))
        Tabel.Columns.Add(Kolom)

        Dim headerTextBlock As New TextBlock()
        headerTextBlock.Text = Kolom.Header
        headerTextBlock.TextWrapping = TextWrapping.Wrap
        headerTextBlock.HorizontalAlignment = HorizontalAlignment.Center
        headerTextBlock.TextAlignment = TextAlignment.Center
        headerTextBlock.Padding = New Thickness(0)
        Tabel.Columns(Tabel.Columns.Count - 1).Header = headerTextBlock

    End Sub

    Sub TambahkanKolomTextBoxDataGrid_Nota_WPF(Tabel As DataGrid, Kolom As DataGridTextColumn, BindingKolom As String, JudulKolom As String, Lebar As Integer, FormatTeks As String, Rata As String, ModeUrut As String, Visibilitas As String)
        TambahkanKolomTextBoxDataGrid_WPF(Tabel, Kolom, BindingKolom, JudulKolom, Lebar, FormatTeks, Rata, ModeUrut, Visibilitas)
        Kolom.ElementStyle.Setters.Add(New Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap))
    End Sub


    Sub TambahkanKolomCheckBoxDataGrid_WPF(Tabel As DataGrid, Kolom As DataGridCheckBoxColumn, BindingKolom As String, JudulKolom As String, Lebar As Integer, FormatTeks As String, Rata As String, ModeUrut As String, Visibilitas As String, BisaDiceklis As Boolean)

        Dim PaddingHorizontal = 3
        Dim PaddingVertikal = 2

        Kolom.Header = JudulKolom
        Kolom.Width = Lebar
        If FormatTeks = FormatAngka Then
            Kolom.Binding = New Binding(BindingKolom) With {.StringFormat = "N0"}
        Else
            Kolom.Binding = New Binding(BindingKolom)
        End If
        Kolom.ElementStyle = New Style(GetType(CheckBox))
        Select Case Rata
            Case KananAtas
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case KananTengah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case KananBawah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Right))
            Case TengahAtas
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case TengahTengah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case TengahBawah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center))
            Case KiriAtas
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Top))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Left))
            Case KiriTengah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Left))
            Case KiriBawah
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Bottom))
                Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Left))
        End Select
        If Visibilitas = Terlihat Then Kolom.Visibility = Visibility.Visible
        If Visibilitas = Tersembunyi Then Kolom.Visibility = Visibility.Collapsed
        Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.PaddingProperty, New Thickness(PaddingHorizontal, PaddingVertikal, PaddingHorizontal, PaddingVertikal)))
        Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.PaddingProperty, New Thickness(PaddingHorizontal, PaddingVertikal, PaddingHorizontal, PaddingVertikal)))
        If BisaDiceklis Then
            Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.IsEnabledProperty, True))
        Else
            Kolom.ElementStyle.Setters.Add(New Setter(CheckBox.IsEnabledProperty, False))
        End If
        Tabel.Columns.Add(Kolom)

        Dim headerTextBlock As New TextBlock()
        headerTextBlock.Text = Kolom.Header
        headerTextBlock.TextWrapping = TextWrapping.Wrap
        headerTextBlock.TextAlignment = TextAlignment.Center
        headerTextBlock.Padding = New Thickness(0)
        Tabel.Columns(Tabel.Columns.Count - 1).Header = headerTextBlock

    End Sub

    Public Sub TambahkanKolomButtonDataGrid_WPF(Tabel As DataGrid, BindingKolom As String, JudulKolom As String, Lebar As Integer, Bulan As Integer, Handler As RoutedEventHandler)

        Dim kolom As New DataGridTemplateColumn()

        kolom.Header = JudulKolom
        kolom.Width = Lebar

        ' ----- isi cell: Button -----
        Dim btnFactory As New FrameworkElementFactory(GetType(Button))

        ' tampilan tombol (silakan sesuaikan)
        btnFactory.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch)
        btnFactory.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch)
        btnFactory.SetValue(Button.MarginProperty, New Thickness(1))
        btnFactory.SetValue(Button.PaddingProperty, New Thickness(2, 0, 2, 0))

        ' teks di tombol (bisa "Post", atau nama bulan, terserah)
        btnFactory.SetValue(Button.ContentProperty, "Post")

        ' simpan informasi bulan di Tag
        btnFactory.SetValue(Button.TagProperty, Bulan)

        'binding untuk disable jika sudah posting
        Dim enabledBinding As New Binding(BindingKolom) With {
            .Converter = New cls_EnableButtonConverter()
        }

        ' kalau mau isi tombol di-bind ke value cell:
        Dim b As New Binding(BindingKolom)
        btnFactory.SetBinding(Button.ContentProperty, b)

        ' event klik
        btnFactory.AddHandler(Button.ClickEvent, Handler)
        btnFactory.SetBinding(Button.IsEnabledProperty, enabledBinding)

        ' pasang ke DataTemplate
        Dim template As New DataTemplate()
        template.VisualTree = btnFactory
        kolom.CellTemplate = template

        ' tambahkan ke DataGrid
        Tabel.Columns.Add(kolom)
    End Sub


    'STYLE TABEL DASAR :
    ' Warna menggunakan variabel dari mdlPub_ModulUmum.vb yang selaras dengan StyleColor.xaml
    Public Sub StyleTabelDasar_WPF(ByVal Tabel As DataGrid)
        If Proses = False Then
            Tabel.AutoGenerateColumns = False
            Tabel.HorizontalAlignment = HorizontalAlignment.Left
            Tabel.VerticalAlignment = VerticalAlignment.Top
            Tabel.HeadersVisibility = DataGridHeadersVisibility.Column
            Tabel.CanUserDeleteRows = False
            Tabel.RowBackground = WarnaPutih_WPF                          ' clrDataGridBg #FFFFFF
            Tabel.CanUserSortColumns = False
            Tabel.CanUserReorderColumns = False
            Tabel.CanUserDeleteRows = False
            Tabel.CanUserAddRows = False
            Tabel.CanUserResizeRows = False
            Tabel.SelectionMode = DataGridSelectionMode.Single
            Tabel.SelectionUnit = DataGridSelectionUnit.FullRow
            Tabel.BorderThickness = New Thickness(0.5)
            Tabel.VerticalGridLinesBrush = WarnaHitam_15_WPF              ' clrDataGridGridLine #EEEEEE
            Tabel.HorizontalGridLinesBrush = WarnaHitam_15_WPF            ' clrDataGridGridLine #EEEEEE
            Tabel.GridLinesVisibility = DataGridGridLinesVisibility.All
            Tabel.IsReadOnly = True

            'Style Header Kolom :
            Dim styleHeader As New Style(GetType(DataGridColumnHeader))
            styleHeader.Setters.Add(New Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty, HorizontalAlignment.Center))
            Tabel.ColumnHeaderStyle = styleHeader

            'Style Baris :
            Dim rowStyle As New Style(GetType(DataGridRow))
            rowStyle.Setters.Add(New Setter(DataGridRow.ForegroundProperty, WarnaTeksStandar_WPF))
            Dim selectedRowTrigger As New Trigger() With {
                .Property = DataGridRow.IsSelectedProperty,
                .Value = True
            }
            selectedRowTrigger.Setters.Add(New Setter(DataGridRow.BackgroundProperty, WarnaHijauSolid_WPF))  ' clrPrimary #388E3C
            selectedRowTrigger.Setters.Add(New Setter(DataGridRow.ForegroundProperty, WarnaPutih_WPF))       ' Teks putih agar terbaca
            rowStyle.Triggers.Add(selectedRowTrigger)

            'Style Cell (agar warna seleksi terlihat):
            Dim cellStyle As New Style(GetType(DataGridCell))
            cellStyle.Setters.Add(New Setter(DataGridCell.BackgroundProperty, Brushes.Transparent))
            cellStyle.Setters.Add(New Setter(DataGridCell.ForegroundProperty, WarnaTeksStandar_WPF))
            cellStyle.Setters.Add(New Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent))
            cellStyle.Setters.Add(New Setter(DataGridCell.FocusVisualStyleProperty, Nothing))
            Dim selectedCellTrigger As New Trigger() With {
                .Property = DataGridCell.IsSelectedProperty,
                .Value = True
            }
            selectedCellTrigger.Setters.Add(New Setter(DataGridCell.BackgroundProperty, WarnaHijauSolid_WPF))   ' Hijau solid #388E3C
            selectedCellTrigger.Setters.Add(New Setter(DataGridCell.ForegroundProperty, WarnaPutih_WPF))        ' Teks putih
            selectedCellTrigger.Setters.Add(New Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent))
            cellStyle.Triggers.Add(selectedCellTrigger)

            ' Terapkan Style ke DataGrid
            Tabel.RowStyle = rowStyle
            Tabel.CellStyle = cellStyle

        End If
    End Sub

    'STYLE TABEL UTAMA :
    Public Sub StyleTabelUtama_WPF(ByRef datagridUtama As DataGrid, ByRef datatabelUtama As DataTable, ByRef dataviewUtama As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridUtama)
            datagridUtama.AlternatingRowBackground = WarnaHitam_5_WPF     ' clrDataGridBgAlt #FAFAFA
            dataviewUtama = New DataView(datatabelUtama)
            dataviewUtama.RowFilter = Kosongan
            datagridUtama.ItemsSource = dataviewUtama
        End If
    End Sub

    Public Sub StyleTabelUtama_Laporan_WPF(ByRef datagridUtama As DataGrid, ByRef datatabelUtama As DataTable, ByRef dataviewUtama As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridUtama)
            dataviewUtama = New DataView(datatabelUtama)
            dataviewUtama.RowFilter = Kosongan
            datagridUtama.ItemsSource = dataviewUtama
        End If
    End Sub

    'STYLE TABEL BISA DI-CREATE :
    Public Sub StyleTabelBisaDicreate_WPF(ByRef datagridBisaDiCreate As DataGrid, ByRef datatabelBisaDiCreate As DataTable, ByRef dataviewBisaDiCreate As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridBisaDiCreate)
            datagridBisaDiCreate.AlternatingRowBackground = WarnaHitam_5_WPF   ' clrDataGridBgAlt #FAFAFA
            dataviewBisaDiCreate = New DataView(datatabelBisaDiCreate)
            dataviewBisaDiCreate.RowFilter = Kosongan
            datagridBisaDiCreate.ItemsSource = dataviewBisaDiCreate
        End If
    End Sub

    'STYLE TABEL PEMBANTU :
    Public Sub StyleTabelPembantu_WPF(ByRef datagridPembantu As DataGrid, ByRef datatabelPembantu As DataTable, ByRef dataviewPembantu As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridPembantu)
            datagridPembantu.AlternatingRowBackground = WarnaHitam_5_WPF      ' clrDataGridBgAlt #FAFAFA
            dataviewPembantu = New DataView(datatabelPembantu)
            dataviewPembantu.RowFilter = Kosongan
            datagridPembantu.ItemsSource = dataviewPembantu
        End If
    End Sub

    'STYLE TABEL Total :
    Public Sub StyleTabelTotal_WPF(ByRef datagridTotal As DataGrid, ByRef datatabelTotal As DataTable, ByRef dataviewTotal As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridTotal)
            dataviewTotal = New DataView(datatabelTotal)
            dataviewTotal.RowFilter = Kosongan
            datagridTotal.ItemsSource = dataviewTotal
            datagridTotal.HeadersVisibility = DataGridHeadersVisibility.None
            datagridTotal.CanUserResizeColumns = False
            datagridTotal.RowBackground = WarnaHitam_5_WPF                    ' clrDataGridBgAlt #FAFAFA
        End If
    End Sub

    'STYLE TABEL NOTA :
    Public Sub StyleTabelNota_WPF(ByRef datagridNota As DataGrid, ByRef datatabelNota As DataTable, ByRef dataviewNota As DataView)
        If Proses = False Then
            StyleTabelDasar_WPF(datagridNota)
            dataviewNota = New DataView(datatabelNota)
            dataviewNota.RowFilter = Kosongan
            datagridNota.ItemsSource = dataviewNota
            datagridNota.HorizontalScrollBarVisibility = False
            datagridNota.VerticalScrollBarVisibility = False
            datagridNota.CanUserResizeColumns = False
        End If
    End Sub

    Sub ApplicationDoEvents()
        'Terabas()
        'Application.Current.Terabas()
    End Sub

    Sub CetakPanel(Kanvas As Panel)

        Dim Dpi = 600
        Dim StandarDpi = 96 '(Value ini jangan dirubah...!!!!)
        Dim RasioDpi As Double = Dpi / StandarDpi

        ' Buat PrintDialog untuk memilih printer
        Dim printDlg As New PrintDialog()

        ' Jika user memilih printer dan menekan OK
        If printDlg.ShowDialog() = True Then
            ' Buat ukuran panel berdasarkan actual size-nya
            Dim panelSize As Size = New Size(Kanvas.ActualWidth, Kanvas.ActualHeight)
            Kanvas.Measure(panelSize)
            Kanvas.Arrange(New Rect(panelSize))

            ' Render konten panel ke dalam bitmap
            Dim rtb As New RenderTargetBitmap(CInt(Kanvas.ActualWidth * RasioDpi), CInt(Kanvas.ActualHeight * RasioDpi), Dpi, Dpi, PixelFormats.Pbgra32)
            rtb.Render(Kanvas)

            ' Buat DrawingVisual untuk mencetak
            Dim dv As New DrawingVisual()

            Using dc As DrawingContext = dv.RenderOpen()
                ' Gambar visual panel ke context cetak
                dc.DrawImage(rtb, New Rect(New Point(0, 0), New Size((Kanvas.ActualWidth), (Kanvas.ActualHeight))))
            End Using

            ' Cetak visual panel
            printDlg.PrintVisual(dv, "Panel Print")
        End If

    End Sub


    Sub Pratinjau(Kanvas As Panel)

        ' Buat ukuran panel berdasarkan actual size-nya
        Dim panelSize As Size = New Size(Kanvas.ActualWidth, Kanvas.ActualHeight)
        Kanvas.Measure(panelSize)
        Kanvas.Arrange(New Rect(panelSize))

        ' Render konten panel ke dalam bitmap
        Dim rtb As New RenderTargetBitmap(CInt(Kanvas.ActualWidth), CInt(Kanvas.ActualHeight), 96, 96, PixelFormats.Pbgra32)
        rtb.Render(Kanvas)

        ' Buat BlockUIContainer untuk memasukkan gambar panel
        Dim img As New Image()
        img.Source = rtb
        Dim blockUI As New BlockUIContainer(img)

        ' Buat FlowDocument untuk menampung gambar yang akan dicetak
        Dim doc As New FlowDocument(blockUI)

        ' Buat Window untuk preview
        Dim previewWindow As New Window With {
            .Title = "Print Preview",
            .Width = 800,
            .Height = 600
        }

        ' DocumentViewer untuk menampilkan preview
        Dim docViewer As New DocumentViewer()
        docViewer.Document = ConvertFlowDocumentToFixedDocument(doc)

        ' Tambahkan DocumentViewer ke Window
        previewWindow.Content = docViewer

        ' Tampilkan preview window
        If previewWindow.ShowDialog() = True Then
            ' Jika pengguna memilih untuk mencetak
            Dim printDlg As New PrintDialog()

            ' Jika user memilih printer dan menekan OK
            If printDlg.ShowDialog() = True Then
                ' Cetak FlowDocument
                printDlg.PrintDocument(CType(docViewer.Document, IDocumentPaginatorSource).DocumentPaginator, "Panel Print")
            End If
        End If
    End Sub


    ' Fungsi untuk mengkonversi FlowDocument ke FixedDocument untuk bisa di-preview
    Private Function ConvertFlowDocumentToFixedDocument(flowDoc As FlowDocument) As FixedDocument
        Dim paginator As DocumentPaginator = CType(flowDoc, IDocumentPaginatorSource).DocumentPaginator

        Dim fixedDoc As New FixedDocument()
        fixedDoc.DocumentPaginator.PageSize = New Size(800, 600)

        ' Proses setiap halaman
        For pageIndex As Integer = 0 To paginator.PageCount - 1
            paginator.GetPage(pageIndex)
            Dim fixedPage As New FixedPage()
            Dim pageContent As New PageContent()

            ' Masukkan page ke dalam FixedDocument
            CType(pageContent, IAddChild).AddChild(fixedPage)
            fixedDoc.Pages.Add(pageContent)
        Next

        Return fixedDoc
    End Function


    Public Function CellFormatAngka_WPF(CellContent, IsiCell) As Boolean
        Dim SebagaiFormatAngka As Boolean
        If IsiCell = Nothing Or IsiCell = Kosongan Then Return False
        Dim IsiCellString As String = IsiCell.ToString
        If CellContent.HorizontalAlignment = HorizontalAlignment.Right _
            And (AmbilTeksKanan(IsiCellString, 1) = "0" _
                Or AmbilTeksKanan(IsiCellString, 1) = "1" _
                Or AmbilTeksKanan(IsiCellString, 1) = "2" _
                Or AmbilTeksKanan(IsiCellString, 1) = "3" _
                Or AmbilTeksKanan(IsiCellString, 1) = "4" _
                Or AmbilTeksKanan(IsiCellString, 1) = "5" _
                Or AmbilTeksKanan(IsiCellString, 1) = "6" _
                Or AmbilTeksKanan(IsiCellString, 1) = "7" _
                Or AmbilTeksKanan(IsiCellString, 1) = "8" _
                Or AmbilTeksKanan(IsiCellString, 1) = "9") _
            And (AmbilTeksKiri(IsiCellString, 1) = "0" _
                Or AmbilTeksKiri(IsiCellString, 1) = "1" _
                Or AmbilTeksKiri(IsiCellString, 1) = "2" _
                Or AmbilTeksKiri(IsiCellString, 1) = "3" _
                Or AmbilTeksKiri(IsiCellString, 1) = "4" _
                Or AmbilTeksKiri(IsiCellString, 1) = "5" _
                Or AmbilTeksKiri(IsiCellString, 1) = "6" _
                Or AmbilTeksKiri(IsiCellString, 1) = "7" _
                Or AmbilTeksKiri(IsiCellString, 1) = "8" _
                Or AmbilTeksKiri(IsiCellString, 1) = "9") _
            Then
            SebagaiFormatAngka = True
        Else
            SebagaiFormatAngka = False
        End If
        Return SebagaiFormatAngka
    End Function


    Public Function CellFormatAngka(IsiCell As String)
        Dim SebagaiFormatAngka As Boolean = False
        If IsiCell = Nothing Or IsiCell = Kosongan Then Return False
        If PanjangTeks(IsiCell.ToString) <= 21 Then
            If IsiCell.ToString = AmbilAngka(IsiCell).ToString Then SebagaiFormatAngka = True
        End If
        Return SebagaiFormatAngka
    End Function


    Public Function CellFormatTanggalStrip(IsiCell)
        Dim SebagaiFormatTanggalStrip As Boolean = False
        If IsiCell = Nothing Or IsiCell = Kosongan Then Return False
        Dim IsiCellString As String = IsiCell.ToString
        If PanjangTeks(IsiCellString) = 10 Then
            If AmbilTeksTengah(IsiCellString, 3, 1) = "-" _
                And AmbilTeksTengah(IsiCellString, 6, 1) = "-" _
                And (AmbilTeksKanan(IsiCellString, 1) = "0" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "1" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "2" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "3" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "4" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "5" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "6" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "7" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "8" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "9") _
                And (AmbilTeksKiri(IsiCellString, 1) = "0" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "1" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "2" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "3" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "4" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "5" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "6" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "7" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "8" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "9") _
            Then
                SebagaiFormatTanggalStrip = True
            Else
                SebagaiFormatTanggalStrip = False
            End If
        End If
        Return SebagaiFormatTanggalStrip
    End Function


    Public Function CellFormatTanggalSlash(IsiCell)
        Dim SebagaiFormatTanggalSlash As Boolean = False
        If IsiCell = Nothing Or IsiCell = Kosongan Then Return False
        Dim IsiCellString As String = IsiCell.ToString
        If PanjangTeks(IsiCellString) = 10 Then
            If AmbilTeksTengah(IsiCellString, 3, 1) = "/" _
                And AmbilTeksTengah(IsiCellString, 6, 1) = "/" _
                And (AmbilTeksKanan(IsiCellString, 1) = "0" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "1" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "2" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "3" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "4" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "5" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "6" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "7" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "8" _
                    Or AmbilTeksKanan(IsiCellString, 1) = "9") _
                And (AmbilTeksKiri(IsiCellString, 1) = "0" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "1" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "2" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "3" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "4" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "5" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "6" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "7" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "8" _
                    Or AmbilTeksKiri(IsiCellString, 1) = "9") _
            Then
                SebagaiFormatTanggalSlash = True
            Else
                SebagaiFormatTanggalSlash = False
            End If
        End If
        Return SebagaiFormatTanggalSlash
    End Function




    'Eksport Tabel ke EXCEL :
    Sub EksporDataTableKeEXCEL(datatableBahanEkspor As DataTable, datagridBahanEkspor As DataGrid)
        win_ProgressExport_EXCEL = New wpfWin_ProgresExport_EXCEL
        win_ProgressExport_EXCEL.ResetForm()
        win_ProgressExport_EXCEL.SumberData = win_ProgressExport_EXCEL.SumberData_DataTable
        win_ProgressExport_EXCEL.datatableBahanEkspor = datatableBahanEkspor
        win_ProgressExport_EXCEL.datagridBahanEkspor = datagridBahanEkspor
        win_ProgressExport_EXCEL.ShowDialog()
    End Sub



    'Eksport Tabel ke EXCEL :
    Sub EksporDataGridKeEXCEL(datagridBahanEkspor As DataGrid)
        win_ProgressExport_EXCEL = New wpfWin_ProgresExport_EXCEL
        win_ProgressExport_EXCEL.ResetForm()
        win_ProgressExport_EXCEL.SumberData = win_ProgressExport_EXCEL.SumberData_DataGrid
        win_ProgressExport_EXCEL.datagridBahanEkspor = datagridBahanEkspor
        win_ProgressExport_EXCEL.ShowDialog()
    End Sub



    Sub EksporDataGridViewKeEXCEL(datagridBahanEkspor As DataGridView)

        Dim JumlahBarisBahanExport
        Dim NamaFileExportEXCEL

        Dim sfd_Simpan As New SaveFileDialog

        JumlahBarisBahanExport = datagridBahanEkspor.RowCount
        If JumlahBarisBahanExport = 0 Then
            PesanPemberitahuan("Tidak ada bahan data yang akan di-export.")
            Return
        Else
            'Dialog Penyimpanan (Save As) :
            sfd_Simpan.FileName = Kosongan
            sfd_Simpan.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd_Simpan.ShowDialog()
            If sfd_Simpan.FileName = Kosongan Then Return
        End If

        NamaFileExportEXCEL = sfd_Simpan.FileName.ToString

        Dim ProsesEkspor As Boolean

        Dim workbook As New XLWorkbook()
        Dim worksheet As IXLWorksheet = workbook.Worksheets.Add("Data Export")

        Dim excelCol As Integer = 1
        Dim columnWidths As New List(Of Double)()

        Try

            ' Menyalin header dan mengumpulkan informasi lebar kolom
            For i As Integer = 0 To datagridBahanEkspor.Columns.Count - 1
                If datagridBahanEkspor.Columns(i).Visible Then
                    worksheet.Cell(1, excelCol).Value = datagridBahanEkspor.Columns(i).HeaderText
                    worksheet.Cell(1, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ' Mengonversi lebar pixel ke unit karakter Excel
                    Dim width As Double = datagridBahanEkspor.Columns(i).Width / 7 + 5
                    columnWidths.Add(width)
                    excelCol += 1
                End If
            Next

            ' Menyalin data
            Dim excelRow As Integer = 2
            For i As Integer = 0 To datagridBahanEkspor.Rows.Count - 1
                If Not datagridBahanEkspor.Rows(i).IsNewRow Then
                    excelCol = 1
                    For j As Integer = 0 To datagridBahanEkspor.Columns.Count - 1
                        If datagridBahanEkspor.Columns(j).Visible Then
                            Dim cellContent As Object = datagridBahanEkspor.Rows(i).Cells(j).Value
                            Dim IsiCell As String = Kosongan
                            If cellContent IsNot Nothing Then
                                IsiCell = cellContent.ToString()
                                IsiCell = PenghapusEnter(IsiCell)
                                If CellFormatTanggalStrip(IsiCell) Then 'Jika Cell Format Tanggal Strip (01-01-1900):
                                    worksheet.Cell(excelRow, excelCol).Value = GantiTeks(IsiCell, "-", "/")
                                    worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/mm/yyyy"
                                    worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                ElseIf CellFormatTanggalSlash(IsiCell) Then 'Jika Cell Format Tanggal Slash (01/01/1900) :
                                    worksheet.Cell(excelRow, excelCol).Value = IsiCell
                                    worksheet.Cell(excelRow, excelCol).Style.NumberFormat.Format = "dd/mm/yyyy"
                                    worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                ElseIf CellFormatAngka(IsiCell) Then 'Jika Cell Fornat Angka :
                                    worksheet.Cell(excelRow, excelCol).Value = AmbilAngka(IsiCell)
                                    worksheet.Cell(excelRow, excelCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                ElseIf IsiCell = StripKosong Then 'Jika Cell berisi StripKosong ("-")
                                    worksheet.Cell(excelRow, excelCol).Value = Kosongan
                                Else
                                    worksheet.Cell(excelRow, excelCol).Value = "'" & IsiCell
                                End If
                            Else
                                IsiCell = Kosongan
                                worksheet.Cell(excelRow, excelCol).Value = IsiCell
                            End If
                            excelCol += 1
                        End If
                    Next
                    excelRow += 1
                End If
            Next

            ' Mengatur lebar kolom
            For i As Integer = 0 To columnWidths.Count - 1
                worksheet.Column(i + 1).Width = columnWidths(i)
            Next

            ' Menyimpan file
            workbook.SaveAs(NamaFileExportEXCEL)

            ProsesEkspor = True

        Catch ex As Exception

            ProsesEkspor = False

        End Try

        If ProsesEkspor = True Then
            PesanSukses("Ekspor BERHASIL..!")
        Else
            PesanSukses("Ekspor GAGAL..!" & Enter2Baris & "Kemungkinan file target kondisi terbuka di Excel.")
        End If

    End Sub

    Function HitungPPNInclude(TotalTagihan As Int64, TarifPPN As Decimal)
        Dim PPN As Int64 = TotalTagihan - (TotalTagihan * (100 / (100 + TarifPPN)))
        Return PPN
    End Function

    Function HitungDPPUntukPPNInclude(Jumlah As Int64, TarifPPN As Decimal) As Decimal
        Return Jumlah - (Jumlah - (Jumlah * 100 / (100 + TarifPPN)))
    End Function

    Function AmbilValue_JenisPPNBerdasarkanPOPenjualan(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JenisPPN = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JenisPPN = drPO.Item("Jenis_PPN")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisPPN
    End Function

    Function AmbilValue_PerlakuanPPNBerdasarkanPOPenjualan(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim PerlakuanPPN = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            PerlakuanPPN = drPO.Item("Perlakuan_PPN")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return PerlakuanPPN
    End Function

    Function AmbilValue_JumlahTerminBerdasarkanPOPenjualan(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JumlahTermin = 0
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JumlahTermin = drPO.Item("Jumlah_Termin")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahTermin
    End Function

    Function AmbilValue_JenisPPNBerdasarkanPOPembelian(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JenisPPN = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JenisPPN = drPO.Item("Jenis_PPN")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisPPN
    End Function

    Function AmbilValue_PerlakuanPPNBerdasarkanPOPembelian(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim PerlakuanPPN = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            PerlakuanPPN = drPO.Item("Perlakuan_PPN")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return PerlakuanPPN
    End Function

    Function AmbilValue_JumlahTerminBerdasarkanPOPembelian(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JumlahTermin = 0
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JumlahTermin = drPO.Item("Jumlah_Termin")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahTermin
    End Function

    Function AmbilValue_JenisPenjualanBerdasarkanInvoicePenjualan(NomorInvoice As String)
        Dim cmdInvoice As OdbcCommand
        Dim drInvoice As OdbcDataReader
        Dim JenisPenjualan = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdInvoice = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drInvoice = cmdInvoice.ExecuteReader()
        drInvoice.Read()
        If drInvoice.HasRows Then
            JenisPenjualan = drInvoice.Item("Jenis_Penjualan")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisPenjualan
    End Function

    Function AmbilValue_JenisPembelianBerdasarkanInvoicePembelian(NomorInvoice As String)
        Dim cmdInvoice As OdbcCommand
        Dim drInvoice As OdbcDataReader
        Dim JenisPembelian = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdInvoice = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drInvoice = cmdInvoice.ExecuteReader()
        drInvoice.Read()
        If drInvoice.HasRows Then
            JenisPembelian = drInvoice.Item("Jenis_Pembelian")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisPembelian
    End Function

    Function AmbilValue_JenisProdukIndukBerdasarkanPOPenjualan(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JenisProdukInduk = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JenisProdukInduk = drPO.Item("Jenis_Produk_Induk")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisProdukInduk
    End Function

    Function AmbilValue_JenisProdukIndukBerdasarkanPOPembelian(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JenisProdukInduk = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JenisProdukInduk = drPO.Item("Jenis_Produk_Induk")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisProdukInduk
    End Function

    Function AmbilValue_JenisJasaBerdasarkanPOPembelian(NomorPO As String)
        Dim cmdPO As OdbcCommand
        Dim drPO As OdbcDataReader
        Dim JenisJasa = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdPO = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drPO = cmdPO.ExecuteReader()
        drPO.Read()
        If drPO.HasRows Then
            JenisJasa = drPO.Item("Jenis_Jasa")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return JenisJasa
    End Function

    Sub IsiValueComboBypassTerkunci(Combo As ComboBox, Value As String)
        If Combo IsNot Nothing Then
            Combo.Items.Clear()
            Combo.Items.Add(Value)
            Combo.SelectedValue = Value
        Else
            PesanUntukProgrammer("Elemen Combo masih Nothing...!!!")
        End If
    End Sub

    Sub IsiValueComboBypassTerbuka(Combo As ComboBox, Value As String)
        If Combo IsNot Nothing Then
            Combo.SelectedValue = Value
        Else
            PesanUntukProgrammer("Elemen Combo masih Nothing...!!!")
        End If
    End Sub

    Function PenyesuaianAngkaBerdasarkanMataUang(KodeMataUang As String, Angka As Decimal) As String
        If KodeMataUang = Kosongan Then KodeMataUang = KodeMataUang_IDR
        If KodeMataUang = KodeMataUang_IDR Then
            FormatUlangAngkaKeBilanganBulat(Angka)
        Else
            FormatUlangAngkaKeBilanganDesimal(Angka)
        End If
        Return Angka.ToString
    End Function

    Function Ambilangka_MultiCurrency(KodeNataUang As String, Teksboks As TextBox) As Decimal
        Dim Jumlah As Decimal
        If KodeNataUang = KodeMataUang_IDR Then
            Jumlah = AmbilAngka(Teksboks.Text)
        Else
            Jumlah = AmbilAngka_Asing(Teksboks.Text)
        End If
        Return Jumlah
    End Function

    Sub IsiValue_DateTimePicker_DariDatabaseMySQL(Kolom_dB As String, dtp_Tanggal As DatePicker)
        If TanggalFormatTampilan(dr.Item(Kolom_dB)) = TanggalFormatTampilan(TanggalKosong) Then
            dtp_Tanggal.Text = Kosongan
        Else
            dtp_Tanggal.SelectedDate = dr.Item(Kolom_dB)
        End If
    End Sub


    Sub KosongkanItemCombo(Combo As ComboBox)
        If Combo IsNot Nothing Then
            Combo.Items.Clear()
            Combo.Text = Kosongan
        Else
            PesanUntukProgrammer("Elemen Combo masih Nothing...!!!")
        End If
    End Sub

    Function AmbilValueCellTeksBerpotensiDBNull_RowView(RowView As DataRowView, Kolom As String)
        Dim isi = RowView(Kolom)
        If IsDBNull(isi) Then isi = Kosongan
        Return isi
    End Function

    Function AmbilValueCellTeksBerpotensiDBNull_Row(Row As DataRow, Kolom As String)
        Dim isi = Row(Kolom)
        If IsDBNull(isi) Then isi = Kosongan
        Return isi
    End Function

    Sub KonversiDBNullJadiKosongan(ByRef Isi)
        If IsDBNull(Isi) Then Isi = Kosongan
    End Sub

    Sub KonversiDBNullJadiTanggalKosong(ByRef Isi)
        If IsDBNull(Isi) Then Isi = Kosongan
        If Isi = Kosongan Then Isi = TanggalKosong
    End Sub


    Function AmbilValue_ListProdukBerdasarkanInvoicePembelian(NomorInvoice As String)
        Dim cmdProduk As OdbcCommand
        Dim drProduk As OdbcDataReader
        Dim ListProduk = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdProduk = New OdbcCommand(" SELECT Nama_Produk FROM tbl_Pembelian_Invoice " &
                                    " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drProduk = cmdProduk.ExecuteReader()
        Do While drProduk.Read
            If ListProduk = Kosongan Then
                ListProduk = drProduk.Item("Nama_Produk")
            Else
                ListProduk += SlashGanda_Pemisah & drProduk.Item("Nama_Produk")
            End If
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return ListProduk
    End Function


    Function AmbilValue_ListProdukBerdasarkanInvoicePenjualan(NomorInvoice As String)
        Dim cmdProduk As OdbcCommand
        Dim drProduk As OdbcDataReader
        Dim ListProduk = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        cmdProduk = New OdbcCommand(" SELECT Nama_Produk FROM tbl_Penjualan_Invoice " &
                                    " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drProduk = cmdProduk.ExecuteReader()
        Do While drProduk.Read
            If ListProduk = Kosongan Then
                ListProduk = drProduk.Item("Nama_Produk")
            Else
                ListProduk += SlashGanda_Pemisah & drProduk.Item("Nama_Produk")
            End If
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return ListProduk
    End Function

    Sub LihatJurnal(NomorJV)
        win_JurnalVoucher = New wpfWin_JurnalVoucher
        win_JurnalVoucher.ResetForm()
        win_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        win_JurnalVoucher.Angka_NomorJV = NomorJV
        win_JurnalVoucher.ShowDialog()
    End Sub

    Sub BukuBesarPembantu(KodeLawanTransaksi, COA)
        win_BukuBesarPembantu = New wpfWin_BukuBesarPembantu
        win_BukuBesarPembantu.ResetForm()
        win_BukuBesarPembantu.KodeLawanTransaksi = KodeLawanTransaksi
        win_BukuBesarPembantu.COABukuPembantu = COA
        win_BukuBesarPembantu.ShowDialog()
    End Sub

    Function COATermasukDEBET(COA) As Boolean
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        Dim TermasukDebet As Boolean = False
        BukaDatabaseGeneral_Kondisional()
        cmdCOA = New OdbcCommand(" SELECT D_K FROM tbl_COA " &
                                 " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        drCOA = cmdCOA.ExecuteReader()
        drCOA.Read()
        If drCOA.HasRows Then
            If drCOA.Item("D_K") = "DEBET" Then TermasukDebet = True
        End If
        TutupDatabaseGeneral_Kondisional()
        Return TermasukDebet
    End Function

    Function COATermasukMUA(COA) As Boolean
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        Dim TermasukMUA As Boolean = False
        BukaDatabaseGeneral_Kondisional()
        cmdCOA = New OdbcCommand(" SELECT Kode_Mata_Uang FROM tbl_COA " &
                                 " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        drCOA = cmdCOA.ExecuteReader()
        drCOA.Read()
        If drCOA.HasRows Then
            If drCOA.Item("Kode_Mata_Uang") <> KodeMataUang_IDR Then TermasukMUA = True
        End If
        TutupDatabaseGeneral_Kondisional()
        Return TermasukMUA
    End Function


    Function COATermasukIDR(COA) As Boolean
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        Dim TermasukIDR As Boolean = False
        BukaDatabaseGeneral_Kondisional()
        cmdCOA = New OdbcCommand(" SELECT Kode_Mata_Uang FROM tbl_COA " &
                                 " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        drCOA = cmdCOA.ExecuteReader()
        drCOA.Read()
        If drCOA.HasRows Then
            If drCOA.Item("Kode_Mata_Uang") = KodeMataUang_IDR Then TermasukIDR = True
        End If
        TutupDatabaseGeneral_Kondisional()
        Return TermasukIDR
    End Function


    Function COATermasukKREDIT(COA)
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        Dim TermasukKredit As Boolean = False
        BukaDatabaseGeneral_Kondisional()
        cmdCOA = New OdbcCommand(" SELECT D_K FROM tbl_COA " &
                                 " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        drCOA = cmdCOA.ExecuteReader()
        drCOA.Read()
        If drCOA.HasRows Then
            If drCOA.Item("D_K") = "KREDIT" Then TermasukKredit = True
        End If
        TutupDatabaseGeneral_Kondisional()
        Return TermasukKredit
    End Function



    Public Sub PesanPeringatan_SilakanIsiKolomTeks(ByVal eTextBox As TextBox, ByVal NamaKolom As String)
        PesanPeringatan("Silakan isi kolom '" & NamaKolom & "'.")
        eTextBox.Focus()
    End Sub


    Public Sub PesanPeringatan_SilakanIsiKolomTeksKaya(ByVal eTextBox As RichTextBox, ByVal NamaKolom As String)
        PesanPeringatan("Silakan isi kolom '" & NamaKolom & "'.")
        eTextBox.Focus()
    End Sub


    Public Sub PesanPeringatan_SilakanIsiKolomTanggal(ByVal eDatePicker As DatePicker, ByVal NamaKolomTanggal As String)
        PesanPeringatan("Silakan isi '" & NamaKolomTanggal & "'.")
        eDatePicker.Focus()
    End Sub


    Public Sub PesanPeringatan_SilakanPilihCombo(ByVal eCombo As ComboBox, ByVal NamaCombo As String)
        PesanPeringatan("Silakan pilih '" & NamaCombo & "'.")
        eCombo.Focus()
    End Sub


    Function KonversiNomorJVStringKeAngka(NomorJVString As String)
        Dim AngkaNomorJV As Int64
        AngkaNomorJV = AmbilAngka(Mid(NomorJVString, PanjangTeks_AwalanNomorJV_Plus1))
        Return AngkaNomorJV
    End Function

    Function KonversiNomorJVAngkaKeString(AngkaNomorJV As Int64)
        Dim NomorJVSrting As String
        NomorJVSrting = AwalanNomorJV & AngkaNomorJV
        Return NomorJVSrting
    End Function

    Public Sub EditJurnal(NomorJV As Int64)
        'frm_InputJurnal.ResetForm()
        'frm_InputJurnal.JalurMasuk = Halaman_JURNALUMUM
        'frm_InputJurnal.FungsiForm = FungsiForm_EDIT
        'frm_InputJurnal.AngkaNomorJV = NomorJV
        'frm_InputJurnal.ShowDialog()
        win_InputJurnal = New wpfWin_InputJurnal
        win_InputJurnal.ResetForm()
        win_InputJurnal.JalurMasuk = Halaman_JURNALUMUM
        win_InputJurnal.FungsiForm = FungsiForm_EDIT
        win_InputJurnal.AngkaNomorJV = NomorJV
        win_InputJurnal.ShowDialog()
    End Sub

    Function AmbilValue_JumlahBarisJurnal(NomorJV As Int64)
        Dim JumlahBarisJurnal As Integer = 0
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                    " WHERE Nomor_JV = '" & NomorJV & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader()
        Do While drKhusus.Read
            JumlahBarisJurnal += 1
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahBarisJurnal
    End Function

    Sub BukaHalamanBukuBesar(COA)
        win_BOOKU.BukaModul_BukuBesar(COA)
    End Sub

    Sub BukaFormListLawanTransaksi(txt_KodeLawanTransaksi As TextBox,
                                   txt_NamaLawanTransaksi As TextBox,
                                   CustomerSupplier As String,
                                   LokasiLawanTransaksi As String,
                                   SebagaiPemegangSaham As String,
                                   SebagaiAfiliasi As String,
                                   SebagaiLembagaKeuangan As String
                                   )
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = CustomerSupplier
        win_ListLawanTransaksi.PilihLokasiWP = LokasiLawanTransaksi
        win_ListLawanTransaksi.PilihPemegangSaham = SebagaiPemegangSaham
        win_ListLawanTransaksi.PilihAfiliasi = SebagaiAfiliasi
        win_ListLawanTransaksi.PilihLembagaKeuangan = SebagaiLembagaKeuangan
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeLawanTransaksi.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaLawanTransaksi.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
    End Sub


    Sub KontenComboBulan_Public_WPF(ComboBulan As ComboBox)
        ComboBulan.Items.Clear()
        ComboBulan.Items.Add(Bulan_Januari)
        ComboBulan.Items.Add(Bulan_Februari)
        ComboBulan.Items.Add(Bulan_Maret)
        ComboBulan.Items.Add(Bulan_April)
        ComboBulan.Items.Add(Bulan_Mei)
        ComboBulan.Items.Add(Bulan_Juni)
        ComboBulan.Items.Add(Bulan_Juli)
        ComboBulan.Items.Add(Bulan_Agustus)
        ComboBulan.Items.Add(Bulan_September)
        ComboBulan.Items.Add(Bulan_Oktober)
        ComboBulan.Items.Add(Bulan_Nopember)
        ComboBulan.Items.Add(Bulan_Desember)
    End Sub


    Sub KontenComboBulanDibatasi_Public_WPF(ComboBulan As ComboBox, BatasBulanAngka As Integer)
        Dim BulanAngkaTelusur As Integer = 0
        ComboBulan.Items.Clear()
        If BatasBulanAngka = 0 Then Return
        If BatasBulanAngka > 12 Then BatasBulanAngka = 12
        Do While BulanAngkaTelusur < BatasBulanAngka
            BulanAngkaTelusur += 1
            ComboBulan.Items.Add(KonversiAngkaKeBulanString(BulanAngkaTelusur))
        Loop
    End Sub


    Public TotalDebetBahanJurnal As Int64
    Public TotalKreditBahanJurnal As Int64
    Public NomorUrutBahanJurnal
    Public Sub Reset_BahanJurnal(JenisJurnal As String, TanggalJurnal As Date)
        TotalDebetBahanJurnal = 0
        TotalKreditBahanJurnal = 0
        NomorUrutBahanJurnal = 0
        win_InputJurnal = New wpfWin_InputJurnal
        win_InputJurnal.ResetForm()
        win_InputJurnal.JalurMasuk = Menu_JurnalAdjusment
        win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        win_InputJurnal.cmb_JenisJurnal.SelectedValue = JenisJurnal
        win_InputJurnal.dtp_TanggalJurnal.SelectedDate = TanggalFormatWPF(TanggalJurnal)
        win_InputJurnal.datatabelUtama.Rows.Clear() '(Ini jangan dihapus...!!!)
    End Sub
    Sub TambahBarisDebet_BahanJurnal(COA, JumlahDebet)
        NomorUrutBahanJurnal += 1
        TotalDebetBahanJurnal += JumlahDebet
        win_InputJurnal.datatabelUtama.Rows.Add(NomorUrutBahanJurnal, COA, AmbilValue_NamaAkun(COA), dk_D, JumlahDebet, 0)
    End Sub
    Sub TambahBarisKredit_BahanJurnal(COA, JumlahKredit)
        NomorUrutBahanJurnal += 1
        TotalKreditBahanJurnal += JumlahKredit
        win_InputJurnal.datatabelUtama.Rows.Add(NomorUrutBahanJurnal, COA, PenjorokNamaAkun & AmbilValue_NamaAkun(COA), dk_K, 0, JumlahKredit)
    End Sub
    Sub TampilkanFormInputJurnal()
        win_InputJurnal.JumlahBarisJurnal = NomorUrutBahanJurnal
        win_InputJurnal.datatabelUtama.Rows.Add()
        win_InputJurnal.datatabelUtama.Rows.Add()
        win_InputJurnal.datatabelUtama.Rows(NomorUrutBahanJurnal + 1)("Jumlah_Debet") = TotalDebetBahanJurnal
        win_InputJurnal.datatabelUtama.Rows(NomorUrutBahanJurnal + 1)("Jumlah_Kredit") = TotalKreditBahanJurnal
        If TotalDebetBahanJurnal = TotalKreditBahanJurnal Then
            win_InputJurnal.lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
            win_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            win_InputJurnal.btn_Simpan.IsEnabled = True
        Else
            win_InputJurnal.lbl_StatusBalance.Foreground = WarnaMerahSolid_WPF
            win_InputJurnal.lbl_StatusBalance.Text = "Ada Selisih"
            win_InputJurnal.btn_Simpan.IsEnabled = False
        End If
        win_InputJurnal.ShowDialog()
    End Sub

    Sub StartProgress(pgb_Progress As ProgressBar, Minimum As Int64, Maximum As Int64)
        pgb_Progress.Foreground = WarnaHijauSolid_WPF
        ProgressMinimum = Minimum
        ProgressMaximum = Maximum
        ProgressValue = Minimum
        pgb_Progress.Minimum = ProgressMinimum
        pgb_Progress.Maximum = ProgressMaximum
        pgb_Progress.Value = ProgressValue
    End Sub
    Sub ProgressUp(pgb_Progress As ProgressBar)
        pgb_Progress.Value += 1
        ProgressValue = pgb_Progress.Value
        Terabas()
    End Sub

    Sub AlgoritmaDPP(ByRef DPP)
        If TahunBukuAktif = 2025 Then DPP = 11 / 12 * DPP
    End Sub

    Public PengambilanGambar As Boolean
    Sub IsiGambarLogoPerusahaan(Gambar As Image)
        Try
            Dim imageBytes() As Byte = File.ReadAllBytes(FilePathLogoPerusahaan)
            Using ms As New MemoryStream(imageBytes)
                Dim bitmap As New BitmapImage()
                bitmap.BeginInit()
                bitmap.StreamSource = ms
                bitmap.CacheOption = BitmapCacheOption.OnLoad
                bitmap.EndInit()
                Gambar.Source = bitmap
            End Using
            PengambilanGambar = True
        Catch ex As Exception
            PengambilanGambar = False
        End Try
    End Sub
    Sub KosongkanLogoPerusahaan(Gambar As Image)
        Gambar.Source = Nothing
    End Sub

    Sub PerbaruiVariabelTerkaitServer()
        urlLocalhost = "http://" & LokasiServerDatabase & ":" & PortApache
        urlPhpMyAdmin = urlLocalhost & "/phpmyadmin"
    End Sub


    Function JumlahItemProdukDiPOPembelianYangBelumDikirim(NomorPO As String) As Integer
        Dim NamaProduk
        Dim JumlahItemProdukYangBelumDikim As Integer = 0
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim MetodePembayaran As String = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHUSUS = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "'",
                                        KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Dim cmdTELUSUR As OdbcCommand
        Dim drTELUSUR As OdbcDataReader
        Do While drKHUSUS.Read
            MetodePembayaran = drKHUSUS.Item("Metode_Pembayaran")
            JumlahProduk_Dipesan = drKHUSUS.Item("Jumlah_Produk")
            NamaProduk = drKHUSUS.Item("Nama_Produk")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR = cmdTELUSUR.ExecuteReader
            JumlahProduk_Dieksekusi = 0
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_BAST " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR = cmdTELUSUR.ExecuteReader
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            If JumlahProduk_Dipesan > JumlahProduk_Dieksekusi Then JumlahItemProdukYangBelumDikim += 1
            'PesanUntukProgrammer("Jumlah Produk Dipesan : " & JumlahProduk_Dipesan & Enter2Baris &
            '                     "Jumlah Produk Dikirim: " & JumlahProduk_Dieksekusi)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahItemProdukYangBelumDikim
    End Function

    Sub UpdateStatusKontrolPOPembelian(NomorPO, StatusKontrol)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdEDIT = New OdbcCommand(" UPDATE  tbl_Pembelian_PO " &
                                      " SET     Kontrol   = '" & StatusKontrol & "' " &
                                      " WHERE   Nomor_PO  = '" & NomorPO & "' ",
                                      KoneksiDatabaseTransaksi)
        Try
            cmdEDIT.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    Sub UpdateStatusKontrolPOPembelianBerdasarkanMetodePembayaran(MetodePembayaran, NomorPO)
        Dim StatusKontrolPO As String = Kosongan
        Dim JumlahItemProdukYangBelumDikim As Integer = JumlahItemProdukDiPOPembelianYangBelumDikirim(NomorPO)
        If MetodePembayaran = MetodePembayaran_Normal Then
            If JumlahItemProdukYangBelumDikim > 0 Then
                StatusKontrolPO = Status_Used
            Else
                StatusKontrolPO = Status_Closed
            End If
        Else
            StatusKontrolPO = Status_Used
        End If
        PesanUntukProgrammer("Nomor PO : " & NomorPO & Enter2Baris &
                             "Metode Pembayaran : " & MetodePembayaran & Enter2Baris &
                             "Item Belum Dikirim : " & JumlahItemProdukYangBelumDikim & Enter2Baris &
                             "Status Kontrol : " & StatusKontrolPO)
        UpdateStatusKontrolPOPembelian(NomorPO, StatusKontrolPO)
    End Sub


    Function AdaPOdiDataSJBASTPembelian(NomorPO As String) As Boolean
        Dim AdaPO As Boolean = False
        Dim AdaPOdiSJ As Boolean = False
        Dim AdaPOdiBAST As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Pembelian_SJ WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPOdiSJ = True
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Pembelian_BAST WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPOdiBAST = True
        TutupDatabaseTransaksi_Kondisional()
        If AdaPOdiSJ Or AdaPOdiBAST Then AdaPO = True
        Return AdaPO
    End Function


    Function AdaPOdiDataSJBASTPenjualan(NomorPO As String) As Boolean
        Dim AdaPO As Boolean = False
        Dim AdaPOdiSJ As Boolean = False
        Dim AdaPOdiBAST As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Penjualan_SJ WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPOdiSJ = True
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Penjualan_BAST WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPOdiBAST = True
        TutupDatabaseTransaksi_Kondisional()
        If AdaPOdiSJ Or AdaPOdiBAST Then AdaPO = True
        Return AdaPO
    End Function


    Function AdaPOdiDataInvoicePembelian(NomorPO As String) As Boolean
        Dim AdaPO As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Pembelian_Invoice WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPO = True
        TutupDatabaseTransaksi_Kondisional()
        Return AdaPO
    End Function


    Function AdaPOdiDataInvoicePenjualan(NomorPO As String) As Boolean
        Dim AdaPO As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_PO_Produk FROM tbl_Penjualan_Invoice WHERE Nomor_PO_Produk = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaPO = True
        TutupDatabaseTransaksi_Kondisional()
        Return AdaPO
    End Function


    Function JumlahItemProdukDiPOPenjualanYangBelumDikirim(NomorPO As String) As Integer
        Dim NamaProduk
        Dim JumlahItemProdukYangBelumDikim As Integer = 0
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim MetodePembayaran As String = Kosongan
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdKHUSUS = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                        " WHERE Nomor_PO = '" & NomorPO & "'",
                                        KoneksiDatabaseTransaksi)
        Dim drKHUSUS = cmdKHUSUS.ExecuteReader
        Dim cmdTELUSUR As OdbcCommand
        Dim drTELUSUR As OdbcDataReader
        Do While drKHUSUS.Read
            MetodePembayaran = drKHUSUS.Item("Metode_Pembayaran")
            JumlahProduk_Dipesan = drKHUSUS.Item("Jumlah_Produk")
            NamaProduk = drKHUSUS.Item("Nama_Produk")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR = cmdTELUSUR.ExecuteReader
            JumlahProduk_Dieksekusi = 0
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR = cmdTELUSUR.ExecuteReader
            Do While drTELUSUR.Read
                JumlahProduk_Dieksekusi += drTELUSUR.Item("Jumlah_Produk")
            Loop
            If JumlahProduk_Dipesan > JumlahProduk_Dieksekusi Then JumlahItemProdukYangBelumDikim += 1
            'PesanUntukProgrammer("Jumlah Produk Dipesan : " & JumlahProduk_Dipesan & Enter2Baris &
            '                     "Jumlah Produk Dikirim: " & JumlahProduk_Dieksekusi)
        Loop
        TutupDatabaseTransaksi_Kondisional()
        Return JumlahItemProdukYangBelumDikim
    End Function

    Sub UpdateStatusKontrolPOPenjualan(NomorPO, StatusKontrol)
        BukaDatabaseTransaksi_Kondisional()
        Dim cmdEDIT = New OdbcCommand(" UPDATE  tbl_Penjualan_PO " &
                                      " SET     Kontrol   = '" & StatusKontrol & "' " &
                                      " WHERE   Nomor_PO  = '" & NomorPO & "' ",
                                      KoneksiDatabaseTransaksi)
        Try
            cmdEDIT.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        TutupDatabaseTransaksi_Kondisional()
    End Sub

    Sub UpdateStatusKontrolPOPenjualanBerdasarkanMetodePembayaran(MetodePembayaran, NomorPO)
        Dim StatusKontrolPO As String = Kosongan
        Dim JumlahItemProdukYangBelumDikim As Integer = JumlahItemProdukDiPOPenjualanYangBelumDikirim(NomorPO)
        If MetodePembayaran = MetodePembayaran_Normal Then
            If JumlahItemProdukYangBelumDikim > 0 Then
                StatusKontrolPO = Status_Used
            Else
                StatusKontrolPO = Status_Closed
            End If
        Else
            StatusKontrolPO = Status_Used
        End If
        PesanUntukProgrammer("Nomor PO : " & NomorPO & Enter2Baris &
                             "Metode Pembayaran : " & MetodePembayaran & Enter2Baris &
                             "Item Belum Dikirim : " & JumlahItemProdukYangBelumDikim & Enter2Baris &
                             "Status Kontrol : " & StatusKontrolPO)
        UpdateStatusKontrolPOPenjualan(NomorPO, StatusKontrolPO)
    End Sub


    Function AdaSJBASTdiDataInvoicePembelian(NomorSJBAST As String) As Boolean
        Dim AdaSJBAST As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_SJ_BAST_Produk FROM tbl_Pembelian_Invoice WHERE Nomor_SJ_BAST_Produk = '" & NomorSJBAST & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaSJBAST = True
        TutupDatabaseTransaksi_Kondisional()
        Return AdaSJBAST
    End Function

    Function AdaSJBASTdiDataInvoicePenjualan(NomorSJBAST As String) As Boolean
        Dim AdaSJBAST As Boolean = False
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT Nomor_SJ_BAST_Produk FROM tbl_Penjualan_Invoice WHERE Nomor_SJ_BAST_Produk = '" & NomorSJBAST & "' ", KoneksiDatabaseTransaksi)
        drKhusus = cmdKhusus.ExecuteReader
        drKhusus.Read()
        If drKhusus.HasRows Then AdaSJBAST = True
        TutupDatabaseTransaksi_Kondisional()
        Return AdaSJBAST
    End Function


    'KONVERSI UANG ASING KE RUPIAH - BULAT OTOMATIS :
    Function AmbilValue_NilaiMataUang(KodeMataUang As String, Kurs As Decimal, JumlahUang As Decimal) As Int64
        Dim Hasil As Int64 = 0
        If KodeMataUang = KodeMataUang_JPY Then JumlahUang /= 100
        Hasil = Convert.ToInt64(Math.Round(Kurs * JumlahUang, MidpointRounding.AwayFromZero))
        Return Hasil
    End Function


    Function AmbilValue_NilaiMataUang_WithCOA(COA As String, KodeMataUang As String, Kurs As String, Jumlah As Decimal) As Int64
        Dim Hasil As Int64
        If COA = KodeTautanCOA_BiayaSelisihPencatatan Then
            Kurs = 1
            If KodeMataUang = KodeMataUang_JPY Then Kurs = 100
        End If
        Hasil = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, Jumlah)
        Return Hasil
    End Function


    'KONVERSI UANG ASING KE RUPIAH - BULAT KE ATAS :
    Function AmbilValue_NilaiMataUang_BulatKeAtas(KodeMataUang As String, Kurs As Decimal, JumlahUang As Decimal) As Int64
        Dim Hasil As Int64 = 0
        If KodeMataUang = KodeMataUang_JPY Then JumlahUang /= 100
        Hasil = Convert.ToInt64(Math.Ceiling(Kurs * JumlahUang))
        Return Hasil
    End Function


    'KONVERSI UANG ASING KE RUPIAH - BULAT KE BAWAH :
    Function AmbilValue_NilaiMataUang_BulatKeBawah(KodeMataUang As String, Kurs As Decimal, JumlahUang As Decimal) As Int64
        Dim Hasil As Int64 = 0
        If KodeMataUang = KodeMataUang_JPY Then JumlahUang /= 100
        Hasil = Convert.ToInt64(Math.Floor(Kurs * JumlahUang))
        Return Hasil
    End Function

    Function PenentuanCOA_HutangUsahaImpor_BerdasarkanKodeMataUang(KodeMataUang As String) As String
        Dim COAHutangUsahaImpor As String = Kosongan
        Select Case KodeMataUang
            Case KodeMataUang_USD
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_USD
            Case KodeMataUang_AUD
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_AUD
            Case KodeMataUang_JPY
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_JPY
            Case KodeMataUang_CNY
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_CNY
            Case KodeMataUang_EUR
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_EUR
            Case KodeMataUang_SGD
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_SGD
            Case KodeMataUang_GBP
                COAHutangUsahaImpor = KodeTautanCOA_HutangUsaha_GBP
        End Select
        Return COAHutangUsahaImpor
    End Function

    Function PenentuanCOA_PiutangUsahaEkspor_BerdasarkanKodeMataUang(KodeMataUang As String) As String
        Dim COAPiutangUsahaEkspor As String = Kosongan
        Select Case KodeMataUang
            Case KodeMataUang_USD
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_USD
            Case KodeMataUang_AUD
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_AUD
            Case KodeMataUang_JPY
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_JPY
            Case KodeMataUang_CNY
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_CNY
            Case KodeMataUang_EUR
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_EUR
            Case KodeMataUang_SGD
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_SGD
            Case KodeMataUang_GBP
                COAPiutangUsahaEkspor = KodeTautanCOA_PiutangUsaha_GBP
        End Select
        Return COAPiutangUsahaEkspor
    End Function

    Function KonversiDesimalKeInt64BulatKeAtas(AngkaDesimal As Decimal) As Int64
        Dim Hasil As Int64
        Try
            Hasil = Convert.ToInt64(Math.Ceiling(AngkaDesimal))
        Catch ex As Exception
            PesanUntukProgrammer("Ada Kesalahan pada Function Konversi Desimal ke Integer...!!!" & Enter2Baris & "Angka Desimal : " & AngkaDesimal)
        End Try
        Return Hasil
    End Function

    Function KonversiDesimalKeInt64BulatKeBawah(AngkaDesimal As Decimal) As Int64
        Dim Hasil As Int64
        Try
            Hasil = Convert.ToInt64(Math.Floor(AngkaDesimal))
        Catch ex As Exception
            PesanUntukProgrammer("Ada Kesalahan pada Function Konversi Desimal ke Integer...!!!" & Enter2Baris & "Angka Desimal : " & AngkaDesimal)
        End Try
        Return Hasil
    End Function


    Function KonversiDesimalKeInt64BulatOtomatis(AngkaDesimal As Decimal) As Int64
        Dim Hasil As Int64
        Try
            Hasil = Convert.ToInt64(Math.Round(AngkaDesimal))
        Catch ex As Exception
            PesanUntukProgrammer("Ada Kesalahan pada Function Konversi Desimal ke Integer...!!!" & Enter2Baris & "Angka Desimal : " & AngkaDesimal)
        End Try
        Return Hasil
    End Function


    Sub InputJurnalAdjusmentAkhirBulan_Forex(COA As String, BulanAngka As Integer)

        Dim KodeMataUang As String = AmbilValue_KodeMataUang_BerdasarkanCOA(COA)
        Dim TanggalAkhirBulan As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
        Dim TanggalTerakhirTransaksi As Date = TanggalTerakhirTransaksiCOA_DiBulanTertentuaAtauSebelumnya(COA, BulanAngka)
        Dim AdaJurnal As Boolean = CekKeberadaanJurnal_DiTanggalTertentu(COA, JenisJurnal_AdjusmentForex, TanggalAkhirBulan)

        If AdaJurnal Then
            PesanPemberitahuan("Jurnal Adjusment Forex " & AmbilValue_NamaAkun(COA) & " untuk bulan " & KonversiAngkaKeBulanString(BulanAngka) & " sudah ada." & Enter2Baris &
                               "Proses Adjusment tidak perlu dilanjutkan.")
            Return
        End If

        Dim SaldoTerakhirTransaksi_MUA As Decimal = SaldoAkhirCOA_SampaiAkhirBulanTertentu_MUA(COA, BulanAngka)
        Dim SaldoTerakhirTransaksi_IDR As Int64 = SaldoAkhirCOA_SampaiAkhirBulanTertentu(COA, BulanAngka)
        Dim KursAkhirBulan As Decimal = KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka)
        Dim SaldoAkhirBulan_IDR As Int64 = AmbilValue_NilaiMataUang(KodeMataUang, KursAkhirBulan, SaldoTerakhirTransaksi_MUA)
        Dim Selisih As Int64 = SaldoTerakhirTransaksi_IDR - SaldoAkhirBulan_IDR
        Dim NamaAkun_YangDiadjusment
        Dim NamaAkun_LabaRugiSelisihKurs
        Dim JumlahPenyesuaian = Selisih
        'PesanUntukProgrammer("Kode Akun : " & COA & Enter2Baris &
        '    "Kode Mata Uang : " & KodeMataUang & Enter2Baris &
        '    "Saldo Akhir MUA : " & SaldoAkhirMUA & Enter2Baris &
        '    "Saldo Terakhir Transaksi : " & SaldoTerakhirTransaksi & Enter2Baris &
        '    "Kurs Akhir Bulan : " & KursAkhirBulan & Enter2Baris &
        '    "Saldo Akhir Bulan : " & SaldoAkhirBulan & Enter2Baris &
        '    "Selisih : " & Selisih & Enter2Baris &
        '    "")
        NamaAkun_YangDiadjusment = AmbilValue_NamaAkun(COA)
        NamaAkun_LabaRugiSelisihKurs = AmbilValue_NamaAkun(KodeTautanCOA_LabaRugiSelisihKurs)
        win_InputJurnalAdjusmentForex = New wpfWin_InputJurnalAdjusmentForex
        win_InputJurnalAdjusmentForex.ResetForm()
        win_InputJurnalAdjusmentForex.JalurMasuk = Halaman_JURNALADJUSMENT
        win_InputJurnalAdjusmentForex.FungsiForm = FungsiForm_TAMBAH
        win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Clear() '(Ini jangan dihapus...!!!)
        If JumlahPenyesuaian >= 0 Then
            If COATermasukDEBET(COA) = True Then
                win_InputJurnalAdjusmentForex.newRow = win_InputJurnalAdjusmentForex.datatabelUtama.NewRow()
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(1, KodeTautanCOA_LabaRugiSelisihKurs,
                                                       NamaAkun_LabaRugiSelisihKurs, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(2, COA,
                                                       PenjorokNamaAkun & NamaAkun_YangDiadjusment, dk_K, 0, JumlahPenyesuaian)
            Else
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(1, COA,
                                                       NamaAkun_YangDiadjusment, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(2, KodeTautanCOA_LabaRugiSelisihKurs,
                                                       PenjorokNamaAkun & NamaAkun_LabaRugiSelisihKurs, dk_K, 0, JumlahPenyesuaian)
            End If
        Else
            JumlahPenyesuaian = -JumlahPenyesuaian
            If COATermasukDEBET(COA) = True Then
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(1, COA,
                                                       NamaAkun_YangDiadjusment, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(2, KodeTautanCOA_LabaRugiSelisihKurs,
                                                       PenjorokNamaAkun & NamaAkun_LabaRugiSelisihKurs, dk_K, 0, JumlahPenyesuaian)
            Else
                win_InputJurnalAdjusmentForex.newRow = win_InputJurnalAdjusmentForex.datatabelUtama.NewRow()
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(1, KodeTautanCOA_LabaRugiSelisihKurs,
                                                       NamaAkun_LabaRugiSelisihKurs, dk_D, JumlahPenyesuaian, 0)
                win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add(2, COA,
                                                       PenjorokNamaAkun & NamaAkun_YangDiadjusment, dk_K, 0, JumlahPenyesuaian)
            End If
        End If
        win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add()
        win_InputJurnalAdjusmentForex.datatabelUtama.Rows.Add()
        win_InputJurnalAdjusmentForex.datatabelUtama.Rows(3)("Jumlah_Debet") = JumlahPenyesuaian
        win_InputJurnalAdjusmentForex.datatabelUtama.Rows(3)("Jumlah_Kredit") = JumlahPenyesuaian
        win_InputJurnalAdjusmentForex.lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
        win_InputJurnalAdjusmentForex.lbl_StatusBalance.Text = "Tidak Ada Selisih"
        win_InputJurnalAdjusmentForex.dtp_TanggalJurnal.SelectedDate = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
        win_InputJurnalAdjusmentForex.cmb_JenisJurnal.SelectedValue = JenisJurnal_AdjusmentForex
        win_InputJurnalAdjusmentForex.dtp_TanggalJurnal.IsEnabled = False
        win_InputJurnalAdjusmentForex.cmb_JenisJurnal.IsEnabled = False
        win_InputJurnalAdjusmentForex.KodeMataUang = KodeMataUang
        win_InputJurnalAdjusmentForex.txt_KursAkhirBulan.Text = KursAkhirBulan
        win_InputJurnalAdjusmentForex.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Internal
        win_InputJurnalAdjusmentForex.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Internal
        win_InputJurnalAdjusmentForex.btn_Reset.IsEnabled = False
        win_InputJurnalAdjusmentForex.btn_Simpan.IsEnabled = True
        win_InputJurnalAdjusmentForex.JumlahBarisJurnal = 2
        win_InputJurnalAdjusmentForex.TanggalTerakhirTransaksi = TanggalTerakhirTransaksi
        win_InputJurnalAdjusmentForex.AkunAdjusment = COA
        win_InputJurnalAdjusmentForex.txt_SaldoAkhirMUA.Text = SaldoTerakhirTransaksi_MUA
        win_InputJurnalAdjusmentForex.txt_SaldoAkhirIDR.Text = SaldoTerakhirTransaksi_IDR
        win_InputJurnalAdjusmentForex.txt_SaldoAkhirBulanIDR.Text = SaldoAkhirBulan_IDR
        win_InputJurnalAdjusmentForex.txt_Selisih.Text = Selisih
        win_InputJurnalAdjusmentForex.ShowDialog()

    End Sub


    Public AdaJurnalAdjusment_Forex As Boolean
    Sub JurnalAdjusment_Forex(COA As String, TanggalTransaksi As Date)

        AdaJurnalAdjusment_Forex = False

        If COATermasukIDR(COA) = True Then Return

        Dim AdaJurnal As Boolean = CekKeberadaanJurnal_DiTanggalTertentu(COA, JenisJurnal_AdjusmentForex, TanggalTransaksi)
        If AdaJurnal Then Return

        Dim TanggalSebelumTransaksi As Date = TanggalTransaksi.AddDays(-1)
        Dim KodeMataUang As String = AmbilValue_KodeMataUang_BerdasarkanCOA(COA)
        Dim BulanAngka As Integer = AmbilBulanAngka_DariTanggal(TanggalTransaksi)
        Dim SaldoSebelumTransaksi_MUA As Decimal = SaldoAkhirCOA_SampaiTanggalTertentu_MUA(COA, TanggalSebelumTransaksi)
        Dim SaldoSebelumTransaksi_IDR As Int64 = SaldoAkhirCOA_SampaiTanggalTertentu(COA, TanggalSebelumTransaksi)
        Dim TanggalTerakhirSebelumTransaksi As Date = TanggalTerakhirTransaksiCOA(COA, TanggalSebelumTransaksi)
        Dim KursSaatTransaksi As Decimal = AmbilValue_KursTengahBI(KodeMataUang, TanggalTransaksi)
        Dim SaldoDiHariTransaksi_IDR As Int64 = AmbilValue_NilaiMataUang(KodeMataUang, KursSaatTransaksi, SaldoSebelumTransaksi_MUA)
        Dim Selisih As Int64 = SaldoSebelumTransaksi_IDR - SaldoDiHariTransaksi_IDR

        PesanUntukProgrammer(
            "Parameter Jurnal Adjusment Forex : " & Enter2Baris &
            "COA : " & COA & Enter2Baris &
            "Nama Akun : " & AmbilValue_NamaAkun(COA) & Enter2Baris &
            "Kode Mata Uang : " & KodeMataUang & Enter2Baris &
            "Tanggal Sebelum Transaksi: " & TanggalSebelumTransaksi & Enter2Baris &
            "Tanggal Terakhir Sebelum Transaksi: " & TanggalTerakhirSebelumTransaksi & Enter2Baris &
            "Saldo Sebelum Transaksi - MUA : " & SaldoSebelumTransaksi_MUA & Enter2Baris &
            "Saldo Sebelum Transaksi - IDR : " & SaldoSebelumTransaksi_IDR & Enter2Baris &
            "Kurs Saat Transaksi : " & KursSaatTransaksi & Enter2Baris &
            "Saldo Saat Transaksi : " & SaldoDiHariTransaksi_IDR & Enter2Baris &
            "Selisih : " & Selisih & Enter2Baris &
            "")

        ResetValueJurnal()
        SistemPenomoranOtomatis_NomorJV()
        jur_TanggalTransaksi = TanggalTransaksi
        jur_JenisJurnal = JenisJurnal_AdjusmentForex
        jur_KodeLawanTransaksi = KodeLawanTransaksi_Internal
        jur_NamaLawanTransaksi = NamaLawanTransaksi_Internal
        SetMataUangIDR_UntukSimpanJurnalPerBaris()
        jur_Direct = 1

        Select Case Selisih
            Case 0 'Jika tidak ada selisih :
                Return
            Case > 0 'Jika Positif :
                If COATermasukDEBET(COA) = True Then
                    ___jurDebet(KodeTautanCOA_LabaRugiSelisihKurs, Selisih)
                    _______jurKredit(COA, Selisih)
                Else
                    ___jurDebet(COA, Selisih)
                    _______jurKredit(KodeTautanCOA_LabaRugiSelisihKurs, Selisih)
                End If
            Case < 0 'Jika Negatif :
                Selisih = -Selisih
                If COATermasukDEBET(COA) = True Then
                    ___jurDebet(COA, Selisih)
                    _______jurKredit(KodeTautanCOA_LabaRugiSelisihKurs, Selisih)
                Else
                    ___jurDebet(KodeTautanCOA_LabaRugiSelisihKurs, Selisih)
                    _______jurKredit(COA, Selisih)
                End If
        End Select

        AdaJurnalAdjusment_Forex = True

    End Sub


    Public Function AmbilValue_KursTengahBI(KodeMataUang As String, tanggal As DateTime) As Decimal

        Dim KursTengah As Decimal = 0

        If KodeMataUang = KodeMataUang_IDR Then Return 1

        If tanggal > Today Then
            PesanPeringatan("Kurs belum terbit!")
            Return 0
        End If

        BukaDatabasePublic()

        If Not StatusKoneksiDatabasePublic Then
            PesanPeringatan("Sistem tidak dapat mengakses Data Kurs" & Enter2Baris & teks_SilakanCobaLagi_Internet)
            Return 0
        End If

        PerintahSQL(KodeMataUang, tanggal)
        If drPublic.HasRows Then
            KursTengah = drPublic.Item("Kurs_Tengah")
        Else
            tanggal = tanggal.AddDays(-1)
            PerintahSQL(KodeMataUang, tanggal)
            If drPublic.HasRows Then
                KursTengah = drPublic.Item("Kurs_Tengah")
            Else
                tanggal = tanggal.AddDays(-1)
                PerintahSQL(KodeMataUang, tanggal)
                If drPublic.HasRows Then
                    KursTengah = drPublic.Item("Kurs_Tengah")
                Else
                    tanggal = tanggal.AddDays(-1)
                    PerintahSQL(KodeMataUang, tanggal)
                    If drPublic.HasRows Then
                        KursTengah = drPublic.Item("Kurs_Tengah")
                    Else
                        tanggal = tanggal.AddDays(-1)
                        PerintahSQL(KodeMataUang, tanggal)
                        If drPublic.HasRows Then
                            KursTengah = drPublic.Item("Kurs_Tengah")
                        Else
                            tanggal = tanggal.AddDays(-1)
                            PerintahSQL(KodeMataUang, tanggal)
                            If drPublic.HasRows Then
                                KursTengah = drPublic.Item("Kurs_Tengah")
                            Else
                                tanggal = tanggal.AddDays(-1)
                                PerintahSQL(KodeMataUang, tanggal)
                                If drPublic.HasRows Then
                                    KursTengah = drPublic.Item("Kurs_Tengah")
                                Else
                                    tanggal = tanggal.AddDays(-1)
                                    PerintahSQL(KodeMataUang, tanggal)
                                    If drPublic.HasRows Then
                                        KursTengah = drPublic.Item("Kurs_Tengah")
                                    Else
                                        tanggal = tanggal.AddDays(-1)
                                        PerintahSQL(KodeMataUang, tanggal)
                                        If drPublic.HasRows Then
                                            KursTengah = drPublic.Item("Kurs_Tengah")
                                        Else
                                            tanggal = tanggal.AddDays(-1)
                                            PerintahSQL(KodeMataUang, tanggal)
                                            If drPublic.HasRows Then
                                                KursTengah = drPublic.Item("Kurs_Tengah")
                                            Else
                                                tanggal = tanggal.AddDays(-1)
                                                PerintahSQL(KodeMataUang, tanggal)
                                                If drPublic.HasRows Then
                                                    KursTengah = drPublic.Item("Kurs_Tengah")
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        TutupDatabasePublic()

        Return KursTengah

    End Function
    Sub PerintahSQL(KodeMataUang, Tanggal)
        drPublic.Close()
        Dim QueryString As String =
            " SELECT * FROM tbl_kurs " &
            " WHERE Tanggal = '" & TanggalFormatSimpan(Tanggal) & "' " &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' "
        cmdPublic = New MySqlCommand(QueryString, KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        drPublic.Read()
    End Sub


    Sub UpdateDataKursAkhirBulan()

        Dim QueryUpdate As String = Kosongan

        BukaDatabasePublic()

        If Not StatusKoneksiDatabasePublic Then
            PesanPeringatan("Sistem tidak dapat mengakses Data Kurs" & Enter2Baris & teks_SilakanCobaLagi_Internet)
            Return
        End If

        cmdPublic = New MySqlCommand(" SELECT * FROM tbl_kursakhirbulan " &
                                     " WHERE Tahun = '" & TahunBukuAktif & "' ORDER BY ID ", KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        Do While drPublic.Read
            QueryUpdate &=
                " UPDATE tbl_kursakhirbulan SET " &
                " Akhir_Tahun_Lalu      = '" & DesimalFormatSimpan(drPublic.Item("Akhir_Tahun_Lalu")) & "', " &
                " Januari               = '" & DesimalFormatSimpan(drPublic.Item("Januari")) & "', " &
                " Februari              = '" & DesimalFormatSimpan(drPublic.Item("Februari")) & "', " &
                " Maret                 = '" & DesimalFormatSimpan(drPublic.Item("Maret")) & "', " &
                " April                 = '" & DesimalFormatSimpan(drPublic.Item("April")) & "', " &
                " Mei                   = '" & DesimalFormatSimpan(drPublic.Item("Mei")) & "', " &
                " Juni                  = '" & DesimalFormatSimpan(drPublic.Item("Juni")) & "', " &
                " Juli                  = '" & DesimalFormatSimpan(drPublic.Item("Juli")) & "', " &
                " Agustus               = '" & DesimalFormatSimpan(drPublic.Item("Agustus")) & "', " &
                " September             = '" & DesimalFormatSimpan(drPublic.Item("September")) & "', " &
                " Oktober               = '" & DesimalFormatSimpan(drPublic.Item("Oktober")) & "', " &
                " Nopember              = '" & DesimalFormatSimpan(drPublic.Item("Nopember")) & "', " &
                " Desember              = '" & DesimalFormatSimpan(drPublic.Item("Desember")) & "'  " &
                " WHERE Kode_Mata_Uang  = '" & drPublic.Item("Kode_Mata_Uang") & "' ; " & vbCrLf
        Loop

        TutupDatabasePublic()

        BukaDatabaseTransaksi_MySQL()
        cmdMySQL = New MySqlCommand(QueryUpdate, KoneksiDatabaseTransaksi_MySQL)
        Try
            cmdMySQL.ExecuteNonQuery()
            StatusKoneksiDatabaseTransaksi_MySQL = True
        Catch ex As Exception
            StatusKoneksiDatabaseTransaksi_MySQL = False
        End Try
        TutupDatabaseTransaksi_MySQL()

    End Sub

    Sub KontenCombo_KodeMataUang_Public(cmb_KodeMataUang As ComboBox)
        cmb_KodeMataUang.Items.Clear()
        cmb_KodeMataUang.Items.Add(KodeMataUang_IDR)
        cmb_KodeMataUang.Items.Add(KodeMataUang_USD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_AUD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_JPY)
        cmb_KodeMataUang.Items.Add(KodeMataUang_CNY)
        cmb_KodeMataUang.Items.Add(KodeMataUang_EUR)
        cmb_KodeMataUang.Items.Add(KodeMataUang_SGD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_GBP)
    End Sub

    Sub KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang As ComboBox)
        cmb_KodeMataUang.Items.Clear()
        cmb_KodeMataUang.Items.Add(KodeMataUang_USD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_AUD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_JPY)
        cmb_KodeMataUang.Items.Add(KodeMataUang_CNY)
        cmb_KodeMataUang.Items.Add(KodeMataUang_EUR)
        cmb_KodeMataUang.Items.Add(KodeMataUang_SGD)
        cmb_KodeMataUang.Items.Add(KodeMataUang_GBP)
    End Sub


    Public Function FormatUlangInt64(Value As Int64) As Int64
        Return Convert.ToInt64(Value)
    End Function


    Public Sub FormatUlangAngkaKeBilanganBulat(ByRef Angka)
        If Angka = Nothing Then Angka = 0
        Convert.ToInt64(Angka)
        Angka = Convert.ToInt64(Angka)
    End Sub


    Public Sub FormatUlangAngkaKeBilanganDesimal(ByRef Angka)
        If Angka = Nothing Then Angka = 0
        Convert.ToDecimal(Angka)
        Angka = Convert.ToDecimal(Angka)
    End Sub

    Function AmbilValue_PPhTerutangBerdasarkanInvoicePembelian(NomorInvoice As String, JumlahBayar As Int64) As Int64
        Dim cmdInvoice As OdbcCommand
        Dim drInvoice As OdbcDataReader
        Dim JumlahHutangUsaha As Int64
        Dim PPhTerutang As Int64
        BukaDatabaseTransaksi_Kondisional()
        cmdInvoice = New OdbcCommand(" SELECT Jumlah_Hutang_Usaha, PPh_Terutang FROM tbl_Pembelian_Invoice " &
                                     " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drInvoice = cmdInvoice.ExecuteReader()
        drInvoice.Read()
        If drInvoice.HasRows Then
            JumlahHutangUsaha = drInvoice.Item("Jumlah_Hutang_Usaha")
            PPhTerutang = drInvoice.Item("PPh_Terutang")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return PPhTerutang * (JumlahBayar / JumlahHutangUsaha)
    End Function

    Function AmbilValue_PPhDitanggungBerdasarkanInvoicePembelian(NomorInvoice As String, JumlahBayar As Int64) As Int64
        Dim cmdInvoice As OdbcCommand
        Dim drInvoice As OdbcDataReader
        Dim JumlahHutangUsaha As Int64
        Dim PPhDitanggung As Int64
        BukaDatabaseTransaksi_Kondisional()
        cmdInvoice = New OdbcCommand(" SELECT Jumlah_Hutang_Usaha, PPh_Ditanggung FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drInvoice = cmdInvoice.ExecuteReader()
        drInvoice.Read()
        If drInvoice.HasRows Then
            JumlahHutangUsaha = drInvoice.Item("Jumlah_Hutang_Usaha")
            PPhDitanggung = drInvoice.Item("PPh_Ditanggung")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return PPhDitanggung * (JumlahBayar / JumlahHutangUsaha)
    End Function

    Function AmbilValue_PPhDipotongBerdasarkanInvoicePembelian(NomorInvoice As String, JumlahBayar As Int64) As Int64
        Dim cmdInvoice As OdbcCommand
        Dim drInvoice As OdbcDataReader
        Dim JumlahHutangUsaha As Int64
        Dim PPhDipotong As Int64
        BukaDatabaseTransaksi_Kondisional()
        cmdInvoice = New OdbcCommand(" SELECT Jumlah_Hutang_Usaha, PPh_Dipotong FROM tbl_Pembelian_Invoice WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        drInvoice = cmdInvoice.ExecuteReader()
        drInvoice.Read()
        If drInvoice.HasRows Then
            JumlahHutangUsaha = drInvoice.Item("Jumlah_Hutang_Usaha")
            PPhDipotong = drInvoice.Item("PPh_Dipotong")
        End If
        TutupDatabaseTransaksi_Kondisional()
        Return PPhDipotong * (JumlahBayar / JumlahHutangUsaha)
    End Function

    Sub KontenCombo_KategoriPenerimaan_Public(cmb_Kategori As ComboBox)
        cmb_Kategori.Items.Clear()
        cmb_Kategori.Items.Add(Kategori_PencairanPiutang)
        cmb_Kategori.Items.Add(Kategori_PenerimaanTunai)
        cmb_Kategori.Items.Add(Kategori_Investasi)
        cmb_Kategori.Items.Add(Kategori_Pengembalian)
        If LevelUserAktif = LevelUser_99_AppDeveloper Then cmb_Kategori.Items.Add(Kategori_Try)
    End Sub

    Sub KontenCombo_PeruntukanPencairanPiutang_Public(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangUsaha_NonAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangUsaha_Afiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangUsaha_Ekspor)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangDividen)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PencairanPiutangLainnya)
    End Sub

    Sub KontenCombo_PeruntukanPenerimaanTunai_Public(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_InvoiceTunai)
        cmb_Peruntukan.Items.Add(Peruntukan_PenjualanEceranHarian)
        cmb_Peruntukan.Items.Add(Peruntukan_DepositOperasional)
        cmb_Peruntukan.Items.Add(Peruntukan_BankGaransi)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangOngkosKirimPenjualan)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangKoperasiKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangSerikat)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangLancarLainnya)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangLeasing)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangBank)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangLembagaKeuanganNonBank)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_HutangAfiliasi)
        'cmb_Peruntukan.Items.Add(Peruntukan_UangMukaPenjualan)
        'cmb_Peruntukan.Items.Add(Peruntukan_Termin)
        'cmb_Peruntukan.Items.Add(Peruntukan_Pelunasan)
        'cmb_Peruntukan.Items.Add(Peruntukan_UangMukaJangkaPanjang)
    End Sub

    Sub KontenCombo_PeruntukanInvestasi_Public(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiModal)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiDeposito)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiSuratBerharga)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiLogamMulia)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiPadaPerusahaanAnak)
    End Sub

    Sub KontenCombo_PeruntukanPengembalian_Public(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_LebihBayarPPhBadan)
        cmb_Peruntukan.Items.Add(Peruntukan_KelebihanPenerimaanPembayaranHutang)
    End Sub


    Sub KontenCombo_KategoriPengeluaran_Public(cmb_Kategori As ComboBox)
        cmb_Kategori.Items.Clear()
        cmb_Kategori.Items.Add(Kategori_PembayaranHutang)
        cmb_Kategori.Items.Add(Kategori_PengeluaranTunai)
        cmb_Kategori.Items.Add(Kategori_Pemindahbukuan)
        cmb_Kategori.Items.Add(Kategori_Investasi)
        If LevelUserAktif = LevelUser_99_AppDeveloper Then cmb_Kategori.Items.Add(Kategori_Try)
    End Sub

    Sub KontenCombo_PeruntukanPembayaranHutang_Pulic(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangUsaha_NonAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangUsaha_Afiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBiaya)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangGaji)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBPJSKesehatan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBPJSKetenagakerjaan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangKoperasiKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangSerikat)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangLancarLainnya)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangLeasing)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangBank)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangPajak)
        cmb_Peruntukan.Items.Add(Peruntukan_PembayaranHutangDividen)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPengeluaranTunai_Pulic(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_InvoiceTunai)
        'cmb_Peruntukan.Items.Add(Peruntukan_UangMukaPembelian)
        'cmb_Peruntukan.Items.Add(Peruntukan_Termin)
        'cmb_Peruntukan.Items.Add(Peruntukan_Pelunasan)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangPemegangSaham)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangKaryawan)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangPihakKetiga)
        cmb_Peruntukan.Items.Add(Peruntukan_PiutangAfiliasi)
        cmb_Peruntukan.Items.Add(Peruntukan_DepositOperasional)
        cmb_Peruntukan.Items.Add(Peruntukan_BankGaransi)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub

    Sub KontenCombo_PeruntukanPemindahbukuan_Pulic(cmb_Peruntukan As ComboBox)
        KontenComboSaranaPembayaran_Public_WPF(cmb_Peruntukan, KodeMataUang_Semua)
    End Sub

    Sub KontenCombo_PeruntukanInvestasi_Pulic(cmb_Peruntukan As ComboBox)
        cmb_Peruntukan.Items.Clear()
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiDeposito)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiSuratBerharga)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiLogamMulia)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiPadaPerusahaanAnak)
        cmb_Peruntukan.Items.Add(Peruntukan_InvestasiGoodWill)
        cmb_Peruntukan.SelectedValue = Kosongan
    End Sub


    Sub PembatasanHakAkses(Jendela As Window)
        win_KunciAkses = New wpfWin_KunciAkses
        win_KunciAkses.ShowDialog()
        If Not win_KunciAkses.BolehMasuk Then Jendela.Close()
    End Sub


    Sub PosisikanWindowKeTengahLayar(Jendela As Window)
        Jendela.Left = (SystemParameters.PrimaryScreenWidth - Jendela.ActualWidth) / 2
        Jendela.Top = (SystemParameters.PrimaryScreenHeight - Jendela.ActualHeight) / 2
    End Sub



End Module

