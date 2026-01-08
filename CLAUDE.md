# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Bahasa

Obrolan dan dokumentasi menggunakan **Bahasa Indonesia**.

## Build Commands

```bash
# Build entire solution (menggunakan MSBuild Visual Studio karena COM Reference)
"/mnt/c/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/MSBuild.exe" "/mnt/d/vb .net project/BookuID/BookuID.sln" -t:Build -verbosity:minimal

# Build specific project
"/mnt/c/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/MSBuild.exe" "/mnt/d/vb .net project/BookuID/Booku/Booku.vbproj" -t:Build -verbosity:minimal

# Alternative dengan dotnet (tanpa COM Reference support)
dotnet build BookuID.sln
dotnet build "Booku/Booku.vbproj"

# Clean build artifacts
dotnet clean BookuID.sln
```

### Release Publishing (dengan Obfuscation)

```powershell
# Dari Windows PowerShell, jalankan script publish-release
cd "D:\vb .net project\BookuID\Booku"
.\publish-release.ps1

# Skip obfuscation (untuk testing cepat)
.\publish-release.ps1 -SkipObfuscation
```

Output: `Booku/bin/Release/Final/Booku.exe` (single-file executable, self-contained)

## Project Overview

BookuID adalah aplikasi akuntansi enterprise berbasis VB.NET dengan arsitektur hybrid WinForms + WPF, menggunakan .NET 8.0 untuk Windows.

### Solution Structure (8 Projects)

| Project | Deskripsi | Namespace |
|---------|-----------|-----------|
| **Booku** | Aplikasi utama | Booku |
| **Booku Library** | Shared library | bcomm |
| **Booku Installer** | Installer + XAMPP setup | Booku_Installer |
| **Booku Updater** | Update utility | Booku_Updater |
| **Booku Backup** | Backup/restore/kloning | Booku_Backup |
| **Booku Encrypter** | Enkripsi utility | Booku_Encrypter |
| **Booku Assistant** | Supporting utility | Booku_Assistant |
| **Booku Uninstaller** | Uninstaller | Booku_Uninstaller |

Semua project bergantung pada **Booku Library** kecuali Booku Assistant (bergantung pada Booku Uninstaller).

## Architecture

### Hybrid WinForms + WPF Pattern

Aplikasi menggunakan WinForms sebagai container utama dengan WPF untuk UI modern:

1. **Entry Point**: `frm_BOOKU` (WinForms MDI Container) di `/Booku/00_START_UP/`
2. **Startup Window**: `wpfWin_StartUp` (WPF) diluncurkan saat inisialisasi
3. **User Controls**: WPF UserControls di-host dalam WinForms via `System.Windows.Forms.Integration`
4. **Exception Handling**: 4 handler terpisah (WinForms ThreadException, AppDomain UnhandledException, Task UnobservedTaskException, WPF DispatcherUnhandledException)

### File Naming Convention

| Prefix | Tipe | Contoh |
|--------|------|--------|
| `frm_` | WinForms Form | `frm_BOOKU.vb` |
| `wpfWin_` | WPF Window | `wpfWin_StartUp.xaml` |
| `wpfUsc_` | WPF UserControl | `wpfUsc_BukuBesar.xaml` |
| `mdl_` | VB Module | `mdl_PublicSub.vb` |
| `cls_` | Class | `cls_DecimalConverter.vb` |
| `X_` | **Deprecated** | `X_frm_LaporanLama.vb` |

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

`StyleAplikasi.xaml` adalah master styles dengan component-specific styles untuk Button, DataGrid, TextBox, ComboBox, dll.

### Database Architecture

- **Remote Server**: MySQL di sqlclusters.com
- **Database Naming**: Prefix `bookuid_booku_` dengan suffix:
  - `_public` - Konfigurasi remote (read-only)
  - `_dasar` - Base configuration
  - `_general` - Data transaksional
  - `_transaksi` - Record transaksi

## Code Conventions

1. **Bahasa Indonesia**: Gunakan bahasa Indonesia untuk semua penamaan
   - "Jurnal" bukan "Journal"
   - "BukuBesar" bukan "GeneralLedger"
   - "Pembelian" bukan "Purchase"

2. **Prinsip Clean Code**: YAGNI, DRY, KISS - ikuti pola yang sudah ada dalam codebase

3. **Single Instance**: Aplikasi menggunakan Mutex untuk mencegah multiple instance

4. **Deprecated Code**: File/folder dengan prefix `X_` adalah kode yang sudah deprecated dan tidak boleh digunakan

5. **Git Commit**: Setiap perubahan kode akan di-commit secara mandiri oleh programmer. Claude tidak perlu menawarkan untuk meng-commit setiap perubahan

## Feature Modules (`/Booku/`)

- **Akuntansi**: `Buku Besar`, `Jurnal`, `Laporan`
- **Transaksi**: `Pembelian - Penjualan`, `Transaksi`, `Manajemen Asset`
- **Data Master**: `Data/` (COA, Karyawan, Mitra, User, Project, Kurs)
- **Buku Pengawasan**: Deposit Operasional, Gaji, Hutang Bank-Leasing, Hutang Pajak
- **Admin**: `App Admin`, `App Developer`, `App IT`, `Security`, `Registrasi`

## Key Dependencies

| Package | Kegunaan |
|---------|----------|
| MySql.Data | MySQL connectivity |
| ClosedXML | Excel handling |
| Microsoft.Web.WebView2 | Web content embedding |
| Newtonsoft.Json | JSON processing |
| HtmlAgilityPack | HTML parsing |
| Obfuscar | Code obfuscation (Release build) |
