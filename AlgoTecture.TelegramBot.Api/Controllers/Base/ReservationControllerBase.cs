using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using Deployf.Botf;
using Telegram.Bot;

namespace AlgoTecture.TelegramBot.Api.Controllers.Base;

public abstract class ReservationControllerBase : BotController
{
    protected readonly IReservationFlowService _flowService;

    protected ReservationControllerBase(
        IReservationFlowService flowService)
    {
        _flowService = flowService;
    }

    protected async Task DeletePreviousMessageIfNeeded(BotSessionState state, long chatId)
    {
        if (state.MessageId == 0) return;
        await Client.DeleteMessageAsync(chatId, state.MessageId);
        state.MessageId = 0;
    }
    
    protected async Task DeletePreviousLocationMessageIfNeeded(BotSessionState state, long chatId)
    {
        if (state.LocationMessageId == 0) return;
        await Client.DeleteMessageAsync(chatId, state.LocationMessageId);
        state.LocationMessageId = 0;
    }
}