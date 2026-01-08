Public Class frm_Help

    Private Sub frm_Help_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbl_KontakKami.Text = WebsiteAplikasi & "/KontakKami"
        lbl_NomorHotline.Text = NomorHotLine
    End Sub

    Private Sub lbl_KontakKami_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbl_KontakKami.LinkClicked

    End Sub

End Class