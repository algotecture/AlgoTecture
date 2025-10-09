using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IMainController
{
    Task Start();

    //Task PressToRentButton();
    
    Task PressToFindReservationsButton(BotState? botState = null);
    
    Task EnterAddress(BotState botState);

    Task PressAddressToRentButton(TelegramToAddressModel telegramToAddressModel, BotState botState);
}