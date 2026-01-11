# Architecture

## Hybrid WinForms + WPF Pattern

Aplikasi menggunakan arsitektur hybrid dengan **WPF sebagai dasar** dan WinForms sebagai fallback.

### Entry Point dan Startup Flow

```
mdlWpf_Program.Main()                    <- Entry point utama
    |-- Mutex (single instance)
    |-- Exception handlers (AppDomain, Task, WPF Dispatcher)
    |-- WPF Application + ResourceDictionary
    |-- wpfWin_StartUp.ShowDialog()       <- Login/splash screen
    |-- Startup logic (UpdateInfoAplikasi, DataAwalLoading, dll)
    |-- [Developer PC] Mode Selection Dialog
    |       |-- Mode Modern -> wpfWin_BOOKU (WPF)
    |       +-- Mode Classic -> frm_BOOKU (WinForms)
    +-- app.Run(wpfWin_BOOKU)             <- Main window
```

### Mode Aplikasi

| Mode | Window Utama | Keterangan |
|------|--------------|------------|
| **Modern** (Default) | `wpfWin_BOOKU` | WPF Window, UI utama |
| **Classic** (Fallback) | `frm_BOOKU` | WinForms MDI, hanya untuk developer PC |

### Komponen Utama

1. **Entry Point**: `mdlWpf_Program.Main()` di `/Booku/WPF/Modul Umum/`
2. **Main Window**: `wpfWin_BOOKU` (WPF) di `/Booku/00_START_UP/`
3. **Fallback Window**: `frm_BOOKU` (WinForms MDI) di `/Booku/00_START_UP/`
4. **Startup Dialog**: `wpfWin_StartUp` (WPF) untuk login/splash
5. **User Controls**: WPF UserControls untuk semua modul
6. **Exception Handling**: 3 handler (AppDomain, Task, WPF Dispatcher)

## File Naming Convention

| Prefix | Tipe | Contoh |
|--------|------|--------|
| `frm_` | WinForms Form | `frm_BOOKU.vb` |
| `wpfWin_` | WPF Window | `wpfWin_StartUp.xaml` |
| `wpfUsc_` | WPF UserControl | `wpfUsc_BukuBesar.xaml` |
| `mdl_` | VB Module | `mdl_PublicSub.vb` |
| `cls_` | Class | `cls_DecimalConverter.vb` |
| `X_` | **Deprecated** | `X_frm_LaporanLama.vb` |

## Core Modules (`/Booku/Modul Umum/`)

- **mdl_PublicSub.vb** - Subroutine library utama (inisialisasi data, COA tagging, konfigurasi jurnal)
- **mdl_VariabelUmum.vb** - Variabel global (info aplikasi, perusahaan, user, path folder)
- **mdl_KoneksiDatabase.vb** - Koneksi database (XAMPP/MySQL init, ODBC, OleDb, transaksi)
- **mdl_TautanCOA.vb** - Konfigurasi Chart of Accounts
- **mdl_Logger.vb** - Logging exception dan error

## WPF Core Modules (`/Booku/WPF/Modul Umum/`)

- **mdlWpf_Program.vb** - **Entry point utama** (`Sub Main`), Mutex, exception handlers, WPF Application lifecycle
- **wpfMdl_ClassWindow.vb** - Deklarasi window global (`win_BOOKU`, `win_Startup`, dll)
- **wpfMdl_PublicSub.vb** - Subroutine khusus WPF

## Shared Library (`/Booku Library/`)

- **mdlPub_Enkripsi.vb** - AES-256 encryption dengan 3 key-IV pairs
- **mdlPub_KoneksiDatabase.vb** - Database utilities publik
- **mdlPub_ModulUmum.vb** - Common utilities
- **mdlPub_Styling.vb** - UI styling utilities
