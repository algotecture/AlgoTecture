using AlgoTecture.TelegramBot.Models;

namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IParkingController
{

    Task EnterAddress(BotState botState);
    //Task PressToMainBookingPage(BotState botState, RentTimeState rentTimeState, DateTime? dateTime);

    Task PressToParkingButton(TelegramToAddressModel telegramToAddressModel, BotState botState);

    Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime);

    Task PressToStartParkingButton(BotState botState);
}