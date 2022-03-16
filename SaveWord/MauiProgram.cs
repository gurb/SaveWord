using Microsoft.AspNetCore.Components.WebView.Maui;
using SaveWord.Data;
using SaveWord;

#if WINDOWS
#endif

namespace SaveWord;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .RegisterBlazorMauiWebView()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddBlazorWebView();
        builder.Services.AddSingleton(new WordService("words.db"));

        return builder.Build();
    }
}
