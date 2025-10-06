using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Application.UI;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;
using Telegram.Bot;

namespace AlgoTecture.TelegramBot.Api.Controllers.Base;

public abstract class ReservationControllerBase : BotController
{
    protected readonly IReservationFlowService _flowService;
    protected readonly ReservationUiBuilder _uiBuilder;

    protected ReservationControllerBase(
        IReservationFlowService flowService,
        ReservationUiBuilder uiBuilder)
    {
        _flowService = flowService;
        _uiBuilder = uiBuilder;
    }

    protected async Task DeletePreviousMessageIfNeeded(BotSessionState state, long chatId)
    {
        if (state.LastMessageId == default) return;
        await Client.DeleteMessageAsync(chatId, state.LastMessageId);
        state.LastMessageId = default;
    }

    protected void ApplyTimeSelection(BotSessionState state, TimeSelectionStage stage, DateTime? selectedDate, string? inputText)
    {
        _flowService.ApplyTimeSelection(state, stage, selectedDate, inputText);
        _flowService.ValidateRentalPeriod(state);
    }

    protected Task BuildCalendar(
        BotSessionState state,
        TimeSelectionStage stage,
        Func<DateTime, string> onSelect)
    {
        var now = DateTime.UtcNow;

        var builder = new CalendarMessageBuilder()
            .Year(now.Year).Month(now.Month)
            .Depth(CalendarDepth.Days)
            .OnSelectPath(date => onSelect(date));

        builder.Build(Message, new PagingService());
        return Task.CompletedTask;
    }
}