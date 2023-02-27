namespace AlgoTecture.TelegramBot.Controllers.Interfaces;

public interface IBoatController
{
      Task PressToMainBookingPage(int utilizationTypeId, int messageId);
}