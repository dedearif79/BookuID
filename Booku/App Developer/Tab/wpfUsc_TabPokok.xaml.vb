Imports System.Windows

Public Class wpfUsc_TabPokok


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub



    Sub New()
        InitializeComponent()
        'ChatGPT
        usc_ChatGPT = New wpfUsc_WebBrowser
        usc_ChatGPT.AlamatURL = urlChatGPT
        pnl_ChatGPT.Children.Add(usc_ChatGPT)
        'Telegram
        usc_Telegram = New wpfUsc_WebBrowser
        usc_Telegram.AlamatURL = urlTelegram
        pnl_Telegram.Children.Add(usc_Telegram)
        'WhatsApp
        usc_WhatsApp = New wpfUsc_WebBrowser
        usc_WhatsApp.AlamatURL = urlWhatsApp
        pnl_WhatsApp.Children.Add(usc_WhatsApp)
    End Sub

End Class
