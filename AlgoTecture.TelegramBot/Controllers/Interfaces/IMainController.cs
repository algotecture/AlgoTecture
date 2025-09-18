using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IMainController
{
    Task Start();

    //Task PressToRentButton();
    
    Task PressToFindReservationsButton();
    
    Task EnterAddress(BotState botState);

    Task PressAddressToRentButton(TelegramToAddressModel telegramToAddressModel, BotState botState);
}