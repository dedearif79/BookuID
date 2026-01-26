# Booku Remote Relay

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote Relay** (folder `BookuID/Booku Remote Relay/`)

## Deskripsi

**Booku Remote Relay** adalah server relay untuk koneksi remote desktop via internet. Server ini berjalan di VPS dan berfungsi sebagai perantara antara Host dan Tamu yang tidak berada dalam jaringan LAN yang sama.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Remote Relay |
| **Output** | `Booku Remote Relay` (Console App, tanpa .exe) |
| **Namespace** | `BookuRemoteRelay` |
| **Target Framework** | .NET 8.0 |
| **Runtime** | **win-x64** (self-contained) |
| **Port TCP** | 45680 (control plane) |
| **Port UDP** | 45681 (video streaming) |

## Fitur Keamanan & Optimasi

| Fitur | Status | Implementasi |
|-------|--------|--------------|
| **Single Instance** | ✅ | Windows Mutex (`Global\BookuRemoteRelayServer`) |
| **Obfuscation** | ✅ | IL Trimming (`PublishTrimmed=true`) |
| **Single Executable** | ✅ | Self-contained, compressed |
| **Compression** | ✅ | `EnableCompressionInSingleFile=true` |
| **Cross-Platform** | ✅ | Windows x64 (primary target untuk VPS) |

## Struktur File

```
Booku Remote Relay/
├── Booku Remote Relay.csproj     # Project file dengan Release config
├── Program.cs                     # Entry point + Single Instance (PID file lock)
├── appsettings.json              # Konfigurasi server
├── PUBLISH-RELEASE.sh            # Script build Release (Linux)
├── PUBLISH-RELEASE.bat           # Script build Release (Windows, legacy)
├── booku-relay.service           # Systemd service file untuk Linux
│
├── Models/                        # Model classes
│   ├── Enums.cs                   # TipePaket enum
│   └── PaketData.cs               # Struktur paket data
│
└── Services/                      # Business logic
    ├── TcpListenerService.cs      # TCP listener pada port 45680 (control)
    ├── UdpListenerService.cs      # UDP listener pada port 45681 (video)
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
    "Port": 45680,
    "PortUdp": 45681,
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
| `Port` | 45680 | Port TCP untuk listen (control plane) |
| `PortUdp` | 45681 | Port UDP untuk video streaming (data plane) |
| `HostExpiryMinutes` | 60 | Waktu expired Host tanpa heartbeat |
| `HeartbeatIntervalSeconds` | 30 | Interval heartbeat dari Host |
| `SessionTimeoutMinutes` | 30 | Timeout sesi tanpa aktivitas |
| `MaxHostsPerIP` | 5 | Maksimal Host per IP address |
| `MaxTamusPerIP` | 10 | Maksimal Tamu per IP address |

## Arsitektur

### Hybrid TCP/UDP Architecture

Relay Server menggunakan arsitektur **hybrid TCP/UDP**:

| Layer | Port | Protokol | Kegunaan |
|-------|------|----------|----------|
| **Control Plane** | 45680 | TCP | Login, heartbeat, session management |
| **Data Plane** | 45681 | UDP | Video frames forwarding |

```
┌─────────────────────────────────────────────────────────────┐
│              RELAY SERVER (VPS)                             │
│                                                             │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────┐ │
│  │ Connection  │  │   Packet    │  │    Protocol         │ │
│  │ Manager     │  │   Router    │  │    Service          │ │
│  └─────────────┘  └─────────────┘  └─────────────────────┘ │
│         │                │                    │             │
│         └────────────────┴────────────────────┘             │
│                          │                                  │
│      ┌───────────────────┼───────────────────┐              │
│      │                   │                   │              │
│  ┌───┴───────────┐  ┌────┴──────────┐        │              │
│  │ TcpListener   │  │ UdpListener   │        │              │
│  │ (Port 45680)  │  │ (Port 45681)  │        │              │
│  │ Control Plane │  │ Data Plane    │        │              │
│  └───────────────┘  └───────────────┘        │              │
└─────────────────────────────────────────────────────────────┘
         ▲                    ▲                ▲
         │ TCP 45680          │ UDP 45681      │ TCP 45680
         │                    │                │
┌────────┴────────┐           │       ┌────────┴────────┐
│      HOST       │───────────┘       │       TAMU      │
│  (Windows WPF)  │                   │ (Windows/Android)│
│  Sends frames   │◄──────────────────│  Receives frames │
└─────────────────┘   UDP forwarding  └─────────────────┘
```

## Komponen Utama

### 1. TcpListenerService

Mendengarkan koneksi TCP pada port 45680 untuk control plane.

| Fungsi | Deskripsi |
|--------|-----------|
| `StartAsync()` | Mulai TCP listener |
| `Stop()` | Hentikan listener |
| `HandleClientAsync()` | Handle koneksi baru |

### 2. UdpListenerService

Mendengarkan dan meneruskan paket UDP pada port 45681 untuk video streaming.

| Fungsi | Deskripsi |
|--------|-----------|
| `StartAsync()` | Mulai UDP listener |
| `Stop()` | Hentikan listener |
| `ForwardPacketAsync()` | Forward UDP packet berdasarkan SessionId |
| `RegisterEndpoint()` | Register endpoint Host/Tamu untuk session |

**Cara Kerja UDP Forwarding:**
1. Host/Tamu mengirim "registration packet" (header-only, FrameId=0) ke relay
2. Relay menyimpan endpoint (IP:Port) berdasarkan SessionId
3. Saat Host mengirim frame, relay lookup SessionId dan forward ke Tamu
4. Sesi UDP expire otomatis setelah timeout tanpa aktivitas

### 3. ConnectionManager

Mengelola koneksi Host dan Tamu yang terdaftar.

| Fungsi | Deskripsi |
|--------|-----------|
| `RegisterHost()` | Daftarkan Host baru, generate HostCode |
| `UnregisterHost()` | Hapus Host dari daftar |
| `GetHostByCode()` | Cari Host berdasarkan HostCode |
| `RegisterTamu()` | Daftarkan Tamu untuk sesi |
| `CreateSession()` | Buat sesi Host-Tamu |

### 4. PacketRouter

Routing paket antara Host dan Tamu.

| Fungsi | Deskripsi |
|--------|-----------|
| `RoutePacket()` | Forward paket ke tujuan |
| `HandleRelayPacket()` | Handle paket relay (register, query, dll) |

### 5. ProtocolService

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

```batch
# Menggunakan script batch (Windows)
PUBLISH-RELEASE.bat

# Atau manual
dotnet publish "Booku Remote Relay.csproj" -c Release -o "bin/Publish"
```

### Output

```
bin/Publish/Booku Remote Relay.exe (~11 MB, self-contained)
```

### Deploy ke VPS Windows

1. Copy `Booku Remote Relay.exe` ke VPS Windows (misal ke `C:\BookuID\Relay\`)
2. Pastikan port 45680 tidak digunakan aplikasi lain
3. Jalankan executable
4. Server akan listen pada `0.0.0.0:45680`

### Menjalankan Server (Manual)

```batch
# Jalankan dengan port default (45680)
"Booku Remote Relay.exe"

# Jalankan dengan port custom via CLI argument
"Booku Remote Relay.exe" 8080
```

### Menjalankan sebagai Windows Service

Gunakan NSSM (Non-Sucking Service Manager) atau Task Scheduler:

```batch
# Menggunakan NSSM
nssm install BookuRelay "C:\BookuID\Relay\Booku Remote Relay.exe"
nssm start BookuRelay

# Atau menggunakan Task Scheduler (at startup)
schtasks /create /tn "Booku Remote Relay" /tr "C:\BookuID\Relay\Booku Remote Relay.exe" /sc onstart /ru SYSTEM
```

### Prioritas Konfigurasi Port

Server membaca port dengan prioritas berikut:

| Prioritas | Sumber | Contoh |
|-----------|--------|--------|
| 1 (Tertinggi) | **CLI Argument** | `Booku Remote Relay.exe 8080` |
| 2 | **appsettings.json** | `"RelayServer": { "Port": 45680 }` |
| 3 (Terendah) | **Default Constant** | `DEFAULT_PORT = 45680` |

Output log akan menampilkan sumber port yang digunakan: `[RELAY] Port source: CLI argument` / `appsettings.json` / `default`

## Alur Kerja

### Host Register

```
HOST                           RELAY
 │ TCP Connect (45680)            │
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
| **Single Instance** | PID file lock mencegah multiple server |
| **Port TCP 45680** | Port kustom untuk control plane |
| **Port UDP 45681** | Port kustom untuk video streaming |
| **SessionId** | 32-bit hash dari KunciSesi untuk UDP routing (djb2 algorithm) |
| **HostCode** | 6 karakter = 2.1 miliar kombinasi |
| **Password** | Opsional, untuk akses terbatas |
| **Rate Limiting** | MaxHostsPerIP, MaxTamusPerIP |

### Catatan Penting: SessionId Hash Algorithm

Semua komponen (Relay, Host WPF, Android) **HARUS** menggunakan algoritma hash yang sama untuk SessionId:

| Komponen | File | Method |
|----------|------|--------|
| **Relay** | `ConnectionManager.cs` | `GenerateUdpSessionId()` |
| **Host (VB.NET)** | `mdl_UdpStreaming.vb` | `GenerateSessionId()` |
| **Android (C#)** | `UdpStreamingService.cs` | `GenerateSessionId()` |

**Algoritma:** djb2 hash (deterministic, cross-platform)

```csharp
uint hash = 5381;
foreach (char c in sessionKey)
{
    hash = ((hash << 5) + hash) ^ c;
}
return (int)(hash & 0x7FFFFFFF);
```

> **PENTING:** Jangan gunakan `GetHashCode()` karena hasilnya **TIDAK konsisten** antar platform (.NET Windows vs Android). Hal ini menyebabkan UDP packet tidak bisa di-route dengan benar.

## Single Instance (Mutex)

Server menggunakan Windows Mutex untuk mencegah multiple instance:

| Komponen | Nilai |
|----------|-------|
| **Mutex Name** | `Global\BookuRemoteRelayServer` |
| **Scope** | System-wide (semua user session) |

Mutex otomatis di-release saat server shutdown dengan Ctrl+C atau saat process terminated.

## Troubleshooting

| Masalah | Solusi |
|---------|--------|
| Port 45680 sudah digunakan | Matikan aplikasi lain atau gunakan port custom |
| Port 45681 (UDP) blocked | Buka port UDP 45681 di firewall |
| Host tidak terdaftar | Cek koneksi internet Host, pastikan heartbeat jalan |
| Tamu tidak bisa connect | Verifikasi HostCode benar, Host masih online |
| Video tidak streaming | Pastikan UDP registration packet terkirim |
| "Server sudah berjalan" padahal tidak | Cek Task Manager, kill process `Booku Remote Relay.exe` |
| Firewall blocking | Buka port TCP 45680 dan UDP 45681 di Windows Firewall |

## Dokumentasi Terkait

| Topik | File |
|-------|------|
| **Booku Remote (WPF)** | `.claude/rules/Booku Remote/overview.md` |
| **Booku Remote Android** | `.claude/rules/Booku Remote Android/overview.md` |
| **Protokol lengkap** | `.claude/rules/Booku Remote/overview.md` |
