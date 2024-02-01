using Microsoft.Extensions.Logging;
using WerfLogApp.ViewModels;

namespace WerfLogApp
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
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<WerfViewModel>();

            builder.Services.AddTransient<NotitiePage>();
            builder.Services.AddTransient<NotitieViewModel>();
            return builder.Build();
        }
    }
}
