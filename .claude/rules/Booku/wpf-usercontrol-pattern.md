# WPF UserControl Pattern

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku** (folder `BookuID/Booku/`)

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

## 4a. Tipe-Tipe Kolom DataGrid

### Ringkasan Tipe Kolom

| Tipe Kolom | Deklarasi | DataTable | Fungsi Tambah |
|------------|-----------|-----------|---------------|
| **String** | `DataGridTextColumn` | `.Add("Nama")` | `TambahkanKolomTextBoxDataGrid_WPF` |
| **Angka** | `DataGridTextColumn` | `.Add("Nama", GetType(...))` | `TambahkanKolomTextBoxDataGrid_WPF` |
| **CheckBox** | `DataGridCheckBoxColumn` | `.Add("Nama")` | `TambahkanKolomCheckBoxDataGrid_WPF` |
| **Button** | Tidak perlu | Tidak perlu | `TambahkanKolomButtonDataGrid_WPF` |

### a. Kolom String (Default)

```vb
' Deklarasi
Dim Kolom_Keterangan As New DataGridTextColumn

' DataTable - tanpa parameter tipe (default String)
datatabelUtama.Columns.Add("Keterangan")

' Tambah ke DataGrid
TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Keterangan, "Keterangan", "Keterangan", 200, FormatString, KiriTengah, KunciUrut, Terlihat)
```

### b. Kolom Angka

```vb
' Deklarasi
Dim Kolom_Jumlah As New DataGridTextColumn

' DataTable - dengan GetType untuk tipe numerik
datatabelUtama.Columns.Add("Jumlah", GetType(Int64))
datatabelUtama.Columns.Add("Saldo", GetType(Decimal))

' Tambah ke DataGrid
TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Jumlah, "Jumlah", "Jumlah", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
```

### c. Kolom CheckBox

Contoh: `wpfUsc_BukuPengawasanPiutangPihakKetiga.xaml.vb`

```vb
' Deklarasi - menggunakan DataGridCheckBoxColumn
Dim Jadwal_Ceklis_ As New DataGridCheckBoxColumn

' DataTable - tetap string (tanpa GetType)
datatabelJadwalAngsuran.Columns.Add("Jadwal_Ceklis_")

' Tambah ke DataGrid - parameter terakhir: IsReadOnly (Boolean)
TambahkanKolomCheckBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Ceklis_, "Jadwal_Ceklis_", "Chk", 45, FormatAngka, TengahTengah, KunciUrut, Terlihat, False)
```

**Parameter `TambahkanKolomCheckBoxDataGrid_WPF`:**
1. `datagrid` - DataGrid target
2. `kolom` - Variabel DataGridCheckBoxColumn
3. `binding` - Nama kolom di DataTable
4. `header` - Judul kolom
5. `lebar` - Lebar kolom
6. `format` - Format (FormatAngka/FormatString)
7. `alignment` - Alignment (TengahTengah, dll)
8. `kunci` - KunciUrut
9. `visibility` - Terlihat/Tersembunyi
10. `isReadOnly` - True/False

### d. Kolom Button

Contoh: `wpfUsc_Adjusment_Forex.xaml.vb`

```vb
' Tidak perlu deklarasi variabel kolom
' Tidak perlu kolom di DataTable

' Tambah ke DataGrid - dengan Tag (identifier) dan AddressOf handler
TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Januari_", "Januari", 72, 1, AddressOf btn_TombolTabel_Click)
TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Februari_", "Februari", 72, 2, AddressOf btn_TombolTabel_Click)
TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Maret_", "Maret", 72, 3, AddressOf btn_TombolTabel_Click)
```

**Parameter `TambahkanKolomButtonDataGrid_WPF`:**
1. `datagrid` - DataGrid target
2. `binding` - Nama binding (untuk identifikasi)
3. `header` - Judul kolom / teks button
4. `lebar` - Lebar kolom
5. `tag` - Tag identifier (Integer) untuk membedakan tombol di handler
6. `handler` - AddressOf event handler

**Contoh Handler untuk Kolom Button:**
```vb
Private Sub btn_TombolTabel_Click(sender As Object, e As RoutedEventArgs)
    Dim btn As Button = TryCast(sender, Button)
    If btn Is Nothing Then Return

    Dim tagBulan As Integer = CInt(btn.Tag)

    ' Lakukan aksi berdasarkan tag
    Select Case tagBulan
        Case 1 : ' Januari
        Case 2 : ' Februari
        ' ... dst
    End Select
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

## 12. Pattern Tipe Input Field

> **Dokumentasi lengkap:** Lihat file `.claude/rules/Booku/wpf-window-pattern.md` section 1a.

Pattern ini berlaku untuk input field di UserControl (filter, form inline, dll).

### Ringkasan Tipe Input Field

| Tipe Input | Style XAML | Code-Behind | Contoh |
|------------|------------|-------------|--------|
| **Teks String** | `styleTextBoxFormDialog` | Langsung `.Text` | Kode, Nama, Filter |
| **Angka Ribuan** | `styleTextBoxFormDialogAngkaSeparatePlus` | `AmbilAngka()` | Jumlah, Harga |
| **Keterangan/Uraian** | `styleRichTextBoxFormDialog` | Helper functions | Keterangan, Catatan |

### a. Input Angka dengan Separator Ribuan

> **PENTING:** Gunakan `styleTextBoxFormDialogAngkaSeparatePlus` - style ini **otomatis** menangani formatting separator ribuan. **JANGAN** panggil fungsi formatter manual.

```xml
<TextBox Style="{StaticResource styleTextBoxFormDialogAngkaSeparatePlus}" x:Name="txt_Jumlah"/>
```

```vb
' Mengisi nilai - cukup .ToString(), style akan auto-format
txt_Jumlah.Text = JumlahDariDatabase.ToString()

' Mengambil nilai
Jumlah = AmbilAngka(txt_Jumlah.Text)
```

### b. Input Keterangan/Uraian (RichTextBox)

> **PENTING:** Gunakan `RichTextBox` dengan `styleRichTextBoxFormDialog`, bukan `TextBox` dengan `TextWrapping="Wrap"`.

```xml
<!-- Label di row terpisah dengan titik dua (:) -->
<TextBlock Style="{StaticResource styleTextBlockFormDialog}" Text="Keterangan :"/>

<!-- RichTextBox dengan Grid.ColumnSpan="2" -->
<RichTextBox Style="{StaticResource styleRichTextBoxFormDialog}" x:Name="txt_Keterangan" Grid.ColumnSpan="2"/>
```

**Helper Functions:**

| Fungsi | Tujuan |
|--------|--------|
| `IsiValueElemenRichTextBox(rtb, nilai)` | Mengisi nilai ke RichTextBox |
| `KosongkanValueElemenRichTextBox(rtb)` | Mengosongkan RichTextBox |
| `IsiValueVariabelRichTextBox(rtb)` | Mengambil nilai dari RichTextBox |

```vb
' Mengisi nilai
IsiValueElemenRichTextBox(txt_Keterangan, KeteranganDariDatabase)

' Mengosongkan
KosongkanValueElemenRichTextBox(txt_Keterangan)

' Mengambil nilai
Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
```

## 13. WPF Host Pattern

> **Dokumentasi lengkap:** Lihat file `.claude/rules/Booku/wpf-host-pattern.md`

Host adalah class wrapper untuk UserControl dengan konfigurasi spesifik. Satu UserControl dapat memiliki banyak varian Host.

### Ringkasan Aturan

| Aturan | Penjelasan |
|--------|------------|
| **1 file = 1 UserControl** | `wpfHost_XXX.vb` hanya untuk 1 `wpfUsc_XXX` |
| **Banyak varian** | 1 file host bisa berisi banyak class varian |
| **Lokasi file** | Folder yang sama dengan UserControl |
| **Inherits** | `ContentControl` |

### Contoh Singkat

```vb
Public Class wpfHost_BukuPembelian_Lokal
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pembelian"
        Inisialisasi()
        Me.Content = usc_BukuPembelian_Lokal
    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian With {
            .AsalPembelian = AsalPembelian_Lokal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPembelian_Lokal.RefreshTampilanData()
    End Sub
End Class
```
