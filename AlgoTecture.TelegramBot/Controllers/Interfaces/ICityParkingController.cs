using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface ICityParkingController
{
    Task PressToMainBookingPage(BotState botState);

    Task PressToParkingButton(TelegramToAddressModel telegramToAddressModel, BotState botState);

    Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime);
}