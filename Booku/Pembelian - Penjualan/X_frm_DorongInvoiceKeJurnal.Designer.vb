<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DorongInvoiceKeJurnal
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
        txt_NomorFakturPajak = New TextBox()
        lbl_SilakanIsiNomorFaktur = New Label()
        btn_DorongKeJurnal = New Button()
        btn_Batal = New Button()
        SuspendLayout()
        ' 
        ' txt_NomorFakturPajak
        ' 
        txt_NomorFakturPajak.Location = New Point(37, 70)
        txt_NomorFakturPajak.Margin = New Padding(4, 3, 4, 3)
        txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        txt_NomorFakturPajak.Size = New Size(241, 23)
        txt_NomorFakturPajak.TabIndex = 10
        ' 
        ' lbl_SilakanIsiNomorFaktur
        ' 
        lbl_SilakanIsiNomorFaktur.AutoSize = True
        lbl_SilakanIsiNomorFaktur.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_SilakanIsiNomorFaktur.Location = New Point(69, 30)
        lbl_SilakanIsiNomorFaktur.Margin = New Padding(4, 0, 4, 0)
        lbl_SilakanIsiNomorFaktur.Name = "lbl_SilakanIsiNomorFaktur"
        lbl_SilakanIsiNomorFaktur.Size = New Size(157, 13)
        lbl_SilakanIsiNomorFaktur.TabIndex = 10212
        lbl_SilakanIsiNomorFaktur.Text = "Silakan isi Nomor Faktur Pajak :"
        ' 
        ' btn_DorongKeJurnal
        ' 
        btn_DorongKeJurnal.Location = New Point(141, 123)
        btn_DorongKeJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_DorongKeJurnal.Name = "btn_DorongKeJurnal"
        btn_DorongKeJurnal.Size = New Size(138, 47)
        btn_DorongKeJurnal.TabIndex = 900
        btn_DorongKeJurnal.Text = "Dorong Ke Jurnal"
        btn_DorongKeJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Location = New Point(37, 123)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 47)
        btn_Batal.TabIndex = 999
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' frm_DorongInvoiceKeJurnal
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(316, 196)
        Controls.Add(btn_DorongKeJurnal)
        Controls.Add(btn_Batal)
        Controls.Add(txt_NomorFakturPajak)
        Controls.Add(lbl_SilakanIsiNomorFaktur)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_DorongInvoiceKeJurnal"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dorong Invoice Ke Jurnal"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents txt_NomorFakturPajak As TextBox
    Friend WithEvents lbl_SilakanIsiNomorFaktur As Label
    Friend WithEvents btn_DorongKeJurnal As Button
    Friend WithEvents btn_Batal As Button
End Class
