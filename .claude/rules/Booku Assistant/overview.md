# Booku Assistant

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Assistant** (folder `BookuID/Booku Assistant/`)

## Deskripsi

**Booku Assistant** adalah aplikasi launcher dan utilitas pendukung untuk produk-produk BookuID. Aplikasi ini menyediakan antarmuka terpusat untuk menjalankan berbagai aplikasi dalam ekosistem BookuID.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Assistant |
| **Output** | `Booku Assistant.exe` (WinExe) |
| **Namespace** | `Booku_Assistant` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF dengan UserControl pattern |
| **Entry Point** | `Application.xaml` → `App.Main()` → `wpfWin_StartUp.xaml` |
| **Single Instance** | Ya (Mutex protection) |
| **Single Executable** | Ya (Self-contained, compressed) |

## Struktur File

```
Booku Assistant/
├── Booku Assistant.vbproj        # Project file (dengan Release config)
├── Application.xaml              # Application entry point (merge StyleAplikasi.xaml)
├── Application.xaml.vb           # Application code-behind (Mutex + Single Instance)
├── AssemblyInfo.vb               # Assembly information
├── PUBLISH-RELEASE.bat           # Script build Release
├── wpfWin_StartUp.xaml           # Main window (header + TabControl + ContentControl)
├── wpfWin_StartUp.xaml.vb        # Window controls + set Content tab ke UserControl
│
├── Modul/                        # Folder module
│   └── wpfMdl_ClassUserControl.vb   # Deklarasi public usc_XXX
│
└── UserControls/                 # Folder UserControl
    ├── wpfUsc_Beranda.xaml       # Konten tab Beranda (logo, tagline, versi)
    ├── wpfUsc_Beranda.xaml.vb
    ├── wpfUsc_Aplikasi.xaml      # Konten tab Aplikasi (daftar aplikasi + tombol)
    ├── wpfUsc_Aplikasi.xaml.vb   # Logika launcher (CekKetersediaan, JalankanAplikasi)
    ├── wpfUsc_Pengaturan.xaml    # Konten tab Pengaturan (placeholder)
    └── wpfUsc_Pengaturan.xaml.vb
```

## Arsitektur UserControl

Setiap tab di `wpfWin_StartUp` dipisahkan menjadi UserControl terpisah untuk kemudahan pengelolaan:

```
wpfWin_StartUp
├── Header (gradient + logo + window controls)
└── TabControl
    ├── Tab Beranda    → ContentControl → usc_Beranda (wpfUsc_Beranda)
    ├── Tab Aplikasi   → ContentControl → usc_Aplikasi (wpfUsc_Aplikasi)
    └── Tab Pengaturan → ContentControl → usc_Pengaturan (wpfUsc_Pengaturan)
```

### Deklarasi UserControl (wpfMdl_ClassUserControl.vb)

```vb
Module wpfMdl_ClassUserControl

    Public usc_Beranda As New wpfUsc_Beranda
    Public usc_Aplikasi As New wpfUsc_Aplikasi
    Public usc_Pengaturan As New wpfUsc_Pengaturan

End Module
```

### Cara Content di-set (wpfWin_StartUp.xaml.vb)

```vb
Private Sub wpfWin_Loaded(...) Handles Me.Loaded
    konten_Beranda.Content = usc_Beranda
    konten_Aplikasi.Content = usc_Aplikasi
    konten_Pengaturan.Content = usc_Pengaturan
End Sub
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke `bcomm.dll` (utilities, `JalankanAplikasi`, `FolderRootBookuID`) |
| **Booku Styles** | Project Reference | XAML styling (`BookuID.Styles.dll`) |

## Fitur Utama

### 1. Tab Beranda (wpfUsc_Beranda)
- Menampilkan logo BookuID (text-based: "BOOKU" + "ID")
- Tagline "Sistem Akuntansi Terpadu"
- Versi aplikasi "Assistant v1.0"

### 2. Tab Aplikasi (wpfUsc_Aplikasi)
- Daftar aplikasi produk BookuID dalam format card
- Setiap card berisi: icon, nama, deskripsi, tombol "Jalankan"
- Cek ketersediaan aplikasi saat UserControl loaded (disable tombol jika exe tidak ada)
- Menjalankan aplikasi menggunakan `JalankanAplikasi()` dari bcomm.dll

### 3. Tab Pengaturan (wpfUsc_Pengaturan)
- Placeholder untuk fitur mendatang
- Menampilkan pesan "Segera hadir..."

## Daftar Aplikasi

| Aplikasi | Path Default | Deskripsi |
|----------|--------------|-----------|
| **Booku** | `C:\BookuID\Booku\Booku.exe` | Aplikasi Sistem Akuntansi Terpadu untuk mengelola finance, akuntansi, dan perhitungan pajak. |
| **Booku Remote** | `C:\BookuID\Booku Remote\Booku Remote.exe` | Aplikasi remote desktop untuk mengontrol PC lain dalam jaringan LAN maupun internet. |

## Window Properties

| Property | Value | Keterangan |
|----------|-------|------------|
| Width | 560 | Lebar fixed |
| Height | 480 | Tinggi fixed |
| ResizeMode | NoResize | Tidak bisa resize |
| WindowStartupLocation | CenterScreen | Muncul di tengah layar |
| WindowStyle | None | Borderless dengan custom header |
| AllowsTransparency | True | Untuk efek shadow dan rounded corners |

## Styling

### Resources dari Booku Styles

```xml
pack://application:,,,/BookuID.Styles;component/WPF/Styles/StyleAplikasi.xaml
```

### Style Lokal di wpfWin_StartUp.xaml

| Style Key | Target | Keterangan |
|-----------|--------|------------|
| `CardShadow` | DropShadowEffect | Shadow untuk container utama |
| `HeaderGradient` | LinearGradientBrush | Gradient hijau untuk header |
| `WindowControlButton` | Button | Tombol minimize (transparan, hover putih) |
| `WindowCloseButton` | Button | Tombol close (hover merah) |
| `TabItemModern` | TabItem | Tab dengan underline indicator |
| `TabControlModern` | TabControl | TabControl transparan tanpa border |

### Style Lokal di wpfUsc_Aplikasi.xaml

| Style Key | Target | Keterangan |
|-----------|--------|------------|
| `AppCardBorder` | Border | Card aplikasi dengan hover effect |
| `BtnJalankan` | Button | Tombol hijau primary |
| `BtnJalankanSekunder` | Button | Tombol biru secondary |

### Warna yang Digunakan

Dari `StyleColor.xaml`:
- `clrBgWindow`, `clrBgPanel` - Background
- `clrBorderFocus`, `clrBorderDefault`, `clrBorderPrimary` - Border
- `clrTeksPrimer`, `clrTeksSekunder`, `clrTeksDisabled` - Teks
- `clrBtnPrimerBg`, `clrBtnPrimerBgHover`, `clrBtnPrimerBgPressed` - Tombol primer
- `clrStatusInfo`, `clrStatusInfoBg` - Warna biru info
- `clrPrimary`, `clrPrimaryDark`, `clrPrimaryDarker`, `clrPrimaryLight` - Gradient header

## Cara Menambah Aplikasi Baru

1. **Di `wpfUsc_Aplikasi.xaml`:**
   - Tambah card baru di dalam `pnl_DaftarAplikasi`
   - Copy struktur card yang ada, ubah:
     - Icon (glyph dari Segoe MDL2 Assets)
     - Background icon (`clrBgPrimarySurface`, `clrStatusInfoBg`, dll.)
     - Nama aplikasi
     - Deskripsi
     - `x:Name` tombol (contoh: `btn_JalankanAplikasiBaru`)
     - Style tombol (`BtnJalankan` untuk primary, `BtnJalankanSekunder` untuk secondary)

2. **Di `wpfUsc_Aplikasi.xaml.vb`:**
   - Tambah path baru:
     ```vb
     Private ReadOnly PathAplikasiBaru As String = Path.Combine(FolderRootBookuID, "Aplikasi Baru", "Aplikasi Baru.exe")
     ```
   - Tambah cek di `CekKetersediaanAplikasi()`:
     ```vb
     btn_JalankanAplikasiBaru.IsEnabled = File.Exists(PathAplikasiBaru)
     ```
   - Tambah event handler:
     ```vb
     Private Sub btn_JalankanAplikasiBaru_Click(...) Handles btn_JalankanAplikasiBaru.Click
         If File.Exists(PathAplikasiBaru) Then
             JalankanAplikasi(PathAplikasiBaru)
         Else
             MessageBox.Show(...)
         End If
     End Sub
     ```

## Cara Menambah Tab Baru

1. **Buat UserControl baru** di folder `UserControls/`:
   - `wpfUsc_NamaTab.xaml`
   - `wpfUsc_NamaTab.xaml.vb`

2. **Tambah deklarasi** di `Modul/wpfMdl_ClassUserControl.vb`:
   ```vb
   Public usc_NamaTab As New wpfUsc_NamaTab
   ```

3. **Tambah TabItem** di `wpfWin_StartUp.xaml`:
   ```xml
   <TabItem x:Name="tab_NamaTab" Header="Nama Tab" Style="{StaticResource TabItemModern}">
       <ContentControl x:Name="konten_NamaTab"/>
   </TabItem>
   ```

4. **Set Content** di `wpfWin_StartUp.xaml.vb` (dalam `wpfWin_Loaded`):
   ```vb
   konten_NamaTab.Content = usc_NamaTab
   ```

## Aturan Pengembangan

1. **Namespace**: Semua kode harus dalam namespace `Booku_Assistant`
2. **UserControl Pattern**: Setiap tab harus dipisahkan menjadi UserControl terpisah
3. **Deklarasi**: UserControl dideklarasikan di `wpfMdl_ClassUserControl.vb` dengan format `Public usc_XXX As New wpfUsc_XXX`
4. **Styling**: Gunakan styles dari Booku Styles (pack URI), style lokal untuk komponen spesifik
5. **Launch**: Gunakan `JalankanAplikasi()` dari bcomm.dll
6. **Path**: Gunakan `FolderRootBookuID` dari bcomm.dll, kombinasikan dengan `Path.Combine()`
7. **Validasi**: Selalu cek `File.Exists()` sebelum menjalankan aplikasi
8. **Icon**: Gunakan Segoe MDL2 Assets font untuk icon (karakter Unicode)

## Fitur Keamanan & Optimasi

| Fitur | Status | Implementasi |
|-------|--------|--------------|
| **Single Instance** | Selesai | Mutex protection (`BookuAssistantSingleInstance`) |
| **Single Executable** | Selesai | Self-contained, compressed |
| **Compression** | Selesai | `EnableCompressionInSingleFile=true` |

> **Catatan:** WPF tidak mendukung IL Trimming. Untuk obfuscation, gunakan tool terpisah seperti Obfuscar.

### Single Instance (Mutex)

Aplikasi menggunakan Mutex untuk mencegah multiple instance:

```vb
' Di Application.xaml.vb (Static Constructor)
Dim appName As String = "BookuAssistantSingleInstance"
MutexApp = New Mutex(True, appName, createdNew)

If Not createdNew Then
    FocusExistingApp()
    Environment.Exit(0)
End If
```

Jika aplikasi sudah berjalan, instance baru akan fokus ke window yang ada dan keluar.

## Build Release

### Menggunakan Script (Recommended)

```batch
cd "D:\vb .net project\BookuID\Booku Assistant"
PUBLISH-RELEASE.bat
```

### Manual Build

```batch
dotnet publish "Booku Assistant.vbproj" -c Release -o "bin\Release\Final"
```

### Output Release

```
bin/Release/Final/Booku Assistant.exe  (Single executable)
```

**Fitur Release Build:**
- Single executable (tidak perlu file DLL tambahan)
- Self-contained (.NET runtime sudah include)
- Compressed (ukuran file lebih kecil)
- No debug symbols (menyulitkan debugging)

## Status Pengembangan

| Fitur | Status | Catatan |
|-------|--------|---------|
| Tab Beranda | Selesai | Logo dan tagline (wpfUsc_Beranda) |
| Tab Aplikasi | Selesai | 2 aplikasi (wpfUsc_Aplikasi) |
| Tab Pengaturan | Placeholder | Belum diimplementasi (wpfUsc_Pengaturan) |
| Launcher Booku | Selesai | Validasi ketersediaan |
| Launcher Booku Remote | Selesai | Validasi ketersediaan |
| UserControl Pattern | Selesai | Refactored dari inline content |
| Modern UI | Selesai | Borderless window dengan custom header |
| **Single Instance** | Selesai | Mutex protection |
| **Single Executable** | Selesai | Self-contained, compressed |
