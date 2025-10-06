using AlgoTecture.TelegramBot.Api.Controllers.Base;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Application.UI;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : ReservationControllerBase
{
    private readonly IUserAuthenticationService _authService;

    public MainController(IUserAuthenticationService authService, IReservationFlowService flow, ReservationUiBuilder ui)
        : base(flow, ui)
    {
        _authService = authService;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var userFullName = Context.GetUserFullName();
        if (userId == null) return;

        var linkedUserId = await _authService.EnsureUserAuthenticatedAsync(
            userId.Value,
            chatId!.Value,
            userFullName,
            userName
        );
        if (linkedUserId == Guid.Empty) return;
        //Idustriestrasse 24 8305
        Thread.Sleep(100000);

        PushL("I am your parking 🅿️ assistant. I help you find and manage spots near you.");
        RowButton("🚗 reserve a parking", Q(StartParkingFlow));
        //RowButton("📅 Manage reservations", Q(PressToFindReservationsButton));
    }

    [Action]
    public async Task StartParkingFlow()
    {
        var state = new BotSessionState
            { CurrentReservation = new ParkingReservationDraft { SelectedSpaceTypeId = 1 } };

        RowButton("⏱️ when to park?", Q(SelectRentalTime, state, TimeSelectionStage.None, null!));
        //RowButton("📍 Where to park?", Q(_parkingController.PressToEnterTheStartEndTime()));

        RowButton("↩️ go back", Q(Start));

        await SendOrUpdate();
    }
    
    [Action]
    public async Task SelectRentalTime(BotSessionState state, TimeSelectionStage stage, DateTime? selectedDate)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        await DeletePreviousMessageIfNeeded(state, chatId.Value);

        ApplyTimeSelection(state, stage, selectedDate, null);

        PushL("⏰ Choose rental start time or enter manually:");
        
        await SendOrUpdate();
    }
    

    [On(Handle.Exception)]
    public void Exception(Exception ex)
    {
        //_logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.ChainTimeout)]
    void ChainTimeout(Exception ex)
    {
        //_logger.LogError(ex, "Handle.Exception on telegram-bot");
    }
}