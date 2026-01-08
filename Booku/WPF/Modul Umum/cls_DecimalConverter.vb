Imports System.Globalization
Imports System.Windows.Data

Public Class cls_DecimalConverter

    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing OrElse IsDBNull(value) Then
            Return String.Empty ' Alternatif pengganti kehampaan / dbNull
        End If

        Dim number As Decimal
        If Decimal.TryParse(value.ToString(), number) Then
            ' Format angka dengan pemisah ribuan dan 2 digit di belakang koma
            Return number.ToString("N2", New CultureInfo("id-ID")) ' "1.234,00"
        End If

        Return value ' Jika bukan angka, kembalikan nilai asli
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        ' Implementasi untuk binding dua arah jika diperlukan
        Return Binding.DoNothing
    End Function

End Class
