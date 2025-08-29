using Deployf.Botf;

namespace Algotecture.TelegramBot.Extensions;

internal class BotFExtensions : BotController
{
    public static WebApplicationBuilder ConfigureBot(string[] args, WebApplicationBuilder builder)
    {
        var options = new BotfOptions();
        
        var str = builder.Configuration["botf"];
        
        options = ConnectionString.Parse(str ?? throw new InvalidOperationException());
        if (options == null)
            throw new BotfException(
                "Configuration is not passed. Check the appsettings*.json.\nThere must be configuration object like `{ \"bot\": { \"Token\": \"BotToken...\" } " +
                "}`\nOr connection string(in root) like `{ \"botf\": \"bot_token?key=value\" }`");
        
        builder.Services.AddBotf(options);
        builder.Services.AddHttpClient();
        
        return builder;
    }
}