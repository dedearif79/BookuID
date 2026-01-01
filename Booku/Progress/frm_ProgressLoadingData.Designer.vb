<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ProgressLoadingData
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
        Me.lbl_ProgressReport = New System.Windows.Forms.Label()
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
        'lbl_ProgressReport
        '
        Me.lbl_ProgressReport.AutoSize = True
        Me.lbl_ProgressReport.Location = New System.Drawing.Point(32, 127)
        Me.lbl_ProgressReport.Name = "lbl_ProgressReport"
        Me.lbl_ProgressReport.Size = New System.Drawing.Size(83, 13)
        Me.lbl_ProgressReport.TabIndex = 100000040
        Me.lbl_ProgressReport.Text = "Progress Report"
        '
        'frm_ProgressLoadingData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 166)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbl_ProgressReport)
        Me.Controls.Add(Me.lbl_Baris_01)
        Me.Controls.Add(Me.lbl_Baris_02)
        Me.Controls.Add(Me.pgb_Progress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ProgressLoadingData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Proses..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pgb_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents lbl_Baris_02 As System.Windows.Forms.Label
    Friend WithEvents lbl_Baris_01 As System.Windows.Forms.Label
    Friend WithEvents lbl_ProgressReport As System.Windows.Forms.Label
End Class
