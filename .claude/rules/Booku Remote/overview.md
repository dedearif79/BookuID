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
│   └── cls_SetelPort.vb          # Konfigurasi port (load/save JSON)
│
├── Modul/                        # Module files
│   ├── mdl_VariabelUmum.vb       # Variabel global, enum, konstanta
│   ├── mdl_PenemuanPerangkat.vb  # Discovery perangkat di LAN (UDP)
│   ├── mdl_KoneksiJaringan.vb    # Koneksi TCP dan streaming (LAN)
│   ├── mdl_KoneksiRelay.vb       # Koneksi via Relay Server (Internet)
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

### Port yang Digunakan

| Port | Protokol | Lokasi | Kegunaan |
|------|----------|--------|----------|
| `45678` | UDP | LAN | Discovery perangkat (broadcast) |
| `45679` | TCP | LAN | Koneksi langsung (data, frame, input) |
| `45680` | TCP | VPS | Relay Server (koneksi via internet) |

> **Catatan:** Semua port di atas adalah nilai **default** dan dapat dikonfigurasi manual melalui UI di window Host atau Tamu. Settings disimpan ke file JSON di `%AppData%\BookuID\Booku Remote\port-settings.json`.

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

Berisi konstanta, enum, dan variabel global:

| Kategori | Contoh |
|----------|--------|
| **Konstanta Port Default** | `DEFAULT_PORT_DISCOVERY = 45678`, `DEFAULT_PORT_KONEKSI = 45679`, `DEFAULT_PORT_RELAY = 45680` |
| **Port Aktif (Runtime)** | `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `RelayServerIPAktif` |
| **Settings Object** | `SetelPortAktif As cls_SetelPort` — instance untuk load/save port settings |
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

### 7. mdl_KoneksiRelay.vb

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

## Konfigurasi Port

Port jaringan dapat dikonfigurasi manual melalui UI (Expander "Pengaturan Port" di window Host/Tamu).

### File dan Lokasi

| Komponen | Lokasi/File |
|----------|-------------|
| **Class Settings** | `Kelas/cls_SetelPort.vb` |
| **File JSON** | `%AppData%\BookuID\Booku Remote\port-settings.json` |

### Port yang Dapat Dikonfigurasi

| Port | Default | Deskripsi |
|------|---------|-----------|
| `PortDiscovery` | 45678 | UDP broadcast untuk discovery LAN |
| `PortKoneksi` | 45679 | TCP koneksi remote LAN |
| `PortRelay` | 45680 | TCP koneksi via relay server |
| `RelayServerIP` | 155.117.43.209 | Alamat IP relay server |

### Cara Kerja

1. **Startup:** `MuatSetelPort()` dipanggil di `InisialisasiVariabelUmum()` untuk load settings dari file JSON
2. **Runtime:** Variabel `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `RelayServerIPAktif` digunakan
3. **UI:** User dapat mengubah port via Expander di window Host/Tamu
4. **Simpan:** Perubahan disimpan ke file JSON via `SetelPortAktif.SimpanKeFile()`
5. **Reset:** Tombol "Reset ke Default" mengembalikan ke nilai default

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
| **Expander "Pengaturan Port"** | Konfigurasi port Discovery, Koneksi, Relay, dan Relay Server IP |

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

| Parameter | Nilai Default | Deskripsi |
|-----------|---------------|-----------|
| Skala Frame | 0.35 (35%) | Skala screenshot |
| Target FPS | 20 | Frame per second |
| JPEG Quality | 30 | Kualitas kompresi JPEG |
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
| Koneksi TCP (LAN) | Selesai | Handshake + Heartbeat |
| Koneksi Relay (Internet) | Selesai | Via VPS 155.117.43.209:45680 |
| Screen Streaming | Selesai | JPEG compression |
| Keyboard Control | Selesai | SendInput API |
| Mouse Control | Selesai | Move, Click, Wheel |
| **Port Settings** | Selesai | Configurable via UI, JSON persistence |
| File Transfer | Belum | Fase 3 |
| Clipboard Sync | Belum | Fase 3 |

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
