# Komponen Utama

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## 1. mdl_VariabelUmum.vb

Berisi konstanta, enum, variabel global, dan Mode Developer detection.

| Kategori | Contoh |
|----------|--------|
| **Mode Developer** | `ModeDeveloper` property — deteksi otomatis berdasarkan path eksekusi |
| **Konstanta Port Default** | `DEFAULT_PORT_DISCOVERY = 45678`, `DEFAULT_PORT_KONEKSI = 45679`, `DEFAULT_PORT_RELAY = 45680`, `DEFAULT_PORT_UDP_VIDEO = 45681` |
| **Konstanta FPS** | `DEFAULT_TARGET_FPS = 15`, `MIN_TARGET_FPS = 5`, `MAX_TARGET_FPS = 30` |
| **Port Aktif (Runtime)** | `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `PortUdpVideoAktif`, `RelayServerIPAktif` |
| **FPS Aktif (Runtime)** | `TargetFPSAktif` — diambil dari `SetelPortAktif.TargetFPS` |
| **Settings Object** | `SetelPortAktif As cls_SetelPort` — instance untuk load/save port & streaming settings |
| **Timeout** | `TIMEOUT_DISCOVERY = 3000ms`, `TIMEOUT_KONEKSI = 10000ms` |
| **Enum Status** | `StatusKoneksi`, `ModeAplikasi`, `TipeAksiMouse` |
| **Variabel Global** | `ModeAplikasiSaatIni`, `StatusKoneksiSaatIni`, `KunciSesiAktif`, `LocalTestMode` |

## 2. mdl_PenemuanPerangkat.vb

Menangani discovery perangkat Host di jaringan LAN menggunakan UDP broadcast.

| Fungsi | Deskripsi |
|--------|-----------|
| `MulaiMendengarkanDiscoveryAsync()` | Host: Mendengarkan broadcast discovery |
| `ScanPerangkatDiLANAsync()` | Tamu: Scan perangkat Host di LAN |
| `BuatPaketDiscoveryBroadcast()` | Membuat paket discovery request |
| `BuatPaketResponDiscovery()` | Membuat paket discovery response |

## 3. mdl_KoneksiJaringan.vb

Menangani koneksi TCP, streaming frame, dan event handling.

| Fungsi | Deskripsi |
|--------|-----------|
| `MulaiMendengarkanKoneksiAsync()` | Host: Listen koneksi TCP masuk |
| `SambungKePeerAsync()` | Tamu: Connect ke Host |
| `MulaiStreamingLayarAsync()` | Host: Mulai kirim frame layar |
| `KirimPaketAsync()` | Kirim paket ke peer |
| `DengarkanPaketAsync()` | Loop mendengarkan paket masuk |

## 4. mdl_TangkapLayar.vb

Menangkap screenshot layar dengan scaling dan cursor drawing.

| Fungsi | Deskripsi |
|--------|-----------|
| `TangkapLayarPenuh()` | Tangkap seluruh layar utama (full resolution) |
| `TangkapLayarDenganSkala()` | Tangkap layar dengan skala tertentu (default 0.6) |
| `TangkapFrameAsync()` | Tangkap screenshot sebagai `cls_FrameLayar` |
| `TangkapLayarKeBgra()` | Tangkap layar dan return BGRA data untuk H.264 |
| `GambarCursorPadaBitmap()` | **Menggambar cursor pada screenshot** |

### Cursor Drawing Feature

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

## 5. mdl_InjeksiInput.vb

Menyuntikkan input keyboard/mouse menggunakan Windows API `SendInput`.

| Fungsi | Deskripsi |
|--------|-----------|
| `InjeksiMouse()` | Injeksi event mouse (move, click, wheel) |
| `InjeksiKeyboard()` | Injeksi event keyboard (key down/up) |

## 6. mdl_Protokol.vb

Serialisasi dan deserialisasi paket data.

| Fungsi | Deskripsi |
|--------|-----------|
| `BuatPaketXxx()` | Membuat paket dengan tipe tertentu |
| `DeserializeXxx()` | Parse payload dari paket |
| `SerializeInputKeyboard()` | Serialize input keyboard ke JSON |
| `SerializeInputMouse()` | Serialize input mouse ke JSON |

## 7. mdl_UdpStreaming.vb

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

## 8. mdl_KoneksiRelay.vb

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

## 9. mdl_Clipboard.vb (Fase 3a)

Menangani sinkronisasi clipboard bidirectional antara Host dan Tamu.

- Sinkronisasi teks dan gambar
- Loop prevention untuk menghindari infinite sync

## 10. mdl_TransferBerkas.vb (Fase 3b)

Menangani transfer berkas antara Host dan Tamu.

- Bidirectional transfer
- Chunking untuk file besar
- Progress tracking
- Cancel support
