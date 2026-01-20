# Architecture

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku** (folder `BookuID/Booku/`)

## Terminologi

| Istilah | Mengacu Kepada |
|---------|----------------|
| **BookuID** | Keseluruhan Solution (berisi 9 project) |
| **Booku** / **Project Booku** | Project utama di folder `BookuID/Booku/` |
| **Booku Styles** | Project styling terpusat di folder `BookuID/Booku Styles/` |

> **PENTING:** Dokumentasi arsitektur di file ini fokus pada **project Booku** (project utama). Untuk daftar lengkap semua project dalam solution BookuID, lihat `modules-dependencies.md`.

## WPF Native Architecture

Project Booku menggunakan arsitektur **WPF murni**:
- **Entry Point**: `App.xaml` + `App.xaml.vb` (WPF Application class)
- **UI**: Seluruh antarmuka menggunakan WPF (Window, UserControl, Style)
- **Styling**: Terpusat di project Booku Styles (`BookuID.Styles.dll`)
- **Library**: Booku Library (bcomm.dll) juga WPF murni tanpa dependency WinForms

### Dependency Order

```
Booku Library (bcomm.dll)
    ↓
Booku Styles (BookuID.Styles.dll)
    ↓
Booku (Main App)
```

### Konfigurasi Project (Booku.vbproj)

```xml
<!-- Entry point menggunakan App.xaml (WPF Application class) -->
<!-- UseWindowsForms tetap true untuk kompatibilitas library, tapi entry point sudah WPF murni -->
<UseWindowsForms>true</UseWindowsForms>
<UseWPF>True</UseWPF>
<StartupObject>Booku.App</StartupObject>
```

Entry point adalah `App.Main()` di `App.xaml.vb`. VB.NET memerlukan `Shared Sub Main()` sebagai entry point, tidak seperti C# yang auto-generate. `UseWindowsForms=true` dipertahankan hanya untuk kompatibilitas dengan beberapa library.

### Entry Point dan Startup Flow

```
App.xaml.vb (WPF Application)               <- Entry point AKTIF
    |
    |-- Shared Sub Main() [Entry Point]:
    |   |-- Dim app As New App()
    |   +-- app.Run()
    |
    |-- Shared Sub New() [Static Constructor - dipanggil sebelum Main]:
    |   |-- Mutex (single instance protection)
    |   +-- AppDomain & Task exception handlers
    |
    |-- Sub New() [Instance Constructor]:
    |   +-- Manual load StyleAplikasi.xaml ke Resources
    |
    |-- OnStartup():
    |   |-- WPF Dispatcher exception handler
    |   |-- Parameter awal (file paths)
    |   |-- StandarisasiSetinganAplikasi()
    |   |-- wpfWin_StartUp.ShowDialog()   <- Login/splash screen
    |   |-- UpdateInfoAplikasi()
    |   |-- DataAwalLoadingAplikasi()
    |   |-- CekVersiDanApdetAplikasi()
    |   |-- CekStatusRegistrasiPerangkat()
    |   +-- win_BOOKU.Show()              <- Main window
    |
    +-- OnExit():
        +-- MutexApp.ReleaseMutex()
```

### File Entry Point

| File | Status | Keterangan |
|------|--------|------------|
| `App.xaml` + `App.xaml.vb` | **AKTIF** | Entry point WPF Application class |
| `X_wpfMdl_Program.vb` | Deprecated | Entry point lama (hybrid) |

### Komponen Utama

1. **Entry Point**: `App.xaml` + `App.xaml.vb` di `/Booku/`
2. **Main Window**: `wpfWin_BOOKU` (WPF) di `/Booku/00_START_UP/`
3. **Startup Dialog**: `wpfWin_StartUp` (WPF) untuk login/splash
4. **User Controls**: WPF UserControls untuk semua modul
5. **Host Pattern**: WPF Host sebagai wrapper UserControl dengan konfigurasi spesifik
6. **Exception Handling**: 3 handler (AppDomain, Task, WPF Dispatcher)
7. **Resources**: Auto-load dari `App.xaml` + manual load di constructor

## File Naming Convention

| Prefix | Tipe | Contoh |
|--------|------|--------|
| `wpfWin_` | WPF Window | `wpfWin_StartUp.xaml` |
| `wpfUsc_` | WPF UserControl | `wpfUsc_BukuBesar.xaml` |
| `wpfHost_` | WPF Host (wrapper) | `wpfHost_BukuPembelian.vb` |
| `wpfMdl_` | WPF Module | `wpfMdl_ClassWindow.vb` |
| `mdl_` | VB Module | `mdl_PublicSub.vb` |
| `cls_` | Class | `cls_DecimalConverter.vb` |
| `X_` | **Deprecated** | `X_mdlWpf_Program.vb` |

## Core Modules (`/Booku/Modul Umum/`)

- **mdl_PublicSub.vb** - Subroutine library utama (inisialisasi data, COA tagging, konfigurasi jurnal)
- **mdl_VariabelUmum.vb** - Variabel global (info aplikasi, perusahaan, user, path folder)
- **mdl_KoneksiDatabase.vb** - Koneksi database (XAMPP/MySQL init, ODBC, OleDb, transaksi)
- **mdl_TautanCOA.vb** - Konfigurasi Chart of Accounts
- **mdl_Logger.vb** - Logging exception dan error

## WPF Core Modules (`/Booku/WPF/Modul Umum/`)

- **wpfMdl_ClassWindow.vb** - Deklarasi window global (`win_BOOKU`, `win_Startup`, dll)
- **wpfMdl_ClassUserControl.vb** - Deklarasi UserControl global (`usc_BukuBesar`, dll)
- **wpfMdl_ClassHost.vb** - Deklarasi Host global (`host_BukuPembelian_Lokal`, dll)
- **wpfMdl_PublicSub.vb** - Subroutine khusus WPF (styling, DataGrid helpers, dll)
- **wpfMdl_StyleBridge.vb** - Bridge/alias ke `BookuID.Styles` untuk backward compatibility
- **wpfMdl_Pesan.vb** - Custom message dialog WPF
- **wpfMdl_TutupBuku.vb** - Logic tutup buku/periode
- **wpfMdl_SaldoDanPenyesuaian.vb** - Kalkulasi saldo dan penyesuaian

> **Note:** Entry point aktif ada di `App.xaml` + `App.xaml.vb` (WPF Application class).
> File `X_wpfMdl_Program.vb` adalah entry point lama (hybrid) yang sudah deprecated.

## Shared Library (`/Booku Library/`)

- **mdlPub_Enkripsi.vb** - AES-256 encryption dengan 3 key-IV pairs
- **mdlPub_KoneksiDatabase.vb** - Database utilities publik
- **mdlPub_ModulUmum.vb** - Common utilities + **variabel warna WPF** (`WarnaXxx_WPF`)
- **mdlPub_Styling.vb** - UI styling utilities

## WPF Styles (`/Booku Styles/WPF/Styles/`)

> **CATATAN:** File styling sudah dipindahkan ke project **Booku Styles** (post-migrasi).

**Lokasi Aktual:**
- **XAML Styles**: `/Booku Styles/WPF/Styles/` (StyleAplikasi.xaml, StyleColor.xaml, dll)
- **Behaviors**: `/Booku Styles/WPF/Behaviors/` (TextBoxBehavior.vb, RichTextBoxBehavior.vb)
- **Modules**: `/Booku Styles/WPF/Modules/` (wpfMdl_StyleColor.vb, StylingElemen.vb)

**Backward Compatibility:**
- File `wpfMdl_StyleBridge.vb` di `/Booku/WPF/Modul Umum/` menyediakan alias
- Kode lama di Booku tetap berfungsi tanpa perubahan

> Lihat `.claude/rules/Booku Styles/overview.md` untuk dokumentasi project Booku Styles
> Lihat `.claude/rules/Booku/wpf-styling.md` untuk cara penggunaan styling
