Imports bcomm
Imports System.Data.Odbc

Module mdl_IsiTabelCOA

    Public Sub IsiTabelCOA()

        Dim QITC As String = Kosongan '(QITC = Query Isi Tabel COA)

        QITC = " INSERT INTO tbl_COA ( COA, Nama_Akun, Kode_Mata_Uang, D_K, Visibilitas ) VALUES "

        '-------------------------------------
        ' A K T I V A
        '-------------------------------------

        'AKTIVA LANCAR - Petty Cash 
        QITC &= "( '11101', 'Petty Cash - Administrasi',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11102', 'Petty Cash - Produksi',                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11103', 'Petty Cash 3',                                             'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - Kas :
        QITC &= "( '11201', 'Kas',                                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11202', 'Kas 2',                                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11203', 'Kas 3',                                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11204', 'Kas 4',                                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11205', 'Kas 5',                                                    'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - Kas MUA :
        QITC &= "( '11211', 'Kas (USD)',                                                'USD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11212', 'Kas (AUD)',                                                'AUD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11213', 'Kas (JPY)',                                                'JPY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11214', 'Kas (CNY)',                                                'CNY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11215', 'Kas (EUR)',                                                'EUR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11216', 'Kas (SGD)',                                                'SGD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11217', 'Kas (GBP)',                                                'GBP',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - Kas Outlet :
        QITC &= "( '11221', 'Kas Outlet-1',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11222', 'Kas Outlet-2',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11223', 'Kas Outlet-3',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11224', 'Kas Outlet-4',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11225', 'Kas Outlet-5',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11226', 'Kas Outlet-6',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11227', 'Kas Outlet-7',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11228', 'Kas Outlet-8',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11229', 'Kas Outlet-9',                                             'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - Bank :
        QITC &= "( '11301', 'Bank 1',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11302', 'Bank 2',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11303', 'Bank 3',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11304', 'Bank 4',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11305', 'Bank 5',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11306', 'Bank 6',                                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11307', 'Bank 7',                                                   'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - Bank Eceran :
        QITC &= "( '11311', 'Bank Eceran 1',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11312', 'Bank Eceran 2',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11313', 'Bank Eceran 3',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11314', 'Bank Eceran 4',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11315', 'Bank Eceran 5',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11316', 'Bank Eceran 6',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11317', 'Bank Eceran 7',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11318', 'Bank Eceran 8',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11319', 'Bank Eceran 9',                                            'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LANCAR  - e-Wallet :
        QITC &= "( '11351', 'e-Wallet-1',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11352', 'e-Wallet-2',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11353', 'e-Wallet-3',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11354', 'e-Wallet-4',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11355', 'e-Wallet-5',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11356', 'e-Wallet-6',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11357', 'e-Wallet-7',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11358', 'e-Wallet-8',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11359', 'e-Wallet-9',                                               'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Cash Advance :
        QITC &= "( '11401', 'Cash Advance',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11402', 'Cash Advance 2',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11403', 'Cash Advance 3',                                           'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Piutang :
        QITC &= "( '11501', 'Piutang Usaha',                                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11503', 'Piutang Afiliasi',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11504', 'Piutang Pihak Ketiga',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11505', 'Piutang Pemegang Saham',                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11506', 'Piutang Dividen',                                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11507', 'Piutang 7',                                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11508', 'Piutang Karyawan',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11509', 'Piutang Lainnya',                                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11511', 'Piutang Usaha (USD)',                                      'USD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11512', 'Piutang Usaha (AUD)',                                      'AUD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11513', 'Piutang Usaha (JPY)',                                      'JPY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11514', 'Piutang Usaha (CNY)',                                      'CNY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11515', 'Piutang Usaha (EUR)',                                      'EUR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11516', 'Piutang Usaha (SGD)',                                      'SGD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11517', 'Piutang Usaha (GBP)',                                      'GBP',  'DEBET',  'Tidak' ), "
        QITC &= "( '11520', 'Piutang Usaha - Afiliasi',                                 'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Biaya Dibayar Dimuka
        QITC &= "( '11600', 'Gaji Dibayar Dimuka',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11601', 'Sewa Tanah dan/atau Bangunan Dibayar Dimuka',              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11602', 'Sewa Mesin dan Peralatan Dibayar Dimuka',                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11603', 'Sewa Kendaraan Dibayar Dimuka',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11604', 'Biaya Renovasi Dibayar Dimuka',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11605', 'Biaya Pendirian Perusahaan Dibayar Dimuka',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11606', 'Asuransi Dibayar Dimuka',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11607', 'Sewa Asset Lainnya Dibayar Dimuka',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11608', 'Deposit Operasional',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11609', 'Bank Garansi',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11610', 'Deposit Operasional Ekspor',                               'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Uang Muka Pembelian :
        QITC &= "( '11700', 'Uang Muka Pembelian',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11701', 'Uang Muka Pembelian - Impor (USD)',                        'USD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11702', 'Uang Muka Pembelian - Impor (AUD)',                        'AUD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11703', 'Uang Muka Pembelian - Impor (JPY)',                        'JPY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11704', 'Uang Muka Pembelian - Impor (CNY)',                        'CNY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11705', 'Uang Muka Pembelian - Impor (EUR)',                        'EUR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11706', 'Uang Muka Pembelian - Impor (SGD)',                        'SGD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11707', 'Uang Muka Pembelian - Impor (GBP)',                        'GBP',  'DEBET',  'Tidak' ), "
        QITC &= "( '11710', 'Biaya Dibayar Dimuka',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11711', 'Biaya Dibayar Dimuka (USD)',                               'USD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11712', 'Biaya Dibayar Dimuka (AUD)',                               'AUD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11713', 'Biaya Dibayar Dimuka (JPY)',                               'JPY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11714', 'Biaya Dibayar Dimuka (CNY)',                               'CNY',  'DEBET',  'Tidak' ), "
        QITC &= "( '11715', 'Biaya Dibayar Dimuka (EUR)',                               'EUR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11716', 'Biaya Dibayar Dimuka (SGD)',                               'SGD',  'DEBET',  'Tidak' ), "
        QITC &= "( '11717', 'Biaya Dibayar Dimuka (GBP)',                               'GBP',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Persediaan :
        QITC &= "( '11801', 'Persediaan Bahan Penolong',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11802', 'Persediaan Bahan Baku - Lokal',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11803', 'Persediaan Barang Dalam Proses',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11804', 'Persediaan Barang Jadi',                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11805', 'Persediaan Bahan Baku - Impor',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11806', 'Persediaan 6',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11807', 'Persediaan 7',                                             'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Pajak Dibayar Dimuka :
        QITC &= "( '11901', 'Pajak Dibayar Dimuka - PPh Pasal 21',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11902', 'Pajak Dibayar Dimuka - PPh Pasal 22_Lokal',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11903', 'Pajak Dibayar Dimuka - PPh Pasal 22_Impor',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11904', 'Pajak Dibayar Dimuka - PPh Pasal 23',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11905', 'Pajak Dibayar Dimuka - PPh Pasal 4 (2)',                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11906', 'Pajak Dibayar Dimuka - PPh Pasal 25',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11907', 'PPN Masukan - Lokal',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11908', 'PPN Masukan - Impor',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11909', 'PPN - Lebih Bayar',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11910', 'PPh Badan - Lebih Bayar',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11914', 'Pajak Dibayar Dimuka - PPh Pasal 23_BP Belum Diterima',    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11915', 'Pajak Dibayar Dimuka - PPh Pasal 4 (2)_BP Belum Diterima', 'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA LANCAR  - Aktiva Lancar Lainnya :
        QITC &= "( '11921', 'Aktiva Lancar 2.1', 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11922', 'Aktiva Lancar 2.2', 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11923', 'Aktiva Lancar 2.3', 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11924', 'Aktiva Lancar 2.4', 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '11925', 'Aktiva Lancar 2.5', 'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA TETAP - Tanah, Bangunan, Kendaraan, Peralatan Kantor, Peralatan Gudang, dan Penyusutan :
        QITC &= "( '12100', 'Tanah',                                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12101', 'Tanah Kantor',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12102', 'Tanah Produksi',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12103', 'Tanah Gudang',                                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12104', 'Tanah Lainnya',                                            'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '12210', 'Bangunan Kantor',                                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12211', 'Ak. Penyusutan Bangunan Kantor',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12220', 'Bangunan Kantor Lainnya',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12221', 'Ak. Penyusutan Bangunan Kantor Lainnya',                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12230', 'Bangunan Produksi',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12231', 'Ak. Penyusutan Bangunan Produksi',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12240', 'Bangunan Produksi Lainnya',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12241', 'Ak. Penyusutan Bangunan Produksi Lainnya',                 'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '12310', 'Kendaraan Kantor',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12311', 'Ak. Penyusutan Kendaraan Kantor',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12320', 'Kendaraan Produksi',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12321', 'Ak. Penyusutan Kendaraan Produksi',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12330', 'Kendaraan Produksi Lainnya',                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12331', 'Ak. Penyusutan Kendaraan Produksi Lainnya',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12340', 'Kendaraan Lainnya',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12341', 'Ak. Penyusutan Kendaraan Lainnya',                         'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '12410', 'Mesin dan Peralatan Kantor',                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12411', 'Ak. Penyusutan Mesin dan Peralatan Kantor',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12420', 'Mesin dan Peralatan Produksi',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12421', 'Ak. Penyusutan Mesin dan Peralatan Produksi',              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12430', 'Mesin dan Peralatan Produksi Lainnya',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12431', 'Ak. Penyusutan Mesin dan Peralatan Produksi Lainnya',      'IDR',  'DEBET',  'Tidak' ), "

        'Asset Tetap Lainnya :
        QITC &= "( '12500', 'Asset Tetap Lainnya',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12501', 'Ak. Penyusutan Asset Tetap Lainnya',                       'IDR',  'DEBET',  'Tidak' ), "


        'AKTIVA TETAP - Asset Dalam Penyelesaian :
        QITC &= "( '12921', 'Asset Dalam Penyelesaian 1.1',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12922', 'Asset Dalam Penyelesaian 2.1',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12923', 'Asset Dalam Penyelesaian 3.1',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '12990', 'Bangunan Dalam Penyelesaian',                              'IDR',  'DEBET',  'Tidak' ), "

        'AKTIVA LAIN-LAIN :
        QITC &= "( '13101', 'Investasi Deposito',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '13102', 'Investasi Surat Berharga',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '13103', 'Investasi Logam Mulia',                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '13201', 'Investasi Pada Perusahaan Anak',                           'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '14101', 'Goodwill',                                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '14102', 'Aktiva Lain-lain 2',                                       'IDR',  'DEBET',  'Tidak' ), "


        '-------------------------------------
        ' P A S S I V A
        '-------------------------------------

        'PASSIVA - Hutang Usaha :
        QITC &= "( '21101', 'Hutang Usaha',                                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21103', 'Hutang Rabat Penjualan',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21111', 'Hutang Usaha (USD)',                                       'USD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21112', 'Hutang Usaha (AUD)',                                       'AUD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21113', 'Hutang Usaha (JPY)',                                       'JPY',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21114', 'Hutang Usaha (CNY)',                                       'CNY',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21115', 'Hutang Usaha (EUR)',                                       'EUR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21116', 'Hutang Usaha (SGD)',                                       'SGD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21117', 'Hutang Usaha (GBP)',                                       'GBP',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21120', 'Hutang Usaha - Afiliasi',                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21130', 'Hutang Deposit',                                           'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Biaya :
        QITC &= "( '21201', 'Hutang Biaya',                                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21202', 'Hutang Ketetapan Pajak',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21203', 'Hutang Biaya 3',                                           'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Asuransi :
        QITC &= "( '21301', 'Hutang BPJS Kesehatan',                                    'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21302', 'Hutang BPJS Ketenagakerjaan',                              'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21303', 'Hutang Asuransi 3',                                        'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21304', 'Hutang Asuransi 4',                                        'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Gaji :
        QITC &= "( '21401', 'Hutang Gaji',                                              'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21402', 'Hutang Gaji 2',                                            'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21403', 'Hutang Gaji 3',                                            'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Uang Muka Penjualan :
        QITC &= "( '21500', 'Uang Muka Penjualan',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21501', 'Uang Muka Penjualan - Ekspor (USD)',                       'USD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21502', 'Uang Muka Penjualan - Ekspor (AUD)',                       'AUD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21503', 'Uang Muka Penjualan - Ekspor (JPY)',                       'JPY',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21504', 'Uang Muka Penjualan - Ekspor (CNY)',                       'CNY',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21505', 'Uang Muka Penjualan - Ekspor (EUR)',                       'EUR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21506', 'Uang Muka Penjualan - Ekspor (SGD)',                       'SGD',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21507', 'Uang Muka Penjualan - Ekspor (GBP)',                       'GBP',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21510', 'Hutang Ongkos Kirim Penjualan',                            'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Pajak :
        QITC &= "( '21601', 'Hutang PPh Pasal 21',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21602', 'Hutang PPh Pasal 22',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21603', 'Hutang PPh Pasal 23',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21604', 'Hutang PPh Pasal 4 (2)',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21605', 'Hutang PPh Pasal 25',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21606', 'Hutang PPh Pasal 26',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21607', 'PPN Keluaran',                                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21608', 'Hutang PPN',                                               'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21609', 'Hutang PPh Pasal 29',                                      'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '21610', 'Hutang PPh Pasal 21 - 100',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21611', 'Hutang PPh Pasal 21 - 401',                                'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '21630', 'Hutang PPh Pasal 23 - 100',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21631', 'Hutang PPh Pasal 23 - 101',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21632', 'Hutang PPh Pasal 23 - 102',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21633', 'Hutang PPh Pasal 23 - 103',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21634', 'Hutang PPh Pasal 23 - 104',                                'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '21642', 'Hutang PPh Pasal 4 (2) - 402',                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21643', 'Hutang PPh Pasal 4 (2) - 403',                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21648', 'Hutang PPh Pasal 4 (2) - 409',                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21649', 'Hutang PPh Pasal 4 (2) - 419',                             'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '21660', 'Hutang PPh Pasal 26 - 100',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21661', 'Hutang PPh Pasal 26 - 101',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21662', 'Hutang PPh Pasal 26 - 102',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21663', 'Hutang PPh Pasal 26 - 103',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21664', 'Hutang PPh Pasal 26 - 104',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21665', 'Hutang PPh Pasal 26 - 105',                                'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '21680', 'Hutang PPN - 100',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21681', 'Hutang PPN - 101',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21682', 'Hutang PPN - 102',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21683', 'Hutang PPN - 103',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21684', 'Hutang PPN - Impor',                                       'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Lancar :
        QITC &= "( '21701', 'Hutang Koperasi Karyawan',                                 'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21801', 'Hutang Serikat',                                           'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21802', 'Hutang Pihak Ketiga',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21808', 'Hutang Karyawan',                                          'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '21901', 'Hutang Lancar Lainnya',                                    'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Hutang Jangka Panjang :
        QITC &= "( '22100', 'Hutang Leasing',                                           'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '22200', 'Hutang Leasing 2',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '22300', 'Hutang Leasing 3',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '22400', 'Hutang Leasing 4',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '22500', 'Hutang Leasing 5',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '23100', 'Hutang Bank',                                              'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '23201', 'Hutang Lembaga Keuangan Non Bank',                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '23300', 'Hutang Bank 3',                                            'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '23400', 'Hutang Bank 4',                                            'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '23500', 'Hutang Bank 5',                                            'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '24100', 'Hutang Pemegang Saham',                                    'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '25100', 'Hutang Afiliasi',                                          'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '26100', 'Penghasilan Diterima Dimuka Jangka Panjang',               'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '27100', 'Hutang Jangka Panjang 7',                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '28100', 'Hutang Jangka Panjang 8',                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '29100', 'Hutang Jangka Panjang 9',                                  'IDR',  'KREDIT',  'Tidak' ), "

        QITC &= "( '29999', 'Hutang Dividen',                                           'IDR',  'KREDIT',  'Tidak' ), "


        'PASSIVA - Equity :
        QITC &= "( '31101', 'Modal',                                                    'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '31102', 'Modal 2',                                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '31103', 'Modal 3',                                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '31104', 'Modal 4',                                                  'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '32101', 'Tambahan Modal Lainnya',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '33101', 'Laba Tax Amnesty',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '33102', 'Equitas Lainnya 1',                                        'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '33103', 'Equitas Lainnya 2',                                        'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '34101', 'Laba Ditahan',                                             'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '35101', 'Laba Tahun Berjalan',                                      'IDR',  'KREDIT',  'Tidak' ), "


        '-------------------------------------
        ' L A B A / R U G I
        '-------------------------------------

        'LABA/RUGI - Pendapatan :
        QITC &= "( '41001', 'Penjualan Barang - Trading',                               'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '42001', 'Penjualan Jasa',                                           'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '43001', 'Penjualan Jasa Konstruksi',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '44001', 'Penjualan Ekspor',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '45001', 'Penjualan Eceran',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '46001', 'Penghasilan Sewa Asset',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '48001', 'Penjualan Asset - Tanah dan/atau Bangunan',                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '48002', 'Penjualan Asset - Lainnya',                                'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '47001', 'Penjualan Barang - Manufaktur',                            'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '49001', 'Penjualan Limbah',                                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '49999', 'Retur Penjualan',                                          'IDR',  'KREDIT',  'Tidak' ), "


        'LABA/RUGI - Harga Pokok Produksi :
        QITC &= "( '51100', 'Pembelian Bahan Baku - Lokal',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51101', 'Retur Pembelian Bahan Baku - Lokal',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51102', 'Biaya Transportasi Pembelian BB - Lokal',                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51103', 'Biaya Pembelian Bahan Baku Lainnya - Lokal',               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51104', 'Biaya BB Lokal-4',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51105', 'Biaya BB Lokal-5',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51199', 'Biaya Pemakaian Bahan Baku - Lokal',                       'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '51200', 'Pembelian Bahan Baku - Impor',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51201', 'Retur Pembelian Bahan Baku - Impor',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51202', 'Biaya Asuransi Impor',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51203', 'Biaya Pengapalan',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51204', 'Bea Masuk Impor',                                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51205', 'Biaya Transportasi Impor',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51206', 'Biaya Jasa Pergudangan Impor',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51207', 'Biaya Pengurusan Dokumen Impor',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51208', 'Biaya BB Impor-8',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51209', 'Biaya Pembelian Bahan Baku Lainnya - Impor',               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '51299', 'Biaya Pemakaian Bahan Baku - Impor',                       'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '51300', 'Biaya Bahan Baku',                                         'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '52101', 'Biaya Gaji Produksi',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52102', 'Biaya Gaji Produksi - Tunjangan 2',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52103', 'Biaya Gaji Produksi - Tunjangan 3',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52104', 'Biaya Gaji Produksi - Tunjangan 4',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52105', 'Biaya THR/Bonus - Produksi',                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52106', 'Biaya Tunjangan PPh 21 - Produksi',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52107', 'Biaya BPJS TK-JKK/JKM - Produksi',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52108', 'Biaya BPJS TK-JHT/IP - Produksi',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52109', 'Biaya BPJS Kesehatan - Produksi',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52110', 'Biaya Asuransi Karyawan - Produksi',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52111', 'Biaya Tunjangan Zakat Penghasilan Karyawan - Produksi',    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52112', 'Biaya Gaji Produksi Lainnya-12',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52113', 'Biaya Gaji Produksi Lainnya-13',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52114', 'Biaya Gaji Produksi Lainnya-14',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '52115', 'Biaya Pesangon Karyawan - Produksi',                       'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '52300', 'Biaya Tenaga Kerja Langsung',                              'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '53100', 'Pembelian Bahan Penolong',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53101', 'Retur Pembelian Bahan Penolong',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53102', 'Biaya Subkon/Maklon',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53103', 'Biaya Makan dan Minum Produksi ',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53104', 'Biaya Energi Gas, Oksigen dan Lainnya',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53105', 'Biaya Sparepart Mesin dan Peralatan Produksi',             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53106', 'Biaya Service Mesin dan Peralatan Produksi',               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53107', 'Biaya Sparepart Kendaraan Produksi',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53108', 'Biaya Service Kendaraan Produksi',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53109', 'Biaya Transportasi, BBM, Toll dan Parkir Produksi',        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53110', 'Biaya Energi Listrik Produksi',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53111', 'Biaya Sewa Mesin dan Peralatan Produksi',                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53112', 'Biaya Sewa Kendaraan Produksi',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53113', 'Biaya Oli Mesin dan Peralatan',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53114', 'Biaya Perlengkapan Produksi',                              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53115', 'Biaya Asuransi Bangunan Produksi ',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53116', 'Biaya Asuransi Kendaraan Produksi ',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53117', 'Biaya Asuransi Peralatan Produksi ',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53118', 'Biaya Sewa Tanah dan/atau Gedung Produksi',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53119', 'Biaya Sewa Tanah dan/atau Gudang',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53120', 'Biaya Pemeliharaan Gedung dan Gudang Produksi - Jasa',     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53121', 'Biaya Jasa Catering Produksi',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53122', 'Biaya Pengiriman dan Ekspedisi Produksi',                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53123', 'Biaya Pemeliharaan Gedung dan Gudang Produksi - Bahan',    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53124', 'Biaya Jasa Maklon Produksi',                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53125', 'Biaya Jasa Outsourcing Produksi',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53126', 'Biaya Overhead-26',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53127', 'Biaya Overhead-27',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53128', 'Biaya Overhead-28',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53129', 'Biaya Turun Barang',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53199', 'Biaya Pemakaian Bahan Penolong',                           'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '53321', 'Biaya Penyusutan Bangunan Produksi',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53322', 'Biaya Penyusutan Bangunan Produksi Lainnya',               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53331', 'Biaya Penyusutan Kendaraan Produksi',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53332', 'Biaya Penyusutan Kendaraan Produksi Lainnya',              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53341', 'Biaya Penyusutan Mesin dan Peralatan Produksi',            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '53342', 'Biaya Penyusutan Mesin dan Peralatan Produksi Lainnya',    'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '53500', 'Biaya Overhead Pabrik',                                    'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '53998', 'Retur Pembelian Lainnya',                                  'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '55551', 'Biaya Produksi',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '55552', 'Harga Pokok Produksi',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '55555', 'Harga Pokok Penjualan',                                    'IDR',  'DEBET',  'Tidak' ), "


        'LABA/ RUGI - Beban Usaha 
        QITC &= "( '61101', 'Biaya Gaji Administrasi',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61102', 'Biaya Gaji Administrasi - Tunjangan 2',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61103', 'Biaya Gaji Administrasi - Tunjangan 3',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61104', 'Biaya Gaji Administrasi - Tunjangan 4',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61105', 'Biaya THR/Bonus - Administrasi',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61106', 'Biaya Tunjangan PPh 21 - Administrasi',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61107', 'Biaya BPJS TK-JKK/JKM - Administrasi',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61108', 'Biaya BPJS TK-JHT/IP - Administrasi',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61109', 'Biaya BPJS Kesehatan - Administrasi',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61110', 'Biaya Asuransi Karyawan - Administrasi',                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61111', 'Biaya Tunjangan Zakat Penghasilan Karyawan - Administrasi', 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61112', 'Biaya Gaji Administrasi Lainnya - 12',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61113', 'Biaya Gaji Administrasi Lainnya - 13',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61114', 'Biaya Gaji Administrasi Lainnya - 14',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61115', 'Biaya Gaji Administrasi Lainnya - 15',                     'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61121', 'Biaya Pengobatan Karyawan',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61122', 'Biaya Natura 2',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61123', 'Biaya Natura 3',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61129', 'Biaya Pesangon Karyawan - Administrasi',                   'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61131', 'Biaya Jasa Profesional',                                   'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61201', 'Biaya Makan dan Minum Administrasi',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61202', 'Biaya Seragam Kantor',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61203', 'Biaya Perlengkapan Kantor',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61204', 'Biaya Pulsa dan Internet',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61205', 'Biaya Transportasi, BBM, Toll dan Parkir Administrasi',    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61206', 'Biaya Pajak-Pajak Daerah',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61207', 'Biaya Perjalanan Dinas',                                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61208', 'Biaya Keamanan dan Lingkungan',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61209', 'Biaya Sertifikasi dan Legalitas',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61210', 'Biaya Asuransi Kendaraan Kantor',                          'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61211', 'Biaya Asuransi Gedung Kantor',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61212', 'Biaya Asuransi Mesin dan Peralatan Kantor',                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61213', 'Biaya Pengiriman, Pos dan Ekspedisi Administrasi',         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61214', 'Biaya Pendirian Perusahaan',                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61215', 'Biaya Tiket dan Karcis Perjalanan',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61216', 'Biaya Akomodasi dan Penginapan',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61217', 'Biaya PDAM',                                               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61218', 'Biaya Pulsa Selular',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61219', 'Biaya Jasa Outsourcing Administrasi',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61220', 'Biaya Energi Listrik Administrasi',                        'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61301', 'Biaya Pemeliharaan Gedung Administrasi - Bahan',           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61302', 'Biaya Sparepart Mesin dan Peralatan Kantor',               'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61303', 'Biaya Sparepart Kendaraan Administrasi',                   'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61304', 'Biaya Sparepart Teknologi Informasi',                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61305', 'Biaya Sparepart 5',                                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61306', 'Biaya Sparepart 6',                                        'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61401', 'Biaya Catering',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61402', 'Biaya Jasa Teknologi Informasi',                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61403', 'Biaya Pemeliharaan Kawasan/Lingkungan',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61404', 'Biaya Pemeliharaan Gedung Administrasi - Jasa',            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61405', 'Biaya Service Mesin dan Peralatan Administrasi',           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61406', 'Biaya Service Kendaraan Administrasi',                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61407', 'Biaya Jasa - 7',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61408', 'Biaya Jasa - 8',                                           'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61409', 'Biaya Jasa - 9',                                           'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61501', 'Biaya Sewa Tanah dan/atau Gedung Kantor',                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61502', 'Biaya Sewa Mesin dan Peralatan Kantor',                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61503', 'Biaya Sewa Kendaraan Kantor',                              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61504', 'Biaya Sewa Asset - 4',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61505', 'Biaya Sewa Asset - 5',                                     'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61506', 'Biaya Sewa Asset Lainnya',                                 'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61621', 'Biaya Penyusutan Bangunan Kantor',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61622', 'Biaya Penyusutan Bangunan Kantor Lainnya',                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61631', 'Biaya Penyusutan Kendaraan Kantor',                        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61632', 'Biaya Penyusutan Kendaraan Lainnya',                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61641', 'Biaya Penyusutan Mesin dan Peralatan Kantor',              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61651', 'Biaya Penyusutan Asset Tetap Lainnya',                     'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61701', 'Biaya PPh Pasal 21',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61702', 'Biaya PPh Pasal 22',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61703', 'Biaya PPh Pasal 23',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61704', 'Biaya PPh Pasal 24',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61705', 'Biaya PPh Pasal 25',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61706', 'Biaya PPh Pasal 26',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61707', 'Biaya PPh Pasal 29',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61708', 'Biaya PPh Pasal 4 (2)',                                    'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61709', 'Biaya PPN',                                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61710', 'Biaya Ketetapan Pajak',                                    'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '61801', 'Biaya Entertaint',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61802', 'Biaya Zakat dan Sumbangan',                                'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '61999', 'Biaya Belum Teridentifikasi',                              'IDR',  'DEBET',  'Tidak' ), "

        QITC &= "( '62101', 'Biaya Fee Marketing',                                      'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62201', 'Biaya Sponsorship/Pemasaran',                              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62301', 'Biaya Keperluan Pameran',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62401', 'Biaya Iklan',                                              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62402', 'Biaya Jasa Pameran',                                       'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62403', 'Biaya Transportasi BBM, Toll dan Parkir Penjualan',        'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62404', 'Biaya Asuransi Penjualan',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '62999', 'Biaya Pemasaran Belum Teridentifikasi',                    'IDR',  'DEBET',  'Tidak' ), "


        'LABA/RUGI - Penghasilan Lainnya :
        QITC &= "( '71001', 'Penghasilan Bunga/Denda Pinjaman',                         'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '78001', 'Laba/Rugi Selisih Kurs',                                   'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '78002', 'Penghasilan Dividen',                                      'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '79998', 'Penjualan Lainnya',                                        'IDR',  'KREDIT',  'Tidak' ), "
        QITC &= "( '79999', 'Penghasilan Lainnya',                                      'IDR',  'KREDIT',  'Tidak' ), "


        'LABA/RUGI - Biaya di Luar Usaha :
        QITC &= "( '81001', 'Biaya Administrasi Bank',                                  'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '82001', 'Biaya PPh atas Penghasilan Bunga',                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '82002', 'Biaya PPh Pasal 4 (2) - 402',                              'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '83001', 'Biaya Bunga Bank',                                         'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '83002', 'Biaya Denda Bank/Leasing',                                 'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '83003', 'Biaya Administrasi Perjanjian',                            'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '84001', 'HPP Penjualan/Disposal Asset',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '85001', 'Biaya Diluar Usaha Lainnya_1',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '85002', 'Biaya Diluar Usaha Lainnya_2',                             'IDR',  'DEBET',  'Tidak' ), "
        QITC &= "( '89999', 'Biaya Selisih Pencatatan',                                 'IDR',  'DEBET',  'Tidak' )  " '<----- Ujung Query, tidak pakai KOMA ya....!!!!!!!
        '------------------------------------------------------------------------------------------------------------ Kalau pakai KOMA nanti malah error..!!!!!!!

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QITC, KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        AksesDatabase_General(Tutup)
        QITC = Kosongan '(Untuk mengurangi beban memory, karena Query terlalu panjang.)

        If StatusSuntingDatabase = True Then
            HasilPembuatanDatabaseGeneral = True
            Pesan_Sukses("Pengisian Data 'COA' Berhasil.")
        Else
            HasilPembuatanDatabaseGeneral = False
            Pesan_Gagal("Pengisian Data 'COA' Gagal.")
        End If

    End Sub

End Module
