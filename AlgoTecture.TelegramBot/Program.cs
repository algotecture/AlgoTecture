using AlgoTecture.EfCli;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Persistence;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;

BotfProgram.StartBot(args, false, onConfigure: (service, cfg) =>
{
    service.UseEfCliLibrary();
    service.UsePersistenceLibrary();
    service.AddTransient<ITelegramUserInfoService, TelegramUserInfoService>();
    service.AddTransient<ITelegramToAddressResolver, TelegramToAddressResolver>();
    service.AddApplication<GeoAdminSearcherModule>();
});