<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ProgressImportJurnal
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
        pgb_Progress = New ProgressBar()
        lbl_Baris_02 = New Label()
        lbl_Baris_01 = New Label()
        btn_Proses = New Button()
        btn_Batal = New Button()
        bgw_Posting = New ComponentModel.BackgroundWorker()
        lbl_ProgressReport = New Label()
        ofd_Import = New OpenFileDialog()
        bgw_ImportDataExcel = New ComponentModel.BackgroundWorker()
        btn_TetapkanHasilPosting = New Button()
        btn_BuangHasilPosting = New Button()
        txt_LaporanHasilPostingJurnal = New RichTextBox()
        DataGridView = New DataGridView()
        CType(DataGridView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pgb_Progress
        ' 
        pgb_Progress.Location = New Point(41, 100)
        pgb_Progress.Margin = New Padding(4, 3, 4, 3)
        pgb_Progress.Name = "pgb_Progress"
        pgb_Progress.Size = New Size(509, 27)
        pgb_Progress.TabIndex = 0
        ' 
        ' lbl_Baris_02
        ' 
        lbl_Baris_02.AutoSize = True
        lbl_Baris_02.Location = New Point(37, 60)
        lbl_Baris_02.Margin = New Padding(4, 0, 4, 0)
        lbl_Baris_02.Name = "lbl_Baris_02"
        lbl_Baris_02.Size = New Size(47, 15)
        lbl_Baris_02.TabIndex = 1
        lbl_Baris_02.Text = "Baris 02"
        ' 
        ' lbl_Baris_01
        ' 
        lbl_Baris_01.AutoSize = True
        lbl_Baris_01.Location = New Point(37, 29)
        lbl_Baris_01.Margin = New Padding(4, 0, 4, 0)
        lbl_Baris_01.Name = "lbl_Baris_01"
        lbl_Baris_01.Size = New Size(47, 15)
        lbl_Baris_01.TabIndex = 2
        lbl_Baris_01.Text = "Baris 01"
        ' 
        ' btn_Proses
        ' 
        btn_Proses.Location = New Point(346, 153)
        btn_Proses.Margin = New Padding(4, 3, 4, 3)
        btn_Proses.Name = "btn_Proses"
        btn_Proses.Size = New Size(98, 42)
        btn_Proses.TabIndex = 10
        btn_Proses.Text = "Proses"
        btn_Proses.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Location = New Point(451, 153)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(98, 42)
        btn_Batal.TabIndex = 20
        btn_Batal.Text = "Batalkan"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' bgw_Posting
        ' 
        bgw_Posting.WorkerReportsProgress = True
        bgw_Posting.WorkerSupportsCancellation = True
        ' 
        ' lbl_ProgressReport
        ' 
        lbl_ProgressReport.AutoSize = True
        lbl_ProgressReport.Location = New Point(37, 147)
        lbl_ProgressReport.Margin = New Padding(4, 0, 4, 0)
        lbl_ProgressReport.Name = "lbl_ProgressReport"
        lbl_ProgressReport.Size = New Size(90, 15)
        lbl_ProgressReport.TabIndex = 100000039
        lbl_ProgressReport.Text = "Progress Report"
        ' 
        ' bgw_ImportDataExcel
        ' 
        bgw_ImportDataExcel.WorkerReportsProgress = True
        bgw_ImportDataExcel.WorkerSupportsCancellation = True
        ' 
        ' btn_TetapkanHasilPosting
        ' 
        btn_TetapkanHasilPosting.Location = New Point(379, 273)
        btn_TetapkanHasilPosting.Margin = New Padding(4, 3, 4, 3)
        btn_TetapkanHasilPosting.Name = "btn_TetapkanHasilPosting"
        btn_TetapkanHasilPosting.Size = New Size(170, 42)
        btn_TetapkanHasilPosting.TabIndex = 50
        btn_TetapkanHasilPosting.Text = "Tetapkan Hasil Posting"
        btn_TetapkanHasilPosting.UseVisualStyleBackColor = True
        ' 
        ' btn_BuangHasilPosting
        ' 
        btn_BuangHasilPosting.Location = New Point(202, 273)
        btn_BuangHasilPosting.Margin = New Padding(4, 3, 4, 3)
        btn_BuangHasilPosting.Name = "btn_BuangHasilPosting"
        btn_BuangHasilPosting.Size = New Size(170, 42)
        btn_BuangHasilPosting.TabIndex = 40
        btn_BuangHasilPosting.Text = "Buang Hasil Posting"
        btn_BuangHasilPosting.UseVisualStyleBackColor = True
        ' 
        ' txt_LaporanHasilPostingJurnal
        ' 
        txt_LaporanHasilPostingJurnal.Location = New Point(41, 209)
        txt_LaporanHasilPostingJurnal.Margin = New Padding(4, 3, 4, 3)
        txt_LaporanHasilPostingJurnal.Name = "txt_LaporanHasilPostingJurnal"
        txt_LaporanHasilPostingJurnal.ScrollBars = RichTextBoxScrollBars.Vertical
        txt_LaporanHasilPostingJurnal.Size = New Size(508, 154)
        txt_LaporanHasilPostingJurnal.TabIndex = 30
        txt_LaporanHasilPostingJurnal.Text = ""
        ' 
        ' DataGridView
        ' 
        DataGridView.AllowUserToAddRows = False
        DataGridView.AllowUserToDeleteRows = False
        DataGridView.AllowUserToResizeRows = False
        DataGridView.BorderStyle = BorderStyle.Fixed3D
        DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView.Location = New Point(231, 14)
        DataGridView.Margin = New Padding(4, 3, 4, 3)
        DataGridView.MultiSelect = False
        DataGridView.Name = "DataGridView"
        DataGridView.ReadOnly = True
        DataGridView.RowHeadersVisible = False
        DataGridView.RowHeadersWidth = 33
        DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView.Size = New Size(160, 39)
        DataGridView.TabIndex = 100000040
        DataGridView.Visible = False
        ' 
        ' frm_ProgressImportJurnal
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(590, 563)
        ControlBox = False
        Controls.Add(DataGridView)
        Controls.Add(txt_LaporanHasilPostingJurnal)
        Controls.Add(btn_BuangHasilPosting)
        Controls.Add(btn_TetapkanHasilPosting)
        Controls.Add(lbl_ProgressReport)
        Controls.Add(btn_Batal)
        Controls.Add(btn_Proses)
        Controls.Add(lbl_Baris_01)
        Controls.Add(lbl_Baris_02)
        Controls.Add(pgb_Progress)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frm_ProgressImportJurnal"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Proses..."
        CType(DataGridView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
