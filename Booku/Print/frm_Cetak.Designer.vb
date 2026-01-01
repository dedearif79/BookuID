<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Cetak
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.web_TampilanCetak = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.btn_PrintPreview = New System.Windows.Forms.Button()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.lbl_Judul = New System.Windows.Forms.Label()
        CType(Me.web_TampilanCetak, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'web_TampilanCetak
        '
        Me.web_TampilanCetak.AllowExternalDrop = True
        Me.web_TampilanCetak.CreationProperties = Nothing
        Me.web_TampilanCetak.DefaultBackgroundColor = System.Drawing.Color.White
        Me.web_TampilanCetak.Location = New System.Drawing.Point(21, 64)
        Me.web_TampilanCetak.Name = "web_TampilanCetak"
        Me.web_TampilanCetak.Size = New System.Drawing.Size(737, 557)
        Me.web_TampilanCetak.TabIndex = 0
        Me.web_TampilanCetak.ZoomFactor = 1.0R
        '
        'btn_PrintPreview
        '
        Me.btn_PrintPreview.Location = New System.Drawing.Point(569, 12)
        Me.btn_PrintPreview.Name = "btn_PrintPreview"
        Me.btn_PrintPreview.Size = New System.Drawing.Size(92, 40)
        Me.btn_PrintPreview.TabIndex = 10035
        Me.btn_PrintPreview.Text = "Pratinjau"
        Me.btn_PrintPreview.UseVisualStyleBackColor = True
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Location = New System.Drawing.Point(667, 12)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(92, 40)
        Me.btn_Cetak.TabIndex = 10036
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        '
        'lbl_Judul
        '
        Me.lbl_Judul.AutoSize = True
        Me.lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Judul.Location = New System.Drawing.Point(18, 21)
        Me.lbl_Judul.Name = "lbl_Judul"
        Me.lbl_Judul.Size = New System.Drawing.Size(117, 20)
        Me.lbl_Judul.TabIndex = 10037
        Me.lbl_Judul.Text = "Pratinjau Cetak"
        '
        'frm_Cetak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 641)
        Me.Controls.Add(Me.lbl_Judul)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.btn_PrintPreview)
        Me.Controls.Add(Me.web_TampilanCetak)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Cetak"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pratinjau Cetak"
        CType(Me.web_TampilanCetak, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents web_TampilanCetak As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btn_PrintPreview As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents lbl_Judul As Label
End Class
