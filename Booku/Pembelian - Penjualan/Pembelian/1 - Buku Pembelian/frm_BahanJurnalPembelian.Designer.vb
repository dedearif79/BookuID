<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BahanJurnalPembelian
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_StatusBalance = New System.Windows.Forms.Label()
        Me.btn_Kirim = New System.Windows.Forms.Button()
        Me.Label = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_TanggalInvoice = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbl_NomorInvoice = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbl_TanggalFakturPajak = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lbl_NomorFakturPajak = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lbl_PPNDikreditkan = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lbl_PPhTerutang = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lbl_NamaLawanTransaksi = New System.Windows.Forms.Label()
        Me.lbl_AlamatLawanTransaksi = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbl_NomorPEMB = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtp_TanggalJurnal = New System.Windows.Forms.DateTimePicker()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(510, 613)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 1000
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 25)
        Me.Label2.TabIndex = 10024
        Me.Label2.Text = "Bahan Jurnal"
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(600, 613)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(84, 36)
        Me.btn_Edit.TabIndex = 100000005
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.COA, Me.Nama_Akun, Me.D_K, Me.Debet, Me.Kredit, Me.Keterangan, Me.Nomor_ID})
        Me.DataGridView.Location = New System.Drawing.Point(12, 163)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(780, 400)
        Me.DataGridView.TabIndex = 100000008
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 42
        '
        'COA
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.COA.DefaultCellStyle = DataGridViewCellStyle2
        Me.COA.HeaderText = "Kode Akun"
        Me.COA.Name = "COA"
        Me.COA.ReadOnly = True
        Me.COA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.COA.Width = 54
        '
        'Nama_Akun
        '
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Akun.DefaultCellStyle = DataGridViewCellStyle3
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 180
        '
        'D_K
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.D_K.DefaultCellStyle = DataGridViewCellStyle4
        Me.D_K.HeaderText = "D/K"
        Me.D_K.Name = "D_K"
        Me.D_K.ReadOnly = True
        Me.D_K.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.D_K.Width = 45
        '
        'Debet
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Debet.DefaultCellStyle = DataGridViewCellStyle5
        Me.Debet.HeaderText = "Debet"
        Me.Debet.Name = "Debet"
        Me.Debet.ReadOnly = True
        Me.Debet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Debet.Width = 111
        '
        'Kredit
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kredit.DefaultCellStyle = DataGridViewCellStyle6
        Me.Kredit.HeaderText = "Kredit"
        Me.Kredit.Name = "Kredit"
        Me.Kredit.ReadOnly = True
        Me.Kredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kredit.Width = 111
        '
        'Keterangan
        '
        DataGridViewCellStyle7.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Keterangan.DefaultCellStyle = DataGridViewCellStyle7
        Me.Keterangan.HeaderText = "Keterangan"
        Me.Keterangan.Name = "Keterangan"
        Me.Keterangan.ReadOnly = True
        Me.Keterangan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan.Width = 231
        '
        'Nomor_ID
        '
        Me.Nomor_ID.HeaderText = "Nomor ID"
        Me.Nomor_ID.Name = "Nomor_ID"
        Me.Nomor_ID.ReadOnly = True
        Me.Nomor_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID.Visible = False
        '
        'lbl_StatusBalance
        '
        Me.lbl_StatusBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StatusBalance.Location = New System.Drawing.Point(293, 578)
        Me.lbl_StatusBalance.Name = "lbl_StatusBalance"
        Me.lbl_StatusBalance.Size = New System.Drawing.Size(193, 25)
        Me.lbl_StatusBalance.TabIndex = 100000033
        Me.lbl_StatusBalance.Text = "Status Balance"
        Me.lbl_StatusBalance.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_Kirim
        '
        Me.btn_Kirim.Location = New System.Drawing.Point(690, 613)
        Me.btn_Kirim.Name = "btn_Kirim"
        Me.btn_Kirim.Size = New System.Drawing.Size(100, 36)
        Me.btn_Kirim.TabIndex = 100000040
        Me.btn_Kirim.Text = "Kirim ke Jurnal"
        Me.btn_Kirim.UseVisualStyleBackColor = True
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(9, 80)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(77, 13)
        Me.Label.TabIndex = 100000007
        Me.Label.Text = "Tanggal Jurnal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(96, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 13)
        Me.Label3.TabIndex = 100000010
        Me.Label3.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 100000011
        Me.Label6.Text = "Tanggal Invoice"
        '
        'lbl_TanggalInvoice
        '
        Me.lbl_TanggalInvoice.AutoSize = True
        Me.lbl_TanggalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalInvoice.Location = New System.Drawing.Point(105, 102)
        Me.lbl_TanggalInvoice.Name = "lbl_TanggalInvoice"
        Me.lbl_TanggalInvoice.Size = New System.Drawing.Size(84, 13)
        Me.lbl_TanggalInvoice.TabIndex = 100000012
        Me.lbl_TanggalInvoice.Text = "Tanggal Invoice"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(96, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 100000013
        Me.Label4.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(9, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 100000014
        Me.Label9.Text = "Nomor Invoice"
        '
        'lbl_NomorInvoice
        '
        Me.lbl_NomorInvoice.AutoSize = True
        Me.lbl_NomorInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorInvoice.Location = New System.Drawing.Point(105, 124)
        Me.lbl_NomorInvoice.Name = "lbl_NomorInvoice"
        Me.lbl_NomorInvoice.Size = New System.Drawing.Size(76, 13)
        Me.lbl_NomorInvoice.TabIndex = 100000015
        Me.lbl_NomorInvoice.Text = "Nomor Invoice"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(96, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 13)
        Me.Label7.TabIndex = 100000016
        Me.Label7.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(245, 58)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 13)
        Me.Label12.TabIndex = 100000017
        Me.Label12.Text = "Tgl. Faktur Pajak"
        '
        'lbl_TanggalFakturPajak
        '
        Me.lbl_TanggalFakturPajak.AutoSize = True
        Me.lbl_TanggalFakturPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalFakturPajak.Location = New System.Drawing.Point(363, 58)
        Me.lbl_TanggalFakturPajak.Name = "lbl_TanggalFakturPajak"
        Me.lbl_TanggalFakturPajak.Size = New System.Drawing.Size(88, 13)
        Me.lbl_TanggalFakturPajak.TabIndex = 100000018
        Me.lbl_TanggalFakturPajak.Text = "Tgl. Faktur Pajak"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(354, 58)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 13)
        Me.Label10.TabIndex = 100000019
        Me.Label10.Text = ":"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(245, 80)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(108, 13)
        Me.Label24.TabIndex = 100000020
        Me.Label24.Text = "No. Seri Faktur Pajak"
        '
        'lbl_NomorFakturPajak
        '
        Me.lbl_NomorFakturPajak.AutoSize = True
        Me.lbl_NomorFakturPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorFakturPajak.Location = New System.Drawing.Point(363, 80)
        Me.lbl_NomorFakturPajak.Name = "lbl_NomorFakturPajak"
        Me.lbl_NomorFakturPajak.Size = New System.Drawing.Size(122, 13)
        Me.lbl_NomorFakturPajak.TabIndex = 100000021
        Me.lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(354, 80)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(10, 13)
        Me.Label22.TabIndex = 100000022
        Me.Label22.Text = ":"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(245, 102)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(86, 13)
        Me.Label21.TabIndex = 100000023
        Me.Label21.Text = "PPN Dikreditkan"
        '
        'lbl_PPNDikreditkan
        '
        Me.lbl_PPNDikreditkan.AutoSize = True
        Me.lbl_PPNDikreditkan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPNDikreditkan.Location = New System.Drawing.Point(363, 102)
        Me.lbl_PPNDikreditkan.Name = "lbl_PPNDikreditkan"
        Me.lbl_PPNDikreditkan.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PPNDikreditkan.TabIndex = 100000024
        Me.lbl_PPNDikreditkan.Text = "PPN Dikreditkan"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(354, 102)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(10, 13)
        Me.Label19.TabIndex = 100000025
        Me.Label19.Text = ":"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(245, 124)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 13)
        Me.Label18.TabIndex = 100000026
        Me.Label18.Text = "PPh Terutang"
        '
        'lbl_PPhTerutang
        '
        Me.lbl_PPhTerutang.AutoSize = True
        Me.lbl_PPhTerutang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPhTerutang.Location = New System.Drawing.Point(363, 124)
        Me.lbl_PPhTerutang.Name = "lbl_PPhTerutang"
        Me.lbl_PPhTerutang.Size = New System.Drawing.Size(73, 13)
        Me.lbl_PPhTerutang.TabIndex = 100000027
        Me.lbl_PPhTerutang.Text = "PPh Terutang"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(354, 124)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(10, 13)
        Me.Label16.TabIndex = 100000028
        Me.Label16.Text = ":"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(500, 58)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(94, 13)
        Me.Label27.TabIndex = 100000034
        Me.Label27.Text = "Lawan Transaksi :"
        '
        'lbl_NamaLawanTransaksi
        '
        Me.lbl_NamaLawanTransaksi.AutoSize = True
        Me.lbl_NamaLawanTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaLawanTransaksi.Location = New System.Drawing.Point(500, 80)
        Me.lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        Me.lbl_NamaLawanTransaksi.Size = New System.Drawing.Size(144, 13)
        Me.lbl_NamaLawanTransaksi.TabIndex = 100000035
        Me.lbl_NamaLawanTransaksi.Text = "NAMA LAWAN TRANSAKSI"
        '
        'lbl_AlamatLawanTransaksi
        '
        Me.lbl_AlamatLawanTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_AlamatLawanTransaksi.Location = New System.Drawing.Point(500, 102)
        Me.lbl_AlamatLawanTransaksi.Name = "lbl_AlamatLawanTransaksi"
        Me.lbl_AlamatLawanTransaksi.Size = New System.Drawing.Size(290, 47)
        Me.lbl_AlamatLawanTransaksi.TabIndex = 100000036
        Me.lbl_AlamatLawanTransaksi.Text = "Alamat Lawan Transaksi"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 58)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 13)
        Me.Label15.TabIndex = 100000037
        Me.Label15.Text = "Nomor PEMB"
        '
        'lbl_NomorPEMB
        '
        Me.lbl_NomorPEMB.AutoSize = True
        Me.lbl_NomorPEMB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorPEMB.Location = New System.Drawing.Point(105, 58)
        Me.lbl_NomorPEMB.Name = "lbl_NomorPEMB"
        Me.lbl_NomorPEMB.Size = New System.Drawing.Size(81, 13)
        Me.lbl_NomorPEMB.TabIndex = 100000038
        Me.lbl_NomorPEMB.Text = "Nomor PEMB"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(96, 58)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(10, 13)
        Me.Label13.TabIndex = 100000039
        Me.Label13.Text = ":"
        '
        'dtp_TanggalJurnal
        '
        Me.dtp_TanggalJurnal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalJurnal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalJurnal.Location = New System.Drawing.Point(108, 77)
        Me.dtp_TanggalJurnal.Name = "dtp_TanggalJurnal"
        Me.dtp_TanggalJurnal.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalJurnal.TabIndex = 100000041
        '
        'frm_BahanJurnal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(804, 661)
        Me.ControlBox = False
        Me.Controls.Add(Me.dtp_TanggalJurnal)
        Me.Controls.Add(Me.btn_Kirim)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lbl_NomorPEMB)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lbl_AlamatLawanTransaksi)
        Me.Controls.Add(Me.lbl_NamaLawanTransaksi)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.lbl_StatusBalance)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lbl_PPhTerutang)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lbl_PPNDikreditkan)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lbl_NomorFakturPajak)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lbl_TanggalFakturPajak)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lbl_NomorInvoice)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbl_TanggalInvoice)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.Label)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_BahanJurnal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bahan Jurnal"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_StatusBalance As System.Windows.Forms.Label
    Friend WithEvents btn_Kirim As System.Windows.Forms.Button
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbl_TanggalInvoice As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorInvoice As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbl_TanggalFakturPajak As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorFakturPajak As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lbl_PPNDikreditkan As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lbl_PPhTerutang As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lbl_NamaLawanTransaksi As System.Windows.Forms.Label
    Friend WithEvents lbl_AlamatLawanTransaksi As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorPEMB As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalJurnal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_K As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Debet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Keterangan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
