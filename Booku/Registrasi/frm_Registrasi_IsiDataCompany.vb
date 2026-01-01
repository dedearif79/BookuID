Imports bcomm

Public Class frm_Registrasi_IsiDataCompany

    Private Sub frm_BuatDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ResetForm()

    End Sub

    Public Sub ResetForm()

        txt_NamaPerusahaan.Text = Nothing
        txt_NamaDirektur.Text = Nothing
        txt_NPWP.Text = Nothing
        KontenComboJenisUsahaPerusahaan()
        KontenComboJenisWPPerusahaan()
        txt_AlamatPerusahaan.Text = Nothing
        txt_EmailPerusahaan.Text = Nothing
        txt_PIC.Text = Nothing
        txt_NomorSKT.Text = Kosongan
        KontenComboSistemApproval()
        KontenComboTahunCutOff()
        KontenComboSistemCOA()

    End Sub

    Sub KontenComboJenisUsahaPerusahaan()
        cmb_JenisUsahaPerusahaan.Items.Clear()
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_IndustriManufactur)
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_Dagang)
        cmb_JenisUsahaPerusahaan.Items.Add(JenisPerusahaan_Jasa)
        cmb_JenisUsahaPerusahaan.Text = Kosongan
    End Sub

    Sub KontenComboJenisWPPerusahaan()
        cmb_JenisWPPerusahaan.Items.Clear()
        cmb_JenisWPPerusahaan.Items.Add(JenisWP_OrangPribadi)
        cmb_JenisWPPerusahaan.Items.Add(JenisWP_BadanHukum)
        cmb_JenisWPPerusahaan.Text = Kosongan
    End Sub

    Sub KontenComboSistemApproval()
        cmb_SistemApproval.Items.Clear()
        cmb_SistemApproval.Items.Add(Pilihan_YA_)
        cmb_SistemApproval.Items.Add(Pilihan_TIDAK_)
        cmb_SistemApproval.Text = Kosongan
    End Sub

    Sub KontenComboTahunCutOff()
        cmb_TahunCutOff.Items.Clear()
        cmb_TahunCutOff.Items.Add(TahunIni - 4)
        cmb_TahunCutOff.Items.Add(TahunIni - 3)
        cmb_TahunCutOff.Items.Add(TahunIni - 2)
        cmb_TahunCutOff.Items.Add(TahunIni - 1)
        cmb_TahunCutOff.Text = TahunIni - 1
    End Sub

    Sub KontenComboSistemCOA()
        cmb_SistemCOA.Items.Clear()
        cmb_SistemCOA.Items.Add(SistemCOA_StandarAplikasi)
        cmb_SistemCOA.Items.Add(SistemCOA_Customize)
        cmb_SistemCOA.Text = Kosongan
    End Sub

    Private Sub cmb_JenisPerusahaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisUsahaPerusahaan.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPerusahaan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisUsahaPerusahaan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_SistemApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SistemApproval.SelectedIndexChanged
    End Sub
    Private Sub cmb_SistemApproval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_SistemApproval.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_TahunCutOff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunCutOff.SelectedIndexChanged
    End Sub
    Private Sub cmb_TahunCutOff_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunCutOff.KeyPress
        KunciTotalInputan(sender, e)
    End Sub



    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If txt_NamaPerusahaan.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'Nama Perusahaan'")
            txt_NamaPerusahaan.Focus()
            Return
        End If

        If txt_NamaDirektur.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'Nama Direktur'")
            txt_NamaDirektur.Focus()
            Return
        End If

        If txt_NPWP.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'NPWP'")
            txt_NPWP.Focus()
            Return
        End If

        If cmb_JenisUsahaPerusahaan.Text = Kosongan Then
            MsgBox("Silakan pilih 'Jenis Usaha'")
            cmb_JenisUsahaPerusahaan.Focus()
            Return
        End If

        If cmb_JenisWPPerusahaan.Text = Kosongan Then
            MsgBox("Silakan pilih 'Jenis WP'")
            cmb_JenisWPPerusahaan.Focus()
            Return
        End If

        If txt_AlamatPerusahaan.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'Alamat'")
            txt_AlamatPerusahaan.Focus()
            Return
        End If

        If txt_EmailPerusahaan.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'Email'")
            txt_EmailPerusahaan.Focus()
            Return
        End If

        If txt_PIC.Text = Kosongan Then
            MsgBox("Silakan ini kolom 'PIC'")
            txt_PIC.Focus()
            Return
        End If

        If cmb_SistemApproval.Text = Kosongan Then
            MsgBox("Silakan pilih 'Sistem Approval'")
            cmb_SistemApproval.Focus()
            Return
        End If

        If cmb_SistemApproval.Text = "YA" Then
            MsgBox("Mohon maaf. Untuk fitur 'Sistem Approval' sementara ini belum bisa digunakan." & Enter2Baris & "Silakan pilih 'TIDAK'.")
            cmb_SistemApproval.Focus()
            Return
        End If

        If cmb_SistemCOA.Text = Kosongan Then
            MsgBox("Silakan pilih Sistem COA yang akan digunakan.")
            cmb_SistemCOA.Focus()
            Return
        End If

        If cmb_SistemCOA.Text = SistemCOA_Customize Then
            MsgBox("Mohon maaf. Sistem COA 'Customize' saat ini belum memungkinkan." & Enter2Baris &
                   "Silakan pilih Sistem COA '" & SistemCOA_StandarAplikasi & "'. ")
            cmb_SistemCOA.Focus()
            Return
        End If

        Lanjutkan = True
        Me.Close()

    End Sub

End Class