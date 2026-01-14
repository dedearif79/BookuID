Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows
Imports bcomm

Public Module mdlPub_Enkripsi

    'Enkripsi/Dekripsi AES :
    Public KunciAES1 As Byte() = Encoding.UTF8.GetBytes("32202256325447291136254525875201") 'Jangan dirubah...!!!!!!
    Public KunciAES2 As Byte() = Encoding.UTF8.GetBytes("32951357369542542129875256357402") 'Jangan dirubah...!!!!!!
    Public KunciAES3 As Byte() = Encoding.UTF8.GetBytes("32654987321951357563214852147803") 'Jangan dirubah...!!!!!!
    Public IVAES1 As Byte() = Encoding.UTF8.GetBytes("1652256325745601")                    'Jangan dirubah...!!!!!!
    Public IVAES2 As Byte() = Encoding.UTF8.GetBytes("1652362536947802")                    'Jangan dirubah...!!!!!!
    Public IVAES3 As Byte() = Encoding.UTF8.GetBytes("1683215468185203")                    'Jangan dirubah...!!!!!!
    Public Function EnkripsiTeksAES1(TeksAsli As String) As String
        Return EncryptString(TeksAsli, KunciAES1, IVAES1)
    End Function
    Public Function DekripsiTeksAES1(TeksEnkripsi As String) As String
        Return DecryptString(TeksEnkripsi, KunciAES1, IVAES1)
    End Function
    Public Function EnkripsiTeksAES2(TeksAsli As String) As String
        Return EncryptString(TeksAsli, KunciAES2, IVAES2)
    End Function
    Public Function DekripsiTeksAES2(TeksEnkripsi As String) As String
        Return DecryptString(TeksEnkripsi, KunciAES2, IVAES2)
    End Function
    Public Function EnkripsiTeksAES3(TeksAsli As String) As String
        Return EncryptString(TeksAsli, KunciAES3, IVAES3)
    End Function
    Public Function DekripsiTeksAES3(TeksEnkripsi As String) As String
        Return DecryptString(TeksEnkripsi, KunciAES3, IVAES3)
    End Function
    Public Function EnkripsiAngkaAES1(AngkaAsli As String) As String
        Return EncryptString(AngkaAsli, KunciAES1, IVAES1)
    End Function
    Public Function DekripsiAngkaAES1(AngkaEnkripsi As String) As String
        Return DecryptAngka(AngkaEnkripsi, KunciAES1, IVAES1)
    End Function
    Public Function EnkripsiAngkaAES2(AngkaAsli As String) As String
        Return EncryptString(AngkaAsli, KunciAES2, IVAES2)
    End Function
    Public Function DekripsiAngkaAES2(AngkaEnkripsi As String) As String
        Return DecryptAngka(AngkaEnkripsi, KunciAES2, IVAES2)
    End Function
    Public Function EnkripsiAngkaAES3(AngkaAsli As String) As String
        Return EncryptString(AngkaAsli, KunciAES3, IVAES3)
    End Function
    Public Function DekripsiAngkaAES3(AngkaEnkripsi As String) As String
        Return DecryptAngka(AngkaEnkripsi, KunciAES3, IVAES3)
    End Function
    Public Function EncryptString(ByVal plainText As String, ByVal key As Byte(), ByVal iv As Byte()) As String
        Dim encrypted As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = key
            aesAlg.IV = iv
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
            Using msEncrypt As New MemoryStream()
                Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                    Using swEncrypt As New StreamWriter(csEncrypt)
                        swEncrypt.Write(plainText)
                    End Using
                    encrypted = msEncrypt.ToArray()
                End Using
            End Using
        End Using
        Return Convert.ToBase64String(encrypted)
    End Function
    Public Function DecryptString(ByVal cipherText As String, ByVal key As Byte(), ByVal iv As Byte()) As String
        Dim plaintext As String = Nothing
        Dim cipherBytes As Byte()
        Try
            cipherBytes = Convert.FromBase64String(cipherText)
        Catch ex As Exception
            Return teks_DekripsiTeksGagal
        End Try
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = key
            aesAlg.IV = iv
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
            Using msDecrypt As New MemoryStream(cipherBytes)
                Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                    Using srDecrypt As New StreamReader(csDecrypt)
                        plaintext = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
        Return plaintext
    End Function
    Public Function DecryptAngka(ByVal cipherText As String, ByVal key As Byte(), ByVal iv As Byte()) As String
        Dim plaintext As String = Nothing
        Dim cipherBytes As Byte()
        Try
            cipherBytes = Convert.FromBase64String(cipherText)
        Catch ex As Exception
            Return teks_DekripsiAngkaGagal
        End Try
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = key
            aesAlg.IV = iv
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
            Using msDecrypt As New MemoryStream(cipherBytes)
                Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                    Using srDecrypt As New StreamReader(csDecrypt)
                        plaintext = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
        Return plaintext
    End Function
    Public teks_DekripsiTeksGagal = "Dekripsi Teks Gagal"
    Public teks_DekripsiAngkaGagal As Integer = 999999999

    'ENKRIPSI TEKS :
    Public Function EnkripsiTeks(ByVal value As String) As String
        Dim PanjangTeks = value.Length
        value = New String(value.Reverse().ToArray())
        Dim TeksTerenkripsi As String = AcakKarakter(123)
        If PanjangTeks > 0 Then
            Dim Baris = 0
            Do While Baris < PanjangTeks
                Baris = Baris + 1
                TeksTerenkripsi = TeksTerenkripsi & value(Baris - 1).ToString() & AcakKarakter(9)
            Loop
        Else
            TeksTerenkripsi = "&dfd87&*^&565%434!~24325)_898Hjkdfb/dyuh-f&*^(*&DRFH*&^&%|}{Pmkdfnjk2022dfUH(*^ERDjkdf/khHJGHJd89^&FDEHJFFJjkdfhd(*^^5464$335(&*8fhhdfkjd-fh2023edf5536/45%^%^%632025/4820430-348(*&*&*34937d2024klfjd34jgey^&%6defhdjk"
        End If
        Return TeksTerenkripsi
    End Function

    'DEKRIPSI TEKS
    Public Function DekripsiTeks(ByVal value As String) As String
        Dim TeksTerDekripsi As String = Nothing
        If value <> "&dfd87&*^&565%434!~24325)_898Hjkdfb/dyuh-f&*^(*&DRFH*&^&%|}{Pmkdfnjk2022dfUH(*^ERDjkdf/khHJGHJd89^&FDEHJFFJjkdfhd(*^^5464$335(&*8fhhdfkjd-fh2023edf5536/45%^%^%632025/4820430-348(*&*&*34937d2024klfjd34jgey^&%6defhdjk" Then
            value = New String(value.Reverse().ToArray())
            Dim PanjangTeks = (value.Length - 123) / 10
            Dim Baris = 0
            Dim LacakKarakter = 123
            Do While Baris < PanjangTeks
                Baris += 1
                LacakKarakter = (Baris * 10)
                TeksTerDekripsi = TeksTerDekripsi & value(LacakKarakter - 1).ToString()
            Loop
        End If
        Return TeksTerDekripsi
    End Function

    'ENKRIPSI ANGKA
    Public Function EnkripsiAngka(ByVal value As String) As String
        Dim AngkaAsli_String As String = value.ToString
        Dim JumlahDigit = AngkaAsli_String.Length
        Dim AngkaTerenkripsi_String As String = AcakKarakter(180)
        Dim SusurKarakter
        Dim Hitung = 0
        Do While Hitung <= JumlahDigit
            Hitung = Hitung + 1
            SusurKarakter = AngkaAsli_String(Hitung - 1).ToString()
            AngkaTerenkripsi_String = AngkaTerenkripsi_String & KonversiAngkaKeTigaHuruf(SusurKarakter) & AcakKarakter(27)
        Loop
        AngkaTerenkripsi_String = AngkaTerenkripsi_String & AcakKarakter(99)
        Return AngkaTerenkripsi_String
    End Function

    'DEKRIPSI ANGKA
    Public ValidasiDekripsiAngka As Boolean
    Public Function DekripsiAngka(ByVal value As String) As String
        ValidasiDekripsiAngka = True
        Dim AngkaTerDekripsi As Int64 = 0
        Dim AngkaTerDekripsi_String As String = Nothing
        Dim SusurKarakter As String
        Dim UjungHitung = AmbilAngka(value.Length) - 99 - 27
        Dim Hitung = 180
        Do While Hitung < UjungHitung
            SusurKarakter = value.Substring(Hitung, 3)
            Select Case SusurKarakter
                Case "aWb"
                    SusurKarakter = "1"
                Case "mTf"
                    SusurKarakter = "2"
                Case "XDt"
                    SusurKarakter = "3"
                Case "clR"
                    SusurKarakter = "4"
                Case "tXe"
                    SusurKarakter = "5"
                Case "VoN"
                    SusurKarakter = "6"
                Case "Ian"
                    SusurKarakter = "7"
                Case "LjQ"
                    SusurKarakter = "8"
                Case "sdO"
                    SusurKarakter = "9"
                Case "XcR"
                    SusurKarakter = "0"
                Case Else
                    ValidasiDekripsiAngka = False
            End Select
            AngkaTerDekripsi_String = AngkaTerDekripsi_String + SusurKarakter
            Hitung = Hitung + 30
        Loop
        AngkaTerDekripsi = AmbilAngka(AngkaTerDekripsi_String)
        If ValidasiDekripsiAngka = False Then AngkaTerDekripsi = 0
        Return AngkaTerDekripsi
    End Function

    'ENKRIPSI TANGGAL
    Public Function EnkripsiTanggal(ByVal value As String) As String
        Dim TanggalAsli As String = value.Substring(0, 10)
        Dim TanggalTerenkripsi As String = AcakKarakter(2)
        Dim SusurKarakter
        Dim Hitung = 0
        Do While Hitung <= 10
            Hitung = Hitung + 1
            SusurKarakter = TanggalAsli(Hitung - 1).ToString()
            TanggalTerenkripsi = TanggalTerenkripsi & AcakKarakter(27) & KonversiAngkaKeTigaHuruf(SusurKarakter)
        Loop
        TanggalTerenkripsi = TanggalTerenkripsi & AcakKarakter(63)
        Return TanggalTerenkripsi
    End Function

    'DEKRIPSI TANGGAL
    Public ValidasiDekripsiTanggal
    Public Function DekripsiTanggal(ByVal value As String) As String
        ValidasiDekripsiTanggal = True
        Dim TanggalTerDekripsi As String = Nothing
        Dim SusurKarakter As String
        Dim Hitung = 0
        Do While Hitung <= 270
            Hitung = Hitung + 30
            SusurKarakter = value.Substring(Hitung - 1, 3)
            Select Case SusurKarakter
                Case "aWb"
                    SusurKarakter = "1"
                Case "mTf"
                    SusurKarakter = "2"
                Case "XDt"
                    SusurKarakter = "3"
                Case "clR"
                    SusurKarakter = "4"
                Case "tXe"
                    SusurKarakter = "5"
                Case "VoN"
                    SusurKarakter = "6"
                Case "Ian"
                    SusurKarakter = "7"
                Case "LjQ"
                    SusurKarakter = "8"
                Case "sdO"
                    SusurKarakter = "9"
                Case "XcR"
                    SusurKarakter = "0"
                Case "fyi"
                    SusurKarakter = "-"
                Case Else
                    ValidasiDekripsiTanggal = False
            End Select
            TanggalTerDekripsi = TanggalTerDekripsi + SusurKarakter
        Loop
        Return TanggalTerDekripsi
    End Function

    'Konversi Angka ke 3 huruf (Contoh : 1 = "aWb") || Ini masih ada kaitannya dengan Enkripsi Tanggal
    Public Function KonversiAngkaKeTigaHuruf(ByVal value As String) As String
        Dim Huruf As String = ""
        If value = "1" Then Huruf = "aWb"
        If value = "2" Then Huruf = "mTf"
        If value = "3" Then Huruf = "XDt"
        If value = "4" Then Huruf = "clR"
        If value = "5" Then Huruf = "tXe"
        If value = "6" Then Huruf = "VoN"
        If value = "7" Then Huruf = "Ian"
        If value = "8" Then Huruf = "LjQ"
        If value = "9" Then Huruf = "sdO"
        If value = "0" Then Huruf = "XcR"
        If value = "-" Or value = "/" Then Huruf = "fyi"
        Return Huruf
    End Function

    'ACAK KARAKTER
    Public Function AcakKarakter(ByVal value As String) As String
        Dim JumlahKarakter = AmbilAngka(value)
        Dim MateriAcak = "1234567890-=][poiuytrewqasdfghjkl;/.,mnbvcxz~!@#$%^&*()_+|}{POIUYTREWQASDFGHJKL:?><MNBVCXZ"
        Dim KarakterTeracak As String = ""
        Dim Hitung = 0
        Jeda(3)
        Dim Acak As New Random
        Dim strPOS = ""
        While Hitung < JumlahKarakter
            strPOS = Acak.Next(0, MateriAcak.Length)
            KarakterTeracak = KarakterTeracak + MateriAcak(strPOS)
            Hitung = Hitung + 1
        End While
        Return KarakterTeracak
    End Function


    'ACAK ANGKA
    Public Function AcakAngka(ByVal value As String) As String
        Dim JumlahAngka = AmbilAngka(value)
        Dim MateriAcak = "1234567890"
        Dim AngkaTeracak As String = ""
        Dim Hitung = 0
        Jeda(3)
        Dim Acak As New Random
        Dim strPOS = ""
        While Hitung < JumlahAngka
            strPOS = Acak.Next(0, MateriAcak.Length)
            AngkaTeracak = AngkaTeracak + MateriAcak(strPOS)
            Hitung = Hitung + 1
        End While
        Return AngkaTeracak
    End Function

    Public pm_PLUS As String = "PLUS"
    Public pm_MINUS As String = "MINUS"


    'MENGAMBIL ANGKA DARI TEKS YANG MENGANDUNG HURUF DAN ANGKA
    Public Function AmbilAngka(ByVal value) As Int64    'Type data dari variabel "value" biarkan bebas, jangan pakai As String, atau apa pun..!!!
        If IsDBNull(value) Then Return 0
        If value Is Nothing Then Return 0
        Dim valueString As String = value.ToString
        Dim PlusMinus = pm_PLUS
        If valueString(0) = "-"c Then PlusMinus = pm_MINUS
        Dim HasilString As String = Kosongan
        If valueString = "" Or valueString = Kosongan Or valueString = Nothing Then valueString = "xyZ0"
        Dim coll As MatchCollection = Regex.Matches(valueString, "\d+")
        For Each a As Match In coll
            HasilString += a.ToString()
        Next
        If HasilString = "" Or HasilString = Kosongan Or HasilString = Nothing Then HasilString = "0"
        Dim HasilAngka As Int64 = Convert.ToInt64(HasilString)
        If PlusMinus = pm_MINUS Then HasilAngka = -HasilAngka
        Return HasilAngka
    End Function

    'MENGAMBIL ANGKA DARI TEKS YANG MENGANDUNG HURUF DAN ANGKA DENGAN MENGABAIKAN SIMBOL MINUS (-)
    Public Function AmbilAngka_TanpaMinus(ByVal value As String) As Int64
        Dim HasilAngka As String = String.Empty
        If value = "" Then value = "xyZ0"
        Dim coll As MatchCollection = Regex.Matches(value, "\d+")
        For Each a As Match In coll
            HasilAngka += a.ToString()
        Next
        If HasilAngka = "" Then HasilAngka = "0"
        HasilAngka = Convert.ToInt64(HasilAngka)
        Return HasilAngka
    End Function

    Public ProsesAmbilAngkaDesimal As Boolean
    'MENGAMBIL ANGKA DARI DARI TEXTBOX DESIMAL :
    Public Function AmbilAngka_Desimal(ByVal value) As Decimal
        If IsDBNull(value) Then Return 0
        If value Is Nothing Then Return 0
        Dim valueString As String = value.ToString
        If valueString = Kosongan Then Return 0
        If valueString = "-" Then Return 0
        Dim HasilString As String
        HasilString = valueString.Replace(".", "")
        HasilString = valueString.Replace(" ", "")
        If HasilString = "" Or HasilString = Kosongan Or HasilString = Nothing Or HasilString = "-" Then HasilString = "0"
        'HasilString = AmbilAngka(HasilString) 'Ini Bahaya..! Tapi disimpan dulu, untuk cadangan..!
        Dim HasilAngka As Decimal
        Try
            HasilAngka = Convert.ToDecimal(HasilString)
            ProsesAmbilAngkaDesimal = True
        Catch ex As Exception
            HasilAngka = 0
            ProsesAmbilAngkaDesimal = False
        End Try
        Return HasilAngka
    End Function


    'MENGAMBIL ANGKA DARI DARI TEXTBOX MATA UANG ASING :
    Public Function AmbilAngka_Asing(ByVal value) As Decimal
        If IsDBNull(value) Then Return 0
        If value Is Nothing Then Return 0
        Dim valueString As String = value.ToString
        If valueString = Kosongan Then Return 0
        If valueString = "-" Then Return 0
        Dim HasilString As String
        HasilString = valueString.Replace(".", "")
        HasilString = valueString.Replace(" ", "")
        If HasilString = "" Or HasilString = Kosongan Or HasilString = Nothing Or HasilString = "-" Then HasilString = "0"
        'HasilString = AmbilAngka(HasilString) 'Ini Bahaya..! Tapi disimpan dulu, untuk cadangan..!
        Dim HasilAngka As Decimal
        Try
            HasilAngka = Convert.ToDecimal(HasilString)
            ProsesAmbilAngkaDesimal = True
        Catch ex As Exception
            HasilAngka = 0
            ProsesAmbilAngkaDesimal = False
        End Try
        Return HasilAngka
    End Function


    ' =================================================================
    ' HELPER FUNCTIONS: PENGGANTI MICROSOFT.VISUALBASIC STRING FUNCTIONS
    ' =================================================================

    ''' <summary>
    ''' Mengambil n karakter dari kiri string. Pengganti Microsoft.VisualBasic.Left()
    ''' </summary>
    Public Function AmbilKiri(value As Object, length As Integer) As String
        If value Is Nothing Then Return String.Empty
        Dim s = value.ToString()
        If s.Length <= length Then Return s
        Return s.Substring(0, length)
    End Function

    ''' <summary>
    ''' Mengambil substring dari posisi tertentu. Pengganti Microsoft.VisualBasic.Mid()
    ''' PERHATIAN: Start index dimulai dari 1 (seperti VB6/VBA), bukan 0
    ''' </summary>
    Public Function AmbilTengah(value As Object, start As Integer, Optional length As Integer = -1) As String
        If value Is Nothing Then Return String.Empty
        Dim s = value.ToString()
        Dim startIndex = start - 1 ' Konversi dari 1-based ke 0-based
        If startIndex < 0 OrElse startIndex >= s.Length Then Return String.Empty
        If length < 0 Then Return s.Substring(startIndex)
        Dim maxLength = Math.Min(length, s.Length - startIndex)
        Return s.Substring(startIndex, maxLength)
    End Function

    ''' <summary>
    ''' Membalik urutan karakter dalam string. Pengganti Microsoft.VisualBasic.StrReverse()
    ''' </summary>
    Public Function BalikTeks(value As Object) As String
        If value Is Nothing Then Return String.Empty
        Dim s = value.ToString()
        Return New String(s.Reverse().ToArray())
    End Function


End Module
