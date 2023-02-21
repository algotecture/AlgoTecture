using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Space.Interfaces;
using AlgoTecture.Libraries.UtilizationType;
using AlgoTecture.Persistence.Core.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Volo.Abp.Modularity;

namespace AlgoTecture.TelegramBot.Controllers;

[DependsOn(typeof(GeoAdminSearcher))]
public class TelegramBotController : BotController
{
    private readonly GeoAdminSearcher _geoAdminSearcher;
    readonly BotfOptions _options;
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;
    private readonly IUtilizationTypeGetter _utilizationTypeGetter;
    private readonly IUnitOfWork _unitOfWork;

    readonly PagingService _pagingService;

    public TelegramBotController(GeoAdminSearcher geoAdminSearcher, ITelegramUserInfoService telegramUserInfoService,
        ITelegramToAddressResolver telegramToAddressResolver, ISpaceGetter spaceGetter, IUnitOfWork unitOfWork, PagingService pagingService, IUtilizationTypeGetter utilizationTypeGetter)
    {
        _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
        _telegramUserInfoService = telegramUserInfoService ?? throw new ArgumentNullException(nameof(telegramUserInfoService));
        _telegramToAddressResolver = telegramToAddressResolver ?? throw new ArgumentNullException(nameof(telegramToAddressResolver));
        _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
        _unitOfWork = unitOfWork;
        _pagingService = pagingService;
        _utilizationTypeGetter = utilizationTypeGetter;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var fullUserName = Context.GetUserFullName();
        var addTelegramUserInfoModel = new AddTelegramUserInfoModel
        {
            TelegramUserId = userId,
            TelegramChatId = chatId,
            TelegramUserName = userName,
            TelegramUserFullName = fullUserName
        };

        _ = await _telegramUserInfoService.Create(addTelegramUserInfoModel);

        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        Button("I want to rent", Q(PressToRentButton));
        Button("I have a booking", Q(PressTryToFindButton));
    }

    [Action]
    private async Task PressToRentButton()
    {
        var utilizationTypes = (await _utilizationTypeGetter.GetAll()).Skip(6).ToList();

        var utilizationTypeToTelegramList = new List<UtilizationTypeToTelegramOut>();
        foreach (var utilizationType in utilizationTypes)
        {
            var utilizationTypeOut = new UtilizationTypeToTelegramOut
            {
                Name = utilizationType.Name,
                Id = utilizationType.Id
            };

            utilizationTypeToTelegramList.Add(utilizationTypeOut);

            RowButton(utilizationType.Name, Q(PressToRentTargetUtilizationButton, utilizationType.Id, default(int)));   
        }
        RowButton("Go Back", Q(Start));

        PushL("Choose your boat");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToChangingUtilizationType()
    {
       
    }

    [Action]
    private async Task PressToCalendarButton()
    {
        await Calendar("");
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

        RowButton("Go Back", Q(PressToRentButton));

        PushL("Choose your boat");
        await SendOrUpdate();
    }

    [Action]
    private async Task Calendar(string state)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        PushL("Pick the time");

        var now = DateTime.Now;
        new CalendarMessageBuilder()
            .Year(now.Year).Month(now.Month).Day(now.Day)
            .Depth(CalendarDepth.Days)
            .SetState(state)
            .OnNavigatePath(s => Q(Calendar, s))
            .OnSelectPath(d => Q(DT, d.ToBinary().Base64()))
            .SkipHour(d => d.Hour < 10 || d.Hour > 19)
            .SkipDay(d => d.DayOfWeek == DayOfWeek.Sunday || d.DayOfWeek == DayOfWeek.Saturday)
            .SkipMinute(d => (d.Minute % 15) != 0)
            .SkipYear(y => y < DateTime.Now.Year)
            .FormatMinute(d => $"{d:HH:mm}")
            .FormatText((dt, depth, b) =>
            {
                b.PushL($"Select {depth}");
                b.PushL($"Current state: {dt}");
            })
            .Build(Message, new PagingService());

        RowButton("Go Back", Q(Start));
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
        RowButton("Go Back", Q(PressToRentTargetUtilizationButton,default(int), message.MessageId));
        await SendOrUpdate();
    }


    [Obsolete]
    [Action]
    private async Task PressToRentButton1()
    {
        PushL("Enter the address or part of the address");
        await Send();

        var term = await AwaitText();
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var telegramToAddressList = new List<TelegramToAddressModel>();


        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            var telegramToAddressModel = new TelegramToAddressModel
            {
                FeatureId = label.featureId,
                latitude = label.lat,
                longitude = label.lon,
                Address = label.label
            };
            telegramToAddressList.Add(telegramToAddressModel);
            RowButton(label.label, Q(PressAddressToRentButton, label.featureId));
        }

        if (!labels.Any())
        {
            RowButton("Try again", Q(PressToRentButton));
            await Send("Nothing found");
        }
        else
        {
            _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);
            await Send("Choose the right address");
        }
    }

    [Obsolete]
    [Action]
    private async Task PressAddressToRentButton(string geoAdminFeatureId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetAddress = _telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value).FirstOrDefault(x => x.FeatureId == geoAdminFeatureId);

        var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        var targetSpace = await _spaceGetter.GetByCoordinates(targetAddress.latitude, targetAddress.longitude);
        //only for parking
        if (targetSpace == null)
        {
            var newSpace = new Space
            {
                UtilizationTypeId = 1,
                Latitude = targetAddress.latitude,
                Longitude = targetAddress.longitude,
                SpaceAddress = targetAddress.Address
            };
            var spaceEntity = await _unitOfWork.Spaces.Add(newSpace);
            // await _unitOfWork.CompleteAsync();
            var newSubSpaceId = Guid.NewGuid();
            var newSpaceProperty = new SpaceProperty
            {
                SpaceId = spaceEntity.Id,
                SpacePropertyId = Guid.NewGuid(),
                SubSpaces = new List<SubSpace>()
                {
                    new SubSpace
                    {
                        OwnerId = user.Id,
                        SubSpaceId = newSubSpaceId,
                        SubSpaceIdHash = newSubSpaceId.GetHashCode(),
                        UtilizationTypeId = 11,
                    }
                }
            };
            newSpace.SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty);
            await _unitOfWork.CompleteAsync();
            _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);
            PressGetSubSpacePropertiesButton(spaceEntity.Id, newSpaceProperty.SubSpaces.First().SubSpaceIdHash);
            //await Send(targetAddress.Address);
        }
        else
        {
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);
            var userSubSpaces = targetSpaceProperty.SubSpaces.Where(x => x.OwnerId == user.Id).ToList();
            if (userSubSpaces.Any())
            {
                var counter = 1;
                foreach (var userSubSpace in userSubSpaces)
                {
                    Button($"({counter})", Q(PressGetSubSpacePropertiesButton, targetSpace.Id, userSubSpace.SubSpaceIdHash));
                }
            }

            _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);

            await Send("Your parking spaces at this address");
        }
    }

    [Action]
    private async Task PressGetSubSpacePropertiesButton(long spaceId, int subSpaceIdHash)
    {
        var targetSpace = await _unitOfWork.Spaces.GetById(spaceId);
        if (targetSpace == null) return;

        var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);
        var targetSubSpace = targetSpaceProperty.SubSpaces.FirstOrDefault(x => x.SubSpaceIdHash == subSpaceIdHash);
        if (targetSubSpace == null) return;

        Button("Update", Q(PressToRentButton));
        Button("Upload photo", Q(PressToRentButton));
        Button("Remove", Q(PressToRentButton));
        await Send($"{targetSpace.SpaceAddress}{Environment.NewLine}Area: {targetSubSpace.Area}{Environment.NewLine}Contract: none ");
    }


    [Action]
    private async Task PressTryToFindButton()
    {
        PushL("Enter the address or part of the address");
        await Send();
        var term = await AwaitText();

        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            RowButton(label.label, Q(PressAddressButton));
        }

        await Send("You won");
    }


    [Action]
    private async Task PressAddressButton()
    {
        PushL("Have a good day");
        await Send();
    }

    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        var z = Context.Update.Message.Photo;
        var stream = System.IO.File.Open($"/Users/sipakov/Documents/repositories/MyProjects/AlgoTecture/AlgoTecture.TelegramBot/1.jpg",
            FileMode.OpenOrCreate);
        await Client.GetInfoAndDownloadFileAsync(z[2].FileId, stream);
        if (Context.Update.Type == UpdateType.Message && Context.Update.Message.Type == MessageType.Document)
        {
            // var stream = System.IO.File.Open("file.txt");
            // await Client.GetInfoAndDownloadFileAsync(Context.Update.Message.Document.FileId, stream);
        }
    }
}