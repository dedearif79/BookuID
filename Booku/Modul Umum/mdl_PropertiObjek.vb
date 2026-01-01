Imports bcomm
Imports System.Windows

Module mdl_PropertiObjek

    'STYLE TABEL DASAR :
    Public Sub StyleTabelDasar(ByVal Tabel As DataGridView)
        If Proses = False Then
            Tabel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Tabel.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            Tabel.ColumnHeadersVisible = True
            Tabel.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            Tabel.AllowUserToAddRows = False
            Tabel.AllowUserToDeleteRows = False
            Tabel.AllowUserToOrderColumns = False
            Tabel.AllowUserToResizeRows = False
            Tabel.BorderStyle = BorderStyle.None
            Tabel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Tabel.MultiSelect = False
            Tabel.RowHeadersVisible = False
            Tabel.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Tabel.DefaultCellStyle.Padding = New Padding(3, 0, 3, 0)
        End If
    End Sub

    'STYLE TABEL UTAMA :
    Public Sub StyleTabelUtama(ByVal Tabel As DataGridView)
        If Proses = False Then
            StyleTabelDasar(Tabel)
            Tabel.ReadOnly = True
            Tabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top _
                Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right),
                System.Windows.Forms.AnchorStyles)
        End If
    End Sub

    'STYLE TABEL BISA DI-CREATE :
    Public Sub StyleTabelBisaDicreate(ByVal Tabel As DataGridView)
        If Proses = False Then
            StyleTabelDasar(Tabel)
            Tabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top _
                Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right),
                System.Windows.Forms.AnchorStyles)
        End If
    End Sub

    'STYLE TABEL PEMBANTU :
    Public Sub StyleTabelPembantu(ByVal Tabel As DataGridView)
        If Proses = False Then
            Tabel.AllowUserToAddRows = False
            Tabel.AllowUserToDeleteRows = False
            Tabel.ReadOnly = True
            Tabel.RowHeadersVisible = False
            Tabel.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End If
    End Sub

    'PEMECAH RIBUAN UNTUK TEXTBOX :
    Public Sub PemecahRibuanUntukTextBox(ByVal Teks As TextBox)
        Teks.MaxLength = 21
        Try
            If Teks.Text.Trim() <> Nothing Then
                Teks.Text = CDec(Teks.Text).ToString("N0")
                Teks.SelectionStart = Teks.TextLength
            End If
        Catch ex As Exception
        End Try
        If AmbilAngka(Teks.Text) = 0 Then Teks.Text = Nothing
        Teks.TextAlign = Forms.HorizontalAlignment.Right
    End Sub

    'PEMECAH RIBUAN UNTUK LABEL :
    Public Function PemecahRibuanUntukLabel(ByVal Kiriman As String) As String
        Dim Hasil = Kosongan
        Return Hasil
    End Function

    'STYLE TOMBOL 1 :
    Public Sub StyleTombol_1(ByVal Tombol As Button)
        Tombol.Size = New System.Drawing.Size(100, 100)
    End Sub


    'STYLE INPUTAN COA :
    Public Sub StyleInputanCOA(ByVal Teks As TextBox)
        Teks.MaxLength = JumlahDigitCOA
    End Sub


End Module
