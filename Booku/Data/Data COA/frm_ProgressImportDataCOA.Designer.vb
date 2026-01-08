<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ProgressImportDataCOA
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
        Me.pgb_Progress = New System.Windows.Forms.ProgressBar()
        Me.lbl_Baris_02 = New System.Windows.Forms.Label()
        Me.lbl_Baris_01 = New System.Windows.Forms.Label()
        Me.btn_Proses = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.bgw_Posting = New System.ComponentModel.BackgroundWorker()
        Me.lbl_ProgressReport = New System.Windows.Forms.Label()
        Me.ofd_Import = New System.Windows.Forms.OpenFileDialog()
        Me.bgw_ImportDataExcel = New System.ComponentModel.BackgroundWorker()
        Me.btn_TetapkanHasilPosting = New System.Windows.Forms.Button()
        Me.btn_BuangHasilPosting = New System.Windows.Forms.Button()
        Me.txt_LaporanHasilPostingJurnal = New System.Windows.Forms.RichTextBox()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pgb_Progress
        '
        Me.pgb_Progress.Location = New System.Drawing.Point(35, 87)
        Me.pgb_Progress.Name = "pgb_Progress"
        Me.pgb_Progress.Size = New System.Drawing.Size(436, 23)
        Me.pgb_Progress.TabIndex = 0
        '
        'lbl_Baris_02
        '
        Me.lbl_Baris_02.AutoSize = True
        Me.lbl_Baris_02.Location = New System.Drawing.Point(32, 52)
        Me.lbl_Baris_02.Name = "lbl_Baris_02"
        Me.lbl_Baris_02.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Baris_02.TabIndex = 1
        Me.lbl_Baris_02.Text = "Baris 02"
        '
        'lbl_Baris_01
        '
        Me.lbl_Baris_01.AutoSize = True
        Me.lbl_Baris_01.Location = New System.Drawing.Point(32, 25)
        Me.lbl_Baris_01.Name = "lbl_Baris_01"
        Me.lbl_Baris_01.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Baris_01.TabIndex = 2
        Me.lbl_Baris_01.Text = "Baris 01"
        '
        'btn_Proses
        '
        Me.btn_Proses.Location = New System.Drawing.Point(297, 133)
        Me.btn_Proses.Name = "btn_Proses"
        Me.btn_Proses.Size = New System.Drawing.Size(84, 36)
        Me.btn_Proses.TabIndex = 10
        Me.btn_Proses.Text = "Proses"
        Me.btn_Proses.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(387, 133)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 20
        Me.btn_Batal.Text = "Batalkan"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'bgw_Posting
        '
        Me.bgw_Posting.WorkerReportsProgress = True
        Me.bgw_Posting.WorkerSupportsCancellation = True
        '
        'lbl_ProgressReport
        '
        Me.lbl_ProgressReport.AutoSize = True
        Me.lbl_ProgressReport.Location = New System.Drawing.Point(32, 127)
        Me.lbl_ProgressReport.Name = "lbl_ProgressReport"
        Me.lbl_ProgressReport.Size = New System.Drawing.Size(83, 13)
        Me.lbl_ProgressReport.TabIndex = 100000039
        Me.lbl_ProgressReport.Text = "Progress Report"
        '
        'bgw_ImportDataExcel
        '
        Me.bgw_ImportDataExcel.WorkerReportsProgress = True
        Me.bgw_ImportDataExcel.WorkerSupportsCancellation = True
        '
        'btn_TetapkanHasilPosting
        '
        Me.btn_TetapkanHasilPosting.Location = New System.Drawing.Point(325, 237)
        Me.btn_TetapkanHasilPosting.Name = "btn_TetapkanHasilPosting"
        Me.btn_TetapkanHasilPosting.Size = New System.Drawing.Size(146, 36)
        Me.btn_TetapkanHasilPosting.TabIndex = 50
        Me.btn_TetapkanHasilPosting.Text = "Tetapkan Hasil Posting"
        Me.btn_TetapkanHasilPosting.UseVisualStyleBackColor = True
        '
        'btn_BuangHasilPosting
        '
        Me.btn_BuangHasilPosting.Location = New System.Drawing.Point(173, 237)
        Me.btn_BuangHasilPosting.Name = "btn_BuangHasilPosting"
        Me.btn_BuangHasilPosting.Size = New System.Drawing.Size(146, 36)
        Me.btn_BuangHasilPosting.TabIndex = 40
        Me.btn_BuangHasilPosting.Text = "Buang Hasil Posting"
        Me.btn_BuangHasilPosting.UseVisualStyleBackColor = True
        '
        'txt_LaporanHasilPostingJurnal
        '
        Me.txt_LaporanHasilPostingJurnal.Location = New System.Drawing.Point(35, 181)
        Me.txt_LaporanHasilPostingJurnal.Name = "txt_LaporanHasilPostingJurnal"
        Me.txt_LaporanHasilPostingJurnal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txt_LaporanHasilPostingJurnal.Size = New System.Drawing.Size(436, 134)
        Me.txt_LaporanHasilPostingJurnal.TabIndex = 30
        Me.txt_LaporanHasilPostingJurnal.Text = ""
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(198, 12)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(137, 34)
        Me.DataGridView.TabIndex = 100000040
        Me.DataGridView.Visible = False
        '
        'frm_ProgressImportDataCOA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 488)
        Me.ControlBox = False
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.txt_LaporanHasilPostingJurnal)
        Me.Controls.Add(Me.btn_BuangHasilPosting)
        Me.Controls.Add(Me.btn_TetapkanHasilPosting)
        Me.Controls.Add(Me.lbl_ProgressReport)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.btn_Proses)
        Me.Controls.Add(Me.lbl_Baris_01)
        Me.Controls.Add(Me.lbl_Baris_02)
        Me.Controls.Add(Me.pgb_Progress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ProgressImportDataCOA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Proses..."
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pgb_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents lbl_Baris_02 As System.Windows.Forms.Label
    Friend WithEvents lbl_Baris_01 As System.Windows.Forms.Label
    Friend WithEvents btn_Proses As System.Windows.Forms.Button
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents bgw_Posting As System.ComponentModel.BackgroundWorker
    Friend WithEvents lbl_ProgressReport As System.Windows.Forms.Label
    Friend WithEvents ofd_Import As System.Windows.Forms.OpenFileDialog
    Friend WithEvents bgw_ImportDataExcel As System.ComponentModel.BackgroundWorker
    Friend WithEvents btn_TetapkanHasilPosting As System.Windows.Forms.Button
    Friend WithEvents btn_BuangHasilPosting As System.Windows.Forms.Button
    Friend WithEvents txt_LaporanHasilPostingJurnal As System.Windows.Forms.RichTextBox
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
End Class
