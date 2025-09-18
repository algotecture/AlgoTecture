using AlgoTecturer.TelegramBot.Contracts.Dto;

namespace AlgoTecture.TelegramBot.Api.Interfaces;

public interface IMainController
{
    Task Start();

    Task PressToRentButton();
    
    Task PressToFindReservationsButton();
    
    Task EnterAddress(BotState botState); 
}