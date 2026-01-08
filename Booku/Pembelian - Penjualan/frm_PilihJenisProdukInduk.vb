Imports bcomm

Public Class frm_PilihJenisProdukInduk

    Public JenisProduk_Induk
    Public Lanjutkan As Boolean

    Private Sub frm_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BeginInvoke(Sub() ResetForm())
    End Sub

    Sub ResetForm()
        JenisProduk_Induk = Kosongan
        Lanjutkan = False
        rdb_Barang.Checked = False
        rdb_Jasa.Checked = False
        rdb_BarangDanJasa.Checked = False
        rdb_JasaKonstruksi.Checked = False
        btn_Lanjutkan.Enabled = False
    End Sub

    Sub PilihJenisProduk()
        If rdb_Barang.Checked = True Then JenisProduk_Induk = JenisProduk_Barang
        If rdb_Jasa.Checked = True Then JenisProduk_Induk = JenisProduk_Jasa
        If rdb_BarangDanJasa.Checked = True Then JenisProduk_Induk = JenisProduk_BarangDanJasa
        If rdb_JasaKonstruksi.Checked = True Then JenisProduk_Induk = JenisProduk_JasaKonstruksi
        If rdb_Barang.Checked = True Or rdb_Jasa.Checked = True Or rdb_BarangDanJasa.Checked = True Or rdb_JasaKonstruksi.Checked = True Then
            btn_Lanjutkan.Enabled = True
        Else
            btn_Lanjutkan.Enabled = False
        End If
    End Sub

    Private Sub rdb_Barang_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Barang.CheckedChanged
        PilihJenisProduk()
    End Sub

    Private Sub rdb_Jasa_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Jasa.CheckedChanged
        PilihJenisProduk()
    End Sub

    Private Sub rdb_BarangDanJasa_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_BarangDanJasa.CheckedChanged
        PilihJenisProduk()
    End Sub

    Private Sub rdb_JasaKonstruksi_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_JasaKonstruksi.CheckedChanged
        PilihJenisProduk()
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click
        Lanjutkan = True
        Me.Close()
    End Sub

End Class