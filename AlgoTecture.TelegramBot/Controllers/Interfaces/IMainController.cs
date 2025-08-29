using Algotecture.TelegramBot.Models;

namespace Algotecture.TelegramBot.Controllers.Interfaces;

public interface IMainController
{
    Task Start();

    Task PressToRentButton();
    
    Task PressToFindReservationsButton();
    
    Task EnterAddress(BotState botState);
}