Public Class X_ops_PilihJurnal_DataAsset

    Dim NomorJV
    Public JalurMasuk

    'Variabel Terkait Jurnal
    Public AngkaNomorJV_Januari
    Public AngkaNomorJV_Februari
    Public AngkaNomorJV_Maret
    Public AngkaNomorJV_April
    Public AngkaNomorJV_Mei
    Public AngkaNomorJV_Juni
    Public AngkaNomorJV_Juli
    Public AngkaNomorJV_Agustus
    Public AngkaNomorJV_September
    Public AngkaNomorJV_Oktober
    Public AngkaNomorJV_Nopember
    Public AngkaNomorJV_Desember

    Private Sub dlg_PilihJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ResetForm()
        rdb_Januari.Enabled = False
        rdb_Januari.Checked = False
        rdb_Februari.Enabled = False
        rdb_Februari.Checked = False
        rdb_Maret.Enabled = False
        rdb_Maret.Checked = False
        rdb_April.Enabled = False
        rdb_April.Checked = False
        rdb_Mei.Enabled = False
        rdb_Mei.Checked = False
        rdb_Juni.Enabled = False
        rdb_Juni.Checked = False
        rdb_Juli.Enabled = False
        rdb_Juli.Checked = False
        rdb_Agustus.Enabled = False
        rdb_Agustus.Checked = False
        rdb_September.Enabled = False
        rdb_September.Checked = False
        rdb_Oktober.Enabled = False
        rdb_Oktober.Checked = False
        rdb_Nopember.Enabled = False
        rdb_Nopember.Checked = False
        rdb_Desember.Enabled = False
        rdb_Desember.Checked = False
        btn_Lanjutkan.Enabled = False
    End Sub

    Sub LogikaTombolLanjutkan()
        btn_Lanjutkan.Enabled = True
    End Sub

    Private Sub rdb_Januari_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Januari.CheckedChanged
        If rdb_Januari.Checked = True Then
            NomorJV = AngkaNomorJV_Januari
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Februari_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Februari.CheckedChanged
        If rdb_Februari.Checked = True Then
            NomorJV = AngkaNomorJV_Februari
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Maret_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Maret.CheckedChanged
        If rdb_Maret.Checked = True Then
            NomorJV = AngkaNomorJV_Maret
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_April_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_April.CheckedChanged
        If rdb_April.Checked = True Then
            NomorJV = AngkaNomorJV_April
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Mei_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Mei.CheckedChanged
        If rdb_Mei.Checked = True Then
            NomorJV = AngkaNomorJV_Mei
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Juni_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Juni.CheckedChanged
        If rdb_Juni.Checked = True Then
            NomorJV = AngkaNomorJV_Juni
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Juli_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Juli.CheckedChanged
        If rdb_Juli.Checked = True Then
            NomorJV = AngkaNomorJV_Juli
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Agustus_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Agustus.CheckedChanged
        If rdb_Agustus.Checked = True Then
            NomorJV = AngkaNomorJV_Agustus
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_September_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_September.CheckedChanged
        If rdb_September.Checked = True Then
            NomorJV = AngkaNomorJV_September
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Oktober_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Oktober.CheckedChanged
        If rdb_Oktober.Checked = True Then
            NomorJV = AngkaNomorJV_Oktober
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Nopember_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Nopember.CheckedChanged
        If rdb_Nopember.Checked = True Then
            NomorJV = AngkaNomorJV_Nopember
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub rdb_Desember_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Desember.CheckedChanged
        If rdb_Desember.Checked = True Then
            NomorJV = AngkaNomorJV_Desember
            LogikaTombolLanjutkan()
        End If
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click
        LihatJurnal(NomorJV)
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class