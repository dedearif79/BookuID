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
├── MainPage.xaml / MainPage.cs    # Halaman utama (LAN/Internet mode, discovery, HostCode input)
├── LinkerConfig.xml               # IL Trimmer configuration
├── PUBLISH-RELEASE.bat            # Script build Release
│
├── Models/                        # Model classes
│   ├── Enums.cs                   # TipePaket, StatusKoneksi, dll
│   ├── PerangkatLAN.cs            # Model perangkat Host
│   ├── PaketData.cs               # Struktur paket dengan checksum
│   ├── FrameLayar.cs              # Frame layar (Base64 PNG)
│   ├── InputData.cs               # Input keyboard/mouse + VK mapper
│   └── PortSettings.cs            # Model pengaturan port (dengan default values)
│
├── Services/                      # Business logic services
│   ├── ProtocolService.cs         # Serialisasi/deserialisasi JSON
│   ├── DiscoveryService.cs        # UDP broadcast discovery
│   ├── SessionService.cs          # State management sesi
│   ├── SettingsService.cs         # Konfigurasi port (MAUI Preferences API)
│   ├── NetworkService.cs          # Koneksi TCP + streaming (tanpa newline delimiter)
│   ├── UdpStreamingService.cs     # UDP video receiver + frame reassembly + codec routing
│   └── H264DecoderService.cs      # Android MediaCodec H.264 decoder
│
├── Views/                         # Additional pages
│   ├── ViewerPage.xaml/.cs        # Viewer layar Host + touch input
│   └── SettingsPage.xaml/.cs      # Halaman pengaturan port
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

### Arsitektur Hybrid TCP/UDP

Sama dengan Booku Remote WPF, Android client menggunakan arsitektur **hybrid TCP/UDP**:

| Layer | Protokol | Kegunaan |
|-------|----------|----------|
| **Control Plane** | TCP | Login, input touch, heartbeat |
| **Data Plane** | UDP | Video frames (high-throughput, low-latency) |

### Port

| Port | Protokol | Lokasi | Kegunaan |
|------|----------|--------|----------|
| `45678` | UDP | LAN | Discovery broadcast |
| `45679` | TCP | LAN | Koneksi langsung (control plane) |
| `45680` | TCP | VPS | Relay Server (koneksi via internet) |
| `45681` | **UDP** | LAN/Relay | **Video streaming** (data plane) |

> **Catatan:** Semua port di atas adalah nilai **default** dan dapat dikonfigurasi melalui halaman Settings (⚙️ di toolbar). Settings disimpan menggunakan MAUI Preferences API.

### Tipe Paket yang Digunakan (Tamu)

**Mode LAN:**

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

**Mode Internet (Relay):**

| Enum | Nilai | Kegunaan di Android |
|------|-------|---------------------|
| `RELAY_QUERY_HOST` | 45 | Query ketersediaan Host via HostCode |
| `RELAY_CONNECT_REQUEST` | 47 | Request koneksi via relay |

### Tipe Paket yang Diterima (dari Host/Relay)

| Enum | Nilai | Kegunaan |
|------|-------|----------|
| `RESPON_DISCOVERY` | 2 | Daftar Host tersedia (LAN) |
| `RESPON_KONEKSI` | 11 | Hasil persetujuan koneksi |
| `FRAME_LAYAR` | 20 | Frame screenshot |
| `RELAY_QUERY_HOST_RESULT` | 46 | Info Host dari query (Internet) |

## Konfigurasi Port

Port jaringan dapat dikonfigurasi melalui halaman Settings yang dapat diakses via tombol ⚙️ di toolbar MainPage.

### Komponen

| File | Deskripsi |
|------|-----------|
| `Models/PortSettings.cs` | Model class dengan default values |
| `Services/SettingsService.cs` | Singleton service untuk load/save via MAUI Preferences API |
| `Views/SettingsPage.xaml/.cs` | UI halaman settings |

### Port yang Dapat Dikonfigurasi

| Port | Default | Deskripsi |
|------|---------|-----------|
| `PortDiscovery` | 45678 | UDP broadcast untuk discovery LAN |
| `PortKoneksi` | 45679 | TCP koneksi remote LAN (control plane) |
| `PortRelay` | 45680 | TCP koneksi via relay server |
| `PortUdpVideo` | 45681 | **UDP video streaming** (data plane) |
| `RelayServerIP` | 155.117.43.209 | Alamat IP relay server |

### Cara Kerja

1. **Startup:** `SettingsService.Instance` otomatis load settings dari MAUI Preferences
2. **Akses:** Gunakan `SettingsService.Current` untuk mendapatkan `PortSettings` aktif
3. **UI:** User navigasi ke SettingsPage via toolbar button (⚙️)
4. **Simpan:** Perubahan disimpan via `SettingsService.Instance.SaveSettings()`
5. **Reset:** Tombol "Reset Default" mengembalikan ke nilai default

## Touch to Mouse Mapping

| Gesture Android | Aksi Mouse di Host |
|-----------------|-------------------|
| Tap | Left Click |
| Double Tap | Double Click |
| Long Press | Right Click |
| Pan/Drag | Mouse Move |
| Pinch In/Out | Scroll Wheel |

## UDP Streaming (UdpStreamingService)

Service untuk menerima video frame via UDP dengan frame reassembly dan codec routing.

### Komponen

| Class | Deskripsi |
|-------|-----------|
| `UdpStreamingService` | UDP receiver dengan frame reassembly + codec detection |
| `FrameAssemblyInfo` | Info untuk assembly frame dari chunks |
| `UdpFrameEventArgs` | Event args untuk frame yang diterima (+ CodecType) |
| `UdpStatisticsEventArgs` | Event args untuk statistik UDP |

### Konstanta

| Konstanta | Nilai | Deskripsi |
|-----------|-------|-----------|
| `UDP_HEADER_SIZE` | 16 bytes | Ukuran header UDP packet |
| `CODEC_TYPE_SIZE` | 1 byte | Ukuran codec type di payload |
| `CODEC_TYPE_JPEG` | 0x00 | Codec type JPEG |
| `CODEC_TYPE_H264` | 0x01 | Codec type H.264 |
| `FRAME_TIMEOUT_MS` | 100 ms | Timeout untuk frame assembly |
| `MAX_PENDING_FRAMES` | 10 | Maksimum concurrent frames yang di-track |

### Events

| Event | Deskripsi |
|-------|-----------|
| `FrameReceived` | Raised ketika frame lengkap diterima (dengan CodecType) |
| `StatisticsUpdated` | Raised setiap 1 detik dengan info FPS |

### Cara Kerja

1. **Start Receiver:** `StartReceiver(sessionId)` - bind ke UDP port
2. **Registration (Relay):** `SendRegistrationAsync()` - kirim header-only packet ke relay
3. **Receive Loop:** Parse header, filter by SessionId, extract CodecType, add chunk ke assembler
4. **Assembly:** Ketika semua chunks diterima, assemble dan raise `FrameReceived`
5. **Codec Routing:** `UdpFrameEventArgs.CodecType` menentukan apakah JPEG atau H.264
6. **Timeout:** Chunks yang tidak lengkap dalam 100ms di-drop

### Static Method: GenerateSessionId()

Method `UdpStreamingService.GenerateSessionId(string)` menggunakan **djb2 hash algorithm** untuk menghasilkan SessionId dari KunciSesi.

> **PENTING:** Algoritma ini **HARUS** sama dengan Host (VB.NET) dan Relay Server (C#). Jangan gunakan `GetHashCode()` karena hasilnya tidak konsisten antar platform. Lihat dokumentasi Relay untuk detail algoritma.

## H.264 Decoding (H264DecoderService)

Service untuk decode H.264 video stream menggunakan Android MediaCodec.

### Komponen

| Class | Deskripsi |
|-------|-----------|
| `H264DecoderService` | Android MediaCodec H.264 decoder |
| `DecodedFrameEventArgs` | Event args untuk frame decoded (JPEG bytes) |
| `DecoderErrorEventArgs` | Event args untuk error decoder |

### Cara Kerja

1. **Start:** `Start(width, height)` - initialize MediaCodec dengan estimasi resolusi
2. **Send Data:** `SendData(h264Data)` atau `ProcessH264Data(h264Data)` - kirim NAL unit ke decoder
3. **Output Loop:** Background task membaca output buffer, convert YUV → JPEG
4. **Format Changed:** MediaCodec otomatis detect resolusi dari SPS, raise `OUTPUT_FORMAT_CHANGED`
5. **Frame Decoded:** Raise `FrameDecoded` dengan JPEG bytes

### Dynamic Resolution

MediaCodec akan auto-detect resolusi dari SPS (Sequence Parameter Set) yang dikirim Host bersama keyframe. ViewerPage tidak perlu tahu resolusi exact saat start.

```csharp
// Dalam OutputLoopAsync():
if (outputBufferIndex == (int)MediaCodecInfoState.OutputFormatChanged)
{
    var newFormat = _decoder.OutputFormat;
    _width = newFormat.GetInteger(MediaFormat.KeyWidth);
    _height = newFormat.GetInteger(MediaFormat.KeyHeight);
}
```

### Integrasi di ViewerPage

```csharp
// Auto-start decoder saat frame H.264 pertama diterima
private void OnUdpFrameReceived(object? sender, UdpFrameEventArgs e)
{
    if (e.IsH264)
    {
        if (!_h264Decoder.IsRunning)
        {
            _h264Decoder.Start(768, 432); // Estimasi awal
        }
        _h264Decoder.ProcessH264Data(e.FrameData);
    }
    else // JPEG
    {
        RenderJpegFrame(e.FrameData);
    }
}
```

## Rendering (ViewerPage)

Menggunakan pendekatan **single Image** untuk menampilkan frame dengan smooth:

| Komponen | Fungsi |
|----------|--------|
| `imgScreenA` | Image utama untuk menampilkan frame |

**Alur Rendering:**
1. Frame diterima dari UDP, byte array di-copy untuk menghindari race condition
2. `ImageSource.FromStream()` langsung di-set ke `imgScreenA`
3. MAUI menangani transisi gambar secara internal

> **Catatan:** Pendekatan double buffering dengan visibility swap sebelumnya justru menyebabkan flicker karena swap terjadi sebelum image selesai loading. Single Image approach lebih smooth karena MAUI sudah optimize rendering.

## Alur Kerja

### Mode LAN

```
┌─────────────────────────────────────────────────────────────────┐
│  1. DISCOVERY                                                   │
│     ├── User buka app, pilih "Mode LAN"                         │
│     ├── Tekan "Scan", kirim UDP broadcast ke port 45678         │
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
```

### Mode Internet (Relay)

```
┌─────────────────────────────────────────────────────────────────┐
│  1. QUERY HOST                                                  │
│     ├── User buka app, pilih "Mode Internet"                    │
│     ├── Input HostCode 6 karakter                               │
│     ├── Kirim RELAY_QUERY_HOST ke relay (155.117.43.209:45680) │
│     └── Tampilkan info Host (nama, status password)             │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. KONEKSI VIA RELAY                                           │
│     ├── User tekan "Sambung"                                    │
│     ├── Input password jika diperlukan                          │
│     ├── Kirim RELAY_CONNECT_REQUEST ke relay                    │
│     ├── Relay forward ke Host, tunggu persetujuan               │
│     └── Jika disetujui, navigate ke ViewerPage                  │
└─────────────────────────────────────────────────────────────────┘
```

### Streaming & Input (Setelah Terhubung)

```
┌─────────────────────────────────────────────────────────────────┐
│  3. STREAMING                                                   │
│     ├── Kirim PERMINTAAN_STREAMING                              │
│     ├── Terima FRAME_LAYAR (JPEG Base64)                        │
│     ├── Decode dan tampilkan ke Image control                   │
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

**Mode LAN:**
1. Jalankan **Booku Remote WPF** di Windows sebagai Host (pilih Mode LAN)
2. Jalankan **Booku Remote Android** di emulator/device
3. Pastikan keduanya di jaringan yang sama (LAN)
4. **Untuk Emulator:** Gunakan IP `10.0.2.2` (manual input) untuk terhubung ke Host di PC yang sama
5. **Untuk Device Fisik:** Pilih "Mode LAN", tekan "Scan" untuk menemukan Host
6. Sambungkan dan test streaming + kontrol

**Mode Internet:**
1. Jalankan **Booku Remote Relay** di VPS (155.117.43.209)
2. Jalankan **Booku Remote WPF** di Windows sebagai Host (pilih Mode Internet)
3. Catat HostCode yang ditampilkan (6 karakter)
4. Jalankan **Booku Remote Android**, pilih "Mode Internet"
5. Input HostCode, tunggu query berhasil
6. Tekan "Sambung", masukkan password jika diperlukan
7. Setelah disetujui Host, test streaming + kontrol

## Status Pengembangan

| Komponen | Status | Catatan |
|----------|--------|---------|
| Project Structure | Selesai | Semua file dasar dibuat |
| Discovery Service (LAN) | Selesai | UDP broadcast |
| Network Service (LAN) | Selesai | TCP + streaming (no newline delimiter) |
| Network Service (Relay) | Selesai | Koneksi via relay server |
| Protocol Service | Selesai | JSON serialization (bracket tracking) |
| **UDP Streaming Service (JPEG)** | **Selesai** | **Port 45681, frame reassembly** |
| **UDP Streaming Service (H.264)** | **Selesai** | **CodecType routing, auto-start decoder** |
| **H.264 Decoder Service** | **⚠️ Issue** | **Android MediaCodec works, tapi ViewerPage crash "recycled bitmap"** |
| Touch Input | Selesai | Gesture recognition |
| Manual IP Input | Selesai | Untuk emulator (10.0.2.2) |
| Mode Selector (LAN/Internet) | Selesai | UI untuk pilih mode koneksi |
| HostCode Input | Selesai | Input 6 karakter + auto-query |
| **Port Settings** | Selesai | SettingsPage, MAUI Preferences API |
| Single Instance | Selesai | `LaunchMode.SingleTask` |
| Obfuscation | Selesai | IL Trimming + R8 |
| Release Build | Selesai | `PUBLISH-RELEASE.bat` |
| UI/UX | Dasar | Perlu polish |
| Testing | Ongoing | LAN + Internet + H.264 testing |

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
