# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Bahasa

Obrolan dan dokumentasi menggunakan **Bahasa Indonesia**.

## Project Overview

BookuID adalah aplikasi akuntansi enterprise berbasis VB.NET dengan arsitektur hybrid WinForms + WPF, menggunakan .NET 8.0 untuk Windows.

### Quick Reference

| Topik | File Rules |
|-------|------------|
| Build & Release | `.claude/rules/build-commands.md` |
| Arsitektur & Modul | `.claude/rules/architecture.md` |
| WPF Styling | `.claude/rules/wpf-styling.md` |
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

## File Naming Convention

| Prefix | Tipe | Contoh |
|--------|------|--------|
| `frm_` | WinForms Form | `frm_BOOKU.vb` |
| `wpfWin_` | WPF Window | `wpfWin_StartUp.xaml` |
| `wpfUsc_` | WPF UserControl | `wpfUsc_BukuBesar.xaml` |
| `wpfHost_` | WPF Host (wrapper) | `wpfHost_BukuPembelian.vb` |
| `mdl_` | VB Module | `mdl_PublicSub.vb` |
| `cls_` | Class | `cls_DecimalConverter.vb` |
| `X_` | **Deprecated** | `X_frm_LaporanLama.vb` |
