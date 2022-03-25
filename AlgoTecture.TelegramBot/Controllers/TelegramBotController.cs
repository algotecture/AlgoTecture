using AlgoTecture.Libraries.GeoAdminSearch;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Controllers;

public class TelegramBotController : BotController
{
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    readonly BotfOptions _options;

    public TelegramBotController(IGeoAdminSearcher geoAdminSearcher)
    {
        _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
    }
    [Action("/start", "start the bot")]
    public async Task Start()
    {
        PushL("Hi! This bot will allow you to quickly rent a parking space (test mode)");
        RowButton("Try to find!", Q(PressTryToFind));
    }

    [Action]
    private async Task PressTryToFind()
    {
        PushL("Start typing the address");
        await Send(); 
        var term =  await AwaitText();
        
        
        // return new ActionResult<IEnumerable<Attrs>>(labels);
        await Send("Your text: " + term);
    }
}