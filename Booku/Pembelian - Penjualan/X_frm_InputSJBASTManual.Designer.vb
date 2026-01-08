<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_InputSJBASTManual
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
        btn_Tambahkan = New Button()
        btn_Batal = New Button()
        txt_NomorSJBAST = New TextBox()
        lbl_Nomor = New Label()
        dtp_TanggalDiterima = New DateTimePicker()
        lbl_TanggalDiterima = New Label()
        dtp_TanggalSJBAST = New DateTimePicker()
        lbl_Tanggal = New Label()
        SuspendLayout()
        ' 
        ' btn_Tambahkan
        ' 
        btn_Tambahkan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Tambahkan.Location = New Point(246, 205)
        btn_Tambahkan.Margin = New Padding(4, 3, 4, 3)
        btn_Tambahkan.Name = "btn_Tambahkan"
        btn_Tambahkan.Size = New Size(115, 40)
        btn_Tambahkan.TabIndex = 10143
        btn_Tambahkan.Text = "Tambahkan >>"
        btn_Tambahkan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(142, 205)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 10144
        btn_Batal.Text = "<< Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' txt_NomorSJBAST
        ' 
        txt_NomorSJBAST.Location = New Point(181, 52)
        txt_NomorSJBAST.Margin = New Padding(4, 3, 4, 3)
        txt_NomorSJBAST.Name = "txt_NomorSJBAST"
        txt_NomorSJBAST.Size = New Size(180, 23)
        txt_NomorSJBAST.TabIndex = 10134
        ' 
        ' lbl_Nomor
        ' 
        lbl_Nomor.AutoSize = True
        lbl_Nomor.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Nomor.Location = New Point(38, 55)
        lbl_Nomor.Margin = New Padding(4, 0, 4, 0)
        lbl_Nomor.Name = "lbl_Nomor"
        lbl_Nomor.Size = New Size(86, 13)
        lbl_Nomor.TabIndex = 10146
        lbl_Nomor.Text = "Nomor SJ/BAST"
        ' 
        ' dtp_TanggalDiterima
        ' 
        dtp_TanggalDiterima.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalDiterima.Enabled = False
        dtp_TanggalDiterima.Format = DateTimePickerFormat.Custom
        dtp_TanggalDiterima.Location = New Point(181, 112)
        dtp_TanggalDiterima.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalDiterima.Name = "dtp_TanggalDiterima"
        dtp_TanggalDiterima.Size = New Size(110, 23)
        dtp_TanggalDiterima.TabIndex = 10381
        ' 
        ' lbl_TanggalDiterima
        ' 
        lbl_TanggalDiterima.AutoSize = True
        lbl_TanggalDiterima.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_TanggalDiterima.Location = New Point(38, 118)
        lbl_TanggalDiterima.Margin = New Padding(4, 0, 4, 0)
        lbl_TanggalDiterima.Name = "lbl_TanggalDiterima"
        lbl_TanggalDiterima.Size = New Size(87, 13)
        lbl_TanggalDiterima.TabIndex = 10382
        lbl_TanggalDiterima.Text = "Tanggal Diterima"
        ' 
        ' dtp_TanggalSJBAST
        ' 
        dtp_TanggalSJBAST.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalSJBAST.Format = DateTimePickerFormat.Custom
        dtp_TanggalSJBAST.Location = New Point(181, 82)
        dtp_TanggalSJBAST.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalSJBAST.Name = "dtp_TanggalSJBAST"
        dtp_TanggalSJBAST.Size = New Size(110, 23)
        dtp_TanggalSJBAST.TabIndex = 10383
        ' 
        ' lbl_Tanggal
        ' 
        lbl_Tanggal.AutoSize = True
        lbl_Tanggal.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Tanggal.Location = New Point(38, 88)
        lbl_Tanggal.Margin = New Padding(4, 0, 4, 0)
        lbl_Tanggal.Name = "lbl_Tanggal"
        lbl_Tanggal.Size = New Size(94, 13)
        lbl_Tanggal.TabIndex = 10384
        lbl_Tanggal.Text = "Tanggal SJ/BAST"
        ' 
        ' frm_InputSJBASTManual
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(412, 283)
        Controls.Add(dtp_TanggalSJBAST)
        Controls.Add(lbl_Tanggal)
        Controls.Add(dtp_TanggalDiterima)
        Controls.Add(lbl_TanggalDiterima)
        Controls.Add(btn_Tambahkan)
        Controls.Add(btn_Batal)
        Controls.Add(txt_NomorSJBAST)
        Controls.Add(lbl_Nomor)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "X_frm_InputSJBASTManual"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Surat Jalan / BAST"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambahkan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_NomorSJBAST As TextBox
    Friend WithEvents lbl_Nomor As Label
    Friend WithEvents dtp_TanggalDiterima As DateTimePicker
    Friend WithEvents lbl_TanggalDiterima As Label
    Friend WithEvents dtp_TanggalSJBAST As DateTimePicker
    Friend WithEvents lbl_Tanggal As Label
End Class
