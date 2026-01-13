Imports bcomm

Public Class frm_InputJurnalPerTransaksi

    Public JalurMasuk
    Public FungsiForm
    Public COATerseleksi
    Public NamaAkunTerseleksi
    Public DK
    Public JumlahDebet As Int64
    Public JumlahKredit As Int64
    Public JumlahTransaksi As Int64
    Public TombolPenutup

    Private Sub frm_InputJurnalPerTransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_EDIT Then

        End If

        If FungsiForm = FungsiForm_TAMBAH Then

        End If

    End Sub

    Sub KontenComboDK()
        cmb_DK.Items.Clear()
        cmb_DK.Items.Add("DEBET")
        cmb_DK.Items.Add("KREDIT")
        cmb_DK.Text = ""
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_COA.Enabled = True
        btn_PilihCOA.Enabled = True
        txt_NamaAkun.Enabled = False
        cmb_DK.Enabled = True
        txt_JumlahTransaksi.Enabled = True
        txt_COA.Text = Nothing
        txt_NamaAkun.Text = Nothing
        KontenComboDK()
        txt_JumlahTransaksi.Text = Nothing

        ProsesResetForm = False

    End Sub

    Private Sub txt_COA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_COA_Enter(sender As Object, e As EventArgs) Handles txt_COA.Enter
        'btn_PilihCOA_Click(sender, e)
    End Sub

    Private Sub btn_PilihCOA_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Semua
        win_ListCOA.ShowDialog()
        txt_COA.Text = win_ListCOA.COATerseleksi
        txt_NamaAkun.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        Try
            If txt_JumlahTransaksi.Text.Trim() <> "" Then
                txt_JumlahTransaksi.Text = CDec(txt_JumlahTransaksi.Text).ToString("N0")
                txt_JumlahTransaksi.SelectionStart = txt_JumlahTransaksi.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

        If txt_COA.Text = Nothing Then
            MsgBox("Silakan isi 'Kode Akun'")
            txt_COA.Focus()
            Return
        End If
        If txt_NamaAkun.Text = Nothing Then
            MsgBox("Silakan isi 'Nama Akun'")
            txt_NamaAkun.Focus()
            Return
        End If
        If FungsiForm = FungsiForm_TAMBAH Then
            For Each row As DataGridViewRow In win_InputJurnal.datatabelUtama.Rows 'Kalau nanti ada ERROR di sini, berarti masalahnya ada di DataGridView yang seharusnya adalah DataGrid
                Dim KodeAkun = row.Cells("Kode_Akun").Value
                If txt_COA.Text = KodeAkun Then
                    MsgBox("Akun '" & txt_NamaAkun.Text & "' sudah ada pada jurnal ini." _
                           & Enter2Baris & "Silakan input akun yang lain, atau edit akun yang sudah ada.")
                    Return
                End If
            Next
        End If
        If cmb_DK.Text = Nothing Then
            MsgBox("Silakan pilih 'Debet/Kredit'")
            cmb_DK.Focus()
            Return
        End If
        If AmbilAngka(txt_JumlahTransaksi.Text) = 0 Then
            MsgBox("Silakan isi 'Jumlah Transaksi'")
            txt_JumlahTransaksi.Text = Nothing
            txt_JumlahTransaksi.Focus()
            Return
        End If

        COATerseleksi = txt_COA.Text
        NamaAkunTerseleksi = txt_NamaAkun.Text
        If cmb_DK.Text = "DEBET" Then
            DK = "D"
        End If
        If cmb_DK.Text = "KREDIT" Then
            DK = "K"
        End If
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        TombolPenutup = "OK"
        Me.Close()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        TombolPenutup = "BATAL"
        Me.Close()
    End Sub

    Private Sub cmb_DK_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DK.SelectedIndexChanged
    End Sub
    Private Sub cmb_DK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DK.KeyPress

        If e.KeyChar = Chr(68) Then cmb_DK.Text = "DEBET" 'Belum berfungsi.
        If e.KeyChar = Chr(75) Then cmb_DK.Text = "KREDIT" 'Belum berfungsi.
        KunciTotalInputan(sender, e)

    End Sub

End Class
