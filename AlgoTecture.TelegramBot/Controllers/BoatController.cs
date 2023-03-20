using AlgoTecture.Domain.Models;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
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
    public async Task PressToMainBookingPage(int utilizationTypeId, int messageId)
    {
        RowButton("See available time", Q(PressToSeeAvailableTimeToRent, utilizationTypeId));
        RowButton("See available boats", Q(PressToRentTargetUtilizationButton, utilizationTypeId, default(int)));
        RowButton("Book", Q(PressToBook, utilizationTypeId));

        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();

        RowButton("Go Back", Q(mainControllerService.PressToRentButton));

        PushL("Booking");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToRentTargetUtilizationButton(int utilizationType, int messageId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        if (messageId != default(int))
        {
            await Client.DeleteMessageAsync(chatId, messageId);
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

            Button(spaceToTelegramOut.Name, Q(PressToSelectTheBoatButton, space.Id));
        }

        RowButton("Go Back", Q(PressToMainBookingPage, utilizationType, default(int)));

        PushL("Choose Your Desired Boat");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToSeeAvailableTimeToRent(int utilizationTypeId)
    {
        await Calendar("", utilizationTypeId, false);
    }
    
    [Action]
    private async Task PressToBook(int utilizationTypeId)
    {
        RowButton("Rental start time", Q(PressToChooseTheDate, utilizationTypeId));
        RowButton("Rental end time", Q(PressToChooseTheDate,utilizationTypeId));
        RowButton("Choose a boat", Q(PressToRentTargetUtilizationButton, utilizationTypeId, default(int)));
        
        RowButton("Go Back", Q(PressToMainBookingPage, utilizationTypeId, default(int)));

        PushL("Booking");
        await SendOrUpdate();   
    }

    [Action]
    private async Task PressToChooseTheDate(int utilizationTypeId)
    {
        await Calendar("", utilizationTypeId, false);
    }

    [Action]
    private async Task PressToEnterTheStartTime(int utilizationTypeId)
    {
        PushL("Enter the rental start time (in hh:mm format, for example, 14:15)");
        await Send();
        var term = await AwaitText();
       
        RowButton(term, Q(PressToChooseTheDate, utilizationTypeId));
      //  RowButton("Rental end time", Q(PressToChooseTheDate,utilizationTypeId));
       // RowButton("Choose a boat", Q(PressToRentTargetUtilizationButton, utilizationTypeId, default(int)));
        
      //  RowButton("Go Back", Q(PressToMainBookingPage, utilizationTypeId, default(int)));

        await Send("text");   
    }

    [Action]
    private async Task Calendar(string state, int utilizationTypeId, bool isNavigateBetweenMonths)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        PushL("Pick the time");

        var now = DateTime.Now;

        var calendar = new CalendarMessageBuilder();
        if (isNavigateBetweenMonths)
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, utilizationTypeId, true))
                .OnSelectPath(d => Q(PressToEnterTheStartTime, utilizationTypeId));
        }
        else
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, utilizationTypeId, true))
                .OnSelectPath(d => Q(PressToEnterTheStartTime, utilizationTypeId))
                .SkipDay(d => d.Day < now.Day);
        }

        calendar.Build(Message, new PagingService());

        RowButton("Go Back", Q(PressToBook, utilizationTypeId));
        PushL("Choose date");
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
    private async Task PressToSelectTheBoatButton(long spaceId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetSpace = await _spaceGetter.GetById(spaceId);

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

        PushL($"{targetSpaceProperty.Name}");
        RowButton("Go Back", Q(PressToRentTargetUtilizationButton, default(int), message.MessageId));
        await SendOrUpdate();
    }


    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        var s = Context.Items;
        PushL(
            "I'm sorry, but I'm not yet able to understand natural language requests at the moment. Please provide specific instructions using the AlgoTecture bot interface for me to execute tasks.");
        await SendOrUpdate();
    }
}