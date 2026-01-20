Option Explicit On
Option Strict On

Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Threading.Tasks

''' <summary>
''' Modul untuk penemuan perangkat di LAN menggunakan UDP broadcast.
''' </summary>
Public Module mdl_PenemuanPerangkat

#Region "Events"

    ''' <summary>Event ketika perangkat ditemukan</summary>
    Public Event PerangkatDitemukan(perangkat As cls_PerangkatLAN)

    ''' <summary>Event ketika ada permintaan discovery masuk (untuk Host)</summary>
    Public Event PermintaanDiscoveryMasuk(alamatPengirim As IPEndPoint)

    ''' <summary>Event ketika terjadi error</summary>
    Public Event ErrorDiscovery(pesan As String)

#End Region

#Region "Private Variables"

    Private _udpClient As UdpClient
    Private _udpListener As UdpClient
    Private _sedangMendengarkan As Boolean = False
    Private _cancellationTokenSource As CancellationTokenSource
    Private _daftarPerangkat As New List(Of cls_PerangkatLAN)
    Private ReadOnly _lockDaftar As New Object

#End Region

#Region "Properties"

    ''' <summary>
    ''' Mendapatkan daftar perangkat yang ditemukan.
    ''' </summary>
    Public ReadOnly Property DaftarPerangkat As List(Of cls_PerangkatLAN)
        Get
            SyncLock _lockDaftar
                Return _daftarPerangkat.ToList()
            End SyncLock
        End Get
    End Property

    ''' <summary>
    ''' Menunjukkan apakah sedang mendengarkan discovery.
    ''' </summary>
    Public ReadOnly Property SedangMendengarkan As Boolean
        Get
            Return _sedangMendengarkan
        End Get
    End Property

#End Region

#Region "Host Mode - Mendengarkan Discovery"

    ''' <summary>
    ''' Mulai mendengarkan permintaan discovery (untuk mode Host).
    ''' </summary>
    Public Sub MulaiMendengarkanDiscovery()
        If _sedangMendengarkan Then Return

        Try
            _cancellationTokenSource = New CancellationTokenSource()
            _udpListener = New UdpClient()
            _udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)
            _udpListener.Client.Bind(New IPEndPoint(IPAddress.Any, PORT_DISCOVERY))

            _sedangMendengarkan = True

            ' Mulai task untuk mendengarkan
            Task.Run(Async Function()
                         Await DengarkanDiscoveryAsync(_cancellationTokenSource.Token)
                     End Function)

        Catch ex As Exception
            RaiseEvent ErrorDiscovery($"Gagal memulai listener discovery: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Task async untuk mendengarkan permintaan discovery.
    ''' </summary>
    Private Async Function DengarkanDiscoveryAsync(token As CancellationToken) As Task
        While _sedangMendengarkan AndAlso Not token.IsCancellationRequested
            Try
                ' Terima data UDP
                Dim result = Await _udpListener.ReceiveAsync()
                Dim data = BytesKeString(result.Buffer)

                ' Parse paket
                Dim paket = DeserializePaket(data)
                If paket IsNot Nothing AndAlso paket.TipePaket = TipePaket.BROADCAST_DISCOVERY Then
                    If paket.Payload = MAGIC_DISCOVERY Then
                        ' Kirim respon
                        RaiseEvent PermintaanDiscoveryMasuk(result.RemoteEndPoint)
                        KirimResponDiscovery(result.RemoteEndPoint)
                    End If
                End If

            Catch ex As ObjectDisposedException
                ' Socket ditutup, keluar dari loop
                Exit While
            Catch ex As SocketException
                If Not token.IsCancellationRequested Then
                    RaiseEvent ErrorDiscovery($"Error saat mendengarkan: {ex.Message}")
                End If
            Catch ex As Exception
                If Not token.IsCancellationRequested Then
                    RaiseEvent ErrorDiscovery($"Error: {ex.Message}")
                End If
            End Try
        End While
    End Function

    ''' <summary>
    ''' Kirim respon discovery ke perangkat yang meminta.
    ''' </summary>
    Private Sub KirimResponDiscovery(tujuan As IPEndPoint)
        Try
            ' Buat info perangkat ini
            Dim infoPerangkat As New cls_PerangkatLAN With {
                .NamaPerangkat = NamaPerangkatIni,
                .AlamatIP = AlamatIPLokal,
                .PortTCP = PORT_KONEKSI,
                .Status = If(StatusKoneksiSaatIni = StatusKoneksi.TERHUBUNG, StatusPerangkat.SIBUK, StatusPerangkat.TERSEDIA),
                .VersiProtokol = VERSI_PROTOKOL
            }

            ' Buat dan kirim paket respon
            Dim paketRespon = BuatPaketResponDiscovery(infoPerangkat)
            Dim data = StringKeBytes(SerializePaket(paketRespon))

            Using sendClient As New UdpClient()
                sendClient.Send(data, data.Length, tujuan)
            End Using

        Catch ex As Exception
            RaiseEvent ErrorDiscovery($"Gagal mengirim respon discovery: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Tamu Mode - Mencari Perangkat"

    ''' <summary>
    ''' Mencari perangkat Host di LAN (untuk mode Tamu).
    ''' </summary>
    Public Async Function CariPerangkatAsync() As Task(Of List(Of cls_PerangkatLAN))
        SyncLock _lockDaftar
            _daftarPerangkat.Clear()
        End SyncLock

        Try
            ' Buat UDP client untuk broadcast
            Using client As New UdpClient()
                client.EnableBroadcast = True

                ' Buat paket discovery
                Dim paket = BuatPaketDiscoveryBroadcast()
                Dim data = StringKeBytes(SerializePaket(paket))

                ' Broadcast ke semua alamat di subnet
                Dim broadcastEndpoint As New IPEndPoint(IPAddress.Broadcast, PORT_DISCOVERY)
                Await client.SendAsync(data, data.Length, broadcastEndpoint)

                ' Tunggu respon dengan timeout
                Dim endTime = DateTime.Now.AddMilliseconds(TIMEOUT_DISCOVERY)
                client.Client.ReceiveTimeout = TIMEOUT_DISCOVERY

                While DateTime.Now < endTime
                    Try
                        Dim result = Await TerimaResponDenganTimeoutAsync(client, CInt((endTime - DateTime.Now).TotalMilliseconds))
                        If result IsNot Nothing Then
                            ProcessResponDiscovery(result.Value.data, result.Value.remoteEndPoint)
                        End If
                    Catch ex As SocketException When ex.SocketErrorCode = SocketError.TimedOut
                        Exit While
                    Catch
                        Exit While
                    End Try
                End While
            End Using

        Catch ex As Exception
            RaiseEvent ErrorDiscovery($"Gagal mencari perangkat: {ex.Message}")
        End Try

        Return DaftarPerangkat
    End Function

    ''' <summary>
    ''' Helper untuk menerima respon dengan timeout.
    ''' </summary>
    Private Async Function TerimaResponDenganTimeoutAsync(client As UdpClient, timeout As Integer) As Task(Of (data As Byte(), remoteEndPoint As IPEndPoint)?)
        Try
            Dim cts As New CancellationTokenSource(timeout)
            Dim receiveTask = client.ReceiveAsync()

            Dim completedTask = Await Task.WhenAny(receiveTask, Task.Delay(timeout, cts.Token))
            If completedTask Is receiveTask Then
                cts.Cancel()
                Dim result = Await receiveTask
                Return (result.Buffer, result.RemoteEndPoint)
            End If
        Catch
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Proses respon discovery yang diterima.
    ''' </summary>
    Private Sub ProcessResponDiscovery(data As Byte(), remoteEndPoint As IPEndPoint)
        Try
            Dim json = BytesKeString(data)
            Dim paket = DeserializePaket(json)

            If paket IsNot Nothing AndAlso paket.TipePaket = TipePaket.RESPON_DISCOVERY Then
                Dim perangkat = DeserializePerangkatLAN(paket.Payload)
                If perangkat IsNot Nothing Then
                    ' Pastikan alamat IP sesuai dengan pengirim
                    perangkat.AlamatIP = remoteEndPoint.Address.ToString()
                    perangkat.WaktuTerdeteksi = DateTime.Now

                    SyncLock _lockDaftar
                        ' Cek apakah sudah ada di daftar
                        Dim existing = _daftarPerangkat.FirstOrDefault(Function(p) p.AlamatIP = perangkat.AlamatIP)
                        If existing Is Nothing Then
                            _daftarPerangkat.Add(perangkat)
                        Else
                            ' Update info
                            existing.NamaPerangkat = perangkat.NamaPerangkat
                            existing.Status = perangkat.Status
                            existing.WaktuTerdeteksi = perangkat.WaktuTerdeteksi
                        End If
                    End SyncLock

                    RaiseEvent PerangkatDitemukan(perangkat)
                End If
            End If

        Catch ex As Exception
            RaiseEvent ErrorDiscovery($"Gagal memproses respon: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Cleanup"

    ''' <summary>
    ''' Hentikan semua aktivitas discovery.
    ''' </summary>
    Public Sub HentikanDiscovery()
        _sedangMendengarkan = False

        Try
            _cancellationTokenSource?.Cancel()
        Catch
        End Try

        Try
            _udpClient?.Close()
            _udpClient?.Dispose()
        Catch
        End Try

        Try
            _udpListener?.Close()
            _udpListener?.Dispose()
        Catch
        End Try

        _udpClient = Nothing
        _udpListener = Nothing
    End Sub

    ''' <summary>
    ''' Bersihkan daftar perangkat.
    ''' </summary>
    Public Sub BersihkanDaftarPerangkat()
        SyncLock _lockDaftar
            _daftarPerangkat.Clear()
        End SyncLock
    End Sub

#End Region

End Module
