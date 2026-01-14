# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Bahasa

Obrolan dan dokumentasi menggunakan **Bahasa Indonesia**.

## Project Overview

BookuID adalah aplikasi akuntansi enterprise berbasis VB.NET dengan arsitektur **hybrid** (WinForms entry point + WPF UI), menggunakan .NET 8.0 untuk Windows.

> **Arsitektur Hybrid:** Entry point menggunakan `Sub Main()` di `wpfMdl_Program.vb` (pola WinForms), sementara seluruh UI menggunakan WPF. File `App.xaml.vb` sudah disiapkan untuk migrasi WPF murni di masa depan.

### Quick Reference

| Topik | File Rules |
|-------|------------|
| Build & Release | `.claude/rules/build-commands.md` |
| Arsitektur & Modul | `.claude/rules/architecture.md` |
| WPF Styling & Warna | `.claude/rules/wpf-styling.md` |
| WPF UserControl Pattern | `.claude/rules/wpf-usercontrol-pattern.md` |
| WPF Window Pattern | `.claude/rules/wpf-window-pattern.md` |
| WPF Host Pattern | `.claude/rules/wpf-host-pattern.md` |
| Database | `.claude/rules/database.md` |
| Modul & Dependencies | `.claude/rules/modules-dependencies.md` |

## Code Conventions

1. **Bahasa Indonesia**: Gunakan bahasa Indonesia untuk semua penamaan
   - "Jurnal" bukan "Journal"
   - "BukuBesar" bukan "GeneralLedger"
   - "Pembelian" bukan "Purchase"

2. **Prinsip Clean Code**: YAGNI, DRY, KISS - ikuti pola yang sudah ada dalam codebase

3. **Single Instance**: Aplikasi menggunakan Mutex untuk mencegah multiple instance

4. **Deprecated Code**: File/folder dengan prefix `X_` adalah kode yang sudah deprecated dan tidak boleh digunakan

5. **Git Commit**: Setiap perubahan kode akan di-commit secara mandiri oleh programmer. Claude tidak perlu menawarkan untuk meng-commit setiap perubahan

6. **Sistem Warna Terpusat**: Semua warna harus menggunakan sistem warna terpusat
   - XAML: Gunakan `{StaticResource clrXxx}` dari `StyleColor.xaml`
   - Code-Behind: Gunakan variabel `WarnaXxx_WPF` dari `mdlPub_ModulUmum.vb`
   - **JANGAN** hardcode warna seperti `#388E3C` atau `Brushes.Red`
   - Lihat `.claude/rules/wpf-styling.md` untuk daftar lengkap warna

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
