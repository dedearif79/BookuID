<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PilihJurnal_Penjualan_Retur
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
        Me.btn_JurnalPenjualan = New System.Windows.Forms.Button()
        Me.btn_JurnalRetur = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_JurnalPenjualan
        '
        Me.btn_JurnalPenjualan.Location = New System.Drawing.Point(25, 26)
        Me.btn_JurnalPenjualan.Name = "btn_JurnalPenjualan"
        Me.btn_JurnalPenjualan.Size = New System.Drawing.Size(116, 56)
        Me.btn_JurnalPenjualan.TabIndex = 9934
        Me.btn_JurnalPenjualan.Text = "Jurnal Penjualan"
        Me.btn_JurnalPenjualan.UseVisualStyleBackColor = True
        '
        'btn_JurnalRetur
        '
        Me.btn_JurnalRetur.Location = New System.Drawing.Point(147, 26)
        Me.btn_JurnalRetur.Name = "btn_JurnalRetur"
        Me.btn_JurnalRetur.Size = New System.Drawing.Size(116, 56)
        Me.btn_JurnalRetur.TabIndex = 9935
        Me.btn_JurnalRetur.Text = "Jurnal Retur"
        Me.btn_JurnalRetur.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(102, 98)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 49)
        Me.btn_Tutup.TabIndex = 9936
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'frm_PilihJurnal_Penjualan_Retur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 159)
        Me.Controls.Add(Me.btn_Tutup)
        Me.Controls.Add(Me.btn_JurnalRetur)
        Me.Controls.Add(Me.btn_JurnalPenjualan)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_PilihJurnal_Penjualan_Retur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pilih Jurnal"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_JurnalPenjualan As Button
    Friend WithEvents btn_JurnalRetur As Button
    Friend WithEvents btn_Tutup As Button
End Class
