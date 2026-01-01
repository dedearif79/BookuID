<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_JurnalVoucher_X
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
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Setujui = New System.Windows.Forms.Button()
        Me.Label = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Debet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Kredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_TanggalJurnal = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lbl_NomorFakturPajak = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lbl_StatusBalance = New System.Windows.Forms.Label()
        Me.lbl_NamaLawanTransaksi = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_NamaUserEntry = New System.Windows.Forms.Label()
        Me.lbl_NamaUserApprove = New System.Windows.Forms.Label()
        Me.lbl_Approve = New System.Windows.Forms.Label()
        Me.lbl_NomorJV = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_Referensi = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lbl_JenisJurnal = New System.Windows.Forms.Label()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.txt_TanggalInvoice = New System.Windows.Forms.TextBox()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(797, 606)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 1000
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(10, 10)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(145, 25)
        Me.lbl_JudulForm.TabIndex = 10024
        Me.lbl_JudulForm.Text = "Jurnal Voucher"
        '
        'btn_Setujui
        '
        Me.btn_Setujui.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Setujui.Location = New System.Drawing.Point(708, 606)
        Me.btn_Setujui.Name = "btn_Setujui"
        Me.btn_Setujui.Size = New System.Drawing.Size(84, 36)
        Me.btn_Setujui.TabIndex = 100000005
        Me.btn_Setujui.Text = "Setujui"
        Me.btn_Setujui.UseVisualStyleBackColor = True
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(12, 83)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(77, 13)
        Me.Label.TabIndex = 100000007
        Me.Label.Text = "Tanggal Jurnal"
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Kode_Akun, Me.Nama_Akun, Me.D_K, Me.Jumlah_Debet, Me.Jumlah_Kredit, Me.Keterangan_, Me.Nomor_ID})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 136)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(869, 392)
        Me.DataTabelUtama.TabIndex = 100000008
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Padding = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle15
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 42
        '
        'Kode_Akun
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kode_Akun.DefaultCellStyle = DataGridViewCellStyle16
        Me.Kode_Akun.HeaderText = "Kode Akun"
        Me.Kode_Akun.Name = "Kode_Akun"
        Me.Kode_Akun.ReadOnly = True
        Me.Kode_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Akun.Width = 54
        '
        'Nama_Akun
        '
        DataGridViewCellStyle17.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Akun.DefaultCellStyle = DataGridViewCellStyle17
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 270
        '
        'D_K
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.D_K.DefaultCellStyle = DataGridViewCellStyle18
        Me.D_K.HeaderText = "D/K"
        Me.D_K.Name = "D_K"
        Me.D_K.ReadOnly = True
        Me.D_K.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.D_K.Width = 45
        '
        'Jumlah_Debet
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "N0"
        DataGridViewCellStyle19.NullValue = Nothing
        DataGridViewCellStyle19.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Jumlah_Debet.DefaultCellStyle = DataGridViewCellStyle19
        Me.Jumlah_Debet.HeaderText = "Debet"
        Me.Jumlah_Debet.Name = "Jumlah_Debet"
        Me.Jumlah_Debet.ReadOnly = True
        Me.Jumlah_Debet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Debet.Width = 111
        '
        'Jumlah_Kredit
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle20.Format = "N0"
        DataGridViewCellStyle20.NullValue = Nothing
        DataGridViewCellStyle20.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Jumlah_Kredit.DefaultCellStyle = DataGridViewCellStyle20
        Me.Jumlah_Kredit.HeaderText = "Kredit"
        Me.Jumlah_Kredit.Name = "Jumlah_Kredit"
        Me.Jumlah_Kredit.ReadOnly = True
        Me.Jumlah_Kredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Kredit.Width = 111
        '
        'Keterangan_
        '
        DataGridViewCellStyle21.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Keterangan_.DefaultCellStyle = DataGridViewCellStyle21
        Me.Keterangan_.HeaderText = "Keterangan"
        Me.Keterangan_.Name = "Keterangan_"
        Me.Keterangan_.ReadOnly = True
        Me.Keterangan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_.Width = 231
        '
        'Nomor_ID
        '
        Me.Nomor_ID.HeaderText = "Nomor ID"
        Me.Nomor_ID.Name = "Nomor_ID"
        Me.Nomor_ID.ReadOnly = True
        Me.Nomor_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID.Visible = False
        '
        'lbl_TanggalJurnal
        '
        Me.lbl_TanggalJurnal.AutoSize = True
        Me.lbl_TanggalJurnal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalJurnal.Location = New System.Drawing.Point(101, 83)
        Me.lbl_TanggalJurnal.Name = "lbl_TanggalJurnal"
        Me.lbl_TanggalJurnal.Size = New System.Drawing.Size(77, 13)
        Me.lbl_TanggalJurnal.TabIndex = 100000009
        Me.lbl_TanggalJurnal.Text = "Tanggal Jurnal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(92, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 13)
        Me.Label3.TabIndex = 100000010
        Me.Label3.Text = ":"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(325, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 100000013
        Me.Label4.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(238, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 100000011
        Me.Label6.Text = "Tanggal Invoice"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(325, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 13)
        Me.Label7.TabIndex = 100000016
        Me.Label7.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(238, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 100000014
        Me.Label9.Text = "Nomor Invoice"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(635, 109)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(10, 13)
        Me.Label22.TabIndex = 100000022
        Me.Label22.Text = ":"
        '
        'lbl_NomorFakturPajak
        '
        Me.lbl_NomorFakturPajak.AutoSize = True
        Me.lbl_NomorFakturPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorFakturPajak.Location = New System.Drawing.Point(644, 83)
        Me.lbl_NomorFakturPajak.Name = "lbl_NomorFakturPajak"
        Me.lbl_NomorFakturPajak.Size = New System.Drawing.Size(101, 13)
        Me.lbl_NomorFakturPajak.TabIndex = 100000021
        Me.lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(537, 83)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(87, 13)
        Me.Label24.TabIndex = 100000020
        Me.Label24.Text = "No. Faktur Pajak"
        '
        'lbl_StatusBalance
        '
        Me.lbl_StatusBalance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_StatusBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StatusBalance.Location = New System.Drawing.Point(293, 544)
        Me.lbl_StatusBalance.Name = "lbl_StatusBalance"
        Me.lbl_StatusBalance.Size = New System.Drawing.Size(284, 25)
        Me.lbl_StatusBalance.TabIndex = 100000033
        Me.lbl_StatusBalance.Text = "Status Balance"
        Me.lbl_StatusBalance.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_NamaLawanTransaksi
        '
        Me.lbl_NamaLawanTransaksi.AutoSize = True
        Me.lbl_NamaLawanTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaLawanTransaksi.Location = New System.Drawing.Point(644, 109)
        Me.lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        Me.lbl_NamaLawanTransaksi.Size = New System.Drawing.Size(144, 13)
        Me.lbl_NamaLawanTransaksi.TabIndex = 100000035
        Me.lbl_NamaLawanTransaksi.Text = "NAMA LAWAN TRANSAKSI"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(537, 109)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 13)
        Me.Label27.TabIndex = 100000034
        Me.Label27.Text = "Lawan Transaksi"
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cetak.Location = New System.Drawing.Point(782, 12)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(100, 50)
        Me.btn_Cetak.TabIndex = 100000040
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 591)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 100000041
        Me.Label1.Text = "Data Entry :"
        '
        'lbl_NamaUserEntry
        '
        Me.lbl_NamaUserEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_NamaUserEntry.AutoSize = True
        Me.lbl_NamaUserEntry.Location = New System.Drawing.Point(12, 618)
        Me.lbl_NamaUserEntry.Name = "lbl_NamaUserEntry"
        Me.lbl_NamaUserEntry.Size = New System.Drawing.Size(87, 13)
        Me.lbl_NamaUserEntry.TabIndex = 100000042
        Me.lbl_NamaUserEntry.Text = "Nama User Entry"
        '
        'lbl_NamaUserApprove
        '
        Me.lbl_NamaUserApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_NamaUserApprove.AutoSize = True
        Me.lbl_NamaUserApprove.Location = New System.Drawing.Point(180, 618)
        Me.lbl_NamaUserApprove.Name = "lbl_NamaUserApprove"
        Me.lbl_NamaUserApprove.Size = New System.Drawing.Size(103, 13)
        Me.lbl_NamaUserApprove.TabIndex = 100000044
        Me.lbl_NamaUserApprove.Text = "Nama User Approve"
        '
        'lbl_Approve
        '
        Me.lbl_Approve.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_Approve.AutoSize = True
        Me.lbl_Approve.Location = New System.Drawing.Point(180, 591)
        Me.lbl_Approve.Name = "lbl_Approve"
        Me.lbl_Approve.Size = New System.Drawing.Size(53, 13)
        Me.lbl_Approve.TabIndex = 100000043
        Me.lbl_Approve.Text = "Approve :"
        '
        'lbl_NomorJV
        '
        Me.lbl_NomorJV.AutoSize = True
        Me.lbl_NomorJV.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorJV.Location = New System.Drawing.Point(12, 44)
        Me.lbl_NomorJV.Name = "lbl_NomorJV"
        Me.lbl_NomorJV.Size = New System.Drawing.Size(34, 17)
        Me.lbl_NomorJV.TabIndex = 100000045
        Me.lbl_NomorJV.Text = "No. "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(92, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 13)
        Me.Label2.TabIndex = 100000048
        Me.Label2.Text = ":"
        '
        'lbl_Referensi
        '
        Me.lbl_Referensi.AutoSize = True
        Me.lbl_Referensi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Referensi.Location = New System.Drawing.Point(101, 109)
        Me.lbl_Referensi.Name = "lbl_Referensi"
        Me.lbl_Referensi.Size = New System.Drawing.Size(52, 13)
        Me.lbl_Referensi.TabIndex = 100000047
        Me.lbl_Referensi.Text = "Referensi"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 109)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 100000046
        Me.Label11.Text = "Referensi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(635, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 13)
        Me.Label10.TabIndex = 100000019
        Me.Label10.Text = ":"
        '
        'lbl_JenisJurnal
        '
        Me.lbl_JenisJurnal.AutoSize = True
        Me.lbl_JenisJurnal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisJurnal.Location = New System.Drawing.Point(210, 44)
        Me.lbl_JenisJurnal.Name = "lbl_JenisJurnal"
        Me.lbl_JenisJurnal.Size = New System.Drawing.Size(92, 17)
        Me.lbl_JenisJurnal.TabIndex = 100000049
        Me.lbl_JenisJurnal.Text = "Jenis Jurnal :"
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Enabled = False
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(337, 106)
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(147, 20)
        Me.txt_NomorInvoice.TabIndex = 100000050
        '
        'txt_TanggalInvoice
        '
        Me.txt_TanggalInvoice.Enabled = False
        Me.txt_TanggalInvoice.Location = New System.Drawing.Point(337, 80)
        Me.txt_TanggalInvoice.Name = "txt_TanggalInvoice"
        Me.txt_TanggalInvoice.Size = New System.Drawing.Size(147, 20)
        Me.txt_TanggalInvoice.TabIndex = 100000051
        '
        'frm_JurnalVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(894, 654)
        Me.ControlBox = False
        Me.Controls.Add(Me.txt_TanggalInvoice)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.lbl_JenisJurnal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbl_Referensi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lbl_NomorJV)
        Me.Controls.Add(Me.lbl_NamaUserApprove)
        Me.Controls.Add(Me.lbl_Approve)
        Me.Controls.Add(Me.lbl_NamaUserEntry)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.lbl_NamaLawanTransaksi)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.lbl_StatusBalance)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lbl_NomorFakturPajak)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_TanggalJurnal)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.Label)
        Me.Controls.Add(Me.btn_Setujui)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.btn_Batal)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_JurnalVoucher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Jurnal Voucher"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_Setujui As System.Windows.Forms.Button
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_TanggalJurnal As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorFakturPajak As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lbl_StatusBalance As System.Windows.Forms.Label
    Friend WithEvents lbl_NamaLawanTransaksi As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btn_Cetak As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_NamaUserEntry As System.Windows.Forms.Label
    Friend WithEvents lbl_NamaUserApprove As System.Windows.Forms.Label
    Friend WithEvents lbl_Approve As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorJV As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_Referensi As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lbl_JenisJurnal As Label
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents D_K As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Debet As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Kredit As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents txt_NomorInvoice As TextBox
    Friend WithEvents txt_TanggalInvoice As TextBox
End Class
