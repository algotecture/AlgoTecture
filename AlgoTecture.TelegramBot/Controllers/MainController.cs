using System.Globalization;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.UtilizationTypes;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace AlgoTecture.TelegramBot.Controllers;

public class MainController : BotController, IMainController
{
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    private readonly IUtilizationTypeGetter _utilizationTypeGetter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MainController> _logger;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;
    private readonly IServiceProvider _serviceProvider;
    private readonly IBoatController _boatController;
    private readonly ISpaceService _spaceService;
    private readonly IParkingController _parkingController;
    private readonly ICityParkingController _cityParkingController;

    private Dictionary<string, string> utilizationTypeToSmile = new()
    {
        { "Residential", "🏠" }, { "Parking", "🚙" }, { "City Parking", "🅿️" }, { "Boat", "🚤" }, { "Coworking", "🏢" }
    };

    public MainController(ITelegramUserInfoService telegramUserInfoService, IBoatController boatController,
        IUtilizationTypeGetter utilizationTypeGetter,
        IUnitOfWork unitOfWork, ILogger<MainController> logger, IGeoAdminSearcher geoAdminSearcher,
        ITelegramToAddressResolver telegramToAddressResolver,
        ISpaceGetter spaceGetter, IServiceProvider serviceProvider, ISpaceService spaceService,
        IParkingController parkingController, ICityParkingController cityParkingController)
    {
        _telegramUserInfoService = telegramUserInfoService;
        _boatController = boatController;
        _utilizationTypeGetter = utilizationTypeGetter;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _geoAdminSearcher = geoAdminSearcher;
        _telegramToAddressResolver = telegramToAddressResolver;
        _spaceGetter = spaceGetter;
        _serviceProvider = serviceProvider;
        _spaceService = spaceService;
        _parkingController = parkingController;
        _cityParkingController = cityParkingController;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var fullUserName = Context.GetUserFullName();

        var addTelegramUserInfoModel = new AddOrUpdateTelegramUserInfoModel
        {
            TelegramUserId = userId,
            TelegramChatId = chatId,
            TelegramUserName = userName,
            TelegramUserFullName = fullUserName
        };

        var user = await _telegramUserInfoService.AddOrUpdate(addTelegramUserInfoModel);

        _logger.LogInformation($"User {user.TelegramUserFullName} logged in by telegram bot");

        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        var parkingControllerService = _serviceProvider.GetRequiredService<IParkingController>();
        RowButton("🔍 Explore & 📌 Reserve Spaces",
            Q(parkingControllerService.EnterAddress, new BotState { UtilizationTypeId = 15 }));
        RowButton("📅 Control & 📝 Manage Reservations", Q(PressToFindReservationsButton));
    }

    [Action]
    public async Task PressToFindReservationsButton()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        var reservations = await _unitOfWork.Reservations.GetReservationsByUserId(user.Id);

        var reservationList = new List<ReservationToTelegramOut>();

        foreach (var reservation in reservations)
        {
            var reservationToTelegram = new ReservationToTelegramOut
            {
                Id = reservation.Id,
                DateTimeFrom = $"{reservation.ReservationFromUtc!.Value + TimeSpan.FromHours(2):dd-MM-yyyy HH:mm}",
                Description = reservation.Description,
                TotlaPrice = reservation.TotalPrice,
                PriceCurrency = reservation.PriceSpecification?.PriceCurrency,
                Address = string.IsNullOrEmpty(reservation.Space?.SpaceAddress)
                    ? reservation.Description
                    : reservation.Space?.SpaceAddress
            };
            //only for demo utc +2
            reservationList.Add(reservationToTelegram);
            var description =
                $"{reservationToTelegram.Address.Substring(0,10)}..., \n\r{reservationToTelegram.DateTimeFrom}, {reservationToTelegram.TotlaPrice} \n\r" +
                $"{reservationToTelegram.PriceCurrency?.ToUpper()}";
            RowButton(description, Q(PressToManageContract, new BotState() { ReservationId = reservation.Id }));
        }

        RowButton("Go Back", Q(Start));

        PushL("Reservations");
        await SendOrUpdate();
    }

    [Action]
    public async Task PressToManageContract(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        var reservation = await _unitOfWork.Reservations.GetById(botState.ReservationId.Value);

        var space = await _spaceGetter.GetById(reservation.SpaceId);
        var spaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(space.SpaceProperty);


        var imageNames = spaceProperty.Images;

        if (imageNames != null && imageNames.Any() && space.UtilizationTypeId == 15)
        {
            var pathToBoatImage =
                System.IO.Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Spaces", "0",
                    "10000000-0000-46ee-b65b-137aa08b3c9a.png");

            await using var stream = File.OpenRead(pathToBoatImage);
            var inputOnlineFile = new InputOnlineFile(stream, spaceProperty.Name);

            var message = await Client.SendPhotoAsync(
                chatId: chatId,
                photo: inputOnlineFile,
                caption: $"<b>{spaceProperty.Name}</b>" + "\n" +
                         $"<b>Button above image 👆</b>",
                ParseMode.Html
            );

            botState.MessageId = message.MessageId;
        }

        PushL($"📅 Reservation date: {reservation.ReservationToUtc.Value + TimeSpan.FromHours(2):dddd, MMMM dd}\n\r" +
              $"⌚ Time: {reservation.ReservationFromUtc.Value + TimeSpan.FromHours(2):HH:mm}" + " - " + $"{reservation.ReservationToUtc.Value + TimeSpan.FromHours(2):HH:mm}\n\r" +
              $"📍 Location: {space.SpaceAddress}\n\r" +
              $"📍 Parking Number: {spaceProperty.Name}\n\r" +
              $"🔢 Confirmation Number: {reservation.ReservationUniqueIdentifier}\n\r \n\r" +
              "If you have any questions or need to make changes to your reservation, " +
              "please feel free to contact our support team at @AlgoTecture." +
              " Thank you for choosing our service! 🙌");
        RowButton("Go Back", Q(PressToFindReservationsButton));
        await SendOrUpdate();
    }

    [Action]
    public async Task PressAddressToRentButton(TelegramToAddressModel telegramToAddressModel, BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetAddress =
            _telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)!.FirstOrDefault(x =>
                x.FeatureId == telegramToAddressModel.FeatureId);
        //only for demo
        if (botState.UtilizationTypeId == 15)
        {
            //only for demo 
            var targetSpaces = await _spaceGetter.GetByType(botState.UtilizationTypeId);

            var nearestParkingSpaces = await _spaceService.GetNearestSpaces(targetSpaces,
                telegramToAddressModel.latitude, telegramToAddressModel.longitude, 7);

            if (nearestParkingSpaces.Any())
            {
                var parkingControllerService = _serviceProvider.GetRequiredService<IParkingController>();
                var counter = 1;
                foreach (var nearestParkingSpace in nearestParkingSpaces)
                {
                    var tamModel = new TelegramToAddressModel
                    {
                        latitude = nearestParkingSpace.Value.Latitude,
                        longitude = nearestParkingSpace.Value.Longitude
                    };
                    RowButton(
                        $"Parking at {telegramToAddressModel.Address} in {nearestParkingSpace.Key} meters. Tap to details",
                        Q(parkingControllerService.PressToParkingButton, tamModel, botState));
                    counter++;
                }

                RowButton("Go Back", Q(EnterAddress, botState));

                PushL($"Found!");
            }
            else
            {
                RowButton("Try again"!);
                await Send("Nothing found");
            }
        }

        else
        {
            var targetSpace = await _spaceGetter.GetByCoordinates(targetAddress!.latitude, targetAddress.longitude);

            _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);

            if (targetSpace == null)
            {
                var formattedGeoAdminFeatureId = !string.IsNullOrEmpty(telegramToAddressModel.FeatureId)
                    ? telegramToAddressModel.FeatureId.Split('_')[0]
                    : string.Empty;
                PushL("This space will soon be available for rent. Go to space properties or /start to try again");

                var urlToAddressProperties =
                    $"https://algotecture.io/webapi-qrcode/spacePropertyPage?featureId={formattedGeoAdminFeatureId}&label={telegramToAddressModel.Address}";
                Button(WebApp("Look on the map", urlToAddressProperties));
                //RowButton("Go to space properties", urlToAddressProperties);
                await SendOrUpdate();
            }
            else
            {
                var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

                var boatControllerService = _serviceProvider.GetRequiredService<IBoatController>();

                RowButton("Rent!", Q(boatControllerService.PressToEnterTheStartEndTime, new BotState
                {
                    SpaceId = targetSpace.Id,
                    SpaceName = targetSpaceProperty!.Name
                }, RentTimeState.None, null!));
                RowButton("Go to main", Q(Start));

                PushL(
                    $"Found! {targetSpace.UtilizationType?.Name}: {targetSpaceProperty?.Name}. {targetSpaceProperty?.Description}");
            }
        }
    }

    [Action]
    public async Task EnterAddress(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        PushL("Enter the address or part of the address");
        await SendOrUpdate();

        var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

        var user = await _unitOfWork.TelegramUserInfos.GetByTelegramChatId(chatId.Value);

        _logger.LogInformation($"User {user?.TelegramUserFullName} entered text {address} to search for an address");

        var telegramToAddressList = new List<TelegramToAddressModel>();

        var labels = (await _geoAdminSearcher.GetAddress(address)).ToList();

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
            RowButton(label.label, Q(PressAddressToRentButton, telegramToAddressModel, botState));
        }

        if (!labels.Any())
        {
            RowButton("Try again"!);
            await Send("Nothing found");
        }
        else
        {
            _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);

            if (_telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)!.Count > 1)
            {
                await Send("Choose the right address");
            }
            else
            {
                await Send("Address");
            }
        }
    }

    [Action]
    private void RedirectToAddressPropertiesButton(string geoAdminFeatureId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
    }


    [On(Handle.Exception)]
    public void Exception(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        RowButton("Enter address", Q(EnterAddress, new BotState()));
        RowButton("Go back", Q(Start));

        PushL(
            "I'm sorry, but I'm not yet able to understand natural language requests at the moment. Enter an address to search for the space");
        await SendOrUpdate();
    }

    [On(Handle.ChainTimeout)]
    void ChainTimeout(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }
}