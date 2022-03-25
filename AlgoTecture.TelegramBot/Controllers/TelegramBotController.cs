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
        RowButton("Try to find!", Q(PressTryToFindButton));
    }

    [Action]
    private async Task PressTryToFindButton()
    {
        PushL("Enter the address or part of the address");
        await Send(); 
        var term =  await AwaitText();

        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            RowButton(label.label, Q(PressAddressButton));
        }
        await Send("You won");
    }

    [Action]
    private async Task PressAddressButton()
    {
        PushL("Have a good day");  
        await Send();
    }
}