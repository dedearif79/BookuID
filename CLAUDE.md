# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Bahasa

Obrolan dan dokumentasi menggunakan **Bahasa Indonesia**.

## Terminologi

| Istilah | Mengacu Kepada |
|---------|----------------|
| **BookuID** | Keseluruhan Solution (berisi 11 project) |
| **Booku** / **Project Booku** | Project utama di folder `BookuID/Booku/` |
| **Booku Styles** | Project styling terpusat di folder `BookuID/Booku Styles/` |

> **PENTING:** Jangan keliru antara "BookuID" (solution) dan "Booku" (project utama). Jika disebut "project Booku", artinya mengacu kepada project yang ada di folder `BookuID/Booku/`, bukan keseluruhan solution.

## Solution Overview

**BookuID** adalah sebuah Solution berbasis VB.NET dengan arsitektur **WPF murni** untuk Windows, menggunakan .NET 8.0. Solution ini berisi beberapa project pendukung yang saling terintegrasi.

**Booku** (project utama) adalah aplikasi **Sistem Akuntansi Terpadu** yang memadukan:
- Sisi **Finance** (pengelolaan keuangan)
- Sisi **Akuntansi** (pembukuan)
- **Perhitungan Pajak** otomatis sesuai peraturan perpajakan Indonesia

**Multicurrency:** Mendukung 8 mata uang — IDR (Rupiah) + 7 mata uang asing: **USD, AUD, JPY, CNY, EUR, SGD, GBP**. Dapat ditambah sesuai kebutuhan klien.

> **Arsitektur WPF Murni:** Entry point menggunakan `App.xaml` + `App.xaml.vb` (WPF Application class). Seluruh UI menggunakan WPF (Window, UserControl, Style). Styling terpusat di project Booku Styles (`BookuID.Styles.dll`). Tidak ada dependency WinForms.

### Quick Reference

**Dokumentasi Umum (Semua Project)**

| Topik | File Rules |
|-------|------------|
| Build & Release | `.claude/rules/build-commands.md` |
| Database | `.claude/rules/database.md` |
| Modul & Dependencies | `.claude/rules/modules-dependencies.md` |

**Dokumentasi Khusus Project Booku**

| Topik | File Rules |
|-------|------------|
| Arsitektur & Modul | `.claude/rules/Booku/architecture.md` |
| WPF Styling & Warna | `.claude/rules/Booku/wpf-styling.md` |
| WPF UserControl Pattern | `.claude/rules/Booku/wpf-usercontrol-pattern.md` |
| WPF Window Pattern (Index) | `.claude/rules/Booku/wpf-window-pattern.md` |
| ↳ Window Input/Edit | `.claude/rules/Booku/wpf-window-input-pattern.md` |
| ↳ Window List/Picker | `.claude/rules/Booku/wpf-window-list-pattern.md` |
| ↳ Window Common | `.claude/rules/Booku/wpf-window-common.md` |
| WPF Host Pattern | `.claude/rules/Booku/wpf-host-pattern.md` |
| WPF Async Loading | `.claude/rules/Booku/wpf-async-loading-pattern.md` |

**Dokumentasi Khusus Project Booku Styles**

**Booku Styles** adalah project styling terpusat yang menangani semua WPF styling untuk solution BookuID. Berisi XAML resources, behaviors, dan styling modules. Dikompilasi menjadi `BookuID.Styles.dll`.

| Komponen | Isi |
|----------|-----|
| XAML Styles | `StyleAplikasi.xaml`, `StyleColor.xaml`, dan 20+ file style komponen |
| Behaviors | `TextBoxBehavior.vb`, `RichTextBoxBehavior.vb` |
| Modules | `wpfMdl_StyleColor.vb`, `StylingElemen.vb`, `mdlPub_Styling.vb` |

| Topik | File Rules |
|-------|------------|
| Overview & Struktur | `.claude/rules/Booku Styles/overview.md` |

**Dokumentasi Khusus Project Booku Library**

**Booku Library** adalah shared library (`bcomm.dll`) yang digunakan oleh seluruh project dalam solution BookuID. Berisi utilities, enkripsi, koneksi database, dan variabel warna WPF.

| Modul | Fungsi |
|-------|--------|
| `mdlPub_ModulUmum.vb` | Utilities umum, variabel warna WPF (`WarnaXxx_WPF`) |
| `mdlPub_Enkripsi.vb` | Enkripsi/dekripsi AES-256 (3 key-IV pairs) |
| `mdlPub_KoneksiDatabase.vb` | Koneksi database public (MySQL) |
| `mdlPub_VariabelUmum.vb` | Konstanta umum (`Kosongan`, `Enter1Baris`, dll) |

| Topik | File Rules |
|-------|------------|
| Overview & Modul | `.claude/rules/Booku Library/overview.md` |

**Dokumentasi Khusus Project Booku Updater**

**Booku Updater** adalah aplikasi untuk mengupdate aplikasi Booku secara otomatis. Dijalankan terpisah dari aplikasi utama untuk menghindari konflik file saat proses update.

| Langkah | Proses |
|---------|--------|
| 1 | Ambil info versi dari database public |
| 2 | Download paket update (ZIP) dari server |
| 3 | Ekstrak dan replace file di folder Booku |
| 4 | Simpan versi baru ke file config |

| Status | Keterangan |
|--------|------------|
| Saat Ini | Sudah jalan (sederhana) |
| Rencana | Akan dikembangkan lebih lanjut |

| Topik | File Rules |
|-------|------------|
| Overview & Alur Kerja | `.claude/rules/Booku Updater/overview.md` |

**Dokumentasi Khusus Project Booku Backup**

**Booku Backup** adalah aplikasi untuk mem-backup folder solution BookuID secara otomatis. Digunakan oleh developer untuk membuat backup source code dan database.

| Fitur | Deskripsi |
|-------|-----------|
| Backup File Project | Kompres folder solution → Upload ke server |
| Backup Database | Backup database MySQL (dalam pengembangan) |

| Langkah Backup | Proses |
|----------------|--------|
| 1 | Hapus folder `.vs` dan backup lama |
| 2 | Kompres project ke ZIP |
| 3 | Copy ke Downloads |
| 4 | Upload ke server (opsional) |

| Topik | File Rules |
|-------|------------|
| Overview & Alur Kerja | `.claude/rules/Booku Backup/overview.md` |

**Dokumentasi Khusus Project Booku Encrypter**

**Booku Encrypter** adalah aplikasi utilitas untuk enkripsi dan dekripsi teks/data. Digunakan oleh developer untuk mengenkripsi string yang akan digunakan di kode sumber.

| Status | Keterangan |
|--------|------------|
| Saat Ini | **Skeleton / Placeholder** (belum diimplementasi) |
| Rencana | Tool enkripsi AES1/AES2/AES3 untuk developer |

| Fitur yang Direncanakan | Fungsi |
|-------------------------|--------|
| Enkripsi Teks | `EnkripsiTeksAES1/2/3()` |
| Dekripsi Teks | `DekripsiTeksAES1/2/3()` |
| Copy to Clipboard | Hasil siap paste ke kode |

| Topik | File Rules |
|-------|------------|
| Overview & Fitur | `.claude/rules/Booku Encrypter/overview.md` |

**Dokumentasi Khusus Project Booku Remote**

**Booku Remote** adalah aplikasi remote desktop untuk mengontrol PC lain dalam jaringan LAN maupun internet. Mendukung screen sharing dan kontrol keyboard/mouse jarak jauh.

| Fase | Deskripsi | Status |
|------|-----------|--------|
| Fase 1 | Discovery + Koneksi LAN | Selesai |
| Fase 2 | View-Only Screen Streaming | Selesai |
| Fase 2b | Kontrol Keyboard dan Mouse | Selesai |
| Fase 3 | Transfer Berkas | Belum dimulai |
| Fase 4 | Remote via Internet (NAT traversal, relay server) | Belum dimulai |

| Topik | File Rules |
|-------|------------|
| Overview, Arsitektur & Fase 4 Detail | `.claude/rules/Booku Remote/overview.md` |

**Dokumentasi Khusus Project Booku Remote Android**

**Booku Remote Android** adalah aplikasi Android berbasis .NET MAUI (C#) untuk remote desktop. Berfungsi sebagai **Tamu (client) saja** - menghubungkan ke Host yang menjalankan Booku Remote WPF di Windows.

| Aspek | Keterangan |
|-------|------------|
| Platform | Android (MAUI) |
| Bahasa | C# (bukan VB.NET) |
| Peran | Tamu only (tidak bisa jadi Host) |
| Protokol | Sama dengan Booku Remote WPF |

| Fitur | Deskripsi |
|-------|-----------|
| Discovery | Scan Host di jaringan LAN (UDP broadcast) |
| Streaming | Menerima frame layar dari Host |
| Touch Input | Gesture → Mouse/Keyboard (tap, drag, pinch) |

| Topik | File Rules |
|-------|------------|
| Overview, Struktur & Protokol | `.claude/rules/Booku Remote Android/overview.md` |

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
   - Lihat `.claude/rules/Booku/wpf-styling.md` untuk daftar lengkap warna

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
