' =================================================================
' wpfMdl_StyleBridge.vb
' =================================================================
' Bridge module untuk backward compatibility.
' Menyediakan alias ke variabel dari BookuID.Styles sehingga kode lama
' tetap berfungsi tanpa perlu mengubah setiap file.
'
' CATATAN: File ini hanya untuk backward compatibility.
' Untuk kode baru, gunakan langsung:
'   Imports BookuID.Styles
' =================================================================

Imports System.Windows
Imports System.Windows.Media

Public Module wpfMdl_StyleBridge

    ' =================================================================
    ' WINDOW STYLING FUNCTIONS - Alias ke BookuID.Styles.mdlPub_Styling
    ' =================================================================

    Public Sub StyleWindowDialogWPF_Dasar(Jendela As Window)
        BookuID.Styles.mdlPub_Styling.StyleWindowDialogWPF_Dasar(Jendela)
    End Sub

    Public Sub StyleWindowDialogWPF_TanpaTombolX(Jendela As Window)
        BookuID.Styles.mdlPub_Styling.StyleWindowDialogWPF_TanpaTombolX(Jendela)
    End Sub

    Public Sub StyleWindowDialogWPF_Sizable(Jendela As Window)
        BookuID.Styles.mdlPub_Styling.StyleWindowDialogWPF_Sizable(Jendela)
    End Sub

    ' =================================================================
    ' STYLING ELEMEN - Alias ke BookuID.Styles.StylingElemen
    ' =================================================================
    Public ReadOnly Property style_TextBoxFormDialogAngkaPlus As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaPlus
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaSeparate As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaSeparate
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaSeparatePlus As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaSeparatePlus
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaSeparatePlusReadOnly As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaSeparatePlusReadOnly
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaAsingPlus As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaAsingPlus
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaAsing As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaAsing
        End Get
    End Property

    Public ReadOnly Property style_TextBoxFormDialogAngkaAsingPlusReadOnly As String
        Get
            Return BookuID.Styles.StylingElemen.style_TextBoxFormDialogAngkaAsingPlusReadOnly
        End Get
    End Property

    ' =================================================================
    ' COLOR BRUSHES - Alias ke BookuID.Styles.wpfMdl_StyleColor
    ' =================================================================
    ' Hanya yang digunakan di Booku (berdasarkan error build)

    Public ReadOnly Property clrPrimary As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrPrimary
        End Get
    End Property

    Public ReadOnly Property clrError As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrError
        End Get
    End Property

    Public ReadOnly Property clrWarning As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrWarning
        End Get
    End Property

    Public ReadOnly Property clrSuccess As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrSuccess
        End Get
    End Property

    Public ReadOnly Property clrInfo As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrInfo
        End Get
    End Property

    Public ReadOnly Property clrTeksPrimer As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrTeksPrimer
        End Get
    End Property

    Public ReadOnly Property clrTeksSekunder As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrTeksSekunder
        End Get
    End Property

    Public ReadOnly Property clrTeksDisabled As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrTeksDisabled
        End Get
    End Property

    Public ReadOnly Property clrBgWindow As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrBgWindow
        End Get
    End Property

    Public ReadOnly Property clrBgPanel As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrBgPanel
        End Get
    End Property

    Public ReadOnly Property clrStatusSuccessBg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrStatusSuksesBg
        End Get
    End Property

    Public ReadOnly Property clrStatusWarningBg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrStatusPeringatanBg
        End Get
    End Property

    Public ReadOnly Property clrStatusErrorBg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrStatusErrorBg
        End Get
    End Property

    Public ReadOnly Property clrStatusInfoBg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrStatusInfoBg
        End Get
    End Property

    ' BASE COLORS
    Public ReadOnly Property clrWhite As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrWhite
        End Get
    End Property

    Public ReadOnly Property clrBlack As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrBlack
        End Get
    End Property

    ' NEUTRAL COLORS
    Public ReadOnly Property clrNeutral50 As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrNeutral50
        End Get
    End Property

    Public ReadOnly Property clrNeutral100 As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrNeutral100
        End Get
    End Property

    Public ReadOnly Property clrNeutral200 As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrNeutral200
        End Get
    End Property

    Public ReadOnly Property clrNeutral500 As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrNeutral500
        End Get
    End Property

    ' DATAGRID COLORS
    Public ReadOnly Property clrDataGridBg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrDataGridBg
        End Get
    End Property

    Public ReadOnly Property clrDataGridRowSelect As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrDataGridRowSelect
        End Get
    End Property

    Public ReadOnly Property clrDataGridRowSelectFg As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrDataGridRowSelectFg
        End Get
    End Property

    ' SPECIAL/LEGACY COLORS
    Public ReadOnly Property clrDataTahunLalu As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrDataTahunLalu
        End Get
    End Property

    Public ReadOnly Property clrTeksDataTahunLalu As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrTeksDataTahunLalu
        End Get
    End Property

    Public ReadOnly Property clrTeksDataBelumLunas As SolidColorBrush
        Get
            Return BookuID.Styles.wpfMdl_StyleColor.clrTeksDataBelumLunas
        End Get
    End Property

End Module

