# Booku Remote Android

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote Android** (folder `BookuID/Booku Remote Android/`)

## Deskripsi

**Booku Remote Android** adalah aplikasi Android berbasis .NET MAUI (C#) untuk remote desktop. Aplikasi ini berfungsi sebagai **Tamu (client) saja** - menghubungkan ke Host yang menjalankan Booku Remote WPF di Windows. Menggunakan protokol yang sama dengan Booku Remote WPF.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Remote Android |
| **Output** | `com.bookuid.remote-Signed.apk` |
| **Namespace** | `BookuRemoteAndroid` |
| **Target Framework** | .NET 8.0 Android |
| **Arsitektur** | .NET MAUI (C#) |
| **Entry Point** | `MauiProgram.cs` → `App.xaml` → `MainPage.xaml` |
| **Single Instance** | Ya (`LaunchMode.SingleTask`) |
| **Obfuscation** | IL Trimming + R8 Optimizer |

## Perbedaan dengan Booku Remote WPF

| Aspek | Booku Remote WPF | Booku Remote Android |
|-------|------------------|---------------------|
| **Platform** | Windows (WPF) | Android (MAUI) |
| **Bahasa** | VB.NET | C# |
| **Peran** | Host + Tamu | **Tamu saja** |
| **Input** | Mouse + Keyboard | Touch gestures |

## Struktur File

```
Booku Remote Android/
├── Booku Remote Android.csproj    # Project file (MAUI) + Release config
├── MauiProgram.cs                 # Entry point + DI registration
├── App.xaml / App.xaml.cs         # Application class
├── AppShell.xaml / AppShell.cs    # Navigation shell
├── MainPage.xaml / MainPage.cs    # Halaman utama (discovery + connect + manual IP)
├── LinkerConfig.xml               # IL Trimmer configuration
├── PUBLISH-RELEASE.bat            # Script build Release
│
├── Models/                        # Model classes
│   ├── Enums.cs                   # TipePaket, StatusKoneksi, dll
│   ├── PerangkatLAN.cs            # Model perangkat Host
│   ├── PaketData.cs               # Struktur paket dengan checksum
│   ├── FrameLayar.cs              # Frame layar (Base64 PNG)
│   └── InputData.cs               # Input keyboard/mouse + VK mapper
│
├── Services/                      # Business logic services
│   ├── ProtocolService.cs         # Serialisasi/deserialisasi JSON
│   ├── DiscoveryService.cs        # UDP broadcast discovery
│   ├── SessionService.cs          # State management sesi
│   └── NetworkService.cs          # Koneksi TCP + streaming (tanpa newline delimiter)
│
├── Views/                         # Additional pages
│   └── ViewerPage.xaml/.cs        # Viewer layar Host + touch input
│
├── Resources/
│   ├── AppIcon/                   # App icon (SVG)
│   ├── Splash/                    # Splash screen (SVG)
│   └── Styles/                    # Colors.xaml, Styles.xaml
│
└── Platforms/
    └── Android/
        ├── AndroidManifest.xml    # Permissions
        ├── MainActivity.cs        # Android activity (SingleTask)
        ├── MainApplication.cs     # Android application
        └── proguard.cfg           # R8/ProGuard rules
```

## Dependencies

| Package | Versi | Kegunaan |
|---------|-------|----------|
| **Microsoft.Maui.Controls** | 8.0.91 | MAUI framework |
| **System.Text.Json** | 8.0.5 | JSON serialization |

**Catatan:** Project ini **standalone** - tidak memiliki dependency ke project VB.NET lainnya dalam solution. Protokol diimplementasikan ulang dalam C#.

## Protokol (Sama dengan Booku Remote WPF)

### Catatan Penting Protokol

> **PENTING:** Protokol menggunakan JSON **TANPA newline delimiter**. Host (VB.NET) mengirim paket tanpa `\n` di akhir. Android harus parsing JSON dengan mendeteksi batas object `{}` menggunakan bracket tracking (bukan split by newline).

### Port

| Port | Protokol | Kegunaan |
|------|----------|----------|
| `45678` | UDP | Discovery broadcast |
| `45679` | TCP | Koneksi remote |

### Tipe Paket yang Digunakan (Tamu)

| Enum | Nilai | Kegunaan di Android |
|------|-------|---------------------|
| `BROADCAST_DISCOVERY` | 1 | Scan perangkat Host |
| `PERMINTAAN_KONEKSI` | 10 | Request koneksi ke Host |
| `TUTUP_KONEKSI` | 12 | Putuskan koneksi |
| `HEARTBEAT` | 13 | Keep-alive |
| `INPUT_KEYBOARD` | 21 | Kirim input keyboard |
| `INPUT_MOUSE` | 22 | Kirim input mouse |
| `PERMINTAAN_STREAMING` | 24 | Minta streaming layar |
| `HENTIKAN_STREAMING` | 25 | Stop streaming |

### Tipe Paket yang Diterima (dari Host)

| Enum | Nilai | Kegunaan |
|------|-------|----------|
| `RESPON_DISCOVERY` | 2 | Daftar Host tersedia |
| `RESPON_KONEKSI` | 11 | Hasil persetujuan koneksi |
| `FRAME_LAYAR` | 20 | Frame screenshot |

## Touch to Mouse Mapping

| Gesture Android | Aksi Mouse di Host |
|-----------------|-------------------|
| Tap | Left Click |
| Double Tap | Double Click |
| Long Press | Right Click |
| Pan/Drag | Mouse Move |
| Pinch In/Out | Scroll Wheel |

## Alur Kerja

```
┌─────────────────────────────────────────────────────────────────┐
│  1. DISCOVERY                                                   │
│     ├── User buka app, tekan "Scan"                             │
│     ├── Kirim UDP broadcast ke port 45678                       │
│     └── Tampilkan daftar Host yang merespon                     │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. KONEKSI                                                     │
│     ├── User pilih Host, tekan "Sambungkan"                     │
│     ├── Kirim PERMINTAAN_KONEKSI via TCP port 45679             │
│     ├── Tunggu persetujuan dari Host                            │
│     └── Jika disetujui, navigate ke ViewerPage                  │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. STREAMING                                                   │
│     ├── Kirim PERMINTAAN_STREAMING                              │
│     ├── Terima FRAME_LAYAR (PNG Base64)                         │
│     ├── Decode dan tampilkan di Image control                   │
│     └── Maintain heartbeat setiap 5 detik                       │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  4. INPUT (Jika diizinkan Host)                                 │
│     ├── Capture touch gestures                                  │
│     ├── Convert ke INPUT_KEYBOARD / INPUT_MOUSE                 │
│     ├── Kirim dengan koordinat normalized (0.0-1.0)             │
│     └── Mouse throttling: minimal 30ms antar event              │
└─────────────────────────────────────────────────────────────────┘
```

## Permissions Android

```xml
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.CHANGE_WIFI_MULTICAST_STATE" />
<uses-permission android:name="android.permission.WAKE_LOCK" />
```

## Build dan Test

### Prerequisites

1. Visual Studio 2022 dengan workload ".NET Multi-platform App UI development"
2. Android SDK (otomatis terinstall dengan workload)
3. Android Emulator atau device fisik

### Build Debug

```bash
# Dari command line
dotnet build "Booku Remote Android/Booku Remote Android.csproj" -c Debug

# Atau dari Visual Studio: Build → Build Solution
```

### Build Release (Recommended)

```bash
# Menggunakan script batch (recommended)
PUBLISH-RELEASE.bat

# Atau dari command line
dotnet build "Booku Remote Android/Booku Remote Android.csproj" -c Release
```

**Output Release:**
```
bin/Release/net8.0-android/com.bookuid.remote-Signed.apk  (±28 MB)
```

### Test

1. Jalankan **Booku Remote WPF** di Windows sebagai Host
2. Jalankan **Booku Remote Android** di emulator/device
3. Pastikan keduanya di jaringan yang sama (LAN)
4. **Untuk Emulator:** Gunakan IP `10.0.2.2` (manual input) untuk terhubung ke Host di PC yang sama
5. **Untuk Device Fisik:** Tekan "Scan" untuk menemukan Host
6. Sambungkan dan test streaming + kontrol

## Status Pengembangan

| Komponen | Status | Catatan |
|----------|--------|---------|
| Project Structure | Selesai | Semua file dasar dibuat |
| Discovery Service | Selesai | UDP broadcast |
| Network Service | Selesai | TCP + streaming (no newline delimiter) |
| Protocol Service | Selesai | JSON serialization (bracket tracking) |
| Touch Input | Selesai | Gesture recognition |
| Manual IP Input | Selesai | Untuk emulator (10.0.2.2) |
| Single Instance | Selesai | `LaunchMode.SingleTask` |
| Obfuscation | Selesai | IL Trimming + R8 |
| Release Build | Selesai | `PUBLISH-RELEASE.bat` |
| UI/UX | Dasar | Perlu polish |
| Testing | Ongoing | Koneksi + streaming tested |

## Release Configuration

### Single Instance
- `MainActivity.cs`: `LaunchMode = LaunchMode.SingleTask`
- Mencegah multiple instance aplikasi berjalan bersamaan

### Obfuscation (.csproj Release)
```xml
<PublishTrimmed>true</PublishTrimmed>
<TrimMode>link</TrimMode>
<AndroidLinkTool>r8</AndroidLinkTool>
```
- **IL Trimming**: Menghapus kode tidak terpakai, menyulitkan reverse engineering
- **R8 Optimizer**: Obfuscation untuk Java/Kotlin code
- **LinkerConfig.xml**: Preserve Models dan Services dari trimming
- **proguard.cfg**: R8 rules untuk MAUI

### Single Executable
```xml
<EmbedAssembliesIntoApk>true</EmbedAssembliesIntoApk>
<AndroidPackageFormat>apk</AndroidPackageFormat>
```
- Semua assembly ter-embed dalam satu file APK

## Aturan Pengembangan

1. **Bahasa**: C# (bukan VB.NET - MAUI tidak support VB.NET)
2. **Namespace**: `BookuRemoteAndroid`
3. **Protokol**: Harus kompatibel 100% dengan Booku Remote WPF (tanpa newline delimiter)
4. **Tamu Only**: Tidak ada fitur Host di Android
5. **Dependency**: Jangan reference project VB.NET lainnya
6. **JSON Parsing**: Gunakan bracket tracking, bukan split by newline

## Dokumentasi Terkait

| Topik | File |
|-------|------|
| Protokol lengkap | `.claude/rules/Booku Remote/overview.md` |
| Solution overview | `.claude/rules/modules-dependencies.md` |
