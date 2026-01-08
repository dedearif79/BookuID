<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_Laporan_LabaRugi_X
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataTabelUtama = New DataGridView()
        Nama_Akun = New DataGridViewTextBoxColumn()
        Kode_Akun = New DataGridViewTextBoxColumn()
        Saldo_Januari = New DataGridViewTextBoxColumn()
        Saldo_Februari = New DataGridViewTextBoxColumn()
        Saldo_Maret = New DataGridViewTextBoxColumn()
        Saldo_April = New DataGridViewTextBoxColumn()
        Saldo_Mei = New DataGridViewTextBoxColumn()
        Saldo_Juni = New DataGridViewTextBoxColumn()
        Saldo_Juli = New DataGridViewTextBoxColumn()
        Saldo_Agustus = New DataGridViewTextBoxColumn()
        Saldo_September = New DataGridViewTextBoxColumn()
        Saldo_Oktober = New DataGridViewTextBoxColumn()
        Saldo_Nopember = New DataGridViewTextBoxColumn()
        Saldo_Desember = New DataGridViewTextBoxColumn()
        Saldo_Total = New DataGridViewTextBoxColumn()
        btn_Refresh = New Button()
        lbl_Judul = New Label()
        btn_Export = New Button()
        btn_TrialBalance = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nama_Akun, Kode_Akun, Saldo_Januari, Saldo_Februari, Saldo_Maret, Saldo_April, Saldo_Mei, Saldo_Juni, Saldo_Juli, Saldo_Agustus, Saldo_September, Saldo_Oktober, Saldo_Nopember, Saldo_Desember, Saldo_Total})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1494, 617)
        DataTabelUtama.TabIndex = 10018
        ' 
        ' Nama_Akun
        ' 
        Nama_Akun.Frozen = True
        Nama_Akun.HeaderText = "NAMA AKUN"
        Nama_Akun.Name = "Nama_Akun"
        Nama_Akun.ReadOnly = True
        Nama_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Akun.Width = 150
        ' 
        ' Kode_Akun
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        Kode_Akun.DefaultCellStyle = DataGridViewCellStyle1
        Kode_Akun.Frozen = True
        Kode_Akun.HeaderText = "KODE"
        Kode_Akun.Name = "Kode_Akun"
        Kode_Akun.ReadOnly = True
        Kode_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Akun.Width = 54
        ' 
        ' Saldo_Januari
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Saldo_Januari.DefaultCellStyle = DataGridViewCellStyle2
        Saldo_Januari.HeaderText = "JANUARI"
        Saldo_Januari.Name = "Saldo_Januari"
        Saldo_Januari.ReadOnly = True
        Saldo_Januari.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Februari
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Saldo_Februari.DefaultCellStyle = DataGridViewCellStyle3
        Saldo_Februari.HeaderText = "FEBRUARI"
        Saldo_Februari.Name = "Saldo_Februari"
        Saldo_Februari.ReadOnly = True
        Saldo_Februari.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Maret
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Saldo_Maret.DefaultCellStyle = DataGridViewCellStyle4
        Saldo_Maret.HeaderText = "MARET"
        Saldo_Maret.Name = "Saldo_Maret"
        Saldo_Maret.ReadOnly = True
        Saldo_Maret.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_April
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        Saldo_April.DefaultCellStyle = DataGridViewCellStyle5
        Saldo_April.HeaderText = "APRIL"
        Saldo_April.Name = "Saldo_April"
        Saldo_April.ReadOnly = True
        Saldo_April.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Mei
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        Saldo_Mei.DefaultCellStyle = DataGridViewCellStyle6
        Saldo_Mei.HeaderText = "MEI"
        Saldo_Mei.Name = "Saldo_Mei"
        Saldo_Mei.ReadOnly = True
        Saldo_Mei.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Juni
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        Saldo_Juni.DefaultCellStyle = DataGridViewCellStyle7
        Saldo_Juni.HeaderText = "JUNI"
        Saldo_Juni.Name = "Saldo_Juni"
        Saldo_Juni.ReadOnly = True
        Saldo_Juni.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Juli
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        DataGridViewCellStyle8.NullValue = Nothing
        Saldo_Juli.DefaultCellStyle = DataGridViewCellStyle8
        Saldo_Juli.HeaderText = "JULI"
        Saldo_Juli.Name = "Saldo_Juli"
        Saldo_Juli.ReadOnly = True
        Saldo_Juli.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Agustus
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = Nothing
        Saldo_Agustus.DefaultCellStyle = DataGridViewCellStyle9
        Saldo_Agustus.HeaderText = "AGUSTUS"
        Saldo_Agustus.Name = "Saldo_Agustus"
        Saldo_Agustus.ReadOnly = True
        Saldo_Agustus.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_September
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        DataGridViewCellStyle10.NullValue = Nothing
        Saldo_September.DefaultCellStyle = DataGridViewCellStyle10
        Saldo_September.HeaderText = "SEPTEMBER"
        Saldo_September.Name = "Saldo_September"
        Saldo_September.ReadOnly = True
        Saldo_September.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Oktober
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        DataGridViewCellStyle11.NullValue = Nothing
        Saldo_Oktober.DefaultCellStyle = DataGridViewCellStyle11
        Saldo_Oktober.HeaderText = "OKTOBER"
        Saldo_Oktober.Name = "Saldo_Oktober"
        Saldo_Oktober.ReadOnly = True
        Saldo_Oktober.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Nopember
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        DataGridViewCellStyle12.NullValue = Nothing
        Saldo_Nopember.DefaultCellStyle = DataGridViewCellStyle12
        Saldo_Nopember.HeaderText = "NOPEMBER"
        Saldo_Nopember.Name = "Saldo_Nopember"
        Saldo_Nopember.ReadOnly = True
        Saldo_Nopember.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Desember
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        DataGridViewCellStyle13.NullValue = Nothing
        Saldo_Desember.DefaultCellStyle = DataGridViewCellStyle13
        Saldo_Desember.HeaderText = "DESEMBER"
        Saldo_Desember.Name = "Saldo_Desember"
        Saldo_Desember.ReadOnly = True
        Saldo_Desember.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Saldo_Total
        ' 
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        DataGridViewCellStyle14.NullValue = Nothing
        Saldo_Total.DefaultCellStyle = DataGridViewCellStyle14
        Saldo_Total.HeaderText = "TOTAL"
        Saldo_Total.Name = "Saldo_Total"
        Saldo_Total.ReadOnly = True
        Saldo_Total.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 64)
        btn_Refresh.Margin = New Padding(4)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' lbl_Judul
        ' 
        lbl_Judul.AutoSize = True
        lbl_Judul.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_Judul.Location = New Point(11, 11)
        lbl_Judul.Margin = New Padding(4, 0, 4, 0)
        lbl_Judul.Name = "lbl_Judul"
        lbl_Judul.Size = New Size(273, 32)
        lbl_Judul.TabIndex = 10030
        lbl_Judul.Text = "Laporan Laba Rugi"
        lbl_Judul.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(1392, 14)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10072
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' btn_TrialBalance
        ' 
        btn_TrialBalance.Location = New Point(116, 64)
        btn_TrialBalance.Margin = New Padding(4)
        btn_TrialBalance.Name = "btn_TrialBalance"
        btn_TrialBalance.Size = New Size(109, 75)
        btn_TrialBalance.TabIndex = 10073
        btn_TrialBalance.Text = "Trial Balance"
        btn_TrialBalance.UseVisualStyleBackColor = True
        ' 
        ' frm_Laporan_LabaRugi
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1522, 786)
        Controls.Add(btn_TrialBalance)
        Controls.Add(btn_Export)
        Controls.Add(lbl_Judul)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4)
        Name = "frm_Laporan_LabaRugi"
        StartPosition = FormStartPosition.CenterParent
        Text = "Laporan Laba Rugi"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_Judul As System.Windows.Forms.Label
    Friend WithEvents btn_Export As System.Windows.Forms.Button
    Friend WithEvents Nama_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Januari As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Februari As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Maret As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_April As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Mei As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Juni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Juli As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Agustus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_September As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Oktober As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Nopember As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Desember As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_TrialBalance As System.Windows.Forms.Button
End Class
