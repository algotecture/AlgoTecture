using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Domain;

namespace AlgoTecture.TelegramBot.Application.Services;

public interface IReservationFlowService
{
    void ApplyTimeSelection(BotSessionState state, TimeSelectionStage stage, DateTime? selectedDate, string? timeText);
}

public class ReservationFlowService : IReservationFlowService
{
    public void ApplyTimeSelection(BotSessionState state, TimeSelectionStage stage, DateTime? selectedDate,
        string? timeText)
    {
        if (selectedDate == null || string.IsNullOrWhiteSpace(timeText)) return;

        var parsed = DateTimeParser.GetDateTimeUtc(selectedDate, timeText);

        if (stage == TimeSelectionStage.Start)
            state.PendingStartRentUtc = parsed;
        else if (stage == TimeSelectionStage.End)
            state.PendingEndRentUtc = parsed;
    }

    public void ValidateRentalPeriod(BotSessionState state)
    {
        var now = DateTime.UtcNow;

        if (state.PendingEndRentUtc != null && state.PendingEndRentUtc <= now)
            state.PendingEndRentUtc = null;

        if (state.PendingStartRentUtc != null && state.PendingStartRentUtc <= now)
            state.PendingStartRentUtc = null;

        if (state.PendingStartRentUtc != null && state.PendingEndRentUtc!= null &&
            state.PendingEndRentUtc <= state.PendingStartRentUtc)
            state.PendingEndRentUtc = null;
    }
}