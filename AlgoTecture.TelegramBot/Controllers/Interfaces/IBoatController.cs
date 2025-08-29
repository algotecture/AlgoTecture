using Algotecture.TelegramBot.Models;

namespace Algotecture.TelegramBot.Controllers.Interfaces;

public interface IBoatController
{
      Task PressToMainBookingPage(BotState botState);

      Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime);
}