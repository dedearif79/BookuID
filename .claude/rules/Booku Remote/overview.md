# Booku Remote

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Deskripsi

**Booku Remote** adalah aplikasi remote desktop berbasis WPF untuk mengontrol PC lain dalam jaringan LAN maupun internet. Aplikasi ini memungkinkan screen sharing dan kontrol keyboard/mouse jarak jauh.

> **Roadmap:** Saat ini fokus pengembangan adalah fitur remote dalam jaringan LAN (Fase 1-3). Setelah fitur LAN stabil, akan dikembangkan kemampuan remote melalui jaringan internet (Fase 4).

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Remote |
| **Output** | `Booku Remote.exe` (WinExe) |
| **Namespace** | `Booku_Remote` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF + TCP/UDP Networking |
| **Entry Point** | `Application.xaml` → `wpfWin_StartUp.xaml` |

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
│   └── cls_PaketData.vb          # Payload classes untuk protokol
│
├── Modul/                        # Module files
│   ├── mdl_VariabelUmum.vb       # Variabel global, enum, konstanta
│   ├── mdl_PenemuanPerangkat.vb  # Discovery perangkat di LAN (UDP)
│   ├── mdl_KoneksiJaringan.vb    # Koneksi TCP dan streaming
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
| **Fase 2b** | Kontrol Keyboard dan Mouse | Selesai (testing) |
| **Fase 3** | Transfer Berkas | Belum dimulai |
| **Fase 4** | Remote melalui Jaringan Internet | Belum dimulai |

## Arsitektur Jaringan

### Port yang Digunakan

| Port | Protokol | Kegunaan |
|------|----------|----------|
| `45678` | UDP | Discovery perangkat di LAN (broadcast) |
| `45679` | TCP | Koneksi remote (data, frame, input) |

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

### Fase 4: Remote via Internet (Rencana)

**Tantangan Utama:**
- Perangkat di belakang NAT/firewall tidak bisa diakses langsung dari internet
- IP address dinamis (berubah-ubah)
- Keamanan data yang melintas di jaringan publik

**Arsitektur yang Direncanakan:**

```
┌─────────────────────────────────────────────────────────────────────────┐
│  INFRASTRUKTUR SERVER (Cloud)                                           │
│                                                                         │
│  ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐     │
│  │ Signaling       │    │ STUN Server     │    │ Relay Server    │     │
│  │ Server          │    │ (NAT Discovery) │    │ (TURN/Fallback) │     │
│  │ (WebSocket)     │    │                 │    │                 │     │
│  └────────┬────────┘    └────────┬────────┘    └────────┬────────┘     │
│           │                      │                      │               │
└───────────┼──────────────────────┼──────────────────────┼───────────────┘
            │                      │                      │
            │ Internet             │                      │
            │                      │                      │
┌───────────┼──────────────────────┼──────────────────────┼───────────────┐
│  HOST     │                      │                      │               │
│  (NAT A)  │                      │                      │               │
│  ┌────────┴────────┐    ┌────────┴────────┐             │               │
│  │ 1. Register     │    │ 2. Discover     │             │               │
│  │    & Auth       │    │    Public IP    │             │               │
│  └─────────────────┘    └─────────────────┘             │               │
│           │                      │                      │               │
│           └──────────────────────┴──────────────────────┘               │
│                                  │                                      │
│                    ┌─────────────┴─────────────┐                        │
│                    │ 3a. P2P Direct (UDP Hole  │                        │
│                    │     Punching) ATAU        │◄─────────────────┐     │
│                    │ 3b. Via Relay Server      │                  │     │
│                    └─────────────┬─────────────┘                  │     │
└──────────────────────────────────┼────────────────────────────────┼─────┘
                                   │                                │
                                   │ Internet                       │
                                   │                                │
┌──────────────────────────────────┼────────────────────────────────┼─────┐
│  TAMU                            │                                │     │
│  (NAT B)                         ▼                                │     │
│                    ┌─────────────────────────┐                    │     │
│                    │ 4. Terima Frame &       │                    │     │
│                    │    Kirim Input          │────────────────────┘     │
│                    └─────────────────────────┘                          │
└─────────────────────────────────────────────────────────────────────────┘
```

**Komponen Server yang Diperlukan:**

| Komponen | Fungsi | Teknologi |
|----------|--------|-----------|
| **Signaling Server** | Koordinasi handshake, exchange SDP/ICE candidates | WebSocket + JSON |
| **STUN Server** | Discover public IP dan tipe NAT | STUN Protocol (RFC 5389) |
| **Relay Server (TURN)** | Fallback jika P2P gagal, relay semua traffic | TURN Protocol (RFC 5766) |
| **Auth Server** | Registrasi perangkat, autentikasi, manajemen sesi | REST API + JWT |

**Alur Koneksi Internet:**

| Langkah | Deskripsi |
|---------|-----------|
| 1. **Registrasi** | Host & Tamu login ke Auth Server, dapat token |
| 2. **Discovery NAT** | Kedua pihak query STUN untuk dapat public IP + port |
| 3. **Signaling** | Tamu request koneksi via Signaling Server |
| 4. **ICE Negotiation** | Exchange ICE candidates (STUN + TURN) |
| 5a. **P2P Direct** | Jika NAT mendukung, koneksi langsung via UDP hole punching |
| 5b. **Relay Fallback** | Jika P2P gagal, gunakan Relay Server |
| 6. **Streaming** | Protokol sama dengan LAN (frame + input) |

**Tipe Paket Tambahan (Rencana):**

```vb
Public Enum TipePaket
    ' ... existing packets ...

    ' Internet/Signaling (40-49)
    REGISTER_DEVICE = 40        ' Daftarkan perangkat ke server
    AUTH_REQUEST = 41           ' Request autentikasi
    AUTH_RESPONSE = 42          ' Response autentikasi (token)
    SIGNAL_OFFER = 43           ' WebRTC-style SDP offer
    SIGNAL_ANSWER = 44          ' WebRTC-style SDP answer
    ICE_CANDIDATE = 45          ' ICE candidate exchange
    PEER_LIST = 46              ' Daftar perangkat online
    KEEP_ALIVE = 47             ' Heartbeat ke signaling server
End Enum
```

**Keamanan Tambahan untuk Internet:**

| Aspek | Implementasi |
|-------|--------------|
| **Enkripsi Transport** | TLS 1.3 untuk signaling, DTLS untuk media |
| **Enkripsi End-to-End** | AES-256-GCM untuk frame dan input |
| **Autentikasi** | JWT token dengan expiry |
| **Device Pairing** | One-time code atau QR code untuk pair pertama kali |
| **Rate Limiting** | Mencegah brute-force dan DDoS |

**File/Modul Baru yang Direncanakan:**

| File | Fungsi |
|------|--------|
| `mdl_SignalingClient.vb` | WebSocket client ke signaling server |
| `mdl_STUNClient.vb` | STUN protocol implementation |
| `mdl_ICENegotiation.vb` | ICE candidate gathering & exchange |
| `mdl_RelayClient.vb` | TURN relay client |
| `mdl_CryptoE2E.vb` | End-to-end encryption |
| `cls_PerangkatInternet.vb` | Model perangkat remote (bukan LAN) |

**Opsi Implementasi:**

| Opsi | Pro | Kontra |
|------|-----|--------|
| **Custom Protocol** | Full control, optimized | Development effort tinggi |
| **WebRTC-based** | Mature, NAT traversal built-in | Dependency besar, learning curve |
| **Third-party Relay** | Cepat deploy | Biaya, dependency eksternal |

> **Catatan:** Detail implementasi akan ditentukan saat memulai Fase 4. Arsitektur di atas adalah rencana awal yang bisa berubah sesuai kebutuhan.

## Komponen Utama

### 1. mdl_VariabelUmum.vb

Berisi konstanta, enum, dan variabel global:

| Kategori | Contoh |
|----------|--------|
| **Konstanta Port** | `PORT_DISCOVERY = 45678`, `PORT_KONEKSI = 45679` |
| **Timeout** | `TIMEOUT_DISCOVERY = 3000ms`, `TIMEOUT_KONEKSI = 10000ms` |
| **Enum Status** | `StatusKoneksi`, `ModeAplikasi`, `TipeAksiMouse` |
| **Variabel Global** | `ModeAplikasiSaatIni`, `StatusKoneksiSaatIni`, `KunciSesiAktif` |

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

Menangkap screenshot layar dengan scaling.

| Fungsi | Deskripsi |
|--------|-----------|
| `TangkapFrameAsync()` | Tangkap screenshot sebagai `cls_FrameLayar` |
| `TangkapLayarDasar()` | Tangkap screenshot mentah (Bitmap) |
| `KompresKeJPEG()` | Kompres Bitmap ke JPEG bytes |

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

## Window dan UI

### wpfWin_StartUp

Menu utama untuk memilih mode aplikasi.

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeHost` | Aktifkan mode Host (dikontrol) |
| `btn_ModeTamu` | Aktifkan mode Tamu (mengontrol) |

### wpfWin_ModeHost

Window Host yang menunggu koneksi.

| Komponen | Fungsi |
|----------|--------|
| `lbl_StatusKoneksi` | Status koneksi saat ini |
| `lbl_AlamatIP` | Alamat IP Host |
| `btn_Hentikan` | Hentikan mode Host |

### wpfWin_ModeTamu

Window Tamu untuk scan dan connect ke Host.

| Komponen | Fungsi |
|----------|--------|
| `datagridUtama` | Daftar perangkat Host yang ditemukan |
| `btn_Scan` | Scan ulang perangkat di LAN |
| `btn_Sambungkan` | Connect ke Host terpilih |

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

| Parameter | Nilai Default | Deskripsi |
|-----------|---------------|-----------|
| Skala Frame | 0.5 (50%) | Skala screenshot |
| Target FPS | 15 | Frame per second |
| JPEG Quality | 50 | Kualitas kompresi JPEG |
| Mouse Throttle | 30ms | Interval minimum mouse move |

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
| Koneksi TCP | Selesai | Handshake + Heartbeat |
| Screen Streaming | Selesai | JPEG compression |
| Keyboard Control | Selesai | SendInput API |
| Mouse Control | Selesai | Move, Click, Wheel |
| File Transfer | Belum | Fase 3 |
| Clipboard Sync | Belum | Fase 3 |
| Internet Remote | Belum | Fase 4 - NAT traversal, relay server |

## Aturan Pengembangan

1. **Namespace**: Semua kode harus dalam namespace `Booku_Remote`
2. **Prefix**: Gunakan prefix standar (`wpfWin_`, `mdl_`, `cls_`)
3. **Async/Await**: Gunakan async untuk operasi jaringan
4. **Error Handling**: Tangani exception network dengan baik
5. **UI Thread**: Gunakan `Dispatcher.Invoke` untuk update UI dari async task
6. **Resource Cleanup**: Dispose TCP connections dan streams dengan benar

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
