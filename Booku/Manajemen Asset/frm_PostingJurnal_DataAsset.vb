Public Class frm_PostingJurnal_DataAsset

    Dim NomorJV
    Public JalurMasuk
    Public LanjutkanPosting As Boolean

    'Variabel Terkait Jurnal

    Private Sub dlg_PilihJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ResetForm()
        LanjutkanPosting = False
        txt_Keterangan.Text = ""
        txt_Keterangan.Focus()
    End Sub

    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click
        LanjutkanPosting = False
        If txt_Keterangan.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Uraian/Keterangan'.")
            txt_Keterangan.Focus()
            Return
        End If
        Pilihan = MessageBox.Show("Yakin akan memposting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        LanjutkanPosting = True
        Me.Close()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        LanjutkanPosting = False
        Me.Close()
    End Sub

End Class