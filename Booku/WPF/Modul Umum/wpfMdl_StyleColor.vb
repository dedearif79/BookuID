' =================================================================
' wpfMdl_StyleColor.vb
' =================================================================
' Modul ini menyediakan akses ke warna dari StyleColor.xaml
' untuk digunakan di code-behind.
'
' Semua variabel warna merujuk ke resource yang didefinisikan
' di /Booku/WPF/Styles/StyleColor.xaml
'
' Penggunaan:
'   e.Row.Foreground = clrTeksPrimer
'   label.Background = clrStatusError
' =================================================================

Imports System.Windows
Imports System.Windows.Media

Public Module wpfMdl_StyleColor

    ' =================================================================
    ' HELPER FUNCTION
    ' =================================================================

    ' Cache untuk menyimpan brush yang sudah diambil
    Private _brushCache As New Dictionary(Of String, SolidColorBrush)

    ' Fallback colors jika resources tidak tersedia
    ' Mencakup CORE COLORS (Layer 1) dan SEMANTIC COLORS (Layer 2)
    Private ReadOnly _fallbackColors As New Dictionary(Of String, Color)

    ''' <summary>
    ''' Inisialisasi fallback colors - dipanggil sekali saat module diakses
    ''' Mencakup SEMUA warna dari StyleColor.xaml
    ''' </summary>
    Private Sub InitFallbackColors()
        If _fallbackColors.Count > 0 Then Return

        ' =================================================================
        ' LAYER 1: CORE COLORS (PALET WARNA DASAR)
        ' =================================================================

        ' PRIMARY (Hijau BOOKU - Brand Color)
        _fallbackColors.Add("clrPrimary", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrPrimaryLight", Color.FromRgb(&H4C, &HAF, &H50))
        _fallbackColors.Add("clrPrimaryLighter", Color.FromRgb(&H81, &HC7, &H84))
        _fallbackColors.Add("clrPrimaryDark", Color.FromRgb(&H2E, &H7D, &H32))
        _fallbackColors.Add("clrPrimaryDarker", Color.FromRgb(&H1B, &H5E, &H20))
        _fallbackColors.Add("clrPrimarySurface", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrPrimaryBorder", Color.FromRgb(&HA5, &HD6, &HA7))

        ' NEUTRAL (Abu-abu)
        _fallbackColors.Add("clrNeutral50", Color.FromRgb(&HFA, &HFA, &HFA))
        _fallbackColors.Add("clrNeutral100", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrNeutral200", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrNeutral300", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrNeutral400", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrNeutral500", Color.FromRgb(&H9E, &H9E, &H9E))
        _fallbackColors.Add("clrNeutral600", Color.FromRgb(&H75, &H75, &H75))
        _fallbackColors.Add("clrNeutral700", Color.FromRgb(&H61, &H61, &H61))
        _fallbackColors.Add("clrNeutral800", Color.FromRgb(&H42, &H42, &H42))
        _fallbackColors.Add("clrNeutral900", Color.FromRgb(&H21, &H21, &H21))

        ' STATUS - SUCCESS (Hijau)
        _fallbackColors.Add("clrSuccess", Color.FromRgb(&H4C, &HAF, &H50))
        _fallbackColors.Add("clrSuccessDark", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrSuccessSurface", Color.FromRgb(&HE8, &HF5, &HE9))

        ' STATUS - WARNING (Oranye)
        _fallbackColors.Add("clrWarning", Color.FromRgb(&HFF, &H98, &H0))
        _fallbackColors.Add("clrWarningDark", Color.FromRgb(&HF5, &H7C, &H0))
        _fallbackColors.Add("clrWarningSurface", Color.FromRgb(&HFF, &HF3, &HE0))

        ' STATUS - ERROR (Merah)
        _fallbackColors.Add("clrError", Color.FromRgb(&HF4, &H43, &H36))
        _fallbackColors.Add("clrErrorDark", Color.FromRgb(&HD3, &H2F, &H2F))
        _fallbackColors.Add("clrErrorSurface", Color.FromRgb(&HFF, &HEB, &HEE))

        ' STATUS - INFO (Biru)
        _fallbackColors.Add("clrInfo", Color.FromRgb(&H21, &H96, &HF3))
        _fallbackColors.Add("clrInfoDark", Color.FromRgb(&H19, &H76, &HD2))
        _fallbackColors.Add("clrInfoSurface", Color.FromRgb(&HE3, &HF2, &HFD))

        ' BASE
        _fallbackColors.Add("clrWhite", Colors.White)
        _fallbackColors.Add("clrBlack", Colors.Black)

        ' OVERLAY
        _fallbackColors.Add("clrOverlay", Color.FromArgb(&H80, &H0, &H0, &H0))
        _fallbackColors.Add("clrOverlayLight", Color.FromArgb(&HB3, &HFA, &HFA, &HFA))

        ' LEGACY
        _fallbackColors.Add("clrDataTahunLalu", Color.FromArgb(&HFF, &H3F, &H3F, &H80))

        ' =================================================================
        ' LAYER 2: SEMANTIC COLORS (WARNA SEMANTIK)
        ' =================================================================

        ' TEKS
        _fallbackColors.Add("clrTeksPrimer", Color.FromRgb(&H21, &H21, &H21))
        _fallbackColors.Add("clrTeksSekunder", Color.FromRgb(&H75, &H75, &H75))
        _fallbackColors.Add("clrTeksDisabled", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrTeksPlaceholder", Color.FromRgb(&H9E, &H9E, &H9E))
        _fallbackColors.Add("clrTeksLink", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrTeksAksen", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrTeksInvers", Colors.White)
        _fallbackColors.Add("clrTeksDataTahunLalu", Color.FromArgb(&HFF, &H3F, &H3F, &H80))

        ' BACKGROUND
        _fallbackColors.Add("clrBgWindow", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrBgUserControl", Colors.White)
        _fallbackColors.Add("clrBgPanel", Colors.White)
        _fallbackColors.Add("clrBgInput", Colors.White)
        _fallbackColors.Add("clrBgInputHover", Color.FromRgb(&HFA, &HFA, &HFA))
        _fallbackColors.Add("clrBgInputDisabled", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrBgInputReadOnly", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrBgHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrBgPressed", Color.FromRgb(&HA5, &HD6, &HA7))
        _fallbackColors.Add("clrBgSelected", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrBgPrimarySurface", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrBorderPrimary", Color.FromRgb(&HA5, &HD6, &HA7))

        ' DATAGRID
        _fallbackColors.Add("clrDataGridBg", Colors.White)
        _fallbackColors.Add("clrDataGridBgAlt", Color.FromRgb(&HFA, &HFA, &HFA))
        _fallbackColors.Add("clrDataGridHeader", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrDataGridHeaderFg", Color.FromRgb(&H42, &H42, &H42))
        _fallbackColors.Add("clrDataGridRowSelect", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrDataGridRowSelectFg", Colors.White)
        _fallbackColors.Add("clrDataGridRowHover", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrDataGridBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrDataGridGridLine", Color.FromRgb(&HEE, &HEE, &HEE))

        ' TOMBOL SEKUNDER (Default)
        _fallbackColors.Add("clrBtnBg", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrBtnBgHover", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrBtnBgPressed", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrBtnFg", Color.FromRgb(&H42, &H42, &H42))
        _fallbackColors.Add("clrBtnBorder", Color.FromRgb(&HBD, &HBD, &HBD))

        ' TOMBOL PRIMER (Aksi Utama)
        _fallbackColors.Add("clrBtnPrimerBg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrBtnPrimerBgHover", Color.FromRgb(&H2E, &H7D, &H32))
        _fallbackColors.Add("clrBtnPrimerBgPressed", Color.FromRgb(&H1B, &H5E, &H20))
        _fallbackColors.Add("clrBtnPrimerFg", Colors.White)
        _fallbackColors.Add("clrBtnPrimerBorder", Color.FromRgb(&H2E, &H7D, &H32))

        ' TOMBOL DISABLED
        _fallbackColors.Add("clrBtnDisabledBg", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrBtnDisabledFg", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrBtnDisabledBorder", Color.FromRgb(&HE0, &HE0, &HE0))

        ' BORDER
        _fallbackColors.Add("clrBorderDefault", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrBorderFocus", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrBorderDisabled", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrBorderError", Color.FromRgb(&HF4, &H43, &H36))
        _fallbackColors.Add("clrBorderSuccess", Color.FromRgb(&H4C, &HAF, &H50))

        ' MENU & NAVIGASI
        _fallbackColors.Add("clrMenuBg", Colors.White)
        _fallbackColors.Add("clrMenuFg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrMenuHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrMenuActive", Color.FromRgb(&HA5, &HD6, &HA7))
        _fallbackColors.Add("clrMenuBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrSubMenuBg", Colors.White)
        _fallbackColors.Add("clrSubMenuFg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrSubMenuHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrSubMenuBorder", Color.FromRgb(&HA5, &HD6, &HA7))

        ' TAB CONTROL
        _fallbackColors.Add("clrTabBg", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrTabBgSelected", Colors.White)
        _fallbackColors.Add("clrTabBgHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrTabFg", Color.FromRgb(&H61, &H61, &H61))
        _fallbackColors.Add("clrTabFgSelected", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrTabBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrTabBorderSelected", Color.FromRgb(&H38, &H8E, &H3C))

        ' PROGRESS BAR
        _fallbackColors.Add("clrProgressBg", Color.FromRgb(&HEE, &HEE, &HEE))
        _fallbackColors.Add("clrProgressFg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrProgressBorder", Color.FromRgb(&HE0, &HE0, &HE0))

        ' STATUS BAR
        _fallbackColors.Add("clrStatusBarBg", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrStatusBarFg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrStatusBarBorder", Color.FromRgb(&HA5, &HD6, &HA7))

        ' SCROLLBAR
        _fallbackColors.Add("clrScrollbarBg", Color.FromRgb(&HF5, &HF5, &HF5))
        _fallbackColors.Add("clrScrollbarThumb", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrScrollbarThumbHover", Color.FromRgb(&H9E, &H9E, &H9E))

        ' GROUPBOX
        _fallbackColors.Add("clrGroupBoxBg", Colors.White)
        _fallbackColors.Add("clrGroupBoxBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrGroupBoxHeader", Color.FromRgb(&H38, &H8E, &H3C))

        ' CHECKBOX & RADIOBUTTON
        _fallbackColors.Add("clrCheckBoxBg", Colors.White)
        _fallbackColors.Add("clrCheckBoxBorder", Color.FromRgb(&HBD, &HBD, &HBD))
        _fallbackColors.Add("clrCheckBoxCheckedBg", Color.FromRgb(&H38, &H8E, &H3C))
        _fallbackColors.Add("clrCheckBoxCheckedBorder", Color.FromRgb(&H2E, &H7D, &H32))
        _fallbackColors.Add("clrCheckBoxCheckedMark", Colors.White)

        ' COMBOBOX
        _fallbackColors.Add("clrComboBoxBg", Colors.White)
        _fallbackColors.Add("clrComboBoxBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrComboBoxArrow", Color.FromRgb(&H75, &H75, &H75))
        _fallbackColors.Add("clrComboBoxDropdownBg", Colors.White)
        _fallbackColors.Add("clrComboBoxItemHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrComboBoxItemSelected", Color.FromRgb(&HE8, &HF5, &HE9))

        ' DATEPICKER
        _fallbackColors.Add("clrDatePickerBg", Colors.White)
        _fallbackColors.Add("clrDatePickerBorder", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrDatePickerCalendarBg", Colors.White)
        _fallbackColors.Add("clrDatePickerDayHover", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrDatePickerDaySelected", Color.FromRgb(&H38, &H8E, &H3C))

        ' STATUS COLORS (untuk badge, alert, notifikasi)
        _fallbackColors.Add("clrStatusSukses", Color.FromRgb(&H4C, &HAF, &H50))
        _fallbackColors.Add("clrStatusSuksesBg", Color.FromRgb(&HE8, &HF5, &HE9))
        _fallbackColors.Add("clrStatusPeringatan", Color.FromRgb(&HFF, &H98, &H0))
        _fallbackColors.Add("clrStatusPeringatanBg", Color.FromRgb(&HFF, &HF3, &HE0))
        _fallbackColors.Add("clrStatusError", Color.FromRgb(&HF4, &H43, &H36))
        _fallbackColors.Add("clrStatusErrorBg", Color.FromRgb(&HFF, &HEB, &HEE))
        _fallbackColors.Add("clrStatusInfo", Color.FromRgb(&H21, &H96, &HF3))
        _fallbackColors.Add("clrStatusInfoBg", Color.FromRgb(&HE3, &HF2, &HFD))

        ' SEPARATOR
        _fallbackColors.Add("clrSeparator", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrSeparatorLight", Color.FromRgb(&HEE, &HEE, &HEE))

        ' OVERLAY & LOADING
        _fallbackColors.Add("clrBgOverlay", Color.FromArgb(&H80, &H0, &H0, &H0))
        _fallbackColors.Add("clrBgOverlayLight", Color.FromArgb(&HB3, &HFA, &HFA, &HFA))
        _fallbackColors.Add("clrLoadingStroke", Color.FromRgb(&HE0, &HE0, &HE0))
        _fallbackColors.Add("clrLoadingStrokeDark", Color.FromRgb(&H61, &H61, &H61))
        _fallbackColors.Add("clrLoadingText", Color.FromRgb(&H42, &H42, &H42))

        ' SIDEBAR
        _fallbackColors.Add("clrBgSidebar", Color.FromRgb(&H9E, &H9E, &H9E))
    End Sub

    ''' <summary>
    ''' Mengambil SolidColorBrush dari Application.Resources berdasarkan key
    ''' Dengan caching dan fallback ke warna hardcoded
    ''' </summary>
    Private Function GetBrush(key As String) As SolidColorBrush
        ' Pastikan fallback colors sudah diinisialisasi
        InitFallbackColors()

        ' Cek cache dulu
        If _brushCache.ContainsKey(key) Then
            Return _brushCache(key)
        End If

        Dim result As SolidColorBrush = Nothing

        ' Coba ambil dari Application.Resources
        If Application.Current IsNot Nothing Then
            Dim resource = Application.Current.Resources(key)
            If resource IsNot Nothing Then
                result = TryCast(resource, SolidColorBrush)
            End If
        End If

        ' Jika tidak dapat, gunakan fallback
        If result Is Nothing AndAlso _fallbackColors.ContainsKey(key) Then
            result = New SolidColorBrush(_fallbackColors(key))
        End If

        ' Simpan ke cache jika berhasil
        If result IsNot Nothing Then
            _brushCache(key) = result
        End If

        Return result
    End Function

    ''' <summary>
    ''' Mengambil Color dari Application.Resources dan membuat SolidColorBrush baru
    ''' Dengan caching dan fallback ke warna hardcoded
    ''' </summary>
    Private Function GetColorBrush(key As String) As SolidColorBrush
        ' Pastikan fallback colors sudah diinisialisasi
        InitFallbackColors()

        ' Cek cache dulu
        If _brushCache.ContainsKey(key) Then
            Return _brushCache(key)
        End If

        Dim result As SolidColorBrush = Nothing

        ' Coba ambil dari Application.Resources
        If Application.Current IsNot Nothing Then
            Dim resource = Application.Current.Resources(key)
            If resource IsNot Nothing AndAlso TypeOf resource Is Color Then
                result = New SolidColorBrush(DirectCast(resource, Color))
            End If
        End If

        ' Jika tidak dapat, gunakan fallback
        If result Is Nothing AndAlso _fallbackColors.ContainsKey(key) Then
            result = New SolidColorBrush(_fallbackColors(key))
        End If

        ' Simpan ke cache jika berhasil
        If result IsNot Nothing Then
            _brushCache(key) = result
        End If

        Return result
    End Function


    ' =================================================================
    ' LAYER 1: CORE COLORS (PALET WARNA DASAR)
    ' =================================================================

    ' ===== PRIMARY (Hijau BOOKU - Brand Color) =====
    Public ReadOnly Property clrPrimary As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimary")
        End Get
    End Property

    Public ReadOnly Property clrPrimaryLight As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimaryLight")
        End Get
    End Property

    Public ReadOnly Property clrPrimaryLighter As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimaryLighter")
        End Get
    End Property

    Public ReadOnly Property clrPrimaryDark As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimaryDark")
        End Get
    End Property

    Public ReadOnly Property clrPrimaryDarker As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimaryDarker")
        End Get
    End Property

    Public ReadOnly Property clrPrimarySurface As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimarySurface")
        End Get
    End Property

    Public ReadOnly Property clrPrimaryBorder As SolidColorBrush
        Get
            Return GetColorBrush("clrPrimaryBorder")
        End Get
    End Property


    ' ===== NEUTRAL (Abu-abu) =====
    Public ReadOnly Property clrNeutral50 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral50")
        End Get
    End Property

    Public ReadOnly Property clrNeutral100 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral100")
        End Get
    End Property

    Public ReadOnly Property clrNeutral200 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral200")
        End Get
    End Property

    Public ReadOnly Property clrNeutral300 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral300")
        End Get
    End Property

    Public ReadOnly Property clrNeutral400 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral400")
        End Get
    End Property

    Public ReadOnly Property clrNeutral500 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral500")
        End Get
    End Property

    Public ReadOnly Property clrNeutral600 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral600")
        End Get
    End Property

    Public ReadOnly Property clrNeutral700 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral700")
        End Get
    End Property

    Public ReadOnly Property clrNeutral800 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral800")
        End Get
    End Property

    Public ReadOnly Property clrNeutral900 As SolidColorBrush
        Get
            Return GetColorBrush("clrNeutral900")
        End Get
    End Property


    ' ===== STATUS - SUCCESS (Hijau) =====
    Public ReadOnly Property clrSuccess As SolidColorBrush
        Get
            Return GetColorBrush("clrSuccess")
        End Get
    End Property

    Public ReadOnly Property clrSuccessDark As SolidColorBrush
        Get
            Return GetColorBrush("clrSuccessDark")
        End Get
    End Property

    Public ReadOnly Property clrSuccessSurface As SolidColorBrush
        Get
            Return GetColorBrush("clrSuccessSurface")
        End Get
    End Property


    ' ===== STATUS - WARNING (Oranye) =====
    Public ReadOnly Property clrWarning As SolidColorBrush
        Get
            Return GetColorBrush("clrWarning")
        End Get
    End Property

    Public ReadOnly Property clrWarningDark As SolidColorBrush
        Get
            Return GetColorBrush("clrWarningDark")
        End Get
    End Property

    Public ReadOnly Property clrWarningSurface As SolidColorBrush
        Get
            Return GetColorBrush("clrWarningSurface")
        End Get
    End Property


    ' ===== STATUS - ERROR (Merah) =====
    Public ReadOnly Property clrError As SolidColorBrush
        Get
            Return GetColorBrush("clrError")
        End Get
    End Property

    Public ReadOnly Property clrErrorDark As SolidColorBrush
        Get
            Return GetColorBrush("clrErrorDark")
        End Get
    End Property

    Public ReadOnly Property clrErrorSurface As SolidColorBrush
        Get
            Return GetColorBrush("clrErrorSurface")
        End Get
    End Property


    ' ===== STATUS - INFO (Biru) =====
    Public ReadOnly Property clrInfo As SolidColorBrush
        Get
            Return GetColorBrush("clrInfo")
        End Get
    End Property

    Public ReadOnly Property clrInfoDark As SolidColorBrush
        Get
            Return GetColorBrush("clrInfoDark")
        End Get
    End Property

    Public ReadOnly Property clrInfoSurface As SolidColorBrush
        Get
            Return GetColorBrush("clrInfoSurface")
        End Get
    End Property


    ' ===== BASE =====
    Public ReadOnly Property clrWhite As SolidColorBrush
        Get
            Return GetColorBrush("clrWhite")
        End Get
    End Property

    Public ReadOnly Property clrBlack As SolidColorBrush
        Get
            Return GetColorBrush("clrBlack")
        End Get
    End Property


    ' ===== OVERLAY =====
    Public ReadOnly Property clrOverlay As SolidColorBrush
        Get
            Return GetColorBrush("clrOverlay")
        End Get
    End Property

    Public ReadOnly Property clrOverlayLight As SolidColorBrush
        Get
            Return GetColorBrush("clrOverlayLight")
        End Get
    End Property


    ' ===== LEGACY (untuk backward compatibility) =====
    Public ReadOnly Property clrDataTahunLalu As SolidColorBrush
        Get
            Return GetBrush("clrTeksDataTahunLalu")
        End Get
    End Property


    ' =================================================================
    ' LAYER 2: SEMANTIC COLORS (WARNA SEMANTIK)
    ' =================================================================


    ' =================================================================
    ' TEKS
    ' =================================================================

    Public ReadOnly Property clrTeksPrimer As SolidColorBrush
        Get
            Return GetBrush("clrTeksPrimer")
        End Get
    End Property

    Public ReadOnly Property clrTeksSekunder As SolidColorBrush
        Get
            Return GetBrush("clrTeksSekunder")
        End Get
    End Property

    Public ReadOnly Property clrTeksDisabled As SolidColorBrush
        Get
            Return GetBrush("clrTeksDisabled")
        End Get
    End Property

    Public ReadOnly Property clrTeksPlaceholder As SolidColorBrush
        Get
            Return GetBrush("clrTeksPlaceholder")
        End Get
    End Property

    Public ReadOnly Property clrTeksLink As SolidColorBrush
        Get
            Return GetBrush("clrTeksLink")
        End Get
    End Property

    Public ReadOnly Property clrTeksAksen As SolidColorBrush
        Get
            Return GetBrush("clrTeksAksen")
        End Get
    End Property

    Public ReadOnly Property clrTeksInvers As SolidColorBrush
        Get
            Return GetBrush("clrTeksInvers")
        End Get
    End Property


    ' =================================================================
    ' BACKGROUND
    ' =================================================================

    Public ReadOnly Property clrBgWindow As SolidColorBrush
        Get
            Return GetBrush("clrBgWindow")
        End Get
    End Property

    Public ReadOnly Property clrBgUserControl As SolidColorBrush
        Get
            Return GetBrush("clrBgUserControl")
        End Get
    End Property

    Public ReadOnly Property clrBgPanel As SolidColorBrush
        Get
            Return GetBrush("clrBgPanel")
        End Get
    End Property

    Public ReadOnly Property clrBgInput As SolidColorBrush
        Get
            Return GetBrush("clrBgInput")
        End Get
    End Property

    Public ReadOnly Property clrBgInputHover As SolidColorBrush
        Get
            Return GetBrush("clrBgInputHover")
        End Get
    End Property

    Public ReadOnly Property clrBgInputDisabled As SolidColorBrush
        Get
            Return GetBrush("clrBgInputDisabled")
        End Get
    End Property

    Public ReadOnly Property clrBgInputReadOnly As SolidColorBrush
        Get
            Return GetBrush("clrBgInputReadOnly")
        End Get
    End Property

    Public ReadOnly Property clrBgHover As SolidColorBrush
        Get
            Return GetBrush("clrBgHover")
        End Get
    End Property

    Public ReadOnly Property clrBgPressed As SolidColorBrush
        Get
            Return GetBrush("clrBgPressed")
        End Get
    End Property

    Public ReadOnly Property clrBgSelected As SolidColorBrush
        Get
            Return GetBrush("clrBgSelected")
        End Get
    End Property

    Public ReadOnly Property clrBgPrimarySurface As SolidColorBrush
        Get
            Return GetBrush("clrBgPrimarySurface")
        End Get
    End Property

    Public ReadOnly Property clrBorderPrimary As SolidColorBrush
        Get
            Return GetBrush("clrBorderPrimary")
        End Get
    End Property


    ' =================================================================
    ' DATAGRID
    ' =================================================================

    Public ReadOnly Property clrDataGridBg As SolidColorBrush
        Get
            Return GetBrush("clrDataGridBg")
        End Get
    End Property

    Public ReadOnly Property clrDataGridBgAlt As SolidColorBrush
        Get
            Return GetBrush("clrDataGridBgAlt")
        End Get
    End Property

    Public ReadOnly Property clrDataGridHeader As SolidColorBrush
        Get
            Return GetBrush("clrDataGridHeader")
        End Get
    End Property

    Public ReadOnly Property clrDataGridHeaderFg As SolidColorBrush
        Get
            Return GetBrush("clrDataGridHeaderFg")
        End Get
    End Property

    Public ReadOnly Property clrDataGridRowSelect As SolidColorBrush
        Get
            Return GetBrush("clrDataGridRowSelect")
        End Get
    End Property

    Public ReadOnly Property clrDataGridRowSelectFg As SolidColorBrush
        Get
            Return GetBrush("clrDataGridRowSelectFg")
        End Get
    End Property

    Public ReadOnly Property clrDataGridRowHover As SolidColorBrush
        Get
            Return GetBrush("clrDataGridRowHover")
        End Get
    End Property

    Public ReadOnly Property clrDataGridBorder As SolidColorBrush
        Get
            Return GetBrush("clrDataGridBorder")
        End Get
    End Property

    Public ReadOnly Property clrDataGridGridLine As SolidColorBrush
        Get
            Return GetBrush("clrDataGridGridLine")
        End Get
    End Property


    ' =================================================================
    ' TOMBOL SEKUNDER (Default)
    ' =================================================================

    Public ReadOnly Property clrBtnBg As SolidColorBrush
        Get
            Return GetBrush("clrBtnBg")
        End Get
    End Property

    Public ReadOnly Property clrBtnBgHover As SolidColorBrush
        Get
            Return GetBrush("clrBtnBgHover")
        End Get
    End Property

    Public ReadOnly Property clrBtnBgPressed As SolidColorBrush
        Get
            Return GetBrush("clrBtnBgPressed")
        End Get
    End Property

    Public ReadOnly Property clrBtnFg As SolidColorBrush
        Get
            Return GetBrush("clrBtnFg")
        End Get
    End Property

    Public ReadOnly Property clrBtnBorder As SolidColorBrush
        Get
            Return GetBrush("clrBtnBorder")
        End Get
    End Property


    ' =================================================================
    ' TOMBOL PRIMER (Aksi Utama)
    ' =================================================================

    Public ReadOnly Property clrBtnPrimerBg As SolidColorBrush
        Get
            Return GetBrush("clrBtnPrimerBg")
        End Get
    End Property

    Public ReadOnly Property clrBtnPrimerBgHover As SolidColorBrush
        Get
            Return GetBrush("clrBtnPrimerBgHover")
        End Get
    End Property

    Public ReadOnly Property clrBtnPrimerBgPressed As SolidColorBrush
        Get
            Return GetBrush("clrBtnPrimerBgPressed")
        End Get
    End Property

    Public ReadOnly Property clrBtnPrimerFg As SolidColorBrush
        Get
            Return GetBrush("clrBtnPrimerFg")
        End Get
    End Property

    Public ReadOnly Property clrBtnPrimerBorder As SolidColorBrush
        Get
            Return GetBrush("clrBtnPrimerBorder")
        End Get
    End Property


    ' =================================================================
    ' TOMBOL DISABLED
    ' =================================================================

    Public ReadOnly Property clrBtnDisabledBg As SolidColorBrush
        Get
            Return GetBrush("clrBtnDisabledBg")
        End Get
    End Property

    Public ReadOnly Property clrBtnDisabledFg As SolidColorBrush
        Get
            Return GetBrush("clrBtnDisabledFg")
        End Get
    End Property

    Public ReadOnly Property clrBtnDisabledBorder As SolidColorBrush
        Get
            Return GetBrush("clrBtnDisabledBorder")
        End Get
    End Property


    ' =================================================================
    ' BORDER
    ' =================================================================

    Public ReadOnly Property clrBorderDefault As SolidColorBrush
        Get
            Return GetBrush("clrBorderDefault")
        End Get
    End Property

    Public ReadOnly Property clrBorderFocus As SolidColorBrush
        Get
            Return GetBrush("clrBorderFocus")
        End Get
    End Property

    Public ReadOnly Property clrBorderDisabled As SolidColorBrush
        Get
            Return GetBrush("clrBorderDisabled")
        End Get
    End Property

    Public ReadOnly Property clrBorderError As SolidColorBrush
        Get
            Return GetBrush("clrBorderError")
        End Get
    End Property

    Public ReadOnly Property clrBorderSuccess As SolidColorBrush
        Get
            Return GetBrush("clrBorderSuccess")
        End Get
    End Property


    ' =================================================================
    ' MENU & NAVIGASI
    ' =================================================================

    Public ReadOnly Property clrMenuBg As SolidColorBrush
        Get
            Return GetBrush("clrMenuBg")
        End Get
    End Property

    Public ReadOnly Property clrMenuFg As SolidColorBrush
        Get
            Return GetBrush("clrMenuFg")
        End Get
    End Property

    Public ReadOnly Property clrMenuHover As SolidColorBrush
        Get
            Return GetBrush("clrMenuHover")
        End Get
    End Property

    Public ReadOnly Property clrMenuActive As SolidColorBrush
        Get
            Return GetBrush("clrMenuActive")
        End Get
    End Property

    Public ReadOnly Property clrMenuBorder As SolidColorBrush
        Get
            Return GetBrush("clrMenuBorder")
        End Get
    End Property

    Public ReadOnly Property clrSubMenuBg As SolidColorBrush
        Get
            Return GetBrush("clrSubMenuBg")
        End Get
    End Property

    Public ReadOnly Property clrSubMenuFg As SolidColorBrush
        Get
            Return GetBrush("clrSubMenuFg")
        End Get
    End Property

    Public ReadOnly Property clrSubMenuHover As SolidColorBrush
        Get
            Return GetBrush("clrSubMenuHover")
        End Get
    End Property

    Public ReadOnly Property clrSubMenuBorder As SolidColorBrush
        Get
            Return GetBrush("clrSubMenuBorder")
        End Get
    End Property


    ' =================================================================
    ' TAB CONTROL
    ' =================================================================

    Public ReadOnly Property clrTabBg As SolidColorBrush
        Get
            Return GetBrush("clrTabBg")
        End Get
    End Property

    Public ReadOnly Property clrTabBgSelected As SolidColorBrush
        Get
            Return GetBrush("clrTabBgSelected")
        End Get
    End Property

    Public ReadOnly Property clrTabBgHover As SolidColorBrush
        Get
            Return GetBrush("clrTabBgHover")
        End Get
    End Property

    Public ReadOnly Property clrTabFg As SolidColorBrush
        Get
            Return GetBrush("clrTabFg")
        End Get
    End Property

    Public ReadOnly Property clrTabFgSelected As SolidColorBrush
        Get
            Return GetBrush("clrTabFgSelected")
        End Get
    End Property

    Public ReadOnly Property clrTabBorder As SolidColorBrush
        Get
            Return GetBrush("clrTabBorder")
        End Get
    End Property

    Public ReadOnly Property clrTabBorderSelected As SolidColorBrush
        Get
            Return GetBrush("clrTabBorderSelected")
        End Get
    End Property


    ' =================================================================
    ' PROGRESS BAR
    ' =================================================================

    Public ReadOnly Property clrProgressBg As SolidColorBrush
        Get
            Return GetBrush("clrProgressBg")
        End Get
    End Property

    Public ReadOnly Property clrProgressFg As SolidColorBrush
        Get
            Return GetBrush("clrProgressFg")
        End Get
    End Property

    Public ReadOnly Property clrProgressBorder As SolidColorBrush
        Get
            Return GetBrush("clrProgressBorder")
        End Get
    End Property


    ' =================================================================
    ' STATUS BAR
    ' =================================================================

    Public ReadOnly Property clrStatusBarBg As SolidColorBrush
        Get
            Return GetBrush("clrStatusBarBg")
        End Get
    End Property

    Public ReadOnly Property clrStatusBarFg As SolidColorBrush
        Get
            Return GetBrush("clrStatusBarFg")
        End Get
    End Property

    Public ReadOnly Property clrStatusBarBorder As SolidColorBrush
        Get
            Return GetBrush("clrStatusBarBorder")
        End Get
    End Property


    ' =================================================================
    ' SCROLLBAR
    ' =================================================================

    Public ReadOnly Property clrScrollbarBg As SolidColorBrush
        Get
            Return GetBrush("clrScrollbarBg")
        End Get
    End Property

    Public ReadOnly Property clrScrollbarThumb As SolidColorBrush
        Get
            Return GetBrush("clrScrollbarThumb")
        End Get
    End Property

    Public ReadOnly Property clrScrollbarThumbHover As SolidColorBrush
        Get
            Return GetBrush("clrScrollbarThumbHover")
        End Get
    End Property


    ' =================================================================
    ' GROUPBOX
    ' =================================================================

    Public ReadOnly Property clrGroupBoxBg As SolidColorBrush
        Get
            Return GetBrush("clrGroupBoxBg")
        End Get
    End Property

    Public ReadOnly Property clrGroupBoxBorder As SolidColorBrush
        Get
            Return GetBrush("clrGroupBoxBorder")
        End Get
    End Property

    Public ReadOnly Property clrGroupBoxHeader As SolidColorBrush
        Get
            Return GetBrush("clrGroupBoxHeader")
        End Get
    End Property


    ' =================================================================
    ' CHECKBOX & RADIOBUTTON
    ' =================================================================

    Public ReadOnly Property clrCheckBoxBg As SolidColorBrush
        Get
            Return GetBrush("clrCheckBoxBg")
        End Get
    End Property

    Public ReadOnly Property clrCheckBoxBorder As SolidColorBrush
        Get
            Return GetBrush("clrCheckBoxBorder")
        End Get
    End Property

    Public ReadOnly Property clrCheckBoxCheckedBg As SolidColorBrush
        Get
            Return GetBrush("clrCheckBoxCheckedBg")
        End Get
    End Property

    Public ReadOnly Property clrCheckBoxCheckedBorder As SolidColorBrush
        Get
            Return GetBrush("clrCheckBoxCheckedBorder")
        End Get
    End Property

    Public ReadOnly Property clrCheckBoxCheckedMark As SolidColorBrush
        Get
            Return GetBrush("clrCheckBoxCheckedMark")
        End Get
    End Property


    ' =================================================================
    ' COMBOBOX
    ' =================================================================

    Public ReadOnly Property clrComboBoxBg As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxBg")
        End Get
    End Property

    Public ReadOnly Property clrComboBoxBorder As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxBorder")
        End Get
    End Property

    Public ReadOnly Property clrComboBoxArrow As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxArrow")
        End Get
    End Property

    Public ReadOnly Property clrComboBoxDropdownBg As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxDropdownBg")
        End Get
    End Property

    Public ReadOnly Property clrComboBoxItemHover As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxItemHover")
        End Get
    End Property

    Public ReadOnly Property clrComboBoxItemSelected As SolidColorBrush
        Get
            Return GetBrush("clrComboBoxItemSelected")
        End Get
    End Property


    ' =================================================================
    ' DATEPICKER
    ' =================================================================

    Public ReadOnly Property clrDatePickerBg As SolidColorBrush
        Get
            Return GetBrush("clrDatePickerBg")
        End Get
    End Property

    Public ReadOnly Property clrDatePickerBorder As SolidColorBrush
        Get
            Return GetBrush("clrDatePickerBorder")
        End Get
    End Property

    Public ReadOnly Property clrDatePickerCalendarBg As SolidColorBrush
        Get
            Return GetBrush("clrDatePickerCalendarBg")
        End Get
    End Property

    Public ReadOnly Property clrDatePickerDayHover As SolidColorBrush
        Get
            Return GetBrush("clrDatePickerDayHover")
        End Get
    End Property

    Public ReadOnly Property clrDatePickerDaySelected As SolidColorBrush
        Get
            Return GetBrush("clrDatePickerDaySelected")
        End Get
    End Property


    ' =================================================================
    ' STATUS COLORS (untuk badge, alert, notifikasi)
    ' =================================================================

    Public ReadOnly Property clrStatusSukses As SolidColorBrush
        Get
            Return GetBrush("clrStatusSukses")
        End Get
    End Property

    Public ReadOnly Property clrStatusSuksesBg As SolidColorBrush
        Get
            Return GetBrush("clrStatusSuksesBg")
        End Get
    End Property

    Public ReadOnly Property clrStatusPeringatan As SolidColorBrush
        Get
            Return GetBrush("clrStatusPeringatan")
        End Get
    End Property

    Public ReadOnly Property clrStatusPeringatanBg As SolidColorBrush
        Get
            Return GetBrush("clrStatusPeringatanBg")
        End Get
    End Property

    Public ReadOnly Property clrStatusError As SolidColorBrush
        Get
            Return GetBrush("clrStatusError")
        End Get
    End Property

    Public ReadOnly Property clrStatusErrorBg As SolidColorBrush
        Get
            Return GetBrush("clrStatusErrorBg")
        End Get
    End Property

    Public ReadOnly Property clrStatusInfo As SolidColorBrush
        Get
            Return GetBrush("clrStatusInfo")
        End Get
    End Property

    Public ReadOnly Property clrStatusInfoBg As SolidColorBrush
        Get
            Return GetBrush("clrStatusInfoBg")
        End Get
    End Property


    ' =================================================================
    ' SEPARATOR
    ' =================================================================

    Public ReadOnly Property clrSeparator As SolidColorBrush
        Get
            Return GetBrush("clrSeparator")
        End Get
    End Property

    Public ReadOnly Property clrSeparatorLight As SolidColorBrush
        Get
            Return GetBrush("clrSeparatorLight")
        End Get
    End Property


    ' =================================================================
    ' OVERLAY & LOADING
    ' =================================================================

    Public ReadOnly Property clrBgOverlay As SolidColorBrush
        Get
            Return GetBrush("clrBgOverlay")
        End Get
    End Property

    Public ReadOnly Property clrBgOverlayLight As SolidColorBrush
        Get
            Return GetBrush("clrBgOverlayLight")
        End Get
    End Property

    Public ReadOnly Property clrLoadingStroke As SolidColorBrush
        Get
            Return GetBrush("clrLoadingStroke")
        End Get
    End Property

    Public ReadOnly Property clrLoadingStrokeDark As SolidColorBrush
        Get
            Return GetBrush("clrLoadingStrokeDark")
        End Get
    End Property

    Public ReadOnly Property clrLoadingText As SolidColorBrush
        Get
            Return GetBrush("clrLoadingText")
        End Get
    End Property


    ' =================================================================
    ' SIDEBAR
    ' =================================================================

    Public ReadOnly Property clrBgSidebar As SolidColorBrush
        Get
            Return GetBrush("clrBgSidebar")
        End Get
    End Property


    ' =================================================================
    ' LEGACY (untuk backward compatibility)
    ' =================================================================

    Public ReadOnly Property clrTeksDataTahunLalu As SolidColorBrush
        Get
            Return GetBrush("clrTeksDataTahunLalu")
        End Get
    End Property


End Module
