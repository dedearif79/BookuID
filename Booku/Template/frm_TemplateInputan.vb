Imports bcomm

Public Class frm_TemplateInputan

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input ......"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit ......."
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")



        Me.Text = JudulForm

        ProsesLoadingForm = False

        BeginInvoke(Sub() Style_FormInput(Me))

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0


        ProsesResetForm = False

    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        If FungsiForm = FungsiForm_TAMBAH Then

            'NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_") + 1
            'NomorID = AmbilNomorIdTerakhir(DatabaseGeneral, "tbl_") + 1

        End If

        If FungsiForm = FungsiForm_EDIT Then

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            frm_TemplateModul.TampilkanData()  'Ini Harus Diganti dengan Modul Terkait.
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class