' =================================================================
' mdlPub_WpfInputHelper.vb
' =================================================================
' Modul ini berisi fungsi helper untuk input validation di WPF.
' Digunakan oleh TextBoxBehavior dan RichTextBoxBehavior di Booku Styles.
'
' Fungsi:
' - KarakterDilarangMasuk: Karakter yang dilarang dalam input
' - HanyaBolehInputAngka_WPF: Validasi input angka (positif/negatif)
' - HanyaBolehInputAngkaPlus_WPF: Validasi input angka positif saja
' =================================================================

Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Module mdlPub_WpfInputHelper

    ' =================================================================
    ' KARAKTER DILARANG
    ' =================================================================

    ''' <summary>
    ''' Karakter yang dilarang dalam input teks.
    ''' Mencegah karakter yang bisa menyebabkan masalah SQL injection atau parsing.
    ''' </summary>
    Public KarakterDilarangMasuk As String = "'`;"""

    ' =================================================================
    ' VALIDASI INPUT ANGKA
    ' =================================================================

    ''' <summary>
    ''' Validasi input hanya boleh angka (bisa positif atau negatif).
    ''' Minus (-) hanya diperbolehkan di awal teks.
    ''' </summary>
    Public Sub HanyaBolehInputAngka_WPF(teks As TextBox, e As TextCompositionEventArgs)
        ' Izinkan input angka atau tanda minus (-) hanya di awal teks
        If Not (Char.IsDigit(e.Text, 0) OrElse (e.Text = "-" AndAlso teks.SelectionStart = 0 AndAlso Not teks.Text.Contains("-"))) Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Validasi input hanya boleh angka positif.
    ''' Tidak mengizinkan tanda minus (-).
    ''' </summary>
    Public Sub HanyaBolehInputAngkaPlus_WPF(teks As TextBox, e As TextCompositionEventArgs)
        If Not Char.IsDigit(e.Text, e.Text.Length - 1) Then
            e.Handled = True
        End If
    End Sub

End Module
