Imports System.Globalization
Imports System.Windows.Data
Imports bcomm

Public Class cls_EnableButtonConverter

    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type,
                            parameter As Object, culture As CultureInfo) _
                            As Object Implements IValueConverter.Convert

        'Jika cell berisi "Posted" maka tombol NONAKTIF
        If value Is Nothing Then Return True
        If value.ToString() = Kosongan Then Return False
        Return True
    End Function

    Public Function ConvertBack(value As Object, targetType As Type,
                                parameter As Object, culture As CultureInfo) _
                                As Object Implements IValueConverter.ConvertBack
        Return Binding.DoNothing
    End Function

End Class