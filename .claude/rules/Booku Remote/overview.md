# Booku Remote

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Deskripsi

**Booku Remote** adalah aplikasi remote desktop berbasis WPF untuk mengontrol PC lain dalam jaringan LAN maupun internet. Aplikasi ini memungkinkan screen sharing dan kontrol keyboard/mouse jarak jauh.

> **Status:** Fase 1-4 telah selesai diimplementasi. Aplikasi mendukung remote desktop via LAN dan Internet (melalui Relay Server).

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
│   └── cls_NalParser.vb          # NAL unit parser untuk H.264 stream
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
│   └── mdl_InjeksiInput.vb       # Keyboard/mouse injection (SendInput API)
│
└── Windows/                      # WPF Windows
    ├── wpfWin_StartUp.xaml       # Main window (pilih mode Host/Tamu)
    ├── wpfWin_ModeHost.xaml      # Host window (menunggu koneksi)
    ├── wpfWin_ModeTamu.xaml      # Tamu window (scan & connect)
    ├── wpfWin_PersetujuanKoneksi.xaml  # Dialog persetujuan di Host
    └── wpfWin_Viewer.xaml        # Viewer layar Host di Tamu
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
| **Fase 3** | Transfer Berkas | Belum dimulai |
| **Fase 4** | Remote via Internet (Relay Server) | Selesai |

## Arsitektur Jaringan

### Arsitektur Hybrid TCP/UDP

Booku Remote menggunakan arsitektur **hybrid TCP/UDP** untuk performa optimal:

| Layer | Protokol | Kegunaan |
|-------|----------|----------|
| **Control Plane** | TCP | Login, input keyboard/mouse, heartbeat |
| **Data Plane** | UDP | Video frames (high-throughput, low-latency) |

### Port yang Digunakan

| Port | Protokol | Lokasi | Kegunaan |
|------|----------|--------|----------|
| `45678` | UDP | LAN | Discovery perangkat (broadcast) |
| `45679` | TCP | LAN | Koneksi langsung (control plane) |
| `45680` | TCP | VPS | Relay Server (koneksi via internet) |
| `45681` | **UDP** | LAN/Relay | **Video streaming** (data plane) |

> **Catatan:** Semua port di atas adalah nilai **default** dan dapat dikonfigurasi manual melalui UI di window Host atau Tamu. Settings disimpan ke file JSON di `%AppData%\BookuID\Booku Remote\port-settings.json`.

### UDP Packet Structure (Video Streaming)

```
┌────────────────────────────────────────────────────────────┐
│  UDP Packet (max 1216 bytes)                               │
├────────────────────────────────────────────────────────────┤
│  Header (16 bytes)                                         │
│  ├── SessionId     (4 bytes) - Routing key                 │
│  ├── FrameId       (4 bytes) - Frame sequence number       │
│  ├── ChunkIndex    (2 bytes) - Chunk number (0-based)      │
│  ├── ChunkCount    (2 bytes) - Total chunks in frame       │
│  └── TimestampMs   (4 bytes) - Capture timestamp           │
├────────────────────────────────────────────────────────────┤
│  Payload (max 1200 bytes)                                  │
│  ├── CodecType     (1 byte)  - 0x00=JPEG, 0x01=H.264       │
│  └── VideoData     (max 1199 bytes) - JPEG/H.264 chunk     │
└────────────────────────────────────────────────────────────┘
```

**Video Codec Support:**
| Codec | CodecType | Keterangan |
|-------|-----------|------------|
| **JPEG** | `0x00` | Default, backward compatible |
| **H.264** | `0x01` | Low-latency encoding via FFmpeg |

**Keunggulan UDP Streaming:**
- **Latency rendah** - Tidak ada TCP acknowledgment overhead
- **Frame dropping** - Timeout 100ms, frame lama diabaikan
- **MTU-safe** - Chunk size 1200 bytes menghindari fragmentasi
- **Multi-codec** - Support JPEG dan H.264
- **Backward compatible** - JPEG fallback jika FFmpeg tidak tersedia

### Tipe Paket (Protokol)

```vb
Public Enum TipePaket
    ' Discovery (1-9)
    BROADCAST_DISCOVERY = 1
    RESPON_DISCOVERY = 2

    ' Koneksi (10-19)
    PERMINTAAN_KONEKSI = 10
    RESPON_KONEKSI = 11
    TUTUP_KONEKSI = 12
    HEARTBEAT = 13

    ' Remote Desktop (20-29)
    FRAME_LAYAR = 20
    INPUT_KEYBOARD = 21
    INPUT_MOUSE = 22
    CLIPBOARD_DATA = 23
    PERMINTAAN_STREAMING = 24
    HENTIKAN_STREAMING = 25

    ' Transfer Berkas (30-39)
    PERMINTAAN_BERKAS = 30
    DATA_BERKAS = 31
    KONFIRMASI_BERKAS = 32
    DAFTAR_FOLDER = 33

    ' Relay Server (40-59)
    RELAY_REGISTER_HOST = 40      ' Host → Relay: register
    RELAY_REGISTER_HOST_OK = 41   ' Relay → Host: confirm + HostCode
    RELAY_UNREGISTER_HOST = 42    ' Host → Relay: unregister
    RELAY_HOST_HEARTBEAT = 43     ' Host → Relay: keep-alive
    RELAY_QUERY_HOST = 45         ' Tamu → Relay: cari Host by code
    RELAY_QUERY_HOST_RESULT = 46  ' Relay → Tamu: info Host
    RELAY_CONNECT_REQUEST = 47    ' Tamu → Relay: minta koneksi
    RELAY_SESSION_STARTED = 52    ' Notify session dimulai
    RELAY_SESSION_ENDED = 53      ' Notify session berakhir
    RELAY_ERROR = 55              ' Generic error
    RELAY_HOST_OFFLINE = 56       ' Host tidak online
    RELAY_INVALID_CODE = 57       ' HostCode tidak valid
End Enum
```

## Alur Kerja

### Fase 1: Discovery dan Koneksi

```
┌──────────────────────────────────────────────────────────────────┐
│  TAMU                                                            │
│  ┌─────────────┐    UDP Broadcast    ┌─────────────┐             │
│  │ Scan Devices│ ──────────────────► │ All Hosts   │             │
│  │ (Port 45678)│                     │ in LAN      │             │
│  └─────────────┘                     └─────────────┘             │
│         ▲                                   │                    │
│         │         UDP Response              │                    │
│         └───────────────────────────────────┘                    │
└──────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌──────────────────────────────────────────────────────────────────┐
│  KONEKSI TCP                                                     │
│  ┌─────────────┐    PERMINTAAN_KONEKSI   ┌─────────────┐         │
│  │ Tamu        │ ──────────────────────► │ Host        │         │
│  │             │                         │             │         │
│  │             │ ◄────────────────────── │ Dialog      │         │
│  │             │    RESPON_KONEKSI       │ Persetujuan │         │
│  └─────────────┘    (Terima/Tolak)       └─────────────┘         │
└──────────────────────────────────────────────────────────────────┘
```

### Fase 2: Screen Streaming

```
┌──────────────────────────────────────────────────────────────────┐
│  HOST (Screen Capture)                                           │
│  ┌─────────────┐                     ┌─────────────┐             │
│  │ mdl_Tangkap │ ──► Screenshot ──►  │ Kompres     │             │
│  │ Layar       │     (Bitmap)        │ (JPEG)      │             │
│  └─────────────┘                     └─────────────┘             │
│                                            │                     │
│                              FRAME_LAYAR (TipePaket=20)          │
└────────────────────────────────────────────┼─────────────────────┘
                                             │ TCP Port 45679
                                             ▼
┌────────────────────────────────────────────┼─────────────────────┐
│  TAMU (Viewer)                             │                     │
│                                   ┌────────┴────────┐            │
│  ┌─────────────┐                  │ Decompress      │            │
│  │ wpfWin_     │ ◄── BitmapImage ◄│ (JPEG→Bitmap)   │            │
│  │ Viewer      │                  └─────────────────┘            │
│  └─────────────┘                                                 │
└──────────────────────────────────────────────────────────────────┘
```

### Fase 2b: Kontrol Keyboard/Mouse

```
┌──────────────────────────────────────────────────────────────────┐
│  TAMU (Remote Controller)                                        │
│  ┌─────────────────┐     ┌─────────────────┐                     │
│  │ wpfWin_Viewer   │ ──► │ Capture Events  │                     │
│  │ (KeyDown/Mouse) │     │ (WPF Events)    │                     │
│  └─────────────────┘     └────────┬────────┘                     │
│                                   │                              │
│                          KirimPaketAsync()                       │
│                        (INPUT_KEYBOARD/MOUSE)                    │
└───────────────────────────────────┼──────────────────────────────┘
                                    │ TCP Port 45679
                                    ▼
┌───────────────────────────────────┼──────────────────────────────┐
│  HOST (Receives Input)            │                              │
│                          PaketDiterima event                     │
│                                   │                              │
│  ┌─────────────────┐     ┌───────┴─────────┐                     │
│  │ mdl_InjeksiInput│ ◄── │ DeserializeInput│                     │
│  │ (SendInput API) │     │ (JSON→Object)   │                     │
│  └─────────────────┘     └─────────────────┘                     │
└──────────────────────────────────────────────────────────────────┘
```

### Fase 4: Remote via Internet (Relay Server)

**Arsitektur: Relay-Only**

Implementasi menggunakan arsitektur **Relay-Only** di mana semua traffic Host-Tamu melewati Relay Server di VPS. Dipilih karena:
- Sederhana dan reliable
- Tidak perlu NAT traversal kompleks
- Kontrol penuh atas infrastruktur

**VPS:** Windows 11 Pro @ 155.117.43.209 (2 CPU, 4GB RAM, 100Mbps)

```
┌─────────────────────────────────────────────────────────────┐
│              RELAY SERVER (VPS)                             │
│              155.117.43.209:45680                            │
│                                                             │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────┐ │
│  │ Connection  │  │   Session   │  │   Packet Router     │ │
│  │ Manager     │  │   Manager   │  │   (transparent)     │ │
│  └─────────────┘  └─────────────┘  └─────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
         ▲                                    ▲
         │ TCP 45680                         │ TCP 45680
         │                                    │
┌────────┴────────┐                ┌─────────┴─────────┐
│      HOST       │                │       TAMU        │
│  Register →     │                │  Input HostCode → │
│  Get HostCode   │                │  Connect via Relay│
└─────────────────┘                └───────────────────┘
```

**Alur Host Register ke Relay:**

```
HOST                           RELAY
 │ TCP Connect                   │
 ├──────────────────────────────►│
 │ RELAY_REGISTER_HOST           │
 │ {NamaPerangkat, Password?}    │
 ├──────────────────────────────►│
 │                               │ Generate HostCode "XY7K2M"
 │ RELAY_REGISTER_HOST_OK        │
 │ {HostCode: "XY7K2M"}          │
 │◄──────────────────────────────┤
 │                               │
 │ [Tampilkan HostCode di UI]    │
 │                               │
 │ RELAY_HOST_HEARTBEAT (30s)    │
 ├──────────────────────────────►│ (repeat)
```

**Alur Tamu Connect via Relay:**

```
TAMU                    RELAY                    HOST
 │ TCP Connect            │                        │
 ├───────────────────────►│                        │
 │ RELAY_QUERY_HOST       │                        │
 │ {HostCode: "XY7K2M"}   │                        │
 ├───────────────────────►│                        │
 │ RELAY_QUERY_HOST_RESULT│                        │
 │ {Found, NamaHost}      │                        │
 │◄───────────────────────┤                        │
 │                        │                        │
 │ RELAY_CONNECT_REQUEST  │                        │
 │ {HostCode, NamaTamu}   │                        │
 ├───────────────────────►│ PERMINTAAN_KONEKSI    │
 │                        ├───────────────────────►│
 │                        │                        │ [Dialog]
 │                        │ RESPON_KONEKSI         │
 │                        │◄───────────────────────┤
 │ RESPON_KONEKSI         │                        │
 │◄───────────────────────┤                        │
 │                        │                        │
 │ [Streaming via Relay]  │                        │
```

**Komponen Relay Server (Project Terpisah):**

| Komponen | Fungsi |
|----------|--------|
| `TcpListenerService` | Mendengarkan koneksi TCP port 45680 |
| `ConnectionManager` | Manajemen koneksi Host dan Tamu |
| `SessionManager` | Manajemen sesi Host-Tamu |
| `PacketRouter` | Routing paket antara Host-Tamu (transparent) |
| `HostCodeGenerator` | Generate kode unik 6 karakter |

**HostCode:**
- 6 karakter alphanumeric (A-Z, 0-9)
- Unik per Host yang terdaftar
- Mudah diingat dan diketik manual

## Komponen Utama

### 1. mdl_VariabelUmum.vb

Berisi konstanta, enum, variabel global, dan Mode Developer detection.

| Kategori | Contoh |
|----------|--------|
| **Mode Developer** | `ModeDeveloper` property — deteksi otomatis berdasarkan path eksekusi (lihat section Mode Developer di bawah) |
| **Konstanta Port Default** | `DEFAULT_PORT_DISCOVERY = 45678`, `DEFAULT_PORT_KONEKSI = 45679`, `DEFAULT_PORT_RELAY = 45680`, `DEFAULT_PORT_UDP_VIDEO = 45681` |
| **Konstanta FPS** | `DEFAULT_TARGET_FPS = 15`, `MIN_TARGET_FPS = 5`, `MAX_TARGET_FPS = 30` |
| **Port Aktif (Runtime)** | `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `PortUdpVideoAktif`, `RelayServerIPAktif` |
| **FPS Aktif (Runtime)** | `TargetFPSAktif` — diambil dari `SetelPortAktif.TargetFPS` |
| **Settings Object** | `SetelPortAktif As cls_SetelPort` — instance untuk load/save port & streaming settings |
| **Timeout** | `TIMEOUT_DISCOVERY = 3000ms`, `TIMEOUT_KONEKSI = 10000ms` |
| **Enum Status** | `StatusKoneksi`, `ModeAplikasi`, `TipeAksiMouse` |
| **Variabel Global** | `ModeAplikasiSaatIni`, `StatusKoneksiSaatIni`, `KunciSesiAktif`, `LocalTestMode` |

### 2. mdl_PenemuanPerangkat.vb

Menangani discovery perangkat Host di jaringan LAN menggunakan UDP broadcast.

| Fungsi | Deskripsi |
|--------|-----------|
| `MulaiMendengarkanDiscoveryAsync()` | Host: Mendengarkan broadcast discovery |
| `ScanPerangkatDiLANAsync()` | Tamu: Scan perangkat Host di LAN |
| `BuatPaketDiscoveryBroadcast()` | Membuat paket discovery request |
| `BuatPaketResponDiscovery()` | Membuat paket discovery response |

### 3. mdl_KoneksiJaringan.vb

Menangani koneksi TCP, streaming frame, dan event handling.

| Fungsi | Deskripsi |
|--------|-----------|
| `MulaiMendengarkanKoneksiAsync()` | Host: Listen koneksi TCP masuk |
| `SambungKePeerAsync()` | Tamu: Connect ke Host |
| `MulaiStreamingLayarAsync()` | Host: Mulai kirim frame layar |
| `KirimPaketAsync()` | Kirim paket ke peer |
| `DengarkanPaketAsync()` | Loop mendengarkan paket masuk |

### 4. mdl_TangkapLayar.vb

Menangkap screenshot layar dengan scaling dan cursor drawing.

| Fungsi | Deskripsi |
|--------|-----------|
| `TangkapLayarPenuh()` | Tangkap seluruh layar utama (full resolution) |
| `TangkapLayarDenganSkala()` | Tangkap layar dengan skala tertentu (default 0.6) |
| `TangkapFrameAsync()` | Tangkap screenshot sebagai `cls_FrameLayar` |
| `TangkapLayarKeBgra()` | Tangkap layar dan return BGRA data untuk H.264 |
| `GambarCursorPadaBitmap()` | **Menggambar cursor pada screenshot** |

**Cursor Drawing Feature:**

Karena `CopyFromScreen()` tidak menangkap cursor, modul ini menggunakan Windows API untuk menggambar cursor secara manual pada screenshot:

| Windows API | Fungsi |
|-------------|--------|
| `GetCursorInfo()` | Mendapatkan posisi dan handle cursor saat ini |
| `GetIconInfo()` | Mendapatkan hotspot cursor |
| `CopyIcon()` | Copy cursor handle untuk digunakan |
| `DestroyIcon()` | Cleanup cursor handle |

| Variabel | Default | Deskripsi |
|----------|---------|-----------|
| `GambarCursor` | `True` | Flag untuk mengaktifkan/menonaktifkan cursor drawing |
| `SKALA_DEFAULT` | `0.6` | Skala default screen capture (60%) |

> **Catatan:** Cursor di-draw setelah screenshot di-scale, dengan posisi dan ukuran yang disesuaikan dengan skala.

### 5. mdl_InjeksiInput.vb

Menyuntikkan input keyboard/mouse menggunakan Windows API `SendInput`.

| Fungsi | Deskripsi |
|--------|-----------|
| `InjeksiMouse()` | Injeksi event mouse (move, click, wheel) |
| `InjeksiKeyboard()` | Injeksi event keyboard (key down/up) |

### 6. mdl_Protokol.vb

Serialisasi dan deserialisasi paket data.

| Fungsi | Deskripsi |
|--------|-----------|
| `BuatPaketXxx()` | Membuat paket dengan tipe tertentu |
| `DeserializeXxx()` | Parse payload dari paket |
| `SerializeInputKeyboard()` | Serialize input keyboard ke JSON |
| `SerializeInputMouse()` | Serialize input mouse ke JSON |

### 7. mdl_UdpStreaming.vb

Menangani UDP video streaming untuk performa tinggi dengan dukungan multi-codec (JPEG dan H.264).

| Fungsi | Deskripsi |
|--------|-----------|
| `MulaiUdpSender()` | Host: Mulai UDP sender |
| `KirimFrameUdpAsync()` | Host: Kirim frame JPEG via UDP (LAN direct) |
| `KirimFrameViaRelayAsync()` | Host: Kirim frame via UDP relay |
| `KirimH264NalUnitViaUdp()` | Host: Kirim NAL unit H.264 via UDP |
| `MulaiUdpReceiverAsync()` | Tamu: Mulai UDP receiver (fire-and-forget pattern) |
| `HentikanUdpStreaming()` | Stop UDP streaming |
| `GenerateSessionId()` | Generate SessionId dari KunciSesi (djb2 hash) |
| `MulaiH264EncoderAsync()` | Host: Mulai FFmpeg H.264 encoder |
| `HentikanH264Encoder()` | Host: Hentikan FFmpeg encoder |

| Event | Deskripsi |
|-------|-----------|
| `FrameUdpDiterima` | Raised ketika frame lengkap diterima |
| `StatistikUdp` | Raised setiap 1 detik dengan info packets/FPS |

| Class Pendukung | Deskripsi |
|-----------------|-----------|
| `cls_UdpPacket` | Model UDP packet dengan header 16 byte + CodecType |
| `cls_UdpFrameChunker` | Memecah frame JPEG/H.264 menjadi chunks |
| `cls_UdpFrameAssembler` | Menyusun chunks menjadi frame lengkap |
| `UdpConstants` | Konstanta UDP (header size, max payload, codec types) |

> **PENTING - SessionId Hash:** `GenerateSessionId()` menggunakan **djb2 hash algorithm** (bukan `GetHashCode()`) untuk konsistensi cross-platform. Relay Server dan Android juga menggunakan algoritma yang sama. Lihat dokumentasi Relay untuk detail.

> **PENTING - Fire-and-Forget Pattern:** `MulaiUdpReceiverAsync()` menggunakan `Task.Run()` **tanpa** `Await` untuk memulai `LoopTerimaUdp()`. Ini penting karena loop berjalan terus sampai streaming dihentikan. Jika menggunakan `Await Task.Run(...)`, fungsi akan blocking forever dan kode setelahnya (seperti registrasi ke relay) tidak akan dieksekusi.

### 7a. H.264 Encoding Components (Host)

| Komponen | File | Deskripsi |
|----------|------|-----------|
| **FFmpeg Manager** | `mdl_FFmpegManager.vb` | Deteksi FFmpeg, lifecycle management |
| **H.264 Encoder** | `cls_H264Encoder.vb` | Wrapper FFmpeg via stdin/stdout pipe |
| **NAL Parser** | `cls_NalParser.vb` | Parse NAL units dari H.264 stream |

**Alur H.264 Encoding:**
```
Screen Capture → BitmapKeBgra() → FFmpeg stdin → H.264 stream → NAL Parser → UDP Chunker → Send
```

**Screen Capture (`mdl_TangkapLayar.vb`):**
- `TangkapLayarDenganSkala(skala)` - Capture layar dengan skala (default 0.6 = 60%)
- `BitmapKeBgra(bitmap)` - Ekstrak raw BGRA pixel data untuk FFmpeg

**Catatan Penting BitmapKeBgra():**
- Gunakan copy langsung dari `bmpData.Scan0` ke byte array
- **JANGAN** tambahkan stride padding handling row-by-row
- **JANGAN** force alpha channel ke 255
- Windows GDI sudah menghasilkan data BGRA yang benar
- Format: `LockBits` → `Marshal.Copy` langsung → `Return`

**FFmpeg Settings (Low-Latency):**
```bash
ffmpeg -f rawvideo -pix_fmt bgra -s [W]x[H] -r [FPS] -i pipe:0 \
       -c:v libx264 -preset ultrafast -tune zerolatency \
       -profile:v baseline -level 3.0 -pix_fmt yuv420p \
       -x264-params "bframes=0:keyint=30:min-keyint=30" \
       -f h264 pipe:1
```

**NAL Unit Types:**
| Type | Deskripsi | Handling |
|------|-----------|----------|
| 7 (SPS) | Sequence Parameter Set | Disimpan saat di-parse, prepend ke keyframe PERTAMA saja |
| 8 (PPS) | Picture Parameter Set | Disimpan saat di-parse, prepend ke keyframe PERTAMA saja |
| 5 (IDR) | Keyframe | Keyframe PERTAMA: SPS+PPS prepended. Keyframe berikutnya: tanpa SPS+PPS |
| 1 (Non-IDR) | P-frame | Dikirim langsung (setelah keyframe pertama terkirim) |

**Catatan Penting SPS+PPS Handling:**
- SPS+PPS **HANYA** dikirim dengan keyframe **PERTAMA** di awal sesi
- Keyframe berikutnya dikirim **TANPA** SPS+PPS karena decoder sudah tahu parameternya
- Flag `_firstKeyframeSent` digunakan untuk tracking ini
- **JANGAN** kirim SPS+PPS dengan setiap keyframe - ini bisa menyebabkan decode error

> **PENTING:** Host akan skip non-keyframe NAL units sampai keyframe pertama dengan SPS/PPS terkirim. Ini memastikan decoder di client bisa initialize dengan benar.

### 7b. H.264 Decoding Components (Tamu)

| Komponen | File | Deskripsi |
|----------|------|-----------|
| **H.264 Decoder** | `cls_H264Decoder.vb` | Wrapper FFmpeg decoder via stdin/stdout pipe |

**Alur H.264 Decoding:**
```
UDP Receive → Frame Assembler → FFmpeg stdin → BGRA output → WriteableBitmap
```

**FFmpeg Decoder Settings:**
```bash
# Mode dengan resolusi fixed
ffmpeg -f h264 -i pipe:0 -f rawvideo -pix_fmt bgra -s [W]x[H] pipe:1

# Mode auto-resolusi (deteksi dari SPS)
ffmpeg -f h264 -i pipe:0 -f rawvideo -pix_fmt bgra pipe:1
```

**Decoder Modes:**
| Mode | Method | Deskripsi |
|------|--------|-----------|
| Fixed Resolution | `Start(width, height)` | Resolusi output di-specify saat start |
| Auto Resolution | `StartAutoResolusi()` | Resolusi auto-detect dari SPS dalam H.264 stream |

**BGRA Rendering di Viewer:**
- **Direct Copy**: BGRA data dari FFmpeg langsung di-copy ke WriteableBitmap tanpa transformasi tambahan.
- **Auto-Resolution**: Resolusi WriteableBitmap dibuat berdasarkan resolusi yang terdeteksi dari FFmpeg stderr output.
- **Kesederhanaan**: Tidak perlu stride handling khusus karena resolusi sudah match.

**Events:**
| Event | Deskripsi |
|-------|-----------|
| `FrameReady` | BGRA frame tersedia (sudah di-decode) |
| `DecoderError` | Error pada decoder |
| `DecoderStopped` | Decoder berhenti |

### 7c. File Logging System

Sistem logging ke file untuk debugging, diimplementasikan di `mdl_VariabelUmum.vb`.

> **PENTING:** File logging **HANYA aktif di Mode Developer** (aplikasi dijalankan dari folder Debug). Di Release mode, `WriteLog()` hanya menulis ke `Debug.WriteLine` tanpa membuat file log.

**Lokasi Log Files:**
```
{AppDomain.CurrentDomain.BaseDirectory}\Logs\
```
Contoh: `Booku Remote\bin\Debug\net8.0-windows\Logs\`

**Format Nama File:**
```
BookuRemote_{Role}_{timestamp}.log
```
Contoh: `BookuRemote_Host_20260126_214532.log`, `BookuRemote_Tamu_20260126_214535.log`

**Fungsi Logging:**

| Fungsi | Deskripsi |
|--------|-----------|
| `InitLogFile(role As String)` | Inisialisasi file log dengan role (Host/Tamu) dan timestamp. **Hanya aktif jika `ModeDeveloper = True`** |
| `WriteLog(message As String)` | Menulis pesan ke `Debug.WriteLine` (selalu) + file log (hanya jika Mode Developer) |
| `BersihkanLogLama()` | Menghapus file log yang lebih dari 7 hari |

**Cara Penggunaan:**
```vb
' Di wpfWin_StartUp saat user pilih mode:
InitLogFile("Host")  ' atau "Tamu"

' Di seluruh kode untuk logging:
WriteLog("[UDP-HOST] Mengirim frame #123")
WriteLog($"[H264-DEC] Frame decoded: {width}x{height}")
```

**Fitur:**
- Thread-safe dengan `SyncLock`
- Dual output: `Debug.WriteLine` (selalu) + file logging (hanya Mode Developer)
- Auto-cleanup file lama (>7 hari)
- Graceful failure (tidak throw exception jika gagal write)
- **Kondisional**: File logging dinonaktifkan di Release mode untuk performa

> **Catatan:** Di Release mode, folder `Logs/` tidak akan dibuat dan tidak ada file log yang ditulis. Ini menghindari penumpukan file log di PC user.

### 8. mdl_KoneksiRelay.vb

Menangani koneksi ke Relay Server untuk remote via internet.

| Fungsi | Deskripsi |
|--------|-----------|
| `SambungKeRelayServerAsync()` | Connect ke relay server (Host/Tamu) |
| `RegisterHostAsync()` | Host: Register dan dapat HostCode |
| `UnregisterHostAsync()` | Host: Unregister dari relay |
| `QueryHostAsync()` | Tamu: Query ketersediaan Host |
| `ConnectViaRelayAsync()` | Tamu: Request koneksi via relay |
| `MulaiHeartbeatRelay()` | Mulai heartbeat ke relay |
| `HentikanKoneksiRelay()` | Tutup koneksi relay |

| Variabel Global | Deskripsi |
|-----------------|-----------|
| `RelayServerIPAktif` | IP address relay server (default: 155.117.43.209, dapat dikonfigurasi) |
| `PortRelayAktif` | Port relay server (default: 45680, dapat dikonfigurasi) |
| `HostCodeSaatIni` | HostCode yang didapat setelah register |
| `StatusKoneksiRelay` | Status koneksi ke relay |

## Konfigurasi Port dan Streaming

Port jaringan dan parameter streaming dapat dikonfigurasi manual melalui UI di window Host/Tamu.

### File dan Lokasi

| Komponen | Lokasi/File |
|----------|-------------|
| **Class Settings** | `Kelas/cls_SetelPort.vb` |
| **File JSON** | `%AppData%\BookuID\Booku Remote\port-settings.json` |

### Parameter yang Dapat Dikonfigurasi

| Parameter | Default | Range | Deskripsi |
|-----------|---------|-------|-----------|
| `PortDiscovery` | 45678 | - | UDP broadcast untuk discovery LAN |
| `PortKoneksi` | 45679 | - | TCP koneksi remote LAN (control plane) |
| `PortRelay` | 45680 | - | TCP koneksi via relay server |
| `PortUdpVideo` | 45681 | - | **UDP video streaming** (data plane) |
| `RelayServerIP` | 155.117.43.209 | - | Alamat IP relay server |
| `TargetFPS` | **15** | **5-30** | **Frame rate streaming (slider)** |

### Cara Kerja

1. **Startup:** `MuatSetelPort()` dipanggil di `InisialisasiVariabelUmum()` untuk load settings dari file JSON
2. **Runtime:** Variabel `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `RelayServerIPAktif`, `TargetFPSAktif` digunakan
3. **UI (Port):** User dapat mengubah port via Expander "Pengaturan Port" di window Host/Tamu
4. **UI (FPS):** User dapat mengubah FPS via slider di Expander "Pengaturan Streaming" di window Host
5. **Simpan:** Perubahan disimpan ke file JSON via `SetelPortAktif.SimpanKeFile()`
6. **Reset:** Tombol "Reset ke Default" mengembalikan ke nilai default

## Window dan UI

### wpfWin_StartUp

Menu utama untuk memilih mode aplikasi.

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeHost` | Aktifkan mode Host (dikontrol) |
| `btn_ModeTamu` | Aktifkan mode Tamu (mengontrol) |

### wpfWin_ModeHost

Window Host yang menunggu koneksi (LAN atau Internet).

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeLAN` / `btn_ModeInternet` | Switch mode koneksi |
| `lbl_StatusKoneksi` | Status koneksi saat ini |
| `lbl_AlamatIP` | Alamat IP Host (mode LAN) |
| `lbl_HostCode` | HostCode untuk mode Internet |
| `txt_Password` | Password opsional untuk koneksi |
| `btn_Hentikan` | Hentikan mode Host |
| **Expander "Pengaturan Port"** | Konfigurasi port Discovery, Koneksi, Relay, UDP Video, dan Relay Server IP |
| **Expander "Pengaturan Streaming"** | **Slider untuk Target FPS (5-30, default 15)** |
| **Local Test Mode** | Checkbox untuk testing di 1 PC — **hanya tampil di Mode Developer** |

**Local Test Mode:**
- Hanya visible jika `ModeDeveloper = True` (aplikasi dijalankan dari folder Debug)
- Saat diaktifkan, input dari Tamu **tidak di-inject ke Windows** (hanya di-log)
- Berguna untuk testing Host + Tamu di satu PC tanpa input saling interferensi
- Di Release mode, checkbox ini tersembunyi dan `LocalTestMode` selalu `False`

### wpfWin_ModeTamu

Window Tamu untuk scan dan connect ke Host (LAN atau Internet).

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeLAN` / `btn_ModeInternet` | Switch mode koneksi |
| `datagridUtama` | Daftar perangkat Host (mode LAN) |
| `btn_Scan` | Scan ulang perangkat di LAN |
| `txt_HostCode` | Input HostCode (mode Internet) |
| `lbl_HostInfo` | Info Host dari query (mode Internet) |
| `btn_Sambungkan` | Connect ke Host terpilih |
| **Expander "Pengaturan Lanjutan"** | Konfigurasi port Discovery, Relay Server IP, dan port Relay |

### wpfWin_PersetujuanKoneksi

Dialog yang muncul di Host saat ada permintaan koneksi.

| Komponen | Fungsi |
|----------|--------|
| `lbl_NamaTamu` | Nama perangkat Tamu |
| `chk_IzinKontrol` | Izinkan kontrol keyboard/mouse |
| `btn_Terima` | Terima koneksi |
| `btn_Tolak` | Tolak koneksi |

### wpfWin_Viewer

Window Viewer di Tamu untuk melihat dan mengontrol layar Host.

| Komponen | Fungsi |
|----------|--------|
| `img_Layar` | Image control untuk menampilkan frame |
| `tgl_Kontrol` | Toggle aktifkan kontrol keyboard/mouse |
| `btn_Fullscreen` | Mode fullscreen |
| `btn_Putuskan` | Putuskan koneksi |
| `lbl_FPS` | Info FPS streaming |
| `lbl_Latency` | Info latency |

## Konfigurasi Streaming

| Parameter | Nilai Default | Range | Deskripsi |
|-----------|---------------|-------|-----------|
| **Skala Frame** | **0.6 (60%)** | 0.1-1.0 | Skala screenshot (resolusi output) |
| **Target FPS** | **15** | **5-30** | **Frame per second (dapat dikonfigurasi via slider di Host)** |
| JPEG Quality | 30 | - | Kualitas kompresi JPEG |
| Mouse Throttle | 30ms | - | Interval minimum mouse move |
| **Gambar Cursor** | **True** | - | **Menggambar cursor Host pada screenshot** |

> **Catatan:** Target FPS dapat diubah melalui slider di Expander "Pengaturan Streaming" pada window Host. Nilai lebih rendah mengurangi beban bandwidth dan CPU, berguna jika client (terutama Android) mengalami lag atau crash.

## Keamanan

| Aspek | Implementasi |
|-------|--------------|
| **Persetujuan** | Host harus menyetujui setiap koneksi |
| **Izin Kontrol** | Host memilih apakah mengizinkan kontrol input |
| **Kunci Sesi** | Session key untuk validasi paket |
| **Toggle Kontrol** | Tamu harus manually enable kontrol |

## Keyboard Shortcut (Viewer)

| Shortcut | Fungsi |
|----------|--------|
| **Ctrl+K** | Toggle kontrol ON/OFF |
| **F11** | Fullscreen (jika diimplementasi) |

## Status Pengembangan

| Komponen | Status | Catatan |
|----------|--------|---------|
| Discovery UDP | Selesai | Broadcast + Response |
| Koneksi TCP (LAN) | Selesai | Handshake + Heartbeat |
| Koneksi Relay (Internet) | Selesai | Via VPS 155.117.43.209:45680 |
| Screen Streaming (TCP) | Selesai | JPEG compression, fallback mode |
| **Screen Streaming (UDP/JPEG) LAN** | **Selesai** | **Port 45681, chunking/reassembly, low-latency** |
| **Screen Streaming (UDP/JPEG) Relay** | **Selesai** | **Via relay server, fire-and-forget receiver pattern** |
| **Screen Streaming (UDP/H.264) Host** | **Selesai** | **FFmpeg encoder, NAL parsing, codec routing** |
| **Screen Streaming (UDP/H.264) Tamu WPF** | **Selesai** | **FFmpeg decoder, BGRA render berfungsi** |
| Keyboard Control | Selesai | SendInput API |
| Mouse Control | Selesai | Move, Click, Wheel |
| **Cursor Drawing** | **Selesai** | **Menggambar cursor Host pada screenshot via Windows API** |
| **Port Settings** | Selesai | Configurable via UI, JSON persistence |
| **FPS Settings** | **Selesai** | **Slider 5-30 FPS di Host, default 15** |
| **File Logging System** | **Selesai** | **WriteLog() ke folder Logs/, auto-cleanup 7 hari, kondisional Mode Developer** |
| **Mode Developer** | **Selesai** | **Deteksi otomatis berdasarkan path eksekusi (Debug vs Release)** |
| **Local Test Mode** | **Selesai** | **Testing di 1 PC tanpa input injection, hanya tampil di Mode Developer** |
| **Single Instance (Kondisional)** | **Selesai** | **Mutex hanya aktif di Release mode** |
| File Transfer | Belum | Fase 3 |
| Clipboard Sync | Belum | Fase 3 |

### Catatan Implementasi H.264 Streaming

**Prinsip Dasar - Kesederhanaan adalah Kunci:**

Implementasi H.264 streaming yang berhasil menggunakan pendekatan **sederhana dan langsung**. Jangan menambahkan "perbaikan" yang tidak diperlukan.

**Yang BENAR (Implementasi Saat Ini):**
1. ✅ SPS+PPS hanya di-prepend pada keyframe **PERTAMA** saja
2. ✅ `BitmapKeBgra()` menggunakan copy langsung tanpa stride handling
3. ✅ Skala capture default 0.6 (60% dari resolusi layar)
4. ✅ Cursor drawing via Windows API untuk menampilkan cursor di screenshot
5. ✅ Decoder menggunakan auto-resolution detection dari SPS

**Yang SALAH (Jangan Dilakukan):**
- ❌ Jangan kirim SPS+PPS dengan setiap keyframe
- ❌ Jangan tambahkan stride padding handling row-by-row di `BitmapKeBgra()`
- ❌ Jangan force alpha channel ke 255
- ❌ Jangan tambahkan `CompositingMode.SourceCopy` di screen capture

> **Referensi:** Jika ada masalah, bandingkan dengan versi backup yang berhasil. Folder `X_Booku Remote` berisi versi yang gagal untuk referensi perbandingan.

## Mode Developer

Sistem deteksi otomatis yang membedakan antara Development mode dan Production mode berdasarkan path eksekusi.

### Cara Kerja

```vb
Public ReadOnly Property ModeDeveloper As Boolean
    Get
        Dim baseDir = AppDomain.CurrentDomain.BaseDirectory
        ' Cek apakah path mengandung "\Debug\" (case-insensitive)
        Return baseDir.IndexOf("\Debug\", StringComparison.OrdinalIgnoreCase) >= 0
    End Get
End Property
```

### Perbedaan Mode

| Aspek | Mode Developer (Debug) | Mode Production (Release) |
|-------|------------------------|---------------------------|
| **Path Eksekusi** | Mengandung `\Debug\` | Tidak mengandung `\Debug\` |
| **Single Instance** | Dinonaktifkan (multi-instance OK) | Aktif (Mutex protection) |
| **File Logging** | Aktif (folder `Logs/` dibuat) | Dinonaktifkan (tidak ada file log) |
| **Local Test Mode** | Checkbox tampil di Host | Checkbox tersembunyi |
| **Debug.WriteLine** | Output ke Visual Studio | Tetap aktif tapi tidak terlihat |

### Implementasi

| File | Perubahan |
|------|-----------|
| `mdl_VariabelUmum.vb` | Property `ModeDeveloper`, kondisional di `InitLogFile()` dan `WriteLog()` |
| `Application.xaml.vb` | Mutex hanya aktif jika `Not ModeDeveloper` |
| `wpfWin_ModeHost.xaml.vb` | Local Test Mode checkbox visibility berdasarkan `ModeDeveloper` |

## Debugging Notes

> **Prasyarat:** File log hanya tersedia jika aplikasi dijalankan dalam **Mode Developer** (dari folder Debug). Lihat section [Mode Developer](#mode-developer) untuk detail.

### Cara Debugging dengan File Log

1. **Jalankan aplikasi dari folder Debug** - Log file akan dibuat otomatis di `bin/Debug/net8.0-windows/Logs/`
2. **Setelah test** - Baca file log dengan nama format `BookuRemote_Host_*.log` atau `BookuRemote_Tamu_*.log`
3. **Cari error** - Gunakan `grep` atau search untuk mencari pattern seperti `[ERROR]`, `Exception`, atau komponen spesifik seperti `[H264-DEC]`

**Tag Log yang Digunakan:**
| Tag | Komponen |
|-----|----------|
| `[UDP-HOST]` | UDP streaming di Host |
| `[UDP-TAMU]` | UDP streaming di Tamu |
| `[H264-ENC]` | H.264 encoder (Host) |
| `[H264-DEC]` | H.264 decoder (Tamu) |
| `[H264-DIAG]` | Diagnostic pixel data |
| `[VIEWER]` | Frame rendering di Viewer |
| `[CURSOR]` | Cursor drawing pada screenshot |
| `[FFMPEG-ENC-ERR]` | FFmpeg encoder stderr |
| `[FFMPEG-DEC-ERR]` | FFmpeg decoder stderr |
| `[LOCAL-TEST]` | Input yang tidak di-inject karena Local Test Mode aktif |
| `[LOG]` | Status sistem logging |

### Contoh Log H.264 yang Berhasil

**Host Log:**
```
[H264-HOST] Encoder started: 1152x648 @ 15fps
[H264-DATA] FIRST IDR frame with SPS+PPS prepended: 281 bytes
[H264-DATA] IDR frame (no SPS+PPS): 248 bytes
[H264-DATA] Non-IDR frame: 12 bytes (type=1)
```

**Tamu Log:**
```
[UDP-TAMU] H.264 detected, enabled ordered delivery mode
[H264-DEC] Decoder STARTED with auto-resolution detection
[FFMPEG-DEC-ERR] Stream #0:0: Video: rawvideo (BGRA), bgra, 1152x648
[H264-DEC] Frame #1 decoded: 1152x648, BGRA size=2985984
[H264] New WriteableBitmap: 1152x648, BackBufferStride=4608
[VIEWER] First frame rendered, loading overlay hidden
```

> **Catatan:** BGRA size = Width × Height × 4 = 1152 × 648 × 4 = 2,985,984 bytes

## Aturan Pengembangan

1. **Namespace**: Semua kode harus dalam namespace `Booku_Remote`
2. **Prefix**: Gunakan prefix standar (`wpfWin_`, `mdl_`, `cls_`)
3. **Async/Await**: Gunakan async untuk operasi jaringan
4. **Error Handling**: Tangani exception network dengan baik
5. **UI Thread**: Gunakan `Dispatcher.Invoke` untuk update UI dari async task
6. **Resource Cleanup**: Dispose TCP connections dan streams dengan benar
7. **Logging**: Gunakan `WriteLog()` untuk logging, bukan `Console.WriteLine` atau `Debug.WriteLine` langsung. Gunakan tag yang konsisten (lihat Debugging Notes)

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
| Testing Guide Fase 2b | `/Booku Remote/TESTING_FASE_2B.md` |
| Plan File | `~/.claude/plans/happy-mixing-flute.md` |
