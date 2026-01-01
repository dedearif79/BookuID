Public Class frm_PilihJurnal_Penjualan_Retur

    Public NomorJV_Penjualan
    Public NomorJV_Retur

    Private Sub frm_PilihJurnal_Penjualan_Retur_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub ResetForm()
        NomorJV_Penjualan = 0
        NomorJV_Retur = 0
    End Sub

    Private Sub btn_JurnalPenjualan_Click(sender As Object, e As EventArgs) Handles btn_JurnalPenjualan.Click
        LihatJurnal(NomorJV_Penjualan)
    End Sub

    Private Sub btn_JurnalRetur_Click(sender As Object, e As EventArgs) Handles btn_JurnalRetur.Click
        LihatJurnal(NomorJV_Retur)
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class