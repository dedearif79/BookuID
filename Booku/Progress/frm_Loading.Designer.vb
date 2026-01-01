<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Loading
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
        lbl_Baris_01 = New Label()
        SuspendLayout()
        ' 
        ' lbl_Baris_01
        ' 
        lbl_Baris_01.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbl_Baris_01.AutoSize = True
        lbl_Baris_01.Location = New Point(387, 232)
        lbl_Baris_01.Margin = New Padding(4, 0, 4, 0)
        lbl_Baris_01.Name = "lbl_Baris_01"
        lbl_Baris_01.Size = New Size(83, 15)
        lbl_Baris_01.TabIndex = 3
        lbl_Baris_01.Text = "Sedang Proses"
        ' 
        ' frm_Loading
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        BackColor = SystemColors.ButtonFace
        ClientSize = New Size(922, 510)
        Controls.Add(lbl_Baris_01)
        FormBorderStyle = FormBorderStyle.None
        Name = "frm_Loading"
        StartPosition = FormStartPosition.CenterParent
        WindowState = FormWindowState.Maximized
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lbl_Baris_01 As Label
End Class
