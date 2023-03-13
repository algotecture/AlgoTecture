using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Spaces;
using AlgoTecture.Libraries.UtilizationTypes;
using AlgoTecture.Data.Persistence;
using AlgoTecture.TelegramBot.Controllers;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;
using Microsoft.AspNetCore;

BotfProgram.StartBot(args, false, onConfigure: (service, cfg) =>
{
    service.UseUtilizationTypeLibrary();
    service.UseSpaceLibrary();
    service.UsePersistenceLibrary();
    service.UseGeoAdminSearchLibrary();
    service.AddTransient<ITelegramUserInfoService, TelegramUserInfoService>();
    service.AddTransient<ITelegramToAddressResolver, TelegramToAddressResolver>();
    service.AddTransient<IMainController, MainController>();
    service.AddTransient<IBoatController, BoatController>();
});