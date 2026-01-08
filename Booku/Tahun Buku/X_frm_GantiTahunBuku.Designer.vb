<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GantiTahunBuku
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
        btn_Ganti = New Button()
        Label1 = New Label()
        cmb_TahunBuku = New ComboBox()
        lbl_JudulForm = New Label()
        SuspendLayout()
        ' 
        ' btn_Ganti
        ' 
        btn_Ganti.Location = New Point(136, 158)
        btn_Ganti.Margin = New Padding(4, 3, 4, 3)
        btn_Ganti.Name = "btn_Ganti"
        btn_Ganti.Size = New Size(97, 40)
        btn_Ganti.TabIndex = 9933
        btn_Ganti.Text = "Ganti"
        btn_Ganti.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(102, 96)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(76, 15)
        Label1.TabIndex = 9935
        Label1.Text = "Tahun Buku :"
        ' 
        ' cmb_TahunBuku
        ' 
        cmb_TahunBuku.FormattingEnabled = True
        cmb_TahunBuku.Location = New Point(201, 92)
        cmb_TahunBuku.Margin = New Padding(4, 3, 4, 3)
        cmb_TahunBuku.Name = "cmb_TahunBuku"
        cmb_TahunBuku.Size = New Size(67, 23)
        cmb_TahunBuku.TabIndex = 9934
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Location = New Point(117, 35)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(124, 15)
        lbl_JudulForm.TabIndex = 9936
        lbl_JudulForm.Text = "GANTI TAHUN BUKU :"
        ' 
        ' frm_GantiTahunBuku
        ' 
        AcceptButton = btn_Ganti
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(379, 235)
        Controls.Add(lbl_JudulForm)
        Controls.Add(Label1)
        Controls.Add(cmb_TahunBuku)
        Controls.Add(btn_Ganti)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frm_GantiTahunBuku"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Ganti Tahun Buku"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Ganti As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_TahunBuku As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
End Class
