using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Space;
using AlgoTecture.Libraries.UtilizationType;
using AlgoTecture.Data.Persistence;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;
using Microsoft.AspNetCore;

BotfProgram.StartBot(args, false, onConfigure: (service, cfg) =>
{
    service.UseUtilizationTypeLibrary();
    service.UseSpaceLibrary();
    service.UsePersistenceLibrary();
    service.AddTransient<ITelegramUserInfoService, TelegramUserInfoService>();
    service.AddTransient<ITelegramToAddressResolver, TelegramToAddressResolver>();
    service.AddApplication<GeoAdminSearcherModule>();
});