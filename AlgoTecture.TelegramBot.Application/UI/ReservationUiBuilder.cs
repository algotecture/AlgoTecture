using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Application.UI;

public class ReservationUiBuilder
{
    public void BuildSummaryButtons(
        BotController ctrl,
        BotSessionState state,
        Func<BotSessionState, TimeSelectionStage, Delegate> dateAction,
        Func<BotSessionState, Delegate> reservationAction,
        Func<BotSessionState, Delegate> backAction)
    {
        var startForUser = state.PendingStartRentUtc?.AddHours(2); // demo shift
        var endForUser = state.PendingEndRentUtc?.AddHours(2);

        ctrl.RowButton(
            state.PendingStartRentUtc != null ? $"{startForUser:dddd, MMMM dd yyyy HH:mm}" : "Rental start time",
            ctrl.Q(dateAction(state, TimeSelectionStage.Start)));

        ctrl.RowButton(
            state.PendingEndRentUtc != null ? $"{endForUser:dddd, MMMM dd yyyy HH:mm}" : "Rental end time",
            ctrl.Q(dateAction(state, TimeSelectionStage.End)));

        if (state.PendingStartRentUtc != null && state.PendingEndRentUtc != null && state.SelectedSpaceId != default)
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

        ctrl.RowButton("🔙 Go Back", ctrl.Q(backAction(state)));
    }
}