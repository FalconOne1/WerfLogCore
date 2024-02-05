using Microsoft.Extensions.Logging;
using WerfLogApp.ViewModels;
using WerfLogBl.AutoMapperProfiles;
using WerfLogBl.Interfaces;
using WerfLogBl.Managers;
using WerfLogDal;
using WerfLogDal.Interfaces; 
using WerfLogDal.Repositories;

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
            //test automapper
            builder.Services.AddAutoMapper(typeof(DtoMappers).Assembly); //assembly = verzameling van DTOmappers

            builder.Services.AddSingleton<DbContext>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<WerfViewModel>();

            builder.Services.AddSingleton<NotitiePage>();
            builder.Services.AddSingleton<NotitieViewModel>();


            // Configureer je repositories
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IWerfRepository, WerfRepository>();
            builder.Services.AddTransient<INotitieRepository, NotitieRepository>();
            builder.Services.AddTransient<ITijdregistratieRepository, TijdregistratieRepository>();
            builder.Services.AddTransient<IProjectOverlegRepository, ProjectoverlegRepository>();

            builder.Services.AddTransient<IWerfManager, WerfManager>();
            builder.Services.AddTransient<INotitieManager, NotitieManager>();

            return builder.Build();
        }
    }
}
