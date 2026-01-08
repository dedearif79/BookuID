Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input


Public Class RichTextBoxBehavior

    'KARAKTER DILARANG :
    Public Shared ReadOnly Property Dilarang As String = KarakterDilarangMasuk
    ' Attached Property untuk mengaktifkan behavior
    Public Shared ReadOnly KarakterDilarangProperty As DependencyProperty =
        DependencyProperty.RegisterAttached("KarakterDilarang",
                                            GetType(Boolean),
                                            GetType(RichTextBoxBehavior),
                                            New PropertyMetadata(False, AddressOf OnKarakterDilarangChanged))

    Public Shared Sub SetKarakterDilarang(element As DependencyObject, value As Boolean)
        element.SetValue(KarakterDilarangProperty, value)
    End Sub

    Public Shared Function GetKarakterDilarang(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(KarakterDilarangProperty))
    End Function

    ' Event saat Attached Property diubah
    Private Shared Sub OnKarakterDilarangChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim richTextBox As RichTextBox = TryCast(d, RichTextBox)
        If richTextBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler richTextBox.PreviewTextInput, AddressOf Richtxt_KarakterDilarangPreviewTextInput
            AddHandler richTextBox.PreviewKeyDown, AddressOf Richtxt_KarakterDilarangPreviewKeyDown
            AddHandler richTextBox.TextChanged, AddressOf Richtxt_KarakterDilarangTextChanged
        ElseIf richTextBox IsNot Nothing Then
            RemoveHandler richTextBox.PreviewTextInput, AddressOf Richtxt_KarakterDilarangPreviewTextInput
            RemoveHandler richTextBox.PreviewKeyDown, AddressOf Richtxt_KarakterDilarangPreviewKeyDown
            RemoveHandler richTextBox.TextChanged, AddressOf Richtxt_KarakterDilarangTextChanged
        End If
    End Sub

    Private Shared Sub Richtxt_KarakterDilarangTextChanged(sender As Object, e As TextChangedEventArgs)
        Dim richTextBox As RichTextBox = TryCast(sender, RichTextBox)
        If richTextBox IsNot Nothing Then
            ' Ambil teks dari RichTextBox
            Dim teksBaru As String = New TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text

            ' Filter teks dengan menghapus karakter yang dilarang
            Dim teksBersih As String = New String(teksBaru.Where(Function(c) Not Dilarang.Contains(c)).ToArray())

            ' Perbarui teks jika ada karakter yang harus dihapus
            If teksBaru <> teksBersih Then
                Dim caretPosition = richTextBox.CaretPosition
                richTextBox.Document.Blocks.Clear()
                richTextBox.Document.Blocks.Add(New Paragraph(New Run(teksBersih)))
                richTextBox.CaretPosition = richTextBox.Document.ContentEnd ' Pindahkan kursor ke akhir teks
            End If
        End If
    End Sub

    Private Shared Sub Richtxt_KarakterDilarangPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Dilarang.Contains(e.Text) Then
            e.Handled = True
        End If
    End Sub

    Private Shared Sub Richtxt_KarakterDilarangPreviewKeyDown(sender As Object, e As KeyEventArgs)
        If Dilarang.Contains(e.Key.ToString()) Then
            e.Handled = True
        End If
    End Sub

End Class