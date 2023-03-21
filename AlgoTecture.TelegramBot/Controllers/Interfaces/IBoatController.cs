using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IBoatController
{
      Task PressToMainBookingPage(BotState botState);
}