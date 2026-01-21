using BookuRemoteAndroid.Services;
using Microsoft.Extensions.Logging;

namespace BookuRemoteAndroid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Services
        builder.Services.AddSingleton<DiscoveryService>();
        builder.Services.AddSingleton<NetworkService>();
        builder.Services.AddSingleton<ProtocolService>();
        builder.Services.AddSingleton<SessionService>();

        // Register ViewModels (jika pakai MVVM)
        // builder.Services.AddTransient<MainViewModel>();

        // Register Pages
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<Views.ViewerPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
