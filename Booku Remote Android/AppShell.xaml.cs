using BookuRemoteAndroid.Views;

namespace BookuRemoteAndroid;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes untuk navigation
        Routing.RegisterRoute(nameof(ViewerPage), typeof(ViewerPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
