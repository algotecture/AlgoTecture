using AlgoTecture.Common;
using AlgoTecture.Data.Persistence;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Core.Repositories;
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
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Serilog;

namespace AlgoTecture.TelegramBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webAppBuilder = WebAppBuilder.CreateWebApplicationBuilder(args);
        webAppBuilder.Services.Configure<DeepSeekConfig>(
            webAppBuilder.Configuration.GetSection("DeepSeek"));

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
            webAppBuilder.Services.AddTransient<ICityParkingController, CityParkingController>();
            webAppBuilder.Services.AddTransient<IReservationService, ReservationService>();
            
            webAppBuilder.Services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<DeepSeekConfig>>().Value;
                return new DeepSeekService(config.ApiKey, config.BaseUrl);
            });

            webAppBuilder.Services.AddTransient<IntentRecognitionService>();
            webAppBuilder.Services.AddTransient<BookingActionService>();

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