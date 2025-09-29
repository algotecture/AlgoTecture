using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Application.UI;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;
using Telegram.Bot;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class ReservationControllerBase : BotController
{
    private readonly ReservationFlowService _flowService;
    private readonly ReservationUiBuilder _uiBuilder;

    protected ReservationControllerBase(ReservationFlowService flowService, ReservationUiBuilder uiBuilder)
    {
        _flowService = flowService;
        _uiBuilder = uiBuilder;
    }

    [Action]
    public async Task SelectRentalTime(BotSessionState state, TimeSelectionStage stage, DateTime? selectedDate)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        await DeletePreviousMessageIfNeeded(state, chatId.Value);

        string? userInputTime = null;
        if (selectedDate != null)
        {
            PushL("⏰ Choose rental start time or enter manually:");

            RowButton("▶️ +30m", Q(SetStartTimeQuick, state, TimeSpan.FromMinutes(30)));
            RowButton("▶️ +1h", Q(SetStartTimeQuick, state, TimeSpan.FromHours(1)));
            RowButton("▶️ +2h", Q(SetStartTimeQuick, state, TimeSpan.FromHours(2)));

            RowButton("⌨️ Enter manually (in HH:mm format e.g., 14:15)", Q(EnterStartTimeManual, state));

            await SendOrUpdate();
            
            // PushL("⏰ Enter the rental time in HH:mm format (e.g., 14:15)");
            // await Send();
            // userInputTime = await AwaitText(() => Send("⚠️ Timeout. Use /start to try again"));
        }

        _flowService.ApplyTimeSelection(state, stage, selectedDate, userInputTime);
        _flowService.ValidateRentalPeriod(state);

        // _uiBuilder.BuildSummaryButtons(
        //     this,
        //     state,
        //     (s, stage) => (Func<Task>)(() => SelectDate(s, stage)),
        //   //  s => (Func<Task>)(() => PressMakeAReservation(s)),
        //   //  s => (Func<Task>)(() => EnterAddress(s))
        // );

        if (string.IsNullOrEmpty(userInputTime))
            await SendOrUpdate();
        else
            await Send("Reservation");
    }

    [Action]
    public async Task SelectDate(BotSessionState state, TimeSelectionStage stage)
    {
        await ShowCalendar("", state, false, stage);
    }

    [Action]
    public async Task ShowCalendar(string navigationState, BotSessionState state, bool isNavigation, TimeSelectionStage stage)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var now = DateTime.UtcNow;

        var builder = new CalendarMessageBuilder()
            .Year(now.Year).Month(now.Month)
            .Depth(CalendarDepth.Days)
            .SetState(navigationState)
            .OnNavigatePath(s => Q(ShowCalendar, s, state, true, stage))
            .OnSelectPath(date => Q(SelectRentalTime, state, stage, date));

        if (!isNavigation)
            builder = builder.SkipDay(d => d.Day < now.Day);

        builder.Build(Message, new PagingService());

        RowButton("🔙 Go Back", Q(SelectRentalTime, state, TimeSelectionStage.None, null!));
        PushL("📅 Pick the date");
        await SendOrUpdate();
    }
    
    [Action]
    private Task SetStartTimeQuick(BotSessionState state, TimeSpan offset)
    {
        state.PendingStartRentUtc = DateTime.UtcNow.Add(offset);
        return SelectRentalTime(state, TimeSelectionStage.End, null);
    }

    [Action]
    private async Task EnterStartTimeManual(BotSessionState state)
    {
        PushL("⌨️ Enter time in HH:mm format (e.g., 14:15)");
        var input = await AwaitText();
        state.PendingStartRentUtc = DateTimeParser.GetDateTimeUtc(DateTime.UtcNow, input);
        await SelectRentalTime(state, TimeSelectionStage.End, null);
    }

    protected async Task DeletePreviousMessageIfNeeded(BotSessionState state, long chatId)
    {
        if (state.LastMessageId == default) return;

        await Client.DeleteMessageAsync(chatId, state.LastMessageId);
        state.LastMessageId = default;
    }
}