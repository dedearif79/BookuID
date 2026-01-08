Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports bcomm

Public Class TextBoxBehavior

    'KARAKTER DILARANG :
    Public Shared ReadOnly Property Dilarang As String = KarakterDilarangMasuk
    Public Shared ReadOnly KarakterDilarangProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("KarakterDilarang",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnKarakterDilarangChanged))
    Public Shared Sub SetKarakterDilarang(element As DependencyObject, value As Boolean)
        element.SetValue(KarakterDilarangProperty, value)
    End Sub
    Public Shared Function GetKarakterDilarang(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(KarakterDilarangProperty))
    End Function
    Private Shared Sub OnKarakterDilarangChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_KarakterDilarangPreviewTextInput
            AddHandler textBox.PreviewKeyDown, AddressOf txt_KarakterDilarangPreviewKeyDown
            AddHandler textBox.TextChanged, AddressOf txt_KarakterDilarangTextChanged
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_KarakterDilarangPreviewTextInput
            RemoveHandler textBox.PreviewKeyDown, AddressOf txt_KarakterDilarangPreviewKeyDown
            RemoveHandler textBox.TextChanged, AddressOf txt_KarakterDilarangTextChanged
        End If
    End Sub
    Private Shared Sub txt_KarakterDilarangTextChanged(sender As Object, e As TextChangedEventArgs)
        Dim textBox As TextBox = TryCast(sender, TextBox)
        If textBox IsNot Nothing Then
            ' Contoh: Hapus karakter yang dilarang jika sudah terlanjur masuk
            Dim teksBaru As String = New String(textBox.Text.Where(Function(c) Not Dilarang.Contains(c)).ToArray())
            If textBox.Text <> teksBaru Then
                textBox.Text = teksBaru
                textBox.CaretIndex = textBox.Text.Length ' Pindahkan kursor ke akhir teks
            End If
        End If
    End Sub
    Private Shared Sub txt_KarakterDilarangPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Dilarang.Contains(e.Text) Then
            e.Handled = True
        End If
    End Sub
    Private Shared Sub txt_KarakterDilarangPreviewKeyDown(sender As Object, e As KeyEventArgs)
        If Dilarang.Contains(e.Key.ToString()) Then
            e.Handled = True
        End If
    End Sub





    'HANYA BOLEH INPUT ANGKA (Bisa Plus dan Minus/Negatif) :
    Public Shared ReadOnly HanyaBolehInputAngkaProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngka",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaChanged))
    Public Shared Sub SetHanyaBolehInputAngka(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaProperty, value)
    End Sub
    Public Shared Function GetHanyaBolehInputAngka(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaProperty))
    End Function
    Private Shared Sub OnHanyaBolehInputAngkaChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaPreviewTextInput
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaPasting)
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaPreviewTextInput
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaPasting)
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        HanyaBolehInputAngka_WPF(CType(sender, TextBox), e)
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaPasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim isNegative As Boolean = text.StartsWith("-")
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c)).ToArray())
            If isNegative AndAlso filteredText.Length > 0 Then filteredText = "-" & filteredText

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub





    'HANYA BOLEH INPUT ANGKA PLUS :
    Public Shared ReadOnly HanyaBolehInputAngkaPlusProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaPlus",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaPlusChanged))
    Public Shared Sub SetHanyaBolehInputAngkaPlus(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaPlusProperty, value)
    End Sub

    Public Shared Function GetHanyaBolehInputAngkaPlus(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaPlusProperty))
    End Function
    Private Shared Sub OnHanyaBolehInputAngkaPlusChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaPlusPreviewTextInput
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaPlusPasting)
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaPlusPreviewTextInput
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaPlusPasting)
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaPlusPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        HanyaBolehInputAngkaPlus_WPF(CType(sender, TextBox), e)
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaPlusPasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c)).ToArray())

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub



    'HANYA BOLEH INPUT ANGKA PLUS - SEPARATE :
    Public Shared ReadOnly HanyaBolehInputAngkaSeparatePlusProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaSeparatePlus",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaSeparatePlusChanged))
    Public Shared Sub SetHanyaBolehInputAngkaSeparatePlus(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaSeparatePlusProperty, value)
    End Sub

    Public Shared Function GetHanyaBolehInputAngkaSeparatePlus(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaSeparatePlusProperty))
    End Function
    Private Shared Sub OnHanyaBolehInputAngkaSeparatePlusChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaSeparatePlusPreviewTextInput
            AddHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaSeparatePlusTextChanged
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaSeparatePlusPasting)
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaSeparatePlusPreviewTextInput
            RemoveHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaSeparatePlusTextChanged
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaSeparatePlusPasting)
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparatePlusPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        HanyaBolehInputAngkaPlus_WPF(CType(sender, TextBox), e)
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparatePlusPasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c)).ToArray())

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparatePlusTextChanged(sender As Object, e As TextChangedEventArgs)

        Dim teks As TextBox = CType(sender, TextBox)

        If teks IsNot Nothing Then

            ' Ambil teks dari TextBox dan hilangkan spasi berlebih
            Dim teksBaru As String = teks.Text.Trim()

            If Right(teksBaru, 3) = ",00" Then Replace(teksBaru, ",00", "")
            If Right(teksBaru, 3) = ".00" Then Replace(teksBaru, ".00", "")

            ' Cek apakah teks memiliki minus di awal
            Dim isNegative As Boolean = teksBaru.StartsWith("-")

            ' Hapus semua minus yang tidak ada di awal
            teksBaru = teksBaru.TrimStart("-") ' Hilangkan semua minus di awal dulu
            teksBaru = teksBaru.Replace("-", "") ' Hilangkan minus yang mungkin ada di tengah

            ' Tambahkan kembali satu minus jika awalnya memang negatif
            If isNegative Then teksBaru = "-" & teksBaru

            ' Hapus semua karakter selain angka dan satu minus di awal
            Dim teksBersih As String = New String(teksBaru.Where(Function(c) Char.IsDigit(c) OrElse (c = "-" AndAlso teksBaru.IndexOf("-") = 0)).ToArray())

            ' Jika ada angka, format dengan pemisah ribuan
            If teksBersih.Length > 0 AndAlso teksBersih <> "-" Then
                Dim angka As Double
                If Double.TryParse(teksBersih, angka) Then
                    teksBersih = angka.ToString("N0") ' Format angka dengan pemisah ribuan
                    If isNegative Then teksBersih = "-" & teksBersih ' Pastikan minus tetap ada di awal
                End If
            End If

            If teksBersih = 0.ToString Then teks.Text = Kosongan 'Harus pakai ToString...!

            ' Cek apakah teks berubah
            If teksBaru <> teksBersih Then
                ' Simpan posisi kursor sebelum perubahan
                Dim caretPosition As Integer = teks.SelectionStart

                ' Perbarui teks di TextBox
                teks.Text = teksBersih.Replace("--", "-")

                ' Kembalikan posisi kursor ke akhir teks
                teks.SelectionStart = teks.Text.Length
            End If

        End If

    End Sub



    'HANYA BOLEH INPUT ANGKA PLUS MINUS - SEPARATE :
    Public Shared ReadOnly HanyaBolehInputAngkaSeparateProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaSeparate",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaSeparateChanged))
    Public Shared Sub SetHanyaBolehInputAngkaSeparate(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaSeparateProperty, value)
    End Sub

    Public Shared Function GetHanyaBolehInputAngkaSeparate(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaSeparateProperty))
    End Function
    Private Shared Sub OnHanyaBolehInputAngkaSeparateChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaSeparatePreviewTextInput
            AddHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaSeparateTextChanged
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaSeparatePasting)
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaSeparatePreviewTextInput
            RemoveHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaSeparateTextChanged
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaSeparatePasting)
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparatePreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        HanyaBolehInputAngka_WPF(CType(sender, TextBox), e)
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparatePasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim isNegative As Boolean = text.StartsWith("-")
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c)).ToArray())
            If isNegative AndAlso filteredText.Length > 0 Then filteredText = "-" & filteredText

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaSeparateTextChanged(sender As Object, e As TextChangedEventArgs)

        Dim teks As TextBox = CType(sender, TextBox)

        If teks IsNot Nothing Then

            ' Ambil teks dari TextBox dan hilangkan spasi berlebih
            Dim teksBaru As String = teks.Text.Trim()

            If Right(teksBaru, 3) = ",00" Then Replace(teksBaru, ",00", "")
            If Right(teksBaru, 3) = ".00" Then Replace(teksBaru, ".00", "")

            ' Cek apakah teks memiliki minus di awal
            Dim isNegative As Boolean = teksBaru.StartsWith("-")

            ' Hapus semua minus yang tidak ada di awal
            teksBaru = teksBaru.TrimStart("-") ' Hilangkan semua minus di awal dulu
            teksBaru = teksBaru.Replace("-", "") ' Hilangkan minus yang mungkin ada di tengah

            ' Tambahkan kembali satu minus jika awalnya memang negatif
            If isNegative Then teksBaru = "-" & teksBaru

            ' Hapus semua karakter selain angka dan satu minus di awal
            Dim teksBersih As String = New String(teksBaru.Where(Function(c) Char.IsDigit(c) OrElse (c = "-" AndAlso teksBaru.IndexOf("-") = 0)).ToArray())

            ' Jika ada angka, format dengan pemisah ribuan
            If teksBersih.Length > 0 AndAlso teksBersih <> "-" Then
                Dim angka As Double
                If Double.TryParse(teksBersih, angka) Then
                    teksBersih = angka.ToString("N0") ' Format angka dengan pemisah ribuan
                    If isNegative Then teksBersih = "-" & teksBersih ' Pastikan minus tetap ada di awal
                End If
            End If

            If teksBersih = 0.ToString Then teks.Text = Kosongan 'Harus pakai ToString...!

            ' Cek apakah teks berubah
            If teksBaru <> teksBersih Then
                ' Simpan posisi kursor sebelum perubahan
                Dim caretPosition As Integer = teks.SelectionStart

                ' Perbarui teks di TextBox
                teks.Text = teksBersih.Replace("--", "-")

                ' Kembalikan posisi kursor ke akhir teks
                teks.SelectionStart = teks.Text.Length
            End If

        End If

    End Sub



    'HANYA BOLEH INPUT ANGKA DESIMAL :
    Public Shared ReadOnly HanyaBolehInputAngkaDesimalProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaDesimal",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaDesimalChanged))
    Public Shared Sub SetHanyaBolehInputAngkaDesimal(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaDesimalProperty, value)
    End Sub

    Public Shared Function GetHanyaBolehInputAngkaDesimal(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaDesimalProperty))
    End Function

    Private Shared Sub OnHanyaBolehInputAngkaDesimalChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaDesimalPreviewTextInput
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaDesimalPasting)
            AddHandler textBox.LostFocus, AddressOf txt_HanyaBolehInputAngkaDesimalLostFocus
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaDesimalPreviewTextInput
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaDesimalPasting)
            RemoveHandler textBox.LostFocus, AddressOf txt_HanyaBolehInputAngkaDesimalLostFocus
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        Dim tb As TextBox = CType(sender, TextBox)
        Dim currentText As String = tb.Text.Insert(tb.CaretIndex, e.Text) ' Simulasi input
        ' Regex hanya memperbolehkan angka, titik (pemisah ribuan), dan koma (pemisah desimal)
        Dim regex As New Regex("^[0-9]*(\[0-9]{0,3})?(,[0-9]{0,2})?$")
        If Not regex.IsMatch(currentText) Then
            e.Handled = True ' Mencegah input tidak valid
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim isNegative As Boolean = text.StartsWith("-")
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c) OrElse c = "."c OrElse c = ","c).ToArray())
            If isNegative AndAlso filteredText.Length > 0 Then filteredText = "-" & filteredText

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalLostFocus(sender As Object, e As RoutedEventArgs)
        Dim tb As TextBox = CType(sender, TextBox)
        Dim text As String = tb.Text
        If Not String.IsNullOrWhiteSpace(text) Then
            Try
                ' Ubah titik menjadi kosong (karena pemisah ribuan) dan koma menjadi titik (sebagai pemisah desimal)
                Dim nilai As Double = Double.Parse(text.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
                ' Format ke style Indonesia dengan pemisah ribuan titik dan dua angka desimal
                tb.Text = String.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:N2}", nilai)
            Catch ex As Exception
                ' Jika input tidak valid, biarkan tidak berubah
            End Try
        End If
    End Sub



    'HANYA BOLEH INPUT ANGKA DESIMAL - PLUS :
    Public Shared ReadOnly HanyaBolehInputAngkaDesimalPlusProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaDesimalPlus",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaDesimalPlusChanged))
    Public Shared Sub SetHanyaBolehInputAngkaDesimalPlus(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaDesimalPlusProperty, value)
    End Sub

    Public Shared Function GetHanyaBolehInputAngkaDesimalPlus(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaDesimalPlusProperty))
    End Function

    Private Shared Sub OnHanyaBolehInputAngkaDesimalPlusChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaDesimalPlusPreviewTextInput
            DataObject.AddPastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaDesimalPlusPasting)
            AddHandler textBox.LostFocus, AddressOf txt_HanyaBolehInputAngkaDesimalPlusLostFocus
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.PreviewTextInput, AddressOf txt_HanyaBolehInputAngkaDesimalPlusPreviewTextInput
            DataObject.RemovePastingHandler(textBox, AddressOf txt_HanyaBolehInputAngkaDesimalPlusPasting)
            RemoveHandler textBox.LostFocus, AddressOf txt_HanyaBolehInputAngkaDesimalPlusLostFocus
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPlusPreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        Dim tb As TextBox = CType(sender, TextBox)
        Dim currentText As String = tb.Text.Insert(tb.CaretIndex, e.Text) ' Simulasi input
        ' Regex hanya memperbolehkan angka, titik (pemisah ribuan), dan koma (pemisah desimal)
        Dim regex As New Regex("^[0-9]*(\[0-9]{0,3})?(,[0-9]{0,2})?$")
        If Not regex.IsMatch(currentText) Then
            e.Handled = True ' Mencegah input tidak valid
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPlusPasting(sender As Object, e As DataObjectPastingEventArgs)
        e.CancelCommand()
        If e.DataObject.GetDataPresent(DataFormats.Text) Then
            Dim text As String = DirectCast(e.DataObject.GetData(DataFormats.Text), String)
            Dim filteredText As String = New String(text.Where(Function(c) Char.IsDigit(c) OrElse c = "."c OrElse c = ","c).ToArray())

            If Not String.IsNullOrEmpty(filteredText) Then
                Dim textBox As TextBox = DirectCast(sender, TextBox)
                Dim caretIndex As Integer = textBox.CaretIndex
                textBox.Text = textBox.Text.Insert(caretIndex, filteredText)
                textBox.CaretIndex = caretIndex + filteredText.Length
            End If
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPlusLostFocus(sender As Object, e As RoutedEventArgs)
        Dim tb As TextBox = CType(sender, TextBox)
        Dim text As String = tb.Text
        If Not String.IsNullOrWhiteSpace(text) Then
            Try
                ' Ubah titik menjadi kosong (karena pemisah ribuan) dan koma menjadi titik (sebagai pemisah desimal)
                Dim nilai As Double = Double.Parse(text.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
                ' Format ke style Indonesia dengan pemisah ribuan titik dan dua angka desimal
                tb.Text = String.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:N2}", nilai)
            Catch ex As Exception
                ' Jika input tidak valid, biarkan tidak berubah
            End Try
        End If
    End Sub





    'HANYA BOLEH INPUT ANGKA DESIMAL - PLUS - READ ONLY (HASIL PERHITUNGAN) :
    Public Shared ReadOnly HanyaBolehInputAngkaDesimalPlusReadOnlyProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HanyaBolehInputAngkaDesimalPlusReadOnly",
                                                GetType(Boolean),
                                                GetType(TextBoxBehavior),
                                                New PropertyMetadata(False, AddressOf OnHanyaBolehInputAngkaDesimalPlusReadOnlyChanged))
    Public Shared Sub SetHanyaBolehInputAngkaDesimalPlusReadOnly(element As DependencyObject, value As Boolean)
        element.SetValue(HanyaBolehInputAngkaDesimalPlusReadOnlyProperty, value)
    End Sub
    Public Shared Function GetHanyaBolehInputAngkaDesimalPlusReadOnly(element As DependencyObject) As Boolean
        Return CBool(element.GetValue(HanyaBolehInputAngkaDesimalPlusReadOnlyProperty))
    End Function
    Private Shared Sub OnHanyaBolehInputAngkaDesimalPlusReadOnlyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim textBox As TextBox = TryCast(d, TextBox)
        If textBox IsNot Nothing AndAlso CBool(e.NewValue) Then
            AddHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaDesimalPlusReadOnlyTextChanged
        ElseIf textBox IsNot Nothing Then
            RemoveHandler textBox.TextChanged, AddressOf txt_HanyaBolehInputAngkaDesimalPlusReadOnlyTextChanged
        End If
    End Sub
    Private Shared Sub txt_HanyaBolehInputAngkaDesimalPlusReadOnlyTextChanged(sender As Object, e As TextChangedEventArgs)
        Dim tb As TextBox = CType(sender, TextBox)
        Dim text As String = tb.Text
        If Not String.IsNullOrWhiteSpace(text) Then
            Try
                Dim nilai As Double = Double.Parse(text.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
                tb.Text = String.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:N2}", nilai)
                'If tb.Text = "0,00" Then tb.Text = String.Empty
            Catch ex As Exception
            End Try
        End If
    End Sub





End Class