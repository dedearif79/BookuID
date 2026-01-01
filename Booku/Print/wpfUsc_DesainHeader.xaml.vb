Imports bcomm
Imports System.Windows


Public Class wpfUsc_DesainHeader

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ResetForm()

        IsiGambarLogoPerusahaan(img_LogoPerusahaan)
        If PengambilanGambar = False Then
            PesanPeringatan("File logo perusahaan tidak terdeteksi." & Enter2Baris &
                             "Silakan upload logo perusahaan terlebih dahulu pada menu 'Company Profile', atau Anda akan mencetak dokumen tanpa logo.")
        End If

        ProsesLoadingForm = True

        lbl_NamaPerusahaan.Text = NamaPerusahaan
        lbl_TaglinePerusahaan.Text = TaglinePerusahaan
        lbl_AlamatPerusahaan.Text = AlamatPerusahaan
        lbl_KontakPerusahaan.Text = "Kontak : " & PICPerusahaan

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        lbl_NamaPerusahaan.Text = Kosongan
        lbl_TaglinePerusahaan.Text = Kosongan
        lbl_AlamatPerusahaan.Text = Kosongan
        lbl_KontakPerusahaan.Text = Kosongan

    End Sub


    Sub New()

        InitializeComponent()

    End Sub

End Class
