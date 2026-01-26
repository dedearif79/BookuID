' =================================================================
' ComboBoxBehavior.vb
' =================================================================
' Attached Behaviors untuk ComboBox WPF.
' Menyediakan behavior untuk mencegah perubahan nilai saat scroll.
'
' Behaviors tersedia:
' - AbaikanMouseWheel: Mencegah perubahan nilai saat user scroll
' =================================================================

Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media

' Namespace sudah ditentukan oleh RootNamespace di project file (BookuID.Styles)
' Jadi tidak perlu deklarasi Namespace di sini

Public Class ComboBoxBehavior

    ' ABAIKAN MOUSE WHEEL (Mencegah scroll mengubah nilai ComboBox) :
    Public Shared ReadOnly AbaikanMouseWheelProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("AbaikanMouseWheel",
                                                GetType(Boolean),
                                                GetType(ComboBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnAbaikanMouseWheelChanged))

    Public Shared Sub SetAbaikanMouseWheel(element As DependencyObject, value As Boolean)
        element.SetValue(AbaikanMouseWheelProperty, value)
    End Sub

    Public Shared Function GetAbaikanMouseWheel(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(AbaikanMouseWheelProperty))
    End Function

    Private Shared Sub OnAbaikanMouseWheelChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim comboBox As ComboBox = TryCast(d, ComboBox)
        If comboBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler comboBox.PreviewMouseWheel, AddressOf cmb_PreviewMouseWheel
        ElseIf comboBox IsNot Nothing Then
            RemoveHandler comboBox.PreviewMouseWheel, AddressOf cmb_PreviewMouseWheel
        End If
    End Sub

    Private Shared Sub cmb_PreviewMouseWheel(sender As Object, e As MouseWheelEventArgs)
        ' Jika dropdown tidak terbuka, abaikan scroll dan teruskan ke parent (ScrollViewer)
        Dim comboBox As ComboBox = TryCast(sender, ComboBox)
        If comboBox IsNot Nothing AndAlso Not comboBox.IsDropDownOpen Then
            e.Handled = True

            ' Cari parent UIElement dan teruskan event
            Dim parent As UIElement = TryCast(VisualTreeHelper.GetParent(comboBox), UIElement)
            If parent IsNot Nothing Then
                Dim newEventArgs As New MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) With {
                    .RoutedEvent = UIElement.MouseWheelEvent,
                    .Source = comboBox
                }
                parent.RaiseEvent(newEventArgs)
            End If
        End If
    End Sub

End Class
