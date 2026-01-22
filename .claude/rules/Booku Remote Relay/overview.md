# Booku Remote Relay

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote Relay** (folder `BookuID/Booku Remote Relay/`)

## Deskripsi

**Booku Remote Relay** adalah server relay untuk koneksi remote desktop via internet. Server ini berjalan di VPS dan berfungsi sebagai perantara antara Host dan Tamu yang tidak berada dalam jaringan LAN yang sama.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Remote Relay |
| **Output** | `Booku Remote Relay.exe` (Console App) |
| **Namespace** | `BookuRemoteRelay` |
| **Target Framework** | .NET 8.0 |
| **Runtime** | win-x64 (self-contained) |
| **Port** | 443 (HTTPS port untuk melewati firewall) |

## Fitur Keamanan & Optimasi

| Fitur | Status | Implementasi |
|-------|--------|--------------|
| **Single Instance** | ✅ | Mutex `Global\BookuRemoteRelayServer` |
| **Obfuscation** | ✅ | IL Trimming (`PublishTrimmed=true`) |
| **Single Executable** | ✅ | Self-contained, compressed |
| **Compression** | ✅ | `EnableCompressionInSingleFile=true` |

## Struktur File

```
Booku Remote Relay/
├── Booku Remote Relay.csproj     # Project file dengan Release config
├── Program.cs                     # Entry point + Single Instance (Mutex)
├── appsettings.json              # Konfigurasi server
├── PUBLISH-RELEASE.bat           # Script build Release
│
├── Models/                        # Model classes
│   ├── Enums.cs                   # TipePaket enum
│   └── PaketData.cs               # Struktur paket data
│
└── Services/                      # Business logic
    ├── TcpListenerService.cs      # TCP listener pada port 443
    ├── ConnectionManager.cs       # Manajemen koneksi Host & Tamu
    ├── PacketRouter.cs            # Routing paket antar Host-Tamu
    └── ProtocolService.cs         # Serialisasi/deserialisasi JSON
```

## Dependencies

| Package | Versi | Kegunaan |
|---------|-------|----------|
| **Microsoft.Extensions.Configuration** | 8.0.0 | Configuration management |
| **Microsoft.Extensions.Configuration.Json** | 8.0.0 | JSON config file |
| **Microsoft.Extensions.Hosting** | 8.0.0 | Generic host |
| **System.Text.Json** | 8.0.5 | JSON serialization |

## Konfigurasi (appsettings.json)

```json
{
  "RelayServer": {
    "Port": 443,
    "HostExpiryMinutes": 60,
    "HeartbeatIntervalSeconds": 30,
    "SessionTimeoutMinutes": 30,
    "MaxHostsPerIP": 5,
    "MaxTamusPerIP": 10
  }
}
```

| Setting | Default | Deskripsi |
|---------|---------|-----------|
| `Port` | 443 | Port TCP untuk listen (HTTPS port) |
| `HostExpiryMinutes` | 60 | Waktu expired Host tanpa heartbeat |
| `HeartbeatIntervalSeconds` | 30 | Interval heartbeat dari Host |
| `SessionTimeoutMinutes` | 30 | Timeout sesi tanpa aktivitas |
| `MaxHostsPerIP` | 5 | Maksimal Host per IP address |
| `MaxTamusPerIP` | 10 | Maksimal Tamu per IP address |

## Arsitektur

```
┌─────────────────────────────────────────────────────────────┐
│              RELAY SERVER (VPS)                             │
│              IP:443                                         │
│                                                             │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────┐ │
│  │ Connection  │  │   Packet    │  │    Protocol         │ │
│  │ Manager     │  │   Router    │  │    Service          │ │
│  └─────────────┘  └─────────────┘  └─────────────────────┘ │
│         │                │                    │             │
│         └────────────────┴────────────────────┘             │
│                          │                                  │
│              ┌───────────┴───────────┐                      │
│              │  TcpListenerService   │                      │
│              │      (Port 443)       │                      │
│              └───────────────────────┘                      │
└─────────────────────────────────────────────────────────────┘
         ▲                                    ▲
         │ TCP 443                            │ TCP 443
         │                                    │
┌────────┴────────┐                ┌─────────┴─────────┐
│      HOST       │                │       TAMU        │
│  (Windows WPF)  │                │  (Windows/Android)│
└─────────────────┘                └───────────────────┘
```

## Komponen Utama

### 1. TcpListenerService

Mendengarkan koneksi TCP pada port 443.

| Fungsi | Deskripsi |
|--------|-----------|
| `StartAsync()` | Mulai listener |
| `Stop()` | Hentikan listener |
| `HandleClientAsync()` | Handle koneksi baru |

### 2. ConnectionManager

Mengelola koneksi Host dan Tamu yang terdaftar.

| Fungsi | Deskripsi |
|--------|-----------|
| `RegisterHost()` | Daftarkan Host baru, generate HostCode |
| `UnregisterHost()` | Hapus Host dari daftar |
| `GetHostByCode()` | Cari Host berdasarkan HostCode |
| `RegisterTamu()` | Daftarkan Tamu untuk sesi |
| `CreateSession()` | Buat sesi Host-Tamu |

### 3. PacketRouter

Routing paket antara Host dan Tamu.

| Fungsi | Deskripsi |
|--------|-----------|
| `RoutePacket()` | Forward paket ke tujuan |
| `HandleRelayPacket()` | Handle paket relay (register, query, dll) |

### 4. ProtocolService

Serialisasi dan deserialisasi paket JSON.

| Fungsi | Deskripsi |
|--------|-----------|
| `Serialize()` | Object → JSON string |
| `Deserialize()` | JSON string → Object |

## Tipe Paket Relay

| Enum | Nilai | Deskripsi |
|------|-------|-----------|
| `RELAY_REGISTER_HOST` | 40 | Host mendaftar ke relay |
| `RELAY_REGISTER_HOST_OK` | 41 | Response dengan HostCode |
| `RELAY_UNREGISTER_HOST` | 42 | Host logout |
| `RELAY_HOST_HEARTBEAT` | 43 | Host heartbeat |
| `RELAY_QUERY_HOST` | 45 | Tamu query Host by code |
| `RELAY_QUERY_HOST_RESULT` | 46 | Response info Host |
| `RELAY_CONNECT_REQUEST` | 47 | Tamu minta koneksi |
| `RELAY_SESSION_STARTED` | 52 | Notify session dimulai |
| `RELAY_SESSION_ENDED` | 53 | Notify session berakhir |
| `RELAY_ERROR` | 55 | Generic error |
| `RELAY_HOST_OFFLINE` | 56 | Host tidak online |
| `RELAY_INVALID_CODE` | 57 | HostCode tidak valid |

## HostCode

- **Format:** 6 karakter alphanumeric (A-Z, 0-9)
- **Contoh:** `XY7K2M`, `AB3D9F`
- **Unik:** Per Host yang terdaftar
- **Fungsi:** Identifier untuk Tamu menghubungi Host

## Build & Deploy

### Build Release

```bash
# Menggunakan script batch (recommended)
PUBLISH-RELEASE.bat

# Atau manual
dotnet publish "Booku Remote Relay.csproj" -c Release -o "bin\Publish"
```

### Output

```
bin\Publish\Booku Remote Relay.exe (~11 MB, self-contained)
```

### Deploy ke VPS

1. Copy `Booku Remote Relay.exe` ke VPS Windows
2. Pastikan port 443 tidak digunakan aplikasi lain
3. Jalankan sebagai **Administrator** (port 443 memerlukan admin privilege)
4. Server akan listen pada `0.0.0.0:443`

### Menjalankan Server

```cmd
# Jalankan dengan port default (443)
Booku Remote Relay.exe

# Jalankan dengan port custom via CLI argument
Booku Remote Relay.exe 8443
```

### Prioritas Konfigurasi Port

Server membaca port dengan prioritas berikut:

| Prioritas | Sumber | Contoh |
|-----------|--------|--------|
| 1 (Tertinggi) | **CLI Argument** | `Booku Remote Relay.exe 8443` |
| 2 | **appsettings.json** | `"RelayServer": { "Port": 443 }` |
| 3 (Terendah) | **Default Constant** | `DEFAULT_PORT = 443` |

Output log akan menampilkan sumber port yang digunakan: `[RELAY] Port source: CLI argument` / `appsettings.json` / `default`

## Alur Kerja

### Host Register

```
HOST                           RELAY
 │ TCP Connect (443)             │
 ├──────────────────────────────►│
 │ RELAY_REGISTER_HOST           │
 │ {NamaPerangkat, Password?}    │
 ├──────────────────────────────►│
 │                               │ Generate HostCode
 │ RELAY_REGISTER_HOST_OK        │
 │ {HostCode: "XY7K2M"}          │
 │◄──────────────────────────────┤
 │                               │
 │ RELAY_HOST_HEARTBEAT (30s)    │
 ├──────────────────────────────►│ (repeat)
```

### Tamu Connect

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
 │ [Streaming via Relay - transparent forwarding] │
```

## Keamanan

| Aspek | Implementasi |
|-------|--------------|
| **Single Instance** | Mutex mencegah multiple server |
| **Port 443** | Melewati kebanyakan firewall |
| **HostCode** | 6 karakter = 2.1 miliar kombinasi |
| **Password** | Opsional, untuk akses terbatas |
| **Rate Limiting** | MaxHostsPerIP, MaxTamusPerIP |

## Troubleshooting

| Masalah | Solusi |
|---------|--------|
| Port 443 sudah digunakan | Matikan web server lain atau gunakan port custom |
| Access denied port 443 | Jalankan sebagai Administrator |
| Host tidak terdaftar | Cek koneksi internet Host, pastikan heartbeat jalan |
| Tamu tidak bisa connect | Verifikasi HostCode benar, Host masih online |

## Dokumentasi Terkait

| Topik | File |
|-------|------|
| **Booku Remote (WPF)** | `.claude/rules/Booku Remote/overview.md` |
| **Booku Remote Android** | `.claude/rules/Booku Remote Android/overview.md` |
| **Protokol lengkap** | `.claude/rules/Booku Remote/overview.md` |
