Option Explicit On
Option Strict On

Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Serialization

''' <summary>
''' Kelas untuk menyimpan pengaturan port dan alamat server.
''' Settings disimpan ke file JSON di folder AppData.
''' </summary>
Public Class cls_SetelPort

#Region "Konstanta Default"

    ''' <summary>Port UDP default untuk discovery perangkat di LAN</summary>
    Public Const DEFAULT_PORT_DISCOVERY As Integer = 45678

    ''' <summary>Port TCP default untuk koneksi remote LAN</summary>
    Public Const DEFAULT_PORT_KONEKSI As Integer = 45679

    ''' <summary>Port TCP default untuk relay server (internet)</summary>
    Public Const DEFAULT_PORT_RELAY As Integer = 45680

    ''' <summary>Alamat IP default relay server</summary>
    Public Const DEFAULT_RELAY_SERVER_IP As String = "155.117.43.209"

    ''' <summary>Nama file settings</summary>
    Private Const NAMA_FILE_SETTINGS As String = "port-settings.json"

    ''' <summary>Nama folder aplikasi di AppData</summary>
    Private Const NAMA_FOLDER_APP As String = "BookuID\Booku Remote"

#End Region

#Region "Properties"

    ''' <summary>Port UDP untuk discovery perangkat di LAN</summary>
    <JsonPropertyName("portDiscovery")>
    Public Property PortDiscovery As Integer = DEFAULT_PORT_DISCOVERY

    ''' <summary>Port TCP untuk koneksi remote LAN</summary>
    <JsonPropertyName("portKoneksi")>
    Public Property PortKoneksi As Integer = DEFAULT_PORT_KONEKSI

    ''' <summary>Port TCP untuk relay server (internet)</summary>
    <JsonPropertyName("portRelay")>
    Public Property PortRelay As Integer = DEFAULT_PORT_RELAY

    ''' <summary>Alamat IP relay server</summary>
    <JsonPropertyName("relayServerIP")>
    Public Property RelayServerIP As String = DEFAULT_RELAY_SERVER_IP

#End Region

#Region "Computed Properties"

    ''' <summary>
    ''' Mendapatkan alamat lengkap relay server (IP:Port).
    ''' </summary>
    <JsonIgnore>
    Public ReadOnly Property RelayServerAddress As String
        Get
            Return $"{RelayServerIP}:{PortRelay}"
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan path lengkap file settings.
    ''' </summary>
    <JsonIgnore>
    Public Shared ReadOnly Property PathFileSettings As String
        Get
            Dim folderAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Return Path.Combine(folderAppData, NAMA_FOLDER_APP, NAMA_FILE_SETTINGS)
        End Get
    End Property

    ''' <summary>
    ''' Mendapatkan path folder settings.
    ''' </summary>
    <JsonIgnore>
    Public Shared ReadOnly Property PathFolderSettings As String
        Get
            Dim folderAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Return Path.Combine(folderAppData, NAMA_FOLDER_APP)
        End Get
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        ' Gunakan nilai default
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Reset semua pengaturan ke nilai default.
    ''' </summary>
    Public Sub ResetKeDefault()
        PortDiscovery = DEFAULT_PORT_DISCOVERY
        PortKoneksi = DEFAULT_PORT_KONEKSI
        PortRelay = DEFAULT_PORT_RELAY
        RelayServerIP = DEFAULT_RELAY_SERVER_IP
    End Sub

    ''' <summary>
    ''' Simpan pengaturan ke file JSON.
    ''' </summary>
    ''' <returns>True jika berhasil, False jika gagal</returns>
    Public Function SimpanKeFile() As Boolean
        Try
            ' Pastikan folder ada
            Dim folderPath = PathFolderSettings
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            ' Serialize ke JSON dengan format yang rapi
            Dim options As New JsonSerializerOptions With {
                .WriteIndented = True
            }
            Dim jsonString = JsonSerializer.Serialize(Me, options)

            ' Tulis ke file
            File.WriteAllText(PathFileSettings, jsonString)
            Return True

        Catch ex As Exception
            Console.WriteLine($"[SetelPort] Gagal menyimpan settings: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Load pengaturan dari file JSON. Jika file tidak ada, gunakan nilai default.
    ''' </summary>
    ''' <returns>Instance cls_SetelPort dengan nilai dari file atau default</returns>
    Public Shared Function MuatDariFile() As cls_SetelPort
        Try
            Dim filePath = PathFileSettings

            ' Jika file tidak ada, kembalikan instance baru dengan default
            If Not File.Exists(filePath) Then
                Console.WriteLine("[SetelPort] File settings tidak ditemukan, menggunakan nilai default.")
                Return New cls_SetelPort()
            End If

            ' Baca dan deserialize JSON
            Dim jsonString = File.ReadAllText(filePath)
            Dim settings = JsonSerializer.Deserialize(Of cls_SetelPort)(jsonString)

            If settings Is Nothing Then
                Console.WriteLine("[SetelPort] Gagal deserialize, menggunakan nilai default.")
                Return New cls_SetelPort()
            End If

            ' Validasi nilai port
            settings.ValidasiNilai()

            Console.WriteLine($"[SetelPort] Settings dimuat: Discovery={settings.PortDiscovery}, Koneksi={settings.PortKoneksi}, Relay={settings.PortRelay}, IP={settings.RelayServerIP}")
            Return settings

        Catch ex As Exception
            Console.WriteLine($"[SetelPort] Error saat memuat settings: {ex.Message}")
            Return New cls_SetelPort()
        End Try
    End Function

    ''' <summary>
    ''' Validasi dan koreksi nilai port yang tidak valid.
    ''' </summary>
    Private Sub ValidasiNilai()
        ' Validasi port (harus antara 1-65535)
        If PortDiscovery < 1 OrElse PortDiscovery > 65535 Then
            PortDiscovery = DEFAULT_PORT_DISCOVERY
        End If

        If PortKoneksi < 1 OrElse PortKoneksi > 65535 Then
            PortKoneksi = DEFAULT_PORT_KONEKSI
        End If

        If PortRelay < 1 OrElse PortRelay > 65535 Then
            PortRelay = DEFAULT_PORT_RELAY
        End If

        ' Validasi IP (tidak boleh kosong)
        If String.IsNullOrWhiteSpace(RelayServerIP) Then
            RelayServerIP = DEFAULT_RELAY_SERVER_IP
        End If
    End Sub

    ''' <summary>
    ''' Validasi apakah port memerlukan hak administrator (port di bawah 1024).
    ''' </summary>
    ''' <param name="port">Port yang akan divalidasi</param>
    ''' <returns>True jika port memerlukan hak admin</returns>
    Public Shared Function PerluHakAdmin(port As Integer) As Boolean
        Return port < 1024
    End Function

    ''' <summary>
    ''' Cek apakah settings saat ini sama dengan default.
    ''' </summary>
    ''' <returns>True jika semua nilai sama dengan default</returns>
    Public Function AdalahDefault() As Boolean
        Return PortDiscovery = DEFAULT_PORT_DISCOVERY AndAlso
               PortKoneksi = DEFAULT_PORT_KONEKSI AndAlso
               PortRelay = DEFAULT_PORT_RELAY AndAlso
               RelayServerIP = DEFAULT_RELAY_SERVER_IP
    End Function

    ''' <summary>
    ''' Override ToString untuk debugging.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return $"SetelPort: Discovery={PortDiscovery}, Koneksi={PortKoneksi}, Relay={PortRelay}, IP={RelayServerIP}"
    End Function

#End Region

End Class
