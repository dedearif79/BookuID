Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports bcomm

''' <summary>
''' WPF Dialog untuk memilih folder (pengganti FolderBrowserDialog WinForms)
''' </summary>
Public Class wpfWin_SelectFolder

    ' === PUBLIC PROPERTIES (RETURN VALUE) ===
    Public Property SelectedPath As String = ""
    Public Property HasResult As Boolean = False

    ' === PRIVATE VARIABLES ===
    Private RootFolders As New ObservableCollection(Of FolderItem)


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Height = 500
        Width = 450
        SizeToContent = SizeToContent.Manual

        ' Register handler untuk TreeViewItem.Expanded event
        treeViewFolder.AddHandler(TreeViewItem.ExpandedEvent, New RoutedEventHandler(AddressOf TreeViewItem_Expanded))
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LoadDrives()
        treeViewFolder.ItemsSource = RootFolders

        ' Jika ada path awal, set ke textbox
        If SelectedPath <> "" AndAlso Directory.Exists(SelectedPath) Then
            txt_Path.Text = SelectedPath
            ExpandToPath(SelectedPath)
        End If

        txt_Path.Focus()
    End Sub


    ''' <summary>
    ''' Load semua drive yang tersedia
    ''' </summary>
    Private Sub LoadDrives()
        RootFolders.Clear()
        Try
            For Each drive In DriveInfo.GetDrives()
                If drive.IsReady Then
                    Dim folderItem As New FolderItem With {
                        .Name = drive.Name,
                        .FullPath = drive.RootDirectory.FullName,
                        .IsExpanded = False
                    }
                    ' Tambahkan dummy child untuk menunjukkan expand arrow
                    folderItem.SubFolders.Add(New FolderItem With {.Name = "Loading..."})
                    RootFolders.Add(folderItem)
                End If
            Next
        Catch ex As Exception
            ' Ignore drive access errors
        End Try
    End Sub


    ''' <summary>
    ''' Load subfolder secara lazy (saat expand)
    ''' </summary>
    Private Sub LoadSubFolders(parentItem As FolderItem)
        If parentItem.IsLoaded Then Return

        parentItem.SubFolders.Clear()
        Try
            Dim dirInfo As New DirectoryInfo(parentItem.FullPath)
            For Each subDir In dirInfo.GetDirectories()
                ' Skip hidden dan system folders
                If (subDir.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                If (subDir.Attributes And FileAttributes.System) = FileAttributes.System Then Continue For

                Dim subItem As New FolderItem With {
                    .Name = subDir.Name,
                    .FullPath = subDir.FullName,
                    .IsExpanded = False
                }
                ' Cek apakah ada subfolder
                Try
                    If subDir.GetDirectories().Length > 0 Then
                        subItem.SubFolders.Add(New FolderItem With {.Name = "Loading..."})
                    End If
                Catch
                    ' Ignore access denied
                End Try
                parentItem.SubFolders.Add(subItem)
            Next
        Catch ex As Exception
            ' Ignore folder access errors
        End Try

        parentItem.IsLoaded = True
    End Sub


    ''' <summary>
    ''' Handler saat TreeViewItem di-expand
    ''' </summary>
    Private Sub TreeViewItem_Expanded(sender As Object, e As RoutedEventArgs)
        Dim treeViewItem = TryCast(e.OriginalSource, TreeViewItem)
        If treeViewItem Is Nothing Then Return

        Dim folderItem = TryCast(treeViewItem.Header, FolderItem)
        If folderItem Is Nothing Then Return

        LoadSubFolders(folderItem)
    End Sub


    ''' <summary>
    ''' Handler saat item TreeView dipilih
    ''' </summary>
    Private Sub treeViewFolder_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object)) Handles treeViewFolder.SelectedItemChanged
        Dim selectedItem = TryCast(treeViewFolder.SelectedItem, FolderItem)
        If selectedItem IsNot Nothing Then
            txt_Path.Text = selectedItem.FullPath
        End If
    End Sub


    ''' <summary>
    ''' Expand TreeView ke path tertentu
    ''' </summary>
    Private Sub ExpandToPath(path As String)
        ' Implementasi sederhana - tidak expand otomatis ke path
        ' User bisa navigate manual atau ketik path langsung
    End Sub


    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click
        Dim inputPath = txt_Path.Text.Trim()

        If inputPath = "" Then
            Pesan_Peringatan("Silakan pilih atau masukkan path folder.")
            txt_Path.Focus()
            Return
        End If

        If Not Directory.Exists(inputPath) Then
            Pesan_Peringatan("Folder tidak ditemukan: " & inputPath)
            txt_Path.Focus()
            Return
        End If

        SelectedPath = inputPath
        HasResult = True
        Me.Close()
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        SelectedPath = ""
        HasResult = False
        Me.Close()
    End Sub


    ''' <summary>
    ''' Reset form untuk penggunaan ulang
    ''' </summary>
    Public Sub ResetForm()
        SelectedPath = ""
        HasResult = False
        txt_Path.Text = ""
        RootFolders.Clear()
    End Sub

End Class


''' <summary>
''' Class untuk item folder di TreeView
''' </summary>
Public Class FolderItem
    Public Property Name As String
    Public Property FullPath As String
    Public Property IsExpanded As Boolean
    Public Property IsLoaded As Boolean = False
    Public Property SubFolders As New ObservableCollection(Of FolderItem)
End Class
