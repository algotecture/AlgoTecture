using AlgoTecture.Common;
using AlgoTecture.Data.Persistence;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.PriceSpecifications;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Spaces;
using AlgoTecture.Libraries.UtilizationTypes;
using AlgoTecture.TelegramBot.Controllers;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Extensions;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;
using Serilog;

namespace AlgoTecture.TelegramBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webAppBuilder = WebAppBuilder.CreateWebApplicationBuilder(args);

        try
        {
            var pathToLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "algotecture", "log", "telegram-bot", "serilog-log-.json");

            webAppBuilder.Host.UseSerilog((context, services, configuration) =>
            {
                var env = context.HostingEnvironment;

                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("App", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .WriteTo.Seq("http://localhost:5341")
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