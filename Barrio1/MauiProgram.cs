using Barrio1.Models;
using Barrio1.Services;
using Barrio1.ViewModels;
using Barrio1.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Bundled.Shared;
using Plugin.Firebase.Crashlytics;
using Microsoft.Maui.LifecycleEvents;


#if ANDROID
using Plugin.Firebase.Bundled.Platforms.Android;
#endif


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
            builder.Services.AddSingleton<LoginViewModel>();

            // Add Views
            builder.Services.AddSingleton<VentasListingPage>();
            builder.Services.AddTransient<AddVentaPage>();
            builder.Services.AddTransient<UpdateVentaPage>();
            builder.Services.AddSingleton<LoginView>();

            // Add Data Service
            builder.Services.AddSingleton<IDataServices, DataServices>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        //private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        //{
        //    builder.ConfigureLifecycleEvents(events =>
        //    {
        //        events.AddAndroid(Android => Android.OnCreate((activity, _) =>
        //            CrossFirebase.Initialize(activity, CreateCrossFirebaseSettings())));
        //        CrossFirebaseCrashlytics.Current.SetCrashlyticsCollectionEnabled(true);
        //    });

        //    builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
        //    return builder;
        //}

        //private static CrossFirebaseSettings CreateCrossFirebaseSettings()
        //{
        //    return new CrossFirebaseSettings(
        //        isAuthEnabled: true,
        //        isCloudMessagingEnabled: true,
        //        isAnalyticsEnabled: true
        //        );
        //}
    }
}