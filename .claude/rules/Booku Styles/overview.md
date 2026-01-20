# Booku Styles

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Styles** (folder `BookuID/Booku Styles/`)

## Deskripsi

**Booku Styles** adalah project styling terpusat yang menangani semua WPF styling untuk solution BookuID. Project ini berisi XAML resources, behaviors, dan styling modules yang digunakan oleh Booku (main app).

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Styles |
| **Assembly Name** | `BookuID.Styles.dll` |
| **Namespace** | `BookuID.Styles` |
| **Target Framework** | .NET 8.0 Windows |
| **Output Type** | Library |
| **Entry Point** | Tidak ada (library) |

## Struktur File

```
Booku Styles/
├── Booku Styles.vbproj
└── WPF/
    ├── Styles/                          # XAML Resources
    │   ├── StyleAplikasi.xaml           # Master file (merge semua style)
    │   ├── StyleColor.xaml              # Definisi warna terpusat
    │   ├── StyleWindow.xaml
    │   ├── StyleUserControl.xaml
    │   ├── StyleButton.xaml
    │   ├── StyleTextBox.xaml
    │   ├── StyleRichTextBox.xaml
    │   ├── StyleComboBox.xaml
    │   ├── StyleDataGrid.xaml
    │   ├── StyleDatePicker.xaml
    │   ├── StyleCheckBox.xaml
    │   ├── StyleRadioButton.xaml
    │   ├── StyleLabel.xaml
    │   ├── StyleTextBlock.xaml
    │   ├── StylePanel.xaml
    │   ├── StyleGrid.xaml
    │   ├── StyleGroupBox.xaml
    │   ├── StyleBorder.xaml
    │   ├── StyleScrollViewer.xaml
    │   ├── StyleMenu.xaml
    │   ├── StyleTabControl.xaml
    │   ├── StyleStatusBar.xaml
    │   └── StyleLainnya.xaml
    │
    ├── Behaviors/                       # Attached Behaviors
    │   ├── TextBoxBehavior.vb           # Input validation untuk TextBox
    │   └── RichTextBoxBehavior.vb       # Input validation untuk RichTextBox
    │
    └── Modules/                         # VB Modules
        ├── wpfMdl_StyleColor.vb         # Color brush properties
        ├── StylingElemen.vb             # Style name constants
        ├── mdlPub_Styling.vb            # Window styling helpers
        ├── mdlPub_WpfInputHelper.vb     # Input validation helpers
        └── cls_WindowDialogWPF_TanpaX.vb  # Class untuk window tanpa tombol X
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke utilities (`bcomm.dll`) |

## Dependency Order

```
Booku Library (bcomm.dll)
    ↓
Booku Styles (BookuID.Styles.dll)
    ↓
Booku (Main App) + Project lainnya
```

## Komponen Utama

### 1. XAML Styles (`/WPF/Styles/`)

| File | Tujuan |
|------|--------|
| `StyleAplikasi.xaml` | Master file yang merge semua style |
| `StyleColor.xaml` | Definisi warna terpusat (2-Layer Color System) |
| `Style[Komponen].xaml` | Style per komponen WPF |

### 2. Attached Behaviors (`/WPF/Behaviors/`)

| File | Tujuan |
|------|--------|
| `TextBoxBehavior.vb` | Validasi input TextBox (hanya angka, separator ribuan, dll) |
| `RichTextBoxBehavior.vb` | Validasi karakter dilarang untuk RichTextBox |

### 3. VB Modules (`/WPF/Modules/`)

| File | Tujuan |
|------|--------|
| `wpfMdl_StyleColor.vb` | Definisi warna sebagai `SolidColorBrush` untuk akses dari code-behind |
| `StylingElemen.vb` | Konstanta nama style untuk referensi dari code-behind |
| `mdlPub_Styling.vb` | Helper functions untuk styling WPF Window (`StyleWindowDialogWPF_Dasar`, dll) |
| `mdlPub_WpfInputHelper.vb` | Helper functions untuk validasi input (`HanyaBolehInputAngka_WPF`, dll) |
| `cls_WindowDialogWPF_TanpaX.vb` | Class untuk membuat window tanpa tombol close (X) |

## Cara Menggunakan di Project Lain

### Akses XAML Resources

Di `App.xaml` atau Window/UserControl resources:

```xml
<ResourceDictionary Source="pack://application:,,,/BookuID.Styles;component/WPF/Styles/StyleAplikasi.xaml" />
```

### Akses dari Code-Behind

```vb
Imports BookuID.Styles

' Menggunakan warna
label.Foreground = wpfMdl_StyleColor.clrPrimary

' Menggunakan nama style
txtBox.Style = CType(Application.Current.Resources(StylingElemen.style_TextBoxFormDialogAngkaSeparatePlus), Style)

' Styling window dialog
StyleWindowDialogWPF_Dasar(Me)              ' SizeToContent, CenterScreen, NoResize
StyleWindowDialogWPF_TanpaTombolX(Me)       ' Sembunyikan tombol X (close)
StyleWindowDialogWPF_Sizable(Me)            ' Aktifkan resize
```

## Backward Compatibility

File `wpfMdl_StyleBridge.vb` di `/Booku/WPF/Modul Umum/` menyediakan alias untuk backward compatibility:

```vb
' Di Booku project - kode lama tetap berfungsi
label.Foreground = clrPrimary  ' Alias ke BookuID.Styles.wpfMdl_StyleColor.clrPrimary
```

## Catatan Namespace

- **RootNamespace** di project file sudah set ke `BookuID.Styles`
- File VB di project ini **TIDAK PERLU** deklarasi `Namespace BookuID.Styles` wrapper
- SDK VB.NET otomatis prepend RootNamespace ke semua code

```vb
' BENAR - tanpa wrapper namespace
Imports System.Windows.Media

Public Module wpfMdl_StyleColor
    Public ReadOnly clrPrimary As New SolidColorBrush(...)
End Module
```

## Aturan Pengembangan

1. **Lokasi File**: Semua styling files harus ada di project ini
2. **Namespace**: Tidak perlu wrapper namespace (sudah via RootNamespace)
3. **Warna**: Sinkronkan dengan `mdlPub_ModulUmum.vb` di Booku Library
4. **Pack URI**: Gunakan `pack://application:,,,/BookuID.Styles;component/...`
5. **Bridge Update**: Jika tambah API baru, update `wpfMdl_StyleBridge.vb` di Booku

## Dokumentasi Terkait

| Topik | File |
|-------|------|
| Cara penggunaan WPF Styling | `.claude/rules/Booku/wpf-styling.md` |
| Warna di Code-Behind | `.claude/rules/Booku Library/overview.md` |
| Architecture overview | `.claude/rules/Booku/architecture.md` |
