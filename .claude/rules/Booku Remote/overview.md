# Booku Remote

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Deskripsi

**Booku Remote** adalah aplikasi remote desktop berbasis WPF untuk mengontrol PC lain dalam jaringan LAN maupun internet. Aplikasi ini memungkinkan screen sharing dan kontrol keyboard/mouse jarak jauh.

> **Status:** Fase 1-4 telah selesai diimplementasi. Aplikasi mendukung remote desktop via LAN dan Internet (melalui Relay Server).

## Quick Reference

| Topik | File |
|-------|------|
| Arsitektur Jaringan, Port, Protokol | `network-protocol.md` |
| Alur Kerja (Fase 1-4) | `workflow.md` |
| Komponen Utama (Modul VB) | `components.md` |
| H.264 Streaming | `h264-streaming.md` |
| Window dan UI | `ui-windows.md` |
| Developer Guide (Mode, Debugging) | `developer-guide.md` |

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Remote |
| **Output** | `Booku Remote.exe` (WinExe) |
| **Namespace** | `Booku_Remote` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF + TCP/UDP Networking |
| **Entry Point** | `Application.xaml` → `wpfWin_StartUp.xaml` |
| **Single Instance** | Kondisional (Release mode only) |
| **Mode Developer** | Otomatis berdasarkan path eksekusi |

## Struktur File

```
Booku Remote/
├── Booku Remote.vbproj           # Project file
├── Application.xaml              # Application entry point
├── Application.xaml.vb           # Application code-behind
├── AssemblyInfo.vb               # Assembly information
│
├── Kelas/                        # Class files
│   ├── cls_PerangkatLAN.vb       # Model perangkat di LAN
│   ├── cls_FrameLayar.vb         # Model frame layar (screenshot)
│   ├── cls_SesiRemote.vb         # State management sesi remote
│   ├── cls_PaketData.vb          # Payload classes untuk protokol
│   ├── cls_SetelPort.vb          # Konfigurasi port (load/save JSON)
│   ├── cls_UdpPacket.vb          # UDP packet model + chunking/reassembly
│   ├── cls_H264Encoder.vb        # FFmpeg H.264 encoder wrapper (Host)
│   ├── cls_H264Decoder.vb        # FFmpeg H.264 decoder wrapper (Tamu)
│   ├── cls_NalParser.vb          # NAL unit parser untuk H.264 stream
│   └── cls_TransferBerkas.vb     # State management transfer berkas (Fase 3b)
│
├── Modul/                        # Module files
│   ├── mdl_VariabelUmum.vb       # Variabel global, enum, konstanta, file logging
│   ├── mdl_PenemuanPerangkat.vb  # Discovery perangkat di LAN (UDP)
│   ├── mdl_KoneksiJaringan.vb    # Koneksi TCP dan streaming (LAN)
│   ├── mdl_KoneksiRelay.vb       # Koneksi via Relay Server (Internet)
│   ├── mdl_UdpStreaming.vb       # UDP video streaming (sender/receiver) + H.264 integration
│   ├── mdl_FFmpegManager.vb      # FFmpeg process lifecycle management
│   ├── mdl_Protokol.vb           # Serialisasi/deserialisasi paket
│   ├── mdl_TangkapLayar.vb       # Screen capture
│   ├── mdl_InjeksiInput.vb       # Keyboard/mouse injection (SendInput API)
│   ├── mdl_Clipboard.vb          # Clipboard sync bidirectional (Fase 3a)
│   └── mdl_TransferBerkas.vb     # Transfer berkas logic (Fase 3b)
│
└── Windows/                      # WPF Windows
    ├── wpfWin_StartUp.xaml       # Main window (pilih mode Host/Tamu)
    ├── wpfWin_ModeHost.xaml      # Host window (menunggu koneksi)
    ├── wpfWin_ModeTamu.xaml      # Tamu window (scan & connect)
    ├── wpfWin_PersetujuanKoneksi.xaml  # Dialog persetujuan di Host
    ├── wpfWin_Viewer.xaml        # Viewer layar Host di Tamu
    ├── wpfWin_FileBrowser.xaml   # File browser dialog (Fase 3b)
    └── wpfWin_TransferProgress.xaml  # Transfer progress dialog (Fase 3b)
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke `bcomm.dll` (utilities) |
| **Booku Styles** | Project Reference | XAML styling (`BookuID.Styles.dll`) |
| **Obfuscar** | NuGet Package | Code obfuscation (Release build) |

## Fase Pengembangan

| Fase | Deskripsi | Status |
|------|-----------|--------|
| **Fase 1** | Discovery + Koneksi LAN | Selesai |
| **Fase 2** | View-Only Screen Streaming | Selesai |
| **Fase 2b** | Kontrol Keyboard dan Mouse | Selesai |
| **Fase 3a** | Clipboard Sync (Bidirectional) | Selesai |
| **Fase 3b** | Transfer Berkas (Bidirectional) | Selesai |
| **Fase 4** | Remote via Internet (Relay Server) | Selesai |

## Port Default

| Port | Protokol | Kegunaan |
|------|----------|----------|
| `45678` | UDP | Discovery perangkat (broadcast) |
| `45679` | TCP | Koneksi langsung (control plane) |
| `45680` | TCP | Relay Server (koneksi via internet) |
| `45681` | UDP | Video streaming (data plane) |

> **Detail lengkap:** Lihat `network-protocol.md`

## Versi Android

Terdapat versi Android dari aplikasi ini yang berfungsi sebagai **Tamu saja** (tidak bisa jadi Host):

| Project | Deskripsi |
|---------|-----------|
| **Booku Remote Android** | Aplikasi Android (MAUI/C#) untuk remote desktop sebagai Tamu |

Versi Android menggunakan protokol yang **100% kompatibel** dengan Booku Remote WPF sehingga dapat terhubung ke Host yang menjalankan versi WPF.

> Lihat dokumentasi lengkap di `.claude/rules/Booku Remote Android/overview.md`

## Dokumentasi Terkait

| Topik | File |
|-------|------|
| **Booku Remote Android** | `.claude/rules/Booku Remote Android/overview.md` |
| **Booku Remote Relay** | `.claude/rules/Booku Remote Relay/overview.md` |
| Testing Guide Fase 2b | `/Booku Remote/TESTING_FASE_2B.md` |
