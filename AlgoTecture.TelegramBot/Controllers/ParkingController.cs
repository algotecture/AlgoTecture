using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Controllers;

public class ParkingController : BotController, IParkingController
{
    private readonly IServiceProvider _serviceProvider;

    public ParkingController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [Action]
    public async Task PressToMainBookingPage(BotState botState)
    {
        //only for demo
        const int parkingTargetOfSpaceId = 11;
        if (botState.UtilizationTypeId != parkingTargetOfSpaceId)
        {
            return;
        }
        
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();

        RowButton("Enter address", Q(mainControllerService.EnterAddress, new BotState{UtilizationTypeId = 11}));
        RowButton("Go Back", Q(mainControllerService.PressToRentButton));
        
        PushL("Enter an address to search for the parking space");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToParkingButton(TelegramToAddressModel telegramToAddressModel, BotState botState)
    {
        //for example
        var urlToAddressProperties = $"https://www.google.com/maps/search/?api=1&query={telegramToAddressModel.latitude}-{telegramToAddressModel.longitude}";
        RowButton("Look on the map", urlToAddressProperties);
        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        PushL("Details");
        await SendOrUpdate();
    }
    
    
}