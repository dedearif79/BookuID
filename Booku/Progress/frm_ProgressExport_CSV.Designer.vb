<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ProgressExport_CSV
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
        sfd_Simpan = New SaveFileDialog()
        Bgw_Export = New ComponentModel.BackgroundWorker()
        lbl_ProgressReport = New Label()
        btn_Tutup = New Button()
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
        ' Bgw_Export
        ' 
        Bgw_Export.WorkerReportsProgress = True
        Bgw_Export.WorkerSupportsCancellation = True
        ' 
        ' lbl_ProgressReport
        ' 
        lbl_ProgressReport.AutoSize = True
        lbl_ProgressReport.Location = New Point(37, 147)
        lbl_ProgressReport.Margin = New Padding(4, 0, 4, 0)
        lbl_ProgressReport.Name = "lbl_ProgressReport"
        lbl_ProgressReport.Size = New Size(90, 15)
        lbl_ProgressReport.TabIndex = 100000040
        lbl_ProgressReport.Text = "Progress Report"
        ' 
        ' btn_Tutup
        ' 
        btn_Tutup.Location = New Point(451, 155)
        btn_Tutup.Margin = New Padding(4, 3, 4, 3)
        btn_Tutup.Name = "btn_Tutup"
        btn_Tutup.Size = New Size(98, 42)
        btn_Tutup.TabIndex = 100000043
        btn_Tutup.Text = "Tutup"
        btn_Tutup.UseVisualStyleBackColor = True
        ' 
        ' frm_ProgressExport_CSV
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(590, 210)
        ControlBox = False
        Controls.Add(btn_Tutup)
        Controls.Add(lbl_ProgressReport)
        Controls.Add(lbl_Baris_01)
        Controls.Add(lbl_Baris_02)
        Controls.Add(pgb_Progress)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frm_ProgressExport_CSV"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Proses..."
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents pgb_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents lbl_Baris_02 As System.Windows.Forms.Label
    Friend WithEvents lbl_Baris_01 As System.Windows.Forms.Label
    Friend WithEvents sfd_Simpan As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Bgw_Export As System.ComponentModel.BackgroundWorker
    Friend WithEvents lbl_ProgressReport As System.Windows.Forms.Label
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
End Class
