Imports bcomm

Public Class X_frm_InputSJBASTManual

    Public JudulForm
    Public FungsiForm

    Dim NomorSJBAST
    Public JalurMasuk
    'Dim TanggalSJBAST
    'Dim TanggalDiterima

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If JalurMasuk = Kosongan Then
            PesanUntukProgrammer("Jalur Masuk belum ditentukan...!!!")
        End If


        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        JalurMasuk = Kosongan
        txt_NomorSJBAST.Text = Kosongan
        dtp_TanggalSJBAST.Value = Today
        dtp_TanggalDiterima.Value = Today

        ProsesResetForm = False

    End Sub

    Private Sub txt_NomorSJBAST_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorSJBAST.TextChanged
        NomorSJBAST = txt_NomorSJBAST.Text
    End Sub

    Private Sub dtp_TanggalSJBAST_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalSJBAST.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalSJBAST)
        KunciTanggalBulanDanTahun_TidakBolehLebihDari(dtp_TanggalSJBAST, dtp_TanggalDiterima.Value.Day, dtp_TanggalDiterima.Value.Month, dtp_TanggalDiterima.Value.Year)
        'TanggalSJBAST = TanggalFormatTampilan(dtp_TanggalSJBAST.Value)
    End Sub

    Private Sub dtp_TanggalDiterima_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalDiterima.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalDiterima)
        'TanggalDiterima = TanggalFormatTampilan(dtp_TanggalDiterima.Value)
    End Sub

    Private Sub btn_Tambahkan_Click(sender As Object, e As EventArgs) Handles btn_Tambahkan.Click
        Select Case JalurMasuk
            Case Form_INPUTINVOICEPEMBELIAN
                win_InputInvoicePembelian.datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalFormatTampilan(dtp_TanggalSJBAST.Value), TanggalFormatTampilan(dtp_TanggalDiterima.Value), Kosongan, 0)
            Case Form_INPUTINVOICEPENJUALAN
                win_InputInvoicePenjualan.datatabelSJBAST.Rows.Add(NomorSJBAST, TanggalFormatTampilan(dtp_TanggalSJBAST.Value), TanggalFormatTampilan(dtp_TanggalDiterima.Value), Kosongan, 0)
        End Select
        Me.Close()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub


End Class