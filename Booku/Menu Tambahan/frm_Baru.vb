Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Windows.Forms.Integration

Public Class frm_Baru

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        ProsesLoadingForm = False

    End Sub


End Class