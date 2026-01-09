Imports System.Windows.Forms
Imports bcomm

Module mdl_Pesan

    Private Const JudulAplikasi As String = "Booku"

#Region "Enum Kategori Pesan"

    Public Enum KategoriPesan
        Informasi       ' Pemberitahuan biasa
        Sukses          ' Operasi berhasil
        Peringatan      ' Warning/Alert
        Gagal           ' Operasi gagal
        KesalahanTeknis ' Error sistem
    End Enum

#End Region

#Region "Fungsi Utama"

    ''' <summary>
    ''' Menampilkan pesan dengan kategori tertentu
    ''' </summary>
    Public Sub TampilkanPesan(pesan As String, kategori As KategoriPesan)
        Dim ikon As MessageBoxIcon
        Select Case kategori
            Case KategoriPesan.Informasi, KategoriPesan.Sukses
                ikon = MessageBoxIcon.Information
            Case KategoriPesan.Peringatan
                ikon = MessageBoxIcon.Warning
            Case KategoriPesan.Gagal, KategoriPesan.KesalahanTeknis
                ikon = MessageBoxIcon.Error
            Case Else
                ikon = MessageBoxIcon.Information
        End Select
        MessageBox.Show(pesan, JudulAplikasi, MessageBoxButtons.OK, ikon)
    End Sub

    ''' <summary>
    ''' Menampilkan konfirmasi Yes/No dan mengembalikan True jika Yes
    ''' </summary>
    Public Function TanyaKonfirmasi(pesan As String) As Boolean
        Dim hasil = MessageBox.Show(pesan, JudulAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Return hasil = DialogResult.Yes
    End Function

    ''' <summary>
    ''' Menampilkan pesan dengan tombol kustom dan mengembalikan DialogResult
    ''' </summary>
    Public Function TampilkanPesanDenganPilihan(pesan As String, tombol As MessageBoxButtons, ikon As MessageBoxIcon) As DialogResult
        Return MessageBox.Show(pesan, JudulAplikasi, tombol, ikon)
    End Function

#End Region

#Region "Fungsi Shortcut per Kategori"

    ''' <summary>
    ''' Menampilkan pesan informasi
    ''' </summary>
    Public Sub Pesan_Informasi(pesan As String)
        TampilkanPesan(pesan, KategoriPesan.Informasi)
    End Sub

    ''' <summary>
    ''' Menampilkan pesan sukses/berhasil
    ''' </summary>
    Public Sub Pesan_Sukses(pesan As String)
        TampilkanPesan(pesan, KategoriPesan.Sukses)
    End Sub

    ''' <summary>
    ''' Menampilkan pesan peringatan
    ''' </summary>
    Public Sub Pesan_Peringatan(pesan As String)
        TampilkanPesan(pesan, KategoriPesan.Peringatan)
    End Sub

    ''' <summary>
    ''' Menampilkan pesan gagal
    ''' </summary>
    Public Sub Pesan_Gagal(pesan As String)
        TampilkanPesan(pesan, KategoriPesan.Gagal)
    End Sub

    ''' <summary>
    ''' Menampilkan pesan kesalahan teknis
    ''' </summary>
    Public Sub Pesan_KesalahanTeknis(Optional detailError As String = "")
        Dim pesan As String = "Terjadi kesalahan teknis. Silakan coba lagi."
        If Not String.IsNullOrEmpty(detailError) Then
            pesan &= Enter2Baris & detailError
        End If
        TampilkanPesan(pesan, KategoriPesan.KesalahanTeknis)
    End Sub

    ''' <summary>
    ''' Menampilkan konfirmasi dan mengembalikan True jika Yes
    ''' </summary>
    Public Function Pesan_Konfirmasi(pesan As String) As Boolean
        Return TanyaKonfirmasi(pesan)
    End Function

    ''' <summary>
    ''' Konfirmasi untuk melanjutkan aksi tertentu
    ''' </summary>
    Public Function Pesan_KonfirmasiLanjutkan(aksi As String) As Boolean
        Return TanyaKonfirmasi(aksi & Enter2Baris & "Lanjutkan?")
    End Function

    ''' <summary>
    ''' Konfirmasi untuk menghapus data
    ''' </summary>
    Public Function Pesan_KonfirmasiHapus(Optional namaData As String = "data terpilih") As Boolean
        Return TanyaKonfirmasi("Yakin ingin menghapus " & namaData & "?")
    End Function

    ''' <summary>
    ''' Konfirmasi untuk menyimpan data
    ''' </summary>
    Public Function Pesan_KonfirmasiSimpan(Optional keterangan As String = "") As Boolean
        Dim pesan As String = "Lanjutkan penyimpanan?"
        If Not String.IsNullOrEmpty(keterangan) Then
            pesan = keterangan & Enter2Baris & pesan
        End If
        Return TanyaKonfirmasi(pesan)
    End Function

#End Region

#Region "Konstanta Teks Standar"

    ' === Teks Sukses ===
    Public teks_SimpanData As String = "Data berhasil disimpan."
    Public teks_EditData As String = "Data berhasil diperbarui."
    Public teks_HapusData As String = "Data berhasil dihapus."
    Public teks_TambahData As String = "Data berhasil ditambahkan."
    Public teks_KirimKeJurnal As String = "Data berhasil dikirim ke Jurnal."

    ' === Teks Gagal ===
    Public teks_GagalSimpan As String = "Data gagal disimpan."
    Public teks_GagalEdit As String = "Data gagal diperbarui."
    Public teks_GagalHapus As String = "Data gagal dihapus."
    Public teks_GagalKirimKeJurnal As String = "Data gagal dikirim ke Jurnal."

    ' === Teks Instruksi ===
    Public teks_SilakanPilihDataHapus As String = "Silakan pilih data yang akan dihapus."
    Public teks_SilakanCobaLagi_Database As String = "Pastikan koneksi database lancar, lalu coba lagi."
    Public teks_SilakanUlangiLagi_Database As String = "Pastikan koneksi database lancar, lalu ulangi lagi."
    Public teks_SilakanCobaLagi_Internet As String = "Pastikan koneksi internet lancar, lalu coba lagi."
    Public teks_SilakanUlangiLagi_Internet As String = "Pastikan koneksi internet lancar, lalu ulangi lagi."
    Public teks_SilakanSesuaikanSaldo As String = "Silakan sesuaikan selisih Saldo Awal dengan klik tombol Sesuaikan."

    ' === Teks Peringatan ===
    Public teks_CoaBelumTerdaftar As String = "COA belum terdaftar."
    Public teks_SistemTerkunci_HubungiDeveloper As String = "Sistem terkunci karena kesalahan teknis. Silakan hubungi Developer."
    Public teks_BelumDibikinKodingnya As String = "Fitur ini belum tersedia."

    ' === Teks Konfirmasi ===
    Public teks_TanyaSetuju As String = "Setelah disetujui, data tidak dapat direvisi lagi." & Enter2Baris & "Yakin ingin menyetujui?"
    Public teks_DataAkanDisimpanDiBukuPengawasanDanJurnal As String = "Data akan disimpan di Buku Pengawasan dan dikirim ke Jurnal." & Enter2Baris & "Lanjutkan penyimpanan?"
    Public teks_PerubahanDataAkanBerpengaruhPadaJurnal As String = "Perubahan data ini akan berpengaruh pada Jurnal terkait." & Enter2Baris & "Lanjutkan penyimpanan?"

#End Region

#Region "Pesan Spesifik - Koneksi"

    Public Sub pesan_AdaMasalahDenganKoneksiDatabase()
        Pesan_Gagal("Ada masalah dengan koneksi database." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_AdaMasalahDenganKoneksiInternet()
        Pesan_Gagal("Ada masalah dengan koneksi internet." & Enter2Baris & teks_SilakanCobaLagi_Internet)
    End Sub

    Public Sub pesan_AdaKesalahanTeknis_Database(PesanError)
        Pesan_KesalahanTeknis(teks_SilakanCobaLagi_Database & Enter2Baris & PesanError)
    End Sub

    Public Sub pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
        Pesan_KesalahanTeknis("Silakan coba lagi dalam beberapa saat.")
    End Sub

#End Region

#Region "Pesan Spesifik - CRUD"

    ' === Hapus ===
    Public Sub pesan_DataTerpilihBerhasilDihapus()
        Pesan_Sukses("Data terpilih berhasil dihapus.")
    End Sub

    Public Sub pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_Sukses("Data terpilih berhasil dihapus.")
        Else
            Pesan_Sukses("Data terpilih berhasil dihapus dari tabel ini dan dari tabel Jurnal.")
        End If
    End Sub

    Public Sub pesan_DataTerpilihGagalDihapus()
        Pesan_Gagal(teks_GagalHapus & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    ' === Simpan ===
    Public Sub pesan_DataBerhasilDisimpan()
        Pesan_Sukses(teks_SimpanData)
    End Sub

    Public Sub pesan_DataBerhasilDitambahkan()
        Pesan_Sukses(teks_TambahData)
    End Sub

    Public Sub pesan_DataBerhasilDisimpan_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_Sukses("Data terpilih berhasil disimpan.")
        Else
            Pesan_Sukses("Data terpilih berhasil disimpan di tabel ini dan di tabel Jurnal.")
        End If
    End Sub

    Public Sub pesan_DataGagalDisimpan()
        Pesan_Gagal(teks_GagalSimpan & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    ' === Edit/Perbarui ===
    Public Sub pesan_DataGagalDiperbarui()
        Pesan_Gagal(teks_GagalEdit & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_DataTerpilihBerhasilDiperbarui()
        Pesan_Sukses("Data terpilih berhasil diperbarui.")
    End Sub

    Public Sub pesan_DataBerhasilDiedit_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Pesan_Sukses("Data terpilih berhasil diperbarui.")
        Else
            Pesan_Sukses("Data terpilih berhasil diperbarui di tabel ini dan di tabel Jurnal.")
        End If
    End Sub

    Public Sub pesan_DataTerpilihGagalDiperbarui()
        Pesan_Gagal(teks_GagalEdit & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    ' === Jurnal ===
    Public Sub pesan_DataBerhasilDikirimKeJurnal()
        Pesan_Sukses(teks_KirimKeJurnal)
    End Sub

    Public Sub pesan_DataGagalDikirimKeJurnal()
        Pesan_Gagal(teks_GagalKirimKeJurnal & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

#End Region

#Region "Pesan Spesifik - Validasi"

    Public Sub pesan_SilakanIsiKolom(namaKolom As String)
        Pesan_Peringatan("Silakan isi " & namaKolom & ".")
    End Sub

    Public Sub pesan_SilakanPilih(namaItem As String)
        Pesan_Peringatan("Silakan pilih " & namaItem & ".")
    End Sub

    Public Sub pesan_DataSudahAda(namaData As String)
        Pesan_Peringatan(namaData & " sudah ada. Silakan masukkan yang lain.")
    End Sub

    Public Sub pesan_DataTidakDitemukan(Optional namaData As String = "Data")
        Pesan_Peringatan(namaData & " tidak ditemukan.")
    End Sub

    Public Sub pesan_TidakDapatDiedit(Optional alasan As String = "")
        Dim pesan As String = "Data tidak dapat diedit."
        If Not String.IsNullOrEmpty(alasan) Then
            pesan &= " " & alasan
        End If
        Pesan_Peringatan(pesan)
    End Sub

    Public Sub pesan_TidakDapatDihapus(Optional alasan As String = "")
        Dim pesan As String = "Data tidak dapat dihapus."
        If Not String.IsNullOrEmpty(alasan) Then
            pesan &= " " & alasan
        End If
        Pesan_Peringatan(pesan)
    End Sub

#End Region

End Module
