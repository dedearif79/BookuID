Public Module mdlPub_VariabelUmum

    Public Kosongan As String = String.Empty
    Public Enter1Baris As String = Environment.NewLine
    Public Enter2Baris As String = Environment.NewLine & Environment.NewLine
    Public Enter3Baris As String = Environment.NewLine & Environment.NewLine & Environment.NewLine
    Public Enter4Baris As String = Environment.NewLine & Environment.NewLine & Environment.NewLine & Environment.NewLine

    Public Spasi1 As String = " "
    Public Spasi2 As String = "  "
    Public Spasi3 As String = "   "
    Public Spasi4 As String = "    "
    Public Spasi5 As String = "     "

    Public HeaderConfig1 = "!!!...PERHATIAN...!!!"
    Public HeaderConfig2 = "MERUBAH DAN MENGHAPUS FILE INI DAPAT MENGAKIBATKAN KEGAGALAN SISTEM."
    Public FooterConfig1 = "SISTEM AKUNTANSI TERPADU"
    Public FooterConfig2 = "booku.id"

    Public HeaderConfig = HeaderConfig1 & Enter1Baris & HeaderConfig2 & Enter2Baris
    Public FooterConfig = Enter2Baris & Enter1Baris & FooterConfig1 & Enter1Baris & FooterConfig2

    Public NamaFileDataKoneksi As String = "0001.conf"
    Public NamaFileRegistrasiPerangkat As String = "0002.conf"
    Public NamaFileRegistrasiPerangkat_Backup As String = "0002b.conf"
    Public NamaFileVersiDanApdetAplikasi As String = "0003.conf"




End Module
