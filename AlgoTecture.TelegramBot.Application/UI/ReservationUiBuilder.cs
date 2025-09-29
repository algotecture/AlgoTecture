using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Application.UI;

public class ReservationUiBuilder
{
    private readonly TimeZoneInfo _zurichTz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zurich");
    public void BuildSummaryButtons(
        BotController ctrl,
        BotSessionState state,
        Func<BotSessionState, TimeSelectionStage, Delegate> dateAction,
        Func<BotSessionState, Delegate> reservationAction,
        Func<object> backAction)
    {
        DateTime? startForUser = state.CurrentReservation.PendingStartRentUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(state.CurrentReservation.PendingStartRentUtc.Value, _zurichTz)
            : null;

        DateTime? endForUser = state.CurrentReservation.PendingEndRentUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(state.CurrentReservation.PendingEndRentUtc.Value, _zurichTz)
            : null;

        ctrl.RowButton(
            state.CurrentReservation.PendingStartRentUtc != null ? $"{startForUser:dddd, MMMM dd yyyy HH:mm}" : "Rental start time",
            ctrl.Q(dateAction(state, TimeSelectionStage.Start)));

        ctrl.RowButton(
            state.CurrentReservation.PendingEndRentUtc != null ? $"{endForUser:dddd, MMMM dd yyyy HH:mm}" : "Rental end time",
            ctrl.Q(dateAction(state, TimeSelectionStage.End)));

        if (state.CurrentReservation.PendingStartRentUtc != null && state.CurrentReservation.PendingEndRentUtc != null && state.CurrentReservation.SelectedSpaceId != default)
        {
            // var spec = new PriceSpecification { PricePerTime = "2", PriceCurrency = "CHF" };
            // var total = _priceCalculator.CalculateTotalPriceToReservation(
            //     state.RentStartUtc.Value,
            //     state.RentEndUtc.Value,
            //     UnitOfDateTime.Hour,
            //     spec.PricePerTime);

            ctrl.RowButton(
                $"✅ Make a reservation!",
                ctrl.Q(reservationAction(state)));
        }

        ctrl.RowButton("🔙 Go Back", ctrl.Q(backAction));
    }
}