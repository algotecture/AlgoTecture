using Algotecture.Common;
using Algotecture.Data.Persistence;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations;
using Algotecture.Libraries.Spaces;
using Algotecture.Libraries.UtilizationTypes;
using Algotecture.TelegramBot.Controllers;
using Algotecture.TelegramBot.Controllers.Interfaces;
using Algotecture.TelegramBot.Extensions;
using Algotecture.TelegramBot.Implementations;
using Algotecture.TelegramBot.Interfaces;
using Deployf.Botf;
using Serilog;

namespace Algotecture.TelegramBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webAppBuilder = WebAppBuilder.CreateWebApplicationBuilder(args);

        try
        {
            var pathToLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "algotecture", "log", "telegram-bot", "serilog-log-.json");

            webAppBuilder.Host.UseSerilog((context, _, configuration) =>
            {
                var env = context.HostingEnvironment;

                configuration
                    .ReadFrom.Configuration(webAppBuilder.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .WriteTo.File(pathToLog, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 104857600, retainedFileCountLimit: 31);
            });
            webAppBuilder = BotFExtensions.ConfigureBot(args, webAppBuilder);

            webAppBuilder.Services.UseUtilizationTypeLibrary();
            webAppBuilder.Services.UseSpaceLibrary();
            webAppBuilder.Services.UsePersistenceLibrary();
            webAppBuilder.Services.UseGeoAdminSearchLibrary();
            webAppBuilder.Services.UseReservationLibrary();
            webAppBuilder.Services.UsePriceSpecificationLibrary();
            webAppBuilder.Services.AddTransient<ITelegramUserInfoService, TelegramUserInfoService>();
            webAppBuilder.Services.AddTransient<ITelegramToAddressResolver, TelegramToAddressResolver>();
            webAppBuilder.Services.AddTransient<IMainController, MainController>();
            webAppBuilder.Services.AddTransient<IBoatController, BoatController>();
            webAppBuilder.Services.AddTransient<IParkingController, ParkingController>();

            var webApp = webAppBuilder.Build();

            webApp.UseBotf();

            await webApp.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}