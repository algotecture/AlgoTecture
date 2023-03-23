namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IMainController
{
    Task Start();

    Task PressToRentButton();
    
    Task PressToFindReservationsButton();
}