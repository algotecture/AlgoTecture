using AlgoTecture.Libraries.GeoAdminSearch;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot;

public class Program
{
    public static void Main(string[] args)
    {
        BotfProgram.StartBot(args, onConfigure: (service, cfg) =>
        {
            service.UseGeoAdminLibrary();
        });
    }
}