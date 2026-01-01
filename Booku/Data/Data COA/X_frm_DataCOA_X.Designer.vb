<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_DataCOA_X
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
        DataTabelUtama = New DataGridView()
        lbl_Judul = New Label()
        lbl_CariAkun = New Label()
        txt_CariAkun = New TextBox()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_TautanCOA = New Button()
        btn_Refresh = New Button()
        Label1 = New Label()
        cmb_Visibilitas = New ComboBox()
        btn_ImportCOA = New Button()
        btn_Export = New Button()
        grb_Balancer = New GroupBox()
        Label4 = New Label()
        txt_SelisihNeraca = New TextBox()
        Label3 = New Label()
        txt_JumlahPassiva = New TextBox()
        Label2 = New Label()
        txt_JumlahAktiva = New TextBox()
        Label5 = New Label()
        txt_KepalaCOA = New TextBox()
        COA_ = New DataGridViewTextBoxColumn()
        Nama_Akun = New DataGridViewTextBoxColumn()
        D_K = New DataGridViewTextBoxColumn()
        Saldo_Awal = New DataGridViewTextBoxColumn()
        Uraian_ = New DataGridViewTextBoxColumn()
        Visibilitas_ = New DataGridViewTextBoxColumn()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Balancer.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {COA_, Nama_Akun, D_K, Saldo_Awal, Uraian_, Visibilitas_})
        DataTabelUtama.Location = New Point(14, 138)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1242, 534)
        DataTabelUtama.TabIndex = 10020
        ' 
        ' lbl_Judul
        ' 
        lbl_Judul.AutoSize = True
        lbl_Judul.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_Judul.Location = New Point(12, 12)
        lbl_Judul.Margin = New Padding(4, 0, 4, 0)
        lbl_Judul.Name = "lbl_Judul"
        lbl_Judul.Size = New Size(150, 32)
        lbl_Judul.TabIndex = 10031
        lbl_Judul.Text = "Data COA"
        lbl_Judul.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lbl_CariAkun
        ' 
        lbl_CariAkun.AutoSize = True
        lbl_CariAkun.Location = New Point(341, 88)
        lbl_CariAkun.Margin = New Padding(4, 0, 4, 0)
        lbl_CariAkun.Name = "lbl_CariAkun"
        lbl_CariAkun.Size = New Size(65, 15)
        lbl_CariAkun.TabIndex = 10033
        lbl_CariAkun.Text = "Cari Akun :"
        ' 
        ' txt_CariAkun
        ' 
        txt_CariAkun.Location = New Point(342, 108)
        txt_CariAkun.Margin = New Padding(4, 3, 4, 3)
        txt_CariAkun.Name = "txt_CariAkun"
        txt_CariAkun.Size = New Size(216, 23)
        txt_CariAkun.TabIndex = 10032
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Edit.Location = New Point(1056, 91)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10036
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(1160, 91)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10035
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(952, 91)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10034
        btn_Tambah.Text = "Tambah"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_TautanCOA
        ' 
        btn_TautanCOA.Location = New Point(115, 57)
        btn_TautanCOA.Margin = New Padding(4, 3, 4, 3)
        btn_TautanCOA.Name = "btn_TautanCOA"
        btn_TautanCOA.Size = New Size(127, 75)
        btn_TautanCOA.TabIndex = 10037
        btn_TautanCOA.Text = "Tautan COA"
        btn_TautanCOA.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 57)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10038
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(568, 88)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 15)
        Label1.TabIndex = 10039
        Label1.Text = "Visibilitas :"
        ' 
        ' cmb_Visibilitas
        ' 
        cmb_Visibilitas.FormattingEnabled = True
        cmb_Visibilitas.Location = New Point(566, 108)
        cmb_Visibilitas.Margin = New Padding(4, 3, 4, 3)
        cmb_Visibilitas.Name = "cmb_Visibilitas"
        cmb_Visibilitas.Size = New Size(80, 23)
        cmb_Visibilitas.TabIndex = 10040
        ' 
        ' btn_ImportCOA
        ' 
        btn_ImportCOA.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_ImportCOA.Location = New Point(1020, 14)
        btn_ImportCOA.Margin = New Padding(4, 3, 4, 3)
        btn_ImportCOA.Name = "btn_ImportCOA"
        btn_ImportCOA.Size = New Size(115, 52)
        btn_ImportCOA.TabIndex = 10074
        btn_ImportCOA.Text = "Import COA"
        btn_ImportCOA.UseVisualStyleBackColor = True
        btn_ImportCOA.Visible = False
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(1142, 14)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 52)
        btn_Export.TabIndex = 10075
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' grb_Balancer
        ' 
        grb_Balancer.Controls.Add(Label4)
        grb_Balancer.Controls.Add(txt_SelisihNeraca)
        grb_Balancer.Controls.Add(Label3)
        grb_Balancer.Controls.Add(txt_JumlahPassiva)
        grb_Balancer.Controls.Add(Label2)
        grb_Balancer.Controls.Add(txt_JumlahAktiva)
        grb_Balancer.Location = New Point(667, 17)
        grb_Balancer.Margin = New Padding(4, 3, 4, 3)
        grb_Balancer.Name = "grb_Balancer"
        grb_Balancer.Padding = New Padding(4, 3, 4, 3)
        grb_Balancer.Size = New Size(236, 114)
        grb_Balancer.TabIndex = 10076
        grb_Balancer.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(18, 81)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(40, 15)
        Label4.TabIndex = 10082
        Label4.Text = "Selisih"
        ' 
        ' txt_SelisihNeraca
        ' 
        txt_SelisihNeraca.Location = New Point(83, 77)
        txt_SelisihNeraca.Margin = New Padding(4, 3, 4, 3)
        txt_SelisihNeraca.Name = "txt_SelisihNeraca"
        txt_SelisihNeraca.Size = New Size(142, 23)
        txt_SelisihNeraca.TabIndex = 10081
        txt_SelisihNeraca.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(18, 51)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(45, 15)
        Label3.TabIndex = 10080
        Label3.Text = "Passiva"
        ' 
        ' txt_JumlahPassiva
        ' 
        txt_JumlahPassiva.Location = New Point(83, 47)
        txt_JumlahPassiva.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahPassiva.Name = "txt_JumlahPassiva"
        txt_JumlahPassiva.Size = New Size(142, 23)
        txt_JumlahPassiva.TabIndex = 10079
        txt_JumlahPassiva.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(18, 21)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(40, 15)
        Label2.TabIndex = 10078
        Label2.Text = "Aktiva"
        ' 
        ' txt_JumlahAktiva
        ' 
        txt_JumlahAktiva.Location = New Point(83, 17)
        txt_JumlahAktiva.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahAktiva.Name = "txt_JumlahAktiva"
        txt_JumlahAktiva.Size = New Size(142, 23)
        txt_JumlahAktiva.TabIndex = 10077
        txt_JumlahAktiva.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(248, 88)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(79, 15)
        Label5.TabIndex = 10078
        Label5.Text = "Kepala Akun :"
        ' 
        ' txt_KepalaCOA
        ' 
        txt_KepalaCOA.Location = New Point(250, 108)
        txt_KepalaCOA.Margin = New Padding(4, 3, 4, 3)
        txt_KepalaCOA.MaxLength = 5
        txt_KepalaCOA.Name = "txt_KepalaCOA"
        txt_KepalaCOA.Size = New Size(84, 23)
        txt_KepalaCOA.TabIndex = 10077
        ' 
        ' COA_
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        COA_.DefaultCellStyle = DataGridViewCellStyle1
        COA_.HeaderText = "COA"
        COA_.Name = "COA_"
        COA_.ReadOnly = True
        COA_.SortMode = DataGridViewColumnSortMode.NotSortable
        COA_.Width = 63
        ' 
        ' Nama_Akun
        ' 
        DataGridViewCellStyle2.Padding = New Padding(3, 0, 3, 0)
        Nama_Akun.DefaultCellStyle = DataGridViewCellStyle2
        Nama_Akun.HeaderText = "Nama Akun"
        Nama_Akun.Name = "Nama_Akun"
        Nama_Akun.ReadOnly = True
        Nama_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Akun.Width = 330
        ' 
        ' D_K
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        D_K.DefaultCellStyle = DataGridViewCellStyle3
        D_K.HeaderText = "D/K"
        D_K.Name = "D_K"
        D_K.ReadOnly = True
        D_K.SortMode = DataGridViewColumnSortMode.NotSortable
        D_K.Width = 69
        ' 
        ' Saldo_Awal
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Saldo_Awal.DefaultCellStyle = DataGridViewCellStyle4
        Saldo_Awal.HeaderText = "Saldo Awal"
        Saldo_Awal.Name = "Saldo_Awal"
        Saldo_Awal.ReadOnly = True
        Saldo_Awal.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo_Awal.Width = 120
        ' 
        ' Uraian_
        ' 
        DataGridViewCellStyle5.Padding = New Padding(3, 0, 3, 0)
        Uraian_.DefaultCellStyle = DataGridViewCellStyle5
        Uraian_.HeaderText = "Uraian"
        Uraian_.Name = "Uraian_"
        Uraian_.ReadOnly = True
        Uraian_.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_.Width = 330
        ' 
        ' Visibilitas_
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        Visibilitas_.DefaultCellStyle = DataGridViewCellStyle6
        Visibilitas_.HeaderText = "Visibilitas"
        Visibilitas_.Name = "Visibilitas_"
        Visibilitas_.ReadOnly = True
        Visibilitas_.SortMode = DataGridViewColumnSortMode.NotSortable
        Visibilitas_.Width = 72
        ' 
        ' frm_DataCOA
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1273, 690)
        Controls.Add(Label5)
        Controls.Add(txt_KepalaCOA)
        Controls.Add(grb_Balancer)
        Controls.Add(btn_Export)
        Controls.Add(btn_ImportCOA)
        Controls.Add(cmb_Visibilitas)
        Controls.Add(Label1)
        Controls.Add(btn_Refresh)
        Controls.Add(btn_TautanCOA)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(lbl_CariAkun)
        Controls.Add(txt_CariAkun)
        Controls.Add(lbl_Judul)
        Controls.Add(DataTabelUtama)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_DataCOA"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Data COA"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Balancer.ResumeLayout(False)
        grb_Balancer.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_Judul As System.Windows.Forms.Label
    Friend WithEvents lbl_CariAkun As System.Windows.Forms.Label
    Friend WithEvents txt_CariAkun As System.Windows.Forms.TextBox
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_TautanCOA As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_Visibilitas As System.Windows.Forms.ComboBox
    Friend WithEvents btn_ImportCOA As System.Windows.Forms.Button
    Friend WithEvents btn_Export As System.Windows.Forms.Button
    Friend WithEvents grb_Balancer As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPassiva As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahAktiva As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_SelisihNeraca As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_KepalaCOA As TextBox
    Friend WithEvents COA_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents D_K As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_ As DataGridViewTextBoxColumn
    Friend WithEvents Visibilitas_ As DataGridViewTextBoxColumn
End Class
