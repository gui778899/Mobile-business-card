using Camera.MAUI;
using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using PWApp;
using PWApp.Data.Services;
using PWApp.Properties;
using PWApp.ViewModels;
using Sharpnado.Tabs;
using System.Reflection;
using ZXing.Net.Maui.Controls;

namespace pwapp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCameraView()
                .UseBarcodeReader()
                .UseSharpnadoTabs(loggerEnable: true)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "IconsFont");
                });
            RegisterSettings(builder);
            RegisterServices(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);


#if DEBUG
    		builder.Logging.AddDebug();
#endif
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
            return builder.Build();
        }
        private static void RegisterServices(MauiAppBuilder builder)
        {
            var settings = builder.Configuration.GetSection("FirebaseSettings").Get<FirebaseConfig>();
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = settings.apiKey,
                AuthDomain = settings.apiDomain,
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
               {
                new EmailProvider()
               }
            }));
            builder.Services.AddSingleton(new DataService());
        }
        private static void RegisterSettings(MauiAppBuilder builder)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("pwapp.appsettings.json");

            var config = new ConfigurationBuilder()
                         .AddJsonStream(stream)
                         .Build();
            builder.Configuration.AddConfiguration(config);
        }
        private static void RegisterViews(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CardsPage>();
        }
        private static void RegisterViewModels(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<NewCardPageViewModel>();
            builder.Services.AddTransient<ChangeCardPageViewModel>();
            builder.Services.AddTransient<QrCodePageViewModel>();
            builder.Services.AddTransient<CardsPageViewModel>();
        }
    }
}
