' =================================================================
' StylingElemen.vb
' =================================================================
' Modul berisi variabel string untuk referensi style dari code-behind.
' Style-style ini didefinisikan di Booku Styles Class Library.
'
' Penggunaan:
'   Dim namaStyle As String = style_TextBoxFormDialogAngkaPlus
'   txtBox.Style = CType(Application.Current.Resources(namaStyle), Style)
' =================================================================

Imports System.Windows

' Namespace sudah ditentukan oleh RootNamespace di project file (BookuID.Styles)
' Jadi tidak perlu deklarasi Namespace di sini

Public Module StylingElemen

    ' TextBox Styles - String references to styles defined in StyleTextBox.xaml
    Public style_TextBoxFormDialogAngkaPlus As String = "styleTextBoxFormDialogAngkaPlus"
    Public style_TextBoxFormDialogAngkaSeparate As String = "styleTextBoxFormDialogAngkaSeparate"
    Public style_TextBoxFormDialogAngkaSeparatePlus As String = "styleTextBoxFormDialogAngkaSeparatePlus"
    Public style_TextBoxFormDialogAngkaSeparatePlusReadOnly As String = "styleTextBoxFormDialogAngkaSeparatePlusReadOnly"
    Public style_TextBoxFormDialogAngkaAsingPlus As String = "styleTextBoxFormDialogAngkaAsingPlus"
    Public style_TextBoxFormDialogAngkaAsing As String = "styleTextBoxFormDialogAngkaAsing"
    Public style_TextBoxFormDialogAngkaAsingPlusReadOnly As String = "styleTextBoxFormDialogAngkaAsingPlusReadOnly"

End Module
