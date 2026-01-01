<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BackupDatabase
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
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.btn_Backup = New System.Windows.Forms.Button()
        Me.sfd_Simpan = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(323, 209)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9934
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'btn_Backup
        '
        Me.btn_Backup.Location = New System.Drawing.Point(412, 159)
        Me.btn_Backup.Name = "btn_Backup"
        Me.btn_Backup.Size = New System.Drawing.Size(134, 85)
        Me.btn_Backup.TabIndex = 9935
        Me.btn_Backup.Text = "Backup"
        Me.btn_Backup.UseVisualStyleBackColor = True
        '
        'frm_BackupDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 256)
        Me.Controls.Add(Me.btn_Backup)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_BackupDatabase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Database"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents btn_Backup As System.Windows.Forms.Button
    Friend WithEvents sfd_Simpan As System.Windows.Forms.SaveFileDialog
End Class
