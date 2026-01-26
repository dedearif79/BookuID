Option Explicit On
Option Strict On

Imports System.IO

Namespace Booku_Remote

    ''' <summary>
    ''' Parser untuk H.264 NAL (Network Abstraction Layer) units.
    ''' Memisahkan stream H.264 menjadi individual NAL units untuk chunking.
    ''' </summary>
    Public Class cls_NalParser

#Region "NAL Unit Types"

        ''' <summary>NAL unit type: Non-IDR Slice (P-frame)</summary>
        Public Const NAL_TYPE_SLICE As Integer = 1

        ''' <summary>NAL unit type: Slice Data Partition A</summary>
        Public Const NAL_TYPE_DPA As Integer = 2

        ''' <summary>NAL unit type: Slice Data Partition B</summary>
        Public Const NAL_TYPE_DPB As Integer = 3

        ''' <summary>NAL unit type: Slice Data Partition C</summary>
        Public Const NAL_TYPE_DPC As Integer = 4

        ''' <summary>NAL unit type: IDR Slice (I-frame / Keyframe)</summary>
        Public Const NAL_TYPE_IDR As Integer = 5

        ''' <summary>NAL unit type: Supplemental Enhancement Information</summary>
        Public Const NAL_TYPE_SEI As Integer = 6

        ''' <summary>NAL unit type: Sequence Parameter Set</summary>
        Public Const NAL_TYPE_SPS As Integer = 7

        ''' <summary>NAL unit type: Picture Parameter Set</summary>
        Public Const NAL_TYPE_PPS As Integer = 8

        ''' <summary>NAL unit type: Access Unit Delimiter</summary>
        Public Const NAL_TYPE_AUD As Integer = 9

        ''' <summary>NAL unit type: End of Sequence</summary>
        Public Const NAL_TYPE_END_SEQ As Integer = 10

        ''' <summary>NAL unit type: End of Stream</summary>
        Public Const NAL_TYPE_END_STREAM As Integer = 11

        ''' <summary>NAL unit type: Filler Data</summary>
        Public Const NAL_TYPE_FILLER As Integer = 12

#End Region

#Region "Private Fields"

        ''' <summary>Buffer untuk mengumpulkan data dari encoder</summary>
        Private _buffer As MemoryStream

        ''' <summary>SPS data terakhir (untuk re-send pada keyframe)</summary>
        Private _lastSPS As Byte()

        ''' <summary>PPS data terakhir (untuk re-send pada keyframe)</summary>
        Private _lastPPS As Byte()

#End Region

#Region "Properties"

        ''' <summary>SPS (Sequence Parameter Set) terakhir yang di-parse</summary>
        Public ReadOnly Property LastSPS As Byte()
            Get
                Return _lastSPS
            End Get
        End Property

        ''' <summary>PPS (Picture Parameter Set) terakhir yang di-parse</summary>
        Public ReadOnly Property LastPPS As Byte()
            Get
                Return _lastPPS
            End Get
        End Property

        ''' <summary>True jika SPS dan PPS sudah tersedia</summary>
        Public ReadOnly Property HasParameterSets As Boolean
            Get
                Return _lastSPS IsNot Nothing AndAlso _lastPPS IsNot Nothing
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            _buffer = New MemoryStream()
        End Sub

#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Menambahkan data H.264 ke buffer dan mengembalikan NAL units yang lengkap.
        ''' </summary>
        ''' <param name="data">Data H.264 dari encoder</param>
        ''' <returns>List NAL units yang lengkap</returns>
        Public Function ParseData(data As Byte()) As List(Of NalUnit)
            If data Is Nothing OrElse data.Length = 0 Then
                Return New List(Of NalUnit)()
            End If

            ' Tambahkan ke buffer
            Dim prevBufferLen = _buffer.Length
            _buffer.Write(data, 0, data.Length)

            System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] ParseData: input={data.Length} bytes, bufferBefore={prevBufferLen}, bufferAfter={_buffer.Length}")

            ' Parse NAL units dari buffer
            Return ExtractNalUnits()
        End Function

        ''' <summary>
        ''' Mendapatkan NAL type dari byte pertama NAL unit.
        ''' </summary>
        ''' <param name="nalHeader">Byte header NAL (setelah start code)</param>
        ''' <returns>NAL unit type (5 bits LSB)</returns>
        Public Shared Function GetNalType(nalHeader As Byte) As Integer
            Return nalHeader And &H1F
        End Function

        ''' <summary>
        ''' Mengecek apakah NAL type adalah keyframe (IDR).
        ''' </summary>
        Public Shared Function IsKeyframe(nalType As Integer) As Boolean
            Return nalType = NAL_TYPE_IDR
        End Function

        ''' <summary>
        ''' Mengecek apakah NAL type adalah parameter set (SPS/PPS).
        ''' </summary>
        Public Shared Function IsParameterSet(nalType As Integer) As Boolean
            Return nalType = NAL_TYPE_SPS OrElse nalType = NAL_TYPE_PPS
        End Function

        ''' <summary>
        ''' Mengecek apakah NAL type adalah video frame (IDR atau non-IDR slice).
        ''' </summary>
        Public Shared Function IsVideoFrame(nalType As Integer) As Boolean
            Return nalType = NAL_TYPE_IDR OrElse nalType = NAL_TYPE_SLICE
        End Function

        ''' <summary>
        ''' Membuat access unit lengkap dengan SPS+PPS+NAL (untuk keyframe).
        ''' </summary>
        ''' <param name="nalUnit">NAL unit (biasanya IDR frame)</param>
        ''' <returns>Byte array dengan SPS+PPS+NAL, atau hanya NAL jika parameter sets belum tersedia</returns>
        Public Function CreateAccessUnitWithParams(nalUnit As NalUnit) As Byte()
            If Not HasParameterSets Then
                System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] CreateAccessUnit: No param sets, returning raw NAL ({nalUnit.RawData.Length} bytes)")
                Return nalUnit.RawData
            End If

            ' Hitung total size
            Dim totalSize = _lastSPS.Length + _lastPPS.Length + nalUnit.RawData.Length

            System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] CreateAccessUnit: SPS={_lastSPS.Length} + PPS={_lastPPS.Length} + NAL={nalUnit.RawData.Length} = {totalSize} bytes")

            Using ms As New MemoryStream(totalSize)
                ms.Write(_lastSPS, 0, _lastSPS.Length)
                ms.Write(_lastPPS, 0, _lastPPS.Length)
                ms.Write(nalUnit.RawData, 0, nalUnit.RawData.Length)
                Return ms.ToArray()
            End Using
        End Function

        ''' <summary>
        ''' Mereset parser state.
        ''' </summary>
        Public Sub Reset()
            _buffer = New MemoryStream()
            _lastSPS = Nothing
            _lastPPS = Nothing
        End Sub

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Mengekstrak NAL units dari buffer.
        ''' NAL units dipisahkan oleh start code: 0x00 0x00 0x01 atau 0x00 0x00 0x00 0x01
        ''' </summary>
        Private Function ExtractNalUnits() As List(Of NalUnit)
            Dim result As New List(Of NalUnit)()

            Dim bufferData = _buffer.ToArray()
            If bufferData.Length < 4 Then
                System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] ExtractNalUnits: buffer too small ({bufferData.Length} bytes)")
                Return result
            End If

            Dim startPositions As New List(Of Integer)()

            ' Cari semua posisi start code
            For i = 0 To bufferData.Length - 4
                ' Cek 4-byte start code: 00 00 00 01
                If bufferData(i) = 0 AndAlso bufferData(i + 1) = 0 AndAlso
                   bufferData(i + 2) = 0 AndAlso bufferData(i + 3) = 1 Then
                    startPositions.Add(i)
                    i += 3 ' Skip ahead
                    ' Cek 3-byte start code: 00 00 01
                ElseIf bufferData(i) = 0 AndAlso bufferData(i + 1) = 0 AndAlso bufferData(i + 2) = 1 Then
                    startPositions.Add(i)
                    i += 2 ' Skip ahead
                End If
            Next

            System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] ExtractNalUnits: bufferLen={bufferData.Length}, startCodes={startPositions.Count}")

            ' Ekstrak NAL units
            For i = 0 To startPositions.Count - 2
                Dim startPos = startPositions(i)
                Dim endPos = startPositions(i + 1)
                Dim nalLen = endPos - startPos

                Dim nalData(nalLen - 1) As Byte
                Array.Copy(bufferData, startPos, nalData, 0, nalData.Length)

                Dim nalUnit = ParseNalUnit(nalData)
                If nalUnit IsNot Nothing Then
                    result.Add(nalUnit)
                    System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] Extracted NAL #{i}: type={nalUnit.NalType} ({nalUnit.TypeName}), size={nalData.Length}")

                    ' Simpan SPS/PPS untuk keyframe
                    If nalUnit.NalType = NAL_TYPE_SPS Then
                        _lastSPS = nalData
                        System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] Stored SPS: {nalData.Length} bytes")
                    ElseIf nalUnit.NalType = NAL_TYPE_PPS Then
                        _lastPPS = nalData
                        System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] Stored PPS: {nalData.Length} bytes")
                    End If
                End If
            Next

            ' Pertahankan sisa data di buffer (NAL terakhir yang belum lengkap)
            If startPositions.Count > 0 Then
                Dim lastStart = startPositions(startPositions.Count - 1)
                Dim remaining = bufferData.Length - lastStart

                _buffer = New MemoryStream()
                _buffer.Write(bufferData, lastStart, remaining)
                System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] Remaining in buffer: {remaining} bytes (from position {lastStart})")
            Else
                System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] No start codes found, keeping all {bufferData.Length} bytes in buffer")
            End If

            System.Diagnostics.Debug.WriteLine($"[NAL-PARSER] Result: {result.Count} NAL units extracted")
            Return result
        End Function

        ''' <summary>
        ''' Parse satu NAL unit dari raw data.
        ''' </summary>
        Private Function ParseNalUnit(data As Byte()) As NalUnit
            If data Is Nothing OrElse data.Length < 4 Then
                Return Nothing
            End If

            ' Cari posisi header setelah start code
            Dim headerPos As Integer
            If data(0) = 0 AndAlso data(1) = 0 AndAlso data(2) = 0 AndAlso data(3) = 1 Then
                headerPos = 4
            ElseIf data(0) = 0 AndAlso data(1) = 0 AndAlso data(2) = 1 Then
                headerPos = 3
            Else
                Return Nothing
            End If

            If headerPos >= data.Length Then
                Return Nothing
            End If

            Dim nalHeader = data(headerPos)
            Dim nalType = GetNalType(nalHeader)

            Return New NalUnit With {
                .RawData = data,
                .NalType = nalType,
                .IsKeyframe = IsKeyframe(nalType),
                .IsParameterSet = IsParameterSet(nalType)
            }
        End Function

#End Region

    End Class

#Region "NalUnit Class"

    ''' <summary>
    ''' Representasi satu NAL unit.
    ''' </summary>
    Public Class NalUnit

        ''' <summary>Raw data NAL unit (termasuk start code)</summary>
        Public Property RawData As Byte()

        ''' <summary>NAL unit type</summary>
        Public Property NalType As Integer

        ''' <summary>True jika ini adalah keyframe (IDR)</summary>
        Public Property IsKeyframe As Boolean

        ''' <summary>True jika ini adalah parameter set (SPS/PPS)</summary>
        Public Property IsParameterSet As Boolean

        ''' <summary>Ukuran data dalam bytes</summary>
        Public ReadOnly Property Size As Integer
            Get
                Return If(RawData IsNot Nothing, RawData.Length, 0)
            End Get
        End Property

        ''' <summary>Mendapatkan nama NAL type untuk debugging</summary>
        Public ReadOnly Property TypeName As String
            Get
                Select Case NalType
                    Case cls_NalParser.NAL_TYPE_SLICE : Return "SLICE"
                    Case cls_NalParser.NAL_TYPE_IDR : Return "IDR"
                    Case cls_NalParser.NAL_TYPE_SEI : Return "SEI"
                    Case cls_NalParser.NAL_TYPE_SPS : Return "SPS"
                    Case cls_NalParser.NAL_TYPE_PPS : Return "PPS"
                    Case cls_NalParser.NAL_TYPE_AUD : Return "AUD"
                    Case Else : Return $"TYPE_{NalType}"
                End Select
            End Get
        End Property

    End Class

#End Region

End Namespace
