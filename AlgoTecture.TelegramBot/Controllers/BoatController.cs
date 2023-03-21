using AlgoTecture.Domain.Models;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace AlgoTecture.TelegramBot.Controllers;

public class BoatController : BotController, IBoatController
{
    private readonly ISpaceGetter _spaceGetter;
    private readonly IServiceProvider _serviceProvider;

    public BoatController(ISpaceGetter spaceGetter, IServiceProvider serviceProvider)
    {
        _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
        _serviceProvider = serviceProvider;
    }

    [Action]
    public async Task PressToMainBookingPage(BotState botState)
    {
        RowButton("See available time", Q(PressToSeeAvailableTimeToRent, botState));
        RowButton("See available boats", Q(PressToRentTargetUtilizationButton, botState));
        RowButton("Make a reservation", Q(PressToBook, botState));

        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();

        RowButton("Go Back", Q(mainControllerService.PressToRentButton));

        PushL("Reservation");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToRentTargetUtilizationButton(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        if (botState.MessageId != default)
        {
            await Client.DeleteMessageAsync(chatId, botState.MessageId);
        }

        const int boatTargetOfSpaceId = 12;

        var targetSpaces = await _spaceGetter.GetByType(boatTargetOfSpaceId);

        var spaceToTelegramOutList = new List<SpaceToTelegramOut>();

        foreach (var space in targetSpaces)
        {
            var spaceToTelegramOut = new SpaceToTelegramOut
            {
                Name = JsonConvert.DeserializeObject<SpaceProperty>(space.SpaceProperty)?.Name,
                SpaceId = space.Id
            };

            spaceToTelegramOutList.Add(spaceToTelegramOut);

            botState.SpaceId = space.Id;

            Button(spaceToTelegramOut.Name, Q(PressToSelectTheBoatButton, botState));
        }

        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        PushL("Choose Your Desired Boat");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToSeeAvailableTimeToRent(BotState botState)
    {
        await Calendar("", botState, false, RentTimeState.Non);
    }

    [Action]
    private async Task PressToBook(BotState botState)
    {
        RowButton("Rental start time", Q(PressToChooseTheDate, botState, RentTimeState.StartRent));
        RowButton("Rental end time", Q(PressToChooseTheDate, botState, RentTimeState.EndRent));
        RowButton("Choose a boat", Q(PressToRentTargetUtilizationButton, botState));

        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        PushL("Reservation");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToChooseTheDate(BotState botState, RentTimeState rentTimeState)
    {
        await Calendar("", botState, false, rentTimeState);
    }

    [Action]
    private async Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime)
    {
        PushL("Enter the rental start time (in hh:mm format, for example, 14:15)");
        await Send();
        var time = await AwaitText();

        botState.StartRent = rentTimeState == RentTimeState.StartRent ? DateTimeParser.GetDateTime(dateTime, time) : botState.StartRent;
        botState.EndRent = rentTimeState == RentTimeState.EndRent ? DateTimeParser.GetDateTime(dateTime, time) : botState.EndRent;
        RowButton(
            botState.StartRent != null ? $"{botState.StartRent.Value:dddd, MMMM dd yyyy HH:mm}"
                : "Rental start time", Q(PressToChooseTheDate, botState, RentTimeState.StartRent));
        RowButton(
            botState.EndRent != null ? $"{botState.EndRent.Value:dddd, MMMM dd yyyy HH:mm}"
                : "Rental end time", Q(PressToChooseTheDate, botState, RentTimeState.EndRent));
        RowButton("Choose a boat", Q(PressToRentTargetUtilizationButton, botState));

        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        await Send("Reservation");
    }

    [Action]
    private async Task Calendar(string state, BotState botState, bool isNavigateBetweenMonths, RentTimeState rentTimeState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var now = DateTime.UtcNow;

        var calendar = new CalendarMessageBuilder();
        if (isNavigateBetweenMonths)
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, botState, true, rentTimeState))
                .OnSelectPath(date =>
                    Q(PressToEnterTheStartEndTime, botState, rentTimeState, date));
        }
        else
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, botState, true, rentTimeState))
                .OnSelectPath(date =>
                    Q(PressToEnterTheStartEndTime, botState, rentTimeState, date))
                .SkipDay(d => d.Day < now.Day);
        }

        calendar.Build(Message, new PagingService());

        RowButton("Go Back", Q(PressToBook, botState));
        PushL("Pick the date");
        await SendOrUpdate();
    }

    [Action]
    private async Task DT(string dt)
    {
        var datetime = DateTime.FromBinary(dt.Base64());
        Button("Select new", "/start");
        Push(datetime.ToString());
        await SendOrUpdate();
    }


    [Action]
    private async Task PressToSelectTheBoatButton(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetSpace = await _spaceGetter.GetById(botState.SpaceId);

        var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

        if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));

        //find the file without extension
        var pathToBoatImage =
            System.IO.Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Boats", $"{targetSpaceProperty.SpacePropertyId}.jpeg");

        await using var stream = System.IO.File.OpenRead(pathToBoatImage);
        var inputOnlineFile = new InputOnlineFile(stream, targetSpaceProperty.Name);

        var message = await Client.SendPhotoAsync(
            chatId: chatId,
            photo: inputOnlineFile,
            caption: $"<b>{targetSpaceProperty.Description}</b>",
            ParseMode.Html
        );

        botState.MessageId = message.MessageId;
        PushL($"{targetSpaceProperty.Name}");
        RowButton("Go Back", Q(PressToRentTargetUtilizationButton, botState));
        await SendOrUpdate();
    }

    [On(Handle.Exception)]
    public async Task Exception()
    {
        var s = Context;
        PushL(
            "Ooops");
        await SendOrUpdate();
    }

    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        PushL(
            "I'm sorry, but I'm not yet able to understand natural language requests at the moment. Please provide specific instructions using the AlgoTecture bot interface for me to execute tasks.");
        await SendOrUpdate();
    }
}