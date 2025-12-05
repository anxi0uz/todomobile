using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using todomobile.View;
using System.Net.Http;
using todomobile.Services;
using todomobile.ViewModels;

namespace todomobile
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AuthorizationMessageHandler>();
            builder.Services.AddHttpClient<HttpClient>("Api",opt=>opt.BaseAddress = new Uri("https://localhost:7068")).AddHttpMessageHandler<AuthorizationMessageHandler>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<LoginViewModel>();
            return builder.Build();
        }
    }
}
