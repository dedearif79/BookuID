Imports System.Xml

Module X_mdl_DesainUmum

    'Pewarnaan :
    Public WarnaHitamSolid As Color = Color.Black
    Public WarnaAbuAbu As Color = Color.Gray
    Public WarnaBiruSolid As Color = Color.Blue
    Public WarnaBiruTerang As Color = Color.LightBlue
    Public WarnaMerahSolid As Color = Color.Red
    Public WarnaMerahTerang As Color = Color.Pink
    Public WarnaKuningSolid As Color = Color.Yellow
    Public WarnaKuningTerang As Color = Color.LightYellow
    Public WarnaHijauSolid As Color = Color.Green
    Public WarnaTegas As Color = WarnaHitamSolid
    Public WarnaPudar As Color = WarnaAbuAbu
    Public WarnaPeringatan As Color = WarnaMerahSolid
    Public WarnaBermasalahTerseleksi As Color = WarnaMerahTerang
    Public WarnaAlternatif_1 As Color = WarnaBiruSolid
    Public WarnaPutih As Color = Color.White
    Public WarnaPutihAsap As Color = Color.WhiteSmoke
    Public WarnaDasar As Color = WarnaPutihAsap

    Public Sub Style_SemuaForm(ByVal Formulir As Form)
        Formulir.BackColor = WarnaDasar
    End Sub

    Public Sub Style_HalamanModul(ByRef Halaman As Form)
        Style_SemuaForm(Halaman)
        Halaman.WindowState = FormWindowState.Normal
        Halaman.ImeMode = ImeMode.Disable
        Halaman.StartPosition = FormStartPosition.CenterParent
        Halaman.FormBorderStyle = FormBorderStyle.Sizable
        Halaman.Width = 1270
        Halaman.Height = 603
    End Sub

    'Style Form Input :
    Public Sub Style_FormInput(ByRef FormInput As Form)
        Style_SemuaForm(FormInput)
        FormInput.StartPosition = FormStartPosition.CenterScreen
        FormInput.FormBorderStyle = FormBorderStyle.FixedToolWindow
        FormInput.Opacity = Persen(93)
    End Sub


    'Kunci Ukuran Form :
    Public Sub KunciUkuranForm(ByVal Halaman As Form, ByVal LebarMinimum As Integer, ByVal TinggiMinimum As Integer)
        If Halaman.Width < LebarMinimum Then Halaman.Width = LebarMinimum
        If Halaman.Height < TinggiMinimum Then Halaman.Height = TinggiMinimum
    End Sub

    'Pengatur Warna Huruf Pada Cell Suatu Tabel :
    Public Sub PengaturWaranaCell_Huruf(Tabel As DataGridView, NamaKolom As String, IndeksBaris As Integer, Warna As Color)
        If IndeksBaris >= 0 AndAlso IndeksBaris < Tabel.Rows.Count Then
            Dim targetCell As DataGridViewCell = Tabel.Rows(IndeksBaris).Cells(NamaKolom)

            If targetCell IsNot Nothing Then
                targetCell.Style.ForeColor = Warna
            End If
        End If
    End Sub

    'Pengatur Warna Background Pada Cell Suatu Tabel :
    Public Sub PengaturWaranaCell_Background(Tabel As DataGridView, NamaKolom As String, IndeksBaris As Integer, Warna As Color)
        If IndeksBaris >= 0 AndAlso IndeksBaris < Tabel.Rows.Count Then
            Dim targetCell As DataGridViewCell = Tabel.Rows(IndeksBaris).Cells(NamaKolom)

            If targetCell IsNot Nothing Then
                targetCell.Style.BackColor = Warna
            End If
        End If
    End Sub


End Module
