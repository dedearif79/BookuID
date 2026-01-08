# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build entire solution
dotnet build BookuID.sln

# Build specific project
dotnet build "Booku/Booku.vbproj"
dotnet build "Booku Library/Booku Library.vbproj"

# Build for release (single file executable)
dotnet publish "Booku/Booku.vbproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Clean build artifacts
dotnet clean BookuID.sln
```

## Project Overview

BookuID adalah aplikasi akuntansi enterprise berbasis VB.NET dengan arsitektur hybrid WinForms + WPF, menggunakan .NET 8.0 untuk Windows.

### Solution Structure (8 Projects)

| Project | Deskripsi | Dependencies |
|---------|-----------|--------------|
| **Booku** | Aplikasi utama | Booku Library |
| **Booku Library** | Shared library (namespace: `bcomm`) | - |
| **Booku Installer** | Installer + XAMPP setup | Booku Library |
| **Booku Updater** | Update utility | Booku Library |
| **Booku Backup** | Backup/restore/kloning | Booku Library |
| **Booku Encrypter** | Enkripsi utility | Booku Library |
| **Booku Assistant** | Supporting utility | Booku Uninstaller |
| **Booku Uninstaller** | Uninstaller | Booku Library |

## Architecture

### Hybrid WinForms + WPF Pattern

Aplikasi menggunakan WinForms sebagai container utama dengan WPF untuk UI modern:

1. **Entry Point**: `frm_BOOKU` (WinForms) di `/Booku/00_START_UP/`
2. **Startup Window**: `wpfWin_StartUp` (WPF) diluncurkan saat inisialisasi
3. **User Controls**: WPF UserControls di-host dalam WinForms via `System.Windows.Forms.Integration`
4. **Exception Handling**: 4 handler terpisah (WinForms ThreadException, AppDomain UnhandledException, Task UnobservedTaskException, WPF DispatcherUnhandledException)

### File Naming Convention (Prefix)

| Prefix | Tipe | Contoh |
|--------|------|--------|
| `frm_` | WinForms Form | `frm_BOOKU.vb`, `frm_BukuBesar.vb` |
| `wpfWin_` | WPF Window | `wpfWin_StartUp.xaml` |
| `wpfUsc_` | WPF UserControl | `wpfUsc_BukuBesar.xaml` |
| `mdl_` | VB Module | `mdl_PublicSub.vb` |
| `cls_` | Class | `cls_DecimalConverter.vb` |

### Core Modules (`/Booku/Modul Umum/`)

- **mdl_PublicSub.vb** - Subroutine library utama (inisialisasi data, COA tagging, konfigurasi jurnal)
- **mdl_VariabelUmum.vb** - Variabel global (info aplikasi, perusahaan, user, path folder)
- **mdl_KoneksiDatabase.vb** - Koneksi database (XAMPP/MySQL init, ODBC, OleDb, transaksi)
- **mdl_TautanCOA.vb** - Konfigurasi Chart of Accounts
- **mdl_Logger.vb** - Logging exception dan error

### Shared Library (`/Booku Library/`)

- **mdlPub_Enkripsi.vb** - AES-256 encryption dengan 3 key-IV pairs
- **mdlPub_KoneksiDatabase.vb** - Database utilities publik
- **mdlPub_ModulUmum.vb** - Common utilities
- **mdlPub_Styling.vb** - UI styling utilities

### WPF Styling System (`/Booku/WPF/Styles/`)

`StyleAplikasi.xaml` adalah master styles, dengan component-specific styles untuk Button, DataGrid, TextBox, ComboBox, dll.

### Database Architecture

- **Remote Server**: MySQL di sqlclusters.com
- **Database Naming**: Prefix `bookuid_booku_` dengan suffix:
  - `_public` - Konfigurasi remote (read-only)
  - `_dasar` - Base configuration
  - `_general` - Data transaksional
  - `_transaksi` - Record transaksi

## Code Conventions

1. **Bahasa Indonesia**: Gunakan bahasa Indonesia untuk semua penamaan (variabel, fungsi, class, module)
   - "Jurnal" bukan "Journal"
   - "BukuBesar" bukan "GeneralLedger"
   - "Pembelian" bukan "Purchase"

2. **Prinsip Clean Code**: YAGNI, DRY, KISS - ikuti pola yang sudah ada dalam codebase

3. **Single Instance**: Aplikasi menggunakan Mutex untuk mencegah multiple instance

4. **Deprecated Code (Dead Code)**: Ada beberapa file, folder, function, sub, class, module dan lainnya di codebase yang sudah deprecated. Ciri-cirinya (tidak mutlak):
   - File atau folder yang diawali dengan prefix `X_` (contoh: `X_frm_LaporanLama.vb`, `X_Modul Lama/`)
   - Sub atau function yang sudah tidak dipanggil lagi di tempat lain dalam codebase

## Feature Modules (`/Booku/`)

Fitur-fitur terorganisir dalam folder terpisah:

- **Akuntansi**: `Buku Besar`, `Jurnal`, `Laporan`
- **Transaksi**: `Pembelian - Penjualan`, `Transaksi`, `Manajemen Asset`
- **Data Master**: `Data/` (COA, Karyawan, Mitra, User, Project, Kurs)
- **Buku Pengawasan**: Deposit Operasional, Gaji, Hutang Bank-Leasing, Hutang Pajak, PPh Pasal 29
- **Admin**: `App Admin`, `App Developer`, `App IT`, `Security`, `Registrasi`

## Key Dependencies

- `MySql.Data` - MySQL connectivity
- `ClosedXML` - Excel handling
- `Microsoft.Web.WebView2` - Web content embedding
- `Newtonsoft.Json` - JSON processing
- `HtmlAgilityPack` - HTML parsing
