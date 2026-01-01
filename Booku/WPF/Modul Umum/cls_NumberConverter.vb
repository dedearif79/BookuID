Imports bcomm
Imports System.Globalization
Imports System.Windows.Data

Public Class cls_NumberConverter

    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing OrElse IsDBNull(value) Then
            Return Kosongan '(Alternatif pengganti kehampaan / dbNull )
        End If

        Dim number As Decimal
        If Decimal.TryParse(value.ToString(), number) Then
            If number = 0 Then
                Return StripKosong '(Alternatif pengganti Angka Nol )
            End If

            ' Format angka tanpa desimal jika bulat
            If number = Math.Truncate(number) Then
                Return number.ToString("#,##0", New CultureInfo("id-ID")) ' Tanpa desimal
            Else
                Return number.ToString("#,##0.00", New CultureInfo("id-ID")) ' Dengan desimal
            End If
        End If

        Return value
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        ' Implementasi untuk binding dua arah (jika diperlukan)
        Return Binding.DoNothing
    End Function

End Class