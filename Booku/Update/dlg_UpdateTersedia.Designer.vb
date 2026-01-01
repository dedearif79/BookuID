<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlg_UpdateTersedia
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
        Me.lbl_PesanBaris1 = New System.Windows.Forms.Label()
        Me.lbl_PesanBaris2 = New System.Windows.Forms.Label()
        Me.btn_UpdateSekarang = New System.Windows.Forms.Button()
        Me.btn_UpdateNanti = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lbl_PesanBaris1
        '
        Me.lbl_PesanBaris1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PesanBaris1.Location = New System.Drawing.Point(49, 41)
        Me.lbl_PesanBaris1.Name = "lbl_PesanBaris1"
        Me.lbl_PesanBaris1.Size = New System.Drawing.Size(273, 22)
        Me.lbl_PesanBaris1.TabIndex = 1
        Me.lbl_PesanBaris1.Text = "Tersedia update terbaru"
        Me.lbl_PesanBaris1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_PesanBaris2
        '
        Me.lbl_PesanBaris2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PesanBaris2.Location = New System.Drawing.Point(49, 74)
        Me.lbl_PesanBaris2.Name = "lbl_PesanBaris2"
        Me.lbl_PesanBaris2.Size = New System.Drawing.Size(273, 22)
        Me.lbl_PesanBaris2.TabIndex = 2
        Me.lbl_PesanBaris2.Text = "untuk Aplikasi Ini."
        Me.lbl_PesanBaris2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_UpdateSekarang
        '
        Me.btn_UpdateSekarang.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btn_UpdateSekarang.Location = New System.Drawing.Point(55, 131)
        Me.btn_UpdateSekarang.Name = "btn_UpdateSekarang"
        Me.btn_UpdateSekarang.Size = New System.Drawing.Size(124, 41)
        Me.btn_UpdateSekarang.TabIndex = 352
        Me.btn_UpdateSekarang.Text = "Update Sekarang"
        Me.btn_UpdateSekarang.UseVisualStyleBackColor = True
        '
        'btn_UpdateNanti
        '
        Me.btn_UpdateNanti.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_UpdateNanti.Location = New System.Drawing.Point(185, 131)
        Me.btn_UpdateNanti.Name = "btn_UpdateNanti"
        Me.btn_UpdateNanti.Size = New System.Drawing.Size(124, 41)
        Me.btn_UpdateNanti.TabIndex = 353
        Me.btn_UpdateNanti.Text = "Update Nanti"
        Me.btn_UpdateNanti.UseVisualStyleBackColor = True
        '
        'frm_UpdateTersedia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 208)
        Me.Controls.Add(Me.btn_UpdateNanti)
        Me.Controls.Add(Me.btn_UpdateSekarang)
        Me.Controls.Add(Me.lbl_PesanBaris2)
        Me.Controls.Add(Me.lbl_PesanBaris1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_UpdateTersedia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update Aplikasi"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbl_PesanBaris1 As System.Windows.Forms.Label
    Friend WithEvents lbl_PesanBaris2 As System.Windows.Forms.Label
    Friend WithEvents btn_UpdateSekarang As System.Windows.Forms.Button
    Friend WithEvents btn_UpdateNanti As System.Windows.Forms.Button
End Class
