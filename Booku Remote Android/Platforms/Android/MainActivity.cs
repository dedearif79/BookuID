using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;

namespace BookuRemoteAndroid;

[Activity(Theme = "@style/Maui.SplashTheme",
          MainLauncher = true,
          LaunchMode = LaunchMode.SingleTask,
          ConfigurationChanges = ConfigChanges.ScreenSize |
                                 ConfigChanges.Orientation |
                                 ConfigChanges.UiMode |
                                 ConfigChanges.ScreenLayout |
                                 ConfigChanges.SmallestScreenSize |
                                 ConfigChanges.Density,
          ScreenOrientation = ScreenOrientation.Landscape)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Keep screen on during remote session
        Window?.AddFlags(WindowManagerFlags.KeepScreenOn);
    }

    protected override void OnResume()
    {
        base.OnResume();

        // Hide system UI for immersive experience (optional)
        // HideSystemUI();
    }

    private void HideSystemUI()
    {
        if (Window == null) return;

        // Use WindowInsetsController for Android 11+ (API 30+)
        var windowInsetsController = WindowCompat.GetInsetsController(Window, Window.DecorView);
        if (windowInsetsController != null)
        {
            windowInsetsController.Hide(WindowInsetsCompat.Type.SystemBars());
            windowInsetsController.SystemBarsBehavior =
                WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
        }

        // Set layout flags for edge-to-edge
        WindowCompat.SetDecorFitsSystemWindows(Window, false);
    }
}
