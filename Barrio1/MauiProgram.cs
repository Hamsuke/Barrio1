using Barrio1.Models;
using Barrio1.Services;
using Barrio1.ViewModels;
using Barrio1.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Supabase;

namespace Barrio1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            //Servicios integrados
            //Supabase
            var url = AppConfig.supaUrl;
            var key = AppConfig.supaKey;
            builder.Services.AddSingleton(provider => new Supabase.Client(url, key));

            // Add ViewModels
            builder.Services.AddSingleton<VentasListingViewModel>();
            builder.Services.AddTransient<AddVentaViewModel>();
            builder.Services.AddTransient<UpdateVentaViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

            // Add Views
            builder.Services.AddSingleton<VentasListingPage>();
            builder.Services.AddTransient<AddVentaPage>();
            builder.Services.AddTransient<UpdateVentaPage>();
            builder.Services.AddTransient<LoginView>();

            // Add Data Service
            builder.Services.AddSingleton<IDataServices, DataServices>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}