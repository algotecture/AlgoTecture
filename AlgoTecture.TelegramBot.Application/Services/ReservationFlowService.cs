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
        var now = DateTime.UtcNow;

        if (state.CurrentReservation.PendingEndRentUtc != null && state.CurrentReservation.PendingEndRentUtc <= now)
            state.CurrentReservation.PendingEndRentUtc = null;

        if (state.CurrentReservation.PendingStartRentUtc != null && state.CurrentReservation.PendingStartRentUtc <= now)
            state.CurrentReservation.PendingStartRentUtc = null;

        if (state.CurrentReservation.PendingStartRentUtc != null && state.CurrentReservation.PendingEndRentUtc!= null &&
            state.CurrentReservation.PendingEndRentUtc <= state.CurrentReservation.PendingStartRentUtc)
            state.CurrentReservation.PendingEndRentUtc = null;
    }
}