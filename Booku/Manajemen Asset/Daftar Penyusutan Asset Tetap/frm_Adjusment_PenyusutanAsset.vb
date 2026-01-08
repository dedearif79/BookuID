Imports System.Windows.Forms.Integration
Imports bcomm


Public Class frm_Adjusment_PenyusutanAsset


    Public JudulForm
    Public JalurMasuk

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Adjusment Penyusutan Asset"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_Adjusment_PenyusutanAsset
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_Adjusment_PenyusutanAsset = New wpfUsc_Adjusment_PenyusutanAsset
    End Sub

    Sub CekAdjusment()
        Inisialisasi()
        usc_Adjusment_PenyusutanAsset.EksekusiTampilanData = True
        usc_Adjusment_PenyusutanAsset.RefreshTampilanData()
    End Sub

    Sub RefreshForm()
        Dim frm As Form = Me.FindForm()
        If frm IsNot Nothing Then
            Dim stateAwal = frm.WindowState
            frm.WindowState = FormWindowState.Minimized
            Terabas()
            frm.WindowState = stateAwal
        End If
    End Sub


End Class