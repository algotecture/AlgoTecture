using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Domain;

namespace AlgoTecture.TelegramBot.Application.Services;

public interface IReservationFlowService
{
    void ValidateRentalPeriod(BotSessionState state);
}

public class ReservationFlowService : IReservationFlowService
{
    public void ValidateRentalPeriod(BotSessionState state)
    {
        var now = DateTimeOffset.UtcNow;

        if (state.CurrentReservation.PendingEndRentLocal != null && state.CurrentReservation.PendingEndRentLocal <= now)
            state.CurrentReservation.PendingEndRentLocal = null;

        if (state.CurrentReservation.PendingStartRentLocal != null && state.CurrentReservation.PendingStartRentLocal <= now)
            state.CurrentReservation.PendingStartRentLocal = null;

        if (state.CurrentReservation.PendingStartRentLocal != null && state.CurrentReservation.PendingEndRentLocal!= null &&
            state.CurrentReservation.PendingEndRentLocal <= state.CurrentReservation.PendingStartRentLocal)
            state.CurrentReservation.PendingEndRentLocal = null;
    }
}