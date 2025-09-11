using Algotecture.TelegramBot.Models;

namespace Algotecture.TelegramBot.Controllers.Interfaces;

public interface IParkingController
{
    Task PressToMainBookingPage(BotState botState);

    Task PressToParkingButton(TelegramToAddressModel telegramToAddressModel, BotState botState);

    Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime);
}