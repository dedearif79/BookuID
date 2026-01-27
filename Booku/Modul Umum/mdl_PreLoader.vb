Option Explicit On
Option Strict On

Imports System.Threading.Tasks
Imports System.Windows
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

''' <summary>
''' Module untuk pre-loading komponen saat startup.
''' Tujuan: Mengurangi "cold start" delay saat pertama kali membuka tab/menu.
'''
''' Yang di-preload:
''' 1. JIT Compile - Beberapa UserControl yang sering digunakan
''' 2. Assembly Loading - DLL yang diperlukan (MySql.Data, ClosedXML, dll)
''' 3. XAML Resources - Akses style untuk memastikan resources ter-load
''' 4. Database Connection Pool - Warmup koneksi database
''' </summary>
Module mdl_PreLoader

    ''' <summary>
    ''' Flag untuk menandai pre-loading sudah selesai
    ''' </summary>
    Public PreLoadingSelesai As Boolean = False

    ''' <summary>
    ''' Callback untuk update progress (opsional)
    ''' </summary>
    Public PreLoadProgressCallback As Action(Of String) = Nothing

    ''' <summary>
    ''' Jalankan pre-loading secara async.
    ''' Panggil dari wpfWin_StartUp setelah koneksi database berhasil.
    ''' </summary>
    Public Async Function JalankanPreLoadAsync() As Task
        Try
            ' 1. Pre-load Assemblies (memaksa .NET me-load DLL)
            UpdateProgress("Memuat komponen...")
            Await Task.Run(Sub() PreLoadAssemblies())

            ' 2. Pre-load XAML Resources (akses beberapa style)
            UpdateProgress("Memuat tampilan...")
            PreLoadXamlResources()

            ' 3. Pre-load UserControl (trigger JIT compilation)
            UpdateProgress("Menyiapkan modul...")
            Await Task.Run(Sub() PreLoadUserControls())

            ' 4. Pre-warm Database Connection Pool
            UpdateProgress("Menyiapkan koneksi...")
            Await Task.Run(Sub() PreWarmDatabaseConnection())

            PreLoadingSelesai = True
            UpdateProgress("Siap.")

        Catch ex As Exception
            ' Jangan gagalkan startup jika pre-loading error
            ' Hanya log dan lanjutkan
            Debug.WriteLine($"[PreLoader] Error: {ex.Message}")
            PreLoadingSelesai = True
        End Try
    End Function

    ''' <summary>
    ''' Pre-load assemblies dengan mengakses tipe dari masing-masing DLL.
    ''' Ini memaksa CLR untuk me-load assembly ke memory.
    ''' </summary>
    Private Sub PreLoadAssemblies()
        Try
            ' MySql.Data.dll
            Dim mysqlType = GetType(MySqlConnection)
            Debug.WriteLine($"[PreLoader] Loaded: {mysqlType.Assembly.GetName().Name}")

            ' System.Data.Odbc
            Dim odbcType = GetType(OdbcConnection)
            Debug.WriteLine($"[PreLoader] Loaded: {odbcType.Assembly.GetName().Name}")

            ' ClosedXML (jika digunakan)
            Try
                Dim closedXmlType = Type.GetType("ClosedXML.Excel.XLWorkbook, ClosedXML")
                If closedXmlType IsNot Nothing Then
                    Debug.WriteLine($"[PreLoader] Loaded: ClosedXML")
                End If
            Catch
                ' Ignore jika ClosedXML tidak ada
            End Try

            ' Newtonsoft.Json (jika digunakan)
            Try
                Dim jsonType = Type.GetType("Newtonsoft.Json.JsonConvert, Newtonsoft.Json")
                If jsonType IsNot Nothing Then
                    Debug.WriteLine($"[PreLoader] Loaded: Newtonsoft.Json")
                End If
            Catch
                ' Ignore jika tidak ada
            End Try

        Catch ex As Exception
            Debug.WriteLine($"[PreLoader] Assembly error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Pre-load XAML resources dengan mengakses beberapa style.
    ''' Ini memaksa WPF untuk parse dan compile XAML resources.
    ''' </summary>
    Private Sub PreLoadXamlResources()
        Try
            ' Akses dari UI thread
            Application.Current.Dispatcher.Invoke(
                Sub()
                    ' Akses beberapa style populer untuk trigger XAML parsing
                    Dim resources = Application.Current.Resources

                    ' Style untuk DataGrid (paling kompleks, paling lama di-parse)
                    Dim styleDataGrid = TryCast(resources("styleDataGridFormHalaman"), Style)
                    Dim styleButton = TryCast(resources("styleButtonFormHalaman"), Style)
                    Dim styleTextBox = TryCast(resources("styleTextBoxFormDialog"), Style)
                    Dim styleComboBox = TryCast(resources("styleComboBoxFormDialog"), Style)
                    Dim stylePanel = TryCast(resources("stylePanelFormHalaman"), Style)

                    Debug.WriteLine($"[PreLoader] XAML Resources loaded")
                End Sub)

        Catch ex As Exception
            Debug.WriteLine($"[PreLoader] XAML error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Pre-load beberapa UserControl yang sering digunakan.
    ''' Cukup akses instance-nya untuk trigger JIT compilation.
    ''' HARUS dijalankan di UI thread karena UserControl adalah WPF.
    ''' </summary>
    Private Sub PreLoadUserControls()
        Try
            ' Jalankan di UI thread
            Application.Current.Dispatcher.Invoke(
                Sub()
                    ' Akses beberapa UserControl populer
                    ' Ini akan trigger:
                    ' - JIT compilation untuk class UserControl
                    ' - Constructor execution (InitializeComponent, Buat_DataTabelUtama)
                    ' - XAML parsing untuk UserControl tersebut

                    ' Data Master (sering dibuka pertama kali)
                    Dim temp1 = usc_DataCOA
                    Dim temp2 = usc_DataLawanTransaksi
                    Dim temp3 = usc_DataKaryawan

                    ' Buku Besar (kompleks, banyak kolom)
                    Dim temp4 = usc_BukuBesar

                    ' Jurnal (sering digunakan)
                    Dim temp5 = usc_JurnalUmum

                    Debug.WriteLine("[PreLoader] UserControls pre-loaded")
                End Sub)

        Catch ex As Exception
            Debug.WriteLine($"[PreLoader] UserControl error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Pre-warm database connection pool.
    ''' Membuka dan menutup koneksi untuk inisialisasi pool.
    ''' </summary>
    Private Sub PreWarmDatabaseConnection()
        Try
            ' Warm up koneksi General (ODBC)
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabaseGeneral Then
                ' Jalankan query sederhana untuk warm up
                Dim cmd As New OdbcCommand("SELECT 1", KoneksiDatabaseGeneral)
                Try
                    cmd.ExecuteScalar()
                Catch
                End Try
            End If
            AksesDatabase_General(Tutup)

            ' Warm up koneksi Transaksi
            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabaseTransaksi Then
                Dim cmd As New OdbcCommand("SELECT 1", KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteScalar()
                Catch
                End Try
            End If
            AksesDatabase_Transaksi(Tutup)

            Debug.WriteLine("[PreLoader] Database connection pool warmed up")

        Catch ex As Exception
            Debug.WriteLine($"[PreLoader] Database error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Update progress callback jika ada
    ''' </summary>
    Private Sub UpdateProgress(message As String)
        If PreLoadProgressCallback IsNot Nothing Then
            Application.Current.Dispatcher.Invoke(Sub() PreLoadProgressCallback(message))
        End If
        Debug.WriteLine($"[PreLoader] {message}")
    End Sub

End Module
