using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Application.UI;
using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class ParkingController : ReservationControllerBase
{
    public ParkingController(ReservationFlowService flow, ReservationUiBuilder ui) 
        : base(flow, ui) { }

    [Action("/parking", "reserve parking")]
    public async Task StartParkingFlow()
    {
        var state = new BotSessionState { SelectedSpaceTypeId = 1 };
        await SelectRentalTime(state, TimeSelectionStage.None, null);
    }
}