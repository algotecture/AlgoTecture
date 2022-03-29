using AlgoTecture.EfCli;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Persistence;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot;

public class Program
{
    public static void Main(string[] args)
    {
        BotfProgram.StartBot(args, onConfigure: (service, cfg) =>
        {
            service.UseEfCliLibrary();
            service.UsePersistenceLibrary();
            service.AddTransient<ITelegramUserInfoService, TelegramUserInfoService>();
            service.UseGeoAdminLibrary();
        });
    }
}