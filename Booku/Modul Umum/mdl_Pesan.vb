Imports bcomm

Module mdl_Pesan

    Public teks_SimpanData As String = "Data berhasil disimpan."

    Public teks_EditData As String = "Data behasil diedit."

    Public teks_HapusData As String = "Data sudah dihapus."

    Public teks_SilakanPilihDataHapus As String = "Silakan pilih data yang akan dihapus."

    Public teks_CoaBelumTerdaftar As String = "COA BELUM TERDAFTAR..!"

    Public teks_TanyaSetuju As String = "Setelah disetujui, data tidak akan bisa lagi direvisi." & Enter2Baris & "Yakin akan menyetujui "

    Public teks_SilakanCobaLagi_Database As String = "Pastikan koneksi database lancar, dan silakan dicoba lagi."

    Public teks_SilakanUlangiLagi_Database As String = "Pastikan koneksi database lancar, dan silakan ulangi lagi."

    Public teks_SilakanCobaLagi_Internet As String = "Pastikan koneksi internet lancar, dan silakan dicoba lagi."

    Public teks_SilakanUlangiLagi_Internet As String = "Pastikan koneksi internet lancar, dan silakan ulangi lagi."

    Public teks_SistemTerkunci_HubungiDeveloper As String = "Sistem terkunci karena ada kesalahan teknis..!!!" & Enter2Baris & "Silakan hubungi Developer."

    Public teks_DataAkanDisimpanDiBukuPengawasanDanJurnal As String = "Data akan disimpan di Buku Pengawasan dan akan dikirim ke Jurnal." & Enter2Baris & "Lanjutkan penyimpanan..?"

    Public teks_PerubahanDataAkanBerpengaruhPadaJurnal As String = "Perubahan data ini akan berpengaruh pula pada Jurnal terkait." & Enter2Baris & "Lanjutkan penyimpanan..?"

    Public teks_SilakanSesuaikanSaldo As String = "Silakan sesuaikan selisih Saldo Awal dengan cara klik tombol Sesuaikan..!"

    Public teks_BelumDibikinKodingnya As String = "Belum dibikin kodingnya"

    Public Sub pesan_AdaMasalahDenganKoneksiDatabase()
        PesanPeringatan("Ada masalah dengan koneksi database." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_AdaMasalahDenganKoneksiInternet()
        PesanPeringatan("Ada masalah dengan koneksi internet." & Enter2Baris & teks_SilakanCobaLagi_Internet)
    End Sub

    Public Sub pesan_AdaKesalahanTeknis_Database(PesanError)
        PesanPeringatan("Ups... Ada kesalahan teknis!" & Enter2Baris & teks_SilakanCobaLagi_Database & Enter2Baris & PesanError)
    End Sub

    Public Sub pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
        PesanPeringatan("Ups... Ada Kesalahan Teknis..!" & Enter2Baris & "Silakan coba lagi dalam beberapa saat.")
    End Sub

    Public Sub pesan_DataTerpilihBerhasilDihapus()
        MsgBox("Data terpilih BERHASIL dihapus.")
    End Sub

    Public Sub pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then MsgBox("Data terpilih BERHASIL dihapus.")
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then MsgBox("Data terpilih BERHASIL dihapus dari tabel ini dan dari tabel Jurnal.")
    End Sub

    Public Sub pesan_DataTerpilihGagalDihapus()
        PesanPeringatan("Data terpilih GAGAL dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_DataBerhasilDisimpan()
        MsgBox("Data BERHASIL disimpan.")
    End Sub

    Public Sub pesan_DataBerhasilDitambahkan()
        MsgBox("Data BERHASIL ditambahkan.")
    End Sub

    Public Sub pesan_DataBerhasilDisimpan_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then MsgBox("Data terpilih BERHASIL disimpan.")
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then MsgBox("Data terpilih BERHASIL disimpan di tabel ini dan di tabel Jurnal.")
    End Sub

    Public Sub pesan_DataGagalDisimpan()
        PesanPeringatan("Data GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_DataGagalDiperbarui()
        PesanPeringatan("Data GAGAL diperbarui." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_DataTerpilihBerhasilDiperbarui()
        PesanSukses("Data terpilih BERHASIL diperbarui.")
    End Sub

    Public Sub pesan_DataBerhasilDiedit_PlusJurnal()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then MsgBox("Data terpilih BERHASIL diperbarui.")
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then MsgBox("Data terpilih BERHASIL diperbarui di tabel ini dan di tabel Jurnal.")
    End Sub

    Public Sub pesan_DataTerpilihGagalDiperbarui()
        PesanPeringatan("Data GAGAL diperbarui." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

    Public Sub pesan_DataBerhasilDikirimKeJurnal()
        MsgBox("Data BERHASIL dikirim ke JURNAL.")
    End Sub

    Public Sub pesan_DataGagalDikirimKeJurnal()
        PesanPeringatan("Data GAGAL dikirim ke JURNAL." & Enter2Baris & teks_SilakanCobaLagi_Database)
    End Sub

End Module
