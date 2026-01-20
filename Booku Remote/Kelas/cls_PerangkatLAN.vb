Option Explicit On
Option Strict On

''' <summary>
''' Kelas untuk menyimpan informasi perangkat yang ditemukan di LAN.
''' </summary>
Public Class cls_PerangkatLAN

#Region "Properties"

    ''' <summary>Nama perangkat (hostname)</summary>
    Public Property NamaPerangkat As String = ""

    ''' <summary>Alamat IP perangkat</summary>
    Public Property AlamatIP As String = ""

    ''' <summary>Port TCP untuk koneksi</summary>
    Public Property PortTCP As Integer = PORT_KONEKSI

    ''' <summary>Status perangkat</summary>
    Public Property Status As StatusPerangkat = StatusPerangkat.TERSEDIA

    ''' <summary>Versi protokol yang digunakan</summary>
    Public Property VersiProtokol As String = ""

    ''' <summary>Timestamp terakhir kali terdeteksi</summary>
    Public Property WaktuTerdeteksi As DateTime = DateTime.Now

#End Region

#Region "Constructor"

    Public Sub New()
    End Sub

    Public Sub New(namaPerangkat As String, alamatIP As String)
        Me.NamaPerangkat = namaPerangkat
        Me.AlamatIP = alamatIP
        Me.WaktuTerdeteksi = DateTime.Now
    End Sub

#End Region

#Region "Display"

    ''' <summary>
    ''' Mendapatkan teks status untuk ditampilkan di UI.
    ''' </summary>
    Public ReadOnly Property TeksStatus As String
        Get
            Select Case Status
                Case StatusPerangkat.TERSEDIA
                    Return "Tersedia"
                Case StatusPerangkat.SIBUK
                    Return "Sibuk"
                Case Else
                    Return "Tidak Tersedia"
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Override ToString untuk debugging.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return $"{NamaPerangkat} ({AlamatIP}) - {TeksStatus}"
    End Function

#End Region

End Class
