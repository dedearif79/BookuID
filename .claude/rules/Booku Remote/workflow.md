# Workflow (Alur Kerja)

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Fase 1: Discovery dan Koneksi

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

## Fase 2: Screen Streaming

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

## Fase 2b: Kontrol Keyboard/Mouse

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

## Fase 4: Remote via Internet (Relay Server)

### Arsitektur: Relay-Only

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

### Alur Host Register ke Relay

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

### Alur Tamu Connect via Relay

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

### Komponen Relay Server (Project Terpisah)

| Komponen | Fungsi |
|----------|--------|
| `TcpListenerService` | Mendengarkan koneksi TCP port 45680 |
| `ConnectionManager` | Manajemen koneksi Host dan Tamu |
| `SessionManager` | Manajemen sesi Host-Tamu |
| `PacketRouter` | Routing paket antara Host-Tamu (transparent) |
| `HostCodeGenerator` | Generate kode unik 6 karakter |

### HostCode

- 6 karakter alphanumeric (A-Z, 0-9)
- Unik per Host yang terdaftar
- Mudah diingat dan diketik manual
