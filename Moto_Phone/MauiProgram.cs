using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Moto_Phone.Services;
using Moto_Phone.ViewModels;
using Moto_Phone.Views;

namespace Moto_Phone
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<MotoApiService>();

            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<LogoutViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<AdDetailsPageViewModel>();
            builder.Services.AddSingleton<ListPageViewModel>();
            builder.Services.AddTransient<AddingAdViewModel>();
            builder.Services.AddSingleton<AddImagesViewModel>();
            builder.Services.AddSingleton<MyAdsViewModel>();
            builder.Services.AddSingleton<AdDetailsChangeViewModel>();
            builder.Services.AddSingleton<ListPageTempViewModel>();

            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LogoutPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<AdDetailsPage>();
            builder.Services.AddSingleton<ListPage>();
            builder.Services.AddTransient<AddingAd>();
            builder.Services.AddSingleton<AddImagesPage>();
            builder.Services.AddSingleton<MyAdsPage>();
            builder.Services.AddSingleton<AdDetailsChangePage>();
            builder.Services.AddSingleton<ListPageTemp>();

            return builder.Build();
        }
    }
}
