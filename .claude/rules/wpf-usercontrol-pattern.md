# WPF UserControl Pattern

Pattern standar untuk membuat WPF UserControl dengan DataGrid, berdasarkan analisis `wpfUsc_BukuBesar`.

## 1. Struktur UI (XAML)

### Layout Hierarki Standar

```
DockPanel (pnl_Halaman) - Root container
├── StackPanel (Header)
│   ├── Filter Data (ComboBox, TextBox)
│   ├── Tombol Form (Refresh, CRUD, Export)
│   └── Info Tambahan (GroupBox) - opsional
├── StackPanel (Footer) - opsional
└── DockPanel (pnl_Konten)
    └── DataGrid (datagridUtama)
```

### Contoh XAML Skeleton

```xml
<UserControl x:Class="wpfUsc_NamaModul"
             Style="{StaticResource styleUserControlFormHalaman}">
    <UserControl.Resources>
        <ResourceDictionary Source="/WPF/Styles/StyleAplikasi.xaml"/>
    </UserControl.Resources>

    <DockPanel x:Name="pnl_Halaman" Style="{StaticResource stylePanelFormHalaman}">
        <!-- HEADER -->
        <StackPanel DockPanel.Dock="Top">
            <Grid>
                <!-- Filter, Tombol, Info -->
            </Grid>
        </StackPanel>

        <!-- FOOTER (opsional) -->
        <StackPanel DockPanel.Dock="Bottom"/>

        <!-- KONTEN -->
        <DockPanel x:Name="pnl_Konten">
            <StackPanel x:Name="pnl_DataGridUtama">
                <DataGrid x:Name="datagridUtama"/>
            </StackPanel>
        </DockPanel>
    </DockPanel>
</UserControl>
```

## 2. Deklarasi Variabel Standar

### a. Status Form

```vb
' === STATUS FORM ===
Public StatusAktif As Boolean = False           ' Flag form aktif (Public untuk parent)
Private SudahDimuat As Boolean = False          ' Flag load pertama kali
Private EksekusiTampilanData As Boolean = False ' Guard clause untuk TampilkanData
```

| Variabel | Scope | Tujuan |
|----------|-------|--------|
| `StatusAktif` | Public | Menandai form aktif, bisa diakses parent window |
| `SudahDimuat` | Private | Mencegah event Loaded dieksekusi lebih dari sekali |
| `EksekusiTampilanData` | Private | Mencegah TampilkanData() dipanggil saat ComboBox sedang diisi |

### b. DataTable & DataView

```vb
' === DATATABLE & DATAVIEW ===
Public datatabelUtama As DataTable
Public dataviewUtama As DataView
Public rowviewUtama As DataRowView
Public BarisTerseleksi As Integer
Public JumlahBaris As Integer
```

### c. Kolom DataGrid

```vb
' === KOLOM DATAGRID ===
Dim Kolom_NomorUrut As New DataGridTextColumn
Dim Kolom_Tanggal As New DataGridTextColumn
Dim Kolom_Keterangan As New DataGridTextColumn
' ... dst sesuai kebutuhan
```

### d. Variabel '_Terseleksi'

Naming convention: `[NamaKolom]_Terseleksi`

```vb
' === VARIABEL TERSELEKSI ===
Dim NomorUrut_Terseleksi As Integer
Dim NomorJV_Terseleksi As Int64
Dim KodeLawanTransaksi_Terseleksi As String
Dim NamaLawanTransaksi_Terseleksi As String
' ... dst sesuai kebutuhan
```

## 3. Pattern Constructor

```vb
Sub New()
    InitializeComponent()
    Buat_DataTabelUtama()

    ' Set read-only properties jika diperlukan
    txt_Kode.IsReadOnly = True
    cmb_Filter.IsReadOnly = True
End Sub
```

## 4. Pattern Buat_DataTabelUtama

```vb
Sub Buat_DataTabelUtama()
    datatabelUtama = New DataTable

    ' Definisikan kolom dengan tipe data
    datatabelUtama.Columns.Add("Nomor_Urut")
    datatabelUtama.Columns.Add("Tanggal")
    datatabelUtama.Columns.Add("Keterangan")
    datatabelUtama.Columns.Add("Jumlah", GetType(Decimal))
    datatabelUtama.Columns.Add("Saldo", GetType(Int64))

    ' Setup styling dan binding
    StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

    ' Tambahkan kolom ke DataGrid
    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_NomorUrut, "Nomor_Urut", "No.", 45, ...)
    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Tanggal, "Tanggal", "Tanggal", 80, ...)
    ' ... dst
End Sub
```

## 5. Pattern Event Loaded

```vb
Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
    ' GUARD: Cegah eksekusi berulang
    If SudahDimuat Then Return

    StatusAktif = True

    ' Inisialisasi tampilan
    LogikaTampilan()
    RefreshTampilanData()

    ' Set flag setelah selesai
    SudahDimuat = True
End Sub
```

## 6. Pattern RefreshTampilanData

```vb
Sub RefreshTampilanData()
    ' GUARD: Matikan eksekusi TampilkanData saat isi ComboBox
    EksekusiTampilanData = False

    ' Isi ComboBox filter
    KontenCombo_Filter1()
    KontenCombo_Filter2()

    ' GUARD: Aktifkan kembali
    EksekusiTampilanData = True

    ' Tampilkan data
    TampilkanData()
End Sub
```

## 7. Pattern TampilkanData

```vb
Sub TampilkanData()
    ' GUARD: Jangan eksekusi jika sedang isi ComboBox
    If Not EksekusiTampilanData Then Return

    ' Disable UI saat loading
    KetersediaanMenuHalaman(pnl_Halaman, False)

    ' Reset counter
    NomorUrut = 0

    ' Query database
    AksesDatabase_Transaksi(Buka)
    cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
    dr = cmd.ExecuteReader

    Do While dr.Read
        ' Extract nilai dari dr
        ' Hitung saldo running (jika ada)
        ' Tambah baris
        TambahBaris()
    Loop

    AksesDatabase_Transaksi(Tutup)

    ' Clear selection
    BersihkanSeleksi()

    ' Enable UI
    KetersediaanMenuHalaman(pnl_Halaman, True)
End Sub
```

## 8. Pattern SelectedCellsChanged

```vb
Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

    ' Ambil index baris
    BarisTerseleksi = datagridUtama.SelectedIndex
    If BarisTerseleksi < 0 Then Return

    ' Cast ke DataRowView
    rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
    If rowviewUtama Is Nothing Then Return

    ' === ISI VARIABEL _TERSELEKSI ===
    NomorUrut_Terseleksi = rowviewUtama("Nomor_Urut")
    NomorJV_Terseleksi = rowviewUtama("Nomor_JV")
    KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")
    ' ... dst

    ' === UPDATE BUTTON STATE ===
    If KondisiTertentu Then
        btn_Edit.IsEnabled = True
        btn_Hapus.IsEnabled = True
    Else
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End If
End Sub
```

## 9. Pattern BersihkanSeleksi

```vb
Sub BersihkanSeleksi()
    datagridUtama.SelectedIndex = -1
    BarisTerseleksi = -1

    ' Reset variabel _Terseleksi
    NomorUrut_Terseleksi = 0
    NomorJV_Terseleksi = 0
    KodeLawanTransaksi_Terseleksi = Kosongan

    ' Disable tombol
    btn_Edit.IsEnabled = False
    btn_Hapus.IsEnabled = False
    btn_LihatJurnal.IsEnabled = False
End Sub
```

## 10. Pattern ComboBox SelectionChanged

```vb
Private Sub cmb_Filter_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Filter.SelectionChanged
    ' Ambil nilai terpilih
    Pilih_Filter = cmb_Filter.SelectedValue

    ' Khusus jika pilih "Semua"
    If cmb_Filter.SelectedValue = Pilihan_Semua Then
        Pilih_Filter = Pilihan_Semua
    End If

    ' Refresh data dengan filter baru
    TampilkanData()
End Sub
```

## 11. Pattern Button Click Handler

```vb
Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
    ' 1. Buat form input
    win_InputForm = New wpfWin_InputForm
    win_InputForm.ResetForm()
    win_InputForm.FungsiForm = FungsiForm_EDIT

    ' 2. Set properties dengan variabel _Terseleksi
    win_InputForm.NomorJV = NomorJV_Terseleksi
    win_InputForm.KodeLawanTransaksi = KodeLawanTransaksi_Terseleksi

    ' 3. Query dan populate form
    AksesDatabase_Transaksi(Buka)
    ' ... populate form
    AksesDatabase_Transaksi(Tutup)

    ' 4. Show dialog
    win_InputForm.ShowDialog()

    ' 5. Refresh setelah dialog ditutup
    TampilkanData()
End Sub
```

## Ringkasan Alur

```
Constructor: New()
    └── Buat_DataTabelUtama()

Event Loaded:
    └── SudahDimuat check
        └── RefreshTampilanData()
            ├── EksekusiTampilanData = False
            ├── KontenCombo_...()
            ├── EksekusiTampilanData = True
            └── TampilkanData()

Event SelectedCellsChanged:
    └── Isi variabel _Terseleksi
        └── Update button state

Button Click:
    └── Gunakan variabel _Terseleksi
        └── TampilkanData() (refresh)
```
