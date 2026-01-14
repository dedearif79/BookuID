# Architecture

## WPF Application Pattern

Aplikasi menggunakan arsitektur **full WPF** untuk semua UI.

### Entry Point dan Startup Flow

```
App.xaml.vb (Inherits Application)       <- Entry point utama
    |-- Static Constructor:
    |   |-- Mutex (single instance)
    |   +-- Exception handlers (AppDomain, Task)
    |-- Instance Constructor:
    |   +-- Manual load StyleAplikasi.xaml ke Resources
    |-- OnStartup:
    |   |-- WPF Dispatcher exception handler
    |   |-- wpfWin_StartUp.ShowDialog()   <- Login/splash screen
    |   |-- Startup logic (UpdateInfoAplikasi, DataAwalLoading, dll)
    |   +-- win_BOOKU.Show()              <- Main window
    +-- OnExit:
        +-- MutexApp.ReleaseMutex()
```

### Komponen Utama

1. **Entry Point**: `App.xaml` + `App.xaml.vb` (WPF Application class) di `/Booku/`
2. **Main Window**: `wpfWin_BOOKU` (WPF) di `/Booku/00_START_UP/`
3. **Startup Dialog**: `wpfWin_StartUp` (WPF) untuk login/splash
4. **User Controls**: WPF UserControls untuk semua modul
5. **Host Pattern**: WPF Host sebagai wrapper UserControl dengan konfigurasi spesifik
6. **Exception Handling**: 3 handler (AppDomain, Task, WPF Dispatcher)
7. **Resources**: Load dari `App.xaml` + manual load di constructor (StyleAplikasi.xaml)

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
- **wpfMdl_Pesan.vb** - Custom message dialog WPF
- **wpfMdl_TutupBuku.vb** - Logic tutup buku/periode
- **wpfMdl_SaldoDanPenyesuaian.vb** - Kalkulasi saldo dan penyesuaian

> **Note:** Entry point ada di `/Booku/App.xaml.vb` (WPF Application class).
> File `X_mdlWpf_Program.vb` adalah versi lama yang sudah deprecated.

## Shared Library (`/Booku Library/`)

- **mdlPub_Enkripsi.vb** - AES-256 encryption dengan 3 key-IV pairs
- **mdlPub_KoneksiDatabase.vb** - Database utilities publik
- **mdlPub_ModulUmum.vb** - Common utilities + **variabel warna WPF** (`WarnaXxx_WPF`)
- **mdlPub_Styling.vb** - UI styling utilities

## WPF Styles (`/Booku/WPF/Styles/`)

- **StyleAplikasi.xaml** - Master file yang merge semua style
- **StyleColor.xaml** - **Definisi warna terpusat** (2-Layer Color System)
- **StyleDataGrid.xaml** - Style DataGrid dengan warna seleksi
- **Style[Komponen].xaml** - Style per komponen (Button, TextBox, dll)

> Lihat `.claude/rules/wpf-styling.md` untuk dokumentasi lengkap sistem warna
